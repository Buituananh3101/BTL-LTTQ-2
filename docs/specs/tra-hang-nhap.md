# Đặc tả Chức năng: Trả hàng nhập (Purchase Returns - Windows Forms)

> **Mục tiêu**: Quản lý nghiệp vụ trả hàng nhập cho Nhà cung cấp trên nền tảng Windows Forms (.NET Framework 4.7.2). Cho phép người dùng chọn số lượng hàng cần trả theo từng phiếu nhập, kiểm tra logic validation chặt chẽ (chặn âm, chặn vượt số lượng nhập, chặn vượt tồn kho hiện tại) và tự động tính toán số tiền hàng trả lại để trừ vào công nợ của đơn hoặc xác định số tiền Nhà cung cấp cần hoàn trả lại cho cửa hàng.

---

## 1. Kiến trúc File & CSDL

### 1.1. Bản đồ File liên quan (File Map)
- **Thư mục chức năng**: `BTL-LTTQ-2/GUI/Forms/NhapHang/`
  - `frmTraHangNhap.cs`, `.Designer.cs`, `.resx`: Form nghiệp vụ Trả hàng nhập, được mở từ nút `Trả hàng nhập` của `frmChiTietPhieuNhap`.
  - `frmChiTietPhieuNhap.cs`: Form xem chi tiết đơn nhập, nơi gọi `frmTraHangNhap(maPhieuNhap)`.
- **Truy xuất dữ liệu (DAL)**: `BTL-LTTQ-2/DAL/DataProcessing.cs` (ADO.NET thuần: `ReadData`, `ReadValue`, `ChangeDataTransaction`).
- **Quản lý phiên đăng nhập**: `BTL-LTTQ-2/BLL/SessionContext.cs` (lấy `MaTaiKhoan`, vai trò).

### 1.2. Cơ sở dữ liệu tham gia
- **`PhieuTraHangNhap`**:
  - `MaPhieuTraHang` INT IDENTITY(1,1) PRIMARY KEY
  - `MaPhieu` VARCHAR(30) UNIQUE (Mã sinh tự động với tiền tố `THN000001`)
  - `MaKho` INT NOT NULL (Khóa ngoại $\to$ `Kho`)
  - `MaDonViCapHang` INT NULL (Khóa ngoại $\to$ `DonViCapHang`)
  - `MaNvlap` INT NOT NULL (Khóa ngoại $\to$ `TaiKhoan`)
  - `NgayLap`, `NgayTra` DATETIME DEFAULT GETDATE()
  - `TrangThai` VARCHAR(20) (`NHAP`, `DA_TRA_HANG`, `HUY`)
  - `TongSoLuong` DECIMAL(18,2)
  - `TongTienHang` DECIMAL(18,2)
  - `CanThuNcc` DECIMAL(18,2) (Số tiền NCC hoàn lại hoặc trừ công nợ)
  - `DaThuNcc` DECIMAL(18,2) (Số tiền NCC đã thanh toán hoàn trả lại)
  - `GhiChu` NVARCHAR(500) NULL
  - `DaDanhDau` BIT DEFAULT 0
- **`ChiTietPhieuTraHangNhap`**:
  - `MaCtphieuTra` INT IDENTITY(1,1) PRIMARY KEY
  - `MaPhieuTraHang` INT NOT NULL (Khóa ngoại $\to$ `PhieuTraHangNhap`)
  - `MaHang` INT NOT NULL (Khóa ngoại $\to$ `HangHoa`)
  - `SoLuong` DECIMAL(18,2) NOT NULL
  - `DonGiaTra` DECIMAL(18,2) NOT NULL (Đơn giá nhập theo phiếu gốc)
  - `ThanhTien` DECIMAL(18,2) (`SoLuong * DonGiaTra`)
  - `GhiChu` NVARCHAR(255) NULL
- **`vw_BienDongKho` & `vw_TonKhoHienTai`**:
  - Khi phiếu trả hàng ở trạng thái `DA_TRA_HANG`, tự động ghi nhận xuất giảm tồn kho tại `MaKho`.

---

## 2. Quy chuẩn Giao diện & Trải nghiệm Người dùng

### 2.1. Bố cục Form `frmTraHangNhap`:
- **Thông tin đơn nhập gốc & Header**:
  - Mã phiếu trả: Tự động đề xuất theo quy tắc `THN00000X`.
  - Phiếu nhập gốc: Mã phiếu nhập tham chiếu (ReadOnly).
  - Nhà cung cấp & Kho: Tên NCC và kho nhận trả hàng.
  - Ngày trả: DateTimePicker chọn thời điểm thực tế xuất trả hàng.
  - Người thực hiện: Tên tài khoản đang đăng nhập.
  - Ghi chú: TextBox nhập lý do trả hàng (hàng lỗi, cận date, sai quy cách...).
- **Bảng dữ liệu hàng hóa (`dgvHangTra`)**:
  - STT, Mã hàng nội bộ, Tên hàng hóa, ĐVT.
  - `Số lượng nhập`: Số lượng đã nhập theo phiếu gốc (ReadOnly).
  - `Tồn kho hiện tại`: Tồn thực tế tại kho từ `vw_TonKhoHienTai` (ReadOnly).
  - `Số lượng trả`: Cột cho phép người dùng nhập (`ReadOnly = false`). Mặc định ban đầu = 0.
  - `Giá nhập`: Đơn giá mua từ phiếu nhập gốc (ReadOnly).
  - `Thành tiền trả`: Tự động tính (`Số lượng trả * Giá nhập`).
- **Khối tính toán tài chính & Công nợ (Bên phải / Footer)**:
  - `Tổng số lượng trả`: Tổng số lượng hàng hóa được hoàn trả.
  - `Tổng giá trị trả hàng`: Tổng tiền của các mặt hàng trả lại.
  - `Nợ đơn nhập gốc`: Số tiền còn nợ của phiếu nhập này (`CanTraNcc`).
  - `Khấu trừ vào nợ đơn`: Tự động tính = $\min(\text{Tổng giá trị trả}, \text{Nợ đơn nhập gốc})$.
  - `Tiền NCC cần hoàn trả lại`: Tự động tính = $\max(0, \text{Tổng giá trị trả} - \text{Nợ đơn nhập gốc})$.
  - `Nợ còn lại của đơn sau trả`: Tự động tính = $\max(0, \text{Nợ đơn nhập gốc} - \text{Tổng giá trị trả})$.
  - `Tiền NCC đã trả lại`: Ô cho phép nhập số tiền thực tế NCC đã chuyển khoản hoặc trả tiền mặt cho cửa hàng.
- **Nút bấm thao tác**:
  - `Lưu tạm`: Lưu phiếu ở trạng thái `NHAP` (chưa trừ kho).
  - `Trả hàng`: Xác nhận hoàn tất trả hàng (`DA_TRA_HANG`), kiểm tra tồn kho, trừ tồn kho và cập nhật công nợ trong Transaction.
  - `Đóng`: Hủy thao tác và đóng form.
  - Toàn bộ nút sử dụng định dạng chuẩn WinForms (`UseVisualStyleBackColor = true`), không dùng màu tùy tiện.

---

## 3. Logic Nghiệp vụ & Kiểm tra Ràng buộc (Validation)

1. **Kiểm tra Số lượng trả (`SoLuongTra`)**:
   - $\text{Số lượng trả} \ge 0$: Tuyệt đối không cho phép nhập số âm.
   - $\text{Số lượng trả} \le \text{Số lượng nhập}$: Không được trả vượt quá số lượng đã nhập trong phiếu gốc. Thông báo cảnh báo hiển thị định dạng số nguyên `N0` (ví dụ `12` và `10`, chặn hoàn toàn đuôi số thực `.00`).
   - $\text{Số lượng trả} \le \text{Tồn kho hiện tại}$: Không được trả vượt quá số lượng hàng thực tế đang còn trong kho (tránh âm kho).
   - Tổng số lượng trả trên phiếu phải $> 0$ (phải có ít nhất 1 mặt hàng có số lượng trả $> 0$).
2. **Thuật toán Khấu trừ Tài chính**:
   - **Trường hợp 1**: Cửa hàng chưa trả hết tiền cho NCC ($\text{Nợ đơn gốc} > 0$):
     - Giá trị hàng trả sẽ ưu tiên bù trừ vào khoản nợ của đơn nhập gốc trước (`PhieuNhap.CanTraNcc`).
     - Nếu $\text{Giá trị trả} \le \text{Nợ đơn gốc}$: Toàn bộ giá trị trả được trừ vào nợ. Cửa hàng không phải trả nợ khoản này, NCC không phải trả tiền mặt.
     - Nếu $\text{Giá trị trả} > \text{Nợ đơn gốc}$: Nợ của đơn gốc về 0; phần dư dôi ra chính là số tiền NCC phải trả lại tiền mặt/chuyển khoản cho cửa hàng.
   - **Trường hợp 2**: Cửa hàng đã thanh toán 100% tiền đơn nhập gốc ($\text{Nợ đơn gốc} = 0$):
     - Toàn bộ giá trị hàng trả chuyển thành khoản "Tiền NCC cần hoàn trả lại".
   - **Lưu ý**: Công nợ NCC được tính động từ chứng từ phiếu nhập, không thực hiện cập nhật cột tĩnh `NoCanTraHienTai` trong `DonViCapHang`.
3. **Thực thi Giao dịch (Transaction)**:
   - Thao tác lưu phiếu trả hàng, chi tiết hàng trả, cập nhật công nợ đơn nhập gốc và ghi nhật ký thao tác `NhatKyThaoTac` phải được thực thi trọn vẹn trong `DataProcessing.ChangeDataTransaction`. Nếu có bất kỳ lỗi nào, tự động Rollback toàn bộ.
