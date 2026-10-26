using BTL_LTTQ_2.Classes;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Windows.Forms;

namespace BTL_LTTQ_2.GUI.Forms.ChuyenKho
{
    public partial class frmThemPhieuChuyenKhoNew : Form
    {
        private DataTable dtHangChuyen;
        private DataTable dtDanhSachHang;
        private bool isCalculating = false;

        private int trangHienTai = 1;
        private int soDongMoiTrang = 20;
        private int tongSoTrang = 1;

        // Mã phiếu do hệ thống tự sinh (để tự cấp mã mới nếu bị trùng khi lưu)
        private string maPhieuTuSinh = "";

        // Kho gửi đang chọn trước đó (để khôi phục khi người dùng không đồng ý đổi kho)
        private int maKhoNguonDangChon = 0;
        private bool dangKhoiPhucKho = false;

        public bool DaThayDoiDuLieu { get; private set; } = false;

        public frmThemPhieuChuyenKhoNew()
        {
            InitializeComponent();
            KhoiTaoBangHangChuyen();
            CauHinhDataGridView();
        }

        private void KhoiTaoBangHangChuyen()
        {
            dtHangChuyen = new DataTable();
            dtHangChuyen.Columns.Add("STT", typeof(int));
            dtHangChuyen.Columns.Add("MaHang", typeof(int));
            dtHangChuyen.Columns.Add("MaHangNoiBo", typeof(string));
            dtHangChuyen.Columns.Add("TenHang", typeof(string));
            dtHangChuyen.Columns.Add("TenDVT", typeof(string));
            dtHangChuyen.Columns.Add("TonKho", typeof(decimal));
            dtHangChuyen.Columns.Add("SoLuong", typeof(decimal));
            dtHangChuyen.Columns.Add("DonGiaChuyen", typeof(decimal));
            dtHangChuyen.Columns.Add("ThanhTien", typeof(decimal));
        }

        private void CauHinhDataGridView()
        {
            try
            {
                typeof(DataGridView).InvokeMember("DoubleBuffered",
                    System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.SetProperty,
                    null, dgvHangChuyen, new object[] { true });
            }
            catch { }

            dgvHangChuyen.AutoGenerateColumns = false;
            dgvHangChuyen.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(248, 250, 252);
            dgvHangChuyen.DefaultCellStyle.SelectionBackColor = Color.FromArgb(224, 231, 255);
            dgvHangChuyen.DefaultCellStyle.SelectionForeColor = Color.FromArgb(30, 41, 59);

            dgvHangChuyen.ColumnHeadersDefaultCellStyle.WrapMode = DataGridViewTriState.False;
            dgvHangChuyen.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.EnableResizing;
            dgvHangChuyen.ColumnHeadersHeight = 36;

            colStt.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            colMaHangNoiBo.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            colDonViTinh.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            colDonViTinh.Visible = false;
            colSoLuong.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            colSoLuong.DefaultCellStyle.Format = "N0";
            colSoLuong.DefaultCellStyle.BackColor = Color.FromArgb(254, 252, 232);
            colSoLuong.ToolTipText = "Nhập số lượng chuyển trực tiếp tại đây";
            colDonGia.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            colDonGia.DefaultCellStyle.Format = "N0";
            colThanhTien.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            colThanhTien.DefaultCellStyle.Format = "N0";

            dgvHangChuyen.EditMode = DataGridViewEditMode.EditOnEnter;
            dgvHangChuyen.EditingControlShowing += dgvHangChuyen_EditingControlShowing;
            dgvHangChuyen.Leave += (s, e) =>
            {
                if (dgvHangChuyen.IsCurrentCellInEditMode)
                {
                    ApDungSoLuongNhap();
                }
            };

            dgvHangChuyen.DataSource = dtHangChuyen;
        }

        private void frmThemPhieuChuyenKhoNew_Load(object sender, EventArgs e)
        {
            txtMaPhieu.Text = SinhMaPhieuTuDong();
            maPhieuTuSinh = txtMaPhieu.Text;
            dtpNgayChuyen.Value = DateTime.Now;

            TaiDanhSachKho();
            TaiDanhSachNhanVien();
            TaiDanhSachHangHoa();

            if (cboSoDongMoiTrang != null)
            {
                if (cboSoDongMoiTrang.Items.Count == 0)
                {
                    cboSoDongMoiTrang.Items.AddRange(new object[] { "10", "20", "50", "100" });
                }
                cboSoDongMoiTrang.SelectedItem = "20";
                if (cboSoDongMoiTrang.SelectedIndex < 0 && cboSoDongMoiTrang.Items.Count > 0)
                {
                    cboSoDongMoiTrang.SelectedIndex = 1;
                }
            }

            CapNhatTongKet();
            CapNhatPhanTrang();
        }

        private string SinhMaPhieuTuDong()
        {
            try
            {
                using (SqlConnection conn = DataProcessing.OpenConnect())
                {
                    return SinhMaPhieuTuDong(conn, null);
                }
            }
            catch
            {
                return "CK" + DateTime.Now.ToString("yyMMddHHmm");
            }
        }

        /// <summary>
        /// Sinh mã phiếu CK kế tiếp dựa trên hậu tố số lớn nhất của các mã CK đã có (không dựa vào MAX(MaPhieuChuyen)),
        /// lặp cho tới khi mã chưa được sử dụng. Dùng được cả trong giao dịch (trans khác null).
        /// </summary>
        private static string SinhMaPhieuTuDong(SqlConnection conn, SqlTransaction trans)
        {
            string sqlMax = "SELECT ISNULL(MAX(TRY_CAST(SUBSTRING(MaPhieu, 3, 30) AS INT)), 0) FROM PhieuChuyenKho WHERE MaPhieu LIKE 'CK%'";
            int soLonNhat;
            using (SqlCommand cmd = new SqlCommand(sqlMax, conn, trans))
            {
                soLonNhat = Convert.ToInt32(cmd.ExecuteScalar());
            }

            string ma;
            do
            {
                soLonNhat++;
                ma = "CK" + soLonNhat.ToString("D6");
            }
            while (MaPhieuDaTonTai(conn, trans, ma));
            return ma;
        }

        private static bool MaPhieuDaTonTai(SqlConnection conn, SqlTransaction trans, string maPhieu)
        {
            // UPDLOCK + HOLDLOCK: giữ khóa khoảng mã trong giao dịch để chống 2 người lưu trùng mã cùng lúc
            string sql = "SELECT COUNT(1) FROM PhieuChuyenKho WITH (UPDLOCK, HOLDLOCK) WHERE MaPhieu = @MaPhieu";
            using (SqlCommand cmd = new SqlCommand(sql, conn, trans))
            {
                cmd.Parameters.Add("@MaPhieu", SqlDbType.VarChar, 30).Value = maPhieu;
                return Convert.ToInt32(cmd.ExecuteScalar()) > 0;
            }
        }

        private void TaiDanhSachKho()
        {
            try
            {
                string sql = "SELECT MaKho, TenKho FROM Kho WHERE TrangThai = 'HOAT_DONG' OR TrangThai = 'ACTIVE' OR TrangThai IS NULL ORDER BY TenKho ASC";
                if (SessionContext.MaKhoHienTai > 0)
                {
                    DataTable dtKhoNguon = DataProcessing.ReadData("SELECT MaKho, TenKho FROM Kho WHERE MaKho = @MaKho", new SqlParameter[] { new SqlParameter("@MaKho", SessionContext.MaKhoHienTai) });
                    cboKhoNguon.DisplayMember = "TenKho";
                    cboKhoNguon.ValueMember = "MaKho";
                    cboKhoNguon.DataSource = dtKhoNguon;
                    cboKhoNguon.SelectedValue = SessionContext.MaKhoHienTai;
                    cboKhoNguon.Enabled = false;
                    CapNhatDanhSachKhoDich();
                    return;
                }

                cboKhoNguon.Enabled = true;
                DataTable dtKhoAll = DataProcessing.ReadData(sql);
                cboKhoNguon.DisplayMember = "TenKho";
                cboKhoNguon.ValueMember = "MaKho";
                cboKhoNguon.DataSource = dtKhoAll;

                if (dtKhoAll.Rows.Count > 0)
                {
                    cboKhoNguon.SelectedIndex = 0;
                }
                CapNhatDanhSachKhoDich();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi nạp danh sách kho: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void CapNhatDanhSachKhoDich()
        {
            if (cboKhoNguon.SelectedValue == null) return;
            int maKhoNguon = 0;
            if (cboKhoNguon.SelectedValue is DataRowView drv && drv["MaKho"] != DBNull.Value)
            {
                maKhoNguon = Convert.ToInt32(drv["MaKho"]);
            }
            else if (int.TryParse(cboKhoNguon.SelectedValue.ToString(), out int k))
            {
                maKhoNguon = k;
            }

            if (maKhoNguon <= 0) return;

            string sqlKhoDich = "SELECT MaKho, TenKho FROM Kho WHERE (TrangThai = 'HOAT_DONG' OR TrangThai = 'ACTIVE' OR TrangThai IS NULL) AND MaKho <> @MaKhoNguon ORDER BY TenKho ASC";
            DataTable dtKD = DataProcessing.ReadData(sqlKhoDich, new SqlParameter[] {
                new SqlParameter("@MaKhoNguon", maKhoNguon)
            });

            cboKhoDich.DisplayMember = "TenKho";
            cboKhoDich.ValueMember = "MaKho";
            cboKhoDich.DataSource = dtKD;
            if (dtKD.Rows.Count > 0)
            {
                cboKhoDich.SelectedIndex = 0;
            }
        }

        private void cboKhoNguon_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Đang khôi phục lựa chọn cũ: không xử lý lại
            if (dangKhoiPhucKho) return;
            if (cboKhoNguon.SelectedValue == null) return;

            int maKhoMoi = 0;
            if (cboKhoNguon.SelectedValue is DataRowView drv && drv["MaKho"] != DBNull.Value)
            {
                maKhoMoi = Convert.ToInt32(drv["MaKho"]);
            }
            else if (int.TryParse(cboKhoNguon.SelectedValue.ToString(), out int k))
            {
                maKhoMoi = k;
            }

            if (maKhoMoi > 0 && maKhoMoi != maKhoNguonDangChon)
            {
                if (dtHangChuyen != null && dtHangChuyen.Rows.Count > 0 && maKhoNguonDangChon > 0)
                {
                    DialogResult dr = MessageBox.Show(
                        "Đổi kho gửi sẽ làm mới danh sách hàng hóa đã chọn. Bạn có chắc chắn muốn đổi không?",
                        "Xác nhận đổi kho gửi",
                        MessageBoxButtons.YesNo,
                        MessageBoxIcon.Question);

                    if (dr == DialogResult.No)
                    {
                        // Khôi phục kho gửi đã chọn trước đó, không kích hoạt lại sự kiện
                        try
                        {
                            dangKhoiPhucKho = true;
                            cboKhoNguon.SelectedValue = maKhoNguonDangChon;
                        }
                        finally
                        {
                            dangKhoiPhucKho = false;
                        }
                        return;
                    }

                    dtHangChuyen.Clear();
                    CapNhatTongKet();
                    CapNhatPhanTrang();
                }

                maKhoNguonDangChon = maKhoMoi;
                CapNhatDanhSachKhoDich();
                TaiDanhSachHangHoa();
            }
        }

        private void TaiDanhSachNhanVien()
        {
            try
            {
                // Người chuyển lấy từ NhanVien đang làm (MaNVLap/MaNVXuat tham chiếu NhanVien.MaNV, không dùng MaTaiKhoan)
                string sql = @"
                    SELECT
                        nv.MaNV,
                        nv.HoTen
                    FROM NhanVien nv
                    WHERE nv.TrangThai = 'DANG_LAM'
                    ORDER BY nv.HoTen ASC";
                DataTable dt = DataProcessing.ReadData(sql);

                ComboboxHelper.CaiDatTimKiem(cboNguoiChuyen, dt, "HoTen", "MaNV");

                if (SessionContext.MaNV.HasValue && SessionContext.MaNV.Value > 0)
                {
                    cboNguoiChuyen.SelectedValue = SessionContext.MaNV.Value;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi nạp danh sách nhân viên: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void TaiDanhSachHangHoa()
        {
            try
            {
                int maKho = 1;
                if (cboKhoNguon.SelectedValue is DataRowView drv && drv["MaKho"] != DBNull.Value)
                {
                    maKho = Convert.ToInt32(drv["MaKho"]);
                }
                else if (cboKhoNguon.SelectedValue != null && int.TryParse(cboKhoNguon.SelectedValue.ToString(), out int k))
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
                        ISNULL(h.GiaVon, 0) AS DonGia,
                        ISNULL(tk.SoLuongTon, 0) AS TonKho
                    FROM HangHoa h
                    LEFT JOIN DonViTinh dvt ON h.MaDVT = dvt.MaDVT
                    LEFT JOIN vw_TonKhoHienTai tk ON h.MaHang = tk.MaHang AND tk.MaKho = @MaKho
                    WHERE ISNULL(h.TrangThai, '') NOT IN ('NGUNG_BAN', 'DA_XOA')
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

        private void btnThemHang_Click(object sender, EventArgs e)
        {
            try
            {
                // Kiểm tra xem cột Số lượng có đang hiển thị trên bảng hay không
                if (!dgvHangChuyen.Columns.Contains("colSoLuong") || !dgvHangChuyen.Columns["colSoLuong"].Visible)
                {
                    MessageBox.Show(
                        "Cột dữ liệu 'Số lượng' hiện đang không hiển thị trên danh sách hàng chuyển.\n\nVui lòng vào mục 'Hiện cột' và tích chọn hiển thị lại cột 'Số lượng' trước khi thêm hàng!",
                        "Thông báo",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning);
                    return;
                }

                if (dgvHangChuyen.IsCurrentCellInEditMode)
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

                DataRow rowHang = rows[0];
                decimal tonKho = Convert.ToDecimal(rowHang["TonKho"]);
                decimal donGia = Convert.ToDecimal(rowHang["DonGia"]);

                if (tonKho <= 0)
                {
                    MessageBox.Show("Mặt hàng này hiện không còn tồn trong kho gửi (Tồn: 0)!", "Cảnh báo hết hàng", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                DataRow[] daCo = dtHangChuyen.Select($"MaHang = {maHang}");
                if (daCo.Length > 0)
                {
                    int sttCuaHang = Convert.ToInt32(daCo[0]["STT"]);
                    int pageChuaHang = (int)Math.Ceiling((double)sttCuaHang / soDongMoiTrang);
                    CapNhatPhanTrang(pageChuaHang);

                    decimal slHienCo = Convert.ToDecimal(daCo[0]["SoLuong"]);
                    if (slHienCo < tonKho)
                    {
                        slHienCo += 1;
                        daCo[0]["SoLuong"] = slHienCo;
                        daCo[0]["ThanhTien"] = slHienCo * donGia;
                        CapNhatTongKet();
                    }
                    else
                    {
                        MessageBox.Show(string.Format("Số lượng chuyển mặt hàng '{0}' đã đạt mức tồn tối đa ({1:N0})!", rowHang["TenHang"], tonKho), "Cảnh báo vượt tồn", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }

                    dgvHangChuyen.Focus();
                    foreach (DataGridViewRow dgvRow in dgvHangChuyen.Rows)
                    {
                        if (dgvRow.Cells["colMaHangNoiBo"].Value?.ToString() == rowHang["MaHangNoiBo"]?.ToString())
                        {
                            if (dgvHangChuyen.Columns.Contains("colSoLuong") && dgvHangChuyen.Columns["colSoLuong"].Visible)
                            {
                                try
                                {
                                    dgvHangChuyen.CurrentCell = dgvRow.Cells["colSoLuong"];
                                    dgvRow.Selected = true;
                                    dgvHangChuyen.BeginEdit(true);
                                    if (dgvHangChuyen.EditingControl is TextBox txtBox)
                                    {
                                        txtBox.SelectAll();
                                    }
                                }
                                catch
                                {
                                    // Bỏ qua nếu không thể đặt cell hiện tại
                                }
                            }
                            break;
                        }
                    }
                }
                else
                {
                    DataRow rMoi = dtHangChuyen.NewRow();
                    rMoi["STT"] = dtHangChuyen.Rows.Count + 1;
                    rMoi["MaHang"] = maHang;
                    rMoi["MaHangNoiBo"] = rowHang["MaHangNoiBo"];
                    rMoi["TenHang"] = rowHang["TenHang"];
                    rMoi["TenDVT"] = rowHang["TenDVT"];
                    rMoi["TonKho"] = tonKho;
                    rMoi["SoLuong"] = 1;
                    rMoi["DonGiaChuyen"] = donGia;
                    rMoi["ThanhTien"] = donGia;

                    dtHangChuyen.Rows.Add(rMoi);
                    CapNhatTongKet();

                    int targetPage = (int)Math.Ceiling((double)dtHangChuyen.Rows.Count / soDongMoiTrang);
                    CapNhatPhanTrang(targetPage);

                    dgvHangChuyen.Focus();
                    foreach (DataGridViewRow dgvRow in dgvHangChuyen.Rows)
                    {
                        if (dgvRow.Cells["colMaHangNoiBo"].Value?.ToString() == rowHang["MaHangNoiBo"]?.ToString())
                        {
                            if (dgvHangChuyen.Columns.Contains("colSoLuong") && dgvHangChuyen.Columns["colSoLuong"].Visible)
                            {
                                try
                                {
                                    dgvHangChuyen.CurrentCell = dgvRow.Cells["colSoLuong"];
                                    dgvRow.Selected = true;
                                    dgvHangChuyen.BeginEdit(true);
                                    if (dgvHangChuyen.EditingControl is TextBox txtBox)
                                    {
                                        txtBox.SelectAll();
                                    }
                                }
                                catch
                                {
                                    // Bỏ qua nếu không thể đặt cell hiện tại
                                }
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

        private void dgvHangChuyen_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0 || e.RowIndex >= dgvHangChuyen.Rows.Count) return;

            string tenHang = dgvHangChuyen.Columns.Contains("colTenHang") ? dgvHangChuyen.Rows[e.RowIndex].Cells["colTenHang"].Value?.ToString() : "";
            string maHang = dgvHangChuyen.Columns.Contains("colMaHangNoiBo") ? dgvHangChuyen.Rows[e.RowIndex].Cells["colMaHangNoiBo"].Value?.ToString() : "";
            string tenHienThi = !string.IsNullOrWhiteSpace(tenHang) ? $"'{tenHang}' ({maHang})" : "mặt hàng này";

            DialogResult dr = MessageBox.Show(
                $"Bạn có chắc chắn muốn xóa {tenHienThi} khỏi phiếu chuyển kho?",
                "Xác nhận xóa",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            if (dr == DialogResult.Yes)
            {
                if (dgvHangChuyen.IsCurrentCellInEditMode)
                {
                    dgvHangChuyen.CancelEdit();
                }

                if (dgvHangChuyen.Rows[e.RowIndex].DataBoundItem is DataRowView drv)
                {
                    drv.Row.Delete();
                    dtHangChuyen.AcceptChanges();
                }
                else if (e.RowIndex < dtHangChuyen.Rows.Count)
                {
                    dtHangChuyen.Rows.RemoveAt(e.RowIndex);
                }

                DanhSoLaiStt();
                CapNhatTongKet();
                CapNhatPhanTrang();
            }
        }

        private void DanhSoLaiStt()
        {
            for (int i = 0; i < dtHangChuyen.Rows.Count; i++)
            {
                dtHangChuyen.Rows[i]["STT"] = i + 1;
            }
        }

        private void ApDungSoLuongNhap()
        {
            try
            {
                if (dgvHangChuyen.CurrentCell == null) return;
                int rowIndex = dgvHangChuyen.CurrentCell.RowIndex;
                if (rowIndex < 0 || rowIndex >= dgvHangChuyen.Rows.Count) return;

                DataGridViewRow dgvRow = dgvHangChuyen.Rows[rowIndex];
                DataRowView drv = dgvRow.DataBoundItem as DataRowView;
                DataRow row = drv != null ? drv.Row : (rowIndex < dtHangChuyen.Rows.Count ? dtHangChuyen.Rows[rowIndex] : null);
                if (row == null) return;

                // 1. Lấy chuỗi số lượng từ ô nhập liệu hoặc ô trên lưới
                string text = "";
                if (dgvHangChuyen.EditingControl is TextBox txt)
                {
                    text = txt.Text;
                }
                else if (dgvHangChuyen.Columns.Contains("colSoLuong") && dgvRow.Cells["colSoLuong"].Value != null)
                {
                    object giaTriO = dgvRow.Cells["colSoLuong"].Value;
                    // Giá trị decimal (vd 5.00) chuyển sang chuỗi số nguyên để không bị hiểu nhầm dấu thập phân
                    text = giaTriO is decimal d
                        ? Math.Floor(d).ToString("0", System.Globalization.CultureInfo.InvariantCulture)
                        : giaTriO.ToString();
                }

                decimal tonKho = row["TonKho"] != DBNull.Value ? Convert.ToDecimal(row["TonKho"]) : 0;
                decimal donGia = row["DonGiaChuyen"] != DBNull.Value ? Convert.ToDecimal(row["DonGiaChuyen"]) : 0;

                decimal slChuyen = 1;
                string cleanStr = text.Replace(",", "").Replace(".", "").Trim();
                if (string.IsNullOrEmpty(cleanStr) || !decimal.TryParse(cleanStr, out slChuyen) || slChuyen <= 0)
                {
                    MessageBox.Show("Số lượng chuyển phải là số nguyên lớn hơn 0!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    slChuyen = 1;
                }
                else if (tonKho > 0 && slChuyen > tonKho)
                {
                    MessageBox.Show(string.Format("Số lượng chuyển ({0:N0}) không được vượt quá số lượng tồn kho gửi ({1:N0})!", slChuyen, tonKho), "Cảnh báo vượt tồn", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    slChuyen = tonKho;
                }

                slChuyen = (int)Math.Floor(slChuyen);
                decimal thanhTien = slChuyen * donGia;

                // 2. Kết thúc edit trên DataGridView
                if (dgvHangChuyen.IsCurrentCellInEditMode)
                {
                    dgvHangChuyen.EndEdit();
                }

                // 3. Cập nhật dữ liệu vào DataRow / DataRowView
                if (drv != null)
                {
                    drv["SoLuong"] = slChuyen;
                    drv["ThanhTien"] = thanhTien;
                    drv.EndEdit();
                }
                row["SoLuong"] = slChuyen;
                row["ThanhTien"] = thanhTien;

                // 4. Cập nhật hiển thị lên các ô của lưới
                if (dgvHangChuyen.Columns.Contains("colSoLuong"))
                {
                    dgvRow.Cells["colSoLuong"].Value = slChuyen;
                }
                if (dgvHangChuyen.Columns.Contains("colThanhTien"))
                {
                    dgvRow.Cells["colThanhTien"].Value = thanhTien;
                }

                // 5. Kết thúc edit của BindingContext
                this.BindingContext[dtHangChuyen]?.EndCurrentEdit();

                // 6. Cập nhật các trường tổng kết (Số mặt hàng, Tổng SL, Tổng giá trị)
                CapNhatTongKet();

                // 7. Refresh lại dòng để hiển thị giá trị mới ngay lập tức
                dgvHangChuyen.InvalidateRow(rowIndex);
            }
            catch
            {
                // Bỏ qua tránh crash
            }
        }

        private void dgvHangChuyen_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            if (isCalculating || e.RowIndex < 0 || e.RowIndex >= dgvHangChuyen.Rows.Count) return;

            string colName = dgvHangChuyen.Columns[e.ColumnIndex].Name;
            if (colName == "colSoLuong")
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
            if (keyData == Keys.Enter && dgvHangChuyen.IsCurrentCellInEditMode)
            {
                string colName = dgvHangChuyen.CurrentCell != null ? dgvHangChuyen.Columns[dgvHangChuyen.CurrentCell.ColumnIndex].Name : "";
                if (colName == "colSoLuong")
                {
                    ApDungSoLuongNhap();
                    return true;
                }
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }

        private void dgvHangChuyen_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            if (e.Control is TextBox txt)
            {
                txt.KeyPress -= TxtSoLuongDgv_KeyPress;
                txt.KeyDown -= TxtSoLuongDgv_KeyDown;

                string colName = dgvHangChuyen.CurrentCell != null ? dgvHangChuyen.Columns[dgvHangChuyen.CurrentCell.ColumnIndex].Name : "";
                if (colName == "colSoLuong")
                {
                    txt.KeyPress += TxtSoLuongDgv_KeyPress;
                    txt.KeyDown += TxtSoLuongDgv_KeyDown;
                }
            }
        }

        private void TxtSoLuongDgv_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void TxtSoLuongDgv_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;
                ApDungSoLuongNhap();
            }
        }

        private void CapNhatPhanTrang(int pageToFocus = -1)
        {
            if (dtHangChuyen == null) return;

            if (dgvHangChuyen.IsCurrentCellInEditMode)
            {
                dgvHangChuyen.EndEdit();
            }

            int tongSoBanGhi = dtHangChuyen.Rows.Count;
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
                dtHangChuyen.DefaultView.RowFilter = "";
                lblTrangHienTai.Text = "Trang 1 / 1";
                btnTrangTruoc.Enabled = false;
                btnTrangSau.Enabled = false;
                return;
            }

            int minSTT = (trangHienTai - 1) * soDongMoiTrang + 1;
            int maxSTT = trangHienTai * soDongMoiTrang;

            dtHangChuyen.DefaultView.RowFilter = $"STT >= {minSTT} AND STT <= {maxSTT}";

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

        private void CapNhatTongKet()
        {
            int soMatHang = dtHangChuyen.Rows.Count;
            decimal tongSoLuong = 0;
            decimal tongGiaTri = 0;

            foreach (DataRow r in dtHangChuyen.Rows)
            {
                if (r.RowState == DataRowState.Deleted) continue;
                tongSoLuong += Convert.ToDecimal(r["SoLuong"]);
                tongGiaTri += Convert.ToDecimal(r["ThanhTien"]);
            }

            lblSoMatHangVal.Text = soMatHang.ToString("N0");
            lblTongSoLuongVal.Text = tongSoLuong.ToString("N0");
            lblTongGiaTriVal.Text = string.Format("{0:N0} đ", tongGiaTri);
        }

        private void btnHienCot_Click(object sender, EventArgs e)
        {
            foreach (Form openForm in Application.OpenForms)
            {
                if (openForm is frmHienCotHangChuyen)
                {
                    openForm.Close();
                    return;
                }
            }

            frmHienCotHangChuyen frm = new frmHienCotHangChuyen(dgvHangChuyen);
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
            if (!dgvHangChuyen.Columns.Contains("colSoLuong") || !dgvHangChuyen.Columns["colSoLuong"].Visible)
            {
                MessageBox.Show(
                    "Cột dữ liệu 'Số lượng' hiện đang không hiển thị trên danh sách hàng chuyển.\n\nVui lòng vào mục 'Hiện cột' và tích chọn hiển thị lại cột 'Số lượng' trước khi nhập file!",
                    "Thông báo",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
                return;
            }

            if (cboKhoNguon.SelectedValue == null) return;
            int maKhoNguon = 0;
            if (cboKhoNguon.SelectedValue is DataRowView drv && drv["MaKho"] != DBNull.Value)
            {
                maKhoNguon = Convert.ToInt32(drv["MaKho"]);
            }
            else if (int.TryParse(cboKhoNguon.SelectedValue.ToString(), out int k))
            {
                maKhoNguon = k;
            }

            using (frmNhapChuyenKhoFile frm = new frmNhapChuyenKhoFile(maKhoNguon))
            {
                if (frm.ShowDialog(this) == DialogResult.OK && frm.DtKetQuaNhap != null)
                {
                    int demThemMoi = 0;
                    int demCongDon = 0;
                    List<string> dsBoQua = new List<string>(); // Các mặt hàng không nạp được vào phiếu kèm lý do

                    foreach (DataRow r in frm.DtKetQuaNhap.Rows)
                    {
                        int maHang = Convert.ToInt32(r["MaHang"]);
                        decimal slChuyen = Convert.ToDecimal(r["SoLuongChuyen"]);

                        DataRow[] existing = dtHangChuyen.Select($"MaHang = {maHang}");
                        if (existing.Length > 0)
                        {
                            // Phương án "Bỏ qua": không sửa dòng đã có trong phiếu
                            if (!frm.CapNhatNeuTrung)
                            {
                                dsBoQua.Add($"Mã '{r["MaHangNoiBo"]}' đã có trong phiếu (không cập nhật)");
                                continue;
                            }

                            decimal tonKho = Convert.ToDecimal(existing[0]["TonKho"]);
                            decimal slMoi = Convert.ToDecimal(existing[0]["SoLuong"]) + slChuyen;
                            if (slMoi <= tonKho)
                            {
                                existing[0]["SoLuong"] = slMoi;
                                existing[0]["ThanhTien"] = slMoi * Convert.ToDecimal(existing[0]["DonGiaChuyen"]);
                                demCongDon++;
                            }
                            else
                            {
                                dsBoQua.Add($"Mã '{r["MaHangNoiBo"]}': cộng dồn {slMoi:N0} vượt tồn kho gửi ({tonKho:N0})");
                            }
                        }
                        else
                        {
                            DataRow newRow = dtHangChuyen.NewRow();
                            newRow["STT"] = dtHangChuyen.Rows.Count + 1;
                            newRow["MaHang"] = maHang;
                            newRow["MaHangNoiBo"] = r["MaHangNoiBo"];
                            newRow["TenHang"] = r["TenHang"];
                            newRow["TenDVT"] = r["TenDVT"];

                            decimal ton = 0;
                            if (dtDanhSachHang != null)
                            {
                                DataRow[] hRows = dtDanhSachHang.Select($"MaHang = {maHang}");
                                if (hRows.Length > 0) ton = Convert.ToDecimal(hRows[0]["TonKho"]);
                            }

                            newRow["TonKho"] = ton;
                            newRow["SoLuong"] = slChuyen;
                            newRow["DonGiaChuyen"] = r["DonGia"];
                            newRow["ThanhTien"] = slChuyen * Convert.ToDecimal(r["DonGia"]);

                            dtHangChuyen.Rows.Add(newRow);
                            demThemMoi++;
                        }
                    }

                    DanhSoLaiStt();
                    CapNhatTongKet();
                    CapNhatPhanTrang();

                    // Báo kết quả nạp vào phiếu (kể cả các dòng không nạp được)
                    if (dsBoQua.Count > 0)
                    {
                        MessageBox.Show($"Nạp vào phiếu: thêm mới {demThemMoi} dòng, cộng dồn {demCongDon} dòng." + frmNhapChuyenKhoFile.TaoThongBaoBoQua(dsBoQua),
                            "Kết quả nạp file", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
            }
        }

        private void btnBoQua_Click(object sender, EventArgs e)
        {
            if (dgvHangChuyen.IsCurrentCellInEditMode)
            {
                dgvHangChuyen.CancelEdit();
            }

            if (dtHangChuyen.Rows.Count > 0)
            {
                DialogResult dr = MessageBox.Show(
                    "Dữ liệu phiếu chuyển kho chưa được lưu. Bạn có chắc chắn muốn bỏ qua?",
                    "Xác nhận",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question);
                if (dr != DialogResult.Yes) return;
            }
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void frmThemPhieuChuyenKhoNew_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                btnBoQua_Click(null, null);
            }
        }

        private void btnLuuTam_Click(object sender, EventArgs e)
        {
            LuuPhieu("NHAP");
        }

        private void btnLuu_Click(object sender, EventArgs e)
        {
            LuuPhieu("DA_XUAT");
        }

        private void LuuPhieu(string trangThaiMoi)
        {
            if (dgvHangChuyen.IsCurrentCellInEditMode)
            {
                ApDungSoLuongNhap();
            }
            this.BindingContext[dtHangChuyen]?.EndCurrentEdit();

            // Bắt buộc tài khoản phải gắn với nhân viên (MaNVLap/MaNVXuat tham chiếu NhanVien.MaNV)
            if (!SessionContext.MaNV.HasValue || SessionContext.MaNV.Value <= 0)
            {
                MessageBox.Show("Tài khoản chưa được gắn với nhân viên, không thể lập chứng từ.", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string maPhieu = txtMaPhieu.Text.Trim();
            if (string.IsNullOrEmpty(maPhieu))
            {
                MessageBox.Show("Mã phiếu không được để trống!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtMaPhieu.Focus();
                return;
            }

            int maKhoNguon = 0;
            if (cboKhoNguon.SelectedValue is DataRowView drvNguon && drvNguon["MaKho"] != DBNull.Value)
            {
                maKhoNguon = Convert.ToInt32(drvNguon["MaKho"]);
            }
            else if (cboKhoNguon.SelectedValue != null && int.TryParse(cboKhoNguon.SelectedValue.ToString(), out int kn))
            {
                maKhoNguon = kn;
            }

            if (maKhoNguon <= 0)
            {
                MessageBox.Show("Vui lòng chọn kho gửi!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            int maKhoDich = 0;
            if (cboKhoDich.SelectedValue is DataRowView drvDich && drvDich["MaKho"] != DBNull.Value)
            {
                maKhoDich = Convert.ToInt32(drvDich["MaKho"]);
            }
            else if (cboKhoDich.SelectedValue != null && int.TryParse(cboKhoDich.SelectedValue.ToString(), out int kd))
            {
                maKhoDich = kd;
            }

            if (maKhoDich <= 0)
            {
                MessageBox.Show("Vui lòng chọn kho nhận!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (maKhoNguon == maKhoDich)
            {
                MessageBox.Show("Kho gửi và kho nhận không được trùng nhau!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (dtHangChuyen.Rows.Count == 0)
            {
                MessageBox.Show("Vui lòng thêm ít nhất một mặt hàng cần chuyển!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            int maNVChon = SessionContext.MaNV.Value;
            if (cboNguoiChuyen.SelectedValue is DataRowView drvNV && drvNV["MaNV"] != DBNull.Value)
            {
                maNVChon = Convert.ToInt32(drvNV["MaNV"]);
            }
            else if (cboNguoiChuyen.SelectedValue != null && int.TryParse(cboNguoiChuyen.SelectedValue.ToString(), out int nvId) && nvId > 0)
            {
                maNVChon = nvId;
            }

            if (trangThaiMoi == "DA_XUAT")
            {
                // Kiểm tra sơ bộ tồn kho gửi (thông báo thân thiện); sẽ kiểm tra lại trong giao dịch khi lưu
                foreach (DataRow r in dtHangChuyen.Rows)
                {
                    if (r.RowState == DataRowState.Deleted) continue;

                    int maHang = Convert.ToInt32(r["MaHang"]);
                    decimal slChuyen = Convert.ToDecimal(r["SoLuong"]);

                    string sqlCheckTon = "SELECT ISNULL(SoLuongTon, 0) FROM vw_TonKhoHienTai WHERE MaKho = @MaKho AND MaHang = @MaHang";
                    object tonObj = DataProcessing.ReadValue(sqlCheckTon, new SqlParameter[] {
                        new SqlParameter("@MaKho", maKhoNguon),
                        new SqlParameter("@MaHang", maHang)
                    });

                    decimal tonHienTai = tonObj != null && tonObj != DBNull.Value ? Convert.ToDecimal(tonObj) : 0;
                    if (slChuyen > tonHienTai)
                    {
                        MessageBox.Show(string.Format("Mặt hàng '{0}' ({1}) có số lượng chuyển {2:N0} vượt quá tồn kho thực tế hiện tại {3:N0}!\nKhông thể thực hiện chuyển hàng.",
                            r["TenHang"], r["MaHangNoiBo"], slChuyen, tonHienTai), "Chặn tồn âm", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                }

                string msg = string.Format("Bạn có chắc chắn muốn chuyển hàng từ '{0}' đến '{1}'?\n\nSố lượng tồn sẽ được trừ khỏi kho gửi ngay sau khi xác nhận.",
                    cboKhoNguon.Text, cboKhoDich.Text);
                if (MessageBox.Show(msg, "Xác nhận chuyển hàng", MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
                {
                    return;
                }
            }

            decimal tongSoLuong = 0;
            decimal tongGiaTri = 0;
            foreach (DataRow r in dtHangChuyen.Rows)
            {
                if (r.RowState == DataRowState.Deleted) continue;
                tongSoLuong += Convert.ToDecimal(r["SoLuong"]);
                tongGiaTri += Convert.ToDecimal(r["ThanhTien"]);
            }

            try
            {
                DataProcessing.ChangeDataTransaction((conn, trans) =>
                {
                    // 0. Kiểm tra lại mã phiếu ngay trong giao dịch (chống trùng khi nhiều người lập phiếu cùng lúc)
                    if (MaPhieuDaTonTai(conn, trans, maPhieu))
                    {
                        if (maPhieu == maPhieuTuSinh)
                        {
                            // Mã tự sinh đã bị người khác dùng: cấp mã mới
                            maPhieu = SinhMaPhieuTuDong(conn, trans);
                        }
                        else
                        {
                            throw new InvalidOperationException(string.Format("Mã phiếu '{0}' đã tồn tại trong hệ thống! Vui lòng chọn mã khác.", maPhieu));
                        }
                    }

                    // 1. Chuyển hàng ngay: kiểm tra lại tồn kho gửi trong giao dịch (cùng conn/trans), thiếu thì rollback
                    if (trangThaiMoi == "DA_XUAT")
                    {
                        string sqlKiemTon = "SELECT ISNULL(SUM(SoLuongTon), 0) FROM vw_TonKhoHienTai WHERE MaKho = @MaKho AND MaHang = @MaHang";
                        foreach (DataRow r in dtHangChuyen.Rows)
                        {
                            if (r.RowState == DataRowState.Deleted) continue;

                            decimal slCanChuyen = Convert.ToDecimal(r["SoLuong"]);
                            decimal tonTrongGiaoDich;
                            using (SqlCommand cmdTon = new SqlCommand(sqlKiemTon, conn, trans))
                            {
                                cmdTon.Parameters.AddWithValue("@MaKho", maKhoNguon);
                                cmdTon.Parameters.AddWithValue("@MaHang", r["MaHang"]);
                                tonTrongGiaoDich = Convert.ToDecimal(cmdTon.ExecuteScalar());
                            }

                            if (slCanChuyen > tonTrongGiaoDich)
                            {
                                throw new InvalidOperationException(string.Format("Mặt hàng '{0}' ({1}) có số lượng chuyển {2:N0} vượt quá tồn kho thực tế hiện tại {3:N0}!\nKhông thể thực hiện chuyển hàng.",
                                    r["TenHang"], r["MaHangNoiBo"], slCanChuyen, tonTrongGiaoDich));
                            }
                        }
                    }

                    string sqlInsertP = @"INSERT INTO PhieuChuyenKho
                                         (MaPhieu, MaKhoNguon, MaKhoDich, MaNVLap, MaNVXuat, NgayLap, ThoiDiemXuat, 
                                          TrangThai, GhiChu, DaDanhDau, TongSoLuongChuyen, TongSoLuongNhan, TongGiaTriChuyen)
                                         VALUES 
                                         (@MaPhieu, @MaKhoNguon, @MaKhoDich, @MaNVLap, @MaNVXuat, @NgayLap, @ThoiDiemXuat,
                                          @TrangThai, @GhiChu, 0, @TongSoLuongChuyen, 0, @TongGiaTriChuyen);
                                         SELECT SCOPE_IDENTITY();";

                    int maPhieuChuyenInserted;
                    using (SqlCommand cmdP = new SqlCommand(sqlInsertP, conn, trans))
                    {
                        cmdP.Parameters.AddWithValue("@MaPhieu", maPhieu);
                        cmdP.Parameters.AddWithValue("@MaKhoNguon", maKhoNguon);
                        cmdP.Parameters.AddWithValue("@MaKhoDich", maKhoDich);
                        cmdP.Parameters.AddWithValue("@MaNVLap", maNVChon);
                        cmdP.Parameters.AddWithValue("@MaNVXuat", trangThaiMoi == "DA_XUAT" ? (object)maNVChon : DBNull.Value);
                        cmdP.Parameters.AddWithValue("@NgayLap", dtpNgayChuyen.Value);
                        cmdP.Parameters.AddWithValue("@ThoiDiemXuat", trangThaiMoi == "DA_XUAT" ? (object)DateTime.Now : DBNull.Value);
                        cmdP.Parameters.AddWithValue("@TrangThai", trangThaiMoi);
                        cmdP.Parameters.AddWithValue("@GhiChu", string.IsNullOrWhiteSpace(txtGhiChu.Text) ? (object)DBNull.Value : txtGhiChu.Text.Trim());
                        cmdP.Parameters.AddWithValue("@TongSoLuongChuyen", tongSoLuong);
                        cmdP.Parameters.AddWithValue("@TongGiaTriChuyen", tongGiaTri);

                        maPhieuChuyenInserted = Convert.ToInt32(cmdP.ExecuteScalar());
                    }

                    string sqlInsertCT = @"INSERT INTO ChiTietPhieuChuyenKho 
                                          (MaPhieuChuyen, MaHang, SoLuong, DonGiaChuyen, ThanhTien)
                                          VALUES 
                                          (@MaPhieuChuyen, @MaHang, @SoLuong, @DonGiaChuyen, @ThanhTien)";

                    foreach (DataRow r in dtHangChuyen.Rows)
                    {
                        if (r.RowState == DataRowState.Deleted) continue;

                        using (SqlCommand cmdCT = new SqlCommand(sqlInsertCT, conn, trans))
                        {
                            cmdCT.Parameters.AddWithValue("@MaPhieuChuyen", maPhieuChuyenInserted);
                            cmdCT.Parameters.AddWithValue("@MaHang", r["MaHang"]);
                            cmdCT.Parameters.AddWithValue("@SoLuong", r["SoLuong"]);
                            cmdCT.Parameters.AddWithValue("@DonGiaChuyen", r["DonGiaChuyen"]);
                            cmdCT.Parameters.AddWithValue("@ThanhTien", r["ThanhTien"]);
                            cmdCT.ExecuteNonQuery();
                        }
                    }

                    string hanhDong = trangThaiMoi == "DA_XUAT" ? "XUAT_CHUYEN" : "LUU_TAM";
                    string noiDung = trangThaiMoi == "DA_XUAT"
                        ? string.Format("Lập phiếu chuyển hàng {0} từ {1} đến {2} ({3:N0} sản phẩm)", maPhieu, cboKhoNguon.Text, cboKhoDich.Text, tongSoLuong)
                        : string.Format("Lưu tạm phiếu chuyển kho {0} ({1} ➜ {2})", maPhieu, cboKhoNguon.Text, cboKhoDich.Text);

                    string sqlLog = @"INSERT INTO NhatKyThaoTac (MaTaiKhoan, ThoiDiem, ThaoTac, LoaiDoiTuong, MaDoiTuong, NoiDung)
                                     VALUES (@MaTaiKhoan, GETDATE(), @ThaoTac, 'CHUYEN_KHO', @MaDoiTuong, @NoiDung)";
                    using (SqlCommand cmdLog = new SqlCommand(sqlLog, conn, trans))
                    {
                        cmdLog.Parameters.AddWithValue("@MaTaiKhoan", SessionContext.MaTaiKhoan > 0 ? SessionContext.MaTaiKhoan : 1);
                        cmdLog.Parameters.AddWithValue("@ThaoTac", hanhDong);
                        cmdLog.Parameters.AddWithValue("@MaDoiTuong", maPhieu);
                        cmdLog.Parameters.AddWithValue("@NoiDung", noiDung);
                        cmdLog.ExecuteNonQuery();
                    }
                });

                string thongBao = trangThaiMoi == "DA_XUAT"
                    ? "Lập phiếu chuyển kho và chuyển hàng thành công!\nTồn kho tại kho gửi đã được cập nhật."
                    : "Lưu tạm phiếu chuyển kho thành công!";
                MessageBox.Show(thongBao, "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);

                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            catch (InvalidOperationException ex)
            {
                MessageBox.Show(ex.Message, "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            catch (SqlException ex) when (ex.Number == 2627 || ex.Number == 2601)
            {
                // Vi phạm ràng buộc duy nhất (trùng mã phiếu do lưu đồng thời): cấp lại mã mới để người dùng lưu lại
                if (txtMaPhieu.Text.Trim() == maPhieuTuSinh)
                {
                    txtMaPhieu.Text = SinhMaPhieuTuDong();
                    maPhieuTuSinh = txtMaPhieu.Text;
                }
                MessageBox.Show("Mã phiếu chuyển kho bị trùng với phiếu đã có (có thể do người khác vừa lưu phiếu cùng lúc).\nVui lòng kiểm tra và bấm lưu lại.", "Trùng dữ liệu", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi lưu phiếu chuyển kho: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
