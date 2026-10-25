using BTL_LTTQ_2.Classes;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Text;
using System.Windows.Forms;

namespace BTL_LTTQ_2.GUI.Forms.KiemKho
{
    public partial class frmChiTietKiemKho : Form
    {
        private int maPhieuKiemKe;
        private string maPhieu;
        private string trangThai;
        private DataTable dtChiTietData;

        public bool DaThayDoiDuLieu { get; private set; } = false;

        public frmChiTietKiemKho(int maPhieuKiemKe)
        {
            InitializeComponent();
            this.maPhieuKiemKe = maPhieuKiemKe;
            CauHinhDataGridView();
        }

        private void CauHinhDataGridView()
        {
            // Bật DoubleBuffered chống nhấp nháy giật lag
            typeof(DataGridView).InvokeMember("DoubleBuffered",
                System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.SetProperty,
                null, dgvChiTiet, new object[] { true });

            dgvChiTiet.AutoGenerateColumns = false;
            dgvChiTiet.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(248, 250, 252);
            dgvChiTiet.DefaultCellStyle.SelectionBackColor = Color.FromArgb(224, 231, 255);
            dgvChiTiet.DefaultCellStyle.SelectionForeColor = Color.FromArgb(30, 41, 59);

            colStt.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            colMaHangNoiBo.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            colDonViTinh.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            colTonKho.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            colThucTe.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            colSlLech.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            colDonGia.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            colGiaTriLech.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;

            colTonKho.DefaultCellStyle.Format = "N0";
            colThucTe.DefaultCellStyle.Format = "N0";
            colSlLech.DefaultCellStyle.Format = "N0";
            colDonGia.DefaultCellStyle.Format = "N0";
            colGiaTriLech.DefaultCellStyle.Format = "N0";

            dgvChiTiet.CellFormatting += dgvChiTiet_CellFormatting;
        }

        private void dgvChiTiet_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.RowIndex < 0 || e.RowIndex >= dgvChiTiet.Rows.Count) return;

            string colName = dgvChiTiet.Columns[e.ColumnIndex].Name;
            if ((colName == "colSlLech" || colName == "colGiaTriLech") && e.Value != null)
            {
                if (decimal.TryParse(e.Value.ToString(), out decimal val) && val != 0)
                {
                    e.CellStyle.ForeColor = Color.Red;
                    e.CellStyle.Font = new Font(dgvChiTiet.Font, FontStyle.Bold);
                }
            }
        }

        private void frmChiTietKiemKho_Load(object sender, EventArgs e)
        {
            NapThongTinPhieu();
        }

        public void NapThongTinPhieu()
        {
            try
            {
                string sqlHeader = @"
                    SELECT 
                        pkk.MaPhieuKiemKe,
                        ISNULL(pkk.MaPhieu, 'PKK' + RIGHT('000000' + CAST(pkk.MaPhieuKiemKe AS VARCHAR(10)), 6)) AS MaPhieu,
                        pkk.MaKho,
                        ISNULL(k.TenKho, N'Kho trung tâm') AS TenKho,
                        pkk.MaNVLap,
                        ISNULL(nvLap.HoTen, N'Quản trị viên') AS TenNvLap,
                        pkk.ThoiDiemChot,
                        pkk.TrangThai,
                        ISNULL(pkk.GhiChu, '') AS GhiChu,
                        pdc.NgayDuyet AS NgayCanBang,
                        ISNULL(nvDuyet.HoTen, '') AS TenNvCanBang
                    FROM PhieuKiemKe pkk
                    LEFT JOIN Kho k ON pkk.MaKho = k.MaKho
                    LEFT JOIN NhanVien nvLap ON pkk.MaNVLap = nvLap.MaNV
                    LEFT JOIN PhieuDieuChinh pdc ON pdc.MaPhieuKiemKe = pkk.MaPhieuKiemKe AND pdc.TrangThai = 'DA_DUYET'
                    LEFT JOIN NhanVien nvDuyet ON pdc.MaNVDuyet = nvDuyet.MaNV
                    WHERE pkk.MaPhieuKiemKe = @MaPhieuKiemKe";

                SqlParameter[] pHeader = { new SqlParameter("@MaPhieuKiemKe", maPhieuKiemKe) };
                DataTable dtHeader = DataProcessing.ReadData(sqlHeader, pHeader);

                if (dtHeader == null || dtHeader.Rows.Count == 0)
                {
                    MessageBox.Show("Không tìm thấy thông tin phiếu kiểm kho này!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    this.Close();
                    return;
                }

                DataRow row = dtHeader.Rows[0];
                this.maPhieu = row["MaPhieu"].ToString();
                this.trangThai = row["TrangThai"].ToString();

                lblMaPhieu.Text = this.maPhieu;
                lblKho.Text = "Kho: " + row["TenKho"].ToString();
                txtGhiChu.Text = row["GhiChu"].ToString();

                string tenNvLap = row["TenNvLap"].ToString();
                DateTime thoiDiemChot = Convert.ToDateTime(row["ThoiDiemChot"]);
                lblNguoiTao.Text = "Người tạo: " + tenNvLap;
                lblNgayTao.Text = "Ngày tạo: " + thoiDiemChot.ToString("dd/MM/yyyy HH:mm");

                // Trạng thái và Người cân bằng
                if (trangThai == "DA_DIEU_CHINH")
                {
                    lblTrangThaiBadge.Text = "Đã cân bằng";
                    lblTrangThaiBadge.ForeColor = Color.FromArgb(22, 163, 74);

                    string tenNvCanBang = row["TenNvCanBang"].ToString();
                    if (string.IsNullOrEmpty(tenNvCanBang)) tenNvCanBang = tenNvLap;

                    string ngayCanBangStr = row["NgayCanBang"] != DBNull.Value 
                        ? Convert.ToDateTime(row["NgayCanBang"]).ToString("dd/MM/yyyy HH:mm") 
                        : thoiDiemChot.ToString("dd/MM/yyyy HH:mm");

                    lblNguoiCanBang.Text = "Người cân bằng: " + tenNvCanBang;
                    lblNgayCanBang.Text = "Ngày cân bằng: " + ngayCanBangStr;
                    lblNguoiCanBang.Visible = true;
                    lblNgayCanBang.Visible = true;

                    // Phiếu đã cân bằng: Không được phép xóa (Luật 6)
                    btnXoa.Enabled = false;
                    btnXoa.Text = "Xóa";
                    btnHoanThanh.Visible = false;
                }
                else if (trangThai == "NHAP" || trangThai == "DANG_KIEM" || trangThai == "DA_KIEM")
                {
                    lblTrangThaiBadge.Text = "Phiếu tạm";
                    lblTrangThaiBadge.ForeColor = Color.FromArgb(217, 119, 6);

                    lblNguoiCanBang.Text = "Người cân bằng: --";
                    lblNgayCanBang.Text = "Ngày cân bằng: --";
                    lblNguoiCanBang.Visible = true;
                    lblNgayCanBang.Visible = true;

                    btnXoa.Enabled = true;
                    btnXoa.Text = "Xóa";
                    btnHoanThanh.Visible = true;
                    btnHoanThanh.Enabled = true;
                }
                else // HUY
                {
                    lblTrangThaiBadge.Text = "Đã hủy";
                    lblTrangThaiBadge.ForeColor = Color.FromArgb(100, 116, 139);

                    lblNguoiCanBang.Visible = false;
                    lblNgayCanBang.Visible = false;

                    btnXoa.Enabled = false;
                    btnXoa.Text = "Xóa";
                    btnHoanThanh.Visible = false;
                }

                // Nạp chi tiết hàng hóa kiểm kê
                NapChiTietHangHoa();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi tải thông tin phiếu kiểm kho: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void NapChiTietHangHoa()
        {
            try
            {
                string sqlChiTiet = @"
                    SELECT 
                        ROW_NUMBER() OVER(ORDER BY ctpkk.MaCTKiemKe ASC) AS STT,
                        ctpkk.MaCTKiemKe,
                        ctpkk.MaHang,
                        h.MaHangNoiBo,
                        h.TenHang,
                        ISNULL(dvt.TenDVT, N'Cái') AS TenDVT,
                        ctpkk.SoLuongTheoSo AS TonKho,
                        ctpkk.SoLuongThucTe,
                        ctpkk.DonGia,
                        CASE 
                            WHEN ctpkk.SoLuongThucTe IS NOT NULL THEN (ctpkk.SoLuongThucTe - ctpkk.SoLuongTheoSo) 
                            ELSE 0 
                        END AS SlChenhLech,
                        CASE 
                            WHEN ctpkk.SoLuongThucTe IS NOT NULL THEN (ctpkk.SoLuongThucTe - ctpkk.SoLuongTheoSo) * ctpkk.DonGia 
                            ELSE 0 
                        END AS GiaTriChenhLech
                    FROM ChiTietPhieuKiemKe ctpkk
                    JOIN HangHoa h ON ctpkk.MaHang = h.MaHang
                    LEFT JOIN DonViTinh dvt ON h.MaDVT = dvt.MaDVT
                    WHERE ctpkk.MaPhieuKiemKe = @MaPhieuKiemKe
                    ORDER BY ctpkk.MaCTKiemKe ASC";

                SqlParameter[] pChiTiet = { new SqlParameter("@MaPhieuKiemKe", maPhieuKiemKe) };
                dtChiTietData = DataProcessing.ReadData(sqlChiTiet, pChiTiet);
                dgvChiTiet.DataSource = dtChiTietData;

                // Tính toán tổng kết
                decimal tongThucTe = 0;
                decimal tongGiaTriThucTe = 0;
                decimal tongLechTang = 0;
                decimal tongGiaTriLechTang = 0;
                decimal tongLechGiam = 0;
                decimal tongGiaTriLechGiam = 0;
                decimal tongChenhLech = 0;
                decimal tongGiaTriChenhLech = 0;

                if (dtChiTietData != null)
                {
                    foreach (DataRow r in dtChiTietData.Rows)
                    {
                        if (r["SoLuongThucTe"] != DBNull.Value)
                        {
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
                    }
                }

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
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi tải chi tiết dòng hàng: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        #region Thao Tác Nghiệp Vụ

        private void btnXoa_Click(object sender, EventArgs e)
        {
            if (trangThai == "DA_DIEU_CHINH")
            {
                MessageBox.Show("Phiếu kiểm kho đã cân bằng kho không được phép xóa theo Điều luật 6 quản lý kho!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            DialogResult dr = MessageBox.Show(
                $"Bạn có chắc chắn muốn xóa phiếu kiểm kho '{this.maPhieu}'?\nHành động này không thể hoàn tác!",
                "Xác nhận xóa phiếu",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            if (dr == DialogResult.Yes)
            {
                try
                {
                    DataProcessing.ChangeDataTransaction((conn, trans) =>
                    {
                        // 1. Xóa chi tiết kiểm kê
                        string sqlDeleteDetail = "DELETE FROM ChiTietPhieuKiemKe WHERE MaPhieuKiemKe = @MaPhieuKiemKe";
                        using (SqlCommand cmdDtl = new SqlCommand(sqlDeleteDetail, conn, trans))
                        {
                            cmdDtl.Parameters.AddWithValue("@MaPhieuKiemKe", maPhieuKiemKe);
                            cmdDtl.ExecuteNonQuery();
                        }

                        // 2. Xóa phiếu kiểm kê
                        string sqlDeleteHeader = "DELETE FROM PhieuKiemKe WHERE MaPhieuKiemKe = @MaPhieuKiemKe";
                        using (SqlCommand cmdHdr = new SqlCommand(sqlDeleteHeader, conn, trans))
                        {
                            cmdHdr.Parameters.AddWithValue("@MaPhieuKiemKe", maPhieuKiemKe);
                            cmdHdr.ExecuteNonQuery();
                        }

                        // 3. Ghi nhật ký thao tác
                        string sqlLog = @"
                            INSERT INTO NhatKyThaoTac (MaTaiKhoan, ThoiDiem, ThaoTac, LoaiDoiTuong, MaDoiTuong, NoiDung)
                            VALUES (@MaTaiKhoan, sysdatetime(), N'Xóa phiếu kiểm kho', 'PhieuKiemKe', @MaDoiTuong, @NoiDung)";
                        using (SqlCommand cmdLog = new SqlCommand(sqlLog, conn, trans))
                        {
                            cmdLog.Parameters.AddWithValue("@MaTaiKhoan", SessionContext.MaTaiKhoan > 0 ? SessionContext.MaTaiKhoan : 1);
                            cmdLog.Parameters.AddWithValue("@MaDoiTuong", maPhieuKiemKe.ToString());
                            cmdLog.Parameters.AddWithValue("@NoiDung", $"Xóa vĩnh viễn phiếu kiểm kho tạm {this.maPhieu}");
                            cmdLog.ExecuteNonQuery();
                        }
                    });

                    MessageBox.Show($"Đã xóa thành công phiếu kiểm kho {this.maPhieu}!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    DaThayDoiDuLieu = true;
                    this.DialogResult = DialogResult.OK;
                    this.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi khi xóa phiếu kiểm kho: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void btnHoanThanh_Click(object sender, EventArgs e)
        {
            if (trangThai == "DA_DIEU_CHINH" || trangThai == "HUY") return;

            if (dtChiTietData == null || dtChiTietData.Rows.Count == 0)
            {
                MessageBox.Show("Phiếu kiểm kho không có hàng hóa để cân bằng!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            DialogResult dr = MessageBox.Show(
                $"Bạn có chắc chắn muốn HOÀN THÀNH (Cân bằng kho) phiếu kiểm kê '{this.maPhieu}'?\n\nHành động này sẽ cập nhật tồn kho thực tế vào sổ sách và khóa vĩnh viễn phiếu kiểm kê (không thể hủy/sửa theo Luật 6)!",
                "Xác nhận hoàn thành kiểm kho",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            if (dr != DialogResult.Yes) return;

            try
            {
                int maNhanVien = (SessionContext.MaNV.HasValue && SessionContext.MaNV.Value > 0) ? SessionContext.MaNV.Value : 1;

                DataProcessing.ChangeDataTransaction((conn, trans) =>
                {
                    // 1. Cập nhật trạng thái PhieuKiemKe sang DA_DIEU_CHINH
                    string sqlUpdatePkk = @"
                        UPDATE PhieuKiemKe 
                        SET TrangThai = 'DA_DIEU_CHINH', 
                            ThoiDiemChot = sysdatetime() 
                        WHERE MaPhieuKiemKe = @MaPhieuKiemKe";

                    using (SqlCommand cmdPkk = new SqlCommand(sqlUpdatePkk, conn, trans))
                    {
                        cmdPkk.Parameters.AddWithValue("@MaPhieuKiemKe", maPhieuKiemKe);
                        cmdPkk.ExecuteNonQuery();
                    }

                    // 2. Tạo PhieuDieuChinh trạng thái DA_DUYET
                    string sqlPdc = @"
                        INSERT INTO PhieuDieuChinh (MaPhieuKiemKe, MaNVLap, MaNVDuyet, NgayLap, NgayDuyet, TrangThai, GhiChu)
                        VALUES (@MaPhieuKiemKe, @MaNVLap, @MaNVDuyet, sysdatetime(), sysdatetime(), 'DA_DUYET', @GhiChu);
                        SELECT SCOPE_IDENTITY();";

                    int newMaPhieuDieuChinh;
                    using (SqlCommand cmdPdc = new SqlCommand(sqlPdc, conn, trans))
                    {
                        cmdPdc.Parameters.AddWithValue("@MaPhieuKiemKe", maPhieuKiemKe);
                        cmdPdc.Parameters.AddWithValue("@MaNVLap", maNhanVien);
                        cmdPdc.Parameters.AddWithValue("@MaNVDuyet", maNhanVien);
                        cmdPdc.Parameters.AddWithValue("@GhiChu", $"Cân bằng kiểm kê phiếu {this.maPhieu}");
                        object pdcRes = cmdPdc.ExecuteScalar();
                        newMaPhieuDieuChinh = Convert.ToInt32(pdcRes);
                    }

                    // 3. Thêm các dòng ChiTietPhieuDieuChinh cho mặt hàng có chênh lệch
                    int countDieuChinh = 0;
                    foreach (DataRow r in dtChiTietData.Rows)
                    {
                        int maCTKiemKe = Convert.ToInt32(r["MaCTKiemKe"]);
                        decimal slChenhLech = Convert.ToDecimal(r["SlChenhLech"]);

                        if (slChenhLech != 0)
                        {
                            string sqlCtpdc = @"
                                INSERT INTO ChiTietPhieuDieuChinh (MaPhieuDieuChinh, MaCTKiemKe, SoLuongDieuChinh)
                                VALUES (@MaPhieuDieuChinh, @MaCTKiemKe, @SoLuongDieuChinh)";

                            using (SqlCommand cmdCtpdc = new SqlCommand(sqlCtpdc, conn, trans))
                            {
                                cmdCtpdc.Parameters.AddWithValue("@MaPhieuDieuChinh", newMaPhieuDieuChinh);
                                cmdCtpdc.Parameters.AddWithValue("@MaCTKiemKe", maCTKiemKe);
                                cmdCtpdc.Parameters.AddWithValue("@SoLuongDieuChinh", slChenhLech);
                                cmdCtpdc.ExecuteNonQuery();
                            }
                            countDieuChinh++;
                        }
                    }

                    // 4. Ghi nhật ký thao tác
                    string sqlLog = @"
                        INSERT INTO NhatKyThaoTac (MaTaiKhoan, ThoiDiem, ThaoTac, LoaiDoiTuong, MaDoiTuong, NoiDung)
                        VALUES (@MaTaiKhoan, sysdatetime(), N'Cân bằng kho', 'PhieuKiemKe', @MaDoiTuong, @NoiDung)";

                    using (SqlCommand cmdLog = new SqlCommand(sqlLog, conn, trans))
                    {
                        cmdLog.Parameters.AddWithValue("@MaTaiKhoan", SessionContext.MaTaiKhoan > 0 ? SessionContext.MaTaiKhoan : 1);
                        cmdLog.Parameters.AddWithValue("@MaDoiTuong", maPhieuKiemKe.ToString());
                        cmdLog.Parameters.AddWithValue("@NoiDung", $"Cân bằng kiểm kho {this.maPhieu}, {dtChiTietData.Rows.Count} dòng kiểm, {countDieuChinh} dòng điều chỉnh");
                        cmdLog.ExecuteNonQuery();
                    }
                });

                DaThayDoiDuLieu = true;
                this.DialogResult = DialogResult.OK;
                MessageBox.Show($"Cân bằng kho cho phiếu kiểm '{this.maPhieu}' thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                NapThongTinPhieu();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi cân bằng phiếu kiểm kho: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnXuatFile_Click(object sender, EventArgs e)
        {
            if (dtChiTietData == null || dtChiTietData.Rows.Count == 0)
            {
                MessageBox.Show("Không có dữ liệu chi tiết để xuất file!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            using (SaveFileDialog sfd = new SaveFileDialog())
            {
                sfd.Title = "Xuất chi tiết phiếu kiểm kho";
                sfd.Filter = "File Excel (*.xls)|*.xls|File CSV (*.csv)|*.csv";
                sfd.FileName = $"ChiTiet_{this.maPhieu}_{DateTime.Now:yyyyMMdd_HHmm}.xls";

                if (sfd.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        string ext = Path.GetExtension(sfd.FileName).ToLowerInvariant();
                        if (ext == ".csv")
                        {
                            XuatCsvChiTiet(sfd.FileName);
                        }
                        else
                        {
                            XuatExcelHtmlChiTiet(sfd.FileName);
                        }

                        MessageBox.Show("Xuất file thành công!\nĐường dẫn: " + sfd.FileName, "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Lỗi khi xuất file: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        private void XuatCsvChiTiet(string filePath)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine($"PHIẾU KIỂM KHO: {this.maPhieu}");
            sb.AppendLine($"Trạng thái: {lblTrangThaiBadge.Text}");
            sb.AppendLine($"{lblNguoiTao.Text} - {lblNgayTao.Text}");
            sb.AppendLine($"{lblNguoiCanBang.Text} - {lblNgayCanBang.Text}");
            sb.AppendLine($"{lblKho.Text}");
            sb.AppendLine($"Ghi chú: {txtGhiChu.Text}");
            sb.AppendLine();
            sb.AppendLine("STT,Mã hàng,Tên hàng,ĐVT,Tồn kho (sổ sách),Thực tế,SL lệch,Đơn giá,Giá trị lệch");

            foreach (DataRow r in dtChiTietData.Rows)
            {
                string thucTe = r["SoLuongThucTe"] != DBNull.Value ? r["SoLuongThucTe"].ToString() : "Chưa kiểm";
                sb.AppendLine($"{r["STT"]},\"{r["MaHangNoiBo"]}\",\"{r["TenHang"]}\",\"{r["TenDVT"]}\",{r["TonKho"]},{thucTe},{r["SlChenhLech"]},{r["DonGia"]},{r["GiaTriChenhLech"]}");
            }

            sb.AppendLine();
            sb.AppendLine($"Tổng thực tế: {lblTongThucTeVal.Text}");
            sb.AppendLine($"Tổng lệch tăng: {lblTongLechTangVal.Text}");
            sb.AppendLine($"Tổng lệch giảm: {lblTongLechGiamVal.Text}");
            sb.AppendLine($"Tổng chênh lệch: {lblTongChenhLechVal.Text}");

            File.WriteAllText(filePath, sb.ToString(), new UTF8Encoding(true));
        }

        private void XuatExcelHtmlChiTiet(string filePath)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("<html xmlns:o=\"urn:schemas-microsoft-com:office:office\" xmlns:x=\"urn:schemas-microsoft-com:office:excel\" xmlns=\"http://www.w3.org/TR/REC-html40\">");
            sb.AppendLine("<head>");
            sb.AppendLine("<meta http-equiv=\"Content-Type\" content=\"text/html; charset=utf-8\" />");
            sb.AppendLine("<style>");
            sb.AppendLine("body { font-family: Segoe UI, Arial; font-size: 10pt; }");
            sb.AppendLine("th { background-color: #0066CC; color: #FFFFFF; font-weight: bold; border: 0.5pt solid #CCCCCC; padding: 6px; }");
            sb.AppendLine("td { border: 0.5pt solid #E0E0E0; padding: 5px; }");
            sb.AppendLine(".num { mso-number-format: \"#,##0\"; text-align: right; }");
            sb.AppendLine(".center { text-align: center; }");
            sb.AppendLine(".bold { font-weight: bold; }");
            sb.AppendLine("</style>");
            sb.AppendLine("</head>");
            sb.AppendLine("<body>");

            sb.AppendLine($"<h2>CHI TIẾT PHIẾU KIỂM KHO: {this.maPhieu}</h2>");
            sb.AppendLine($"<p><b>Trạng thái:</b> {lblTrangThaiBadge.Text} | <b>{lblKho.Text}</b></p>");
            sb.AppendLine($"<p>{lblNguoiTao.Text} | {lblNgayTao.Text}</p>");
            sb.AppendLine($"<p>{lblNguoiCanBang.Text} | {lblNgayCanBang.Text}</p>");
            sb.AppendLine($"<p><b>Ghi chú:</b> {txtGhiChu.Text}</p>");
            sb.AppendLine("<br/>");

            sb.AppendLine("<table>");
            sb.AppendLine("<tr>");
            sb.AppendLine("<th>STT</th>");
            sb.AppendLine("<th>Mã hàng</th>");
            sb.AppendLine("<th>Tên hàng</th>");
            sb.AppendLine("<th>ĐVT</th>");
            sb.AppendLine("<th>Tồn kho (sổ sách)</th>");
            sb.AppendLine("<th>Thực tế</th>");
            sb.AppendLine("<th>SL lệch</th>");
            sb.AppendLine("<th>Đơn giá</th>");
            sb.AppendLine("<th>Giá trị lệch</th>");
            sb.AppendLine("</tr>");

            foreach (DataRow r in dtChiTietData.Rows)
            {
                string thucTe = r["SoLuongThucTe"] != DBNull.Value ? Convert.ToDecimal(r["SoLuongThucTe"]).ToString("N0") : "Chưa kiểm";
                sb.AppendLine("<tr>");
                sb.AppendLine($"<td class=\"center\">{r["STT"]}</td>");
                sb.AppendLine($"<td class=\"center\">{r["MaHangNoiBo"]}</td>");
                sb.AppendLine($"<td>{r["TenHang"]}</td>");
                sb.AppendLine($"<td class=\"center\">{r["TenDVT"]}</td>");
                sb.AppendLine($"<td class=\"num\">{Convert.ToDecimal(r["TonKho"]):N0}</td>");
                sb.AppendLine($"<td class=\"num\">{thucTe}</td>");
                sb.AppendLine($"<td class=\"num\">{Convert.ToDecimal(r["SlChenhLech"]):N0}</td>");
                sb.AppendLine($"<td class=\"num\">{Convert.ToDecimal(r["DonGia"]):N0}</td>");
                sb.AppendLine($"<td class=\"num\">{Convert.ToDecimal(r["GiaTriChenhLech"]):N0}</td>");
                sb.AppendLine("</tr>");
            }

            sb.AppendLine("</table>");
            sb.AppendLine("<br/>");
            sb.AppendLine($"<p><b>Tổng thực tế:</b> {lblTongThucTeVal.Text}</p>");
            sb.AppendLine($"<p><b>Tổng lệch tăng:</b> {lblTongLechTangVal.Text}</p>");
            sb.AppendLine($"<p><b>Tổng lệch giảm:</b> {lblTongLechGiamVal.Text}</p>");
            sb.AppendLine($"<p><b>Tổng chênh lệch:</b> {lblTongChenhLechVal.Text}</p>");

            sb.AppendLine("</body>");
            sb.AppendLine("</html>");

            File.WriteAllText(filePath, sb.ToString(), Encoding.UTF8);
        }

        private void btnDong_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        #endregion
    }
}
