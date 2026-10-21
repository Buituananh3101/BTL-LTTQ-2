# Đặc tả Chức năng: Trả hàng nhập (Purchase Returns)

> **Mục tiêu**: Quản lý trả hàng cho NCC chuẩn chuyên nghiệp & prototype `prototype-trahangnhap.html`, đồng bộ khuôn mẫu Nhập hàng (`NhapHang`). Tái sử dụng khối LEGO: `KiotFilter`, `KiotToast`, `f3-product-search`, `_KiotDateFilter`, `_KiotPromptModal`...

---

## 1. Kiến trúc CSDL, ViewModel & Bản đồ File

### 1.1. Bảng CSDL:
- **`PhieuTraHangNhap`**:
  - `MaPhieuTraHang` (PK, INT IDENTITY), `MaPhieu` (VARCHAR(30), UNIQUE, tiền tố `THN000001`).
  - `MaKho` (FK Kho, NOT NULL), `MaDonViCapHang` (FK DonViCapHang, NULL - NCC vãng lai).
  - `MaNvlap` (FK NhanVien, NOT NULL), `MaNvtra` (FK NhanVien, NULL).
  - `NgayLap`, `NgayTra` (DATETIME, NOT NULL, DEFAULT GETDATE()).
  - `TrangThai` (VARCHAR(20), CHECK IN (`'NHAP'`, `'DA_TRA_HANG'`, `'HUY'`)).
  - `TongSoLuong`, `TongTienHang`, `GiamGia` (DECIMAL(18,2)).
  - `CanThuNcc` (DECIMAL(18,2) = TongTienHang - GiamGia), `DaThuNcc` (DECIMAL(18,2)).
  - `GhiChu` (NVARCHAR(500), NULL), `DaDanhDau` (BIT, DEFAULT 0).
- **`ChiTietPhieuTraHangNhap`**:
  - `MaCtphieuTra` (PK, INT IDENTITY), `MaPhieuTraHang` (FK, INT, ON DELETE CASCADE).
  - `MaHang` (FK HangHoa, NOT NULL), `SoLuong` (DECIMAL(18,2), NOT NULL).
  - `GiaVon` (DECIMAL(18,2), giá vốn tại thời điểm trả).
  - `DonGiaTra` (DECIMAL(18,2), giá trả lại NCC).
  - `ThanhTien` (DECIMAL(18,2) = SoLuong * DonGiaTra).
  - `GhiChu` (NVARCHAR(255), NULL).
- **Cập nhật `vw_BienDongKho` & `vw_TonKhoHienTai`**:
  - Khi `TrangThai = 'DA_TRA_HANG'`: Thêm nhánh `TRA_HANG_NHAP` với `SoLuongGiam = ct.SoLuong` tại `MaKho`.
  - Khi chuyển `'HUY'`: Tự động loại bỏ khỏi View -> Tồn kho tự động hoàn lại, không sinh bút toán đảo.
- **`NhatKyThaoTac`**: Ghi log thao tác tạo, hoàn thành, hủy.

### 1.2. ViewModels (`ViewModels/TraHangNhap/`):
- `TraHangNhapIndexVM`: Danh sách phân trang (`PagedResult<TraHangNhapListItemVM>`), bộ lọc, thống kê tổng kỳ.
- `TraHangNhapFilterVM`: Kế thừa `BaseFilterParams`, lọc `DateFilterType`, `Statuses`, `NccIds`, `CreatorIds`, `PayerIds`, `Q`, `AdvSup`, `AdvProd`, `DaDanhDau`.
- `TraHangNhapListItemVM`: Dòng danh sách (`MaPhieuHienThi`, `NgayTra`, `TenNcc`, `TenKho`, `TenNguoiTra`, `TenNguoiLap`, `TongSoLuong`, `SoMatHang`, `TongTienHang`, `GiamGia`, `CanThuNcc`, `DaThuNcc`, `TrangThai`, `BadgeClass`, `DaDanhDau`).
- `TraHangNhapFormVM`: Form Lập/Sửa (`MaPhieuTraHang`, `MaPhieu`, `MaKho`, `TenKho`, `MaDonViCapHang`, `TenNcc`, `MaNvlap`, `TenNguoiLap`, `MaNvtra`, `NgayTra`, `TrangThai`, `GiamGia`, `LoaiGiamGia`, `DaThuNcc`, `GhiChu`, `ChiTiets`).
- `TraHangNhapLineVM`: Dòng hàng trả (`MaHang`, `MaHangNoiBo`, `TenHang`, `TenDvt`, `SoLuong`, `GiaVon`, `DonGiaTra`, `ThanhTien`, `TonKhoHienTai`).
- `SaveTraHangNhapRequest`: DTO submit form ("Lưu tạm" hoặc "Hoàn thành").

### 1.3. Bản đồ File:
- **Migration**: `migrations/sql/14_bo_sung_tra_hang_nhap.sql`.
- **Model**: `Models/PhieuTraHangNhap.cs`, `Models/ChiTietPhieuTraHangNhap.cs`, DbContext.
- **Service**: Bổ sung `FormatMaTraHangNhap` vào `MaChungTuHelper.cs`. Tận dụng `ITonKhoService.KiemTraXuatKhoAsync`.
- **Controller**: `Controllers/TraHangNhapController.cs` (kế thừa `BaseKhoController`).
- **Views**:
  - `Views/TraHangNhap/Index.cshtml`: Màn hình danh sách.
  - `Views/TraHangNhap/_TraHangNhapFilterSidebar.cshtml`: Sidebar lọc (Thời gian, Trạng thái, Người tạo, Người trả, NCC).
  - `Views/TraHangNhap/_TraHangNhapTable.cshtml`: Bảng danh sách + hàng tổng cộng + Drawer chi tiết inline 992px.
  - `Views/TraHangNhap/Create.cshtml`, `Edit.cshtml`, `_TraHangNhapForm.cshtml`: Form 2 cột.
- **CSS & JS**:
  - `wwwroot/css/tra-hang-nhap.css`, `wwwroot/js/tra-hang-nhap-index.js`, `wwwroot/js/tra-hang-nhap-form.js`.
- **Menu**: Thêm link vào `_MainMenu.cshtml`.

---

## 2. Luồng Nghiệp vụ & Giao diện (Chuẩn thực tế & Prototype)

### 2.1. Danh sách (`Index`)
- **Cột lọc trái (Leftmenu)**: Thời gian (`_KiotDateFilter`), Trạng thái (Phiếu tạm, Đã trả hàng, Đã hủy), MultiSelect dropdowns tag pills kèm tìm kiếm nội bộ (Người tạo, Người trả, NCC). Nút tròn F4 thò thụt có hover buffer chống flicker (khi thu gọn luôn bám sát lề trái 8px). Phím tắt F4 bật/tắt bộ lọc, F3 tìm kiếm nhanh, hỗ trợ mở/đóng trên mobile.
- **Topbar**: Tìm mã phiếu (F3), tìm nâng cao (NCC, hàng hóa), nút "+ Trả hàng nhập", "Xuất file", "Tùy chọn cột".
- **Bảng dữ liệu**:
  - Cuộn 2 chiều độc lập, hàng tổng hợp cố định dưới header.
  - Sắp xếp cột 3 trạng thái (Tăng -> Giảm -> Thường), sao bookmark, badges trạng thái chuẩn Bootstrap.
  - Drawer chi tiết inline 992px: Tab "Thông tin", thông tin chung, bảng hàng hóa có ô lọc con, ghi chú, khối tổng tiền (Số mặt hàng, Tổng tiền, Giảm giá, Cần trả, Đã trả, Tính vào công nợ).
  - Thanh thao tác chi tiết: Hủy phiếu, Sao chép, Xuất file, Lưu nhanh, In, Mở/Sửa phiếu, Hoàn thành.

### 2.2. Lập / Sửa Phiếu (`_TraHangNhapForm`)
- **Vùng bảng hàng hóa (Trái - `.f-main`)**:
  - Nút quay lại, ô tìm kiếm F3 autocomplete (hiển thị mã, tên, giá vốn, tồn kho).
  - Nạp Excel/CSV (kèm nút tải mẫu `Mau_Tra_Hang_Nhap.xlsx` hoặc `MauTraHangNhap.csv`), empty state với nút "Chọn file dữ liệu" (`#btnSelectFile`), xử lý server-side ClosedXML qua API `TraHangNhap/ImportExcel` tra cứu tồn kho và thông tin hàng từ CSDL.
  - Bảng lưới: Nút xóa, STT, Mã, Tên, ĐVT, Số lượng (.qty - cảnh báo đỏ nếu vượt tồn), Giá nhập, Giá trả lại (.qty), Thành tiền.
- **Panel thông tin (Phải - `.f-panel` / Rightmenu)**:
  - Khung 440px cố định `.f-panel-inner`, nút tròn toggle F4 trượt mượt mà 0.25s cubic-bezier (từ 425px về 8px khi thu gọn).
  - Căn lề phải sắc nét (chuẩn 185px) cho: Người trả & Thời gian trả (popup `.dtpop` chuẩn Kiểm tồn), Mã trả hàng nhập (cho phép nhập tay hoặc để trống tự động sinh mã, tự động viết hoa, kiểm tra chống trùng), Giảm giá (toggle VND / %), Tiền NCC trả (F8).
  - NCC autocomplete chip + modal tạo nhanh NCC.
  - Ghim đáy: Cụm 2 nút hành động `.f-actions` 56px xếp dọc icon/text chuẩn Kiểm tồn: "Lưu tạm (F9)" (`.save`, xanh dương `#0a66e8`) & "Hoàn thành (F10)" (`.done`, xanh lá `#12b34a`).

---

## 3. Quy tắc Nghiệp vụ Bất biến
1. **Chặn tồn âm**: Trước khi hoàn thành, bắt buộc gọi `ITonKhoService.KiemTraXuatKhoAsync`. Nếu `SoLuongTra > TonKhoHienTai`, chặn ngay.
2. **NCC vãng lai**: Nếu không chọn NCC, bắt buộc NCC trả đủ (`DaThuNcc == CanThuNcc`).
3. **Quy tắc Hủy**: Phiếu tạm hủy không đổi gì; Phiếu đã trả hủy thì `vw_BienDongKho` tự loại bỏ, tồn kho tự động hoàn lại, công nợ NCC được hoàn lại. Không xóa cứng.
4. **Giao dịch**: Mọi tác vụ bọc trong `BeginTransactionAsync()`, ghi `NhatKyThaoTac`.
