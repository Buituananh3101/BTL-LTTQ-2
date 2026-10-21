USE [QuanLyKhoPhuQuocLTW];
GO

SET NOCOUNT ON;

BEGIN TRANSACTION;

-- 1. SỬA LẠI TÊN VÀ ĐỊA CHỈ TRONG BẢNG KHO
UPDATE Kho SET TenKho = N'Kho trung tâm', DiaChi = N'Dương Đông, Phú Quốc', MoTa = N'Kho tổng phân phối chính toàn đảo' WHERE MaKho = 1;
UPDATE Kho SET TenKho = N'Kho An Thới', DiaChi = N'An Thới, Phú Quốc', MoTa = N'Kho trung chuyển chi nhánh phía Nam' WHERE MaKho = 2;
UPDATE Kho SET TenKho = N'Kho phụ kiện Tuna', DiaChi = N'Dương Đông, Phú Quốc', MoTa = N'Kho chuyên chứa vật tư và phụ kiện ngành gas' WHERE MaKho = 3;

-- 2. ĐỒNG BỘ ĐƠN VỊ TÍNH CHUẨN
UPDATE DonViTinh SET TenDvt = N'Bình' WHERE MaDvt = 1;
UPDATE DonViTinh SET TenDvt = N'Cái' WHERE MaDvt = 2;
UPDATE DonViTinh SET TenDvt = N'Bao' WHERE MaDvt = 3;
UPDATE DonViTinh SET TenDvt = N'Mét' WHERE MaDvt = 4;

-- Chuyển các hàng hóa trỏ vào ĐVT lỗi về ĐVT chuẩn
UPDATE HangHoa SET MaDvt = 1 WHERE MaDvt IN (SELECT MaDvt FROM DonViTinh WHERE TenDvt LIKE N'%B%nh%' AND MaDvt <> 1);
UPDATE HangHoa SET MaDvt = 2 WHERE MaDvt IN (SELECT MaDvt FROM DonViTinh WHERE (TenDvt LIKE N'%C%i%' OR TenDvt = N'CĂ¡i') AND MaDvt <> 2);
UPDATE HangHoa SET MaDvt = 4 WHERE MaDvt IN (SELECT MaDvt FROM DonViTinh WHERE (TenDvt LIKE N'%M%t%' OR TenDvt = N'MĂ©t') AND MaDvt <> 4);

-- Xóa các ĐVT lỗi
DELETE FROM DonViTinh WHERE MaDvt > 4;

-- Thêm các ĐVT mở rộng chuẩn UTF-8
IF NOT EXISTS (SELECT 1 FROM DonViTinh WHERE TenDvt = N'Cuộn') INSERT INTO DonViTinh (TenDvt) VALUES (N'Cuộn');
IF NOT EXISTS (SELECT 1 FROM DonViTinh WHERE TenDvt = N'Hộp') INSERT INTO DonViTinh (TenDvt) VALUES (N'Hộp');
IF NOT EXISTS (SELECT 1 FROM DonViTinh WHERE TenDvt = N'Bộ') INSERT INTO DonViTinh (TenDvt) VALUES (N'Bộ');

-- 3. ĐỒNG BỘ NHÓM HÀNG CHUẨN
UPDATE NhomHang SET TenNhom = N'Bình gas' WHERE MaNhom = 1;
UPDATE NhomHang SET TenNhom = N'Phụ kiện gas' WHERE MaNhom = 2;
UPDATE NhomHang SET TenNhom = N'Than' WHERE MaNhom = 3;
UPDATE NhomHang SET TenNhom = N'Bình oxy' WHERE MaNhom = 4;
UPDATE NhomHang SET TenNhom = N'Thiết bị an toàn' WHERE MaNhom = 5;

-- Chuyển các hàng hóa trỏ vào Nhóm lỗi về Nhóm chuẩn
UPDATE HangHoa SET MaNhom = 1 WHERE MaNhom IN (SELECT MaNhom FROM NhomHang WHERE TenNhom LIKE N'%B%nh gas%' AND MaNhom <> 1);
UPDATE HangHoa SET MaNhom = 2 WHERE MaNhom IN (SELECT MaNhom FROM NhomHang WHERE TenNhom LIKE N'%Ph% ki%n gas%' AND MaNhom <> 2);
UPDATE HangHoa SET MaNhom = 4 WHERE MaNhom IN (SELECT MaNhom FROM NhomHang WHERE TenNhom LIKE N'%B%nh oxy%' AND MaNhom <> 4);
UPDATE HangHoa SET MaNhom = 5 WHERE MaNhom IN (SELECT MaNhom FROM NhomHang WHERE (TenNhom LIKE N'%Thi%t b% an to%n%' OR TenNhom LIKE N'%Th% nghi%m%') AND MaNhom <> 5);

-- Xóa các Nhóm lỗi
DELETE FROM NhomHang WHERE MaNhom > 5;

-- 4. ĐỒNG BỘ NHÀ CUNG CẤP & KHÁCH HÀNG
UPDATE DonViCapHang SET TenDonVi = N'Công ty Gas Kiên Giang', DiaChi = N'Rạch Giá, Kiên Giang' WHERE MaDonViCapHang = 1;
UPDATE DonViCapHang SET TenDonVi = N'Công ty Phụ kiện Việt', DiaChi = N'TP. Hồ Chí Minh' WHERE MaDonViCapHang = 2;
UPDATE DonViCapHang SET TenDonVi = N'Công ty Khí công nghiệp Pharmedic', DiaChi = N'Phú Quốc, Kiên Giang' WHERE MaDonViCapHang = 3;

-- Xóa các nhà cung cấp mojibake thừa (nếu không có ràng buộc phiếu nhập)
DELETE FROM DonViCapHang WHERE TenDonVi LIKE N'%CĂ´ng ty%' AND MaDonViCapHang NOT IN (SELECT DISTINCT MaDonViCapHang FROM PhieuNhap WHERE MaDonViCapHang IS NOT NULL);

UPDATE KhachHang SET TenKH = N'Cửa hàng Minh Anh', DiaChi = N'Cửa Dương, Phú Quốc', LoaiKhachHang = 'CUA_HANG' WHERE MaKH = 1;
UPDATE KhachHang SET TenKH = N'Khách sạn Sunset Phú Quốc', DiaChi = N'Dương Đông, Phú Quốc', LoaiKhachHang = 'CUA_HANG' WHERE MaKH = 2;
DELETE FROM KhachHang WHERE MaKH > 2;

-- 5. SỬA TÊN HÀNG HÓA CHUẨN TIẾNG VIỆT
UPDATE HangHoa SET TenHang = N'Bình gas 12kg', MaNhom = 1, MaDvt = 1, GiaBanThamKhao = 420000, GiaVon = 350000 WHERE MaHang = 1;
UPDATE HangHoa SET TenHang = N'Bếp gas đơn Rinnai', MaNhom = 2, MaDvt = 2, GiaBanThamKhao = 350000, GiaVon = 280000 WHERE MaHang = 2;
UPDATE HangHoa SET TenHang = N'Ống dẫn gas chịu áp', MaNhom = 2, MaDvt = 4, GiaBanThamKhao = 25000, GiaVon = 15000 WHERE MaHang = 3;
UPDATE HangHoa SET TenHang = N'Than sạch không khói', MaNhom = 3, MaDvt = 3, GiaBanThamKhao = 70000, GiaVon = 45000 WHERE MaHang = 4;
UPDATE HangHoa SET TenHang = N'Bình oxy y tế 40 lít', MaNhom = 4, MaDvt = 1, GiaBanThamKhao = 850000, GiaVon = 650000 WHERE MaHang = 5;

UPDATE HangHoa SET TenHang = N'Bình gas công nghiệp 45kg', MaNhom = 1, MaDvt = 1, GiaBanThamKhao = 1450000, GiaVon = 1180000 WHERE MaHangNoiBo = N'GAS45';
UPDATE HangHoa SET TenHang = N'Bếp gas đôi Paloma cao cấp', MaNhom = 2, MaDvt = 2, GiaBanThamKhao = 1200000, GiaVon = 950000 WHERE MaHangNoiBo = N'BEP-DOI';
UPDATE HangHoa SET TenHang = N'Van điều áp ngắt gas tự động Namilux', MaNhom = 2, MaDvt = 2, GiaBanThamKhao = 185000, GiaVon = 130000 WHERE MaHangNoiBo = N'VAN-01';
UPDATE HangHoa SET TenHang = N'Thiết bị báo động rò rỉ khí gas', MaNhom = 5, MaDvt = 2, GiaBanThamKhao = 320000, GiaVon = 220000 WHERE MaHangNoiBo = N'CB-GAS';
UPDATE HangHoa SET TenHang = N'Bình oxy cầm tay mini 1000ml', MaNhom = 4, MaDvt = 1, GiaBanThamKhao = 150000, GiaVon = 95000 WHERE MaHangNoiBo = N'OXI-MINI';

COMMIT TRANSACTION;
PRINT N'==> ĐÃ KHẮC PHỤC 100% LỖI ENCODING VÀ CHUẨN HÓA DỮ LIỆU TIẾNG VIỆT THÀNH CÔNG!';
GO
