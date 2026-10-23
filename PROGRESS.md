# Tiến độ dự án BTL-LTTQ-2

**Cập nhật lần cuối**: 2026-10-22 – Tái thiết kế toàn diện Form Thêm/Sửa Nhà cung cấp (`frmDonViCapHang`) chuẩn hóa theo Form Tạo hàng hóa (`frmHangHoa`) (Phiên 10)
**Cập nhật lần cuối**: 2026-10-23 – Dọn dẹp Code sạch, Chuẩn hóa 100% Font Segoe UI, Tái cấu trúc thư mục `Classes/` & Chốt Khuôn mẫu Thiết kế UI/UX toàn hệ thống (Phiên 7)

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
- [x] Hoàn thiện Form Danh sách Hàng hóa (`frmDanhSachHangHoa`): Kết nối dữ liệu thật từ SQL Server, phân trang, bộ lọc đa tiêu chí, tính tốc độ bán dự kiến hết hàng, tính động số lượng "Khách đặt" từ đơn đặt hàng bán, ẩn/hiện cột linh hoạt qua `frmHienCot`, nhập/xuất Excel.
- [x] Hoàn thiện Form Thêm/Sửa Hàng hóa (`frmHangHoa`): Quản lý đầy đủ thông tin sản phẩm, định mức tồn, vị trí và lưu trữ 5 ảnh đại diện.
- [x] Chốt phân hệ Danh mục Hàng hóa làm Khuôn mẫu Thiết kế Chuẩn (Design System) cho toàn bộ hệ thống.
- [ ] Chuyển sang phân hệ Xác thực & Phân quyền (`frmDangNhap`, tiện ích mã hóa mật khẩu `PasswordHelper.cs` SHA-256, liên kết phân quyền trên `frmMain`).

---

## Việc kế tiếp
1. **Xác thực & Phân quyền**: Hoàn tất `frmDangNhap`, tích hợp phân quyền MenuStrip và StatusStrip trên `frmMain`.
2. **Quản lý Danh mục (CRUD Master Data)**:
   - Form Danh mục Nhóm hàng & Đơn vị tính (`frmNhomHang`, `frmDonViTinh`).
   - Form Danh mục Chi nhánh Kho & phân quyền `TaiKhoanKho` (`frmKho`).
   - Form Danh mục Khách hàng (`frmKhachHang`).
1. **Xác thực & Phân quyền**: Hoàn tất `frmDangNhap` hỗ trợ chọn chi nhánh kho, tiện ích SHA-256 `PasswordHelper`, tích hợp phân quyền MenuStrip và StatusStrip trên `frmMain`.
2. **Quản lý Danh mục Cơ bản (Kế thừa khuôn mẫu Danh sách Hàng hóa)**:
   - Form Danh mục Nhóm hàng (`frmNhomHang`) & Đơn vị tính (`frmDonViTinh`).
   - Form Danh mục Vị trí (`frmViTri`).
   - Form Danh mục Chi nhánh Kho & phân quyền `TaiKhoanKho`.
   - Form Danh mục Nhà cung cấp (`frmNhaCungCap`).
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
| 5 | **Danh mục Hàng hóa** | `frmDanhSachHangHoa` & `frmHangHoa` (**Đã chốt làm Khuôn mẫu Thiết kế chuẩn UI/UX toàn hệ thống**) | Hoàn thành 100% |
| 6 | **Danh mục Cơ bản** | CRUD Nhóm hàng, Đơn vị tính, Chi nhánh Kho, Nhà cung cấp | Chưa bắt đầu |
| 7 | **Phiếu Nhập hàng** | `frmNhapHang`, tính giá vốn bình quân (MAC), cộng tồn dynamic | Chưa bắt đầu |
| 8 | **Kiểm kê & Cân bằng kho** | `frmKiemKho`, nạp tồn sổ sách, sinh phiếu điều chỉnh, khóa vĩnh viễn | Chưa bắt đầu |
| 9 | **Chuyển kho Liên chi nhánh** | `frmChuyenKho`, 2 bước Xuất kho ➜ Nhận kho, chặn tồn âm kho nguồn | Chưa bắt đầu |
| 10 | **Trả hàng nhập & Xuất hủy** | `frmTraHangNhap`, `frmXuatHuy`, kiểm tra tồn kho trước khi xuất | Chưa bắt đầu |
| 11 | **Báo cáo Tồn kho & Thẻ kho** | Báo cáo tồn đa kho, Thẻ kho từ `vw_BienDongKho`, xuất file Excel | Chưa bắt đầu |

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
