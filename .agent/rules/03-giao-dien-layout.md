# 03. Giao diện, Layout & Thành phần WinForms

## 1. Mô hình Giao diện MDI chuẩn (`frmMain`)

Hệ thống sử dụng mô hình **MDI (Multiple Document Interface)**:
- **Form cha (`frmMain`)**:
  - `IsMdiContainer = true`.
  - Phông chữ toàn hệ thống: `Segoe UI`, kích thước `9.5pt` (`new System.Drawing.Font("Segoe UI", 9.5F)`).
  - MenuStrip điều hướng: Gồm các menu chính: **Hệ thống**, **Kho hàng**, **Hàng hóa**, **Mua hàng**, **Báo cáo**, **Trợ giúp**.
  - StatusStrip góc dưới:
    - `tssNguoiDung`: Hiển thị họ tên và tên đăng nhập người dùng (`Người dùng: Quản trị viên (quanly)`).
    - `tssKho`: Hiển thị tên kho đang làm việc (`Kho: Kho trung tâm`).
    - `tssThoiGian`: Hiển thị ngày giờ hệ thống (`Thời gian: 21/10/2026 15:30`).

---

## 2. Tiêu chuẩn Bố cục Màn hình Danh sách & Bộ lọc

Mọi Form danh sách dữ liệu (Hàng hóa, Chứng từ, Báo cáo) thiết kế theo bố cục chuẩn 3 khối:
1. **Khối trên (Top Panel / GroupBox Tìm kiếm & Bộ lọc)**:
   - Ô nhập từ khóa `txtTimKiem` (placeholder hoặc Label "Tìm kiếm").
   - Nút hành động: `btnThem` (+ Hàng hóa / + Kiểm kho / + Phiếu nhập), `btnNhapFile` (Nhập file), `btnXuatFile` (Xuất file), `btnHienCot` (Hiện cột), `btnHuongDanSuDung` (?).
2. **Khối bộ lọc bên trái (Left Filter Panel `pnlBoLoc` & `grpBoLoc`)**:
   - Chiều rộng chuẩn: 348px - 360px kèm `Splitter` co giãn.
   - Tuân thủ 100% ComboBox theo quy chuẩn Điều luật 16.
3. **Khối giữa (Center DataGridView Bảng dữ liệu)**:
   - DataGridView hiển thị danh sách, dock `DockStyle.Fill`.
4. **Khối phân trang đáy (Bottom Pagination Panel `pnlPhanTrang`)**:
   - `lblTongSo`: Hiển thị tổng số bản ghi (`Tổng số: X hàng hóa / phiếu`).
   - `cboSoDongMoiTrang`: Tùy chọn 10, 15, 20, 50, 100 dòng.
   - `btnTrangTruoc` (<), `lblTrangHienTai` (Trang 1 / 1), `btnTrangSau` (>).

---

## 3. Chuẩn hóa Định dạng `DataGridView`

Mọi lưới `DataGridView` trong hệ thống phải được định dạng qua một hàm dùng chung hoặc thiết lập trong sự kiện `Load`:

```csharp
private void DinhDangLuoi(DataGridView dgv)
{
    // Bật DoubleBuffered chống nhấp nháy
    typeof(DataGridView).InvokeMember("DoubleBuffered",
        System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.SetProperty,
        null, dgv, new object[] { true });

    dgv.AllowUserToAddRows = false;
    dgv.AllowUserToDeleteRows = false;
    dgv.ReadOnly = true;
    dgv.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
    dgv.MultiSelect = false;
    dgv.BackgroundColor = Color.White;
    dgv.RowHeadersVisible = false;
    dgv.BorderStyle = BorderStyle.Fixed3D;

    // Màu xen kẽ dòng để dễ nhìn
    dgv.RowsDefaultCellStyle.BackColor = Color.White;
    dgv.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(248, 250, 252);
    dgv.DefaultCellStyle.SelectionBackColor = Color.FromArgb(224, 231, 255);
    dgv.DefaultCellStyle.SelectionForeColor = Color.FromArgb(15, 23, 42);

    // Tiêu đề cột
    dgv.EnableHeadersVisualStyles = false;
    dgv.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(235, 240, 245);
    dgv.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 9.5f, FontStyle.Bold);
    dgv.ColumnHeadersHeight = 32;

    // Căn chỉnh nội dung:
    // - Mã, Ngày tháng, Trạng thái: Căn giữa (MiddleCenter)
    // - Số lượng, Đơn giá, Thành tiền: Căn phải (MiddleRight), định dạng N0 hoặc N2
    // - Tên, Ghi chú, Mô tả: Căn trái (MiddleLeft)
}
```

---

## 4. Chuẩn hóa Màn hình Chứng từ Master - Detail (Nhập, Xuất, Chuyển, Kiểm)

Màn hình chứng từ nghiệp vụ gồm 2 vùng thông tin:
1. **Vùng Master (Thông tin phiếu)**:
   - Mã chứng từ (readonly, tự sinh), Ngày lập / chốt (`DateTimePicker`), Kho làm việc (`ComboBox`), Người lập (lấy từ Session), Trạng thái (hiển thị Badge/Label màu), Ghi chú (`TextBox`).
2. **Vùng Detail (Bảng dòng hàng chi tiết)**:
   - DataGridView chi tiết: `STT`, `MaHangNoiBo`, `TenHang`, `TenDvt`, `SoLuong`, `DonGia`, `ThanhTien`, `GhiChu`.
   - Panel thêm hàng nhanh: Ô chọn hàng hóa `cboHangHoa`, ô nhập số lượng `nudSoLuong`, nút `btnThemDong`, nút nạp tất cả.
   - Vùng tổng kết: `lblTongSoLuong`, `lblTongTien` / `lblTongChenhLech` (chữ số to, đậm, rõ ràng).
   - Nút hành động: `btnLuuTam` (Lưu phiếu tạm `NHAP`), `btnCanBangKho` / `btnHoanThanh` (Xác nhận), `btnXoa` / `btnHuy`, `btnDong`.

---

## 5. Chuẩn hóa Hộp thoại Thông báo (`MessageBox`)

Mọi thông báo giao tiếp với người dùng phải tuân theo 4 cấp độ:

1. **Xác nhận hành động (Hỏi người dùng trước khi làm)**:
   ```csharp
   DialogResult dr = MessageBox.Show(
       "Bạn có chắc chắn muốn cân bằng kho? Hành động này sẽ khóa vĩnh viễn phiếu kiểm kê!",
       "Xác nhận cân bằng",
       MessageBoxButtons.YesNo,
       MessageBoxIcon.Question);
   ```
2. **Thông báo thành công**:
   ```csharp
   MessageBox.Show("Lưu chứng từ thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
   ```
3. **Cảnh báo dữ liệu (Thiếu trường, vi phạm nghiệp vụ)**:
   ```csharp
   MessageBox.Show("Số lượng xuất vượt quá số lượng tồn hiện có trong kho!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
   ```
4. **Thông báo lỗi hệ thống**:
   ```csharp
   MessageBox.Show("Đã xảy ra lỗi khi kết nối CSDL: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
   ```

---

## 6. Bộ Quy Chuẩn Thiết Kế Giao Diện Mẫu (Khuôn mẫu Chốt từ Danh mục Hàng hóa)

Phân hệ **Danh mục Hàng hóa** (`frmDanhSachHangHoa` & `frmHangHoa`) đã được chốt làm **Khuôn mẫu chuẩn (Design System)** cho toàn bộ các màn hình phát triển tiếp theo (Nhập kho, Kiểm kê, Chuyển kho, Danh mục Nhà cung cấp, Khách hàng, Báo cáo):

### 6.1. Quy chuẩn Phông chữ (Typography) – Lấy `frmDanhSachHangHoa` làm chuẩn duy nhất (100% Segoe UI)
- **Quy tắc bắt buộc (Điều luật 18)**: Toàn bộ Form, Dialog, UserControl trong ứng dụng bắt buộc sử dụng bộ font chuẩn của `frmDanhSachHangHoa`. Tuyệt đối không tự ý thay đổi font family hoặc font size khi chưa có yêu cầu từ người dùng.
- **Phông nền tảng cấp Form (`this.Font`)**: `Segoe UI, 9.5pt, Regular` (`new System.Drawing.Font("Segoe UI", 9.5F)`).
- **Tiêu đề lớn của Form (`lblTieuDe`, `lblTaoHangHoa`)**: `Segoe UI, 12pt, Bold` (`new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold)`).
- **Tiêu đề phân nhóm / GroupBox / Header popup (`lblTieuDeGia`, `lblHienThiCot`)**: `Segoe UI, 9.5pt` hoặc `10pt, Bold`.
- **Nội dung / Textbox / ComboBox / Dòng DataGridView**: `Segoe UI, 9.5pt, Regular`.
- **Thanh trạng thái góc dưới (StatusStrip)**: `Segoe UI, 9pt, Regular` hoặc `9.5pt`.
- **Khai báo chuỗi tiếng Việt & Encoding file**:
  - Mọi câu lệnh SQL chứa ký tự tiếng Việt có dấu (ví dụ: `N'['`, `N' (Tồn: '`, `N'Phiếu tạm'`) BẮT BUỘC phải có tiền tố `N` phía trước dấu nháy đơn (`N'...'`) để tránh SQL Server chuyển đổi sang ANSI biến thành dấu hỏi `?`.
  - Mọi file mã nguồn C# (`.cs`, `.Designer.cs`) chứa chuỗi tiếng Việt BẮT BUỘC phải lưu dưới định dạng **UTF-8 with BOM** (Preamble `0xEF, 0xBB, 0xBF`) để trình biên dịch MSBuild/Roslyn không bị lỗi mã hóa ký tự Unicode.

### 6.2. Bảng Màu Nhận Diện Thống Nhất (Color Palette)
- **Màu nền vùng MDI Client**: `Color.FromArgb(240, 242, 245)` (`#F0F2F5`).
- **Màu nền Form / Panel chính**: `Color.White` (`#FFFFFF`).
- **Màu nền dòng xen kẽ DataGridView**:
  - Dòng chẵn: `Color.White` (`#FFFFFF`).
  - Dòng lẻ: `Color.FromArgb(248, 250, 252)`.
  - Dòng đang chọn (Selected Row): Nền `Color.FromArgb(224, 231, 255)`, Chữ `Color.FromArgb(30, 41, 59)`.
- **Màu cảnh báo / Nguy hiểm (Hết hàng, Âm tồn, Nút xóa)**: `Color.Red` hoặc `#EF4444` / `#DC2626`.
- **Màu nút hành động chính (Primary Button)**: Tuân thủ WinForms chuẩn (`UseVisualStyleBackColor = true`), không tự ý đổi màu nền trừ khi có yêu cầu.

### 6.3. Tiêu Chuẩn Trải Nghiệm & Tương Tác (UX / Behavior)
- **Đệm đôi chống nhấp nháy**: Mọi DataGridView bắt buộc kích hoạt `DoubleBuffered = true` bằng Reflection tại sự kiện `Load`.
- **Khóa chỉnh sửa trên lưới danh sách**: `ReadOnly = true`, `AllowUserToAddRows = false`, `SelectionMode = FullRowSelect`.
- **Kích thước Form MDI Child**: Form danh sách phải có `ClientSize = (1782, 853)` và khi mở từ `frmMain` bắt buộc gán `frm.WindowState = FormWindowState.Maximized;` để hiển thị tràn màn hình MDI cha, tránh cửa sổ con lơ lửng bị tràn cuộn ngang.
- **Thanh phân trang đáy (Pagination)**: Dock dưới cùng, hỗ trợ tùy chọn số dòng/trang (10, 15, 20, 50, 100), nút trước/sau và nhãn trang hiện tại.

### 6.4. Quy Chuẩn Bộ Lọc GroupBox Bên Trái (Left Filter GroupBox) – Đồng bộ với `frmDanhSachHangHoa`
- **Thống nhất 100% sử dụng ComboBox**:
  - Toàn bộ các tiêu chí lọc trong GroupBox bên trái (`grpBoLoc`) đều phải là `ComboBox`, **tuyệt đối không dùng CheckBox rời rạc**.
  - Ví dụ: Trạng thái chứng từ/hàng hóa phải là ComboBox `cboTrangThai` với các giá trị: `-- Tất cả trạng thái --`, `Đã cân bằng`, `Phiếu tạm`, `Đã hủy`... (chọn 1 trạng thái rõ ràng, có thể định nghĩa dạng `DropDownList` hoặc `DropDown`).
- **Tìm kiếm trực tiếp bằng text nhập vào với danh mục nhiều giá trị**:
  - Đối với các trường danh mục nhiều giá trị (Nhà cung cấp, Nhóm hàng, Vị trí, Người tạo, Người trả/nhập...): bắt buộc tích hợp qua `ComboboxHelper.CaiDatTimKiem(cbo, dt, displayCol, valueCol, onCommitted)`.
  - Cho phép người dùng gõ text tìm kiếm không phân biệt hoa thường, không dấu/có dấu, tự động giới hạn độ cao tối đa 8 dòng dropdown và tự động chọn phần tử đầu tiên khi gõ Enter.
- **Ngoại trừ duy nhất bộ lọc khoảng thời gian**:
  - Không dùng ComboBox cho khoảng thời gian mà bắt buộc sử dụng **cụm GroupBox con `grpThoiGianTao`** làm chuẩn duy nhất theo mẫu `frmDanhSachHangHoa`:
    - GroupBox: `grpThoiGianTao`, kích thước `322x100`, nằm trong `grpBoLoc`.
    - Gồm 2 DateTimePicker: `dtpTuNgay` (Từ ngày) và `dtpDenNgay` (Đến ngày) đều cấu hình `Format = Short` (`dd/MM/yyyy`).
    - Nhãn chỉ dẫn: `lblTuNgay` (Từ), `lblDenNgay` (Đến).
    - Sự kiện `ValueChanged` tự động làm mới danh sách bảng dữ liệu về trang 1.
