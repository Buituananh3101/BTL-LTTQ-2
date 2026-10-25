namespace BTL_LTTQ_2.GUI.Forms.DoiKho
{
    partial class frmDoiKho
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            this.lblKhoLamViec = new System.Windows.Forms.Label();
            this.cboKhoLamViec = new System.Windows.Forms.ComboBox();
            this.btnXacNhan = new System.Windows.Forms.Button();
            this.btnHuy = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // lblKhoLamViec
            // 
            this.lblKhoLamViec.AutoSize = true;
            this.lblKhoLamViec.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            this.lblKhoLamViec.Location = new System.Drawing.Point(34, 61);
            this.lblKhoLamViec.Name = "lblKhoLamViec";
            this.lblKhoLamViec.Size = new System.Drawing.Size(101, 21);
            this.lblKhoLamViec.TabIndex = 0;
            this.lblKhoLamViec.Text = "Kho làm việc:";
            // 
            // cboKhoLamViec
            // 
            this.cboKhoLamViec.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboKhoLamViec.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            this.cboKhoLamViec.FormattingEnabled = true;
            this.cboKhoLamViec.Location = new System.Drawing.Point(38, 99);
            this.cboKhoLamViec.Name = "cboKhoLamViec";
            this.cboKhoLamViec.Size = new System.Drawing.Size(415, 29);
            this.cboKhoLamViec.TabIndex = 1;
            // 
            // btnXacNhan
            // 
            this.btnXacNhan.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            this.btnXacNhan.Location = new System.Drawing.Point(268, 20);
            this.btnXacNhan.Name = "btnXacNhan";
            this.btnXacNhan.Size = new System.Drawing.Size(95, 30);
            this.btnXacNhan.TabIndex = 2;
            this.btnXacNhan.Text = "Xác nhận";
            this.btnXacNhan.UseVisualStyleBackColor = true;
            this.btnXacNhan.Click += new System.EventHandler(this.btnXacNhan_Click);
            // 
            // btnHuy
            // 
            this.btnHuy.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnHuy.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            this.btnHuy.Location = new System.Drawing.Point(373, 20);
            this.btnHuy.Name = "btnHuy";
            this.btnHuy.Size = new System.Drawing.Size(95, 30);
            this.btnHuy.TabIndex = 3;
            this.btnHuy.Text = "Hủy";
            this.btnHuy.UseVisualStyleBackColor = true;
            this.btnHuy.Click += new System.EventHandler(this.btnHuy_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.btnHuy);
            this.groupBox1.Controls.Add(this.btnXacNhan);
            this.groupBox1.Location = new System.Drawing.Point(-15, 191);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(508, 56);
            this.groupBox1.TabIndex = 4;
            this.groupBox1.TabStop = false;
            // 
            // frmDoiKho
            // 
            this.AcceptButton = this.btnXacNhan;
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 21F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnHuy;
            this.ClientSize = new System.Drawing.Size(482, 253);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.cboKhoLamViec);
            this.Controls.Add(this.lblKhoLamViec);
            this.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmDoiKho";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Chọn kho làm việc";
            this.Load += new System.EventHandler(this.frmDoiKho_Load);
            this.groupBox1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblKhoLamViec;
        private System.Windows.Forms.ComboBox cboKhoLamViec;
        private System.Windows.Forms.Button btnXacNhan;
        private System.Windows.Forms.Button btnHuy;
        private System.Windows.Forms.GroupBox groupBox1;
    }
}
