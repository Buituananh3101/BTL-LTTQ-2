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

            SetCheckedIfExists("colChon", chkChon);
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
            if (dgvNhapHang == null) return;

            BindColumn("colChon", chkChon);
            BindColumn("colMaPhieu", chkMaPhieu);
            BindColumn("colThoiGian", chkThoiGian);
            BindColumn("colMaNcc", chkMaNcc);
            BindColumn("colTenNcc", chkTenNcc);
            BindColumn("colCanTraNcc", chkCanTraNcc);
            BindColumn("colTongTien", chkTongTien);
            BindColumn("colTrangThai", chkTrangThai);
        }

        private void BindColumn(string colName, CheckBox chk)
        {
            chk.CheckedChanged += (s, e) =>
            {
                if (dgvNhapHang.Columns.Contains(colName))
                {
                    dgvNhapHang.Columns[colName].Visible = chk.Checked;
                }
            };
        }
    }
}
