# Đặc tả Chức năng: Đăng nhập Hệ thống & Phân quyền Thao tác

> **Phân hệ**: Xác thực & Phân quyền Hệ thống  
> **Tài liệu tham chiếu**: [`.agent/rules/04-xac-thuc-phan-quyen.md`](../../.agent/rules/04-xac-thuc-phan-quyen.md) | [`AGENTS.md`](../../AGENTS.md)

---

## 1. Mục tiêu & Phạm vi nghiệp vụ
- **Mục tiêu**: Cung cấp màn hình Đăng nhập (`frmDangNhap`) khởi động đầu tiên khi chạy chương trình, xác thực tài khoản và mật khẩu bằng băm mã hóa SHA-256 an toàn, chọn chi nhánh kho làm việc ban đầu, lưu thông tin phiên làm việc vào `SessionContext`, đồng thời phân quyền hiển thị và thao tác trên thanh thực đơn `MenuStrip` của `frmMain` theo 3 vai trò người dùng chuẩn.
- **3 Vai trò người dùng**:
  1. **Quản lý** (`MaVaiTro = 1`):
     - **Phạm vi kho**: Quản lý mọi kho hàng trong hệ thống. Khi đăng nhập có lựa chọn "Tất cả kho" hoặc bất kỳ chi nhánh kho nào; trong phiên làm việc có toàn quyền sử dụng chức năng "Đổi kho" (`frmDoiKho`) để chuyển qua lại giữa các kho.
     - **Quyền hạn chức năng**: Toàn quyền truy cập tất cả các phân hệ và menu (Hệ thống, Kho hàng, Hàng hóa, Mua hàng, Báo cáo).
  2. **Thủ kho** (`MaVaiTro = 2`):
     - **Phạm vi kho**: Quản lý 1 chi nhánh kho duy nhất được phân công. Khi đăng nhập ComboBox kho được cố định duy nhất kho đó (khóa không cho chọn kho khác hoặc "Tất cả kho"); trong phiên làm việc menu "Đổi kho" bị vô hiệu hóa/ẩn.
     - **Quyền hạn chức năng**: Toàn quyền thực hiện các nghiệp vụ quản lý kho của mình: Kiểm kho, Chuyển kho, Danh sách hàng hóa, Nhà cung cấp, Nhập hàng, Trả hàng nhập, Báo cáo tồn kho, Báo cáo thẻ kho.
  3. **Nhân viên kho** (`MaVaiTro = 3`):
     - **Phạm vi kho**: Thực hiện thao tác nghiệp vụ tại kho được phân công. Không được đổi kho (menu "Đổi kho" bị ẩn).
     - **Quyền hạn chức năng**: Thực hiện các thao tác tác nghiệp: Kiểm kho, Chuyển kho, Nhập hàng, Trả hàng nhập, xem Danh sách hàng hóa, xem Báo cáo tồn kho. Ẩn các chức năng quản lý danh mục nhạy cảm và thẻ kho sổ sách (Nhà cung cấp, Báo cáo thẻ kho).

---

## 2. Bảng CSDL & Tiện ích liên quan
- **Bảng CSDL tham gia**:
  - `TaiKhoan`: `MaTaiKhoan`, `TenDangNhap`, `MatKhauHash`, `MaVaiTro`, `MaNV`, `TrangThai` (`HOAT_DONG` / `KHOA`).
  - `VaiTro`: `MaVaiTro`, `TenVaiTro`, `MoTa` (1: Quản lý, 2: Thủ kho, 3: Nhân viên kho).
  - `NhanVien`: `MaNV`, `HoTen`, `DienThoai`, `Email`, `TrangThai`.
  - `TaiKhoanKho`: `MaTaiKhoan`, `MaKho` (Liên kết phân quyền kho phụ trách).
  - `Kho`: `MaKho`, `TenKho`, `DiaChi`, `TrangThai` (`HOAT_DONG`).
- **Các lớp tiện ích**:
  - `PasswordHelper.cs`: Băm mật khẩu một chiều SHA-256 thuần (`HashPassword`), đối soát mật khẩu (`VerifyPassword`).
  - `SessionContext.cs`: Lưu giữ phiên đăng nhập gồm `MaTaiKhoan`, `TenDangNhap`, `MaNV`, `HoTen`, `MaVaiTro`, `TenVaiTro`, `MaKhoHienTai`, `TenKhoHienTai`, cờ `IsDangXuat`, và các cờ vai trò: `IsAdmin` / `IsQuanLy`, `IsThuKho`, `IsNhanVienKho`. Phương thức `XoaPhien()` giải phóng ngữ cảnh khi đăng xuất.

---

## 3. Luồng màn hình & Giao diện

### 3.1. Form Đăng nhập (`frmDangNhap`)
- **Kích thước form**: Form Size Nhỏ gọn gàng (`ClientSize = (444, 321)`), `FormBorderStyle = FixedDialog`, `MaximizeBox = false`, `MinimizeBox = false`, `StartPosition = CenterScreen`.
- **Thành phần giao diện**:
  - Tiêu đề header: `HỆ THỐNG QUẢN LÝ KHO PHÚ QUỐC` (`Segoe UI, 12pt, Bold`), phụ đề `Đăng nhập để xác thực quyền hạn và kho làm việc`.
  - Ô nhập Tên đăng nhập: `txtTenDangNhap`, sự kiện `Leave` tự động nhận diện vai trò tài khoản để nạp danh sách kho tương ứng vào `cboKhoLamViec`. Phím `Enter` tự động chuyển con trỏ sang ô Mật khẩu.
  - Ô nhập Mật khẩu: `txtMatKhau` (`UseSystemPasswordChar = true`), phím `Enter` kích hoạt đăng nhập ngay.
  - CheckBox Hiện mật khẩu: `chkHienMatKhau` bật/tắt hiển thị mật khẩu.
  - ComboBox Chi nhánh kho: `cboKhoLamViec`:
    - Nếu là Quản lý: Có lựa chọn "Tất cả kho" ở đầu danh sách + tất cả các kho; mở `Enabled = true`.
    - Nếu là Thủ kho: Nạp đúng 1 kho duy nhất được phân công; khóa `Enabled = false`.
    - Nếu là Nhân viên kho: Nạp kho được phân công; không có lựa chọn "Tất cả kho".
  - Nút hành động:
    - Nút `Đăng nhập` (`btnDangNhap`): Nút chính, phím tắt `Enter` (`AcceptButton`), màu nền mặc định WinForms chuẩn.
    - Nút `Thoát` (`btnThoat`): Nút hủy, phím tắt `Esc` (`CancelButton`), đóng ứng dụng nếu chưa đăng nhập.

### 3.2. Luồng Khởi động Chương trình (`Program.cs`)
- Vòng lặp `while (true)`:
  - Khởi tạo `frmDangNhap.ShowDialog()`.
  - Nếu kết quả `!= DialogResult.OK`: Thoát chương trình sạch sẽ.
  - Nếu `== DialogResult.OK`: Mở `frmMain` qua `Application.Run(new frmMain())`.
  - Khi `frmMain` đóng:
    - Nếu `SessionContext.IsDangXuat == true`: Gọi `SessionContext.XoaPhien()` và tiếp tục vòng lặp để quay lại `frmDangNhap`.
    - Ngược lại: Thoát ứng dụng hoàn toàn.

### 3.3. Phân quyền trên `frmMain` (`PhanQuyenMenu()`)
- Khi `frmMain` tải dữ liệu:
  - Cập nhật tiêu đề cửa sổ: `Quản Lý Kho Phú Quốc - [{TenKhoHienTai}]`.
  - Cập nhật StatusStrip: `Người dùng: {HoTen} ({TenVaiTro})` | `Kho: {TenKhoHienTai}` | `Thời gian`.
  - Phân quyền các mục Menu:
    - **Quản lý (`IsAdmin` / `IsQuanLy`)**: Toàn quyền mọi phân hệ; `mnuDoiKho.Visible = true; mnuDoiKho.Enabled = true;`
    - **Thủ kho (`IsThuKho`)**: Toàn quyền nghiệp vụ kho; khóa `mnuDoiKho.Visible = false;` vì quản lý 1 kho duy nhất.
    - **Nhân viên kho (`IsNhanVienKho`)**: Khóa `mnuDoiKho.Visible = false;`, thực hiện Kiểm kho, Chuyển kho, Nhập hàng, Trả hàng nhập, xem Danh sách hàng hóa, xem Báo cáo tồn kho; ẩn các mục quản lý Nhà cung cấp và Thẻ kho.

---

## 4. Quy tắc Nghiệp vụ & Bảo mật
1. Mật khẩu được băm SHA-256 một chiều, không bao giờ so sánh hoặc lưu trữ dạng văn bản thuần (plain text).
2. Tài khoản ở trạng thái khóa (`TrangThai != 'HOAT_DONG'`) bị từ chối đăng nhập kèm thông báo rõ ràng: *"Tài khoản này hiện đang bị khóa hoặc ngưng hoạt động. Vui lòng liên hệ Quản trị viên để được hỗ trợ."*
3. Thông tin đăng nhập sai sẽ hiển thị thông báo chung: *"Tên đăng nhập hoặc mật khẩu không chính xác."* để tăng cường an ninh.
4. Chống SQL Injection 100% bằng cách sử dụng `SqlParameter` trong mọi câu lệnh truy vấn qua `DataProcessing.ReadData`.
5. Đảm bảo 100% giao diện và thông báo sử dụng tiếng Việt có dấu, font `Segoe UI, 9.5pt` theo quy chuẩn hệ thống.
