-- =========================================================================================
-- FILE CẬP NHẬT CƠ SỞ DỮ LIỆU: docs/database_update.sql
-- Dự án: Quản lý kho hàng nhiều kho (QuanLyKhoPhuQuocLTW)
-- Ngày thực hiện: 2026-10-13
-- Tuân thủ: 3 Nguyên tắc vàng trong .agent/rules/05-co-so-du-lieu.md ("Chỉ cộng, không phá")
-- =========================================================================================

USE QuanLyKhoPhuQuocLTW;
GO

PRINT N'=== BẮT ĐẦU NÂNG CẤP CƠ SỞ DỮ LIỆU QUANLYKHOPHUQUOCLTW ===';
GO

-- -----------------------------------------------------------------------------------------
-- 1. BẢNG HANGHOA: Bổ sung cột GiaVon (Giá vốn sản phẩm)
-- Lý do: Phục vụ tính giá trị tồn kho, định giá hàng hóa và chênh lệch kiểm kê chính xác
-- -----------------------------------------------------------------------------------------
IF NOT EXISTS (SELECT 1 FROM sys.columns WHERE object_id = OBJECT_ID(N'HangHoa') AND name = N'GiaVon')
BEGIN
    ALTER TABLE HangHoa ADD GiaVon DECIMAL(18, 2) NOT NULL CONSTRAINT DF_HangHoa_GiaVon DEFAULT (0);
    PRINT N'[OK] Đã thêm cột GiaVon vào bảng HangHoa (DEFAULT 0).';
END
ELSE
BEGIN
    PRINT N'[SKIP] Cột GiaVon đã tồn tại trong bảng HangHoa.';
END
GO

-- -----------------------------------------------------------------------------------------
-- 2. BẢNG PHIEUXUAT & CHITIETPHIEUXUAT: Mở rộng loại phiếu xuất & liên kết kho trực tiếp
-- Lý do: Cho phép xuất nội bộ, xuất hủy, xuất trả NCC mà không phụ thuộc vào DonBan
-- -----------------------------------------------------------------------------------------
-- 2.1. Bổ sung cột LoaiPhieuXuat vào PhieuXuat (XUAT_BAN, XUAT_NOI_BO, XUAT_HUY, XUAT_TRA_NCC)
IF NOT EXISTS (SELECT 1 FROM sys.columns WHERE object_id = OBJECT_ID(N'PhieuXuat') AND name = N'LoaiPhieuXuat')
BEGIN
    ALTER TABLE PhieuXuat ADD LoaiPhieuXuat VARCHAR(30) NOT NULL CONSTRAINT DF_PhieuXuat_Loai DEFAULT ('XUAT_BAN');
    PRINT N'[OK] Đã thêm cột LoaiPhieuXuat vào bảng PhieuXuat (DEFAULT XUAT_BAN).';
END
ELSE
BEGIN
    PRINT N'[SKIP] Cột LoaiPhieuXuat đã tồn tại trong bảng PhieuXuat.';
END
GO

-- 2.2. Bổ sung cột MaKho vào PhieuXuat (để biết xuất từ kho nào khi không có DonBan)
IF NOT EXISTS (SELECT 1 FROM sys.columns WHERE object_id = OBJECT_ID(N'PhieuXuat') AND name = N'MaKho')
BEGIN
    ALTER TABLE PhieuXuat ADD MaKho INT NULL;
    PRINT N'[OK] Đã thêm cột MaKho vào bảng PhieuXuat.';
END
ELSE
BEGIN
    PRINT N'[SKIP] Cột MaKho đã tồn tại trong bảng PhieuXuat.';
END
GO

-- Đồng bộ MaKho cho các phiếu xuất hiện tại từ bảng DonBan
UPDATE px
SET px.MaKho = db.MaKho
FROM PhieuXuat px
JOIN DonBan db ON db.MaDonBan = px.MaDonBan
WHERE px.MaKho IS NULL;
PRINT N'[OK] Đã đồng bộ MaKho từ DonBan cho các PhieuXuat hiện có.';
GO

-- Tạo khóa ngoại MaKho -> Kho(MaKho) nếu chưa có
IF NOT EXISTS (SELECT 1 FROM sys.foreign_keys WHERE name = N'FK_PhieuXuat_Kho')
BEGIN
    ALTER TABLE PhieuXuat ADD CONSTRAINT FK_PhieuXuat_Kho FOREIGN KEY (MaKho) REFERENCES Kho(MaKho);
    PRINT N'[OK] Đã tạo khóa ngoại FK_PhieuXuat_Kho.';
END
GO

-- 2.3. Nới lỏng MaDonBan trong PhieuXuat cho phép NULL (để xuất nội bộ/hủy không cần đơn bán)
IF EXISTS (SELECT 1 FROM sys.columns WHERE object_id = OBJECT_ID(N'PhieuXuat') AND name = N'MaDonBan' AND is_nullable = 0)
BEGIN
    ALTER TABLE PhieuXuat ALTER COLUMN MaDonBan INT NULL;
    PRINT N'[OK] Đã cập nhật MaDonBan trong PhieuXuat cho phép nhận giá trị NULL.';
END
GO

-- 2.4. Mở rộng ChiTietPhieuXuat: Bổ sung MaHang, DonGiaXuat và cho phép MaCTDonBan nhận NULL
IF NOT EXISTS (SELECT 1 FROM sys.columns WHERE object_id = OBJECT_ID(N'ChiTietPhieuXuat') AND name = N'MaHang')
BEGIN
    ALTER TABLE ChiTietPhieuXuat ADD MaHang INT NULL;
    PRINT N'[OK] Đã thêm cột MaHang vào bảng ChiTietPhieuXuat.';
END
ELSE
BEGIN
    PRINT N'[SKIP] Cột MaHang đã tồn tại trong bảng ChiTietPhieuXuat.';
END
GO

IF NOT EXISTS (SELECT 1 FROM sys.columns WHERE object_id = OBJECT_ID(N'ChiTietPhieuXuat') AND name = N'DonGiaXuat')
BEGIN
    ALTER TABLE ChiTietPhieuXuat ADD DonGiaXuat DECIMAL(18, 2) NOT NULL CONSTRAINT DF_CTPX_DonGia DEFAULT (0);
    PRINT N'[OK] Đã thêm cột DonGiaXuat vào bảng ChiTietPhieuXuat (DEFAULT 0).';
END
ELSE
BEGIN
    PRINT N'[SKIP] Cột DonGiaXuat đã tồn tại trong bảng ChiTietPhieuXuat.';
END
GO

-- Đồng bộ MaHang và DonGiaXuat cho dữ liệu ChiTietPhieuXuat hiện có từ ChiTietDonBan
UPDATE ctpx
SET ctpx.MaHang = ctdb.MaHang,
    ctpx.DonGiaXuat = ctdb.DonGiaApDung
FROM ChiTietPhieuXuat ctpx
JOIN ChiTietDonBan ctdb ON ctdb.MaCTDonBan = ctpx.MaCTDonBan
WHERE ctpx.MaHang IS NULL;
PRINT N'[OK] Đã đồng bộ MaHang và DonGiaXuat từ ChiTietDonBan cho ChiTietPhieuXuat.';
GO

-- Tạo khóa ngoại MaHang -> HangHoa(MaHang) nếu chưa có
IF NOT EXISTS (SELECT 1 FROM sys.foreign_keys WHERE name = N'FK_CTPX_HangHoa')
BEGIN
    ALTER TABLE ChiTietPhieuXuat ADD CONSTRAINT FK_CTPX_HangHoa FOREIGN KEY (MaHang) REFERENCES HangHoa(MaHang);
    PRINT N'[OK] Đã tạo khóa ngoại FK_CTPX_HangHoa.';
END
GO

-- Nới lỏng MaCTDonBan cho phép NULL
IF EXISTS (SELECT 1 FROM sys.columns WHERE object_id = OBJECT_ID(N'ChiTietPhieuXuat') AND name = N'MaCTDonBan' AND is_nullable = 0)
BEGIN
    ALTER TABLE ChiTietPhieuXuat ALTER COLUMN MaCTDonBan INT NULL;
    PRINT N'[OK] Đã cập nhật MaCTDonBan trong ChiTietPhieuXuat cho phép nhận giá trị NULL.';
END
GO

-- -----------------------------------------------------------------------------------------
-- 3. BẢNG TAIKHOAN_KHO: Phân quyền làm việc theo từng kho
-- Lý do: Kiểm soát quyền hạn nhân viên truy cập kho, hỗ trợ Warehouse Switcher
-- -----------------------------------------------------------------------------------------
IF NOT EXISTS (SELECT 1 FROM sys.tables WHERE name = N'TaiKhoan_Kho')
BEGIN
    CREATE TABLE TaiKhoan_Kho (
        MaTaiKhoan INT NOT NULL,
        MaKho INT NOT NULL,
        NgayPhanCong DATETIME NOT NULL CONSTRAINT DF_TaiKhoan_Kho_NgayPhanCong DEFAULT (SYSDATETIME()),
        GhiChu NVARCHAR(255) NULL,
        CONSTRAINT PK_TaiKhoan_Kho PRIMARY KEY CLUSTERED (MaTaiKhoan, MaKho),
        CONSTRAINT FK_TaiKhoanKho_TaiKhoan FOREIGN KEY (MaTaiKhoan) REFERENCES TaiKhoan(MaTaiKhoan) ON DELETE CASCADE,
        CONSTRAINT FK_TaiKhoanKho_Kho FOREIGN KEY (MaKho) REFERENCES Kho(MaKho) ON DELETE CASCADE
    );
    PRINT N'[OK] Đã tạo mới bảng phân quyền TaiKhoan_Kho.';

    -- Khởi tạo phân quyền mặc định: Gán toàn bộ tài khoản hiện có quyền truy cập vào tất cả các kho
    INSERT INTO TaiKhoan_Kho (MaTaiKhoan, MaKho, NgayPhanCong, GhiChu)
    SELECT tk.MaTaiKhoan, k.MaKho, SYSDATETIME(), N'Khởi tạo phân quyền mặc định hệ thống'
    FROM TaiKhoan tk
    CROSS JOIN Kho k
    WHERE NOT EXISTS (
        SELECT 1 FROM TaiKhoan_Kho tkk 
        WHERE tkk.MaTaiKhoan = tk.MaTaiKhoan AND tkk.MaKho = k.MaKho
    );
    PRINT N'[OK] Đã phân công quyền truy cập mặc định cho các tài khoản hiện có vào tất cả các kho.';
END
ELSE
BEGIN
    PRINT N'[SKIP] Bảng TaiKhoan_Kho đã tồn tại.';
END
GO

-- -----------------------------------------------------------------------------------------
-- 4. VIEW BIẾN ĐỘNG KHO (vw_BienDongKho) & VIEW TỒN HIỆN TẠI (vw_TonKhoHienTai)
-- Lý do ([ISSUE-01], [ISSUE-03]):
-- 1. vw_BienDongKho: Lưu từng giao dịch kèm ngày tháng, mã chứng từ -> phục vụ Thẻ kho & Báo cáo Nhập-Xuất-Tồn theo kỳ.
-- 2. vw_TonKhoHienTai: Tổng hợp từ vw_BienDongKho, loại bỏ hoàn toàn mệnh đề WHERE COALESCE(...) IS NOT NULL (chống nuốt lỗi âm thầm).
-- -----------------------------------------------------------------------------------------
CREATE OR ALTER VIEW vw_BienDongKho
AS
-- 1. Nhập kho đã xác nhận: Tăng tồn tại MaKho
SELECT CAST(ISNULL(pn.NgayXacNhan, pn.NgayLap) AS DATETIME) AS NgayGiaoDich,
       pn.MaKho,
       ct.MaHang,
       CAST('PHIEU_NHAP' AS VARCHAR(30)) AS LoaiChungTu,
       pn.MaPhieuNhap AS MaChungTu,
       ct.SoLuong AS SoLuongTang,
       CAST(0 AS DECIMAL(18,2)) AS SoLuongGiam
FROM PhieuNhap pn
JOIN ChiTietPhieuNhap ct ON ct.MaPhieuNhap = pn.MaPhieuNhap
WHERE pn.TrangThai = 'DA_XAC_NHAN'

UNION ALL
-- 2. Xuất kho đã xác nhận (Bao gồm xuất bán, xuất nội bộ, xuất hủy): Giảm tồn
SELECT CAST(ISNULL(px.NgayXacNhan, px.NgayLap) AS DATETIME) AS NgayGiaoDich,
       px.MaKho,
       ctpx.MaHang,
       CAST(px.LoaiPhieuXuat AS VARCHAR(30)) AS LoaiChungTu,
       px.MaPhieuXuat AS MaChungTu,
       CAST(0 AS DECIMAL(18,2)) AS SoLuongTang,
       ctpx.SoLuongXuat AS SoLuongGiam
FROM PhieuXuat px
JOIN ChiTietPhieuXuat ctpx ON ctpx.MaPhieuXuat = px.MaPhieuXuat
WHERE px.TrangThai = 'DA_XAC_NHAN'

UNION ALL
-- 3. Chuyển kho đã xuất / đã nhận: Trừ tồn kho nguồn
SELECT CAST(ISNULL(pc.ThoiDiemXuat, pc.NgayLap) AS DATETIME) AS NgayGiaoDich,
       pc.MaKhoNguon AS MaKho,
       ct.MaHang,
       CAST('CHUYEN_XUAT' AS VARCHAR(30)) AS LoaiChungTu,
       pc.MaPhieuChuyen AS MaChungTu,
       CAST(0 AS DECIMAL(18,2)) AS SoLuongTang,
       ct.SoLuong AS SoLuongGiam
FROM PhieuChuyenKho pc
JOIN ChiTietPhieuChuyenKho ct ON ct.MaPhieuChuyen = pc.MaPhieuChuyen
WHERE pc.TrangThai IN ('DA_XUAT', 'DA_NHAN')

UNION ALL
-- 4. Chuyển kho đã nhận: Cộng tồn kho đích
SELECT CAST(ISNULL(pc.ThoiDiemNhan, ISNULL(pc.ThoiDiemXuat, pc.NgayLap)) AS DATETIME) AS NgayGiaoDich,
       pc.MaKhoDich AS MaKho,
       ct.MaHang,
       CAST('CHUYEN_NHAP' AS VARCHAR(30)) AS LoaiChungTu,
       pc.MaPhieuChuyen AS MaChungTu,
       ct.SoLuong AS SoLuongTang,
       CAST(0 AS DECIMAL(18,2)) AS SoLuongGiam
FROM PhieuChuyenKho pc
JOIN ChiTietPhieuChuyenKho ct ON ct.MaPhieuChuyen = pc.MaPhieuChuyen
WHERE pc.TrangThai = 'DA_NHAN'

UNION ALL
-- 5. Cân bằng kiểm kê: Tăng hoặc giảm theo số lượng điều chỉnh
SELECT CAST(ISNULL(pdc.NgayDuyet, pdc.NgayLap) AS DATETIME) AS NgayGiaoDich,
       pkk.MaKho,
       ctpkk.MaHang,
       CAST('KIEM_KE' AS VARCHAR(30)) AS LoaiChungTu,
       pkk.MaPhieuKiemKe AS MaChungTu,
       CASE WHEN ctpdc.SoLuongDieuChinh > 0 THEN ctpdc.SoLuongDieuChinh ELSE CAST(0 AS DECIMAL(18,2)) END AS SoLuongTang,
       CASE WHEN ctpdc.SoLuongDieuChinh < 0 THEN -ctpdc.SoLuongDieuChinh ELSE CAST(0 AS DECIMAL(18,2)) END AS SoLuongGiam
FROM PhieuDieuChinh pdc
JOIN PhieuKiemKe pkk ON pkk.MaPhieuKiemKe = pdc.MaPhieuKiemKe
JOIN ChiTietPhieuDieuChinh ctpdc ON ctpdc.MaPhieuDieuChinh = pdc.MaPhieuDieuChinh
JOIN ChiTietPhieuKiemKe ctpkk ON ctpkk.MaCTKiemKe = ctpdc.MaCTKiemKe
WHERE pdc.TrangThai = 'DA_DUYET'

UNION ALL
-- 6. Trả hàng nhập đã xác nhận: Giảm tồn kho tại MaKho
SELECT CAST(ISNULL(pth.NgayTra, pth.NgayLap) AS DATETIME) AS NgayGiaoDich,
       pth.MaKho,
       ctpth.MaHang,
       CAST('TRA_HANG_NHAP' AS VARCHAR(30)) AS LoaiChungTu,
       pth.MaPhieuTraHang AS MaChungTu,
       CAST(0 AS DECIMAL(18,2)) AS SoLuongTang,
       ctpth.SoLuong AS SoLuongGiam
FROM PhieuTraHangNhap pth
JOIN ChiTietPhieuTraHangNhap ctpth ON ctpth.MaPhieuTraHang = pth.MaPhieuTraHang
WHERE pth.TrangThai = 'DA_TRA_HANG';
GO

-- 4.2. View Tồn kho hiện tại tổng hợp
CREATE OR ALTER VIEW vw_TonKhoHienTai
AS
SELECT MaKho,
       MaHang,
       CAST(SUM(SoLuongTang - SoLuongGiam) AS DECIMAL(18,2)) AS SoLuongTon
FROM vw_BienDongKho
GROUP BY MaKho, MaHang;
GO

-- -----------------------------------------------------------------------------------------
-- 5. BẢNG HANGHOA: Bổ sung cột DaDanhDau (Đánh dấu sao / Sản phẩm yêu thích)
-- Lý do: Hỗ trợ chức năng đánh dấu sao và lọc danh sách hàng hóa được đánh dấu sao KiotViet
-- -----------------------------------------------------------------------------------------
IF NOT EXISTS (SELECT 1 FROM sys.columns WHERE object_id = OBJECT_ID(N'HangHoa') AND name = N'DaDanhDau')
BEGIN
    ALTER TABLE HangHoa ADD DaDanhDau BIT NOT NULL CONSTRAINT DF_HangHoa_DaDanhDau DEFAULT (0);
    PRINT N'[OK] Đã thêm cột DaDanhDau vào bảng HangHoa (DEFAULT 0).';
END
ELSE
BEGIN
    PRINT N'[SKIP] Cột DaDanhDau đã tồn tại trong bảng HangHoa.';
END
-- -----------------------------------------------------------------------------------------
-- 6. BẢNG CHITIETPHIEUKIEMKE: Bổ sung cột DonGia (Đơn giá chốt tại thời điểm kiểm kho)
-- Lý do ([ISSUE-07]): Đóng băng giá trị chênh lệch kiểm kê tại thời điểm kiểm, không bị nhảy theo giá mới tương lai
-- -----------------------------------------------------------------------------------------
IF NOT EXISTS (SELECT 1 FROM sys.columns WHERE object_id = OBJECT_ID(N'ChiTietPhieuKiemKe') AND name = N'DonGia')
BEGIN
    ALTER TABLE ChiTietPhieuKiemKe ADD DonGia DECIMAL(18, 2) NOT NULL CONSTRAINT DF_CTPKK_DonGia DEFAULT (0);
    PRINT N'[OK] Đã thêm cột DonGia vào bảng ChiTietPhieuKiemKe (DEFAULT 0).';
END
ELSE
BEGIN
    PRINT N'[SKIP] Cột DonGia đã tồn tại trong bảng ChiTietPhieuKiemKe.';
END
GO

-- Đồng bộ đơn giá chốt cho các dòng kiểm kê lịch sử từ GiaVon (hoặc GiaBanThamKhao)
UPDATE ctpkk
SET ctpkk.DonGia = ISNULL(NULLIF(h.GiaVon, 0), h.GiaBanThamKhao)
FROM ChiTietPhieuKiemKe ctpkk
JOIN HangHoa h ON h.MaHang = ctpkk.MaHang
WHERE ctpkk.DonGia = 0;
GO

-- -----------------------------------------------------------------------------------------
-- 7. BẢNG VITRI: Quản lý danh mục vị trí lưu kho
-- Lý do: Quản lý vị trí kệ/dãy/ngăn và nạp danh mục gợi ý khi thêm/sửa hàng hóa (frmHangHoa, frmViTri)
-- -----------------------------------------------------------------------------------------
IF NOT EXISTS (SELECT 1 FROM sys.tables WHERE name = N'ViTri')
BEGIN
    CREATE TABLE ViTri (
        MaViTri INT IDENTITY(1,1) PRIMARY KEY,
        TenViTri NVARCHAR(100) NOT NULL,
        TrangThai VARCHAR(20) NOT NULL CONSTRAINT DF_ViTri_TrangThai DEFAULT ('HOAT_DONG')
    );
    PRINT N'[OK] Đã tạo bảng ViTri.';
END
ELSE
BEGIN
    PRINT N'[SKIP] Bảng ViTri đã tồn tại.';
END
GO

PRINT N'=== HOÀN TẤT NÂNG CẤP CƠ SỞ DỮ LIỆU THÀNH CÔNG ===';
GO

-- -----------------------------------------------------------------------------------------
-- 7. BẢNG NHOMNHACUNGCAP & BỔ SUNG CỘT BẢNG DONVICAPHANG
-- Lý do: Quản lý phân nhóm nhà cung cấp, hồ sơ chi tiết (Mã NCC, Email, MST, Ghi chú) theo chuẩn KiotViet
-- Tuân thủ: "Chỉ cộng, không phá" (Rule 8)
-- -----------------------------------------------------------------------------------------
-- 7.1. Tạo bảng NhomNhaCungCap nếu chưa có
IF NOT EXISTS (SELECT 1 FROM sys.tables WHERE name = N'NhomNhaCungCap')
BEGIN
    CREATE TABLE NhomNhaCungCap (
        MaNhomNcc INT IDENTITY(1,1) PRIMARY KEY,
        TenNhom NVARCHAR(150) NOT NULL,
        GhiChu NVARCHAR(255) NULL
    );
    PRINT N'[OK] Đã tạo mới bảng NhomNhaCungCap.';

    -- Thêm một số nhóm mẫu cơ bản
    INSERT INTO NhomNhaCungCap (TenNhom, GhiChu)
    VALUES 
        (N'Nhà cung cấp Gas & Nhiên liệu', N'Các đơn vị cung ứng khí hóa lỏng, vỏ bình gas'),
        (N'Nhà cung cấp Thiết bị & Phụ kiện', N'Cung cấp van, dây dẫn gas, bếp gas công nghiệp'),
        (N'Nhà cung cấp Khí công nghiệp', N'Cung ứng oxy y tế, nito, argon');
    PRINT N'[OK] Đã thêm nhóm nhà cung cấp mẫu mặc định.';
END
ELSE
BEGIN
    PRINT N'[SKIP] Bảng NhomNhaCungCap đã tồn tại.';
END
GO

-- 7.2. Bổ sung các cột hồ sơ cho bảng DonViCapHang
IF NOT EXISTS (SELECT 1 FROM sys.columns WHERE object_id = OBJECT_ID(N'DonViCapHang') AND name = N'MaNcc')
BEGIN
    ALTER TABLE DonViCapHang ADD MaNcc NVARCHAR(50) NULL;
    PRINT N'[OK] Đã thêm cột MaNcc vào bảng DonViCapHang.';
END
GO

IF NOT EXISTS (SELECT 1 FROM sys.columns WHERE object_id = OBJECT_ID(N'DonViCapHang') AND name = N'Email')
BEGIN
    ALTER TABLE DonViCapHang ADD Email NVARCHAR(150) NULL;
    PRINT N'[OK] Đã thêm cột Email vào bảng DonViCapHang.';
END
GO

IF NOT EXISTS (SELECT 1 FROM sys.columns WHERE object_id = OBJECT_ID(N'DonViCapHang') AND name = N'MaNhomNcc')
BEGIN
    ALTER TABLE DonViCapHang ADD MaNhomNcc INT NULL;
    PRINT N'[OK] Đã thêm cột MaNhomNcc vào bảng DonViCapHang.';
END
GO

IF NOT EXISTS (SELECT 1 FROM sys.columns WHERE object_id = OBJECT_ID(N'DonViCapHang') AND name = N'MaSoThue')
BEGIN
    ALTER TABLE DonViCapHang ADD MaSoThue NVARCHAR(50) NULL;
    PRINT N'[OK] Đã thêm cột MaSoThue vào bảng DonViCapHang.';
END
GO

IF NOT EXISTS (SELECT 1 FROM sys.columns WHERE object_id = OBJECT_ID(N'DonViCapHang') AND name = N'KhuVuc')
BEGIN
    ALTER TABLE DonViCapHang ADD KhuVuc NVARCHAR(150) NULL;
    PRINT N'[OK] Đã thêm cột KhuVuc vào bảng DonViCapHang.';
END
GO

IF NOT EXISTS (SELECT 1 FROM sys.columns WHERE object_id = OBJECT_ID(N'DonViCapHang') AND name = N'GhiChu')
BEGIN
    ALTER TABLE DonViCapHang ADD GhiChu NVARCHAR(500) NULL;
    PRINT N'[OK] Đã thêm cột GhiChu vào bảng DonViCapHang.';
END
GO

IF NOT EXISTS (SELECT 1 FROM sys.columns WHERE object_id = OBJECT_ID(N'DonViCapHang') AND name = N'DaDanhDau')
BEGIN
    ALTER TABLE DonViCapHang ADD DaDanhDau BIT NOT NULL CONSTRAINT DF_DonViCapHang_DaDanhDau DEFAULT (0);
    PRINT N'[OK] Đã thêm cột DaDanhDau vào bảng DonViCapHang (DEFAULT 0).';
END
GO

IF NOT EXISTS (SELECT 1 FROM sys.columns WHERE object_id = OBJECT_ID(N'DonViCapHang') AND name = N'NgayTao')
BEGIN
    ALTER TABLE DonViCapHang ADD NgayTao DATETIME NOT NULL CONSTRAINT DF_DonViCapHang_NgayTao DEFAULT (GETDATE());
    PRINT N'[OK] Đã thêm cột NgayTao vào bảng DonViCapHang (DEFAULT GETDATE()).';
END
GO

-- 7.3. Tạo khóa ngoại FK_DonViCapHang_NhomNcc nếu chưa có
IF NOT EXISTS (SELECT 1 FROM sys.foreign_keys WHERE name = N'FK_DonViCapHang_NhomNcc')
BEGIN
    ALTER TABLE DonViCapHang ADD CONSTRAINT FK_DonViCapHang_NhomNcc FOREIGN KEY (MaNhomNcc) REFERENCES NhomNhaCungCap(MaNhomNcc);
    PRINT N'[OK] Đã tạo khóa ngoại FK_DonViCapHang_NhomNcc.';
END
GO

-- 7.4. Đồng bộ MaNcc tự động chuẩn định dạng NCC00001 cho các NCC đã có
UPDATE DonViCapHang
SET MaNcc = 'NCC' + RIGHT('00000' + CAST(MaDonViCapHang AS VARCHAR(10)), 5)
WHERE MaNcc IS NULL OR LTRIM(RTRIM(MaNcc)) = '';
PRINT N'[OK] Đã chuẩn hóa mã định danh NCC (NCC0000X) cho dữ liệu DonViCapHang hiện có.';
GO


-- -----------------------------------------------------------------------------------------
-- 8. BẢNG PHIEUTRAHANGNHAP & CHITIETPHIEUTRAHANGNHAP: Quản lý trả hàng nhập cho NCC
-- Lý do: Phục vụ chức năng Trả hàng nhập, tự động giảm tồn kho và khấu trừ công nợ
-- -----------------------------------------------------------------------------------------
IF NOT EXISTS (SELECT 1 FROM sys.tables WHERE name = N'PhieuTraHangNhap')
BEGIN
    CREATE TABLE PhieuTraHangNhap (
        MaPhieuTraHang INT IDENTITY(1,1) PRIMARY KEY,
        MaPhieu VARCHAR(30) NOT NULL UNIQUE,
        MaKho INT NOT NULL CONSTRAINT FK_PhieuTraHangNhap_Kho REFERENCES Kho(MaKho),
        MaDonViCapHang INT NULL CONSTRAINT FK_PhieuTraHangNhap_NCC REFERENCES DonViCapHang(MaDonViCapHang),
        MaNvlap INT NOT NULL CONSTRAINT FK_PhieuTraHangNhap_NvLap REFERENCES TaiKhoan(MaTaiKhoan),
        MaNvtra INT NULL CONSTRAINT FK_PhieuTraHangNhap_NvTra REFERENCES TaiKhoan(MaTaiKhoan),
        NgayLap DATETIME NOT NULL CONSTRAINT DF_PTHN_NgayLap DEFAULT (GETDATE()),
        NgayTra DATETIME NOT NULL CONSTRAINT DF_PTHN_NgayTra DEFAULT (GETDATE()),
        TrangThai VARCHAR(20) NOT NULL CONSTRAINT DF_PTHN_TrangThai DEFAULT ('NHAP')
            CONSTRAINT CK_PTHN_TrangThai CHECK (TrangThai IN ('NHAP', 'DA_TRA_HANG', 'HUY')),
        TongSoLuong DECIMAL(18,2) NOT NULL CONSTRAINT DF_PTHN_TongSL DEFAULT (0),
        TongTienHang DECIMAL(18,2) NOT NULL CONSTRAINT DF_PTHN_TongTien DEFAULT (0),
        GiamGia DECIMAL(18,2) NOT NULL CONSTRAINT DF_PTHN_GiamGia DEFAULT (0),
        CanThuNcc DECIMAL(18,2) NOT NULL CONSTRAINT DF_PTHN_CanThu DEFAULT (0),
        DaThuNcc DECIMAL(18,2) NOT NULL CONSTRAINT DF_PTHN_DaThu DEFAULT (0),
        GhiChu NVARCHAR(500) NULL,
        DaDanhDau BIT NOT NULL CONSTRAINT DF_PTHN_DaDanhDau DEFAULT (0)
    );
    PRINT N'[OK] Đã tạo mới bảng PhieuTraHangNhap.';
END
ELSE
BEGIN
    PRINT N'[SKIP] Bảng PhieuTraHangNhap đã tồn tại.';
END
GO

IF NOT EXISTS (SELECT 1 FROM sys.tables WHERE name = N'ChiTietPhieuTraHangNhap')
BEGIN
    CREATE TABLE ChiTietPhieuTraHangNhap (
        MaCtphieuTra INT IDENTITY(1,1) PRIMARY KEY,
        MaPhieuTraHang INT NOT NULL CONSTRAINT FK_CTPTHN_PhieuTra REFERENCES PhieuTraHangNhap(MaPhieuTraHang) ON DELETE CASCADE,
        MaHang INT NOT NULL CONSTRAINT FK_CTPTHN_HangHoa REFERENCES HangHoa(MaHang),
        SoLuong DECIMAL(18,2) NOT NULL CONSTRAINT CK_CTPTHN_SoLuong CHECK (SoLuong > 0),
        GiaVon DECIMAL(18,2) NOT NULL CONSTRAINT DF_CTPTHN_GiaVon DEFAULT (0),
        DonGiaTra DECIMAL(18,2) NOT NULL CONSTRAINT DF_CTPTHN_DonGiaTra DEFAULT (0),
        ThanhTien DECIMAL(18,2) NOT NULL CONSTRAINT DF_CTPTHN_ThanhTien DEFAULT (0),
        GhiChu NVARCHAR(255) NULL
    );
    PRINT N'[OK] Đã tạo mới bảng ChiTietPhieuTraHangNhap.';
END
ELSE
BEGIN
    PRINT N'[SKIP] Bảng ChiTietPhieuTraHangNhap đã tồn tại.';
END
GO


-- 8.1. BẢNG PHIEUTRAHANGNHAP: Bổ sung cột MaPhieuNhap (Tham chiếu đơn nhập gốc nếu có)
IF NOT EXISTS (
    SELECT 1 FROM sys.columns 
    WHERE object_id = OBJECT_ID(N'PhieuTraHangNhap') AND name = N'MaPhieuNhap'
)
BEGIN
    ALTER TABLE PhieuTraHangNhap ADD MaPhieuNhap INT NULL 
        CONSTRAINT FK_PhieuTraHangNhap_PhieuNhap REFERENCES PhieuNhap(MaPhieuNhap);
    PRINT N'[OK] Đã thêm cột MaPhieuNhap vào bảng PhieuTraHangNhap.';
END
GO

