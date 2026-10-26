using System;
using System.Drawing;
using System.Windows.Forms;

namespace BTL_LTTQ_2.GUI.Forms.TraHangNhap
{
    public partial class frmHienCotHangTra : Form
    {
        private DataGridView dgvHangTra;

        public frmHienCotHangTra()
        {
            InitializeComponent();
            CaiDatSuKienPhim();
        }

        public frmHienCotHangTra(DataGridView dgv) : this()
        {
            this.dgvHangTra = dgv;
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
            if (dgvHangTra == null) return;

            SetCheckedIfExists("colMaHangNoiBo", chkMaHang);
            SetCheckedIfExists("colTenHang", chkTenHang);
            SetCheckedIfExists("colDonViTinh", chkDonViTinh);
            SetCheckedIfExists("colTonKho", chkTonKho);
            SetCheckedIfExists("colSoLuongTra", chkSoLuong);
            SetCheckedIfExists("colDonGiaTra", chkDonGia);
            SetCheckedIfExists("colThanhTien", chkThanhTien);
        }

        private void SetCheckedIfExists(string colName, CheckBox chk)
        {
            if (dgvHangTra.Columns.Contains(colName))
            {
                chk.Checked = dgvHangTra.Columns[colName].Visible;
            }
        }

        private void DangKySuKienCot()
        {
            chkMaHang.CheckedChanged += (s, e) => ToggleColumn("colMaHangNoiBo", chkMaHang.Checked);
            chkTenHang.CheckedChanged += (s, e) => ToggleColumn("colTenHang", chkTenHang.Checked);
            chkDonViTinh.CheckedChanged += (s, e) => ToggleColumn("colDonViTinh", chkDonViTinh.Checked);
            chkTonKho.CheckedChanged += (s, e) => ToggleColumn("colTonKho", chkTonKho.Checked);
            chkSoLuong.CheckedChanged += (s, e) => ToggleColumn("colSoLuongTra", chkSoLuong.Checked);
            chkDonGia.CheckedChanged += (s, e) => ToggleColumn("colDonGiaTra", chkDonGia.Checked);
            chkThanhTien.CheckedChanged += (s, e) => ToggleColumn("colThanhTien", chkThanhTien.Checked);
        }

        private void ToggleColumn(string colName, bool isVisible)
        {
            if (dgvHangTra != null && dgvHangTra.Columns.Contains(colName))
            {
                dgvHangTra.Columns[colName].Visible = isVisible;
            }
        }
    }
}
