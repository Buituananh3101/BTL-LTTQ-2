# 04. Xác thực, Phân quyền & Quản lý Kho làm việc

## 1. Kiến trúc Xác thực Tài khoản (Authentication)

Hệ thống xác thực tài khoản trực tiếp qua bảng `TaiKhoan`, liên kết `NhanVien` và `VaiTro` trong CSDL SQL Server:

### 1.1. Băm mật khẩu SHA-256 an toàn
Mật khẩu được băm bằng thuật toán SHA-256 thuần thông qua lớp tiện ích `PasswordHelper`:
```csharp
using System.Security.Cryptography;
using System.Text;

public static class PasswordHelper
{
    public static string HashPassword(string plainPassword)
    {
        using (SHA256 sha256 = SHA256.Create())
        {
            byte[] bytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(plainPassword));
            StringBuilder builder = new StringBuilder();
            foreach (byte b in bytes)
            {
                builder.Append(b.ToString("x2"));
            }
            return builder.ToString();
        }
    }
}
```
> [!IMPORTANT]
> Tuyệt đối không lưu hoặc so sánh mật khẩu ở dạng plain-text. Mọi tài khoản demo trong `DuLieuMau.sql` (mật khẩu mặc định `123456`) đều đã được băm SHA-256.

---

## 2. Quản lý Phiên làm việc toàn cục (`SessionContext`)

Để lưu giữ thông tin người dùng và chi nhánh kho trong suốt vòng đời ứng dụng WinForms, sử dụng lớp tĩnh `SessionContext`:

```csharp
public static class SessionContext
{
    public static int MaTaiKhoan { get; set; }
    public static string TenDangNhap { get; set; }
    public static int? MaNV { get; set; }
    public static string HoTen { get; set; }
    public static int MaVaiTro { get; set; }
    public static string TenVaiTro { get; set; }

    public static int MaKhoHienTai { get; set; }
    public static string TenKhoHienTai { get; set; }

    // Các cờ kiểm tra vai trò nhanh
    public static bool IsAdmin => MaVaiTro == 1;      // Quản trị viên
    public static bool IsThuKho => MaVaiTro == 2;     // Thủ kho
    public static bool IsBanHang => MaVaiTro == 3;    // Nhân viên bán hàng
    public static bool IsKeToan => MaVaiTro == 4;     // Kế toán

    public static void XoaPhien()
    {
        MaTaiKhoan = 0;
        TenDangNhap = string.Empty;
        HoTen = string.Empty;
        MaVaiTro = 0;
        TenVaiTro = string.Empty;
        MaKhoHienTai = 0;
        TenKhoHienTai = string.Empty;
    }
}
```

---

## 3. Phân quyền Chức năng theo Vai trò (Role-based Authorization)

Khi `frmMain` khởi động sau khi đăng nhập thành công, gọi phương thức `PhanQuyenMenu()` để cấu hình ẩn/hiện hoặc vô hiệu hóa các mục menu:

```csharp
private void PhanQuyenMenu()
{
    if (SessionContext.IsAdmin)
    {
        // Admin có toàn quyền truy cập tất cả menu
        return;
    }

    if (SessionContext.IsThuKho)
    {
        // Thủ kho: Ẩn quản trị người dùng, giữ Quản lý kho, Danh mục, Báo cáo tồn
        mnuDoiMatKhau.Visible = true;
        mnuDanhMuc.Visible = true;
        mnuQuanLyKho.Visible = true;
        mnuBaoCao.Visible = true;
    }
    else if (SessionContext.IsBanHang)
    {
        // Nhân viên bán hàng: Không cho phép nhập hàng, xuất hủy, kiểm kho
        mnuNhapHang.Visible = false;
        mnuKiemKho.Visible = false;
        mnuXuatHuy.Visible = false;
        mnuChuyenKho.Visible = false;
    }
    else if (SessionContext.IsKeToan)
    {
        // Kế toán: Xem báo cáo và danh mục
        mnuNhapHang.Enabled = false;
        mnuKiemKho.Enabled = false;
    }
}
```

---

## 4. Phân quyền theo Chi nhánh Kho (`TaiKhoanKho`)

1. **Bảng phân quyền `TaiKhoanKho`**:
   - Khóa chính kép: `(MaTaiKhoan, MaKho)`.
   - Admin: Mặc định có quyền truy cập vào tất cả các chi nhánh kho trong hệ thống.
   - Các vai trò khác: Chỉ được chọn và thao tác trên những kho được cấp phép trong bảng `TaiKhoanKho`.
2. **Cơ chế cách ly dữ liệu theo kho đang làm việc**:
   - Khi tạo bất kỳ chứng từ nào (Phiếu nhập, Phiếu chuyển, Phiếu kiểm, Phiếu xuất), giá trị `MaKho` tự động gán bằng `SessionContext.MaKhoHienTai`.
   - Số lượng tồn kho và các báo cáo luôn tự động lọc theo `SessionContext.MaKhoHienTai`.
   - Người dùng có thể đổi kho làm việc nhanh qua chức năng "Đổi kho" hoặc combobox trên thanh trạng thái/topbar (chỉ nạp các kho mà tài khoản có quyền).
