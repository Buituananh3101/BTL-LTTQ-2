using BTL_LTTQ_2.Classes;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Windows.Forms;

namespace BTL_LTTQ_2.GUI.Forms.ChuyenKho
{
    public partial class frmThemPhieuChuyenKho : Form
    {
        private DataTable dtChiTiet;
        private DataTable dtHangHoaAll;

        public frmThemPhieuChuyenKho()
        {
            InitializeComponent();
            KhoiTaoBangChiTiet();
        }

        private void KhoiTaoBangChiTiet()
        {
            dtChiTiet = new DataTable();
            dtChiTiet.Columns.Add("STT", typeof(int));
            dtChiTiet.Columns.Add("MaHang", typeof(int));
            dtChiTiet.Columns.Add("MaHangNoiBo", typeof(string));
            dtChiTiet.Columns.Add("TenHang", typeof(string));
            dtChiTiet.Columns.Add("TenDVT", typeof(string));
            dtChiTiet.Columns.Add("TonKho", typeof(decimal));
            dtChiTiet.Columns.Add("SoLuong", typeof(decimal));
            dtChiTiet.Columns.Add("DonGiaChuyen", typeof(decimal));
            dtChiTiet.Columns.Add("ThanhTien", typeof(decimal));
        }

        private void frmThemPhieuChuyenKho_Load(object sender, EventArgs e)
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
            colTonKho.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            colTonKho.DefaultCellStyle.Format = "N0";
            colSoLuong.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            colSoLuong.DefaultCellStyle.Format = "N0";
            colDonGia.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            colDonGia.DefaultCellStyle.Format = "N0";
            colThanhTien.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            colThanhTien.DefaultCellStyle.Format = "N0";

            dgvChiTiet.DataSource = dtChiTiet;

            lblNguoiChuyenVal.Text = SessionContext.HoTen;
            dtpNgayChuyen.Value = DateTime.Now;

            TaiDanhSachKho();
            TaiDanhSachHangHoa();
        }

        private void TaiDanhSachKho()
        {
            try
            {
                string sqlKho = "SELECT MaKho, TenKho FROM Kho WHERE TrangThai = 'HOAT_DONG' OR TrangThai = 'ACTIVE' ORDER BY MaKho ASC";
                DataTable dtKhoNguon = DataProcessing.ReadData(sqlKho);
                DataTable dtKhoDich = DataProcessing.ReadData(sqlKho);

                cboKhoNguon.DataSource = dtKhoNguon;
                cboKhoNguon.DisplayMember = "TenKho";
                cboKhoNguon.ValueMember = "MaKho";

                if (SessionContext.MaKhoHienTai > 0)
                {
                    cboKhoNguon.SelectedValue = SessionContext.MaKhoHienTai;
                }

                if (!SessionContext.IsAdmin)
                {
                    cboKhoNguon.Enabled = false;
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
            if (cboKhoNguon.SelectedValue == null || !int.TryParse(cboKhoNguon.SelectedValue.ToString(), out int maKhoNguon)) return;

            string sqlKhoDich = "SELECT MaKho, TenKho FROM Kho WHERE (TrangThai = 'HOAT_DONG' OR TrangThai = 'ACTIVE') AND MaKho <> @MaKhoNguon ORDER BY MaKho ASC";
            DataTable dtKD = DataProcessing.ReadData(sqlKhoDich, new SqlParameter[] {
                new SqlParameter("@MaKhoNguon", maKhoNguon)
            });

            cboKhoDich.DataSource = dtKD;
            cboKhoDich.DisplayMember = "TenKho";
            cboKhoDich.ValueMember = "MaKho";
        }

        private void cboKhoNguon_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cboKhoNguon.SelectedValue != null && int.TryParse(cboKhoNguon.SelectedValue.ToString(), out int _))
            {
                CapNhatDanhSachKhoDich();
                TaiDanhSachHangHoa();

                if (dtChiTiet.Rows.Count > 0)
                {
                    DialogResult dr = MessageBox.Show("Đổi kho gửi sẽ làm mới danh sách hàng hóa đã chọn. Bạn có chắc chắn muốn đổi không?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (dr == DialogResult.Yes)
                    {
                        dtChiTiet.Clear();
                        CapNhatTongKet();
                    }
                }
            }
        }

        private void TaiDanhSachHangHoa()
        {
            if (cboKhoNguon.SelectedValue == null || !int.TryParse(cboKhoNguon.SelectedValue.ToString(), out int maKhoNguon)) return;

            try
            {
                string sql = @"SELECT h.MaHang, h.MaHangNoiBo, h.TenHang, ISNULL(dvt.TenDVT, '') AS TenDVT, 
                                      ISNULL(h.GiaVon, 0) AS GiaVon,
                                      ISNULL(t.SoLuongTon, 0) AS TonKho,
                                      ('[' + h.MaHangNoiBo + N'] ' + h.TenHang + N' (Tồn: ' + CAST(CAST(ISNULL(t.SoLuongTon, 0) AS INT) AS NVARCHAR(20)) + N' ' + ISNULL(dvt.TenDVT, '') + N')') AS TenHienThi
                               FROM HangHoa h
                               LEFT JOIN DonViTinh dvt ON h.MaDvt = dvt.MaDvt
                               LEFT JOIN vw_TonKhoHienTai t ON h.MaHang = t.MaHang AND t.MaKho = @MaKho
                               WHERE (h.TrangThai = 'DANG_BAN' OR h.TrangThai = 'DANG_KINH_DOANH' OR h.TrangThai IS NULL)
                               ORDER BY h.MaHang ASC";

                dtHangHoaAll = DataProcessing.ReadData(sql, new SqlParameter[] {
                    new SqlParameter("@MaKho", maKhoNguon)
                });

                ComboboxHelper.CaiDatTimKiem(cboHangHoa, dtHangHoaAll, "TenHienThi", "MaHang", () => {
                    CapNhatTonHienTai();
                });

                CapNhatTonHienTai();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi nạp danh sách hàng hóa: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void cboHangHoa_SelectedIndexChanged(object sender, EventArgs e)
        {
            CapNhatTonHienTai();
        }

        private void CapNhatTonHienTai()
        {
            if (cboHangHoa.SelectedValue != null && dtHangHoaAll != null &&
                int.TryParse(cboHangHoa.SelectedValue.ToString(), out int maHang) && maHang > 0)
            {
                DataRow[] rows = dtHangHoaAll.Select("MaHang = " + maHang);
                if (rows.Length > 0)
                {
                    decimal tonKho = Convert.ToDecimal(rows[0]["TonKho"]);
                    string dvt = rows[0]["TenDVT"].ToString();
                    lblTonKhoGui.Text = string.Format("Tồn kho gửi: {0:N0} {1}", tonKho, dvt);

                    if (tonKho > 0)
                    {
                        nudSoLuong.Maximum = tonKho;
                        nudSoLuong.Value = 1;
                    }
                    else
                    {
                        nudSoLuong.Maximum = 1;
                        nudSoLuong.Value = 1;
                    }
                    return;
                }
            }
            lblTonKhoGui.Text = "Tồn kho gửi: ---";
        }

        private void btnThemHang_Click(object sender, EventArgs e)
        {
            if (cboHangHoa.SelectedValue == null || !int.TryParse(cboHangHoa.SelectedValue.ToString(), out int maHang) || maHang <= 0)
            {
                MessageBox.Show("Vui lòng chọn một mặt hàng!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            DataRow[] rows = dtHangHoaAll.Select("MaHang = " + maHang);
            if (rows.Length == 0) return;

            DataRow rH = rows[0];
            decimal tonKho = Convert.ToDecimal(rH["TonKho"]);
            decimal slChuyen = nudSoLuong.Value;

            if (tonKho <= 0)
            {
                MessageBox.Show("Mặt hàng này hiện không còn tồn trong kho gửi (Tồn: 0)!", "Cảnh báo hết hàng", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Kiểm tra xem đã có trong bảng chưa
            DataRow[] existing = dtChiTiet.Select("MaHang = " + maHang);
            if (existing.Length > 0)
            {
                decimal slHienCo = Convert.ToDecimal(existing[0]["SoLuong"]);
                decimal slMoi = slHienCo + slChuyen;

                if (slMoi > tonKho)
                {
                    MessageBox.Show(string.Format("Tổng số lượng chuyển ({0:N0}) vượt quá số lượng tồn kho gửi ({1:N0})!", slMoi, tonKho), "Cảnh báo vượt tồn", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                existing[0]["SoLuong"] = slMoi;
                existing[0]["ThanhTien"] = slMoi * Convert.ToDecimal(existing[0]["DonGiaChuyen"]);
            }
            else
            {
                if (slChuyen > tonKho)
                {
                    MessageBox.Show(string.Format("Số lượng chuyển ({0:N0}) vượt quá tồn kho gửi ({1:N0})!", slChuyen, tonKho), "Cảnh báo vượt tồn", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                DataRow newRow = dtChiTiet.NewRow();
                newRow["STT"] = dtChiTiet.Rows.Count + 1;
                newRow["MaHang"] = maHang;
                newRow["MaHangNoiBo"] = rH["MaHangNoiBo"];
                newRow["TenHang"] = rH["TenHang"];
                newRow["TenDVT"] = rH["TenDVT"];
                newRow["TonKho"] = tonKho;
                newRow["SoLuong"] = slChuyen;
                newRow["DonGiaChuyen"] = rH["GiaVon"];
                newRow["ThanhTien"] = slChuyen * Convert.ToDecimal(rH["GiaVon"]);

                dtChiTiet.Rows.Add(newRow);
            }

            DanhSoLaiSTT();
            CapNhatTongKet();
        }

        private void btnNhapFile_Click(object sender, EventArgs e)
        {
            if (cboKhoNguon.SelectedValue == null) return;
            int maKhoNguon = Convert.ToInt32(cboKhoNguon.SelectedValue);

            using (frmNhapChuyenKhoFile frm = new frmNhapChuyenKhoFile(maKhoNguon))
            {
                if (frm.ShowDialog(this) == DialogResult.OK && frm.DtKetQuaNhap != null)
                {
                    foreach (DataRow r in frm.DtKetQuaNhap.Rows)
                    {
                        int maHang = Convert.ToInt32(r["MaHang"]);
                        decimal slChuyen = Convert.ToDecimal(r["SoLuongChuyen"]);

                        DataRow[] existing = dtChiTiet.Select("MaHang = " + maHang);
                        if (existing.Length > 0)
                        {
                            decimal tonKho = Convert.ToDecimal(existing[0]["TonKho"]);
                            decimal slMoi = Convert.ToDecimal(existing[0]["SoLuong"]) + slChuyen;
                            if (slMoi <= tonKho)
                            {
                                existing[0]["SoLuong"] = slMoi;
                                existing[0]["ThanhTien"] = slMoi * Convert.ToDecimal(existing[0]["DonGiaChuyen"]);
                            }
                        }
                        else
                        {
                            DataRow newRow = dtChiTiet.NewRow();
                            newRow["STT"] = dtChiTiet.Rows.Count + 1;
                            newRow["MaHang"] = maHang;
                            newRow["MaHangNoiBo"] = r["MaHangNoiBo"];
                            newRow["TenHang"] = r["TenHang"];
                            newRow["TenDVT"] = r["TenDVT"];

                            decimal ton = 0;
                            DataRow[] hRows = dtHangHoaAll.Select("MaHang = " + maHang);
                            if (hRows.Length > 0) ton = Convert.ToDecimal(hRows[0]["TonKho"]);

                            newRow["TonKho"] = ton;
                            newRow["SoLuong"] = slChuyen;
                            newRow["DonGiaChuyen"] = r["DonGia"];
                            newRow["ThanhTien"] = slChuyen * Convert.ToDecimal(r["DonGia"]);

                            dtChiTiet.Rows.Add(newRow);
                        }
                    }

                    DanhSoLaiSTT();
                    CapNhatTongKet();
                }
            }
        }

        private void dgvChiTiet_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0 || e.RowIndex >= dtChiTiet.Rows.Count) return;

            DataRow row = dtChiTiet.Rows[e.RowIndex];
            decimal tonKho = Convert.ToDecimal(row["TonKho"]);
            decimal slChuyen = 0;

            if (row["SoLuong"] == DBNull.Value || !decimal.TryParse(row["SoLuong"].ToString(), out slChuyen) || slChuyen <= 0)
            {
                MessageBox.Show("Số lượng chuyển phải là số lớn hơn 0!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                slChuyen = 1;
                row["SoLuong"] = slChuyen;
            }
            else if (slChuyen > tonKho)
            {
                MessageBox.Show(string.Format("Số lượng chuyển ({0:N0}) không được vượt quá số lượng tồn kho gửi ({1:N0})!", slChuyen, tonKho), "Cảnh báo vượt tồn", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                slChuyen = tonKho;
                row["SoLuong"] = slChuyen;
            }

            decimal donGia = Convert.ToDecimal(row["DonGiaChuyen"]);
            row["ThanhTien"] = slChuyen * donGia;

            CapNhatTongKet();
        }

        private void dgvChiTiet_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && dgvChiTiet.Columns[e.ColumnIndex].Name == "colXoa")
            {
                dtChiTiet.Rows.RemoveAt(e.RowIndex);
                DanhSoLaiSTT();
                CapNhatTongKet();
            }
        }

        private void DanhSoLaiSTT()
        {
            for (int i = 0; i < dtChiTiet.Rows.Count; i++)
            {
                dtChiTiet.Rows[i]["STT"] = i + 1;
            }
        }

        private void CapNhatTongKet()
        {
            int soMatHang = dtChiTiet.Rows.Count;
            decimal tongSoLuong = 0;
            decimal tongGiaTri = 0;

            foreach (DataRow r in dtChiTiet.Rows)
            {
                tongSoLuong += Convert.ToDecimal(r["SoLuong"]);
                tongGiaTri += Convert.ToDecimal(r["ThanhTien"]);
            }

            lblSoMatHang.Text = "Số mặt hàng: " + soMatHang.ToString("N0");
            lblTongSoLuong.Text = "Tổng số lượng chuyển: " + tongSoLuong.ToString("N0");
            lblTongGiaTri.Text = "Tổng giá trị: " + tongGiaTri.ToString("N0") + " VNĐ";
        }

        private void btnBoQua_Click(object sender, EventArgs e)
        {
            if (dtChiTiet.Rows.Count > 0)
            {
                DialogResult dr = MessageBox.Show("Dữ liệu phiếu chuyển kho chưa được lưu. Bạn có chắc chắn muốn bỏ qua?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (dr != DialogResult.Yes) return;
            }
            this.DialogResult = DialogResult.Cancel;
            this.Close();
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
            if (cboKhoNguon.SelectedValue == null || !int.TryParse(cboKhoNguon.SelectedValue.ToString(), out int maKhoNguon) || maKhoNguon <= 0)
            {
                MessageBox.Show("Vui lòng chọn kho gửi!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (cboKhoDich.SelectedValue == null || !int.TryParse(cboKhoDich.SelectedValue.ToString(), out int maKhoDich) || maKhoDich <= 0)
            {
                MessageBox.Show("Vui lòng chọn kho nhận!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (maKhoNguon == maKhoDich)
            {
                MessageBox.Show("Kho gửi và kho nhận không được trùng nhau!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (dtChiTiet.Rows.Count == 0)
            {
                MessageBox.Show("Vui lòng thêm ít nhất một mặt hàng cần chuyển!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Kiểm tra tồn âm kho nguồn nếu chuyển hàng (DA_XUAT)
            if (trangThaiMoi == "DA_XUAT")
            {
                foreach (DataRow r in dtChiTiet.Rows)
                {
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
            foreach (DataRow r in dtChiTiet.Rows)
            {
                tongSoLuong += Convert.ToDecimal(r["SoLuong"]);
                tongGiaTri += Convert.ToDecimal(r["ThanhTien"]);
            }

            try
            {
                DataProcessing.ChangeDataTransaction((conn, trans) =>
                {
                    // 1. Sinh mã phiếu CK00000X
                    string sqlMaxId = "SELECT ISNULL(MAX(MaPhieuChuyen), 0) FROM PhieuChuyenKho";
                    int nextId;
                    using (SqlCommand cmdMax = new SqlCommand(sqlMaxId, conn, trans))
                    {
                        nextId = Convert.ToInt32(cmdMax.ExecuteScalar()) + 1;
                    }
                    string maPhieu = "CK" + nextId.ToString("D6");

                    // 2. Chèn PhieuChuyenKho
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
                        cmdP.Parameters.AddWithValue("@MaNVLap", SessionContext.MaNV.HasValue ? (object)SessionContext.MaNV.Value : 1);
                        cmdP.Parameters.AddWithValue("@MaNVXuat", trangThaiMoi == "DA_XUAT" ? (SessionContext.MaNV.HasValue ? (object)SessionContext.MaNV.Value : 1) : DBNull.Value);
                        cmdP.Parameters.AddWithValue("@NgayLap", dtpNgayChuyen.Value);
                        cmdP.Parameters.AddWithValue("@ThoiDiemXuat", trangThaiMoi == "DA_XUAT" ? (object)DateTime.Now : DBNull.Value);
                        cmdP.Parameters.AddWithValue("@TrangThai", trangThaiMoi);
                        cmdP.Parameters.AddWithValue("@GhiChu", string.IsNullOrWhiteSpace(txtGhiChu.Text) ? (object)DBNull.Value : txtGhiChu.Text.Trim());
                        cmdP.Parameters.AddWithValue("@TongSoLuongChuyen", tongSoLuong);
                        cmdP.Parameters.AddWithValue("@TongGiaTriChuyen", tongGiaTri);

                        maPhieuChuyenInserted = Convert.ToInt32(cmdP.ExecuteScalar());
                    }

                    // 3. Chèn ChiTietPhieuChuyenKho
                    string sqlInsertCT = @"INSERT INTO ChiTietPhieuChuyenKho 
                                          (MaPhieuChuyen, MaHang, SoLuong, DonGiaChuyen, ThanhTien)
                                          VALUES 
                                          (@MaPhieuChuyen, @MaHang, @SoLuong, @DonGiaChuyen, @ThanhTien)";

                    foreach (DataRow r in dtChiTiet.Rows)
                    {
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

                    // 4. Ghi audit log
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
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi lưu phiếu chuyển kho: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
