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

namespace BTL_LTTQ_2.GUI.Forms.NhaCungCap
{
    public partial class frmNhaCungCap : Form
    {
        // Các biến phục vụ phân trang
        private int trangHienTai = 1;
        private int soDongMoiTrang = 20;
        private int tongSoBanGhi = 0;
        private int tongSoTrang = 1;

        // Cờ kiểm soát tránh load lặp khi khởi tạo combobox
        private bool isLoaded = false;
        private bool isHeaderChecked = false;
        private bool isUpdatingCheckAll = false;
        private bool isHeaderHovered = false;

        private Timer searchDebounceTimer;

        public frmNhaCungCap()
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

        private void frmNhaCungCap_Load(object sender, EventArgs e)
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
                null, dgvNhaCungCap, new object[] { true });

            dgvNhaCungCap.AutoGenerateColumns = false;
            dgvNhaCungCap.Columns.Clear();
            dgvNhaCungCap.AllowUserToAddRows = false;
            dgvNhaCungCap.AllowUserToDeleteRows = false;
            dgvNhaCungCap.AllowUserToResizeRows = false;
            dgvNhaCungCap.ReadOnly = false;
            dgvNhaCungCap.EditMode = DataGridViewEditMode.EditOnEnter;
            dgvNhaCungCap.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvNhaCungCap.MultiSelect = false;
            dgvNhaCungCap.RowHeadersVisible = false;
            dgvNhaCungCap.RowTemplate.Height = 32;
            dgvNhaCungCap.BackgroundColor = Color.White;
            dgvNhaCungCap.BorderStyle = BorderStyle.None;

            // Màu dòng xen kẽ hiện đại chuẩn KiotViet
            dgvNhaCungCap.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(248, 250, 252);
            dgvNhaCungCap.DefaultCellStyle.SelectionBackColor = Color.FromArgb(224, 231, 255);
            dgvNhaCungCap.DefaultCellStyle.SelectionForeColor = Color.FromArgb(30, 41, 59);

            // Cột 0: Mã ID ngầm
            dgvNhaCungCap.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "colMaDonViCapHang",
                DataPropertyName = "MaDonViCapHang",
                Visible = false,
                ReadOnly = true
            });

            // Cột 1: Hộp kiểm chọn (Checkbox)
            dgvNhaCungCap.Columns.Add(new DataGridViewCheckBoxColumn
            {
                Name = "colChon",
                DataPropertyName = "Chon",
                HeaderText = "",
                Width = 45,
                ReadOnly = false,
                SortMode = DataGridViewColumnSortMode.NotSortable,
                TrueValue = true,
                FalseValue = false,
                DefaultCellStyle = new DataGridViewCellStyle
                {
                    Alignment = DataGridViewContentAlignment.MiddleCenter,
                    NullValue = false
                }
            });

            // Cột 2: Mã nhà cung cấp
            dgvNhaCungCap.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "colMaNCC",
                DataPropertyName = "MaNcc",
                HeaderText = "Mã nhà cung cấp",
                Width = 140,
                ReadOnly = true,
                DefaultCellStyle = new DataGridViewCellStyle { Alignment = DataGridViewContentAlignment.MiddleCenter }
            });

            // Cột 3: Tên nhà cung cấp
            dgvNhaCungCap.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "colTenNCC",
                DataPropertyName = "TenDonVi",
                HeaderText = "Tên nhà cung cấp",
                AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill,
                ReadOnly = true,
                DefaultCellStyle = new DataGridViewCellStyle { Alignment = DataGridViewContentAlignment.MiddleLeft }
            });

            // Cột 4: Điện thoại
            dgvNhaCungCap.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "colDienThoai",
                DataPropertyName = "DienThoai",
                HeaderText = "Điện thoại",
                Width = 130,
                ReadOnly = true,
                DefaultCellStyle = new DataGridViewCellStyle { Alignment = DataGridViewContentAlignment.MiddleCenter }
            });

            // Cột 5: Email
            dgvNhaCungCap.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "colEmail",
                DataPropertyName = "Email",
                HeaderText = "Email",
                Width = 180,
                ReadOnly = true,
                DefaultCellStyle = new DataGridViewCellStyle { Alignment = DataGridViewContentAlignment.MiddleLeft }
            });

            // Cột 6: Nợ cần trả hiện tại
            dgvNhaCungCap.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "colNoCanTra",
                DataPropertyName = "NoCanTraHienTai",
                HeaderText = "Nợ cần trả hiện tại",
                Width = 160,
                ReadOnly = true,
                DefaultCellStyle = new DataGridViewCellStyle { Alignment = DataGridViewContentAlignment.MiddleRight, Format = "N0" }
            });

            // Cột 7: Tổng mua
            dgvNhaCungCap.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "colTongMua",
                DataPropertyName = "TongMua",
                HeaderText = "Tổng mua",
                Width = 140,
                ReadOnly = true,
                DefaultCellStyle = new DataGridViewCellStyle { Alignment = DataGridViewContentAlignment.MiddleRight, Format = "N0" }
            });

            // Cột 8: Trạng thái
            dgvNhaCungCap.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "colTrangThai",
                DataPropertyName = "TrangThai",
                HeaderText = "Trạng thái",
                Width = 130,
                ReadOnly = true,
                DefaultCellStyle = new DataGridViewCellStyle { Alignment = DataGridViewContentAlignment.MiddleCenter }
            });
        }

        public void NapDanhSachNhomNCC(int maNhomChon = 0, Action locDuLieu = null)
        {
            try
            {
                string sqlNhom = @"
                    IF NOT EXISTS (SELECT 1 FROM sys.tables WHERE name = N'NhomNhaCungCap')
                    BEGIN
                        CREATE TABLE NhomNhaCungCap (
                            MaNhomNcc INT IDENTITY(1,1) PRIMARY KEY,
                            TenNhom NVARCHAR(150) NOT NULL,
                            GhiChu NVARCHAR(255) NULL
                        );
                    END;
                    SELECT MaNhomNcc, TenNhom FROM NhomNhaCungCap ORDER BY TenNhom;";

                DataTable dtNhom = DataProcessing.ReadData(sqlNhom);
                DataRow drAllNhom = dtNhom.NewRow();
                drAllNhom["MaNhomNcc"] = 0;
                drAllNhom["TenNhom"] = "-- Tất cả các nhóm --";
                dtNhom.Rows.InsertAt(drAllNhom, 0);

                Action action = locDuLieu ?? new Action(() =>
                {
                    if (isLoaded)
                    {
                        trangHienTai = 1;
                        HienThiDanhSach();
                    }
                });

                ComboboxHelper.CaiDatTimKiem(cboNhomNCC, dtNhom, "TenNhom", "MaNhomNcc", action);

                if (maNhomChon > 0)
                {
                    cboNhomNCC.SelectedValue = maNhomChon;
                }
                else
                {
                    cboNhomNCC.SelectedIndex = 0;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi tải danh mục nhóm nhà cung cấp: " + ex.Message, "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        public void NapDuLieuBoLoc()
        {
            // 1. Nhóm nhà cung cấp
            NapDanhSachNhomNCC();

            // 2. Mức tổng mua (dạng ComboBox)
            cboMucTongMua.Items.Clear();
            cboMucTongMua.Items.AddRange(new object[] {
                "-- Tất cả mức mua --",
                "Dưới 5 triệu (< 5.000.000 đ)",
                "Từ 5 - 20 triệu",
                "Từ 20 - 50 triệu",
                "Trên 50 triệu (> 50.000.000 đ)"
            });
            cboMucTongMua.SelectedIndex = 0;

            // 3. Mức nợ hiện tại (dạng ComboBox)
            cboNoHienTai.Items.Clear();
            cboNoHienTai.Items.AddRange(new object[] {
                "-- Tất cả công nợ --",
                "Còn nợ (> 0)",
                "Hết nợ (= 0)",
                "Nợ lớn (> 10 triệu)",
                "Nợ rất lớn (> 50 triệu)"
            });
            cboNoHienTai.SelectedIndex = 0;

            // 4. Trạng thái (dạng ComboBox)
            cboTrangThai.Items.Clear();
            cboTrangThai.Items.AddRange(new object[] {
                "-- Tất cả trạng thái --",
                "Đang hoạt động",
                "Ngừng hoạt động"
            });
            cboTrangThai.SelectedIndex = 0;

            // 5. Khoảng thời gian tạo (Mặc định từ đầu năm đến ngày hiện tại, đồng bộ frmDanhSachHangHoa)
            dtpTuNgay.Value = new DateTime(DateTime.Now.Year, 1, 1);
            dtpDenNgay.Value = DateTime.Today;

            // 6. Số dòng mỗi trang
            if (cboSoDongMoiTrang.Items.Count > 0 && cboSoDongMoiTrang.SelectedIndex < 0)
            {
                cboSoDongMoiTrang.SelectedItem = "20";
            }
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

                // Lọc từ khóa tìm kiếm (Mã NCC, Tên NCC, Số điện thoại)
                if (!string.IsNullOrWhiteSpace(txtTimKiem.Text))
                {
                    whereSql.Append(" AND (c.MaNcc LIKE @Search OR c.TenDonVi LIKE @Search OR c.DienThoai LIKE @Search) ");
                    parameters.Add(new SqlParameter("@Search", "%" + txtTimKiem.Text.Trim() + "%"));
                }

                // Lọc nhóm NCC
                if (cboNhomNCC.SelectedValue != null && int.TryParse(cboNhomNCC.SelectedValue.ToString(), out int maNhom) && maNhom > 0)
                {
                    whereSql.Append(" AND c.MaNhomNcc = @MaNhom ");
                    parameters.Add(new SqlParameter("@MaNhom", maNhom));
                }

                // Lọc trạng thái
                if (cboTrangThai.SelectedIndex == 1) // Đang hoạt động
                {
                    whereSql.Append(" AND c.TrangThai = 'HOAT_DONG' ");
                }
                else if (cboTrangThai.SelectedIndex == 2) // Ngừng hoạt động
                {
                    whereSql.Append(" AND c.TrangThai IN ('NGUNG_GIAO_DICH', 'NGUNG_HOAT_DONG') ");
                }

                // Lọc mức tổng mua
                if (cboMucTongMua.SelectedIndex == 1) // < 5tr
                {
                    whereSql.Append(" AND c.TongMua < 5000000 ");
                }
                else if (cboMucTongMua.SelectedIndex == 2) // 5 - 20tr
                {
                    whereSql.Append(" AND c.TongMua >= 5000000 AND c.TongMua <= 20000000 ");
                }
                else if (cboMucTongMua.SelectedIndex == 3) // 20 - 50tr
                {
                    whereSql.Append(" AND c.TongMua > 20000000 AND c.TongMua <= 50000000 ");
                }
                else if (cboMucTongMua.SelectedIndex == 4) // > 50tr
                {
                    whereSql.Append(" AND c.TongMua > 50000000 ");
                }

                // Lọc mức nợ hiện tại
                if (cboNoHienTai.SelectedIndex == 1) // Còn nợ (> 0)
                {
                    whereSql.Append(" AND c.NoCanTraHienTai > 0 ");
                }
                else if (cboNoHienTai.SelectedIndex == 2) // Hết nợ (= 0)
                {
                    whereSql.Append(" AND c.NoCanTraHienTai = 0 ");
                }
                else if (cboNoHienTai.SelectedIndex == 3) // Nợ lớn (> 10tr)
                {
                    whereSql.Append(" AND c.NoCanTraHienTai > 10000000 ");
                }
                else if (cboNoHienTai.SelectedIndex == 4) // Nợ rất lớn (> 50tr)
                {
                    whereSql.Append(" AND c.NoCanTraHienTai > 50000000 ");
                }

                // Lọc theo khoảng ngày tạo (đồng bộ frmDanhSachHangHoa)
                whereSql.Append(" AND c.NgayTao >= @TuNgay AND c.NgayTao < @DenNgay ");
                parameters.Add(new SqlParameter("@TuNgay", dtpTuNgay.Value.Date));
                parameters.Add(new SqlParameter("@DenNgay", dtpDenNgay.Value.Date.AddDays(1)));

                // Truy vấn CTE linh hoạt, tự động sinh mã hiển thị nếu MaNcc đang NULL
                string cteSql = $@"
                    WITH CTE_NhaCungCap AS (
                        SELECT 
                            ncc.MaDonViCapHang,
                            ISNULL(ncc.MaNcc, 'NCC' + RIGHT('00000' + CAST(ncc.MaDonViCapHang AS VARCHAR(10)), 5)) AS MaNcc,
                            ncc.TenDonVi,
                            ncc.DienThoai,
                            ncc.Email,
                            ncc.DiaChi,
                            ncc.TrangThai,
                            ISNULL(ncc.NgayTao, GETDATE()) AS NgayTao,
                            ISNULL(ncc.MaNhomNcc, 0) AS MaNhomNcc,
                            ISNULL(nhom.TenNhom, N'-- Chưa phân nhóm --') AS TenNhom,
                            ISNULL((
                                SELECT SUM(pn.TongTien) 
                                FROM PhieuNhap pn 
                                WHERE pn.MaDonViCapHang = ncc.MaDonViCapHang 
                                  AND pn.TrangThai IN ('DA_XAC_NHAN', 'DA_NHAN')
                            ), 0) AS TongMua,
                            ISNULL((
                                SELECT SUM(pn.TongTien) 
                                FROM PhieuNhap pn 
                                WHERE pn.MaDonViCapHang = ncc.MaDonViCapHang 
                                  AND pn.TrangThai IN ('DA_XAC_NHAN', 'DA_NHAN')
                            ), 0) AS NoCanTraHienTai
                        FROM DonViCapHang ncc
                        LEFT JOIN NhomNhaCungCap nhom ON ncc.MaNhomNcc = nhom.MaNhomNcc
                    )";

                // 1. Đếm tổng số bản ghi
                string sqlCount = $@"
                    {cteSql}
                    SELECT COUNT(1) FROM CTE_NhaCungCap c
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

                // 3. Lấy dữ liệu phân trang (bao gồm cột Chon kiểu BIT tránh lỗi cell formatting)
                string sqlData = $@"
                    {cteSql}
                    SELECT CAST(0 AS BIT) AS Chon,
                           c.MaDonViCapHang, c.MaNcc, c.TenDonVi, c.DienThoai, c.Email,
                           c.NoCanTraHienTai, c.TongMua, c.TrangThai, c.TenNhom, c.NgayTao
                    FROM CTE_NhaCungCap c
                    {whereSql}
                    ORDER BY c.MaDonViCapHang DESC
                    OFFSET @Skip ROWS FETCH NEXT @Take ROWS ONLY;";

                DataTable dtNCC = DataProcessing.ReadData(sqlData, parameters.ToArray());

                // Đảm bảo cột Chon tồn tại và không bị giá trị NULL gây FormatException
                if (dtNCC != null)
                {
                    if (!dtNCC.Columns.Contains("Chon"))
                    {
                        dtNCC.Columns.Add("Chon", typeof(bool));
                    }
                    foreach (DataRow row in dtNCC.Rows)
                    {
                        if (row["Chon"] == DBNull.Value)
                        {
                            row["Chon"] = false;
                        }
                    }
                }

                dgvNhaCungCap.DataSource = dtNCC;
                isHeaderChecked = false;
                CapNhatTrangThaiHeaderCheck();

                // 4. Cập nhật thanh phân trang
                CapNhatGiaoDienPhanTrang();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi tải danh sách nhà cung cấp: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void CapNhatGiaoDienPhanTrang()
        {
            lblTrangHienTai.Text = $"Trang {trangHienTai} / {tongSoTrang}";


            lblTongSoNCC.Text = $"Tổng số: {tongSoBanGhi:N0} nhà cung cấp";

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

        private void dgvNhaCungCap_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {
            // Ngăn chặn DataGridView hiển thị hộp thoại FormatException mặc định
            e.Cancel = true;
            e.ThrowException = false;
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

        private void dgvNhaCungCap_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.RowIndex < 0 || e.RowIndex >= dgvNhaCungCap.Rows.Count) return;

            string colName = dgvNhaCungCap.Columns[e.ColumnIndex].Name;

            // Xử lý an toàn cho cột hộp kiểm tránh lỗi FormatException
            if (colName == "colChon")
            {
                if (e.Value == null || e.Value == DBNull.Value)
                {
                    e.Value = false;
                }
                e.FormattingApplied = true;
                return;
            }

            // Định dạng hiển thị trạng thái
            if (colName == "colTrangThai" && e.Value != null)
            {
                string val = e.Value.ToString();
                if (val == "HOAT_DONG")
                {
                    e.Value = "Đang hoạt động";
                    e.CellStyle.ForeColor = Color.ForestGreen;
                }
                else if (val == "NGUNG_GIAO_DICH" || val == "NGUNG_HOAT_DONG")
                {
                    e.Value = "Ngừng hoạt động";
                    e.CellStyle.ForeColor = Color.Gray;
                }
            }

            // Nổi bật nợ cần trả nếu > 0
            if (colName == "colNoCanTra" && e.Value != null)
            {
                if (decimal.TryParse(e.Value.ToString(), out decimal no) && no > 0)
                {
                    e.CellStyle.ForeColor = Color.FromArgb(220, 38, 38);
                    e.CellStyle.Font = new Font(dgvNhaCungCap.Font, FontStyle.Bold);
                }
            }
        }

        // Xử lý commit ngay khi người dùng tích/bỏ tích ô kiểm
        private void dgvNhaCungCap_CurrentCellDirtyStateChanged(object sender, EventArgs e)
        {
            if (dgvNhaCungCap.IsCurrentCellDirty && dgvNhaCungCap.CurrentCell is DataGridViewCheckBoxCell)
            {
                dgvNhaCungCap.CommitEdit(DataGridViewDataErrorContexts.Commit);
            }
        }

        private void dgvNhaCungCap_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && dgvNhaCungCap.Columns[e.ColumnIndex].Name == "colChon")
            {
                dgvNhaCungCap.CommitEdit(DataGridViewDataErrorContexts.Commit);
            }
        }

        private void dgvNhaCungCap_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (isUpdatingCheckAll) return;
            if (e.RowIndex >= 0 && dgvNhaCungCap.Columns[e.ColumnIndex].Name == "colChon")
            {
                CapNhatTrangThaiHeaderCheck();
            }
        }

        // Vẽ hộp kiểm ở tiêu đề cột colChon (Chọn tất cả) - Dùng CheckBoxRenderer chuẩn 100% giống các ô bên dưới
        private void dgvNhaCungCap_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        {
            if (e.RowIndex == -1 && e.ColumnIndex >= 0 && dgvNhaCungCap.Columns[e.ColumnIndex].Name == "colChon")
            {
                // 1. Vẽ nền và viền tiêu đề cột chuẩn của DataGridView
                e.PaintBackground(e.CellBounds, true);
                e.Paint(e.CellBounds, DataGridViewPaintParts.Border);

                // 2. Xác định trạng thái Checkbox
                CheckBoxState state;
                if (isHeaderChecked)
                {
                    state = isHeaderHovered ? CheckBoxState.CheckedHot : CheckBoxState.CheckedNormal;
                }
                else
                {
                    state = isHeaderHovered ? CheckBoxState.UncheckedHot : CheckBoxState.UncheckedNormal;
                }

                // 3. Lấy kích thước glyph chuẩn theo hệ thống (VisualStyles)
                Size glyphSize = CheckBoxRenderer.GetGlyphSize(e.Graphics, state);
                Point pt = new Point(
                    e.CellBounds.Left + (e.CellBounds.Width - glyphSize.Width) / 2,
                    e.CellBounds.Top + (e.CellBounds.Height - glyphSize.Height) / 2
                );

                // 4. Vẽ ô kiểm giống hệt các ô bên dưới
                CheckBoxRenderer.DrawCheckBox(e.Graphics, pt, state);
                e.Handled = true;
            }
        }

        // Hiệu ứng rê chuột trên Header CheckBox
        private void dgvNhaCungCap_CellMouseMove(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.RowIndex == -1 && e.ColumnIndex >= 0 && dgvNhaCungCap.Columns[e.ColumnIndex].Name == "colChon")
            {
                if (!isHeaderHovered)
                {
                    isHeaderHovered = true;
                    dgvNhaCungCap.InvalidateCell(e.ColumnIndex, -1);
                }
            }
            else if (isHeaderHovered)
            {
                isHeaderHovered = false;
                if (dgvNhaCungCap.Columns.Contains("colChon"))
                    dgvNhaCungCap.InvalidateCell(dgvNhaCungCap.Columns["colChon"].Index, -1);
            }
        }

        private void dgvNhaCungCap_CellMouseLeave(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex == -1 && isHeaderHovered)
            {
                isHeaderHovered = false;
                if (dgvNhaCungCap.Columns.Contains("colChon"))
                    dgvNhaCungCap.InvalidateCell(dgvNhaCungCap.Columns["colChon"].Index, -1);
            }
        }

        // Click tiêu đề cột để Chọn tất cả / Bỏ chọn tất cả
        private void dgvNhaCungCap_ColumnHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left && e.ColumnIndex >= 0 && dgvNhaCungCap.Columns[e.ColumnIndex].Name == "colChon")
            {
                // Kết thúc chỉnh sửa và bỏ focus khỏi ô hiện tại để giải phóng hoàn toàn edit buffer
                dgvNhaCungCap.EndEdit();
                dgvNhaCungCap.CurrentCell = null;

                // Nếu đang chọn tất cả -> bỏ chọn tất cả. Ngược lại -> chọn tất cả.
                bool targetState = !isHeaderChecked;

                isUpdatingCheckAll = true;
                try
                {
                    isHeaderChecked = targetState;

                    // 1. Cập nhật trong DataTable nguồn
                    if (dgvNhaCungCap.DataSource is DataTable dt)
                    {
                        foreach (DataRow row in dt.Rows)
                        {
                            row["Chon"] = targetState;
                        }
                    }

                    // 2. Cập nhật trực tiếp trên DataGridViewRow để giao diện phản hồi tức thì
                    foreach (DataGridViewRow row in dgvNhaCungCap.Rows)
                    {
                        row.Cells["colChon"].Value = targetState;
                    }
                }
                finally
                {
                    isUpdatingCheckAll = false;
                }

                dgvNhaCungCap.Refresh();
            }
        }

        private void CapNhatTrangThaiHeaderCheck()
        {
            if (isUpdatingCheckAll) return;

            if (dgvNhaCungCap.Rows.Count == 0)
            {
                isHeaderChecked = false;
            }
            else
            {
                bool allChecked = true;
                foreach (DataGridViewRow row in dgvNhaCungCap.Rows)
                {
                    object val = row.Cells["colChon"].Value;
                    bool isChecked = val != null && val != DBNull.Value && Convert.ToBoolean(val);
                    if (!isChecked)
                    {
                        allChecked = false;
                        break;
                    }
                }
                isHeaderChecked = allChecked;
            }

            if (dgvNhaCungCap.Columns.Contains("colChon"))
            {
                dgvNhaCungCap.InvalidateCell(dgvNhaCungCap.Columns["colChon"].Index, -1);
            }
        }

        #endregion

        #region Hành động & Nút chức năng

        // Click đúp vào dòng để mở form xem chi tiết
        private void dgvNhaCungCap_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;
            if (dgvNhaCungCap.Columns[e.ColumnIndex].Name == "colChon") return;

            var cellMaDonVi = dgvNhaCungCap.Rows[e.RowIndex].Cells["colMaDonViCapHang"].Value;
            if (cellMaDonVi != null && int.TryParse(cellMaDonVi.ToString(), out int maDonVi))
            {
                MoFormChiTietNhaCungCap(maDonVi);
            }
        }

        // Bấm nút + Nhà cung cấp mở form thêm mới
        private void btnThemNhaCungCap_Click(object sender, EventArgs e)
        {
            MoFormThemNhaCungCap();
        }

        // Bấm nút + tạo mới/quản lý nhóm nhà cung cấp ngay cạnh ComboBox
        private void btnThemNhomNCC_Click(object sender, EventArgs e)
        {
            using (frmNhomNhaCungCap frmNhom = new frmNhomNhaCungCap())
            {
                frmNhom.ShowDialog(this);
                int maNhomMoi = frmNhom.MaNhomVuaTao;
                NapDanhSachNhomNCC(maNhomMoi);
            }
        }

        private void MoFormThemNhaCungCap()
        {
            using (frmDonViCapHang frm = new frmDonViCapHang())
            {
                frm.StartPosition = FormStartPosition.CenterParent;
                if (frm.ShowDialog(this) == DialogResult.OK)
                {
                    HienThiDanhSach();
                }
            }
        }

        private void MoFormChiTietNhaCungCap(int maDonViCapHang)
        {
            using (frmChiTietNhaCungCap frm = new frmChiTietNhaCungCap(maDonViCapHang))
            {
                frm.StartPosition = FormStartPosition.CenterParent;
                if (frm.ShowDialog(this) == DialogResult.OK)
                {
                    HienThiDanhSach();
                }
            }
        }

        private void MoFormDonViCapHang(int maDonViCapHang)
        {
            if (maDonViCapHang > 0)
            {
                MoFormChiTietNhaCungCap(maDonViCapHang);
            }
            else
            {
                MoFormThemNhaCungCap();
            }
        }

        private void btnXuatFile_Click(object sender, EventArgs e)
        {
            if (dgvNhaCungCap.Rows.Count == 0)
            {
                MessageBox.Show("Không có dữ liệu để xuất file!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            using (SaveFileDialog sfd = new SaveFileDialog())
            {
                sfd.Title = "Xuất danh sách nhà cung cấp";
                sfd.Filter = "File Excel (*.xls)|*.xls";
                sfd.FileName = $"DanhSachNhaCungCap_{DateTime.Now:yyyyMMdd_HHmm}.xls";

                if (sfd.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
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

                        // Tiêu đề cột
                        sb.AppendLine("<tr>");
                        foreach (DataGridViewColumn col in dgvNhaCungCap.Columns)
                        {
                            if (col.Visible && col.Name != "colChon" && col.Name != "colMaDonViCapHang")
                            {
                                sb.AppendLine($"<th>{System.Net.WebUtility.HtmlEncode(col.HeaderText)}</th>");
                            }
                        }
                        sb.AppendLine("</tr>");

                        // Dữ liệu từng dòng
                        foreach (DataGridViewRow row in dgvNhaCungCap.Rows)
                        {
                            sb.AppendLine("<tr>");
                            foreach (DataGridViewColumn col in dgvNhaCungCap.Columns)
                            {
                                if (col.Visible && col.Name != "colChon" && col.Name != "colMaDonViCapHang")
                                {
                                    string cellVal = row.Cells[col.Index].FormattedValue?.ToString() ?? "";
                                    string cssClass = "text";
                                    if (col.DefaultCellStyle.Alignment == DataGridViewContentAlignment.MiddleRight)
                                        cssClass = "number";
                                    else if (col.DefaultCellStyle.Alignment == DataGridViewContentAlignment.MiddleCenter)
                                        cssClass = "center";

                                    sb.AppendLine($"<td class=\"{cssClass}\">{System.Net.WebUtility.HtmlEncode(cellVal)}</td>");
                                }
                            }
                            sb.AppendLine("</tr>");
                        }

                        sb.AppendLine("</table></body></html>");
                        File.WriteAllText(sfd.FileName, sb.ToString(), new UTF8Encoding(true));
                        MessageBox.Show("Xuất danh sách nhà cung cấp thành công tại:\n" + sfd.FileName, "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Lỗi khi xuất file: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        // Bấm nút Nhập file
        private void btnNhapFile_Click(object sender, EventArgs e)
        {
            using (frmNhapNhaCungCapFile frm = new frmNhapNhaCungCapFile())
            {
                if (frm.ShowDialog(this) == DialogResult.OK)
                {
                    trangHienTai = 1;
                    HienThiDanhSach();
                }
            }
        }




        // Bấm nút Hiện cột mở form tùy chọn cột xuất hiện ngay dưới nút btnHienCot
        private void btnHienCot_Click(object sender, EventArgs e)
        {
            foreach (Form openForm in Application.OpenForms)
            {
                if (openForm is frmHienCotNcc)
                {
                    openForm.Activate();
                    openForm.BringToFront();
                    return;
                }
            }

            frmHienCotNcc frm = new frmHienCotNcc(this.dgvNhaCungCap);
            frm.StartPosition = FormStartPosition.Manual;

            // Tính toạ độ hiển thị ngay phía dưới nút btnHienCot
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
            string huongDan = "HƯỚNG DẪN QUẢN LÝ NHÀ CUNG CẤP:\n\n" +
                              "1. Tìm kiếm: Nhập mã, tên hoặc số điện thoại vào ô Tìm kiếm trên đầu.\n" +
                              "2. Bộ lọc: Chọn Nhóm nhà cung cấp, Mức tổng mua, Nợ hiện tại, Trạng thái hoặc Khoảng thời gian tạo ở bảng bên trái.\n" +
                              "3. Thêm mới NCC: Nhấp vào nút '+ Nhà cung cấp'.\n" +
                              "4. Xem / Chỉnh sửa: Nhấp đúp chuột (Double click) vào bất kỳ dòng nhà cung cấp nào trên bảng.\n" +
                              "5. Phân trang: Nhấp nút 'Trước' / 'Sau' hoặc chọn lại số dòng mỗi trang ở góc dưới bảng.\n" +
                              "6. Xuất file: Nhấp 'Xuất file' để lưu danh sách ra file CSV mở bằng Excel.\n" +
                              "7. Hiện/Ẩn cột: Nhấp nút 'Hiện cột' để bật/tắt các cột hiển thị trên bảng.";

            MessageBox.Show(huongDan, "Hướng dẫn sử dụng", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        #endregion
    }
}
