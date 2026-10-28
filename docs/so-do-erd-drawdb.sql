-- =============================================================================
-- HE THONG QUAN LY KHO HANG PHU QUOC LTTQ - SO DO CSDL (DRAWDB SQL IMPORT)
-- Tong cong: 30 bang, 37 quan he khoa ngoai, chia theo 5 phan he nghiep vu
-- =============================================================================

-- Bang: VaiTro
CREATE TABLE [VaiTro] (
    [MaVaiTro] INT PRIMARY KEY IDENTITY(1,1),
    [TenVaiTro] NVARCHAR(50) NOT NULL,
    [MoTa] NVARCHAR(255)
);

-- Bang: NhanVien
CREATE TABLE [NhanVien] (
    [MaNV] INT PRIMARY KEY IDENTITY(1,1),
    [HoTen] NVARCHAR(100) NOT NULL,
    [DienThoai] VARCHAR(15),
    [Email] VARCHAR(100),
    [DiaChi] NVARCHAR(255),
    [NgayVaoLam] DATE,
    [TrangThai] VARCHAR(20) NOT NULL,
    [GhiChu] NVARCHAR(500)
);

-- Bang: TaiKhoan
CREATE TABLE [TaiKhoan] (
    [MaTaiKhoan] INT PRIMARY KEY IDENTITY(1,1),
    [MaNV] INT NOT NULL,
    [MaVaiTro] INT NOT NULL,
    [TenDangNhap] VARCHAR(50) NOT NULL,
    [MatKhauHash] VARCHAR(255) NOT NULL,
    [TrangThai] VARCHAR(20) NOT NULL,
    [LanDangNhapCuoi] DATETIME2
);

-- Bang: TaiKhoanKho
CREATE TABLE [TaiKhoanKho] (
    [MaTaiKhoan] INT NOT NULL,
    [MaKho] INT NOT NULL,
    [NgayPhanCong] DATETIME NOT NULL,
    [GhiChu] NVARCHAR(255),
    CONSTRAINT [PK_TaiKhoanKho] PRIMARY KEY ([MaTaiKhoan], [MaKho])
);

-- Bang: NhatKyThaoTac
CREATE TABLE [NhatKyThaoTac] (
    [MaNhatKy] BIGINT PRIMARY KEY IDENTITY(1,1),
    [MaTaiKhoan] INT NOT NULL,
    [ThoiDiem] DATETIME2 NOT NULL,
    [ThaoTac] NVARCHAR(100) NOT NULL,
    [LoaiDoiTuong] NVARCHAR(50) NOT NULL,
    [MaDoiTuong] VARCHAR(50) NOT NULL,
    [NoiDung] NVARCHAR(1000)
);

-- Bang: Kho
CREATE TABLE [Kho] (
    [MaKho] INT PRIMARY KEY IDENTITY(1,1),
    [TenKho] NVARCHAR(100) NOT NULL,
    [DiaChi] NVARCHAR(255),
    [MoTa] NVARCHAR(255),
    [TrangThai] VARCHAR(20) NOT NULL
);

-- Bang: DonViTinh
CREATE TABLE [DonViTinh] (
    [MaDVT] INT PRIMARY KEY IDENTITY(1,1),
    [TenDVT] NVARCHAR(50) NOT NULL,
    [MoTa] NVARCHAR(255)
);

-- Bang: NhomHang
CREATE TABLE [NhomHang] (
    [MaNhom] INT PRIMARY KEY IDENTITY(1,1),
    [TenNhom] NVARCHAR(100) NOT NULL,
    [MoTa] NVARCHAR(255),
    [TrangThai] VARCHAR(20) NOT NULL
);

-- Bang: ViTri
CREATE TABLE [ViTri] (
    [MaViTri] INT PRIMARY KEY IDENTITY(1,1),
    [TenViTri] NVARCHAR(100) NOT NULL,
    [TrangThai] VARCHAR(20) NOT NULL
);

-- Bang: HangHoa
CREATE TABLE [HangHoa] (
    [MaHang] INT PRIMARY KEY IDENTITY(1,1),
    [MaNhom] INT NOT NULL,
    [MaDVT] INT NOT NULL,
    [MaHangNoiBo] VARCHAR(30) NOT NULL,
    [TenHang] NVARCHAR(150) NOT NULL,
    [QuyCach] NVARCHAR(255),
    [GiaBanThamKhao] DECIMAL(18,2) NOT NULL,
    [TrangThai] VARCHAR(20) NOT NULL,
    [HinhAnh] VARBINARY,
    [NgayTao] DATETIME,
    [GiaVon] DECIMAL(18,2) NOT NULL,
    [DaDanhDau] BIT NOT NULL,
    [ViTri] NVARCHAR(100),
    [TrongLuong] DECIMAL(18,2),
    [DonViTrongLuong] VARCHAR(10),
    [ThuocTinh] NVARCHAR(MAX),
    [MoTa] NVARCHAR(MAX)
);

-- Bang: HinhAnhHangHoa
CREATE TABLE [HinhAnhHangHoa] (
    [MaHinhAnh] INT PRIMARY KEY IDENTITY(1,1),
    [MaHang] INT NOT NULL,
    [DuLieuAnh] VARBINARY NOT NULL,
    [ThuTu] INT NOT NULL,
    [LaAnhChinh] BIT NOT NULL,
    [NgayTao] DATETIME
);

-- Bang: NguongTonKho
CREATE TABLE [NguongTonKho] (
    [MaKho] INT NOT NULL,
    [MaHang] INT NOT NULL,
    [TonToiThieu] DECIMAL(18,2) NOT NULL,
    [TonCaoNhat] DECIMAL(18,2),
    CONSTRAINT [PK_NguongTonKho] PRIMARY KEY ([MaKho], [MaHang])
);

-- Bang: NhomNhaCungCap
CREATE TABLE [NhomNhaCungCap] (
    [MaNhomNcc] INT PRIMARY KEY IDENTITY(1,1),
    [TenNhom] NVARCHAR(150) NOT NULL,
    [GhiChu] NVARCHAR(255)
);

-- Bang: DonViCapHang
CREATE TABLE [DonViCapHang] (
    [MaDonViCapHang] INT PRIMARY KEY IDENTITY(1,1),
    [TenDonVi] NVARCHAR(150) NOT NULL,
    [DienThoai] VARCHAR(15),
    [DiaChi] NVARCHAR(255),
    [TrangThai] VARCHAR(20) NOT NULL,
    [MaNcc] VARCHAR(30),
    [Email] NVARCHAR(100),
    [KhuVuc] NVARCHAR(100),
    [PhuongXa] NVARCHAR(100),
    [MaNhomNcc] INT,
    [GhiChu] NVARCHAR(500),
    [TenCongTy] NVARCHAR(200),
    [MaSoThue] VARCHAR(30),
    [SoCccd] VARCHAR(30),
    [NgayTao] DATETIME NOT NULL,
    [NguoiTao] NVARCHAR(100),
    [DaDanhDau] BIT NOT NULL
);

-- Bang: PhieuNhap
CREATE TABLE [PhieuNhap] (
    [MaPhieuNhap] INT PRIMARY KEY IDENTITY(1,1),
    [MaKho] INT NOT NULL,
    [MaDonViCapHang] INT,
    [MaNVLap] INT NOT NULL,
    [MaNVXacNhan] INT,
    [NgayLap] DATETIME2 NOT NULL,
    [NgayXacNhan] DATETIME2,
    [LoaiPhieuNhap] VARCHAR(20) NOT NULL,
    [TrangThai] VARCHAR(20) NOT NULL,
    [GhiChu] NVARCHAR(500),
    [MaPhieu] VARCHAR(30),
    [DaDanhDau] BIT NOT NULL,
    [SoHoaDon] VARCHAR(50),
    [GiamGia] DECIMAL(18,2) NOT NULL,
    [TongTien] DECIMAL(18,2) NOT NULL,
    [CanTraNcc] DECIMAL(18,2) NOT NULL,
    [MaDatHangNhap] VARCHAR(30)
);

-- Bang: ChiTietPhieuNhap
CREATE TABLE [ChiTietPhieuNhap] (
    [MaCTPhieuNhap] INT PRIMARY KEY IDENTITY(1,1),
    [MaPhieuNhap] INT NOT NULL,
    [MaHang] INT NOT NULL,
    [SoLuong] DECIMAL(18,2) NOT NULL,
    [DonGiaNhap] DECIMAL(18,2) NOT NULL,
    [GiamGia] DECIMAL(18,2) NOT NULL,
    [ThanhTien] DECIMAL(18,2) NOT NULL
);

-- Bang: PhieuTraHangNhap
CREATE TABLE [PhieuTraHangNhap] (
    [MaPhieuTraHang] INT PRIMARY KEY IDENTITY(1,1),
    [MaPhieu] VARCHAR(30),
    [MaKho] INT NOT NULL,
    [MaDonViCapHang] INT,
    [MaNvlap] INT NOT NULL,
    [MaNvtra] INT,
    [NgayLap] DATETIME NOT NULL,
    [NgayTra] DATETIME NOT NULL,
    [TrangThai] VARCHAR(20) NOT NULL,
    [TongSoLuong] DECIMAL(18,2) NOT NULL,
    [TongTienHang] DECIMAL(18,2) NOT NULL,
    [GiamGia] DECIMAL(18,2) NOT NULL,
    [CanThuNcc] DECIMAL(18,2) NOT NULL,
    [DaThuNcc] DECIMAL(18,2) NOT NULL,
    [GhiChu] NVARCHAR(500),
    [DaDanhDau] BIT NOT NULL
);

-- Bang: ChiTietPhieuTraHangNhap
CREATE TABLE [ChiTietPhieuTraHangNhap] (
    [MaCtphieuTra] INT PRIMARY KEY IDENTITY(1,1),
    [MaPhieuTraHang] INT NOT NULL,
    [MaHang] INT NOT NULL,
    [SoLuong] DECIMAL(18,2) NOT NULL,
    [GiaVon] DECIMAL(18,2) NOT NULL,
    [DonGiaTra] DECIMAL(18,2) NOT NULL,
    [ThanhTien] DECIMAL(18,2) NOT NULL,
    [GhiChu] NVARCHAR(255)
);

-- Bang: PhieuChuyenKho
CREATE TABLE [PhieuChuyenKho] (
    [MaPhieuChuyen] INT PRIMARY KEY IDENTITY(1,1),
    [MaKhoNguon] INT NOT NULL,
    [MaKhoDich] INT NOT NULL,
    [MaNVLap] INT NOT NULL,
    [MaNVXuat] INT,
    [MaNVNhan] INT,
    [NgayLap] DATETIME2 NOT NULL,
    [ThoiDiemXuat] DATETIME2,
    [ThoiDiemNhan] DATETIME2,
    [TrangThai] VARCHAR(20) NOT NULL,
    [GhiChu] NVARCHAR(500),
    [MaPhieu] VARCHAR(30),
    [DaDanhDau] BIT NOT NULL,
    [GhiChuNhan] NVARCHAR(500),
    [TongSoLuongChuyen] DECIMAL(18,2) NOT NULL,
    [TongSoLuongNhan] DECIMAL(18,2) NOT NULL,
    [TongGiaTriChuyen] DECIMAL(18,2) NOT NULL
);

-- Bang: ChiTietPhieuChuyenKho
CREATE TABLE [ChiTietPhieuChuyenKho] (
    [MaCTPhieuChuyen] INT PRIMARY KEY IDENTITY(1,1),
    [MaPhieuChuyen] INT NOT NULL,
    [MaHang] INT NOT NULL,
    [SoLuong] DECIMAL(18,2) NOT NULL,
    [SoLuongNhan] DECIMAL(18,2),
    [DonGiaChuyen] DECIMAL(18,2) NOT NULL,
    [ThanhTien] DECIMAL(18,2) NOT NULL
);

-- Bang: PhieuKiemKe
CREATE TABLE [PhieuKiemKe] (
    [MaPhieuKiemKe] INT PRIMARY KEY IDENTITY(1,1),
    [MaKho] INT NOT NULL,
    [MaNVLap] INT NOT NULL,
    [ThoiDiemChot] DATETIME2 NOT NULL,
    [TrangThai] VARCHAR(20) NOT NULL,
    [GhiChu] NVARCHAR(500),
    [MaPhieu] VARCHAR(30),
    [DaDanhDau] BIT NOT NULL
);

-- Bang: ChiTietPhieuKiemKe
CREATE TABLE [ChiTietPhieuKiemKe] (
    [MaCTKiemKe] INT PRIMARY KEY IDENTITY(1,1),
    [MaPhieuKiemKe] INT NOT NULL,
    [MaHang] INT NOT NULL,
    [SoLuongTheoSo] DECIMAL(18,2) NOT NULL,
    [SoLuongThucTe] DECIMAL(18,2),
    [DonGia] DECIMAL(18,2) NOT NULL
);

-- Bang: PhieuDieuChinh
CREATE TABLE [PhieuDieuChinh] (
    [MaPhieuDieuChinh] INT PRIMARY KEY IDENTITY(1,1),
    [MaPhieuKiemKe] INT NOT NULL,
    [MaNVLap] INT NOT NULL,
    [MaNVDuyet] INT,
    [NgayLap] DATETIME2 NOT NULL,
    [NgayDuyet] DATETIME2,
    [TrangThai] VARCHAR(20) NOT NULL,
    [GhiChu] NVARCHAR(500)
);

-- Bang: ChiTietPhieuDieuChinh
CREATE TABLE [ChiTietPhieuDieuChinh] (
    [MaCTDieuChinh] INT PRIMARY KEY IDENTITY(1,1),
    [MaPhieuDieuChinh] INT NOT NULL,
    [MaCTKiemKe] INT NOT NULL,
    [SoLuongDieuChinh] DECIMAL(18,2) NOT NULL
);

-- Bang: PhieuXuat
CREATE TABLE [PhieuXuat] (
    [MaPhieuXuat] INT PRIMARY KEY IDENTITY(1,1),
    [MaDonBan] INT,
    [MaNVLap] INT NOT NULL,
    [MaNVXacNhan] INT,
    [NgayLap] DATETIME2 NOT NULL,
    [NgayXacNhan] DATETIME2,
    [TrangThai] VARCHAR(20) NOT NULL,
    [GhiChu] NVARCHAR(500),
    [LoaiPhieuXuat] VARCHAR(30) NOT NULL,
    [MaKho] INT NOT NULL,
    [MaPhieu] VARCHAR(30),
    [DaDanhDau] BIT NOT NULL
);

-- Bang: ChiTietPhieuXuat
CREATE TABLE [ChiTietPhieuXuat] (
    [MaCTPhieuXuat] INT PRIMARY KEY IDENTITY(1,1),
    [MaPhieuXuat] INT NOT NULL,
    [MaCTDonBan] INT,
    [SoLuongXuat] DECIMAL(18,2) NOT NULL,
    [MaHang] INT NOT NULL,
    [DonGiaXuat] DECIMAL(18,2) NOT NULL
);

-- Bang: KhachHang
CREATE TABLE [KhachHang] (
    [MaKH] INT PRIMARY KEY IDENTITY(1,1),
    [TenKH] NVARCHAR(150) NOT NULL,
    [DienThoai] VARCHAR(15),
    [DiaChi] NVARCHAR(255),
    [LoaiKhachHang] VARCHAR(20) NOT NULL,
    [TrangThai] VARCHAR(20) NOT NULL
);

-- Bang: DonBan
CREATE TABLE [DonBan] (
    [MaDonBan] INT PRIMARY KEY IDENTITY(1,1),
    [MaKH] INT,
    [MaKho] INT NOT NULL,
    [MaNVLap] INT NOT NULL,
    [NgayLap] DATETIME2 NOT NULL,
    [TrangThai] VARCHAR(20) NOT NULL,
    [GhiChu] NVARCHAR(500)
);

-- Bang: ChiTietDonBan
CREATE TABLE [ChiTietDonBan] (
    [MaCTDonBan] INT PRIMARY KEY IDENTITY(1,1),
    [MaDonBan] INT NOT NULL,
    [MaHang] INT NOT NULL,
    [SoLuong] DECIMAL(18,2) NOT NULL,
    [DonGiaApDung] DECIMAL(18,2) NOT NULL,
    [ChietKhau] DECIMAL(18,2) NOT NULL
);

-- Bang: KhoanThu
CREATE TABLE [KhoanThu] (
    [MaKhoanThu] INT PRIMARY KEY IDENTITY(1,1),
    [MaDonBan] INT NOT NULL,
    [MaNVThu] INT NOT NULL,
    [SoTien] DECIMAL(18,2) NOT NULL,
    [PhuongThuc] VARCHAR(20) NOT NULL,
    [ThoiDiemThu] DATETIME2 NOT NULL,
    [GhiChu] NVARCHAR(500)
);

-- =============================================================================
-- KHOA NGOAI (FOREIGN KEY CONSTRAINTS)
-- =============================================================================

ALTER TABLE [DonBan] ADD CONSTRAINT [FK_CTDB_DonBan] FOREIGN KEY ([MaDonBan]) REFERENCES [ChiTietDonBan] ([MaDonBan]);
ALTER TABLE [DonBan] ADD CONSTRAINT [FK_PhieuXuat_DonBan] FOREIGN KEY ([MaDonBan]) REFERENCES [PhieuXuat] ([MaDonBan]);
ALTER TABLE [DonBan] ADD CONSTRAINT [FK_KhoanThu_DonBan] FOREIGN KEY ([MaDonBan]) REFERENCES [KhoanThu] ([MaDonBan]);
ALTER TABLE [ChiTietDonBan] ADD CONSTRAINT [FK_CTPX_CTDonBan] FOREIGN KEY ([MaCTDonBan]) REFERENCES [ChiTietPhieuXuat] ([MaCTDonBan]);
ALTER TABLE [PhieuTraHangNhap] ADD CONSTRAINT [FK_ChiTietPhieuTra_PhieuTra] FOREIGN KEY ([MaPhieuTraHang]) REFERENCES [ChiTietPhieuTraHangNhap] ([MaPhieuTraHang]);
ALTER TABLE [PhieuXuat] ADD CONSTRAINT [FK_CTPX_PhieuXuat] FOREIGN KEY ([MaPhieuXuat]) REFERENCES [ChiTietPhieuXuat] ([MaPhieuXuat]);
ALTER TABLE [PhieuChuyenKho] ADD CONSTRAINT [FK_CTPCK_PhieuChuyen] FOREIGN KEY ([MaPhieuChuyen]) REFERENCES [ChiTietPhieuChuyenKho] ([MaPhieuChuyen]);
ALTER TABLE [PhieuKiemKe] ADD CONSTRAINT [FK_CTPKK_PhieuKiemKe] FOREIGN KEY ([MaPhieuKiemKe]) REFERENCES [ChiTietPhieuKiemKe] ([MaPhieuKiemKe]);
ALTER TABLE [PhieuKiemKe] ADD CONSTRAINT [FK_PhieuDieuChinh_PhieuKiemKe] FOREIGN KEY ([MaPhieuKiemKe]) REFERENCES [PhieuDieuChinh] ([MaPhieuKiemKe]);
ALTER TABLE [ChiTietPhieuKiemKe] ADD CONSTRAINT [FK_CTPDC_CTKiemKe] FOREIGN KEY ([MaCTKiemKe]) REFERENCES [ChiTietPhieuDieuChinh] ([MaCTKiemKe]);
ALTER TABLE [VaiTro] ADD CONSTRAINT [FK_TaiKhoan_VaiTro] FOREIGN KEY ([MaVaiTro]) REFERENCES [TaiKhoan] ([MaVaiTro]);
ALTER TABLE [NhanVien] ADD CONSTRAINT [FK_PhieuNhap_NVXacNhan] FOREIGN KEY ([MaNV]) REFERENCES [PhieuNhap] ([MaNVXacNhan]);
ALTER TABLE [NhanVien] ADD CONSTRAINT [FK_DonBan_NVLap] FOREIGN KEY ([MaNV]) REFERENCES [DonBan] ([MaNVLap]);
ALTER TABLE [NhanVien] ADD CONSTRAINT [FK_PhieuXuat_NVLap] FOREIGN KEY ([MaNV]) REFERENCES [PhieuXuat] ([MaNVLap]);
ALTER TABLE [NhanVien] ADD CONSTRAINT [FK_PhieuTraHangNhap_NhanVienLap] FOREIGN KEY ([MaNV]) REFERENCES [PhieuTraHangNhap] ([MaNvlap]);
ALTER TABLE [NhanVien] ADD CONSTRAINT [FK_PhieuXuat_NVXacNhan] FOREIGN KEY ([MaNV]) REFERENCES [PhieuXuat] ([MaNVXacNhan]);
ALTER TABLE [NhanVien] ADD CONSTRAINT [FK_PhieuTraHangNhap_NhanVienTra] FOREIGN KEY ([MaNV]) REFERENCES [PhieuTraHangNhap] ([MaNvtra]);
ALTER TABLE [NhanVien] ADD CONSTRAINT [FK_KhoanThu_NhanVien] FOREIGN KEY ([MaNV]) REFERENCES [KhoanThu] ([MaNVThu]);
ALTER TABLE [NhanVien] ADD CONSTRAINT [FK_PhieuChuyen_NVLap] FOREIGN KEY ([MaNV]) REFERENCES [PhieuChuyenKho] ([MaNVLap]);
ALTER TABLE [NhanVien] ADD CONSTRAINT [FK_PhieuChuyen_NVXuat] FOREIGN KEY ([MaNV]) REFERENCES [PhieuChuyenKho] ([MaNVXuat]);
ALTER TABLE [NhanVien] ADD CONSTRAINT [FK_PhieuChuyen_NVNhan] FOREIGN KEY ([MaNV]) REFERENCES [PhieuChuyenKho] ([MaNVNhan]);
ALTER TABLE [NhanVien] ADD CONSTRAINT [FK_PhieuKiemKe_NVLap] FOREIGN KEY ([MaNV]) REFERENCES [PhieuKiemKe] ([MaNVLap]);
ALTER TABLE [NhanVien] ADD CONSTRAINT [FK_PhieuDieuChinh_NVLap] FOREIGN KEY ([MaNV]) REFERENCES [PhieuDieuChinh] ([MaNVLap]);
ALTER TABLE [NhanVien] ADD CONSTRAINT [FK_PhieuDieuChinh_NVDuyet] FOREIGN KEY ([MaNV]) REFERENCES [PhieuDieuChinh] ([MaNVDuyet]);
ALTER TABLE [NhanVien] ADD CONSTRAINT [FK_TaiKhoan_NhanVien] FOREIGN KEY ([MaNV]) REFERENCES [TaiKhoan] ([MaNV]);
ALTER TABLE [NhanVien] ADD CONSTRAINT [FK_PhieuNhap_NVLap] FOREIGN KEY ([MaNV]) REFERENCES [PhieuNhap] ([MaNVLap]);
ALTER TABLE [PhieuDieuChinh] ADD CONSTRAINT [FK_CTPDC_PhieuDieuChinh] FOREIGN KEY ([MaPhieuDieuChinh]) REFERENCES [ChiTietPhieuDieuChinh] ([MaPhieuDieuChinh]);
ALTER TABLE [TaiKhoan] ADD CONSTRAINT [FK_TaiKhoanKho_TaiKhoan] FOREIGN KEY ([MaTaiKhoan]) REFERENCES [TaiKhoanKho] ([MaTaiKhoan]);
ALTER TABLE [TaiKhoan] ADD CONSTRAINT [FK_NhatKy_TaiKhoan] FOREIGN KEY ([MaTaiKhoan]) REFERENCES [NhatKyThaoTac] ([MaTaiKhoan]);
ALTER TABLE [Kho] ADD CONSTRAINT [FK_PhieuXuat_Kho] FOREIGN KEY ([MaKho]) REFERENCES [PhieuXuat] ([MaKho]);
ALTER TABLE [Kho] ADD CONSTRAINT [FK_TaiKhoanKho_Kho] FOREIGN KEY ([MaKho]) REFERENCES [TaiKhoanKho] ([MaKho]);
ALTER TABLE [Kho] ADD CONSTRAINT [FK_DonBan_Kho] FOREIGN KEY ([MaKho]) REFERENCES [DonBan] ([MaKho]);
ALTER TABLE [Kho] ADD CONSTRAINT [FK_PhieuTraHangNhap_Kho] FOREIGN KEY ([MaKho]) REFERENCES [PhieuTraHangNhap] ([MaKho]);
ALTER TABLE [Kho] ADD CONSTRAINT [FK_PhieuChuyen_KhoNguon] FOREIGN KEY ([MaKho]) REFERENCES [PhieuChuyenKho] ([MaKhoNguon]);
ALTER TABLE [Kho] ADD CONSTRAINT [FK_PhieuChuyen_KhoDich] FOREIGN KEY ([MaKho]) REFERENCES [PhieuChuyenKho] ([MaKhoDich]);
ALTER TABLE [Kho] ADD CONSTRAINT [FK_PhieuKiemKe_Kho] FOREIGN KEY ([MaKho]) REFERENCES [PhieuKiemKe] ([MaKho]);
ALTER TABLE [Kho] ADD CONSTRAINT [FK_NguongTonKho_Kho] FOREIGN KEY ([MaKho]) REFERENCES [NguongTonKho] ([MaKho]);
ALTER TABLE [Kho] ADD CONSTRAINT [FK_PhieuNhap_Kho] FOREIGN KEY ([MaKho]) REFERENCES [PhieuNhap] ([MaKho]);
ALTER TABLE [NhomHang] ADD CONSTRAINT [FK_HangHoa_NhomHang] FOREIGN KEY ([MaNhom]) REFERENCES [HangHoa] ([MaNhom]);
ALTER TABLE [DonViTinh] ADD CONSTRAINT [FK_HangHoa_DonViTinh] FOREIGN KEY ([MaDVT]) REFERENCES [HangHoa] ([MaDVT]);
ALTER TABLE [HangHoa] ADD CONSTRAINT [FK_CTPN_HangHoa] FOREIGN KEY ([MaHang]) REFERENCES [ChiTietPhieuNhap] ([MaHang]);
ALTER TABLE [HangHoa] ADD CONSTRAINT [FK_CTPX_HangHoa] FOREIGN KEY ([MaHang]) REFERENCES [ChiTietPhieuXuat] ([MaHang]);
ALTER TABLE [HangHoa] ADD CONSTRAINT [FK_CTDB_HangHoa] FOREIGN KEY ([MaHang]) REFERENCES [ChiTietDonBan] ([MaHang]);
ALTER TABLE [HangHoa] ADD CONSTRAINT [FK_ChiTietPhieuTra_HangHoa] FOREIGN KEY ([MaHang]) REFERENCES [ChiTietPhieuTraHangNhap] ([MaHang]);
ALTER TABLE [HangHoa] ADD CONSTRAINT [FK_CTPCK_HangHoa] FOREIGN KEY ([MaHang]) REFERENCES [ChiTietPhieuChuyenKho] ([MaHang]);
ALTER TABLE [HangHoa] ADD CONSTRAINT [FK_CTPKK_HangHoa] FOREIGN KEY ([MaHang]) REFERENCES [ChiTietPhieuKiemKe] ([MaHang]);
ALTER TABLE [HangHoa] ADD CONSTRAINT [FK_NguongTonKho_HangHoa] FOREIGN KEY ([MaHang]) REFERENCES [NguongTonKho] ([MaHang]);
ALTER TABLE [HangHoa] ADD CONSTRAINT [FK_HinhAnhHangHoa_HangHoa] FOREIGN KEY ([MaHang]) REFERENCES [HinhAnhHangHoa] ([MaHang]);
ALTER TABLE [KhachHang] ADD CONSTRAINT [FK_DonBan_KhachHang] FOREIGN KEY ([MaKH]) REFERENCES [DonBan] ([MaKH]);
ALTER TABLE [DonViCapHang] ADD CONSTRAINT [FK_PhieuTraHangNhap_DonViCapHang] FOREIGN KEY ([MaDonViCapHang]) REFERENCES [PhieuTraHangNhap] ([MaDonViCapHang]);
ALTER TABLE [DonViCapHang] ADD CONSTRAINT [FK_PhieuNhap_NhaCungCap] FOREIGN KEY ([MaDonViCapHang]) REFERENCES [PhieuNhap] ([MaDonViCapHang]);
ALTER TABLE [PhieuNhap] ADD CONSTRAINT [FK_CTPN_PhieuNhap] FOREIGN KEY ([MaPhieuNhap]) REFERENCES [ChiTietPhieuNhap] ([MaPhieuNhap]);
ALTER TABLE [ViTri] ADD CONSTRAINT [FK_HangHoa_ViTri] FOREIGN KEY ([TenViTri]) REFERENCES [HangHoa] ([ViTri]);
