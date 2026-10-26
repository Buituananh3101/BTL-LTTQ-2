using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using BTL_LTTQ_2.Classes;

namespace BTL_LTTQ_2.GUI.Forms.NhaCungCap
{

    public partial class frmNhomNhaCungCap : Form
    {
        public int MaNhomVuaTao { get; private set; } = 0;
        public string TenNhomVuaTao { get; private set; } = string.Empty;
        private DataTable dtNhomHang;

        private int maNhomDangChon = 0;

        public frmNhomNhaCungCap()
        {
            InitializeComponent();
            this.Load += frmNhomNhaCungCap_Load;
        }

        private void frmNhomNhaCungCap_Load(object sender, EventArgs e)
        {
            DinhDangGiaoDien();
            NapDuLieu();
            DatLaiTrangThaiBanDau();
        }

        private void DinhDangGiaoDien()
        {
            // Bật đệm đôi chống giật lag
            typeof(DataGridView).InvokeMember("DoubleBuffered",
                System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.SetProperty,
                null, dgvNhomHang, new object[] { true });

            // Đưa DataGridView lên trên để tự động Fill chính xác phần diện tích còn lại giữa các panel Top và Left
            dgvNhomHang.BringToFront();

            // Cấu hình font chữ và kiểu dáng tiêu đề cột giống hệt dgvHangHoa trên frmDanhSachHangHoa
            dgvNhomHang.Font = new Font("Segoe UI", 9.5F, FontStyle.Regular);
            dgvNhomHang.EnableHeadersVisualStyles = true;
            dgvNhomHang.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 9.5F, FontStyle.Regular);
            dgvNhomHang.ColumnHeadersDefaultCellStyle.WrapMode = DataGridViewTriState.False;
            dgvNhomHang.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            dgvNhomHang.ColumnHeadersHeight = 32;
            dgvNhomHang.AutoGenerateColumns = false;
            dgvNhomHang.Columns.Clear();
            dgvNhomHang.ReadOnly = true;
            dgvNhomHang.AllowUserToAddRows = false;
            dgvNhomHang.AllowUserToDeleteRows = false;
            dgvNhomHang.AllowUserToResizeRows = false;
            dgvNhomHang.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvNhomHang.MultiSelect = false;
            dgvNhomHang.RowHeadersVisible = false;
            dgvNhomHang.RowTemplate.Height = 36;
            dgvNhomHang.BackgroundColor = Color.White;
            dgvNhomHang.BorderStyle = BorderStyle.None;

            // Màu dòng xen kẽ và lựa chọn hiện đại
            dgvNhomHang.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(248, 250, 252);
            dgvNhomHang.DefaultCellStyle.SelectionBackColor = Color.FromArgb(224, 231, 255);
            dgvNhomHang.DefaultCellStyle.SelectionForeColor = Color.FromArgb(30, 41, 59);

            // Cột 1: Mã nhóm (Ẩn ngầm phục vụ sửa/xóa)
            dgvNhomHang.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "colMaNhom",
                DataPropertyName = "MaNhomNcc",
                HeaderText = "Mã nhóm",
                Visible = false
            });

            // Cột 2: Tên nhóm nhà cung cấp
            dgvNhomHang.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "colTenNhom",
                DataPropertyName = "TenNhom",
                HeaderText = "Tên nhóm nhà cung cấp",
                AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill,
                DefaultCellStyle = new DataGridViewCellStyle { Alignment = DataGridViewContentAlignment.MiddleLeft }
            });

            // Cột 3: Số nhà cung cấp đang dùng nhóm này
            dgvNhomHang.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "colSoLuongNCC",
                DataPropertyName = "SoLuongNCC",
                HeaderText = "Số nhà cung cấp",
                Width = 130,
                DefaultCellStyle = new DataGridViewCellStyle { Alignment = DataGridViewContentAlignment.MiddleRight, Format = "N0" }
            });

            // Đăng ký sự kiện
            dgvNhomHang.CellClick += dgvNhomHang_CellClick;
            dgvNhomHang.CellDoubleClick += dgvNhomHang_CellDoubleClick;
            txtTimKiem.TextChanged += txtTimKiem_TextChanged;
            btnThem.Click += btnThem_Click;
            btnSua.Click += btnSua_Click;
            if (btnHuy != null) btnHuy.Click += btnHuy_Click;
            if (btnXuatFile != null) btnXuatFile.Click += btnXuatFile_Click;
            if (btnNhapFile != null) btnNhapFile.Click += btnNhapFile_Click;

            // Hỗ trợ phím Escape để hủy chọn hoặc đóng form
            this.KeyPreview = true;
            this.KeyDown += (s, ev) =>
            {
                if (ev.KeyCode == Keys.Escape)
                {
                    if (maNhomDangChon > 0)
                    {
                        DatLaiTrangThaiBanDau();
                    }
                    else
                    {
                        this.Close();
                    }
                }
            };
        }

        private void NapDuLieu()
        {
            try
            {
                string sqlCheckTable = @"
                    IF NOT EXISTS (SELECT 1 FROM sys.tables WHERE name = N'NhomNhaCungCap')
                    BEGIN
                        CREATE TABLE NhomNhaCungCap (
                            MaNhomNcc INT IDENTITY(1,1) PRIMARY KEY,
                            TenNhom NVARCHAR(150) NOT NULL,
                            GhiChu NVARCHAR(255) NULL
                        );
                    END";
                DataProcessing.ChangeData(sqlCheckTable);

                string sql = @"
                    SELECT n.MaNhomNcc, n.TenNhom, COUNT(ncc.MaDonViCapHang) AS SoLuongNCC
                    FROM NhomNhaCungCap n
                    LEFT JOIN DonViCapHang ncc ON n.MaNhomNcc = ncc.MaNhomNcc
                    GROUP BY n.MaNhomNcc, n.TenNhom
                    ORDER BY n.TenNhom;";
                dtNhomHang = DataProcessing.ReadData(sql);
                dgvNhomHang.DataSource = dtNhomHang;
                CapNhatTongSo();

                if (!string.IsNullOrWhiteSpace(txtTimKiem.Text))
                {
                    txtTimKiem_TextChanged(this, EventArgs.Empty);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi tải danh sách nhóm nhà cung cấp: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void DatLaiTrangThaiBanDau()
        {
            maNhomDangChon = 0;
            txtTenNhom.Clear();
            btnThem.Visible = true;
            btnThem.Enabled = true;
            btnSua.Visible = false;
            if (btnHuy != null) btnHuy.Visible = false;
            if (dgvNhomHang.CurrentRow != null)
            {
                dgvNhomHang.ClearSelection();
            }
            txtTenNhom.Focus();
        }

        // Click vào dòng: hiển thị tên nhóm ra textbox, giữ nút Thêm nhưng disable, hiện nút Sửa và nút Hủy
        private void dgvNhomHang_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0 || e.RowIndex >= dgvNhomHang.Rows.Count) return;
            var cellMaNhom = dgvNhomHang.Rows[e.RowIndex].Cells["colMaNhom"].Value;
            var cellTenNhom = dgvNhomHang.Rows[e.RowIndex].Cells["colTenNhom"].Value;
            if (cellMaNhom == null || !int.TryParse(cellMaNhom.ToString(), out int maNhom)) return;
            maNhomDangChon = maNhom;
            txtTenNhom.Text = cellTenNhom?.ToString() ?? "";

            // Nút Thêm không ẩn mà chỉ enable = false để không ấn được
            btnThem.Visible = true;
            btnThem.Enabled = false;
            btnSua.Visible = true;
            btnSua.Enabled = true;
            if (btnHuy != null)
            {
                btnHuy.Visible = true;
                btnHuy.Enabled = true;
            }
        }

        // Bấm nút Thêm: thêm nhóm nhà cung cấp mới
        private void btnThem_Click(object sender, EventArgs e)
        {
            string tenNhom = txtTenNhom.Text.Trim();
            if (string.IsNullOrWhiteSpace(tenNhom))
            {
                MessageBox.Show("Vui lòng nhập tên nhóm nhà cung cấp!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtTenNhom.Focus();
                return;
            }
            if (tenNhom.Length > 100)
            {
                MessageBox.Show("Tên nhóm nhà cung cấp không được vượt quá 100 ký tự!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtTenNhom.Focus();
                return;
            }

            try
            {
                string sqlCheck = "SELECT COUNT(1) FROM NhomNhaCungCap WHERE LOWER(TenNhom) = LOWER(@TenNhom)";
                object res = DataProcessing.ReadValue(sqlCheck, new SqlParameter[] { new SqlParameter("@TenNhom", tenNhom) });
                if (res != null && int.TryParse(res.ToString(), out int count) && count > 0)
                {
                    MessageBox.Show($"Tên nhóm nhà cung cấp '{tenNhom}' đã tồn tại trong hệ thống!", "Cảnh báo trùng", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtTenNhom.SelectAll();
                    txtTenNhom.Focus();
                    return;
                }

                string sqlInsert = @"
                    INSERT INTO NhomNhaCungCap (TenNhom) VALUES (@TenNhom);
                    SELECT SCOPE_IDENTITY();";
                object newId = DataProcessing.ReadValue(sqlInsert, new SqlParameter[] { new SqlParameter("@TenNhom", tenNhom) });
                if (newId != null && int.TryParse(newId.ToString(), out int id))
                {
                    MaNhomVuaTao = id;
                    TenNhomVuaTao = tenNhom;
                    this.DialogResult = DialogResult.OK;
                }

                NapDuLieu();
                DatLaiTrangThaiBanDau();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi thêm nhóm nhà cung cấp: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Bấm nút Sửa: cập nhật tên nhóm nhà cung cấp đang chọn
        private void btnSua_Click(object sender, EventArgs e)
        {
            if (maNhomDangChon <= 0)
            {
                MessageBox.Show("Vui lòng chọn nhóm nhà cung cấp cần sửa từ danh sách!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            string tenMoi = txtTenNhom.Text.Trim();
            if (string.IsNullOrWhiteSpace(tenMoi))
            {
                MessageBox.Show("Vui lòng nhập tên nhóm nhà cung cấp!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtTenNhom.Focus();
                return;
            }
            if (tenMoi.Length > 100)
            {
                MessageBox.Show("Tên nhóm nhà cung cấp không được vượt quá 100 ký tự!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtTenNhom.Focus();
                return;
            }

            try
            {
                string sqlCheck = "SELECT COUNT(1) FROM NhomNhaCungCap WHERE LOWER(TenNhom) = LOWER(@TenNhom) AND MaNhomNcc <> @MaNhomNcc";
                object res = DataProcessing.ReadValue(sqlCheck, new SqlParameter[] {
                    new SqlParameter("@TenNhom", tenMoi),
                    new SqlParameter("@MaNhomNcc", maNhomDangChon)
                });
                if (res != null && int.TryParse(res.ToString(), out int count) && count > 0)
                {
                    MessageBox.Show($"Tên nhóm '{tenMoi}' đã trùng với một nhóm nhà cung cấp khác!", "Cảnh báo trùng", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtTenNhom.SelectAll();
                    txtTenNhom.Focus();
                    return;
                }

                string sqlUpdate = "UPDATE NhomNhaCungCap SET TenNhom = @TenNhom WHERE MaNhomNcc = @MaNhomNcc";
                DataProcessing.ChangeData(sqlUpdate, new SqlParameter[] {
                    new SqlParameter("@TenNhom", tenMoi),
                    new SqlParameter("@MaNhomNcc", maNhomDangChon)
                });
                MessageBox.Show("Cập nhật tên nhóm nhà cung cấp thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                NapDuLieu();
                DatLaiTrangThaiBanDau();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi sửa nhóm nhà cung cấp: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Double click vào dòng để xóa và hỏi xác nhận
        private void dgvNhomHang_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0 || e.RowIndex >= dgvNhomHang.Rows.Count) return;
            var cellMaNhom = dgvNhomHang.Rows[e.RowIndex].Cells["colMaNhom"].Value;
            var cellTenNhom = dgvNhomHang.Rows[e.RowIndex].Cells["colTenNhom"].Value;
            if (cellMaNhom == null || !int.TryParse(cellMaNhom.ToString(), out int maNhom)) return;
            string tenNhom = cellTenNhom?.ToString() ?? "";

            try
            {
                string sqlCheck = "SELECT COUNT(1) FROM DonViCapHang WHERE MaNhomNcc = @MaNhomNcc";
                object res = DataProcessing.ReadValue(sqlCheck, new SqlParameter[] { new SqlParameter("@MaNhomNcc", maNhom) });
                int soNcc = res != null && int.TryParse(res.ToString(), out int c) ? c : 0;
                if (soNcc > 0)
                {
                    MessageBox.Show($"Không thể xóa nhóm nhà cung cấp '{tenNhom}' vì đang có {soNcc} nhà cung cấp thuộc nhóm này!",
                        "Không thể xóa", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                DialogResult xacNhan = MessageBox.Show(
                    $"Bạn có chắc chắn muốn xóa nhóm nhà cung cấp '{tenNhom}' không?",
                    "Xác nhận xóa",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question);
                if (xacNhan == DialogResult.Yes)
                {
                    string sqlDelete = "DELETE FROM NhomNhaCungCap WHERE MaNhomNcc = @MaNhomNcc";
                    DataProcessing.ChangeData(sqlDelete, new SqlParameter[] { new SqlParameter("@MaNhomNcc", maNhom) });

                    NapDuLieu();
                    DatLaiTrangThaiBanDau();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi xóa nhóm nhà cung cấp: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Tìm kiếm realtime theo tên nhóm
        private void CapNhatTongSo()
        {
            if (lblTongSoNhom == null) return;
            int count = dgvNhomHang.Rows.Count;
            lblTongSoNhom.Text = $"Tổng số: {count:N0} nhóm nhà cung cấp";
        }

        private void txtTimKiem_TextChanged(object sender, EventArgs e)
        {
            if (dtNhomHang == null) return;
            string kw = ComboboxHelper.ThoatKyTuLikeRowFilter(txtTimKiem.Text.Trim());
            if (string.IsNullOrEmpty(kw))
            {
                dtNhomHang.DefaultView.RowFilter = "";
            }
            else
            {
                dtNhomHang.DefaultView.RowFilter = $"TenNhom LIKE '%{kw}%'";
            }
            CapNhatTongSo();
        }

        // Bấm nút Hủy để trở về chế độ thêm mới
        private void btnHuy_Click(object sender, EventArgs e)
        {
            DatLaiTrangThaiBanDau();
        }

        // Bấm nút Xuất file: Xuất danh sách nhóm nhà cung cấp đang hiển thị ra file Excel (.xls) hoặc CSV (.csv)
        private void btnXuatFile_Click(object sender, EventArgs e)
        {
            if (dgvNhomHang.Rows.Count == 0)
            {
                MessageBox.Show("Không có dữ liệu nhóm nhà cung cấp để xuất file!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            using (SaveFileDialog sfd = new SaveFileDialog())
            {
                sfd.Title = "Xuất danh sách nhóm nhà cung cấp";
                sfd.Filter = "File Excel (*.xls)|*.xls|File CSV (*.csv)|*.csv";
                sfd.FileName = $"DanhSachNhomNhaCungCap_{DateTime.Now:yyyyMMdd_HHmm}.xls";
                if (sfd.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        string ext = Path.GetExtension(sfd.FileName).ToLower();
                        if (ext == ".xls")
                        {
                            StringBuilder sb = new StringBuilder();
                            sb.AppendLine("<!DOCTYPE html>");
                            sb.AppendLine("<html><head><meta http-equiv=\"content-type\" content=\"application/vnd.ms-excel; charset=UTF-8\" />");
                            sb.AppendLine("<style>");
                            sb.AppendLine("table { border-collapse: collapse; font-family: Segoe UI, Tahoma, sans-serif; font-size: 11pt; }");
                            sb.AppendLine("th { background-color: #E2E8F0; color: #0F172A; font-weight: bold; border: 1px solid #94A3B8; padding: 8px 14px; }");
                            sb.AppendLine("td { border: 1px solid #CBD5E1; padding: 6px 12px; }");
                            sb.AppendLine(".text { mso-number-format: '\\@'; text-align: left; }");
                            sb.AppendLine(".number { mso-number-format: '#,##0'; text-align: right; }");
                            sb.AppendLine("</style></head><body>");
                            sb.AppendLine("<table>");

                            // Dòng tiêu đề
                            sb.AppendLine("<tr>");
                            sb.AppendLine("<th>Tên nhóm nhà cung cấp</th>");
                            sb.AppendLine("<th>Số nhà cung cấp</th>");
                            sb.AppendLine("</tr>");

                            // Dữ liệu từng dòng
                            foreach (DataGridViewRow row in dgvNhomHang.Rows)
                            {
                                string tenNhom = row.Cells["colTenNhom"].Value?.ToString() ?? "";
                                string soLuong = row.Cells["colSoLuongNCC"].FormattedValue?.ToString() ?? "0";
                                sb.AppendLine("<tr>");
                                sb.AppendLine($"<td class=\"text\">{System.Net.WebUtility.HtmlEncode(tenNhom)}</td>");
                                sb.AppendLine($"<td class=\"number\">{System.Net.WebUtility.HtmlEncode(soLuong)}</td>");
                                sb.AppendLine("</tr>");
                            }
                            sb.AppendLine("</table></body></html>");
                            File.WriteAllText(sfd.FileName, sb.ToString(), new UTF8Encoding(true));
                        }
                        else
                        {
                            StringBuilder sb = new StringBuilder();
                            sb.AppendLine("sep=,");
                            sb.AppendLine("\"Tên nhóm nhà cung cấp\",\"Số nhà cung cấp\"");

                            foreach (DataGridViewRow row in dgvNhomHang.Rows)
                            {
                                string tenNhom = row.Cells["colTenNhom"].Value?.ToString() ?? "";
                                string soLuong = row.Cells["colSoLuongNCC"].FormattedValue?.ToString() ?? "0";
                                sb.AppendLine($"\"{tenNhom.Replace("\"", "\"\"")}\",\"{soLuong.Replace("\"", "\"\"")}\"");
                            }
                            File.WriteAllText(sfd.FileName, sb.ToString(), new UTF8Encoding(true));
                        }
                        DialogResult dr = MessageBox.Show(
                            $"Xuất file thành công {dgvNhomHang.Rows.Count:N0} nhóm nhà cung cấp!\n\nĐường dẫn:\n{sfd.FileName}\n\nBạn có muốn mở file vừa xuất không?",
                            "Xuất file thành công",
                            MessageBoxButtons.YesNo,
                            MessageBoxIcon.Question);
                        if (dr == DialogResult.Yes)
                        {
                            Process.Start(sfd.FileName);
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Lỗi khi xuất file: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        // Bấm nút Nhập file: Đọc dữ liệu từ file Excel/CSV/TXT và nhập vào CSDL
        private void btnNhapFile_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog ofd = new OpenFileDialog())
            {
                ofd.Title = "Chọn file dữ liệu nhóm nhà cung cấp để nhập";
                ofd.Filter = "Tất cả định dạng hỗ trợ (*.csv;*.xls;*.xlsx;*.txt)|*.csv;*.xls;*.xlsx;*.txt|File CSV (*.csv)|*.csv|File Excel (*.xls;*.xlsx)|*.xls;*.xlsx|File văn bản (*.txt)|*.txt";
                ofd.Multiselect = false;
                if (ofd.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        List<string> rawNames = DocDanhSachTenNhomTuFile(ofd.FileName);
                        if (rawNames == null || rawNames.Count == 0)
                        {
                            MessageBox.Show("Không tìm thấy dữ liệu tên nhóm nhà cung cấp hợp lệ trong file đã chọn!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            return;
                        }

                        // Lấy danh sách nhóm nhà cung cấp hiện có trong CSDL để lọc trùng
                        DataTable dtHienCo = DataProcessing.ReadData("SELECT TenNhom FROM NhomNhaCungCap");
                        HashSet<string> tapTenHienCo = new HashSet<string>(StringComparer.OrdinalIgnoreCase);
                        if (dtHienCo != null)
                        {
                            foreach (DataRow r in dtHienCo.Rows)
                            {
                                string tn = r["TenNhom"]?.ToString()?.Trim();
                                if (!string.IsNullOrEmpty(tn))
                                {
                                    tapTenHienCo.Add(tn);
                                }
                            }
                        }
                        List<string> dsThemMoi = new List<string>();
                        HashSet<string> daXuLyTrongFile = new HashSet<string>(StringComparer.OrdinalIgnoreCase);
                        int soLuongTrung = 0;

                        foreach (string item in rawNames)
                        {
                            string ten = item.Trim();
                            if (ten.Length > 100) ten = ten.Substring(0, 100);
                            if (daXuLyTrongFile.Contains(ten) || tapTenHienCo.Contains(ten))
                            {
                                soLuongTrung++;
                            }
                            else
                            {
                                dsThemMoi.Add(ten);
                                daXuLyTrongFile.Add(ten);
                                tapTenHienCo.Add(ten);
                            }
                        }
                        if (dsThemMoi.Count == 0)
                        {
                            MessageBox.Show($"Tất cả {rawNames.Count} nhóm nhà cung cấp trong file đều đã tồn tại trong hệ thống (bỏ qua {soLuongTrung} dòng trùng).",
                                "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            return;
                        }

                        // Thực hiện thêm mới vào CSDL bọc trong Transaction
                        DataProcessing.ChangeDataTransaction((conn, trans) =>
                        {
                            using (SqlCommand cmd = new SqlCommand("INSERT INTO NhomNhaCungCap (TenNhom) VALUES (@TenNhom)", conn, trans))
                            {
                                cmd.Parameters.Add("@TenNhom", SqlDbType.NVarChar, 100);

                                foreach (string tenMoi in dsThemMoi)
                                {
                                    cmd.Parameters["@TenNhom"].Value = tenMoi;
                                    cmd.ExecuteNonQuery();
                                }
                            }
                        });
                        NapDuLieu();
                        DatLaiTrangThaiBanDau();
                        string msg = $"Nhập dữ liệu thành công!\n"
                            + $"- Tổng số nhóm nhà cung cấp trong file: {rawNames.Count}\n"
                            + $"- Thêm mới thành công: {dsThemMoi.Count} nhóm nhà cung cấp\n"
                            + $"- Bỏ qua: {soLuongTrung} nhóm nhà cung cấp (đã tồn tại hoặc trùng lặp)";
                        MessageBox.Show(msg, "Kết quả nhập file", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Lỗi khi nhập file: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        private List<string> DocDanhSachTenNhomTuFile(string filePath)
        {
            List<string> dsTen = new List<string>();
            string ext = Path.GetExtension(filePath).ToLower();
            if (ext == ".xls" || ext == ".xlsx")
            {
                // Kiểm tra xem file .xls có phải là HTML Table (như file do hệ thống xuất ra)
                bool laFileHtml = false;

                try
                {
                    string preview = File.ReadAllText(filePath, Encoding.UTF8);
                    if (preview.IndexOf("<table", StringComparison.OrdinalIgnoreCase) >= 0)
                    {
                        laFileHtml = true;
                        var trMatches = Regex.Matches(preview, @"<tr[^>]*>(.*?)</tr>", RegexOptions.Singleline | RegexOptions.IgnoreCase);

                        foreach (Match tr in trMatches)
                        {
                            var tdMatches = Regex.Matches(tr.Groups[1].Value, @"<td[^>]*>(.*?)</td>", RegexOptions.Singleline | RegexOptions.IgnoreCase);
                            if (tdMatches.Count > 0)
                            {
                                string cellText = tdMatches[0].Groups[1].Value;
                                cellText = Regex.Replace(cellText, "<.*?>", "");
                                cellText = System.Net.WebUtility.HtmlDecode(cellText).Trim();
                                if (!string.IsNullOrEmpty(cellText) && !IsHeaderTitle(cellText))
                                {
                                    dsTen.Add(cellText);
                                }
                            }
                        }
                    }
                }
                catch
                {
                    // Tiếp tục thử đọc nhị phân nếu không phải file HTML
                }
                if (laFileHtml) return dsTen;

                // Nếu là file Excel chuẩn nhị phân (Binary .xls hoặc .xlsx)
                try
                {
                    string connStr = ext == ".xls"
                        ? $"Provider=Microsoft.Jet.OLEDB.4.0;Data Source={filePath};Extended Properties=\"Excel 8.0;HDR=Yes;IMEX=1;\""
                        : $"Provider=Microsoft.ACE.OLEDB.12.0;Data Source={filePath};Extended Properties=\"Excel 12.0 Xml;HDR=Yes;IMEX=1;\"";

                    using (OleDbConnection conn = new OleDbConnection(connStr))
                    {
                        conn.Open();
                        DataTable dtSheets = conn.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null);
                        if (dtSheets != null && dtSheets.Rows.Count > 0)
                        {
                            string firstSheet = dtSheets.Rows[0]["TABLE_NAME"].ToString();

                            using (OleDbCommand cmd = new OleDbCommand($"SELECT * FROM [{firstSheet}]", conn))
                            using (OleDbDataReader reader = cmd.ExecuteReader())
                            {
                                int colIdx = 0;
                                for (int i = 0; i < reader.FieldCount; i++)
                                {
                                    string colName = reader.GetName(i).ToLower();
                                    if (colName.Contains("ten") || colName.Contains("tên") || colName.Contains("nhom") || colName.Contains("nhóm"))
                                    {
                                        colIdx = i;
                                        break;
                                    }
                                }
                                while (reader.Read())
                                {
                                    if (!reader.IsDBNull(colIdx))
                                    {
                                        string val = reader.GetValue(colIdx)?.ToString()?.Trim();
                                        if (!string.IsNullOrEmpty(val) && !IsHeaderTitle(val))
                                        {
                                            dsTen.Add(val);
                                        }
                                    }
                                }
                            }
                        }
                    }
                    return dsTen;
                }
                catch (Exception exOleDb)
                {
                    // Fallback đọc text nếu là plain text nhưng đổi đuôi .xls
                    try
                    {
                        // Kiểm tra chữ ký nhị phân theo byte: file Excel thật (.xls OLE: D0 CF 11 E0, .xlsx ZIP: 50 4B)
                        // không được đọc dạng text, tránh nhập chuỗi rác thành tên nhóm
                        byte[] dauFile = new byte[4];
                        using (FileStream fs = new FileStream(filePath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
                        {
                            fs.Read(dauFile, 0, dauFile.Length);
                        }
                        bool laFileNhiPhan = (dauFile[0] == 0xD0 && dauFile[1] == 0xCF && dauFile[2] == 0x11 && dauFile[3] == 0xE0)
                            || (dauFile[0] == 0x50 && dauFile[1] == 0x4B);
                        if (!laFileNhiPhan)
                        {
                            string[] lines = File.ReadAllLines(filePath, Encoding.UTF8);
                            return DocTuDongTextLines(lines);
                        }
                    }
                    catch { }
                    throw new Exception("Không thể đọc định dạng file Excel này. Vui lòng lưu file sang định dạng CSV (UTF-8) hoặc Excel 97-2003 (.xls) để nhập dữ liệu! Chi tiết: " + exOleDb.Message);
                }
            }
            else
            {
                // Đọc file .CSV hoặc .TXT
                string[] lines = File.ReadAllLines(filePath, Encoding.UTF8);
                return DocTuDongTextLines(lines);
            }
        }

        private List<string> DocTuDongTextLines(string[] lines)
        {
            List<string> ds = new List<string>();
            if (lines == null || lines.Length == 0) return ds;
            int colIndex = 0;
            bool daXacDinhCot = false;

            foreach (string rawLine in lines)
            {
                string line = rawLine?.Trim();
                if (string.IsNullOrEmpty(line)) continue;
                if (line.StartsWith("sep=", StringComparison.OrdinalIgnoreCase)) continue;

                // Tự động nhận diện phân cách: tab, chấm phẩy hoặc dấu phẩy
                char separator = ',';
                if (line.Contains("\t")) separator = '\t';
                else if (line.Contains(";")) separator = ';';
                // Tách có hỗ trợ ô trong dấu nháy kép (vd "Bao bì, màng co" do chính form này xuất ra)
                List<string> parts = DanhSachHangHoa.frmNhapHangHoaFile.TachDongCsv(line, separator);
                if (parts.Count == 0) continue;
                if (!daXacDinhCot)
                {
                    for (int i = 0; i < parts.Count; i++)
                    {
                        string p = BoDauTiengViet(parts[i].Trim('\"', ' ', '\t'));
                        if (p.Contains("ten") || p.Contains("tên") || p.Contains("nhom") || p.Contains("nhóm"))
                        {
                            colIndex = i;
                            break;
                        }
                    }
                    daXacDinhCot = true;

                    // Nếu dòng đầu là tiêu đề thì bỏ qua
                    if (IsHeaderTitle(parts[colIndex].Trim('\"', ' ', '\t')))
                    {
                        continue;
                    }
                }
                if (colIndex < parts.Count)
                {
                    string val = parts[colIndex].Trim('\"', ' ', '\t');
                    if (!string.IsNullOrEmpty(val) && !IsHeaderTitle(val))
                    {
                        ds.Add(val);
                    }
                }
            }
            return ds;
        }

        private static string BoDauTiengViet(string str)
        {
            if (string.IsNullOrEmpty(str)) return "";
            string normalized = str.Normalize(System.Text.NormalizationForm.FormD);
            System.Text.StringBuilder sb = new System.Text.StringBuilder();

            foreach (char c in normalized)
            {
                if (System.Globalization.CharUnicodeInfo.GetUnicodeCategory(c) != System.Globalization.UnicodeCategory.NonSpacingMark)
                {
                    sb.Append(c);
                }
            }
            return sb.ToString().Normalize(System.Text.NormalizationForm.FormC).Replace('đ', 'd').Replace('Đ', 'D').ToLower().Trim();
        }

        private bool IsHeaderTitle(string text)
        {
            if (string.IsNullOrWhiteSpace(text)) return true;
            string t = BoDauTiengViet(text);
            return t == "ten nhom"
                || t == "ten nhom nha cung cap"
                || t == "ten nhom ncc"
                || t == "nhom nha cung cap"
                || t == "ten nhom hang"
                || t == "nhom hang"
                || t == "ten"
                || t == "ma nhom"
                || t == "stt"
                || t == "so mat hang"
                || t == "so nha cung cap"
                || t == "so ncc"
                || t == "so luong";
        }
    }
}
