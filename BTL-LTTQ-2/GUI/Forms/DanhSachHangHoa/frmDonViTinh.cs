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

namespace BTL_LTTQ_2.GUI.Forms.DanhSachHangHoa
{
    public partial class frmDonViTinh : Form
    {
        private DataTable dtDonViTinh;
        private int maDvtDangChon = 0;

        public frmDonViTinh()
        {
            InitializeComponent();
            this.Load += frmDonViTinh_Load;
        }

        private void frmDonViTinh_Load(object sender, EventArgs e)
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
                null, dgvDonViTinh, new object[] { true });

            dgvDonViTinh.BringToFront();

            dgvDonViTinh.Font = new Font("Segoe UI", 9.5F, FontStyle.Regular);
            dgvDonViTinh.EnableHeadersVisualStyles = true;
            dgvDonViTinh.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 9.5F, FontStyle.Regular);
            dgvDonViTinh.ColumnHeadersDefaultCellStyle.WrapMode = DataGridViewTriState.False;
            dgvDonViTinh.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            dgvDonViTinh.ColumnHeadersHeight = 32;
            dgvDonViTinh.AutoGenerateColumns = false;
            dgvDonViTinh.Columns.Clear();
            dgvDonViTinh.ReadOnly = true;
            dgvDonViTinh.AllowUserToAddRows = false;
            dgvDonViTinh.AllowUserToDeleteRows = false;
            dgvDonViTinh.AllowUserToResizeRows = false;
            dgvDonViTinh.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvDonViTinh.MultiSelect = false;
            dgvDonViTinh.RowHeadersVisible = false;
            dgvDonViTinh.RowTemplate.Height = 36;
            dgvDonViTinh.BackgroundColor = Color.White;
            dgvDonViTinh.BorderStyle = BorderStyle.None;

            dgvDonViTinh.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(248, 250, 252);
            dgvDonViTinh.DefaultCellStyle.SelectionBackColor = Color.FromArgb(224, 231, 255);
            dgvDonViTinh.DefaultCellStyle.SelectionForeColor = Color.FromArgb(30, 41, 59);

            // Cột 1: Mã ĐVT (ẩn)
            dgvDonViTinh.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "colMaDVT",
                DataPropertyName = "MaDVT",
                HeaderText = "Mã ĐVT",
                Visible = false
            });

            // Cột 2: Tên đơn vị tính
            dgvDonViTinh.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "colTenDVT",
                DataPropertyName = "TenDVT",
                HeaderText = "Tên đơn vị tính",
                AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill,
                DefaultCellStyle = new DataGridViewCellStyle { Alignment = DataGridViewContentAlignment.MiddleLeft }
            });

            // Cột 3: Số mặt hàng đang dùng ĐVT này
            dgvDonViTinh.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "colSoLuongHang",
                DataPropertyName = "SoLuongHang",
                HeaderText = "Số mặt hàng",
                Width = 130,
                DefaultCellStyle = new DataGridViewCellStyle { Alignment = DataGridViewContentAlignment.MiddleRight, Format = "N0" }
            });

            // Đăng ký sự kiện
            dgvDonViTinh.CellClick += dgvDonViTinh_CellClick;
            dgvDonViTinh.CellDoubleClick += dgvDonViTinh_CellDoubleClick;
            txtTimKiem.TextChanged += txtTimKiem_TextChanged;
            btnThem.Click += btnThem_Click;
            btnSua.Click += btnSua_Click;
            if (btnHuy != null) btnHuy.Click += btnHuy_Click;
            if (btnXuatFile != null) btnXuatFile.Click += btnXuatFile_Click;
            if (btnNhapFile != null) btnNhapFile.Click += btnNhapFile_Click;

            this.KeyPreview = true;
            this.KeyDown += (s, ev) =>
            {
                if (ev.KeyCode == Keys.Escape)
                {
                    if (maDvtDangChon > 0)
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
                    SELECT dvt.MaDVT, dvt.TenDVT, COUNT(h.MaHang) AS SoLuongHang
                    FROM DonViTinh dvt
                    LEFT JOIN HangHoa h ON dvt.MaDVT = h.MaDVT
                    GROUP BY dvt.MaDVT, dvt.TenDVT
                    ORDER BY dvt.TenDVT;";

                dtDonViTinh = DataProcessing.ReadData(sql);
                dgvDonViTinh.DataSource = dtDonViTinh;
                CapNhatTongSo();

                if (!string.IsNullOrWhiteSpace(txtTimKiem.Text))
                {
                    txtTimKiem_TextChanged(this, EventArgs.Empty);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi tải danh sách đơn vị tính: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void CapNhatTongSo()
        {
            if (lblTongSoDvt == null) return;
            int count = dgvDonViTinh.Rows.Count;
            lblTongSoDvt.Text = $"Tổng số: {count:N0} đơn vị tính";
        }

        private void DatLaiTrangThaiBanDau()
        {
            maDvtDangChon = 0;
            txtTenDvt.Clear();
            btnThem.Visible = true;
            btnThem.Enabled = true;
            btnSua.Visible = false;
            if (btnHuy != null) btnHuy.Visible = false;
            if (dgvDonViTinh.CurrentRow != null)
            {
                dgvDonViTinh.ClearSelection();
            }
            txtTenDvt.Focus();
        }

        private void dgvDonViTinh_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0 || e.RowIndex >= dgvDonViTinh.Rows.Count) return;
            var cellMaDVT = dgvDonViTinh.Rows[e.RowIndex].Cells["colMaDVT"].Value;
            var cellTenDVT = dgvDonViTinh.Rows[e.RowIndex].Cells["colTenDVT"].Value;
            if (cellMaDVT == null || !int.TryParse(cellMaDVT.ToString(), out int maDvt)) return;

            maDvtDangChon = maDvt;
            txtTenDvt.Text = cellTenDVT?.ToString() ?? "";

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

        private void btnThem_Click(object sender, EventArgs e)
        {
            string tenDvt = txtTenDvt.Text.Trim();
            if (string.IsNullOrWhiteSpace(tenDvt))
            {
                MessageBox.Show("Vui lòng nhập tên đơn vị tính!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtTenDvt.Focus();
                return;
            }
            if (tenDvt.Length > 50)
            {
                MessageBox.Show("Tên đơn vị tính không được vượt quá 50 ký tự!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtTenDvt.Focus();
                return;
            }

            try
            {
                string sqlCheck = "SELECT COUNT(1) FROM DonViTinh WHERE LOWER(TenDVT) = LOWER(@TenDVT)";
                object res = DataProcessing.ReadValue(sqlCheck, new SqlParameter[] { new SqlParameter("@TenDVT", tenDvt) });
                if (res != null && int.TryParse(res.ToString(), out int count) && count > 0)
                {
                    MessageBox.Show($"Tên đơn vị tính '{tenDvt}' đã tồn tại trong hệ thống!", "Cảnh báo trùng", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtTenDvt.SelectAll();
                    txtTenDvt.Focus();
                    return;
                }

                string sqlInsert = "INSERT INTO DonViTinh (TenDVT) VALUES (@TenDVT)";
                DataProcessing.ChangeData(sqlInsert, new SqlParameter[] { new SqlParameter("@TenDVT", tenDvt) });

                NapDuLieu();
                DatLaiTrangThaiBanDau();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi thêm đơn vị tính: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            if (maDvtDangChon <= 0)
            {
                MessageBox.Show("Vui lòng chọn đơn vị tính cần sửa từ danh sách!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string tenMoi = txtTenDvt.Text.Trim();
            if (string.IsNullOrWhiteSpace(tenMoi))
            {
                MessageBox.Show("Vui lòng nhập tên đơn vị tính!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtTenDvt.Focus();
                return;
            }
            if (tenMoi.Length > 50)
            {
                MessageBox.Show("Tên đơn vị tính không được vượt quá 50 ký tự!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtTenDvt.Focus();
                return;
            }

            try
            {
                string sqlCheck = "SELECT COUNT(1) FROM DonViTinh WHERE LOWER(TenDVT) = LOWER(@TenDVT) AND MaDVT <> @MaDVT";
                object res = DataProcessing.ReadValue(sqlCheck, new SqlParameter[] {
                    new SqlParameter("@TenDVT", tenMoi),
                    new SqlParameter("@MaDVT", maDvtDangChon)
                });
                if (res != null && int.TryParse(res.ToString(), out int count) && count > 0)
                {
                    MessageBox.Show($"Tên đơn vị tính '{tenMoi}' đã trùng với một đơn vị tính khác!", "Cảnh báo trùng", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtTenDvt.SelectAll();
                    txtTenDvt.Focus();
                    return;
                }

                string sqlUpdate = "UPDATE DonViTinh SET TenDVT = @TenDVT WHERE MaDVT = @MaDVT";
                DataProcessing.ChangeData(sqlUpdate, new SqlParameter[] {
                    new SqlParameter("@TenDVT", tenMoi),
                    new SqlParameter("@MaDVT", maDvtDangChon)
                });

                MessageBox.Show("Cập nhật tên đơn vị tính thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                NapDuLieu();
                DatLaiTrangThaiBanDau();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi sửa đơn vị tính: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void dgvDonViTinh_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0 || e.RowIndex >= dgvDonViTinh.Rows.Count) return;
            var cellMaDVT = dgvDonViTinh.Rows[e.RowIndex].Cells["colMaDVT"].Value;
            var cellTenDVT = dgvDonViTinh.Rows[e.RowIndex].Cells["colTenDVT"].Value;
            if (cellMaDVT == null || !int.TryParse(cellMaDVT.ToString(), out int maDvt)) return;
            string tenDvt = cellTenDVT?.ToString() ?? "";

            try
            {
                string sqlCheck = "SELECT COUNT(1) FROM HangHoa WHERE MaDVT = @MaDVT";
                object res = DataProcessing.ReadValue(sqlCheck, new SqlParameter[] { new SqlParameter("@MaDVT", maDvt) });
                int soHang = res != null && int.TryParse(res.ToString(), out int c) ? c : 0;
                if (soHang > 0)
                {
                    MessageBox.Show($"Không thể xóa đơn vị tính '{tenDvt}' vì đang có {soHang} mặt hàng sử dụng đơn vị tính này!",
                        "Không thể xóa", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                DialogResult xacNhan = MessageBox.Show(
                    $"Bạn có chắc chắn muốn xóa đơn vị tính '{tenDvt}' không?",
                    "Xác nhận xóa đơn vị tính",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question);

                if (xacNhan == DialogResult.Yes)
                {
                    string sqlDelete = "DELETE FROM DonViTinh WHERE MaDVT = @MaDVT";
                    DataProcessing.ChangeData(sqlDelete, new SqlParameter[] { new SqlParameter("@MaDVT", maDvt) });

                    NapDuLieu();
                    DatLaiTrangThaiBanDau();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi xóa đơn vị tính: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnHuy_Click(object sender, EventArgs e)
        {
            DatLaiTrangThaiBanDau();
        }

        private void txtTimKiem_TextChanged(object sender, EventArgs e)
        {
            if (dtDonViTinh == null) return;
            // Thoát ký tự đặc biệt (' [ ] * %) để RowFilter không lỗi khi gõ ký tự đại diện
            string kw = ComboboxHelper.ThoatKyTuLikeRowFilter(txtTimKiem.Text.Trim());
            if (string.IsNullOrEmpty(kw))
            {
                dtDonViTinh.DefaultView.RowFilter = "";
            }
            else
            {
                dtDonViTinh.DefaultView.RowFilter = $"TenDVT LIKE '%{kw}%'";
            }
            CapNhatTongSo();
        }

        private void btnXuatFile_Click(object sender, EventArgs e)
        {
            if (dgvDonViTinh.Rows.Count == 0)
            {
                MessageBox.Show("Không có dữ liệu đơn vị tính để xuất file!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            using (SaveFileDialog sfd = new SaveFileDialog())
            {
                sfd.Title = "Xuất danh sách đơn vị tính";
                sfd.Filter = "File Excel (*.xls)|*.xls|File CSV (*.csv)|*.csv";
                sfd.FileName = $"DanhSachDonViTinh_{DateTime.Now:yyyyMMdd_HHmm}.xls";

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

                            sb.AppendLine("<tr>");
                            sb.AppendLine("<th>Tên đơn vị tính</th>");
                            sb.AppendLine("<th>Số mặt hàng</th>");
                            sb.AppendLine("</tr>");

                            foreach (DataGridViewRow row in dgvDonViTinh.Rows)
                            {
                                string tenDvt = row.Cells["colTenDVT"].Value?.ToString() ?? "";
                                string soLuong = row.Cells["colSoLuongHang"].FormattedValue?.ToString() ?? "0";
                                sb.AppendLine("<tr>");
                                sb.AppendLine($"<td class=\"text\">{System.Net.WebUtility.HtmlEncode(tenDvt)}</td>");
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
                            sb.AppendLine("\"Tên đơn vị tính\",\"Số mặt hàng\"");

                            foreach (DataGridViewRow row in dgvDonViTinh.Rows)
                            {
                                string tenDvt = row.Cells["colTenDVT"].Value?.ToString() ?? "";
                                string soLuong = row.Cells["colSoLuongHang"].FormattedValue?.ToString() ?? "0";
                                sb.AppendLine($"\"{tenDvt.Replace("\"", "\"\"")}\",\"{soLuong.Replace("\"", "\"\"")}\"");
                            }

                            File.WriteAllText(sfd.FileName, sb.ToString(), new UTF8Encoding(true));
                        }

                        DialogResult dr = MessageBox.Show(
                            $"Xuất file thành công {dgvDonViTinh.Rows.Count:N0} đơn vị tính!\n\nĐường dẫn:\n{sfd.FileName}\n\nBạn có muốn mở file vừa xuất không?",
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

        private void btnNhapFile_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog ofd = new OpenFileDialog())
            {
                ofd.Title = "Chọn file dữ liệu đơn vị tính để nhập";
                ofd.Filter = "Tất cả định dạng hỗ trợ (*.csv;*.xls;*.xlsx;*.txt)|*.csv;*.xls;*.xlsx;*.txt|File CSV (*.csv)|*.csv|File Excel (*.xls;*.xlsx)|*.xls;*.xlsx|File văn bản (*.txt)|*.txt";

                if (ofd.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        List<string> danhSachTen = DocDuLieuTuFile(ofd.FileName);
                        if (danhSachTen == null || danhSachTen.Count == 0)
                        {
                            MessageBox.Show("Không tìm thấy dữ liệu tên đơn vị tính hợp lệ trong file đã chọn!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            return;
                        }

                        DataTable dtHienCo = DataProcessing.ReadData("SELECT TenDVT FROM DonViTinh");
                        HashSet<string> tapHopHienCo = new HashSet<string>(StringComparer.OrdinalIgnoreCase);
                        foreach (DataRow r in dtHienCo.Rows)
                        {
                            string t = r["TenDVT"]?.ToString().Trim();
                            if (!string.IsNullOrEmpty(t)) tapHopHienCo.Add(t);
                        }

                        int themMoi = 0;
                        int boQuaTrung = 0;
                        int boQuaLoi = 0;
                        HashSet<string> tapHopFile = new HashSet<string>(StringComparer.OrdinalIgnoreCase);

                        foreach (string rawName in danhSachTen)
                        {
                            string ten = rawName?.Trim();
                            if (string.IsNullOrWhiteSpace(ten)) continue;

                            if (ten.Length > 50)
                            {
                                boQuaLoi++;
                                continue;
                            }

                            if (tapHopHienCo.Contains(ten) || tapHopFile.Contains(ten))
                            {
                                boQuaTrung++;
                                continue;
                            }

                            tapHopFile.Add(ten);

                            try
                            {
                                string sqlInsert = "INSERT INTO DonViTinh (TenDVT) VALUES (@TenDVT)";
                                DataProcessing.ChangeData(sqlInsert, new SqlParameter[] { new SqlParameter("@TenDVT", ten) });
                                tapHopHienCo.Add(ten);
                                themMoi++;
                            }
                            catch
                            {
                                boQuaLoi++;
                            }
                        }

                        NapDuLieu();
                        DatLaiTrangThaiBanDau();

                        StringBuilder msg = new StringBuilder();
                        msg.AppendLine("Kết quả nhập file đơn vị tính:");
                        msg.AppendLine($"- Tổng số dòng đọc được: {danhSachTen.Count:N0}");
                        msg.AppendLine($"- Thêm mới thành công: {themMoi:N0}");
                        if (boQuaTrung > 0) msg.AppendLine($"- Bỏ qua do trùng lặp: {boQuaTrung:N0}");
                        if (boQuaLoi > 0) msg.AppendLine($"- Bỏ qua do vượt 50 ký tự hoặc lỗi: {boQuaLoi:N0}");

                        MessageBox.Show(msg.ToString(), "Hoàn tất nhập file", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Lỗi khi đọc file: " + ex.Message, "Lỗi nhập file", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        private List<string> DocDuLieuTuFile(string filePath)
        {
            string ext = Path.GetExtension(filePath).ToLower();
            List<string> result = new List<string>();

            if (ext == ".csv" || ext == ".txt")
            {
                string[] lines = File.ReadAllLines(filePath, Encoding.UTF8);
                bool daBoQuaTieuDe = false;

                foreach (string rawLine in lines)
                {
                    string line = rawLine?.Trim();
                    if (string.IsNullOrWhiteSpace(line)) continue;
                    if (line.StartsWith("sep=", StringComparison.OrdinalIgnoreCase)) continue;

                    string[] parts = line.Split(new char[] { ',', '\t', ';' });
                    string ten = parts[0].Trim().Trim('"').Trim();

                    string tenKhongDau = BoDauTiengViet(ten).ToLower();
                    if (!daBoQuaTieuDe && (tenKhongDau.Contains("ten don vi tinh") || tenKhongDau.Contains("don vi tinh") || tenKhongDau == "dvt" || tenKhongDau == "ten"))
                    {
                        daBoQuaTieuDe = true;
                        continue;
                    }

                    if (!string.IsNullOrWhiteSpace(ten))
                    {
                        result.Add(ten);
                    }
                }
            }
            else if (ext == ".xls" || ext == ".xlsx")
            {
                string textDau = "";
                try
                {
                    using (StreamReader sr = new StreamReader(filePath, Encoding.UTF8))
                    {
                        char[] buffer = new char[500];
                        int count = sr.Read(buffer, 0, buffer.Length);
                        textDau = new string(buffer, 0, count);
                    }
                }
                catch { }

                if (textDau.IndexOf("<table", StringComparison.OrdinalIgnoreCase) >= 0 ||
                    textDau.IndexOf("<html", StringComparison.OrdinalIgnoreCase) >= 0 ||
                    textDau.IndexOf("<?xml", StringComparison.OrdinalIgnoreCase) >= 0)
                {
                    result = DocFileBangHTML(filePath);
                }
                else
                {
                    result = DocFileBangOleDb(filePath);
                }
            }

            return result;
        }

        private List<string> DocFileBangHTML(string filePath)
        {
            List<string> list = new List<string>();
            string content = File.ReadAllText(filePath, Encoding.UTF8);
            var trMatches = Regex.Matches(content, @"<tr[^>]*>(.*?)</tr>", RegexOptions.IgnoreCase | RegexOptions.Singleline);
            bool daBoQuaHeader = false;

            foreach (Match tr in trMatches)
            {
                var tdMatches = Regex.Matches(tr.Groups[1].Value, @"<(?:td|th)[^>]*>(.*?)</(?:td|th)>", RegexOptions.IgnoreCase | RegexOptions.Singleline);
                if (tdMatches.Count > 0)
                {
                    string val = Regex.Replace(tdMatches[0].Groups[1].Value, @"<[^>]+>", "").Trim();
                    val = System.Net.WebUtility.HtmlDecode(val);

                    string khongDau = BoDauTiengViet(val).ToLower();
                    if (!daBoQuaHeader && (khongDau.Contains("ten don vi tinh") || khongDau.Contains("don vi tinh") || khongDau == "dvt" || khongDau == "ten"))
                    {
                        daBoQuaHeader = true;
                        continue;
                    }

                    if (!string.IsNullOrWhiteSpace(val))
                    {
                        list.Add(val);
                    }
                }
            }
            return list;
        }

        private List<string> DocFileBangOleDb(string filePath)
        {
            List<string> list = new List<string>();
            string ext = Path.GetExtension(filePath).ToLower();
            string connStr = "";

            if (ext == ".xlsx")
            {
                connStr = $"Provider=Microsoft.ACE.OLEDB.12.0;Data Source={filePath};Extended Properties=\"Excel 12.0 Xml;HDR=YES;IMEX=1;\"";
            }
            else
            {
                connStr = $"Provider=Microsoft.Jet.OLEDB.4.0;Data Source={filePath};Extended Properties=\"Excel 8.0;HDR=YES;IMEX=1;\"";
            }

            try
            {
                using (OleDbConnection conn = new OleDbConnection(connStr))
                {
                    conn.Open();
                    DataTable dtSchema = conn.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null);
                    if (dtSchema != null && dtSchema.Rows.Count > 0)
                    {
                        string sheetName = dtSchema.Rows[0]["TABLE_NAME"].ToString();
                        using (OleDbCommand cmd = new OleDbCommand($"SELECT * FROM [{sheetName}]", conn))
                        using (OleDbDataAdapter da = new OleDbDataAdapter(cmd))
                        {
                            DataTable dtData = new DataTable();
                            da.Fill(dtData);
                            if (dtData.Columns.Count > 0)
                            {
                                foreach (DataRow r in dtData.Rows)
                                {
                                    string val = r[0]?.ToString().Trim();
                                    if (!string.IsNullOrWhiteSpace(val))
                                    {
                                        list.Add(val);
                                    }
                                }
                            }
                        }
                    }
                }
            }
            catch
            {
                string[] lines = File.ReadAllLines(filePath, Encoding.Default);
                bool header = false;
                foreach (string l in lines)
                {
                    string[] parts = l.Split(new char[] { '\t', ',', ';' });
                    if (parts.Length > 0)
                    {
                        string v = parts[0].Trim().Trim('"').Trim();
                        string kd = BoDauTiengViet(v).ToLower();
                        if (!header && (kd.Contains("ten don vi tinh") || kd.Contains("don vi tinh") || kd == "dvt"))
                        {
                            header = true;
                            continue;
                        }
                        if (!string.IsNullOrWhiteSpace(v)) list.Add(v);
                    }
                }
            }
            return list;
        }

        private static string BoDauTiengViet(string text)
        {
            if (string.IsNullOrWhiteSpace(text)) return "";
            string[] pattern = new string[] {
                "a|á|à|ả|ã|ạ|ă|ắ|ằ|ẳ|ẵ|ặ|â|ấ|ầ|ẩ|ẫ|ậ",
                "d|đ",
                "e|é|è|ẻ|ẽ|ẹ|ê|ế|ề|ể|ễ|ệ",
                "i|í|ì|ỉ|ĩ|ị",
                "o|ó|ò|ỏ|õ|ọ|ô|ố|ồ|ổ|ỗ|ộ|ơ|ớ|ờ|ở|ỡ|ợ",
                "u|ú|ù|ủ|ũ|ụ|ư|ứ|ừ|ử|ữ|ự",
                "y|ý|ỳ|ỷ|ỹ|ỵ"
            };
            char[] replaceChar = new char[] { 'a', 'd', 'e', 'i', 'o', 'u', 'y' };
            for (int i = 0; i < pattern.Length; i++)
            {
                string[] chars = pattern[i].Split('|');
                foreach (string c in chars)
                {
                    text = text.Replace(c, replaceChar[i].ToString());
                    text = text.Replace(c.ToUpper(), replaceChar[i].ToString().ToUpper());
                }
            }
            return text;
        }
    }
}
