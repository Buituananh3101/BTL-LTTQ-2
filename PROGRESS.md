# Tiến độ dự án BTL-LTTQ-2

**Cập nhật lần cuối**: 2026-10-26 – Hoàn thiện chức năng "Hiện cột" và chuẩn hóa logic form Thêm phiếu kiểm kho (Phiên 49)
---

## Đang làm
- [x] Chuẩn hóa giao diện DataGridView danh sách người dùng (`frmDanhSachNguoiDung`) đồng bộ 100% với khuôn mẫu `frmDanhSachHangHoa` (Header Checkbox, màu xen kẽ, selection pastel, co giãn AutoSizeMode Fill, màu trạng thái 3 nhóm).
- [x] Viết logic Đăng nhập (`frmDangNhap.cs`): tự động nạp kho làm việc theo vai trò và phân quyền trong `TaiKhoanKho`, kiểm tra mật khẩu và trạng thái hoạt động.
- [x] Viết logic Đổi mật khẩu (`frmDoiMatKhau.cs`): xác thực mật khẩu hiện tại và cập nhật mật khẩu mới.
- [x] Đồng bộ thông tin phiên `SessionContext` và phân quyền hiển thị trên `frmMain`.
- [x] Chuẩn hóa giao diện & logic Form Thêm phiếu kiểm kho (`frmThemPhieuKiemKho`): tích hợp nút "Hiện cột" và tạo mới form con `frmHienCotHangKiem`.
- [x] Kiểm tra mã hóa tự động `tools/KiemTraMaHoa.ps1`: 100% file đạt chuẩn UTF-8 with BOM, 0 lỗi Mojibake.
- [x] Kiểm tra biên dịch mã nguồn C# bằng MSBuild: **Build succeeded. 0 Warning(s), 0 Error(s)**.

## Việc kế tiếp
1. **Nghiệp vụ CRUD Người dùng**:
   - Viết logic tìm kiếm, phân trang và lọc cho danh sách người dùng (`frmDanhSachNguoiDung`).
   - Viết logic Tạo mới người dùng (`frmNguoiDung`) và Cập nhật/Khóa/Xóa tài khoản (`frmChiTietNguoiDung`) đồng bộ với `TaiKhoan` và `TaiKhoanKho`.
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
| 4 | **Xác thực & Phân quyền** | SessionContext, SHA-256 PasswordHelper, rmDangNhap, phân quyền menu theo 3 vai trò | Hoàn thành 100% |
| 5 | **Danh mục Hàng hóa** | `frmDanhSachHangHoa` & `frmHangHoa` (**Khuôn mẫu Thiết kế chuẩn UI/UX toàn hệ thống**) | Hoàn thành 100% |
| 6 | **Danh mục Nhà cung cấp** | `frmNhaCungCap`, `frmDonViCapHang`, `frmNhomNhaCungCap`, `frmHienCotNcc`, `frmNhapNhaCungCapFile` | Hoàn thành 100% |
| 7 | **Phiếu Nhập hàng** | `frmNhapHang`, `frmThemPhieuNhap`, `frmChiTietPhieuNhap`, `frmHienCotNhapHang` | Hoàn thành 100% |
| 8 | **Trả hàng nhập** | `frmTraHangNhap`, `frmThemPhieuTraHang`, `frmChiTietTraHangNhap`, `frmHienCotTraHang` | Hoàn thành 100% |
| 9 | **Danh mục Đơn vị tính** | `frmDonViTinh`, `frmChiTietDonViTinh`, `frmHienCotDvt`, tạo nhanh từ `frmHangHoa` | Hoàn thành 100% |
| 10 | **Danh mục Cơ bản khác** | CRUD Nhóm hàng, Vị trí, Chi nhánh Kho, Khách hàng | Chưa bắt đầu |
| 11 | **Kiểm kê & Cân bằng kho** | `frmKiemKho`, nạp tồn sổ sách, sinh phiếu điều chỉnh, khóa vĩnh viễn, `frmThemPhieuKiemKho`, `frmHienCotHangKiem` | Hoàn thành 100% |
| 12 | **Chuyển kho Liên chi nhánh & Đổi kho** | `frmChuyenKho`, 2 bước Xuất kho ➜ Nhận kho, chặn tồn âm kho nguồn, modal `frmDoiKho` | Hoàn thành 100% |
| 13 | **Xuất hủy** | `frmXuatHuy` | Chưa bắt đầu |
| 14 | **Báo cáo Tồn kho & Thẻ kho** | Báo cáo tồn đa kho (`frmBaoCaoTonKho`), Thẻ kho (`frmBaoCaoTheKho`) từ `vw_BienDongKho`, xuất CSV UTF-8 BOM | Hoàn thành 100% |

---

## Nhật ký phiên (Session Logs)

- **Phiên 49 (2026-10-26) – Chuẩn hóa logic Form Thêm phiếu kiểm kho và Xây dựng Form chức năng "Hiện cột" hàng kiểm**:
  - **Nhu cầu**: Người dùng đã cập nhật giao diện `frmThemPhieuKiemKho`: bỏ nút "Nạp tất cả hàng trong kho", thêm nút "Hiện cột" (`button2`), đổi tên nút thêm thành `+ Thêm`. Yêu cầu giữ nguyên 100% giao diện đã chỉnh, cấu hình lại tên và logic điều khiển, đồng thời tạo mới form popup "Hiện cột".
  - **Kết quả thực hiện**:
    1. **Tạo mới `frmHienCotHangKiem` (`frmHienCotHangKiem.cs`, `.Designer.cs`, `.resx`)**:
       - Khung popup ẩn/hiện cột cho lưới chi tiết hàng kiểm `dgvHangKiem`.
       - Danh sách 8 checkbox tương ứng các cột: Mã hàng nội bộ, Tên hàng, Đơn vị tính, Tồn kho, SL thực tế, SL lệch, Đơn giá, Giá trị lệch (cột STT và nút Xóa dòng cố định).
       - Tự động gán vị trí neo thông minh ngay dưới nút `btnHienCot`, chống tràn màn hình, phím ESC và sự kiện Deactivate để đóng nhẹ nhàng.
    2. **Cập nhật `frmThemPhieuKiemKho`**:
       - Đổi tên `button2` thành `btnHienCot` trong `frmThemPhieuKiemKho.Designer.cs`, gán sự kiện `btnHienCot_Click`.
       - Thay thế hàm `btnNapToanBoHang_Click` thành `btnHienCot_Click` gọi mở form `frmHienCotHangKiem`.
       - Giữ nguyên 100% kích thước, vị trí tọa độ mà người dùng đã thiết kế.
       - Chuẩn hóa thao tác xóa dòng trong `dgvHangKiem`: Bỏ cột nút Xóa (`colXoaDong`), thay thế bằng sự kiện nhấn đúp (`CellDoubleClick`) vào dòng mặt hàng để xóa, hiển thị hộp thoại xác nhận Yes/No trước khi xóa.
       - Chuẩn hóa cột lưới hàng kiểm: Ẩn mặc định cột Đơn vị tính (`colDonViTinh.Visible = false`), bỏ chữ "(nhập)" trên tiêu đề cột thành "SL thực tế", đồng bộ mặc định bỏ tích ĐVT trong popup `frmHienCotHangKiem`.
       - Khắc phục lỗi tràn/xuống dòng tiêu đề cột trên `dgvHangKiem`: Tắt WrapMode header (`WrapMode = False`), cố định chiều cao header 36px và tăng độ rộng các cột (Mã hàng: 110, Tồn kho: 90, SL lệch: 90, Đơn giá: 100), bảo đảm toàn bộ tiêu đề cột hiển thị trên 1 dòng duy nhất.
    3. **Kiểm tra mã hóa & biên dịch**:
       - Đăng ký form mới đầy đủ trong `BTL-LTTQ-2.csproj`.
       - Chạy `tools/KiemTraMaHoa.ps1`: **180/180 file đạt chuẩn UTF-8 with BOM, 0 lỗi Mojibake**.
       - Biên dịch MSBuild: **Build succeeded. 0 Warning(s), 0 Error(s)**.

- **Phiên 48 (2026-10-26) – Chuẩn hóa phân hệ Quản lý Người dùng và Hoàn thiện toàn diện Logic Đăng nhập / Đổi mật khẩu**:
  - **Nhu cầu**: Người dùng yêu cầu:
    1. Kiểm tra và chuẩn hóa bộ Form Người dùng (`frmDanhSachNguoiDung`, `frmNguoiDung`, `frmChiTietNguoiDung`, `frmHienCotNguoiDung`): font Segoe UI 9.5pt, layout, thứ tự nút bấm theo Rule 19b và 15d.
    2. Viết toàn bộ logic xác thực cho chức năng Đăng nhập (`frmDangNhap`): nạp kho tự động theo tài khoản/vai trò, so sánh mật khẩu, kiểm tra trạng thái tài khoản và khởi tạo `SessionContext`.
    3. Đảm bảo hỗ trợ đăng nhập mật khẩu chuỗi thuần plain-text lẫn băm SHA-256 từ dữ liệu mẫu.
  - **Kết quả thực hiện**:
    1. **Tự động nhận diện tài khoản và tải kho làm việc (`CapNhatDanhSachKhoTheoTaiKhoan`)**:
       - Bắt sự kiện khi người dùng gõ tên đăng nhập và rời ô (`txtTenDangNhap_Leave`) hoặc nhấn Enter (`KeyDown`).
       - Truy vấn vai trò của tài khoản từ CSDL:
         + Nếu là **Quản lý (`MaVaiTro = 1`)**: Tải toàn bộ kho đang hoạt động từ bảng `Kho`, chèn tùy chọn `"Tất cả kho"` (`MaKho = 0`) ở đầu danh sách, cho phép chuyển đổi kho (`cboKhoLamViec.Enabled = true`).
         + Nếu là **Thủ kho (`MaVaiTro = 2`)** hoặc **Nhân viên kho (`MaVaiTro = 3`)**: Truy vấn các kho được phân công từ bảng `TaiKhoanKho`. Nếu chỉ có 1 kho duy nhất thì tự động chọn và khóa ComboBox (`cboKhoLamViec.Enabled = false`); nếu có nhiều kho được phân công thì cho phép chọn.
    2. **Xác thực đăng nhập và nạp phiên làm việc (`btnDangNhap_Click`)**:
       - Kiểm tra rỗng tên đăng nhập và mật khẩu với thông báo rõ ràng.
       - Truy vấn thông tin tài khoản qua `SqlParameter` (chống SQL Injection 100%).
       - Kiểm tra mật khẩu: So sánh trực tiếp chuỗi thuần plain-text hoặc SHA-256 đối với các tài khoản seed mặc định.
       - Kiểm tra trạng thái tài khoản: Nếu không phải `HOAT_DONG` thì từ chối và cảnh báo.
       - Cập nhật thời điểm `LanDangNhapCuoi = GETDATE()`.
       - Gán đầy đủ thông tin vào `SessionContext` (`MaTaiKhoan`, `TenDangNhap`, `MaNV`, `HoTen`, `MaVaiTro`, `TenVaiTro`, `MaKhoHienTai`, `TenKhoHienTai`, `IsDangXuat = false`).
       - Trả về `DialogResult.OK` để `Program.cs` khởi chạy `frmMain`.
    3. **Hoàn thiện logic Đổi mật khẩu (`frmDoiMatKhau.cs`)**:
       - Bắt buộc nhập mật khẩu cũ, mật khẩu mới, xác nhận mật khẩu mới.
       - Kiểm tra mật khẩu cũ khớp với CSDL trước khi cho phép cập nhật.
       - Lưu mật khẩu mới an toàn bằng `SqlParameter`.
    4. **Kiểm tra chất lượng**:
       - Chạy script kiểm thử logic trực tiếp trên CSDL: Xác thực thành công cho cả 4 tài khoản mẫu (`quanly`, `thukho`, `banhang`, `Tuna`).
       - Chạy `tools/KiemTraMaHoa.ps1`: **177/177 file đạt chuẩn UTF-8 with BOM, 0 lỗi Mojibake**.
       - Biên dịch MSBuild: **Build succeeded. 0 Warning(s), 0 Error(s)**.

- **Phiên 47 (2026-10-25) – Xây dựng Form Đăng nhập hệ thống, Tiện ích băm mật khẩu SHA-256 an toàn và Phân quyền thao tác theo 3 vai trò chuẩn**:
  - **Nhu cầu**: Người dùng yêu cầu:
    1. Kiểm tra 1 lượt classes và các file liên quan có thể có chứa hàm băm của mật khẩu. Nếu tồn tại, gọi hàm hash đó; nếu chưa, cho phép tạo class `PasswordHelper.cs`.
    2. Tạo form đăng nhập cho hệ thống, khi bắt đầu chạy chương trình yêu cầu đăng nhập với tài khoản và mật khẩu để phân quyền thao tác được phép.
    3. Hỗ trợ 3 vai trò người dùng chuẩn:
       - *Quản lý*: Quản lý mọi kho hàng trong hệ thống.
       - *Thủ kho*: Quản lý 1 kho duy nhất được phân công.
       - *Nhân viên kho*: Thao tác nghiệp vụ tại kho.
  - **Kết quả thực hiện**:
    1. **Kiểm tra và xây dựng tiện ích băm mật khẩu SHA-256 (`Classes/PasswordHelper.cs`)**:
       - Quét toàn bộ codebase xác nhận chưa có hàm băm mật khẩu.
       - Tạo mới `Classes/PasswordHelper.cs` cung cấp `HashPassword(string plainPassword)` (SHA-256 chữ thường) và `VerifyPassword(string plainPassword, string hashedPassword)`.
    2. **Xây dựng Form Đăng nhập (`frmDangNhap`) chuẩn WinForms**:
       - Kích thước form Size Nhỏ `ClientSize = (444, 321)`, `FixedDialog`, `CenterScreen`.
       - Gồm các control: Tiêu đề hệ thống, Tên đăng nhập (`txtTenDangNhap`), Mật khẩu (`txtMatKhau`), CheckBox Hiện mật khẩu (`chkHienMatKhau`), ComboBox Kho làm việc (`cboKhoLamViec`), nút Đăng nhập (`btnDangNhap`) và Thoát (`btnThoat`).
       - Tự động nhận diện vai trò tài khoản khi nhập tên đăng nhập để nạp danh sách kho tương ứng.
    3. **Phân quyền thao tác và phạm vi kho chuẩn hóa theo 3 vai trò**:
       - *Quản lý (`MaVaiTro = 1`)*: Toàn quyền truy cập tất cả các phân hệ và menu; quản lý mọi kho hàng; được chọn "Tất cả kho" hoặc kho chi nhánh bất kỳ khi đăng nhập và có quyền đổi kho (`frmDoiKho`).
       - *Thủ kho (`MaVaiTro = 2`)*: Quản lý 1 kho duy nhất được phân công (cố định ComboBox kho khi đăng nhập, ẩn/khóa menu "Đổi kho"); toàn quyền các nghiệp vụ kho của mình (Kiểm kho, Chuyển kho, Hàng hóa, Nhà cung cấp, Nhập hàng, Trả hàng nhập, Báo cáo tồn kho & Thẻ kho).
       - *Nhân viên kho (`MaVaiTro = 3`)*: Thao tác tại kho được phân công (ẩn menu "Đổi kho"); thực hiện Kiểm kho, Chuyển kho, Nhập hàng, Trả hàng nhập, xem Danh sách hàng hóa, xem Báo cáo tồn kho; ẩn các mục quản lý đối tác (Nhà cung cấp) và Thẻ kho sổ sách.
    4. **Cơ chế khởi động chương trình và Đăng xuất linh hoạt**:
       - Tại `Program.cs`: Bọc vòng lặp `while (true)` yêu cầu đăng nhập trước khi hiển thị `frmMain`.
       - Tại `frmMain.cs`: Khi người dùng nhấn "Đăng xuất" trên menu Hệ thống, hệ thống xóa sạch phiên làm việc `SessionContext.XoaPhien()`, đóng `frmMain` và quay lại màn hình `frmDangNhap` ngay lập tức.
    5. **Đồng bộ CSDL & Dữ liệu mẫu**:
       - Cập nhật script `docs/database_update.sql` và `docs/DuLieuMau.sql`: Đồng bộ 3 vai trò chuẩn trong `VaiTro`, 3 nhân viên trong `NhanVien`, 3 tài khoản demo (`quanly`, `thukho`, `nvkho` mật khẩu mặc định `123456`), và phân quyền kho trong `TaiKhoanKho`.
    6. **Kiểm tra chất lượng**:
       - Chạy `tools/KiemTraMaHoa.ps1`: **158/158 file đạt chuẩn UTF-8 with BOM, 0 lỗi Mojibake**.
       - Biên dịch MSBuild: **Build succeeded. 0 Warning(s), 0 Error(s)**.

- **Phiên 46 (2026-10-25) – Khắc phục lỗi Hiện cột Chuyển kho, Chuẩn hóa TextBox Mã phiếu tự chọn có check trùng, Mở rộng chiều rộng và Tối ưu cơ chế nhập Số lượng nguyên trên DataGridView của 4 form thêm mới**:
  - **Nhu cầu**: Người dùng yêu cầu qua 4 ảnh:
    1. Lỗi bỏ chọn trong Hiện cột nhưng không bị ẩn trong chức năng Chuyển kho (ảnh 1).
    2. Khi tạo phiếu, mã phiếu là ô textBox đã được điền sẵn mã tự động nhưng vẫn cho phép người dùng nhập mã tự chọn (có check trùng mã) như trong ảnh 2 chứ không để thành label như ảnh 3.
    3. Các form: Thêm phiếu kiểm kho, Thêm phiếu chuyển kho, Thêm phiếu nhập hàng và Thêm phiếu trả hàng nhập:
       - Tăng chiều rộng form để thông tin được hiển thị rộng rãi hơn (nhưng không được bằng hoặc vượt quá form cha).
       - Cho phép nhập số lượng trong dgv, có dấu hiệu rõ ràng cho người dùng nhận biết (như ảnh 4). Phần nhập số lượng phải cho enter hoặc kích chuột ra ngoài để nhận số lượng nhập vào (hiện tại khi chỉ thêm vào 1 mặt hàng, nhấn số lượng và ấn enter mà form vẫn không nhận số lượng mới) và chỉ cho phép nhập vào số nguyên.
  - **Kết quả thực hiện**:
    1. **Sửa lỗi Hiện cột Chuyển kho (`frmHienCotChuyenKho`)**:
       - Sửa khớp tên cột `"colTongSlChuyen"`, `"colTongSlNhan"`, `"colTongGiaTri"` khi đồng bộ trạng thái ban đầu và khi bắt sự kiện `CheckedChanged`, đảm bảo ẩn/hiện cột ngay lập tức và chính xác 100%.
    2. **Chuẩn hóa ô TextBox Mã phiếu tự chọn có check trùng**:
       - Trên cả 4 Form thêm mới (`frmThemPhieuKiemKho`, `frmThemPhieuChuyenKho`, `frmThemPhieuNhap`, `frmThemPhieuTraHang`), chuyển mã phiếu thành `TextBox` (`txtMaPhieu`) được tự động gợi ý mã chuẩn (PKKxxxxxx, CKxxxxxx, PNxxxxxx, THNxxxxxx).
       - Cho phép người dùng chỉnh sửa hoặc tự đặt mã theo ý muốn.
       - Khi ấn Lưu hoặc Lưu tạm, hệ thống bắt buộc kiểm tra không để trống và truy vấn CSDL kiểm tra chống trùng mã (`COUNT(1) ... WHERE MaPhieu = @MaPhieu`).
    3. **Tăng kích thước chiều rộng form (`ClientSize = (1400, 803)`)**:
       - Cả 4 form thêm mới được mở rộng chiều rộng từ 1182px lên 1400px (nhỏ hơn form cha 1782px), giải phóng không gian cho khối DataGridView trung tâm rộng rãi hơn 218px, hiển thị đầy đủ tên hàng hóa dài, đơn giá, thành tiền/chênh lệch và nút Xóa dòng.
    4. **Tối ưu trải nghiệm và ràng buộc nhập số lượng nguyên trên DataGridView**:
       - Cột số lượng trên cả 4 form có nền nhận diện `#FEFCE8`, HeaderText gắn nhãn `(nhập)` rõ ràng ("Số lượng (nhập)", "SL thực tế (nhập)", "SL chuyển (nhập)", "SL trả (nhập)").
       - Kích hoạt `EditMode = EditOnEnter` cho phép click 1 lần nhập ngay.
       - Xử lý phím Enter (`ProcessCmdKey` & `EditingControlShowing.KeyDown`) và kích chuột ra ngoài (`Leave`, `CellEndEdit`, `btnLuu.Click`): gọi `dgv.EndEdit()` và commit BindingContext, sửa triệt để lỗi khi bảng chỉ có 1 mặt hàng nhấn Enter hoặc click ra ngoài form không nhận số lượng mới.
       - Ràng buộc kiểu dữ liệu: Chặn gõ chữ cái, dấu chấm, dấu phẩy, dấu âm qua `KeyPress` (`char.IsDigit`), chỉ cho phép nhập số nguyên dương (đối với kiểm kho là số nguyên $\ge 0$).
    5. **Kiểm tra chất lượng**:
       - Chạy `tools/KiemTraMaHoa.ps1`: **153/153 file đạt chuẩn UTF-8 with BOM, 0 lỗi Mojibake**.
       - Biên dịch MSBuild: **Build succeeded. 0 Warning(s), 0 Error(s)**.

- **Phiên 45 (2026-10-25) – Bỏ trường trạng thái tĩnh trên các form thêm mới, Tối ưu giao diện & nghiệp vụ Kiểm kho, Tái thiết kế Form Thêm phiếu chuyển kho sang Right Menu Layout**:
  - **Nhu cầu**: Người dùng yêu cầu qua 4 ảnh:
    1. Bỏ trường trạng thái trong các phiếu thêm (ảnh 3) (Chuyển kho, Nhập hàng, Trả hàng nhập, Kiểm kho). Trạng thái sẽ được quyết định và hiển thị lên dgv sau khi người dùng ấn Lưu tạm hay Lưu.

- **Phiên 47 (2026-10-25) – Xây dựng Form Đăng nhập hệ thống, Tiện ích băm mật khẩu SHA-256 an toàn và Phân quyền thao tác theo 3 vai trò chuẩn**:
  - **Nhu cầu**: Người dùng yêu cầu:
    1. Kiểm tra 1 lượt classes và các file liên quan có thể có chứa hàm băm của mật khẩu. Nếu tồn tại, gọi hàm hash đó; nếu chưa, cho phép tạo class `PasswordHelper.cs`.
    2. Tạo form đăng nhập cho hệ thống, khi bắt đầu chạy chương trình yêu cầu đăng nhập với tài khoản và mật khẩu để phân quyền thao tác được phép.
    3. Hỗ trợ 3 vai trò người dùng chuẩn:
       - *Quản lý*: Quản lý mọi kho hàng trong hệ thống.
       - *Thủ kho*: Quản lý 1 kho duy nhất được phân công.
       - *Nhân viên kho*: Thao tác nghiệp vụ tại kho.
  - **Kết quả thực hiện**:
    1. **Kiểm tra và xây dựng tiện ích băm mật khẩu SHA-256 (`Classes/PasswordHelper.cs`)**:
       - Quét toàn bộ codebase xác nhận chưa có hàm băm mật khẩu.
       - Tạo mới `Classes/PasswordHelper.cs` cung cấp `HashPassword(string plainPassword)` (SHA-256 chữ thường) và `VerifyPassword(string plainPassword, string hashedPassword)`.
    2. **Xây dựng Form Đăng nhập (`frmDangNhap`) chuẩn WinForms**:
       - Kích thước form Size Nhỏ `ClientSize = (444, 321)`, `FixedDialog`, `CenterScreen`.
       - Gồm các control: Tiêu đề hệ thống, Tên đăng nhập (`txtTenDangNhap`), Mật khẩu (`txtMatKhau`), CheckBox Hiện mật khẩu (`chkHienMatKhau`), ComboBox Kho làm việc (`cboKhoLamViec`), nút Đăng nhập (`btnDangNhap`) và Thoát (`btnThoat`).
       - Tự động nhận diện vai trò tài khoản khi nhập tên đăng nhập để nạp danh sách kho tương ứng.
    3. **Phân quyền thao tác và phạm vi kho chuẩn hóa theo 3 vai trò**:
       - *Quản lý (`MaVaiTro = 1`)*: Toàn quyền truy cập tất cả các phân hệ và menu; quản lý mọi kho hàng; được chọn "Tất cả kho" hoặc kho chi nhánh bất kỳ khi đăng nhập và có quyền đổi kho (`frmDoiKho`).
       - *Thủ kho (`MaVaiTro = 2`)*: Quản lý 1 kho duy nhất được phân công (cố định ComboBox kho khi đăng nhập, ẩn/khóa menu "Đổi kho"); toàn quyền các nghiệp vụ kho của mình (Kiểm kho, Chuyển kho, Hàng hóa, Nhà cung cấp, Nhập hàng, Trả hàng nhập, Báo cáo tồn kho & Thẻ kho).
       - *Nhân viên kho (`MaVaiTro = 3`)*: Thao tác tại kho được phân công (ẩn menu "Đổi kho"); thực hiện Kiểm kho, Chuyển kho, Nhập hàng, Trả hàng nhập, xem Danh sách hàng hóa, xem Báo cáo tồn kho; ẩn các mục quản lý đối tác (Nhà cung cấp) và Thẻ kho sổ sách.
    4. **Cơ chế khởi động chương trình và Đăng xuất linh hoạt**:
       - Tại `Program.cs`: Bọc vòng lặp `while (true)` yêu cầu đăng nhập trước khi hiển thị `frmMain`.
       - Tại `frmMain.cs`: Khi người dùng nhấn "Đăng xuất" trên menu Hệ thống, hệ thống xóa sạch phiên làm việc `SessionContext.XoaPhien()`, đóng `frmMain` và quay lại màn hình `frmDangNhap` ngay lập tức.
    5. **Đồng bộ CSDL & Dữ liệu mẫu**:
       - Cập nhật script `docs/database_update.sql` và `docs/DuLieuMau.sql`: Đồng bộ 3 vai trò chuẩn trong `VaiTro`, 3 nhân viên trong `NhanVien`, 3 tài khoản demo (`quanly`, `thukho`, `nvkho` mật khẩu mặc định `123456`), và phân quyền kho trong `TaiKhoanKho`.
    6. **Kiểm tra chất lượng**:
       - Chạy `tools/KiemTraMaHoa.ps1`: **158/158 file đạt chuẩn UTF-8 with BOM, 0 lỗi Mojibake**.
       - Biên dịch MSBuild: **Build succeeded. 0 Warning(s), 0 Error(s)**.
    2. Chức năng Kiểm kho (ảnh 1 & ảnh 4):
       - Ảnh 1: Phần xóa hàng khỏi bảng đang bị che mất do thiếu không gian -> Chuyển sang form Size Lớn (1182, 803) để chứa đầy đủ thông tin. Phần số lượng thực tế cho phép nhập trực tiếp từ dgv cần thể hiện rõ ràng, phân biệt rõ có cho nhập vào hay không.
       - Ảnh 4: Trong filter trạng thái bỏ chọn trạng thái "Đã hủy" (kiểm kho không có trạng thái Đã hủy). Khi đã cân bằng thì không cho xóa, khi là phiếu tạm thì xóa hẳn khỏi CSDL.
    3. Chức năng Chuyển kho (ảnh 2): Thiết kế phần Right Menu để nhập thông tin và phần dgv để xem thông tin hàng hóa chuyển là chính theo khuôn mẫu form Thêm phiếu kiểm kho.
  - **Kết quả thực hiện**:
    1. **Bỏ trường trạng thái tĩnh trên 4 form thêm mới**:
       - Tại `frmThemPhieuNhap.Designer.cs`, `frmThemPhieuTraHang.Designer.cs`, `frmThemPhieuKiemKho.Designer.cs`, `frmThemPhieuChuyenKho.Designer.cs`: Xóa bỏ nhãn tiêu đề `lblTrangThaiTitle` và giá trị `lblTrangThaiVal`, dời các control phía dưới lên lấp đầy khoảng trống, căn chỉnh layout sắc nét.
       - Trạng thái chứng từ được quyết định tự động và hiển thị lên DataGridView danh sách khi người dùng nhấn "Lưu tạm" (`NHAP` - Phiếu tạm) hoặc "Lưu" (`DA_NHAP_HANG`, `DA_TRA_HANG`, `DA_DIEU_CHINH`, `DANG_CHUYEN`).
    2. **Kiểm kho - Form Thêm mới (`frmThemPhieuKiemKho`)**:
       - Chuyển form sang Size Lớn `ClientSize = (1182, 803)` theo Điều luật 15e.
       - Phân bổ lại chiều rộng các cột hiển thị trên DataGridView: `colStt` (45), `colMaHangNoiBo` (85), `colTenHang` (Fill, Min 120), `colDonViTinh` (55), `colTonKho` (75), `colSoLuongThucTe` (110), `colSlChenhLech` (75), `colDonGia` (85), `colGiaTriChenhLech` (100), `colXoaDong` (45). Tổng chiều rộng các cột cố định 675px nhỏ hơn chiều rộng grid 812px, đảm bảo cột Xóa dòng hiển thị 100% không bị che khuất.
       - Làm rõ ô nhập SL thực tế: Đổi `HeaderText = "SL thực tế (nhập)"`, tô màu nền dịu nổi bật `#FEFCE8`, thêm `ToolTipText` hướng dẫn và bật `dgvHangKiem.EditMode = DataGridViewEditMode.EditOnEnter` click 1 lần cho phép nhập ngay.
    3. **Kiểm kho - Danh sách & Chi tiết (`frmKiemKho`, `frmChiTietKiemKho`)**:
       - Bỏ lựa chọn "Đã hủy" khỏi ComboBox filter trạng thái `cboTrangThai` và câu lệnh SQL tải dữ liệu.
       - Tại `frmChiTietKiemKho.cs`: Đối với phiếu đã cân bằng (`DA_DIEU_CHINH`), vô hiệu hóa nút xóa `btnXoa.Enabled = false`, giữ nguyên Text "Xóa" (tuân thủ Điều luật 15d & Luật 6).
       - Đối với phiếu tạm (`NHAP`): Khi người dùng chọn Xóa, hệ thống xóa vĩnh viễn dữ liệu khỏi CSDL (`DELETE FROM ChiTietPhieuKiemKe`, `DELETE FROM PhieuKiemKe`).
    4. **Chuyển kho - Tái thiết kế Form Thêm mới (`frmThemPhieuChuyenKho`)**:
       - Tái thiết kế toàn bộ theo bố cục Right Menu Layout chuẩn mực của `frmThemPhieuKiemKho`: Form Size Lớn `ClientSize = (1182, 803)`.
       - Lưới hàng hóa chuyển trung tâm bên trái (rộng 842px) với các cột STT, Mã hàng, Tên hàng, ĐVT, Tồn kho gửi, SL chuyển (nền `#FEFCE8`, sửa trực tiếp), Đơn giá, Thành tiền, Xóa dòng.
       - Right Menu bên phải (rộng 340px) chứa thông tin Người lập, Ngày lập, Mã phiếu (CKxxxxxx tự sinh), Kho gửi, Kho nhận, Ghi chú, thống kê Số mặt hàng / Tổng SL chuyển / Tổng giá trị chuyển. Đáy là cụm 3 nút hành động theo thứ tự chuẩn Điều luật 19b: `Bỏ qua` ➜ `Lưu tạm` ➜ `Lưu`.
    5. **Đồng bộ tài liệu & Kiểm tra chất lượng**:
       - Cập nhật tài liệu đặc tả `docs/specs/kiem-kho.md` và `docs/specs/chuyen-kho.md`.
       - Cập nhật sổ quyết định `docs/quyet-dinh.md` và tiến độ `PROGRESS.md`.
       - Chạy `tools/KiemTraMaHoa.ps1`: **153/153 file đạt chuẩn UTF-8 with BOM, 0 lỗi Mojibake**.
       - Biên dịch MSBuild: **Build succeeded. 0 Warning(s), 0 Error(s)**.

- **Phiên 44 (2026-10-25) – Thiết lập Ngữ cảnh Kho làm việc Tập trung Toàn hệ thống, Thêm lựa chọn "Tất cả kho", Bỏ bộ lọc kho riêng lẻ và Chuẩn hóa hiển thị cột Kho**:
  - **Nhu cầu**: Người dùng yêu cầu:
    1. Bỏ phần cho phép lựa chọn kho trong các form: Kiểm kho, Báo cáo tồn kho, Báo cáo thẻ kho (các filter Chi nhánh kho) và Chuyển kho (filter Kho đối ứng).
    2. Phần chức năng đổi kho, thêm 1 lựa chọn nữa vào comboBox là "Tất cả kho".
    3. Khi ở trong kho làm việc nào, chỉ hiển thị dữ liệu của kho đó trong tất cả chức năng:
       - Kiểm kho: Chỉ hiện phiếu của kho đang làm việc, thêm cột Kho trong dgv hiển thị tên kho (mặc định ẩn, bật/tắt qua Hiện cột). Khi thêm phiếu comboBox chọn kho chỉ hiển thị tên kho đang làm việc, không cho phép sửa đổi (trừ khi ở Tất cả kho).
       - Chuyển kho: Chỉ hiển thị phiếu khi kho đang làm việc là kho gửi hoặc kho nhận, khi thêm phiếu comboBox kho gửi chỉ hiển thị tên kho đang làm việc, không cho phép sửa đổi (trừ khi ở Tất cả kho).
       - Danh sách hàng hóa: Số lượng tồn kho hiển thị theo đúng số lượng tồn của kho đang làm việc.
       - Nhập hàng: Thêm cột Kho trong dgv hiển thị tên kho (mặc định ẩn, bật/tắt qua Hiện cột). Khi thêm phiếu comboBox chọn kho chỉ hiển thị tên kho đang làm việc, không cho phép sửa đổi (trừ khi ở Tất cả kho).

- **Phiên 47 (2026-10-25) – Xây dựng Form Đăng nhập hệ thống, Tiện ích băm mật khẩu SHA-256 an toàn và Phân quyền thao tác theo 3 vai trò chuẩn**:
  - **Nhu cầu**: Người dùng yêu cầu:
    1. Kiểm tra 1 lượt classes và các file liên quan có thể có chứa hàm băm của mật khẩu. Nếu tồn tại, gọi hàm hash đó; nếu chưa, cho phép tạo class `PasswordHelper.cs`.
    2. Tạo form đăng nhập cho hệ thống, khi bắt đầu chạy chương trình yêu cầu đăng nhập với tài khoản và mật khẩu để phân quyền thao tác được phép.
    3. Hỗ trợ 3 vai trò người dùng chuẩn:
       - *Quản lý*: Quản lý mọi kho hàng trong hệ thống.
       - *Thủ kho*: Quản lý 1 kho duy nhất được phân công.
       - *Nhân viên kho*: Thao tác nghiệp vụ tại kho.
  - **Kết quả thực hiện**:
    1. **Kiểm tra và xây dựng tiện ích băm mật khẩu SHA-256 (`Classes/PasswordHelper.cs`)**:
       - Quét toàn bộ codebase xác nhận chưa có hàm băm mật khẩu.
       - Tạo mới `Classes/PasswordHelper.cs` cung cấp `HashPassword(string plainPassword)` (SHA-256 chữ thường) và `VerifyPassword(string plainPassword, string hashedPassword)`.
    2. **Xây dựng Form Đăng nhập (`frmDangNhap`) chuẩn WinForms**:
       - Kích thước form Size Nhỏ `ClientSize = (444, 321)`, `FixedDialog`, `CenterScreen`.
       - Gồm các control: Tiêu đề hệ thống, Tên đăng nhập (`txtTenDangNhap`), Mật khẩu (`txtMatKhau`), CheckBox Hiện mật khẩu (`chkHienMatKhau`), ComboBox Kho làm việc (`cboKhoLamViec`), nút Đăng nhập (`btnDangNhap`) và Thoát (`btnThoat`).
       - Tự động nhận diện vai trò tài khoản khi nhập tên đăng nhập để nạp danh sách kho tương ứng.
    3. **Phân quyền thao tác và phạm vi kho chuẩn hóa theo 3 vai trò**:
       - *Quản lý (`MaVaiTro = 1`)*: Toàn quyền truy cập tất cả các phân hệ và menu; quản lý mọi kho hàng; được chọn "Tất cả kho" hoặc kho chi nhánh bất kỳ khi đăng nhập và có quyền đổi kho (`frmDoiKho`).
       - *Thủ kho (`MaVaiTro = 2`)*: Quản lý 1 kho duy nhất được phân công (cố định ComboBox kho khi đăng nhập, ẩn/khóa menu "Đổi kho"); toàn quyền các nghiệp vụ kho của mình (Kiểm kho, Chuyển kho, Hàng hóa, Nhà cung cấp, Nhập hàng, Trả hàng nhập, Báo cáo tồn kho & Thẻ kho).
       - *Nhân viên kho (`MaVaiTro = 3`)*: Thao tác tại kho được phân công (ẩn menu "Đổi kho"); thực hiện Kiểm kho, Chuyển kho, Nhập hàng, Trả hàng nhập, xem Danh sách hàng hóa, xem Báo cáo tồn kho; ẩn các mục quản lý đối tác (Nhà cung cấp) và Thẻ kho sổ sách.
    4. **Cơ chế khởi động chương trình và Đăng xuất linh hoạt**:
       - Tại `Program.cs`: Bọc vòng lặp `while (true)` yêu cầu đăng nhập trước khi hiển thị `frmMain`.
       - Tại `frmMain.cs`: Khi người dùng nhấn "Đăng xuất" trên menu Hệ thống, hệ thống xóa sạch phiên làm việc `SessionContext.XoaPhien()`, đóng `frmMain` và quay lại màn hình `frmDangNhap` ngay lập tức.
    5. **Đồng bộ CSDL & Dữ liệu mẫu**:
       - Cập nhật script `docs/database_update.sql` và `docs/DuLieuMau.sql`: Đồng bộ 3 vai trò chuẩn trong `VaiTro`, 3 nhân viên trong `NhanVien`, 3 tài khoản demo (`quanly`, `thukho`, `nvkho` mật khẩu mặc định `123456`), và phân quyền kho trong `TaiKhoanKho`.
    6. **Kiểm tra chất lượng**:
       - Chạy `tools/KiemTraMaHoa.ps1`: **158/158 file đạt chuẩn UTF-8 with BOM, 0 lỗi Mojibake**.
       - Biên dịch MSBuild: **Build succeeded. 0 Warning(s), 0 Error(s)**.
       - Trả hàng nhập: Thêm cột Kho trong dgv hiển thị tên kho (mặc định ẩn, bật/tắt qua Hiện cột). Khi thêm phiếu comboBox chọn kho chỉ hiển thị tên kho đang làm việc, không cho phép sửa đổi (trừ khi ở Tất cả kho).
       - Báo cáo tồn kho & Thẻ kho: Chỉ hiển thị dữ liệu báo cáo của đúng kho đang làm việc, ẩn cột Chi nhánh kho (thay tên cột thành Kho) trong dgv, khi người dùng chọn hiển thị trong Hiện cột mới hiển thị lên.
  - **Kết quả thực hiện**:
    1. **Bổ sung lựa chọn "Tất cả kho" tại `frmDoiKho`**:
       - Thêm mục `drAll["MaKho"] = 0`, `drAll["TenKho"] = "Tất cả kho"` vào đầu ComboBox đổi kho.
       - Cập nhật `SessionContext.MaKhoHienTai = 0`, `SessionContext.TenKhoHienTai = "Tất cả kho"`.
       - Tại `frmMain.cs`, cập nhật tiêu đề form cha và tự động làm mới toàn bộ 7 form con MDI đang mở (`frmChuyenKho`, `frmKiemKho`, `frmNhapHang`, `frmTraHangNhap`, `frmDanhSachHangHoa`, `frmBaoCaoTonKho`, `frmBaoCaoTheKho`).
    2. **Danh mục Hàng hóa (`frmDanhSachHangHoa`)**:
       - Tồn kho tự động bám theo kho làm việc: Nếu `MaKhoHienTai > 0` thì `JOIN vw_TonKhoHienTai tk ON h.MaHang = tk.MaHang AND tk.MaKho = @MaKho` và `JOIN NguongTonKho ntk ON h.MaHang = ntk.MaHang AND ntk.MaKho = @MaKho`.
       - Nếu chọn "Tất cả kho" (`MaKhoHienTai == 0`): Tính tổng tồn toàn hệ thống qua `SELECT MaHang, SUM(SoLuongTon) AS SoLuongTon FROM vw_TonKhoHienTai GROUP BY MaHang`.
    3. **Phân hệ Kiểm kho (`frmKiemKho`, `frmHienCotKiemKho`, `frmThemPhieuKiemKho`)**:
       - Xóa `lblKho` và `cboKho` khỏi GroupBox bộ lọc bên trái.
       - Lọc danh sách phiếu theo đúng kho làm việc: `WHERE pkk.MaKho = @MaKho` khi `MaKhoHienTai > 0`.
       - Thêm cột `colKho` (`HeaderText = "Kho"`, `DataPropertyName = "TenKho"`, `Visible = false`).
       - Bổ sung checkbox `chkKho` trên form Hiện cột `frmHienCotKiemKho`.
       - Khi thêm mới (`frmThemPhieuKiemKho`): Nếu `MaKhoHienTai > 0` chỉ nạp duy nhất kho đang làm việc và khóa `cboKho.Enabled = false`; nếu `MaKhoHienTai == 0` nạp tất cả kho và mở `cboKho.Enabled = true`.
    4. **Phân hệ Chuyển kho (`frmChuyenKho`, `frmThemPhieuChuyenKho`)**:
       - Xóa `lblKhoDoiUng` và `cboKhoDoiUng` khỏi bộ lọc bên trái.
       - Lọc phiếu chuyển theo kho làm việc: `AND (pc.MaKhoNguon = @KhoHienTai OR pc.MaKhoDich = @KhoHienTai)`.
       - Khi thêm mới (`frmThemPhieuChuyenKho`): Nếu `MaKhoHienTai > 0` chỉ nạp duy nhất kho đang làm việc vào `cboKhoNguon` và khóa `cboKhoNguon.Enabled = false`; nếu `MaKhoHienTai == 0` nạp tất cả kho và mở `cboKhoNguon.Enabled = true`.
    5. **Phân hệ Nhập hàng (`frmNhapHang`, `frmHienCotNhapHang`, `frmThemPhieuNhap`)**:
       - Lọc phiếu nhập theo kho làm việc: `AND pn.MaKho = @MaKho` khi `MaKhoHienTai > 0`.
       - Thêm cột `colKho` (`HeaderText = "Kho"`, `DataPropertyName = "TenKho"`, `Visible = false`).
       - Bổ sung checkbox `chkKho` trên `frmHienCotNhapHang`.
       - Khi thêm mới (`frmThemPhieuNhap`): Nếu `MaKhoHienTai > 0` chỉ nạp duy nhất kho đang làm việc và khóa `cboKho.Enabled = false`; nếu `MaKhoHienTai == 0` nạp tất cả kho và mở `cboKho.Enabled = true`.
    6. **Phân hệ Trả hàng nhập (`frmTraHangNhap`, `frmHienCotTraHang`, `frmThemPhieuTraHang`)**:
       - Lọc phiếu trả hàng theo kho làm việc: `AND pth.MaKho = @MaKho` khi `MaKhoHienTai > 0`.
       - Thêm cột `colKho` (`HeaderText = "Kho"`, `DataPropertyName = "TenKho"`, `Visible = false`).
       - Bổ sung checkbox `chkKho` trên `frmHienCotTraHang`.
       - Khi thêm mới (`frmThemPhieuTraHang`): Nếu `MaKhoHienTai > 0` chỉ nạp duy nhất kho đang làm việc và khóa `cboKho.Enabled = false`; nếu `MaKhoHienTai == 0` nạp tất cả kho và mở `cboKho.Enabled = true`.
    7. **Phân hệ Báo cáo tồn kho (`frmBaoCaoTonKho`) & Báo cáo thẻ kho (`frmBaoCaoTheKho`)**:
       - Xóa ComboBox `cboKho` và nhãn `lblKho` khỏi GroupBox bộ lọc bên trái của cả hai form.
       - Cột "Chi nhánh kho" được đổi `HeaderText = "Kho"` và ẩn mặc định (`Visible = false`), người dùng tùy chọn hiển thị lại qua `frmHienCotBaoCao`.
       - Báo cáo tồn kho: Truy vấn tự động tính theo `SessionContext.MaKhoHienTai` (nếu > 0 thì lọc theo kho đó, nếu = 0 thì tổng hợp toàn hệ thống).
       - Báo cáo thẻ kho: Dữ liệu biến động và tồn đầu kỳ tự động lọc theo `SessionContext.MaKhoHienTai` (khi double-click từ Báo cáo tồn kho tự động đồng bộ kho hiện tại).
    8. **Kiểm tra chất lượng**:
       - Chạy `tools/KiemTraMaHoa.ps1`: **153/153 file đạt chuẩn UTF-8 with BOM, 0 lỗi Mojibake**.
       - Biên dịch MSBuild: **Build succeeded. 0 Warning(s), 0 Error(s)**.

- **Phiên 43 (2026-10-25) – Khắc phục lỗi cột colMaHang trên Báo cáo tồn kho và Sửa bố cục hiển thị GroupBox bộ lọc Nhập hàng**:
  - **Nhu cầu**: Người dùng phản hồi qua 2 ảnh:
    1. Ảnh 1: Phần hiển thị cột của chức năng Báo cáo tồn kho đang có thêm phần `colMaHang` bất thường làm lỗi hệ thống. Yêu cầu xóa nó.
    2. Ảnh 2: Giao diện của phần groupBox bộ lọc của chức năng Nhập hàng đang bị lỗi bố cục và hiển thị. Yêu cầu sửa lại.
  - **Kết quả thực hiện**:
    1. **Khắc phục triệt để lỗi cột `colMaHang` trên Báo cáo tồn kho (`frmBaoCaoTonKho`)**:
       - Tại `frmBaoCaoTonKho.cs`: Cố định cột khóa chính nội bộ `colMaHang` với `HeaderText = ""` và luôn `Visible = false` (kể cả sau khi gán `DataSource`).
       - Tại `frmHienCotBaoCao.cs`: Bổ sung điều kiện loại trừ tuyệt đối cột `colMaHang` (`col.Name == "colMaHang" || string.Equals(col.HeaderText, "colMaHang", StringComparison.OrdinalIgnoreCase)`). Popup Hiện cột chỉ quản lý 13 cột nghiệp vụ hợp lệ, hoàn toàn loại bỏ checkbox `colMaHang` và chặn hiển thị cột này trên DataGridView.
    2. **Chuẩn hóa bố cục và hiển thị GroupBox bộ lọc Nhập hàng (`frmNhapHang`)**:
       - Tại `frmNhapHang.Designer.cs`:
         * Gỡ bỏ các thuộc tính sai lệch trên `grpBoLoc`: bỏ `Dock = Fill`, bỏ `FlatStyle = Flat`, bỏ `Padding = 12`.
         * Thiết lập kích thước và vị trí chuẩn mực theo form mẫu `frmDanhSachHangHoa`: `Location = (12, 9)`, `Size = (360, 717)` mang lại đường viền GroupBox sắc nét, chuẩn WinForms.
         * Căn chỉnh lề trái các nhãn thẳng hàng với ComboBox (`X = 18`).
         * Bố trí dàn đều khoảng cách thoáng đãng giữa các bộ lọc: Trạng thái (Top 35/65) ➜ Nhà cung cấp (Top 165/195) ➜ Người tạo (Top 295/325) ➜ Người nhập (Top 425/455) với bước nhảy ~75px.
         * Đưa cụm thời gian tạo (`lblThoiGianTao` Top 565, `grpThoiGianTao` Top 593, đáy 701) xuống cố định ở vị trí đáy của vùng bộ lọc tuân thủ tuyệt đối Điều luật 16.
         * Khắc phục triệt để lỗi tràn viền phải của DateTimePicker trong `grpThoiGianTao`: chỉnh `lblTuNgay` / `lblDenNgay` (`X = 6`), `dtpTuNgay` / `dtpDenNgay` (`Location = (92, 23/63)`, `Size = (227, 24)` nằm gọn gàng bên trong khung rộng 322px).
    3. **Kiểm tra chất lượng**:
       - Chạy `tools/KiemTraMaHoa.ps1`: **153/153 file đạt chuẩn UTF-8 with BOM, 0 lỗi Mojibake**.
       - Biên dịch MSBuild: **Build succeeded. 0 Warning(s), 0 Error(s)**.
       - Đồng bộ tài liệu đặc tả `docs/specs/bao-cao-ton-kho.md` và `docs/specs/nhap-hang.md`.

- **Phiên 42 (2026-10-25) – Chuẩn hóa thanh panel phân trang đồng nhất 100% với form mẫu Danh sách hàng hóa**:
  - **Nhu cầu**: Người dùng yêu cầu chuẩn hóa thanh panel phân trang trên toàn bộ các form danh sách để khớp 100% với form mẫu `frmDanhSachHangHoa`:
    1. Thay label Hiển thị (ngoài cùng bên trái) thành label tổng số và đưa ra tổng số hàng dữ liệu đang có (ví dụ: "Tổng số: 14 phiếu kiểm", "Tổng số: 13 phiếu trả hàng").
    2. Sửa lại hiển thị của label hiển thị số trang có chữ "Trang ", ví dụ "Trang 1 / 1" hoặc "Trang 1 / 2" thay vì hiển thị trần "1 / 1".
    3. Chỉnh lại khoảng cách và kích thước cụm: label Số dòng, comboBox số dòng hiển thị, button trang trước, label số trang, button trang sau và căn đều lề viền panel khớp hoàn toàn 100% với form mẫu.
  - **Kết quả thực hiện**:
    1. **Chuẩn hóa nhãn tổng số bản ghi ngoài cùng bên trái**:
       - Cập nhật text hiển thị thành `"Tổng số: {tongSoBanGhi:N0} [tên bản ghi]"` trên `frmTraHangNhap`, `frmNhapHang`, `frmNhaCungCap`, `frmDonViTinh`, `frmKiemKho`, `frmChuyenKho`, `frmBaoCaoTonKho`, `frmBaoCaoTheKho`.
       - Cố định tọa độ lề trái `Location = (22, 12)` cho nhãn tổng số trên toàn bộ các form danh sách.
    2. **Đồng bộ tiền tố "Trang " cho nhãn hiển thị số trang**:
       - Sửa `lblTrangHienTai.Text = $"Trang {trangHienTai} / {tongSoTrang}"` trên `frmKiemKho`, `frmChuyenKho`, `frmDonViTinh`.
    3. **Đồng bộ 100% bố cục, tọa độ và khoảng cách cụm điều khiển phân trang**:
       - Chiều cao `pnlPhanTrang`: chuẩn hóa cố định 42px (`Size = (1395, 42)`, `Location = (387, 811)`).
       - Tọa độ chuẩn của cụm phân trang bên phải:
         * `lblSoDongMoiTrang`: `Location = (932, 13)`, `Size = (61, 17)`, Text `"Số dòng:"`.
         * `cboSoDongMoiTrang`: `Location = (1008, 10)`, `Size = (70, 25)`.
         * `btnTrangTruoc`: `Location = (1117, 6)`, `Size = (80, 31)`, Text `"◀ Trước"`.
         * `lblTrangHienTai`: `Location = (1214, 13)`, `Size = (72, 17)`, Text `"Trang 1 / 1"`.
         * `btnTrangSau`: `Location = (1303, 6)`, `Size = (80, 31)`, Text `"Sau ▶"`.
       - Căn đều dọc cân đối hoàn hảo trong thanh panel cao 42px (nút bấm cao 31px căn giữa Top 6, ComboBox cao 25px căn giữa Top 10, Label cao 17px căn giữa Top 13).
    4. **Kiểm tra chất lượng**:
       - Chạy `tools/KiemTraMaHoa.ps1`: **153/153 file đạt chuẩn UTF-8 with BOM, 0 lỗi Mojibake**.
       - Biên dịch MSBuild: **Build succeeded. 0 Warning(s), 0 Error(s)**.

- **Phiên 41 (2026-10-25) – Chuẩn hóa cột Thời gian tạo và Ngày cân bằng trên Phân hệ Kiểm kho**:
  - **Nhu cầu**: Người dùng yêu cầu ở form chức năng Kiểm kho:
    1. Thay tên cột "Thời gian" thành "Thời gian tạo" (lưu thời gian tạo ra phiếu, cột này chỉ lưu thời gian tạo ra phiếu, không thể thay đổi, không cập nhật khi thay đổi trạng thái của phiếu).
    2. Ẩn cột này đi mặc định (`Visible = false`), khi người dùng chọn hiển thị trong phần "Hiện cột" mới hiện lên; vị trí hiện lên ở sau cột "Tổng chênh lệch", trước cột "Trạng thái".
    3. Đưa cột "Ngày cân bằng" lên sau cột "Mã kiểm kho"; nếu phiếu là phiếu tạm thì để trống; lưu thời gian phiếu được tạo ở trạng thái đã cân bằng hoặc cập nhật thời gian là thời gian phiếu tạm được ấn Hoàn thành.
  - **Kết quả thực hiện**:
    1. **Đổi tên, ẩn mặc định và định vị cột Thời gian tạo**:
       - Tại `frmKiemKho.Designer.cs`: Đổi `colThoiGian.HeaderText = "Thời gian tạo"`, `colThoiGian.Visible = false`.
       - Thứ tự cột hiển thị trên DataGridView: `colChon` ➜ `colMaPhieu` ➜ `colNgayCanBang` ➜ `colSlLechTang` ➜ `colSlLechGiam` ➜ `colSlThucTe` ➜ `colGhiChu` (ẩn) ➜ `colTongThucTe` ➜ `colTongChenhLech` ➜ `colThoiGian` (Thời gian tạo - ẩn) ➜ `colTrangThai` ➜ `colMaPhieuKiemKe`.
       - Tại `frmHienCotKiemKho.Designer.cs` & `frmHienCotKiemKho.cs`: Đổi checkbox `chkThoiGian.Text = "Thời gian tạo"`, `chkThoiGian.Checked = false`. Khi người dùng tích chọn, cột Thời gian tạo xuất hiện ngay sau Tổng chênh lệch và trước Trạng thái.
    2. **Đưa Ngày cân bằng lên sau Mã kiểm kho & Logic lưu trữ chuẩn xác**:
       - Tại `frmKiemKho.cs` (`TaiDuLieu`): Sử dụng subquery `LEFT JOIN (SELECT MaPhieuKiemKe, MAX(NgayDuyet) AS NgayDuyet FROM PhieuDieuChinh WHERE TrangThai = 'DA_DUYET' GROUP BY MaPhieuKiemKe) pdc` để lấy `NgayCanBang` chống nhân dòng.
       - Logic hiển thị: `CASE WHEN pkk.TrangThai = 'DA_DIEU_CHINH' THEN ISNULL(CONVERT(VARCHAR(16), pdc.NgayDuyet, 120), CONVERT(VARCHAR(16), pkk.ThoiDiemChot, 120)) ELSE '' END AS NgayCanBang`. Nếu phiếu tạm (`NHAP`), hiển thị rỗng `''`.
       - Tại `frmChiTietKiemKho.cs` (`btnHoanThanh_Click`): Loại bỏ việc cập nhật `ThoiDiemChot = sysdatetime()` để bảo toàn vĩnh viễn thời điểm tạo ban đầu của phiếu tạm; thời điểm ấn Hoàn thành được lưu vào `PhieuDieuChinh.NgayDuyet = sysdatetime()`.
       - Tại `frmThemPhieuKiemKho.cs`: Khi tạo mới phiếu ở trạng thái Đã cân bằng (`DA_DIEU_CHINH`), gán `NgayDuyet = @ThoiDiemChot` để ngày cân bằng lưu chính xác thời gian tạo phiếu đã cân bằng.
       - Đồng bộ thứ tự cột xuất file CSV (`XuatCsvDanhSach`) và Excel HTML (`XuatExcelHtmlDanhSach`).
    3. **Kiểm tra chất lượng**:
       - Chạy `tools/KiemTraMaHoa.ps1`: **152/152 file đạt chuẩn UTF-8 with BOM, 0 lỗi Mojibake**.
       - Biên dịch MSBuild: **Build succeeded. 0 Warning(s), 0 Error(s)**.
       - Đồng bộ tài liệu `docs/specs/kiem-kho.md` và `docs/quyet-dinh.md`.

- **Phiên 40 (2026-10-25) – Dàn đều bộ lọc Danh sách hàng hóa, Ẩn cột Ghi chú Kiểm kho & Chuyển kho, Đưa Tổng thực tế & Chênh lệch về trước Trạng thái và Chuẩn hóa báo đỏ in đậm chênh lệch kiểm kê**:
  - **Nhu cầu**: Người dùng yêu cầu: (1) Form Danh sách hàng hóa: chỉnh lại khoảng cách giữa các filter cho rộng hơn, không để thừa không gian ở dưới mà các filter lại sát vào nhau; (2) Form Kiểm kho và Chuyển kho: ẩn cột ghi chú mặc định, khi người dùng tích chọn cột ghi chú trong form Hiện cột mới hiện ra; đưa cột Tổng thực tế và Tổng chênh lệch của form Kiểm kho về cuối dgv, phía trước cột Trạng thái; (3) Khi Kiểm kho: lệch tăng và lệch giảm nếu tổng lại về 0 (hoặc bù trừ về 0) thì trong form chi tiết kiểm kho vẫn phải báo đỏ thể hiện bất thường chỉ cần có mặt hàng bị chênh lệch trong phiếu; các trường thông tin Tổng lệch tăng, tổng lệch giảm, tổng chênh lệch khi có báo đỏ bất thường thì in đậm như trong dgv.

- **Phiên 47 (2026-10-25) – Xây dựng Form Đăng nhập hệ thống, Tiện ích băm mật khẩu SHA-256 an toàn và Phân quyền thao tác theo 3 vai trò chuẩn**:
  - **Nhu cầu**: Người dùng yêu cầu:
    1. Kiểm tra 1 lượt classes và các file liên quan có thể có chứa hàm băm của mật khẩu. Nếu tồn tại, gọi hàm hash đó; nếu chưa, cho phép tạo class `PasswordHelper.cs`.
    2. Tạo form đăng nhập cho hệ thống, khi bắt đầu chạy chương trình yêu cầu đăng nhập với tài khoản và mật khẩu để phân quyền thao tác được phép.
    3. Hỗ trợ 3 vai trò người dùng chuẩn:
       - *Quản lý*: Quản lý mọi kho hàng trong hệ thống.
       - *Thủ kho*: Quản lý 1 kho duy nhất được phân công.
       - *Nhân viên kho*: Thao tác nghiệp vụ tại kho.
  - **Kết quả thực hiện**:
    1. **Kiểm tra và xây dựng tiện ích băm mật khẩu SHA-256 (`Classes/PasswordHelper.cs`)**:
       - Quét toàn bộ codebase xác nhận chưa có hàm băm mật khẩu.
       - Tạo mới `Classes/PasswordHelper.cs` cung cấp `HashPassword(string plainPassword)` (SHA-256 chữ thường) và `VerifyPassword(string plainPassword, string hashedPassword)`.
    2. **Xây dựng Form Đăng nhập (`frmDangNhap`) chuẩn WinForms**:
       - Kích thước form Size Nhỏ `ClientSize = (444, 321)`, `FixedDialog`, `CenterScreen`.
       - Gồm các control: Tiêu đề hệ thống, Tên đăng nhập (`txtTenDangNhap`), Mật khẩu (`txtMatKhau`), CheckBox Hiện mật khẩu (`chkHienMatKhau`), ComboBox Kho làm việc (`cboKhoLamViec`), nút Đăng nhập (`btnDangNhap`) và Thoát (`btnThoat`).
       - Tự động nhận diện vai trò tài khoản khi nhập tên đăng nhập để nạp danh sách kho tương ứng.
    3. **Phân quyền thao tác và phạm vi kho chuẩn hóa theo 3 vai trò**:
       - *Quản lý (`MaVaiTro = 1`)*: Toàn quyền truy cập tất cả các phân hệ và menu; quản lý mọi kho hàng; được chọn "Tất cả kho" hoặc kho chi nhánh bất kỳ khi đăng nhập và có quyền đổi kho (`frmDoiKho`).
       - *Thủ kho (`MaVaiTro = 2`)*: Quản lý 1 kho duy nhất được phân công (cố định ComboBox kho khi đăng nhập, ẩn/khóa menu "Đổi kho"); toàn quyền các nghiệp vụ kho của mình (Kiểm kho, Chuyển kho, Hàng hóa, Nhà cung cấp, Nhập hàng, Trả hàng nhập, Báo cáo tồn kho & Thẻ kho).
       - *Nhân viên kho (`MaVaiTro = 3`)*: Thao tác tại kho được phân công (ẩn menu "Đổi kho"); thực hiện Kiểm kho, Chuyển kho, Nhập hàng, Trả hàng nhập, xem Danh sách hàng hóa, xem Báo cáo tồn kho; ẩn các mục quản lý đối tác (Nhà cung cấp) và Thẻ kho sổ sách.
    4. **Cơ chế khởi động chương trình và Đăng xuất linh hoạt**:
       - Tại `Program.cs`: Bọc vòng lặp `while (true)` yêu cầu đăng nhập trước khi hiển thị `frmMain`.
       - Tại `frmMain.cs`: Khi người dùng nhấn "Đăng xuất" trên menu Hệ thống, hệ thống xóa sạch phiên làm việc `SessionContext.XoaPhien()`, đóng `frmMain` và quay lại màn hình `frmDangNhap` ngay lập tức.
    5. **Đồng bộ CSDL & Dữ liệu mẫu**:
       - Cập nhật script `docs/database_update.sql` và `docs/DuLieuMau.sql`: Đồng bộ 3 vai trò chuẩn trong `VaiTro`, 3 nhân viên trong `NhanVien`, 3 tài khoản demo (`quanly`, `thukho`, `nvkho` mật khẩu mặc định `123456`), và phân quyền kho trong `TaiKhoanKho`.
    6. **Kiểm tra chất lượng**:
       - Chạy `tools/KiemTraMaHoa.ps1`: **158/158 file đạt chuẩn UTF-8 with BOM, 0 lỗi Mojibake**.
       - Biên dịch MSBuild: **Build succeeded. 0 Warning(s), 0 Error(s)**.
  - **Kết quả thực hiện**:
    1. **Dàn đều và mở rộng khoảng cách bộ lọc `frmDanhSachHangHoa`**:
       - Bố trí lại toàn bộ 7 cụm bộ lọc trên `grpBoLoc` (chiều cao 717px) với khoảng cách thoáng đãng ~39px giữa mỗi cụm: Nhóm hàng (Top 25/51) ➜ Đơn vị tính (Top 115/141) ➜ Tồn kho (Top 205/231) ➜ Nhà cung cấp (Top 295/321) ➜ Vị trí (Top 385/411) ➜ Trạng thái (Top 475/501) ➜ Cụm thời gian tạo `grpThoiGianTao` (Top 565/593, đáy 701px, cách viền dưới 16px) tuân thủ tuyệt đối Điều luật 16.
    2. **Ẩn cột Ghi chú & Đưa cột Tổng thực tế / Chênh lệch về trước Trạng thái**:
       - Form Kiểm kho (`frmKiemKho`): Đặt `colGhiChu.Visible = false`, thứ tự cột dgv: `colChon`, `colMaPhieu`, `colThoiGian`, `colSlLechTang`, `colSlLechGiam`, `colNgayCanBang`, `colSlThucTe`, `colGhiChu`, `colTongThucTe`, `colTongChenhLech`, `colTrangThai`. Cột Tổng thực tế và Tổng chênh lệch nằm ở cuối dgv, ngay trước Trạng thái. Đồng bộ thứ tự cột xuất CSV và Excel HTML.
       - Form Chuyển kho (`frmChuyenKho`): Đặt `colGhiChu.Visible = false`.
       - Popup Hiện cột (`frmHienCotKiemKho`, `frmHienCotChuyenKho`): Đặt mặc định `chkGhiChu.Checked = false`, tích chọn sẽ lập tức hiển thị lại cột trên dgv.
    3. **Chuẩn hóa cảnh báo bất thường báo đỏ in đậm khi Kiểm kho**:
       - Tại `frmChiTietKiemKho.cs` và `frmThemPhieuKiemKho.cs`: Cập nhật điều kiện xác định chênh lệch `bool coChenhLech = (coLechTang || coLechGiam || tongChenhLech != 0 || tongGiaTriChenhLech != 0)`. Chỉ cần có bất kỳ mặt hàng nào lệch trong phiếu (dù số lượng lệch bù trừ về 0 hay tiền bù trừ về 0), trường `Tổng chênh lệch` (`lblTongChenhLechVal`) bắt buộc đổi sang màu Đỏ (`Color.Red`) và in đậm (`FontStyle.Bold`).
       - Các trường `Tổng lệch tăng` (`lblTongLechTangVal`), `Tổng lệch giảm` (`lblTongLechGiamVal`) khi có chênh lệch cũng đổi sang màu Đỏ và in đậm (`FontStyle.Bold`) đồng bộ với DataGridView.
       - Đồng bộ logic CellFormatting trên danh sách `frmKiemKho` để cột `Tổng chênh lệch` báo đỏ in đậm khi có mặt hàng lệch.
    4. **Kiểm tra chất lượng**:
       - Chạy `tools/KiemTraMaHoa.ps1`: **152/152 file đạt chuẩn UTF-8 with BOM, 0 lỗi Mojibake**.
       - Biên dịch MSBuild: **Build succeeded. 0 Warning(s), 0 Error(s)**.
       - Đồng bộ tài liệu `docs/specs/kiem-kho.md` và `docs/specs/chuyen-kho.md`.

- **Phiên 39 (2026-10-25) – Bỏ chức năng Đơn vị tính khỏi MenuStrip Hàng hóa, Tích hợp Filter Đơn vị tính và Form Quản lý Đơn vị tính con theo khuôn mẫu Nhóm hàng và Vị trí**:
  - **Nhu cầu**: Người dùng yêu cầu trong menuStrip chức năng Hàng hóa, bỏ chức năng Đơn vị tính; thêm 1 filter Đơn vị tính vào form Danh sách hàng hóa và có nút thêm đơn vị tính ở bên cạnh (bố cục và logic, form thêm Đơn vị tính giống như 2 filter Nhóm hàng và Vị trí).
  - **Kết quả thực hiện**:
    1. **Loại bỏ Đơn vị tính khỏi MenuStrip rmMain**:
       - Gỡ bỏ hoàn toàn mnuDonViTinh và 	oolStripSeparator3 khỏi mnuDanhMuc (menu Hàng hóa) trên rmMain.Designer.cs.
       - Xóa bỏ phương thức xử lý sự kiện mnuDonViTinh_Click trong rmMain.cs.
    2. **Tích hợp Filter Đơn vị tính vào rmDanhSachHangHoa**:
       - Thêm nhãn lblDonViTinh, ComboBox cboDonViTinh (rộng 284) và nút thêm nhanh tnThemDonViTinhNhanh (size 29x29, text +, viền xám phẳng).
       - Bố trí vị trí cân xứng, đều đặn giữa các bộ lọc: Nhóm hàng (Top 30/56) ➜ Đơn vị tính (Top 96/122) ➜ Tồn kho (Top 162/188) ➜ NCC (Top 228/254) ➜ Vị trí (Top 294/320) ➜ Trạng thái (Top 360/386) ➜ Cụm thời gian tạo grpThoiGianTao (Top 436/464) ở đáy tuân thủ tuyệt đối Điều luật 16.
       - Tích hợp logic tìm kiếm không dấu ComboboxHelper.CaiDatTimKiem với giá trị mặc định -- Tất cả đơn vị tính --.
       - Bổ sung điều kiện lọc AND h.MaDVT = @MaDVT vào hàm LayCauTrucQuery().
    3. **Tạo Form Quản lý Đơn vị tính con (rmDonViTinh trong GUI/Forms/DanhSachHangHoa/)**:
       - Kích thước form 1082 x 553, StartPosition = CenterParent, font Segoe UI, 9.5pt đồng bộ 100% với rmNhomHang và rmViTri.
       - Panel Header: Tiêu đề "Đơn vị tính", ô tìm kiếm tức thời theo tên ĐVT, nút "Nhập file", nút "Xuất file" (CSV / Excel HTML Table UTF-8 BOM).
       - Panel Trái: Ô nhập tên ĐVT, nút "Thêm", "Sửa", "Hủy".
       - DataGridView: Hiển thị danh sách gồm Mã ĐVT (ẩn), Tên đơn vị tính (Fill), Số mặt hàng đang sử dụng (MiddleRight, format N0).
       - Logic nghiệp vụ: Click chọn dòng để sửa, double click để xóa (kiểm tra toàn vẹn dữ liệu, chặn xóa nếu đang có hàng hóa liên kết), kiểm tra chống trùng tên khi thêm/sửa, nạp lại ComboBox tự động sau khi đóng form.
    4. **Kiểm tra biên dịch & mã hóa**:
       - Đăng ký đầy đủ 3 file mới vào BTL-LTTQ-2.csproj.
       - Chạy 	ools/KiemTraMaHoa.ps1: **154/154 file đạt chuẩn UTF-8 with BOM, 0 lỗi Mojibake**.
       - Biên dịch MSBuild: **Build succeeded. 0 Warning(s), 0 Error(s)**.
       - Đồng bộ tài liệu docs/specs/danh-sach-hang-hoa.md và docs/quyet-dinh.md.
- **Phiên 38 (2026-10-25) – Khắc phục lỗi nút đè thanh phân trang frmBaoCaoTonKho và Chuẩn hóa khoảng cách bộ lọc bên trái theo frmDanhSachHangHoa**:
  - **Nhu cầu**: Người dùng phản hồi (1) Trong form báo cáo tồn kho đang tồn tại 1 nút không rõ chức năng bị đè dưới label tổng số hàng; (2) Khoảng cách giữa các filter trong các form bị quá chật, yêu cầu chỉnh lại rộng hơn, lấy khoảng cách các filter trong form Danh sách hàng hóa làm mẫu.
  - **Kết quả thực hiện**:
    1. **Khắc phục lỗi thanh phân trang `frmBaoCaoTonKho`**: Gỡ bỏ `BackColor = Color.White` trên `pnlPhanTrang` và `pnlBoLoc` để đồng bộ nền hệ thống WinForms chuẩn `frmDanhSachHangHoa`. Căn chỉnh lại toàn bộ cụm controls phân trang bên phải theo đúng tọa độ chuẩn `frmDanhSachHangHoa`: `lblSoDongMoiTrang (951, 12)`, `cboSoDongMoiTrang (1035, 7)`, `btnTrangTruoc (1118, 6)`, `lblTrangHienTai (1211, 11)`, `btnTrangSau (1309, 6)` (khắc phục lỗi `btnTrangSau` bị tràn viền 1416 > 1395). Bổ sung `lblTongSoHang.BringToFront()` trong `frmBaoCaoTonKho_Load`.
    2. **Chuẩn hóa khoảng cách các bộ lọc bên trái (`grpBoLoc`) trên 8 form**: Lấy bước nhảy và khoảng cách chuẩn từ `frmDanhSachHangHoa` làm khuôn mẫu (Label đặt cách nhau đều đặn, ComboBox đặt tại `Label.Y + 34`, khoảng cách từ đáy ComboBox đến Label tiếp theo thoáng đãng ~27-40px, cụm thời gian `grpThoiGianTao` cố định ở đáy theo Điều luật 16). Áp dụng đồng bộ cho cả 8 form: `frmBaoCaoTonKho`, `frmBaoCaoTheKho`, `frmNhapHang`, `frmTraHangNhap`, `frmChuyenKho`, `frmKiemKho`, `frmNhaCungCap`, `frmDonViTinh`.
    3. **Kiểm tra chất lượng**: Chạy `tools/KiemTraMaHoa.ps1` xác nhận 151/151 file UTF-8 with BOM, 0 lỗi Mojibake; MSBuild Rebuild thành công 0 Warning, 0 Error.

- **Phiên 37 (2026-10-25) – Khắc phục lỗi giao diện 2 chức năng Báo cáo (Báo cáo thẻ kho & Báo cáo tồn kho), Chuẩn hóa bộ lọc bên trái và mở rộng chiều cao card KPI vừa vặn panel cha**:
  - **Nhu cầu**: Người dùng phản hồi:
    1. Lỗi giao diện trong 2 chức năng báo cáo cần sửa lại (lỗi chữ "Tìm kiếm" bị ô nhập liệu đè lên chữ; vùng bộ lọc bên trái bị sai kích thước khiến combobox và groupbox thời gian đâm thủng viền phải gây méo mó, biến dạng).
    2. Mở rộng các panel con chứa báo cáo tổng quan ở phần đầu form sao cho vừa với phần panel cha về chiều cao (vẫn phải có khoảng cách với phần viền nhưng vừa đủ, như khoảng cách của các panel con với phần viền trên của panel cha hiện tại).
  - **Kết quả thực hiện**:
    1. **Khắc phục lỗi ô tìm kiếm bị đè chữ**:
       - Tại `frmBaoCaoTheKho.Designer.cs` & `frmBaoCaoTonKho.Designer.cs`: Tách biệt `lblTimKiem` (Location `(385, 25)`) và `txtTimKiem` (Location `(465, 21)`), đồng bộ 100% tọa độ với khuôn mẫu hệ thống `frmDanhSachHangHoa`.
    2. **Mở rộng chiều cao các panel con KPI vừa khít panel cha**:
       - Panel cha `pnlBannerHangHoa` và `pnlKpi` có `Height = 90`. Các panel con có khoảng cách với viền trên `Top = 9`. Để khoảng cách với viền dưới bằng viền trên (9px), tăng chiều cao các panel con từ 52/56px lên chuẩn `72px` ($90 - 9 - 9 = 72$).
       - `frmBaoCaoTheKho`: Cập nhật 5 panel con (`pnlThongTinHang`, `pnlCardTonDau`, `pnlCardNhapKy`, `pnlCardXuatKy`, `pnlCardTonCuoi`) lên chiều cao 72px; căn chỉnh nhãn tiêu đề (Top 10) và giá trị số (Top 38/42) cân đối, thoáng đẹp.
       - `frmBaoCaoTonKho`: Cập nhật 4 panel con (`pnlCardSku`, `pnlCardSoLuong`, `pnlCardGiaTri`, `pnlCardCanLuuY`) lên chiều cao 72px; căn chỉnh nhãn tiêu đề (Top 10) và giá trị số (Top 38).
    3. **Chuẩn hóa vùng bộ lọc bên trái & Cụm thời gian ở đáy theo Điều luật 16**:
       - Đồng bộ vùng bộ lọc với `frmDanhSachHangHoa`: `pnlBoLoc` (Width 387px), `grpBoLoc` (Width 360px), bổ sung `splitterBoLoc` đỉnh (Height 1).
       - Đưa cụm thời gian phát sinh / tạo hàng (`lblThoiGianTao` và `grpThoiGianTao`) xuống **VỊ TRÍ CUỐI CÙNG (ở đáy)** của vùng bộ lọc (Location `(18, 513)`), chuẩn hóa bỏ Text của GroupBox theo Điều luật 16.
       - Căn chỉnh lại `pnlPhanTrang` và DataGridView (`dgvTheKho`, `dgvBaoCaoTonKho`) khít mép phải bộ lọc (`Location X = 387`, `Width = 1395`).
    4. **Kiểm tra biên dịch, mã hóa & Đồng bộ tài liệu**:
       - Đồng bộ đặc tả chức năng `docs/specs/the-kho.md` và `docs/specs/bao-cao-ton-kho.md`.
       - Chạy `tools/KiemTraMaHoa.ps1`: **150/150 file đạt chuẩn UTF-8 with BOM, 0 lỗi Mojibake**.
       - Biên dịch MSBuild: **Build succeeded. 0 Warning(s), 0 Error(s)**.

- **Phiên 36 (2026-10-25) – Rà soát toàn bộ Form, Khắc phục triệt để lỗi font chữ (Mojibake) và Thiết lập cơ chế kiểm soát mã hóa cố định UTF-8 BOM**:
  - **Nhu cầu**: Người dùng phản hồi trên giao diện form Chi tiết chuyển kho (`frmChiTietChuyenKho`) xuất hiện tình trạng lỗi font chữ / biến dạng ký tự tiếng Việt (như nhãn trạng thái bị vỡ ký tự, mũi tên lộ trình bị lỗi, tên người nhận hiển thị ký tự lạ, ghi chú và các nút chức năng bị biến dạng). Yêu cầu rà soát toàn bộ tất cả các form và đảm bảo rằng không bao giờ bị lỗi font chữ nữa; bổ sung rule và cơ chế cố định duy nhất 1 chuẩn Unicode để giữ form không bao giờ lỗi font khi chạy các phiên sửa.

- **Phiên 47 (2026-10-25) – Xây dựng Form Đăng nhập hệ thống, Tiện ích băm mật khẩu SHA-256 an toàn và Phân quyền thao tác theo 3 vai trò chuẩn**:
  - **Nhu cầu**: Người dùng yêu cầu:
    1. Kiểm tra 1 lượt classes và các file liên quan có thể có chứa hàm băm của mật khẩu. Nếu tồn tại, gọi hàm hash đó; nếu chưa, cho phép tạo class `PasswordHelper.cs`.
    2. Tạo form đăng nhập cho hệ thống, khi bắt đầu chạy chương trình yêu cầu đăng nhập với tài khoản và mật khẩu để phân quyền thao tác được phép.
    3. Hỗ trợ 3 vai trò người dùng chuẩn:
       - *Quản lý*: Quản lý mọi kho hàng trong hệ thống.
       - *Thủ kho*: Quản lý 1 kho duy nhất được phân công.
       - *Nhân viên kho*: Thao tác nghiệp vụ tại kho.
  - **Kết quả thực hiện**:
    1. **Kiểm tra và xây dựng tiện ích băm mật khẩu SHA-256 (`Classes/PasswordHelper.cs`)**:
       - Quét toàn bộ codebase xác nhận chưa có hàm băm mật khẩu.
       - Tạo mới `Classes/PasswordHelper.cs` cung cấp `HashPassword(string plainPassword)` (SHA-256 chữ thường) và `VerifyPassword(string plainPassword, string hashedPassword)`.
    2. **Xây dựng Form Đăng nhập (`frmDangNhap`) chuẩn WinForms**:
       - Kích thước form Size Nhỏ `ClientSize = (444, 321)`, `FixedDialog`, `CenterScreen`.
       - Gồm các control: Tiêu đề hệ thống, Tên đăng nhập (`txtTenDangNhap`), Mật khẩu (`txtMatKhau`), CheckBox Hiện mật khẩu (`chkHienMatKhau`), ComboBox Kho làm việc (`cboKhoLamViec`), nút Đăng nhập (`btnDangNhap`) và Thoát (`btnThoat`).
       - Tự động nhận diện vai trò tài khoản khi nhập tên đăng nhập để nạp danh sách kho tương ứng.
    3. **Phân quyền thao tác và phạm vi kho chuẩn hóa theo 3 vai trò**:
       - *Quản lý (`MaVaiTro = 1`)*: Toàn quyền truy cập tất cả các phân hệ và menu; quản lý mọi kho hàng; được chọn "Tất cả kho" hoặc kho chi nhánh bất kỳ khi đăng nhập và có quyền đổi kho (`frmDoiKho`).
       - *Thủ kho (`MaVaiTro = 2`)*: Quản lý 1 kho duy nhất được phân công (cố định ComboBox kho khi đăng nhập, ẩn/khóa menu "Đổi kho"); toàn quyền các nghiệp vụ kho của mình (Kiểm kho, Chuyển kho, Hàng hóa, Nhà cung cấp, Nhập hàng, Trả hàng nhập, Báo cáo tồn kho & Thẻ kho).
       - *Nhân viên kho (`MaVaiTro = 3`)*: Thao tác tại kho được phân công (ẩn menu "Đổi kho"); thực hiện Kiểm kho, Chuyển kho, Nhập hàng, Trả hàng nhập, xem Danh sách hàng hóa, xem Báo cáo tồn kho; ẩn các mục quản lý đối tác (Nhà cung cấp) và Thẻ kho sổ sách.
    4. **Cơ chế khởi động chương trình và Đăng xuất linh hoạt**:
       - Tại `Program.cs`: Bọc vòng lặp `while (true)` yêu cầu đăng nhập trước khi hiển thị `frmMain`.
       - Tại `frmMain.cs`: Khi người dùng nhấn "Đăng xuất" trên menu Hệ thống, hệ thống xóa sạch phiên làm việc `SessionContext.XoaPhien()`, đóng `frmMain` và quay lại màn hình `frmDangNhap` ngay lập tức.
    5. **Đồng bộ CSDL & Dữ liệu mẫu**:
       - Cập nhật script `docs/database_update.sql` và `docs/DuLieuMau.sql`: Đồng bộ 3 vai trò chuẩn trong `VaiTro`, 3 nhân viên trong `NhanVien`, 3 tài khoản demo (`quanly`, `thukho`, `nvkho` mật khẩu mặc định `123456`), và phân quyền kho trong `TaiKhoanKho`.
    6. **Kiểm tra chất lượng**:
       - Chạy `tools/KiemTraMaHoa.ps1`: **158/158 file đạt chuẩn UTF-8 with BOM, 0 lỗi Mojibake**.
       - Biên dịch MSBuild: **Build succeeded. 0 Warning(s), 0 Error(s)**.
  - **Kết quả thực hiện**:
    1. **Khắc phục 100% mã nguồn bị lỗi**:
       - Khôi phục chuẩn tiếng Việt UTF-8 cho toàn bộ các chuỗi trong `frmChiTietChuyenKho.cs`: nhãn trạng thái ("Đang chuyển", "Đã nhận", "Phiếu tạm", "Đã hủy"), mũi tên lộ trình ("Kho gửi ➜ Kho nhận"), tên người nhận ("Chưa rõ"), ghi chú ("(trống)"), nút hành động ("Nhận hàng", "Chuyển hàng"), đơn vị tiền tệ ("VNĐ"), và toàn bộ các hộp thoại `MessageBox`.
       - Làm sạch các comment bị lỗi mã hóa trong `frmBaoCaoTheKho.cs`, `frmBaoCaoTonKho.cs`, và mục 5 trong `.agent/rules/05-co-so-du-lieu.md`.
    2. **Cố định bảng mã trình biên dịch & Editor**:
       - Xác nhận thuộc tính `<CodePage>65001</CodePage>` trong `BTL-LTTQ-2.csproj` ép trình biên dịch C# (`csc.exe`) xử lý toàn bộ mã nguồn theo chuẩn UTF-8.
       - Tạo file `.editorconfig` ở thư mục gốc: ép cứng `charset = utf-8-bom` và `end_of_line = crlf` cho Visual Studio, VS Code và mọi trình soạn thảo.
       - Cập nhật `.gitattributes`: ép `working-tree-encoding=UTF-8` và `eol=crlf`.
    3. **Xây dựng công cụ kiểm tra tự động độc lập**:
       - Viết script `tools/KiemTraMaHoa.ps1`: tự động quét 150 file toàn dự án, tự động bổ sung UTF-8 BOM cho các file thiếu, phát hiện 0 lỗi Mojibake.
    4. **Cập nhật Quy chuẩn Vận hành**:
       - Cập nhật `AGENTS.md` (Quy trình cuối phiên & Điều luật 18): bắt buộc chạy `tools/KiemTraMaHoa.ps1` trước khi chốt phiên, quy định chuẩn mã hóa cố định duy nhất **UTF-8 with BOM (`0xEF, 0xBB, 0xBF`)**.
       - Đồng bộ `.agent/rules/01-phong-cach-code.md` (Mục 4) và `.agent/rules/03-giao-dien-layout.md` (Mục 6.6).
    5. **Kiểm tra biên dịch & Xác nhận**:
       - Chạy `tools/KiemTraMaHoa.ps1`: **150/150 file đạt chuẩn UTF-8 with BOM, 0 lỗi Mojibake**.
       - Biên dịch MSBuild: **Build succeeded. 0 Warning(s), 0 Error(s)**.

- **Phiên 35 (2026-10-25) – Chuẩn hóa toàn bộ Form  Hiển thị cột theo mẫu rmHienCot của Danh mục hàng hóa và Cơ chế Co giãn Cột DataGridView Lấp đầy Bảng**:
  - **Nhu cầu**: Người dùng phản hồi:
    1. Chuẩn hóa lại tất cả các form Hiển thị cột trong tất cả các chức năng (trừ Danh sách hàng hóa) thành form giống 100% form rmHienCot của Danh mục hàng hóa (style, color, border style, start position...).
    2. Ở tất cả các form danh sách, khi chọn hiển thị thêm các cột hoặc bỏ bớt các cột cần hiển thị thì các cột còn đang hiển thị tự động mở rộng hoặc thu hẹp lại sao cho vừa với DataGridView hoặc form hiện tại (giải quyết triệt để lỗi khoảng trống thừa lớn bên phải như ảnh Kiểm kho người dùng gửi). Chỉ xuất hiện thanh cuộn ngang khi hiển thị quá nhiều thông tin chạm ngưỡng MinimumWidth.
  - **Kết quả thực hiện**:
    1. **Chuẩn hóa 100% Form Hiện cột**:
       - rmHienCotKiemKho: Chuyển sang FixedDialog, nền trắng, tiêu đề lblHienThiCot Hiển thị cột 10F Bold, 10 CheckBox chia 2 cột đều đặn (ClientSize = 340 x 205), bỏ nút tnDong và sự kiện Deactivate.
       - rmHienCotChuyenKho: Chuyển sang FixedDialog, nền trắng, tiêu đề Hiển thị cột, 10 CheckBox chia 2 cột đều đặn (ClientSize = 340 x 205), bỏ nút tnDong.
       - rmHienCotNhapHang: Loại bỏ hoàn toàn pnlMain viền đen và borderless, đưa về FixedDialog nền trắng, 7 CheckBox 1 cột gọn gàng (ClientSize = 220 x 255).
       - rmHienCotTraHang: Loại bỏ pnlMain, đưa về FixedDialog nền trắng, 8 CheckBox chia 2 cột 4-4 (ClientSize = 340 x 175).
       - rmHienCotNcc: Loại bỏ pnlMain, đưa về FixedDialog nền trắng, 7 CheckBox 1 cột gọn gàng (ClientSize = 220 x 255).
       - rmHienCotDvt: Loại bỏ pnlMain, đưa về FixedDialog nền trắng, 5 CheckBox 1 cột gọn gàng (ClientSize = 220 x 200).
       - rmHienCotBaoCao: Loại bỏ pnlTop, pnlBottom, nút Đóng, nút Chọn tất cả; tự động sinh CheckBox chia làm 2 cột cân đối và tự động co giãn chiều cao form vừa vặn theo số hàng, phục vụ cho cả Báo cáo tồn kho và Báo cáo thẻ kho.
    2. **Chuẩn hóa Cơ chế Tự động Co giãn DataGridView Lấp đầy Bảng**:
       - Áp dụng AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill kết hợp FillWeight theo tỷ lệ chuẩn cho toàn bộ các cột dữ liệu trên DataGridView danh sách ở tất cả 8 phân hệ (rmKiemKho, rmChuyenKho, rmNhapHang, rmTraHangNhap, rmNhaCungCap, rmDonViTinh, rmBaoCaoTonKho, rmBaoCaoTheKho).
       - Cài đặt MinimumWidth chuẩn mực cho từng cột để tránh co bẹp chữ khi hiển thị nhiều cột; tự động xuất hiện thanh cuộn ngang khi vượt quá chiều rộng DataGridView.
       - Chuẩn hóa sự kiện tnHienCot_Click: chống mở trùng (Application.OpenForms), mở form ngay dưới mép phải nút tnHienCot và bảo đảm trong workingArea.
    3. **Kiểm tra biên dịch & Chuẩn định dạng**:
       - 100% file mã nguồn lưu định dạng **UTF-8 with BOM** (Điều luật 18).
       - Biên dịch MSBuild: **Build succeeded. 0 Warning(s), 0 Error(s)**.

- **Phiên 34 (2026-10-24) – Chuẩn hóa toàn bộ Font chữ & Màu chữ theo `frmDanhSachHangHoa` và Ban hành Quy chuẩn Kích thước Form**:
  - **Nhu cầu**: Người dùng phản hồi trên các form (chi tiết chứng từ, thêm mới, báo cáo...), các nhãn và trường dữ liệu (Tổng tiền hàng, NCC cần trả, Mã phiếu...) bị tự ý in đậm (`FontStyle.Bold`) và chọn màu chữ (`ForeColor`) mỗi trường một kiểu cọc cạch như ảnh 2. Yêu cầu:

- **Phiên 47 (2026-10-25) – Xây dựng Form Đăng nhập hệ thống, Tiện ích băm mật khẩu SHA-256 an toàn và Phân quyền thao tác theo 3 vai trò chuẩn**:
  - **Nhu cầu**: Người dùng yêu cầu:
    1. Kiểm tra 1 lượt classes và các file liên quan có thể có chứa hàm băm của mật khẩu. Nếu tồn tại, gọi hàm hash đó; nếu chưa, cho phép tạo class `PasswordHelper.cs`.
    2. Tạo form đăng nhập cho hệ thống, khi bắt đầu chạy chương trình yêu cầu đăng nhập với tài khoản và mật khẩu để phân quyền thao tác được phép.
    3. Hỗ trợ 3 vai trò người dùng chuẩn:
       - *Quản lý*: Quản lý mọi kho hàng trong hệ thống.
       - *Thủ kho*: Quản lý 1 kho duy nhất được phân công.
       - *Nhân viên kho*: Thao tác nghiệp vụ tại kho.
  - **Kết quả thực hiện**:
    1. **Kiểm tra và xây dựng tiện ích băm mật khẩu SHA-256 (`Classes/PasswordHelper.cs`)**:
       - Quét toàn bộ codebase xác nhận chưa có hàm băm mật khẩu.
       - Tạo mới `Classes/PasswordHelper.cs` cung cấp `HashPassword(string plainPassword)` (SHA-256 chữ thường) và `VerifyPassword(string plainPassword, string hashedPassword)`.
    2. **Xây dựng Form Đăng nhập (`frmDangNhap`) chuẩn WinForms**:
       - Kích thước form Size Nhỏ `ClientSize = (444, 321)`, `FixedDialog`, `CenterScreen`.
       - Gồm các control: Tiêu đề hệ thống, Tên đăng nhập (`txtTenDangNhap`), Mật khẩu (`txtMatKhau`), CheckBox Hiện mật khẩu (`chkHienMatKhau`), ComboBox Kho làm việc (`cboKhoLamViec`), nút Đăng nhập (`btnDangNhap`) và Thoát (`btnThoat`).
       - Tự động nhận diện vai trò tài khoản khi nhập tên đăng nhập để nạp danh sách kho tương ứng.
    3. **Phân quyền thao tác và phạm vi kho chuẩn hóa theo 3 vai trò**:
       - *Quản lý (`MaVaiTro = 1`)*: Toàn quyền truy cập tất cả các phân hệ và menu; quản lý mọi kho hàng; được chọn "Tất cả kho" hoặc kho chi nhánh bất kỳ khi đăng nhập và có quyền đổi kho (`frmDoiKho`).
       - *Thủ kho (`MaVaiTro = 2`)*: Quản lý 1 kho duy nhất được phân công (cố định ComboBox kho khi đăng nhập, ẩn/khóa menu "Đổi kho"); toàn quyền các nghiệp vụ kho của mình (Kiểm kho, Chuyển kho, Hàng hóa, Nhà cung cấp, Nhập hàng, Trả hàng nhập, Báo cáo tồn kho & Thẻ kho).
       - *Nhân viên kho (`MaVaiTro = 3`)*: Thao tác tại kho được phân công (ẩn menu "Đổi kho"); thực hiện Kiểm kho, Chuyển kho, Nhập hàng, Trả hàng nhập, xem Danh sách hàng hóa, xem Báo cáo tồn kho; ẩn các mục quản lý đối tác (Nhà cung cấp) và Thẻ kho sổ sách.
    4. **Cơ chế khởi động chương trình và Đăng xuất linh hoạt**:
       - Tại `Program.cs`: Bọc vòng lặp `while (true)` yêu cầu đăng nhập trước khi hiển thị `frmMain`.
       - Tại `frmMain.cs`: Khi người dùng nhấn "Đăng xuất" trên menu Hệ thống, hệ thống xóa sạch phiên làm việc `SessionContext.XoaPhien()`, đóng `frmMain` và quay lại màn hình `frmDangNhap` ngay lập tức.
    5. **Đồng bộ CSDL & Dữ liệu mẫu**:
       - Cập nhật script `docs/database_update.sql` và `docs/DuLieuMau.sql`: Đồng bộ 3 vai trò chuẩn trong `VaiTro`, 3 nhân viên trong `NhanVien`, 3 tài khoản demo (`quanly`, `thukho`, `nvkho` mật khẩu mặc định `123456`), và phân quyền kho trong `TaiKhoanKho`.
    6. **Kiểm tra chất lượng**:
       - Chạy `tools/KiemTraMaHoa.ps1`: **158/158 file đạt chuẩn UTF-8 with BOM, 0 lỗi Mojibake**.
       - Biên dịch MSBuild: **Build succeeded. 0 Warning(s), 0 Error(s)**.
    1. Đưa TẤT CẢ các form về đúng một chuẩn duy nhất theo form mẫu `frmDanhSachHangHoa` (Font thường `Regular 9.5pt`, màu chữ mặc định hệ thống `SystemColors.ControlText`).
    2. Thêm vào Rule: Chuẩn font chữ, màu chữ bắt buộc tuân thủ tuyệt đối; ban hành quy chuẩn phân tầng kích thước form (Chức năng lớn chuẩn `frmDanhSachHangHoa`: `1782 x 853`, chức năng nhỏ hơn phân làm 3 cỡ: Size Lớn theo `frmHangHoa`: `1182 x 803`, Size Vừa theo `frmDonViCapHang`: `960 x 705`, Size Nhỏ theo `frmDoiKho`: `356 x 136`).
  - **Kết quả thực hiện**:
    1. **Quy chuẩn Rules (`AGENTS.md` & `.agent/rules/03-giao-dien-layout.md`)**:
       - Cập nhật Điều luật 15(e) & Điều luật 18: Nghiêm cấm tự ý in đậm các trường dữ liệu, số tiền, nhãn thống kê; cấm tự ý chọn màu chữ mỗi trường một kiểu. Mọi controls đều dùng `Segoe UI, 9.5pt Regular` và màu mặc định. Ngoại lệ: Tiêu đề lớn 12pt Bold, Header DataGridView 9.5pt Bold, Cảnh báo đỏ Bold cho chênh lệch kiểm kê/tồn <= 0/nợ quá hạn (Điều 15a), Màu trạng thái 3 nhóm (Điều 15b/19a).
       - Bổ sung quy chuẩn phân tầng kích thước Form rõ ràng cho Chức năng lớn và 3 cỡ Chức năng nhỏ (Lớn / Vừa / Nhỏ).
    2. **Chuẩn hóa toàn diện mã nguồn các Form**:
       - `TraHangNhap`: Gỡ bỏ toàn bộ ForeColor xám/slate/xanh đen và gỡ bỏ `FontStyle.Bold` trên `frmChiTietTraHangNhap` và `frmThemPhieuTraHang`.
       - `NhapHang`: Gỡ bỏ ForeColor và Bold trên `frmChiTietPhieuNhap`, `frmThemPhieuNhap`, `frmTraHangNhap`.
       - `KiemKho`: Gỡ bỏ Bold và ForeColor trên `frmChiTietKiemKho` (ảnh 1), `frmThemPhieuKiemKho`, `frmKiemKho`, `frmNhapKiemKhoFile`.
       - `ChuyenKho`: Gỡ bỏ ForeColor và Bold trên `frmChiTietChuyenKho`, `frmThemPhieuChuyenKho`, `frmNhanChuyenKho`, `frmChuyenKho`.
       - `NhaCungCap`: Gỡ bỏ ForeColor và Bold trên `frmDonViCapHang`, `frmNhaCungCap`, `frmNhomNhaCungCap`.
       - `DonViTinh`: Gỡ bỏ ForeColor và Bold trên `frmChiTietDonViTinh`, `frmDonViTinh`.
       - `BaoCao`: Gỡ bỏ ForeColor `DimGray`/Slate và Bold trên thẻ KPI của `frmBaoCaoTonKho` và `frmBaoCaoTheKho`.
    3. **Kiểm tra biên dịch & Chuẩn định dạng**:
       - 100% file mã nguồn lưu định dạng **UTF-8 with BOM** (Điều luật 18).
       - Biên dịch MSBuild: **Build succeeded. 0 Warning(s), 0 Error(s)**.


- **Phiên 32 (2026-10-24) – Chuẩn hóa toàn bộ trạng thái Chuyển kho và các thuộc tính Báo cáo về font chữ thường theo mẫu Trả hàng nhập**:
  - **Nhu cầu**: Người dùng phản hồi thanh/cột trạng thái của chức năng Chuyển kho và nhiều thuộc tính ở 2 chức năng Báo cáo (Báo cáo tồn kho, Báo cáo thẻ kho) bị in đậm không theo yêu cầu. Yêu cầu chuẩn hóa tất cả về font chữ thường (FontStyle.Regular) và kiểu chữ tiêu chuẩn như thuộc tính trạng thái trong form Trả hàng nhập (rmTraHangNhap).
  - **Kết quả thực hiện**:
    1. **rmChuyenKho & rmChiTietChuyenKho (Chuyển kho)**:
       - Bỏ FontStyle.Bold tại colTrangThai trong sự kiện dgvChuyenKho_CellFormatting cho tất cả các trạng thái "Đã nhận", "Đang chuyển", "Phiếu tạm"; chỉ áp dụng màu sắc 3 nhóm chuẩn (Xanh lá, Vàng amber, Xám nhạt).
       - Chuyển lblTrangThai.Font trên rmChiTietChuyenKho sang 10pt Regular đồng bộ rmChiTietTraHangNhap.
    2. **rmBaoCaoTonKho (Báo cáo tồn kho)**:
       - Bỏ FontStyle.Bold tại colTrangThai cho "Đang kinh doanh", chỉ giữ màu xanh lá và font chữ thường theo chuẩn Điều luật 15b.
       - Cảnh báo tồn kho $\le$ 0 vẫn duy trì chữ đỏ in đậm chuẩn xác theo Điều luật 15a.
    3. **rmBaoCaoTheKho (Báo cáo thẻ kho)**:
       - Bỏ FontStyle.Bold tại colLoaiGiaoDich cho các giao dịch "Nhập mua NCC", "Chuyển kho đến".
       - Đổi text hiển thị dòng đầu và dòng cuối từ viết hoa toàn bộ TỒN ĐẦU KỲ / TỒN CUỐI KỲ thành kiểu chữ tiêu chuẩn Tồn đầu kỳ / Tồn cuối kỳ.
       - Bỏ việc ép FontStyle.Bold lên toàn bộ các cột của dòng Tồn đầu kỳ và Tồn cuối kỳ, chỉ duy trì màu nền phân biệt nhẹ Color.FromArgb(241, 245, 249).
    4. **rmDonViTinh (Danh mục ĐVT)**:
       - Đồng bộ loại bỏ FontStyle.Bold trên colTrangThai khi "Đang sử dụng".
    5. **Kiểm tra biên dịch & Chuẩn định dạng**:
       - 100% file mã nguồn lưu định dạng **UTF-8 with BOM** (Điều luật 18).
       - Biên dịch MSBuild thành công: **Build succeeded. 0 Warning(s), 0 Error(s)**.
       - Đồng bộ đầy đủ docs/specs/the-kho.md, docs/specs/bao-cao-ton-kho.md, docs/specs/chuyen-kho.md, docs/quyet-dinh.md và PROGRESS.md.

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

- **Phiên 47 (2026-10-25) – Xây dựng Form Đăng nhập hệ thống, Tiện ích băm mật khẩu SHA-256 an toàn và Phân quyền thao tác theo 3 vai trò chuẩn**:
  - **Nhu cầu**: Người dùng yêu cầu:
    1. Kiểm tra 1 lượt classes và các file liên quan có thể có chứa hàm băm của mật khẩu. Nếu tồn tại, gọi hàm hash đó; nếu chưa, cho phép tạo class `PasswordHelper.cs`.
    2. Tạo form đăng nhập cho hệ thống, khi bắt đầu chạy chương trình yêu cầu đăng nhập với tài khoản và mật khẩu để phân quyền thao tác được phép.
    3. Hỗ trợ 3 vai trò người dùng chuẩn:
       - *Quản lý*: Quản lý mọi kho hàng trong hệ thống.
       - *Thủ kho*: Quản lý 1 kho duy nhất được phân công.
       - *Nhân viên kho*: Thao tác nghiệp vụ tại kho.
  - **Kết quả thực hiện**:
    1. **Kiểm tra và xây dựng tiện ích băm mật khẩu SHA-256 (`Classes/PasswordHelper.cs`)**:
       - Quét toàn bộ codebase xác nhận chưa có hàm băm mật khẩu.
       - Tạo mới `Classes/PasswordHelper.cs` cung cấp `HashPassword(string plainPassword)` (SHA-256 chữ thường) và `VerifyPassword(string plainPassword, string hashedPassword)`.
    2. **Xây dựng Form Đăng nhập (`frmDangNhap`) chuẩn WinForms**:
       - Kích thước form Size Nhỏ `ClientSize = (444, 321)`, `FixedDialog`, `CenterScreen`.
       - Gồm các control: Tiêu đề hệ thống, Tên đăng nhập (`txtTenDangNhap`), Mật khẩu (`txtMatKhau`), CheckBox Hiện mật khẩu (`chkHienMatKhau`), ComboBox Kho làm việc (`cboKhoLamViec`), nút Đăng nhập (`btnDangNhap`) và Thoát (`btnThoat`).
       - Tự động nhận diện vai trò tài khoản khi nhập tên đăng nhập để nạp danh sách kho tương ứng.
    3. **Phân quyền thao tác và phạm vi kho chuẩn hóa theo 3 vai trò**:
       - *Quản lý (`MaVaiTro = 1`)*: Toàn quyền truy cập tất cả các phân hệ và menu; quản lý mọi kho hàng; được chọn "Tất cả kho" hoặc kho chi nhánh bất kỳ khi đăng nhập và có quyền đổi kho (`frmDoiKho`).
       - *Thủ kho (`MaVaiTro = 2`)*: Quản lý 1 kho duy nhất được phân công (cố định ComboBox kho khi đăng nhập, ẩn/khóa menu "Đổi kho"); toàn quyền các nghiệp vụ kho của mình (Kiểm kho, Chuyển kho, Hàng hóa, Nhà cung cấp, Nhập hàng, Trả hàng nhập, Báo cáo tồn kho & Thẻ kho).
       - *Nhân viên kho (`MaVaiTro = 3`)*: Thao tác tại kho được phân công (ẩn menu "Đổi kho"); thực hiện Kiểm kho, Chuyển kho, Nhập hàng, Trả hàng nhập, xem Danh sách hàng hóa, xem Báo cáo tồn kho; ẩn các mục quản lý đối tác (Nhà cung cấp) và Thẻ kho sổ sách.
    4. **Cơ chế khởi động chương trình và Đăng xuất linh hoạt**:
       - Tại `Program.cs`: Bọc vòng lặp `while (true)` yêu cầu đăng nhập trước khi hiển thị `frmMain`.
       - Tại `frmMain.cs`: Khi người dùng nhấn "Đăng xuất" trên menu Hệ thống, hệ thống xóa sạch phiên làm việc `SessionContext.XoaPhien()`, đóng `frmMain` và quay lại màn hình `frmDangNhap` ngay lập tức.
    5. **Đồng bộ CSDL & Dữ liệu mẫu**:
       - Cập nhật script `docs/database_update.sql` và `docs/DuLieuMau.sql`: Đồng bộ 3 vai trò chuẩn trong `VaiTro`, 3 nhân viên trong `NhanVien`, 3 tài khoản demo (`quanly`, `thukho`, `nvkho` mật khẩu mặc định `123456`), và phân quyền kho trong `TaiKhoanKho`.
    6. **Kiểm tra chất lượng**:
       - Chạy `tools/KiemTraMaHoa.ps1`: **158/158 file đạt chuẩn UTF-8 with BOM, 0 lỗi Mojibake**.
       - Biên dịch MSBuild: **Build succeeded. 0 Warning(s), 0 Error(s)**.
  - **Kết quả thực hiện**:
    1. **Form modal Đổi kho làm việc (`GUI/Forms/DoiKho/frmDoiKho`)**:
       - Khớp 100% giao diện ảnh mẫu (`media_1792806433137_dfddc08d.png`): Kích thước nhỏ gọn `430x170`, `FixedDialog`, tiêu đề "Chọn kho làm việc".
       - Nạp ComboBox kho từ CSDL: Nếu là Admin nạp toàn bộ kho đang hoạt động; nếu là Thủ kho/Nhân viên chỉ nạp các kho được gán quyền qua bảng `TaiKhoanKho`.
       - Khi người dùng bấm "Xác nhận", cập nhật `SessionContext.MaKhoHienTai`, `TenKhoHienTai`, tự động đổi tiêu đề `frmMain` kèm tên kho và làm mới toàn bộ Form con đang mở trong MDI.
    2. **Phân hệ Chuyển kho liên chi nhánh (`GUI/Forms/ChuyenKho/`)**:
       - `frmChuyenKho`: Màn hình danh sách chuẩn tỉ lệ `1782x853` FixedSingle, bộ lọc bên trái 100% ComboBox, cụm thời gian ở đáy theo Điều luật 16, DataGridView phân trang, lọc theo trạng thái (`DA_XUAT`, `DA_NHAN`, `NHAP`, `HUY`), hỗ trợ tìm kiếm tiếng Việt không dấu.
       - `frmThemPhieuChuyenKho`: Lập phiếu chuyển kho mới; chọn kho nguồn và kho đích (chặn chọn trùng kho); chọn hàng hóa nạp tồn sổ sách tức thời từ `vw_TonKhoHienTai`; chặn xuất chuyển vượt quá tồn thực tế (Luật 6); hỗ trợ phím tắt và thao tác mượt mà; các nút đáy sắp xếp chuẩn `Bỏ qua` ➜ `Lưu tạm` (`NHAP`) ➜ `Lưu` (`DA_XUAT`).
       - `frmChiTietChuyenKho`: Màn hình Master-Detail xem và cập nhật phiếu chuyển; hiển thị nhãn trạng thái dòng chữ màu (Điều luật 15b, 19a); nút "Nhận hàng" cho kho đích; nút "Xóa" tuân thủ Điều luật 15d & Luật 5 (chỉ xóa phiếu nháp, vô hiệu hóa khi phiếu đã hoàn tất/đã xuất); nút "Xuất file" CSV định dạng UTF-8 BOM.

- **Phiên 47 (2026-10-25) – Xây dựng Form Đăng nhập hệ thống, Tiện ích băm mật khẩu SHA-256 an toàn và Phân quyền thao tác theo 3 vai trò chuẩn**:
  - **Nhu cầu**: Người dùng yêu cầu:
    1. Kiểm tra 1 lượt classes và các file liên quan có thể có chứa hàm băm của mật khẩu. Nếu tồn tại, gọi hàm hash đó; nếu chưa, cho phép tạo class `PasswordHelper.cs`.
    2. Tạo form đăng nhập cho hệ thống, khi bắt đầu chạy chương trình yêu cầu đăng nhập với tài khoản và mật khẩu để phân quyền thao tác được phép.
    3. Hỗ trợ 3 vai trò người dùng chuẩn:
       - *Quản lý*: Quản lý mọi kho hàng trong hệ thống.
       - *Thủ kho*: Quản lý 1 kho duy nhất được phân công.
       - *Nhân viên kho*: Thao tác nghiệp vụ tại kho.
  - **Kết quả thực hiện**:
    1. **Kiểm tra và xây dựng tiện ích băm mật khẩu SHA-256 (`Classes/PasswordHelper.cs`)**:
       - Quét toàn bộ codebase xác nhận chưa có hàm băm mật khẩu.
       - Tạo mới `Classes/PasswordHelper.cs` cung cấp `HashPassword(string plainPassword)` (SHA-256 chữ thường) và `VerifyPassword(string plainPassword, string hashedPassword)`.
    2. **Xây dựng Form Đăng nhập (`frmDangNhap`) chuẩn WinForms**:
       - Kích thước form Size Nhỏ `ClientSize = (444, 321)`, `FixedDialog`, `CenterScreen`.
       - Gồm các control: Tiêu đề hệ thống, Tên đăng nhập (`txtTenDangNhap`), Mật khẩu (`txtMatKhau`), CheckBox Hiện mật khẩu (`chkHienMatKhau`), ComboBox Kho làm việc (`cboKhoLamViec`), nút Đăng nhập (`btnDangNhap`) và Thoát (`btnThoat`).
       - Tự động nhận diện vai trò tài khoản khi nhập tên đăng nhập để nạp danh sách kho tương ứng.
    3. **Phân quyền thao tác và phạm vi kho chuẩn hóa theo 3 vai trò**:
       - *Quản lý (`MaVaiTro = 1`)*: Toàn quyền truy cập tất cả các phân hệ và menu; quản lý mọi kho hàng; được chọn "Tất cả kho" hoặc kho chi nhánh bất kỳ khi đăng nhập và có quyền đổi kho (`frmDoiKho`).
       - *Thủ kho (`MaVaiTro = 2`)*: Quản lý 1 kho duy nhất được phân công (cố định ComboBox kho khi đăng nhập, ẩn/khóa menu "Đổi kho"); toàn quyền các nghiệp vụ kho của mình (Kiểm kho, Chuyển kho, Hàng hóa, Nhà cung cấp, Nhập hàng, Trả hàng nhập, Báo cáo tồn kho & Thẻ kho).
       - *Nhân viên kho (`MaVaiTro = 3`)*: Thao tác tại kho được phân công (ẩn menu "Đổi kho"); thực hiện Kiểm kho, Chuyển kho, Nhập hàng, Trả hàng nhập, xem Danh sách hàng hóa, xem Báo cáo tồn kho; ẩn các mục quản lý đối tác (Nhà cung cấp) và Thẻ kho sổ sách.
    4. **Cơ chế khởi động chương trình và Đăng xuất linh hoạt**:
       - Tại `Program.cs`: Bọc vòng lặp `while (true)` yêu cầu đăng nhập trước khi hiển thị `frmMain`.
       - Tại `frmMain.cs`: Khi người dùng nhấn "Đăng xuất" trên menu Hệ thống, hệ thống xóa sạch phiên làm việc `SessionContext.XoaPhien()`, đóng `frmMain` và quay lại màn hình `frmDangNhap` ngay lập tức.
    5. **Đồng bộ CSDL & Dữ liệu mẫu**:
       - Cập nhật script `docs/database_update.sql` và `docs/DuLieuMau.sql`: Đồng bộ 3 vai trò chuẩn trong `VaiTro`, 3 nhân viên trong `NhanVien`, 3 tài khoản demo (`quanly`, `thukho`, `nvkho` mật khẩu mặc định `123456`), và phân quyền kho trong `TaiKhoanKho`.
    6. **Kiểm tra chất lượng**:
       - Chạy `tools/KiemTraMaHoa.ps1`: **158/158 file đạt chuẩn UTF-8 with BOM, 0 lỗi Mojibake**.
       - Biên dịch MSBuild: **Build succeeded. 0 Warning(s), 0 Error(s)**.
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

- **Phiên 47 (2026-10-25) – Xây dựng Form Đăng nhập hệ thống, Tiện ích băm mật khẩu SHA-256 an toàn và Phân quyền thao tác theo 3 vai trò chuẩn**:
  - **Nhu cầu**: Người dùng yêu cầu:
    1. Kiểm tra 1 lượt classes và các file liên quan có thể có chứa hàm băm của mật khẩu. Nếu tồn tại, gọi hàm hash đó; nếu chưa, cho phép tạo class `PasswordHelper.cs`.
    2. Tạo form đăng nhập cho hệ thống, khi bắt đầu chạy chương trình yêu cầu đăng nhập với tài khoản và mật khẩu để phân quyền thao tác được phép.
    3. Hỗ trợ 3 vai trò người dùng chuẩn:
       - *Quản lý*: Quản lý mọi kho hàng trong hệ thống.
       - *Thủ kho*: Quản lý 1 kho duy nhất được phân công.
       - *Nhân viên kho*: Thao tác nghiệp vụ tại kho.
  - **Kết quả thực hiện**:
    1. **Kiểm tra và xây dựng tiện ích băm mật khẩu SHA-256 (`Classes/PasswordHelper.cs`)**:
       - Quét toàn bộ codebase xác nhận chưa có hàm băm mật khẩu.
       - Tạo mới `Classes/PasswordHelper.cs` cung cấp `HashPassword(string plainPassword)` (SHA-256 chữ thường) và `VerifyPassword(string plainPassword, string hashedPassword)`.
    2. **Xây dựng Form Đăng nhập (`frmDangNhap`) chuẩn WinForms**:
       - Kích thước form Size Nhỏ `ClientSize = (444, 321)`, `FixedDialog`, `CenterScreen`.
       - Gồm các control: Tiêu đề hệ thống, Tên đăng nhập (`txtTenDangNhap`), Mật khẩu (`txtMatKhau`), CheckBox Hiện mật khẩu (`chkHienMatKhau`), ComboBox Kho làm việc (`cboKhoLamViec`), nút Đăng nhập (`btnDangNhap`) và Thoát (`btnThoat`).
       - Tự động nhận diện vai trò tài khoản khi nhập tên đăng nhập để nạp danh sách kho tương ứng.
    3. **Phân quyền thao tác và phạm vi kho chuẩn hóa theo 3 vai trò**:
       - *Quản lý (`MaVaiTro = 1`)*: Toàn quyền truy cập tất cả các phân hệ và menu; quản lý mọi kho hàng; được chọn "Tất cả kho" hoặc kho chi nhánh bất kỳ khi đăng nhập và có quyền đổi kho (`frmDoiKho`).
       - *Thủ kho (`MaVaiTro = 2`)*: Quản lý 1 kho duy nhất được phân công (cố định ComboBox kho khi đăng nhập, ẩn/khóa menu "Đổi kho"); toàn quyền các nghiệp vụ kho của mình (Kiểm kho, Chuyển kho, Hàng hóa, Nhà cung cấp, Nhập hàng, Trả hàng nhập, Báo cáo tồn kho & Thẻ kho).
       - *Nhân viên kho (`MaVaiTro = 3`)*: Thao tác tại kho được phân công (ẩn menu "Đổi kho"); thực hiện Kiểm kho, Chuyển kho, Nhập hàng, Trả hàng nhập, xem Danh sách hàng hóa, xem Báo cáo tồn kho; ẩn các mục quản lý đối tác (Nhà cung cấp) và Thẻ kho sổ sách.
    4. **Cơ chế khởi động chương trình và Đăng xuất linh hoạt**:
       - Tại `Program.cs`: Bọc vòng lặp `while (true)` yêu cầu đăng nhập trước khi hiển thị `frmMain`.
       - Tại `frmMain.cs`: Khi người dùng nhấn "Đăng xuất" trên menu Hệ thống, hệ thống xóa sạch phiên làm việc `SessionContext.XoaPhien()`, đóng `frmMain` và quay lại màn hình `frmDangNhap` ngay lập tức.
    5. **Đồng bộ CSDL & Dữ liệu mẫu**:
       - Cập nhật script `docs/database_update.sql` và `docs/DuLieuMau.sql`: Đồng bộ 3 vai trò chuẩn trong `VaiTro`, 3 nhân viên trong `NhanVien`, 3 tài khoản demo (`quanly`, `thukho`, `nvkho` mật khẩu mặc định `123456`), và phân quyền kho trong `TaiKhoanKho`.
    6. **Kiểm tra chất lượng**:
       - Chạy `tools/KiemTraMaHoa.ps1`: **158/158 file đạt chuẩn UTF-8 with BOM, 0 lỗi Mojibake**.
       - Biên dịch MSBuild: **Build succeeded. 0 Warning(s), 0 Error(s)**.
    2. Thống nhất toàn bộ các nút chức năng nhỏ được để ở cuối form (mẫu như `frmThemPhieuNhap`), không để lên đầu như ảnh 2.
    3. Chốt thứ tự các nút là `Bỏ qua`, `Lưu tạm` và `Lưu` cho toàn bộ các form (thêm vào rule).
  - **Kết quả thực hiện**:
    1. **Chuẩn hóa nhãn trạng thái (Điều luật 19a)**:
       - `frmChiTietKiemKho.Designer.cs`: Bỏ `BackColor`, `Padding(8, 3, 8, 3)`, chuyển `lblTrangThaiBadge` sang font `Segoe UI, 10pt Regular`, chỉ là dòng chữ hiển thị màu trạng thái tự nhiên theo mẫu `frmChiTietPhieuNhap`.
       - `frmChiTietKiemKho.cs`: Bỏ các dòng gán `BackColor`, cập nhật màu `ForeColor` theo 3 nhóm chuẩn: "Đã cân bằng" xanh lá `Color.FromArgb(22, 163, 74)`, "Phiếu tạm" vàng amber `Color.FromArgb(217, 119, 6)`, "Đã hủy" xám nhạt `Color.FromArgb(100, 116, 139)`.
    2. **Thống nhất vị trí & thứ tự nút chức năng nhỏ ở cuối form (Điều luật 19b)**:
       - `frmThemPhieuKiemKho`:
         + Xóa bỏ các nút `[Nhập file]`, `[Lưu tạm]`, `[Cân bằng kho]`, `[Đóng]` khỏi vùng trên (`pnlTop`), vùng trên chỉ còn nhãn tiêu đề form "Thêm phiếu kiểm kho" theo chuẩn `frmThemPhieuNhap`.

- **Phiên 47 (2026-10-25) – Xây dựng Form Đăng nhập hệ thống, Tiện ích băm mật khẩu SHA-256 an toàn và Phân quyền thao tác theo 3 vai trò chuẩn**:
  - **Nhu cầu**: Người dùng yêu cầu:
    1. Kiểm tra 1 lượt classes và các file liên quan có thể có chứa hàm băm của mật khẩu. Nếu tồn tại, gọi hàm hash đó; nếu chưa, cho phép tạo class `PasswordHelper.cs`.
    2. Tạo form đăng nhập cho hệ thống, khi bắt đầu chạy chương trình yêu cầu đăng nhập với tài khoản và mật khẩu để phân quyền thao tác được phép.
    3. Hỗ trợ 3 vai trò người dùng chuẩn:
       - *Quản lý*: Quản lý mọi kho hàng trong hệ thống.
       - *Thủ kho*: Quản lý 1 kho duy nhất được phân công.
       - *Nhân viên kho*: Thao tác nghiệp vụ tại kho.
  - **Kết quả thực hiện**:
    1. **Kiểm tra và xây dựng tiện ích băm mật khẩu SHA-256 (`Classes/PasswordHelper.cs`)**:
       - Quét toàn bộ codebase xác nhận chưa có hàm băm mật khẩu.
       - Tạo mới `Classes/PasswordHelper.cs` cung cấp `HashPassword(string plainPassword)` (SHA-256 chữ thường) và `VerifyPassword(string plainPassword, string hashedPassword)`.
    2. **Xây dựng Form Đăng nhập (`frmDangNhap`) chuẩn WinForms**:
       - Kích thước form Size Nhỏ `ClientSize = (444, 321)`, `FixedDialog`, `CenterScreen`.
       - Gồm các control: Tiêu đề hệ thống, Tên đăng nhập (`txtTenDangNhap`), Mật khẩu (`txtMatKhau`), CheckBox Hiện mật khẩu (`chkHienMatKhau`), ComboBox Kho làm việc (`cboKhoLamViec`), nút Đăng nhập (`btnDangNhap`) và Thoát (`btnThoat`).
       - Tự động nhận diện vai trò tài khoản khi nhập tên đăng nhập để nạp danh sách kho tương ứng.
    3. **Phân quyền thao tác và phạm vi kho chuẩn hóa theo 3 vai trò**:
       - *Quản lý (`MaVaiTro = 1`)*: Toàn quyền truy cập tất cả các phân hệ và menu; quản lý mọi kho hàng; được chọn "Tất cả kho" hoặc kho chi nhánh bất kỳ khi đăng nhập và có quyền đổi kho (`frmDoiKho`).
       - *Thủ kho (`MaVaiTro = 2`)*: Quản lý 1 kho duy nhất được phân công (cố định ComboBox kho khi đăng nhập, ẩn/khóa menu "Đổi kho"); toàn quyền các nghiệp vụ kho của mình (Kiểm kho, Chuyển kho, Hàng hóa, Nhà cung cấp, Nhập hàng, Trả hàng nhập, Báo cáo tồn kho & Thẻ kho).
       - *Nhân viên kho (`MaVaiTro = 3`)*: Thao tác tại kho được phân công (ẩn menu "Đổi kho"); thực hiện Kiểm kho, Chuyển kho, Nhập hàng, Trả hàng nhập, xem Danh sách hàng hóa, xem Báo cáo tồn kho; ẩn các mục quản lý đối tác (Nhà cung cấp) và Thẻ kho sổ sách.
    4. **Cơ chế khởi động chương trình và Đăng xuất linh hoạt**:
       - Tại `Program.cs`: Bọc vòng lặp `while (true)` yêu cầu đăng nhập trước khi hiển thị `frmMain`.
       - Tại `frmMain.cs`: Khi người dùng nhấn "Đăng xuất" trên menu Hệ thống, hệ thống xóa sạch phiên làm việc `SessionContext.XoaPhien()`, đóng `frmMain` và quay lại màn hình `frmDangNhap` ngay lập tức.
    5. **Đồng bộ CSDL & Dữ liệu mẫu**:
       - Cập nhật script `docs/database_update.sql` và `docs/DuLieuMau.sql`: Đồng bộ 3 vai trò chuẩn trong `VaiTro`, 3 nhân viên trong `NhanVien`, 3 tài khoản demo (`quanly`, `thukho`, `nvkho` mật khẩu mặc định `123456`), và phân quyền kho trong `TaiKhoanKho`.
    6. **Kiểm tra chất lượng**:
       - Chạy `tools/KiemTraMaHoa.ps1`: **158/158 file đạt chuẩn UTF-8 with BOM, 0 lỗi Mojibake**.
       - Biên dịch MSBuild: **Build succeeded. 0 Warning(s), 0 Error(s)**.
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

- **Phiên 47 (2026-10-25) – Xây dựng Form Đăng nhập hệ thống, Tiện ích băm mật khẩu SHA-256 an toàn và Phân quyền thao tác theo 3 vai trò chuẩn**:
  - **Nhu cầu**: Người dùng yêu cầu:
    1. Kiểm tra 1 lượt classes và các file liên quan có thể có chứa hàm băm của mật khẩu. Nếu tồn tại, gọi hàm hash đó; nếu chưa, cho phép tạo class `PasswordHelper.cs`.
    2. Tạo form đăng nhập cho hệ thống, khi bắt đầu chạy chương trình yêu cầu đăng nhập với tài khoản và mật khẩu để phân quyền thao tác được phép.
    3. Hỗ trợ 3 vai trò người dùng chuẩn:
       - *Quản lý*: Quản lý mọi kho hàng trong hệ thống.
       - *Thủ kho*: Quản lý 1 kho duy nhất được phân công.
       - *Nhân viên kho*: Thao tác nghiệp vụ tại kho.
  - **Kết quả thực hiện**:
    1. **Kiểm tra và xây dựng tiện ích băm mật khẩu SHA-256 (`Classes/PasswordHelper.cs`)**:
       - Quét toàn bộ codebase xác nhận chưa có hàm băm mật khẩu.
       - Tạo mới `Classes/PasswordHelper.cs` cung cấp `HashPassword(string plainPassword)` (SHA-256 chữ thường) và `VerifyPassword(string plainPassword, string hashedPassword)`.
    2. **Xây dựng Form Đăng nhập (`frmDangNhap`) chuẩn WinForms**:
       - Kích thước form Size Nhỏ `ClientSize = (444, 321)`, `FixedDialog`, `CenterScreen`.
       - Gồm các control: Tiêu đề hệ thống, Tên đăng nhập (`txtTenDangNhap`), Mật khẩu (`txtMatKhau`), CheckBox Hiện mật khẩu (`chkHienMatKhau`), ComboBox Kho làm việc (`cboKhoLamViec`), nút Đăng nhập (`btnDangNhap`) và Thoát (`btnThoat`).
       - Tự động nhận diện vai trò tài khoản khi nhập tên đăng nhập để nạp danh sách kho tương ứng.
    3. **Phân quyền thao tác và phạm vi kho chuẩn hóa theo 3 vai trò**:
       - *Quản lý (`MaVaiTro = 1`)*: Toàn quyền truy cập tất cả các phân hệ và menu; quản lý mọi kho hàng; được chọn "Tất cả kho" hoặc kho chi nhánh bất kỳ khi đăng nhập và có quyền đổi kho (`frmDoiKho`).
       - *Thủ kho (`MaVaiTro = 2`)*: Quản lý 1 kho duy nhất được phân công (cố định ComboBox kho khi đăng nhập, ẩn/khóa menu "Đổi kho"); toàn quyền các nghiệp vụ kho của mình (Kiểm kho, Chuyển kho, Hàng hóa, Nhà cung cấp, Nhập hàng, Trả hàng nhập, Báo cáo tồn kho & Thẻ kho).
       - *Nhân viên kho (`MaVaiTro = 3`)*: Thao tác tại kho được phân công (ẩn menu "Đổi kho"); thực hiện Kiểm kho, Chuyển kho, Nhập hàng, Trả hàng nhập, xem Danh sách hàng hóa, xem Báo cáo tồn kho; ẩn các mục quản lý đối tác (Nhà cung cấp) và Thẻ kho sổ sách.
    4. **Cơ chế khởi động chương trình và Đăng xuất linh hoạt**:
       - Tại `Program.cs`: Bọc vòng lặp `while (true)` yêu cầu đăng nhập trước khi hiển thị `frmMain`.
       - Tại `frmMain.cs`: Khi người dùng nhấn "Đăng xuất" trên menu Hệ thống, hệ thống xóa sạch phiên làm việc `SessionContext.XoaPhien()`, đóng `frmMain` và quay lại màn hình `frmDangNhap` ngay lập tức.
    5. **Đồng bộ CSDL & Dữ liệu mẫu**:
       - Cập nhật script `docs/database_update.sql` và `docs/DuLieuMau.sql`: Đồng bộ 3 vai trò chuẩn trong `VaiTro`, 3 nhân viên trong `NhanVien`, 3 tài khoản demo (`quanly`, `thukho`, `nvkho` mật khẩu mặc định `123456`), và phân quyền kho trong `TaiKhoanKho`.
    6. **Kiểm tra chất lượng**:
       - Chạy `tools/KiemTraMaHoa.ps1`: **158/158 file đạt chuẩn UTF-8 with BOM, 0 lỗi Mojibake**.
       - Biên dịch MSBuild: **Build succeeded. 0 Warning(s), 0 Error(s)**.
       - `frmChiTietKiemKho`: Form chi tiết khi nhấn đúp vào 1 phiếu kiểm kho (chuẩn bố cục ảnh 3). Hiển thị đầy đủ thông tin Header (Mã phiếu, Badge trạng thái, Người tạo/Ngày tạo, Người cân bằng/Ngày cân bằng, Tên kho), bảng chi tiết dòng hàng (Mã hàng, Tên hàng, ĐVT, Tồn kho, Thực tế, SL lệch, Đơn giá, Giá trị lệch), vùng ghi chú và vùng tổng kết 4 chỉ số (Tổng thực tế, Tổng lệch tăng, Tổng lệch giảm, Tổng chênh lệch). Ở thanh dưới chỉ giữ lại 2 nút: **Xóa** (khóa không cho xóa phiếu đã cân bằng theo Luật 6, cho phép xóa phiếu tạm `NHAP` kèm xác nhận) và **Xuất file** (kèm nút Đóng).
       - `frmNhapKiemKhoFile`: Form nhập file kiểm kê hỗ trợ .xlsx, .xls, .csv, có nút tải file mẫu Excel (.xls) và file mẫu CSV (.csv) kèm dữ liệu mẫu thực tế từ DB; validate từng dòng và nạp danh sách hợp lệ vào `frmThemPhieuKiemKho`.
       - `frmHienCotKiemKho`: Form tùy chọn ẩn/hiện cột bảng kiểm kho.
    2. **Đăng ký sự kiện mở form trên `frmMain`**: Đăng ký `mnuKiemKho_Click` mở form `frmKiemKho` trong MDI Container (hoặc `Activate()` nếu form đã mở).
    3. **Đăng ký file vào `BTL-LTTQ-2.csproj`**.
    4. **Đồng bộ đặc tả `docs/specs/kiem-kho.md` và `docs/quyet-dinh.md`**.
    5. **Kiểm tra biên dịch**: Chạy build MSBuild thành công **0 Warning(s), 0 Error(s)**.
  - **Nhu cầu**: 
    1. Vẫn tồn tại lỗi khi truy vấn database: `Invalid column name 'MaPhieuNhap'` khi xem chi tiết phiếu trả hàng nhập trong `frmTraHangNhap`. Cho phép dùng query SELECT để kiểm tra cấu trúc schema thực tế và quét chuẩn hóa toàn bộ code.

- **Phiên 47 (2026-10-25) – Xây dựng Form Đăng nhập hệ thống, Tiện ích băm mật khẩu SHA-256 an toàn và Phân quyền thao tác theo 3 vai trò chuẩn**:
  - **Nhu cầu**: Người dùng yêu cầu:
    1. Kiểm tra 1 lượt classes và các file liên quan có thể có chứa hàm băm của mật khẩu. Nếu tồn tại, gọi hàm hash đó; nếu chưa, cho phép tạo class `PasswordHelper.cs`.
    2. Tạo form đăng nhập cho hệ thống, khi bắt đầu chạy chương trình yêu cầu đăng nhập với tài khoản và mật khẩu để phân quyền thao tác được phép.
    3. Hỗ trợ 3 vai trò người dùng chuẩn:
       - *Quản lý*: Quản lý mọi kho hàng trong hệ thống.
       - *Thủ kho*: Quản lý 1 kho duy nhất được phân công.
       - *Nhân viên kho*: Thao tác nghiệp vụ tại kho.
  - **Kết quả thực hiện**:
    1. **Kiểm tra và xây dựng tiện ích băm mật khẩu SHA-256 (`Classes/PasswordHelper.cs`)**:
       - Quét toàn bộ codebase xác nhận chưa có hàm băm mật khẩu.
       - Tạo mới `Classes/PasswordHelper.cs` cung cấp `HashPassword(string plainPassword)` (SHA-256 chữ thường) và `VerifyPassword(string plainPassword, string hashedPassword)`.
    2. **Xây dựng Form Đăng nhập (`frmDangNhap`) chuẩn WinForms**:
       - Kích thước form Size Nhỏ `ClientSize = (444, 321)`, `FixedDialog`, `CenterScreen`.
       - Gồm các control: Tiêu đề hệ thống, Tên đăng nhập (`txtTenDangNhap`), Mật khẩu (`txtMatKhau`), CheckBox Hiện mật khẩu (`chkHienMatKhau`), ComboBox Kho làm việc (`cboKhoLamViec`), nút Đăng nhập (`btnDangNhap`) và Thoát (`btnThoat`).
       - Tự động nhận diện vai trò tài khoản khi nhập tên đăng nhập để nạp danh sách kho tương ứng.
    3. **Phân quyền thao tác và phạm vi kho chuẩn hóa theo 3 vai trò**:
       - *Quản lý (`MaVaiTro = 1`)*: Toàn quyền truy cập tất cả các phân hệ và menu; quản lý mọi kho hàng; được chọn "Tất cả kho" hoặc kho chi nhánh bất kỳ khi đăng nhập và có quyền đổi kho (`frmDoiKho`).
       - *Thủ kho (`MaVaiTro = 2`)*: Quản lý 1 kho duy nhất được phân công (cố định ComboBox kho khi đăng nhập, ẩn/khóa menu "Đổi kho"); toàn quyền các nghiệp vụ kho của mình (Kiểm kho, Chuyển kho, Hàng hóa, Nhà cung cấp, Nhập hàng, Trả hàng nhập, Báo cáo tồn kho & Thẻ kho).
       - *Nhân viên kho (`MaVaiTro = 3`)*: Thao tác tại kho được phân công (ẩn menu "Đổi kho"); thực hiện Kiểm kho, Chuyển kho, Nhập hàng, Trả hàng nhập, xem Danh sách hàng hóa, xem Báo cáo tồn kho; ẩn các mục quản lý đối tác (Nhà cung cấp) và Thẻ kho sổ sách.
    4. **Cơ chế khởi động chương trình và Đăng xuất linh hoạt**:
       - Tại `Program.cs`: Bọc vòng lặp `while (true)` yêu cầu đăng nhập trước khi hiển thị `frmMain`.
       - Tại `frmMain.cs`: Khi người dùng nhấn "Đăng xuất" trên menu Hệ thống, hệ thống xóa sạch phiên làm việc `SessionContext.XoaPhien()`, đóng `frmMain` và quay lại màn hình `frmDangNhap` ngay lập tức.
    5. **Đồng bộ CSDL & Dữ liệu mẫu**:
       - Cập nhật script `docs/database_update.sql` và `docs/DuLieuMau.sql`: Đồng bộ 3 vai trò chuẩn trong `VaiTro`, 3 nhân viên trong `NhanVien`, 3 tài khoản demo (`quanly`, `thukho`, `nvkho` mật khẩu mặc định `123456`), và phân quyền kho trong `TaiKhoanKho`.
    6. **Kiểm tra chất lượng**:
       - Chạy `tools/KiemTraMaHoa.ps1`: **158/158 file đạt chuẩn UTF-8 with BOM, 0 lỗi Mojibake**.
       - Biên dịch MSBuild: **Build succeeded. 0 Warning(s), 0 Error(s)**.
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

- **Phiên 47 (2026-10-25) – Xây dựng Form Đăng nhập hệ thống, Tiện ích băm mật khẩu SHA-256 an toàn và Phân quyền thao tác theo 3 vai trò chuẩn**:
  - **Nhu cầu**: Người dùng yêu cầu:
    1. Kiểm tra 1 lượt classes và các file liên quan có thể có chứa hàm băm của mật khẩu. Nếu tồn tại, gọi hàm hash đó; nếu chưa, cho phép tạo class `PasswordHelper.cs`.
    2. Tạo form đăng nhập cho hệ thống, khi bắt đầu chạy chương trình yêu cầu đăng nhập với tài khoản và mật khẩu để phân quyền thao tác được phép.
    3. Hỗ trợ 3 vai trò người dùng chuẩn:
       - *Quản lý*: Quản lý mọi kho hàng trong hệ thống.
       - *Thủ kho*: Quản lý 1 kho duy nhất được phân công.
       - *Nhân viên kho*: Thao tác nghiệp vụ tại kho.
  - **Kết quả thực hiện**:
    1. **Kiểm tra và xây dựng tiện ích băm mật khẩu SHA-256 (`Classes/PasswordHelper.cs`)**:
       - Quét toàn bộ codebase xác nhận chưa có hàm băm mật khẩu.
       - Tạo mới `Classes/PasswordHelper.cs` cung cấp `HashPassword(string plainPassword)` (SHA-256 chữ thường) và `VerifyPassword(string plainPassword, string hashedPassword)`.
    2. **Xây dựng Form Đăng nhập (`frmDangNhap`) chuẩn WinForms**:
       - Kích thước form Size Nhỏ `ClientSize = (444, 321)`, `FixedDialog`, `CenterScreen`.
       - Gồm các control: Tiêu đề hệ thống, Tên đăng nhập (`txtTenDangNhap`), Mật khẩu (`txtMatKhau`), CheckBox Hiện mật khẩu (`chkHienMatKhau`), ComboBox Kho làm việc (`cboKhoLamViec`), nút Đăng nhập (`btnDangNhap`) và Thoát (`btnThoat`).
       - Tự động nhận diện vai trò tài khoản khi nhập tên đăng nhập để nạp danh sách kho tương ứng.
    3. **Phân quyền thao tác và phạm vi kho chuẩn hóa theo 3 vai trò**:
       - *Quản lý (`MaVaiTro = 1`)*: Toàn quyền truy cập tất cả các phân hệ và menu; quản lý mọi kho hàng; được chọn "Tất cả kho" hoặc kho chi nhánh bất kỳ khi đăng nhập và có quyền đổi kho (`frmDoiKho`).
       - *Thủ kho (`MaVaiTro = 2`)*: Quản lý 1 kho duy nhất được phân công (cố định ComboBox kho khi đăng nhập, ẩn/khóa menu "Đổi kho"); toàn quyền các nghiệp vụ kho của mình (Kiểm kho, Chuyển kho, Hàng hóa, Nhà cung cấp, Nhập hàng, Trả hàng nhập, Báo cáo tồn kho & Thẻ kho).
       - *Nhân viên kho (`MaVaiTro = 3`)*: Thao tác tại kho được phân công (ẩn menu "Đổi kho"); thực hiện Kiểm kho, Chuyển kho, Nhập hàng, Trả hàng nhập, xem Danh sách hàng hóa, xem Báo cáo tồn kho; ẩn các mục quản lý đối tác (Nhà cung cấp) và Thẻ kho sổ sách.
    4. **Cơ chế khởi động chương trình và Đăng xuất linh hoạt**:
       - Tại `Program.cs`: Bọc vòng lặp `while (true)` yêu cầu đăng nhập trước khi hiển thị `frmMain`.
       - Tại `frmMain.cs`: Khi người dùng nhấn "Đăng xuất" trên menu Hệ thống, hệ thống xóa sạch phiên làm việc `SessionContext.XoaPhien()`, đóng `frmMain` và quay lại màn hình `frmDangNhap` ngay lập tức.
    5. **Đồng bộ CSDL & Dữ liệu mẫu**:
       - Cập nhật script `docs/database_update.sql` và `docs/DuLieuMau.sql`: Đồng bộ 3 vai trò chuẩn trong `VaiTro`, 3 nhân viên trong `NhanVien`, 3 tài khoản demo (`quanly`, `thukho`, `nvkho` mật khẩu mặc định `123456`), và phân quyền kho trong `TaiKhoanKho`.
    6. **Kiểm tra chất lượng**:
       - Chạy `tools/KiemTraMaHoa.ps1`: **158/158 file đạt chuẩn UTF-8 with BOM, 0 lỗi Mojibake**.
       - Biên dịch MSBuild: **Build succeeded. 0 Warning(s), 0 Error(s)**.
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

