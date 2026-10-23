# Tiến độ dự án BTL-LTTQ-2

**Cập nhật lần cuối**: 2026-10-23 – Hoàn thiện Cột Khách đặt, Nhập file Excel, Đấu nối form Hiện cột & Tối ưu giao diện Danh sách Hàng hóa (Phiên 6)

---

## Đang làm
- [x] Hoàn thiện Form Danh sách Hàng hóa (`frmDanhSachHangHoa`): Kết nối dữ liệu thật từ SQL Server, phân trang, bộ lọc đa tiêu chí, tính tốc độ bán dự kiến hết hàng, tính động số lượng "Khách đặt" từ đơn đặt hàng bán, ẩn/hiện cột linh hoạt qua `frmHienCot`, nhập/xuất Excel.
- [x] Hoàn thiện Form Thêm/Sửa Hàng hóa (`frmHangHoa`): Quản lý đầy đủ thông tin sản phẩm, định mức tồn, vị trí và lưu trữ 5 ảnh đại diện.
- [ ] Tiếp tục phân hệ Danh mục Cơ bản (Nhóm hàng, Đơn vị tính, Vị trí kho, Nhà cung cấp) hoặc Xác thực & Phân quyền (`frmDangNhap`).

---

## Việc kế tiếp
1. **Quản lý Danh mục (CRUD Master Data)**:
   - Form Danh mục Nhóm hàng (`frmNhomHang`) & Đơn vị tính (`frmDonViTinh`).
   - Form Danh mục Vị trí (`frmViTri`).
   - Form Danh mục Chi nhánh Kho & phân quyền `TaiKhoanKho`.
   - Form Danh mục Nhà cung cấp (`frmNhaCungCap`).
2. **Xác thực & Phân quyền**: Hoàn tất `frmDangNhap`, SHA-256 `PasswordHelper`, tích hợp phân quyền MenuStrip và StatusStrip trên `frmMain`.
3. **Quản lý Kho & Chứng từ Biến động**:
   - Form Nhập hàng từ Nhà cung cấp (`frmNhapHang`) - Tự tính giá vốn MAC, cộng tồn.
   - Form Kiểm kê & Cân bằng kho (`frmKiemKho`) - Khóa vĩnh viễn phiếu sau khi cân bằng.
   - Form Chuyển kho liên chi nhánh (`frmChuyenKho`) - Luồng 2 bước Xuất chuyển / Nhận hàng.
   - Form Trả hàng nhập (`frmTraHangNhap`) & Xuất hủy (`frmXuatHuy`).
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
| 6 | **Danh mục Cơ bản** | CRUD Nhóm hàng, Đơn vị tính, Chi nhánh Kho, Nhà cung cấp | Chưa bắt đầu |
| 7 | **Phiếu Nhập hàng** | `frmNhapHang`, tính giá vốn bình quân (MAC), cộng tồn dynamic | Chưa bắt đầu |
| 8 | **Kiểm kê & Cân bằng kho** | `frmKiemKho`, nạp tồn sổ sách, sinh phiếu điều chỉnh, khóa vĩnh viễn | Chưa bắt đầu |
| 9 | **Chuyển kho Liên chi nhánh** | `frmChuyenKho`, 2 bước Xuất kho ➜ Nhận kho, chặn tồn âm kho nguồn | Chưa bắt đầu |
| 10 | **Trả hàng nhập & Xuất hủy** | `frmTraHangNhap`, `frmXuatHuy`, kiểm tra tồn kho trước khi xuất | Chưa bắt đầu |
| 11 | **Báo cáo Tồn kho & Thẻ kho** | Báo cáo tồn đa kho, Thẻ kho từ `vw_BienDongKho`, xuất file Excel | Chưa bắt đầu |

---

## Nhật ký phiên (Session Logs)

- **Phiên 6 (2026-10-23) – Triển khai Nghiệp vụ Cột "Khách đặt" & Tối ưu Giao diện Thanh Công Cụ**:
  - **Nhu cầu**: Hiện thực hóa logic tính toán và hiển thị cho cột "Khách đặt" trên `frmDanhSachHangHoa` và đấu nối form tùy chọn cột `frmHienCot`; khắc phục lỗi tràn ô tìm kiếm `txtTimKiem` che khuất các nút hành động trên màn hình có độ phân giải phổ thông.
  - **Kết quả thực hiện**:
    1. **Bổ sung cột `colKhachDat` vào DataGridView**: Thêm cột "Khách đặt" ngay sau cột Tồn kho (`colTonKho`), độ rộng 95px, căn phải, định dạng số nguyên `N0`, mặc định ẩn (`Visible = false`).
    2. **Logic tính toán số lượng khách đặt động (Tuân thủ Luật 4)**: Tính tổng số lượng từ các đơn bán mở (`DonBan` & `ChiTietDonBan`) theo kho đang làm việc (`db.MaKho = @MaKho`) chưa xuất hàng xong và chưa hủy (`db.TrangThai NOT IN ('HOAN_TAT', 'HUY')`).
    3. **Đấu nối `chkKhachDat` trên `frmHienCot`**: Đồng bộ trạng thái checkbox với trạng thái hiển thị của `colKhachDat`, cho phép người dùng bật/tắt cột linh hoạt tức thì.
    4. **Tự động xuất dữ liệu Excel**: Xuất chuẩn định dạng số lượng khách đặt khi người dùng xuất danh sách ra `.xls` hoặc `.csv` lúc cột đang hiển thị.
    5. **Tối ưu thanh công cụ `grpTop`**: Điều chỉnh kích thước `txtTimKiem` (từ 540 xuống 320px) giúp bố cục cân đối, loại bỏ triệt để hiện tượng che khuất nút `+ Hàng hóa` và `Nhập file`.
    6. **Biên dịch & Kiểm thử UI**: Build thành công với **0 Warning(s), 0 Error(s)**, kiểm thử UI tự động xác nhận số liệu thực tế chính xác (sản phẩm `Bếp gas đơn Rinnai` hiển thị Tồn: 26, Khách đặt: 1).

- **Phiên 5 (2026-10-23) – Triển khai Chức năng Nhập File Hàng Hóa Chuẩn KiotViet (`frmNhapHangHoaFile`)**:
  - **Nhu cầu**: Thiết kế và hiện thực hóa trọn vẹn chức năng "Nhập file" (`btnNhapFile`) trên `frmDanhSachHangHoa`, bao gồm tải file mẫu, chọn file dữ liệu, tùy chọn xử lý trùng mã (Cập nhật / Bỏ qua), tự động tạo nhóm hàng/ĐVT/vị trí mới nếu chưa có, tự sinh mã hàng nếu để trống, bọc trong Transaction và kiểm soát an toàn tồn kho theo Luật 4.
  - **Kết quả thực hiện**:
    1. Cập nhật đặc tả chi tiết tại `docs/specs/danh-sach-hang-hoa.md` mục 5.
    2. Thiết kế hộp thoại Modal [`frmNhapHangHoaFile`](file:///d:/CODE/BTL-LTTQ-2/BTL-LTTQ-2/GUI/Forms/DanhSachHangHoa/frmNhapHangHoaFile.cs) giao diện chuẩn WinForms (`664 x 475`, Segoe UI 9.5F, nút Primary Blue `#0078D7`).
    3. Hỗ trợ tải file mẫu Excel (.xls) định dạng HTML Table UTF-8 sắc nét và CSV (.csv) UTF-8 có BOM.
    4. Bộ đọc file đa định dạng: Hỗ trợ Excel OpenXML (.xlsx) qua `System.IO.Compression` chuẩn không cần thư viện ngoài, Excel HTML/OleDb (.xls), và CSV/TXT với cơ chế tách trường và bỏ dấu FormD thông minh.
    5. Xử lý nghiệp vụ CSDL: Bọc trong `DataProcessing.ChangeDataTransaction` + `SqlParameter`; tự sinh mã tăng dần `HHxxxxxx`; tự động tạo Nhóm hàng / ĐVT / Vị trí nếu là tên mới; khi cập nhật nếu để trống thì giữ nguyên giá trị cũ; tuyệt đối không đụng đến tồn kho sổ sách.
    6. Đấu nối sự kiện `btnNhapFile.Click` trên `frmDanhSachHangHoa.cs` mở modal và tự động tải lại bộ lọc + danh sách sau khi nhập.
    7. Biên dịch dự án: **0 Warning(s), 0 Error(s)**.


- **Phiên 4 (2026-10-22) – Hoàn thiện Form Ẩn Hiện Cột (`frmHienCot`), Dự Báo Hết Hàng, Xuất File Excel Chuẩn & Lưu Trọn Vẹn 5 Ảnh**:
  - **Nhu cầu**: Thiết kế form tùy chọn ẩn/hiện cột theo mẫu ảnh người dùng; định vị ngay dưới nút; kích hoạt tính toán cột "Dự kiến hết hàng" và "Thời gian tạo"; sửa lỗi xuất file bị dồn 1 cột; lưu toàn bộ 5 ảnh hàng hóa vào CSDL.
  - **Kết quả thực hiện**:
    1. Hoàn thiện [`frmHienCot`](file:///d:/CODE/BTL-LTTQ-2/BTL-LTTQ-2/GUI/Forms/DanhSachHangHoa/frmHienCot.cs): Giao diện 13 CheckBox xếp 2 cột đúng 100% nhãn/text theo ảnh. Mở chuẩn vị trí ngay dưới nút `btnHienCot`, chống tràn màn hình.
    2. Cấu hình mặc định khi load: 5 cột *Khách đặt, Vị trí, Thời gian tạo, Dự kiến hết, Trạng thái* mặc định ẩn (`Visible = false`) và bỏ chọn CheckBox.
    3. Triển khai cột **"Dự kiến hết"** (Phương án 1): Tính động tốc độ bán 30 ngày từ `vw_BienDongKho`, chia cho tồn kho ra số ngày còn lại (`X ngày`, `Hết hàng`, `> 90 ngày`), hiển thị chữ thường đồng bộ.
    4. Bổ sung cột **"Thời gian tạo"** (`colThoiGianTao`) chuẩn `dd/MM/yyyy HH:mm`.
    5. Nâng cấp chức năng **"Xuất file"**: Hỗ trợ xuất File Excel (`.xls` HTML Table UTF-8) tự động chia từng cột, kẻ viền, căn lề và định dạng số, triệt tiêu hoàn toàn lỗi bị dồn vào cột A.
    6. Nâng cấp xử lý **5 ảnh hàng hóa** trên [`frmHangHoa`](file:///d:/CODE/BTL-LTTQ-2/BTL-LTTQ-2/GUI/Forms/DanhSachHangHoa/frmHangHoa.cs): Đồng bộ lưu toàn bộ 5 slot ảnh vào bảng CSDL `HinhAnhHangHoa` (`MaHang, DuLieuAnh, ThuTu, LaAnhChinh`) bọc trong Transaction; tự động nạp đầy đủ cả 5 ảnh khi mở sửa.
    7. Biên dịch dự án: **0 Warning(s), 0 Error(s)**.

- **Phiên 3 (2026-10-22) – Hoàn thiện Toàn bộ Logic Danh mục Hàng hóa (`frmDanhSachHangHoa` & `frmHangHoa`)**:
  - **Nhu cầu**: Hiện thực hóa logic nghiệp vụ nạp dữ liệu, tìm kiếm tức thì, lọc đa chiều, phân trang SQL Server và thêm/sửa hàng hóa bám sát 100% giao diện đã thiết kế.
  - **Kết quả thực hiện**:
    1. Tạo [`Classes/SessionContext.cs`](file:///d:/CODE/BTL-LTTQ-2/BTL-LTTQ-2/Classes/SessionContext.cs): Quản trị phiên kho hiện tại (`MaKhoHienTai = 1`), phục vụ tính tồn kho động chính xác qua `vw_TonKhoHienTai`.
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
