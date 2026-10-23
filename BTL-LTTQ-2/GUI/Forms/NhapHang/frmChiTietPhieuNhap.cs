using BTL_LTTQ_2.BLL;
using BTL_LTTQ_2.DAL;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Text;
using System.Windows.Forms;

namespace BTL_LTTQ_2.GUI.Forms.NhapHang
{
    public partial class frmChiTietPhieuNhap : Form
    {
        private int maPhieuNhap;
        private string maPhieu;
        private string currentTrangThai;
        private int maDonViCapHang;
        private int maKho;
        private decimal canTraNccHienTai;
        private decimal tongTienHienTai;
        private DataTable dtChiTietData;

        public frmChiTietPhieuNhap(int maPhieuNhap)
        {
            InitializeComponent();
            this.maPhieuNhap = maPhieuNhap;
            CauHinhDataGridView();
        }

        private void CauHinhDataGridView()
        {
            dgvChiTiet.AutoGenerateColumns = false;
            dgvChiTiet.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(248, 250, 252);
            dgvChiTiet.DefaultCellStyle.SelectionBackColor = Color.FromArgb(224, 231, 255);
            dgvChiTiet.DefaultCellStyle.SelectionForeColor = Color.FromArgb(15, 23, 42);

            colStt.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            colMaHangNoiBo.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            colDonViTinh.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            colSoLuong.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            colDonGia.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            colGiamGia.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            colThanhTien.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;

            colSoLuong.DefaultCellStyle.Format = "N0";
            colDonGia.DefaultCellStyle.Format = "N0";
            colGiamGia.DefaultCellStyle.Format = "N0";
            colThanhTien.DefaultCellStyle.Format = "N0";
        }

        private void frmChiTietPhieuNhap_Load(object sender, EventArgs e)
        {
            NapThongTinPhieu();
        }

        public void NapThongTinPhieu()
        {
            try
            {
                // 1. Nạp thông tin phiếu nhập
                string sqlHeader = @"
                    SELECT 
                        pn.MaPhieuNhap,
                        ISNULL(pn.MaPhieu, 'PN' + RIGHT('000000' + CAST(pn.MaPhieuNhap AS VARCHAR(10)), 6)) AS MaPhieu,
                        pn.MaKho,
                        ISNULL(k.TenKho, N'Kho trung tâm') AS TenKho,
                        ISNULL(pn.MaDonViCapHang, 0) AS MaDonViCapHang,
                        ISNULL(ncc.TenDonVi, N'Nhà cung cấp lẻ') AS TenDonVi,
                        ISNULL(ncc.MaNcc, 'NCC' + RIGHT('00000' + CAST(ISNULL(ncc.MaDonViCapHang, 0) AS VARCHAR(10)), 5)) AS MaNcc,
                        pn.MaNVLap,
                        ISNULL(nvLap.HoTen, N'Quản trị viên') AS TenNvLap,
                        pn.MaNVXacNhan,
                        ISNULL(nvNhap.HoTen, nvLap.HoTen) AS TenNvNhap,
                        pn.NgayLap,
                        pn.NgayXacNhan,
                        pn.TrangThai,
                        ISNULL(pn.SoHoaDon, '') AS SoHoaDon,
                        ISNULL(pn.GhiChu, '') AS GhiChu,
                        pn.GiamGia,
                        pn.TongTien,
                        pn.CanTraNcc
                    FROM PhieuNhap pn
                    LEFT JOIN Kho k ON pn.MaKho = k.MaKho
                    LEFT JOIN DonViCapHang ncc ON pn.MaDonViCapHang = ncc.MaDonViCapHang
                    LEFT JOIN NhanVien nvLap ON pn.MaNVLap = nvLap.MaNV
                    LEFT JOIN NhanVien nvNhap ON pn.MaNVXacNhan = nvNhap.MaNV
                    WHERE pn.MaPhieuNhap = @MaPhieuNhap";

                DataTable dtHeader = DataProcessing.ReadData(sqlHeader, new SqlParameter[] {
                    new SqlParameter("@MaPhieuNhap", maPhieuNhap)
                });

                if (dtHeader.Rows.Count == 0)
                {
                    MessageBox.Show("Không tìm thấy thông tin phiếu nhập này!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    this.Close();
                    return;
                }

                DataRow row = dtHeader.Rows[0];
                maPhieu = row["MaPhieu"].ToString();
                currentTrangThai = row["TrangThai"].ToString();
                maDonViCapHang = Convert.ToInt32(row["MaDonViCapHang"]);
                maKho = Convert.ToInt32(row["MaKho"]);
                canTraNccHienTai = Convert.ToDecimal(row["CanTraNcc"]);
                tongTienHienTai = Convert.ToDecimal(row["TongTien"]);

                lblMaPhieuHienThi.Text = maPhieu;
                lblKhoChiNhanh.Text = "Kho: " + row["TenKho"].ToString();
                lblNguoiTaoVal.Text = row["TenNvLap"].ToString();
                lblNguoiNhapVal.Text = row["TenNvNhap"].ToString();
                lblNgayNhapVal.Text = Convert.ToDateTime(row["NgayLap"]).ToString("dd/MM/yyyy HH:mm");
                lblTenNccVal.Text = $"{row["TenDonVi"]} ({row["MaNcc"]})";
                lblSoHoaDonVal.Text = string.IsNullOrEmpty(row["SoHoaDon"].ToString()) ? "-" : row["SoHoaDon"].ToString();
                txtGhiChu.Text = row["GhiChu"].ToString();

                // Cập nhật trạng thái hiển thị
                if (currentTrangThai == "DA_XAC_NHAN")
                {
                    lblTrangThai.Text = "Đã nhập hàng";
                    lblTrangThai.ForeColor = Color.FromArgb(22, 163, 74);
                    btnXoa.Enabled = true;
                    btnTraHangNhap.Enabled = true;
                }
                else if (currentTrangThai == "NHAP")
                {
                    lblTrangThai.Text = "Phiếu tạm";
                    lblTrangThai.ForeColor = Color.FromArgb(37, 99, 235);
                    btnXoa.Enabled = true;
                    btnTraHangNhap.Enabled = false; // Chưa nhập kho nên không thể trả hàng
                }
                else if (currentTrangThai == "HUY")
                {
                    lblTrangThai.Text = "Đã hủy";
                    lblTrangThai.ForeColor = Color.FromArgb(100, 116, 139);
                    btnXoa.Enabled = false; // Đã hủy rồi
                    btnTraHangNhap.Enabled = false;
                }

                // 2. Nạp danh sách chi tiết hàng hóa
                string sqlLines = @"
                    SELECT 
                        ROW_NUMBER() OVER (ORDER BY ct.MaCTPhieuNhap ASC) AS STT,
                        ct.MaCTPhieuNhap,
                        ct.MaHang,
                        h.MaHangNoiBo,
                        h.TenHang,
                        ISNULL(d.TenDvt, N'Cái') AS TenDvt,
                        ct.SoLuong,
                        ct.DonGiaNhap,
                        ct.GiamGia,
                        ct.ThanhTien
                    FROM ChiTietPhieuNhap ct
                    JOIN HangHoa h ON ct.MaHang = h.MaHang
                    LEFT JOIN DonViTinh d ON h.MaDvt = d.MaDvt
                    WHERE ct.MaPhieuNhap = @MaPhieuNhap
                    ORDER BY ct.MaCTPhieuNhap ASC";

                dtChiTietData = DataProcessing.ReadData(sqlLines, new SqlParameter[] {
                    new SqlParameter("@MaPhieuNhap", maPhieuNhap)
                });

                dgvChiTiet.DataSource = dtChiTietData;

                // 3. Tính toán tổng kết hiển thị
                decimal tongTienHang = 0;
                foreach (DataRow r in dtChiTietData.Rows)
                {
                    tongTienHang += Convert.ToDecimal(r["ThanhTien"]);
                }

                decimal giamGia = Convert.ToDecimal(row["GiamGia"]);
                decimal tongCong = Convert.ToDecimal(row["TongTien"]);
                decimal canTra = Convert.ToDecimal(row["CanTraNcc"]);
                decimal daTra = tongCong - canTra;
                if (daTra < 0) daTra = 0;

                lblSoLuongMatHangVal.Text = $"Số lượng mặt hàng: {dtChiTietData.Rows.Count}";
                lblTongTienHangVal.Text = tongTienHang.ToString("N0");
                lblGiamGiaVal.Text = giamGia.ToString("N0");
                lblTongCongVal.Text = tongCong.ToString("N0");
                lblTienDaTraVal.Text = daTra.ToString("N0");
                lblCanTraNccVal.Text = canTra.ToString("N0");

                if (canTra > 0)
                {
                    lblCanTraNccVal.ForeColor = Color.Red;
                }
                else
                {
                    lblCanTraNccVal.ForeColor = Color.FromArgb(30, 41, 59);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi nạp thông tin chi tiết: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            if (currentTrangThai == "HUY")
            {
                MessageBox.Show("Phiếu nhập này đã bị hủy trước đó!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            if (currentTrangThai == "NHAP")
            {
                // Phiếu tạm: Xóa hoàn toàn
                DialogResult dr = MessageBox.Show(
                    $"Bạn có chắc chắn muốn XÓA phiếu tạm '{maPhieu}' khỏi hệ thống?",
                    "Xác nhận xóa phiếu",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question
                );

                if (dr == DialogResult.Yes)
                {
                    try
                    {
                        DataProcessing.ChangeDataTransaction((conn, trans) =>
                        {
                            SqlCommand cmd1 = new SqlCommand("DELETE FROM ChiTietPhieuNhap WHERE MaPhieuNhap = @MaPhieuNhap", conn, trans);
                            cmd1.Parameters.AddWithValue("@MaPhieuNhap", maPhieuNhap);
                            cmd1.ExecuteNonQuery();

                            SqlCommand cmd2 = new SqlCommand("DELETE FROM PhieuNhap WHERE MaPhieuNhap = @MaPhieuNhap", conn, trans);
                            cmd2.Parameters.AddWithValue("@MaPhieuNhap", maPhieuNhap);
                            cmd2.ExecuteNonQuery();

                            // Ghi nhật ký
                            SqlCommand cmdLog = new SqlCommand(@"
                                INSERT INTO NhatKyThaoTac (MaTaiKhoan, ThoiDiem, ThaoTac, LoaiDoiTuong, MaDoiTuong, NoiDung)
                                VALUES (@MaTaiKhoan, GETDATE(), 'XOA_PHIEU_TAM', 'PhieuNhap', @MaDoiTuong, @NoiDung)", conn, trans);
                            cmdLog.Parameters.AddWithValue("@MaTaiKhoan", SessionContext.MaTaiKhoan > 0 ? SessionContext.MaTaiKhoan : 1);
                            cmdLog.Parameters.AddWithValue("@MaDoiTuong", maPhieu);
                            cmdLog.Parameters.AddWithValue("@NoiDung", $"Xóa hoàn toàn phiếu tạm {maPhieu} (ID: {maPhieuNhap})");
                            cmdLog.ExecuteNonQuery();
                        });

                        MessageBox.Show("Đã xóa phiếu tạm thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        this.DialogResult = DialogResult.OK;
                        this.Close();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Lỗi khi xóa phiếu: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            else if (currentTrangThai == "DA_XAC_NHAN")
            {
                // Phiếu đã xác nhận: Chuyển trạng thái sang HUY (theo đúng Luật 5 và Luật 02-nghiep-vu-kho)
                DialogResult dr = MessageBox.Show(
                    $"Phiếu nhập '{maPhieu}' đã hoàn thành nhập kho.\n\n" +
                    "Thao tác xóa sẽ chuyển trạng thái phiếu sang 'Đã hủy', tự động trừ hoàn lại tồn kho trong hệ thống và giảm trừ công nợ nhà cung cấp.\n\n" +
                    "Bạn có chắc chắn muốn XÓA/HỦY phiếu này không?",
                    "Xác nhận hủy/xóa phiếu nhập",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Warning
                );

                if (dr == DialogResult.Yes)
                {
                    try
                    {
                        // Kiểm tra an toàn: Nếu trừ tồn kho có làm âm kho không?
                        foreach (DataRow r in dtChiTietData.Rows)
                        {
                            int maHang = Convert.ToInt32(r["MaHang"]);
                            decimal slNhap = Convert.ToDecimal(r["SoLuong"]);
                            string sku = r["MaHangNoiBo"].ToString();
                            string ten = r["TenHang"].ToString();

                            string checkTonSql = "SELECT ISNULL(SoLuongTon, 0) FROM vw_TonKhoHienTai WHERE MaKho = @MaKho AND MaHang = @MaHang";
                            object tonObj = DataProcessing.ReadValue(checkTonSql, new SqlParameter[] {
                                new SqlParameter("@MaKho", maKho),
                                new SqlParameter("@MaHang", maHang)
                            });
                            decimal tonHienTai = tonObj != null && tonObj != DBNull.Value ? Convert.ToDecimal(tonObj) : 0m;

                            if (tonHienTai < slNhap)
                            {
                                MessageBox.Show(
                                    $"Không thể hủy phiếu nhập vì mặt hàng '{sku} - {ten}' đã phát sinh xuất bán/chuyển kho (Tồn hiện tại: {tonHienTai:N0}, Số lượng phiếu cần trừ: {slNhap:N0}).\nNếu hủy sẽ dẫn đến âm kho!",
                                    "Chặn âm kho",
                                    MessageBoxButtons.OK,
                                    MessageBoxIcon.Stop
                                );
                                return;
                            }
                        }

                        DataProcessing.ChangeDataTransaction((conn, trans) =>
                        {
                            // 1. Cập nhật trạng thái phiếu thành HUY
                            SqlCommand cmd = new SqlCommand("UPDATE PhieuNhap SET TrangThai = 'HUY' WHERE MaPhieuNhap = @MaPhieuNhap", conn, trans);
                            cmd.Parameters.AddWithValue("@MaPhieuNhap", maPhieuNhap);
                            cmd.ExecuteNonQuery();

                            // 2. Ghi nhật ký thao tác
                            SqlCommand cmdLog = new SqlCommand(@"
                                INSERT INTO NhatKyThaoTac (MaTaiKhoan, ThoiDiem, ThaoTac, LoaiDoiTuong, MaDoiTuong, NoiDung)
                                VALUES (@MaTaiKhoan, GETDATE(), 'HUY_PHIEU_NHAP', 'PhieuNhap', @MaDoiTuong, @NoiDung)", conn, trans);
                            cmdLog.Parameters.AddWithValue("@MaTaiKhoan", SessionContext.MaTaiKhoan > 0 ? SessionContext.MaTaiKhoan : 1);
                            cmdLog.Parameters.AddWithValue("@MaDoiTuong", maPhieu);
                            cmdLog.Parameters.AddWithValue("@NoiDung", $"Hủy/xóa phiếu nhập {maPhieu} (ID: {maPhieuNhap}), hoàn lại tồn kho và nợ NCC");
                            cmdLog.ExecuteNonQuery();
                        });

                        MessageBox.Show($"Đã hủy/xóa phiếu nhập '{maPhieu}' thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        NapThongTinPhieu();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Lỗi khi hủy phiếu nhập: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        private void btnXuatFile_Click(object sender, EventArgs e)
        {
            if (dtChiTietData == null || dtChiTietData.Rows.Count == 0)
            {
                MessageBox.Show("Không có dữ liệu mặt hàng để xuất file!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            using (SaveFileDialog sfd = new SaveFileDialog())
            {
                sfd.Filter = "CSV File (*.csv)|*.csv|All Files (*.*)|*.*";
                sfd.FileName = $"ChiTiet_{maPhieu}_{DateTime.Now:yyyyMMdd_HHmmss}.csv";
                if (sfd.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        StringBuilder sb = new StringBuilder();
                        sb.AppendLine($"PHIẾU NHẬP HÀNG: {maPhieu}");
                        sb.AppendLine($"Ngày nhập: {lblNgayNhapVal.Text}");
                        sb.AppendLine($"Nhà cung cấp: \"{lblTenNccVal.Text}\"");
                        sb.AppendLine($"Người tạo: {lblNguoiTaoVal.Text}");
                        sb.AppendLine($"Người nhập: {lblNguoiNhapVal.Text}");
                        sb.AppendLine($"Trạng thái: {lblTrangThai.Text}");
                        sb.AppendLine($"Ghi chú: \"{txtGhiChu.Text.Replace("\"", "\"\"")}\"");
                        sb.AppendLine("");

                        sb.AppendLine("STT,Mã hàng,Tên hàng,ĐVT,Số lượng,Đơn giá,Giảm giá,Thành tiền");
                        foreach (DataRow r in dtChiTietData.Rows)
                        {
                            int stt = Convert.ToInt32(r["STT"]);
                            string sku = r["MaHangNoiBo"].ToString();
                            string ten = $"\"{r["TenHang"].ToString().Replace("\"", "\"\"")}\"";
                            string dvt = r["TenDvt"].ToString();
                            decimal sl = Convert.ToDecimal(r["SoLuong"]);
                            decimal dg = Convert.ToDecimal(r["DonGiaNhap"]);
                            decimal gg = Convert.ToDecimal(r["GiamGia"]);
                            decimal tt = Convert.ToDecimal(r["ThanhTien"]);

                            sb.AppendLine($"{stt},{sku},{ten},{dvt},{sl},{dg},{gg},{tt}");
                        }

                        sb.AppendLine("");
                        sb.AppendLine($",,,,,Tổng tiền hàng:,{lblTongTienHangVal.Text.Replace(",", "")}");
                        sb.AppendLine($",,,,,Giảm giá:,{lblGiamGiaVal.Text.Replace(",", "")}");
                        sb.AppendLine($",,,,,Tổng cộng:,{lblTongCongVal.Text.Replace(",", "")}");
                        sb.AppendLine($",,,,,Tiền đã trả NCC:,{lblTienDaTraVal.Text.Replace(",", "")}");
                        sb.AppendLine($",,,,,Cần trả NCC:,{lblCanTraNccVal.Text.Replace(",", "")}");

                        File.WriteAllText(sfd.FileName, sb.ToString(), new UTF8Encoding(true));
                        MessageBox.Show("Xuất file chi tiết đơn nhập thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Lỗi xuất file: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        private void btnTraHangNhap_Click(object sender, EventArgs e)
        {
            if (currentTrangThai != "DA_XAC_NHAN")
            {
                MessageBox.Show("Chỉ có thể trả hàng cho phiếu đã hoàn thành nhập kho!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            using (frmTraHangNhap frm = new frmTraHangNhap(maPhieuNhap))
            {
                if (frm.ShowDialog() == DialogResult.OK)
                {
                    // Trả hàng xong -> Nạp lại chi tiết để thấy công nợ đã giảm
                    NapThongTinPhieu();
                }
            }
        }

        private void btnDong_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
