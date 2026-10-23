using System;
using System.Windows.Forms;

namespace BTL_LTTQ_2.GUI.Forms.TraHangNhap
{
    public partial class frmHienCotTraHang : Form
    {
        private DataGridView dgvTraHang;

        public frmHienCotTraHang()
        {
            InitializeComponent();
            CaiDatSuKienPhim();
        }

        public frmHienCotTraHang(DataGridView dgv) : this()
        {
            this.dgvTraHang = dgv;
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
        }

        private void frmHienCotTraHang_Deactivate(object sender, EventArgs e)
        {
            this.Close();
        }

        private void DongBoTrangThaiCot()
        {
            if (dgvTraHang == null) return;

            SetCheckedIfExists("colMaPhieu", chkMaPhieu);
            SetCheckedIfExists("colThoiGian", chkThoiGian);
            SetCheckedIfExists("colTenNcc", chkTenNcc);
            SetCheckedIfExists("colTongTienHang", chkTongTienHang);
            SetCheckedIfExists("colGiamGia", chkGiamGia);
            SetCheckedIfExists("colCanThuNcc", chkCanThuNcc);
            SetCheckedIfExists("colDaThuNcc", chkDaThuNcc);
            SetCheckedIfExists("colTrangThai", chkTrangThai);
        }

        private void SetCheckedIfExists(string colName, CheckBox chk)
        {
            if (dgvTraHang.Columns.Contains(colName))
            {
                chk.Checked = dgvTraHang.Columns[colName].Visible;
            }
        }

        private void DangKySuKienCot()
        {
            chkMaPhieu.CheckedChanged += (s, e) => DoiTrangThaiCot("colMaPhieu", chkMaPhieu.Checked);
            chkThoiGian.CheckedChanged += (s, e) => DoiTrangThaiCot("colThoiGian", chkThoiGian.Checked);
            chkTenNcc.CheckedChanged += (s, e) => DoiTrangThaiCot("colTenNcc", chkTenNcc.Checked);
            chkTongTienHang.CheckedChanged += (s, e) => DoiTrangThaiCot("colTongTienHang", chkTongTienHang.Checked);
            chkGiamGia.CheckedChanged += (s, e) => DoiTrangThaiCot("colGiamGia", chkGiamGia.Checked);
            chkCanThuNcc.CheckedChanged += (s, e) => DoiTrangThaiCot("colCanThuNcc", chkCanThuNcc.Checked);
            chkDaThuNcc.CheckedChanged += (s, e) => DoiTrangThaiCot("colDaThuNcc", chkDaThuNcc.Checked);
            chkTrangThai.CheckedChanged += (s, e) => DoiTrangThaiCot("colTrangThai", chkTrangThai.Checked);
        }

        private void DoiTrangThaiCot(string colName, bool visible)
        {
            if (dgvTraHang != null && dgvTraHang.Columns.Contains(colName))
            {
                dgvTraHang.Columns[colName].Visible = visible;
            }
        }
    }
}
