namespace SalesManager.Views {
    partial class SalesView {
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

        #region コンポーネント デザイナーで生成されたコード

        /// <summary> 
        /// デザイナー サポートに必要なメソッドです。このメソッドの内容を 
        /// コード エディターで変更しないでください。
        /// </summary>
        private void InitializeComponent() {
            this.labelAggregationUnit = new System.Windows.Forms.Label();
            this.labelAggregationPeriod = new System.Windows.Forms.Label();
            this.salesGridControl = new DevExpress.XtraGrid.GridControl();
            this.gridView1 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.dtpSalesStartDate = new System.Windows.Forms.DateTimePicker();
            this.dtpSalesEndDate = new System.Windows.Forms.DateTimePicker();
            this.btnCreateWeeklyReport = new System.Windows.Forms.Button();
            this.tild = new System.Windows.Forms.Label();
            this.panelControl1 = new DevExpress.XtraEditors.PanelControl();
            this.cmbAggregationPeriod = new System.Windows.Forms.ComboBox();
            this.cmbAggregationUnit = new System.Windows.Forms.ComboBox();
            this.panelControl2 = new DevExpress.XtraEditors.PanelControl();
            this.lblSalesEndDate = new System.Windows.Forms.Label();
            this.lblSalesStartDate = new System.Windows.Forms.Label();
            this.panelControl3 = new DevExpress.XtraEditors.PanelControl();
            this.panelControl4 = new DevExpress.XtraEditors.PanelControl();
            ((System.ComponentModel.ISupportInitialize)(this.salesGridControl)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).BeginInit();
            this.panelControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl2)).BeginInit();
            this.panelControl2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl3)).BeginInit();
            this.panelControl3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl4)).BeginInit();
            this.panelControl4.SuspendLayout();
            this.SuspendLayout();
            // 
            // labelAggregationUnit
            // 
            this.labelAggregationUnit.AutoSize = true;
            this.labelAggregationUnit.Location = new System.Drawing.Point(3, 5);
            this.labelAggregationUnit.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.labelAggregationUnit.Name = "labelAggregationUnit";
            this.labelAggregationUnit.Size = new System.Drawing.Size(90, 18);
            this.labelAggregationUnit.TabIndex = 0;
            this.labelAggregationUnit.Text = "集計単位(&U)";
            // 
            // labelAggregationPeriod
            // 
            this.labelAggregationPeriod.AutoSize = true;
            this.labelAggregationPeriod.Location = new System.Drawing.Point(7, 45);
            this.labelAggregationPeriod.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.labelAggregationPeriod.Name = "labelAggregationPeriod";
            this.labelAggregationPeriod.Size = new System.Drawing.Size(120, 18);
            this.labelAggregationPeriod.TabIndex = 2;
            this.labelAggregationPeriod.Text = "集計対象期間(&T)";
            // 
            // salesGridControl
            // 
            this.salesGridControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.salesGridControl.EmbeddedNavigator.Margin = new System.Windows.Forms.Padding(4);
            this.salesGridControl.Location = new System.Drawing.Point(13, 140);
            this.salesGridControl.MainView = this.gridView1;
            this.salesGridControl.Margin = new System.Windows.Forms.Padding(4);
            this.salesGridControl.Name = "salesGridControl";
            this.salesGridControl.Size = new System.Drawing.Size(827, 396);
            this.salesGridControl.TabIndex = 11;
            this.salesGridControl.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridView1});
            // 
            // gridView1
            // 
            this.gridView1.DetailHeight = 437;
            this.gridView1.GridControl = this.salesGridControl;
            this.gridView1.Name = "gridView1";
            this.gridView1.OptionsBehavior.Editable = false;
            this.gridView1.OptionsCustomization.AllowFilter = false;
            this.gridView1.OptionsEditForm.PopupEditFormWidth = 1067;
            this.gridView1.OptionsFilter.AllowFilterEditor = false;
            this.gridView1.OptionsFilter.AllowMRUFilterList = false;
            this.gridView1.OptionsNavigation.EnterMoveNextColumn = true;
            this.gridView1.OptionsNavigation.UseTabKey = false;
            this.gridView1.OptionsView.ShowGroupExpandCollapseButtons = false;
            this.gridView1.OptionsView.ShowGroupPanel = false;
            // 
            // dtpSalesStartDate
            // 
            this.dtpSalesStartDate.CustomFormat = "yyyy/MM/dd";
            this.dtpSalesStartDate.Location = new System.Drawing.Point(4, 41);
            this.dtpSalesStartDate.Margin = new System.Windows.Forms.Padding(4);
            this.dtpSalesStartDate.Name = "dtpSalesStartDate";
            this.dtpSalesStartDate.Size = new System.Drawing.Size(156, 26);
            this.dtpSalesStartDate.TabIndex = 8;
            this.dtpSalesStartDate.ValueChanged += new System.EventHandler(this.dtpSalesStartDate_ValueChanged);
            this.dtpSalesStartDate.KeyDown += new System.Windows.Forms.KeyEventHandler(this.dtpSalesStartDate_KeyDown);
            // 
            // dtpSalesEndDate
            // 
            this.dtpSalesEndDate.CustomFormat = "yyyy/MM/dd";
            this.dtpSalesEndDate.Location = new System.Drawing.Point(199, 41);
            this.dtpSalesEndDate.Margin = new System.Windows.Forms.Padding(4);
            this.dtpSalesEndDate.Name = "dtpSalesEndDate";
            this.dtpSalesEndDate.Size = new System.Drawing.Size(156, 26);
            this.dtpSalesEndDate.TabIndex = 10;
            this.dtpSalesEndDate.ValueChanged += new System.EventHandler(this.dtpSalesEndDate_ValueChanged);
            // 
            // btnCreateWeeklyReport
            // 
            this.btnCreateWeeklyReport.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCreateWeeklyReport.Location = new System.Drawing.Point(587, 25);
            this.btnCreateWeeklyReport.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnCreateWeeklyReport.Name = "btnCreateWeeklyReport";
            this.btnCreateWeeklyReport.Size = new System.Drawing.Size(237, 50);
            this.btnCreateWeeklyReport.TabIndex = 12;
            this.btnCreateWeeklyReport.Text = "週次報告書を作成(&W)";
            this.btnCreateWeeklyReport.UseVisualStyleBackColor = true;
            this.btnCreateWeeklyReport.Click += new System.EventHandler(this.btnCreateWeeklyReport_Click);
            // 
            // tild
            // 
            this.tild.AutoSize = true;
            this.tild.Location = new System.Drawing.Point(168, 42);
            this.tild.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.tild.Name = "tild";
            this.tild.Size = new System.Drawing.Size(23, 18);
            this.tild.TabIndex = 10;
            this.tild.Text = "～";
            // 
            // panelControl1
            // 
            this.panelControl1.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.panelControl1.Controls.Add(this.cmbAggregationPeriod);
            this.panelControl1.Controls.Add(this.cmbAggregationUnit);
            this.panelControl1.Controls.Add(this.labelAggregationPeriod);
            this.panelControl1.Controls.Add(this.labelAggregationUnit);
            this.panelControl1.Location = new System.Drawing.Point(4, 4);
            this.panelControl1.Margin = new System.Windows.Forms.Padding(4);
            this.panelControl1.Name = "panelControl1";
            this.panelControl1.Size = new System.Drawing.Size(319, 79);
            this.panelControl1.TabIndex = 0;
            // 
            // cmbAggregationPeriod
            // 
            this.cmbAggregationPeriod.FormattingEnabled = true;
            this.cmbAggregationPeriod.Location = new System.Drawing.Point(147, 42);
            this.cmbAggregationPeriod.Name = "cmbAggregationPeriod";
            this.cmbAggregationPeriod.Size = new System.Drawing.Size(156, 26);
            this.cmbAggregationPeriod.TabIndex = 3;
            this.cmbAggregationPeriod.SelectedIndexChanged += new System.EventHandler(this.cmbAggregationPeriod_SelectedIndexChanged);
            // 
            // cmbAggregationUnit
            // 
            this.cmbAggregationUnit.FormattingEnabled = true;
            this.cmbAggregationUnit.Location = new System.Drawing.Point(147, 2);
            this.cmbAggregationUnit.Name = "cmbAggregationUnit";
            this.cmbAggregationUnit.Size = new System.Drawing.Size(156, 26);
            this.cmbAggregationUnit.TabIndex = 1;
            this.cmbAggregationUnit.SelectedIndexChanged += new System.EventHandler(this.cmbAggregationUnit_SelectedIndexChanged);
            // 
            // panelControl2
            // 
            this.panelControl2.AllowTouchScroll = true;
            this.panelControl2.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.panelControl2.Controls.Add(this.dtpSalesEndDate);
            this.panelControl2.Controls.Add(this.dtpSalesStartDate);
            this.panelControl2.Controls.Add(this.lblSalesEndDate);
            this.panelControl2.Controls.Add(this.lblSalesStartDate);
            this.panelControl2.Controls.Add(this.tild);
            this.panelControl2.Location = new System.Drawing.Point(331, 4);
            this.panelControl2.Margin = new System.Windows.Forms.Padding(4);
            this.panelControl2.Name = "panelControl2";
            this.panelControl2.Size = new System.Drawing.Size(358, 79);
            this.panelControl2.TabIndex = 4;
            // 
            // lblSalesEndDate
            // 
            this.lblSalesEndDate.AutoSize = true;
            this.lblSalesEndDate.Location = new System.Drawing.Point(196, 14);
            this.lblSalesEndDate.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblSalesEndDate.Name = "lblSalesEndDate";
            this.lblSalesEndDate.Size = new System.Drawing.Size(73, 18);
            this.lblSalesEndDate.TabIndex = 9;
            this.lblSalesEndDate.Text = "終了日(&Z)";
            // 
            // lblSalesStartDate
            // 
            this.lblSalesStartDate.AutoSize = true;
            this.lblSalesStartDate.Location = new System.Drawing.Point(4, 14);
            this.lblSalesStartDate.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblSalesStartDate.Name = "lblSalesStartDate";
            this.lblSalesStartDate.Size = new System.Drawing.Size(74, 18);
            this.lblSalesStartDate.TabIndex = 7;
            this.lblSalesStartDate.Text = "開始日(&A)";
            // 
            // panelControl3
            // 
            this.panelControl3.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.panelControl3.Controls.Add(this.btnCreateWeeklyReport);
            this.panelControl3.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panelControl3.Location = new System.Drawing.Point(13, 536);
            this.panelControl3.Margin = new System.Windows.Forms.Padding(4);
            this.panelControl3.Name = "panelControl3";
            this.panelControl3.Size = new System.Drawing.Size(827, 77);
            this.panelControl3.TabIndex = 12;
            // 
            // panelControl4
            // 
            this.panelControl4.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.panelControl4.Controls.Add(this.panelControl2);
            this.panelControl4.Controls.Add(this.panelControl1);
            this.panelControl4.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelControl4.Location = new System.Drawing.Point(13, 12);
            this.panelControl4.Margin = new System.Windows.Forms.Padding(4);
            this.panelControl4.Name = "panelControl4";
            this.panelControl4.Size = new System.Drawing.Size(827, 128);
            this.panelControl4.TabIndex = 0;
            // 
            // SalesView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.salesGridControl);
            this.Controls.Add(this.panelControl3);
            this.Controls.Add(this.panelControl4);
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "SalesView";
            this.Padding = new System.Windows.Forms.Padding(13, 12, 13, 12);
            this.Size = new System.Drawing.Size(853, 625);
            this.Load += new System.EventHandler(this.SalesView_Load);
            ((System.ComponentModel.ISupportInitialize)(this.salesGridControl)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).EndInit();
            this.panelControl1.ResumeLayout(false);
            this.panelControl1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl2)).EndInit();
            this.panelControl2.ResumeLayout(false);
            this.panelControl2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl3)).EndInit();
            this.panelControl3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.panelControl4)).EndInit();
            this.panelControl4.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label labelAggregationUnit;
        private System.Windows.Forms.Label labelAggregationPeriod;
        private DevExpress.XtraGrid.GridControl salesGridControl;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView1;
        private System.Windows.Forms.DateTimePicker dtpSalesStartDate;
        private System.Windows.Forms.DateTimePicker dtpSalesEndDate;
        private System.Windows.Forms.Button btnCreateWeeklyReport;
        private System.Windows.Forms.Label tild;
        private DevExpress.XtraEditors.PanelControl panelControl1;
        private DevExpress.XtraEditors.PanelControl panelControl2;
        private DevExpress.XtraEditors.PanelControl panelControl3;
        private DevExpress.XtraEditors.PanelControl panelControl4;
        private System.Windows.Forms.Label lblSalesEndDate;
        private System.Windows.Forms.Label lblSalesStartDate;
        private System.Windows.Forms.ComboBox cmbAggregationPeriod;
        private System.Windows.Forms.ComboBox cmbAggregationUnit;
    }
}
