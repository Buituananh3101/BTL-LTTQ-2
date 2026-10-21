# Đặc tả Chức năng: Báo cáo Thẻ kho (Stock Card Report)

> **Mục tiêu**: Cung cấp cho Thủ kho và Quản lý báo cáo theo dõi chi tiết lịch sử xuất - nhập - tồn của từng mặt hàng tại một chi nhánh kho hoặc toàn hệ thống theo trình tự thời gian chuẩn chuyên nghiệp. Tái sử dụng 100% các khối LEGO: `KiotFilter`, `ExcelHelper`, `_KiotDateFilter`, `_KiotStatusFilter`, `_Pagination`.

---

## 1. Nguyên lý Nghiệp vụ & Công thức Tính toán

### 1.1. Nguồn dữ liệu biến động (`vw_BienDongKho`)
Số liệu Thẻ kho được truy vấn từ View CSDL **`vw_BienDongKho`** (hợp nhất từ các chứng từ đã hoàn thành/duyệt):
- `PHIEU_NHAP` (`TrangThai = 'DA_XAC_NHAN'`): Nhập hàng tăng kho (`SoLuongTang`).
- `XUAT_BAN`, `XUAT_HUY`, `XUAT_KHAC` (`TrangThai = 'DA_XAC_NHAN'`): Xuất kho giảm tồn (`SoLuongGiam`).
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
   *(Khớp chính xác 100% với `vw_TonKhoHienTai` khi ngày kết thúc $\ge$ Hiện tại).*

---

## 2. Kiến trúc CSDL & ViewModels (`ViewModels/BaoCao/`)

### 2.1. ViewModels
- **`TheKhoFilterParams`**: Kế thừa `BaseFilterParams`. Chứa `MaHang`, `MaKho`, `DateRange`, `TuNgay`, `DenNgay`, `LoaiChungTu` (`List<string>`), `TuKhoa`, `Page`, `PageSize`.
- **`TheKhoDongVM`**: Chi tiết dòng thẻ kho (`Stt`, `NgayGiaoDich`, `MaChungTu`, `MaChungTuHienThi`, `LoaiChungTu`, `TenLoaiChungTu`, `BadgeClass`, `DoiTac`, `GhiChu`, `DonGia`, `SoLuongTang`, `SoLuongGiam`, `TonLuyKe`, `UrlChiTiet`).
- **`TheKhoTongHopVM`**: Chỉ số KPI & thông tin hàng hóa (`MaHang`, `MaHangNoiBo`, `TenHang`, `TenDvt`, `TenNhomHang`, `ViTri`, `GiaVon`, `GiaBan`, `MaKho`, `TenKho`, `TuNgay`, `DenNgay`, `TonDauKy`, `TongNhapKy`, `TongXuatKy`, `TonCuoiKy`, `TongSoGiaoDich`).
- **`TheKhoIndexVM`**: Đóng gói `Filter`, `TongHop`, `DanhSachDong` (`PagedResult<TheKhoDongVM>`), `DanhSachKho`, `DanhSachNhomHang`.

---

## 3. Bản đồ Tệp tin (File Map)

| Tệp tin | Trách nhiệm |
|---|---|
| `ViewModels/BaoCao/TheKho*.cs` | Quản lý tham số lọc, DTO dòng thẻ kho, KPI tổng hợp và Index VM |
| `Services/ITheKhoService.cs` | Interface dịch vụ nghiệp vụ Thẻ kho |
| `Services/TheKhoService.cs` | Tính toán Tồn đầu, Tồn lũy kế, nạp chi tiết đối tác, xuất Excel ClosedXML |
| `Controllers/BaoCaoController.cs` | Kế thừa `BaseKhoController`, cung cấp action `TheKho`, `FilterTheKho`, `ExportTheKho`, `GetTheKhoQuick`, `SearchHangHoa` |
| `Views/BaoCao/TheKho.cshtml` | View chính 2 cột hệ thống, Toolbar, Container AJAX đồng bộ |
| `Views/BaoCao/_TheKhoFilterSidebar.cshtml` | Sidebar Lego: Autocomplete F3 chọn hàng, Dropdown kho, `_KiotDateFilter`, `_KiotStatusFilter` |
| `Views/BaoCao/_TheKhoSummary.cshtml` | Banner chi tiết hàng hóa và 4 thẻ KPI Thẻ kho |
| `Views/BaoCao/_TheKhoTable.cshtml` | Bảng dòng biến động kèm dòng Tồn đầu kỳ, Tồn cuối kỳ, `_Pagination` |
| `Views/BaoCao/_TheKhoContent.cshtml` | Wrapper nạp đồng bộ KPI Summary và Bảng dữ liệu qua 1 request AJAX |
| `wwwroot/css/the-kho.css` | CSS styling chuẩn chuyên nghiệp cho Autocomplete, Banner, KPI Cards, Bảng Thẻ kho, Badges |
| `wwwroot/js/the-kho-index.js` | Tương tác Autocomplete F3, Lego Date Filter, AJAX pagination, Export Excel, Print |
| `Views/Shared/_MainMenu.cshtml` | Kích hoạt liên kết `Báo cáo ➜ Kho ➜ Thẻ kho` |
| `Views/Shared/_Header.cshtml` | Kích hoạt menu mobile `Báo cáo Thẻ kho` |
| `Views/HangHoa/_HangHoaTable.cshtml` | Thay thế mock data tab 3 bằng dynamic container và link "Xem thẻ kho chi tiết" |
| `wwwroot/js/hang-hoa-index.js` | Nạp nhanh 10 giao dịch thực tế qua `GetTheKhoQuick` khi click tab Thẻ kho |

---

## 4. Luồng Màn hình & Tương tác (Chuẩn giao diện UI/UX)

### 4.1. Màn hình Báo cáo Thẻ kho (`/BaoCao/TheKho`)
1. **Sidebar bộ lọc bên trái**:
   - Khối tìm kiếm hàng hóa Autocomplete F3: Gõ từ khóa tìm kiếm theo tên hoặc mã, nhấn phím điều hướng Up/Down/Enter để chọn, thẻ mặt hàng cập nhật tức thì.
   - Dropdown chi nhánh kho: Thủ kho cố định kho làm việc; Quản lý được chọn "Tất cả chi nhánh kho".
   - Bộ lọc thời gian: Lego `_KiotDateFilter` (Hôm nay, Hôm qua, 7 ngày qua, Tháng này, Tháng trước, Tùy chọn dải ngày).
   - Bộ lọc loại giao dịch: Checkbox Lego `_KiotStatusFilter` kèm chấm chỉ báo `filter-dot` (`#dotLoaiGd`).
   - Phím tắt F4 thu gọn / mở rộng sidebar; hỗ trợ nút phễu mở bộ lọc trên mobile.
2. **Cột hiển thị chính bên phải**:
   - Toolbar: Ô tìm kiếm nhanh giao dịch, nút **"Xuất file"** Excel ClosedXML, nút **"In thẻ kho"**, nút tải lại bảng.
   - Banner hàng hóa: Tên hàng, mã hàng nội bộ, ĐVT, nhóm hàng, vị trí, giá vốn, giá bán, kho, dải ngày.
   - 4 Thẻ KPI: Tồn đầu kỳ, Tổng nhập trong kỳ (+), Tổng xuất trong kỳ (-), Tồn cuối kỳ.
   - Bảng biến động Thẻ kho:
     * Dòng đầu: Cố định **"Tồn đầu kỳ"**.
     * Các dòng giao dịch: Thời gian, Mã chứng từ linkable, Loại giao dịch (badge màu), Đối tác/Diễn giải, Đơn giá, Nhập (+), Xuất (-), Tồn lũy kế, Ghi chú.
     * Dòng cuối: Cố định **"Tồn cuối kỳ"** và tổng phát sinh.
     * Phân trang AJAX: Tự động tải lại bảng khi bấm trang.

### 4.2. Tích hợp liên kết hai chiều với Danh sách Hàng hóa
- Khi mở Drawer chi tiết hàng hóa tại `/HangHoa` và bấm vào Tab **"Thẻ kho"**:
  - Tự động gọi API `/BaoCao/GetTheKhoQuick?maHang={id}&soLuong=10` nạp 10 giao dịch gần nhất kèm tồn lũy kế chính xác.
  - Hiển thị badge loại giao dịch pastel chuẩn chuyên nghiệp (`.badge-tk`), phân tách rõ ràng cột SL Nhập (+) và SL Xuất (-).
  - Cung cấp nút **"Xem thẻ kho chi tiết ➜"** mở trực tiếp trang `/BaoCao/TheKho?maHang={id}`.

---

## 5. Quy tắc Nghiệp vụ Bất biến (Tuân thủ AGENTS.md)
1. **Tính toán động**: Số liệu tồn đầu kỳ và tồn lũy kế luôn tính động từ `vw_BienDongKho`, không lưu tồn tĩnh.
2. **Bảo toàn dữ liệu**: Chứng từ trạng thái `'HUY'` tự động bị loại trừ khỏi Thẻ kho, số dư tự động hoàn lại.
3. **Phân quyền chặt chẽ**: Thủ kho chỉ được xem thẻ kho của kho phụ trách; Quản lý có quyền xem liên kho.
4. **Không âm thầm nuốt lỗi**: Nếu không tìm thấy hàng hóa hoặc không có quyền truy cập, hiển thị thông báo giao diện thân thiện thay vì ném ngoại lệ 500.
