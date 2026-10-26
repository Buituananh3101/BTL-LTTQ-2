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
    public partial class frmThemPhieuTraHang : Form
    {
        private DataTable dtHangTra;
        private DataTable dtDanhSachHang;
        private bool isCalculating = false;

        private int trangHienTai = 1;
        private int soDongMoiTrang = 20;
        private int tongSoTrang = 1;

        // Mã phiếu do hệ thống tự sinh (nếu người dùng giữ nguyên thì được phép sinh lại khi bị trùng lúc lưu)
        private string maPhieuTuSinh = "";

        public frmThemPhieuTraHang()
        {
            InitializeComponent();
            KhoiTaoBangHangTra();
            CauHinhDataGridView();

            // Ô tiền (VND, số nguyên): chỉ cho nhập chữ số, kiểm tra giới hạn khi rời ô
            txtGiamGia.KeyPress += TxtSoTien_KeyPress;
            txtDaThuNcc.KeyPress += TxtSoTien_KeyPress;
            txtGiamGia.Leave += (s, e) => KiemTraGioiHanTien(true);
            txtDaThuNcc.Leave += (s, e) => KiemTraGioiHanTien(true);
        }

        /// <summary>
        /// Đọc số tiền VND (số nguyên) từ ô nhập: bỏ toàn bộ dấu '.', ',' và khoảng trắng; không hợp lệ hoặc âm -> 0
        /// </summary>
        private static decimal DocSoTienVnd(string text)
        {
            if (string.IsNullOrWhiteSpace(text)) return 0;
            string sach = text.Replace(".", "").Replace(",", "").Replace(" ", "").Replace(" ", "").Trim();
            if (decimal.TryParse(sach, System.Globalization.NumberStyles.None, System.Globalization.CultureInfo.InvariantCulture, out decimal giaTri))
            {
                return giaTri;
            }
            return 0;
        }

        private void TxtSoTien_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Chỉ cho phép nhập chữ số (không cho số âm / ký tự khác)
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private decimal TinhTongTienHangTra()
        {
            decimal tong = 0;
            if (dtHangTra == null) return 0;
            foreach (DataRow r in dtHangTra.Rows)
            {
                if (r.RowState == DataRowState.Deleted) continue;
                tong += Convert.ToDecimal(r["ThanhTien"]);
            }
            return tong;
        }

        /// <summary>
        /// Giảm giá không vượt quá tổng tiền hàng trả; tiền NCC đã trả không vượt quá số cần thu. Vượt thì cảnh báo và điều chỉnh lại.
        /// Trả về false nếu có điều chỉnh.
        /// </summary>
        private bool KiemTraGioiHanTien(bool hienCanhBao)
        {
            bool hopLe = true;
            decimal tongTienHang = TinhTongTienHangTra();

            decimal giamGia = DocSoTienVnd(txtGiamGia.Text);
            if (giamGia > tongTienHang)
            {
                if (hienCanhBao)
                {
                    MessageBox.Show($"Giảm giá ({giamGia:N0} đ) không được vượt quá tổng tiền hàng trả ({tongTienHang:N0} đ).\nHệ thống đã điều chỉnh giảm giá về {tongTienHang:N0} đ.", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                giamGia = tongTienHang;
                txtGiamGia.Text = giamGia.ToString("N0");
                hopLe = false;
            }

            decimal canThuNcc = Math.Max(0, tongTienHang - giamGia);
            decimal daThuNcc = DocSoTienVnd(txtDaThuNcc.Text);
            if (daThuNcc > canThuNcc)
            {
                if (hienCanhBao)
                {
                    MessageBox.Show($"Tiền NCC đã trả ({daThuNcc:N0} đ) không được vượt quá số tiền NCC cần trả ({canThuNcc:N0} đ).\nHệ thống đã điều chỉnh về {canThuNcc:N0} đ.", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                txtDaThuNcc.Text = canThuNcc.ToString("N0");
                hopLe = false;
            }

            return hopLe;
        }

        /// <summary>
        /// Sinh mã phiếu trả hàng nhập THNxxxxxx: lấy hậu tố số lớn nhất của các mã THN hiện có + 1, lặp kiểm tra đến khi mã chưa được dùng.
        /// Khi gọi trong giao dịch (trans != null) sẽ khóa phạm vi để tránh 2 người lưu trùng mã.
        /// </summary>
        private static string SinhMaPhieuTraHangNhap(SqlConnection conn, SqlTransaction trans)
        {
            string sqlMax = trans != null
                ? "SELECT ISNULL(MAX(TRY_CAST(SUBSTRING(MaPhieu, 4, 20) AS BIGINT)), 0) FROM PhieuTraHangNhap WITH (UPDLOCK, HOLDLOCK) WHERE MaPhieu LIKE 'THN[0-9]%'"
                : "SELECT ISNULL(MAX(TRY_CAST(SUBSTRING(MaPhieu, 4, 20) AS BIGINT)), 0) FROM PhieuTraHangNhap WHERE MaPhieu LIKE 'THN[0-9]%'";
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

            colSoLuongTra.DefaultCellStyle.BackColor = Color.FromArgb(254, 252, 232);
            colSoLuongTra.ToolTipText = "Nhập số lượng trả trực tiếp tại đây (nhấn Enter để xác nhận)";

            dgvHangTra.EditMode = DataGridViewEditMode.EditOnEnter;
            dgvHangTra.EditingControlShowing += dgvHangTra_EditingControlShowing;
            dgvHangTra.CellEndEdit += dgvHangTra_CellEndEdit;
            dgvHangTra.Leave += (s, e) => { if (dgvHangTra.IsCurrentCellInEditMode) dgvHangTra.EndEdit(); };

            dgvHangTra.DataSource = dtHangTra;
        }

        private void frmThemPhieuTraHang_Load(object sender, EventArgs e)
        {
            maPhieuTuSinh = SinhMaPhieuTuDong();
            txtMaPhieu.Text = maPhieuTuSinh;
            dtpNgayTra.Value = DateTime.Now;

            NapDanhSachKho();
            NapDanhSachNhaCungCap();
            NapDanhSachNhanVien();
            NapDanhSachHangHoa();

            cboSoDongMoiTrang.SelectedItem = "20";
            CapNhatPhanTrang();
            TinhToanTongKet();
        }

        private void frmThemPhieuTraHang_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                btnBoQua.PerformClick();
            }
        }

        private string SinhMaPhieuTuDong()
        {
            try
            {
                using (SqlConnection conn = DataProcessing.OpenConnect())
                {
                    return SinhMaPhieuTraHangNhap(conn, null);
                }
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
                if (SessionContext.MaKhoHienTai > 0)
                {
                    string sql = "SELECT MaKho, TenKho FROM Kho WHERE MaKho = @MaKho";
                    DataTable dt = DataProcessing.ReadData(sql, new SqlParameter[] { new SqlParameter("@MaKho", SessionContext.MaKhoHienTai) });
                    cboKho.DisplayMember = "TenKho";
                    cboKho.ValueMember = "MaKho";
                    cboKho.DataSource = dt;
                    cboKho.SelectedValue = SessionContext.MaKhoHienTai;
                    cboKho.Enabled = false;
                }
                else
                {
                    string sql = "SELECT MaKho, TenKho FROM Kho WHERE TrangThai = 'HOAT_DONG' OR TrangThai = 'ACTIVE' ORDER BY TenKho ASC";
                    DataTable dt = DataProcessing.ReadData(sql);
                    cboKho.DisplayMember = "TenKho";
                    cboKho.ValueMember = "MaKho";
                    cboKho.DataSource = dt;
                    cboKho.Enabled = true;
                    if (dt.Rows.Count > 0)
                    {
                        cboKho.SelectedIndex = 0;
                    }
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
                string sql = "SELECT MaDonViCapHang, TenDonVi FROM DonViCapHang WHERE TrangThai = 'HOAT_DONG' OR TrangThai = 'ACTIVE' OR TrangThai IS NULL ORDER BY TenDonVi ASC";
                DataTable dt = DataProcessing.ReadData(sql);
                ComboboxHelper.CaiDatTimKiem(cboNhaCungCap, dt, "TenDonVi", "MaDonViCapHang");
                if (dt.Rows.Count > 0)
                {
                    cboNhaCungCap.SelectedIndex = 0;
                }
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
                // Giá trị chọn là NhanVien.MaNV (khóa ngoại MaNvtra), chỉ lấy nhân viên đang làm có tài khoản hoạt động
                string sql = @"
                    SELECT
                        nv.MaNV AS MaNhanVien,
                        nv.HoTen
                    FROM NhanVien nv
                    WHERE nv.TrangThai = 'DANG_LAM'
                      AND EXISTS (SELECT 1 FROM TaiKhoan tk WHERE tk.MaNV = nv.MaNV AND tk.TrangThai = 'HOAT_DONG')
                    ORDER BY nv.HoTen ASC";
                DataTable dt = DataProcessing.ReadData(sql);
                ComboboxHelper.CaiDatTimKiem(cboNguoiTra, dt, "HoTen", "MaNhanVien");

                if (SessionContext.MaNV.HasValue && SessionContext.MaNV.Value > 0)
                {
                    cboNguoiTra.SelectedValue = SessionContext.MaNV.Value;
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
                if (cboKho.SelectedValue is DataRowView drv && drv["MaKho"] != DBNull.Value)
                {
                    maKho = Convert.ToInt32(drv["MaKho"]);
                }
                else if (cboKho.SelectedValue != null && int.TryParse(cboKho.SelectedValue.ToString(), out int k))
                {
                    maKho = k;
                }

                string sql = @"
                    SELECT 
                        h.MaHang,
                        h.MaHangNoiBo,
                        h.TenHang,
                        N'[' + h.MaHangNoiBo + N'] ' + h.TenHang + N' (Tồn: ' + CAST(CAST(ISNULL(tk.SoLuongTon, 0) AS INT) AS NVARCHAR(20)) + N' ' + ISNULL(dvt.TenDVT, N'') + N')' AS HienThi,
                        ISNULL(dvt.TenDVT, N'Cái') AS TenDVT,
                        ISNULL(h.GiaVon, 0) AS GiaNhap,
                        ISNULL(tk.SoLuongTon, 0) AS TonKho
                    FROM HangHoa h
                    LEFT JOIN DonViTinh dvt ON h.MaDVT = dvt.MaDVT
                    LEFT JOIN vw_TonKhoHienTai tk ON h.MaHang = tk.MaHang AND tk.MaKho = @MaKho
                    WHERE ISNULL(h.TrangThai, '') NOT IN ('NGUNG_BAN', 'NGUNG_KINH_DOANH', 'DA_XOA')
                    ORDER BY h.TenHang ASC";

                SqlParameter[] p = { new SqlParameter("@MaKho", maKho) };
                dtDanhSachHang = DataProcessing.ReadData(sql, p);

                ComboboxHelper.CaiDatTimKiem(cboHangHoa, dtDanhSachHang, "HienThi", "MaHang");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi nạp danh sách hàng hóa: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void cboKho_SelectedIndexChanged(object sender, EventArgs e)
        {
            NapDanhSachHangHoa();
            CapNhatTonKhoTheoKhoMoi();
        }

        private void CapNhatTonKhoTheoKhoMoi()
        {
            if (dtHangTra == null || dtHangTra.Rows.Count == 0 || dtDanhSachHang == null) return;

            foreach (DataRow rTra in dtHangTra.Rows)
            {
                if (rTra.RowState == DataRowState.Deleted) continue;
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

        private void btnThemHang_Click(object sender, EventArgs e)
        {
            try
            {
                // 1. Kiểm tra cột Số lượng trả có đang hiển thị không
                if (!dgvHangTra.Columns.Contains("colSoLuongTra") || !dgvHangTra.Columns["colSoLuongTra"].Visible)
                {
                    MessageBox.Show(
                        "Cột dữ liệu 'Số lượng trả' hiện đang không hiển thị trên danh sách hàng trả.\n\nVui lòng vào mục 'Hiện cột' và tích chọn hiển thị lại cột 'Số lượng trả' trước khi thêm hàng!",
                        "Thông báo",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning);
                    return;
                }

                if (dgvHangTra.IsCurrentCellInEditMode)
                {
                    ApDungSoLuongNhap();
                }

                if (cboHangHoa.SelectedValue == null || !int.TryParse(cboHangHoa.SelectedValue.ToString(), out int maHang) || maHang <= 0)
                {
                    MessageBox.Show("Vui lòng chọn một mặt hàng!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (dtDanhSachHang == null) return;
                DataRow[] rows = dtDanhSachHang.Select($"MaHang = {maHang}");
                if (rows.Length == 0) return;

                DataRow rowItem = rows[0];
                string sku = rowItem["MaHangNoiBo"] != DBNull.Value ? rowItem["MaHangNoiBo"].ToString() : "";
                string ten = rowItem["TenHang"].ToString();
                string dvt = rowItem["TenDVT"].ToString();
                decimal tonKho = Convert.ToDecimal(rowItem["TonKho"]);
                decimal donGia = Convert.ToDecimal(rowItem["GiaNhap"]);

                if (tonKho <= 0)
                {
                    MessageBox.Show("Mặt hàng này hiện không còn tồn trong kho xuất trả (Tồn: 0)!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                DataRow[] daCo = dtHangTra.Select($"MaHang = {maHang}");
                if (daCo.Length > 0)
                {
                    int sttCuaHang = Convert.ToInt32(daCo[0]["STT"]);
                    int pageChuaHang = (int)Math.Ceiling((double)sttCuaHang / soDongMoiTrang);
                    CapNhatPhanTrang(pageChuaHang);

                    decimal slHienCo = Convert.ToDecimal(daCo[0]["SoLuongTra"]);
                    if (slHienCo < tonKho)
                    {
                        slHienCo += 1;
                        daCo[0]["SoLuongTra"] = slHienCo;
                        daCo[0]["ThanhTien"] = slHienCo * donGia;
                        TinhToanTongKet();
                    }
                    else
                    {
                        MessageBox.Show(string.Format("Số lượng trả mặt hàng '{0}' đã đạt mức tồn tối đa ({1:N0})!", ten, tonKho), "Cảnh báo vượt tồn", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }

                    dgvHangTra.Focus();
                    foreach (DataGridViewRow dgvRow in dgvHangTra.Rows)
                    {
                        if (dgvRow.Cells["colMaHangNoiBo"].Value?.ToString() == sku)
                        {
                            if (dgvHangTra.Columns.Contains("colSoLuongTra") && dgvHangTra.Columns["colSoLuongTra"].Visible)
                            {
                                try
                                {
                                    dgvHangTra.CurrentCell = dgvRow.Cells["colSoLuongTra"];
                                    dgvRow.Selected = true;
                                    dgvHangTra.BeginEdit(true);
                                    if (dgvHangTra.EditingControl is TextBox txtBox)
                                    {
                                        txtBox.SelectAll();
                                    }
                                }
                                catch { }
                            }
                            break;
                        }
                    }
                }
                else
                {
                    DataRow rMoi = dtHangTra.NewRow();
                    rMoi["STT"] = dtHangTra.Rows.Count + 1;
                    rMoi["MaHang"] = maHang;
                    rMoi["MaHangNoiBo"] = sku;
                    rMoi["TenHang"] = ten;
                    rMoi["TenDVT"] = dvt;
                    rMoi["TonKho"] = tonKho;
                    rMoi["SoLuongTra"] = 1;
                    rMoi["DonGiaTra"] = donGia;
                    rMoi["ThanhTien"] = donGia;

                    dtHangTra.Rows.Add(rMoi);
                    TinhToanTongKet();

                    int targetPage = (int)Math.Ceiling((double)dtHangTra.Rows.Count / soDongMoiTrang);
                    CapNhatPhanTrang(targetPage);

                    dgvHangTra.Focus();
                    foreach (DataGridViewRow dgvRow in dgvHangTra.Rows)
                    {
                        if (dgvRow.Cells["colMaHangNoiBo"].Value?.ToString() == sku)
                        {
                            if (dgvHangTra.Columns.Contains("colSoLuongTra") && dgvHangTra.Columns["colSoLuongTra"].Visible)
                            {
                                try
                                {
                                    dgvHangTra.CurrentCell = dgvRow.Cells["colSoLuongTra"];
                                    dgvRow.Selected = true;
                                    dgvHangTra.BeginEdit(true);
                                    if (dgvHangTra.EditingControl is TextBox txtBox)
                                    {
                                        txtBox.SelectAll();
                                    }
                                }
                                catch { }
                            }
                            break;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Không thể thực hiện thêm hàng: " + ex.Message, "Thông báo lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void dgvHangTra_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0 || e.RowIndex >= dgvHangTra.Rows.Count) return;

            string tenHang = dgvHangTra.Columns.Contains("colTenHang") ? dgvHangTra.Rows[e.RowIndex].Cells["colTenHang"].Value?.ToString() : "";
            string maHang = dgvHangTra.Columns.Contains("colMaHangNoiBo") ? dgvHangTra.Rows[e.RowIndex].Cells["colMaHangNoiBo"].Value?.ToString() : "";
            string tenHienThi = !string.IsNullOrWhiteSpace(tenHang) ? $"'{tenHang}' ({maHang})" : "mặt hàng này";

            DialogResult dr = MessageBox.Show(
                $"Bạn có chắc chắn muốn xóa {tenHienThi} khỏi phiếu trả hàng nhập?",
                "Xác nhận xóa",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            if (dr == DialogResult.Yes)
            {
                if (dgvHangTra.IsCurrentCellInEditMode)
                {
                    dgvHangTra.CancelEdit();
                }

                if (dgvHangTra.Rows[e.RowIndex].DataBoundItem is DataRowView drv)
                {
                    drv.Row.Delete();
                    dtHangTra.AcceptChanges();
                }
                else if (e.RowIndex < dtHangTra.Rows.Count)
                {
                    dtHangTra.Rows.RemoveAt(e.RowIndex);
                }

                DanhSoLaiStt();
                TinhToanTongKet();
                CapNhatPhanTrang();
            }
        }

        private void DanhSoLaiStt()
        {
            for (int i = 0; i < dtHangTra.Rows.Count; i++)
            {
                dtHangTra.Rows[i]["STT"] = i + 1;
            }
        }

        private void ApDungSoLuongNhap()
        {
            try
            {
                if (dgvHangTra.CurrentCell == null) return;
                int rowIndex = dgvHangTra.CurrentCell.RowIndex;
                if (rowIndex < 0 || rowIndex >= dgvHangTra.Rows.Count) return;

                DataGridViewRow dgvRow = dgvHangTra.Rows[rowIndex];
                DataRowView drv = dgvRow.DataBoundItem as DataRowView;
                DataRow row = drv != null ? drv.Row : (rowIndex < dtHangTra.Rows.Count ? dtHangTra.Rows[rowIndex] : null);
                if (row == null) return;

                string colName = dgvHangTra.Columns[dgvHangTra.CurrentCell.ColumnIndex].Name;

                // Lấy chuỗi từ ô nhập
                string text = "";
                if (dgvHangTra.EditingControl is TextBox txt)
                {
                    text = txt.Text;
                }
                else if (dgvHangTra.Columns.Contains(colName) && dgvRow.Cells[colName].Value != null)
                {
                    text = dgvRow.Cells[colName].Value.ToString();
                }

                decimal tonKho = row["TonKho"] != DBNull.Value ? Convert.ToDecimal(row["TonKho"]) : 0;
                decimal soLuongTra = row["SoLuongTra"] != DBNull.Value ? Convert.ToDecimal(row["SoLuongTra"]) : 1;
                decimal donGiaTra = row["DonGiaTra"] != DBNull.Value ? Convert.ToDecimal(row["DonGiaTra"]) : 0;

                string cleanStr = text.Replace(",", "").Replace(".", "").Trim();

                if (colName == "colSoLuongTra")
                {
                    if (string.IsNullOrEmpty(cleanStr) || !decimal.TryParse(cleanStr, out soLuongTra) || soLuongTra <= 0)
                    {
                        MessageBox.Show("Số lượng trả phải là số nguyên lớn hơn 0!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        soLuongTra = 1;
                    }
                    else if (tonKho > 0 && soLuongTra > tonKho)
                    {
                        MessageBox.Show(string.Format("Số lượng trả ({0:N0}) không được vượt quá số lượng tồn kho ({1:N0})!", soLuongTra, tonKho), "Cảnh báo vượt tồn", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        soLuongTra = tonKho;
                    }
                    soLuongTra = (int)Math.Floor(soLuongTra);
                }
                else if (colName == "colDonGiaTra")
                {
                    if (string.IsNullOrEmpty(cleanStr) || !decimal.TryParse(cleanStr, out donGiaTra) || donGiaTra < 0)
                    {
                        MessageBox.Show("Đơn giá trả không hợp lệ!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        donGiaTra = 0;
                    }
                }

                decimal thanhTien = soLuongTra * donGiaTra;

                if (dgvHangTra.IsCurrentCellInEditMode)
                {
                    dgvHangTra.EndEdit();
                }

                if (drv != null)
                {
                    drv["SoLuongTra"] = soLuongTra;
                    drv["DonGiaTra"] = donGiaTra;
                    drv["ThanhTien"] = thanhTien;
                    drv.EndEdit();
                }
                row["SoLuongTra"] = soLuongTra;
                row["DonGiaTra"] = donGiaTra;
                row["ThanhTien"] = thanhTien;

                if (dgvHangTra.Columns.Contains("colSoLuongTra"))
                {
                    dgvRow.Cells["colSoLuongTra"].Value = soLuongTra;
                }
                if (dgvHangTra.Columns.Contains("colDonGiaTra"))
                {
                    dgvRow.Cells["colDonGiaTra"].Value = donGiaTra;
                }
                if (dgvHangTra.Columns.Contains("colThanhTien"))
                {
                    dgvRow.Cells["colThanhTien"].Value = thanhTien;
                }

                this.BindingContext[dtHangTra]?.EndCurrentEdit();
                TinhToanTongKet();
                dgvHangTra.InvalidateRow(rowIndex);
            }
            catch { }
        }

        private void dgvHangTra_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            if (isCalculating || e.RowIndex < 0 || e.RowIndex >= dgvHangTra.Rows.Count) return;
            if (e.ColumnIndex < 0 || e.ColumnIndex >= dgvHangTra.Columns.Count) return;

            string colName = dgvHangTra.Columns[e.ColumnIndex].Name;
            if (colName == "colSoLuongTra" || colName == "colDonGiaTra")
            {
                try
                {
                    isCalculating = true;
                    ApDungSoLuongNhap();
                }
                finally
                {
                    isCalculating = false;
                }
            }
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (keyData == Keys.Enter && dgvHangTra.IsCurrentCellInEditMode)
            {
                string colName = (dgvHangTra.CurrentCell != null && dgvHangTra.CurrentCell.ColumnIndex >= 0 && dgvHangTra.CurrentCell.ColumnIndex < dgvHangTra.Columns.Count)
                    ? dgvHangTra.Columns[dgvHangTra.CurrentCell.ColumnIndex].Name : "";
                if (colName == "colSoLuongTra" || colName == "colDonGiaTra")
                {
                    ApDungSoLuongNhap();
                    return true;
                }
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }

        private void dgvHangTra_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            if (e.Control is TextBox txt)
            {
                txt.KeyPress -= TxtNumberDgv_KeyPress;
                txt.KeyDown -= TxtNumberDgv_KeyDown;

                string colName = (dgvHangTra.CurrentCell != null && dgvHangTra.CurrentCell.ColumnIndex >= 0 && dgvHangTra.CurrentCell.ColumnIndex < dgvHangTra.Columns.Count)
                    ? dgvHangTra.Columns[dgvHangTra.CurrentCell.ColumnIndex].Name : "";
                if (colName == "colSoLuongTra" || colName == "colDonGiaTra")
                {
                    txt.KeyPress += TxtNumberDgv_KeyPress;
                    txt.KeyDown += TxtNumberDgv_KeyDown;
                }
            }
        }

        private void TxtNumberDgv_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void TxtNumberDgv_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;
                ApDungSoLuongNhap();
            }
        }

        private void CapNhatPhanTrang(int pageToFocus = -1)
        {
            if (dtHangTra == null) return;

            if (dgvHangTra.IsCurrentCellInEditMode)
            {
                dgvHangTra.EndEdit();
            }

            int tongSoBanGhi = dtHangTra.Rows.Count;
            if (soDongMoiTrang <= 0) soDongMoiTrang = 20;

            tongSoTrang = (int)Math.Ceiling((double)tongSoBanGhi / soDongMoiTrang);
            if (tongSoTrang < 1) tongSoTrang = 1;

            if (pageToFocus > 0)
            {
                trangHienTai = Math.Min(pageToFocus, tongSoTrang);
            }
            else
            {
                if (trangHienTai > tongSoTrang) trangHienTai = tongSoTrang;
                if (trangHienTai < 1) trangHienTai = 1;
            }

            if (tongSoBanGhi == 0)
            {
                dtHangTra.DefaultView.RowFilter = "";
                lblTrangHienTai.Text = "Trang 1 / 1";
                btnTrangTruoc.Enabled = false;
                btnTrangSau.Enabled = false;
                return;
            }

            int minSTT = (trangHienTai - 1) * soDongMoiTrang + 1;
            int maxSTT = trangHienTai * soDongMoiTrang;

            dtHangTra.DefaultView.RowFilter = $"STT >= {minSTT} AND STT <= {maxSTT}";

            lblTrangHienTai.Text = $"Trang {trangHienTai} / {tongSoTrang}";
            btnTrangTruoc.Enabled = (trangHienTai > 1);
            btnTrangSau.Enabled = (trangHienTai < tongSoTrang);
        }

        private void btnTrangTruoc_Click(object sender, EventArgs e)
        {
            if (trangHienTai > 1)
            {
                trangHienTai--;
                CapNhatPhanTrang();
            }
        }

        private void btnTrangSau_Click(object sender, EventArgs e)
        {
            if (trangHienTai < tongSoTrang)
            {
                trangHienTai++;
                CapNhatPhanTrang();
            }
        }

        private void cboSoDongMoiTrang_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cboSoDongMoiTrang.SelectedItem != null && int.TryParse(cboSoDongMoiTrang.SelectedItem.ToString(), out int rows))
            {
                soDongMoiTrang = rows;
                trangHienTai = 1;
                CapNhatPhanTrang();
            }
        }

        private void TinhToanTongKet()
        {
            if (isCalculating) return;
            isCalculating = true;

            try
            {
                int soMatHang = dtHangTra.Rows.Count;
                decimal tongSl = 0;
                decimal tongTienHang = 0;

                if (dtHangTra != null)
                {
                    foreach (DataRow r in dtHangTra.Rows)
                    {
                        if (r.RowState == DataRowState.Deleted) continue;
                        tongSl += Convert.ToDecimal(r["SoLuongTra"]);
                        tongTienHang += Convert.ToDecimal(r["ThanhTien"]);
                    }
                }

                // Đọc tiền VND thống nhất (bỏ '.', ',' và khoảng trắng); giảm giá tối đa bằng tổng tiền hàng trả
                decimal giamGia = Math.Min(DocSoTienVnd(txtGiamGia.Text), tongTienHang);

                decimal canThuNcc = Math.Max(0, tongTienHang - giamGia);

                lblSoMatHangVal.Text = soMatHang.ToString("N0");
                lblTongSlTraVal.Text = tongSl.ToString("N0");
                lblTongTienTraVal.Text = string.Format("{0:N0} đ", tongTienHang);
                lblCanThuNccVal.Text = string.Format("{0:N0} đ", canThuNcc);

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

        private void btnHienCot_Click(object sender, EventArgs e)
        {
            foreach (Form openForm in Application.OpenForms)
            {
                if (openForm is frmHienCotHangTra fOpen)
                {
                    fOpen.Activate();
                    return;
                }
            }

            frmHienCotHangTra frm = new frmHienCotHangTra(dgvHangTra);
            Point btnPos = btnHienCot.PointToScreen(Point.Empty);
            int posX = btnPos.X + btnHienCot.Width - frm.Width;
            int posY = btnPos.Y + btnHienCot.Height + 2;

            Rectangle workingArea = Screen.FromControl(btnHienCot).WorkingArea;
            if (posX < workingArea.Left) posX = workingArea.Left;
            if (posX + frm.Width > workingArea.Right) posX = workingArea.Right - frm.Width;
            if (posY + frm.Height > workingArea.Bottom) posY = btnPos.Y - frm.Height - 2;

            frm.StartPosition = FormStartPosition.Manual;
            frm.Location = new Point(posX, posY);
            frm.Show(this);
        }

        private void btnNhapFile_Click(object sender, EventArgs e)
        {
            if (!dgvHangTra.Columns.Contains("colSoLuongTra") || !dgvHangTra.Columns["colSoLuongTra"].Visible)
            {
                MessageBox.Show(
                    "Cột dữ liệu 'Số lượng trả' hiện đang không hiển thị trên danh sách hàng trả.\n\nVui lòng vào mục 'Hiện cột' và tích chọn hiển thị lại cột 'Số lượng trả' trước khi nhập file!",
                    "Thông báo",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
                return;
            }

            int maKhoXuat = 0;
            if (cboKho.SelectedValue is DataRowView drv)
            {
                maKhoXuat = Convert.ToInt32(drv["MaKho"]);
            }
            else if (cboKho.SelectedValue != null && int.TryParse(cboKho.SelectedValue.ToString(), out int k))
            {
                maKhoXuat = k;
            }

            using (frmNhapHangTraFile frm = new frmNhapHangTraFile(maKhoXuat))
            {
                if (frm.ShowDialog(this) == DialogResult.OK && frm.DtKetQuaNhap != null && frm.DtKetQuaNhap.Rows.Count > 0)
                {
                    int demThemMoi = 0;
                    int demCapNhat = 0;
                    int demBoQua = 0;

                    foreach (DataRow r in frm.DtKetQuaNhap.Rows)
                    {
                        int maH = Convert.ToInt32(r["MaHang"]);
                        decimal sl = Convert.ToDecimal(r["SoLuongTra"]);
                        decimal dg = Convert.ToDecimal(r["DonGiaTra"]);
                        decimal ton = Convert.ToDecimal(r["TonKho"]);

                        DataRow[] daCo = dtHangTra.Select($"MaHang = {maH}");
                        if (daCo.Length > 0 && !frm.CapNhatKhiTrung)
                        {
                            // Người dùng chọn "Bỏ qua": giữ nguyên dòng đã có trong phiếu
                            demBoQua++;
                        }
                        else if (daCo.Length > 0)
                        {
                            decimal slMoi = Convert.ToDecimal(daCo[0]["SoLuongTra"]) + sl;
                            if (ton > 0 && slMoi > ton) slMoi = ton;
                            decimal dgApDung = dg > 0 ? dg : Convert.ToDecimal(daCo[0]["DonGiaTra"]);

                            daCo[0]["SoLuongTra"] = slMoi;
                            daCo[0]["DonGiaTra"] = dgApDung;
                            daCo[0]["ThanhTien"] = slMoi * dgApDung;
                            demCapNhat++;
                        }
                        else
                        {
                            if (ton > 0 && sl > ton) sl = ton;
                            if (sl > 0)
                            {
                                DataRow nr = dtHangTra.NewRow();
                                nr["STT"] = dtHangTra.Rows.Count + 1;
                                nr["MaHang"] = maH;
                                nr["MaHangNoiBo"] = r["MaHangNoiBo"];
                                nr["TenHang"] = r["TenHang"];
                                nr["TenDVT"] = r["TenDVT"];
                                nr["TonKho"] = ton;
                                nr["SoLuongTra"] = sl;
                                nr["DonGiaTra"] = dg;
                                nr["ThanhTien"] = sl * dg;

                                dtHangTra.Rows.Add(nr);
                                demThemMoi++;
                            }
                        }
                    }

                    TinhToanTongKet();
                    CapNhatPhanTrang();

                    if (demBoQua > 0)
                    {
                        MessageBox.Show($"Đã thêm {demThemMoi:N0} mặt hàng mới, cộng dồn {demCapNhat:N0} mặt hàng.\nBỏ qua {demBoQua:N0} mặt hàng đã có sẵn trong phiếu (giữ nguyên số lượng hiện tại).", "Kết quả nhập file", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }
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
            if (dgvHangTra.IsCurrentCellInEditMode)
            {
                ApDungSoLuongNhap();
            }
            this.BindingContext[dtHangTra]?.EndCurrentEdit();

            string maPhieu = txtMaPhieu.Text.Trim();
            if (string.IsNullOrEmpty(maPhieu))
            {
                MessageBox.Show("Mã phiếu không được để trống!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtMaPhieu.Focus();
                return;
            }

            // Người lập phiếu là nhân viên gắn với tài khoản đăng nhập (khóa ngoại NhanVien.MaNV, không dùng MaTaiKhoan)
            if (!SessionContext.MaNV.HasValue || SessionContext.MaNV.Value <= 0)
            {
                MessageBox.Show("Tài khoản chưa được gắn với nhân viên, không thể lập chứng từ.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Mã phiếu nhập tay bị trùng -> báo ngay; mã tự sinh bị trùng -> sẽ tự sinh lại trong giao dịch
            bool laMaTuSinh = string.Equals(maPhieu, maPhieuTuSinh, StringComparison.OrdinalIgnoreCase);
            if (!laMaTuSinh)
            {
                string checkSql = "SELECT COUNT(1) FROM PhieuTraHangNhap WHERE MaPhieu = @MaPhieu";
                object exist = DataProcessing.ReadValue(checkSql, new SqlParameter[] { new SqlParameter("@MaPhieu", maPhieu) });
                if (Convert.ToInt32(exist) > 0)
                {
                    MessageBox.Show(string.Format("Mã phiếu '{0}' đã tồn tại trong hệ thống! Vui lòng chọn mã khác.", maPhieu), "Trùng mã", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtMaPhieu.Focus();
                    return;
                }
            }

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
                if (r.RowState == DataRowState.Deleted) continue;
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

            // Kiểm tra giới hạn giảm giá / tiền NCC đã trả (cảnh báo và điều chỉnh nếu vượt)
            if (!KiemTraGioiHanTien(true))
            {
                TinhToanTongKet();
                return;
            }

            decimal giamGia = DocSoTienVnd(txtGiamGia.Text);

            decimal canThuNcc = Math.Max(0, tongTienHang - giamGia);

            decimal daThuNcc = DocSoTienVnd(txtDaThuNcc.Text);

            if (cboNguoiTra.SelectedValue == null || !int.TryParse(cboNguoiTra.SelectedValue.ToString(), out int maNvTra) || maNvTra <= 0)
            {
                MessageBox.Show("Vui lòng chọn nhân viên thực hiện trả hàng!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                cboNguoiTra.Focus();
                return;
            }

            int maKho = Convert.ToInt32(cboKho.SelectedValue);
            int maNcc = Convert.ToInt32(cboNhaCungCap.SelectedValue);
            int maNvLap = SessionContext.MaNV.Value;
            DateTime ngayTra = dtpNgayTra.Value;
            string ghiChu = txtGhiChu.Text.Trim();

            string msgXacNhan = trangThai == "NHAP"
                ? "Bạn có muốn lưu tạm phiếu trả hàng nhập này không?"
                : "Bạn có chắc chắn muốn hoàn tất trả hàng nhập không?\nSố lượng hàng hóa sẽ được ghi nhận xuất giảm tồn kho ngay sau khi xác nhận.";

            DialogResult dr = MessageBox.Show(msgXacNhan, "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dr != DialogResult.Yes) return;

            string maPhieuDaLuu = maPhieu;
            try
            {
                DataProcessing.ChangeDataTransaction((conn, trans) =>
                {
                    // 0.1. Kiểm tra lại mã phiếu ngay trong giao dịch (chống trùng khi nhiều người lưu cùng lúc)
                    using (SqlCommand cmdDem = new SqlCommand("SELECT COUNT(1) FROM PhieuTraHangNhap WITH (UPDLOCK, HOLDLOCK) WHERE MaPhieu = @MaPhieu", conn, trans))
                    {
                        cmdDem.Parameters.AddWithValue("@MaPhieu", maPhieu);
                        if (Convert.ToInt32(cmdDem.ExecuteScalar()) > 0)
                        {
                            if (!laMaTuSinh)
                            {
                                throw new InvalidOperationException($"Mã phiếu '{maPhieu}' đã tồn tại trong hệ thống! Vui lòng chọn mã khác.");
                            }
                            maPhieuDaLuu = SinhMaPhieuTraHangNhap(conn, trans);
                        }
                    }

                    // 0.2. Hoàn thành trả hàng: kiểm tra tồn kho ngay trong giao dịch (chặn âm tồn)
                    if (trangThai == "DA_TRA_HANG")
                    {
                        foreach (DataRow r in dtHangTra.Rows)
                        {
                            if (r.RowState == DataRowState.Deleted) continue;
                            int maHangKt = Convert.ToInt32(r["MaHang"]);
                            decimal slTraKt = 0;
                            foreach (DataRow r2 in dtHangTra.Rows)
                            {
                                if (r2.RowState != DataRowState.Deleted && Convert.ToInt32(r2["MaHang"]) == maHangKt)
                                    slTraKt += Convert.ToDecimal(r2["SoLuongTra"]);
                            }

                            using (SqlCommand cmdTon = new SqlCommand("SELECT ISNULL(SUM(SoLuongTon), 0) FROM vw_TonKhoHienTai WHERE MaKho = @MaKho AND MaHang = @MaHang", conn, trans))
                            {
                                cmdTon.Parameters.AddWithValue("@MaKho", maKho);
                                cmdTon.Parameters.AddWithValue("@MaHang", maHangKt);
                                decimal tonHienTai = Convert.ToDecimal(cmdTon.ExecuteScalar());
                                if (slTraKt > tonHienTai)
                                {
                                    throw new InvalidOperationException($"Mặt hàng [{r["TenHang"]}] có số lượng trả ({slTraKt:N0}) vượt quá tồn kho thực tế hiện tại ({tonHienTai:N0})!");
                                }
                            }
                        }
                    }

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
                        cmdHeader.Parameters.AddWithValue("@MaPhieu", maPhieuDaLuu);
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
                        if (r.RowState == DataRowState.Deleted) continue;
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
                            cmdLog.Parameters.AddWithValue("@MaTaiKhoan", SessionContext.MaTaiKhoan > 0 ? SessionContext.MaTaiKhoan : 1);
                            cmdLog.Parameters.AddWithValue("@ThaoTac", trangThai == "NHAP" ? "THEM_TAM" : "HOAN_THANH");
                            cmdLog.Parameters.AddWithValue("@MaDoiTuong", maPhieuDaLuu);
                            cmdLog.Parameters.AddWithValue("@NoiDung", $"Lập phiếu trả hàng {maPhieuDaLuu} ({trangThai})");
                            cmdLog.ExecuteNonQuery();
                        }
                    }
                    catch { }
                });

                string msgDone = trangThai == "NHAP"
                    ? $"Đã lưu tạm phiếu trả hàng [{maPhieuDaLuu}] thành công!"
                    : $"Đã hoàn tất trả hàng nhập [{maPhieuDaLuu}] thành công!\nTồn kho đã được tự động cập nhật giảm.";
                if (!string.Equals(maPhieuDaLuu, maPhieu, StringComparison.OrdinalIgnoreCase))
                {
                    msgDone += $"\n(Mã '{maPhieu}' vừa được sử dụng bởi phiếu khác nên hệ thống đã cấp mã mới.)";
                }
                MessageBox.Show(msgDone, "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            catch (InvalidOperationException exNghiepVu)
            {
                // Vi phạm ràng buộc nghiệp vụ (trùng mã / âm tồn) -> giao dịch đã rollback
                MessageBox.Show(exNghiepVu.Message, "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            catch (SqlException exSql) when (exSql.Number == 2627 || exSql.Number == 2601)
            {
                MessageBox.Show("Mã phiếu trả hàng bị trùng do có người dùng khác vừa lưu phiếu cùng lúc.\nVui lòng bấm lưu lại hoặc đổi mã phiếu khác.", "Trùng mã phiếu", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi lưu phiếu trả hàng: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnBoQua_Click(object sender, EventArgs e)
        {
            if (dgvHangTra.IsCurrentCellInEditMode)
            {
                dgvHangTra.CancelEdit();
            }

            if (dtHangTra != null && dtHangTra.Rows.Count > 0)
            {
                DialogResult dr = MessageBox.Show(
                    "Dữ liệu phiếu trả hàng nhập chưa được lưu. Bạn có chắc chắn muốn bỏ qua?",
                    "Xác nhận bỏ qua",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question);

                if (dr != DialogResult.Yes) return;
            }

            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }
    }
}
