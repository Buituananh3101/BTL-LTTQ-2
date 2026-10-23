using BTL_LTTQ_2.GUI.Forms.NhapHang;
using BTL_LTTQ_2.GUI.Forms.NhaCungCap;
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

            // Thiết lập thông tin trạng thái ở góc trái dưới
            tssNguoiDung.Text = "Người dùng: Quản trị viên (quanly)";
            tssKho.Text = "Kho: Kho trung tâm";
            tssThoiGian.Text = "Thời gian: " + DateTime.Now.ToString("dd/MM/yyyy HH:mm");
        }

        private void menuDanhSachHangHoa_Click(object sender, EventArgs e)
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
                this.Close();
            }
        }

        private void menuThoat_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Bạn có chắc chắn muốn thoát chương trình?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
            {
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
            frm.WindowState = FormWindowState.Maximized;
            frm.Show();
        }
    }
}
