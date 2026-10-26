using System;
using System.Windows.Forms;

namespace BTL_LTTQ_2.GUI.Forms.DonViTinh
{
    public partial class frmHienCotDvt : Form
    {
        private DataGridView dgvDonViTinh;

        public frmHienCotDvt()
        {
            InitializeComponent();
            CaiDatSuKienPhim();
        }

        public frmHienCotDvt(DataGridView dgv) : this()
        {
            this.dgvDonViTinh = dgv;
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

        private void DongBoTrangThaiCot()
        {
            if (dgvDonViTinh == null) return;

            SetCheckedIfExists("colMaDVT", chkMaDVT);
            SetCheckedIfExists("colTenDVT", chkTenDVT);
            SetCheckedIfExists("colSoLuongHangHoa", chkSoLuongHangHoa);
            SetCheckedIfExists("colTrangThai", chkTrangThai);
            SetCheckedIfExists("colHangHoaTieuBieu", chkHangHoaTieuBieu);
        }

        private void SetCheckedIfExists(string colName, CheckBox chk)
        {
            if (dgvDonViTinh.Columns.Contains(colName))
            {
                chk.Checked = dgvDonViTinh.Columns[colName].Visible;
            }
        }

        private void DangKySuKienCot()
        {
            chkMaDVT.CheckedChanged += (s, e) => DoiTrangThaiCot("colMaDVT", chkMaDVT.Checked);
            chkTenDVT.CheckedChanged += (s, e) => DoiTrangThaiCot("colTenDVT", chkTenDVT.Checked);
            chkSoLuongHangHoa.CheckedChanged += (s, e) => DoiTrangThaiCot("colSoLuongHangHoa", chkSoLuongHangHoa.Checked);
            chkTrangThai.CheckedChanged += (s, e) => DoiTrangThaiCot("colTrangThai", chkTrangThai.Checked);
            chkHangHoaTieuBieu.CheckedChanged += (s, e) => DoiTrangThaiCot("colHangHoaTieuBieu", chkHangHoaTieuBieu.Checked);
        }

        private void DoiTrangThaiCot(string colName, bool visible)
        {
            if (dgvDonViTinh != null && dgvDonViTinh.Columns.Contains(colName))
            {
                dgvDonViTinh.Columns[colName].Visible = visible;
            }
        }
    }
}
