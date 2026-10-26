# Đặc tả Chức năng: Quản lý Đơn vị tính (Unit of Measure Management - Windows Forms)

> **Mục tiêu**: Quản lý danh mục Đơn vị tính chuẩn giao diện & nghiệp vụ hệ thống kho hàng nội bộ. Hỗ trợ tìm kiếm tức thì, lọc tình trạng sử dụng, thống kê số lượng mặt hàng đang áp dụng từng đơn vị tính, bảo vệ ràng buộc toàn vẹn dữ liệu (không xóa ĐVT đang có hàng hóa liên kết), mở form MDI, thêm mới/chỉnh sửa đơn vị tính, tạo nhanh ĐVT từ màn hình thêm hàng hóa và xuất danh sách ra file CSV/Excel UTF-8 BOM.

---

## 1. Kiến trúc File & CSDL

### 1.1. Bản đồ File liên quan (File Map)
- **File chức năng**: `BTL-LTTQ-2/GUI/Forms/DanhSachHangHoa/frmDonViTinh.cs` (kèm `.Designer.cs`, `.resx`): Form popup quản lý Đơn vị tính (kích thước chuẩn khuôn mẫu `frmNhomHang` & `frmViTri`), gồm danh sách DataGridView (`colMaDVT`, `colTenDVT`, `colSoLuongHang`), tìm kiếm tức thì, chức năng Thêm / Sửa / Xóa (có kiểm tra ràng buộc hàng hóa đang dùng), nhập file / xuất file Excel/CSV.
- **Tích hợp quản lý & lọc**:
  - `BTL-LTTQ-2/GUI/Forms/DanhSachHangHoa/frmDanhSachHangHoa.cs`: Bộ lọc ComboBox `cboDonViTinh` bên trái kèm nút `+` (`btnThemDonViTinh`) mở `frmDonViTinh`.
  - `BTL-LTTQ-2/GUI/Forms/DanhSachHangHoa/frmHangHoa.cs`: ComboBox chọn đơn vị tính khi tạo/sửa hàng hóa.
- *(Lưu ý: Thư mục cũ `GUI/Forms/DonViTinh/` gồm 3 form MDI độc lập đã được loại bỏ hoàn toàn để đồng bộ kiến trúc quản lý danh mục con theo quy định tại Phiên 39 & 50).*
- **Truy xuất dữ liệu (DAL)**: `BTL-LTTQ-2/Classes/DataProcessing.cs` (ADO.NET thuần: `ReadData`, `ReadValue`, `ChangeDataTransaction`).

### 1.2. Cơ sở dữ liệu
- **Bảng `DonViTinh`**:
  - `MaDVT` INT IDENTITY(1,1) PRIMARY KEY (Mã định danh đơn vị tính, hiển thị dạng `DVT00X`)
  - `TenDVT` NVARCHAR(50) NOT NULL (Tên đơn vị tính: Cái, Hộp, Bình, Mét, Thùng, Bao, Cuộn, Bộ...)
  - `MoTa` NVARCHAR(255) NULL
- **Bảng liên quan**:
  - `HangHoa` (`MaHang`, `MaHangNoiBo`, `TenHang`, `MaDVT` FK)

---

## 2. Luồng Tương tác Nghiệp vụ Chi tiết

| Luồng | Tương tác người dùng | Giao diện WinForms (GUI) | Xử lý & CSDL (DAL / SQL) |
|---|---|---|---|
| **1. Mở danh sách** | Vào Menu "Hàng hóa" ➜ "Đơn vị tính" | `mnuDonViTinh_Click` kiểm tra form con trong `MdiChildren`. Nếu có ➜ `Activate()`, nếu chưa ➜ `Show()` | Nạp danh sách ĐVT kèm thống kê số lượng hàng hóa đang sử dụng từ `HangHoa` |
| **2. Lọc tình trạng sử dụng** | Chọn Trạng thái sử dụng trên ComboBox bên trái | Đặt lại `trangHienTai = 1`, nạp lại danh sách | Mệnh đề `WHERE` / `HAVING` lọc: Tất cả / Đang có hàng hóa sử dụng (`COUNT(h.MaHang) > 0`) / Chưa sử dụng (`COUNT(h.MaHang) = 0`) |
| **3. Tìm kiếm tức thời** | Gõ từ khóa vào ô Tìm kiếm | Sự kiện `txtTimKiem_TextChanged` kích hoạt tìm tức thời | `WHERE dvt.TenDvt LIKE @Search OR CAST(dvt.MaDVT AS NVARCHAR(20)) LIKE @Search OR ('DVT' + RIGHT('000' + CAST(dvt.MaDVT AS VARCHAR(10)), 3)) LIKE @Search` |
| **4. Phân trang** | Bấm "◀ Trước", "Sau ▶" hoặc đổi số dòng: 10, 20, 50, 100 | Phân trang chuẩn hệ thống | SQL Server phân trang chuẩn `OFFSET @Skip ROWS FETCH NEXT @Take ROWS ONLY` |
| **5. Thêm mới ĐVT** | Bấm nút "+ Đơn vị tính" | Mở `frmChiTietDonViTinh` dạng modal dialog `ShowDialog(this)` chế độ thêm mới (`maDVT = 0`). Tự động sinh mã gợi ý `DVT00X` hiển thị cho phép xem & sửa; ẩn nút Xóa | Validate bắt buộc `txtMaDvt` và `TenDvt`, chống trùng mã và trùng tên ĐVT, bọc `ChangeDataTransaction` |
| **6. Chỉnh sửa ĐVT** | Double-click vào một dòng trên DataGridView | Mở `frmChiTietDonViTinh` dạng modal dialog `ShowDialog(this)` chế độ sửa (`maDVT > 0`), nạp thông tin mã ĐVT, tên ĐVT, hiển thị nút Xóa ở góc trái dưới đáy, nạp danh sách hàng hóa đang dùng | Khóa mã ĐVT (chỉ đọc), cho phép sửa tên ĐVT, kiểm tra trùng lặp với các ĐVT khác, cập nhật `DonViTinh` trong Transaction, khi đóng trả `DialogResult.OK` tự làm mới danh sách |
| **7. Xóa ĐVT** | Nhấn nút "Xóa" trong form chi tiết `frmChiTietDonViTinh` | Kiểm tra điều kiện xóa: nếu đang có mặt hàng sử dụng ➜ chặn xóa và thông báo chi tiết. Nếu 0 mặt hàng ➜ hiện hộp thoại xác nhận | Thực hiện `DELETE FROM DonViTinh WHERE MaDVT = @MaDVT` trong Transaction, ghi nhật ký thao tác `NhatKyThaoTac`, trả về `DialogResult.OK` để form danh sách tự tải lại |
| **8. Tạo nhanh ĐVT từ Form Hàng hóa** | Bấm nút "+" cạnh `cboDonVi` trên `frmHangHoa` | Mở modal dialog `frmChiTietDonViTinh` | Sau khi lưu thành công, nạp lại ComboBox `cboDonVi` và tự động chọn ĐVT vừa tạo |
| **9. Tùy chọn Ẩn/Hiện cột** | Bấm nút "Hiện cột" | Mở popup `frmHienCotDvt` ngay dưới nút | Bật/tắt `dgv.Columns[col].Visible` tức thì |
| **10. Xuất file CSV / Excel** | Bấm nút "Xuất file" | Hộp thoại `SaveFileDialog` lưu file `.csv` | Xuất đúng các cột đang hiển thị với định dạng UTF-8 BOM đọc tốt tiếng Việt trên Excel |

---

## 3. Quy chuẩn Giao diện & Bộ lọc

### 3.1. Cột lọc bên trái (Sidebar Filter - Chiều rộng 340px, đồng bộ `frmDanhSachHangHoa`):
1. **Tình trạng sử dụng**:
   - ComboBox `cboTrangThai`: DropDownList ("-- Tất cả đơn vị tính --", "Đang có hàng hóa sử dụng", "Chưa có hàng hóa nào").
2. **Sắp xếp**:
   - ComboBox `cboSapXep`: "Tên đơn vị tính (A-Z)", "Tên đơn vị tính (Z-A)", "Số lượng hàng hóa (Nhiều nhất)", "Số lượng hàng hóa (Ít nhất)", "Mã đơn vị tính (Tăng dần)".
3. **Khoảng thời gian tạo** (GroupBox `grpThoiGianTao` ở đáy theo Luật 16).

### 3.2. Nút bấm thao tác trên cùng:
- Tiêu đề: `Đơn vị tính` (Font `Segoe UI, 12pt Bold`).
- Ô tìm kiếm: `txtTimKiem` (tìm kiếm theo Tên, Mã ĐVT).
- Nút `+ Đơn vị tính` (`btnThemDonViTinh`): Nút WinForms chuẩn (`UseVisualStyleBackColor = true`), bố trí ngay cạnh ô tìm kiếm.
- Nút `Xuất file` (`btnXuatFile`), `Hiện cột` (`btnHienCot`), `Hướng dẫn sử dụng` (`btnHuongDanSuDung`): Căn phải đồng bộ toàn hệ thống.
- *(Lưu ý: Nút Xóa đã chuyển vào trong form chi tiết `frmChiTietDonViTinh` theo mẫu `frmHangHoa`).*

### 3.3. Chuẩn DataGridView danh sách `frmDonViTinh`:
- Đệm đôi: `DoubleBuffered = true`.
- Không dùng cột STT (đã loại bỏ theo yêu cầu người dùng).
- Cột hộp kiểm `colChon`: CheckBox chọn dòng.
- Cột `colMaDVT`: Căn giữa `MiddleCenter`, hiển thị mã đơn vị tính (`MaDvtNoiBo` hoặc `DVT00X`).
- Cột `colTenDVT`: Căn trái `MiddleLeft`, in đậm nhẹ.
- Cột `colSoLuongHangHoa`: Số lượng mặt hàng đang dùng, căn phải `MiddleRight`, định dạng `"N0"`.
- Cột `colTrangThaiSuDung`: "Đang sử dụng" (Màu xanh lá `Color.FromArgb(22, 163, 74)`), "Chưa sử dụng" (Màu xám nhạt `Color.FromArgb(100, 116, 139)`).
- Cột `colHangHoaTieuBieu`: Liệt kê top mặt hàng đại diện đang sử dụng.
- Không tự ý thêm icon emoji vào nút bấm (Luật 15c).
- Font chữ toàn bộ là `Segoe UI, 9.5pt` (Luật 18).

### 3.4. Chuẩn Form chi tiết `frmChiTietDonViTinh`:
- Kích thước chuẩn: `ClientSize = (684, 520)`, `FormBorderStyle = FixedDialog`, `MaximizeBox = false`, `StartPosition = CenterParent`.
- Bố cục thanh đáy `pnlBottom`:
  + Góc trái: `btnXoa` (`Location = (20, 11)`). Ẩn khi thêm mới (`Visible = false`), hiện khi sửa (`Visible = true`).
  + Góc phải: `btnBoQua` (`Location = (464, 11)`), `btnLuu` (`Location = (572, 11)`).
- Nhập liệu:
  + `txtMaDvt`: Mã đơn vị tính tự động sinh gợi ý khi thêm mới (`DVT00X`), vẫn hiển thị cho người dùng xem và có thể sửa (`ReadOnly = false`). Kiểm tra trùng mã khi lưu.
  + `txtTenDvt`: Tên đơn vị tính bắt buộc nhập, kiểm tra trùng tên khi lưu.
  + `dgvHangHoa`: Bảng lưới danh sách các mặt hàng đang áp dụng ĐVT này (Tên hàng, Nhóm hàng, Giá bán).


---

## 4. Checklist kiểm tra
- [ ] Code biên dịch không lỗi (0 Warning, 0 Error).
- [ ] Tuân thủ 18 điều luật cốt lõi trong `AGENTS.md`.
- [ ] Đăng ký form mới vào `BTL-LTTQ-2.csproj`.
- [ ] Lưu tất cả file C# bằng UTF-8 with BOM.
- [ ] Kiểm tra ràng buộc khóa ngoại không làm hỏng dữ liệu `HangHoa`.
