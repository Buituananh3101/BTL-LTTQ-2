using BTL_LTTQ_2.Classes;
using System;
using System.Collections.Generic;
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

        // Dữ liệu người dùng nạp từ CSDL (lọc và phân trang trong bộ nhớ vì số lượng tài khoản nhỏ)
        private DataTable dtNguoiDung = null;
        private int trangHienTai = 1;
        private int soDongMoiTrang = 20;
        private bool dangNapBoLoc = false;

        private Timer searchDebounceTimer;

        public frmDanhSachNguoiDung()
        {
            InitializeComponent();
            KhoiTaoDebounceTimer();

            // Đăng ký sự kiện tìm kiếm, bộ lọc, phân trang và nút hướng dẫn
            txtTimKiem.TextChanged += txtTimKiem_TextChanged;
            txtTimKiem.KeyDown += txtTimKiem_KeyDown;
            cboVaiTro.SelectedIndexChanged += BoLoc_SelectedIndexChanged;
            cboTrangThai.SelectedIndexChanged += BoLoc_SelectedIndexChanged;
            cboKho.SelectedIndexChanged += BoLoc_SelectedIndexChanged;
            cboSoDongMoiTrang.SelectedIndexChanged += cboSoDongMoiTrang_SelectedIndexChanged;
            btnTrangTruoc.Click += btnTrangTruoc_Click;
            btnTrangSau.Click += btnTrangSau_Click;
            btnHuongDanSuDung.Click += btnHuongDanSuDung_Click;
        }

        private void KhoiTaoDebounceTimer()
        {
            searchDebounceTimer = new Timer();
            searchDebounceTimer.Interval = 300;
            searchDebounceTimer.Tick += (s, e) =>
            {
                searchDebounceTimer.Stop();
                trangHienTai = 1;
                ApDungBoLoc();
            };
        }

        private void frmDanhSachNguoiDung_Load(object sender, EventArgs e)
        {
            DinhDangLuoi();
            NapBoLocGiaoDien();
            HienThiDanhSach();
        }

        protected override void OnFormClosed(FormClosedEventArgs e)
        {
            searchDebounceTimer?.Stop();
            searchDebounceTimer?.Dispose();
            base.OnFormClosed(e);
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
            dgvNguoiDung.ReadOnly = true; // Cột CheckBox được đảo giá trị bằng code ở CellClick/Space (giống frmDanhSachHangHoa), tránh lưới tự đảo thêm lần nữa
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

            // Đặt toàn bộ các cột thành ReadOnly = true (cột colChon được tick bằng code)
            colChon.ReadOnly = true;
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
                    // 'KHOA' -> Đã khóa (nhóm Đã hủy / Ngừng: màu xám nhạt theo Điều luật 15b)
                    e.Value = "Đã khóa";
                    e.CellStyle.ForeColor = Color.FromArgb(100, 116, 139);
                }
                e.FormattingApplied = true;
            }
        }

        #endregion

        #region Dữ liệu & Bộ lọc

        private void NapBoLocGiaoDien()
        {
            dangNapBoLoc = true;

            // TaiKhoan không có cột ngày tạo -> ẩn nhóm lọc 'Thời gian tạo' để không che mất dữ liệu
            grpThoiGianTao.Visible = false;

            cboVaiTro.Items.Clear();
            cboVaiTro.Items.AddRange(new object[] { "Tất cả", "Quản lý", "Thủ kho", "Nhân viên kho" });
            cboVaiTro.SelectedIndex = 0;

            cboTrangThai.Items.Clear();
            cboTrangThai.Items.AddRange(new object[] { "Tất cả", "Đang hoạt động", "Đã khóa" });
            cboTrangThai.SelectedIndex = 0;

            // Nạp danh sách kho phụ trách từ CSDL
            try
            {
                DataTable dtKho = DataProcessing.ReadData("SELECT MaKho, TenKho FROM Kho WHERE TrangThai = 'HOAT_DONG' ORDER BY MaKho ASC");
                if (dtKho == null)
                {
                    dtKho = new DataTable();
                    dtKho.Columns.Add("MaKho", typeof(int));
                    dtKho.Columns.Add("TenKho", typeof(string));
                }

                DataRow rowTatCa = dtKho.NewRow();
                rowTatCa["MaKho"] = 0;
                rowTatCa["TenKho"] = "Tất cả các kho";
                dtKho.Rows.InsertAt(rowTatCa, 0);

                cboKho.DataSource = dtKho;
                cboKho.DisplayMember = "TenKho";
                cboKho.ValueMember = "MaKho";
                cboKho.SelectedIndex = 0;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi nạp danh sách kho: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            if (cboSoDongMoiTrang.Items.Count > 0 && cboSoDongMoiTrang.SelectedIndex < 0)
            {
                cboSoDongMoiTrang.SelectedIndex = 1; // 20 dòng
            }
            soDongMoiTrang = LaySoDongMoiTrang();

            dangNapBoLoc = false;
        }

        private int LaySoDongMoiTrang()
        {
            int soDong;
            if (cboSoDongMoiTrang.SelectedItem != null && int.TryParse(cboSoDongMoiTrang.SelectedItem.ToString(), out soDong) && soDong > 0)
            {
                return soDong;
            }
            return 20;
        }

        public void HienThiDanhSach()
        {
            try
            {
                string sql = @"
                    SELECT tk.MaTaiKhoan, tk.TenDangNhap, nv.HoTen, nv.DienThoai,
                           tk.MaVaiTro, vt.TenVaiTro, tk.TrangThai, tk.LanDangNhapCuoi,
                           ISNULL((
                               SELECT STRING_AGG(k.TenKho, ', ')
                               FROM TaiKhoanKho tkk
                               INNER JOIN Kho k ON tkk.MaKho = k.MaKho
                               WHERE tkk.MaTaiKhoan = tk.MaTaiKhoan
                           ), N'Chưa phân công kho') AS KhoPhuTrach,
                           ',' + ISNULL((
                               SELECT STRING_AGG(CAST(tkk.MaKho AS VARCHAR(10)), ',')
                               FROM TaiKhoanKho tkk
                               WHERE tkk.MaTaiKhoan = tk.MaTaiKhoan
                           ), '') + ',' AS DanhSachMaKho
                    FROM TaiKhoan tk
                    INNER JOIN VaiTro vt ON tk.MaVaiTro = vt.MaVaiTro
                    LEFT JOIN NhanVien nv ON tk.MaNV = nv.MaNV
                    ORDER BY tk.MaTaiKhoan ASC";

                dtNguoiDung = DataProcessing.ReadData(sql);
            }
            catch (Exception ex)
            {
                // Không hiển thị dữ liệu mẫu giả khi lỗi, báo lỗi rõ ràng cho người dùng
                dtNguoiDung = null;
                MessageBox.Show("Lỗi tải danh sách người dùng: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            ApDungBoLoc();
        }

        /// <summary>
        /// Lọc theo từ khóa, vai trò, trạng thái, kho phụ trách rồi phân trang và đổ dữ liệu lên lưới
        /// </summary>
        private void ApDungBoLoc()
        {
            dgvNguoiDung.Rows.Clear();
            isHeaderChecked = false;
            dgvNguoiDung.InvalidateCell(colChon.Index, -1);

            List<DataRow> dsLoc = new List<DataRow>();
            if (dtNguoiDung != null)
            {
                string tuKhoa = txtTimKiem.Text.Trim();
                int maVaiTroLoc = cboVaiTro.SelectedIndex > 0 ? cboVaiTro.SelectedIndex : 0; // 1 = Quản lý, 2 = Thủ kho, 3 = Nhân viên kho
                int trangThaiLoc = cboTrangThai.SelectedIndex; // 1 = Đang hoạt động, 2 = Đã khóa
                int maKhoLoc = 0;
                if (cboKho.SelectedValue != null && !(cboKho.SelectedValue is DataRowView))
                {
                    int.TryParse(cboKho.SelectedValue.ToString(), out maKhoLoc);
                }

                foreach (DataRow dr in dtNguoiDung.Rows)
                {
                    int maVaiTro = dr["MaVaiTro"] != DBNull.Value ? Convert.ToInt32(dr["MaVaiTro"]) : 0;
                    string trangThai = dr["TrangThai"] != DBNull.Value ? dr["TrangThai"].ToString() : string.Empty;
                    bool dangHoatDong = string.Equals(trangThai, "HOAT_DONG", StringComparison.OrdinalIgnoreCase);

                    if (maVaiTroLoc > 0 && maVaiTro != maVaiTroLoc) continue;
                    if (trangThaiLoc == 1 && !dangHoatDong) continue;
                    if (trangThaiLoc == 2 && dangHoatDong) continue;

                    // Quản lý phụ trách mọi kho; vai trò khác lọc theo TaiKhoanKho
                    if (maKhoLoc > 0 && maVaiTro != 1)
                    {
                        string dsMaKho = dr["DanhSachMaKho"] != DBNull.Value ? dr["DanhSachMaKho"].ToString() : string.Empty;
                        if (dsMaKho.IndexOf("," + maKhoLoc + ",", StringComparison.Ordinal) < 0) continue;
                    }

                    if (!string.IsNullOrEmpty(tuKhoa))
                    {
                        bool khop = ChuaTuKhoa(dr["TenDangNhap"], tuKhoa) || ChuaTuKhoa(dr["HoTen"], tuKhoa)
                            || ChuaTuKhoa(dr["DienThoai"], tuKhoa) || ChuaTuKhoa(dr["TenVaiTro"], tuKhoa)
                            || ChuaTuKhoa(dr["KhoPhuTrach"], tuKhoa);
                        if (!khop) continue;
                    }

                    dsLoc.Add(dr);
                }
            }

            // Phân trang trong bộ nhớ
            int tongSo = dsLoc.Count;
            int tongTrang = Math.Max(1, (int)Math.Ceiling(tongSo / (double)soDongMoiTrang));
            if (trangHienTai > tongTrang) trangHienTai = tongTrang;
            if (trangHienTai < 1) trangHienTai = 1;

            int batDau = (trangHienTai - 1) * soDongMoiTrang;
            int ketThuc = Math.Min(batDau + soDongMoiTrang, tongSo);
            for (int i = batDau; i < ketThuc; i++)
            {
                DataRow dr = dsLoc[i];
                int maTk = Convert.ToInt32(dr["MaTaiKhoan"]);
                string tenDn = dr["TenDangNhap"] != DBNull.Value ? dr["TenDangNhap"].ToString() : "";
                string hoTen = dr["HoTen"] != DBNull.Value ? dr["HoTen"].ToString() : "";
                string sdt = dr["DienThoai"] != DBNull.Value ? dr["DienThoai"].ToString() : "";
                string vaiTro = dr["TenVaiTro"] != DBNull.Value ? dr["TenVaiTro"].ToString() : "";
                string kho = dr["KhoPhuTrach"] != DBNull.Value ? dr["KhoPhuTrach"].ToString() : "";
                string dangNhapCuoi = dr["LanDangNhapCuoi"] != DBNull.Value ? Convert.ToDateTime(dr["LanDangNhapCuoi"]).ToString("dd/MM/yyyy HH:mm") : "Chưa đăng nhập";
                string trangThai = dr["TrangThai"] != DBNull.Value ? dr["TrangThai"].ToString() : "HOAT_DONG";

                ThemDong(maTk, tenDn, hoTen, sdt, vaiTro, kho, dangNhapCuoi, trangThai);
            }

            lblTongSoHang.Text = $"Tổng số người dùng: {tongSo}";
            lblTrangHienTai.Text = $"Trang {trangHienTai} / {tongTrang}";
            btnTrangTruoc.Enabled = trangHienTai > 1;
            btnTrangSau.Enabled = trangHienTai < tongTrang;
            CapNhatTrangThaiHeaderCheckBox();
        }

        private static bool ChuaTuKhoa(object giaTri, string tuKhoa)
        {
            if (giaTri == null || giaTri == DBNull.Value) return false;
            return giaTri.ToString().IndexOf(tuKhoa, StringComparison.CurrentCultureIgnoreCase) >= 0;
        }

        private void BoLoc_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (dangNapBoLoc) return;
            trangHienTai = 1;
            ApDungBoLoc();
        }

        private void cboSoDongMoiTrang_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (dangNapBoLoc) return;
            soDongMoiTrang = LaySoDongMoiTrang();
            trangHienTai = 1;
            ApDungBoLoc();
        }

        private void btnTrangTruoc_Click(object sender, EventArgs e)
        {
            if (trangHienTai > 1)
            {
                trangHienTai--;
                ApDungBoLoc();
            }
        }

        private void btnTrangSau_Click(object sender, EventArgs e)
        {
            trangHienTai++;
            ApDungBoLoc();
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
            // Gắn form cha để popup tự đóng cùng danh sách người dùng
            frm.Show(this);
        }

        private void btnHuongDanSuDung_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Màn hình Quản lý người dùng cho phép:\n" +
                "- Xem danh sách tài khoản và phân quyền kho làm việc.\n" +
                "- Tìm kiếm theo tên đăng nhập, họ tên, điện thoại; lọc theo vai trò, trạng thái, kho phụ trách.\n" +
                "- Tạo mới người dùng và phân quyền kho (+ Người dùng).\n" +
                "- Nhấp đúp chuột vào một dòng để xem chi tiết, cập nhật hoặc khóa tài khoản.\n" +
                "- Tùy biến ẩn/hiện cột hiển thị qua nút 'Hiện cột'.",
                "Hướng dẫn sử dụng", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void txtTimKiem_TextChanged(object sender, EventArgs e)
        {
            // Trì hoãn 300ms để tránh lọc lại liên tục khi đang gõ
            searchDebounceTimer?.Stop();
            searchDebounceTimer?.Start();
        }

        private void txtTimKiem_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;
                searchDebounceTimer?.Stop();
                trangHienTai = 1;
                ApDungBoLoc();
            }
        }

        #endregion
    }
}

