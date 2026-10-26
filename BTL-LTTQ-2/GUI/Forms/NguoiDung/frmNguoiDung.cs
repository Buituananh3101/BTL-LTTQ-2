using BTL_LTTQ_2.Classes;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace BTL_LTTQ_2.GUI.Forms.NguoiDung
{
    /// <summary>
    /// Form tạo mới người dùng: thêm Nhân viên + Tài khoản + Phân quyền kho trong một giao dịch
    /// </summary>
    public partial class frmNguoiDung : Form
    {
        // Cờ chặn xử lý sự kiện đổi vai trò trong lúc đang nạp dữ liệu
        private bool dangNapDuLieu = false;
        private int vaiTroTruoc = -1;

        public frmNguoiDung()
        {
            InitializeComponent();
        }

        private void frmNguoiDung_Load(object sender, EventArgs e)
        {
            dangNapDuLieu = true;
            NapDanhSachKho();

            lblTieuDe.Text = "Tạo người dùng";
            cboVaiTro.SelectedIndex = 1; // Mặc định Thủ kho
            cboTrangThai.SelectedIndex = 0; // Đang hoạt động

            dangNapDuLieu = false;
            vaiTroTruoc = cboVaiTro.SelectedIndex;
            CapNhatPhanQuyenKhoTheoVaiTro();
        }

        /// <summary>
        /// Nạp danh sách kho đang hoạt động từ CSDL vào danh sách phân quyền
        /// </summary>
        private void NapDanhSachKho()
        {
            try
            {
                clbDanhSachKho.Items.Clear();
                clbDanhSachKho.DisplayMember = "TenKho";

                string sqlKho = "SELECT MaKho, TenKho FROM Kho WHERE TrangThai = 'HOAT_DONG' ORDER BY MaKho ASC";
                DataTable dtKho = DataProcessing.ReadData(sqlKho);
                if (dtKho == null) return;

                // Mỗi mục là một DataRowView (MaKho, TenKho) để lấy đúng mã kho khi lưu
                foreach (DataRowView drv in dtKho.DefaultView)
                {
                    clbDanhSachKho.Items.Add(drv, false);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi nạp danh sách kho: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Lấy danh sách mã kho đang được tick trong danh sách phân quyền
        /// </summary>
        private List<int> LayDanhSachKhoDuocChon()
        {
            List<int> ds = new List<int>();
            for (int i = 0; i < clbDanhSachKho.Items.Count; i++)
            {
                if (clbDanhSachKho.GetItemChecked(i) && clbDanhSachKho.Items[i] is DataRowView drv)
                {
                    ds.Add(Convert.ToInt32(drv["MaKho"]));
                }
            }
            return ds;
        }

        private void chkHienMatKhau_CheckedChanged(object sender, EventArgs e)
        {
            txtMatKhau.UseSystemPasswordChar = !chkHienMatKhau.Checked;
        }

        private void cboVaiTro_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (dangNapDuLieu) return;

            // Chuyển từ Quản lý sang vai trò khác: bỏ tick toàn bộ kho để phân quyền lại
            if (vaiTroTruoc == 0 && cboVaiTro.SelectedIndex != 0)
            {
                for (int i = 0; i < clbDanhSachKho.Items.Count; i++)
                {
                    clbDanhSachKho.SetItemChecked(i, false);
                }
            }

            vaiTroTruoc = cboVaiTro.SelectedIndex;
            CapNhatPhanQuyenKhoTheoVaiTro();
        }

        private void CapNhatPhanQuyenKhoTheoVaiTro()
        {
            bool laQuanLy = cboVaiTro.SelectedIndex == 0;
            if (laQuanLy)
            {
                for (int i = 0; i < clbDanhSachKho.Items.Count; i++)
                {
                    clbDanhSachKho.SetItemChecked(i, true);
                }
                clbDanhSachKho.Enabled = false;
            }
            else
            {
                clbDanhSachKho.Enabled = true;
            }
        }

        private void btnBoQua_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        /// <summary>
        /// Kiểm tra dữ liệu đầu vào trước khi tạo người dùng
        /// </summary>
        private bool KiemTraHopLe(int maVaiTro, List<int> dsKho)
        {
            string tenDangNhap = txtTenDangNhap.Text.Trim();
            if (string.IsNullOrEmpty(tenDangNhap))
            {
                MessageBox.Show("Vui lòng nhập tên đăng nhập.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtTenDangNhap.Focus();
                return false;
            }

            if (tenDangNhap.Length > 50)
            {
                MessageBox.Show("Tên đăng nhập tối đa 50 ký tự.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtTenDangNhap.Focus();
                return false;
            }

            if (string.IsNullOrEmpty(txtMatKhau.Text))
            {
                MessageBox.Show("Vui lòng nhập mật khẩu.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtMatKhau.Focus();
                return false;
            }

            if (txtMatKhau.Text.Length < 6)
            {
                MessageBox.Show("Mật khẩu phải có tối thiểu 6 ký tự.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtMatKhau.Focus();
                return false;
            }

            if (string.IsNullOrWhiteSpace(txtHoTen.Text))
            {
                MessageBox.Show("Vui lòng nhập họ và tên nhân viên.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtHoTen.Focus();
                return false;
            }

            if (cboVaiTro.SelectedIndex < 0)
            {
                MessageBox.Show("Vui lòng chọn vai trò.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                cboVaiTro.Focus();
                return false;
            }

            // Quy tắc phân quyền kho: Quản lý = mọi kho, Thủ kho = đúng 1 kho, Nhân viên kho = tối thiểu 1 kho
            if (maVaiTro == 2 && dsKho.Count != 1)
            {
                MessageBox.Show("Thủ kho phải được phân công đúng 1 kho.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                clbDanhSachKho.Focus();
                return false;
            }

            if (maVaiTro == 3 && dsKho.Count == 0)
            {
                MessageBox.Show("Nhân viên kho phải được phân công ít nhất 1 kho.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                clbDanhSachKho.Focus();
                return false;
            }

            // Tên đăng nhập không được trùng (không phân biệt hoa thường)
            object soTk = DataProcessing.ReadValue("SELECT COUNT(1) FROM TaiKhoan WHERE LOWER(TenDangNhap) = LOWER(@TenDangNhap)",
                new SqlParameter[] { new SqlParameter("@TenDangNhap", tenDangNhap) });
            if (soTk != null && soTk != DBNull.Value && Convert.ToInt32(soTk) > 0)
            {
                MessageBox.Show($"Tên đăng nhập '{tenDangNhap}' đã tồn tại. Vui lòng chọn tên khác.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtTenDangNhap.Focus();
                return false;
            }

            // Điện thoại / Email là duy nhất trong bảng NhanVien (chỉ cho phép một giá trị rỗng)
            string dienThoai = txtDienThoai.Text.Trim();
            string sqlDt = string.IsNullOrEmpty(dienThoai)
                ? "SELECT COUNT(1) FROM NhanVien WHERE DienThoai IS NULL"
                : "SELECT COUNT(1) FROM NhanVien WHERE DienThoai = @GiaTri";
            object soDt = DataProcessing.ReadValue(sqlDt, new SqlParameter[] { new SqlParameter("@GiaTri", dienThoai) });
            if (soDt != null && soDt != DBNull.Value && Convert.ToInt32(soDt) > 0)
            {
                MessageBox.Show(string.IsNullOrEmpty(dienThoai)
                    ? "Đã có nhân viên khác để trống số điện thoại. Vui lòng nhập số điện thoại."
                    : "Số điện thoại đã được sử dụng bởi nhân viên khác.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtDienThoai.Focus();
                return false;
            }

            string email = txtEmail.Text.Trim();
            string sqlEmail = string.IsNullOrEmpty(email)
                ? "SELECT COUNT(1) FROM NhanVien WHERE Email IS NULL"
                : "SELECT COUNT(1) FROM NhanVien WHERE Email = @GiaTri";
            object soEmail = DataProcessing.ReadValue(sqlEmail, new SqlParameter[] { new SqlParameter("@GiaTri", email) });
            if (soEmail != null && soEmail != DBNull.Value && Convert.ToInt32(soEmail) > 0)
            {
                MessageBox.Show(string.IsNullOrEmpty(email)
                    ? "Đã có nhân viên khác để trống email. Vui lòng nhập email."
                    : "Email đã được sử dụng bởi nhân viên khác.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtEmail.Focus();
                return false;
            }

            return true;
        }

        private void btnLuu_Click(object sender, EventArgs e)
        {
            try
            {
                // Vai trò theo combobox: 0 = Quản lý (1), 1 = Thủ kho (2), 2 = Nhân viên kho (3)
                int maVaiTro = cboVaiTro.SelectedIndex >= 0 ? cboVaiTro.SelectedIndex + 1 : 3;
                string trangThai = cboTrangThai.SelectedIndex == 1 ? "KHOA" : "HOAT_DONG";
                List<int> dsKho = LayDanhSachKhoDuocChon();

                // 1. Kiểm tra dữ liệu đầu vào
                if (!KiemTraHopLe(maVaiTro, dsKho))
                {
                    return;
                }

                string tenDangNhap = txtTenDangNhap.Text.Trim();
                string dienThoai = txtDienThoai.Text.Trim();
                string email = txtEmail.Text.Trim();
                string matKhauHash = PasswordHelper.HashPassword(txtMatKhau.Text);

                // 2. Mở giao dịch thêm Nhân viên, Tài khoản, Phân quyền kho và Nhật ký
                DataProcessing.ChangeDataTransaction((conn, trans) =>
                {
                    // 2.1. Thêm hồ sơ nhân viên
                    string sqlNV = @"
                        INSERT INTO NhanVien (HoTen, DienThoai, Email, DiaChi, NgayVaoLam, TrangThai, GhiChu)
                        VALUES (@HoTen, @DienThoai, @Email, @DiaChi, CAST(GETDATE() AS DATE), 'DANG_LAM', @GhiChu);
                        SELECT CAST(SCOPE_IDENTITY() AS INT);";
                    int maNVMoi;
                    using (SqlCommand cmdNV = new SqlCommand(sqlNV, conn, trans))
                    {
                        cmdNV.Parameters.AddWithValue("@HoTen", txtHoTen.Text.Trim());
                        cmdNV.Parameters.AddWithValue("@DienThoai", string.IsNullOrEmpty(dienThoai) ? (object)DBNull.Value : dienThoai);
                        cmdNV.Parameters.AddWithValue("@Email", string.IsNullOrEmpty(email) ? (object)DBNull.Value : email);
                        cmdNV.Parameters.AddWithValue("@DiaChi", string.IsNullOrWhiteSpace(txtDiaChi.Text) ? (object)DBNull.Value : txtDiaChi.Text.Trim());
                        cmdNV.Parameters.AddWithValue("@GhiChu", string.IsNullOrWhiteSpace(txtGhiChu.Text) ? (object)DBNull.Value : txtGhiChu.Text.Trim());
                        maNVMoi = Convert.ToInt32(cmdNV.ExecuteScalar());
                    }

                    // 2.2. Thêm tài khoản (mật khẩu lưu dạng băm SHA-256)
                    string sqlTK = @"
                        INSERT INTO TaiKhoan (MaNV, MaVaiTro, TenDangNhap, MatKhauHash, TrangThai)
                        VALUES (@MaNV, @MaVaiTro, @TenDangNhap, @MatKhauHash, @TrangThai);
                        SELECT CAST(SCOPE_IDENTITY() AS INT);";
                    int maTaiKhoanMoi;
                    using (SqlCommand cmdTK = new SqlCommand(sqlTK, conn, trans))
                    {
                        cmdTK.Parameters.AddWithValue("@MaNV", maNVMoi);
                        cmdTK.Parameters.AddWithValue("@MaVaiTro", maVaiTro);
                        cmdTK.Parameters.AddWithValue("@TenDangNhap", tenDangNhap);
                        cmdTK.Parameters.AddWithValue("@MatKhauHash", matKhauHash);
                        cmdTK.Parameters.AddWithValue("@TrangThai", trangThai);
                        maTaiKhoanMoi = Convert.ToInt32(cmdTK.ExecuteScalar());
                    }

                    // 2.3. Phân quyền kho
                    if (maVaiTro == 1)
                    {
                        // Quản lý: phân quyền toàn bộ kho đang hoạt động
                        string sqlInsAll = @"
                            INSERT INTO TaiKhoanKho (MaTaiKhoan, MaKho, NgayPhanCong, GhiChu)
                            SELECT @MaTaiKhoan, MaKho, GETDATE(), N'Phân công khi tạo mới tài khoản'
                            FROM Kho WHERE TrangThai = 'HOAT_DONG'";
                        using (SqlCommand cmdInsAll = new SqlCommand(sqlInsAll, conn, trans))
                        {
                            cmdInsAll.Parameters.AddWithValue("@MaTaiKhoan", maTaiKhoanMoi);
                            cmdInsAll.ExecuteNonQuery();
                        }
                    }
                    else
                    {
                        foreach (int maKho in dsKho)
                        {
                            string sqlIns = @"
                                INSERT INTO TaiKhoanKho (MaTaiKhoan, MaKho, NgayPhanCong, GhiChu)
                                VALUES (@MaTaiKhoan, @MaKho, GETDATE(), N'Phân công khi tạo mới tài khoản')";
                            using (SqlCommand cmdIns = new SqlCommand(sqlIns, conn, trans))
                            {
                                cmdIns.Parameters.AddWithValue("@MaTaiKhoan", maTaiKhoanMoi);
                                cmdIns.Parameters.AddWithValue("@MaKho", maKho);
                                cmdIns.ExecuteNonQuery();
                            }
                        }
                    }

                    // 2.4. Ghi nhật ký thao tác
                    string sqlLog = @"
                        INSERT INTO NhatKyThaoTac (MaTaiKhoan, ThoiDiem, ThaoTac, LoaiDoiTuong, MaDoiTuong, NoiDung)
                        VALUES (@MaTaiKhoan, GETDATE(), @ThaoTac, N'TaiKhoan', @MaDoiTuong, @NoiDung)";
                    using (SqlCommand cmdLog = new SqlCommand(sqlLog, conn, trans))
                    {
                        cmdLog.Parameters.AddWithValue("@MaTaiKhoan", SessionContext.MaTaiKhoan > 0 ? SessionContext.MaTaiKhoan : maTaiKhoanMoi);
                        cmdLog.Parameters.AddWithValue("@ThaoTac", "TAO_TAI_KHOAN");
                        cmdLog.Parameters.AddWithValue("@MaDoiTuong", maTaiKhoanMoi.ToString());
                        cmdLog.Parameters.AddWithValue("@NoiDung", $"Tạo tài khoản '{tenDangNhap}' cho nhân viên '{txtHoTen.Text.Trim()}'");
                        cmdLog.ExecuteNonQuery();
                    }
                });

                MessageBox.Show($"Tạo người dùng '{tenDangNhap}' thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi tạo người dùng: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
