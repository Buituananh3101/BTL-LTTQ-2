using BTL_LTTQ_2.DAL;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Reflection;
using System.Text;
using System.Windows.Forms;
using System.Windows.Forms.VisualStyles;

namespace BTL_LTTQ_2.GUI.Forms.NhapHang
{
    public partial class frmNhapHang : Form
    {
        private int currentPage = 1;
        private int pageSize = 15;
        private int totalRecords = 0;
        private int totalPages = 1;
        private bool isUpdatingCheckAll = false;
        private bool isHeaderChecked = false;
        private bool isHeaderHovered = false;
        private Timer searchDebounceTimer;

        public frmNhapHang()
        {
            InitializeComponent();
            CauHinhDataGridView();
            KhoiTaoDebounceTimer();
        }

        private void KhoiTaoDebounceTimer()
        {
            searchDebounceTimer = new Timer();
            searchDebounceTimer.Interval = 300;
            searchDebounceTimer.Tick += (s, e) =>
            {
                searchDebounceTimer.Stop();
                currentPage = 1;
                LoadData();
            };
        }

        private void CauHinhDataGridView()
        {
            // Bật DoubleBuffered chống rung giật lưới khi render
            typeof(DataGridView).InvokeMember("DoubleBuffered",
                BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.SetProperty,
                null, dgvNhapHang, new object[] { true });

            dgvNhapHang.AutoGenerateColumns = false;
            dgvNhapHang.ReadOnly = false;
            dgvNhapHang.EditMode = DataGridViewEditMode.EditOnEnter;
            dgvNhapHang.RowTemplate.Height = 32;
            dgvNhapHang.RowHeadersWidth = 51;

            // Màu dòng xen kẽ đồng bộ với các form đã có (frmDanhSachHangHoa, frmNhaCungCap)
            dgvNhapHang.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(248, 250, 252);
            dgvNhapHang.DefaultCellStyle.SelectionBackColor = Color.FromArgb(224, 231, 255);
            dgvNhapHang.DefaultCellStyle.SelectionForeColor = Color.FromArgb(30, 41, 59);

            // Đồng bộ độ rộng cột tiêu đề giống frmNhaCungCap (tránh ngắt dòng làm tăng chiều cao hàng tiêu đề)
            colChon.Width = 45;
            colChon.ReadOnly = false;
            colMaNcc.Width = 140;
            colMaPhieu.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            colThoiGian.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            colMaNcc.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            colTenNcc.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
            colCanTraNcc.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            colTongTien.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            colTrangThai.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            colCanTraNcc.DefaultCellStyle.Format = "N0";
            colTongTien.DefaultCellStyle.Format = "N0";
            colThoiGian.DefaultCellStyle.Format = "dd/MM/yyyy HH:mm";
        }

        private void frmNhapHang_Load(object sender, EventArgs e)
        {
            // Thiết lập giá trị mặc định cho bộ lọc ngày
            dtpTuNgay.Value = new DateTime(DateTime.Today.Year, 1, 1);
            dtpDenNgay.Value = DateTime.Today.AddDays(1);

            NapDanhSachTrangThai();
            NapDanhSachNhaCungCap();
            NapDanhSachNhanVien();

            cboSoDongMoiTrang.SelectedItem = "15";
            LoadData();
        }

        private void NapDanhSachTrangThai()
        {
            cboTrangThai.Items.Clear();
            cboTrangThai.Items.Add(new ComboBoxItem("Tất cả trạng thái", null));
            cboTrangThai.Items.Add(new ComboBoxItem("Phiếu tạm", "NHAP"));
            cboTrangThai.Items.Add(new ComboBoxItem("Đã nhập hàng", "DA_XAC_NHAN"));
            cboTrangThai.Items.Add(new ComboBoxItem("Đã hủy", "HUY"));
            cboTrangThai.SelectedIndex = 0;
        }

        private void NapDanhSachNhaCungCap()
        {
            cboNhaCungCap.Items.Clear();
            cboNhaCungCap.Items.Add(new ComboBoxItem("Tất cả nhà cung cấp", null));

            try
            {
                string sql = "SELECT MaDonViCapHang, TenDonVi FROM DonViCapHang ORDER BY TenDonVi ASC";
                DataTable dt = DataProcessing.ReadData(sql);
                foreach (DataRow row in dt.Rows)
                {
                    int id = Convert.ToInt32(row["MaDonViCapHang"]);
                    string ten = row["TenDonVi"].ToString();
                    cboNhaCungCap.Items.Add(new ComboBoxItem(ten, id));
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi nạp danh sách nhà cung cấp: " + ex.Message, "Thông báo lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            cboNhaCungCap.SelectedIndex = 0;
        }

        private void NapDanhSachNhanVien()
        {
            cboNguoiTao.Items.Clear();
            cboNguoiTao.Items.Add(new ComboBoxItem("Tất cả người tạo", null));

            cboNguoiNhap.Items.Clear();
            cboNguoiNhap.Items.Add(new ComboBoxItem("Tất cả người nhập", null));

            try
            {
                string sql = "SELECT MaNV, HoTen FROM NhanVien ORDER BY HoTen ASC";
                DataTable dt = DataProcessing.ReadData(sql);
                foreach (DataRow row in dt.Rows)
                {
                    int id = Convert.ToInt32(row["MaNV"]);
                    string ten = row["HoTen"].ToString();
                    cboNguoiTao.Items.Add(new ComboBoxItem(ten, id));
                    cboNguoiNhap.Items.Add(new ComboBoxItem(ten, id));
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi nạp danh sách nhân viên: " + ex.Message, "Thông báo lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            cboNguoiTao.SelectedIndex = 0;
            cboNguoiNhap.SelectedIndex = 0;
        }

        public void LoadData()
        {
            try
            {
                // Thu thập tham số lọc
                string trangThai = (cboTrangThai.SelectedItem as ComboBoxItem)?.Value?.ToString();
                int? maNcc = (cboNhaCungCap.SelectedItem as ComboBoxItem)?.Value as int?;
                int? maNvLap = (cboNguoiTao.SelectedItem as ComboBoxItem)?.Value as int?;
                int? maNvNhap = (cboNguoiNhap.SelectedItem as ComboBoxItem)?.Value as int?;
                DateTime tuNgay = dtpTuNgay.Value.Date;
                DateTime denNgay = dtpDenNgay.Value.Date.AddDays(1);
                string timKiem = txtTimKiem.Text.Trim();

                string countSql = @"
                    SELECT COUNT(1)
                    FROM PhieuNhap pn
                    LEFT JOIN DonViCapHang ncc ON pn.MaDonViCapHang = ncc.MaDonViCapHang
                    WHERE (@TrangThai IS NULL OR pn.TrangThai = @TrangThai)
                      AND (@MaNcc IS NULL OR pn.MaDonViCapHang = @MaNcc)
                      AND (@MaNVLap IS NULL OR pn.MaNVLap = @MaNVLap)
                      AND (@MaNVNhap IS NULL OR pn.MaNVXacNhan = @MaNVNhap)
                      AND (pn.NgayLap >= @TuNgay AND pn.NgayLap < @DenNgay)
                      AND (
                          @TimKiem = ''
                          OR pn.MaPhieu LIKE @TimKiemLike
                          OR pn.SoHoaDon LIKE @TimKiemLike
                          OR ncc.TenDonVi LIKE @TimKiemLike
                          OR ncc.MaNcc LIKE @TimKiemLike
                          OR pn.GhiChu LIKE @TimKiemLike
                      )";

                SqlParameter[] countParams = new SqlParameter[]
                {
                    new SqlParameter("@TrangThai", (object)trangThai ?? DBNull.Value),
                    new SqlParameter("@MaNcc", (object)maNcc ?? DBNull.Value),
                    new SqlParameter("@MaNVLap", (object)maNvLap ?? DBNull.Value),
                    new SqlParameter("@MaNVNhap", (object)maNvNhap ?? DBNull.Value),
                    new SqlParameter("@TuNgay", tuNgay),
                    new SqlParameter("@DenNgay", denNgay),
                    new SqlParameter("@TimKiem", timKiem),
                    new SqlParameter("@TimKiemLike", "%" + timKiem + "%")
                };

                object countObj = DataProcessing.ReadValue(countSql, countParams);
                totalRecords = Convert.ToInt32(countObj);
                totalPages = (int)Math.Ceiling((double)totalRecords / pageSize);
                if (totalPages < 1) totalPages = 1;
                if (currentPage > totalPages) currentPage = totalPages;

                int skip = (currentPage - 1) * pageSize;

                string querySql = @"
                    SELECT 
                        CAST(0 AS BIT) AS Chon,
                        pn.MaPhieuNhap,
                        ISNULL(pn.MaPhieu, 'PN' + RIGHT('000000' + CAST(pn.MaPhieuNhap AS VARCHAR(10)), 6)) AS MaPhieu,
                        pn.NgayLap,
                        ISNULL(ncc.MaNcc, 'NCC' + RIGHT('00000' + CAST(ISNULL(ncc.MaDonViCapHang, 0) AS VARCHAR(10)), 5)) AS MaNccHienThi,
                        ISNULL(ncc.TenDonVi, N'Nhà cung cấp lẻ') AS TenDonVi,
                        pn.CanTraNcc,
                        pn.TongTien,
                        pn.TrangThai,
                        CASE pn.TrangThai 
                            WHEN 'NHAP' THEN N'Phiếu tạm'
                            WHEN 'DA_XAC_NHAN' THEN N'Đã nhập hàng'
                            WHEN 'HUY' THEN N'Đã hủy'
                            ELSE pn.TrangThai 
                        END AS TenTrangThai
                    FROM PhieuNhap pn
                    LEFT JOIN DonViCapHang ncc ON pn.MaDonViCapHang = ncc.MaDonViCapHang
                    WHERE (@TrangThai IS NULL OR pn.TrangThai = @TrangThai)
                      AND (@MaNcc IS NULL OR pn.MaDonViCapHang = @MaNcc)
                      AND (@MaNVLap IS NULL OR pn.MaNVLap = @MaNVLap)
                      AND (@MaNVNhap IS NULL OR pn.MaNVXacNhan = @MaNVNhap)
                      AND (pn.NgayLap >= @TuNgay AND pn.NgayLap < @DenNgay)
                      AND (
                          @TimKiem = ''
                          OR pn.MaPhieu LIKE @TimKiemLike
                          OR pn.SoHoaDon LIKE @TimKiemLike
                          OR ncc.TenDonVi LIKE @TimKiemLike
                          OR ncc.MaNcc LIKE @TimKiemLike
                          OR pn.GhiChu LIKE @TimKiemLike
                      )
                    ORDER BY pn.NgayLap DESC, pn.MaPhieuNhap DESC
                    OFFSET @Skip ROWS FETCH NEXT @Take ROWS ONLY";

                SqlParameter[] queryParams = new SqlParameter[]
                {
                    new SqlParameter("@TrangThai", (object)trangThai ?? DBNull.Value),
                    new SqlParameter("@MaNcc", (object)maNcc ?? DBNull.Value),
                    new SqlParameter("@MaNVLap", (object)maNvLap ?? DBNull.Value),
                    new SqlParameter("@MaNVNhap", (object)maNvNhap ?? DBNull.Value),
                    new SqlParameter("@TuNgay", tuNgay),
                    new SqlParameter("@DenNgay", denNgay),
                    new SqlParameter("@TimKiem", timKiem),
                    new SqlParameter("@TimKiemLike", "%" + timKiem + "%"),
                    new SqlParameter("@Skip", skip),
                    new SqlParameter("@Take", pageSize)
                };

                DataTable dt = DataProcessing.ReadData(querySql, queryParams);

                if (!dt.Columns.Contains("Chon"))
                {
                    DataColumn col = new DataColumn("Chon", typeof(bool));
                    col.DefaultValue = false;
                    dt.Columns.Add(col);
                }

                dgvNhapHang.DataSource = dt;

                CapNhatThanhPhanTrang();
                isHeaderChecked = false;
                dgvNhapHang.Invalidate();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi nạp dữ liệu phiếu nhập hàng: " + ex.Message, "Thông báo lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void CapNhatThanhPhanTrang()
        {
            lblTongSoPhieu.Text = $"Tổng số: {totalRecords:N0} phiếu nhập";
            lblTrangHienTai.Text = $"Trang {currentPage} / {totalPages}";
            btnTrangTruoc.Enabled = currentPage > 1;
            btnTrangSau.Enabled = currentPage < totalPages;
        }

        private void BoLoc_SelectedIndexChanged(object sender, EventArgs e)
        {
            currentPage = 1;
            LoadData();
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
                pageSize = val;
                currentPage = 1;
                LoadData();
            }
        }

        private void btnTrangTruoc_Click(object sender, EventArgs e)
        {
            if (currentPage > 1)
            {
                currentPage--;
                LoadData();
            }
        }

        private void btnTrangSau_Click(object sender, EventArgs e)
        {
            if (currentPage < totalPages)
            {
                currentPage++;
                LoadData();
            }
        }

        private void btnThemNhapHang_Click(object sender, EventArgs e)
        {
            using (frmThemPhieuNhap frm = new frmThemPhieuNhap())
            {
                if (frm.ShowDialog() == DialogResult.OK)
                {
                    LoadData();
                }
            }
        }

        private void dgvNhapHang_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;
            if (e.ColumnIndex == colChon.Index) return;

            MoChiTietPhieu(e.RowIndex);
        }

        private void MoChiTietPhieu(int rowIndex)
        {
            if (rowIndex < 0 || rowIndex >= dgvNhapHang.Rows.Count) return;

            var cellVal = dgvNhapHang.Rows[rowIndex].Cells["colMaPhieuNhap"].Value;
            if (cellVal != null && int.TryParse(cellVal.ToString(), out int maPhieuNhap))
            {
                using (frmChiTietPhieuNhap frm = new frmChiTietPhieuNhap(maPhieuNhap))
                {
                    frm.ShowDialog();
                    // Load lại dữ liệu vì phiếu có thể bị xóa/hủy hoặc trả hàng
                    LoadData();
                }
            }
        }

        private void btnXuatFile_Click(object sender, EventArgs e)
        {
            if (dgvNhapHang.Rows.Count == 0)
            {
                MessageBox.Show("Không có dữ liệu để xuất file!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            using (SaveFileDialog sfd = new SaveFileDialog())
            {
                sfd.Filter = "CSV File (*.csv)|*.csv|All Files (*.*)|*.*";
                sfd.FileName = $"DanhSachNhapHang_{DateTime.Now:yyyyMMdd_HHmmss}.csv";
                if (sfd.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        StringBuilder sb = new StringBuilder();
                        // Dòng tiêu đề
                        sb.AppendLine("Mã nhập hàng,Thời gian,Mã NCC,Nhà cung cấp,Cần trả NCC,Tổng tiền,Trạng thái");

                        DataTable dt = dgvNhapHang.DataSource as DataTable;
                        if (dt != null)
                        {
                            foreach (DataRow row in dt.Rows)
                            {
                                string maPhieu = row["MaPhieu"]?.ToString() ?? "";
                                string thoiGian = Convert.ToDateTime(row["NgayLap"]).ToString("dd/MM/yyyy HH:mm");
                                string maNcc = row["MaNccHienThi"]?.ToString() ?? "";
                                string tenNcc = $"\"{row["TenDonVi"]?.ToString().Replace("\"", "\"\"")}\"";
                                decimal canTra = Convert.ToDecimal(row["CanTraNcc"]);
                                decimal tongTien = Convert.ToDecimal(row["TongTien"]);
                                string trangThai = row["TenTrangThai"]?.ToString() ?? "";

                                sb.AppendLine($"{maPhieu},{thoiGian},{maNcc},{tenNcc},{canTra},{tongTien},{trangThai}");
                            }
                        }

                        // Ghi với UTF-8 with BOM để Excel đọc không lỗi font tiếng Việt
                        File.WriteAllText(sfd.FileName, sb.ToString(), new UTF8Encoding(true));
                        MessageBox.Show("Xuất file dữ liệu thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Lỗi xuất file: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        private void btnHienCot_Click(object sender, EventArgs e)
        {
            frmHienCotNhapHang frm = new frmHienCotNhapHang(dgvNhapHang);
            Point btnPos = btnHienCot.PointToScreen(Point.Empty);
            frm.Location = new Point(btnPos.X + btnHienCot.Width - frm.Width, btnPos.Y + btnHienCot.Height + 2);
            frm.Show();
        }

        private void btnHuongDanSuDung_Click(object sender, EventArgs e)
        {
            MessageBox.Show(
                "HƯỚNG DẪN QUẢN LÝ NHẬP HÀNG:\n\n" +
                "- Bấm '+ Nhập hàng' để lập phiếu nhập hàng mới từ Nhà cung cấp.\n" +
                "- Nhấp đúp chuột vào một dòng đơn nhập để xem chi tiết, hủy/xóa phiếu hoặc trả hàng.\n" +
                "- Ở form chi tiết đơn nhập, chọn 'Trả hàng nhập' để hoàn trả một phần hoặc toàn bộ số lượng hàng.\n" +
                "- Sử dụng các bộ lọc dạng ComboBox bên trái để tra cứu đơn nhập theo trạng thái, nhà cung cấp, người nhập...\n" +
                "- Bấm 'Xuất file' để lưu danh sách ra định dạng CSV/Excel.",
                "Hướng dẫn sử dụng",
                MessageBoxButtons.OK,
                MessageBoxIcon.Information
            );
        }

        private void dgvNhapHang_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.RowIndex < 0) return;

            // Bôi đỏ nếu còn nợ cần trả NCC
            if (dgvNhapHang.Columns[e.ColumnIndex].Name == "colCanTraNcc" && e.Value != null)
            {
                if (decimal.TryParse(e.Value.ToString(), out decimal canTra) && canTra > 0)
                {
                    e.CellStyle.ForeColor = Color.FromArgb(220, 38, 38);
                    e.CellStyle.Font = new Font(dgvNhapHang.Font, FontStyle.Bold);
                }
            }

            // Đổi màu hiển thị trạng thái
            if (dgvNhapHang.Columns[e.ColumnIndex].Name == "colTrangThai" && e.Value != null)
            {
                string tt = e.Value.ToString();
                if (tt == "Đã nhập hàng")
                {
                    e.CellStyle.ForeColor = Color.FromArgb(22, 163, 74);
                }
                else if (tt == "Phiếu tạm")
                {
                    e.CellStyle.ForeColor = Color.FromArgb(37, 99, 235);
                }
                else if (tt == "Đã hủy")
                {
                    e.CellStyle.ForeColor = Color.FromArgb(100, 116, 139);
                }
            }
        }

        private void dgvNhapHang_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        {
            if (e.RowIndex == -1 && e.ColumnIndex >= 0 && dgvNhapHang.Columns[e.ColumnIndex].Name == "colChon")
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

        private void dgvNhapHang_CellMouseMove(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.RowIndex == -1 && e.ColumnIndex >= 0 && dgvNhapHang.Columns[e.ColumnIndex].Name == "colChon")
            {
                if (!isHeaderHovered)
                {
                    isHeaderHovered = true;
                    dgvNhapHang.InvalidateCell(e.ColumnIndex, -1);
                }
            }
        }

        private void dgvNhapHang_CellMouseLeave(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex == -1 && e.ColumnIndex >= 0 && dgvNhapHang.Columns[e.ColumnIndex].Name == "colChon")
            {
                if (isHeaderHovered)
                {
                    isHeaderHovered = false;
                    dgvNhapHang.InvalidateCell(e.ColumnIndex, -1);
                }
            }
        }

        private void dgvNhapHang_ColumnHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.ColumnIndex >= 0 && dgvNhapHang.Columns[e.ColumnIndex].Name == "colChon")
            {
                dgvNhapHang.EndEdit();
                dgvNhapHang.CurrentCell = null;

                isHeaderChecked = !isHeaderChecked;
                isUpdatingCheckAll = true;

                DataTable dt = dgvNhapHang.DataSource as DataTable;
                if (dt != null)
                {
                    foreach (DataRow row in dt.Rows)
                    {
                        row["Chon"] = isHeaderChecked;
                    }
                }

                isUpdatingCheckAll = false;
                dgvNhapHang.Invalidate();
            }
        }

        private void dgvNhapHang_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (isUpdatingCheckAll) return;
            if (e.RowIndex >= 0 && e.ColumnIndex >= 0 && dgvNhapHang.Columns[e.ColumnIndex].Name == "colChon")
            {
                CapNhatTrangThaiHeaderCheck();
            }
        }

        private void dgvNhapHang_CurrentCellDirtyStateChanged(object sender, EventArgs e)
        {
            if (dgvNhapHang.IsCurrentCellDirty && dgvNhapHang.CurrentCell.ColumnIndex == colChon.Index)
            {
                dgvNhapHang.CommitEdit(DataGridViewDataErrorContexts.Commit);
            }
        }

        private void dgvNhapHang_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.ColumnIndex == colChon.Index)
            {
                dgvNhapHang.CommitEdit(DataGridViewDataErrorContexts.Commit);
            }
        }

        private void CapNhatTrangThaiHeaderCheck()
        {
            DataTable dt = dgvNhapHang.DataSource as DataTable;
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
            dgvNhapHang.InvalidateCell(colChon.Index, -1);
        }

        private void dgvNhapHang_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {
            e.Cancel = true;
            e.ThrowException = false;
        }

        private class ComboBoxItem
        {
            public string Text { get; }
            public object Value { get; }

            public ComboBoxItem(string text, object value)
            {
                Text = text;
                Value = value;
            }

            public override string ToString() => Text;
        }
    }
}
