# Đặc tả Chức năng: Chuyển kho (Quản lý Chuyển kho)

> **Mô tả**: Quản lý luồng chuyển hàng hóa giữa các kho nội bộ (Kho gửi -> Kho nhận), kiểm soát tồn kho theo thời gian thực, xử lý nhận hàng từng phần và thất thoát trên đường chuyển.

---

## 1. Mục tiêu & Phạm vi nghiệp vụ
- **Người dùng**: Quản lý kho, Thủ kho các chi nhánh.
- **Mã tiền tố chứng từ**: `CK` (Ví dụ: `CK000001`, `CK000002`).
- **4 Trạng thái chứng từ (`TrangThai` trong CSDL)**:
  1. `'NHAP'`: **Phiếu tạm** (Chưa trừ tồn kho gửi, có thể chỉnh sửa/xóa/hủy).
  2. `'DA_XUAT'`: **Đang chuyển** (Đã trừ tồn kho gửi, đang vận chuyển, chờ kho nhận).
  3. `'DA_NHAN'`: **Đã nhận** (Kho nhận đã nhận hàng, cộng tồn kho nhận; khóa vĩnh viễn không được hủy).
  4. `'HUY'`: **Đã hủy** (Phiếu tạm hoặc phiếu đang chuyển bị hủy; nếu hủy khi đang chuyển thì tự động hoàn trả tồn cho kho gửi).

---

## 2. Quy tắc Nghiệp vụ cốt lõi & Tồn kho
1. **Kiểm tra & Chặn tồn âm**:
   - Khi bấm **"Chuyển hàng"** (từ `NHAP` -> `DA_XUAT`), hệ thống dùng `ITonKhoService.KiemTraXuatKhoAsync` kiểm tra tồn hiện tại tại `MaKhoNguon`. Nếu bất kỳ mặt hàng nào có `SoLuongChuyen > TonKho`, hệ thống chặn đứng giao dịch và báo lỗi tiếng Việt.
2. **Biến động tồn kho (`vw_BienDongKho`)**:
   - **Kho gửi**: Bị trừ tồn ngay khi chuyển sang trạng thái `'DA_XUAT'` hoặc `'DA_NHAN'`.
   - **Kho đích**: Chỉ được cộng tồn khi chuyển sang trạng thái `'DA_NHAN'` với số lượng thực nhận `ct.SoLuongNhan` (`ISNULL(ct.SoLuongNhan, ct.SoLuong)`).
   - **Thất thoát hàng hóa**: Khi `SoLuongNhan < SoLuongChuyen`, số lượng thiếu được tính là hao hụt/thất thoát trên đường chuyển. Bắt buộc nhập lý do vào `GhiChuNhan`.
3. **Phân quyền & Ràng buộc Kho**:
   - Chỉ người dùng đang ở kho nguồn (`MaKhoNguon == CurrentKhoId`) mới được quyền bấm **"Chuyển hàng"**.
   - Chỉ người dùng đang ở kho đích (`MaKhoDich == CurrentKhoId`) mới được quyền bấm **"Nhận hàng"**.
   - Không cho phép chuyển hàng giữa cùng một kho (`MaKhoNguon != MaKhoDich`).
4. **Quy tắc Hủy phiếu**:
   - Phiếu tạm (`NHAP`): Hủy không ảnh hưởng tồn kho.
   - Phiếu đang chuyển (`DA_XUAT`): Hủy sẽ chuyển trạng thái sang `'HUY'`. View `vw_BienDongKho` tự động loại bỏ phiếu khỏi danh sách trừ tồn, giúp tồn kho gửi được phục hồi ngay lập tức mà không sinh chứng từ rác.
   - Phiếu đã nhận (`DA_NHAN`): **Khóa vĩnh viễn, tuyệt đối không được hủy** (Tuân thủ Luật 5 AGENTS.md).

---

## 3. Cấu trúc CSDL & ViewModels

### 3.1. CSDL (`QuanLyKhoPhuQuocLTW`)
- **`PhieuChuyenKho`**: Bổ sung `MaPhieu` (VARCHAR(30), Unique Filtered), `DaDanhDau` (BIT), `GhiChuNhan` (NVARCHAR(500)), `TongSoLuongChuyen`, `TongSoLuongNhan`, `TongGiaTriChuyen`.
- **`ChiTietPhieuChuyenKho`**: Bổ sung `SoLuongNhan` (DECIMAL(18,2) NULL), `DonGiaChuyen` (DECIMAL(18,2)), `ThanhTien` (DECIMAL(18,2)).
- **`vw_BienDongKho`**: Cập nhật nhánh nhận hàng cộng theo `ISNULL(ct.SoLuongNhan, ct.SoLuong)`.

### 3.2. ViewModels (`ViewModels/ChuyenKho/`)
- `ChuyenKhoIndexVM`: Model màn hình danh sách, chứa dữ liệu phân trang và bộ lọc.
- `ChuyenKhoListItemVM`: Dòng dữ liệu hiển thị trên bảng và chi tiết mở rộng.
- `ChuyenKhoFilterVM`: Tham số lọc (Hướng chuyển, Trạng thái, Thời gian, Từ khóa).
- `ChuyenKhoFormVM`: Model màn hình Lập/Sửa phiếu chuyển.
- `ChuyenKhoLineVM`: Chi tiết dòng hàng hóa chuyển.
- `SaveChuyenKhoRequest`: DTO nhận dữ liệu JSON khi lưu phiếu tạm / chuyển hàng.
- `ReceiveChuyenKhoRequest`: DTO nhận dữ liệu khi kho đích bấm nhận hàng.

---

## 4. Giao diện & Các Khối Lego Bắt Buộc

### 4.1. Màn hình 1: Danh sách (`/ChuyenKho/Index`)
- **Sidebar lọc bên trái**:
  - Hướng chuyển (Radio: Tất cả, Chuyển đi từ kho này, Chuyển đến kho này).
  - Trạng thái (Checkbox Lego: Phiếu tạm, Đang chuyển, Đã nhận, Đã hủy).
  - Thời gian (`_KiotDateFilter` + `KiotFilter.initDateFilter`).
- **Topbar**: Tiêu đề "Chuyển hàng", ô tìm kiếm mã phiếu F3, nút "+ Chuyển hàng", "Xuất file", nút tùy chọn hiển thị cột (`#btnColsDropdown` + `#colMenu`).
- **Bảng dữ liệu Master-Detail chuẩn Kiểm tồn & hệ thống (`PhieuKiemKe`)**:
  - Dòng cha + Dòng chi tiết viền chữ nhật `#0070f3` (`KiotFilter.initRowDetail`).
  - Dòng tổng hợp `tr.summary-row` ngay dưới thead hiển thị tổng SL chuyển, tổng SL nhận, tổng Giá trị chuyển.
  - Drawer chi tiết chuẩn Kiểm tồn (`PhieuKiemKe`):
    - Tab bar `.dp-tabs`, tiêu đề mã phiếu in đậm `18px`, badge trạng thái pill chuẩn mực.
    - Hàng thông tin tóm tắt `.meta` (Kho gửi, Kho nhận, Người tạo/chuyển/nhận, Ngày tạo/chuyển/nhận).
    - Bảng danh sách hàng hóa `.dtl-tbl` với header `#f8fafc`, hàng tìm kiếm nhanh `tr.fin`.
    - Khối ghi chú và tóm tắt số liệu `.dp-bottom`, hàng nút hành động `.dp-actions`.
  - Tùy chọn ẩn/hiển thị 11 cột lưu `localStorage` (`KiotFilter.initColumnToggle`).
  - Sắp xếp cột 3 trạng thái click-to-sort (ASC -> DESC -> Bỏ sắp xếp) trên 11 cột dữ liệu.
  - Phân trang AJAX (`_Pagination.cshtml`).

### 4.2. Màn hình 2: Lập/Sửa phiếu (`/ChuyenKho/Create`, `/ChuyenKho/Edit/{id}`)
- Layout 2 cột chuẩn chuyên nghiệp:
  - Cột trái: Nút quay lại, ô tìm hàng F3 autocomplete, nút nhập Excel (`#btnImportExcel`), empty state "Thêm sản phẩm từ file excel" với nút "Chọn file dữ liệu" (`#btnPickExcel`) & link tải file mẫu (`Mau_Chuyen_Kho.xlsx`, `DownloadTemplate`), import server-side ClosedXML qua API `ChuyenKho/ImportExcel`, bảng lưới hàng hóa có cảnh báo vượt tồn.
  - Cột phải: Box lộ trình `routebox` (Kho gửi -> Kho nhận), dropdown kho nhận `.wh-custom-select-btn`, thời gian `.dtpop` & mã chuyển hàng `.inp` căn phải đồng bộ Kiểm kho (nhập tay hoặc để trống tự sinh `CK000001`...), tổng SL, tổng giá trị, ghi chú.
  - Thu gọn/mở rộng panel phải mượt mà 0.25s chuẩn Kiểm kho (F4, `.f-panel-inner` 440px cố định chống giật méo nội dung, nút trượt đồng bộ mép).
  - Nút cố định đáy: "Lưu tạm" (`NHAP`) và "Chuyển hàng" (`DA_XUAT`).

### 4.3. Modal Nhận hàng (`/ChuyenKho/Receive`)
- Modal popup hiển thị danh sách mặt hàng, ô nhập `SL nhận` (chặn không cho vượt `SL chuyển`).
- Nếu nhận thiếu, bắt buộc nhập ghi chú lý do thất thoát.

---

## 5. Kế hoạch Triển khai
1. Migration SQL `13_bo_sung_chuyen_kho.sql` & cập nhật Model EF Core.
2. ViewModel và `ChuyenKhoController.cs` (Transaction, chặn âm kho, BaseKhoController).
3. Giao diện Danh sách `Index`, `_ChuyenKhoFilterSidebar`, `_ChuyenKhoTable`.
4. Giao diện Lập/Sửa `Create`, `Edit`, `_ChuyenKhoForm` (F3 autocomplete, custom dtPop, toggle panel, nạp Excel/CSV).
5. Modal `_ReceiveModal` nhận hàng & cập nhật `_MainMenu.cshtml`.
6. Biên dịch và đồng bộ `PROGRESS.md`.
