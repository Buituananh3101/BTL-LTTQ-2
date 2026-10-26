using BTL_LTTQ_2.Classes;
using System;
using System.Collections.Generic;
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
                    btnCanBang.Visible = false;
                }
                else if (trangThai == "HUY")
                {
                    // Phiếu đã hủy: chỉ xem, không cho xóa và không cho cân bằng
                    lblTrangThaiBadge.Text = "Đã hủy";
                    lblTrangThaiBadge.ForeColor = Color.FromArgb(100, 116, 139);

                    lblNguoiCanBang.Text = "Người cân bằng: --";
                    lblNgayCanBang.Text = "Ngày cân bằng: --";
                    lblNguoiCanBang.Visible = true;
                    lblNgayCanBang.Visible = true;

                    btnXoa.Enabled = false;
                    btnXoa.Text = "Xóa";
                    btnCanBang.Visible = false;
                }
                else
                {
                    lblTrangThaiBadge.Text = "Phiếu tạm";
                    lblTrangThaiBadge.ForeColor = Color.FromArgb(217, 119, 6);

                    lblNguoiCanBang.Text = "Người cân bằng: --";
                    lblNgayCanBang.Text = "Ngày cân bằng: --";
                    lblNguoiCanBang.Visible = true;
                    lblNgayCanBang.Visible = true;

                    btnXoa.Enabled = true;
                    btnXoa.Text = "Xóa";
                    btnCanBang.Visible = true;
                    btnCanBang.Enabled = true;
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
                        -- Dòng chưa kiểm (SoLuongThucTe NULL): để trống chênh lệch, không tính vào tổng
                        CASE
                            WHEN ctpkk.SoLuongThucTe IS NOT NULL THEN (ctpkk.SoLuongThucTe - ctpkk.SoLuongTheoSo)
                            ELSE NULL
                        END AS SlChenhLech,
                        CASE
                            WHEN ctpkk.SoLuongThucTe IS NOT NULL THEN (ctpkk.SoLuongThucTe - ctpkk.SoLuongTheoSo) * ctpkk.DonGia
                            ELSE NULL
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
            if (trangThai == "HUY") return;

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
                        // 1. Xóa chi tiết kiểm kê (chỉ khi phiếu vẫn còn là phiếu tạm)
                        string sqlDeleteDetail = @"
                            DELETE FROM ChiTietPhieuKiemKe
                            WHERE MaPhieuKiemKe = @MaPhieuKiemKe
                              AND EXISTS (SELECT 1 FROM PhieuKiemKe WHERE MaPhieuKiemKe = @MaPhieuKiemKe AND TrangThai IN ('NHAP', 'DANG_KIEM', 'DA_KIEM'))";
                        using (SqlCommand cmdDtl = new SqlCommand(sqlDeleteDetail, conn, trans))
                        {
                            cmdDtl.Parameters.AddWithValue("@MaPhieuKiemKe", maPhieuKiemKe);
                            cmdDtl.ExecuteNonQuery();
                        }

                        // 2. Xóa phiếu kiểm kê (kiểm tra số dòng tác động để chống xóa phiếu vừa được cân bằng/hủy)
                        string sqlDeleteHeader = "DELETE FROM PhieuKiemKe WHERE MaPhieuKiemKe = @MaPhieuKiemKe AND TrangThai IN ('NHAP', 'DANG_KIEM', 'DA_KIEM')";
                        using (SqlCommand cmdHdr = new SqlCommand(sqlDeleteHeader, conn, trans))
                        {
                            cmdHdr.Parameters.AddWithValue("@MaPhieuKiemKe", maPhieuKiemKe);
                            if (cmdHdr.ExecuteNonQuery() != 1)
                            {
                                throw new InvalidOperationException("Phiếu kiểm kho không còn ở trạng thái phiếu tạm (có thể đã được cân bằng hoặc hủy). Vui lòng tải lại dữ liệu.");
                            }
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
                catch (InvalidOperationException ex)
                {
                    MessageBox.Show(ex.Message, "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    DaThayDoiDuLieu = true;
                    NapThongTinPhieu();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi khi xóa phiếu kiểm kho: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void btnCanBang_Click(object sender, EventArgs e)
        {
            if (trangThai == "DA_DIEU_CHINH" || trangThai == "HUY") return;

            if (dtChiTietData == null || dtChiTietData.Rows.Count == 0)
            {
                MessageBox.Show("Phiếu kiểm kho không có hàng hóa để cân bằng!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Bắt buộc tài khoản phải gắn với nhân viên (MaNVLap/MaNVDuyet tham chiếu NhanVien.MaNV)
            if (!SessionContext.MaNV.HasValue || SessionContext.MaNV.Value <= 0)
            {
                MessageBox.Show("Tài khoản chưa được gắn với nhân viên, không thể lập chứng từ.", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Đếm dòng chưa kiểm (SoLuongThucTe NULL) sẽ bị bỏ qua khi cân bằng
            int soDongChuaKiem = 0;
            foreach (DataRow r in dtChiTietData.Rows)
            {
                if (r["SoLuongThucTe"] == DBNull.Value) soDongChuaKiem++;
            }
            if (soDongChuaKiem == dtChiTietData.Rows.Count)
            {
                MessageBox.Show("Phiếu kiểm kho chưa có mặt hàng nào được nhập số lượng thực tế, không thể cân bằng!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string luuYChuaKiem = soDongChuaKiem > 0
                ? $"\n\nLưu ý: Có {soDongChuaKiem} mặt hàng chưa kiểm sẽ được bỏ qua (giữ nguyên tồn hiện tại)."
                : "";

            DialogResult dr = MessageBox.Show(
                $"Bạn có chắc chắn muốn HOÀN THÀNH (Cân bằng kho) phiếu kiểm kê '{this.maPhieu}'?\n\nHệ thống sẽ đọc lại tồn sổ sách tại thời điểm cân bằng, cập nhật tồn kho theo số lượng thực tế và khóa vĩnh viễn phiếu kiểm kê (không thể hủy/sửa theo Luật 6)!" + luuYChuaKiem,
                "Xác nhận hoàn thành kiểm kho",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            if (dr != DialogResult.Yes) return;

            try
            {
                int maNhanVien = SessionContext.MaNV.Value;
                KetQuaCanBang ketQua = null;

                DataProcessing.ChangeDataTransaction((conn, trans) =>
                {
                    // 1. Cân bằng kho: đọc lại tồn hiện tại, cập nhật SoLuongTheoSo, sinh phiếu điều chỉnh (cùng conn/trans)
                    ketQua = CanBangKhoTrongGiaoDich(conn, trans, maPhieuKiemKe, maNhanVien, this.maPhieu);

                    // 2. Ghi nhật ký thao tác
                    string sqlLog = @"
                        INSERT INTO NhatKyThaoTac (MaTaiKhoan, ThoiDiem, ThaoTac, LoaiDoiTuong, MaDoiTuong, NoiDung)
                        VALUES (@MaTaiKhoan, sysdatetime(), N'Cân bằng kho', 'PhieuKiemKe', @MaDoiTuong, @NoiDung)";

                    using (SqlCommand cmdLog = new SqlCommand(sqlLog, conn, trans))
                    {
                        cmdLog.Parameters.AddWithValue("@MaTaiKhoan", SessionContext.MaTaiKhoan > 0 ? SessionContext.MaTaiKhoan : 1);
                        cmdLog.Parameters.AddWithValue("@MaDoiTuong", maPhieuKiemKe.ToString());
                        cmdLog.Parameters.AddWithValue("@NoiDung", $"Cân bằng kiểm kho {this.maPhieu}, {ketQua.SoDongKiem} dòng kiểm, {ketQua.SoDongDieuChinh} dòng điều chỉnh, {ketQua.SoDongChuaKiem} dòng chưa kiểm");
                        cmdLog.ExecuteNonQuery();
                    }
                });

                DaThayDoiDuLieu = true;
                this.DialogResult = DialogResult.OK;
                MessageBox.Show(ketQua.TaoThongBao(this.maPhieu), "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                NapThongTinPhieu();
            }
            catch (InvalidOperationException ex)
            {
                MessageBox.Show(ex.Message, "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                DaThayDoiDuLieu = true;
                NapThongTinPhieu();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi cân bằng phiếu kiểm kho: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Kết quả một lần cân bằng kho (phục vụ thông báo tóm tắt sau khi cân bằng)
        /// </summary>
        internal sealed class KetQuaCanBang
        {
            public int SoDongKiem { get; set; }
            public int SoDongChuaKiem { get; set; }
            public int SoDongDieuChinh { get; set; }
            public decimal TongDieuChinhTang { get; set; }
            public decimal TongDieuChinhGiam { get; set; }

            public string TaoThongBao(string maPhieu)
            {
                return $"Cân bằng kho thành công cho phiếu {maPhieu}!\n\n" +
                       $"- Số dòng đã kiểm: {SoDongKiem:N0}\n" +
                       $"- Số dòng điều chỉnh: {SoDongDieuChinh:N0}\n" +
                       $"- Tổng điều chỉnh tăng: +{TongDieuChinhTang:N0}\n" +
                       $"- Tổng điều chỉnh giảm: -{TongDieuChinhGiam:N0}\n" +
                       $"- Số dòng chưa kiểm (bỏ qua): {SoDongChuaKiem:N0}\n\n" +
                       "Tồn sổ sách đã được chốt lại theo thời điểm cân bằng.";
            }
        }

        /// <summary>
        /// Cân bằng kho cho 1 phiếu kiểm trong giao dịch đang mở (dùng chung cho frmThemPhieuKiemKho và frmChiTietKiemKho):
        /// đọc lại tồn hiện tại từ vw_TonKhoHienTai tại kho của phiếu, cập nhật SoLuongTheoSo,
        /// sinh PhieuDieuChinh (DA_DUYET, thời điểm = lúc cân bằng) và ChiTietPhieuDieuChinh cho các dòng lệch khác 0.
        /// Dòng chưa kiểm (SoLuongThucTe NULL) được bỏ qua.
        /// </summary>
        internal static KetQuaCanBang CanBangKhoTrongGiaoDich(SqlConnection conn, SqlTransaction trans, int maPhieuKiemKe, int maNhanVien, string maPhieu)
        {
            KetQuaCanBang ketQua = new KetQuaCanBang();

            // 1. Khóa phiếu: chỉ phiếu tạm mới được cân bằng (chống cân bằng 2 lần hoặc cân bằng phiếu đã hủy)
            string sqlKhoaPhieu = @"
                UPDATE PhieuKiemKe
                SET TrangThai = 'DA_DIEU_CHINH'
                WHERE MaPhieuKiemKe = @MaPhieuKiemKe AND TrangThai IN ('NHAP', 'DANG_KIEM', 'DA_KIEM')";
            using (SqlCommand cmd = new SqlCommand(sqlKhoaPhieu, conn, trans))
            {
                cmd.Parameters.AddWithValue("@MaPhieuKiemKe", maPhieuKiemKe);
                if (cmd.ExecuteNonQuery() != 1)
                {
                    throw new InvalidOperationException("Phiếu kiểm kho không còn ở trạng thái phiếu tạm (có thể đã được cân bằng hoặc hủy). Vui lòng tải lại dữ liệu.");
                }
            }

            int maKho;
            using (SqlCommand cmd = new SqlCommand("SELECT MaKho FROM PhieuKiemKe WHERE MaPhieuKiemKe = @MaPhieuKiemKe", conn, trans))
            {
                cmd.Parameters.AddWithValue("@MaPhieuKiemKe", maPhieuKiemKe);
                maKho = Convert.ToInt32(cmd.ExecuteScalar());
            }

            // 2. Đọc lại tồn sổ sách hiện tại của từng dòng tại kho của phiếu (cùng conn/trans)
            string sqlDong = @"
                SELECT
                    ct.MaCTKiemKe,
                    ct.SoLuongThucTe,
                    h.MaHangNoiBo,
                    h.TenHang,
                    ISNULL(tk.SoLuongTon, 0) AS TonHienTai
                FROM ChiTietPhieuKiemKe ct
                JOIN HangHoa h ON ct.MaHang = h.MaHang
                LEFT JOIN vw_TonKhoHienTai tk ON tk.MaHang = ct.MaHang AND tk.MaKho = @MaKho
                WHERE ct.MaPhieuKiemKe = @MaPhieuKiemKe
                ORDER BY ct.MaCTKiemKe ASC";

            DataTable dtDong = new DataTable();
            using (SqlCommand cmd = new SqlCommand(sqlDong, conn, trans))
            {
                cmd.Parameters.AddWithValue("@MaKho", maKho);
                cmd.Parameters.AddWithValue("@MaPhieuKiemKe", maPhieuKiemKe);
                using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                {
                    da.Fill(dtDong);
                }
            }

            // 3. Chốt SoLuongTheoSo = tồn hiện tại và tính lệch = thực tế - tồn tại thời điểm cân bằng
            var danhSachDieuChinh = new List<Tuple<int, decimal>>(); // MaCTKiemKe, SoLuongDieuChinh
            string sqlChotTon = "UPDATE ChiTietPhieuKiemKe SET SoLuongTheoSo = @SoLuongTheoSo WHERE MaCTKiemKe = @MaCTKiemKe";

            foreach (DataRow r in dtDong.Rows)
            {
                // Dòng chưa kiểm: bỏ qua, giữ nguyên tồn hiện tại
                if (r["SoLuongThucTe"] == DBNull.Value)
                {
                    ketQua.SoDongChuaKiem++;
                    continue;
                }

                ketQua.SoDongKiem++;
                int maCTKiemKe = Convert.ToInt32(r["MaCTKiemKe"]);
                decimal thucTe = Convert.ToDecimal(r["SoLuongThucTe"]);
                decimal tonHienTai = Convert.ToDecimal(r["TonHienTai"]);

                if (tonHienTai < 0)
                {
                    throw new InvalidOperationException($"Mặt hàng '{r["MaHangNoiBo"]} - {r["TenHang"]}' đang có tồn sổ sách âm ({tonHienTai:N0}), không thể cân bằng. Vui lòng kiểm tra lại các chứng từ liên quan.");
                }

                using (SqlCommand cmd = new SqlCommand(sqlChotTon, conn, trans))
                {
                    cmd.Parameters.AddWithValue("@SoLuongTheoSo", tonHienTai);
                    cmd.Parameters.AddWithValue("@MaCTKiemKe", maCTKiemKe);
                    cmd.ExecuteNonQuery();
                }

                decimal slDieuChinh = thucTe - tonHienTai;
                if (slDieuChinh != 0)
                {
                    danhSachDieuChinh.Add(new Tuple<int, decimal>(maCTKiemKe, slDieuChinh));
                    if (slDieuChinh > 0) ketQua.TongDieuChinhTang += slDieuChinh;
                    else ketQua.TongDieuChinhGiam += Math.Abs(slDieuChinh);
                }
            }
            ketQua.SoDongDieuChinh = danhSachDieuChinh.Count;

            // 4. Tạo PhieuDieuChinh DA_DUYET với thời điểm thực tế lúc cân bằng
            string sqlPdc = @"
                INSERT INTO PhieuDieuChinh (MaPhieuKiemKe, MaNVLap, MaNVDuyet, NgayLap, NgayDuyet, TrangThai, GhiChu)
                VALUES (@MaPhieuKiemKe, @MaNVLap, @MaNVDuyet, SYSDATETIME(), SYSDATETIME(), 'DA_DUYET', @GhiChu);
                SELECT SCOPE_IDENTITY();";

            int maPhieuDieuChinh;
            using (SqlCommand cmd = new SqlCommand(sqlPdc, conn, trans))
            {
                cmd.Parameters.AddWithValue("@MaPhieuKiemKe", maPhieuKiemKe);
                cmd.Parameters.AddWithValue("@MaNVLap", maNhanVien);
                cmd.Parameters.AddWithValue("@MaNVDuyet", maNhanVien);
                cmd.Parameters.AddWithValue("@GhiChu", $"Cân bằng kiểm kê phiếu {maPhieu}");
                maPhieuDieuChinh = Convert.ToInt32(cmd.ExecuteScalar());
            }

            // 5. Thêm ChiTietPhieuDieuChinh cho các dòng có chênh lệch khác 0
            string sqlCtpdc = @"
                INSERT INTO ChiTietPhieuDieuChinh (MaPhieuDieuChinh, MaCTKiemKe, SoLuongDieuChinh)
                VALUES (@MaPhieuDieuChinh, @MaCTKiemKe, @SoLuongDieuChinh)";
            foreach (var item in danhSachDieuChinh)
            {
                using (SqlCommand cmd = new SqlCommand(sqlCtpdc, conn, trans))
                {
                    cmd.Parameters.AddWithValue("@MaPhieuDieuChinh", maPhieuDieuChinh);
                    cmd.Parameters.AddWithValue("@MaCTKiemKe", item.Item1);
                    cmd.Parameters.AddWithValue("@SoLuongDieuChinh", item.Item2);
                    cmd.ExecuteNonQuery();
                }
            }

            return ketQua;
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
                // Dòng chưa kiểm: để trống chênh lệch
                string slLech = r["SlChenhLech"] != DBNull.Value ? Convert.ToDecimal(r["SlChenhLech"]).ToString("N0") : "";
                string giaTriLech = r["GiaTriChenhLech"] != DBNull.Value ? Convert.ToDecimal(r["GiaTriChenhLech"]).ToString("N0") : "";
                sb.AppendLine($"<td class=\"num\">{thucTe}</td>");
                sb.AppendLine($"<td class=\"num\">{slLech}</td>");
                sb.AppendLine($"<td class=\"num\">{Convert.ToDecimal(r["DonGia"]):N0}</td>");
                sb.AppendLine($"<td class=\"num\">{giaTriLech}</td>");
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
