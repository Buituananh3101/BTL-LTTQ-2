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
using BTL_LTTQ_2.DAL;

namespace BTL_LTTQ_2.GUI.Forms.DanhSachHangHoa
{
    public partial class frmViTri : Form
    {
        private DataTable dtViTri;
        private int maViTriDangChon = 0;
        private string tenViTriDangChon = "";

        public frmViTri()
        {
            InitializeComponent();
            this.Load += frmViTri_Load;
        }

        private void frmViTri_Load(object sender, EventArgs e)
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
                null, dataGridView1, new object[] { true });

            // Đưa DataGridView lên trên để tự động Fill phần diện tích còn lại
            dataGridView1.BringToFront();

            // Cấu hình font chữ và kiểu dáng tiêu đề cột
            dataGridView1.Font = new Font("Segoe UI", 9.5F, FontStyle.Regular);
            dataGridView1.EnableHeadersVisualStyles = true;
            dataGridView1.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 9.5F, FontStyle.Regular);
            dataGridView1.ColumnHeadersDefaultCellStyle.WrapMode = DataGridViewTriState.False;
            dataGridView1.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            dataGridView1.ColumnHeadersHeight = 32;
            dataGridView1.AutoGenerateColumns = false;
            dataGridView1.Columns.Clear();
            dataGridView1.ReadOnly = true;
            dataGridView1.AllowUserToAddRows = false;
            dataGridView1.AllowUserToDeleteRows = false;
            dataGridView1.AllowUserToResizeRows = false;
            dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dataGridView1.MultiSelect = false;
            dataGridView1.RowHeadersVisible = false;
            dataGridView1.RowTemplate.Height = 36;
            dataGridView1.BackgroundColor = Color.White;
            dataGridView1.BorderStyle = BorderStyle.None;

            // Màu dòng xen kẽ và lựa chọn
            dataGridView1.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(248, 250, 252);
            dataGridView1.DefaultCellStyle.SelectionBackColor = Color.FromArgb(224, 231, 255);
            dataGridView1.DefaultCellStyle.SelectionForeColor = Color.FromArgb(30, 41, 59);

            // Cột 1: Mã vị trí (Ẩn ngầm)
            dataGridView1.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "colMaViTri",
                DataPropertyName = "MaViTri",
                HeaderText = "Mã vị trí",
                Visible = false
            });

            // Cột 2: Tên vị trí
            dataGridView1.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "colTenViTri",
                DataPropertyName = "TenViTri",
                HeaderText = "Tên vị trí",
                AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill,
                DefaultCellStyle = new DataGridViewCellStyle { Alignment = DataGridViewContentAlignment.MiddleLeft }
            });

            // Cột 3: Số mặt hàng đang đặt tại vị trí này
            dataGridView1.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "colSoLuongHang",
                DataPropertyName = "SoLuongHang",
                HeaderText = "Số mặt hàng",
                Width = 130,
                DefaultCellStyle = new DataGridViewCellStyle { Alignment = DataGridViewContentAlignment.MiddleRight, Format = "N0" }
            });

            // Đăng ký sự kiện
            dataGridView1.CellClick += dgvViTri_CellClick;
            dataGridView1.CellDoubleClick += dgvViTri_CellDoubleClick;
            textBox2.TextChanged += txtTimKiem_TextChanged;
            button1.Click += btnThem_Click;
            button2.Click += btnSua_Click;
            if (btnHuy != null) btnHuy.Click += btnHuy_Click;
            if (btnXuatFile != null) btnXuatFile.Click += btnXuatFile_Click;
            if (btnNhapFile != null) btnNhapFile.Click += btnNhapFile_Click;

            // Hỗ trợ phím Escape để hủy chọn hoặc đóng form
            this.KeyPreview = true;
            this.KeyDown += (s, ev) =>
            {
                if (ev.KeyCode == Keys.Escape)
                {
                    if (maViTriDangChon > 0)
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
                string sql = @"
                    SELECT vt.MaViTri, vt.TenViTri, COUNT(h.MaHang) AS SoLuongHang
                    FROM ViTri vt
                    LEFT JOIN HangHoa h ON vt.TenViTri = h.ViTri
                    WHERE vt.TrangThai = 'HOAT_DONG'
                    GROUP BY vt.MaViTri, vt.TenViTri
                    ORDER BY vt.TenViTri;";

                dtViTri = DataProcessing.ReadData(sql);
                dataGridView1.DataSource = dtViTri;
                CapNhatTongSo();

                // Cập nhật lại bộ lọc tìm kiếm nếu đang gõ
                if (!string.IsNullOrWhiteSpace(textBox2.Text))
                {
                    txtTimKiem_TextChanged(this, EventArgs.Empty);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi tải danh sách vị trí: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void CapNhatTongSo()
        {
            if (lblTongSoViTri == null) return;
            int count = dtViTri != null ? dtViTri.DefaultView.Count : dataGridView1.Rows.Count;
            lblTongSoViTri.Text = $"Tổng số: {count:N0} vị trí";
        }

        private void DatLaiTrangThaiBanDau()
        {
            maViTriDangChon = 0;
            tenViTriDangChon = "";
            textBox1.Clear();
            button1.Visible = true;
            button1.Enabled = true;
            button2.Visible = false;
            if (btnHuy != null) btnHuy.Visible = false;

            if (dataGridView1.CurrentRow != null)
            {
                dataGridView1.ClearSelection();
            }
            CapNhatTongSo();
            textBox1.Focus();
        }

        // Click vào dòng: hiển thị tên vị trí ra textbox, giữ nút Thêm nhưng disable, hiện nút Sửa và nút Hủy
        private void dgvViTri_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0 || e.RowIndex >= dataGridView1.Rows.Count) return;

            var cellMaViTri = dataGridView1.Rows[e.RowIndex].Cells["colMaViTri"].Value;
            var cellTenViTri = dataGridView1.Rows[e.RowIndex].Cells["colTenViTri"].Value;
            if (cellMaViTri == null || !int.TryParse(cellMaViTri.ToString(), out int maViTri)) return;

            maViTriDangChon = maViTri;
            tenViTriDangChon = cellTenViTri?.ToString() ?? "";
            textBox1.Text = tenViTriDangChon;

            button1.Visible = true;
            button1.Enabled = false;
            button2.Visible = true;
            button2.Enabled = true;
            if (btnHuy != null)
            {
                btnHuy.Visible = true;
                btnHuy.Enabled = true;
            }
        }

        // Bấm nút Thêm: thêm vị trí mới
        private void btnThem_Click(object sender, EventArgs e)
        {
            string tenViTri = textBox1.Text.Trim();
            if (string.IsNullOrWhiteSpace(tenViTri))
            {
                MessageBox.Show("Vui lòng nhập tên vị trí!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                textBox1.Focus();
                return;
            }

            if (tenViTri.Length > 100)
            {
                MessageBox.Show("Tên vị trí không được vượt quá 100 ký tự!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                textBox1.Focus();
                return;
            }

            try
            {
                // Kiểm tra trùng tên vị trí
                string sqlCheck = "SELECT COUNT(1) FROM ViTri WHERE TenViTri = @TenViTri AND TrangThai = 'HOAT_DONG'";
                object res = DataProcessing.ReadValue(sqlCheck, new SqlParameter[] { new SqlParameter("@TenViTri", tenViTri) });
                if (res != null && int.TryParse(res.ToString(), out int count) && count > 0)
                {
                    MessageBox.Show($"Tên vị trí '{tenViTri}' đã tồn tại trong hệ thống!", "Cảnh báo trùng", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    textBox1.SelectAll();
                    textBox1.Focus();
                    return;
                }

                // Thêm mới
                string sqlInsert = "INSERT INTO ViTri (TenViTri, TrangThai) VALUES (@TenViTri, 'HOAT_DONG')";
                DataProcessing.ChangeData(sqlInsert, new SqlParameter[] { new SqlParameter("@TenViTri", tenViTri) });

                // Thêm thành công: tự động làm mới danh sách
                NapDuLieu();
                DatLaiTrangThaiBanDau();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi thêm vị trí: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Bấm nút Sửa: cập nhật tên vị trí đang chọn
        private void btnSua_Click(object sender, EventArgs e)
        {
            if (maViTriDangChon <= 0)
            {
                MessageBox.Show("Vui lòng chọn vị trí cần sửa từ danh sách!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string tenMoi = textBox1.Text.Trim();
            if (string.IsNullOrWhiteSpace(tenMoi))
            {
                MessageBox.Show("Vui lòng nhập tên vị trí!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                textBox1.Focus();
                return;
            }

            if (tenMoi.Length > 100)
            {
                MessageBox.Show("Tên vị trí không được vượt quá 100 ký tự!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                textBox1.Focus();
                return;
            }

            try
            {
                // Kiểm tra trùng tên với vị trí khác
                string sqlCheck = "SELECT COUNT(1) FROM ViTri WHERE TenViTri = @TenViTri AND MaViTri <> @MaViTri AND TrangThai = 'HOAT_DONG'";
                object res = DataProcessing.ReadValue(sqlCheck, new SqlParameter[] {
                    new SqlParameter("@TenViTri", tenMoi),
                    new SqlParameter("@MaViTri", maViTriDangChon)
                });
                if (res != null && int.TryParse(res.ToString(), out int count) && count > 0)
                {
                    MessageBox.Show($"Tên vị trí '{tenMoi}' đã trùng với một vị trí khác!", "Cảnh báo trùng", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    textBox1.SelectAll();
                    textBox1.Focus();
                    return;
                }

                // Cập nhật CSDL trong Transaction (Cập nhật bảng ViTri và đồng bộ HangHoa)
                DataProcessing.ChangeDataTransaction((conn, trans) =>
                {
                    string sqlUpdateViTri = "UPDATE ViTri SET TenViTri = @TenMoi WHERE MaViTri = @MaViTri";
                    using (SqlCommand cmd = new SqlCommand(sqlUpdateViTri, conn, trans))
                    {
                        cmd.Parameters.AddWithValue("@TenMoi", tenMoi);
                        cmd.Parameters.AddWithValue("@MaViTri", maViTriDangChon);
                        cmd.ExecuteNonQuery();
                    }

                    if (!string.IsNullOrEmpty(tenViTriDangChon))
                    {
                        string sqlUpdateHangHoa = "UPDATE HangHoa SET ViTri = @TenMoi WHERE ViTri = @TenCu";
                        using (SqlCommand cmd = new SqlCommand(sqlUpdateHangHoa, conn, trans))
                        {
                            cmd.Parameters.AddWithValue("@TenMoi", tenMoi);
                            cmd.Parameters.AddWithValue("@TenCu", tenViTriDangChon);
                            cmd.ExecuteNonQuery();
                        }
                    }
                });

                MessageBox.Show("Cập nhật tên vị trí thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                NapDuLieu();
                DatLaiTrangThaiBanDau();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi sửa vị trí: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Double click vào dòng để xóa và hỏi xác nhận
        private void dgvViTri_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0 || e.RowIndex >= dataGridView1.Rows.Count) return;

            var cellMaViTri = dataGridView1.Rows[e.RowIndex].Cells["colMaViTri"].Value;
            var cellTenViTri = dataGridView1.Rows[e.RowIndex].Cells["colTenViTri"].Value;
            if (cellMaViTri == null || !int.TryParse(cellMaViTri.ToString(), out int maViTri)) return;

            string tenViTri = cellTenViTri?.ToString() ?? "";

            try
            {
                // Kiểm tra xem vị trí đã có mặt hàng nào đặt ở đó chưa
                string sqlCheck = "SELECT COUNT(1) FROM HangHoa WHERE ViTri = @TenViTri";
                object res = DataProcessing.ReadValue(sqlCheck, new SqlParameter[] { new SqlParameter("@TenViTri", tenViTri) });
                int soHang = res != null && int.TryParse(res.ToString(), out int c) ? c : 0;

                if (soHang > 0)
                {
                    MessageBox.Show($"Không thể xóa vị trí '{tenViTri}' vì đang có {soHang} mặt hàng thuộc vị trí này!",
                        "Không thể xóa", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                DialogResult xacNhan = MessageBox.Show(
                    $"Bạn có chắc chắn muốn xóa vị trí '{tenViTri}' không?",
                    "Xác nhận xóa vị trí",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question);

                if (xacNhan == DialogResult.Yes)
                {
                    string sqlDelete = "DELETE FROM ViTri WHERE MaViTri = @MaViTri";
                    DataProcessing.ChangeData(sqlDelete, new SqlParameter[] { new SqlParameter("@MaViTri", maViTri) });

                    NapDuLieu();
                    DatLaiTrangThaiBanDau();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi xóa vị trí: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Tìm kiếm realtime theo tên vị trí
        private void txtTimKiem_TextChanged(object sender, EventArgs e)
        {
            if (dtViTri == null) return;
            string kw = textBox2.Text.Trim().Replace("'", "''").Replace("[", "[[]").Replace("%", "[%]");
            if (string.IsNullOrEmpty(kw))
            {
                dtViTri.DefaultView.RowFilter = "";
            }
            else
            {
                dtViTri.DefaultView.RowFilter = $"TenViTri LIKE '%{kw}%'";
            }
            CapNhatTongSo();
        }

        // Bấm nút Hủy để trở về chế độ thêm mới
        private void btnHuy_Click(object sender, EventArgs e)
        {
            DatLaiTrangThaiBanDau();
        }

        // Bấm nút Xuất file: Xuất danh sách vị trí đang hiển thị ra file Excel (.xls) hoặc CSV (.csv)
        private void btnXuatFile_Click(object sender, EventArgs e)
        {
            if (dataGridView1.Rows.Count == 0)
            {
                MessageBox.Show("Không có dữ liệu vị trí để xuất file!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            using (SaveFileDialog sfd = new SaveFileDialog())
            {
                sfd.Title = "Xuất danh sách vị trí hàng hóa";
                sfd.Filter = "File Excel (*.xls)|*.xls|File CSV (*.csv)|*.csv";
                sfd.FileName = $"DanhSachViTri_{DateTime.Now:yyyyMMdd_HHmm}.xls";

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
                            sb.AppendLine("<th>Tên vị trí</th>");
                            sb.AppendLine("<th>Số mặt hàng</th>");
                            sb.AppendLine("</tr>");

                            // Dữ liệu từng dòng
                            foreach (DataGridViewRow row in dataGridView1.Rows)
                            {
                                string tenViTri = row.Cells["colTenViTri"].Value?.ToString() ?? "";
                                string soLuong = row.Cells["colSoLuongHang"].FormattedValue?.ToString() ?? "0";

                                sb.AppendLine("<tr>");
                                sb.AppendLine($"<td class=\"text\">{System.Net.WebUtility.HtmlEncode(tenViTri)}</td>");
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
                            sb.AppendLine("\"Tên vị trí\",\"Số mặt hàng\"");

                            foreach (DataGridViewRow row in dataGridView1.Rows)
                            {
                                string tenViTri = row.Cells["colTenViTri"].Value?.ToString() ?? "";
                                string soLuong = row.Cells["colSoLuongHang"].FormattedValue?.ToString() ?? "0";
                                sb.AppendLine($"\"{tenViTri.Replace("\"", "\"\"")}\",\"{soLuong.Replace("\"", "\"\"")}\"");
                            }

                            File.WriteAllText(sfd.FileName, sb.ToString(), new UTF8Encoding(true));
                        }

                        DialogResult dr = MessageBox.Show(
                            $"Xuất file thành công {dataGridView1.Rows.Count:N0} vị trí!\n\nĐường dẫn:\n{sfd.FileName}\n\nBạn có muốn mở file vừa xuất không?",
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
                ofd.Title = "Chọn file dữ liệu vị trí để nhập";
                ofd.Filter = "Tất cả định dạng hỗ trợ (*.csv;*.xls;*.xlsx;*.txt)|*.csv;*.xls;*.xlsx;*.txt|File CSV (*.csv)|*.csv|File Excel (*.xls;*.xlsx)|*.xls;*.xlsx|File văn bản (*.txt)|*.txt";
                ofd.Multiselect = false;

                if (ofd.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        List<string> rawNames = DocDanhSachTenViTriTuFile(ofd.FileName);
                        if (rawNames == null || rawNames.Count == 0)
                        {
                            MessageBox.Show("Không tìm thấy dữ liệu tên vị trí hợp lệ trong file đã chọn!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            return;
                        }

                        // Lấy danh sách vị trí hiện có trong CSDL để lọc trùng
                        DataTable dtHienCo = DataProcessing.ReadData("SELECT TenViTri FROM ViTri WHERE TrangThai = 'HOAT_DONG'");
                        HashSet<string> tapTenHienCo = new HashSet<string>(StringComparer.OrdinalIgnoreCase);
                        if (dtHienCo != null)
                        {
                            foreach (DataRow r in dtHienCo.Rows)
                            {
                                string tn = r["TenViTri"]?.ToString()?.Trim();
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
                            MessageBox.Show($"Tất cả {rawNames.Count} vị trí trong file đều đã tồn tại trong hệ thống (bỏ qua {soLuongTrung} dòng trùng).",
                                "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            return;
                        }

                        // Thêm mới vào CSDL bọc trong Transaction
                        DataProcessing.ChangeDataTransaction((conn, trans) =>
                        {
                            using (SqlCommand cmd = new SqlCommand("INSERT INTO ViTri (TenViTri, TrangThai) VALUES (@TenViTri, 'HOAT_DONG')", conn, trans))
                            {
                                cmd.Parameters.Add("@TenViTri", SqlDbType.NVarChar, 100);
                                foreach (string tenMoi in dsThemMoi)
                                {
                                    cmd.Parameters["@TenViTri"].Value = tenMoi;
                                    cmd.ExecuteNonQuery();
                                }
                            }
                        });

                        NapDuLieu();
                        DatLaiTrangThaiBanDau();

                        string msg = $"Nhập dữ liệu thành công!\n"
                            + $"- Tổng số vị trí trong file: {rawNames.Count}\n"
                            + $"- Thêm mới thành công: {dsThemMoi.Count} vị trí\n"
                            + $"- Bỏ qua: {soLuongTrung} vị trí (đã tồn tại hoặc trùng lặp)";
                        MessageBox.Show(msg, "Kết quả nhập file", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Lỗi khi nhập file: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        private List<string> DocDanhSachTenViTriTuFile(string filePath)
        {
            List<string> dsTen = new List<string>();
            string ext = Path.GetExtension(filePath).ToLower();

            if (ext == ".xls" || ext == ".xlsx")
            {
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
                catch { }

                if (laFileHtml) return dsTen;

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
                                    if (colName.Contains("vi") || colName.Contains("tri") || colName.Contains("vị") || colName.Contains("trí"))
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
                    try
                    {
                        string[] lines = File.ReadAllLines(filePath, Encoding.UTF8);
                        if (lines.Length > 0 && !lines[0].StartsWith("\xD0\xCF\x11\xE0"))
                        {
                            return DocTuDongTextLines(lines);
                        }
                    }
                    catch { }

                    throw new Exception("Không thể đọc định dạng file Excel này. Vui lòng lưu file sang định dạng CSV (UTF-8) hoặc Excel 97-2003 (.xls) để nhập dữ liệu! Chi tiết: " + exOleDb.Message);
                }
            }
            else
            {
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

                char separator = ',';
                if (line.Contains("\t")) separator = '\t';
                else if (line.Contains(";")) separator = ';';

                string[] parts = line.Split(separator);
                if (parts.Length == 0) continue;

                if (!daXacDinhCot)
                {
                    for (int i = 0; i < parts.Length; i++)
                    {
                        string p = BoDauTiengViet(parts[i].Trim('\"', ' ', '\t'));
                        if (p.Contains("vi tri") || p.Contains("ten") || p.Contains("ke") || p.Contains("kho"))
                        {
                            colIndex = i;
                            break;
                        }
                    }
                    daXacDinhCot = true;

                    if (IsHeaderTitle(parts[colIndex].Trim('\"', ' ', '\t')))
                    {
                        continue;
                    }
                }

                if (colIndex < parts.Length)
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
            return t == "ten vi tri"
                || t == "vi tri"
                || t == "ten vi tri hang hoa"
                || t == "vi tri hang"
                || t == "ten"
                || t == "ma vi tri"
                || t == "stt"
                || t == "so mat hang"
                || t == "so luong";
        }
    }
}
