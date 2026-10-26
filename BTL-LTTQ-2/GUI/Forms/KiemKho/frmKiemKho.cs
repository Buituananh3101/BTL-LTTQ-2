using BTL_LTTQ_2.Classes;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Text;
using System.Windows.Forms;
using System.Windows.Forms.VisualStyles;

namespace BTL_LTTQ_2.GUI.Forms.KiemKho
{
    public partial class frmKiemKho : Form
    {
        private int trangHienTai = 1;
        private int soDongMoiTrang = 20;
        private int tongSoTrang = 1;
        private int tongSoBanGhi = 0;
        private bool isInitializing = true;
        private DataTable dtDanhSachPhieu;
        // Truy vấn đầy đủ (không phân trang) của lần tải gần nhất, dùng cho Xuất file
        private string sqlXuatToanBo = null;
        private List<SqlParameter> thamSoXuatToanBo = null;
        private bool isHeaderChecked = false;

        private Timer searchDebounceTimer;

        public frmKiemKho()
        {
            InitializeComponent();
            CauHinhDataGridView();
            KhoiTaoDebounceTimer();
            if (txtTimKiem != null)
            {
                txtTimKiem.KeyDown += txtTimKiem_KeyDown;
            }
        }

        private void KhoiTaoDebounceTimer()
        {
            searchDebounceTimer = new Timer();
            searchDebounceTimer.Interval = 300;
            searchDebounceTimer.Tick += (s, e) =>
            {
                searchDebounceTimer.Stop();
                if (!isInitializing)
                {
                    trangHienTai = 1;
                    TaiDuLieu();
                }
            };
        }

        private void txtTimKiem_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;
                searchDebounceTimer?.Stop();
                if (!isInitializing)
                {
                    trangHienTai = 1;
                    TaiDuLieu();
                }
            }
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (keyData == Keys.F3)
            {
                if (txtTimKiem != null && txtTimKiem.CanFocus)
                {
                    txtTimKiem.Focus();
                    txtTimKiem.SelectAll();
                    return true;
                }
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }

        private void CauHinhDataGridView()
        {
            // Bật DoubleBuffered chống rung nhấp nháy lưới
            typeof(DataGridView).InvokeMember("DoubleBuffered",
                System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.SetProperty,
                null, dgvKiemKho, new object[] { true });

            dgvKiemKho.AutoGenerateColumns = false;
            dgvKiemKho.BorderStyle = BorderStyle.None;
            dgvKiemKho.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(248, 250, 252);
            dgvKiemKho.DefaultCellStyle.SelectionBackColor = Color.FromArgb(224, 231, 255);
            dgvKiemKho.DefaultCellStyle.SelectionForeColor = Color.FromArgb(30, 41, 59);

            colChon.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            colChon.SortMode = DataGridViewColumnSortMode.NotSortable;
            colChon.Resizable = DataGridViewTriState.False;
            colMaPhieu.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            colThoiGian.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            colNgayCanBang.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            colTrangThai.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            colTongThucTe.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            colTongChenhLech.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            colSlLechTang.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            colSlLechGiam.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;

            // Cấu hình co giãn các cột tự động lấp đầy DataGridView theo tỷ lệ
            colMaPhieu.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            colMaPhieu.FillWeight = 115;
            colNgayCanBang.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            colNgayCanBang.FillWeight = 135;
            colSlLechTang.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            colSlLechTang.FillWeight = 95;
            colSlLechGiam.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            colSlLechGiam.FillWeight = 95;
            colSlThucTe.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            colSlThucTe.FillWeight = 95;
            colGhiChu.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            colGhiChu.FillWeight = 150;
            colTongThucTe.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            colTongThucTe.FillWeight = 130;
            colTongChenhLech.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            colTongChenhLech.FillWeight = 130;
            colThoiGian.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            colThoiGian.FillWeight = 135;
            colTrangThai.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            colTrangThai.FillWeight = 120;
            colSlThucTe.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;

            colTongThucTe.DefaultCellStyle.Format = "N0";
            colTongChenhLech.DefaultCellStyle.Format = "N0";
            colSlLechTang.DefaultCellStyle.Format = "N0";
            colSlLechGiam.DefaultCellStyle.Format = "N0";
            colSlThucTe.DefaultCellStyle.Format = "N0";

            dgvKiemKho.CellFormatting += dgvKiemKho_CellFormatting;
        }

        private void dgvKiemKho_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.RowIndex < 0 || e.RowIndex >= dgvKiemKho.Rows.Count) return;

            string colName = dgvKiemKho.Columns[e.ColumnIndex].Name;

            if (colName == "colTrangThai" && e.Value != null)
            {
                string tt = e.Value.ToString();
                if (tt == "Đã cân bằng")
                {
                    e.CellStyle.ForeColor = Color.FromArgb(22, 163, 74);
                }
                else if (tt == "Phiếu tạm")
                {
                    e.CellStyle.ForeColor = Color.FromArgb(217, 119, 6);
                }
                else if (tt == "Đã hủy")
                {
                    e.CellStyle.ForeColor = Color.FromArgb(100, 116, 139);
                }
            }
            if (colName == "colTongChenhLech" && e.RowIndex >= 0 && e.RowIndex < dgvKiemKho.Rows.Count)
            {
                DataRowView drv = dgvKiemKho.Rows[e.RowIndex].DataBoundItem as DataRowView;
                if (drv != null)
                {
                    decimal slTang = drv["SlLechTang"] != DBNull.Value ? Convert.ToDecimal(drv["SlLechTang"]) : 0;
                    decimal slGiam = drv["SlLechGiam"] != DBNull.Value ? Convert.ToDecimal(drv["SlLechGiam"]) : 0;
                    decimal giaTri = drv["TongGiaTriChenhLech"] != DBNull.Value ? Convert.ToDecimal(drv["TongGiaTriChenhLech"]) : 0;
                    if (slTang > 0 || slGiam > 0 || giaTri != 0)
                    {
                        e.CellStyle.ForeColor = Color.Red;
                        e.CellStyle.Font = new Font(dgvKiemKho.Font, FontStyle.Bold);
                    }
                }
            }
            else if ((colName == "colSlLechTang" || colName == "colSlLechGiam") && e.Value != null)
            {
                if (decimal.TryParse(e.Value.ToString(), out decimal val) && val != 0)
                {
                    e.CellStyle.ForeColor = Color.Red;
                    e.CellStyle.Font = new Font(dgvKiemKho.Font, FontStyle.Bold);
                }
            }
        }

        private void frmKiemKho_Load(object sender, EventArgs e)
        {
            isInitializing = true;

            // Khởi tạo thời gian: Từ ngày đầu tháng tới cuối ngày hôm nay
            dtpTuNgay.Value = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
            dtpDenNgay.Value = DateTime.Now;

            // Nạp ComboBox phân trang
            cboSoDongMoiTrang.SelectedItem = "20";

            // Nạp các bộ lọc ComboBox
            NapBoLocTrangThai();
            NapBoLocNguoiTao();


            isInitializing = false;
            TaiDuLieu();
        }

        #region Nạp Danh Sách Bộ Lọc

        private void NapBoLocTrangThai()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("MaTrangThai", typeof(string));
            dt.Columns.Add("TenTrangThai", typeof(string));

            dt.Rows.Add("", "-- Tất cả trạng thái --");
            dt.Rows.Add("DA_DIEU_CHINH", "Đã cân bằng");
            dt.Rows.Add("NHAP", "Phiếu tạm");

            cboTrangThai.DisplayMember = "TenTrangThai";
            cboTrangThai.ValueMember = "MaTrangThai";
            cboTrangThai.DataSource = dt;
            cboTrangThai.SelectedIndex = 0;
        }

        private void NapBoLocNguoiTao()
        {
            try
            {
                // Lọc theo người tạo: giá trị là NhanVien.MaNV (khớp cột PhieuKiemKe.MaNVLap)
                string sql = @"
                    SELECT
                        0 AS MaNV,
                        N'-- Tất cả người tạo --' AS HoTen
                    UNION ALL
                    SELECT
                        nv.MaNV,
                        nv.HoTen
                    FROM NhanVien nv
                    WHERE nv.TrangThai = 'DANG_LAM'";

                DataTable dt = DataProcessing.ReadData(sql);
                Action callbackNguoiTao = () => { trangHienTai = 1; TaiDuLieu(); };
                ComboboxHelper.CaiDatTimKiem(cboNguoiTao, dt, "HoTen", "MaNV", callbackNguoiTao);
                Action dummy = () =>
                {
                    trangHienTai = 1;
                    TaiDuLieu();
                };
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi nạp danh sách người tạo: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void NapBoLocKho_Disabled()
        {
            try
            {
                string sql = @"
                    SELECT 0 AS MaKho, N'-- Tất cả chi nhánh kho --' AS TenKho
                    UNION ALL
                    SELECT MaKho, TenKho FROM Kho 
                    WHERE TrangThai = 'HOAT_DONG' OR TrangThai = 'ACTIVE' OR TrangThai IS NULL";

                DataTable dt = DataProcessing.ReadData(sql);
                Action loc = () =>
                {
                    trangHienTai = 1;
                    TaiDuLieu();
                };
                return;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi nạp danh sách kho: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        #endregion

        #region Tải Dữ Liệu & Phân Trang

        public void TaiDuLieu()
        {
            if (isInitializing) return;

            try
            {
                Cursor.Current = Cursors.WaitCursor;

                // 1. Xây dựng điều kiện lọc an toàn với SqlParameter
                string whereClause = " WHERE 1 = 1 ";
                List<SqlParameter> listParams = new List<SqlParameter>();

                // Tìm kiếm từ khóa theo mã phiếu hoặc ghi chú
                string tuKhoa = txtTimKiem.Text.Trim();
                if (!string.IsNullOrEmpty(tuKhoa))
                {
                    whereClause += " AND (pkk.MaPhieu LIKE @TuKhoa OR pkk.GhiChu LIKE @TuKhoa) ";
                    listParams.Add(new SqlParameter("@TuKhoa", "%" + tuKhoa + "%"));
                }

                // Lọc theo khoảng thời gian tạo
                DateTime tuNgay = dtpTuNgay.Value.Date;
                DateTime denNgay = dtpDenNgay.Value.Date.AddDays(1).AddSeconds(-1);
                whereClause += " AND (pkk.ThoiDiemChot BETWEEN @TuNgay AND @DenNgay) ";
                listParams.Add(new SqlParameter("@TuNgay", tuNgay));
                listParams.Add(new SqlParameter("@DenNgay", denNgay));

                // Lọc theo trạng thái
                if (cboTrangThai.SelectedValue != null)
                {
                    string tt = cboTrangThai.SelectedValue.ToString();
                    if (!string.IsNullOrEmpty(tt))
                    {
                        if (tt == "NHAP")
                        {
                            whereClause += " AND (pkk.TrangThai = 'NHAP' OR pkk.TrangThai = 'DANG_KIEM' OR pkk.TrangThai = 'DA_KIEM') ";
                        }
                        else
                        {
                            whereClause += " AND pkk.TrangThai = @TrangThai ";
                            listParams.Add(new SqlParameter("@TrangThai", tt));
                        }
                    }
                }

                // Lọc theo người tạo
                if (cboNguoiTao.SelectedValue != null && int.TryParse(cboNguoiTao.SelectedValue.ToString(), out int maNv) && maNv > 0)
                {
                    whereClause += " AND pkk.MaNVLap = @MaNVLap ";
                    listParams.Add(new SqlParameter("@MaNVLap", maNv));
                }

                // Lọc theo kho
                if (SessionContext.MaKhoHienTai > 0)
                {
                    whereClause += " AND pkk.MaKho = @MaKho ";
                    listParams.Add(new SqlParameter("@MaKho", SessionContext.MaKhoHienTai));
                }

                // 2. Đếm tổng số bản ghi
                string sqlCount = $"SELECT COUNT(*) FROM PhieuKiemKe pkk {whereClause}";
                SqlParameter[] countParams = SaoChepThamSo(listParams);
                object countVal = DataProcessing.ReadValue(sqlCount, countParams);
                tongSoBanGhi = (countVal != null && countVal != DBNull.Value) ? Convert.ToInt32(countVal) : 0;

                // 3. Tính phân trang
                if (soDongMoiTrang <= 0) soDongMoiTrang = 20;
                tongSoTrang = (int)Math.Ceiling((double)tongSoBanGhi / soDongMoiTrang);
                if (tongSoTrang < 1) tongSoTrang = 1;
                if (trangHienTai > tongSoTrang) trangHienTai = tongSoTrang;
                if (trangHienTai < 1) trangHienTai = 1;

                int skip = (trangHienTai - 1) * soDongMoiTrang;

                // 4. Truy vấn lấy dữ liệu chi tiết của trang hiện tại
                string sqlData = $@"
                    SELECT 
                        pkk.MaKho,
                        ISNULL(k.TenKho, N'') AS TenKho,
                        pkk.MaPhieuKiemKe,
                        ISNULL(pkk.MaPhieu, 'PKK' + RIGHT('000000' + CAST(pkk.MaPhieuKiemKe AS VARCHAR(10)), 6)) AS MaPhieu,
                        pkk.ThoiDiemChot,
                        CONVERT(VARCHAR(16), pkk.ThoiDiemChot, 120) AS ThoiGian,
                        pkk.TrangThai,
                        CASE 
                            WHEN pkk.TrangThai = 'DA_DIEU_CHINH' THEN N'Đã cân bằng'
                            WHEN pkk.TrangThai = 'HUY' THEN N'Đã hủy'
                            ELSE N'Phiếu tạm'
                        END AS TenTrangThai,
                        ISNULL(pkk.GhiChu, '') AS GhiChu,
                        pdc.NgayDuyet,
                        CASE 
                            WHEN pkk.TrangThai = 'DA_DIEU_CHINH' THEN 
                                ISNULL(CONVERT(VARCHAR(16), pdc.NgayDuyet, 120), CONVERT(VARCHAR(16), pkk.ThoiDiemChot, 120))
                            ELSE ''
                        END AS NgayCanBang,
                        -- Tính các chỉ số thực tế và chênh lệch từ ChiTietPhieuKiemKe
                        ISNULL(ct.TongSlThucTe, 0) AS TongSlThucTe,
                        ISNULL(ct.TongGiaTriThucTe, 0) AS TongGiaTriThucTe,
                        ISNULL(ct.SlLechTang, 0) AS SlLechTang,
                        ISNULL(ct.SlLechGiam, 0) AS SlLechGiam,
                        ISNULL(ct.TongGiaTriChenhLech, 0) AS TongGiaTriChenhLech
                    FROM PhieuKiemKe pkk
                    LEFT JOIN Kho k ON pkk.MaKho = k.MaKho
                    LEFT JOIN (
                        SELECT MaPhieuKiemKe, MAX(NgayDuyet) AS NgayDuyet
                        FROM PhieuDieuChinh
                        WHERE TrangThai = 'DA_DUYET'
                        GROUP BY MaPhieuKiemKe
                    ) pdc ON pdc.MaPhieuKiemKe = pkk.MaPhieuKiemKe
                    LEFT JOIN (
                        SELECT 
                            MaPhieuKiemKe,
                            SUM(CASE WHEN SoLuongThucTe IS NOT NULL THEN SoLuongThucTe ELSE 0 END) AS TongSlThucTe,
                            SUM(CASE WHEN SoLuongThucTe IS NOT NULL THEN SoLuongThucTe * DonGia ELSE 0 END) AS TongGiaTriThucTe,
                            SUM(CASE WHEN SoLuongThucTe IS NOT NULL AND SoLuongThucTe > SoLuongTheoSo THEN (SoLuongThucTe - SoLuongTheoSo) ELSE 0 END) AS SlLechTang,
                            SUM(CASE WHEN SoLuongThucTe IS NOT NULL AND SoLuongThucTe < SoLuongTheoSo THEN (SoLuongTheoSo - SoLuongThucTe) ELSE 0 END) AS SlLechGiam,
                            SUM(CASE WHEN SoLuongThucTe IS NOT NULL THEN (SoLuongThucTe - SoLuongTheoSo) * DonGia ELSE 0 END) AS TongGiaTriChenhLech
                        FROM ChiTietPhieuKiemKe
                        GROUP BY MaPhieuKiemKe
                    ) ct ON ct.MaPhieuKiemKe = pkk.MaPhieuKiemKe
                    {whereClause}
                    ORDER BY pkk.ThoiDiemChot DESC, pkk.MaPhieuKiemKe DESC
                    OFFSET @Skip ROWS FETCH NEXT @Take ROWS ONLY";

                // Lưu truy vấn không phân trang để "Xuất file" lấy toàn bộ phiếu khớp bộ lọc
                sqlXuatToanBo = sqlData.Replace("OFFSET @Skip ROWS FETCH NEXT @Take ROWS ONLY", "");
                thamSoXuatToanBo = new List<SqlParameter>();
                foreach (SqlParameter p in listParams) thamSoXuatToanBo.Add(new SqlParameter(p.ParameterName, p.Value));

                listParams.Add(new SqlParameter("@Skip", skip));
                listParams.Add(new SqlParameter("@Take", soDongMoiTrang));

                dtDanhSachPhieu = DataProcessing.ReadData(sqlData, listParams.ToArray());

                // Thêm cột logic Chon vào DataTable để DataGridViewCheckBoxColumn bind chuẩn xác
                if (!dtDanhSachPhieu.Columns.Contains("Chon"))
                {
                    dtDanhSachPhieu.Columns.Add("Chon", typeof(bool));
                    dtDanhSachPhieu.Columns["Chon"].DefaultValue = false;
                    foreach (DataRow r in dtDanhSachPhieu.Rows)
                    {
                        r["Chon"] = false;
                    }
                }
                dgvKiemKho.DataSource = dtDanhSachPhieu;

                // Reset trạng thái Header CheckBox khi nạp trang mới
                isHeaderChecked = false;
                if (dgvKiemKho.Columns.Contains("colChon"))
                {
                    dgvKiemKho.InvalidateCell(dgvKiemKho.Columns["colChon"].Index, -1);
                }

                // 5. Cập nhật nhãn phân trang
                lblTongSoPhieu.Text = $"Tổng số: {tongSoBanGhi:N0} phiếu kiểm";
                lblTrangHienTai.Text = $"Trang {trangHienTai} / {tongSoTrang}";
                btnTrangTruoc.Enabled = (trangHienTai > 1);
                btnTrangSau.Enabled = (trangHienTai < tongSoTrang);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi tải dữ liệu phiếu kiểm kho: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                Cursor.Current = Cursors.Default;
            }
        }

        private SqlParameter[] SaoChepThamSo(List<SqlParameter> list)
        {
            SqlParameter[] arr = new SqlParameter[list.Count];
            for (int i = 0; i < list.Count; i++)
            {
                arr[i] = new SqlParameter(list[i].ParameterName, list[i].Value);
            }
            return arr;
        }

        private void BoLoc_ThayDoi(object sender, EventArgs e)
        {
            if (isInitializing) return;
            trangHienTai = 1;
            TaiDuLieu();
        }

        private void txtTimKiem_TextChanged(object sender, EventArgs e)
        {
            searchDebounceTimer?.Stop();
            searchDebounceTimer?.Start();
        }

        #endregion

        #region Xử Lý Phân Trang

        private void btnTrangTruoc_Click(object sender, EventArgs e)
        {
            if (trangHienTai > 1)
            {
                trangHienTai--;
                TaiDuLieu();
            }
        }

        private void btnTrangSau_Click(object sender, EventArgs e)
        {
            if (trangHienTai < tongSoTrang)
            {
                trangHienTai++;
                TaiDuLieu();
            }
        }

        private void cboSoDongMoiTrang_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cboSoDongMoiTrang.SelectedItem != null && int.TryParse(cboSoDongMoiTrang.SelectedItem.ToString(), out int rows))
            {
                soDongMoiTrang = rows;
                trangHienTai = 1;
                TaiDuLieu();
            }
        }

        #endregion

        #region Mở Form Nghiệp Vụ

        private void btnKiemKho_Click(object sender, EventArgs e)
        {
            using (frmThemPhieuKiemKho frm = new frmThemPhieuKiemKho())
            {
                if (frm.ShowDialog() == DialogResult.OK && frm.DaThayDoiDuLieu)
                {
                    trangHienTai = 1;
                    TaiDuLieu();
                }
            }
        }

        private void dgvKiemKho_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;
            if (e.ColumnIndex >= 0 && dgvKiemKho.Columns[e.ColumnIndex].Name == "colChon") return;

            DataGridViewRow row = dgvKiemKho.Rows[e.RowIndex];
            if (row.Cells["colMaPhieuKiemKe"].Value != null &&
                int.TryParse(row.Cells["colMaPhieuKiemKe"].Value.ToString(), out int maPhieuKiemKe))
            {
                using (frmChiTietKiemKho frm = new frmChiTietKiemKho(maPhieuKiemKe))
                {
                    frm.ShowDialog();
                    if (frm.DaThayDoiDuLieu)
                    {
                        TaiDuLieu();
                    }
                }
            }
        }

        #region Xử lý CheckBox danh sách & Header CheckBox

        private void dgvKiemKho_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        {
            if (e.RowIndex == -1 && e.ColumnIndex >= 0 && dgvKiemKho.Columns[e.ColumnIndex].Name == "colChon")
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

        private void dgvKiemKho_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.ColumnIndex >= 0 && dgvKiemKho.Columns[e.ColumnIndex].Name == "colChon")
            {
                bool currentVal = Convert.ToBoolean(dgvKiemKho.Rows[e.RowIndex].Cells["colChon"].Value ?? false);
                bool newVal = !currentVal;
                dgvKiemKho.Rows[e.RowIndex].Cells["colChon"].Value = newVal;
                if (dgvKiemKho.DataSource is DataTable dt && e.RowIndex < dt.Rows.Count)
                {
                    dt.Rows[e.RowIndex]["Chon"] = newVal;
                }
                CapNhatTrangThaiHeaderCheckBox();
            }
        }

        private void dgvKiemKho_ColumnHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.ColumnIndex >= 0 && dgvKiemKho.Columns[e.ColumnIndex].Name == "colChon")
            {
                if (dgvKiemKho.Rows.Count == 0) return;

                isHeaderChecked = !isHeaderChecked;
                foreach (DataGridViewRow row in dgvKiemKho.Rows)
                {
                    row.Cells["colChon"].Value = isHeaderChecked;
                }
                if (dgvKiemKho.DataSource is DataTable dt)
                {
                    foreach (DataRow dr in dt.Rows)
                    {
                        dr["Chon"] = isHeaderChecked;
                    }
                }
                dgvKiemKho.InvalidateCell(e.ColumnIndex, -1);
            }
        }

        private void dgvKiemKho_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Space && dgvKiemKho.CurrentRow != null && dgvKiemKho.CurrentRow.Index >= 0)
            {
                var cell = dgvKiemKho.CurrentRow.Cells["colChon"];
                if (cell != null)
                {
                    bool currentVal = Convert.ToBoolean(cell.Value ?? false);
                    bool newVal = !currentVal;
                    cell.Value = newVal;
                    if (dgvKiemKho.DataSource is DataTable dt && dgvKiemKho.CurrentRow.Index < dt.Rows.Count)
                    {
                        dt.Rows[dgvKiemKho.CurrentRow.Index]["Chon"] = newVal;
                    }
                    CapNhatTrangThaiHeaderCheckBox();
                    e.Handled = true;
                }
            }
        }

        private void CapNhatTrangThaiHeaderCheckBox()
        {
            if (dgvKiemKho.Rows.Count == 0)
            {
                isHeaderChecked = false;
                return;
            }

            bool allChecked = true;
            foreach (DataGridViewRow row in dgvKiemKho.Rows)
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
                if (dgvKiemKho.Columns.Contains("colChon"))
                {
                    dgvKiemKho.InvalidateCell(dgvKiemKho.Columns["colChon"].Index, -1);
                }
            }
        }

        public List<int> LayDanhSachMaPhieuKiemKeDaChon()
        {
            List<int> list = new List<int>();
            foreach (DataGridViewRow row in dgvKiemKho.Rows)
            {
                if (Convert.ToBoolean(row.Cells["colChon"].Value ?? false))
                {
                    if (row.Cells["colMaPhieuKiemKe"].Value != null &&
                        int.TryParse(row.Cells["colMaPhieuKiemKe"].Value.ToString(), out int id))
                    {
                        list.Add(id);
                    }
                }
            }
            return list;
        }

        #endregion

        private void btnHienCot_Click(object sender, EventArgs e)
        {
            foreach (Form openForm in Application.OpenForms)
            {
                if (openForm is frmHienCotKiemKho)
                {
                    openForm.Activate();
                    openForm.BringToFront();
                    return;
                }
            }

            frmHienCotKiemKho frm = new frmHienCotKiemKho(dgvKiemKho);
            frm.StartPosition = FormStartPosition.Manual;

            // Tính toạ độ hiển thị ngay phía dưới nút btnHienCot (căn thẳng mép phải nút)
            Point btnPos = btnHienCot.PointToScreen(Point.Empty);
            int posX = btnPos.X + btnHienCot.Width - frm.Width;
            int posY = btnPos.Y + btnHienCot.Height + 2;

            // Bảo đảm form không bị tràn ra ngoài màn hình làm việc
            Rectangle workingArea = Screen.FromControl(btnHienCot).WorkingArea;
            if (posX + frm.Width > workingArea.Right)
            {
                posX = workingArea.Right - frm.Width;
            }
            if (posX < workingArea.Left)
            {
                posX = workingArea.Left;
            }
            if (posY + frm.Height > workingArea.Bottom)
            {
                posY = btnPos.Y - frm.Height - 2;
            }

            frm.Location = new Point(posX, posY);
            frm.Show(this);
            frm.BringToFront();
        }

        private void btnHuongDanSuDung_Click(object sender, EventArgs e)
        {
            MessageBox.Show(
                "HƯỚNG DẪN SỬ DỤNG MÀN HÌNH KIỂM KHO:\n\n" +
                "1. Nhấn '+ Kiểm kho' để tạo phiếu kiểm kê mới.\n" +
                "2. Trong form thêm phiếu, có thể chọn từng sản phẩm từ danh mục hoặc nạp tự động qua nút 'Nhập file' (hỗ trợ tải file mẫu Excel/CSV).\n" +
                "3. Nhấn đúp chuột vào một dòng phiếu kiểm kho bất kỳ để xem chi tiết.\n" +
                "4. Phiếu ở trạng thái 'Đã cân bằng' sẽ bị khóa vĩnh viễn (không thể xóa).\n" +
                "5. Nhấn 'Xuất file' để lưu danh sách kiểm kho ra file Excel hoặc CSV.",
                "Hướng dẫn sử dụng",
                MessageBoxButtons.OK,
                MessageBoxIcon.Information);
        }

        #endregion

        #region Xuất File Danh Sách

        private void btnXuatFile_Click(object sender, EventArgs e)
        {
            if (dtDanhSachPhieu == null || dtDanhSachPhieu.Rows.Count == 0)
            {
                MessageBox.Show("Không có dữ liệu phiếu kiểm kho để xuất file!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            using (SaveFileDialog sfd = new SaveFileDialog())
            {
                // Xác định danh sách dòng cần xuất: Ưu tiên các dòng được tích chọn CheckBox
                List<DataRow> danhSachXuat = new List<DataRow>();
                foreach (DataGridViewRow row in dgvKiemKho.Rows)
                {
                    if (Convert.ToBoolean(row.Cells["colChon"].Value ?? false))
                    {
                        DataRowView drv = row.DataBoundItem as DataRowView;
                        if (drv != null)
                        {
                            danhSachXuat.Add(drv.Row);
                        }
                    }
                }

                bool coDongChon = danhSachXuat.Count > 0;
                if (!coDongChon)
                {
                    // Không tích dòng nào: xuất toàn bộ phiếu khớp bộ lọc (không chỉ trang đang xem)
                    DataTable dtToanBo = null;
                    try
                    {
                        if (!string.IsNullOrEmpty(sqlXuatToanBo) && thamSoXuatToanBo != null)
                        {
                            List<SqlParameter> ps = new List<SqlParameter>();
                            foreach (SqlParameter p in thamSoXuatToanBo) ps.Add(new SqlParameter(p.ParameterName, p.Value));
                            dtToanBo = DataProcessing.ReadData(sqlXuatToanBo, ps.ToArray());
                        }
                    }
                    catch { dtToanBo = null; }

                    foreach (DataRow r in (dtToanBo ?? dtDanhSachPhieu).Rows)
                    {
                        danhSachXuat.Add(r);
                    }
                }

                sfd.Title = coDongChon ? $"Xuất {danhSachXuat.Count} phiếu kiểm kho đã chọn" : "Xuất danh sách phiếu kiểm kho";
                sfd.Filter = "File Excel (*.xls)|*.xls|File CSV (*.csv)|*.csv";
                sfd.FileName = coDongChon
                    ? $"DanhSachKiemKho_DaChon_{DateTime.Now:yyyyMMdd_HHmm}.xls"
                    : $"DanhSachKiemKho_{DateTime.Now:yyyyMMdd_HHmm}.xls";

                if (sfd.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        string ext = Path.GetExtension(sfd.FileName).ToLowerInvariant();
                        if (ext == ".csv")
                        {
                            XuatCsvDanhSach(sfd.FileName, danhSachXuat);
                        }
                        else
                        {
                            XuatExcelHtmlDanhSach(sfd.FileName, danhSachXuat);
                        }

                        MessageBox.Show("Xuất file danh sách thành công!\nĐường dẫn: " + sfd.FileName, "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Lỗi khi xuất file: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        private void XuatCsvDanhSach(string filePath, IEnumerable<DataRow> danhSach)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("Mã kiểm kho,Ngày cân bằng,SL lệch tăng,SL lệch giảm,SL thực tế,Ghi chú,Tổng thực tế,Tổng chênh lệch,Thời gian tạo,Trạng thái");

            // Thứ tự giá trị khớp đúng thứ tự tiêu đề cột (giống file Excel); số ghi theo định dạng bất biến
            foreach (DataRow r in danhSach)
            {
                sb.AppendLine(string.Join(",",
                    ChuoiCsv(r["MaPhieu"]),
                    ChuoiCsv(r["NgayCanBang"]),
                    SoCsv(r["SlLechTang"]),
                    SoCsv(r["SlLechGiam"]),
                    SoCsv(r["TongSlThucTe"]),
                    ChuoiCsv(r["GhiChu"]),
                    SoCsv(r["TongGiaTriThucTe"]),
                    SoCsv(r["TongGiaTriChenhLech"]),
                    ChuoiCsv(r["ThoiGian"]),
                    ChuoiCsv(r["TenTrangThai"])));
            }

            File.WriteAllText(filePath, sb.ToString(), new UTF8Encoding(true));
        }

        private static string ChuoiCsv(object giaTri)
        {
            string s = giaTri == null || giaTri == DBNull.Value ? "" : giaTri.ToString();
            return "\"" + s.Replace("\"", "\"\"") + "\"";
        }

        private static string SoCsv(object giaTri)
        {
            if (giaTri == null || giaTri == DBNull.Value) return "";
            return Convert.ToDecimal(giaTri).ToString("0.##", CultureInfo.InvariantCulture);
        }

        private void XuatExcelHtmlDanhSach(string filePath, IEnumerable<DataRow> danhSach)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("<html xmlns:o=\"urn:schemas-microsoft-com:office:office\" xmlns:x=\"urn:schemas-microsoft-com:office:excel\" xmlns=\"http://www.w3.org/TR/REC-html40\">");
            sb.AppendLine("<head>");
            sb.AppendLine("<meta http-equiv=\"Content-Type\" content=\"text/html; charset=utf-8\" />");
            sb.AppendLine("<style>");
            sb.AppendLine("body { font-family: Segoe UI, Arial; font-size: 10pt; }");
            sb.AppendLine("th { background-color: #0078D7; color: #FFFFFF; font-weight: bold; border: 0.5pt solid #CCCCCC; padding: 6px; }");
            sb.AppendLine("td { border: 0.5pt solid #E0E0E0; padding: 5px; }");
            sb.AppendLine(".num { mso-number-format: \"#,##0\"; text-align: right; }");
            sb.AppendLine(".center { text-align: center; }");
            sb.AppendLine("</style>");
            sb.AppendLine("</head>");
            sb.AppendLine("<body>");
            sb.AppendLine("<h2>DANH SÁCH PHIẾU KIỂM KHO</h2>");
            sb.AppendLine($"<p>Thời gian xuất: {DateTime.Now:dd/MM/yyyy HH:mm} | Tổng số: {tongSoBanGhi} phiếu</p>");
            sb.AppendLine("<table>");
            sb.AppendLine("<tr>");
            sb.AppendLine("<th>Mã kiểm kho</th>");
            sb.AppendLine("<th>Ngày cân bằng</th>");
            sb.AppendLine("<th>SL lệch tăng</th>");
            sb.AppendLine("<th>SL lệch giảm</th>");
            sb.AppendLine("<th>SL thực tế</th>");
            sb.AppendLine("<th>Ghi chú</th>");
            sb.AppendLine("<th>Tổng thực tế</th>");
            sb.AppendLine("<th>Tổng chênh lệch</th>");
            sb.AppendLine("<th>Thời gian tạo</th>");
            sb.AppendLine("<th>Trạng thái</th>");
            sb.AppendLine("</tr>");

            foreach (DataRow r in danhSach)
            {
                sb.AppendLine("<tr>");
                sb.AppendLine($"<td class=\"center\">{r["MaPhieu"]}</td>");
                sb.AppendLine($"<td class=\"center\">{r["NgayCanBang"]}</td>");
                sb.AppendLine($"<td class=\"num\">{Convert.ToDecimal(r["SlLechTang"]):N0}</td>");
                sb.AppendLine($"<td class=\"num\">{Convert.ToDecimal(r["SlLechGiam"]):N0}</td>");
                sb.AppendLine($"<td class=\"num\">{Convert.ToDecimal(r["TongSlThucTe"]):N0}</td>");
                sb.AppendLine($"<td>{r["GhiChu"]}</td>");
                sb.AppendLine($"<td class=\"num\">{Convert.ToDecimal(r["TongGiaTriThucTe"]):N0}</td>");
                sb.AppendLine($"<td class=\"num\">{Convert.ToDecimal(r["TongGiaTriChenhLech"]):N0}</td>");
                sb.AppendLine($"<td class=\"center\">{r["ThoiGian"]}</td>");
                sb.AppendLine($"<td class=\"center\">{r["TenTrangThai"]}</td>");
                sb.AppendLine("</tr>");
            }

            sb.AppendLine("</table>");
            sb.AppendLine("</body>");
            sb.AppendLine("</html>");

            File.WriteAllText(filePath, sb.ToString(), Encoding.UTF8);
        }

        #endregion
    }
}
