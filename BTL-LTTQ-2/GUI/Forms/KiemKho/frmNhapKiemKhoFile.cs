using BTL_LTTQ_2.Classes;
using System;
using System.Collections.Generic;
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
        public int SoLuongThemMoi { get; private set; }
        public int SoLuongCapNhat { get; private set; }

        public frmNhapKiemKhoFile()
        {
            InitializeComponent();
            KhoiTaoBangKetQua();
        }

        private void KhoiTaoBangKetQua()
        {
            DtKetQuaNhap = new DataTable();
            DtKetQuaNhap.Columns.Add("STT", typeof(int));
            DtKetQuaNhap.Columns.Add("MaHang", typeof(int));
            DtKetQuaNhap.Columns.Add("MaHangNoiBo", typeof(string));
            DtKetQuaNhap.Columns.Add("TenHang", typeof(string));
            DtKetQuaNhap.Columns.Add("TenDVT", typeof(string));
            DtKetQuaNhap.Columns.Add("SoLuongThucTe", typeof(decimal));
            DtKetQuaNhap.Columns.Add("DonGia", typeof(decimal));
            DtKetQuaNhap.Columns.Add("GhiChu", typeof(string));
        }

        private void btnDong_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void btnChonFile_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog ofd = new OpenFileDialog())
            {
                ofd.Title = "Chọn file dữ liệu kiểm kho để nhập";
                ofd.Filter = "File dữ liệu (*.xlsx;*.xls;*.csv)|*.xlsx;*.xls;*.csv|File Excel (*.xlsx;*.xls)|*.xlsx;*.xls|File CSV (*.csv)|*.csv|Tất cả các file (*.*)|*.*";
                ofd.Multiselect = false;
                if (ofd.ShowDialog() == DialogResult.OK)
                {
                    txtDuongDanFile.Text = ofd.FileName;
                }
            }
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
                        html.AppendLine("<th>ĐVT</th>");
                        html.AppendLine("<th>SL thực tế (*)</th>");
                        html.AppendLine("<th>Ghi chú</th>");
                        html.AppendLine("</tr>");

                        // Nạp 3 dòng mẫu từ CSDL nếu có
                        DataTable dtHang = DataProcessing.ReadData("SELECT TOP 3 h.MaHangNoiBo, h.TenHang, d.TenDVT FROM HangHoa h LEFT JOIN DonViTinh d ON h.MaDVT = d.MaDVT WHERE h.TrangThai = 'DANG_BAN'");
                        if (dtHang != null && dtHang.Rows.Count > 0)
                        {
                            int sl = 10;
                            foreach (DataRow r in dtHang.Rows)
                            {
                                html.AppendLine("<tr>");
                                html.AppendLine($"<td class=\"text\">{r["MaHangNoiBo"]}</td>");
                                html.AppendLine($"<td>{r["TenHang"]}</td>");
                                html.AppendLine($"<td>{r["TenDVT"]}</td>");
                                html.AppendLine($"<td class=\"number\">{sl}</td>");
                                html.AppendLine("<td>Kiểm kho định kỳ</td>");
                                html.AppendLine("</tr>");
                                sl += 5;
                            }
                        }
                        else
                        {
                            html.AppendLine("<tr><td class=\"text\">SP00001</td><td>Sản phẩm mẫu 1</td><td>Cái</td><td class=\"number\">15</td><td>Kiểm kho định kỳ</td></tr>");
                            html.AppendLine("<tr><td class=\"text\">SP00002</td><td>Sản phẩm mẫu 2</td><td>Hộp</td><td class=\"number\">20</td><td>Kiểm kho định kỳ</td></tr>");
                        }

                        html.AppendLine("</table></body></html>");

                        File.WriteAllText(sfd.FileName, html.ToString(), Encoding.UTF8);
                        MessageBox.Show("Đã lưu file mẫu Excel thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Lỗi khi lưu file mẫu: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        #endregion

        #region Thực Hiện Nhập File

        private void btnThucHien_Click(object sender, EventArgs e)
        {
            string duongDan = txtDuongDanFile.Text.Trim();
            if (string.IsNullOrEmpty(duongDan) || !File.Exists(duongDan))
            {
                MessageBox.Show("Vui lòng chọn một file dữ liệu hợp lệ trước khi thực hiện!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                btnChonFile.Focus();
                return;
            }

            btnThucHien.Enabled = false;
            btnDong.Enabled = false;
            btnChonFile.Enabled = false;
            btnTaiFileMauExcel.Enabled = false;
            Cursor = Cursors.WaitCursor;
            prgTienTrinh.Visible = true;
            prgTienTrinh.Style = ProgressBarStyle.Marquee;
            Application.DoEvents();

            try
            {
                DataTable dtRaw = DocFileTho(duongDan);
                if (dtRaw == null || dtRaw.Rows.Count == 0)
                {
                    MessageBox.Show("Không tìm thấy dữ liệu hợp lệ nào trong file!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // Tra cứu danh sách hàng hóa trong hệ thống
                DataTable dtHang = DataProcessing.ReadData("SELECT h.MaHang, h.MaHangNoiBo, h.TenHang, d.TenDVT, h.GiaVon FROM HangHoa h LEFT JOIN DonViTinh d ON h.MaDVT = d.MaDVT");
                Dictionary<string, DataRow> dictHang = new Dictionary<string, DataRow>(StringComparer.OrdinalIgnoreCase);
                if (dtHang != null)
                {
                    foreach (DataRow r in dtHang.Rows)
                    {
                        string ma = r["MaHangNoiBo"]?.ToString()?.Trim();
                        if (!string.IsNullOrEmpty(ma) && !dictHang.ContainsKey(ma))
                        {
                            dictHang.Add(ma, r);
                        }
                    }
                }

                KhoiTaoBangKetQua();
                Dictionary<int, DataRow> dictKetQua = new Dictionary<int, DataRow>();
                int themMoi = 0;
                int capNhat = 0;
                bool capNhatTrung = rdoCapNhat.Checked;

                foreach (DataRow row in dtRaw.Rows)
                {
                    string maHangNoiBo = row["MaHang"]?.ToString()?.Trim();
                    if (string.IsNullOrEmpty(maHangNoiBo)) continue;

                    if (!dictHang.ContainsKey(maHangNoiBo)) continue; // Bỏ qua nếu mã hàng không tồn tại trong hệ thống

                    DataRow hInfo = dictHang[maHangNoiBo];
                    int maHang = Convert.ToInt32(hInfo["MaHang"]);
                    decimal donGia = hInfo["GiaVon"] != DBNull.Value ? Convert.ToDecimal(hInfo["GiaVon"]) : 0;
                    string tenHang = hInfo["TenHang"]?.ToString() ?? "";
                    string tenDvt = hInfo["TenDVT"]?.ToString() ?? "";

                    decimal slThucTe = 0;
                    if (row["SoLuongThucTe"] != DBNull.Value && decimal.TryParse(row["SoLuongThucTe"].ToString(), out decimal parsedSl))
                    {
                        slThucTe = parsedSl >= 0 ? parsedSl : 0;
                    }

                    string ghiChu = row["GhiChu"]?.ToString() ?? "";

                    if (dictKetQua.ContainsKey(maHang))
                    {
                        if (capNhatTrung)
                        {
                            dictKetQua[maHang]["SoLuongThucTe"] = slThucTe;
                            if (!string.IsNullOrEmpty(ghiChu))
                            {
                                dictKetQua[maHang]["GhiChu"] = ghiChu;
                            }
                            capNhat++;
                        }
                    }
                    else
                    {
                        DataRow rNew = DtKetQuaNhap.NewRow();
                        rNew["STT"] = DtKetQuaNhap.Rows.Count + 1;
                        rNew["MaHang"] = maHang;
                        rNew["MaHangNoiBo"] = maHangNoiBo;
                        rNew["TenHang"] = tenHang;
                        rNew["TenDVT"] = tenDvt;
                        rNew["SoLuongThucTe"] = slThucTe;
                        rNew["DonGia"] = donGia;
                        rNew["GhiChu"] = ghiChu;

                        DtKetQuaNhap.Rows.Add(rNew);
                        dictKetQua.Add(maHang, rNew);
                        themMoi++;
                    }
                }

                SoLuongThemMoi = themMoi;
                SoLuongCapNhat = capNhat;

                if (DtKetQuaNhap.Rows.Count == 0)
                {
                    MessageBox.Show("Không tìm thấy dòng dữ liệu nào khớp với danh mục hàng hóa trong hệ thống!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                MessageBox.Show($"Đã đọc thành công {DtKetQuaNhap.Rows.Count} mặt hàng kiểm kho từ file!\n(Thêm mới: {themMoi}, Cập nhật trùng: {capNhat})", "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Có lỗi xảy ra trong quá trình xử lý file: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                btnThucHien.Enabled = true;
                btnDong.Enabled = true;
                btnChonFile.Enabled = true;
                btnTaiFileMauExcel.Enabled = true;
                Cursor = Cursors.Default;
                prgTienTrinh.Visible = false;
            }
        }

        #endregion

        #region Đọc File Dữ Liệu (Excel & CSV)

        private DataTable DocFileTho(string filePath)
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("MaHang", typeof(string));
            dt.Columns.Add("TenHang", typeof(string));
            dt.Columns.Add("DonViTinh", typeof(string));
            dt.Columns.Add("SoLuongThucTe", typeof(string));
            dt.Columns.Add("GhiChu", typeof(string));

            string ext = Path.GetExtension(filePath).ToLower();
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

            char sep = lines[0].Contains(";") ? ';' : (lines[0].Contains("\t") ? '\t' : ',');

            for (int i = 1; i < lines.Length; i++)
            {
                string line = lines[i].Trim();
                if (string.IsNullOrEmpty(line)) continue;

                string[] parts = line.Split(sep);
                if (parts.Length < 2) continue;

                DataRow r = dt.NewRow();
                r["MaHang"] = parts[0].Trim().Trim('\"');
                if (parts.Length >= 4)
                {
                    r["TenHang"] = parts[1].Trim().Trim('\"');
                    r["DonViTinh"] = parts[2].Trim().Trim('\"');
                    r["SoLuongThucTe"] = parts[3].Trim().Trim('\"');
                    r["GhiChu"] = parts.Length > 4 ? parts[4].Trim().Trim('\"') : "";
                }
                else
                {
                    r["SoLuongThucTe"] = parts[1].Trim().Trim('\"');
                    r["GhiChu"] = parts.Length > 2 ? parts[2].Trim().Trim('\"') : "";
                }
                dt.Rows.Add(r);
            }
        }

        private void DocFileXlsx(string filePath, DataTable dt)
        {
            try
            {
                using (ZipArchive archive = ZipFile.OpenRead(filePath))
                {
                    List<string> sharedStrings = new List<string>();
                    ZipArchiveEntry sstEntry = archive.GetEntry("xl/sharedStrings.xml");
                    if (sstEntry != null)
                    {
                        using (Stream s = sstEntry.Open())
                        {
                            XDocument xdoc = XDocument.Load(s);
                            XNamespace ns = "http://schemas.openxmlformats.org/spreadsheetml/2006/main";
                            foreach (XElement si in xdoc.Descendants(ns + "si"))
                            {
                                StringBuilder sb = new StringBuilder();
                                foreach (XElement t in si.Descendants(ns + "t"))
                                {
                                    sb.Append(t.Value);
                                }
                                sharedStrings.Add(sb.ToString());
                            }
                        }
                    }

                    ZipArchiveEntry sheetEntry = archive.GetEntry("xl/worksheets/sheet1.xml");
                    if (sheetEntry != null)
                    {
                        using (Stream s = sheetEntry.Open())
                        {
                            XDocument xdoc = XDocument.Load(s);
                            XNamespace ns = "http://schemas.openxmlformats.org/spreadsheetml/2006/main";
                            IEnumerable<XElement> rows = xdoc.Descendants(ns + "row");

                            bool isHeader = true;
                            foreach (XElement row in rows)
                            {
                                if (isHeader)
                                {
                                    isHeader = false;
                                    continue;
                                }

                                Dictionary<string, string> cellValues = new Dictionary<string, string>();
                                foreach (XElement c in row.Elements(ns + "c"))
                                {
                                    string cellRef = (string)c.Attribute("r");
                                    string colRef = Regex.Replace(cellRef, @"[\d]", "");
                                    string type = (string)c.Attribute("t");
                                    string val = c.Element(ns + "v")?.Value ?? "";

                                    if (type == "s" && int.TryParse(val, out int idx) && idx >= 0 && idx < sharedStrings.Count)
                                    {
                                        val = sharedStrings[idx];
                                    }
                                    cellValues[colRef] = val;
                                }

                                if (cellValues.ContainsKey("A") && !string.IsNullOrEmpty(cellValues["A"]))
                                {
                                    DataRow r = dt.NewRow();
                                    r["MaHang"] = cellValues["A"].Trim();
                                    r["TenHang"] = cellValues.ContainsKey("B") ? cellValues["B"].Trim() : "";
                                    r["DonViTinh"] = cellValues.ContainsKey("C") ? cellValues["C"].Trim() : "";
                                    r["SoLuongThucTe"] = cellValues.ContainsKey("D") ? cellValues["D"].Trim() : (cellValues.ContainsKey("B") ? cellValues["B"].Trim() : "0");
                                    r["GhiChu"] = cellValues.ContainsKey("E") ? cellValues["E"].Trim() : "";
                                    dt.Rows.Add(r);
                                }
                            }
                        }
                    }
                }
            }
            catch
            {
                DocFileCsv(filePath, dt);
            }
        }

        private void DocFileXls(string filePath, DataTable dt)
        {
            string content = File.ReadAllText(filePath, Encoding.UTF8);
            if (content.Contains("<table") || content.Contains("<html"))
            {
                MatchCollection rows = Regex.Matches(content, @"<tr[^>]*>(.*?)</tr>", RegexOptions.Singleline | RegexOptions.IgnoreCase);
                bool isHeader = true;

                foreach (Match rowMatch in rows)
                {
                    if (isHeader)
                    {
                        isHeader = false;
                        continue;
                    }

                    MatchCollection cells = Regex.Matches(rowMatch.Groups[1].Value, @"<t[dh][^>]*>(.*?)</t[dh]>", RegexOptions.Singleline | RegexOptions.IgnoreCase);
                    if (cells.Count >= 2)
                    {
                        DataRow r = dt.NewRow();
                        r["MaHang"] = Regex.Replace(cells[0].Groups[1].Value, @"<[^>]+>", "").Trim();
                        if (cells.Count >= 4)
                        {
                            r["TenHang"] = Regex.Replace(cells[1].Groups[1].Value, @"<[^>]+>", "").Trim();
                            r["DonViTinh"] = Regex.Replace(cells[2].Groups[1].Value, @"<[^>]+>", "").Trim();
                            r["SoLuongThucTe"] = Regex.Replace(cells[3].Groups[1].Value, @"<[^>]+>", "").Trim();
                            r["GhiChu"] = cells.Count > 4 ? Regex.Replace(cells[4].Groups[1].Value, @"<[^>]+>", "").Trim() : "";
                        }
                        else
                        {
                            r["SoLuongThucTe"] = Regex.Replace(cells[1].Groups[1].Value, @"<[^>]+>", "").Trim();
                            r["GhiChu"] = cells.Count > 2 ? Regex.Replace(cells[2].Groups[1].Value, @"<[^>]+>", "").Trim() : "";
                        }
                        dt.Rows.Add(r);
                    }
                }
            }
        }

        #endregion
    }
}