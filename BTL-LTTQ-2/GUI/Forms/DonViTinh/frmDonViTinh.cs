using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Reflection;
using System.Text;
using System.Windows.Forms;
using System.Windows.Forms.VisualStyles;
using BTL_LTTQ_2.Classes;

namespace BTL_LTTQ_2.GUI.Forms.DonViTinh
{
    public partial class frmDonViTinh : Form
    {
        private int trangHienTai = 1;
        private int soDongMoiTrang = 20;
        private int tongSoBanGhi = 0;
        private int tongSoTrang = 1;
        private bool isUpdatingCheckAll = false;
        private bool isHeaderChecked = false;

        public frmDonViTinh()
        {
            InitializeComponent();
        }

        private void frmDonViTinh_Load(object sender, EventArgs e)
        {
            // Bật đệm đôi chống giật lag khi cuộn DataGridView
            try
            {
                typeof(DataGridView).InvokeMember("DoubleBuffered",
                    BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.SetProperty,
                    null, dgvDonViTinh, new object[] { true });
            }
            catch { }

            KhoiTaoBoLoc();
            HienThiDanhSach();
        }

        private void KhoiTaoBoLoc()
        {
            // Trạng thái sử dụng
            cboTrangThai.Items.Clear();
            cboTrangThai.Items.AddRange(new object[] {
                "-- Tất cả đơn vị tính --",
                "Đang có hàng hóa sử dụng",
                "Chưa có hàng hóa nào"
            });
            cboTrangThai.SelectedIndex = 0;

            // Sắp xếp
            cboSapXep.Items.Clear();
            cboSapXep.Items.AddRange(new object[] {
                "Tên đơn vị tính (A-Z)",
                "Tên đơn vị tính (Z-A)",
                "Số lượng hàng hóa (Nhiều nhất)",
                "Số lượng hàng hóa (Ít nhất)",
                "Mã đơn vị tính (Tăng dần)"
            });
            cboSapXep.SelectedIndex = 0;

            // Số dòng mỗi trang
            cboSoDongMoiTrang.Items.Clear();
            cboSoDongMoiTrang.Items.AddRange(new object[] { 10, 20, 50, 100 });
            cboSoDongMoiTrang.SelectedItem = 20;

            // Thời gian tạo
            dtpTuNgay.Value = new DateTime(DateTime.Now.Year, 1, 1);
            dtpDenNgay.Value = DateTime.Now;
        }

        public void HienThiDanhSach()
        {
            try
            {
                // Giải phóng ô đang edit
                if (dgvDonViTinh.IsCurrentCellInEditMode)
                {
                    dgvDonViTinh.EndEdit();
                }

                string tuKhoa = txtTimKiem.Text.Trim();
                string trangThai = cboTrangThai.SelectedItem?.ToString() ?? "-- Tất cả đơn vị tính --";
                string sapXep = cboSapXep.SelectedItem?.ToString() ?? "Tên đơn vị tính (A-Z)";

                // Xây dựng điều kiện WHERE / HAVING
                string whereClause = " WHERE 1=1 ";
                string havingClause = " ";

                if (!string.IsNullOrWhiteSpace(tuKhoa))
                {
                    whereClause += @" AND (
                        dvt.TenDVT LIKE @Search 
                        OR CAST(dvt.MaDVT AS NVARCHAR(20)) LIKE @Search 
                        OR ('DVT' + RIGHT('000' + CAST(dvt.MaDVT AS VARCHAR(10)), 3)) LIKE @Search
                    ) ";
                }

                if (trangThai == "Đang có hàng hóa sử dụng")
                {
                    havingClause += " HAVING COUNT(h.MaHang) > 0 ";
                }
                else if (trangThai == "Chưa có hàng hóa nào")
                {
                    havingClause += " HAVING COUNT(h.MaHang) = 0 ";
                }

                // Xây dựng câu lệnh ORDER BY
                string orderByClause = " ORDER BY dvt.TenDVT ASC ";
                switch (sapXep)
                {
                    case "Tên đơn vị tính (Z-A)":
                        orderByClause = " ORDER BY dvt.TenDVT DESC ";
                        break;
                    case "Số lượng hàng hóa (Nhiều nhất)":
                        orderByClause = " ORDER BY COUNT(h.MaHang) DESC, dvt.TenDVT ASC ";
                        break;
                    case "Số lượng hàng hóa (Ít nhất)":
                        orderByClause = " ORDER BY COUNT(h.MaHang) ASC, dvt.TenDVT ASC ";
                        break;
                    case "Mã đơn vị tính (Tăng dần)":
                        orderByClause = " ORDER BY dvt.MaDVT ASC ";
                        break;
                }

                // Đếm tổng số bản ghi
                string sqlCount = @"
                    SELECT COUNT(*) FROM (
                        SELECT dvt.MaDVT
                        FROM DonViTinh dvt
                        LEFT JOIN HangHoa h ON dvt.MaDVT = h.MaDVT
                        " + whereClause + @"
                        GROUP BY dvt.MaDVT, dvt.TenDVT
                        " + havingClause + @"
                    ) AS T";

                SqlParameter[] pCount = string.IsNullOrWhiteSpace(tuKhoa)
                    ? null
                    : new SqlParameter[] { new SqlParameter("@Search", "%" + tuKhoa + "%") };

                object objCount = DataProcessing.ReadValue(sqlCount, pCount);
                tongSoBanGhi = (objCount != null && int.TryParse(objCount.ToString(), out int c)) ? c : 0;

                tongSoTrang = (int)Math.Ceiling((double)tongSoBanGhi / soDongMoiTrang);
                if (tongSoTrang <= 0) tongSoTrang = 1;
                if (trangHienTai > tongSoTrang) trangHienTai = tongSoTrang;
                if (trangHienTai < 1) trangHienTai = 1;

                int skip = (trangHienTai - 1) * soDongMoiTrang;

                // Truy vấn dữ liệu phân trang
                string sqlData = @"
                    SELECT 
                        CAST(0 AS BIT) AS Chon,
                        dvt.MaDVT,
                        dvt.TenDVT,
                        ('DVT' + RIGHT('000' + CAST(dvt.MaDVT AS VARCHAR(10)), 3)) AS MaHienThi,
                        COUNT(h.MaHang) AS SoLuongHangHoa,
                        CASE WHEN COUNT(h.MaHang) > 0 THEN N'Đang sử dụng' ELSE N'Chưa sử dụng' END AS TrangThaiSuDung,
                        ISNULL(STUFF((
                            SELECT TOP 3 N', ' + h2.TenHang
                            FROM HangHoa h2
                            WHERE h2.MaDVT = dvt.MaDVT
                            ORDER BY h2.MaHang
                            FOR XML PATH(''), TYPE).value('.', 'NVARCHAR(MAX)'), 1, 2, ''), N'(Chưa có mặt hàng)') AS HangHoaTieuBieu
                    FROM DonViTinh dvt
                    LEFT JOIN HangHoa h ON dvt.MaDVT = h.MaDVT
                    " + whereClause + @"
                    GROUP BY dvt.MaDVT, dvt.TenDVT
                    " + havingClause + @"
                    " + orderByClause + @"
                    OFFSET @Skip ROWS FETCH NEXT @Take ROWS ONLY";

                SqlParameter[] pData;
                if (string.IsNullOrWhiteSpace(tuKhoa))
                {
                    pData = new SqlParameter[] {
                        new SqlParameter("@Skip", skip),
                        new SqlParameter("@Take", soDongMoiTrang)
                    };
                }
                else
                {
                    pData = new SqlParameter[] {
                        new SqlParameter("@Search", "%" + tuKhoa + "%"),
                        new SqlParameter("@Skip", skip),
                        new SqlParameter("@Take", soDongMoiTrang)
                    };
                }

                DataTable dt = DataProcessing.ReadData(sqlData, pData);
                dgvDonViTinh.AutoGenerateColumns = false;
                dgvDonViTinh.DataSource = dt;

                // Cập nhật nhãn phân trang
                CapNhatThanhPhanTrang();

                // Đặt lại cờ check all
                isHeaderChecked = false;
                dgvDonViTinh.InvalidateCell(dgvDonViTinh.Columns["colChon"].Index, -1);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi tải danh sách đơn vị tính: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void CapNhatThanhPhanTrang()
        {
            if (tongSoBanGhi == 0)
            {
                lblTongSoDvt.Text = "Không tìm thấy đơn vị tính nào";
                lblTrangHienTai.Text = "0 / 0";
                btnTrangTruoc.Enabled = false;
                btnTrangSau.Enabled = false;
            }
            else
            {
                int tuDong = (trangHienTai - 1) * soDongMoiTrang + 1;
                int denDong = Math.Min(trangHienTai * soDongMoiTrang, tongSoBanGhi);
                lblTongSoDvt.Text = string.Format("Hiển thị {0} - {1} trên tổng số {2} đơn vị tính", tuDong, denDong, tongSoBanGhi);
                lblTrangHienTai.Text = string.Format("{0} / {1}", trangHienTai, tongSoTrang);
                btnTrangTruoc.Enabled = (trangHienTai > 1);
                btnTrangSau.Enabled = (trangHienTai < tongSoTrang);
            }
        }

        #region Các sự kiện giao diện DataGridView

        private void dgvDonViTinh_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.RowIndex < 0 || e.Value == null) return;

            string colName = dgvDonViTinh.Columns[e.ColumnIndex].Name;

            if (colName == "colMaDVT")
            {
                dgvDonViTinh.Rows[e.RowIndex].Cells[e.ColumnIndex].Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            }
            else if (colName == "colSoLuongHangHoa" && int.TryParse(e.Value.ToString(), out int sl))
            {
                e.Value = sl.ToString("N0");
                e.FormattingApplied = true;
                dgvDonViTinh.Rows[e.RowIndex].Cells[e.ColumnIndex].Style.Alignment = DataGridViewContentAlignment.MiddleRight;
            }
            else if (colName == "colTrangThai")
            {
                dgvDonViTinh.Rows[e.RowIndex].Cells[e.ColumnIndex].Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                string val = e.Value.ToString();
                if (val == "Đang sử dụng")
                {
                    // Màu xanh lá chuẩn Nhóm Hoàn thành / Hoạt động (Luật 15b, font chữ thường đồng bộ theo mẫu Trả hàng nhập)
                    e.CellStyle.ForeColor = Color.FromArgb(22, 163, 74);
                }
                else
                {
                    // Màu xám nhạt chuẩn Nhóm Đã hủy / Ngừng (Luật 15b)
                    e.CellStyle.ForeColor = Color.FromArgb(100, 116, 139);
                }
            }
        }

        private void dgvDonViTinh_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;

            // Bỏ qua nếu click vào cột checkbox
            if (dgvDonViTinh.Columns[e.ColumnIndex].Name == "colChon") return;

            MoFormChiTietDongHienTai(e.RowIndex);
        }

        private void MoFormChiTietDongHienTai(int rowIndex)
        {
            try
            {
                var row = dgvDonViTinh.Rows[rowIndex];
                if (row.DataBoundItem is DataRowView drv)
                {
                    int maDvt = Convert.ToInt32(drv["MaDVT"]);
                    using (frmChiTietDonViTinh frm = new frmChiTietDonViTinh(maDvt))
                    {
                        if (frm.ShowDialog(this) == DialogResult.OK)
                        {
                            HienThiDanhSach();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi mở chi tiết đơn vị tính: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        #endregion

        #region Xử lý Header CheckBox "Chọn tất cả"

        private void dgvDonViTinh_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        {
            if (e.RowIndex == -1 && e.ColumnIndex == dgvDonViTinh.Columns["colChon"].Index)
            {
                e.PaintBackground(e.CellBounds, true);

                CheckBoxState state = isHeaderChecked ? CheckBoxState.CheckedNormal : CheckBoxState.UncheckedNormal;
                Point checkPt = new Point(e.CellBounds.Left + (e.CellBounds.Width - 14) / 2,
                                          e.CellBounds.Top + (e.CellBounds.Height - 14) / 2);
                CheckBoxRenderer.DrawCheckBox(e.Graphics, checkPt, state);

                e.Handled = true;
            }
        }

        private void dgvDonViTinh_ColumnHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.ColumnIndex == dgvDonViTinh.Columns["colChon"].Index)
            {
                // Giải phóng ô đang edit
                dgvDonViTinh.CurrentCell = null;

                isHeaderChecked = !isHeaderChecked;
                isUpdatingCheckAll = true;

                if (dgvDonViTinh.DataSource is DataTable dt)
                {
                    foreach (DataRow r in dt.Rows)
                    {
                        r["Chon"] = isHeaderChecked;
                    }
                }

                isUpdatingCheckAll = false;
                dgvDonViTinh.Invalidate();
            }
        }

        private void dgvDonViTinh_CurrentCellDirtyStateChanged(object sender, EventArgs e)
        {
            if (dgvDonViTinh.IsCurrentCellDirty && dgvDonViTinh.CurrentCell.ColumnIndex == dgvDonViTinh.Columns["colChon"].Index)
            {
                dgvDonViTinh.CommitEdit(DataGridViewDataErrorContexts.Commit);
            }
        }

        private void dgvDonViTinh_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (isUpdatingCheckAll) return;
            if (e.RowIndex >= 0 && dgvDonViTinh.Columns[e.ColumnIndex].Name == "colChon")
            {
                KiemTraVaCapNhatHeaderCheck();
            }
        }

        private void KiemTraVaCapNhatHeaderCheck()
        {
            if (dgvDonViTinh.DataSource is DataTable dt && dt.Rows.Count > 0)
            {
                bool tatCaDaChon = true;
                foreach (DataRow r in dt.Rows)
                {
                    if (r["Chon"] == DBNull.Value || !Convert.ToBoolean(r["Chon"]))
                    {
                        tatCaDaChon = false;
                        break;
                    }
                }
                isHeaderChecked = tatCaDaChon;
                dgvDonViTinh.InvalidateCell(dgvDonViTinh.Columns["colChon"].Index, -1);
            }
        }

        #endregion

        #region Các nút thao tác Top Bar

        private void btnThemDonViTinh_Click(object sender, EventArgs e)
        {
            using (frmChiTietDonViTinh frm = new frmChiTietDonViTinh())
            {
                if (frm.ShowDialog(this) == DialogResult.OK)
                {
                    HienThiDanhSach();
                }
            }
        }

        private void btnXuatFile_Click(object sender, EventArgs e)
        {
            try
            {
                DataTable dt = dgvDonViTinh.DataSource as DataTable;
                if (dt == null || dt.Rows.Count == 0)
                {
                    MessageBox.Show("Không có dữ liệu để xuất file!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                using (SaveFileDialog sfd = new SaveFileDialog())
                {
                    sfd.Filter = "File Excel / CSV (*.csv)|*.csv|Tất cả các file (*.*)|*.*";
                    sfd.FileName = string.Format("DanhSachDonViTinh_{0:yyyyMMdd_HHmmss}.csv", DateTime.Now);
                    sfd.Title = "Xuất danh sách đơn vị tính ra file CSV";

                    if (sfd.ShowDialog() == DialogResult.OK)
                    {
                        StringBuilder sb = new StringBuilder();

                        // Header chỉ lấy các cột Visible
                        bool firstCol = true;
                        foreach (DataGridViewColumn col in dgvDonViTinh.Columns)
                        {
                            if (col.Visible && col.Name != "colChon")
                            {
                                if (!firstCol) sb.Append(",");
                                sb.Append("\"" + col.HeaderText.Replace("\"", "\"\"") + "\"");
                                firstCol = false;
                            }
                        }
                        sb.AppendLine();

                        // Rows
                        for (int i = 0; i < dgvDonViTinh.Rows.Count; i++)
                        {
                            var row = dgvDonViTinh.Rows[i];
                            firstCol = true;
                            foreach (DataGridViewColumn col in dgvDonViTinh.Columns)
                            {
                                if (col.Visible && col.Name != "colChon")
                                {
                                    if (!firstCol) sb.Append(",");
                                    string cellVal = row.Cells[col.Index].FormattedValue?.ToString() ?? "";
                                    sb.Append("\"" + cellVal.Replace("\"", "\"\"") + "\"");
                                    firstCol = false;
                                }
                            }
                            sb.AppendLine();
                        }

                        // Ghi file với UTF-8 with BOM để mở trực tiếp trong Excel không bị lỗi font tiếng Việt
                        File.WriteAllText(sfd.FileName, sb.ToString(), new UTF8Encoding(true));

                        DialogResult dr = MessageBox.Show("Xuất file thành công! Bạn có muốn mở file ngay không?",
                            "Xuất file", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
                        if (dr == DialogResult.Yes)
                        {
                            System.Diagnostics.Process.Start(sfd.FileName);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi xuất file: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnHienCot_Click(object sender, EventArgs e)
        {
            try
            {
                frmHienCotDvt frm = new frmHienCotDvt(dgvDonViTinh);
                Point pt = btnHienCot.PointToScreen(new Point(0, btnHienCot.Height));
                frm.Location = new Point(pt.X + btnHienCot.Width - frm.Width, pt.Y);
                frm.Show();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi mở tùy chọn hiện cột: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnHuongDanSuDung_Click(object sender, EventArgs e)
        {
            string huongDan = @"HƯỚNG DẪN QUẢN LÝ ĐƠN VỊ TÍNH

1. Xem & Quản lý danh sách:
   - Danh sách hiển thị toàn bộ đơn vị tính kèm số lượng mặt hàng đang sử dụng.
   - Nhấn đúp chuột vào bất kỳ dòng nào để xem chi tiết, chỉnh sửa thông tin hoặc xóa đơn vị tính.

2. Thêm mới đơn vị tính:
   - Bấm nút '+ Đơn vị tính' ở thanh trên cùng để mở form tạo mới.
   - Hoặc bấm nút '+' cạnh ComboBox Đơn vị tính trên form Thêm/Sửa Hàng hóa.
   - Mã đơn vị tính được hệ thống tự động sinh nhưng cho phép người dùng tùy ý chỉnh sửa (có kiểm tra chống trùng mã).

3. Xóa đơn vị tính:
   - Nhấn đúp vào đơn vị tính cần xóa, bấm nút 'Xóa' ở góc dưới bên trái của form chi tiết.
   - LƯU Ý BẢO MẬT: Hệ thống tự động chặn không cho xóa bất kỳ đơn vị tính nào đang được áp dụng cho hàng hóa.

4. Tìm kiếm & Lọc:
   - Gõ từ khóa vào ô tìm kiếm để lọc nhanh theo tên hoặc mã.
   - Sử dụng bộ lọc bên trái để lọc theo trạng thái sử dụng hoặc sắp xếp theo số lượng hàng.";

            MessageBox.Show(huongDan, "Hướng dẫn quản lý đơn vị tính", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        #endregion

        #region Phân trang & Tìm kiếm

        private void txtTimKiem_TextChanged(object sender, EventArgs e)
        {
            trangHienTai = 1;
            HienThiDanhSach();
        }

        private void BoLoc_SelectedIndexChanged(object sender, EventArgs e)
        {
            trangHienTai = 1;
            HienThiDanhSach();
        }

        private void cboSoDongMoiTrang_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (int.TryParse(cboSoDongMoiTrang.SelectedItem?.ToString(), out int val))
            {
                soDongMoiTrang = val;
                trangHienTai = 1;
                HienThiDanhSach();
            }
        }

        private void btnTrangTruoc_Click(object sender, EventArgs e)
        {
            if (trangHienTai > 1)
            {
                trangHienTai--;
                HienThiDanhSach();
            }
        }

        private void btnTrangSau_Click(object sender, EventArgs e)
        {
            if (trangHienTai < tongSoTrang)
            {
                trangHienTai++;
                HienThiDanhSach();
            }
        }

        #endregion
    }
}
