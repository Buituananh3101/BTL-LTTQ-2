using System;
using System.Security.Cryptography;
using System.Text;

namespace BTL_LTTQ_2.Classes
{
    /// <summary>
    /// Tiện ích băm và kiểm tra mật khẩu bằng thuật toán SHA-256 thuần an toàn
    /// </summary>
    public static class PasswordHelper
    {
        /// <summary>
        /// Băm chuỗi mật khẩu dạng văn bản thuần sang chuỗi băm hex SHA-256 (chữ thường)
        /// </summary>
        /// <param name="plainPassword">Mật khẩu ban đầu</param>
        /// <returns>Chuỗi hex 64 ký tự đại diện cho hash SHA-256</returns>
        public static string HashPassword(string plainPassword)
        {
            if (string.IsNullOrEmpty(plainPassword))
            {
                return string.Empty;
            }

            using (SHA256 sha256 = SHA256.Create())
            {
                byte[] bytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(plainPassword));
                StringBuilder builder = new StringBuilder(bytes.Length * 2);
                foreach (byte b in bytes)
                {
                    builder.Append(b.ToString("x2"));
                }
                return builder.ToString();
            }
        }

        /// <summary>
        /// Xác thực mật khẩu nhập vào có khớp với chuỗi băm đã lưu trong CSDL hay không
        /// </summary>
        /// <param name="plainPassword">Mật khẩu người dùng nhập</param>
        /// <param name="hashedPassword">Mật khẩu đã băm trong CSDL</param>
        /// <returns>True nếu trùng khớp, ngược lại False</returns>
        public static bool VerifyPassword(string plainPassword, string hashedPassword)
        {
            if (string.IsNullOrEmpty(plainPassword) || string.IsNullOrEmpty(hashedPassword))
            {
                return false;
            }

            string hashOfInput = HashPassword(plainPassword);
            return string.Equals(hashOfInput, hashedPassword, StringComparison.OrdinalIgnoreCase);
        }

        /// <summary>
        /// Kiểm tra chuỗi lưu trong CSDL có phải chuỗi băm SHA-256 dạng hex (64 ký tự 0-9, a-f) hay không
        /// </summary>
        public static bool LaChuoiBamSha256(string giaTriLuu)
        {
            if (string.IsNullOrEmpty(giaTriLuu) || giaTriLuu.Length != 64)
            {
                return false;
            }

            foreach (char c in giaTriLuu)
            {
                bool laHex = (c >= '0' && c <= '9') || (c >= 'a' && c <= 'f') || (c >= 'A' && c <= 'F');
                if (!laHex)
                {
                    return false;
                }
            }
            return true;
        }

        /// <summary>
        /// Kiểm tra mật khẩu nhập vào với giá trị lưu trong CSDL (dùng chung cho Đăng nhập và Đổi mật khẩu):
        /// - Nếu giá trị lưu là chuỗi băm SHA-256: chỉ so sánh SHA256(mật khẩu nhập), không chấp nhận nhập thẳng chuỗi băm.
        /// - Nếu là dữ liệu cũ dạng văn bản thuần: so sánh trực tiếp (nơi gọi cần nâng cấp sang chuỗi băm khi hợp lệ).
        /// </summary>
        public static bool KiemTraMatKhau(string matKhauNhap, string giaTriLuu)
        {
            if (string.IsNullOrEmpty(matKhauNhap) || string.IsNullOrEmpty(giaTriLuu))
            {
                return false;
            }

            if (LaChuoiBamSha256(giaTriLuu))
            {
                return VerifyPassword(matKhauNhap, giaTriLuu);
            }

            return string.Equals(matKhauNhap, giaTriLuu, StringComparison.Ordinal);
        }
    }
}
