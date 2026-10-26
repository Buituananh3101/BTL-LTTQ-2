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
    }
}
