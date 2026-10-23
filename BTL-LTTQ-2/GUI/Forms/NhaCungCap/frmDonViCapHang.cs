using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Windows.Forms;
using BTL_LTTQ_2.Classes;

namespace BTL_LTTQ_2.GUI.Forms.NhaCungCap
{
    public partial class frmDonViCapHang : Form
    {
        private int maDonViCapHangHienTai = 0; // 0: Thêm mới, > 0: Chỉnh sửa

        public frmDonViCapHang()
        {
            InitializeComponent();
            maDonViCapHangHienTai = 0;
            BatChongChopGiat();
        }

        public frmDonViCapHang(int maDonViCapHang)
        {
            InitializeComponent();
            maDonViCapHangHienTai = maDonViCapHang;
            BatChongChopGiat();
        }

        private void BatChongChopGiat()
        {
            this.DoubleBuffered = true;
            this.SetStyle(ControlStyles.OptimizedDoubleBuffer | ControlStyles.AllPaintingInWmPaint | ControlStyles.UserPaint, true);
            this.UpdateStyles();
        }

        protected override CreateParams CreateParams
        {
            get
            {
                CreateParams cp = base.CreateParams;
                cp.ExStyle |= 0x02000000; // WS_EX_COMPOSITED
                return cp;
            }
        }

        private void frmDonViCapHang_Load(object sender, EventArgs e)
        {
            NapCombobox();

            if (maDonViCapHangHienTai > 0)
            {
                lblTieuDe.Text = "Cập nhật nhà cung cấp";
                this.Text = "Cập nhật thông tin nhà cung cấp";
                grpCongNo.Visible = true;
                this.ClientSize = new Size(960, 705);
                NapThongTinNhaCungCap();
            }
            else
            {
                lblTieuDe.Text = "Tạo nhà cung cấp";
                this.Text = "Thêm nhà cung cấp mới";
                grpCongNo.Visible = false;
                this.ClientSize = new Size(960, 610);
                SinhMaNccGoiY();
            }
        }

        #region Nạp dữ liệu & Khởi tạo

        private void NapCombobox()
        {
            // 1. Nhóm nhà cung cấp
            try
            {
                string sqlNhom = @"
                    IF NOT EXISTS (SELECT 1 FROM sys.tables WHERE name = N'NhomNhaCungCap')
                    BEGIN
                        CREATE TABLE NhomNhaCungCap (
                            MaNhomNcc INT IDENTITY(1,1) PRIMARY KEY,
                            TenNhom NVARCHAR(150) NOT NULL,
                            GhiChu NVARCHAR(255) NULL
                        );
                    END;
                    SELECT MaNhomNcc, TenNhom FROM NhomNhaCungCap ORDER BY TenNhom;";

                DataTable dtNhom = DataProcessing.ReadData(sqlNhom);
                DataRow drNone = dtNhom.NewRow();
                drNone["MaNhomNcc"] = 0;
                drNone["TenNhom"] = "-- Chưa phân nhóm --";
                dtNhom.Rows.InsertAt(drNone, 0);

                cboNhomNcc.DataSource = dtNhom;
                cboNhomNcc.ValueMember = "MaNhomNcc";
                cboNhomNcc.DisplayMember = "TenNhom";
                cboNhomNcc.SelectedIndex = 0;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi tải danh mục nhóm nhà cung cấp: " + ex.Message, "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

            // 2. Trạng thái
            cboTrangThai.Items.Clear();
            cboTrangThai.Items.Add(new ComboboxItem("Đang hoạt động", "HOAT_DONG"));
            cboTrangThai.Items.Add(new ComboboxItem("Ngừng hoạt động", "NGUNG_GIAO_DICH"));
            cboTrangThai.SelectedIndex = 0;
        }

        private void SinhMaNccGoiY()
        {
            try
            {
                string sql = "SELECT ISNULL(MAX(MaDonViCapHang), 0) + 1 FROM DonViCapHang";
                object val = DataProcessing.ReadValue(sql);
                int nextId = val != null && int.TryParse(val.ToString(), out int id) ? id : 1;
                txtMaNcc.Text = $"NCC{nextId:D5}";
            }
            catch
            {
                txtMaNcc.Text = "NCC00001";
            }
        }

        private void NapThongTinNhaCungCap()
        {
            try
            {
                string sql = @"
                    SELECT ncc.MaDonViCapHang, 
                           ISNULL(ncc.MaNcc, 'NCC' + RIGHT('00000' + CAST(ncc.MaDonViCapHang AS VARCHAR(10)), 5)) AS MaNcc,
                           ncc.TenDonVi, ncc.DienThoai, ncc.Email, ncc.DiaChi, ncc.KhuVuc, ncc.MaSoThue,
                           ISNULL(ncc.MaNhomNcc, 0) AS MaNhomNcc, ncc.GhiChu, ncc.TrangThai,
                           ISNULL((
                               SELECT SUM(pn.TongTien) 
                               FROM PhieuNhap pn 
                               WHERE pn.MaDonViCapHang = ncc.MaDonViCapHang 
                                 AND pn.TrangThai IN ('DA_XAC_NHAN', 'DA_NHAN')
                           ), 0) AS TongMua
                    FROM DonViCapHang ncc
                    WHERE ncc.MaDonViCapHang = @MaDonViCapHang;";

                DataTable dt = DataProcessing.ReadData(sql, new SqlParameter[] { new SqlParameter("@MaDonViCapHang", maDonViCapHangHienTai) });
                if (dt.Rows.Count > 0)
                {
                    DataRow r = dt.Rows[0];
                    txtMaNcc.Text = r["MaNcc"].ToString();
                    txtTenDonVi.Text = r["TenDonVi"].ToString();
                    txtDienThoai.Text = r["DienThoai"] != DBNull.Value ? r["DienThoai"].ToString() : "";
                    txtEmail.Text = r["Email"] != DBNull.Value ? r["Email"].ToString() : "";
                    txtMaSoThue.Text = r["MaSoThue"] != DBNull.Value ? r["MaSoThue"].ToString() : "";
                    txtKhuVuc.Text = r["KhuVuc"] != DBNull.Value ? r["KhuVuc"].ToString() : "";
                    txtDiaChi.Text = r["DiaChi"] != DBNull.Value ? r["DiaChi"].ToString() : "";
                    txtGhiChu.Text = r["GhiChu"] != DBNull.Value ? r["GhiChu"].ToString() : "";

                    // Chọn nhóm
                    if (r["MaNhomNcc"] != DBNull.Value && int.TryParse(r["MaNhomNcc"].ToString(), out int maNhom))
                    {
                        cboNhomNcc.SelectedValue = maNhom;
                    }

                    // Chọn trạng thái
                    string trangThai = r["TrangThai"] != DBNull.Value ? r["TrangThai"].ToString() : "HOAT_DONG";
                    if (trangThai == "NGUNG_GIAO_DICH" || trangThai == "NGUNG_HOAT_DONG")
                    {
                        cboTrangThai.SelectedIndex = 1;
                    }
                    else
                    {
                        cboTrangThai.SelectedIndex = 0;
                    }

                    // Hiển thị công nợ & tổng mua
                    decimal tongMua = r["TongMua"] != DBNull.Value && decimal.TryParse(r["TongMua"].ToString(), out decimal tm) ? tm : 0;
                    lblTongMuaVal.Text = $"{tongMua:N0} đ";
                    lblNoHienTaiVal.Text = $"{tongMua:N0} đ"; // Khi chưa có sổ quỹ, nợ = tổng mua
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi tải thông tin nhà cung cấp: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        #endregion

        #region Xử lý Sự kiện & Lưu dữ liệu

        private void btnLuu_Click(object sender, EventArgs e)
        {
            string tenDonVi = txtTenDonVi.Text.Trim();
            if (string.IsNullOrWhiteSpace(tenDonVi))
            {
                MessageBox.Show("Vui lòng nhập tên nhà cung cấp!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtTenDonVi.Focus();
                return;
            }

            string maNcc = txtMaNcc.Text.Trim();
            if (string.IsNullOrWhiteSpace(maNcc))
            {
                SinhMaNccGoiY();
                maNcc = txtMaNcc.Text.Trim();
            }

            try
            {
                // Kiểm tra trùng mã nhà cung cấp
                string sqlCheckMa = @"
                    SELECT COUNT(1) FROM DonViCapHang 
                    WHERE LOWER(MaNcc) = LOWER(@MaNcc) AND MaDonViCapHang <> @MaDonViCapHang";
                object valCheck = DataProcessing.ReadValue(sqlCheckMa, new SqlParameter[] {
                    new SqlParameter("@MaNcc", maNcc),
                    new SqlParameter("@MaDonViCapHang", maDonViCapHangHienTai)
                });

                if (valCheck != null && int.TryParse(valCheck.ToString(), out int tonTai) && tonTai > 0)
                {
                    MessageBox.Show($"Mã nhà cung cấp '{maNcc}' đã được sử dụng. Vui lòng nhập mã khác!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtMaNcc.Focus();
                    return;
                }

                // Nhóm nhà cung cấp
                object valMaNhom = DBNull.Value;
                if (cboNhomNcc.SelectedValue != null && int.TryParse(cboNhomNcc.SelectedValue.ToString(), out int maNhom) && maNhom > 0)
                {
                    valMaNhom = maNhom;
                }

                // Trạng thái
                string trangThai = "HOAT_DONG";
                if (cboTrangThai.SelectedItem is ComboboxItem item)
                {
                    trangThai = item.Value.ToString();
                }

                // Bọc Transaction an toàn
                DataProcessing.ChangeDataTransaction((conn, trans) =>
                {
                    if (maDonViCapHangHienTai == 0)
                    {
                        // Thêm mới
                        string sqlInsert = @"
                            INSERT INTO DonViCapHang (
                                MaNcc, TenDonVi, DienThoai, Email, MaNhomNcc,
                                MaSoThue, KhuVuc, DiaChi, GhiChu, TrangThai, NgayTao
                            )
                            VALUES (
                                @MaNcc, @TenDonVi, @DienThoai, @Email, @MaNhomNcc,
                                @MaSoThue, @KhuVuc, @DiaChi, @GhiChu, @TrangThai, GETDATE()
                            );";

                        using (SqlCommand cmd = new SqlCommand(sqlInsert, conn, trans))
                        {
                            cmd.Parameters.AddWithValue("@MaNcc", maNcc);
                            cmd.Parameters.AddWithValue("@TenDonVi", tenDonVi);
                            cmd.Parameters.AddWithValue("@DienThoai", string.IsNullOrWhiteSpace(txtDienThoai.Text) ? (object)DBNull.Value : txtDienThoai.Text.Trim());
                            cmd.Parameters.AddWithValue("@Email", string.IsNullOrWhiteSpace(txtEmail.Text) ? (object)DBNull.Value : txtEmail.Text.Trim());
                            cmd.Parameters.AddWithValue("@MaNhomNcc", valMaNhom);
                            cmd.Parameters.AddWithValue("@MaSoThue", string.IsNullOrWhiteSpace(txtMaSoThue.Text) ? (object)DBNull.Value : txtMaSoThue.Text.Trim());
                            cmd.Parameters.AddWithValue("@KhuVuc", string.IsNullOrWhiteSpace(txtKhuVuc.Text) ? (object)DBNull.Value : txtKhuVuc.Text.Trim());
                            cmd.Parameters.AddWithValue("@DiaChi", string.IsNullOrWhiteSpace(txtDiaChi.Text) ? (object)DBNull.Value : txtDiaChi.Text.Trim());
                            cmd.Parameters.AddWithValue("@GhiChu", string.IsNullOrWhiteSpace(txtGhiChu.Text) ? (object)DBNull.Value : txtGhiChu.Text.Trim());
                            cmd.Parameters.AddWithValue("@TrangThai", trangThai);
                            cmd.ExecuteNonQuery();
                        }
                    }
                    else
                    {
                        // Cập nhật
                        string sqlUpdate = @"
                            UPDATE DonViCapHang SET
                                MaNcc = @MaNcc,
                                TenDonVi = @TenDonVi,
                                DienThoai = @DienThoai,
                                Email = @Email,
                                MaNhomNcc = @MaNhomNcc,
                                MaSoThue = @MaSoThue,
                                KhuVuc = @KhuVuc,
                                DiaChi = @DiaChi,
                                GhiChu = @GhiChu,
                                TrangThai = @TrangThai
                            WHERE MaDonViCapHang = @MaDonViCapHang;";

                        using (SqlCommand cmd = new SqlCommand(sqlUpdate, conn, trans))
                        {
                            cmd.Parameters.AddWithValue("@MaNcc", maNcc);
                            cmd.Parameters.AddWithValue("@TenDonVi", tenDonVi);
                            cmd.Parameters.AddWithValue("@DienThoai", string.IsNullOrWhiteSpace(txtDienThoai.Text) ? (object)DBNull.Value : txtDienThoai.Text.Trim());
                            cmd.Parameters.AddWithValue("@Email", string.IsNullOrWhiteSpace(txtEmail.Text) ? (object)DBNull.Value : txtEmail.Text.Trim());
                            cmd.Parameters.AddWithValue("@MaNhomNcc", valMaNhom);
                            cmd.Parameters.AddWithValue("@MaSoThue", string.IsNullOrWhiteSpace(txtMaSoThue.Text) ? (object)DBNull.Value : txtMaSoThue.Text.Trim());
                            cmd.Parameters.AddWithValue("@KhuVuc", string.IsNullOrWhiteSpace(txtKhuVuc.Text) ? (object)DBNull.Value : txtKhuVuc.Text.Trim());
                            cmd.Parameters.AddWithValue("@DiaChi", string.IsNullOrWhiteSpace(txtDiaChi.Text) ? (object)DBNull.Value : txtDiaChi.Text.Trim());
                            cmd.Parameters.AddWithValue("@GhiChu", string.IsNullOrWhiteSpace(txtGhiChu.Text) ? (object)DBNull.Value : txtGhiChu.Text.Trim());
                            cmd.Parameters.AddWithValue("@TrangThai", trangThai);
                            cmd.Parameters.AddWithValue("@MaDonViCapHang", maDonViCapHangHienTai);
                            cmd.ExecuteNonQuery();
                        }
                    }
                });

                string msg = maDonViCapHangHienTai == 0 ? "Thêm mới nhà cung cấp thành công!" : "Cập nhật thông tin nhà cung cấp thành công!";
                MessageBox.Show(msg, "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi lưu nhà cung cấp: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnBoQua_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        #endregion

        #region Lớp tiện ích ComboboxItem

        private class ComboboxItem
        {
            public string Text { get; set; }
            public object Value { get; set; }

            public ComboboxItem(string text, object value)
            {
                Text = text;
                Value = value;
            }

            public override string ToString()
            {
                return Text;
            }
        }

        #endregion
    }
}
