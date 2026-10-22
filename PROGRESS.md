# Tiến độ dự án BTL-LTTQ-2

**Cập nhật lần cuối**: 2026-10-22 – Khắc phục lỗi lưu hoàn thành / trả hàng, chuẩn hóa hiển thị số lượng số nguyên, đồng bộ hàng tiêu đề cột và trạng thái không in đậm (Phiên 12)

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
- [x] Tái thiết kế toàn diện Form Chi tiết Nhà cung cấp (`frmDonViCapHang`) theo form Tạo hàng hóa (`frmHangHoa`): nhãn đặt trên ô nhập, trường Địa chỉ chuyển thành ô nhiều dòng (Multiline), tối ưu hóa diện tích hiển thị (960x720).
- [x] Thiết kế và hiện thực Form Danh sách Đơn nhập hàng (`frmNhapHang`) với nút `+ Nhập hàng`, bộ lọc 100% ComboBox bên trái, tìm kiếm F3, phân trang SQL Server và xuất file CSV UTF-8 BOM.
- [x] Thiết kế Form Lập phiếu nhập hàng (`frmThemPhieuNhap`): gợi ý tự động mã phiếu, chọn NCC, kho, thêm danh sách hàng nhập, tính tổng tiền và công nợ, lưu tạm hoặc hoàn thành trong Transaction.
- [x] Thiết kế Form Chi tiết đơn nhập (`frmChiTietPhieuNhap`): mở khi nhấp đúp vào 1 đơn nhập trên lưới, hiển thị chi tiết header, bảng hàng và khối thanh toán theo ảnh mẫu; **chỉ giữ lại 3 nút chức năng: `Xóa`, `Xuất file`, `Trả hàng nhập`**.
- [x] Thiết kế Form Trả hàng nhập (`frmTraHangNhap`): mở từ nút `Trả hàng nhập` của form chi tiết; cho chọn số lượng trả kèm logic validation chặt chẽ (chặn âm, chặn vượt số lượng nhập, chặn vượt tồn kho hiện tại) và thuật toán tự động khấu trừ nợ đơn gốc hoặc xác định tiền NCC cần hoàn trả lại; lưu trong Transaction.
- [x] Bổ sung nhánh `TRA_HANG_NHAP` vào View `vw_BienDongKho` và cập nhật script `docs/database_update.sql`.
- [x] Sửa triệt để lỗi runtime `Invalid column name 'NoCanTraHienTai'` khi hoàn thành phiếu nhập hoặc thực hiện trả hàng: loại bỏ thao tác UPDATE cột tĩnh không tồn tại trong `DonViCapHang`, tuân thủ nguyên tắc công nợ NCC tính động từ chứng từ nhập.
- [x] Chuẩn hóa số lượng trong tất cả hộp thoại cảnh báo và thông báo về định dạng số nguyên `N0` (chặn đuôi `.00`).
- [x] Đồng bộ hàng tên cột (Column Header Row) của `dgvNhapHang` với `frmDanhSachHangHoa` và `frmNhaCungCap` (giữ VisualStyles chuẩn WinForms, không đổi màu nền phẳng hay đổi font in đậm; HeaderText `Mã nhà cung cấp`, `Tên nhà cung cấp`).
- [x] Bỏ in đậm trạng thái "Đã nhập hàng" trên cả DataGridView và Form Chi tiết đơn nhập hàng (chuyển sang font Regular).
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
   - Form Kiểm kê & Cân bằng kho (`frmKiemKho`) - Khóa vĩnh viễn phiếu sau khi cân bằng.
   - Form Chuyển kho liên chi nhánh (`frmChuyenKho`) - Luồng 2 bước Xuất chuyển / Nhận hàng.
   - Form Xuất hủy (`frmXuatHuy`).
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
| 8 | **Phiếu Nhập hàng** | `frmNhapHang`, `frmThemPhieuNhap`, `frmChiTietPhieuNhap`, `frmHienCotNhapHang` | Hoàn thành 100% |
| 9 | **Kiểm kê & Cân bằng kho** | `frmKiemKho`, nạp tồn sổ sách, sinh phiếu điều chỉnh, khóa vĩnh viễn | Chưa bắt đầu |
| 10 | **Chuyển kho Liên chi nhánh** | `frmChuyenKho`, 2 bước Xuất kho ➜ Nhận kho, chặn tồn âm kho nguồn | Chưa bắt đầu |
| 11 | **Trả hàng nhập & Xuất hủy** | `frmTraHangNhap` (hoàn tất 100%), `frmXuatHuy` (chưa bắt đầu) | Đang thực hiện |
| 12 | **Báo cáo Tồn kho & Thẻ kho** | Báo cáo tồn đa kho, Thẻ kho từ `vw_BienDongKho`, xuất file Excel | Chưa bắt đầu |

---

## Nhật ký phiên (Session Logs)

- **Phiên 12 (2026-10-22) – Khắc phục lỗi hoàn thành & trả hàng, chuẩn hóa số lượng số nguyên, đồng bộ hàng tiêu đề cột & bỏ in đậm trạng thái**:
  - **Nhu cầu**: Người dùng phản hồi:
    1. Số lượng ghi trong các thông báo vẫn là số thực (ví dụ `12.00` và `10.00`).
    2. Lưu tạm đã thành công, nhưng vẫn gặp lỗi runtime khi Hoàn thành đơn nhập và Trả hàng (`Invalid column name 'NoCanTraHienTai'`).
    3. Hàng tên cột cần đồng bộ với các form đã có, không được tự ý đổi.
    4. Trạng thái "Đã nhập hàng" không cần in đậm.
  - **Kết quả thực hiện**:
    1. **Khắc phục lỗi CSDL `NoCanTraHienTai`**:
       - Xóa bỏ hoàn toàn các câu lệnh UPDATE `DonViCapHang.NoCanTraHienTai` trong `frmThemPhieuNhap.cs`, `frmChiTietPhieuNhap.cs` và `frmTraHangNhap.cs`. Tuân thủ nguyên tắc cốt lõi: công nợ NCC được tính toán động từ chứng từ phiếu nhập (`PhieuNhap.CanTraNcc`), bảng `DonViCapHang` không lưu số nợ tĩnh.
    2. **Đồng bộ hiển thị số lượng nguyên trong thông báo**:
       - Cập nhật định dạng số lượng trong mọi `MessageBox.Show` (`frmTraHangNhap.cs`, `frmChiTietPhieuNhap.cs`) từ `:N2` sang `:N0` (ví dụ: cảnh báo vượt quá số lượng, xác nhận trả hàng, cảnh báo chặn âm tồn).
    3. **Đồng bộ hàng tên cột (Column Header Row)**:
       - Ở `frmNhapHang.cs` và `frmNhapHang.Designer.cs`: Bỏ cấu hình can thiệp style tiêu đề cột (`EnableHeadersVisualStyles = false`, bỏ màu nền phẳng `241, 245, 249`, bỏ font Bold, bỏ chiều cao custom), đưa về VisualStyles mặc định chuẩn Windows Forms 100% giống `frmDanhSachHangHoa` và `frmNhaCungCap`.
       - Đồng bộ độ rộng cột: tăng `colMaNcc.Width` từ `130` lên `140` (khớp hoàn toàn với `frmNhaCungCap`), loại bỏ hoàn toàn tình trạng ngắt dòng "Mã nhà cung cấp" làm đội chiều cao hàng tiêu đề từ 22px lên 39px; cập nhật `colChon.Width = 45` và `dgvNhapHang.RowHeadersWidth = 51`, đồng bộ font form về `Segoe UI 9.5pt` và `SelectionForeColor` về `Color.FromArgb(30, 41, 59)`. Kết quả hàng tiêu đề cột có chiều cao 22px gọn gàng, đồng bộ tuyệt đối với `frmDanhSachHangHoa` và `frmNhaCungCap`.
       - Chuẩn hóa tên cột tiêu đề: `Mã nhà cung cấp`, `Tên nhà cung cấp` (khớp hoàn toàn với `frmNhaCungCap`).
    4. **Bỏ in đậm trạng thái "Đã nhập hàng"**:
       - Trong `dgvNhapHang_CellFormatting`: Bỏ thiết lập `FontStyle.Bold` khi hiển thị trạng thái `Đã nhập hàng`, chỉ giữ màu xanh lá nhận diện chuẩn với font Regular như `frmDanhSachHangHoa`.
       - Trong `frmChiTietPhieuNhap.Designer.cs`: Chuyển font của `lblTrangThai` từ Bold sang Regular.
    5. **Kiểm tra biên dịch**: Roslyn compiler biên dịch mã nguồn C# thành công: **0 Warning(s), 0 Error(s)**.

- **Phiên 11 (2026-10-22) – Thiết kế Phân hệ Nhập hàng (`frmNhapHang`, `frmThemPhieuNhap`, `frmChiTietPhieuNhap`) & Form Trả hàng nhập (`frmTraHangNhap`)**:
  - **Nhu cầu**: Người dùng yêu cầu:
    1. Thiết kế `frmNhapHang` và các form liên quan, đặt tên nút thêm phiếu nhập là `+ Nhập hàng`. Toàn bộ filter bên trái dạng ComboBox theo khuôn mẫu đang có.
    2. Khi nhấn đúp vào 1 đơn nhập sẽ xuất hiện form chi tiết đơn nhập tham khảo ảnh 2 và 3.
    3. Ở form chi tiết đơn nhập, **chỉ giữ lại 3 nút**: `Xóa` (thay cho Hủy), `Xuất file`, `Trả hàng nhập`.
    4. Ở form trả hàng nhập cho chọn số lượng hàng cần trả (logic check không được âm hoặc vượt quá số lượng nhập, chặn vượt tồn hiện tại), tự động tính toán số tiền hàng dựa trên giá hàng để trừ vào số nợ cần trả hoặc số tiền nhà cung cấp cần trả lại.
    5. Rule quan trọng: Tuân theo thiết kế chung của các form đang có sẵn (100% tiếng Việt có dấu, `UseVisualStyleBackColor = true`, không tô màu tùy tiện).
  - **Kết quả thực hiện**:
    1. **Tạo mới 5 Form trong `GUI/Forms/NhapHang/`**:
       - `frmNhapHang.cs`, `.Designer.cs`, `.resx`: Form danh sách đơn nhập hàng, bộ lọc 100% ComboBox (`cboTrangThai`, `cboNhaCungCap`, `cboNguoiTao`, `cboNguoiNhap`, `grpThoiGianTao`), tìm kiếm tức thời, phân trang SQL Server, DataGridView đệm đôi chống giật lag, xuất file CSV UTF-8 BOM, sự kiện double-click mở chi tiết đơn.
       - `frmThemPhieuNhap.cs`, `.Designer.cs`, `.resx`: Form tạo mới phiếu nhập (nút `+ Nhập hàng`), gợi ý tự động mã `PN00000X`, chọn NCC, kho, thêm/xóa hàng nhập, tính tổng tiền, nợ, lưu tạm (`NHAP`) hoặc hoàn thành (`DA_XAC_NHAN`) trong Transaction. Khắc phục triệt để lỗi truy vấn `GiaNhap` thành `ISNULL(h.GiaVon, 0) AS GiaNhap`, sửa giá trị `LoaiPhieuNhap = 'THONG_THUONG'` tuân thủ CHECK constraint `CK_PhieuNhap_Loai`, đồng bộ số lượng hàng nhập thành số nguyên (`N0`, `DecimalPlaces = 0`).
       - `frmChiTietPhieuNhap.cs`, `.Designer.cs`, `.resx`: Form chi tiết đơn nhập hiển thị thông tin header, bảng hàng hóa, khối thanh toán chuẩn theo ảnh mẫu. Cung cấp đúng 3 nút: `Xóa` (xóa phiếu tạm hoặc chuyển trạng thái `HUY` cho phiếu đã xác nhận kèm kiểm tra chặn âm tồn), `Xuất file`, `Trả hàng nhập`. Đồng bộ số lượng hiển thị thành số nguyên (`N0`) và sửa các cột ghi nhật ký `NhatKyThaoTac`.
       - `frmTraHangNhap.cs`, `.Designer.cs`, `.resx`: Form trả hàng nhập với logic validation kiểm tra số lượng trả (không âm, không vượt số lượng nhập, không vượt tồn kho hiện tại) và thuật toán tự động khấu trừ vào công nợ đơn gốc hoặc tính số tiền NCC cần hoàn trả lại; lưu `PhieuTraHangNhap` và `ChiTietPhieuTraHangNhap` trong Transaction. Thiết kế ô nhập số lượng trả rõ ràng bằng hộp TextBox có viền nổi bật (`CellPainting`), hỗ trợ tự động bôi đen khi click (`SelectAll`) và chặn ký tự phi số; chuyển đổi số lượng sang số nguyên (`N0`) đồng bộ toàn hệ thống; chuẩn hóa các cột ghi `NhatKyThaoTac`.
       - `frmHienCotNhapHang.cs`, `.Designer.cs`, `.resx`: Form popup ẩn/hiện cột.
    2. **Đồng bộ Menu và Form MDI**:
       - Cập nhật `frmMain.Designer.cs` và `frmMain.cs` gắn sự kiện `mnuNhapHang_Click` kiểm tra kích hoạt hoặc mở form MDI `frmNhapHang`.
    3. **Đồng bộ CSDL**:
       - Cập nhật View `vw_BienDongKho` thêm nhánh `TRA_HANG_NHAP` giảm tồn kho tại `MaKho` khi `TrangThai = 'DA_TRA_HANG'`.
       - Bổ sung cấu trúc bảng `PhieuTraHangNhap` và `ChiTietPhieuTraHangNhap` vào `docs/database_update.sql`.
    4. **Đồng bộ tài liệu đặc tả**:
       - Cập nhật `docs/specs/nhap-hang.md` và `docs/specs/tra-hang-nhap.md`.
    5. **Kiểm tra biên dịch**:
       - Biên dịch MSBuild thành công: **0 Warning(s), 0 Error(s)**.

- **Phiên 10 (2026-10-22) – Tái thiết kế toàn diện Form Thêm/Sửa Nhà cung cấp (`frmDonViCapHang`) chuẩn hóa theo Form Tạo hàng hóa (`frmHangHoa`)**:
  - **Nhu cầu**: Người dùng phản hồi: "Phần frmDonViCapHang đang để trống khá nhiều không gian. Tôi muốn thiết kế giống với form Thêm hàng hóa này (trường địa chỉ nên để multiline)" kèm hình ảnh chụp form `frmHangHoa`.
  - **Nguyên nhân cốt lõi**:
    1. Form `frmDonViCapHang` ban đầu thiết kế với kích thước nhỏ `782x653`, nhãn đặt bên trái ô nhập (Left-aligned) khiến các trường bị dồn cục, để trống nhiều diện tích bên phải và bên dưới.
    2. Trường `txtDiaChi` trước đó chỉ là ô nhập 1 dòng (Single-line), gây khó khăn khi nhập địa chỉ chi tiết (số nhà, đường, phường/xã, quận/huyện).
  - **Kết quả thực hiện**:
    1. **Nâng cấp không gian & Bố cục chuẩn mực**: Mở rộng kích thước form lên `960x720` (vừa vặn, thoáng đãng trên cả màn hình HD lẫn Full HD). Đồng bộ chiều cao động (`610px` khi thêm mới, `705px` khi chỉnh sửa có khối công nợ).
    2. **Đồng bộ phong cách theo `frmHangHoa`**:
       - Toàn bộ nhãn (`Label`) được đưa lên TRÊN ô nhập liệu (`TextBox` / `ComboBox`), font `Segoe UI` rõ nét, hiện đại.
       - **Khối Thông tin cơ bản**:
         - Cột trái (400px): `Mã nhà cung cấp` (`txtMaNcc`), `Tên nhà cung cấp (*)` (`txtTenDonVi`), `Nhóm nhà cung cấp` (`cboNhomNcc`).
         - Cột phải (450px): `Mã số thuế` (`txtMaSoThue`), `Địa chỉ` (`txtDiaChi` - **Multiline**, chiều cao 99px, thanh cuộn `ScrollBars = Vertical`, cân xứng hoàn hảo với 2 hàng input bên trái tương tự ô `Mô tả` bên `frmHangHoa`).
       - **Khối GroupBox "Thông tin liên hệ" (`grpLienHe`)**: Tiêu đề in đậm, gồm 3 cột thẳng hàng: `Điện thoại`, `Email`, `Khu vực`.
       - **Khối GroupBox "Trạng thái & Ghi chú" (`grpGhiChu`)**: Tiêu đề in đậm, gồm `Trạng thái` và `Ghi chú` (Multiline).
       - **Khối GroupBox "Giao dịch & Công nợ" (`grpCongNo`)**: Tiêu đề in đậm, hiển thị khi chỉnh sửa hồ sơ.
       - **Thanh thao tác Bottom (`pnlBottom`)**: Nút `Bỏ qua` và `Lưu` chuẩn WinForms (`UseVisualStyleBackColor = true`), căn phải.
    3. **Kiểm tra biên dịch**: Chạy MSBuild thành công **0 Error(s)**.

- **Phiên 9 (2026-10-22) – Chuẩn hóa Header CheckBox VisualStyles, Xóa nút thêm nhóm ở `frmDonViCapHang` & Cân đối tỉ lệ nút ở `frmNhaCungCap`**:
  - **Nhu cầu**: Người dùng yêu cầu: (1) Ô chọn tất cả ở tiêu đề cột CheckBox phải có giao diện giống 100% các ô bên dưới; (2) Sửa lỗi ô chọn tất cả không hoạt động khi chưa chọn gì, và khi có dòng chọn nhấn vào thì bỏ chọn tất cả nhưng bị giữ lại dòng cuối cùng; (3) Xóa nút thêm nhóm ở trong `frmDonViCapHang`; (4) Điều chỉnh lại tỷ lệ nút thêm nhóm ở `frmNhaCungCap`.
  - **Kết quả thực hiện**:
    1. Sử dụng `CheckBoxRenderer.DrawCheckBox` chuẩn Windows VisualStyles.
    2. Khắc phục triệt để lỗi Chọn tất cả, giải phóng edit buffer.
    3. Xóa nút thêm nhóm ở `frmDonViCapHang`, mở rộng ComboBox thẳng hàng.
    4. Cân chỉnh `btnThemNhomNCC` về kích thước hình vuông hoàn hảo `29x29px`.
    5. Kiểm tra biên dịch: MSBuild thành công **0 Warning(s), 0 Error(s)**.
