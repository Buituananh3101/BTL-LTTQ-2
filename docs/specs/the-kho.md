# Đặc tả Chức năng: Báo cáo Thẻ kho (`frmBaoCaoTheKho`)

> **Mã chức năng**: `BaoCao_TheKho`  
> **Form C#**: `GUI/Forms/BaoCao/frmBaoCaoTheKho.cs`  
> **Menu**: Desktop MDI: `Báo cáo` ➜ `Báo cáo thẻ kho` (`mnuBaoCaoTheKho`)  
> **Người dùng**: Quản lý (xem toàn bộ kho hoặc từng kho), Thủ kho / Nhân viên bán hàng (xem các kho được phân quyền)

---

## 1. Nguyên lý Nghiệp vụ & Công thức Tính toán

### 1.1. Nguồn dữ liệu biến động (`vw_BienDongKho`)
Số liệu Thẻ kho được truy vấn từ View CSDL **`vw_BienDongKho`** (hợp nhất từ các chứng từ đã hoàn thành/duyệt):
- `PHIEU_NHAP` (`TrangThai = 'DA_XAC_NHAN'`): Nhập hàng tăng kho (`SoLuongTang`).
- `XUAT_BAN`, `XUAT_NOI_BO` (`TrangThai = 'DA_XAC_NHAN'`): Xuất kho giảm tồn (`SoLuongGiam`).
- `CHUYEN_XUAT` (`TrangThai IN ('DA_XUAT', 'DA_NHAN')`): Chuyển đi giảm kho nguồn (`SoLuongGiam`).
- `CHUYEN_NHAP` (`TrangThai = 'DA_NHAN'`): Chuyển đến tăng kho đích (`SoLuongTang`).
- `KIEM_KE` (`PhieuDieuChinh.TrangThai = 'DA_DUYET'`): Cân bằng tăng/giảm tồn kho.
- `TRA_HANG_NHAP` (`TrangThai = 'DA_TRA_HANG'`): Trả NCC giảm kho (`SoLuongGiam`).

### 1.2. Công thức tính toán cốt lõi
1. **Tồn đầu kỳ ($T_{đầu}$)**:
   $$\text{Tồn đầu kỳ} = \sum (\text{SoLuongTang} - \text{SoLuongGiam}) \quad \text{với } \text{NgayGiaoDich} < \text{TuNgay}$$
2. **Giao dịch trong kỳ**:
   Truy vấn các bản ghi có $\text{TuNgay} \le \text{NgayGiaoDich} \le \text{DenNgay}$, sắp xếp theo `NgayGiaoDich ASC, MaChungTu ASC`.
3. **Tồn lũy kế dòng $i$ ($T_i$)**:
   $$T_i = T_{i-1} + \text{SoLuongTang}_i - \text{SoLuongGiam}_i$$
4. **Tồn cuối kỳ ($T_{cuối}$)**:
   $$T_{cuối} = T_{đầu} + \sum \text{NhapKy} - \sum \text{XuatKy}$$

---

## 2. Kiến trúc Form & Giao diện WinForms

- **Tỉ lệ & Kích thước form**: Kế thừa chuẩn `frmDanhSachHangHoa` (`ClientSize = (1782, 853)`, `FixedSingle`, `MaximizeBox = false`, `CenterScreen`).
- **Khởi tạo linh hoạt & Điều hướng 2 chiều**:
  - Hỗ trợ constructor rỗng `frmBaoCaoTheKho()` mở từ MenuStrip (nút điều hướng hiển thị nhãn "Đóng").
  - Hỗ trợ constructor có tham số `frmBaoCaoTheKho(int maHang, int maKho = 0, bool moTuTonKho = false)` và phương thức `ChonMatHangVaKho(maHang, maKho, moTuTonKho = false)` mở trực tiếp từ Báo cáo tồn kho.
  - Khi `moTuTonKho = true`: Nút điều hướng hiển thị nhãn "Quay lại" (`btnQuayLai`). Khi bấm `Quay lại`, nhấn phím `Escape` hoặc đóng form (dấu [X]), hệ thống tự động đóng Thẻ kho và kích hoạt lại `frmBaoCaoTonKho` (`Activate()`) đưa người dùng về ngay dòng hàng và trang đang xem dở.
- **Phân vùng giao diện**:
  - `pnlTop` (Dock = Top, Cao 74px):
    - `grpTop`: Tiêu đề "Báo cáo thẻ kho", ô tìm kiếm nhanh chứng từ/đối tác (`lblTimKiem` tại (385, 25) và `txtTimKiem` tại (465, 21)), các nút chức năng bên phải: `btnXuatFile`, `btnHienCot`, `btnHuongDanSuDung` (`?`).
  - `panel1` (Dock = Top, Cao 133px, đặt trên `dgvTheKho`):
    - Chứa `groupBox1` gồm:
      - `pnlThongTinHang` (23, 24, Size 709x72): Hiển thị tên hàng, mã SKU, Kho, ĐVT, Nhóm hàng, Vị trí, Giá vốn.
      - 4 thẻ KPI (Cursor = Hand, hỗ trợ nhấp để lọc/cuộn nhanh):
        + `pnlCardTonDau` (754, 24, 100x72): Tồn đầu kỳ (nhấp để reset lọc và cuộn lên dòng đầu).
        + `pnlCardNhapKy` (876, 24, 173x72): Tổng nhập trong kỳ (+) (nhấp để lọc phiếu nhập mua).
        + `pnlCardXuatKy` (1071, 24, 173x72): Tổng xuất trong kỳ (-) (nhấp để lọc phiếu xuất bán).
        + `pnlCardTonCuoi` (1266, 24, 100x72): Tồn cuối kỳ (nhấp để reset lọc và cuộn xuống dòng cuối).
  - `pnlBoLoc` (Dock = Left, Rộng 387px chuẩn `frmDanhSachHangHoa`):
    - `grpBoLoc` (Location (12, 6), Size (360, 717)):
      - `cboHangHoa` (18, 58): ComboBox chọn mặt hàng (tích hợp `ComboboxHelper.CaiDatTimKiem`).
      - `cboLoaiChungTu` (18, 198): ComboBox lọc loại chứng từ phát sinh.
      - `lblThoiGianTao` (18, 565): Nhãn "Thời gian phát sinh".
      - `grpThoiGianTao` (18, 593): Cụm 2 DateTimePicker Short (`dtpTuNgay`, `dtpDenNgay`) đặt tại đáy vùng bộ lọc theo Điều luật 16.
    - `btnQuayLai` (Location (12, 735), Size (95, 29)): Đặt ở đáy vùng bộ lọc theo Điều luật 19b.
  - `dgvTheKho` (Dock = Fill):
    - Bảng hiển thị danh sách dòng thẻ kho: Dòng Tồn đầu kỳ (highlight màu nền nhẹ, font chữ thường), các dòng phát sinh chứng từ (STT, Thời gian, Mã chứng từ, Loại giao dịch, Đối tác / Diễn giải, Chi nhánh kho, SL Nhập, SL Xuất, Tồn lũy kế, Ghi chú), dòng Tồn cuối kỳ (highlight màu nền nhẹ, font chữ thường). Toàn bộ thuộc tính hiển thị font chữ thường đồng bộ theo mẫu Trả hàng nhập (không in đậm tùy tiện).
    - Tồn lũy kế âm được tô đỏ đậm (Điều luật 15a).
    - Loại giao dịch nhập/tăng tô xanh lá (font chữ thường), xuất/giảm màu slate (font chữ thường).
    - Cột `colTenKho` tự động hiển thị khi lọc "Toàn hệ thống" và tự động ẩn khi theo dõi kho cụ thể.
  - `pnlPhanTrang` (Dock = Bottom, Cao 42px): Thống kê tổng số giao dịch phát sinh trong kỳ.

---

## 3. Quy tắc Nghiệp vụ Bất biến

1. **Tính toán động**: Số liệu tồn đầu kỳ và tồn lũy kế luôn tính động từ `vw_BienDongKho`, không lưu tồn tĩnh.
2. **Bảo toàn dữ liệu**: Chứng từ trạng thái `'HUY'` tự động bị loại trừ khỏi Thẻ kho, số dư tự động hoàn lại.
3. **Phân quyền chặt chẽ**: Nhân viên chỉ được xem thẻ kho của kho phụ trách; Quản lý có quyền xem liên kho.
4. **An toàn dữ liệu**: 100% câu truy vấn sử dụng `SqlParameter`, file code và file xuất dữ liệu lưu định dạng UTF-8 with BOM.
