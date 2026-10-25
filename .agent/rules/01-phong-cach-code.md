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

### 1.3. Tiêu chuẩn Ngôn ngữ & Màu sắc Nút bấm / Chức năng
- **100% Tiếng Việt có dấu trên giao diện**: Tất cả các nút (`Button`), Menu, ToolStrip, GroupBox, Label và thông báo bắt buộc phải đặt hiển thị bằng tiếng Việt có dấu. Tuyệt đối không để sót các từ tiếng Anh (ví dụ: nút chức năng nhập file bắt buộc là `Nhập file` thay vì `Import file`; nút xuất file là `Xuất file` thay vì `Export file`).
- **Không tự ý thêm màu sắc cho nút và chức năng**: Mọi nút bấm và chức năng thao tác mặc định phải sử dụng giao diện WinForms chuẩn (`UseVisualStyleBackColor = true`), không tự ý gán màu nền (`BackColor`), màu viền (`FlatAppearance`), hay màu chữ (`ForeColor`) khi người dùng chưa yêu cầu hoặc chỉ định rõ.
- **Comment trong code**: Tiếng Việt ngắn gọn, giải thích từng bước logic xử lý (ví dụ: `// 1. Kiểm tra dữ liệu đầu vào`, `// 2. Mở giao dịch lưu phiếu và chi tiết`).
- **Thông báo người dùng**: 100% tiếng Việt có dấu, văn phong lịch sự, rõ ràng (ví dụ: `Vui lòng nhập tên hàng hóa.`, `Lưu chứng từ thành công!`, `Hàng hóa đã phát sinh giao dịch, không thể xóa.`).

---

## 2. Cấu trúc Dự án

Hệ thống được tổ chức tinh gọn, trực quan và dễ quản lý:
1. **`Classes` (Thư mục Lớp đối tượng & Tiện ích dùng chung)**:
   - [`DataProcessing.cs`](../../BTL-LTTQ-2/Classes/DataProcessing.cs): Lớp tiện ích ADO.NET thuần quản lý kết nối và thực thi truy vấn tới CSDL SQL Server bằng `SqlCommand`, `SqlDataAdapter`, `SqlParameter`, `SqlTransaction`.
   - [`SessionContext.cs`](../../BTL-LTTQ-2/Classes/SessionContext.cs): Quản lý thông tin phiên đăng nhập và chi nhánh kho đang làm việc hiện tại.
   - [`ComboboxHelper.cs`](../../BTL-LTTQ-2/Classes/ComboboxHelper.cs): Tiện ích tìm kiếm trực tiếp trong ComboBox (hỗ trợ tiếng Việt không dấu, giới hạn độ cao scrollbar).
2. **`GUI` (Graphical User Interface - Tầng Giao diện người dùng)**:
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


---

## 4. Tiêu chuẩn Mã hóa Tệp tin & Phòng chống Lỗi Font (Mojibake)

- **Chuẩn mã hóa duy nhất toàn dự án**: 100% file mã nguồn (`.cs`, `.Designer.cs`, `.resx`, `.sql`, `.config`, `.md`, `.json`, `.ps1`) bắt buộc phải lưu dưới định dạng **UTF-8 with BOM (Byte Order Mark: `0xEF, 0xBB, 0xBF`)** và ngắt dòng kiểu Windows `CRLF`.
- **Cấu hình C# Compiler**: Thuộc tính `<CodePage>65001</CodePage>` trong `BTL-LTTQ-2.csproj` bảo đảm Roslyn compiler (`csc.exe`) xử lý toàn bộ chuỗi ký tự theo UTF-8.
- **Cấu hình Editor & Git**: File `.editorconfig` (`charset = utf-8-bom`, `end_of_line = crlf`) và `.gitattributes` bảo đảm mọi trình soạn thảo (Visual Studio, VS Code...) không làm thay đổi hay mất BOM.
- **Công cụ kiểm tra tự động**: Chạy `powershell -ExecutionPolicy Bypass -File tools/KiemTraMaHoa.ps1` vào cuối mỗi phiên để tự động quét 100% tệp tin, bảo đảm không có file nào thiếu BOM và 0 lỗi Mojibake.
