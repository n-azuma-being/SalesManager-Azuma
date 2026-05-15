using ClosedXML.Excel;
using SalesManager.Infrastructure;
using System;
using System.Data;
using System.IO;

namespace SalesManager.Services {
    /// <summary>
    /// 週次報告書作成画面の内部処理を行うためのクラス
    /// </summary>
    internal class WeeklyReportService {
        public void ValidateAggregationPeriod() { }

        /// <summary>
        /// 週次報告書作成
        /// </summary>
        public void RequestWeeklyReport(string templatePath, string folderPath, DateTime start, DateTime end) {

            DataTable dt = GetWeeklyDate(start, end);

            if (dt.Rows.Count == 0) {
                throw new Exception("選択された期間に売上データが見つかりませんでした。");
            }

            string fileName = "週次報告書_" + DateTime.Now.ToString("yyyyMMdd", System.Globalization.CultureInfo.InvariantCulture) + ".xlsx";
            string filePath = Path.Combine(folderPath, fileName);

            if (File.Exists(filePath)) {
                var ans = MessageManager.ShowQuestion("ファイルを上書きしますか？");
                if (!ans) return;
            }

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

                var totalRange = worksheet.Range(totalRow, 6, totalRow, 7);
                totalRange.Merge();

                worksheet.Cell(totalRow, 6).FormulaA1 = "SUM(G" + detailTemplateRow + ":G" + currentRow + ")";

                workbook.SaveAs(filePath);
                MessageManager.ShowInfo("週次報告書の作成が完了しました。");
            }
        }

        /// <summary>
        /// 発注対象の商品一覧を取得する
        /// </summary>
        public DataTable GetWeeklyDate(DateTime startDate, DateTime endDate) {
            try {
                return Repository.GetWeeklyData(startDate, endDate);
            } catch (Exception ex) {
                throw new Exception($"週次データの取得に失敗しました。{Environment.NewLine}{ex.Message}");
            }
        }

        public void ExportFile(string folderPath, string templatePath, DateTime start, DateTime end) {
            try {

                RequestWeeklyReport(templatePath, folderPath, start, end);

            } catch (Exception ex) {
                MessageManager.ShowError($"エラーが発生したため処理を終了しました。{Environment.NewLine}{ex.Message}");
            }
        }

        /// <summary>
        /// 月曜日と日曜日を計算する
        /// </summary>
        public (DateTime Start, DateTime End) GetWeeklyRange(DateTime selectedDate) {
            int diff = (7 + (selectedDate.DayOfWeek - DayOfWeek.Monday)) % 7;
            DateTime monday = selectedDate.AddDays(-1 * diff).Date;
            DateTime sunday = monday.AddDays(6).Date;

            return (monday, sunday);
        }
    }
}
