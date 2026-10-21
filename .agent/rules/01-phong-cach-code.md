# 01. Phong cách code & Quy ước lập trình WinForms

## 1. Ngôn ngữ & Quy ước đặt tên

### 1.1. Tên lớp, phương thức & biến (C#)
- **Tên lớp Form / GUI**: Bắt đầu bằng tiền tố `frm`, viết theo PascalCase tiếng Việt không dấu.
  - Ví dụ: `frmMain`, `frmDanhSachHangHoa`, `frmHangHoaChiTiet`, `frmNhapHang`, `frmKiemKho`, `frmChuyenKho`, `frmDangNhap`.
- **Tên lớp nghiệp vụ / DTO**: Viết theo PascalCase tiếng Việt không dấu.
  - Ví dụ: `HangHoa`, `Kho`, `PhieuNhap`, `ChiTietPhieuNhap`, `SessionContext`, `DataProcessing`.
- **Tên phương thức**: PascalCase mô tả hành động rõ ràng.
  - Ví dụ: `LoadData()`, `HienThiDanhSach()`, `KiemTraHopLe()`, `LuuChungTu()`, `LamMoi()`.
- **Tên biến cục bộ & tham số**: camelCase tiếng Việt không dấu hoặc tiếng Anh ngắn gọn.
  - Ví dụ: `dtHangHoa`, `maKho`, `soLuong`, `sqlQuery`, `parameters`.

### 1.2. Quy ước đặt tên WinForms Controls (Nguyên tắc: Tiền tố + Tên theo đúng Text hiển thị)
Mọi control kéo thả trên Form phải được đổi tên bám sát nội dung nhãn/chức năng hiển thị (`Text`) của thành phần đó, viết bằng **tiếng Việt không dấu PascalCase**, bắt đầu bằng tiền tố loại control:

- **Nguyên tắc vàng**: "Text hiển thị tên gì thì tên biến đặt theo đúng chữ đó" (bỏ dấu và ký tự đặc biệt như `+`, `?`, `-`).
  - Nút `+ Hàng hóa` ➜ `btnHangHoa`
  - Nút `Nhập file` ➜ `btnNhapFile`
  - Nút `Xuất file` ➜ `btnXuatFile`
  - Nút `Hiện cột` ➜ `btnHienCot`
  - Nút `?` ➜ `btnHuongDanSuDung` (với các ký hiệu đặc biệt, đặt theo đúng ý nghĩa hành động)
  - Nhãn "Tìm kiếm" / Ô nhập tìm kiếm ➜ `lblTimKiem` / `txtTimKiem`
  - Lưới hiển thị danh sách đối tượng ➜ `dgv` + Tên đối tượng (ví dụ: `dgvHangHoa`, `dgvNhapHang`, `dgvKiemKho`)
  - ComboBox & Label đi kèm ➜ `cbo` / `lbl` + Tên nhãn (ví dụ: `cboNhomHang` / `lblNhomHang`, `cboNhaCungCap` / `lblNhaCungCap`, `cboTrangThai` / `lblTrangThai`)
  - Bộ chọn ngày & Label đi kèm ➜ `dtpTuNgay` / `lblTuNgay`, `dtpDenNgay` / `lblDenNgay`
  - Vùng chứa: GroupBox ➜ `grp...` (ví dụ: `grpBoLoc`, `grpThoiGianTao`), Panel ➜ `pnl...` (ví dụ: `pnlTop`, `pnlBoLoc`), Splitter ➜ `splitter...`

- **Danh mục tiền tố chuẩn**:
  - `btn`: Button
  - `txt`: TextBox
  - `cbo`: ComboBox
  - `dgv`: DataGridView
  - `lbl`: Label
  - `dtp`: DateTimePicker
  - `grp`: GroupBox
  - `pnl`: Panel
  - `splitter`: Splitter
  - `chk`: CheckBox
  - `rdo`: RadioButton
  - `mnu`: ToolStripMenuItem / MenuStrip
  - `tss`: ToolStripStatusLabel
  - `pic`: PictureBox

> [!IMPORTANT]
> **Bảo toàn thiết kế của người dùng**: Khi người dùng tự tay dựng giao diện trên Designer, AI tuyệt đối **GIỮ NGUYÊN 100% GIAO DIỆN** (vị trí `Location`, kích thước `Size`, màu sắc, font, anchor, dock), chỉ hỗ trợ đổi tên biến chuẩn chỉ theo đúng quy tắc trên.

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
   - Form cha MDI [`frmMain.cs`](../../BTL-LTTQ-2/GUI/Forms/frmMain.cs) điều hướng và hiển thị các Form con.

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

### 3.2. Quản lý Mở Form con trong MDI Form (`frmMain`)
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
