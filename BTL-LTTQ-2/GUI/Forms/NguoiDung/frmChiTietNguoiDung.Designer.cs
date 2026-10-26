namespace BTL_LTTQ_2.GUI.Forms.NguoiDung
{
    partial class frmChiTietNguoiDung
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
            this.pnlTop = new System.Windows.Forms.Panel();
            this.grpTieuDe = new System.Windows.Forms.GroupBox();
            this.lblTieuDe = new System.Windows.Forms.Label();
            this.pnlThongTinCoBan = new System.Windows.Forms.Panel();
            this.cboTrangThai = new System.Windows.Forms.ComboBox();
            this.lblTrangThai = new System.Windows.Forms.Label();
            this.cboVaiTro = new System.Windows.Forms.ComboBox();
            this.lblVaiTro = new System.Windows.Forms.Label();
            this.chkHienMatKhau = new System.Windows.Forms.CheckBox();
            this.lblMatKhau = new System.Windows.Forms.Label();
            this.txtMatKhau = new System.Windows.Forms.TextBox();
            this.lblTenDangNhap = new System.Windows.Forms.Label();
            this.txtTenDangNhap = new System.Windows.Forms.TextBox();
            this.grpPhanQuyenKho = new System.Windows.Forms.GroupBox();
            this.clbDanhSachKho = new System.Windows.Forms.CheckedListBox();
            this.grpThongTinNhanSu = new System.Windows.Forms.GroupBox();
            this.lblGhiChu = new System.Windows.Forms.Label();
            this.txtGhiChu = new System.Windows.Forms.TextBox();
            this.lblDiaChi = new System.Windows.Forms.Label();
            this.txtDiaChi = new System.Windows.Forms.TextBox();
            this.lblEmail = new System.Windows.Forms.Label();
            this.txtEmail = new System.Windows.Forms.TextBox();
            this.lblDienThoai = new System.Windows.Forms.Label();
            this.txtDienThoai = new System.Windows.Forms.TextBox();
            this.lblHoTen = new System.Windows.Forms.Label();
            this.txtHoTen = new System.Windows.Forms.TextBox();
            this.pnlBottom = new System.Windows.Forms.Panel();
            this.grpThaoTac = new System.Windows.Forms.GroupBox();
            this.btnLuu = new System.Windows.Forms.Button();
            this.btnKhoaTaiKhoan = new System.Windows.Forms.Button();
            this.btnXoa = new System.Windows.Forms.Button();
            this.btnBoQua = new System.Windows.Forms.Button();
            this.pnlTop.SuspendLayout();
            this.grpTieuDe.SuspendLayout();
            this.pnlThongTinCoBan.SuspendLayout();
            this.grpPhanQuyenKho.SuspendLayout();
            this.grpThongTinNhanSu.SuspendLayout();
            this.pnlBottom.SuspendLayout();
            this.grpThaoTac.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlTop
            // 
            this.pnlTop.Controls.Add(this.grpTieuDe);
            this.pnlTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlTop.Location = new System.Drawing.Point(0, 0);
            this.pnlTop.Name = "pnlTop";
            this.pnlTop.Size = new System.Drawing.Size(1182, 63);
            this.pnlTop.TabIndex = 0;
            // 
            // grpTieuDe
            // 
            this.grpTieuDe.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.grpTieuDe.Controls.Add(this.lblTieuDe);
            this.grpTieuDe.Location = new System.Drawing.Point(0, 3);
            this.grpTieuDe.Name = "grpTieuDe";
            this.grpTieuDe.Size = new System.Drawing.Size(1182, 57);
            this.grpTieuDe.TabIndex = 0;
            this.grpTieuDe.TabStop = false;
            // 
            // lblTieuDe
            // 
            this.lblTieuDe.AutoSize = true;
            this.lblTieuDe.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.lblTieuDe.Location = new System.Drawing.Point(16, 18);
            this.lblTieuDe.Name = "lblTieuDe";
            this.lblTieuDe.Size = new System.Drawing.Size(197, 28);
            this.lblTieuDe.TabIndex = 0;
            this.lblTieuDe.Text = "Chi tiết người dùng";
            // 
            // pnlThongTinCoBan
            // 
            this.pnlThongTinCoBan.AutoScroll = true;
            this.pnlThongTinCoBan.Controls.Add(this.cboTrangThai);
            this.pnlThongTinCoBan.Controls.Add(this.lblTrangThai);
            this.pnlThongTinCoBan.Controls.Add(this.cboVaiTro);
            this.pnlThongTinCoBan.Controls.Add(this.lblVaiTro);
            this.pnlThongTinCoBan.Controls.Add(this.chkHienMatKhau);
            this.pnlThongTinCoBan.Controls.Add(this.lblMatKhau);
            this.pnlThongTinCoBan.Controls.Add(this.txtMatKhau);
            this.pnlThongTinCoBan.Controls.Add(this.lblTenDangNhap);
            this.pnlThongTinCoBan.Controls.Add(this.txtTenDangNhap);
            this.pnlThongTinCoBan.Controls.Add(this.grpPhanQuyenKho);
            this.pnlThongTinCoBan.Controls.Add(this.grpThongTinNhanSu);
            this.pnlThongTinCoBan.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlThongTinCoBan.Location = new System.Drawing.Point(0, 63);
            this.pnlThongTinCoBan.Name = "pnlThongTinCoBan";
            this.pnlThongTinCoBan.Size = new System.Drawing.Size(1182, 680);
            this.pnlThongTinCoBan.TabIndex = 1;
            // 
            // cboTrangThai
            // 
            this.cboTrangThai.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboTrangThai.FormattingEnabled = true;
            this.cboTrangThai.Items.AddRange(new object[] {
            "Đang hoạt động",
            "Ngừng hoạt động"});
            this.cboTrangThai.Location = new System.Drawing.Point(736, 98);
            this.cboTrangThai.Name = "cboTrangThai";
            this.cboTrangThai.Size = new System.Drawing.Size(320, 29);
            this.cboTrangThai.TabIndex = 17;
            // 
            // lblTrangThai
            // 
            this.lblTrangThai.AutoSize = true;
            this.lblTrangThai.Location = new System.Drawing.Point(629, 98);
            this.lblTrangThai.Name = "lblTrangThai";
            this.lblTrangThai.Size = new System.Drawing.Size(82, 21);
            this.lblTrangThai.TabIndex = 16;
            this.lblTrangThai.Text = "Trạng thái:";
            // 
            // cboVaiTro
            // 
            this.cboVaiTro.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboVaiTro.FormattingEnabled = true;
            this.cboVaiTro.Items.AddRange(new object[] {
            "Quản lý",
            "Thủ kho",
            "Nhân viên kho"});
            this.cboVaiTro.Location = new System.Drawing.Point(736, 41);
            this.cboVaiTro.Name = "cboVaiTro";
            this.cboVaiTro.Size = new System.Drawing.Size(320, 29);
            this.cboVaiTro.TabIndex = 15;
            this.cboVaiTro.SelectedIndexChanged += new System.EventHandler(this.cboVaiTro_SelectedIndexChanged);
            // 
            // lblVaiTro
            // 
            this.lblVaiTro.AutoSize = true;
            this.lblVaiTro.Location = new System.Drawing.Point(629, 41);
            this.lblVaiTro.Name = "lblVaiTro";
            this.lblVaiTro.Size = new System.Drawing.Size(79, 21);
            this.lblVaiTro.TabIndex = 14;
            this.lblVaiTro.Text = "Vai trò (*):";
            // 
            // chkHienMatKhau
            // 
            this.chkHienMatKhau.AutoSize = true;
            this.chkHienMatKhau.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            this.chkHienMatKhau.Location = new System.Drawing.Point(219, 128);
            this.chkHienMatKhau.Name = "chkHienMatKhau";
            this.chkHienMatKhau.Size = new System.Drawing.Size(133, 25);
            this.chkHienMatKhau.TabIndex = 13;
            this.chkHienMatKhau.Text = "Hiện mật khẩu";
            this.chkHienMatKhau.UseVisualStyleBackColor = true;
            this.chkHienMatKhau.CheckedChanged += new System.EventHandler(this.chkHienMatKhau_CheckedChanged);
            // 
            // lblMatKhau
            // 
            this.lblMatKhau.AutoSize = true;
            this.lblMatKhau.Location = new System.Drawing.Point(62, 98);
            this.lblMatKhau.Name = "lblMatKhau";
            this.lblMatKhau.Size = new System.Drawing.Size(110, 21);
            this.lblMatKhau.TabIndex = 11;
            this.lblMatKhau.Text = "Mật khẩu mới:";
            // 
            // txtMatKhau
            // 
            this.txtMatKhau.Location = new System.Drawing.Point(219, 95);
            this.txtMatKhau.Name = "txtMatKhau";
            this.txtMatKhau.Size = new System.Drawing.Size(320, 29);
            this.txtMatKhau.TabIndex = 12;
            this.txtMatKhau.UseSystemPasswordChar = true;
            // 
            // lblTenDangNhap
            // 
            this.lblTenDangNhap.AutoSize = true;
            this.lblTenDangNhap.Location = new System.Drawing.Point(62, 41);
            this.lblTenDangNhap.Name = "lblTenDangNhap";
            this.lblTenDangNhap.Size = new System.Drawing.Size(135, 21);
            this.lblTenDangNhap.TabIndex = 9;
            this.lblTenDangNhap.Text = "Tên đăng nhập (*):";
            // 
            // txtTenDangNhap
            // 
            this.txtTenDangNhap.Location = new System.Drawing.Point(219, 38);
            this.txtTenDangNhap.Name = "txtTenDangNhap";
            this.txtTenDangNhap.Size = new System.Drawing.Size(320, 29);
            this.txtTenDangNhap.TabIndex = 10;
            // 
            // grpPhanQuyenKho
            // 
            this.grpPhanQuyenKho.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.grpPhanQuyenKho.Controls.Add(this.clbDanhSachKho);
            this.grpPhanQuyenKho.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            this.grpPhanQuyenKho.Location = new System.Drawing.Point(32, 455);
            this.grpPhanQuyenKho.Name = "grpPhanQuyenKho";
            this.grpPhanQuyenKho.Size = new System.Drawing.Size(1110, 207);
            this.grpPhanQuyenKho.TabIndex = 2;
            this.grpPhanQuyenKho.TabStop = false;
            this.grpPhanQuyenKho.Text = "Phân quyền quản lý kho";
            // 
            // clbDanhSachKho
            // 
            this.clbDanhSachKho.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.clbDanhSachKho.CheckOnClick = true;
            this.clbDanhSachKho.FormattingEnabled = true;
            this.clbDanhSachKho.Location = new System.Drawing.Point(48, 38);
            this.clbDanhSachKho.Name = "clbDanhSachKho";
            this.clbDanhSachKho.Size = new System.Drawing.Size(1014, 148);
            this.clbDanhSachKho.TabIndex = 1;
            // 
            // grpThongTinNhanSu
            // 
            this.grpThongTinNhanSu.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.grpThongTinNhanSu.Controls.Add(this.lblGhiChu);
            this.grpThongTinNhanSu.Controls.Add(this.txtGhiChu);
            this.grpThongTinNhanSu.Controls.Add(this.lblDiaChi);
            this.grpThongTinNhanSu.Controls.Add(this.txtDiaChi);
            this.grpThongTinNhanSu.Controls.Add(this.lblEmail);
            this.grpThongTinNhanSu.Controls.Add(this.txtEmail);
            this.grpThongTinNhanSu.Controls.Add(this.lblDienThoai);
            this.grpThongTinNhanSu.Controls.Add(this.txtDienThoai);
            this.grpThongTinNhanSu.Controls.Add(this.lblHoTen);
            this.grpThongTinNhanSu.Controls.Add(this.txtHoTen);
            this.grpThongTinNhanSu.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            this.grpThongTinNhanSu.Location = new System.Drawing.Point(32, 171);
            this.grpThongTinNhanSu.Name = "grpThongTinNhanSu";
            this.grpThongTinNhanSu.Size = new System.Drawing.Size(1109, 278);
            this.grpThongTinNhanSu.TabIndex = 1;
            this.grpThongTinNhanSu.TabStop = false;
            this.grpThongTinNhanSu.Text = "Thông tin nhân sự";
            // 
            // lblGhiChu
            // 
            this.lblGhiChu.AutoSize = true;
            this.lblGhiChu.Location = new System.Drawing.Point(473, 104);
            this.lblGhiChu.Name = "lblGhiChu";
            this.lblGhiChu.Size = new System.Drawing.Size(66, 21);
            this.lblGhiChu.TabIndex = 8;
            this.lblGhiChu.Text = "Ghi chú:";
            // 
            // txtGhiChu
            // 
            this.txtGhiChu.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtGhiChu.Location = new System.Drawing.Point(556, 107);
            this.txtGhiChu.Multiline = true;
            this.txtGhiChu.Name = "txtGhiChu";
            this.txtGhiChu.Size = new System.Drawing.Size(506, 143);
            this.txtGhiChu.TabIndex = 9;
            // 
            // lblDiaChi
            // 
            this.lblDiaChi.AutoSize = true;
            this.lblDiaChi.Location = new System.Drawing.Point(44, 160);
            this.lblDiaChi.Name = "lblDiaChi";
            this.lblDiaChi.Size = new System.Drawing.Size(60, 21);
            this.lblDiaChi.TabIndex = 6;
            this.lblDiaChi.Text = "Địa chỉ:";
            // 
            // txtDiaChi
            // 
            this.txtDiaChi.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtDiaChi.Location = new System.Drawing.Point(154, 157);
            this.txtDiaChi.Multiline = true;
            this.txtDiaChi.Name = "txtDiaChi";
            this.txtDiaChi.Size = new System.Drawing.Size(284, 93);
            this.txtDiaChi.TabIndex = 7;
            // 
            // lblEmail
            // 
            this.lblEmail.AutoSize = true;
            this.lblEmail.Location = new System.Drawing.Point(473, 44);
            this.lblEmail.Name = "lblEmail";
            this.lblEmail.Size = new System.Drawing.Size(51, 21);
            this.lblEmail.TabIndex = 4;
            this.lblEmail.Text = "Email:";
            // 
            // txtEmail
            // 
            this.txtEmail.Location = new System.Drawing.Point(556, 41);
            this.txtEmail.Name = "txtEmail";
            this.txtEmail.Size = new System.Drawing.Size(506, 29);
            this.txtEmail.TabIndex = 5;
            // 
            // lblDienThoai
            // 
            this.lblDienThoai.AutoSize = true;
            this.lblDienThoai.Location = new System.Drawing.Point(44, 102);
            this.lblDienThoai.Name = "lblDienThoai";
            this.lblDienThoai.Size = new System.Drawing.Size(104, 21);
            this.lblDienThoai.TabIndex = 2;
            this.lblDienThoai.Text = "Số điện thoại:";
            // 
            // txtDienThoai
            // 
            this.txtDienThoai.Location = new System.Drawing.Point(154, 102);
            this.txtDienThoai.Name = "txtDienThoai";
            this.txtDienThoai.Size = new System.Drawing.Size(284, 29);
            this.txtDienThoai.TabIndex = 3;
            // 
            // lblHoTen
            // 
            this.lblHoTen.AutoSize = true;
            this.lblHoTen.Location = new System.Drawing.Point(44, 44);
            this.lblHoTen.Name = "lblHoTen";
            this.lblHoTen.Size = new System.Drawing.Size(100, 21);
            this.lblHoTen.TabIndex = 0;
            this.lblHoTen.Text = "Họ và tên (*):";
            // 
            // txtHoTen
            // 
            this.txtHoTen.Location = new System.Drawing.Point(154, 41);
            this.txtHoTen.Name = "txtHoTen";
            this.txtHoTen.Size = new System.Drawing.Size(284, 29);
            this.txtHoTen.TabIndex = 1;
            // 
            // pnlBottom
            // 
            this.pnlBottom.Controls.Add(this.grpThaoTac);
            this.pnlBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnlBottom.Location = new System.Drawing.Point(0, 743);
            this.pnlBottom.Name = "pnlBottom";
            this.pnlBottom.Size = new System.Drawing.Size(1182, 60);
            this.pnlBottom.TabIndex = 2;
            // 
            // grpThaoTac
            // 
            this.grpThaoTac.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.grpThaoTac.Controls.Add(this.btnLuu);
            this.grpThaoTac.Controls.Add(this.btnKhoaTaiKhoan);
            this.grpThaoTac.Controls.Add(this.btnXoa);
            this.grpThaoTac.Controls.Add(this.btnBoQua);
            this.grpThaoTac.Location = new System.Drawing.Point(0, 0);
            this.grpThaoTac.Name = "grpThaoTac";
            this.grpThaoTac.Size = new System.Drawing.Size(1182, 57);
            this.grpThaoTac.TabIndex = 0;
            this.grpThaoTac.TabStop = false;
            // 
            // btnLuu
            // 
            this.btnLuu.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnLuu.Location = new System.Drawing.Point(1031, 19);
            this.btnLuu.Name = "btnLuu";
            this.btnLuu.Size = new System.Drawing.Size(110, 32);
            this.btnLuu.TabIndex = 2;
            this.btnLuu.Text = "Sửa";
            this.btnLuu.UseVisualStyleBackColor = true;
            this.btnLuu.Click += new System.EventHandler(this.btnLuu_Click);
            // 
            // btnKhoaTaiKhoan
            // 
            this.btnKhoaTaiKhoan.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnKhoaTaiKhoan.Location = new System.Drawing.Point(32, 19);
            this.btnKhoaTaiKhoan.Name = "btnKhoaTaiKhoan";
            this.btnKhoaTaiKhoan.Size = new System.Drawing.Size(110, 32);
            this.btnKhoaTaiKhoan.TabIndex = 1;
            this.btnKhoaTaiKhoan.Text = "Khóa";
            this.btnKhoaTaiKhoan.UseVisualStyleBackColor = true;
            this.btnKhoaTaiKhoan.Click += new System.EventHandler(this.btnKhoaTaiKhoan_Click);
            // 
            // btnXoa
            // 
            this.btnXoa.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnXoa.Location = new System.Drawing.Point(159, 19);
            this.btnXoa.Name = "btnXoa";
            this.btnXoa.Size = new System.Drawing.Size(110, 32);
            this.btnXoa.TabIndex = 2;
            this.btnXoa.Text = "Xóa";
            this.btnXoa.UseVisualStyleBackColor = true;
            this.btnXoa.Click += new System.EventHandler(this.btnXoa_Click);
            // 
            // btnBoQua
            // 
            this.btnBoQua.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnBoQua.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnBoQua.Location = new System.Drawing.Point(903, 19);
            this.btnBoQua.Name = "btnBoQua";
            this.btnBoQua.Size = new System.Drawing.Size(110, 32);
            this.btnBoQua.TabIndex = 0;
            this.btnBoQua.Text = "Bỏ qua";
            this.btnBoQua.UseVisualStyleBackColor = true;
            this.btnBoQua.Click += new System.EventHandler(this.btnBoQua_Click);
            // 
            // frmChiTietNguoiDung
            // 
            this.AcceptButton = this.btnLuu;
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 21F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnBoQua;
            this.ClientSize = new System.Drawing.Size(1182, 803);
            this.Controls.Add(this.pnlThongTinCoBan);
            this.Controls.Add(this.pnlBottom);
            this.Controls.Add(this.pnlTop);
            this.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.MaximizeBox = false;
            this.Name = "frmChiTietNguoiDung";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Thông tin người dùng";
            this.Load += new System.EventHandler(this.frmChiTietNguoiDung_Load);
            this.pnlTop.ResumeLayout(false);
            this.grpTieuDe.ResumeLayout(false);
            this.grpTieuDe.PerformLayout();
            this.pnlThongTinCoBan.ResumeLayout(false);
            this.pnlThongTinCoBan.PerformLayout();
            this.grpPhanQuyenKho.ResumeLayout(false);
            this.grpThongTinNhanSu.ResumeLayout(false);
            this.grpThongTinNhanSu.PerformLayout();
            this.pnlBottom.ResumeLayout(false);
            this.grpThaoTac.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pnlTop;
        private System.Windows.Forms.GroupBox grpTieuDe;
        private System.Windows.Forms.Label lblTieuDe;
        private System.Windows.Forms.Panel pnlThongTinCoBan;
        private System.Windows.Forms.GroupBox grpThongTinNhanSu;
        private System.Windows.Forms.Label lblHoTen;
        private System.Windows.Forms.TextBox txtHoTen;
        private System.Windows.Forms.Label lblDienThoai;
        private System.Windows.Forms.TextBox txtDienThoai;
        private System.Windows.Forms.Label lblEmail;
        private System.Windows.Forms.TextBox txtEmail;
        private System.Windows.Forms.Label lblDiaChi;
        private System.Windows.Forms.TextBox txtDiaChi;
        private System.Windows.Forms.Label lblGhiChu;
        private System.Windows.Forms.TextBox txtGhiChu;
        private System.Windows.Forms.GroupBox grpPhanQuyenKho;
        private System.Windows.Forms.CheckedListBox clbDanhSachKho;
        private System.Windows.Forms.Panel pnlBottom;
        private System.Windows.Forms.GroupBox grpThaoTac;
        private System.Windows.Forms.Button btnKhoaTaiKhoan;
        private System.Windows.Forms.Button btnXoa;
        private System.Windows.Forms.Button btnBoQua;
        private System.Windows.Forms.Button btnLuu;
        private System.Windows.Forms.ComboBox cboTrangThai;
        private System.Windows.Forms.Label lblTrangThai;
        private System.Windows.Forms.ComboBox cboVaiTro;
        private System.Windows.Forms.Label lblVaiTro;
        private System.Windows.Forms.CheckBox chkHienMatKhau;
        private System.Windows.Forms.Label lblMatKhau;
        private System.Windows.Forms.TextBox txtMatKhau;
        private System.Windows.Forms.Label lblTenDangNhap;
        private System.Windows.Forms.TextBox txtTenDangNhap;
    }
}