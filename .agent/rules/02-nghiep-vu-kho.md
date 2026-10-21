# 02. Nghiệp vụ Quản lý kho hàng & Trạng thái chứng từ

## 1. Cơ chế tính toán Tồn kho động (`vw_TonKhoHienTai`)

Trong hệ thống, tồn kho **tuyệt đối không lưu thành bảng số lượng tĩnh** để tránh lệch số liệu giữa tồn kho và chứng từ lịch sử. Tồn kho của từng mặt hàng tại từng kho được tính toán động thông qua View CSDL **`vw_TonKhoHienTai`**:

$$\text{Tồn kho} = (\text{Tổng Nhập} + \text{Chuyển đến đã nhận} + \text{Điều chỉnh tăng}) - (\text{Tổng Xuất} + \text{Chuyển đi} + \text{Điều chỉnh giảm})$$

### Các nguồn phát sinh biến động tồn kho:
1. **Nhập kho (`PhieuNhap`)**: Trạng thái `TrangThai = 'DA_XAC_NHAN'` ➜ Cộng tồn tại `MaKho`.
2. **Xuất kho (`PhieuXuat`)**: Trạng thái `TrangThai = 'DA_XAC_NHAN'` ➜ Trừ tồn tại `MaKho`.
3. **Chuyển kho (`PhieuChuyenKho`)**:
   - Trạng thái `'DA_XUAT'` hoặc `'DA_NHAN'` ➜ Trừ tồn tại kho nguồn (`MaKhoNguon`).
   - Trạng thái `'DA_NHAN'` ➜ Cộng tồn tại kho đích (`MaKhoDich`).
4. **Kiểm kho & Điều chỉnh (`PhieuDieuChinh`)**:
   - Trạng thái `TrangThai = 'DA_DUYET'`.
   - `ChiTietPhieuDieuChinh.SoLuongDieuChinh > 0` ➜ Tăng tồn kho tương ứng.
   - `ChiTietPhieuDieuChinh.SoLuongDieuChinh < 0` ➜ Giảm tồn kho tương ứng.
5. **Trả hàng nhập (`PhieuTraHangNhap`)**: `TrangThai = 'DA_TRA_HANG'` ➜ Trừ tồn tại `MaKho`.

---

## 2. Chuẩn hóa Trạng thái chứng từ (Chuỗi `VARCHAR`)

Các chứng từ trong hệ thống dùng mã chuỗi tiếng Việt không dấu (khớp với CHECK Constraint trong CSDL):

| Loại chứng từ | Giá trị trạng thái | Ý nghĩa | Trạng thái hiển thị |
|---|---|---|---|
| **`PhieuNhap`** | `'NHAP'` | Phiếu tạm | Tạm lưu |
| | `'DA_XAC_NHAN'` | Đã nhập kho (ảnh hưởng tồn) | Đã nhập hàng |
| | `'HUY'` | Đã hủy | Đã hủy |
| **`PhieuTraHangNhap`** | `'NHAP'` | Phiếu tạm | Tạm lưu |
| | `'DA_TRA_HANG'` | Đã trả hàng (ảnh hưởng tồn) | Đã trả hàng |
| | `'HUY'` | Đã hủy | Đã hủy |
| **`PhieuXuat`** | `'NHAP'` | Phiếu tạm | Tạm lưu |
| | `'DA_XAC_NHAN'` | Đã xuất kho (ảnh hưởng tồn) | Đã xuất kho |
| | `'HUY'` | Đã hủy | Đã hủy |
| **`PhieuChuyenKho`** | `'NHAP'` | Phiếu tạm | Tạm lưu |
| | `'DA_XUAT'` | Đang chuyển (đã trừ kho nguồn) | Đang chuyển |
| | `'DA_NHAN'` | Đã nhận (đã cộng kho đích) | Đã nhận |
| | `'HUY'` | Đã hủy | Đã hủy |
| **`PhieuKiemKe`** | `'NHAP'` | Phiếu tạm | Đang kiểm |
| | `'DA_KIEM'` | Đã kiểm xong, chờ duyệt cân bằng | Đã kiểm |
| | `'DA_DIEU_CHINH'` | Đã cân bằng kho (khóa vĩnh viễn) | Đã cân bằng |
| | `'HUY'` | Đã hủy | Đã hủy |
| **`PhieuDieuChinh`** | `'CHO_DUYET'` | Chờ duyệt | Chờ duyệt |
| | `'DA_DUYET'` | Đã duyệt (ảnh hưởng tồn) | Đã duyệt |
| | `'HUY'` | Đã hủy | Đã hủy |

---

## 3. Các quy tắc nghiệp vụ bất biến

1. **Hiệu lực tồn kho**: CHỈ những chứng từ ở trạng thái `DA_XAC_NHAN`, `DA_NHAN`, `DA_DUYET`, `DA_TRA_HANG` mới làm thay đổi số lượng tồn trong `vw_TonKhoHienTai`. Phiếu tạm (`NHAP`) **tuyệt đối không ảnh hưởng đến tồn kho**.
2. **Quyền sửa & xóa chứng từ**:
   - **Phiếu tạm (`NHAP`)**: Được phép chỉnh sửa số lượng, thêm/bớt mặt hàng hoặc xóa phiếu.
   - **Phiếu đã hoàn thành**: Tuyệt đối **không được sửa hoặc xóa** để bảo đảm tính toàn vẹn sổ sách. Chỉ được thực hiện thao tác **Hủy** (cập nhật `TrangThai = 'HUY'`, View SQL tự động loại trừ phiếu đã hủy).
3. **NGOẠI LỆ ĐẶC BIỆT: Phiếu kiểm kho đã cân bằng KHÔNG ĐƯỢC HỦY**:
   - Khi một `PhieuKiemKe` đã chuyển sang trạng thái `'DA_DIEU_CHINH'` (đã sinh `PhieuDieuChinh` có `DA_DUYET`), phiếu này bị **khóa vĩnh viễn, không được hủy hay xóa**.
   - **Lý do**: Sau khi cân bằng, kho đã phát sinh các giao dịch tiếp theo dựa trên mốc tồn mới. Hủy phiếu kiểm cũ sẽ làm sai lệch toàn bộ chuỗi giao dịch tiếp theo. Nếu phát hiện kiểm sai, bắt buộc phải lập một phiếu kiểm mới.
4. **Quy tắc chốt tồn tại thời điểm cân bằng kiểm kho**:
   - Khi bấm **"Cân bằng kho"**, hệ thống phải đọc lại tồn hiện tại từ `vw_TonKhoHienTai` để gán vào `SoLuongTheoSo` của phiếu.
   - Chênh lệch thực tế được tính: $\text{Số lượng điều chỉnh} = \text{Thực tế} - \text{Sổ sách tại thời điểm cân bằng}$.
5. **Dòng "Chưa kiểm" trong kiểm kho**:
   - Mặt hàng có `SoLuongThucTe = null` (Chưa kiểm) sẽ **bỏ qua**: giữ nguyên tồn hiện tại, không sinh dòng điều chỉnh kho và không tính vào tổng chênh lệch.
6. **Chặn tồn âm**:
   - Trước khi xác nhận xuất kho, chuyển kho hoặc trả hàng nhập, hệ thống phải kiểm tra số lượng tồn hiện thời tại kho nguồn qua `vw_TonKhoHienTai`. Nếu $\text{Số lượng tồn} < \text{Số lượng xuất}$, phải thông báo lỗi tiếng Việt rõ ràng và hủy bỏ giao dịch.
7. **Luôn dùng Giao dịch (Transaction)**:
   - Mọi thao tác lưu phiếu cùng các dòng chi tiết hoặc cân bằng kho phải được bọc trong giao dịch qua `DataProcessing.ChangeDataTransaction(...)` để đảm bảo hoặc thành công toàn bộ, hoặc rollback toàn bộ khi có lỗi.
8. **Luôn ghi `NhatKyThaoTac`**:
   - Mọi thao tác quan trọng (Hoàn thành chứng từ, Cân bằng kho, Hủy phiếu, Xóa phiếu tạm) đều phải ghi một bản ghi vào bảng `NhatKyThaoTac` với thông tin: Mã tài khoản, thời điểm, đối tượng, nội dung thao tác.
9. **Quy tắc Đơn vị tính cơ bản duy nhất**:
   - Toàn bộ hệ thống chỉ quản lý tồn kho theo **1 Đơn vị tính cơ bản duy nhất** cho mỗi mặt hàng (`HangHoa.MaDvt`).
   - Mọi chứng từ (Nhập, Xuất, Chuyển, Kiểm) chỉ ghi nhận theo ĐVT cơ bản, không dùng ĐVT phụ hay hệ số quy đổi.
