using BTL_LTTQ_2.Classes;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Globalization;
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

        /// <summary>
        /// true: cập nhật lại SL nếu trùng mã hàng; false ("Bỏ qua"): không sửa các dòng đã có trong phiếu
        /// </summary>
        public bool CapNhatNeuTrung { get; private set; } = true;

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
                                html.AppendLine($"<td class=\"text\">{System.Net.WebUtility.HtmlEncode(r["MaHangNoiBo"].ToString())}</td>");
                                html.AppendLine($"<td>{System.Net.WebUtility.HtmlEncode(r["TenHang"].ToString())}</td>");
                                html.AppendLine($"<td>{System.Net.WebUtility.HtmlEncode(r["TenDVT"].ToString())}</td>");
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
                CapNhatNeuTrung = capNhatTrung;

                // Danh sách dòng bị bỏ qua kèm lý do (báo cáo cho người dùng)
                List<string> dsBoQua = new List<string>();

                foreach (DataRow row in dtRaw.Rows)
                {
                    string maHangNoiBo = row["MaHang"]?.ToString()?.Trim();
                    if (string.IsNullOrEmpty(maHangNoiBo)) continue;

                    string dongSo = row["DongSo"]?.ToString();
                    if (!dictHang.ContainsKey(maHangNoiBo))
                    {
                        dsBoQua.Add($"Dòng {dongSo}: mã hàng '{maHangNoiBo}' không tồn tại trong hệ thống");
                        continue;
                    }

                    DataRow hInfo = dictHang[maHangNoiBo];
                    int maHang = Convert.ToInt32(hInfo["MaHang"]);
                    decimal donGia = hInfo["GiaVon"] != DBNull.Value ? Convert.ToDecimal(hInfo["GiaVon"]) : 0;
                    string tenHang = hInfo["TenHang"]?.ToString() ?? "";
                    string tenDvt = hInfo["TenDVT"]?.ToString() ?? "";

                    // Số lượng thực tế bắt buộc là số nguyên >= 0 (ô trống hoặc sai định dạng: bỏ qua dòng)
                    string slChuoi = row["SoLuongThucTe"]?.ToString()?.Trim() ?? "";
                    if (string.IsNullOrEmpty(slChuoi))
                    {
                        dsBoQua.Add($"Dòng {dongSo}: mã '{maHangNoiBo}' chưa nhập SL thực tế");
                        continue;
                    }
                    if (!TryDocSoLuongNguyen(slChuoi, out int slNguyen))
                    {
                        dsBoQua.Add($"Dòng {dongSo}: mã '{maHangNoiBo}' có SL thực tế '{slChuoi}' không hợp lệ (phải là số nguyên >= 0)");
                        continue;
                    }
                    decimal slThucTe = slNguyen;

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
                        else
                        {
                            dsBoQua.Add($"Dòng {dongSo}: mã '{maHangNoiBo}' bị trùng trong file");
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
                    MessageBox.Show("Không tìm thấy dòng dữ liệu hợp lệ nào khớp với danh mục hàng hóa trong hệ thống!" + TaoThongBaoBoQua(dsBoQua), "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                MessageBox.Show($"Đã đọc thành công {DtKetQuaNhap.Rows.Count} mặt hàng kiểm kho từ file!\n(Thêm mới: {themMoi}, Cập nhật trùng: {capNhat})" + TaoThongBaoBoQua(dsBoQua), "Thành công", MessageBoxButtons.OK, dsBoQua.Count > 0 ? MessageBoxIcon.Warning : MessageBoxIcon.Information);
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

        /// <summary>
        /// Đọc số lượng nguyên không phụ thuộc cấu hình vùng: chấp nhận số nguyên thuần, có dấu phân cách hàng nghìn
        /// (1,234 / 1.234) hoặc dạng thập phân có phần lẻ bằng 0 do Excel lưu (10.0 / 10,00). Không chấp nhận số âm, số lẻ.
        /// </summary>
        private static bool TryDocSoLuongNguyen(string giaTri, out int soLuong)
        {
            soLuong = 0;
            if (string.IsNullOrWhiteSpace(giaTri)) return false;

            string s = giaTri.Trim().Replace(" ", "").Replace(" ", "");
            if (Regex.IsMatch(s, @"^\d{1,3}([.,]\d{3})+$"))
            {
                s = s.Replace(",", "").Replace(".", "");
            }
            else if (Regex.IsMatch(s, @"^\d+[.,]0+$"))
            {
                s = s.Substring(0, s.IndexOfAny(new[] { '.', ',' }));
            }
            return int.TryParse(s, NumberStyles.None, CultureInfo.InvariantCulture, out soLuong);
        }

        /// <summary>
        /// Tạo đoạn thông báo các dòng bị bỏ qua (tối đa 10 dòng kèm lý do)
        /// </summary>
        private static string TaoThongBaoBoQua(List<string> dsBoQua)
        {
            if (dsBoQua == null || dsBoQua.Count == 0) return "";

            StringBuilder sb = new StringBuilder();
            sb.AppendLine();
            sb.AppendLine();
            sb.AppendLine($"Bỏ qua {dsBoQua.Count} dòng không hợp lệ:");
            for (int i = 0; i < dsBoQua.Count && i < 10; i++)
            {
                sb.AppendLine("- " + dsBoQua[i]);
            }
            if (dsBoQua.Count > 10)
            {
                sb.AppendLine($"... và {dsBoQua.Count - 10} dòng khác.");
            }
            return sb.ToString().TrimEnd();
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
            dt.Columns.Add("DongSo", typeof(int)); // Số dòng trong file (phục vụ báo lỗi)

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

                string[] parts = DanhSachHangHoa.frmNhapHangHoaFile.TachDongCsv(line, sep).ToArray();
                if (parts.Length < 2) continue;

                DataRow r = dt.NewRow();
                r["DongSo"] = i + 1;
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
                            int dongSo = 0;
                            foreach (XElement row in rows)
                            {
                                dongSo++;
                                if (isHeader)
                                {
                                    isHeader = false;
                                    continue;
                                }
                                // Ưu tiên số dòng thật trong sheet (thuộc tính r của thẻ row)
                                int dongThuc;
                                if (!int.TryParse((string)row.Attribute("r"), out dongThuc)) dongThuc = dongSo;

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
                                    else if (type == "inlineStr")
                                    {
                                        // Chuỗi nhúng trực tiếp trong ô (<is><t>...</t></is>), không có thẻ <v>
                                        StringBuilder sbInline = new StringBuilder();
                                        foreach (XElement t in c.Descendants(ns + "t"))
                                        {
                                            sbInline.Append(t.Value);
                                        }
                                        val = sbInline.ToString();
                                    }
                                    cellValues[colRef] = val;
                                }

                                if (cellValues.ContainsKey("A") && !string.IsNullOrEmpty(cellValues["A"]))
                                {
                                    DataRow r = dt.NewRow();
                                    r["DongSo"] = dongThuc;
                                    r["MaHang"] = cellValues["A"].Trim();
                                    r["TenHang"] = cellValues.ContainsKey("B") ? cellValues["B"].Trim() : "";
                                    r["DonViTinh"] = cellValues.ContainsKey("C") ? cellValues["C"].Trim() : "";
                                    // File đủ cột (có ĐVT/Ghi chú) mà trống cột D: coi là chưa nhập SL; file 2 cột (Mã, SL): lấy cột B
                                    bool duCot = cellValues.ContainsKey("C") || cellValues.ContainsKey("E");
                                    r["SoLuongThucTe"] = cellValues.ContainsKey("D") ? cellValues["D"].Trim() : (!duCot && cellValues.ContainsKey("B") ? cellValues["B"].Trim() : "");
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
                int dongSo = 0;

                foreach (Match rowMatch in rows)
                {
                    dongSo++;
                    if (isHeader)
                    {
                        isHeader = false;
                        continue;
                    }

                    MatchCollection cells = Regex.Matches(rowMatch.Groups[1].Value, @"<t[dh][^>]*>(.*?)</t[dh]>", RegexOptions.Singleline | RegexOptions.IgnoreCase);
                    if (cells.Count >= 2)
                    {
                        DataRow r = dt.NewRow();
                        r["DongSo"] = dongSo;
                        r["MaHang"] = LayNoiDungO(cells[0]);
                        if (cells.Count >= 4)
                        {
                            r["TenHang"] = LayNoiDungO(cells[1]);
                            r["DonViTinh"] = LayNoiDungO(cells[2]);
                            r["SoLuongThucTe"] = LayNoiDungO(cells[3]);
                            r["GhiChu"] = cells.Count > 4 ? LayNoiDungO(cells[4]) : "";
                        }
                        else
                        {
                            r["SoLuongThucTe"] = LayNoiDungO(cells[1]);
                            r["GhiChu"] = cells.Count > 2 ? LayNoiDungO(cells[2]) : "";
                        }
                        dt.Rows.Add(r);
                    }
                }
            }
            else
            {
                // File .xls nhị phân (Excel 97-2003) không đọc được khi không dùng thư viện ngoài
                throw new InvalidOperationException("File .xls định dạng Excel 97-2003 chưa được hỗ trợ. Vui lòng lưu lại file dưới dạng .xlsx hoặc .csv rồi nhập lại.");
            }
        }

        /// <summary>
        /// Lấy nội dung text của 1 ô HTML: bỏ thẻ con và giải mã ký tự HTML (&amp;amp;, &amp;nbsp;...)
        /// </summary>
        private static string LayNoiDungO(Match o)
        {
            string text = Regex.Replace(o.Groups[1].Value, @"<[^>]+>", "");
            return System.Net.WebUtility.HtmlDecode(text).Trim();
        }

        #endregion
    }
}