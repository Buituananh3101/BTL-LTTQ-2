# Đặc tả Chức năng: Nhập hàng (Goods Receipt)

> **Mục tiêu**: Quản lý chứng từ Nhập hàng từ Nhà cung cấp chuẩn giao diện & nghiệp vụ hệ thống, đồng bộ 100% với khuôn mẫu Kiểm kho (`_PhieuKiemKeForm.cshtml` & `_PhieuKiemKeTable.cshtml`). Tái sử dụng trọn vẹn các khối LEGO đã đóng gói (`KiotFilter`, `KiotToast`, `f3-product-search`, `_KiotDateFilter`...).

---

## 1. Kiến trúc CSDL, ViewModel & Bản đồ File

### 1.1. Bảng CSDL tham gia:
- `PhieuNhap`: `MaPhieuNhap`, `MaPhieu`, `MaKho`, `MaDonViCapHang`, `MaNvlap`, `NgayLap`, `TrangThai`, `GhiChu`, `DaDanhDau`, `SoHoaDon`, `GiamGia`, `TongTien`, `CanTraNcc`.
- `ChiTietPhieuNhap`: `MaCtphieuNhap`, `MaPhieuNhap`, `MaHang`, `SoLuong`, `DonGiaNhap`, `GiamGia`, `ThanhTien`.
- `DonViCapHang`: Nhà cung cấp (`TenDonVi`, `MaNcc`, `DienThoai`).
- `vw_BienDongKho`: Tự động cộng tồn kho khi `DA_XAC_NHAN`.
- `NhatKyThaoTac`: Ghi log kiểm toán thao tác nhập hàng.

### 1.2. ViewModels sử dụng (`ViewModels/NhapHang/`):
- `NhapHangIndexVM`: Dữ liệu màn hình Index, `PagedData`, lọc sao, danh sách Nhân viên.
- `NhapHangFilterVM`: Bộ lọc (`DateFilterType`, `Statuses`, `NccIds`, `CreatorIds`, `ConfirmerIds`, `SoHoaDon`, `Q`, `DaDanhDau`).
- `NhapHangListItemVM`: Dòng danh sách (`MaPhieuHienThi`, `NgayLap`, `TenNcc`, `TongTien`, `CanTraNcc`, `TrangThai`, `BadgeClass`, `DaDanhDau`).
- `NhapHangFormVM`: Dữ liệu form lập/sửa, danh sách `NhanViens`, `NhaCungCaps`, `IsReadOnly`.
- `NhapHangLineVM`: Dòng hàng hóa (`MaHang`, `MaHangNoiBo`, `TenHang`, `TenDvt`, `SoLuong`, `DonGiaNhap`, `GiamGia`, `ThanhTien`, `TonKho`).
- `SaveNhapHangRequest`: DTO submit ("Lưu tạm" hoặc "Hoàn thành").

### 1.3. Bản đồ File:
- **Controller**: `Controllers/NhapHangController.cs` (kế thừa `BaseKhoController`, DI `INhatKyThaoTacService`).
- **Views**:
  - `Views/NhapHang/Index.cshtml`: Màn hình danh sách (cột lọc trái + toolbar + bảng).
  - `Views/NhapHang/_NhapHangFilterSidebar.cshtml`: 5 khối lọc (Thời gian ALL, Trạng thái, Người tạo, Người nhập, NCC).
  - `Views/NhapHang/_NhapHangTable.cshtml`: Bảng phiếu nhập, sao header, accordion drawer inline cố định 992px chuẩn Kiểm kho.
  - `Views/NhapHang/Create.cshtml`, `Edit.cshtml`, `Details.cshtml`: Màn hình lập / sửa / xem chi tiết.
  - `Views/NhapHang/_NhapHangForm.cshtml`: Form 2 cột chuẩn Kiểm kho (`.kk-container`, `.form-wrap`, `.f-main`, `.f-panel`, `.panel-toggle` F4).
- **Styles & Scripts**:
  - `wwwroot/css/kiem-kho.css` & `wwwroot/css/nhap-hang.css`: Đồng bộ layout khung nhìn, bảng lưới và panel.
  - `wwwroot/js/nhap-hang-index.js`: Lọc AJAX, sao, Excel, LEGO `KiotFilter.initColumnToggle` & `initRowDetail`.
  - `wwwroot/js/nhap-hang-form.js`: LEGO `f3-product-search`, modal NCC đầy đủ, import Excel, phím tắt F3/F4/F9/F10, widget Nhập gần đây.

---

## 2. Luồng Nghiệp vụ & Giao diện (Đồng bộ Kiểm Kho)

### 2.1. Màn hình Danh sách Phiếu Nhập Hàng (`Index`)
- **Cột lọc trái (Sidebar - Khớp chuẩn Kiểm kho)**:
  - 1. Thời gian: `<partial name="_KiotDateFilter" />` (mặc định "Toàn thời gian" ALL + popup chọn nhanh / lịch tùy chỉnh).
  - 2. Trạng thái: Checkbox `Phiếu tạm` (`NHAP`), `Đã nhập hàng` (`DA_XAC_NHAN`), `Đã hủy` (`HUY`).
  - 3. Người tạo, 4. Người nhập, 5. Nhà cung cấp: Khối lọc multi-select.
- **Thanh công cụ (Topbar)**:
  - Tiêu đề: "Nhập hàng", ô tìm kiếm "Theo mã phiếu nhập (F3)".
  - Nút: `+ Nhập hàng` (`/NhapHang/Create`), `Xuất file ▾`, `Tùy chọn hiển thị cột` (dropdown 20 cột), `Tải lại`, `Trợ giúp`.
- **Bảng dữ liệu (`_NhapHangTable` - Chuẩn Kiểm kho)**:
  - Bọc trong `<section id="content">` và `#tableContainer.kiot-table-wrapper` đảm bảo `.kiot-table-scroll-body` có chiều cao flex cố định, cuộn 2 chiều độc lập (dọc và ngang) mượt mà khi mở drawer chi tiết.
  - Sắp xếp cột 3 trạng thái (Tăng -> Giảm -> Bình thường) chuẩn chuyên nghiệp: Click 1 sắp xếp Tăng dần (ASC), Click 2 Giảm dần (DESC), Click 3 quay về Bình thường (hủy sắp xếp, trở về mặc định theo thời gian giảm dần).
  - Bảng cuộn ngang `min-width: 1750px`, viền chữ nhật `#0070f3` khép kín (`2px` trên/dưới, `3px` hai bên) khi mở dòng.
  - Dữ liệu bắt đầu ngay sau `<thead>`, mã phiếu hiển thị dạng text đen chuẩn mực.
  - Badges trạng thái dùng Bootstrap subtle chuẩn: `bg-success-subtle text-success`, `bg-primary-subtle text-primary`, `bg-secondary-subtle text-secondary`.
  - Drawer chi tiết cố định 992px (`.kk-detail-fixed-wrap`), tab `Thông tin`, bảng con `.dtl-tbl` lọc nhanh `.fin`, `.dp-bottom` (`.dp-note` & `.sums`), `.dp-actions` (Hủy, Sao chép, Xuất file, Lưu, In, Mở phiếu/Xem chi tiết). Mở/đóng tại chỗ theo vị trí cuộn tự nhiên của người dùng, không tự động dịch chuyển màn hình.

### 2.2. Màn hình Lập / Sửa Phiếu Nhập Hàng (`_NhapHangForm`)
- **Bố cục khung nhìn**: Vừa khít viewport (`height: 100%; overflow: hidden;`), bảng cuộn kèm sticky header; footer tổng mặt hàng, tổng số lượng và nút F9/F10 ghim đáy.
- **Vùng bảng hàng hóa (Trái - `.f-main`)**:
  - Nút quay lại `←`, tiêu đề `Nhập hàng` (20px, bold).
  - Ô tìm kiếm hàng hóa: F3 autocomplete chuẩn layout ngang (badge, tên, giá, tồn, phím mũi tên/Enter).
  - Toolbar phụ: Nút nạp file Excel/CSV, nút ẩn/hiện cột (`tEye`), nút lưu ý nghiệp vụ (`tNote`).
  - Trạng thái rỗng: Khối empty box chuẩn Kiểm kho (icon `bi-file-earmark-spreadsheet`, Tải file mẫu, Chọn file dữ liệu, gợi ý F3).
  - Bảng hàng hóa: Xóa (cột 1), STT (cột 2), Mã hàng, Tên hàng (kèm tồn kho), ĐVT, Số lượng (.qty), Đơn giá nhập (.qty), Giảm giá (.qty), Thành tiền.
- **Panel thông tin (Phải - `.f-panel`)**:
  - Nút tròn F4 thu gọn/mở rộng panel (`.panel-toggle`) trượt mượt mà.
  - Người tạo + ngày giờ nhập (bộ chọn popup `.dtpop` chuẩn Kiểm kho: lịch chọn ngày và khung giờ 30 phút, định dạng `dd/MM/yyyy HH:mm`, đóng khi click ngoài/Escape).
  - Nhà cung cấp: Input autocomplete căn trái, icon tìm kiếm, nút '✕' xóa chọn + nút '+' mở modal tạo mới NCC đầy đủ.
  - Mã phiếu nhập, Số HĐ đầu vào, Kho nhập, Trạng thái chứng từ.
  - Khối thanh toán: Tổng tiền hàng, Giảm giá, Cần trả NCC (bold to nổi bật).
  - Ghi chú: Textarea 2 dòng.
  - Widget "Nhập gần đây" (`.recent`): Tự động ghi nhận lịch sử thêm/sửa hàng hóa trong phiên.
  - 2 Nút thao tác đáy ghim cố định:
    - **"Lưu tạm (F9)"** (`save` - Xanh dương): Chưa tăng tồn kho, cho phép sửa/xóa.
    - **"Hoàn thành (F10)"** (`done` - Xanh lá): Tăng tồn kho ngay lập tức, ghi nhận công nợ NCC, khóa chứng từ vĩnh viễn.

---

## 3. Khối Lego & Quy tắc Nghiệp vụ
- **Lego**: `_KiotDateFilter.cshtml`, `_Pagination.cshtml`, `KiotFilter` (ColumnToggle, RowDetail, EntityActions), `f3-product-search.js`, `KiotToast`, `.kiot-num`.
- **Mã phiếu nhập**: Tiền tố `PN` + 6 chữ số (`PN000001`), hỗ trợ nhập mã tùy biến nếu không trùng.
- **Biến động kho & Công nợ**: Tự động tăng tồn kho qua `vw_TonKhoHienTai` và tăng công nợ NCC khi phiếu `DA_XAC_NHAN`.
- **Quy tắc bất biến**: Phiếu đã hoàn thành (`DA_XAC_NHAN`) **tuyệt đối không được sửa hay xóa**. Phiếu tạm (`NHAP`) được phép sửa/xóa.
- **Giao dịch**: Lưu chứng từ luôn bọc trong `Database.BeginTransactionAsync()`.
