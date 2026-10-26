using BTL_LTTQ_2.Classes;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.IO.Compression;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using System.Xml.Linq;

namespace BTL_LTTQ_2.GUI.Forms.TraHangNhap
{
    public partial class frmNhapHangTraFile : Form
    {
        public DataTable DtKetQuaNhap { get; private set; }
        public int SoLuongThemMoi { get; private set; }
        public int SoLuongCapNhat { get; private set; }

        /// <summary>
        /// Lựa chọn của người dùng khi trùng mã hàng: true = Cộng dồn/Cập nhật, false = Bỏ qua (không sửa dòng đã có trong phiếu)
        /// </summary>
        public bool CapNhatKhiTrung { get; private set; }
        private int maKhoXuat;

        public frmNhapHangTraFile(int maKhoXuat = 0)
        {
            InitializeComponent();
            this.maKhoXuat = maKhoXuat > 0 ? maKhoXuat : SessionContext.MaKhoHienTai;
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
            DtKetQuaNhap.Columns.Add("TonKho", typeof(decimal));
            DtKetQuaNhap.Columns.Add("SoLuongTra", typeof(decimal));
            DtKetQuaNhap.Columns.Add("DonGiaTra", typeof(decimal));
            DtKetQuaNhap.Columns.Add("ThanhTien", typeof(decimal));
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
                ofd.Title = "Chọn file dữ liệu hàng trả để nạp";
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
                sfd.Title = "Lưu file mẫu trả hàng Excel";
                sfd.Filter = "File Excel (*.xls)|*.xls";
                sfd.FileName = "Mau_Tra_Hang.xls";

                if (sfd.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        StringBuilder html = new StringBuilder();
                        html.AppendLine("<html xmlns:o=\"urn:schemas-microsoft-com:office:office\" xmlns:x=\"urn:schemas-microsoft-com:office:excel\" xmlns=\"http://www.w3.org/TR/REC-html40\">");
                        html.AppendLine("<head>");
                        html.AppendLine("<meta http-equiv=\"Content-Type\" content=\"text/html; charset=utf-8\" />");
                        html.AppendLine("<!--[if gte mso 9]><xml><x:ExcelWorkbook><x:ExcelWorksheets><x:ExcelWorksheet><x:Name>MauTraHang</x:Name><x:WorksheetOptions><x:DisplayGridlines/></x:WorksheetOptions></x:ExcelWorksheet></x:ExcelWorksheets></x:ExcelWorkbook></xml><![endif]-->");
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
                        html.AppendLine("<th>SL trả (*)</th>");
                        html.AppendLine("<th>Giá trả lại</th>");
                        html.AppendLine("<th>Ghi chú</th>");
                        html.AppendLine("</tr>");

                        string sqlMau = @"
                            SELECT TOP 3 h.MaHangNoiBo, h.TenHang, d.TenDVT, h.GiaVon, ISNULL(t.SoLuongTon, 0) AS TonKho
                            FROM HangHoa h
                            LEFT JOIN DonViTinh d ON h.MaDVT = d.MaDVT
                            OUTER APPLY (SELECT SoLuongTon FROM vw_TonKhoHienTai WHERE MaHang = h.MaHang AND MaKho = @MaKho) t
                            WHERE h.TrangThai = 'DANG_BAN' AND ISNULL(t.SoLuongTon, 0) > 0";

                        SqlParameter[] prms = { new SqlParameter("@MaKho", maKhoXuat) };
                        DataTable dtHang = DataProcessing.ReadData(sqlMau, prms);

                        if (dtHang != null && dtHang.Rows.Count > 0)
                        {
                            foreach (DataRow r in dtHang.Rows)
                            {
                                decimal ton = Convert.ToDecimal(r["TonKho"]);
                                decimal giaVon = r["GiaVon"] != DBNull.Value ? Convert.ToDecimal(r["GiaVon"]) : 100000;
                                decimal slTra = ton >= 2 ? 2 : 1;

                                html.AppendLine("<tr>");
                                html.AppendLine($"<td class=\"text\">{r["MaHangNoiBo"]}</td>");
                                html.AppendLine($"<td>{r["TenHang"]}</td>");
                                html.AppendLine($"<td>{r["TenDVT"]}</td>");
                                html.AppendLine($"<td class=\"number\">{slTra}</td>");
                                html.AppendLine($"<td class=\"number\">{giaVon}</td>");
                                html.AppendLine("<td>Trả hàng nhà cung cấp</td>");
                                html.AppendLine("</tr>");
                            }
                        }
                        else
                        {
                            html.AppendLine("<tr><td class=\"text\">SP00001</td><td>Sản phẩm mẫu 1</td><td>Cái</td><td class=\"number\">2</td><td class=\"number\">150000</td><td>Trả hàng lỗi</td></tr>");
                            html.AppendLine("<tr><td class=\"text\">SP00002</td><td>Sản phẩm mẫu 2</td><td>Hộp</td><td class=\"number\">5</td><td class=\"number\">200000</td><td>Trả hàng cận date</td></tr>");
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

                // Tra cứu danh sách hàng hóa và tồn kho xuất
                string sqlHang = @"
                    SELECT h.MaHang, h.MaHangNoiBo, h.TenHang, d.TenDVT, h.GiaVon, ISNULL(t.SoLuongTon, 0) AS TonKho
                    FROM HangHoa h
                    LEFT JOIN DonViTinh d ON h.MaDVT = d.MaDVT
                    OUTER APPLY (SELECT SoLuongTon FROM vw_TonKhoHienTai WHERE MaHang = h.MaHang AND MaKho = @MaKho) t
                    WHERE h.TrangThai = 'DANG_BAN'";

                SqlParameter[] prms = { new SqlParameter("@MaKho", maKhoXuat) };
                DataTable dtHang = DataProcessing.ReadData(sqlHang, prms);

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
                int vuotTon = 0;
                bool capNhatTrung = rdoCapNhat.Checked;
                CapNhatKhiTrung = capNhatTrung;
                List<string> dsBoQua = new List<string>();

                foreach (DataRow row in dtRaw.Rows)
                {
                    string dongSo = row["DongSo"] != DBNull.Value ? row["DongSo"].ToString() : "?";
                    string maHangNoiBo = row["MaHang"]?.ToString()?.Trim();
                    if (string.IsNullOrEmpty(maHangNoiBo))
                    {
                        // Dòng trống hoàn toàn thì bỏ qua lặng lẽ, còn dữ liệu khác thì báo thiếu mã hàng
                        bool dongTrong = string.IsNullOrWhiteSpace(row["SoLuongTra"]?.ToString()) && string.IsNullOrWhiteSpace(row["DonGiaTra"]?.ToString())
                                         && string.IsNullOrWhiteSpace(row["TenHang"]?.ToString());
                        if (!dongTrong) dsBoQua.Add($"Dòng {dongSo}: Thiếu mã hàng");
                        continue;
                    }

                    if (!dictHang.ContainsKey(maHangNoiBo))
                    {
                        dsBoQua.Add($"Dòng {dongSo}: Mã hàng '{maHangNoiBo}' không tồn tại hoặc đã ngừng kinh doanh");
                        continue;
                    }

                    DataRow hInfo = dictHang[maHangNoiBo];
                    int maHang = Convert.ToInt32(hInfo["MaHang"]);
                    decimal giaVon = hInfo["GiaVon"] != DBNull.Value ? Convert.ToDecimal(hInfo["GiaVon"]) : 0;
                    decimal tonKho = Convert.ToDecimal(hInfo["TonKho"]);
                    string tenHang = hInfo["TenHang"]?.ToString() ?? "";
                    string tenDvt = hInfo["TenDVT"]?.ToString() ?? "";

                    // Số lượng trả phải là số nguyên dương
                    string slText = row["SoLuongTra"]?.ToString() ?? "";
                    if (!TryDocSo(slText, out decimal slTra) || slTra <= 0)
                    {
                        dsBoQua.Add($"Dòng {dongSo}: Số lượng trả '{slText}' không hợp lệ (phải là số nguyên lớn hơn 0)");
                        continue;
                    }
                    if (slTra != Math.Floor(slTra))
                    {
                        dsBoQua.Add($"Dòng {dongSo}: Số lượng trả '{slText}' không được là số thập phân");
                        continue;
                    }

                    // Giá trả lại: để trống -> dùng giá vốn hệ thống
                    decimal donGiaTra = giaVon;
                    string dgText = row["DonGiaTra"]?.ToString() ?? "";
                    if (!string.IsNullOrWhiteSpace(dgText))
                    {
                        if (!TryDocSo(dgText, out decimal parsedDg) || parsedDg < 0)
                        {
                            dsBoQua.Add($"Dòng {dongSo}: Giá trả lại '{dgText}' không hợp lệ");
                            continue;
                        }
                        donGiaTra = parsedDg;
                    }

                    string ghiChu = row["GhiChu"]?.ToString() ?? "";

                    if (dictKetQua.ContainsKey(maHang))
                    {
                        if (capNhatTrung)
                        {
                            decimal slHienTai = Convert.ToDecimal(dictKetQua[maHang]["SoLuongTra"]);
                            decimal slTong = slHienTai + slTra;
                            if (tonKho > 0 && slTong > tonKho)
                            {
                                slTong = tonKho;
                                vuotTon++;
                            }
                            decimal dgApDung = donGiaTra > 0 ? donGiaTra : Convert.ToDecimal(dictKetQua[maHang]["DonGiaTra"]);

                            dictKetQua[maHang]["SoLuongTra"] = slTong;
                            dictKetQua[maHang]["DonGiaTra"] = dgApDung;
                            dictKetQua[maHang]["ThanhTien"] = slTong * dgApDung;
                            if (!string.IsNullOrEmpty(ghiChu))
                            {
                                dictKetQua[maHang]["GhiChu"] = ghiChu;
                            }
                            capNhat++;
                        }
                        else
                        {
                            dsBoQua.Add($"Dòng {dongSo}: Mã hàng '{maHangNoiBo}' bị trùng trong file (đã chọn 'Bỏ qua')");
                        }
                    }
                    else
                    {
                        if (tonKho > 0 && slTra > tonKho)
                        {
                            slTra = tonKho;
                            vuotTon++;
                        }

                        DataRow rNew = DtKetQuaNhap.NewRow();
                        rNew["STT"] = DtKetQuaNhap.Rows.Count + 1;
                        rNew["MaHang"] = maHang;
                        rNew["MaHangNoiBo"] = maHangNoiBo;
                        rNew["TenHang"] = tenHang;
                        rNew["TenDVT"] = tenDvt;
                        rNew["TonKho"] = tonKho;
                        rNew["SoLuongTra"] = slTra;
                        rNew["DonGiaTra"] = donGiaTra;
                        rNew["ThanhTien"] = slTra * donGiaTra;
                        rNew["GhiChu"] = ghiChu;

                        DtKetQuaNhap.Rows.Add(rNew);
                        dictKetQua.Add(maHang, rNew);
                        themMoi++;
                    }
                }

                SoLuongThemMoi = themMoi;
                SoLuongCapNhat = capNhat;

                // Tóm tắt các dòng bị bỏ qua (tối đa 10 dòng đầu kèm lý do)
                StringBuilder sbBoQua = new StringBuilder();
                if (dsBoQua.Count > 0)
                {
                    sbBoQua.AppendLine($"\n\nCó {dsBoQua.Count:N0} dòng bị bỏ qua:");
                    for (int k = 0; k < dsBoQua.Count && k < 10; k++)
                    {
                        sbBoQua.AppendLine("  • " + dsBoQua[k]);
                    }
                    if (dsBoQua.Count > 10) sbBoQua.AppendLine($"  ... và {dsBoQua.Count - 10:N0} dòng khác.");
                }

                if (DtKetQuaNhap.Rows.Count == 0)
                {
                    MessageBox.Show("Không tìm thấy dòng dữ liệu hợp lệ nào khớp với danh mục hàng hóa trong kho xuất trả!" + sbBoQua.ToString(), "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                string thongBao = $"Đã đọc thành công {DtKetQuaNhap.Rows.Count} mặt hàng trả từ file!\n(Thêm mới: {themMoi}, Cộng dồn: {capNhat})";
                if (vuotTon > 0)
                {
                    thongBao += $"\n\nLưu ý: Có {vuotTon} mặt hàng có số lượng trả vượt quá tồn kho thực tế đã được tự động điều chỉnh về mức tồn kho tối đa.";
                }
                thongBao += sbBoQua.ToString();

                MessageBox.Show(thongBao, "Thành công", MessageBoxButtons.OK, dsBoQua.Count > 0 ? MessageBoxIcon.Warning : MessageBoxIcon.Information);
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
        /// Đọc số (tiền VND / số lượng) không phụ thuộc thiết lập vùng miền của máy:
        /// "1,000" / "1.000" / "1 000" được hiểu là phân cách hàng nghìn (= 1000);
        /// nếu có cả '.' và ',' thì dấu xuất hiện sau cùng là dấu thập phân.
        /// </summary>
        private static bool TryDocSo(string chuoi, out decimal giaTri)
        {
            giaTri = 0;
            if (chuoi == null) return false;
            string s = chuoi.Trim().Replace(" ", "").Replace(" ", "").Replace("₫", "").Replace("đ", "").Replace("Đ", "");
            if (s.Length == 0) return false;

            bool coCham = s.IndexOf('.') >= 0;
            bool coPhay = s.IndexOf(',') >= 0;
            if (coCham && coPhay)
            {
                char dauThapPhan = s.LastIndexOf('.') > s.LastIndexOf(',') ? '.' : ',';
                char dauNghin = dauThapPhan == '.' ? ',' : '.';
                s = s.Replace(dauNghin.ToString(), "").Replace(dauThapPhan, '.');
            }
            else if (coCham || coPhay)
            {
                char dau = coCham ? '.' : ',';
                if (Regex.IsMatch(s, @"^[+-]?\d{1,3}(" + Regex.Escape(dau.ToString()) + @"\d{3})+$"))
                {
                    s = s.Replace(dau.ToString(), ""); // Nhóm 3 chữ số -> phân cách hàng nghìn
                }
                else
                {
                    s = s.Replace(dau, '.'); // Còn lại -> dấu thập phân
                }
            }

            return decimal.TryParse(s, NumberStyles.AllowLeadingSign | NumberStyles.AllowDecimalPoint | NumberStyles.AllowExponent,
                CultureInfo.InvariantCulture, out giaTri);
        }

        #endregion

        #region Đọc File Dữ Liệu (Excel & CSV)

        private DataTable DocFileTho(string filePath)
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("DongSo", typeof(int));
            dt.Columns.Add("MaHang", typeof(string));
            dt.Columns.Add("TenHang", typeof(string));
            dt.Columns.Add("DonViTinh", typeof(string));
            dt.Columns.Add("SoLuongTra", typeof(string));
            dt.Columns.Add("DonGiaTra", typeof(string));
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

                string[] parts = DanhSachHangHoa.frmNhapHangHoaFile.TachDongCsv(line, sep).ToArray();

                DataRow r = dt.NewRow();
                r["DongSo"] = i + 1;
                r["MaHang"] = parts[0].Trim().Trim('\"');
                if (parts.Length < 2)
                {
                    // Dòng thiếu cột số lượng: vẫn đưa vào để báo lý do bỏ qua
                    r["SoLuongTra"] = "";
                }
                else if (parts.Length >= 4)
                {
                    r["TenHang"] = parts[1].Trim().Trim('\"');
                    r["DonViTinh"] = parts[2].Trim().Trim('\"');
                    r["SoLuongTra"] = parts[3].Trim().Trim('\"');
                    r["DonGiaTra"] = parts.Length > 4 ? parts[4].Trim().Trim('\"') : "";
                    r["GhiChu"] = parts.Length > 5 ? parts[5].Trim().Trim('\"') : "";
                }
                else
                {
                    r["SoLuongTra"] = parts[1].Trim().Trim('\"');
                    r["DonGiaTra"] = parts.Length > 2 ? parts[2].Trim().Trim('\"') : "";
                    r["GhiChu"] = parts.Length > 3 ? parts[3].Trim().Trim('\"') : "";
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

                                // Giữ lại mọi dòng có dữ liệu (kể cả thiếu mã hàng) để báo lý do bỏ qua
                                bool coDuLieu = false;
                                foreach (string v in cellValues.Values)
                                {
                                    if (!string.IsNullOrWhiteSpace(v)) { coDuLieu = true; break; }
                                }
                                if (coDuLieu)
                                {
                                    DataRow r = dt.NewRow();
                                    int.TryParse((string)row.Attribute("r"), out int soDongExcel);
                                    r["DongSo"] = soDongExcel;
                                    r["MaHang"] = cellValues.ContainsKey("A") ? cellValues["A"].Trim() : "";
                                    r["TenHang"] = cellValues.ContainsKey("B") ? cellValues["B"].Trim() : "";
                                    r["DonViTinh"] = cellValues.ContainsKey("C") ? cellValues["C"].Trim() : "";
                                    r["SoLuongTra"] = cellValues.ContainsKey("D") ? cellValues["D"].Trim() : (cellValues.ContainsKey("B") ? cellValues["B"].Trim() : "0");
                                    r["DonGiaTra"] = cellValues.ContainsKey("E") ? cellValues["E"].Trim() : "";
                                    r["GhiChu"] = cellValues.ContainsKey("F") ? cellValues["F"].Trim() : "";
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
                int soDong = 0;

                foreach (Match rowMatch in rows)
                {
                    soDong++;
                    if (isHeader)
                    {
                        isHeader = false;
                        continue;
                    }

                    MatchCollection cells = Regex.Matches(rowMatch.Groups[1].Value, @"<t[dh][^>]*>(.*?)</t[dh]>", RegexOptions.Singleline | RegexOptions.IgnoreCase);
                    if (cells.Count >= 1)
                    {
                        DataRow r = dt.NewRow();
                        r["DongSo"] = soDong;
                        r["MaHang"] = Regex.Replace(cells[0].Groups[1].Value, @"<[^>]+>", "").Trim();
                        if (cells.Count < 2)
                        {
                            // Dòng thiếu cột số lượng: nếu dòng trống hoàn toàn thì bỏ, ngược lại đưa vào để báo lý do
                            if (string.IsNullOrWhiteSpace(r["MaHang"].ToString())) continue;
                            r["SoLuongTra"] = "";
                        }
                        else if (cells.Count >= 4)
                        {
                            r["TenHang"] = Regex.Replace(cells[1].Groups[1].Value, @"<[^>]+>", "").Trim();
                            r["DonViTinh"] = Regex.Replace(cells[2].Groups[1].Value, @"<[^>]+>", "").Trim();
                            r["SoLuongTra"] = Regex.Replace(cells[3].Groups[1].Value, @"<[^>]+>", "").Trim();
                            r["DonGiaTra"] = cells.Count > 4 ? Regex.Replace(cells[4].Groups[1].Value, @"<[^>]+>", "").Trim() : "";
                            r["GhiChu"] = cells.Count > 5 ? Regex.Replace(cells[5].Groups[1].Value, @"<[^>]+>", "").Trim() : "";
                        }
                        else
                        {
                            r["SoLuongTra"] = Regex.Replace(cells[1].Groups[1].Value, @"<[^>]+>", "").Trim();
                            r["DonGiaTra"] = cells.Count > 2 ? Regex.Replace(cells[2].Groups[1].Value, @"<[^>]+>", "").Trim() : "";
                            r["GhiChu"] = cells.Count > 3 ? Regex.Replace(cells[3].Groups[1].Value, @"<[^>]+>", "").Trim() : "";
                        }
                        dt.Rows.Add(r);
                    }
                }
            }
        }

        #endregion
    }
}