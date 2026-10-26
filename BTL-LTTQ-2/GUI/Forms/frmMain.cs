using BTL_LTTQ_2.Classes;
using BTL_LTTQ_2.GUI.Forms.BaoCao;
using BTL_LTTQ_2.GUI.Forms.ChuyenKho;
using BTL_LTTQ_2.GUI.Forms.DoiKho;
using BTL_LTTQ_2.GUI.Forms.NhaCungCap;
using BTL_LTTQ_2.GUI.Forms.NhapHang;
using BTL_LTTQ_2.GUI.Forms.DangNhap;
using BTL_LTTQ_2.GUI.Forms.NguoiDung;
using System;
using System.Drawing;

using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace BTL_LTTQ_2.GUI.Forms
{
    public partial class frmMain : Form
    {
        public frmMain()
        {
            InitializeComponent();
        }

        private void frmMain_Load(object sender, EventArgs e)
        {
            // Thiết lập chế độ kết xuất System giúp menu và statusbar sắc nét, không bị vệt mờ gradient
            menuStrip1.RenderMode = ToolStripRenderMode.System;
            statusStrip1.RenderMode = ToolStripRenderMode.System;

            // Thiết lập ảnh nền kho hàng phủ tối cho vùng làm việc MDI
            CaiDatHinhNenMdi();

            // Thiết lập tiêu đề kèm kho làm việc hiện tại
            this.Text = "Hệ thống quản lí kho - [" + SessionContext.TenKhoHienTai + "]";

            // Thiết lập thông tin trạng thái ở góc trái dưới
            tssNguoiDung.Text = "Người dùng: " + SessionContext.HoTen + " (" + SessionContext.TenVaiTro + ")";
            tssKho.Text = "Kho: " + SessionContext.TenKhoHienTai;
            tssThoiGian.Text = "Thời gian: " + DateTime.Now.ToString("dd/MM/yyyy HH:mm");

            // Phân quyền hiển thị và thao tác trên Menu theo vai trò tài khoản
            PhanQuyenMenu();
        }

        /// <summary>
        /// Phân quyền các mục MenuStrip theo 3 vai trò:
        /// 1. Quản lý (Quản lý mọi kho hàng, toàn quyền chức năng)
        /// 2. Thủ kho (Quản lý 1 kho duy nhất, toàn quyền nghiệp vụ kho, không được đổi kho)
        /// 3. Nhân viên kho (Tác nghiệp tại kho được phân công, không được đổi kho, ẩn danh mục quản lý)
        /// </summary>
        private void PhanQuyenMenu()
        {
            if (SessionContext.IsAdmin)
            {
                // Vai trò 1: Quản lý - Toàn quyền mọi chức năng và mọi kho
                mnuNguoiDung.Visible = true;
                mnuDoiKho.Visible = true;
                mnuDoiKho.Enabled = true;

                mnuKiemKho.Visible = true;
                mnuChuyenKho.Visible = true;
                mnuDanhSachHangHoa.Visible = true;
                mnuNhaCungCap.Visible = true;
                mnuNhapHang.Visible = true;
                mnuTraHangNhap.Visible = true;
                mnuBaoCaoTonKho.Visible = true;
                mnuBaoCaoTheKho.Visible = true;
            }
            else if (SessionContext.IsThuKho)
            {
                // Vai trò 2: Thủ kho (Quản lý 1 kho duy nhất)
                // Khóa chức năng đổi kho vì thủ kho chỉ quản lý 1 kho cố định được phân công
                mnuNguoiDung.Visible = false;
                mnuDoiKho.Visible = false;
                mnuDoiKho.Enabled = false;

                // Toàn quyền các nghiệp vụ quản lý kho của mình
                mnuKiemKho.Visible = true;
                mnuChuyenKho.Visible = true;
                mnuDanhSachHangHoa.Visible = true;
                mnuNhaCungCap.Visible = true;
                mnuNhapHang.Visible = true;
                mnuTraHangNhap.Visible = true;
                mnuBaoCaoTonKho.Visible = true;
                mnuBaoCaoTheKho.Visible = true;
            }
            else
            {
                // Vai trò 3: Nhân viên kho
                // Không được đổi kho
                mnuNguoiDung.Visible = false;
                mnuDoiKho.Visible = false;
                mnuDoiKho.Enabled = false;

                // Các chức năng tác nghiệp kho
                mnuKiemKho.Visible = true;
                mnuChuyenKho.Visible = true;
                mnuDanhSachHangHoa.Visible = true;
                mnuNhapHang.Visible = true;
                mnuTraHangNhap.Visible = true;
                mnuBaoCaoTonKho.Visible = true;

                // Ẩn các chức năng quản lý danh mục nhạy cảm và thẻ kho sổ sách
                mnuNhaCungCap.Visible = false;
                mnuBaoCaoTheKho.Visible = false;
            }
        }

                private void mnuNguoiDung_Click(object sender, EventArgs e)
        {
            foreach (Form form in this.MdiChildren)
            {
                if (form is frmDanhSachNguoiDung)
                {
                    form.Activate();
                    return;
                }
            }

            frmDanhSachNguoiDung frm = new frmDanhSachNguoiDung();
            frm.MdiParent = this;
            frm.Show();
        }

        private void mnuDoiMatKhau_Click(object sender, EventArgs e)
        {
            using (frmDoiMatKhau frm = new frmDoiMatKhau())
            {
                frm.ShowDialog();
            }
        }

        private void mnuDanhSachHangHoa_Click(object sender, EventArgs e)
        {
            // Kiểm tra xem form danh sách hàng hóa đã mở chưa, nếu đã mở thì kích hoạt (Active) lên trước
            foreach (Form form in this.MdiChildren)
            {
                if (form is frmDanhSachHangHoa)
                {
                    form.Activate();
                    return;
                }
            }

            frmDanhSachHangHoa frm = new frmDanhSachHangHoa();
            frm.MdiParent = this;
            frm.Show();
        }

        private void menuDangXuat_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Bạn có muốn đăng xuất khỏi hệ thống?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
            {
                SessionContext.IsDangXuat = true;
                this.Close();
            }
        }

        private void menuThoat_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Bạn có chắc chắn muốn thoát chương trình?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
            {
                SessionContext.IsDangXuat = false;
                Application.Exit();
            }
        }

        private void menuThongTin_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Hệ thống quản lí kho - BTL Lập trình trực quan\nPhiên bản: 1.0\nNền tảng: Windows Forms .NET Framework", "Thông tin phần mềm", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void mnuNhaCungCap_Click(object sender, EventArgs e)
        {
            // Kiểm tra xem form danh sách nhà cung cấp đã mở chưa, nếu đã mở thì kích hoạt (Active) lên trước
            foreach (Form form in this.MdiChildren)
            {
                if (form is frmNhaCungCap)
                {
                    form.Activate();
                    return;
                }
            }

            frmNhaCungCap frm = new frmNhaCungCap();
            frm.MdiParent = this;
            frm.Show();
        }

        private void mnuNhapHang_Click(object sender, EventArgs e)
        {
            // Kiểm tra xem form nhập hàng đã mở chưa, nếu đã mở thì kích hoạt (Active) lên trước
            foreach (Form form in this.MdiChildren)
            {
                if (form is frmNhapHang)
                {
                    form.Activate();
                    return;
                }
            }

            frmNhapHang frm = new frmNhapHang();
            frm.MdiParent = this;
            frm.Show();
        }

        private void mnuTraHangNhap_Click(object sender, EventArgs e)
        {
            // Kiểm tra xem form danh sách trả hàng nhập đã mở chưa, nếu đã mở thì kích hoạt (Active) lên trước
            foreach (Form form in this.MdiChildren)
            {
                if (form is BTL_LTTQ_2.GUI.Forms.TraHangNhap.frmTraHangNhap)
                {
                    form.Activate();
                    return;
                }
            }

            BTL_LTTQ_2.GUI.Forms.TraHangNhap.frmTraHangNhap frm = new BTL_LTTQ_2.GUI.Forms.TraHangNhap.frmTraHangNhap();
            frm.MdiParent = this;
            frm.Show();
        }

        private void mnuKiemKho_Click(object sender, EventArgs e)
        {
            // Kiểm tra xem form danh sách kiểm kho đã mở chưa, nếu đã mở thì kích hoạt (Active) lên trước
            foreach (Form form in this.MdiChildren)
            {
                if (form is BTL_LTTQ_2.GUI.Forms.KiemKho.frmKiemKho)
                {
                    form.Activate();
                    return;
                }
            }

            BTL_LTTQ_2.GUI.Forms.KiemKho.frmKiemKho frm = new BTL_LTTQ_2.GUI.Forms.KiemKho.frmKiemKho();
            frm.MdiParent = this;
            frm.Show();
        }

        private void mnuChuyenKho_Click(object sender, EventArgs e)
        {
            // Kiểm tra xem form danh sách chuyển kho đã mở chưa, nếu đã mở thì kích hoạt (Active) lên trước
            foreach (Form form in this.MdiChildren)
            {
                if (form is frmChuyenKho)
                {
                    form.Activate();
                    return;
                }
            }

            frmChuyenKho frm = new frmChuyenKho();
            frm.MdiParent = this;
            frm.Show();
        }

        private void mnuDoiKho_Click(object sender, EventArgs e)
        {
            using (frmDoiKho frm = new frmDoiKho())
            {
                if (frm.ShowDialog(this) == DialogResult.OK)
                {
                    // Cập nhật tiêu đề Form cha và StatusStrip
                    this.Text = "Hệ thống quản lí kho - [" + SessionContext.TenKhoHienTai + "]";
                    tssKho.Text = "Kho: " + SessionContext.TenKhoHienTai;

                    // Làm mới các form con MDI đang mở
                    foreach (Form form in this.MdiChildren)
                    {
                        if (form is frmChuyenKho ck)
                        {
                            ck.TaiDuLieu();
                        }
                        else if (form is BTL_LTTQ_2.GUI.Forms.KiemKho.frmKiemKho kk)
                        {
                            kk.TaiDuLieu();
                        }
                        else if (form is frmNhapHang nh)
                        {
                            nh.LoadData();
                        }
                        else if (form is frmBaoCaoTonKho bctk)
                        {
                            bctk.TaiDuLieu();
                        }
                        else if (form is frmBaoCaoTheKho bcthk)
                        {
                            bcthk.TaiDuLieuTheKho();
                        }
                        else if (form is frmDanhSachHangHoa dshh)
                        {
                            dshh.HienThiDanhSach();
                        }
                        else if (form is BTL_LTTQ_2.GUI.Forms.TraHangNhap.frmTraHangNhap thn)
                        {
                            thn.LoadData();
                        }
                    }
                }
            }
        }

        private void mnuBaoCaoTonKho_Click(object sender, EventArgs e)
        {
            // Kiểm tra xem form báo cáo tồn kho đã mở chưa, nếu đã mở thì kích hoạt (Active) lên trước
            foreach (Form form in this.MdiChildren)
            {
                if (form is frmBaoCaoTonKho)
                {
                    form.Activate();
                    return;
                }
            }

            frmBaoCaoTonKho frm = new frmBaoCaoTonKho();
            frm.MdiParent = this;
            frm.Show();
        }

        private void mnuBaoCaoTheKho_Click(object sender, EventArgs e)
        {
            // Kiểm tra xem form báo cáo thẻ kho đã mở chưa, nếu đã mở thì kích hoạt (Active) lên trước
            foreach (Form form in this.MdiChildren)
            {
                if (form is frmBaoCaoTheKho)
                {
                    form.Activate();
                    return;
                }
            }

            frmBaoCaoTheKho frm = new frmBaoCaoTheKho();
            frm.MdiParent = this;
            frm.Show();
        }

        private Image hinhNenKho = null;

        /// <summary>
        /// Cấu hình ảnh nền kho hàng được phủ tối dịu mắt cho vùng làm việc MdiClient
        /// Bật chế độ vẽ tùy biến (UserPaint) để vẽ duy nhất 1 ảnh to toàn màn hình,
        /// hoàn toàn khắc phục lỗi Windows Forms tự động lặp caro (tile) nhiều ảnh nhỏ.
        /// </summary>
        private void CaiDatHinhNenMdi()
        {
            hinhNenKho = LayHinhNenKho();
            foreach (Control ctrl in this.Controls)
            {
                if (ctrl is MdiClient mdiClient)
                {
                    if (hinhNenKho != null)
                    {
                        // Kích hoạt UserPaint và đệm đôi để tự vẽ 1 ảnh to duy nhất thay vì bị lặp ô gạch
                        var setStyleMethod = typeof(Control).GetMethod("SetStyle",
                            System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.NonPublic);
                        if (setStyleMethod != null)
                        {
                            setStyleMethod.Invoke(mdiClient, new object[] {
                                ControlStyles.UserPaint |
                                ControlStyles.AllPaintingInWmPaint |
                                ControlStyles.OptimizedDoubleBuffer,
                                true
                            });
                        }

                        // Không gán BackgroundImage mặc định để tránh Win32 MdiClient vẽ lặp caro
                        mdiClient.BackgroundImage = null;

                        // Gắn bộ điều khiển can thiệp thông điệp cuộn để vẽ lại ảnh ngay khi cuộn
                        new MdiScrollController(mdiClient);

                        // Tự vẽ duy nhất 1 ảnh lớn KHÓA CỨNG: không bị bóp khi thanh cuộn xuất hiện, không bị trôi khi cuộn
                        mdiClient.Paint += (s, e) =>
                        {
                            if (hinhNenKho != null)
                            {
                                e.Graphics.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;

                                // Lấy tọa độ cuộn thực tế của MdiClient để bù trừ, khóa cứng ảnh không bị trôi khi cuộn
                                int scrollX = GetScrollPos(mdiClient.Handle, SB_HORZ);
                                int scrollY = GetScrollPos(mdiClient.Handle, SB_VERT);

                                // Luôn vẽ theo Width và Height toàn phần của MdiClient (thay vì ClientSize bị trừ thanh cuộn),
                                // bảo đảm ảnh không bao giờ bị co bóp hay méo hình khi thanh cuộn ngang/dọc xuất hiện
                                e.Graphics.DrawImage(hinhNenKho, scrollX, scrollY, mdiClient.Width, mdiClient.Height);

                                // Lớp phủ tối bán trong suốt (Dark Overlay) làm chìm hình nền kho, chống chói lóa và nổi bật các form con
                                using (var brush = new SolidBrush(Color.FromArgb(90, 0, 0, 0)))
                                {
                                    e.Graphics.FillRectangle(brush, scrollX, scrollY, mdiClient.Width, mdiClient.Height);
                                }
                            }
                        };

                        // Tự động vẽ lại toàn bộ khi form thay đổi kích thước hoặc phóng to toàn màn hình
                        mdiClient.Resize += (s, e) => mdiClient.Invalidate();
                        this.Resize += (s, e) => mdiClient.Invalidate();
                    }
                    else
                    {
                        mdiClient.BackColor = System.Drawing.Color.FromArgb(240, 242, 245);
                    }
                    break;
                }
            }
        }

        /// <summary>
        /// Nạp ảnh nền kho hàng từ tài nguyên nhúng hoặc thư mục Resources
        /// </summary>
        private Image LayHinhNenKho()
        {
            try
            {
                // 1. Đọc từ tài nguyên nhúng của Assembly
                var asm = System.Reflection.Assembly.GetExecutingAssembly();
                var stream = asm.GetManifestResourceStream("BTL_LTTQ_2.Resources.hinh_nen_kho.jpg");
                if (stream != null)
                {
                    return Image.FromStream(stream);
                }
            }
            catch { }

            try
            {
                // 2. Dự phòng: Đọc từ thư mục Resources của ứng dụng
                string path = System.IO.Path.Combine(Application.StartupPath, "Resources", "hinh_nen_kho.jpg");
                if (System.IO.File.Exists(path))
                {
                    return Image.FromFile(path);
                }
            }
            catch { }

            return null;
        }

        #region Win32 API & Bộ điều khiển cuộn MdiClient giúp khóa cứng hình nền

        [DllImport("user32.dll")]
        private static extern int GetScrollPos(IntPtr hWnd, int nBar);
        private const int SB_HORZ = 0;
        private const int SB_VERT = 1;

        /// <summary>
        /// Bộ lắng nghe thông điệp cuộn WM_HSCROLL / WM_VSCROLL của MdiClient
        /// Khi người dùng cuộn vùng làm việc, lập tức yêu cầu vẽ lại để ảnh nền giữ nguyên vị trí cố định
        /// </summary>
        private class MdiScrollController : NativeWindow
        {
            private readonly MdiClient _mdiClient;
            private const int WM_HSCROLL = 0x0114;
            private const int WM_VSCROLL = 0x0115;

            public MdiScrollController(MdiClient mdiClient)
            {
                _mdiClient = mdiClient;
                AssignHandle(mdiClient.Handle);
            }

            protected override void WndProc(ref Message m)
            {
                base.WndProc(ref m);
                if (m.Msg == WM_HSCROLL || m.Msg == WM_VSCROLL)
                {
                    _mdiClient.Invalidate();
                }
            }
        }

        #endregion
    }
}