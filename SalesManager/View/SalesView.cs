using SalesManager.Services;
using System;
using System.Windows.Forms;

namespace SalesManager.View {
    public partial class SalesView : UserControl {

        private readonly SalesService _salesService = new SalesService();

        public SalesView() {
            InitializeComponent();
        }

        private void SalesView_Load(object sender, EventArgs e) {
            DateTime latestDate = _salesService.GetLatestSalesDate(); //DBから最新の日付を取得
            dtpSalesEndDate.Value = latestDate; //売上データ終了日を設定
            dtpSalesStartDate.Value = latestDate.AddYears(-1);

            InitializeComboBox();
            DisplaySalesList();
        }

        ///<summary>
        ///売上データを表示
        ///</summary>
        public void DisplaySalesList() {
            //コンボボックスの状態に関わらず、カレンダーの値を正として取得
            string start = dtpSalesStartDate.Value.ToString("yyyy-MM-dd");
            string end = dtpSalesEndDate.Value.ToString("yyyy-MM-dd");

            salesGridControl.DataSource = _salesService.GetSales(start, end);
        }

        private void OpenWeeklyReportView() {
            WeeklyReportForm form = new WeeklyReportForm();
            form.ShowDialog();
        }

        private void InitializeComboBox() {
            var units = new[] {
                new { Text = "未選択", Value = "" },
                new { Text = "日次", Value = "Day" },
                new { Text = "週次", Value = "Week" },
                new { Text = "月次", Value = "Month" },
                new { Text = "年次", Value = "Year" }
            };
            cmbAggregationUnit.DisplayMember = "Text";
            cmbAggregationUnit.ValueMember = "Value";
            cmbAggregationUnit.DataSource = units;
        }

        private void UpdatePeriodList() {
            string unit = cmbAggregationUnit.SelectedValue?.ToString();

            cmbAggregationPeriod.Items.Clear();

            // 未選択または日次の場合はカレンダーを有効にして処理抜ける
            if (string.IsNullOrEmpty(unit) || unit == "Day") {
                cmbAggregationPeriod.Enabled = false;
                dtpSalesStartDate.Enabled = true;
                dtpSalesEndDate.Enabled = true;
                return;
            }

            if (unit == "Week") {
                cmbAggregationPeriod.Enabled = false;
                dtpSalesStartDate.Enabled = true;
                dtpSalesEndDate.Enabled = false;

                ApplyWeeklyRange(dtpSalesStartDate.Value);
                return;
            }

            cmbAggregationPeriod.Enabled = true;
            dtpSalesStartDate.Enabled = false;
            dtpSalesEndDate.Enabled = false;

            DateTime now = DateTime.Now;

            if (unit == "Year") {
                for (int i = 0; i < 5; i++) {
                    cmbAggregationPeriod.Items.Add($"{now.Year - i}年");
                }
            } 
            else if (unit == "Month") {
                for (int i = 0; i < 12; i++) {
                    DateTime target = now.AddMonths(-i);
                    cmbAggregationPeriod.Items.Add(target.ToString("yyyy年MM月"));
                }
            }

            if (cmbAggregationPeriod.Items.Count > 0) {
                cmbAggregationPeriod.SelectedIndex = 0;
            }
        }

        //選択した日を含む月～日
        private void ApplyWeeklyRange(DateTime selectedDate) {
            // 月曜日を計算
            int diff = (7 + (selectedDate.DayOfWeek - DayOfWeek.Monday)) % 7;
            DateTime monday = selectedDate.AddDays(-1 * diff).Date;
            DateTime sunday = monday.AddDays(6).Date;

            dtpSalesStartDate.Value = monday;
            dtpSalesEndDate.Value = sunday;
        }

        // --- イベント ---

        //週次報告書作成ボタン
        private void btnCreateWeeklyReport_Click(object sender, EventArgs e) {
            OpenWeeklyReportView();
        }

        //集計単位選択
        private void cmbAggregationUnit_SelectedIndexChanged(object sender, EventArgs e) {
            UpdatePeriodList();
            DisplaySalesList();
        }

        //集計対象期間選択
        private void cmbAggregationPeriod_SelectedIndexChanged(object sender, EventArgs e) {
            string unit = cmbAggregationUnit.SelectedValue?.ToString();
            string selectedText = cmbAggregationPeriod.SelectedItem?.ToString();

            if (string.IsNullOrEmpty(selectedText)) return;

            DateTime start = DateTime.Today;
            DateTime end = DateTime.Today;

            if (unit == "Year") {
                int year = int.Parse(selectedText.Replace("年", ""));
                start = new DateTime(year, 4, 1);
                end = new DateTime(year + 1, 3, 31);
            } else if (unit == "Month") {
                DateTime parsedDate = DateTime.ParseExact(selectedText, "yyyy年MM月", null);
                start = new DateTime(parsedDate.Year, parsedDate.Month, 1);
                end = start.AddMonths(1).AddDays(-1); // 月末日
            }

            // カレンダーの値を更新
            dtpSalesStartDate.Value = start;
            dtpSalesEndDate.Value = end;
        }

        //売上データ開始日
        private void dtpSalesStartDate_ValueChanged(object sender, EventArgs e) {
            string unit = cmbAggregationUnit.SelectedValue?.ToString();

            if (unit == "Week") {
                ApplyWeeklyRange(dtpSalesStartDate.Value);
            }
            DisplaySalesList();
        }

        //売上データ終了日
        private void dtpSalesEndDate_ValueChanged(object sender, EventArgs e) {
            DisplaySalesList();
        }

        //
        //private void btnSearch_Click(object sender, EventArgs e) {
        //    DisplaySalesList();
        //}

        private void btnReport_Click(object sender, EventArgs e) {
            OpenWeeklyReportView();
        }

    }
}
