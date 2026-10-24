using BTL_LTTQ_2.Classes;

using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Windows.Forms;

namespace BTL_LTTQ_2.GUI.Forms.NhapHang
{
    public partial class frmTraHangNhap : Form
    {
        private int maPhieuNhap;
        private string maPhieuGoc;
        private int maKho;
        private int maDonViCapHang;
        private decimal canTraNccGoc;
        private DataTable dtHangTra;
        private bool isCalculating = false;

        public frmTraHangNhap(int maPhieuNhap)
        {
            InitializeComponent();
            this.maPhieuNhap = maPhieuNhap;
            CauHinhDataGridView();
        }

        private void CauHinhDataGridView()
        {
            // Bật đệm đôi chống giật lag
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
            colSoLuongNhap.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            colTonKho.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            colSoLuongTra.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            colDonGiaTra.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            colThanhTien.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;

            colSoLuongNhap.DefaultCellStyle.Format = "N0";
            colTonKho.DefaultCellStyle.Format = "N0";
            colSoLuongTra.DefaultCellStyle.Format = "N0";
            colDonGiaTra.DefaultCellStyle.Format = "N0";
            colThanhTien.DefaultCellStyle.Format = "N0";

            // Đánh dấu ô số lượng trả rõ ràng bằng font chữ đậm
            colSoLuongTra.DefaultCellStyle.Font = new Font("Segoe UI", 9.75F, FontStyle.Bold);
        }

        private void frmTraHangNhap_Load(object sender, EventArgs e)
        {
            NapDanhSachNhanVien();
            dtpNgayTra.Value = DateTime.Now;
            NapThongTinPhieuGocVaHangHoa();
        }

        private void NapDanhSachNhanVien()
        {
            cboNguoiTra.Items.Clear();
            try
            {
                string sql = @"
                    SELECT tk.MaTaiKhoan, ISNULL(nv.HoTen, tk.TenDangNhap) AS HoTen 
                    FROM TaiKhoan tk 
                    LEFT JOIN NhanVien nv ON tk.MaNV = nv.MaNV 
                    WHERE tk.TrangThai = 'HOAT_DONG' OR tk.TrangThai = 'ACTIVE' OR tk.TrangThai IS NULL
                    ORDER BY HoTen ASC";
                DataTable dt = DataProcessing.ReadData(sql);
                foreach (DataRow row in dt.Rows)
                {
                    int id = Convert.ToInt32(row["MaTaiKhoan"]);
                    string ten = row["HoTen"].ToString();
                    cboNguoiTra.Items.Add(new ComboBoxItem(ten, id));
                }
                if (cboNguoiTra.Items.Count > 0)
                {
                    cboNguoiTra.SelectedIndex = 0;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi nạp danh sách nhân viên: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void NapThongTinPhieuGocVaHangHoa()
        {
            try
            {
                // 1. Lấy thông tin phiếu nhập gốc
                string sqlHeader = @"
                    SELECT 
                        pn.MaPhieuNhap,
                        ISNULL(pn.MaPhieu, 'PN' + RIGHT('000000' + CAST(pn.MaPhieuNhap AS VARCHAR(10)), 6)) AS MaPhieu,
                        pn.MaKho,
                        ISNULL(k.TenKho, N'Kho trung tâm') AS TenKho,
                        ISNULL(pn.MaDonViCapHang, 0) AS MaDonViCapHang,
                        ISNULL(ncc.TenDonVi, N'Nhà cung cấp lẻ') AS TenDonVi,
                        ISNULL(ncc.MaNcc, 'NCC' + RIGHT('00000' + CAST(ISNULL(ncc.MaDonViCapHang, 0) AS VARCHAR(10)), 5)) AS MaNcc,
                        pn.CanTraNcc
                    FROM PhieuNhap pn
                    LEFT JOIN Kho k ON pn.MaKho = k.MaKho
                    LEFT JOIN DonViCapHang ncc ON pn.MaDonViCapHang = ncc.MaDonViCapHang
                    WHERE pn.MaPhieuNhap = @MaPhieuNhap";

                DataTable dtH = DataProcessing.ReadData(sqlHeader, new SqlParameter[] {
                    new SqlParameter("@MaPhieuNhap", maPhieuNhap)
                });

                if (dtH.Rows.Count == 0)
                {
                    MessageBox.Show("Không tìm thấy phiếu nhập này!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    this.Close();
                    return;
                }

                DataRow rh = dtH.Rows[0];
                maPhieuGoc = rh["MaPhieu"].ToString();
                maKho = Convert.ToInt32(rh["MaKho"]);
                maDonViCapHang = Convert.ToInt32(rh["MaDonViCapHang"]);
                canTraNccGoc = Convert.ToDecimal(rh["CanTraNcc"]);

                lblMaPhieuGoc.Text = $"(Theo đơn nhập: {maPhieuGoc})";
                lblNccVal.Text = $"{rh["TenDonVi"]} ({rh["MaNcc"]})";
                lblKhoVal.Text = rh["TenKho"].ToString();
                lblNoCanTraGocVal.Text = canTraNccGoc.ToString("N0") + " đ";

                // 2. Lấy danh sách hàng hóa và tồn kho thực tế hiện tại
                string sqlItems = @"
                    SELECT 
                        ROW_NUMBER() OVER (ORDER BY ct.MaCTPhieuNhap ASC) AS STT,
                        ct.MaHang,
                        h.MaHangNoiBo,
                        h.TenHang,
                        ISNULL(d.TenDvt, N'Cái') AS TenDvt,
                        ct.SoLuong AS SoLuongNhap,
                        ISNULL(ton.SoLuongTon, 0) AS TonKho,
                        CAST(0 AS DECIMAL(18,2)) AS SoLuongTra,
                        ct.DonGiaNhap,
                        CAST(0 AS DECIMAL(18,2)) AS ThanhTienTra
                    FROM ChiTietPhieuNhap ct
                    JOIN HangHoa h ON ct.MaHang = h.MaHang
                    LEFT JOIN DonViTinh d ON h.MaDvt = d.MaDvt
                    LEFT JOIN vw_TonKhoHienTai ton ON ton.MaHang = ct.MaHang AND ton.MaKho = @MaKho
                    WHERE ct.MaPhieuNhap = @MaPhieuNhap
                    ORDER BY ct.MaCTPhieuNhap ASC";

                dtHangTra = DataProcessing.ReadData(sqlItems, new SqlParameter[] {
                    new SqlParameter("@MaPhieuNhap", maPhieuNhap),
                    new SqlParameter("@MaKho", maKho)
                });

                dgvHangTra.DataSource = dtHangTra;

                TinhToanTienVaCongNo();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi nạp dữ liệu trả hàng: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void dgvHangTra_CellValidating(object sender, DataGridViewCellValidatingEventArgs e)
        {
            if (e.RowIndex < 0) return;
            if (dgvHangTra.Columns[e.ColumnIndex].Name == "colSoLuongTra")
            {
                string input = e.FormattedValue?.ToString().Trim();
                if (string.IsNullOrEmpty(input))
                {
                    // Trống coi như 0
                    return;
                }

                if (!decimal.TryParse(input, out decimal val))
                {
                    MessageBox.Show("Số lượng trả phải là số hợp lệ!", "Lỗi nhập liệu", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    e.Cancel = true;
                    return;
                }

                if (val < 0)
                {
                    MessageBox.Show("Số lượng trả không được nhỏ hơn 0!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    e.Cancel = true;
                    return;
                }

                decimal slNhap = Convert.ToDecimal(dgvHangTra.Rows[e.RowIndex].Cells["colSoLuongNhap"].Value);
                if (val > slNhap)
                {
                    MessageBox.Show($"Số lượng trả ({val:N0}) không được vượt quá số lượng đã nhập ({slNhap:N0})!", "Cảnh báo vượt số lượng", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    e.Cancel = true;
                    return;
                }
            }
        }

        private void dgvHangTra_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (isCalculating) return;
            if (e.RowIndex >= 0 && dgvHangTra.Columns[e.ColumnIndex].Name == "colSoLuongTra")
            {
                CapNhatDongTra(e.RowIndex);
                TinhToanTienVaCongNo();
            }
        }

        private void CapNhatDongTra(int rowIndex)
        {
            if (rowIndex < 0 || rowIndex >= dgvHangTra.Rows.Count) return;

            DataGridViewRow row = dgvHangTra.Rows[rowIndex];
            decimal slTra = 0;
            decimal donGia = 0;

            if (row.Cells["colSoLuongTra"].Value != null)
                decimal.TryParse(row.Cells["colSoLuongTra"].Value.ToString(), out slTra);

            if (row.Cells["colDonGiaTra"].Value != null)
                decimal.TryParse(row.Cells["colDonGiaTra"].Value.ToString(), out donGia);

            decimal slNhap = Convert.ToDecimal(row.Cells["colSoLuongNhap"].Value);
            slTra = Math.Floor(slTra);
            if (slTra < 0) slTra = 0;
            if (slTra > slNhap) slTra = slNhap;

            decimal thanhTien = slTra * donGia;

            isCalculating = true;
            row.Cells["colSoLuongTra"].Value = slTra;
            row.Cells["colThanhTien"].Value = thanhTien;
            isCalculating = false;
        }

        private void TinhToanTienVaCongNo()
        {
            decimal tongSlTra = 0;
            decimal tongTienTra = 0;

            foreach (DataGridViewRow row in dgvHangTra.Rows)
            {
                if (row.Cells["colSoLuongTra"].Value != null)
                {
                    decimal.TryParse(row.Cells["colSoLuongTra"].Value.ToString(), out decimal sl);
                    tongSlTra += sl;
                }
                if (row.Cells["colThanhTien"].Value != null)
                {
                    decimal.TryParse(row.Cells["colThanhTien"].Value.ToString(), out decimal tt);
                    tongTienTra += tt;
                }
            }

            lblTongSlTra.Text = $"Tổng số lượng trả: {tongSlTra:N0}";
            txtTongTienTra.Text = tongTienTra.ToString("N0");

            // Tự động tính toán công nợ theo yêu cầu:
            // "tự động tính toán số tiền hàng dựa trên giá hàng để trừ vào số nợ cần trả hoặc số tiền nhà cung cấp cần trả lại"
            decimal truVaoCongNo = 0;
            decimal noConLai = 0;
            decimal tienNccTraLai = 0;

            if (tongTienTra <= canTraNccGoc)
            {
                // Tiền hàng trả <= số nợ đang thiếu: Trừ toàn bộ vào nợ
                truVaoCongNo = tongTienTra;
                noConLai = canTraNccGoc - tongTienTra;
                tienNccTraLai = 0;
            }
            else
            {
                // Tiền hàng trả > số nợ: Xóa hết nợ, phần chênh lệch là NCC cần trả lại tiền!
                truVaoCongNo = canTraNccGoc;
                noConLai = 0;
                tienNccTraLai = tongTienTra - canTraNccGoc;
            }

            txtTruVaoCongNo.Text = truVaoCongNo.ToString("N0");
            txtNoConLai.Text = noConLai.ToString("N0");
            txtTienNccTraLai.Text = tienNccTraLai.ToString("N0");
        }

        private void dgvHangTra_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.RowIndex < 0) return;

            // Cảnh báo nếu số lượng trả vượt tồn kho hiện tại
            if (dgvHangTra.Columns[e.ColumnIndex].Name == "colTonKho" || dgvHangTra.Columns[e.ColumnIndex].Name == "colSoLuongTra")
            {
                DataGridViewRow row = dgvHangTra.Rows[e.RowIndex];
                if (row.Cells["colSoLuongTra"].Value != null && row.Cells["colTonKho"].Value != null)
                {
                    decimal.TryParse(row.Cells["colSoLuongTra"].Value.ToString(), out decimal slTra);
                    decimal.TryParse(row.Cells["colTonKho"].Value.ToString(), out decimal tonKho);

                    if (slTra > tonKho && slTra > 0)
                    {
                        row.Cells["colSoLuongTra"].Style.ForeColor = Color.Red;
                    }
                    else
                    {
                        row.Cells["colSoLuongTra"].Style.ForeColor = Color.Black;
                    }
                }
            }
        }

        private void btnXacNhanTra_Click(object sender, EventArgs e)
        {
            dgvHangTra.EndEdit();

            // 1. Kiểm tra có mặt hàng nào trả không
            decimal tongSlTra = 0;
            decimal tongTienTra = 0;
            foreach (DataGridViewRow row in dgvHangTra.Rows)
            {
                decimal.TryParse(row.Cells["colSoLuongTra"].Value?.ToString(), out decimal sl);
                decimal.TryParse(row.Cells["colThanhTien"].Value?.ToString(), out decimal tt);
                tongSlTra += sl;
                tongTienTra += tt;
            }

            if (tongSlTra <= 0)
            {
                MessageBox.Show("Vui lòng nhập số lượng hàng cần trả (> 0) cho ít nhất một mặt hàng!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // 2. Kiểm tra tồn kho an toàn (Chặn âm tồn theo Luật 6)
            foreach (DataGridViewRow row in dgvHangTra.Rows)
            {
                decimal.TryParse(row.Cells["colSoLuongTra"].Value?.ToString(), out decimal slTra);
                decimal.TryParse(row.Cells["colTonKho"].Value?.ToString(), out decimal tonKho);
                string sku = row.Cells["colMaHangNoiBo"].Value?.ToString();
                string ten = row.Cells["colTenHang"].Value?.ToString();

                if (slTra > 0 && slTra > tonKho)
                {
                    MessageBox.Show(
                        $"Không thể thực hiện trả hàng vì mặt hàng '{sku} - {ten}' có số lượng trả ({slTra:N0}) vượt quá tồn kho thực tế hiện tại ({tonKho:N0}) tại kho này!\n\nVui lòng kiểm tra lại số lượng tồn thực tế.",
                        "Chặn xuất âm tồn kho",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Stop
                    );
                    return;
                }
            }

            if (cboNguoiTra.SelectedItem == null)
            {
                MessageBox.Show("Vui lòng chọn nhân viên thực hiện trả hàng!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                cboNguoiTra.Focus();
                return;
            }

            // 3. Tính toán các chỉ số tài chính
            decimal truVaoCongNo = 0;
            decimal noConLai = 0;
            decimal tienNccTraLai = 0;

            if (tongTienTra <= canTraNccGoc)
            {
                truVaoCongNo = tongTienTra;
                noConLai = canTraNccGoc - tongTienTra;
                tienNccTraLai = 0;
            }
            else
            {
                truVaoCongNo = canTraNccGoc;
                noConLai = 0;
                tienNccTraLai = tongTienTra - canTraNccGoc;
            }

            string xacNhanMsg = $"XÁC NHẬN TRẢ HÀNG CHO NHÀ CUNG CẤP:\n\n" +
                                $"- Tổng số lượng trả: {tongSlTra:N0} sản phẩm\n" +
                                $"- Tổng giá trị hoàn lại: {tongTienTra:N0} đ\n" +
                                $"- Trừ vào công nợ đơn: {truVaoCongNo:N0} đ (Nợ đơn còn lại: {noConLai:N0} đ)\n" +
                                $"- Số tiền NCC cần hoàn trả lại: {tienNccTraLai:N0} đ\n\n" +
                                $"Bạn có chắc chắn muốn tiến hành trả hàng?";

            if (MessageBox.Show(xacNhanMsg, "Xác nhận trả hàng", MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
            {
                return;
            }

            int maNvTra = Convert.ToInt32((cboNguoiTra.SelectedItem as ComboBoxItem).Value);
            int maNvLap = SessionContext.MaTaiKhoan > 0 ? SessionContext.MaTaiKhoan : maNvTra;
            DateTime ngayTra = dtpNgayTra.Value;
            string ghiChu = txtGhiChu.Text.Trim();

            // 4. Bọc trong Transaction an toàn
            string maPhieuTraMoi = "";
            try
            {
                DataProcessing.ChangeDataTransaction((conn, trans) =>
                {
                    // 4.1. Sinh mã phiếu trả hàng THN000001
                    string nextIdSql = "SELECT ISNULL(MAX(MaPhieuTraHang), 0) + 1 FROM PhieuTraHangNhap";
                    SqlCommand cmdNextId = new SqlCommand(nextIdSql, conn, trans);
                    int nextId = Convert.ToInt32(cmdNextId.ExecuteScalar());
                    string maPhieuTra = "THN" + nextId.ToString("D6");
                    maPhieuTraMoi = maPhieuTra;

                    // 4.2. Thêm PhieuTraHangNhap
                    string insertPthSql = @"
                        INSERT INTO PhieuTraHangNhap (
                            MaPhieu, MaKho, MaDonViCapHang, MaNvlap, MaNvtra,
                            NgayLap, NgayTra, TrangThai, TongSoLuong, TongTienHang,
                            GiamGia, CanThuNcc, DaThuNcc, GhiChu, DaDanhDau
                        ) VALUES (
                            @MaPhieu, @MaKho, @MaDonViCapHang, @MaNvlap, @MaNvtra,
                            GETDATE(), @NgayTra, 'DA_TRA_HANG', @TongSoLuong, @TongTienHang,
                            0, @CanThuNcc, 0, @GhiChu, 0
                        );
                        SELECT SCOPE_IDENTITY();";

                    SqlCommand cmdPth = new SqlCommand(insertPthSql, conn, trans);
                    cmdPth.Parameters.AddWithValue("@MaPhieu", maPhieuTra);
                    cmdPth.Parameters.AddWithValue("@MaKho", maKho);
                    cmdPth.Parameters.AddWithValue("@MaDonViCapHang", maDonViCapHang > 0 ? (object)maDonViCapHang : DBNull.Value);
                    cmdPth.Parameters.AddWithValue("@MaNvlap", maNvLap);
                    cmdPth.Parameters.AddWithValue("@MaNvtra", maNvTra);
                    cmdPth.Parameters.AddWithValue("@NgayTra", ngayTra);
                    cmdPth.Parameters.AddWithValue("@TongSoLuong", tongSlTra);
                    cmdPth.Parameters.AddWithValue("@TongTienHang", tongTienTra);
                    cmdPth.Parameters.AddWithValue("@CanThuNcc", tienNccTraLai);
                    cmdPth.Parameters.AddWithValue("@GhiChu", $"Trả hàng từ đơn nhập {maPhieuGoc}. " + ghiChu);

                    int maPhieuTraHangMoi = Convert.ToInt32(cmdPth.ExecuteScalar());

                    // 4.3. Thêm ChiTietPhieuTraHangNhap
                    string insertCtpSql = @"
                        INSERT INTO ChiTietPhieuTraHangNhap (
                            MaPhieuTraHang, MaHang, SoLuong, GiaVon, DonGiaTra, ThanhTien, GhiChu
                        ) VALUES (
                            @MaPhieuTraHang, @MaHang, @SoLuong, @DonGiaTra, @DonGiaTra, @ThanhTien, @GhiChu
                        );";

                    foreach (DataGridViewRow row in dgvHangTra.Rows)
                    {
                        decimal.TryParse(row.Cells["colSoLuongTra"].Value?.ToString(), out decimal slTra);
                        if (slTra > 0)
                        {
                            int maHang = Convert.ToInt32(row.Cells["colMaHang"].Value);
                            decimal donGia = Convert.ToDecimal(row.Cells["colDonGiaTra"].Value);
                            decimal thanhTien = Convert.ToDecimal(row.Cells["colThanhTien"].Value);

                            SqlCommand cmdCt = new SqlCommand(insertCtpSql, conn, trans);
                            cmdCt.Parameters.AddWithValue("@MaPhieuTraHang", maPhieuTraHangMoi);
                            cmdCt.Parameters.AddWithValue("@MaHang", maHang);
                            cmdCt.Parameters.AddWithValue("@SoLuong", slTra);
                            cmdCt.Parameters.AddWithValue("@DonGiaTra", donGia);
                            cmdCt.Parameters.AddWithValue("@ThanhTien", thanhTien);
                            cmdCt.Parameters.AddWithValue("@GhiChu", $"Trả từ {maPhieuGoc}");
                            cmdCt.ExecuteNonQuery();
                        }
                    }

                    // 4.4. Cập nhật giảm nợ cần trả của PhieuNhap gốc
                    string updatePnSql = @"
                        UPDATE PhieuNhap 
                        SET CanTraNcc = @NoConLai 
                        WHERE MaPhieuNhap = @MaPhieuNhap";
                    SqlCommand cmdPn = new SqlCommand(updatePnSql, conn, trans);
                    cmdPn.Parameters.AddWithValue("@NoConLai", noConLai);
                    cmdPn.Parameters.AddWithValue("@MaPhieuNhap", maPhieuNhap);
                    cmdPn.ExecuteNonQuery();

                    // 4.5. Ghi nhật ký kiểm toán thao tác
                    string logSql = @"
                        INSERT INTO NhatKyThaoTac (MaTaiKhoan, ThoiDiem, ThaoTac, LoaiDoiTuong, MaDoiTuong, NoiDung)
                        VALUES (@MaTaiKhoan, GETDATE(), 'TRA_HANG_NHAP', 'PhieuTraHangNhap', @MaDoiTuong, @NoiDung)";
                    SqlCommand cmdLog = new SqlCommand(logSql, conn, trans);
                    cmdLog.Parameters.AddWithValue("@MaTaiKhoan", SessionContext.MaTaiKhoan > 0 ? SessionContext.MaTaiKhoan : 1);
                    cmdLog.Parameters.AddWithValue("@MaDoiTuong", maPhieuTra);
                    cmdLog.Parameters.AddWithValue("@NoiDung", $"Lập phiếu trả hàng {maPhieuTra} cho đơn {maPhieuGoc}, tổng SL: {tongSlTra:N0}, tổng tiền: {tongTienTra:N0}, trừ nợ: {truVaoCongNo:N0}, NCC hoàn lại: {tienNccTraLai:N0}");
                    cmdLog.ExecuteNonQuery();
                });

                MessageBox.Show($"Xác nhận trả hàng thành công!\nPhiếu trả hàng đã sinh: {maPhieuTraMoi}", "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi trong quá trình lưu phiếu trả hàng: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnBoQua_Click(object sender, EventArgs e)
        {
            this.Close();
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
    
        private void dgvHangTra_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        {
            if (e.RowIndex >= 0 && e.ColumnIndex == colSoLuongTra.Index)
            {
                // 1. Vẽ nền hàng
                e.PaintBackground(e.ClipBounds, true);

                // 2. Tính toán khung ô nhập liệu dạng hộp TextBox rõ nét
                Rectangle boxRect = new Rectangle(e.CellBounds.X + 4, e.CellBounds.Y + 4, e.CellBounds.Width - 8, e.CellBounds.Height - 8);

                // 3. Tô nền hộp nhập liệu màu trắng tinh
                using (SolidBrush brush = new SolidBrush(Color.White))
                {
                    e.Graphics.FillRectangle(brush, boxRect);
                }

                // 4. Vẽ viền hộp nhập liệu: xanh dương đậm nổi bật nếu ô đang active/chọn, viền xám rõ ràng nếu bình thường
                bool isCurrent = dgvHangTra.CurrentCell != null && dgvHangTra.CurrentCell.RowIndex == e.RowIndex && dgvHangTra.CurrentCell.ColumnIndex == e.ColumnIndex;
                Color borderColor = isCurrent ? Color.FromArgb(37, 99, 235) : Color.FromArgb(148, 163, 184);
                int penWidth = isCurrent ? 2 : 1;

                using (Pen pen = new Pen(borderColor, penWidth))
                {
                    e.Graphics.DrawRectangle(pen, boxRect);
                }

                // 5. Vẽ giá trị số lượng căn phải bên trong hộp
                string valStr = e.FormattedValue != null ? Convert.ToDecimal(e.Value ?? 0).ToString("N0") : "0";
                Rectangle textRect = new Rectangle(boxRect.X, boxRect.Y, boxRect.Width - 8, boxRect.Height);
                TextRenderer.DrawText(e.Graphics, valStr, e.CellStyle.Font, textRect, Color.FromArgb(30, 41, 59),
                    TextFormatFlags.Right | TextFormatFlags.VerticalCenter);

                e.Handled = true;
            }
        }

        private void dgvHangTra_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            if (dgvHangTra.CurrentCell != null && dgvHangTra.CurrentCell.ColumnIndex == colSoLuongTra.Index)
            {
                if (e.Control is TextBox tb)
                {
                    tb.TextAlign = HorizontalAlignment.Right;
                    tb.Font = new Font("Segoe UI", 9.75F, FontStyle.Bold);
                    tb.KeyPress -= Tb_KeyPress;
                    tb.KeyPress += Tb_KeyPress;
                    this.BeginInvoke(new Action(() => tb.SelectAll()));
                }
            }
        }

        private void Tb_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Chỉ cho phép nhập số nguyên (0-9) và các phím điều khiển (Backspace)
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }
}
}
