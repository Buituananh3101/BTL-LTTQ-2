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
- **Quy tắc bắt buộc (Điều luật 18)**: Toàn bộ Form, Dialog, UserControl trong ứng dụng bắt buộc sử dụng bộ font chuẩn và màu chữ của `frmDanhSachHangHoa`. Mọi form phải tuân thủ tuyệt đối, không được làm khác trừ khi được yêu cầu.
- **Phông nền tảng cấp Form (`this.Font`)**: `Segoe UI, 9.5pt, Regular` (`new System.Drawing.Font("Segoe UI", 9.5F)`).
- **Tiêu đề lớn của Form (`lblTieuDe`, `lblTaoHangHoa`)**: `Segoe UI, 12pt, Bold` (`new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold)`).
- **Tiêu đề phân nhóm / GroupBox**: `Segoe UI, 9.5pt, Regular` (hoặc tiêu đề nhóm danh mục lớn giữ chuẩn WinForms).
- **Tiêu đề cột DataGridView**: `Segoe UI, 9.5pt, Bold`.
- **Nội dung / Textbox / ComboBox / Dòng DataGridView / Toàn bộ nhãn thông tin và nhãn thống kê**: Bắt buộc dùng `Segoe UI, 9.5pt, Regular`.
  + **Nghiêm cấm tự ý in đậm (`FontStyle.Bold`)**: Không được tự ý in đậm các trường dữ liệu, số tiền, nhãn thống kê như Tổng tiền hàng, NCC cần trả, Giảm giá, NCC đã trả, Tổng số lượng, Đơn giá... Toàn bộ phải để chữ thường `Regular`.
- **Thanh trạng thái góc dưới (StatusStrip)**: `Segoe UI, 9pt, Regular` hoặc `9.5pt`.
- **Khai báo chuỗi tiếng Việt & Encoding file**:
  - Mọi câu lệnh SQL chứa ký tự tiếng Việt có dấu (ví dụ: `N'['`, `N' (Tồn: '`, `N'Phiếu tạm'`) BẮT BUỘC phải có tiền tố `N` phía trước dấu nháy đơn (`N'...'`) để tránh SQL Server chuyển đổi sang ANSI biến thành dấu hỏi `?`.
  - Mọi file mã nguồn C# (`.cs`, `.Designer.cs`) chứa chuỗi tiếng Việt BẮT BUỘC phải lưu dưới định dạng **UTF-8 with BOM** (Preamble `0xEF, 0xBB, 0xBF`) để trình biên dịch MSBuild/Roslyn không bị lỗi mã hóa ký tự Unicode.

### 6.2. Bảng Màu Nhận Diện & Nút Bấm Thống Nhất (Color Palette & Controls)
- **Màu nền vùng MDI Client**: `Color.FromArgb(240, 242, 245)` (`#F0F2F5`).
- **Màu nền Form / Panel chính**: `Color.White` (`#FFFFFF`).
- **Màu nền dòng xen kẽ DataGridView**:
  - Dòng chẵn: `Color.White` (`#FFFFFF`).
  - Dòng lẻ: `Color.FromArgb(248, 250, 252)`.
  - Dòng đang chọn (Selected Row): Nền `Color.FromArgb(224, 231, 255)`, Chữ `Color.FromArgb(30, 41, 59)`.
- **Quy chuẩn màu chữ (ForeColor) & Màu trạng thái (Lấy `frmDanhSachHangHoa` làm chuẩn duy nhất)**:
  - **Màu chữ mặc định**: Mọi nhãn (Label), ô nhập liệu (TextBox), ComboBox... đều dùng màu chữ mặc định của hệ thống (`SystemColors.ControlText` / kế thừa từ Form cha), **tuyệt đối KHÔNG tự ý chọn màu chữ mỗi trường một kiểu** (như tự ý tô `DimGray`, `FromArgb(100, 116, 139)`, `FromArgb(71, 85, 105)`, `FromArgb(15, 23, 42)`, `FromArgb(30, 41, 59)`...).
  - Chỉ các trường hợp **chênh lệch hoặc bất thường cần lưu ý mới được tô ĐỎ** (`Color.Red`, font Bold) – ví dụ: chênh lệch khi kiểm kho (cả lệch tăng và lệch giảm khi khác 0 đều tô đỏ, theo mẫu cảnh báo tồn kho <= 0 của `frmDanhSachHangHoa`), công nợ quá hạn/tồn âm.
  - **Quy chuẩn màu sắc hiển thị trạng thái (3 nhóm chuẩn toàn hệ thống)**:
    + **Nhóm Hoàn thành / Hoạt động** (*Đã nhập hàng*, *Đã nhận*, *Đã cân bằng*, *Đã trả hàng*, *Đang hoạt động*, *Đang bán*): Màu **Xanh lá** (`Color.ForestGreen` hoặc `Color.FromArgb(22, 163, 74)` theo mẫu `frmNhaCungCap`).
    + **Nhóm Tạm thời** (*Phiếu tạm*): Màu **Vàng** (`Color.FromArgb(217, 119, 6)` theo mẫu `frmChiTietTraHangNhap`).
    + **Nhóm Hủy / Ngừng** (*Đã hủy*, *Ngừng hoạt động*, *Ngừng bán*): Màu **Xám nhạt** (`Color.FromArgb(100, 116, 139)` theo mẫu `frmTraHangNhap`).
- **Quy chuẩn Nút bấm (Button)**:
  - Tuân thủ WinForms chuẩn (`UseVisualStyleBackColor = true`), không tự ý đổi màu nền trừ khi có yêu cầu.
  - Tuyệt đối KHÔNG tự ý chèn icon, emoji (như 🔒, 🗑, v.v.) vào nội dung Text của nút bấm.
  - Tuyệt đối KHÔNG gán phím tắt hàng F (`F9`, `F10`...) cho các nút chức năng trên giao diện.
  - **Quy chuẩn nút Xóa**: Khi 1 phiếu/đơn không đủ điều kiện xóa (ví dụ phiếu đã cân bằng kho, đã duyệt, đã hủy...), CHỈ disable nút xóa (`btnXoa.Enabled = false`), **vẫn giữ nguyên chữ "Xóa"**, tuyệt đối không được tự ý đổi text của nút (không đổi thành "Không thể xóa", "Đã hủy"...) hoặc đưa ra thông báo cảnh báo khi nhấn như trong `frmDanhSachHangHoa`.

### 6.3. Tiêu Chuẩn Trải Nghiệm & Kích Thước Form (Form Sizes Hierarchy)
- **Đệm đôi chống nhấp nháy**: Mọi DataGridView bắt buộc kích hoạt `DoubleBuffered = true` bằng Reflection tại sự kiện `Load`.
- **Khóa chỉnh sửa trên lưới danh sách**: `ReadOnly = true`, `AllowUserToAddRows = false`, `SelectionMode = FullRowSelect`.
- **Quy chuẩn Kích thước Form toàn hệ thống (Lấy các form mẫu làm khung chuẩn)**:
  1. **Chức năng lớn (Màn hình danh sách chính / Phân hệ lớn)**:
     - Lấy `frmDanhSachHangHoa` làm chuẩn kích thước duy nhất:
       + Kích thước: `ClientSize = new System.Drawing.Size(1782, 853);`
       + Khóa mở rộng: `FormBorderStyle = FormBorderStyle.FixedSingle;`, `MaximizeBox = false;`
       + Định vị: `StartPosition = FormStartPosition.CenterScreen;`
     - Mọi form chức năng lớn khác đều phải có kích thước đúng như vậy, tuyệt đối không được làm khác trừ khi được yêu cầu cụ thể (ngoại trừ duy nhất form chức năng lớn Đổi kho `frmDoiKho` được yêu cầu làm bằng form size nhỏ).
     - **Tuyệt đối KHÔNG gán `WindowState = FormWindowState.Maximized;`** khi mở form từ `frmMain`, giữ nguyên kích thước chuẩn cố định không fill full màn hình.
  2. **Chức năng nhỏ hơn (Form chi tiết, Form thêm/sửa/xóa, Popup, Modal)**:
     - Lấy mẫu các form sau làm khung để các form khác tuân thủ:
       + **Size Lớn**: Lấy size của form Cập nhật hàng hóa (`frmHangHoa`) làm chuẩn (`ClientSize = new System.Drawing.Size(1182, 803)`). Dành cho các form thêm mới chứng từ nhiều trường, nhiều vùng dữ liệu phức tạp (`frmThemPhieuNhap`, `frmThemPhieuKiemKho`, `frmThemPhieuChuyenKho`, `frmThemPhieuTraHang`...).
       + **Size Vừa**: Lấy size form Cập nhật thông tin nhà cung cấp (`frmDonViCapHang`) làm chuẩn (`ClientSize = new System.Drawing.Size(960, 705)`). Dành cho các form chi tiết chứng từ Master-Detail (`frmChiTietPhieuNhap`, `frmChiTietTraHangNhap`, `frmChiTietKiemKho`, `frmChiTietChuyenKho`...) và form cập nhật danh mục trung bình.
       + **Size Nhỏ**: Lấy size của form chức năng lớn Đổi kho (`frmDoiKho`) làm chuẩn (`ClientSize = new System.Drawing.Size(356, 136)`). Dành cho các form modal lựa chọn nhanh, popup xác nhận đơn giản.
- **Thanh phân trang đáy (Pagination)**: Dock dưới cùng, hỗ trợ tùy chọn số dòng/trang (10, 15, 20, 50, 100), nút trước/sau và nhãn trang hiện tại.

### 6.4. Quy Chuẩn Bộ Lọc GroupBox Bên Trái (Left Filter GroupBox) – Đồng bộ với `frmDanhSachHangHoa`
- **Thống nhất 100% sử dụng ComboBox**:
  - Toàn bộ các tiêu chí lọc trong GroupBox bên trái (`grpBoLoc`) đều phải là `ComboBox`, **tuyệt đối không dùng CheckBox rời rạc**.
  - Ví dụ: Trạng thái chứng từ/hàng hóa phải là ComboBox `cboTrangThai` với các giá trị: `-- Tất cả trạng thái --`, `Đã cân bằng`, `Phiếu tạm`, `Đã hủy`... (chọn 1 trạng thái rõ ràng, có thể định nghĩa dạng `DropDownList` hoặc `DropDown`).
- **Tìm kiếm trực tiếp bằng text nhập vào với danh mục nhiều giá trị**:
  - Đối với các trường danh mục nhiều giá trị (Nhà cung cấp, Nhóm hàng, Vị trí, Người tạo, Người trả/nhập...): bắt buộc tích hợp qua `ComboboxHelper.CaiDatTimKiem(cbo, dt, displayCol, valueCol, onCommitted)`.
  - Cho phép người dùng gõ text tìm kiếm không phân biệt hoa thường, không dấu/có dấu, tự động giới hạn độ cao tối đa 8 dòng dropdown và tự động chọn phần tử đầu tiên khi gõ Enter.
- **Ngoại trừ duy nhất bộ lọc khoảng thời gian & BẮT BUỘC ĐẶT Ở VỊ TRÍ CUỐI CÙNG (ở đáy)**:
  - Không dùng ComboBox cho khoảng thời gian mà bắt buộc sử dụng **cụm GroupBox con `grpThoiGianTao`** làm chuẩn duy nhất theo mẫu `frmDanhSachHangHoa`:
    - GroupBox: `grpThoiGianTao`, kích thước `322x100`, nằm trong `grpBoLoc`.
    - **Vị trí bắt buộc**: Đặt ở **CUỐI CÙNG (dưới cùng)** của các bộ lọc bên trái. Tất cả các bộ lọc danh mục ComboBox khác đều phải đặt ở phía trên.
    - Gồm 2 DateTimePicker: `dtpTuNgay` (Từ ngày) và `dtpDenNgay` (Đến ngày) đều cấu hình `Format = Short` (`dd/MM/yyyy`).
    - Nhãn chỉ dẫn: `lblTuNgay` (Từ), `lblDenNgay` (Đến).
    - Sự kiện `ValueChanged` tự động làm mới danh sách bảng dữ liệu về trang 1.

### 6.5. Quy Chuẩn Vị Trí & Thứ Tự Nút Chức Năng Nhỏ và Nhãn Trạng Thái (Điều luật 19)
- **6.5.1. Nhãn đánh dấu trạng thái chứng từ**:
  - Nhãn hiển thị trạng thái của phiếu (chi tiết phiếu nhập, chi tiết phiếu kiểm, chi tiết trả hàng...) hiển thị dưới dạng **dòng chữ có màu của trạng thái** (tuân thủ Bảng màu 3 nhóm tại Mục 6.2), lấy khuôn mẫu từ `frmChiTietPhieuNhap` (`lblTrangThai`: `Font = Segoe UI, 10pt Regular`, không viền, không nền màu `BackColor`, không đệm `Padding`).
  - Tuyệt đối KHÔNG làm thành khung/badge có viền, nền màu hay padding.
- **6.5.2. Vị trí và thứ tự các nút chức năng nhỏ trên form**:
  - Toàn bộ các nút chức năng thao tác của form nghiệp vụ/nhập liệu bắt buộc đặt ở **CUỐI (ở đáy) của form** (`DockStyle.Bottom` hoặc panel đáy) lấy khuôn mẫu từ `frmThemPhieuNhap`. Tuyệt đối không đặt lên trên đầu/header của form.
  - **Thứ tự nút chốt toàn hệ thống (tính từ trái qua phải)**:
    1. `Bỏ qua`: Hủy/đóng form mà không lưu.
    2. `Lưu tạm`: Lưu chứng từ ở trạng thái nháp/tạm (`NHAP`), không làm biến động sổ sách tồn kho (nếu form có hỗ trợ lưu tạm).
    3. `Lưu`: Lưu và xác nhận hoàn tất nghiệp vụ (cập nhật tồn kho / cân bằng kho / lưu dữ liệu chính thức).
  - Đối với form danh mục không có lưu tạm (ví dụ `frmHangHoa`, `frmDonViCapHang`, `frmNhomNhaCungCap`), thứ tự từ trái qua phải là: `Bỏ qua` ➜ `Lưu`.
