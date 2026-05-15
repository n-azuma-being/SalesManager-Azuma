using SalesManager.Infrastructure;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;

namespace SalesManager.Services {
    /// <summary>
    /// データ取込画面の内部処理を行うクラス
    /// </summary>
    public class ImportService {

        /// <summary>
        /// 全テーブルの初期化
        /// </summary>
        public void InitializeDatabase() {
            Repository.CreateTable();
        }

        /// <summary>
        /// ファイル入力
        /// </summary>
        public void ExecuteImport(string[] paths) {
            //存在チェック
            string productPath = paths.FirstOrDefault(p => Path.GetFileName(p).ToLower().Contains("products"));
            string inventoryPath = paths.FirstOrDefault(p => Path.GetFileName(p).ToLower().Contains("inventory"));
            string salesPath = paths.FirstOrDefault(p => Path.GetFileName(p).ToLower().Contains("sales"));

            List<string> missingFiles = new List<string>();
            if (string.IsNullOrEmpty(productPath)) missingFiles.Add("・商品マスター (products.csv)");
            if (string.IsNullOrEmpty(inventoryPath)) missingFiles.Add("・在庫データ (inventory.csv)");
            if (string.IsNullOrEmpty(salesPath)) missingFiles.Add("・売上データ (sales_YYYYMMDD.csv)");

            if (missingFiles.Count > 0) {
                MessageManager.ShowInfo($"以下の必要なファイルが選択されていません。{Environment.NewLine}" + string.Join($"{Environment.NewLine}", missingFiles));
                return; // 処理を中断
            }

            // DB整合性のために順番を固定して実行
            string storePath = paths.FirstOrDefault(p => Path.GetFileName(p).ToLower().Contains("store"));
            if (storePath != null) ImportStores(storePath);

            // 商品 -> 在庫 -> 売上 の順
            ImportProducts(productPath);
            ImportInventory(inventoryPath);
            ImportSales(salesPath);

            MessageManager.ShowInfo("売上集計が完了しました。");
        }


        /// <summary>
        /// インポートの一括実行
        /// </summary>
        public void ExecuteImport(string productPath, string inventoryPath, string salesPath, string storePath = null) {
            if (!ValidateCsvData(productPath, inventoryPath, salesPath, storePath)) {
                throw new Exception("CSVファイルの中身（列数など）が正しくありません。");
            }

            if (!string.IsNullOrEmpty(storePath)) ImportStores(storePath);

            //商品情報のインポート
            try {
                ImportProducts(productPath);
            } catch (Exception ex) {
                MessageManager.ShowError($"商品データの登録に失敗しました。{Environment.NewLine}{ex.Message}");
            }

            //在庫情報のインポート
            try {
                ImportInventory(inventoryPath);
            } catch (Exception ex) {
                MessageManager.ShowError($"在庫データの登録に失敗しました。{Environment.NewLine}{ex.Message}");
            }

        }

        /// <summary>
        /// CSVデータの列数などが正しいか検証
        /// </summary>
        public bool ValidateCsvData(string productPath, string inventoryPath, string salesPath, string storePath) {
            // 商品: ID, 名前, 単価, カテゴリ
            if (!CheckColumns(productPath, 4)) return false;
            // 在庫: 店舗ID, 商品ID, 在庫数
            if (!CheckColumns(inventoryPath, 3)) return false;
            // 売上: 日付, 店舗ID, 商品ID, 数量
            if (!CheckColumns(salesPath, 4)) return false;
            // 店舗: ID, 名前
            if (!string.IsNullOrEmpty(storePath) && !CheckColumns(storePath, 2)) return false;

            return true;
        }

        private bool CheckColumns(string path, int minColumns) {
            if (!File.Exists(path)) return false;
            using (var sr = new StreamReader(path)) {
                string header = sr.ReadLine();
                if (string.IsNullOrEmpty(header)) return false;
                return header.Split(',').Length >= minColumns;
            }
        }

        /// <summary>
        /// 商品情報インポート
        /// </summary>
        public void ImportProducts(string csvPath) {
            var lines = File.ReadAllLines(csvPath);

            var categories = new Dictionary<string, string>();
            var dtCat = DbManager.ExecuteQuery("SELECT category_name, category_id FROM categories");
            foreach (DataRow row in dtCat.Rows) {
                categories[row["category_name"].ToString()] = row["category_id"].ToString();
            }

            DbManager.ExecuteBatch(cmd => {
                for (int i = 1; i < lines.Length; i++) {
                    var cols = lines[i].Split(',');
                    if (cols.Length < 4) continue;

                    string catName = cols[3];
                    string catId;

                    // メモリにあればそれを使う。なければ作成
                    if (categories.ContainsKey(catName)) {
                        catId = categories[catName];
                    } else {
                        // 新規カテゴリ採番
                        catId = (categories.Count + 1).ToString("D4");

                        // カテゴリテーブルへ追加（この cmd は同じトランザクション内）
                        cmd.CommandText = "INSERT INTO categories (category_id, category_name) VALUES (@cid, @cname)";
                        cmd.Parameters.Clear();
                        cmd.Parameters.AddWithValue("@cid", catId);
                        cmd.Parameters.AddWithValue("@cname", catName);
                        cmd.ExecuteNonQuery();

                        categories[catName] = catId; // メモリも更新
                    }

                    // 商品登録
                    cmd.CommandText = "INSERT OR REPLACE INTO products VALUES (@id, @name, @price, @catid)";
                    cmd.Parameters.Clear();
                    cmd.Parameters.AddWithValue("@id", int.Parse(cols[0]));
                    cmd.Parameters.AddWithValue("@name", cols[1]);
                    cmd.Parameters.AddWithValue("@price", decimal.Parse(cols[2]));
                    cmd.Parameters.AddWithValue("@catid", catId);
                    cmd.ExecuteNonQuery();
                }
            });
        }

        /// <summary>
        /// 在庫情報インポート
        /// </summary>
        public void ImportInventory(string csvPath) {
            string[] lines;
            try {
                lines = File.ReadAllLines(csvPath);
            } catch (Exception ex) {
                MessageManager.ShowError($"CSV読み込み失敗: {ex.Message}");
                return;
            }

            string today = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            var stockTotals = new Dictionary<int, int>();

            for (int i = 1; i < lines.Length; i++) {
                var cols = lines[i].Split(',');
                if (cols.Length < 3) continue;
                int productId = int.Parse(cols[1]);
                int stock = int.Parse(cols[2]);

                if (stockTotals.ContainsKey(productId)) stockTotals[productId] += stock;
                else stockTotals[productId] = stock;
            }

            //csv再読み込み時の上書きに対応するため、在庫数から全売上データの販売数を引くことにする
            var dtSales = DbManager.ExecuteQuery("SELECT product_id, SUM(quantity) as total_qty FROM sales GROUP BY product_id");
            foreach (DataRow row in dtSales.Rows) {
                int productId = Convert.ToInt32(row["product_id"]);
                int soldQty = Convert.ToInt32(row["total_qty"]);

                if (stockTotals.ContainsKey(productId)) {
                    stockTotals[productId] -= soldQty;
                } else {
                    stockTotals[productId] = -soldQty;
                }
            }

            DbManager.ExecuteBatch(cmd => {
                foreach (var item in stockTotals) {
                    cmd.CommandText = "INSERT OR REPLACE INTO stocks VALUES (@pid, @stock, @date)";
                    cmd.Parameters.Clear();
                    cmd.Parameters.AddWithValue("@pid", item.Key);
                    cmd.Parameters.AddWithValue("@stock", item.Value);
                    cmd.Parameters.AddWithValue("@date", today);
                    cmd.ExecuteNonQuery();
                }
            });
        }

        /// <summary>
        /// 売上情報インポート
        /// </summary>
        public void ImportSales(string csvPath) {
            var lines = File.ReadAllLines(csvPath);
            var sequenceMap = new Dictionary<string, int>();

            var priceMap = new Dictionary<int, decimal>();
            var dt = DbManager.ExecuteQuery("SELECT product_id, unit_price FROM products");
            foreach (DataRow row in dt.Rows) {
                priceMap[Convert.ToInt32(row["product_id"])] = Convert.ToDecimal(row["unit_price"]);
            }

            DbManager.ExecuteBatch(cmd => {
                for (int i = 1; i < lines.Length; i++) {
                    var cols = lines[i].Split(',');
                    if (cols.Length < 4) continue;

                    string sDate = cols[0];
                    int pId = int.Parse(cols[2]);
                    string dateKey = sDate.Replace("-", "");

                    if (!sequenceMap.ContainsKey(dateKey)) sequenceMap[dateKey] = 1;
                    string saleId = $"{dateKey}{sequenceMap[dateKey]++:D6}";

                    decimal price = priceMap.ContainsKey(pId) ? priceMap[pId] : 0;

                    cmd.CommandText = "INSERT OR REPLACE INTO sales VALUES (@sid, @date, @stid, @pid, @price, @qty)";
                    cmd.Parameters.Clear();
                    cmd.Parameters.AddWithValue("@sid", saleId);
                    cmd.Parameters.AddWithValue("@date", sDate);
                    cmd.Parameters.AddWithValue("@stid", cols[1]);
                    cmd.Parameters.AddWithValue("@pid", pId);
                    cmd.Parameters.AddWithValue("@price", price);
                    cmd.Parameters.AddWithValue("@qty", cols[3]);
                    cmd.ExecuteNonQuery();
                }
            });
        }

        /// <summary>
        /// 店舗情報インポート
        /// </summary>
        public void ImportStores(string csvPath) {
            var lines = File.ReadAllLines(csvPath);
            DbManager.ExecuteBatch(cmd => {
                for (int i = 1; i < lines.Length; i++) {
                    var cols = lines[i].Split(',');
                    if (cols.Length < 2) continue;

                    cmd.CommandText = "INSERT OR REPLACE INTO stores VALUES (@id, @name)";
                    cmd.Parameters.Clear();
                    cmd.Parameters.AddWithValue("@id", cols[0]);
                    cmd.Parameters.AddWithValue("@name", cols[1]);
                    cmd.ExecuteNonQuery();
                }
            });
        }
    }
}