using System;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using BTL_LTTQ_2.GUI.Forms;

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
            Application.Run(new frmMain());
        }
    }
}
