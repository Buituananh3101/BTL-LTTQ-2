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
            // Nhấn phím Escape để đóng form
            this.KeyDown += (s, e) =>
            {
                if (e.KeyCode == Keys.Escape)
                {
                    this.Close();
                }
            };
        }

        private void frmHienCotDvt_Deactivate(object sender, EventArgs e)
        {
            // Tự đóng khi người dùng click ra ngoài
            this.Close();
        }

        /// <summary>
        /// Nạp trạng thái Visible hiện thời của các cột trên DataGridView vào CheckBox
        /// </summary>
        private void DongBoTrangThaiCot()
        {
            if (dgvDonViTinh == null) return;

            SetCheckedIfExists("colMaDVT", chkMaDVT);
            SetCheckedIfExists("colTenDVT", chkTenDVT);
            SetCheckedIfExists("colSoLuongHangHoa", chkSoLuongHangHoa);
            SetCheckedIfExists("colTrangThai", chkTrangThai);
            SetCheckedIfExists("colHangHoaTieuBieu", chkHangHoaTieuBieu);
        }

        private void SetCheckedIfExists(string columnName, CheckBox chk)
        {
            if (dgvDonViTinh.Columns.Contains(columnName))
            {
                chk.Checked = dgvDonViTinh.Columns[columnName].Visible;
            }
        }

        /// <summary>
        /// Gắn sự kiện khi tick/bỏ tick CheckBox thì ẩn/hiện cột tương ứng trên DataGridView
        /// </summary>
        private void DangKySuKienCot()
        {
            chkMaDVT.CheckedChanged += (s, e) => DoiTrangThaiCot("colMaDVT", chkMaDVT.Checked);
            chkTenDVT.CheckedChanged += (s, e) => DoiTrangThaiCot("colTenDVT", chkTenDVT.Checked);
            chkSoLuongHangHoa.CheckedChanged += (s, e) => DoiTrangThaiCot("colSoLuongHangHoa", chkSoLuongHangHoa.Checked);
            chkTrangThai.CheckedChanged += (s, e) => DoiTrangThaiCot("colTrangThai", chkTrangThai.Checked);
            chkHangHoaTieuBieu.CheckedChanged += (s, e) => DoiTrangThaiCot("colHangHoaTieuBieu", chkHangHoaTieuBieu.Checked);
        }

        private void DoiTrangThaiCot(string columnName, bool visible)
        {
            if (dgvDonViTinh != null && dgvDonViTinh.Columns.Contains(columnName))
            {
                dgvDonViTinh.Columns[columnName].Visible = visible;
            }
        }
    }
}
