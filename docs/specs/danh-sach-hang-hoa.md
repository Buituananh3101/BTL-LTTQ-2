# Đặc tả Chức năng: Quản lý Hàng hóa (Product Catalog)

> **Mục tiêu**: **Khuôn mẫu chuẩn cho toàn bộ màn hình Danh mục (CRUD)** trong hệ thống. Lọc sidebar, phân trang AJAX, modal JSON, bảo toàn dữ liệu, nhập/xuất Excel chuẩn chuyên nghiệp.

---

## 1. Kiến trúc File & Bảng CSDL liên quan

### 1.1. Bản đồ File liên quan (File Map)
### 1.1. Bản đồ File liên quan (File Map)
- **Controller**: `Controllers/HangHoaController.cs` (nhận `HangHoaFilterParams`, gọi `IHangHoaExcelService`, xử lý CRUD AJAX).
- **Service & Helper**: `IHangHoaExcelService.cs`, `HangHoaExcelService.cs`, `ExcelHelper.cs`, `ITonKhoService.cs`, `TonKhoService.cs`.
- **ViewModel**: `ViewModels/HangHoa/` (`HangHoaForm.cs`, `HangHoaFilterParams.cs`, `ExportHangHoaRequest.cs`, `ImportHangHoaResult.cs`).
- **View & Partial**: `Views/HangHoa/Index.cshtml`, `_HangHoaFilterSidebar.cshtml`, `_HangHoaExcelActions.cshtml`, `_HangHoaTable.cshtml` (+ `_Pagination.cshtml`).
- **Modal Modun**: `_HangHoaModal.cshtml`, `_HangHoaExportModal.cshtml`, `_HangHoaImportModal.cshtml`.
- **Styles & Scripts**: `kiot-filter.js`, `hang-hoa-index.js`, `hang-hoa-form.js`, `hang-hoa-excel.js`.
- **DbContext & Entity**: `QuanLyKhoPhuQuocLtwContext`, Entity (`HangHoa`, `HinhAnhHangHoa`, `NhomHang`, `DonViTinh`, `NguongTonKho`, `VwTonKhoHienTai`).

### 1.2. Bảng CSDL tham gia:
- `HangHoa`: `MaHang`, `MaHangNoiBo`, `TenHang`, `MaNhom`, `MaDvt`, `QuyCach`, `GiaBanThamKhao`, `GiaVon`, `HinhAnh`, `TrangThai`, `NgayTao`, `DaDanhDau`, `ViTri`, `TrongLuong`, `DonViTrongLuong`, `ThuocTinh`, `MoTa`.
- `HinhAnhHangHoa`: `MaHinhAnh`, `MaHang`, `DuLieuAnh`, `ThuTu`, `LaAnhChinh`, `NgayTao` (tối đa 5 ảnh).
- `NhomHang`, `DonViTinh`, `NguongTonKho`: Phân loại, ĐVT và định mức tồn kho (`MaHang`, `MaKho`, `TonToiThieu`, `TonCaoNhat`).
- `vw_TonKhoHienTai`: View tính tồn kho động hiện thời theo `(MaKho, MaHang)`.

---

## 2. Bảng Luồng Tương tác Chi tiết (4 Bước)

| Tên Luồng | 1. Tương tác | 2. Frontend (View / JS) | 3. Backend (Controller) | 4. CSDL & Phản hồi |
|---|---|---|---|---|
| **Load ban đầu** | Vào `/HangHoa` | Gửi GET `/HangHoa` | `Index()` nạp danh mục, kho | Render `Index.cshtml` |
| **Tìm kiếm & Lọc** | Gõ tìm / Chọn lọc / Đánh dấu sao | JS debounce 300ms ➜ AJAX `GET /HangHoa/Filter` | `Filter(...)` query + `vw_TonKhoHienTai` | Render `_HangHoaTable.cshtml` (AJAX) |
| **Phân trang** | Click trang | JS gán `pageIndex` ➜ AJAX `GET /HangHoa/Filter` | `Filter(...)` dùng `.Skip().Take()` | Cập nhật bảng và phân trang |
| **Thêm mới hàng** | "+ Thêm mới", nhập form, "Lưu" | JS gom form + Base64 ➜ `POST /HangHoa/Create` | `Create(...)` validate, sinh mã hàng | Lưu CSDL + PN đầu kỳ ➜ JSON `{success:true}` |
| **Mở form Sửa** | Click Chỉnh sửa | JS AJAX `GET /HangHoa/GetItem?id=X` | `GetItem(id)` đọc hàng & tồn | Trả JSON ➜ điền modal sửa |
| **Lưu Sửa hàng** | Sửa form, "Cập nhật" | JS gom form ➜ AJAX `POST /HangHoa/Edit?id=X` | `Edit(id, form)` validate & cập nhật | Lưu CSDL ➜ JSON `{success:true}` |
| **Xóa hàng** | Click Xóa ➜ Xác nhận | Mở `#hhDeleteProductOv` ➜ AJAX `POST /HangHoa/Delete/{id}` | `Delete(id)` check 5 bảng GD, bọc Trans | Chưa GD: Xóa. Đã có: Báo chặn & gợi ý ngừng bán |
| **Xuất file Excel** | Bấm "Xuất file" ➜ Chọn cột ➜ Bấm Xuất | Mở `#hhExportModalOv` ➜ `POST /HangHoa/ExportExcel` | ClosedXML xuất đúng cột chọn | Trả file `.xlsx` |
| **Nhập file Excel** | Kéo thả file ➜ Thực hiện nhập | Mở `#hhImportModalOv` ➜ `POST /HangHoa/ImportExcel` | Validate, parse 16 trường, xử lý trùng mã | Commit Trans ➜ reload bảng |

---

## 3. Quy chuẩn Giao diện & Bộ lọc

### 3.1. Cột lọc bên trái (Sidebar Filter):
1. **Nhóm hàng**: ComboBox tìm kiếm tiếng Việt không dấu (`ComboboxHelper.CaiDatTimKiem`) + Nút `+` mở form popup quản lý nhóm hàng (`frmNhomHang`).
2. **Đơn vị tính**: ComboBox tìm kiếm tiếng Việt không dấu (`ComboboxHelper.CaiDatTimKiem`) + Nút `+` mở form popup quản lý đơn vị tính (`frmDonViTinh`) đồng bộ bố cục và logic theo `frmNhomHang` và `frmViTri`.
3. **Tồn kho**: ComboBox tùy chọn mức tồn (Tất cả / Còn hàng / Hết hàng / Dưới định mức tồn).
4. **Nhà cung cấp**: ComboBox tìm kiếm tiếng Việt không dấu theo NCC đã từng nhập hàng.
5. **Vị trí**: ComboBox tìm kiếm tiếng Việt không dấu + Nút `+` mở form popup quản lý vị trí (`frmViTri`).
6. **Trạng thái**: ComboBox chọn trạng thái kinh doanh (Tất cả / Đang kinh doanh / Ngừng kinh doanh).
7. **Thời gian tạo**: Cụm `grpThoiGianTao` chứa 2 `DateTimePicker` (Từ ngày - Đến ngày) **ở vị trí đáy** tuân thủ tuyệt đối Điều luật 16.
### 3.2. Cột Checkbox & Ẩn hiện cột:
- **Cột CheckBox lựa chọn (colChon)**: Cột cố định đầu tiên (35px) phục vụ chọn từng dòng hoặc chọn tất cả qua Header CheckBox. Click trực tiếp hoặc bấm phím Space để đảo trạng thái chọn tức thì mà không ảnh hưởng trạng thái chỉ đọc của các cột dữ liệu. Cung cấp API LayDanhSachMaHangDaChon() để phục vụ các tác vụ hàng loạt. Tự động loại trừ khi xuất file Excel/CSV.
- **Sắp xếp 3 trạng thái & Ẩn hiện cột**:
- **Sắp xếp 3 trạng thái**: Click header: Lần 1 ASC (`bi-arrow-up`) ➜ Lần 2 DESC (`bi-arrow-down`) ➜ Lần 3 Default. Cột căn trái dùng inline-flex chống tràn viền, cột số dùng padding-right 22px cố định icon.
- **Tùy chọn hiển thị cột**: Nút `#btnColsDropdown` (`bi-view-list`), menu `#colMenu` 2 cột chuẩn chuyên nghiệp. Quản lý qua `KiotFilter.initColumnToggle` lưu `kiot_hang_hoa_cols_v1`. Quản lý 13 cột dữ liệu (`img`, `mahang`, `tenhang`, `nhomhang`, `dvt`, `giaban`, `giavon`, `tonkho`, `khachdat`, `vitri`, `ngaytao`, `dukienhethang`, `trangthai`) + 2 cột cố định. Tự động đồng bộ `colspan` và class `.last-visible-col`. Bảng `#productTable` áp dụng chuẩn `width: max-content; min-width: 760px; table-layout: fixed;` (chuẩn Kiểm tồn & Nhà cung cấp), khóa cố định `width`/`min-width` từng cột chống co rút khi thêm cột. Khung chi tiết `.kiot-detail-fixed-wrap` cố định 992px.

---

## 4. Các Quy tắc Nghiệp vụ Bất biến

1. **BẢO TOÀN DỮ LIỆU & RÀNG BUỘC CHỨNG TỪ**: Kiểm tra cả 5 bảng: `ChiTietDonBan`, `ChiTietPhieuNhap`, `ChiTietPhieuXuat`, `ChiTietPhieuChuyenKho`, `ChiTietPhieuKiemKe`. Có giao dịch: chặn xóa, gợi ý chuyển `NGUNG_BAN`. Chưa có: xóa cascade ảnh và ngưỡng tồn trong Transaction.
2. **QUY CHUẨN XUẤT NHẬP EXCEL ĐỒNG BỘ 16 TRƯỜNG**:
   - 16 trường quản lý chuẩn: Loại hàng, Nhóm hàng, Mã hàng, Tên hàng (*), Giá bán, Giá vốn, Tồn kho, Tồn nhỏ nhất, Tồn lớn nhất, Đơn vị tính, Thuộc tính, Trọng lượng, Đơn vị TL, Vị trí, Mô tả, Trạng thái.
   - Khi xuất file, người bán được chọn cột muốn xuất. Có cảnh báo: Nếu bỏ chọn các cột thiết yếu (Tên hàng, Mã hàng, Loại hàng, ĐVT) thì file xuất ra KHÔNG THỂ làm file nhập ngược lại.
   - Khi nhập file, bắt buộc phải có cột "Tên hàng (*)". Xử lý trùng mã theo 3 chế độ: Cập nhật (ghi đè), Bỏ qua dòng trùng, Báo lỗi và dừng. Tự động sinh phiếu nhập `DAU_KY` nếu có tồn kho ban đầu.
3. **Mã nội bộ & Retry**: Mã `HH` + 6 số tự tăng (`HH000001`), Unique, retry chống Race Condition.
4. **Tồn kho theo kho làm việc**: Đọc động từ `vw_TonKhoHienTai` theo `CurrentWarehouseId`.
5. **Tối ưu RAM & Xử lý ảnh**: Không `SELECT HinhAnh` ở danh sách. Dùng LINQ Projection `HasImage`. Quá trình nén ảnh client tự động đổ nền trắng `#FFFFFF` trên Canvas nhằm triệt tiêu hoàn toàn lỗi ảnh trong suốt (PNG/WebP) bị chuyển thành nền đen khi nén JPEG; hỗ trợ Kéo-Thả (Drag & Drop) và Dán ảnh từ Clipboard (`Ctrl+V`); backend tự động nhận diện chính xác MIME type (`image/png`, `image/jpeg`, `image/webp`, `image/gif`).


---

## 5. Quy chuẩn Chức năng Nhập File Hàng Hóa (Desktop WinForms - frmNhapHangHoaFile)

### 5.1. Luồng thao tác giao diện:
1. Từ `frmDanhSachHangHoa`, người dùng bấm `btnNhapFile` ("Nhập file").
2. Hiển thị hộp thoại modal `frmNhapHangHoaFile` (`882 x 453`, CenterParent, FixedDialog).
3. Phần 1 - Chọn file: Người dùng bấm `btnChonFile` ("Chọn file...") để nạp file Excel (`.xlsx`, `.xls`).
4. Phần 2 - Tùy chọn trùng mã:
   - `rdoCapNhat` (Mặc định): Cập nhật thông tin nếu trùng mã hàng.
   - `rdoBoQua`: Bỏ qua không cập nhật nếu trùng mã hàng, chỉ thêm mới dòng chưa có mã.
5. Thanh điều khiển dưới đáy (Footer):
   - Bên trái: `btnTaiFileMauExcel` ("Tải file mẫu Excel") xuất file `.xls` mẫu chuẩn.
   - Bên phải: `btnThucHien` ("Thực hiện nhập") và `btnDong` ("Đóng").
6. Bấm `btnThucHien`: Kích hoạt thanh tiến trình `prgTienTrinh`, đổi con trỏ chuột sang chờ (`WaitCursor`), kiểm tra tính hợp lệ và bọc toàn bộ trong `DataProcessing.ChangeDataTransaction`. Sau khi xong, hiển thị hộp thoại báo cáo tổng kết chi tiết và kích hoạt làm mới danh sách hàng hóa trên form chính.

### 5.2. Quy tắc nghiệp vụ dữ liệu:
- **Tên hàng hóa (Bắt buộc duy nhất)**: Bắt buộc phải có giá trị. Dòng thiếu tên hàng hóa sẽ bị ghi nhận lỗi/bỏ qua.
- **Mã hàng (`MaHangNoiBo`)**:
  - Nếu bỏ trống: Hệ thống tự động sinh mã tăng dần tiếp theo (`HH000001`, `HH000002`...).
  - Nếu có sẵn: Kiểm tra trùng mã trong CSDL để quyết định Ghi đè hoặc Bỏ qua theo tùy chọn người dùng.
- **Tồn kho (BẤT BIẾN)**:
  - TUYỆT ĐỐI KHÔNG ghi đè tồn kho qua nhập file danh mục. Tồn kho thực tế tính động qua `vw_TonKhoHienTai`, biến động kho bắt buộc thông qua chứng từ Nhập kho hoặc Kiểm kê.
- **Nhóm hàng (`NhomHang`)**:
  - Bắt buộc trong CSDL (`MaNhom NOT NULL`).
  - Khi thêm mới: Nếu có trong file thì liên kết; nếu tên nhóm chưa tồn tại thì tự động tạo mới vào bảng `NhomHang`; nếu để trống thì tự động gán vào nhóm mặc định **"Khác"** (tự tạo nếu chưa có).
  - Khi cập nhật ghi đè: Nếu để trống cột nhóm hàng trong file thì **giữ nguyên nhóm hàng cũ** của sản phẩm.
- **Đơn vị tính (`DonViTinh`)**:
  - Bắt buộc trong CSDL (`MaDVT NOT NULL`).
  - Khi thêm mới: Nếu có trong file thì liên kết; nếu chưa có thì tự động tạo mới vào bảng `DonViTinh`; nếu để trống thì tự động gán ĐVT mặc định **"Cái"** (tự tạo nếu chưa có).
  - Khi cập nhật ghi đè: Nếu để trống thì **giữ nguyên ĐVT cũ**.
- **Vị trí (`ViTri`)**:
  - Cho phép `NULL`.
  - Khi thêm mới: Nếu có tên vị trí chưa tồn tại thì tự động thêm vào bảng `ViTri` và lưu tên vị trí; nếu để trống thì lưu `NULL`.
  - Khi cập nhật ghi đè: Nếu để trống thì **giữ nguyên vị trí cũ**.
- **Giá vốn, Giá bán, Trọng lượng**:
  - Tự động parse số thập phân (loại bỏ dấu phẩy/chấm phân cách nghìn).
  - Khi thêm mới: Để trống hoặc sai số mặc định về `0`.
  - Khi cập nhật ghi đè: Để trống giữ nguyên giá trị cũ.
- **An toàn giao dịch**: 100% bọc trong `SqlTransaction` và tham số hóa câu lệnh (`SqlParameter`).

---

## 6. Nghiệp vụ Cột "Khách đặt" (Reserved / On-order)
- **Định nghĩa nghiệp vụ**: Cột "Khách đặt" phản ánh tổng số lượng hàng hóa nằm trong các đơn đặt hàng bán của khách (`DonBan` & `ChiTietDonBan`) tại kho đang làm việc (`MaKho = @MaKho`) chưa được giao/xuất kho hoàn tất và chưa bị hủy (`TrangThai NOT IN ('HOAN_TAT', 'HUY')`).
- **Ý nghĩa quản trị**: Giúp nhân viên kho và bộ phận bán hàng biết được lượng hàng đã được giữ chỗ/đặt trước cho khách, từ đó xác định lượng tồn khả dụng thực tế (`Tồn khả dụng = Tồn kho - Khách đặt`).
- **Quy chuẩn hiển thị & Tương tác**:
  - Cột: `colKhachDat`, HeaderText: `Khách đặt`, DataPropertyName: `KhachDat`.
  - Vị trí: Đứng ngay sau cột Tồn kho (`colTonKho`), độ rộng 95px, căn phải (`MiddleRight`), định dạng số `N0`.
  - Mặc định ẩn (`Visible = false`) khi tải danh sách; người dùng bật/tắt hiển thị linh hoạt thông qua form "Hiện cột" (`frmHienCot`, checkbox `chkKhachDat`).
  - Khi xuất file Excel (`.xls`) hoặc `.csv`: Nếu cột đang hiển thị, hệ thống tự động xuất kèm dữ liệu số lượng khách đặt với định dạng số chuẩn.
