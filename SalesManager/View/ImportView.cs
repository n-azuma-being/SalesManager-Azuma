using SalesManager.Infrastructure;
using SalesManager.Services;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using static DevExpress.Data.Filtering.Helpers.SubExprHelper;

namespace SalesManager.Views {
    public partial class ImportView : DevExpress.XtraEditors.XtraUserControl {

        private readonly ImportService _importService = new ImportService();

        public ImportView() {
            InitializeComponent();

            btnExecuteImport.Enabled = false;
        }

        /// <summary>
        /// ファイル出力
        /// </summary>
        public void ExecuteImport() {
            // テキストボックスからパスを分割して取得
            string[] paths = txtFilePath.Text.Split(';');


            //存在チェック
            string productPath = paths.FirstOrDefault(p => Path.GetFileName(p).ToLower().Contains("products"));
            string inventoryPath = paths.FirstOrDefault(p => Path.GetFileName(p).ToLower().Contains("inventory"));
            string salesPath = paths.FirstOrDefault(p => Path.GetFileName(p).ToLower().Contains("sales"));

            List<string> missingFiles = new List<string>();

            if (string.IsNullOrEmpty(productPath)) missingFiles.Add("・商品マスター (products.csv)");
            if (string.IsNullOrEmpty(inventoryPath)) missingFiles.Add("・在庫データ (inventory.csv)");
            if (string.IsNullOrEmpty(salesPath)) missingFiles.Add("・売上データ (sales_YYYYMMDD.csv)");

            if (missingFiles.Count > 0) {
                string message = "以下の必要なファイルが選択されていません：\n\n" + string.Join("\n", missingFiles);
                MessageManager.ShowInfo(message);
                return; // 処理を中断
            }

            try {
                this.Cursor = Cursors.WaitCursor;
                btnExecuteImport.Enabled = false;

                // DB整合性のために順番を固定して実行
                string storePath = paths.FirstOrDefault(p => Path.GetFileName(p).ToLower().Contains("store"));
                if (storePath != null) _importService.ImportStores(storePath);

                // 商品 -> 在庫 -> 売上 の順
                _importService.ImportProducts(productPath);
                _importService.ImportInventory(inventoryPath);
                _importService.ImportSales(salesPath);

                MessageManager.ShowInfo("売上集計が完了しました。\t\t\t");
                txtFilePath.Clear();
            } catch (Exception ex) {
                MessageManager.ShowError($"データベース登録中にエラーが発生しました。：\n{ex.Message}");
            } finally {
                this.Cursor = Cursors.Default;
                btnExecuteImport.Enabled = true;
            }
        }

        public void SelectFile() {
            string[] files = FileManager.ShowOpenFilesDialog(
                "取込対象のCSVファイルをすべて選択してください",
                "CSVファイル (*.csv)|*.csv"
            );

            //キャンセル
            if (files == null || files.Length == 0) return;

            txtFilePath.Text = string.Join(";", files);

            for (int i = 0; i < files.Length; i++) {

                Control[] controls = this.Controls.Find("lblFileName" + (i + 1), true);

                if (controls.Length > 0) {

                    controls[0].Visible = true;

                    // ファイル名のみ表示
                    controls[0].Text = Path.GetFileName(files[i]);
                }
            }
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

        //集計開始ボタン
        private void btnExecuteImport_Click(object sender, EventArgs e) {
            ExecuteImport();
        }
    }
}
