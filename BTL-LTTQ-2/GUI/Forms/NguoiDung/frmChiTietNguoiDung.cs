using BTL_LTTQ_2.Classes;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace BTL_LTTQ_2.GUI.Forms.NguoiDung
{
    public partial class frmChiTietNguoiDung : Form
    {
        private bool isEditMode = false;
        private string editUsername = string.Empty;

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
                cboVaiTro.SelectedItem = vaiTro;
            }

            btnKhoaTaiKhoan.Visible = true;
            btnXoa.Visible = true;
        }

        private void frmChiTietNguoiDung_Load(object sender, EventArgs e)
        {
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
            }

            CapNhatPhanQuyenKhoTheoVaiTro();
        }

        private void NapDanhSachKho()
        {
            try
            {
                clbDanhSachKho.Items.Clear();
                string sqlKho = "SELECT MaKho, TenKho FROM Kho WHERE TrangThai = 'HOAT_DONG' OR TrangThai = 'ACTIVE' ORDER BY MaKho ASC";
                DataTable dtKho = DataProcessing.ReadData(sqlKho);
                if (dtKho != null && dtKho.Rows.Count > 0)
                {
                    DataTable dtPhanQuyen = null;
                    if (isEditMode && !string.IsNullOrEmpty(editUsername))
                    {
                        string sqlPQ = @"
                            SELECT tkk.MaKho 
                            FROM TaiKhoanKho tkk 
                            INNER JOIN TaiKhoan tk ON tkk.MaTaiKhoan = tk.MaTaiKhoan 
                            WHERE tk.TenDangNhap = @TenDangNhap";
                        dtPhanQuyen = DataProcessing.ReadData(sqlPQ, new SqlParameter[] { new SqlParameter("@TenDangNhap", editUsername) });
                    }

                    foreach (DataRow row in dtKho.Rows)
                    {
                        int maKho = Convert.ToInt32(row["MaKho"]);
                        string tenKho = row["TenKho"].ToString();
                        bool duocChon = false;

                        if (dtPhanQuyen != null)
                        {
                            foreach (DataRow pq in dtPhanQuyen.Rows)
                            {
                                if (Convert.ToInt32(pq["MaKho"]) == maKho)
                                {
                                    duocChon = true;
                                    break;
                                }
                            }
                        }

                        clbDanhSachKho.Items.Add(tenKho, duocChon);
                    }
                    return;
                }
            }
            catch { }

            NapDanhSachKhoDemo();
        }

        private void NapDanhSachKhoDemo()
        {
            clbDanhSachKho.Items.Clear();
            clbDanhSachKho.Items.Add("Kho trung tâm", true);
            clbDanhSachKho.Items.Add("Kho An Thới", false);
            clbDanhSachKho.Items.Add("Kho phụ kiện Tuna", false);

            if (cboVaiTro.SelectedIndex < 0)
            {
                cboVaiTro.SelectedIndex = 0;
            }
            if (cboTrangThai.SelectedIndex < 0)
            {
                cboTrangThai.SelectedIndex = 0;
            }
        }

        private void chkHienMatKhau_CheckedChanged(object sender, EventArgs e)
        {
            txtMatKhau.UseSystemPasswordChar = !chkHienMatKhau.Checked;
        }

        private void cboVaiTro_SelectedIndexChanged(object sender, EventArgs e)
        {
            CapNhatPhanQuyenKhoTheoVaiTro();
        }

        private void CapNhatPhanQuyenKhoTheoVaiTro()
        {
            bool laQuanLy = cboVaiTro.SelectedItem != null && cboVaiTro.SelectedItem.ToString() == "Quản lý";
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

        private void btnKhoaTaiKhoan_Click(object sender, EventArgs e)
        {
            string trangThaiHienTai = cboTrangThai.SelectedItem?.ToString() ?? "Đang hoạt động";
            bool dangHoatDong = (trangThaiHienTai == "Đang hoạt động");
            string hanhDong = dangHoatDong ? "khóa" : "mở khóa";

            DialogResult dr = MessageBox.Show($"Bạn có chắc chắn muốn {hanhDong} tài khoản '{editUsername}'?", 
                $"Xác nhận {hanhDong} tài khoản", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (dr == DialogResult.Yes)
            {
                try
                {
                    string trangThaiMoi = dangHoatDong ? "NGUNG_HOAT_DONG" : "HOAT_DONG";
                    string sql = "UPDATE TaiKhoan SET TrangThai = @TrangThai WHERE TenDangNhap = @TenDangNhap";
                    DataProcessing.ChangeData(sql, new SqlParameter[] {
                        new SqlParameter("@TrangThai", trangThaiMoi),
                        new SqlParameter("@TenDangNhap", editUsername)
                    });

                    cboTrangThai.SelectedItem = dangHoatDong ? "Ngừng hoạt động" : "Đang hoạt động";
                    MessageBox.Show($"Đã {hanhDong} tài khoản '{editUsername}' thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.DialogResult = DialogResult.OK;
                    this.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Lỗi {hanhDong} tài khoản: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            DialogResult dr = MessageBox.Show($"Bạn có chắc chắn muốn xóa tài khoản '{editUsername}' khỏi hệ thống?", 
                "Xác nhận xóa tài khoản", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

            if (dr == DialogResult.Yes)
            {
                try
                {
                    string sqlInfo = "SELECT MaTaiKhoan, MaNV FROM TaiKhoan WHERE TenDangNhap = @TenDangNhap";
                    DataTable dt = DataProcessing.ReadData(sqlInfo, new SqlParameter[] { new SqlParameter("@TenDangNhap", editUsername) });

                    if (dt != null && dt.Rows.Count > 0)
                    {
                        int maTk = Convert.ToInt32(dt.Rows[0]["MaTaiKhoan"]);

                        // Kiểm tra xem tài khoản có phát sinh chứng từ trên hệ thống không
                        string sqlCheck = @"
                            SELECT 
                                (SELECT COUNT(1) FROM PhieuNhap WHERE NguoiNhap = @TenDangNhap) +
                                (SELECT COUNT(1) FROM PhieuTraHangNhap WHERE NguoiTao = @TenDangNhap) +
                                (SELECT COUNT(1) FROM PhieuKiemKho WHERE NguoiKiem = @TenDangNhap) +
                                (SELECT COUNT(1) FROM PhieuChuyenKho WHERE NguoiChuyen = @TenDangNhap) AS TongChungTu";

                        object val = DataProcessing.ReadValue(sqlCheck, new SqlParameter[] { new SqlParameter("@TenDangNhap", editUsername) });
                        int tongChungTu = (val != null && val != DBNull.Value) ? Convert.ToInt32(val) : 0;

                        if (tongChungTu > 0)
                        {
                            MessageBox.Show($"Tài khoản '{editUsername}' đã từng phát sinh chứng từ nghiệp vụ trên hệ thống ({tongChungTu} chứng từ).\nKhông thể xóa vĩnh viễn, bạn chỉ có thể chọn 'Khóa tài khoản'.", 
                                "Không thể xóa", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            return;
                        }

                        DataProcessing.ChangeDataTransaction((conn, trans) =>
                        {
                            using (SqlCommand cmdDelKho = new SqlCommand("DELETE FROM TaiKhoanKho WHERE MaTaiKhoan = @MaTaiKhoan", conn, trans))
                            {
                                cmdDelKho.Parameters.AddWithValue("@MaTaiKhoan", maTk);
                                cmdDelKho.ExecuteNonQuery();
                            }

                            using (SqlCommand cmdDelTK = new SqlCommand("DELETE FROM TaiKhoan WHERE MaTaiKhoan = @MaTaiKhoan", conn, trans))
                            {
                                cmdDelTK.Parameters.AddWithValue("@MaTaiKhoan", maTk);
                                cmdDelTK.ExecuteNonQuery();
                            }
                        });

                        MessageBox.Show($"Đã xóa tài khoản '{editUsername}' thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        this.DialogResult = DialogResult.OK;
                        this.Close();
                    }
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

            if (string.IsNullOrWhiteSpace(txtHoTen.Text))
            {
                MessageBox.Show("Vui lòng nhập họ và tên nhân viên.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtHoTen.Focus();
                return;
            }

            try
            {
                if (isEditMode)
                {
                    int maVaiTro = 3;
                    if (cboVaiTro.SelectedItem != null)
                    {
                        string vt = cboVaiTro.SelectedItem.ToString();
                        if (vt == "Quản lý") maVaiTro = 1;
                        else if (vt == "Thủ kho") maVaiTro = 2;
                    }

                    string trangThai = (cboTrangThai.SelectedItem != null && cboTrangThai.SelectedItem.ToString() == "Ngừng hoạt động") 
                        ? "NGUNG_HOAT_DONG" : "HOAT_DONG";

                    string sqlInfo = "SELECT MaTaiKhoan, MaNV FROM TaiKhoan WHERE TenDangNhap = @TenDangNhap";
                    DataTable dtInfo = DataProcessing.ReadData(sqlInfo, new SqlParameter[] { new SqlParameter("@TenDangNhap", editUsername) });

                    if (dtInfo != null && dtInfo.Rows.Count > 0)
                    {
                        int maTk = Convert.ToInt32(dtInfo.Rows[0]["MaTaiKhoan"]);
                        object maNvObj = dtInfo.Rows[0]["MaNV"];

                        DataProcessing.ChangeDataTransaction((conn, trans) =>
                        {
                            // 1. Cập nhật thông tin nhân sự
                            if (maNvObj != DBNull.Value)
                            {
                                string sqlNV = "UPDATE NhanVien SET HoTen = @HoTen, DienThoai = @DienThoai WHERE MaNV = @MaNV";
                                using (SqlCommand cmdNV = new SqlCommand(sqlNV, conn, trans))
                                {
                                    cmdNV.Parameters.AddWithValue("@HoTen", txtHoTen.Text.Trim());
                                    cmdNV.Parameters.AddWithValue("@DienThoai", txtDienThoai.Text.Trim());
                                    cmdNV.Parameters.AddWithValue("@MaNV", Convert.ToInt32(maNvObj));
                                    cmdNV.ExecuteNonQuery();
                                }
                            }

                            // 2. Cập nhật tài khoản (chỉ sửa mật khẩu nếu quản lý nhập mật khẩu mới)
                            string sqlTK = string.IsNullOrWhiteSpace(txtMatKhau.Text)
                                ? "UPDATE TaiKhoan SET MaVaiTro = @MaVaiTro, TrangThai = @TrangThai WHERE MaTaiKhoan = @MaTaiKhoan"
                                : "UPDATE TaiKhoan SET MaVaiTro = @MaVaiTro, TrangThai = @TrangThai, MatKhauHash = @MatKhauMoi WHERE MaTaiKhoan = @MaTaiKhoan";

                            using (SqlCommand cmdTK = new SqlCommand(sqlTK, conn, trans))
                            {
                                cmdTK.Parameters.AddWithValue("@MaVaiTro", maVaiTro);
                                cmdTK.Parameters.AddWithValue("@TrangThai", trangThai);
                                cmdTK.Parameters.AddWithValue("@MaTaiKhoan", maTk);
                                if (!string.IsNullOrWhiteSpace(txtMatKhau.Text))
                                {
                                    cmdTK.Parameters.AddWithValue("@MatKhauMoi", txtMatKhau.Text);
                                }
                                cmdTK.ExecuteNonQuery();
                            }

                            // 3. Cập nhật phân quyền kho trong TaiKhoanKho
                            using (SqlCommand cmdDel = new SqlCommand("DELETE FROM TaiKhoanKho WHERE MaTaiKhoan = @MaTaiKhoan", conn, trans))
                            {
                                cmdDel.Parameters.AddWithValue("@MaTaiKhoan", maTk);
                                cmdDel.ExecuteNonQuery();
                            }

                            for (int i = 0; i < clbDanhSachKho.Items.Count; i++)
                            {
                                if (clbDanhSachKho.GetItemChecked(i))
                                {
                                    string tenKho = clbDanhSachKho.Items[i].ToString();
                                    string sqlIns = @"
                                        INSERT INTO TaiKhoanKho (MaTaiKhoan, MaKho, NgayPhanCong, GhiChu)
                                        SELECT @MaTaiKhoan, MaKho, GETDATE(), N'Phân quyền tài khoản'
                                        FROM Kho WHERE TenKho = @TenKho";
                                    using (SqlCommand cmdIns = new SqlCommand(sqlIns, conn, trans))
                                    {
                                        cmdIns.Parameters.AddWithValue("@MaTaiKhoan", maTk);
                                        cmdIns.Parameters.AddWithValue("@TenKho", tenKho);
                                        cmdIns.ExecuteNonQuery();
                                    }
                                }
                            }
                        });

                        MessageBox.Show("Cập nhật thông tin người dùng thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }

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
