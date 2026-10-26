using BTL_LTTQ_2.Classes;
using System;
using System.Data;
using System.Windows.Forms;

namespace BTL_LTTQ_2.GUI.Forms.DangNhap
{
    public partial class frmDangNhap : Form
    {
        public frmDangNhap()
        {
            InitializeComponent();
        }

        private void frmDangNhap_Load(object sender, EventArgs e)
        {
            NapDanhSachKhoGiaoDien();
            txtTenDangNhap.Focus();
        }

        /// <summary>
        /// Nạp danh sách kho demo/mẫu lên giao diện
        /// </summary>
        private void NapDanhSachKhoGiaoDien()
        {
            try
            {
                DataTable dt = new DataTable();
                dt.Columns.Add("MaKho", typeof(int));
                dt.Columns.Add("TenKho", typeof(string));

                dt.Rows.Add(0, "Tất cả kho");
                dt.Rows.Add(1, "Kho Tổng Phú Quốc");
                dt.Rows.Add(2, "Kho Chi nhánh Dương Đông");
                dt.Rows.Add(3, "Kho Chi nhánh An Thới");

                cboKhoLamViec.DataSource = dt;
                cboKhoLamViec.DisplayMember = "TenKho";
                cboKhoLamViec.ValueMember = "MaKho";
                cboKhoLamViec.SelectedIndex = 0;
            }
            catch
            {
                // Bỏ qua lỗi giao diện ban đầu
            }
        }

        private void chkHienMatKhau_CheckedChanged(object sender, EventArgs e)
        {
            txtMatKhau.UseSystemPasswordChar = !chkHienMatKhau.Checked;
        }

        private void txtTenDangNhap_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;
                txtMatKhau.Focus();
            }
        }

        private void txtMatKhau_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;
                btnDangNhap.PerformClick();
            }
        }

        private void btnDangNhap_Click(object sender, EventArgs e)
        {
            string tenDangNhap = txtTenDangNhap.Text.Trim();
            string matKhau = txtMatKhau.Text;

            if (string.IsNullOrEmpty(tenDangNhap))
            {
                MessageBox.Show("Vui lòng nhập tên đăng nhập.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtTenDangNhap.Focus();
                return;
            }

            if (string.IsNullOrEmpty(matKhau))
            {
                MessageBox.Show("Vui lòng nhập mật khẩu.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtMatKhau.Focus();
                return;
            }

            // Thiết lập thông tin phiên làm việc giao diện
            SessionContext.MaTaiKhoan = 1;
            SessionContext.TenDangNhap = tenDangNhap;
            SessionContext.HoTen = "Quản trị viên Demo";
            SessionContext.MaVaiTro = 1;
            SessionContext.TenVaiTro = "Quản lý";
            SessionContext.MaKhoHienTai = cboKhoLamViec.SelectedValue != null ? Convert.ToInt32(cboKhoLamViec.SelectedValue) : 0;
            SessionContext.TenKhoHienTai = cboKhoLamViec.Text;
            SessionContext.IsDangXuat = false;

            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }
    }
}