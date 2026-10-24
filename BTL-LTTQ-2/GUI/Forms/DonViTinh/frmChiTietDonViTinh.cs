using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Windows.Forms;
using BTL_LTTQ_2.Classes;

namespace BTL_LTTQ_2.GUI.Forms.DonViTinh
{
    public partial class frmChiTietDonViTinh : Form
    {
        private int maDvt = 0;
        private string tenDvtGoc = string.Empty;
        private string maDvtGoc = string.Empty;

        /// <summary>
        /// Mã đơn vị tính sau khi thêm mới hoặc chỉnh sửa (dùng khi form cha cần lấy giá trị vừa lưu)
        /// </summary>
        public int MaDvtMoi { get; private set; }

        /// <summary>
        /// Constructor thêm mới đơn vị tính
        /// </summary>
        public frmChiTietDonViTinh()
        {
            InitializeComponent();
            this.maDvt = 0;
        }

        /// <summary>
        /// Constructor xem / chỉnh sửa đơn vị tính
        /// </summary>
        /// <param name="maDvt">Mã định danh đơn vị tính</param>
        public frmChiTietDonViTinh(int maDvt) : this()
        {
            this.maDvt = maDvt;
        }

        private void frmChiTietDonViTinh_Load(object sender, EventArgs e)
        {
            CaiDatGiaoDien();

            if (maDvt > 0)
            {
                TaiThongTinDonViTinh();
                TaiDanhSachHangHoaSuDung();
                btnXoa.Visible = true;
                txtMaDvt.ReadOnly = true;
                txtMaDvt.BackColor = Color.FromArgb(243, 244, 246);
            }
            else
            {
                lblTieuDe.Text = "Thêm mới đơn vị tính";
                this.Text = "Thêm mới đơn vị tính";
                txtMaDvt.Text = SinhMaDvtGoiY();
                txtMaDvt.ReadOnly = false;
                txtMaDvt.BackColor = Color.White;
                btnXoa.Visible = false;

                dgvHangHoa.Visible = false;
                lblChuaCoHang.Visible = true;
                lblChuaCoHang.Text = "Đơn vị tính mới chưa có mặt hàng nào sử dụng.";
                grpDanhSachHangHoa.Text = "Danh sách mặt hàng đang sử dụng đơn vị tính này (0 mặt hàng)";
            }

            txtTenDvt.Focus();
        }

        private string SinhMaDvtGoiY()
        {
            try
            {
                string sql = "SELECT ISNULL(MAX(MaDVT), 0) + 1 FROM DonViTinh";
                object res = DataProcessing.ReadValue(sql);
                int nextId = (res != null && int.TryParse(res.ToString(), out int id)) ? id : 1;
                return string.Format("DVT{0:D3}", nextId);
            }
            catch
            {
                return "DVT001";
            }
        }

        private void CaiDatGiaoDien()
        {
            dgvHangHoa.AutoGenerateColumns = false;
            dgvHangHoa.CellFormatting += DgvHangHoa_CellFormatting;
            dgvHangHoa.RowPostPaint += DgvHangHoa_RowPostPaint;
        }

        private void DgvHangHoa_RowPostPaint(object sender, DataGridViewRowPostPaintEventArgs e)
        {
            // Tự động đánh số thứ tự STT
            if (e.RowIndex >= 0 && e.RowIndex < dgvHangHoa.Rows.Count)
            {
                dgvHangHoa.Rows[e.RowIndex].Cells["colSTT"].Value = (e.RowIndex + 1).ToString();
            }
        }

        private void DgvHangHoa_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.RowIndex < 0 || e.Value == null) return;

            string colName = dgvHangHoa.Columns[e.ColumnIndex].Name;
            if (colName == "colGiaBan" && decimal.TryParse(e.Value.ToString(), out decimal giaBan))
            {
                e.Value = giaBan.ToString("N0") + " đ";
                e.FormattingApplied = true;
                dgvHangHoa.Rows[e.RowIndex].Cells[e.ColumnIndex].Style.Alignment = DataGridViewContentAlignment.MiddleRight;
            }
            else if (colName == "colMaHangNoiBo" || colName == "colSTT")
            {
                dgvHangHoa.Rows[e.RowIndex].Cells[e.ColumnIndex].Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            }
        }

        private void TaiThongTinDonViTinh()
        {
            try
            {
                string sql = @"
                    SELECT MaDVT, TenDVT, 
                           ('DVT' + RIGHT('000' + CAST(MaDVT AS VARCHAR(10)), 3)) AS MaHienThi 
                    FROM DonViTinh 
                    WHERE MaDVT = @MaDVT";
                SqlParameter[] thamSo = { new SqlParameter("@MaDVT", maDvt) };
                DataTable dt = DataProcessing.ReadData(sql, thamSo);

                if (dt != null && dt.Rows.Count > 0)
                {
                    DataRow r = dt.Rows[0];
                    tenDvtGoc = r["TenDVT"].ToString();
                    maDvtGoc = r["MaHienThi"].ToString();

                    txtTenDvt.Text = tenDvtGoc;
                    txtMaDvt.Text = maDvtGoc;
                    lblTieuDe.Text = "Chỉnh sửa đơn vị tính - " + tenDvtGoc;
                    this.Text = "Chỉnh sửa đơn vị tính - " + tenDvtGoc;
                }
                else
                {
                    MessageBox.Show("Không tìm thấy thông tin đơn vị tính!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    this.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi nạp thông tin đơn vị tính: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void TaiDanhSachHangHoaSuDung()
        {
            try
            {
                string sql = @"
                    SELECT h.MaHangNoiBo, h.TenHang, ISNULL(nh.TenNhom, N'--') AS TenNhom, ISNULL(h.GiaBanThamKhao, 0) AS GiaBanThamKhao
                    FROM HangHoa h
                    LEFT JOIN NhomHang nh ON h.MaNhom = nh.MaNhom
                    WHERE h.MaDVT = @MaDVT
                    ORDER BY h.TenHang";

                SqlParameter[] thamSo = { new SqlParameter("@MaDVT", maDvt) };
                DataTable dt = DataProcessing.ReadData(sql, thamSo);

                if (dt != null && dt.Rows.Count > 0)
                {
                    dgvHangHoa.DataSource = dt;
                    dgvHangHoa.Visible = true;
                    lblChuaCoHang.Visible = false;
                    grpDanhSachHangHoa.Text = string.Format("Danh sách mặt hàng đang sử dụng đơn vị tính này ({0} mặt hàng)", dt.Rows.Count);
                }
                else
                {
                    dgvHangHoa.DataSource = null;
                    dgvHangHoa.Visible = false;
                    lblChuaCoHang.Visible = true;
                    lblChuaCoHang.Text = "Chưa có mặt hàng nào sử dụng đơn vị tính này.";
                    grpDanhSachHangHoa.Text = "Danh sách mặt hàng đang sử dụng đơn vị tính này (0 mặt hàng)";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi nạp danh sách hàng hóa: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnLuu_Click(object sender, EventArgs e)
        {
            string maDvtNhap = txtMaDvt.Text.Trim();
            string tenDvt = txtTenDvt.Text.Trim();

            // Validate dữ liệu
            if (string.IsNullOrWhiteSpace(maDvtNhap))
            {
                MessageBox.Show("Vui lòng nhập mã đơn vị tính!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtMaDvt.Focus();
                return;
            }

            if (string.IsNullOrWhiteSpace(tenDvt))
            {
                MessageBox.Show("Vui lòng nhập tên đơn vị tính!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtTenDvt.Focus();
                return;
            }

            // Tách mã số nguyên từ chuỗi nhập (ví dụ: "DVT016" -> 16, "16" -> 16)
            int customId = 0;
            string chiSo = maDvtNhap.ToUpper().StartsWith("DVT") ? maDvtNhap.Substring(3).Trim() : maDvtNhap;
            if (int.TryParse(chiSo, out int parsedId) && parsedId > 0)
            {
                customId = parsedId;
            }
            else if (maDvt == 0)
            {
                MessageBox.Show("Mã đơn vị tính phải có định dạng 'DVT' kèm số (ví dụ: DVT001, DVT010...). Vui lòng nhập lại!", 
                    "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtMaDvt.SelectAll();
                txtMaDvt.Focus();
                return;
            }

            try
            {
                // 1. Kiểm tra trùng mã đơn vị tính khi thêm mới
                if (maDvt == 0 && customId > 0)
                {
                    string sqlKiemTraMa = "SELECT COUNT(*) FROM DonViTinh WHERE MaDVT = @MaDVT";
                    SqlParameter[] thamSoKiemTraMa = { new SqlParameter("@MaDVT", customId) };
                    object kqKiemTraMa = DataProcessing.ReadValue(sqlKiemTraMa, thamSoKiemTraMa);
                    if (kqKiemTraMa != null && Convert.ToInt32(kqKiemTraMa) > 0)
                    {
                        MessageBox.Show(
                            string.Format("Mã đơn vị tính '{0}' đã tồn tại trong hệ thống. Vui lòng nhập mã khác!", maDvtNhap),
                            "Cảnh báo trùng mã", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        txtMaDvt.SelectAll();
                        txtMaDvt.Focus();
                        return;
                    }
                }

                // 2. Kiểm tra trùng tên đơn vị tính
                string sqlKiemTraTen = "SELECT COUNT(*) FROM DonViTinh WHERE LOWER(TenDVT) = LOWER(@TenDVT) AND MaDVT <> @MaDVT";
                SqlParameter[] thamSoKiemTraTen = {
                    new SqlParameter("@TenDVT", tenDvt),
                    new SqlParameter("@MaDVT", maDvt)
                };

                object kqKiemTraTen = DataProcessing.ReadValue(sqlKiemTraTen, thamSoKiemTraTen);
                if (kqKiemTraTen != null && Convert.ToInt32(kqKiemTraTen) > 0)
                {
                    MessageBox.Show(
                        string.Format("Đơn vị tính '{0}' đã tồn tại trong hệ thống. Vui lòng nhập tên khác!", tenDvt),
                        "Cảnh báo trùng tên", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtTenDvt.SelectAll();
                    txtTenDvt.Focus();
                    return;
                }

                // 3. Thực hiện lưu dữ liệu bọc trong Transaction (Luật số 7)
                DataProcessing.ChangeDataTransaction((conn, trans) =>
                {
                    if (maDvt == 0)
                    {
                        // Thêm mới
                        string sqlNext = "SELECT ISNULL(MAX(MaDVT), 0) + 1 FROM DonViTinh";
                        using (SqlCommand cmdNext = new SqlCommand(sqlNext, conn, trans))
                        {
                            object objNext = cmdNext.ExecuteScalar();
                            int nextAutoId = (objNext != null && int.TryParse(objNext.ToString(), out int nId)) ? nId : 1;

                            if (customId > 0 && customId != nextAutoId)
                            {
                                // Người dùng chỉ định ID tùy biến khác ID tự tăng
                                string sqlInsertCustom = @"
                                    SET IDENTITY_INSERT DonViTinh ON;
                                    INSERT INTO DonViTinh (MaDVT, TenDVT) VALUES (@MaDVT, @TenDVT);
                                    SET IDENTITY_INSERT DonViTinh OFF;";
                                using (SqlCommand cmd = new SqlCommand(sqlInsertCustom, conn, trans))
                                {
                                    cmd.Parameters.AddWithValue("@MaDVT", customId);
                                    cmd.Parameters.AddWithValue("@TenDVT", tenDvt);
                                    cmd.ExecuteNonQuery();
                                    MaDvtMoi = customId;
                                }
                            }
                            else
                            {
                                // Tự tăng chuẩn SQL Server
                                string sqlInsert = @"
                                    INSERT INTO DonViTinh (TenDVT) VALUES (@TenDVT); 
                                    SELECT SCOPE_IDENTITY();";
                                using (SqlCommand cmd = new SqlCommand(sqlInsert, conn, trans))
                                {
                                    cmd.Parameters.AddWithValue("@TenDVT", tenDvt);
                                    object newId = cmd.ExecuteScalar();
                                    MaDvtMoi = Convert.ToInt32(newId);
                                }
                            }
                        }

                        // Ghi nhật ký thao tác
                        GhiNhatKy(conn, trans, "THEM_MOI", string.Format("Thêm mới đơn vị tính: {0} (Mã: DVT{1:D3})", tenDvt, MaDvtMoi));
                    }
                    else
                    {
                        // Cập nhật
                        string sqlUpdate = @"
                            UPDATE DonViTinh 
                            SET TenDVT = @TenDVT 
                            WHERE MaDVT = @MaDVT";
                        using (SqlCommand cmd = new SqlCommand(sqlUpdate, conn, trans))
                        {
                            cmd.Parameters.AddWithValue("@TenDVT", tenDvt);
                            cmd.Parameters.AddWithValue("@MaDVT", maDvt);
                            cmd.ExecuteNonQuery();
                            MaDvtMoi = maDvt;
                        }

                        // Ghi nhật ký thao tác
                        GhiNhatKy(conn, trans, "CAP_NHAT", string.Format("Cập nhật đơn vị tính mã DVT{0:D3}: '{1}' -> '{2}'", 
                            maDvt, tenDvtGoc, tenDvt));
                    }
                });

                MessageBox.Show(maDvt == 0 ? "Thêm mới đơn vị tính thành công!" : "Cập nhật đơn vị tính thành công!",
                    "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);

                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi lưu đơn vị tính: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            if (maDvt <= 0)
            {
                MessageBox.Show("Đơn vị tính này chưa được lưu nên không thể xóa!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            try
            {
                // Kiểm tra xem có hàng hóa nào đang sử dụng đơn vị tính này không
                string sqlCheck = "SELECT COUNT(*) FROM HangHoa WHERE MaDVT = @MaDVT";
                SqlParameter[] pCheck = { new SqlParameter("@MaDVT", maDvt) };
                object objCount = DataProcessing.ReadValue(sqlCheck, pCheck);
                int soLuongHangHoa = (objCount != null && int.TryParse(objCount.ToString(), out int c)) ? c : 0;

                if (soLuongHangHoa > 0)
                {
                    MessageBox.Show(
                        string.Format("Không thể xóa đơn vị tính '{0}' ({1}) vì hiện đang có {2} mặt hàng liên kết sử dụng đơn vị tính này.\n\nQuy chuẩn quản lý kho không cho phép xóa đơn vị tính đang được sử dụng để bảo toàn tính toàn vẹn dữ liệu hàng hóa.",
                        txtTenDvt.Text.Trim(), txtMaDvt.Text.Trim(), soLuongHangHoa),
                        "Không thể xóa đơn vị tính", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // Xác nhận xóa
                DialogResult dr = MessageBox.Show(
                    string.Format("Bạn có chắc chắn muốn xóa đơn vị tính '{0}' ({1}) khỏi hệ thống không?\n\nThao tác này không thể hoàn tác!",
                    txtTenDvt.Text.Trim(), txtMaDvt.Text.Trim()),
                    "Xác nhận xóa",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question,
                    MessageBoxDefaultButton.Button2);

                if (dr != DialogResult.Yes) return;

                // Xóa dữ liệu bọc trong Transaction
                DataProcessing.ChangeDataTransaction((conn, trans) =>
                {
                    string sqlDelete = "DELETE FROM DonViTinh WHERE MaDVT = @MaDVT";
                    using (SqlCommand cmd = new SqlCommand(sqlDelete, conn, trans))
                    {
                        cmd.Parameters.AddWithValue("@MaDVT", maDvt);
                        cmd.ExecuteNonQuery();
                    }

                    // Ghi nhật ký thao tác
                    GhiNhatKy(conn, trans, "XOA", string.Format("Xóa đơn vị tính: {0} ({1})", txtTenDvt.Text.Trim(), txtMaDvt.Text.Trim()));
                });

                MessageBox.Show("Đã xóa đơn vị tính thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi xóa đơn vị tính: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void GhiNhatKy(SqlConnection conn, SqlTransaction trans, string thaoTac, string noiDung)
        {
            try
            {
                int maTaiKhoan = SessionContext.MaTaiKhoan > 0 ? SessionContext.MaTaiKhoan : 1;
                string sqlLog = @"
                    INSERT INTO NhatKyThaoTac (MaTaiKhoan, ThoiDiem, ThaoTac, LoaiDoiTuong, MaDoiTuong, NoiDung)
                    VALUES (@MaTaiKhoan, GETDATE(), @ThaoTac, N'DonViTinh', @MaDoiTuong, @NoiDung)";

                using (SqlCommand cmdLog = new SqlCommand(sqlLog, conn, trans))
                {
                    cmdLog.Parameters.AddWithValue("@MaTaiKhoan", maTaiKhoan);
                    cmdLog.Parameters.AddWithValue("@ThaoTac", thaoTac);
                    cmdLog.Parameters.AddWithValue("@MaDoiTuong", MaDvtMoi.ToString());
                    cmdLog.Parameters.AddWithValue("@NoiDung", noiDung);
                    cmdLog.ExecuteNonQuery();
                }
            }
            catch
            {
                // Bỏ qua lỗi log không làm gián đoạn luồng chính
            }
        }

        private void btnBoQua_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }
    }
}