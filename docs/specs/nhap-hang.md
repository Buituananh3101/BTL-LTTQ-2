# Đặc tả Chức năng: Nhập hàng (Purchase Orders - Windows Forms)

> **Mục tiêu**: Quản lý chứng từ Nhập hàng từ Nhà cung cấp chuẩn giao diện & nghiệp vụ hệ thống kho hàng nội bộ đa chi nhánh. Đồng bộ 100% với khuôn mẫu `frmDanhSachHangHoa` và `frmNhaCungCap` (bộ lọc bên trái 100% ComboBox, tìm kiếm tức thời, phân trang SQL Server, DataGridView đệm đôi chống giật lag, xuất file CSV UTF-8 BOM, nút bấm chuẩn WinForms không màu mè).

---

## 1. Kiến trúc File & CSDL

### 1.1. Bản đồ File liên quan (File Map)
- **Thư mục chức năng**: `BTL-LTTQ-2/GUI/Forms/NhapHang/`
  - `frmNhapHang.cs`, `.Designer.cs`, `.resx`: Form chính hiển thị danh sách đơn nhập hàng, bộ lọc ComboBox bên trái, tìm kiếm tức thời F3, nút `+ Nhập hàng`, `Xuất file`, `Hiện cột`. Hỗ trợ nhấn đúp chuột vào đơn nhập để mở form xem chi tiết đơn.
  - `frmThemPhieuNhap.cs`, `.Designer.cs`, `.resx`: Form lập phiếu nhập hàng mới (nút `+ Nhập hàng`). Cho phép chọn NCC, kho, ngày nhập, tìm kiếm chọn hàng hóa, nhập số lượng, đơn giá, chiết khấu, tính toán tự động tổng tiền hàng, tiền đã trả, tiền nợ cần trả NCC; bọc Transaction khi lưu phiếu tạm (`NHAP`) hoặc hoàn thành (`DA_XAC_NHAN`).
  - `frmChiTietPhieuNhap.cs`, `.Designer.cs`, `.resx`: Form xem chi tiết đơn nhập hàng (mở khi double-click từ danh sách). Hiển thị đầy đủ thông tin header (Mã phiếu, NCC, Kho, Ngày nhập, Trạng thái, Người nhập, Ghi chú), bảng lưới hàng hóa đã nhập, tổng kết thanh toán. **Chỉ giữ lại 3 nút chức năng duy nhất: `Xóa` (thay thế nút Hủy), `Xuất file`, `Trả hàng nhập`**.
  - `frmHienCotNhapHang.cs`, `.Designer.cs`, `.resx`: Form popup ẩn/hiện cột cho DataGridView phiếu nhập hàng.
  - `frmTraHangNhap.cs`, `.Designer.cs`, `.resx`: Form trả hàng nhập được kích hoạt từ nút `Trả hàng nhập` trên form chi tiết đơn.
- **Form cha MDI & Menu**: `BTL-LTTQ-2/GUI/Forms/frmMain.cs` (gắn sự kiện `mnuNhapHang_Click` kiểm tra kích hoạt hoặc tạo mới form `frmNhapHang`).
- **Truy xuất dữ liệu (DAL)**: `BTL-LTTQ-2/DAL/DataProcessing.cs` (ADO.NET thuần: `ReadData`, `ReadValue`, `ChangeDataTransaction`).
- **Quản lý phiên đăng nhập**: `BTL-LTTQ-2/BLL/SessionContext.cs` (lấy `MaTaiKhoan`, `MaKhoHienTai`, vai trò).

### 1.2. Cơ sở dữ liệu tham gia
- **`PhieuNhap`**: `MaPhieuNhap`, `MaPhieu`, `MaKho`, `MaDonViCapHang`, `MaNvlap`, `NgayLap`, `NgayXacNhan`, `TrangThai` (`NHAP`, `DA_XAC_NHAN`, `HUY`), `TongTien`, `GiamGia`, `DaTraNcc`, `CanTraNcc`, `GhiChu`, `DaDanhDau`.
- **`ChiTietPhieuNhap`**: `MaCtphieuNhap`, `MaPhieuNhap`, `MaHang`, `SoLuong`, `DonGiaNhap`, `GiamGia`, `ThanhTien`.
- **`DonViCapHang`**: Nhà cung cấp cung ứng hàng.
- **`vw_BienDongKho` & `vw_TonKhoHienTai`**: Tự động tăng số lượng tồn khi phiếu chuyển sang `DA_XAC_NHAN`. Tự động hoàn lại khi chuyển trạng thái `HUY`.
- **`NhatKyThaoTac`**: Ghi log thao tác tạo, hoàn thành, xóa hoặc hủy phiếu nhập.

---

## 2. Luồng Tương tác Nghiệp vụ Chi tiết

| Luồng | Tương tác người dùng | Giao diện WinForms (GUI) | Xử lý & CSDL (DAL / SQL) |
|---|---|---|---|
| **1. Mở danh sách** | Vào Menu "Mua hàng" ➜ "Nhập hàng" | `mnuNhapHang_Click` kiểm tra form con trong `MdiChildren`. Nếu đã mở ➜ `Activate()`, nếu chưa ➜ `Show()` | Nạp danh mục NCC, người tạo, người nhập vào các ComboBox; nạp danh sách trang 1 |
| **2. Lọc đa tiêu chí** | Chọn Trạng thái, Nhà cung cấp, Người tạo, Người nhập hoặc Khoảng thời gian tạo | Đặt lại `trangHienTai = 1`, gọi `HienThiDanhSach()` nạp lại dữ liệu | Mệnh đề `WHERE` lọc động theo các tham số `@TrangThai`, `@MaNCC`, `@MaNguoiTao`, `@MaNguoiNhap`, `@TuNgay`, `@DenNgay` |
| **3. Tìm kiếm tức thời** | Gõ mã phiếu hoặc số hóa đơn vào ô Tìm kiếm | Sự kiện `txtTimKiem_TextChanged` kích hoạt tìm tức thời | Lọc `pn.MaPhieu LIKE @Search OR pn.GhiChu LIKE @Search` |
| **4. Phân trang** | Bấm "◀ Trước", "Sau ▶" hoặc đổi "Hiển thị: 15 dòng" | `cboSoDongMoiTrang_SelectedIndexChanged`, `btnTrangTruoc_Click`, `btnTrangSau_Click` | SQL Server phân trang chuẩn `OFFSET @Skip ROWS FETCH NEXT @Take ROWS ONLY` |
| **5. Thêm mới đơn nhập** | Bấm nút `+ Nhập hàng` (`btnThemPhieuNhap`) | Mở form `frmThemPhieuNhap` dưới dạng Modal Dialog | Gợi ý tự động mã phiếu `PN00000X`, cho chọn NCC, kho, thêm hàng hóa, tính tổng tiền, nợ. Lưu tạm (`NHAP`) hoặc Hoàn thành (`DA_XAC_NHAN`) trong Transaction |
| **6. Xem chi tiết đơn nhập** | Nhấn đúp chuột (Double Click) vào 1 dòng đơn nhập | Mở form `frmChiTietPhieuNhap` | Nạp thông tin header, danh sách hàng hóa và tổng kết thanh toán. Cung cấp đúng 3 nút: `Xóa`, `Xuất file`, `Trả hàng nhập` |
| **7. Xóa đơn nhập** | Bấm nút `Xóa` trên form chi tiết | Xác nhận DialogResult: Nếu phiếu `NHAP` ➜ Xóa cứng; Nếu `DA_XAC_NHAN` ➜ Kiểm tra an toàn tồn kho, chuyển trạng thái `HUY` | Bọc trong Transaction; ghi nhật ký thao tác; nếu hủy làm âm tồn kho thì chặn ngay |
| **8. Trả hàng nhập** | Bấm nút `Trả hàng nhập` trên form chi tiết | Mở modal dialog `frmTraHangNhap(maPhieuNhap)` | Chỉ mở được với phiếu `DA_XAC_NHAN`. Chuyển sang giao diện trả hàng nhập |
| **9. Tùy chọn Ẩn/Hiện cột** | Bấm nút "Hiện cột" | Mở popup `frmHienCotNhapHang` | Bật/tắt `dgv.Columns[col].Visible` tức thì |
| **10. Xuất file CSV** | Bấm nút "Xuất file" | Hộp thoại `SaveFileDialog` lưu file `.csv` | Xuất đúng các cột đang hiển thị với định dạng UTF-8 BOM đọc tốt trên Microsoft Excel |

---

## 3. Quy chuẩn Giao diện & Bộ lọc (Tuân thủ Luật 14 & 15)

### 3.1. Cột lọc bên trái (Sidebar Filter - 100% ComboBox):
1. **Trạng thái**: ComboBox `cboTrangThai`: DropDownList ("-- Tất cả trạng thái --", "Phiếu tạm (Lưu tạm)", "Đã nhập hàng", "Đã hủy").
2. **Nhà cung cấp**: ComboBox `cboNhaCungCap`: DropDownList ("-- Tất cả nhà cung cấp --", danh sách NCC từ CSDL).
3. **Người tạo**: ComboBox `cboNguoiTao`: DropDownList ("-- Tất cả người tạo --", danh sách nhân viên).
4. **Người nhập**: ComboBox `cboNguoiNhap`: DropDownList ("-- Tất cả người nhập --", danh sách nhân viên).
5. **Khoảng thời gian tạo** (GroupBox `grpThoiGianTao`):
   - `dtpTuNgay`: Chọn từ ngày (mặc định đầu năm).
   - `dtpDenNgay`: Chọn đến ngày (mặc định hôm nay).
   - Tự động lọc lại khi thay đổi ngày.

### 3.2. Nút bấm & Nhãn hiển thị:
- Nút `+ Nhập hàng`: Định dạng WinForms chuẩn (`UseVisualStyleBackColor = true`), không tự ý tô màu tùy tiện.
- 100% hiển thị tiếng Việt có dấu rõ ràng: `Xóa`, `Xuất file`, `Trả hàng nhập`, `Lưu tạm`, `Hoàn thành`, `Đóng`.

### 3.3. Chi tiết đơn nhập (`frmChiTietPhieuNhap`):
- Tham khảo đầy đủ các vùng thông tin từ ảnh mẫu của người dùng:
  - Header: Mã phiếu, Nhà cung cấp, Kho nhập, Trạng thái chứng từ, Ngày nhập, Người nhập, Ghi chú.
  - Bảng DataGridView hàng hóa: STT, Mã hàng, Tên hàng, ĐVT, Số lượng, Đơn giá nhập, Giảm giá, Thành tiền.
  - Khối tổng kết thanh toán: Tổng số lượng, Tổng tiền hàng, Giảm giá, Tổng cộng cần trả, Đã trả NCC, Còn nợ NCC.
- **Ràng buộc quan trọng**: Giữ lại duy nhất 3 nút bấm chức năng ở góc dưới:
  1. `Xóa` (`btnXoaPhieu`): Thực hiện xóa phiếu tạm hoặc chuyển trạng thái sang `HUY` đối với phiếu đã xác nhận (kèm kiểm tra chặn âm tồn kho).
  2. `Xuất file` (`btnXuatFile`): Xuất thông tin phiếu và bảng hàng hóa ra file CSV/Excel UTF-8 BOM.
  3. `Trả hàng nhập` (`btnTraHangNhap`): Kích hoạt `frmTraHangNhap` để hoàn trả một phần hoặc toàn bộ lô hàng cho NCC.
