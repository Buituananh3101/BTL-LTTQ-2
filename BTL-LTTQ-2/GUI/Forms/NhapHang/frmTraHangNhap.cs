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

            // Cột "SL đã trả": số lượng đã trả (hoặc đang nằm trong phiếu trả tạm) theo đúng phiếu nhập gốc này
            if (!dgvHangTra.Columns.Contains("colSoLuongDaTra"))
            {
                DataGridViewTextBoxColumn colSoLuongDaTra = new DataGridViewTextBoxColumn
                {
                    Name = "colSoLuongDaTra",
                    DataPropertyName = "SoLuongDaTra",
                    HeaderText = "SL đã trả",
                    ReadOnly = true,
                    Width = 85,
                    SortMode = DataGridViewColumnSortMode.NotSortable
                };
                colSoLuongDaTra.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                colSoLuongDaTra.DefaultCellStyle.Format = "N0";
                dgvHangTra.Columns.Insert(colSoLuongNhap.Index + 1, colSoLuongDaTra);
            }

            // Lưới có thể bind lại khi hiển thị -> khóa lại các dòng đã trả hết sau mỗi lần bind
            dgvHangTra.DataBindingComplete += (s, e) => KhoaDongDaTraHet();

            // Đánh dấu ô số lượng trả rõ ràng bằng font chữ đậm
            // colSoLuongTra.DefaultCellStyle.Font = Segoe UI 9.5F Regular
        }

        /// <summary>
        /// Số lượng còn được phép trả của một dòng = SL đã nhập trên phiếu gốc - SL đã trả (phiếu trả tạm + đã trả) theo phiếu gốc
        /// </summary>
        private decimal LaySoLuongConDuocTra(DataGridViewRow row)
        {
            decimal slNhap = 0, slDaTra = 0;
            if (row.Cells["colSoLuongNhap"].Value != null && row.Cells["colSoLuongNhap"].Value != DBNull.Value)
                slNhap = Convert.ToDecimal(row.Cells["colSoLuongNhap"].Value);
            if (row.Cells["colSoLuongDaTra"].Value != null && row.Cells["colSoLuongDaTra"].Value != DBNull.Value)
                slDaTra = Convert.ToDecimal(row.Cells["colSoLuongDaTra"].Value);
            decimal conLai = slNhap - slDaTra;
            return conLai > 0 ? conLai : 0;
        }

        /// <summary>
        /// Khóa ô "SL cần trả" của các dòng đã trả hết theo phiếu gốc. Trả về tổng số lượng còn được trả của cả phiếu.
        /// </summary>
        private decimal KhoaDongDaTraHet()
        {
            decimal tongConDuocTra = 0;
            foreach (DataGridViewRow r in dgvHangTra.Rows)
            {
                decimal conLai = LaySoLuongConDuocTra(r);
                tongConDuocTra += conLai;
                if (conLai <= 0)
                {
                    r.Cells["colSoLuongTra"].ReadOnly = true;
                    r.DefaultCellStyle.ForeColor = Color.Gray;
                }
            }
            return tongConDuocTra;
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
                // Giá trị chọn là NhanVien.MaNV (khóa ngoại MaNvtra), chỉ lấy nhân viên đang làm có tài khoản hoạt động
                string sql = @"
                    SELECT nv.MaNV, nv.HoTen
                    FROM NhanVien nv
                    WHERE nv.TrangThai = 'DANG_LAM'
                      AND EXISTS (SELECT 1 FROM TaiKhoan tk WHERE tk.MaNV = nv.MaNV AND tk.TrangThai = 'HOAT_DONG')
                    ORDER BY nv.HoTen ASC";
                DataTable dt = DataProcessing.ReadData(sql);
                int viTriMacDinh = -1;
                foreach (DataRow row in dt.Rows)
                {
                    int id = Convert.ToInt32(row["MaNV"]);
                    string ten = row["HoTen"].ToString();
                    int viTri = cboNguoiTra.Items.Add(new ComboBoxItem(ten, id));
                    if (SessionContext.MaNV.HasValue && SessionContext.MaNV.Value == id)
                    {
                        viTriMacDinh = viTri;
                    }
                }
                if (viTriMacDinh >= 0)
                {
                    cboNguoiTra.SelectedIndex = viTriMacDinh;
                }
                else if (cboNguoiTra.Items.Count > 0)
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

                // 2. Lấy danh sách hàng hóa (gộp theo mặt hàng), số lượng đã trả theo phiếu gốc và tồn kho thực tế hiện tại
                //    - SoLuongDaTra: tổng SL trên các phiếu trả hàng nhập (tạm + đã trả) lập từ phiếu nhập này.
                //    - DonGiaNhap (giá trả lại): đơn giá NET của dòng = (SL * Đơn giá nhập - Giảm giá dòng) / SL.
                //      Lưu ý: KHÔNG phân bổ giảm giá tổng phiếu (PhieuNhap.GiamGia) vào giá trả lại.
                string sqlItems = @"
                    SELECT
                        ROW_NUMBER() OVER (ORDER BY MIN(ct.MaCTPhieuNhap) ASC) AS STT,
                        ct.MaHang,
                        h.MaHangNoiBo,
                        h.TenHang,
                        ISNULL(d.TenDvt, N'Cái') AS TenDvt,
                        SUM(ct.SoLuong) AS SoLuongNhap,
                        ISNULL(MAX(tra.SoLuongDaTra), 0) AS SoLuongDaTra,
                        ISNULL(MAX(ton.SoLuongTon), 0) AS TonKho,
                        CAST(0 AS DECIMAL(18,2)) AS SoLuongTra,
                        CAST(ROUND((SUM(ct.SoLuong * ct.DonGiaNhap) - SUM(ct.GiamGia)) / NULLIF(SUM(ct.SoLuong), 0), 2) AS DECIMAL(18,2)) AS DonGiaNhap,
                        CAST(0 AS DECIMAL(18,2)) AS ThanhTienTra
                    FROM ChiTietPhieuNhap ct
                    JOIN HangHoa h ON ct.MaHang = h.MaHang
                    LEFT JOIN DonViTinh d ON h.MaDvt = d.MaDvt
                    LEFT JOIN vw_TonKhoHienTai ton ON ton.MaHang = ct.MaHang AND ton.MaKho = @MaKho
                    OUTER APPLY (
                        SELECT SUM(ctt.SoLuong) AS SoLuongDaTra
                        FROM ChiTietPhieuTraHangNhap ctt
                        JOIN PhieuTraHangNhap pt ON ctt.MaPhieuTraHang = pt.MaPhieuTraHang
                        WHERE pt.MaPhieuNhap = @MaPhieuNhap
                          AND pt.TrangThai IN ('DA_TRA_HANG', 'NHAP')
                          AND ctt.MaHang = ct.MaHang
                    ) tra
                    WHERE ct.MaPhieuNhap = @MaPhieuNhap
                    GROUP BY ct.MaHang, h.MaHangNoiBo, h.TenHang, d.TenDvt
                    ORDER BY MIN(ct.MaCTPhieuNhap) ASC";

                dtHangTra = DataProcessing.ReadData(sqlItems, new SqlParameter[] {
                    new SqlParameter("@MaPhieuNhap", maPhieuNhap),
                    new SqlParameter("@MaKho", maKho)
                });

                dgvHangTra.DataSource = dtHangTra;

                // 3. Khóa ô nhập số lượng của các dòng đã trả hết; chặn trả nếu toàn phiếu không còn gì để trả
                decimal tongConDuocTra = KhoaDongDaTraHet();

                if (tongConDuocTra <= 0)
                {
                    btnXacNhanTra.Enabled = false;
                    MessageBox.Show($"Toàn bộ hàng hóa của phiếu nhập '{maPhieuGoc}' đã được trả hết (hoặc đang nằm trong phiếu trả hàng tạm).\nKhông thể lập thêm phiếu trả hàng cho phiếu nhập này.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }

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

                // Số lượng trả tối đa = SL đã nhập trên phiếu gốc - SL đã trả theo phiếu gốc
                DataGridViewRow rowDangSua = dgvHangTra.Rows[e.RowIndex];
                decimal slNhap = Convert.ToDecimal(rowDangSua.Cells["colSoLuongNhap"].Value);
                decimal slConDuocTra = LaySoLuongConDuocTra(rowDangSua);
                if (val > slConDuocTra)
                {
                    MessageBox.Show($"Số lượng trả ({val:N0}) không được vượt quá số lượng còn được trả ({slConDuocTra:N0}).\n(Đã nhập: {slNhap:N0}, đã trả theo phiếu này: {slNhap - slConDuocTra:N0})", "Cảnh báo vượt số lượng", MessageBoxButtons.OK, MessageBoxIcon.Warning);
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

            decimal slConDuocTra = LaySoLuongConDuocTra(row);
            slTra = Math.Floor(slTra);
            if (slTra < 0) slTra = 0;
            if (slTra > slConDuocTra) slTra = slConDuocTra;

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

            // Người lập phiếu là nhân viên gắn với tài khoản đăng nhập (khóa ngoại NhanVien.MaNV, không dùng MaTaiKhoan)
            if (!SessionContext.MaNV.HasValue || SessionContext.MaNV.Value <= 0)
            {
                MessageBox.Show("Tài khoản chưa được gắn với nhân viên, không thể lập chứng từ.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            int maNvTra = Convert.ToInt32((cboNguoiTra.SelectedItem as ComboBoxItem).Value);
            int maNvLap = SessionContext.MaNV.Value;
            DateTime ngayTra = dtpNgayTra.Value;
            string ghiChu = txtGhiChu.Text.Trim();

            // Gom các dòng có số lượng trả > 0 để dùng trong giao dịch
            var dongTra = new System.Collections.Generic.List<Tuple<int, string, decimal, decimal, decimal>>();
            foreach (DataGridViewRow row in dgvHangTra.Rows)
            {
                decimal.TryParse(row.Cells["colSoLuongTra"].Value?.ToString(), out decimal slTra);
                if (slTra > 0)
                {
                    int maHang = Convert.ToInt32(row.Cells["colMaHang"].Value);
                    string tenHienThi = $"{row.Cells["colMaHangNoiBo"].Value} - {row.Cells["colTenHang"].Value}";
                    decimal donGia = Convert.ToDecimal(row.Cells["colDonGiaTra"].Value);
                    decimal thanhTien = Convert.ToDecimal(row.Cells["colThanhTien"].Value);
                    dongTra.Add(Tuple.Create(maHang, tenHienThi, slTra, donGia, thanhTien));
                }
            }

            // 4. Bọc trong Transaction an toàn
            string maPhieuTraMoi = "";
            decimal truVaoCongNoLuu = 0;
            decimal tienNccTraLaiLuu = 0;
            try
            {
                DataProcessing.ChangeDataTransaction((conn, trans) =>
                {
                    // 4.0. Đọc lại (khóa dòng) phiếu nhập gốc để tính công nợ theo số liệu mới nhất
                    decimal canTraNccMoiNhat;
                    using (SqlCommand cmdGoc = new SqlCommand("SELECT TrangThai, CanTraNcc FROM PhieuNhap WITH (UPDLOCK, ROWLOCK) WHERE MaPhieuNhap = @MaPhieuNhap", conn, trans))
                    {
                        cmdGoc.Parameters.AddWithValue("@MaPhieuNhap", maPhieuNhap);
                        using (SqlDataReader rd = cmdGoc.ExecuteReader())
                        {
                            if (!rd.Read() || rd["TrangThai"].ToString() != "DA_XAC_NHAN")
                            {
                                throw new InvalidOperationException($"Phiếu nhập '{maPhieuGoc}' không còn ở trạng thái 'Đã nhập hàng', không thể trả hàng.");
                            }
                            canTraNccMoiNhat = Convert.ToDecimal(rd["CanTraNcc"]);
                        }
                    }

                    // 4.1. Kiểm tra lại trong giao dịch: SL còn được trả theo phiếu gốc & tồn kho (chặn âm tồn)
                    string sqlKiemTraDong = @"
                        SELECT
                            (SELECT ISNULL(SUM(ct.SoLuong), 0) FROM ChiTietPhieuNhap ct
                             WHERE ct.MaPhieuNhap = @MaPhieuNhap AND ct.MaHang = @MaHang) AS SoLuongNhap,
                            (SELECT ISNULL(SUM(ctt.SoLuong), 0)
                             FROM ChiTietPhieuTraHangNhap ctt
                             JOIN PhieuTraHangNhap pt WITH (UPDLOCK, HOLDLOCK) ON ctt.MaPhieuTraHang = pt.MaPhieuTraHang
                             WHERE pt.MaPhieuNhap = @MaPhieuNhap AND pt.TrangThai IN ('DA_TRA_HANG', 'NHAP') AND ctt.MaHang = @MaHang) AS SoLuongDaTra,
                            (SELECT ISNULL(SUM(t.SoLuongTon), 0) FROM vw_TonKhoHienTai t
                             WHERE t.MaKho = @MaKho AND t.MaHang = @MaHang) AS TonKho";

                    foreach (var dong in dongTra)
                    {
                        using (SqlCommand cmdKt = new SqlCommand(sqlKiemTraDong, conn, trans))
                        {
                            cmdKt.Parameters.AddWithValue("@MaPhieuNhap", maPhieuNhap);
                            cmdKt.Parameters.AddWithValue("@MaHang", dong.Item1);
                            cmdKt.Parameters.AddWithValue("@MaKho", maKho);
                            using (SqlDataReader rd = cmdKt.ExecuteReader())
                            {
                                rd.Read();
                                decimal slNhap = Convert.ToDecimal(rd["SoLuongNhap"]);
                                decimal slDaTra = Convert.ToDecimal(rd["SoLuongDaTra"]);
                                decimal tonKho = Convert.ToDecimal(rd["TonKho"]);

                                if (dong.Item3 > slNhap - slDaTra)
                                {
                                    throw new InvalidOperationException(
                                        $"Mặt hàng '{dong.Item2}' chỉ còn được trả tối đa {Math.Max(0, slNhap - slDaTra):N0} (đã nhập {slNhap:N0}, đã trả theo phiếu này {slDaTra:N0}).\nVui lòng tải lại và kiểm tra số lượng trả.");
                                }
                                if (dong.Item3 > tonKho)
                                {
                                    throw new InvalidOperationException(
                                        $"Không thể thực hiện trả hàng vì mặt hàng '{dong.Item2}' có số lượng trả ({dong.Item3:N0}) vượt quá tồn kho thực tế hiện tại ({tonKho:N0}) tại kho này!\n\nVui lòng kiểm tra lại số lượng tồn thực tế.");
                                }
                            }
                        }
                    }

                    // 4.2. Tính lại các chỉ số công nợ theo số liệu mới nhất
                    decimal truNoMoi = tongTienTra <= canTraNccMoiNhat ? tongTienTra : Math.Max(0, canTraNccMoiNhat);
                    decimal noConLaiMoi = canTraNccMoiNhat - truNoMoi;
                    decimal tienHoanLaiMoi = tongTienTra - truNoMoi;

                    // 4.3. Sinh mã phiếu trả hàng THN000001 (theo hậu tố số lớn nhất, lặp đến khi chưa bị dùng)
                    string maPhieuTra = SinhMaPhieuTraHangNhap(conn, trans);
                    maPhieuTraMoi = maPhieuTra;

                    // 4.4. Thêm PhieuTraHangNhap (lưu liên kết MaPhieuNhap với phiếu nhập gốc)
                    string insertPthSql = @"
                        INSERT INTO PhieuTraHangNhap (
                            MaPhieu, MaKho, MaDonViCapHang, MaNvlap, MaNvtra,
                            NgayLap, NgayTra, TrangThai, TongSoLuong, TongTienHang,
                            GiamGia, CanThuNcc, DaThuNcc, GhiChu, DaDanhDau, MaPhieuNhap
                        ) VALUES (
                            @MaPhieu, @MaKho, @MaDonViCapHang, @MaNvlap, @MaNvtra,
                            GETDATE(), @NgayTra, 'DA_TRA_HANG', @TongSoLuong, @TongTienHang,
                            0, @CanThuNcc, 0, @GhiChu, 0, @MaPhieuNhap
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
                    cmdPth.Parameters.AddWithValue("@CanThuNcc", tienHoanLaiMoi);
                    cmdPth.Parameters.AddWithValue("@GhiChu", $"Trả hàng từ đơn nhập {maPhieuGoc}. " + ghiChu);
                    cmdPth.Parameters.AddWithValue("@MaPhieuNhap", maPhieuNhap);

                    int maPhieuTraHangMoi = Convert.ToInt32(cmdPth.ExecuteScalar());

                    // 4.5. Thêm ChiTietPhieuTraHangNhap
                    string insertCtpSql = @"
                        INSERT INTO ChiTietPhieuTraHangNhap (
                            MaPhieuTraHang, MaHang, SoLuong, GiaVon, DonGiaTra, ThanhTien, GhiChu
                        ) VALUES (
                            @MaPhieuTraHang, @MaHang, @SoLuong, @DonGiaTra, @DonGiaTra, @ThanhTien, @GhiChu
                        );";

                    foreach (var dong in dongTra)
                    {
                        SqlCommand cmdCt = new SqlCommand(insertCtpSql, conn, trans);
                        cmdCt.Parameters.AddWithValue("@MaPhieuTraHang", maPhieuTraHangMoi);
                        cmdCt.Parameters.AddWithValue("@MaHang", dong.Item1);
                        cmdCt.Parameters.AddWithValue("@SoLuong", dong.Item3);
                        cmdCt.Parameters.AddWithValue("@DonGiaTra", dong.Item4);
                        cmdCt.Parameters.AddWithValue("@ThanhTien", dong.Item5);
                        cmdCt.Parameters.AddWithValue("@GhiChu", $"Trả từ {maPhieuGoc}");
                        cmdCt.ExecuteNonQuery();
                    }

                    // 4.6. Cập nhật giảm nợ cần trả của PhieuNhap gốc
                    string updatePnSql = @"
                        UPDATE PhieuNhap
                        SET CanTraNcc = @NoConLai
                        WHERE MaPhieuNhap = @MaPhieuNhap";
                    SqlCommand cmdPn = new SqlCommand(updatePnSql, conn, trans);
                    cmdPn.Parameters.AddWithValue("@NoConLai", noConLaiMoi);
                    cmdPn.Parameters.AddWithValue("@MaPhieuNhap", maPhieuNhap);
                    cmdPn.ExecuteNonQuery();

                    // 4.7. Ghi nhật ký kiểm toán thao tác
                    string logSql = @"
                        INSERT INTO NhatKyThaoTac (MaTaiKhoan, ThoiDiem, ThaoTac, LoaiDoiTuong, MaDoiTuong, NoiDung)
                        VALUES (@MaTaiKhoan, GETDATE(), 'TRA_HANG_NHAP', 'PhieuTraHangNhap', @MaDoiTuong, @NoiDung)";
                    SqlCommand cmdLog = new SqlCommand(logSql, conn, trans);
                    cmdLog.Parameters.AddWithValue("@MaTaiKhoan", SessionContext.MaTaiKhoan > 0 ? SessionContext.MaTaiKhoan : 1);
                    cmdLog.Parameters.AddWithValue("@MaDoiTuong", maPhieuTra);
                    cmdLog.Parameters.AddWithValue("@NoiDung", $"Lập phiếu trả hàng {maPhieuTra} cho đơn {maPhieuGoc}, tổng SL: {tongSlTra:N0}, tổng tiền: {tongTienTra:N0}, trừ nợ: {truNoMoi:N0}, NCC hoàn lại: {tienHoanLaiMoi:N0}");
                    cmdLog.ExecuteNonQuery();

                    truVaoCongNoLuu = truNoMoi;
                    tienNccTraLaiLuu = tienHoanLaiMoi;
                });

                MessageBox.Show($"Xác nhận trả hàng thành công!\nPhiếu trả hàng đã sinh: {maPhieuTraMoi}\n- Trừ vào công nợ đơn: {truVaoCongNoLuu:N0} đ\n- NCC cần hoàn trả lại: {tienNccTraLaiLuu:N0} đ", "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            catch (InvalidOperationException exNghiepVu)
            {
                // Vi phạm ràng buộc nghiệp vụ (vượt SL còn được trả / âm tồn) -> giao dịch đã rollback
                MessageBox.Show(exNghiepVu.Message, "Không thể trả hàng", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
            catch (SqlException exSql) when (exSql.Number == 2627 || exSql.Number == 2601)
            {
                MessageBox.Show("Mã phiếu trả hàng vừa sinh đã bị trùng do có người dùng khác lưu phiếu cùng lúc.\nVui lòng bấm 'Xác nhận trả hàng' lại để hệ thống sinh mã mới.", "Trùng mã phiếu", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi trong quá trình lưu phiếu trả hàng: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Sinh mã phiếu trả hàng nhập THNxxxxxx trong giao dịch: lấy hậu tố số lớn nhất của các mã THN hiện có + 1,
        /// sau đó lặp kiểm tra cho đến khi mã chưa được sử dụng.
        /// </summary>
        private static string SinhMaPhieuTraHangNhap(SqlConnection conn, SqlTransaction trans)
        {
            string sqlMax = @"
                SELECT ISNULL(MAX(TRY_CAST(SUBSTRING(MaPhieu, 4, 20) AS BIGINT)), 0)
                FROM PhieuTraHangNhap WITH (UPDLOCK, HOLDLOCK)
                WHERE MaPhieu LIKE 'THN[0-9]%'";
            long soHienTai;
            using (SqlCommand cmdMax = new SqlCommand(sqlMax, conn, trans))
            {
                soHienTai = Convert.ToInt64(cmdMax.ExecuteScalar());
            }

            while (true)
            {
                soHienTai++;
                string maMoi = "THN" + soHienTai.ToString("D6");
                using (SqlCommand cmdDem = new SqlCommand("SELECT COUNT(1) FROM PhieuTraHangNhap WHERE MaPhieu = @MaPhieu", conn, trans))
                {
                    cmdDem.Parameters.AddWithValue("@MaPhieu", maMoi);
                    if (Convert.ToInt32(cmdDem.ExecuteScalar()) == 0)
                    {
                        return maMoi;
                    }
                }
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
