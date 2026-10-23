namespace BTL_LTTQ_2.GUI.Forms.NhapHang
{
    partial class frmHienCotNhapHang
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
            this.chkMaPhieu = new System.Windows.Forms.CheckBox();
            this.chkThoiGian = new System.Windows.Forms.CheckBox();
            this.chkMaNcc = new System.Windows.Forms.CheckBox();
            this.chkTenNcc = new System.Windows.Forms.CheckBox();
            this.chkCanTraNcc = new System.Windows.Forms.CheckBox();
            this.chkTongTien = new System.Windows.Forms.CheckBox();
            this.chkTrangThai = new System.Windows.Forms.CheckBox();
            this.pnlMain.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlMain
            // 
            this.pnlMain.BackColor = System.Drawing.Color.White;
            this.pnlMain.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlMain.Controls.Add(this.chkTrangThai);
            this.pnlMain.Controls.Add(this.chkTongTien);
            this.pnlMain.Controls.Add(this.chkCanTraNcc);
            this.pnlMain.Controls.Add(this.chkTenNcc);
            this.pnlMain.Controls.Add(this.chkMaNcc);
            this.pnlMain.Controls.Add(this.chkThoiGian);
            this.pnlMain.Controls.Add(this.chkMaPhieu);
            this.pnlMain.Controls.Add(this.lblTieuDe);
            this.pnlMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlMain.Location = new System.Drawing.Point(0, 0);
            this.pnlMain.Name = "pnlMain";
            this.pnlMain.Padding = new System.Windows.Forms.Padding(12);
            this.pnlMain.Size = new System.Drawing.Size(230, 260);
            this.pnlMain.TabIndex = 0;
            // 
            // lblTieuDe
            // 
            this.lblTieuDe.AutoSize = true;
            this.lblTieuDe.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold);
            this.lblTieuDe.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(41)))), ((int)(((byte)(59)))));
            this.lblTieuDe.Location = new System.Drawing.Point(12, 10);
            this.lblTieuDe.Name = "lblTieuDe";
            this.lblTieuDe.Size = new System.Drawing.Size(142, 17);
            this.lblTieuDe.TabIndex = 0;
            this.lblTieuDe.Text = "Tùy chọn cột hiển thị";
            // 
            // chkMaPhieu
            // 
            this.chkMaPhieu.AutoSize = true;
            this.chkMaPhieu.Location = new System.Drawing.Point(16, 40);
            this.chkMaPhieu.Name = "chkMaPhieu";
            this.chkMaPhieu.Size = new System.Drawing.Size(111, 21);
            this.chkMaPhieu.TabIndex = 1;
            this.chkMaPhieu.Text = "Mã nhập hàng";
            this.chkMaPhieu.UseVisualStyleBackColor = true;
            // 
            // chkThoiGian
            // 
            this.chkThoiGian.AutoSize = true;
            this.chkThoiGian.Location = new System.Drawing.Point(16, 70);
            this.chkThoiGian.Name = "chkThoiGian";
            this.chkThoiGian.Size = new System.Drawing.Size(81, 21);
            this.chkThoiGian.TabIndex = 2;
            this.chkThoiGian.Text = "Thời gian";
            this.chkThoiGian.UseVisualStyleBackColor = true;
            // 
            // chkMaNcc
            // 
            this.chkMaNcc.AutoSize = true;
            this.chkMaNcc.Location = new System.Drawing.Point(16, 100);
            this.chkMaNcc.Name = "chkMaNcc";
            this.chkMaNcc.Size = new System.Drawing.Size(130, 21);
            this.chkMaNcc.TabIndex = 3;
            this.chkMaNcc.Text = "Mã nhà cung cấp";
            this.chkMaNcc.UseVisualStyleBackColor = true;
            // 
            // chkTenNcc
            // 
            this.chkTenNcc.AutoSize = true;
            this.chkTenNcc.Location = new System.Drawing.Point(16, 130);
            this.chkTenNcc.Name = "chkTenNcc";
            this.chkTenNcc.Size = new System.Drawing.Size(131, 21);
            this.chkTenNcc.TabIndex = 4;
            this.chkTenNcc.Text = "Tên nhà cung cấp";
            this.chkTenNcc.UseVisualStyleBackColor = true;
            // 
            // chkCanTraNcc
            // 
            this.chkCanTraNcc.AutoSize = true;
            this.chkCanTraNcc.Location = new System.Drawing.Point(16, 160);
            this.chkCanTraNcc.Name = "chkCanTraNcc";
            this.chkCanTraNcc.Size = new System.Drawing.Size(95, 21);
            this.chkCanTraNcc.TabIndex = 5;
            this.chkCanTraNcc.Text = "Cần trả NCC";
            this.chkCanTraNcc.UseVisualStyleBackColor = true;
            // 
            // chkTongTien
            // 
            this.chkTongTien.AutoSize = true;
            this.chkTongTien.Location = new System.Drawing.Point(16, 190);
            this.chkTongTien.Name = "chkTongTien";
            this.chkTongTien.Size = new System.Drawing.Size(83, 21);
            this.chkTongTien.TabIndex = 6;
            this.chkTongTien.Text = "Tổng tiền";
            this.chkTongTien.UseVisualStyleBackColor = true;
            // 
            // chkTrangThai
            // 
            this.chkTrangThai.AutoSize = true;
            this.chkTrangThai.Location = new System.Drawing.Point(16, 220);
            this.chkTrangThai.Name = "chkTrangThai";
            this.chkTrangThai.Size = new System.Drawing.Size(86, 21);
            this.chkTrangThai.TabIndex = 7;
            this.chkTrangThai.Text = "Trạng thái";
            this.chkTrangThai.UseVisualStyleBackColor = true;
            // 
            // frmHienCotNhapHang
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(230, 260);
            this.Controls.Add(this.pnlMain);
            this.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.KeyPreview = true;
            this.Name = "frmHienCotNhapHang";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "Hiện cột";
            this.Deactivate += new System.EventHandler(this.frmHienCotNhapHang_Deactivate);
            this.pnlMain.ResumeLayout(false);
            this.pnlMain.PerformLayout();
            this.ResumeLayout(false);
        }

        #endregion

        private System.Windows.Forms.Panel pnlMain;
        private System.Windows.Forms.Label lblTieuDe;
        private System.Windows.Forms.CheckBox chkMaPhieu;
        private System.Windows.Forms.CheckBox chkThoiGian;
        private System.Windows.Forms.CheckBox chkMaNcc;
        private System.Windows.Forms.CheckBox chkTenNcc;
        private System.Windows.Forms.CheckBox chkCanTraNcc;
        private System.Windows.Forms.CheckBox chkTongTien;
        private System.Windows.Forms.CheckBox chkTrangThai;
    }
}
