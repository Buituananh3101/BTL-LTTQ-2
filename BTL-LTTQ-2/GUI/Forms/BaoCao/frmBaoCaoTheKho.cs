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
    public partial class frmBaoCaoTheKho : Form
    {
        private int maHangKhoiTao = 0;
        private int maKhoKhoiTao = 0;
        private bool moTuTonKho = false;
        private bool dangNapDuLieu = false;
        private DataTable dtTheKhoHienThi;

        private Timer searchDebounceTimer;

        public frmBaoCaoTheKho()
        {
            InitializeComponent();
            btnQuayLai.Visible = false;
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
                    TaiDuLieuTheKho();
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
                    TaiDuLieuTheKho();
                }
            }
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (keyData == Keys.Escape && moTuTonKho)
            {
                QuayLaiBaoCaoTonKho();
                return true;
            }

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

        public frmBaoCaoTheKho(int maHang, int maKho = 0, bool moTuTonKho = false) : this()
        {
            this.maHangKhoiTao = maHang;
            this.maKhoKhoiTao = maKho;
            this.moTuTonKho = moTuTonKho;
            btnQuayLai.Visible = moTuTonKho;
        }

        private void frmBaoCaoTheKho_Load(object sender, EventArgs e)
        {
            try
            {
                typeof(DataGridView).InvokeMember("DoubleBuffered",
                    BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.SetProperty,
                    null, dgvTheKho, new object[] { true });
            }
            catch { }

            KhoiTaoCacCotLuoi();
            KhoiTaoBoLoc();

            // Nếu được truyền mã hàng từ form khác
            if (maHangKhoiTao > 0)
            {
                ChonMatHangVaKho(maHangKhoiTao, maKhoKhoiTao, moTuTonKho);
            }
            else
            {
                TaiDuLieuTheKho();
            }
        }

        public void ChonMatHangVaKho(int maHang, int maKho = 0, bool moTuTonKho = false)
        {
            this.moTuTonKho = moTuTonKho;
            btnQuayLai.Visible = moTuTonKho;

            dangNapDuLieu = true;

            if (maKho > 0)
            {
                cboKho.SelectedValue = maKho;
            }

            if (maHang > 0)
            {
                cboHangHoa.SelectedValue = maHang;
            }

            dangNapDuLieu = false;
            TaiDuLieuTheKho();
        }

        private void KhoiTaoCacCotLuoi()
        {
            dgvTheKho.AutoGenerateColumns = false;
            dgvTheKho.Columns.Clear();
            dgvTheKho.BorderStyle = BorderStyle.None;

            dgvTheKho.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(248, 250, 252);
            dgvTheKho.DefaultCellStyle.SelectionBackColor = Color.FromArgb(224, 231, 255);
            dgvTheKho.DefaultCellStyle.SelectionForeColor = Color.FromArgb(30, 41, 59);

            // 1. STT
            DataGridViewTextBoxColumn colStt = new DataGridViewTextBoxColumn
            {
                Name = "colStt",
                HeaderText = "STT",
                DataPropertyName = "Stt",
                Width = 50
            };
            colStt.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgvTheKho.Columns.Add(colStt);

            // 2. Thời gian
            DataGridViewTextBoxColumn colNgayGiaoDich = new DataGridViewTextBoxColumn
            {
                Name = "colNgayGiaoDich",
                HeaderText = "Thời gian",
                DataPropertyName = "NgayGiaoDichStr",
                Width = 135
            };
            colNgayGiaoDich.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgvTheKho.Columns.Add(colNgayGiaoDich);

            // 3. Mã chứng từ
            DataGridViewTextBoxColumn colMaChungTu = new DataGridViewTextBoxColumn
            {
                Name = "colMaChungTu",
                HeaderText = "Mã chứng từ",
                DataPropertyName = "MaChungTuHienThi",
                Width = 115
            };
            colMaChungTu.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgvTheKho.Columns.Add(colMaChungTu);

            // 4. Loại giao dịch
            DataGridViewTextBoxColumn colLoaiGiaoDich = new DataGridViewTextBoxColumn
            {
                Name = "colLoaiGiaoDich",
                HeaderText = "Loại giao dịch",
                DataPropertyName = "TenLoaiChungTu",
                Width = 140
            };
            colLoaiGiaoDich.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgvTheKho.Columns.Add(colLoaiGiaoDich);

            // 5. Đối tác / Diễn giải
            DataGridViewTextBoxColumn colDoiTac = new DataGridViewTextBoxColumn
            {
                Name = "colDoiTac",
                HeaderText = "Đối tác / Diễn giải",
                DataPropertyName = "DoiTac",
                Width = 230,
                AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill,
                MinimumWidth = 180
            };
            dgvTheKho.Columns.Add(colDoiTac);

            // 6. Chi nhánh kho
            DataGridViewTextBoxColumn colTenKho = new DataGridViewTextBoxColumn
            {
                Name = "colTenKho",
                HeaderText = "Chi nhánh kho",
                DataPropertyName = "TenKho",
                Width = 135
            };
            dgvTheKho.Columns.Add(colTenKho);

            // 7. SL Nhập (+)
            DataGridViewTextBoxColumn colSoLuongTang = new DataGridViewTextBoxColumn
            {
                Name = "colSoLuongTang",
                HeaderText = "SL Nhập (+)",
                DataPropertyName = "SoLuongTangStr",
                Width = 105
            };
            colSoLuongTang.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgvTheKho.Columns.Add(colSoLuongTang);

            // 8. SL Xuất (-)
            DataGridViewTextBoxColumn colSoLuongGiam = new DataGridViewTextBoxColumn
            {
                Name = "colSoLuongGiam",
                HeaderText = "SL Xuất (-)",
                DataPropertyName = "SoLuongGiamStr",
                Width = 105
            };
            colSoLuongGiam.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgvTheKho.Columns.Add(colSoLuongGiam);

            // 9. Tồn lũy kế
            DataGridViewTextBoxColumn colTonLuyKe = new DataGridViewTextBoxColumn
            {
                Name = "colTonLuyKe",
                HeaderText = "Tồn lũy kế",
                DataPropertyName = "TonLuyKeStr",
                Width = 110
            };
            colTonLuyKe.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgvTheKho.Columns.Add(colTonLuyKe);

            // 10. Ghi chú
            DataGridViewTextBoxColumn colGhiChu = new DataGridViewTextBoxColumn
            {
                Name = "colGhiChu",
                HeaderText = "Ghi chú",
                DataPropertyName = "GhiChu",
                Width = 140
            };
            dgvTheKho.Columns.Add(colGhiChu);
        }

        private void KhoiTaoBoLoc()
        {
            dangNapDuLieu = true;

            // 1. Nạp danh mục Mặt hàng
            try
            {
                string sqlHang = @"SELECT MaHang, MaHangNoiBo, TenHang 
                                   FROM HangHoa 
                                   ORDER BY TenHang ASC";
                DataTable dtHang = DataProcessing.ReadData(sqlHang);
                DataTable dtComboHang = new DataTable();
                dtComboHang.Columns.Add("MaHang", typeof(int));
                dtComboHang.Columns.Add("TenHienThi", typeof(string));

                if (dtHang != null)
                {
                    foreach (DataRow r in dtHang.Rows)
                    {
                        string sku = r["MaHangNoiBo"] != null ? r["MaHangNoiBo"].ToString() : "";
                        string ten = r["TenHang"] != null ? r["TenHang"].ToString() : "";
                        string hienThi = $"[{sku}] {ten}";
                        dtComboHang.Rows.Add(Convert.ToInt32(r["MaHang"]), hienThi);
                    }
                }

                ComboboxHelper.CaiDatTimKiem(cboHangHoa, dtComboHang, "TenHienThi", "MaHang", () => {
                    TaiDuLieuTheKho();
                });

                if (dtComboHang.Rows.Count > 0)
                {
                    cboHangHoa.SelectedIndex = 0;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi tải danh mục hàng hóa: " + ex.Message, "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

            // 2. Nạp Chi nhánh kho
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

                ComboboxHelper.CaiDatTimKiem(cboKho, dtComboKho, "TenKho", "MaKho", () =>
                {
                    TaiDuLieuTheKho();
                });

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

            // 3. Loại chứng từ / giao dịch
            cboLoaiChungTu.Items.Clear();
            cboLoaiChungTu.Items.AddRange(new object[] {
                "-- Tất cả giao dịch --",
                "Phiếu nhập mua (NCC)",
                "Xuất bán hàng",
                "Xuất hủy / Hao hụt",
                "Chuyển kho đến",
                "Chuyển kho đi",
                "Cân bằng kiểm kê",
                "Trả hàng nhập"
            });
            cboLoaiChungTu.SelectedIndex = 0;

            // 4. Thời gian phát sinh (30 ngày gần nhất)
            dtpTuNgay.Value = DateTime.Now.Date.AddDays(-30);
            dtpDenNgay.Value = DateTime.Now;

            dangNapDuLieu = false;
        }

        public void TaiDuLieuTheKho()
        {
            if (dangNapDuLieu) return;

            try
            {
                // 1. Kiểm tra chọn mặt hàng
                int maHang = 0;
                if (cboHangHoa.SelectedValue != null && int.TryParse(cboHangHoa.SelectedValue.ToString(), out int hVal))
                {
                    maHang = hVal;
                }

                if (maHang <= 0)
                {
                    lblTenHangHienThi.Text = "Vui lòng chọn một mặt hàng...";
                    lblThongTinChiTiet.Text = "ĐVT: -- | Nhóm: -- | Vị trí: -- | Giá vốn: 0 đ";
                    lblTonDauVal.Text = "0";
                    lblNhapKyVal.Text = "0";
                    lblXuatKyVal.Text = "0";
                    lblTonCuoiVal.Text = "0";
                    lblTongSoGiaoDich.Text = "Tổng số: 0 giao dịch phát sinh";
                    dgvTheKho.DataSource = null;
                    return;
                }

                // 2. Lấy thông tin mặt hàng hiển thị lên Banner
                string sqlHang = @"
                    SELECT h.MaHangNoiBo, h.TenHang, dvt.TenDVT, nh.TenNhom, h.ViTri, ISNULL(h.GiaVon, 0) AS GiaVon
                    FROM HangHoa h
                    LEFT JOIN DonViTinh dvt ON h.MaDVT = dvt.MaDVT
                    LEFT JOIN NhomHang nh ON h.MaNhom = nh.MaNhom
                    WHERE h.MaHang = @MaHang;
                ";
                DataTable dtHang = DataProcessing.ReadData(sqlHang, new SqlParameter[] { new SqlParameter("@MaHang", maHang) });
                if (dtHang != null && dtHang.Rows.Count > 0)
                {
                    DataRow rH = dtHang.Rows[0];
                    string sku = rH["MaHangNoiBo"] != null ? rH["MaHangNoiBo"].ToString() : "";
                    string ten = rH["TenHang"] != null ? rH["TenHang"].ToString() : "";
                    string dvt = rH["TenDVT"] != null ? rH["TenDVT"].ToString() : "";
                    string nhom = rH["TenNhom"] != null ? rH["TenNhom"].ToString() : "";
                    string vitri = rH["ViTri"] != null ? rH["ViTri"].ToString() : "--";
                    decimal giaVon = Convert.ToDecimal(rH["GiaVon"]);

                    lblTenHangHienThi.Text = $"[{sku}] {ten}";
                    lblThongTinChiTiet.Text = $"ĐVT: {dvt}  |  Nhóm: {nhom}  |  Vị trí: {vitri}  |  Giá vốn: {giaVon:N0} đ";
                }

                // 3. Đọc các tham số lọc
                int maKho = 0;
                if (cboKho.SelectedValue != null && int.TryParse(cboKho.SelectedValue.ToString(), out int kVal))
                {
                    maKho = kVal;
                }

                DateTime tuNgay = dtpTuNgay.Value.Date;
                DateTime denNgay = dtpDenNgay.Value.Date.AddDays(1).AddSeconds(-1);
                string tuKhoa = txtTimKiem.Text.Trim();
                int loaiChungTuIdx = cboLoaiChungTu.SelectedIndex;

                // 4. Tính Tồn đầu kỳ (T_đầu) trước ngày TuNgay
                string sqlTonDau = @"
                    SELECT ISNULL(SUM(SoLuongTang - SoLuongGiam), 0) AS TonDauKy
                    FROM vw_BienDongKho
                    WHERE MaHang = @MaHang
                      AND (@MaKho = 0 OR MaKho = @MaKho)
                      AND NgayGiaoDich < @TuNgay;
                ";
                SqlParameter[] pTonDau = new SqlParameter[] {
                    new SqlParameter("@MaHang", maHang),
                    new SqlParameter("@MaKho", maKho),
                    new SqlParameter("@TuNgay", tuNgay)
                };
                object objTonDau = DataProcessing.ReadValue(sqlTonDau, pTonDau);
                decimal tonDauKy = objTonDau != null && decimal.TryParse(objTonDau.ToString(), out decimal td) ? td : 0;

                // 5. Truy vấn danh sách giao dịch phát sinh trong kỳ từ vw_BienDongKho
                StringBuilder sqlGd = new StringBuilder(@"
                    SELECT bd.NgayGiaoDich, bd.MaKho, k.TenKho, bd.MaHang, bd.LoaiChungTu, bd.MaChungTu,
                           bd.SoLuongTang, bd.SoLuongGiam,
                           nccPn.TenDonVi AS PnTenNCC,
                           nccPth.TenDonVi AS PthTenNCC,
                           pc.MaKhoNguon, kn.TenKho AS TenKhoNguon,
                           pc.MaKhoDich, kd.TenKho AS TenKhoDich,
                           kh.TenKH AS PxTenKH, px.GhiChu AS PxGhiChu
                    FROM vw_BienDongKho bd
                    LEFT JOIN Kho k ON bd.MaKho = k.MaKho
                    -- Thông tin phiếu nhập
                    LEFT JOIN PhieuNhap pn ON bd.LoaiChungTu = 'PHIEU_NHAP' AND bd.MaChungTu = pn.MaPhieuNhap
                    LEFT JOIN DonViCapHang nccPn ON pn.MaDonViCapHang = nccPn.MaDonViCapHang
                    -- Thông tin phiếu trả hàng nhập
                    LEFT JOIN PhieuTraHangNhap pth ON bd.LoaiChungTu = 'TRA_HANG_NHAP' AND bd.MaChungTu = pth.MaPhieuTraHang
                    LEFT JOIN DonViCapHang nccPth ON pth.MaDonViCapHang = nccPth.MaDonViCapHang
                    -- Thông tin chuyển kho
                    LEFT JOIN PhieuChuyenKho pc ON (bd.LoaiChungTu = 'CHUYEN_XUAT' OR bd.LoaiChungTu = 'CHUYEN_NHAP') AND bd.MaChungTu = pc.MaPhieuChuyen
                    LEFT JOIN Kho kn ON pc.MaKhoNguon = kn.MaKho
                    LEFT JOIN Kho kd ON pc.MaKhoDich = kd.MaKho
                    -- Thông tin phiếu xuất
                    LEFT JOIN PhieuXuat px ON (bd.LoaiChungTu = 'XUAT_BAN' OR bd.LoaiChungTu = 'XUAT_HUY' OR bd.LoaiChungTu = 'XUAT_NOI_BO') AND bd.MaChungTu = px.MaPhieuXuat
                    LEFT JOIN DonBan db ON px.MaDonBan = db.MaDonBan
                    LEFT JOIN KhachHang kh ON db.MaKH = kh.MaKH
                    WHERE bd.MaHang = @MaHang
                      AND (@MaKho = 0 OR bd.MaKho = @MaKho)
                      AND bd.NgayGiaoDich >= @TuNgay AND bd.NgayGiaoDich <= @DenNgay
                ");

                List<SqlParameter> pGd = new List<SqlParameter>
                {
                    new SqlParameter("@MaHang", maHang),
                    new SqlParameter("@MaKho", maKho),
                    new SqlParameter("@TuNgay", tuNgay),
                    new SqlParameter("@DenNgay", denNgay)
                };

                // Lọc theo loại chứng từ
                if (loaiChungTuIdx == 1) // Phiếu nhập mua (NCC)
                {
                    sqlGd.Append(" AND bd.LoaiChungTu = 'PHIEU_NHAP' ");
                }
                else if (loaiChungTuIdx == 2) // Xuất bán
                {
                    sqlGd.Append(" AND bd.LoaiChungTu = 'XUAT_BAN' ");
                }
                else if (loaiChungTuIdx == 3) // Xuất hủy
                {
                    sqlGd.Append(" AND bd.LoaiChungTu = 'XUAT_HUY' ");
                }
                else if (loaiChungTuIdx == 4) // Chuyển kho đến
                {
                    sqlGd.Append(" AND bd.LoaiChungTu = 'CHUYEN_NHAP' ");
                }
                else if (loaiChungTuIdx == 5) // Chuyển kho đi
                {
                    sqlGd.Append(" AND bd.LoaiChungTu = 'CHUYEN_XUAT' ");
                }
                else if (loaiChungTuIdx == 6) // Cân bằng kiểm kê
                {
                    sqlGd.Append(" AND bd.LoaiChungTu = 'KIEM_KE' ");
                }
                else if (loaiChungTuIdx == 7) // Trả hàng nhập
                {
                    sqlGd.Append(" AND bd.LoaiChungTu = 'TRA_HANG_NHAP' ");
                }

                sqlGd.Append(" ORDER BY bd.NgayGiaoDich ASC, bd.MaChungTu ASC;");

                DataTable dtRaw = DataProcessing.ReadData(sqlGd.ToString(), pGd.ToArray());

                // 6. Xây dựng bảng hiển thị thẻ kho có tính tồn lũy kế
                dtTheKhoHienThi = new DataTable();
                dtTheKhoHienThi.Columns.Add("Stt", typeof(string));
                dtTheKhoHienThi.Columns.Add("NgayGiaoDichStr", typeof(string));
                dtTheKhoHienThi.Columns.Add("MaChungTuHienThi", typeof(string));
                dtTheKhoHienThi.Columns.Add("TenLoaiChungTu", typeof(string));
                dtTheKhoHienThi.Columns.Add("DoiTac", typeof(string));
                dtTheKhoHienThi.Columns.Add("TenKho", typeof(string));
                dtTheKhoHienThi.Columns.Add("SoLuongTangStr", typeof(string));
                dtTheKhoHienThi.Columns.Add("SoLuongGiamStr", typeof(string));
                dtTheKhoHienThi.Columns.Add("TonLuyKeStr", typeof(string));
                dtTheKhoHienThi.Columns.Add("TonLuyKeNum", typeof(decimal));
                dtTheKhoHienThi.Columns.Add("GhiChu", typeof(string));

                // Dòng Tồn đầu kỳ (font chữ thường chuẩn hóa theo mẫu Trả hàng nhập)
                string tenKhoHienThi = (maKho > 0) ? (cboKho.Text) : "Toàn hệ thống";
                dtTheKhoHienThi.Rows.Add(
                    "",
                    tuNgay.ToString("dd/MM/yyyy"),
                    "",
                    "Tồn đầu kỳ",
                    "Số dư tồn kho đầu kỳ chuyển sang",
                    tenKhoHienThi,
                    "-",
                    "-",
                    tonDauKy.ToString("N0"),
                    tonDauKy,
                    ""
                );

                decimal tonLuyKe = tonDauKy;
                decimal tongNhapKy = 0;
                decimal tongXuatKy = 0;
                int stt = 1;

                if (dtRaw != null)
                {
                    foreach (DataRow r in dtRaw.Rows)
                    {
                        DateTime ngayGd = Convert.ToDateTime(r["NgayGiaoDich"]);
                        string loaiCt = r["LoaiChungTu"].ToString();
                        int maCt = Convert.ToInt32(r["MaChungTu"]);
                        decimal slTang = Convert.ToDecimal(r["SoLuongTang"]);
                        decimal slGiam = Convert.ToDecimal(r["SoLuongGiam"]);
                        string tenKhoRow = r["TenKho"] != null ? r["TenKho"].ToString() : "";

                        // Định dạng mã hiển thị & Diễn giải đối tác
                        string maHienThi = "";
                        string tenLoai = "";
                        string doiTac = "";
                        string ghiChu = "";

                        switch (loaiCt)
                        {
                            case "PHIEU_NHAP":
                                maHienThi = "PN" + maCt.ToString("D6");
                                tenLoai = "Nhập mua NCC";
                                doiTac = r["PnTenNCC"] != DBNull.Value ? r["PnTenNCC"].ToString() : "Nhà cung cấp";
                                break;
                            case "CHUYEN_XUAT":
                                maHienThi = "CK" + maCt.ToString("D6");
                                tenLoai = "Chuyển kho đi";
                                doiTac = "Chuyển đến: " + (r["TenKhoDich"] != DBNull.Value ? r["TenKhoDich"].ToString() : "Kho đích");
                                break;
                            case "CHUYEN_NHAP":
                                maHienThi = "CK" + maCt.ToString("D6");
                                tenLoai = "Chuyển kho đến";
                                doiTac = "Nhận từ: " + (r["TenKhoNguon"] != DBNull.Value ? r["TenKhoNguon"].ToString() : "Kho nguồn");
                                break;
                            case "KIEM_KE":
                                maHienThi = "PKK" + maCt.ToString("D6");
                                tenLoai = "Cân bằng kiểm kê";
                                doiTac = "Điều chỉnh cân bằng kho";
                                break;
                            case "TRA_HANG_NHAP":
                                maHienThi = "THN" + maCt.ToString("D6");
                                tenLoai = "Trả hàng nhập";
                                doiTac = r["PthTenNCC"] != DBNull.Value ? r["PthTenNCC"].ToString() : "Nhà cung cấp";
                                break;
                            case "XUAT_BAN":
                                maHienThi = "PX" + maCt.ToString("D6");
                                tenLoai = "Xuất bán hàng";
                                doiTac = r["PxTenKH"] != DBNull.Value ? r["PxTenKH"].ToString() : "Khách mua lẻ";
                                ghiChu = r["PxGhiChu"] != DBNull.Value ? r["PxGhiChu"].ToString() : "";
                                break;
                            case "XUAT_HUY":
                                maHienThi = "PX" + maCt.ToString("D6");
                                tenLoai = "Xuất hủy";
                                doiTac = "Xuất hủy hao hụt";
                                ghiChu = r["PxGhiChu"] != DBNull.Value ? r["PxGhiChu"].ToString() : "";
                                break;
                            default:
                                maHienThi = maCt.ToString();
                                tenLoai = loaiCt;
                                doiTac = "Biến động kho";
                                break;
                        }

                        // Lọc từ khóa tìm kiếm (hỗ trợ cả tiếng Việt có dấu và không dấu)
                        if (!string.IsNullOrWhiteSpace(tuKhoa))
                        {
                            string tuKhoaKhongDau = ComboboxHelper.BoDauTiengViet(tuKhoa);
                            bool match = maHienThi.IndexOf(tuKhoa, StringComparison.OrdinalIgnoreCase) >= 0 ||
                                         doiTac.IndexOf(tuKhoa, StringComparison.OrdinalIgnoreCase) >= 0 ||
                                         ghiChu.IndexOf(tuKhoa, StringComparison.OrdinalIgnoreCase) >= 0 ||
                                         ComboboxHelper.BoDauTiengViet(doiTac).IndexOf(tuKhoaKhongDau, StringComparison.OrdinalIgnoreCase) >= 0 ||
                                         ComboboxHelper.BoDauTiengViet(ghiChu).IndexOf(tuKhoaKhongDau, StringComparison.OrdinalIgnoreCase) >= 0;
                            if (!match) continue;
                        }

                        // Tính tồn lũy kế
                        tonLuyKe = tonLuyKe + slTang - slGiam;
                        tongNhapKy += slTang;
                        tongXuatKy += slGiam;

                        string slTangStr = slTang > 0 ? slTang.ToString("N0") : "-";
                        string slGiamStr = slGiam > 0 ? slGiam.ToString("N0") : "-";

                        dtTheKhoHienThi.Rows.Add(
                            stt.ToString(),
                            ngayGd.ToString("dd/MM/yyyy HH:mm"),
                            maHienThi,
                            tenLoai,
                            doiTac,
                            tenKhoRow,
                            slTangStr,
                            slGiamStr,
                            tonLuyKe.ToString("N0"),
                            tonLuyKe,
                            ghiChu
                        );

                        stt++;
                    }
                }

                // Dòng Tồn cuối kỳ (font chữ thường chuẩn hóa theo mẫu Trả hàng nhập)
                decimal tonCuoiKy = tonLuyKe;
                dtTheKhoHienThi.Rows.Add(
                    "",
                    denNgay.ToString("dd/MM/yyyy"),
                    "",
                    "Tồn cuối kỳ",
                    "Số dư tồn kho cuối kỳ",
                    tenKhoHienThi,
                    tongNhapKy.ToString("N0"),
                    tongXuatKy.ToString("N0"),
                    tonCuoiKy.ToString("N0"),
                    tonCuoiKy,
                    ""
                );

                dgvTheKho.DataSource = dtTheKhoHienThi;

                // 7. Cập nhật 4 thẻ KPI Thẻ kho
                lblTonDauVal.Text = tonDauKy.ToString("N0");
                lblNhapKyVal.Text = tongNhapKy.ToString("N0");
                lblXuatKyVal.Text = tongXuatKy.ToString("N0");
                lblTonCuoiVal.Text = tonCuoiKy.ToString("N0");

                if (tonCuoiKy < 0)
                {
                    lblTonCuoiVal.ForeColor = Color.Red;
                }
                else
                {
                    lblTonCuoiVal.ForeColor = SystemColors.ControlText;
                }

                lblTongSoGiaoDich.Text = $"Tổng số: {(stt - 1):N0} giao dịch phát sinh trong kỳ";
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi tải báo cáo thẻ kho: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void dgvTheKho_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.RowIndex < 0 || e.ColumnIndex < 0) return;

            string colName = dgvTheKho.Columns[e.ColumnIndex].Name;

            // Highlight dòng Tồn đầu kỳ và Tồn cuối kỳ bằng màu nền nhẹ, font chữ thường chuẩn hóa
            if (e.RowIndex == 0 || (dtTheKhoHienThi != null && e.RowIndex == dtTheKhoHienThi.Rows.Count - 1))
            {
                e.CellStyle.BackColor = Color.FromArgb(241, 245, 249);
            }

            // Tô đỏ số lượng tồn lũy kế nếu < 0 theo Điều luật 15a
            if (colName == "colTonLuyKe" && e.Value != null)
            {
                if (decimal.TryParse(e.Value.ToString().Replace(",", "").Replace(".", ""), out decimal ton))
                {
                    if (ton < 0)
                    {
                        e.CellStyle.ForeColor = Color.Red;
                        e.CellStyle.Font = new Font(dgvTheKho.Font, FontStyle.Bold);
                    }
                }
            }
            // Màu sắc Loại giao dịch theo chuẩn (font chữ thường đồng bộ theo mẫu Trả hàng nhập)
            else if (colName == "colLoaiGiaoDich" && e.Value != null)
            {
                string loai = e.Value.ToString();
                if (loai == "Nhập mua NCC" || loai == "Chuyển kho đến")
                {
                    e.CellStyle.ForeColor = Color.FromArgb(22, 163, 74); // Xanh lá
                }
                else if (loai == "Xuất bán hàng" || loai == "Chuyển kho đi" || loai == "Trả hàng nhập" || loai == "Xuất hủy")
                {
                    e.CellStyle.ForeColor = SystemColors.ControlText;
                }
            }
        }

        private void BoLoc_SelectedIndexChanged(object sender, EventArgs e)
        {
            TaiDuLieuTheKho();
        }

        private void txtTimKiem_TextChanged(object sender, EventArgs e)
        {
            searchDebounceTimer?.Stop();
            searchDebounceTimer?.Start();
        }

        private void btnXuatFile_Click(object sender, EventArgs e)
        {
            try
            {
                if (dtTheKhoHienThi == null || dtTheKhoHienThi.Rows.Count == 0)
                {
                    MessageBox.Show("Không có dữ liệu để xuất file!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                SaveFileDialog sfd = new SaveFileDialog
                {
                    Filter = "CSV File (*.csv)|*.csv",
                    FileName = $"TheKho_{DateTime.Now:yyyyMMdd_HHmmss}.csv"
                };

                if (sfd.ShowDialog() == DialogResult.OK)
                {
                    StringBuilder sb = new StringBuilder();

                    // Tiêu đề báo cáo
                    sb.AppendLine($"\"BÁO CÁO THẺ KHO - {lblTenHangHienThi.Text.Replace("\"", "\"\"")}\"");
                    sb.AppendLine($"\"{lblThongTinChiTiet.Text.Replace("\"", "\"\"")}\"");
                    sb.AppendLine($"\"Thời gian: Từ {dtpTuNgay.Value:dd/MM/yyyy} đến {dtpDenNgay.Value:dd/MM/yyyy}\"");
                    sb.AppendLine();

                    // Ghi tiêu đề cột
                    List<string> headers = new List<string>();
                    List<string> colNames = new List<string>();
                    foreach (DataGridViewColumn col in dgvTheKho.Columns)
                    {
                        if (col.Visible && !string.IsNullOrWhiteSpace(col.HeaderText))
                        {
                            headers.Add($"\"{col.HeaderText.Replace("\"", "\"\"")}\"");
                            colNames.Add(col.DataPropertyName);
                        }
                    }
                    sb.AppendLine(string.Join(",", headers));

                    // Ghi từng dòng dữ liệu
                    foreach (DataRow row in dtTheKhoHienThi.Rows)
                    {
                        List<string> cells = new List<string>();
                        foreach (string colName in colNames)
                        {
                            string strVal = row[colName] != null ? row[colName].ToString().Replace("\"", "\"\"") : "";
                            cells.Add($"\"{strVal}\"");
                        }
                        sb.AppendLine(string.Join(",", cells));
                    }

                    // Lưu file UTF-8 with BOM chuẩn tiếng Việt
                    File.WriteAllText(sfd.FileName, sb.ToString(), new UTF8Encoding(true));
                    MessageBox.Show("Xuất file báo cáo thẻ kho thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
                if (openForm is frmHienCotBaoCao)
                {
                    openForm.Activate();
                    openForm.BringToFront();
                    return;
                }
            }

            frmHienCotBaoCao frm = new frmHienCotBaoCao(dgvTheKho);
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
            string huongDan = "HƯỚNG DẪN BÁO CÁO THẺ KHO\n\n" +
                "1. Chọn mặt hàng: Bắt buộc chọn mặt hàng cần theo dõi tại ComboBox 'Mặt hàng' bên trái (hỗ trợ gõ tìm kiếm theo mã hoặc tên).\n" +
                "2. Chi nhánh kho: Chọn xem thẻ kho tại 1 kho cụ thể hoặc toàn hệ thống.\n" +
                "3. Dải ngày: Chọn mốc thời gian để hệ thống tính Tồn đầu kỳ và Tồn cuối kỳ chuẩn xác theo chuỗi chứng từ.\n" +
                "4. Tồn lũy kế: Cột 'Tồn lũy kế' tự động cộng dồn SL nhập và trừ đi SL xuất theo trình tự thời gian phát sinh.\n" +
                "5. Xuất file: Nhấn 'Xuất file' để xuất toàn bộ biến động thẻ kho ra file CSV chuẩn tiếng Việt có dấu.";

            MessageBox.Show(huongDan, "Hướng dẫn sử dụng", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void btnQuayLai_Click(object sender, EventArgs e)
        {
            QuayLaiBaoCaoTonKho();
        }

        private void QuayLaiBaoCaoTonKho()
        {
            if (this.MdiParent != null)
            {
                foreach (Form form in this.MdiParent.MdiChildren)
                {
                    if (form is frmBaoCaoTonKho frmTonKho)
                    {
                        frmTonKho.Activate();
                        break;
                    }
                }
            }
            this.Close();
        }

        private void frmBaoCaoTheKho_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (moTuTonKho && this.MdiParent != null)
            {
                foreach (Form form in this.MdiParent.MdiChildren)
                {
                    if (form is frmBaoCaoTonKho frmTonKho)
                    {
                        frmTonKho.Activate();
                        break;
                    }
                }
            }
        }
    }
}
