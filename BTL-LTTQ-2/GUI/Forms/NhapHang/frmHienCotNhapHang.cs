using System;
using System.Windows.Forms;

namespace BTL_LTTQ_2.GUI.Forms.NhapHang
{
    public partial class frmHienCotNhapHang : Form
    {
        private DataGridView dgvNhapHang;

        public frmHienCotNhapHang()
        {
            InitializeComponent();
            CaiDatSuKienPhim();
        }

        public frmHienCotNhapHang(DataGridView dgv) : this()
        {
            this.dgvNhapHang = dgv;
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

        private void frmHienCotNhapHang_Deactivate(object sender, EventArgs e)
        {
            this.Close();
        }

        private void DongBoTrangThaiCot()
        {
            if (dgvNhapHang == null) return;

            SetCheckedIfExists("colMaPhieu", chkMaPhieu);
            SetCheckedIfExists("colThoiGian", chkThoiGian);
            SetCheckedIfExists("colMaNcc", chkMaNcc);
            SetCheckedIfExists("colTenNcc", chkTenNcc);
            SetCheckedIfExists("colCanTraNcc", chkCanTraNcc);
            SetCheckedIfExists("colTongTien", chkTongTien);
            SetCheckedIfExists("colTrangThai", chkTrangThai);
        }

        private void SetCheckedIfExists(string colName, CheckBox chk)
        {
            if (dgvNhapHang.Columns.Contains(colName))
            {
                chk.Checked = dgvNhapHang.Columns[colName].Visible;
            }
        }

        private void DangKySuKienCot()
        {
            chkMaPhieu.CheckedChanged += (s, e) => DoiTrangThaiCot("colMaPhieu", chkMaPhieu.Checked);
            chkThoiGian.CheckedChanged += (s, e) => DoiTrangThaiCot("colThoiGian", chkThoiGian.Checked);
            chkMaNcc.CheckedChanged += (s, e) => DoiTrangThaiCot("colMaNcc", chkMaNcc.Checked);
            chkTenNcc.CheckedChanged += (s, e) => DoiTrangThaiCot("colTenNcc", chkTenNcc.Checked);
            chkCanTraNcc.CheckedChanged += (s, e) => DoiTrangThaiCot("colCanTraNcc", chkCanTraNcc.Checked);
            chkTongTien.CheckedChanged += (s, e) => DoiTrangThaiCot("colTongTien", chkTongTien.Checked);
            chkTrangThai.CheckedChanged += (s, e) => DoiTrangThaiCot("colTrangThai", chkTrangThai.Checked);
        }

        private void DoiTrangThaiCot(string colName, bool visible)
        {
            if (dgvNhapHang != null && dgvNhapHang.Columns.Contains(colName))
            {
                dgvNhapHang.Columns[colName].Visible = visible;
            }
        }
    }
}
