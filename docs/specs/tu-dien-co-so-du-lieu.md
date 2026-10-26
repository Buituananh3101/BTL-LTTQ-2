# Đặc Tả: Từ Điển Cơ Sở Dữ Liệu QuanLyKhoPhuQuocLTW

> **Hệ thống CSDL**: SQL Server | **ORM**: EF Core 10 | **Quy mô**: 29 Bảng + 2 Views (3NF)

---

## 1. Danh Mục 29 Bảng Theo 5 Phân Hệ

| STT | Tên Bảng | PK | FK | Cột | Nghiệp Vụ Chính |
|---|---|---|---|:---:|---|
| 1 | `VaiTro` | `MaVaiTro` | - | 3 | Quản lý, Thủ kho, Nhân viên |
| 2 | `NhanVien` | `MaNv` | - | 8 | Thông tin nhân sự, SĐT, Email, Ghi chú |
| 3 | `TaiKhoan` | `MaTaiKhoan` | `MaVaiTro`, `MaNv` | 8 | Đăng nhập, hash BCrypt, trạng thái |
| 4 | `TaiKhoanKho` | `MaTaiKhoan, MaKho` | `MaTaiKhoan`, `MaKho` | 3 | Phân quyền chi nhánh kho được truy cập |
| 5 | `NhatKyThaoTac` | `MaNhatKy` | `MaTaiKhoan` | 9 | Audit log hành vi hệ thống |
| 6 | `Kho` | `MaKho` | - | 6 | Chi nhánh kho vật lý, địa chỉ |
| 7 | `DonViTinh` | `MaDvt` | - | 2 | Đơn vị đo lường: Cái, Hộp, Thùng, Kg |
| 8 | `NhomHang` | `MaNhom` | - | 3 | Phân loại ngành hàng |
| 9 | `HangHoa` | `MaHang` | `MaDvt`, `MaNhom` | 17 | SKU, Barcode, Giá vốn, Giá bán, Trạng thái |
| 10 | `HinhAnhHangHoa`| `MaHinhAnh` | `MaHang` | 5 | Bộ 5 ảnh chi tiết từng mặt hàng |
| 11 | `NguongTonKho` | `MaNguong` | `MaHang`, `MaKho` | 5 | Định mức tồn Min/Max theo kho |
| 12 | `NhomNhaCungCap`| `MaNhomNcc` | - | 3 | Phân loại nhà cung cấp |
| 13 | `DonViCapHang` | `MaNcc` | `MaNhomNcc` | 13 | Hồ sơ NCC, MST, công nợ |
| 14 | `KhachHang` | `MaKh` | - | 7 | Khách hàng, SĐT, điểm tích lũy |
| 15 | `PhieuNhap` | `MaPhieuNhap` | `MaKho`, `MaNcc`, `MaNv...` | 12 | Phiếu nhập hàng từ nhà cung cấp |
| 16 | `ChiTietPhieuNhap`| `MaCtphieuNhap`| `MaPhieuNhap`, `MaHang` | 6 | Chi tiết hàng nhập, đơn giá, SL |
| 17 | `PhieuTraHangNhap`| `MaPhieuTraHang`| `MaKho`, `MaNcc`, `MaNv...` | 12 | Phiếu trả hàng cho nhà cung cấp |
| 18 | `ChiTietPhieuTraHangNhap`| `MaCtphieuTra`| `MaPhieuTra`, `MaHang` | 7 | Chi tiết hàng trả, giá hoàn, lý do |
| 19 | `PhieuXuat` | `MaPhieuXuat` | `MaKho`, `MaDonBan`, `MaNv...`| 12 | Phiếu xuất hủy / xuất hao hụt |
| 20 | `ChiTietPhieuXuat`| `MaCtphieuXuat`| `MaPhieuXuat`, `MaHang` | 6 | Chi tiết hàng xuất, SL xuất, giá vốn |
| 21 | `PhieuChuyenKho`| `MaPhieuChuyen`| `KhoNguon`, `KhoDich`, `MaNv...` | 14 | Phiếu điều chuyển hàng liên kho |
| 22 | `ChiTietPhieuChuyenKho`| `MaCtphieuChuyen`| `MaPhieuChuyen`, `MaHang` | 7 | Chi tiết hàng chuyển, SL chuyển, SL nhận |
| 23 | `PhieuKiemKe` | `MaPhieuKiemKe`| `MaKho`, `MaNvlap` | 9 | Đợt kiểm kê hàng hóa tại kho |
| 24 | `ChiTietPhieuKiemKe`| `MaCtkiemKe` | `MaPhieuKiemKe`, `MaHang` | 7 | Tồn sổ sách vs Tồn thực tế đếm |
| 25 | `PhieuDieuChinh`| `MaPhieuDieuChinh`| `MaPhieuKiemKe`, `MaNv...` | 8 | Cân bằng kho tự động sinh |
| 26 | `ChiTietPhieuDieuChinh`| `MaCtdieuChinh`| `MaPhieuDieuChinh`, `MaCt...`| 5 | Chi tiết SL lệch cần bù trừ |
| 27 | `DonBan` | `MaDonBan` | `MaKh`, `MaKho`, `MaNvlap` | 11 | Đơn bán lẻ / xuất bán |
| 28 | `ChiTietDonBan` | `MaCtdonBan` | `MaDonBan`, `MaHang` | 6 | Chi tiết mặt hàng bán, chiết khấu |
| 29 | `KhoanThu` | `MaKhoanThu` | `MaDonBan`, `MaNvnop` | 7 | Thu tiền thanh toán đơn bán |

---

## 2. Chi Tiết Các Bảng Trọng Tâm

### 2.1. Bảng `HangHoa`
| Tên Cột | Kiểu Dữ Liệu | Ràng Buộc | Ý Nghĩa |
|---|---|---|---|
| `MaHang` | `INT IDENTITY` | PK | Mã định danh hàng hóa |
| `MaHangNoiBo` | `NVARCHAR(50)` | UQ, NOT NULL | Mã SKU (VD: `SP0001`) |
| `TenHang` | `NVARCHAR(250)` | NOT NULL | Tên mặt hàng |
| `MaVach` | `VARCHAR(50)` | INDEX | Barcode quét nhanh |
| `GiaVon` | `DECIMAL(18,2)` | DEFAULT 0 | Giá vốn bình quân |
| `GiaBanThamKhao`| `DECIMAL(18,2)` | DEFAULT 0 | Giá bán niêm yết |
| `MaDvt` | `INT` | FK $\to$ `DonViTinh` | Đơn vị tính cơ sở |
| `MaNhom` | `INT` | FK $\to$ `NhomHang` | Nhóm ngành hàng |
| `TrangThai` | `NVARCHAR(50)` | DEFAULT `'DANG_KINH_DOANH'` | Trạng thái kinh doanh |
| `ViTriKho` | `NVARCHAR(100)` | NULL | Vị trí lưu kho (Kệ/Dãy) |
| `TrongLuong` | `DECIMAL(18,2)` | NULL | Trọng lượng (gam) |
| `TonToiDa` | `DECIMAL(18,2)` | NULL | Hạn mức tồn tối đa |
| `MoTa` | `NVARCHAR(MAX)` | NULL | Mô tả chi tiết sản phẩm |
| `DaDanhDau` | `BIT` | DEFAULT 0 | Cờ gắn sao mặt hàng |

### 2.2. Bảng `Kho` & `TaiKhoan`
| Bảng | Tên Cột | Kiểu | Ràng Buộc | Ý Nghĩa |
|---|---|---|---|---|
| `Kho` | `MaKho` | `INT IDENTITY` | PK | Mã chi nhánh kho |
| `Kho` | `TenKho` | `NVARCHAR(200)`| UQ, NOT NULL | Tên chi nhánh |
| `Kho` | `TrangThai` | `NVARCHAR(50)` | DEFAULT `'HOAT_DONG'`| `HOAT_DONG`/`NGUNG_HOAT_DONG` |
| `TaiKhoan` | `MaTaiKhoan` | `INT IDENTITY` | PK | Khóa chính tài khoản |
| `TaiKhoan` | `TenDangNhap` | `VARCHAR(50)` | UQ, NOT NULL | Tên đăng nhập |
| `TaiKhoan` | `MatKhauHash` | `VARCHAR(255)` | NOT NULL | Hash BCrypt mật khẩu |
| `TaiKhoan` | `MaVaiTro` | `INT` | FK $\to$ `VaiTro` | Vai trò quyền hạn |
| `TaiKhoan` | `MaNv` | `INT` | FK $\to$ `NhanVien` | Nhân sự sở hữu |

### 2.3. Bảng `PhieuNhap` & `PhieuChuyenKho`
| Bảng | Tên Cột | Kiểu | Ràng Buộc | Ý Nghĩa |
|---|---|---|---|---|
| `PhieuNhap` | `MaPhieuNhap` | `INT IDENTITY` | PK | Mã định danh phiếu |
| `PhieuNhap` | `MaPhieu` | `NVARCHAR(50)` | UQ | Mã hiển thị (`PN000001`) |
| `PhieuNhap` | `MaKho` | `INT` | FK $\to$ `Kho` | Chi nhánh nhập hàng |
| `PhieuNhap` | `MaNcc` | `INT` | FK $\to$ `DonViCapHang` | Nhà cung cấp |
| `PhieuNhap` | `TrangThai` | `NVARCHAR(50)` | NOT NULL | `PHIEU_TAM`, `DA_XAC_NHAN`, `DA_HUY` |
| `ChuyenKho` | `MaPhieuChuyen`| `INT IDENTITY` | PK | Mã định danh phiếu chuyển |
| `ChuyenKho` | `MaKhoNguon` | `INT` | FK $\to$ `Kho` | Kho xuất đi |
| `ChuyenKho` | `MaKhoDich` | `INT` | FK $\to$ `Kho` | Kho nhận đến |
| `ChuyenKho` | `TrangThai` | `NVARCHAR(50)` | NOT NULL | `PHIEU_TAM`, `DA_CHUYEN`, `DA_NHAN` |

---

## 3. Lược Đồ 2 Views Tồn Kho Động

1. **`vw_BienDongKho` (Hợp nhất 6 luồng chứng từ)**:
   - Gom: Nhập kho (+), Xuất hủy (-), Chuyển xuất (- kho nguồn), Chuyển nhập (+ kho đích theo `SoLuongNhan`), Điều chỉnh kiểm kê (+/-), Trả hàng nhập (-).
2. **`vw_TonKhoHienTai` (Tồn kho tức thời)**:
   - `SELECT MaKho, MaHang, CAST(SUM(SoLuongTang) - SUM(SoLuongGiam) AS DECIMAL(18,2)) AS SoLuongTon FROM vw_BienDongKho GROUP BY MaKho, MaHang;`
