using System;
using System.Drawing;
using System.Windows.Forms;

namespace BTL_LTTQ_2.GUI.Forms.ChuyenKho
{
    public partial class frmHienCotHangChuyen : Form
    {
        private DataGridView dgvHangChuyen;

        public frmHienCotHangChuyen()
        {
            InitializeComponent();
            CaiDatSuKienPhim();
        }

        public frmHienCotHangChuyen(DataGridView dgv) : this()
        {
            this.dgvHangChuyen = dgv;
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
            if (dgvHangChuyen == null) return;

            SetCheckedIfExists("colMaHangNoiBo", chkMaHang);
            SetCheckedIfExists("colTenHang", chkTenHang);
            SetCheckedIfExists("colDonViTinh", chkDonViTinh);
            SetCheckedIfExists("colSoLuong", chkSoLuong);
            SetCheckedIfExists("colDonGia", chkDonGia);
            SetCheckedIfExists("colThanhTien", chkThanhTien);
        }

        private void SetCheckedIfExists(string colName, CheckBox chk)
        {
            if (dgvHangChuyen.Columns.Contains(colName))
            {
                chk.Checked = dgvHangChuyen.Columns[colName].Visible;
            }
        }

        private void DangKySuKienCot()
        {
            chkMaHang.CheckedChanged += (s, e) => ToggleColumn("colMaHangNoiBo", chkMaHang.Checked);
            chkTenHang.CheckedChanged += (s, e) => ToggleColumn("colTenHang", chkTenHang.Checked);
            chkDonViTinh.CheckedChanged += (s, e) => ToggleColumn("colDonViTinh", chkDonViTinh.Checked);
            chkSoLuong.CheckedChanged += (s, e) => ToggleColumn("colSoLuong", chkSoLuong.Checked);
            chkDonGia.CheckedChanged += (s, e) => ToggleColumn("colDonGia", chkDonGia.Checked);
            chkThanhTien.CheckedChanged += (s, e) => ToggleColumn("colThanhTien", chkThanhTien.Checked);
        }

        private void ToggleColumn(string colName, bool isVisible)
        {
            if (dgvHangChuyen != null && dgvHangChuyen.Columns.Contains(colName))
            {
                dgvHangChuyen.Columns[colName].Visible = isVisible;
            }
        }
    }
}
