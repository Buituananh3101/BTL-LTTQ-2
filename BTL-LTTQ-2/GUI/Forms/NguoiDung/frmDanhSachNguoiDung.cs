using System;
using System.Drawing;
using System.Windows.Forms;

namespace BTL_LTTQ_2.GUI.Forms.NguoiDung
{
    public partial class frmDanhSachNguoiDung : Form
    {
        public frmDanhSachNguoiDung()
        {
            InitializeComponent();
        }

        private void frmDanhSachNguoiDung_Load(object sender, EventArgs e)
        {
            NapBoLocGiaoDien();
            NapDuLieuMauGiaoDien();
        }

        private void NapBoLocGiaoDien()
        {
            cboVaiTro.Items.Clear();
            cboVaiTro.Items.AddRange(new object[] { "Tất cả", "Quản lý", "Thủ kho", "Nhân viên kho" });
            cboVaiTro.SelectedIndex = 0;

            cboTrangThai.Items.Clear();
            cboTrangThai.Items.AddRange(new object[] { "Tất cả", "Đang hoạt động", "Ngừng hoạt động" });
            cboTrangThai.SelectedIndex = 0;

            cboKho.Items.Clear();
            cboKho.Items.AddRange(new object[] { "Tất cả các kho", "Kho Tổng Phú Quốc", "Kho Chi nhánh Dương Đông", "Kho Chi nhánh An Thới" });
            cboKho.SelectedIndex = 0;

            cboSoDongMoiTrang.SelectedIndex = 1; // 20 dòng
        }

        private void NapDuLieuMauGiaoDien()
        {
            dgvNguoiDung.Rows.Clear();

            // Mẫu 1: Tài khoản Quản lý
            ThemDongMau(1, "quanly", "123456", "Nguyễn Văn Quản Trị", "0901000001", "Quản lý", "Toàn quyền tất cả các kho", "20/10/2026 08:30", "Đang hoạt động", true);

            // Mẫu 2: Tài khoản Thủ kho
            ThemDongMau(2, "thukho", "123456", "Trần Đình Thủ Kho", "0901000002", "Thủ kho", "Kho Tổng Phú Quốc", "20/10/2026 09:15", "Đang hoạt động", true);

            // Mẫu 3: Tài khoản Nhân viên kho 1
            ThemDongMau(3, "nvkho", "123456", "Lê Văn Nhân Viên", "0901000003", "Nhân viên kho", "Kho Chi nhánh Dương Đông", "21/10/2026 14:00", "Đang hoạt động", true);

            // Mẫu 4: Tài khoản Bị khóa
            ThemDongMau(4, "nvtamkhoa", "123456", "Phạm Văn Tạm", "0901000004", "Nhân viên kho", "Kho Chi nhánh An Thới", "22/10/2026 10:20", "Ngừng hoạt động", false);

            lblTongSoHang.Text = $"Tổng số người dùng: {dgvNguoiDung.Rows.Count}";
        }

        private void ThemDongMau(int maTk, string tenDn, string matKhau, string hoTen, string sdt, string vaiTro, string kho, string ngayTao, string trangThai, bool dangHoatDong)
        {
            int idx = dgvNguoiDung.Rows.Add(maTk, tenDn, matKhau, hoTen, sdt, vaiTro, kho, ngayTao, trangThai);
            DataGridViewRow row = dgvNguoiDung.Rows[idx];

            if (dangHoatDong)
            {
                row.Cells["colTrangThai"].Style.ForeColor = Color.ForestGreen;
                row.Cells["colTrangThai"].Style.Font = new Font("Segoe UI", 9.5f, FontStyle.Bold);
            }
            else
            {
                row.Cells["colTrangThai"].Style.ForeColor = Color.FromArgb(100, 116, 139);
            }
        }

        private void btnThemNguoiDung_Click(object sender, EventArgs e)
        {
            using (frmNguoiDung frm = new frmNguoiDung())
            {
                frm.ShowDialog();
            }
        }

        private void dgvNguoiDung_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;

            DataGridViewRow row = dgvNguoiDung.Rows[e.RowIndex];
            string tenDangNhap = row.Cells["colTenDangNhap"].Value?.ToString() ?? "";
            string hoTen = row.Cells["colHoTen"].Value?.ToString() ?? "";
            string vaiTro = row.Cells["colVaiTro"].Value?.ToString() ?? "";
            string sdt = row.Cells["colDienThoai"].Value?.ToString() ?? "";
            string matKhau = row.Cells["colMatKhau"].Value?.ToString() ?? "";

            using (frmNguoiDung frm = new frmNguoiDung(tenDangNhap, hoTen, vaiTro, sdt, matKhau))
            {
                frm.ShowDialog();
            }
        }

        private void btnHienCot_Click(object sender, EventArgs e)
        {
            foreach (Form openForm in Application.OpenForms)
            {
                if (openForm is frmHienCotNguoiDung)
                {
                    openForm.Activate();
                    openForm.BringToFront();
                    return;
                }
            }

            frmHienCotNguoiDung frm = new frmHienCotNguoiDung(this.dgvNguoiDung);
            frm.StartPosition = FormStartPosition.Manual;

            // Định vị popup ngay dưới nút Hiện cột
            Point btnLocation = btnHienCot.PointToScreen(Point.Empty);
            int popupX = btnLocation.X + btnHienCot.Width - frm.Width;
            int popupY = btnLocation.Y + btnHienCot.Height + 2;

            Rectangle screenBounds = Screen.FromControl(btnHienCot).WorkingArea;
            if (popupX + frm.Width > screenBounds.Right)
            {
                popupX = screenBounds.Right - frm.Width;
            }
            if (popupX < screenBounds.Left)
            {
                popupX = screenBounds.Left;
            }

            frm.Location = new Point(popupX, popupY);
            frm.Show(this);
        }
    }
}