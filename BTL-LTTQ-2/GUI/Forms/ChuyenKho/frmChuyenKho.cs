using BTL_LTTQ_2.Classes;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Text;
using System.Windows.Forms;

namespace BTL_LTTQ_2.GUI.Forms.ChuyenKho
{
    public partial class frmChuyenKho : Form
    {
        private DataTable dtDanhSach;
        private int trangHienTai = 1;
        private int soDongMoiTrang = 20;
        private int tongSoTrang = 1;
        private int tongSoDong = 0;
        private bool dangNapDuLieu = false;

        public frmChuyenKho()
        {
            InitializeComponent();
        }

        private void frmChuyenKho_Load(object sender, EventArgs e)
        {
            try
            {
                typeof(DataGridView).InvokeMember("DoubleBuffered",
                    System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.SetProperty,
                    null, dgvChuyenKho, new object[] { true });
            }
            catch { }

            dgvChuyenKho.AutoGenerateColumns = false;
            dgvChuyenKho.BorderStyle = BorderStyle.None;
            dgvChuyenKho.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(248, 250, 252);
            dgvChuyenKho.DefaultCellStyle.SelectionBackColor = Color.FromArgb(224, 231, 255);
            dgvChuyenKho.DefaultCellStyle.SelectionForeColor = Color.FromArgb(30, 41, 59);

            colChon.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            colMaPhieu.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            colNgayLap.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            colTongSlChuyen.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            colTongSlChuyen.DefaultCellStyle.Format = "N0";
            colTongSlNhan.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            colTongGiaTri.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            colTongGiaTri.DefaultCellStyle.Format = "N0";
            colTrangThai.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            // Cấu hình co giãn các cột tự động lấp đầy DataGridView theo tỷ lệ
            colMaPhieu.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            colMaPhieu.FillWeight = 120;
            colNgayLap.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            colNgayLap.FillWeight = 140;
            colKhoNguon.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            colKhoNguon.FillWeight = 180;
            colKhoDich.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            colKhoDich.FillWeight = 180;
            colTongSlChuyen.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            colTongSlChuyen.FillWeight = 110;
            colTongSlNhan.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            colTongSlNhan.FillWeight = 110;
            colTongGiaTri.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            colTongGiaTri.FillWeight = 130;
            colNguoiTao.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            colNguoiTao.FillWeight = 140;
            colGhiChu.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            colGhiChu.FillWeight = 180;
            colTrangThai.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            colTrangThai.FillWeight = 120;

            dangNapDuLieu = true;

            // Thiết lập mặc định lọc thời gian: 30 ngày gần nhất
            dtpTuNgay.Value = DateTime.Now.Date.AddDays(-30);
            dtpDenNgay.Value = DateTime.Now.Date.AddDays(1).AddSeconds(-1);

            NapDuLieuBoLoc();

            cboSoDongMoiTrang.SelectedIndex = 2; // 20
            dangNapDuLieu = false;

            TaiDuLieu();
        }

        private void NapDuLieuBoLoc()
        {
            // 1. Trạng thái
            cboTrangThai.Items.Clear();
            cboTrangThai.Items.Add("-- Tất cả trạng thái --");
            cboTrangThai.Items.Add("Đã nhận");
            cboTrangThai.Items.Add("Đang chuyển");
            cboTrangThai.Items.Add("Phiếu tạm");
            cboTrangThai.Items.Add("Đã hủy");
            cboTrangThai.SelectedIndex = 0;

            // 2. Hướng chuyển
            cboHuongChuyen.Items.Clear();
            cboHuongChuyen.Items.Add("-- Tất cả hướng chuyển --");
            cboHuongChuyen.Items.Add("Chuyển đi (từ kho này)");
            cboHuongChuyen.Items.Add("Chuyển đến (về kho này)");
            cboHuongChuyen.SelectedIndex = 0;

            // 3. Kho đối ứng
            string sqlKho = "SELECT MaKho, TenKho FROM Kho WHERE TrangThai = 'HOAT_DONG' OR TrangThai = 'ACTIVE' ORDER BY MaKho ASC";
            DataTable dtKho = DataProcessing.ReadData(sqlKho);
            DataRow rowTatCaKho = dtKho.NewRow();
            rowTatCaKho["MaKho"] = 0;
            rowTatCaKho["TenKho"] = "-- Tất cả kho đối ứng --";
            dtKho.Rows.InsertAt(rowTatCaKho, 0);

            ComboboxHelper.CaiDatTimKiem(cboKhoDoiUng, dtKho, "TenKho", "MaKho", () => {
                trangHienTai = 1;
                TaiDuLieu();
            });
            cboKhoDoiUng.SelectedValue = 0;

            // 4. Người tạo
            string sqlNguoiTao = @"SELECT tk.MaNV, ISNULL(nv.HoTen, tk.TenDangNhap) AS HoTen
                                  FROM TaiKhoan tk
                                  LEFT JOIN NhanVien nv ON tk.MaNV = nv.MaNV
                                  WHERE tk.TrangThai = 'HOAT_DONG' OR tk.TrangThai = 'ACTIVE' OR tk.TrangThai IS NULL
                                  ORDER BY HoTen ASC";
            DataTable dtNguoiTao = DataProcessing.ReadData(sqlNguoiTao);
            DataRow rowTatCaNguoiTao = dtNguoiTao.NewRow();
            rowTatCaNguoiTao["MaNV"] = 0;
            rowTatCaNguoiTao["HoTen"] = "-- Tất cả người tạo --";
            dtNguoiTao.Rows.InsertAt(rowTatCaNguoiTao, 0);

            ComboboxHelper.CaiDatTimKiem(cboNguoiTao, dtNguoiTao, "HoTen", "MaNV", () => {
                trangHienTai = 1;
                TaiDuLieu();
            });
            cboNguoiTao.SelectedValue = 0;
        }

        public void TaiDuLieu()
        {
            if (dangNapDuLieu) return;

            try
            {
                StringBuilder sbWhere = new StringBuilder(" WHERE 1=1 ");
                SqlParameter[] paras = new SqlParameter[10];
                int paramIndex = 0;

                // 1. Khoảng thời gian
                sbWhere.Append(" AND pc.NgayLap >= @TuNgay AND pc.NgayLap <= @DenNgay ");
                paras[paramIndex++] = new SqlParameter("@TuNgay", dtpTuNgay.Value.Date);
                paras[paramIndex++] = new SqlParameter("@DenNgay", dtpDenNgay.Value.Date.AddDays(1).AddSeconds(-1));

                // 2. Tìm kiếm từ khóa
                if (!string.IsNullOrWhiteSpace(txtTimKiem.Text))
                {
                    sbWhere.Append(" AND (pc.MaPhieu LIKE @TuKhoa OR pc.GhiChu LIKE @TuKhoa OR kn.TenKho LIKE @TuKhoa OR kd.TenKho LIKE @TuKhoa) ");
                    paras[paramIndex++] = new SqlParameter("@TuKhoa", "%" + txtTimKiem.Text.Trim() + "%");
                }

                // 3. Trạng thái
                if (cboTrangThai.SelectedIndex > 0)
                {
                    string tt = cboTrangThai.Text;
                    string maTT = "";
                    if (tt == "Đã nhận") maTT = "DA_NHAN";
                    else if (tt == "Đang chuyển") maTT = "DA_XUAT";
                    else if (tt == "Phiếu tạm") maTT = "NHAP";
                    else if (tt == "Đã hủy") maTT = "HUY";

                    if (!string.IsNullOrEmpty(maTT))
                    {
                        sbWhere.Append(" AND pc.TrangThai = @TrangThai ");
                        paras[paramIndex++] = new SqlParameter("@TrangThai", maTT);
                    }
                }

                // 4. Hướng chuyển & Kho làm việc hiện tại
                int khoHienTai = SessionContext.MaKhoHienTai;
                if (cboHuongChuyen.SelectedIndex == 1) // Chuyển đi
                {
                    sbWhere.Append(" AND pc.MaKhoNguon = @KhoHienTai ");
                    paras[paramIndex++] = new SqlParameter("@KhoHienTai", khoHienTai);
                }
                else if (cboHuongChuyen.SelectedIndex == 2) // Chuyển đến
                {
                    sbWhere.Append(" AND pc.MaKhoDich = @KhoHienTai ");
                    paras[paramIndex++] = new SqlParameter("@KhoHienTai", khoHienTai);
                }
                else
                {
                    // Nếu không phải admin: mặc định chỉ thấy phiếu liên quan đến kho hiện tại
                    if (!SessionContext.IsAdmin && khoHienTai > 0)
                    {
                        sbWhere.Append(" AND (pc.MaKhoNguon = @KhoHienTai OR pc.MaKhoDich = @KhoHienTai) ");
                        paras[paramIndex++] = new SqlParameter("@KhoHienTai", khoHienTai);
                    }
                }

                // 5. Kho đối ứng
                if (cboKhoDoiUng.SelectedValue != null && int.TryParse(cboKhoDoiUng.SelectedValue.ToString(), out int maKhoDoiUng) && maKhoDoiUng > 0)
                {
                    sbWhere.Append(" AND (pc.MaKhoNguon = @KhoDoiUng OR pc.MaKhoDich = @KhoDoiUng) ");
                    paras[paramIndex++] = new SqlParameter("@KhoDoiUng", maKhoDoiUng);
                }

                // 6. Người tạo
                if (cboNguoiTao.SelectedValue != null && int.TryParse(cboNguoiTao.SelectedValue.ToString(), out int maNvTao) && maNvTao > 0)
                {
                    sbWhere.Append(" AND pc.MaNVLap = @MaNVTao ");
                    paras[paramIndex++] = new SqlParameter("@MaNVTao", maNvTao);
                }

                // 7. Đếm tổng số dòng
                string sqlCount = @"SELECT COUNT(*) 
                                   FROM PhieuChuyenKho pc
                                   JOIN Kho kn ON pc.MaKhoNguon = kn.MaKho
                                   JOIN Kho kd ON pc.MaKhoDich = kd.MaKho
                                   LEFT JOIN NhanVien nv ON pc.MaNVLap = nv.MaNV " + sbWhere.ToString();

                SqlParameter[] countParams = new SqlParameter[paramIndex];
                for (int i = 0; i < paramIndex; i++)
                {
                    countParams[i] = new SqlParameter(paras[i].ParameterName, paras[i].Value);
                }

                object countObj = DataProcessing.ReadValue(sqlCount, countParams);
                tongSoDong = countObj != null && countObj != DBNull.Value ? Convert.ToInt32(countObj) : 0;

                tongSoTrang = (int)Math.Ceiling((double)tongSoDong / soDongMoiTrang);
                if (tongSoTrang <= 0) tongSoTrang = 1;
                if (trangHienTai > tongSoTrang) trangHienTai = tongSoTrang;

                int offset = (trangHienTai - 1) * soDongMoiTrang;

                // 8. Đọc dữ liệu phân trang (tạo mảng SqlParameter mới độc lập để tránh lỗi SqlParameterCollection)
                SqlParameter[] dataParams = new SqlParameter[paramIndex + 2];
                for (int i = 0; i < paramIndex; i++)
                {
                    dataParams[i] = new SqlParameter(paras[i].ParameterName, paras[i].Value);
                }
                dataParams[paramIndex] = new SqlParameter("@Offset", offset);
                dataParams[paramIndex + 1] = new SqlParameter("@Fetch", soDongMoiTrang);

                string sqlData = @"SELECT pc.MaPhieuChuyen, pc.MaPhieu, pc.NgayLap, pc.TrangThai,
                                         pc.MaKhoNguon, kn.TenKho AS TenKhoNguon,
                                         pc.MaKhoDich, kd.TenKho AS TenKhoDich,
                                         pc.TongSoLuongChuyen, pc.TongSoLuongNhan, pc.TongGiaTriChuyen,
                                         ISNULL(nv.HoTen, N'Chưa rõ') AS TenNguoiTao,
                                         pc.GhiChu
                                  FROM PhieuChuyenKho pc
                                  JOIN Kho kn ON pc.MaKhoNguon = kn.MaKho
                                  JOIN Kho kd ON pc.MaKhoDich = kd.MaKho
                                  LEFT JOIN NhanVien nv ON pc.MaNVLap = nv.MaNV "
                                  + sbWhere.ToString() +
                                  @" ORDER BY pc.NgayLap DESC, pc.MaPhieuChuyen DESC
                                     OFFSET @Offset ROWS FETCH NEXT @Fetch ROWS ONLY";

                dtDanhSach = DataProcessing.ReadData(sqlData, dataParams);

                dtDanhSach.Columns.Add("NgayLapHienThi", typeof(string));
                dtDanhSach.Columns.Add("TongSoLuongNhanHienThi", typeof(string));
                dtDanhSach.Columns.Add("TrangThaiHienThi", typeof(string));

                foreach (DataRow r in dtDanhSach.Rows)
                {
                    r["NgayLapHienThi"] = r["NgayLap"] != DBNull.Value ? Convert.ToDateTime(r["NgayLap"]).ToString("dd/MM/yyyy HH:mm") : "";

                    string tt = r["TrangThai"] != DBNull.Value ? r["TrangThai"].ToString() : "";
                    if (tt == "DA_NHAN")
                    {
                        r["TrangThaiHienThi"] = "Đã nhận";
                        decimal slNhan = 0;
                        if (r["TongSoLuongNhan"] != DBNull.Value && decimal.TryParse(r["TongSoLuongNhan"].ToString(), out slNhan))
                        {
                            r["TongSoLuongNhanHienThi"] = slNhan.ToString("N0");
                        }
                        else
                        {
                            r["TongSoLuongNhanHienThi"] = "---";
                        }
                    }
                    else if (tt == "DA_XUAT")
                    {
                        r["TrangThaiHienThi"] = "Đang chuyển";
                        r["TongSoLuongNhanHienThi"] = "---";
                    }
                    else if (tt == "NHAP")
                    {
                        r["TrangThaiHienThi"] = "Phiếu tạm";
                        r["TongSoLuongNhanHienThi"] = "---";
                    }
                    else
                    {
                        r["TrangThaiHienThi"] = "Đã hủy";
                        r["TongSoLuongNhanHienThi"] = "---";
                    }
                }

                dgvChuyenKho.DataSource = dtDanhSach;

                lblTongSoPhieu.Text = string.Format("Tổng số: {0:N0} phiếu chuyển", tongSoDong);
                lblTrangHienTai.Text = string.Format("{0} / {1}", trangHienTai, tongSoTrang);

                btnTrangTruoc.Enabled = trangHienTai > 1;
                btnTrangSau.Enabled = trangHienTai < tongSoTrang;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi tải danh sách phiếu chuyển kho: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void dgvChuyenKho_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.RowIndex < 0 || e.RowIndex >= dgvChuyenKho.Rows.Count) return;

            string colName = dgvChuyenKho.Columns[e.ColumnIndex].Name;
            if (colName == "colTrangThai")
            {
                if (e.Value != null)
                {
                    string tt = e.Value.ToString();
                    // 3 nhóm màu trạng thái chuẩn (Điều luật 15b, font chữ thường đồng bộ theo mẫu Trả hàng nhập)
                    if (tt == "Đã nhận")
                    {
                        e.CellStyle.ForeColor = Color.FromArgb(22, 163, 74); // Xanh lá
                    }
                    else if (tt == "Đang chuyển" || tt == "Phiếu tạm")
                    {
                        e.CellStyle.ForeColor = Color.FromArgb(217, 119, 6); // Vàng amber
                    }
                    else if (tt == "Đã hủy")
                    {
                        e.CellStyle.ForeColor = Color.FromArgb(100, 116, 139); // Xám nhạt
                    }
                }
            }
            else if (colName == "colTongSlNhan")
            {
                DataRowView rowView = dgvChuyenKho.Rows[e.RowIndex].DataBoundItem as DataRowView;
                if (rowView != null)
                {
                    string tt = rowView["TrangThai"].ToString();
                    if (tt == "DA_NHAN")
                    {
                        decimal slChuyen = Convert.ToDecimal(rowView["TongSoLuongChuyen"]);
                        decimal slNhan = Convert.ToDecimal(rowView["TongSoLuongNhan"]);
                        if (slNhan < slChuyen)
                        {
                            e.CellStyle.ForeColor = Color.Red;
                            e.CellStyle.Font = new Font(dgvChuyenKho.Font, FontStyle.Bold);
                        }
                    }
                }
            }
        }

        private void dgvChuyenKho_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;

            DataRowView rowView = dgvChuyenKho.Rows[e.RowIndex].DataBoundItem as DataRowView;
            if (rowView != null)
            {
                int maPhieuChuyen = Convert.ToInt32(rowView["MaPhieuChuyen"]);
                using (frmChiTietChuyenKho frm = new frmChiTietChuyenKho(maPhieuChuyen))
                {
                    frm.ShowDialog(this);
                    TaiDuLieu();
                }
            }
        }

        private void btnChuyenKho_Click(object sender, EventArgs e)
        {
            using (frmThemPhieuChuyenKho frm = new frmThemPhieuChuyenKho())
            {
                if (frm.ShowDialog(this) == DialogResult.OK)
                {
                    TaiDuLieu();
                }
            }
        }

        private void btnXuatFile_Click(object sender, EventArgs e)
        {
            if (dtDanhSach == null || dtDanhSach.Rows.Count == 0)
            {
                MessageBox.Show("Không có dữ liệu phiếu chuyển kho để xuất!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                SaveFileDialog sfd = new SaveFileDialog();
                sfd.Filter = "File CSV (*.csv)|*.csv";
                sfd.FileName = string.Format("Danh_Sach_Chuyen_Kho_{0}.csv", DateTime.Now.ToString("yyyyMMdd_HHmmss"));

                if (sfd.ShowDialog() == DialogResult.OK)
                {
                    StringBuilder sb = new StringBuilder();
                    sb.AppendLine("Mã chuyển kho,Thời gian,Kho gửi,Kho nhận,Tổng SL chuyển,Tổng SL nhận,Tổng giá trị,Người tạo,Trạng thái,Ghi chú");

                    foreach (DataRow r in dtDanhSach.Rows)
                    {
                        sb.AppendLine(string.Format("{0},{1},\"{2}\",\"{3}\",{4},{5},{6},\"{7}\",\"{8}\",\"{9}\"",
                            r["MaPhieu"],
                            r["NgayLapHienThi"],
                            r["TenKhoNguon"].ToString().Replace("\"", "\"\""),
                            r["TenKhoDich"].ToString().Replace("\"", "\"\""),
                            r["TongSoLuongChuyen"],
                            r["TongSoLuongNhanHienThi"],
                            r["TongGiaTriChuyen"],
                            r["TenNguoiTao"].ToString().Replace("\"", "\"\""),
                            r["TrangThaiHienThi"],
                            r["GhiChu"].ToString().Replace("\"", "\"\"")));
                    }

                    File.WriteAllText(sfd.FileName, sb.ToString(), new UTF8Encoding(true));
                    MessageBox.Show("Xuất file CSV danh sách chuyển kho thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi xuất file: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnHienCot_Click(object sender, EventArgs e)
        {
            foreach (Form openForm in Application.OpenForms)
            {
                if (openForm is frmHienCotChuyenKho)
                {
                    openForm.Activate();
                    openForm.BringToFront();
                    return;
                }
            }

            frmHienCotChuyenKho frm = new frmHienCotChuyenKho(dgvChuyenKho);
            frm.StartPosition = FormStartPosition.Manual;

            // Tính toạ độ hiển thị ngay phía dưới nút btnHienCot (căn thẳng mép phải nút)
            Point btnPos = btnHienCot.PointToScreen(Point.Empty);
            int posX = btnPos.X + btnHienCot.Width - frm.Width;
            int posY = btnPos.Y + btnHienCot.Height + 2;

            // Bảo đảm form không bị tràn ra ngoài màn hình làm việc
            Rectangle workingArea = Screen.FromControl(btnHienCot).WorkingArea;
            if (posX + frm.Width > workingArea.Right)
            {
                posX = workingArea.Right - frm.Width;
            }
            if (posX < workingArea.Left)
            {
                posX = workingArea.Left;
            }
            if (posY + frm.Height > workingArea.Bottom)
            {
                posY = btnPos.Y - frm.Height - 2;
            }

            frm.Location = new Point(posX, posY);
            frm.Show(this);
            frm.BringToFront();
        }

        private void btnHuongDanSuDung_Click(object sender, EventArgs e)
        {
            MessageBox.Show("HƯỚNG DẪN QUẢN LÝ CHUYỂN KHO:\n\n" +
                "1. Lập phiếu chuyển mới: Bấm nút '+ Chuyển kho', chọn kho gửi, kho nhận và các sản phẩm cần chuyển.\n" +
                "2. Quy tắc tồn kho:\n" +
                "   - 'Lưu tạm': Phiếu nháp, chưa trừ tồn kho gửi.\n" +
                "   - 'Lưu' (Chuyển hàng): Hệ thống kiểm tra và trừ tồn kho gửi ngay lập tức. Phiếu chuyển sang trạng thái 'Đang chuyển'.\n" +
                "3. Nhận hàng: Khi hàng về tới kho nhận, thủ kho kho nhận mở chi tiết phiếu và bấm 'Nhận hàng' để kiểm đếm và cộng tồn vào kho nhận.\n" +
                "4. Xem chi tiết: Nhấn đúp vào dòng phiếu trên bảng để xem chi tiết, nhận hàng, xuất file hoặc xóa/hủy.",
                "Hướng dẫn sử dụng Chuyển kho", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void txtTimKiem_TextChanged(object sender, EventArgs e)
        {
            trangHienTai = 1;
            TaiDuLieu();
        }

        private void BoLoc_ThayDoi(object sender, EventArgs e)
        {
            trangHienTai = 1;
            TaiDuLieu();
        }

        private void cboSoDongMoiTrang_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cboSoDongMoiTrang.SelectedItem != null && int.TryParse(cboSoDongMoiTrang.SelectedItem.ToString(), out int soDong))
            {
                soDongMoiTrang = soDong;
                trangHienTai = 1;
                TaiDuLieu();
            }
        }

        private void btnTrangTruoc_Click(object sender, EventArgs e)
        {
            if (trangHienTai > 1)
            {
                trangHienTai--;
                TaiDuLieu();
            }
        }

        private void btnTrangSau_Click(object sender, EventArgs e)
        {
            if (trangHienTai < tongSoTrang)
            {
                trangHienTai++;
                TaiDuLieu();
            }
        }
    }
}
