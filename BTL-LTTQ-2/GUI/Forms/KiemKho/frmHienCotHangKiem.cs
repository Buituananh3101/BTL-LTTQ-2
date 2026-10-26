using System;
using System.Drawing;
using System.Windows.Forms;

namespace BTL_LTTQ_2.GUI.Forms.KiemKho
{
    public partial class frmHienCotHangKiem : Form
    {
        private DataGridView dgvHangKiem;

        public frmHienCotHangKiem()
        {
            InitializeComponent();
            CaiDatSuKienPhim();
        }

        public frmHienCotHangKiem(DataGridView dgv) : this()
        {
            this.dgvHangKiem = dgv;
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
            if (dgvHangKiem == null) return;

            SetCheckedIfExists("colMaHangNoiBo", chkMaHang);
            SetCheckedIfExists("colTenHang", chkTenHang);
            SetCheckedIfExists("colDonViTinh", chkDonViTinh);
            SetCheckedIfExists("colTonKho", chkTonKho);
            SetCheckedIfExists("colSoLuongThucTe", chkSoLuongThucTe);
            SetCheckedIfExists("colSlChenhLech", chkSlChenhLech);
            SetCheckedIfExists("colDonGia", chkDonGia);
            SetCheckedIfExists("colGiaTriChenhLech", chkGiaTriChenhLech);
        }

        private void SetCheckedIfExists(string colName, CheckBox chk)
        {
            if (dgvHangKiem.Columns.Contains(colName))
            {
                chk.Checked = dgvHangKiem.Columns[colName].Visible;
            }
        }

        private void DangKySuKienCot()
        {
            chkMaHang.CheckedChanged += (s, e) => DoiTrangThaiCot("colMaHangNoiBo", chkMaHang.Checked);
            chkTenHang.CheckedChanged += (s, e) => DoiTrangThaiCot("colTenHang", chkTenHang.Checked);
            chkDonViTinh.CheckedChanged += (s, e) => DoiTrangThaiCot("colDonViTinh", chkDonViTinh.Checked);
            chkTonKho.CheckedChanged += (s, e) => DoiTrangThaiCot("colTonKho", chkTonKho.Checked);
            chkSoLuongThucTe.CheckedChanged += (s, e) => DoiTrangThaiCot("colSoLuongThucTe", chkSoLuongThucTe.Checked);
            chkSlChenhLech.CheckedChanged += (s, e) => DoiTrangThaiCot("colSlChenhLech", chkSlChenhLech.Checked);
            chkDonGia.CheckedChanged += (s, e) => DoiTrangThaiCot("colDonGia", chkDonGia.Checked);
            chkGiaTriChenhLech.CheckedChanged += (s, e) => DoiTrangThaiCot("colGiaTriChenhLech", chkGiaTriChenhLech.Checked);
        }

        private void DoiTrangThaiCot(string colName, bool visible)
        {
            if (dgvHangKiem != null && dgvHangKiem.Columns.Contains(colName))
            {
                dgvHangKiem.Columns[colName].Visible = visible;
            }
        }
    }
}
