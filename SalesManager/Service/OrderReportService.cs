using ClosedXML.Excel;
using SalesManager.Infrastructure;
using System;
using System.Data;
using System.IO;

namespace SalesManager.Service {
    /// <summary>
    /// 発注書を作成するためのクラス
    /// </summary>
    internal class OrderReportService {

        //発注しきい値
        public const int LowStockThreshold = 5;

        public void CreateOrderReport(string templatePath, string folderPath) {
            DataTable dt = GetLowStockProducts();
            if (dt.Rows.Count == 0) {
                throw new Exception("発注対象の商品がありません。");
            }

            string fileName = "発注書_" + DateTime.Now.ToString("yyyyMMdd", System.Globalization.CultureInfo.InvariantCulture) + ".xlsx";
            string filePath = Path.Combine(folderPath, fileName);

            if (File.Exists(filePath)) {
                bool ans = MessageManager.ShowQuestion("ファイルを上書きしますか？");

                if (!ans) return;
            }

            using (XLWorkbook workbook = new XLWorkbook(templatePath)) {
                IXLWorksheet worksheet = workbook.Worksheet(1);

                worksheet.Cell("E5").Value = DateTime.Now.ToString("yyyy/MM/dd", System.Globalization.CultureInfo.InvariantCulture);

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
                MessageManager.ShowInfo("発注書の作成が完了しました。");
            }
        }

        /// <summary>
        /// 発注対象の商品一覧を取得する
        /// </summary>
        public DataTable GetLowStockProducts() {
            return Repository.GetLowStockItems(LowStockThreshold);
        }
    }
}
