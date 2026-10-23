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
- **2026-10-23 (Phiên 5)** – Chốt phân hệ Danh mục Hàng hóa làm Khuôn mẫu Thiết kế UI/UX cho toàn bộ dự án – Toàn bộ các form chức năng sau (Nhập kho, Kiểm kho, Chuyển kho, Danh mục...) bắt buộc tuân thủ 100% quy chuẩn: Phông `Segoe UI` (Base 9.5pt, Tiêu đề 12pt Bold), màu xen kẽ DataGridView (`#FFFFFF` & `#F8FAFC`, Selection `#E0E7FF`), bố cục 3 khối (Lọc bên trái + Lưới trung tâm + Phân trang đáy).
- **2026-10-23 (Phiên 5)** – Tinh giản cấu trúc thư mục thành `Classes/` & `GUI/` – Bỏ các thư mục trung gian `BLL/` và `DAL/` chưa có DTO/Service độc lập để đưa `DataProcessing.cs`, `SessionContext.cs`, `ComboboxHelper.cs` vào chung thư mục `Classes/`, phù hợp với mô hình đồ án LTTQ tinh gọn, trực quan và dễ tiếp cận cho người mới học.
