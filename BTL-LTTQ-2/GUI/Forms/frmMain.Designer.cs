namespace BTL_LTTQ_2.GUI.Forms
{
    partial class frmMain
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
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.mnuHeThong = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuNguoiDung = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuDoiMatKhau = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuDangXuat = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.mnuThoat = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuQuanLyKho = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuKiemKho = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuChuyenKho = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.mnuDoiKho = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuDanhMuc = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuDanhSachHangHoa = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuMuaHang = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuNhaCungCap = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
            this.mnuNhapHang = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuTraHangNhap = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuBaoCao = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuBaoCaoTonKho = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuBaoCaoTheKho = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuTroGiup = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuHuongDan = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuThongTin = new System.Windows.Forms.ToolStripMenuItem();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.tssNguoiDung = new System.Windows.Forms.ToolStripStatusLabel();
            this.tssKho = new System.Windows.Forms.ToolStripStatusLabel();
            this.tssThoiGian = new System.Windows.Forms.ToolStripStatusLabel();
            this.menuStrip1.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            this.menuStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuHeThong,
            this.mnuQuanLyKho,
            this.mnuDanhMuc,
            this.mnuMuaHang,
            this.mnuBaoCao,
            this.mnuTroGiup});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(1008, 25);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // mnuHeThong
            // 
            this.mnuHeThong.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuNguoiDung,
            this.mnuDoiMatKhau,
            this.mnuDangXuat,
            this.toolStripSeparator1,
            this.mnuThoat});
            this.mnuHeThong.Name = "mnuHeThong";
            this.mnuHeThong.Size = new System.Drawing.Size(74, 21);
            this.mnuHeThong.Text = "&Hệ thống";
            // 
            // mnuNguoiDung
            // 
            this.mnuNguoiDung.Name = "mnuNguoiDung";
            this.mnuNguoiDung.Size = new System.Drawing.Size(153, 22);
            this.mnuNguoiDung.Text = "Người dùng";
            this.mnuNguoiDung.Click += new System.EventHandler(this.mnuNguoiDung_Click);
            // 
            // mnuDoiMatKhau
            // 
            this.mnuDoiMatKhau.Name = "mnuDoiMatKhau";
            this.mnuDoiMatKhau.Size = new System.Drawing.Size(153, 22);
            this.mnuDoiMatKhau.Text = "Đổi mật khẩu";
            this.mnuDoiMatKhau.Click += new System.EventHandler(this.mnuDoiMatKhau_Click);
            // 
            // mnuDangXuat
            // 
            this.mnuDangXuat.Name = "mnuDangXuat";
            this.mnuDangXuat.Size = new System.Drawing.Size(153, 22);
            this.mnuDangXuat.Text = "Đăng xuất";
            this.mnuDangXuat.Click += new System.EventHandler(this.menuDangXuat_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(150, 6);
            // 
            // mnuThoat
            // 
            this.mnuThoat.Name = "mnuThoat";
            this.mnuThoat.Size = new System.Drawing.Size(153, 22);
            this.mnuThoat.Text = "Thoát";
            this.mnuThoat.Click += new System.EventHandler(this.menuThoat_Click);
            // 
            // mnuQuanLyKho
            // 
            this.mnuQuanLyKho.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuKiemKho,
            this.mnuChuyenKho,
            this.toolStripSeparator2,
            this.mnuDoiKho});
            this.mnuQuanLyKho.Name = "mnuQuanLyKho";
            this.mnuQuanLyKho.Size = new System.Drawing.Size(76, 21);
            this.mnuQuanLyKho.Text = "&Kho hàng";
            // 
            // mnuKiemKho
            // 
            this.mnuKiemKho.Name = "mnuKiemKho";
            this.mnuKiemKho.Size = new System.Drawing.Size(143, 22);
            this.mnuKiemKho.Text = "Kiểm kho";
            this.mnuKiemKho.Click += new System.EventHandler(this.mnuKiemKho_Click);
            // 
            // mnuChuyenKho
            // 
            this.mnuChuyenKho.Name = "mnuChuyenKho";
            this.mnuChuyenKho.Size = new System.Drawing.Size(143, 22);
            this.mnuChuyenKho.Text = "Chuyển kho";
            this.mnuChuyenKho.Click += new System.EventHandler(this.mnuChuyenKho_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(140, 6);
            // 
            // mnuDoiKho
            // 
            this.mnuDoiKho.Name = "mnuDoiKho";
            this.mnuDoiKho.Size = new System.Drawing.Size(143, 22);
            this.mnuDoiKho.Text = "Đổi kho";
            this.mnuDoiKho.Click += new System.EventHandler(this.mnuDoiKho_Click);
            // 
            // mnuDanhMuc
            // 
            this.mnuDanhMuc.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuDanhSachHangHoa});
            this.mnuDanhMuc.Name = "mnuDanhMuc";
            this.mnuDanhMuc.Size = new System.Drawing.Size(77, 21);
            this.mnuDanhMuc.Text = "H&àng hóa";
            // 
            // mnuDanhSachHangHoa
            // 
            this.mnuDanhSachHangHoa.Name = "mnuDanhSachHangHoa";
            this.mnuDanhSachHangHoa.Size = new System.Drawing.Size(195, 22);
            this.mnuDanhSachHangHoa.Text = "Danh sách hàng hóa";
            this.mnuDanhSachHangHoa.Click += new System.EventHandler(this.mnuDanhSachHangHoa_Click);
            // 
            // mnuMuaHang
            // 
            this.mnuMuaHang.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuNhaCungCap,
            this.toolStripSeparator4,
            this.mnuNhapHang,
            this.mnuTraHangNhap});
            this.mnuMuaHang.Name = "mnuMuaHang";
            this.mnuMuaHang.Size = new System.Drawing.Size(79, 21);
            this.mnuMuaHang.Text = "&Mua hàng";
            // 
            // mnuNhaCungCap
            // 
            this.mnuNhaCungCap.Name = "mnuNhaCungCap";
            this.mnuNhaCungCap.Size = new System.Drawing.Size(160, 22);
            this.mnuNhaCungCap.Text = "Nhà cung cấp";
            this.mnuNhaCungCap.Click += new System.EventHandler(this.mnuNhaCungCap_Click);
            // 
            // toolStripSeparator4
            // 
            this.toolStripSeparator4.Name = "toolStripSeparator4";
            this.toolStripSeparator4.Size = new System.Drawing.Size(157, 6);
            // 
            // mnuNhapHang
            // 
            this.mnuNhapHang.Name = "mnuNhapHang";
            this.mnuNhapHang.Size = new System.Drawing.Size(160, 22);
            this.mnuNhapHang.Text = "Nhập hàng";
            this.mnuNhapHang.Click += new System.EventHandler(this.mnuNhapHang_Click);
            // 
            // mnuTraHangNhap
            // 
            this.mnuTraHangNhap.Name = "mnuTraHangNhap";
            this.mnuTraHangNhap.Size = new System.Drawing.Size(160, 22);
            this.mnuTraHangNhap.Text = "Trả hàng nhập";
            this.mnuTraHangNhap.Click += new System.EventHandler(this.mnuTraHangNhap_Click);
            // 
            // mnuBaoCao
            // 
            this.mnuBaoCao.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuBaoCaoTonKho,
            this.mnuBaoCaoTheKho});
            this.mnuBaoCao.Name = "mnuBaoCao";
            this.mnuBaoCao.Size = new System.Drawing.Size(67, 21);
            this.mnuBaoCao.Text = "&Báo cáo";
            // 
            // mnuBaoCaoTonKho
            // 
            this.mnuBaoCaoTonKho.Name = "mnuBaoCaoTonKho";
            this.mnuBaoCaoTonKho.Size = new System.Drawing.Size(171, 22);
            this.mnuBaoCaoTonKho.Text = "Báo cáo tồn kho";
            this.mnuBaoCaoTonKho.Click += new System.EventHandler(this.mnuBaoCaoTonKho_Click);
            // 
            // mnuBaoCaoTheKho
            // 
            this.mnuBaoCaoTheKho.Name = "mnuBaoCaoTheKho";
            this.mnuBaoCaoTheKho.Size = new System.Drawing.Size(171, 22);
            this.mnuBaoCaoTheKho.Text = "Báo cáo thẻ kho";
            this.mnuBaoCaoTheKho.Click += new System.EventHandler(this.mnuBaoCaoTheKho_Click);
            // 
            // mnuTroGiup
            // 
            this.mnuTroGiup.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuHuongDan,
            this.mnuThongTin});
            this.mnuTroGiup.Name = "mnuTroGiup";
            this.mnuTroGiup.Size = new System.Drawing.Size(69, 21);
            this.mnuTroGiup.Text = "&Trợ giúp";
            // 
            // mnuHuongDan
            // 
            this.mnuHuongDan.Name = "mnuHuongDan";
            this.mnuHuongDan.Size = new System.Drawing.Size(197, 22);
            this.mnuHuongDan.Text = "Hướng dẫn sử dụng";
            // 
            // mnuThongTin
            // 
            this.mnuThongTin.Name = "mnuThongTin";
            this.mnuThongTin.Size = new System.Drawing.Size(197, 22);
            this.mnuThongTin.Text = "Thông tin phần mềm";
            this.mnuThongTin.Click += new System.EventHandler(this.menuThongTin_Click);
            // 
            // statusStrip1
            // 
            this.statusStrip1.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.statusStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tssNguoiDung,
            this.tssKho,
            this.tssThoiGian});
            this.statusStrip1.Location = new System.Drawing.Point(0, 599);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(1008, 22);
            this.statusStrip1.TabIndex = 1;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // tssNguoiDung
            // 
            this.tssNguoiDung.Name = "tssNguoiDung";
            this.tssNguoiDung.Size = new System.Drawing.Size(192, 17);
            this.tssNguoiDung.Text = "Người dùng: Quản trị viên (quanly)";
            // 
            // tssKho
            // 
            this.tssKho.Name = "tssKho";
            this.tssKho.Size = new System.Drawing.Size(111, 17);
            this.tssKho.Text = "Kho: Kho trung tâm";
            // 
            // tssThoiGian
            // 
            this.tssThoiGian.Name = "tssThoiGian";
            this.tssThoiGian.Size = new System.Drawing.Size(63, 17);
            this.tssThoiGian.Text = "Thời gian: ";
            // 
            // frmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1008, 621);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.menuStrip1);
            this.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            this.IsMdiContainer = true;
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "frmMain";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Hệ thống quản lí kho";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.frmMain_Load);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem mnuHeThong;
        private System.Windows.Forms.ToolStripMenuItem mnuNguoiDung;
        private System.Windows.Forms.ToolStripMenuItem mnuDoiMatKhau;
        private System.Windows.Forms.ToolStripMenuItem mnuDangXuat;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem mnuThoat;
        private System.Windows.Forms.ToolStripMenuItem mnuDanhMuc;
        private System.Windows.Forms.ToolStripMenuItem mnuQuanLyKho;
        private System.Windows.Forms.ToolStripMenuItem mnuChuyenKho;
        private System.Windows.Forms.ToolStripMenuItem mnuKiemKho;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripMenuItem mnuDoiKho;
        private System.Windows.Forms.ToolStripMenuItem mnuMuaHang;
        private System.Windows.Forms.ToolStripMenuItem mnuNhaCungCap;
        private System.Windows.Forms.ToolStripMenuItem mnuNhapHang;
        private System.Windows.Forms.ToolStripMenuItem mnuTraHangNhap;
        private System.Windows.Forms.ToolStripMenuItem mnuBaoCao;
        private System.Windows.Forms.ToolStripMenuItem mnuBaoCaoTonKho;
        private System.Windows.Forms.ToolStripMenuItem mnuBaoCaoTheKho;
        private System.Windows.Forms.ToolStripMenuItem mnuTroGiup;
        private System.Windows.Forms.ToolStripMenuItem mnuHuongDan;
        private System.Windows.Forms.ToolStripMenuItem mnuThongTin;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel tssNguoiDung;
        private System.Windows.Forms.ToolStripStatusLabel tssKho;
        private System.Windows.Forms.ToolStripStatusLabel tssThoiGian;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator4;
        private System.Windows.Forms.ToolStripMenuItem mnuDanhSachHangHoa;
    }
}
