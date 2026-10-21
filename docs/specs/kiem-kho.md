# Đặc tả Chức năng: Kiểm kho (Stock Take)

> **Mục tiêu**: Chuẩn hóa toàn diện phân hệ Kiểm kho theo khuôn mẫu LEGO hệ thống của module Danh sách Hàng hóa.

---

## 1. Các bảng CSDL, ViewModel & Cấu trúc File

### 1.1. Bảng CSDL tham gia:
- [`PhieuKiemKe`](../../BTL-LTW-QuanLiKho-2/Models/PhieuKiemKe.cs): Thông tin phiếu (`MaPhieuKiemKe`, `MaPhieu`, `MaKho`, `MaNvlap`, `ThoiDiemChot`, `TrangThai: NHAP / DA_DIEU_CHINH / HUY`, `GhiChu`, `DaDanhDau`).
- [`ChiTietPhieuKiemKe`](../../BTL-LTW-QuanLiKho-2/Models/ChiTietPhieuKiemKe.cs): Dòng kiểm (`MaHang`, `SoLuongTheoSo`, `SoLuongThucTe`, `DonGia`).
- [`PhieuDieuChinh`](../../BTL-LTW-QuanLiKho-2/Models/PhieuDieuChinh.cs) & [`ChiTietPhieuDieuChinh`](../../BTL-LTW-QuanLiKho-2/Models/ChiTietPhieuDieuChinh.cs): Tự sinh khi cân bằng kho (`DA_DUYET`).
- [`NhatKyThaoTac`](../../BTL-LTW-QuanLiKho-2/Models/NhatKyThaoTac.cs): Ghi log kiểm toán thao tác.

### 1.2. ViewModels sử dụng:
- [`PhieuKiemKeIndexVM`](../../BTL-LTW-QuanLiKho-2/ViewModels/KiemKho/KiemKhoViewModels.cs): Dữ liệu danh sách, `PagedData`, lọc sao `DaDanhDauFilter`.
- [`PhieuKiemKeFilterVM`](../../BTL-LTW-QuanLiKho-2/ViewModels/KiemKho/KiemKhoViewModels.cs): Bộ lọc (`CreatorIds`, `DateFilterType`, `Statuses`, `Q`, `DaDanhDau`).
- [`PhieuKiemKeListItemVM`](../../BTL-LTW-QuanLiKho-2/ViewModels/KiemKho/KiemKhoViewModels.cs): Dữ liệu dòng, cờ `DaDanhDau`, mảng `Lines` chi tiết.
- [`PhieuKiemKeFormVM`](../../BTL-LTW-QuanLiKho-2/ViewModels/KiemKho/KiemKhoViewModels.cs): Dữ liệu form lập/sửa/chi tiết.
- [`SaveStockTakeRequest`](../../BTL-LTW-QuanLiKho-2/ViewModels/KiemKho/KiemKhoViewModels.cs): DTO nhận JSON lưu tạm/cân bằng.
- [`UpdateNoteRequest`](../../BTL-LTW-QuanLiKho-2/ViewModels/KiemKho/KiemKhoViewModels.cs): DTO cập nhật nhanh ghi chú inline.

### 1.3. Cấu trúc View & Script:
- `Views/PhieuKiemKe/Index.cshtml`: Layout `.kiot-container`, `.kiot-main-col`, `.kiot-toolbar-row`, nút F4.
- `Views/PhieuKiemKe/_PhieuKiemKeFilterSidebar.cshtml`: Sidebar lọc thời gian, trạng thái, người tạo.
- `Views/PhieuKiemKe/_PhieuKiemKeTable.cshtml`: Bảng phiếu, sao bookmark, chi tiết inline `tr.dtl`, phân trang qua `_Pagination.cshtml`.
- `Views/PhieuKiemKe/Create.cshtml` & `Edit.cshtml`: Màn hình lập và sửa phiếu kiểm.
- `Views/PhieuKiemKe/_PhieuKiemKeForm.cshtml`: Partial form 2 cột hệ thống dùng chung.
- `wwwroot/css/kiem-kho.css`: CSS bổ trợ popup lịch và form 2 cột.
- `wwwroot/js/phieu-kiem-ke-index.js`: Script lọc AJAX, đánh dấu sao, F4/F3, accordion `tr.dtl`.
- `wwwroot/js/phieu-kiem-ke-form.js`: Script tính lệch realtime, F3/F9/F10, kiểm gần đây.

---

## 2. Luồng màn hình & Tương tác

### 2.1. Màn hình Danh sách (`Index`)
- **Toolbar (`.kiot-toolbar-row`)**: Ô tìm kiếm `#keywordSearch` (F3 debounce), nút `+ Kiểm kho`, nút `Xuất file`, nút `Hiển thị cột`, tải lại bảng, trợ giúp.
- **Sidebar (`.kiot-sidebar-col`)**:
  - Tiêu đề "Phiếu kiểm kho", nút tròn F4 thu gọn/mở rộng sidebar.
  - Thời gian: 2 radio (Toàn thời gian / Tùy chỉnh), popup chọn nhanh 3 cột và popup lịch 2 tháng Dual Calendar.
  - Trạng thái: Checkbox Phiếu tạm (`NHAP`), Đã cân bằng (`DA_DIEU_CHINH`), Đã hủy (`HUY`).
  - Người tạo: Multi-select chuẩn chuyên nghiệp (`.kiot-multiselect-box`, `.kiot-pill-tag`, `+N khác`, tìm kiếm sticky).
- **Đánh dấu Sao Bookmark (Chuẩn Hàng hóa)**:
  - Header bảng: Icon sao `.star-btn.star-header-filter` cho phép click lọc danh sách các phiếu có sao (`DaDanhDau`).
  - Dòng bảng: Nút sao `.star-btn` đổi màu cam `#f97316` và lưu trạng thái vào CSDL qua API `ToggleFavorite`.
- **Sắp xếp cột 3 trạng thái (Chuẩn Hàng hóa)**:
  - Click lần 1: Tăng dần (ASC, hiện icon `bi-arrow-up`).
  - Click lần 2: Giảm dần (DESC, hiện icon `bi-arrow-down`).
  - Click lần 3: Bỏ sắp xếp (về mặc định theo thời gian chốt mới nhất).
  - Áp dụng trên 11 cột (`ma`, `tg`, `tt`, `cl`, `tang`, `giam`, `cb`, `slt`, `ghi`, `st`, `kho`), hỗ trợ căn lề số học `.num` và đồng bộ qua AJAX/Export.
- **Bung mở chi tiết (`tr.dtl`) & Hiển thị ổn định không rung giật (Chuẩn Nhà cung cấp)**:
  - Bảng `#stockTakeTable` cấu hình `width: max-content; min-width: 760px; border-collapse: separate !important; border-spacing: 0 !important;` đồng bộ Nhà cung cấp (`#supplierTable`), không ép tràn cố định 1450px.
  - Tối ưu Drawer ổn định tuyệt đối: loại bỏ xung đột hoạt ảnh đồng thời khi đóng mở nhiều dòng, dùng `.stop(true, true)` chống hàng đợi click dồn dập, gán `.last-visible-col` trực tiếp không gây DOM layout thrashing, loại bỏ negative margin trên `.dp-tabs`, tích hợp `scrollbar-gutter: stable` ngăn co giật ngang khi thanh cuộn xuất hiện.
  - Tái sử dụng khối Lego `KiotFilter.initEntityActions`: Nút Hủy (`.btn-cancel-doc`) qua modal xác nhận (chỉ hủy phiếu tạm). Các nút Sao chép, Xuất file, Mở phiếu, Lưu ghi chú, In.
- **Xuất file Excel chuẩn chuyên nghiệp (`.xlsx`)**:
  - Hỗ trợ 2 chế độ qua `ClosedXML`:
    + **Xuất danh sách phiếu** (`type = "list"`): Xuất danh sách 13 cột theo bộ lọc hiện tại.
    + **Xuất chi tiết dòng hàng** (`type = "detail"`): Xuất 16 cột chi tiết dòng hàng kiểm kê (hỗ trợ xuất riêng 1 phiếu từ nút dòng).
  - Định dạng header xanh hệ thống `#0066cc`, viền ô, căn lề số/ngày và auto-fit độ rộng cột chuẩn mực.
- **Phân trang**: Tái sử dụng `_Pagination.cshtml` dùng chung qua AJAX.

### 2.2. Màn hình Lập / Sửa phiếu (`Create` / `Edit`)
- **Cột trái (Lưới kiểm hàng)**:
  - Ô tìm kiếm F3: Autocomplete gợi ý hàng hóa, quét trùng tự động `+1` số Thực tế.
  - Nút **Chọn theo nhóm**: Modal chọn nhóm nạp hàng loạt sản phẩm Chưa kiểm.
  - 4 Tab lọc: Tất cả, Khớp, Lệch, Chưa kiểm (hiển thị số lượng trên tab).
  - Lưới hàng hóa: `table-layout: fixed;`, cột Tên hàng cố định 280px tự xuống dòng.
  - **Xác nhận xóa dòng hàng**: Nhấp icon thùng rác kích hoạt modal xác nhận hệ thống ("Bỏ qua" / "Đồng ý xóa"), ngăn xóa nhầm dữ liệu.
- **Cột phải (Panel thông tin & Thao tác)**:
  - Nút gập mở panel F4. Bộ chọn Ngày giờ `#fDate` & `#dtPop` không chớp tắt, đóng bằng Escape.
  - 2 Nút hành động đáy: **Lưu tạm (F9)** và **Cân bằng kho (F10)**.

---

## 3. Nghiệp vụ Backend & Cân bằng kho (`Complete`)
1. **Lưu tạm (`SaveDraft`)**: Lưu phiếu trạng thái `'NHAP'`, bọc Transaction, không đổi tồn kho sổ sách.
2. **Cân bằng kho (`Complete`)**:
   - Chặn tồn âm qua `_tonKhoService.KiemTraKiemKeAsync`.
   - Bọc trong `Database.BeginTransactionAsync`.
   - Đọc lại tồn sổ sách hiện thời từ view `vw_TonKhoHienTai`.
   - Chốt giá vốn và cập nhật `ChiTietPhieuKiemKe`.
   - Sinh `PhieuDieuChinh` trạng thái `'DA_DUYET'` và các bản ghi `ChiTietPhieuDieuChinh`.
   - Chuyển trạng thái phiếu kiểm sang `'DA_DIEU_CHINH'`.
   - Ghi nhật ký vào `NhatKyThaoTac`. Commit transaction.
3. **Khóa phiếu vĩnh viễn (Luật 6)**:
   - Phiếu đã cân bằng (`DA_DIEU_CHINH`) bị khóa vĩnh viễn: cấm sửa, xóa hoặc hủy.
   - Endpoint `HuyPhieu` chỉ cho phép hủy phiếu tạm (`NHAP`).
