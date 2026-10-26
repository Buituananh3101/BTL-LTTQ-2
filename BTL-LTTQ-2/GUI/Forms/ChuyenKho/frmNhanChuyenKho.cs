using BTL_LTTQ_2.Classes;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Windows.Forms;

namespace BTL_LTTQ_2.GUI.Forms.ChuyenKho
{
    public partial class frmNhanChuyenKho : Form
    {
        private int maPhieuChuyen;
        private string maPhieu;
        private string tenKhoNguon;
        private string tenKhoDich;
        private DataTable dtChiTiet;

        public frmNhanChuyenKho(int maPhieuChuyen, string maPhieu, string tenKhoNguon, string tenKhoDich)
        {
            InitializeComponent();
            this.maPhieuChuyen = maPhieuChuyen;
            this.maPhieu = maPhieu;
            this.tenKhoNguon = tenKhoNguon;
            this.tenKhoDich = tenKhoDich;

            // Ô SL thực nhận chỉ cho nhập chữ số; chặn hộp thoại lỗi DataError mặc định
            dgvHangNhan.EditingControlShowing += dgvHangNhan_EditingControlShowing;
            dgvHangNhan.DataError += dgvHangNhan_DataError;
        }

        private void dgvHangNhan_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            if (e.Control is TextBox txt)
            {
                txt.KeyPress -= TxtSoLuongNhan_KeyPress;
                txt.TextChanged -= TxtSoLuongNhan_TextChanged;
                txt.MaxLength = 32767;

                string colName = dgvHangNhan.CurrentCell != null ? dgvHangNhan.Columns[dgvHangNhan.CurrentCell.ColumnIndex].Name : "";
                if (colName == "colSoLuongNhan")
                {
                    txt.MaxLength = 9; // Giới hạn độ dài để không tràn số
                    txt.KeyPress += TxtSoLuongNhan_KeyPress;
                    txt.TextChanged += TxtSoLuongNhan_TextChanged;
                }
            }
        }

        private void TxtSoLuongNhan_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void TxtSoLuongNhan_TextChanged(object sender, EventArgs e)
        {
            // Loại bỏ ký tự không phải chữ số khi người dùng dán dữ liệu (Ctrl+V)
            if (sender is TextBox txt)
            {
                string chiSo = new string(Array.FindAll(txt.Text.ToCharArray(), char.IsDigit));
                if (chiSo != txt.Text)
                {
                    txt.Text = chiSo;
                    txt.SelectionStart = txt.Text.Length;
                }
            }
        }

        private void dgvHangNhan_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {
            // Phòng hờ: không hiện hộp thoại lỗi mặc định, giữ ô ở chế độ sửa để người dùng nhập lại
            e.ThrowException = false;
            e.Cancel = true;
        }

        private void frmNhanChuyenKho_Load(object sender, EventArgs e)
        {
            lblThongTinPhieu.Text = string.Format("Mã phiếu: {0} | Kho gửi: {1} ➜ Kho nhận: {2}", this.maPhieu, this.tenKhoNguon, this.tenKhoDich);

            try
            {
                string sqlCheck = "SELECT MaKhoDich, TrangThai FROM PhieuChuyenKho WHERE MaPhieuChuyen = @MaPhieuChuyen";
                DataTable dtP = DataProcessing.ReadData(sqlCheck, new SqlParameter[] {
                    new SqlParameter("@MaPhieuChuyen", this.maPhieuChuyen)
                });

                if (dtP != null && dtP.Rows.Count > 0)
                {
                    string tt = dtP.Rows[0]["TrangThai"].ToString();
                    int maKD = Convert.ToInt32(dtP.Rows[0]["MaKhoDich"]);

                    if (tt != "DA_XUAT")
                    {
                        MessageBox.Show("Phiếu chuyển kho này không ở trạng thái Đang chuyển hàng (DA_XUAT)!\nKhông thể thực hiện nhận hàng.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        this.DialogResult = DialogResult.Cancel;
                        this.Close();
                        return;
                    }

                    if (!SessionContext.IsAdmin && SessionContext.MaKhoHienTai != maKD)
                    {
                        MessageBox.Show(string.Format("Tài khoản hiện tại đang ở kho khác, không thuộc kho nhận hàng '{0}'!\nKhông có quyền nhận hàng.", this.tenKhoDich), "Cảnh báo quyền", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        this.DialogResult = DialogResult.Cancel;
                        this.Close();
                        return;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi kiểm tra thông tin phiếu chuyển: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            TaiDanhSachHangNhan();
        }

        private void TaiDanhSachHangNhan()
        {
            try
            {
                string sql = @"SELECT ct.MaCTPhieuChuyen, ct.MaHang, h.MaHangNoiBo, h.TenHang, 
                                      ISNULL(dvt.TenDVT, '') AS TenDVT,
                                      ct.SoLuong,
                                      ISNULL(ct.SoLuongNhan, ct.SoLuong) AS SoLuongNhan,
                                      (ct.SoLuong - ISNULL(ct.SoLuongNhan, ct.SoLuong)) AS HaoHut
                               FROM ChiTietPhieuChuyenKho ct
                               JOIN HangHoa h ON ct.MaHang = h.MaHang
                               LEFT JOIN DonViTinh dvt ON h.MaDvt = dvt.MaDvt
                               WHERE ct.MaPhieuChuyen = @MaPhieuChuyen
                               ORDER BY ct.MaCTPhieuChuyen ASC";

                dtChiTiet = DataProcessing.ReadData(sql, new SqlParameter[] {
                    new SqlParameter("@MaPhieuChuyen", this.maPhieuChuyen)
                });

                dtChiTiet.Columns.Add("STT", typeof(int));
                for (int i = 0; i < dtChiTiet.Rows.Count; i++)
                {
                    dtChiTiet.Rows[i]["STT"] = i + 1;
                }

                dgvHangNhan.AutoGenerateColumns = false;
                dgvHangNhan.DataSource = dtChiTiet;

                CapNhatTongKet();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi tải danh sách hàng hóa chuyển: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void dgvHangNhan_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0 || e.RowIndex >= dgvHangNhan.Rows.Count) return;

            // Lấy đúng dòng dữ liệu gắn với dòng lưới (chỉ số dòng lưới lệch chỉ số DataTable khi đã bấm sắp xếp cột)
            DataRowView drv = dgvHangNhan.Rows[e.RowIndex].DataBoundItem as DataRowView;
            if (drv == null) return;
            DataRow row = drv.Row;
            decimal soLuongChuyen = Convert.ToDecimal(row["SoLuong"]);
            decimal soLuongNhan = 0;

            if (row["SoLuongNhan"] == DBNull.Value || !decimal.TryParse(row["SoLuongNhan"].ToString(), out soLuongNhan))
            {
                soLuongNhan = soLuongChuyen;
                row["SoLuongNhan"] = soLuongNhan;
            }

            // Số lượng nhận là số nguyên
            if (soLuongNhan != Math.Floor(soLuongNhan))
            {
                soLuongNhan = Math.Floor(soLuongNhan);
                row["SoLuongNhan"] = soLuongNhan;
            }

            if (soLuongNhan < 0)
            {
                MessageBox.Show("Số lượng nhận không thể nhỏ hơn 0!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                soLuongNhan = 0;
                row["SoLuongNhan"] = soLuongNhan;
            }
            else if (soLuongNhan > soLuongChuyen)
            {
                MessageBox.Show(string.Format("Số lượng thực nhận ({0:N0}) không được vượt quá số lượng chuyển ({1:N0})!", soLuongNhan, soLuongChuyen), "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                soLuongNhan = soLuongChuyen;
                row["SoLuongNhan"] = soLuongNhan;
            }

            row["HaoHut"] = soLuongChuyen - soLuongNhan;
            CapNhatTongKet();
        }

        private void dgvHangNhan_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.RowIndex < 0 || e.RowIndex >= dgvHangNhan.Rows.Count) return;

            string colName = dgvHangNhan.Columns[e.ColumnIndex].Name;
            if (colName == "colHaoHut")
            {
                if (e.Value != null && decimal.TryParse(e.Value.ToString(), out decimal haoHut))
                {
                    e.Value = haoHut.ToString("N0");
                    e.FormattingApplied = true;

                    if (haoHut > 0)
                    {
                        e.CellStyle.ForeColor = Color.Red;
                        e.CellStyle.Font = new Font(dgvHangNhan.Font, FontStyle.Bold);
                        e.CellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                    }
                    else
                    {
                        e.CellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                    }
                }
            }
        }

        private void CapNhatTongKet()
        {
            decimal tongChuyen = 0;
            decimal tongNhan = 0;
            decimal tongHaoHut = 0;

            if (dtChiTiet != null)
            {
                foreach (DataRow r in dtChiTiet.Rows)
                {
                    decimal chuyen = Convert.ToDecimal(r["SoLuong"]);
                    decimal nhan = r["SoLuongNhan"] != DBNull.Value ? Convert.ToDecimal(r["SoLuongNhan"]) : chuyen;
                    tongChuyen += chuyen;
                    tongNhan += nhan;
                    tongHaoHut += (chuyen - nhan);
                }
            }

            lblTongChuyen.Text = "Tổng chuyển: " + tongChuyen.ToString("N0");
            lblTongThucNhan.Text = "Tổng thực nhận: " + tongNhan.ToString("N0");
            lblTongHaoHut.Text = "Tổng hao hụt: " + tongHaoHut.ToString("N0");

            if (tongHaoHut > 0)
            {
                lblTongHaoHut.ForeColor = Color.Red;
            }
            else
            {
                lblTongHaoHut.ForeColor = SystemColors.ControlText;
            }
        }

        private void btnXacNhanNhan_Click(object sender, EventArgs e)
        {
            if (dtChiTiet == null || dtChiTiet.Rows.Count == 0)
            {
                MessageBox.Show("Không có dữ liệu mặt hàng để nhận!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Bắt buộc tài khoản phải gắn với nhân viên (MaNVNhan tham chiếu NhanVien.MaNV)
            if (!SessionContext.MaNV.HasValue || SessionContext.MaNV.Value <= 0)
            {
                MessageBox.Show("Tài khoản chưa được gắn với nhân viên, không thể lập chứng từ.", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (dgvHangNhan.IsCurrentCellInEditMode && !dgvHangNhan.EndEdit())
            {
                return;
            }

            decimal tongChuyen = 0;
            decimal tongNhan = 0;
            decimal tongHaoHut = 0;

            foreach (DataRow r in dtChiTiet.Rows)
            {
                decimal chuyen = Convert.ToDecimal(r["SoLuong"]);
                decimal nhan = r["SoLuongNhan"] != DBNull.Value ? Convert.ToDecimal(r["SoLuongNhan"]) : chuyen;
                tongChuyen += chuyen;
                tongNhan += nhan;
                tongHaoHut += (chuyen - nhan);
            }

            if (tongHaoHut > 0 && string.IsNullOrWhiteSpace(txtGhiChuNhan.Text))
            {
                MessageBox.Show(string.Format("Có tổng cộng {0:N0} sản phẩm bị hao hụt/nhận thiếu!\nBắt buộc phải nhập lý do vào ô 'Ghi chú nhận hàng' trước khi xác nhận.", tongHaoHut), "Yêu cầu nhập lý do hao hụt", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtGhiChuNhan.Focus();
                return;
            }

            string thongBaoXacNhan = string.Format("Bạn có chắc chắn muốn xác nhận nhận hàng cho phiếu chuyển {0}?\n\n- Kho nhận: {1}\n- Tổng nhận: {2:N0}/{3:N0} sản phẩm\n- Hao hụt: {4:N0} sản phẩm\n\nSau khi nhận, phiếu sẽ hoàn tất và tự động cộng tồn kho vào kho nhận.",
                this.maPhieu, this.tenKhoDich, tongNhan, tongChuyen, tongHaoHut);

            if (MessageBox.Show(thongBaoXacNhan, "Xác nhận nhận hàng", MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
            {
                return;
            }

            try
            {
                int maNVNhan = SessionContext.MaNV.Value;

                DataProcessing.ChangeDataTransaction((conn, trans) =>
                {
                    // 1. Cập nhật phiếu chuyển kho sang DA_NHAN, chỉ khi phiếu vẫn đang chuyển (chống nhận phiếu vừa bị hủy/đã nhận)
                    string sqlUpdatePhieu = @"UPDATE PhieuChuyenKho
                                             SET TrangThai = 'DA_NHAN',
                                                 ThoiDiemNhan = SYSDATETIME(),
                                                 MaNVNhan = @MaNVNhan,
                                                 TongSoLuongNhan = @TongSoLuongNhan,
                                                 GhiChuNhan = @GhiChuNhan
                                             WHERE MaPhieuChuyen = @MaPhieuChuyen AND TrangThai = 'DA_XUAT'";

                    using (SqlCommand cmdPhieu = new SqlCommand(sqlUpdatePhieu, conn, trans))
                    {
                        cmdPhieu.Parameters.AddWithValue("@MaNVNhan", maNVNhan);
                        cmdPhieu.Parameters.AddWithValue("@TongSoLuongNhan", tongNhan);
                        cmdPhieu.Parameters.AddWithValue("@GhiChuNhan", string.IsNullOrWhiteSpace(txtGhiChuNhan.Text) ? (object)DBNull.Value : txtGhiChuNhan.Text.Trim());
                        cmdPhieu.Parameters.AddWithValue("@MaPhieuChuyen", this.maPhieuChuyen);
                        if (cmdPhieu.ExecuteNonQuery() != 1)
                        {
                            throw new InvalidOperationException("Phiếu chuyển kho không còn ở trạng thái Đang chuyển (có thể đã bị hủy hoặc đã được nhận). Vui lòng tải lại dữ liệu.");
                        }
                    }

                    // 2. Cập nhật số lượng nhận từng dòng
                    string sqlUpdateCT = "UPDATE ChiTietPhieuChuyenKho SET SoLuongNhan = @SoLuongNhan WHERE MaCTPhieuChuyen = @MaCT AND MaPhieuChuyen = @MaPhieuChuyen";
                    foreach (DataRow r in dtChiTiet.Rows)
                    {
                        using (SqlCommand cmdCT = new SqlCommand(sqlUpdateCT, conn, trans))
                        {
                            cmdCT.Parameters.AddWithValue("@SoLuongNhan", r["SoLuongNhan"] != DBNull.Value ? r["SoLuongNhan"] : r["SoLuong"]);
                            cmdCT.Parameters.AddWithValue("@MaCT", r["MaCTPhieuChuyen"]);
                            cmdCT.Parameters.AddWithValue("@MaPhieuChuyen", this.maPhieuChuyen);
                            cmdCT.ExecuteNonQuery();
                        }
                    }

                    // 3. Ghi log kiểm toán
                    string sqlLog = @"INSERT INTO NhatKyThaoTac (MaTaiKhoan, ThoiDiem, ThaoTac, LoaiDoiTuong, MaDoiTuong, NoiDung)
                                     VALUES (@MaTaiKhoan, GETDATE(), 'NHAN_HANG', 'CHUYEN_KHO', @MaDoiTuong, @NoiDung)";
                    using (SqlCommand cmdLog = new SqlCommand(sqlLog, conn, trans))
                    {
                        cmdLog.Parameters.AddWithValue("@MaTaiKhoan", SessionContext.MaTaiKhoan > 0 ? SessionContext.MaTaiKhoan : 1);
                        cmdLog.Parameters.AddWithValue("@MaDoiTuong", this.maPhieu);
                        cmdLog.Parameters.AddWithValue("@NoiDung", string.Format("Xác nhận nhận {0:N0}/{1:N0} sản phẩm từ phiếu chuyển kho {2} vào {3}", tongNhan, tongChuyen, this.maPhieu, this.tenKhoDich));
                        cmdLog.ExecuteNonQuery();
                    }
                });

                MessageBox.Show("Xác nhận nhận hàng chuyển kho thành công!\nTồn kho tại kho nhận đã được cập nhật.", "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            catch (InvalidOperationException ex)
            {
                MessageBox.Show(ex.Message, "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                // Đóng form để màn hình chi tiết tải lại trạng thái mới nhất
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi trong quá trình nhận hàng: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnHuy_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }
    }
}
