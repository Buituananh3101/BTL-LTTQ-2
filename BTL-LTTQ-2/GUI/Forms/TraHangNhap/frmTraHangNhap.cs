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
using System.Windows.Forms.VisualStyles;

namespace BTL_LTTQ_2.GUI.Forms.TraHangNhap
{
    public partial class frmTraHangNhap : Form
    {
        private bool isLoaded = false;
        private int trangHienTai = 1;
        private int soDongMoiTrang = 20;
        private int tongSoBanGhi = 0;
        private int tongSoTrang = 1;
        private bool isUpdatingCheckAll = false;
        private bool isHeaderChecked = false;
        private bool isHeaderHovered = false;
        private Timer searchDebounceTimer;

        public frmTraHangNhap()
        {
            InitializeComponent();
            DinhDangLuoi();
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
                if (isLoaded)
                {
                    trangHienTai = 1;
                    HienThiDanhSach();
                }
            };
        }

        private void txtTimKiem_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;
                searchDebounceTimer?.Stop();
                if (isLoaded)
                {
                    trangHienTai = 1;
                    HienThiDanhSach();
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

        private void DinhDangLuoi()
        {
            // Bật DoubleBuffered chống rung giật lưới khi render
            typeof(DataGridView).InvokeMember("DoubleBuffered",
                BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.SetProperty,
                null, dgvTraHangNhap, new object[] { true });

            dgvTraHangNhap.AutoGenerateColumns = false;
            dgvTraHangNhap.ReadOnly = false;
            dgvTraHangNhap.EditMode = DataGridViewEditMode.EditOnEnter;
            dgvTraHangNhap.RowTemplate.Height = 32;
            dgvTraHangNhap.RowHeadersVisible = false;
            dgvTraHangNhap.RowHeadersWidth = 51;

            // Màu dòng xen kẽ đồng bộ với Design System
            dgvTraHangNhap.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(248, 250, 252);
            dgvTraHangNhap.DefaultCellStyle.SelectionBackColor = Color.FromArgb(224, 231, 255);
            dgvTraHangNhap.DefaultCellStyle.SelectionForeColor = Color.FromArgb(30, 41, 59);

            colChon.Width = 45;
            colChon.ReadOnly = false;
            colMaPhieu.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            colThoiGian.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            colKho.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
            colTenNcc.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
            colTongTienHang.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            colGiamGia.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            colCanThuNcc.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            colDaThuNcc.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            colTrangThai.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            // Cấu hình co giãn các cột tự động lấp đầy DataGridView theo tỷ lệ
            colMaPhieu.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            colMaPhieu.FillWeight = 120;
            colThoiGian.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            colThoiGian.FillWeight = 140;
            colTenNcc.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            colTenNcc.FillWeight = 220;
            colTongTienHang.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            colTongTienHang.FillWeight = 130;
            colGiamGia.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            colGiamGia.FillWeight = 110;
            colCanThuNcc.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            colCanThuNcc.FillWeight = 130;
            colDaThuNcc.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            colDaThuNcc.FillWeight = 130;
            colTrangThai.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            colTrangThai.FillWeight = 120;

            colTongTienHang.DefaultCellStyle.Format = "N0";
            colGiamGia.DefaultCellStyle.Format = "N0";
            colCanThuNcc.DefaultCellStyle.Format = "N0";
            colDaThuNcc.DefaultCellStyle.Format = "N0";
            colThoiGian.DefaultCellStyle.Format = "dd/MM/yyyy HH:mm";

            // Đăng ký sự kiện phím Space chọn CheckBox
            dgvTraHangNhap.KeyDown += dgvTraHangNhap_KeyDown;
        }

        private void dgvTraHangNhap_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Space && dgvTraHangNhap.CurrentRow != null)
            {
                int rowIndex = dgvTraHangNhap.CurrentRow.Index;
                if (rowIndex >= 0 && rowIndex < dgvTraHangNhap.Rows.Count)
                {
                    bool cur = Convert.ToBoolean(dgvTraHangNhap.Rows[rowIndex].Cells["colChon"].Value ?? false);
                    dgvTraHangNhap.Rows[rowIndex].Cells["colChon"].Value = !cur;
                    dgvTraHangNhap.CommitEdit(DataGridViewDataErrorContexts.Commit);
                    CapNhatTrangThaiHeaderCheck();
                    e.Handled = true;
                }
            }
        }

        private void frmTraHangNhap_Load(object sender, EventArgs e)
        {
            dtpTuNgay.Value = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
            dtpDenNgay.Value = DateTime.Now;

            cboSoDongMoiTrang.SelectedItem = "20";

            NapDuLieuBoLoc();
            isLoaded = true;
            HienThiDanhSach();
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

            // 1. Trạng thái (ComboBox chuẩn hóa)
            try
            {
                DataTable dtTrangThai = new DataTable();
                dtTrangThai.Columns.Add("TenTrangThai", typeof(string));
                dtTrangThai.Columns.Add("MaTrangThai", typeof(string));

                dtTrangThai.Rows.Add("-- Tất cả trạng thái --", "");
                dtTrangThai.Rows.Add("Đã trả hàng", "DA_TRA_HANG");
                dtTrangThai.Rows.Add("Phiếu tạm", "NHAP");
                dtTrangThai.Rows.Add("Đã hủy", "HUY");

                cboTrangThai.DropDownStyle = ComboBoxStyle.DropDownList;
                cboTrangThai.DisplayMember = "TenTrangThai";
                cboTrangThai.ValueMember = "MaTrangThai";
                cboTrangThai.DataSource = dtTrangThai;
                cboTrangThai.SelectedIndex = 0;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi nạp trạng thái: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

            // 2. Nhà cung cấp (Hỗ trợ tìm kiếm theo text)
            try
            {
                string sqlNcc = "SELECT MaDonViCapHang, TenDonVi FROM DonViCapHang ORDER BY TenDonVi ASC";
                DataTable dtNcc = DataProcessing.ReadData(sqlNcc);
                DataRow drAllNcc = dtNcc.NewRow();
                drAllNcc["MaDonViCapHang"] = 0;
                drAllNcc["TenDonVi"] = "-- Tất cả nhà cung cấp --";
                dtNcc.Rows.InsertAt(drAllNcc, 0);

                ComboboxHelper.CaiDatTimKiem(cboNhaCungCap, dtNcc, "TenDonVi", "MaDonViCapHang", locDuLieu);
                cboNhaCungCap.SelectedIndex = 0;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi tải danh mục nhà cung cấp: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

            // 3. Người tạo (Hỗ trợ tìm kiếm theo text)
            try
            {
                string sqlNv = @"
                    SELECT tk.MaTaiKhoan, ISNULL(nv.HoTen, tk.TenDangNhap) AS HoTen 
                    FROM TaiKhoan tk 
                    LEFT JOIN NhanVien nv ON tk.MaNV = nv.MaNV 
                    WHERE tk.TrangThai = 'HOAT_DONG' OR tk.TrangThai = 'ACTIVE' OR tk.TrangThai IS NULL
                    ORDER BY HoTen ASC";
                DataTable dtNguoiTao = DataProcessing.ReadData(sqlNv);
                DataRow drAllNguoiTao = dtNguoiTao.NewRow();
                drAllNguoiTao["MaTaiKhoan"] = 0;
                drAllNguoiTao["HoTen"] = "-- Tất cả người tạo --";
                dtNguoiTao.Rows.InsertAt(drAllNguoiTao, 0);

                ComboboxHelper.CaiDatTimKiem(cboNguoiTao, dtNguoiTao, "HoTen", "MaTaiKhoan", locDuLieu);
                cboNguoiTao.SelectedIndex = 0;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi tải danh sách người tạo: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

            // 4. Người trả (Hỗ trợ tìm kiếm theo text)
            try
            {
                string sqlNv = @"
                    SELECT tk.MaTaiKhoan, ISNULL(nv.HoTen, tk.TenDangNhap) AS HoTen 
                    FROM TaiKhoan tk 
                    LEFT JOIN NhanVien nv ON tk.MaNV = nv.MaNV 
                    WHERE tk.TrangThai = 'HOAT_DONG' OR tk.TrangThai = 'ACTIVE' OR tk.TrangThai IS NULL
                    ORDER BY HoTen ASC";
                DataTable dtNguoiTra = DataProcessing.ReadData(sqlNv);
                DataRow drAllNguoiTra = dtNguoiTra.NewRow();
                drAllNguoiTra["MaTaiKhoan"] = 0;
                drAllNguoiTra["HoTen"] = "-- Tất cả người trả --";
                dtNguoiTra.Rows.InsertAt(drAllNguoiTra, 0);

                ComboboxHelper.CaiDatTimKiem(cboNguoiTra, dtNguoiTra, "HoTen", "MaTaiKhoan", locDuLieu);
                cboNguoiTra.SelectedIndex = 0;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi tải danh sách người trả: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        public void LoadData()
        {
            HienThiDanhSach();
        }

        public void HienThiDanhSach()
        {
            try
            {
                string maTrangThai = cboTrangThai.SelectedValue?.ToString();
                string filterTrangThaiSql = "";
                if (!string.IsNullOrEmpty(maTrangThai))
                {
                    filterTrangThaiSql = " AND pth.TrangThai = @TrangThai ";
                }

                int maNcc = 0;
                if (cboNhaCungCap.SelectedValue != null && int.TryParse(cboNhaCungCap.SelectedValue.ToString(), out int nccVal))
                {
                    maNcc = nccVal;
                }
                string filterNccSql = "";
                if (maNcc > 0)
                {
                    filterNccSql = " AND pth.MaDonViCapHang = @MaNCC ";
                }

                int maNvLap = 0;
                if (cboNguoiTao.SelectedValue != null && int.TryParse(cboNguoiTao.SelectedValue.ToString(), out int nvlVal))
                {
                    maNvLap = nvlVal;
                }
                string filterNvlSql = "";
                if (maNvLap > 0)
                {
                    filterNvlSql = " AND pth.MaNvlap = @MaNVLap ";
                }

                int maNvTra = 0;
                if (cboNguoiTra.SelectedValue != null && int.TryParse(cboNguoiTra.SelectedValue.ToString(), out int nvtVal))
                {
                    maNvTra = nvtVal;
                }
                string filterNvtSql = "";
                if (maNvTra > 0)
                {
                    filterNvtSql = " AND pth.MaNvtra = @MaNVTra ";
                }

                DateTime tuNgay = dtpTuNgay.Value.Date;
                int maKhoHienTai = SessionContext.MaKhoHienTai;
                string filterKhoSql = "";
                if (maKhoHienTai > 0)
                {
                    filterKhoSql = " AND pth.MaKho = @MaKho ";
                }
                DateTime denNgay = dtpDenNgay.Value.Date.AddDays(1);
                string timKiem = txtTimKiem.Text.Trim();

                string countSql = $@"
                    SELECT COUNT(1)
                    FROM PhieuTraHangNhap pth
                    LEFT JOIN DonViCapHang ncc ON pth.MaDonViCapHang = ncc.MaDonViCapHang
                    LEFT JOIN Kho k ON pth.MaKho = k.MaKho
                    WHERE (pth.NgayTra >= @TuNgay AND pth.NgayTra < @DenNgay)
                      {filterKhoSql}
                      {filterTrangThaiSql}
                      {filterNccSql}
                      {filterNvlSql}
                      {filterNvtSql}
                      AND (
                          @TimKiem = ''
                          OR pth.MaPhieu LIKE @TimKiemLike
                          OR ncc.TenDonVi LIKE @TimKiemLike
                          OR pth.GhiChu LIKE @TimKiemLike
                      )";

                List<SqlParameter> countParams = new List<SqlParameter>
                {
                    new SqlParameter("@TuNgay", tuNgay),
                    new SqlParameter("@DenNgay", denNgay),
                    new SqlParameter("@TimKiem", timKiem),
                    new SqlParameter("@TimKiemLike", "%" + timKiem + "%")
                };
                if (maKhoHienTai > 0) countParams.Add(new SqlParameter("@MaKho", maKhoHienTai));
                if (!string.IsNullOrEmpty(maTrangThai)) countParams.Add(new SqlParameter("@TrangThai", maTrangThai));
                if (maNcc > 0) countParams.Add(new SqlParameter("@MaNCC", maNcc));
                if (maNvLap > 0) countParams.Add(new SqlParameter("@MaNVLap", maNvLap));
                if (maNvTra > 0) countParams.Add(new SqlParameter("@MaNVTra", maNvTra));

                object countObj = DataProcessing.ReadValue(countSql, countParams.ToArray());
                tongSoBanGhi = countObj != null && countObj != DBNull.Value ? Convert.ToInt32(countObj) : 0;

                tongSoTrang = (int)Math.Ceiling((double)tongSoBanGhi / soDongMoiTrang);
                if (tongSoTrang < 1) tongSoTrang = 1;
                if (trangHienTai > tongSoTrang) trangHienTai = tongSoTrang;

                int skip = (trangHienTai - 1) * soDongMoiTrang;

                string querySql = $@"
                    SELECT 
                        CAST(0 AS BIT) AS Chon,
                        pth.MaPhieuTraHang,
                        ISNULL(pth.MaPhieu, 'THN' + RIGHT('000000' + CAST(pth.MaPhieuTraHang AS VARCHAR(10)), 6)) AS MaPhieu,
                        pth.NgayTra,
                        ISNULL(k.TenKho, N'') AS TenKho,
                        ISNULL(ncc.TenDonVi, N'Nhà cung cấp lẻ') AS TenDonVi,
                        pth.TongTienHang,
                        pth.GiamGia,
                        pth.CanThuNcc,
                        pth.DaThuNcc,
                        pth.TrangThai,
                        CASE pth.TrangThai 
                            WHEN 'NHAP' THEN N'Phiếu tạm'
                            WHEN 'DA_TRA_HANG' THEN N'Đã trả hàng'
                            WHEN 'HUY' THEN N'Đã hủy'
                            ELSE pth.TrangThai 
                        END AS TenTrangThai
                    FROM PhieuTraHangNhap pth
                    LEFT JOIN DonViCapHang ncc ON pth.MaDonViCapHang = ncc.MaDonViCapHang
                    LEFT JOIN Kho k ON pth.MaKho = k.MaKho
                    WHERE (pth.NgayTra >= @TuNgay AND pth.NgayTra < @DenNgay)
                      {filterKhoSql}
                      {filterTrangThaiSql}
                      {filterNccSql}
                      {filterNvlSql}
                      {filterNvtSql}
                      AND (
                          @TimKiem = ''
                          OR pth.MaPhieu LIKE @TimKiemLike
                          OR ncc.TenDonVi LIKE @TimKiemLike
                          OR pth.GhiChu LIKE @TimKiemLike
                      )
                    ORDER BY pth.NgayTra DESC, pth.MaPhieuTraHang DESC
                    OFFSET @Skip ROWS FETCH NEXT @Take ROWS ONLY";

                List<SqlParameter> queryParams = new List<SqlParameter>
                {
                    new SqlParameter("@TuNgay", tuNgay),
                    new SqlParameter("@DenNgay", denNgay),
                    new SqlParameter("@TimKiem", timKiem),
                    new SqlParameter("@TimKiemLike", "%" + timKiem + "%"),
                    new SqlParameter("@Skip", skip),
                    new SqlParameter("@Take", soDongMoiTrang)
                };
                if (maKhoHienTai > 0) queryParams.Add(new SqlParameter("@MaKho", maKhoHienTai));
                if (!string.IsNullOrEmpty(maTrangThai)) queryParams.Add(new SqlParameter("@TrangThai", maTrangThai));
                if (maNcc > 0) queryParams.Add(new SqlParameter("@MaNCC", maNcc));
                if (maNvLap > 0) queryParams.Add(new SqlParameter("@MaNVLap", maNvLap));
                if (maNvTra > 0) queryParams.Add(new SqlParameter("@MaNVTra", maNvTra));

                DataTable dt = DataProcessing.ReadData(querySql, queryParams.ToArray());

                if (!dt.Columns.Contains("Chon"))
                {
                    DataColumn col = new DataColumn("Chon", typeof(bool));
                    col.DefaultValue = false;
                    dt.Columns.Add(col);
                }

                dgvTraHangNhap.DataSource = dt;

                CapNhatThanhPhanTrang();
                isHeaderChecked = false;
                dgvTraHangNhap.Invalidate();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi nạp danh sách đơn trả hàng: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void CapNhatThanhPhanTrang()
        {


            lblTongSoPhieu.Text = $"Tổng số: {tongSoBanGhi:N0} phiếu trả hàng";
            lblTrangHienTai.Text = $"Trang {trangHienTai} / {tongSoTrang}";
            btnTrangTruoc.Enabled = trangHienTai > 1;
            btnTrangSau.Enabled = trangHienTai < tongSoTrang;
        }

        private void BoLoc_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (isLoaded)
            {
                trangHienTai = 1;
                HienThiDanhSach();
            }
        }

        private void txtTimKiem_TextChanged(object sender, EventArgs e)
        {
            searchDebounceTimer.Stop();
            searchDebounceTimer.Start();
        }

        private void cboSoDongMoiTrang_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (int.TryParse(cboSoDongMoiTrang.SelectedItem?.ToString(), out int val))
            {
                soDongMoiTrang = val;
                if (isLoaded)
                {
                    trangHienTai = 1;
                    HienThiDanhSach();
                }
            }
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

        private void btnThemTraHang_Click(object sender, EventArgs e)
        {
            using (frmThemPhieuTraHang frm = new frmThemPhieuTraHang())
            {
                if (frm.ShowDialog(this) == DialogResult.OK)
                {
                    HienThiDanhSach();
                }
            }
        }

        private void dgvTraHangNhap_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;
            if (dgvTraHangNhap.Columns[e.ColumnIndex].Name == "colChon") return;

            int maPhieuTra = Convert.ToInt32(dgvTraHangNhap.Rows[e.RowIndex].Cells["colMaPhieuTraHang"].Value);
            using (frmChiTietTraHangNhap frm = new frmChiTietTraHangNhap(maPhieuTra))
            {
                frm.ShowDialog(this);
                HienThiDanhSach();
            }
        }

        private void btnXuatFile_Click(object sender, EventArgs e)
        {
            if (dgvTraHangNhap.Rows.Count == 0 && tongSoBanGhi == 0)
            {
                MessageBox.Show("Không có dữ liệu đơn trả hàng nhập để xuất file!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            // Xác định danh sách dòng cần xuất: Ưu tiên các dòng được tích chọn CheckBox
            List<DataRow> danhSachXuat = new List<DataRow>();
            foreach (DataGridViewRow row in dgvTraHangNhap.Rows)
            {
                if (Convert.ToBoolean(row.Cells["colChon"].Value ?? false))
                {
                    DataRowView drv = row.DataBoundItem as DataRowView;
                    if (drv != null)
                    {
                        danhSachXuat.Add(drv.Row);
                    }
                }
            }

            bool coDongChon = danhSachXuat.Count > 0;
            if (!coDongChon)
            {
                try
                {
                    string maTrangThai = cboTrangThai.SelectedValue?.ToString();
                    string filterTrangThaiSql = "";
                    if (!string.IsNullOrEmpty(maTrangThai))
                    {
                        filterTrangThaiSql = " AND pth.TrangThai = @TrangThai ";
                    }

                    int maNcc = 0;
                    if (cboNhaCungCap.SelectedValue != null && int.TryParse(cboNhaCungCap.SelectedValue.ToString(), out int nccVal))
                    {
                        maNcc = nccVal;
                    }
                    string filterNccSql = "";
                    if (maNcc > 0)
                    {
                        filterNccSql = " AND pth.MaDonViCapHang = @MaNCC ";
                    }

                    int maNvLap = 0;
                    if (cboNguoiTao.SelectedValue != null && int.TryParse(cboNguoiTao.SelectedValue.ToString(), out int nvlVal))
                    {
                        maNvLap = nvlVal;
                    }
                    string filterNvlSql = "";
                    if (maNvLap > 0)
                    {
                        filterNvlSql = " AND pth.MaNvlap = @MaNVLap ";
                    }

                    int maNvTra = 0;
                    if (cboNguoiTra.SelectedValue != null && int.TryParse(cboNguoiTra.SelectedValue.ToString(), out int nvtVal))
                    {
                        maNvTra = nvtVal;
                    }
                    string filterNvtSql = "";
                    if (maNvTra > 0)
                    {
                        filterNvtSql = " AND pth.MaNvtra = @MaNVTra ";
                    }

                    DateTime tuNgay = dtpTuNgay.Value.Date;
                    int maKhoHienTai = SessionContext.MaKhoHienTai;
                    string filterKhoSql = "";
                    if (maKhoHienTai > 0)
                    {
                        filterKhoSql = " AND pth.MaKho = @MaKho ";
                    }
                    DateTime denNgay = dtpDenNgay.Value.Date.AddDays(1);
                    string timKiem = txtTimKiem.Text.Trim();

                    string exportSql = $@"
                        SELECT 
                            ISNULL(pth.MaPhieu, 'THN' + RIGHT('000000' + CAST(pth.MaPhieuTraHang AS VARCHAR(10)), 6)) AS MaPhieu,
                            pth.NgayTra,
                            ISNULL(k.TenKho, N'') AS TenKho,
                            ISNULL(ncc.TenDonVi, N'Nhà cung cấp lẻ') AS TenDonVi,
                            pth.TongTienHang,
                            pth.GiamGia,
                            pth.CanThuNcc,
                            pth.DaThuNcc,
                            CASE pth.TrangThai 
                                WHEN 'NHAP' THEN N'Phiếu tạm'
                                WHEN 'DA_TRA_HANG' THEN N'Đã trả hàng'
                                WHEN 'HUY' THEN N'Đã hủy'
                                ELSE pth.TrangThai 
                            END AS TenTrangThai
                        FROM PhieuTraHangNhap pth
                        LEFT JOIN DonViCapHang ncc ON pth.MaDonViCapHang = ncc.MaDonViCapHang
                        LEFT JOIN Kho k ON pth.MaKho = k.MaKho
                        WHERE (pth.NgayTra >= @TuNgay AND pth.NgayTra < @DenNgay)
                          {filterKhoSql}
                          {filterTrangThaiSql}
                          {filterNccSql}
                          {filterNvlSql}
                          {filterNvtSql}
                          AND (
                              @TimKiem = ''
                              OR pth.MaPhieu LIKE @TimKiemLike
                              OR ncc.TenDonVi LIKE @TimKiemLike
                              OR pth.GhiChu LIKE @TimKiemLike
                          )
                        ORDER BY pth.NgayTra DESC, pth.MaPhieuTraHang DESC";

                    List<SqlParameter> p = new List<SqlParameter>
                    {
                        new SqlParameter("@TuNgay", tuNgay),
                        new SqlParameter("@DenNgay", denNgay),
                        new SqlParameter("@TimKiem", timKiem),
                        new SqlParameter("@TimKiemLike", "%" + timKiem + "%")
                    };
                    if (maKhoHienTai > 0) p.Add(new SqlParameter("@MaKho", maKhoHienTai));
                    if (!string.IsNullOrEmpty(maTrangThai)) p.Add(new SqlParameter("@TrangThai", maTrangThai));
                    if (maNcc > 0) p.Add(new SqlParameter("@MaNCC", maNcc));
                    if (maNvLap > 0) p.Add(new SqlParameter("@MaNVLap", maNvLap));
                    if (maNvTra > 0) p.Add(new SqlParameter("@MaNVTra", maNvTra));

                    DataTable dtExport = DataProcessing.ReadData(exportSql, p.ToArray());
                    if (dtExport != null && dtExport.Rows.Count > 0)
                    {
                        foreach (DataRow r in dtExport.Rows)
                        {
                            danhSachXuat.Add(r);
                        }
                    }
                    else if (dgvTraHangNhap.DataSource is DataTable dt)
                    {
                        foreach (DataRow r in dt.Rows)
                        {
                            danhSachXuat.Add(r);
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi truy vấn dữ liệu xuất: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }

            if (danhSachXuat.Count == 0)
            {
                MessageBox.Show("Không tìm thấy dữ liệu phù hợp để xuất file!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            using (SaveFileDialog sfd = new SaveFileDialog())
            {
                sfd.Title = "Xuất danh sách đơn trả hàng nhập";
                sfd.Filter = "File Excel (*.xls)|*.xls|File CSV (*.csv)|*.csv";
                sfd.FileName = $"DanhSachTraHangNhap_{DateTime.Now:yyyyMMdd_HHmm}.xls";

                if (sfd.ShowDialog(this) == DialogResult.OK)
                {
                    try
                    {
                        string ext = Path.GetExtension(sfd.FileName).ToLowerInvariant();
                        if (ext == ".csv")
                        {
                            XuatCsvDanhSach(sfd.FileName, danhSachXuat);
                        }
                        else
                        {
                            XuatExcelHtmlDanhSach(sfd.FileName, danhSachXuat);
                        }

                        string thongBao = coDongChon
                            ? $"Xuất thành công {danhSachXuat.Count} phiếu trả hàng đã chọn ra file!\nĐường dẫn: {sfd.FileName}"
                            : $"Xuất toàn bộ {danhSachXuat.Count} phiếu trả hàng theo bộ lọc ra file thành công!\nĐường dẫn: {sfd.FileName}";

                        MessageBox.Show(thongBao, "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Lỗi khi xuất file: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        private void XuatExcelHtmlDanhSach(string filePath, IEnumerable<DataRow> danhSach)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("<html xmlns:o=\"urn:schemas-microsoft-com:office:office\" xmlns:x=\"urn:schemas-microsoft-com:office:excel\" xmlns=\"http://www.w3.org/TR/REC-html40\">");
            sb.AppendLine("<head>");
            sb.AppendLine("<meta http-equiv=\"Content-Type\" content=\"text/html; charset=utf-8\" />");
            sb.AppendLine("<style>");
            sb.AppendLine("body { font-family: Segoe UI, Arial; font-size: 10pt; }");
            sb.AppendLine("th { background-color: #0078D7; color: #FFFFFF; font-weight: bold; border: 0.5pt solid #CCCCCC; padding: 6px; }");
            sb.AppendLine("td { border: 0.5pt solid #E0E0E0; padding: 5px; }");
            sb.AppendLine(".num { mso-number-format: \"#,##0\"; text-align: right; }");
            sb.AppendLine(".center { text-align: center; }");
            sb.AppendLine("</style>");
            sb.AppendLine("</head>");
            sb.AppendLine("<body>");
            sb.AppendLine("<h2>DANH SÁCH ĐƠN TRẢ HÀNG NHẬP</h2>");
            sb.AppendLine($"<p>Thời gian xuất: {DateTime.Now:dd/MM/yyyy HH:mm} | Tổng số: {tongSoBanGhi:N0} phiếu</p>");
            sb.AppendLine("<table>");
            sb.AppendLine("<tr>");
            sb.AppendLine("<th>Mã trả hàng nhập</th>");
            sb.AppendLine("<th>Thời gian</th>");
            sb.AppendLine("<th>Kho hàng</th>");
            sb.AppendLine("<th>Nhà cung cấp</th>");
            sb.AppendLine("<th>Tổng tiền hàng</th>");
            sb.AppendLine("<th>Giảm giá</th>");
            sb.AppendLine("<th>NCC cần trả</th>");
            sb.AppendLine("<th>NCC đã trả</th>");
            sb.AppendLine("<th>Trạng thái</th>");
            sb.AppendLine("</tr>");

            foreach (DataRow r in danhSach)
            {
                string maPhieu = r["MaPhieu"]?.ToString() ?? "";
                string thoiGian = r["NgayTra"] != DBNull.Value ? Convert.ToDateTime(r["NgayTra"]).ToString("dd/MM/yyyy HH:mm") : "";
                string khoHang = r.Table.Columns.Contains("TenKho") ? (r["TenKho"]?.ToString() ?? "") : "";
                string tenNcc = r["TenDonVi"]?.ToString() ?? "";
                decimal tongTien = r["TongTienHang"] != DBNull.Value ? Convert.ToDecimal(r["TongTienHang"]) : 0m;
                decimal giamGia = r["GiamGia"] != DBNull.Value ? Convert.ToDecimal(r["GiamGia"]) : 0m;
                decimal canThu = r["CanThuNcc"] != DBNull.Value ? Convert.ToDecimal(r["CanThuNcc"]) : 0m;
                decimal daThu = r["DaThuNcc"] != DBNull.Value ? Convert.ToDecimal(r["DaThuNcc"]) : 0m;
                string trangThai = r["TenTrangThai"]?.ToString() ?? "";

                sb.AppendLine("<tr>");
                sb.AppendLine($"<td class=\"center\">{maPhieu}</td>");
                sb.AppendLine($"<td class=\"center\">{thoiGian}</td>");
                sb.AppendLine($"<td>{System.Net.WebUtility.HtmlEncode(khoHang)}</td>");
                sb.AppendLine($"<td>{System.Net.WebUtility.HtmlEncode(tenNcc)}</td>");
                sb.AppendLine($"<td class=\"num\">{tongTien:N0}</td>");
                sb.AppendLine($"<td class=\"num\">{giamGia:N0}</td>");
                sb.AppendLine($"<td class=\"num\">{canThu:N0}</td>");
                sb.AppendLine($"<td class=\"num\">{daThu:N0}</td>");
                sb.AppendLine($"<td class=\"center\">{trangThai}</td>");
                sb.AppendLine("</tr>");
            }

            sb.AppendLine("</table>");
            sb.AppendLine("</body>");
            sb.AppendLine("</html>");

            File.WriteAllText(filePath, sb.ToString(), Encoding.UTF8);
        }

        private void XuatCsvDanhSach(string filePath, IEnumerable<DataRow> danhSach)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("sep=,");
            sb.AppendLine("Mã trả hàng nhập,Thời gian,Kho hàng,Nhà cung cấp,Tổng tiền hàng,Giảm giá,NCC cần trả,NCC đã trả,Trạng thái");

            foreach (DataRow r in danhSach)
            {
                string maPhieu = r["MaPhieu"]?.ToString() ?? "";
                string thoiGian = r["NgayTra"] != DBNull.Value ? Convert.ToDateTime(r["NgayTra"]).ToString("dd/MM/yyyy HH:mm") : "";
                string khoHang = r.Table.Columns.Contains("TenKho") ? (r["TenKho"]?.ToString() ?? "") : "";
                string tenNcc = r["TenDonVi"]?.ToString() ?? "";
                decimal tongTien = r["TongTienHang"] != DBNull.Value ? Convert.ToDecimal(r["TongTienHang"]) : 0m;
                decimal giamGia = r["GiamGia"] != DBNull.Value ? Convert.ToDecimal(r["GiamGia"]) : 0m;
                decimal canThu = r["CanThuNcc"] != DBNull.Value ? Convert.ToDecimal(r["CanThuNcc"]) : 0m;
                decimal daThu = r["DaThuNcc"] != DBNull.Value ? Convert.ToDecimal(r["DaThuNcc"]) : 0m;
                string trangThai = r["TenTrangThai"]?.ToString() ?? "";

                sb.AppendLine(string.Format("\"{0}\",\"{1}\",\"{2}\",\"{3}\",{4},{5},{6},{7},\"{8}\"",
                    maPhieu,
                    thoiGian,
                    khoHang.Replace("\"", "\"\""),
                    tenNcc.Replace("\"", "\"\""),
                    tongTien,
                    giamGia,
                    canThu,
                    daThu,
                    trangThai));
            }

            File.WriteAllText(filePath, sb.ToString(), new UTF8Encoding(true));
        }

#if FALSE
        private void btnXuatFile_Click_Old(object sender, EventArgs e)
        {
            using (SaveFileDialog sfd = new SaveFileDialog())
            {
                sfd.Filter = "CSV File (*.csv)|*.csv|All Files (*.*)|*.*";
                sfd.FileName = $"DanhSachTraHangNhap_{DateTime.Now:yyyyMMdd_HHmm}.csv";

                if (sfd.ShowDialog(this) == DialogResult.OK)
                {
                    try
                    {
                        string maTrangThai = cboTrangThai.SelectedValue?.ToString();
                        string filterTrangThaiSql = "";
                        if (!string.IsNullOrEmpty(maTrangThai))
                        {
                            filterTrangThaiSql = " AND pth.TrangThai = @TrangThai ";
                        }

                        int maNcc = 0;
                        if (cboNhaCungCap.SelectedValue != null && int.TryParse(cboNhaCungCap.SelectedValue.ToString(), out int nccVal))
                        {
                            maNcc = nccVal;
                        }
                        string filterNccSql = "";
                        if (maNcc > 0)
                        {
                            filterNccSql = " AND pth.MaDonViCapHang = @MaNCC ";
                        }

                        int maNvLap = 0;
                        if (cboNguoiTao.SelectedValue != null && int.TryParse(cboNguoiTao.SelectedValue.ToString(), out int nvlVal))
                        {
                            maNvLap = nvlVal;
                        }
                        string filterNvlSql = "";
                        if (maNvLap > 0)
                        {
                            filterNvlSql = " AND pth.MaNvlap = @MaNVLap ";
                        }

                        int maNvTra = 0;
                        if (cboNguoiTra.SelectedValue != null && int.TryParse(cboNguoiTra.SelectedValue.ToString(), out int nvtVal))
                        {
                            maNvTra = nvtVal;
                        }
                        string filterNvtSql = "";
                        if (maNvTra > 0)
                        {
                            filterNvtSql = " AND pth.MaNvtra = @MaNVTra ";
                        }

                        DateTime tuNgay = dtpTuNgay.Value.Date;
                int maKhoHienTai = SessionContext.MaKhoHienTai;
                string filterKhoSql = "";
                if (maKhoHienTai > 0)
                {
                    filterKhoSql = " AND pth.MaKho = @MaKho ";
                }
                        DateTime denNgay = dtpDenNgay.Value.Date.AddDays(1);
                        string timKiem = txtTimKiem.Text.Trim();

                        string exportSql = $@"
                            SELECT 
                                ISNULL(pth.MaPhieu, 'THN' + RIGHT('000000' + CAST(pth.MaPhieuTraHang AS VARCHAR(10)), 6)) AS MaPhieu,
                                pth.NgayTra,
                        ISNULL(k.TenKho, N'') AS TenKho,
                                ISNULL(ncc.TenDonVi, N'Nhà cung cấp lẻ') AS TenDonVi,
                                pth.TongTienHang,
                                pth.GiamGia,
                                pth.CanThuNcc,
                                pth.DaThuNcc,
                                CASE pth.TrangThai 
                                    WHEN 'NHAP' THEN N'Phiếu tạm'
                                    WHEN 'DA_TRA_HANG' THEN N'Đã trả hàng'
                                    WHEN 'HUY' THEN N'Đã hủy'
                                    ELSE pth.TrangThai 
                                END AS TenTrangThai
                            FROM PhieuTraHangNhap pth
                            LEFT JOIN DonViCapHang ncc ON pth.MaDonViCapHang = ncc.MaDonViCapHang
                    LEFT JOIN Kho k ON pth.MaKho = k.MaKho
                            WHERE (pth.NgayTra >= @TuNgay AND pth.NgayTra < @DenNgay)
                              {filterKhoSql}
                      {filterTrangThaiSql}
                              {filterNccSql}
                              {filterNvlSql}
                              {filterNvtSql}
                              AND (
                                  @TimKiem = ''
                                  OR pth.MaPhieu LIKE @TimKiemLike
                                  OR ncc.TenDonVi LIKE @TimKiemLike
                                  OR pth.GhiChu LIKE @TimKiemLike
                              )
                            ORDER BY pth.NgayTra DESC, pth.MaPhieuTraHang DESC";

                        List<SqlParameter> p = new List<SqlParameter>
                        {
                            new SqlParameter("@TuNgay", tuNgay),
                            new SqlParameter("@DenNgay", denNgay),
                            new SqlParameter("@TimKiem", timKiem),
                            new SqlParameter("@TimKiemLike", "%" + timKiem + "%")
                        };
                        if (maKhoHienTai > 0) p.Add(new SqlParameter("@MaKho", maKhoHienTai));
                        if (!string.IsNullOrEmpty(maTrangThai)) p.Add(new SqlParameter("@TrangThai", maTrangThai));
                        if (maNcc > 0) p.Add(new SqlParameter("@MaNCC", maNcc));
                        if (maNvLap > 0) p.Add(new SqlParameter("@MaNVLap", maNvLap));
                        if (maNvTra > 0) p.Add(new SqlParameter("@MaNVTra", maNvTra));

                        DataTable dtExport = DataProcessing.ReadData(exportSql, p.ToArray());

                        StringBuilder sb = new StringBuilder();
                        sb.AppendLine("Mã trả hàng nhập,Thời gian,Nhà cung cấp,Tổng tiền hàng,Giảm giá,NCC cần trả,NCC đã trả,Trạng thái");

                        if (dtExport != null)
                        {
                            foreach (DataRow row in dtExport.Rows)
                            {
                                string maPhieu = EscapeCsv(row["MaPhieu"].ToString());
                                string thoiGian = Convert.ToDateTime(row["NgayTra"]).ToString("dd/MM/yyyy HH:mm");
                                string tenNcc = EscapeCsv(row["TenDonVi"].ToString());
                                string tongTien = Convert.ToDecimal(row["TongTienHang"]).ToString("N0");
                                string giamGia = Convert.ToDecimal(row["GiamGia"]).ToString("N0");
                                string canThu = Convert.ToDecimal(row["CanThuNcc"]).ToString("N0");
                                string daThu = Convert.ToDecimal(row["DaThuNcc"]).ToString("N0");
                                string trangThai = EscapeCsv(row["TenTrangThai"].ToString());

                                sb.AppendLine($"{maPhieu},{thoiGian},{tenNcc},{tongTien},{giamGia},{canThu},{daThu},{trangThai}");
                            }
                        }

                        File.WriteAllText(sfd.FileName, sb.ToString(), new UTF8Encoding(true));
                        MessageBox.Show("Xuất danh sách đơn trả hàng nhập ra file thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Lỗi khi xuất file: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

#endif

        private string EscapeCsv(string text)
        {
            if (string.IsNullOrEmpty(text)) return "\"\"";
            if (text.Contains(",") || text.Contains("\"") || text.Contains("\n") || text.Contains("\r"))
            {
                return "\"" + text.Replace("\"", "\"\"") + "\"";
            }
            return text;
        }

        private void btnHienCot_Click(object sender, EventArgs e)
        {
            foreach (Form openForm in Application.OpenForms)
            {
                if (openForm is frmHienCotTraHang)
                {
                    openForm.Activate();
                    openForm.BringToFront();
                    return;
                }
            }

            frmHienCotTraHang frm = new frmHienCotTraHang(dgvTraHangNhap);
            frm.StartPosition = FormStartPosition.Manual;

            Point btnPos = btnHienCot.PointToScreen(Point.Empty);
            int posX = btnPos.X + btnHienCot.Width - frm.Width;
            int posY = btnPos.Y + btnHienCot.Height + 2;

            Rectangle workingArea = Screen.FromControl(btnHienCot).WorkingArea;
            if (posX + frm.Width > workingArea.Right) posX = workingArea.Right - frm.Width;
            if (posX < workingArea.Left) posX = workingArea.Left;
            if (posY + frm.Height > workingArea.Bottom) posY = btnPos.Y - frm.Height - 2;

            frm.Location = new Point(posX, posY);
            frm.Show(this);
            frm.BringToFront();
        }

        private void btnHuongDanSuDung_Click(object sender, EventArgs e)
        {
            string huongDan = "HƯỚNG DẪN QUẢN LÝ TRẢ HÀNG NHẬP:\n\n" +
                              "1. Tìm kiếm: Nhập mã phiếu trả hoặc tên nhà cung cấp vào ô tìm kiếm.\n" +
                              "2. Bộ lọc bên trái: Chọn Trạng thái (Phiếu tạm, Đã trả hàng, Đã hủy), Nhà cung cấp, Người tạo, Người trả, Khoảng thời gian tạo.\n" +
                              "3. Lập phiếu trả hàng mới: Nhấp nút '+ Trả hàng nhập' để chọn nhà cung cấp và thêm mặt hàng cần trả từ danh mục hàng hóa.\n" +
                              "4. Xem chi tiết / Xóa / Hủy: Nhấp đúp chuột (Double click) vào bất kỳ dòng đơn trả nào trên bảng.\n" +
                              "5. Phân trang: Nhấp 'Trước' / 'Sau' hoặc chọn lại số dòng hiển thị mỗi trang ở góc dưới.\n" +
                              "6. Xuất file: Nhấp 'Xuất file' để lưu danh sách ra định dạng CSV chuẩn UTF-8 BOM mở bằng Excel.\n" +
                              "7. Hiện/Ẩn cột: Nhấp nút 'Hiện cột' để bật/tắt hiển thị các cột trên bảng.";

            MessageBox.Show(huongDan, "Hướng dẫn sử dụng", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void dgvTraHangNhap_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.RowIndex < 0) return;

            // Đổi màu hiển thị trạng thái
            if (dgvTraHangNhap.Columns[e.ColumnIndex].Name == "colTrangThai" && e.Value != null)
            {
                string tt = e.Value.ToString();
                if (tt == "Đã trả hàng")
                {
                    e.CellStyle.ForeColor = Color.FromArgb(22, 163, 74);
                }
                else if (tt == "Phiếu tạm")
                {
                    e.CellStyle.ForeColor = Color.FromArgb(217, 119, 6);
                }
                else if (tt == "Đã hủy")
                {
                    e.CellStyle.ForeColor = Color.FromArgb(100, 116, 139);
                }
            }
        }

        private void dgvTraHangNhap_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        {
            if (e.RowIndex == -1 && e.ColumnIndex >= 0 && dgvTraHangNhap.Columns[e.ColumnIndex].Name == "colChon")
            {
                e.PaintBackground(e.CellBounds, true);
                e.Paint(e.CellBounds, DataGridViewPaintParts.Border);

                CheckBoxState state = isHeaderChecked
                    ? (isHeaderHovered ? CheckBoxState.CheckedHot : CheckBoxState.CheckedNormal)
                    : (isHeaderHovered ? CheckBoxState.UncheckedHot : CheckBoxState.UncheckedNormal);

                Size glyphSize = CheckBoxRenderer.GetGlyphSize(e.Graphics, state);
                Point pt = new Point(
                    e.CellBounds.Left + (e.CellBounds.Width - glyphSize.Width) / 2,
                    e.CellBounds.Top + (e.CellBounds.Height - glyphSize.Height) / 2
                );

                CheckBoxRenderer.DrawCheckBox(e.Graphics, pt, state);
                e.Handled = true;
            }
        }

        private void dgvTraHangNhap_CellMouseMove(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.RowIndex == -1 && e.ColumnIndex >= 0 && dgvTraHangNhap.Columns[e.ColumnIndex].Name == "colChon")
            {
                if (!isHeaderHovered)
                {
                    isHeaderHovered = true;
                    dgvTraHangNhap.InvalidateCell(e.ColumnIndex, -1);
                }
            }
        }

        private void dgvTraHangNhap_CellMouseLeave(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex == -1 && e.ColumnIndex >= 0 && dgvTraHangNhap.Columns[e.ColumnIndex].Name == "colChon")
            {
                if (isHeaderHovered)
                {
                    isHeaderHovered = false;
                    dgvTraHangNhap.InvalidateCell(e.ColumnIndex, -1);
                }
            }
        }

        private void dgvTraHangNhap_ColumnHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.ColumnIndex >= 0 && dgvTraHangNhap.Columns[e.ColumnIndex].Name == "colChon")
            {
                dgvTraHangNhap.EndEdit();
                dgvTraHangNhap.CurrentCell = null;

                isHeaderChecked = !isHeaderChecked;
                isUpdatingCheckAll = true;

                DataTable dt = dgvTraHangNhap.DataSource as DataTable;
                if (dt != null)
                {
                    foreach (DataRow row in dt.Rows)
                    {
                        row["Chon"] = isHeaderChecked;
                    }
                }

                isUpdatingCheckAll = false;
                dgvTraHangNhap.Invalidate();
            }
        }

        private void dgvTraHangNhap_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (isUpdatingCheckAll) return;
            if (e.RowIndex >= 0 && e.ColumnIndex >= 0 && dgvTraHangNhap.Columns[e.ColumnIndex].Name == "colChon")
            {
                CapNhatTrangThaiHeaderCheck();
            }
        }

        private void dgvTraHangNhap_CurrentCellDirtyStateChanged(object sender, EventArgs e)
        {
            if (dgvTraHangNhap.IsCurrentCellDirty && dgvTraHangNhap.CurrentCell.ColumnIndex == colChon.Index)
            {
                dgvTraHangNhap.CommitEdit(DataGridViewDataErrorContexts.Commit);
            }
        }

        private void dgvTraHangNhap_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.ColumnIndex == colChon.Index)
            {
                dgvTraHangNhap.CommitEdit(DataGridViewDataErrorContexts.Commit);
            }
        }

        private void CapNhatTrangThaiHeaderCheck()
        {
            DataTable dt = dgvTraHangNhap.DataSource as DataTable;
            if (dt == null || dt.Rows.Count == 0)
            {
                isHeaderChecked = false;
            }
            else
            {
                bool allChecked = true;
                foreach (DataRow row in dt.Rows)
                {
                    if (row["Chon"] == DBNull.Value || !Convert.ToBoolean(row["Chon"]))
                    {
                        allChecked = false;
                        break;
                    }
                }
                isHeaderChecked = allChecked;
            }
            dgvTraHangNhap.InvalidateCell(colChon.Index, -1);
        }

        private void dgvTraHangNhap_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {
            e.Cancel = true;
            e.ThrowException = false;
        }
    }
}
