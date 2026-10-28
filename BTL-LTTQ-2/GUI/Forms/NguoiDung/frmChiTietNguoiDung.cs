using BTL_LTTQ_2.Classes;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace BTL_LTTQ_2.GUI.Forms.NguoiDung
{
    public partial class frmChiTietNguoiDung : Form
    {
        private bool isEditMode = false;
        private string editUsername = string.Empty;

        // Thông tin thực tế của tài khoản nạp từ CSDL khi mở form sửa
        private int maTaiKhoan = 0;
        private int maNV = 0;
        private int maVaiTroHienTai = 0;
        private string trangThaiHienTai = "HOAT_DONG";
        private bool daNapThongTin = false;

        // Cờ chặn xử lý sự kiện đổi vai trò trong lúc đang nạp dữ liệu
        private bool dangNapDuLieu = false;
        private int vaiTroTruoc = -1;

        public frmChiTietNguoiDung()
        {
            InitializeComponent();
            isEditMode = false;
        }

        public frmChiTietNguoiDung(string tenDangNhap, string hoTen, string vaiTro, string sdt, string matKhau = "") : this()
        {
            isEditMode = true;
            editUsername = tenDangNhap;

            lblTieuDe.Text = $"Cập nhật người dùng: {tenDangNhap}";
            txtTenDangNhap.Text = tenDangNhap;
            txtTenDangNhap.ReadOnly = true;

            // Lúc mở lên để sửa: ô mật khẩu trắng tinh, nhãn là 'Mật khẩu mới:', không lấy mật khẩu từ CSDL
            txtMatKhau.Text = string.Empty;
            lblMatKhau.Text = "Mật khẩu mới:";

            txtHoTen.Text = hoTen;
            txtDienThoai.Text = sdt;

            if (!string.IsNullOrEmpty(vaiTro))
            {
                dangNapDuLieu = true;
                cboVaiTro.SelectedItem = vaiTro;
                dangNapDuLieu = false;
            }

            btnKhoaTaiKhoan.Visible = true;
            btnXoa.Visible = true;
        }

        private void frmChiTietNguoiDung_Load(object sender, EventArgs e)
        {
            dangNapDuLieu = true;
            NapDanhSachKho();

            if (!isEditMode)
            {
                lblTieuDe.Text = "Tạo người dùng";
                lblMatKhau.Text = "Mật khẩu:";
                btnKhoaTaiKhoan.Visible = false;
                btnXoa.Visible = false;
                cboVaiTro.SelectedIndex = 1; // Mặc định Thủ kho
                cboTrangThai.SelectedIndex = 0; // Đang hoạt động
            }
            else
            {
                lblMatKhau.Text = "Mật khẩu mới:";
                txtMatKhau.Text = string.Empty;
                btnKhoaTaiKhoan.Visible = true;
                btnXoa.Visible = true;
                NapThongTinTaiKhoan();
            }

            dangNapDuLieu = false;
            vaiTroTruoc = cboVaiTro.SelectedIndex;
            CapNhatPhanQuyenKhoTheoVaiTro();
        }

        /// <summary>
        /// Nạp thông tin thực tế của tài khoản (vai trò, trạng thái, nhân sự) từ CSDL theo tên đăng nhập
        /// </summary>
        private void NapThongTinTaiKhoan()
        {
            try
            {
                string sql = @"
                    SELECT tk.MaTaiKhoan, tk.MaNV, tk.MaVaiTro, tk.TrangThai,
                           nv.HoTen, nv.DienThoai, nv.Email, nv.DiaChi, nv.GhiChu
                    FROM TaiKhoan tk
                    LEFT JOIN NhanVien nv ON tk.MaNV = nv.MaNV
                    WHERE tk.TenDangNhap = @TenDangNhap";
                DataTable dt = DataProcessing.ReadData(sql, new SqlParameter[] { new SqlParameter("@TenDangNhap", editUsername) });

                if (dt == null || dt.Rows.Count == 0)
                {
                    MessageBox.Show($"Không tìm thấy tài khoản '{editUsername}' trong hệ thống.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    KhoaThaoTacKhiLoi();
                    return;
                }

                DataRow row = dt.Rows[0];
                maTaiKhoan = Convert.ToInt32(row["MaTaiKhoan"]);
                maNV = row["MaNV"] != DBNull.Value ? Convert.ToInt32(row["MaNV"]) : 0;
                maVaiTroHienTai = Convert.ToInt32(row["MaVaiTro"]);
                trangThaiHienTai = row["TrangThai"] != DBNull.Value ? row["TrangThai"].ToString() : "HOAT_DONG";

                txtHoTen.Text = row["HoTen"] != DBNull.Value ? row["HoTen"].ToString() : string.Empty;
                txtDienThoai.Text = row["DienThoai"] != DBNull.Value ? row["DienThoai"].ToString() : string.Empty;
                txtEmail.Text = row["Email"] != DBNull.Value ? row["Email"].ToString() : string.Empty;
                txtDiaChi.Text = row["DiaChi"] != DBNull.Value ? row["DiaChi"].ToString() : string.Empty;
                txtGhiChu.Text = row["GhiChu"] != DBNull.Value ? row["GhiChu"].ToString() : string.Empty;

                // Vai trò: 1 = Quản lý, 2 = Thủ kho, 3 = Nhân viên kho (thứ tự trong combobox)
                if (maVaiTroHienTai >= 1 && maVaiTroHienTai <= cboVaiTro.Items.Count)
                {
                    cboVaiTro.SelectedIndex = maVaiTroHienTai - 1;
                }

                // Trạng thái: 0 = Đang hoạt động, 1 = Đã khóa
                cboTrangThai.SelectedIndex = LaDangHoatDong(trangThaiHienTai) ? 0 : 1;

                daNapThongTin = true;
                CapNhatNutKhoa();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi nạp thông tin tài khoản: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                KhoaThaoTacKhiLoi();
            }
        }

        /// <summary>
        /// Không cho phép Lưu/Khóa/Xóa khi chưa nạp được dữ liệu thật để tránh ghi đè sai
        /// </summary>
        private void KhoaThaoTacKhiLoi()
        {
            daNapThongTin = false;
            btnLuu.Enabled = false;
            btnKhoaTaiKhoan.Enabled = false;
            btnXoa.Enabled = false;
        }

        private static bool LaDangHoatDong(string trangThai)
        {
            return string.Equals(trangThai, "HOAT_DONG", StringComparison.OrdinalIgnoreCase)
                || string.Equals(trangThai, "ACTIVE", StringComparison.OrdinalIgnoreCase);
        }

        /// <summary>
        /// Đổi nhãn nút Khóa / Mở khóa theo trạng thái thực tế của tài khoản
        /// </summary>
        private void CapNhatNutKhoa()
        {
            btnKhoaTaiKhoan.Text = LaDangHoatDong(trangThaiHienTai) ? "Khóa" : "Mở khóa";
        }

        private bool LaTaiKhoanDangDangNhap()
        {
            return (SessionContext.MaTaiKhoan > 0 && maTaiKhoan == SessionContext.MaTaiKhoan)
                || string.Equals(editUsername, SessionContext.TenDangNhap, StringComparison.OrdinalIgnoreCase);
        }

        /// <summary>
        /// Đếm số tài khoản Quản lý đang hoạt động khác (không tính tài khoản đang sửa)
        /// </summary>
        private int DemQuanLyHoatDongKhac()
        {
            string sql = "SELECT COUNT(1) FROM TaiKhoan WHERE MaVaiTro = 1 AND TrangThai = 'HOAT_DONG' AND MaTaiKhoan <> @MaTaiKhoan";
            object val = DataProcessing.ReadValue(sql, new SqlParameter[] { new SqlParameter("@MaTaiKhoan", maTaiKhoan) });
            return (val != null && val != DBNull.Value) ? Convert.ToInt32(val) : 0;
        }

        /// <summary>
        /// True nếu tài khoản đang sửa là Quản lý hoạt động cuối cùng của hệ thống
        /// </summary>
        private bool LaQuanLyHoatDongCuoiCung()
        {
            if (maVaiTroHienTai != 1 || !LaDangHoatDong(trangThaiHienTai))
            {
                return false;
            }
            return DemQuanLyHoatDongKhac() == 0;
        }

        private void NapDanhSachKho()
        {
            try
            {
                clbDanhSachKho.Items.Clear();
                clbDanhSachKho.DisplayMember = "TenKho";

                string sqlKho = "SELECT MaKho, TenKho FROM Kho WHERE TrangThai = 'HOAT_DONG' ORDER BY MaKho ASC";
                DataTable dtKho = DataProcessing.ReadData(sqlKho);
                if (dtKho == null || dtKho.Rows.Count == 0)
                {
                    return;
                }

                HashSet<int> khoDaPhanCong = new HashSet<int>();
                if (isEditMode && !string.IsNullOrEmpty(editUsername))
                {
                    string sqlPQ = @"
                        SELECT tkk.MaKho
                        FROM TaiKhoanKho tkk
                        INNER JOIN TaiKhoan tk ON tkk.MaTaiKhoan = tk.MaTaiKhoan
                        WHERE tk.TenDangNhap = @TenDangNhap";
                    DataTable dtPhanQuyen = DataProcessing.ReadData(sqlPQ, new SqlParameter[] { new SqlParameter("@TenDangNhap", editUsername) });
                    if (dtPhanQuyen != null)
                    {
                        foreach (DataRow pq in dtPhanQuyen.Rows)
                        {
                            khoDaPhanCong.Add(Convert.ToInt32(pq["MaKho"]));
                        }
                    }
                }

                // Mỗi mục là một DataRowView (MaKho, TenKho) để lấy đúng mã kho khi lưu
                foreach (DataRowView drv in dtKho.DefaultView)
                {
                    int maKho = Convert.ToInt32(drv["MaKho"]);
                    clbDanhSachKho.Items.Add(drv, khoDaPhanCong.Contains(maKho));
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

        /// <summary>
        /// Mã vai trò theo combobox: 1 = Quản lý, 2 = Thủ kho, 3 = Nhân viên kho
        /// </summary>
        private int LayMaVaiTroDangChon()
        {
            int idx = cboVaiTro.SelectedIndex;
            return (idx >= 0 && idx <= 2) ? idx + 1 : 3;
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

        /// <summary>
        /// Kiểm tra quy tắc phân quyền kho: Quản lý = mọi kho, Thủ kho = đúng 1 kho, Nhân viên kho = tối thiểu 1 kho
        /// </summary>
        private bool KiemTraPhanQuyenKho(int maVaiTro, List<int> dsKho)
        {
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

            return true;
        }

        /// <summary>
        /// Kiểm tra trùng Điện thoại / Email với nhân viên khác (CSDL ràng buộc UNIQUE, chỉ cho phép một giá trị rỗng)
        /// </summary>
        private bool KiemTraTrungThongTinNhanVien(string dienThoai, string email)
        {
            string sqlDt = string.IsNullOrEmpty(dienThoai)
                ? "SELECT COUNT(1) FROM NhanVien WHERE DienThoai IS NULL AND MaNV <> @MaNV"
                : "SELECT COUNT(1) FROM NhanVien WHERE DienThoai = @GiaTri AND MaNV <> @MaNV";
            object soDt = DataProcessing.ReadValue(sqlDt, new SqlParameter[] {
                new SqlParameter("@GiaTri", dienThoai),
                new SqlParameter("@MaNV", maNV)
            });
            if (soDt != null && soDt != DBNull.Value && Convert.ToInt32(soDt) > 0)
            {
                MessageBox.Show(string.IsNullOrEmpty(dienThoai)
                    ? "Đã có nhân viên khác để trống số điện thoại. Vui lòng nhập số điện thoại."
                    : "Số điện thoại đã được sử dụng bởi nhân viên khác.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtDienThoai.Focus();
                return false;
            }

            string sqlEmail = string.IsNullOrEmpty(email)
                ? "SELECT COUNT(1) FROM NhanVien WHERE Email IS NULL AND MaNV <> @MaNV"
                : "SELECT COUNT(1) FROM NhanVien WHERE Email = @GiaTri AND MaNV <> @MaNV";
            object soEmail = DataProcessing.ReadValue(sqlEmail, new SqlParameter[] {
                new SqlParameter("@GiaTri", email),
                new SqlParameter("@MaNV", maNV)
            });
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

        /// <summary>
        /// Ghi nhật ký thao tác trên tài khoản trong cùng giao dịch
        /// </summary>
        private static void GhiNhatKyThaoTac(SqlConnection conn, SqlTransaction trans, string thaoTac, int maDoiTuong, string noiDung)
        {
            string sqlLog = @"
                INSERT INTO NhatKyThaoTac (MaTaiKhoan, ThoiDiem, ThaoTac, LoaiDoiTuong, MaDoiTuong, NoiDung)
                VALUES (@MaTaiKhoan, GETDATE(), @ThaoTac, N'TaiKhoan', @MaDoiTuong, @NoiDung)";
            using (SqlCommand cmdLog = new SqlCommand(sqlLog, conn, trans))
            {
                cmdLog.Parameters.AddWithValue("@MaTaiKhoan", SessionContext.MaTaiKhoan > 0 ? SessionContext.MaTaiKhoan : 1);
                cmdLog.Parameters.AddWithValue("@ThaoTac", thaoTac);
                cmdLog.Parameters.AddWithValue("@MaDoiTuong", maDoiTuong.ToString());
                cmdLog.Parameters.AddWithValue("@NoiDung", noiDung);
                cmdLog.ExecuteNonQuery();
            }
        }

        private void btnBoQua_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnKhoaTaiKhoan_Click(object sender, EventArgs e)
        {
            if (!daNapThongTin) return;

            // Dựa trên trạng thái thực tế trong CSDL, không dựa vào combobox
            bool dangHoatDong = LaDangHoatDong(trangThaiHienTai);
            string hanhDong = dangHoatDong ? "khóa" : "mở khóa";

            try
            {
                if (dangHoatDong)
                {
                    // Không cho tự khóa tài khoản đang đăng nhập
                    if (LaTaiKhoanDangDangNhap())
                    {
                        MessageBox.Show("Bạn không thể khóa tài khoản đang đăng nhập.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }

                    // Luôn giữ lại ít nhất một Quản lý đang hoạt động
                    if (LaQuanLyHoatDongCuoiCung())
                    {
                        MessageBox.Show("Đây là tài khoản Quản lý đang hoạt động cuối cùng.\nHệ thống phải luôn có ít nhất một Quản lý hoạt động, không thể khóa.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                }

                DialogResult dr = MessageBox.Show($"Bạn có chắc chắn muốn {hanhDong} tài khoản '{editUsername}'?",
                    $"Xác nhận {hanhDong} tài khoản", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (dr != DialogResult.Yes) return;

                string trangThaiMoi = dangHoatDong ? "KHOA" : "HOAT_DONG";
                DataProcessing.ChangeDataTransaction((conn, trans) =>
                {
                    using (SqlCommand cmd = new SqlCommand("UPDATE TaiKhoan SET TrangThai = @TrangThai WHERE MaTaiKhoan = @MaTaiKhoan", conn, trans))
                    {
                        cmd.Parameters.AddWithValue("@TrangThai", trangThaiMoi);
                        cmd.Parameters.AddWithValue("@MaTaiKhoan", maTaiKhoan);
                        cmd.ExecuteNonQuery();
                    }

                    GhiNhatKyThaoTac(conn, trans, dangHoatDong ? "KHOA_TAI_KHOAN" : "MO_KHOA_TAI_KHOAN", maTaiKhoan,
                        $"{(dangHoatDong ? "Khóa" : "Mở khóa")} tài khoản '{editUsername}'");
                });

                trangThaiHienTai = trangThaiMoi;
                cboTrangThai.SelectedIndex = dangHoatDong ? 1 : 0;
                CapNhatNutKhoa();

                MessageBox.Show($"Đã {hanhDong} tài khoản '{editUsername}' thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi {hanhDong} tài khoản: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            if (!daNapThongTin) return;

            // Không cho tự xóa tài khoản đang đăng nhập
            if (LaTaiKhoanDangDangNhap())
            {
                MessageBox.Show("Bạn không thể xóa tài khoản đang đăng nhập.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            DialogResult dr = MessageBox.Show($"Bạn có chắc chắn muốn xóa tài khoản '{editUsername}' khỏi hệ thống?",
                "Xác nhận xóa tài khoản", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

            if (dr == DialogResult.Yes)
            {
                try
                {
                    // Luôn giữ lại ít nhất một Quản lý đang hoạt động
                    if (LaQuanLyHoatDongCuoiCung())
                    {
                        MessageBox.Show("Đây là tài khoản Quản lý đang hoạt động cuối cùng.\nHệ thống phải luôn có ít nhất một Quản lý hoạt động, không thể xóa.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }

                    // Kiểm tra tài khoản đã phát sinh chứng từ (theo MaNV) hoặc nhật ký thao tác (theo MaTaiKhoan) hay chưa
                    string sqlCheck = @"
                        SELECT
                            (SELECT COUNT(1) FROM PhieuNhap WHERE MaNVLap = @MaNV OR MaNVXacNhan = @MaNV) +
                            (SELECT COUNT(1) FROM PhieuTraHangNhap WHERE MaNvlap = @MaNV OR MaNvtra = @MaNV) +
                            (SELECT COUNT(1) FROM PhieuKiemKe WHERE MaNVLap = @MaNV) +
                            (SELECT COUNT(1) FROM PhieuChuyenKho WHERE MaNVLap = @MaNV OR MaNVXuat = @MaNV OR MaNVNhan = @MaNV) +
                            (SELECT COUNT(1) FROM PhieuDieuChinh WHERE MaNVLap = @MaNV OR MaNVDuyet = @MaNV) +
                            (SELECT COUNT(1) FROM PhieuXuat WHERE MaNVLap = @MaNV OR MaNVXacNhan = @MaNV) +
                            (SELECT COUNT(1) FROM DonBan WHERE MaNVLap = @MaNV) +
                            (SELECT COUNT(1) FROM KhoanThu WHERE MaNVThu = @MaNV) AS TongChungTu,
                            (SELECT COUNT(1) FROM NhatKyThaoTac WHERE MaTaiKhoan = @MaTaiKhoan) AS TongNhatKy";

                    DataTable dtCheck = DataProcessing.ReadData(sqlCheck, new SqlParameter[] {
                        new SqlParameter("@MaNV", maNV),
                        new SqlParameter("@MaTaiKhoan", maTaiKhoan)
                    });
                    int tongChungTu = 0;
                    int tongNhatKy = 0;
                    if (dtCheck != null && dtCheck.Rows.Count > 0)
                    {
                        tongChungTu = dtCheck.Rows[0]["TongChungTu"] != DBNull.Value ? Convert.ToInt32(dtCheck.Rows[0]["TongChungTu"]) : 0;
                        tongNhatKy = dtCheck.Rows[0]["TongNhatKy"] != DBNull.Value ? Convert.ToInt32(dtCheck.Rows[0]["TongNhatKy"]) : 0;
                    }

                    if (tongChungTu > 0 || tongNhatKy > 0)
                    {
                        MessageBox.Show($"Tài khoản '{editUsername}' đã phát sinh dữ liệu trên hệ thống ({tongChungTu} chứng từ, {tongNhatKy} nhật ký thao tác).\nKhông thể xóa vĩnh viễn, bạn chỉ có thể chọn 'Khóa' tài khoản.",
                            "Không thể xóa", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }

                    // Xóa phân quyền kho rồi xóa tài khoản (giữ lại hồ sơ nhân viên)
                    DataProcessing.ChangeDataTransaction((conn, trans) =>
                    {
                        using (SqlCommand cmdDelKho = new SqlCommand("DELETE FROM TaiKhoanKho WHERE MaTaiKhoan = @MaTaiKhoan", conn, trans))
                        {
                            cmdDelKho.Parameters.AddWithValue("@MaTaiKhoan", maTaiKhoan);
                            cmdDelKho.ExecuteNonQuery();
                        }

                        using (SqlCommand cmdDelTK = new SqlCommand("DELETE FROM TaiKhoan WHERE MaTaiKhoan = @MaTaiKhoan", conn, trans))
                        {
                            cmdDelTK.Parameters.AddWithValue("@MaTaiKhoan", maTaiKhoan);
                            cmdDelTK.ExecuteNonQuery();
                        }

                        GhiNhatKyThaoTac(conn, trans, "XOA_TAI_KHOAN", maTaiKhoan, $"Xóa tài khoản '{editUsername}'");
                    });

                    MessageBox.Show($"Đã xóa tài khoản '{editUsername}' thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.DialogResult = DialogResult.OK;
                    this.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi xóa tài khoản: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void btnLuu_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtTenDangNhap.Text))
            {
                MessageBox.Show("Vui lòng nhập tên đăng nhập.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtTenDangNhap.Focus();
                return;
            }

            // Chỉ bắt buộc mật khẩu khi TẠO MỚI. Khi SỬA CHI TIẾT: nếu để trống thì giữ nguyên mật khẩu cũ trong CSDL
            if (!isEditMode && string.IsNullOrWhiteSpace(txtMatKhau.Text))
            {
                MessageBox.Show("Vui lòng nhập mật khẩu.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtMatKhau.Focus();
                return;
            }

            if (!string.IsNullOrEmpty(txtMatKhau.Text) && txtMatKhau.Text.Length < 6)
            {
                MessageBox.Show("Mật khẩu phải có tối thiểu 6 ký tự.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtMatKhau.Focus();
                return;
            }

            if (string.IsNullOrWhiteSpace(txtHoTen.Text))
            {
                MessageBox.Show("Vui lòng nhập họ và tên nhân viên.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtHoTen.Focus();
                return;
            }

            // Form này chỉ dùng để cập nhật; tạo mới thực hiện tại frmNguoiDung
            if (!isEditMode || !daNapThongTin)
            {
                return;
            }

            try
            {
                int maVaiTro = LayMaVaiTroDangChon();
                string trangThai = cboTrangThai.SelectedIndex == 1 ? "KHOA" : "HOAT_DONG";
                List<int> dsKho = LayDanhSachKhoDuocChon();

                // 1. Không cho tự hạ vai trò hoặc tự khóa tài khoản đang đăng nhập
                if (LaTaiKhoanDangDangNhap())
                {
                    if (maVaiTroHienTai == 1 && maVaiTro != 1)
                    {
                        MessageBox.Show("Bạn không thể tự hạ vai trò của tài khoản đang đăng nhập.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                    if (trangThai == "KHOA")
                    {
                        MessageBox.Show("Bạn không thể khóa tài khoản đang đăng nhập.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                }

                // 2. Luôn giữ lại ít nhất một Quản lý đang hoạt động
                bool boQuyenQuanLy = maVaiTro != 1 || trangThai != "HOAT_DONG";
                if (boQuyenQuanLy && LaQuanLyHoatDongCuoiCung())
                {
                    MessageBox.Show("Đây là tài khoản Quản lý đang hoạt động cuối cùng.\nHệ thống phải luôn có ít nhất một Quản lý hoạt động, không thể đổi vai trò hoặc khóa.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // 3. Kiểm tra quy tắc phân quyền kho theo vai trò
                if (!KiemTraPhanQuyenKho(maVaiTro, dsKho))
                {
                    return;
                }

                // 4. Kiểm tra trùng điện thoại / email
                string dienThoai = txtDienThoai.Text.Trim();
                string email = txtEmail.Text.Trim();
                if (!KiemTraTrungThongTinNhanVien(dienThoai, email))
                {
                    return;
                }

                string matKhauMoi = txtMatKhau.Text;

                DataProcessing.ChangeDataTransaction((conn, trans) =>
                {
                    // 1. Cập nhật thông tin nhân sự
                    if (maNV > 0)
                    {
                        string sqlNV = @"UPDATE NhanVien
                                         SET HoTen = @HoTen, DienThoai = @DienThoai, Email = @Email, DiaChi = @DiaChi, GhiChu = @GhiChu
                                         WHERE MaNV = @MaNV";
                        using (SqlCommand cmdNV = new SqlCommand(sqlNV, conn, trans))
                        {
                            cmdNV.Parameters.AddWithValue("@HoTen", txtHoTen.Text.Trim());
                            cmdNV.Parameters.AddWithValue("@DienThoai", string.IsNullOrEmpty(dienThoai) ? (object)DBNull.Value : dienThoai);
                            cmdNV.Parameters.AddWithValue("@Email", string.IsNullOrEmpty(email) ? (object)DBNull.Value : email);
                            cmdNV.Parameters.AddWithValue("@DiaChi", string.IsNullOrWhiteSpace(txtDiaChi.Text) ? (object)DBNull.Value : txtDiaChi.Text.Trim());
                            cmdNV.Parameters.AddWithValue("@GhiChu", string.IsNullOrWhiteSpace(txtGhiChu.Text) ? (object)DBNull.Value : txtGhiChu.Text.Trim());
                            cmdNV.Parameters.AddWithValue("@MaNV", maNV);
                            cmdNV.ExecuteNonQuery();
                        }
                    }

                    // 2. Cập nhật tài khoản (chỉ sửa mật khẩu nếu quản lý nhập mật khẩu mới, lưu dạng băm SHA-256)
                    string sqlTK = string.IsNullOrEmpty(matKhauMoi)
                        ? "UPDATE TaiKhoan SET MaVaiTro = @MaVaiTro, TrangThai = @TrangThai WHERE MaTaiKhoan = @MaTaiKhoan"
                        : "UPDATE TaiKhoan SET MaVaiTro = @MaVaiTro, TrangThai = @TrangThai, MatKhauHash = @MatKhauMoi WHERE MaTaiKhoan = @MaTaiKhoan";

                    using (SqlCommand cmdTK = new SqlCommand(sqlTK, conn, trans))
                    {
                        cmdTK.Parameters.AddWithValue("@MaVaiTro", maVaiTro);
                        cmdTK.Parameters.AddWithValue("@TrangThai", trangThai);
                        cmdTK.Parameters.AddWithValue("@MaTaiKhoan", maTaiKhoan);
                        if (!string.IsNullOrEmpty(matKhauMoi))
                        {
                            cmdTK.Parameters.AddWithValue("@MatKhauMoi", PasswordHelper.HashPassword(matKhauMoi));
                        }
                        cmdTK.ExecuteNonQuery();
                    }

                    // 3. Cập nhật phân quyền kho trong TaiKhoanKho
                    using (SqlCommand cmdDel = new SqlCommand("DELETE FROM TaiKhoanKho WHERE MaTaiKhoan = @MaTaiKhoan", conn, trans))
                    {
                        cmdDel.Parameters.AddWithValue("@MaTaiKhoan", maTaiKhoan);
                        cmdDel.ExecuteNonQuery();
                    }

                    if (maVaiTro == 1)
                    {
                        // Quản lý: phân quyền toàn bộ kho đang hoạt động
                        string sqlInsAll = @"
                            INSERT INTO TaiKhoanKho (MaTaiKhoan, MaKho, NgayPhanCong, GhiChu)
                            SELECT @MaTaiKhoan, MaKho, GETDATE(), N'Cập nhật phân quyền kho'
                            FROM Kho WHERE TrangThai = 'HOAT_DONG'";
                        using (SqlCommand cmdInsAll = new SqlCommand(sqlInsAll, conn, trans))
                        {
                            cmdInsAll.Parameters.AddWithValue("@MaTaiKhoan", maTaiKhoan);
                            cmdInsAll.ExecuteNonQuery();
                        }
                    }
                    else
                    {
                        foreach (int maKho in dsKho)
                        {
                            string sqlIns = @"
                                INSERT INTO TaiKhoanKho (MaTaiKhoan, MaKho, NgayPhanCong, GhiChu)
                                VALUES (@MaTaiKhoan, @MaKho, GETDATE(), N'Cập nhật phân quyền kho')";
                            using (SqlCommand cmdIns = new SqlCommand(sqlIns, conn, trans))
                            {
                                cmdIns.Parameters.AddWithValue("@MaTaiKhoan", maTaiKhoan);
                                cmdIns.Parameters.AddWithValue("@MaKho", maKho);
                                cmdIns.ExecuteNonQuery();
                            }
                        }
                    }

                    // 4. Ghi nhật ký thao tác
                    GhiNhatKyThaoTac(conn, trans, "CAP_NHAT_TAI_KHOAN", maTaiKhoan, $"Cập nhật thông tin tài khoản '{editUsername}'");
                });

                MessageBox.Show("Cập nhật thông tin người dùng thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);

                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi lưu người dùng: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
