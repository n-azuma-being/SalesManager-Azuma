using SalesManager.Infrastructure;
using SalesManager.Services;
using System;
using System.Windows.Forms;

namespace SalesManager.Views {
    /// <summary>
    /// 売上集計画面を表示するためのクラス
    /// </summary>
    public partial class SalesView : UserControl {

        private readonly SalesService _salesService = new SalesService();
        private bool _isUpdatingPeriod = false;

        public void FocusDefault() {
            this.ActiveControl = cmbAggregationUnit;
            cmbAggregationUnit.Focus();//初期フォーカス位置
        }

        public SalesView() {
            InitializeComponent();

            dtpSalesStartDate.Format = DateTimePickerFormat.Custom;
            dtpSalesStartDate.CustomFormat = "yyyy/MM/dd";

            dtpSalesEndDate.Format = DateTimePickerFormat.Custom;
            dtpSalesEndDate.CustomFormat = "yyyy/MM/dd";

            this.TabStop = false;
            this.SetStyle(ControlStyles.Selectable, false);
        }

        private void SalesView_Load(object sender, EventArgs e) {
            DateTime latestDate = _salesService.GetSalesDateRange().MaxDate; //DBから最新の日付を取得
            dtpSalesStartDate.Value = latestDate.AddYears(-1);
            dtpSalesEndDate.Value = latestDate; //売上データ終了日を設定

            InitializeComboBox();
            DisplaySalesList();
        }

        /// <summary>
        /// 売上データ表示
        /// </summary>
        public void DisplaySalesList() {
            //コンボボックスの状態に関わらず、カレンダーの値を正として取得
            string start = dtpSalesStartDate.Value.ToString("yyyy-MM-dd");
            string end = dtpSalesEndDate.Value.ToString("yyyy-MM-dd");

            var salesList = _salesService.GetSales(start, end);

            salesGridControl.DataSource = salesList;

            btnCreateWeeklyReport.Enabled = salesList.Count > 0;
        }

        private void InitializeComboBox() {
            cmbAggregationUnit.DropDownStyle = ComboBoxStyle.DropDownList;

            var units = new[] {
                new { Text = "自由選択", Value = "Free" },
                new { Text = "日次", Value = "Day" },
                new { Text = "週次", Value = "Week" },
                new { Text = "月次", Value = "Month" },
                new { Text = "年次", Value = "Year" }
            };

            cmbAggregationUnit.DisplayMember = "Text";
            cmbAggregationUnit.ValueMember = "Value";
            cmbAggregationUnit.DataSource = units;
        }

        /// <summary>
        /// 集計単位に応じて期間選択肢やカレンダーの活性状態を更新する
        /// </summary>
        private void UpdatePeriodList() {
            string unit = cmbAggregationUnit.SelectedValue?.ToString();

            cmbAggregationPeriod.Items.Clear();

            // UIの状態制御
            bool isFree = (unit == "Free");
            bool isDay = (unit == "Day");
            bool isWeek = (unit == "Week");

            cmbAggregationPeriod.Enabled = (unit == "Month" || unit == "Year");
            dtpSalesStartDate.Enabled = (isFree || isDay || isWeek);
            dtpSalesEndDate.Enabled = isFree;

            if (isFree) {
                var range = _salesService.GetSalesDateRange();
                dtpSalesStartDate.MinDate = range.MinDate;
                dtpSalesStartDate.MaxDate = range.MaxDate;
                dtpSalesEndDate.MinDate = range.MinDate;
                dtpSalesEndDate.MaxDate = range.MaxDate;
                CmbAggregationPeriodList_Clear();
            } else {
                dtpSalesStartDate.MinDate = new DateTime(2000, 1, 1);
                dtpSalesStartDate.MaxDate = new DateTime(2099, 12, 31);
                dtpSalesEndDate.MinDate = new DateTime(2000, 1, 1);
                dtpSalesEndDate.MaxDate = new DateTime(2099, 12, 31);
            }

            // 日次の場合は、開始日を終了日に合わせる
            if (isDay) {
                dtpSalesStartDate.Value = dtpSalesEndDate.Value;
                CmbAggregationPeriodList_Clear();
                return;
            }

            // 週次の場合はカレンダーに連動して計算を実行
            if (isWeek) {
                SyncPeriodRange(unit, "");
                CmbAggregationPeriodList_Clear();
                return;
            }

            // 年次・月次のリストをServiceから取得してセット
            if (cmbAggregationPeriod.Enabled) {
                var periods = _salesService.GetAggregationPeriods(unit);
                cmbAggregationPeriod.Items.AddRange(periods.ToArray());
                if (cmbAggregationPeriod.Items.Count > 0) {
                    cmbAggregationPeriod.SelectedIndex = 0;
                }
            }
        }

        public void CmbAggregationPeriodList_Clear() {
            cmbAggregationPeriod.Items.Clear();
            cmbAggregationPeriod.Text = "";
            cmbAggregationPeriod.SelectedIndex = -1;
        }

        /// <summary>
        /// 期間を計算してカレンダーを更新
        /// </summary>
        private void SyncPeriodRange(string unit, string selectedText) {
            try {

                _isUpdatingPeriod = true;
                var range = _salesService.GetPeriodRange(unit, selectedText, dtpSalesEndDate.Value);

                dtpSalesStartDate.Value = range.Start;
                dtpSalesEndDate.Value = range.End;
            } finally {
                _isUpdatingPeriod = false;
            }
        }

        private void OpenWeeklyReportView() {
            DateTime currentEndDate = dtpSalesEndDate.Value;
            using (WeeklyReportForm form = new WeeklyReportForm(currentEndDate)) {
                form.ShowDialog();
            }
        }

        //tabキー ループ処理
        protected override bool ProcessDialogKey(Keys keyData) {
            if (FocusManager.HandleTabLoop(this.ActiveControl, cmbAggregationUnit, btnCreateWeeklyReport, keyData)) {
                return true;
            }
            return base.ProcessDialogKey(keyData);
        }

        // --- イベントハンドラ ---

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

            SyncPeriodRange(unit, selectedText);
        }

        //開始日選択
        private void dtpSalesStartDate_ValueChanged(object sender, EventArgs e) {
            string unit = cmbAggregationUnit.SelectedValue?.ToString();
            DateTime selectedDate = dtpSalesStartDate.Value;

            if (unit == "Day" || unit == "Week") {
                var range = _salesService.GetPeriodRange(unit, "", dtpSalesStartDate.Value);

                // 終了日を自動更新
                dtpSalesEndDate.Value = range.End;

                if (unit == "Week" && selectedDate.Date != range.Start.Date) {
                    MessageManager.ShowInfo("集計期間を月～日に修正しました");
                }
            } else {
                //逆転防止
                if (dtpSalesStartDate.Value > dtpSalesEndDate.Value) {
                    dtpSalesEndDate.Value = dtpSalesStartDate.Value;
                }
            }
            DisplaySalesList();
        }

        //集計単位が週次のとき、Downキーで集計期間を次の週にする
        private void dtpSalesStartDate_KeyDown(object sender, KeyEventArgs e) {
            if (e.KeyCode == Keys.Down) {

                dtpSalesStartDate.Value = dtpSalesStartDate.Value.AddDays(8);

                e.SuppressKeyPress = true;
            }
        }

        //終了日選択
        private void dtpSalesEndDate_ValueChanged(object sender, EventArgs e) {
            string unit = cmbAggregationUnit.SelectedValue?.ToString();

            if (unit == "Day" || unit == "Week") {
                var range = _salesService.GetPeriodRange(unit, "", dtpSalesEndDate.Value);

                // 開始日を自動更新（日次なら開始日と同じ、週次なら日曜になる）
                dtpSalesStartDate.Value = range.Start;
            }
            DisplaySalesList();
        }

        //週次報告書作成ボタン
        private void btnCreateWeeklyReport_Click(object sender, EventArgs e) {
            OpenWeeklyReportView();
        }
    }
}
