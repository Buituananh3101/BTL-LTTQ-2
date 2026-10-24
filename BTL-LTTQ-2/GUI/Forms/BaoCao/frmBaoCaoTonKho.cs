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

        public frmBaoCaoTonKho()
        {
            InitializeComponent();
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
                HeaderText = "Chi nhánh kho",
                DataPropertyName = "TenKho",
                Width = 140
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

            // 1. Nạp danh sách Chi nhánh kho
            try
            {
                string sqlKho;
                SqlParameter[] pKho = null;

                if (SessionContext.IsAdmin)
                {
                    sqlKho = "SELECT MaKho, TenKho FROM Kho WHERE TrangThai = 'HOAT_DONG' OR TrangThai = 'ACTIVE' ORDER BY MaKho ASC";
                }
                else
                {
                    sqlKho = @"SELECT k.MaKho, k.TenKho 
                              FROM Kho k 
                              INNER JOIN TaiKhoanKho tk ON k.MaKho = tk.MaKho 
                              WHERE tk.MaTaiKhoan = @MaTaiKhoan 
                                AND (k.TrangThai = 'HOAT_DONG' OR k.TrangThai = 'ACTIVE') 
                              ORDER BY k.MaKho ASC";
                    pKho = new SqlParameter[] { new SqlParameter("@MaTaiKhoan", SessionContext.MaTaiKhoan) };
                }

                DataTable dtKho = DataProcessing.ReadData(sqlKho, pKho);
                DataTable dtComboKho = new DataTable();
                dtComboKho.Columns.Add("MaKho", typeof(int));
                dtComboKho.Columns.Add("TenKho", typeof(string));

                dtComboKho.Rows.Add(0, "-- Tất cả các kho --");
                if (dtKho != null)
                {
                    foreach (DataRow r in dtKho.Rows)
                    {
                        dtComboKho.Rows.Add(Convert.ToInt32(r["MaKho"]), r["TenKho"].ToString());
                    }
                }

                cboKho.DataSource = dtComboKho;
                cboKho.DisplayMember = "TenKho";
                cboKho.ValueMember = "MaKho";

                if (SessionContext.MaKhoHienTai > 0)
                {
                    cboKho.SelectedValue = SessionContext.MaKhoHienTai;
                }
                else
                {
                    cboKho.SelectedIndex = 0;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi tải danh mục kho: " + ex.Message, "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

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

            // 5. Thời gian tạo
            dtpTuNgay.Value = new DateTime(DateTime.Now.Year, 1, 1);
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
                int maKho = 0;
                if (cboKho.SelectedValue != null && int.TryParse(cboKho.SelectedValue.ToString(), out int kVal))
                {
                    maKho = kVal;
                }

                string tuKhoa = txtTimKiem.Text.Trim();
                string nhomHang = cboNhomHang.Text.Trim();
                int tieuChiTonIndex = cboTieuChiTon.SelectedIndex;
                string trangThai = cboTrangThai.SelectedItem?.ToString() ?? "-- Tất cả trạng thái --";
                DateTime tuNgay = dtpTuNgay.Value.Date;
                DateTime denNgay = dtpDenNgay.Value.Date.AddDays(1).AddSeconds(-1);

                List<SqlParameter> parameters = new List<SqlParameter>();
                StringBuilder whereSql = new StringBuilder(" WHERE 1=1 ");

                // Lọc từ khóa
                if (!string.IsNullOrWhiteSpace(tuKhoa))
                {
                    whereSql.Append(" AND (h.MaHangNoiBo LIKE @Search OR h.TenHang LIKE @Search OR h.MaVach LIKE @Search) ");
                    parameters.Add(new SqlParameter("@Search", "%" + tuKhoa + "%"));
                }

                // Lọc nhóm hàng
                if (!string.IsNullOrEmpty(nhomHang) && nhomHang != "-- Tất cả nhóm hàng --")
                {
                    whereSql.Append(" AND nh.TenNhom = @TenNhom ");
                    parameters.Add(new SqlParameter("@TenNhom", nhomHang));
                }

                // Lọc trạng thái kinh doanh
                if (trangThai == "Đang kinh doanh")
                {
                    whereSql.Append(" AND (h.TrangThai = 'DANG_KINH_DOANH' OR h.TrangThai = N'Đang kinh doanh' OR h.TrangThai IS NULL) ");
                }
                else if (trangThai == "Ngừng kinh doanh")
                {
                    whereSql.Append(" AND (h.TrangThai = 'NGUNG_KINH_DOANH' OR h.TrangThai = N'Ngừng kinh doanh') ");
                }

                // Lọc thời gian tạo
                whereSql.Append(" AND h.NgayTao >= @TuNgay AND h.NgayTao <= @DenNgay ");
                parameters.Add(new SqlParameter("@TuNgay", tuNgay));
                parameters.Add(new SqlParameter("@DenNgay", denNgay));

                // Điều kiện lọc mức tồn kho
                string tonKhoExpression = "ISNULL(tk.SoLuongTon, 0)";
                string tonToiThieuExp = "ISNULL(ntk.TonToiThieu, 0)";
                string tonToiDaExp = "ISNULL(ntk.TonCaoNhat, 0)";

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
                    whereSql.Append($" AND {tonKhoExpression} < {tonToiThieuExp} ");
                }
                else if (tieuChiTonIndex == 5) // Vượt định mức tối đa
                {
                    whereSql.Append($" AND {tonKhoExpression} > {tonToiDaExp} AND {tonToiDaExp} > 0 ");
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
                            SELECT MaHang, MAX(TonToiThieu) AS TonToiThieu, MAX(TonCaoNhat) AS TonCaoNhat
                            FROM NguongTonKho
                            GROUP BY MaHang
                        ) ntk ON h.MaHang = ntk.MaHang
                    ";
                }

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
                               WHEN h.TrangThai = 'NGUNG_KINH_DOANH' OR h.TrangThai = N'Ngừng kinh doanh' THEN N'Ngừng kinh doanh'
                               ELSE N'Đang kinh doanh'
                           END AS TrangThai
                    {fromAndJoin}
                    {whereSql}
                    ORDER BY h.MaHang ASC
                    OFFSET @Skip ROWS FETCH NEXT @Take ROWS ONLY;
                ";

                DataTable dtData = DataProcessing.ReadData(sqlData, parameters.ToArray());
                dgvBaoCaoTonKho.DataSource = dtData;

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
                        ISNULL(SUM(CASE WHEN ISNULL(tk.SoLuongTon, 0) <= 0 OR ISNULL(tk.SoLuongTon, 0) < ISNULL(ntk.TonToiThieu, 0) THEN 1 ELSE 0 END), 0) AS CanLuuY
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
                        lblCardCanLuuYVal.ForeColor = Color.DimGray;
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
            // Màu sắc trạng thái theo 3 nhóm chuẩn (Điều luật 15b)
            else if (colName == "colTrangThai" && e.Value != null)
            {
                string val = e.Value.ToString();
                if (val == "Đang kinh doanh")
                {
                    e.CellStyle.ForeColor = Color.FromArgb(22, 163, 74); // Xanh lá
                    e.CellStyle.Font = new Font(dgvBaoCaoTonKho.Font, FontStyle.Bold);
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
        }

        private void txtTimKiem_TextChanged(object sender, EventArgs e)
        {
            trangHienTai = 1;
            TaiDuLieu();
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
            if (e.RowIndex >= 0)
            {
                MoBaoCaoTheKho();
            }
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

            int maKho = 0;
            if (cboKho.SelectedValue != null)
            {
                int.TryParse(cboKho.SelectedValue.ToString(), out maKho);
            }

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

                    // Ghi dữ liệu từng dòng
                    foreach (DataGridViewRow row in dgvBaoCaoTonKho.Rows)
                    {
                        List<string> cells = new List<string>();
                        foreach (string colName in colNames)
                        {
                            object val = row.Cells[colName].Value;
                            string strVal = val != null ? val.ToString().Replace("\"", "\"\"") : "";
                            cells.Add($"\"{strVal}\"");
                        }
                        sb.AppendLine(string.Join(",", cells));
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
            frmHienCotBaoCao frm = new frmHienCotBaoCao(dgvBaoCaoTonKho);
            Point pt = btnHienCot.PointToScreen(new Point(0, btnHienCot.Height));
            frm.StartPosition = FormStartPosition.Manual;
            frm.Location = pt;
            frm.Show(this);
        }

        private void btnHuongDanSuDung_Click(object sender, EventArgs e)
        {
            string huongDan = "HƯỚNG DẪN BÁO CÁO TỒN KHO\n\n" +
                "1. Xem tồn kho: Chọn Chi nhánh kho ở bộ lọc bên trái để xem tồn từng kho, hoặc chọn '-- Tất cả các kho --' để xem tổng hợp toàn hệ thống.\n" +
                "2. Bộ lọc: Kết hợp lọc theo Nhóm hàng, Tiêu chí mức tồn (Còn tồn, Hết hàng, Tồn âm, Dưới định mức, Vượt định mức) và Trạng thái kinh doanh.\n" +
                "3. Xem thẻ kho: Nhấp đúp vào dòng mặt hàng trên bảng để mở xem lịch sử xuất - nhập - tồn thẻ kho chi tiết.\n" +
                "4. Xuất file: Nhấn 'Xuất file' để xuất danh sách ra định dạng CSV chuẩn tiếng Việt có dấu.\n" +
                "5. Cảnh báo đỏ: Các mặt hàng hết hàng hoặc tồn âm được đánh dấu đỏ nổi bật.";

            MessageBox.Show(huongDan, "Hướng dẫn sử dụng", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
    }
}
