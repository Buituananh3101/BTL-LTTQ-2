using System;
using System.Windows.Forms;

namespace BTL_LTTQ_2.GUI.Forms.KiemKho
{
    public partial class frmHienCotKiemKho : Form
    {
        private DataGridView dgvKiemKho;

        public frmHienCotKiemKho()
        {
            InitializeComponent();
            CaiDatSuKienPhim();
        }

        public frmHienCotKiemKho(DataGridView dgv) : this()
        {
            this.dgvKiemKho = dgv;
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
            if (dgvKiemKho == null) return;

            SetCheckedIfExists("colMaPhieu", chkMaPhieu);
            SetCheckedIfExists("colNgayCanBang", chkNgayCanBang);
            SetCheckedIfExists("colSlLechTang", chkSlLechTang);
            SetCheckedIfExists("colSlLechGiam", chkSlLechGiam);
            SetCheckedIfExists("colSlThucTe", chkSlThucTe);
            SetCheckedIfExists("colGhiChu", chkGhiChu);
            SetCheckedIfExists("colTongThucTe", chkTongThucTe);
            SetCheckedIfExists("colTongChenhLech", chkTongChenhLech);
            SetCheckedIfExists("colThoiGian", chkThoiGian);
            SetCheckedIfExists("colKho", chkKho);
            SetCheckedIfExists("colTrangThai", chkTrangThai);
        }

        private void SetCheckedIfExists(string colName, CheckBox chk)
        {
            if (dgvKiemKho.Columns.Contains(colName))
            {
                chk.Checked = dgvKiemKho.Columns[colName].Visible;
            }
        }

        private void DangKySuKienCot()
        {
            chkMaPhieu.CheckedChanged += (s, e) => DoiTrangThaiCot("colMaPhieu", chkMaPhieu.Checked);
            chkNgayCanBang.CheckedChanged += (s, e) => DoiTrangThaiCot("colNgayCanBang", chkNgayCanBang.Checked);
            chkSlLechTang.CheckedChanged += (s, e) => DoiTrangThaiCot("colSlLechTang", chkSlLechTang.Checked);
            chkSlLechGiam.CheckedChanged += (s, e) => DoiTrangThaiCot("colSlLechGiam", chkSlLechGiam.Checked);
            chkSlThucTe.CheckedChanged += (s, e) => DoiTrangThaiCot("colSlThucTe", chkSlThucTe.Checked);
            chkGhiChu.CheckedChanged += (s, e) => DoiTrangThaiCot("colGhiChu", chkGhiChu.Checked);
            chkTongThucTe.CheckedChanged += (s, e) => DoiTrangThaiCot("colTongThucTe", chkTongThucTe.Checked);
            chkTongChenhLech.CheckedChanged += (s, e) => DoiTrangThaiCot("colTongChenhLech", chkTongChenhLech.Checked);
            chkThoiGian.CheckedChanged += (s, e) => DoiTrangThaiCot("colThoiGian", chkThoiGian.Checked);
            chkKho.CheckedChanged += (s, e) => DoiTrangThaiCot("colKho", chkKho.Checked);
            chkTrangThai.CheckedChanged += (s, e) => DoiTrangThaiCot("colTrangThai", chkTrangThai.Checked);
        }

        private void DoiTrangThaiCot(string colName, bool visible)
        {
            if (dgvKiemKho != null && dgvKiemKho.Columns.Contains(colName))
            {
                dgvKiemKho.Columns[colName].Visible = visible;
            }
        }
    }
}
