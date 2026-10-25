using BTL_LTTQ_2.Classes;
using BTL_LTTQ_2.GUI.Forms.BaoCao;
using BTL_LTTQ_2.GUI.Forms.ChuyenKho;
using BTL_LTTQ_2.GUI.Forms.DoiKho;
using BTL_LTTQ_2.GUI.Forms.NhaCungCap;
using BTL_LTTQ_2.GUI.Forms.NhapHang;
using System;
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

            // Đổi màu nền vùng làm việc MDI sang màu sáng hiện đại
            foreach (Control ctrl in this.Controls)
            {
                if (ctrl is MdiClient mdiClient)
                {
                    mdiClient.BackColor = System.Drawing.Color.FromArgb(240, 242, 245);
                    break;
                }
            }

            // Thiết lập tiêu đề kèm kho làm việc hiện tại
            this.Text = "Quản Lý Kho Phú Quốc - [" + SessionContext.TenKhoHienTai + "]";

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
            MessageBox.Show("Hệ thống Quản lý Kho - BTL Lập trình trực quan\nPhiên bản: 1.0\nNền tảng: Windows Forms .NET Framework", "Thông tin phần mềm", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
                    this.Text = "Quản Lý Kho Phú Quốc - [" + SessionContext.TenKhoHienTai + "]";
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
    }
}
