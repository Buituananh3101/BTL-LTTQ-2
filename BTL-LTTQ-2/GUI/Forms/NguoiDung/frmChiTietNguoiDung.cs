using System;
using System.Windows.Forms;

namespace BTL_LTTQ_2.GUI.Forms.NguoiDung
{
    public partial class frmChiTietNguoiDung : Form
    {
        private bool isEditMode = false;
        private string editUsername = string.Empty;

        public frmChiTietNguoiDung()
        {
            InitializeComponent();
            isEditMode = false;
        }

        public frmChiTietNguoiDung(string tenDangNhap, string hoTen, string vaiTro, string sdt, string matKhau) : this()
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

            btnKhoaTaiKhoan.Visible = true;
            btnXoa.Visible = true;
        }

        private void frmChiTietNguoiDung_Load(object sender, EventArgs e)
        {
            NapDanhSachKhoDemo();

            if (!isEditMode)
            {
                lblTieuDe.Text = "Tạo người dùng";
                btnKhoaTaiKhoan.Visible = false;
                btnXoa.Visible = false;
                cboVaiTro.SelectedIndex = 1; // Mặc định Thủ kho
                cboTrangThai.SelectedIndex = 0; // Đang hoạt động
            }
            else
            {
                btnKhoaTaiKhoan.Visible = true;
                btnXoa.Visible = true;
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

        private void btnKhoaTaiKhoan_Click(object sender, EventArgs e)
        {
            DialogResult dr = MessageBox.Show($"Bạn có chắc chắn muốn khóa tài khoản '{editUsername}'?", "Xác nhận khóa tài khoản", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dr == DialogResult.Yes)
            {
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            DialogResult dr = MessageBox.Show($"Bạn có chắc chắn muốn xóa vĩnh viễn tài khoản '{editUsername}' khỏi hệ thống?", "Xác nhận xóa tài khoản", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (dr == DialogResult.Yes)
            {
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
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
