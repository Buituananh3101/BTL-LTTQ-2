# Đặc tả Chức năng: Quản lý Nhà cung cấp (Supplier Management)

> **Mục tiêu**: Quản lý danh mục Nhà cung cấp chuẩn giao diện & nghiệp vụ hệ thống. Tìm kiếm, lọc đa tiêu chí, Master-Detail xem lịch sử & nợ, modal thêm/sửa với Accordions, đánh dấu sao yêu thích.

---

## 1. Kiến trúc File & CSDL

### 1.1. Bản đồ File liên quan (File Map)
- **Controller**: `Controllers/NhaCungCapController.cs` (CRUD, Filter AJAX, ToggleFavorite, ToggleStatus).
- **ViewModels (`ViewModels/NhaCungCap/`)**: `NhaCungCapIndexVM`, `NhaCungCapFilterParams`, `NhaCungCapItemVM`, `NhaCungCapFormVM`, `NhaCungCapDetailVM`.
- **Views (`Views/NhaCungCap/`)**:
  - `Index.cshtml`: Khung trang, topbar công cụ, bảng dữ liệu.
  - `_NhaCungCapFilterSidebar.cshtml`: 5 khối lọc hệ thống (Nhóm, Tổng mua, Thời gian, Nợ, Trạng thái).
  - `_NhaCungCapTable.cshtml`: Bảng AJAX (`product-row`, inline panel, cột sao, ẩn/hiện 12 cột).
  - `_NhaCungCapModal.cshtml`: Modal tạo mới/sửa NCC (3 Accordion, sub-modals nhóm).
  - `_NhaCungCapDetail.cshtml`: Panel chi tiết (Tabs: Thông tin, Lịch sử nhập/trả, Nợ cần trả).
- **Styles & Scripts**: `wwwroot/js/nha-cung-cap-index.js`, `wwwroot/css/nha-cung-cap.css`.

### 1.2. Cơ sở dữ liệu (Tuân thủ 3 Nguyên tắc Vàng CSDL)
- **Bảng `DonViCapHang`**: `MaDonViCapHang` PK, `TenDonVi` NOT NULL, `MaNcc`, `DienThoai`, `Email`, `DiaChi`, `KhuVuc`, `PhuongXa`, `MaNhomNcc` (FK), `GhiChu`, `TenCongTy`, `MaSoThue`, `SoCccd`, `TrangThai` (`HOAT_DONG` / `NGUNG_GIAO_DICH`), `DaDanhDau` BIT, `NgayTao`, `NguoiTao`.
- **Bảng `NhomNhaCungCap`**: `MaNhomNcc` PK, `TenNhom` NVARCHAR(150), `GhiChu`.

---

## 2. Luồng Tương tác Nghiệp vụ Chi tiết

| Luồng | Tương tác người dùng | Frontend (JS / View) | Backend (Controller / DB) |
|---|---|---|---|
| **1. Nạp ban đầu** | Vào menu "Mua hàng" ➜ "Nhà cung cấp" | GET `/NhaCungCap` | `Index()` nạp nhóm, danh sách trang 1, tính tổng mua & nợ ➜ Render `Index.cshtml` |
| **2. Lọc & Tìm kiếm** | Gõ tìm kiếm (F3) / Nhóm / Thời gian / Trạng thái | Debounce 300ms ➜ AJAX `GET /NhaCungCap/Filter` | `Filter(...)` lọc LINQ, tính nợ/tổng mua ➜ Render `_NhaCungCapTable.cshtml` |
| **3. Phân trang & Sắp xếp** | Click số trang hoặc tiêu đề cột | Gán page/sort ➜ AJAX `GET /NhaCungCap/Filter` | `.OrderBy()`, `.Skip().Take()` ➜ Cập nhật bảng và phân trang |
| **4. Xem chi tiết (Inline Drawer)** | Click vào 1 dòng NCC trên bảng | Bung dòng chi tiết `tr.product-detail-row` trượt êm, viền xanh `selected` khép kín hình chữ nhật. 3 card thuộc tính cố định 992px (320px/card) không dãn theo khung xanh. Tab 1 sẵn, Tab 2 & 3 AJAX | Nạp thông tin NCC, lịch sử phiếu nhập, công nợ qua `GetDetail` |
| **5. Thêm mới NCC** | Bấm "+ Nhà cung cấp", điền form, bấm "Lưu" | Form validate ➜ AJAX `POST /NhaCungCap/Create` | Tự sinh `MaNcc`, lưu DB ➜ Trả JSON `{success: true}` ➜ Reload bảng |
| **6. Sửa NCC** | Bấm "Chỉnh sửa" trong chi tiết hoặc dòng | AJAX `GET /NhaCungCap/GetItem?id=X` ➜ Đổ vào modal | AJAX `POST /NhaCungCap/Edit?id=X` ➜ Cập nhật DB ➜ Reload bảng & detail |
| **7. Xóa NCC** | Bấm nút Xóa trong panel chi tiết | Modal `KiotConfirm` xác nhận | Kiểm tra `PhieuNhaps.Any()`. Nếu đã có GD ➜ Báo lỗi, gợi ý "Ngừng hoạt động" |
| **8. Quản lý Nhóm NCC** | Bấm "+ Tạo mới" hoặc ✎ sửa/xóa trong dropdown nhóm | Mở `KiotPrompt` nhập tên nhóm | `CreateGroupQuick`, `UpdateGroupQuick`, `DeleteGroupQuick` |
| **9. Đánh dấu sao** | Click sao dòng hoặc sao header | Click sao dòng: Toggle AJAX `/NhaCungCap/ToggleFavorite` tức thì âm thầm (bỏ toast). Sao header: Lọc các NCC đã đánh dấu sao | Cập nhật `DaDanhDau`, trả JSON `{success, isFavorite}` |
| **10. Chuyển đổi trạng thái** | Bấm "Ngừng hoạt động" hoặc "Kích hoạt lại" | Mở modal `#nccStatusConfirmOv` xác nhận hành động ➜ Bấm "Đồng ý" | `ToggleStatus(id)` chuyển đổi `HOAT_DONG` ⇋ `NGUNG_GIAO_DICH` |

---

## 3. Khối Lego Tái sử dụng (Tuân thủ Luật 10 & 03-giao-dien-layout.md)
- [x] **Master-Detail Accordion & Tabs**: Lego `KiotFilter.initRowDetail` bung drawer êm ái, chuyển tab, tự đồng bộ `colspan` và `.last-visible-col`.
- [x] **Thao tác Thực thể**: Lego `KiotFilter.initEntityActions` xử lý các nút Chỉnh sửa, Xóa, Ngừng hoạt động.
- [x] **Hiển thị Cột (`#colMenu`)**: Lego `KiotFilter.initColumnToggle` lưu `kiot_ncc_columns_v2`.
- [x] **Panel Chi tiết Cố định**: `.kiot-detail-fixed-wrap`, `.kiot-attr-grid`, `.kiot-attr-col` cố định 3 card 992px (320px/card), viền khép kín hình chữ nhật.
- [x] **Cột Sao Yêu Thích**: Cột sao cố định, toggle sao âm thầm và lọc sao trên header.
- [x] **Modal Xác nhận Ngừng hoạt động**: `#nccStatusConfirmOv` căn giữa, hỏi xác nhận trước khi đổi trạng thái.
- [x] **Bộ lọc Thời gian**: `<partial name="_KiotDateFilter" ... />` kết hợp `KiotFilter.initDateFilter`.
- [x] **Bộ lọc Nhóm NCC**: `<partial name="_KiotMultiSelectFilter" ... />` hỗ trợ sửa/xóa ✎ tại chỗ.
- [x] **Bộ lọc Trạng thái**: `<partial name="_KiotStatusFilter" ... />` (Pills: Đang hoạt động / Ngừng hoạt động).
- [x] **Bộ lọc Giá trị Từ - Tới**: `<partial name="_KiotValueFilter" ... />` (Tổng mua, Nợ hiện tại).
- [x] **Phân trang AJAX**: `<partial name="_Pagination" model="Model.PagedData" />`.
- [x] **Khối Accordion Form**: `.kiot-sec`, `.kiot-sec-h`, `.kiot-chev` cho các khối thông tin.
- [x] **Modal con KiotPrompt & KiotConfirm**: Thêm nhanh nhóm, sửa ✎ nhóm, xác nhận xóa.
- [x] **Format tiền tệ & Toast**: Class `.kiot-num` và `window.KiotToast` toàn hệ thống.

---

## 4. Quy tắc Nghiệp vụ Bất biến
1. **Mã nhà cung cấp**: Tự động sinh `NCC00001`, `NCC00002`... nếu để trống. Kiểm tra chống trùng lặp.
2. **Tên nhà cung cấp**: Bắt buộc nhập, tối đa 150 ký tự, không trùng lặp.
3. **Bảo vệ toàn vẹn dữ liệu chứng từ**: Tuyệt đối không xóa cứng NCC đã phát sinh phiếu nhập kho (`PhieuNhap`).
4. **Tính toán Công nợ & Tổng mua**:
   - `TongMua`: Tổng giá trị các phiếu nhập kho trạng thái hoàn thành (`DA_XAC_NHAN` / `DA_NHAN`).
   - `NoCanTraHienTai`: Tổng mua trừ tổng tiền đã thanh toán (bằng `TongMua` khi chưa có sổ quỹ).
