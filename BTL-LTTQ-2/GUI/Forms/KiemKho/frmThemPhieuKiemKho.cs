using BTL_LTTQ_2.Classes;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Windows.Forms;

namespace BTL_LTTQ_2.GUI.Forms.KiemKho
{
    public partial class frmThemPhieuKiemKho : Form
    {
        private DataTable dtHangKiem;
        private DataTable dtDanhSachHang;
        private bool isCalculating = false;

        private int trangHienTai = 1;
        private int soDongMoiTrang = 20;
        private int tongSoTrang = 1;

        public bool DaThayDoiDuLieu { get; private set; } = false;

        public frmThemPhieuKiemKho()
        {
            InitializeComponent();
            KhoiTaoBangHangKiem();
            CauHinhDataGridView();
        }

        private void KhoiTaoBangHangKiem()
        {
            dtHangKiem = new DataTable();
            dtHangKiem.Columns.Add("STT", typeof(int));
            dtHangKiem.Columns.Add("MaHang", typeof(int));
            dtHangKiem.Columns.Add("MaHangNoiBo", typeof(string));
            dtHangKiem.Columns.Add("TenHang", typeof(string));
            dtHangKiem.Columns.Add("TenDVT", typeof(string));
            dtHangKiem.Columns.Add("TonKho", typeof(decimal));
            dtHangKiem.Columns.Add("SoLuongThucTe", typeof(decimal));
            dtHangKiem.Columns.Add("SlChenhLech", typeof(decimal));
            dtHangKiem.Columns.Add("DonGia", typeof(decimal));
            dtHangKiem.Columns.Add("GiaTriChenhLech", typeof(decimal));
        }

        private void CauHinhDataGridView()
        {
            // Bật DoubleBuffered chống rung giật
            typeof(DataGridView).InvokeMember("DoubleBuffered",
                System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.SetProperty,
                null, dgvHangKiem, new object[] { true });

            dgvHangKiem.AutoGenerateColumns = false;
            dgvHangKiem.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(248, 250, 252);
            dgvHangKiem.DefaultCellStyle.SelectionBackColor = Color.FromArgb(224, 231, 255);
            dgvHangKiem.DefaultCellStyle.SelectionForeColor = Color.FromArgb(30, 41, 59);

            dgvHangKiem.ColumnHeadersDefaultCellStyle.WrapMode = DataGridViewTriState.False;
            dgvHangKiem.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.EnableResizing;
            dgvHangKiem.ColumnHeadersHeight = 36;

            colStt.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            colMaHangNoiBo.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            colDonViTinh.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            colDonViTinh.Visible = false;
            colTonKho.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            colSoLuongThucTe.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            colSlChenhLech.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            colDonGia.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            colGiaTriChenhLech.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;

            colTonKho.DefaultCellStyle.Format = "N0";
            colSoLuongThucTe.DefaultCellStyle.Format = "N0";
            colSoLuongThucTe.DefaultCellStyle.BackColor = Color.FromArgb(254, 252, 232);
            colSoLuongThucTe.ToolTipText = "Nhập trực tiếp số lượng thực tế kiểm đếm tại đây";
            colSlChenhLech.DefaultCellStyle.Format = "N0";
            colDonGia.DefaultCellStyle.Format = "N0";
            colGiaTriChenhLech.DefaultCellStyle.Format = "N0";

            dgvHangKiem.EditMode = DataGridViewEditMode.EditOnEnter;
            dgvHangKiem.CellFormatting += dgvHangKiem_CellFormatting;

            dgvHangKiem.CellClick += dgvHangKiem_CellClick;
            dgvHangKiem.EditingControlShowing += dgvHangKiem_EditingControlShowing;
            dgvHangKiem.Leave += (s, e) =>
            {
                if (dgvHangKiem.IsCurrentCellInEditMode)
                {
                    ApDungSoLuongThucTe();
                }
            };

            dgvHangKiem.DataSource = dtHangKiem;
        }

        private void dgvHangKiem_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.RowIndex < 0 || e.RowIndex >= dgvHangKiem.Rows.Count) return;

            string colName = dgvHangKiem.Columns[e.ColumnIndex].Name;
            if (colName == "colSoLuongThucTe")
            {
                e.CellStyle.BackColor = Color.FromArgb(254, 252, 232);
            }
            if ((colName == "colSlChenhLech" || colName == "colGiaTriChenhLech") && e.Value != null)
            {
                if (decimal.TryParse(e.Value.ToString(), out decimal val) && val != 0)
                {
                    e.CellStyle.ForeColor = Color.Red;
                    e.CellStyle.Font = new Font(dgvHangKiem.Font, FontStyle.Bold);
                }
            }
        }

        private void frmThemPhieuKiemKho_Load(object sender, EventArgs e)
        {
            txtMaPhieu.Text = SinhMaPhieuTuDong();
            dtpThoiDiemChot.Value = DateTime.Now;


            NapDanhSachKho();
            NapDanhSachNhanVien();
            NapDanhSachHangHoa();

            // Khởi tạo ComboBox phân trang
            if (cboSoDongMoiTrang != null)
            {
                if (cboSoDongMoiTrang.Items.Count == 0)
                {
                    cboSoDongMoiTrang.Items.AddRange(new object[] { "10", "20", "50", "100" });
                }
                cboSoDongMoiTrang.SelectedItem = "20";
                if (cboSoDongMoiTrang.SelectedIndex < 0 && cboSoDongMoiTrang.Items.Count > 0)
                {
                    cboSoDongMoiTrang.SelectedIndex = 1;
                }
            }

            TinhToanTongKet();
            CapNhatPhanTrang();
        }

        private string SinhMaPhieuTuDong()
        {
            try
            {
                string sql = "SELECT ISNULL(MAX(MaPhieuKiemKe), 0) + 1 FROM PhieuKiemKe";
                object val = DataProcessing.ReadValue(sql);
                int nextId = (val != null && val != DBNull.Value) ? Convert.ToInt32(val) : 1;
                string ma = "PKK" + nextId.ToString("D6");
                while (true)
                {
                    object exist = DataProcessing.ReadValue("SELECT COUNT(1) FROM PhieuKiemKe WHERE MaPhieu = @MaPhieu", new SqlParameter[] { new SqlParameter("@MaPhieu", ma) });
                    if (exist != null && Convert.ToInt32(exist) > 0)
                    {
                        nextId++;
                        ma = "PKK" + nextId.ToString("D6");
                    }
                    else
                    {
                        break;
                    }
                }
                return ma;
            }
            catch
            {
                return "PKK" + DateTime.Now.ToString("yyMMddHHmm");
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
                string sql = "SELECT MaKho, TenKho FROM Kho WHERE TrangThai = 'HOAT_DONG' OR TrangThai = 'ACTIVE' OR TrangThai IS NULL ORDER BY TenKho ASC";
                DataTable dt = DataProcessing.ReadData(sql);
                cboKho.DisplayMember = "TenKho";
                cboKho.ValueMember = "MaKho";
                cboKho.DataSource = dt;

                if (SessionContext.MaKhoHienTai > 0)
                {
                    cboKho.SelectedValue = SessionContext.MaKhoHienTai;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi nạp danh sách kho: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void NapDanhSachNhanVien()
        {
            try
            {
                string sql = @"
                    SELECT 
                        nv.MaNV AS MaNhanVien, 
                        nv.HoTen 
                    FROM NhanVien nv 
                    WHERE nv.TrangThai = 'HOAT_DONG' OR nv.TrangThai = 'ACTIVE' OR nv.TrangThai IS NULL
                    ORDER BY nv.HoTen ASC";
                DataTable dt = DataProcessing.ReadData(sql);
                if (dt == null || dt.Rows.Count == 0)
                {
                    sql = @"
                        SELECT 
                            ISNULL(nv.MaNV, tk.MaTaiKhoan) AS MaNhanVien, 
                            ISNULL(nv.HoTen, tk.TenDangNhap) AS HoTen 
                        FROM TaiKhoan tk 
                        LEFT JOIN NhanVien nv ON tk.MaNV = nv.MaNV 
                        WHERE tk.TrangThai = 'HOAT_DONG' OR tk.TrangThai = 'ACTIVE' OR tk.TrangThai IS NULL
                        ORDER BY HoTen ASC";
                    dt = DataProcessing.ReadData(sql);
                }

                ComboboxHelper.CaiDatTimKiem(cboNguoiKiem, dt, "HoTen", "MaNhanVien");

                if (SessionContext.MaNV.HasValue && SessionContext.MaNV.Value > 0)
                {
                    cboNguoiKiem.SelectedValue = SessionContext.MaNV.Value;
                }
                else if (SessionContext.MaTaiKhoan > 0)
                {
                    cboNguoiKiem.SelectedValue = SessionContext.MaTaiKhoan;
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
                int maKho = 1;
                if (cboKho.SelectedValue is DataRowView drv && drv["MaKho"] != DBNull.Value)
                {
                    maKho = Convert.ToInt32(drv["MaKho"]);
                }
                else if (cboKho.SelectedValue != null && int.TryParse(cboKho.SelectedValue.ToString(), out int k))
                {
                    maKho = k;
                }

                string sql = @"
                    SELECT 
                        h.MaHang,
                        h.MaHangNoiBo,
                        h.TenHang,
                        N'[' + h.MaHangNoiBo + N'] ' + h.TenHang + N' (Tồn: ' + CAST(CAST(ISNULL(tk.SoLuongTon, 0) AS INT) AS NVARCHAR(10)) + N')' AS HienThi,
                        ISNULL(dvt.TenDVT, N'Cái') AS TenDVT,
                        ISNULL(h.GiaVon, 0) AS DonGia,
                        ISNULL(tk.SoLuongTon, 0) AS TonKho
                    FROM HangHoa h
                    LEFT JOIN DonViTinh dvt ON h.MaDVT = dvt.MaDVT
                    LEFT JOIN vw_TonKhoHienTai tk ON h.MaHang = tk.MaHang AND tk.MaKho = @MaKho
                    WHERE ISNULL(h.TrangThai, '') NOT IN ('NGUNG_BAN', 'DA_XOA')
                    ORDER BY h.TenHang ASC";

                SqlParameter[] p = { new SqlParameter("@MaKho", maKho) };
                dtDanhSachHang = DataProcessing.ReadData(sql, p);

                ComboboxHelper.CaiDatTimKiem(cboHangHoa, dtDanhSachHang, "HienThi", "MaHang", () =>
                {
                    cboHangHoa_SelectedIndexChanged(null, null);
                });

                cboHangHoa_SelectedIndexChanged(null, null);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi nạp danh sách hàng hóa: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void cboKho_SelectedIndexChanged(object sender, EventArgs e)
        {
            int maKhoMoi = 0;
            if (cboKho.SelectedValue is DataRowView drv && drv["MaKho"] != DBNull.Value)
            {
                maKhoMoi = Convert.ToInt32(drv["MaKho"]);
            }
            else if (cboKho.SelectedValue != null && int.TryParse(cboKho.SelectedValue.ToString(), out int k))
            {
                maKhoMoi = k;
            }

            if (maKhoMoi > 0)
            {
                if (dtHangKiem != null && dtHangKiem.Rows.Count > 0)
                {
                    DialogResult dr = MessageBox.Show(
                        "Đổi kho sẽ cập nhật lại số lượng tồn sổ sách của các hàng hóa theo kho mới.\nBạn có muốn tiếp tục?",
                        "Xác nhận đổi kho",
                        MessageBoxButtons.YesNo,
                        MessageBoxIcon.Question);

                    if (dr == DialogResult.No) return;
                }

                NapDanhSachHangHoa();
                CapNhatLaiTonKhoTheoKhoMoi();
            }
        }

        private void CapNhatLaiTonKhoTheoKhoMoi()
        {
            if (dtHangKiem == null || dtHangKiem.Rows.Count == 0 || dtDanhSachHang == null) return;

            foreach (DataRow r in dtHangKiem.Rows)
            {
                if (r.RowState == DataRowState.Deleted) continue;

                int maHang = Convert.ToInt32(r["MaHang"]);
                DataRow[] tim = dtDanhSachHang.Select($"MaHang = {maHang}");
                if (tim != null && tim.Length > 0)
                {
                    decimal tonMoi = Convert.ToDecimal(tim[0]["TonKho"]);
                    r["TonKho"] = tonMoi;
                    decimal tt = Convert.ToDecimal(r["SoLuongThucTe"]);
                    decimal donGia = Convert.ToDecimal(r["DonGia"]);
                    decimal slLech = tt - tonMoi;
                    r["SlChenhLech"] = slLech;
                    r["GiaTriChenhLech"] = slLech * donGia;
                }
            }
            TinhToanTongKet();
        }

        private void cboHangHoa_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Không cần cập nhật nudSoLuongThucTe nữa do người dùng nhập SL thực tế trực tiếp trên bảng
        }

        #region Xử Lý Phân Trang Lưới Hàng Kiểm

        private void CapNhatPhanTrang(int pageToFocus = -1)
        {
            if (dtHangKiem == null) return;

            if (dgvHangKiem.IsCurrentCellInEditMode)
            {
                dgvHangKiem.EndEdit();
            }

            int tongSoBanGhi = dtHangKiem.Rows.Count;
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
                dtHangKiem.DefaultView.RowFilter = "";
                lblTrangHienTai.Text = "Trang 1 / 1";
                btnTrangTruoc.Enabled = false;
                btnTrangSau.Enabled = false;
                return;
            }

            int minSTT = (trangHienTai - 1) * soDongMoiTrang + 1;
            int maxSTT = trangHienTai * soDongMoiTrang;

            dtHangKiem.DefaultView.RowFilter = $"STT >= {minSTT} AND STT <= {maxSTT}";

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

        #endregion

        #region Thao Tác Thêm & Xóa Dòng Hàng

        private void btnThemHang_Click(object sender, EventArgs e)
        {
            if (dgvHangKiem.IsCurrentCellInEditMode)
            {
                dgvHangKiem.EndEdit();
            }

            if (cboHangHoa.SelectedValue == null || !int.TryParse(cboHangHoa.SelectedValue.ToString(), out int maHang))
            {
                MessageBox.Show("Vui lòng chọn hàng hóa!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            DataRow[] rows = dtDanhSachHang.Select($"MaHang = {maHang}");
            if (rows.Length == 0) return;

            DataRow rowHang = rows[0];
            decimal tonKho = Convert.ToDecimal(rowHang["TonKho"]);
            decimal donGia = Convert.ToDecimal(rowHang["DonGia"]);

            // Kiểm tra xem hàng này đã có trong bảng chưa
            DataRow[] daCo = dtHangKiem.Select($"MaHang = {maHang}");
            if (daCo.Length > 0)
            {
                int sttCuaHang = Convert.ToInt32(daCo[0]["STT"]);
                int pageChuaHang = (int)Math.Ceiling((double)sttCuaHang / soDongMoiTrang);
                CapNhatPhanTrang(pageChuaHang);

                dgvHangKiem.Focus();
                foreach (DataGridViewRow dgvRow in dgvHangKiem.Rows)
                {
                    if (dgvRow.Cells["colMaHangNoiBo"].Value?.ToString() == rowHang["MaHangNoiBo"]?.ToString())
                    {
                        if (dgvHangKiem.Columns.Contains("colSoLuongThucTe") && dgvHangKiem.Columns["colSoLuongThucTe"].Visible)
                        {
                            try
                            {
                                dgvHangKiem.CurrentCell = dgvRow.Cells["colSoLuongThucTe"];
                                dgvRow.Selected = true;
                                dgvHangKiem.BeginEdit(true);
                                if (dgvHangKiem.EditingControl is TextBox txtBox)
                                {
                                    txtBox.SelectAll();
                                }
                            }
                            catch
                            {
                            }
                        }
                        break;
                    }
                }

                MessageBox.Show($"Mặt hàng '{rowHang["TenHang"]}' đã có trong phiếu kiểm kê!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                // Thêm dòng mới
                DataRow rMoi = dtHangKiem.NewRow();
                rMoi["STT"] = dtHangKiem.Rows.Count + 1;
                rMoi["MaHang"] = maHang;
                rMoi["MaHangNoiBo"] = rowHang["MaHangNoiBo"];
                rMoi["TenHang"] = rowHang["TenHang"];
                rMoi["TenDVT"] = rowHang["TenDVT"];
                rMoi["TonKho"] = tonKho;
                rMoi["SoLuongThucTe"] = tonKho; // Mặc định khớp tồn sổ sách, người dùng chỉnh sửa trực tiếp trên lưới
                rMoi["SlChenhLech"] = 0;
                rMoi["DonGia"] = donGia;
                rMoi["GiaTriChenhLech"] = 0;
                dtHangKiem.Rows.Add(rMoi);

                TinhToanTongKet();

                // Chuyển tới trang cuối cùng (trang chứa dòng vừa thêm)
                int targetPage = (int)Math.Ceiling((double)dtHangKiem.Rows.Count / soDongMoiTrang);
                CapNhatPhanTrang(targetPage);

                // Cuộn đến dòng hàng vừa thêm và focus vào ô SL thực tế
                dgvHangKiem.Focus();
                foreach (DataGridViewRow dgvRow in dgvHangKiem.Rows)
                {
                    if (dgvRow.Cells["colMaHangNoiBo"].Value?.ToString() == rowHang["MaHangNoiBo"]?.ToString())
                    {
                        if (dgvHangKiem.Columns.Contains("colSoLuongThucTe") && dgvHangKiem.Columns["colSoLuongThucTe"].Visible)
                        {
                            try
                            {
                                dgvHangKiem.CurrentCell = dgvRow.Cells["colSoLuongThucTe"];
                                dgvRow.Selected = true;
                                dgvHangKiem.BeginEdit(true);
                                if (dgvHangKiem.EditingControl is TextBox txtBox)
                                {
                                    txtBox.SelectAll();
                                }
                            }
                            catch
                            {
                            }
                        }
                        break;
                    }
                }
            }
        }

        private void btnHienCot_Click(object sender, EventArgs e)
        {
            foreach (Form openForm in Application.OpenForms)
            {
                if (openForm is frmHienCotHangKiem)
                {
                    openForm.BringToFront();
                    openForm.Activate();
                    return;
                }
            }

            frmHienCotHangKiem frm = new frmHienCotHangKiem(dgvHangKiem);
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

        private void dgvHangKiem_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0 || e.RowIndex >= dgvHangKiem.Rows.Count) return;

            string tenHang = dgvHangKiem.Rows[e.RowIndex].Cells["colTenHang"].Value?.ToString();
            string maHang = dgvHangKiem.Rows[e.RowIndex].Cells["colMaHangNoiBo"].Value?.ToString();
            string tenHienThi = !string.IsNullOrWhiteSpace(tenHang) ? $"'{tenHang}' ({maHang})" : "mặt hàng này";

            DialogResult dr = MessageBox.Show(
                $"Bạn có chắc chắn muốn xóa {tenHienThi} khỏi phiếu kiểm kê?",
                "Xác nhận xóa",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            if (dr == DialogResult.Yes)
            {
                if (dgvHangKiem.IsCurrentCellInEditMode)
                {
                    dgvHangKiem.CancelEdit();
                }

                if (dgvHangKiem.Rows[e.RowIndex].DataBoundItem is DataRowView drv)
                {
                    drv.Row.Delete();
                    dtHangKiem.AcceptChanges();
                }
                else if (e.RowIndex < dtHangKiem.Rows.Count)
                {
                    dtHangKiem.Rows.RemoveAt(e.RowIndex);
                }

                DanhSoLaiStt();
                TinhToanTongKet();
                CapNhatPhanTrang();
            }
        }

        private void dgvHangKiem_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.ColumnIndex >= 0)
            {
                string colName = dgvHangKiem.Columns[e.ColumnIndex].Name;
                if (colName == "colSoLuongThucTe")
                {
                    dgvHangKiem.BeginEdit(true);
                    if (dgvHangKiem.EditingControl is TextBox txt)
                    {
                        txt.SelectAll();
                    }
                }
            }
        }

        private void ApDungSoLuongThucTe()
        {
            try
            {
                if (dgvHangKiem.CurrentCell == null) return;
                int rowIndex = dgvHangKiem.CurrentCell.RowIndex;
                if (rowIndex < 0 || rowIndex >= dgvHangKiem.Rows.Count) return;

                DataGridViewRow dgvRow = dgvHangKiem.Rows[rowIndex];
                DataRowView drv = dgvRow.DataBoundItem as DataRowView;
                DataRow row = drv != null ? drv.Row : (rowIndex < dtHangKiem.Rows.Count ? dtHangKiem.Rows[rowIndex] : null);
                if (row == null) return;

                // 1. Lấy chuỗi số lượng từ ô nhập liệu hoặc ô trên lưới
                string text = "";
                if (dgvHangKiem.EditingControl is TextBox txt)
                {
                    text = txt.Text;
                }
                else if (dgvHangKiem.Columns.Contains("colSoLuongThucTe") && dgvRow.Cells["colSoLuongThucTe"].Value != null)
                {
                    text = dgvRow.Cells["colSoLuongThucTe"].Value.ToString();
                }

                decimal tonKho = row["TonKho"] != DBNull.Value ? Convert.ToDecimal(row["TonKho"]) : 0;
                decimal donGia = row["DonGia"] != DBNull.Value ? Convert.ToDecimal(row["DonGia"]) : 0;

                decimal slThucTe = 0;
                string cleanStr = text.Replace(",", "").Replace(".", "").Trim();
                if (!string.IsNullOrEmpty(cleanStr) && decimal.TryParse(cleanStr, out decimal parsedVal))
                {
                    slThucTe = Math.Max(0, (int)Math.Floor(parsedVal));
                }
                else
                {
                    slThucTe = 0;
                }

                decimal slChenhLech = slThucTe - tonKho;
                decimal giaTriChenhLech = slChenhLech * donGia;

                // 2. Kết thúc edit trên DataGridView
                if (dgvHangKiem.IsCurrentCellInEditMode)
                {
                    dgvHangKiem.EndEdit();
                }

                // 3. Cập nhật dữ liệu vào DataRow / DataRowView
                if (drv != null)
                {
                    drv["SoLuongThucTe"] = slThucTe;
                    drv["SlChenhLech"] = slChenhLech;
                    drv["GiaTriChenhLech"] = giaTriChenhLech;
                    drv.EndEdit();
                }
                row["SoLuongThucTe"] = slThucTe;
                row["SlChenhLech"] = slChenhLech;
                row["GiaTriChenhLech"] = giaTriChenhLech;

                // 4. Cập nhật hiển thị lên các ô của lưới
                if (dgvHangKiem.Columns.Contains("colSoLuongThucTe"))
                {
                    dgvRow.Cells["colSoLuongThucTe"].Value = slThucTe;
                }
                if (dgvHangKiem.Columns.Contains("colSlChenhLech"))
                {
                    dgvRow.Cells["colSlChenhLech"].Value = slChenhLech;
                }
                if (dgvHangKiem.Columns.Contains("colGiaTriChenhLech"))
                {
                    dgvRow.Cells["colGiaTriChenhLech"].Value = giaTriChenhLech;
                }

                // 5. Kết thúc edit của BindingContext
                this.BindingContext[dtHangKiem]?.EndCurrentEdit();

                // 6. Cập nhật các trường tổng kết
                TinhToanTongKet();

                // 7. Refresh lại dòng để hiển thị giá trị mới ngay lập tức
                dgvHangKiem.InvalidateRow(rowIndex);
            }
            catch
            {
                // Bỏ qua tránh crash
            }
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (keyData == Keys.Enter && dgvHangKiem.IsCurrentCellInEditMode)
            {
                string colName = dgvHangKiem.CurrentCell != null ? dgvHangKiem.Columns[dgvHangKiem.CurrentCell.ColumnIndex].Name : "";
                if (colName == "colSoLuongThucTe")
                {
                    ApDungSoLuongThucTe();
                    return true;
                }
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }

        private void dgvHangKiem_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            if (e.Control is TextBox txt)
            {
                txt.KeyPress -= TxtSoLuongDgv_KeyPress;
                txt.KeyDown -= TxtSoLuongDgv_KeyDown;

                string colName = dgvHangKiem.CurrentCell != null ? dgvHangKiem.Columns[dgvHangKiem.CurrentCell.ColumnIndex].Name : "";
                if (colName == "colSoLuongThucTe")
                {
                    txt.KeyPress += TxtSoLuongDgv_KeyPress;
                    txt.KeyDown += TxtSoLuongDgv_KeyDown;
                }
            }
        }

        private void TxtSoLuongDgv_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void TxtSoLuongDgv_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;
                ApDungSoLuongThucTe();
            }
        }

        private void dgvHangKiem_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            if (isCalculating || e.RowIndex < 0 || e.RowIndex >= dgvHangKiem.Rows.Count) return;

            string colName = dgvHangKiem.Columns[e.ColumnIndex].Name;
            if (colName == "colSoLuongThucTe")
            {
                try
                {
                    isCalculating = true;
                    ApDungSoLuongThucTe();
                }
                finally
                {
                    isCalculating = false;
                }
            }
        }

        private void DanhSoLaiStt()
        {
            int stt = 1;
            for (int i = 0; i < dtHangKiem.Rows.Count; i++)
            {
                if (dtHangKiem.Rows[i].RowState != DataRowState.Deleted)
                {
                    dtHangKiem.Rows[i]["STT"] = stt++;
                }
            }
        }

        private void TinhToanTongKet()
        {
            decimal tongThucTe = 0;
            decimal tongGiaTriThucTe = 0;
            decimal tongLechTang = 0;
            decimal tongGiaTriLechTang = 0;
            decimal tongLechGiam = 0;
            decimal tongGiaTriLechGiam = 0;
            decimal tongChenhLech = 0;
            decimal tongGiaTriChenhLech = 0;

            foreach (DataRow r in dtHangKiem.Rows)
            {
                if (r.RowState == DataRowState.Deleted) continue;

                decimal tt = Convert.ToDecimal(r["SoLuongThucTe"]);
                decimal donGia = Convert.ToDecimal(r["DonGia"]);
                decimal slLech = Convert.ToDecimal(r["SlChenhLech"]);
                decimal giaTriLech = Convert.ToDecimal(r["GiaTriChenhLech"]);

                tongThucTe += tt;
                tongGiaTriThucTe += (tt * donGia);

                if (slLech > 0)
                {
                    tongLechTang += slLech;
                    tongGiaTriLechTang += giaTriLech;
                }
                else if (slLech < 0)
                {
                    tongLechGiam += Math.Abs(slLech);
                    tongGiaTriLechGiam += Math.Abs(giaTriLech);
                }

                tongChenhLech += slLech;
                tongGiaTriChenhLech += giaTriLech;
            }

            lblTongSoHangVal.Text = dtHangKiem.Rows.Count.ToString();
            lblTongThucTeVal.Text = $"{tongThucTe:N0}  |  {tongGiaTriThucTe:N0} đ";
            lblTongLechTangVal.Text = $"{tongLechTang:N0}  |  {tongGiaTriLechTang:N0} đ";
            lblTongLechGiamVal.Text = $"{tongLechGiam:N0}  |  {tongGiaTriLechGiam:N0} đ";
            lblTongChenhLechVal.Text = $"{tongChenhLech:N0}  |  {tongGiaTriChenhLech:N0} đ";

            // Chỉ tô đỏ khi có chênh lệch hoặc bất thường (cả lệch tăng và lệch giảm khi != 0 đều tô đỏ)
            bool coLechTang = (tongLechTang > 0 || tongGiaTriLechTang > 0);
            bool coLechGiam = (tongLechGiam > 0 || tongGiaTriLechGiam > 0);
            bool coChenhLech = (coLechTang || coLechGiam || tongChenhLech != 0 || tongGiaTriChenhLech != 0);

            lblTongThucTeVal.ForeColor = SystemColors.ControlText;
            lblTongThucTeVal.Font = new Font(lblTongThucTeVal.Font, FontStyle.Regular);

            if (coLechTang)
            {
                lblTongLechTangVal.ForeColor = Color.Red;
                lblTongLechTangVal.Font = new Font(lblTongLechTangVal.Font, FontStyle.Bold);
            }
            else
            {
                lblTongLechTangVal.ForeColor = SystemColors.ControlText;
                lblTongLechTangVal.Font = new Font(lblTongLechTangVal.Font, FontStyle.Regular);
            }

            if (coLechGiam)
            {
                lblTongLechGiamVal.ForeColor = Color.Red;
                lblTongLechGiamVal.Font = new Font(lblTongLechGiamVal.Font, FontStyle.Bold);
            }
            else
            {
                lblTongLechGiamVal.ForeColor = SystemColors.ControlText;
                lblTongLechGiamVal.Font = new Font(lblTongLechGiamVal.Font, FontStyle.Regular);
            }

            if (coChenhLech)
            {
                lblTongChenhLechVal.ForeColor = Color.Red;
                lblTongChenhLechVal.Font = new Font(lblTongChenhLechVal.Font, FontStyle.Bold);
            }
            else
            {
                lblTongChenhLechVal.ForeColor = SystemColors.ControlText;
                lblTongChenhLechVal.Font = new Font(lblTongChenhLechVal.Font, FontStyle.Regular);
            }
        }

        #endregion

        #region Chức Năng Nhập File

        private void btnNhapFile_Click(object sender, EventArgs e)
        {
            using (frmNhapKiemKhoFile frm = new frmNhapKiemKhoFile())
            {
                if (frm.ShowDialog() == DialogResult.OK && frm.DtKetQuaNhap != null && frm.DtKetQuaNhap.Rows.Count > 0)
                {
                    int demThemMoi = 0;
                    int demCapNhat = 0;

                    foreach (DataRow rFile in frm.DtKetQuaNhap.Rows)
                    {
                        int maHang = Convert.ToInt32(rFile["MaHang"]);
                        decimal slThucTe = Convert.ToDecimal(rFile["SoLuongThucTe"]);

                        // Tìm trong danh sách hàng hóa của kho
                        DataRow[] hangTrongKho = dtDanhSachHang.Select($"MaHang = {maHang}");
                        decimal tonKho = 0;
                        decimal donGia = Convert.ToDecimal(rFile["DonGia"]);
                        string maHangNoiBo = rFile["MaHangNoiBo"].ToString();
                        string tenHang = rFile["TenHang"].ToString();
                        string tenDvt = rFile["TenDVT"].ToString();

                        if (hangTrongKho != null && hangTrongKho.Length > 0)
                        {
                            tonKho = Convert.ToDecimal(hangTrongKho[0]["TonKho"]);
                            if (donGia == 0) donGia = Convert.ToDecimal(hangTrongKho[0]["DonGia"]);
                        }

                        // Tìm trong dtHangKiem
                        DataRow[] daCo = dtHangKiem.Select($"MaHang = {maHang}");
                        if (daCo.Length > 0)
                        {
                            daCo[0]["SoLuongThucTe"] = slThucTe;
                            decimal slLech = slThucTe - tonKho;
                            daCo[0]["SlChenhLech"] = slLech;
                            daCo[0]["GiaTriChenhLech"] = slLech * donGia;
                            demCapNhat++;
                        }
                        else
                        {
                            DataRow rMoi = dtHangKiem.NewRow();
                            rMoi["STT"] = dtHangKiem.Rows.Count + 1;
                            rMoi["MaHang"] = maHang;
                            rMoi["MaHangNoiBo"] = maHangNoiBo;
                            rMoi["TenHang"] = tenHang;
                            rMoi["TenDVT"] = tenDvt;
                            rMoi["TonKho"] = tonKho;
                            rMoi["SoLuongThucTe"] = slThucTe;
                            decimal slLech = slThucTe - tonKho;
                            rMoi["SlChenhLech"] = slLech;
                            rMoi["DonGia"] = donGia;
                            rMoi["GiaTriChenhLech"] = slLech * donGia;
                            dtHangKiem.Rows.Add(rMoi);
                            demThemMoi++;
                        }
                    }

                    DanhSoLaiStt();
                    TinhToanTongKet();
                    trangHienTai = 1;
                    CapNhatPhanTrang();

                    MessageBox.Show($"Nạp file thành công!\nThêm mới: {demThemMoi} dòng | Cập nhật: {demCapNhat} dòng.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }

        #endregion

        #region Lưu Tạm & Cân Bằng Kho

        private void btnLuuTam_Click(object sender, EventArgs e)
        {
            LuuPhieuKiemKho(trangThaiMoi: "NHAP");
        }

        private void btnCanBangKho_Click(object sender, EventArgs e)
        {
            if (dgvHangKiem.IsCurrentCellInEditMode)
            {
                dgvHangKiem.EndEdit();
            }
            this.BindingContext[dtHangKiem]?.EndCurrentEdit();

            if (dtHangKiem.Rows.Count == 0)
            {
                MessageBox.Show("Vui lòng thêm ít nhất một mặt hàng để cân bằng kho!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            DialogResult dr = MessageBox.Show(
                "Bạn có chắc chắn muốn cân bằng kho?\nHành động này sẽ cập nhật tồn kho thực tế vào sổ sách và khóa vĩnh viễn phiếu kiểm kê (không thể hủy/sửa)!",
                "Xác nhận cân bằng kho",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            if (dr == DialogResult.Yes)
            {
                LuuPhieuKiemKho(trangThaiMoi: "DA_DIEU_CHINH");
            }
        }

        private void LuuPhieuKiemKho(string trangThaiMoi)
        {
            if (dgvHangKiem.IsCurrentCellInEditMode)
            {
                dgvHangKiem.EndEdit();
            }
            this.BindingContext[dtHangKiem]?.EndCurrentEdit();

            if (dtHangKiem.Rows.Count == 0)
            {
                MessageBox.Show("Vui lòng thêm ít nhất một mặt hàng vào phiếu kiểm kê!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            int maKho = 1;
            if (cboKho.SelectedValue is DataRowView drvKho && drvKho["MaKho"] != DBNull.Value)
            {
                maKho = Convert.ToInt32(drvKho["MaKho"]);
            }
            else if (cboKho.SelectedValue != null && int.TryParse(cboKho.SelectedValue.ToString(), out int k))
            {
                maKho = k;
            }

            int maNhanVien = 1;
            if (cboNguoiKiem.SelectedValue is DataRowView drvNv && drvNv["MaNhanVien"] != DBNull.Value)
            {
                maNhanVien = Convert.ToInt32(drvNv["MaNhanVien"]);
            }
            else if (cboNguoiKiem.SelectedValue != null && int.TryParse(cboNguoiKiem.SelectedValue.ToString(), out int nvId))
            {
                maNhanVien = nvId;
            }
            else if (SessionContext.MaNV.HasValue && SessionContext.MaNV.Value > 0)
            {
                maNhanVien = SessionContext.MaNV.Value;
            }

            DateTime thoiDiemChot = dtpThoiDiemChot.Value;
            string ghiChu = txtGhiChu.Text.Trim();
            string maPhieu = txtMaPhieu.Text.Trim();
            if (string.IsNullOrEmpty(maPhieu))
            {
                MessageBox.Show("Mã phiếu không được để trống!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtMaPhieu.Focus();
                return;
            }

            string checkSql = "SELECT COUNT(1) FROM PhieuKiemKe WHERE MaPhieu = @MaPhieu";
            object exist = DataProcessing.ReadValue(checkSql, new SqlParameter[] { new SqlParameter("@MaPhieu", maPhieu) });
            if (Convert.ToInt32(exist) > 0)
            {
                MessageBox.Show(string.Format("Mã phiếu '{0}' đã tồn tại trong hệ thống! Vui lòng chọn mã khác.", maPhieu), "Trùng mã", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtMaPhieu.Focus();
                return;
            }

            try
            {
                Cursor.Current = Cursors.WaitCursor;

                DataProcessing.ChangeDataTransaction((conn, trans) =>
                {
                    // 1. Thêm PhieuKiemKe
                    string sqlPkk = @"
                        INSERT INTO PhieuKiemKe (MaKho, MaNVLap, ThoiDiemChot, TrangThai, GhiChu, MaPhieu, DaDanhDau)
                        VALUES (@MaKho, @MaNVLap, @ThoiDiemChot, @TrangThai, @GhiChu, @MaPhieu, 0);
                        SELECT SCOPE_IDENTITY();";

                    int newMaPhieuKiemKe = 0;
                    using (SqlCommand cmdPkk = new SqlCommand(sqlPkk, conn, trans))
                    {
                        cmdPkk.Parameters.AddWithValue("@MaKho", maKho);
                        cmdPkk.Parameters.AddWithValue("@MaNVLap", maNhanVien);
                        cmdPkk.Parameters.AddWithValue("@ThoiDiemChot", thoiDiemChot);
                        cmdPkk.Parameters.AddWithValue("@TrangThai", trangThaiMoi);
                        cmdPkk.Parameters.AddWithValue("@GhiChu", (object)ghiChu ?? DBNull.Value);
                        cmdPkk.Parameters.AddWithValue("@MaPhieu", maPhieu);

                        object result = cmdPkk.ExecuteScalar();
                        newMaPhieuKiemKe = Convert.ToInt32(result);
                    }

                    // 2. Thêm từng dòng ChiTietPhieuKiemKe
                    var listDongDieuChinh = new List<Tuple<int, decimal>>(); // MaCTKiemKe, SoLuongDieuChinh

                    foreach (DataRow r in dtHangKiem.Rows)
                    {
                        if (r.RowState == DataRowState.Deleted) continue;

                        int maHang = Convert.ToInt32(r["MaHang"]);
                        decimal tonKho = Convert.ToDecimal(r["TonKho"]);
                        decimal thucTe = Convert.ToDecimal(r["SoLuongThucTe"]);
                        decimal donGia = Convert.ToDecimal(r["DonGia"]);
                        decimal slChenhLech = thucTe - tonKho;

                        string sqlCtpkk = @"
                            INSERT INTO ChiTietPhieuKiemKe (MaPhieuKiemKe, MaHang, SoLuongTheoSo, SoLuongThucTe, DonGia)
                            VALUES (@MaPhieuKiemKe, @MaHang, @SoLuongTheoSo, @SoLuongThucTe, @DonGia);
                            SELECT SCOPE_IDENTITY();";

                        int newMaCtKiemKe = 0;
                        using (SqlCommand cmdCt = new SqlCommand(sqlCtpkk, conn, trans))
                        {
                            cmdCt.Parameters.AddWithValue("@MaPhieuKiemKe", newMaPhieuKiemKe);
                            cmdCt.Parameters.AddWithValue("@MaHang", maHang);
                            cmdCt.Parameters.AddWithValue("@SoLuongTheoSo", tonKho);
                            cmdCt.Parameters.AddWithValue("@SoLuongThucTe", thucTe);
                            cmdCt.Parameters.AddWithValue("@DonGia", donGia);

                            object ctRes = cmdCt.ExecuteScalar();
                            newMaCtKiemKe = Convert.ToInt32(ctRes);
                        }

                        // Nếu cân bằng kho và có chênh lệch thì đưa vào danh sách điều chỉnh
                        if (trangThaiMoi == "DA_DIEU_CHINH" && slChenhLech != 0)
                        {
                            listDongDieuChinh.Add(new Tuple<int, decimal>(newMaCtKiemKe, slChenhLech));
                        }
                    }

                    // 3. Nếu Cân bằng kho: Sinh PhieuDieuChinh và ChiTietPhieuDieuChinh
                    if (trangThaiMoi == "DA_DIEU_CHINH")
                    {
                        string sqlPdc = @"
                            INSERT INTO PhieuDieuChinh (MaPhieuKiemKe, MaNVLap, MaNVDuyet, NgayLap, NgayDuyet, TrangThai, GhiChu)
                            VALUES (@MaPhieuKiemKe, @MaNVLap, @MaNVDuyet, @ThoiDiemChot, @ThoiDiemChot, 'DA_DUYET', @GhiChu);
                            SELECT SCOPE_IDENTITY();";

                        int newMaPhieuDieuChinh = 0;
                        using (SqlCommand cmdPdc = new SqlCommand(sqlPdc, conn, trans))
                        {
                            cmdPdc.Parameters.AddWithValue("@MaPhieuKiemKe", newMaPhieuKiemKe);
                            cmdPdc.Parameters.AddWithValue("@MaNVLap", maNhanVien);
                            cmdPdc.Parameters.AddWithValue("@MaNVDuyet", maNhanVien);
                            cmdPdc.Parameters.AddWithValue("@ThoiDiemChot", thoiDiemChot);
                            cmdPdc.Parameters.AddWithValue("@GhiChu", $"Cân bằng kiểm kê phiếu {maPhieu}");

                            object pdcRes = cmdPdc.ExecuteScalar();
                            newMaPhieuDieuChinh = Convert.ToInt32(pdcRes);
                        }

                        // Thêm các dòng ChiTietPhieuDieuChinh
                        foreach (var item in listDongDieuChinh)
                        {
                            string sqlCtpdc = @"
                                INSERT INTO ChiTietPhieuDieuChinh (MaPhieuDieuChinh, MaCTKiemKe, SoLuongDieuChinh)
                                VALUES (@MaPhieuDieuChinh, @MaCTKiemKe, @SoLuongDieuChinh)";

                            using (SqlCommand cmdCtpdc = new SqlCommand(sqlCtpdc, conn, trans))
                            {
                                cmdCtpdc.Parameters.AddWithValue("@MaPhieuDieuChinh", newMaPhieuDieuChinh);
                                cmdCtpdc.Parameters.AddWithValue("@MaCTKiemKe", item.Item1);
                                cmdCtpdc.Parameters.AddWithValue("@SoLuongDieuChinh", item.Item2);
                                cmdCtpdc.ExecuteNonQuery();
                            }
                        }
                    }

                    // 4. Ghi nhật ký thao tác
                    string sqlLog = @"
                        INSERT INTO NhatKyThaoTac (MaTaiKhoan, ThoiDiem, ThaoTac, LoaiDoiTuong, MaDoiTuong, NoiDung)
                        VALUES (@MaTaiKhoan, sysdatetime(), @ThaoTac, 'PhieuKiemKe', @MaDoiTuong, @NoiDung)";

                    using (SqlCommand cmdLog = new SqlCommand(sqlLog, conn, trans))
                    {
                        cmdLog.Parameters.AddWithValue("@MaTaiKhoan", SessionContext.MaTaiKhoan > 0 ? SessionContext.MaTaiKhoan : 1);
                        cmdLog.Parameters.AddWithValue("@ThaoTac", trangThaiMoi == "DA_DIEU_CHINH" ? "Cân bằng kho" : "Lưu tạm phiếu kiểm");
                        cmdLog.Parameters.AddWithValue("@MaDoiTuong", newMaPhieuKiemKe.ToString());
                        cmdLog.Parameters.AddWithValue("@NoiDung", trangThaiMoi == "DA_DIEU_CHINH" 
                            ? $"Cân bằng kiểm kho {maPhieu}, {dtHangKiem.Rows.Count} dòng kiểm, {listDongDieuChinh.Count} dòng điều chỉnh"
                            : $"Lưu tạm phiếu kiểm kho {maPhieu}, {dtHangKiem.Rows.Count} dòng kiểm");
                        cmdLog.ExecuteNonQuery();
                    }
                });

                string thongBao = (trangThaiMoi == "DA_DIEU_CHINH") 
                    ? $"Cân bằng kho thành công cho phiếu {maPhieu}!\nSố liệu tồn kho sổ sách đã được cập nhật chính xác."
                    : $"Lưu tạm phiếu kiểm kho {maPhieu} thành công!";

                MessageBox.Show(thongBao, "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                DaThayDoiDuLieu = true;
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi lưu phiếu kiểm kho: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                Cursor.Current = Cursors.Default;
            }
        }

        #endregion

        private void frmThemPhieuKiemKho_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                btnBoQua_Click(null, null);
                e.Handled = true;
            }
        }

        private void btnBoQua_Click(object sender, EventArgs e)
        {
            if (dgvHangKiem.IsCurrentCellInEditMode)
            {
                dgvHangKiem.CancelEdit();
            }

            if (dtHangKiem.Rows.Count > 0)
            {
                DialogResult dr = MessageBox.Show(
                    "Dữ liệu phiếu kiểm chưa được lưu. Bạn có chắc chắn muốn bỏ qua?",
                    "Xác nhận bỏ qua",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question);

                if (dr != DialogResult.Yes) return;
            }

            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void btnDong_Click(object sender, EventArgs e)
        {
            btnBoQua_Click(sender, e);
        }
    }
}
