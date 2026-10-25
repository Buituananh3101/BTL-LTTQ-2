using BTL_LTTQ_2.Classes;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Windows.Forms;

namespace BTL_LTTQ_2.GUI.Forms.NhapHang
{
    public partial class frmThemPhieuNhap : Form
    {
        private DataTable dtHangHoaSource;
        private bool isCalculating = false;

        public frmThemPhieuNhap()
        {
            InitializeComponent();
            CauHinhGridChiTiet();
        }

        private void CauHinhGridChiTiet()
        {
            // Bật đệm đôi chống giật lag
            typeof(DataGridView).InvokeMember("DoubleBuffered",
                System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.SetProperty,
                null, dgvChiTietHang, new object[] { true });

            dgvChiTietHang.AutoGenerateColumns = false;
            dgvChiTietHang.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(248, 250, 252);
            dgvChiTietHang.DefaultCellStyle.SelectionBackColor = Color.FromArgb(224, 231, 255);
            dgvChiTietHang.DefaultCellStyle.SelectionForeColor = Color.FromArgb(30, 41, 59);

            colStt.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            colMaHangNoiBo.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            colDonViTinh.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            colSoLuong.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            colDonGia.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            colGiamGia.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            colThanhTien.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            colXoa.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            colSoLuong.DefaultCellStyle.Format = "N0";
            colDonGia.DefaultCellStyle.Format = "N0";
            colGiamGia.DefaultCellStyle.Format = "N0";
            colThanhTien.DefaultCellStyle.Format = "N0";

            // Hỗ trợ phím Enter khi chọn hàng và nhập số lượng, đơn giá
            cboHangHoa.KeyDown += (s, ev) =>
            {
                if (ev.KeyCode == Keys.Enter)
                {
                    ev.SuppressKeyPress = true;
                    txtSoLuongNhap.Focus();
                }
            };
            txtSoLuongNhap.KeyDown += (s, ev) =>
            {
                if (ev.KeyCode == Keys.Enter)
                {
                    ev.SuppressKeyPress = true;
                    txtDonGiaNhap.Focus();
                }
            };
            txtDonGiaNhap.KeyDown += (s, ev) =>
            {
                if (ev.KeyCode == Keys.Enter)
                {
                    ev.SuppressKeyPress = true;
                    btnThemHang.PerformClick();
                }
            };
        }

        private void frmThemPhieuNhap_Load(object sender, EventArgs e)
        {
            NapDanhSachNhaCungCap();
            NapDanhSachKho();
            NapDanhSachNhanVien();
            NapDanhSachHangHoa();
            GoiYMaPhieuMoi();

            dtpNgayNhap.Value = DateTime.Now;
        }

        private void NapDanhSachNhaCungCap()
        {
            try
            {
                string sql = "SELECT MaDonViCapHang, TenDonVi FROM DonViCapHang ORDER BY TenDonVi ASC";
                DataTable dt = DataProcessing.ReadData(sql);
                cboNhaCungCap.DisplayMember = "TenDonVi";
                cboNhaCungCap.ValueMember = "MaDonViCapHang";
                cboNhaCungCap.DataSource = dt;

                if (dt.Rows.Count > 0)
                {
                    cboNhaCungCap.SelectedIndex = 0;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi nạp nhà cung cấp: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void NapDanhSachKho()
        {
            try
            {
                string sql = "SELECT MaKho, TenKho FROM Kho WHERE TrangThai = 'HOAT_DONG' OR TrangThai = 'ACTIVE' ORDER BY TenKho ASC";
                DataTable dt = DataProcessing.ReadData(sql);
                cboKho.DisplayMember = "TenKho";
                cboKho.ValueMember = "MaKho";
                cboKho.DataSource = dt;

                if (SessionContext.MaKhoHienTai > 0)
                {
                    cboKho.SelectedValue = SessionContext.MaKhoHienTai;
                }
                else if (dt.Rows.Count > 0)
                {
                    cboKho.SelectedIndex = 0;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi nạp danh sách kho: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void NapDanhSachNhanVien()
        {
            try
            {
                string sql = "SELECT MaNV, HoTen FROM NhanVien ORDER BY HoTen ASC";
                DataTable dt = DataProcessing.ReadData(sql);
                cboNguoiNhap.DisplayMember = "HoTen";
                cboNguoiNhap.ValueMember = "MaNV";
                cboNguoiNhap.DataSource = dt;

                if (SessionContext.MaTaiKhoan > 0)
                {
                    cboNguoiNhap.SelectedValue = SessionContext.MaTaiKhoan;
                }
                else if (dt.Rows.Count > 0)
                {
                    cboNguoiNhap.SelectedIndex = 0;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi nạp danh sách nhân viên: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void NapDanhSachHangHoa()
        {
            try
            {
                string sql = @"
                    SELECT 
                        h.MaHang, 
                        h.MaHangNoiBo, 
                        h.TenHang, 
                        ISNULL(d.TenDvt, N'Cái') AS TenDvt, 
                        ISNULL(h.GiaVon, 0) AS GiaNhap,
                        N'[' + h.MaHangNoiBo + N'] ' + h.TenHang + N' (' + ISNULL(d.TenDvt, N'Cái') + N') [Giá vốn: ' + CAST(CAST(ISNULL(h.GiaVon, 0) AS INT) AS NVARCHAR(15)) + N']' AS HienThi
                    FROM HangHoa h
                    LEFT JOIN DonViTinh d ON h.MaDvt = d.MaDvt
                    WHERE ISNULL(h.TrangThai, '') NOT IN ('NGUNG_BAN', 'NGUNG_KINH_DOANH', 'DA_XOA')
                    ORDER BY h.TenHang ASC";
                dtHangHoaSource = DataProcessing.ReadData(sql);

                ComboboxHelper.CaiDatTimKiem(cboHangHoa, dtHangHoaSource, "HienThi", "MaHang", () =>
                {
                    cboHangHoa_SelectedIndexChanged(null, null);
                });

                if (dtHangHoaSource.Rows.Count > 0)
                {
                    cboHangHoa_SelectedIndexChanged(null, null);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi nạp hàng hóa: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void GoiYMaPhieuMoi()
        {
            try
            {
                string sql = "SELECT ISNULL(MAX(MaPhieuNhap), 0) + 1 FROM PhieuNhap";
                object res = DataProcessing.ReadValue(sql);
                int nextId = Convert.ToInt32(res);
                txtMaPhieu.Text = "PN" + nextId.ToString("D6");
            }
            catch
            {
                txtMaPhieu.Text = "PN" + DateTime.Now.ToString("yyMMddHHmmss");
            }
        }

        private void cboHangHoa_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cboHangHoa.SelectedValue != null && int.TryParse(cboHangHoa.SelectedValue.ToString(), out int maHang))
            {
                if (dtHangHoaSource != null)
                {
                    DataRow[] rows = dtHangHoaSource.Select($"MaHang = {maHang}");
                    if (rows.Length > 0)
                    {
                        decimal giaNhap = Convert.ToDecimal(rows[0]["GiaNhap"]);
                        txtDonGiaNhap.Text = giaNhap.ToString("N0");
                    }
                }
            }
        }

        private void btnThemHang_Click(object sender, EventArgs e)
        {
            if (cboHangHoa.SelectedValue == null || !int.TryParse(cboHangHoa.SelectedValue.ToString(), out int maHang))
            {
                MessageBox.Show("Vui lòng chọn một mặt hàng!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (dtHangHoaSource == null) return;
            DataRow[] rows = dtHangHoaSource.Select($"MaHang = {maHang}");
            if (rows.Length == 0) return;

            DataRow rowItem = rows[0];
            string sku = rowItem["MaHangNoiBo"] != DBNull.Value ? rowItem["MaHangNoiBo"].ToString() : "";
            string ten = rowItem["TenHang"].ToString();
            string dvt = rowItem["TenDvt"].ToString();

            decimal soLuong = txtSoLuongNhap.Value;
            if (soLuong <= 0)
            {
                MessageBox.Show("Số lượng nhập phải lớn hơn 0!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string cleanPrice = txtDonGiaNhap.Text.Replace(",", "").Replace(".", "").Trim();
            if (!decimal.TryParse(cleanPrice, out decimal donGia) || donGia < 0)
            {
                MessageBox.Show("Đơn giá nhập không hợp lệ!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Kiểm tra xem hàng này đã có trong lưới chưa
            foreach (DataGridViewRow row in dgvChiTietHang.Rows)
            {
                if (Convert.ToInt32(row.Cells["colMaHang"].Value) == maHang)
                {
                    // Đã có -> cộng dồn số lượng
                    decimal currentQty = Convert.ToDecimal(row.Cells["colSoLuong"].Value);
                    row.Cells["colSoLuong"].Value = currentQty + soLuong;
                    row.Cells["colDonGia"].Value = donGia;
                    CapNhatThanhTienDong(row.Index);
                    TinhTongThanhToan();
                    return;
                }
            }

            // Chưa có -> thêm dòng mới
            int newIdx = dgvChiTietHang.Rows.Add();
            DataGridViewRow newRow = dgvChiTietHang.Rows[newIdx];
            newRow.Cells["colStt"].Value = newIdx + 1;
            newRow.Cells["colMaHang"].Value = maHang;
            newRow.Cells["colMaHangNoiBo"].Value = sku;
            newRow.Cells["colTenHang"].Value = ten;
            newRow.Cells["colDonViTinh"].Value = dvt;
            newRow.Cells["colSoLuong"].Value = soLuong;
            newRow.Cells["colDonGia"].Value = donGia;
            newRow.Cells["colGiamGia"].Value = 0m;
            newRow.Cells["colThanhTien"].Value = soLuong * donGia;

            DanhSoLaiStt();
            TinhTongThanhToan();
        }

        private void dgvChiTietHang_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.ColumnIndex == colXoa.Index)
            {
                dgvChiTietHang.Rows.RemoveAt(e.RowIndex);
                DanhSoLaiStt();
                TinhTongThanhToan();
            }
        }

        private void dgvChiTietHang_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (isCalculating) return;
            if (e.RowIndex >= 0)
            {
                string colName = dgvChiTietHang.Columns[e.ColumnIndex].Name;
                if (colName == "colSoLuong" || colName == "colDonGia" || colName == "colGiamGia")
                {
                    CapNhatThanhTienDong(e.RowIndex);
                    TinhTongThanhToan();
                }
            }
        }

        private void CapNhatThanhTienDong(int rowIndex)
        {
            if (rowIndex < 0 || rowIndex >= dgvChiTietHang.Rows.Count) return;

            DataGridViewRow row = dgvChiTietHang.Rows[rowIndex];
            decimal sl = 0;
            decimal dg = 0;
            decimal gg = 0;

            if (row.Cells["colSoLuong"].Value != null)
                decimal.TryParse(row.Cells["colSoLuong"].Value.ToString(), out sl);

            if (row.Cells["colDonGia"].Value != null)
                decimal.TryParse(row.Cells["colDonGia"].Value.ToString().Replace(",", "").Replace(".", ""), out dg);

            if (row.Cells["colGiamGia"].Value != null)
                decimal.TryParse(row.Cells["colGiamGia"].Value.ToString().Replace(",", "").Replace(".", ""), out gg);

            if (sl < 0) sl = 0;
            if (dg < 0) dg = 0;
            if (gg < 0) gg = 0;

            decimal thanhTien = (sl * dg) - gg;
            if (thanhTien < 0) thanhTien = 0;

            isCalculating = true;
            row.Cells["colSoLuong"].Value = sl;
            row.Cells["colDonGia"].Value = dg;
            row.Cells["colGiamGia"].Value = gg;
            row.Cells["colThanhTien"].Value = thanhTien;
            isCalculating = false;
        }

        private void DanhSoLaiStt()
        {
            for (int i = 0; i < dgvChiTietHang.Rows.Count; i++)
            {
                dgvChiTietHang.Rows[i].Cells["colStt"].Value = i + 1;
            }
        }

        private void TinhTongThanhToan()
        {
            if (isCalculating) return;

            decimal tongTienHang = 0;
            decimal tongSoLuong = 0;
            int soMatHang = dgvChiTietHang.Rows.Count;

            foreach (DataGridViewRow row in dgvChiTietHang.Rows)
            {
                if (row.Cells["colThanhTien"].Value != null)
                {
                    decimal.TryParse(row.Cells["colThanhTien"].Value.ToString(), out decimal tt);
                    tongTienHang += tt;
                }
                if (row.Cells["colSoLuong"].Value != null)
                {
                    decimal.TryParse(row.Cells["colSoLuong"].Value.ToString(), out decimal sl);
                    tongSoLuong += sl;
                }
            }

            lblSoMatHang.Text = $"Số mặt hàng: {soMatHang}";
            lblTongSoLuong.Text = $"Tổng số lượng: {tongSoLuong:N0}";
            txtTongTienHang.Text = tongTienHang.ToString("N0");

            decimal giamGiaTong = 0;
            string cleanGg = txtGiamGiaTong.Text.Replace(",", "").Replace(".", "").Trim();
            if (decimal.TryParse(cleanGg, out decimal ggVal))
            {
                giamGiaTong = ggVal;
            }

            decimal tongCong = tongTienHang - giamGiaTong;
            if (tongCong < 0) tongCong = 0;
            txtTongCong.Text = tongCong.ToString("N0");

            decimal tienDaTra = 0;
            string cleanDaTra = txtTienDaTra.Text.Replace(",", "").Replace(".", "").Trim();
            if (decimal.TryParse(cleanDaTra, out decimal dtVal))
            {
                tienDaTra = dtVal;
            }
            else
            {
                // Mặc định tự động điền tiền đã trả = tổng cộng nếu chưa nhập
                tienDaTra = tongCong;
                txtTienDaTra.Text = tienDaTra.ToString("N0");
            }

            decimal canTraNcc = tongCong - tienDaTra;
            if (canTraNcc < 0) canTraNcc = 0;
            txtCanTraNcc.Text = canTraNcc.ToString("N0");
        }

        private void txtGiamGiaTong_TextChanged(object sender, EventArgs e)
        {
            TinhTongThanhToan();
        }

        private void txtTienDaTra_TextChanged(object sender, EventArgs e)
        {
            TinhTongThanhToan();
        }

        private void btnLuuTam_Click(object sender, EventArgs e)
        {
            LuuPhieuNhap("NHAP");
        }

        private void btnHoanThanh_Click(object sender, EventArgs e)
        {
            LuuPhieuNhap("DA_XAC_NHAN");
        }

        private void LuuPhieuNhap(string trangThai)
        {
            // 1. Kiểm tra tính hợp lệ
            if (cboNhaCungCap.SelectedValue == null)
            {
                MessageBox.Show("Vui lòng chọn nhà cung cấp!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                cboNhaCungCap.Focus();
                return;
            }

            if (cboKho.SelectedValue == null)
            {
                MessageBox.Show("Vui lòng chọn kho nhập!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                cboKho.Focus();
                return;
            }

            if (cboNguoiNhap.SelectedValue == null)
            {
                MessageBox.Show("Vui lòng chọn người nhập!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                cboNguoiNhap.Focus();
                return;
            }

            if (dgvChiTietHang.Rows.Count == 0)
            {
                MessageBox.Show("Phiếu nhập chưa có mặt hàng nào! Vui lòng thêm ít nhất một mặt hàng.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string maPhieu = txtMaPhieu.Text.Trim();
            if (string.IsNullOrEmpty(maPhieu))
            {
                MessageBox.Show("Mã phiếu không được để trống!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtMaPhieu.Focus();
                return;
            }

            // Kiểm tra chống trùng mã phiếu
            string checkSql = "SELECT COUNT(1) FROM PhieuNhap WHERE MaPhieu = @MaPhieu";
            object exist = DataProcessing.ReadValue(checkSql, new SqlParameter[] { new SqlParameter("@MaPhieu", maPhieu) });
            if (Convert.ToInt32(exist) > 0)
            {
                MessageBox.Show($"Mã phiếu '{maPhieu}' đã tồn tại trong hệ thống! Vui lòng chọn mã khác.", "Trùng mã", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtMaPhieu.Focus();
                return;
            }

            int maNcc = Convert.ToInt32(cboNhaCungCap.SelectedValue);
            int maKho = Convert.ToInt32(cboKho.SelectedValue);
            int maNvNhap = Convert.ToInt32(cboNguoiNhap.SelectedValue);
            int maNvLap = SessionContext.MaTaiKhoan > 0 ? SessionContext.MaTaiKhoan : maNvNhap;
            DateTime ngayNhap = dtpNgayNhap.Value;
            string soHoaDon = txtSoHoaDon.Text.Trim();
            string ghiChu = txtGhiChu.Text.Trim();

            decimal giamGiaTong = 0;
            decimal.TryParse(txtGiamGiaTong.Text.Replace(",", "").Replace(".", ""), out giamGiaTong);

            decimal tongCong = 0;
            decimal.TryParse(txtTongCong.Text.Replace(",", "").Replace(".", ""), out tongCong);

            decimal canTraNcc = 0;
            decimal.TryParse(txtCanTraNcc.Text.Replace(",", "").Replace(".", ""), out canTraNcc);

            string xacNhanMsg = trangThai == "DA_XAC_NHAN"
                ? "Bạn có chắc chắn muốn HOÀN THÀNH phiếu nhập này? Tồn kho sẽ được cộng ngay lập tức và công nợ sẽ được ghi nhận."
                : "Bạn có chắc chắn muốn LƯU TẠM phiếu nhập này?";

            if (MessageBox.Show(xacNhanMsg, "Xác nhận lưu", MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
            {
                return;
            }

            // 2. Thực thi giao dịch lưu CSDL
            try
            {
                DataProcessing.ChangeDataTransaction((conn, trans) =>
                {
                    // 2.1. Thêm PhieuNhap
                    string insertPnSql = @"
                        INSERT INTO PhieuNhap (
                            MaKho, MaDonViCapHang, MaNVLap, MaNVXacNhan, NgayLap, NgayXacNhan,
                            LoaiPhieuNhap, TrangThai, GhiChu, MaPhieu, DaDanhDau, SoHoaDon,
                            GiamGia, TongTien, CanTraNcc
                        )
                        VALUES (
                            @MaKho, @MaDonViCapHang, @MaNVLap, @MaNVXacNhan, @NgayLap, @NgayXacNhan,
                            'THONG_THUONG', @TrangThai, @GhiChu, @MaPhieu, 0, @SoHoaDon,
                            @GiamGia, @TongTien, @CanTraNcc
                        );
                        SELECT SCOPE_IDENTITY();";

                    SqlCommand cmdPn = new SqlCommand(insertPnSql, conn, trans);
                    cmdPn.Parameters.AddWithValue("@MaKho", maKho);
                    cmdPn.Parameters.AddWithValue("@MaDonViCapHang", maNcc);
                    cmdPn.Parameters.AddWithValue("@MaNVLap", maNvLap);
                    cmdPn.Parameters.AddWithValue("@MaNVXacNhan", trangThai == "DA_XAC_NHAN" ? (object)maNvNhap : DBNull.Value);
                    cmdPn.Parameters.AddWithValue("@NgayLap", ngayNhap);
                    cmdPn.Parameters.AddWithValue("@NgayXacNhan", trangThai == "DA_XAC_NHAN" ? (object)DateTime.Now : DBNull.Value);
                    cmdPn.Parameters.AddWithValue("@TrangThai", trangThai);
                    cmdPn.Parameters.AddWithValue("@GhiChu", string.IsNullOrEmpty(ghiChu) ? (object)DBNull.Value : ghiChu);
                    cmdPn.Parameters.AddWithValue("@MaPhieu", maPhieu);
                    cmdPn.Parameters.AddWithValue("@SoHoaDon", string.IsNullOrEmpty(soHoaDon) ? (object)DBNull.Value : soHoaDon);
                    cmdPn.Parameters.AddWithValue("@GiamGia", giamGiaTong);
                    cmdPn.Parameters.AddWithValue("@TongTien", tongCong);
                    cmdPn.Parameters.AddWithValue("@CanTraNcc", canTraNcc);

                    object newIdObj = cmdPn.ExecuteScalar();
                    int maPhieuNhap = Convert.ToInt32(newIdObj);

                    // 2.2. Thêm ChiTietPhieuNhap
                    string insertCtSql = @"
                        INSERT INTO ChiTietPhieuNhap (MaPhieuNhap, MaHang, SoLuong, DonGiaNhap, GiamGia, ThanhTien)
                        VALUES (@MaPhieuNhap, @MaHang, @SoLuong, @DonGiaNhap, @GiamGia, @ThanhTien);";

                    foreach (DataGridViewRow row in dgvChiTietHang.Rows)
                    {
                        int maHangItem = Convert.ToInt32(row.Cells["colMaHang"].Value);
                        decimal soLuong = Convert.ToDecimal(row.Cells["colSoLuong"].Value);
                        decimal donGia = Convert.ToDecimal(row.Cells["colDonGia"].Value);
                        decimal giamGia = Convert.ToDecimal(row.Cells["colGiamGia"].Value);
                        decimal thanhTien = Convert.ToDecimal(row.Cells["colThanhTien"].Value);

                        SqlCommand cmdCt = new SqlCommand(insertCtSql, conn, trans);
                        cmdCt.Parameters.AddWithValue("@MaPhieuNhap", maPhieuNhap);
                        cmdCt.Parameters.AddWithValue("@MaHang", maHangItem);
                        cmdCt.Parameters.AddWithValue("@SoLuong", soLuong);
                        cmdCt.Parameters.AddWithValue("@DonGiaNhap", donGia);
                        cmdCt.Parameters.AddWithValue("@GiamGia", giamGia);
                        cmdCt.Parameters.AddWithValue("@ThanhTien", thanhTien);
                        cmdCt.ExecuteNonQuery();
                    }

                    // 2.3. Ghi nhật ký thao tác
                    string logSql = @"
                        INSERT INTO NhatKyThaoTac (MaTaiKhoan, ThoiDiem, ThaoTac, LoaiDoiTuong, MaDoiTuong, NoiDung)
                        VALUES (@MaTaiKhoan, GETDATE(), @ThaoTac, 'PhieuNhap', @MaDoiTuong, @NoiDung)";
                    SqlCommand cmdLog = new SqlCommand(logSql, conn, trans);
                    cmdLog.Parameters.AddWithValue("@MaTaiKhoan", SessionContext.MaTaiKhoan > 0 ? SessionContext.MaTaiKhoan : 1);
                    cmdLog.Parameters.AddWithValue("@ThaoTac", trangThai == "DA_XAC_NHAN" ? "HOAN_THANH_NHAP_HANG" : "LUU_TAM_NHAP_HANG");
                    cmdLog.Parameters.AddWithValue("@MaDoiTuong", maPhieu);
                    cmdLog.Parameters.AddWithValue("@NoiDung", $"Lập phiếu nhập {maPhieu} (ID: {maPhieuNhap}), tổng tiền: {tongCong:N0}, trạng thái: {trangThai}");
                    cmdLog.ExecuteNonQuery();
                });

                string thongBao = trangThai == "DA_XAC_NHAN"
                    ? $"Hoàn thành phiếu nhập hàng '{maPhieu}' thành công!"
                    : $"Lưu tạm phiếu nhập hàng '{maPhieu}' thành công!";

                MessageBox.Show(thongBao, "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi lưu phiếu nhập: " + ex.Message, "Thông báo lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnBoQua_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
