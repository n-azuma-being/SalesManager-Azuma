using SalesManager.Infrastructure;
using SalesManager.Model;
using System;
using System.Collections.Generic;
using System.Data;

namespace SalesManager.Services {
    /// <summary>
    /// 売上集計画面の内部処理を行うクラス
    /// </summary>
    public class SalesService {

        /// <summary>
        /// 売上データを取得
        /// </summary>
        public List<SaleRecord> GetSales(string start, string end) {
            var list = new List<SaleRecord>();
            DataTable dt = Repository.GetSalesSummary(start, end);

            foreach (DataRow row in dt.Rows) {
                list.Add(new SaleRecord {
                    SaleDate = Convert.ToDateTime(row["売上日"]),
                    StoreName = row["店舗名"].ToString(),
                    ProductName = row["商品名"].ToString(),
                    UnitPrice = Convert.ToInt32(row["単価"]),
                    Quantity = Convert.ToInt32(row["販売数"]),
                });
            }
            return list;
        }

        /// <summary>
        /// 売上データの中で一番新しい日付を返す
        /// </summary>
        public (DateTime MinDate, DateTime MaxDate) GetSalesDateRange() {
            // SQLは直接書かず、Repositoryに問い合わせる
            DateTime minDate = Repository.GetMinSalesDate();
            DateTime maxDate = Repository.GetMaxSalesDate();

            return (minDate, maxDate);
        }

        /// <summary>
        /// 集計単位に応じて選択肢を生成
        /// </summary>
        public List<string> GetAggregationPeriods(string unit) {
            var periods = new List<string>();
            var range = GetSalesDateRange();
            DateTime minDate = range.MinDate;
            DateTime maxDate = range.MaxDate;

            // データが0件の場合は"今日"が含まれる年度もしくは月を作成する
            if (minDate == DateTime.Today && maxDate == DateTime.Today && Repository.GetMaxSalesDate() == DateTime.Today) {
                if (unit == "Year") periods.Add($"{maxDate.Year}年");
                else if (unit == "Month") periods.Add(maxDate.ToString("yyyy年MM月"));
                return periods;
            }
            if (unit == "Year") {
                int startFiscalYear = (maxDate.Month >= 4) ? maxDate.Year : maxDate.Year - 1;
                int endFiscalYear = (minDate.Month >= 4) ? minDate.Year : minDate.Year - 1;

                for (int y = startFiscalYear; y >= endFiscalYear; y--) {
                    periods.Add($"{y}年");
                }
            } else if (unit == "Month") {
                DateTime current = new DateTime(maxDate.Year, maxDate.Month, 1);
                DateTime limit = new DateTime(minDate.Year, minDate.Month, 1);

                while (current >= limit) {
                    periods.Add(current.ToString("yyyy年MM月"));
                    current = current.AddMonths(-1);
                }
            }
            return periods;
        }

        /// <summary>
        /// 期間を計算
        /// </summary>
        public (DateTime Start, DateTime End) GetPeriodRange(string unit, string selectedText, DateTime baseDate) {
            DateTime start = baseDate;
            DateTime end = baseDate;

            if (unit == "Year" && !string.IsNullOrEmpty(selectedText)) {
                int year = int.Parse(selectedText.Replace("年", ""));
                start = new DateTime(year, 4, 1);
                end = new DateTime(year + 1, 3, 31);
            } else if (unit == "Month" && !string.IsNullOrEmpty(selectedText)) {
                DateTime parsedDate = DateTime.ParseExact(selectedText, "yyyy年MM月", null);
                start = new DateTime(parsedDate.Year, parsedDate.Month, 1);
                end = start.AddMonths(1).AddDays(-1);
            } else if (unit == "Week") {
                int diff = (7 + (DayOfWeek.Sunday - baseDate.DayOfWeek)) % 7;

                end = baseDate.AddDays(diff).Date;
                start = end.AddDays(-6).Date;
            } else if (unit == "Day") {
                start = end = baseDate.Date;
            }

            return (start, end);
        }
    }
}