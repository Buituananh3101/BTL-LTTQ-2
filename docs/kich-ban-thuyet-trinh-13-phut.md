# Kịch bản Thuyết trình Đề tài Quản lý Kho hàng Nội bộ (Khung 13 phút)
## Đồ án: Bài tập lớn Lập trình trực quan (LTTQ) – WinForms C# & SQL Server

---

## ⏱️ Bảng phân bổ thời gian tổng thể (Tổng: 13 phút = 780 giây)

| Phần | Nội dung | Thời lượng | Mục tiêu cần đạt |
|---|---|---|---|
| **Mở đầu** | Đặt vấn đề, bài toán chuỗi kho & Kiến trúc cốt lõi | **1.5 phút** | Gây ấn tượng về bài toán thực tế và CSDL chuẩn hóa |
| **Phần 1** | Quản trị Hệ thống, Đổi kho & Danh mục Hàng hóa | **2.0 phút** | Nêu bật cơ chế Đổi kho linh hoạt & Radar cảnh báo tồn |
| **Phần 2** | Chuỗi cung ứng: Nhập hàng & Trả hàng NCC | **2.5 phút** | Nêu bật tính Bất biến chứng từ & Đối trừ công nợ |
| **Phần 3** | Luân chuyển nội bộ: Chuyển kho 2 chặng | **2.0 phút** | Nêu bật cơ chế Hàng đang chuyển & Chống thất thoát |
| **Phần 4** | Kiểm kê & Cân bằng kho tự động | **2.5 phút** | Nêu bật Cân bằng tự động 1-click & Khóa phiếu vĩnh viễn |
| **Phần 5** | Xuất hủy & Báo cáo Thẻ kho chi tiết | **1.5 phút** | Nêu bật Truy vết thẻ kho đa chiều (Audit Trail) |
| **Kết luận** | Điểm sáng kỹ thuật (Tồn kho động, Debounce, Transaction) | **1.0 phút** | Chốt lại độ tin cậy và trải nghiệm mượt mà của hệ thống |

---

## 🎤 Kịch bản chi tiết từng chức năng (Nghiệp vụ nhanh & Điểm sáng đặc biệt)

### 1. Mở đầu & Kiến trúc Dữ liệu nền tảng (00:00 – 01:30 | 1.5 phút)
- **Nghiệp vụ tóm tắt**: Dự án giải quyết bài toán quản lý kho hàng nội bộ cho chuỗi chi nhánh (mô hình kho Phú Quốc), quản lý xuyên suốt dòng chảy hàng hóa từ nhà cung cấp qua các kho nội bộ đến khi xuất dùng hoặc xuất hủy. Hệ thống xây dựng trên Windows Forms .NET Framework 4.7.2, CSDL SQL Server với 30 bảng quan hệ chặt chẽ.
- ⭐ **Điểm sáng đặc biệt**: **Cơ chế Tồn kho Động (`vw_TonKhoHienTai`) & An toàn Giao dịch (`SqlTransaction`)**.
  - Hệ thống **tuyệt đối không lưu tồn kho thành cột số lượng tĩnh** để loại bỏ hoàn toàn nguy cơ lệch số liệu khi nhiều máy cùng thao tác. Tồn kho được tính toán thời gian thực từ 6 nguồn chứng từ phát sinh. Mọi thao tác lưu phiếu nhiều dòng chi tiết đều bọc trong giao dịch ADO.NET, bảo đảm dữ liệu luôn toàn vẹn 100%.

---

### 2. Quản trị Hệ thống & Đổi kho linh hoạt (01:30 – 02:30 | 1.0 phút)
- **Nghiệp vụ tóm tắt**: Hệ thống phân quyền chặt chẽ theo vai trò (RBAC: Admin, Thủ kho, Nhân viên) và phân quyền theo chi nhánh phụ trách. Mọi thao tác trọng yếu đều được ghi vết tự động vào bảng `NhatKyThaoTac`.
- ⭐ **Điểm sáng đặc biệt**: **Chuyển đổi ngữ cảnh kho tức thời (`frmDoiKho`)**.
  - Người quản lý phụ trách nhiều chi nhánh có thể đổi kho làm việc ngay trên thanh công cụ trong 1 click. Hệ thống tự động chuyển đổi ngữ cảnh (`SessionContext`), làm mới toàn bộ bộ lọc và bảng dữ liệu của các màn hình đang mở theo kho mới mà không cần đăng xuất đăng nhập lại.

---

### 3. Danh mục Hàng hóa & Vị trí lưu trữ (02:30 – 03:30 | 1.0 phút)
- **Nghiệp vụ tóm tắt**: Quản lý thông tin hàng hóa, nhóm hàng, đơn vị tính chuẩn, vị trí kệ hàng và thiết lập ngưỡng định mức tồn kho (Tồn tối thiểu - Tồn tối đa) cho từng chi nhánh.
- ⭐ **Điểm sáng đặc biệt**: **Radar cảnh báo tồn kho & Trực quan hóa dữ liệu**.
  - Giao diện danh mục tự động nhận diện và **tô đỏ nổi bật** các mặt hàng có tồn kho rơi xuống dưới mức tối thiểu hoặc tồn âm, giúp thủ kho phát hiện ngay nguy cơ đứt gãy nguồn hàng mà không cần phải chạy báo cáo phức tạp.

---

### 4. Nhập hàng từ Nhà cung cấp (03:30 – 04:45 | 1.25 phút)
- **Nghiệp vụ tóm tắt**: Lập phiếu nhập kho từ nhà cung cấp (`frmThemPhieuNhap`), hỗ trợ tính toán chiết khấu, thuế VAT, chi phí phát sinh và theo dõi công nợ phải trả NCC. Cho phép lưu tạm (`NHAP`) để bổ sung dần trước khi xác nhận.
- ⭐ **Điểm sáng đặc biệt**: **Quy tắc Chứng từ Bất biến (Document Immutability)**.
  - Phân tách rạch ròi giữa phiếu nháp và phiếu chính thức. Một khi đã nhấn **"Lưu"** (`DA_XAC_NHAN`), phiếu lập tức ghi nhận vào tồn kho và **khóa cứng quyền sửa/xóa** để bảo vệ tính pháp lý của sổ sách kho. Nếu có sai sót, nhân viên bắt buộc phải lập phiếu đối ứng (Trả hàng) hoặc Hủy phiếu có kiểm toán.

---

### 5. Trả hàng Nhà cung cấp (04:45 – 06:00 | 1.25 phút)
- **Nghiệp vụ tóm tắt**: Xử lý việc hoàn trả hàng lỗi, hàng kém chất lượng cho nhà cung cấp (`frmTraHangTheoPhieuNhap`), giảm trừ công nợ phải trả hoặc ghi nhận thu hồi tiền mặt, đồng thời tự động xuất giảm số lượng tồn kho.
- ⭐ **Điểm sáng đặc biệt**: **Truy vết trả hàng theo Lô/Phiếu nhập gốc & Chặn tồn âm**.
  - Thủ kho có thể chọn trả hàng trực tiếp từ phiếu nhập ban đầu. Hệ thống tự động kiểm soát số lượng còn lại có thể trả của từng dòng hàng, chặn tuyệt đối việc trả vượt quá số lượng đã mua hoặc xuất trả khi kho không đủ tồn thực tế.

---

### 6. Luân chuyển nội bộ – Chuyển kho 2 chặng (06:00 – 08:00 | 2.0 phút)
- **Nghiệp vụ tóm tắt**: Điều phối hàng hóa giữa các kho trong cùng hệ thống (Ví dụ: Từ Kho Tổng sang Kho Chi nhánh).
- ⭐ **Điểm sáng đặc biệt**: **Quy trình luân chuyển 2 chặng độc lập (Transit State Pipeline)**.
  - Khi kho gửi xuất hàng, phiếu mang trạng thái `DA_XUAT` – tồn kho gửi lập tức giảm, nhưng kho nhận **chưa tăng** (hàng đang nằm ở trạng thái vận chuyển trung gian).
  - Khi xe tới nơi, thủ kho nhận mở màn hình **"Nhận chuyển kho" (`frmNhanChuyenKho`)** để trực tiếp kiểm đếm số lượng thực tế nhận được trước khi bấm xác nhận `DA_NHAN`. Nếu có rơi vỡ, thất thoát dọc đường, hệ thống phản ánh minh bạch và xác định chính xác trách nhiệm.

---

### 7. Kiểm kê & Cân bằng kho tự động (08:00 – 10:30 | 2.5 phút)
- **Nghiệp vụ tóm tắt**: Hỗ trợ kiểm kho định kỳ hoặc đột xuất (`frmKiemKho`). Thủ kho quét mã hoặc chọn mặt hàng, nhập số lượng đếm được thực tế tại kho.
- ⭐ **Điểm sáng đặc biệt**: **Cân bằng kho 1-click & Khóa phiếu vĩnh viễn chống gian lận**.
  - **Tự động hóa**: Khi nhấn "Cân bằng kho", hệ thống tự động đối chiếu số thực tế với tồn sổ sách realtime từ `vw_TonKhoHienTai`, tự tính độ lệch (lệch thừa/lệch thiếu đều được tô đỏ cảnh báo) và tự sinh Phiếu điều chỉnh kho để đưa số sách khớp 100% với thực tế.
  - **Khóa vĩnh viễn**: Khác với mọi phiếu khác, phiếu kiểm kê đã cân bằng **tuyệt đối không được phép Hủy hay Xóa**. Vì sau thời điểm cân bằng, kho đã phát sinh chuỗi giao dịch tiếp theo dựa trên mốc tồn mới, việc hủy phiếu cũ sẽ phá hủy toàn bộ tính đúng đắn của chuỗi dữ liệu phía sau.

---

### 8. Xuất hủy rủi ro hàng hỏng / hết date (10:30 – 11:30 | 1.0 phút)
- **Nghiệp vụ tóm tắt**: Xử lý các mặt hàng bị ẩm mốc, vỡ hỏng hoặc quá hạn sử dụng (`frmXuatHuy`), loại bỏ hàng ra khỏi kệ kinh doanh và hạch toán chi phí thiệt hại của chi nhánh.
- ⭐ **Điểm sáng đặc biệt**: **Bắt buộc phân loại nguyên nhân hủy & Cập nhật giá trị thiệt hại tức thời**.
  - Mỗi dòng xuất hủy bắt buộc gắn liền với lý do cụ thể và đơn giá vốn tại thời điểm hủy, giúp ban giám đốc có số liệu thống kê chính xác về tỷ lệ hao hụt rủi ro của từng kho để tối ưu quy trình bảo quản.

---

### 9. Báo cáo Tồn kho & Thẻ kho Đa chiều (11:30 – 12:30 | 1.0 phút)
- **Nghiệp vụ tóm tắt**: Cung cấp bức tranh toàn cảnh về tài sản kho: Báo cáo tổng hợp tồn kho theo nhóm, giá trị tồn kho theo kho, và Thẻ kho chi tiết cho từng mặt hàng.
- ⭐ **Điểm sáng đặc biệt**: **Truy vết Thẻ kho dòng chảy xuyên suốt (Complete Audit Trail)**.
  - Màn hình Thẻ kho (`frmBaoCaoTheKho`) cho phép tra cứu lịch sử chi tiết của một sản phẩm qua tất cả các nghiệp vụ: Tồn đầu kỳ, từng lần Nhập - Xuất - Chuyển - Trả - Kiểm kê - Xuất hủy, ai là người tạo phiếu, vào thời gian nào và số tồn luỹ kế biến động ra sao sau từng sự kiện.

---

### 10. Tổng kết & Điểm cộng Kỹ thuật (12:30 – 13:00 | 0.5 phút)
- Tóm tắt 3 thế mạnh công nghệ:
  1. **Tối ưu trải nghiệm (UX)**: Tích hợp **Search Debounce Timer 300ms** và tìm kiếm tiếng Việt không dấu tự động, loại bỏ hoàn toàn hiện tượng giật lag khi gõ phím.
  2. **Hiệu năng cao**: Cơ chế phân trang dữ liệu tận dụng sức mạnh SQL Server (`OFFSET...FETCH NEXT`).
  3. **Bảo mật & Toàn vẹn**: 100% câu truy vấn dùng `SqlParameter` chống SQL Injection, mật khẩu băm SHA-256 an toàn.
- Lời cảm ơn thầy cô và sẵn sàng bước vào phần trả lời phản biện.

---

## 💡 Bỏ túi 3 câu "chốt hạ" khi Hội đồng hỏi sâu về kỹ thuật:
1. **Hỏi: "Tại sao không tạo bảng `TonKho` có cột `SoLuongTon` rồi cứ nhập thì cộng, xuất thì trừ cho tiện?"**
   - *Trả lời*: Dạ thưa thầy/cô, trong hệ thống đa người dùng, lưu cột số lượng tĩnh rất dễ bị tranh chấp ghi (race condition) và lệch số khi có người sửa hoặc hủy phiếu quá khứ. Nhóm em dùng View động `vw_TonKhoHienTai` tính toán thời gian thực từ các chứng từ hợp lệ, đảm bảo số liệu tồn kho luôn khớp tuyệt đối với sổ sách chứng từ gốc.
2. **Hỏi: "Hệ thống làm sao đảm bảo khi nhập một phiếu 20 dòng hàng thì không bị tình trạng lưu được một nửa rồi mất điện sinh lỗi?"**
   - *Trả lời*: Dạ toàn bộ thao tác lưu Master-Detail trong nhóm em đều được bọc trong giao dịch `SqlTransaction` qua `DataProcessing.ChangeDataTransaction`. Chỉ khi cả phần Header và tất cả các dòng chi tiết ghi thành công thì giao dịch mới được Commit; nếu có bất kỳ lỗi nào xảy ra thì hệ thống tự động Rollback hoàn toàn về trạng thái ban đầu.
3. **Hỏi: "Tại sao phiếu kiểm kho đã cân bằng lại cấm không cho Hủy?"**
   - *Trả lời*: Dạ vì khi đã cân bằng, kho đã chốt một mốc tồn thực tế mới và các phiếu nhập/xuất sau đó đã tiếp tục phát sinh dựa trên mốc này. Nếu cho phép hủy phiếu kiểm kê cũ, toàn bộ số tồn hiện tại và các giao dịch tiếp theo sẽ bị sai dây chuyền. Nếu phát hiện có sai sót sau kiểm kê, thủ kho bắt buộc phải lập một phiếu kiểm kê mới theo đúng nguyên tắc kế toán kho chuyên nghiệp.
