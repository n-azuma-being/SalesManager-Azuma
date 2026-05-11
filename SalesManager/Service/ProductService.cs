using ClosedXML.Excel;
using SalesManager.Infrastructure;
using System;
using System.Data;
using System.Data.SQLite;
using System.IO;


namespace SalesManager.Services {
    public class ProductService {

        //発注しきい値
        public const int LowStockThreshold = 5;

        /// <summary>
        /// 商品データを取得
        /// </summary>
        public DataTable GetProduct() {
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

        /// <summary>
        /// 発注書作成
        /// </summary>
        public void RequestOrderReport(string templatePath, string folderPath) {
            DataTable dt = GetLowStockProducts();
            if (dt.Rows.Count == 0) {
                throw new Exception("発注対象の商品がありません。");
            }

            string fileName = "発注書_" + DateTime.Now.ToString("yyyyMMdd") + ".xlsx";
            string filePath = Path.Combine(folderPath, fileName);

            using (XLWorkbook workbook = new XLWorkbook(templatePath)) {
                IXLWorksheet worksheet = workbook.Worksheet(1);

                int detailTemplateRow = 19;
                int currentRow = detailTemplateRow;

                for (int i = 0; i < dt.Rows.Count; i++) {
                    currentRow = detailTemplateRow + i;
                    DataRow dataRow = dt.Rows[i];

                    if (i > 0) {
                        var rowToCopy = worksheet.Row(detailTemplateRow);
                        worksheet.Row(currentRow).InsertRowsAbove(1);
                        rowToCopy.CopyTo(worksheet.Row(currentRow));
                    }

                    string productName = dataRow["商品名"].ToString();
                    decimal price = Convert.ToDecimal(dataRow["単価"]);
                    int quantity = 100;

                    worksheet.Cell(currentRow, 2).Value = productName;
                    worksheet.Cell(currentRow, 3).Value = quantity;
                    worksheet.Cell(currentRow, 4).Value = price;
                }

                //合計行入力
                int totalRow = currentRow + 1;

                worksheet.Cell(totalRow, 5).FormulaA1 = "SUM(E" + detailTemplateRow + ":E" + currentRow + ")";
                worksheet.Cell(15, 2).FormulaA1 = ("E" + totalRow);

                workbook.SaveAs(filePath);
            }
        }

        /// <summary>
        /// 発注対象の商品一覧を取得する
        /// </summary>
        public DataTable GetLowStockProducts() {
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
    }
}