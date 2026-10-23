# 05. Cơ sở dữ liệu & Thao tác ADO.NET thuần

## 1. Môi trường CSDL & Cấu hình Kết nối

- **Hệ quản trị CSDL**: Microsoft SQL Server.
- **Tên CSDL**: `QuanLyKhoPhuQuocLTW`.
- **Chuỗi kết nối**: Khai báo tập trung trong [`App.config`](../../BTL-LTTQ-2/App.config):
  ```xml
  <connectionStrings>
      <add name="QuanLyKhoConnection" 
           connectionString="Server=localhost\SQLEXPRESS;Database=QuanLyKhoPhuQuocLTW;Integrated Security=True;TrustServerCertificate=True;MultipleActiveResultSets=True" 
           providerName="System.Data.SqlClient" />
  </connectionStrings>
  ```
- **Lớp truy xuất dữ liệu**: [`Classes/DataProcessing.cs`](../../BTL-LTTQ-2/Classes/DataProcessing.cs).

---

## 2. 3 "Nguyên tắc vàng" khi thao tác CSDL

Khi hệ thống cần thêm tính năng mới dẫn đến thay đổi CSDL, AI **bắt buộc tuân thủ 3 nguyên tắc sau**:

### 🌟 Nguyên tắc 1: "Chỉ cộng, không phá" (Additive only)
- Chỉ được phép **thêm bảng mới** (`CREATE TABLE`) hoặc **thêm cột mới** (`ALTER TABLE ADD ... NULL / DEFAULT`).
- **Tuyệt đối KHÔNG** xóa bảng (`DROP TABLE`), không xóa cột (`DROP COLUMN`) và không đổi tên cột cũ đang có dữ liệu để tránh làm hỏng dữ liệu mẫu và các truy vấn hiện hữu.

### 🌟 Nguyên tắc 2: "Đồng bộ tức thì & Quản lý Script"
- Mọi câu lệnh DDL thay đổi cấu trúc bảng, thêm View, thêm Trigger phải được lưu lại vào thư mục `docs/database_update.sql`.
- Người dùng chỉ cần mở SSMS chạy script là CSDL đồng bộ 100% với Code.

### 🌟 Nguyên tắc 3: "Báo cáo trước khi chạm CSDL"
- Trước khi thêm cột hoặc bảng mới, AI phải giải thích rõ ràng trong phiên:
  - Cần thêm bảng/cột gì?
  - Lý do nghiệp vụ tại sao cần thêm?
  - Ảnh hưởng thế nào đến các bảng và View cũ?
- Chờ người dùng đồng ý mới tiến hành sửa code và chạy script.

---

## 3. Tiêu chuẩn Thao tác ADO.NET qua `DataProcessing`

Lớp `DataProcessing` cung cấp 4 phương thức nền tảng để giao tiếp với CSDL:

### 3.1. Đọc dữ liệu ra `DataTable` (`ReadData`)
```csharp
string sql = "SELECT MaHang, MaHangNoiBo, TenHang, GiaBanThamKhao FROM HangHoa WHERE TrangThai = @TrangThai";
SqlParameter[] pars = new SqlParameter[]
{
    new SqlParameter("@TrangThai", "DANG_BAN")
};

DataTable dt = DataProcessing.ReadData(sql, pars);
dgvHangHoa.DataSource = dt;
```

### 3.2. Cập nhật dữ liệu INSERT / UPDATE / DELETE (`ChangeData`)
```csharp
string sql = @"UPDATE HangHoa 
               SET TenHang = @TenHang, GiaBanThamKhao = @GiaBan, MaNhom = @MaNhom 
               WHERE MaHang = @MaHang";

SqlParameter[] pars = new SqlParameter[]
{
    new SqlParameter("@TenHang", txtTenHang.Text.Trim()),
    new SqlParameter("@GiaBan", Convert.ToDecimal(txtGiaBan.Text)),
    new SqlParameter("@MaNhom", Convert.ToInt32(cboNhomHang.SelectedValue)),
    new SqlParameter("@MaHang", maHang)
};

int soDong = DataProcessing.ChangeData(sql, pars);
```

### 3.3. Lấy giá trị đơn lẻ (`ReadValue`)
Phục vụ các hàm `COUNT(*)`, `SUM(...)`, `MAX(...)`, `SCOPE_IDENTITY()`:
```csharp
string sql = "SELECT COUNT(*) FROM HangHoa WHERE TrangThai = 'DANG_BAN'";
int tongSoHang = Convert.ToInt32(DataProcessing.ReadValue(sql));
```

### 3.4. Thực thi Giao dịch an toàn (`ChangeDataTransaction`)
Dùng khi lưu một chứng từ kèm nhiều dòng chi tiết. Nếu có bất kỳ dòng nào lỗi, toàn bộ giao dịch được Rollback tự động:
```csharp
DataProcessing.ChangeDataTransaction((conn, trans) =>
{
    // 1. Thêm phiếu chính và lấy MaPhieu tự tăng
    string sqlPhieu = @"INSERT INTO PhieuNhap (MaPhieuNhapNoiBo, MaKho, MaNCC, MaTaiKhoan, NgayLap, TrangThai, TongTien)
                        VALUES (@MaNoiBo, @MaKho, @MaNCC, @MaTK, GETDATE(), 'DA_XAC_NHAN', @TongTien);
                        SELECT SCOPE_IDENTITY();";
    
    SqlCommand cmdPhieu = new SqlCommand(sqlPhieu, conn, trans);
    cmdPhieu.Parameters.AddWithValue("@MaNoiBo", maPhieuNoiBo);
    cmdPhieu.Parameters.AddWithValue("@MaKho", maKho);
    cmdPhieu.Parameters.AddWithValue("@MaNCC", maNcc);
    cmdPhieu.Parameters.AddWithValue("@MaTK", maTaiKhoan);
    cmdPhieu.Parameters.AddWithValue("@TongTien", tongTien);
    int maPhieuNhap = Convert.ToInt32(cmdPhieu.ExecuteScalar());

    // 2. Thêm từng dòng chi tiết
    foreach (var dong in danhSachChiTiet)
    {
        string sqlCT = @"INSERT INTO ChiTietPhieuNhap (MaPhieuNhap, MaHang, SoLuong, DonGiaNhap, ThanhTien)
                         VALUES (@MaPN, @MaHang, @SL, @DonGia, @ThanhTien)";
        SqlCommand cmdCT = new SqlCommand(sqlCT, conn, trans);
        cmdCT.Parameters.AddWithValue("@MaPN", maPhieuNhap);
        cmdCT.Parameters.AddWithValue("@MaHang", dong.MaHang);
        cmdCT.Parameters.AddWithValue("@SL", dong.SoLuong);
        cmdCT.Parameters.AddWithValue("@DonGia", dong.DonGia);
        cmdCT.Parameters.AddWithValue("@ThanhTien", dong.ThanhTien);
        cmdCT.ExecuteNonQuery();
    }
});
```

---

## 4. Bắt buộc phòng chống SQL Injection

> [!CAUTION]
> **TUYỆT ĐỐI KHÔNG** ghép chuỗi SQL trực tiếp với dữ liệu nhập từ giao diện:
> ❌ `string sql = "SELECT * FROM TaiKhoan WHERE TenDangNhap = '" + txtUser.Text + "'";` (CẤM)
> 
> **LUÔN LUÔN** dùng `SqlParameter`:
> ✔️ `string sql = "SELECT * FROM TaiKhoan WHERE TenDangNhap = @User";` kèm `new SqlParameter("@User", txtUser.Text.Trim())` (BẮT BUỘC).

---

## 5. Cáº¥m Ä‘oÃ¡n bá»«a cáº¥u trÃºc CSDL - Báº¯t buá»™c xin quyá»n truy váº¥n SELECT kiá»ƒm tra

> [!IMPORTANT]
> **Quy táº¯c báº¥t biáº¿n khi viáº¿t cÃ¢u lá»‡nh SQL tá»›i CSDL**:
> 1. Khi táº¡o má»›i hoáº·c chá»‰nh sá»­a truy váº¥n tá»›i CSDL trong code, náº¿u **chÆ°a rÃµ 100%** vá» tÃªn báº£ng, thuá»™c tÃ­nh/tÃªn cá»™t, kiá»ƒu dá»¯ liá»‡u, cÃ¡c giÃ¡ trá»‹ cho phÃ©p cá»§a cá»™t (vÃ­ dá»¥: TrangThai dáº¡ng archar hay int, chá»©a 'HOAT_DONG' hay 1, khÃ³a ngoáº¡i trá» sang TaiKhoan(MaTaiKhoan) hay NhanVien(MaNV)):
>    - **Báº®T BUá»˜C** pháº£i xin phÃ©p ngÆ°á»i dÃ¹ng cáº¥p quyá»n cháº¡y truy váº¥n tá»›i CSDL Ä‘á»ƒ kiá»ƒm tra.
>    - **CHá»ˆ ÄÆ¯á»¢C PHÃ‰P** sá»­ dá»¥ng cÃ¡c cÃ¢u lá»‡nh truy váº¥n SELECT (nhÆ° INFORMATION_SCHEMA.COLUMNS, sys.tables, SELECT DISTINCT TrangThai FROM ...).
>    - **NGHIÃŠM Cáº¤M** chá»‰nh sá»­a cáº¥u trÃºc hay dá»¯ liá»‡u database (DROP, ALTER, UPDATE, DELETE) khi chÆ°a Ä‘Æ°á»£c phÃ©p.
> 2. **TUYá»†T Äá»I KHÃ”NG ÄÆ¯á»¢C ÄOÃN Bá»ªA** tÃªn cá»™t hay giÃ¡ trá»‹ tráº¡ng thÃ¡i Ä‘á»ƒ trÃ¡nh phÃ¡t sinh lá»—i thá»i gian cháº¡y (Invalid column name, Conversion failed, v.v.).