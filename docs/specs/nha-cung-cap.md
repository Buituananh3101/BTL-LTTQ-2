# Đặc tả Chức năng: Quản lý Nhà cung cấp (Supplier Management - Windows Forms)

> **Mục tiêu**: Quản lý danh mục Nhà cung cấp chuẩn giao diện & nghiệp vụ hệ thống kho hàng nội bộ. Tìm kiếm tức thì, lọc đa tiêu chí bằng ComboBox, phân trang SQL Server, mở form MDI, form phụ thêm/sửa NCC bọc Transaction, tạo nhanh nhóm NCC và xuất dữ liệu ra file CSV/Excel UTF-8 BOM.

---

## 1. Kiến trúc File & CSDL

### 1.1. Bản đồ File liên quan (File Map)
- **Thư mục chức năng**: `BTL-LTTQ-2/GUI/Forms/NhaCungCap/`
  - `frmNhaCungCap.cs`, `frmNhaCungCap.Designer.cs`, `frmNhaCungCap.resx`: Form chính hiển thị danh sách nhà cung cấp, thanh tìm kiếm F3, các combobox bộ lọc bên trái, DataGridView định dạng chuẩn và thanh phân trang.
  - `frmDonViCapHang.cs`, `frmDonViCapHang.Designer.cs`, `frmDonViCapHang.resx`: Form phụ Thêm mới / Chỉnh sửa hồ sơ nhà cung cấp (mã tự sinh `NCC00001`, validate chống trùng, nạp nhóm, bọc `ChangeDataTransaction`).
  - `frmNhomNhaCungCap.cs`, `frmNhomNhaCungCap.Designer.cs`, `frmNhomNhaCungCap.resx`: Form phụ Tạo mới nhóm nhà cung cấp khi bấm "+ Tạo mới" trên bộ lọc hoặc form chi tiết.
  - `frmHienCotNcc.cs`, `frmHienCotNcc.Designer.cs`, `frmHienCotNcc.resx`: Form phụ popup tùy chọn Ẩn / Hiện cột của DataGridView nhà cung cấp.
- **Form cha MDI & Menu**: `BTL-LTTQ-2/GUI/Forms/frmMain.cs` (gắn sự kiện `mnuNhaCungCap_Click` mở và kích hoạt form MDI Child).
- **Truy xuất dữ liệu (DAL)**: `BTL-LTTQ-2/DAL/DataProcessing.cs` (ADO.NET thuần: `ReadData`, `ReadValue`, `ChangeDataTransaction`).

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
| **4. Phân trang** | Bấm "◀ Trước", "Sau ▶" hoặc đổi "Hiển thị: 15 dòng" | `cboSoDongMoiTrang_SelectedIndexChanged`, `btnTrangTruoc_Click`, `btnTrangSau_Click` | SQL Server phân trang chuẩn `OFFSET @Skip ROWS FETCH NEXT @Take ROWS ONLY` |
| **5. Thêm mới NCC** | Bấm nút "+ Nhà cung cấp" | Mở `frmDonViCapHang` chế độ thêm mới (`maNCC = 0`), tự sinh mã `NCC0000X` | Validate bắt buộc `TenDonVi`, kiểm tra chống trùng `MaNcc`, bọc `ChangeDataTransaction` |
| **6. Chỉnh sửa NCC** | Double-click vào một dòng trên DataGridView | Mở `frmDonViCapHang` chế độ sửa (`maNCC > 0`), nạp thông tin và thống kê công nợ | Cập nhật `DonViCapHang`, khi đóng trả `DialogResult.OK` tự làm mới danh sách |
| **7. Tạo nhanh Nhóm NCC** | Bấm nút "+" cạnh combobox Nhóm trên `frmNhaCungCap` | Mở modal dialog `frmNhomNhaCungCap` | Thêm vào `NhomNhaCungCap`, tự động nạp lại combobox và chọn ngay nhóm vừa tạo |
| **8. Tùy chọn Ẩn/Hiện cột** | Bấm nút "Hiện cột" | Mở popup `frmHienCotNcc` ngay dưới nút | Bật/tắt `dgv.Columns[col].Visible` tức thì, nhấn ESC hoặc click ra ngoài để đóng |
| **9. Xuất file CSV / Excel** | Bấm "Xuất file" | Hộp thoại `SaveFileDialog` lưu file `.csv` | Xuất đúng các cột đang hiển thị với định dạng UTF-8 BOM đọc tốt tiếng Việt trên Excel |

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

### 3.3. Chuẩn DataGridView:
- Đệm đôi chống giật lag chuột: `DoubleBuffered = true`.
- Cột hộp kiểm `colChon`: Cấu hình `DataPropertyName = "Chon"`, `TrueValue = true`, `FalseValue = false`, `NullValue = false`, `ReadOnly = false`, kích hoạt `EditMode = EditOnEnter` và tự động `CommitEdit` tức thời khi click chuột.
- Header CheckBox "Chọn tất cả": Tự động vẽ hộp kiểm chuẩn Windows VisualStyles (`CheckBoxRenderer.DrawCheckBox` qua `CellPainting`), đồng bộ 100% giao diện với các checkbox bên dưới. Click tiêu đề cột để Chọn tất cả / Bỏ chọn tất cả các dòng (`ColumnHeaderMouseClick`), giải phóng edit buffer (`CurrentCell = null`) và sử dụng biến cờ chống lặp `isUpdatingCheckAll` để không bao giờ bị bỏ sót dòng cuối cùng. Bỏ qua mở form sửa khi click đúp vào cột này.
- Các cột dữ liệu khác: Khóa `ReadOnly = true` bảo đảm an toàn dữ liệu danh sách.
- Màu dòng xen kẽ: `AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(248, 250, 252)`.
- Màu dòng được chọn: `DefaultCellStyle.SelectionBackColor = Color.FromArgb(224, 231, 255)`, `ForeColor = Color.FromArgb(30, 41, 59)`.
- Định dạng cột số tiền: Căn phải `MiddleRight`, Format `"N0"`. Nợ cần trả > 0 hiển thị màu đỏ in đậm.
- Định dạng trạng thái: Căn giữa `MiddleCenter`. "Đang hoạt động" màu `ForestGreen`, "Ngừng hoạt động" màu `Gray`.

---

## 4. Quy tắc Nghiệp vụ Bất biến
1. **Mã nhà cung cấp**: Tự động sinh `NCC00001`, `NCC00002`... nếu để trống. Bắt buộc kiểm tra chống trùng lặp.
2. **Tên nhà cung cấp**: Bắt buộc nhập, tối đa 250 ký tự.
3. **Bảo vệ toàn vẹn dữ liệu chứng từ**: Tuyệt đối không xóa cứng nhà cung cấp đã phát sinh phiếu nhập kho (`PhieuNhap`).
4. **Tính toán Công nợ & Tổng mua**:
   - `TongMua`: Tổng giá trị các phiếu nhập kho trạng thái hoàn thành (`DA_XAC_NHAN` / `DA_NHAN`).
   - `NoCanTraHienTai`: Tạm thời bằng `TongMua` khi chưa có phân hệ Sổ quỹ / Phiếu chi.
