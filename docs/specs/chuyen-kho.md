# Đặc tả Chức năng: Quản lý Chuyển kho & Đổi kho làm việc

> **Mô tả**: Quản lý toàn diện luồng điều chuyển hàng hóa liên chi nhánh kho (Kho gửi -> Kho nhận), cơ chế tồn kho động thời gian thực qua `vw_TonKhoHienTai`, xử lý nhận hàng và hao hụt thất thoát trên đường chuyển; đồng thời cung cấp form Đổi kho làm việc nhanh cho người dùng trên nền tảng Windows Forms .NET Framework 4.7.2.

---

## 1. Cấu trúc Bảng CSDL & Nghiệp vụ

### 1.1. Bảng CSDL tham gia:
- **`PhieuChuyenKho`**:
  - `MaPhieuChuyen` (INT IDENTITY, PK): Mã định danh phiếu chuyển kho.
  - `MaPhieu` (VARCHAR(30)): Mã hiển thị (ví dụ: `CK000001`, `CK000017`).
  - `MaKhoNguon` (INT, FK `Kho`): Chi nhánh kho xuất chuyển đi.
  - `MaKhoDich` (INT, FK `Kho`): Chi nhánh kho nhận hàng đến.
  - `MaNVLap` (INT, FK `NhanVien`): Nhân viên lập phiếu.
  - `MaNVXuat` (INT, FK `NhanVien`, NULL): Nhân viên xác nhận xuất chuyển.
  - `MaNVNhan` (INT, FK `NhanVien`, NULL): Nhân viên xác nhận nhận hàng tại kho đích.
  - `NgayLap` (DATETIME2): Thời điểm lập phiếu.
  - `ThoiDiemXuat` (DATETIME2, NULL): Thời điểm xuất kho chuyển đi.
  - `ThoiDiemNhan` (DATETIME2, NULL): Thời điểm kho đích nhận hàng hoàn tất.
  - `TrangThai` (VARCHAR(20)): 4 trạng thái chuẩn:
    1. `'NHAP'`: Phiếu tạm (chưa trừ tồn kho gửi, có thể chỉnh sửa/xóa).
    2. `'DA_XUAT'`: Đang chuyển (đã trừ tồn kho gửi, chờ kho đích nhận, cho phép hủy).
    3. `'DA_NHAN'`: Đã nhận (đã cộng tồn kho đích, khóa vĩnh viễn không được xóa/hủy).
    4. `'HUY'`: Đã hủy (hoàn trả tồn kho gửi ngay lập tức).
  - `GhiChu` (NVARCHAR(500)): Ghi chú khi chuyển.
  - `GhiChuNhan` (NVARCHAR(500)): Ghi chú khi nhận hàng (bắt buộc khi có hao hụt).
  - `DaDanhDau` (BIT): Đánh dấu sao.
  - `TongSoLuongChuyen` (DECIMAL(18,2)): Tổng số lượng xuất chuyển.
  - `TongSoLuongNhan` (DECIMAL(18,2)): Tổng số lượng thực tế nhận được.
  - `TongGiaTriChuyen` (DECIMAL(18,2)): Tổng giá trị hàng hóa chuyển (theo giá vốn).

- **`ChiTietPhieuChuyenKho`**:
  - `MaCTPhieuChuyen` (INT IDENTITY, PK).
  - `MaPhieuChuyen` (INT, FK `PhieuChuyenKho`).
  - `MaHang` (INT, FK `HangHoa`).
  - `SoLuong` (DECIMAL(18,2)): Số lượng xuất chuyển.
  - `SoLuongNhan` (DECIMAL(18,2), NULL): Số lượng thực nhận tại kho đích.
  - `DonGiaChuyen` (DECIMAL(18,2)): Đơn giá chuyển (lấy từ giá vốn `GiaVon`).
  - `ThanhTien` (DECIMAL(18,2)): Thành tiền (`SoLuong * DonGiaChuyen`).

- **`vw_TonKhoHienTai` & `vw_BienDongKho`**:
  - Khi `TrangThai IN ('DA_XUAT', 'DA_NHAN')`: Trừ tồn kho tại `MaKhoNguon`.
  - Khi `TrangThai = 'DA_NHAN'`: Cộng tồn kho tại `MaKhoDich` với số lượng `ct.SoLuongNhan`.
  - Khi `TrangThai = 'HUY'`: Tự động loại trừ khỏi biến động, phục hồi tồn cho kho gửi.

- **`NhatKyThaoTac`**: Ghi vết kiểm toán mọi hành vi Lập phiếu, Chuyển hàng, Nhận hàng, Xóa phiếu tạm, Hủy phiếu.

---

## 2. Kiến trúc Phân hệ Form

| Tên Form | Loại | Đường dẫn | Vai trò & Nghiệp vụ |
|---|---|---|---|
| `frmDoiKho` | Modal Dialog | `GUI/Forms/DoiKho/frmDoiKho.cs` | Form chọn kho làm việc nhanh theo ảnh mẫu: ComboBox chọn chi nhánh kho hoạt động được phân quyền, 2 nút "Xác nhận", "Hủy"; cập nhật `SessionContext` và toàn bộ giao diện cha. |
| `frmChuyenKho` | MDI Child | `GUI/Forms/ChuyenKho/frmChuyenKho.cs` | Form danh sách phiếu chuyển kho, chuẩn kích thước `1782x853`, bộ lọc 100% ComboBox, cụm thời gian ở đáy, hiển thị màu 3 nhóm trạng thái bằng font chữ thường theo chuẩn Trả hàng nhập, phân trang. |
| `frmThemPhieuChuyenKho` | Modal Dialog | `GUI/Forms/ChuyenKho/frmThemPhieuChuyenKho.cs` | Form lập phiếu chuyển mới: chọn kho gửi, kho nhận, chọn hàng hóa từ kho gửi, chặn tồn âm, thứ tự nút đáy chuẩn `Bỏ qua` ➜ `Lưu tạm` ➜ `Lưu`. |
| `frmChiTietChuyenKho` | Modal Dialog | `GUI/Forms/ChuyenKho/frmChiTietChuyenKho.cs` | Form chi tiết phiếu chuyển kho khi double-click: Master-Detail, nhãn trạng thái dòng chữ màu font chữ thường theo chuẩn Trả hàng nhập, nút Hoàn thành (xuất chuyển cho phiếu tạm `NHAP`), Nhận hàng (cho kho đích khi `DA_XUAT`), Xóa (theo Luật 5 & 15d), Xuất file. |
| `frmNhanChuyenKho` | Modal Dialog | `GUI/Forms/ChuyenKho/frmNhanChuyenKho.cs` | Form xác nhận nhận hàng tại kho đích: kiểm đếm số lượng thực nhận, phát hiện hao hụt, bắt buộc nhập lý do thất thoát, cập nhật `DA_NHAN`. |
| `frmHienCotChuyenKho` | Tool Window | `GUI/Forms/ChuyenKho/frmHienCotChuyenKho.cs` | Popup tùy chọn ẩn/hiện cột bảng chuyển kho. |
| `frmNhapChuyenKhoFile` | Modal Dialog | `GUI/Forms/ChuyenKho/frmNhapChuyenKhoFile.cs` | Form nạp danh sách hàng hóa chuyển từ file CSV (hỗ trợ tải file mẫu chuẩn). |

---

## 3. Quy tắc Nghiệp vụ cốt lõi

1. **Chặn tồn âm kho gửi (Luật 6)**:
   - Trước khi xác nhận chuyển hàng (`DA_XUAT`), hệ thống kiểm tra tồn kho sổ sách tức thời qua `vw_TonKhoHienTai`. Nếu `SoLuongChuyen > TonKho`, chặn đứng giao dịch và hiển thị thông báo cảnh báo chi tiết.
2. **Quyền nhận hàng liên chi nhánh**:
   - Nút "Nhận hàng" trong `frmChiTietChuyenKho` chỉ hiển thị và cho phép thao tác khi phiếu ở trạng thái `'DA_XUAT'` và người dùng hiện tại đang làm việc tại kho đích (`SessionContext.MaKhoHienTai == MaKhoDich` hoặc tài khoản có vai trò Quản lý Admin).
3. **Xử lý hao hụt / nhận thiếu**:
   - Thủ kho nhận có quyền ghi nhận `SoLuongNhan <= SoLuongChuyen`. Nếu `SoLuongNhan < SoLuongChuyen`, phần chênh lệch được đánh dấu hao hụt (tô đỏ) và bắt buộc phải nhập lý do vào ô `GhiChuNhan` trước khi lưu.
4. **Quy tắc Xóa & Hủy (Luật 5 & Điều 15d)**:
   - Phiếu tạm (`NHAP`): Được phép xóa hoàn toàn khỏi CSDL.
   - Phiếu đang chuyển (`DA_XUAT`): Được phép Hủy (`HUY`), tồn kho gửi được tự động phục hồi ngay lập tức.
   - Phiếu đã nhận (`DA_NHAN`): Khóa vĩnh viễn, nút Xóa bị vô hiệu hóa (`btnXoa.Enabled = false`, giữ nguyên text "Xóa").
5. **Thứ tự nút đáy chuẩn (Điều luật 19b)**:
   - Thanh nút thao tác ở đáy form `frmThemPhieuChuyenKho` tuân thủ đúng chuẩn toàn hệ thống: `Bỏ qua` ➜ `Lưu tạm` ➜ `Lưu`.
6. **Màu sắc trạng thái 3 nhóm (Điều luật 15b & 19a)**:
   - Nhóm Hoàn thành ("Đã nhận"): Màu xanh lá `Color.FromArgb(22, 163, 74)`.
   - Nhóm Tạm thời ("Đang chuyển", "Phiếu tạm"): Màu vàng amber `Color.FromArgb(217, 119, 6)`.
   - Nhóm Đã hủy ("Đã hủy"): Màu xám nhạt `Color.FromArgb(100, 116, 139)`.
   - Nhãn trạng thái hiển thị bằng dòng chữ tự nhiên có màu, không viền khung badge.
7. **Nút Hoàn thành cho Phiếu tạm (`NHAP`)**:
   - Khi phiếu chuyển ở trạng thái `NHAP` (Phiếu tạm), form chi tiết `frmChiTietChuyenKho` hiển thị nút `Hoàn thành` (`btnHoanThanh`).
   - Khi bấm Hoàn thành, hệ thống kiểm tra tồn kho tại kho gửi (chặn âm tồn kho theo Luật 6), hỏi xác nhận, bọc trong Transaction cập nhật `TrangThai = 'DA_XUAT'`, `ThoiDiemXuat = GETDATE()`, `MaNVXuat`, hàng hóa xuất kho gửi và chuyển sang trạng thái "Đang chuyển" để kho đích thực hiện nhận hàng.
