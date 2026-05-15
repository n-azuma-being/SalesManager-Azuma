using SalesManager.Infrastructure;
using SalesManager.Services;
using SalesManager.Views;
using System;
using System.Windows.Forms;

namespace SalesManager {
    /// <summary>
    /// メイン画面を表示するためのクラス
    /// </summary>
    public partial class MainForm : System.Windows.Forms.Form {

        private readonly ImportService _importService = new ImportService();


        // 各画面のインスタンスを保持
        private ImportView _importView;

        public MainForm() {
            InitializeComponent();
            this.Load += MainForm_Load;
        }

        private void MainForm_Load(object sender, EventArgs e) {
            try {
                _importService.InitializeDatabase();

                InitializeViews();

                this.KeyPreview = true;

                this.KeyDown += MainForm_KeyDown;

                ShowImportView();

            } catch (Exception ex) {
                MessageManager.ShowError($"起動エラー{Environment.NewLine} {ex.Message}");
            }
        }

        private void MainForm_KeyDown(object sender, KeyEventArgs e) {
            if (e.Alt && e.KeyCode == Keys.I) {
                try {
                    ShowImportView();
                } catch (Exception ex) {
                    MessageManager.ShowError($"エラーが発生したため処理を終了しました。{Environment.NewLine}{ex.Message}");
                }
            } else if (e.Alt && e.KeyCode == Keys.S) {
                try {
                    ShowSalesView();
                } catch (Exception ex) {
                    MessageManager.ShowError($"エラーが発生したため処理を終了しました。{Environment.NewLine}{ex.Message}");
                }
            } else if (e.Alt && e.KeyCode == Keys.P) {
                try {
                    ShowProductView();
                } catch (Exception ex) {
                    MessageManager.ShowError($"エラーが発生したため処理を終了しました。{Environment.NewLine}{ex.Message}");
                }
            }
        }

        private void InitializeViews() {
            // --- 取込画面 ---
            leftNav.SelectedElement = btnImport;
            this.importView.Dock = DockStyle.Fill;
            this.salesView.Dock = DockStyle.Fill;
            this.productView.Dock = DockStyle.Fill;
        }

        // --- 画面切り替えメソッド群 ---

        public void ShowImportView() {
            mainFrame.SelectedPage = pageImport;
            this.Text = "販売管理システム - データ取込";
            this.BeginInvoke(new MethodInvoker(() => {
                importView.FocusDefault();
            }));
        }

        public void ShowSalesView() {
            mainFrame.SelectedPage = pageSales;
            this.Text = "販売管理システム - 売上集計";
            this.BeginInvoke(new MethodInvoker(() => {
                salesView.FocusDefault();
            }));
        }

        public void ShowProductView() {
            mainFrame.SelectedPage = pageProduct;
            this.Text = "販売管理システム - 商品管理";
            productView.FocusDefault();
        }

        // --- メニュー（AccordionControl）からのイベント ---

        //データ取込画面表示ボタン
        private void btnImport_Click(object sender, EventArgs e) {
            try {
                ShowImportView();
            } catch (Exception ex) {
                MessageManager.ShowError($"エラーが発生したため処理を終了しました。{Environment.NewLine}{ex.Message}");
            }
        }
        //売上集計画面表示ボタン
        private void btnSalesSummary_Click(object sender, EventArgs e) {
            try {
                ShowSalesView();
            } catch (Exception ex) {
                MessageManager.ShowError($"エラーが発生したため処理を終了しました。{Environment.NewLine}{ex.Message}");
            }
        }

        //商品管理画面表示ボタン
        private void btnProduct_Click(object sender, EventArgs e) {
            try {
                ShowProductView();
            } catch (Exception ex) {
                MessageManager.ShowError($"エラーが発生したため処理を終了しました。。{Environment.NewLine}{ex.Message}");
            }
        }
    }
}