using BTL_LTTQ_2.Classes;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace BTL_LTTQ_2.GUI.Forms
{
    public partial class frmDangNhap : Form
    {
        public frmDangNhap()
        {
            InitializeComponent();
        }

        private void frmDangNhap_Load(object sender, EventArgs e)
        {
            // Tự động kiểm tra và đồng bộ các vai trò, tài khoản demo chuẩn (quanly, thukho, nvkho)
            DongBoTaiKhoanTestMacDinh();

            // Nạp danh sách kho ban đầu
            NapDanhSachKhoMacDinh();

            // Đặt con trỏ vào ô Tên đăng nhập
            txtTenDangNhap.Focus();
        }

        /// <summary>
        /// Tự động kiểm tra và khởi tạo dữ liệu tài khoản mẫu (quanly, thukho, nvkho / 123456)
        /// Giúp người dùng có thể đăng nhập ngay mà không bắt buộc phải chạy script SQL thủ công
        /// </summary>
        private void DongBoTaiKhoanTestMacDinh()
        {
            try
            {
                // 1. Đồng bộ 3 vai trò chuẩn trong bảng VaiTro: Quản lý (1), Thủ kho (2), Nhân viên kho (3)
                string sqlVaiTro = @"
                    IF NOT EXISTS (SELECT 1 FROM VaiTro WHERE MaVaiTro = 1)
                    BEGIN
                        SET IDENTITY_INSERT VaiTro ON;
                        INSERT INTO VaiTro (MaVaiTro, TenVaiTro, MoTa) VALUES (1, N'Quản lý', N'Quản lý toàn bộ chi nhánh và mọi kho hàng');
                        SET IDENTITY_INSERT VaiTro OFF;
                    END
                    ELSE
                        UPDATE VaiTro SET TenVaiTro = N'Quản lý', MoTa = N'Quản lý toàn bộ chi nhánh và mọi kho hàng' WHERE MaVaiTro = 1;

                    IF NOT EXISTS (SELECT 1 FROM VaiTro WHERE MaVaiTro = 2)
                    BEGIN
                        SET IDENTITY_INSERT VaiTro ON;
                        INSERT INTO VaiTro (MaVaiTro, TenVaiTro, MoTa) VALUES (2, N'Thủ kho', N'Quản lý 1 chi nhánh kho duy nhất được phân công');
                        SET IDENTITY_INSERT VaiTro OFF;
                    END
                    ELSE
                        UPDATE VaiTro SET TenVaiTro = N'Thủ kho', MoTa = N'Quản lý 1 chi nhánh kho duy nhất được phân công' WHERE MaVaiTro = 2;

                    IF NOT EXISTS (SELECT 1 FROM VaiTro WHERE MaVaiTro = 3)
                    BEGIN
                        SET IDENTITY_INSERT VaiTro ON;
                        INSERT INTO VaiTro (MaVaiTro, TenVaiTro, MoTa) VALUES (3, N'Nhân viên kho', N'Nhân viên thực hiện thao tác nghiệp vụ tại kho');
                        SET IDENTITY_INSERT VaiTro OFF;
                    END
                    ELSE
                        UPDATE VaiTro SET TenVaiTro = N'Nhân viên kho', MoTa = N'Nhân viên thực hiện thao tác nghiệp vụ tại kho' WHERE MaVaiTro = 3;";

                DataProcessing.ChangeData(sqlVaiTro);

                // Hash SHA-256 của mật khẩu mặc định "123456"
                string passHash = PasswordHelper.HashPassword("123456");

                // 2. Đảm bảo tài khoản "nvkho" tồn tại trong bảng TaiKhoan
                string sqlCheckNvkho = "SELECT MaTaiKhoan FROM TaiKhoan WHERE TenDangNhap = 'nvkho'";
                object objNvk = DataProcessing.ReadValue(sqlCheckNvkho);

                if (objNvk == null || objNvk == DBNull.Value)
                {
                    // Lấy hoặc tạo nhân viên số 3
                    string sqlInsertNvkho = @"
                        DECLARE @maNv INT = (SELECT TOP 1 MaNV FROM NhanVien WHERE DienThoai = '0901000003');
                        IF @maNv IS NULL
                        BEGIN
                            SET @maNv = (SELECT TOP 1 MaNV FROM NhanVien ORDER BY MaNV DESC);
                        END
                        IF @maNv IS NULL SET @maNv = 1;

                        INSERT INTO TaiKhoan (MaNV, MaVaiTro, TenDangNhap, MatKhauHash, TrangThai)
                        VALUES (@maNv, 3, 'nvkho', @PassHash, N'HOAT_DONG');

                        DECLARE @newId INT = SCOPE_IDENTITY();
                        DECLARE @khoDau INT = (SELECT TOP 1 MaKho FROM Kho WHERE TrangThai = 'HOAT_DONG' ORDER BY MaKho);
                        IF @khoDau IS NOT NULL AND NOT EXISTS (SELECT 1 FROM TaiKhoanKho WHERE MaTaiKhoan = @newId)
                        BEGIN
                            INSERT INTO TaiKhoanKho (MaTaiKhoan, MaKho) VALUES (@newId, @khoDau);
                        END";

                    SqlParameter[] p = new SqlParameter[]
                    {
                        new SqlParameter("@PassHash", passHash)
                    };
                    DataProcessing.ChangeData(sqlInsertNvkho, p);
                }
                else
                {
                    // Đảm bảo mật khẩu và trạng thái hoạt động của nvkho
                    int maTkNvk = Convert.ToInt32(objNvk);
                    string sqlUpdateNvkho = @"
                        UPDATE TaiKhoan 
                        SET MatKhauHash = @PassHash, TrangThai = N'HOAT_DONG', MaVaiTro = 3 
                        WHERE MaTaiKhoan = @MaTaiKhoan;

                        DECLARE @khoDau INT = (SELECT TOP 1 MaKho FROM Kho WHERE TrangThai = 'HOAT_DONG' ORDER BY MaKho);
                        IF @khoDau IS NOT NULL AND NOT EXISTS (SELECT 1 FROM TaiKhoanKho WHERE MaTaiKhoan = @MaTaiKhoan)
                        BEGIN
                            INSERT INTO TaiKhoanKho (MaTaiKhoan, MaKho) VALUES (@MaTaiKhoan, @khoDau);
                        END";

                    SqlParameter[] p = new SqlParameter[]
                    {
                        new SqlParameter("@PassHash", passHash),
                        new SqlParameter("@MaTaiKhoan", maTkNvk)
                    };
                    DataProcessing.ChangeData(sqlUpdateNvkho, p);
                }

                // 3. Đảm bảo tài khoản "quanly" và "thukho" cũng có mật khẩu băm đúng và đang hoạt động
                string sqlEnsureOtherAccounts = @"
                    UPDATE TaiKhoan 
                    SET MatKhauHash = @PassHash, TrangThai = N'HOAT_DONG' 
                    WHERE TenDangNhap IN ('quanly', 'thukho') AND (MatKhauHash IS NULL OR MatKhauHash = '');";
                DataProcessing.ChangeData(sqlEnsureOtherAccounts, new SqlParameter[] { new SqlParameter("@PassHash", passHash) });
            }
            catch
            {
                // Bỏ qua lỗi nếu database có cấu hình bảo mật riêng
            }
        }

        /// <summary>
        /// Nạp toàn bộ danh sách kho đang hoạt động vào ComboBox
        /// </summary>
        private void NapDanhSachKhoMacDinh()
        {
            try
            {
                string sqlKho = "SELECT MaKho, TenKho FROM Kho WHERE TrangThai = 'HOAT_DONG' ORDER BY MaKho";
                DataTable dtKho = DataProcessing.ReadData(sqlKho);

                // Thêm lựa chọn "Tất cả kho" ở đầu danh sách cho tài khoản Quản lý
                DataTable dtHienThi = dtKho.Clone();
                DataRow drAll = dtHienThi.NewRow();
                drAll["MaKho"] = 0;
                drAll["TenKho"] = "Tất cả kho";
                dtHienThi.Rows.Add(drAll);

                foreach (DataRow row in dtKho.Rows)
                {
                    dtHienThi.ImportRow(row);
                }

                cboKhoLamViec.DataSource = dtHienThi;
                cboKhoLamViec.DisplayMember = "TenKho";
                cboKhoLamViec.ValueMember = "MaKho";
                cboKhoLamViec.SelectedIndex = 0;
                cboKhoLamViec.Enabled = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Không thể tải danh sách chi nhánh kho: " + ex.Message, "Lỗi kết nối", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Khi người dùng nhập tên đăng nhập và chuyển sang ô khác, kiểm tra vai trò để nạp kho phù hợp
        /// </summary>
        private void txtTenDangNhap_Leave(object sender, EventArgs e)
        {
            CapNhatKhoTheoTaiKhoan();
        }

        private void CapNhatKhoTheoTaiKhoan()
        {
            string tenDangNhap = txtTenDangNhap.Text.Trim();
            if (string.IsNullOrEmpty(tenDangNhap))
            {
                NapDanhSachKhoMacDinh();
                return;
            }

            try
            {
                string sql = @"
                    SELECT tk.MaTaiKhoan, tk.MaVaiTro, vt.TenVaiTro, tk.TrangThai
                    FROM TaiKhoan tk
                    LEFT JOIN VaiTro vt ON tk.MaVaiTro = vt.MaVaiTro
                    WHERE tk.TenDangNhap = @TenDangNhap";

                SqlParameter[] paras = new SqlParameter[]
                {
                    new SqlParameter("@TenDangNhap", tenDangNhap)
                };

                DataTable dt = DataProcessing.ReadData(sql, paras);
                if (dt != null && dt.Rows.Count > 0)
                {
                    int maTaiKhoan = Convert.ToInt32(dt.Rows[0]["MaTaiKhoan"]);
                    int maVaiTro = Convert.ToInt32(dt.Rows[0]["MaVaiTro"]);
                    string tenVaiTro = dt.Rows[0]["TenVaiTro"]?.ToString() ?? string.Empty;

                    bool laQuanLy = (maVaiTro == 1 || tenVaiTro.IndexOf("Quản lý", StringComparison.OrdinalIgnoreCase) >= 0);
                    bool laThuKho = (maVaiTro == 2 || tenVaiTro.IndexOf("Thủ kho", StringComparison.OrdinalIgnoreCase) >= 0);

                    if (laQuanLy)
                    {
                        // Vai trò 1: Quản lý (Quản lý mọi kho hàng)
                        NapDanhSachKhoMacDinh();
                    }
                    else if (laThuKho)
                    {
                        // Vai trò 2: Thủ kho (Quản lý 1 kho duy nhất)
                        NapKhoPhanCong(maTaiKhoan, true);
                    }
                    else
                    {
                        // Vai trò 3: Nhân viên kho
                        NapKhoPhanCong(maTaiKhoan, false);
                    }
                }
                else
                {
                    NapDanhSachKhoMacDinh();
                }
            }
            catch
            {
                // Giữ trạng thái mặc định nếu truy vấn gặp lỗi
                NapDanhSachKhoMacDinh();
            }
        }

        /// <summary>
        /// Nạp các chi nhánh kho được phân công cho tài khoản từ bảng TaiKhoanKho
        /// </summary>
        private void NapKhoPhanCong(int maTaiKhoan, bool laThuKhoDuyNhat)
        {
            string sqlKho = @"
                SELECT k.MaKho, k.TenKho
                FROM TaiKhoanKho tkk
                JOIN Kho k ON tkk.MaKho = k.MaKho
                WHERE tkk.MaTaiKhoan = @MaTaiKhoan AND k.TrangThai = 'HOAT_DONG'
                ORDER BY k.MaKho";

            SqlParameter[] paras = new SqlParameter[]
            {
                new SqlParameter("@MaTaiKhoan", maTaiKhoan)
            };

            DataTable dtKho = DataProcessing.ReadData(sqlKho, paras);
            if (dtKho == null || dtKho.Rows.Count == 0)
            {
                // Nếu chưa được gán kho cụ thể trong TaiKhoanKho, lấy kho hoạt động đầu tiên
                string sqlDefault = "SELECT TOP 1 MaKho, TenKho FROM Kho WHERE TrangThai = 'HOAT_DONG' ORDER BY MaKho";
                dtKho = DataProcessing.ReadData(sqlDefault);
            }

            cboKhoLamViec.DataSource = dtKho;
            cboKhoLamViec.DisplayMember = "TenKho";
            cboKhoLamViec.ValueMember = "MaKho";

            if (dtKho != null && dtKho.Rows.Count > 0)
            {
                cboKhoLamViec.SelectedIndex = 0;
            }

            // Nếu là Thủ kho (quản lý 1 kho duy nhất) hoặc chỉ có 1 kho được phân công: khóa ComboBox không cho đổi
            if (laThuKhoDuyNhat || (dtKho != null && dtKho.Rows.Count <= 1))
            {
                cboKhoLamViec.Enabled = false;
            }
            else
            {
                cboKhoLamViec.Enabled = true;
            }
        }

        private void chkHienMatKhau_CheckedChanged(object sender, EventArgs e)
        {
            txtMatKhau.UseSystemPasswordChar = !chkHienMatKhau.Checked;
        }

        private void txtTenDangNhap_TextChanged(object sender, EventArgs e)
        {
            string ten = txtTenDangNhap.Text.Trim();
            if (ten.Length >= 4)
            {
                CapNhatKhoTheoTaiKhoan();
            }
        }

        private void txtTenDangNhap_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;
                CapNhatKhoTheoTaiKhoan();
                txtMatKhau.Focus();
            }
        }

        private void txtMatKhau_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;
                btnDangNhap.PerformClick();
            }
        }

        private void btnDangNhap_Click(object sender, EventArgs e)
        {
            string tenDangNhap = txtTenDangNhap.Text.Trim();
            string matKhau = txtMatKhau.Text;

            // Kiểm tra rỗng
            if (string.IsNullOrEmpty(tenDangNhap))
            {
                MessageBox.Show("Vui lòng nhập tên đăng nhập.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtTenDangNhap.Focus();
                return;
            }

            if (string.IsNullOrEmpty(matKhau))
            {
                MessageBox.Show("Vui lòng nhập mật khẩu.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtMatKhau.Focus();
                return;
            }

            try
            {
                // Truy vấn thông tin tài khoản bằng SqlParameter chống SQL Injection 100%
                string sql = @"
                    SELECT tk.MaTaiKhoan, tk.TenDangNhap, tk.MatKhauHash, tk.MaVaiTro, tk.MaNV, tk.TrangThai,
                           vt.TenVaiTro, ISNULL(nv.HoTen, tk.TenDangNhap) AS HoTen
                    FROM TaiKhoan tk
                    LEFT JOIN VaiTro vt ON tk.MaVaiTro = vt.MaVaiTro
                    LEFT JOIN NhanVien nv ON tk.MaNV = nv.MaNV
                    WHERE tk.TenDangNhap = @TenDangNhap";

                SqlParameter[] paras = new SqlParameter[]
                {
                    new SqlParameter("@TenDangNhap", tenDangNhap)
                };

                DataTable dt = DataProcessing.ReadData(sql, paras);
                if (dt == null || dt.Rows.Count == 0)
                {
                    MessageBox.Show("Tên đăng nhập hoặc mật khẩu không chính xác.", "Lỗi đăng nhập", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtMatKhau.Focus();
                    txtMatKhau.SelectAll();
                    return;
                }

                DataRow userRow = dt.Rows[0];
                string dbHash = userRow["MatKhauHash"]?.ToString() ?? string.Empty;

                // Xác thực mật khẩu qua PasswordHelper SHA-256
                if (!PasswordHelper.VerifyPassword(matKhau, dbHash))
                {
                    MessageBox.Show("Tên đăng nhập hoặc mật khẩu không chính xác.", "Lỗi đăng nhập", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtMatKhau.Focus();
                    txtMatKhau.SelectAll();
                    return;
                }

                // Kiểm tra trạng thái tài khoản
                string trangThai = userRow["TrangThai"]?.ToString() ?? string.Empty;
                if (!string.Equals(trangThai, "HOAT_DONG", StringComparison.OrdinalIgnoreCase))
                {
                    MessageBox.Show("Tài khoản này hiện đang bị khóa hoặc ngưng hoạt động.\nVui lòng liên hệ Quản trị viên để được hỗ trợ.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                int maVaiTro = Convert.ToInt32(userRow["MaVaiTro"]);
                string tenVaiTro = userRow["TenVaiTro"]?.ToString() ?? string.Empty;
                bool laQuanLy = (maVaiTro == 1 || tenVaiTro.IndexOf("Quản lý", StringComparison.OrdinalIgnoreCase) >= 0);

                // Nếu tài khoản không phải Quản lý mà ComboBox đang chọn "Tất cả kho" (maKho = 0):
                // Tự động nhận diện và nạp lại kho hợp lệ của tài khoản
                int maKhoChon = (cboKhoLamViec.SelectedValue != null) ? Convert.ToInt32(cboKhoLamViec.SelectedValue) : 0;
                if (!laQuanLy && maKhoChon == 0)
                {
                    NapKhoPhanCong(Convert.ToInt32(userRow["MaTaiKhoan"]), (maVaiTro == 2 || tenVaiTro.IndexOf("Thủ kho", StringComparison.OrdinalIgnoreCase) >= 0));
                    maKhoChon = (cboKhoLamViec.SelectedValue != null) ? Convert.ToInt32(cboKhoLamViec.SelectedValue) : 0;
                }

                // Kiểm tra lại kho làm việc
                if (cboKhoLamViec.SelectedValue == null || (maKhoChon == 0 && !laQuanLy))
                {
                    MessageBox.Show("Chỉ vai trò Quản lý mới được phép thao tác ở chế độ 'Tất cả kho'.\nVui lòng chọn một kho chi nhánh cụ thể.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    cboKhoLamViec.Focus();
                    return;
                }

                string tenKhoChon = cboKhoLamViec.Text;

                // Thiết lập phiên làm việc toàn cục SessionContext
                SessionContext.MaTaiKhoan = Convert.ToInt32(userRow["MaTaiKhoan"]);
                SessionContext.TenDangNhap = userRow["TenDangNhap"].ToString();
                SessionContext.MaNV = userRow["MaNV"] != DBNull.Value ? (int?)Convert.ToInt32(userRow["MaNV"]) : null;
                SessionContext.HoTen = userRow["HoTen"].ToString();
                SessionContext.MaVaiTro = maVaiTro;
                SessionContext.TenVaiTro = string.IsNullOrEmpty(tenVaiTro) ? "Chưa rõ" : tenVaiTro;
                SessionContext.MaKhoHienTai = maKhoChon;
                SessionContext.TenKhoHienTai = tenKhoChon;
                SessionContext.IsDangXuat = false;

                // Đăng nhập thành công, đóng form trả về OK
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi kết nối cơ sở dữ liệu khi đăng nhập: " + ex.Message, "Lỗi hệ thống", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }
    }
}
