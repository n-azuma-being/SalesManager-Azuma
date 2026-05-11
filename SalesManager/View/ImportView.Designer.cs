namespace SalesManager.Views {
    partial class ImportView {
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
            this.txtFilePath = new System.Windows.Forms.TextBox();
            this.btnBrowse = new System.Windows.Forms.Button();
            this.btnExecuteImport = new System.Windows.Forms.Button();
            this.panelControl1 = new DevExpress.XtraEditors.PanelControl();
            this.lblFileName1 = new System.Windows.Forms.Label();
            this.lblFileName2 = new System.Windows.Forms.Label();
            this.lblFileName3 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).BeginInit();
            this.panelControl1.SuspendLayout();
            this.SuspendLayout();
            // 
            // txtFilePath
            // 
            this.txtFilePath.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.txtFilePath.Location = new System.Drawing.Point(0, 4);
            this.txtFilePath.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.txtFilePath.Name = "txtFilePath";
            this.txtFilePath.Size = new System.Drawing.Size(295, 26);
            this.txtFilePath.TabIndex = 0;
            this.txtFilePath.TextChanged += new System.EventHandler(this.txtFilePath_TextChanged);
            // 
            // btnBrowse
            // 
            this.btnBrowse.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.btnBrowse.Location = new System.Drawing.Point(318, 3);
            this.btnBrowse.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btnBrowse.Name = "btnBrowse";
            this.btnBrowse.Size = new System.Drawing.Size(75, 28);
            this.btnBrowse.TabIndex = 1;
            this.btnBrowse.Text = "参照(&B)";
            this.btnBrowse.UseVisualStyleBackColor = true;
            this.btnBrowse.Click += new System.EventHandler(this.btnBrowse_Click);
            // 
            // btnExecuteImport
            // 
            this.btnExecuteImport.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.btnExecuteImport.Location = new System.Drawing.Point(115, 168);
            this.btnExecuteImport.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btnExecuteImport.Name = "btnExecuteImport";
            this.btnExecuteImport.Size = new System.Drawing.Size(171, 60);
            this.btnExecuteImport.TabIndex = 2;
            this.btnExecuteImport.Text = "集計開始(&R)";
            this.btnExecuteImport.UseVisualStyleBackColor = true;
            this.btnExecuteImport.Click += new System.EventHandler(this.btnExecuteImport_Click);
            // 
            // panelControl1
            // 
            this.panelControl1.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.panelControl1.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.panelControl1.Controls.Add(this.lblFileName3);
            this.panelControl1.Controls.Add(this.lblFileName2);
            this.panelControl1.Controls.Add(this.lblFileName1);
            this.panelControl1.Controls.Add(this.btnBrowse);
            this.panelControl1.Controls.Add(this.btnExecuteImport);
            this.panelControl1.Controls.Add(this.txtFilePath);
            this.panelControl1.Location = new System.Drawing.Point(180, 138);
            this.panelControl1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.panelControl1.Name = "panelControl1";
            this.panelControl1.Size = new System.Drawing.Size(393, 232);
            this.panelControl1.TabIndex = 3;
            // 
            // lblFileName1
            // 
            this.lblFileName1.AutoSize = true;
            this.lblFileName1.Location = new System.Drawing.Point(27, 57);
            this.lblFileName1.Name = "lblFileName1";
            this.lblFileName1.Size = new System.Drawing.Size(44, 18);
            this.lblFileName1.TabIndex = 3;
            this.lblFileName1.Text = "label1";
            this.lblFileName1.Visible = false;
            // 
            // lblFileName2
            // 
            this.lblFileName2.AutoSize = true;
            this.lblFileName2.Location = new System.Drawing.Point(27, 75);
            this.lblFileName2.Name = "lblFileName2";
            this.lblFileName2.Size = new System.Drawing.Size(44, 18);
            this.lblFileName2.TabIndex = 4;
            this.lblFileName2.Text = "label2";
            this.lblFileName2.Visible = false;
            // 
            // lblFileName3
            // 
            this.lblFileName3.AutoSize = true;
            this.lblFileName3.Location = new System.Drawing.Point(27, 93);
            this.lblFileName3.Name = "lblFileName3";
            this.lblFileName3.Size = new System.Drawing.Size(44, 18);
            this.lblFileName3.TabIndex = 5;
            this.lblFileName3.Text = "label3";
            this.lblFileName3.Visible = false;
            // 
            // ImportView
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Inherit;
            this.Controls.Add(this.panelControl1);
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Name = "ImportView";
            this.Size = new System.Drawing.Size(731, 501);
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).EndInit();
            this.panelControl1.ResumeLayout(false);
            this.panelControl1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TextBox txtFilePath;
        private System.Windows.Forms.Button btnBrowse;
        private System.Windows.Forms.Button btnExecuteImport;
        private DevExpress.XtraEditors.PanelControl panelControl1;
        private System.Windows.Forms.Label lblFileName3;
        private System.Windows.Forms.Label lblFileName2;
        private System.Windows.Forms.Label lblFileName1;
    }
}
