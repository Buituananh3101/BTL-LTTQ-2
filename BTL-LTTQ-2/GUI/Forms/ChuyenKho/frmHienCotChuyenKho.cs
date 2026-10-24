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

        private void frmHienCotChuyenKho_Deactivate(object sender, EventArgs e)
        {
            this.Close();
        }

        private void DongBoTrangThaiCot()
        {
            if (dgvChuyenKho == null) return;

            SetCheckedIfExists("colMaPhieu", chkMaPhieu);
            SetCheckedIfExists("colNgayLap", chkNgayLap);
            SetCheckedIfExists("colKhoNguon", chkKhoNguon);
            SetCheckedIfExists("colKhoDich", chkKhoDich);
            SetCheckedIfExists("colTongSlChuyen", chkTongSlChuyen);
            SetCheckedIfExists("colTongSlNhan", chkTongSlNhan);
            SetCheckedIfExists("colTongGiaTri", chkTongGiaTri);
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
            chkMaPhieu.CheckedChanged += (s, e) => ToggleColumn("colMaPhieu", chkMaPhieu.Checked);
            chkNgayLap.CheckedChanged += (s, e) => ToggleColumn("colNgayLap", chkNgayLap.Checked);
            chkKhoNguon.CheckedChanged += (s, e) => ToggleColumn("colKhoNguon", chkKhoNguon.Checked);
            chkKhoDich.CheckedChanged += (s, e) => ToggleColumn("colKhoDich", chkKhoDich.Checked);
            chkTongSlChuyen.CheckedChanged += (s, e) => ToggleColumn("colTongSlChuyen", chkTongSlChuyen.Checked);
            chkTongSlNhan.CheckedChanged += (s, e) => ToggleColumn("colTongSlNhan", chkTongSlNhan.Checked);
            chkTongGiaTri.CheckedChanged += (s, e) => ToggleColumn("colTongGiaTri", chkTongGiaTri.Checked);
            chkNguoiTao.CheckedChanged += (s, e) => ToggleColumn("colNguoiTao", chkNguoiTao.Checked);
            chkGhiChu.CheckedChanged += (s, e) => ToggleColumn("colGhiChu", chkGhiChu.Checked);
            chkTrangThai.CheckedChanged += (s, e) => ToggleColumn("colTrangThai", chkTrangThai.Checked);
        }

        private void ToggleColumn(string colName, bool isVisible)
        {
            if (dgvChuyenKho != null && dgvChuyenKho.Columns.Contains(colName))
            {
                dgvChuyenKho.Columns[colName].Visible = isVisible;
            }
        }

        private void btnDong_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
