# AGENTS.md – Quy chuẩn vận hành cho AI Assistant
## Dự án: BTL-LTTQ-2 (Quản lý kho hàng nội bộ - Windows Forms)

## 1. Mô tả dự án & Công nghệ
- **Dự án**: Ứng dụng Desktop Quản lý kho hàng nội bộ đa chi nhánh (Bài tập lớn Lập trình trực quan - LTTQ).
- **Giao diện & Nghiệp vụ**: Mô phỏng hệ thống quản trị kho chuyên nghiệp (Hàng hóa, Kho, Nhập hàng, Chuyển kho, Kiểm kê, Trả hàng, Xuất hủy, Báo cáo).
- **Nền tảng**: Windows Forms trên .NET Framework 4.7.2, ngôn ngữ C#.
- **Truy xuất dữ liệu**: ADO.NET thuần qua lớp tiện ích [`Classes/DataProcessing.cs`](BTL-LTTQ-2/Classes/DataProcessing.cs) (SqlConnection, SqlCommand, SqlDataAdapter, DataTable, SqlTransaction, SqlParameter). Không dùng Entity Framework.
- **Cơ sở dữ liệu**: SQL Server (`QuanLyKhoPhuQuocLTW`), kết nối Windows Authentication / SQL Express.
- **Quy ước code**: Tên Form, Model/DTO, Bảng CSDL và Cột đều dùng **TIẾNG VIỆT KHÔNG DẤU** PascalCase (`HangHoa`, `Kho`, `PhieuKiemKe`, `ChiTietPhieuNhap`...). Tên controls WinForms dùng tiền tố chuẩn (`btn`, `txt`, `cbo`, `dgv`, `lbl`, `dtp`, `mnu`, `tss`).

---

## 2. Quy trình đầu phiên (Bắt buộc trước khi viết code)
1. **Đọc `AGENTS.md` và `PROGRESS.md`**: Nắm rõ luật cốt lõi, tiến độ hiện tại, việc đang dở và danh sách backlog.
2. **Đọc đặc tả chức năng**: Nếu làm chức năng X, đọc file `docs/specs/<chức-năng>.md`. Nếu chưa có, tạo nháp từ `docs/specs/_mau-dac-ta.md` và hỏi người dùng xác nhận trước khi code.
3. **Tóm tắt đầu phiên (3–5 dòng)**: Báo cáo với người dùng: *"Tôi hiểu phiên này làm gì, dự kiến tác động vào các file nào"*. Chờ người dùng đồng ý nếu chỉnh sửa các file dùng chung (`frmMain.cs`, `DataProcessing.cs`, `App.config`, CSDL).

---

## 3. Quy trình cuối phiên (Khi người dùng nói "kết thúc phiên" / "chốt" / "xong")
1. **Kiểm tra biên dịch**: Chạy build kiểm tra dự án bằng lệnh `msbuild` hoặc công cụ tương ứng, bảo đảm **0 Error**.
2. **Đồng bộ đặc tả chức năng**: Nếu có sửa logic, đổi luồng, thêm/bớt trường hoặc controls của chức năng X, BẮT BUỘC cập nhật lại file `docs/specs/<chức-năng>.md` tương ứng trước khi kết thúc phiên.
3. **Cập nhật `PROGRESS.md`**:
   - Chuyển việc đã làm từ "Đang làm" sang "Đã xong".
   - Cập nhật mục "Đang làm" và "Việc kế tiếp".
   - Thêm dòng vào "Nhật ký phiên" (ngày – tóm tắt 2–4 dòng).
4. **Cập nhật quyết định**: Nếu có quyết định kiến trúc/nghiệp vụ mới, ghi thêm dòng vào `docs/quyet-dinh.md`.
5. **Cập nhật tài liệu luật**: Nếu tạo mới Form, UserControl, Helper, cấu trúc CSDL thì cập nhật file tương ứng trong `.agent/rules/`.
6. **Đề xuất Git commit**: Đưa ra gợi ý thông điệp commit ngắn gọn, chuẩn mực để người dùng tự commit.

---

## 4. 15 Luật cốt lõi không được vi phạm
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
15. **Không tự ý thêm màu cho các nút và chức năng**: Các nút bấm và thành phần chức năng trên giao diện không được tự ý thêm màu sắc tùy tiện (`BackColor`, `ForeColor`, `FlatStyle`...) khi người dùng chưa yêu cầu. Mặc định luôn sử dụng phong cách nút bấm WinForms chuẩn (`UseVisualStyleBackColor = true`).

---

## 5. Mục lục hệ thống tài liệu & Khi nào cần đọc

| Đường dẫn file | Nội dung tóm tắt | Khi nào đọc file này |
|---|---|---|
| [`.agent/rules/01-phong-cach-code.md`](./.agent/rules/01-phong-cach-code.md) | Quy ước đặt tên, kiến trúc phân tầng, quản lý Form MDI | Khi tạo hoặc sửa Form, xử lý sự kiện |
| [`.agent/rules/02-nghiep-vu-kho.md`](./.agent/rules/02-nghiep-vu-kho.md) | Nghiệp vụ tồn kho động, trạng thái chứng từ, khóa phiếu | Khi code nhập, xuất, chuyển, kiểm kho |
| [`.agent/rules/03-giao-dien-layout.md`](./.agent/rules/03-giao-dien-layout.md) | Chuẩn giao diện WinForms, DataGridView, Master-Detail, Nút bấm & Màu sắc | Khi dựng giao diện Form mới hoặc sửa bố cục |
| [`.agent/rules/04-xac-thuc-phan-quyen.md`](./.agent/rules/04-xac-thuc-phan-quyen.md) | Đăng nhập SHA-256, SessionContext, phân quyền menu và kho | Khi làm tính năng đăng nhập, đổi mật khẩu, phân quyền |
| [`.agent/rules/05-co-so-du-lieu.md`](./.agent/rules/05-co-so-du-lieu.md) | ADO.NET thuần với DataProcessing, 3 Nguyên tắc CSDL | Khi viết câu lệnh SQL, truy vấn hoặc thay đổi CSDL |
| [`docs/specs/danh-sach-hang-hoa.md`](./docs/specs/danh-sach-hang-hoa.md) | Đặc tả Quản lý Hàng hóa (Khuôn mẫu cho Danh mục CRUD) | Khuôn mẫu làm các danh mục Kho, NCC, Khách hàng |
| [`docs/specs/kiem-kho.md`](./docs/specs/kiem-kho.md) | Đặc tả Kiểm kho (Khuôn mẫu Chứng từ nhiều dòng) | Khuôn mẫu làm form Nhập hàng, Chuyển kho, Xuất hủy |
| [`docs/specs/_mau-dac-ta.md`](./docs/specs/_mau-dac-ta.md) | Mẫu khung tài liệu đặc tả chức năng mới | Khi bắt đầu thiết kế chức năng mới |
| [`docs/quyet-dinh.md`](./docs/quyet-dinh.md) | Sổ ghi chép các quyết định kỹ thuật & nghiệp vụ đã chốt | Khi cần tra cứu lý do chọn giải pháp kỹ thuật |
| [`docs/van-de-can-xu-ly.md`](./docs/van-de-can-xu-ly.md) | Danh sách vấn đề & kế hoạch triển khai (Backlog) | Khi nhận nhiệm vụ phát triển tính năng |
| [`PROGRESS.md`](./PROGRESS.md) | Bảng theo dõi tiến độ, việc đang làm, việc cần làm kế tiếp | Đọc ở đầu mọi phiên và cập nhật cuối phiên |
