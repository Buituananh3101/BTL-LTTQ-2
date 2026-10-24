# Đặc tả Chức năng: Báo cáo Tồn kho (`frmBaoCaoTonKho`)

> **Mã chức năng**: `BaoCao_TonKho`  
> **Form C#**: `GUI/Forms/BaoCao/frmBaoCaoTonKho.cs`  
> **Menu**: Desktop MDI: `Báo cáo` ➜ `Báo cáo tồn kho` (`mnuBaoCaoTonKho`)  
> **Người dùng**: Quản lý (xem toàn bộ kho hoặc từng kho), Thủ kho / Nhân viên (xem các kho được phân quyền trong `TaiKhoanKho` / `SessionContext`)

---

## 1. Mục tiêu & Phạm vi nghiệp vụ

- **Mục tiêu**: Cung cấp bức tranh toàn diện và tức thời về lượng tồn kho, giá trị hàng hóa lưu kho theo từng chi nhánh kho và toàn hệ thống dưới dạng bảng danh sách phân tích tồn chuẩn WinForms chuyên nghiệp.
- **Tính năng chính**:
  1. **Thống kê 4 KPI tổng quan**:
     - Tổng số mặt hàng (SKU).
     - Tổng số lượng tồn kho.
     - Tổng giá trị tồn kho (Giá vốn × Tồn kho).
     - Cần lưu ý (Số mặt hàng hết hàng $\le 0$ hoặc dưới định mức tồn tối thiểu).
  2. **Bảng danh mục tồn kho**:
     - Cột dữ liệu: Mã hàng, Tên hàng hóa, ĐVT, Nhóm hàng, Chi nhánh kho, Giá vốn, Giá bán, Tồn kho, Giá trị tồn, Tồn tối thiểu, Tồn tối đa, Vị trí, Trạng thái kinh doanh.
     - Tồn kho $\le 0$ được tự động bôi đỏ nổi bật (Điều luật 15a).
     - Trạng thái kinh doanh hiển thị theo 3 nhóm màu chuẩn (Đang kinh doanh: Xanh lá, Ngừng kinh doanh: Xám nhạt - Điều luật 15b).
  3. **Lọc đa chiều bên trái** (100% ComboBox, thời gian ở đáy theo Điều luật 16):
     - Chi nhánh kho (`cboKho`): Xem từng kho cụ thể hoặc `-- Tất cả các kho --` (tổng hợp toàn hệ thống).
     - Nhóm hàng (`cboNhomHang`): Tích hợp tìm kiếm nhanh qua `ComboboxHelper.CaiDatTimKiem`.
     - Tiêu chí mức tồn (`cboTieuChiTon`): Tất cả, Còn tồn (>0), Hết hàng (=0), Tồn âm (<0), Dưới định mức tối thiểu, Vượt định mức tối đa.
     - Trạng thái kinh doanh (`cboTrangThai`): Tất cả, Đang kinh doanh, Ngừng kinh doanh.
     - Cụm thời gian tạo (`grpThoiGianTao`): 2 `DateTimePicker` (`dtpTuNgay`, `dtpDenNgay` Short) đặt ở vị trí đáy của bộ lọc.$14. **Xem nhanh thẻ kho qua thao tác nhấn đúp**:
      - Nhấp đúp vào một dòng hàng hóa trên lưới sẽ tự động kích hoạt form **Báo cáo thẻ kho** (`frmBaoCaoTheKho`) của mặt hàng và chi nhánh kho đó (hỗ trợ nút `Quay lại` để trở về danh sách tồn kho mà không mất trạng thái phân trang/bộ lọc).
$15. **Xuất file**:
     - Xuất dữ liệu danh sách tồn kho ra file CSV chuẩn UTF-8 with BOM mở trực tiếp bằng Excel không lỗi font tiếng Việt.$16. **Tùy chọn hiển thị cột**:
     - Hỗ trợ popup `frmHienCotBaoCao` cho phép bật/tắt hiển thị từng cột dữ liệu trên lưới.

---

## 2. Nguồn CSDL & Công thức tính toán

- **Nguồn tồn kho**: View SQL `vw_TonKhoHienTai` (`MaKho`, `MaHang`, `SoLuongTon`) - tuân thủ Điều luật 4 AGENTS.md.
- **Bảng CSDL liên quan**:
  - `HangHoa`: `MaHang`, `MaHangNoiBo`, `TenHang`, `MaDVT`, `MaNhom`, `GiaVon`, `GiaBanThamKhao`, `TrangThai`, `ViTri`, `NgayTao`.
  - `Kho`: `MaKho`, `TenKho`, `TrangThai`.
  - `NhomHang`: `MaNhom`, `TenNhom`.
  - `DonViTinh`: `MaDVT`, `TenDVT`.
  - `NguongTonKho`: `MaKho`, `MaHang`, `TonToiThieu`, `TonCaoNhat` (dùng xác định định mức tồn tối thiểu và tối đa).
  - `TaiKhoanKho`: Xác định danh sách kho mà tài khoản được quyền truy cập.

---

## 3. Kiến trúc Form & Giao diện WinForms

- **Tỉ lệ & Kích thước form**: Kế thừa chuẩn `frmDanhSachHangHoa` (`ClientSize = (1782, 853)`, `FixedSingle`, `MaximizeBox = false`, `CenterScreen`).
- **Phân vùng giao diện**:
  - `pnlTop` (Cao 125px): Gồm `grpTop` (Tiêu đề, ô tìm kiếm nhanh, nút `Xuất file`, `Hiện cột`, `?`) và `pnlKpi` (4 thẻ chỉ số tổng quan).
  - `pnlBoLoc` (Rộng 360px bên trái): Gồm các ComboBox tiêu chí và cụm thời gian ở đáy.
  - `dgvBaoCaoTonKho` (Dock = Fill): DataGridView đệm đôi, xen kẽ màu, căn lề chuẩn số lượng/tiền tệ `N0`.
  - `pnlPhanTrang` (Đáy 42px): Thống kê tổng số hàng, chọn số dòng mỗi trang (10, 20, 50, 100), điều hướng trang trước/sau.

---

## 4. Quy tắc nghiệp vụ bất biến

1. **Tính toán động**: Số liệu tồn kho luôn tính động từ `vw_TonKhoHienTai`, không lưu tồn tĩnh.
2. **Chống SQL Injection**: Mọi truy vấn lọc và tìm kiếm đều tham số hóa 100% bằng `SqlParameter`.
3. **Mã hóa file**: Lưu trữ UTF-8 with BOM.
