using System;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using BTL_LTTQ_2.Classes;
using BTL_LTTQ_2.GUI.Forms;
using BTL_LTTQ_2.GUI.Forms.DangNhap;

namespace BTL_LTTQ_2
{
    internal static class Program
    {
        // Khai báo các API Windows bật High DPI Awareness chống hiện tượng mờ / vỡ nét trên màn hình độ phân giải cao
        [DllImport("user32.dll", SetLastError = true)]
        private static extern bool SetProcessDpiAwarenessContext(IntPtr dpiContext);

        [DllImport("user32.dll", SetLastError = true)]
        private static extern bool SetProcessDPIAware();

        [DllImport("shcore.dll", SetLastError = true)]
        private static extern int SetProcessDpiAwareness(int awareness);

        private static readonly IntPtr DPI_AWARENESS_CONTEXT_PER_MONITOR_AWARE_V2 = new IntPtr(-4);

        /// <summary>
        /// Điểm khởi đầu chính của ứng dụng.
        /// </summary>
        [STAThread]
        static void Main()
        {
            // Bật Per-Monitor V2 DPI Awareness trước khi nạp giao diện WinForms
            try
            {
                if (Environment.OSVersion.Version.Major >= 10)
                {
                    SetProcessDpiAwarenessContext(DPI_AWARENESS_CONTEXT_PER_MONITOR_AWARE_V2);
                }
                else
                {
                    try
                    {
                        SetProcessDpiAwareness(2); // PROCESS_PER_MONITOR_DPI_AWARE (Win 8.1)
                    }
                    catch
                    {
                        SetProcessDPIAware(); // Win 7/8
                    }
                }
            }
            catch
            {
                // Bỏ qua nếu môi trường không hỗ trợ API
            }

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            // Vòng lặp phiên làm việc: Yêu cầu đăng nhập trước khi vào hệ thống
            while (true)
            {
                using (frmDangNhap formDangNhap = new frmDangNhap())
                {
                    if (formDangNhap.ShowDialog() != DialogResult.OK)
                    {
                        // Người dùng nhấn Thoát hoặc đóng form đăng nhập -> Thoát ứng dụng
                        return;
                    }
                }

                // Đăng nhập thành công -> Khởi chạy màn hình chính MDI frmMain
                using (frmMain formMain = new frmMain())
                {
                    Application.Run(formMain);

                    // Khi formMain đóng: kiểm tra xem có phải do người dùng chọn Đăng xuất không
                    if (!SessionContext.IsDangXuat)
                    {
                        // Người dùng chọn Thoát hoặc đóng cửa sổ -> Kết thúc hoàn toàn
                        break;
                    }

                    // Nếu là Đăng xuất: giải phóng phiên làm việc và tiếp tục vòng lặp để mở lại form đăng nhập
                    SessionContext.XoaPhien();
                }
            }
        }
    }
}
