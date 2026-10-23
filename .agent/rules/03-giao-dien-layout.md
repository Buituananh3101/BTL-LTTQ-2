# 03. Giao diện, Layout & Thành phần WinForms

## 1. Mô hình Giao diện MDI chuẩn (`frmMain`)

Hệ thống sử dụng mô hình **MDI (Multiple Document Interface)**:
- **Form cha (`frmMain`)**:
  - `IsMdiContainer = true`.
  - Phông chữ toàn hệ thống: `Segoe UI`, kích thước `9.5pt` hoặc `9pt`.
  - MenuStrip điều hướng: Gồm 5 menu chính: **Hệ thống**, **Danh mục**, **Quản lý kho**, **Báo cáo**, **Trợ giúp**.
  - StatusStrip góc dưới:
    - `tssNguoiDung`: Hiển thị họ tên và tên đăng nhập người dùng (`Người dùng: Quản trị viên (quanly)`).
    - `tssKho`: Hiển thị tên kho đang làm việc (`Kho: Kho trung tâm`).
    - `tssThoiGian`: Hiển thị ngày giờ hệ thống (`Thời gian: 21/10/2026 15:30`).

---

## 2. Tiêu chuẩn Bố cục Màn hình Danh sách & Bộ lọc

Mọi Form danh sách dữ liệu (Hàng hóa, Chứng từ, Báo cáo) thiết kế theo bố cục chuẩn 3 khối:
1. **Khối trên (Top Panel / GroupBox Tìm kiếm & Bộ lọc)**:
   - Ô nhập từ khóa `txtTimKiem` (placeholder hoặc Label "Tìm kiếm theo mã, tên...").
   - Các ComboBox lọc theo danh mục: `cboNhomHang`, `cboKho`, `cboTrangThai`.
   - Lọc theo ngày: `dtpTuNgay`, `dtpDenNgay`.
   - Nút hành động: `btnTimKiem` (icon kính lúp), `btnLamMoi` (làm mới bộ lọc).
2. **Khối giữa (Center DataGridView Bảng dữ liệu)**:
   - DataGridView hiển thị danh sách, dock `DockStyle.Fill`.
3. **Khối thanh công cụ (Bottom / Top ToolStrip hoặc Panel Thao tác)**:
   - Các nút thao tác nghiệp vụ: `btnThem` (+ Thêm mới), `btnSua` (✎ Sửa), `btnXoa` (🗑 Xóa / Ngừng bán), `btnXuatExcel` (📊 Xuất Excel), `btnDong` (✖ Đóng).

---

## 3. Chuẩn hóa Định dạng `DataGridView`

Mọi lưới `DataGridView` trong hệ thống phải được định dạng qua một hàm dùng chung hoặc thiết lập trong sự kiện `Load`:

```csharp
private void DinhDangLuoi(DataGridView dgv)
{
    dgv.AllowUserToAddRows = false;
    dgv.AllowUserToDeleteRows = false;
    dgv.ReadOnly = true;
    dgv.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
    dgv.MultiSelect = false;
    dgv.BackgroundColor = Color.White;
    dgv.RowHeadersVisible = false;
    dgv.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
    dgv.BorderStyle = BorderStyle.Fixed3D;

    // Màu xen kẽ dòng để dễ nhìn
    dgv.RowsDefaultCellStyle.BackColor = Color.White;
    dgv.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(245, 247, 250);

    // Tiêu đề cột
    dgv.EnableHeadersVisualStyles = false;
    dgv.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(235, 240, 245);
    dgv.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 9.5f, FontStyle.Bold);
    dgv.ColumnHeadersHeight = 32;

    // Căn chỉnh nội dung:
    // - Mã, Ngày tháng, Trạng thái: Căn giữa
    // - Số lượng, Đơn giá, Thành tiền: Căn phải, định dạng N0 hoặc N2
    // - Tên, Ghi chú, Mô tả: Căn trái
}
```

### Căn lề và định dạng số:
```csharp
dgv.Columns["SoLuong"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
dgv.Columns["SoLuong"].DefaultCellStyle.Format = "N0";

dgv.Columns["DonGia"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
dgv.Columns["DonGia"].DefaultCellStyle.Format = "N0";

dgv.Columns["NgayLap"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
dgv.Columns["NgayLap"].DefaultCellStyle.Format = "dd/MM/yyyy HH:mm";

dgv.Columns["MaHangNoiBo"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
```

---

## 4. Chuẩn hóa Màn hình Chứng từ Master - Detail (Nhập, Xuất, Chuyển, Kiểm)

Màn hình chứng từ nghiệp vụ gồm 2 vùng thông tin:
1. **Vùng Master (Thông tin phiếu)**:
   - Mã chứng từ (readonly, tự sinh), Ngày lập (`DateTimePicker`), Kho làm việc (`ComboBox`), Người lập (lấy từ Session), Trạng thái (hiển thị Badge/Label màu), Ghi chú (`TextBox`).
2. **Vùng Detail (Bảng dòng hàng chi tiết)**:
   - DataGridView chi tiết: `STT`, `MaHangNoiBo`, `TenHang`, `TenDvt`, `SoLuong`, `DonGia`, `ThanhTien`, `GhiChu`.
   - Panel thêm hàng nhanh: Ô nhập/chọn hàng hóa (hoặc phím tắt F3 mở danh sách chọn), ô nhập số lượng, nút `btnThemDong`, nút `btnXoaDong`.
   - Vùng tổng kết: `lblTongSoLuong`, `lblTongTien` (chữ số to, đậm, màu xanh nổi bật).
   - Nút hành động: `btnLuuTam` (Lưu phiếu tạm `NHAP`), `btnHoanThanh` (Xác nhận `DA_XAC_NHAN`), `btnHuy` (Hủy phiếu `HUY`), `btnDong`.

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

### 6.1. Quy chuẩn Phông chữ (Typography) – 100% Segoe UI
- **Phông nền tảng cấp Form (`this.Font`)**: `Segoe UI, 9.5pt, Regular`.
- **Tiêu đề lớn của Form (`lblTieuDe`, `lblTaoHangHoa`)**: `Segoe UI, 12pt, Bold`.
- **Tiêu đề phân nhóm / GroupBox / Header popup (`lblTieuDeGia`, `lblHienThiCot`)**: `Segoe UI, 9.5pt` hoặc `10pt, Bold`.
- **Nội dung / Textbox / ComboBox / Dòng DataGridView**: `Segoe UI, 9.5pt, Regular`.
- **Thanh trạng thái góc dưới (StatusStrip)**: `Segoe UI, 9pt, Regular`.

### 6.2. Bảng Màu Nhận Diện Thống Nhất (Color Palette)
- **Màu nền vùng MDI Client**: `Color.FromArgb(240, 242, 245)` (`#F0F2F5`).
- **Màu nền Form / Panel chính**: `Color.White` (`#FFFFFF`).
- **Màu nền dòng xen kẽ DataGridView**:
  - Dòng chẵn: `Color.White` (`#FFFFFF`).
  - Dòng lẻ: `Color.FromArgb(248, 250, 252)`.
  - Dòng đang chọn (Selected Row): Nền `Color.FromArgb(224, 231, 255)`, Chữ `Color.FromArgb(30, 41, 59)`.
- **Màu cảnh báo / Nguy hiểm (Hết hàng, Âm tồn, Nút xóa)**: `Color.Red` hoặc `#EF4444` / `#DC2626`.
- **Màu nút hành động chính (Primary Button)**: `btnHangHoa`, `btnLuu` (Màu xanh lá thương hiệu KiotViet `#10B981` / `#059669` hoặc phím nổi bật).
- **Màu nút phụ (Secondary Buttons)**: Xuất file, Nhập file, Hiện cột (Nền trắng, viền `#CBD5E1`, chữ tối màu).

### 6.3. Tiêu Chuẩn Trải Nghiệm & Tương Tác (UX / Behavior)
- **Đệm đôi chống nhấp nháy**: Mọi DataGridView bắt buộc kích hoạt `DoubleBuffered = true` bằng Reflection tại sự kiện `Load`.
- **Khóa chỉnh sửa trên lưới**: `ReadOnly = true`, `AllowUserToAddRows = false`, `SelectionMode = FullRowSelect`.
- **Bộ lọc bên trái (Left Filter Panel)**: Chiều rộng cố định ~360px kèm `Splitter` co giãn, ComboBox tích hợp `ComboboxHelper.CaiDatTimKiem` (gõ phím lọc tức thời, hỗ trợ không dấu, giới hạn 8 dòng scrollbar Win32).
- **Thanh phân trang đáy (Pagination)**: Dock dưới cùng, hỗ trợ tùy chọn số dòng/trang (10, 20, 50, 100), nút trước/sau và nhãn trang hiện tại.
- **Form Chi tiết Thêm/Sửa**: Khi tạo mới để trống các ComboBox chọn danh mục (`SelectedIndex = -1`, `Text = ""`) để người dùng chủ động chọn, tránh lưu nhầm mặc định; bọc toàn bộ thao tác ghi nhiều bảng trong `DataProcessing.ChangeDataTransaction`.
