using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using BTL_LTTQ_2.BLL;
using BTL_LTTQ_2.DAL;
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

        public frmDanhSachHangHoa()
        {
            InitializeComponent();
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
            dgvHangHoa.RowTemplate.Height = 32;
            dgvHangHoa.BackgroundColor = Color.White;
            dgvHangHoa.BorderStyle = BorderStyle.None;

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

            // Cột 1: Mã hàng nội bộ
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

            // Cột 8: Dự kiến hết hàng
            dgvHangHoa.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "colDuKienHetHang",
                DataPropertyName = "DuKienHetHang",
                HeaderText = "Dự kiến hết",
                Width = 110,
                DefaultCellStyle = new DataGridViewCellStyle { Alignment = DataGridViewContentAlignment.MiddleCenter }
            });

            // Cột 9: Vị trí
            dgvHangHoa.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "colViTri",
                DataPropertyName = "ViTri",
                HeaderText = "Vị trí",
                Width = 90,
                DefaultCellStyle = new DataGridViewCellStyle { Alignment = DataGridViewContentAlignment.MiddleCenter }
            });

            // Cột 10: Thời gian tạo
            dgvHangHoa.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "colThoiGianTao",
                DataPropertyName = "NgayTao",
                HeaderText = "Thời gian tạo",
                Width = 140,
                DefaultCellStyle = new DataGridViewCellStyle 
                { 
                    Alignment = DataGridViewContentAlignment.MiddleCenter,
                    Format = "dd/MM/yyyy HH:mm"
                }
            });

            // Cột 11: Trạng thái
            dgvHangHoa.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "colTrangThai",
                DataPropertyName = "TrangThai",
                HeaderText = "Trạng thái",
                Width = 120,
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
            try
            {
                DataTable dtNhom = DataProcessing.ReadData("SELECT MaNhom, TenNhom FROM NhomHang WHERE TrangThai = 'HOAT_DONG' ORDER BY TenNhom");
                DataRow drAllNhom = dtNhom.NewRow();
                drAllNhom["MaNhom"] = 0;
                drAllNhom["TenNhom"] = "-- Tất cả nhóm hàng --";
                dtNhom.Rows.InsertAt(drAllNhom, 0);

                ComboboxHelper.CaiDatTimKiem(cboNhomHang, dtNhom, "TenNhom", "MaNhom", locDuLieu);
                cboNhomHang.SelectedIndex = 0;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi tải danh mục nhóm hàng: " + ex.Message, "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

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
            try
            {
                DataTable dtViTri = DataProcessing.ReadData("SELECT DISTINCT ViTri FROM HangHoa WHERE ViTri IS NOT NULL AND LTRIM(RTRIM(ViTri)) <> '' ORDER BY ViTri");
                List<string> listViTri = new List<string> { "-- Tất cả vị trí --" };
                foreach (DataRow row in dtViTri.Rows)
                {
                    listViTri.Add(row["ViTri"].ToString());
                }
                ComboboxHelper.CaiDatTimKiem(cboViTri, listViTri, locDuLieu);
            }
            catch
            {
                ComboboxHelper.CaiDatTimKiem(cboViTri, new List<string> { "-- Tất cả vị trí --" }, locDuLieu);
            }

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
                parameters.Add(new SqlParameter("@MaKho", SessionContext.MaKhoHienTai));

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
                string sqlCount = $@"
                    SELECT COUNT(DISTINCT h.MaHang)
                    FROM HangHoa h
                    LEFT JOIN NhomHang nh ON h.MaNhom = nh.MaNhom
                    LEFT JOIN DonViTinh dvt ON h.MaDVT = dvt.MaDVT
                    LEFT JOIN vw_TonKhoHienTai tk ON h.MaHang = tk.MaHang AND tk.MaKho = @MaKho
                    LEFT JOIN NguongTonKho ntk ON h.MaHang = ntk.MaHang AND ntk.MaKho = @MaKho
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
                    SELECT h.MaHang, h.MaHangNoiBo, h.TenHang, nh.TenNhom, dvt.TenDVT,
                           h.GiaBanThamKhao, h.GiaVon, ISNULL(tk.SoLuongTon, 0) AS SoLuongTon,
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
                    LEFT JOIN vw_TonKhoHienTai tk ON h.MaHang = tk.MaHang AND tk.MaKho = @MaKho
                    LEFT JOIN NguongTonKho ntk ON h.MaHang = ntk.MaHang AND ntk.MaKho = @MaKho
                    LEFT JOIN (
                        SELECT MaHang, SUM(SoLuongGiam) AS XuatBan30Ngay
                        FROM vw_BienDongKho
                        WHERE MaKho = @MaKho 
                          AND SoLuongGiam > 0
                          AND NgayGiaoDich >= DATEADD(day, -30, GETDATE())
                        GROUP BY MaHang
                    ) x30 ON h.MaHang = x30.MaHang
                    {whereSql}
                    ORDER BY h.MaHang DESC
                    OFFSET @Skip ROWS FETCH NEXT @Take ROWS ONLY;";

                DataTable dtHangHoa = DataProcessing.ReadData(sqlData, parameters.ToArray());
                dgvHangHoa.DataSource = dtHangHoa;

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
            trangHienTai = 1;
            HienThiDanhSach();
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

        #endregion

        #region Hành động & Nút chức năng

        // Click đúp vào dòng để mở form sửa
        private void dgvHangHoa_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;

            var cellMaHang = dgvHangHoa.Rows[e.RowIndex].Cells["colMaHang"].Value;
            if (cellMaHang != null && int.TryParse(cellMaHang.ToString(), out int maHang))
            {
                MoFormHangHoa(maHang);
            }
        }

        // Bấm nút + Hàng hóa mở form thêm mới
        private void btnHangHoa_Click(object sender, EventArgs e)
        {
            MoFormHangHoa(0);
        }

        private void MoFormHangHoa(int maHang)
        {
            if (this.MdiParent != null)
            {
                // Kiểm tra nếu đã mở form frmHangHoa thì kích hoạt
                foreach (Form child in this.MdiParent.MdiChildren)
                {
                    if (child is frmHangHoa)
                    {
                        child.Activate();
                        return;
                    }
                }

                frmHangHoa frm = maHang > 0 ? new frmHangHoa(maHang) : new frmHangHoa();
                frm.MdiParent = this.MdiParent;
                frm.StartPosition = FormStartPosition.Manual;
                int posX = Math.Max(0, (this.MdiParent.ClientSize.Width - frm.Width) / 2);
                int posY = Math.Max(0, (this.MdiParent.ClientSize.Height - frm.Height) / 2);
                frm.Location = new Point(posX, posY);

                frm.FormClosed += (s, ev) =>
                {
                    if (frm.DialogResult == DialogResult.OK)
                    {
                        HienThiDanhSach();
                    }
                };
                frm.Show();
                frm.BringToFront();
            }
            else
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
        }

        // Xuất danh sách ra file CSV / Excel
        private void btnXuatFile_Click(object sender, EventArgs e)
        {
            if (dgvHangHoa.Rows.Count == 0)
            {
                MessageBox.Show("Không có dữ liệu để xuất file!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            using (SaveFileDialog sfd = new SaveFileDialog())
            {
                sfd.Filter = "File CSV (*.csv)|*.csv|Tất cả tệp (*.*)|*.*";
                sfd.FileName = $"DanhSachHangHoa_{DateTime.Now:yyyyMMdd_HHmm}.csv";

                if (sfd.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        StringBuilder sb = new StringBuilder();

                        // Ghi tiêu đề cột (bỏ cột ẩn colMaHang)
                        List<string> headers = new List<string>();
                        foreach (DataGridViewColumn col in dgvHangHoa.Columns)
                        {
                            if (col.Visible) headers.Add($"\"{col.HeaderText}\"");
                        }
                        sb.AppendLine(string.Join(",", headers));

                        // Ghi từng dòng dữ liệu
                        foreach (DataGridViewRow row in dgvHangHoa.Rows)
                        {
                            List<string> rowValues = new List<string>();
                            foreach (DataGridViewColumn col in dgvHangHoa.Columns)
                            {
                                if (col.Visible)
                                {
                                    string cellVal = row.Cells[col.Index].FormattedValue?.ToString() ?? "";
                                    rowValues.Add($"\"{cellVal.Replace("\"", "\"\"")}\"");
                                }
                            }
                            sb.AppendLine(string.Join(",", rowValues));
                        }

                        // Ghi ra file với UTF-8 BOM để Excel hiển thị đúng tiếng Việt
                        File.WriteAllText(sfd.FileName, sb.ToString(), Encoding.UTF8);
                        MessageBox.Show("Xuất file thành công tại:\n" + sfd.FileName, "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
