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
- **2026-10-21 (Phiên 1)** – Các thao tác lưu chứng từ kèm nhiều dòng chi tiết BẮT BUỘC bọc trong Transaction – Dùng `DataProcessing.ChangeDataTransaction` để đảm bảo tính toàn vẹn ACID.
