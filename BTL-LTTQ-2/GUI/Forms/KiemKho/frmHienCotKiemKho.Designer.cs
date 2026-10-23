namespace BTL_LTTQ_2.GUI.Forms.KiemKho
{
    partial class frmHienCotKiemKho
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
            this.chkThoiGian = new System.Windows.Forms.CheckBox();
            this.chkTongThucTe = new System.Windows.Forms.CheckBox();
            this.chkTongChenhLech = new System.Windows.Forms.CheckBox();
            this.chkSlLechTang = new System.Windows.Forms.CheckBox();
            this.chkSlLechGiam = new System.Windows.Forms.CheckBox();
            this.chkNgayCanBang = new System.Windows.Forms.CheckBox();
            this.chkSlThucTe = new System.Windows.Forms.CheckBox();
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
            this.chkMaPhieu.Text = "Mã kiểm kho";
            this.chkMaPhieu.UseVisualStyleBackColor = true;
            // 
            // chkThoiGian
            // 
            this.chkThoiGian.AutoSize = true;
            this.chkThoiGian.Checked = true;
            this.chkThoiGian.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkThoiGian.Location = new System.Drawing.Point(16, 73);
            this.chkThoiGian.Name = "chkThoiGian";
            this.chkThoiGian.Size = new System.Drawing.Size(95, 25);
            this.chkThoiGian.TabIndex = 2;
            this.chkThoiGian.Text = "Thời gian";
            this.chkThoiGian.UseVisualStyleBackColor = true;
            // 
            // chkTongThucTe
            // 
            this.chkTongThucTe.AutoSize = true;
            this.chkTongThucTe.Checked = true;
            this.chkTongThucTe.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkTongThucTe.Location = new System.Drawing.Point(16, 104);
            this.chkTongThucTe.Name = "chkTongThucTe";
            this.chkTongThucTe.Size = new System.Drawing.Size(123, 25);
            this.chkTongThucTe.TabIndex = 3;
            this.chkTongThucTe.Text = "Tổng thực tế";
            this.chkTongThucTe.UseVisualStyleBackColor = true;
            // 
            // chkTongChenhLech
            // 
            this.chkTongChenhLech.AutoSize = true;
            this.chkTongChenhLech.Checked = true;
            this.chkTongChenhLech.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkTongChenhLech.Location = new System.Drawing.Point(16, 135);
            this.chkTongChenhLech.Name = "chkTongChenhLech";
            this.chkTongChenhLech.Size = new System.Drawing.Size(144, 25);
            this.chkTongChenhLech.TabIndex = 4;
            this.chkTongChenhLech.Text = "Tổng chênh lệch";
            this.chkTongChenhLech.UseVisualStyleBackColor = true;
            // 
            // chkSlLechTang
            // 
            this.chkSlLechTang.AutoSize = true;
            this.chkSlLechTang.Checked = true;
            this.chkSlLechTang.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkSlLechTang.Location = new System.Drawing.Point(16, 166);
            this.chkSlLechTang.Name = "chkSlLechTang";
            this.chkSlLechTang.Size = new System.Drawing.Size(117, 25);
            this.chkSlLechTang.TabIndex = 5;
            this.chkSlLechTang.Text = "SL lệch tăng";
            this.chkSlLechTang.UseVisualStyleBackColor = true;
            // 
            // chkSlLechGiam
            // 
            this.chkSlLechGiam.AutoSize = true;
            this.chkSlLechGiam.Checked = true;
            this.chkSlLechGiam.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkSlLechGiam.Location = new System.Drawing.Point(16, 197);
            this.chkSlLechGiam.Name = "chkSlLechGiam";
            this.chkSlLechGiam.Size = new System.Drawing.Size(121, 25);
            this.chkSlLechGiam.TabIndex = 6;
            this.chkSlLechGiam.Text = "SL lệch giảm";
            this.chkSlLechGiam.UseVisualStyleBackColor = true;
            // 
            // chkNgayCanBang
            // 
            this.chkNgayCanBang.AutoSize = true;
            this.chkNgayCanBang.Checked = true;
            this.chkNgayCanBang.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkNgayCanBang.Location = new System.Drawing.Point(16, 228);
            this.chkNgayCanBang.Name = "chkNgayCanBang";
            this.chkNgayCanBang.Size = new System.Drawing.Size(135, 25);
            this.chkNgayCanBang.TabIndex = 7;
            this.chkNgayCanBang.Text = "Ngày cân bằng";
            this.chkNgayCanBang.UseVisualStyleBackColor = true;
            // 
            // chkSlThucTe
            // 
            this.chkSlThucTe.AutoSize = true;
            this.chkSlThucTe.Checked = true;
            this.chkSlThucTe.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkSlThucTe.Location = new System.Drawing.Point(16, 259);
            this.chkSlThucTe.Name = "chkSlThucTe";
            this.chkSlThucTe.Size = new System.Drawing.Size(107, 25);
            this.chkSlThucTe.TabIndex = 8;
            this.chkSlThucTe.Text = "SL thực tế";
            this.chkSlThucTe.UseVisualStyleBackColor = true;
            // 
            // chkGhiChu
            // 
            this.chkGhiChu.AutoSize = true;
            this.chkGhiChu.Checked = true;
            this.chkGhiChu.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkGhiChu.Location = new System.Drawing.Point(16, 290);
            this.chkGhiChu.Name = "chkGhiChu";
            this.chkGhiChu.Size = new System.Drawing.Size(85, 25);
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
            this.chkTrangThai.Size = new System.Drawing.Size(101, 25);
            this.chkTrangThai.TabIndex = 10;
            this.chkTrangThai.Text = "Trạng thái";
            this.chkTrangThai.UseVisualStyleBackColor = true;
            // 
            // btnDong
            // 
            this.btnDong.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnDong.Location = new System.Drawing.Point(100, 358);
            this.btnDong.Name = "btnDong";
            this.btnDong.Size = new System.Drawing.Size(85, 30);
            this.btnDong.TabIndex = 11;
            this.btnDong.Text = "Đóng";
            this.btnDong.UseVisualStyleBackColor = true;
            this.btnDong.Click += new System.EventHandler(this.btnDong_Click);
            // 
            // frmHienCotKiemKho
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 21F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(200, 400);
            this.Controls.Add(this.btnDong);
            this.Controls.Add(this.chkTrangThai);
            this.Controls.Add(this.chkGhiChu);
            this.Controls.Add(this.chkSlThucTe);
            this.Controls.Add(this.chkNgayCanBang);
            this.Controls.Add(this.chkSlLechGiam);
            this.Controls.Add(this.chkSlLechTang);
            this.Controls.Add(this.chkTongChenhLech);
            this.Controls.Add(this.chkTongThucTe);
            this.Controls.Add(this.chkThoiGian);
            this.Controls.Add(this.chkMaPhieu);
            this.Controls.Add(this.lblTieuDe);
            this.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmHienCotKiemKho";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "Hiển thị cột";
            this.Deactivate += new System.EventHandler(this.frmHienCotKiemKho_Deactivate);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblTieuDe;
        private System.Windows.Forms.CheckBox chkMaPhieu;
        private System.Windows.Forms.CheckBox chkThoiGian;
        private System.Windows.Forms.CheckBox chkTongThucTe;
        private System.Windows.Forms.CheckBox chkTongChenhLech;
        private System.Windows.Forms.CheckBox chkSlLechTang;
        private System.Windows.Forms.CheckBox chkSlLechGiam;
        private System.Windows.Forms.CheckBox chkNgayCanBang;
        private System.Windows.Forms.CheckBox chkSlThucTe;
        private System.Windows.Forms.CheckBox chkGhiChu;
        private System.Windows.Forms.CheckBox chkTrangThai;
        private System.Windows.Forms.Button btnDong;
    }
}
