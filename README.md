# Phần mềm quản lý kho hàng Phú Quốc (BTL Lập trình trực quan)

Ứng dụng Windows Forms (C#, .NET Framework 4.7.2, giao diện MDI) quản lý kho nhiều chi nhánh: hàng hóa, nhà cung cấp, nhập hàng, trả hàng nhập, kiểm kho, chuyển kho, báo cáo tồn kho và thẻ kho. Dữ liệu lưu trên SQL Server, truy xuất bằng ADO.NET.

## Yêu cầu

- Windows, Visual Studio 2019 trở lên (workload .NET desktop development).
- SQL Server hoặc SQL Server Express, instance `localhost\SQLEXPRESS`, đăng nhập Windows Authentication.

## Cài đặt CSDL

1. Mở `docs/TaoCSDL_DayDu.sql` trong SQL Server Management Studio, kết nối `localhost\SQLEXPRESS`.
2. Chạy toàn bộ script (F5). Script tạo CSDL `QuanLyKhoPhuQuocLTTQ` gồm bảng, ràng buộc, view và dữ liệu mẫu.
   Script chỉ chạy trên CSDL trống; nếu máy đã có CSDL cùng tên, script dừng và không ghi gì.

Nếu SQL Server dùng tên instance khác, sửa chuỗi kết nối `QuanLyKhoConnection` trong `BTL-LTTQ-2/App.config`.

## Chạy ứng dụng

1. Mở `BTL-LTTQ-2.slnx` bằng Visual Studio.
2. Build và chạy (F5).
3. Đăng nhập bằng một trong các tài khoản mẫu (mật khẩu đều là `123456`):

| Tài khoản | Vai trò | Quyền |
|---|---|---|
| `quanly` | Quản lý | Toàn bộ chức năng, đổi kho làm việc (xem được tất cả kho), quản lý người dùng |
| `thukho` | Thủ kho | Toàn bộ nghiệp vụ và báo cáo tại 1 kho được phân, không đổi kho |
| `nvkho` | Nhân viên kho | Hàng hóa, nhập hàng, trả hàng nhập, kiểm kho, chuyển kho, báo cáo tồn kho; không thấy Nhà cung cấp và Thẻ kho |

## Thư mục

- `BTL-LTTQ-2/Classes/` – lớp dùng chung: `DataProcessing` (truy cập CSDL), `SessionContext` (phiên đăng nhập), `ComboboxHelper`, `PasswordHelper`.
- `BTL-LTTQ-2/GUI/Forms/` – các form theo phân hệ.
- `docs/` – script CSDL, đặc tả chức năng (`docs/specs/`), sổ quyết định, sơ đồ.
- `tools/` – `XuatScriptCSDL.ps1` (xuất lại `docs/TaoCSDL_DayDu.sql` từ CSDL hiện có), `KiemTraMaHoa.ps1` (kiểm tra mã hóa UTF-8 BOM).
