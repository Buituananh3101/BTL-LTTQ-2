# Danh sách Vấn đề & Kế hoạch Triển khai (Backlog)
## Hệ Thống Quản Lý Kho Hàng BTL-LTTQ-2 (Windows Forms)

> **Quy tắc vận hành**:
> - Danh sách này do Chủ dự án phê duyệt và quản lý.
> - AI khi thực hiện chỉ nhận nhiệm vụ từ danh sách này, phát triển và sửa đúng phần được giao.
> - Sau khi hoàn thành và kiểm tra build thành công, đánh dấu `[x]` và cập nhật vào `PROGRESS.md`.
> - Tuyệt đối không tự ý xóa bỏ đầu việc hoặc phá vỡ cấu trúc có sẵn.

---

## NHÓM 1: NỀN TẢNG KIẾN TRÚC & XÁC THỰC (Foundation & Auth)

- [x] **[TASK-01] Khởi tạo Solution WinForms .NET 4.7.2 & Kết nối CSDL**
  - Cấu hình chuỗi kết nối `QuanLyKhoConnection` tới `localhost\SQLEXPRESS;Database=QuanLyKhoPhuQuocLTW` trong `App.config`.
  - Xây dựng lớp tiện ích ADO.NET thuần `DAL/DataProcessing.cs` hỗ trợ `ReadData`, `ChangeData`, `ReadValue`, `ChangeDataTransaction`.
- [x] **[TASK-02] Thiết kế Form chính MDI (`frmMain`)**
  - Thiết lập thuộc tính `IsMdiContainer = true`.
  - Tạo cấu trúc MenuStrip hoàn chỉnh: Hệ thống, Danh mục, Quản lý kho, Báo cáo, Trợ giúp.
  - Tạo StatusStrip góc dưới hiển thị: Người dùng, Kho đang làm việc, Thời gian hiện tại.
- [ ] **[TASK-03] Xây dựng Lớp quản lý phiên toàn cục `SessionContext` & `PasswordHelper`**
  - Tạo `BLL/SessionContext.cs`: Lưu `MaTaiKhoan`, `TenDangNhap`, `HoTen`, `MaVaiTro`, `TenVaiTro`, `MaKhoHienTai`, `TenKhoHienTai`.
  - Tạo `BLL/PasswordHelper.cs`: Thuật toán băm SHA-256 khớp chuẩn dữ liệu trong `TaiKhoan` và `DuLieuMau.sql`.
- [ ] **[TASK-04] Xây dựng Form Đăng nhập (`frmDangNhap`) & Phân quyền Menu**
  - Màn hình đăng nhập tài khoản và mật khẩu, kiểm tra trạng thái tài khoản (`HOAT_DONG`).
  - Cho phép người dùng chọn kho làm việc ban đầu (lấy từ bảng `TaiKhoanKho` hoặc tất cả kho nếu là Admin).
  - Phân quyền ẩn/hiện các menu theo vai trò: Admin (toàn quyền), Thủ kho (kho & chứng từ), Nhân viên (bán hàng), Kế toán (báo cáo).
- [ ] **[TASK-05] Xây dựng Form Đổi mật khẩu (`frmDoiMatKhau`)**
  - Cho phép người dùng cập nhật mật khẩu mới (kiểm tra mật khẩu cũ băm SHA-256).

---

## NHÓM 2: PHÂN HỆ QUẢN LÝ DANH MỤC (CRUD Master Data)

- [ ] **[TASK-06] Hoàn thiện Form Danh sách Hàng hóa (`frmDanhSachHangHoa`)**
  - Giao diện tìm kiếm theo từ khóa (Mã/Tên/Mã vạch), lọc theo Nhóm hàng, lọc theo Tình trạng tồn (Tất cả, Còn hàng, Hết hàng).
  - DataGridView hiển thị danh sách hàng kèm số lượng tồn kho lấy động từ `vw_TonKhoHienTai`.
  - Nút chức năng: Thêm mới, Sửa, Xóa (ngừng kinh doanh), Xuất Excel, Làm mới.
- [ ] **[TASK-07] Xây dựng Form Thêm/Sửa Hàng hóa (`frmHangHoaChiTiet`)**
  - Form nhập thông tin hàng: Mã nội bộ (tự sinh), Tên hàng, Nhóm hàng, Đơn vị tính, Giá bán, Giá vốn, Vị trí, Mô tả.
  - Hỗ trợ chọn ảnh minh họa và lưu ảnh vào CSDL (bảng `HinhAnhHangHoa` hoặc cột `HinhAnh`).
- [ ] **[TASK-08] Xây dựng Form Danh mục Nhóm hàng & Đơn vị tính**
  - CRUD nhóm hàng hóa (`NhomHang`).
  - CRUD đơn vị tính cơ bản (`DonViTinh`).
- [ ] **[TASK-09] Xây dựng Form Danh mục Chi nhánh Kho (`frmKho`)**
  - CRUD thông tin kho (`Kho`).
  - Cấu hình phân công nhân sự vào kho (`TaiKhoanKho`).
- [ ] **[TASK-10] Xây dựng Form Danh mục Nhà cung cấp (`frmNhaCungCap`)**
  - CRUD thông tin nhà cung cấp (`DonViCapHang`).
  - Hiển thị công nợ hiện tại.

---

## NHÓM 3: PHÂN HỆ QUẢN LÝ KHO & CHỨNG TỪ BIẾN ĐỘNG (Logistics Documents)

- [ ] **[TASK-11] Xây dựng Form Nhập hàng từ NCC (`frmNhapHang`)**
  - Lập phiếu nhập (`PhieuNhap` + `ChiTietPhieuNhap`).
  - Tìm chọn hàng hóa đưa vào lưới chi tiết, nhập số lượng, đơn giá nhập, tính thành tiền.
  - Hỗ trợ trạng thái Lưu tạm (`NHAP`) và Hoàn thành (`DA_XAC_NHAN`).
  - Khi hoàn thành: Cập nhật giá vốn bình quân gia quyền (MAC) và tăng tồn kho trong `vw_TonKhoHienTai`.
- [ ] **[TASK-12] Xây dựng Form Kiểm kê & Cân bằng kho (`frmKiemKho`)**
  - Lập phiếu kiểm kê (`PhieuKiemKe` + `ChiTietPhieuKiemKe`).
  - Nạp số lượng tồn sổ sách hiện tại từ `vw_TonKhoHienTai`.
  - Nhập số lượng thực tế kiểm đếm, tự tính chênh lệch tăng/giảm.
  - Chức năng Cân bằng kho: Sinh `PhieuDieuChinh` trạng thái `DA_DUYET`, cập nhật `DA_DIEU_CHINH` cho phiếu kiểm, KHÓA VĨNH VIỄN phiếu kiểm.
- [ ] **[TASK-13] Xây dựng Form Chuyển kho liên chi nhánh (`frmChuyenKho`)**
  - Lập phiếu chuyển (`PhieuChuyenKho` + `ChiTietPhieuChuyenKho`).
  - Chọn kho nguồn và kho đích. Kiểm tra chặn tồn âm tại kho nguồn.
  - Quy trình 2 bước: Xuất chuyển (`DA_XUAT` - trừ kho nguồn) ➜ Nhận hàng (`DA_NHAN` - cộng kho đích).
- [ ] **[TASK-14] Xây dựng Form Trả hàng nhập cho NCC (`frmTraHangNhap`)**
  - Lập phiếu trả hàng nhập (`PhieuTraHangNhap` + `ChiTietPhieuTraHangNhap`).
  - Kiểm tra tồn kho trước khi xuất trả. Trừ tồn tại kho khi trạng thái `DA_TRA_HANG`.
- [ ] **[TASK-15] Xây dựng Form Xuất hủy hàng hỏng/hết hạn (`frmXuatHuy`)**
  - Lập phiếu xuất hủy (`PhieuXuat` loại `XUAT_HUY` + `ChiTietPhieuXuat`).
  - Kiểm tra tồn kho, trừ tồn khi trạng thái `DA_XAC_NHAN`.

---

## NHÓM 4: BÁO CÁO & TIỆN ÍCH HỆ THỐNG (Reporting & Tools)

- [ ] **[TASK-16] Xây dựng Báo cáo Tồn kho Đa chi nhánh (`frmBaoCaoTonKho`)**
  - Truy vấn dữ liệu từ `vw_TonKhoHienTai`.
  - Hiển thị danh sách tồn kho theo từng kho hoặc ma trận tổng hợp các kho.
  - Thống kê tổng số mặt hàng, tổng số lượng tồn, tổng giá trị tồn kho.
  - Bộ lọc: Chi nhánh kho, nhóm hàng, tìm kiếm từ khóa.
- [ ] **[TASK-17] Xây dựng Báo cáo Thẻ kho chi tiết (`frmTheKho`)**
  - Lấy lịch sử biến động từ View `vw_BienDongKho`.
  - Chọn mặt hàng, chọn kho, chọn khoảng thời gian (Từ ngày - Đến ngày).
  - Tính toán: Tồn đầu kỳ, chi tiết từng giao dịch nhập/xuất/chuyển/điều chỉnh, tồn lũy kế, tồn cuối kỳ.
- [ ] **[TASK-18] Tiện ích Xuất dữ liệu Excel**
  - Hỗ trợ xuất DataGridView ra file Excel (`.xlsx` hoặc `.csv`) cho mọi màn hình danh sách và báo cáo.
