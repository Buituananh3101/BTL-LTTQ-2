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
            btnQuayLai.Visible = true;
            btnQuayLai.Text = "Đóng";
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
            btnQuayLai.Visible = true;
            btnQuayLai.Text = moTuTonKho ? "Quay lại" : "Đóng";
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

            dgvTheKho.BringToFront();

            KhoiTaoCacCotLuoi();
            KhoiTaoBoLoc();
            KhoiTaoSuKienTheKpi();

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
            this.maHangKhoiTao = maHang;
            this.maKhoKhoiTao = maKho;
            this.moTuTonKho = moTuTonKho;
            btnQuayLai.Visible = true;
            btnQuayLai.Text = moTuTonKho ? "Quay lại" : "Đóng";

            dangNapDuLieu = true;

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
                HeaderText = "Kho",
                DataPropertyName = "TenKho",
                Width = 135,
                Visible = false
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

        private void KhoiTaoSuKienTheKpi()
        {
            // Thiết lập con trỏ tay (Hand) cho các thẻ KPI và nhãn bên trong
            Control[] kpiControls = new Control[]
            {
                pnlCardTonDau, lblTonDauTitle, lblTonDauVal,
                pnlCardNhapKy, lblNhapKyTitle, lblNhapKyVal,
                pnlCardXuatKy, lblXuatKyTitle, lblXuatKyVal,
                pnlCardTonCuoi, lblTonCuoiTitle, lblTonCuoiVal
            };

            foreach (var ctrl in kpiControls)
            {
                if (ctrl != null)
                {
                    ctrl.Cursor = Cursors.Hand;
                }
            }

            // Nhấp Tồn đầu kỳ: Reset về tất cả giao dịch và chọn dòng đầu tiên
            Action chonTonDau = () =>
            {
                if (cboLoaiChungTu.SelectedIndex != 0)
                {
                    cboLoaiChungTu.SelectedIndex = 0;
                }
                if (dgvTheKho.Rows.Count > 0)
                {
                    dgvTheKho.ClearSelection();
                    dgvTheKho.Rows[0].Selected = true;
                    dgvTheKho.FirstDisplayedScrollingRowIndex = 0;
                }
            };
            if (pnlCardTonDau != null) pnlCardTonDau.Click += (s, e) => chonTonDau();
            if (lblTonDauTitle != null) lblTonDauTitle.Click += (s, e) => chonTonDau();
            if (lblTonDauVal != null) lblTonDauVal.Click += (s, e) => chonTonDau();

            // Nhấp Tổng nhập trong kỳ (+): Lọc phiếu nhập mua
            Action chonNhapKy = () =>
            {
                if (cboLoaiChungTu.SelectedIndex != 1)
                {
                    cboLoaiChungTu.SelectedIndex = 1;
                }
            };
            if (pnlCardNhapKy != null) pnlCardNhapKy.Click += (s, e) => chonNhapKy();
            if (lblNhapKyTitle != null) lblNhapKyTitle.Click += (s, e) => chonNhapKy();
            if (lblNhapKyVal != null) lblNhapKyVal.Click += (s, e) => chonNhapKy();

            // Nhấp Tổng xuất trong kỳ (-): Lọc xuất bán hàng
            Action chonXuatKy = () =>
            {
                if (cboLoaiChungTu.SelectedIndex != 2)
                {
                    cboLoaiChungTu.SelectedIndex = 2;
                }
            };
            if (pnlCardXuatKy != null) pnlCardXuatKy.Click += (s, e) => chonXuatKy();
            if (lblXuatKyTitle != null) lblXuatKyTitle.Click += (s, e) => chonXuatKy();
            if (lblXuatKyVal != null) lblXuatKyVal.Click += (s, e) => chonXuatKy();

            // Nhấp Tồn cuối kỳ: Reset về tất cả giao dịch và cuộn xuống dòng cuối cùng
            Action chonTonCuoi = () =>
            {
                if (cboLoaiChungTu.SelectedIndex != 0)
                {
                    cboLoaiChungTu.SelectedIndex = 0;
                }
                if (dgvTheKho.Rows.Count > 0)
                {
                    dgvTheKho.ClearSelection();
                    int lastIdx = dgvTheKho.Rows.Count - 1;
                    dgvTheKho.Rows[lastIdx].Selected = true;
                    dgvTheKho.FirstDisplayedScrollingRowIndex = lastIdx;
                }
            };
            if (pnlCardTonCuoi != null) pnlCardTonCuoi.Click += (s, e) => chonTonCuoi();
            if (lblTonCuoiTitle != null) lblTonCuoiTitle.Click += (s, e) => chonTonCuoi();
            if (lblTonCuoiVal != null) lblTonCuoiVal.Click += (s, e) => chonTonCuoi();
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

            // 3. Loại chứng từ / giao dịch
            cboLoaiChungTu.Items.Clear();
            cboLoaiChungTu.Items.AddRange(new object[] {
                "-- Tất cả giao dịch --",
                "Phiếu nhập mua (NCC)",
                "Xuất bán hàng",
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

                // Xác định mã kho và tên kho áp dụng
                int maKho = (this.maKhoKhoiTao > 0) ? this.maKhoKhoiTao : SessionContext.MaKhoHienTai;
                string tenKhoHienThi = "Toàn hệ thống";
                if (maKho > 0)
                {
                    if (maKho == SessionContext.MaKhoHienTai && !string.IsNullOrWhiteSpace(SessionContext.TenKhoHienTai))
                    {
                        tenKhoHienThi = SessionContext.TenKhoHienTai;
                    }
                    else
                    {
                        object objTenKho = DataProcessing.ReadValue("SELECT TenKho FROM Kho WHERE MaKho = @MaKho", new SqlParameter[] { new SqlParameter("@MaKho", maKho) });
                        tenKhoHienThi = objTenKho != null ? objTenKho.ToString() : $"Kho #{maKho}";
                    }
                }

                // Ẩn / hiện cột Tên kho trên lưới tùy theo xem toàn hệ thống hay xem 1 kho
                if (dgvTheKho.Columns.Contains("colTenKho"))
                {
                    dgvTheKho.Columns["colTenKho"].Visible = (maKho == 0);
                }

                if (maHang <= 0)
                {
                    lblTenHangHienThi.Text = "Vui lòng chọn một mặt hàng...";
                    lblThongTinChiTiet.Text = $"Kho: {tenKhoHienThi}  |  ĐVT: --  |  Nhóm: --  |  Vị trí: --  |  Giá vốn: 0 đ";
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
                    lblThongTinChiTiet.Text = $"Kho: {tenKhoHienThi}  |  ĐVT: {dvt}  |  Nhóm: {nhom}  |  Vị trí: {vitri}  |  Giá vốn: {giaVon:N0} đ";
                }

                // 3. Đọc các tham số lọc

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
                      AND LoaiChungTu <> 'XUAT_HUY'
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
                           kh.TenKH AS PxTenKH, px.GhiChu AS PxGhiChu,
                           COALESCE(pn.MaPhieu, pth.MaPhieu, pc.MaPhieu, px.MaPhieu, pkk.MaPhieu) AS MaPhieuThat
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
                    LEFT JOIN PhieuXuat px ON (bd.LoaiChungTu = 'XUAT_BAN' OR bd.LoaiChungTu = 'XUAT_NOI_BO') AND bd.MaChungTu = px.MaPhieuXuat
                    LEFT JOIN DonBan db ON px.MaDonBan = db.MaDonBan
                    LEFT JOIN KhachHang kh ON db.MaKH = kh.MaKH
                    -- Mã phiếu kiểm kê (để hiển thị đúng mã chứng từ thật)
                    LEFT JOIN PhieuKiemKe pkk ON bd.LoaiChungTu = 'KIEM_KE' AND bd.MaChungTu = pkk.MaPhieuKiemKe
                    WHERE bd.MaHang = @MaHang
                      AND (@MaKho = 0 OR bd.MaKho = @MaKho)
                      AND bd.LoaiChungTu <> 'XUAT_HUY'
                      AND bd.NgayGiaoDich >= @TuNgay AND bd.NgayGiaoDich <= @DenNgay
                ");

                List<SqlParameter> pGd = new List<SqlParameter>
                {
                    new SqlParameter("@MaHang", maHang),
                    new SqlParameter("@MaKho", maKho),
                    new SqlParameter("@TuNgay", tuNgay),
                    new SqlParameter("@DenNgay", denNgay)
                };

                // Lọc theo loại chứng từ: KHÔNG lọc trong SQL để tồn lũy kế và tồn cuối kỳ luôn tính trên
                // toàn bộ giao dịch; bộ lọc chỉ quyết định dòng nào được hiển thị
                string[] maLoaiTheoIdx = { null, "PHIEU_NHAP", "XUAT_BAN", "CHUYEN_NHAP", "CHUYEN_XUAT", "KIEM_KE", "TRA_HANG_NHAP" };
                string loaiCanHienThi = (loaiChungTuIdx > 0 && loaiChungTuIdx < maLoaiTheoIdx.Length) ? maLoaiTheoIdx[loaiChungTuIdx] : null;

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
                            default:
                                maHienThi = maCt.ToString();
                                tenLoai = loaiCt;
                                doiTac = "Biến động kho";
                                break;
                        }

                        // Ưu tiên mã phiếu thật đã lưu trên chứng từ
                        if (r.Table.Columns.Contains("MaPhieuThat") && r["MaPhieuThat"] != DBNull.Value
                            && !string.IsNullOrWhiteSpace(r["MaPhieuThat"].ToString()))
                        {
                            maHienThi = r["MaPhieuThat"].ToString();
                        }

                        // Tồn lũy kế luôn tính trên toàn bộ giao dịch (trước khi áp bộ lọc hiển thị)
                        tonLuyKe = tonLuyKe + slTang - slGiam;
                        tongNhapKy += slTang;
                        tongXuatKy += slGiam;

                        // Lọc theo loại chứng từ (chỉ ẩn dòng, không ảnh hưởng số dư)
                        if (loaiCanHienThi != null && loaiCt != loaiCanHienThi) continue;

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
                else if (loai == "Xuất bán hàng" || loai == "Chuyển kho đi" || loai == "Trả hàng nhập")
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

                List<DataGridViewColumn> cols = new List<DataGridViewColumn>();
                foreach (DataGridViewColumn col in dgvTheKho.Columns)
                {
                    if (col.Visible && !string.IsNullOrWhiteSpace(col.HeaderText))
                    {
                        cols.Add(col);
                    }
                }

                using (SaveFileDialog sfd = new SaveFileDialog())
                {
                    sfd.Title = "Xuất báo cáo thẻ kho";
                    sfd.Filter = "File Excel (*.xls)|*.xls|File CSV (*.csv)|*.csv";
                    sfd.FileName = $"TheKho_{DateTime.Now:yyyyMMdd_HHmm}.xls";

                    if (sfd.ShowDialog(this) == DialogResult.OK)
                    {
                        string ext = Path.GetExtension(sfd.FileName).ToLowerInvariant();
                        if (ext == ".csv")
                        {
                            XuatCsvTheKho(sfd.FileName, cols);
                        }
                        else
                        {
                            XuatExcelHtmlTheKho(sfd.FileName, cols);
                        }

                        MessageBox.Show($"Xuất báo cáo thẻ kho ra file thành công!\nĐường dẫn: {sfd.FileName}", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi xuất file: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void XuatExcelHtmlTheKho(string filePath, List<DataGridViewColumn> cols)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("<html xmlns:o=\"urn:schemas-microsoft-com:office:office\" xmlns:x=\"urn:schemas-microsoft-com:office:excel\" xmlns=\"http://www.w3.org/TR/REC-html40\">");
            sb.AppendLine("<head>");
            sb.AppendLine("<meta http-equiv=\"Content-Type\" content=\"text/html; charset=utf-8\" />");
            sb.AppendLine("<style>");
            sb.AppendLine("body { font-family: Segoe UI, Arial; font-size: 10pt; }");
            sb.AppendLine("th { background-color: #0066CC; color: #FFFFFF; font-weight: bold; border: 0.5pt solid #CCCCCC; padding: 6px; }");
            sb.AppendLine("td { border: 0.5pt solid #E0E0E0; padding: 5px; }");
            sb.AppendLine(".num { mso-number-format: \"#,##0\"; text-align: right; }");
            sb.AppendLine(".center { text-align: center; }");
            sb.AppendLine(".danger { color: #DC2626; font-weight: bold; }");
            sb.AppendLine(".bold { font-weight: bold; }");
            sb.AppendLine("</style>");
            sb.AppendLine("</head>");
            sb.AppendLine("<body>");

            string tenKho = (SessionContext.MaKhoHienTai > 0 && !string.IsNullOrEmpty(SessionContext.TenKhoHienTai)) ? SessionContext.TenKhoHienTai : "Tất cả kho";
            sb.AppendLine($"<h2>BÁO CÁO THẺ KHO - {System.Net.WebUtility.HtmlEncode(lblTenHangHienThi.Text)}</h2>");
            sb.AppendLine($"<p><b>Mặt hàng:</b> {System.Net.WebUtility.HtmlEncode(lblThongTinChiTiet.Text)} | <b>Kho:</b> {System.Net.WebUtility.HtmlEncode(tenKho)}</p>");
            sb.AppendLine($"<p><b>Kỳ báo cáo:</b> Từ ngày {dtpTuNgay.Value:dd/MM/yyyy} đến ngày {dtpDenNgay.Value:dd/MM/yyyy}</p>");
            sb.AppendLine($"<p><b>Tồn đầu kỳ:</b> {lblTonDauVal.Text} | <b>Tổng nhập trong kỳ:</b> {lblNhapKyVal.Text} | <b>Tổng xuất trong kỳ:</b> {lblXuatKyVal.Text} | <b>Tồn cuối kỳ:</b> {lblTonCuoiVal.Text}</p>");
            sb.AppendLine("<br/>");

            sb.AppendLine("<table>");
            sb.AppendLine("<tr>");
            foreach (DataGridViewColumn col in cols)
            {
                sb.AppendLine($"<th>{System.Net.WebUtility.HtmlEncode(col.HeaderText)}</th>");
            }
            sb.AppendLine("</tr>");

            int totalRows = dtTheKhoHienThi.Rows.Count;
            for (int i = 0; i < totalRows; i++)
            {
                DataRow row = dtTheKhoHienThi.Rows[i];
                bool isSpecialRow = (i == 0 || i == totalRows - 1);
                string trTag = isSpecialRow ? "<tr style=\"background-color: #F1F5F9; font-weight: bold;\">" : "<tr>";
                sb.AppendLine(trTag);

                foreach (DataGridViewColumn col in cols)
                {
                    string prop = col.DataPropertyName;
                    string strVal = (prop != null && dtTheKhoHienThi.Columns.Contains(prop) && row[prop] != null) ? row[prop].ToString() : "";

                    if (col.Name == "colSoLuongTang" || col.Name == "colSoLuongGiam" || col.Name == "colTonLuyKe")
                    {
                        if (col.Name == "colTonLuyKe")
                        {
                            decimal tonNum = 0;
                            if (dtTheKhoHienThi.Columns.Contains("TonLuyKeNum") && row["TonLuyKeNum"] != DBNull.Value)
                            {
                                decimal.TryParse(row["TonLuyKeNum"].ToString(), out tonNum);
                            }
                            if (tonNum < 0)
                            {
                                sb.AppendLine($"<td class=\"num danger\">{strVal}</td>");
                            }
                            else
                            {
                                sb.AppendLine($"<td class=\"num\">{strVal}</td>");
                            }
                        }
                        else
                        {
                            sb.AppendLine($"<td class=\"num\">{strVal}</td>");
                        }
                    }
                    else if (col.Name == "colStt" || col.Name == "colNgayGiaoDich" || col.Name == "colMaChungTu" || col.Name == "colLoaiGiaoDich")
                    {
                        sb.AppendLine($"<td class=\"center\">{System.Net.WebUtility.HtmlEncode(strVal)}</td>");
                    }
                    else
                    {
                        sb.AppendLine($"<td>{System.Net.WebUtility.HtmlEncode(strVal)}</td>");
                    }
                }
                sb.AppendLine("</tr>");
            }

            sb.AppendLine("</table>");
            sb.AppendLine("</body>");
            sb.AppendLine("</html>");

            File.WriteAllText(filePath, sb.ToString(), Encoding.UTF8);
        }

        private void XuatCsvTheKho(string filePath, List<DataGridViewColumn> cols)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("sep=,");
            sb.AppendLine($"\"BÁO CÁO THẺ KHO - {lblTenHangHienThi.Text.Replace("\"", "\"\"")}\"");
            sb.AppendLine($"\"{lblThongTinChiTiet.Text.Replace("\"", "\"\"")}\"");
            sb.AppendLine($"\"Thời gian: Từ {dtpTuNgay.Value:dd/MM/yyyy} đến {dtpDenNgay.Value:dd/MM/yyyy}\"");
            sb.AppendLine();

            List<string> headers = new List<string>();
            foreach (DataGridViewColumn col in cols)
            {
                headers.Add($"\"{col.HeaderText.Replace("\"", "\"\"")}\"");
            }
            sb.AppendLine(string.Join(",", headers));

            foreach (DataRow row in dtTheKhoHienThi.Rows)
            {
                List<string> cells = new List<string>();
                foreach (DataGridViewColumn col in cols)
                {
                    string prop = col.DataPropertyName;
                    string strVal = (prop != null && dtTheKhoHienThi.Columns.Contains(prop) && row[prop] != null) ? row[prop].ToString().Replace("\"", "\"\"") : "";
                    cells.Add($"\"{strVal}\"");
                }
                sb.AppendLine(string.Join(",", cells));
            }

            File.WriteAllText(filePath, sb.ToString(), new UTF8Encoding(true));
        }

#if FALSE
        private void btnXuatFile_Click_Old(object sender, EventArgs e)
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

#endif

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
                "2. Kho: Thẻ kho hiển thị theo kho đang làm việc. Quản lý dùng Kho hàng > Đổi kho để xem kho khác hoặc 'Tất cả kho'.\n" +
                "   Bộ lọc 'Loại giao dịch' và ô tìm kiếm chỉ ẩn/hiện dòng; Tồn lũy kế và Tồn cuối kỳ luôn tính trên toàn bộ giao dịch.\n" +
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
