using DevExpress.XtraEditors;
using DevExpress.XtraGrid;
using SalesManager.Infrastructure;
using SalesManager.Services;
using System;
using System.Drawing;
using System.Windows.Forms;
using System.IO;

namespace SalesManager.View {
    public partial class ProductView : UserControl {

        private readonly ProductService _productService = new ProductService();

        public ProductView() {
            InitializeComponent();
        }

        private void ProductView_Load(object sender, EventArgs e) {
            DisplayProductList();
        }

        public void DisplayProductList() {
            productGridControl.DataSource = _productService.GetProduct();
            SetupGridFormatting();
        }
        /// <summary>
        /// ビューの見た目を調整
        /// </summary>
        public void SetupGridFormatting() {
            var view = productGridControl.MainView as DevExpress.XtraGrid.Views.Grid.GridView;
            if (view == null) return;

            GridFormatRule formatRule = new GridFormatRule();
            FormatConditionRuleValue ruleValue = new FormatConditionRuleValue();

            formatRule.Column = view.Columns["在庫数"];
            formatRule.ApplyToRow = true; //対象行全体の色を変更

            ruleValue.Condition = FormatCondition.LessOrEqual;
            ruleValue.Value1 = ProductService.LowStockThreshold;

            ruleValue.Appearance.BackColor = Color.MistyRose;
            ruleValue.Appearance.ForeColor = Color.Red;
            ruleValue.Appearance.Options.UseBackColor = true;
            ruleValue.Appearance.Options.UseForeColor = true;

            formatRule.Rule = ruleValue;
            view.FormatRules.Add(formatRule);
        }

        private void ExportFile(string folderPath) {
            try {
                string templatePath = Path.Combine(Application.StartupPath, "Templates", "OrderTemplate.xlsx");

                _productService.RequestOrderReport(templatePath, folderPath);

                MessageBox.Show("発注書の作成が完了しました。", "完了", MessageBoxButtons.OK, MessageBoxIcon.Information);
            } catch (Exception ex) {
                MessageBox.Show("出力に失敗しました: " + ex.Message, "エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnCreatePurchaseOrder_Click(object sender, EventArgs e) {
            string selectedPath = FileManager.ShowFolderDialog(this, "発注書の出力先フォルダを選択してください");

            if (selectedPath != null) {
                ExportFile(selectedPath);
            }
        }
    }
}
