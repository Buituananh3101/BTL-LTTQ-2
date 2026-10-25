# 04. Xác thực, Phân quyền & Quản lý Kho làm việc

## 1. Kiến trúc Xác thực Tài khoản (Authentication)

Hệ thống xác thực tài khoản trực tiếp qua bảng `TaiKhoan`, liên kết `NhanVien`, `VaiTro` và `TaiKhoanKho` trong CSDL SQL Server:

### 1.1. Băm mật khẩu SHA-256 an toàn
Mật khẩu được băm bằng thuật toán SHA-256 thuần thông qua lớp tiện ích `PasswordHelper`:
```csharp
using System.Security.Cryptography;
using System.Text;

public static class PasswordHelper
{
    public static string HashPassword(string plainPassword)
    {
        if (string.IsNullOrEmpty(plainPassword)) return string.Empty;
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

    public static bool VerifyPassword(string plainPassword, string hashedPassword)
    {
        if (string.IsNullOrEmpty(plainPassword) || string.IsNullOrEmpty(hashedPassword)) return false;
        string hashOfInput = HashPassword(plainPassword);
        return string.Equals(hashOfInput, hashedPassword, StringComparison.OrdinalIgnoreCase);
    }
}
```
> [!IMPORTANT]
> Tuyệt đối không lưu hoặc so sánh mật khẩu ở dạng plain-text. Mọi tài khoản demo trong `DuLieuMau.sql` (mật khẩu mặc định `123456`) đều đã được băm SHA-256 (`8d969eef6ecad3c29a3a629280e686cf0c3f5d5a86aff3ca12020c923adc6c92`).

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
    public static bool IsDangXuat { get; set; }

    // Các cờ kiểm tra 3 vai trò chuẩn
    public static bool IsAdmin => MaVaiTro == 1 || (!string.IsNullOrEmpty(TenVaiTro) && TenVaiTro.IndexOf("Quản lý", StringComparison.OrdinalIgnoreCase) >= 0);
    public static bool IsQuanLy => IsAdmin; // Bí danh
    public static bool IsThuKho => MaVaiTro == 2 || (!string.IsNullOrEmpty(TenVaiTro) && TenVaiTro.IndexOf("Thủ kho", StringComparison.OrdinalIgnoreCase) >= 0);
    public static bool IsNhanVienKho => MaVaiTro == 3 || (!string.IsNullOrEmpty(TenVaiTro) && TenVaiTro.IndexOf("Nhân viên", StringComparison.OrdinalIgnoreCase) >= 0);

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
```

---

## 3. Phân quyền Chức năng & Phạm vi Kho theo 3 Vai trò (Role-based Authorization)

Khi `frmMain` khởi động sau khi đăng nhập thành công, gọi phương thức `PhanQuyenMenu()` để cấu hình ẩn/hiện hoặc vô hiệu hóa các mục menu:

### 3.1. Vai trò 1: Quản lý (`MaVaiTro = 1` - Tài khoản: `quanly`)
- **Phạm vi kho**: Quản lý mọi kho hàng trong hệ thống (toàn quyền truy cập tất cả các kho, được chọn chế độ "Tất cả kho" hoặc kho chi nhánh bất kỳ, được phép sử dụng chức năng "Đổi kho" `frmDoiKho`).
- **Quyền hạn menu**: Toàn quyền truy cập tất cả các menu và chức năng (`Hệ thống`, `Kho hàng`, `Hàng hóa`, `Mua hàng`, `Báo cáo`).

### 3.2. Vai trò 2: Thủ kho (`MaVaiTro = 2` - Tài khoản: `thukho`)
- **Phạm vi kho**: Quản lý 1 chi nhánh kho duy nhất được phân công trong bảng `TaiKhoanKho`. Khi đăng nhập ComboBox kho được cố định duy nhất kho đó; chức năng "Đổi kho" (`mnuDoiKho`) bị ẩn/khóa hoàn toàn (`mnuDoiKho.Visible = false`).
- **Quyền hạn menu**: Toàn quyền các nghiệp vụ quản lý kho của mình:
  - `Kho hàng`: Kiểm kho (`mnuKiemKho`), Chuyển kho (`mnuChuyenKho`).
  - `Hàng hóa`: Danh sách hàng hóa (`mnuDanhSachHangHoa`).
  - `Mua hàng`: Nhà cung cấp (`mnuNhaCungCap`), Nhập hàng (`mnuNhapHang`), Trả hàng nhập (`mnuTraHangNhap`).
  - `Báo cáo`: Báo cáo tồn kho (`mnuBaoCaoTonKho`), Báo cáo thẻ kho (`mnuBaoCaoTheKho`).

### 3.3. Vai trò 3: Nhân viên kho (`MaVaiTro = 3` - Tài khoản: `nvkho`)
- **Phạm vi kho**: Thao tác tác nghiệp tại kho được phân công; chức năng "Đổi kho" bị ẩn/khóa (`mnuDoiKho.Visible = false`).
- **Quyền hạn menu**: Thực hiện các thao tác tác nghiệp kho:
  - Cho phép: Kiểm kho (`mnuKiemKho`), Chuyển kho (`mnuChuyenKho`), Danh sách hàng hóa (`mnuDanhSachHangHoa`), Nhập hàng (`mnuNhapHang`), Trả hàng nhập (`mnuTraHangNhap`), Báo cáo tồn kho (`mnuBaoCaoTonKho`).
  - Ẩn/khóa các mục quản lý danh mục nhạy cảm & thẻ kho sổ sách: Nhà cung cấp (`mnuNhaCungCap.Visible = false`), Báo cáo thẻ kho (`mnuBaoCaoTheKho.Visible = false`).

---

## 4. Phân quyền theo Chi nhánh Kho (`TaiKhoanKho`)

1. **Bảng phân quyền `TaiKhoanKho`**:
   - Khóa chính kép: `(MaTaiKhoan, MaKho)`.
   - **Quản lý**: Có quyền trên tất cả các chi nhánh kho trong hệ thống.
   - **Thủ kho**: Gán quyền đúng 1 kho duy nhất.
   - **Nhân viên kho**: Gán quyền thao tác tại kho phụ trách.
2. **Cơ chế cách ly dữ liệu theo kho đang làm việc**:
   - Khi tạo bất kỳ chứng từ nào (Phiếu nhập, Phiếu chuyển, Phiếu kiểm, Phiếu trả hàng), giá trị `MaKho` tự động gán bằng `SessionContext.MaKhoHienTai`.
   - Số lượng tồn kho và các báo cáo luôn tự động lọc theo `SessionContext.MaKhoHienTai`.
