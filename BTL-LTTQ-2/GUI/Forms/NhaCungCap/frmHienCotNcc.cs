using System;
using System.Windows.Forms;

namespace BTL_LTTQ_2.GUI.Forms.NhaCungCap
{
    public partial class frmHienCotNcc : Form
    {
        private DataGridView dgvNhaCungCap;

        public frmHienCotNcc()
        {
            InitializeComponent();
            CaiDatSuKienPhim();
        }

        public frmHienCotNcc(DataGridView dgv) : this()
        {
            this.dgvNhaCungCap = dgv;
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

        private void frmHienCotNcc_Deactivate(object sender, EventArgs e)
        {
            // Tự đóng khi người dùng click ra ngoài
            this.Close();
        }

        /// <summary>
        /// Nạp trạng thái Visible hiện thời của các cột trên DataGridView vào CheckBox
        /// </summary>
        private void DongBoTrangThaiCot()
        {
            if (dgvNhaCungCap == null) return;

            SetCheckedIfExists("colMaNCC", chkMaNcc);
            SetCheckedIfExists("colTenNCC", chkTenNcc);
            SetCheckedIfExists("colDienThoai", chkDienThoai);
            SetCheckedIfExists("colEmail", chkEmail);
            SetCheckedIfExists("colNoCanTra", chkNoCanTra);
            SetCheckedIfExists("colTongMua", chkTongMua);
            SetCheckedIfExists("colTrangThai", chkTrangThai);
        }

        private void SetCheckedIfExists(string columnName, CheckBox chk)
        {
            if (dgvNhaCungCap.Columns.Contains(columnName))
            {
                chk.Checked = dgvNhaCungCap.Columns[columnName].Visible;
            }
        }

        /// <summary>
        /// Gắn sự kiện khi tick/bỏ tick CheckBox thì ẩn/hiện cột tương ứng trên DataGridView
        /// </summary>
        private void DangKySuKienCot()
        {
            chkMaNcc.CheckedChanged += (s, e) => DoiTrangThaiCot("colMaNCC", chkMaNcc.Checked);
            chkTenNcc.CheckedChanged += (s, e) => DoiTrangThaiCot("colTenNCC", chkTenNcc.Checked);
            chkDienThoai.CheckedChanged += (s, e) => DoiTrangThaiCot("colDienThoai", chkDienThoai.Checked);
            chkEmail.CheckedChanged += (s, e) => DoiTrangThaiCot("colEmail", chkEmail.Checked);
            chkNoCanTra.CheckedChanged += (s, e) => DoiTrangThaiCot("colNoCanTra", chkNoCanTra.Checked);
            chkTongMua.CheckedChanged += (s, e) => DoiTrangThaiCot("colTongMua", chkTongMua.Checked);
            chkTrangThai.CheckedChanged += (s, e) => DoiTrangThaiCot("colTrangThai", chkTrangThai.Checked);
        }

        private void DoiTrangThaiCot(string columnName, bool visible)
        {
            if (dgvNhaCungCap != null && dgvNhaCungCap.Columns.Contains(columnName))
            {
                dgvNhaCungCap.Columns[columnName].Visible = visible;
            }
        }
    }
}
