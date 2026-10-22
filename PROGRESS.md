# Tiến độ dự án BTL-LTTQ-2

**Cập nhật lần cuối**: 2026-10-22 – Chuẩn hóa Header CheckBox, Xóa nút thêm nhóm ở `frmDonViCapHang` & Tinh chỉnh tỉ lệ nút thêm nhóm ở `frmNhaCungCap` (Phiên 9)

---

## Đang làm
- [x] Thiết kế và hiện thực Form Danh sách Nhà cung cấp (`frmNhaCungCap`) với 100% ComboBox bộ lọc bên trái theo mẫu KiotViet.
- [x] Thiết kế các Form phụ: Thêm/Sửa NCC (`frmDonViCapHang`), Tạo nhanh nhóm NCC (`frmNhomNhaCungCap`), Tùy chọn cột (`frmHienCotNcc`).
- [x] Khắc phục lỗi runtime `FormatException` trên cột Checkbox của `dgvNhaCungCap`.
- [x] Đồng bộ bộ lọc thời gian tạo (dual `DateTimePicker`) và kiểu nút `btnThemNhaCungCap` với `frmDanhSachHangHoa`.
- [x] Bổ sung Luật 14 & 15 vào quy chuẩn Agent (`AGENTS.md`, `.agent/rules/01-phong-cach-code.md`, `.agent/rules/03-giao-dien-layout.md`).
- [x] Chuẩn hóa nút "Nhập file" trên `frmNhaCungCap` và xóa bỏ màu tùy tiện trên nút `btnLuu` của các form NCC.
- [x] Căn chỉnh chuẩn pixel tọa độ Y các trường Email, Khu vực, Trạng thái trên `frmDonViCapHang`.
- [x] Xóa nút thêm nhóm trong `frmDonViCapHang`, mở rộng ComboBox Nhóm NCC thẳng tắp với các trường bên dưới.
- [x] Tinh chỉnh tỉ lệ nút "+" tạo nhanh nhóm trên `frmNhaCungCap` về hình vuông hoàn hảo `29x29px` căn thẳng hàng.
- [x] Chuẩn hóa Header CheckBox bằng `CheckBoxRenderer`, giải quyết triệt để lỗi Chọn tất cả / Bỏ chọn tất cả và không bỏ sót dòng cuối.
- [ ] Xây dựng Lớp quản lý phiên toàn cục `BLL/SessionContext.cs` và Tiện ích băm mật khẩu `BLL/PasswordHelper.cs` (SHA-256).
- [ ] Thiết kế và hoàn thiện Form Đăng nhập (`frmDangNhap`) hỗ trợ chọn chi nhánh kho làm việc.

---

## Việc kế tiếp
1. **Xác thực & Phân quyền**: Hoàn tất `frmDangNhap`, tích hợp phân quyền MenuStrip và StatusStrip trên `frmMain`.
2. **Quản lý Danh mục (CRUD Master Data)**:
   - Form Danh mục Nhóm hàng & Đơn vị tính (`frmNhomHang`, `frmDonViTinh`).
   - Form Danh mục Chi nhánh Kho & phân quyền `TaiKhoanKho` (`frmKho`).
   - Form Danh mục Khách hàng (`frmKhachHang`).
3. **Quản lý Kho & Chứng từ Biến động**:
   - Form Nhập hàng từ Nhà cung cấp (`frmNhapHang`) - Tự tính giá vốn MAC, cộng tồn.
   - Form Kiểm kê & Cân bằng kho (`frmKiemKho`) - Khóa vĩnh viễn phiếu sau khi cân bằng.
   - Form Chuyển kho liên chi nhánh (`frmChuyenKho`) - Luồng 2 bước Xuất chuyển / Nhận hàng.
   - Form Trả hàng nhập (`frmTraHangNhap`) & Xuất hủy (`frmXuatHuy`).
4. **Báo cáo & Thống kê**:
   - Báo cáo Tồn kho đa chi nhánh (`frmBaoCaoTonKho`).
   - Báo cáo Thẻ kho chi tiết theo mặt hàng (`frmTheKho`).
   - Tiện ích xuất dữ liệu ra file Excel.

---

## Bảng Tổng hợp Tiến độ Các Phân hệ (WBS Overview)

| STT | Phân hệ / Chức năng | Mô tả chi tiết | Trạng thái |
|:---:|---|---|:---:|
| 1 | **Khởi tạo Kiến trúc & CSDL** | Solution WinForms .NET 4.7.2, `DataProcessing.cs`, CSDL `QuanLyKhoPhuQuocLTW` | Hoàn thành 100% |
| 2 | **Quy chuẩn Vận hành AI** | Thư mục `.agent/rules/`, `AGENTS.md`, `PROGRESS.md`, `docs/` | Hoàn thành 100% |
| 3 | **Giao diện Khung MDI** | `frmMain` MDI Container, MenuStrip 5 phân hệ, StatusStrip trạng thái | Hoàn thành 100% |
| 4 | **Xác thực & Phân quyền** | `SessionContext`, SHA-256 `PasswordHelper`, `frmDangNhap`, phân quyền menu | Đang thực hiện |
| 5 | **Danh mục Hàng hóa** | `frmDanhSachHangHoa` (phân trang, lọc, tìm kiếm, xuất file), `frmHangHoa` (Thêm/Sửa) | Hoàn thành 100% |
| 6 | **Danh mục Nhà cung cấp** | `frmNhaCungCap`, `frmDonViCapHang`, `frmNhomNhaCungCap`, `frmHienCotNcc` | Hoàn thành 100% |
| 7 | **Danh mục Cơ bản khác** | CRUD Nhóm hàng, Đơn vị tính, Chi nhánh Kho, Khách hàng | Chưa bắt đầu |
| 8 | **Phiếu Nhập hàng** | `frmNhapHang`, tính giá vốn bình quân (MAC), cộng tồn dynamic | Chưa bắt đầu |
| 9 | **Kiểm kê & Cân bằng kho** | `frmKiemKho`, nạp tồn sổ sách, sinh phiếu điều chỉnh, khóa vĩnh viễn | Chưa bắt đầu |
| 10 | **Chuyển kho Liên chi nhánh** | `frmChuyenKho`, 2 bước Xuất kho ➜ Nhận kho, chặn tồn âm kho nguồn | Chưa bắt đầu |
| 11 | **Trả hàng nhập & Xuất hủy** | `frmTraHangNhap`, `frmXuatHuy`, kiểm tra tồn kho trước khi xuất | Chưa bắt đầu |
| 12 | **Báo cáo Tồn kho & Thẻ kho** | Báo cáo tồn đa kho, Thẻ kho từ `vw_BienDongKho`, xuất file Excel | Chưa bắt đầu |

---

## Nhật ký phiên (Session Logs)

- **Phiên 9 (2026-10-22) – Chuẩn hóa Header CheckBox VisualStyles, Xóa nút thêm nhóm ở `frmDonViCapHang` & Cân đối tỉ lệ nút ở `frmNhaCungCap`**:
  - **Nhu cầu**: Người dùng yêu cầu: (1) Ô chọn tất cả ở tiêu đề cột CheckBox phải có giao diện giống 100% các ô bên dưới; (2) Sửa lỗi ô chọn tất cả không hoạt động khi chưa chọn gì, và khi có dòng chọn nhấn vào thì bỏ chọn tất cả nhưng bị giữ lại dòng cuối cùng; (3) Xóa nút thêm nhóm ở trong `frmDonViCapHang`; (4) Điều chỉnh lại tỷ lệ nút thêm nhóm ở `frmNhaCungCap`.
  - **Nguyên nhân cốt lõi**:
    1. Header CheckBox trước đó dùng `ControlPaint.DrawCheckBox` (phong cách Windows 3D Classic cũ kỹ) nên khác biệt với Windows Theme Engine của các ô DataGridView bên dưới.
    2. Khi gán hàng loạt giá trị cho các cell, sự kiện `CellValueChanged` kích hoạt đệ quy gọi `CapNhatTrangThaiHeaderCheck()` khiến cờ `isHeaderChecked` bị ghi đè sai thành `false` giữa chừng; đồng thời ô `CurrentCell` (dòng vừa chọn) chưa được giải phóng (`EndEdit` + `CurrentCell = null`) khiến in-place edit buffer tự động commit đè lại giá trị chọn lên dòng đó.
    3. Nút thêm nhanh nhóm ở form chi tiết không còn cần thiết khi đã đưa nút ra form danh sách chính.
    4. Nút `btnThemNhomNCC` ở form danh sách trước đó có kích thước `32x29px` hình chữ nhật ngang hơi bè.
  - **Kết quả thực hiện**:
    1. **Đồng bộ 100% giao diện CheckBox**: Sử dụng `CheckBoxRenderer.DrawCheckBox` chuẩn Windows VisualStyles để render Header CheckBox, kích thước glyph chuẩn DPI, giao diện phẳng, viền xám tinh tế hệt như các ô checkbox bên dưới; bổ sung hiệu ứng hover khi rê chuột (`CellMouseMove` / `CellMouseLeave`).
    2. **Khắc phục triệt để lỗi Chọn tất cả**:
       - Trước khi duyệt cập nhật, gọi `dgvNhaCungCap.EndEdit()` và gán `dgvNhaCungCap.CurrentCell = null` để giải phóng hoàn toàn buffer ô đang chọn, chấm dứt hoàn toàn hiện tượng giữ lại dòng cuối cùng.
       - Thêm cờ `isUpdatingCheckAll` ngăn chặn sự kiện `CellValueChanged` can thiệp làm đảo lộn trạng thái khi đang duyệt hàng loạt.
       - Đặt `colChon.SortMode = NotSortable` đảm bảo click header chỉ kích hoạt thao tác chọn.
    3. **Xóa nút thêm nhóm ở `frmDonViCapHang`**:
       - Xóa hoàn toàn `btnThemNhomNhanh` và sự kiện `btnThemNhomNhanh_Click`.
       - Mở rộng `cboNhomNcc.Size = (225, 29)` tại `Location(504, 34)`, căn thẳng hàng tuyệt đối với `txtEmail`, `txtKhuVuc`, `cboTrangThai` (kết thúc tại X = 729).
    4. **Cân chỉnh tỉ lệ nút thêm nhóm ở `frmNhaCungCap`**:
       - Đưa `btnThemNhomNCC` về kích thước hình vuông hoàn hảo `29x29px` tại `Location(311, 66)`, căn giữa Y cùng ComboBox, mép phải thẳng tắp 340px với các ComboBox bên dưới.
       - Điều chỉnh `cboNhomNCC.Size = (287, 29)`, font nút 10.5F Bold, padding/margin = 0.
    5. **Kiểm tra biên dịch**: `CoreCompile` thành công **0 Warning(s), 0 Error(s)**.

- **Phiên 8 (2026-10-22) – Thêm nút tạo nhanh nhóm NCC và Kích hoạt cột tích chọn trên `frmNhaCungCap`**:
  - **Nhu cầu**: Người dùng yêu cầu: (1) Đưa nút "+" tạo mới nhóm nhà cung cấp ra ngoài, thu hẹp ComboBox Nhóm nhà cung cấp và để nút "+" bên cạnh; (2) Kích hoạt cột tích chọn trên DataGridView đang bị bất động.
  - **Nguyên nhân cốt lõi**:
    1. ComboBox `cboNhomNCC` chiếm trọn 322px trong bộ lọc, thiếu nút thao tác nhanh tạo nhóm nhà cung cấp.
    2. Lưới `dgvNhaCungCap` bị đặt `ReadOnly = true;` khiến cột Checkbox bị khóa cứng hoàn toàn, không thể click tick/bỏ tick. Ngoài ra thiếu cơ chế `CommitEdit` tức thời khi click chuột và chưa có Header Checkbox chọn toàn bộ dòng.
  - **Kết quả thực hiện**:
    1. Thu hẹp `cboNhomNCC` về 284px (Location `18, 66`), bổ sung nút `btnThemNhomNCC` ("+") kích thước 32x29px tại Location `(308, 66)`, mép phải thẳng hàng hoàn hảo với các ComboBox khác (340px). Nút có phong cách WinForms chuẩn (`UseVisualStyleBackColor = true`), click mở `frmNhomNhaCungCap` và tự động nạp lại danh sách nhóm, chọn ngay nhóm vừa tạo.
    2. Kích hoạt cột chọn: Đặt `dgvNhaCungCap.ReadOnly = false;`, `colChon.ReadOnly = false;`, các cột dữ liệu còn lại `ReadOnly = true;`, thiết lập `EditMode = EditOnEnter`.
    3. Bắt sự kiện `CurrentCellDirtyStateChanged` và `CellContentClick` gọi `CommitEdit(Commit)` giúp click 1 lần ăn ngay.
    4. Thêm Header CheckBox: Tự động vẽ hộp kiểm tại tiêu đề cột `colChon` (`CellPainting`), bắt sự kiện `ColumnHeaderMouseClick` để Chọn tất cả / Bỏ chọn tất cả các dòng trên trang, đồng bộ trạng thái khi click lẻ từng dòng. Bỏ qua mở form sửa khi nhấp đúp vào cột chọn.
    5. Kiểm tra biên dịch: MSBuild thành công **0 Warning(s), 0 Error(s)**.

- **Phiên 7 (2026-10-22) – Căn chỉnh hoàn hảo layout và tỉ lệ nút bấm trên Form Chi tiết Nhà cung cấp (`frmDonViCapHang`)**:
  - **Nhu cầu**: Người dùng phản hồi: "Phần email, Khu vực và trạng thái đang bi lệch, tỉ lệ nút thêm của nhóm nhà cung cấp không tương xứng với ô textbox bên cạnh."
  - **Nguyên nhân cốt lõi**:
    1. Cột bên phải bị lệch trục Y: `txtEmail` (Y=131 so với `txtDienThoai` Y=144, lệch 13px), `txtKhuVuc` (Y=183 so với `txtMaSoThue` Y=198, lệch 15px), `cboTrangThai` (Y=237 so với `txtDiaChi` Y=252, lệch 15px).
    2. Nút thêm nhanh nhóm `btnThemNhomNhanh` có kích thước to quá khổ `Size(32, 29)` và màu chữ xanh dương thủ công, gây mất cân đối với ComboBox `cboNhomNcc` bên cạnh.
  - **Kết quả thực hiện**:
    1. Căn chuẩn 100% tọa độ Y theo từng dòng:
       - Dòng 3: `txtDienThoai` và `txtEmail` cùng ở Y = 144 (Label Y = 147).
       - Dòng 4: `txtMaSoThue` và `txtKhuVuc` cùng ở Y = 198 (Label Y = 201).
       - Dòng 5: `txtDiaChi` và `cboTrangThai` cùng ở Y = 252 (Label Y = 255).
       - Dòng 6: `txtGhiChu` ở Y = 306 (Label Y = 309), bảo đảm nhịp cách dòng 54px hoàn hảo.
    2. Tinh chỉnh tỉ lệ `cboNhomNcc` và `btnThemNhomNhanh`:
       - Tăng chiều rộng `cboNhomNcc` lên 194px để hiển thị tên nhóm trọn vẹn.
       - Tinh chỉnh `btnThemNhomNhanh` về kích thước nhỏ gọn `Size(27, 26)`, nằm thẳng hàng ở `Location(702, 34)` với lề phải kết thúc đúng tại X = 729 (thẳng tắp với lề phải của `txtEmail`, `txtKhuVuc`, `cboTrangThai` và `txtTenDonVi`).
       - Bỏ màu xanh thủ công `ForeColor` trên `btnThemNhomNhanh`, tuân thủ triệt để Luật 15.
    3. Kiểm tra biên dịch: **0 Warning(s), 0 Error(s)**.

- **Phiên 6 (2026-10-22) – Bổ sung 2 Luật Cốt Lõi (Hiển thị Tiếng Việt 100% & Không Tự Ý Thêm Màu Nút) và Chuẩn hóa Giao diện Nút bấm**:
  - **Nhu cầu**: Người dùng yêu cầu thêm 2 rules vào agent:
    1. Tất cả các nút, chức năng phải được đặt hiển thị là tiếng Việt, ví dụ chức năng "Nhập file" trong `frmNhaCungCap` không được để là "Import file" mà phải để là "Nhập file".
    2. Các nút và chức năng không được tự ý thêm màu.
  - **Kết quả thực hiện**:
    1. **Bổ sung 2 Luật cốt lõi vào hệ thống Agent**:
       - Cập nhật [`AGENTS.md`](file:///d:/Code/C%23/LapTrinhTrucQuan/BTL/BTA_BTL2/BTL-LTTQ-2/AGENTS.md) thêm **Luật 14** (Hiển thị tiếng Việt 100% cho mọi nút bấm và chức năng) và **Luật 15** (Không tự ý thêm màu cho các nút và chức năng).
       - Cập nhật [`.agent/rules/01-phong-cach-code.md`](file:///d:/Code/C%23/LapTrinhTrucQuan/BTL/BTA_BTL2/BTL-LTTQ-2/.agent/rules/01-phong-cach-code.md) (Mục 1.3 - Tiêu chuẩn Ngôn ngữ & Màu sắc Nút bấm / Chức năng).
       - Cập nhật [`.agent/rules/03-giao-dien-layout.md`](file:///d:/Code/C%23/LapTrinhTrucQuan/BTL/BTA_BTL2/BTL-LTTQ-2/.agent/rules/03-giao-dien-layout.md) (Mục 6 - Tiêu chuẩn Nút bấm & Thành phần Thao tác).
    2. **Khắc phục triệt để các vi phạm hiện hữu trong mã nguồn**:
       - Sửa nút `btnNhapFile.Text = "Nhập file"` (thay cho `"Import file"`) trên [`frmNhaCungCap.Designer.cs`](file:///d:/Code/C%23/LapTrinhTrucQuan/BTL/BTA_BTL2/BTL-LTTQ-2/BTL-LTTQ-2/GUI/Forms/NhaCungCap/frmNhaCungCap.Designer.cs).
       - Gỡ bỏ thuộc tính tô màu xanh thủ công `BackColor`, `ForeColor`, `FlatStyle` trên nút `btnLuu` của [`frmDonViCapHang.Designer.cs`](file:///d:/Code/C%23/LapTrinhTrucQuan/BTL/BTA_BTL2/BTL-LTTQ-2/BTL-LTTQ-2/GUI/Forms/NhaCungCap/frmDonViCapHang.Designer.cs) và [`frmNhomNhaCungCap.Designer.cs`](file:///d:/Code/C%23/LapTrinhTrucQuan/BTL/BTA_BTL2/BTL-LTTQ-2/BTL-LTTQ-2/GUI/Forms/NhaCungCap/frmNhomNhaCungCap.Designer.cs), đưa về phong cách WinForms chuẩn (`UseVisualStyleBackColor = true`).
    3. **Kiểm tra biên dịch**:
       - Biên dịch bằng MSBuild thành công: **0 Warning(s), 0 Error(s)**.

- **Phiên 5 (2026-10-22) – Khắc phục lỗi DataGridView, Đồng bộ Bộ lọc thời gian & Tinh chỉnh Nút bấm `frmNhaCungCap`**:
  - **Nhu cầu**: Sửa lỗi runtime `DataGridView Default Error Dialog: System.FormatException: Formatted value of the cell has a wrong type`. Xóa nút link "+ Tạo mới" nhóm nhanh ở filter. Chuyển nút "+ Nhà cung cấp" và phần lọc thời gian tạo đồng bộ giống `frmDanhSachHangHoa` (GroupBox chứa dual DateTimePicker). Bỏ nút "Thiết lập lại bộ lọc".
  - **Kết quả thực hiện**:
    1. **Khắc phục lỗi `FormatException`**:
       - Cấu hình `colChon` (`DataGridViewCheckBoxColumn`) với `DataPropertyName = "Chon"`, `TrueValue = true`, `FalseValue = false`, `DefaultCellStyle.NullValue = false`.
       - Bổ sung `CAST(0 AS BIT) AS Chon` trong câu truy vấn CTE và `SELECT` của `frmNhaCungCap.cs`.
       - Bổ sung mã fallback đảm bảo DataTable luôn chứa cột `"Chon"` không `DBNull`.
       - Gắn sự kiện `dgvNhaCungCap.DataError` chặn triệt để dialog lỗi mặc định (`e.Cancel = true; e.ThrowException = false;`).
       - Xử lý an toàn trong `dgvNhaCungCap_CellFormatting` cho `colChon`.
    2. **Xóa link nhanh & Nút thiết lập lại bộ lọc**:
       - Đã loại bỏ hoàn toàn link `btnTaoNhomMoi` ("+ Tạo mới") và nút `btnDatLaiBoLoc` ("Thiết lập lại bộ lọc") khỏi giao diện `frmNhaCungCap.Designer.cs` và code-behind `frmNhaCungCap.cs`.
    3. **Đồng bộ phần chọn thời gian với `frmDanhSachHangHoa`**:
       - Thêm `grpThoiGianTao` chứa `dtpTuNgay`, `dtpDenNgay`, `lblTuNgay`, `lblDenNgay` đồng bộ giao diện và kích thước.
       - Mặc định ngày: Từ 01/01 năm hiện tại đến ngày hiện tại (`DateTime.Today`).
       - Tích hợp điều kiện lọc SQL Server: `c.NgayTao >= @TuNgay AND c.NgayTao < @DenNgay`.
    4. **Chuẩn hóa nút thêm mới**:
       - Nút `btnThemNhaCungCap` ("+ Nhà cung cấp") chuyển sang phong cách WinForms chuẩn (`UseVisualStyleBackColor = true`), kích thước và vị trí Anchor đồng bộ với nút `+ Hàng hóa`.
    5. **Kiểm tra biên dịch**:
       - Chạy MSBuild build thành công: **0 Warning(s), 0 Error(s)**.
    6. **Đồng bộ tài liệu**:
       - Cập nhật [`docs/specs/nha-cung-cap.md`](file:///d:/Code/C%23/LapTrinhTrucQuan/BTL/BTA_BTL2/BTL-LTTQ-2/docs/specs/nha-cung-cap.md) và [`PROGRESS.md`](file:///d:/Code/C%23/LapTrinhTrucQuan/BTL/BTA_BTL2/BTL-LTTQ-2/PROGRESS.md).

- **Phiên 4 (2026-10-22) – Thiết kế và Hoàn thiện Toàn bộ Cụm Form Quản lý Nhà cung cấp (`frmNhaCungCap`)**:
  - **Nhu cầu**: Thiết kế `frmNhaCungCap` và các form con liên quan. Sử dụng cấu trúc thư mục chứa form như `DanhSachHangHoa` để thiết kế thêm các form phụ như thêm nhà cung cấp. Bố cục giống ảnh KiotViet gửi kèm, đồng bộ phong cách WinForms với `frmDanhSachHangHoa` và toàn bộ các filter bên tay trái là dạng ComboBox.
  - **Kết quả thực hiện**:
    1. Tổ chức thư mục chuẩn mực: `BTL-LTTQ-2/GUI/Forms/NhaCungCap/`.
    2. Hoàn thiện [`frmNhaCungCap`](file:///d:/Code/C%23/LapTrinhTrucQuan/BTL/BTA_BTL2/BTL-LTTQ-2/BTL-LTTQ-2/GUI/Forms/NhaCungCap/frmNhaCungCap.cs):
       - Chuyển đổi 100% các bộ lọc bên trái thành ComboBox: Nhóm nhà cung cấp, Mức tổng mua, Thời gian mua, Mức nợ hiện tại, Trạng thái hoạt động, nút "Thiết lập lại bộ lọc".
       - DataGridView chuẩn mực: đệm đôi chống giật lag, màu dòng xen kẽ `#F8FAFC`, cột checkbox, mã NCC, tên NCC, điện thoại, email, nợ cần trả hiện tại (đỏ đậm khi > 0), tổng mua (`N0`), trạng thái.
       - Tìm kiếm tức thời theo mã, tên, SĐT; phân trang SQL Server `OFFSET ... ROWS FETCH NEXT ... ROWS ONLY` (10, 15, 20, 50, 100 dòng); xuất file CSV UTF-8 BOM đọc tốt trên Excel.
       - Double-click vào dòng để mở form chỉnh sửa thông tin nhà cung cấp.
    3. Hoàn thiện [`frmDonViCapHang`](file:///d:/Code/C%23/LapTrinhTrucQuan/BTL/BTA_BTL2/BTL-LTTQ-2/BTL-LTTQ-2/GUI/Forms/NhaCungCap/frmDonViCapHang.cs): Form phụ Thêm/Sửa NCC với gợi ý mã tự động `NCC0000X`, validate chống trùng mã, nạp nhóm NCC, nhập SĐT/Email/MST/Địa chỉ/Ghi chú và thống kê công nợ; bọc Transaction an toàn.
    4. Hoàn thiện [`frmNhomNhaCungCap`](file:///d:/Code/C%23/LapTrinhTrucQuan/BTL/BTA_BTL2/BTL-LTTQ-2/BTL-LTTQ-2/GUI/Forms/NhaCungCap/frmNhomNhaCungCap.cs): Form popup tạo nhanh nhóm NCC khi click link "+ Tạo mới" trên bộ lọc hoặc nút "+" trong form thêm NCC.
    5. Hoàn thiện [`frmHienCotNcc`](file:///d:/Code/C%23/LapTrinhTrucQuan/BTL/BTA_BTL2/BTL-LTTQ-2/BTL-LTTQ-2/GUI/Forms/NhaCungCap/frmHienCotNcc.cs): Form popup tùy chọn ẩn/hiện cột xuất hiện ngay dưới nút "Hiện cột".
    6. Tích hợp form vào menu MDI: Cập nhật `frmMain.cs` gắn sự kiện `mnuNhaCungCap_Click` mở form con trong MDI Container (kèm cơ chế `Activate()` nếu form đã mở).
    7. Cập nhật `docs/database_update.sql`: Bổ sung script an toàn "chỉ cộng không phá" cho bảng `NhomNhaCungCap` và các cột mở rộng của `DonViCapHang`.
    8. Đồng bộ đặc tả chức năng: Cập nhật hoàn thiện [`docs/specs/nha-cung-cap.md`](file:///d:/Code/C%23/LapTrinhTrucQuan/BTL/BTA_BTL2/BTL-LTTQ-2/docs/specs/nha-cung-cap.md).
    9. Kiểm tra biên dịch Rebuild toàn bộ solution: **0 Warning(s), 0 Error(s)**.

- **Phiên 3 (2026-10-22) – Hoàn thiện Toàn bộ Logic Danh mục Hàng hóa (`frmDanhSachHangHoa` & `frmHangHoa`)**:
  - **Nhu cầu**: Hiện thực hóa logic nghiệp vụ nạp dữ liệu, tìm kiếm tức thì, lọc đa chiều, phân trang SQL Server và thêm/sửa hàng hóa bám sát 100% giao diện đã thiết kế.
  - **Kết quả thực hiện**:
    1. Tạo [`BLL/SessionContext.cs`](file:///d:/CODE/BTL-LTTQ-2/BTL-LTTQ-2/BLL/SessionContext.cs): Quản trị phiên kho hiện tại (`MaKhoHienTai = 1`), phục vụ tính tồn kho động chính xác qua `vw_TonKhoHienTai`.
    2. Hoàn thiện [`frmDanhSachHangHoa`](file:///d:/CODE/BTL-LTTQ-2/BTL-LTTQ-2/GUI/Forms/DanhSachHangHoa/frmDanhSachHangHoa.cs):
       - Tích hợp thanh phân trang SQL `OFFSET ... ROWS FETCH NEXT ... ROWS ONLY` (tùy chọn 10, 20, 50, 100 dòng/trang).
       - Nạp dữ liệu vào 5 ComboBox bộ lọc: Nhóm hàng, Tiêu chí tồn kho, Nhà cung cấp, Vị trí, Trạng thái kinh doanh, Lọc theo khoảng ngày tạo.
       - Cấu hình DataGridView hiển thị đẹp, đệm đôi chống giật lag, màu dòng xen kẽ, cảnh báo đỏ khi hết hàng / âm tồn.
       - Tìm kiếm tức thời theo Mã hàng, Tên hàng, Quy cách.
       - Xuất danh sách đang hiển thị ra file CSV hỗ trợ tiếng Việt UTF-8 BOM mở bằng Excel.
       - Click đúp vào dòng để mở form chỉnh sửa thông tin hàng hóa.
    3. Hoàn thiện [`frmHangHoa`](file:///d:/CODE/BTL-LTTQ-2/BTL-LTTQ-2/GUI/Forms/DanhSachHangHoa/frmHangHoa.cs):
       - Hỗ trợ 2 chế độ: Thêm mới (`maHang = 0`) và Cập nhật (`maHang > 0`).
       - Tự động gợi ý mã hàng khi thêm mới; kiểm tra chống trùng mã hàng nội bộ.
       - Nạp danh mục nhóm hàng, đơn vị tính, vị trí, ảnh đại diện (`HinhAnh`), định mức tồn kho cao/thấp nhất (`NguongTonKho`).
       - Lưu dữ liệu an toàn bọc trong `DataProcessing.ChangeDataTransaction`.
    4. Kiểm tra biên dịch Rebuild toàn bộ solution: **0 Warning(s), 0 Error(s)**.

- **Phiên 2 (2026-10-21) – Khắc phục Hiện tượng Giao diện Vỡ nét (High DPI Scaling & Crisp UI)**:
  - **Nhu cầu**: Người dùng phản hồi: "hiện tại khi chạy nhìn nó cứ vỡ vỡ, bạn hãy khác phục điều này giúp tôi".
  - **Nguyên nhân cốt lõi**:
    1. Thiếu cấu hình High DPI Awareness trong .NET Framework 4.7.2: Windows áp dụng cơ chế Bitmap Virtualization (DPI virtualization) co giãn thô, làm phông chữ và đường viền bị nhòe/vỡ hạt trên màn hình có tỉ lệ scale 125%, 150%, 175%.
    2. MenuStrip và StatusStrip sử dụng renderer gradient cũ tạo vệt mờ.
    3. `FormMain` và các Form con (`frmDanhSachHangHoa`) lệch thông số `AutoScaleDimensions` và thiếu khai báo Font đồng bộ.
  - **Giải pháp & Kết quả thực hiện**:
    1. Tạo file manifest [`app.manifest`](file:///d:/CODE/BTL-LTTQ-2/BTL-LTTQ-2/app.manifest): Kích hoạt `PerMonitorV2` DPI awareness, liên kết `Common-Controls 6.0` và khai báo tương thích Windows 10/11.
    2. Cập nhật [`App.config`](file:///d:/CODE/BTL-LTTQ-2/BTL-LTTQ-2/App.config): Bật `EnableWindowsFormsHighDpiAutoResizing` và `DpiAwareness = PerMonitorV2`.
    3. Cập nhật [`Program.cs`](file:///d:/CODE/BTL-LTTQ-2/BTL-LTTQ-2/Program.cs): Khai báo và gọi API Win32 `SetProcessDpiAwarenessContext` chế độ `PerMonitorV2` trước khi khởi tạo UI.
    4. Tinh chỉnh giao diện: Thiết lập `RenderMode = ToolStripRenderMode.System`, đồng bộ màu nền MDI Client sang màu sáng hiện đại (`#F0F2F5`), chuẩn hóa `AutoScaleDimensions` trên `frmDanhSachHangHoa`.
    5. Biên dịch và kiểm thử thực thi thành công: `BTL-LTTQ-2.exe` hiển thị chữ và đường nét cực kỳ sắc nét trên mọi độ phân giải.

- **Phiên 1 (2026-10-21) – Khởi tạo Khung Quy chuẩn Vận hành & Tài liệu Dự án**:
  - **Nhu cầu**: Dựa trên `BTL-LTW-QuanLiKho-2`, tạo thư mục `.agent`, file theo dõi tiến độ `PROGRESS.md`, và thư mục tài liệu `docs/` cho dự án `BTL-LTTQ-2`.
  - **Giải pháp & Kết quả thực hiện**:
    1. Nghiên cứu cấu trúc và kế thừa toàn bộ tri thức kiến trúc, nghiệp vụ kho từ `BTL-LTW-QuanLiKho-2`.
    2. Tạo thư mục `.agent/rules/` gồm 5 bộ luật cốt lõi đã điều chỉnh phù hợp với WinForms và ADO.NET:
       - `01-phong-cach-code.md`: Quy ước lập trình WinForms, đặt tên controls, phân tầng DAL/BLL/GUI.
       - `02-nghiep-vu-kho.md`: Nghiệp vụ kho, tính tồn động qua `vw_TonKhoHienTai`, trạng thái chứng từ, luật bất biến khóa phiếu kiểm kho đã cân bằng.
       - `03-giao-dien-layout.md`: Tiêu chuẩn giao diện MDI, DataGridView chuẩn mực, Master-Detail, MessageBox.
       - `04-xac-thuc-phan-quyen.md`: Đăng nhập SHA-256, lớp toàn cục `SessionContext`, phân quyền vai trò và phân quyền kho `TaiKhoanKho`.
       - `05-co-so-du-lieu.md`: Thao tác ADO.NET thuần với `DataProcessing.cs`, phòng chống 100% SQL Injection, 3 nguyên tắc vàng CSDL.
    3. Tạo file chỉ dẫn trung tâm `AGENTS.md` tại thư mục gốc với 12 Luật cốt lõi và quy trình đầu/cuối phiên.
    4. Thiết lập thư mục `docs/`:
       - `docs/quyet-dinh.md`: Sổ ghi nhận quyết định kiến trúc và công nghệ.
       - `docs/van-de-can-xu-ly.md`: Backlog 18 đầu việc phân chia theo 4 nhóm.
       - `docs/so-do-er-va-rm-csdl.md`: Mô hình hóa ERD & Relational Model CSDL `QuanLyKhoPhuQuocLTW`.
       - `docs/so-do-activity-nghiep-vu.md`: Lưu đồ Activity nghiệp vụ Nhập, Kiểm, Chuyển, Trả, Xuất.
       - `docs/so-do-use-case.md`: Sơ đồ Use Case phân quyền các vai trò.
       - Thư mục `docs/specs/`: 13 file đặc tả kỹ thuật và mẫu đặc tả chức năng.
       - Sao chép các script SQL `database_update.sql`, `DuLieuMau.sql`, `fix_mojibake.sql`.
    5. Tạo file `PROGRESS.md` theo dõi tiến độ và lộ trình phát triển chi tiết.
