namespace BTL_LTTQ_2.GUI.Forms.TraHangNhap
{
    partial class frmHienCotTraHang
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
            this.chkTenNcc = new System.Windows.Forms.CheckBox();
            this.chkTongTienHang = new System.Windows.Forms.CheckBox();
            this.chkGiamGia = new System.Windows.Forms.CheckBox();
            this.chkCanThuNcc = new System.Windows.Forms.CheckBox();
            this.chkDaThuNcc = new System.Windows.Forms.CheckBox();
            this.chkTrangThai = new System.Windows.Forms.CheckBox();
            this.pnlMain.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlMain
            // 
            this.pnlMain.BackColor = System.Drawing.Color.White;
            this.pnlMain.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlMain.Controls.Add(this.chkTrangThai);
            this.pnlMain.Controls.Add(this.chkDaThuNcc);
            this.pnlMain.Controls.Add(this.chkCanThuNcc);
            this.pnlMain.Controls.Add(this.chkGiamGia);
            this.pnlMain.Controls.Add(this.chkTongTienHang);
            this.pnlMain.Controls.Add(this.chkTenNcc);
            this.pnlMain.Controls.Add(this.chkThoiGian);
            this.pnlMain.Controls.Add(this.chkMaPhieu);
            this.pnlMain.Controls.Add(this.lblTieuDe);
            this.pnlMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlMain.Location = new System.Drawing.Point(0, 0);
            this.pnlMain.Name = "pnlMain";
            this.pnlMain.Padding = new System.Windows.Forms.Padding(12);
            this.pnlMain.Size = new System.Drawing.Size(230, 290);
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
            this.chkMaPhieu.Size = new System.Drawing.Size(134, 21);
            this.chkMaPhieu.TabIndex = 1;
            this.chkMaPhieu.Text = "Mã trả hàng nhập";
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
            // chkTenNcc
            // 
            this.chkTenNcc.AutoSize = true;
            this.chkTenNcc.Location = new System.Drawing.Point(16, 100);
            this.chkTenNcc.Name = "chkTenNcc";
            this.chkTenNcc.Size = new System.Drawing.Size(110, 21);
            this.chkTenNcc.TabIndex = 3;
            this.chkTenNcc.Text = "Nhà cung cấp";
            this.chkTenNcc.UseVisualStyleBackColor = true;
            // 
            // chkTongTienHang
            // 
            this.chkTongTienHang.AutoSize = true;
            this.chkTongTienHang.Location = new System.Drawing.Point(16, 130);
            this.chkTongTienHang.Name = "chkTongTienHang";
            this.chkTongTienHang.Size = new System.Drawing.Size(114, 21);
            this.chkTongTienHang.TabIndex = 4;
            this.chkTongTienHang.Text = "Tổng tiền hàng";
            this.chkTongTienHang.UseVisualStyleBackColor = true;
            // 
            // chkGiamGia
            // 
            this.chkGiamGia.AutoSize = true;
            this.chkGiamGia.Location = new System.Drawing.Point(16, 160);
            this.chkGiamGia.Name = "chkGiamGia";
            this.chkGiamGia.Size = new System.Drawing.Size(80, 21);
            this.chkGiamGia.TabIndex = 5;
            this.chkGiamGia.Text = "Giảm giá";
            this.chkGiamGia.UseVisualStyleBackColor = true;
            // 
            // chkCanThuNcc
            // 
            this.chkCanThuNcc.AutoSize = true;
            this.chkCanThuNcc.Location = new System.Drawing.Point(16, 190);
            this.chkCanThuNcc.Name = "chkCanThuNcc";
            this.chkCanThuNcc.Size = new System.Drawing.Size(100, 21);
            this.chkCanThuNcc.TabIndex = 6;
            this.chkCanThuNcc.Text = "NCC cần trả";
            this.chkCanThuNcc.UseVisualStyleBackColor = true;
            // 
            // chkDaThuNcc
            // 
            this.chkDaThuNcc.AutoSize = true;
            this.chkDaThuNcc.Location = new System.Drawing.Point(16, 220);
            this.chkDaThuNcc.Name = "chkDaThuNcc";
            this.chkDaThuNcc.Size = new System.Drawing.Size(94, 21);
            this.chkDaThuNcc.TabIndex = 7;
            this.chkDaThuNcc.Text = "NCC đã trả";
            this.chkDaThuNcc.UseVisualStyleBackColor = true;
            // 
            // chkTrangThai
            // 
            this.chkTrangThai.AutoSize = true;
            this.chkTrangThai.Location = new System.Drawing.Point(16, 250);
            this.chkTrangThai.Name = "chkTrangThai";
            this.chkTrangThai.Size = new System.Drawing.Size(87, 21);
            this.chkTrangThai.TabIndex = 8;
            this.chkTrangThai.Text = "Trạng thái";
            this.chkTrangThai.UseVisualStyleBackColor = true;
            // 
            // frmHienCotTraHang
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(230, 290);
            this.Controls.Add(this.pnlMain);
            this.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.KeyPreview = true;
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "frmHienCotTraHang";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "Tùy chọn cột";
            this.Deactivate += new System.EventHandler(this.frmHienCotTraHang_Deactivate);
            this.pnlMain.ResumeLayout(false);
            this.pnlMain.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pnlMain;
        private System.Windows.Forms.Label lblTieuDe;
        private System.Windows.Forms.CheckBox chkMaPhieu;
        private System.Windows.Forms.CheckBox chkThoiGian;
        private System.Windows.Forms.CheckBox chkTenNcc;
        private System.Windows.Forms.CheckBox chkTongTienHang;
        private System.Windows.Forms.CheckBox chkGiamGia;
        private System.Windows.Forms.CheckBox chkCanThuNcc;
        private System.Windows.Forms.CheckBox chkDaThuNcc;
        private System.Windows.Forms.CheckBox chkTrangThai;
    }
}
