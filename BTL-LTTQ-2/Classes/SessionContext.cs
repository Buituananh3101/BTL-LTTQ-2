using System;

namespace BTL_LTTQ_2.Classes
{
    /// <summary>
    /// Lưu trữ thông tin phiên làm việc hiện tại của người dùng, vai trò và kho đang chọn
    /// </summary>
    public static class SessionContext
    {
        public static int MaTaiKhoan { get; set; } = 0;
        public static string TenDangNhap { get; set; } = string.Empty;
        public static int? MaNV { get; set; } = null;
        public static string HoTen { get; set; } = string.Empty;
        public static int MaVaiTro { get; set; } = 0;
        public static string TenVaiTro { get; set; } = string.Empty;
        public static int MaKhoHienTai { get; set; } = 0;
        public static string TenKhoHienTai { get; set; } = string.Empty;

        /// <summary>
        /// Cờ đánh dấu người dùng chủ động chọn Đăng xuất (để Program.cs quay lại form đăng nhập)
        /// </summary>
        public static bool IsDangXuat { get; set; } = false;

        /// <summary>
        /// Vai trò Quản lý: Toàn quyền truy cập mọi chức năng và quản lý mọi kho hàng
        /// (Ưu tiên MaVaiTro 1/2/3; chỉ so khớp theo tên vai trò khi MaVaiTro = 0 để các cờ không thể cùng đúng)
        /// </summary>
        public static bool IsAdmin => MaVaiTro > 0 ? MaVaiTro == 1 : KhopTenVaiTro("Quản lý");

        /// <summary>
        /// Vai trò Quản lý (bí danh của IsAdmin)
        /// </summary>
        public static bool IsQuanLy => IsAdmin;

        /// <summary>
        /// Vai trò Thủ kho: Quản lý 1 kho duy nhất được phân công
        /// </summary>
        public static bool IsThuKho => MaVaiTro > 0 ? MaVaiTro == 2 : KhopTenVaiTro("Thủ kho");

        /// <summary>
        /// Vai trò Nhân viên kho: Thực hiện các nghiệp vụ tác nghiệp tại kho được phân công
        /// </summary>
        public static bool IsNhanVienKho => MaVaiTro > 0 ? MaVaiTro == 3 : KhopTenVaiTro("Nhân viên");

        /// <summary>
        /// So khớp tên vai trò (chỉ dùng dự phòng khi chưa có MaVaiTro)
        /// </summary>
        private static bool KhopTenVaiTro(string tuKhoa)
        {
            return !string.IsNullOrEmpty(TenVaiTro) && TenVaiTro.IndexOf(tuKhoa, StringComparison.OrdinalIgnoreCase) >= 0;
        }

        /// <summary>
        /// Xóa sạch thông tin phiên làm việc khi người dùng đăng xuất
        /// </summary>
        public static void XoaPhien()
        {
            MaTaiKhoan = 0;
            TenDangNhap = string.Empty;
            MaNV = null;
            HoTen = string.Empty;
            MaVaiTro = 0;
            TenVaiTro = string.Empty;
            MaKhoHienTai = 0;
            TenKhoHienTai = string.Empty;
            IsDangXuat = false;
        }
    }
}
