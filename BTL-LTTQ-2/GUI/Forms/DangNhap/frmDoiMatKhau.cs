using BTL_LTTQ_2.Classes;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Security.Cryptography;
using System.Text;
using System.Windows.Forms;

namespace BTL_LTTQ_2.GUI.Forms.DangNhap
{
    public partial class frmDoiMatKhau : Form
    {
        public frmDoiMatKhau()
        {
            InitializeComponent();
        }

        private void chkHienMatKhau_CheckedChanged(object sender, EventArgs e)
        {
            bool hien = chkHienMatKhau.Checked;
            txtMatKhauCu.UseSystemPasswordChar = !hien;
            txtMatKhauMoi.UseSystemPasswordChar = !hien;
            txtXacNhanMatKhau.UseSystemPasswordChar = !hien;
        }

        private void txtMatKhauCu_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;
                txtMatKhauMoi.Focus();
            }
        }

        private void txtMatKhauMoi_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;
                txtXacNhanMatKhau.Focus();
            }
        }

        private void txtXacNhanMatKhau_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;
                btnLuu.PerformClick();
            }
        }

        private void btnBoQua_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void btnLuu_Click(object sender, EventArgs e)
        {
            string matKhauCu = txtMatKhauCu.Text;
            string matKhauMoi = txtMatKhauMoi.Text;
            string xacNhan = txtXacNhanMatKhau.Text;

            if (string.IsNullOrEmpty(matKhauCu))
            {
                MessageBox.Show("Vui lòng nhập mật khẩu hiện tại.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtMatKhauCu.Focus();
                return;
            }

            if (string.IsNullOrEmpty(matKhauMoi))
            {
                MessageBox.Show("Vui lòng nhập mật khẩu mới.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtMatKhauMoi.Focus();
                return;
            }

            if (matKhauMoi != xacNhan)
            {
                MessageBox.Show("Xác nhận mật khẩu mới không khớp.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtXacNhanMatKhau.Focus();
                return;
            }

            if (matKhauCu == matKhauMoi)
            {
                MessageBox.Show("Mật khẩu mới không được trùng với mật khẩu hiện tại.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtMatKhauMoi.Focus();
                return;
            }

            try
            {
                if (SessionContext.MaTaiKhoan > 0)
                {
                    string sql = "SELECT MatKhauHash FROM TaiKhoan WHERE MaTaiKhoan = @MaTaiKhoan";
                    SqlParameter[] paras = { new SqlParameter("@MaTaiKhoan", SessionContext.MaTaiKhoan) };
                    DataTable dt = DataProcessing.ReadData(sql, paras);

                    if (dt == null || dt.Rows.Count == 0)
                    {
                        MessageBox.Show("Không tìm thấy thông tin tài khoản hiện tại.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }

                    string dbMatKhau = dt.Rows[0]["MatKhauHash"] != DBNull.Value ? dt.Rows[0]["MatKhauHash"].ToString() : string.Empty;
                    bool hopLe = (matKhauCu == dbMatKhau) || string.Equals(TinhSha256(matKhauCu), dbMatKhau, StringComparison.OrdinalIgnoreCase);

                    if (!hopLe)
                    {
                        MessageBox.Show("Mật khẩu hiện tại không chính xác.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        txtMatKhauCu.Clear();
                        txtMatKhauCu.Focus();
                        return;
                    }

                    // Cập nhật mật khẩu mới (chuỗi plain-text theo chuẩn yêu cầu)
                    string sqlUpdate = "UPDATE TaiKhoan SET MatKhauHash = @MatKhauMoi WHERE MaTaiKhoan = @MaTaiKhoan";
                    SqlParameter[] pUpdate = {
                        new SqlParameter("@MatKhauMoi", matKhauMoi),
                        new SqlParameter("@MaTaiKhoan", SessionContext.MaTaiKhoan)
                    };
                    DataProcessing.ChangeData(sqlUpdate, pUpdate);

                    MessageBox.Show("Đổi mật khẩu thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }

                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi cập nhật mật khẩu: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

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
