using BTL_LTTQ_2.Classes;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Text;
using System.Windows.Forms;

namespace BTL_LTTQ_2.GUI.Forms.ChuyenKho
{
    public partial class frmChiTietChuyenKho : Form
    {
        private int maPhieuChuyen;
        private string maPhieu;
        private string trangThai;
        private int maKhoNguon;
        private int maKhoDich;
        private string tenKhoNguon;
        private string tenKhoDich;
        private DataTable dtChiTiet;

        public frmChiTietChuyenKho(int maPhieuChuyen)
        {
            InitializeComponent();
            this.maPhieuChuyen = maPhieuChuyen;
        }

        private void frmChiTietChuyenKho_Load(object sender, EventArgs e)
        {
            try
            {
                typeof(DataGridView).InvokeMember("DoubleBuffered",
                    System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.SetProperty,
                    null, dgvChiTiet, new object[] { true });
            }
            catch { }

            dgvChiTiet.AutoGenerateColumns = false;
            dgvChiTiet.BorderStyle = BorderStyle.None;
            dgvChiTiet.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(248, 250, 252);
            dgvChiTiet.DefaultCellStyle.SelectionBackColor = Color.FromArgb(224, 231, 255);
            dgvChiTiet.DefaultCellStyle.SelectionForeColor = Color.FromArgb(30, 41, 59);

            colStt.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            colMaHang.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            colDonViTinh.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            colSoLuongChuyen.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            colSoLuongChuyen.DefaultCellStyle.Format = "N0";
            colSoLuongNhan.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            colHaoHut.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            colDonGia.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            colDonGia.DefaultCellStyle.Format = "N0";
            colThanhTien.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            colThanhTien.DefaultCellStyle.Format = "N0";

            TaiThongTinPhieu();
        }

        private void TaiThongTinPhieu()
        {
            try
            {
                // 1. Đọc Header
                string sqlHeader = @"SELECT pc.MaPhieuChuyen, pc.MaPhieu, pc.MaKhoNguon, pc.MaKhoDich,
                                           kn.TenKho AS TenKhoNguon, kd.TenKho AS TenKhoDich,
                                           pc.NgayLap, pc.ThoiDiemXuat, pc.ThoiDiemNhan, pc.TrangThai,
                                           pc.GhiChu, pc.GhiChuNhan,
                                           pc.TongSoLuongChuyen, pc.TongSoLuongNhan, pc.TongGiaTriChuyen,
                                           ISNULL(nvLap.HoTen, N'Chưa rõ') AS TenNvLap,
                                           ISNULL(nvXuat.HoTen, N'Chưa rõ') AS TenNvXuat,
                                           ISNULL(nvNhan.HoTen, N'Chưa rõ') AS TenNvNhan
                                    FROM PhieuChuyenKho pc
                                    JOIN Kho kn ON pc.MaKhoNguon = kn.MaKho
                                    JOIN Kho kd ON pc.MaKhoDich = kd.MaKho
                                    LEFT JOIN NhanVien nvLap ON pc.MaNVLap = nvLap.MaNV
                                    LEFT JOIN NhanVien nvXuat ON pc.MaNVXuat = nvXuat.MaNV
                                    LEFT JOIN NhanVien nvNhan ON pc.MaNVNhan = nvNhan.MaNV
                                    WHERE pc.MaPhieuChuyen = @MaPhieuChuyen";

                DataTable dtH = DataProcessing.ReadData(sqlHeader, new SqlParameter[] {
                    new SqlParameter("@MaPhieuChuyen", this.maPhieuChuyen)
                });

                if (dtH == null || dtH.Rows.Count == 0)
                {
                    MessageBox.Show("Không tìm thấy thông tin phiếu chuyển kho này!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    this.Close();
                    return;
                }

                DataRow rH = dtH.Rows[0];
                this.maPhieu = rH["MaPhieu"].ToString();
                this.trangThai = rH["TrangThai"].ToString();
                this.maKhoNguon = Convert.ToInt32(rH["MaKhoNguon"]);
                this.maKhoDich = Convert.ToInt32(rH["MaKhoDich"]);
                this.tenKhoNguon = rH["TenKhoNguon"].ToString();
                this.tenKhoDich = rH["TenKhoDich"].ToString();

                lblMaPhieuHienThi.Text = this.maPhieu;
                lblLoTrinh.Text = string.Format("{0} ➜ {1}", this.tenKhoNguon, this.tenKhoDich);
                lblKhoNguonVal.Text = this.tenKhoNguon;
                lblKhoDichVal.Text = this.tenKhoDich;

                lblNguoiLapVal.Text = rH["TenNvLap"].ToString();
                lblNgayLapVal.Text = Convert.ToDateTime(rH["NgayLap"]).ToString("dd/MM/yyyy HH:mm");

                lblNguoiXuatVal.Text = rH["TenNvXuat"] != DBNull.Value && !string.IsNullOrEmpty(rH["TenNvXuat"].ToString()) ? rH["TenNvXuat"].ToString() : "---";
                lblThoiDiemXuatVal.Text = rH["ThoiDiemXuat"] != DBNull.Value ? Convert.ToDateTime(rH["ThoiDiemXuat"]).ToString("dd/MM/yyyy HH:mm") : "---";

                lblNguoiNhanVal.Text = rH["TenNvNhan"] != DBNull.Value && !string.IsNullOrEmpty(rH["TenNvNhan"].ToString()) ? rH["TenNvNhan"].ToString() : "---";
                lblThoiDiemNhanVal.Text = rH["ThoiDiemNhan"] != DBNull.Value ? Convert.ToDateTime(rH["ThoiDiemNhan"]).ToString("dd/MM/yyyy HH:mm") : "---";

                lblGhiChuChuyenVal.Text = rH["GhiChu"] != DBNull.Value && !string.IsNullOrWhiteSpace(rH["GhiChu"].ToString()) ? rH["GhiChu"].ToString() : "(trống)";
                lblGhiChuNhanVal.Text = rH["GhiChuNhan"] != DBNull.Value && !string.IsNullOrWhiteSpace(rH["GhiChuNhan"].ToString()) ? rH["GhiChuNhan"].ToString() : "(trống)";

                // Cập nhật trạng thái màu 3 nhóm chuẩn (Điều luật 19a & 15b)
                CapNhatHienThiTrangThai();

                // 2. Đọc Detail
                string sqlDetail = @"SELECT ct.MaCTPhieuChuyen, ct.MaHang, h.MaHangNoiBo, h.TenHang,
                                           ISNULL(dvt.TenDVT, '') AS TenDVT,
                                           ct.SoLuong,
                                           ct.SoLuongNhan,
                                           ct.DonGiaChuyen,
                                           ct.ThanhTien
                                    FROM ChiTietPhieuChuyenKho ct
                                    JOIN HangHoa h ON ct.MaHang = h.MaHang
                                    LEFT JOIN DonViTinh dvt ON h.MaDvt = dvt.MaDvt
                                    WHERE ct.MaPhieuChuyen = @MaPhieuChuyen
                                    ORDER BY ct.MaCTPhieuChuyen ASC";

                dtChiTiet = DataProcessing.ReadData(sqlDetail, new SqlParameter[] {
                    new SqlParameter("@MaPhieuChuyen", this.maPhieuChuyen)
                });

                dtChiTiet.Columns.Add("STT", typeof(int));
                dtChiTiet.Columns.Add("SoLuongNhanHienThi", typeof(string));
                dtChiTiet.Columns.Add("HaoHut", typeof(decimal));

                decimal tongSlChuyen = 0;
                decimal tongSlNhan = 0;
                decimal tongHaoHut = 0;
                decimal tongGiaTri = 0;

                for (int i = 0; i < dtChiTiet.Rows.Count; i++)
                {
                    DataRow r = dtChiTiet.Rows[i];
                    r["STT"] = i + 1;

                    decimal slChuyen = Convert.ToDecimal(r["SoLuong"]);
                    tongSlChuyen += slChuyen;

                    if (r["SoLuongNhan"] != DBNull.Value)
                    {
                        decimal slNhan = Convert.ToDecimal(r["SoLuongNhan"]);
                        tongSlNhan += slNhan;
                        r["SoLuongNhanHienThi"] = slNhan.ToString("N0");

                        decimal haoHut = slChuyen - slNhan;
                        r["HaoHut"] = haoHut;
                        tongHaoHut += haoHut;
                    }
                    else
                    {
                        r["SoLuongNhanHienThi"] = "---";
                        r["HaoHut"] = 0;
                    }

                    tongGiaTri += Convert.ToDecimal(r["ThanhTien"]);
                }

                dgvChiTiet.DataSource = dtChiTiet;

                lblTongSlChuyenVal.Text = tongSlChuyen.ToString("N0");
                lblTongSlNhanVal.Text = (this.trangThai == "DA_NHAN") ? tongSlNhan.ToString("N0") : "---";
                lblTongHaoHutVal.Text = (this.trangThai == "DA_NHAN") ? tongHaoHut.ToString("N0") : "---";
                lblTongGiaTriVal.Text = tongGiaTri.ToString("N0") + " VNĐ";

                if (tongHaoHut > 0 && this.trangThai == "DA_NHAN")
                {
                    lblTongHaoHutVal.ForeColor = Color.Red;
                }
                else
                {
                    lblTongHaoHutVal.ForeColor = SystemColors.ControlText;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi tải thông tin chi tiết phiếu chuyển kho: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Quyền thao tác phía kho gửi (chuyển hàng, hủy, xóa phiếu tạm): Admin hoặc người đang làm việc tại kho gửi
        /// </summary>
        private bool DuocQuyenKhoGui()
        {
            return SessionContext.IsAdmin || SessionContext.MaKhoHienTai == this.maKhoNguon;
        }

        private void CapNhatHienThiTrangThai()
        {
            bool duocQuyenKhoGui = DuocQuyenKhoGui();

            // Bảng màu 3 nhóm chuẩn toàn hệ thống (Điều luật 15b & 19a)
            if (this.trangThai == "DA_NHAN")
            {
                lblTrangThai.Text = "Đã nhận";
                lblTrangThai.ForeColor = Color.FromArgb(22, 163, 74); // Xanh lá
                btnNhanHang.Visible = false;
                btnHoanThanh.Visible = false;
                btnXoa.Enabled = false; // Khóa vĩnh viễn không được xóa/hủy (Luật 5)
            }
            else if (this.trangThai == "DA_XUAT")
            {
                lblTrangThai.Text = "Đang chuyển";
                lblTrangThai.ForeColor = Color.FromArgb(217, 119, 6); // Vàng amber
                
                // Nút nhận hàng chỉ khả dụng với người ở kho đích hoặc Admin
                bool duocQuyenNhan = SessionContext.IsAdmin || (SessionContext.MaKhoHienTai == this.maKhoDich);
                btnNhanHang.Visible = duocQuyenNhan;
                btnNhanHang.Enabled = duocQuyenNhan;
                btnHoanThanh.Visible = false;
                btnXoa.Enabled = duocQuyenKhoGui; // Cho phép Hủy khi đang chuyển (chỉ kho gửi hoặc Admin)
            }
            else if (this.trangThai == "NHAP")
            {
                lblTrangThai.Text = "Phiếu tạm";
                lblTrangThai.ForeColor = Color.FromArgb(217, 119, 6); // Vàng amber
                btnNhanHang.Visible = false;
                // Phiếu tạm: kho gửi (hoặc Admin) bấm "Chuyển hàng" để xuất kho chuyển đi
                btnHoanThanh.Text = "Chuyển hàng";
                btnHoanThanh.Visible = true;
                btnHoanThanh.Enabled = duocQuyenKhoGui;
                btnXoa.Enabled = duocQuyenKhoGui; // Cho phép Xóa phiếu tạm (chỉ kho gửi hoặc Admin)
            }
            else // HUY
            {
                lblTrangThai.Text = "Đã hủy";
                lblTrangThai.ForeColor = Color.FromArgb(100, 116, 139); // Xám nhạt
                btnNhanHang.Visible = false;
                btnHoanThanh.Visible = false;
                btnXoa.Enabled = false;
            }
        }

        private void dgvChiTiet_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.RowIndex < 0 || e.RowIndex >= dgvChiTiet.Rows.Count) return;

            string colName = dgvChiTiet.Columns[e.ColumnIndex].Name;
            if (colName == "colHaoHut")
            {
                if (e.Value != null && decimal.TryParse(e.Value.ToString(), out decimal haoHut))
                {
                    if (this.trangThai == "DA_NHAN")
                    {
                        e.Value = haoHut.ToString("N0");
                        e.FormattingApplied = true;

                        if (haoHut > 0)
                        {
                            e.CellStyle.ForeColor = Color.Red;
                            e.CellStyle.Font = new Font(dgvChiTiet.Font, FontStyle.Bold);
                        }
                    }
                    else
                    {
                        e.Value = "---";
                        e.FormattingApplied = true;
                    }
                }
            }
        }

        private void btnNhanHang_Click(object sender, EventArgs e)
        {
            if (this.trangThai != "DA_XUAT")
            {
                MessageBox.Show("Chỉ phiếu đang chuyển mới có thể thực hiện nhận hàng!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            using (frmNhanChuyenKho frm = new frmNhanChuyenKho(this.maPhieuChuyen, this.maPhieu, this.tenKhoNguon, this.tenKhoDich))
            {
                if (frm.ShowDialog(this) == DialogResult.OK)
                {
                    TaiThongTinPhieu();
                }
            }
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            if (this.trangThai == "DA_NHAN" || this.trangThai == "HUY")
            {
                MessageBox.Show("Phiếu đã nhận hoặc đã hủy không thể thao tác xóa!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            // Chỉ Admin hoặc người đang làm việc tại kho gửi mới được xóa/hủy phiếu
            if (!DuocQuyenKhoGui()) return;

            if (this.trangThai == "NHAP")
            {
                // Phiếu tạm: Xóa hoàn toàn khỏi CSDL
                DialogResult dr = MessageBox.Show(string.Format("Bạn có chắc chắn muốn XÓA phiếu tạm chuyển kho '{0}' không?\nThao tác này sẽ xóa hoàn toàn phiếu khỏi hệ thống.", this.maPhieu),
                    "Xác nhận xóa phiếu tạm", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (dr == DialogResult.Yes)
                {
                    try
                    {
                        DataProcessing.ChangeDataTransaction((conn, trans) =>
                        {
                            // Chỉ xóa khi phiếu vẫn còn là phiếu tạm (chống xóa phiếu vừa được người khác chuyển hàng)
                            string sqlDelCT = @"DELETE FROM ChiTietPhieuChuyenKho
                                                WHERE MaPhieuChuyen = @ID
                                                  AND EXISTS (SELECT 1 FROM PhieuChuyenKho WHERE MaPhieuChuyen = @ID AND TrangThai = 'NHAP')";
                            using (SqlCommand cmdCT = new SqlCommand(sqlDelCT, conn, trans))
                            {
                                cmdCT.Parameters.AddWithValue("@ID", this.maPhieuChuyen);
                                cmdCT.ExecuteNonQuery();
                            }

                            string sqlDelP = "DELETE FROM PhieuChuyenKho WHERE MaPhieuChuyen = @ID AND TrangThai = 'NHAP'";
                            using (SqlCommand cmdP = new SqlCommand(sqlDelP, conn, trans))
                            {
                                cmdP.Parameters.AddWithValue("@ID", this.maPhieuChuyen);
                                if (cmdP.ExecuteNonQuery() != 1)
                                {
                                    throw new InvalidOperationException("Phiếu chuyển kho không còn ở trạng thái phiếu tạm (có thể đã được chuyển hàng). Vui lòng tải lại dữ liệu.");
                                }
                            }

                            string sqlLog = @"INSERT INTO NhatKyThaoTac (MaTaiKhoan, ThoiDiem, ThaoTac, LoaiDoiTuong, MaDoiTuong, NoiDung)
                                             VALUES (@MaTaiKhoan, GETDATE(), 'XOA', 'CHUYEN_KHO', @MaDoiTuong, @NoiDung)";
                            using (SqlCommand cmdLog = new SqlCommand(sqlLog, conn, trans))
                            {
                                cmdLog.Parameters.AddWithValue("@MaTaiKhoan", SessionContext.MaTaiKhoan);
                                cmdLog.Parameters.AddWithValue("@MaDoiTuong", this.maPhieu);
                                cmdLog.Parameters.AddWithValue("@NoiDung", "Xóa phiếu tạm chuyển kho " + this.maPhieu);
                                cmdLog.ExecuteNonQuery();
                            }
                        });

                        MessageBox.Show("Đã xóa phiếu tạm chuyển kho thành công!", "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        this.DialogResult = DialogResult.OK;
                        this.Close();
                    }
                    catch (InvalidOperationException ex)
                    {
                        MessageBox.Show(ex.Message, "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        TaiThongTinPhieu();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Lỗi xóa phiếu tạm: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            else if (this.trangThai == "DA_XUAT")
            {
                // Đang chuyển: Chuyển sang trạng thái HUY (hoàn tồn kho gửi)
                DialogResult dr = MessageBox.Show(string.Format("Bạn có chắc chắn muốn HỦY phiếu chuyển kho '{0}' không?\n\nLưu ý: Hàng hóa đang chuyển sẽ được hoàn trả lại số lượng tồn cho kho gửi '{1}' ngay lập tức.", this.maPhieu, this.tenKhoNguon),
                    "Xác nhận hủy phiếu chuyển kho", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

                if (dr == DialogResult.Yes)
                {
                    try
                    {
                        DataProcessing.ChangeDataTransaction((conn, trans) =>
                        {
                            // Chỉ hủy khi phiếu vẫn đang chuyển (chống hủy phiếu vừa được kho nhận xác nhận)
                            string sqlHuy = "UPDATE PhieuChuyenKho SET TrangThai = 'HUY' WHERE MaPhieuChuyen = @ID AND TrangThai = 'DA_XUAT'";
                            using (SqlCommand cmdHuy = new SqlCommand(sqlHuy, conn, trans))
                            {
                                cmdHuy.Parameters.AddWithValue("@ID", this.maPhieuChuyen);
                                if (cmdHuy.ExecuteNonQuery() != 1)
                                {
                                    throw new InvalidOperationException("Phiếu chuyển kho không còn ở trạng thái Đang chuyển (có thể kho nhận vừa nhận hàng hoặc phiếu đã bị hủy). Vui lòng tải lại dữ liệu.");
                                }
                            }

                            string sqlLog = @"INSERT INTO NhatKyThaoTac (MaTaiKhoan, ThoiDiem, ThaoTac, LoaiDoiTuong, MaDoiTuong, NoiDung)
                                             VALUES (@MaTaiKhoan, GETDATE(), 'HUY', 'CHUYEN_KHO', @MaDoiTuong, @NoiDung)";
                            using (SqlCommand cmdLog = new SqlCommand(sqlLog, conn, trans))
                            {
                                cmdLog.Parameters.AddWithValue("@MaTaiKhoan", SessionContext.MaTaiKhoan);
                                cmdLog.Parameters.AddWithValue("@MaDoiTuong", this.maPhieu);
                                cmdLog.Parameters.AddWithValue("@NoiDung", "Hủy phiếu đang chuyển kho " + this.maPhieu + ", hoàn trả tồn cho " + this.tenKhoNguon);
                                cmdLog.ExecuteNonQuery();
                            }
                        });

                        MessageBox.Show("Đã hủy phiếu chuyển kho thành công!\nSố lượng tồn đã được phục hồi cho kho gửi.", "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        TaiThongTinPhieu();
                    }
                    catch (InvalidOperationException ex)
                    {
                        MessageBox.Show(ex.Message, "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        TaiThongTinPhieu();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Lỗi hủy phiếu chuyển kho: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        private void btnXuatFile_Click(object sender, EventArgs e)
        {
            if (dtChiTiet == null || dtChiTiet.Rows.Count == 0)
            {
                MessageBox.Show("Không có dữ liệu mặt hàng để xuất file!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                SaveFileDialog sfd = new SaveFileDialog();
                sfd.Filter = "File CSV (*.csv)|*.csv";
                sfd.FileName = string.Format("Phieu_Chuyen_Kho_{0}.csv", this.maPhieu);

                if (sfd.ShowDialog() == DialogResult.OK)
                {
                    StringBuilder sb = new StringBuilder();
                    sb.AppendLine(string.Format("Phiếu chuyển kho: {0},Trạng thái: {1}", this.maPhieu, lblTrangThai.Text));
                    sb.AppendLine(string.Format("Kho gửi: {0},Kho nhận: {1}", this.tenKhoNguon, this.tenKhoDich));
                    sb.AppendLine(string.Format("Người lập: {0},Ngày lập: {1}", lblNguoiLapVal.Text, lblNgayLapVal.Text));
                    sb.AppendLine(string.Format("Người xuất: {0},Ngày xuất: {1}", lblNguoiXuatVal.Text, lblThoiDiemXuatVal.Text));
                    sb.AppendLine(string.Format("Người nhận: {0},Ngày nhận: {1}", lblNguoiNhanVal.Text, lblThoiDiemNhanVal.Text));
                    sb.AppendLine();
                    sb.AppendLine("STT,Mã hàng,Tên hàng,ĐVT,SL chuyển,SL nhận,Hao hụt,Đơn giá chuyển,Thành tiền");

                    foreach (DataRow r in dtChiTiet.Rows)
                    {
                        sb.AppendLine(string.Format("{0},{1},\"{2}\",{3},{4},{5},{6},{7},{8}",
                            r["STT"],
                            r["MaHangNoiBo"],
                            r["TenHang"].ToString().Replace("\"", "\"\""),
                            r["TenDVT"],
                            r["SoLuong"],
                            r["SoLuongNhan"] != DBNull.Value ? r["SoLuongNhan"] : 0,
                            r["HaoHut"],
                            r["DonGiaChuyen"],
                            r["ThanhTien"]));
                    }

                    sb.AppendLine();
                    sb.AppendLine(string.Format("Tổng SL chuyển:,{0},Tổng SL nhận:,{1},Tổng hao hụt:,{2},Tổng giá trị:,{3}",
                        lblTongSlChuyenVal.Text, lblTongSlNhanVal.Text, lblTongHaoHutVal.Text, lblTongGiaTriVal.Text));

                    // Lưu file với UTF-8 BOM
                    File.WriteAllText(sfd.FileName, sb.ToString(), new UTF8Encoding(true));
                    MessageBox.Show("Xuất file CSV thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi xuất file: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnHoanThanh_Click(object sender, EventArgs e)
        {
            if (this.trangThai != "NHAP") return;
            if (!DuocQuyenKhoGui()) return;

            // Bắt buộc tài khoản phải gắn với nhân viên (MaNVXuat tham chiếu NhanVien.MaNV)
            if (!SessionContext.MaNV.HasValue || SessionContext.MaNV.Value <= 0)
            {
                MessageBox.Show("Tài khoản chưa được gắn với nhân viên, không thể lập chứng từ.", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (dtChiTiet == null || dtChiTiet.Rows.Count == 0)
            {
                MessageBox.Show("Phiếu chuyển kho không có mặt hàng nào để chuyển!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // 1. Kiểm tra sơ bộ tồn kho tại kho gửi trước khi xuất chuyển (thông báo thân thiện)
            if (dtChiTiet != null && dtChiTiet.Rows.Count > 0)
            {
                foreach (DataRow r in dtChiTiet.Rows)
                {
                    int maHang = Convert.ToInt32(r["MaHang"]);
                    decimal slChuyen = Convert.ToDecimal(r["SoLuong"]);
                    string sku = r["MaHangNoiBo"].ToString();
                    string ten = r["TenHang"].ToString();

                    string sqlCheck = "SELECT SoLuongTon FROM vw_TonKhoHienTai WHERE MaHang = @MaHang AND MaKho = @MaKho";
                    object tonObj = DataProcessing.ReadValue(sqlCheck, new SqlParameter[] {
                        new SqlParameter("@MaHang", maHang),
                        new SqlParameter("@MaKho", this.maKhoNguon)
                    });
                    decimal tonKho = (tonObj != null && tonObj != DBNull.Value) ? Convert.ToDecimal(tonObj) : 0m;

                    if (tonKho < slChuyen)
                    {
                        MessageBox.Show(
                            $"Không thể chuyển hàng vì mặt hàng '{sku} - {ten}' không đủ số lượng tồn tại kho gửi '{this.tenKhoNguon}' (Tồn hiện tại: {tonKho:N0}, Cần chuyển: {slChuyen:N0})!",
                            "Chặn âm kho gửi",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Warning);
                        return;
                    }
                }
            }

            DialogResult dr = MessageBox.Show(
                $"Bạn có chắc chắn muốn CHUYỂN HÀNG (Xuất chuyển) phiếu chuyển kho '{this.maPhieu}' không?\n\nSau khi chuyển hàng:\n- Hàng hóa sẽ được xuất khỏi kho gửi '{this.tenKhoNguon}'.\n- Trạng thái phiếu sẽ chuyển sang 'Đang chuyển'.\n- Kho nhận '{this.tenKhoDich}' có thể xác nhận nhận hàng.",
                "Xác nhận chuyển hàng",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            if (dr != DialogResult.Yes) return;

            try
            {
                int maNVXuat = SessionContext.MaNV.Value;

                DataProcessing.ChangeDataTransaction((conn, trans) =>
                {
                    // 1. Kiểm tra lại tồn kho gửi ngay trong giao dịch (cùng conn/trans) cho từng mặt hàng của phiếu
                    string sqlKiemTon = @"
                        SELECT ct.MaHang, h.MaHangNoiBo, h.TenHang,
                               SUM(ct.SoLuong) AS SoLuongChuyen,
                               ISNULL(MAX(tk.SoLuongTon), 0) AS TonKho
                        FROM ChiTietPhieuChuyenKho ct
                        JOIN HangHoa h ON ct.MaHang = h.MaHang
                        LEFT JOIN vw_TonKhoHienTai tk ON tk.MaHang = ct.MaHang AND tk.MaKho = @MaKhoNguon
                        WHERE ct.MaPhieuChuyen = @MaPhieuChuyen
                        GROUP BY ct.MaHang, h.MaHangNoiBo, h.TenHang
                        HAVING SUM(ct.SoLuong) > ISNULL(MAX(tk.SoLuongTon), 0)";

                    using (SqlCommand cmdTon = new SqlCommand(sqlKiemTon, conn, trans))
                    {
                        cmdTon.Parameters.AddWithValue("@MaKhoNguon", this.maKhoNguon);
                        cmdTon.Parameters.AddWithValue("@MaPhieuChuyen", this.maPhieuChuyen);
                        using (SqlDataReader rd = cmdTon.ExecuteReader())
                        {
                            if (rd.Read())
                            {
                                string loi = string.Format("Không thể chuyển hàng vì mặt hàng '{0} - {1}' không đủ số lượng tồn tại kho gửi '{2}' (Tồn hiện tại: {3:N0}, Cần chuyển: {4:N0})!",
                                    rd["MaHangNoiBo"], rd["TenHang"], this.tenKhoNguon, Convert.ToDecimal(rd["TonKho"]), Convert.ToDecimal(rd["SoLuongChuyen"]));
                                throw new InvalidOperationException(loi);
                            }
                        }
                    }

                    // 2. Chuyển trạng thái sang DA_XUAT, chỉ khi phiếu vẫn còn là phiếu tạm
                    string sqlUpdate = @"
                        UPDATE PhieuChuyenKho
                        SET TrangThai = 'DA_XUAT',
                            ThoiDiemXuat = SYSDATETIME(),
                            MaNVXuat = @MaNVXuat
                        WHERE MaPhieuChuyen = @MaPhieuChuyen AND TrangThai = 'NHAP'";

                    using (SqlCommand cmd = new SqlCommand(sqlUpdate, conn, trans))
                    {
                        cmd.Parameters.AddWithValue("@MaPhieuChuyen", this.maPhieuChuyen);
                        cmd.Parameters.AddWithValue("@MaNVXuat", maNVXuat);
                        if (cmd.ExecuteNonQuery() != 1)
                        {
                            throw new InvalidOperationException("Phiếu chuyển kho không còn ở trạng thái phiếu tạm (có thể đã được chuyển hàng hoặc bị xóa). Vui lòng tải lại dữ liệu.");
                        }
                    }

                    string sqlLog = @"
                        INSERT INTO NhatKyThaoTac (MaTaiKhoan, ThoiDiem, ThaoTac, LoaiDoiTuong, MaDoiTuong, NoiDung)
                        VALUES (@MaTaiKhoan, GETDATE(), 'HOAN_THANH_CHUYEN_KHO', 'PhieuChuyenKho', @MaDoiTuong, @NoiDung)";

                    using (SqlCommand cmdLog = new SqlCommand(sqlLog, conn, trans))
                    {
                        cmdLog.Parameters.AddWithValue("@MaTaiKhoan", SessionContext.MaTaiKhoan > 0 ? SessionContext.MaTaiKhoan : 1);
                        cmdLog.Parameters.AddWithValue("@MaDoiTuong", this.maPhieu);
                        cmdLog.Parameters.AddWithValue("@NoiDung", $"Hoàn thành xuất chuyển phiếu {this.maPhieu} từ kho {this.tenKhoNguon} sang {this.tenKhoDich}");
                        cmdLog.ExecuteNonQuery();
                    }
                });

                MessageBox.Show($"Chuyển hàng phiếu '{this.maPhieu}' thành công!\nHàng hóa đang được chuyển đến kho '{this.tenKhoDich}'.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.DialogResult = DialogResult.OK;
                TaiThongTinPhieu();
            }
            catch (InvalidOperationException ex)
            {
                MessageBox.Show(ex.Message, "Chặn âm kho gửi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                TaiThongTinPhieu();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi chuyển hàng: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnDong_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}