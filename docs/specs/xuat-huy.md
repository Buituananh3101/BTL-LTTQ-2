# Đặc tả Chức năng: Xuất hủy (Waste-Disposal Export)

> **Mục tiêu**: Chuẩn hóa toàn diện phân hệ Xuất hủy theo khuôn mẫu LEGO hệ thống dựa trên giao diện thực tế hệ thống.

---

## 1. CSDL, ViewModel & Cấu trúc File

### 1.1. Bảng CSDL tham gia:
- [`PhieuXuat`](../../BTL-LTW-QuanLiKho-2/Models/PhieuXuat.cs): `MaPhieuXuat`, `MaPhieu`, `LoaiPhieuXuat: 'XUAT_HUY'`, `MaKho`, `MaNvlap`, `MaNvxacNhan`, `NgayLap`, `NgayXacNhan`, `TrangThai` (`NHAP`/`DA_XAC_NHAN`/`HUY`), `GhiChu`, `DaDanhDau`. Filtered Index `UQ_PhieuXuat_MaDonBan` (`WHERE MaDonBan IS NOT NULL`).
- [`ChiTietPhieuXuat`](../../BTL-LTW-QuanLiKho-2/Models/ChiTietPhieuXuat.cs): `MaCtphieuXuat`, `MaPhieuXuat`, `MaHang`, `SoLuongXuat`, `DonGiaXuat`. Filtered Index `UQ_ChiTietPhieuXuat_MaCTDonBan` (`WHERE MaCTDonBan IS NOT NULL`).
- [`vw_BienDongKho`](../../migrations/sql/01_init_schema.sql): View tính biến động kho (tự trừ tồn kho khi `LoaiPhieuXuat = 'XUAT_HUY'` & `TrangThai = 'DA_XAC_NHAN'`).
- [`NhatKyThaoTac`](../../BTL-LTW-QuanLiKho-2/Models/NhatKyThaoTac.cs): Ghi log kiểm toán.

### 1.2. ViewModels sử dụng:
- [`XuatHuyIndexVM`](../../BTL-LTW-QuanLiKho-2/ViewModels/XuatHuy/XuatHuyViewModels.cs): Màn hình Index, `PagedData`, lọc sao `DaDanhDauFilter`, danh sách Người tạo & Xuất hủy.
- [`XuatHuyFilterVM`](../../BTL-LTW-QuanLiKho-2/ViewModels/XuatHuy/XuatHuyViewModels.cs): Bộ lọc (`DateFilterType` mặc định `THANG_NAY`, `Statuses`, `CreatorIds`, `ConfirmerIds`, `Q`, `DaDanhDau`).
- [`XuatHuyListItemVM`](../../BTL-LTW-QuanLiKho-2/ViewModels/XuatHuy/XuatHuyViewModels.cs): Dòng danh sách (`XH00000X`, `TongSoLuong`, `TongGiaTriHuy`, `Lines`).
- [`XuatHuyFormVM`](../../BTL-LTW-QuanLiKho-2/ViewModels/XuatHuy/XuatHuyViewModels.cs): Dữ liệu form lập/sửa, `NhanViens`, `IsReadOnly`.
- [`SaveXuatHuyRequest`](../../BTL-LTW-QuanLiKho-2/ViewModels/XuatHuy/XuatHuyViewModels.cs): DTO nhận JSON lưu tạm/hoàn thành.
- [`UpdateXuatHuyNoteRequest`](../../BTL-LTW-QuanLiKho-2/ViewModels/XuatHuy/XuatHuyViewModels.cs): DTO cập nhật nhanh ghi chú inline.

### 1.3. Cấu trúc View & Script:
- `Views/XuatHuy/Index.cshtml`: Layout `.kiot-container`, `.kiot-main-col`, toolbar hệ thống, phím tắt F4.
- `Views/XuatHuy/_XuatHuyFilterSidebar.cshtml`: Sidebar lọc thời gian (Tháng này), trạng thái, người tạo, người xuất hủy.
- `Views/XuatHuy/_XuatHuyTable.cshtml`: Bảng chứng từ, sao bookmark, accordion `tr.dtl`, `_Pagination.cshtml`.
- `Views/XuatHuy/Create.cshtml`, `Edit.cshtml`, `Details.cshtml`: Màn hình tạo, sửa và xem chi tiết (read-only).
- `Views/XuatHuy/_XuatHuyForm.cshtml`: Partial form 2 cột chuẩn `kiem-kho.css` (lưới hàng hóa, F3 autocomplete, F4 panel, F9/F10).
- `wwwroot/css/xuat-huy.css` & `kiem-kho.css`: CSS bảng xuất hủy, panel thông tin F4, trạng thái active `.open`.
- `wwwroot/js/xuat-huy-index.js`: Script lọc AJAX, sao bookmark, phím tắt F3/F4, accordion `tr.dtl`, in ấn, xuất Excel.
- `wwwroot/js/xuat-huy-form.js`: Script tìm kiếm hàng F3, bảng hàng xuất hủy, import Excel, phím tắt F3/F4/F9/F10.

---

## 2. Luồng màn hình & Tương tác

### 2.1. Màn hình Danh sách (`Index`)
- **Toolbar (`.kiot-toolbar-row`)**: Ô tìm kiếm `#keywordSearch` ("Theo mã xuất hủy (F3)"), nút `+ Xuất hủy`, nút `Xuất file` (Danh sách / Chi tiết), `Tùy chọn hiển thị cột` (9 cột dữ liệu), nút tải lại.
- **Sidebar (`.kiot-sidebar-col`)**:
  - Tiêu đề "Xuất hủy", nút tròn F4 thu gọn/mở rộng sidebar.
  - Trạng thái: Checkbox `Phiếu tạm` (`NHAP`), `Hoàn thành` (`DA_XAC_NHAN`), `Đã hủy` (`HUY`).
  - Thời gian: Mặc định `Tháng này` (`THANG_NAY`), hỗ trợ Dual Calendar tùy chỉnh ngày.
  - Người tạo / Người xuất hủy: Multi-select dropdown (`_KiotMultiSelectFilter.cshtml`).
- **Đánh dấu Sao Bookmark**: Header lọc sao (`DaDanhDau`); click sao dòng lưu trực tiếp qua AJAX `ToggleFavorite`.
- **Cấu trúc Bảng (`_XuatHuyTable`)**:
  - Dòng tổng hợp `summary-row` cuộn tự nhiên cùng bảng, hiển thị `TongGiaTriHuy.Sum()`.
  - Checkbox `#selectAllRows` và `.row-select` từng dòng; Sao bookmark header và từng dòng.
  - Các cột: Mã xuất hủy, Tổng giá trị hủy, Thời gian, Thời gian tạo, Chi nhánh, Người xuất hủy, Người tạo, Ghi chú, Trạng thái.
- **Bung mở chi tiết (`tr.product-detail-row` qua LEGO `KiotFilter.initRowDetail`)**:
  - Trượt slideDown/slideUp mượt mà 150ms bọc trong `.product-detail-panel-wrap`.
  - **Khung nội dung chi tiết cố định 992px** (`.xh-detail-fixed-wrap.kiot-detail-fixed-wrap`) chuẩn Nhà cung cấp, không bị kéo dãn trên màn hình rộng nhiều cột.
  - Viền xanh `#0070f3` khép kín hình chữ nhật 4 cạnh liền mạch (`tr.product-row.selected > td` và `tr.product-detail-row > td`), tự động gán `.last-visible-col` cho ô cuối cùng.
  - Tự động đồng bộ `colspan` với số cột hiển thị qua LEGO `KiotFilter.initColumnToggle`.
  - Header chi tiết: Mã phiếu, badge trạng thái, người tạo, người xuất hủy, ngày xuất hủy, chi nhánh.
  - Bảng dòng hàng (`dtl-tbl`): 5 cột `Mã hàng`, `Tên hàng`, `SL hủy`, `Giá vốn`, `Giá trị hủy` kèm ô lọc inline.
  - Khối dưới: Textarea ghi chú (placeholder "Chưa có") và tổng kết `Tổng số lượng hủy`, `Tổng giá trị hủy`.
  - Hàng nút hành động: Trái (`Hủy` qua modal `KiotConfirm`, `Sao chép`, `Xuất file`), Phải (`Lưu` ghi chú AJAX, `In`, `Mở phiếu`).

### 2.2. Màn hình Lập / Sửa / Chi tiết (`Create` / `Edit` / `Details`)
- Tự điều hướng: Vào `Edit/{id}` với phiếu đã hoàn thành/hủy tự chuyển tiếp sang `Details/{id}`.
- Chế độ chỉ đọc (`IsReadOnly = true`): Ẩn ô tìm kiếm F3, khóa input, hiện banner xác nhận.
- Header: Nút `← Xuất hủy`, ô `Tìm mặt hàng (F3)` gợi ý tức thì, nút tải mẫu và import Excel.
- Bảng hàng (Trái): Trống hiện empty box thân thiện; có hàng hiện bảng STT, Mã, Tên, ĐVT, SL hủy, Giá vốn, Thành tiền, nút Xóa. Chọn trùng tăng SL lên 1.
- Panel F4 (Phải): Nút tròn F4 đóng/mở panel phải (.panel-toggle tự động bám mép phải right: 8px khi thu gọn). Người tạo / xuất hủy (hiển thị tên tài khoản đăng nhập kèm icon người đồng bộ 100% với Kiểm kho và Nhập hàng, lưu `MaNvxacNhan` qua hidden input), ngày giờ (bộ chọn popup `.dtpop` chuẩn Kiểm kho / Nhập hàng: chọn ngày/giờ không tự tắt, đóng khi click ngoài/Escape, định vị scrollTop mượt mà), mã phiếu (`XH{0:D6}` tự sinh), badge trạng thái, tổng giá trị đỏ đậm, ghi chú. Hai nút đáy: `Lưu tạm (F9)`, `Hoàn thành (F10)`.

---

## 3. Quy chuẩn nghiệp vụ & Bảo mật
1. Mọi thao tác lưu và hoàn thành phiếu đều bọc trong giao dịch `Database.BeginTransactionAsync()`.
2. Phiếu đã hoàn thành (`DA_XAC_NHAN`) bị khóa chỉnh sửa nội dung chi tiết.
3. Hủy phiếu hoàn thành tự động hoàn lại tồn kho sổ sách qua view `vw_BienDongKho`.
4. Không tạo bảng tồn kho tĩnh; tuân thủ triệt để 12 luật cốt lõi trong `AGENTS.md`.
