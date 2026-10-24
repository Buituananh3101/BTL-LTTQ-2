# Sổ Quyết định Kỹ thuật & Nghiệp vụ (Architecture & Business Decisions)
## Hệ Thống Quản Lý Kho Hàng BTL-LTTQ-2 (Windows Forms C#)

> **Mục đích**: Ghi lại mọi quyết định kiến trúc, công nghệ và nghiệp vụ đã được thống nhất để các phiên làm việc tiếp theo không hỏi lại hoặc làm mâu thuẫn với các quyết định trước. Mỗi dòng ghi theo định dạng: `[Ngày] – [Quyết định] – [Lý do]`.

---

- **2026-10-21 (Phiên 1)** – Chọn nền tảng Windows Forms (.NET Framework 4.7.2) & C# – Phù hợp với môn học Lập trình trực quan (LTTQ), tận dụng thư viện đồ họa và controls WinForms tiêu chuẩn.
- **2026-10-21 (Phiên 1)** – Sử dụng kiến trúc giao diện MDI (Multiple Document Interface) với `FormMain` làm form cha – Cho phép mở đồng thời nhiều form nghiệp vụ (Hàng hóa, Nhập hàng, Kiểm kho, Báo cáo) trong một cửa sổ duy nhất, quản lý tab/child forms tập trung.
- **2026-10-21 (Phiên 1)** – Sử dụng ADO.NET thuần qua lớp tiện ích `DAL/DataProcessing.cs` – Không dùng EF Core nhằm đáp ứng yêu cầu môn học LTTQ, kiểm soát chặt chẽ hiệu năng câu truy vấn SQL, hỗ trợ SqlDataAdapter, DataTable, SqlTransaction.
- **2026-10-21 (Phiên 1)** – Kế thừa và sử dụng chung CSDL `QuanLyKhoPhuQuocLTW` từ dự án Web (`BTL-LTW-QuanLiKho-2`) – Tận dụng toàn bộ cấu trúc bảng, khóa ngoại, dữ liệu mẫu và các View nghiệp vụ đã chuẩn hóa 100%.
- **2026-10-21 (Phiên 1)** – Tính toán tồn kho động qua View `vw_TonKhoHienTai` và `vw_BienDongKho` – Không lưu tồn kho tĩnh trong bảng để loại bỏ rủi ro lệch số liệu sổ sách và giải quyết triệt để tranh chấp dữ liệu.
- **2026-10-21 (Phiên 1)** – Áp dụng 3 "Nguyên tắc vàng" CSDL: (1) Chỉ cộng thêm không phá; (2) Quản lý script migration có phiên bản; (3) Báo cáo trước khi chạm CSDL.
- **2026-10-21 (Phiên 1)** – Xác thực tài khoản bằng mật khẩu băm SHA-256 – Đồng bộ hoàn toàn với hệ thống web và dữ liệu mẫu trong `DuLieuMau.sql`, tuyệt đối không lưu plain-text password.
- **2026-10-21 (Phiên 1)** – Quản lý phiên làm việc người dùng qua lớp tĩnh `SessionContext` – Lưu trữ thông tin tài khoản đang đăng nhập, vai trò, chi nhánh kho hiện tại để áp dụng phân quyền menu và lọc dữ liệu.
- **2026-10-21 (Phiên 1)** – Phiếu kiểm kho đã cân bằng (`DA_DIEU_CHINH`) KHÔNG bao giờ được phép hủy hoặc xóa – Sau khi cân bằng, số liệu tồn đã được chốt và phục vụ các giao dịch tiếp theo. Nếu sai lệch bắt buộc lập phiếu kiểm mới.
- **2026-10-21 (Phiên 1)** – Mọi chứng từ đã hoàn thành (`DA_XAC_NHAN`, `DA_NHAN`, `DA_DUYET`) không được sửa/xóa, chỉ cho phép HỦY – Cập nhật `TrangThai = 'HUY'` để View tồn kho tự động loại trừ.
- **2026-10-21 (Phiên 1)** – Quản lý hàng hóa và tồn kho theo 1 Đơn vị tính cơ bản duy nhất – Tránh phát sinh số lẻ tồn kho và quy đổi phức tạp.
- **2026-10-21 (Phiên 1)** – Toàn bộ truy vấn SQL có tham số nhập từ giao diện BẮT BUỘC dùng `SqlParameter` – Ngăn chặn tuyệt đối lỗ hổng SQL Injection.
- **2026-10-21 (Phiên 2)** – Nguyên tắc phát triển tinh gọn: "Làm đến đâu, thiếu gì thêm đó" – AI tuyệt đối không tự ý tạo trước các lớp trừu tượng (BaseForm, Helper dư thừa...) khi chưa có yêu cầu cụ thể, đảm bảo người dùng nắm quyền kiểm soát 100% từng bước phát triển của dự án.
- **2026-10-21 (Phiên 2)** – Cấu hình PerMonitorV2 High DPI qua app.manifest và App.config – Khắc phục triệt để hiện tượng vỡ nét/mờ chữ toàn hệ thống mà không cần chỉnh sửa từng file form.
- **2026-10-21 (Phiên 3)** – Quy tắc đặt tên biến Controls: Tiền tố + Đúng theo Text hiển thị – Tên biến bám sát chữ trên nhãn/nút (bỏ dấu và ký tự đặc biệt, PascalCase): ví dụ `+ Hàng hóa` ➜ `btnHangHoa`, `Nhập file` ➜ `btnNhapFile`, `?` ➜ `btnHuongDanSuDung`, DataGridView ➜ `dgv` + Tên đối tượng. Tuyệt đối giữ nguyên 100% layout, vị trí, kích thước người dùng tự thiết kế.
- **2026-10-21 (Phiên 3)** – Chuẩn hóa tên form chính MDI thành `frmMain` – Đồng bộ tiền tố `frm` xuyên suốt toàn bộ các Form trong dự án (`frmMain`, `frmDanhSachHangHoa`, `frmDangNhap`...).
- **2026-10-22 (Phiên 4)** – Kiến trúc Phân hệ Nhà cung cấp: Thiết lập cụm form `GUI/Forms/NhaCungCap/` gồm `frmNhaCungCap` (danh sách chính), `frmDonViCapHang` (thêm/sửa hồ sơ), `frmNhomNhaCungCap` (tạo nhanh nhóm) và `frmHienCotNcc` (tùy chọn cột). Toàn bộ bộ lọc bên trái dùng 100% ComboBox – Đồng bộ cấu trúc tổ chức file với `DanhSachHangHoa`, bám sát thiết kế giao diện KiotViet, lọc linh hoạt và thao tác nhanh chóng trên WinForms.
- **2026-10-23 (Phiên 5)** – Chốt phân hệ Danh mục Hàng hóa làm Khuôn mẫu Thiết kế UI/UX cho toàn bộ dự án – Toàn bộ các form chức năng sau (Nhập kho, Kiểm kho, Chuyển kho, Danh mục...) bắt buộc tuân thủ 100% quy chuẩn: Phông `Segoe UI` (Base 9.5pt, Tiêu đề 12pt Bold), màu xen kẽ DataGridView (`#FFFFFF` & `#F8FAFC`, Selection `#E0E7FF`), bố cục 3 khối (Lọc bên trái + Lưới trung tâm + Phân trang đáy).
- **2026-10-23 (Phiên 5)** – Tinh giản cấu trúc thư mục thành `Classes/` & `GUI/` – Bỏ các thư mục trung gian `BLL/` và `DAL/` chưa có DTO/Service độc lập để đưa `DataProcessing.cs`, `SessionContext.cs`, `ComboboxHelper.cs` vào chung thư mục `Classes/`, phù hợp với mô hình đồ án LTTQ tinh gọn, trực quan và dễ tiếp cận cho người mới học.
- **2026-10-23 (Phiên 14)** – Thiết lập phân hệ Trả hàng nhập độc lập (`GUI/Forms/TraHangNhap/`) gồm cụm 4 Form: `frmTraHangNhap` (danh sách), `frmThemPhieuTraHang` (thêm mới từ danh mục hàng hóa), `frmChiTietTraHangNhap` (chi tiết theo ảnh mẫu KiotViet, chỉ giữ lại nút Xóa và Xuất file) và `frmHienCotTraHang` (tùy chọn cột) – Bám sát Design System Phiên 5 & 13, đảm bảo chặn âm kho và tự động giảm tồn kho qua `vw_BienDongKho`.
- **2026-10-23 (Phiên 14)** – Bổ sung cột `MaPhieuNhap INT NULL` vào bảng `PhieuTraHangNhap` – Hỗ trợ liên kết mã đơn nhập gốc khi phiếu trả phát sinh từ đơn nhập, đồng thời cho phép tạo phiếu trả tự do từ danh sách hàng hóa.
- **2026-10-23 (Phiên 15)** – Chuẩn hóa toàn bộ bộ lọc ở GroupBox bên trái thành ComboBox (kèm tìm kiếm text trực tiếp qua `ComboboxHelper.CaiDatTimKiem`) và chuẩn hóa nút "Hiện cột" theo `frmDanhSachHangHoa` – Thống nhất trải nghiệm nhập liệu và tra cứu nhanh trên toàn bộ hệ thống; loại bỏ CheckBox rời rạc ở bộ lọc; riêng khoảng thời gian lấy cụm `grpThoiGianTao` (2 DateTimePicker Short) của `frmDanhSachHangHoa` làm chuẩn duy nhất.
- **2026-10-23 (Phiên 15)** – Đăng ký sự kiện mở `frmTraHangNhap` trên MenuStrip của `frmMain` (`mnuTraHangNhap_Click`) theo mô hình MDI – Đảm bảo người dùng nhấp vào mục "Trả hàng nhập" trên thanh menu là form hiển thị ngay lập tức hoặc activate nếu đã mở.
- **2026-10-24 (Phiên 17)** - Ban hành Điều luật 17: Cấm đoán bừa cấu trúc CSDL, bắt buộc xin quyền truy vấn SELECT kiểm tra khi chưa rõ; Chuẩn hóa tất cả các ComboBox nhiều giá trị (NCC, Người tạo, Người nhập/trả, Nhóm NCC) ở mọi form danh sách sang hỗ trợ nhập text và tìm kiếm qua `ComboboxHelper.CaiDatTimKiem` theo chuẩn `frmDanhSachHangHoa`.
- **2026-10-24 (Phiên 18)** – Thiết lập trọn vẹn Phân hệ Kiểm kho độc lập (`GUI/Forms/KiemKho/`) gồm cụm 5 Form chuẩn mực: `frmKiemKho` (danh sách MDI child, nút `+ Kiểm kho`, `Xuất file`, `Hiện cột`), `frmThemPhieuKiemKho` (lập phiếu mới chọn từ danh mục hàng hóa, sửa trực tiếp trên lưới, nhập file, Lưu tạm F9, Cân bằng kho F10), `frmChiTietKiemKho` (chi tiết khi nhấn đúp theo ảnh 3, chỉ giữ lại 2 nút: Xóa và Xuất file, khóa vĩnh viễn không cho xóa phiếu đã cân bằng theo Luật 6), `frmNhapKiemKhoFile` (nhập file Excel/CSV kèm tải file mẫu chuẩn theo khuôn mẫu `frmNhapHangHoaFile`) và `frmHienCotKiemKho` (tùy chọn cột). Liên kết mở form qua MenuStrip `mnuKiemKho_Click` trên `frmMain`.
- **2026-10-24 (Phiên 19)** – Chuẩn hóa toàn diện giao diện và bộ phông chữ theo `frmDanhSachHangHoa` (Ban hành Điều luật 18):
  1. Loại bỏ cột đánh dấu sao (`colDaDanhDau`) và nút "Làm mới bộ lọc" (`btnLamMoi`) khỏi `frmKiemKho` để tuân thủ 100% mẫu chuẩn.
  2. Khắc phục triệt để lỗi tràn ngang và co bẹp cột: Đồng bộ kích thước form `ClientSize = (1782, 853)`, thiết lập mở maximized (`WindowState = FormWindowState.Maximized`) từ `frmMain.cs`; cố định `MinimumWidth = 85` cho cột `SL thực tế` (`colSlThucTe`), các cột số lượng/giá trị khác và `colGhiChu` (`AutoSizeMode = Fill`, `MinimumWidth = 120`).
  3. Khắc phục triệt để lỗi font / mojibake: Khôi phục toàn bộ text tiếng Việt trên `menuStrip` và `statusStrip` của `frmMain.Designer.cs`; sửa câu lệnh SQL string concat nạp sản phẩm trong `frmThemPhieuKiemKho.cs` có tiền tố `N` (`N'['`, `N' (Tồn: '`); chuyển toàn bộ file mã nguồn sang UTF-8 with BOM và bổ sung `<CodePage>65001</CodePage>` trong file project `.csproj`.
- **2026-10-24 (Phiên 20)** – Chuẩn hóa toàn diện màu chữ, nút bấm, biểu tượng, bộ lọc và phông chữ toàn hệ thống (Cập nhật Điều luật 15 & 16):
  1. **Quy chuẩn màu chữ**: Cấm tự ý tô màu chữ (`ForeColor`) trong bất kỳ form nào (mã phiếu, mã hàng, nhãn tổng kết, trạng thái...). Chỉ tô ĐỎ (`Color.Red`, font Bold) khi có chênh lệch hoặc bất thường (lệch kiểm kho tăng/giảm khác 0, tồn kho <= 0, nợ cần trả > 0).
  2. **Quy chuẩn nút Xóa**: Khi phiếu/đơn không đủ điều kiện xóa (đã cân bằng, đã duyệt, đã hoàn thành, đã hủy), chỉ disable nút xóa (`btnXoa.Enabled = false`), giữ nguyên nội dung Text là "Xóa", tuyệt đối không đổi text sang "Không thể xóa" hay "Đã hủy".
  3. **Cấm chèn icon/emoji vào nút bấm**: Tuyệt đối không tự ý chèn emoji hoặc biểu tượng (như 🔒, 🗑, ✕...) vào nội dung Text của nút bấm WinForms.
  4. **Chuẩn hóa bộ lọc thời gian tạo**: Cụm `grpThoiGianTao` (2 DateTimePicker) BẮT BUỘC đặt ở vị trí CUỐI CÙNG (ở đáy) của vùng bộ lọc bên trái trên toàn bộ các form danh sách; tất cả các ComboBox danh mục đặt ở phía trên.
  5. **Khắc phục lỗi font chữ & mojibake**: Phục hồi 100% tiếng Việt có dấu trong ComboBox và thông báo tại `frmNhaCungCap`, `frmNhapHang`, `frmTraHangNhap`; chuyển 100% file mã nguồn sang UTF-8 with BOM; chuẩn hóa phông `Segoe UI, 9.5pt`.
- **2026-10-24 (Phiên 21)** – Cố định tỉ lệ form khởi đầu theo `frmDanhSachHangHoa`, loại bỏ phím tắt hàng F, chuẩn hóa bảng màu trạng thái 3 nhóm:
  1. **Tỉ lệ form khởi đầu cố định chuẩn `frmDanhSachHangHoa`**: Mọi form danh sách khởi đầu (`frmDanhSachHangHoa`, `frmKiemKho`, `frmNhapHang`, `frmTraHangNhap`, `frmNhaCungCap`) đều có `ClientSize = (1782, 853)`, `FormBorderStyle = FixedSingle`, `MaximizeBox = false`, `StartPosition = CenterScreen`. Bỏ lệnh `WindowState = Maximized;` khi mở form từ `frmMain` để form không fill full toàn bộ màn hình, khóa cố định tỉ lệ theo đúng ảnh mẫu 1.
  2. **Loại bỏ phím tắt hàng F**: Không gán các phím tắt F9, F10... cho chức năng; xóa text `(F9)`, `(F10)` trên nút bấm (đổi thành "Lưu tạm", "Cân bằng kho") và bỏ sự kiện bắt phím tắt `Keys.F9`, `Keys.F10` trong mã nguồn.
  3. **Chuẩn hóa bảng màu trạng thái 3 nhóm toàn hệ thống**:
     - *Nhóm Đã hoàn thành/hoạt động* (Đã nhập hàng, Đã nhận, Đã cân bằng, Đã trả hàng, Đang hoạt động, Đang bán): Tô màu **Xanh lá** (`Color.ForestGreen` hoặc `Color.FromArgb(22, 163, 74)` theo mẫu `frmNhaCungCap`).
     - *Nhóm Tạm thời* (Phiếu tạm): Tô màu **Vàng** (`Color.FromArgb(217, 119, 6)` theo mẫu `frmChiTietTraHangNhap`).
     - *Nhóm Đã hủy/ngừng* (Đã hủy, Ngừng hoạt động, Ngừng bán): Tô màu **Xám nhạt** (`Color.FromArgb(100, 116, 139)` theo mẫu `frmTraHangNhap`).
- **2026-10-24 (Phiên 22)** – Chuẩn hóa hiển thị nhãn trạng thái dòng chữ màu (bỏ khung badge) và thống nhất vị trí & thứ tự nút chức năng nhỏ ở cuối form (Ban hành Điều luật 19):
  1. **Hiển thị nhãn trạng thái chứng từ (Điều luật 19a)**: Nhãn đánh dấu trạng thái chứng từ (ở form chi tiết) hiển thị dưới dạng dòng chữ có màu trạng thái (3 nhóm màu chuẩn: Xanh lá / Vàng amber / Xám nhạt) lấy `frmChiTietPhieuNhap` làm khuôn mẫu; tuyệt đối không làm thành khung badge có viền, nền màu hay đệm padding. Đã sửa `frmChiTietKiemKho` từ badge nền viền sang dòng chữ màu như mẫu `frmChiTietPhieuNhap`.
  2. **Vị trí và thứ tự các nút chức năng nhỏ trên form (Điều luật 19b)**:
     - Toàn bộ các nút chức năng thao tác của form nghiệp vụ/nhập liệu bắt buộc đặt ở CUỐI (đáy) form lấy khuôn mẫu `frmThemPhieuNhap`, tuyệt đối không đặt lên trên đầu/header form như ảnh 2.
     - Chốt thống nhất thứ tự các nút từ trái qua phải trên toàn bộ các form là: `Bỏ qua` ➜ `Lưu tạm` (nếu có) ➜ `Lưu` (đối với form danh mục chỉ có Lưu thì thứ tự là `Bỏ qua` ➜ `Lưu`).
     - Đã áp dụng đồng bộ:
       + `frmThemPhieuKiemKho`: Đưa nút `Bỏ qua`, `Lưu tạm`, `Lưu` xuống thanh đáy (`pnlActions`) theo đúng thứ tự; đưa nút `Nhập file` sang thanh chọn hàng nhanh `pnlThemHang` cạnh nút "Nạp tất cả hàng trong kho"; thanh tiêu đề `pnlTop` chỉ giữ lại tiêu đề form.
       + `frmThemPhieuTraHang`: Đổi thứ tự nút đáy từ `Lưu tạm`, `Trả hàng`, `Bỏ qua` sang đúng thứ tự `Bỏ qua` ➜ `Lưu tạm` ➜ `Lưu`.
       + `frmThemPhieuNhap`: Đổi text nút hoàn thành sang `Lưu` đồng bộ thứ tự `Bỏ qua` ➜ `Lưu tạm` ➜ `Lưu`.
       + `frmNhomNhaCungCap`: Đổi thứ tự nút đáy sang đúng `Bỏ qua` ➜ `Lưu`.

- **2026-10-24 (Phiên 23)** – Thiết lập trọn vẹn Phân hệ Đơn vị tính (`GUI/Forms/DonViTinh/`) gồm cụm 3 Form chuẩn mực (`frmDonViTinh`, `frmChiTietDonViTinh`, `frmHienCotDvt`) và tích hợp tạo nhanh trên `frmHangHoa`:
  1. `frmDonViTinh`: MDI Child danh sách ĐVT kế thừa 100% tỉ lệ chuẩn `frmDanhSachHangHoa` (`1782x853`, `FixedSingle`, `MaximizeBox = false`, `StartPosition = CenterScreen`), thanh tìm kiếm F3/tức thời, bộ lọc ComboBox bên trái (Trạng thái sử dụng, Sắp xếp theo số lượng mặt hàng / tên, cụm `grpThoiGianTao` ở đáy), DataGridView chuẩn đệm đôi, Header CheckBox chọn tất cả, màu trạng thái 3 nhóm (Đang sử dụng xanh lá, Chưa sử dụng xám nhạt), phân trang đáy, nút `Xóa` bảo vệ ràng buộc toàn vẹn dữ liệu (chặn xóa nếu đang có hàng hóa liên kết), nút `Xuất file` CSV UTF-8 BOM.
  2. `frmChiTietDonViTinh`: Modal Dialog thêm mới/chỉnh sửa ĐVT (kích thước `684x520`), tự động sinh mã, validate chống trùng tên, hiển thị trực quan danh sách các mặt hàng đang áp dụng ĐVT này, thứ tự nút đáy chuẩn Điều luật 19b (`Bỏ qua` ➜ `Lưu`), bọc `ChangeDataTransaction`.
  3. `frmHienCotDvt`: Form popup tùy chọn ẩn/hiện cột DataGridView Đơn vị tính.
  4. Tích hợp nút `+` tạo nhanh ĐVT cạnh `cboDonVi` trên `frmHangHoa` và gắn sự kiện `mnuDonViTinh_Click` trên thanh Menu `frmMain`.
- **2026-10-24 (Phiên 24)** – Nâng cấp trải nghiệm & Đồng bộ chuẩn Quản lý Đơn vị tính (`frmDonViTinh` & `frmChiTietDonViTinh`):
  1. Loại bỏ cột STT (`colSTT`) không cần thiết khỏi DataGridView danh sách `frmDonViTinh`.
  2. Chuyển nút `Xóa` từ ngoài danh sách vào trong form chi tiết `frmChiTietDonViTinh`: Bố trí ở góc trái thanh đáy (`pnlBottom`), `btnBoQua` và `btnLuu` ở bên phải theo chuẩn khuôn mẫu `frmHangHoa`. Khi thêm mới ẩn nút xóa (`Visible = false`), khi xem/sửa hiện nút xóa; kiểm tra ràng buộc hàng hóa liên kết trong `HangHoa`: nếu đang có hàng hóa thì chặn xóa kèm thông báo chi tiết, nếu 0 hàng hóa thì cho phép xóa trong Transaction và ghi nhật ký thao tác.
- **2026-10-24 (Phiên 25)** – Khắc phục triệt để lỗi `Invalid column name 'MaDvtNoiBo'` và chuẩn hóa sử dụng trực tiếp thuộc tính `MaDVT` trong bảng `DonViTinh`:
  1. Giữ nguyên schema CSDL không thêm cột mới: Bảng `DonViTinh` sử dụng nguyên vẹn các cột hiện hữu (`MaDVT INT IDENTITY(1,1)`, `TenDVT NVARCHAR(50)`, `MoTa NVARCHAR(255)`), xóa bỏ đoạn code DDL `BaoDamCotMaDvtNoiBo()` và loại bỏ Mục 9 khỏi `docs/database_update.sql`.
  2. Chuẩn hóa truy vấn SQL: Thay thế toàn bộ tham chiếu `MaDvtNoiBo` trong `frmDonViTinh.cs` và `frmChiTietDonViTinh.cs` bằng `MaDVT` và định dạng mã hiển thị thống nhất `('DVT' + RIGHT('000' + CAST(dvt.MaDVT AS VARCHAR(10)), 3)) AS MaHienThi`.
  3. Nghiệp vụ xem & nhập mã ĐVT: Tại `frmChiTietDonViTinh`, mã `DVT00X` hiển thị rõ ràng cho người dùng xem; khi thêm mới cho phép người dùng tùy ý nhập mã (kiểm tra chống trùng mã qua `MaDVT`, hỗ trợ `SET IDENTITY_INSERT DonViTinh ON` khi chỉ định ID tùy biến); khi xem/chỉnh sửa khóa ô mã (`ReadOnly = true`, nền xám nhạt) để bảo toàn tính toàn vẹn khóa chính và liên kết khóa ngoại với `HangHoa`.

- **2026-10-24 (Phiên 26)** – Thiết kế và hiện thực trọn vẹn Chức năng Đổi kho làm việc (`frmDoiKho`) và Phân hệ Quản lý Chuyển kho (`GUI/Forms/ChuyenKho/`):
  1. **Chức năng Đổi kho (`frmDoiKho`)**:
     - Form Modal Dialog (`356x136`, `CenterParent`, `FixedDialog`) đúng chuẩn ảnh mẫu: ComboBox chọn kho làm việc từ các kho đang hoạt động (lọc theo quyền `TaiKhoanKho` nếu không phải Admin), 2 nút "Xác nhận" và "Hủy".
     - Khi xác nhận: Cập nhật `SessionContext.MaKhoHienTai`, `SessionContext.TenKhoHienTai`, cập nhật tiêu đề `frmMain` (`Quản Lý Kho Phú Quốc - [Tên kho]`), StatusStrip (`Kho: Tên kho`) và tự động làm mới các form con MDI đang mở.
  2. **Phân hệ Quản lý Chuyển kho (`GUI/Forms/ChuyenKho/`)**:
     - `frmChuyenKho`: MDI Child danh sách chuyển kho tỉ lệ chuẩn `1782x853` (`FixedSingle`, không phóng to/maximize theo Điều 15e). Bộ lọc bên trái 100% ComboBox (Trạng thái, Hướng chuyển, Kho đối ứng, Người tạo) với `ComboboxHelper.CaiDatTimKiem`, cụm thời gian tạo `grpThoiGianTao` ở ĐÁY (Điều luật 16). DataGridView đệm đôi, màu 3 nhóm trạng thái chuẩn (Đã nhận xanh lá, Đang chuyển / Phiếu tạm vàng amber, Đã hủy xám nhạt theo Điều 15b), phân trang đáy, nút `+ Chuyển kho`, `Xuất file` CSV UTF-8 BOM, `Hiện cột`, `?`.
     - `frmThemPhieuChuyenKho`: Modal Dialog lập phiếu chuyển kho mới. Chọn kho gửi (mặc định kho hiện tại), kho nhận (khác kho gửi), chọn hàng hóa kèm kiểm tra tồn hiện tại theo kho gửi qua `vw_TonKhoHienTai` (chặn tồn âm theo Luật 6), hỗ trợ nhập file `frmNhapChuyenKhoFile`. Thanh nút đáy chuẩn Điều luật 19b: `Bỏ qua` ➜ `Lưu tạm` (`NHAP`) ➜ `Lưu` (`DA_XUAT`). Bọc lưu trong `ChangeDataTransaction`, ghi nhật ký `NhatKyThaoTac`.
     - `frmChiTietChuyenKho`: Modal Dialog xem chi tiết khi double-click phiếu. Hiển thị Master-Detail đầy đủ, nhãn trạng thái dòng chữ màu không khung viền theo Điều luật 19a, nút "Nhận hàng" khả dụng khi đang chuyển và người dùng ở kho đích, nút "Xóa" tuân thủ Điều luật 15d & Luật 5 (xóa phiếu tạm, hủy phiếu đang chuyển, khóa phiếu đã nhận), nút "Xuất file".
     - `frmNhanChuyenKho`: Modal Dialog xác nhận nhận hàng tại kho đích. Kiểm đếm số lượng thực nhận, phát hiện hao hụt/thiếu trên đường chuyển, bắt buộc nhập lý do thất thoát vào ghi chú nhận hàng, cập nhật sang `DA_NHAN` và tự động cộng tồn kho đích qua View động.
     - `frmHienCotChuyenKho` & `frmNhapChuyenKhoFile`: Form tùy chọn ẩn hiện cột và nhập file dữ liệu mẫu CSV UTF-8 BOM.
  3. **Biên dịch & Chuẩn định dạng**: 100% file mã nguồn lưu định dạng **UTF-8 with BOM** (Điều luật 18). Biên dịch MSBuild thành công **0 Warning(s), 0 Error(s)**.

- **2026-10-24 (Phiên 28)** – Quét và khắc phục triệt để 4 lỗi phát sinh tại Phân hệ Chuyển kho và Phân hệ Báo cáo:
  1. **Khắc phục lỗi nạp hàng hóa tại `frmThemPhieuChuyenKho` (`Cannot find column [System.Data.DataRowView]`)**:
     - Bổ sung kiểm tra an toàn `int.TryParse(cboHangHoa.SelectedValue?.ToString(), out int maHang)` trước khi lọc `dtHangHoaAll.Select("MaHang = " + maHang)` trong `CapNhatTonHienTai()` và `btnThemHang_Click()`, loại bỏ ngoại lệ do ComboBox trả về đối tượng `DataRowView` trong quá trình binding.
     - Bổ sung kiểm tra an toàn cho `cboKhoNguon` và `cboKhoDich`.
  2. **Khắc phục lỗi truy vấn Báo cáo tồn kho tại `frmBaoCaoTonKho` (`Invalid column name 'TonToiDa'`)**:
     - Bảng `HangHoa` trong CSDL không có cột `TonToiDa`. Hệ thống quản lý định mức tồn tối đa và tối thiểu độc lập qua bảng `NguongTonKho` (cột `TonCaoNhat` và `TonToiThieu`).
     - Chuẩn hóa biểu thức SQL trong `frmBaoCaoTonKho.cs`: Thay thế `ISNULL(ntk.TonCaoNhat, ISNULL(h.TonToiDa, 0))` bằng `ISNULL(ntk.TonCaoNhat, 0)` trong cả mệnh đề WHERE lọc định mức và danh sách cột SELECT.
  3. **Khắc phục lỗi ComboBox Báo cáo thẻ kho tại `frmBaoCaoTheKho` (`Items collection cannot be modified when the DataSource property is set`)**:
     - `cboHangHoa` dùng `DataTable` làm dữ liệu nhưng lại gọi overload `ComboboxHelper.CaiDatTimKiem(cbo, List<string>)` vốn thao tác xóa `cbo.Items`.
     - Chuyển sang gọi đúng overload `ComboboxHelper.CaiDatTimKiem(cboHangHoa, dtComboHang, "TenHienThi", "MaHang", ...)` hỗ trợ DataSource thông qua `DataView`, hoàn toàn không chạm vào `cbo.Items`.
  4. **Khắc phục lỗi tên cột truy vấn Thẻ kho tại `frmBaoCaoTheKho` (`Invalid column name 'MaNCC'`, `'TenNCC'`, `'MaKH'`)**:
     - Tuân thủ Điều luật 17: Chạy truy vấn `SELECT` thực tế từ `INFORMATION_SCHEMA.COLUMNS` để xác định chính xác 100% thuộc tính của các bảng liên quan.
     - Sửa đúng khóa ngoại và thuộc tính thực tế: `pn.MaDonViCapHang`, `nccPn.TenDonVi` (thay vì `pn.MaNCC`, `nccPn.TenNCC`); `pth.MaDonViCapHang`, `nccPth.TenDonVi` (thay vì `pth.MaNCC`, `nccPth.TenNCC`); `px.MaDonBan -> DonBan.MaKh -> KhachHang.TenKH` (thay vì `px.MaKH`).
  5. **Bảo đảm chuẩn mã nguồn & biên dịch**:
     - 100% file mã nguồn lưu định dạng **UTF-8 with BOM** (Điều luật 18).
     - Biên dịch MSBuild: **Build succeeded. 0 Warning(s), 0 Error(s)**.

- **2026-10-24 (Phiên 29)** – Tối ưu trải nghiệm điều hướng 2 chiều giữa Báo cáo tồn kho và Báo cáo thẻ kho:
  1. **Bối cảnh & Vấn đề**: Khi người dùng nhấn đúp vào mặt hàng trên Báo cáo tồn kho (`frmBaoCaoTonKho`), hệ thống mở Báo cáo thẻ kho (`frmBaoCaoTheKho`) của mặt hàng đó. Tuy nhiên, trên giao diện Thẻ kho không có cơ chế quay lại, khiến người dùng muốn trở lại danh sách tồn kho phải vào lại MenuStrip để kích hoạt lại rất bất tiện.
  2. **Giải pháp kiến trúc & UX đã chọn (Phương án 1 theo yêu cầu người dùng)**:
     - Nút `btnQuayLai` ("Quay lại") được bổ sung vào thanh tiêu đề của `frmBaoCaoTheKho` cạnh tiêu đề "Báo cáo thẻ kho", tuân thủ Điều luật 14 và 15a (nút WinForms chuẩn, `UseVisualStyleBackColor = true`).
     - **Ngữ cảnh hiển thị**: Nút `btnQuayLai` chỉ xuất hiện (`Visible = true`) khi được mở từ Báo cáo tồn kho (`moTuTonKho = true`); khi mở độc lập từ MenuStrip thì nút này ẩn (`Visible = false`).
     - **Cơ chế quay lại đa kênh**: Khi nhấn nút `btnQuayLai`, nhấn phím `Escape`, hoặc bấm dấu `[X]` đóng cửa sổ con, hệ thống tự động `Activate()` lại `frmBaoCaoTonKho` và đóng cửa sổ thẻ kho.
     - **Bảo toàn trạng thái**: Dòng hàng đang chọn, trang hiện tại, bộ lọc của `frmBaoCaoTonKho` được bảo toàn 100%, đồng thời tự động focus lại DataGridView khi kích hoạt (`OnActivated`).
  3. **Biên dịch & Định dạng**: 100% file mã nguồn lưu định dạng **UTF-8 with BOM** (Điều luật 18). Biên dịch MSBuild: **Build succeeded. 0 Warning(s), 0 Error(s)**.

- **2026-10-24 (Phiên 31)** – Loại bỏ tính năng In thẻ kho trên Báo cáo thẻ kho (rmBaoCaoTheKho) và Xem thẻ kho trên Báo cáo tồn kho (rmBaoCaoTonKho):
  1. **Bối cảnh & Yêu cầu**: Người dùng yêu cầu loại bỏ tính năng in thẻ kho trong 2 chức năng báo cáo. Xác nhận cụ thể theo mong muốn người dùng: gỡ bỏ nút In thẻ kho trên form Thẻ kho và gỡ bỏ nút Xem thẻ kho trên form Báo cáo tồn kho.
  2. **Giải pháp thực hiện**:
     - rmBaoCaoTheKho: Gỡ bỏ nút tnInTheKho và sự kiện tnInTheKho_Click; căn chỉnh lại tnXuatFile (1465, 18), tnHienCot (1590, 18), tnHuongDanSuDung (1705, 18).
     - rmBaoCaoTonKho: Gỡ bỏ nút tnXemTheKho, sự kiện tnXemTheKho_Click, sự kiện CellDoubleClick và phương thức MoBaoCaoTheKho; cập nhật lại hướng dẫn sử dụng tnHuongDanSuDung_Click; các nút góc phải đồng bộ 100% với form Thẻ kho.
  3. **Biên dịch & Định dạng**: Lưu định dạng **UTF-8 with BOM** (Điều luật 18). Biên dịch MSBuild: **Build succeeded. 0 Warning(s), 0 Error(s)**.