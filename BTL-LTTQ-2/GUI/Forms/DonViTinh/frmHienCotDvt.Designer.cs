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
            this.lblHienThiCot = new System.Windows.Forms.Label();
            this.chkMaDVT = new System.Windows.Forms.CheckBox();
            this.chkTenDVT = new System.Windows.Forms.CheckBox();
            this.chkSoLuongHangHoa = new System.Windows.Forms.CheckBox();
            this.chkTrangThai = new System.Windows.Forms.CheckBox();
            this.chkHangHoaTieuBieu = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // lblHienThiCot
            // 
            this.lblHienThiCot.AutoSize = true;
            this.lblHienThiCot.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblHienThiCot.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(41)))), ((int)(((byte)(59)))));
            this.lblHienThiCot.Location = new System.Drawing.Point(20, 16);
            this.lblHienThiCot.Name = "lblHienThiCot";
            this.lblHienThiCot.Size = new System.Drawing.Size(86, 19);
            this.lblHienThiCot.TabIndex = 0;
            this.lblHienThiCot.Text = "Hiển thị cột";
            // 
            // chkMaDVT
            // 
            this.chkMaDVT.AutoSize = true;
            this.chkMaDVT.Checked = true;
            this.chkMaDVT.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkMaDVT.Cursor = System.Windows.Forms.Cursors.Hand;
            this.chkMaDVT.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkMaDVT.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(65)))), ((int)(((byte)(85)))));
            this.chkMaDVT.Location = new System.Drawing.Point(24, 48);
            this.chkMaDVT.Name = "chkMaDVT";
            this.chkMaDVT.Size = new System.Drawing.Size(107, 21);
            this.chkMaDVT.TabIndex = 1;
            this.chkMaDVT.Text = "Mã đơn vị tính";
            this.chkMaDVT.UseVisualStyleBackColor = true;
            // 
            // chkTenDVT
            // 
            this.chkTenDVT.AutoSize = true;
            this.chkTenDVT.Checked = true;
            this.chkTenDVT.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkTenDVT.Cursor = System.Windows.Forms.Cursors.Hand;
            this.chkTenDVT.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkTenDVT.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(65)))), ((int)(((byte)(85)))));
            this.chkTenDVT.Location = new System.Drawing.Point(24, 76);
            this.chkTenDVT.Name = "chkTenDVT";
            this.chkTenDVT.Size = new System.Drawing.Size(108, 21);
            this.chkTenDVT.TabIndex = 2;
            this.chkTenDVT.Text = "Tên đơn vị tính";
            this.chkTenDVT.UseVisualStyleBackColor = true;
            // 
            // chkSoLuongHangHoa
            // 
            this.chkSoLuongHangHoa.AutoSize = true;
            this.chkSoLuongHangHoa.Checked = true;
            this.chkSoLuongHangHoa.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkSoLuongHangHoa.Cursor = System.Windows.Forms.Cursors.Hand;
            this.chkSoLuongHangHoa.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkSoLuongHangHoa.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(65)))), ((int)(((byte)(85)))));
            this.chkSoLuongHangHoa.Location = new System.Drawing.Point(24, 104);
            this.chkSoLuongHangHoa.Name = "chkSoLuongHangHoa";
            this.chkSoLuongHangHoa.Size = new System.Drawing.Size(139, 21);
            this.chkSoLuongHangHoa.TabIndex = 3;
            this.chkSoLuongHangHoa.Text = "Số lượng hàng hóa";
            this.chkSoLuongHangHoa.UseVisualStyleBackColor = true;
            // 
            // chkTrangThai
            // 
            this.chkTrangThai.AutoSize = true;
            this.chkTrangThai.Checked = true;
            this.chkTrangThai.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkTrangThai.Cursor = System.Windows.Forms.Cursors.Hand;
            this.chkTrangThai.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkTrangThai.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(65)))), ((int)(((byte)(85)))));
            this.chkTrangThai.Location = new System.Drawing.Point(24, 132);
            this.chkTrangThai.Name = "chkTrangThai";
            this.chkTrangThai.Size = new System.Drawing.Size(136, 21);
            this.chkTrangThai.TabIndex = 4;
            this.chkTrangThai.Text = "Trạng thái sử dụng";
            this.chkTrangThai.UseVisualStyleBackColor = true;
            // 
            // chkHangHoaTieuBieu
            // 
            this.chkHangHoaTieuBieu.AutoSize = true;
            this.chkHangHoaTieuBieu.Checked = true;
            this.chkHangHoaTieuBieu.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkHangHoaTieuBieu.Cursor = System.Windows.Forms.Cursors.Hand;
            this.chkHangHoaTieuBieu.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkHangHoaTieuBieu.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(65)))), ((int)(((byte)(85)))));
            this.chkHangHoaTieuBieu.Location = new System.Drawing.Point(24, 160);
            this.chkHangHoaTieuBieu.Name = "chkHangHoaTieuBieu";
            this.chkHangHoaTieuBieu.Size = new System.Drawing.Size(138, 21);
            this.chkHangHoaTieuBieu.TabIndex = 5;
            this.chkHangHoaTieuBieu.Text = "Mặt hàng tiêu biểu";
            this.chkHangHoaTieuBieu.UseVisualStyleBackColor = true;
            // 
            // frmHienCotDvt
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(220, 200);
            this.Controls.Add(this.chkHangHoaTieuBieu);
            this.Controls.Add(this.chkTrangThai);
            this.Controls.Add(this.chkSoLuongHangHoa);
            this.Controls.Add(this.chkTenDVT);
            this.Controls.Add(this.chkMaDVT);
            this.Controls.Add(this.lblHienThiCot);
            this.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.KeyPreview = true;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmHienCotDvt";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        #endregion

        private System.Windows.Forms.Label lblHienThiCot;
        private System.Windows.Forms.CheckBox chkMaDVT;
        private System.Windows.Forms.CheckBox chkTenDVT;
        private System.Windows.Forms.CheckBox chkSoLuongHangHoa;
        private System.Windows.Forms.CheckBox chkTrangThai;
        private System.Windows.Forms.CheckBox chkHangHoaTieuBieu;
    }
}