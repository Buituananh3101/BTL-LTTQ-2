-- =====================================================================================
-- KHO PRO - SCRIPT DỮ LIỆU MẪU ĐẦY ĐỦ (DEMO SEED DATA)
-- File: docs/DuLieuMau.sql (Đồng bộ với migrations/sql/05_dulieumau_seed.sql)
-- Mô tả: Cung cấp toàn bộ dữ liệu mẫu chuẩn cho hệ thống Quản lý kho hàng nhiều kho
-- Tài khoản mặc định (đồng bộ với mục 9 của docs/database_update.sql):
--   1. quanly / 123456 (Vai trò: Quản lý - Toàn quyền, mọi kho)
--   2. thukho / 123456 (Vai trò: Thủ kho - Kho trung tâm)
--   3. nvkho  / 123456 (Vai trò: Nhân viên kho - Kho trung tâm)
-- =====================================================================================

USE [QuanLyKhoPhuQuocLTTQ];
GO

SET NOCOUNT ON;

PRINT N'==> BẮT ĐẦU NẠP DỮ LIỆU MẪU CHO HỆ THỐNG KHO PRO...';

-- -------------------------------------------------------------------------------------
-- 1. BẢNG VAI TRÒ (VaiTro)
-- -------------------------------------------------------------------------------------
IF NOT EXISTS (SELECT 1 FROM VaiTro WHERE MaVaiTro = 1)
BEGIN
    SET IDENTITY_INSERT VaiTro ON;
    INSERT INTO VaiTro (MaVaiTro, TenVaiTro, MoTa) VALUES (1, N'Quản lý', N'Quản lý toàn bộ chi nhánh và mọi kho hàng');
    SET IDENTITY_INSERT VaiTro OFF;
END
ELSE
    UPDATE VaiTro SET TenVaiTro = N'Quản lý', MoTa = N'Quản lý toàn bộ chi nhánh và mọi kho hàng' WHERE MaVaiTro = 1;

IF NOT EXISTS (SELECT 1 FROM VaiTro WHERE MaVaiTro = 2)
BEGIN
    SET IDENTITY_INSERT VaiTro ON;
    INSERT INTO VaiTro (MaVaiTro, TenVaiTro, MoTa) VALUES (2, N'Thủ kho', N'Quản lý 1 chi nhánh kho duy nhất được phân công');
    SET IDENTITY_INSERT VaiTro OFF;
END
ELSE
    UPDATE VaiTro SET TenVaiTro = N'Thủ kho', MoTa = N'Quản lý 1 chi nhánh kho duy nhất được phân công' WHERE MaVaiTro = 2;

IF NOT EXISTS (SELECT 1 FROM VaiTro WHERE MaVaiTro = 3)
BEGIN
    SET IDENTITY_INSERT VaiTro ON;
    INSERT INTO VaiTro (MaVaiTro, TenVaiTro, MoTa) VALUES (3, N'Nhân viên kho', N'Nhân viên thực hiện thao tác nghiệp vụ tại kho');
    SET IDENTITY_INSERT VaiTro OFF;
END
ELSE
    UPDATE VaiTro SET TenVaiTro = N'Nhân viên kho', MoTa = N'Nhân viên thực hiện thao tác nghiệp vụ tại kho' WHERE MaVaiTro = 3;

PRINT N'   [1/10] Đã đồng bộ bảng VaiTro.';

-- -------------------------------------------------------------------------------------
-- 2. BẢNG NHÂN VIÊN (NhanVien)
-- -------------------------------------------------------------------------------------
IF NOT EXISTS (SELECT 1 FROM NhanVien WHERE DienThoai = '0901000001')
BEGIN
    SET IDENTITY_INSERT NhanVien ON;
    INSERT INTO NhanVien (MaNV, HoTen, DienThoai, Email, NgayVaoLam, TrangThai)
    VALUES (1, N'Nguyễn Minh Quản', N'0901000001', N'quan.ly@khopro.vn', '2025-01-01', N'DANG_LAM');
    SET IDENTITY_INSERT NhanVien OFF;
END

IF NOT EXISTS (SELECT 1 FROM NhanVien WHERE DienThoai = '0901000002')
BEGIN
    SET IDENTITY_INSERT NhanVien ON;
    INSERT INTO NhanVien (MaNV, HoTen, DienThoai, Email, NgayVaoLam, TrangThai)
    VALUES (2, N'Trần Thị Kho', N'0901000002', N'thu.kho@khopro.vn', '2025-01-15', N'DANG_LAM');
    SET IDENTITY_INSERT NhanVien OFF;
END

IF NOT EXISTS (SELECT 1 FROM NhanVien WHERE DienThoai = '0901000003')
BEGIN
    SET IDENTITY_INSERT NhanVien ON;
    INSERT INTO NhanVien (MaNV, HoTen, DienThoai, Email, NgayVaoLam, TrangThai)
    VALUES (3, N'Lê Văn Kho', N'0901000003', N'nhanvien.kho@khopro.vn', '2025-02-01', N'DANG_LAM');
    SET IDENTITY_INSERT NhanVien OFF;
END

PRINT N'   [2/10] Đã đồng bộ bảng NhanVien.';

-- -------------------------------------------------------------------------------------
-- 3. BẢNG TÀI KHOẢN (TaiKhoan) - Mật khẩu: 123456 (SHA-256)
-- Hash: 8d969eef6ecad3c29a3a629280e686cf0c3f5d5a86aff3ca12020c923adc6c92
-- -------------------------------------------------------------------------------------
DECLARE @PassHash VARCHAR(256) = '8d969eef6ecad3c29a3a629280e686cf0c3f5d5a86aff3ca12020c923adc6c92';

DECLARE @nv1 INT = (SELECT TOP 1 MaNV FROM NhanVien WHERE DienThoai = '0901000001');
DECLARE @nv2 INT = (SELECT TOP 1 MaNV FROM NhanVien WHERE DienThoai = '0901000002');
DECLARE @nv3 INT = (SELECT TOP 1 MaNV FROM NhanVien WHERE DienThoai = '0901000003');

-- 3 vai trò chuẩn: 1 = Quản lý, 2 = Thủ kho, 3 = Nhân viên kho
DECLARE @vt1 INT = 1;
DECLARE @vt2 INT = 2;
DECLARE @vt3 INT = 3;

-- Dữ liệu cũ dùng tài khoản 'banhang': đổi tên thành 'nvkho' để không trùng nhân viên số 3
IF EXISTS (SELECT 1 FROM TaiKhoan WHERE TenDangNhap = 'banhang')
   AND NOT EXISTS (SELECT 1 FROM TaiKhoan WHERE TenDangNhap = 'nvkho')
    UPDATE TaiKhoan SET TenDangNhap = 'nvkho' WHERE TenDangNhap = 'banhang';

IF NOT EXISTS (SELECT 1 FROM TaiKhoan WHERE TenDangNhap = 'quanly')
    INSERT INTO TaiKhoan (MaNV, MaVaiTro, TenDangNhap, MatKhauHash, TrangThai)
    VALUES (ISNULL(@nv1, 1), ISNULL(@vt1, 1), 'quanly', @PassHash, N'HOAT_DONG');
ELSE
    UPDATE TaiKhoan SET MatKhauHash = @PassHash, TrangThai = N'HOAT_DONG', MaVaiTro = ISNULL(@vt1, 1) WHERE TenDangNhap = 'quanly';

IF NOT EXISTS (SELECT 1 FROM TaiKhoan WHERE TenDangNhap = 'thukho')
    INSERT INTO TaiKhoan (MaNV, MaVaiTro, TenDangNhap, MatKhauHash, TrangThai)
    VALUES (ISNULL(@nv2, 2), ISNULL(@vt2, 2), 'thukho', @PassHash, N'HOAT_DONG');
ELSE
    UPDATE TaiKhoan SET MatKhauHash = @PassHash, TrangThai = N'HOAT_DONG', MaVaiTro = ISNULL(@vt2, 2) WHERE TenDangNhap = 'thukho';

IF NOT EXISTS (SELECT 1 FROM TaiKhoan WHERE TenDangNhap = 'nvkho')
    INSERT INTO TaiKhoan (MaNV, MaVaiTro, TenDangNhap, MatKhauHash, TrangThai)
    VALUES (ISNULL(@nv3, 3), @vt3, 'nvkho', @PassHash, N'HOAT_DONG');
ELSE
    UPDATE TaiKhoan SET MatKhauHash = @PassHash, TrangThai = N'HOAT_DONG', MaVaiTro = @vt3 WHERE TenDangNhap = 'nvkho';

PRINT N'   [3/10] Đã đồng bộ bảng TaiKhoan (quanly, thukho, nvkho / 123456).';

-- -------------------------------------------------------------------------------------
-- 4. BẢNG KHO HÀNG (Kho)
-- -------------------------------------------------------------------------------------
IF NOT EXISTS (SELECT 1 FROM Kho WHERE MaKho = 1)
BEGIN
    SET IDENTITY_INSERT Kho ON;
    INSERT INTO Kho (MaKho, TenKho, DiaChi, MoTa, TrangThai)
    VALUES (1, N'Kho trung tâm', N'Dương Đông, Phú Quốc', N'Kho tổng phân phối chính toàn đảo', N'HOAT_DONG');
    SET IDENTITY_INSERT Kho OFF;
END
ELSE
    UPDATE Kho SET TenKho = N'Kho trung tâm', DiaChi = N'Dương Đông, Phú Quốc', TrangThai = N'HOAT_DONG' WHERE MaKho = 1;

IF NOT EXISTS (SELECT 1 FROM Kho WHERE MaKho = 2)
BEGIN
    SET IDENTITY_INSERT Kho ON;
    INSERT INTO Kho (MaKho, TenKho, DiaChi, MoTa, TrangThai)
    VALUES (2, N'Kho An Thới', N'An Thới, Phú Quốc', N'Kho trung chuyển chi nhánh phía Nam', N'HOAT_DONG');
    SET IDENTITY_INSERT Kho OFF;
END
ELSE
    UPDATE Kho SET TenKho = N'Kho An Thới', DiaChi = N'An Thới, Phú Quốc', TrangThai = N'HOAT_DONG' WHERE MaKho = 2;

IF NOT EXISTS (SELECT 1 FROM Kho WHERE MaKho = 3)
BEGIN
    SET IDENTITY_INSERT Kho ON;
    INSERT INTO Kho (MaKho, TenKho, DiaChi, MoTa, TrangThai)
    VALUES (3, N'Kho phụ kiện Tuna', N'Dương Đông, Phú Quốc', N'Kho chuyên chứa vật tư và phụ kiện ngành gas', N'HOAT_DONG');
    SET IDENTITY_INSERT Kho OFF;
END
ELSE
    UPDATE Kho SET TenKho = N'Kho phụ kiện Tuna', DiaChi = N'Dương Đông, Phú Quốc', TrangThai = N'HOAT_DONG' WHERE MaKho = 3;

PRINT N'   [4/10] Đã đồng bộ bảng Kho.';

-- -------------------------------------------------------------------------------------
-- 5. BẢNG PHÂN QUYỀN KHO (TaiKhoanKho) – theo quy tắc phân quyền 3 vai trò:
--    Quản lý: mọi kho đang hoạt động. Thủ kho / Nhân viên kho: Kho trung tâm (MaKho = 1)
--    nếu tài khoản chưa được phân công kho nào (không ghi đè phân công đã có).
-- -------------------------------------------------------------------------------------
INSERT INTO TaiKhoanKho (MaTaiKhoan, MaKho)
SELECT t.MaTaiKhoan, k.MaKho
FROM TaiKhoan t
CROSS JOIN Kho k
WHERE t.MaVaiTro = 1
  AND k.TrangThai = 'HOAT_DONG'
  AND NOT EXISTS (
      SELECT 1 FROM TaiKhoanKho tk WHERE tk.MaTaiKhoan = t.MaTaiKhoan AND tk.MaKho = k.MaKho
  );

INSERT INTO TaiKhoanKho (MaTaiKhoan, MaKho)
SELECT t.MaTaiKhoan, 1
FROM TaiKhoan t
WHERE t.MaVaiTro IN (2, 3)
  AND EXISTS (SELECT 1 FROM Kho WHERE MaKho = 1)
  AND NOT EXISTS (SELECT 1 FROM TaiKhoanKho tk WHERE tk.MaTaiKhoan = t.MaTaiKhoan);

PRINT N'   [5/10] Đã phân quyền kho theo vai trò trong TaiKhoanKho.';

-- -------------------------------------------------------------------------------------
-- 6. BẢNG ĐƠN VỊ TÍNH (DonViTinh) & NHÓM HÀNG (NhomHang)
-- -------------------------------------------------------------------------------------
IF NOT EXISTS (SELECT 1 FROM DonViTinh WHERE TenDvt = N'Bình') INSERT INTO DonViTinh (TenDvt) VALUES (N'Bình');
IF NOT EXISTS (SELECT 1 FROM DonViTinh WHERE TenDvt = N'Cái') INSERT INTO DonViTinh (TenDvt) VALUES (N'Cái');
IF NOT EXISTS (SELECT 1 FROM DonViTinh WHERE TenDvt = N'Bao') INSERT INTO DonViTinh (TenDvt) VALUES (N'Bao');
IF NOT EXISTS (SELECT 1 FROM DonViTinh WHERE TenDvt = N'Mét') INSERT INTO DonViTinh (TenDvt) VALUES (N'Mét');
IF NOT EXISTS (SELECT 1 FROM DonViTinh WHERE TenDvt = N'Cuộn') INSERT INTO DonViTinh (TenDvt) VALUES (N'Cuộn');
IF NOT EXISTS (SELECT 1 FROM DonViTinh WHERE TenDvt = N'Hộp') INSERT INTO DonViTinh (TenDvt) VALUES (N'Hộp');
IF NOT EXISTS (SELECT 1 FROM DonViTinh WHERE TenDvt = N'Bộ') INSERT INTO DonViTinh (TenDvt) VALUES (N'Bộ');

IF NOT EXISTS (SELECT 1 FROM NhomHang WHERE TenNhom = N'Bình gas') INSERT INTO NhomHang (TenNhom) VALUES (N'Bình gas');
IF NOT EXISTS (SELECT 1 FROM NhomHang WHERE TenNhom = N'Phụ kiện gas') INSERT INTO NhomHang (TenNhom) VALUES (N'Phụ kiện gas');
IF NOT EXISTS (SELECT 1 FROM NhomHang WHERE TenNhom = N'Than') INSERT INTO NhomHang (TenNhom) VALUES (N'Than');
IF NOT EXISTS (SELECT 1 FROM NhomHang WHERE TenNhom = N'Bình oxy') INSERT INTO NhomHang (TenNhom) VALUES (N'Bình oxy');
IF NOT EXISTS (SELECT 1 FROM NhomHang WHERE TenNhom = N'Thiết bị an toàn') INSERT INTO NhomHang (TenNhom) VALUES (N'Thiết bị an toàn');

PRINT N'   [6/10] Đã đồng bộ bảng DonViTinh và NhomHang.';

-- -------------------------------------------------------------------------------------
-- 7. BẢNG ĐƠN VỊ CẤP HÀNG / NHÀ CUNG CẤP (DonViCapHang) & KHÁCH HÀNG (KhachHang)
-- -------------------------------------------------------------------------------------
IF NOT EXISTS (SELECT 1 FROM DonViCapHang WHERE TenDonVi = N'Công ty Gas Kiên Giang')
    INSERT INTO DonViCapHang (TenDonVi, DienThoai, DiaChi, TrangThai)
    VALUES (N'Công ty Gas Kiên Giang', N'0297000001', N'Rạch Giá, Kiên Giang', N'HOAT_DONG');

IF NOT EXISTS (SELECT 1 FROM DonViCapHang WHERE TenDonVi = N'Công ty Phụ kiện Việt')
    INSERT INTO DonViCapHang (TenDonVi, DienThoai, DiaChi, TrangThai)
    VALUES (N'Công ty Phụ kiện Việt', N'0280000002', N'TP. Hồ Chí Minh', N'HOAT_DONG');

IF NOT EXISTS (SELECT 1 FROM DonViCapHang WHERE TenDonVi = N'Công ty Khí công nghiệp Pharmedic')
    INSERT INTO DonViCapHang (TenDonVi, DienThoai, DiaChi, TrangThai)
    VALUES (N'Công ty Khí công nghiệp Pharmedic', N'0297000003', N'Phú Quốc, Kiên Giang', N'HOAT_DONG');

IF NOT EXISTS (SELECT 1 FROM KhachHang WHERE TenKH = N'Cửa hàng Minh Anh')
    INSERT INTO KhachHang (TenKH, DienThoai, DiaChi, LoaiKhachHang, TrangThai)
    VALUES (N'Cửa hàng Minh Anh', N'0902000001', N'Cửa Dương, Phú Quốc', N'CUA_HANG', N'HOAT_DONG');

IF NOT EXISTS (SELECT 1 FROM KhachHang WHERE TenKH = N'Khách sạn Sunset Phú Quốc')
    INSERT INTO KhachHang (TenKH, DienThoai, DiaChi, LoaiKhachHang, TrangThai)
    VALUES (N'Khách sạn Sunset Phú Quốc', N'0902000002', N'Dương Đông, Phú Quốc', N'CUA_HANG', N'HOAT_DONG');

PRINT N'   [7/10] Đã đồng bộ bảng DonViCapHang và KhachHang.';

-- -------------------------------------------------------------------------------------
-- 8. BẢNG HÀNG HÓA (HangHoa) - Cập nhật thông tin chuẩn và giá vốn
-- -------------------------------------------------------------------------------------
DECLARE @dvtBinh INT = (SELECT TOP 1 MaDvt FROM DonViTinh WHERE TenDvt = N'Bình');
DECLARE @dvtCai INT = (SELECT TOP 1 MaDvt FROM DonViTinh WHERE TenDvt = N'Cái');
DECLARE @dvtBao INT = (SELECT TOP 1 MaDvt FROM DonViTinh WHERE TenDvt = N'Bao');
DECLARE @dvtMet INT = (SELECT TOP 1 MaDvt FROM DonViTinh WHERE TenDvt = N'Mét');

DECLARE @nhomGas INT = (SELECT TOP 1 MaNhom FROM NhomHang WHERE TenNhom = N'Bình gas');
DECLARE @nhomPk INT = (SELECT TOP 1 MaNhom FROM NhomHang WHERE TenNhom = N'Phụ kiện gas');
DECLARE @nhomThan INT = (SELECT TOP 1 MaNhom FROM NhomHang WHERE TenNhom = N'Than');
DECLARE @nhomOxi INT = (SELECT TOP 1 MaNhom FROM NhomHang WHERE TenNhom = N'Bình oxy');
DECLARE @nhomAnToan INT = (SELECT TOP 1 MaNhom FROM NhomHang WHERE TenNhom = N'Thiết bị an toàn');

-- Cập nhật thông tin và giá vốn cho các mặt hàng nòng cốt
UPDATE HangHoa SET TenHang = N'Bình gas 12kg', MaNhom = @nhomGas, MaDvt = @dvtBinh, GiaBanThamKhao = 420000, GiaVon = 350000, TrangThai = N'DANG_BAN' WHERE MaHang = 1;
UPDATE HangHoa SET TenHang = N'Bếp gas đơn Rinnai', MaNhom = @nhomPk, MaDvt = @dvtCai, GiaBanThamKhao = 350000, GiaVon = 280000, TrangThai = N'DANG_BAN' WHERE MaHang = 2;
UPDATE HangHoa SET TenHang = N'Ống dẫn gas chịu áp', MaNhom = @nhomPk, MaDvt = @dvtMet, GiaBanThamKhao = 25000, GiaVon = 15000, TrangThai = N'DANG_BAN' WHERE MaHang = 3;
UPDATE HangHoa SET TenHang = N'Than sạch không khói', MaNhom = @nhomThan, MaDvt = @dvtBao, GiaBanThamKhao = 70000, GiaVon = 45000, TrangThai = N'DANG_BAN' WHERE MaHang = 4;
UPDATE HangHoa SET TenHang = N'Bình oxy y tế 40 lít', MaNhom = @nhomOxi, MaDvt = @dvtBinh, GiaBanThamKhao = 850000, GiaVon = 650000, TrangThai = N'DANG_BAN' WHERE MaHang = 5;

-- Thêm các mặt hàng phong phú nếu chưa có
IF NOT EXISTS (SELECT 1 FROM HangHoa WHERE MaHangNoiBo = N'GAS45')
    INSERT INTO HangHoa (MaHangNoiBo, TenHang, MaNhom, MaDvt, GiaBanThamKhao, GiaVon, TrangThai, DaDanhDau)
    VALUES (N'GAS45', N'Bình gas công nghiệp 45kg', @nhomGas, @dvtBinh, 1450000, 1180000, N'DANG_BAN', 1);

IF NOT EXISTS (SELECT 1 FROM HangHoa WHERE MaHangNoiBo = N'BEP-DOI')
    INSERT INTO HangHoa (MaHangNoiBo, TenHang, MaNhom, MaDvt, GiaBanThamKhao, GiaVon, TrangThai, DaDanhDau)
    VALUES (N'BEP-DOI', N'Bếp gas đôi Paloma cao cấp', @nhomPk, @dvtCai, 1200000, 950000, N'DANG_BAN', 0);

IF NOT EXISTS (SELECT 1 FROM HangHoa WHERE MaHangNoiBo = N'VAN-01')
    INSERT INTO HangHoa (MaHangNoiBo, TenHang, MaNhom, MaDvt, GiaBanThamKhao, GiaVon, TrangThai, DaDanhDau)
    VALUES (N'VAN-01', N'Van điều áp ngắt gas tự động Namilux', @nhomPk, @dvtCai, 185000, 130000, N'DANG_BAN', 1);

IF NOT EXISTS (SELECT 1 FROM HangHoa WHERE MaHangNoiBo = N'CB-GAS')
    INSERT INTO HangHoa (MaHangNoiBo, TenHang, MaNhom, MaDvt, GiaBanThamKhao, GiaVon, TrangThai, DaDanhDau)
    VALUES (N'CB-GAS', N'Thiết bị báo động rò rỉ khí gas', @nhomAnToan, @dvtCai, 320000, 220000, N'DANG_BAN', 1);

IF NOT EXISTS (SELECT 1 FROM HangHoa WHERE MaHangNoiBo = N'OXI-MINI')
    INSERT INTO HangHoa (MaHangNoiBo, TenHang, MaNhom, MaDvt, GiaBanThamKhao, GiaVon, TrangThai, DaDanhDau)
    VALUES (N'OXI-MINI', N'Bình oxy cầm tay mini 1000ml', @nhomOxi, @dvtBinh, 150000, 95000, N'DANG_BAN', 0);

PRINT N'   [8/10] Đã đồng bộ bảng HangHoa với danh mục đa dạng kèm GiaVon.';

-- -------------------------------------------------------------------------------------
-- 9. BẢNG ĐỊNH MỨC TỒN KHO (NguongTonKho)
-- -------------------------------------------------------------------------------------
IF NOT EXISTS (SELECT 1 FROM NguongTonKho WHERE MaHang = 1 AND MaKho = 1)
    INSERT INTO NguongTonKho (MaHang, MaKho, TonToiThieu) VALUES (1, 1, 20);

IF NOT EXISTS (SELECT 1 FROM NguongTonKho WHERE MaHang = 2 AND MaKho = 1)
    INSERT INTO NguongTonKho (MaHang, MaKho, TonToiThieu) VALUES (2, 1, 10);

IF NOT EXISTS (SELECT 1 FROM NguongTonKho WHERE MaHang = 3 AND MaKho = 1)
    INSERT INTO NguongTonKho (MaHang, MaKho, TonToiThieu) VALUES (3, 1, 50);

PRINT N'   [9/10] Đã thiết lập ngưỡng tồn kho an toàn trong NguongTonKho.';

-- -------------------------------------------------------------------------------------
-- 10. ĐỒNG BỘ TỒN KHO MẪU QUA PHIẾU NHẬP ĐẦU KỲ
-- Đảm bảo các mặt hàng mới tạo có sẵn tồn kho để phục vụ demo chức năng Xuất, Kiểm
-- -------------------------------------------------------------------------------------
DECLARE @newItems TABLE (MaHang INT, SoLuong DECIMAL(18,2), GiaNhap DECIMAL(18,2));

INSERT INTO @newItems (MaHang, SoLuong, GiaNhap)
SELECT h.MaHang, 50, h.GiaVon
FROM HangHoa h
WHERE h.MaHangNoiBo IN ('GAS45', 'BEP-DOI', 'VAN-01', 'CB-GAS', 'OXI-MINI')
  AND NOT EXISTS (
      SELECT 1 FROM ChiTietPhieuNhap ct 
      JOIN PhieuNhap pn ON ct.MaPhieuNhap = pn.MaPhieuNhap
      WHERE ct.MaHang = h.MaHang AND pn.MaKho = 1 AND pn.TrangThai = 'DA_XAC_NHAN'
  );

IF EXISTS (SELECT 1 FROM @newItems)
BEGIN
    INSERT INTO PhieuNhap (MaKho, MaDonViCapHang, MaNVLap, MaNVXacNhan, NgayLap, NgayXacNhan, LoaiPhieuNhap, TrangThai, GhiChu)
    VALUES (1, 1, 1, 1, GETDATE(), GETDATE(), N'DAU_KY', N'DA_XAC_NHAN', N'Khởi tạo tồn kho ban đầu cho các mặt hàng demo bổ sung');

    DECLARE @pnId INT = SCOPE_IDENTITY();

    INSERT INTO ChiTietPhieuNhap (MaPhieuNhap, MaHang, SoLuong, DonGiaNhap)
    SELECT @pnId, MaHang, SoLuong, GiaNhap FROM @newItems;

    PRINT N'   [10/10] Đã sinh phiếu nhập kho đầu kỳ mã PN' + CAST(@pnId AS NVARCHAR) + N' cấp tồn cho các mặt hàng demo mới.';
END
ELSE
BEGIN
    PRINT N'   [10/10] Toàn bộ mặt hàng đã có sẵn tồn kho hợp lệ.';
END

PRINT N'==> HOÀN TẤT NẠP DỮ LIỆU MẪU THÀNH CÔNG!';
PRINT N'==> Bạn có thể đăng nhập ngay với:';
PRINT N'    - quanly  / 123456';
PRINT N'    - thukho  / 123456';
PRINT N'    - nvkho   / 123456';
GO
