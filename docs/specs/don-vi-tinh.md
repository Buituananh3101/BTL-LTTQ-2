# Đặc tả Chức năng: Quản lý Đơn vị tính (Unit of Measure Management - Windows Forms)

> **Mục tiêu**: Quản lý danh mục Đơn vị tính chuẩn giao diện & nghiệp vụ hệ thống kho hàng nội bộ. Hỗ trợ tìm kiếm tức thì, lọc tình trạng sử dụng, thống kê số lượng mặt hàng đang áp dụng từng đơn vị tính, bảo vệ ràng buộc toàn vẹn dữ liệu (không xóa ĐVT đang có hàng hóa liên kết), mở form MDI, thêm mới/chỉnh sửa đơn vị tính, tạo nhanh ĐVT từ màn hình thêm hàng hóa và xuất danh sách ra file CSV/Excel UTF-8 BOM.

---

## 1. Kiến trúc File & CSDL

### 1.1. Bản đồ File liên quan (File Map)
- **Thư mục chức năng**: `BTL-LTTQ-2/GUI/Forms/DonViTinh/`
  - `frmDonViTinh.cs`, `frmDonViTinh.Designer.cs`, `frmDonViTinh.resx`: Form chính hiển thị danh sách Đơn vị tính (kích thước chuẩn `1782x853`, `FixedSingle`, `MaximizeBox = false`, `StartPosition = CenterScreen`), thanh tìm kiếm F3, combobox bộ lọc bên trái (chiều rộng 340px), DataGridView định dạng chuẩn đệm đôi `DoubleBuffered`, thanh phân trang (tùy chọn 10, 20, 50, 100 dòng).
  - `frmChiTietDonViTinh.cs`, `frmChiTietDonViTinh.Designer.cs`, `frmChiTietDonViTinh.resx`: Form phụ Modal Dialog Thêm mới / Chỉnh sửa đơn vị tính (cho phép nhập Tên ĐVT, ghi chú nếu có, hiển thị danh sách các mặt hàng đang áp dụng ĐVT này, bọc `ChangeDataTransaction`).
  - `frmHienCotDvt.cs`, `frmHienCotDvt.Designer.cs`, `frmHienCotDvt.resx`: Form popup tùy chọn Ẩn / Hiện cột của DataGridView đơn vị tính.
- **Tích hợp tạo nhanh**:
  - `BTL-LTTQ-2/GUI/Forms/DanhSachHangHoa/frmHangHoa.cs`, `frmHangHoa.Designer.cs`: Bổ sung nút `+` cạnh `cboDonVi` để mở nhanh `frmChiTietDonViTinh` và tự động chọn ĐVT vừa tạo.
- **Form cha MDI & Menu**: `BTL-LTTQ-2/GUI/Forms/frmMain.cs` (gắn sự kiện `mnuDonViTinh_Click` mở và kích hoạt form MDI Child).
- **Truy xuất dữ liệu (DAL)**: `BTL-LTTQ-2/Classes/DataProcessing.cs` (ADO.NET thuần: `ReadData`, `ReadValue`, `ChangeDataTransaction`).

### 1.2. Cơ sở dữ liệu
- **Bảng `DonViTinh`**:
  - `MaDvt` INT IDENTITY(1,1) PRIMARY KEY
  - `TenDvt` NVARCHAR(50) NOT NULL (Tên đơn vị tính: Cái, Hộp, Bình, Mét, Thùng, Bao, Cuộn, Bộ...)
- **Bảng liên quan**:
  - `HangHoa` (`MaHang`, `MaHangNoiBo`, `TenHang`, `MaDvt` FK)

---

## 2. Luồng Tương tác Nghiệp vụ Chi tiết

| Luồng | Tương tác người dùng | Giao diện WinForms (GUI) | Xử lý & CSDL (DAL / SQL) |
|---|---|---|---|
| **1. Mở danh sách** | Vào Menu "Hàng hóa" ➜ "Đơn vị tính" | `mnuDonViTinh_Click` kiểm tra form con trong `MdiChildren`. Nếu có ➜ `Activate()`, nếu chưa ➜ `Show()` | Nạp danh sách ĐVT kèm thống kê số lượng hàng hóa đang sử dụng từ `HangHoa` |
| **2. Lọc tình trạng sử dụng** | Chọn Trạng thái sử dụng trên ComboBox bên trái | Đặt lại `trangHienTai = 1`, nạp lại danh sách | Mệnh đề `WHERE` / `HAVING` lọc: Tất cả / Đang có hàng hóa sử dụng (`COUNT(h.MaHang) > 0`) / Chưa sử dụng (`COUNT(h.MaHang) = 0`) |
| **3. Tìm kiếm tức thời** | Gõ từ khóa vào ô Tìm kiếm | Sự kiện `txtTimKiem_TextChanged` kích hoạt tìm tức thời | `WHERE dvt.TenDvt LIKE @Search OR CAST(dvt.MaDvt AS NVARCHAR) LIKE @Search` |
| **4. Phân trang** | Bấm "◀ Trước", "Sau ▶" hoặc đổi số dòng: 10, 20, 50, 100 | Phân trang chuẩn hệ thống | SQL Server phân trang chuẩn `OFFSET @Skip ROWS FETCH NEXT @Take ROWS ONLY` |
| **5. Thêm mới ĐVT** | Bấm nút "+ Đơn vị tính" | Mở `frmChiTietDonViTinh` dạng modal dialog `ShowDialog(this)` chế độ thêm mới (`maDVT = 0`) | Validate bắt buộc `TenDvt`, chống trùng tên ĐVT, bọc `ChangeDataTransaction` |
| **6. Chỉnh sửa ĐVT** | Double-click vào một dòng trên DataGridView | Mở `frmChiTietDonViTinh` dạng modal dialog `ShowDialog(this)` chế độ sửa (`maDVT > 0`), hiển thị danh sách hàng hóa đang dùng | Cập nhật `DonViTinh`, khi đóng trả `DialogResult.OK` tự làm mới danh sách |
| **7. Xóa ĐVT** | Chọn dòng (hoặc tích chọn nhiều dòng) và bấm nút "Xóa" | Hiện hộp thoại xác nhận | Kiểm tra ràng buộc: Nếu ĐVT đang được gắn với ít nhất 1 mặt hàng trong bảng `HangHoa`, chặn không cho xóa và thông báo cảnh báo rõ ràng. Nếu chưa có hàng hóa nào sử dụng thì cho phép xóa |
| **8. Tạo nhanh ĐVT từ Form Hàng hóa** | Bấm nút "+" cạnh `cboDonVi` trên `frmHangHoa` | Mở modal dialog `frmChiTietDonViTinh` | Sau khi lưu thành công, nạp lại ComboBox `cboDonVi` và tự động chọn ĐVT vừa tạo |
| **9. Tùy chọn Ẩn/Hiện cột** | Bấm nút "Hiện cột" | Mở popup `frmHienCotDvt` ngay dưới nút | Bật/tắt `dgv.Columns[col].Visible` tức thì |
| **10. Xuất file CSV / Excel** | Bấm nút "Xuất file" | Hộp thoại `SaveFileDialog` lưu file `.csv` | Xuất đúng các cột đang hiển thị với định dạng UTF-8 BOM đọc tốt tiếng Việt trên Excel |

---

## 3. Quy chuẩn Giao diện & Bộ lọc

### 3.1. Cột lọc bên trái (Sidebar Filter - Chiều rộng 340px, đồng bộ `frmDanhSachHangHoa`):
1. **Tình trạng sử dụng**:
   - ComboBox `cboTrangThaiSuDung`: DropDownList ("-- Tất cả đơn vị tính --", "Đang có hàng hóa sử dụng", "Chưa có hàng hóa sử dụng").
2. **Khoảng thời gian tạo** (GroupBox `grpThoiGianTao` ở đáy theo Luật 16).

### 3.2. Nút bấm thao tác trên cùng:
- Tiêu đề: `Đơn vị tính` (Font `Segoe UI, 12pt Bold`).
- Ô tìm kiếm: `txtTimKiem` (placeholder hoặc label tìm kiếm theo Tên, Mã ĐVT).
- Nút `+ Đơn vị tính` (`btnThemDonViTinh`): Nút WinForms chuẩn (`UseVisualStyleBackColor = true`).
- Nút `Xóa` (`btnXoa`): Xóa các ĐVT chưa có hàng hóa liên kết.
- Nút `Xuất file` (`btnXuatFile`), `Hiện cột` (`btnHienCot`), `Hướng dẫn sử dụng` (`btnHuongDanSuDung`): Căn phải đồng bộ toàn hệ thống.

### 3.3. Chuẩn DataGridView:
- Đệm đôi: `DoubleBuffered = true`.
- Cột hộp kiểm `colChon`: CheckBox chọn nhiều dòng, có header "Chọn tất cả".
- Cột `colMaDVT`: Căn giữa `MiddleCenter`.
- Cột `colTenDVT`: Căn trái `MiddleLeft`, in đậm nhẹ.
- Cột `colSoLuongHangHoa`: Số lượng mặt hàng đang dùng, căn phải `MiddleRight`, định dạng `"N0"`.
- Cột `colTrangThai`: "Đang sử dụng" (Màu xanh lá `Color.FromArgb(22, 163, 74)`), "Chưa sử dụng" (Màu xám nhạt `Color.FromArgb(100, 116, 139)`).
- Không tự ý thêm icon emoji vào nút bấm (Luật 15c).
- Font chữ toàn bộ là `Segoe UI, 9.5pt` (Luật 18).

---

## 4. Checklist kiểm tra
- [ ] Code biên dịch không lỗi (0 Warning, 0 Error).
- [ ] Tuân thủ 18 điều luật cốt lõi trong `AGENTS.md`.
- [ ] Đăng ký form mới vào `BTL-LTTQ-2.csproj`.
- [ ] Lưu tất cả file C# bằng UTF-8 with BOM.
- [ ] Kiểm tra ràng buộc khóa ngoại không làm hỏng dữ liệu `HangHoa`.
