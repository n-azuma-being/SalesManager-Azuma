using DevExpress.XtraBars.Navigation;
using SalesManager.Infrastructure;
using SalesManager.Services;
using SalesManager.View;
using SalesManager.Views;
using System;
using System.Windows.Forms;

namespace SalesManager {
    public partial class MainForm : System.Windows.Forms.Form {

        private readonly ImportService _importService = new ImportService();
        private readonly SalesService _salesService = new SalesService();


        // 各画面のインスタンスを保持
        private ImportView _importView;
        private SalesView _salesView;
        private ProductView _productView;

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
                MessageManager.ShowError($"起動エラー:\r\n {ex.Message}");
            }
        }

        private void MainForm_KeyDown(object sender, KeyEventArgs e) {
            if (e.Alt && e.KeyCode == Keys.I) {
                try {
                    ShowImportView();
                } catch (Exception ex) {
                    MessageManager.ShowError($"エラーが発生したため処理を終了しました。:{ex.Message}");
                }
            } else if (e.Alt && e.KeyCode == Keys.S) {
                try {
                    ShowSalesView();
                } catch (Exception ex) {
                    MessageManager.ShowError($"エラーが発生したため処理を終了しました。:{ex.Message}");
                }
            } else if (e.Alt && e.KeyCode == Keys.P) {
                try {
                    ShowProductView();
                } catch (Exception ex) {
                    MessageManager.ShowError($"エラーが発生したため処理を終了しました。:{ex.Message}");
                }
            }
        }

        private void InitializeViews() {
            // --- 取込画面 ---
            _importView = new ImportView();
            _importView.Dock = DockStyle.Fill;
            mainFrame.Controls.Add(_importView);
            leftNav.SelectedElement = btnImport;
        }

        // --- 画面切り替えメソッド群 ---

        public void ShowImportView() {
            mainFrame.SelectedPage = pageImport;
            this.Text = "販売管理システム - データ取込";
        }

        public void ShowSalesView() {
            mainFrame.SelectedPage = pageSales;
            this.Text = "販売管理システム - 売上集計";
        }

        public void ShowProductView() {
            mainFrame.SelectedPage = pageProduct;
            this.Text = "販売管理システム - 商品管理";
        }

        // --- メニュー（AccordionControl）からのイベント ---

        private void btnImport_Click(object sender, EventArgs e) {
            try{
                ShowImportView();
            } catch (Exception ex) {
                MessageManager.ShowError($"エラーが発生したため処理を終了しました。:{ex.Message}");
            }
        }

        private void btnSalesSummary_Click(object sender, EventArgs e) {
            try {
                ShowSalesView();
            } catch (Exception ex) {
                MessageManager.ShowError($"エラーが発生したため処理を終了しました。:{ex.Message}");
            }
        }

        private void btnProduct_Click(object sender, EventArgs e) {
            try {
                ShowProductView();
            } catch (Exception ex) {
                MessageManager.ShowError($"エラーが発生したため処理を終了しました。:{ex.Message}");
            }
        }

        private void salesView_Load(object sender, EventArgs e) {

        }
    }
}