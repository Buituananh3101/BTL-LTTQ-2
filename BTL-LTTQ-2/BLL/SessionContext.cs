using System;

namespace BTL_LTTQ_2.BLL
{
    /// <summary>
    /// Lưu trữ thông tin phiên làm việc hiện tại của người dùng và kho đang chọn
    /// </summary>
    public static class SessionContext
    {
        public static int MaTaiKhoan { get; set; } = 1;
        public static string TenDangNhap { get; set; } = "quanly";
        public static string HoTen { get; set; } = "Quản trị viên";
        public static int MaVaiTro { get; set; } = 1; // 1: Admin
        public static string TenVaiTro { get; set; } = "Quản trị hệ thống";
        public static int MaKhoHienTai { get; set; } = 1;
        public static string TenKhoHienTai { get; set; } = "Kho trung tâm";

        public static bool IsAdmin => MaVaiTro == 1;
    }
}
