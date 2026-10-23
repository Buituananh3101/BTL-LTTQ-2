using BTL_LTTQ_2.Classes;
using System;
using System.Data;
using System.Drawing;
using System.IO;
using System.IO.Compression;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using System.Xml.Linq;

namespace BTL_LTTQ_2.GUI.Forms.KiemKho
{
    public partial class frmNhapKiemKhoFile : Form
    {
        public DataTable DtKetQuaNhap { get; private set; }
        private DataTable dtXemTruoc;

        public frmNhapKiemKhoFile()
        {
            InitializeComponent();
            KhoiTaoBangDuLieu();
        }

        private void frmNhapKiemKhoFile_Load(object sender, EventArgs e)
        {
            // Bật DoubleBuffered chống rung nhấp nháy
            typeof(DataGridView).InvokeMember("DoubleBuffered",
                System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.SetProperty,
                null, dgvXemTruoc, new object[] { true });

            dgvXemTruoc.AutoGenerateColumns = false;
            dgvXemTruoc.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(248, 250, 252);
            dgvXemTruoc.DefaultCellStyle.SelectionBackColor = Color.FromArgb(224, 231, 255);
            dgvXemTruoc.DefaultCellStyle.SelectionForeColor = Color.FromArgb(15, 23, 42);

            colStt.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            colMaHang.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            colDonViTinh.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            colSoLuongThucTe.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            colSoLuongThucTe.DefaultCellStyle.Format = "N0";
        }

        private void KhoiTaoBangDuLieu()
        {
            dtXemTruoc = new DataTable();
            dtXemTruoc.Columns.Add("STT", typeof(int));
            dtXemTruoc.Columns.Add("MaHang", typeof(int));
            dtXemTruoc.Columns.Add("MaHangNoiBo", typeof(string));
            dtXemTruoc.Columns.Add("TenHang", typeof(string));
            dtXemTruoc.Columns.Add("TenDVT", typeof(string));
            dtXemTruoc.Columns.Add("SoLuongThucTe", typeof(decimal));
            dtXemTruoc.Columns.Add("DonGia", typeof(decimal));
            dtXemTruoc.Columns.Add("GhiChu", typeof(string));
            dtXemTruoc.Columns.Add("TrangThaiDong", typeof(string));
            dtXemTruoc.Columns.Add("HopLe", typeof(bool));

            dgvXemTruoc.DataSource = dtXemTruoc;
        }

        #region Tải File Mẫu Chuẩn

        private void btnTaiFileMauExcel_Click(object sender, EventArgs e)
        {
            using (SaveFileDialog sfd = new SaveFileDialog())
            {
                sfd.Title = "Lưu file mẫu kiểm kho Excel";
                sfd.Filter = "File Excel (*.xls)|*.xls";
                sfd.FileName = "Mau_Kiem_Kho.xls";

                if (sfd.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        StringBuilder html = new StringBuilder();
                        html.AppendLine("<html xmlns:o=\"urn:schemas-microsoft-com:office:office\" xmlns:x=\"urn:schemas-microsoft-com:office:excel\" xmlns=\"http://www.w3.org/TR/REC-html40\">");
                        html.AppendLine("<head>");
                        html.AppendLine("<meta http-equiv=\"Content-Type\" content=\"text/html; charset=utf-8\" />");
                        html.AppendLine("<!--[if gte mso 9]><xml><x:ExcelWorkbook><x:ExcelWorksheets><x:ExcelWorksheet><x:Name>MauKiemKho</x:Name><x:WorksheetOptions><x:DisplayGridlines/></x:WorksheetOptions></x:ExcelWorksheet></x:ExcelWorksheets></x:ExcelWorkbook></xml><![endif]-->");
                        html.AppendLine("<style>");
                        html.AppendLine("th { background-color: #0078D7; color: #FFFFFF; font-family: Segoe UI, Arial; font-size: 10pt; font-weight: bold; border: 0.5pt solid #CCCCCC; padding: 6px; }");
                        html.AppendLine("td { font-family: Segoe UI, Arial; font-size: 9.5pt; border: 0.5pt solid #E0E0E0; padding: 5px; }");
                        html.AppendLine(".text { mso-number-format: \"\\@\"; }");
                        html.AppendLine(".number { mso-number-format: \"#,##0\"; text-align: right; }");
                        html.AppendLine("</style>");
                        html.AppendLine("</head>");
                        html.AppendLine("<body>");
                        html.AppendLine("<table>");
                        html.AppendLine("<tr>");
                        html.AppendLine("<th>Mã hàng (*)</th>");
                        html.AppendLine("<th>Tên hàng</th>");
                        html.AppendLine("<th>Đơn vị tính</th>");
                        html.AppendLine("<th>Số lượng thực tế (*)</th>");
                        html.AppendLine("<th>Ghi chú</th>");
                        html.AppendLine("</tr>");

                        // Lấy 3 mặt hàng mẫu thực tế từ CSDL nếu có
                        DataTable dtMau = LayDuLieuMauHangHoa();
                        if (dtMau != null && dtMau.Rows.Count > 0)
                        {
                            foreach (DataRow r in dtMau.Rows)
                            {
                                html.AppendLine("<tr>");
                                html.AppendLine($"<td class=\"text\">{r["MaHangNoiBo"]}</td>");
                                html.AppendLine($"<td>{r["TenHang"]}</td>");
                                html.AppendLine($"<td>{r["TenDVT"]}</td>");
                                html.AppendLine($"<td class=\"number\">{r["TonGoiY"]}</td>");
                                html.AppendLine("<td>Kiểm kê thực tế</td>");
                                html.AppendLine("</tr>");
                            }
                        }
                        else
                        {
                            html.AppendLine("<tr><td class=\"text\">SP000001</td><td>Bình gas 12kg</td><td>Bình</td><td class=\"number\">100</td><td>Kiểm đếm kho</td></tr>");
                            html.AppendLine("<tr><td class=\"text\">SP000002</td><td>Bình gas 45kg</td><td>Bình</td><td class=\"number\">50</td><td>Kiểm đếm kho</td></tr>");
                        }

                        html.AppendLine("</table>");
                        html.AppendLine("</body>");
                        html.AppendLine("</html>");

                        File.WriteAllText(sfd.FileName, html.ToString(), Encoding.UTF8);
                        MessageBox.Show("Tải file mẫu Excel thành công!\nĐường dẫn: " + sfd.FileName, "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Lỗi khi tải file mẫu: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        private void btnTaiFileMauCsv_Click(object sender, EventArgs e)
        {
            using (SaveFileDialog sfd = new SaveFileDialog())
            {
                sfd.Title = "Lưu file mẫu kiểm kho CSV";
                sfd.Filter = "File CSV (*.csv)|*.csv";
                sfd.FileName = "Mau_Kiem_Kho.csv";

                if (sfd.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        StringBuilder csv = new StringBuilder();
                        csv.AppendLine("Mã hàng (*),Tên hàng,Đơn vị tính,Số lượng thực tế (*),Ghi chú");

                        DataTable dtMau = LayDuLieuMauHangHoa();
                        if (dtMau != null && dtMau.Rows.Count > 0)
                        {
                            foreach (DataRow r in dtMau.Rows)
                            {
                                csv.AppendLine($"\"{r["MaHangNoiBo"]}\",\"{r["TenHang"]}\",\"{r["TenDVT"]}\",{r["TonGoiY"]},\"Kiểm kê thực tế\"");
                            }
                        }
                        else
                        {
                            csv.AppendLine("\"SP000001\",\"Bình gas 12kg\",\"Bình\",100,\"Kiểm đếm kho\"");
                            csv.AppendLine("\"SP000002\",\"Bình gas 45kg\",\"Bình\",50,\"Kiểm đếm kho\"");
                        }

                        File.WriteAllText(sfd.FileName, csv.ToString(), new UTF8Encoding(true));
                        MessageBox.Show("Tải file mẫu CSV thành công!\nĐường dẫn: " + sfd.FileName, "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Lỗi khi tải file mẫu CSV: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        private DataTable LayDuLieuMauHangHoa()
        {
            try
            {
                string sql = @"
                    SELECT TOP 5 
                        h.MaHangNoiBo, 
                        h.TenHang, 
                        ISNULL(dvt.TenDVT, N'Cái') AS TenDVT,
                        ISNULL(tk.SoLuongTon, 10) AS TonGoiY
                    FROM HangHoa h
                    LEFT JOIN DonViTinh dvt ON h.MaDVT = dvt.MaDVT
                    LEFT JOIN vw_TonKhoHienTai tk ON h.MaHang = tk.MaHang AND tk.MaKho = 1
                    WHERE ISNULL(h.TrangThai, '') NOT IN ('NGUNG_BAN', 'DA_XOA')
                    ORDER BY h.MaHang ASC";
                return DataProcessing.ReadData(sql);
            }
            catch
            {
                return null;
            }
        }

        #endregion

        #region Chọn & Đọc File Dữ Liệu

        private void btnChonFile_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog ofd = new OpenFileDialog())
            {
                ofd.Title = "Chọn file dữ liệu kiểm kho để nhập";
                ofd.Filter = "Tất cả file hỗ trợ (*.xlsx;*.xls;*.csv)|*.xlsx;*.xls;*.csv|File Excel (*.xlsx;*.xls)|*.xlsx;*.xls|File CSV (*.csv)|*.csv|Tất cả file (*.*)|*.*";
                ofd.Multiselect = false;

                if (ofd.ShowDialog() == DialogResult.OK)
                {
                    txtDuongDanFile.Text = ofd.FileName;
                    btnDocDuLieu_Click(null, null);
                }
            }
        }

        private void btnDocDuLieu_Click(object sender, EventArgs e)
        {
            string duongDan = txtDuongDanFile.Text.Trim();
            if (string.IsNullOrEmpty(duongDan) || !File.Exists(duongDan))
            {
                MessageBox.Show("Vui lòng chọn một file dữ liệu hợp lệ!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                Cursor.Current = Cursors.WaitCursor;
                dtXemTruoc.Clear();

                DataTable dtRaw = DocFileTho(duongDan);
                if (dtRaw == null || dtRaw.Rows.Count == 0)
                {
                    MessageBox.Show("Không đọc được dữ liệu nào từ file hoặc file rỗng!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                // Nạp toàn bộ danh mục hàng hóa hiện có trong hệ thống để đối chiếu nhanh
                string sqlHang = @"
                    SELECT 
                        h.MaHang, 
                        h.MaHangNoiBo, 
                        h.TenHang, 
                        ISNULL(dvt.TenDVT, N'Cái') AS TenDVT,
                        ISNULL(h.GiaVon, 0) AS GiaVon
                    FROM HangHoa h
                    LEFT JOIN DonViTinh dvt ON h.MaDVT = dvt.MaDVT";
                DataTable dtDbHang = DataProcessing.ReadData(sqlHang);

                int stt = 1;
                int countHopLe = 0;
                int countLoi = 0;

                foreach (DataRow r in dtRaw.Rows)
                {
                    string maHangNoiBo = r["MaHang"] != null ? r["MaHang"].ToString().Trim() : "";
                    if (string.IsNullOrEmpty(maHangNoiBo)) continue;

                    string slStr = r["SoLuong"] != null ? r["SoLuong"].ToString().Trim() : "";
                    string ghiChu = r["GhiChu"] != null ? r["GhiChu"].ToString().Trim() : "";

                    DataRow rowMoi = dtXemTruoc.NewRow();
                    rowMoi["STT"] = stt++;
                    rowMoi["MaHangNoiBo"] = maHangNoiBo;
                    rowMoi["GhiChu"] = ghiChu;

                    // Tìm mặt hàng trong DB
                    DataRow[] timThay = dtDbHang.Select($"MaHangNoiBo = '{maHangNoiBo.Replace("'", "''")}'");
                    if (timThay == null || timThay.Length == 0)
                    {
                        rowMoi["MaHang"] = 0;
                        rowMoi["TenHang"] = r["TenHang"] != null ? r["TenHang"].ToString() : "(Không tìm thấy hàng)";
                        rowMoi["TenDVT"] = "";
                        rowMoi["SoLuongThucTe"] = 0;
                        rowMoi["DonGia"] = 0;
                        rowMoi["TrangThaiDong"] = "Lỗi: Mã hàng không tồn tại";
                        rowMoi["HopLe"] = false;
                        countLoi++;
                    }
                    else
                    {
                        DataRow hDb = timThay[0];
                        rowMoi["MaHang"] = Convert.ToInt32(hDb["MaHang"]);
                        rowMoi["TenHang"] = hDb["TenHang"].ToString();
                        rowMoi["TenDVT"] = hDb["TenDVT"].ToString();
                        rowMoi["DonGia"] = Convert.ToDecimal(hDb["GiaVon"]);

                        // Parse số lượng
                        slStr = slStr.Replace(",", "").Replace(" ", "");
                        if (decimal.TryParse(slStr, out decimal slThucTe) && slThucTe >= 0)
                        {
                            rowMoi["SoLuongThucTe"] = slThucTe;
                            rowMoi["TrangThaiDong"] = "Hợp lệ";
                            rowMoi["HopLe"] = true;
                            countHopLe++;
                        }
                        else
                        {
                            rowMoi["SoLuongThucTe"] = 0;
                            rowMoi["TrangThaiDong"] = "Lỗi: Số lượng không hợp lệ";
                            rowMoi["HopLe"] = false;
                            countLoi++;
                        }
                    }

                    dtXemTruoc.Rows.Add(rowMoi);
                }

                lblThongKe.Text = $"Tổng: {dtXemTruoc.Rows.Count} dòng | Hợp lệ: {countHopLe} | Lỗi: {countLoi}";
                btnDongY.Enabled = (countHopLe > 0);

                if (countLoi > 0)
                {
                    lblThongKe.ForeColor = Color.FromArgb(220, 38, 38);
                }
                else
                {
                    lblThongKe.ForeColor = Color.FromArgb(16, 185, 129);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi đọc file: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                Cursor.Current = Cursors.Default;
            }
        }

        private DataTable DocFileTho(string filePath)
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("MaHang", typeof(string));
            dt.Columns.Add("TenHang", typeof(string));
            dt.Columns.Add("DonViTinh", typeof(string));
            dt.Columns.Add("SoLuong", typeof(string));
            dt.Columns.Add("GhiChu", typeof(string));

            string ext = Path.GetExtension(filePath).ToLowerInvariant();
            if (ext == ".csv")
            {
                DocFileCsv(filePath, dt);
            }
            else if (ext == ".xlsx")
            {
                DocFileXlsx(filePath, dt);
            }
            else if (ext == ".xls")
            {
                DocFileXls(filePath, dt);
            }

            return dt;
        }

        private void DocFileCsv(string filePath, DataTable dt)
        {
            string[] lines = File.ReadAllLines(filePath, Encoding.UTF8);
            if (lines.Length <= 1) return;

            // Bỏ dòng tiêu đề (i=1)
            for (int i = 1; i < lines.Length; i++)
            {
                string line = lines[i].Trim();
                if (string.IsNullOrEmpty(line)) continue;

                string[] parts = TachDongCsv(line);
                if (parts.Length >= 2)
                {
                    DataRow r = dt.NewRow();
                    r["MaHang"] = parts[0].Trim('\"', ' ');
                    r["TenHang"] = parts.Length > 1 ? parts[1].Trim('\"', ' ') : "";
                    r["DonViTinh"] = parts.Length > 2 ? parts[2].Trim('\"', ' ') : "";
                    r["SoLuong"] = parts.Length > 3 ? parts[3].Trim('\"', ' ') : parts[1].Trim('\"', ' ');
                    r["GhiChu"] = parts.Length > 4 ? parts[4].Trim('\"', ' ') : "";
                    dt.Rows.Add(r);
                }
            }
        }

        private string[] TachDongCsv(string line)
        {
            var regex = new Regex("(?:^|,)(\"(?:[^\"]+|\"\")*\"|[^,]*)");
            var matches = regex.Matches(line);
            string[] results = new string[matches.Count];
            for (int i = 0; i < matches.Count; i++)
            {
                results[i] = matches[i].Groups[1].Value;
            }
            return results;
        }

        private void DocFileXlsx(string filePath, DataTable dt)
        {
            try
            {
                using (ZipArchive archive = ZipFile.OpenRead(filePath))
                {
                    ZipArchiveEntry sharedStringsEntry = archive.GetEntry("xl/sharedStrings.xml");
                    var sharedStrings = new System.Collections.Generic.List<string>();
                    if (sharedStringsEntry != null)
                    {
                        using (var stream = sharedStringsEntry.Open())
                        {
                            XDocument doc = XDocument.Load(stream);
                            XNamespace ns = "http://schemas.openxmlformats.org/spreadsheetml/2006/main";
                            foreach (var si in doc.Descendants(ns + "si"))
                            {
                                sharedStrings.Add(si.Value);
                            }
                        }
                    }

                    ZipArchiveEntry sheetEntry = archive.GetEntry("xl/worksheets/sheet1.xml");
                    if (sheetEntry != null)
                    {
                        using (var stream = sheetEntry.Open())
                        {
                            XDocument doc = XDocument.Load(stream);
                            XNamespace ns = "http://schemas.openxmlformats.org/spreadsheetml/2006/main";

                            bool isHeader = true;
                            foreach (var row in doc.Descendants(ns + "row"))
                            {
                                if (isHeader)
                                {
                                    isHeader = false;
                                    continue;
                                }

                                var cells = row.Elements(ns + "c");
                                var values = new System.Collections.Generic.List<string>();
                                foreach (var c in cells)
                                {
                                    string val = c.Element(ns + "v")?.Value ?? "";
                                    string type = c.Attribute("t")?.Value;
                                    if (type == "s" && int.TryParse(val, out int idx) && idx >= 0 && idx < sharedStrings.Count)
                                    {
                                        val = sharedStrings[idx];
                                    }
                                    values.Add(val);
                                }

                                if (values.Count >= 2)
                                {
                                    DataRow r = dt.NewRow();
                                    r["MaHang"] = values[0];
                                    r["TenHang"] = values.Count > 1 ? values[1] : "";
                                    r["DonViTinh"] = values.Count > 2 ? values[2] : "";
                                    r["SoLuong"] = values.Count > 3 ? values[3] : values[1];
                                    r["GhiChu"] = values.Count > 4 ? values[4] : "";
                                    dt.Rows.Add(r);
                                }
                            }
                        }
                    }
                }
            }
            catch
            {
                // Fallback nếu không đọc được OpenXml trực tiếp
                DocFileCsv(filePath, dt);
            }
        }

        private void DocFileXls(string filePath, DataTable dt)
        {
            // Kiểm tra nếu là định dạng HTML giả lập Excel
            string content = File.ReadAllText(filePath, Encoding.UTF8);
            if (content.Contains("<table") || content.Contains("<html"))
            {
                var rowRegex = new Regex("<tr[^>]*>(.*?)</tr>", RegexOptions.Singleline | RegexOptions.IgnoreCase);
                var cellRegex = new Regex("<t[dh][^>]*>(.*?)</t[dh]>", RegexOptions.Singleline | RegexOptions.IgnoreCase);

                var rowMatches = rowRegex.Matches(content);
                bool isHeader = true;
                foreach (Match rm in rowMatches)
                {
                    if (isHeader)
                    {
                        isHeader = false;
                        continue;
                    }

                    var cellMatches = cellRegex.Matches(rm.Groups[1].Value);
                    if (cellMatches.Count >= 2)
                    {
                        DataRow r = dt.NewRow();
                        r["MaHang"] = StripHtml(cellMatches[0].Groups[1].Value);
                        r["TenHang"] = cellMatches.Count > 1 ? StripHtml(cellMatches[1].Groups[1].Value) : "";
                        r["DonViTinh"] = cellMatches.Count > 2 ? StripHtml(cellMatches[2].Groups[1].Value) : "";
                        r["SoLuong"] = cellMatches.Count > 3 ? StripHtml(cellMatches[3].Groups[1].Value) : "";
                        r["GhiChu"] = cellMatches.Count > 4 ? StripHtml(cellMatches[4].Groups[1].Value) : "";
                        dt.Rows.Add(r);
                    }
                }
            }
        }

        private string StripHtml(string input)
        {
            return Regex.Replace(input, "<.*?>", string.Empty).Trim();
        }

        #endregion

        private void btnDongY_Click(object sender, EventArgs e)
        {
            // Lọc ra các dòng hợp lệ
            DataView dv = new DataView(dtXemTruoc);
            dv.RowFilter = "HopLe = true";
            DtKetQuaNhap = dv.ToTable();

            if (DtKetQuaNhap.Rows.Count == 0)
            {
                MessageBox.Show("Không có dòng dữ liệu hợp lệ nào để nhập!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void btnDong_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }
    }
}
