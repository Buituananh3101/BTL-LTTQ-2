# Mẫu Đặc tả Chức năng mới: [Tên chức năng]

> **Ghi chú**: Sử dụng mẫu này khi bắt đầu thiết kế bất kỳ chức năng chứng từ hoặc danh mục mới nào trong hệ thống. Điền đầy đủ thông tin trước khi bắt tay vào viết code.

---

## 1. Mục tiêu & Phạm vi nghiệp vụ
- **Mục tiêu**: Mô tả chức năng này làm gì, người dùng là ai (Thủ kho / Bán hàng / Admin).
- **Mã tiền tố chứng từ**: (Ví dụ: `PN` cho Nhập hàng, `PX` cho Xuất hàng, `CK` cho Chuyển kho).
- **Trạng thái chứng từ**:
  - `'NHAP'`: Phiếu tạm.
  - `'DA_XAC_NHAN'` / `'DA_NHAN'`: Hoàn thành (ảnh hưởng tồn kho).
  - `'HUY'`: Đã hủy.

---

## 2. Các bảng CSDL & ViewModels tham gia
- **Bảng CSDL**:
  - Bảng chính: `[TênBangChinh]`
  - Bảng chi tiết: `[TênBangChiTiet]`
  - Bảng liên quan: `Kho`, `HangHoa`, `NhanVien`, `NhatKyThaoTac`
- **ViewModels cần tạo (đặt trong `ViewModels/`)**:
  - `[TenChucNang]IndexVM`: Hiển thị danh sách và bộ lọc.
  - `[TenChucNang]FilterVM`: Chứa các tiêu chí tìm kiếm.
  - `[TenChucNang]FormVM`: Dùng cho màn hình Thêm/Sửa.
  - `[TenChucNang]LineVM`: Đại diện một dòng hàng hóa chi tiết.
  - `Save[TenChucNang]Request`: Model nhận dữ liệu JSON khi submit form.

---

## 3. Luồng màn hình & Giao diện (Chuẩn giao diện hệ thống)
1. **Màn hình Danh sách (`Index`)**:
   - Bộ lọc sidebar trái: Ngày tạo, Trạng thái, Kho làm việc, Người tạo.
   - Bảng danh sách: Phân trang AJAX qua partial view `_[TenChucNang]Table.cshtml`.
2. **Màn hình Tạo mới / Chỉnh sửa (`Create` / `Edit`)**:
   - Tìm kiếm hàng hóa (phím tắt F3, autocomplete).
   - Bảng nhiều dòng chi tiết: Thêm dòng, sửa số lượng, đơn giá, thành tiền, xóa dòng.
   - Panel thông tin: Kho, Người lập, Ngày lập, Ghi chú, Tổng tiền / Tổng số lượng.
   - Nút hành động: "Lưu tạm", "Hoàn thành", "Bỏ qua".
3. **Màn hình Chi tiết (`Details`)**:
   - Chế độ chỉ đọc, có nút In phiếu và nút Hủy phiếu (nếu được phép).

### 3.1. Danh mục Khối Lego BẮT BUỘC tái sử dụng (Luật 10 AGENTS.md & 03-giao-dien-layout.md):
- [ ] **Bộ lọc Ngày tháng**: `<partial name="_KiotDateFilter" ... />` + `KiotFilter.initDateFilter` + `DateRangeHelper.Resolve`.
- [ ] **Bộ lọc Dropdown chọn nhiều**: `<partial name="_KiotMultiSelectFilter" ... />` + `KiotFilter.renderTagPills`.
- [ ] **Bộ lọc Trạng thái checkbox**: `<partial name="_KiotStatusFilter" ... />`.
- [ ] **Phân trang AJAX**: `<partial name="_Pagination" model="Model.PagedData" />` + `BaseFilterParams`.
- [ ] **Combobox vừa gõ vừa chọn có icon bút chì ✎**: `KiotFilter.initCombobox`.
- [ ] **Modal con tạo nhanh / sửa ✎ / xóa tại chỗ**: `KiotPrompt.open({ ... })`.
- [ ] **Ô nhập số tự format hàng nghìn**: class `.kiot-num` + `KiotNumber.get()`.
- [ ] **Khối gập mở Accordion**: class `.kiot-sec`, `.kiot-sec-h`, `.kiot-chev`.
- [ ] **Nút lưu đa năng Split Button**: class `.kiot-split`, `.caret`, `.kiot-menu`.
- [ ] **Thông báo Toast**: `KiotToast.success()`, `KiotToast.error()`, `KiotToast.info()`.

---

## 4. Quy tắc nghiệp vụ & Xử lý Tồn kho
- **Ảnh hưởng tồn kho**:
  - Khi nào thay đổi tồn kho? (Chỉ khi chuyển sang trạng thái Hoàn thành).
  - Có kiểm tra tồn kho âm trước khi thực hiện không? (Bắt buộc kiểm tra khi xuất kho).
- **Quy tắc sửa / hủy**:
  - Phiếu tạm được sửa/xóa.
  - Phiếu đã hoàn thành không được sửa/xóa; chỉ được Hủy (chuyển trạng thái chứng từ sang 'HUY' và kiểm tra chặn tồn âm nếu hủy phiếu nhập). Do tồn kho được tính toán động từ View `vw_TonKhoHienTai`, chứng từ ở trạng thái 'HUY' sẽ tự động không còn tính vào tồn kho mà không cần sinh bút toán đảo.
- **Giao dịch**: Bọc toàn bộ quá trình lưu trong `BeginTransactionAsync()`.
- **Nhật ký**: Ghi 1 dòng vào `NhatKyThaoTac`.

---

## 5. Checklist kiểm tra trước khi hoàn thành
- [ ] Code biên dịch không lỗi.
- [ ] Tên lớp, controller, view, thuộc tính dùng tiếng Việt không dấu.
- [ ] Giao diện tiếng Việt có dấu, đồng bộ Bootstrap 5.
- [ ] Có ValidateAntiForgeryToken / kiểm tra dữ liệu đầu vào.
- [ ] Có phân quyền / kiểm tra kho đang làm việc (`CurrentWarehouseId`).
- [ ] Không có lỗi runtime khi submit dữ liệu rỗng.
