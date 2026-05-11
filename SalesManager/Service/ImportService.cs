using SalesManager.Infrastructure;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace SalesManager.Services {
    public class ImportService {

        /// <summary>
        /// 全テーブルの初期化
        /// </summary>
        public void InitializeDatabase() {
            string sql = @"
                CREATE TABLE IF NOT EXISTS categories (
                    category_id TEXT PRIMARY KEY,
                    category_name TEXT UNIQUE
                );
                CREATE TABLE IF NOT EXISTS products (
                    product_id INTEGER PRIMARY KEY,
                    product_name TEXT,
                    unit_price REAL,
                    category_id TEXT
                );
                CREATE TABLE IF NOT EXISTS stocks (
                    product_id INTEGER PRIMARY KEY,
                    stock INTEGER,
                    updated_at TEXT
                );
                CREATE TABLE IF NOT EXISTS stores (
                    store_id INTEGER PRIMARY KEY,
                    store_name TEXT
                );
                CREATE TABLE IF NOT EXISTS sales (
                    sale_id TEXT PRIMARY KEY,
                    sale_date TEXT,
                    store_id INTEGER,
                    product_id INTEGER,
                    unit_price REAL,
                    quantity INTEGER
                );";
            DbManager.ExecuteNonQuery(sql);
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
                MessageBox.Show($"商品データの登録に失敗しました: {ex.Message}");
            }

            //在庫情報のインポート
            try {
                ImportInventory(inventoryPath);
            } catch (Exception ex) {
                MessageBox.Show($"在庫データの登録に失敗しました: {ex.Message}");
            }

            //売上情報のインポート
            try {
                ImportSales(salesPath);
            } catch (Exception ex) {
                MessageBox.Show($"売上データの登録に失敗しました: {ex.Message}");
            }
        }

        /// <summary>
        /// CSVデータの列数などが正しいか検証
        /// </summary>
        public bool ValidateCsvData(string productPath, string inventoryPath, string salesPath, string storePath) {
            // 商品: ID, 名前, 単価, カテゴリ
            if (!CheckColumns(productPath, 4)) return false;
            // 在庫: 店舗ID, 商品ID, 出庫数
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

            DbManager.ExecuteBatch(cmd =>
            {
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
                MessageManager.ShowError($"CSVファイルの読み込みに失敗しました : {ex.Message}");
                return;
            }

            string today = DateTime.Now.ToString("yyyy-MM-dd");

            var currentStocks = new Dictionary<int, int>();//前回在庫
            try{
                var dt = DbManager.ExecuteQuery("SELECT product_id, stock FROM stocks");

                foreach (DataRow row in dt.Rows) {
                    currentStocks[Convert.ToInt32(row["product_id"])] = Convert.ToInt32(row["stock"]);
                }
            } catch (Exception ex) {
                MessageManager.ShowError($"在庫データの取得に失敗しました : {ex.Message}");
            }

            DbManager.ExecuteBatch(cmd => {
                for (int i = 1; i < lines.Length; i++) {
                    var cols = lines[i].Split(',');
                    if (cols.Length < 3) continue;

                    int pId = int.Parse(cols[1]);
                    int change = int.Parse(cols[2]);

                    int current = currentStocks.ContainsKey(pId) ? currentStocks[pId] : 0;
                    int updated = current - change;
                    currentStocks[pId] = updated;

                    cmd.CommandText = "INSERT OR REPLACE INTO stocks VALUES (@pid, @stock, @date)";
                    cmd.Parameters.Clear();
                    cmd.Parameters.AddWithValue("@pid", pId);
                    cmd.Parameters.AddWithValue("@stock", updated);
                    cmd.Parameters.AddWithValue("@date", today);

                    try{
                        cmd.ExecuteNonQuery();
                    } catch (Exception ex) {
                        MessageManager.ShowError($"在庫データの登録に失敗しました : {ex.Message}");
                    }
                }
            });
        }

        /// <summary>
        /// 売上情報インポート
        /// </summary>
        public void ImportSales(string csvPath) {
            var lines = File.ReadAllLines(csvPath);
            var sequenceMap = new Dictionary<string, int>();

            // 単価マスタを読み込み
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

                    cmd.CommandText = "INSERT INTO sales VALUES (@sid, @date, @stid, @pid, @price, @qty)";
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