using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Text;
using System.Windows.Forms;
using System.Windows.Forms.VisualStyles;
using BTL_LTTQ_2.Classes;

using BTL_LTTQ_2.GUI.Forms.DanhSachHangHoa;

namespace BTL_LTTQ_2.GUI.Forms
{
    public partial class frmDanhSachHangHoa : Form
    {
        // Các biến phục vụ phân trang
        private int trangHienTai = 1;
        private int soDongMoiTrang = 20;
        private int tongSoBanGhi = 0;
        private int tongSoTrang = 1;

        // Cờ kiểm soát tránh load lặp khi khởi tạo combobox
        private bool isLoaded = false;

        // Cờ kiểm soát Header CheckBox (Chọn tất cả)
        private bool isHeaderChecked = false;

        // Lưu truy vấn đầy đủ (không phân trang) để xuất file toàn bộ kết quả lọc
        private string sqlXuatFile = null;
        private SqlParameter[] thamSoXuatFile = null;
        private Timer searchDebounceTimer;

        public frmDanhSachHangHoa()
        {
            InitializeComponent();
            KhoiTaoDebounceTimer();
            if (txtTimKiem != null)
            {
                txtTimKiem.KeyDown += txtTimKiem_KeyDown;
            }
            if (btnNhapFile != null)
            {
                btnNhapFile.Click += btnNhapFile_Click;
            }
        }

        private void KhoiTaoDebounceTimer()
        {
            searchDebounceTimer = new Timer();
            searchDebounceTimer.Interval = 300;
            searchDebounceTimer.Tick += (s, e) =>
            {
                searchDebounceTimer.Stop();
                trangHienTai = 1;
                HienThiDanhSach();
            };
        }

        private void txtTimKiem_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;
                searchDebounceTimer?.Stop();
                trangHienTai = 1;
                HienThiDanhSach();
            }
        }

        private void frmDanhSachHangHoa_Load(object sender, EventArgs e)
        {
            DinhDangLuoi();
            NapDuLieuBoLoc();
            isLoaded = true;
            HienThiDanhSach();
        }

        #region Định dạng giao diện & Cấu hình lưới

        private void DinhDangLuoi()
        {
            // Bật đệm đôi chống giật lag khi cuộn chuột
            typeof(DataGridView).InvokeMember("DoubleBuffered",
                System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.SetProperty,
                null, dgvHangHoa, new object[] { true });

            dgvHangHoa.AutoGenerateColumns = false;
            dgvHangHoa.Columns.Clear();
            dgvHangHoa.AllowUserToAddRows = false;
            dgvHangHoa.AllowUserToDeleteRows = false;
            dgvHangHoa.AllowUserToResizeRows = false;
            dgvHangHoa.ReadOnly = true;
            dgvHangHoa.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvHangHoa.MultiSelect = false;
            dgvHangHoa.RowHeadersVisible = false;
            dgvHangHoa.RowTemplate.Height = 48;
            dgvHangHoa.BackgroundColor = Color.White;
            dgvHangHoa.BorderStyle = BorderStyle.None;

            // Đăng ký sự kiện vẽ header và click chọn CheckBox
            dgvHangHoa.CellPainting += dgvHangHoa_CellPainting;
            dgvHangHoa.CellClick += dgvHangHoa_CellClick;
            dgvHangHoa.ColumnHeaderMouseClick += dgvHangHoa_ColumnHeaderMouseClick;
            dgvHangHoa.KeyDown += dgvHangHoa_KeyDown;

            // Màu dòng xen kẽ hiện đại
            dgvHangHoa.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(248, 250, 252);
            dgvHangHoa.DefaultCellStyle.SelectionBackColor = Color.FromArgb(224, 231, 255);
            dgvHangHoa.DefaultCellStyle.SelectionForeColor = Color.FromArgb(30, 41, 59);

            // Cột 0: Mã hàng (ID ngầm)
            dgvHangHoa.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "colMaHang",
                DataPropertyName = "MaHang",
                Visible = false
            });

            // Cột 1: Chọn CheckBox
            dgvHangHoa.Columns.Add(new DataGridViewCheckBoxColumn
            {
                Name = "colChon",
                DataPropertyName = "Chon",
                HeaderText = "",
                Width = 35,
                Resizable = DataGridViewTriState.False,
                SortMode = DataGridViewColumnSortMode.NotSortable,
                DefaultCellStyle = new DataGridViewCellStyle { Alignment = DataGridViewContentAlignment.MiddleCenter }
            });

            // Cột 2: Hình ảnh (Tạo thành hình vuông 48x48)
            dgvHangHoa.Columns.Add(new DataGridViewImageColumn
            {
                Name = "colHinhAnh",
                DataPropertyName = "HinhAnh",
                HeaderText = "",
                Width = 48,
                ImageLayout = DataGridViewImageCellLayout.Zoom,
                DefaultCellStyle = new DataGridViewCellStyle
                {
                    Alignment = DataGridViewContentAlignment.MiddleCenter,
                    NullValue = null
                }
            });

            // Cột 2: Mã hàng nội bộ
            dgvHangHoa.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "colMaHangNoiBo",
                DataPropertyName = "MaHangNoiBo",
                HeaderText = "Mã hàng",
                Width = 110,
                DefaultCellStyle = new DataGridViewCellStyle { Alignment = DataGridViewContentAlignment.MiddleCenter }
            });

            // Cột 2: Tên hàng
            dgvHangHoa.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "colTenHang",
                DataPropertyName = "TenHang",
                HeaderText = "Tên hàng hóa",
                AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill,
                DefaultCellStyle = new DataGridViewCellStyle { Alignment = DataGridViewContentAlignment.MiddleLeft }
            });

            // Cột 3: Nhóm hàng
            dgvHangHoa.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "colTenNhom",
                DataPropertyName = "TenNhom",
                HeaderText = "Nhóm hàng",
                Width = 140,
                DefaultCellStyle = new DataGridViewCellStyle { Alignment = DataGridViewContentAlignment.MiddleLeft }
            });

            // Cột 4: Đơn vị tính
            dgvHangHoa.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "colTenDVT",
                DataPropertyName = "TenDVT",
                HeaderText = "ĐVT",
                Width = 70,
                DefaultCellStyle = new DataGridViewCellStyle { Alignment = DataGridViewContentAlignment.MiddleCenter }
            });

            // Cột 5: Giá bán
            dgvHangHoa.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "colGiaBan",
                DataPropertyName = "GiaBanThamKhao",
                HeaderText = "Giá bán",
                Width = 120,
                DefaultCellStyle = new DataGridViewCellStyle { Alignment = DataGridViewContentAlignment.MiddleRight, Format = "N0" }
            });

            // Cột 6: Giá vốn
            dgvHangHoa.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "colGiaVon",
                DataPropertyName = "GiaVon",
                HeaderText = "Giá vốn",
                Width = 120,
                DefaultCellStyle = new DataGridViewCellStyle { Alignment = DataGridViewContentAlignment.MiddleRight, Format = "N0" }
            });

            // Cột 7: Tồn kho
            dgvHangHoa.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "colTonKho",
                DataPropertyName = "SoLuongTon",
                HeaderText = "Tồn kho",
                Width = 100,
                DefaultCellStyle = new DataGridViewCellStyle { Alignment = DataGridViewContentAlignment.MiddleRight, Format = "N0" }
            });

            // Cột 8: Khách đặt (Mặc định ẩn khi mới load, bật tắt qua form Hiện cột)
            dgvHangHoa.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "colKhachDat",
                DataPropertyName = "KhachDat",
                HeaderText = "Khách đặt",
                Width = 95,
                Visible = false,
                DefaultCellStyle = new DataGridViewCellStyle { Alignment = DataGridViewContentAlignment.MiddleRight, Format = "N0" }
            });

            // Cột 9: Dự kiến hết hàng (Mặc định ẩn khi mới load)
            dgvHangHoa.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "colDuKienHetHang",
                DataPropertyName = "DuKienHetHang",
                HeaderText = "Dự kiến hết",
                Width = 110,
                Visible = false,
                DefaultCellStyle = new DataGridViewCellStyle { Alignment = DataGridViewContentAlignment.MiddleCenter }
            });

            // Cột 9: Vị trí (Mặc định ẩn khi mới load)
            dgvHangHoa.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "colViTri",
                DataPropertyName = "ViTri",
                HeaderText = "Vị trí",
                Width = 90,
                Visible = false,
                DefaultCellStyle = new DataGridViewCellStyle { Alignment = DataGridViewContentAlignment.MiddleCenter }
            });

            // Cột 10: Thời gian tạo (Mặc định ẩn khi mới load)
            dgvHangHoa.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "colThoiGianTao",
                DataPropertyName = "NgayTao",
                HeaderText = "Thời gian tạo",
                Width = 140,
                Visible = false,
                DefaultCellStyle = new DataGridViewCellStyle 
                { 
                    Alignment = DataGridViewContentAlignment.MiddleCenter,
                    Format = "dd/MM/yyyy HH:mm"
                }
            });

            // Cột 11: Trạng thái (Mặc định ẩn khi mới load)
            dgvHangHoa.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "colTrangThai",
                DataPropertyName = "TrangThai",
                HeaderText = "Trạng thái",
                Width = 120,
                Visible = false,
                DefaultCellStyle = new DataGridViewCellStyle { Alignment = DataGridViewContentAlignment.MiddleCenter }
            });
        }

        private void NapDuLieuBoLoc()
        {
            Action locDuLieu = () =>
            {
                if (isLoaded)
                {
                    trangHienTai = 1;
                    HienThiDanhSach();
                }
            };

            // 1. Nhóm hàng
            NapDanhSachNhomHang(locDuLieu);

            // 2. Đơn vị tính
            NapDanhSachDonViTinh(locDuLieu);

            // 2. Tiêu chí tồn
            List<string> listTieuChiTon = new List<string> {
                "-- Tất cả tồn kho --",
                "Còn hàng (> 0)",
                "Hết hàng (= 0)",
                "Dưới định mức tồn"
            };
            ComboboxHelper.CaiDatTimKiem(cboTieuChiTon, listTieuChiTon, locDuLieu);

            // 3. Nhà cung cấp
            try
            {
                DataTable dtNCC = DataProcessing.ReadData("SELECT MaDonViCapHang, TenDonVi FROM DonViCapHang WHERE TrangThai = 'HOAT_DONG' ORDER BY TenDonVi");
                DataRow drAllNCC = dtNCC.NewRow();
                drAllNCC["MaDonViCapHang"] = 0;
                drAllNCC["TenDonVi"] = "-- Tất cả nhà cung cấp --";
                dtNCC.Rows.InsertAt(drAllNCC, 0);

                ComboboxHelper.CaiDatTimKiem(cboNhaCungCap, dtNCC, "TenDonVi", "MaDonViCapHang", locDuLieu);
                cboNhaCungCap.SelectedIndex = 0;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi tải danh mục nhà cung cấp: " + ex.Message, "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

            // 4. Vị trí
            NapDanhSachViTri(locDuLieu);

            // 5. Trạng thái
            List<string> listTrangThai = new List<string> {
                "-- Tất cả trạng thái --",
                "Đang kinh doanh",
                "Ngừng kinh doanh"
            };
            ComboboxHelper.CaiDatTimKiem(cboTrangThai, listTrangThai, locDuLieu);

            // 6. Số dòng mỗi trang
            if (cboSoDongMoiTrang.Items.Count > 0 && cboSoDongMoiTrang.SelectedIndex < 0)
            {
                cboSoDongMoiTrang.SelectedIndex = 1; // 20 dòng
            }

            // 7. Khoảng thời gian tạo (Mặc định từ đầu năm đến ngày hiện tại)
            dtpTuNgay.Value = new DateTime(DateTime.Now.Year, 1, 1);
            dtpDenNgay.Value = DateTime.Today;
        }

        #endregion

        #region Tải dữ liệu & Lọc dữ liệu

        public void HienThiDanhSach()
        {
            if (!isLoaded) return;

            try
            {
                List<SqlParameter> parameters = new List<SqlParameter>();
                StringBuilder whereSql = new StringBuilder(" WHERE 1=1 ");

                // Tham số kho hiện tại
                int currentMaKho = SessionContext.MaKhoHienTai;
                parameters.Add(new SqlParameter("@MaKho", currentMaKho));

                // Lọc từ khóa (Mã hàng, tên hàng, quy cách)
                if (!string.IsNullOrWhiteSpace(txtTimKiem.Text))
                {
                    whereSql.Append(" AND (h.MaHangNoiBo LIKE @Search OR h.TenHang LIKE @Search OR h.QuyCach LIKE @Search) ");
                    parameters.Add(new SqlParameter("@Search", "%" + txtTimKiem.Text.Trim() + "%"));
                }

                // Lọc nhóm hàng
                if (cboNhomHang.SelectedValue != null && int.TryParse(cboNhomHang.SelectedValue.ToString(), out int maNhom) && maNhom > 0)
                {
                    whereSql.Append(" AND h.MaNhom = @MaNhom ");
                    parameters.Add(new SqlParameter("@MaNhom", maNhom));
                }

                // Lọc đơn vị tính
                if (cboDonViTinh.SelectedValue != null && int.TryParse(cboDonViTinh.SelectedValue.ToString(), out int maDVT) && maDVT > 0)
                {
                    whereSql.Append(" AND h.MaDVT = @MaDVT ");
                    parameters.Add(new SqlParameter("@MaDVT", maDVT));
                }

                // Lọc nhà cung cấp (hàng từng nhập từ NCC này)
                if (cboNhaCungCap.SelectedValue != null && int.TryParse(cboNhaCungCap.SelectedValue.ToString(), out int maNCC) && maNCC > 0)
                {
                    whereSql.Append(@" AND EXISTS (
                        SELECT 1 FROM ChiTietPhieuNhap ctpn 
                        JOIN PhieuNhap pn ON ctpn.MaPhieuNhap = pn.MaPhieuNhap 
                        WHERE ctpn.MaHang = h.MaHang AND pn.MaDonViCapHang = @MaNCC
                    ) ");
                    parameters.Add(new SqlParameter("@MaNCC", maNCC));
                }

                // Lọc vị trí
                if (cboViTri.SelectedIndex > 0 && cboViTri.SelectedItem != null)
                {
                    whereSql.Append(" AND h.ViTri = @ViTri ");
                    parameters.Add(new SqlParameter("@ViTri", cboViTri.SelectedItem.ToString()));
                }

                // Lọc trạng thái
                if (cboTrangThai.SelectedIndex == 1) // Đang kinh doanh
                {
                    whereSql.Append(" AND h.TrangThai = 'DANG_BAN' ");
                }
                else if (cboTrangThai.SelectedIndex == 2) // Ngừng kinh doanh
                {
                    whereSql.Append(" AND h.TrangThai = 'NGUNG_BAN' ");
                }

                // Lọc theo khoảng ngày tạo
                whereSql.Append(" AND h.NgayTao >= @TuNgay AND h.NgayTao < @DenNgay ");
                parameters.Add(new SqlParameter("@TuNgay", dtpTuNgay.Value.Date));
                parameters.Add(new SqlParameter("@DenNgay", dtpDenNgay.Value.Date.AddDays(1)));

                // Lọc tiêu chí tồn
                if (cboTieuChiTon.SelectedIndex == 1) // Còn hàng (> 0)
                {
                    whereSql.Append(" AND ISNULL(tk.SoLuongTon, 0) > 0 ");
                }
                else if (cboTieuChiTon.SelectedIndex == 2) // Hết hàng (= 0)
                {
                    whereSql.Append(" AND ISNULL(tk.SoLuongTon, 0) <= 0 ");
                }
                else if (cboTieuChiTon.SelectedIndex == 3) // Dưới định mức tồn
                {
                    whereSql.Append(" AND ISNULL(tk.SoLuongTon, 0) < ISNULL(ntk.TonToiThieu, 0) ");
                }

                // 1. Đếm tổng số bản ghi
                string joinTonKho = currentMaKho > 0 ? "LEFT JOIN vw_TonKhoHienTai tk ON h.MaHang = tk.MaHang AND tk.MaKho = @MaKho" : "LEFT JOIN (SELECT MaHang, SUM(SoLuongTon) AS SoLuongTon FROM vw_TonKhoHienTai GROUP BY MaHang) tk ON h.MaHang = tk.MaHang";
                string joinNguongTon = currentMaKho > 0 ? "LEFT JOIN NguongTonKho ntk ON h.MaHang = ntk.MaHang AND ntk.MaKho = @MaKho" : "LEFT JOIN (SELECT MaHang, MAX(TonToiThieu) AS TonToiThieu FROM NguongTonKho GROUP BY MaHang) ntk ON h.MaHang = ntk.MaHang";


                string sqlCount = $@"
                    SELECT COUNT(DISTINCT h.MaHang)
                    FROM HangHoa h
                    LEFT JOIN NhomHang nh ON h.MaNhom = nh.MaNhom
                    LEFT JOIN DonViTinh dvt ON h.MaDVT = dvt.MaDVT
                    {joinTonKho}
                    {joinNguongTon}
                    {whereSql};";

                SqlParameter[] countParams = new SqlParameter[parameters.Count];
                for (int i = 0; i < parameters.Count; i++)
                {
                    countParams[i] = new SqlParameter(parameters[i].ParameterName, parameters[i].Value);
                }

                object resultCount = DataProcessing.ReadValue(sqlCount, countParams);
                tongSoBanGhi = resultCount != null && int.TryParse(resultCount.ToString(), out int c) ? c : 0;

                // 2. Tính lại phân trang nếu vượt trang
                tongSoTrang = (int)Math.Ceiling((double)tongSoBanGhi / soDongMoiTrang);
                if (tongSoTrang <= 0) tongSoTrang = 1;
                if (trangHienTai > tongSoTrang) trangHienTai = tongSoTrang;

                int skip = (trangHienTai - 1) * soDongMoiTrang;
                parameters.Add(new SqlParameter("@Skip", skip));
                parameters.Add(new SqlParameter("@Take", soDongMoiTrang));

                // 3. Lấy dữ liệu phân trang kèm tốc độ bán 30 ngày và Dự kiến hết hàng
                string sqlData = $@"
                    SELECT h.MaHang, h.HinhAnh, h.MaHangNoiBo, h.TenHang, nh.TenNhom, dvt.TenDVT,
                           h.GiaBanThamKhao, h.GiaVon, ISNULL(tk.SoLuongTon, 0) AS SoLuongTon,
                           ISNULL(kd.SoLuongKhachDat, 0) AS KhachDat,
                           CASE 
                               WHEN ISNULL(tk.SoLuongTon, 0) <= 0 THEN N'Hết hàng'
                               WHEN ISNULL(x30.XuatBan30Ngay, 0) <= 0 THEN N'> 90 ngày'
                               WHEN CAST(ISNULL(tk.SoLuongTon, 0) AS FLOAT) / (CAST(x30.XuatBan30Ngay AS FLOAT) / 30.0) > 90 THEN N'> 90 ngày'
                               ELSE CONCAT(CAST(ROUND(CAST(ISNULL(tk.SoLuongTon, 0) AS FLOAT) / (CAST(x30.XuatBan30Ngay AS FLOAT) / 30.0), 0) AS INT), N' ngày')
                           END AS DuKienHetHang,
                           h.ViTri, h.NgayTao, h.TrangThai, ISNULL(ntk.TonToiThieu, 0) AS TonToiThieu
                    FROM HangHoa h
                    LEFT JOIN NhomHang nh ON h.MaNhom = nh.MaNhom
                    LEFT JOIN DonViTinh dvt ON h.MaDVT = dvt.MaDVT
                    {joinTonKho}
                    {joinNguongTon}
                    LEFT JOIN (
                        SELECT ctdb.MaHang, SUM(ctdb.SoLuong) AS SoLuongKhachDat
                        FROM DonBan db
                        JOIN ChiTietDonBan ctdb ON db.MaDonBan = ctdb.MaDonBan
                        WHERE (@MaKho = 0 OR db.MaKho = @MaKho)
                          AND db.TrangThai NOT IN ('HOAN_TAT', 'HUY')
                        GROUP BY ctdb.MaHang
                    ) kd ON h.MaHang = kd.MaHang
                    LEFT JOIN (
                        SELECT MaHang, SUM(SoLuongGiam) AS XuatBan30Ngay
                        FROM vw_BienDongKho
                        WHERE (@MaKho = 0 OR MaKho = @MaKho)
                          AND SoLuongGiam > 0
                          AND NgayGiaoDich >= DATEADD(day, -30, GETDATE())
                        GROUP BY MaHang
                    ) x30 ON h.MaHang = x30.MaHang
                    {whereSql}
                    ORDER BY h.MaHang DESC
";

                // Lưu lại truy vấn và tham số đầy đủ (không phân trang) để xuất file toàn bộ kết quả lọc
                sqlXuatFile = sqlData;
                thamSoXuatFile = countParams;

                string sqlDataPhanTrang = sqlData + "\n                    OFFSET @Skip ROWS FETCH NEXT @Take ROWS ONLY;";

                DataTable dtHangHoa = DataProcessing.ReadData(sqlDataPhanTrang, parameters.ToArray());

                // Thêm cột logic Chon vào DataTable để DataGridViewCheckBoxColumn bind chuẩn xác
                if (!dtHangHoa.Columns.Contains("Chon"))
                {
                    dtHangHoa.Columns.Add("Chon", typeof(bool));
                    dtHangHoa.Columns["Chon"].DefaultValue = false;
                    foreach (DataRow r in dtHangHoa.Rows)
                    {
                        r["Chon"] = false;
                    }
                }

                dgvHangHoa.DataSource = dtHangHoa;

                // Reset trạng thái Header CheckBox khi nạp trang mới
                isHeaderChecked = false;
                if (dgvHangHoa.Columns.Contains("colChon"))
                {
                    dgvHangHoa.InvalidateCell(dgvHangHoa.Columns["colChon"].Index, -1);
                }

                // Cố định chiều cao dòng 48px để ô cột hình ảnh luôn tạo thành hình vuông 48x48
                foreach (DataGridViewRow r in dgvHangHoa.Rows)
                {
                    r.Height = 48;
                }

                // 4. Cập nhật thanh phân trang
                CapNhatGiaoDienPhanTrang();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi tải danh sách hàng hóa: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void CapNhatGiaoDienPhanTrang()
        {
            lblTrangHienTai.Text = $"Trang {trangHienTai} / {tongSoTrang}";
            lblTongSoHang.Text = $"Tổng số: {tongSoBanGhi:N0} hàng";

            btnTrangTruoc.Enabled = (trangHienTai > 1);
            btnTrangSau.Enabled = (trangHienTai < tongSoTrang);
        }

        #endregion

        #region Sự kiện Tìm kiếm, Lọc & Phân trang

        private void txtTimKiem_TextChanged(object sender, EventArgs e)
        {
            searchDebounceTimer?.Stop();
            searchDebounceTimer?.Start();
        }

        private void BoLoc_SelectedIndexChanged(object sender, EventArgs e)
        {
            trangHienTai = 1;
            HienThiDanhSach();
        }

        private void btnTrangTruoc_Click(object sender, EventArgs e)
        {
            if (trangHienTai > 1)
            {
                trangHienTai--;
                HienThiDanhSach();
            }
        }

        private void btnTrangSau_Click(object sender, EventArgs e)
        {
            if (trangHienTai < tongSoTrang)
            {
                trangHienTai++;
                HienThiDanhSach();
            }
        }

        private void cboSoDongMoiTrang_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (int.TryParse(cboSoDongMoiTrang.SelectedItem?.ToString(), out int soDong))
            {
                soDongMoiTrang = soDong;
                trangHienTai = 1;
                HienThiDanhSach();
            }
        }

        private void dgvHangHoa_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.RowIndex < 0 || e.RowIndex >= dgvHangHoa.Rows.Count) return;

            string colName = dgvHangHoa.Columns[e.ColumnIndex].Name;

            // Định dạng hiển thị trạng thái
            if (colName == "colTrangThai" && e.Value != null)
            {
                string val = e.Value.ToString();
                if (val == "DANG_BAN")
                {
                    e.Value = "Đang bán";
                    e.CellStyle.ForeColor = Color.ForestGreen;
                }
                else if (val == "NGUNG_BAN")
                {
                    e.Value = "Ngừng bán";
                    e.CellStyle.ForeColor = Color.Gray;
                }
            }

            // Cảnh báo hết hàng hoặc âm tồn
            if (colName == "colTonKho" && e.Value != null)
            {
                if (decimal.TryParse(e.Value.ToString(), out decimal ton))
                {
                    if (ton <= 0)
                    {
                        e.CellStyle.ForeColor = Color.Red;
                        e.CellStyle.Font = new Font(dgvHangHoa.Font, FontStyle.Bold);
                    }
                }
            }
        }

        // Vẽ Header Checkbox và ô cột hình ảnh thành khung hình vuông
        private void dgvHangHoa_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        {
            // 1. Vẽ Header CheckBox cho cột colChon
            if (e.RowIndex == -1 && e.ColumnIndex >= 0 && dgvHangHoa.Columns[e.ColumnIndex].Name == "colChon")
            {
                e.PaintBackground(e.CellBounds, true);

                int boxSize = 14;
                int x = e.CellBounds.X + (e.CellBounds.Width - boxSize) / 2;
                int y = e.CellBounds.Y + (e.CellBounds.Height - boxSize) / 2;
                Rectangle rect = new Rectangle(x, y, boxSize, boxSize);

                if (Application.RenderWithVisualStyles)
                {
                    CheckBoxState state = isHeaderChecked
                        ? CheckBoxState.CheckedNormal
                        : CheckBoxState.UncheckedNormal;
                    CheckBoxRenderer.DrawCheckBox(e.Graphics, new Point(x, y), state);
                }
                else
                {
                    ControlPaint.DrawCheckBox(e.Graphics, rect, isHeaderChecked ? ButtonState.Checked : ButtonState.Normal);
                }

                e.Handled = true;
                return;
            }

            // 2. Vẽ ô cột hình ảnh thành một khung hình vuông hoàn hảo
            if (e.RowIndex >= 0 && e.ColumnIndex >= 0 && dgvHangHoa.Columns[e.ColumnIndex].Name == "colHinhAnh")
            {
                // Vẽ màu nền dòng (màu xen kẽ hoặc màu khi click chọn dòng)
                e.PaintBackground(e.CellBounds, true);

                // Tính toán kích thước cạnh hình vuông căn giữa ô
                int squareSize = Math.Min(e.CellBounds.Width, e.CellBounds.Height) - 8;
                if (squareSize > 10)
                {
                    int squareX = e.CellBounds.X + (e.CellBounds.Width - squareSize) / 2;
                    int squareY = e.CellBounds.Y + (e.CellBounds.Height - squareSize) / 2;
                    Rectangle squareRect = new Rectangle(squareX, squareY, squareSize, squareSize);

                    // Vẽ khung hình vuông nền sáng viền mỏng thanh lịch
                    using (SolidBrush bgBrush = new SolidBrush(Color.FromArgb(248, 250, 252)))
                    using (Pen borderPen = new Pen(Color.FromArgb(226, 232, 240), 1))
                    {
                        e.Graphics.FillRectangle(bgBrush, squareRect);
                        e.Graphics.DrawRectangle(borderPen, squareRect);
                    }

                    // Nếu ô có dữ liệu ảnh thì vẽ ảnh vào trong khung vuông
                    if (e.Value != null && e.Value != DBNull.Value)
                    {
                        Image img = null;
                        if (e.Value is Image i)
                        {
                            img = i;
                        }
                        else if (e.Value is byte[] bytes && bytes.Length > 0)
                        {
                            try
                            {
                                using (MemoryStream ms = new MemoryStream(bytes))
                                {
                                    img = Image.FromStream(ms);
                                }
                            }
                            catch { }
                        }

                        if (img != null)
                        {
                            int pad = 2;
                            int maxW = squareSize - pad * 2;
                            int maxH = squareSize - pad * 2;
                            float scale = Math.Min((float)maxW / img.Width, (float)maxH / img.Height);
                            int drawW = Math.Max(1, (int)(img.Width * scale));
                            int drawH = Math.Max(1, (int)(img.Height * scale));
                            int drawX = squareRect.X + (squareRect.Width - drawW) / 2;
                            int drawY = squareRect.Y + (squareRect.Height - drawH) / 2;

                            e.Graphics.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
                            e.Graphics.DrawImage(img, new Rectangle(drawX, drawY, drawW, drawH));
                            img.Dispose();
                        }
                    }
                }

                e.Handled = true;
            }
        }

        private void dgvHangHoa_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.ColumnIndex >= 0 && dgvHangHoa.Columns[e.ColumnIndex].Name == "colChon")
            {
                bool currentVal = Convert.ToBoolean(dgvHangHoa.Rows[e.RowIndex].Cells["colChon"].Value ?? false);
                bool newVal = !currentVal;
                dgvHangHoa.Rows[e.RowIndex].Cells["colChon"].Value = newVal;
                if (dgvHangHoa.DataSource is DataTable dt && e.RowIndex < dt.Rows.Count)
                {
                    dt.Rows[e.RowIndex]["Chon"] = newVal;
                }
                CapNhatTrangThaiHeaderCheckBox();
            }
        }

        private void dgvHangHoa_ColumnHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.ColumnIndex >= 0 && dgvHangHoa.Columns[e.ColumnIndex].Name == "colChon")
            {
                if (dgvHangHoa.Rows.Count == 0) return;

                isHeaderChecked = !isHeaderChecked;
                foreach (DataGridViewRow row in dgvHangHoa.Rows)
                {
                    row.Cells["colChon"].Value = isHeaderChecked;
                }
                if (dgvHangHoa.DataSource is DataTable dt)
                {
                    foreach (DataRow dr in dt.Rows)
                    {
                        dr["Chon"] = isHeaderChecked;
                    }
                }
                dgvHangHoa.InvalidateCell(e.ColumnIndex, -1);
            }
        }

        private void dgvHangHoa_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Space && dgvHangHoa.CurrentRow != null && dgvHangHoa.CurrentRow.Index >= 0)
            {
                var cell = dgvHangHoa.CurrentRow.Cells["colChon"];
                if (cell != null)
                {
                    bool currentVal = Convert.ToBoolean(cell.Value ?? false);
                    bool newVal = !currentVal;
                    cell.Value = newVal;
                    if (dgvHangHoa.DataSource is DataTable dt && dgvHangHoa.CurrentRow.Index < dt.Rows.Count)
                    {
                        dt.Rows[dgvHangHoa.CurrentRow.Index]["Chon"] = newVal;
                    }
                    CapNhatTrangThaiHeaderCheckBox();
                    e.Handled = true;
                }
            }
        }

        private void CapNhatTrangThaiHeaderCheckBox()
        {
            if (dgvHangHoa.Rows.Count == 0)
            {
                isHeaderChecked = false;
                return;
            }

            bool allChecked = true;
            foreach (DataGridViewRow row in dgvHangHoa.Rows)
            {
                bool val = Convert.ToBoolean(row.Cells["colChon"].Value ?? false);
                if (!val)
                {
                    allChecked = false;
                    break;
                }
            }

            if (isHeaderChecked != allChecked)
            {
                isHeaderChecked = allChecked;
                if (dgvHangHoa.Columns.Contains("colChon"))
                {
                    dgvHangHoa.InvalidateCell(dgvHangHoa.Columns["colChon"].Index, -1);
                }
            }
        }

        /// <summary>
        /// Trả về danh sách MaHang của các dòng đang được tích chọn CheckBox
        /// </summary>
        public List<int> LayDanhSachMaHangDaChon()
        {
            List<int> ketQua = new List<int>();
            foreach (DataGridViewRow row in dgvHangHoa.Rows)
            {
                if (Convert.ToBoolean(row.Cells["colChon"].Value ?? false))
                {
                    var valMaHang = row.Cells["colMaHang"].Value;
                    if (valMaHang != null && int.TryParse(valMaHang.ToString(), out int maHang))
                    {
                        ketQua.Add(maHang);
                    }
                }
            }
            return ketQua;
        }

        #endregion

        #region Hành động & Nút chức năng

        // Click đúp vào dòng để mở form sửa
        private void dgvHangHoa_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;
            if (e.ColumnIndex >= 0 && dgvHangHoa.Columns[e.ColumnIndex].Name == "colChon") return;

            var cellMaHang = dgvHangHoa.Rows[e.RowIndex].Cells["colMaHang"].Value;
            if (cellMaHang != null && int.TryParse(cellMaHang.ToString(), out int maHang))
            {
                MoFormHangHoa(maHang);
            }
        }

        // Bấm nút dấu cộng bên cạnh đơn vị tính để mở frmDonViTinh
        private void btnThemDonViTinh_Click(object sender, EventArgs e)
        {
            using (frmDonViTinh frm = new frmDonViTinh())
            {
                frm.StartPosition = FormStartPosition.CenterParent;
                frm.ShowDialog(this);
                NapDanhSachDonViTinh();
            }
        }

        public void NapDanhSachDonViTinh(Action locDuLieu = null)
        {
            try
            {
                object selectedVal = cboDonViTinh.SelectedValue;
                DataTable dtDvt = DataProcessing.ReadData("SELECT MaDVT, TenDVT FROM DonViTinh ORDER BY TenDVT");
                DataRow drAllDvt = dtDvt.NewRow();
                drAllDvt["MaDVT"] = 0;
                drAllDvt["TenDVT"] = "-- Tất cả đơn vị tính --";
                dtDvt.Rows.InsertAt(drAllDvt, 0);

                Action action = locDuLieu ?? (() =>
                {
                    if (isLoaded)
                    {
                        trangHienTai = 1;
                        HienThiDanhSach();
                    }
                });

                ComboboxHelper.CaiDatTimKiem(cboDonViTinh, dtDvt, "TenDVT", "MaDVT", action);

                if (selectedVal != null && int.TryParse(selectedVal.ToString(), out int val) && val > 0)
                {
                    cboDonViTinh.SelectedValue = val;
                }
                else
                {
                    cboDonViTinh.SelectedIndex = 0;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi tải danh mục đơn vị tính: " + ex.Message, "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
        // Bấm nút dấu cộng bên cạnh nhóm hàng để mở frmNhomHang
        private void btnThemNhomHang_Click(object sender, EventArgs e)
        {
            using (frmNhomHang frm = new frmNhomHang())
            {
                frm.StartPosition = FormStartPosition.CenterParent;
                frm.ShowDialog(this);
                NapDanhSachNhomHang();
            }
        }

        public void NapDanhSachNhomHang(Action locDuLieu = null)
        {
            try
            {
                object selectedVal = cboNhomHang.SelectedValue;
                DataTable dtNhom = DataProcessing.ReadData("SELECT MaNhom, TenNhom FROM NhomHang WHERE TrangThai = 'HOAT_DONG' ORDER BY TenNhom");
                DataRow drAllNhom = dtNhom.NewRow();
                drAllNhom["MaNhom"] = 0;
                drAllNhom["TenNhom"] = "-- Tất cả nhóm hàng --";
                dtNhom.Rows.InsertAt(drAllNhom, 0);

                Action action = locDuLieu ?? (() =>
                {
                    if (isLoaded)
                    {
                        trangHienTai = 1;
                        HienThiDanhSach();
                    }
                });

                ComboboxHelper.CaiDatTimKiem(cboNhomHang, dtNhom, "TenNhom", "MaNhom", action);

                if (selectedVal != null && int.TryParse(selectedVal.ToString(), out int val) && val > 0)
                {
                    cboNhomHang.SelectedValue = val;
                }
                else
                {
                    cboNhomHang.SelectedIndex = 0;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi tải danh mục nhóm hàng: " + ex.Message, "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        // Bấm nút dấu cộng bên cạnh vị trí để mở frmViTri
        private void btnThemViTri_Click(object sender, EventArgs e)
        {
            using (frmViTri frm = new frmViTri())
            {
                frm.StartPosition = FormStartPosition.CenterParent;
                frm.ShowDialog(this);
                NapDanhSachViTri();
            }
        }

        public void NapDanhSachViTri(Action locDuLieu = null)
        {
            try
            {
                string selectedVal = cboViTri.SelectedItem != null ? cboViTri.SelectedItem.ToString() : null;
                DataTable dtViTri = DataProcessing.ReadData("SELECT TenViTri FROM ViTri WHERE TrangThai = 'HOAT_DONG' ORDER BY TenViTri");
                List<string> listViTri = new List<string> { "-- Tất cả vị trí --" };
                foreach (DataRow row in dtViTri.Rows)
                {
                    listViTri.Add(row["TenViTri"].ToString());
                }

                Action action = locDuLieu ?? (() =>
                {
                    if (isLoaded)
                    {
                        trangHienTai = 1;
                        HienThiDanhSach();
                    }
                });

                ComboboxHelper.CaiDatTimKiem(cboViTri, listViTri, action);

                if (!string.IsNullOrEmpty(selectedVal) && listViTri.Contains(selectedVal))
                {
                    cboViTri.SelectedItem = selectedVal;
                }
                else
                {
                    cboViTri.SelectedIndex = 0;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi tải danh mục vị trí: " + ex.Message, "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        // Bấm nút + Hàng hóa mở form thêm mới
        
        // Bấm nút Nhập file mở form nhập hàng hóa từ file
        private void btnNhapFile_Click(object sender, EventArgs e)
        {
            using (frmNhapHangHoaFile frm = new frmNhapHangHoaFile())
            {
                frm.StartPosition = FormStartPosition.CenterParent;
                if (frm.ShowDialog(this) == DialogResult.OK)
                {
                    NapDuLieuBoLoc();
                    HienThiDanhSach();
                }
            }
        }
private void btnHangHoa_Click(object sender, EventArgs e)
        {
            MoFormHangHoa(0);
        }

        private void MoFormHangHoa(int maHang)
        {
            using (frmHangHoa frm = maHang > 0 ? new frmHangHoa(maHang) : new frmHangHoa())
            {
                frm.StartPosition = FormStartPosition.CenterParent;
                if (frm.ShowDialog(this) == DialogResult.OK)
                {
                    HienThiDanhSach();
                }
            }
        }

                // Xuất danh sách ra file CSV / Excel
        private void btnXuatFile_Click(object sender, EventArgs e)
        {
            if (dgvHangHoa.Rows.Count == 0 && tongSoBanGhi == 0)
            {
                MessageBox.Show("Không có dữ liệu để xuất file!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            // Xác định danh sách dòng cần xuất: Ưu tiên các dòng được tích chọn CheckBox
            List<DataGridViewRow> danhSachChon = new List<DataGridViewRow>();
            foreach (DataGridViewRow row in dgvHangHoa.Rows)
            {
                if (Convert.ToBoolean(row.Cells["colChon"].Value ?? false))
                {
                    danhSachChon.Add(row);
                }
            }

            bool coDongChon = danhSachChon.Count > 0;

            // Lấy danh sách cột hiển thị cần xuất (bỏ cột CheckBox và cột Hình ảnh)
            List<DataGridViewColumn> colsXuat = new List<DataGridViewColumn>();
            foreach (DataGridViewColumn col in dgvHangHoa.Columns)
            {
                if (col.Visible && !(col is DataGridViewImageColumn) && !(col is DataGridViewCheckBoxColumn))
                {
                    colsXuat.Add(col);
                }
            }

            if (colsXuat.Count == 0)
            {
                MessageBox.Show("Không có cột nào đang hiển thị để xuất dữ liệu!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Thu thập dữ liệu các dòng cần xuất
            List<string[]> dataRows = new List<string[]>();

            if (coDongChon)
            {
                // Người dùng tích chọn cụ thể các dòng trên trang: xuất các dòng đã chọn
                foreach (DataGridViewRow row in danhSachChon)
                {
                    string[] cells = new string[colsXuat.Count];
                    for (int i = 0; i < colsXuat.Count; i++)
                    {
                        cells[i] = row.Cells[colsXuat[i].Index].FormattedValue?.ToString() ?? "";
                    }
                    dataRows.Add(cells);
                }
            }
            else
            {
                // Người dùng không tích chọn dòng nào: xuất toàn bộ danh sách thỏa mãn bộ lọc hiện tại
                DataTable dtTatCa = null;
                if (!string.IsNullOrEmpty(sqlXuatFile) && thamSoXuatFile != null)
                {
                    List<SqlParameter> ps = new List<SqlParameter>();
                    foreach (var p in thamSoXuatFile)
                    {
                        ps.Add(new SqlParameter(p.ParameterName, p.Value));
                    }
                    dtTatCa = DataProcessing.ReadData(sqlXuatFile, ps.ToArray());
                }

                if (dtTatCa != null && dtTatCa.Rows.Count > 0)
                {
                    foreach (DataRow dr in dtTatCa.Rows)
                    {
                        string[] cells = new string[colsXuat.Count];
                        for (int i = 0; i < colsXuat.Count; i++)
                        {
                            var col = colsXuat[i];
                            string prop = col.DataPropertyName;
                            object val = (!string.IsNullOrEmpty(prop) && dtTatCa.Columns.Contains(prop)) ? dr[prop] : null;

                            if (val == null || val == DBNull.Value)
                            {
                                cells[i] = "";
                            }
                            else if (col.Name == "colTrangThai")
                            {
                                string sVal = val.ToString();
                                cells[i] = (sVal == "DANG_BAN") ? "Đang bán" : (sVal == "NGUNG_BAN" ? "Ngừng bán" : sVal);
                            }
                            else if (col.Name == "colThoiGianTao" && DateTime.TryParse(val.ToString(), out DateTime dtVal))
                            {
                                cells[i] = dtVal.ToString("dd/MM/yyyy HH:mm");
                            }
                            else if (col.DefaultCellStyle.Format == "N0" && decimal.TryParse(val.ToString(), out decimal numVal))
                            {
                                cells[i] = numVal.ToString("N0");
                            }
                            else
                            {
                                cells[i] = val.ToString();
                            }
                        }
                        dataRows.Add(cells);
                    }
                }
                else
                {
                    // Dự phòng nếu dtTatCa rỗng thì lấy từ các dòng đang có trên DataGridView
                    foreach (DataGridViewRow row in dgvHangHoa.Rows)
                    {
                        string[] cells = new string[colsXuat.Count];
                        for (int i = 0; i < colsXuat.Count; i++)
                        {
                            cells[i] = row.Cells[colsXuat[i].Index].FormattedValue?.ToString() ?? "";
                        }
                        dataRows.Add(cells);
                    }
                }
            }

            if (dataRows.Count == 0)
            {
                MessageBox.Show("Không có dữ liệu để xuất file!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            using (SaveFileDialog sfd = new SaveFileDialog())
            {
                sfd.Title = coDongChon ? $"Xuất {dataRows.Count} hàng hóa đã chọn" : "Xuất danh sách hàng hóa";
                sfd.Filter = "File Excel (*.xls)|*.xls|File CSV (*.csv)|*.csv";
                sfd.FileName = coDongChon 
                    ? $"DanhSachHangHoa_DaChon_{DateTime.Now:yyyyMMdd_HHmm}.xls" 
                    : $"DanhSachHangHoa_{DateTime.Now:yyyyMMdd_HHmm}.xls";

                if (sfd.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        string ext = Path.GetExtension(sfd.FileName).ToLower();

                        if (ext == ".xls")
                        {
                            // Xuất định dạng bảng tính Excel (HTML Table UTF-8): Mở bằng Excel tự động chia từng cột, kẻ viền, căn lề chuẩn
                            StringBuilder sb = new StringBuilder();
                            sb.AppendLine("<!DOCTYPE html>");
                            sb.AppendLine("<html><head><meta http-equiv=\"content-type\" content=\"application/vnd.ms-excel; charset=UTF-8\" />");
                            sb.AppendLine("<style>");
                            sb.AppendLine("table { border-collapse: collapse; font-family: Segoe UI, Tahoma, sans-serif; font-size: 11pt; }");
                            sb.AppendLine("th { background-color: #E2E8F0; color: #0F172A; font-weight: bold; border: 1px solid #94A3B8; padding: 8px 12px; }");
                            sb.AppendLine("td { border: 1px solid #CBD5E1; padding: 6px 10px; }");
                            sb.AppendLine(".text { mso-number-format: '\\@'; text-align: left; }");
                            sb.AppendLine(".center { text-align: center; }");
                            sb.AppendLine(".number { mso-number-format: '#,##0'; text-align: right; }");
                            sb.AppendLine("</style></head><body>");
                            sb.AppendLine("<table>");

                            // Dòng tiêu đề cột
                            sb.AppendLine("<tr>");
                            foreach (var col in colsXuat)
                            {
                                sb.AppendLine($"<th>{System.Net.WebUtility.HtmlEncode(col.HeaderText)}</th>");
                            }
                            sb.AppendLine("</tr>");

                            // Dữ liệu từng dòng
                            foreach (var rowValues in dataRows)
                            {
                                sb.AppendLine("<tr>");
                                for (int i = 0; i < colsXuat.Count; i++)
                                {
                                    var col = colsXuat[i];
                                    string cellVal = rowValues[i];
                                    string cssClass = "text";
                                    if (col.DefaultCellStyle.Alignment == DataGridViewContentAlignment.MiddleRight)
                                        cssClass = "number";
                                    else if (col.DefaultCellStyle.Alignment == DataGridViewContentAlignment.MiddleCenter)
                                        cssClass = "center";

                                    sb.AppendLine($"<td class=\"{cssClass}\">{System.Net.WebUtility.HtmlEncode(cellVal)}</td>");
                                }
                                sb.AppendLine("</tr>");
                            }

                            sb.AppendLine("</table></body></html>");
                            File.WriteAllText(sfd.FileName, sb.ToString(), new UTF8Encoding(true));
                        }
                        else
                        {
                            // Xuất định dạng CSV kèm chỉ thị sep=, để Excel nhận diện chuẩn dấu phân cách cột
                            StringBuilder sb = new StringBuilder();
                            sb.AppendLine("sep=,");

                            List<string> headers = new List<string>();
                            foreach (var col in colsXuat)
                            {
                                headers.Add($"\"{col.HeaderText.Replace("\"", "\"\"")}\"");
                            }
                            sb.AppendLine(string.Join(",", headers));

                            foreach (var rowValues in dataRows)
                            {
                                List<string> rowValuesEscaped = new List<string>();
                                foreach (var val in rowValues)
                                {
                                    rowValuesEscaped.Add($"\"{val.Replace("\"", "\"\"")}\"");
                                }
                                sb.AppendLine(string.Join(",", rowValuesEscaped));
                            }

                            File.WriteAllText(sfd.FileName, sb.ToString(), new UTF8Encoding(true));
                        }

                        string thongBao = coDongChon
                            ? $"Xuất file thành công {dataRows.Count:N0} hàng hóa đã chọn tại:\n{sfd.FileName}"
                            : $"Xuất file thành công toàn bộ {dataRows.Count:N0} hàng hóa tại:\n{sfd.FileName}";
                        MessageBox.Show(thongBao, "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Lỗi khi xuất file: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        // Bấm nút Hiện cột mở form tùy chọn cột xuất hiện ngay dưới nút btnHienCot
        private void btnHienCot_Click(object sender, EventArgs e)
        {
            foreach (Form openForm in Application.OpenForms)
            {
                if (openForm is frmHienCot)
                {
                    openForm.Activate();
                    openForm.BringToFront();
                    return;
                }
            }

            frmHienCot frm = new frmHienCot(this.dgvHangHoa);
            frm.StartPosition = FormStartPosition.Manual;

            // Tính toạ độ hiển thị ngay phía dưới nút btnHienCot (căn thẳng mép phải nút)
            Point btnPos = btnHienCot.PointToScreen(Point.Empty);
            int posX = btnPos.X + btnHienCot.Width - frm.Width;
            int posY = btnPos.Y + btnHienCot.Height + 2;

            // Bảo đảm form không bị tràn ra ngoài màn hình làm việc
            Rectangle workingArea = Screen.FromControl(btnHienCot).WorkingArea;
            if (posX + frm.Width > workingArea.Right)
            {
                posX = workingArea.Right - frm.Width;
            }
            if (posX < workingArea.Left)
            {
                posX = workingArea.Left;
            }
            if (posY + frm.Height > workingArea.Bottom)
            {
                posY = btnPos.Y - frm.Height - 2;
            }

            frm.Location = new Point(posX, posY);
            frm.Show(this);
            frm.BringToFront();
        }

        // Hướng dẫn sử dụng
        private void btnHuongDanSuDung_Click(object sender, EventArgs e)
        {
            string huongDan = "HƯỚNG DẪN QUẢN LÝ DANH SÁCH HÀNG HÓA:\n\n" +
                              "1. Tìm kiếm: Nhập mã, tên hàng hoặc quy cách vào ô Tìm kiếm trên đầu.\n" +
                              "2. Bộ lọc: Chọn Nhóm hàng, Tồn kho, Nhà cung cấp, Vị trí hoặc Trạng thái ở bảng bên trái để lọc nhanh.\n" +
                              "3. Thêm mới: Nhấp vào nút '+ Hàng hóa'.\n" +
                              "4. Xem / Chỉnh sửa: Nhấp đúp chuột (Double click) vào bất kỳ dòng hàng hóa nào trên bảng.\n" +
                              "5. Phân trang: Nhấp nút 'Trước' / 'Sau' hoặc chọn lại số dòng mỗi trang ở góc dưới bảng.\n" +
                              "6. Xuất file: Nhấp 'Xuất file' để lưu danh sách ra file CSV mở bằng Excel.";

            MessageBox.Show(huongDan, "Hướng dẫn sử dụng", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        #endregion
    }
}
