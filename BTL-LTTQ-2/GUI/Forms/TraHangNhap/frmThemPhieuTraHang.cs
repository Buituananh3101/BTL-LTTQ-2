using BTL_LTTQ_2.Classes;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Windows.Forms;

namespace BTL_LTTQ_2.GUI.Forms.TraHangNhap
{
    public partial class frmThemPhieuTraHang : Form
    {
        private DataTable dtHangTra;
        private DataTable dtDanhSachHang;
        private bool isCalculating = false;

        public frmThemPhieuTraHang()
        {
            InitializeComponent();
            KhoiTaoBangHangTra();
            CauHinhDataGridView();
        }

        private void KhoiTaoBangHangTra()
        {
            dtHangTra = new DataTable();
            dtHangTra.Columns.Add("STT", typeof(int));
            dtHangTra.Columns.Add("MaHang", typeof(int));
            dtHangTra.Columns.Add("MaHangNoiBo", typeof(string));
            dtHangTra.Columns.Add("TenHang", typeof(string));
            dtHangTra.Columns.Add("TenDVT", typeof(string));
            dtHangTra.Columns.Add("TonKho", typeof(decimal));
            dtHangTra.Columns.Add("SoLuongTra", typeof(decimal));
            dtHangTra.Columns.Add("DonGiaTra", typeof(decimal));
            dtHangTra.Columns.Add("ThanhTien", typeof(decimal));
        }

        private void CauHinhDataGridView()
        {
            // Bật DoubleBuffered chống giật lag lưới
            typeof(DataGridView).InvokeMember("DoubleBuffered",
                System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.SetProperty,
                null, dgvHangTra, new object[] { true });

            dgvHangTra.AutoGenerateColumns = false;
            dgvHangTra.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(248, 250, 252);
            dgvHangTra.DefaultCellStyle.SelectionBackColor = Color.FromArgb(224, 231, 255);
            dgvHangTra.DefaultCellStyle.SelectionForeColor = Color.FromArgb(30, 41, 59);

            colStt.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            colMaHangNoiBo.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            colDonViTinh.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            colTonKho.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            colSoLuongTra.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            colDonGiaTra.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            colThanhTien.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;

            colTonKho.DefaultCellStyle.Format = "N0";
            colSoLuongTra.DefaultCellStyle.Format = "N0";
            colDonGiaTra.DefaultCellStyle.Format = "N0";
            colThanhTien.DefaultCellStyle.Format = "N0";

            colSoLuongTra.DefaultCellStyle.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);

            dgvHangTra.DataSource = dtHangTra;
        }

        private void frmThemPhieuTraHang_Load(object sender, EventArgs e)
        {
            lblMaPhieuVal.Text = SinhMaPhieuTuDong();
            dtpNgayTra.Value = DateTime.Now;

            NapDanhSachKho();
            NapDanhSachNhaCungCap();
            NapDanhSachNhanVien();
            NapDanhSachHangHoa();

            TinhToanTongKet();
        }

        private string SinhMaPhieuTuDong()
        {
            try
            {
                string sql = "SELECT ISNULL(MAX(MaPhieuTraHang), 0) + 1 FROM PhieuTraHangNhap";
                object val = DataProcessing.ReadValue(sql);
                int nextId = (val != null && val != DBNull.Value) ? Convert.ToInt32(val) : 1;
                return "THN" + nextId.ToString("D6");
            }
            catch
            {
                return "THN" + DateTime.Now.ToString("yyMMddHHmm");
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
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi nạp danh sách kho: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void NapDanhSachNhaCungCap()
        {
            try
            {
                string sql = "SELECT MaDonViCapHang, TenDonVi, MaNcc FROM DonViCapHang WHERE TrangThai = 'HOAT_DONG' OR TrangThai = 'ACTIVE' OR TrangThai IS NULL ORDER BY TenDonVi ASC";
                DataTable dt = DataProcessing.ReadData(sql);
                cboNhaCungCap.DisplayMember = "TenDonVi";
                cboNhaCungCap.ValueMember = "MaDonViCapHang";
                cboNhaCungCap.DataSource = dt;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi nạp danh sách nhà cung cấp: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void NapDanhSachNhanVien()
        {
            try
            {
                string sql = @"
                    SELECT tk.MaTaiKhoan, ISNULL(nv.HoTen, tk.TenDangNhap) AS HoTen 
                    FROM TaiKhoan tk 
                    LEFT JOIN NhanVien nv ON tk.MaNV = nv.MaNV 
                    WHERE tk.TrangThai = 'HOAT_DONG' OR tk.TrangThai = 'ACTIVE' OR tk.TrangThai IS NULL
                    ORDER BY HoTen ASC";
                DataTable dt = DataProcessing.ReadData(sql);
                cboNguoiTra.DisplayMember = "HoTen";
                cboNguoiTra.ValueMember = "MaTaiKhoan";
                cboNguoiTra.DataSource = dt;

                if (SessionContext.MaTaiKhoan > 0)
                {
                    cboNguoiTra.SelectedValue = SessionContext.MaTaiKhoan;
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
                int maKho = 1;
                if (cboKho.SelectedValue != null && int.TryParse(cboKho.SelectedValue.ToString(), out int k))
                {
                    maKho = k;
                }

                string sql = @"
                    SELECT 
                        h.MaHang,
                        h.MaHangNoiBo,
                        h.TenHang,
                        '[' + h.MaHangNoiBo + '] ' + h.TenHang AS HienThi,
                        ISNULL(dvt.TenDVT, N'Cái') AS TenDVT,
                        ISNULL(h.GiaVon, 0) AS GiaNhap,
                        ISNULL(tk.SoLuongTon, 0) AS TonKho
                    FROM HangHoa h
                    LEFT JOIN DonViTinh dvt ON h.MaDVT = dvt.MaDVT
                    LEFT JOIN vw_TonKhoHienTai tk ON h.MaHang = tk.MaHang AND tk.MaKho = @MaKho
                    WHERE ISNULL(h.TrangThai, '') NOT IN ('NGUNG_BAN', 'NGUNG_KINH_DOANH', 'DA_XOA')
                    ORDER BY h.TenHang";

                SqlParameter[] p = { new SqlParameter("@MaKho", maKho) };
                dtDanhSachHang = DataProcessing.ReadData(sql, p);

                cboHangHoa.DisplayMember = "HienThi";
                cboHangHoa.ValueMember = "MaHang";
                cboHangHoa.DataSource = dtDanhSachHang;

                cboHangHoa_SelectedIndexChanged(null, null);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi nạp danh sách hàng hóa: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void cboKho_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cboKho.SelectedValue != null && int.TryParse(cboKho.SelectedValue.ToString(), out _))
            {
                NapDanhSachHangHoa();

                // Cập nhật lại tồn kho trong dtHangTra nếu đã có sản phẩm
                CapNhatTonKhoTheoKhoMoi();
            }
        }

        private void CapNhatTonKhoTheoKhoMoi()
        {
            if (dtHangTra == null || dtHangTra.Rows.Count == 0 || dtDanhSachHang == null) return;

            foreach (DataRow rTra in dtHangTra.Rows)
            {
                int maH = Convert.ToInt32(rTra["MaHang"]);
                DataRow[] found = dtDanhSachHang.Select($"MaHang = {maH}");
                if (found.Length > 0)
                {
                    rTra["TonKho"] = found[0]["TonKho"];
                }
                else
                {
                    rTra["TonKho"] = 0;
                }
            }
            dgvHangTra.Refresh();
        }

        private void cboHangHoa_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cboHangHoa.SelectedValue == null || dtDanhSachHang == null) return;

            if (int.TryParse(cboHangHoa.SelectedValue.ToString(), out int maHang))
            {
                DataRow[] found = dtDanhSachHang.Select($"MaHang = {maHang}");
                if (found.Length > 0)
                {
                    DataRow r = found[0];
                    lblDonViTinhVal.Text = r["TenDVT"].ToString();
                    decimal tonKho = Convert.ToDecimal(r["TonKho"]);
                    lblTonKhoVal.Text = tonKho.ToString("N0");

                    decimal giaNhap = Convert.ToDecimal(r["GiaNhap"]);
                    txtDonGiaTra.Text = giaNhap.ToString("N0");
                    txtSoLuongTra.Text = "1";
                    txtSoLuongTra.Focus();
                    txtSoLuongTra.SelectAll();
                }
            }
        }

        private void btnThemHang_Click(object sender, EventArgs e)
        {
            ThemHangVaoBang();
        }

        private void txtSoLuongTra_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;
                ThemHangVaoBang();
            }
        }

        private void txtDonGiaTra_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;
                ThemHangVaoBang();
            }
        }

        private void ThemHangVaoBang()
        {
            if (cboHangHoa.SelectedValue == null || dtDanhSachHang == null)
            {
                MessageBox.Show("Vui lòng chọn hàng hóa cần trả!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (!int.TryParse(cboHangHoa.SelectedValue.ToString(), out int maHang)) return;

            DataRow[] found = dtDanhSachHang.Select($"MaHang = {maHang}");
            if (found.Length == 0) return;

            DataRow rHang = found[0];
            string maHangNoiBo = rHang["MaHangNoiBo"].ToString();
            string tenHang = rHang["TenHang"].ToString();
            string tenDvt = rHang["TenDVT"].ToString();
            decimal tonKho = Convert.ToDecimal(rHang["TonKho"]);

            if (!decimal.TryParse(txtSoLuongTra.Text.Replace(",", "").Trim(), out decimal slTra) || slTra <= 0)
            {
                MessageBox.Show("Số lượng trả phải là số dương lớn hơn 0!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtSoLuongTra.Focus();
                return;
            }

            if (!decimal.TryParse(txtDonGiaTra.Text.Replace(",", "").Trim(), out decimal donGiaTra) || donGiaTra < 0)
            {
                MessageBox.Show("Đơn giá trả không hợp lệ!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtDonGiaTra.Focus();
                return;
            }

            // Kiểm tra tồn kho
            if (slTra > tonKho)
            {
                MessageBox.Show($"Số lượng trả ({slTra:N0}) không được vượt quá số lượng tồn kho hiện tại ({tonKho:N0})!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtSoLuongTra.Focus();
                return;
            }

            // Kiểm tra xem sản phẩm đã có trong bảng chưa
            DataRow[] existRows = dtHangTra.Select($"MaHang = {maHang}");
            if (existRows.Length > 0)
            {
                DataRow rExist = existRows[0];
                decimal slCu = Convert.ToDecimal(rExist["SoLuongTra"]);
                decimal tongSlMoi = slCu + slTra;

                if (tongSlMoi > tonKho)
                {
                    MessageBox.Show($"Tổng số lượng trả sau khi thêm ({tongSlMoi:N0}) vượt quá tồn kho hiện tại ({tonKho:N0})!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                rExist["SoLuongTra"] = tongSlMoi;
                rExist["DonGiaTra"] = donGiaTra;
                rExist["ThanhTien"] = tongSlMoi * donGiaTra;
            }
            else
            {
                DataRow rNew = dtHangTra.NewRow();
                rNew["STT"] = dtHangTra.Rows.Count + 1;
                rNew["MaHang"] = maHang;
                rNew["MaHangNoiBo"] = maHangNoiBo;
                rNew["TenHang"] = tenHang;
                rNew["TenDVT"] = tenDvt;
                rNew["TonKho"] = tonKho;
                rNew["SoLuongTra"] = slTra;
                rNew["DonGiaTra"] = donGiaTra;
                rNew["ThanhTien"] = slTra * donGiaTra;
                dtHangTra.Rows.Add(rNew);
            }

            CapNhatLaiStt();
            TinhToanTongKet();

            // Reset ô nhập
            txtSoLuongTra.Text = "1";
            cboHangHoa.Focus();
        }

        private void CapNhatLaiStt()
        {
            for (int i = 0; i < dtHangTra.Rows.Count; i++)
            {
                dtHangTra.Rows[i]["STT"] = i + 1;
            }
        }

        private void dgvHangTra_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;

            // Xử lý nút xóa dòng
            if (dgvHangTra.Columns[e.ColumnIndex].Name == "colXoa")
            {
                string tenHang = dgvHangTra.Rows[e.RowIndex].Cells["colTenHang"].Value.ToString();
                DialogResult dr = MessageBox.Show($"Bạn có muốn xóa mặt hàng [{tenHang}] khỏi danh sách trả không?", "Xác nhận xóa dòng", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (dr == DialogResult.Yes)
                {
                    dtHangTra.Rows.RemoveAt(e.RowIndex);
                    CapNhatLaiStt();
                    TinhToanTongKet();
                }
            }
        }

        private void dgvHangTra_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0 || isCalculating) return;

            string colName = dgvHangTra.Columns[e.ColumnIndex].Name;
            if (colName == "colSoLuongTra" || colName == "colDonGiaTra")
            {
                isCalculating = true;
                try
                {
                    DataRow row = dtHangTra.Rows[e.RowIndex];
                    decimal tonKho = Convert.ToDecimal(row["TonKho"]);
                    decimal slTra = Convert.ToDecimal(row["SoLuongTra"]);
                    decimal donGiaTra = Convert.ToDecimal(row["DonGiaTra"]);

                    if (slTra < 0)
                    {
                        MessageBox.Show("Số lượng trả không được âm!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        row["SoLuongTra"] = 0;
                        slTra = 0;
                    }
                    else if (slTra > tonKho)
                    {
                        MessageBox.Show($"Số lượng trả ({slTra:N0}) không được vượt quá tồn kho hiện tại ({tonKho:N0})!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        row["SoLuongTra"] = tonKho;
                        slTra = tonKho;
                    }

                    if (donGiaTra < 0)
                    {
                        MessageBox.Show("Đơn giá trả không được âm!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        row["DonGiaTra"] = 0;
                        donGiaTra = 0;
                    }

                    row["ThanhTien"] = slTra * donGiaTra;
                }
                catch
                {
                }
                finally
                {
                    isCalculating = false;
                }

                TinhToanTongKet();
            }
        }

        private void TinhToanTongKet()
        {
            if (isCalculating) return;
            isCalculating = true;

            try
            {
                decimal tongSl = 0;
                decimal tongTienHang = 0;

                if (dtHangTra != null)
                {
                    foreach (DataRow r in dtHangTra.Rows)
                    {
                        tongSl += Convert.ToDecimal(r["SoLuongTra"]);
                        tongTienHang += Convert.ToDecimal(r["ThanhTien"]);
                    }
                }

                decimal giamGia = 0;
                if (!string.IsNullOrEmpty(txtGiamGia.Text))
                {
                    decimal.TryParse(txtGiamGia.Text.Replace(",", "").Trim(), out giamGia);
                }

                decimal canThuNcc = Math.Max(0, tongTienHang - giamGia);

                lblTongSlTraVal.Text = tongSl.ToString("N0");
                lblTongTienTraVal.Text = tongTienHang.ToString("N0") + " đ";
                lblCanThuNccVal.Text = canThuNcc.ToString("N0") + " đ";

                if (string.IsNullOrEmpty(txtDaThuNcc.Text) || txtDaThuNcc.Text == "0")
                {
                    txtDaThuNcc.Text = "0";
                }
            }
            finally
            {
                isCalculating = false;
            }
        }

        private void txtGiamGia_TextChanged(object sender, EventArgs e)
        {
            TinhToanTongKet();
        }

        private void btnLuuTam_Click(object sender, EventArgs e)
        {
            LuuPhieuTraHang("NHAP");
        }

        private void btnXacNhanTra_Click(object sender, EventArgs e)
        {
            LuuPhieuTraHang("DA_TRA_HANG");
        }

        private void LuuPhieuTraHang(string trangThai)
        {
            // Kiểm tra ràng buộc
            if (cboNhaCungCap.SelectedValue == null)
            {
                MessageBox.Show("Vui lòng chọn Nhà cung cấp nhận trả hàng!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                cboNhaCungCap.Focus();
                return;
            }

            if (cboKho.SelectedValue == null)
            {
                MessageBox.Show("Vui lòng chọn Kho xuất trả hàng!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                cboKho.Focus();
                return;
            }

            if (dtHangTra == null || dtHangTra.Rows.Count == 0)
            {
                MessageBox.Show("Danh sách hàng trả không được để trống!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            decimal tongSlTra = 0;
            decimal tongTienHang = 0;
            foreach (DataRow r in dtHangTra.Rows)
            {
                decimal sl = Convert.ToDecimal(r["SoLuongTra"]);
                decimal ton = Convert.ToDecimal(r["TonKho"]);

                if (sl <= 0)
                {
                    MessageBox.Show($"Số lượng trả của mặt hàng [{r["TenHang"]}] phải lớn hơn 0!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (trangThai == "DA_TRA_HANG" && sl > ton)
                {
                    MessageBox.Show($"Mặt hàng [{r["TenHang"]}] có số lượng trả ({sl:N0}) vượt quá tồn kho thực tế ({ton:N0})!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                tongSlTra += sl;
                tongTienHang += Convert.ToDecimal(r["ThanhTien"]);
            }

            if (tongSlTra <= 0)
            {
                MessageBox.Show("Tổng số lượng hàng trả phải lớn hơn 0!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            decimal giamGia = 0;
            decimal.TryParse(txtGiamGia.Text.Replace(",", "").Trim(), out giamGia);

            decimal canThuNcc = Math.Max(0, tongTienHang - giamGia);

            decimal daThuNcc = 0;
            decimal.TryParse(txtDaThuNcc.Text.Replace(",", "").Trim(), out daThuNcc);

            int maKho = Convert.ToInt32(cboKho.SelectedValue);
            int maNcc = Convert.ToInt32(cboNhaCungCap.SelectedValue);
            int maNvTra = cboNguoiTra.SelectedValue != null ? Convert.ToInt32(cboNguoiTra.SelectedValue) : 1;
            int maNvLap = SessionContext.MaTaiKhoan > 0 ? SessionContext.MaTaiKhoan : maNvTra;
            DateTime ngayTra = dtpNgayTra.Value;
            string ghiChu = txtGhiChu.Text.Trim();
            string maPhieu = SinhMaPhieuTuDong();

            string msgXacNhan = trangThai == "NHAP"
                ? "Bạn có muốn lưu tạm phiếu trả hàng nhập này không?"
                : "Bạn có chắc chắn muốn hoàn tất trả hàng nhập không?\nSố lượng hàng hóa sẽ được ghi nhận xuất giảm tồn kho ngay sau khi xác nhận.";

            DialogResult dr = MessageBox.Show(msgXacNhan, "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dr != DialogResult.Yes) return;

            try
            {
                DataProcessing.ChangeDataTransaction((conn, trans) =>
                {
                    // 1. Chèn Header PhieuTraHangNhap
                    string sqlHeader = @"
                        INSERT INTO PhieuTraHangNhap (
                            MaPhieu, MaKho, MaDonViCapHang, MaNvlap, MaNvtra,
                            NgayLap, NgayTra, TrangThai, TongSoLuong, TongTienHang,
                            GiamGia, CanThuNcc, DaThuNcc, GhiChu, DaDanhDau
                        ) VALUES (
                            @MaPhieu, @MaKho, @MaDonViCapHang, @MaNvlap, @MaNvtra,
                            GETDATE(), @NgayTra, @TrangThai, @TongSoLuong, @TongTienHang,
                            @GiamGia, @CanThuNcc, @DaThuNcc, @GhiChu, 0
                        );
                        SELECT SCOPE_IDENTITY();";

                    int maPhieuTraHangMoi = 0;
                    using (SqlCommand cmdHeader = new SqlCommand(sqlHeader, conn, trans))
                    {
                        cmdHeader.Parameters.AddWithValue("@MaPhieu", maPhieu);
                        cmdHeader.Parameters.AddWithValue("@MaKho", maKho);
                        cmdHeader.Parameters.AddWithValue("@MaDonViCapHang", maNcc);
                        cmdHeader.Parameters.AddWithValue("@MaNvlap", maNvLap);
                        cmdHeader.Parameters.AddWithValue("@MaNvtra", maNvTra);
                        cmdHeader.Parameters.AddWithValue("@NgayTra", ngayTra);
                        cmdHeader.Parameters.AddWithValue("@TrangThai", trangThai);
                        cmdHeader.Parameters.AddWithValue("@TongSoLuong", tongSlTra);
                        cmdHeader.Parameters.AddWithValue("@TongTienHang", tongTienHang);
                        cmdHeader.Parameters.AddWithValue("@GiamGia", giamGia);
                        cmdHeader.Parameters.AddWithValue("@CanThuNcc", canThuNcc);
                        cmdHeader.Parameters.AddWithValue("@DaThuNcc", daThuNcc);
                        cmdHeader.Parameters.AddWithValue("@GhiChu", string.IsNullOrEmpty(ghiChu) ? (object)DBNull.Value : ghiChu);

                        object idObj = cmdHeader.ExecuteScalar();
                        maPhieuTraHangMoi = Convert.ToInt32(idObj);
                    }

                    // 2. Chèn Chi tiết ChiTietPhieuTraHangNhap
                    string sqlCt = @"
                        INSERT INTO ChiTietPhieuTraHangNhap (
                            MaPhieuTraHang, MaHang, SoLuong, GiaVon, DonGiaTra, ThanhTien, GhiChu
                        ) VALUES (
                            @MaPhieuTraHang, @MaHang, @SoLuong, @GiaVon, @DonGiaTra, @ThanhTien, @GhiChu
                        )";

                    foreach (DataRow r in dtHangTra.Rows)
                    {
                        using (SqlCommand cmdCt = new SqlCommand(sqlCt, conn, trans))
                        {
                            cmdCt.Parameters.AddWithValue("@MaPhieuTraHang", maPhieuTraHangMoi);
                            cmdCt.Parameters.AddWithValue("@MaHang", Convert.ToInt32(r["MaHang"]));
                            cmdCt.Parameters.AddWithValue("@SoLuong", Convert.ToDecimal(r["SoLuongTra"]));
                            cmdCt.Parameters.AddWithValue("@GiaVon", Convert.ToDecimal(r["DonGiaTra"]));
                            cmdCt.Parameters.AddWithValue("@DonGiaTra", Convert.ToDecimal(r["DonGiaTra"]));
                            cmdCt.Parameters.AddWithValue("@ThanhTien", Convert.ToDecimal(r["ThanhTien"]));
                            cmdCt.Parameters.AddWithValue("@GhiChu", DBNull.Value);

                            cmdCt.ExecuteNonQuery();
                        }
                    }

                    // 3. Ghi nhật ký thao tác
                    try
                    {
                        string sqlLog = @"
                            INSERT INTO NhatKyThaoTac (MaTaiKhoan, ThoiDiem, ThaoTac, LoaiDoiTuong, MaDoiTuong, NoiDung)
                            VALUES (@MaTaiKhoan, GETDATE(), @ThaoTac, 'PhieuTraHangNhap', @MaDoiTuong, @NoiDung)";
                        using (SqlCommand cmdLog = new SqlCommand(sqlLog, conn, trans))
                        {
                            cmdLog.Parameters.AddWithValue("@MaTaiKhoan", maNvLap);
                            cmdLog.Parameters.AddWithValue("@ThaoTac", trangThai == "NHAP" ? "THEM_TAM" : "HOAN_THANH");
                            cmdLog.Parameters.AddWithValue("@MaDoiTuong", maPhieu);
                            cmdLog.Parameters.AddWithValue("@NoiDung", $"Lập phiếu trả hàng {maPhieu} ({trangThai})");
                            cmdLog.ExecuteNonQuery();
                        }
                    }
                    catch
                    {
                        // Bỏ qua nếu bảng log chưa tồn tại
                    }
                });

                string msgDone = trangThai == "NHAP"
                    ? $"Đã lưu tạm phiếu trả hàng [{maPhieu}] thành công!"
                    : $"Đã hoàn tất trả hàng nhập [{maPhieu}] thành công!\nTồn kho đã được tự động cập nhật giảm.";
                MessageBox.Show(msgDone, "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi lưu phiếu trả hàng: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnBoQua_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
