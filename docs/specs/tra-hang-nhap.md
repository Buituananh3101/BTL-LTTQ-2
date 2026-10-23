# Đặc tả Chức năng: Quản lý Trả hàng nhập (Purchase Returns - Windows Forms)

> **Mục tiêu**: Xây dựng phân hệ quản trị nghiệp vụ Trả hàng nhập cho Nhà cung cấp trên nền tảng Windows Forms (.NET Framework 4.7.2) bám sát trải nghiệm thực tế của KiotViet và tuân thủ tuyệt đối Khuôn mẫu Thiết kế Design System (Phiên 5, 13 & 15). Cung cấp màn hình danh sách đơn trả với bộ lọc 100% ComboBox trực quan, màn hình lập phiếu trả cho phép chọn sản phẩm từ danh mục hàng hóa (chặn âm kho, tính toán công nợ động), màn hình chi tiết đơn trả (chỉ giữ lại 2 nút: Xóa và Xuất file) và tiện ích xuất file CSV UTF-8 BOM.

---

## 1. Kiến trúc File & CSDL

### 1.1. Bản đồ File liên quan (File Map)
- **Thư mục phân hệ chính**: `BTL-LTTQ-2/GUI/Forms/TraHangNhap/`
  - `frmTraHangNhap.cs`, `.Designer.cs`, `.resx`: Màn hình danh sách đơn trả hàng nhập (MDI Child mở từ MenuStrip `Quản lý kho` $\to$ `Trả hàng nhập` / `mnuTraHangNhap`).
  - `frmThemPhieuTraHang.cs`, `.Designer.cs`, `.resx`: Form lập phiếu trả hàng nhập mới (mở khi bấm nút `+ Trả hàng nhập`).
  - `frmChiTietTraHangNhap.cs`, `.Designer.cs`, `.resx`: Form chi tiết đơn trả hàng nhập (mở khi nhấp đúp vào dòng trên lưới; chỉ có 2 nút hành động: `Xóa` và `Xuất file`).
  - `frmHienCotTraHang.cs`, `.Designer.cs`, `.resx`: Form popup ẩn/hiện cột cho DataGridView `dgvTraHangNhap`.
- **Form liên kết trong phân hệ Nhập hàng**:
  - `BTL-LTTQ-2/GUI/Forms/NhapHang/frmChiTietPhieuNhap.cs`: Cho phép mở form trả hàng theo từng đơn nhập gốc.
- **Truy xuất dữ liệu (DAL)**: `BTL-LTTQ-2/Classes/DataProcessing.cs` (ADO.NET thuần: `ReadData`, `ReadValue`, `ChangeDataTransaction`).
- **Phiên làm việc**: `BTL-LTTQ-2/Classes/SessionContext.cs` (lấy `MaTaiKhoan`, `MaKhoHienTai`).
- **Hỗ trợ giao diện**: `BTL-LTTQ-2/Classes/ComboboxHelper.cs` (tìm kiếm text trong ComboBox không dấu, tự động chọn khi Enter).

### 1.2. Cơ sở dữ liệu tham gia
- **`PhieuTraHangNhap`**:
  - `MaPhieuTraHang` INT IDENTITY(1,1) PRIMARY KEY
  - `MaPhieu` VARCHAR(30) UNIQUE NOT NULL (Mã tự sinh dạng `THN000001`)
  - `MaKho` INT NOT NULL (Khóa ngoại $\to$ `Kho`)
  - `MaDonViCapHang` INT NULL (Khóa ngoại $\to$ `DonViCapHang`)
  - `MaNvlap` INT NOT NULL (Khóa ngoại $\to$ `TaiKhoan`)
  - `MaNvtra` INT NULL (Khóa ngoại $\to$ `TaiKhoan`)
  - `NgayLap`, `NgayTra` DATETIME DEFAULT GETDATE()
  - `TrangThai` VARCHAR(20) (`NHAP`: Phiếu tạm; `DA_TRA_HANG`: Đã trả hàng; `HUY`: Đã hủy)
  - `TongSoLuong` DECIMAL(18,2)
  - `TongTienHang` DECIMAL(18,2)
  - `GiamGia` DECIMAL(18,2)
  - `CanThuNcc` DECIMAL(18,2) (Số tiền NCC hoàn lại hoặc trừ công nợ)
  - `DaThuNcc` DECIMAL(18,2) (Số tiền NCC đã thanh toán hoàn trả lại)
  - `GhiChu` NVARCHAR(500) NULL
  - `DaDanhDau` BIT DEFAULT 0
  - *(Ghi chú)*: Trong schema CSDL thực tế, bảng `PhieuTraHangNhap` không có cột `MaPhieuNhap`. Code phân hệ Trả hàng nhập được chuẩn hóa truy vấn đúng schema thực tế (hiển thị "Trả tự do" cho mã đơn nhập gốc). Khi nào có nhu cầu liên kết với đơn nhập gốc, cột `MaPhieuNhap` sẽ được đề xuất bổ sung theo đúng 3 Nguyên tắc vàng CSDL và Điều luật 17.
- **`ChiTietPhieuTraHangNhap`**:
  - `MaCtphieuTra` INT IDENTITY(1,1) PRIMARY KEY
  - `MaPhieuTraHang` INT NOT NULL (Khóa ngoại $\to$ `PhieuTraHangNhap` ON DELETE CASCADE)
  - `MaHang` INT NOT NULL (Khóa ngoại $\to$ `HangHoa`)
  - `SoLuong` DECIMAL(18,2) NOT NULL (CHECK `SoLuong > 0`)
  - `GiaVon` DECIMAL(18,2) NOT NULL (Đơn giá nhập/giá vốn tham chiếu)
  - `DonGiaTra` DECIMAL(18,2) NOT NULL (Giá trả lại)
  - `ThanhTien` DECIMAL(18,2) (`SoLuong * DonGiaTra`)
  - `GhiChu` NVARCHAR(255) NULL
- **View `vw_BienDongKho` & `vw_TonKhoHienTai`**:
  - Ghi nhận biến động loại `TRA_HANG_NHAP` làm giảm tồn kho sổ sách tại `MaKho` khi chứng từ ở trạng thái `DA_TRA_HANG`. Khi ở trạng thái `NHAP` hoặc `HUY`, số lượng không ảnh hưởng đến tồn kho.

---

## 2. Quy chuẩn Giao diện & Trải nghiệm Người dùng

### 2.1. Form Danh sách Đơn trả hàng (`frmTraHangNhap`)
- **Kích thước**: `1782 x 853` px, bố cục 3 khối chuẩn Design System theo `frmDanhSachHangHoa`.
- **Top bar (`grpTop`)**:
  - Tiêu đề & Hướng dẫn: `btnHuongDanSuDung` (Kích thước `43x29`, Text `?`).
  - Ô tìm kiếm: `lblTimKiem` ("Tìm kiếm:"), `txtTimKiem` (Kích thước `602x24`, hỗ trợ Debounce Timer 300ms tìm theo mã phiếu hoặc tên NCC).
  - Cụm nút thao tác bên phải:
    - `btnHienCot` ("Hiện cột", kích thước chuẩn `95x29`).
    - `btnXuatFile` ("Xuất file", kích thước `110x29`).
    - `btnThemTraHang` ("+ Trả hàng nhập", kích thước `145x29`).
    - Tất cả nút bấm hiển thị 100% tiếng Việt có dấu, phong cách chuẩn WinForms (`UseVisualStyleBackColor = true`).
- **Thanh lọc bên trái (`pnlBoLoc` & `grpBoLoc` - Width 387px)**:
  - **Trạng thái**: ComboBox `cboTrangThai` (kích thước `322x25`, DropDownList) gồm `-- Tất cả trạng thái --`, `Đã trả hàng`, `Phiếu tạm`, `Đã hủy`.
  - **Nhà cung cấp**: ComboBox `cboNhaCungCap` (kích thước `322x25`), tích hợp `ComboboxHelper.CaiDatTimKiem` hỗ trợ gõ phím tìm kiếm không dấu và Enter chọn nhanh.
  - **Người tạo**: ComboBox `cboNguoiTao` (kích thước `322x25`), tích hợp `ComboboxHelper.CaiDatTimKiem`.
  - **Người trả**: ComboBox `cboNguoiTra` (kích thước `322x25`), tích hợp `ComboboxHelper.CaiDatTimKiem`.
  - **Thời gian tạo**: GroupBox con `grpThoiGianTao` (kích thước `322x108`) lấy `frmDanhSachHangHoa` làm chuẩn duy nhất: gồm 2 DateTimePicker `dtpTuNgay` và `dtpDenNgay` (Format = Short `dd/MM/yyyy`).
- **Lưới dữ liệu trung tâm (`dgvTraHangNhap`)**:
  - Đệm đôi `DoubleBuffered = true` chống giật lag.
  - Hàng tiêu đề có CheckBox tổng dùng `CheckBoxRenderer` vẽ trực quan, phím `Space` bật/tắt chọn dòng.
  - Cột hiển thị: CheckBox chọn dòng, Mã trả hàng nhập (`THN...`), Thời gian (`dd/MM/yyyy HH:mm`), Nhà cung cấp, Tổng tiền hàng (`N0`), Giảm giá (`N0`), NCC cần trả (`N0`), NCC đã trả (`N0`), Trạng thái (`Đã trả hàng`, `Phiếu tạm`, `Đã hủy`).
  - Sự kiện `CellDoubleClick`: Nhấp đúp chuột vào bất kỳ dòng nào (trừ ô CheckBox) mở form `frmChiTietTraHangNhap`.
- **Phân trang đáy (`pnlPhanTrang`)**:
  - Tùy chọn `10, 20, 50, 100` dòng/trang (mặc định 20 dòng).
  - Nhãn: `Hiển thị X - Y trên tổng số Z phiếu trả hàng`.

### 2.2. Form Lập phiếu trả hàng (`frmThemPhieuTraHang`)
- **Thông tin chung**: Tự động sinh mã `THN00000X`, chọn Nhà cung cấp, chọn Kho xuất trả, chọn Người trả, Ngày trả, Ghi chú.
- **Chọn sản phẩm từ danh mục hàng hóa**:
  - ComboBox `cboHangHoa`: Tự động lọc sản phẩm đang kinh doanh và lấy động tồn kho tại kho xuất đã chọn.
  - Nhãn hiển thị: Đơn vị tính (`lblDonViTinhVal`), Tồn kho hiện tại (`lblTonKhoVal`).
  - Ô nhập: Số lượng trả (`txtSoLuongTra`, mặc định 1), Giá trả lại (`txtDonGiaTra`). Hỗ trợ nhấn `Enter` để thêm ngay vào bảng.
- **Bảng hàng trả (`dgvHangTra`)**:
  - Cột: STT, Mã hàng, Tên hàng, ĐVT, Tồn kho, Số lượng trả, Đơn giá trả, Thành tiền, Nút xóa dòng.
  - Cho phép sửa trực tiếp Số lượng trả và Giá trả lại trên ô lưới; tự động kiểm tra chặn số âm và chặn vượt tồn kho hiện tại.
- **Tổng kết & Thao tác**:
  - Tính toán: Tổng số lượng trả, Tổng tiền hàng, Giảm giá, NCC cần trả, Tiền NCC đã trả.
  - Nút bấm: `Lưu tạm` (`TrangThai = 'NHAP'`), `Trả hàng` (`TrangThai = 'DA_TRA_HANG'`), `Bỏ qua`. Bọc toàn bộ trong `DataProcessing.ChangeDataTransaction`.

### 2.3. Form Chi tiết đơn trả (`frmChiTietTraHangNhap`)
- **Header**: Mã phiếu `THN...`, Badge trạng thái màu (Xanh lá: Đã trả hàng, Vàng cam: Phiếu tạm, Đỏ: Đã hủy), Kho chi nhánh.
- **Thông tin**: Người tạo, Người trả, Ngày trả, Tên NCC, Mã nhập hàng gốc (nếu có liên kết, hoặc "Trả tự do").
- **Bảng chi tiết**: Danh sách sản phẩm trả với Mã hàng, Tên hàng, ĐVT, Số lượng, Giá nhập, Giá trả lại, Thành tiền.
- **Khối tổng kết**: Ghi chú, Số lượng mặt hàng, Tổng tiền hàng, Giảm giá, NCC cần trả, NCC đã trả.
- **Nút bấm thao tác**: **Chỉ giữ lại 2 nút nghiệp vụ: `Xóa` (thay vì Hủy) và `Xuất file`** (kèm nút `Đóng` / CancelButton).
  - Nút `Xóa`: Nếu phiếu tạm (`NHAP`), xóa vĩnh viễn khỏi CSDL; nếu đã trả hàng (`DA_TRA_HANG`), chuyển sang trạng thái `HUY` để hoàn tồn tự động qua View, tuyệt đối không DROP dữ liệu hoàn thành.
  - Nút `Xuất file`: Xuất chi tiết đơn hàng ra file CSV UTF-8 BOM chuẩn tiếng Việt.

---

## 3. Logic Nghiệp vụ & Ràng buộc (Validation)

1. **Ràng buộc Số lượng trả**:
   - `Số lượng trả > 0` và không được nhập số âm.
   - `Số lượng trả <= Tồn kho hiện tại` tại kho xuất trả hàng (chặn triệt để âm kho).
   - Danh sách hàng trả phải có ít nhất 1 mặt hàng.
2. **Khấu trừ Tài chính & Tiền NCC hoàn trả**:
   - $\text{NCC cần trả} = \max(0, \text{Tổng tiền hàng trả} - \text{Giảm giá})$.
   - Tiền NCC đã trả lưu nhận tiền thực tế nhận về từ NCC.
3. **An toàn Giao dịch & Toàn vẹn Dữ liệu**:
   - Mọi thao tác lưu, xóa phiếu tạm hoặc hủy phiếu hoàn thành đều bọc trọn vẹn trong `DataProcessing.ChangeDataTransaction`.
   - Chống SQL Injection 100% bằng `SqlParameter`.
