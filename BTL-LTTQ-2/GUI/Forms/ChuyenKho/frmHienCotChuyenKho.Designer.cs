namespace BTL_LTTQ_2.GUI.Forms.ChuyenKho
{
    partial class frmHienCotChuyenKho
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
            this.lblTieuDe = new System.Windows.Forms.Label();
            this.chkMaPhieu = new System.Windows.Forms.CheckBox();
            this.chkNgayLap = new System.Windows.Forms.CheckBox();
            this.chkKhoNguon = new System.Windows.Forms.CheckBox();
            this.chkKhoDich = new System.Windows.Forms.CheckBox();
            this.chkTongSlChuyen = new System.Windows.Forms.CheckBox();
            this.chkTongSlNhan = new System.Windows.Forms.CheckBox();
            this.chkTongGiaTri = new System.Windows.Forms.CheckBox();
            this.chkNguoiTao = new System.Windows.Forms.CheckBox();
            this.chkGhiChu = new System.Windows.Forms.CheckBox();
            this.chkTrangThai = new System.Windows.Forms.CheckBox();
            this.btnDong = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // lblTieuDe
            // 
            this.lblTieuDe.AutoSize = true;
            this.lblTieuDe.Font = new System.Drawing.Font("Segoe UI", 10.5F, System.Drawing.FontStyle.Bold);
            this.lblTieuDe.Location = new System.Drawing.Point(12, 9);
            this.lblTieuDe.Name = "lblTieuDe";
            this.lblTieuDe.Size = new System.Drawing.Size(126, 25);
            this.lblTieuDe.TabIndex = 0;
            this.lblTieuDe.Text = "Tùy chọn cột";
            // 
            // chkMaPhieu
            // 
            this.chkMaPhieu.AutoSize = true;
            this.chkMaPhieu.Checked = true;
            this.chkMaPhieu.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkMaPhieu.Location = new System.Drawing.Point(16, 42);
            this.chkMaPhieu.Name = "chkMaPhieu";
            this.chkMaPhieu.Size = new System.Drawing.Size(126, 25);
            this.chkMaPhieu.TabIndex = 1;
            this.chkMaPhieu.Text = "Mã chuyển kho";
            this.chkMaPhieu.UseVisualStyleBackColor = true;
            // 
            // chkNgayLap
            // 
            this.chkNgayLap.AutoSize = true;
            this.chkNgayLap.Checked = true;
            this.chkNgayLap.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkNgayLap.Location = new System.Drawing.Point(16, 73);
            this.chkNgayLap.Name = "chkNgayLap";
            this.chkNgayLap.Size = new System.Drawing.Size(95, 25);
            this.chkNgayLap.TabIndex = 2;
            this.chkNgayLap.Text = "Thời gian";
            this.chkNgayLap.UseVisualStyleBackColor = true;
            // 
            // chkKhoNguon
            // 
            this.chkKhoNguon.AutoSize = true;
            this.chkKhoNguon.Checked = true;
            this.chkKhoNguon.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkKhoNguon.Location = new System.Drawing.Point(16, 104);
            this.chkKhoNguon.Name = "chkKhoNguon";
            this.chkKhoNguon.Size = new System.Drawing.Size(83, 25);
            this.chkKhoNguon.TabIndex = 3;
            this.chkKhoNguon.Text = "Kho gửi";
            this.chkKhoNguon.UseVisualStyleBackColor = true;
            // 
            // chkKhoDich
            // 
            this.chkKhoDich.AutoSize = true;
            this.chkKhoDich.Checked = true;
            this.chkKhoDich.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkKhoDich.Location = new System.Drawing.Point(16, 135);
            this.chkKhoDich.Name = "chkKhoDich";
            this.chkKhoDich.Size = new System.Drawing.Size(96, 25);
            this.chkKhoDich.TabIndex = 4;
            this.chkKhoDich.Text = "Kho nhận";
            this.chkKhoDich.UseVisualStyleBackColor = true;
            // 
            // chkTongSlChuyen
            // 
            this.chkTongSlChuyen.AutoSize = true;
            this.chkTongSlChuyen.Checked = true;
            this.chkTongSlChuyen.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkTongSlChuyen.Location = new System.Drawing.Point(16, 166);
            this.chkTongSlChuyen.Name = "chkTongSlChuyen";
            this.chkTongSlChuyen.Size = new System.Drawing.Size(142, 25);
            this.chkTongSlChuyen.TabIndex = 5;
            this.chkTongSlChuyen.Text = "Tổng SL chuyển";
            this.chkTongSlChuyen.UseVisualStyleBackColor = true;
            // 
            // chkTongSlNhan
            // 
            this.chkTongSlNhan.AutoSize = true;
            this.chkTongSlNhan.Checked = true;
            this.chkTongSlNhan.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkTongSlNhan.Location = new System.Drawing.Point(16, 197);
            this.chkTongSlNhan.Name = "chkTongSlNhan";
            this.chkTongSlNhan.Size = new System.Drawing.Size(127, 25);
            this.chkTongSlNhan.TabIndex = 6;
            this.chkTongSlNhan.Text = "Tổng SL nhận";
            this.chkTongSlNhan.UseVisualStyleBackColor = true;
            // 
            // chkTongGiaTri
            // 
            this.chkTongGiaTri.AutoSize = true;
            this.chkTongGiaTri.Checked = true;
            this.chkTongGiaTri.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkTongGiaTri.Location = new System.Drawing.Point(16, 228);
            this.chkTongGiaTri.Name = "chkTongGiaTri";
            this.chkTongGiaTri.Size = new System.Drawing.Size(117, 25);
            this.chkTongGiaTri.TabIndex = 7;
            this.chkTongGiaTri.Text = "Tổng giá trị";
            this.chkTongGiaTri.UseVisualStyleBackColor = true;
            // 
            // chkNguoiTao
            // 
            this.chkNguoiTao.AutoSize = true;
            this.chkNguoiTao.Checked = true;
            this.chkNguoiTao.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkNguoiTao.Location = new System.Drawing.Point(16, 259);
            this.chkNguoiTao.Name = "chkNguoiTao";
            this.chkNguoiTao.Size = new System.Drawing.Size(103, 25);
            this.chkNguoiTao.TabIndex = 8;
            this.chkNguoiTao.Text = "Người tạo";
            this.chkNguoiTao.UseVisualStyleBackColor = true;
            // 
            // chkGhiChu
            // 
            this.chkGhiChu.AutoSize = true;
            this.chkGhiChu.Checked = true;
            this.chkGhiChu.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkGhiChu.Location = new System.Drawing.Point(16, 290);
            this.chkGhiChu.Name = "chkGhiChu";
            this.chkGhiChu.Size = new System.Drawing.Size(84, 25);
            this.chkGhiChu.TabIndex = 9;
            this.chkGhiChu.Text = "Ghi chú";
            this.chkGhiChu.UseVisualStyleBackColor = true;
            // 
            // chkTrangThai
            // 
            this.chkTrangThai.AutoSize = true;
            this.chkTrangThai.Checked = true;
            this.chkTrangThai.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkTrangThai.Location = new System.Drawing.Point(16, 321);
            this.chkTrangThai.Name = "chkTrangThai";
            this.chkTrangThai.Size = new System.Drawing.Size(100, 25);
            this.chkTrangThai.TabIndex = 10;
            this.chkTrangThai.Text = "Trạng thái";
            this.chkTrangThai.UseVisualStyleBackColor = true;
            // 
            // btnDong
            // 
            this.btnDong.Location = new System.Drawing.Point(16, 357);
            this.btnDong.Name = "btnDong";
            this.btnDong.Size = new System.Drawing.Size(150, 30);
            this.btnDong.TabIndex = 11;
            this.btnDong.Text = "Đóng";
            this.btnDong.UseVisualStyleBackColor = true;
            this.btnDong.Click += new System.EventHandler(this.btnDong_Click);
            // 
            // frmHienCotChuyenKho
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(185, 400);
            this.Controls.Add(this.btnDong);
            this.Controls.Add(this.chkTrangThai);
            this.Controls.Add(this.chkGhiChu);
            this.Controls.Add(this.chkNguoiTao);
            this.Controls.Add(this.chkTongGiaTri);
            this.Controls.Add(this.chkTongSlNhan);
            this.Controls.Add(this.chkTongSlChuyen);
            this.Controls.Add(this.chkKhoDich);
            this.Controls.Add(this.chkKhoNguon);
            this.Controls.Add(this.chkNgayLap);
            this.Controls.Add(this.chkMaPhieu);
            this.Controls.Add(this.lblTieuDe);
            this.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Name = "frmHienCotChuyenKho";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "Tùy chọn cột";
            this.Deactivate += new System.EventHandler(this.frmHienCotChuyenKho_Deactivate);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblTieuDe;
        private System.Windows.Forms.CheckBox chkMaPhieu;
        private System.Windows.Forms.CheckBox chkNgayLap;
        private System.Windows.Forms.CheckBox chkKhoNguon;
        private System.Windows.Forms.CheckBox chkKhoDich;
        private System.Windows.Forms.CheckBox chkTongSlChuyen;
        private System.Windows.Forms.CheckBox chkTongSlNhan;
        private System.Windows.Forms.CheckBox chkTongGiaTri;
        private System.Windows.Forms.CheckBox chkNguoiTao;
        private System.Windows.Forms.CheckBox chkGhiChu;
        private System.Windows.Forms.CheckBox chkTrangThai;
        private System.Windows.Forms.Button btnDong;
    }
}
