using SalesManager.Infrastructure;
using SalesManager.Services;
using System;
using System.Windows.Forms;
using System.IO;

namespace SalesManager {
    /// <summary>
    /// 週次報告書作成画面を表示するためのクラス
    /// </summary>
    public partial class WeeklyReportForm : Form {

        private readonly WeeklyReportService _weeklyReportService = new WeeklyReportService();
        private DateTime _initialEndDate;

        public WeeklyReportForm(DateTime endDate) {
            InitializeComponent();

            dtpSummaryStartDate.Format = DateTimePickerFormat.Custom;
            dtpSummaryStartDate.CustomFormat = "yyyy/MM/dd";

            dtpSummaryEndDate.Format = DateTimePickerFormat.Custom;
            dtpSummaryEndDate.CustomFormat = "yyyy/MM/dd";

            _initialEndDate = endDate;

            this.KeyPreview = true;
            this.KeyDown += WeeklyReportForm_KeyDown;
        }

        private void WeeklyReportForm_Load(object sender, EventArgs e) {
            dtpSummaryEndDate.Value = _initialEndDate;
            ApplyWeeklyRange(_initialEndDate);
        }

        private bool _isUpdating = false;

        private void ApplyWeeklyRange(DateTime selectedDate) {
            if (_isUpdating) return;
            _isUpdating = true;

            try {
                var range = _weeklyReportService.GetWeeklyRange(selectedDate);

                dtpSummaryStartDate.Value = range.Start;
                dtpSummaryEndDate.Value = range.End;
            } finally {
                _isUpdating = false;
            }
        }

        // ---- イベント ----

        //週次報告書作成ボタン
        private void btnExportWeeklyReport_Click(object sender, EventArgs e) {
            string selectedPath = FileManager.ShowFolderDialog("週次報告書の出力先フォルダを選択してください", "フォルダ");

            //キャンセルされなかった場合のみ続行
            if (selectedPath != null) {
                string templatePath = Path.Combine(Application.StartupPath, "Templates", "WeeklyReportTemplate.xlsx");

                DateTime start = dtpSummaryStartDate.Value;
                DateTime end = dtpSummaryEndDate.Value;

                _weeklyReportService.ExportFile(selectedPath, templatePath, start, end);
            }
        }

        //集計期間開始日
        private void dtpSummaryStartDate_ValueChanged(object sender, EventArgs e) {
            if (_isUpdating) return;

            DateTime inputDate = dtpSummaryStartDate.Value;

            ApplyWeeklyRange(inputDate);

            if (inputDate.Date != dtpSummaryStartDate.Value.Date) {
                MessageManager.ShowInfo("集計期間を月～日に修正しました");
            }
        }

        //集計単位が週次のとき、Downキーで集計期間を次の週にする
        private void dtpSummaryStartDate_KeyDown(object sender, KeyEventArgs e) {
            dtpSummaryStartDate.Value = dtpSummaryStartDate.Value.AddDays(8);
            e.SuppressKeyPress = true;
        }

        //集計期間終了日
        private void dtpSummaryEndDate_ValueChanged(object sender, EventArgs e) {
            if (_isUpdating) return;

            ApplyWeeklyRange(dtpSummaryEndDate.Value);
        }

        //escキーで画面を閉じる
        private void WeeklyReportForm_KeyDown(object sender, KeyEventArgs e) {
            if (e.KeyCode == Keys.Escape) {
                this.Close();
            }
        }
    }
}
