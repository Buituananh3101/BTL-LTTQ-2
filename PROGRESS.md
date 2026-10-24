# Tiến độ dự án BTL-LTTQ-2

**Cập nhật lần cuối**: 2026-10-24 – Chuẩn hóa hiển thị nhãn trạng thái chữ màu (bỏ khung badge) và thống nhất vị trí & thứ tự nút chức năng nhỏ (Bỏ qua ➜ Lưu tạm ➜ Lưu) ở cuối form (Phiên 22)

---

## Đang làm
- [x] Chuẩn hóa hiển thị nhãn trạng thái chứng từ: dạng dòng chữ có màu của trạng thái (Xanh lá / Vàng amber / Xám nhạt), không làm khung/badge có viền, nền màu hay padding theo mẫu `frmChiTietPhieuNhap` (sửa `frmChiTietKiemKho`).
- [x] Thống nhất toàn bộ các nút chức năng nhỏ đưa về cuối form (không để lên đầu như ảnh 2), lấy khuôn mẫu từ `frmThemPhieuNhap`.
- [x] Chốt thứ tự các nút chức năng nhỏ toàn hệ thống: `Bỏ qua` ➜ `Lưu tạm` ➜ `Lưu` (Ban hành Điều luật 19).
- [x] Áp dụng đồng bộ thứ tự và vị trí nút bấm cho `frmThemPhieuKiemKho`, `frmThemPhieuTraHang`, `frmThemPhieuNhap`, `frmNhomNhaCungCap`.
- [x] Kiểm tra biên dịch mã nguồn C# bằng MSBuild: **0 Warning(s), 0 Error(s)**.
- [ ] Tiện ích băm mật khẩu `Classes/PasswordHelper.cs` (SHA-256).
- [ ] Thiết kế và hoàn thiện Form Đăng nhập (`frmDangNhap`) hỗ trợ chọn chi nhánh kho làm việc.
- [ ] Chuyển sang phân hệ Xác thực & Phân quyền (`frmDangNhap`, tiện ích mã hóa mật khẩu `PasswordHelper.cs` SHA-256, liên kết phân quyền trên `frmMain`).

---

## Việc kế tiếp
1. **Xác thực & Phân quyền**: Hoàn tất `frmDangNhap` hỗ trợ chọn chi nhánh kho, tiện ích SHA-256 `PasswordHelper`, tích hợp phân quyền MenuStrip và StatusStrip trên `frmMain`.
2. **Quản lý Danh mục Cơ bản (Kế thừa khuôn mẫu Danh sách Hàng hóa)**:
   - Form Danh mục Nhóm hàng (`frmNhomHang`) & Đơn vị tính (`frmDonViTinh`).
   - Form Danh mục Vị trí (`frmViTri`).
   - Form Danh mục Chi nhánh Kho & phân quyền `TaiKhoanKho` (`frmKho`).
   - Form Danh mục Khách hàng (`frmKhachHang`).
3. **Quản lý Kho & Chứng từ Biến động**:
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
| 5 | **Danh mục Hàng hóa** | `frmDanhSachHangHoa` & `frmHangHoa` (**Khuôn mẫu Thiết kế chuẩn UI/UX toàn hệ thống**) | Hoàn thành 100% |
| 6 | **Danh mục Nhà cung cấp** | `frmNhaCungCap`, `frmDonViCapHang`, `frmNhomNhaCungCap`, `frmHienCotNcc`, `frmNhapNhaCungCapFile` | Hoàn thành 100% |
| 7 | **Phiếu Nhập hàng** | `frmNhapHang`, `frmThemPhieuNhap`, `frmChiTietPhieuNhap`, `frmHienCotNhapHang` | Hoàn thành 100% |
| 8 | **Trả hàng nhập** | `frmTraHangNhap`, `frmThemPhieuTraHang`, `frmChiTietTraHangNhap`, `frmHienCotTraHang` | Hoàn thành 100% |
| 9 | **Danh mục Cơ bản khác** | CRUD Nhóm hàng, Đơn vị tính, Chi nhánh Kho, Khách hàng | Chưa bắt đầu |
| 10 | **Kiểm kê & Cân bằng kho** | `frmKiemKho`, nạp tồn sổ sách, sinh phiếu điều chỉnh, khóa vĩnh viễn | Hoàn thành 100% |
| 11 | **Chuyển kho Liên chi nhánh** | `frmChuyenKho`, 2 bước Xuất kho ➜ Nhận kho, chặn tồn âm kho nguồn | Chưa bắt đầu |
| 12 | **Xuất hủy** | `frmXuatHuy` | Chưa bắt đầu |
| 13 | **Báo cáo Tồn kho & Thẻ kho** | Báo cáo tồn đa kho, Thẻ kho từ `vw_BienDongKho`, xuất file Excel | Chưa bắt đầu |

---

## Nhật ký phiên (Session Logs)

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
