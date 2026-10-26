using System;
using System.Drawing;
using System.Windows.Forms;

namespace BTL_LTTQ_2.GUI.Forms.NhapHang
{
    public partial class frmHienCotHangNhap : Form
    {
        private DataGridView dgvChiTiet;

        public frmHienCotHangNhap()
        {
            InitializeComponent();
            CaiDatSuKienPhim();
        }

        public frmHienCotHangNhap(DataGridView dgv) : this()
        {
            this.dgvChiTiet = dgv;
            DongBoTrangThaiCot();
            DangKySuKienCot();
        }

        private void CaiDatSuKienPhim()
        {
            this.KeyDown += (s, e) =>
            {
                if (e.KeyCode == Keys.Escape)
                {
                    this.Close();
                }
            };

            this.Deactivate += (s, e) =>
            {
                this.Close();
            };
        }

        private void DongBoTrangThaiCot()
        {
            if (dgvChiTiet == null) return;

            SetCheckedIfExists("colMaHangNoiBo", chkMaHang);
            SetCheckedIfExists("colTenHang", chkTenHang);
            SetCheckedIfExists("colDonViTinh", chkDonViTinh);
            SetCheckedIfExists("colSoLuong", chkSoLuong);
            SetCheckedIfExists("colDonGia", chkDonGia);
            SetCheckedIfExists("colGiamGia", chkGiamGia);
            SetCheckedIfExists("colThanhTien", chkThanhTien);
        }

        private void SetCheckedIfExists(string colName, CheckBox chk)
        {
            if (dgvChiTiet.Columns.Contains(colName))
            {
                chk.Checked = dgvChiTiet.Columns[colName].Visible;
            }
        }

        private void DangKySuKienCot()
        {
            chkMaHang.CheckedChanged += (s, e) => ToggleColumn("colMaHangNoiBo", chkMaHang.Checked);
            chkTenHang.CheckedChanged += (s, e) => ToggleColumn("colTenHang", chkTenHang.Checked);
            chkDonViTinh.CheckedChanged += (s, e) => ToggleColumn("colDonViTinh", chkDonViTinh.Checked);
            chkSoLuong.CheckedChanged += (s, e) => ToggleColumn("colSoLuong", chkSoLuong.Checked);
            chkDonGia.CheckedChanged += (s, e) => ToggleColumn("colDonGia", chkDonGia.Checked);
            chkGiamGia.CheckedChanged += (s, e) => ToggleColumn("colGiamGia", chkGiamGia.Checked);
            chkThanhTien.CheckedChanged += (s, e) => ToggleColumn("colThanhTien", chkThanhTien.Checked);
        }

        private void ToggleColumn(string colName, bool isVisible)
        {
            if (dgvChiTiet != null && dgvChiTiet.Columns.Contains(colName))
            {
                dgvChiTiet.Columns[colName].Visible = isVisible;
            }
        }
    }
}
