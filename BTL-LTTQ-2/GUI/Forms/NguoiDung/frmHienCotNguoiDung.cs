using System;
using System.Windows.Forms;

namespace BTL_LTTQ_2.GUI.Forms.NguoiDung
{
    public partial class frmHienCotNguoiDung : Form
    {
        private DataGridView dgvNguoiDung;

        public frmHienCotNguoiDung()
        {
            InitializeComponent();
            CaiDatSuKienPhim();
        }

        public frmHienCotNguoiDung(DataGridView dgv) : this()
        {
            this.dgvNguoiDung = dgv;
            DongBoTrangThaiCot();
            DangKySuKienCot();
        }

        private void CaiDatSuKienPhim()
        {
            // Nhấn phím Escape để đóng form
            this.KeyDown += (s, e) =>
            {
                if (e.KeyCode == Keys.Escape)
                {
                    this.Close();
                }
            };
        }

        /// <summary>
        /// Nạp trạng thái Visible hiện thời của các cột trên DataGridView vào CheckBox
        /// </summary>
        private void DongBoTrangThaiCot()
        {
            if (dgvNguoiDung == null) return;

            SetCheckedIfExists("colMaTaiKhoan", chkMaTaiKhoan);
            SetCheckedIfExists("colTenDangNhap", chkTenDangNhap);
            SetCheckedIfExists("colMatKhau", chkMatKhau);
            SetCheckedIfExists("colHoTen", chkHoTen);
            SetCheckedIfExists("colDienThoai", chkDienThoai);
            SetCheckedIfExists("colVaiTro", chkVaiTro);
            SetCheckedIfExists("colKhoPhuTrach", chkKhoPhuTrach);
            SetCheckedIfExists("colNgayTao", chkNgayTao);
            SetCheckedIfExists("colTrangThai", chkTrangThai);
        }

        private void SetCheckedIfExists(string columnName, CheckBox chk)
        {
            if (dgvNguoiDung.Columns.Contains(columnName))
            {
                chk.Checked = dgvNguoiDung.Columns[columnName].Visible;
            }
        }

        /// <summary>
        /// Gắn sự kiện khi tick/bỏ tick CheckBox thì ẩn/hiện cột tương ứng trên DataGridView
        /// </summary>
        private void DangKySuKienCot()
        {
            chkMaTaiKhoan.CheckedChanged += (s, e) => DoiTrangThaiCot("colMaTaiKhoan", chkMaTaiKhoan.Checked);
            chkTenDangNhap.CheckedChanged += (s, e) => DoiTrangThaiCot("colTenDangNhap", chkTenDangNhap.Checked);
            chkMatKhau.CheckedChanged += (s, e) => DoiTrangThaiCot("colMatKhau", chkMatKhau.Checked);
            chkHoTen.CheckedChanged += (s, e) => DoiTrangThaiCot("colHoTen", chkHoTen.Checked);
            chkDienThoai.CheckedChanged += (s, e) => DoiTrangThaiCot("colDienThoai", chkDienThoai.Checked);
            chkVaiTro.CheckedChanged += (s, e) => DoiTrangThaiCot("colVaiTro", chkVaiTro.Checked);
            chkKhoPhuTrach.CheckedChanged += (s, e) => DoiTrangThaiCot("colKhoPhuTrach", chkKhoPhuTrach.Checked);
            chkNgayTao.CheckedChanged += (s, e) => DoiTrangThaiCot("colNgayTao", chkNgayTao.Checked);
            chkTrangThai.CheckedChanged += (s, e) => DoiTrangThaiCot("colTrangThai", chkTrangThai.Checked);
        }

        private void DoiTrangThaiCot(string columnName, bool visible)
        {
            if (dgvNguoiDung != null && dgvNguoiDung.Columns.Contains(columnName))
            {
                dgvNguoiDung.Columns[columnName].Visible = visible;
            }
        }
    }
}