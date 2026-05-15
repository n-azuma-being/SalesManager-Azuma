using DevExpress.XtraEditors;
using DevExpress.XtraGrid;
using SalesManager.Infrastructure;
using SalesManager.Services;
using System;
using System.Drawing;
using System.Windows.Forms;
using System.IO;
using SalesManager.Service;

namespace SalesManager.View {
    /// <summary>
    /// 商品管理画面を表示するためのクラス
    /// </summary>
    public partial class ProductView : UserControl {

        private readonly ProductService _productService = new ProductService();

        public void FocusDefault() {
            productGridControl.Focus();//初期フォーカス位置
        }

        public ProductView() {
            InitializeComponent();
        }

        private void ProductView_Load(object sender, EventArgs e) {
            DisplayProductList();
        }

        public void DisplayProductList() {
            var products = _productService.GetProduct();
            productGridControl.DataSource = products;
            if (products.Count <= 0) {
                btnCreatePurchaseOrder.Enabled = products.Count > 0;
            }
            SetupGridFormatting();
        }

        /// <summary>
        /// ビューの見た目を調整
        /// </summary>
        public void SetupGridFormatting() {
            var view = productGridControl.MainView as DevExpress.XtraGrid.Views.Grid.GridView;
            if (view == null) return;

            view.FormatRules.Clear();

            GridFormatRule formatRule = new GridFormatRule();
            FormatConditionRuleValue ruleValue = new FormatConditionRuleValue();

            formatRule.Column = view.Columns["StockQuantity"];
            formatRule.ApplyToRow = true; //対象行全体の色を変更

            ruleValue.Condition = FormatCondition.LessOrEqual;
            ruleValue.Value1 = OrderReportService.LowStockThreshold;

            ruleValue.Appearance.BackColor = Color.MistyRose;
            ruleValue.Appearance.ForeColor = Color.Red;
            ruleValue.Appearance.Options.UseBackColor = true;
            ruleValue.Appearance.Options.UseForeColor = true;

            formatRule.Rule = ruleValue;
            view.FormatRules.Add(formatRule);
        }

        //tabキー ループ処理
        protected override bool ProcessDialogKey(Keys keyData) {
            if (FocusManager.HandleTabLoop(this.ActiveControl, productGridControl, btnCreatePurchaseOrder, keyData)) {
                return true;
            }
            return base.ProcessDialogKey(keyData);
        }

        // --- イベント ---

        //発注書作成ボタン
        private void btnCreatePurchaseOrder_Click(object sender, EventArgs e) {
            string selectedPath = FileManager.ShowFolderDialog("発注書の保存先を選択してください", "フォルダ");

            if (selectedPath != null) {
                string templatePath = Path.Combine(Application.StartupPath, "Templates", "OrderTemplate.xlsx");
                _productService.ExportFile(selectedPath, templatePath);
            }
        }
    }
}
