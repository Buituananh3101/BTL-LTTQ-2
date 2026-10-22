using System;
using System.Drawing;
using System.Windows.Forms;

namespace BTL_LTTQ_2.GUI.Forms.DanhSachHangHoa
{
    public partial class frmHienCot : Form
    {
        private DataGridView dgvHangHoa;

        public frmHienCot()
        {
            InitializeComponent();
            CaiDatSuKienPopup();
        }

        public frmHienCot(DataGridView dgv) : this()
        {
            this.dgvHangHoa = dgv;
            DongBoTrangThaiCot();
            DangKySuKienCot();
        }

        /// <summary>
        /// Tạo hiệu ứng bóng đổ (Drop Shadow) tự nhiên chuẩn Windows
        /// </summary>
        protected override CreateParams CreateParams
        {
            get
            {
                CreateParams cp = base.CreateParams;
                cp.ClassStyle |= 0x00020000; // CS_DROPSHADOW
                return cp;
            }
        }

        /// <summary>
        /// Vẽ viền 1px màu xám nhạt xung quanh form popup
        /// </summary>
        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            using (Pen borderPen = new Pen(Color.FromArgb(203, 213, 225), 1))
            {
                e.Graphics.DrawRectangle(borderPen, 0, 0, this.ClientSize.Width - 1, this.ClientSize.Height - 1);
            }
        }

        private void CaiDatSuKienPopup()
        {
            // Tự động đóng popup khi người dùng bấm chuột ra ngoài cửa sổ
            this.Deactivate += (s, e) => this.Close();

            // Nhấn phím Escape để đóng nhanh
            this.KeyDown += (s, e) =>
            {
                if (e.KeyCode == Keys.Escape)
                {
                    this.Close();
                }
            };
        }

        /// <summary>
        /// Nạp trạng thái Visible hiện thời của các cột trên DataGridView vào CheckBox
        /// </summary>
        private void DongBoTrangThaiCot()
        {
            if (dgvHangHoa == null) return;

            SetCheckedIfExists("colMaHangNoiBo", chkMaHang);
            SetCheckedIfExists("colTenHang", chkTenHang);
            SetCheckedIfExists("colTenNhom", chkNhomHang);
            SetCheckedIfExists("colTenDVT", chkDonViTinh);
            SetCheckedIfExists("colGiaBan", chkGiaBan);
            SetCheckedIfExists("colGiaVon", chkGiaVon);
            SetCheckedIfExists("colTonKho", chkTonKho);
            SetCheckedIfExists("colViTri", chkViTri);
            SetCheckedIfExists("colTrangThai", chkTrangThai);
        }

        private void SetCheckedIfExists(string columnName, CheckBox chk)
        {
            if (dgvHangHoa.Columns.Contains(columnName))
            {
                chk.Checked = dgvHangHoa.Columns[columnName].Visible;
            }
        }

        /// <summary>
        /// Gắn sự kiện khi tick/bỏ tick CheckBox thì ẩn/hiện cột tương ứng trên DataGridView
        /// </summary>
        private void DangKySuKienCot()
        {
            chkMaHang.CheckedChanged += (s, e) => DoiTrangThaiCot("colMaHangNoiBo", chkMaHang.Checked);
            chkTenHang.CheckedChanged += (s, e) => DoiTrangThaiCot("colTenHang", chkTenHang.Checked);
            chkNhomHang.CheckedChanged += (s, e) => DoiTrangThaiCot("colTenNhom", chkNhomHang.Checked);
            chkDonViTinh.CheckedChanged += (s, e) => DoiTrangThaiCot("colTenDVT", chkDonViTinh.Checked);
            chkGiaBan.CheckedChanged += (s, e) => DoiTrangThaiCot("colGiaBan", chkGiaBan.Checked);
            chkGiaVon.CheckedChanged += (s, e) => DoiTrangThaiCot("colGiaVon", chkGiaVon.Checked);
            chkTonKho.CheckedChanged += (s, e) => DoiTrangThaiCot("colTonKho", chkTonKho.Checked);
            chkViTri.CheckedChanged += (s, e) => DoiTrangThaiCot("colViTri", chkViTri.Checked);
            chkTrangThai.CheckedChanged += (s, e) => DoiTrangThaiCot("colTrangThai", chkTrangThai.Checked);
        }

        private void DoiTrangThaiCot(string columnName, bool visible)
        {
            if (dgvHangHoa != null && dgvHangHoa.Columns.Contains(columnName))
            {
                dgvHangHoa.Columns[columnName].Visible = visible;
            }
        }
    }
}
