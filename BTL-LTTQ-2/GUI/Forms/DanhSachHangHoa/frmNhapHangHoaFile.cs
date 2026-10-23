using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Data.SqlClient;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.IO.Compression;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using System.Xml.Linq;
using BTL_LTTQ_2.DAL;

namespace BTL_LTTQ_2.GUI.Forms.DanhSachHangHoa
{
    public partial class frmNhapHangHoaFile : Form
    {
        public int SoLuongThemMoi { get; private set; }
        public int SoLuongCapNhat { get; private set; }

        public frmNhapHangHoaFile()
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
                ofd.Title = "Chọn file dữ liệu hàng hóa để nhập";
                ofd.Filter = "File Excel (*.xlsx;*.xls)|*.xlsx;*.xls|File Excel OpenXML (*.xlsx)|*.xlsx|File Excel 97-2003 (*.xls)|*.xls|Tất cả các file (*.*)|*.*";
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
                sfd.Title = "Lưu file mẫu Excel";
                sfd.Filter = "File Excel (*.xls)|*.xls";
                sfd.FileName = "Mau_Nhap_Hang_Hoa.xls";

                if (sfd.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        StringBuilder html = new StringBuilder();
                        html.AppendLine("<html xmlns:o=\"urn:schemas-microsoft-com:office:office\" xmlns:x=\"urn:schemas-microsoft-com:office:excel\" xmlns=\"http://www.w3.org/TR/REC-html40\">");
                        html.AppendLine("<head>");
                        html.AppendLine("<meta http-equiv=\"Content-Type\" content=\"text/html; charset=utf-8\" />");
                        html.AppendLine("<!--[if gte mso 9]><xml><x:ExcelWorkbook><x:ExcelWorksheets><x:ExcelWorksheet><x:Name>MauHangHoa</x:Name><x:WorksheetOptions><x:DisplayGridlines/></x:WorksheetOptions></x:ExcelWorksheet></x:ExcelWorksheets></x:ExcelWorkbook></xml><![endif]-->");
                        html.AppendLine("<style>");
                        html.AppendLine("th { background-color: #0078D7; color: #FFFFFF; font-family: Segoe UI, Arial; font-size: 10pt; font-weight: bold; border: 0.5pt solid #CCCCCC; padding: 6px; }");
                        html.AppendLine("td { font-family: Segoe UI, Arial; font-size: 9.5pt; border: 0.5pt solid #E0E0E0; padding: 5px; }");
                        html.AppendLine(".text { mso-number-format: \"\\@\"; }");
                        html.AppendLine(".number { mso-number-format: \"#,##0\"; text-align: right; }");
                        html.AppendLine(".decimal { mso-number-format: \"#,##0.00\"; text-align: right; }");
                        html.AppendLine("</style>");
                        html.AppendLine("</head>");
                        html.AppendLine("<body>");
                        html.AppendLine("<table>");
                        html.AppendLine("<tr>");
                        html.AppendLine("<th>Mã hàng</th>");
                        html.AppendLine("<th>Tên hàng (*)</th>");
                        html.AppendLine("<th>Nhóm hàng</th>");
                        html.AppendLine("<th>Đơn vị tính</th>");
                        html.AppendLine("<th>Giá vốn</th>");
                        html.AppendLine("<th>Giá bán</th>");
                        html.AppendLine("<th>Trọng lượng</th>");
                        html.AppendLine("<th>Đơn vị TL</th>");
                        html.AppendLine("<th>Vị trí</th>");
                        html.AppendLine("<th>Quy cách</th>");
                        html.AppendLine("<th>Mô tả</th>");
                        html.AppendLine("</tr>");

                        html.AppendLine("<tr>");
                        html.AppendLine("<td class=\"text\">GAS-12KG</td>");
                        html.AppendLine("<td>Bình gas 12kg</td>");
                        html.AppendLine("<td>Bình gas</td>");
                        html.AppendLine("<td>Bình</td>");
                        html.AppendLine("<td class=\"number\">350000</td>");
                        html.AppendLine("<td class=\"number\">420000</td>");
                        html.AppendLine("<td class=\"decimal\">12</td>");
                        html.AppendLine("<td>kg</td>");
                        html.AppendLine("<td>Kệ A-01</td>");
                        html.AppendLine("<td>Van ren tiêu chuẩn</td>");
                        html.AppendLine("<td>Bình gas chính hãng Petrolimex</td>");
                        html.AppendLine("</tr>");

                        html.AppendLine("<tr>");
                        html.AppendLine("<td class=\"text\">BEP-RINNAI</td>");
                        html.AppendLine("<td>Bếp gas đơn Rinnai</td>");
                        html.AppendLine("<td>Phụ kiện gas</td>");
                        html.AppendLine("<td>Cái</td>");
                        html.AppendLine("<td class=\"number\">280000</td>");
                        html.AppendLine("<td class=\"number\">350000</td>");
                        html.AppendLine("<td class=\"decimal\">2.5</td>");
                        html.AppendLine("<td>kg</td>");
                        html.AppendLine("<td>Kệ B-02</td>");
                        html.AppendLine("<td>Đầu đốt đồng thau</td>");
                        html.AppendLine("<td>Bếp gas đơn chống dính</td>");
                        html.AppendLine("</tr>");

                        html.AppendLine("<tr>");
                        html.AppendLine("<td class=\"text\"></td>");
                        html.AppendLine("<td>Than sạch không khói</td>");
                        html.AppendLine("<td>Than</td>");
                        html.AppendLine("<td>Bao</td>");
                        html.AppendLine("<td class=\"number\">45000</td>");
                        html.AppendLine("<td class=\"number\">70000</td>");
                        html.AppendLine("<td class=\"decimal\">10</td>");
                        html.AppendLine("<td>kg</td>");
                        html.AppendLine("<td>Kệ C-01</td>");
                        html.AppendLine("<td>Đóng bao kín</td>");
                        html.AppendLine("<td>Mã hàng tự động sinh nếu để trống</td>");
                        html.AppendLine("</tr>");

                        html.AppendLine("</table>");
                        html.AppendLine("</body>");
                        html.AppendLine("</html>");

                        File.WriteAllText(sfd.FileName, html.ToString(), Encoding.UTF8);
                        MessageBox.Show("Đã tải file mẫu Excel thành công!\nĐường dẫn: " + sfd.FileName, "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Lỗi khi lưu file mẫu Excel: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        #endregion

        #region Thực Hiện Nhập Liệu

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
                List<DongHangHoaFile> dsDong = DocDuLieuTuFile(duongDan);
                if (dsDong == null || dsDong.Count == 0)
                {
                    MessageBox.Show("Không tìm thấy dữ liệu hàng hóa nào trong file!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                Application.DoEvents();

                XuLyLuuVaoCSDL(dsDong);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Có lỗi xảy ra trong quá trình xử lý: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
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

        internal void XuLyLuuVaoCSDL(List<DongHangHoaFile> dsDong, bool hienHopThoai = true)
        {
            // 1. Tải trước các danh mục hiện tại để tra cứu và tối ưu
            DataTable dtNhom = DataProcessing.ReadData("SELECT MaNhom, TenNhom FROM NhomHang");
            Dictionary<string, int> dictNhom = new Dictionary<string, int>(StringComparer.OrdinalIgnoreCase);
            if (dtNhom != null)
            {
                foreach (DataRow r in dtNhom.Rows)
                {
                    string ten = r["TenNhom"]?.ToString()?.Trim();
                    if (!string.IsNullOrEmpty(ten) && int.TryParse(r["MaNhom"]?.ToString(), out int id))
                    {
                        if (!dictNhom.ContainsKey(ten)) dictNhom.Add(ten, id);
                    }
                }
            }

            DataTable dtDvt = DataProcessing.ReadData("SELECT MaDVT, TenDVT FROM DonViTinh");
            Dictionary<string, int> dictDvt = new Dictionary<string, int>(StringComparer.OrdinalIgnoreCase);
            if (dtDvt != null)
            {
                foreach (DataRow r in dtDvt.Rows)
                {
                    string ten = r["TenDVT"]?.ToString()?.Trim();
                    if (!string.IsNullOrEmpty(ten) && int.TryParse(r["MaDVT"]?.ToString(), out int id))
                    {
                        if (!dictDvt.ContainsKey(ten)) dictDvt.Add(ten, id);
                    }
                }
            }

            DataTable dtViTri = DataProcessing.ReadData("SELECT TenViTri FROM ViTri WHERE TrangThai = 'HOAT_DONG'");
            HashSet<string> setViTri = new HashSet<string>(StringComparer.OrdinalIgnoreCase);
            if (dtViTri != null)
            {
                foreach (DataRow r in dtViTri.Rows)
                {
                    string ten = r["TenViTri"]?.ToString()?.Trim();
                    if (!string.IsNullOrEmpty(ten)) setViTri.Add(ten);
                }
            }

            // 2. Tải danh sách Hàng hóa hiện tại theo MaHangNoiBo
            DataTable dtHang = DataProcessing.ReadData("SELECT MaHang, MaHangNoiBo, TenHang, MaNhom, MaDVT, ViTri, GiaVon, GiaBanThamKhao, TrongLuong, DonViTrongLuong, QuyCach, MoTa FROM HangHoa");
            Dictionary<string, HangHoaHienTai> dictHang = new Dictionary<string, HangHoaHienTai>(StringComparer.OrdinalIgnoreCase);
            int maxMaHangNoiBoSo = 0;

            if (dtHang != null)
            {
                foreach (DataRow r in dtHang.Rows)
                {
                    string ma = r["MaHangNoiBo"]?.ToString()?.Trim();
                    if (!string.IsNullOrEmpty(ma))
                    {
                        HangHoaHienTai h = new HangHoaHienTai
                        {
                            MaHang = Convert.ToInt32(r["MaHang"]),
                            MaHangNoiBo = ma,
                            TenHang = r["TenHang"]?.ToString(),
                            MaNhom = Convert.ToInt32(r["MaNhom"]),
                            MaDVT = Convert.ToInt32(r["MaDVT"]),
                            ViTri = r["ViTri"] != DBNull.Value ? r["ViTri"]?.ToString() : null,
                            GiaVon = r["GiaVon"] != DBNull.Value ? Convert.ToDecimal(r["GiaVon"]) : 0,
                            GiaBan = r["GiaBanThamKhao"] != DBNull.Value ? Convert.ToDecimal(r["GiaBanThamKhao"]) : 0,
                            TrongLuong = r["TrongLuong"] != DBNull.Value ? Convert.ToDecimal(r["TrongLuong"]) : 0,
                            DonViTrongLuong = r["DonViTrongLuong"] != DBNull.Value ? r["DonViTrongLuong"]?.ToString() : "kg",
                            QuyCach = r["QuyCach"] != DBNull.Value ? r["QuyCach"]?.ToString() : null,
                            MoTa = r["MoTa"] != DBNull.Value ? r["MoTa"]?.ToString() : null
                        };

                        if (!dictHang.ContainsKey(ma)) dictHang.Add(ma, h);

                        // Tìm số thứ tự lớn nhất nếu mã có dạng HHxxxxxx
                        if (ma.StartsWith("HH", StringComparison.OrdinalIgnoreCase) && ma.Length > 2)
                        {
                            string phanSo = ma.Substring(2);
                            if (int.TryParse(phanSo, out int so) && so > maxMaHangNoiBoSo)
                            {
                                maxMaHangNoiBoSo = so;
                            }
                        }
                    }
                }
            }

            int soThemMoi = 0;
            int soCapNhat = 0;
            int soBoQua = 0;
            int soLoi = 0;
            List<string> dsChiTietLoi = new List<string>();
            bool ghiDeKhiTrung = rdoCapNhat.Checked;

            // 3. Thực hiện lưu vào CSDL trong một Transaction
            DataProcessing.ChangeDataTransaction((conn, trans) =>
            {
                // Chuẩn bị các SqlCommand cần thiết
                string sqlInsertNhom = "INSERT INTO NhomHang (TenNhom, TrangThai) VALUES (@TenNhom, 'HOAT_DONG'); SELECT SCOPE_IDENTITY();";
                string sqlInsertDvt = "INSERT INTO DonViTinh (TenDVT) VALUES (@TenDVT); SELECT SCOPE_IDENTITY();";
                string sqlInsertViTri = "INSERT INTO ViTri (TenViTri, TrangThai) VALUES (@TenViTri, 'HOAT_DONG');";

                string sqlInsertHang = @"
                    INSERT INTO HangHoa (
                        MaHangNoiBo, TenHang, MaNhom, MaDVT, GiaBanThamKhao, 
                        GiaVon, TrangThai, NgayTao, ViTri, TrongLuong, 
                        DonViTrongLuong, QuyCach, MoTa, DaDanhDau
                    )
                    VALUES (
                        @MaHangNoiBo, @TenHang, @MaNhom, @MaDVT, @GiaBan, 
                        @GiaVon, 'DANG_BAN', GETDATE(), @ViTri, @TrongLuong, 
                        @DonViTrongLuong, @QuyCach, @MoTa, 0
                    );
                    SELECT SCOPE_IDENTITY();";

                string sqlUpdateHang = @"
                    UPDATE HangHoa
                    SET TenHang = @TenHang,
                        MaNhom = @MaNhom,
                        MaDVT = @MaDVT,
                        GiaBanThamKhao = @GiaBan,
                        GiaVon = @GiaVon,
                        ViTri = @ViTri,
                        TrongLuong = @TrongLuong,
                        DonViTrongLuong = @DonViTrongLuong,
                        QuyCach = @QuyCach,
                        MoTa = @MoTa
                    WHERE MaHang = @MaHang;";

                using (SqlCommand cmdInsertNhom = new SqlCommand(sqlInsertNhom, conn, trans))
                using (SqlCommand cmdInsertDvt = new SqlCommand(sqlInsertDvt, conn, trans))
                using (SqlCommand cmdInsertViTri = new SqlCommand(sqlInsertViTri, conn, trans))
                using (SqlCommand cmdInsertHang = new SqlCommand(sqlInsertHang, conn, trans))
                using (SqlCommand cmdUpdateHang = new SqlCommand(sqlUpdateHang, conn, trans))
                {
                    cmdInsertNhom.Parameters.Add("@TenNhom", SqlDbType.NVarChar, 100);
                    cmdInsertDvt.Parameters.Add("@TenDVT", SqlDbType.NVarChar, 50);
                    cmdInsertViTri.Parameters.Add("@TenViTri", SqlDbType.NVarChar, 100);

                    // Tham số Insert Hàng
                    cmdInsertHang.Parameters.Add("@MaHangNoiBo", SqlDbType.VarChar, 50);
                    cmdInsertHang.Parameters.Add("@TenHang", SqlDbType.NVarChar, 255);
                    cmdInsertHang.Parameters.Add("@MaNhom", SqlDbType.Int);
                    cmdInsertHang.Parameters.Add("@MaDVT", SqlDbType.Int);
                    cmdInsertHang.Parameters.Add("@GiaBan", SqlDbType.Decimal);
                    cmdInsertHang.Parameters.Add("@GiaVon", SqlDbType.Decimal);
                    cmdInsertHang.Parameters.Add("@ViTri", SqlDbType.NVarChar, 100);
                    cmdInsertHang.Parameters.Add("@TrongLuong", SqlDbType.Decimal);
                    cmdInsertHang.Parameters.Add("@DonViTrongLuong", SqlDbType.VarChar, 20);
                    cmdInsertHang.Parameters.Add("@QuyCach", SqlDbType.NVarChar, 255);
                    cmdInsertHang.Parameters.Add("@MoTa", SqlDbType.NVarChar);

                    // Tham số Update Hàng
                    cmdUpdateHang.Parameters.Add("@TenHang", SqlDbType.NVarChar, 255);
                    cmdUpdateHang.Parameters.Add("@MaNhom", SqlDbType.Int);
                    cmdUpdateHang.Parameters.Add("@MaDVT", SqlDbType.Int);
                    cmdUpdateHang.Parameters.Add("@GiaBan", SqlDbType.Decimal);
                    cmdUpdateHang.Parameters.Add("@GiaVon", SqlDbType.Decimal);
                    cmdUpdateHang.Parameters.Add("@ViTri", SqlDbType.NVarChar, 100);
                    cmdUpdateHang.Parameters.Add("@TrongLuong", SqlDbType.Decimal);
                    cmdUpdateHang.Parameters.Add("@DonViTrongLuong", SqlDbType.VarChar, 20);
                    cmdUpdateHang.Parameters.Add("@QuyCach", SqlDbType.NVarChar, 255);
                    cmdUpdateHang.Parameters.Add("@MoTa", SqlDbType.NVarChar);
                    cmdUpdateHang.Parameters.Add("@MaHang", SqlDbType.Int);

                    foreach (DongHangHoaFile d in dsDong)
                    {
                        // 1. Kiểm tra trường bắt buộc Tên hàng
                        string tenHang = d.TenHang?.Trim();
                        if (string.IsNullOrEmpty(tenHang))
                        {
                            soLoi++;
                            if (dsChiTietLoi.Count < 5)
                            {
                                dsChiTietLoi.Add($"Dòng {d.SoThuTuDong}: Bỏ trống Tên hàng hóa.");
                            }
                            continue;
                        }

                        // 2. Xác định mã hàng
                        string maHang = d.MaHang?.Trim();
                        bool laMaTuSinh = false;

                        if (string.IsNullOrEmpty(maHang))
                        {
                            maxMaHangNoiBoSo++;
                            maHang = $"HH{maxMaHangNoiBoSo.ToString("D6")}";
                            laMaTuSinh = true;
                        }

                        // 3. Phân nhánh Cập nhật hay Thêm mới
                        if (!laMaTuSinh && dictHang.ContainsKey(maHang))
                        {
                            // Đã tồn tại mã hàng này trong CSDL
                            if (!ghiDeKhiTrung)
                            {
                                soBoQua++;
                                continue;
                            }

                            HangHoaHienTai old = dictHang[maHang];

                            // Giải quyết nhóm hàng khi cập nhật (nếu file để trống -> giữ nguyên nhóm cũ)
                            int maNhomFinal = old.MaNhom;
                            string tenNhom = d.NhomHang?.Trim();
                            if (!string.IsNullOrEmpty(tenNhom))
                            {
                                if (dictNhom.ContainsKey(tenNhom))
                                {
                                    maNhomFinal = dictNhom[tenNhom];
                                }
                                else
                                {
                                    cmdInsertNhom.Parameters["@TenNhom"].Value = tenNhom;
                                    int newNhomId = Convert.ToInt32(cmdInsertNhom.ExecuteScalar());
                                    dictNhom[tenNhom] = newNhomId;
                                    maNhomFinal = newNhomId;
                                }
                            }

                            // Giải quyết ĐVT khi cập nhật (nếu file để trống -> giữ nguyên ĐVT cũ)
                            int maDvtFinal = old.MaDVT;
                            string tenDvt = d.DonViTinh?.Trim();
                            if (!string.IsNullOrEmpty(tenDvt))
                            {
                                if (dictDvt.ContainsKey(tenDvt))
                                {
                                    maDvtFinal = dictDvt[tenDvt];
                                }
                                else
                                {
                                    cmdInsertDvt.Parameters["@TenDVT"].Value = tenDvt;
                                    int newDvtId = Convert.ToInt32(cmdInsertDvt.ExecuteScalar());
                                    dictDvt[tenDvt] = newDvtId;
                                    maDvtFinal = newDvtId;
                                }
                            }

                            // Giải quyết Vị trí khi cập nhật (nếu file để trống -> giữ nguyên vị trí cũ)
                            string viTriFinal = old.ViTri;
                            string tenViTri = d.ViTri?.Trim();
                            if (!string.IsNullOrEmpty(tenViTri))
                            {
                                if (!setViTri.Contains(tenViTri))
                                {
                                    cmdInsertViTri.Parameters["@TenViTri"].Value = tenViTri;
                                    cmdInsertViTri.ExecuteNonQuery();
                                    setViTri.Add(tenViTri);
                                }
                                viTriFinal = tenViTri;
                            }

                            // Giải quyết Giá vốn, Giá bán, Trọng lượng (nếu file để trống -> giữ nguyên giá trị cũ)
                            decimal giaVonFinal = ChuyenDoiSoThapPhan(d.GiaVonRaw, old.GiaVon);
                            decimal giaBanFinal = ChuyenDoiSoThapPhan(d.GiaBanRaw, old.GiaBan);
                            decimal trongLuongFinal = ChuyenDoiSoThapPhan(d.TrongLuongRaw, old.TrongLuong);

                            string dvTrongLuongFinal = !string.IsNullOrWhiteSpace(d.DonViTrongLuong)
                                ? d.DonViTrongLuong.Trim()
                                : old.DonViTrongLuong;

                            string quyCachFinal = !string.IsNullOrWhiteSpace(d.QuyCach) ? d.QuyCach.Trim() : old.QuyCach;
                            string moTaFinal = !string.IsNullOrWhiteSpace(d.MoTa) ? d.MoTa.Trim() : old.MoTa;

                            // Thực hiện UPDATE
                            cmdUpdateHang.Parameters["@TenHang"].Value = tenHang;
                            cmdUpdateHang.Parameters["@MaNhom"].Value = maNhomFinal;
                            cmdUpdateHang.Parameters["@MaDVT"].Value = maDvtFinal;
                            cmdUpdateHang.Parameters["@GiaBan"].Value = giaBanFinal;
                            cmdUpdateHang.Parameters["@GiaVon"].Value = giaVonFinal;
                            cmdUpdateHang.Parameters["@ViTri"].Value = (object)viTriFinal ?? DBNull.Value;
                            cmdUpdateHang.Parameters["@TrongLuong"].Value = trongLuongFinal;
                            cmdUpdateHang.Parameters["@DonViTrongLuong"].Value = dvTrongLuongFinal ?? "kg";
                            cmdUpdateHang.Parameters["@QuyCach"].Value = (object)quyCachFinal ?? DBNull.Value;
                            cmdUpdateHang.Parameters["@MoTa"].Value = (object)moTaFinal ?? DBNull.Value;
                            cmdUpdateHang.Parameters["@MaHang"].Value = old.MaHang;

                            cmdUpdateHang.ExecuteNonQuery();
                            soCapNhat++;
                        }
                        else
                        {
                            // THÊM MỚI HÀNG HÓA

                            // Giải quyết nhóm hàng (nếu để trống -> tự gán vào nhóm "Khác")
                            int maNhomFinal;
                            string tenNhom = d.NhomHang?.Trim();
                            if (string.IsNullOrEmpty(tenNhom)) tenNhom = "Khác";

                            if (dictNhom.ContainsKey(tenNhom))
                            {
                                maNhomFinal = dictNhom[tenNhom];
                            }
                            else
                            {
                                cmdInsertNhom.Parameters["@TenNhom"].Value = tenNhom;
                                int newNhomId = Convert.ToInt32(cmdInsertNhom.ExecuteScalar());
                                dictNhom[tenNhom] = newNhomId;
                                maNhomFinal = newNhomId;
                            }

                            // Giải quyết ĐVT (nếu để trống -> tự gán "Cái")
                            int maDvtFinal;
                            string tenDvt = d.DonViTinh?.Trim();
                            if (string.IsNullOrEmpty(tenDvt)) tenDvt = "Cái";

                            if (dictDvt.ContainsKey(tenDvt))
                            {
                                maDvtFinal = dictDvt[tenDvt];
                            }
                            else
                            {
                                cmdInsertDvt.Parameters["@TenDVT"].Value = tenDvt;
                                int newDvtId = Convert.ToInt32(cmdInsertDvt.ExecuteScalar());
                                dictDvt[tenDvt] = newDvtId;
                                maDvtFinal = newDvtId;
                            }

                            // Giải quyết Vị trí (nếu để trống -> NULL)
                            string viTriFinal = null;
                            string tenViTri = d.ViTri?.Trim();
                            if (!string.IsNullOrEmpty(tenViTri))
                            {
                                if (!setViTri.Contains(tenViTri))
                                {
                                    cmdInsertViTri.Parameters["@TenViTri"].Value = tenViTri;
                                    cmdInsertViTri.ExecuteNonQuery();
                                    setViTri.Add(tenViTri);
                                }
                                viTriFinal = tenViTri;
                            }

                            decimal giaVonFinal = ChuyenDoiSoThapPhan(d.GiaVonRaw, 0);
                            decimal giaBanFinal = ChuyenDoiSoThapPhan(d.GiaBanRaw, 0);
                            decimal trongLuongFinal = ChuyenDoiSoThapPhan(d.TrongLuongRaw, 0);
                            string dvTrongLuongFinal = !string.IsNullOrWhiteSpace(d.DonViTrongLuong) ? d.DonViTrongLuong.Trim() : "kg";
                            string quyCachFinal = !string.IsNullOrWhiteSpace(d.QuyCach) ? d.QuyCach.Trim() : null;
                            string moTaFinal = !string.IsNullOrWhiteSpace(d.MoTa) ? d.MoTa.Trim() : null;

                            // Thực hiện INSERT
                            cmdInsertHang.Parameters["@MaHangNoiBo"].Value = maHang;
                            cmdInsertHang.Parameters["@TenHang"].Value = tenHang;
                            cmdInsertHang.Parameters["@MaNhom"].Value = maNhomFinal;
                            cmdInsertHang.Parameters["@MaDVT"].Value = maDvtFinal;
                            cmdInsertHang.Parameters["@GiaBan"].Value = giaBanFinal;
                            cmdInsertHang.Parameters["@GiaVon"].Value = giaVonFinal;
                            cmdInsertHang.Parameters["@ViTri"].Value = (object)viTriFinal ?? DBNull.Value;
                            cmdInsertHang.Parameters["@TrongLuong"].Value = trongLuongFinal;
                            cmdInsertHang.Parameters["@DonViTrongLuong"].Value = dvTrongLuongFinal;
                            cmdInsertHang.Parameters["@QuyCach"].Value = (object)quyCachFinal ?? DBNull.Value;
                            cmdInsertHang.Parameters["@MoTa"].Value = (object)moTaFinal ?? DBNull.Value;

                            object newId = cmdInsertHang.ExecuteScalar();
                            int newMaHang = Convert.ToInt32(newId);

                            // Lưu vào dictionary bộ nhớ để chống trùng lặp dòng tiếp theo trong cùng 1 file
                            dictHang[maHang] = new HangHoaHienTai
                            {
                                MaHang = newMaHang,
                                MaHangNoiBo = maHang,
                                TenHang = tenHang,
                                MaNhom = maNhomFinal,
                                MaDVT = maDvtFinal,
                                ViTri = viTriFinal,
                                GiaVon = giaVonFinal,
                                GiaBan = giaBanFinal,
                                TrongLuong = trongLuongFinal,
                                DonViTrongLuong = dvTrongLuongFinal,
                                QuyCach = quyCachFinal,
                                MoTa = moTaFinal
                            };

                            soThemMoi++;
                        }
                    }
                }
            });

            this.SoLuongThemMoi = soThemMoi;
            this.SoLuongCapNhat = soCapNhat;

            // 4. Báo cáo kết quả
            StringBuilder msg = new StringBuilder();
            msg.AppendLine("Nhập dữ liệu thành công!");
            msg.AppendLine($"• Tổng số dòng trong file: {dsDong.Count}");
            msg.AppendLine($"• Thêm mới thành công: {soThemMoi} hàng hóa");
            msg.AppendLine($"• Cập nhật thành công: {soCapNhat} hàng hóa");
            if (soBoQua > 0) msg.AppendLine($"• Bỏ qua: {soBoQua} dòng (đã tồn tại mã hàng)");
            if (soLoi > 0)
            {
                msg.AppendLine($"• Bỏ qua lỗi: {soLoi} dòng (thiếu tên hàng hóa bắt buộc)");
                if (dsChiTietLoi.Count > 0)
                {
                    msg.AppendLine("\nChi tiết lỗi:");
                    foreach (string err in dsChiTietLoi) msg.AppendLine(" - " + err);
                }
            }

            if (hienHopThoai)
            {
                MessageBox.Show(msg.ToString(), "Kết quả nhập file", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

            this.DialogResult = DialogResult.OK;
            if (hienHopThoai)
            {
                this.Close();
            }
        }

        private decimal ChuyenDoiSoThapPhan(string raw, decimal giaTriMacDinh)
        {
            if (string.IsNullOrWhiteSpace(raw)) return giaTriMacDinh;
            string clean = raw.Trim().Replace(" ", "");

            // Xử lý dấu phẩy hoặc chấm
            if (clean.Contains(",") && clean.Contains("."))
            {
                // Dạng 1,234.56 hoặc 1.234,56
                if (clean.LastIndexOf(',') > clean.LastIndexOf('.'))
                {
                    clean = clean.Replace(".", "").Replace(",", ".");
                }
                else
                {
                    clean = clean.Replace(",", "");
                }
            }
            else if (clean.Contains(","))
            {
                clean = clean.Replace(",", ".");
            }

            if (decimal.TryParse(clean, NumberStyles.Any, CultureInfo.InvariantCulture, out decimal val))
            {
                return val;
            }
            return giaTriMacDinh;
        }

        #endregion

        #region Bộ Đọc Dữ Liệu Đa Định Dạng (XLSX, XLS, CSV, TXT)

        private List<DongHangHoaFile> DocDuLieuTuFile(string filePath)
        {
            string ext = Path.GetExtension(filePath).ToLower();

            if (ext == ".xlsx")
            {
                return DocFileOpenXmlXlsx(filePath);
            }
            else if (ext == ".xls")
            {
                // Thử đọc theo định dạng HTML Table (file hệ thống xuất ra)
                try
                {
                    string preview = File.ReadAllText(filePath, Encoding.UTF8);
                    if (preview.IndexOf("<table", StringComparison.OrdinalIgnoreCase) >= 0)
                    {
                        return DocFileHtmlTable(preview);
                    }
                }
                catch { }

                // Fallback OleDb hoặc Plain text
                return DocFileOleDb(filePath, ext);
            }
            else
            {
                // Đọc CSV hoặc TXT
                return DocFileCsv(filePath);
            }
        }

        private List<DongHangHoaFile> DocFileOpenXmlXlsx(string filePath)
        {
            List<DongHangHoaFile> ketQua = new List<DongHangHoaFile>();

            using (FileStream fs = new FileStream(filePath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
            using (ZipArchive archive = new ZipArchive(fs, ZipArchiveMode.Read))
            {
                // 1. Đọc SharedStrings
                List<string> sharedStrings = new List<string>();
                ZipArchiveEntry entryStrings = archive.GetEntry("xl/sharedStrings.xml");
                if (entryStrings != null)
                {
                    using (Stream stream = entryStrings.Open())
                    {
                        XDocument docStrings = XDocument.Load(stream);
                        XNamespace ns = docStrings.Root.Name.Namespace;

                        foreach (XElement si in docStrings.Root.Elements(ns + "si"))
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

                // 2. Đọc Sheet1
                ZipArchiveEntry entrySheet = archive.GetEntry("xl/worksheets/sheet1.xml");
                if (entrySheet == null)
                {
                    // Lấy sheet đầu tiên bất kỳ
                    foreach (ZipArchiveEntry e in archive.Entries)
                    {
                        if (e.FullName.StartsWith("xl/worksheets/sheet", StringComparison.OrdinalIgnoreCase))
                        {
                            entrySheet = e;
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
                    XDocument docSheet = XDocument.Load(stream);
                    XNamespace ns = docSheet.Root.Name.Namespace;

                    XElement sheetData = docSheet.Root.Element(ns + "sheetData");
                    if (sheetData != null)
                    {
                        foreach (XElement row in sheetData.Elements(ns + "row"))
                        {
                            List<string> rowCells = new List<string>();
                            int currentCol = 0;

                            foreach (XElement c in row.Elements(ns + "c"))
                            {
                                string cellRef = c.Attribute("r")?.Value;
                                int colIdx = LayChiSoCotTuCellRef(cellRef);

                                while (currentCol < colIdx)
                                {
                                    rowCells.Add(string.Empty);
                                    currentCol++;
                                }

                                string type = c.Attribute("t")?.Value;
                                string val = c.Element(ns + "v")?.Value;

                                if (type == "s" && int.TryParse(val, out int strIdx) && strIdx >= 0 && strIdx < sharedStrings.Count)
                                {
                                    rowCells.Add(sharedStrings[strIdx]);
                                }
                                else if (type == "inlineStr")
                                {
                                    string inlineText = c.Element(ns + "is")?.Element(ns + "t")?.Value ?? string.Empty;
                                    rowCells.Add(inlineText);
                                }
                                else
                                {
                                    rowCells.Add(val ?? string.Empty);
                                }
                                currentCol++;
                            }
                            if (rowCells.Count > 0)
                            {
                                rawRows.Add(rowCells);
                            }
                        }
                    }
                }

                return ChuyenDoiRawRowsThanhDongHangHoa(rawRows);
            }
        }

        private int LayChiSoCotTuCellRef(string cellRef)
        {
            if (string.IsNullOrEmpty(cellRef)) return 0;
            string colLetters = Regex.Replace(cellRef, @"[\d]", "").ToUpper();
            int index = 0;
            foreach (char ch in colLetters)
            {
                index = index * 26 + (ch - 'A' + 1);
            }
            return Math.Max(0, index - 1);
        }

        private List<DongHangHoaFile> DocFileHtmlTable(string htmlContent)
        {
            List<List<string>> rawRows = new List<List<string>>();
            var trMatches = Regex.Matches(htmlContent, @"<tr[^>]*>(.*?)</tr>", RegexOptions.Singleline | RegexOptions.IgnoreCase);

            foreach (Match tr in trMatches)
            {
                var cellMatches = Regex.Matches(tr.Groups[1].Value, @"<(td|th)[^>]*>(.*?)</(td|th)>", RegexOptions.Singleline | RegexOptions.IgnoreCase);
                if (cellMatches.Count > 0)
                {
                    List<string> row = new List<string>();
                    foreach (Match cell in cellMatches)
                    {
                        string val = cell.Groups[2].Value;
                        val = Regex.Replace(val, "<.*?>", "");
                        val = System.Net.WebUtility.HtmlDecode(val).Trim();
                        row.Add(val);
                    }
                    rawRows.Add(row);
                }
            }

            return ChuyenDoiRawRowsThanhDongHangHoa(rawRows);
        }

        private List<DongHangHoaFile> DocFileCsv(string filePath)
        {
            List<List<string>> rawRows = new List<List<string>>();
            string[] lines = File.ReadAllLines(filePath, Encoding.UTF8);

            char delimiter = ',';
            if (lines.Length > 0)
            {
                string first = lines[0];
                if (first.Contains("\t")) delimiter = '\t';
                else if (first.Contains(";") && !first.Contains(",")) delimiter = ';';
            }

            foreach (string line in lines)
            {
                if (string.IsNullOrWhiteSpace(line)) continue;
                if (line.StartsWith("sep=", StringComparison.OrdinalIgnoreCase)) continue;

                List<string> fields = TachDongCsv(line, delimiter);
                rawRows.Add(fields);
            }

            return ChuyenDoiRawRowsThanhDongHangHoa(rawRows);
        }

        private List<string> TachDongCsv(string line, char delimiter)
        {
            List<string> result = new List<string>();
            StringBuilder cur = new StringBuilder();
            bool inQuotes = false;

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
                else if (c == delimiter && !inQuotes)
                {
                    result.Add(cur.ToString().Trim());
                    cur.Clear();
                }
                else
                {
                    cur.Append(c);
                }
            }
            result.Add(cur.ToString().Trim());
            return result;
        }

        private List<DongHangHoaFile> DocFileOleDb(string filePath, string ext)
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
                                    row.Add(reader.IsDBNull(i) ? string.Empty : reader.GetValue(i)?.ToString()?.Trim());
                                }
                                rawRows.Add(row);
                            }
                        }
                    }
                }
            }
            catch
            {
                // Fallback nếu không có OLEDB driver: đọc file theo text UTF-8
                string[] lines = File.ReadAllLines(filePath, Encoding.UTF8);
                foreach (string l in lines)
                {
                    if (!string.IsNullOrWhiteSpace(l))
                    {
                        rawRows.Add(TachDongCsv(l, ','));
                    }
                }
            }

            return ChuyenDoiRawRowsThanhDongHangHoa(rawRows);
        }

        private List<DongHangHoaFile> ChuyenDoiRawRowsThanhDongHangHoa(List<List<string>> rawRows)
        {
            List<DongHangHoaFile> ketQua = new List<DongHangHoaFile>();
            if (rawRows == null || rawRows.Count == 0) return ketQua;

            // 1. Tìm dòng tiêu đề
            int headerRowIdx = -1;
            int idxMaHang = -1;
            int idxTenHang = -1;
            int idxNhomHang = -1;
            int idxDonViTinh = -1;
            int idxGiaVon = -1;
            int idxGiaBan = -1;
            int idxTrongLuong = -1;
            int idxDonViTL = -1;
            int idxViTri = -1;
            int idxQuyCach = -1;
            int idxMoTa = -1;

            for (int r = 0; r < Math.Min(rawRows.Count, 10); r++)
            {
                List<string> row = rawRows[r];
                for (int c = 0; c < row.Count; c++)
                {
                    string h = ChuanHoaTieuDe(row[c]);
                    if (h.Contains("tenhang") || h.Contains("tensanpham") || h.Contains("ten") || h.Contains("hanghoa"))
                    {
                        headerRowIdx = r;
                        break;
                    }
                }
                if (headerRowIdx >= 0) break;
            }

            if (headerRowIdx < 0)
            {
                // Không tìm thấy tiêu đề rõ ràng, lấy dòng đầu tiên làm tiêu đề
                headerRowIdx = 0;
            }

            // Map các cột từ dòng tiêu đề
            List<string> header = rawRows[headerRowIdx];
            for (int c = 0; c < header.Count; c++)
            {
                string h = ChuanHoaTieuDe(header[c]);

                if (idxTenHang < 0 && (h.Contains("tenhang") || h.Contains("tensanpham") || h.Contains("tensp") || h == "ten" || h.Contains("hanghoa")))
                    idxTenHang = c;
                else if (idxMaHang < 0 && (h.Contains("mahang") || h.Contains("masp") || h.Contains("ma_hang") || h.Contains("mahangnoibo") || h == "ma"))
                    idxMaHang = c;
                else if (idxNhomHang < 0 && (h.Contains("nhomhang") || h.Contains("nhom") || h.Contains("loaihang") || h.Contains("tennhom")))
                    idxNhomHang = c;
                else if (idxDonViTinh < 0 && (h.Contains("donvitinh") || h.Contains("dvt") || h.Contains("donvi")))
                    idxDonViTinh = c;
                else if (idxGiaVon < 0 && (h.Contains("giavon") || h.Contains("gianhap")))
                    idxGiaVon = c;
                else if (idxGiaBan < 0 && (h.Contains("giaban") || h.Contains("giabanthamkhao") || h.Contains("dongia")))
                    idxGiaBan = c;
                else if (idxDonViTL < 0 && (h.Contains("donvitl") || h.Contains("donvitrongluong") || h.Contains("dvtl") || h == "donvitl"))
                    idxDonViTL = c;
                else if (idxTrongLuong < 0 && (h.Contains("trongluong") || h.Contains("khoiluong") || h == "tl"))
                    idxTrongLuong = c;
                else if (idxViTri < 0 && (h.Contains("vitri") || h.Contains("ke") || h.Contains("khuvuc")))
                    idxViTri = c;
                else if (idxQuyCach < 0 && (h.Contains("quycach") || h.Contains("thongso")))
                    idxQuyCach = c;
                else if (idxMoTa < 0 && (h.Contains("mota") || h.Contains("ghichu")))
                    idxMoTa = c;
            }

            // Nếu không map được Tên hàng nhưng có cột thứ 2 (hoặc cột 1)
            if (idxTenHang < 0)
            {
                idxTenHang = header.Count > 1 ? 1 : 0;
            }

            // 2. Đọc các dòng dữ liệu
            for (int r = headerRowIdx + 1; r < rawRows.Count; r++)
            {
                List<string> row = rawRows[r];
                if (row.Count == 0) continue;

                // Kiểm tra xem dòng có hoàn toàn rỗng không
                bool toanBoRong = true;
                foreach (string s in row)
                {
                    if (!string.IsNullOrWhiteSpace(s)) { toanBoRong = false; break; }
                }
                if (toanBoRong) continue;

                DongHangHoaFile d = new DongHangHoaFile();
                d.SoThuTuDong = r + 1;
                d.MaHang = LayGiaTriCot(row, idxMaHang);
                d.TenHang = LayGiaTriCot(row, idxTenHang);
                d.NhomHang = LayGiaTriCot(row, idxNhomHang);
                d.DonViTinh = LayGiaTriCot(row, idxDonViTinh);
                d.GiaVonRaw = LayGiaTriCot(row, idxGiaVon);
                d.GiaBanRaw = LayGiaTriCot(row, idxGiaBan);
                d.TrongLuongRaw = LayGiaTriCot(row, idxTrongLuong);
                d.DonViTrongLuong = LayGiaTriCot(row, idxDonViTL);
                d.ViTri = LayGiaTriCot(row, idxViTri);
                d.QuyCach = LayGiaTriCot(row, idxQuyCach);
                d.MoTa = LayGiaTriCot(row, idxMoTa);

                ketQua.Add(d);
            }

            return ketQua;
        }

        private string LayGiaTriCot(List<string> row, int colIdx)
        {
            if (colIdx >= 0 && colIdx < row.Count)
            {
                return row[colIdx]?.Trim();
            }
            return null;
        }

        private string ChuanHoaTieuDe(string text)
        {
            if (string.IsNullOrWhiteSpace(text)) return string.Empty;
            string s = text.Trim();

            // 1. Chuan hoa phan ra ky tu FormD va bo dau
            string normalized = s.Normalize(NormalizationForm.FormD);
            StringBuilder sb = new StringBuilder();
            foreach (char c in normalized)
            {
                UnicodeCategory uc = CharUnicodeInfo.GetUnicodeCategory(c);
                if (uc != UnicodeCategory.NonSpacingMark)
                {
                    sb.Append(c);
                }
            }

            s = sb.ToString().Normalize(NormalizationForm.FormC).ToLower();
            s = s.Replace("đ", "d");

            // 2. Loai bo toan bo ky tu dac biet, khoang trang, dau ngoac
            s = Regex.Replace(s, @"[^\w]", "");
            return s;
        }

        #endregion

        #region Lớp DTO Phụ Trợ

        internal class DongHangHoaFile
        {
            public int SoThuTuDong { get; set; }
            public string MaHang { get; set; }
            public string TenHang { get; set; }
            public string NhomHang { get; set; }
            public string DonViTinh { get; set; }
            public string GiaVonRaw { get; set; }
            public string GiaBanRaw { get; set; }
            public string TrongLuongRaw { get; set; }
            public string DonViTrongLuong { get; set; }
            public string ViTri { get; set; }
            public string QuyCach { get; set; }
            public string MoTa { get; set; }
        }

        internal class HangHoaHienTai
        {
            public int MaHang { get; set; }
            public string MaHangNoiBo { get; set; }
            public string TenHang { get; set; }
            public int MaNhom { get; set; }
            public int MaDVT { get; set; }
            public string ViTri { get; set; }
            public decimal GiaVon { get; set; }
            public decimal GiaBan { get; set; }
            public decimal TrongLuong { get; set; }
            public string DonViTrongLuong { get; set; }
            public string QuyCach { get; set; }
            public string MoTa { get; set; }
        }

        #endregion
    }
}
