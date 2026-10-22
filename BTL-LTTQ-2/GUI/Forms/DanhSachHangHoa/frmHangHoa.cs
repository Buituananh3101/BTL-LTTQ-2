using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using BTL_LTTQ_2.BLL;
using BTL_LTTQ_2.DAL;

namespace BTL_LTTQ_2.GUI.Forms.DanhSachHangHoa
{
    public partial class frmHangHoa : Form
    {
        private int maHangHienTai = 0; // 0: Thêm mới, > 0: Chỉnh sửa
        private byte[] duLieuAnhHienTai = null;
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

        // Bật WS_EX_COMPOSITED để vẽ form nguyên khối trong bộ nhớ đệm, chống chớp giật
        protected override CreateParams CreateParams
        {
            get
            {
                CreateParams cp = base.CreateParams;
                cp.ExStyle |= 0x02000000; // WS_EX_COMPOSITED
                return cp;
            }
        }

        private void frmHangHoa_Load(object sender, EventArgs e)
        {
            //// Căn giữa vùng làm việc MDI (khắc phục việc Windows MDI bỏ qua thuộc tính CenterScreen)
            //if (this.MdiParent != null)
            //{
            //    MdiClient mdiClient = null;
            //    foreach (Control c in this.MdiParent.Controls)
            //    {
            //        if (c is MdiClient mc) { mdiClient = mc; break; }
            //    }

            //    int clientW = mdiClient != null ? mdiClient.ClientSize.Width : this.MdiParent.ClientSize.Width;
            //    int clientH = mdiClient != null ? mdiClient.ClientSize.Height : this.MdiParent.ClientSize.Height;

            //    this.Location = new Point(
            //        Math.Max(0, (clientW - this.Width) / 2),
            //        Math.Max(0, (clientH - this.Height) / 2)
            //    );
            //}
            //else
            //{
            //    this.CenterToScreen();
            //}

            //
            KhoiTao5KhungAnh();
            NapCombobox();

            if (maHangHienTai > 0)
            {
                lblTaoHangHoa.Text = "Cập nhật hàng hóa";
                this.Text = "Cập nhật thông tin hàng hóa";
                btnLuu.Text = "Sửa";

                // Khóa ô tồn kho vì tồn kho tính động qua chứng từ kho
                txtTonKho.ReadOnly = true;
                txtTonKho.BackColor = Color.FromArgb(243, 244, 246);

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
                txtDinhMucTonThapNhat.Text = "0";
                txtDinhMucTonCaoNhat.Text = "0";

                SinhMaHangGoiY();
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
                DataTable dtViTri = DataProcessing.ReadData("SELECT DISTINCT ViTri FROM HangHoa WHERE ViTri IS NOT NULL AND LTRIM(RTRIM(ViTri)) <> '' ORDER BY ViTri");
                List<string> listViTri = new List<string>();
                foreach (DataRow row in dtViTri.Rows)
                {
                    listViTri.Add(row["ViTri"].ToString());
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
                object result = DataProcessing.ReadValue("SELECT ISNULL(MAX(MaHang), 0) + 1 FROM HangHoa");
                int nextId = result != null && int.TryParse(result.ToString(), out int id) ? id : 1;
                txtMaHang.Text = $"HH{nextId:D4}";
            }
            catch
            {
                txtMaHang.Text = "HH0001";
            }
        }

        private void NapThongTinHangHoa()
        {
            try
            {
                string sql = @"
                    SELECT h.MaHang, h.MaHangNoiBo, h.TenHang, h.MaNhom, h.MaDVT, h.QuyCach,
                           h.GiaBanThamKhao, h.GiaVon, h.TrangThai, h.HinhAnh, h.ViTri, h.TrongLuong, h.DonViTrongLuong, h.MoTa,
                           ISNULL(tk.SoLuongTon, 0) AS SoLuongTon,
                           ISNULL(ntk.TonToiThieu, 0) AS TonToiThieu,
                           ISNULL(ntk.TonCaoNhat, 0) AS TonCaoNhat
                    FROM HangHoa h
                    LEFT JOIN vw_TonKhoHienTai tk ON h.MaHang = tk.MaHang AND tk.MaKho = @MaKho
                    LEFT JOIN NguongTonKho ntk ON h.MaHang = ntk.MaHang AND ntk.MaKho = @MaKho
                    WHERE h.MaHang = @MaHang;";

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
                    txtTrongLuong.Text = row["TrongLuong"] != DBNull.Value ? Convert.ToDecimal(row["TrongLuong"]).ToString("N2") : "0";

                    if (row["DonViTrongLuong"] != DBNull.Value && !string.IsNullOrWhiteSpace(row["DonViTrongLuong"].ToString()))
                    {
                        cboDonViTrongLuong.SelectedItem = row["DonViTrongLuong"].ToString();
                    }
                    else
                    {
                        cboDonViTrongLuong.SelectedItem = "kg";
                    }
                    txtDinhMucTonThapNhat.Text = Convert.ToDecimal(row["TonToiThieu"]).ToString("N0");
                    txtDinhMucTonCaoNhat.Text = Convert.ToDecimal(row["TonCaoNhat"]).ToString("N0");

                    // Hiển thị ảnh
                    if (row["HinhAnh"] != DBNull.Value)
                    {
                        duLieuAnhHienTai = (byte[])row["HinhAnh"];
                        danhSachByteAnh[0] = duLieuAnhHienTai;
                        using (MemoryStream ms = new MemoryStream(duLieuAnhHienTai))
                        {
                            danhSachAnh[0] = Image.FromStream(ms);
                        }
                        CapNhatHienThi5Anh();
                    }
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

            for (int i = 0; i < danhSachPic.Length; i++)
            {
                int index = i;
                PictureBox pic = danhSachPic[i];
                pic.SizeMode = PictureBoxSizeMode.Zoom;
                pic.Cursor = Cursors.Hand;
                pic.BorderStyle = BorderStyle.FixedSingle;

                // Tooltip chỉ dẫn
                if (index == 0)
                {
                    toolTipAnh.SetToolTip(pic, "Ảnh chính (ô to) sẽ được lưu vào cơ sở dữ liệu khi bấm Lưu.\nClick để chọn/thay ảnh mới. Click chuột phải để xóa.");
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

            CapNhatHienThi5Anh();
            daChonAnhMoi = true;
        }

        private void XoaAnhSlot(int index)
        {
            if (index < 0 || index >= 5) return;
            danhSachAnh[index] = null;
            danhSachByteAnh[index] = null;
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
                            byte[] bytes = File.ReadAllBytes(ofd.FileNames[f]);
                            using (MemoryStream ms = new MemoryStream(bytes))
                            {
                                danhSachAnh[currentSlot] = Image.FromStream(ms);
                                danhSachByteAnh[currentSlot] = bytes;
                            }
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show("Không thể tải ảnh: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }

                        currentSlot++;
                    }

                    CapNhatHienThi5Anh();
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
                if (danhSachAnh[i] != null)
                {
                    danhSachPic[i].BackColor = Color.White;
                }
                else
                {
                    danhSachPic[i].BackColor = SystemColors.ActiveBorder;
                }
            }
        }

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
            decimal.TryParse(txtTrongLuong.Text.Replace(",", "."), out decimal trongLuong);
            string donViTrongLuong = cboDonViTrongLuong.SelectedItem?.ToString() ?? "kg";
            decimal.TryParse(txtDinhMucTonThapNhat.Text.Replace(",", "").Replace(".", ""), out decimal tonThapNhat);
            decimal.TryParse(txtDinhMucTonCaoNhat.Text.Replace(",", "").Replace(".", ""), out decimal tonCaoNhat);

            string quyCach = null;
            string moTa = txtMoTa.Text.Trim();
            string viTri = cboViTri.Text.Trim();

            // Luôn lấy dữ liệu ảnh ở ô to (slot 0) để lưu vào cơ sở dữ liệu
            duLieuAnhHienTai = danhSachByteAnh[0];

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
                    if (maHangHienTai == 0) // THÊM MỚI
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

                            object newId = cmd.ExecuteScalar();
                            maHangHienTai = Convert.ToInt32(newId);
                        }
                    }
                    else // CẬP NHẬT
                    {
                        string sqlUpdate = @"
                            UPDATE HangHoa 
                            SET MaNhom = @MaNhom, 
                                MaDVT = @MaDVT, 
                                MaHangNoiBo = @MaHangNoiBo, 
                                TenHang = @TenHang, 
                                QuyCach = @QuyCach, 
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
                            cmd.Parameters.AddWithValue("@MaHang", maHangHienTai);
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

                            if (daChonAnhMoi)
                            {
                                SqlParameter paramHinhAnh = new SqlParameter("@HinhAnh", SqlDbType.VarBinary);
                                paramHinhAnh.Value = duLieuAnhHienTai != null ? (object)duLieuAnhHienTai : DBNull.Value;
                                cmd.Parameters.Add(paramHinhAnh);
                            }

                            cmd.ExecuteNonQuery();
                        }
                    }

                    // Lưu định mức tồn kho (NguongTonKho)
                    if (tonThapNhat > 0 || tonCaoNhat > 0)
                    {
                        string sqlNguong = @"
                            IF EXISTS (SELECT 1 FROM NguongTonKho WHERE MaKho = @MaKho AND MaHang = @MaHang)
                                UPDATE NguongTonKho 
                                SET TonToiThieu = @TonThapNhat, TonCaoNhat = @TonCaoNhat 
                                WHERE MaKho = @MaKho AND MaHang = @MaHang;
                            ELSE
                                INSERT INTO NguongTonKho (MaKho, MaHang, TonToiThieu, TonCaoNhat) 
                                VALUES (@MaKho, @MaHang, @TonThapNhat, @TonCaoNhat);";

                        using (SqlCommand cmd = new SqlCommand(sqlNguong, conn, trans))
                        {
                            cmd.Parameters.AddWithValue("@MaKho", SessionContext.MaKhoHienTai);
                            cmd.Parameters.AddWithValue("@MaHang", maHangHienTai);
                            cmd.Parameters.AddWithValue("@TonThapNhat", tonThapNhat);
                            cmd.Parameters.AddWithValue("@TonCaoNhat", tonCaoNhat);
                            cmd.ExecuteNonQuery();
                        }
                    }
                });

                MessageBox.Show("Lưu thông tin hàng hóa thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
