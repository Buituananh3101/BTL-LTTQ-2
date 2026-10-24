# Tiến độ dự án BTL-LTTQ-2

**Cập nhật lần cuối**: 2026-10-24 – Bỏ tính năng in/xem thẻ kho trên 2 chức năng Báo cáo (Phiên 31)

---

## Đang làm
- [x] Bỏ nút `In thẻ kho` (`btnInTheKho`) và sự kiện `btnInTheKho_Click` trên Báo cáo thẻ kho (`frmBaoCaoTheKho`).
- [x] Bỏ nút `Xem thẻ kho` (`btnXemTheKho`) khỏi thanh công cụ `frmBaoCaoTonKho`, giữ nguyên tính năng nhấn đúp (`CellDoubleClick`) mở Thẻ kho của mặt hàng.
- [x] Căn chỉnh nhóm nút thao tác bên phải trên cả 2 form báo cáo đồng bộ chuẩn 100%: `btnXuatFile` (1465, 18), `btnHienCot` (1590, 18), `btnHuongDanSuDung` (1705, 18).
- [x] Kiểm tra biên dịch mã nguồn C# bằng MSBuild: **0 Warning(s), 0 Error(s)**.
- [ ] Tiện ích băm mật khẩu `Classes/PasswordHelper.cs` (SHA-256).
- [ ] Thiết kế và hoàn thiện Form Đăng nhập (`frmDangNhap`) hỗ trợ chọn chi nhánh kho làm việc.
- [ ] Chuyển sang phân hệ Xác thực & Phân quyền (`frmDangNhap`, tiện ích mã hóa mật khẩu `PasswordHelper.cs` SHA-256, liên kết phân quyền trên `frmMain`).

---

## Việc kế tiếp
1. **Xác thực & Phân quyền**: Hoàn tất `frmDangNhap` hỗ trợ chọn chi nhánh kho, tiện ích SHA-256 `PasswordHelper`, tích hợp phân quyền MenuStrip và StatusStrip trên `frmMain`.
2. **Quản lý Danh mục Cơ bản (Kế thừa khuôn mẫu Danh sách Hàng hóa)**:
   - Form Danh mục Nhóm hàng (`frmNhomHang`).
   - Form Danh mục Vị trí (`frmViTri`).
   - Form Danh mục Chi nhánh Kho & phân quyền `TaiKhoanKho` (`frmKho`).
   - Form Danh mục Khách hàng (`frmKhachHang`).
3. **Quản lý Kho & Chứng từ Biến động**:
   - Form Xuất hủy (`frmXuatHuy`).

---

## Bảng Tổng hợp Tiến độ Các Phân hệ (WBS Overview)

| STT | Phân hệ / Chức năng | Mô tả chi tiết | Trạng thái |
|:---:|---|---|:---:|
| 1 | **Khởi tạo Kiến trúc & CSDL** | Solution WinForms .NET 4.7.2, `DataProcessing.cs`, CSDL `QuanLyKhoPhuQuocLTW` | Hoàn thành 100% |
| 2 | **Quy chuẩn Vận hành AI** | Thư mục `.agent/rules/`, `AGENTS.md`, `PROGRESS.md`, `docs/` | Hoàn thành 100% |
| 3 | **Giao diện Khung MDI** | `frmMain` MDI Container, MenuStrip 5 phân hệ, StatusStrip trạng thái | Hoàn thành 100% |
| 4 | **Xác thực & Phân quyền** | `SessionContext`, SHA-256 `PasswordHelper`, `frmDangNhap`, phân quyền menu | Đang thực hiện |
| 5 | **Danh mục Hàng hóa** | `frmDanhSachHangHoa` & `frmHangHoa` (**Khuôn mẫu Thiết kế chuẩn UI/UX toàn hệ thống**) | Hoàn thành 100% |
| 6 | **Danh mục Nhà cung cấp** | `frmNhaCungCap`, `frmDonViCapHang`, `frmNhomNhaCungCap`, `frmHienCotNcc`, `frmNhapNhaCungCapFile` | Hoàn thành 100% |
| 7 | **Phiếu Nhập hàng** | `frmNhapHang`, `frmThemPhieuNhap`, `frmChiTietPhieuNhap`, `frmHienCotNhapHang` | Hoàn thành 100% |
| 8 | **Trả hàng nhập** | `frmTraHangNhap`, `frmThemPhieuTraHang`, `frmChiTietTraHangNhap`, `frmHienCotTraHang` | Hoàn thành 100% |
| 9 | **Danh mục Đơn vị tính** | `frmDonViTinh`, `frmChiTietDonViTinh`, `frmHienCotDvt`, tạo nhanh từ `frmHangHoa` | Hoàn thành 100% |
| 10 | **Danh mục Cơ bản khác** | CRUD Nhóm hàng, Vị trí, Chi nhánh Kho, Khách hàng | Chưa bắt đầu |
| 11 | **Kiểm kê & Cân bằng kho** | `frmKiemKho`, nạp tồn sổ sách, sinh phiếu điều chỉnh, khóa vĩnh viễn | Hoàn thành 100% |
| 12 | **Chuyển kho Liên chi nhánh & Đổi kho** | `frmChuyenKho`, 2 bước Xuất kho ➜ Nhận kho, chặn tồn âm kho nguồn, modal `frmDoiKho` | Hoàn thành 100% |
| 13 | **Xuất hủy** | `frmXuatHuy` | Chưa bắt đầu |
| 14 | **Báo cáo Tồn kho & Thẻ kho** | Báo cáo tồn đa kho (`frmBaoCaoTonKho`), Thẻ kho (`frmBaoCaoTheKho`) từ `vw_BienDongKho`, xuất CSV UTF-8 BOM | Hoàn thành 100% |

---

## Nhật ký phiên (Session Logs)

- **Phiên 31 (2026-10-24) – Bỏ nút in/xem thẻ kho trên 2 chức năng báo cáo, giữ nguyên thao tác nhấn đúp mở Thẻ kho**:
  - **Nhu cầu**: Người dùng yêu cầu bỏ tính năng in thẻ kho trong 2 chức năng báo cáo. Sau khi xác nhận cụ thể với người dùng qua câu hỏi làm rõ, hệ thống thực hiện: bỏ nút `In thẻ kho` trên form Báo cáo thẻ kho và bỏ nút `Xem thẻ kho` trên form Báo cáo tồn kho.
  - **Kết quả thực hiện**:
    1. **`frmBaoCaoTheKho` (Báo cáo thẻ kho)**:
       - Loại bỏ hoàn toàn control `btnInTheKho` ("In thẻ kho") và sự kiện `btnInTheKho_Click`.
       - Căn chỉnh lại nhóm nút thao tác bên phải của `grpTop`: `btnXuatFile` Location `(1465, 18)`, Size `(110, 29)`; `btnHienCot` Location `(1590, 18)`, Size `(95, 29)`; `btnHuongDanSuDung` Location `(1705, 18)`, Size `(43, 29)`.
        2. **`frmBaoCaoTonKho` (Báo cáo tồn kho)**:
       - Loại bỏ nút `btnXemTheKho` ("Xem thẻ kho") và sự kiện `btnXemTheKho_Click` khỏi thanh công cụ trên cùng.
       - Giữ nguyên sự kiện `dgvBaoCaoTonKho_CellDoubleClick` và phương thức `MoBaoCaoTheKho()`: khi người dùng nhấp đúp vào bất kỳ dòng hàng nào trên bảng, hệ thống tự động mở Báo cáo thẻ kho tương ứng của mặt hàng đó (kèm nút `Quay lại`).
       - Cập nhật lại nội dung `btnHuongDanSuDung_Click` hướng dẫn thao tác nhấp đúp xem thẻ kho.
       - Nhóm nút thao tác bên phải đồng bộ 100% tọa độ và kích thước với Báo cáo thẻ kho.
    3. **Kiểm tra biên dịch & Chuẩn định dạng**:
       - 100% file mã nguồn lưu định dạng **UTF-8 with BOM** (Điều luật 18).
       - Biên dịch MSBuild thành công: **0 Warning(s), 0 Error(s)**.
       - Đồng bộ đầy đủ `docs/specs/the-kho.md`, `docs/specs/bao-cao-ton-kho.md`, `docs/quyet-dinh.md` và `PROGRESS.md`.
- **Phiên 30 (2026-10-24) – Khắc phục vị trí nút `?` hướng dẫn bị tràn viền / che khuất trên `frmBaoCaoTheKho`**:
  - **Nhu cầu**: Người dùng phản hồi nút `?` hướng dẫn trên `frmBaoCaoTheKho` bị đè dưới tên chức năng "Báo cáo thẻ kho" và mất khỏi góc phải phía trên thanh công cụ.
  - **Nguyên nhân**: Trong `frmBaoCaoTheKho.Designer.cs`, cụm nút góc phải bị định vị quá xa về phía bên phải (`btnInTheKho: 1440`, `btnXuatFile: 1550`, `btnHienCot: 1660`, `btnHuongDanSuDung: 1765`), khiến nút `?` (`X=1765 + 43 = 1808 > 1782`) bị tràn ra ngoài biên phải của form (1782) và biến mất khỏi góc phải, đồng thời gây xung đột layout.
  - **Kết quả thực hiện**:
    1. **`frmBaoCaoTheKho.Designer.cs`**:
       - Căn chỉnh lại toàn bộ tọa độ nhóm nút bên phải của `grpTop` đồng bộ 100% với khuôn mẫu `frmBaoCaoTonKho`:
         + `btnInTheKho`: Location `(1370, 18)`, Size `(100, 29)`, Anchor `Top, Right`.
         + `btnXuatFile`: Location `(1480, 18)`, Size `(100, 29)`, Anchor `Top, Right`.
         + `btnHienCot`: Location `(1590, 18)`, Size `(95, 29)`, Anchor `Top, Right`.
         + `btnHuongDanSuDung`: Location `(1705, 18)`, Size `(43, 29)`, Anchor `Top, Right`.
       - Bảo đảm khoảng cách an toàn: Nút `?` nằm gọn ở góc trên cùng bên phải, cách lề phải 34px, không còn bất kỳ hiện tượng bị tràn viền hay đè lên tiêu đề `lblTieuDe` ("Báo cáo thẻ kho") ở bên trái (`X=24`).
    2. **Kiểm tra biên dịch & Chuẩn định dạng**:
       - Lưu file định dạng **UTF-8 with BOM** (Điều luật 18).
       - Biên dịch MSBuild thành công: **0 Warning(s), 0 Error(s)**.
       - Đồng bộ `PROGRESS.md` và `docs/specs/the-kho.md`.

- **Phiên 29 (2026-10-24) – Hoàn thiện nút "Quay lại" điều hướng 2 chiều giữa Báo cáo tồn kho và Báo cáo thẻ kho**:
  - **Nhu cầu**: Khắc phục trải nghiệm bất tiện khi người dùng nhấn đúp xem thẻ kho của mặt hàng từ Báo cáo tồn kho (`frmBaoCaoTonKho`), form Thẻ kho (`frmBaoCaoTheKho`) che kín màn hình nhưng không có nút quay lại, buộc phải chọn lại menu.
  - **Kết quả thực hiện (Phương án 1)**:
    1. **`frmBaoCaoTheKho`**:
       - Thêm nút `btnQuayLai` ("Quay lại") cạnh tiêu đề form "Báo cáo thẻ kho", tuân thủ Điều luật 14 và 15a (`UseVisualStyleBackColor = true`).
       - Nút này chỉ xuất hiện (`Visible = true`) khi mở từ Báo cáo tồn kho (`moTuTonKho = true`); khi mở độc lập từ MenuStrip thì nút này ẩn (`Visible = false`).
       - Hỗ trợ đa kênh quay về: Bấm nút `Quay lại`, nhấn phím `Escape`, hoặc bấm dấu `[X]` đóng cửa sổ con đều tự động kích hoạt `Activate()` đưa `frmBaoCaoTonKho` lên trên cùng và đóng cửa sổ thẻ kho.
    2. **`frmBaoCaoTonKho`**:
       - Truyền tham số `moTuTonKho: true` khi người dùng nhấn đúp vào dòng hàng hoặc nhấn nút `Xem thẻ kho`.
       - Bảo toàn 100% dữ liệu phân trang, dòng hàng đang chọn và bộ lọc, đồng thời tự động focus vào lưới DataGridView (`OnActivated`) khi quay lại để người dùng tiếp tục thao tác bằng bàn phím.
    3. **Kiểm tra biên dịch & Chuẩn định dạng**:
       - 100% file mã nguồn lưu định dạng **UTF-8 with BOM** (Điều luật 18).
       - Biên dịch MSBuild thành công **0 Warning(s), 0 Error(s)**.
       - Đồng bộ tài liệu đặc tả `docs/specs/the-kho.md`, `docs/specs/bao-cao-ton-kho.md`, sổ quyết định `docs/quyet-dinh.md` và `PROGRESS.md`.

- **Phiên 28 (2026-10-24) – Quét và khắc phục triệt để 4 lỗi phát sinh tại Phân hệ Chuyển kho và Phân hệ Báo cáo**:
  - **Nhu cầu**: Khắc phục 4 lỗi người dùng phản hồi qua ảnh:
    1. Ảnh 1: Lỗi khi chọn chức năng thêm phiếu chuyển kho: `Cannot find column [System.Data.DataRowView]`.
    2. Ảnh 2: Lỗi khi mở chức năng báo cáo tồn kho: `Invalid column name 'TonToiDa'`.
    3. Ảnh 3: Lỗi khi mở chức năng báo cáo thẻ kho: `Items collection cannot be modified when the DataSource property is set`.
    4. Ảnh 4: Lỗi khi tải báo cáo thẻ kho: `Invalid column name 'MaNCC'`, `'TenNCC'`, `'MaKH'`.
  - **Kết quả thực hiện**:
    1. **`frmThemPhieuChuyenKho.cs`**:
       - Thêm kiểm tra `int.TryParse(cboHangHoa.SelectedValue?.ToString(), out int maHang)` trước khi lọc `dtHangHoaAll.Select("MaHang = " + maHang)` trong `CapNhatTonHienTai()` và `btnThemHang_Click()`, loại bỏ dứt điểm ngoại lệ do `DataRowView`.
       - Bổ sung kiểm tra an toàn cho `cboKhoNguon` và `cboKhoDich`.
    2. **`frmBaoCaoTonKho.cs`**:
       - Sửa biểu thức truy vấn định mức tồn: Thay `ISNULL(ntk.TonCaoNhat, ISNULL(h.TonToiDa, 0))` bằng `ISNULL(ntk.TonCaoNhat, 0)` trong cả WHERE clause và SELECT list theo đúng cấu trúc bảng `NguongTonKho`.
    3. **`frmBaoCaoTheKho.cs`**:
       - Chuyển `ComboboxHelper.CaiDatTimKiem` cho `cboHangHoa` sang overload DataTable: `ComboboxHelper.CaiDatTimKiem(cboHangHoa, dtComboHang, "TenHienThi", "MaHang", ...)`, khắc phục triệt để lỗi thao tác `Items` khi ComboBox có `DataSource`.
       - Tuân thủ nghiêm ngặt Điều luật 17: Chạy câu lệnh `SELECT` kiểm tra trực tiếp từ `INFORMATION_SCHEMA.COLUMNS` trong SQL Server để xác thực 100% tên cột thực tế.
       - Chuẩn hóa các phép JOIN trong câu query thẻ kho: `pn.MaDonViCapHang`, `nccPn.TenDonVi`; `pth.MaDonViCapHang`, `nccPth.TenDonVi`; `px.MaDonBan -> DonBan.MaKh -> KhachHang.TenKH`.
    4. **Kiểm tra biên dịch & Chuẩn định dạng**:
       - 100% file mã nguồn lưu định dạng **UTF-8 with BOM** (Điều luật 18).
       - Biên dịch MSBuild thành công **0 Warning(s), 0 Error(s)**.
       - Đồng bộ đầy đủ `docs/specs/bao-cao-ton-kho.md`, `docs/quyet-dinh.md` và `PROGRESS.md`.
  - **Nhu cầu**: Thiết kế form cho 2 chức năng Báo cáo tồn kho và Báo cáo thẻ kho theo đúng nghiệp vụ của hệ thống dựa trên thiết kế giao diện của các form đã hoàn thiện.
  - **Kết quả thực hiện**:
    1. **Báo cáo Tồn kho (`frmBaoCaoTonKho`)**:
       - MDI child tỉ lệ chuẩn `1782x853` FixedSingle (`MaximizeBox = false`, `CenterScreen`).
       - Thanh công cụ trên: Ô tìm kiếm nhanh SKU/tên hàng/mã vạch, nút `Xem thẻ kho` (kích hoạt mở ngay thẻ kho của hàng đang chọn), nút `Xuất file` CSV UTF-8 with BOM, `Hiện cột`, `?`.
       - 4 Thẻ KPI tổng kết trên đầu bảng: Tổng số mặt hàng (SKU), Tổng số lượng tồn, Tổng giá trị tồn (Giá vốn × Tồn kho), Cần lưu ý (Hết hàng <= 0 hoặc dưới định mức).
       - Bộ lọc GroupBox bên trái: Chi nhánh kho (Tất cả / Kho cụ thể), Nhóm hàng (`ComboboxHelper.CaiDatTimKiem`), Tiêu chí mức tồn, Trạng thái kinh doanh; Cụm thời gian tạo `grpThoiGianTao` (2 DateTimePicker Short) **ở vị trí đáy** tuân thủ tuyệt đối Điều luật 16.
       - DataGridView chuẩn đệm đôi, tự động tô đỏ số lượng tồn khi <= 0 (Điều luật 15a), trạng thái kinh doanh màu xanh lá/xám nhạt (Điều luật 15b), phân trang đáy chuẩn.
    2. **Báo cáo Thẻ kho (`frmBaoCaoTheKho`)**:
       - MDI child tỉ lệ chuẩn `1782x853`, hỗ trợ mở độc lập hoặc khởi tạo nhận tham số `(int maHang, int maKho)` khi bấm "Xem thẻ kho" từ Báo cáo tồn kho hoặc Danh mục hàng hóa.
       - Banner hàng hóa chi tiết (SKU, Tên hàng, ĐVT, Nhóm, Vị trí, Giá vốn) kèm 4 thẻ KPI Thẻ kho: Tồn đầu kỳ ($T_{đầu}$), Tổng nhập (+), Tổng xuất (-), Tồn cuối kỳ ($T_{cuối}$).
       - Bộ lọc bên trái: ComboBox chọn mặt hàng (`ComboboxHelper.CaiDatTimKiem`), ComboBox kho, ComboBox loại chứng từ phát sinh, cụm thời gian phát sinh ở đáy theo Điều luật 16.
       - Bảng biến động thẻ kho: Dòng TỒN ĐẦU KỲ (highlight), các dòng phát sinh chứng từ (Phiếu nhập, Chuyển đến, Chuyển đi, Kiểm kê, Trả hàng, Xuất bán, Xuất hủy) kèm đối tác/diễn giải và tính tồn lũy kế theo thời gian, dòng TỒN CUỐI KỲ (highlight). Tô đỏ tồn lũy kế khi âm (< 0), phân biệt màu sắc chứng từ nhập (xanh lá) và xuất (slate).
       - Hỗ trợ nút `In thẻ kho` xem trước thông tin và `Xuất file` CSV UTF-8 with BOM.
    3. **Form tùy chọn hiển thị cột (`frmHienCotBaoCao`)**:
       - Popup linh hoạt cho phép bật/tắt hiển thị từng cột dữ liệu trên DataGridView, tự đóng khi click ra ngoài hoặc bấm Escape.
    4. **Tích hợp hệ thống & Kiểm tra biên dịch**:
       - Đăng ký sự kiện mở form trên `frmMain`: `mnuBaoCaoTonKho_Click` và `mnuBaoCaoTheKho_Click`.
       - Đăng ký đầy đủ các file form mới vào `BTL-LTTQ-2.csproj`.
       - Chuyển đổi 100% file mã nguồn sang **UTF-8 with BOM** (Điều luật 18).
       - Biên dịch MSBuild: **Build succeeded. 0 Warning(s), 0 Error(s)**.
       - Đồng bộ đầy đủ tài liệu đặc tả `docs/specs/bao-cao-ton-kho.md`, `docs/specs/the-kho.md` và sổ quyết định `docs/quyet-dinh.md`.


- **Phiên 26 (2026-10-24) – Thiết kế và hiện thực trọn vẹn 2 phân hệ Chuyển kho liên chi nhánh và Đổi kho làm việc**:
  - **Nhu cầu**: Thiết kế form cho 2 chức năng: Chuyển kho (ghi nhận các phiếu chuyển hàng giữa các kho với nhau, kèm các form phụ trợ liên quan) và Đổi kho (modal nhỏ chọn kho làm việc theo ảnh mẫu), tuân thủ nghiêm ngặt nghiệp vụ kho, phân quyền và design system chuẩn WinForms.
  - **Kết quả thực hiện**:
    1. **Form modal Đổi kho làm việc (`GUI/Forms/DoiKho/frmDoiKho`)**:
       - Khớp 100% giao diện ảnh mẫu (`media_1792806433137_dfddc08d.png`): Kích thước nhỏ gọn `430x170`, `FixedDialog`, tiêu đề "Chọn kho làm việc".
       - Nạp ComboBox kho từ CSDL: Nếu là Admin nạp toàn bộ kho đang hoạt động; nếu là Thủ kho/Nhân viên chỉ nạp các kho được gán quyền qua bảng `TaiKhoanKho`.
       - Khi người dùng bấm "Xác nhận", cập nhật `SessionContext.MaKhoHienTai`, `TenKhoHienTai`, tự động đổi tiêu đề `frmMain` kèm tên kho và làm mới toàn bộ Form con đang mở trong MDI.
    2. **Phân hệ Chuyển kho liên chi nhánh (`GUI/Forms/ChuyenKho/`)**:
       - `frmChuyenKho`: Màn hình danh sách chuẩn tỉ lệ `1782x853` FixedSingle, bộ lọc bên trái 100% ComboBox, cụm thời gian ở đáy theo Điều luật 16, DataGridView phân trang, lọc theo trạng thái (`DA_XUAT`, `DA_NHAN`, `NHAP`, `HUY`), hỗ trợ tìm kiếm tiếng Việt không dấu.
       - `frmThemPhieuChuyenKho`: Lập phiếu chuyển kho mới; chọn kho nguồn và kho đích (chặn chọn trùng kho); chọn hàng hóa nạp tồn sổ sách tức thời từ `vw_TonKhoHienTai`; chặn xuất chuyển vượt quá tồn thực tế (Luật 6); hỗ trợ phím tắt và thao tác mượt mà; các nút đáy sắp xếp chuẩn `Bỏ qua` ➜ `Lưu tạm` (`NHAP`) ➜ `Lưu` (`DA_XUAT`).
       - `frmChiTietChuyenKho`: Màn hình Master-Detail xem và cập nhật phiếu chuyển; hiển thị nhãn trạng thái dòng chữ màu (Điều luật 15b, 19a); nút "Nhận hàng" cho kho đích; nút "Xóa" tuân thủ Điều luật 15d & Luật 5 (chỉ xóa phiếu nháp, vô hiệu hóa khi phiếu đã hoàn tất/đã xuất); nút "Xuất file" CSV định dạng UTF-8 BOM.
       - `frmNhanChuyenKho`: Modal xác nhận nhận hàng tại chi nhánh đích; cho phép kiểm đếm và ghi nhận số lượng nhận thực tế `SoLuongNhan <= SoLuongChuyen`; phát hiện hao hụt (tô đỏ) và bắt buộc nhập lý do vào `GhiChuNhan`; khi xác nhận cập nhật trạng thái `DA_NHAN` và tự động cộng tồn kho đích qua View `vw_TonKhoHienTai`.
       - `frmHienCotChuyenKho`: Popup không viền tùy biến ẩn/hiện 10 cột dữ liệu bảng chuyển kho.
       - `frmNhapChuyenKhoFile`: Nạp danh sách hàng hóa chuyển từ file CSV, tải file mẫu CSV có dữ liệu thực tế, kiểm tra mã hàng và tồn kho gửi.
    3. **Tích hợp hệ thống & Kiểm tra biên dịch**:
       - Bổ sung `SessionContext.MaNV` (mặc định lấy NV đầu tiên trong CSDL nếu chưa đăng nhập).
       - Đăng ký sự kiện mở form trên `frmMain`: `mnuChuyenKho_Click` và `mnuDoiKho_Click`.
       - Đăng ký đầy đủ 14 file code vào `BTL-LTTQ-2.csproj`.
       - Chuyển đổi 100% file mã nguồn sang **UTF-8 with BOM** (Điều luật 18).
       - Biên dịch MSBuild: **Build succeeded. 0 Warning(s), 0 Error(s)**.
       - Đồng bộ đầy đủ tài liệu đặc tả `docs/specs/chuyen-kho.md` và sổ quyết định `docs/quyet-dinh.md`.


- **Phiên 25 (2026-10-24) – Khắc phục triệt để lỗi phát sinh Invalid column name 'MaDvtNoiBo' tại frmDonViTinh**:
  - **Nhu cầu**: Người dùng phản hồi lỗi phát sinh khi mở `frmDonViTinh`: `Invalid column name 'MaDvtNoiBo'`. Sau khi phân tích, người dùng yêu cầu: sử dụng trực tiếp cột `MaDVT` đã có sẵn trong bảng `DonViTinh` thay vì thêm cột mới, chỉ sửa lại query trong code.
  - **Kết quả thực hiện**:
    1. **Bảo toàn schema CSDL**: Giữ nguyên cấu trúc bảng `DonViTinh` (`MaDVT INT IDENTITY`, `TenDVT NVARCHAR(50)`, `MoTa NVARCHAR(255)`), loại bỏ đoạn code DDL tự động và script thêm cột `MaDvtNoiBo` trong `docs/database_update.sql`.
    2. **Chuẩn hóa câu lệnh SQL trong `frmDonViTinh.cs`**:
       - Thay thế `dvt.MaDvtNoiBo` bằng biểu thức định dạng hiển thị `('DVT' + RIGHT('000' + CAST(dvt.MaDVT AS VARCHAR(10)), 3)) AS MaHienThi`.
       - Lược bỏ `dvt.MaDvtNoiBo` khỏi các mệnh đề `GROUP BY` trong `sqlCount` và `sqlData`.
       - Cập nhật tìm kiếm: Hỗ trợ tìm kiếm theo tên ĐVT, mã số `MaDVT` hoặc mã định dạng `DVT00X`.
       - Sắp xếp mã ĐVT: Sắp xếp theo `dvt.MaDVT ASC`.
    3. **Chuẩn hóa xử lý trong form chi tiết `frmChiTietDonViTinh.cs`**:
       - Bỏ tham chiếu `MaDvtNoiBo`, nạp mã hiển thị dạng `DVT00X` từ `MaDVT`.
       - Khi thêm mới: Tự sinh mã gợi ý `DVT00X` (dựa trên `MAX(MaDVT) + 1`); cho phép người dùng xem và tùy chỉnh; kiểm tra chống trùng lặp mã; hỗ trợ lưu tự tăng chuẩn hoặc `SET IDENTITY_INSERT DonViTinh ON` khi chỉ định ID tùy biến; bọc lưu trong Transaction.
       - Khi xem/sửa: Hiển thị mã `DVT00X` và khóa ô mã (`ReadOnly = true`, nền xám nhạt) nhằm bảo vệ tính toàn vẹn khóa chính và liên kết khóa ngoại với `HangHoa`.
    4. **Kiểm tra biên dịch & Chuẩn định dạng**:
       - 100% file mã nguồn lưu định dạng **UTF-8 with BOM** (Điều luật 18).
       - Biên dịch MSBuild thành công **0 Warning(s), 0 Error(s)**.
       - Đồng bộ tài liệu `docs/specs/don-vi-tinh.md`, `docs/quyet-dinh.md` và `PROGRESS.md`.

- **Phiên 24 (2026-10-24) – Nâng cấp trải nghiệm & Tối ưu Phân hệ Đơn vị tính theo phản hồi người dùng**:
  - **Nhu cầu**:
    1. Bỏ cột số thứ tự (`colSTT`) không cần thiết trong `frmDonViTinh`.
    2. Chuyển nút Xóa của đơn vị tính từ ngoài `frmDonViTinh` vào form chi tiết `frmChiTietDonViTinh`, vị trí đặt các nút và mẫu các nút như trong form Cập nhật thông tin hàng hóa (`frmHangHoa`).
    3. Mã đơn vị tính tự động sinh nhưng vẫn hiện ra cho người dùng xem và có thể sửa như form Cập nhật thông tin hàng hóa (có check trùng mã, nếu trùng yêu cầu nhập lại).
  - **Kết quả thực hiện**:
    1. **Loại bỏ cột STT trên `frmDonViTinh`**: Xóa `colSTT` khỏi DataGridView `dgvDonViTinh` (cả Designer và xử lý `RowPostPaint`), danh sách gọn gàng hơn, không thừa cột.
    2. **Chuyển nút Xóa vào form chi tiết `frmChiTietDonViTinh`**:
       - Xóa nút `btnXoa` khỏi thanh công cụ trên cùng của `frmDonViTinh`, căn chỉnh lại nút `btnThemDonViTinh`.
       - Trong `frmChiTietDonViTinh`: Bố trí nút `btnXoa` ở góc trái thanh đáy `pnlBottom` (`Location = (20, 11)`), các nút `btnBoQua` (`Location = (464, 11)`) và `btnLuu` (`Location = (572, 11)`) ở góc phải theo đúng khuôn mẫu `frmHangHoa`.
       - Trạng thái nút Xóa: Khi thêm mới (`maDvt == 0`), ẩn nút xóa (`Visible = false`); khi xem/sửa (`maDvt > 0`), hiện nút xóa (`Visible = true`).
       - Ràng buộc toàn vẹn dữ liệu: Khi bấm Xóa, kiểm tra số lượng mặt hàng liên kết trong `HangHoa`. Nếu có hàng hóa liên kết (`count > 0`), CHẶN XÓA kèm thông báo cảnh báo chi tiết. Nếu 0 mặt hàng liên kết, hiển thị hộp thoại xác nhận và thực hiện xóa bọc trong Transaction kèm ghi nhật ký thao tác `NhatKyThaoTac`.
    3. **Cho phép xem & sửa mã đơn vị tính**:
       - Bổ sung cột `MaDvtNoiBo NVARCHAR(50) NULL` vào bảng `DonViTinh` (tuân thủ 3 Nguyên tắc vàng CSDL trong `docs/database_update.sql` và phương thức bảo đảm tự động `BaoDamCotMaDvtNoiBo()`).
       - Chuyển `txtMaDvt` sang chế độ cho phép nhập (`ReadOnly = false`, nền trắng `BackColor = White`, `MaxLength = 50`).
       - Khi thêm mới: Tự sinh mã gợi ý `DVT00X` (kiểm tra không trùng với các mã đang có) và nạp vào `txtMaDvt`.
       - Khi lưu (`btnLuu_Click`): Kiểm tra bắt buộc nhập cả mã và tên ĐVT; kiểm tra trùng mã `MaDvtNoiBo` với các đơn vị tính khác (nếu trùng hiển thị cảnh báo và yêu cầu nhập lại); kiểm tra trùng tên `TenDVT`; thực hiện lưu cả mã và tên bọc trong Transaction.
    4. **Kiểm tra biên dịch & Chuẩn định dạng**:
       - 100% file mã nguồn lưu định dạng **UTF-8 with BOM** (Điều luật 18).
       - Biên dịch MSBuild thành công **0 Warning(s), 0 Error(s)**.
       - Cập nhật đầy đủ `docs/specs/don-vi-tinh.md`, `docs/quyet-dinh.md` và `PROGRESS.md`.


- **Phiên 23 (2026-10-24) – Thiết kế và hiện thực trọn vẹn Phân hệ Đơn vị tính (GUI/Forms/DonViTinh/) theo chuẩn nghiệp vụ và Design System hệ thống**:
  - **Nhu cầu**: Thiết kế frm Đơn vị tính và các form liên quan theo đúng nghiệp vụ của hệ thống dựa trên mẫu giao diện đã thống nhất từ các frm đã hoàn thành trước đó.
  - **Kết quả thực hiện**:
    1. **Tạo mới 3 Form trong thư mục `GUI/Forms/DonViTinh/`**:
       - `frmDonViTinh`: Form danh sách MDI child kế thừa chuẩn tỉ lệ `frmDanhSachHangHoa` (1782x853, FixedSingle, MaximizeBox = false, StartPosition = CenterScreen). Thanh công cụ trên cùng gồm: ô tìm kiếm tức thời (theo mã hoặc tên), nút `+ Đơn vị tính`, nút `Xóa`, `Xuất file` CSV UTF-8 BOM, `Hiện cột`, `Hướng dẫn sử dụng`. Bộ lọc ComboBox bên trái gồm: Trạng thái sử dụng (Tất cả / Đang có hàng hóa / Chưa có hàng hóa), Sắp xếp, cụm thời gian `grpThoiGianTao` ở đáy (Điều luật 16). DataGridView chuẩn đệm đôi, Header CheckBox chọn tất cả, màu trạng thái 3 nhóm (Đang sử dụng xanh lá, Chưa sử dụng xám nhạt theo Điều luật 15b), phân trang đáy chuẩn.
       - `frmChiTietDonViTinh`: Form Modal Dialog Thêm mới / Chỉnh sửa đơn vị tính (684x520). Tự sinh mã định dạng `DVT00X`, kiểm tra bắt buộc tên ĐVT, chống trùng lặp tên; hiển thị danh sách các mặt hàng đang áp dụng ĐVT này (kèm phân loại nhóm và giá bán) để quản lý/thủ kho dễ dàng nắm bắt trước khi sửa/đổi; thứ tự nút ở đáy chuẩn Điều luật 19b (`Bỏ qua` ➜ `Lưu`), bọc lưu dữ liệu và nhật ký trong `DataProcessing.ChangeDataTransaction`.
       - `frmHienCotDvt`: Form popup không viền tùy chọn ẩn/hiện các cột của bảng Đơn vị tính, tự đóng khi click ra ngoài hoặc bấm Escape.
    2. **Ràng buộc nghiệp vụ bảo vệ toàn vẹn dữ liệu**:
       - Khi xóa đơn vị tính: Kiểm tra tự động số lượng mặt hàng đang liên kết trong bảng `HangHoa`. Nếu có hàng hóa đang áp dụng, hệ thống lập tức CHẶN XÓA và đưa ra thông báo cảnh báo chi tiết từng mặt hàng để bảo vệ toàn vẹn dữ liệu. Chỉ cho phép xóa các ĐVT chưa có hàng hóa nào liên kết.
    3. **Tích hợp liên kết phân hệ**:
       - Bổ sung nút tạo nhanh ĐVT `btnThemDonViNhanh` ("+") ngay cạnh `cboDonVi` trên `frmHangHoa`: Bấm nút mở ngay `frmChiTietDonViTinh`, sau khi lưu tự động nạp lại danh sách ĐVT và chọn ngay ĐVT mới tạo.
       - Gắn sự kiện `mnuDonViTinh_Click` trên thanh MenuStrip của `frmMain`: Mở form con MDI hoặc kích hoạt nếu form đã mở.
    4. **Bảo đảm chuẩn mã nguồn & biên dịch**:
       - Đăng ký đầy đủ các file form mới trong `BTL-LTTQ-2.csproj`.
       - 100% file mã nguồn C# lưu dưới định dạng **UTF-8 with BOM** (Điều luật 18).
       - Kiểm tra biên dịch bằng MSBuild thành công **0 Warning(s), 0 Error(s)**.
- **Phiên 22 (2026-10-24) – Chuẩn hóa hiển thị nhãn trạng thái dòng chữ màu (bỏ khung badge) và thống nhất thứ tự nút Bỏ qua - Lưu tạm - Lưu (Ban hành Điều luật 19)**:
  - **Nhu cầu**:
    1. Phần ghi đánh dấu các trạng thái phiếu không làm thành 1 phần có khung như ảnh 1 mà chỉ là dòng chữ có màu của trạng thái như trong mẫu `frmChiTietPhieuNhap` (không sửa vào `frmDanhSachHangHoa`).
    2. Thống nhất toàn bộ các nút chức năng nhỏ được để ở cuối form (mẫu như `frmThemPhieuNhap`), không để lên đầu như ảnh 2.
    3. Chốt thứ tự các nút là `Bỏ qua`, `Lưu tạm` và `Lưu` cho toàn bộ các form (thêm vào rule).
  - **Kết quả thực hiện**:
    1. **Chuẩn hóa nhãn trạng thái (Điều luật 19a)**:
       - `frmChiTietKiemKho.Designer.cs`: Bỏ `BackColor`, `Padding(8, 3, 8, 3)`, chuyển `lblTrangThaiBadge` sang font `Segoe UI, 10pt Regular`, chỉ là dòng chữ hiển thị màu trạng thái tự nhiên theo mẫu `frmChiTietPhieuNhap`.
       - `frmChiTietKiemKho.cs`: Bỏ các dòng gán `BackColor`, cập nhật màu `ForeColor` theo 3 nhóm chuẩn: "Đã cân bằng" xanh lá `Color.FromArgb(22, 163, 74)`, "Phiếu tạm" vàng amber `Color.FromArgb(217, 119, 6)`, "Đã hủy" xám nhạt `Color.FromArgb(100, 116, 139)`.
    2. **Thống nhất vị trí & thứ tự nút chức năng nhỏ ở cuối form (Điều luật 19b)**:
       - `frmThemPhieuKiemKho`:
         + Xóa bỏ các nút `[Nhập file]`, `[Lưu tạm]`, `[Cân bằng kho]`, `[Đóng]` khỏi vùng trên (`pnlTop`), vùng trên chỉ còn nhãn tiêu đề form "Thêm phiếu kiểm kho" theo chuẩn `frmThemPhieuNhap`.
         + Chuyển nút `Nhập file` sang thanh chọn hàng nhanh `pnlThemHang` (bên cạnh nút "Nạp tất cả hàng trong kho").
         + Thiết lập thanh nút chức năng đáy `pnlActions` (Dock = Bottom) với đúng thứ tự chốt: `Bỏ qua` (`btnBoQua`) ➜ `Lưu tạm` (`btnLuuTam`) ➜ `Lưu` (`btnCanBangKho`). Gán `CancelButton = btnBoQua`.
       - `frmThemPhieuTraHang`: Đổi thứ tự nút đáy từ `Lưu tạm`, `Trả hàng`, `Bỏ qua` sang đúng thứ tự chuẩn: `Bỏ qua` (`btnBoQua`) ➜ `Lưu tạm` (`btnLuuTam`) ➜ `Lưu` (`btnXacNhanTra`).
       - `frmThemPhieuNhap`: Cập nhật text nút hành động sang `Lưu` (`btnHoanThanh`) để thống nhất bộ ba `Bỏ qua` ➜ `Lưu tạm` ➜ `Lưu`.
       - `frmNhomNhaCungCap`: Đổi thứ tự 2 nút đáy sang chuẩn `Bỏ qua` ➜ `Lưu`.
    3. **Ban hành Điều luật 19 trong quy chuẩn Agent**: Cập nhật `AGENTS.md` (Điều luật 19), `.agent/rules/03-giao-dien-layout.md` (Mục 6.5), `docs/quyet-dinh.md` và `docs/specs/kiem-kho.md`.
    4. **Bảo đảm chuẩn mã nguồn & biên dịch**: 100% file mã nguồn lưu định dạng **UTF-8 with BOM**, kiểm tra biên dịch MSBuild thành công **0 Warning(s), 0 Error(s)**.

- **Phiên 21 (2026-10-24) – Cố định tỉ lệ form khởi đầu theo frmDanhSachHangHoa, bỏ phím tắt F, chuẩn hóa màu sắc trạng thái 3 nhóm**:
  - **Nhu cầu**:
    1. Form khởi đầu tất cả các chức năng lớn (`frmDanhSachHangHoa`, `frmKiemKho`, `frmNhapHang`, `frmTraHangNhap`, `frmNhaCungCap`) khi mở ra đều có tỉ lệ cố định như `frmDanhSachHangHoa` (ảnh 1), khóa không cho phóng to/mở rộng, không fill full màn hình `frmMain`.
    2. Loại bỏ hoàn toàn các phím tắt hàng F (F9, F10...) khỏi chức năng và giao diện nút bấm (ảnh 2).
    3. Chuẩn hóa màu sắc trạng thái: Đã hoàn thành/hoạt động tô màu xanh lá (theo mẫu `frmNhaCungCap`), Phiếu tạm tô màu vàng (theo mẫu `frmChiTietTraHangNhap`), Đã hủy tô màu xám nhạt (theo mẫu `frmTraHangNhap`).
  - **Kết quả thực hiện**:
    1. **Cố định kích thước & tỉ lệ form khởi đầu**:
       - Trong `frmMain.cs`: Xóa bỏ lệnh `frm.WindowState = FormWindowState.Maximized;` khi mở `frmKiemKho`, để form mở ở trạng thái `Normal` với kích thước cố định.
       - Chuẩn hóa thuộc tính form cho `frmKiemKho`, `frmNhapHang`, `frmTraHangNhap`, `frmNhaCungCap`, `frmDanhSachHangHoa`: `ClientSize = new System.Drawing.Size(1782, 853);`, `FormBorderStyle = FormBorderStyle.FixedSingle;`, `MaximizeBox = false;`, `StartPosition = FormStartPosition.CenterScreen;`.
    2. **Loại bỏ phím tắt hàng F**:
       - `frmThemPhieuKiemKho.Designer.cs`: Đổi text nút `btnLuuTam` thành `"Lưu tạm"`, `btnCanBangKho` thành `"Cân bằng kho"`.
       - `frmThemPhieuKiemKho.cs`: Loại bỏ hoàn toàn sự kiện bắt phím `Keys.F9` và `Keys.F10`.
    3. **Chuẩn hóa màu sắc trạng thái 3 nhóm**:
       - `frmTraHangNhap.cs`: Đổi màu "Phiếu tạm" từ xanh dương sang vàng amber `Color.FromArgb(217, 119, 6)`.
       - `frmChiTietTraHangNhap.cs`: Đổi màu "Đã hủy" từ đỏ sang màu nhạt hơn `Color.FromArgb(100, 116, 139)`.
       - `frmNhapHang.cs`: Bổ sung xử lý đổi màu cột `colTrangThai` trong `dgvNhapHang_CellFormatting` ("Đã nhập hàng"/"Đã nhận" xanh lá `Color.FromArgb(22, 163, 74)`, "Phiếu tạm" vàng amber `Color.FromArgb(217, 119, 6)`, "Đã hủy" xám nhạt `Color.FromArgb(100, 116, 139)`).
       - `frmChiTietPhieuNhap.cs`: Đổi màu "Phiếu tạm" từ xanh dương sang vàng amber `Color.FromArgb(217, 119, 6)`.
       - `frmKiemKho.cs`: Bổ sung xử lý đổi màu cột `colTrangThai` trong `dgvKiemKho_CellFormatting` ("Đã cân bằng" xanh lá `Color.FromArgb(22, 163, 74)`, "Phiếu tạm" vàng amber `Color.FromArgb(217, 119, 6)`, "Đã hủy" xám nhạt `Color.FromArgb(100, 116, 139)`).
    4. **Bảo đảm chuẩn mã nguồn**: Toàn bộ 11 file chỉnh sửa đều được lưu ở định dạng **UTF-8 with BOM**, kiểm tra biên dịch CoreCompile thành công **0 Warning(s), 0 Error(s)**.

- **Phiên 20 (2026-10-24) – Chuẩn hóa toàn bộ màu chữ, nút bấm, biểu tượng, bộ lọc và phông chữ toàn hệ thống (Cập nhật Điều luật 15, 16)**:
  - **Nhu cầu**:
    1. Cấm tự ý tô màu chữ trong bất kỳ form nào (như mã phiếu PKK00018 bị tô xanh, nhãn tổng kết bị tô xanh/đỏ khi chưa có lệch). Chỉ tô đỏ đối với các chênh lệch hoặc bất thường (lệch kiểm kho tăng/giảm khi khác 0, tồn kho <= 0).
    2. Nút Xóa khi chứng từ không đủ điều kiện xóa (đã cân bằng, đã hủy, đã xác nhận): chỉ disable nút (`btnXoa.Enabled = false`), giữ nguyên text là "Xóa", không đổi text sang "Không thể xóa" hay "Đã hủy".
    3. Cấm tự ý chèn icon/emoji vào nút bấm (loại bỏ 🔒, 🗑 khỏi các nút bấm).
    4. Khắc phục triệt để lỗi phông chữ / mojibake tại các form danh sách khác (`frmNhaCungCap`, `frmNhapHang`, `frmTraHangNhap`), phục hồi các text bị hỏng trong ComboBox như `-- Tất cả các nhóm --`, `-- Tất cả nhà cung cấp --`, `-- Tất cả người tạo/nhập --`.
    5. Đưa bộ lọc khoảng thời gian tạo (`grpThoiGianTao`) về cuối cùng (ở đáy) của vùng bộ lọc bên trái trên toàn bộ các form danh sách.
  - **Kết quả thực hiện**:
    1. **Chuẩn hóa màu chữ**:
       - `frmThemPhieuKiemKho`: Xóa bỏ `ForeColor` xanh dương của `lblMaPhieuVal`; đưa các nhãn tổng kết `lblTongThucTeVal`, `lblTongLechTangVal`, `lblTongLechGiamVal`, `lblTongChenhLechVal` về màu mặc định, chỉ tô đỏ `Color.Red` (Bold) khi có chênh lệch khác 0; bổ sung sự kiện `dgvHangKiem_CellFormatting` tô đỏ cột chênh lệch khi khác 0.
       - `frmChiTietKiemKho`: Đưa `lblMaPhieu` và các nhãn tổng kết về màu mặc định, chỉ tô đỏ khi có chênh lệch; bổ sung sự kiện `dgvChiTiet_CellFormatting` tô đỏ cột chênh lệch khi khác 0.
       - `frmKiemKho`: Bổ sung sự kiện `dgvKiemKho_CellFormatting` tô đỏ các cột tổng chênh lệch và số lượng lệch tăng/giảm khi khác 0.
       - Loại bỏ màu xanh dương tùy tiện tại các nhãn của `frmThemPhieuTraHang.Designer.cs`, `frmChiTietTraHangNhap.Designer.cs` và màu trạng thái tùy tiện trong `frmNhapHang.cs`.
    2. **Chuẩn hóa nút Xóa**:
       - Ở `frmChiTietKiemKho`, nút `btnXoa` luôn giữ nguyên text là `"Xóa"`. Khi phiếu đã cân bằng hoặc đã hủy, chỉ gán `btnXoa.Enabled = false`, không thay đổi Text.
    3. **Loại bỏ icon trên nút bấm**:
       - Xóa bỏ toàn bộ emoji/icon `🔒`, `🗑`, `✕` khỏi `btnXoa`, `colXoa` trên tất cả các form.
    4. **Khắc phục lỗi phông chữ & mojibake**:
       - Phục hồi chính xác tiếng Việt có dấu cho ComboBox: `-- Tất cả các nhóm --` (`frmNhaCungCap.cs`), `-- Tất cả nhà cung cấp --`, `-- Tất cả người tạo --`, `-- Tất cả người nhập --` (`frmNhapHang.cs`).
       - Chuẩn hóa font các form `frmThemPhieuNhap`, `frmChiTietPhieuNhap`, `frmTraHangNhap` về chuẩn `Segoe UI, 9.5pt`.
       - Bảo đảm 100% file mã nguồn C# (`.cs`, `.Designer.cs`) được lưu dưới định dạng **UTF-8 with BOM**.
    5. **Chuẩn hóa vị trí bộ lọc thời gian tạo**:
       - Sắp xếp lại `grpBoLoc` trên `frmKiemKho.Designer.cs`: đưa `grpThoiGianTao` xuống vị trí dưới cùng (y = 256), đẩy các ComboBox Trạng thái (y = 52), Người tạo (y = 120), Chi nhánh kho (y = 188) lên trên. Đồng bộ 100% với `frmDanhSachHangHoa`, `frmNhaCungCap`, `frmNhapHang`, `frmTraHangNhap`.
    6. **Cập nhật quy chuẩn thiết kế**: Cập nhật Điều luật 15 và 16 trong `AGENTS.md` và `.agent/rules/03-giao-dien-layout.md`.
    7. **Kiểm tra biên dịch**: Chạy MSBuild thành công **0 Warning(s), 0 Error(s)**.

- **Phiên 19 (2026-10-24) – Chuẩn hóa toàn diện giao diện Phân hệ Kiểm kho & Bộ phông chữ theo chuẩn frmDanhSachHangHoa (Ban hành Điều luật 18)**:
  - **Nhu cầu**:
    1. Loại bỏ cột đánh dấu sao (★) và nút "Làm mới bộ lọc" ở phần filter bên trái vì không tuân thủ mẫu; kiểm tra và xác nhận không có cột mới nào trong CSDL bị tạo thừa.
    2. Sửa kích thước form không đúng mẫu, các cột hiển thị bị tràn ngang phải kéo scroll, và cột `SL thực tế` đang bị co bẹp thu hẹp không thể nhìn thấy.
    3. Khắc phục triệt để lỗi font chữ tiếng Việt (kể cả trên thanh MenuStrip của `frmMain` và chuỗi `(Tồn: X)` trong combobox hàng hóa). Lấy font chữ ở `frmDanhSachHangHoa` làm chuẩn duy nhất.
    4. Thêm rule mới (Điều luật 18) vào quy chuẩn thiết kế: lấy font chữ ở `frmDanhSachHangHoa` làm chuẩn duy nhất toàn hệ thống (`Segoe UI, 9.5pt` cho form/controls, `Segoe UI, 12pt Bold` cho tiêu đề lớn).
  - **Kết quả thực hiện**:
    1. **Loại bỏ cột sao và nút làm mới**: Xóa bỏ `colDaDanhDau` và `btnLamMoi` trên giao diện `frmKiemKho.Designer.cs`, xóa bỏ logic sao trong câu query SQL `TaiDuLieu` của `frmKiemKho.cs`. Xác nhận schema CSDL không bị thay đổi.
    2. **Khắc phục tràn ngang và co bẹp cột**:
       - Cấu hình kích thước form `frmKiemKho`: `ClientSize = new System.Drawing.Size(1782, 853);` đồng bộ với `frmDanhSachHangHoa`.
       - Trong `frmMain.cs`, gán `frm.WindowState = FormWindowState.Maximized;` khi mở `frmKiemKho` để form tự bung toàn màn hình MDI cha.
       - Cấu hình lại các cột `dgvKiemKho`: Đặt `MinimumWidth = 85`, `Width = 95` cho `colSlThucTe` để chấm dứt tình trạng co bẹp cột; `colGhiChu` đặt `AutoSizeMode = Fill`, `MinimumWidth = 120`. Tất cả các cột hiển thị trọn vẹn không cần cuộn ngang trên màn hình chuẩn.
    3. **Khắc phục lỗi font chữ & mojibake**:
       - Khôi phục 100% text tiếng Việt có dấu chuẩn xác cho toàn bộ MenuStrip và StatusStrip trên `frmMain.Designer.cs`.
       - Sửa câu query SQL string concat nạp sản phẩm trong `frmThemPhieuKiemKho.cs` với tiền tố `N` (`N'['`, `N' (Tồn: '`, `N')'`) để không bị SQL Server convert sang `VARCHAR` gây lỗi `T?n:`.
       - Chuyển toàn bộ 12 file `.cs` và `.Designer.cs` sang mã hóa **UTF-8 with BOM** và cấu hình `<CodePage>65001</CodePage>` trong file project `.csproj`.
    4. **Ban hành Điều luật 18**: Bổ sung vào `AGENTS.md` và `.agent/rules/03-giao-dien-layout.md`.
    5. **Kiểm tra biên dịch**: Chạy build MSBuild thành công **0 Warning(s), 0 Error(s)**.

- **Phiên 18 (2026-10-24) – Thiết kế và hiện thực trọn vẹn Phân hệ Kiểm kho (`GUI/Forms/KiemKho/`) theo Design System & chuẩn giao diện KiotViet**:
  - **Nhu cầu**:
    1. Thiết kế `frmKiemKho` và các form liên quan, đặt tên nút thêm là `+ Kiểm kho`.
    2. Khi nhấn đúp vào 1 phiếu kiểm kho sẽ xuất hiện form chi tiết như trong ảnh 3.
    3. Ở form chi tiết, chỉ giữ lại các nút Hủy (thay bằng Xóa) và Xuất file. Khóa vĩnh viễn phiếu đã cân bằng theo Luật 6.
    4. Ở form thêm phiếu kiểm kho, cho người dùng được chọn từ các sản phẩm có trong danh mục hàng hóa và có nút chức năng nhập file ở trên giống `frmDanhSachHangHoa` (tạo file mẫu để minh họa quy chuẩn nhập và cho phép người dùng tải file mẫu giống như `frmDanhSachHangHoa`).
    5. Đồng bộ giao diện và bố cục theo quyết định Phiên 5 và 17 điều luật cốt lõi trong `AGENTS.md`.
  - **Kết quả thực hiện**:
    1. **Tạo 5 Form mới trong `GUI/Forms/KiemKho/`**:
       - `frmKiemKho`: Danh sách MDI child, thanh công cụ (`+ Kiểm kho`, `Xuất file`, `Hiện cột`), GroupBox bộ lọc bên trái 100% ComboBox (`cboTrangThai`, `cboNguoiTao` hỗ trợ tìm kiếm text qua `ComboboxHelper.CaiDatTimKiem`, `cboKho`), cụm thời gian chuẩn `grpThoiGianTao` (2 DateTimePicker Short), phân trang đáy.
       - `frmThemPhieuKiemKho`: Mở từ nút `+ Kiểm kho`, master panel thông tin phiếu, detail panel chọn hàng nhanh từ danh mục hàng hóa của kho, nạp toàn bộ hàng trong kho, sửa trực tiếp trên ô lưới `SoLuongThucTe`, tự tính chênh lệch và giá trị lệch realtime. Có nút `Nhập file` ở trên, nút `Lưu tạm (F9)` và `Cân bằng kho (F10)`. Luồng cân bằng kho sinh `PhieuDieuChinh` `DA_DUYET` và các dòng `ChiTietPhieuDieuChinh`, cập nhật sổ sách tức thời qua `vw_TonKhoHienTai` và khóa vĩnh viễn phiếu.
       - `frmChiTietKiemKho`: Form chi tiết khi nhấn đúp vào 1 phiếu kiểm kho (chuẩn bố cục ảnh 3). Hiển thị đầy đủ thông tin Header (Mã phiếu, Badge trạng thái, Người tạo/Ngày tạo, Người cân bằng/Ngày cân bằng, Tên kho), bảng chi tiết dòng hàng (Mã hàng, Tên hàng, ĐVT, Tồn kho, Thực tế, SL lệch, Đơn giá, Giá trị lệch), vùng ghi chú và vùng tổng kết 4 chỉ số (Tổng thực tế, Tổng lệch tăng, Tổng lệch giảm, Tổng chênh lệch). Ở thanh dưới chỉ giữ lại 2 nút: **Xóa** (khóa không cho xóa phiếu đã cân bằng theo Luật 6, cho phép xóa phiếu tạm `NHAP` kèm xác nhận) và **Xuất file** (kèm nút Đóng).
       - `frmNhapKiemKhoFile`: Form nhập file kiểm kê hỗ trợ .xlsx, .xls, .csv, có nút tải file mẫu Excel (.xls) và file mẫu CSV (.csv) kèm dữ liệu mẫu thực tế từ DB; validate từng dòng và nạp danh sách hợp lệ vào `frmThemPhieuKiemKho`.
       - `frmHienCotKiemKho`: Form tùy chọn ẩn/hiện cột bảng kiểm kho.
    2. **Đăng ký sự kiện mở form trên `frmMain`**: Đăng ký `mnuKiemKho_Click` mở form `frmKiemKho` trong MDI Container (hoặc `Activate()` nếu form đã mở).
    3. **Đăng ký file vào `BTL-LTTQ-2.csproj`**.
    4. **Đồng bộ đặc tả `docs/specs/kiem-kho.md` và `docs/quyet-dinh.md`**.
    5. **Kiểm tra biên dịch**: Chạy build MSBuild thành công **0 Warning(s), 0 Error(s)**.
  - **Nhu cầu**: 
    1. Vẫn tồn tại lỗi khi truy vấn database: `Invalid column name 'MaPhieuNhap'` khi xem chi tiết phiếu trả hàng nhập trong `frmTraHangNhap`. Cho phép dùng query SELECT để kiểm tra cấu trúc schema thực tế và quét chuẩn hóa toàn bộ code.
    2. Thêm rule mới vào agent: Khi tạo truy vấn mới tới database, nếu chưa rõ về thuộc tính/tên cột/giá trị cho phép thì phải xin cấp quyền truy vấn SELECT để kiểm tra, nghiêm cấm sửa database khi chưa được phép và tuyệt đối không được đoán bừa.
    3. Chuẩn hóa tất cả các bộ lọc ComboBox nhiều giá trị ở mọi form danh sách (trừ frmDanhSachHangHoa làm chuẩn) sang hỗ trợ nhập text và lọc hiển thị linh hoạt theo mẫu của `frmDanhSachHangHoa`.
  - **Kết quả thực hiện**:
    1. **Khắc phục lỗi CSDL `MaPhieuNhap` trong `frmChiTietTraHangNhap.cs`**:
       - Bỏ `pth.MaPhieuNhap` và `LEFT JOIN PhieuNhap pn ON ...` vì bảng `PhieuTraHangNhap` trong schema CSDL thực tế không có cột này.
       - Hiển thị mã đơn nhập gốc là "Trả tự do".
       - Xóa bỏ trường và warning biên dịch liên quan.
    2. **Ban hành Điều luật 17**:
       - Cập nhật `AGENTS.md` (Điều luật 17), `.agent/rules/05-co-so-du-lieu.md` (Mục 5) và `docs/quyet-dinh.md`.
       - Rà soát xác nhận không có bất kỳ xung đột nào với 16 điều luật trước đó.
    3. **Nâng cấp `ComboboxHelper.cs`**:
       - Xử lý loại trừ dòng `-- Tất cả ... --` khỏi kết quả tìm kiếm khi người dùng gõ ký tự (ví dụ gõ 'a' không còn bị chọn nhầm `-- Tất cả nhà cung cấp --`).
       - Đồng bộ logic chọn và khôi phục danh sách đầy đủ khi Enter/Click/Leave.
    4. **Chuẩn hóa bộ lọc ComboBox trên các form danh sách**:
       - `frmTraHangNhap`: Tối ưu tìm kiếm và lọc dữ liệu cho Nhà cung cấp, Người tạo, Người trả.
       - `frmNhapHang`: Chuyển ComboBox Nhà cung cấp, Người tạo, Người nhập sang `ComboboxHelper.CaiDatTimKiem`, hỗ trợ gõ text tìm kiếm.
       - `frmNhaCungCap`: Chuyển ComboBox Nhóm nhà cung cấp sang `ComboboxHelper.CaiDatTimKiem`, hỗ trợ gõ text tìm kiếm.
    5. **Kiểm tra biên dịch**: Chạy build MSBuild thành công **0 Warning(s), 0 Error(s)**.

- **Phiên 16 (2026-10-24) – Quét toàn diện và khắc phục triệt để các lỗi truy vấn cơ sở dữ liệu phân hệ Trả hàng nhập theo Phương án 1**:
  - **Nhu cầu**: Người dùng phản hồi rất nhiều lỗi khi thao tác phân hệ Trả hàng nhập (lỗi `HoTen`, lỗi `HOAT_DONG` sang int, lỗi `GiaNhap`). Yêu cầu quét toàn bộ, phân tích nguyên nhân và thực hiện sửa lỗi theo Phương án 1 (chuẩn hóa câu lệnh SQL trong C#, giữ nguyên schema CSDL).
  - **Kết quả thực hiện**:
    1. **`frmTraHangNhap.cs`**: Chuẩn hóa ComboBox Người tạo (`cboNguoiTao`) và Người trả (`cboNguoiTra`) bằng `TaiKhoan tk LEFT JOIN NhanVien nv ON tk.MaNV = nv.MaNV`, lấy `ISNULL(nv.HoTen, tk.TenDangNhap)` và lọc `TrangThai = 'HOAT_DONG' OR TrangThai = 'ACTIVE' OR TrangThai IS NULL`.
    2. **`frmThemPhieuTraHang.cs`**:
       - Sửa `NapDanhSachKho`: Đổi `WHERE TrangThai = 1` thành `WHERE TrangThai = 'HOAT_DONG' OR TrangThai = 'ACTIVE'`.
       - Sửa `NapDanhSachNhaCungCap`: Đổi `WHERE TrangThai = 1` thành `WHERE TrangThai = 'HOAT_DONG' OR TrangThai = 'ACTIVE' OR TrangThai IS NULL`.
       - Sửa `NapDanhSachNhanVien`: Đổi sang kết nối `TaiKhoan tk LEFT JOIN NhanVien nv` lấy đúng họ tên và mã tài khoản `MaTaiKhoan`.
       - Sửa `NapDanhSachHangHoa`: Thay `ISNULL(h.GiaNhap, ...)` bằng `ISNULL(h.GiaVon, 0) AS GiaNhap` và lọc hàng hóa kinh doanh chuẩn.
       - Chuẩn hóa ghi log `NhatKyThaoTac` với các cột chuẩn: `(MaTaiKhoan, ThoiDiem, ThaoTac, LoaiDoiTuong, MaDoiTuong, NoiDung)`.
    3. **`frmChiTietTraHangNhap.cs`**:
       - Sửa truy vấn thông tin chi tiết: Bổ sung kết nối sang bảng `NhanVien` (`nvLap`, `nvTra`) để lấy tên người lập và người trả phiếu.
       - Chuẩn hóa ghi log xóa/hủy vào bảng `NhatKyThaoTac`.
    4. **Kiểm tra biên dịch**: Chạy build MSBuild thành công **0 Warning, 0 Error**.

- **Phiên 15 (2026-10-23) – Chuẩn hóa bộ lọc `frmTraHangNhap` theo chuẩn `frmDanhSachHangHoa`, khắc phục lỗi Menu MDI, bổ sung Điều luật 16 vào hệ thống quy chuẩn Agent**:
  - **Nhu cầu**: 
    1. Khi click "Trả hàng nhập" từ menuStrip của `frmMain` không có form nào hiện ra.
    2. `frmTraHangNhap` chưa đúng form chuẩn `frmDanhSachHangHoa`: filter Trạng thái chưa phải comboBox, nút Hiển thị cột không đúng mẫu chuẩn.
    3. Thêm rules về thiết kế vào Agent: các filter ở groupBox bên trái thống nhất toàn bộ là comboBox (cho phép nhập và tìm kiếm text đối với các trường nhiều giá trị như vị trí hay nhóm hàng của `frmDanhSachHangHoa`), trừ phần chọn thời gian lấy `frmDanhSachHangHoa` làm chuẩn duy nhất.
  - **Kết quả thực hiện**:
    1. **Khắc phục kết nối menu `frmMain`**: Đăng ký `this.mnuTraHangNhap.Click += new System.EventHandler(this.mnuTraHangNhap_Click);` trong `frmMain.Designer.cs`. Nhấp chọn trên menu lập tức kích hoạt form MDI `frmTraHangNhap`.
    2. **Tái thiết kế & Lập trình `frmTraHangNhap`**:
       - Filter Trạng thái: Chuyển thành `cboTrangThai` với các giá trị `-- Tất cả trạng thái --`, `Đã trả hàng`, `Phiếu tạm`, `Đã hủy`.
       - Nút "Hiện cột": Chuẩn hóa kích thước `95x29` và Text "Hiện cột" khớp chuẩn top bar `frmDanhSachHangHoa`.
       - Tích hợp tìm kiếm text ComboBox: Áp dụng `ComboboxHelper.CaiDatTimKiem` cho `cboNhaCungCap`, `cboNguoiTao`, `cboNguoiTra` (tìm kiếm tiếng Việt không dấu, tự động chọn khi Enter, scrollbar tối đa 8 dòng).
       - Cụm thời gian: Đồng bộ 100% theo `grpThoiGianTao` (`322x108`) với 2 `DateTimePicker` `dtpTuNgay`, `dtpDenNgay` (Format = Short `dd/MM/yyyy`).
       - Cập nhật logic lọc SQL an toàn với `SqlParameter` cho cả bộ lọc bảng và chức năng xuất file CSV UTF-8 BOM.
    3. **Bổ sung Điều luật 16 vào quy chuẩn Agent**:
       - Cập nhật `AGENTS.md` (Điều luật 16), `.agent/rules/03-giao-dien-layout.md` (Mục 6.4), `docs/quyet-dinh.md` và `docs/specs/tra-hang-nhap.md`.
    4. **Kiểm tra biên dịch**: Chạy build thành công **0 Warning, 0 Error**.

- **Phiên 14 (2026-10-23) – Thiết kế và hiện thực trọn vẹn Phân hệ Trả hàng nhập (`GUI/Forms/TraHangNhap/`) theo Design System & chuẩn giao diện KiotViet**:
  - Tạo mới 4 Form: `frmTraHangNhap`, `frmThemPhieuTraHang`, `frmChiTietTraHangNhap` (chỉ 2 nút: Xóa và Xuất file), `frmHienCotTraHang`. Bổ sung cột `MaPhieuNhap INT NULL` vào bảng `PhieuTraHangNhap`.
