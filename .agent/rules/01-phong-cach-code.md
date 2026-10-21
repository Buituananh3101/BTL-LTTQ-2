# 01. Phong cách code & Quy ước lập trình WinForms

## 1. Ngôn ngữ & Quy ước đặt tên

### 1.1. Tên lớp, phương thức & biến (C#)
- **Tên lớp Form / GUI**: Bắt đầu bằng tiền tố `frm` hoặc `Form`, viết theo PascalCase tiếng Việt không dấu.
  - Ví dụ: `FormMain`, `frmDanhSachHangHoa`, `frmHangHoaChiTiet`, `frmNhapHang`, `frmKiemKho`, `frmChuyenKho`, `frmDangNhap`.
- **Tên lớp nghiệp vụ / DTO**: Viết theo PascalCase tiếng Việt không dấu.
  - Ví dụ: `HangHoa`, `Kho`, `PhieuNhap`, `ChiTietPhieuNhap`, `SessionContext`, `DataProcessing`.
- **Tên phương thức**: PascalCase mô tả hành động rõ ràng.
  - Ví dụ: `LoadData()`, `HienThiDanhSach()`, `KiemTraHopLe()`, `LuuChungTu()`, `LamMoi()`.
- **Tên biến cục bộ & tham số**: camelCase tiếng Việt không dấu hoặc tiếng Anh ngắn gọn.
  - Ví dụ: `dtHangHoa`, `maKho`, `soLuong`, `sqlQuery`, `parameters`.

### 1.2. Quy ước đặt tên WinForms Controls (Chuẩn tiền tố)
Mọi control kéo thả trên Form phải được đổi tên có ý nghĩa, bắt đầu bằng tiền tố loại control:
- `btn`: Button (ví dụ: `btnThem`, `btnSua`, `btnXoa`, `btnLuu`, `btnDong`, `btnTimKiem`)
- `txt`: TextBox (ví dụ: `txtMaHang`, `txtTenHang`, `txtTimKiem`, `txtGhiChu`)
- `cbo`: ComboBox (ví dụ: `cboNhomHang`, `cboDonViTinh`, `cboKho`, `cboTrangThai`)
- `dgv`: DataGridView (ví dụ: `dgvHangHoa`, `dgvChiTietPhieu`, `dgvTonKho`)
- `lbl`: Label (ví dụ: `lblTieuDe`, `lblTongTien`, `lblTongSoLuong`)
- `dtp`: DateTimePicker (ví dụ: `dtpNgayLap`, `dtpTuNgay`, `dtpDenNgay`)
- `chk`: CheckBox (ví dụ: `chkHoatDong`, `chkGiaGomThue`)
- `rdo`: RadioButton (ví dụ: `rdoTatCa`, `rdoConHang`, `rdoHetHang`)
- `mnu`: ToolStripMenuItem / MenuStrip (ví dụ: `mnuHeThong`, `mnuHangHoa`, `mnuNhapHang`)
- `tss`: ToolStripStatusLabel (ví dụ: `tssNguoiDung`, `tssKho`, `tssThoiGian`)
- `pic`: PictureBox (ví dụ: `picHinhAnh`)

### 1.3. Comment & Giao diện
- **Comment trong code**: Tiếng Việt ngắn gọn, giải thích từng bước logic xử lý (ví dụ: `// 1. Kiểm tra dữ liệu đầu vào`, `// 2. Mở giao dịch lưu phiếu và chi tiết`).
- **Giao diện & Thông báo người dùng**: 100% **tiếng Việt có dấu**, văn phong lịch sự, rõ ràng (ví dụ: `Vui lòng nhập tên hàng hóa.`, `Lưu chứng từ thành công!`, `Hàng hóa đã phát sinh giao dịch, không thể xóa.`).

---

## 2. Kiến trúc Phân tầng trong Dự án

Hệ thống tuân thủ kiến trúc phân tầng rõ ràng:
1. **`DAL` (Data Access Layer)**:
   - Quản lý truy xuất CSDL SQL Server thông qua lớp tiện ích ADO.NET thuần [`DAL/DataProcessing.cs`](../../BTL-LTTQ-2/DAL/DataProcessing.cs).
   - Thực thi các câu lệnh `SELECT`, `INSERT`, `UPDATE`, `DELETE` bằng `SqlCommand`, `SqlDataAdapter`, `SqlParameter`.
2. **`BLL` (Business Logic Layer) / Dịch vụ**:
   - Chứa logic kiểm tra hợp lệ, tính toán tồn kho, kiểm tra chặn tồn âm, băm mật khẩu SHA-256 (`PasswordHelper`), quản lý phiên (`SessionContext`).
3. **`GUI` (Graphical User Interface)**:
   - Các Form giao diện đặt trong thư mục `GUI/Forms/`.
   - Form cha MDI [`FormMain.cs`](../../BTL-LTTQ-2/GUI/Forms/FormMain.cs) điều hướng và hiển thị các Form con.

---

## 3. Quy chuẩn viết mã cho WinForms Form

### 3.1. Cấu trúc một Form chuẩn
Mỗi Form nghiệp vụ nên có cấu trúc phương thức rõ ràng:
```csharp
public partial class frmDanhSachHangHoa : Form
{
    public frmDanhSachHangHoa()
    {
        InitializeComponent();
    }

    private void frmDanhSachHangHoa_Load(object sender, EventArgs e)
    {
        DinhDangLuoi();
        LoadCombobox();
        HienThiDanhSach();
    }

    private void DinhDangLuoi()
    {
        // Cấu hình hiển thị DataGridView
    }

    private void LoadCombobox()
    {
        // Nạp dữ liệu các dropdown bộ lọc
    }

    private void HienThiDanhSach()
    {
        // Truy vấn dữ liệu qua DataProcessing và bind vào dgv
    }
}
```

### 3.2. Quản lý Mở Form con trong MDI Form (`FormMain`)
Khi click mở một Form con, **luôn kiểm tra xem Form đó đã được mở hay chưa**. Nếu đã mở thì kích hoạt (`Activate()`), không tạo instance mới gây tràn bộ nhớ:
```csharp
private void mnuHangHoa_Click(object sender, EventArgs e)
{
    foreach (Form form in this.MdiChildren)
    {
        if (form is frmDanhSachHangHoa)
        {
            form.Activate();
            return;
        }
    }

    frmDanhSachHangHoa frm = new frmDanhSachHangHoa();
    frm.MdiParent = this;
    frm.Show();
}
```

### 3.3. Xử lý ngoại lệ (Exception Handling)
Mọi thao tác truy xuất CSDL hoặc xử lý nghiệp vụ quan trọng đều phải đặt trong khối `try...catch`:
```csharp
try
{
    // Thao tác CSDL / Logic
}
catch (Exception ex)
{
    MessageBox.Show("Đã xảy ra lỗi: " + ex.Message, "Thông báo lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
}
```
