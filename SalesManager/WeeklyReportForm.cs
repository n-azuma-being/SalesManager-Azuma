using SalesManager.Infrastructure;
using SalesManager.Services;
using System;
using System.Windows.Forms;
using System.IO;

namespace SalesManager {
    public partial class WeeklyReportForm : Form {

        private readonly WeeklyReportService _weeklyReportService = new WeeklyReportService();

        public WeeklyReportForm() {
            InitializeComponent();
        }

        private void ExportFile(string folderPath, DateTime start, DateTime end) {
            try {
                string templatePath = Path.Combine(Application.StartupPath, "Templates", "WeeklyReportTemplate.xlsx");

                _weeklyReportService.RequestWeeklyReport(templatePath, folderPath, start, end);

                MessageBox.Show("週次報告書の作成が完了しました。", "完了", MessageBoxButtons.OK, MessageBoxIcon.Information);
            } catch (Exception ex) {
                MessageBox.Show("エラーが発生したため処理を終了しました。: " + ex.Message, "エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private bool _isUpdating = false;

        private void ApplyWeeklyRange(DateTime selectedDate) {
            if (_isUpdating) return; //更新中なら何もしない
            _isUpdating = true;

            try {
                //月曜日を計算
                int diff = (7 + (selectedDate.DayOfWeek - DayOfWeek.Monday)) % 7;
                DateTime monday = selectedDate.AddDays(-1 * diff).Date;
                DateTime sunday = monday.AddDays(6).Date;

                dtpSummaryStartDate.Value = monday;
                dtpSummaryEndDate.Value = sunday;
            } finally {
                _isUpdating = false;
            }
        }

        // ---- イベント ----

        private void btnCreateReport_Click(object sender, EventArgs e) {
            try {
                DateTime startDate = dtpSummaryStartDate.Value;
                DateTime endDate = dtpSummaryEndDate.Value;

                //期間逆転チェック
                if (startDate > endDate) {
                    MessageBox.Show("開始日は終了日より前の日付を選択してください。");
                    return;
                }

                string selectedPath = FileManager.ShowFolderDialog(this, "週次報告書の保存先を選択してください");
                if (string.IsNullOrEmpty(selectedPath)) return;

                string templatePath = Path.Combine(Application.StartupPath, "Templates", "WeeklyReportTemplate.xlsx");

                _weeklyReportService.RequestWeeklyReport(templatePath, selectedPath, startDate, endDate);

                MessageBox.Show("週次報告書を作成しました。");

            } catch (Exception ex) {
                MessageBox.Show($"エラーが発生したため処理を終了しました :\r\n {ex.Message}");
            }
        }

        private void btnExportWeeklyReport_Click(object sender, EventArgs e) {
            string selectedPath = FileManager.ShowFolderDialog(this, "週次報告書の出力先フォルダを選択してください");

            //キャンセルされなかった場合のみ続行
            if (selectedPath != null) {
                DateTime start = dtpSummaryStartDate.Value;
                DateTime end = dtpSummaryEndDate.Value;

                ExportFile(selectedPath, start, end);
            }
        }

        //集計期間開始日
        private void dtpSummaryStartDate_ValueChanged(object sender, EventArgs e) {
            ApplyWeeklyRange(dtpSummaryStartDate.Value);
        }

        //集計期間終了日
        private void dtpSummaryEndDate_ValueChanged(object sender, EventArgs e) {
            ApplyWeeklyRange(dtpSummaryEndDate.Value);
        }
    }
}
