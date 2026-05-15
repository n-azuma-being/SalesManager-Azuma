namespace SalesManager {
    partial class MainForm {
        /// <summary>
        /// 必要なデザイナー変数です。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 使用中のリソースをすべてクリーンアップします。
        /// </summary>
        /// <param name="disposing">マネージド リソースを破棄する場合は true を指定し、その他の場合は false を指定します。</param>
        protected override void Dispose(bool disposing) {
            if (disposing && (components != null)) {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows フォーム デザイナーで生成されたコード

        /// <summary>
        /// デザイナー サポートに必要なメソッドです。このメソッドの内容を
        /// コード エディターで変更しないでください。
        /// </summary>
        private void InitializeComponent() {
            this.leftNav = new DevExpress.XtraBars.Navigation.AccordionControl();
            this.btnImport = new DevExpress.XtraBars.Navigation.AccordionControlElement();
            this.btnSalesSummary = new DevExpress.XtraBars.Navigation.AccordionControlElement();
            this.btnProduct = new DevExpress.XtraBars.Navigation.AccordionControlElement();
            this.mainFrame = new DevExpress.XtraBars.Navigation.NavigationFrame();
            this.pageImport = new DevExpress.XtraBars.Navigation.NavigationPage();
            this.importView = new SalesManager.Views.ImportView();
            this.pageSales = new DevExpress.XtraBars.Navigation.NavigationPage();
            this.salesView = new SalesManager.Views.SalesView();
            this.pageProduct = new DevExpress.XtraBars.Navigation.NavigationPage();
            this.productView = new SalesManager.View.ProductView();
            ((System.ComponentModel.ISupportInitialize)(this.leftNav)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.mainFrame)).BeginInit();
            this.mainFrame.SuspendLayout();
            this.pageImport.SuspendLayout();
            this.pageSales.SuspendLayout();
            this.pageProduct.SuspendLayout();
            this.SuspendLayout();
            // 
            // leftNav
            // 
            this.leftNav.AllowItemSelection = true;
            this.leftNav.Dock = System.Windows.Forms.DockStyle.Left;
            this.leftNav.Elements.AddRange(new DevExpress.XtraBars.Navigation.AccordionControlElement[] {
            this.btnImport,
            this.btnSalesSummary,
            this.btnProduct});
            this.leftNav.Location = new System.Drawing.Point(0, 0);
            this.leftNav.Margin = new System.Windows.Forms.Padding(5, 2, 5, 2);
            this.leftNav.Name = "leftNav";
            this.leftNav.Size = new System.Drawing.Size(236, 576);
            this.leftNav.TabIndex = 20;
            this.leftNav.TabStop = false;
            // 
            // btnImport
            // 
            this.btnImport.Name = "btnImport";
            this.btnImport.Style = DevExpress.XtraBars.Navigation.ElementStyle.Item;
            this.btnImport.Text = "データ取込";
            this.btnImport.Click += new System.EventHandler(this.btnImport_Click);
            // 
            // btnSalesSummary
            // 
            this.btnSalesSummary.Name = "btnSalesSummary";
            this.btnSalesSummary.Style = DevExpress.XtraBars.Navigation.ElementStyle.Item;
            this.btnSalesSummary.Text = "売上集計";
            this.btnSalesSummary.Click += new System.EventHandler(this.btnSalesSummary_Click);
            // 
            // btnProduct
            // 
            this.btnProduct.Appearance.Hovered.BackColor = System.Drawing.Color.Cyan;
            this.btnProduct.Appearance.Hovered.Options.UseBackColor = true;
            this.btnProduct.HeaderTemplate.AddRange(new DevExpress.XtraBars.Navigation.HeaderElementInfo[] {
            new DevExpress.XtraBars.Navigation.HeaderElementInfo(DevExpress.XtraBars.Navigation.HeaderElementType.Text),
            new DevExpress.XtraBars.Navigation.HeaderElementInfo(DevExpress.XtraBars.Navigation.HeaderElementType.Image),
            new DevExpress.XtraBars.Navigation.HeaderElementInfo(DevExpress.XtraBars.Navigation.HeaderElementType.HeaderControl),
            new DevExpress.XtraBars.Navigation.HeaderElementInfo(DevExpress.XtraBars.Navigation.HeaderElementType.ContextButtons)});
            this.btnProduct.Name = "btnProduct";
            this.btnProduct.Style = DevExpress.XtraBars.Navigation.ElementStyle.Item;
            this.btnProduct.Text = "商品管理";
            this.btnProduct.Click += new System.EventHandler(this.btnProduct_Click);
            // 
            // mainFrame
            // 
            this.mainFrame.AllowTransitionAnimation = DevExpress.Utils.DefaultBoolean.False;
            this.mainFrame.Controls.Add(this.pageImport);
            this.mainFrame.Controls.Add(this.pageSales);
            this.mainFrame.Controls.Add(this.pageProduct);
            this.mainFrame.Dock = System.Windows.Forms.DockStyle.Fill;
            this.mainFrame.Location = new System.Drawing.Point(236, 0);
            this.mainFrame.Margin = new System.Windows.Forms.Padding(5, 2, 5, 2);
            this.mainFrame.Name = "mainFrame";
            this.mainFrame.Pages.AddRange(new DevExpress.XtraBars.Navigation.NavigationPageBase[] {
            this.pageImport,
            this.pageSales,
            this.pageProduct});
            this.mainFrame.SelectedPage = this.pageImport;
            this.mainFrame.Size = new System.Drawing.Size(996, 576);
            this.mainFrame.TabIndex = 0;
            this.mainFrame.Text = "navigationFrame1";
            // 
            // pageImport
            // 
            this.pageImport.Caption = "pageImport";
            this.pageImport.Controls.Add(this.importView);
            this.pageImport.Margin = new System.Windows.Forms.Padding(5, 2, 5, 2);
            this.pageImport.Name = "pageImport";
            this.pageImport.Size = new System.Drawing.Size(996, 576);
            this.pageImport.TabStop = false;
            // 
            // importView
            // 
            this.importView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.importView.Location = new System.Drawing.Point(0, 0);
            this.importView.Margin = new System.Windows.Forms.Padding(5, 6, 5, 6);
            this.importView.Name = "importView";
            this.importView.Size = new System.Drawing.Size(1245, 720);
            this.importView.TabIndex = 0;
            this.importView.TabStop = false;
            // 
            // pageSales
            // 
            this.pageSales.Caption = "pageSales";
            this.pageSales.Controls.Add(this.salesView);
            this.pageSales.Margin = new System.Windows.Forms.Padding(5, 2, 5, 2);
            this.pageSales.Name = "pageSales";
            this.pageSales.Size = new System.Drawing.Size(996, 576);
            // 
            // salesView
            // 
            this.salesView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.salesView.Location = new System.Drawing.Point(0, 0);
            this.salesView.Margin = new System.Windows.Forms.Padding(4);
            this.salesView.Name = "salesView";
            this.salesView.Padding = new System.Windows.Forms.Padding(13, 12, 13, 12);
            this.salesView.Size = new System.Drawing.Size(1245, 720);
            this.salesView.TabIndex = 0;
            this.salesView.TabStop = false;
            // 
            // pageProduct
            // 
            this.pageProduct.Caption = "pageProduct";
            this.pageProduct.Controls.Add(this.productView);
            this.pageProduct.Margin = new System.Windows.Forms.Padding(5, 6, 5, 6);
            this.pageProduct.Name = "pageProduct";
            this.pageProduct.Size = new System.Drawing.Size(996, 576);
            // 
            // productView
            // 
            this.productView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.productView.Location = new System.Drawing.Point(0, 0);
            this.productView.Margin = new System.Windows.Forms.Padding(5);
            this.productView.Name = "productView";
            this.productView.Padding = new System.Windows.Forms.Padding(17, 15, 17, 15);
            this.productView.Size = new System.Drawing.Size(1245, 720);
            this.productView.TabIndex = 1;
            this.productView.TabStop = false;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.ClientSize = new System.Drawing.Size(1232, 576);
            this.Controls.Add(this.mainFrame);
            this.Controls.Add(this.leftNav);
            this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.MinimumSize = new System.Drawing.Size(1000, 498);
            this.Name = "MainForm";
            this.Text = "販売管理アプリ";
            this.Load += new System.EventHandler(this.MainForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.leftNav)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.mainFrame)).EndInit();
            this.mainFrame.ResumeLayout(false);
            this.pageImport.ResumeLayout(false);
            this.pageSales.ResumeLayout(false);
            this.pageProduct.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraBars.Navigation.AccordionControl leftNav;
        private DevExpress.XtraBars.Navigation.AccordionControlElement btnImport;
        private DevExpress.XtraBars.Navigation.AccordionControlElement btnSalesSummary;
        private DevExpress.XtraBars.Navigation.AccordionControlElement btnProduct;
        private DevExpress.XtraBars.Navigation.NavigationFrame mainFrame;
        private DevExpress.XtraBars.Navigation.NavigationPage pageImport;
        private DevExpress.XtraBars.Navigation.NavigationPage pageSales;
        private DevExpress.XtraBars.Navigation.NavigationPage pageProduct;
        private Views.ImportView importView;
        private View.ProductView productView;
        private Views.SalesView salesView;
    }
}

