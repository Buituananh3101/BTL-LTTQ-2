# Đặc tả Chức năng: Quản lý Chi nhánh Kho

> **Mô-đun**: Thiết lập hệ thống ➜ Quản lý Kho  
> **Tài liệu tham chiếu**: [`.agent/rules/02-nghiep-vu-kho.md`](../../.agent/rules/02-nghiep-vu-kho.md) | [`.agent/rules/04-xac-thuc-phan-quyen.md`](../../.agent/rules/04-xac-thuc-phan-quyen.md) | [`AGENTS.md`](../../AGENTS.md)

---

## 1. Mục tiêu & Phạm vi nghiệp vụ

- **Mục tiêu**: Cung cấp giao diện chuẩn chuyên nghiệp giúp Quản lý quản lý danh mục chi nhánh kho, thêm mới kho, chỉnh sửa thông tin, phân công nhân sự và kích hoạt/ngừng hoạt động kho.
- **Phạm vi truy cập**: URL `/Kho`. Chỉ vai trò **Quản lý** (`[Authorize(Roles = "Quản lý")]`) được truy cập. Các vai trò khác bị chặn (403 ➜ `/Account/AccessDenied`).
- **Hành động nghiệp vụ**:
  - Xem danh sách kho kèm tìm kiếm theo từ khóa (Mã, Tên, Địa chỉ, Ghi chú), lọc Trạng thái (Đang hoạt động / Ngừng hoạt động).
  - Thêm mới kho: Nhập Tên kho (*chống trùng tên*), Địa chỉ, Mô tả và phân công nhân sự phụ trách (`TaiKhoanKho`).
  - Chỉnh sửa kho: Cập nhật thông tin và đồng bộ danh sách nhân sự phụ trách.
  - Ngừng hoạt động / Kích hoạt lại kho: Xác nhận bằng modal Lego `KiotConfirm` (`#kiotConfirmOv`), chặn ngừng hoạt động nếu là kho duy nhất.
  - Chống xóa cứng: Không xóa cứng bảng `Kho` để bảo toàn lịch sử chứng từ và tồn kho theo Luật 5 & 8.

---

## 2. Các bảng CSDL & ViewModels

### 2.1. Bảng CSDL (Database First)
- `Kho`: Bảng lưu chi nhánh kho (`MaKho`, `TenKho`, `DiaChi`, `MoTa`, `TrangThai`).
- `TaiKhoanKho`: Bảng phân quyền tài khoản với kho (`MaTaiKhoan`, `MaKho`, `NgayPhanCong`, `GhiChu`).
- `TaiKhoan` & `NhanVien`: Danh sách nhân sự để hiển thị và chọn phân quyền.
- `NhatKyThaoTac`: Ghi vết kiểm toán (Tạo mới, Cập nhật, Kích hoạt, Ngừng hoạt động).

### 2.2. ViewModels (`ViewModels/QuanLyKho/`)
- `KhoFilterParams.cs`: Kế thừa `BaseFilterParams` (`Q`, `TrangThais`, `SortBy`, `SortDir`, `Page`, `PageSize`).
- `KhoListItemVM.cs`: Dòng hiển thị trên bảng (Mã kho, Tên kho, Địa chỉ, Mô tả, Số nhân viên, Số chứng từ, `IsCurrentWarehouse`).
- `KhoIndexVM.cs`: Model trang Index chứa `PagedData`, `Filter`, danh sách tài khoản phân quyền và thống kê.
- `KhoFormVM.cs`: DTO nhận dữ liệu form Thêm mới/Cập nhật kho kèm Data Annotations validation.

---

## 3. Luồng màn hình & Giao diện (Chuẩn giao diện hệ thống)

### 3.1. Màn hình Danh sách (`Index.cshtml`)
- **Topbar**: Nút tìm kiếm F3, nút `+ Kho` (`#btnOpenCreateKhoModal`), nút F4 gập mở sidebar, nút làm mới `#btnReloadTable`.
- **Sidebar lọc trái (`_KhoFilterSidebar.cshtml`)**: Lọc Trạng thái qua Lego `_KiotStatusFilter.cshtml`.
- **Bảng dữ liệu AJAX (`_KhoTable.cshtml`)**:
  - Sắp xếp 3 trạng thái (`sortable-th`): Mã kho, Tên kho, Địa chỉ, Nhân sự, Trạng thái.
  - Cột: STT, Mã kho, Tên kho (kèm badge "Đang chọn"), Địa chỉ, Ghi chú, Nhân sự phụ trách, Số chứng từ, Trạng thái, Thao tác (Sửa, Khóa/Mở).
  - Phân trang AJAX: Tái sử dụng Lego `_Pagination.cshtml`.

### 3.2. Modal Thêm/Sửa Chi nhánh Kho (`_KhoModal.cshtml`)
- Giao diện Modal Overlay hệ thống (`.hh-ov`, `.hh-modal.main` max-width 860px) kế thừa chuẩn Form Tạo hàng hóa (`_HangHoaModal.cshtml`).
- **Header (`.hh-m-head`)**: Tiêu đề modal, nút đóng `X` (`.hh-x`), cụm Tabs chuyển đổi `.hh-tabs` ("Thông tin" & "Mô tả").
- **Body (`.hh-m-body`)**:
  - **Tab 1: Thông tin (`#kho-pane-info`)**:
    * Khối 1 (`.hh-fields` Grid 2 cột): Mã kho (tự động, readonly), Tên kho `*` (bắt buộc), Địa chỉ kho (`.hh-span2`).
    * Khối 2: Accordion Phân quyền nhân sự phụ trách (`.hh-sec open`, `.hh-sec-h`, `.hh-chev`): Nút Chọn tất cả / Bỏ chọn, badge số lượng, lưới thẻ Card nhân viên (`.kho-user-card-grid`, highlight viền xanh khi chọn, không dùng banner alert thừa).
  - **Tab 2: Mô tả (`#kho-pane-desc`)**:
    * Thanh công cụ định dạng `.hh-rte-bar` (Bold, Italic, Underline, Bullet list) và khung soạn thảo `.hh-rte#khoMoTaRte`.
- **Footer (`.hh-m-foot`)**: Checkbox trạng thái "Đang hoạt động" (`.hh-chk`), nút Bỏ qua, cụm Split Button Lego ("Lưu & Thêm mới", "Lưu & Sao chép") và nút chính "Lưu chi nhánh kho" (`.hh-btn.primary`).

---

## 4. Quy tắc nghiệp vụ & An toàn dữ liệu

1. **Bảo mật truy cập**: Áp dụng `[Authorize(Roles = "Quản lý")]` trên `KhoController`.
2. **Kiểm tra trùng tên kho**: Kiểm tra case-insensitive tên kho trước khi thêm hoặc sửa, báo lỗi tiếng Việt thân thiện.
3. **Bảo vệ kho duy nhất**: Chặn thao tác ngừng hoạt động nếu chỉ còn 1 kho hoạt động duy nhất trong hệ thống.
4. **Tự động phân quyền Quản lý**: Kho mới tạo tự động gán quyền truy cập cho tất cả tài khoản Quản lý trong `TaiKhoanKho`.
5. **Giao dịch toàn vẹn (Luật 7)**: Bọc lưu kho và gán tài khoản trong `BeginTransactionAsync()`.
6. **Kiểm toán (Audit Log)**: Ghi nhật ký đầy đủ qua `INhatKyThaoTacService`.
7. **Modal xác nhận Lego**: Dùng `window.KiotConfirm.show(...)` khi Ngừng hoạt động / Kích hoạt kho, không dùng `confirm()` mặc định của trình duyệt.

---

## 5. Danh mục Khối Lego tái sử dụng (Luật 10 AGENTS.md)

- [x] **Modal Thao tác & Xác nhận**: `_KhoModal.cshtml` chuẩn Form Hàng hóa + `KiotConfirm` (`#kiotConfirmOv`).
- [x] **Bộ lọc Trạng thái checkbox**: `_KiotStatusFilter.cshtml` cho trạng thái kho.
- [x] **Phân trang AJAX**: `_Pagination.cshtml` + `BaseFilterParams`.
- [x] **Thông báo Toast**: `KiotToast.success()`, `KiotToast.error()`, `KiotToast.info()`.
- [x] **Sidebar thu gọn**: `KiotFilter.initSidebar` (Phím tắt F4).

---

## 6. Ma trận Kiểm thử

| Thao tác | Dữ liệu kiểm thử | Kết quả mong đợi |
|---|---|---|
| Mở trang `/Kho` bằng `quanly` | Đăng nhập tài khoản `quanly` | Truy cập thành công, nạp bảng danh sách kho |
| Mở trang `/Kho` bằng `thukho` / `banhang` | Đăng nhập `thukho` hoặc `banhang` | Chặn truy cập (HTTP 403 ➜ `/Account/AccessDenied`) |
| Thêm kho mới trùng tên | Nhập "Kho trung tâm" | Báo lỗi trùng tên kho |
| Thêm kho mới hợp lệ | Tên: "Kho Bãi Thơm", Địa chỉ: "Bãi Thơm, Phú Quốc" | Lưu thành công, tự gán quyền cho Quản lý và nhân viên đã chọn |
| Sửa thông tin kho | Cập nhật địa chỉ, thêm nhân sự | Cập nhật thành công, đồng bộ `TaiKhoanKho` |
| Ngừng hoạt động kho duy nhất | Hệ thống chỉ còn 1 kho hoạt động | Chặn thao tác và cảnh báo hệ thống cần ít nhất 1 kho hoạt động |
| Ngừng hoạt động kho khi có nhiều kho | Bấm nút khóa tại kho phụ | Hiện modal KiotConfirm, đổi trạng thái sang "Ngừng hoạt động" |
