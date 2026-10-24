# Đặc tả Chức năng: Quản lý Kiểm kho & Cân bằng kho (Stock Take)

> **Mục tiêu**: Chuẩn hóa toàn diện phân hệ Kiểm kho trên nền tảng Windows Forms theo Design System mẫu `frmDanhSachHangHoa`, tài liệu `docs/quyet-dinh.md` (Phiên 5 & 18), tuân thủ 17 điều luật cốt lõi trong `AGENTS.md`.

---

## 1. Cấu trúc Bảng CSDL & Nghiệp vụ

### 1.1. Bảng CSDL tham gia:
- **`PhieuKiemKe`**:
  - `MaPhieuKiemKe` (INT IDENTITY, PK): Mã định danh phiếu kiểm kho.
  - `MaKho` (INT, FK `Kho`): Chi nhánh kho thực hiện kiểm kê.
  - `MaNVLap` (INT, FK `NhanVien`): Nhân viên thực hiện lập phiếu.
  - `ThoiDiemChot` (DATETIME2): Thời điểm chốt số liệu kiểm kho.
  - `TrangThai` (VARCHAR(20)): `NHAP` (phiếu tạm), `DA_DIEU_CHINH` (đã cân bằng kho), `HUY` (đã hủy).
  - `GhiChu` (NVARCHAR(500)): Ghi chú lý do / mô tả kiểm kê.
  - `MaPhieu` (VARCHAR(30)): Mã hiển thị (ví dụ: `PKK000001`, `KK000003`).
  - `DaDanhDau` (BIT): Đánh dấu sao bookmark.
- **`ChiTietPhieuKiemKe`**:
  - `MaCTKiemKe` (INT IDENTITY, PK).
  - `MaPhieuKiemKe` (INT, FK `PhieuKiemKe`).
  - `MaHang` (INT, FK `HangHoa`).
  - `SoLuongTheoSo` (DECIMAL(18,2)): Tồn sổ sách đọc từ `vw_TonKhoHienTai` tại thời điểm kiểm.
  - `SoLuongThucTe` (DECIMAL(18,2) NULL): Số lượng thực tế kiểm đếm.
  - `DonGia` (DECIMAL(18,2)): Đơn giá chốt (giá vốn hàng hóa) phục vụ tính giá trị chênh lệch.
  - Ràng buộc duy nhất: `UQ_CTPKK_Phieu_Hang` trên `(MaPhieuKiemKe, MaHang)`.
- **`PhieuDieuChinh` & `ChiTietPhieuDieuChinh`**:
  - Tự động sinh khi người dùng nhấn **"Cân bằng kho"** (`DA_DUYET`).
  - `ChiTietPhieuDieuChinh` lưu `SoLuongDieuChinh = SoLuongThucTe - SoLuongTheoSo` cho các mặt hàng có chênh lệch (`SoLuongDieuChinh <> 0`).
  - Tồn kho tự động cập nhật khớp đúng số lượng thực tế qua View động `vw_BienDongKho` & `vw_TonKhoHienTai`.
- **`NhatKyThaoTac`**: Tự động ghi log kiểm toán mọi thao tác Lưu tạm, Cân bằng kho, Xóa phiếu tạm.

---

## 2. Kiến trúc Phân hệ Form (`GUI/Forms/KiemKho/`)

| Tên Form | Loại | Vai trò & Nghiệp vụ |
|---|---|---|
| [`frmKiemKho`](../../BTL-LTTQ-2/GUI/Forms/KiemKho/frmKiemKho.cs) | MDI Child | Màn hình danh sách phiếu kiểm kho, bộ lọc bên trái 100% ComboBox, tìm kiếm, phân trang và xuất file. |
| [`frmThemPhieuKiemKho`](../../BTL-LTTQ-2/GUI/Forms/KiemKho/frmThemPhieuKiemKho.cs) | Modal Dialog | Màn hình lập phiếu kiểm kho mới (nút `+ Kiểm kho`), hỗ trợ chọn hàng từ danh mục, sửa trực tiếp trên lưới, nhập file dữ liệu và thực hiện Lưu tạm / Cân bằng kho. |
| [`frmChiTietKiemKho`](../../BTL-LTTQ-2/GUI/Forms/KiemKho/frmChiTietKiemKho.cs) | Modal Dialog | Màn hình chi tiết khi nhấn đúp vào 1 phiếu kiểm (theo ảnh mẫu 3), chỉ giữ lại 2 nút: **Xóa** (khóa vĩnh viễn phiếu đã cân bằng theo Luật 6) và **Xuất file**. |
| [`frmNhapKiemKhoFile`](../../BTL-LTTQ-2/GUI/Forms/KiemKho/frmNhapKiemKhoFile.cs) | Modal Dialog | Màn hình nhập dữ liệu kiểm kho từ file (Excel/CSV), cung cấp tải file mẫu Excel/CSV chuẩn theo khuôn mẫu `frmNhapHangHoaFile`. |
| [`frmHienCotKiemKho`](../../BTL-LTTQ-2/GUI/Forms/KiemKho/frmHienCotKiemKho.cs) | Tool Window | Popup tùy chọn ẩn/hiện các cột của bảng kiểm kho. |

---

## 3. Luồng Giao diện & Tương tác

### 3.1. Màn hình Danh sách `frmKiemKho`
- **Khối trên (Top Toolbar)**:
  - Tiêu đề: "Phiếu kiểm kho" (`Segoe UI, 12pt Bold`).
  - Ô tìm kiếm `txtTimKiem`: Lọc theo mã phiếu kiểm và ghi chú.
  - Nút `+ Kiểm kho` (`btnKiemKho`): Mở form `frmThemPhieuKiemKho`.
  - Nút `Xuất file` (`btnXuatFile`): Xuất danh sách đang lọc ra Excel/CSV.
  - Nút `Hiện cột` (`btnHienCot`): Mở popup tùy chọn cột `frmHienCotKiemKho`.
  - Nút `?` (`btnHuongDanSuDung`): Hiển thị hướng dẫn sử dụng.
- **Khối bộ lọc bên trái (Left Filter GroupBox)**:
  - Cụm `grpThoiGianTao` (322x100): 2 `DateTimePicker` `dtpTuNgay`, `dtpDenNgay` định dạng Short.
  - `cboTrangThai`: ComboBox DropDownList (-- Tất cả trạng thái --, Đã cân bằng, Phiếu tạm, Đã hủy).
  - `cboNguoiTao`: ComboBox tìm kiếm tiếng Việt không dấu qua `ComboboxHelper.CaiDatTimKiem`.
  - `cboKho`: ComboBox chọn chi nhánh kho.
- **Lưới danh sách `dgvKiemKho`**:
  - Gồm 11 cột hiển thị chuẩn: Checkbox (colChon), Mã kiểm kho (colMaPhieu), Thời gian (colThoiGian), Tổng thực tế (colTongThucTe), Tổng chênh lệch (colTongChenhLech), SL lệch tăng (colSlLechTang), SL lệch giảm (colSlLechGiam), Ngày cân bằng (colNgayCanBang), SL thực tế (colSlThucTe), Ghi chú (colGhiChu - Fill), Trạng thái (colTrangThai).
  - Đệm đôi chống nhấp nháy (`DoubleBuffered = true`), màu xen kẽ, căn phải số lượng và đơn giá định dạng `N0`, các cột có `MinimumWidth` chuẩn chống co bẹp cột.
  - **Sự kiện nhấn đúp (`CellDoubleClick`)**: Kích hoạt mở form `frmChiTietKiemKho`.
- **Khối phân trang đáy**: Hiển thị tổng số phiếu, tùy chọn số dòng mỗi trang (10, 15, 20, 50, 100), nút trang trước/sau.

### 3.2. Màn hình Chi tiết `frmChiTietKiemKho` (Ảnh 3)
- **Vùng Master**:
  - Mã kiểm kho to rõ kèm dòng chữ trạng thái có màu (không khung/badge viền nền theo Điều luật 19a): xanh lá nếu Đã cân bằng, vàng amber nếu Phiếu tạm, xám nếu Đã hủy.
  - Thông tin người tạo, ngày tạo, người cân bằng, ngày cân bằng, chi nhánh kho.
- **Vùng Detail**:
  - Lưới hiển thị chi tiết dòng hàng: STT, Mã hàng, Tên hàng, ĐVT, Tồn kho (sổ sách), Thực tế, SL lệch, Đơn giá, Giá trị lệch.
- **Vùng Tổng kết**:
  - Bên trái: Ghi chú phiếu.
  - Bên phải: Tổng thực tế, Tổng lệch tăng, Tổng lệch giảm, Tổng chênh lệch (hiển thị cả số lượng và giá trị tiền).
- **Thanh nút đáy (Chỉ giữ lại 2 nút nghiệp vụ + nút Đóng)**:
  - Nút **Xóa** (`btnXoa`):
    + Nếu phiếu ở trạng thái `'DA_DIEU_CHINH'`: Khóa vĩnh viễn theo Luật 6, không cho xóa.
    + Nếu phiếu ở trạng thái `'NHAP'`: Xác nhận Yes/No, xóa phiếu và chi tiết, ghi log nhật ký.
  - Nút **Xuất file** (`btnXuatFile`): Xuất chi tiết phiếu kiểm ra file Excel hoặc CSV (UTF-8 BOM).
  - Nút **Đóng** (`btnDong`).

### 3.3. Màn hình Thêm mới `frmThemPhieuKiemKho`
- **Thanh tiêu đề trên (`pnlTop`)**: Chỉ chứa tiêu đề Form "Thêm phiếu kiểm kho", không để nút bấm lên đầu theo Điều luật 19b.
- **Thanh chọn hàng nhanh (`pnlThemHang`)**:
  - ComboBox `cboHangHoa` chọn từ danh mục sản phẩm của kho.
  - NumericUpDown `nudSoLuongThucTe` nhập số lượng thực tế.
  - Nút `+ Thêm hàng` và nút `Nạp tất cả hàng trong kho`.
  - Nút **Nhập file** (`btnNhapFile`): Mở popup `frmNhapKiemKhoFile`.
- **Lưới kiểm kê `dgvHangKiem`**:
  - Cho phép người dùng chỉnh sửa trực tiếp số lượng thực tế trên ô lưới (`CellEndEdit`) để nhập liệu nhanh.
  - Tự động tính chênh lệch số lượng và thành tiền lệch realtime.
  - Nút Xóa dòng hàng.
- **Vùng Tổng kết (`pnlTongKet`)**:
  - Hiển thị tóm tắt: Số dòng kiểm, Tổng thực tế, Tổng lệch tăng, Tổng lệch giảm, Tổng chênh lệch (tô đỏ khi chênh lệch khác 0).
- **Thanh nút đáy (`pnlActions`)**: Thống nhất ở cuối form theo thứ tự chuẩn Điều luật 19b:
  - Nút **Bỏ qua** (`btnBoQua`): Hủy/đóng form khi chưa lưu dữ liệu.
  - Nút **Lưu tạm** (`btnLuuTam`): Lưu phiếu trạng thái `'NHAP'`, không thay đổi tồn kho sổ sách.
  - Nút **Lưu** (`btnCanBangKho`): Hỏi xác nhận, đọc lại tồn sổ sách, sinh `PhieuDieuChinh` `DA_DUYET`, cập nhật tồn kho qua `vw_TonKhoHienTai`, chuyển phiếu sang `'DA_DIEU_CHINH'` và khóa phiếu vĩnh viễn.
  - Nút Xóa dòng hàng.

### 3.4. Màn hình Nhập file `frmNhapKiemKhoFile`
- Cung cấp 2 nút tải file mẫu:
  - **Tải file mẫu Excel (.xls)**: Định dạng bảng HTML-Excel đẹp mắt, có header xanh `#0078D7`, kèm dữ liệu mẫu thực tế từ database.
  - **Tải file mẫu CSV (.csv)**: Định dạng CSV chuẩn UTF-8 BOM.
- Đọc file dữ liệu (.xlsx, .xls, .csv), kiểm tra đối chiếu mã hàng tồn tại trong hệ thống và số lượng thực tế hợp lệ.
- Lưới xem trước hiển thị trạng thái từng dòng (Hợp lệ / Lỗi). Nút "Đồng ý nhập" nạp toàn bộ dòng hợp lệ vào `frmThemPhieuKiemKho`.

---

## 4. Kiểm toán & An toàn Dữ liệu
- 100% câu truy vấn sử dụng `SqlParameter`, ngăn chặn tuyệt đối SQL Injection.
- Toàn bộ thao tác ghi chứng từ nhiều bảng (Phiếu kiểm, Chi tiết kiểm, Phiếu điều chỉnh, Chi tiết điều chỉnh) được bọc trong giao dịch `DataProcessing.ChangeDataTransaction`.
- Tự động ghi nhật ký vào bảng `NhatKyThaoTac`.
