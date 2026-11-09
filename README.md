# HƯỚNG DẪN CÀI ĐẶT & TRIỂN KHAI HỆ THỐNG
## Dự án: Quản lý Kho Hàng Nội Bộ (BTL-LTTQ-2)
**Nền tảng**: Windows Forms (.NET Framework 4.7.2) | **CSDL**: Microsoft SQL Server

---

## 1. Yêu cầu hệ thống (Prerequisites)
1. **Hệ điều hành**: Windows 10 hoặc Windows 11 (mặc định đã tích hợp sẵn .NET Framework 4.7.2 trở lên).
2. **Cơ sở dữ liệu**: Microsoft SQL Server 2016 trở lên (bản Express, Developer hoặc Enterprise) kèm công cụ **SQL Server Management Studio (SSMS)** hoặc **Azure Data Studio**.
3. **Môi trường lập trình (tùy chọn - nếu muốn xem/chỉnh sửa code)**:
   - Visual Studio 2019 hoặc Visual Studio 2022 (đã cài gói *.NET desktop development*).

---

## 2. Quy trình triển khai 3 bước (Chưa đầy 3 phút)

### Bước 1: Khởi tạo Cơ sở dữ liệu (1-Click)
1. Mở công cụ **SQL Server Management Studio (SSMS)** hoặc **Azure Data Studio**.
2. Kết nối vào SQL Server trên máy tính của bạn (ví dụ: `localhost\SQLEXPRESS`, `.` hoặc `localhost`).
3. Mở file script SQL trọn gói tại đường dẫn:
   ```
   docs/TaoCSDL_DayDu.sql
   ```
4. Nhấn phím **F5** (hoặc bấm nút **Execute**) để thực thi toàn bộ script.
   - Script sẽ tự động:
     - Tạo CSDL `QuanLyKhoPhuQuocLTTQ` chuẩn Unicode Tiếng Việt.
     - Tạo đầy đủ 30 bảng nghiệp vụ, ràng buộc khóa chính, chỉ mục.
     - Nạp toàn bộ dữ liệu mẫu ban đầu (Kho, Hàng hóa, Đơn vị tính, Nhóm hàng, Đối tác, Phân quyền, Tồn kho ban đầu).
     - Thiết lập toàn bộ 52 ràng buộc khóa ngoại (Foreign Keys).
     - Tạo 2 View nghiệp vụ tính tồn kho động (`vw_BienDongKho`, `vw_TonKhoHienTai`).
5. Khi màn hình Messages hiển thị:
   ```
   =================================================================
   ==> KHOI TAO CSDL QuanLyKhoPhuQuocLTTQ THANH CONG 100%!
   ==> San sang khoi chay ung dung Windows Forms BTL-LTTQ-2.
   =================================================================
   ```
   👉 CSDL đã sẵn sàng 100%!

---

### Bước 2: Kiểm tra Chuỗi kết nối (Connection String)
Mặc định dự án cấu hình kết nối tới SQL Server bản SQLEXPRESS:
```xml
Server=localhost\SQLEXPRESS;Database=QuanLyKhoPhuQuocLTTQ;Integrated Security=True;TrustServerCertificate=True;MultipleActiveResultSets=True
```

**LƯU Ý QUAN TRỌNG KHI ĐỔI MÁY**:
- Nếu máy tính của bạn cài SQL Server với tên mặc định (không có đuôi `\SQLEXPRESS`), hãy mở file cấu hình:
  - Nếu chạy từ mã nguồn: `BTL-LTTQ-2/App.config`
  - Nếu chạy file exe độc lập: `BTL-LTTQ-2/bin/Release/BTL-LTTQ-2.exe.config`
- Sửa lại phần `Server=localhost\SQLEXPRESS` thành `Server=.` hoặc `Server=localhost` (hoặc đúng tên máy tính / SQL Instance của bạn).
- Lưu file lại.

---

### Bước 3: Khởi chạy Ứng dụng

#### Cách 1: Chạy trực tiếp file thực thi Release (Nhanh nhất - Không cần Visual Studio)
1. Truy cập thư mục:
   ```
   BTL-LTTQ-2/bin/Release/
   ```
2. Nhấp đúp chuột vào file:
   ```
   BTL-LTTQ-2.exe
   ```
3. Ứng dụng sẽ hiển thị ngay màn hình Đăng nhập.

#### Cách 2: Mở và chạy từ Visual Studio (Dành cho nhà phát triển / Chấm bài)
1. Mở file dự án `BTL-LTTQ-2/BTL-LTTQ-2.csproj` bằng Visual Studio.
2. Nhấn **F5** (hoặc chọn Debug ➜ Start Debugging).

---

## 3. Danh sách Tài khoản Đăng nhập Hệ thống Mẫu

Hệ thống đã nạp sẵn 3 tài khoản demo đại diện cho 3 cấp vai trò nghiệp vụ thực tế trong doanh nghiệp:

| Tên đăng nhập | Mật khẩu mặc định | Vai trò | Quyền hạn & Phạm vi kho |
|---|---|---|---|
| **`quanly`** | `123456` | **Quản lý** | Toàn quyền mọi phân hệ, quản lý tất cả các kho trên toàn đảo Phú Quốc |
| **`thukho`** | `123456` | **Thủ kho** | Quản lý độc quyền Kho trung tâm (Xuất, Nhập, Chuyển kho, Kiểm kê) |
| **`nvkho`** | `123456` | **Nhân viên kho** | Thực hiện tác nghiệp ghi nhận hàng hóa tại Kho trung tâm |

---

## 4. Xử lý sự cố thường gặp (Troubleshooting)

1. **Lỗi "Không thể kết nối CSDL" khi vừa mở form Đăng nhập**:
   - **Nguyên nhân**: Dịch vụ SQL Server chưa bật, hoặc tên Server trong file cấu hình không khớp với máy của bạn.
   - **Cách xử lý**:
     - Mở hộp thoại `Run` (Windows + R) ➜ gõ `services.msc` ➜ kiểm tra dịch vụ `SQL Server (...)` có trạng thái **Running** chưa.
     - Mở file `App.config` / `BTL-LTTQ-2.exe.config` và kiểm tra lại thuộc tính `Server=...` cho đúng với SQL Server trên máy bạn.
2. **Lỗi hiển thị tiếng Việt trên SQL Server cũ**:
   - Script `TaoCSDL_DayDu.sql` đã được mã hóa **UTF-8 with BOM** và sử dụng Collation `Vietnamese_CI_AS` chuẩn. Toàn bộ chuỗi tiếng Việt đều có tiền tố `N'...'` bảo đảm không phát sinh lỗi font chữ trên mọi phiên bản SQL Server.
