using BTL_LTTQ_2.Classes;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Text;
using System.Windows.Forms;

namespace BTL_LTTQ_2.GUI.Forms.KiemKho
{
    public partial class frmKiemKho : Form
    {
        private int trangHienTai = 1;
        private int soDongMoiTrang = 15;
        private int tongSoTrang = 1;
        private int tongSoBanGhi = 0;
        private bool isInitializing = true;
        private DataTable dtDanhSachPhieu;

        public frmKiemKho()
        {
            InitializeComponent();
            CauHinhDataGridView();
        }

        private void CauHinhDataGridView()
        {
            // Bật DoubleBuffered chống rung nhấp nháy lưới
            typeof(DataGridView).InvokeMember("DoubleBuffered",
                System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.SetProperty,
                null, dgvKiemKho, new object[] { true });

            dgvKiemKho.AutoGenerateColumns = false;
            dgvKiemKho.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(248, 250, 252);
            dgvKiemKho.DefaultCellStyle.SelectionBackColor = Color.FromArgb(224, 231, 255);
            dgvKiemKho.DefaultCellStyle.SelectionForeColor = Color.FromArgb(15, 23, 42);

            colChon.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            colMaPhieu.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            colThoiGian.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            colNgayCanBang.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            colTrangThai.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            colTongThucTe.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            colTongChenhLech.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            colSlLechTang.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            colSlLechGiam.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            colSlThucTe.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;

            colTongThucTe.DefaultCellStyle.Format = "N0";
            colTongChenhLech.DefaultCellStyle.Format = "N0";
            colSlLechTang.DefaultCellStyle.Format = "N0";
            colSlLechGiam.DefaultCellStyle.Format = "N0";
            colSlThucTe.DefaultCellStyle.Format = "N0";
        }

        private void frmKiemKho_Load(object sender, EventArgs e)
        {
            isInitializing = true;

            // Khởi tạo thời gian: Từ ngày đầu tháng tới cuối ngày hôm nay
            dtpTuNgay.Value = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
            dtpDenNgay.Value = DateTime.Now;

            // Nạp ComboBox phân trang
            cboSoDongMoiTrang.SelectedItem = "15";

            // Nạp các bộ lọc ComboBox
            NapBoLocTrangThai();
            NapBoLocNguoiTao();
            NapBoLocKho();

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
            dt.Rows.Add("HUY", "Đã hủy");

            cboTrangThai.DisplayMember = "TenTrangThai";
            cboTrangThai.ValueMember = "MaTrangThai";
            cboTrangThai.DataSource = dt;
            cboTrangThai.SelectedIndex = 0;
        }

        private void NapBoLocNguoiTao()
        {
            try
            {
                string sql = @"
                    SELECT 
                        0 AS MaNhanVien, 
                        N'-- Tất cả người tạo --' AS HoTen 
                    UNION ALL 
                    SELECT 
                        ISNULL(nv.MaNV, tk.MaTaiKhoan) AS MaNhanVien, 
                        ISNULL(nv.HoTen, tk.TenDangNhap) AS HoTen 
                    FROM TaiKhoan tk 
                    LEFT JOIN NhanVien nv ON tk.MaNV = nv.MaNV 
                    WHERE tk.TrangThai = 'HOAT_DONG' OR tk.TrangThai = 'ACTIVE' OR tk.TrangThai IS NULL";

                DataTable dt = DataProcessing.ReadData(sql);
                ComboboxHelper.CaiDatTimKiem(cboNguoiTao, dt, "HoTen", "MaNhanVien", () =>
                {
                    trangHienTai = 1;
                    TaiDuLieu();
                });
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi nạp danh sách người tạo: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void NapBoLocKho()
        {
            try
            {
                string sql = @"
                    SELECT 0 AS MaKho, N'-- Tất cả chi nhánh kho --' AS TenKho
                    UNION ALL
                    SELECT MaKho, TenKho FROM Kho 
                    WHERE TrangThai = 'HOAT_DONG' OR TrangThai = 'ACTIVE' OR TrangThai IS NULL";

                DataTable dt = DataProcessing.ReadData(sql);
                cboKho.DisplayMember = "TenKho";
                cboKho.ValueMember = "MaKho";
                cboKho.DataSource = dt;
                cboKho.SelectedIndex = 0;
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
                if (cboKho.SelectedValue != null && int.TryParse(cboKho.SelectedValue.ToString(), out int maKho) && maKho > 0)
                {
                    whereClause += " AND pkk.MaKho = @MaKho ";
                    listParams.Add(new SqlParameter("@MaKho", maKho));
                }

                // 2. Đếm tổng số bản ghi
                string sqlCount = $"SELECT COUNT(*) FROM PhieuKiemKe pkk {whereClause}";
                SqlParameter[] countParams = SaoChepThamSo(listParams);
                object countVal = DataProcessing.ReadValue(sqlCount, countParams);
                tongSoBanGhi = (countVal != null && countVal != DBNull.Value) ? Convert.ToInt32(countVal) : 0;

                // 3. Tính phân trang
                if (soDongMoiTrang <= 0) soDongMoiTrang = 15;
                tongSoTrang = (int)Math.Ceiling((double)tongSoBanGhi / soDongMoiTrang);
                if (tongSoTrang < 1) tongSoTrang = 1;
                if (trangHienTai > tongSoTrang) trangHienTai = tongSoTrang;
                if (trangHienTai < 1) trangHienTai = 1;

                int skip = (trangHienTai - 1) * soDongMoiTrang;

                // 4. Truy vấn lấy dữ liệu chi tiết của trang hiện tại
                string sqlData = $@"
                    SELECT 
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
                            WHEN pdc.NgayDuyet IS NOT NULL THEN CONVERT(VARCHAR(16), pdc.NgayDuyet, 120)
                            ELSE ''
                        END AS NgayCanBang,
                        -- Tính các chỉ số thực tế và chênh lệch từ ChiTietPhieuKiemKe
                        ISNULL(ct.TongSlThucTe, 0) AS TongSlThucTe,
                        ISNULL(ct.TongGiaTriThucTe, 0) AS TongGiaTriThucTe,
                        ISNULL(ct.SlLechTang, 0) AS SlLechTang,
                        ISNULL(ct.SlLechGiam, 0) AS SlLechGiam,
                        ISNULL(ct.TongGiaTriChenhLech, 0) AS TongGiaTriChenhLech
                    FROM PhieuKiemKe pkk
                    LEFT JOIN PhieuDieuChinh pdc ON pdc.MaPhieuKiemKe = pkk.MaPhieuKiemKe AND pdc.TrangThai = 'DA_DUYET'
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

                listParams.Add(new SqlParameter("@Skip", skip));
                listParams.Add(new SqlParameter("@Take", soDongMoiTrang));

                dtDanhSachPhieu = DataProcessing.ReadData(sqlData, listParams.ToArray());
                dgvKiemKho.DataSource = dtDanhSachPhieu;

                // 5. Cập nhật nhãn phân trang
                lblTongSoPhieu.Text = $"Tổng số: {tongSoBanGhi:N0} phiếu kiểm";
                lblTrangHienTai.Text = $"{trangHienTai} / {tongSoTrang}";
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
            BoLoc_ThayDoi(sender, e);
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

        private void btnHienCot_Click(object sender, EventArgs e)
        {
            frmHienCotKiemKho frm = new frmHienCotKiemKho(dgvKiemKho);
            Point pt = btnHienCot.PointToScreen(new Point(0, btnHienCot.Height));
            frm.Location = pt;
            frm.Show();
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
                sfd.Title = "Xuất danh sách phiếu kiểm kho";
                sfd.Filter = "File Excel (*.xls)|*.xls|File CSV (*.csv)|*.csv";
                sfd.FileName = $"DanhSachKiemKho_{DateTime.Now:yyyyMMdd_HHmm}.xls";

                if (sfd.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        string ext = Path.GetExtension(sfd.FileName).ToLowerInvariant();
                        if (ext == ".csv")
                        {
                            XuatCsvDanhSach(sfd.FileName);
                        }
                        else
                        {
                            XuatExcelHtmlDanhSach(sfd.FileName);
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

        private void XuatCsvDanhSach(string filePath)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("Mã kiểm kho,Thời gian,Tổng thực tế,Tổng chênh lệch,SL lệch tăng,SL lệch giảm,Ngày cân bằng,SL thực tế,Ghi chú,Trạng thái");

            foreach (DataRow r in dtDanhSachPhieu.Rows)
            {
                sb.AppendLine($"\"{r["MaPhieu"]}\",\"{r["ThoiGian"]}\",{r["TongGiaTriThucTe"]},{r["TongGiaTriChenhLech"]},{r["SlLechTang"]},{r["SlLechGiam"]},\"{r["NgayCanBang"]}\",{r["TongSlThucTe"]},\"{r["GhiChu"]}\",\"{r["TenTrangThai"]}\"");
            }

            File.WriteAllText(filePath, sb.ToString(), new UTF8Encoding(true));
        }

        private void XuatExcelHtmlDanhSach(string filePath)
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
            sb.AppendLine("<th>Thời gian</th>");
            sb.AppendLine("<th>Tổng thực tế</th>");
            sb.AppendLine("<th>Tổng chênh lệch</th>");
            sb.AppendLine("<th>SL lệch tăng</th>");
            sb.AppendLine("<th>SL lệch giảm</th>");
            sb.AppendLine("<th>Ngày cân bằng</th>");
            sb.AppendLine("<th>SL thực tế</th>");
            sb.AppendLine("<th>Ghi chú</th>");
            sb.AppendLine("<th>Trạng thái</th>");
            sb.AppendLine("</tr>");

            foreach (DataRow r in dtDanhSachPhieu.Rows)
            {
                sb.AppendLine("<tr>");
                sb.AppendLine($"<td class=\"center\">{r["MaPhieu"]}</td>");
                sb.AppendLine($"<td class=\"center\">{r["ThoiGian"]}</td>");
                sb.AppendLine($"<td class=\"num\">{Convert.ToDecimal(r["TongGiaTriThucTe"]):N0}</td>");
                sb.AppendLine($"<td class=\"num\">{Convert.ToDecimal(r["TongGiaTriChenhLech"]):N0}</td>");
                sb.AppendLine($"<td class=\"num\">{Convert.ToDecimal(r["SlLechTang"]):N0}</td>");
                sb.AppendLine($"<td class=\"num\">{Convert.ToDecimal(r["SlLechGiam"]):N0}</td>");
                sb.AppendLine($"<td class=\"center\">{r["NgayCanBang"]}</td>");
                sb.AppendLine($"<td class=\"num\">{Convert.ToDecimal(r["TongSlThucTe"]):N0}</td>");
                sb.AppendLine($"<td>{r["GhiChu"]}</td>");
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
