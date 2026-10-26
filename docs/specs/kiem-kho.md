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
| [`frmChiTietKiemKho`](../../BTL-LTTQ-2/GUI/Forms/KiemKho/frmChiTietKiemKho.cs) | Modal Dialog | Màn hình chi tiết khi nhấn đúp vào 1 phiếu kiểm (theo ảnh mẫu 3), cung cấp các nút: **Xóa** (khóa vĩnh viễn phiếu đã cân bằng theo Luật 6), **Xuất file** và **Hoàn thành** (khi là phiếu tạm để cân bằng kho). |
| [`frmNhapKiemKhoFile`](../../BTL-LTTQ-2/GUI/Forms/KiemKho/frmNhapKiemKhoFile.cs) | Modal Dialog | Màn hình nhập dữ liệu kiểm kho từ file (Excel/CSV), cung cấp tải file mẫu Excel/CSV chuẩn theo khuôn mẫu `frmNhapHangHoaFile`. |
| [`frmHienCotKiemKho`](../../BTL-LTTQ-2/GUI/Forms/KiemKho/frmHienCotKiemKho.cs) | Tool Window | Popup tùy chọn ẩn/hiện các cột của bảng danh sách phiếu kiểm kho `dgvKiemKho`. |
| [`frmHienCotHangKiem`](../../BTL-LTTQ-2/GUI/Forms/KiemKho/frmHienCotHangKiem.cs) | Tool Window | Popup tùy chọn ẩn/hiện các cột của bảng chi tiết hàng kiểm kê `dgvHangKiem` trong form thêm mới. |

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
  - `cboTrangThai`: ComboBox DropDownList (-- Tất cả trạng thái --, Đã cân bằng, Phiếu tạm; không có trạng thái Đã hủy do phiếu đã cân bằng không cho xóa/hủy, còn phiếu tạm thì bị xóa hẳn).
  - `cboNguoiTao`: ComboBox tìm kiếm tiếng Việt không dấu qua `ComboboxHelper.CaiDatTimKiem`.
  - `cboKho`: ComboBox chọn chi nhánh kho.
- **Lưới danh sách `dgvKiemKho`**:
  - Gồm các cột hiển thị chuẩn: Checkbox (colChon), Mã kiểm kho (colMaPhieu), Ngày cân bằng (colNgayCanBang), SL lệch tăng (colSlLechTang), SL lệch giảm (colSlLechGiam), SL thực tế (colSlThucTe), Ghi chú (colGhiChu - ẩn mặc định), Tổng thực tế (colTongThucTe), Tổng chênh lệch (colTongChenhLech), Thời gian tạo (colThoiGian - ẩn mặc định, hiển thị sau Tổng chênh lệch và trước Trạng thái khi bật trong Hiện cột), Trạng thái (colTrangThai).
  - Nghiệp vụ thời gian: Cột "Thời gian tạo" chỉ lưu thời gian tạo ra phiếu (ThoiDiemChot), cố định không thay đổi khi đổi trạng thái phiếu. Cột "Ngày cân bằng" đặt ngay sau cột Mã kiểm kho; nếu là phiếu tạm thì để trống, lưu thời gian phiếu được tạo ở trạng thái đã cân bằng hoặc cập nhật thời gian là thời gian phiếu tạm được ấn hoàn thành.
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
- **Thanh nút đáy**:
  - Nút **Xóa** (`btnXoa`):
    + Nếu phiếu ở trạng thái `'DA_DIEU_CHINH'`: Khóa vĩnh viễn theo Luật 6, không cho xóa.
    + Nếu phiếu ở trạng thái `'NHAP'`: Xác nhận Yes/No, xóa phiếu và chi tiết, ghi log nhật ký.
  - Nút **Xuất file** (`btnXuatFile`): Xuất chi tiết phiếu kiểm ra file Excel hoặc CSV (UTF-8 BOM).
  - Nút **Hoàn thành** (`btnHoanThanh`): Chỉ hiển thị khi phiếu ở trạng thái Phiếu tạm (`NHAP`, `DANG_KIEM`, `DA_KIEM`). Bấm Hoàn thành sẽ hỏi xác nhận, bọc trong Transaction cập nhật `PhieuKiemKe` sang `DA_DIEU_CHINH`, sinh `PhieuDieuChinh` và `ChiTietPhieuDieuChinh` trạng thái `DA_DUYET`, cập nhật tồn kho sổ sách qua `vw_TonKhoHienTai` và khóa vĩnh viễn phiếu.
  - Nút **Đóng** (`btnDong`).

### 3.3. Màn hình Thêm mới `frmThemPhieuKiemKho`
- **Kích thước form**: Size Lớn `ClientSize = (1182, 803)` theo Điều luật 15e.
- **Thanh tiêu đề trên (`pnlTopLeft`)**: Tiêu đề "Thêm phiếu kiểm kho", nút `Nhập file` ở góc phải, thanh chọn hàng nhanh gồm `cboHangHoa`, `nudSoLuongThucTe`, nút `+ Thêm`, nút `Hiện cột` (mở popup `frmHienCotHangKiem`).
- **Right Panel (`pnlRight`)**: Chứa thông tin Người kiểm, Ngày kiểm, Mã phiếu (không hiển thị trường trạng thái; trạng thái được quyết định khi người dùng ấn Lưu tạm hoặc Lưu), Kho kiểm, Ghi chú, tóm tắt kết quả (Số dòng kiểm, Tổng thực tế, Tổng lệch tăng, Tổng lệch giảm, Tổng chênh lệch). Ở đáy là thanh nút chuẩn: `Bỏ qua` ➜ `Lưu tạm` ➜ `Lưu`.
- **Lưới kiểm kê `dgvHangKiem`**:
  - Thao tác xóa dòng: Bỏ cột nút Xóa trên lưới; thay thế bằng thao tác nhấn đúp chuột (`Double click`) vào dòng mặt hàng để xóa, hiển thị hộp thoại xác nhận Yes/No trước khi xóa.
  - Cột `SL thực tế (nhập)` cho phép người dùng click sửa trực tiếp (`EditMode = EditOnEnter`), có màu nền phân biệt rõ (`#FEFCE8`) và ToolTip hướng dẫn.
  - Tự động tính chênh lệch số lượng và thành tiền lệch realtime.

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
