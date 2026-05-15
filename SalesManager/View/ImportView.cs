using SalesManager.Infrastructure;
using SalesManager.Services;
using System;
using System.IO;
using System.Windows.Forms;

namespace SalesManager.Views {
    /// <summary>
    /// データ取込画面を表示するためのクラス
    /// </summary>
    public partial class ImportView : System.Windows.Forms.UserControl {

        private readonly ImportService _importService = new ImportService();

        public void FocusDefault() {
            btnBrowse.Focus();//初期フォーカス位置
        }

        public ImportView() {
            InitializeComponent();
            txtFilePath.KeyPress += (s, e) => e.Handled = true;
            this.TabStop = false;
            panelControl1.TabStop = false;
            btnExecuteImport.Enabled = false;
        }

       private void SelectFile() {
            string[] files = FileManager.ShowOpenFilesDialog(
                "取込対象のCSVファイルをすべて選択してください",
                "CSVファイル (*.csv)|*.csv"
            );

            //キャンセル
            if (files == null || files.Length == 0) return;

            if (files.Length > 3) {
                MessageManager.ShowInfo("選択できるファイルは3件までです。");
                return;
            }

            //一旦クリアにする
            lblFileName_Hide();

            txtFilePath.Text = string.Join(";", files);

            btnExecuteImport.Enabled = true;
            btnExecuteImport.TabStop = true;

            lblFileName_Visible(files);
        }

        public void lblFileName_Visible(string[] files){
            for (int i = 0; i < files.Length; i++) {

                Control[] controls = this.Controls.Find("lblFileName" + (i + 1), true);

                if (controls.Length > 0) {
                    controls[0].Visible = true;
                    controls[0].Text = Path.GetFileName(files[i]);// ファイル名のみ表示
                }
            }
        }

        public void lblFileName_Hide() {
            for (int i = 0; i < 3; i++) {

                Control[] controls = this.Controls.Find("lblFileName" + (i + 1), true);

                if (controls.Length > 0) {
                    controls[0].Text = "";
                    controls[0].Visible = false;
                }
            }
        }

        //tabキー ループ処理
        protected override bool ProcessDialogKey(Keys keyData) {
            if (FocusManager.HandleTabLoop(this.ActiveControl, btnBrowse, txtFilePath, keyData)) {
                return true;
            }
            return base.ProcessDialogKey(keyData);
        }

        // --- イベント ---

        //参照ボタン
        private void btnBrowse_Click(object sender, EventArgs e) {
            SelectFile();
        }

        //参照ファイルパス
        private void txtFilePath_TextChanged(object sender, EventArgs e) {
            btnExecuteImport.Enabled = !string.IsNullOrWhiteSpace(txtFilePath.Text);
        }

        //テキストボックス（Enterキー）
        private void txtFilePath_KeyDown(object sender, KeyEventArgs e) {
            if (e.KeyCode == Keys.Enter) {
                btnBrowse_Click(sender, EventArgs.Empty);

                // Enter音無効化
                e.SuppressKeyPress = true;
            }
        }

        //集計開始ボタン
        private void btnExecuteImport_Click(object sender, EventArgs e) {
            string[] paths = txtFilePath.Text.Split(';');

            try {
                this.Cursor = Cursors.WaitCursor;
                btnExecuteImport.Enabled = false;

                _importService.ExecuteImport(paths);

                txtFilePath.Clear();
                lblFileName_Hide();
            } catch (Exception ex) {
                MessageManager.ShowError($"データベース登録中にエラーが発生しました。{Environment.NewLine}{ex.Message}");
            } finally {
                this.Cursor = Cursors.Default;
                btnExecuteImport.Enabled = true;
                btnExecuteImport.TabStop = true;
            }
        }
    }
}
