namespace BTL_LTTQ_2.GUI.Forms.NhaCungCap
{
    partial class frmHienCotNcc
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
            this.pnlMain = new System.Windows.Forms.Panel();
            this.lblTieuDe = new System.Windows.Forms.Label();
            this.chkMaNcc = new System.Windows.Forms.CheckBox();
            this.chkTenNcc = new System.Windows.Forms.CheckBox();
            this.chkDienThoai = new System.Windows.Forms.CheckBox();
            this.chkEmail = new System.Windows.Forms.CheckBox();
            this.chkNoCanTra = new System.Windows.Forms.CheckBox();
            this.chkTongMua = new System.Windows.Forms.CheckBox();
            this.chkTrangThai = new System.Windows.Forms.CheckBox();
            this.pnlMain.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlMain
            // 
            this.pnlMain.BackColor = System.Drawing.Color.White;
            this.pnlMain.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlMain.Controls.Add(this.chkTrangThai);
            this.pnlMain.Controls.Add(this.chkTongMua);
            this.pnlMain.Controls.Add(this.chkNoCanTra);
            this.pnlMain.Controls.Add(this.chkEmail);
            this.pnlMain.Controls.Add(this.chkDienThoai);
            this.pnlMain.Controls.Add(this.chkTenNcc);
            this.pnlMain.Controls.Add(this.chkMaNcc);
            this.pnlMain.Controls.Add(this.lblTieuDe);
            this.pnlMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlMain.Location = new System.Drawing.Point(0, 0);
            this.pnlMain.Name = "pnlMain";
            this.pnlMain.Padding = new System.Windows.Forms.Padding(12);
            this.pnlMain.Size = new System.Drawing.Size(230, 275);
            this.pnlMain.TabIndex = 0;
            // 
            // lblTieuDe
            // 
            this.lblTieuDe.AutoSize = true;
            this.lblTieuDe.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold);
            this.lblTieuDe.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(41)))), ((int)(((byte)(59)))));
            this.lblTieuDe.Location = new System.Drawing.Point(12, 10);
            this.lblTieuDe.Name = "lblTieuDe";
            this.lblTieuDe.Size = new System.Drawing.Size(142, 21);
            this.lblTieuDe.TabIndex = 0;
            this.lblTieuDe.Text = "Tùy chọn cột hiển thị";
            // 
            // chkMaNcc
            // 
            this.chkMaNcc.AutoSize = true;
            this.chkMaNcc.Location = new System.Drawing.Point(16, 42);
            this.chkMaNcc.Name = "chkMaNcc";
            this.chkMaNcc.Size = new System.Drawing.Size(151, 25);
            this.chkMaNcc.TabIndex = 1;
            this.chkMaNcc.Text = "Mã nhà cung cấp";
            this.chkMaNcc.UseVisualStyleBackColor = true;
            // 
            // chkTenNcc
            // 
            this.chkTenNcc.AutoSize = true;
            this.chkTenNcc.Location = new System.Drawing.Point(16, 73);
            this.chkTenNcc.Name = "chkTenNcc";
            this.chkTenNcc.Size = new System.Drawing.Size(153, 25);
            this.chkTenNcc.TabIndex = 2;
            this.chkTenNcc.Text = "Tên nhà cung cấp";
            this.chkTenNcc.UseVisualStyleBackColor = true;
            // 
            // chkDienThoai
            // 
            this.chkDienThoai.AutoSize = true;
            this.chkDienThoai.Location = new System.Drawing.Point(16, 104);
            this.chkDienThoai.Name = "chkDienThoai";
            this.chkDienThoai.Size = new System.Drawing.Size(103, 25);
            this.chkDienThoai.TabIndex = 3;
            this.chkDienThoai.Text = "Điện thoại";
            this.chkDienThoai.UseVisualStyleBackColor = true;
            // 
            // chkEmail
            // 
            this.chkEmail.AutoSize = true;
            this.chkEmail.Location = new System.Drawing.Point(16, 135);
            this.chkEmail.Name = "chkEmail";
            this.chkEmail.Size = new System.Drawing.Size(70, 25);
            this.chkEmail.TabIndex = 4;
            this.chkEmail.Text = "Email";
            this.chkEmail.UseVisualStyleBackColor = true;
            // 
            // chkNoCanTra
            // 
            this.chkNoCanTra.AutoSize = true;
            this.chkNoCanTra.Location = new System.Drawing.Point(16, 166);
            this.chkNoCanTra.Name = "chkNoCanTra";
            this.chkNoCanTra.Size = new System.Drawing.Size(161, 25);
            this.chkNoCanTra.TabIndex = 5;
            this.chkNoCanTra.Text = "Nợ cần trả hiện tại";
            this.chkNoCanTra.UseVisualStyleBackColor = true;
            // 
            // chkTongMua
            // 
            this.chkTongMua.AutoSize = true;
            this.chkTongMua.Location = new System.Drawing.Point(16, 197);
            this.chkTongMua.Name = "chkTongMua";
            this.chkTongMua.Size = new System.Drawing.Size(100, 25);
            this.chkTongMua.TabIndex = 6;
            this.chkTongMua.Text = "Tổng mua";
            this.chkTongMua.UseVisualStyleBackColor = true;
            // 
            // chkTrangThai
            // 
            this.chkTrangThai.AutoSize = true;
            this.chkTrangThai.Location = new System.Drawing.Point(16, 228);
            this.chkTrangThai.Name = "chkTrangThai";
            this.chkTrangThai.Size = new System.Drawing.Size(101, 25);
            this.chkTrangThai.TabIndex = 7;
            this.chkTrangThai.Text = "Trạng thái";
            this.chkTrangThai.UseVisualStyleBackColor = true;
            // 
            // frmHienCotNcc
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 21F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(230, 275);
            this.Controls.Add(this.pnlMain);
            this.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.KeyPreview = true;
            this.Name = "frmHienCotNcc";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "Hiện cột";
            this.Deactivate += new System.EventHandler(this.frmHienCotNcc_Deactivate);
            this.pnlMain.ResumeLayout(false);
            this.pnlMain.PerformLayout();
            this.ResumeLayout(false);
        }

        #endregion

        private System.Windows.Forms.Panel pnlMain;
        private System.Windows.Forms.Label lblTieuDe;
        private System.Windows.Forms.CheckBox chkMaNcc;
        private System.Windows.Forms.CheckBox chkTenNcc;
        private System.Windows.Forms.CheckBox chkDienThoai;
        private System.Windows.Forms.CheckBox chkEmail;
        private System.Windows.Forms.CheckBox chkNoCanTra;
        private System.Windows.Forms.CheckBox chkTongMua;
        private System.Windows.Forms.CheckBox chkTrangThai;
    }
}
