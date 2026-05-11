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
            this.lblWeeklyReport.Location = new System.Drawing.Point(128, 13);
            this.lblWeeklyReport.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblWeeklyReport.Name = "lblWeeklyReport";
            this.lblWeeklyReport.Size = new System.Drawing.Size(147, 14);
            this.lblWeeklyReport.TabIndex = 0;
            this.lblWeeklyReport.Text = "集計期間を選択してください。";
            // 
            // dtpSummaryStartDate
            // 
            this.dtpSummaryStartDate.Location = new System.Drawing.Point(37, 70);
            this.dtpSummaryStartDate.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.dtpSummaryStartDate.Name = "dtpSummaryStartDate";
            this.dtpSummaryStartDate.Size = new System.Drawing.Size(151, 22);
            this.dtpSummaryStartDate.TabIndex = 1;
            this.dtpSummaryStartDate.ValueChanged += new System.EventHandler(this.dtpSummaryStartDate_ValueChanged);
            // 
            // dtpSummaryEndDate
            // 
            this.dtpSummaryEndDate.Location = new System.Drawing.Point(215, 70);
            this.dtpSummaryEndDate.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.dtpSummaryEndDate.Name = "dtpSummaryEndDate";
            this.dtpSummaryEndDate.Size = new System.Drawing.Size(151, 22);
            this.dtpSummaryEndDate.TabIndex = 2;
            this.dtpSummaryEndDate.ValueChanged += new System.EventHandler(this.dtpSummaryEndDate_ValueChanged);
            // 
            // btnExportWeeklyReport
            // 
            this.btnExportWeeklyReport.Location = new System.Drawing.Point(131, 148);
            this.btnExportWeeklyReport.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.btnExportWeeklyReport.Name = "btnExportWeeklyReport";
            this.btnExportWeeklyReport.Size = new System.Drawing.Size(135, 34);
            this.btnExportWeeklyReport.TabIndex = 3;
            this.btnExportWeeklyReport.Text = "作成開始(&R)";
            this.btnExportWeeklyReport.UseVisualStyleBackColor = true;
            this.btnExportWeeklyReport.Click += new System.EventHandler(this.btnExportWeeklyReport_Click);
            // 
            // tilde
            // 
            this.tilde.AutoSize = true;
            this.tilde.Location = new System.Drawing.Point(192, 70);
            this.tilde.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.tilde.Name = "tilde";
            this.tilde.Size = new System.Drawing.Size(19, 14);
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
            this.panelControl1.Location = new System.Drawing.Point(12, 39);
            this.panelControl1.Name = "panelControl1";
            this.panelControl1.Size = new System.Drawing.Size(398, 186);
            this.panelControl1.TabIndex = 5;
            // 
            // WeeklyReportForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(434, 261);
            this.Controls.Add(this.panelControl1);
            this.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.MaximumSize = new System.Drawing.Size(450, 300);
            this.Name = "WeeklyReportForm";
            this.Text = "週次報告書作成";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
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