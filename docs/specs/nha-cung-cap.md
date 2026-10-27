# Đặc tả Chức năng: Quản lý Nhà cung cấp (Supplier Management - Windows Forms)

> **Mục tiêu**: Quản lý danh mục Nhà cung cấp chuẩn giao diện & nghiệp vụ hệ thống kho hàng nội bộ. Tìm kiếm tức thì, lọc đa tiêu chí bằng ComboBox, phân trang SQL Server, mở form MDI, form phụ thêm/sửa NCC bọc Transaction, tạo nhanh nhóm NCC, nhập file danh sách NCC từ Excel/CSV và xuất dữ liệu ra file CSV/Excel UTF-8 BOM.

---

## 1. Kiến trúc File & CSDL

### 1.1. Bản đồ File liên quan (File Map)
- **Thư mục chức năng**: `BTL-LTTQ-2/GUI/Forms/NhaCungCap/`
  - `frmNhaCungCap.cs`, `frmNhaCungCap.Designer.cs`, `frmNhaCungCap.resx`: Form chính hiển thị danh sách nhà cung cấp (kích thước chuẩn 1782x853), thanh tìm kiếm F3, các combobox bộ lọc bên trái (chiều rộng 340px), DataGridView định dạng chuẩn đệm đôi `DoubleBuffered` và thanh phân trang (tùy chọn 10, 20, 50, 100 dòng, mặc định 20 dòng).
  - `frmDonViCapHang.cs`, `frmDonViCapHang.Designer.cs`, `frmDonViCapHang.resx`: Form Thêm mới nhà cung cấp (kích thước 1182x803 theo Rule 15e, mã tự sinh `NCC00001`, validate chống trùng, nạp nhóm, bọc `ChangeDataTransaction`).
  - `frmChiTietNhaCungCap.cs`, `frmChiTietNhaCungCap.Designer.cs`, `frmChiTietNhaCungCap.resx`: Form Chi tiết & Cập nhật hồ sơ nhà cung cấp (kích thước 1182x803, hiển thị công nợ & tổng tiền mua hàng, cập nhật thông tin, xóa / ngừng hoạt động / cho hoạt động lại, bọc `ChangeDataTransaction`).
  - `frmNhomNhaCungCap.cs`, `frmNhomNhaCungCap.Designer.cs`, `frmNhomNhaCungCap.resx`: Form phụ Tạo mới nhóm nhà cung cấp khi bấm nút "+" trên bộ lọc hoặc form chi tiết.
  - `frmHienCotNcc.cs`, `frmHienCotNcc.Designer.cs`, `frmHienCotNcc.resx`: Form phụ popup tùy chọn Ẩn / Hiện cột của DataGridView nhà cung cấp (chỉ gồm 7 cột dữ liệu, không cho phép ẩn cột chọn dòng hệ thống).
  - `frmNhapNhaCungCapFile.cs`, `frmNhapNhaCungCapFile.Designer.cs`, `frmNhapNhaCungCapFile.resx`: Form phụ nhập danh sách nhà cung cấp từ file Excel (.xlsx, .xls) hoặc CSV (tải file mẫu, tùy chọn cập nhật thông tin khi trùng mã hoặc bỏ qua, tự động sinh mã `NCC0000X`, tự động tạo nhóm mới nếu chưa có, bọc `ChangeDataTransaction`).
- **Form cha MDI & Menu**: `BTL-LTTQ-2/GUI/Forms/frmMain.cs` (gắn sự kiện `mnuNhaCungCap_Click` mở và kích hoạt form MDI Child).
- **Truy xuất dữ liệu (DAL)**: `BTL-LTTQ-2/Classes/DataProcessing.cs` (ADO.NET thuần: `ReadData`, `ReadValue`, `ChangeDataTransaction`).
- **Quản lý phiên đăng nhập**: `BTL-LTTQ-2/Classes/SessionContext.cs` (lấy `MaTaiKhoan`, `MaKhoHienTai`, vai trò).

### 1.2. Cơ sở dữ liệu (Tuân thủ 3 Nguyên tắc Vàng CSDL)
- **Bảng `DonViCapHang`**:
  - `MaDonViCapHang` INT IDENTITY(1,1) PRIMARY KEY
  - `MaNcc` NVARCHAR(50) NULL (Mã hiển thị nội bộ, ví dụ `NCC00001`)
  - `TenDonVi` NVARCHAR(250) NOT NULL (Tên nhà cung cấp)
  - `DienThoai` NVARCHAR(50) NULL
  - `Email` NVARCHAR(150) NULL
  - `DiaChi` NVARCHAR(255) NULL
  - `KhuVuc` NVARCHAR(150) NULL
  - `MaNhomNcc` INT NULL (Khóa ngoại $\to$ `NhomNhaCungCap`)
  - `MaSoThue` NVARCHAR(50) NULL
  - `GhiChu` NVARCHAR(500) NULL
  - `TrangThai` NVARCHAR(50) DEFAULT `'HOAT_DONG'` (`HOAT_DONG` / `NGUNG_GIAO_DICH`)
  - `DaDanhDau` BIT DEFAULT 0
  - `NgayTao` DATETIME DEFAULT GETDATE()
- **Bảng `NhomNhaCungCap`**:
  - `MaNhomNcc` INT IDENTITY(1,1) PRIMARY KEY
  - `TenNhom` NVARCHAR(150) NOT NULL
  - `GhiChu` NVARCHAR(255) NULL

---

## 2. Luồng Tương tác Nghiệp vụ Chi tiết

| Luồng | Tương tác người dùng | Giao diện WinForms (GUI) | Xử lý & CSDL (DAL / SQL) |
|---|---|---|---|
| **1. Mở danh sách** | Vào Menu "Mua hàng" ➜ "Nhà cung cấp" | `mnuNhaCungCap_Click` kiểm tra form con trong `MdiChildren`. Nếu có ➜ `Activate()`, nếu chưa ➜ `Show()` | Nạp nhóm NCC vào `cboNhomNCC`, nạp dữ liệu trang 1, tính `TongMua` và `NoCanTraHienTai` |
| **2. Lọc đa tiêu chí** | Chọn Nhóm, Mức tổng mua, Nợ hiện tại, Trạng thái hoặc Khoảng thời gian tạo | Đặt lại `trangHienTai = 1`, gọi `HienThiDanhSach()` nạp lại dữ liệu | Mệnh đề `WHERE` lọc động theo các tham số `@MaNhom`, `@Search`, `@TuNgay`, `@DenNgay`, mức tiền |
| **3. Tìm kiếm tức thời** | Gõ từ khóa vào ô Tìm kiếm | Sự kiện `txtTimKiem_TextChanged` kích hoạt tìm tức thời | Lọc `MaNcc LIKE @Search OR TenDonVi LIKE @Search OR DienThoai LIKE @Search` |
| **4. Phân trang** | Bấm "◀ Trước", "Sau ▶" hoặc đổi số dòng: 10, 20, 50, 100 | `cboSoDongMoiTrang_SelectedIndexChanged`, `btnTrangTruoc_Click`, `btnTrangSau_Click` | SQL Server phân trang chuẩn `OFFSET @Skip ROWS FETCH NEXT @Take ROWS ONLY`. Nhãn hiển thị: `Hiển thị X - Y trên tổng số Z nhà cung cấp` |
| **5. Thêm mới NCC** | Bấm nút "+ Nhà cung cấp" | Mở `frmDonViCapHang` dạng modal dialog `ShowDialog(this)`, tự sinh mã `NCC0000X` | Validate bắt buộc `TenDonVi`, kiểm tra chống trùng `MaNcc`, bọc `ChangeDataTransaction` |
| **6. Xem & Sửa chi tiết NCC** | Double-click vào một dòng trên DataGridView | Mở `frmChiTietNhaCungCap` dạng modal dialog `ShowDialog(this)`, nạp thông tin và thống kê công nợ | Cập nhật `DonViCapHang`, xóa hoặc ngừng hoạt động; khi đóng trả `DialogResult.OK` tự làm mới danh sách |
| **7. Tạo nhanh Nhóm NCC** | Bấm nút "+" cạnh combobox Nhóm trên `frmNhaCungCap` | Mở modal dialog `frmNhomNhaCungCap` | Thêm vào `NhomNhaCungCap`, tự động nạp lại combobox và chọn ngay nhóm vừa tạo |
| **8. Tùy chọn Ẩn/Hiện cột** | Bấm nút "Hiện cột" | Mở popup `frmHienCotNcc` ngay dưới nút | Bật/tắt `dgv.Columns[col].Visible` tức thì, nhấn ESC hoặc click ra ngoài để đóng |
| **9. Nhập file NCC** | Bấm nút "Nhập file" | Mở modal dialog `frmNhapNhaCungCapFile` | Đọc dữ liệu từ file Excel (.xlsx, .xls) hoặc CSV; tải file mẫu; tùy chọn cập nhật hoặc bỏ qua; bọc Transaction |
| **10. Xuất file Excel / CSV** | Bấm nút "Xuất file" | Hộp thoại `SaveFileDialog` hỗ trợ lưu file `.xls` (HTML Table UTF-8 header màu `#0078D7`, format số `#,##0`) và `.csv` (UTF-8 BOM). Ưu tiên xuất các dòng được tích chọn CheckBox; nếu không chọn dòng nào sẽ xuất toàn bộ dữ liệu thỏa mãn bộ lọc hiện tại | Xuất toàn bộ danh sách nhà cung cấp hoặc các nhà cung cấp được chọn ra file Excel/CSV |

### 3.5. Form Nhập file Nhà cung cấp (`frmNhapNhaCungCapFile`):
- **Tải file mẫu Excel (`Mau_Nhap_Nha_Cung_Cap.xls`)**:
  - Chuẩn hóa định dạng bảng HTML Table UTF-8 with BOM đồng bộ 100% với khuôn mẫu Hàng hóa và Nhập hàng.
  - Cấu hình Workbook XML với tên Worksheet `MauNhaCungCap` và kích hoạt `<x:DisplayGridlines/>` hiển thị đường lưới ô Excel tiêu chuẩn.
  - CSS styling đồng nhất: Tiêu đề cột `th` màu xanh `#0078D7`, chữ trắng Segoe UI 10pt in đậm; ô dữ liệu `td` Segoe UI 9.5pt viền xám nhạt `#E0E0E0`.
  - Định dạng kiểu chuỗi `.text` (`mso-number-format: "\@"`) cho các cột: `Mã nhà cung cấp`, `Điện thoại`, `Mã số thuế` (chống mất số 0 ở đầu).
  - Cung cấp 3 dòng dữ liệu mẫu chuẩn trực quan (bao gồm dòng để trống mã nhà cung cấp để minh họa quy tắc tự động sinh mã).
- **Bộ đọc đa định dạng**: Hỗ trợ `.xlsx` (OpenXml ZipArchive), `.xls` (HTML Table / OLEDB), `.csv` (RFC 4180).
- **Quy trình nhập**: Tự động nhận diện cột linh hoạt, bọc `ChangeDataTransaction`, tùy chọn cập nhật hoặc bỏ qua khi trùng mã NCC.

---

## 3. Quy chuẩn Giao diện & Bộ lọc

### 3.1. Cột lọc bên trái (Sidebar Filter - Đồng bộ frmDanhSachHangHoa):
1. **Nhóm nhà cung cấp**:
   - ComboBox `cboNhomNCC`: DropDownList ("-- Tất cả các nhóm --", danh sách nhóm từ CSDL), thu hẹp chiều rộng (287px) cân đối.
   - Nút `btnThemNhomNCC` ("+"): Kích thước hình vuông hoàn hảo `29x29px` đặt tại `(311, 66)` ngay bên cạnh `cboNhomNCC`, mép phải căn thẳng hàng tuyệt đối với các ComboBox bên dưới (340px). Bấm nút mở dialog `frmNhomNhaCungCap` để tạo nhanh nhóm NCC và tự động chọn nhóm vừa tạo.
2. **Tổng mua**:
   - ComboBox `cboMucTongMua`: DropDownList ("-- Tất cả mức mua --", "Dưới 5 triệu", "Từ 5 - 20 triệu", "Từ 20 - 50 triệu", "Trên 50 triệu").
3. **Nợ hiện tại**:
   - ComboBox `cboNoHienTai`: DropDownList ("-- Tất cả công nợ --", "Còn nợ (> 0)", "Hết nợ (= 0)", "Nợ lớn (> 10 triệu)", "Nợ rất lớn (> 50 triệu)").
4. **Trạng thái**:
   - ComboBox `cboTrangThai`: DropDownList ("-- Tất cả trạng thái --", "Đang hoạt động", "Ngừng hoạt động").
5. **Khoảng thời gian tạo** (GroupBox `grpThoiGianTao` đồng bộ `frmDanhSachHangHoa`):
   - `dtpTuNgay`: Chọn từ ngày (mặc định đầu năm).
   - `dtpDenNgay`: Chọn đến ngày (mặc định hôm nay).
   - Tự động kích hoạt lọc dữ liệu khi thay đổi giá trị ngày.

### 3.2. Nút bấm thao tác trên cùng:
- Nút `+ Nhà cung cấp` (`btnThemNhaCungCap`): Định dạng chuẩn nút hệ thống WinForms (`UseVisualStyleBackColor = true`), kích thước và vị trí đồng bộ với nút `+ Hàng hóa` trên `frmDanhSachHangHoa`.
- Nút `Nhập file` (`btnNhapFile`): Hiển thị tiếng Việt có dấu rõ ràng (Luật 14), mở `frmNhapNhaCungCapFile`.
- Nút `Xuất file` (`btnXuatFile`), `Hiện cột` (`btnHienCot`), `Hướng dẫn sử dụng` (`btnHuongDanSuDung`): Căn phải đồng bộ toàn hệ thống.

### 3.3. Chuẩn DataGridView:
- Đệm đôi chống giật lag chuột: `DoubleBuffered = true`.
- Cột hộp kiểm `colChon`: Cấu hình `DataPropertyName = "Chon"`, `TrueValue = true`, `FalseValue = false`, `NullValue = false`, `ReadOnly = false`, kích hoạt `EditMode = EditOnEnter` và tự động `CommitEdit` tức thời khi click chuột.
- Header CheckBox "Chọn tất cả": Tự động vẽ hộp kiểm chuẩn Windows VisualStyles (`CheckBoxRenderer.DrawCheckBox` qua `CellPainting`), đồng bộ 100% giao diện với các checkbox bên dưới. Click tiêu đề cột để Chọn tất cả / Bỏ chọn tất cả các dòng (`ColumnHeaderMouseClick`), giải phóng edit buffer (`CurrentCell = null`) và sử dụng biến cờ chống lặp `isUpdatingCheckAll` để không bao giờ bị bỏ sót dòng cuối cùng. Bỏ qua mở form sửa khi click đúp vào cột này.
- Các cột dữ liệu khác: Khóa `ReadOnly = true` bảo đảm an toàn dữ liệu danh sách.
- Màu dòng xen kẽ: `AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(248, 250, 252)`.
- Màu dòng được chọn: `DefaultCellStyle.SelectionBackColor = Color.FromArgb(224, 231, 255)`, `ForeColor = Color.FromArgb(30, 41, 59)`.
- Định dạng cột số tiền: Căn phải `MiddleRight`, Format `"N0"`. Nợ cần trả > 0 hiển thị màu đỏ in đậm.
- Định dạng trạng thái: Căn giữa `MiddleCenter`. "Đang hoạt động" màu `ForestGreen`, "Ngừng hoạt động" màu `Gray`.

### 3.4. Form Chi tiết Nhà cung cấp (`frmDonViCapHang` - Chuẩn hóa theo `frmHangHoa`):
- **Bố cục tổng thể**: Độ rộng `960px`, chiều cao động (`610px` khi thêm mới, `705px` khi sửa), các nhãn thông tin (`Label`) luôn nằm phía TRÊN ô nhập liệu (`TextBox` / `ComboBox`), tối ưu hóa toàn bộ không gian trống.
- **Khối Thông tin cơ bản**:
  - Cột 1 (trái, 400px): `Mã nhà cung cấp` (`txtMaNcc`), `Tên nhà cung cấp (*)` (`txtTenDonVi`), `Nhóm nhà cung cấp` (`cboNhomNcc`).
  - Cột 2 (phải, 450px): `Mã số thuế` (`txtMaSoThue`), `Địa chỉ` (`txtDiaChi` - ô nhập nhiều dòng `Multiline = true`, chiều cao 99px có `ScrollBars = Vertical` cân xứng với 2 dòng input bên trái).
- **Khối GroupBox "Thông tin liên hệ" (`grpLienHe`)**:
  - Tiêu đề đậm `Thông tin liên hệ`.
  - Gồm 3 cột thẳng hàng: `Điện thoại` (`txtDienThoai`), `Email` (`txtEmail`), `Khu vực` (`txtKhuVuc`).
- **Khối GroupBox "Trạng thái & Ghi chú" (`grpGhiChu`)**:
  - Tiêu đề đậm `Trạng thái & Ghi chú`.
  - Cột trái: `Trạng thái` (`cboTrangThai` DropDownList).
  - Cột phải: `Ghi chú` (`txtGhiChu` Multiline).
- **Khối GroupBox "Giao dịch & Công nợ" (`grpCongNo`)**:
  - Tiêu đề đậm `Giao dịch & Công nợ`, chỉ hiển thị khi mở ở chế độ chỉnh sửa (`maDonViCapHang > 0`).
  - Hiển thị `Tổng tiền mua hàng` (`lblTongMuaVal`) và `Nợ cần trả hiện tại` (`lblNoHienTaiVal`).
- **Thanh thao tác Bottom (`pnlBottom`)**:
  - Nút `Bỏ qua` (`btnBoQua`) và `Lưu` (`btnLuu`) chuẩn WinForms (`UseVisualStyleBackColor = true`), căn phải (`Anchor = Bottom, Right`).

### 3.5. Form Nhập file Nhà cung cấp (`frmNhapNhaCungCapFile`):
- **Tải file mẫu Excel (`Mau_Nhap_Nha_Cung_Cap.xls`)**:
  - Chuẩn hóa định dạng bảng HTML Table UTF-8 with BOM đồng bộ 100% với khuôn mẫu Hàng hóa và Nhập hàng.
  - Cấu hình Workbook XML với tên Worksheet `MauNhaCungCap` và kích hoạt `<x:DisplayGridlines/>` hiển thị đường lưới ô Excel tiêu chuẩn.
  - CSS styling đồng nhất: Tiêu đề cột `th` màu xanh `#0078D7`, chữ trắng Segoe UI 10pt in đậm; ô dữ liệu `td` Segoe UI 9.5pt viền xám nhạt `#E0E0E0`.
  - Định dạng kiểu chuỗi `.text` (`mso-number-format: "\@"`) cho các cột: `Mã nhà cung cấp`, `Điện thoại`, `Mã số thuế` (chống mất số 0 ở đầu).
  - Cung cấp 3 dòng dữ liệu mẫu chuẩn trực quan (bao gồm dòng để trống mã nhà cung cấp để minh họa quy tắc tự động sinh mã).
- **Bộ đọc đa định dạng**: Hỗ trợ `.xlsx` (OpenXml ZipArchive), `.xls` (HTML Table / OLEDB), `.csv` (RFC 4180).
- **Quy trình nhập**: Tự động nhận diện cột linh hoạt, bọc `ChangeDataTransaction`, tùy chọn cập nhật hoặc bỏ qua khi trùng mã NCC.

---

## 4. Quy tắc Nghiệp vụ Bất biến
1. **Mã nhà cung cấp**: Tự động sinh `NCC00001`, `NCC00002`... nếu để trống. Bắt buộc kiểm tra chống trùng lặp.
2. **Tên nhà cung cấp**: Bắt buộc nhập, tối đa 250 ký tự.
3. **Bảo vệ toàn vẹn dữ liệu chứng từ**: Tuyệt đối không xóa cứng nhà cung cấp đã phát sinh phiếu nhập kho (`PhieuNhap`).
4. **Tính toán Công nợ & Tổng mua**:
   - `TongMua`: Tổng giá trị các phiếu nhập kho trạng thái hoàn thành (`DA_XAC_NHAN` / `DA_NHAN`).
   - `NoCanTraHienTai`: Tính toán động từ các phiếu nhập (`PhieuNhap.CanTraNcc`), bảng `DonViCapHang` không lưu trường nợ tĩnh.
