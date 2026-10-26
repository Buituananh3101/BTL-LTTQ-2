using BTL_LTTQ_2.Classes;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Security.Cryptography;
using System.Text;
using System.Windows.Forms;

namespace BTL_LTTQ_2.GUI.Forms.DangNhap
{
    public partial class frmDangNhap : Form
    {
        private string _tenDangNhapHienTai = string.Empty;

        public frmDangNhap()
        {
            InitializeComponent();
            txtTenDangNhap.Leave += txtTenDangNhap_Leave;
        }

        private void frmDangNhap_Load(object sender, EventArgs e)
        {
            NapDanhSachKhoMacDinh();
            txtTenDangNhap.Focus();
        }

        /// <summary>
        /// Nạp toàn bộ danh sách kho đang hoạt động từ CSDL kèm lựa chọn 'Tất cả kho'
        /// </summary>
        private void NapDanhSachKhoMacDinh()
        {
            try
            {
                string sql = "SELECT MaKho, TenKho FROM Kho WHERE TrangThai = 'HOAT_DONG' OR TrangThai = 'ACTIVE' ORDER BY MaKho ASC";
                DataTable dt = DataProcessing.ReadData(sql);

                if (dt == null)
                {
                    dt = new DataTable();
                    dt.Columns.Add("MaKho", typeof(int));
                    dt.Columns.Add("TenKho", typeof(string));
                }

                DataRow rowTatCa = dt.NewRow();
                rowTatCa["MaKho"] = 0;
                rowTatCa["TenKho"] = "Tất cả kho";
                dt.Rows.InsertAt(rowTatCa, 0);

                cboKhoLamViec.DataSource = dt;
                cboKhoLamViec.DisplayMember = "TenKho";
                cboKhoLamViec.ValueMember = "MaKho";
                cboKhoLamViec.SelectedIndex = 0;
                cboKhoLamViec.Enabled = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi nạp danh sách kho: " + ex.Message, "Lỗi kết nối", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Tự động nạp danh sách kho phù hợp theo vai trò của tài khoản khi người dùng nhập tên đăng nhập
        /// </summary>
        private void CapNhatDanhSachKhoTheoTaiKhoan(string tenDangNhap)
        {
            if (string.IsNullOrWhiteSpace(tenDangNhap))
            {
                _tenDangNhapHienTai = string.Empty;
                NapDanhSachKhoMacDinh();
                return;
            }

            if (string.Equals(_tenDangNhapHienTai, tenDangNhap, StringComparison.OrdinalIgnoreCase))
            {
                return;
            }

            _tenDangNhapHienTai = tenDangNhap;

            try
            {
                string sqlUser = @"
                    SELECT tk.MaTaiKhoan, tk.MaVaiTro, vt.TenVaiTro 
                    FROM TaiKhoan tk 
                    INNER JOIN VaiTro vt ON tk.MaVaiTro = vt.MaVaiTro 
                    WHERE tk.TenDangNhap = @TenDangNhap";

                SqlParameter[] paras = { new SqlParameter("@TenDangNhap", tenDangNhap) };
                DataTable dtUser = DataProcessing.ReadData(sqlUser, paras);

                if (dtUser == null || dtUser.Rows.Count == 0)
                {
                    // Tài khoản chưa tìm thấy trong CSDL -> giữ danh sách mặc định để người dùng tiếp tục thao tác
                    NapDanhSachKhoMacDinh();
                    return;
                }

                int maVaiTro = Convert.ToInt32(dtUser.Rows[0]["MaVaiTro"]);
                int maTaiKhoan = Convert.ToInt32(dtUser.Rows[0]["MaTaiKhoan"]);

                if (maVaiTro == 1)
                {
                    // Vai trò Quản lý: Toàn quyền truy cập mọi kho + Tất cả kho
                    NapDanhSachKhoMacDinh();
                }
                else
                {
                    // Vai trò Thủ kho hoặc Nhân viên kho: Chỉ hiển thị các kho được phân công trong TaiKhoanKho
                    string sqlKho = @"
                        SELECT k.MaKho, k.TenKho 
                        FROM Kho k 
                        INNER JOIN TaiKhoanKho tkk ON k.MaKho = tkk.MaKho 
                        WHERE tkk.MaTaiKhoan = @MaTaiKhoan 
                          AND (k.TrangThai = 'HOAT_DONG' OR k.TrangThai = 'ACTIVE') 
                        ORDER BY k.MaKho ASC";

                    SqlParameter[] pKho = { new SqlParameter("@MaTaiKhoan", maTaiKhoan) };
                    DataTable dtKho = DataProcessing.ReadData(sqlKho, pKho);

                    if (dtKho != null && dtKho.Rows.Count > 0)
                    {
                        cboKhoLamViec.DataSource = dtKho;
                        cboKhoLamViec.DisplayMember = "TenKho";
                        cboKhoLamViec.ValueMember = "MaKho";
                        cboKhoLamViec.SelectedIndex = 0;

                        // Nếu chỉ có 1 kho phân công thì cố định kho đó, không cho đổi
                        cboKhoLamViec.Enabled = dtKho.Rows.Count > 1;
                    }
                    else
                    {
                        // Tài khoản chưa được phân công kho nào
                        DataTable dtTrong = new DataTable();
                        dtTrong.Columns.Add("MaKho", typeof(int));
                        dtTrong.Columns.Add("TenKho", typeof(string));
                        dtTrong.Rows.Add(-1, "(Chưa được phân công kho)");

                        cboKhoLamViec.DataSource = dtTrong;
                        cboKhoLamViec.DisplayMember = "TenKho";
                        cboKhoLamViec.ValueMember = "MaKho";
                        cboKhoLamViec.SelectedIndex = 0;
                        cboKhoLamViec.Enabled = false;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi kiểm tra quyền kho: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void chkHienMatKhau_CheckedChanged(object sender, EventArgs e)
        {
            txtMatKhau.UseSystemPasswordChar = !chkHienMatKhau.Checked;
        }

        private void txtTenDangNhap_Leave(object sender, EventArgs e)
        {
            CapNhatDanhSachKhoTheoTaiKhoan(txtTenDangNhap.Text.Trim());
        }

        private void txtTenDangNhap_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;
                CapNhatDanhSachKhoTheoTaiKhoan(txtTenDangNhap.Text.Trim());
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
                // Đồng bộ lại danh sách kho theo tài khoản trước khi xác thực
                CapNhatDanhSachKhoTheoTaiKhoan(tenDangNhap);

                string sql = @"
                    SELECT tk.MaTaiKhoan, tk.TenDangNhap, tk.MatKhauHash, tk.TrangThai, 
                           tk.MaVaiTro, vt.TenVaiTro, tk.MaNV, nv.HoTen 
                    FROM TaiKhoan tk 
                    INNER JOIN VaiTro vt ON tk.MaVaiTro = vt.MaVaiTro 
                    LEFT JOIN NhanVien nv ON tk.MaNV = nv.MaNV 
                    WHERE tk.TenDangNhap = @TenDangNhap";

                SqlParameter[] paras = { new SqlParameter("@TenDangNhap", tenDangNhap) };
                DataTable dt = DataProcessing.ReadData(sql, paras);

                if (dt == null || dt.Rows.Count == 0)
                {
                    MessageBox.Show("Tên đăng nhập hoặc mật khẩu không chính xác.", "Lỗi đăng nhập", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtMatKhau.Clear();
                    txtMatKhau.Focus();
                    return;
                }

                DataRow row = dt.Rows[0];
                string dbMatKhau = row["MatKhauHash"] != DBNull.Value ? row["MatKhauHash"].ToString() : string.Empty;

                // Xác thực mật khẩu: Hỗ trợ so sánh trực tiếp chuỗi thuần (Plain-text) và SHA-256 đối với tài khoản mẫu hệ thống
                bool hopLe = (matKhau == dbMatKhau) || string.Equals(TinhSha256(matKhau), dbMatKhau, StringComparison.OrdinalIgnoreCase);

                if (!hopLe)
                {
                    MessageBox.Show("Tên đăng nhập hoặc mật khẩu không chính xác.", "Lỗi đăng nhập", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtMatKhau.Clear();
                    txtMatKhau.Focus();
                    return;
                }

                // Kiểm tra trạng thái hoạt động của tài khoản
                string trangThai = row["TrangThai"] != DBNull.Value ? row["TrangThai"].ToString() : string.Empty;
                if (!string.Equals(trangThai, "HOAT_DONG", StringComparison.OrdinalIgnoreCase) && 
                    !string.Equals(trangThai, "ACTIVE", StringComparison.OrdinalIgnoreCase))
                {
                    MessageBox.Show("Tài khoản đang bị khóa hoặc ngừng hoạt động.\nVui lòng liên hệ Quản lý để được hỗ trợ.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // Kiểm tra kho làm việc được chọn
                if (cboKhoLamViec.SelectedValue == null)
                {
                    MessageBox.Show("Vui lòng chọn kho làm việc.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    cboKhoLamViec.Focus();
                    return;
                }

                int maKho = Convert.ToInt32(cboKhoLamViec.SelectedValue);
                string tenKho = cboKhoLamViec.Text;
                int maVaiTro = Convert.ToInt32(row["MaVaiTro"]);

                if (maKho < 0)
                {
                    MessageBox.Show("Tài khoản của bạn chưa được phân công kho làm việc hợp lệ.\nVui lòng liên hệ Quản lý.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (maVaiTro != 1 && maKho == 0)
                {
                    MessageBox.Show("Chỉ vai trò Quản lý mới có quyền truy cập 'Tất cả kho'.\nVui lòng chọn một kho cụ thể được phân công.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    cboKhoLamViec.Focus();
                    return;
                }

                // Cập nhật thời điểm đăng nhập cuối cùng
                try
                {
                    string sqlUpdate = "UPDATE TaiKhoan SET LanDangNhapCuoi = GETDATE() WHERE MaTaiKhoan = @MaTaiKhoan";
                    DataProcessing.ChangeData(sqlUpdate, new SqlParameter[] { new SqlParameter("@MaTaiKhoan", Convert.ToInt32(row["MaTaiKhoan"])) });
                }
                catch
                {
                    // Bỏ qua lỗi ghi thời gian nếu có sự cố nhỏ
                }

                // Thiết lập thông tin phiên làm việc toàn hệ thống
                SessionContext.MaTaiKhoan = Convert.ToInt32(row["MaTaiKhoan"]);
                SessionContext.TenDangNhap = row["TenDangNhap"].ToString();
                SessionContext.MaNV = row["MaNV"] != DBNull.Value ? (int?)Convert.ToInt32(row["MaNV"]) : null;
                SessionContext.HoTen = row["HoTen"] != DBNull.Value ? row["HoTen"].ToString() : row["TenDangNhap"].ToString();
                SessionContext.MaVaiTro = maVaiTro;
                SessionContext.TenVaiTro = row["TenVaiTro"].ToString();
                SessionContext.MaKhoHienTai = maKho;
                SessionContext.TenKhoHienTai = tenKho;
                SessionContext.IsDangXuat = false;

                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi kết nối hoặc xử lý đăng nhập: " + ex.Message, "Lỗi hệ thống", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        /// <summary>
        /// Hàm băm SHA-256 phục vụ xác thực đối chiếu với dữ liệu mẫu trong CSDL
        /// </summary>
        private static string TinhSha256(string chuoi)
        {
            if (string.IsNullOrEmpty(chuoi)) return string.Empty;
            using (SHA256 sha256 = SHA256.Create())
            {
                byte[] bytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(chuoi));
                StringBuilder sb = new StringBuilder();
                for (int i = 0; i < bytes.Length; i++)
                {
                    sb.Append(bytes[i].ToString("x2"));
                }
                return sb.ToString();
            }
        }
    }
}
