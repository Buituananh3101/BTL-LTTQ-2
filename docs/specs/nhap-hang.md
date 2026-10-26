# Đặc tả Chức năng: Nhập hàng (Purchase Orders - Windows Forms)

> **Mục tiêu**: Quản lý chứng từ Nhập hàng từ Nhà cung cấp chuẩn giao diện & nghiệp vụ hệ thống kho hàng nội bộ đa chi nhánh. Đồng bộ 100% với khuôn mẫu `frmDanhSachHangHoa` và `frmNhaCungCap` (kích thước form chuẩn 1782x853, bộ lọc bên trái 100% ComboBox chiều rộng 340px, tìm kiếm tức thời, phân trang SQL Server, DataGridView đệm đôi chống giật lag, xuất file CSV UTF-8 BOM, nút bấm chuẩn WinForms không màu mè).

---

## 1. Kiến trúc File & CSDL

### 1.1. Bản đồ File liên quan (File Map)
- **Thư mục chức năng**: `BTL-LTTQ-2/GUI/Forms/NhapHang/`
  - `frmNhapHang.cs`, `.Designer.cs`, `.resx`: Form chính hiển thị danh sách đơn nhập hàng (kích thước chuẩn 1782x853), panel lọc bên trái rộng 387px, ComboBox lọc rộng 340px, tìm kiếm tức thời F3, nút `+ Nhập hàng`, `Xuất file`, `Hiện cột`, `Hướng dẫn sử dụng`. Phân trang 10, 20, 50, 100 dòng (mặc định 20). Hỗ trợ nhấn đúp chuột vào đơn nhập để mở form xem chi tiết đơn.
  - `frmThemPhieuNhap.cs`, `.Designer.cs`, `.resx`: Form lập phiếu nhập hàng mới (mở khi bấm `+ Nhập hàng`), đồng bộ 100% bố cục và giao diện theo khuôn mẫu `frmThemPhieuKiemKho` (kích thước 1582x803, panel thông tin bên phải rộng 400px với 3 nút bấm ở đáy `Bỏ qua` ➜ `Lưu tạm` ➜ `Lưu`, panel trên chọn hàng cao 150px, DataGridView trung tâm chỉnh sửa số lượng trực tiếp trên lưới, kiểm tra an toàn cột số lượng khi ẩn, phân trang đáy 10, 20, 50, 100 dòng, bọc Transaction an toàn).
  - `frmHienCotHangNhap.cs`, `.Designer.cs`, `.resx`: Form popup ẩn/hiện cột dữ liệu cho DataGridView `dgvChiTietHang` trong `frmThemPhieuNhap` (quản lý 7 cột: Mã hàng, Tên hàng, Đơn vị tính, Số lượng, Đơn giá, Giảm giá, Thành tiền).
  - `frmChiTietPhieuNhap.cs`, `.Designer.cs`, `.resx`: Form xem chi tiết đơn nhập hàng (mở khi double-click từ danh sách). Hiển thị đầy đủ thông tin header (Mã phiếu, NCC, Kho, Ngày nhập, Trạng thái, Người nhập, Ghi chú), bảng lưới hàng hóa đã nhập đệm đôi `DoubleBuffered`, tổng kết thanh toán. **Cung cấp các nút chức năng ở thanh đáy: `Xóa`, `Xuất file`, `Hoàn thành` (chỉ hiển thị khi là Phiếu tạm `NHAP` để chuyển sang `DA_XAC_NHAN`), `Trả hàng nhập` (chỉ hiển thị khi đã hoàn thành `DA_XAC_NHAN`)** cùng nút `Đóng`.
  - `frmHienCotNhapHang.cs`, `.Designer.cs`, `.resx`: Form popup ẩn/hiện cột cho DataGridView danh sách phiếu nhập hàng `frmNhapHang` (quản lý các cột hiển thị trên danh sách đơn nhập).
  - `frmTraHangNhap.cs`, `.Designer.cs`, `.resx`: Form trả hàng nhập được kích hoạt từ nút `Trả hàng nhập` trên form chi tiết đơn (kiểm tra chặn âm, chặn vượt số lượng nhập, chặn vượt tồn kho hiện tại, tự động khấu trừ nợ hoặc xác định tiền hoàn lại, bọc Transaction).
- **Form cha MDI & Menu**: `BTL-LTTQ-2/GUI/Forms/frmMain.cs` (gắn sự kiện `mnuNhapHang_Click` kiểm tra kích hoạt hoặc tạo mới form `frmNhapHang`).
- **Truy xuất dữ liệu (DAL)**: `BTL-LTTQ-2/Classes/DataProcessing.cs` (ADO.NET thuần: `ReadData`, `ReadValue`, `ChangeDataTransaction`).
- **Quản lý phiên đăng nhập**: `BTL-LTTQ-2/Classes/SessionContext.cs` (lấy `MaTaiKhoan`, `MaKhoHienTai`, vai trò).

### 1.2. Cơ sở dữ liệu tham gia
- **`PhieuNhap`**: `MaPhieuNhap`, `MaPhieu`, `MaKho`, `MaDonViCapHang`, `MaNvlap`, `NgayLap`, `NgayXacNhan`, `LoaiPhieuNhap` (`THONG_THUONG`), `TrangThai` (`NHAP`, `DA_XAC_NHAN`, `HUY`), `TongTien`, `GiamGia`, `DaTraNcc`, `CanTraNcc`, `GhiChu`, `DaDanhDau`, `SoHoaDon`.
- **`ChiTietPhieuNhap`**: `MaCtphieuNhap`, `MaPhieuNhap`, `MaHang`, `SoLuong`, `DonGiaNhap`, `GiamGia`, `ThanhTien`.
- **`DonViCapHang`**: Nhà cung cấp cung ứng hàng.
- **`vw_BienDongKho` & `vw_TonKhoHienTai`**: Tự động tăng số lượng tồn khi phiếu chuyển sang `DA_XAC_NHAN`. Tự động hoàn lại khi chuyển trạng thái `HUY`.
- **`NhatKyThaoTac`**: Ghi log thao tác tạo, hoàn thành, xóa hoặc hủy phiếu nhập.

---

## 2. Luồng Tương tác Nghiệp vụ Chi tiết

| Luồng | Tương tác người dùng | Giao diện WinForms (GUI) | Xử lý & CSDL (DAL / SQL) |
|---|---|---|---|
| **1. Mở danh sách** | Vào Menu "Mua hàng" ➜ "Nhập hàng" | `mnuNhapHang_Click` kiểm tra form con trong `MdiChildren`. Nếu đã mở ➜ `Activate()`, nếu chưa ➜ `Show()` | Nạp danh mục NCC, người tạo, người nhập vào các ComboBox; nạp danh sách trang 1 |
| **2. Lọc đa tiêu chí** | Chọn Trạng thái, Nhà cung cấp, Người tạo, Người nhập hoặc Khoảng thời gian tạo | Đặt lại `trangHienTai = 1`, gọi `HienThiDanhSach()` nạp lại dữ liệu | Mệnh đề `WHERE` lọc động theo các tham số `@TrangThai`, `@MaNCC`, `@MaNguoiTao`, `@MaNguoiNhap`, `@TuNgay`, `@DenNgay` |
| **3. Tìm kiếm tức thời** | Gõ mã phiếu hoặc số hóa đơn vào ô Tìm kiếm | Sự kiện `txtTimKiem_TextChanged` kích hoạt tìm tức thời | Lọc `pn.MaPhieu LIKE @Search OR pn.GhiChu LIKE @Search OR pn.SoHoaDon LIKE @Search` |
| **4. Phân trang** | Bấm "◀ Trước", "Sau ▶" hoặc đổi số dòng: 10, 20, 50, 100 | `cboSoDongMoiTrang_SelectedIndexChanged`, `btnTrangTruoc_Click`, `btnTrangSau_Click` | SQL Server phân trang chuẩn `OFFSET @Skip ROWS FETCH NEXT @Take ROWS ONLY`. Nhãn: `Hiển thị X - Y trên tổng số Z phiếu nhập` |
| **5. Thêm mới đơn nhập** | Bấm nút `+ Nhập hàng` (`btnThemPhieuNhap`) | Mở form `frmThemPhieuNhap` dưới dạng Modal Dialog `ShowDialog(this)` chuẩn kích thước 1582x803, bố cục chuẩn khuôn mẫu `frmThemPhieuKiemKho` | Gợi ý tự động mã phiếu `PN...`, chọn NCC, kho, thêm hàng hóa, nhập trực tiếp số lượng trên ô lưới, tính tổng tiền, nợ. Lưu tạm (`NHAP`) hoặc Hoàn thành (`DA_XAC_NHAN`) trong Transaction |
| **6. Xem chi tiết đơn nhập** | Nhấn đúp chuột (Double Click) vào 1 dòng đơn nhập | Mở form `frmChiTietPhieuNhap` dạng Modal Dialog | Nạp thông tin header, danh sách hàng hóa và tổng kết thanh toán. Cung cấp đúng 3 nút: `Xóa`, `Xuất file`, `Trả hàng nhập` |
| **7. Xóa đơn nhập** | Bấm nút `Xóa` trên form chi tiết | Xác nhận DialogResult: Nếu phiếu `NHAP` ➜ Xóa cứng; Nếu `DA_XAC_NHAN` ➜ Kiểm tra an toàn tồn kho, chuyển trạng thái `HUY` | Bọc trong Transaction; ghi nhật ký thao tác; nếu hủy làm âm tồn kho thì chặn ngay |
| **8. Trả hàng nhập** | Bấm nút `Trả hàng nhập` trên form chi tiết | Mở modal dialog `frmTraHangNhap(maPhieuNhap)` | Chỉ mở được với phiếu `DA_XAC_NHAN`. Chuyển sang giao diện trả hàng nhập |
| **9. Tùy chọn Ẩn/Hiện cột** | Bấm nút "Hiện cột" | Mở popup `frmHienCotNhapHang` | Bật/tắt `dgv.Columns[col].Visible` tức thì |
| **10. Xuất file CSV** | Bấm nút "Xuất file" | Hộp thoại `SaveFileDialog` lưu file `.csv` | Xuất đúng các cột đang hiển thị với định dạng UTF-8 BOM đọc tốt trên Microsoft Excel |

---

## 3. Quy chuẩn Giao diện & Bộ lọc (Tuân thủ Luật 14 & 15)

### 3.1. Cột lọc bên trái (Sidebar Filter - 100% ComboBox - Kế thừa chuẩn `frmDanhSachHangHoa`):
- Khung `grpBoLoc`: `Location = (12, 9)`, `Size = (360, 717)`, viền chuẩn WinForms, dàn đều các bộ lọc (~75-85px).
- Cụm `grpThoiGianTao`: `Location = (18, 593)`, `Size = (322, 108)` đặt cố định ở đáy theo Điều luật 16, các DateTimePicker `Size = (227, 24)` nằm trọn vẹn trong khung.
1. **Trạng thái**: ComboBox `cboTrangThai`: DropDownList ("-- Tất cả trạng thái --", "Phiếu tạm (Lưu tạm)", "Đã nhập hàng", "Đã hủy").
2. **Nhà cung cấp**: ComboBox `cboNhaCungCap`: DropDownList ("-- Tất cả nhà cung cấp --", danh sách NCC từ CSDL).
3. **Người tạo**: ComboBox `cboNguoiTao`: DropDownList ("-- Tất cả người tạo --", danh sách nhân viên).
4. **Người nhập**: ComboBox `cboNguoiNhap`: DropDownList ("-- Tất cả người nhập --", danh sách nhân viên).
5. **Khoảng thời gian tạo** (GroupBox `grpThoiGianTao`):
   - `dtpTuNgay`: Chọn từ ngày (mặc định đầu năm).
   - `dtpDenNgay`: Chọn đến ngày (mặc định hôm nay).
   - Tự động lọc lại khi thay đổi ngày.

### 3.2. Nút bấm & Nhãn hiển thị:
- Nút `+ Nhập hàng`: Định dạng WinForms chuẩn (`UseVisualStyleBackColor = true`), không tự ý tô màu tùy tiện.
- 100% hiển thị tiếng Việt có dấu rõ ràng: `Xóa`, `Xuất file`, `Trả hàng nhập`, `Lưu tạm`, `Hoàn thành`, `Đóng`.
- Hàng tên cột (Column Header Row) DataGridView: Tuân thủ nghiêm ngặt chuẩn WinForms mặc định (`EnableHeadersVisualStyles = true`), không tự ý đổi màu nền phẳng hay đổi font in đậm; đồng bộ độ rộng các cột (`colChon` 45px, `colMaNcc` 140px) loại bỏ hiện tượng ngắt dòng, giữ chiều cao hàng tiêu đề gọn gàng (22px) và chiều cao dòng dữ liệu chuẩn 32px đồng bộ tuyệt đối với `frmDanhSachHangHoa` và `frmNhaCungCap`.
- Tên các cột tiêu đề đồng bộ chuẩn mực: `Mã nhà cung cấp`, `Tên nhà cung cấp` (khớp 100% với form Nhà cung cấp).
- Trạng thái "Đã nhập hàng" trên lưới và form chi tiết: Hiển thị font chữ chuẩn Regular (không in đậm), màu xanh lá nhận diện chuẩn.
- Số lượng hiển thị trong mọi hộp thoại cảnh báo và thông báo: Định dạng số nguyên chuẩn `N0` (chặn triệt để đuôi số thực `.00`).
- Quản lý công nợ NCC: Tính toán động từ các phiếu nhập (`PhieuNhap.CanTraNcc`), tuyệt đối không update cột tĩnh không tồn tại trong `DonViCapHang`.

### 3.3. Chi tiết đơn nhập (`frmChiTietPhieuNhap`):
- Tham khảo đầy đủ các vùng thông tin từ ảnh mẫu của người dùng:
  - Header: Mã phiếu, Nhà cung cấp, Kho nhập, Trạng thái chứng từ, Ngày nhập, Người nhập, Ghi chú.
  - Bảng DataGridView hàng hóa: STT, Mã hàng, Tên hàng, ĐVT, Số lượng, Đơn giá nhập, Giảm giá, Thành tiền.
  - Khối tổng kết thanh toán: Tổng số lượng, Tổng tiền hàng, Giảm giá, Tổng cộng cần trả, Đã trả NCC, Còn nợ NCC.
- **Ràng buộc quan trọng**: Giữ lại duy nhất 3 nút bấm chức năng ở góc dưới:
  1. `Xóa` (`btnXoaPhieu`): Thực hiện xóa phiếu tạm hoặc chuyển trạng thái sang `HUY` đối với phiếu đã xác nhận (kèm kiểm tra chặn âm tồn kho).
  2. `Xuất file` (`btnXuatFile`): Xuất thông tin phiếu và bảng hàng hóa ra file CSV/Excel UTF-8 BOM.
  3. `Hoàn thành` (`btnHoanThanh`): Chỉ hiển thị khi phiếu ở trạng thái `NHAP` (Phiếu tạm). Bấm Hoàn thành sẽ hỏi xác nhận, bọc trong Transaction cập nhật `TrangThai = 'DA_XAC_NHAN'`, `NgayXacNhan = GETDATE()`, `MaNVXacNhan`, tự động cộng tồn kho và ghi nhận công nợ NCC.
  4. `Trả hàng nhập` (`btnTraHangNhap`): Chỉ hiển thị khi phiếu ở trạng thái `DA_XAC_NHAN`. Kích hoạt `frmTraHangNhap` để hoàn trả một phần hoặc toàn bộ lô hàng cho NCC.

### 3.4. Form Lập phiếu nhập hàng (`frmThemPhieuNhap` - Đồng bộ 100% khuôn mẫu `frmThemPhieuKiemKho`):
- **Kích thước & Thiết lập**:
  - `ClientSize = (1582, 803)`, `MinimumSize = (1050, 680)`, font `Segoe UI, 9.5pt`, `KeyPreview = true`, `StartPosition = CenterScreen`.
- **Bố cục 4 khối chính**:
  1. **Panel thông tin bên phải (`pnlRight` - Width 400px, Dock = Right)**:
     - Viền ngăn cách `pnlRightBorder` (1px, màu `#CBD5E1`).
     - Khung `grpThongTinPhieu` (373 x 784):
       - Người nhập: ComboBox `cboNguoiNhap` (gán mặc định nhân viên đang đăng nhập từ `SessionContext`).
       - Thời gian: DateTimePicker `dtpNgayNhap` (hiển thị ngày giờ hiện tại).
       - Mã phiếu: TextBox `txtMaPhieu` (tự sinh mã `PN...`, chỉ đọc).
       - Nhà cung cấp: ComboBox `cboNhaCungCap` (kèm tìm kiếm tiếng Việt không dấu qua `ComboboxHelper.CaiDatTimKiem`).
       - Kho nhập: ComboBox `cboKhoNhap` (tự động khóa nếu tài khoản đang ở kho chi nhánh).
       - Số hóa đơn: TextBox `txtSoHoaDon`.
       - Ghi chú: TextBox `txtGhiChu` (đa dòng).
       - Thống kê tổng hợp: Số mặt hàng (`lblSoMatHangVal`), Tổng số lượng (`lblTongSoLuongVal`), Tổng tiền hàng (`lblTongTienHangVal`), Giảm giá (TextBox `txtGiamGia`), Tổng cộng (`lblTongCongVal`), Tiền đã trả (TextBox `txtDaTraNcc`), Cần trả NCC (`lblCanTraNccVal`).
       - Cụm 3 nút bấm ở đáy (Location: Y=732, Size: 100x34): `Bỏ qua` (`btnBoQua`) ➜ `Lưu tạm` (`btnLuuTam`) ➜ `Lưu` (`btnLuu`).
  2. **Panel chọn hàng bên trên (`pnlTop` - Height 150px, Dock = Top)**:
     - Khung `grpChonHang` (1182 x 125):
       - Tiêu đề: `Thêm phiếu nhập hàng` (Segoe UI, 12pt, Bold).
       - Tìm kiếm & chọn hàng: ComboBox `cboHangHoa` (Width: 450px) với `ComboboxHelper.CaiDatTimKiem`.
       - Nút `+ Thêm` (`btnThemHang`): Thêm hàng đã chọn vào lưới, tự động focus vào ô Số lượng để người dùng gõ số lượng.
       - Nút `Nhập file` (`btnNhapFile`): Nhập danh sách mặt hàng và số lượng từ file.
       - Nút `Hiện cột` (`btnHienCot`): Mở popup `frmHienCotHangNhap` để ẩn/hiện các cột dữ liệu.
  3. **Lưới dữ liệu trung tâm (`dgvChiTietHang` - Dock = Fill)**:
     - Cột: STT (ReadOnly, MiddleCenter), Mã hàng (ReadOnly, MiddleCenter), Tên hàng (ReadOnly, MiddleLeft), Đơn vị tính (ReadOnly, MiddleCenter), Số lượng (Editable, màu `#FEFCE8`, MiddleRight, định dạng `N0`), Đơn giá (ReadOnly, MiddleRight, `N0`), Giảm giá (ReadOnly, MiddleRight, `N0`), Thành tiền (ReadOnly, MiddleRight, `N0`).
     - Cho phép chỉnh sửa số lượng trực tiếp trên ô lưới; nhấn `Enter` commit dữ liệu và tự động tính toán lại thành tiền dòng cùng các chỉ số tài chính tổng hợp.
     - Nhấp đúp chuột (Double click) vào một dòng trên lưới hiển thị hộp thoại xác nhận xóa mặt hàng khỏi phiếu.
     - **Kiểm tra an toàn cột Số lượng**: Khi thêm hàng mới (`btnThemHang_Click`) hoặc nạp file (`btnNhapFile_Click`), nếu cột 'Số lượng' đang bị ẩn (`!colSoLuong.Visible`), hệ thống chặn thao tác và hiển thị `MessageBox.Show` cảnh báo người dùng bật lại cột trước khi thêm hàng, ngăn chặn hoàn toàn lỗi hệ thống unhandled exception `System.InvalidOperationException: Current cell cannot be set to an invisible cell`.
  4. **Panel phân trang ở đáy (`pnlPhanTrang` - Height 46px, Dock = Bottom)**:
     - Nút `btnTrangTruoc`, nhãn `lblTrangHienTai`, nút `btnTrangSau`.
     - Tùy chọn số dòng mỗi trang: ComboBox `cboSoDongMoiTrang` (10, 20, 50, 100 dòng).
     - Phân trang mượt mà qua `DataView.RowFilter` (`STT >= minSTT AND STT <= maxSTT`).
- **Lưu dữ liệu an toàn**:
  - Toàn bộ thao tác lưu phiếu tạm (`TrangThai = 'NHAP'`) và lưu phiếu hoàn thành (`TrangThai = 'DA_XAC_NHAN'`) đều bọc trong giao dịch ADO.NET `DataProcessing.ChangeDataTransaction`. Khi lưu hoàn thành, tự động cập nhật công nợ và tăng số lượng tồn kho qua View.
