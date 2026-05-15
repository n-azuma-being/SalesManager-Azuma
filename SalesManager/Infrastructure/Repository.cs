using System;
using System.Data;
using System.Data.SQLite;

namespace SalesManager.Infrastructure {
    public static class Repository {
        //csvインポート
        public static void CreateTable() {
            string sql = @"
                CREATE TABLE IF NOT EXISTS categories (
                    category_id INTEGER PRIMARY KEY,
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

        //売上データ
        public static DataTable GetSalesSummary(string start, string end) {
            //まずはこの2つの日付だけで絞り込む
            string sql = @"
                SELECT
                    s.sale_date AS 売上日,
                    st.store_name AS 店舗名,
                    IFNULL(p.product_name, '未登録商品') AS 商品名,
                    s.unit_price AS 単価,
                    SUM(s.quantity) AS 販売数,
                    SUM(IFNULL(s.unit_price, 0) * s.quantity) AS 売上額
                FROM sales s
                LEFT JOIN products p ON s.product_id = p.product_id
                LEFT JOIN stores st ON s.store_id = st.store_id
                WHERE s.sale_date BETWEEN @start AND @end
                GROUP BY s.sale_date,st.store_name,p.product_name,s.unit_price
                ORDER BY s.sale_date DESC";

            var parameters = new[] {
                new SQLiteParameter("@start", start),
                new SQLiteParameter("@end", end)
            };

            return DbManager.ExecuteQuery(sql, parameters);
        }

        //商品データ
        public static DataTable GetProductList() {
            string sql = @"
                SELECT
                    p.product_id AS 商品ID,
                    p.product_name AS 商品名,
                    p.unit_price AS 単価,
                    IFNULL(s.stock, 0) AS 在庫数,
                    IFNULL(s.updated_at, '未更新') AS 更新日時
                FROM products p
                LEFT JOIN stocks s ON p.product_id = s.product_id
                ORDER BY p.product_id ASC";

            return DbManager.ExecuteQuery(sql);
        }

        //要発注
        public static DataTable GetLowStockItems(int LowStockThreshold) {
            string sql = @"
            SELECT 
                p.product_id AS 商品ID, 
                p.product_name AS 商品名, 
                s.stock AS 在庫数,
                p.unit_price AS 単価
            FROM products p
            INNER JOIN stocks s ON p.product_id = s.product_id
            WHERE s.stock <= @threshold
            ORDER BY s.stock ASC";

            var parameters = new[] {
            new SQLiteParameter("@threshold", LowStockThreshold)
            };

            return DbManager.ExecuteQuery(sql, parameters);
        }

        //週次報告書
        public static DataTable GetWeeklyData(DateTime startDate, DateTime endDate) {
            string sql = @"
                SELECT
                    s.product_id AS 商品ID,
                    p.product_name AS 商品名,
                    SUM(s.quantity) AS 先週販売数,
                    MAX(st.stock) + SUM(s.quantity) AS 現在在庫数,
                    MAX(st.stock) AS 販売後在庫,
                    CASE 
                        WHEN MAX(st.stock) <= 5 THEN '発注対象'
                        ELSE ''
                    END AS 発注対象,
                    SUM(s.unit_price * s.quantity) AS 累計売上金額
                FROM sales s
                INNER JOIN products p ON s.product_id = p.product_id
                INNER JOIN stocks st ON s.product_id = st.product_id
                WHERE date(s.sale_date) BETWEEN date(@start) AND date(@end)
                GROUP BY s.product_id, p.product_name, s.unit_price;";

            // DateTimeを文字列に変換してセットする
            SQLiteParameter[] parameters = new SQLiteParameter[] {
                 new SQLiteParameter("@start", startDate.ToString("yyyy-MM-dd")),
                 new SQLiteParameter("@end", endDate.ToString("yyyy-MM-dd"))
             };

            return DbManager.ExecuteQuery(sql, parameters);
        }

        //売上データの最新日付
        public static DateTime GetMaxSalesDate() {
            string sql = "SELECT MAX(date(sale_date)) FROM sales";
            DataTable dt = DbManager.ExecuteQuery(sql);

            if (dt.Rows.Count > 0 && dt.Rows[0][0] != DBNull.Value) {
                return Convert.ToDateTime(dt.Rows[0][0]);
            }

            //データが1件もない場合はtoday
            return DateTime.Today;
        }

        //売上データの最古日付
        public static DateTime GetMinSalesDate() {
            string sql = "SELECT MIN(sale_date) FROM sales";
            DataTable dt = DbManager.ExecuteQuery(sql);

            if (dt.Rows.Count > 0 && dt.Rows[0][0] != DBNull.Value) {
                return Convert.ToDateTime(dt.Rows[0][0]);
            }

            //データが1件もない場合はtoday
            return DateTime.Today;
        }
    }
}
