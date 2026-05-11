using System;
using System.Data;
using System.Data.SQLite;
using SalesManager.Infrastructure;

namespace SalesManager.Services {
    public class SalesService {

        /// <summary>
        /// 売上データを取得
        /// </summary>
        public DataTable GetSales(string start, string end) {
            //まずはこの2つの日付だけで絞り込む
            string sql = @"
                SELECT
                    s.sale_date AS 売上日,
                    IFNULL(p.product_name, '未登録商品') AS 商品名,
                    SUM(s.quantity) AS 販売数,
                    SUM(IFNULL(p.unit_price, 0) * s.quantity) AS 売上額
                FROM sales s
                LEFT JOIN products p ON s.product_id = p.product_id
                WHERE s.sale_date BETWEEN @start AND @end
                GROUP BY s.sale_date, p.product_name
                ORDER BY s.sale_date DESC";

                    var parameters = new[] {
                new SQLiteParameter("@start", start),
                new SQLiteParameter("@end", end)
            };

            return DbManager.ExecuteQuery(sql, parameters);
        }

        /// <summary>
        /// 売上データの中で一番新しい日付を返す
        /// </summary>
        public DateTime GetLatestSalesDate() {
            string sql = "SELECT MAX(sale_date) FROM sales";
            DataTable dt = DbManager.ExecuteQuery(sql);

            if (dt.Rows.Count > 0 && dt.Rows[0][0] != DBNull.Value) {
                return Convert.ToDateTime(dt.Rows[0][0]);
            }

            //データが1件もない場合はtoday
            return DateTime.Today;
        }
    }
}