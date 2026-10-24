namespace BTL_LTTQ_2.GUI.Forms.DonViTinh
{
    partial class frmHienCotDvt
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
            this.chkMaDVT = new System.Windows.Forms.CheckBox();
            this.chkTenDVT = new System.Windows.Forms.CheckBox();
            this.chkSoLuongHangHoa = new System.Windows.Forms.CheckBox();
            this.chkTrangThai = new System.Windows.Forms.CheckBox();
            this.chkHangHoaTieuBieu = new System.Windows.Forms.CheckBox();
            this.pnlMain.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlMain
            // 
            this.pnlMain.BackColor = System.Drawing.Color.White;
            this.pnlMain.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlMain.Controls.Add(this.chkHangHoaTieuBieu);
            this.pnlMain.Controls.Add(this.chkTrangThai);
            this.pnlMain.Controls.Add(this.chkSoLuongHangHoa);
            this.pnlMain.Controls.Add(this.chkTenDVT);
            this.pnlMain.Controls.Add(this.chkMaDVT);
            this.pnlMain.Controls.Add(this.lblTieuDe);
            this.pnlMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlMain.Location = new System.Drawing.Point(0, 0);
            this.pnlMain.Name = "pnlMain";
            this.pnlMain.Padding = new System.Windows.Forms.Padding(12);
            this.pnlMain.Size = new System.Drawing.Size(230, 220);
            this.pnlMain.TabIndex = 0;
            // 
            // lblTieuDe
            // 
            this.lblTieuDe.AutoSize = true;
            this.lblTieuDe.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold);
            this.lblTieuDe.Location = new System.Drawing.Point(12, 10);
            this.lblTieuDe.Name = "lblTieuDe";
            this.lblTieuDe.Size = new System.Drawing.Size(142, 21);
            this.lblTieuDe.TabIndex = 0;
            this.lblTieuDe.Text = "Tùy chọn cột hiển thị";
            // 
            // chkMaDVT
            // 
            this.chkMaDVT.AutoSize = true;
            this.chkMaDVT.Location = new System.Drawing.Point(16, 42);
            this.chkMaDVT.Name = "chkMaDVT";
            this.chkMaDVT.Size = new System.Drawing.Size(126, 25);
            this.chkMaDVT.TabIndex = 1;
            this.chkMaDVT.Text = "Mã đơn vị tính";
            this.chkMaDVT.UseVisualStyleBackColor = true;
            // 
            // chkTenDVT
            // 
            this.chkTenDVT.AutoSize = true;
            this.chkTenDVT.Location = new System.Drawing.Point(16, 73);
            this.chkTenDVT.Name = "chkTenDVT";
            this.chkTenDVT.Size = new System.Drawing.Size(128, 25);
            this.chkTenDVT.TabIndex = 2;
            this.chkTenDVT.Text = "Tên đơn vị tính";
            this.chkTenDVT.UseVisualStyleBackColor = true;
            // 
            // chkSoLuongHangHoa
            // 
            this.chkSoLuongHangHoa.AutoSize = true;
            this.chkSoLuongHangHoa.Location = new System.Drawing.Point(16, 104);
            this.chkSoLuongHangHoa.Name = "chkSoLuongHangHoa";
            this.chkSoLuongHangHoa.Size = new System.Drawing.Size(161, 25);
            this.chkSoLuongHangHoa.TabIndex = 3;
            this.chkSoLuongHangHoa.Text = "Số lượng hàng hóa";
            this.chkSoLuongHangHoa.UseVisualStyleBackColor = true;
            // 
            // chkTrangThai
            // 
            this.chkTrangThai.AutoSize = true;
            this.chkTrangThai.Location = new System.Drawing.Point(16, 135);
            this.chkTrangThai.Name = "chkTrangThai";
            this.chkTrangThai.Size = new System.Drawing.Size(155, 25);
            this.chkTrangThai.TabIndex = 4;
            this.chkTrangThai.Text = "Trạng thái sử dụng";
            this.chkTrangThai.UseVisualStyleBackColor = true;
            // 
            // chkHangHoaTieuBieu
            // 
            this.chkHangHoaTieuBieu.AutoSize = true;
            this.chkHangHoaTieuBieu.Location = new System.Drawing.Point(16, 166);
            this.chkHangHoaTieuBieu.Name = "chkHangHoaTieuBieu";
            this.chkHangHoaTieuBieu.Size = new System.Drawing.Size(165, 25);
            this.chkHangHoaTieuBieu.TabIndex = 5;
            this.chkHangHoaTieuBieu.Text = "Mặt hàng tiêu biểu";
            this.chkHangHoaTieuBieu.UseVisualStyleBackColor = true;
            // 
            // frmHienCotDvt
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(230, 220);
            this.Controls.Add(this.pnlMain);
            this.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "frmHienCotDvt";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "frmHienCotDvt";
            this.Deactivate += new System.EventHandler(this.frmHienCotDvt_Deactivate);
            this.pnlMain.ResumeLayout(false);
            this.pnlMain.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pnlMain;
        private System.Windows.Forms.Label lblTieuDe;
        private System.Windows.Forms.CheckBox chkMaDVT;
        private System.Windows.Forms.CheckBox chkTenDVT;
        private System.Windows.Forms.CheckBox chkSoLuongHangHoa;
        private System.Windows.Forms.CheckBox chkTrangThai;
        private System.Windows.Forms.CheckBox chkHangHoaTieuBieu;
    }
}
