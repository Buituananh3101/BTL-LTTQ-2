using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.IO.Compression;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using System.Xml.Linq;
using BTL_LTTQ_2.Classes;

namespace BTL_LTTQ_2.GUI.Forms.NhaCungCap
{
    public partial class frmNhapNhaCungCapFile : Form
    {
        public int SoLuongThemMoi { get; private set; }
        public int SoLuongCapNhat { get; private set; }

        public frmNhapNhaCungCapFile()
        {
            InitializeComponent();
        }

        private void btnDong_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnChonFile_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog ofd = new OpenFileDialog())
            {
                ofd.Title = "Chọn file dữ liệu nhà cung cấp để nhập";
                ofd.Filter = "File Excel (*.xlsx;*.xls)|*.xlsx;*.xls|File Excel OpenXML (*.xlsx)|*.xlsx|File Excel 97-2003 (*.xls)|*.xls|File CSV (*.csv)|*.csv|Tất cả các file (*.*)|*.*";
                ofd.Multiselect = false;
                if (ofd.ShowDialog() == DialogResult.OK)
                {
                    txtDuongDanFile.Text = ofd.FileName;
                }
            }
        }

        #region Xuất File Mẫu Chuẩn

        private void btnTaiFileMauExcel_Click(object sender, EventArgs e)
        {
            using (SaveFileDialog sfd = new SaveFileDialog())
            {
                sfd.Title = "Lưu file mẫu Excel Nhà cung cấp";
                sfd.Filter = "File Excel (*.xls)|*.xls";
                sfd.FileName = "Mau_Nhap_Nha_Cung_Cap.xls";

                if (sfd.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        StringBuilder sb = new StringBuilder();
                        sb.AppendLine("<html xmlns:o=\"urn:schemas-microsoft-com:office:office\" xmlns:x=\"urn:schemas-microsoft-com:office:excel\" xmlns=\"http://www.w3.org/TR/REC-html40\">");
                        // Ô dạng văn bản (.text) giữ số 0 đầu của điện thoại / mã số thuế khi mở bằng Excel
                        sb.AppendLine("<head><meta http-equiv=\"Content-Type\" content=\"text/html; charset=utf-8\"><style>.text { mso-number-format: \"\\@\"; }</style></head>");
                        sb.AppendLine("<body><table border=\"1\">");

                        // Dòng tiêu đề cột
                        sb.AppendLine("<tr style=\"background-color:#E2E8F0; font-weight:bold; text-align:center;\">");
                        sb.AppendLine("<td>Mã nhà cung cấp</td>");
                        sb.AppendLine("<td>Tên nhà cung cấp (*)</td>");
                        sb.AppendLine("<td>Nhóm nhà cung cấp</td>");
                        sb.AppendLine("<td>Điện thoại</td>");
                        sb.AppendLine("<td>Email</td>");
                        sb.AppendLine("<td>Địa chỉ</td>");
                        sb.AppendLine("<td>Khu vực</td>");
                        sb.AppendLine("<td>Mã số thuế</td>");
                        sb.AppendLine("<td>Ghi chú</td>");
                        sb.AppendLine("</tr>");

                        // Dòng dữ liệu mẫu 1
                        sb.AppendLine("<tr>");
                        sb.AppendLine("<td>NCC00001</td>");
                        sb.AppendLine("<td>Công ty Cổ phần Sữa Việt Nam (Vinamilk)</td>");
                        sb.AppendLine("<td>Thực phẩm & Đồ uống</td>");
                        sb.AppendLine("<td class=\"text\">02854155555</td>");
                        sb.AppendLine("<td>vinamilk@vinamilk.com.vn</td>");
                        sb.AppendLine("<td>10 Tân Trào, Tân Phú, Quận 7, TP.HCM</td>");
                        sb.AppendLine("<td>Miền Nam</td>");
                        sb.AppendLine("<td class=\"text\">0300588569</td>");
                        sb.AppendLine("<td>Nhà cung cấp sữa tươi và các chế phẩm từ sữa</td>");
                        sb.AppendLine("</tr>");

                        // Dòng dữ liệu mẫu 2
                        sb.AppendLine("<tr>");
                        sb.AppendLine("<td>NCC00002</td>");
                        sb.AppendLine("<td>Tập đoàn Masan Consumer</td>");
                        sb.AppendLine("<td>Gia vị & Tiêu dùng nhanh</td>");
                        sb.AppendLine("<td class=\"text\">02862563862</td>");
                        sb.AppendLine("<td>contact@masangroup.com</td>");
                        sb.AppendLine("<td>Tòa nhà Central Plaza, 17 Lê Duẩn, Quận 1, TP.HCM</td>");
                        sb.AppendLine("<td>Miền Nam</td>");
                        sb.AppendLine("<td class=\"text\">0302017440</td>");
                        sb.AppendLine("<td>Cung ứng nước tương, nước mắm, mì gói</td>");
                        sb.AppendLine("</tr>");

                        sb.AppendLine("</table></body></html>");

                        File.WriteAllText(sfd.FileName, sb.ToString(), Encoding.UTF8);

                        MessageBox.Show("Tải file mẫu Excel nhà cung cấp thành công!\nBạn hãy mở file mẫu vừa lưu, điền dữ liệu và chọn để nhập vào phần mềm.",
                            "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Lỗi khi lưu file mẫu: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        #endregion

        #region Thực Hiện Nhập Dữ Liệu

        private void btnThucHien_Click(object sender, EventArgs e)
        {
            string duongDan = txtDuongDanFile.Text.Trim();
            if (string.IsNullOrEmpty(duongDan) || !File.Exists(duongDan))
            {
                MessageBox.Show("Vui lòng chọn file dữ liệu hợp lệ trước khi thực hiện!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                this.Cursor = Cursors.WaitCursor;
                prgTienTrinh.Visible = true;
                prgTienTrinh.Value = 10;

                List<DongNhaCungCapFile> dsDong = DocDuLieuTuFile(duongDan);
                if (dsDong == null || dsDong.Count == 0)
                {
                    MessageBox.Show("Không tìm thấy dữ liệu nhà cung cấp nào trong file!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                prgTienTrinh.Value = 30;

                bool capNhatKhiTrung = rdoCapNhat.Checked;
                int themMoi = 0;
                int capNhat = 0;
                int boQua = 0;
                int soDongLoi = 0;
                List<string> dsLoi = new List<string>();

                DataProcessing.ChangeDataTransaction((conn, trans) =>
                {
                    // Đọc từ điển nhóm nhà cung cấp có sẵn
                    Dictionary<string, int> dictNhom = new Dictionary<string, int>(StringComparer.OrdinalIgnoreCase);
                    using (SqlCommand cmdNhom = new SqlCommand("SELECT MaNhomNcc, TenNhom FROM NhomNhaCungCap", conn, trans))
                    using (SqlDataReader rdr = cmdNhom.ExecuteReader())
                    {
                        while (rdr.Read())
                        {
                            dictNhom[rdr["TenNhom"].ToString().Trim()] = Convert.ToInt32(rdr["MaNhomNcc"]);
                        }
                    }

                    // Đọc danh sách mã NCC hiện hữu và tên NCC (TenDonVi là UNIQUE) -> mã NCC đang dùng tên đó
                    HashSet<string> setMaNccHienHuu = new HashSet<string>(StringComparer.OrdinalIgnoreCase);
                    Dictionary<string, string> dictTenNcc = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);
                    using (SqlCommand cmdCheck = new SqlCommand("SELECT MaNcc, TenDonVi FROM DonViCapHang", conn, trans))
                    using (SqlDataReader rdr = cmdCheck.ExecuteReader())
                    {
                        while (rdr.Read())
                        {
                            string ma = rdr["MaNcc"] == DBNull.Value ? "" : rdr["MaNcc"].ToString().Trim();
                            if (!string.IsNullOrEmpty(ma)) setMaNccHienHuu.Add(ma);
                            string ten = rdr["TenDonVi"] == DBNull.Value ? "" : rdr["TenDonVi"].ToString().Trim();
                            if (!string.IsNullOrEmpty(ten)) dictTenNcc[ten] = ma;
                        }
                    }

                    // Lấy hậu tố số lớn nhất của mã NCCxxxxx để sinh mã tự động (đồng bộ form thêm NCC thủ công)
                    int soThuTuTiepTheo = 1;
                    using (SqlCommand cmdMax = new SqlCommand(@"
                        SELECT ISNULL(MAX(
                            CASE
                                WHEN MaNcc LIKE 'NCC[0-9]%' THEN TRY_CAST(SUBSTRING(MaNcc, 4, 10) AS INT)
                                ELSE 0
                            END
                        ), 0) + 1
                        FROM DonViCapHang WITH (UPDLOCK, HOLDLOCK)", conn, trans))
                    {
                        soThuTuTiepTheo = Convert.ToInt32(cmdMax.ExecuteScalar());
                    }

                    for (int i = 0; i < dsDong.Count; i++)
                    {
                        var dong = dsDong[i];

                        // Kiểm tra dữ liệu bắt buộc & độ dài trường theo cấu trúc bảng DonViCapHang
                        string tenDonVi = dong.TenDonVi?.Trim() ?? "";
                        string maNccFile = dong.MaNcc?.Trim() ?? "";
                        string lyDoLoi = null;
                        if (string.IsNullOrWhiteSpace(tenDonVi)) lyDoLoi = "Thiếu tên nhà cung cấp";
                        else if (tenDonVi.Length > 150) lyDoLoi = "Tên nhà cung cấp vượt quá 150 ký tự";
                        else if (maNccFile.Length > 30) lyDoLoi = "Mã nhà cung cấp vượt quá 30 ký tự";
                        else if ((dong.DienThoai?.Trim() ?? "").Length > 15) lyDoLoi = "Điện thoại vượt quá 15 ký tự";
                        else if ((dong.Email?.Trim() ?? "").Length > 100) lyDoLoi = "Email vượt quá 100 ký tự";
                        else if ((dong.DiaChi?.Trim() ?? "").Length > 255) lyDoLoi = "Địa chỉ vượt quá 255 ký tự";
                        else if ((dong.KhuVuc?.Trim() ?? "").Length > 100) lyDoLoi = "Khu vực vượt quá 100 ký tự";
                        else if ((dong.MaSoThue?.Trim() ?? "").Length > 30) lyDoLoi = "Mã số thuế vượt quá 30 ký tự";
                        else if ((dong.GhiChu?.Trim() ?? "").Length > 500) lyDoLoi = "Ghi chú vượt quá 500 ký tự";
                        else if ((dong.TenNhom?.Trim() ?? "").Length > 150) lyDoLoi = "Tên nhóm vượt quá 150 ký tự";

                        bool laDongCapNhat = !string.IsNullOrEmpty(maNccFile) && setMaNccHienHuu.Contains(maNccFile);
                        if (lyDoLoi == null && !(laDongCapNhat && !capNhatKhiTrung)
                            && dictTenNcc.TryGetValue(tenDonVi, out string maNccDungTen))
                        {
                            // Tên NCC là duy nhất: chỉ hợp lệ khi chính NCC đó đang được cập nhật
                            if (!(laDongCapNhat && string.Equals(maNccDungTen, maNccFile, StringComparison.OrdinalIgnoreCase)))
                            {
                                lyDoLoi = $"Tên nhà cung cấp '{tenDonVi}' đã tồn tại" + (string.IsNullOrEmpty(maNccDungTen) ? "" : $" (mã {maNccDungTen})");
                            }
                        }

                        if (lyDoLoi != null)
                        {
                            soDongLoi++;
                            dsLoi.Add($"Dòng {dong.DongSo}: {lyDoLoi}");
                            continue;
                        }

                        // Bỏ qua dòng trùng mã khi người dùng chọn "Bỏ qua" (trước khi tạo nhóm mới)
                        if (laDongCapNhat && !capNhatKhiTrung)
                        {
                            boQua++;
                            continue;
                        }

                        // Xử lý nhóm NCC
                        int? maNhom = null;
                        if (!string.IsNullOrWhiteSpace(dong.TenNhom))
                        {
                            string tenNhomTrim = dong.TenNhom.Trim();
                            if (dictNhom.ContainsKey(tenNhomTrim))
                            {
                                maNhom = dictNhom[tenNhomTrim];
                            }
                            else
                            {
                                // Tự động tạo nhóm mới
                                using (SqlCommand cmdTaoNhom = new SqlCommand(
                                    "INSERT INTO NhomNhaCungCap (TenNhom, GhiChu) VALUES (@TenNhom, N'Tự động tạo khi nhập file'); SELECT SCOPE_IDENTITY();", conn, trans))
                                {
                                    cmdTaoNhom.Parameters.AddWithValue("@TenNhom", tenNhomTrim);
                                    int newId = Convert.ToInt32(cmdTaoNhom.ExecuteScalar());
                                    dictNhom[tenNhomTrim] = newId;
                                    maNhom = newId;
                                }
                            }
                        }

                        // Xử lý mã nhà cung cấp: tự sinh NCC + 5 chữ số, lặp đến khi chưa bị dùng
                        string maNcc = maNccFile;
                        if (string.IsNullOrEmpty(maNcc))
                        {
                            do
                            {
                                maNcc = "NCC" + (soThuTuTiepTheo++).ToString("D5");
                            } while (setMaNccHienHuu.Contains(maNcc));
                        }

                        if (laDongCapNhat)
                        {
                            // Cập nhật thông tin NCC (mã đã tồn tại và người dùng chọn "Cập nhật")
                            string updateSql = @"
                                UPDATE DonViCapHang
                                SET TenDonVi = @TenDonVi,
                                    DienThoai = @DienThoai,
                                    Email = @Email,
                                    DiaChi = @DiaChi,
                                    KhuVuc = @KhuVuc,
                                    MaSoThue = @MaSoThue,
                                    GhiChu = @GhiChu,
                                    MaNhomNcc = ISNULL(@MaNhomNcc, MaNhomNcc)
                                WHERE MaNcc = @MaNcc";

                            using (SqlCommand cmdUpdate = new SqlCommand(updateSql, conn, trans))
                            {
                                cmdUpdate.Parameters.AddWithValue("@TenDonVi", tenDonVi);
                                cmdUpdate.Parameters.AddWithValue("@DienThoai", (object)dong.DienThoai?.Trim() ?? DBNull.Value);
                                cmdUpdate.Parameters.AddWithValue("@Email", (object)dong.Email?.Trim() ?? DBNull.Value);
                                cmdUpdate.Parameters.AddWithValue("@DiaChi", (object)dong.DiaChi?.Trim() ?? DBNull.Value);
                                cmdUpdate.Parameters.AddWithValue("@KhuVuc", (object)dong.KhuVuc?.Trim() ?? DBNull.Value);
                                cmdUpdate.Parameters.AddWithValue("@MaSoThue", (object)dong.MaSoThue?.Trim() ?? DBNull.Value);
                                cmdUpdate.Parameters.AddWithValue("@GhiChu", (object)dong.GhiChu?.Trim() ?? DBNull.Value);
                                cmdUpdate.Parameters.AddWithValue("@MaNhomNcc", (object)maNhom ?? DBNull.Value);
                                cmdUpdate.Parameters.AddWithValue("@MaNcc", maNcc);

                                cmdUpdate.ExecuteNonQuery();
                                dictTenNcc[tenDonVi] = maNcc;
                                capNhat++;
                            }
                        }
                        else
                        {
                            // Thêm mới NCC
                            string insertSql = @"
                                INSERT INTO DonViCapHang (
                                    MaNcc, TenDonVi, DienThoai, Email, DiaChi, KhuVuc, MaNhomNcc, MaSoThue, GhiChu, TrangThai, NgayTao
                                ) VALUES (
                                    @MaNcc, @TenDonVi, @DienThoai, @Email, @DiaChi, @KhuVuc, @MaNhomNcc, @MaSoThue, @GhiChu, 'HOAT_DONG', GETDATE()
                                )";

                            using (SqlCommand cmdInsert = new SqlCommand(insertSql, conn, trans))
                            {
                                cmdInsert.Parameters.AddWithValue("@MaNcc", maNcc);
                                cmdInsert.Parameters.AddWithValue("@TenDonVi", tenDonVi);
                                cmdInsert.Parameters.AddWithValue("@DienThoai", (object)dong.DienThoai?.Trim() ?? DBNull.Value);
                                cmdInsert.Parameters.AddWithValue("@Email", (object)dong.Email?.Trim() ?? DBNull.Value);
                                cmdInsert.Parameters.AddWithValue("@DiaChi", (object)dong.DiaChi?.Trim() ?? DBNull.Value);
                                cmdInsert.Parameters.AddWithValue("@KhuVuc", (object)dong.KhuVuc?.Trim() ?? DBNull.Value);
                                cmdInsert.Parameters.AddWithValue("@MaNhomNcc", (object)maNhom ?? DBNull.Value);
                                cmdInsert.Parameters.AddWithValue("@MaSoThue", (object)dong.MaSoThue?.Trim() ?? DBNull.Value);
                                cmdInsert.Parameters.AddWithValue("@GhiChu", (object)dong.GhiChu?.Trim() ?? DBNull.Value);

                                cmdInsert.ExecuteNonQuery();
                                setMaNccHienHuu.Add(maNcc);
                                dictTenNcc[tenDonVi] = maNcc;
                                themMoi++;
                            }
                        }

                        int progress = 30 + (int)((i + 1) * 70.0 / dsDong.Count);
                        if (progress > 100) progress = 100;
                        prgTienTrinh.Value = progress;
                    }
                });

                SoLuongThemMoi = themMoi;
                SoLuongCapNhat = capNhat;

                StringBuilder sbLoi = new StringBuilder();
                if (dsLoi.Count > 0)
                {
                    sbLoi.AppendLine("\nCác dòng bị lỗi (tối đa 10 dòng đầu):");
                    for (int k = 0; k < dsLoi.Count && k < 10; k++)
                    {
                        sbLoi.AppendLine("  • " + dsLoi[k]);
                    }
                    if (dsLoi.Count > 10) sbLoi.AppendLine($"  ... và {dsLoi.Count - 10:N0} dòng lỗi khác.");
                }

                string thongBao = $"KẾT QUẢ NHẬP DỮ LIỆU NHÀ CUNG CẤP:\n\n" +
                                  $"- Thêm mới thành công: {themMoi:N0} nhà cung cấp\n" +
                                  $"- Cập nhật thông tin: {capNhat:N0} nhà cung cấp\n" +
                                  $"- Bỏ qua do trùng mã: {boQua:N0} dòng\n" +
                                  $"- Dòng lỗi không được nhập: {soDongLoi:N0} dòng\n" +
                                  sbLoi.ToString() +
                                  $"\nDữ liệu danh sách nhà cung cấp đã được cập nhật!";

                MessageBox.Show(thongBao, "Hoàn tất nhập dữ liệu", MessageBoxButtons.OK, soDongLoi > 0 ? MessageBoxIcon.Warning : MessageBoxIcon.Information);

                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Đã xảy ra lỗi trong quá trình nhập dữ liệu: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                this.Cursor = Cursors.Default;
                prgTienTrinh.Visible = false;
            }
        }

        #endregion

        #region Bộ Đọc Dữ Liệu Đa Định Dạng (XLSX, XLS, CSV)

        private class DongNhaCungCapFile
        {
            public int DongSo { get; set; }
            public string MaNcc { get; set; }
            public string TenDonVi { get; set; }
            public string TenNhom { get; set; }
            public string DienThoai { get; set; }
            public string Email { get; set; }
            public string DiaChi { get; set; }
            public string KhuVuc { get; set; }
            public string MaSoThue { get; set; }
            public string GhiChu { get; set; }
        }

        private List<DongNhaCungCapFile> DocDuLieuTuFile(string filePath)
        {
            string ext = Path.GetExtension(filePath).ToLower();

            if (ext == ".xlsx")
            {
                return DocFileOpenXmlXlsx(filePath);
            }
            else if (ext == ".xls")
            {
                try
                {
                    List<DongNhaCungCapFile> kqHtml = DocFileHtmlTable(filePath);
                    if (kqHtml != null && kqHtml.Count > 0) return kqHtml;
                }
                catch { }

                return DocFileOleDb(filePath, ext);
            }
            else
            {
                return DocFileCsv(filePath);
            }
        }

        private List<DongNhaCungCapFile> DocFileOpenXmlXlsx(string filePath)
        {
            List<DongNhaCungCapFile> ketQua = new List<DongNhaCungCapFile>();

            using (FileStream fs = new FileStream(filePath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
            using (ZipArchive archive = new ZipArchive(fs, ZipArchiveMode.Read))
            {
                // 1. Đọc sharedStrings.xml
                List<string> sharedStrings = new List<string>();
                ZipArchiveEntry entryShared = archive.GetEntry("xl/sharedStrings.xml");
                if (entryShared != null)
                {
                    using (Stream stream = entryShared.Open())
                    {
                        XDocument xdoc = XDocument.Load(stream);
                        XNamespace ns = "http://schemas.openxmlformats.org/spreadsheetml/2006/main";
                        foreach (XElement si in xdoc.Descendants(ns + "si"))
                        {
                            StringBuilder text = new StringBuilder();
                            foreach (XElement t in si.Descendants(ns + "t"))
                            {
                                text.Append(t.Value);
                            }
                            sharedStrings.Add(text.ToString());
                        }
                    }
                }

                // 2. Tìm sheet1.xml
                ZipArchiveEntry entrySheet = archive.GetEntry("xl/worksheets/sheet1.xml");
                if (entrySheet == null)
                {
                    foreach (var entry in archive.Entries)
                    {
                        if (entry.FullName.StartsWith("xl/worksheets/sheet") && entry.FullName.EndsWith(".xml"))
                        {
                            entrySheet = entry;
                            break;
                        }
                    }
                }

                if (entrySheet == null)
                {
                    throw new Exception("Không tìm thấy bảng tính hợp lệ trong file Excel .xlsx!");
                }

                List<List<string>> rawRows = new List<List<string>>();

                using (Stream stream = entrySheet.Open())
                {
                    XDocument xdoc = XDocument.Load(stream);
                    XNamespace ns = "http://schemas.openxmlformats.org/spreadsheetml/2006/main";

                    foreach (XElement row in xdoc.Descendants(ns + "row"))
                    {
                        List<string> cellValues = new List<string>();
                        int lastColIdx = -1;

                        foreach (XElement c in row.Elements(ns + "c"))
                        {
                            string cellRef = c.Attribute("r")?.Value;
                            int colIdx = ConvertCellRefToColIndex(cellRef);

                            while (lastColIdx + 1 < colIdx)
                            {
                                cellValues.Add("");
                                lastColIdx++;
                            }

                            string cellType = c.Attribute("t")?.Value;
                            XElement v = c.Element(ns + "v");
                            string cellVal = "";

                            if (v != null)
                            {
                                string rawVal = v.Value;
                                if (cellType == "s")
                                {
                                    if (int.TryParse(rawVal, out int strIdx) && strIdx >= 0 && strIdx < sharedStrings.Count)
                                    {
                                        cellVal = sharedStrings[strIdx];
                                    }
                                }
                                else
                                {
                                    cellVal = rawVal;
                                }
                            }
                            else
                            {
                                XElement isEl = c.Element(ns + "is");
                                if (isEl != null)
                                {
                                    cellVal = isEl.Value;
                                }
                            }

                            cellValues.Add(cellVal);
                            lastColIdx = colIdx;
                        }

                        rawRows.Add(cellValues);
                    }
                }

                return ParseRawRowsToNhaCungCap(rawRows);
            }
        }

        private int ConvertCellRefToColIndex(string cellRef)
        {
            if (string.IsNullOrEmpty(cellRef)) return 0;
            string letters = Regex.Replace(cellRef, @"[\d]", "");
            int col = 0;
            for (int i = 0; i < letters.Length; i++)
            {
                col = col * 26 + (letters[i] - 'A' + 1);
            }
            return col - 1;
        }

        private List<DongNhaCungCapFile> DocFileHtmlTable(string filePath)
        {
            string html = File.ReadAllText(filePath, Encoding.UTF8);
            if (!html.Contains("<table") && !html.Contains("<TABLE"))
            {
                return null;
            }

            List<List<string>> rawRows = new List<List<string>>();
            MatchCollection rowMatches = Regex.Matches(html, @"<tr[^>]*>(.*?)<\/tr>", RegexOptions.Singleline | RegexOptions.IgnoreCase);

            foreach (Match rowMatch in rowMatches)
            {
                List<string> cells = new List<string>();
                MatchCollection cellMatches = Regex.Matches(rowMatch.Groups[1].Value, @"<t[dh][^>]*>(.*?)<\/t[dh]>", RegexOptions.Singleline | RegexOptions.IgnoreCase);
                foreach (Match c in cellMatches)
                {
                    string txt = Regex.Replace(c.Groups[1].Value, @"<[^>]+>", "");
                    txt = System.Net.WebUtility.HtmlDecode(txt).Trim();
                    cells.Add(txt);
                }
                rawRows.Add(cells);
            }

            return ParseRawRowsToNhaCungCap(rawRows);
        }

        private List<DongNhaCungCapFile> DocFileCsv(string filePath)
        {
            List<List<string>> rawRows = new List<List<string>>();
            string[] lines = File.ReadAllLines(filePath, Encoding.UTF8);

            foreach (string line in lines)
            {
                if (string.IsNullOrWhiteSpace(line)) continue;
                List<string> cells = new List<string>();
                bool inQuotes = false;
                StringBuilder cur = new StringBuilder();

                for (int i = 0; i < line.Length; i++)
                {
                    char c = line[i];
                    if (c == '"')
                    {
                        if (inQuotes && i + 1 < line.Length && line[i + 1] == '"')
                        {
                            cur.Append('"');
                            i++;
                        }
                        else
                        {
                            inQuotes = !inQuotes;
                        }
                    }
                    else if (c == ',' && !inQuotes)
                    {
                        cells.Add(cur.ToString().Trim());
                        cur.Clear();
                    }
                    else
                    {
                        cur.Append(c);
                    }
                }
                cells.Add(cur.ToString().Trim());
                rawRows.Add(cells);
            }

            return ParseRawRowsToNhaCungCap(rawRows);
        }

        private List<DongNhaCungCapFile> DocFileOleDb(string filePath, string ext)
        {
            List<List<string>> rawRows = new List<List<string>>();
            try
            {
                string connStr = ext == ".xls"
                    ? $"Provider=Microsoft.Jet.OLEDB.4.0;Data Source={filePath};Extended Properties=\"Excel 8.0;HDR=No;IMEX=1;\""
                    : $"Provider=Microsoft.ACE.OLEDB.12.0;Data Source={filePath};Extended Properties=\"Excel 12.0 Xml;HDR=No;IMEX=1;\"";

                using (OleDbConnection conn = new OleDbConnection(connStr))
                {
                    conn.Open();
                    DataTable dtSheets = conn.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null);
                    if (dtSheets != null && dtSheets.Rows.Count > 0)
                    {
                        string sheetName = dtSheets.Rows[0]["TABLE_NAME"].ToString();
                        using (OleDbCommand cmd = new OleDbCommand($"SELECT * FROM [{sheetName}]", conn))
                        using (OleDbDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                List<string> row = new List<string>();
                                for (int i = 0; i < reader.FieldCount; i++)
                                {
                                    row.Add(reader[i]?.ToString()?.Trim() ?? "");
                                }
                                rawRows.Add(row);
                            }
                        }
                    }
                }
            }
            catch (Exception exOleDb)
            {
                // File Excel nhị phân thật (OLE: D0 CF 11 E0) mà không mở được bằng OLEDB thì không đọc dạng text (tránh nhập chuỗi rác)
                byte[] dauFile = new byte[4];
                using (FileStream fs = new FileStream(filePath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
                {
                    fs.Read(dauFile, 0, dauFile.Length);
                }
                if (dauFile[0] == 0xD0 && dauFile[1] == 0xCF && dauFile[2] == 0x11 && dauFile[3] == 0xE0)
                {
                    throw new Exception("Không thể đọc file Excel 97-2003 (.xls) này trên máy hiện tại. Vui lòng lưu file sang định dạng .xlsx hoặc CSV (UTF-8) rồi nhập lại! Chi tiết: " + exOleDb.Message);
                }

                string[] lines = File.ReadAllLines(filePath, Encoding.UTF8);
                foreach (string l in lines)
                {
                    if (!string.IsNullOrWhiteSpace(l))
                    {
                        rawRows.Add(DanhSachHangHoa.frmNhapHangHoaFile.TachDongCsv(l, l.Contains("\t") ? '\t' : ','));
                    }
                }
            }

            return ParseRawRowsToNhaCungCap(rawRows);
        }

        private List<DongNhaCungCapFile> ParseRawRowsToNhaCungCap(List<List<string>> rawRows)
        {
            List<DongNhaCungCapFile> ketQua = new List<DongNhaCungCapFile>();
            if (rawRows == null || rawRows.Count == 0) return ketQua;

            int headerIdx = -1;
            Dictionary<string, int> colMap = new Dictionary<string, int>(StringComparer.OrdinalIgnoreCase);

            for (int r = 0; r < Math.Min(rawRows.Count, 10); r++)
            {
                var row = rawRows[r];
                for (int c = 0; c < row.Count; c++)
                {
                    string h = row[c].ToLower().Replace("*", "").Trim();
                    if (h.Contains("tên nhà cung cấp") || h.Contains("tên ncc") || h.Contains("tên đơn vị"))
                    {
                        headerIdx = r;
                        break;
                    }
                }
                if (headerIdx >= 0) break;
            }

            if (headerIdx >= 0)
            {
                var hRow = rawRows[headerIdx];
                for (int c = 0; c < hRow.Count; c++)
                {
                    string h = hRow[c].ToLower().Replace("*", "").Trim();
                    if (h.Contains("mã nhà cung cấp") || h.Contains("mã ncc")) colMap["MaNcc"] = c;
                    else if (h.Contains("tên nhà cung cấp") || h.Contains("tên ncc") || h.Contains("tên đơn vị")) colMap["TenDonVi"] = c;
                    else if (h.Contains("nhóm")) colMap["TenNhom"] = c;
                    else if (h.Contains("điện thoại") || h.Contains("sđt") || h.Contains("phone")) colMap["DienThoai"] = c;
                    else if (h.Contains("email")) colMap["Email"] = c;
                    else if (h.Contains("địa chỉ")) colMap["DiaChi"] = c;
                    else if (h.Contains("khu vực")) colMap["KhuVuc"] = c;
                    else if (h.Contains("mã số thuế") || h.Contains("mst")) colMap["MaSoThue"] = c;
                    else if (h.Contains("ghi chú")) colMap["GhiChu"] = c;
                }
            }

            int startRow = headerIdx >= 0 ? headerIdx + 1 : 0;

            for (int r = startRow; r < rawRows.Count; r++)
            {
                var row = rawRows[r];
                if (row.Count == 0) continue;

                DongNhaCungCapFile item = new DongNhaCungCapFile();

                if (colMap.Count > 0)
                {
                    if (colMap.ContainsKey("MaNcc") && colMap["MaNcc"] < row.Count) item.MaNcc = row[colMap["MaNcc"]];
                    if (colMap.ContainsKey("TenDonVi") && colMap["TenDonVi"] < row.Count) item.TenDonVi = row[colMap["TenDonVi"]];
                    if (colMap.ContainsKey("TenNhom") && colMap["TenNhom"] < row.Count) item.TenNhom = row[colMap["TenNhom"]];
                    if (colMap.ContainsKey("DienThoai") && colMap["DienThoai"] < row.Count) item.DienThoai = row[colMap["DienThoai"]];
                    if (colMap.ContainsKey("Email") && colMap["Email"] < row.Count) item.Email = row[colMap["Email"]];
                    if (colMap.ContainsKey("DiaChi") && colMap["DiaChi"] < row.Count) item.DiaChi = row[colMap["DiaChi"]];
                    if (colMap.ContainsKey("KhuVuc") && colMap["KhuVuc"] < row.Count) item.KhuVuc = row[colMap["KhuVuc"]];
                    if (colMap.ContainsKey("MaSoThue") && colMap["MaSoThue"] < row.Count) item.MaSoThue = row[colMap["MaSoThue"]];
                    if (colMap.ContainsKey("GhiChu") && colMap["GhiChu"] < row.Count) item.GhiChu = row[colMap["GhiChu"]];
                }
                else
                {
                    // Map theo thứ tự cột mặc định
                    if (row.Count > 0) item.MaNcc = row[0];
                    if (row.Count > 1) item.TenDonVi = row[1];
                    if (row.Count > 2) item.TenNhom = row[2];
                    if (row.Count > 3) item.DienThoai = row[3];
                    if (row.Count > 4) item.Email = row[4];
                    if (row.Count > 5) item.DiaChi = row[5];
                    if (row.Count > 6) item.KhuVuc = row[6];
                    if (row.Count > 7) item.MaSoThue = row[7];
                    if (row.Count > 8) item.GhiChu = row[8];
                }

                // Giữ lại mọi dòng có dữ liệu (kể cả thiếu tên) để đếm và báo lỗi chính xác; bỏ qua dòng trống hoàn toàn
                bool coDuLieu = false;
                foreach (string o in row)
                {
                    if (!string.IsNullOrWhiteSpace(o)) { coDuLieu = true; break; }
                }
                if (coDuLieu)
                {
                    item.DongSo = r + 1;
                    ketQua.Add(item);
                }
            }

            return ketQua;
        }

        #endregion
    }
}
