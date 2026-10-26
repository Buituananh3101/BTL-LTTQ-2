using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using BTL_LTTQ_2.Classes;

namespace BTL_LTTQ_2.GUI.Forms.DanhSachHangHoa
{
    public partial class frmViTri : Form
    {
        private DataTable dtViTri;
        private int maViTriDangChon = 0;
        private string tenViTriDangChon = "";

        public frmViTri()
        {
            InitializeComponent();
            this.Load += frmViTri_Load;
        }

        private void frmViTri_Load(object sender, EventArgs e)
        {
            DinhDangGiaoDien();
            NapDuLieu();
            DatLaiTrangThaiBanDau();
        }

        private void DinhDangGiaoDien()
        {
            // Bật đệm đôi chống giật lag
            typeof(DataGridView).InvokeMember("DoubleBuffered",
                System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.SetProperty,
                null, dgvViTri, new object[] { true });

            // Đưa DataGridView lên trên để tự động Fill chính xác phần diện tích còn lại
            dgvViTri.BringToFront();

            // Cấu hình font chữ và kiểu dáng tiêu đề cột giống hệt frmNhomHang
            dgvViTri.Font = new Font("Segoe UI", 9.5F, FontStyle.Regular);
            dgvViTri.EnableHeadersVisualStyles = true;
            dgvViTri.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 9.5F, FontStyle.Regular);
            dgvViTri.ColumnHeadersDefaultCellStyle.WrapMode = DataGridViewTriState.False;
            dgvViTri.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            dgvViTri.ColumnHeadersHeight = 32;
            dgvViTri.AutoGenerateColumns = false;
            dgvViTri.Columns.Clear();
            dgvViTri.ReadOnly = true;
            dgvViTri.AllowUserToAddRows = false;
            dgvViTri.AllowUserToDeleteRows = false;
            dgvViTri.AllowUserToResizeRows = false;
            dgvViTri.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvViTri.MultiSelect = false;
            dgvViTri.RowHeadersVisible = false;
            dgvViTri.RowTemplate.Height = 36;
            dgvViTri.BackgroundColor = Color.White;
            dgvViTri.BorderStyle = BorderStyle.None;

            // Màu dòng xen kẽ và lựa chọn hiện đại
            dgvViTri.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(248, 250, 252);
            dgvViTri.DefaultCellStyle.SelectionBackColor = Color.FromArgb(224, 231, 255);
            dgvViTri.DefaultCellStyle.SelectionForeColor = Color.FromArgb(30, 41, 59);

            // Cột 1: Mã vị trí (Ẩn ngầm phục vụ sửa/xóa)
            dgvViTri.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "colMaViTri",
                DataPropertyName = "MaViTri",
                HeaderText = "Mã vị trí",
                Visible = false
            });

            // Cột 2: Tên vị trí
            dgvViTri.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "colTenViTri",
                DataPropertyName = "TenViTri",
                HeaderText = "Tên vị trí",
                AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill,
                DefaultCellStyle = new DataGridViewCellStyle { Alignment = DataGridViewContentAlignment.MiddleLeft }
            });

            // Cột 3: Số mặt hàng đang đặt tại vị trí này
            dgvViTri.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "colSoLuongHang",
                DataPropertyName = "SoLuongHang",
                HeaderText = "Số mặt hàng",
                Width = 130,
                DefaultCellStyle = new DataGridViewCellStyle { Alignment = DataGridViewContentAlignment.MiddleRight, Format = "N0" }
            });

            // Đăng ký sự kiện
            dgvViTri.CellClick += dgvViTri_CellClick;
            dgvViTri.CellDoubleClick += dgvViTri_CellDoubleClick;
            txtTimKiem.TextChanged += txtTimKiem_TextChanged;
            btnThem.Click += btnThem_Click;
            btnSua.Click += btnSua_Click;
            if (btnHuy != null) btnHuy.Click += btnHuy_Click;
            if (btnXuatFile != null) btnXuatFile.Click += btnXuatFile_Click;
            if (btnNhapFile != null) btnNhapFile.Click += btnNhapFile_Click;

            // Hỗ trợ phím Escape để hủy chọn hoặc đóng form
            this.KeyPreview = true;
            this.KeyDown += (s, ev) =>
            {
                if (ev.KeyCode == Keys.Escape)
                {
                    if (maViTriDangChon > 0)
                    {
                        DatLaiTrangThaiBanDau();
                    }
                    else
                    {
                        this.Close();
                    }
                }
            };
        }

        private void NapDuLieu()
        {
            try
            {
                string sql = @"
                    SELECT vt.MaViTri, vt.TenViTri, COUNT(h.MaHang) AS SoLuongHang
                    FROM ViTri vt
                    LEFT JOIN HangHoa h ON vt.TenViTri = h.ViTri
                    WHERE vt.TrangThai = 'HOAT_DONG'
                    GROUP BY vt.MaViTri, vt.TenViTri
                    ORDER BY vt.TenViTri;";

                dtViTri = DataProcessing.ReadData(sql);
                dgvViTri.DataSource = dtViTri;
                CapNhatTongSo();

                // Cập nhật lại bộ lọc tìm kiếm nếu đang gõ
                if (!string.IsNullOrWhiteSpace(txtTimKiem.Text))
                {
                    txtTimKiem_TextChanged(this, EventArgs.Empty);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi tải danh sách vị trí: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void CapNhatTongSo()
        {
            if (lblTongSoViTri == null) return;
            int count = dgvViTri.Rows.Count;
            lblTongSoViTri.Text = $"Tổng số: {count:N0} vị trí";
        }

        private void DatLaiTrangThaiBanDau()
        {
            maViTriDangChon = 0;
            tenViTriDangChon = "";
            txtTenViTri.Clear();
            btnThem.Visible = true;
            btnThem.Enabled = true;
            btnSua.Visible = false;
            if (btnHuy != null) btnHuy.Visible = false;

            if (dgvViTri.CurrentRow != null)
            {
                dgvViTri.ClearSelection();
            }
            CapNhatTongSo();
            txtTenViTri.Focus();
        }

        // Click vào dòng: hiển thị tên vị trí ra textbox, giữ nút Thêm nhưng disable, hiện nút Sửa và nút Hủy
        private void dgvViTri_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0 || e.RowIndex >= dgvViTri.Rows.Count) return;

            var cellMaViTri = dgvViTri.Rows[e.RowIndex].Cells["colMaViTri"].Value;
            var cellTenViTri = dgvViTri.Rows[e.RowIndex].Cells["colTenViTri"].Value;
            if (cellMaViTri == null || !int.TryParse(cellMaViTri.ToString(), out int maViTri)) return;

            maViTriDangChon = maViTri;
            tenViTriDangChon = cellTenViTri?.ToString() ?? "";
            txtTenViTri.Text = tenViTriDangChon;

            btnThem.Visible = true;
            btnThem.Enabled = false;
            btnSua.Visible = true;
            btnSua.Enabled = true;
            if (btnHuy != null)
            {
                btnHuy.Visible = true;
                btnHuy.Enabled = true;
            }
        }

        // Bấm nút Thêm: thêm vị trí mới
        private void btnThem_Click(object sender, EventArgs e)
        {
            string tenViTri = txtTenViTri.Text.Trim();
            if (string.IsNullOrWhiteSpace(tenViTri))
            {
                MessageBox.Show("Vui lòng nhập tên vị trí!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtTenViTri.Focus();
                return;
            }

            if (tenViTri.Length > 100)
            {
                MessageBox.Show("Tên vị trí không được vượt quá 100 ký tự!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtTenViTri.Focus();
                return;
            }

            try
            {
                // Kiểm tra trùng tên vị trí
                string sqlCheck = "SELECT COUNT(1) FROM ViTri WHERE UPPER(LTRIM(RTRIM(TenViTri))) = UPPER(@TenViTri)";
                object res = DataProcessing.ReadValue(sqlCheck, new SqlParameter[] { new SqlParameter("@TenViTri", tenViTri) });
                if (res != null && int.TryParse(res.ToString(), out int count) && count > 0)
                {
                    MessageBox.Show($"Tên vị trí '{tenViTri}' đã tồn tại trong hệ thống!", "Cảnh báo trùng", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtTenViTri.SelectAll();
                    txtTenViTri.Focus();
                    return;
                }

                // Thêm mới
                string sqlInsert = "INSERT INTO ViTri (TenViTri, TrangThai) VALUES (@TenViTri, 'HOAT_DONG')";
                DataProcessing.ChangeData(sqlInsert, new SqlParameter[] { new SqlParameter("@TenViTri", tenViTri) });

                // Thêm thành công: tự động làm mới danh sách (không hiện popup)
                NapDuLieu();
                DatLaiTrangThaiBanDau();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi thêm vị trí: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Bấm nút Sửa: cập nhật tên vị trí đang chọn
        private void btnSua_Click(object sender, EventArgs e)
        {
            if (maViTriDangChon <= 0)
            {
                MessageBox.Show("Vui lòng chọn vị trí cần sửa từ danh sách!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string tenMoi = txtTenViTri.Text.Trim();
            if (string.IsNullOrWhiteSpace(tenMoi))
            {
                MessageBox.Show("Vui lòng nhập tên vị trí!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtTenViTri.Focus();
                return;
            }

            if (tenMoi.Length > 100)
            {
                MessageBox.Show("Tên vị trí không được vượt quá 100 ký tự!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtTenViTri.Focus();
                return;
            }

            try
            {
                // Kiểm tra trùng tên với vị trí khác
                string sqlCheck = "SELECT COUNT(1) FROM ViTri WHERE UPPER(LTRIM(RTRIM(TenViTri))) = UPPER(@TenViTri) AND MaViTri <> @MaViTri";
                object res = DataProcessing.ReadValue(sqlCheck, new SqlParameter[] {
                    new SqlParameter("@TenViTri", tenMoi),
                    new SqlParameter("@MaViTri", maViTriDangChon)
                });
                if (res != null && int.TryParse(res.ToString(), out int count) && count > 0)
                {
                    MessageBox.Show($"Tên vị trí '{tenMoi}' đã trùng với một vị trí khác!", "Cảnh báo trùng", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtTenViTri.SelectAll();
                    txtTenViTri.Focus();
                    return;
                }

                // Cập nhật CSDL trong Transaction (Cập nhật bảng ViTri và đồng bộ HangHoa)
                DataProcessing.ChangeDataTransaction((conn, trans) =>
                {
                    string sqlUpdateViTri = "UPDATE ViTri SET TenViTri = @TenMoi WHERE MaViTri = @MaViTri";
                    using (SqlCommand cmd = new SqlCommand(sqlUpdateViTri, conn, trans))
                    {
                        cmd.Parameters.AddWithValue("@TenMoi", tenMoi);
                        cmd.Parameters.AddWithValue("@MaViTri", maViTriDangChon);
                        cmd.ExecuteNonQuery();
                    }

                    if (!string.IsNullOrEmpty(tenViTriDangChon))
                    {
                        string sqlUpdateHangHoa = "UPDATE HangHoa SET ViTri = @TenMoi WHERE ViTri = @TenCu";
                        using (SqlCommand cmd = new SqlCommand(sqlUpdateHangHoa, conn, trans))
                        {
                            cmd.Parameters.AddWithValue("@TenMoi", tenMoi);
                            cmd.Parameters.AddWithValue("@TenCu", tenViTriDangChon);
                            cmd.ExecuteNonQuery();
                        }
                    }
                });

                MessageBox.Show("Cập nhật tên vị trí thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                NapDuLieu();
                DatLaiTrangThaiBanDau();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi sửa vị trí: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Double click vào dòng để xóa và hỏi xác nhận
        private void dgvViTri_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0 || e.RowIndex >= dgvViTri.Rows.Count) return;

            var cellMaViTri = dgvViTri.Rows[e.RowIndex].Cells["colMaViTri"].Value;
            var cellTenViTri = dgvViTri.Rows[e.RowIndex].Cells["colTenViTri"].Value;
            if (cellMaViTri == null || !int.TryParse(cellMaViTri.ToString(), out int maViTri)) return;

            string tenViTri = cellTenViTri?.ToString() ?? "";

            try
            {
                // Kiểm tra xem vị trí đã có mặt hàng nào đặt ở đó chưa
                string sqlCheck = "SELECT COUNT(1) FROM HangHoa WHERE ViTri = @TenViTri";
                object res = DataProcessing.ReadValue(sqlCheck, new SqlParameter[] { new SqlParameter("@TenViTri", tenViTri) });
                int soHang = res != null && int.TryParse(res.ToString(), out int c) ? c : 0;

                if (soHang > 0)
                {
                    MessageBox.Show($"Không thể xóa vị trí '{tenViTri}' vì đang có {soHang} mặt hàng thuộc vị trí này!",
                        "Không thể xóa", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                DialogResult xacNhan = MessageBox.Show(
                    $"Bạn có chắc chắn muốn xóa vị trí '{tenViTri}' không?",
                    "Xác nhận xóa vị trí",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question);

                if (xacNhan == DialogResult.Yes)
                {
                    string sqlDelete = "DELETE FROM ViTri WHERE MaViTri = @MaViTri";
                    DataProcessing.ChangeData(sqlDelete, new SqlParameter[] { new SqlParameter("@MaViTri", maViTri) });

                    // Xóa thành công: không cần thông báo, tự động làm mới
                    NapDuLieu();
                    DatLaiTrangThaiBanDau();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi xóa vị trí: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Tìm kiếm realtime theo tên vị trí
        private void txtTimKiem_TextChanged(object sender, EventArgs e)
        {
            if (dtViTri == null) return;
            // Thoát ký tự đặc biệt (' [ ] * %) để RowFilter không lỗi khi gõ ký tự đại diện
            string kw = ComboboxHelper.ThoatKyTuLikeRowFilter(txtTimKiem.Text.Trim());
            if (string.IsNullOrEmpty(kw))
            {
                dtViTri.DefaultView.RowFilter = "";
            }
            else
            {
                dtViTri.DefaultView.RowFilter = $"TenViTri LIKE '%{kw}%'";
            }
            CapNhatTongSo();
        }

        // Bấm nút Hủy để trở về chế độ thêm mới
        private void btnHuy_Click(object sender, EventArgs e)
        {
            DatLaiTrangThaiBanDau();
        }

        // Bấm nút Xuất file: Xuất danh sách vị trí đang hiển thị ra file Excel (.xls) hoặc CSV (.csv)
        private void btnXuatFile_Click(object sender, EventArgs e)
        {
            if (dgvViTri.Rows.Count == 0)
            {
                MessageBox.Show("Không có dữ liệu vị trí để xuất file!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            using (SaveFileDialog sfd = new SaveFileDialog())
            {
                sfd.Title = "Xuất danh sách vị trí";
                sfd.Filter = "File Excel (*.xls)|*.xls|File CSV (*.csv)|*.csv";
                sfd.FileName = $"DanhSachViTri_{DateTime.Now:yyyyMMdd_HHmm}.xls";

                if (sfd.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        string ext = Path.GetExtension(sfd.FileName).ToLower();
                        if (ext == ".xls")
                        {
                            StringBuilder sb = new StringBuilder();
                            sb.AppendLine("<!DOCTYPE html>");
                            sb.AppendLine("<html><head><meta http-equiv=\"content-type\" content=\"application/vnd.ms-excel; charset=UTF-8\" />");
                            sb.AppendLine("<style>");
                            sb.AppendLine("table { border-collapse: collapse; font-family: Segoe UI, Tahoma, sans-serif; font-size: 11pt; }");
                            sb.AppendLine("th { background-color: #E2E8F0; color: #0F172A; font-weight: bold; border: 1px solid #94A3B8; padding: 8px 14px; }");
                            sb.AppendLine("td { border: 1px solid #CBD5E1; padding: 6px 12px; }");
                            sb.AppendLine(".text { mso-number-format: '\\@'; text-align: left; }");
                            sb.AppendLine(".number { mso-number-format: '#,##0'; text-align: right; }");
                            sb.AppendLine("</style></head><body>");
                            sb.AppendLine("<table>");

                            // Dòng tiêu đề
                            sb.AppendLine("<tr>");
                            sb.AppendLine("<th>Tên vị trí</th>");
                            sb.AppendLine("<th>Số mặt hàng</th>");
                            sb.AppendLine("</tr>");

                            // Dữ liệu từng dòng
                            foreach (DataGridViewRow row in dgvViTri.Rows)
                            {
                                string tenViTri = row.Cells["colTenViTri"].Value?.ToString() ?? "";
                                string soLuong = row.Cells["colSoLuongHang"].FormattedValue?.ToString() ?? "0";
                                sb.AppendLine("<tr>");
                                sb.AppendLine($"<td class=\"text\">{System.Net.WebUtility.HtmlEncode(tenViTri)}</td>");
                                sb.AppendLine($"<td class=\"number\">{System.Net.WebUtility.HtmlEncode(soLuong)}</td>");
                                sb.AppendLine("</tr>");
                            }

                            sb.AppendLine("</table></body></html>");
                            File.WriteAllText(sfd.FileName, sb.ToString(), new UTF8Encoding(true));
                        }
                        else
                        {
                            // Xuất định dạng CSV chuẩn UTF-8 có BOM
                            StringBuilder sb = new StringBuilder();
                            sb.AppendLine("sep=,");
                            sb.AppendLine("\"Tên vị trí\",\"Số mặt hàng\"");

                            foreach (DataGridViewRow row in dgvViTri.Rows)
                            {
                                string tenViTri = row.Cells["colTenViTri"].Value?.ToString() ?? "";
                                string soLuong = row.Cells["colSoLuongHang"].FormattedValue?.ToString() ?? "0";
                                sb.AppendLine($"\"{tenViTri.Replace("\"", "\"\"")}\",\"{soLuong.Replace("\"", "\"\"")}\"");
                            }

                            File.WriteAllText(sfd.FileName, sb.ToString(), new UTF8Encoding(true));
                        }

                        DialogResult dr = MessageBox.Show(
                            $"Xuất file thành công {dgvViTri.Rows.Count:N0} vị trí!\n\nĐường dẫn:\n{sfd.FileName}\n\nBạn có muốn mở file vừa xuất không?",
                            "Xuất file thành công",
                            MessageBoxButtons.YesNo,
                            MessageBoxIcon.Question);

                        if (dr == DialogResult.Yes)
                        {
                            Process.Start(sfd.FileName);
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Lỗi khi xuất file: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        // Bấm nút Nhập file: Đọc dữ liệu từ file Excel/CSV/TXT và nhập vào CSDL
        private void btnNhapFile_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog ofd = new OpenFileDialog())
            {
                ofd.Title = "Chọn file dữ liệu vị trí để nhập";
                ofd.Filter = "Tất cả định dạng hỗ trợ (*.csv;*.xls;*.xlsx;*.txt)|*.csv;*.xls;*.xlsx;*.txt|File CSV (*.csv)|*.csv|File Excel (*.xls;*.xlsx)|*.xls;*.xlsx|File văn bản (*.txt)|*.txt";

                if (ofd.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        List<string> danhSachTen = DocDuLieuTuFile(ofd.FileName);
                        if (danhSachTen == null || danhSachTen.Count == 0)
                        {
                            MessageBox.Show("Không tìm thấy dữ liệu tên vị trí hợp lệ trong file đã chọn!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            return;
                        }

                        // Lấy danh sách vị trí hiện có trong CSDL để đối chiếu trùng
                        DataTable dtHienCo = DataProcessing.ReadData("SELECT TenViTri FROM ViTri");
                        HashSet<string> tapHopHienCo = new HashSet<string>(StringComparer.OrdinalIgnoreCase);
                        foreach (DataRow r in dtHienCo.Rows)
                        {
                            string t = r["TenViTri"]?.ToString().Trim();
                            if (!string.IsNullOrEmpty(t)) tapHopHienCo.Add(t);
                        }

                        int themMoi = 0;
                        int boQuaTrung = 0;
                        int boQuaLoi = 0;
                        HashSet<string> tapHopFile = new HashSet<string>(StringComparer.OrdinalIgnoreCase);

                        foreach (string rawName in danhSachTen)
                        {
                            string ten = rawName?.Trim();
                            if (string.IsNullOrWhiteSpace(ten)) continue;

                            if (ten.Length > 100)
                            {
                                boQuaLoi++;
                                continue;
                            }

                            // Kiểm tra trùng trong CSDL hoặc trùng trong chính file
                            if (tapHopHienCo.Contains(ten) || tapHopFile.Contains(ten))
                            {
                                boQuaTrung++;
                                continue;
                            }

                            tapHopFile.Add(ten);

                            try
                            {
                                string sqlInsert = "INSERT INTO ViTri (TenViTri, TrangThai) VALUES (@TenViTri, 'HOAT_DONG')";
                                DataProcessing.ChangeData(sqlInsert, new SqlParameter[] { new SqlParameter("@TenViTri", ten) });
                                tapHopHienCo.Add(ten);
                                themMoi++;
                            }
                            catch
                            {
                                boQuaLoi++;
                            }
                        }

                        // Tải lại dữ liệu lên giao diện
                        NapDuLieu();
                        DatLaiTrangThaiBanDau();

                        StringBuilder msg = new StringBuilder();
                        msg.AppendLine("Kết quả nhập file vị trí:");
                        msg.AppendLine($"- Tổng số dòng đọc được: {danhSachTen.Count:N0}");
                        msg.AppendLine($"- Thêm mới thành công: {themMoi:N0}");
                        if (boQuaTrung > 0) msg.AppendLine($"- Bỏ qua do trùng lặp: {boQuaTrung:N0}");
                        if (boQuaLoi > 0) msg.AppendLine($"- Bỏ qua do vượt 100 ký tự hoặc lỗi: {boQuaLoi:N0}");

                        MessageBox.Show(msg.ToString(), "Hoàn tất nhập file", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Lỗi khi đọc file: " + ex.Message, "Lỗi nhập file", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        // Helper đọc dữ liệu theo định dạng file
        private List<string> DocDuLieuTuFile(string filePath)
        {
            string ext = Path.GetExtension(filePath).ToLower();
            List<string> result = new List<string>();

            if (ext == ".csv" || ext == ".txt")
            {
                string[] lines = File.ReadAllLines(filePath, Encoding.UTF8);
                bool daBoQuaTieuDe = false;

                foreach (string rawLine in lines)
                {
                    string line = rawLine?.Trim();
                    if (string.IsNullOrWhiteSpace(line)) continue;
                    if (line.StartsWith("sep=", StringComparison.OrdinalIgnoreCase)) continue;

                    string[] parts = line.Split(new char[] { ',', '\t', ';' });
                    string ten = parts[0].Trim().Trim('"').Trim();

                    // Nhận diện dòng tiêu đề để bỏ qua
                    string tenKhongDau = BoDauTiengViet(ten).ToLower();
                    if (!daBoQuaTieuDe && (tenKhongDau.Contains("ten vi tri") || tenKhongDau.Contains("vi tri") || tenKhongDau == "ten"))
                    {
                        daBoQuaTieuDe = true;
                        continue;
                    }

                    if (!string.IsNullOrWhiteSpace(ten))
                    {
                        result.Add(ten);
                    }
                }
            }
            else if (ext == ".xls" || ext == ".xlsx")
            {
                // Thử đọc theo chuẩn bảng HTML Table (thường xuất từ ứng dụng WinForms)
                string textDau = "";
                try
                {
                    using (StreamReader sr = new StreamReader(filePath, Encoding.UTF8))
                    {
                        char[] buffer = new char[500];
                        int count = sr.Read(buffer, 0, buffer.Length);
                        textDau = new string(buffer, 0, count);
                    }
                }
                catch { }

                if (textDau.IndexOf("<table", StringComparison.OrdinalIgnoreCase) >= 0 ||
                    textDau.IndexOf("<html", StringComparison.OrdinalIgnoreCase) >= 0 ||
                    textDau.IndexOf("<?xml", StringComparison.OrdinalIgnoreCase) >= 0)
                {
                    result = DocFileExcelXmlHtml(filePath);
                }
                else
                {
                    // Đọc qua OleDb
                    result = DocFileExcelOleDb(filePath);
                }
            }

            return result;
        }

        private List<string> DocFileExcelXmlHtml(string filePath)
        {
            List<string> list = new List<string>();
            string html = File.ReadAllText(filePath, Encoding.UTF8);

            MatchCollection rows = Regex.Matches(html, @"<tr[^>]*>(.*?)</tr>", RegexOptions.Singleline | RegexOptions.IgnoreCase);
            bool daBoQuaTieuDe = false;

            foreach (Match r in rows)
            {
                MatchCollection cells = Regex.Matches(r.Groups[1].Value, @"<(?:td|th)[^>]*>(.*?)</(?:td|th)>", RegexOptions.Singleline | RegexOptions.IgnoreCase);
                if (cells.Count > 0)
                {
                    string val = cells[0].Groups[1].Value;
                    val = Regex.Replace(val, @"<[^>]+>", "");
                    val = System.Net.WebUtility.HtmlDecode(val).Trim();

                    string valKhongDau = BoDauTiengViet(val).ToLower();
                    if (!daBoQuaTieuDe && (valKhongDau.Contains("ten vi tri") || valKhongDau.Contains("vi tri") || valKhongDau == "ten"))
                    {
                        daBoQuaTieuDe = true;
                        continue;
                    }

                    if (!string.IsNullOrWhiteSpace(val))
                    {
                        list.Add(val);
                    }
                }
            }

            return list;
        }

        private List<string> DocFileExcelOleDb(string filePath)
        {
            List<string> list = new List<string>();
            string ext = Path.GetExtension(filePath).ToLower();
            string connStr = "";

            if (ext == ".xls")
            {
                connStr = $"Provider=Microsoft.Jet.OLEDB.4.0;Data Source={filePath};Extended Properties=\"Excel 8.0;HDR=YES;IMEX=1\"";
            }
            else
            {
                connStr = $"Provider=Microsoft.ACE.OLEDB.12.0;Data Source={filePath};Extended Properties=\"Excel 12.0 Xml;HDR=YES;IMEX=1\"";
            }

            try
            {
                using (OleDbConnection conn = new OleDbConnection(connStr))
                {
                    conn.Open();
                    DataTable dtSheets = conn.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null);
                    if (dtSheets == null || dtSheets.Rows.Count == 0) return list;

                    string firstSheet = dtSheets.Rows[0]["TABLE_NAME"].ToString();
                    string query = $"SELECT * FROM [{firstSheet}]";

                    using (OleDbCommand cmd = new OleDbCommand(query, conn))
                    using (OleDbDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            if (reader.FieldCount > 0 && reader[0] != DBNull.Value)
                            {
                                string val = reader[0]?.ToString().Trim();
                                if (!string.IsNullOrWhiteSpace(val))
                                {
                                    list.Add(val);
                                }
                            }
                        }
                    }
                }
            }
            catch
            {
                // Nếu OleDb thiếu driver, fallback sang đọc dạng văn bản
                list = DocFileExcelXmlHtml(filePath);
            }

            return list;
        }

        private string BoDauTiengViet(string text)
        {
            if (string.IsNullOrEmpty(text)) return "";
            string normalized = text.Normalize(NormalizationForm.FormD);
            Regex regex = new Regex(@"\p{IsCombiningDiacriticalMarks}+");
            string noDiacritics = regex.Replace(normalized, string.Empty).Replace('đ', 'd').Replace('Đ', 'D');
            return noDiacritics;
        }
    }
}
