using BTL_LTTQ_2.Classes;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Text;
using System.Windows.Forms;

namespace BTL_LTTQ_2.GUI.Forms.NhapHang
{
    public partial class frmThemPhieuNhap : Form
    {
        private DataTable dtChiTietHang;
        private DataTable dtDanhSachHang;
        private bool isCalculating = false;

        private int trangHienTai = 1;
        private int soDongMoiTrang = 20;
        private int tongSoTrang = 1;

        public frmThemPhieuNhap()
        {
            InitializeComponent();
            KhoiTaoBangChiTiet();
            CauHinhDataGridView();
        }

        private void KhoiTaoBangChiTiet()
        {
            dtChiTietHang = new DataTable();
            dtChiTietHang.Columns.Add("STT", typeof(int));
            dtChiTietHang.Columns.Add("MaHang", typeof(int));
            dtChiTietHang.Columns.Add("MaHangNoiBo", typeof(string));
            dtChiTietHang.Columns.Add("TenHang", typeof(string));
            dtChiTietHang.Columns.Add("TenDVT", typeof(string));
            dtChiTietHang.Columns.Add("SoLuong", typeof(decimal));
            dtChiTietHang.Columns.Add("DonGiaNhap", typeof(decimal));
            dtChiTietHang.Columns.Add("GiamGia", typeof(decimal));
            dtChiTietHang.Columns.Add("ThanhTien", typeof(decimal));
        }

        private void CauHinhDataGridView()
        {
            // Bật DoubleBuffered chống giật lag lưới
            typeof(DataGridView).InvokeMember("DoubleBuffered",
                System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.SetProperty,
                null, dgvChiTietHang, new object[] { true });

            dgvChiTietHang.AutoGenerateColumns = false;
            dgvChiTietHang.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(248, 250, 252);
            dgvChiTietHang.DefaultCellStyle.SelectionBackColor = Color.FromArgb(224, 231, 255);
            dgvChiTietHang.DefaultCellStyle.SelectionForeColor = Color.FromArgb(30, 41, 59);

            colStt.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            colMaHangNoiBo.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            colDonViTinh.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            colSoLuong.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            colDonGia.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            colGiamGia.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            colThanhTien.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;

            colSoLuong.DefaultCellStyle.Format = "N0";
            colDonGia.DefaultCellStyle.Format = "N0";
            colGiamGia.DefaultCellStyle.Format = "N0";
            colThanhTien.DefaultCellStyle.Format = "N0";
            colGiamGia.HeaderText = "Giảm giá (%)";

            colSoLuong.DefaultCellStyle.BackColor = Color.FromArgb(254, 252, 232);
            colSoLuong.ToolTipText = "Nhập số lượng trực tiếp tại đây (nhấn Enter để xác nhận)";

            dgvChiTietHang.EditMode = DataGridViewEditMode.EditOnEnter;
            dgvChiTietHang.EditingControlShowing += dgvChiTietHang_EditingControlShowing;
            dgvChiTietHang.CellEndEdit += dgvChiTietHang_CellEndEdit;
            dgvChiTietHang.CellClick += dgvChiTietHang_CellClick;
            dgvChiTietHang.Leave += (s, e) => { if (dgvChiTietHang.IsCurrentCellInEditMode) dgvChiTietHang.EndEdit(); };

            dgvChiTietHang.DataSource = dtChiTietHang;
        }

        private void frmThemPhieuNhap_Load(object sender, EventArgs e)
        {
            txtMaPhieu.Text = SinhMaPhieuTuDong();
            dtpNgayNhap.Value = DateTime.Now;

            NapDanhSachKho();
            NapDanhSachNhaCungCap();
            NapDanhSachNhanVien();
            NapDanhSachHangHoa();

            cboSoDongMoiTrang.SelectedItem = "20";
            CapNhatPhanTrang();
            CapNhatTongKet();
        }

        private void frmThemPhieuNhap_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                btnBoQua.PerformClick();
            }
        }

        private string SinhMaPhieuTuDong()
        {
            try
            {
                string sql = "SELECT ISNULL(MAX(MaPhieuNhap), 0) + 1 FROM PhieuNhap";
                object res = DataProcessing.ReadValue(sql);
                int nextId = (res != null && res != DBNull.Value) ? Convert.ToInt32(res) : 1;
                return "PN" + nextId.ToString("D6");
            }
            catch
            {
                return "PN" + DateTime.Now.ToString("yyMMddHHmmss");
            }
        }

        private void NapDanhSachKho()
        {
            try
            {
                if (SessionContext.MaKhoHienTai > 0)
                {
                    DataTable dtKhoHienTai = DataProcessing.ReadData("SELECT MaKho, TenKho FROM Kho WHERE MaKho = @MaKho", new SqlParameter[] { new SqlParameter("@MaKho", SessionContext.MaKhoHienTai) });
                    cboKho.DisplayMember = "TenKho";
                    cboKho.ValueMember = "MaKho";
                    cboKho.DataSource = dtKhoHienTai;
                    cboKho.SelectedValue = SessionContext.MaKhoHienTai;
                    cboKho.Enabled = false;
                    return;
                }
                cboKho.Enabled = true;
                string sql = "SELECT MaKho, TenKho FROM Kho WHERE TrangThai = 'HOAT_DONG' OR TrangThai = 'ACTIVE' ORDER BY TenKho ASC";
                DataTable dt = DataProcessing.ReadData(sql);
                cboKho.DisplayMember = "TenKho";
                cboKho.ValueMember = "MaKho";
                cboKho.DataSource = dt;

                if (dt.Rows.Count > 0)
                {
                    cboKho.SelectedIndex = 0;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi nạp danh sách kho: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void NapDanhSachNhaCungCap()
        {
            try
            {
                string sql = "SELECT MaDonViCapHang, TenDonVi FROM DonViCapHang WHERE TrangThai = 'HOAT_DONG' OR TrangThai = 'ACTIVE' OR TrangThai IS NULL ORDER BY TenDonVi ASC";
                DataTable dt = DataProcessing.ReadData(sql);
                ComboboxHelper.CaiDatTimKiem(cboNhaCungCap, dt, "TenDonVi", "MaDonViCapHang");
                if (dt.Rows.Count > 0)
                {
                    cboNhaCungCap.SelectedIndex = 0;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi nạp danh sách nhà cung cấp: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void NapDanhSachNhanVien()
        {
            try
            {
                string sql = @"
                    SELECT 
                        tk.MaTaiKhoan AS MaNhanVien, 
                        ISNULL(nv.HoTen, tk.TenDangNhap) AS HoTen 
                    FROM TaiKhoan tk 
                    LEFT JOIN NhanVien nv ON tk.MaNV = nv.MaNV 
                    WHERE tk.TrangThai = 'HOAT_DONG' OR tk.TrangThai = 'ACTIVE' OR tk.TrangThai IS NULL
                    ORDER BY HoTen ASC";
                DataTable dt = DataProcessing.ReadData(sql);
                ComboboxHelper.CaiDatTimKiem(cboNguoiNhap, dt, "HoTen", "MaNhanVien");

                if (SessionContext.MaNV.HasValue && SessionContext.MaNV.Value > 0)
                {
                    cboNguoiNhap.SelectedValue = SessionContext.MaNV.Value;
                }
                else if (SessionContext.MaTaiKhoan > 0)
                {
                    cboNguoiNhap.SelectedValue = SessionContext.MaTaiKhoan;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi nạp danh sách nhân viên: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void NapDanhSachHangHoa()
        {
            try
            {
                string sql = @"
                    SELECT 
                        h.MaHang, 
                        h.MaHangNoiBo, 
                        h.TenHang, 
                        ISNULL(d.TenDVT, N'Cái') AS TenDVT, 
                        ISNULL(h.GiaVon, 0) AS DonGiaNhap,
                        N'[' + h.MaHangNoiBo + N'] ' + h.TenHang + N' (' + ISNULL(d.TenDVT, N'Cái') + N') [Giá vốn: ' + CAST(CAST(ISNULL(h.GiaVon, 0) AS INT) AS NVARCHAR(15)) + N']' AS HienThi
                    FROM HangHoa h
                    LEFT JOIN DonViTinh d ON h.MaDVT = d.MaDVT
                    WHERE ISNULL(h.TrangThai, '') NOT IN ('NGUNG_BAN', 'NGUNG_KINH_DOANH', 'DA_XOA')
                    ORDER BY h.TenHang ASC";
                dtDanhSachHang = DataProcessing.ReadData(sql);

                ComboboxHelper.CaiDatTimKiem(cboHangHoa, dtDanhSachHang, "HienThi", "MaHang");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi nạp danh sách hàng hóa: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnThemHang_Click(object sender, EventArgs e)
        {
            try
            {
                // 1. Kiểm tra xem cột Số lượng có đang hiển thị trên bảng không
                if (!dgvChiTietHang.Columns.Contains("colSoLuong") || !dgvChiTietHang.Columns["colSoLuong"].Visible)
                {
                    MessageBox.Show(
                        "Cột dữ liệu 'Số lượng' hiện đang không hiển thị trên danh sách hàng nhập.\n\nVui lòng vào mục 'Hiện cột' và tích chọn hiển thị lại cột 'Số lượng' trước khi thêm hàng!",
                        "Thông báo",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning);
                    return;
                }

                if (dgvChiTietHang.IsCurrentCellInEditMode)
                {
                    ApDungSoLuongNhap();
                }

                if (cboHangHoa.SelectedValue == null || !int.TryParse(cboHangHoa.SelectedValue.ToString(), out int maHang) || maHang <= 0)
                {
                    MessageBox.Show("Vui lòng chọn một mặt hàng!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (dtDanhSachHang == null) return;
                DataRow[] rows = dtDanhSachHang.Select($"MaHang = {maHang}");
                if (rows.Length == 0) return;

                DataRow rowItem = rows[0];
                string sku = rowItem["MaHangNoiBo"] != DBNull.Value ? rowItem["MaHangNoiBo"].ToString() : "";
                string ten = rowItem["TenHang"].ToString();
                string dvt = rowItem["TenDVT"].ToString();
                decimal donGia = Convert.ToDecimal(rowItem["DonGiaNhap"]);

                DataRow[] daCo = dtChiTietHang.Select($"MaHang = {maHang}");
                if (daCo.Length > 0)
                {
                    int sttCuaHang = Convert.ToInt32(daCo[0]["STT"]);
                    int pageChuaHang = (int)Math.Ceiling((double)sttCuaHang / soDongMoiTrang);
                    CapNhatPhanTrang(pageChuaHang);

                    decimal slHienCo = Convert.ToDecimal(daCo[0]["SoLuong"]) + 1;
                    daCo[0]["SoLuong"] = slHienCo;
                    decimal ggDong = Convert.ToDecimal(daCo[0]["GiamGia"]);
                    decimal ttDong = (slHienCo * donGia) * (1m - (ggDong / 100m));
                    daCo[0]["ThanhTien"] = ttDong > 0 ? ttDong : 0;
                    CapNhatTongKet();

                    dgvChiTietHang.Focus();
                    foreach (DataGridViewRow dgvRow in dgvChiTietHang.Rows)
                    {
                        if (dgvRow.Cells["colMaHangNoiBo"].Value?.ToString() == sku)
                        {
                            if (dgvChiTietHang.Columns.Contains("colSoLuong") && dgvChiTietHang.Columns["colSoLuong"].Visible)
                            {
                                try
                                {
                                    dgvChiTietHang.CurrentCell = dgvRow.Cells["colSoLuong"];
                                    dgvRow.Selected = true;
                                    dgvChiTietHang.BeginEdit(true);
                                    if (dgvChiTietHang.EditingControl is TextBox txtBox)
                                    {
                                        txtBox.SelectAll();
                                    }
                                }
                                catch { }
                            }
                            break;
                        }
                    }
                }
                else
                {
                    DataRow rMoi = dtChiTietHang.NewRow();
                    rMoi["STT"] = dtChiTietHang.Rows.Count + 1;
                    rMoi["MaHang"] = maHang;
                    rMoi["MaHangNoiBo"] = sku;
                    rMoi["TenHang"] = ten;
                    rMoi["TenDVT"] = dvt;
                    rMoi["SoLuong"] = 1;
                    rMoi["DonGiaNhap"] = donGia;
                    rMoi["GiamGia"] = 0;
                    rMoi["ThanhTien"] = donGia;

                    dtChiTietHang.Rows.Add(rMoi);
                    CapNhatTongKet();

                    int targetPage = (int)Math.Ceiling((double)dtChiTietHang.Rows.Count / soDongMoiTrang);
                    CapNhatPhanTrang(targetPage);

                    dgvChiTietHang.Focus();
                    foreach (DataGridViewRow dgvRow in dgvChiTietHang.Rows)
                    {
                        if (dgvRow.Cells["colMaHangNoiBo"].Value?.ToString() == sku)
                        {
                            if (dgvChiTietHang.Columns.Contains("colSoLuong") && dgvChiTietHang.Columns["colSoLuong"].Visible)
                            {
                                try
                                {
                                    dgvChiTietHang.CurrentCell = dgvRow.Cells["colSoLuong"];
                                    dgvRow.Selected = true;
                                    dgvChiTietHang.BeginEdit(true);
                                    if (dgvChiTietHang.EditingControl is TextBox txtBox)
                                    {
                                        txtBox.SelectAll();
                                    }
                                }
                                catch { }
                            }
                            break;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Không thể thực hiện thêm hàng: " + ex.Message, "Thông báo lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void dgvChiTietHang_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0 || e.RowIndex >= dgvChiTietHang.Rows.Count) return;

            string tenHang = dgvChiTietHang.Columns.Contains("colTenHang") ? dgvChiTietHang.Rows[e.RowIndex].Cells["colTenHang"].Value?.ToString() : "";
            string maHang = dgvChiTietHang.Columns.Contains("colMaHangNoiBo") ? dgvChiTietHang.Rows[e.RowIndex].Cells["colMaHangNoiBo"].Value?.ToString() : "";
            string tenHienThi = !string.IsNullOrWhiteSpace(tenHang) ? $"'{tenHang}' ({maHang})" : "mặt hàng này";

            DialogResult dr = MessageBox.Show(
                $"Bạn có chắc chắn muốn xóa {tenHienThi} khỏi phiếu nhập hàng?",
                "Xác nhận xóa",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            if (dr == DialogResult.Yes)
            {
                if (dgvChiTietHang.IsCurrentCellInEditMode)
                {
                    dgvChiTietHang.CancelEdit();
                }

                if (dgvChiTietHang.Rows[e.RowIndex].DataBoundItem is DataRowView drv)
                {
                    drv.Row.Delete();
                    dtChiTietHang.AcceptChanges();
                }
                else if (e.RowIndex < dtChiTietHang.Rows.Count)
                {
                    dtChiTietHang.Rows.RemoveAt(e.RowIndex);
                }

                DanhSoLaiStt();
                CapNhatTongKet();
                CapNhatPhanTrang();
            }
        }

        private void DanhSoLaiStt()
        {
            for (int i = 0; i < dtChiTietHang.Rows.Count; i++)
            {
                dtChiTietHang.Rows[i]["STT"] = i + 1;
            }
        }

        private decimal ParseDecimalAnToan(object rawVal)
        {
            if (rawVal == null || rawVal == DBNull.Value) return 0m;
            if (rawVal is decimal d) return d;
            if (rawVal is int i) return (decimal)i;
            if (rawVal is long l) return (decimal)l;
            if (rawVal is double db) return (decimal)db;
            if (rawVal is float f) return (decimal)f;

            string s = rawVal.ToString().Trim().Replace("đ", "").Replace("VNĐ", "").Replace("%", "").Trim();
            if (string.IsNullOrEmpty(s)) return 0m;

            if (s.Contains(".") && s.Contains(","))
            {
                if (s.LastIndexOf('.') > s.LastIndexOf(','))
                {
                    s = s.Replace(",", "");
                }
                else
                {
                    s = s.Replace(".", "").Replace(",", ".");
                }
            }
            else if (s.Contains("."))
            {
                int dotIdx = s.IndexOf('.');
                int digitsAfter = s.Length - dotIdx - 1;
                if (digitsAfter == 3 && dotIdx == s.LastIndexOf('.'))
                {
                    s = s.Replace(".", "");
                }
                else if (s.IndexOf('.') != s.LastIndexOf('.'))
                {
                    s = s.Replace(".", "");
                }
            }
            else if (s.Contains(","))
            {
                int commaIdx = s.IndexOf(',');
                int digitsAfter = s.Length - commaIdx - 1;
                if (digitsAfter == 3 && commaIdx == s.LastIndexOf(','))
                {
                    s = s.Replace(",", "");
                }
                else if (s.IndexOf(',') != s.LastIndexOf(','))
                {
                    s = s.Replace(",", "");
                }
                else
                {
                    s = s.Replace(",", ".");
                }
            }

            if (decimal.TryParse(s, NumberStyles.Any, CultureInfo.InvariantCulture, out decimal res))
            {
                return res;
            }
            return 0m;
        }

        private void dgvChiTietHang_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0 || e.RowIndex >= dgvChiTietHang.Rows.Count) return;
            if (e.ColumnIndex < 0 || e.ColumnIndex >= dgvChiTietHang.Columns.Count) return;

            string colName = dgvChiTietHang.Columns[e.ColumnIndex].Name;
            if (colName == "colSoLuong" || colName == "colDonGia" || colName == "colGiamGia")
            {
                dgvChiTietHang.BeginEdit(true);
                if (dgvChiTietHang.EditingControl is TextBox txt)
                {
                    txt.SelectAll();
                }
            }
        }

        private void ApDungSoLuongNhap(int targetRowIndex = -1, int targetColIndex = -1)
        {
            try
            {
                int rowIndex = targetRowIndex >= 0 ? targetRowIndex : (dgvChiTietHang.CurrentCell != null ? dgvChiTietHang.CurrentCell.RowIndex : -1);
                int colIndex = targetColIndex >= 0 ? targetColIndex : (dgvChiTietHang.CurrentCell != null ? dgvChiTietHang.CurrentCell.ColumnIndex : -1);

                if (rowIndex < 0 || rowIndex >= dgvChiTietHang.Rows.Count) return;
                if (colIndex < 0 || colIndex >= dgvChiTietHang.Columns.Count) return;

                if (rowIndex < 0 || rowIndex >= dgvChiTietHang.Rows.Count) return;

                DataGridViewRow dgvRow = dgvChiTietHang.Rows[rowIndex];
                DataRowView drv = dgvRow.DataBoundItem as DataRowView;
                DataRow row = drv != null ? drv.Row : (rowIndex < dtChiTietHang.Rows.Count ? dtChiTietHang.Rows[rowIndex] : null);
                if (row == null) return;

                string colName = dgvChiTietHang.Columns[colIndex].Name;

                // Lấy chuỗi từ ô nhập
                object cellRawVal = null;
                if (dgvChiTietHang.EditingControl is TextBox txt && dgvChiTietHang.CurrentCell != null && dgvChiTietHang.CurrentCell.RowIndex == rowIndex && dgvChiTietHang.CurrentCell.ColumnIndex == colIndex)
                {
                    cellRawVal = txt.Text;
                }
                else if (dgvChiTietHang.Columns.Contains(colName) && dgvRow.Cells[colName].Value != null)
                {
                    cellRawVal = dgvRow.Cells[colName].Value;
                }

                decimal soLuong = row["SoLuong"] != DBNull.Value ? ParseDecimalAnToan(row["SoLuong"]) : 1;
                decimal donGia = row["DonGiaNhap"] != DBNull.Value ? ParseDecimalAnToan(row["DonGiaNhap"]) : 0;
                decimal giamGia = row["GiamGia"] != DBNull.Value ? ParseDecimalAnToan(row["GiamGia"]) : 0;

                decimal inputVal = ParseDecimalAnToan(cellRawVal);

                if (colName == "colSoLuong")
                {
                    if (inputVal <= 0)
                    {
                        MessageBox.Show("Số lượng nhập phải là số nguyên lớn hơn 0!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        soLuong = 1;
                    }
                    else
                    {
                        soLuong = Math.Floor(inputVal);
                    }
                }
                else if (colName == "colDonGia")
                {
                    if (inputVal < 0)
                    {
                        MessageBox.Show("Đơn giá nhập không hợp lệ!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        donGia = 0;
                    }
                    else
                    {
                        donGia = inputVal;
                    }
                }
                else if (colName == "colGiamGia")
                {
                    if (inputVal < 0)
                    {
                        MessageBox.Show("Giảm giá (%) không hợp lệ!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        giamGia = 0;
                    }
                    else if (inputVal > 100)
                    {
                        MessageBox.Show("Giảm giá không được vượt quá 100%!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        giamGia = 100;
                    }
                    else
                    {
                        giamGia = inputVal;
                    }
                }

                decimal tienChuaGiam = soLuong * donGia;
                decimal thanhTien = Math.Max(0, tienChuaGiam * (1m - (giamGia / 100m)));


                if (dgvChiTietHang.IsCurrentCellInEditMode)
                {
                    dgvChiTietHang.EndEdit();
                }

                if (drv != null)
                {
                    drv["SoLuong"] = soLuong;
                    drv["DonGiaNhap"] = donGia;
                    drv["GiamGia"] = giamGia;
                    drv["ThanhTien"] = thanhTien;
                    drv.EndEdit();
                }
                row["SoLuong"] = soLuong;
                row["DonGiaNhap"] = donGia;
                row["GiamGia"] = giamGia;
                row["ThanhTien"] = thanhTien;

                if (dgvChiTietHang.Columns.Contains("colSoLuong"))
                {
                    dgvRow.Cells["colSoLuong"].Value = soLuong;
                }
                if (dgvChiTietHang.Columns.Contains("colDonGia"))
                {
                    dgvRow.Cells["colDonGia"].Value = donGia;
                }
                if (dgvChiTietHang.Columns.Contains("colGiamGia"))
                {
                    dgvRow.Cells["colGiamGia"].Value = giamGia;
                }
                if (dgvChiTietHang.Columns.Contains("colThanhTien"))
                {
                    dgvRow.Cells["colThanhTien"].Value = thanhTien;
                }

                this.BindingContext[dtChiTietHang]?.EndCurrentEdit();
                CapNhatTongKet();
                dgvChiTietHang.InvalidateRow(rowIndex);
            }
            catch { }
        }

        private void dgvChiTietHang_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            if (isCalculating || e.RowIndex < 0 || e.RowIndex >= dgvChiTietHang.Rows.Count) return;
            if (e.ColumnIndex < 0 || e.ColumnIndex >= dgvChiTietHang.Columns.Count) return;

            string colName = dgvChiTietHang.Columns[e.ColumnIndex].Name;
            if (colName == "colSoLuong" || colName == "colDonGia" || colName == "colGiamGia")
            {
                try
                {

                    ApDungSoLuongNhap(e.RowIndex, e.ColumnIndex);
                }
                finally
                {
                    isCalculating = false;
                }
            }
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (keyData == Keys.Enter && dgvChiTietHang.IsCurrentCellInEditMode)
            {
                string colName = (dgvChiTietHang.CurrentCell != null && dgvChiTietHang.CurrentCell.ColumnIndex >= 0 && dgvChiTietHang.CurrentCell.ColumnIndex < dgvChiTietHang.Columns.Count)
                    ? dgvChiTietHang.Columns[dgvChiTietHang.CurrentCell.ColumnIndex].Name : "";
                if (colName == "colSoLuong" || colName == "colDonGia" || colName == "colGiamGia")
                {
                    ApDungSoLuongNhap();
                    return true;
                }
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }

        private void dgvChiTietHang_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            if (e.Control is TextBox txt)
            {
                txt.KeyPress -= TxtNumberDgv_KeyPress;
                txt.KeyDown -= TxtNumberDgv_KeyDown;

                string colName = (dgvChiTietHang.CurrentCell != null && dgvChiTietHang.CurrentCell.ColumnIndex >= 0 && dgvChiTietHang.CurrentCell.ColumnIndex < dgvChiTietHang.Columns.Count)
                    ? dgvChiTietHang.Columns[dgvChiTietHang.CurrentCell.ColumnIndex].Name : "";
                if (colName == "colSoLuong" || colName == "colDonGia" || colName == "colGiamGia")
                {
                    txt.KeyPress += TxtNumberDgv_KeyPress;
                    txt.KeyDown += TxtNumberDgv_KeyDown;

                    this.BeginInvoke(new Action(() =>
                    {
                        if (txt != null && !txt.IsDisposed)
                        {
                            txt.SelectAll();
                        }
                    }));
                }
            }
        }

        private void TxtNumberDgv_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void TxtNumberDgv_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;
                ApDungSoLuongNhap();
            }
        }

        private void CapNhatPhanTrang(int pageToFocus = -1)
        {
            if (dtChiTietHang == null) return;

            if (dgvChiTietHang.IsCurrentCellInEditMode)
            {
                dgvChiTietHang.EndEdit();
            }

            int tongSoBanGhi = dtChiTietHang.Rows.Count;
            if (soDongMoiTrang <= 0) soDongMoiTrang = 20;

            tongSoTrang = (int)Math.Ceiling((double)tongSoBanGhi / soDongMoiTrang);
            if (tongSoTrang < 1) tongSoTrang = 1;

            if (pageToFocus > 0)
            {
                trangHienTai = Math.Min(pageToFocus, tongSoTrang);
            }
            else
            {
                if (trangHienTai > tongSoTrang) trangHienTai = tongSoTrang;
                if (trangHienTai < 1) trangHienTai = 1;
            }

            if (tongSoBanGhi == 0)
            {
                dtChiTietHang.DefaultView.RowFilter = "";
                lblTrangHienTai.Text = "Trang 1 / 1";
                btnTrangTruoc.Enabled = false;
                btnTrangSau.Enabled = false;
                return;
            }

            int minSTT = (trangHienTai - 1) * soDongMoiTrang + 1;
            int maxSTT = trangHienTai * soDongMoiTrang;

            dtChiTietHang.DefaultView.RowFilter = $"STT >= {minSTT} AND STT <= {maxSTT}";

            lblTrangHienTai.Text = $"Trang {trangHienTai} / {tongSoTrang}";
            btnTrangTruoc.Enabled = (trangHienTai > 1);
            btnTrangSau.Enabled = (trangHienTai < tongSoTrang);
        }

        private void btnTrangTruoc_Click(object sender, EventArgs e)
        {
            if (trangHienTai > 1)
            {
                trangHienTai--;
                CapNhatPhanTrang();
            }
        }

        private void btnTrangSau_Click(object sender, EventArgs e)
        {
            if (trangHienTai < tongSoTrang)
            {
                trangHienTai++;
                CapNhatPhanTrang();
            }
        }

        private void cboSoDongMoiTrang_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cboSoDongMoiTrang.SelectedItem != null && int.TryParse(cboSoDongMoiTrang.SelectedItem.ToString(), out int rows))
            {
                soDongMoiTrang = rows;
                trangHienTai = 1;
                CapNhatPhanTrang();
            }
        }

        private void CapNhatTongKet()
        {
            if (isCalculating) return;

            int soMatHang = dtChiTietHang.Rows.Count;
            try
            {
                isCalculating = true;
                decimal tongSoLuong = 0;
            decimal tongTienHang = 0;

            foreach (DataRow r in dtChiTietHang.Rows)
            {
                if (r.RowState == DataRowState.Deleted) continue;
                tongSoLuong += ParseDecimalAnToan(r["SoLuong"]);
                tongTienHang += ParseDecimalAnToan(r["ThanhTien"]);
            }

            lblSoMatHangVal.Text = soMatHang.ToString("N0");
            lblTongSoLuongVal.Text = tongSoLuong.ToString("N0");
            lblTongTienHangVal.Text = string.Format("{0:N0} đ", tongTienHang);

            decimal giamGiaTongPercent = 0;
            string cleanGg = txtGiamGiaTong.Text.Replace(",", "").Replace(".", "").Trim();
            if (decimal.TryParse(cleanGg, out decimal ggVal))
            {
                if (ggVal < 0) ggVal = 0;
                if (ggVal > 100) ggVal = 100;
                giamGiaTongPercent = ggVal;
            }

            decimal giamGiaTien = tongTienHang * (giamGiaTongPercent / 100m);
            decimal tongCong = Math.Max(0, tongTienHang - giamGiaTien);
            lblTongCongVal.Text = string.Format("{0:N0} đ", tongCong);

            decimal tienDaTra = 0;
            string cleanDaTra = txtTienDaTra.Text.Replace(",", "").Replace(".", "").Trim();
            if (decimal.TryParse(cleanDaTra, out decimal dtVal))
            {
                tienDaTra = dtVal;
            }
            else
            {
                tienDaTra = 0;
            }

            decimal canTraNcc = Math.Max(0, tongCong - tienDaTra);
            lblCanTraNccVal.Text = string.Format("{0:N0} đ", canTraNcc);
            }
            finally
            {
                isCalculating = false;
            }
        }

        private void txtGiamGiaTong_Leave(object sender, EventArgs e)
        {
            string clean = txtGiamGiaTong.Text.Replace(",", "").Replace(".", "").Trim();
            if (decimal.TryParse(clean, out decimal val))
            {
                if (val < 0) val = 0;
                if (val > 100) val = 100;
                txtGiamGiaTong.Text = val.ToString("N0");
            }
            else
            {
                txtGiamGiaTong.Text = "0";
            }
            CapNhatTongKet();
        }

        private void txtGiamGiaTong_Enter(object sender, EventArgs e)
        {
            this.BeginInvoke(new Action(() => txtGiamGiaTong.SelectAll()));
        }

        private void txtGiamGiaTong_Click(object sender, EventArgs e)
        {
            txtGiamGiaTong.SelectAll();
        }

        private void txtTienDaTra_Leave(object sender, EventArgs e)
        {
            string clean = txtTienDaTra.Text.Replace(",", "").Replace(".", "").Trim();
            if (decimal.TryParse(clean, out decimal val))
            {
                txtTienDaTra.Text = val.ToString("N0");
            }
            else
            {
                txtTienDaTra.Text = "0";
            }
            CapNhatTongKet();
        }

        private void txtTienDaTra_Enter(object sender, EventArgs e)
        {
            this.BeginInvoke(new Action(() => txtTienDaTra.SelectAll()));
        }

        private void txtTienDaTra_Click(object sender, EventArgs e)
        {
            txtTienDaTra.SelectAll();
        }

        private void txtGiamGiaTong_TextChanged(object sender, EventArgs e)
        {
            CapNhatTongKet();
        }

        private void txtTienDaTra_TextChanged(object sender, EventArgs e)
        {
            CapNhatTongKet();
        }

        private void btnHienCot_Click(object sender, EventArgs e)
        {
            foreach (Form openForm in Application.OpenForms)
            {
                if (openForm is frmHienCotHangNhap fOpen)
                {
                    fOpen.Activate();
                    return;
                }
            }

            frmHienCotHangNhap frm = new frmHienCotHangNhap(dgvChiTietHang);
            Point btnPos = btnHienCot.PointToScreen(Point.Empty);
            int posX = btnPos.X + btnHienCot.Width - frm.Width;
            int posY = btnPos.Y + btnHienCot.Height + 2;

            Rectangle workingArea = Screen.FromControl(btnHienCot).WorkingArea;
            if (posX < workingArea.Left) posX = workingArea.Left;
            if (posX + frm.Width > workingArea.Right) posX = workingArea.Right - frm.Width;
            if (posY + frm.Height > workingArea.Bottom) posY = btnPos.Y - frm.Height - 2;

            frm.StartPosition = FormStartPosition.Manual;
            frm.Location = new Point(posX, posY);
            frm.Show(this);
        }

        private void btnNhapFile_Click(object sender, EventArgs e)
        {
            if (!dgvChiTietHang.Columns.Contains("colSoLuong") || !dgvChiTietHang.Columns["colSoLuong"].Visible)
            {
                MessageBox.Show(
                    "Cột dữ liệu 'Số lượng' hiện đang không hiển thị trên danh sách hàng nhập.\n\nVui lòng vào mục 'Hiện cột' và tích chọn hiển thị lại cột 'Số lượng' trước khi nhập file!",
                    "Thông báo",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
                return;
            }

            using (frmNhapHangNhapFile frm = new frmNhapHangNhapFile())
            {
                if (frm.ShowDialog(this) == DialogResult.OK && frm.DtKetQuaNhap != null && frm.DtKetQuaNhap.Rows.Count > 0)
                {
                    int demThemMoi = 0;
                    int demCapNhat = 0;

                    foreach (DataRow r in frm.DtKetQuaNhap.Rows)
                    {
                        int maH = Convert.ToInt32(r["MaHang"]);
                        decimal sl = Convert.ToDecimal(r["SoLuong"]);
                        decimal dg = Convert.ToDecimal(r["DonGiaNhap"]);
                        decimal gg = Convert.ToDecimal(r["GiamGia"]);

                        DataRow[] daCo = dtChiTietHang.Select($"MaHang = {maH}");
                        if (daCo.Length > 0)
                        {
                            decimal slMoi = Convert.ToDecimal(daCo[0]["SoLuong"]) + sl;
                            decimal ggMoi = Convert.ToDecimal(daCo[0]["GiamGia"]) + gg;
                            decimal dgApDung = dg > 0 ? dg : Convert.ToDecimal(daCo[0]["DonGiaNhap"]);

                            daCo[0]["SoLuong"] = slMoi;
                            daCo[0]["DonGiaNhap"] = dgApDung;
                            daCo[0]["GiamGia"] = ggMoi;
                            daCo[0]["ThanhTien"] = Math.Max(0, (slMoi * dgApDung) * (1m - (ggMoi / 100m)));
                            demCapNhat++;
                        }
                        else
                        {
                            DataRow nr = dtChiTietHang.NewRow();
                            nr["STT"] = dtChiTietHang.Rows.Count + 1;
                            nr["MaHang"] = maH;
                            nr["MaHangNoiBo"] = r["MaHangNoiBo"];
                            nr["TenHang"] = r["TenHang"];
                            nr["TenDVT"] = r["TenDVT"];
                            nr["SoLuong"] = sl;
                            nr["DonGiaNhap"] = dg;
                            nr["GiamGia"] = gg;
                            nr["ThanhTien"] = Math.Max(0, (sl * dg) * (1m - (gg / 100m)));

                            dtChiTietHang.Rows.Add(nr);
                            demThemMoi++;
                        }
                    }

                    CapNhatTongKet();
                    CapNhatPhanTrang();
                }
            }
        }

        private void btnLuuTam_Click(object sender, EventArgs e)
        {
            LuuPhieuNhap("NHAP");
        }

        private void btnHoanThanh_Click(object sender, EventArgs e)
        {
            LuuPhieuNhap("DA_XAC_NHAN");
        }

        private void LuuPhieuNhap(string trangThai)
        {
            if (dgvChiTietHang.IsCurrentCellInEditMode)
            {
                ApDungSoLuongNhap();
            }
            this.BindingContext[dtChiTietHang]?.EndCurrentEdit();

            // 1. Kiểm tra tính hợp lệ
            if (cboNhaCungCap.SelectedValue == null)
            {
                MessageBox.Show("Vui lòng chọn nhà cung cấp!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                cboNhaCungCap.Focus();
                return;
            }

            if (cboKho.SelectedValue == null)
            {
                MessageBox.Show("Vui lòng chọn kho nhập!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                cboKho.Focus();
                return;
            }

            if (cboNguoiNhap.SelectedValue == null)
            {
                MessageBox.Show("Vui lòng chọn người nhập!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                cboNguoiNhap.Focus();
                return;
            }

            if (dtChiTietHang.Rows.Count == 0)
            {
                MessageBox.Show("Phiếu nhập chưa có mặt hàng nào! Vui lòng thêm ít nhất một mặt hàng.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string maPhieu = txtMaPhieu.Text.Trim();
            if (string.IsNullOrEmpty(maPhieu))
            {
                MessageBox.Show("Mã phiếu không được để trống!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtMaPhieu.Focus();
                return;
            }

            string checkSql = "SELECT COUNT(1) FROM PhieuNhap WHERE MaPhieu = @MaPhieu";
            object exist = DataProcessing.ReadValue(checkSql, new SqlParameter[] { new SqlParameter("@MaPhieu", maPhieu) });
            if (Convert.ToInt32(exist) > 0)
            {
                MessageBox.Show($"Mã phiếu '{maPhieu}' đã tồn tại trong hệ thống! Vui lòng chọn mã khác.", "Trùng mã", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtMaPhieu.Focus();
                return;
            }

            int maNcc = Convert.ToInt32(cboNhaCungCap.SelectedValue);
            int maKho = Convert.ToInt32(cboKho.SelectedValue);
            int maNvNhap = Convert.ToInt32(cboNguoiNhap.SelectedValue);
            int maNvLap = SessionContext.MaTaiKhoan > 0 ? SessionContext.MaTaiKhoan : maNvNhap;
            DateTime ngayNhap = dtpNgayNhap.Value;
            string soHoaDon = txtSoHoaDon.Text.Trim();
            string ghiChu = txtGhiChu.Text.Trim();

            decimal tongTienHang = 0;
            foreach (DataRow row in dtChiTietHang.Rows)
            {
                tongTienHang += ParseDecimalAnToan(row["ThanhTien"]);
            }

            decimal giamGiaTongPercent = 0;
            string cleanGg = txtGiamGiaTong.Text.Replace(",", "").Replace(".", "").Trim();
            if (decimal.TryParse(cleanGg, out decimal ggVal))
            {
                if (ggVal < 0) ggVal = 0;
                if (ggVal > 100) ggVal = 100;
                giamGiaTongPercent = ggVal;
            }

            decimal giamGiaTien = tongTienHang * (giamGiaTongPercent / 100m);
            decimal tongCong = Math.Max(0, tongTienHang - giamGiaTien);
            lblTongCongVal.Text = string.Format("{0:N0} đ", tongCong);

            decimal tienDaTra = 0;
            string cleanDaTra = txtTienDaTra.Text.Replace(",", "").Replace(".", "").Trim();
            if (decimal.TryParse(cleanDaTra, out decimal dtVal))
            {
                tienDaTra = dtVal;
            }
            else
            {
                tienDaTra = 0;
            }

            decimal canTraNcc = Math.Max(0, tongCong - tienDaTra);

            string xacNhanMsg = trangThai == "DA_XAC_NHAN"
                ? "Bạn có chắc chắn muốn HOÀN THÀNH phiếu nhập này? Tồn kho sẽ được cộng ngay lập tức và công nợ sẽ được ghi nhận."
                : "Bạn có chắc chắn muốn LƯU TẠM phiếu nhập này?";

            if (MessageBox.Show(xacNhanMsg, "Xác nhận lưu", MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
            {
                return;
            }

            // 2. Thực thi giao dịch lưu CSDL
            try
            {
                DataProcessing.ChangeDataTransaction((conn, trans) =>
                {
                    // 2.1. Thêm PhieuNhap
                    string insertPnSql = @"
                        INSERT INTO PhieuNhap (
                            MaKho, MaDonViCapHang, MaNVLap, MaNVXacNhan, NgayLap, NgayXacNhan,
                            LoaiPhieuNhap, TrangThai, GhiChu, MaPhieu, DaDanhDau, SoHoaDon,
                            GiamGia, TongTien, CanTraNcc
                        )
                        VALUES (
                            @MaKho, @MaDonViCapHang, @MaNVLap, @MaNVXacNhan, @NgayLap, @NgayXacNhan,
                            'THONG_THUONG', @TrangThai, @GhiChu, @MaPhieu, 0, @SoHoaDon,
                            @GiamGia, @TongTien, @CanTraNcc
                        );
                        SELECT SCOPE_IDENTITY();";

                    SqlCommand cmdPn = new SqlCommand(insertPnSql, conn, trans);
                    cmdPn.Parameters.AddWithValue("@MaKho", maKho);
                    cmdPn.Parameters.AddWithValue("@MaDonViCapHang", maNcc);
                    cmdPn.Parameters.AddWithValue("@MaNVLap", maNvLap);
                    cmdPn.Parameters.AddWithValue("@MaNVXacNhan", trangThai == "DA_XAC_NHAN" ? (object)maNvNhap : DBNull.Value);
                    cmdPn.Parameters.AddWithValue("@NgayLap", ngayNhap);
                    cmdPn.Parameters.AddWithValue("@NgayXacNhan", trangThai == "DA_XAC_NHAN" ? (object)DateTime.Now : DBNull.Value);
                    cmdPn.Parameters.AddWithValue("@TrangThai", trangThai);
                    cmdPn.Parameters.AddWithValue("@GhiChu", string.IsNullOrEmpty(ghiChu) ? (object)DBNull.Value : ghiChu);
                    cmdPn.Parameters.AddWithValue("@MaPhieu", maPhieu);
                    cmdPn.Parameters.AddWithValue("@SoHoaDon", string.IsNullOrEmpty(soHoaDon) ? (object)DBNull.Value : soHoaDon);
                    cmdPn.Parameters.AddWithValue("@GiamGia", giamGiaTongPercent);
                    cmdPn.Parameters.AddWithValue("@TongTien", tongCong);
                    cmdPn.Parameters.AddWithValue("@CanTraNcc", canTraNcc);

                    object newIdObj = cmdPn.ExecuteScalar();
                    int maPhieuNhap = Convert.ToInt32(newIdObj);

                    // 2.2. Thêm ChiTietPhieuNhap
                    string insertCtSql = @"
                        INSERT INTO ChiTietPhieuNhap (MaPhieuNhap, MaHang, SoLuong, DonGiaNhap, GiamGia, ThanhTien)
                        VALUES (@MaPhieuNhap, @MaHang, @SoLuong, @DonGiaNhap, @GiamGia, @ThanhTien);";

                    foreach (DataRow row in dtChiTietHang.Rows)
                    {
                        int maHangItem = Convert.ToInt32(row["MaHang"]);
                        decimal soLuong = Convert.ToDecimal(row["SoLuong"]);
                        decimal donGia = Convert.ToDecimal(row["DonGiaNhap"]);
                        decimal giamGia = Convert.ToDecimal(row["GiamGia"]);
                        decimal thanhTien = Convert.ToDecimal(row["ThanhTien"]);

                        SqlCommand cmdCt = new SqlCommand(insertCtSql, conn, trans);
                        cmdCt.Parameters.AddWithValue("@MaPhieuNhap", maPhieuNhap);
                        cmdCt.Parameters.AddWithValue("@MaHang", maHangItem);
                        cmdCt.Parameters.AddWithValue("@SoLuong", soLuong);
                        cmdCt.Parameters.AddWithValue("@DonGiaNhap", donGia);
                        cmdCt.Parameters.AddWithValue("@GiamGia", giamGia);
                        cmdCt.Parameters.AddWithValue("@ThanhTien", thanhTien);
                        cmdCt.ExecuteNonQuery();
                    }

                    // 2.3. Ghi nhật ký thao tác
                    string logSql = @"
                        INSERT INTO NhatKyThaoTac (MaTaiKhoan, ThoiDiem, ThaoTac, LoaiDoiTuong, MaDoiTuong, NoiDung)
                        VALUES (@MaTaiKhoan, GETDATE(), @ThaoTac, 'PhieuNhap', @MaDoiTuong, @NoiDung)";
                    SqlCommand cmdLog = new SqlCommand(logSql, conn, trans);
                    cmdLog.Parameters.AddWithValue("@MaTaiKhoan", SessionContext.MaTaiKhoan > 0 ? SessionContext.MaTaiKhoan : 1);
                    cmdLog.Parameters.AddWithValue("@ThaoTac", trangThai == "DA_XAC_NHAN" ? "HOAN_THANH_NHAP_HANG" : "LUU_TAM_NHAP_HANG");
                    cmdLog.Parameters.AddWithValue("@MaDoiTuong", maPhieu);
                    cmdLog.Parameters.AddWithValue("@NoiDung", $"Lập phiếu nhập {maPhieu} (ID: {maPhieuNhap}), tổng tiền: {tongCong:N0}, trạng thái: {trangThai}");
                    cmdLog.ExecuteNonQuery();
                });

                string thongBao = trangThai == "DA_XAC_NHAN"
                    ? $"Hoàn thành phiếu nhập hàng '{maPhieu}' thành công!"
                    : $"Lưu tạm phiếu nhập hàng '{maPhieu}' thành công!";

                MessageBox.Show(thongBao, "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi lưu phiếu nhập: " + ex.Message, "Thông báo lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnBoQua_Click(object sender, EventArgs e)
        {
            if (dgvChiTietHang.IsCurrentCellInEditMode)
            {
                dgvChiTietHang.CancelEdit();
            }

            if (dtChiTietHang != null && dtChiTietHang.Rows.Count > 0)
            {
                DialogResult dr = MessageBox.Show(
                    "Dữ liệu phiếu nhập hàng chưa được lưu. Bạn có chắc chắn muốn bỏ qua?",
                    "Xác nhận bỏ qua",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question);

                if (dr != DialogResult.Yes) return;
            }

            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }
    }
}
