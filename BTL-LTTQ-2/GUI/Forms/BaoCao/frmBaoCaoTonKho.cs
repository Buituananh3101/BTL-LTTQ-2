using BTL_LTTQ_2.Classes;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Reflection;
using System.Text;
using System.Windows.Forms;

namespace BTL_LTTQ_2.GUI.Forms.BaoCao
{
    public partial class frmBaoCaoTonKho : Form
    {
        private int trangHienTai = 1;
        private int soDongMoiTrang = 20;
        private int tongSoBanGhi = 0;
        private int tongSoTrang = 1;
        private bool dangNapDuLieu = false;

        // Biểu thức "dưới định mức" của lần tải gần nhất (dùng chung cho KPI)
        private string duoiDinhMucHienTai = "(1 = 0)";
        // Truy vấn đầy đủ (không phân trang) của lần tải gần nhất để xuất file toàn bộ kết quả lọc
        private string sqlXuatFile = null;
        private SqlParameter[] thamSoXuatFile = null;

        private Timer searchDebounceTimer;

        public frmBaoCaoTonKho()
        {
            InitializeComponent();
            KhoiTaoDebounceTimer();
            if (txtTimKiem != null)
            {
                txtTimKiem.KeyDown += txtTimKiem_KeyDown;
            }
        }

        private void KhoiTaoDebounceTimer()
        {
            searchDebounceTimer = new Timer();
            searchDebounceTimer.Interval = 300;
            searchDebounceTimer.Tick += (s, e) =>
            {
                searchDebounceTimer.Stop();
                if (!dangNapDuLieu)
                {
                    trangHienTai = 1;
                    TaiDuLieu();
                }
            };
        }

        private void txtTimKiem_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;
                searchDebounceTimer?.Stop();
                if (!dangNapDuLieu)
                {
                    trangHienTai = 1;
                    TaiDuLieu();
                }
            }
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (keyData == Keys.F3)
            {
                if (txtTimKiem != null && txtTimKiem.CanFocus)
                {
                    txtTimKiem.Focus();
                    txtTimKiem.SelectAll();
                    return true;
                }
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }

        private void frmBaoCaoTonKho_Load(object sender, EventArgs e)
        {
            // Bật đệm đôi chống nhấp nháy cho DataGridView
            try
            {
                typeof(DataGridView).InvokeMember("DoubleBuffered",
                    BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.SetProperty,
                    null, dgvBaoCaoTonKho, new object[] { true });
            }
            catch { }

            KhoiTaoCacCotLuoi();
            KhoiTaoBoLoc();
            TaiDuLieu();
            lblTongSoHang.BringToFront();
        }

        protected override void OnActivated(EventArgs e)
        {
            base.OnActivated(e);
            if (dgvBaoCaoTonKho != null && dgvBaoCaoTonKho.CurrentRow != null && !dgvBaoCaoTonKho.Focused)
            {
                dgvBaoCaoTonKho.Focus();
            }
        }

        private void KhoiTaoCacCotLuoi()
        {
            dgvBaoCaoTonKho.AutoGenerateColumns = false;
            dgvBaoCaoTonKho.Columns.Clear();
            dgvBaoCaoTonKho.BorderStyle = BorderStyle.None;

            dgvBaoCaoTonKho.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(248, 250, 252);
            dgvBaoCaoTonKho.DefaultCellStyle.SelectionBackColor = Color.FromArgb(224, 231, 255);
            dgvBaoCaoTonKho.DefaultCellStyle.SelectionForeColor = Color.FromArgb(30, 41, 59);

            // Cột ẩn: MaHang
            DataGridViewTextBoxColumn colMaHang = new DataGridViewTextBoxColumn
            {
                Name = "colMaHang",
                HeaderText = "",
                DataPropertyName = "MaHang",


                Visible = false
            };
            dgvBaoCaoTonKho.Columns.Add(colMaHang);

            // 1. Mã hàng
            DataGridViewTextBoxColumn colMaHangNoiBo = new DataGridViewTextBoxColumn
            {
                Name = "colMaHangNoiBo",
                HeaderText = "Mã hàng",
                DataPropertyName = "MaHangNoiBo",
                Width = 100
            };
            colMaHangNoiBo.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgvBaoCaoTonKho.Columns.Add(colMaHangNoiBo);

            // 2. Tên hàng hóa
            DataGridViewTextBoxColumn colTenHang = new DataGridViewTextBoxColumn
            {
                Name = "colTenHang",
                HeaderText = "Tên hàng hóa",
                DataPropertyName = "TenHang",
                Width = 230,
                AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill,
                MinimumWidth = 180
            };
            dgvBaoCaoTonKho.Columns.Add(colTenHang);

            // 3. ĐVT
            DataGridViewTextBoxColumn colTenDVT = new DataGridViewTextBoxColumn
            {
                Name = "colTenDVT",
                HeaderText = "ĐVT",
                DataPropertyName = "TenDVT",
                Width = 75
            };
            colTenDVT.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgvBaoCaoTonKho.Columns.Add(colTenDVT);

            // 4. Nhóm hàng
            DataGridViewTextBoxColumn colTenNhom = new DataGridViewTextBoxColumn
            {
                Name = "colTenNhom",
                HeaderText = "Nhóm hàng",
                DataPropertyName = "TenNhom",
                Width = 130
            };
            dgvBaoCaoTonKho.Columns.Add(colTenNhom);

            // 5. Chi nhánh kho
            DataGridViewTextBoxColumn colTenKho = new DataGridViewTextBoxColumn
            {
                Name = "colTenKho",
                HeaderText = "Kho",
                DataPropertyName = "TenKho",
                Width = 140,
                Visible = false
            };
            dgvBaoCaoTonKho.Columns.Add(colTenKho);

            // 6. Giá vốn
            DataGridViewTextBoxColumn colGiaVon = new DataGridViewTextBoxColumn
            {
                Name = "colGiaVon",
                HeaderText = "Giá vốn",
                DataPropertyName = "GiaVon",
                Width = 110
            };
            colGiaVon.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            colGiaVon.DefaultCellStyle.Format = "N0";
            dgvBaoCaoTonKho.Columns.Add(colGiaVon);

            // 7. Giá bán
            DataGridViewTextBoxColumn colGiaBan = new DataGridViewTextBoxColumn
            {
                Name = "colGiaBan",
                HeaderText = "Giá bán",
                DataPropertyName = "GiaBanThamKhao",
                Width = 110
            };
            colGiaBan.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            colGiaBan.DefaultCellStyle.Format = "N0";
            dgvBaoCaoTonKho.Columns.Add(colGiaBan);

            // 8. Tồn kho
            DataGridViewTextBoxColumn colTonKho = new DataGridViewTextBoxColumn
            {
                Name = "colTonKho",
                HeaderText = "Tồn kho",
                DataPropertyName = "SoLuongTon",
                Width = 95
            };
            colTonKho.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            colTonKho.DefaultCellStyle.Format = "N0";
            dgvBaoCaoTonKho.Columns.Add(colTonKho);

            // 9. Giá trị tồn
            DataGridViewTextBoxColumn colGiaTriTon = new DataGridViewTextBoxColumn
            {
                Name = "colGiaTriTon",
                HeaderText = "Giá trị tồn",
                DataPropertyName = "GiaTriTon",
                Width = 135
            };
            colGiaTriTon.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            colGiaTriTon.DefaultCellStyle.Format = "N0";
            dgvBaoCaoTonKho.Columns.Add(colGiaTriTon);

            // 10. Tồn tối thiểu
            DataGridViewTextBoxColumn colTonToiThieu = new DataGridViewTextBoxColumn
            {
                Name = "colTonToiThieu",
                HeaderText = "Tồn tối thiểu",
                DataPropertyName = "TonToiThieu",
                Width = 100
            };
            colTonToiThieu.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            colTonToiThieu.DefaultCellStyle.Format = "N0";
            dgvBaoCaoTonKho.Columns.Add(colTonToiThieu);

            // 11. Tồn tối đa
            DataGridViewTextBoxColumn colTonToiDa = new DataGridViewTextBoxColumn
            {
                Name = "colTonToiDa",
                HeaderText = "Tồn tối đa",
                DataPropertyName = "TonToiDa",
                Width = 95
            };
            colTonToiDa.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            colTonToiDa.DefaultCellStyle.Format = "N0";
            dgvBaoCaoTonKho.Columns.Add(colTonToiDa);

            // 12. Vị trí
            DataGridViewTextBoxColumn colViTri = new DataGridViewTextBoxColumn
            {
                Name = "colViTri",
                HeaderText = "Vị trí",
                DataPropertyName = "ViTri",
                Width = 85
            };
            colViTri.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgvBaoCaoTonKho.Columns.Add(colViTri);

            // 13. Trạng thái kinh doanh
            DataGridViewTextBoxColumn colTrangThai = new DataGridViewTextBoxColumn
            {
                Name = "colTrangThai",
                HeaderText = "Trạng thái",
                DataPropertyName = "TrangThai",
                Width = 130
            };
            colTrangThai.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgvBaoCaoTonKho.Columns.Add(colTrangThai);
        }

        private void KhoiTaoBoLoc()
        {
            dangNapDuLieu = true;


            // 2. Nạp Nhóm hàng
            try
            {
                string sqlNhom = "SELECT MaNhom, TenNhom FROM NhomHang ORDER BY TenNhom ASC";
                DataTable dtNhom = DataProcessing.ReadData(sqlNhom);
                List<string> listNhom = new List<string> { "-- Tất cả nhóm hàng --" };
                if (dtNhom != null)
                {
                    foreach (DataRow r in dtNhom.Rows)
                    {
                        listNhom.Add(r["TenNhom"].ToString());
                    }
                }
                ComboboxHelper.CaiDatTimKiem(cboNhomHang, listNhom, () => {
                    trangHienTai = 1;
                    TaiDuLieu();
                });
                cboNhomHang.SelectedIndex = 0;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi tải danh mục nhóm hàng: " + ex.Message, "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

            // 3. Tiêu chí tồn kho
            cboTieuChiTon.Items.Clear();
            cboTieuChiTon.Items.AddRange(new object[] {
                "-- Tất cả mức tồn --",
                "Còn tồn (> 0)",
                "Hết hàng (= 0)",
                "Tồn âm (< 0)",
                "Dưới định mức tối thiểu",
                "Vượt định mức tối đa"
            });
            cboTieuChiTon.SelectedIndex = 0;

            // 4. Trạng thái kinh doanh
            cboTrangThai.Items.Clear();
            cboTrangThai.Items.AddRange(new object[] {
                "-- Tất cả trạng thái --",
                "Đang kinh doanh",
                "Ngừng kinh doanh"
            });
            cboTrangThai.SelectedIndex = 0;

            // 5. Thời gian tạo: mặc định từ ngày tạo mặt hàng sớm nhất để không ẩn hàng cũ
            DateTime tuNgayMacDinh = new DateTime(2000, 1, 1);
            try
            {
                object minNgay = DataProcessing.ReadValue("SELECT MIN(NgayTao) FROM HangHoa");
                if (minNgay != null && minNgay != DBNull.Value)
                {
                    tuNgayMacDinh = Convert.ToDateTime(minNgay).Date;
                }
            }
            catch { }
            dtpTuNgay.Value = tuNgayMacDinh;
            dtpDenNgay.Value = DateTime.Now;

            // 6. Số dòng mỗi trang
            cboSoDongMoiTrang.SelectedIndex = 1; // 20 dòng

            dangNapDuLieu = false;
        }

        public void TaiDuLieu()
        {
            if (dangNapDuLieu) return;

            try
            {
                int maKho = SessionContext.MaKhoHienTai;

                string tuKhoa = txtTimKiem.Text.Trim();
                string nhomHang = cboNhomHang.Text.Trim();
                int tieuChiTonIndex = cboTieuChiTon.SelectedIndex;
                string trangThai = cboTrangThai.SelectedItem?.ToString() ?? "-- Tất cả trạng thái --";
                DateTime tuNgay = dtpTuNgay.Value.Date;
                // Mốc 0h ngày kế tiếp (so sánh "<") để bao trọn cả ngày cuối, kể cả phần mili giây
                DateTime denNgay = dtpDenNgay.Value.Date.AddDays(1);

                List<SqlParameter> parameters = new List<SqlParameter>();
                StringBuilder whereSql = new StringBuilder(" WHERE 1=1 ");

                // Lọc từ khóa
                if (!string.IsNullOrWhiteSpace(tuKhoa))
                {
                    whereSql.Append(" AND (h.MaHangNoiBo LIKE @Search OR h.TenHang LIKE @Search OR h.QuyCach LIKE @Search) ");
                    parameters.Add(new SqlParameter("@Search", "%" + tuKhoa + "%"));
                }

                // Lọc nhóm hàng
                if (!string.IsNullOrEmpty(nhomHang) && nhomHang != "-- Tất cả nhóm hàng --")
                {
                    whereSql.Append(" AND nh.TenNhom = @TenNhom ");
                    parameters.Add(new SqlParameter("@TenNhom", nhomHang));
                }

                // Lọc trạng thái kinh doanh
                // (CSDL chỉ cho phép DANG_BAN / NGUNG_BAN)
                if (trangThai == "Đang kinh doanh")
                {
                    whereSql.Append(" AND (h.TrangThai = 'DANG_BAN' OR h.TrangThai IS NULL) ");
                }
                else if (trangThai == "Ngừng kinh doanh")
                {
                    whereSql.Append(" AND h.TrangThai = 'NGUNG_BAN' ");
                }

                // Lọc thời gian tạo
                whereSql.Append(" AND h.NgayTao >= @TuNgay AND h.NgayTao < @DenNgay ");
                parameters.Add(new SqlParameter("@TuNgay", tuNgay));
                parameters.Add(new SqlParameter("@DenNgay", denNgay));

                // Điều kiện lọc mức tồn kho
                string tonKhoExpression = "ISNULL(tk.SoLuongTon, 0)";
                string tonToiThieuExp = "ISNULL(ntk.TonToiThieu, 0)";
                string tonToiDaExp = "ISNULL(ntk.TonCaoNhat, 0)";
                // Dưới định mức: theo kho đang làm việc, hoặc ở bất kỳ kho nào khi xem "Tất cả kho"
                string duoiDinhMucExp = (maKho > 0)
                    ? $"({tonKhoExpression} < {tonToiThieuExp})"
                    : "(ISNULL(cb.DuoiDinhMuc, 0) = 1)";
                // Vượt định mức: tương tự, xét theo từng kho (không so tổng tồn với tổng định mức các kho)
                string vuotDinhMucExp = (maKho > 0)
                    ? $"({tonKhoExpression} > {tonToiDaExp} AND {tonToiDaExp} > 0)"
                    : "(ISNULL(cb.VuotDinhMuc, 0) = 1)";

                if (tieuChiTonIndex == 1) // Còn tồn (> 0)
                {
                    whereSql.Append($" AND {tonKhoExpression} > 0 ");
                }
                else if (tieuChiTonIndex == 2) // Hết hàng (= 0)
                {
                    whereSql.Append($" AND {tonKhoExpression} = 0 ");
                }
                else if (tieuChiTonIndex == 3) // Tồn âm (< 0)
                {
                    whereSql.Append($" AND {tonKhoExpression} < 0 ");
                }
                else if (tieuChiTonIndex == 4) // Dưới định mức tối thiểu
                {
                    whereSql.Append($" AND {duoiDinhMucExp} ");
                }
                else if (tieuChiTonIndex == 5) // Vượt định mức tối đa
                {
                    whereSql.Append($" AND {vuotDinhMucExp} ");
                }

                // Xây dựng câu truy vấn theo MaKho
                string fromAndJoin;
                if (maKho > 0)
                {
                    fromAndJoin = $@"
                        FROM HangHoa h
                        CROSS JOIN (SELECT MaKho, TenKho FROM Kho WHERE MaKho = @MaKho) k
                        LEFT JOIN NhomHang nh ON h.MaNhom = nh.MaNhom
                        LEFT JOIN DonViTinh dvt ON h.MaDVT = dvt.MaDVT
                        LEFT JOIN vw_TonKhoHienTai tk ON h.MaHang = tk.MaHang AND tk.MaKho = k.MaKho
                        LEFT JOIN NguongTonKho ntk ON h.MaHang = ntk.MaHang AND ntk.MaKho = k.MaKho
                    ";
                    parameters.Add(new SqlParameter("@MaKho", maKho));
                }
                else
                {
                    fromAndJoin = @"
                        FROM HangHoa h
                        LEFT JOIN NhomHang nh ON h.MaNhom = nh.MaNhom
                        LEFT JOIN DonViTinh dvt ON h.MaDVT = dvt.MaDVT
                        LEFT JOIN (
                            SELECT MaHang, SUM(SoLuongTon) AS SoLuongTon
                            FROM vw_TonKhoHienTai
                            GROUP BY MaHang
                        ) tk ON h.MaHang = tk.MaHang
                        LEFT JOIN (
                            SELECT MaHang, SUM(ISNULL(TonToiThieu, 0)) AS TonToiThieu, SUM(ISNULL(TonCaoNhat, 0)) AS TonCaoNhat
                            FROM NguongTonKho
                            GROUP BY MaHang
                        ) ntk ON h.MaHang = ntk.MaHang
                        LEFT JOIN (
                            SELECT n.MaHang,
                                   MAX(CASE WHEN ISNULL(t.SoLuongTon, 0) < ISNULL(n.TonToiThieu, 0) THEN 1 ELSE 0 END) AS DuoiDinhMuc,
                                   MAX(CASE WHEN ISNULL(n.TonCaoNhat, 0) > 0 AND ISNULL(t.SoLuongTon, 0) > n.TonCaoNhat THEN 1 ELSE 0 END) AS VuotDinhMuc
                            FROM NguongTonKho n
                            LEFT JOIN vw_TonKhoHienTai t ON t.MaHang = n.MaHang AND t.MaKho = n.MaKho
                            GROUP BY n.MaHang
                        ) cb ON h.MaHang = cb.MaHang
                    ";
                }
                duoiDinhMucHienTai = duoiDinhMucExp;

                // 1. Đếm tổng số bản ghi
                string sqlCount = $"SELECT COUNT(DISTINCT h.MaHang) {fromAndJoin} {whereSql};";

                SqlParameter[] countParams = new SqlParameter[parameters.Count];
                for (int i = 0; i < parameters.Count; i++)
                {
                    countParams[i] = new SqlParameter(parameters[i].ParameterName, parameters[i].Value);
                }

                object resultCount = DataProcessing.ReadValue(sqlCount, countParams);
                tongSoBanGhi = resultCount != null && int.TryParse(resultCount.ToString(), out int c) ? c : 0;

                // 2. Tính lại phân trang
                tongSoTrang = (int)Math.Ceiling((double)tongSoBanGhi / soDongMoiTrang);
                if (tongSoTrang <= 0) tongSoTrang = 1;
                if (trangHienTai > tongSoTrang) trangHienTai = tongSoTrang;

                int skip = (trangHienTai - 1) * soDongMoiTrang;
                parameters.Add(new SqlParameter("@Skip", skip));
                parameters.Add(new SqlParameter("@Take", soDongMoiTrang));

                // 3. Lấy dữ liệu phân trang
                string tenKhoCol = (maKho > 0) ? "k.TenKho" : "N'Toàn hệ thống' AS TenKho";
                string sqlData = $@"
                    SELECT h.MaHang, h.MaHangNoiBo, h.TenHang, dvt.TenDVT, nh.TenNhom,
                           {tenKhoCol},
                           ISNULL(h.GiaVon, 0) AS GiaVon,
                           ISNULL(h.GiaBanThamKhao, 0) AS GiaBanThamKhao,
                           ISNULL(tk.SoLuongTon, 0) AS SoLuongTon,
                           CAST(ISNULL(tk.SoLuongTon, 0) * ISNULL(h.GiaVon, 0) AS DECIMAL(18,2)) AS GiaTriTon,
                           ISNULL(ntk.TonToiThieu, 0) AS TonToiThieu,
                           ISNULL(ntk.TonCaoNhat, 0) AS TonToiDa,
                           h.ViTri,
                           CASE
                               WHEN h.TrangThai = 'NGUNG_BAN' THEN N'Ngừng kinh doanh'
                               ELSE N'Đang kinh doanh'
                           END AS TrangThai
                    {fromAndJoin}
                    {whereSql}
                    ORDER BY h.MaHang ASC";

                // Lưu lại truy vấn (không phân trang) để Xuất file lấy toàn bộ kết quả lọc
                sqlXuatFile = sqlData;
                thamSoXuatFile = countParams;

                sqlData += " OFFSET @Skip ROWS FETCH NEXT @Take ROWS ONLY;";

                DataTable dtData = DataProcessing.ReadData(sqlData, parameters.ToArray());
                dgvBaoCaoTonKho.DataSource = dtData;
                if (dgvBaoCaoTonKho.Columns.Contains("colMaHang"))
                {
                    dgvBaoCaoTonKho.Columns["colMaHang"].Visible = false;
                }

                // 4. Cập nhật phân trang
                CapNhatThanhPhanTrang();

                // 5. Cập nhật 4 thẻ KPI tổng hợp
                TaiKpiTongHop(fromAndJoin, whereSql.ToString(), countParams);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi tải báo cáo tồn kho: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void TaiKpiTongHop(string fromAndJoin, string whereSql, SqlParameter[] baseParams)
        {
            try
            {
                string sqlKpi = $@"
                    SELECT 
                        COUNT(DISTINCT h.MaHang) AS TongSku,
                        ISNULL(SUM(tk.SoLuongTon), 0) AS TongSoLuong,
                        ISNULL(SUM(ISNULL(tk.SoLuongTon, 0) * ISNULL(h.GiaVon, 0)), 0) AS TongGiaTri,
                        ISNULL(SUM(CASE WHEN ISNULL(tk.SoLuongTon, 0) <= 0 OR {duoiDinhMucHienTai} THEN 1 ELSE 0 END), 0) AS CanLuuY
                    {fromAndJoin}
                    {whereSql};
                ";

                List<SqlParameter> kpiParams = new List<SqlParameter>();
                foreach (var p in baseParams)
                {
                    kpiParams.Add(new SqlParameter(p.ParameterName, p.Value));
                }

                DataTable dtKpi = DataProcessing.ReadData(sqlKpi, kpiParams.ToArray());
                if (dtKpi != null && dtKpi.Rows.Count > 0)
                {
                    DataRow r = dtKpi.Rows[0];
                    int sku = Convert.ToInt32(r["TongSku"]);
                    decimal soLuong = Convert.ToDecimal(r["TongSoLuong"]);
                    decimal giaTri = Convert.ToDecimal(r["TongGiaTri"]);
                    int canLuuY = Convert.ToInt32(r["CanLuuY"]);

                    lblCardSkuVal.Text = sku.ToString("N0");
                    lblCardSoLuongVal.Text = soLuong.ToString("N0");
                    lblCardGiaTriVal.Text = giaTri.ToString("N0") + " đ";
                    lblCardCanLuuYVal.Text = canLuuY.ToString("N0") + " mặt hàng";

                    if (canLuuY > 0)
                    {
                        lblCardCanLuuYVal.ForeColor = Color.Red;
                    }
                    else
                    {
                        lblCardCanLuuYVal.ForeColor = SystemColors.ControlText;
                    }
                }
            }
            catch { }
        }

        private void CapNhatThanhPhanTrang()
        {
            lblTrangHienTai.Text = $"Trang {trangHienTai} / {tongSoTrang}";
            lblTongSoHang.Text = $"Tổng số: {tongSoBanGhi:N0} hàng";
            btnTrangTruoc.Enabled = (trangHienTai > 1);
            btnTrangSau.Enabled = (trangHienTai < tongSoTrang);
        }

        private void dgvBaoCaoTonKho_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.RowIndex < 0 || e.ColumnIndex < 0) return;

            string colName = dgvBaoCaoTonKho.Columns[e.ColumnIndex].Name;

            // Tô đỏ số lượng tồn nếu <= 0 theo Điều luật 15a
            if (colName == "colTonKho" && e.Value != null)
            {
                if (decimal.TryParse(e.Value.ToString(), out decimal ton))
                {
                    if (ton <= 0)
                    {
                        e.CellStyle.ForeColor = Color.Red;
                        e.CellStyle.Font = new Font(dgvBaoCaoTonKho.Font, FontStyle.Bold);
                    }
                }
            }
            // Màu sắc trạng thái theo 3 nhóm chuẩn (Điều luật 15b, font chữ thường đồng bộ theo mẫu Trả hàng nhập)
            else if (colName == "colTrangThai" && e.Value != null)
            {
                string val = e.Value.ToString();
                if (val == "Đang kinh doanh")
                {
                    e.CellStyle.ForeColor = Color.FromArgb(22, 163, 74); // Xanh lá
                }
                else
                {
                    e.CellStyle.ForeColor = Color.FromArgb(100, 116, 139); // Xám nhạt
                }
            }
        }

        private void BoLoc_SelectedIndexChanged(object sender, EventArgs e)
        {
            trangHienTai = 1;
            TaiDuLieu();
            lblTongSoHang.BringToFront();
        }

        private void txtTimKiem_TextChanged(object sender, EventArgs e)
        {
            searchDebounceTimer?.Stop();
            searchDebounceTimer?.Start();
        }

        private void cboSoDongMoiTrang_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (int.TryParse(cboSoDongMoiTrang.SelectedItem?.ToString(), out int val))
            {
                soDongMoiTrang = val;
                trangHienTai = 1;
                TaiDuLieu();
            }
        }

        private void btnTrangTruoc_Click(object sender, EventArgs e)
        {
            if (trangHienTai > 1)
            {
                trangHienTai--;
                TaiDuLieu();
            }
        }

        private void btnTrangSau_Click(object sender, EventArgs e)
        {
            if (trangHienTai < tongSoTrang)
            {
                trangHienTai++;
                TaiDuLieu();
            }
        }

        private void dgvBaoCaoTonKho_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;

            // Nhân viên kho không có quyền xem Báo cáo thẻ kho (menu đã ẩn) nên không mở qua lối nhấp đúp
            if (!SessionContext.IsAdmin && !SessionContext.IsThuKho && SessionContext.IsNhanVienKho)
            {
                MessageBox.Show("Tài khoản của bạn không có quyền xem Báo cáo thẻ kho.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            MoBaoCaoTheKho();
        }

        private void MoBaoCaoTheKho()
        {
            if (dgvBaoCaoTonKho.CurrentRow == null)
            {
                MessageBox.Show("Vui lòng chọn một mặt hàng trên bảng để xem thẻ kho!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            int maHang = 0;
            if (dgvBaoCaoTonKho.CurrentRow.Cells["colMaHang"].Value != null)
            {
                int.TryParse(dgvBaoCaoTonKho.CurrentRow.Cells["colMaHang"].Value.ToString(), out maHang);
            }

                int maKho = SessionContext.MaKhoHienTai;

            // Kiểm tra xem form Thẻ kho đã mở trong MDI chưa
            if (this.MdiParent != null)
            {
                foreach (Form form in this.MdiParent.MdiChildren)
                {
                    if (form is frmBaoCaoTheKho existingForm)
                    {
                        existingForm.Activate();
                        existingForm.ChonMatHangVaKho(maHang, maKho, moTuTonKho: true);
                        return;
                    }
                }
            }

            frmBaoCaoTheKho frmTheKho = new frmBaoCaoTheKho(maHang, maKho, moTuTonKho: true);
            if (this.MdiParent != null)
            {
                frmTheKho.MdiParent = this.MdiParent;
            }
            frmTheKho.Show();
        }

        private void btnXuatFile_Click(object sender, EventArgs e)
        {
            try
            {
                if (dgvBaoCaoTonKho.Rows.Count == 0)
                {
                    MessageBox.Show("Không có dữ liệu để xuất file!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                SaveFileDialog sfd = new SaveFileDialog
                {
                    Filter = "CSV File (*.csv)|*.csv",
                    FileName = $"BaoCaoTonKho_{DateTime.Now:yyyyMMdd_HHmmss}.csv"
                };

                if (sfd.ShowDialog() == DialogResult.OK)
                {
                    StringBuilder sb = new StringBuilder();

                    // Ghi tiêu đề cột
                    List<string> headers = new List<string>();
                    List<string> colNames = new List<string>();
                    foreach (DataGridViewColumn col in dgvBaoCaoTonKho.Columns)
                    {
                        if (col.Visible && !string.IsNullOrWhiteSpace(col.HeaderText))
                        {
                            headers.Add($"\"{col.HeaderText.Replace("\"", "\"\"")}\"");
                            colNames.Add(col.Name);
                        }
                    }
                    sb.AppendLine(string.Join(",", headers));

                    // Lấy toàn bộ kết quả theo bộ lọc hiện tại (không chỉ trang đang xem)
                    DataTable dtXuat = null;
                    if (!string.IsNullOrEmpty(sqlXuatFile) && thamSoXuatFile != null)
                    {
                        List<SqlParameter> ps = new List<SqlParameter>();
                        foreach (var p in thamSoXuatFile) ps.Add(new SqlParameter(p.ParameterName, p.Value));
                        dtXuat = DataProcessing.ReadData(sqlXuatFile, ps.ToArray());
                    }

                    // Ghi dữ liệu từng dòng theo các cột đang hiển thị
                    if (dtXuat != null)
                    {
                        foreach (DataRow row in dtXuat.Rows)
                        {
                            List<string> cells = new List<string>();
                            foreach (string colName in colNames)
                            {
                                string prop = dgvBaoCaoTonKho.Columns[colName].DataPropertyName;
                                object val = (!string.IsNullOrEmpty(prop) && dtXuat.Columns.Contains(prop)) ? row[prop] : null;
                                string strVal = val != null && val != DBNull.Value ? val.ToString().Replace("\"", "\"\"") : "";
                                cells.Add($"\"{strVal}\"");
                            }
                            sb.AppendLine(string.Join(",", cells));
                        }
                    }

                    // Lưu file UTF-8 with BOM chuẩn tiếng Việt
                    File.WriteAllText(sfd.FileName, sb.ToString(), new UTF8Encoding(true));
                    MessageBox.Show("Xuất file báo cáo tồn kho thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi xuất file: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnHienCot_Click(object sender, EventArgs e)
        {
            foreach (Form openForm in Application.OpenForms)
            {
                // Chỉ dùng lại popup của chính form này (popup dùng chung với Báo cáo thẻ kho)
                if (openForm is frmHienCotBaoCao && openForm.Tag == this)
                {
                    openForm.Activate();
                    openForm.BringToFront();
                    return;
                }
            }

            frmHienCotBaoCao frm = new frmHienCotBaoCao(dgvBaoCaoTonKho);
            frm.Tag = this;
            frm.StartPosition = FormStartPosition.Manual;

            // Tính toạ \u0111\u1ED9 hi\u1EC3n th\u1ECB ngay ph\u00EDa d\u01B0\u1EDBi n\u00FAt btnHienCot (c\u0103n th\u1EB3ng m\u00E9p ph\u1EA3i n\u00FAt)
            Point btnPos = btnHienCot.PointToScreen(Point.Empty);
            int posX = btnPos.X + btnHienCot.Width - frm.Width;
            int posY = btnPos.Y + btnHienCot.Height + 2;

            // B\u1EA3o \u0111\u1EA3m form kh\u00F4ng b\u1ECB tr\u00E0n ra ngo\u00E0i m\u00E0n h\u00ECnh l\u00E0m vi\u1EC7c
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

        private void btnHuongDanSuDung_Click(object sender, EventArgs e)
        {
            string huongDan = "HƯỚNG DẪN BÁO CÁO TỒN KHO\n\n" +
                "1. Xem tồn kho: Báo cáo hiển thị tồn của kho đang làm việc. Quản lý dùng menu Kho hàng > Đổi kho để xem kho khác hoặc 'Tất cả kho' (tổng hợp toàn hệ thống).\n" +
                "2. Bộ lọc: Kết hợp lọc theo Nhóm hàng, Tiêu chí mức tồn (Còn tồn, Hết hàng, Tồn âm, Dưới định mức, Vượt định mức) và Trạng thái kinh doanh.\n" +
                "3. Xem thẻ kho: Nhấp đúp vào dòng mặt hàng trên bảng để mở xem lịch sử xuất - nhập - tồn thẻ kho chi tiết.\n" +
                "4. Xuất file: Nhấn 'Xuất file' để xuất toàn bộ kết quả theo bộ lọc hiện tại ra định dạng CSV chuẩn tiếng Việt có dấu.\n" +
                "5. Cảnh báo đỏ: Các mặt hàng hết hàng hoặc tồn âm được đánh dấu đỏ nổi bật.";

            MessageBox.Show(huongDan, "Hướng dẫn sử dụng", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
    }
}
