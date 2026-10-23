using BTL_LTTQ_2.Classes;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Reflection;
using System.Text;
using System.Windows.Forms;
using System.Windows.Forms.VisualStyles;

namespace BTL_LTTQ_2.GUI.Forms.NhapHang
{
    public partial class frmNhapHang : Form
    {
        private int trangHienTai = 1;
        private int soDongMoiTrang = 20;
        private int tongSoBanGhi = 0;
        private int tongSoTrang = 1;
        private bool isUpdatingCheckAll = false;
        private bool isHeaderChecked = false;
        private bool isHeaderHovered = false;
        private Timer searchDebounceTimer;

        public frmNhapHang()
        {
            InitializeComponent();
            DinhDangLuoi();
            KhoiTaoDebounceTimer();
        }

        private void KhoiTaoDebounceTimer()
        {
            searchDebounceTimer = new Timer();
            searchDebounceTimer.Interval = 300;
            searchDebounceTimer.Tick += (s, e) =>
            {
                searchDebounceTimer.Stop();
                trangHienTai = 1;
                HienThiDanhSach();
            };
        }

        private void DinhDangLuoi()
        {
            // Bật DoubleBuffered chống rung giật lưới khi render
            typeof(DataGridView).InvokeMember("DoubleBuffered",
                BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.SetProperty,
                null, dgvNhapHang, new object[] { true });

            dgvNhapHang.AutoGenerateColumns = false;
            dgvNhapHang.ReadOnly = false;
            dgvNhapHang.EditMode = DataGridViewEditMode.EditOnEnter;
            dgvNhapHang.RowTemplate.Height = 32;
            dgvNhapHang.RowHeadersWidth = 51;

            // Màu dòng xen kẽ đồng bộ với các form đã có (frmDanhSachHangHoa, frmNhaCungCap)
            dgvNhapHang.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(248, 250, 252);
            dgvNhapHang.DefaultCellStyle.SelectionBackColor = Color.FromArgb(224, 231, 255);
            dgvNhapHang.DefaultCellStyle.SelectionForeColor = Color.FromArgb(30, 41, 59);

            // Đồng bộ độ rộng cột tiêu đề giống frmNhaCungCap
            colChon.Width = 45;
            colChon.ReadOnly = false;
            colMaNcc.Width = 140;
            colMaPhieu.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            colThoiGian.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            colMaNcc.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            colTenNcc.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
            colCanTraNcc.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            colTongTien.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            colTrangThai.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            colCanTraNcc.DefaultCellStyle.Format = "N0";
            colTongTien.DefaultCellStyle.Format = "N0";
            colThoiGian.DefaultCellStyle.Format = "dd/MM/yyyy HH:mm";

            // Đăng ký sự kiện phím Space chọn CheckBox
            dgvNhapHang.KeyDown += dgvNhapHang_KeyDown;
        }

        private void dgvNhapHang_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Space && dgvNhapHang.CurrentRow != null)
            {
                int rowIndex = dgvNhapHang.CurrentRow.Index;
                if (rowIndex >= 0 && rowIndex < dgvNhapHang.Rows.Count)
                {
                    bool cur = Convert.ToBoolean(dgvNhapHang.Rows[rowIndex].Cells["colChon"].Value ?? false);
                    dgvNhapHang.Rows[rowIndex].Cells["colChon"].Value = !cur;
                    dgvNhapHang.EndEdit();
                    CapNhatTrangThaiHeaderCheck();
                    e.Handled = true;
                }
            }
        }

        private void frmNhapHang_Load(object sender, EventArgs e)
        {
            // Thiết lập giá trị mặc định cho bộ lọc ngày
            dtpTuNgay.Value = new DateTime(DateTime.Today.Year, 1, 1);
            dtpDenNgay.Value = DateTime.Today;

            NapDanhSachTrangThai();
            NapDanhSachNhaCungCap();
            NapDanhSachNhanVien();

            cboSoDongMoiTrang.SelectedItem = "20";
            if (cboSoDongMoiTrang.SelectedIndex < 0 && cboSoDongMoiTrang.Items.Count > 0)
            {
                cboSoDongMoiTrang.SelectedIndex = 1;
            }

            HienThiDanhSach();
        }

        private void NapDanhSachTrangThai()
        {
            cboTrangThai.Items.Clear();
            cboTrangThai.Items.Add(new ComboBoxItem("Tất cả trạng thái", null));
            cboTrangThai.Items.Add(new ComboBoxItem("Phiếu tạm", "NHAP"));
            cboTrangThai.Items.Add(new ComboBoxItem("Đã nhập hàng", "DA_XAC_NHAN"));
            cboTrangThai.Items.Add(new ComboBoxItem("Đã hủy", "HUY"));
            cboTrangThai.SelectedIndex = 0;
        }

        private void NapDanhSachNhaCungCap(Action locDuLieu = null)
        {
            try
            {
                string sql = "SELECT MaDonViCapHang, TenDonVi FROM DonViCapHang WHERE TrangThai = 'HOAT_DONG' OR TrangThai = 'ACTIVE' OR TrangThai IS NULL ORDER BY TenDonVi ASC";
                DataTable dt = DataProcessing.ReadData(sql);
                DataRow drAll = dt.NewRow();
                drAll["MaDonViCapHang"] = 0;
                drAll["TenDonVi"] = "-- Táº¥t cáº£ nhÃ  cung cáº¥p --";
                dt.Rows.InsertAt(drAll, 0);

                Action action = locDuLieu ?? new Action(() =>
                {
                    trangHienTai = 1;
                    HienThiDanhSach();
                });

                ComboboxHelper.CaiDatTimKiem(cboNhaCungCap, dt, "TenDonVi", "MaDonViCapHang", action);
                cboNhaCungCap.SelectedIndex = 0;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lá»—i náº¡p danh sÃ¡ch nhÃ  cung cáº¥p: " + ex.Message, "ThÃ´ng bÃ¡o lá»—i", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void NapDanhSachNhanVien(Action locDuLieu = null)
        {
            try
            {
                string sql = "SELECT MaNV, HoTen FROM NhanVien WHERE TrangThai = 'DANG_LAM' OR TrangThai = 'HOAT_DONG' OR TrangThai IS NULL ORDER BY HoTen ASC";
                DataTable dtNvl = DataProcessing.ReadData(sql);
                DataRow drAllNvl = dtNvl.NewRow();
                drAllNvl["MaNV"] = 0;
                drAllNvl["HoTen"] = "-- Táº¥t cáº£ ngÆ°á»i táº¡o --";
                dtNvl.Rows.InsertAt(drAllNvl, 0);

                DataTable dtNvn = dtNvl.Copy();
                dtNvn.Rows[0]["HoTen"] = "-- Táº¥t cáº£ ngÆ°á»i nháº­p --";

                Action action = locDuLieu ?? new Action(() =>
                {
                    trangHienTai = 1;
                    HienThiDanhSach();
                });

                ComboboxHelper.CaiDatTimKiem(cboNguoiTao, dtNvl, "HoTen", "MaNV", action);
                cboNguoiTao.SelectedIndex = 0;

                ComboboxHelper.CaiDatTimKiem(cboNguoiNhap, dtNvn, "HoTen", "MaNV", action);
                cboNguoiNhap.SelectedIndex = 0;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lá»—i náº¡p danh sÃ¡ch nhÃ¢n viÃªn: " + ex.Message, "ThÃ´ng bÃ¡o lá»—i", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Tương thích gọi từ các form con
        public void LoadData()
        {
            HienThiDanhSach();
        }

        public void HienThiDanhSach()
        {
            try
            {
                // Thu thập tham số lọc
                string trangThai = (cboTrangThai.SelectedItem as ComboBoxItem)?.Value?.ToString();
                int? maNcc = null;
                if (cboNhaCungCap.SelectedValue != null && int.TryParse(cboNhaCungCap.SelectedValue.ToString(), out int nccVal) && nccVal > 0)
                {
                    maNcc = nccVal;
                }
                int? maNvLap = null;
                if (cboNguoiTao.SelectedValue != null && int.TryParse(cboNguoiTao.SelectedValue.ToString(), out int nvlVal) && nvlVal > 0)
                {
                    maNvLap = nvlVal;
                }
                int? maNvNhap = null;
                if (cboNguoiNhap.SelectedValue != null && int.TryParse(cboNguoiNhap.SelectedValue.ToString(), out int nvnVal) && nvnVal > 0)
                {
                    maNvNhap = nvnVal;
                }
                DateTime tuNgay = dtpTuNgay.Value.Date;
                DateTime denNgay = dtpDenNgay.Value.Date.AddDays(1);
                string timKiem = txtTimKiem.Text.Trim();

                string countSql = @"
                    SELECT COUNT(1)
                    FROM PhieuNhap pn
                    LEFT JOIN DonViCapHang ncc ON pn.MaDonViCapHang = ncc.MaDonViCapHang
                    WHERE (@TrangThai IS NULL OR pn.TrangThai = @TrangThai)
                      AND (@MaNcc IS NULL OR pn.MaDonViCapHang = @MaNcc)
                      AND (@MaNVLap IS NULL OR pn.MaNVLap = @MaNVLap)
                      AND (@MaNVNhap IS NULL OR pn.MaNVXacNhan = @MaNVNhap)
                      AND (pn.NgayLap >= @TuNgay AND pn.NgayLap < @DenNgay)
                      AND (
                          @TimKiem = ''
                          OR pn.MaPhieu LIKE @TimKiemLike
                          OR pn.SoHoaDon LIKE @TimKiemLike
                          OR ncc.TenDonVi LIKE @TimKiemLike
                          OR ncc.MaNcc LIKE @TimKiemLike
                          OR pn.GhiChu LIKE @TimKiemLike
                      )";

                SqlParameter[] countParams = new SqlParameter[]
                {
                    new SqlParameter("@TrangThai", (object)trangThai ?? DBNull.Value),
                    new SqlParameter("@MaNcc", (object)maNcc ?? DBNull.Value),
                    new SqlParameter("@MaNVLap", (object)maNvLap ?? DBNull.Value),
                    new SqlParameter("@MaNVNhap", (object)maNvNhap ?? DBNull.Value),
                    new SqlParameter("@TuNgay", tuNgay),
                    new SqlParameter("@DenNgay", denNgay),
                    new SqlParameter("@TimKiem", timKiem),
                    new SqlParameter("@TimKiemLike", "%" + timKiem + "%")
                };

                object countObj = DataProcessing.ReadValue(countSql, countParams);
                tongSoBanGhi = Convert.ToInt32(countObj);
                tongSoTrang = (int)Math.Ceiling((double)tongSoBanGhi / soDongMoiTrang);
                if (tongSoTrang < 1) tongSoTrang = 1;
                if (trangHienTai > tongSoTrang) trangHienTai = tongSoTrang;

                int skip = (trangHienTai - 1) * soDongMoiTrang;

                string querySql = @"
                    SELECT 
                        CAST(0 AS BIT) AS Chon,
                        pn.MaPhieuNhap,
                        ISNULL(pn.MaPhieu, 'PN' + RIGHT('000000' + CAST(pn.MaPhieuNhap AS VARCHAR(10)), 6)) AS MaPhieu,
                        pn.NgayLap,
                        ISNULL(ncc.MaNcc, 'NCC' + RIGHT('00000' + CAST(ISNULL(ncc.MaDonViCapHang, 0) AS VARCHAR(10)), 5)) AS MaNccHienThi,
                        ISNULL(ncc.TenDonVi, N'Nhà cung cấp lẻ') AS TenDonVi,
                        pn.CanTraNcc,
                        pn.TongTien,
                        pn.TrangThai,
                        CASE pn.TrangThai 
                            WHEN 'NHAP' THEN N'Phiếu tạm'
                            WHEN 'DA_XAC_NHAN' THEN N'Đã nhập hàng'
                            WHEN 'HUY' THEN N'Đã hủy'
                            ELSE pn.TrangThai 
                        END AS TenTrangThai
                    FROM PhieuNhap pn
                    LEFT JOIN DonViCapHang ncc ON pn.MaDonViCapHang = ncc.MaDonViCapHang
                    WHERE (@TrangThai IS NULL OR pn.TrangThai = @TrangThai)
                      AND (@MaNcc IS NULL OR pn.MaDonViCapHang = @MaNcc)
                      AND (@MaNVLap IS NULL OR pn.MaNVLap = @MaNVLap)
                      AND (@MaNVNhap IS NULL OR pn.MaNVXacNhan = @MaNVNhap)
                      AND (pn.NgayLap >= @TuNgay AND pn.NgayLap < @DenNgay)
                      AND (
                          @TimKiem = ''
                          OR pn.MaPhieu LIKE @TimKiemLike
                          OR pn.SoHoaDon LIKE @TimKiemLike
                          OR ncc.TenDonVi LIKE @TimKiemLike
                          OR ncc.MaNcc LIKE @TimKiemLike
                          OR pn.GhiChu LIKE @TimKiemLike
                      )
                    ORDER BY pn.NgayLap DESC, pn.MaPhieuNhap DESC
                    OFFSET @Skip ROWS FETCH NEXT @Take ROWS ONLY";

                SqlParameter[] queryParams = new SqlParameter[]
                {
                    new SqlParameter("@TrangThai", (object)trangThai ?? DBNull.Value),
                    new SqlParameter("@MaNcc", (object)maNcc ?? DBNull.Value),
                    new SqlParameter("@MaNVLap", (object)maNvLap ?? DBNull.Value),
                    new SqlParameter("@MaNVNhap", (object)maNvNhap ?? DBNull.Value),
                    new SqlParameter("@TuNgay", tuNgay),
                    new SqlParameter("@DenNgay", denNgay),
                    new SqlParameter("@TimKiem", timKiem),
                    new SqlParameter("@TimKiemLike", "%" + timKiem + "%"),
                    new SqlParameter("@Skip", skip),
                    new SqlParameter("@Take", soDongMoiTrang)
                };

                DataTable dt = DataProcessing.ReadData(querySql, queryParams);

                if (!dt.Columns.Contains("Chon"))
                {
                    DataColumn col = new DataColumn("Chon", typeof(bool));
                    col.DefaultValue = false;
                    dt.Columns.Add(col);
                }

                dgvNhapHang.DataSource = dt;

                CapNhatThanhPhanTrang();
                isHeaderChecked = false;
                dgvNhapHang.Invalidate();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi nạp dữ liệu phiếu nhập hàng: " + ex.Message, "Thông báo lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void CapNhatThanhPhanTrang()
        {
            int tuDong = tongSoBanGhi == 0 ? 0 : (trangHienTai - 1) * soDongMoiTrang + 1;
            int denDong = Math.Min(trangHienTai * soDongMoiTrang, tongSoBanGhi);
            lblTongSoPhieu.Text = $"Hiển thị {tuDong:N0} - {denDong:N0} trên tổng số {tongSoBanGhi:N0} phiếu nhập";
            lblTrangHienTai.Text = $"Trang {trangHienTai} / {tongSoTrang}";
            btnTrangTruoc.Enabled = trangHienTai > 1;
            btnTrangSau.Enabled = trangHienTai < tongSoTrang;
        }

        private void BoLoc_SelectedIndexChanged(object sender, EventArgs e)
        {
            trangHienTai = 1;
            HienThiDanhSach();
        }

        private void txtTimKiem_TextChanged(object sender, EventArgs e)
        {
            searchDebounceTimer.Stop();
            searchDebounceTimer.Start();
        }

        private void cboSoDongMoiTrang_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (int.TryParse(cboSoDongMoiTrang.SelectedItem?.ToString(), out int val))
            {
                soDongMoiTrang = val;
                trangHienTai = 1;
                HienThiDanhSach();
            }
        }

        private void btnTrangTruoc_Click(object sender, EventArgs e)
        {
            if (trangHienTai > 1)
            {
                trangHienTai--;
                HienThiDanhSach();
            }
        }

        private void btnTrangSau_Click(object sender, EventArgs e)
        {
            if (trangHienTai < tongSoTrang)
            {
                trangHienTai++;
                HienThiDanhSach();
            }
        }

        private void btnThemNhapHang_Click(object sender, EventArgs e)
        {
            using (frmThemPhieuNhap frm = new frmThemPhieuNhap())
            {
                if (frm.ShowDialog(this) == DialogResult.OK)
                {
                    HienThiDanhSach();
                }
            }
        }

        private void dgvNhapHang_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;
            if (dgvNhapHang.Columns[e.ColumnIndex].Name == "colChon") return;

            int maPhieuNhap = Convert.ToInt32(dgvNhapHang.Rows[e.RowIndex].Cells["colMaPhieuNhap"].Value);
            using (frmChiTietPhieuNhap frm = new frmChiTietPhieuNhap(maPhieuNhap))
            {
                frm.ShowDialog(this);
                HienThiDanhSach();
            }
        }

        private void btnXuatFile_Click(object sender, EventArgs e)
        {
            using (SaveFileDialog sfd = new SaveFileDialog())
            {
                sfd.Filter = "File CSV (*.csv)|*.csv|Tất cả tệp (*.*)|*.*";
                sfd.FileName = $"DanhSachNhapHang_{DateTime.Now:yyyyMMdd_HHmm}.csv";

                if (sfd.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        StringBuilder sb = new StringBuilder();
                        sb.AppendLine("Mã nhập hàng,Thời gian,Mã NCC,Tên NCC,Cần trả NCC,Tổng tiền,Trạng thái");

                        DataTable dt = dgvNhapHang.DataSource as DataTable;
                        if (dt != null)
                        {
                            foreach (DataRow row in dt.Rows)
                            {
                                string maPhieu = row["MaPhieu"]?.ToString() ?? "";
                                string thoiGian = Convert.ToDateTime(row["NgayLap"]).ToString("dd/MM/yyyy HH:mm");
                                string maNcc = row["MaNccHienThi"]?.ToString() ?? "";
                                string tenNcc = $"\"{row["TenDonVi"]?.ToString().Replace("\"", "\"\"")}\"";
                                decimal canTra = Convert.ToDecimal(row["CanTraNcc"]);
                                decimal tongTien = Convert.ToDecimal(row["TongTien"]);
                                string trangThai = row["TenTrangThai"]?.ToString() ?? "";

                                sb.AppendLine($"{maPhieu},{thoiGian},{maNcc},{tenNcc},{canTra},{tongTien},{trangThai}");
                            }
                        }

                        // Ghi với UTF-8 with BOM để Excel đọc không lỗi font tiếng Việt
                        File.WriteAllText(sfd.FileName, sb.ToString(), new UTF8Encoding(true));
                        MessageBox.Show("Xuất file dữ liệu thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Lỗi xuất file: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        private void btnHienCot_Click(object sender, EventArgs e)
        {
            foreach (Form openForm in Application.OpenForms)
            {
                if (openForm is frmHienCotNhapHang)
                {
                    openForm.Activate();
                    openForm.BringToFront();
                    return;
                }
            }

            frmHienCotNhapHang frm = new frmHienCotNhapHang(dgvNhapHang);
            frm.StartPosition = FormStartPosition.Manual;

            Point btnPos = btnHienCot.PointToScreen(Point.Empty);
            int posX = btnPos.X + btnHienCot.Width - frm.Width;
            int posY = btnPos.Y + btnHienCot.Height + 2;

            Rectangle workingArea = Screen.FromControl(btnHienCot).WorkingArea;
            if (posX + frm.Width > workingArea.Right) posX = workingArea.Right - frm.Width;
            if (posX < workingArea.Left) posX = workingArea.Left;
            if (posY + frm.Height > workingArea.Bottom) posY = btnPos.Y - frm.Height - 2;

            frm.Location = new Point(posX, posY);
            frm.Show(this);
            frm.BringToFront();
        }

        private void btnHuongDanSuDung_Click(object sender, EventArgs e)
        {
            string huongDan = "HƯỚNG DẪN QUẢN LÝ NHẬP HÀNG:\n\n" +
                              "1. Tìm kiếm: Nhập mã phiếu, mã hóa đơn hoặc tên nhà cung cấp vào ô Tìm kiếm trên đầu.\n" +
                              "2. Bộ lọc: Chọn Trạng thái, Nhà cung cấp, Người tạo, Người nhập hoặc Khoảng thời gian ở bảng bên trái.\n" +
                              "3. Lập phiếu nhập mới: Nhấp vào nút '+ Nhập hàng'.\n" +
                              "4. Xem chi tiết / Trả hàng: Nhấp đúp chuột (Double click) vào bất kỳ dòng đơn nhập nào trên bảng.\n" +
                              "5. Phân trang: Nhấp nút 'Trước' / 'Sau' hoặc chọn lại số dòng mỗi trang ở góc dưới bảng.\n" +
                              "6. Xuất file: Nhấp 'Xuất file' để lưu danh sách ra file CSV mở bằng Excel.\n" +
                              "7. Hiện/Ẩn cột: Nhấp nút 'Hiện cột' để bật/tắt các cột hiển thị trên bảng.";

            MessageBox.Show(huongDan, "Hướng dẫn sử dụng", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void dgvNhapHang_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.RowIndex < 0) return;

            // Bôi đỏ nếu còn nợ cần trả NCC
            if (dgvNhapHang.Columns[e.ColumnIndex].Name == "colCanTraNcc" && e.Value != null)
            {
                if (decimal.TryParse(e.Value.ToString(), out decimal canTra) && canTra > 0)
                {
                    e.CellStyle.ForeColor = Color.FromArgb(220, 38, 38);
                    e.CellStyle.Font = new Font(dgvNhapHang.Font, FontStyle.Bold);
                }
            }

            // Đổi màu hiển thị trạng thái
            if (dgvNhapHang.Columns[e.ColumnIndex].Name == "colTrangThai" && e.Value != null)
            {
                string tt = e.Value.ToString();
                if (tt == "Đã nhập hàng")
                {
                    e.CellStyle.ForeColor = Color.FromArgb(22, 163, 74);
                }
                else if (tt == "Phiếu tạm")
                {
                    e.CellStyle.ForeColor = Color.FromArgb(37, 99, 235);
                }
                else if (tt == "Đã hủy")
                {
                    e.CellStyle.ForeColor = Color.FromArgb(100, 116, 139);
                }
            }
        }

        private void dgvNhapHang_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        {
            if (e.RowIndex == -1 && e.ColumnIndex >= 0 && dgvNhapHang.Columns[e.ColumnIndex].Name == "colChon")
            {
                e.PaintBackground(e.CellBounds, true);
                e.Paint(e.CellBounds, DataGridViewPaintParts.Border);

                CheckBoxState state = isHeaderChecked
                    ? (isHeaderHovered ? CheckBoxState.CheckedHot : CheckBoxState.CheckedNormal)
                    : (isHeaderHovered ? CheckBoxState.UncheckedHot : CheckBoxState.UncheckedNormal);

                Size glyphSize = CheckBoxRenderer.GetGlyphSize(e.Graphics, state);
                Point pt = new Point(
                    e.CellBounds.Left + (e.CellBounds.Width - glyphSize.Width) / 2,
                    e.CellBounds.Top + (e.CellBounds.Height - glyphSize.Height) / 2
                );

                CheckBoxRenderer.DrawCheckBox(e.Graphics, pt, state);
                e.Handled = true;
            }
        }

        private void dgvNhapHang_CellMouseMove(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.RowIndex == -1 && e.ColumnIndex >= 0 && dgvNhapHang.Columns[e.ColumnIndex].Name == "colChon")
            {
                if (!isHeaderHovered)
                {
                    isHeaderHovered = true;
                    dgvNhapHang.InvalidateCell(e.ColumnIndex, -1);
                }
            }
        }

        private void dgvNhapHang_CellMouseLeave(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex == -1 && e.ColumnIndex >= 0 && dgvNhapHang.Columns[e.ColumnIndex].Name == "colChon")
            {
                if (isHeaderHovered)
                {
                    isHeaderHovered = false;
                    dgvNhapHang.InvalidateCell(e.ColumnIndex, -1);
                }
            }
        }

        private void dgvNhapHang_ColumnHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.ColumnIndex >= 0 && dgvNhapHang.Columns[e.ColumnIndex].Name == "colChon")
            {
                dgvNhapHang.EndEdit();
                dgvNhapHang.CurrentCell = null;

                isHeaderChecked = !isHeaderChecked;
                isUpdatingCheckAll = true;

                DataTable dt = dgvNhapHang.DataSource as DataTable;
                if (dt != null)
                {
                    foreach (DataRow row in dt.Rows)
                    {
                        row["Chon"] = isHeaderChecked;
                    }
                }

                isUpdatingCheckAll = false;
                dgvNhapHang.Invalidate();
            }
        }

        private void dgvNhapHang_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (isUpdatingCheckAll) return;
            if (e.RowIndex >= 0 && e.ColumnIndex >= 0 && dgvNhapHang.Columns[e.ColumnIndex].Name == "colChon")
            {
                CapNhatTrangThaiHeaderCheck();
            }
        }

        private void dgvNhapHang_CurrentCellDirtyStateChanged(object sender, EventArgs e)
        {
            if (dgvNhapHang.IsCurrentCellDirty && dgvNhapHang.CurrentCell.ColumnIndex == colChon.Index)
            {
                dgvNhapHang.CommitEdit(DataGridViewDataErrorContexts.Commit);
            }
        }

        private void dgvNhapHang_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.ColumnIndex == colChon.Index)
            {
                dgvNhapHang.CommitEdit(DataGridViewDataErrorContexts.Commit);
            }
        }

        private void CapNhatTrangThaiHeaderCheck()
        {
            DataTable dt = dgvNhapHang.DataSource as DataTable;
            if (dt == null || dt.Rows.Count == 0)
            {
                isHeaderChecked = false;
            }
            else
            {
                bool allChecked = true;
                foreach (DataRow row in dt.Rows)
                {
                    if (row["Chon"] == DBNull.Value || !Convert.ToBoolean(row["Chon"]))
                    {
                        allChecked = false;
                        break;
                    }
                }
                isHeaderChecked = allChecked;
            }
            dgvNhapHang.InvalidateCell(colChon.Index, -1);
        }

        private void dgvNhapHang_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {
            e.Cancel = true;
            e.ThrowException = false;
        }

        private class ComboBoxItem
        {
            public string Text { get; }
            public object Value { get; }

            public ComboBoxItem(string text, object value)
            {
                Text = text;
                Value = value;
            }

            public override string ToString() => Text;
        }
    }
}
