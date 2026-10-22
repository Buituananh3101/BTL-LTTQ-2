using System;
using System.Windows.Forms;

namespace BTL_LTTQ_2.GUI.Forms.DanhSachHangHoa
{
    public partial class frmHienCot : Form
    {
        private DataGridView dgvHangHoa;

        public frmHienCot()
        {
            InitializeComponent();
            CaiDatSuKienPhim();
        }

        public frmHienCot(DataGridView dgv) : this()
        {
            this.dgvHangHoa = dgv;
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
            if (dgvHangHoa == null) return;

            SetCheckedIfExists("colMaHangNoiBo", chkMaHang);
            SetCheckedIfExists("colTenHang", chkTenHang);
            SetCheckedIfExists("colTenNhom", chkNhomHang);
            SetCheckedIfExists("colTenDVT", chkDonViTinh);
            SetCheckedIfExists("colGiaBan", chkGiaBan);
            SetCheckedIfExists("colGiaVon", chkGiaVon);
            SetCheckedIfExists("colTonKho", chkTonKho);
            SetCheckedIfExists("colViTri", chkViTri);
            SetCheckedIfExists("colTrangThai", chkTrangThai);
        }

        private void SetCheckedIfExists(string columnName, CheckBox chk)
        {
            if (dgvHangHoa.Columns.Contains(columnName))
            {
                chk.Checked = dgvHangHoa.Columns[columnName].Visible;
            }
        }

        /// <summary>
        /// Gắn sự kiện khi tick/bỏ tick CheckBox thì ẩn/hiện cột tương ứng trên DataGridView
        /// </summary>
        private void DangKySuKienCot()
        {
            chkMaHang.CheckedChanged += (s, e) => DoiTrangThaiCot("colMaHangNoiBo", chkMaHang.Checked);
            chkTenHang.CheckedChanged += (s, e) => DoiTrangThaiCot("colTenHang", chkTenHang.Checked);
            chkNhomHang.CheckedChanged += (s, e) => DoiTrangThaiCot("colTenNhom", chkNhomHang.Checked);
            chkDonViTinh.CheckedChanged += (s, e) => DoiTrangThaiCot("colTenDVT", chkDonViTinh.Checked);
            chkGiaBan.CheckedChanged += (s, e) => DoiTrangThaiCot("colGiaBan", chkGiaBan.Checked);
            chkGiaVon.CheckedChanged += (s, e) => DoiTrangThaiCot("colGiaVon", chkGiaVon.Checked);
            chkTonKho.CheckedChanged += (s, e) => DoiTrangThaiCot("colTonKho", chkTonKho.Checked);
            chkViTri.CheckedChanged += (s, e) => DoiTrangThaiCot("colViTri", chkViTri.Checked);
            chkTrangThai.CheckedChanged += (s, e) => DoiTrangThaiCot("colTrangThai", chkTrangThai.Checked);
        }

        private void DoiTrangThaiCot(string columnName, bool visible)
        {
            if (dgvHangHoa != null && dgvHangHoa.Columns.Contains(columnName))
            {
                dgvHangHoa.Columns[columnName].Visible = visible;
            }
        }
    }
}
