using ClosedXML.Excel;
using SalesManager.Infrastructure;
using System;
using System.Data;
using System.Data.SQLite;
using System.IO;

namespace SalesManager.Services {
    internal class WeeklyReportService {
        public void ValidateAggregationPeriod(){ }

        /// <summary>
        /// 週次報告書作成
        /// </summary>
        public void RequestWeeklyReport(string templatePath, string folderPath, DateTime start, DateTime end) {

            DataTable dt = GetWeeklyDate(start, end);

            if (dt.Rows.Count == 0) {
                throw new Exception("選択された期間に売上データが見つかりませんでした。");
            }

            string fileName = "週次報告書_" + DateTime.Now.ToString("yyyyMMdd") + ".xlsx";
            string filePath = Path.Combine(folderPath, fileName);

            using (XLWorkbook workbook = new XLWorkbook(templatePath)) {
                IXLWorksheet worksheet = workbook.Worksheet(1);

                int detailTemplateRow = 2;
                int currentRow = detailTemplateRow;

                for (int i = 0; i < dt.Rows.Count; i++) {
                    currentRow = detailTemplateRow + i;
                    DataRow dataRow = dt.Rows[i];

                    if (i > 0) {
                        var rowToCopy = worksheet.Row(detailTemplateRow);
                        worksheet.Row(currentRow).InsertRowsAbove(1);
                        rowToCopy.CopyTo(worksheet.Row(currentRow));
                    }

                    int productID = Convert.ToInt32(dataRow["商品ID"]);
                    string productName = dataRow["商品名"].ToString();
                    int lastWeekSalesQuantity = Convert.ToInt32(dataRow["先週販売数"]);
                    int currentStockQuantity = Convert.ToInt32(dataRow["現在在庫数"]);
                    int stockAfterSales = Convert.ToInt32(dataRow["販売後在庫"]);
                    string isReorderRequired = dataRow["発注対象"].ToString();
                    decimal totalSalesAmount = Convert.ToDecimal(dataRow["累計売上金額"]);

                    worksheet.Cell(currentRow, 1).Value = productID;
                    worksheet.Cell(currentRow, 2).Value = productName;
                    worksheet.Cell(currentRow, 3).Value = lastWeekSalesQuantity;
                    worksheet.Cell(currentRow, 4).Value = currentStockQuantity;
                    worksheet.Cell(currentRow, 5).Value = stockAfterSales;
                    worksheet.Cell(currentRow, 6).Value = isReorderRequired;
                    worksheet.Cell(currentRow, 7).Value = totalSalesAmount;
                }

                //合計行入力
                int totalRow = currentRow + 2;

                worksheet.Cell(totalRow, 7).FormulaA1 = "SUM(G" + detailTemplateRow + ":G" + currentRow + ")";

                workbook.SaveAs(filePath);
            }
        }

        /// <summary>
        /// 発注対象の商品一覧を取得する
        /// </summary>
        public DataTable GetWeeklyDate(DateTime startDate, DateTime endDate) {
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

            try{
                return DbManager.ExecuteQuery(sql, parameters);
            } catch (Exception ex) {
                throw new Exception($"週次データの取得に失敗しました。:\r\n {ex.Message}");
            }
        }
    }
}
