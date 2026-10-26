# AGENTS.md – Quy chuẩn vận hành cho AI Assistant
## Dự án: BTL-LTTQ-2 (Quản lý kho hàng nội bộ - Windows Forms)

## 1. Mô tả dự án & Công nghệ
- **Dự án**: Ứng dụng Desktop Quản lý kho hàng nội bộ đa chi nhánh (Bài tập lớn Lập trình trực quan - LTTQ).
- **Giao diện & Nghiệp vụ**: Mô phỏng hệ thống quản trị kho chuyên nghiệp (Hàng hóa, Kho, Nhập hàng, Chuyển kho, Kiểm kê, Trả hàng, Xuất hủy, Báo cáo).
- **Nền tảng**: Windows Forms trên .NET Framework 4.7.2, ngôn ngữ C#.
- **Truy xuất dữ liệu**: ADO.NET thuần qua lớp tiện ích [`Classes/DataProcessing.cs`](BTL-LTTQ-2/Classes/DataProcessing.cs) (SqlConnection, SqlCommand, SqlDataAdapter, DataTable, SqlTransaction, SqlParameter). Không dùng Entity Framework.
- **Cơ sở dữ liệu**: SQL Server (`QuanLyKhoPhuQuocLTTQ`), kết nối Windows Authentication / SQL Express.
- **Quy ước code**: Tên Form, Model/DTO, Bảng CSDL và Cột đều dùng **TIẾNG VIỆT KHÔNG DẤU** PascalCase (`HangHoa`, `Kho`, `PhieuKiemKe`, `ChiTietPhieuNhap`...). Tên controls WinForms dùng tiền tố chuẩn (`btn`, `txt`, `cbo`, `dgv`, `lbl`, `dtp`, `mnu`, `tss`).

---

## 2. Quy trình đầu phiên (Bắt buộc trước khi viết code)
1. **Đọc `AGENTS.md` và `PROGRESS.md`**: Nắm rõ luật cốt lõi, tiến độ hiện tại, việc đang dở và danh sách backlog.
2. **Đọc đặc tả chức năng**: Nếu làm chức năng X, đọc file `docs/specs/<chức-năng>.md`. Nếu chưa có, tạo nháp từ `docs/specs/_mau-dac-ta.md` và hỏi người dùng xác nhận trước khi code.
3. **Tóm tắt đầu phiên (3–5 dòng)**: Báo cáo với người dùng: *"Tôi hiểu phiên này làm gì, dự kiến tác động vào các file nào"*. Chờ người dùng đồng ý nếu chỉnh sửa các file dùng chung (`frmMain.cs`, `DataProcessing.cs`, `App.config`, CSDL).

---

## 3. Quy trình cuối phiên (Khi người dùng nói "kết thúc phiên" / "chốt" / "xong")
1. **Kiểm tra mã hóa & rà soát lỗi font (BẮT BUỘC)**: Chạy lệnh tự động `powershell -ExecutionPolicy Bypass -File tools/KiemTraMaHoa.ps1`. Bảo đảm **100% file có UTF-8 with BOM và 0 lỗi Mojibake**. Nếu phát sinh bất kỳ lỗi font chữ nào, BẮT BUỘC phải xử lý triệt để trước khi chuyển bước tiếp theo.
2. **Kiểm tra biên dịch**: Chạy build kiểm tra dự án bằng lệnh `msbuild` hoặc công cụ tương ứng, bảo đảm **0 Error, 0 Warning**.
3. **Đồng bộ đặc tả chức năng**: Nếu có sửa logic, đổi luồng, thêm/bớt trường hoặc controls của chức năng X, BẮT BUỘC cập nhật lại file `docs/specs/<chức-năng>.md` tương ứng trước khi kết thúc phiên.
4. **Cập nhật `PROGRESS.md`**:
   - Chuyển việc đã làm từ "Đang làm" sang "Đã xong".
   - Cập nhật mục "Đang làm" và "Việc kế tiếp".
   - Thêm dòng vào "Nhật ký phiên" (ngày – tóm tắt 2–4 dòng).
5. **Cập nhật quyết định**: Nếu có quyết định kiến trúc/nghiệp vụ mới, ghi thêm dòng vào `docs/quyet-dinh.md`.
6. **Cập nhật tài liệu luật**: Nếu tạo mới Form, UserControl, Helper, cấu trúc CSDL thì cập nhật file tương ứng trong `.agent/rules/`.
7. **Đề xuất Git commit**: Đưa ra gợi ý thông điệp commit ngắn gọn, chuẩn mực để người dùng tự commit.

---

## 4. 19 Luật cốt lõi không được vi phạm
1. **Tên tiếng Việt trong code**: Toàn bộ Form, Model, Bảng, Cột phải dùng tiếng Việt không dấu PascalCase (`HangHoa`, `MaKho`, `SoLuong`). Tiền tố control WinForms chuẩn (`btnThem`, `txtMaHang`, `dgvHangHoa`). Cấm tự ý dùng tiếng Anh lộn xộn.
2. **Mô hình MDI duy nhất**: Dùng duy nhất `frmMain` làm form cha MDI (`IsMdiContainer = true`). Khi mở form con phải kiểm tra và `Activate()` nếu form đã mở, tránh mở trùng lặp.
3. **Quản lý biến động kho tập trung**: Mọi biến động tồn kho (nhập, xuất, chuyển, cân bằng) phải tuân theo quy tắc trong `.agent/rules/02-nghiep-vu-kho.md`.
4. **Tồn kho thực tế tính qua `vw_TonKhoHienTai`**: Tồn kho sổ sách hiện thời tính động từ View `vw_TonKhoHienTai` (dựa trên phiếu nhập, xuất, chuyển, điều chỉnh), không tạo bảng tồn kho tĩnh.
5. **Cấm sửa/xóa chứng từ đã hoàn thành**: Phiếu tạm (`NHAP`) được sửa/xóa. Phiếu đã xong (`DA_XAC_NHAN`, `DA_DIEU_CHINH`, `DA_NHAN`, `DA_TRA_HANG`) **tuyệt đối không được sửa hay xóa**. Chỉ được phép HỦY (`TrangThai = 'HUY'`).
6. **Phiếu kiểm kho đã cân bằng KHÔNG được hủy**: Khi đã sinh `PhieuDieuChinh` trạng thái `DA_DUYET`, phiếu kiểm kho bị khóa vĩnh viễn. Sai lệch sau đó phải lập phiếu kiểm mới.
7. **Luôn dùng giao dịch (Transaction)**: Mọi thao tác lưu chứng từ kèm nhiều dòng chi tiết và cân bằng kho phải bọc trong `DataProcessing.ChangeDataTransaction((conn, trans) => { ... })`.
8. **3 Nguyên tắc vàng khi sửa CSDL**: (1) Chỉ cộng thêm cột/bảng mới, không DROP/đổi tên cột cũ; (2) Khi sửa cấu trúc CSDL phải lưu script vào `docs/database_update.sql`; (3) Phải báo lý do trước khi chạm CSDL.
9. **Chống SQL Injection 100%**: Tuyệt đối không cộng chuỗi SQL trực tiếp với biến từ TextBox. Mọi tham số bắt buộc dùng `SqlParameter`.
10. **Chuẩn hóa DataGridView & Trải nghiệm WinForms**: DataGridView phải định dạng theo quy chuẩn trong `.agent/rules/03-giao-dien-layout.md` (màu dòng xen kẽ, căn phải số lượng/đơn giá định dạng `N0`, căn giữa mã/ngày, FullRowSelect, ReadOnly).
11. **Không tự ý thêm thư viện ngoài**: Không tự ý cài đặt thêm NuGet package nặng hoặc thư viện bên ngoài khi chưa hỏi ý kiến người dùng.
12. **Bảo toàn và đồng bộ tài liệu đặc tả**: Khi thay đổi code/luồng/file của chức năng X, BẮT BUỘC đồng bộ lại `docs/specs/<chức-năng>.md` ngay trong phiên đó.
13. **Nguyên tắc "Làm đến đâu thiếu gì thêm đó"**: AI tuyệt đối không tự ý viết trước các tầng trừu tượng (BaseForm, Helper, Service...), không làm tắt, không sinh file rác khi người dùng chưa yêu cầu. Từng bước đi phải rõ ràng, tối giản, để người dùng nắm và kiểm soát 100% dự án.
14. **Hiển thị tiếng Việt 100% cho mọi nút bấm và chức năng**: Tất cả các nút bấm (`Button`), menu, nhãn và chức năng trên giao diện bắt buộc hiển thị nhãn bằng **tiếng Việt có dấu** rõ ràng, dễ hiểu. Ví dụ: chức năng "Nhập file" trong `frmNhaCungCap` hay bất kỳ form nào không được để là "Import file" mà phải để là "Nhập file", "Xuất file" thay vì "Export file". Tuyệt đối không để sót từ tiếng Anh trên giao diện người dùng.
15. **Không tự ý thêm màu sắc, chuẩn màu trạng thái 3 nhóm, cấm chèn icon/phím tắt F, Quy chuẩn nút Xóa & Quy chuẩn Kích thước Form**:
    - (a) **Cấm tự ý thêm màu cho các nút và dòng chữ**: Các nút bấm mặc định luôn dùng phong cách WinForms chuẩn (`UseVisualStyleBackColor = true`). Tuyệt đối không tự ý tô màu (`ForeColor`) cho các dòng chữ (Mã phiếu, Mã hàng, tiền, nhãn thống kê, nhãn chi tiết...). Chỉ các trường hợp **chênh lệch hoặc bất thường cần lưu ý mới được tô ĐỎ** (`Color.Red`, font Bold) – ví dụ: chênh lệch khi kiểm kho (cả lệch tăng và lệch giảm khi khác 0 đều tô đỏ, theo mẫu cảnh báo tồn kho <= 0 của `frmDanhSachHangHoa`), nợ cần trả quá hạn/tồn âm.
    - (b) **Bảng màu trạng thái 3 nhóm chuẩn toàn hệ thống**:
      + *Nhóm Hoàn thành / Hoạt động* (Đã nhập hàng, Đã nhận, Đã cân bằng, Đã trả hàng, Đang hoạt động, Đang bán): Màu **Xanh lá** (`Color.ForestGreen` / `Color.FromArgb(22, 163, 74)` theo mẫu `frmNhaCungCap`).
      + *Nhóm Tạm thời* (Phiếu tạm): Màu **Vàng** (`Color.FromArgb(217, 119, 6)` theo mẫu `frmChiTietTraHangNhap`).
      + *Nhóm Đã hủy / Ngừng* (Đã hủy, Ngừng hoạt động, Ngừng bán): Màu **Xám nhạt** (`Color.FromArgb(100, 116, 139)` theo mẫu `frmTraHangNhap`).
    - (c) **Cấm icon & Cấm phím tắt hàng F**: Tuyệt đối không chèn emoji, biểu tượng (như 🔒, 🗑, v.v.) hay icon tùy tiện vào nút bấm; không gán phím tắt hàng F (`F9`, `F10`...) cho các chức năng.
    - (d) **Quy chuẩn nút Xóa**: Khi 1 phiếu/đơn không đủ điều kiện xóa (ví dụ phiếu đã cân bằng kho, đã duyệt, đã hủy...), CHỈ disable nút xóa (`btnXoa.Enabled = false`), **vẫn giữ nguyên nội dung hiển thị là "Xóa"**, tuyệt đối không được tự ý đổi text của nút (không đổi thành "Không thể xóa", "Đã hủy"...) hoặc đưa ra thông báo cảnh báo khi nhấn như trong `frmDanhSachHangHoa`.
    - (e) **Quy chuẩn Kích thước Form toàn hệ thống (Lấy các form mẫu làm khung chuẩn)**:
      + **Chức năng lớn (Màn hình danh sách chính / Phân hệ lớn)**: Lấy `frmDanhSachHangHoa` làm chuẩn kích thước duy nhất (`ClientSize = (1782, 853)`, `FixedSingle`, `MaximizeBox = false`, `StartPosition = CenterScreen`). Mọi form của các chức năng lớn khác đều phải có kích thước đúng như vậy, tuyệt đối không được làm khác trừ khi được yêu cầu cụ thể (ngoại trừ duy nhất form chức năng lớn Đổi kho `frmDoiKho` được yêu cầu làm bằng form size nhỏ). Tuyệt đối không gán `WindowState = Maximized` khi mở từ `frmMain`.
      + **Chức năng nhỏ hơn (Form chi tiết, Form thêm/sửa/xóa, Popup, Modal)**: Lấy mẫu các form sau làm khung để các form khác tuân thủ:
        * **Size Lớn**: Lấy size của form Cập nhật hàng hóa (`frmHangHoa`) làm chuẩn (`ClientSize = (1182, 803)`).
        * **Size Vừa**: Lấy size form Cập nhật thông tin nhà cung cấp (`frmDonViCapHang`) làm chuẩn (`ClientSize = (960, 705)`).
        * **Size Nhỏ**: Lấy size của form chức năng lớn Đổi kho (`frmDoiKho`) làm chuẩn (`ClientSize = (356, 136)`).
16. **Quy chuẩn bộ lọc GroupBox bên trái màn hình danh sách**: Tất cả các bộ lọc ở GroupBox bên trái màn hình danh sách thống nhất 100% sử dụng **ComboBox** (không dùng CheckBox rời rạc). Đối với các trường danh mục nhiều giá trị (Nhà cung cấp, Nhóm hàng, Vị trí, Người tạo, Người trả/nhập...), bắt buộc cho phép nhập và tìm kiếm text qua `ComboboxHelper.CaiDatTimKiem` (tìm kiếm tiếng Việt không dấu, tự động gợi ý/chọn khi Enter). Ngoại trừ duy nhất bộ lọc thời gian: bắt buộc sử dụng cụm `grpThoiGianTao` chứa 2 `DateTimePicker` (`dtpTuNgay`, `dtpDenNgay` định dạng Short) lấy `frmDanhSachHangHoa` làm chuẩn duy nhất và **BẮT BUỘC ĐẶT Ở VỊ TRÍ CUỐI CÙNG (ở đáy) của vùng bộ lọc** trên toàn bộ các form danh sách; tất cả các bộ lọc danh mục ComboBox khác đặt ở phía trên.
17. **Cấm đoán bừa cấu trúc CSDL - Bắt buộc xin quyền truy vấn SELECT kiểm tra**: Khi tạo hoặc chỉnh sửa một truy vấn tới database trong code, nếu chưa rõ về thuộc tính, tên cột, kiểu dữ liệu, các giá trị được cho phép của thuộc tính... BẮT BUỘC phải xin người dùng cấp quyền truy vấn tới database để kiểm tra (chỉ sử dụng các lệnh truy vấn `SELECT`, nghiêm cấm chỉnh sửa cấu trúc hay dữ liệu database khi chưa được phép), tuyệt đối không được đoán bừa.
18. **Chuẩn Font chữ & Màu chữ toàn hệ thống (Lấy `frmDanhSachHangHoa` làm chuẩn duy nhất)**:
    - Lấy font chữ và màu chữ của form `frmDanhSachHangHoa` làm chuẩn duy nhất, mọi form phải tuân thủ tuyệt đối, không được làm khác trừ khi được yêu cầu.
    - Cấp Form và tất cả các Controls thông thường (Label thông tin, ô nhập liệu, ComboBox, Button, dòng DataGridView, và **toàn bộ các trường/nhãn thống kê như Tổng tiền hàng, NCC cần trả, Giảm giá, Số lượng, Đơn giá...**) bắt buộc dùng font thường: `Segoe UI, 9.5pt, Regular` và màu chữ mặc định của hệ thống (`SystemColors.ControlText` / inherit).
    - **Nghiêm cấm tuyệt đối**: Không được tự ý in đậm (`FontStyle.Bold`) các trường dữ liệu, số tiền, nhãn thống kê; không được chọn màu chữ mỗi trường 1 kiểu (như trường màu xám, trường màu xanh đen, trường màu đen đậm). Mọi trường đều phải đồng nhất một chuẩn như `frmDanhSachHangHoa`.
    - **Các ngoại lệ duy nhất**:
      + Tiêu đề lớn của Form/khối: `Segoe UI, 12pt, Bold`.
      + Tiêu đề cột DataGridView: `Segoe UI, 9.5pt, Bold`.
      + Chênh lệch kiểm kho (khác 0), cảnh báo tồn kho $\le$ 0, nợ cần trả quá hạn/tồn âm: Màu **ĐỎ** (`Color.Red`, font Bold) theo Điều luật 15a.
      + Màu trạng thái 3 nhóm (Xanh lá, Vàng amber, Xám nhạt) theo Điều luật 15b và 19a (nhãn text thường, không khung badge).
    - **Quy chuẩn mã hóa Unicode cố định duy nhất (Cấm tuyệt đối mọi định dạng khác)**:
      + Toàn bộ 100% file mã nguồn và tài liệu (`.cs`, `.Designer.cs`, `.resx`, `.sql`, `.config`, `.md`, `.json`, `.ps1`) BẮT BUỘC cố định duy nhất một chuẩn mã hóa: **UTF-8 with BOM (Byte Order Mark: `0xEF, 0xBB, 0xBF`)** và ngắt dòng `CRLF` (Windows).
      + Cố định trình biên dịch C# qua thẻ `<CodePage>65001</CodePage>` trong file dự án `BTL-LTTQ-2.csproj`.
      + Cố định môi trường IDE và trình soạn thảo qua file cấu hình `.editorconfig` (`charset = utf-8-bom`, `end_of_line = crlf`) và `.gitattributes`.
      + Nghiêm cấm tuyệt đối việc sử dụng các công cụ, lệnh PowerShell (như `Out-File` mặc định) hay trình sửa file làm mất BOM hoặc đọc/ghi nhầm sang Windows-1252/ANSI sinh ra ký tự rác (lỗi mã hóa font chữ). Bắt buộc chạy `tools/KiemTraMaHoa.ps1` để tự động xác nhận 0 lỗi font trước khi kết thúc phiên.
      + Mọi chuỗi SQL tiếng Việt bắt buộc có tiền tố `N'...'` (ví dụ `N'Chưa rõ'`) để CSDL SQL Server lưu trữ Unicode chính xác 100%.
19. **Quy chuẩn vị trí & thứ tự nút chức năng nhỏ trên form và hiển thị nhãn trạng thái**:
    - (a) **Hiển thị nhãn trạng thái chứng từ**: Nhãn đánh dấu trạng thái phiếu hiển thị dưới dạng dòng chữ có màu theo bảng màu 3 nhóm chuẩn (Điều luật 15b) lấy khuôn mẫu từ `frmChiTietPhieuNhap`, tuyệt đối không làm thành khung/badge có viền, nền màu hay đệm (Padding/BackColor) như ảnh 1. Các form khác đang sai phải sửa lại như mẫu (ngoại trừ form mẫu hệ thống `frmDanhSachHangHoa`).
    - (b) **Vị trí và thứ tự các nút chức năng nhỏ**: Thống nhất toàn bộ các nút chức năng nhỏ thao tác của form nghiệp vụ/nhập liệu được đặt ở CUỐI (đáy) form lấy khuôn mẫu từ `frmThemPhieuNhap`, tuyệt đối không để lên đầu/header form như ảnh 2. Thứ tự các nút từ trái qua phải chốt cố định toàn hệ thống là: `Bỏ qua` ➜ `Lưu tạm` (nếu form có hỗ trợ lưu nháp) ➜ `Lưu` (form danh mục không có lưu tạm thì thứ tự là `Bỏ qua` ➜ `Lưu`).

---

## 5. Mục lục hệ thống tài liệu & Khi nào cần đọc

| Đường dẫn file | Nội dung tóm tắt | Khi nào đọc file này |
|---|---|---|
| [`.agent/rules/01-phong-cach-code.md`](./.agent/rules/01-phong-cach-code.md) | Quy ước đặt tên, kiến trúc phân tầng, quản lý Form MDI | Khi tạo hoặc sửa Form, xử lý sự kiện |
| [`.agent/rules/02-nghiep-vu-kho.md`](./.agent/rules/02-nghiep-vu-kho.md) | Nghiệp vụ tồn kho động, trạng thái chứng từ, khóa phiếu | Khi code nhập, xuất, chuyển, kiểm kho |
| [`.agent/rules/03-giao-dien-layout.md`](./.agent/rules/03-giao-dien-layout.md) | Chuẩn giao diện WinForms, DataGridView, Master-Detail, Nút bấm & Màu sắc, Quy chuẩn bộ lọc bên trái | Khi dựng giao diện Form mới hoặc sửa bố cục |
| [`.agent/rules/04-xac-thuc-phan-quyen.md`](./.agent/rules/04-xac-thuc-phan-quyen.md) | Đăng nhập SHA-256, SessionContext, phân quyền menu và kho | Khi làm tính năng đăng nhập, đổi mật khẩu, phân quyền |
| [`.agent/rules/05-co-so-du-lieu.md`](./.agent/rules/05-co-so-du-lieu.md) | ADO.NET thuần với DataProcessing, 3 Nguyên tắc CSDL | Khi viết câu lệnh SQL, truy vấn hoặc thay đổi CSDL |
| [`docs/specs/danh-sach-hang-hoa.md`](./docs/specs/danh-sach-hang-hoa.md) | Đặc tả Quản lý Hàng hóa (Khuôn mẫu cho Danh mục CRUD) | Khuôn mẫu làm các danh mục Kho, NCC, Khách hàng |
| [`docs/specs/tra-hang-nhap.md`](./docs/specs/tra-hang-nhap.md) | Đặc tả Quản lý Trả hàng nhập (Màn hình danh sách & chi tiết) | Tham khảo chức năng trả hàng nhà cung cấp |
| [`docs/specs/kiem-kho.md`](./docs/specs/kiem-kho.md) | Đặc tả Kiểm kho (Khuôn mẫu Chứng từ nhiều dòng) | Khuôn mẫu làm form Nhập hàng, Chuyển kho, Xuất hủy |
| [`docs/specs/_mau-dac-ta.md`](./docs/specs/_mau-dac-ta.md) | Mẫu khung tài liệu đặc tả chức năng mới | Khi bắt đầu thiết kế chức năng mới |
| [`docs/quyet-dinh.md`](./docs/quyet-dinh.md) | Sổ ghi chép các quyết định kỹ thuật & nghiệp vụ đã chốt | Khi cần tra cứu lý do chọn giải pháp kỹ thuật |
| [`docs/van-de-can-xu-ly.md`](./docs/van-de-can-xu-ly.md) | Danh sách vấn đề & kế hoạch triển khai (Backlog) | Khi nhận nhiệm vụ phát triển tính năng |
| [`PROGRESS.md`](./PROGRESS.md) | Bảng theo dõi tiến độ, việc đang làm, việc cần làm kế tiếp | Đọc ở đầu mọi phiên và cập nhật cuối phiên |
