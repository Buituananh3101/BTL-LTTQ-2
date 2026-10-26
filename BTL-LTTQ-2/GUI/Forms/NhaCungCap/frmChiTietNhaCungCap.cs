using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Windows.Forms;
using BTL_LTTQ_2.Classes;

namespace BTL_LTTQ_2.GUI.Forms.NhaCungCap
{
    public partial class frmChiTietNhaCungCap : Form
    {
        private int maDonViCapHangHienTai = 0;
        private string trangThaiHienTai = "HOAT_DONG";

        public frmChiTietNhaCungCap()
        {
            InitializeComponent();
            maDonViCapHangHienTai = 0;
            BatChongChopGiat();
        }

        public frmChiTietNhaCungCap(int maDonViCapHang)
        {
            InitializeComponent();
            maDonViCapHangHienTai = maDonViCapHang;
            BatChongChopGiat();
        }

        private void BatChongChopGiat()
        {
            this.DoubleBuffered = true;
            this.SetStyle(ControlStyles.OptimizedDoubleBuffer | ControlStyles.AllPaintingInWmPaint | ControlStyles.UserPaint, true);
            this.UpdateStyles();
        }

        protected override CreateParams CreateParams
        {
            get
            {
                CreateParams cp = base.CreateParams;
                cp.ExStyle |= 0x02000000; // WS_EX_COMPOSITED
                return cp;
            }
        }

        private void frmChiTietNhaCungCap_Load(object sender, EventArgs e)
        {
            // Cố định kích thước chuẩn form Size Lớn theo Rule 15e (1182, 803)
            this.ClientSize = new Size(1182, 803);
            this.StartPosition = FormStartPosition.CenterParent;

            if (btnXoa != null)
            {
                btnXoa.Cursor = Cursors.Hand;
            }

            lblTieuDe.Text = "Chi tiết nhà cung cấp";
            this.Text = "Chi tiết thông tin nhà cung cấp";

            NapCombobox();

            if (maDonViCapHangHienTai > 0)
            {
                NapThongTinNhaCungCap();
            }
            else
            {
                MessageBox.Show("Không tìm thấy thông tin nhà cung cấp cần xem!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                this.DialogResult = DialogResult.Cancel;
                this.Close();
            }
        }

        #region Nạp dữ liệu & Khởi tạo

        private void NapCombobox()
        {
            try
            {
                string sqlNhom = @"
                    IF NOT EXISTS (SELECT 1 FROM sys.tables WHERE name = N'NhomNhaCungCap')
                    BEGIN
                        CREATE TABLE NhomNhaCungCap (
                            MaNhomNcc INT IDENTITY(1,1) PRIMARY KEY,
                            TenNhom NVARCHAR(150) NOT NULL,
                            GhiChu NVARCHAR(255) NULL
                        );
                    END;
                    SELECT MaNhomNcc, TenNhom FROM NhomNhaCungCap ORDER BY TenNhom;";

                DataTable dtNhom = DataProcessing.ReadData(sqlNhom);
                DataRow drNone = dtNhom.NewRow();
                drNone["MaNhomNcc"] = 0;
                drNone["TenNhom"] = "-- Chưa phân nhóm --";
                dtNhom.Rows.InsertAt(drNone, 0);

                ComboboxHelper.CaiDatTimKiem(cboNhomNcc, dtNhom, "TenNhom", "MaNhomNcc");
                cboNhomNcc.SelectedValue = 0;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi tải danh mục nhóm nhà cung cấp: " + ex.Message, "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void NapThongTinNhaCungCap()
        {
            try
            {
                string sql = @"
                    SELECT ncc.MaDonViCapHang, 
                           ISNULL(ncc.MaNcc, 'NCC' + RIGHT('00000' + CAST(ncc.MaDonViCapHang AS VARCHAR(10)), 5)) AS MaNcc,
                           ncc.TenDonVi, ncc.DienThoai, ncc.Email, ncc.DiaChi, ncc.KhuVuc, ncc.MaSoThue,
                           ISNULL(ncc.MaNhomNcc, 0) AS MaNhomNcc, ncc.GhiChu, ncc.TrangThai,
                           ISNULL((
                               SELECT SUM(pn.TongTien) 
                               FROM PhieuNhap pn 
                               WHERE pn.MaDonViCapHang = ncc.MaDonViCapHang 
                                 AND pn.TrangThai = 'DA_XAC_NHAN'
                           ), 0) AS TongMua,
                           ISNULL((
                               SELECT SUM(pt.TongTienTra) 
                               FROM PhieuTraHangNhap pt 
                               WHERE pt.MaDonViCapHang = ncc.MaDonViCapHang 
                                 AND pt.TrangThai = 'DA_TRA_HANG'
                           ), 0) AS TongTra
                    FROM DonViCapHang ncc
                    WHERE ncc.MaDonViCapHang = @MaDonViCapHang;";

                DataTable dt = DataProcessing.ReadData(sql, new SqlParameter[] { new SqlParameter("@MaDonViCapHang", maDonViCapHangHienTai) });
                if (dt != null && dt.Rows.Count > 0)
                {
                    DataRow r = dt.Rows[0];
                    txtMaNcc.Text = r["MaNcc"].ToString();
                    txtTenDonVi.Text = r["TenDonVi"].ToString();
                    txtDienThoai.Text = r["DienThoai"] != DBNull.Value ? r["DienThoai"].ToString() : "";
                    txtEmail.Text = r["Email"] != DBNull.Value ? r["Email"].ToString() : "";
                    txtMaSoThue.Text = r["MaSoThue"] != DBNull.Value ? r["MaSoThue"].ToString() : "";
                    txtKhuVuc.Text = r["KhuVuc"] != DBNull.Value ? r["KhuVuc"].ToString() : "";
                    txtDiaChi.Text = r["DiaChi"] != DBNull.Value ? r["DiaChi"].ToString() : "";
                    txtGhiChu.Text = r["GhiChu"] != DBNull.Value ? r["GhiChu"].ToString() : "";

                    // Chọn nhóm nhà cung cấp
                    if (r["MaNhomNcc"] != DBNull.Value && int.TryParse(r["MaNhomNcc"].ToString(), out int maNhom))
                    {
                        cboNhomNcc.SelectedValue = maNhom;
                    }
                    else
                    {
                        cboNhomNcc.SelectedValue = 0;
                    }

                    // Lưu trạng thái hiện tại
                    trangThaiHienTai = r["TrangThai"] != DBNull.Value ? r["TrangThai"].ToString() : "HOAT_DONG";

                    // Hiển thị công nợ & tổng mua thực tế
                    decimal tongMua = r["TongMua"] != DBNull.Value && decimal.TryParse(r["TongMua"].ToString(), out decimal tm) ? tm : 0;
                    decimal tongTra = r["TongTra"] != DBNull.Value && decimal.TryParse(r["TongTra"].ToString(), out decimal tt) ? tt : 0;
                    decimal noHienTai = tongMua - tongTra;

                    lblTongMuaVal.Text = $"{tongMua:N0} đ";
                    lblNoHienTaiVal.Text = $"{noHienTai:N0} đ";
                    lblNoHienTaiVal.ForeColor = noHienTai > 0 ? Color.Red : SystemColors.ControlText;

                    CapNhatGiaoDienNutXoa();
                }
                else
                {
                    MessageBox.Show("Không tìm thấy thông tin nhà cung cấp này trong cơ sở dữ liệu!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    this.DialogResult = DialogResult.Cancel;
                    this.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi tải thông tin chi tiết nhà cung cấp: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        #endregion

        #region Xử lý Xóa / Cho hoạt động lại nhà cung cấp

        private void CapNhatGiaoDienNutXoa()
        {
            if (btnXoa == null) return;

            btnXoa.Visible = true;
            btnXoa.UseVisualStyleBackColor = true;
            btnXoa.ForeColor = SystemColors.ControlText;
            if (btnLuu != null) btnXoa.Font = btnLuu.Font;

            if (trangThaiHienTai == "NGUNG_HOAT_DONG" || trangThaiHienTai == "NGUNG_GIAO_DICH")
            {
                btnXoa.Text = "Cho hoạt động lại";
            }
            else
            {
                btnXoa.Text = "Xóa";
            }
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            ThucHienThaoTacXoaHoacHoatDongLai();
        }

        private void ThucHienThaoTacXoaHoacHoatDongLai()
        {
            if (trangThaiHienTai == "NGUNG_HOAT_DONG" || trangThaiHienTai == "NGUNG_GIAO_DICH")
            {
                KichHoatHoatDongLai();
            }
            else
            {
                XoaNhaCungCap();
            }
        }

        private void KichHoatHoatDongLai()
        {
            if (maDonViCapHangHienTai <= 0) return;

            DialogResult dialog = MessageBox.Show(
                $"Bạn có chắc chắn muốn kích hoạt hoạt động trở lại cho nhà cung cấp '{txtTenDonVi.Text}' ({txtMaNcc.Text}) không?",
                "Kích hoạt hoạt động lại",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question,
                MessageBoxDefaultButton.Button1);

            if (dialog != DialogResult.Yes) return;

            try
            {
                DataProcessing.ChangeDataTransaction((conn, trans) =>
                {
                    string sqlUpdate = "UPDATE DonViCapHang SET TrangThai = 'HOAT_DONG' WHERE MaDonViCapHang = @MaDonViCapHang;";
                    using (SqlCommand cmd = new SqlCommand(sqlUpdate, conn, trans))
                    {
                        cmd.Parameters.AddWithValue("@MaDonViCapHang", maDonViCapHangHienTai);
                        cmd.ExecuteNonQuery();
                    }

                    GhiNhatKyThaoTac(conn, trans, "HOAT_DONG_LAI", $"Kích hoạt hoạt động trở lại cho nhà cung cấp '{txtTenDonVi.Text}' ({txtMaNcc.Text})");
                });

                MessageBox.Show(
                    $"Đã kích hoạt hoạt động trở lại cho nhà cung cấp '{txtTenDonVi.Text}' thành công!",
                    "Thông báo",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);

                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi kích hoạt hoạt động lại nhà cung cấp: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void XoaNhaCungCap()
        {
            if (maDonViCapHangHienTai <= 0)
            {
                MessageBox.Show("Nhà cung cấp này chưa được lưu vào hệ thống nên không thể xóa!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            try
            {
                // 1. Kiểm tra ràng buộc chứng từ kho & giao dịch
                string sqlKiemTra = @"
                    SELECT 
                        (SELECT COUNT(1) FROM PhieuNhap WHERE MaDonViCapHang = @MaDonViCapHang) AS DemPhieuNhap,
                        (SELECT COUNT(1) FROM PhieuTraHangNhap WHERE MaDonViCapHang = @MaDonViCapHang) AS DemPhieuTra,
                        (SELECT COUNT(DISTINCT ctpn.MaHang) 
                         FROM ChiTietPhieuNhap ctpn 
                         JOIN PhieuNhap pn ON ctpn.MaPhieuNhap = pn.MaPhieuNhap 
                         WHERE pn.MaDonViCapHang = @MaDonViCapHang) AS DemMatHang;";

                DataTable dtCheck = DataProcessing.ReadData(sqlKiemTra, new SqlParameter[] { new SqlParameter("@MaDonViCapHang", maDonViCapHangHienTai) });
                if (dtCheck != null && dtCheck.Rows.Count > 0)
                {
                    DataRow r = dtCheck.Rows[0];
                    int demNhap = Convert.ToInt32(r["DemPhieuNhap"]);
                    int demTra = Convert.ToInt32(r["DemPhieuTra"]);
                    int demHang = Convert.ToInt32(r["DemMatHang"]);

                    int tongGiaoDich = demNhap + demTra + demHang;
                    if (tongGiaoDich > 0)
                    {
                        string lyDo = "";
                        if (demNhap > 0) lyDo += $"\n- {demNhap} phiếu nhập hàng";
                        if (demTra > 0) lyDo += $"\n- {demTra} phiếu trả hàng nhập";
                        if (demHang > 0) lyDo += $"\n- {demHang} mặt hàng liên quan";

                        if (trangThaiHienTai == "NGUNG_HOAT_DONG" || trangThaiHienTai == "NGUNG_GIAO_DICH")
                        {
                            MessageBox.Show(
                                $"Không thể xóa nhà cung cấp '{txtTenDonVi.Text}' ({txtMaNcc.Text}) vì đã phát sinh chứng từ giao dịch trong hệ thống:{lyDo}\n\nQuy chuẩn quản lý kho không cho phép xóa dữ liệu đã có lịch sử giao dịch nhằm bảo toàn tính toàn vẹn sổ sách.\n\nNhà cung cấp này hiện tại đã ở trạng thái 'Ngừng hoạt động'.",
                                "Không thể xóa nhà cung cấp",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Warning);
                            return;
                        }

                        DialogResult hoiChuyen = MessageBox.Show(
                            $"Không thể xóa nhà cung cấp '{txtTenDonVi.Text}' ({txtMaNcc.Text}) vì đã phát sinh chứng từ giao dịch trong hệ thống:{lyDo}\n\nQuy chuẩn quản lý kho không cho phép xóa dữ liệu đã có lịch sử giao dịch nhằm bảo toàn tính toàn vẹn sổ sách.\n\nBạn có muốn chuyển trạng thái nhà cung cấp này sang 'Ngừng hoạt động' không?",
                            "Không thể xóa nhà cung cấp - Chuyển sang Ngừng hoạt động",
                            MessageBoxButtons.YesNo,
                            MessageBoxIcon.Question,
                            MessageBoxDefaultButton.Button1);

                        if (hoiChuyen == DialogResult.Yes)
                        {
                            DataProcessing.ChangeDataTransaction((conn, trans) =>
                            {
                                string sqlUpdateTrangThai = "UPDATE DonViCapHang SET TrangThai = 'NGUNG_HOAT_DONG' WHERE MaDonViCapHang = @MaDonViCapHang;";
                                using (SqlCommand cmd = new SqlCommand(sqlUpdateTrangThai, conn, trans))
                                {
                                    cmd.Parameters.AddWithValue("@MaDonViCapHang", maDonViCapHangHienTai);
                                    cmd.ExecuteNonQuery();
                                }

                                GhiNhatKyThaoTac(conn, trans, "NGUNG_HOAT_DONG", $"Chuyển trạng thái nhà cung cấp '{txtTenDonVi.Text}' ({txtMaNcc.Text}) sang Ngừng hoạt động");
                            });

                            MessageBox.Show(
                                $"Đã chuyển trạng thái nhà cung cấp '{txtTenDonVi.Text}' sang 'Ngừng hoạt động' thành công!",
                                "Thông báo",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Information);

                            this.DialogResult = DialogResult.OK;
                            this.Close();
                        }

                        return;
                    }
                }

                // 2. Nếu chưa từng phát sinh giao dịch: Hỏi xác nhận xóa vĩnh viễn
                DialogResult dialog = MessageBox.Show(
                    $"Bạn có chắc chắn muốn xóa vĩnh viễn nhà cung cấp '{txtTenDonVi.Text}' (Mã: {txtMaNcc.Text}) khỏi cơ sở dữ liệu không?\n\nLưu ý: Toàn bộ thông tin liên quan sẽ bị xóa và không thể khôi phục!",
                    "Xác nhận xóa nhà cung cấp",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question,
                    MessageBoxDefaultButton.Button2);

                if (dialog != DialogResult.Yes) return;

                // 3. Thực hiện xóa trong Transaction ADO.NET
                DataProcessing.ChangeDataTransaction((conn, trans) =>
                {
                    using (SqlCommand cmd = new SqlCommand("DELETE FROM DonViCapHang WHERE MaDonViCapHang = @MaDonViCapHang;", conn, trans))
                    {
                        cmd.Parameters.AddWithValue("@MaDonViCapHang", maDonViCapHangHienTai);
                        cmd.ExecuteNonQuery();
                    }

                    GhiNhatKyThaoTac(conn, trans, "XOA", $"Xóa vĩnh viễn nhà cung cấp '{txtTenDonVi.Text}' ({txtMaNcc.Text})");
                });

                MessageBox.Show($"Đã xóa thành công nhà cung cấp '{txtTenDonVi.Text}'!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi xóa nhà cung cấp: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void GhiNhatKyThaoTac(SqlConnection conn, SqlTransaction trans, string thaoTac, string noiDung)
        {
            try
            {
                string sqlLog = @"
                    INSERT INTO NhatKyThaoTac (MaTaiKhoan, ThoiDiem, ThaoTac, LoaiDoiTuong, MaDoiTuong, NoiDung)
                    VALUES (@MaTaiKhoan, GETDATE(), @ThaoTac, @LoaiDoiTuong, @MaDoiTuong, @NoiDung);";

                using (SqlCommand cmd = new SqlCommand(sqlLog, conn, trans))
                {
                    cmd.Parameters.AddWithValue("@MaTaiKhoan", SessionContext.MaTaiKhoan > 0 ? SessionContext.MaTaiKhoan : 1);
                    cmd.Parameters.AddWithValue("@ThaoTac", thaoTac);
                    cmd.Parameters.AddWithValue("@LoaiDoiTuong", "DonViCapHang");
                    cmd.Parameters.AddWithValue("@MaDoiTuong", string.IsNullOrWhiteSpace(txtMaNcc.Text) ? maDonViCapHangHienTai.ToString() : txtMaNcc.Text.Trim());
                    cmd.Parameters.AddWithValue("@NoiDung", noiDung);
                    cmd.ExecuteNonQuery();
                }
            }
            catch
            {
                // Bỏ qua lỗi ghi nhật ký để không làm ảnh hưởng đến giao dịch nghiệp vụ chính
            }
        }

        #endregion

        #region Xử lý Lưu dữ liệu

        private void btnLuu_Click(object sender, EventArgs e)
        {
            string tenDonVi = txtTenDonVi.Text.Trim();
            if (string.IsNullOrWhiteSpace(tenDonVi))
            {
                MessageBox.Show("Vui lòng nhập tên nhà cung cấp!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtTenDonVi.Focus();
                return;
            }

            string maNcc = txtMaNcc.Text.Trim();
            if (string.IsNullOrWhiteSpace(maNcc))
            {
                MessageBox.Show("Mã nhà cung cấp không được để trống!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtMaNcc.Focus();
                return;
            }

            try
            {
                // Kiểm tra trùng mã nhà cung cấp với các nhà cung cấp khác
                string sqlCheckMa = @"
                    SELECT COUNT(1) FROM DonViCapHang 
                    WHERE LOWER(MaNcc) = LOWER(@MaNcc) AND MaDonViCapHang <> @MaDonViCapHang";
                object valCheck = DataProcessing.ReadValue(sqlCheckMa, new SqlParameter[] {
                    new SqlParameter("@MaNcc", maNcc),
                    new SqlParameter("@MaDonViCapHang", maDonViCapHangHienTai)
                });

                if (valCheck != null && int.TryParse(valCheck.ToString(), out int tonTai) && tonTai > 0)
                {
                    MessageBox.Show($"Mã nhà cung cấp '{maNcc}' đã được sử dụng bởi nhà cung cấp khác. Vui lòng nhập mã khác!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtMaNcc.Focus();
                    return;
                }

                // Nhóm nhà cung cấp
                if (cboNhomNcc.SelectedIndex < 0 && !string.IsNullOrWhiteSpace(cboNhomNcc.Text))
                {
                    int idx = cboNhomNcc.FindStringExact(cboNhomNcc.Text.Trim());
                    if (idx >= 0) cboNhomNcc.SelectedIndex = idx;
                }

                object valMaNhom = DBNull.Value;
                if (cboNhomNcc.SelectedValue != null && int.TryParse(cboNhomNcc.SelectedValue.ToString(), out int maNhom) && maNhom > 0)
                {
                    valMaNhom = maNhom;
                }

                // Cập nhật thông tin trong Transaction an toàn
                DataProcessing.ChangeDataTransaction((conn, trans) =>
                {
                    string sqlUpdate = @"
                        UPDATE DonViCapHang SET
                            MaNcc = @MaNcc,
                            TenDonVi = @TenDonVi,
                            DienThoai = @DienThoai,
                            Email = @Email,
                            MaNhomNcc = @MaNhomNcc,
                            MaSoThue = @MaSoThue,
                            KhuVuc = @KhuVuc,
                            DiaChi = @DiaChi,
                            GhiChu = @GhiChu
                        WHERE MaDonViCapHang = @MaDonViCapHang;";

                    using (SqlCommand cmd = new SqlCommand(sqlUpdate, conn, trans))
                    {
                        cmd.Parameters.AddWithValue("@MaNcc", maNcc);
                        cmd.Parameters.AddWithValue("@TenDonVi", tenDonVi);
                        cmd.Parameters.AddWithValue("@DienThoai", string.IsNullOrWhiteSpace(txtDienThoai.Text) ? (object)DBNull.Value : txtDienThoai.Text.Trim());
                        cmd.Parameters.AddWithValue("@Email", string.IsNullOrWhiteSpace(txtEmail.Text) ? (object)DBNull.Value : txtEmail.Text.Trim());
                        cmd.Parameters.AddWithValue("@MaNhomNcc", valMaNhom);
                        cmd.Parameters.AddWithValue("@MaSoThue", string.IsNullOrWhiteSpace(txtMaSoThue.Text) ? (object)DBNull.Value : txtMaSoThue.Text.Trim());
                        cmd.Parameters.AddWithValue("@KhuVuc", string.IsNullOrWhiteSpace(txtKhuVuc.Text) ? (object)DBNull.Value : txtKhuVuc.Text.Trim());
                        cmd.Parameters.AddWithValue("@DiaChi", string.IsNullOrWhiteSpace(txtDiaChi.Text) ? (object)DBNull.Value : txtDiaChi.Text.Trim());
                        cmd.Parameters.AddWithValue("@GhiChu", string.IsNullOrWhiteSpace(txtGhiChu.Text) ? (object)DBNull.Value : txtGhiChu.Text.Trim());
                        cmd.Parameters.AddWithValue("@MaDonViCapHang", maDonViCapHangHienTai);
                        cmd.ExecuteNonQuery();
                    }

                    GhiNhatKyThaoTac(conn, trans, "CAP_NHAT", $"Cập nhật thông tin nhà cung cấp '{tenDonVi}' ({maNcc})");
                });

                MessageBox.Show("Cập nhật thông tin nhà cung cấp thành công!", "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi lưu thông tin nhà cung cấp: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnBoQua_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        #endregion
    }
}
