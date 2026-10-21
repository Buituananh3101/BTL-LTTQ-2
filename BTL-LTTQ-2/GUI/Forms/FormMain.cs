using System;
using System.Windows.Forms;

namespace BTL_LTTQ_2.GUI.Forms
{
    public partial class FormMain : Form
    {
        public FormMain()
        {
            InitializeComponent();
        }

        private void FormMain_Load(object sender, EventArgs e)
        {
            // Thiết lập thông tin trạng thái ở góc dưới
            tssNguoiDung.Text = "Người dùng: Quản trị viên (quanly)";
            tssKho.Text = "Kho: Kho trung tâm";
            tssThoiGian.Text = "Thời gian: " + DateTime.Now.ToString("dd/MM/yyyy HH:mm");
        }

        private void menuHangHoa_Click(object sender, EventArgs e)
        {
            // Kiểm tra xem form đã mở chưa, nếu mở rồi thì Active lên trước
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
            frm.WindowState = FormWindowState.Maximized;
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
    }
}
