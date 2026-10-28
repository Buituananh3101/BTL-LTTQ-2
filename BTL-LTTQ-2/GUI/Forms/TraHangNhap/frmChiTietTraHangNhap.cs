using BTL_LTTQ_2.Classes;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Text;
using System.Windows.Forms;

namespace BTL_LTTQ_2.GUI.Forms.TraHangNhap
{
    public partial class frmChiTietTraHangNhap : Form
    {
        private int maPhieuTraHang;
        private string maPhieu;
        private string currentTrangThai;
        private int maDonViCapHang;
        private int maKho;
        private DataTable dtChiTietData;

        public frmChiTietTraHangNhap(int maPhieuTraHang)
        {
            InitializeComponent();
            this.maPhieuTraHang = maPhieuTraHang;
            CauHinhDataGridView();
        }

        private void CauHinhDataGridView()
        {
            // Bật DoubleBuffered chống giật lag lưới
            typeof(DataGridView).InvokeMember("DoubleBuffered",
                System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.SetProperty,
                null, dgvChiTiet, new object[] { true });

            dgvChiTiet.AutoGenerateColumns = false;
            dgvChiTiet.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(248, 250, 252);
            dgvChiTiet.DefaultCellStyle.SelectionBackColor = Color.FromArgb(224, 231, 255);
            dgvChiTiet.DefaultCellStyle.SelectionForeColor = Color.FromArgb(30, 41, 59);

            colStt.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            colMaHangNoiBo.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            colDonViTinh.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            colSoLuong.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            colGiaNhap.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            colDonGiaTra.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            colThanhTien.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;

            colSoLuong.DefaultCellStyle.Format = "N0";
            colGiaNhap.DefaultCellStyle.Format = "N0";
            colDonGiaTra.DefaultCellStyle.Format = "N0";
            colThanhTien.DefaultCellStyle.Format = "N0";
        }

        private void frmChiTietTraHangNhap_Load(object sender, EventArgs e)
        {
            NapThongTinPhieu();
        }

        public void NapThongTinPhieu()
        {
            try
            {
                string sqlHeader = @"
                    SELECT 
                        pth.MaPhieuTraHang,
                        ISNULL(pth.MaPhieu, 'THN' + RIGHT('000000' + CAST(pth.MaPhieuTraHang AS VARCHAR(10)), 6)) AS MaPhieu,
                        pth.MaKho,
                        ISNULL(k.TenKho, N'Kho trung tâm') AS TenKho,
                        ISNULL(pth.MaDonViCapHang, 0) AS MaDonViCapHang,
                        ISNULL(ncc.TenDonVi, N'Nhà cung cấp lẻ') AS TenDonVi,
                        pth.MaNvlap,
                        ISNULL(nvLap.HoTen, ISNULL(tkLap.TenDangNhap, N'Quản trị viên')) AS TenNvLap,
                        pth.MaNvtra,
                        ISNULL(nvTra.HoTen, ISNULL(tkTra.TenDangNhap, ISNULL(nvLap.HoTen, N'Quản trị viên'))) AS TenNvTra,
                        pth.NgayLap,
                        pth.NgayTra,
                        pth.TrangThai,
                        ISNULL(pth.GhiChu, '') AS GhiChu,
                        pth.TongSoLuong,
                        pth.TongTienHang,
                        pth.GiamGia,
                        pth.CanThuNcc,
                        pth.DaThuNcc
                    FROM PhieuTraHangNhap pth
                    LEFT JOIN Kho k ON pth.MaKho = k.MaKho
                    LEFT JOIN DonViCapHang ncc ON pth.MaDonViCapHang = ncc.MaDonViCapHang
                    LEFT JOIN TaiKhoan tkLap ON pth.MaNvlap = tkLap.MaTaiKhoan
                    LEFT JOIN NhanVien nvLap ON tkLap.MaNV = nvLap.MaNV
                    LEFT JOIN TaiKhoan tkTra ON pth.MaNvtra = tkTra.MaTaiKhoan
                    LEFT JOIN NhanVien nvTra ON tkTra.MaNV = nvTra.MaNV
                    WHERE pth.MaPhieuTraHang = @MaPhieuTraHang";

                SqlParameter[] pHeader = { new SqlParameter("@MaPhieuTraHang", maPhieuTraHang) };
                DataTable dtHeader = DataProcessing.ReadData(sqlHeader, pHeader);

                if (dtHeader == null || dtHeader.Rows.Count == 0)
                {
                    MessageBox.Show("Không tìm thấy thông tin đơn trả hàng này!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    this.Close();
                    return;
                }

                DataRow row = dtHeader.Rows[0];
                maPhieu = row["MaPhieu"].ToString();
                currentTrangThai = row["TrangThai"].ToString();
                maKho = Convert.ToInt32(row["MaKho"]);
                maDonViCapHang = Convert.ToInt32(row["MaDonViCapHang"]);

                // Hiển thị thông tin Header
                lblMaPhieuHienThi.Text = maPhieu;
                lblKhoChiNhanh.Text = "Kho: " + row["TenKho"].ToString();

                // Định dạng hiển thị Trạng thái chuẩn
                if (currentTrangThai == "DA_TRA_HANG")
                {
                    lblTrangThai.Text = "Đã trả hàng";
                    lblTrangThai.ForeColor = Color.FromArgb(22, 163, 74); // Green
                    btnXoa.Enabled = true;
                    btnHoanThanh.Visible = false;
                }
                else if (currentTrangThai == "NHAP")
                {
                    lblTrangThai.Text = "Phiếu tạm";
                    lblTrangThai.ForeColor = Color.FromArgb(217, 119, 6); // Amber
                    btnXoa.Enabled = true;
                    btnHoanThanh.Visible = true;
                    btnHoanThanh.Enabled = true;
                }
                else
                {
                    lblTrangThai.Text = "Đã hủy";
                    lblTrangThai.ForeColor = Color.FromArgb(100, 116, 139); // Gray/Slate nhạt
                    btnXoa.Enabled = false;
                    btnHoanThanh.Visible = false;
                }

                lblNguoiTaoVal.Text = row["TenNvLap"].ToString();
                lblNguoiTraVal.Text = row["TenNvTra"].ToString();
                lblNgayTraVal.Text = Convert.ToDateTime(row["NgayTra"]).ToString("dd/MM/yyyy HH:mm");
                lblTenNccVal.Text = row["TenDonVi"].ToString();

                lblMaNhapHangVal.Text = "Trả tự do";

                txtGhiChu.Text = row["GhiChu"].ToString();

                decimal tongTienHang = Convert.ToDecimal(row["TongTienHang"]);
                decimal giamGia = Convert.ToDecimal(row["GiamGia"]);
                decimal canThuNcc = Convert.ToDecimal(row["CanThuNcc"]);
                decimal daThuNcc = Convert.ToDecimal(row["DaThuNcc"]);

                lblTongTienHangVal.Text = tongTienHang.ToString("N0") + " đ";
                lblGiamGiaVal.Text = giamGia.ToString("N0") + " %";
                lblCanThuNccVal.Text = canThuNcc.ToString("N0") + " đ";
                lblDaThuNccVal.Text = daThuNcc.ToString("N0") + " đ";

                // Nạp chi tiết hàng trả
                NapChiTietHangTra();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi tải thông tin chi tiết đơn trả hàng: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void NapChiTietHangTra()
        {
            try
            {
                string sqlDetails = @"
                    SELECT 
                        ROW_NUMBER() OVER(ORDER BY ct.MaCtphieuTra) AS STT,
                        ct.MaCtphieuTra,
                        ct.MaHang,
                        h.MaHangNoiBo,
                        h.TenHang,
                        ISNULL(dvt.TenDVT, N'Cái') AS TenDVT,
                        ct.SoLuong,
                        ct.GiaVon AS GiaNhap,
                        ct.DonGiaTra,
                        ct.ThanhTien,
                        ISNULL(ct.GhiChu, '') AS GhiChu
                    FROM ChiTietPhieuTraHangNhap ct
                    JOIN HangHoa h ON ct.MaHang = h.MaHang
                    LEFT JOIN DonViTinh dvt ON h.MaDVT = dvt.MaDVT
                    WHERE ct.MaPhieuTraHang = @MaPhieuTraHang
                    ORDER BY ct.MaCtphieuTra ASC";

                SqlParameter[] pDetails = { new SqlParameter("@MaPhieuTraHang", maPhieuTraHang) };
                dtChiTietData = DataProcessing.ReadData(sqlDetails, pDetails);

                dgvChiTiet.DataSource = dtChiTietData;

                lblSoLuongMatHangVal.Text = dtChiTietData != null ? dtChiTietData.Rows.Count.ToString("N0") : "0";
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi tải danh sách hàng hóa trả: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            if (currentTrangThai == "HUY")
            {
                MessageBox.Show("Đơn trả hàng này đã ở trạng thái Đã hủy trước đó, không thể xóa tiếp!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            string thongBao = currentTrangThai == "NHAP"
                ? $"Bạn có chắc chắn muốn xóa vĩnh viễn phiếu tạm trả hàng [{maPhieu}] không?"
                : $"Bạn có chắc chắn muốn HỦY đơn trả hàng [{maPhieu}] không?\nSau khi hủy, số lượng hàng sẽ tự động hoàn lại tồn kho sổ sách.";

            DialogResult dr = MessageBox.Show(thongBao, "Xác nhận xóa/hủy đơn trả hàng", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (dr != DialogResult.Yes) return;

            try
            {
                DataProcessing.ChangeDataTransaction((conn, trans) =>
                {
                    if (currentTrangThai == "NHAP")
                    {
                        // Phiếu tạm: Xóa hoàn toàn trong CSDL
                        string sqlDelCt = "DELETE FROM ChiTietPhieuTraHangNhap WHERE MaPhieuTraHang = @MaPhieuTraHang";
                        using (SqlCommand cmdCt = new SqlCommand(sqlDelCt, conn, trans))
                        {
                            cmdCt.Parameters.AddWithValue("@MaPhieuTraHang", maPhieuTraHang);
                            cmdCt.ExecuteNonQuery();
                        }

                        string sqlDelHeader = "DELETE FROM PhieuTraHangNhap WHERE MaPhieuTraHang = @MaPhieuTraHang";
                        using (SqlCommand cmdHeader = new SqlCommand(sqlDelHeader, conn, trans))
                        {
                            cmdHeader.Parameters.AddWithValue("@MaPhieuTraHang", maPhieuTraHang);
                            cmdHeader.ExecuteNonQuery();
                        }
                    }
                    else
                    {
                        // Đã trả hàng: Chuyển sang trạng thái HUY để hoàn tồn qua View biến động kho
                        string sqlHuy = "UPDATE PhieuTraHangNhap SET TrangThai = 'HUY' WHERE MaPhieuTraHang = @MaPhieuTraHang";
                        using (SqlCommand cmdHuy = new SqlCommand(sqlHuy, conn, trans))
                        {
                            cmdHuy.Parameters.AddWithValue("@MaPhieuTraHang", maPhieuTraHang);
                            cmdHuy.ExecuteNonQuery();
                        }
                    }

                    // Ghi nhật ký thao tác nếu có bảng NhatKyThaoTac
                    try
                    {
                        string sqlLog = @"
                            INSERT INTO NhatKyThaoTac (MaTaiKhoan, ThoiDiem, ThaoTac, LoaiDoiTuong, MaDoiTuong, NoiDung)
                            VALUES (@MaTaiKhoan, GETDATE(), @ThaoTac, 'PhieuTraHangNhap', @MaDoiTuong, @NoiDung)";
                        using (SqlCommand cmdLog = new SqlCommand(sqlLog, conn, trans))
                        {
                            cmdLog.Parameters.AddWithValue("@MaTaiKhoan", SessionContext.MaTaiKhoan > 0 ? SessionContext.MaTaiKhoan : 1);
                            cmdLog.Parameters.AddWithValue("@ThaoTac", currentTrangThai == "NHAP" ? "XOA_PHIEU_TAM" : "HUY_PHIEU_TRA");
                            cmdLog.Parameters.AddWithValue("@MaDoiTuong", maPhieu);
                            cmdLog.Parameters.AddWithValue("@NoiDung", $"Thao tác xóa/hủy đơn trả hàng {maPhieu}");
                            cmdLog.ExecuteNonQuery();
                        }
                    }
                    catch
                    {
                        // Bỏ qua nếu bảng log chưa tồn tại
                    }
                });

                MessageBox.Show(currentTrangThai == "NHAP" ? "Đã xóa vĩnh viễn phiếu tạm trả hàng thành công!" : "Đã hủy đơn trả hàng thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi thực hiện xóa/hủy đơn trả hàng: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnXuatFile_Click(object sender, EventArgs e)
        {
            if (dtChiTietData == null || dtChiTietData.Rows.Count == 0)
            {
                MessageBox.Show("Không có dữ liệu hàng hóa để xuất file!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            using (SaveFileDialog sfd = new SaveFileDialog())
            {
                sfd.Title = "Xuất chi tiết đơn trả hàng nhập";
                sfd.Filter = "File Excel (*.xls)|*.xls|File CSV (*.csv)|*.csv";
                sfd.FileName = $"ChiTietTraHang_{maPhieu}_{DateTime.Now:yyyyMMdd_HHmm}.xls";

                if (sfd.ShowDialog(this) == DialogResult.OK)
                {
                    try
                    {
                        string ext = Path.GetExtension(sfd.FileName).ToLowerInvariant();
                        if (ext == ".csv")
                        {
                            XuatCsvChiTiet(sfd.FileName);
                        }
                        else
                        {
                            XuatExcelHtmlChiTiet(sfd.FileName);
                        }

                        MessageBox.Show("Xuất file chi tiết đơn trả hàng thành công!\nĐường dẫn: " + sfd.FileName, "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Lỗi khi xuất file: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        private void XuatExcelHtmlChiTiet(string filePath)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("<html xmlns:o=\"urn:schemas-microsoft-com:office:office\" xmlns:x=\"urn:schemas-microsoft-com:office:excel\" xmlns=\"http://www.w3.org/TR/REC-html40\">");
            sb.AppendLine("<head>");
            sb.AppendLine("<meta http-equiv=\"Content-Type\" content=\"text/html; charset=utf-8\" />");
            sb.AppendLine("<style>");
            sb.AppendLine("body { font-family: Segoe UI, Arial; font-size: 10pt; }");
            sb.AppendLine("th { background-color: #0066CC; color: #FFFFFF; font-weight: bold; border: 0.5pt solid #CCCCCC; padding: 6px; }");
            sb.AppendLine("td { border: 0.5pt solid #E0E0E0; padding: 5px; }");
            sb.AppendLine(".num { mso-number-format: \"#,##0\"; text-align: right; }");
            sb.AppendLine(".center { text-align: center; }");
            sb.AppendLine(".bold { font-weight: bold; }");
            sb.AppendLine("</style>");
            sb.AppendLine("</head>");
            sb.AppendLine("<body>");

            sb.AppendLine($"<h2>CHI TIẾT PHIẾU TRẢ HÀNG NHẬP: {maPhieu}</h2>");
            sb.AppendLine($"<p><b>Trạng thái:</b> {lblTrangThai.Text} | <b>Kho hàng:</b> {lblKhoChiNhanh.Text}</p>");
            sb.AppendLine($"<p><b>Nhà cung cấp:</b> {lblTenNccVal.Text}</p>");
            sb.AppendLine($"<p><b>Người tạo:</b> {lblNguoiTaoVal.Text} | <b>Người trả:</b> {lblNguoiTraVal.Text}</p>");
            sb.AppendLine($"<p><b>Ngày trả:</b> {lblNgayTraVal.Text} | <b>Mã đơn nhập gốc:</b> {lblMaNhapHangVal.Text}</p>");
            if (!string.IsNullOrWhiteSpace(txtGhiChu.Text))
            {
                sb.AppendLine($"<p><b>Ghi chú:</b> {System.Net.WebUtility.HtmlEncode(txtGhiChu.Text)}</p>");
            }
            sb.AppendLine("<br/>");

            sb.AppendLine("<table>");
            sb.AppendLine("<tr>");
            sb.AppendLine("<th>STT</th>");
            sb.AppendLine("<th>Mã hàng</th>");
            sb.AppendLine("<th>Tên hàng hóa</th>");
            sb.AppendLine("<th>ĐVT</th>");
            sb.AppendLine("<th>Số lượng trả</th>");
            sb.AppendLine("<th>Giá nhập</th>");
            sb.AppendLine("<th>Giá trả lại</th>");
            sb.AppendLine("<th>Thành tiền</th>");
            sb.AppendLine("</tr>");

            foreach (DataRow row in dtChiTietData.Rows)
            {
                string stt = row["STT"]?.ToString() ?? "";
                string maHang = row["MaHangNoiBo"]?.ToString() ?? "";
                string tenHang = row["TenHang"]?.ToString() ?? "";
                string dvt = row["TenDVT"]?.ToString() ?? "";
                decimal soLuong = row["SoLuong"] != DBNull.Value ? Convert.ToDecimal(row["SoLuong"]) : 0m;
                decimal giaNhap = row["GiaNhap"] != DBNull.Value ? Convert.ToDecimal(row["GiaNhap"]) : 0m;
                decimal donGiaTra = row["DonGiaTra"] != DBNull.Value ? Convert.ToDecimal(row["DonGiaTra"]) : 0m;
                decimal thanhTien = row["ThanhTien"] != DBNull.Value ? Convert.ToDecimal(row["ThanhTien"]) : 0m;

                sb.AppendLine("<tr>");
                sb.AppendLine($"<td class=\"center\">{stt}</td>");
                sb.AppendLine($"<td class=\"center\">{maHang}</td>");
                sb.AppendLine($"<td>{System.Net.WebUtility.HtmlEncode(tenHang)}</td>");
                sb.AppendLine($"<td class=\"center\">{System.Net.WebUtility.HtmlEncode(dvt)}</td>");
                sb.AppendLine($"<td class=\"num\">{soLuong:N0}</td>");
                sb.AppendLine($"<td class=\"num\">{giaNhap:N0}</td>");
                sb.AppendLine($"<td class=\"num\">{donGiaTra:N0}</td>");
                sb.AppendLine($"<td class=\"num\">{thanhTien:N0}</td>");
                sb.AppendLine("</tr>");
            }

            sb.AppendLine("</table>");
            sb.AppendLine("<br/>");
            sb.AppendLine($"<p><b>Số lượng mặt hàng:</b> {lblSoLuongMatHangVal.Text}</p>");
            sb.AppendLine($"<p><b>Tổng tiền hàng:</b> {lblTongTienHangVal.Text}</p>");
            sb.AppendLine($"<p><b>Giảm giá (%):</b> {lblGiamGiaVal.Text}</p>");
            sb.AppendLine($"<p><b>NCC cần trả:</b> {lblCanThuNccVal.Text}</p>");
            sb.AppendLine($"<p><b>NCC đã trả:</b> {lblDaThuNccVal.Text}</p>");

            sb.AppendLine("</body>");
            sb.AppendLine("</html>");

            File.WriteAllText(filePath, sb.ToString(), Encoding.UTF8);
        }

        private void XuatCsvChiTiet(string filePath)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("sep=,");
            sb.AppendLine($"PHIẾU TRẢ HÀNG NHẬP: {maPhieu}");
            sb.AppendLine($"Trạng thái: {lblTrangThai.Text}");
            sb.AppendLine($"Kho hàng: {lblKhoChiNhanh.Text}");
            sb.AppendLine($"Nhà cung cấp: \"{lblTenNccVal.Text.Replace("\"", "\"\"")}\"");
            sb.AppendLine($"Người tạo: {lblNguoiTaoVal.Text} | Người trả: {lblNguoiTraVal.Text}");
            sb.AppendLine($"Ngày trả: {lblNgayTraVal.Text}");
            sb.AppendLine($"Mã đơn nhập gốc: {lblMaNhapHangVal.Text}");
            sb.AppendLine($"Ghi chú: \"{txtGhiChu.Text.Replace("\"", "\"\"")}\"");
            sb.AppendLine();

            sb.AppendLine("STT,Mã hàng,Tên hàng hóa,ĐVT,Số lượng trả,Giá nhập,Giá trả lại,Thành tiền");

            foreach (DataRow row in dtChiTietData.Rows)
            {
                string stt = row["STT"]?.ToString() ?? "";
                string maHang = row["MaHangNoiBo"]?.ToString() ?? "";
                string tenHang = row["TenHang"]?.ToString() ?? "";
                string dvt = row["TenDVT"]?.ToString() ?? "";
                decimal soLuong = row["SoLuong"] != DBNull.Value ? Convert.ToDecimal(row["SoLuong"]) : 0m;
                decimal giaNhap = row["GiaNhap"] != DBNull.Value ? Convert.ToDecimal(row["GiaNhap"]) : 0m;
                decimal donGiaTra = row["DonGiaTra"] != DBNull.Value ? Convert.ToDecimal(row["DonGiaTra"]) : 0m;
                decimal thanhTien = row["ThanhTien"] != DBNull.Value ? Convert.ToDecimal(row["ThanhTien"]) : 0m;

                sb.AppendLine(string.Format("{0},\"{1}\",\"{2}\",\"{3}\",{4},{5},{6},{7}",
                    stt,
                    maHang,
                    tenHang.Replace("\"", "\"\""),
                    dvt.Replace("\"", "\"\""),
                    soLuong,
                    giaNhap,
                    donGiaTra,
                    thanhTien));
            }

            sb.AppendLine();
            sb.AppendLine($",,,Số lượng mặt hàng:,{lblSoLuongMatHangVal.Text}");
            sb.AppendLine($",,,Tổng tiền hàng:,,,{lblTongTienHangVal.Text.Replace(",", "")}");
            sb.AppendLine($",,,Giảm giá (%):,,,{lblGiamGiaVal.Text.Replace(",", "")}");
            sb.AppendLine($",,,NCC cần trả:,,,{lblCanThuNccVal.Text.Replace(",", "")}");
            sb.AppendLine($",,,NCC đã trả:,,,{lblDaThuNccVal.Text.Replace(",", "")}");

            File.WriteAllText(filePath, sb.ToString(), new UTF8Encoding(true));
        }

#if FALSE
        private void btnXuatFile_Click_Old(object sender, EventArgs e)
        {
            if (dtChiTietData == null || dtChiTietData.Rows.Count == 0)
            {
                MessageBox.Show("Không có dữ liệu hàng hóa để xuất file!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            using (SaveFileDialog sfd = new SaveFileDialog())
            {
                sfd.Filter = "CSV File (*.csv)|*.csv|All Files (*.*)|*.*";
                sfd.FileName = $"ChiTietTraHang_{maPhieu}_{DateTime.Now:yyyyMMdd_HHmm}.csv";

                if (sfd.ShowDialog(this) == DialogResult.OK)
                {
                    try
                    {
                        StringBuilder sb = new StringBuilder();
                        // Ghi thông tin Header
                        sb.AppendLine($"PHIẾU TRẢ HÀNG NHẬP: {maPhieu}");
                        sb.AppendLine($"Trạng thái: {lblTrangThai.Text}");
                        sb.AppendLine($"Kho hàng: {lblKhoChiNhanh.Text}");
                        sb.AppendLine($"Nhà cung cấp: {lblTenNccVal.Text}");
                        sb.AppendLine($"Người tạo: {lblNguoiTaoVal.Text} | Người trả: {lblNguoiTraVal.Text}");
                        sb.AppendLine($"Ngày trả: {lblNgayTraVal.Text}");
                        sb.AppendLine($"Mã đơn nhập gốc: {lblMaNhapHangVal.Text}");
                        sb.AppendLine($"Ghi chú: {txtGhiChu.Text}");
                        sb.AppendLine();

                        // Header cột
                        sb.AppendLine("STT,Mã hàng,Tên hàng hóa,ĐVT,Số lượng trả,Giá nhập,Giá trả lại,Thành tiền");

                        foreach (DataRow row in dtChiTietData.Rows)
                        {
                            string stt = row["STT"].ToString();
                            string maHang = EscapeCsv(row["MaHangNoiBo"].ToString());
                            string tenHang = EscapeCsv(row["TenHang"].ToString());
                            string dvt = EscapeCsv(row["TenDVT"].ToString());
                            string soLuong = Convert.ToDecimal(row["SoLuong"]).ToString("N0");
                            string giaNhap = Convert.ToDecimal(row["GiaNhap"]).ToString("N0");
                            string donGiaTra = Convert.ToDecimal(row["DonGiaTra"]).ToString("N0");
                            string thanhTien = Convert.ToDecimal(row["ThanhTien"]).ToString("N0");

                            sb.AppendLine($"{stt},{maHang},{tenHang},{dvt},{soLuong},{giaNhap},{donGiaTra},{thanhTien}");
                        }

                        sb.AppendLine();
                        sb.AppendLine($",,,Số lượng mặt hàng:,{lblSoLuongMatHangVal.Text}");
                        sb.AppendLine($",,,Tổng tiền hàng:,,,{lblTongTienHangVal.Text}");
                        sb.AppendLine($",,,Giảm giá:,,,{lblGiamGiaVal.Text}");
                        sb.AppendLine($",,,NCC cần trả:,,,{lblCanThuNccVal.Text}");
                        sb.AppendLine($",,,NCC đã trả:,,,{lblDaThuNccVal.Text}");

                        // Ghi file với UTF-8 BOM để Excel hiển thị đúng tiếng Việt
                        File.WriteAllText(sfd.FileName, sb.ToString(), new UTF8Encoding(true));
                        MessageBox.Show("Xuất file chi tiết đơn trả hàng thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Lỗi khi xuất file: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

#endif

        private string EscapeCsv(string text)
        {
            if (string.IsNullOrEmpty(text)) return "\"\"";
            if (text.Contains(",") || text.Contains("\"") || text.Contains("\n") || text.Contains("\r"))
            {
                return "\"" + text.Replace("\"", "\"\"") + "\"";
            }
            return text;
        }

        private void btnHoanThanh_Click(object sender, EventArgs e)
        {
            if (currentTrangThai != "NHAP") return;

            // 1. Kiểm tra tồn kho trước khi hoàn thành xuất trả
            if (dtChiTietData != null && dtChiTietData.Rows.Count > 0)
            {
                foreach (DataRow row in dtChiTietData.Rows)
                {
                    int maHang = Convert.ToInt32(row["MaHang"]);
                    decimal slTra = Convert.ToDecimal(row["SoLuong"]);
                    string sku = row["MaHangNoiBo"].ToString();
                    string ten = row["TenHang"].ToString();

                    string sqlCheckTon = "SELECT SoLuongTon FROM vw_TonKhoHienTai WHERE MaHang = @MaHang AND MaKho = @MaKho";
                    object tonObj = DataProcessing.ReadValue(sqlCheckTon, new SqlParameter[] {
                        new SqlParameter("@MaHang", maHang),
                        new SqlParameter("@MaKho", maKho)
                    });
                    decimal tonHienTai = (tonObj != null && tonObj != DBNull.Value) ? Convert.ToDecimal(tonObj) : 0m;

                    if (tonHienTai < slTra)
                    {
                        MessageBox.Show(
                            $"Không thể hoàn thành phiếu trả hàng vì mặt hàng '{sku} - {ten}' không đủ số lượng tồn trong kho (Tồn hiện tại: {tonHienTai:N0}, Cần trả: {slTra:N0})!\nNếu hoàn thành sẽ dẫn đến âm kho.",
                            "Chặn âm tồn kho",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Warning);
                        return;
                    }
                }
            }

            DialogResult dr = MessageBox.Show(
                $"Bạn có chắc chắn muốn HOÀN THÀNH phiếu trả hàng '{maPhieu}' không?\n\nSau khi hoàn thành:\n- Tồn kho các mặt hàng sẽ bị trừ khỏi kho '{lblKhoChiNhanh.Text}'.\n- Công nợ nhà cung cấp sẽ được cập nhật.\n- Trạng thái phiếu sẽ chuyển sang 'Đã trả hàng'.",
                "Xác nhận hoàn thành trả hàng",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            if (dr != DialogResult.Yes) return;

            try
            {
                DataProcessing.ChangeDataTransaction((conn, trans) =>
                {
                    string sqlUpdate = @"
                        UPDATE PhieuTraHangNhap 
                        SET TrangThai = 'DA_TRA_HANG', 
                            NgayTra = GETDATE(), 
                            MaNvtra = @MaNvtra 
                        WHERE MaPhieuTraHang = @MaPhieuTraHang";

                    using (SqlCommand cmd = new SqlCommand(sqlUpdate, conn, trans))
                    {
                        cmd.Parameters.AddWithValue("@MaPhieuTraHang", maPhieuTraHang);
                        cmd.Parameters.AddWithValue("@MaNvtra", SessionContext.MaTaiKhoan > 0 ? (object)SessionContext.MaTaiKhoan : 1);
                        cmd.ExecuteNonQuery();
                    }

                    string sqlLog = @"
                        INSERT INTO NhatKyThaoTac (MaTaiKhoan, ThoiDiem, ThaoTac, LoaiDoiTuong, MaDoiTuong, NoiDung)
                        VALUES (@MaTaiKhoan, GETDATE(), 'HOAN_THANH_TRA_HANG', 'PhieuTraHangNhap', @MaDoiTuong, @NoiDung)";

                    using (SqlCommand cmdLog = new SqlCommand(sqlLog, conn, trans))
                    {
                        cmdLog.Parameters.AddWithValue("@MaTaiKhoan", SessionContext.MaTaiKhoan > 0 ? SessionContext.MaTaiKhoan : 1);
                        cmdLog.Parameters.AddWithValue("@MaDoiTuong", maPhieu);
                        cmdLog.Parameters.AddWithValue("@NoiDung", $"Hoàn thành phiếu trả hàng {maPhieu} (ID: {maPhieuTraHang})");
                        cmdLog.ExecuteNonQuery();
                    }
                });

                MessageBox.Show($"Hoàn thành phiếu trả hàng '{maPhieu}' thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.DialogResult = DialogResult.OK;
                NapThongTinPhieu();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi hoàn thành phiếu trả hàng: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnDong_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
