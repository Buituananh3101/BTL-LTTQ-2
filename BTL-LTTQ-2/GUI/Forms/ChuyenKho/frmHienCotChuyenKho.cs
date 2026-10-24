using System;
using System.Windows.Forms;

namespace BTL_LTTQ_2.GUI.Forms.ChuyenKho
{
    public partial class frmHienCotChuyenKho : Form
    {
        private DataGridView dgvChuyenKho;

        public frmHienCotChuyenKho()
        {
            InitializeComponent();
            CaiDatSuKienPhim();
        }

        public frmHienCotChuyenKho(DataGridView dgv) : this()
        {
            this.dgvChuyenKho = dgv;
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
            if (dgvChuyenKho == null) return;

            SetCheckedIfExists("colMaPhieu", chkMaPhieu);
            SetCheckedIfExists("colNgayLap", chkNgayLap);
            SetCheckedIfExists("colKhoNguon", chkKhoNguon);
            SetCheckedIfExists("colKhoDich", chkKhoDich);
            SetCheckedIfExists("chkTongSlChuyen", chkTongSlChuyen);
            SetCheckedIfExists("chkTongSlNhan", chkTongSlNhan);
            SetCheckedIfExists("chkTongGiaTri", chkTongGiaTri);
            SetCheckedIfExists("colNguoiTao", chkNguoiTao);
            SetCheckedIfExists("colGhiChu", chkGhiChu);
            SetCheckedIfExists("colTrangThai", chkTrangThai);
        }

        private void SetCheckedIfExists(string colName, CheckBox chk)
        {
            if (dgvChuyenKho.Columns.Contains(colName))
            {
                chk.Checked = dgvChuyenKho.Columns[colName].Visible;
            }
        }

        private void DangKySuKienCot()
        {
            chkMaPhieu.CheckedChanged += (s, e) => DoiTrangThaiCot("colMaPhieu", chkMaPhieu.Checked);
            chkNgayLap.CheckedChanged += (s, e) => DoiTrangThaiCot("colNgayLap", chkNgayLap.Checked);
            chkKhoNguon.CheckedChanged += (s, e) => DoiTrangThaiCot("colKhoNguon", chkKhoNguon.Checked);
            chkKhoDich.CheckedChanged += (s, e) => DoiTrangThaiCot("colKhoDich", chkKhoDich.Checked);
            chkTongSlChuyen.CheckedChanged += (s, e) => DoiTrangThaiCot("chkTongSlChuyen", chkTongSlChuyen.Checked);
            chkTongSlNhan.CheckedChanged += (s, e) => DoiTrangThaiCot("chkTongSlNhan", chkTongSlNhan.Checked);
            chkTongGiaTri.CheckedChanged += (s, e) => DoiTrangThaiCot("chkTongGiaTri", chkTongGiaTri.Checked);
            chkNguoiTao.CheckedChanged += (s, e) => DoiTrangThaiCot("colNguoiTao", chkNguoiTao.Checked);
            chkGhiChu.CheckedChanged += (s, e) => DoiTrangThaiCot("colGhiChu", chkGhiChu.Checked);
            chkTrangThai.CheckedChanged += (s, e) => DoiTrangThaiCot("colTrangThai", chkTrangThai.Checked);
        }

        private void DoiTrangThaiCot(string colName, bool visible)
        {
            if (dgvChuyenKho != null && dgvChuyenKho.Columns.Contains(colName))
            {
                dgvChuyenKho.Columns[colName].Visible = visible;
            }
        }
    }
}
