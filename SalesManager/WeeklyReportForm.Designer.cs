namespace SalesManager {
    partial class WeeklyReportForm {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing) {
            if (disposing && (components != null)) {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent() {
            this.lblWeeklyReport = new System.Windows.Forms.Label();
            this.dtpSummaryStartDate = new System.Windows.Forms.DateTimePicker();
            this.dtpSummaryEndDate = new System.Windows.Forms.DateTimePicker();
            this.btnExportWeeklyReport = new System.Windows.Forms.Button();
            this.tilde = new System.Windows.Forms.Label();
            this.panelControl1 = new DevExpress.XtraEditors.PanelControl();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).BeginInit();
            this.panelControl1.SuspendLayout();
            this.SuspendLayout();
            // 
            // lblWeeklyReport
            // 
            this.lblWeeklyReport.AutoSize = true;
            this.lblWeeklyReport.Location = new System.Drawing.Point(171, 16);
            this.lblWeeklyReport.Name = "lblWeeklyReport";
            this.lblWeeklyReport.Size = new System.Drawing.Size(184, 18);
            this.lblWeeklyReport.TabIndex = 1;
            this.lblWeeklyReport.Text = "集計期間を選択してください。";
            // 
            // dtpSummaryStartDate
            // 
            this.dtpSummaryStartDate.CustomFormat = "yyyy/MM/dd";
            this.dtpSummaryStartDate.Location = new System.Drawing.Point(49, 88);
            this.dtpSummaryStartDate.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.dtpSummaryStartDate.Name = "dtpSummaryStartDate";
            this.dtpSummaryStartDate.Size = new System.Drawing.Size(200, 26);
            this.dtpSummaryStartDate.TabIndex = 1;
            this.dtpSummaryStartDate.ValueChanged += new System.EventHandler(this.dtpSummaryStartDate_ValueChanged);
            // 
            // dtpSummaryEndDate
            // 
            this.dtpSummaryEndDate.CustomFormat = "yyyy/MM/dd";
            this.dtpSummaryEndDate.Enabled = false;
            this.dtpSummaryEndDate.Location = new System.Drawing.Point(287, 88);
            this.dtpSummaryEndDate.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.dtpSummaryEndDate.Name = "dtpSummaryEndDate";
            this.dtpSummaryEndDate.Size = new System.Drawing.Size(200, 26);
            this.dtpSummaryEndDate.TabIndex = 2;
            this.dtpSummaryEndDate.ValueChanged += new System.EventHandler(this.dtpSummaryEndDate_ValueChanged);
            // 
            // btnExportWeeklyReport
            // 
            this.btnExportWeeklyReport.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnExportWeeklyReport.Location = new System.Drawing.Point(175, 185);
            this.btnExportWeeklyReport.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnExportWeeklyReport.Name = "btnExportWeeklyReport";
            this.btnExportWeeklyReport.Size = new System.Drawing.Size(180, 42);
            this.btnExportWeeklyReport.TabIndex = 3;
            this.btnExportWeeklyReport.Text = "作成開始(&R)";
            this.btnExportWeeklyReport.UseVisualStyleBackColor = true;
            this.btnExportWeeklyReport.Click += new System.EventHandler(this.btnExportWeeklyReport_Click);
            // 
            // tilde
            // 
            this.tilde.AutoSize = true;
            this.tilde.Location = new System.Drawing.Point(256, 88);
            this.tilde.Name = "tilde";
            this.tilde.Size = new System.Drawing.Size(23, 18);
            this.tilde.TabIndex = 4;
            this.tilde.Text = "～";
            // 
            // panelControl1
            // 
            this.panelControl1.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.panelControl1.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.panelControl1.Controls.Add(this.btnExportWeeklyReport);
            this.panelControl1.Controls.Add(this.tilde);
            this.panelControl1.Controls.Add(this.dtpSummaryEndDate);
            this.panelControl1.Controls.Add(this.dtpSummaryStartDate);
            this.panelControl1.Controls.Add(this.lblWeeklyReport);
            this.panelControl1.Location = new System.Drawing.Point(16, 49);
            this.panelControl1.Margin = new System.Windows.Forms.Padding(4);
            this.panelControl1.Name = "panelControl1";
            this.panelControl1.Size = new System.Drawing.Size(531, 232);
            this.panelControl1.TabIndex = 0;
            // 
            // WeeklyReportForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(576, 316);
            this.Controls.Add(this.panelControl1);
            this.Location = new System.Drawing.Point(10, 20);
            this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(594, 363);
            this.MinimizeBox = false;
            this.Name = "WeeklyReportForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "週次報告書作成";
            this.Load += new System.EventHandler(this.WeeklyReportForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).EndInit();
            this.panelControl1.ResumeLayout(false);
            this.panelControl1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label lblWeeklyReport;
        private System.Windows.Forms.DateTimePicker dtpSummaryStartDate;
        private System.Windows.Forms.DateTimePicker dtpSummaryEndDate;
        private System.Windows.Forms.Button btnExportWeeklyReport;
        private System.Windows.Forms.Label tilde;
        private DevExpress.XtraEditors.PanelControl panelControl1;
    }
}