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
                        ISNULL(nvLap.HoTen, N'Quản trị viên') AS TenNvLap,
                        pth.MaNvtra,
                        ISNULL(nvTra.HoTen, ISNULL(nvLap.HoTen, N'Quản trị viên')) AS TenNvTra,
                        pth.MaPhieuNhap,
                        pnGoc.MaPhieu AS MaPhieuNhapGoc,
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
                    -- MaNvlap / MaNvtra là khóa ngoại tới NhanVien.MaNV
                    LEFT JOIN NhanVien nvLap ON pth.MaNvlap = nvLap.MaNV
                    LEFT JOIN NhanVien nvTra ON pth.MaNvtra = nvTra.MaNV
                    LEFT JOIN PhieuNhap pnGoc ON pth.MaPhieuNhap = pnGoc.MaPhieuNhap
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

                // Hiển thị mã phiếu nhập gốc nếu phiếu trả được lập theo phiếu nhập
                if (row["MaPhieuNhap"] != DBNull.Value)
                {
                    lblMaNhapHangVal.Text = row["MaPhieuNhapGoc"] != DBNull.Value && !string.IsNullOrWhiteSpace(row["MaPhieuNhapGoc"].ToString())
                        ? row["MaPhieuNhapGoc"].ToString()
                        : "PN" + Convert.ToInt32(row["MaPhieuNhap"]).ToString("D6");
                }
                else
                {
                    lblMaNhapHangVal.Text = "Trả tự do";
                }

                txtGhiChu.Text = row["GhiChu"].ToString();

                decimal tongTienHang = Convert.ToDecimal(row["TongTienHang"]);
                decimal giamGia = Convert.ToDecimal(row["GiamGia"]);
                decimal canThuNcc = Convert.ToDecimal(row["CanThuNcc"]);
                decimal daThuNcc = Convert.ToDecimal(row["DaThuNcc"]);

                lblTongTienHangVal.Text = tongTienHang.ToString("N0") + " đ";
                lblGiamGiaVal.Text = giamGia.ToString("N0") + " đ";
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
                        // Phiếu tạm: Xóa hoàn toàn trong CSDL (kiểm tra lại trạng thái, phiếu có thể vừa được hoàn thành ở máy khác)
                        using (SqlCommand cmdKt = new SqlCommand("SELECT TrangThai FROM PhieuTraHangNhap WITH (UPDLOCK, ROWLOCK) WHERE MaPhieuTraHang = @MaPhieuTraHang", conn, trans))
                        {
                            cmdKt.Parameters.AddWithValue("@MaPhieuTraHang", maPhieuTraHang);
                            object ttHienTai = cmdKt.ExecuteScalar();
                            if (ttHienTai == null || ttHienTai == DBNull.Value || ttHienTai.ToString() != "NHAP")
                            {
                                throw new InvalidOperationException("Phiếu trả hàng không còn ở trạng thái phiếu tạm. Vui lòng tải lại phiếu.");
                            }
                        }

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
                        // 1. Đọc lại phiếu (khóa dòng) để lấy liên kết phiếu nhập gốc và số tiền đã trừ vào nợ phiếu nhập
                        object maPhieuNhapGoc = DBNull.Value;
                        decimal tienDaTruNoPhieuNhap = 0;
                        string sqlDoc = @"
                            SELECT MaPhieuNhap, TongTienHang - GiamGia - CanThuNcc AS TienDaTruNo
                            FROM PhieuTraHangNhap WITH (UPDLOCK, ROWLOCK)
                            WHERE MaPhieuTraHang = @MaPhieuTraHang AND TrangThai = 'DA_TRA_HANG'";
                        using (SqlCommand cmdDoc = new SqlCommand(sqlDoc, conn, trans))
                        {
                            cmdDoc.Parameters.AddWithValue("@MaPhieuTraHang", maPhieuTraHang);
                            using (SqlDataReader rd = cmdDoc.ExecuteReader())
                            {
                                if (!rd.Read())
                                {
                                    throw new InvalidOperationException("Đơn trả hàng không còn ở trạng thái 'Đã trả hàng'. Vui lòng tải lại phiếu.");
                                }
                                maPhieuNhapGoc = rd["MaPhieuNhap"];
                                tienDaTruNoPhieuNhap = Math.Max(0, Convert.ToDecimal(rd["TienDaTruNo"]));
                            }
                        }

                        string sqlHuy = "UPDATE PhieuTraHangNhap SET TrangThai = 'HUY' WHERE MaPhieuTraHang = @MaPhieuTraHang AND TrangThai = 'DA_TRA_HANG'";
                        using (SqlCommand cmdHuy = new SqlCommand(sqlHuy, conn, trans))
                        {
                            cmdHuy.Parameters.AddWithValue("@MaPhieuTraHang", maPhieuTraHang);
                            cmdHuy.ExecuteNonQuery();
                        }

                        // 2. Phiếu trả lập theo phiếu nhập: hoàn lại phần đã trừ vào nợ cần trả NCC của phiếu nhập gốc
                        if (maPhieuNhapGoc != DBNull.Value && tienDaTruNoPhieuNhap > 0)
                        {
                            string sqlHoanNo = "UPDATE PhieuNhap SET CanTraNcc = CanTraNcc + @TienHoan WHERE MaPhieuNhap = @MaPhieuNhap";
                            using (SqlCommand cmdHoanNo = new SqlCommand(sqlHoanNo, conn, trans))
                            {
                                cmdHoanNo.Parameters.AddWithValue("@TienHoan", tienDaTruNoPhieuNhap);
                                cmdHoanNo.Parameters.AddWithValue("@MaPhieuNhap", maPhieuNhapGoc);
                                cmdHoanNo.ExecuteNonQuery();
                            }
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
            catch (InvalidOperationException exNghiepVu)
            {
                MessageBox.Show(exNghiepVu.Message, "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                NapThongTinPhieu();
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

            // Người trả hàng là nhân viên gắn với tài khoản đăng nhập (khóa ngoại NhanVien.MaNV)
            if (!SessionContext.MaNV.HasValue || SessionContext.MaNV.Value <= 0)
            {
                MessageBox.Show("Tài khoản chưa được gắn với nhân viên, không thể lập chứng từ.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            int maNvTra = SessionContext.MaNV.Value;

            try
            {
                DataProcessing.ChangeDataTransaction((conn, trans) =>
                {
                    // Kiểm tra lại tồn kho ngay trong giao dịch (gộp theo mặt hàng) để chặn âm tồn
                    string sqlKiemTon = @"
                        SELECT ct.MaHang, h.MaHangNoiBo, h.TenHang, SUM(ct.SoLuong) AS SoLuongTra,
                               ISNULL((SELECT SUM(t.SoLuongTon) FROM vw_TonKhoHienTai t WHERE t.MaKho = @MaKho AND t.MaHang = ct.MaHang), 0) AS TonHienTai
                        FROM ChiTietPhieuTraHangNhap ct
                        JOIN HangHoa h ON ct.MaHang = h.MaHang
                        WHERE ct.MaPhieuTraHang = @MaPhieuTraHang
                        GROUP BY ct.MaHang, h.MaHangNoiBo, h.TenHang";
                    DataTable dtKiemTon = new DataTable();
                    using (SqlCommand cmdTon = new SqlCommand(sqlKiemTon, conn, trans))
                    {
                        cmdTon.Parameters.AddWithValue("@MaKho", maKho);
                        cmdTon.Parameters.AddWithValue("@MaPhieuTraHang", maPhieuTraHang);
                        using (SqlDataAdapter da = new SqlDataAdapter(cmdTon))
                        {
                            da.Fill(dtKiemTon);
                        }
                    }
                    foreach (DataRow r in dtKiemTon.Rows)
                    {
                        decimal slTraKt = Convert.ToDecimal(r["SoLuongTra"]);
                        decimal tonKt = Convert.ToDecimal(r["TonHienTai"]);
                        if (tonKt < slTraKt)
                        {
                            throw new InvalidOperationException(
                                $"Không thể hoàn thành phiếu trả hàng vì mặt hàng '{r["MaHangNoiBo"]} - {r["TenHang"]}' không đủ số lượng tồn trong kho (Tồn hiện tại: {tonKt:N0}, Cần trả: {slTraKt:N0})!\nNếu hoàn thành sẽ dẫn đến âm kho.");
                        }
                    }

                    // Phiếu trả lập theo phiếu nhập: kiểm tra lại SL còn được trả và trừ nợ cần trả NCC của phiếu nhập gốc
                    // (khi hủy sẽ hoàn lại đúng phần TongTienHang - GiamGia - CanThuNcc đã trừ)
                    object maPhieuNhapGoc = DBNull.Value;
                    decimal tienTraHang = 0;
                    using (SqlCommand cmdDoc = new SqlCommand("SELECT MaPhieuNhap, TongTienHang - GiamGia AS TienTraHang FROM PhieuTraHangNhap WITH (UPDLOCK, ROWLOCK) WHERE MaPhieuTraHang = @MaPhieuTraHang AND TrangThai = 'NHAP'", conn, trans))
                    {
                        cmdDoc.Parameters.AddWithValue("@MaPhieuTraHang", maPhieuTraHang);
                        using (SqlDataReader rd = cmdDoc.ExecuteReader())
                        {
                            if (!rd.Read())
                            {
                                throw new InvalidOperationException("Phiếu trả hàng không còn ở trạng thái phiếu tạm. Vui lòng tải lại phiếu.");
                            }
                            maPhieuNhapGoc = rd["MaPhieuNhap"];
                            tienTraHang = Math.Max(0, Convert.ToDecimal(rd["TienTraHang"]));
                        }
                    }

                    if (maPhieuNhapGoc != DBNull.Value)
                    {
                        decimal canTraNccGoc;
                        using (SqlCommand cmdGoc = new SqlCommand("SELECT TrangThai, CanTraNcc FROM PhieuNhap WITH (UPDLOCK, ROWLOCK) WHERE MaPhieuNhap = @MaPhieuNhap", conn, trans))
                        {
                            cmdGoc.Parameters.AddWithValue("@MaPhieuNhap", maPhieuNhapGoc);
                            using (SqlDataReader rd = cmdGoc.ExecuteReader())
                            {
                                if (!rd.Read() || rd["TrangThai"].ToString() != "DA_XAC_NHAN")
                                {
                                    throw new InvalidOperationException("Phiếu nhập gốc không còn ở trạng thái 'Đã nhập hàng', không thể hoàn thành phiếu trả hàng.");
                                }
                                canTraNccGoc = Convert.ToDecimal(rd["CanTraNcc"]);
                            }
                        }

                        string sqlConDuocTra = @"
                            SELECT ct.MaHang, h.MaHangNoiBo, h.TenHang, SUM(ct.SoLuong) AS SoLuongTra,
                                   ISNULL((SELECT SUM(ctn.SoLuong) FROM ChiTietPhieuNhap ctn
                                           WHERE ctn.MaPhieuNhap = @MaPhieuNhap AND ctn.MaHang = ct.MaHang), 0) AS SoLuongNhap,
                                   ISNULL((SELECT SUM(ctt.SoLuong) FROM ChiTietPhieuTraHangNhap ctt
                                           JOIN PhieuTraHangNhap pt ON ctt.MaPhieuTraHang = pt.MaPhieuTraHang
                                           WHERE pt.MaPhieuNhap = @MaPhieuNhap AND pt.TrangThai IN ('NHAP', 'DA_TRA_HANG')
                                             AND pt.MaPhieuTraHang <> @MaPhieuTraHang AND ctt.MaHang = ct.MaHang), 0) AS SoLuongDaTraKhac
                            FROM ChiTietPhieuTraHangNhap ct
                            JOIN HangHoa h ON ct.MaHang = h.MaHang
                            WHERE ct.MaPhieuTraHang = @MaPhieuTraHang
                            GROUP BY ct.MaHang, h.MaHangNoiBo, h.TenHang";
                        DataTable dtConDuocTra = new DataTable();
                        using (SqlCommand cmdCon = new SqlCommand(sqlConDuocTra, conn, trans))
                        {
                            cmdCon.Parameters.AddWithValue("@MaPhieuNhap", maPhieuNhapGoc);
                            cmdCon.Parameters.AddWithValue("@MaPhieuTraHang", maPhieuTraHang);
                            using (SqlDataAdapter da = new SqlDataAdapter(cmdCon))
                            {
                                da.Fill(dtConDuocTra);
                            }
                        }
                        foreach (DataRow r in dtConDuocTra.Rows)
                        {
                            decimal slTraKt = Convert.ToDecimal(r["SoLuongTra"]);
                            decimal slConDuocTra = Convert.ToDecimal(r["SoLuongNhap"]) - Convert.ToDecimal(r["SoLuongDaTraKhac"]);
                            if (slTraKt > slConDuocTra)
                            {
                                throw new InvalidOperationException(
                                    $"Mặt hàng '{r["MaHangNoiBo"]} - {r["TenHang"]}' chỉ còn được trả tối đa {Math.Max(0, slConDuocTra):N0} theo phiếu nhập gốc (Cần trả: {slTraKt:N0}).");
                            }
                        }

                        decimal truNo = Math.Min(tienTraHang, Math.Max(0, canTraNccGoc));
                        decimal canThuNccMoi = tienTraHang - truNo;

                        using (SqlCommand cmdTruNo = new SqlCommand("UPDATE PhieuNhap SET CanTraNcc = CanTraNcc - @TruNo WHERE MaPhieuNhap = @MaPhieuNhap", conn, trans))
                        {
                            cmdTruNo.Parameters.AddWithValue("@TruNo", truNo);
                            cmdTruNo.Parameters.AddWithValue("@MaPhieuNhap", maPhieuNhapGoc);
                            cmdTruNo.ExecuteNonQuery();
                        }

                        using (SqlCommand cmdCanThu = new SqlCommand("UPDATE PhieuTraHangNhap SET CanThuNcc = @CanThuNcc, DaThuNcc = CASE WHEN DaThuNcc > @CanThuNcc THEN @CanThuNcc ELSE DaThuNcc END WHERE MaPhieuTraHang = @MaPhieuTraHang", conn, trans))
                        {
                            cmdCanThu.Parameters.AddWithValue("@CanThuNcc", canThuNccMoi);
                            cmdCanThu.Parameters.AddWithValue("@MaPhieuTraHang", maPhieuTraHang);
                            cmdCanThu.ExecuteNonQuery();
                        }
                    }

                    string sqlUpdate = @"
                        UPDATE PhieuTraHangNhap
                        SET TrangThai = 'DA_TRA_HANG',
                            NgayTra = GETDATE(),
                            MaNvtra = @MaNvtra
                        WHERE MaPhieuTraHang = @MaPhieuTraHang AND TrangThai = 'NHAP'";

                    using (SqlCommand cmd = new SqlCommand(sqlUpdate, conn, trans))
                    {
                        cmd.Parameters.AddWithValue("@MaPhieuTraHang", maPhieuTraHang);
                        cmd.Parameters.AddWithValue("@MaNvtra", maNvTra);
                        if (cmd.ExecuteNonQuery() == 0)
                        {
                            throw new InvalidOperationException("Phiếu trả hàng không còn ở trạng thái phiếu tạm. Vui lòng tải lại phiếu.");
                        }
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
            catch (InvalidOperationException exNghiepVu)
            {
                // Vi phạm ràng buộc nghiệp vụ -> giao dịch đã rollback
                MessageBox.Show(exNghiepVu.Message, "Chặn âm tồn kho", MessageBoxButtons.OK, MessageBoxIcon.Warning);
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
