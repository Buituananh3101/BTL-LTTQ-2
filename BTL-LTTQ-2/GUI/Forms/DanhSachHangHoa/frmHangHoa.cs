using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using BTL_LTTQ_2.Classes;

namespace BTL_LTTQ_2.GUI.Forms.DanhSachHangHoa
{
    public partial class frmHangHoa : Form
    {
        private int maHangHienTai = 0; // 0: Thêm mới, > 0: Chỉnh sửa
        private byte[] duLieuAnhHienTai = null;
        private string trangThaiHienTai = "DANG_BAN";
        private bool daChonAnhMoi = false;

        private readonly Image[] danhSachAnh = new Image[5];
        private readonly byte[][] danhSachByteAnh = new byte[5][];
        private PictureBox[] danhSachPic;
        private readonly ToolTip toolTipAnh = new ToolTip();

        public frmHangHoa()
        {
            InitializeComponent();
            maHangHienTai = 0;
            BatChongChopGiat();
        }

        public frmHangHoa(int maHang)
        {
            InitializeComponent();
            maHangHienTai = maHang;
            BatChongChopGiat();
        }

        private void BatChongChopGiat()
        {
            this.DoubleBuffered = true;
            this.SetStyle(ControlStyles.OptimizedDoubleBuffer | ControlStyles.AllPaintingInWmPaint | ControlStyles.UserPaint, true);
            this.UpdateStyles();
        }

        private void frmHangHoa_Load(object sender, EventArgs e)
        {
            KhoiTao5KhungAnh();

            if (btnXoa != null)
            {
                btnXoa.Cursor = Cursors.Hand;
                toolTipAnh.SetToolTip(btnXoa, "Xóa hàng hóa này khỏi hệ thống");
            }
            NapCombobox();

            // Khóa ô tồn kho (cả thêm mới và sửa) vì tồn kho tính động qua chứng từ kho
            txtTonKho.ReadOnly = true;
            txtTonKho.BackColor = Color.FromArgb(243, 244, 246);
            string goiYTonKho = SessionContext.MaKhoHienTai > 0
                ? "Tồn kho tại kho đang làm việc, chỉ thay đổi qua chứng từ Nhập hàng / Kiểm kho."
                : "Tổng tồn kho của tất cả các kho, chỉ thay đổi qua chứng từ Nhập hàng / Kiểm kho.";
            toolTipAnh.SetToolTip(txtTonKho, goiYTonKho);
            toolTipAnh.SetToolTip(lblTonKho, goiYTonKho);

            // Chế độ "Tất cả kho": định mức tồn đặt theo từng kho nên khóa 2 ô định mức
            if (SessionContext.MaKhoHienTai <= 0)
            {
                const string goiYDinhMuc = "Chọn một kho cụ thể (Đổi kho) để đặt định mức";
                foreach (TextBox txt in new[] { txtDinhMucTonThapNhat, txtDinhMucTonCaoNhat })
                {
                    txt.ReadOnly = true;
                    txt.TabStop = false;
                    txt.BackColor = Color.FromArgb(243, 244, 246);
                    toolTipAnh.SetToolTip(txt, goiYDinhMuc);
                }
                toolTipAnh.SetToolTip(lblDinhMucTonThapNhat, goiYDinhMuc);
                toolTipAnh.SetToolTip(lblDinhMucTonCaoNhat, goiYDinhMuc);
            }

            if (maHangHienTai > 0)
            {
                lblTaoHangHoa.Text = "Cập nhật hàng hóa";
                this.Text = "Cập nhật thông tin hàng hóa";
                btnLuu.Text = "Sửa";

                NapThongTinHangHoa();

            }
            else
            {
                lblTaoHangHoa.Text = "Tạo hàng hóa";
                this.Text = "Thêm hàng hóa mới";
                btnLuu.Text = "Lưu";

                txtTonKho.Text = "0";
                txtGiaVon.Text = "0";
                txtGiaBan.Text = "0";
                txtTrongLuong.Text = "0";
                txtDinhMucTonThapNhat.Text = SessionContext.MaKhoHienTai > 0 ? "0" : "";
                txtDinhMucTonCaoNhat.Text = SessionContext.MaKhoHienTai > 0 ? "0" : "";

                // Để trống các combobox khi tạo mới để người dùng chủ động chọn, tránh lưu nhầm mặc định
                cboNhomHang.SelectedIndex = -1;
                cboDonVi.SelectedIndex = -1;
                cboViTri.SelectedIndex = -1;
                cboViTri.Text = "";

                SinhMaHangGoiY();
                CapNhatGiaoDienNutXoa();

            }
        }

        #region Nạp dữ liệu

        private void NapCombobox()
        {
            // 1. Nhóm hàng
            try
            {
                DataTable dtNhom = DataProcessing.ReadData("SELECT MaNhom, TenNhom FROM NhomHang WHERE TrangThai = 'HOAT_DONG' ORDER BY TenNhom");
                ComboboxHelper.CaiDatTimKiem(cboNhomHang, dtNhom, "TenNhom", "MaNhom");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi tải danh mục nhóm hàng: " + ex.Message, "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

            // 2. Đơn vị tính
            try
            {
                DataTable dtDVT = DataProcessing.ReadData("SELECT MaDVT, TenDVT FROM DonViTinh ORDER BY TenDVT");
                ComboboxHelper.CaiDatTimKiem(cboDonVi, dtDVT, "TenDVT", "MaDVT");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi tải danh mục đơn vị tính: " + ex.Message, "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

            // 3. Vị trí
            try
            {
                DataTable dtViTri = DataProcessing.ReadData("SELECT TenViTri FROM ViTri WHERE TrangThai = 'HOAT_DONG' ORDER BY TenViTri");
                List<string> listViTri = new List<string>();
                foreach (DataRow row in dtViTri.Rows)
                {
                    listViTri.Add(row["TenViTri"].ToString());
                }
                ComboboxHelper.CaiDatTimKiem(cboViTri, listViTri);
            }
            catch
            {
                // Bỏ qua nếu lỗi
            }

            // 4. Đơn vị đo trọng lượng (g, kg)
            if (cboDonViTrongLuong.Items.Count == 0)
            {
                cboDonViTrongLuong.Items.AddRange(new object[] { "g", "kg" });
            }
            if (cboDonViTrongLuong.SelectedIndex < 0)
            {
                cboDonViTrongLuong.SelectedItem = "kg";
            }
        }

        private void SinhMaHangGoiY()
        {
            try
            {
                // Sinh mã chuẩn "HH" + 6 chữ số dựa trên số thứ tự lớn nhất của các mã dạng HH + số (đồng bộ với Nhập file)
                DataTable dtMa = DataProcessing.ReadData("SELECT MaHangNoiBo FROM HangHoa WHERE MaHangNoiBo LIKE 'HH%'");
                HashSet<string> dsMaDaCo = new HashSet<string>(StringComparer.OrdinalIgnoreCase);
                foreach (DataRow r in dtMa.Rows)
                {
                    string ma = r["MaHangNoiBo"]?.ToString()?.Trim();
                    if (!string.IsNullOrEmpty(ma)) dsMaDaCo.Add(ma);
                }

                long soLonNhat = LaySoMaHangLonNhat(dsMaDaCo);
                txtMaHang.Text = TaoMaHangTiepTheo(ref soLonNhat, dsMaDaCo);
            }
            catch
            {
                txtMaHang.Text = "HH000001";
            }
        }

        /// <summary>
        /// Lấy số thứ tự lớn nhất trong các mã hàng nội bộ có dạng ^HH[0-9]+$ (0 nếu chưa có)
        /// </summary>
        internal static long LaySoMaHangLonNhat(IEnumerable<string> dsMaHang)
        {
            long soLonNhat = 0;
            if (dsMaHang == null) return soLonNhat;

            foreach (string ma in dsMaHang)
            {
                string m = ma?.Trim();
                if (string.IsNullOrEmpty(m) || !Regex.IsMatch(m, @"^HH[0-9]+$", RegexOptions.IgnoreCase)) continue;

                if (long.TryParse(m.Substring(2), NumberStyles.None, CultureInfo.InvariantCulture, out long so) && so > soLonNhat)
                {
                    soLonNhat = so;
                }
            }
            return soLonNhat;
        }

        /// <summary>
        /// Sinh mã hàng tiếp theo dạng "HH" + 6 chữ số, tăng dần cho tới khi gặp mã chưa được sử dụng
        /// </summary>
        internal static string TaoMaHangTiepTheo(ref long soLonNhat, ICollection<string> dsMaDaCo)
        {
            string ma;
            do
            {
                soLonNhat++;
                ma = "HH" + soLonNhat.ToString("D6", CultureInfo.InvariantCulture);
            }
            while (dsMaDaCo != null && dsMaDaCo.Contains(ma));
            return ma;
        }

        private void NapThongTinHangHoa()
        {
            try
            {
                string sql = @"
                    SELECT h.MaHang, h.MaHangNoiBo, h.TenHang, h.MaNhom, h.MaDVT, h.QuyCach,
                           h.GiaBanThamKhao, h.GiaVon, h.TrangThai, h.HinhAnh, h.ViTri, h.TrongLuong, h.DonViTrongLuong, h.MoTa,
                           ISNULL((SELECT SUM(tk.SoLuongTon) FROM vw_TonKhoHienTai tk
                                   WHERE tk.MaHang = h.MaHang AND (@MaKho = 0 OR tk.MaKho = @MaKho)), 0) AS SoLuongTon,
                           ISNULL(ntk.TonToiThieu, 0) AS TonToiThieu,
                           ISNULL(ntk.TonCaoNhat, 0) AS TonCaoNhat
                    FROM HangHoa h
                    LEFT JOIN NguongTonKho ntk ON h.MaHang = ntk.MaHang AND ntk.MaKho = @MaKho
                    WHERE h.MaHang = @MaHang;";
                // Chế độ "Tất cả kho" (MaKho = 0): tồn kho là tổng tồn của mọi kho, không có định mức theo kho

                SqlParameter[] parameters = new SqlParameter[]
                {
                    new SqlParameter("@MaKho", SessionContext.MaKhoHienTai),
                    new SqlParameter("@MaHang", maHangHienTai)
                };

                DataTable dt = DataProcessing.ReadData(sql, parameters);
                if (dt.Rows.Count > 0)
                {
                    DataRow row = dt.Rows[0];

                    txtMaHang.Text = row["MaHangNoiBo"]?.ToString();
                    txtTenHang.Text = row["TenHang"]?.ToString();
                    trangThaiHienTai = row["TrangThai"]?.ToString() ?? "DANG_BAN";
                    txtMoTa.Text = row["MoTa"]?.ToString();
                    cboViTri.Text = row["ViTri"]?.ToString();

                    if (row["MaNhom"] != DBNull.Value)
                    {
                        cboNhomHang.SelectedValue = row["MaNhom"];
                    }
                    if (row["MaDVT"] != DBNull.Value)
                    {
                        cboDonVi.SelectedValue = row["MaDVT"];
                    }

                    txtGiaVon.Text = Convert.ToDecimal(row["GiaVon"]).ToString("N0");
                    txtGiaBan.Text = Convert.ToDecimal(row["GiaBanThamKhao"]).ToString("N0");
                    txtTonKho.Text = Convert.ToDecimal(row["SoLuongTon"]).ToString("N0");
                    // Hiển thị trọng lượng dạng số thuần (không nhóm nghìn, dấu chấm thập phân) để đọc lại khi lưu không bị sai
                    txtTrongLuong.Text = row["TrongLuong"] != DBNull.Value ? Convert.ToDecimal(row["TrongLuong"]).ToString("0.##", CultureInfo.InvariantCulture) : "0";

                    if (row["DonViTrongLuong"] != DBNull.Value && !string.IsNullOrWhiteSpace(row["DonViTrongLuong"].ToString()))
                    {
                        cboDonViTrongLuong.SelectedItem = row["DonViTrongLuong"].ToString();
                    }
                    else
                    {
                        cboDonViTrongLuong.SelectedItem = "kg";
                    }
                    if (SessionContext.MaKhoHienTai > 0)
                    {
                        txtDinhMucTonThapNhat.Text = Convert.ToDecimal(row["TonToiThieu"]).ToString("N0");
                        txtDinhMucTonCaoNhat.Text = Convert.ToDecimal(row["TonCaoNhat"]).ToString("N0");
                    }
                    else
                    {
                        txtDinhMucTonThapNhat.Text = "";
                        txtDinhMucTonCaoNhat.Text = "";
                    }

                    // Hiển thị 5 ảnh hàng hóa
                    for (int i = 0; i < 5; i++)
                    {
                        danhSachAnh[i] = null;
                        danhSachByteAnh[i] = null;
                    }
                    duLieuAnhHienTai = null;

                    // 1. Đọc tất cả ảnh của hàng hóa từ bảng HinhAnhHangHoa
                    try
                    {
                        string sqlDocAnh = "SELECT DuLieuAnh, ThuTu, LaAnhChinh FROM HinhAnhHangHoa WHERE MaHang = @MaHang ORDER BY ThuTu ASC";
                        DataTable dtAnh = DataProcessing.ReadData(sqlDocAnh, new SqlParameter[] { new SqlParameter("@MaHang", maHangHienTai) });

                        if (dtAnh != null && dtAnh.Rows.Count > 0)
                        {
                            int currentSlotIndex = 0;
                            foreach (DataRow rAnh in dtAnh.Rows)
                            {
                                if (rAnh["DuLieuAnh"] != DBNull.Value)
                                {
                                    byte[] rawBytes = (byte[])rAnh["DuLieuAnh"];
                                    int thuTu = Convert.ToInt32(rAnh["ThuTu"]);
                                    bool laAnhChinh = (rAnh["LaAnhChinh"] != DBNull.Value && Convert.ToBoolean(rAnh["LaAnhChinh"]));

                                    int slot;
                                    if (laAnhChinh && danhSachAnh[0] == null)
                                    {
                                        slot = 0;
                                    }
                                    else if (thuTu >= 1 && thuTu <= 5 && !dtAnh.AsEnumerable().Any(x => Convert.ToInt32(x["ThuTu"]) == 0))
                                    {
                                        slot = thuTu - 1;
                                    }
                                    else if (thuTu >= 0 && thuTu < 5)
                                    {
                                        slot = thuTu;
                                    }
                                    else
                                    {
                                        slot = currentSlotIndex;
                                    }

                                    if (slot >= 0 && slot < 5)
                                    {
                                        try
                                        {
                                            using (MemoryStream ms = new MemoryStream(rawBytes))
                                            using (Image rawImg = Image.FromStream(ms))
                                            {
                                                danhSachAnh[slot] = XuLyAnhNenTrang(rawImg, out byte[] whiteBytes);
                                                danhSachByteAnh[slot] = whiteBytes;
                                            }
                                        }
                                        catch { }

                                        if (slot == 0)
                                        {
                                            duLieuAnhHienTai = danhSachByteAnh[0];
                                        }
                                    }
                                    currentSlotIndex++;
                                }
                            }
                        }
                    }
                    catch { }

                    // 2. Nếu bảng HinhAnhHangHoa chưa có (hàng hóa cũ), lấy từ cột HangHoa.HinhAnh
                    if (danhSachAnh[0] == null && row["HinhAnh"] != DBNull.Value)
                    {
                        duLieuAnhHienTai = (byte[])row["HinhAnh"];
                        try
                        {
                            using (MemoryStream ms = new MemoryStream(duLieuAnhHienTai))
                            using (Image rawImg = Image.FromStream(ms))
                            {
                                danhSachAnh[0] = XuLyAnhNenTrang(rawImg, out byte[] whiteBytes);
                                danhSachByteAnh[0] = whiteBytes;
                                duLieuAnhHienTai = whiteBytes;
                            }
                        }
                        catch { }
                    }

                    CapNhatHienThi5Anh();
                    CapNhatGiaoDienNutXoa();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi nạp thông tin hàng hóa: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        #endregion

        #region Quản lý 5 khung ảnh & Thao tác

        private void KhoiTao5KhungAnh()
        {
            danhSachPic = new PictureBox[] { picAnhDaiDien, picAnhPhu1, picAnhPhu2, picAnhPhu3, picAnhPhu4 };

            if (btnXoaAnhChinh != null)
            {
                btnXoaAnhChinh.Cursor = Cursors.Hand;
                toolTipAnh.SetToolTip(btnXoaAnhChinh, "Bỏ chọn / Xóa ảnh trong khung to");
                btnXoaAnhChinh.BringToFront();
            }

            for (int i = 0; i < danhSachPic.Length; i++)
            {
                int index = i;
                PictureBox pic = danhSachPic[i];
                pic.SizeMode = PictureBoxSizeMode.Zoom;
                pic.Cursor = Cursors.Hand;
                pic.BorderStyle = BorderStyle.FixedSingle;
                pic.BackColor = Color.White;

                // Tooltip chỉ dẫn
                if (index == 0)
                {
                    toolTipAnh.SetToolTip(pic, "Ảnh chính (ô to). Click để chọn/thay ảnh mới. Click chuột phải để xóa.");
                }
                else
                {
                    toolTipAnh.SetToolTip(pic, $"Ảnh phụ #{index}.\nClick chuột trái để đổi ảnh này lên ô to (làm ảnh chính).\nClick chuột phải để xóa hoặc chọn ảnh mới.");
                }

                // Menu chuột phải
                ContextMenuStrip cms = new ContextMenuStrip();
                ToolStripMenuItem mnuChon = new ToolStripMenuItem("Chọn / Đổi ảnh...");
                mnuChon.Click += (s, e) => ChonAnhChoSlot(index);
                cms.Items.Add(mnuChon);

                if (index > 0)
                {
                    ToolStripMenuItem mnuDoiLenTo = new ToolStripMenuItem("Đổi lên ô to (Làm ảnh chính)");
                    mnuDoiLenTo.Click += (s, e) => HoanDoiAnhVoiOTo(index);
                    cms.Items.Add(mnuDoiLenTo);
                }

                ToolStripMenuItem mnuXoa = new ToolStripMenuItem("Xóa ảnh này");
                mnuXoa.Click += (s, e) => XoaAnhSlot(index);
                cms.Items.Add(mnuXoa);

                pic.ContextMenuStrip = cms;

                // Sự kiện click chuột trái cho 4 ô phụ (ô to dùng picAnhDaiDien_Click)
                if (index > 0)
                {
                    pic.Click += (s, e) =>
                    {
                        if (danhSachAnh[index] != null)
                        {
                            // Đã có ảnh -> Hoán đổi với ô to!
                            HoanDoiAnhVoiOTo(index);
                        }
                        else
                        {
                            // Chưa có ảnh -> Mở chọn ảnh cho ô này
                            ChonAnhChoSlot(index);
                        }
                    };
                }
            }

            CapNhatHienThi5Anh();
        }

        private void btnXoaAnhChinh_Click(object sender, EventArgs e)
        {
            XoaAnhSlot(0);
        }

        private void picAnhDaiDien_Click(object sender, EventArgs e)
        {
            ChonAnhChoSlot(0);
        }

        private void HoanDoiAnhVoiOTo(int index)
        {
            if (index <= 0 || index >= 5) return;
            if (danhSachAnh[index] == null && danhSachAnh[0] == null) return;

            // Hoán đổi ảnh giữa ô to (slot 0) và ô nhỏ (slot index)
            Image tempImg = danhSachAnh[0];
            byte[] tempBytes = danhSachByteAnh[0];

            danhSachAnh[0] = danhSachAnh[index];
            danhSachByteAnh[0] = danhSachByteAnh[index];

            danhSachAnh[index] = tempImg;
            danhSachByteAnh[index] = tempBytes;
            duLieuAnhHienTai = danhSachByteAnh[0];

            CapNhatHienThi5Anh();
            daChonAnhMoi = true;
        }

        private void XoaAnhSlot(int index)
        {
            if (index < 0 || index >= 5) return;
            danhSachAnh[index] = null;
            danhSachByteAnh[index] = null;
            daChonAnhMoi = true;
            CapNhatHienThi5Anh();
            if (index == 0)
            {
                duLieuAnhHienTai = null;
                daChonAnhMoi = true;
            }
        }

        private void ChonAnhChoSlot(int slotBatDau)
        {
            using (OpenFileDialog ofd = new OpenFileDialog())
            {
                ofd.Filter = "Ảnh (*.jpg;*.jpeg;*.png;*.bmp)|*.jpg;*.jpeg;*.png;*.bmp|Tất cả tệp (*.*)|*.*";
                ofd.Title = "Chọn ảnh hàng hóa (có thể chọn cùng lúc nhiều ảnh)";
                ofd.Multiselect = true;

                if (ofd.ShowDialog() == DialogResult.OK && ofd.FileNames.Length > 0)
                {
                    int currentSlot = slotBatDau;
                    for (int f = 0; f < ofd.FileNames.Length; f++)
                    {
                        // Nếu vượt quá 5 slot hoặc các file sau slotBatDau, tìm slot trống kế tiếp
                        if (currentSlot >= 5 || (f > 0 && danhSachAnh[currentSlot] != null))
                        {
                            currentSlot = -1;
                            for (int k = 0; k < 5; k++)
                            {
                                if (danhSachAnh[k] == null)
                                {
                                    currentSlot = k;
                                    break;
                                }
                            }
                            if (currentSlot == -1) break; // Cả 5 ô đều đã có ảnh
                        }

                        try
                        {
                            byte[] rawBytes = File.ReadAllBytes(ofd.FileNames[f]);
                            using (MemoryStream ms = new MemoryStream(rawBytes))
                            using (Image rawImg = Image.FromStream(ms))
                            {
                                danhSachAnh[currentSlot] = XuLyAnhNenTrang(rawImg, out byte[] whiteBytes);
                                danhSachByteAnh[currentSlot] = whiteBytes;
                            }
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show("Không thể tải ảnh: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }

                        currentSlot++;
                    }

                    CapNhatHienThi5Anh();
                    CapNhatGiaoDienNutXoa();
                    daChonAnhMoi = true;
                }
            }
        }

        private void CapNhatHienThi5Anh()
        {
            if (danhSachPic == null) return;

            for (int i = 0; i < 5; i++)
            {
                danhSachPic[i].Image = danhSachAnh[i];
                danhSachPic[i].BackColor = Color.White;
            }

            if (btnXoaAnhChinh != null)
            {
                btnXoaAnhChinh.BringToFront();
            }
        }

        /// <summary>
        /// Xử lý đổ nền trắng #FFFFFF cho ảnh (đặc biệt là ảnh PNG trong suốt) để tránh bị đen nền trong WinForms.
        /// </summary>
        private static Image XuLyAnhNenTrang(Image imgGoc, out byte[] bytesOutput)
        {
            if (imgGoc == null)
            {
                bytesOutput = null;
                return null;
            }

            Bitmap bmpMoi = new Bitmap(imgGoc.Width, imgGoc.Height, System.Drawing.Imaging.PixelFormat.Format32bppRgb);
            using (Graphics g = Graphics.FromImage(bmpMoi))
            {
                g.Clear(Color.White);
                g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
                g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
                g.PixelOffsetMode = System.Drawing.Drawing2D.PixelOffsetMode.HighQuality;
                g.DrawImage(imgGoc, new Rectangle(0, 0, imgGoc.Width, imgGoc.Height));
            }

            using (MemoryStream ms = new MemoryStream())
            {
                bmpMoi.Save(ms, System.Drawing.Imaging.ImageFormat.Png);
                bytesOutput = ms.ToArray();
            }

            return bmpMoi;
        }

        #region Xử lý Xóa hàng hóa

        private void CapNhatGiaoDienNutXoa()
        {
            if (btnXoa == null) return;

            if (maHangHienTai <= 0)
            {
                btnXoa.Visible = false;
                return;
            }

            btnXoa.Visible = true;
            btnXoa.UseVisualStyleBackColor = true;
            btnXoa.ForeColor = SystemColors.ControlText;
            if (btnLuu != null) btnXoa.Font = btnLuu.Font;

            if (trangThaiHienTai == "NGUNG_BAN")
            {
                btnXoa.Text = "Bán lại";
                toolTipAnh.SetToolTip(btnXoa, "Kích hoạt kinh doanh trở lại cho mặt hàng này");
            }
            else
            {
                btnXoa.Text = "Xóa";
                toolTipAnh.SetToolTip(btnXoa, "Xóa hàng hóa này khỏi hệ thống");
            }
        }

        private void KichHoatBanLai()
        {
            if (maHangHienTai <= 0) return;

            DialogResult dialog = MessageBox.Show(
                $"Bạn có chắc chắn muốn kích hoạt kinh doanh trở lại cho mặt hàng '{txtTenHang.Text}' ({txtMaHang.Text}) không?",
                "Kích hoạt bán lại",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question,
                MessageBoxDefaultButton.Button1);

            if (dialog != DialogResult.Yes) return;

            try
            {
                string sqlUpdate = "UPDATE HangHoa SET TrangThai = 'DANG_BAN' WHERE MaHang = @MaHang;";
                int rows = DataProcessing.ChangeData(sqlUpdate, new SqlParameter[] { new SqlParameter("@MaHang", maHangHienTai) });

                if (rows > 0)
                {
                    MessageBox.Show(
                        $"Đã kích hoạt kinh doanh trở lại cho hàng hóa '{txtTenHang.Text}' thành công!",
                        "Thông báo",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information);

                    this.DialogResult = DialogResult.OK;
                    this.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi kích hoạt bán lại hàng hóa: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            ThucHienThaoTacXoaHoacBanLai();
        }


        private void ThucHienThaoTacXoaHoacBanLai()
        {
            if (trangThaiHienTai == "NGUNG_BAN")
            {
                KichHoatBanLai();
            }
            else
            {
                XoaHangHoa();
            }
        }

        private void XoaHangHoa()
        {
            if (maHangHienTai <= 0)
            {
                MessageBox.Show("Hàng hóa này chưa được lưu vào hệ thống nên không thể xóa!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            try
            {
                // 1. Kiểm tra ràng buộc chứng từ kho & giao dịch
                string sqlKiemTra = @"
                    SELECT 
                        (SELECT COUNT(1) FROM ChiTietPhieuNhap WHERE MaHang = @MaHang) AS DemNhap,
                        (SELECT COUNT(1) FROM ChiTietPhieuXuat WHERE MaHang = @MaHang) AS DemXuat,
                        (SELECT COUNT(1) FROM ChiTietDonBan WHERE MaHang = @MaHang) AS DemBan,
                        (SELECT COUNT(1) FROM ChiTietPhieuTraHangNhap WHERE MaHang = @MaHang) AS DemTra,
                        (SELECT COUNT(1) FROM ChiTietPhieuChuyenKho WHERE MaHang = @MaHang) AS DemChuyen,
                        (SELECT COUNT(1) FROM ChiTietPhieuKiemKe WHERE MaHang = @MaHang) AS DemKiem;";

                DataTable dtCheck = DataProcessing.ReadData(sqlKiemTra, new SqlParameter[] { new SqlParameter("@MaHang", maHangHienTai) });
                if (dtCheck != null && dtCheck.Rows.Count > 0)
                {
                    DataRow r = dtCheck.Rows[0];
                    int demNhap = Convert.ToInt32(r["DemNhap"]);
                    int demXuat = Convert.ToInt32(r["DemXuat"]);
                    int demBan = Convert.ToInt32(r["DemBan"]);
                    int demTra = Convert.ToInt32(r["DemTra"]);
                    int demChuyen = Convert.ToInt32(r["DemChuyen"]);
                    int demKiem = Convert.ToInt32(r["DemKiem"]);

                    int tongGiaoDich = demNhap + demXuat + demBan + demTra + demChuyen + demKiem;
                    if (tongGiaoDich > 0)
                    {
                        string lyDo = "";
                        if (demNhap > 0) lyDo += $"\n- {demNhap} dòng phiếu nhập kho";
                        if (demXuat > 0) lyDo += $"\n- {demXuat} dòng phiếu xuất kho";
                        if (demBan > 0) lyDo += $"\n- {demBan} dòng đơn bán hàng";
                        if (demTra > 0) lyDo += $"\n- {demTra} dòng phiếu trả hàng nhập";
                        if (demChuyen > 0) lyDo += $"\n- {demChuyen} dòng phiếu chuyển kho";
                        if (demKiem > 0) lyDo += $"\n- {demKiem} dòng phiếu kiểm kê";

                        if (trangThaiHienTai == "NGUNG_BAN")
                        {
                            MessageBox.Show(
                                $"Không thể xóa hàng hóa '{txtTenHang.Text}' ({txtMaHang.Text}) vì đã phát sinh chứng từ giao dịch trong hệ thống:{lyDo}\n\nQuy chuẩn quản lý kho không cho phép xóa dữ liệu đã có lịch sử giao dịch nhằm bảo toàn tính toàn vẹn sổ sách.\n\nHàng hóa này hiện tại đã ở trạng thái 'Ngừng bán'.",
                                "Không thể xóa hàng hóa",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Warning);
                            return;
                        }

                        DialogResult hoiChuyen = MessageBox.Show(
                            $"Không thể xóa hàng hóa '{txtTenHang.Text}' ({txtMaHang.Text}) vì đã phát sinh chứng từ giao dịch trong hệ thống:{lyDo}\n\nQuy chuẩn quản lý kho không cho phép xóa dữ liệu đã có lịch sử giao dịch nhằm bảo toàn tính toàn vẹn sổ sách.\n\nBạn có muốn chuyển trạng thái hàng hóa này sang 'Ngừng bán' không?",
                            "Không thể xóa hàng hóa - Chuyển sang Ngừng bán",
                            MessageBoxButtons.YesNo,
                            MessageBoxIcon.Question,
                            MessageBoxDefaultButton.Button1);

                        if (hoiChuyen == DialogResult.Yes)
                        {
                            string sqlUpdateTrangThai = "UPDATE HangHoa SET TrangThai = 'NGUNG_BAN' WHERE MaHang = @MaHang;";
                            DataProcessing.ChangeData(sqlUpdateTrangThai, new SqlParameter[] { new SqlParameter("@MaHang", maHangHienTai) });

                            MessageBox.Show(
                                $"Đã chuyển trạng thái hàng hóa '{txtTenHang.Text}' sang 'Ngừng bán' thành công!",
                                "Thông báo",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Information);

                            this.DialogResult = DialogResult.OK;
                            this.Close();
                        }

                        return;
                    }
                }

                // 2. Kiểm tra tồn kho thực tế hiện tại
                object objTon = DataProcessing.ReadValue(
                    "SELECT ISNULL(SUM(SoLuongTon), 0) FROM vw_TonKhoHienTai WHERE MaHang = @MaHang",
                    new SqlParameter[] { new SqlParameter("@MaHang", maHangHienTai) });
                decimal tonKho = objTon != null && decimal.TryParse(objTon.ToString(), out decimal t) ? t : 0;
                if (tonKho > 0)
                {
                    if (trangThaiHienTai == "NGUNG_BAN")
                    {
                        MessageBox.Show(
                            $"Không thể xóa hàng hóa '{txtTenHang.Text}' vì vẫn còn tồn kho ({tonKho:N0})!\n\nHàng hóa này hiện tại đã ở trạng thái 'Ngừng bán'.",
                            "Không thể xóa hàng hóa",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Warning);
                        return;
                    }

                    DialogResult hoiChuyenTon = MessageBox.Show(
                        $"Không thể xóa hàng hóa '{txtTenHang.Text}' vì vẫn còn tồn kho ({tonKho:N0})!\n\nBạn có muốn chuyển trạng thái hàng hóa này sang 'Ngừng bán' không?",
                        "Không thể xóa hàng hóa - Chuyển sang Ngừng bán",
                        MessageBoxButtons.YesNo,
                        MessageBoxIcon.Question,
                        MessageBoxDefaultButton.Button1);

                    if (hoiChuyenTon == DialogResult.Yes)
                    {
                        string sqlUpdateTrangThai = "UPDATE HangHoa SET TrangThai = 'NGUNG_BAN' WHERE MaHang = @MaHang;";
                        DataProcessing.ChangeData(sqlUpdateTrangThai, new SqlParameter[] { new SqlParameter("@MaHang", maHangHienTai) });

                        MessageBox.Show(
                            $"Đã chuyển trạng thái hàng hóa '{txtTenHang.Text}' sang 'Ngừng bán' thành công!",
                            "Thông báo",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Information);

                            this.DialogResult = DialogResult.OK;
                            this.Close();
                    }

                    return;
                }

                // 3. Hỏi xác nhận người dùng trước khi xóa vĩnh viễn
                DialogResult dialog = MessageBox.Show(
                    $"Bạn có chắc chắn muốn xóa vĩnh viễn hàng hóa '{txtTenHang.Text}' (Mã: {txtMaHang.Text}) khỏi cơ sở dữ liệu không?\n\nLưu ý: Toàn bộ thông tin, hình ảnh và định mức tồn kho liên quan sẽ bị xóa và không thể khôi phục!",
                    "Xác nhận xóa hàng hóa",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question,
                    MessageBoxDefaultButton.Button2);

                if (dialog != DialogResult.Yes) return;

                // 4. Thực hiện xóa trong Transaction ADO.NET
                DataProcessing.ChangeDataTransaction((conn, trans) =>
                {
                    // Xóa ảnh trong HinhAnhHangHoa
                    using (SqlCommand cmdAnh = new SqlCommand("DELETE FROM HinhAnhHangHoa WHERE MaHang = @MaHang;", conn, trans))
                    {
                        cmdAnh.Parameters.AddWithValue("@MaHang", maHangHienTai);
                        cmdAnh.ExecuteNonQuery();
                    }

                    // Xóa định mức tồn kho trong NguongTonKho
                    using (SqlCommand cmdNguong = new SqlCommand("DELETE FROM NguongTonKho WHERE MaHang = @MaHang;", conn, trans))
                    {
                        cmdNguong.Parameters.AddWithValue("@MaHang", maHangHienTai);
                        cmdNguong.ExecuteNonQuery();
                    }

                    // Xóa hàng hóa trong HangHoa
                    using (SqlCommand cmdHang = new SqlCommand("DELETE FROM HangHoa WHERE MaHang = @MaHang;", conn, trans))
                    {
                        cmdHang.Parameters.AddWithValue("@MaHang", maHangHienTai);
                        cmdHang.ExecuteNonQuery();
                    }
                });

                MessageBox.Show($"Đã xóa thành công hàng hóa '{txtTenHang.Text}'!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi xóa hàng hóa: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        #endregion

        private void btnBoQua_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void btnLuu_Click(object sender, EventArgs e)
        {
            // 1. Kiểm tra tính hợp lệ
            string maHangNoiBo = txtMaHang.Text.Trim();
            if (string.IsNullOrWhiteSpace(maHangNoiBo))
            {
                MessageBox.Show("Vui lòng nhập Mã hàng hóa!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtMaHang.Focus();
                return;
            }

            string tenHang = txtTenHang.Text.Trim();
            if (string.IsNullOrWhiteSpace(tenHang))
            {
                MessageBox.Show("Vui lòng nhập Tên hàng hóa!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtTenHang.Focus();
                return;
            }

            if (cboNhomHang.SelectedIndex < 0 && !string.IsNullOrWhiteSpace(cboNhomHang.Text))
            {
                int idx = cboNhomHang.FindStringExact(cboNhomHang.Text.Trim());
                if (idx >= 0) cboNhomHang.SelectedIndex = idx;
            }
            if (cboNhomHang.SelectedValue == null || !int.TryParse(cboNhomHang.SelectedValue.ToString(), out int maNhom) || maNhom <= 0)
            {
                MessageBox.Show("Vui lòng chọn Nhóm hàng hợp lệ!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                cboNhomHang.Focus();
                return;
            }

            if (cboDonVi.SelectedIndex < 0 && !string.IsNullOrWhiteSpace(cboDonVi.Text))
            {
                int idx = cboDonVi.FindStringExact(cboDonVi.Text.Trim());
                if (idx >= 0) cboDonVi.SelectedIndex = idx;
            }
            if (cboDonVi.SelectedValue == null || !int.TryParse(cboDonVi.SelectedValue.ToString(), out int maDVT) || maDVT <= 0)
            {
                MessageBox.Show("Vui lòng chọn Đơn vị tính hợp lệ!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                cboDonVi.Focus();
                return;
            }

            // Chuyển đổi số tiền
            decimal.TryParse(txtGiaVon.Text.Replace(",", "").Replace(".", ""), out decimal giaVon);
            decimal.TryParse(txtGiaBan.Text.Replace(",", "").Replace(".", ""), out decimal giaBan);
            // Trọng lượng: đọc theo cùng quy tắc với Nhập file (không phụ thuộc thiết lập vùng miền của máy)
            decimal trongLuong = 0;
            if (!string.IsNullOrWhiteSpace(txtTrongLuong.Text))
            {
                trongLuong = frmNhapHangHoaFile.ChuyenDoiSoThapPhan(txtTrongLuong.Text, -1);
                if (trongLuong < 0)
                {
                    MessageBox.Show("Trọng lượng không hợp lệ! Vui lòng nhập số, ví dụ: 12.5", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtTrongLuong.Focus();
                    return;
                }
            }
            string donViTrongLuong = cboDonViTrongLuong.SelectedItem?.ToString() ?? "kg";
            decimal.TryParse(txtDinhMucTonThapNhat.Text.Replace(",", "").Replace(".", ""), out decimal tonThapNhat);
            decimal.TryParse(txtDinhMucTonCaoNhat.Text.Replace(",", "").Replace(".", ""), out decimal tonCaoNhat);

            // Form không có ô Quy cách: chỉ gán NULL khi thêm mới, khi sửa giữ nguyên giá trị Quy cách đang có trong CSDL
            string quyCach = null;
            string moTa = txtMoTa.Text.Trim();
            string viTri = cboViTri.Text.Trim();

            // Ô ảnh chính (ô to) trống nhưng các ô phụ có ảnh -> lấy ảnh phụ đầu tiên làm ảnh chính
            if (danhSachByteAnh[0] == null)
            {
                for (int i = 1; i < 5; i++)
                {
                    if (danhSachByteAnh[i] != null && danhSachByteAnh[i].Length > 0)
                    {
                        HoanDoiAnhVoiOTo(i);
                        break;
                    }
                }
            }

            // Luôn lấy dữ liệu ảnh ở ô to (slot 0) để lưu vào cơ sở dữ liệu
            duLieuAnhHienTai = danhSachByteAnh[0];

            bool laThemMoi = maHangHienTai == 0;
            int maHangLuu = maHangHienTai;
            int maKhoHienTai = SessionContext.MaKhoHienTai;

            try
            {
                // Kiểm tra trùng mã hàng nội bộ
                string sqlCheckTrung = "SELECT COUNT(*) FROM HangHoa WHERE MaHangNoiBo = @MaHangNoiBo AND MaHang <> @MaHang";
                SqlParameter[] checkParams = new SqlParameter[]
                {
                    new SqlParameter("@MaHangNoiBo", maHangNoiBo),
                    new SqlParameter("@MaHang", maHangHienTai)
                };
                int countTrung = Convert.ToInt32(DataProcessing.ReadValue(sqlCheckTrung, checkParams));
                if (countTrung > 0)
                {
                    MessageBox.Show($"Mã hàng '{maHangNoiBo}' đã được sử dụng bởi hàng hóa khác. Vui lòng chọn mã khác!", "Trùng mã", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtMaHang.Focus();
                    return;
                }

                // Thực hiện lưu với Transaction
                DataProcessing.ChangeDataTransaction((conn, trans) =>
                {
                    if (laThemMoi) // THÊM MỚI
                    {
                        string sqlInsert = @"
                            INSERT INTO HangHoa (
                                MaNhom, MaDVT, MaHangNoiBo, TenHang, QuyCach, 
                                GiaBanThamKhao, GiaVon, TrangThai, HinhAnh, NgayTao, 
                                ViTri, TrongLuong, DonViTrongLuong, MoTa
                            ) 
                            VALUES (
                                @MaNhom, @MaDVT, @MaHangNoiBo, @TenHang, @QuyCach, 
                                @GiaBan, @GiaVon, 'DANG_BAN', @HinhAnh, GETDATE(), 
                                @ViTri, @TrongLuong, @DonViTrongLuong, @MoTa
                            );
                            SELECT SCOPE_IDENTITY();";

                        using (SqlCommand cmd = new SqlCommand(sqlInsert, conn, trans))
                        {
                            cmd.Parameters.AddWithValue("@MaNhom", maNhom);
                            cmd.Parameters.AddWithValue("@MaDVT", maDVT);
                            cmd.Parameters.AddWithValue("@MaHangNoiBo", maHangNoiBo);
                            cmd.Parameters.AddWithValue("@TenHang", tenHang);
                            cmd.Parameters.AddWithValue("@QuyCach", string.IsNullOrEmpty(quyCach) ? (object)DBNull.Value : quyCach);
                            cmd.Parameters.AddWithValue("@GiaBan", giaBan);
                            cmd.Parameters.AddWithValue("@GiaVon", giaVon);
                            cmd.Parameters.AddWithValue("@ViTri", string.IsNullOrEmpty(viTri) ? (object)DBNull.Value : viTri);
                            cmd.Parameters.AddWithValue("@TrongLuong", trongLuong);
                            cmd.Parameters.AddWithValue("@DonViTrongLuong", donViTrongLuong);
                            cmd.Parameters.AddWithValue("@MoTa", string.IsNullOrEmpty(moTa) ? (object)DBNull.Value : moTa);

                            SqlParameter paramHinhAnh = new SqlParameter("@HinhAnh", SqlDbType.VarBinary);
                            paramHinhAnh.Value = duLieuAnhHienTai != null ? (object)duLieuAnhHienTai : DBNull.Value;
                            cmd.Parameters.Add(paramHinhAnh);

                            // Chỉ gán vào biến cục bộ; maHangHienTai được cập nhật sau khi giao dịch commit thành công
                            object newId = cmd.ExecuteScalar();
                            maHangLuu = Convert.ToInt32(newId);
                        }
                    }
                    else // CẬP NHẬT (không ghi đè QuyCach / ThuocTinh vì form không hiển thị các trường này)
                    {
                        string sqlUpdate = @"
                            UPDATE HangHoa
                            SET MaNhom = @MaNhom,
                                MaDVT = @MaDVT,
                                MaHangNoiBo = @MaHangNoiBo,
                                TenHang = @TenHang,
                                GiaBanThamKhao = @GiaBan,
                                GiaVon = @GiaVon, 
                                ViTri = @ViTri, 
                                TrongLuong = @TrongLuong, 
                                DonViTrongLuong = @DonViTrongLuong, 
                                MoTa = @MoTa"
                                + (daChonAnhMoi ? ", HinhAnh = @HinhAnh " : "") +
                            " WHERE MaHang = @MaHang;";

                        using (SqlCommand cmd = new SqlCommand(sqlUpdate, conn, trans))
                        {
                            cmd.Parameters.AddWithValue("@MaHang", maHangLuu);
                            cmd.Parameters.AddWithValue("@MaNhom", maNhom);
                            cmd.Parameters.AddWithValue("@MaDVT", maDVT);
                            cmd.Parameters.AddWithValue("@MaHangNoiBo", maHangNoiBo);
                            cmd.Parameters.AddWithValue("@TenHang", tenHang);
                            cmd.Parameters.AddWithValue("@GiaBan", giaBan);
                            cmd.Parameters.AddWithValue("@GiaVon", giaVon);
                            cmd.Parameters.AddWithValue("@ViTri", string.IsNullOrEmpty(viTri) ? (object)DBNull.Value : viTri);
                            cmd.Parameters.AddWithValue("@TrongLuong", trongLuong);
                            cmd.Parameters.AddWithValue("@DonViTrongLuong", donViTrongLuong);
                            cmd.Parameters.AddWithValue("@MoTa", string.IsNullOrEmpty(moTa) ? (object)DBNull.Value : moTa);

                            if (daChonAnhMoi)
                            {
                                SqlParameter paramHinhAnh = new SqlParameter("@HinhAnh", SqlDbType.VarBinary);
                                paramHinhAnh.Value = duLieuAnhHienTai != null ? (object)duLieuAnhHienTai : DBNull.Value;
                                cmd.Parameters.Add(paramHinhAnh);
                            }

                            cmd.ExecuteNonQuery();
                        }
                    }

                    // Lưu toàn bộ 5 ảnh vào bảng HinhAnhHangHoa
                    if (daChonAnhMoi || laThemMoi)
                    {
                        string sqlXoaAnh = "DELETE FROM HinhAnhHangHoa WHERE MaHang = @MaHang;";
                        using (SqlCommand cmdXoa = new SqlCommand(sqlXoaAnh, conn, trans))
                        {
                            cmdXoa.Parameters.AddWithValue("@MaHang", maHangLuu);
                            cmdXoa.ExecuteNonQuery();
                        }

                        string sqlChenAnh = @"
                            INSERT INTO HinhAnhHangHoa (MaHang, DuLieuAnh, ThuTu, LaAnhChinh, NgayTao)
                            VALUES (@MaHang, @DuLieuAnh, @ThuTu, @LaAnhChinh, GETDATE());";

                        for (int i = 0; i < 5; i++)
                        {
                            if (danhSachByteAnh[i] != null && danhSachByteAnh[i].Length > 0)
                            {
                                using (SqlCommand cmdAnh = new SqlCommand(sqlChenAnh, conn, trans))
                                {
                                    cmdAnh.Parameters.AddWithValue("@MaHang", maHangLuu);
                                    cmdAnh.Parameters.AddWithValue("@ThuTu", i + 1);
                                    cmdAnh.Parameters.AddWithValue("@LaAnhChinh", (i == 0));

                                    SqlParameter paramBytes = new SqlParameter("@DuLieuAnh", SqlDbType.VarBinary);
                                    paramBytes.Value = danhSachByteAnh[i];
                                    cmdAnh.Parameters.Add(paramBytes);

                                    cmdAnh.ExecuteNonQuery();
                                }
                            }
                        }
                    }

                    // Lưu định mức tồn kho (NguongTonKho) - chỉ khi đang làm việc tại một kho cụ thể (MaKho > 0)
                    // Đã có dòng: cả 2 định mức = 0 thì xóa, ngược lại cập nhật (cho phép đặt lại về 0). Chưa có dòng: chỉ thêm khi > 0
                    if (maKhoHienTai > 0)
                    {
                        string sqlNguong = @"
                            IF EXISTS (SELECT 1 FROM NguongTonKho WHERE MaKho = @MaKho AND MaHang = @MaHang)
                            BEGIN
                                IF (@TonThapNhat = 0 AND @TonCaoNhat = 0)
                                    DELETE FROM NguongTonKho WHERE MaKho = @MaKho AND MaHang = @MaHang;
                                ELSE
                                    UPDATE NguongTonKho
                                    SET TonToiThieu = @TonThapNhat, TonCaoNhat = @TonCaoNhat
                                    WHERE MaKho = @MaKho AND MaHang = @MaHang;
                            END
                            ELSE IF (@TonThapNhat > 0 OR @TonCaoNhat > 0)
                                INSERT INTO NguongTonKho (MaKho, MaHang, TonToiThieu, TonCaoNhat)
                                VALUES (@MaKho, @MaHang, @TonThapNhat, @TonCaoNhat);";

                        using (SqlCommand cmd = new SqlCommand(sqlNguong, conn, trans))
                        {
                            cmd.Parameters.AddWithValue("@MaKho", maKhoHienTai);
                            cmd.Parameters.AddWithValue("@MaHang", maHangLuu);
                            cmd.Parameters.AddWithValue("@TonThapNhat", tonThapNhat);
                            cmd.Parameters.AddWithValue("@TonCaoNhat", tonCaoNhat);
                            cmd.ExecuteNonQuery();
                        }
                    }
                });

                // Giao dịch đã commit thành công mới ghi nhận mã hàng (tránh giữ mã đã bị rollback)
                maHangHienTai = maHangLuu;

                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi lưu hàng hóa: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        #endregion
    }
}
