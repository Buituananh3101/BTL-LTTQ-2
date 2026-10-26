using System;
using System.Windows.Forms;

namespace BTL_LTTQ_2.GUI.Forms.NguoiDung
{
    public partial class frmNguoiDung : Form
    {
        private bool isEditMode = false;
        private string editUsername = string.Empty;

        public frmNguoiDung()
        {
            InitializeComponent();
            isEditMode = false;
        }

        public frmNguoiDung(string tenDangNhap, string hoTen, string vaiTro, string sdt, string matKhau) : this()
        {
            isEditMode = true;
            editUsername = tenDangNhap;

            lblTieuDe.Text = $"Cập nhật người dùng: {tenDangNhap}";
            txtTenDangNhap.Text = tenDangNhap;
            txtTenDangNhap.ReadOnly = true;
            txtMatKhau.Text = matKhau;
            txtHoTen.Text = hoTen;
            txtDienThoai.Text = sdt;

            if (!string.IsNullOrEmpty(vaiTro))
            {
                cboVaiTro.SelectedItem = vaiTro;
            }
        }

        private void frmNguoiDung_Load(object sender, EventArgs e)
        {
            NapDanhSachKhoDemo();

            if (!isEditMode)
            {
                lblTieuDe.Text = "Tạo người dùng";
                cboVaiTro.SelectedIndex = 1; // Mặc định Thủ kho
                cboTrangThai.SelectedIndex = 0; // Đang hoạt động
            }

            CapNhatPhanQuyenKhoTheoVaiTro();
        }

        private void NapDanhSachKhoDemo()
        {
            clbDanhSachKho.Items.Clear();
            clbDanhSachKho.Items.Add("Kho Tổng Phú Quốc", true);
            clbDanhSachKho.Items.Add("Kho Chi nhánh Dương Đông", false);
            clbDanhSachKho.Items.Add("Kho Chi nhánh An Thới", false);

            if (cboVaiTro.SelectedIndex < 0)
            {
                cboVaiTro.SelectedIndex = 0;
            }
            if (cboTrangThai.SelectedIndex < 0)
            {
                cboTrangThai.SelectedIndex = 0;
            }
        }

        private void chkHienMatKhau_CheckedChanged(object sender, EventArgs e)
        {
            txtMatKhau.UseSystemPasswordChar = !chkHienMatKhau.Checked;
        }

        private void cboVaiTro_SelectedIndexChanged(object sender, EventArgs e)
        {
            CapNhatPhanQuyenKhoTheoVaiTro();
        }

        private void CapNhatPhanQuyenKhoTheoVaiTro()
        {
            bool laQuanLy = cboVaiTro.SelectedItem != null && cboVaiTro.SelectedItem.ToString() == "Quản lý";
            if (laQuanLy)
            {
                for (int i = 0; i < clbDanhSachKho.Items.Count; i++)
                {
                    clbDanhSachKho.SetItemChecked(i, true);
                }
                clbDanhSachKho.Enabled = false;
            }
            else
            {
                clbDanhSachKho.Enabled = true;
            }
        }

        private void btnBoQua_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnLuu_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtTenDangNhap.Text))
            {
                MessageBox.Show("Vui lòng nhập tên đăng nhập.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtTenDangNhap.Focus();
                return;
            }

            if (string.IsNullOrWhiteSpace(txtMatKhau.Text))
            {
                MessageBox.Show("Vui lòng nhập mật khẩu.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtMatKhau.Focus();
                return;
            }

            if (string.IsNullOrWhiteSpace(txtHoTen.Text))
            {
                MessageBox.Show("Vui lòng nhập họ và tên nhân viên.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtHoTen.Focus();
                return;
            }

            // Giao diện mẫu thành công
            this.DialogResult = DialogResult.OK;
            this.Close();
        }
    }
}
