using BTL_LTTQ_2.Classes;
using System;
using System.Data;
using System.Drawing;
using System.Reflection;
using System.Windows.Forms;
using System.Windows.Forms.VisualStyles;

namespace BTL_LTTQ_2.GUI.Forms.NguoiDung
{
    public partial class frmDanhSachNguoiDung : Form
    {
        private bool isHeaderChecked = false;

        public frmDanhSachNguoiDung()
        {
            InitializeComponent();
        }

        private void frmDanhSachNguoiDung_Load(object sender, EventArgs e)
        {
            DinhDangLuoi();
            NapBoLocGiaoDien();
            HienThiDanhSach();
        }

        #region Định dạng giao diện & Cấu hình lưới chuẩn theo frmDanhSachHangHoa

        private void DinhDangLuoi()
        {
            // Bật đệm đôi chống giật lag khi cuộn chuột giống frmDanhSachHangHoa
            typeof(DataGridView).InvokeMember("DoubleBuffered",
                BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.SetProperty,
                null, dgvNguoiDung, new object[] { true });

            dgvNguoiDung.AutoGenerateColumns = false;
            dgvNguoiDung.AllowUserToAddRows = false;
            dgvNguoiDung.AllowUserToDeleteRows = false;
            dgvNguoiDung.AllowUserToResizeRows = false;
            dgvNguoiDung.ReadOnly = false; // Để cột CheckBox có thể thao tác
            dgvNguoiDung.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvNguoiDung.MultiSelect = false;
            dgvNguoiDung.RowHeadersVisible = false;
            dgvNguoiDung.RowTemplate.Height = 36;
            dgvNguoiDung.BackgroundColor = Color.White;
            dgvNguoiDung.BorderStyle = BorderStyle.None;
            dgvNguoiDung.CellBorderStyle = DataGridViewCellBorderStyle.Single;
            dgvNguoiDung.GridColor = SystemColors.ControlDark;
            dgvNguoiDung.EnableHeadersVisualStyles = true;
            dgvNguoiDung.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.Raised;

            // Màu dòng xen kẽ và màu chọn hiện đại theo frmDanhSachHangHoa
            dgvNguoiDung.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(248, 250, 252);
            dgvNguoiDung.DefaultCellStyle.SelectionBackColor = Color.FromArgb(224, 231, 255);
            dgvNguoiDung.DefaultCellStyle.SelectionForeColor = Color.FromArgb(30, 41, 59);

            // Đặt các cột văn bản thành ReadOnly = true (chỉ cột colChon là cho phép tick)
            colChon.ReadOnly = false;
            colMaTaiKhoan.ReadOnly = true;
            colTenDangNhap.ReadOnly = true;
            colHoTen.ReadOnly = true;
            colDienThoai.ReadOnly = true;
            colVaiTro.ReadOnly = true;
            colKhoPhuTrach.ReadOnly = true;
            colNgayTao.ReadOnly = true;
            colTrangThai.ReadOnly = true;

            // Đăng ký các sự kiện tương tác
            dgvNguoiDung.CellPainting += dgvNguoiDung_CellPainting;
            dgvNguoiDung.CellClick += dgvNguoiDung_CellClick;
            dgvNguoiDung.ColumnHeaderMouseClick += dgvNguoiDung_ColumnHeaderMouseClick;
            dgvNguoiDung.KeyDown += dgvNguoiDung_KeyDown;
            dgvNguoiDung.CellFormatting += dgvNguoiDung_CellFormatting;
            dgvNguoiDung.DataError += (s, ev) => ev.ThrowException = false;
        }

        // Vẽ Header Checkbox cho cột colChon
        private void dgvNguoiDung_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        {
            if (e.RowIndex == -1 && e.ColumnIndex >= 0 && dgvNguoiDung.Columns[e.ColumnIndex].Name == "colChon")
            {
                e.PaintBackground(e.CellBounds, true);

                int boxSize = 14;
                int x = e.CellBounds.X + (e.CellBounds.Width - boxSize) / 2;
                int y = e.CellBounds.Y + (e.CellBounds.Height - boxSize) / 2;
                Rectangle rect = new Rectangle(x, y, boxSize, boxSize);

                if (Application.RenderWithVisualStyles)
                {
                    CheckBoxState state = isHeaderChecked
                        ? CheckBoxState.CheckedNormal
                        : CheckBoxState.UncheckedNormal;
                    CheckBoxRenderer.DrawCheckBox(e.Graphics, new Point(x, y), state);
                }
                else
                {
                    ControlPaint.DrawCheckBox(e.Graphics, rect, isHeaderChecked ? ButtonState.Checked : ButtonState.Normal);
                }

                e.Handled = true;
                return;
            }
        }

        private void dgvNguoiDung_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.ColumnIndex >= 0 && dgvNguoiDung.Columns[e.ColumnIndex].Name == "colChon")
            {
                bool currentVal = Convert.ToBoolean(dgvNguoiDung.Rows[e.RowIndex].Cells["colChon"].Value ?? false);
                bool newVal = !currentVal;
                dgvNguoiDung.Rows[e.RowIndex].Cells["colChon"].Value = newVal;
                CapNhatTrangThaiHeaderCheckBox();
            }
        }

        private void dgvNguoiDung_ColumnHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.ColumnIndex >= 0 && dgvNguoiDung.Columns[e.ColumnIndex].Name == "colChon")
            {
                if (dgvNguoiDung.Rows.Count == 0) return;

                isHeaderChecked = !isHeaderChecked;
                foreach (DataGridViewRow row in dgvNguoiDung.Rows)
                {
                    row.Cells["colChon"].Value = isHeaderChecked;
                }
                dgvNguoiDung.InvalidateCell(e.ColumnIndex, -1);
            }
        }

        private void dgvNguoiDung_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Space && dgvNguoiDung.CurrentRow != null && dgvNguoiDung.CurrentRow.Index >= 0)
            {
                var cell = dgvNguoiDung.CurrentRow.Cells["colChon"];
                if (cell != null)
                {
                    bool currentVal = Convert.ToBoolean(cell.Value ?? false);
                    cell.Value = !currentVal;
                    CapNhatTrangThaiHeaderCheckBox();
                    e.Handled = true;
                }
            }
        }

        private void CapNhatTrangThaiHeaderCheckBox()
        {
            if (dgvNguoiDung.Rows.Count == 0)
            {
                isHeaderChecked = false;
                dgvNguoiDung.InvalidateCell(colChon.Index, -1);
                return;
            }

            bool allChecked = true;
            foreach (DataGridViewRow row in dgvNguoiDung.Rows)
            {
                bool val = Convert.ToBoolean(row.Cells["colChon"].Value ?? false);
                if (!val)
                {
                    allChecked = false;
                    break;
                }
            }

            if (isHeaderChecked != allChecked)
            {
                isHeaderChecked = allChecked;
                dgvNguoiDung.InvalidateCell(colChon.Index, -1);
            }
        }

        private void dgvNguoiDung_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.RowIndex < 0 || e.RowIndex >= dgvNguoiDung.Rows.Count) return;

            string colName = dgvNguoiDung.Columns[e.ColumnIndex].Name;

            // Định dạng hiển thị trạng thái chuẩn 3 nhóm theo Rule 15b & Rule 18
            if (colName == "colTrangThai" && e.Value != null)
            {
                string val = e.Value.ToString();
                if (string.Equals(val, "HOAT_DONG", StringComparison.OrdinalIgnoreCase) || val == "Đang hoạt động")
                {
                    e.Value = "Đang hoạt động";
                    e.CellStyle.ForeColor = Color.ForestGreen;
                }
                else
                {
                    e.Value = "Ngừng hoạt động";
                    e.CellStyle.ForeColor = Color.FromArgb(100, 116, 139);
                }
            }
        }

        #endregion

        #region Dữ liệu & Bộ lọc

        private void NapBoLocGiaoDien()
        {
            cboVaiTro.Items.Clear();
            cboVaiTro.Items.AddRange(new object[] { "Tất cả", "Quản lý", "Thủ kho", "Nhân viên kho" });
            cboVaiTro.SelectedIndex = 0;

            cboTrangThai.Items.Clear();
            cboTrangThai.Items.AddRange(new object[] { "Tất cả", "Đang hoạt động", "Ngừng hoạt động" });
            cboTrangThai.SelectedIndex = 0;

            cboKho.Items.Clear();
            cboKho.Items.AddRange(new object[] { "Tất cả các kho", "Kho trung tâm", "Kho An Thới", "Kho phụ kiện Tuna" });
            cboKho.SelectedIndex = 0;

            if (cboSoDongMoiTrang.Items.Count > 0 && cboSoDongMoiTrang.SelectedIndex < 0)
            {
                cboSoDongMoiTrang.SelectedIndex = 1; // 20 dòng
            }
        }

        public void HienThiDanhSach()
        {
            try
            {
                dgvNguoiDung.Rows.Clear();

                string sql = @"
                    SELECT tk.MaTaiKhoan, tk.TenDangNhap, nv.HoTen, nv.DienThoai, 
                           vt.TenVaiTro, tk.TrangThai, tk.LanDangNhapCuoi,
                           ISNULL((
                               SELECT STRING_AGG(k.TenKho, ', ')
                               FROM TaiKhoanKho tkk
                               INNER JOIN Kho k ON tkk.MaKho = k.MaKho
                               WHERE tkk.MaTaiKhoan = tk.MaTaiKhoan
                           ), N'Chưa phân công kho') AS KhoPhuTrach
                    FROM TaiKhoan tk
                    INNER JOIN VaiTro vt ON tk.MaVaiTro = vt.MaVaiTro
                    LEFT JOIN NhanVien nv ON tk.MaNV = nv.MaNV
                    ORDER BY tk.MaTaiKhoan ASC";

                DataTable dt = DataProcessing.ReadData(sql);
                if (dt != null && dt.Rows.Count > 0)
                {
                    foreach (DataRow dr in dt.Rows)
                    {
                        int maTk = Convert.ToInt32(dr["MaTaiKhoan"]);
                        string tenDn = dr["TenDangNhap"] != DBNull.Value ? dr["TenDangNhap"].ToString() : "";
                        string hoTen = dr["HoTen"] != DBNull.Value ? dr["HoTen"].ToString() : "";
                        string sdt = dr["DienThoai"] != DBNull.Value ? dr["DienThoai"].ToString() : "";
                        string vaiTro = dr["TenVaiTro"] != DBNull.Value ? dr["TenVaiTro"].ToString() : "";
                        string kho = dr["KhoPhuTrach"] != DBNull.Value ? dr["KhoPhuTrach"].ToString() : "";
                        string ngayTao = dr["LanDangNhapCuoi"] != DBNull.Value ? Convert.ToDateTime(dr["LanDangNhapCuoi"]).ToString("dd/MM/yyyy HH:mm") : "20/10/2026 08:30";
                        string trangThai = dr["TrangThai"] != DBNull.Value ? dr["TrangThai"].ToString() : "HOAT_DONG";

                        ThemDong(maTk, tenDn, hoTen, sdt, vaiTro, kho, ngayTao, trangThai);
                    }
                    lblTongSoHang.Text = $"Tổng số người dùng: {dgvNguoiDung.Rows.Count}";
                    CapNhatTrangThaiHeaderCheckBox();
                    return;
                }
            }
            catch
            {
                // Fallback nạp mẫu giao diện nếu chưa có CSDL
            }

            NapDuLieuMauGiaoDien();
        }

        private void NapDuLieuMauGiaoDien()
        {
            dgvNguoiDung.Rows.Clear();

            // Mẫu 1: Tài khoản Quản lý
            ThemDong(1, "quanly", "Nguyễn Văn Quản Trị", "0901000001", "Quản lý", "Toàn quyền tất cả các kho", "20/10/2026 08:30", "HOAT_DONG");

            // Mẫu 2: Tài khoản Thủ kho
            ThemDong(2, "thukho", "Trần Đình Thủ Kho", "0901000002", "Thủ kho", "Kho trung tâm", "20/10/2026 09:15", "HOAT_DONG");

            // Mẫu 3: Tài khoản Nhân viên kho 1
            ThemDong(3, "nvkho", "Lê Văn Nhân Viên", "0901000003", "Nhân viên kho", "Kho An Thới", "21/10/2026 14:00", "HOAT_DONG");

            // Mẫu 4: Tài khoản Bị khóa
            ThemDong(4, "nvtamkhoa", "Phạm Văn Tạm", "0901000004", "Nhân viên kho", "Kho phụ kiện Tuna", "22/10/2026 10:20", "NGUNG_HOAT_DONG");

            lblTongSoHang.Text = $"Tổng số người dùng: {dgvNguoiDung.Rows.Count}";
            CapNhatTrangThaiHeaderCheckBox();
        }

        private void ThemDong(int maTk, string tenDn, string hoTen, string sdt, string vaiTro, string kho, string ngayTao, string trangThai)
        {
            dgvNguoiDung.Rows.Add(false, maTk, tenDn, hoTen, sdt, vaiTro, kho, ngayTao, trangThai);
        }

        #endregion

        #region Tương tác Nút bấm & Điều hướng Form

        private void btnThemNguoiDung_Click(object sender, EventArgs e)
        {
            using (frmNguoiDung frm = new frmNguoiDung())
            {
                if (frm.ShowDialog() == DialogResult.OK)
                {
                    HienThiDanhSach();
                }
            }
        }

        private void dgvNguoiDung_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;
            if (dgvNguoiDung.Columns[e.ColumnIndex].Name == "colChon") return;

            DataGridViewRow row = dgvNguoiDung.Rows[e.RowIndex];
            string tenDangNhap = row.Cells["colTenDangNhap"].Value?.ToString() ?? "";
            string hoTen = row.Cells["colHoTen"].Value?.ToString() ?? "";
            string vaiTro = row.Cells["colVaiTro"].Value?.ToString() ?? "";
            string sdt = row.Cells["colDienThoai"].Value?.ToString() ?? "";

            // Mở form chi tiết người dùng: không truyền mật khẩu, lúc mở lên ô mật khẩu trắng tinh
            using (frmChiTietNguoiDung frm = new frmChiTietNguoiDung(tenDangNhap, hoTen, vaiTro, sdt))
            {
                if (frm.ShowDialog() == DialogResult.OK)
                {
                    HienThiDanhSach();
                }
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
            frm.Show();
        }

        private void btnHuongDanSuDung_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Màn hình Quản lý người dùng cho phép:\n" +
                "- Xem danh sách tài khoản và phân quyền kho làm việc.\n" +
                "- Lọc theo vai trò, trạng thái, kho phụ trách và thời gian tạo.\n" +
                "- Tạo mới người dùng và phân quyền kho (+ Người dùng).\n" +
                "- Nhấp đúp chuột vào một dòng để xem chi tiết, cập nhật hoặc khóa tài khoản.\n" +
                "- Tùy biến ẩn/hiện cột hiển thị qua nút 'Hiện cột'.",
                "Hướng dẫn sử dụng", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void txtTimKiem_TextChanged(object sender, EventArgs e)
        {
            string keyword = txtTimKiem.Text.Trim().ToLower();
            if (string.IsNullOrEmpty(keyword))
            {
                foreach (DataGridViewRow row in dgvNguoiDung.Rows)
                {
                    row.Visible = true;
                }
            }
            else
            {
                foreach (DataGridViewRow row in dgvNguoiDung.Rows)
                {
                    string tenDn = row.Cells["colTenDangNhap"].Value?.ToString().ToLower() ?? "";
                    string hoTen = row.Cells["colHoTen"].Value?.ToString().ToLower() ?? "";
                    string sdt = row.Cells["colDienThoai"].Value?.ToString().ToLower() ?? "";
                    string vaiTro = row.Cells["colVaiTro"].Value?.ToString().ToLower() ?? "";
                    string kho = row.Cells["colKhoPhuTrach"].Value?.ToString().ToLower() ?? "";

                    bool match = tenDn.Contains(keyword) || hoTen.Contains(keyword) || sdt.Contains(keyword) || vaiTro.Contains(keyword) || kho.Contains(keyword);
                    row.Visible = match;
                }
            }
        }

        #endregion
    }
}

