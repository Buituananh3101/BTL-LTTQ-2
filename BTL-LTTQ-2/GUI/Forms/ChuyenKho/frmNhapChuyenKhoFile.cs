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
    public partial class frmNhapChuyenKhoFile : Form
    {
        public DataTable DtKetQuaNhap { get; private set; }
        private DataTable dtXemTruoc;
        private int maKhoNguon;

        public frmNhapChuyenKhoFile(int maKhoNguon = 0)
        {
            InitializeComponent();
            this.maKhoNguon = maKhoNguon > 0 ? maKhoNguon : SessionContext.MaKhoHienTai;
            KhoiTaoBangDuLieu();
        }

        private void frmNhapChuyenKhoFile_Load(object sender, EventArgs e)
        {
            try
            {
                typeof(DataGridView).InvokeMember("DoubleBuffered",
                    System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.SetProperty,
                    null, dgvXemTruoc, new object[] { true });
            }
            catch { }

            dgvXemTruoc.AutoGenerateColumns = false;
            dgvXemTruoc.BorderStyle = BorderStyle.None;
            dgvXemTruoc.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(248, 250, 252);
            dgvXemTruoc.DefaultCellStyle.SelectionBackColor = Color.FromArgb(224, 231, 255);
            dgvXemTruoc.DefaultCellStyle.SelectionForeColor = Color.FromArgb(30, 41, 59);

            colStt.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            colMaHang.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            colDonViTinh.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            colSoLuongChuyen.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            colSoLuongChuyen.DefaultCellStyle.Format = "N0";
            colDonGia.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            colDonGia.DefaultCellStyle.Format = "N0";
        }

        private void KhoiTaoBangDuLieu()
        {
            dtXemTruoc = new DataTable();
            dtXemTruoc.Columns.Add("STT", typeof(int));
            dtXemTruoc.Columns.Add("MaHang", typeof(int));
            dtXemTruoc.Columns.Add("MaHangNoiBo", typeof(string));
            dtXemTruoc.Columns.Add("TenHang", typeof(string));
            dtXemTruoc.Columns.Add("TenDVT", typeof(string));
            dtXemTruoc.Columns.Add("SoLuongChuyen", typeof(decimal));
            dtXemTruoc.Columns.Add("DonGia", typeof(decimal));
            dtXemTruoc.Columns.Add("GhiChu", typeof(string));
            dtXemTruoc.Columns.Add("TrangThaiDong", typeof(string));
            dtXemTruoc.Columns.Add("HopLe", typeof(bool));

            dgvXemTruoc.DataSource = dtXemTruoc;
        }

        private void btnTaiFileMauCsv_Click(object sender, EventArgs e)
        {
            try
            {
                SaveFileDialog sfd = new SaveFileDialog();
                sfd.Filter = "File CSV (*.csv)|*.csv";
                sfd.FileName = "Mau_Chuyen_Kho.csv";

                if (sfd.ShowDialog() == DialogResult.OK)
                {
                    StringBuilder sb = new StringBuilder();
                    sb.AppendLine("MaHang,TenHang,SoLuongChuyen,GhiChu");

                    string sql = @"SELECT TOP 5 h.MaHangNoiBo, h.TenHang, ISNULL(t.SoLuongTon, 0) AS TonKho
                                   FROM HangHoa h
                                   LEFT JOIN vw_TonKhoHienTai t ON h.MaHang = t.MaHang AND t.MaKho = @MaKho
                                   WHERE h.TrangThai = 'DANG_BAN' OR h.TrangThai = 'DANG_KINH_DOANH'
                                   ORDER BY h.MaHang ASC";
                    DataTable dt = DataProcessing.ReadData(sql, new SqlParameter[] { new SqlParameter("@MaKho", this.maKhoNguon) });

                    if (dt != null && dt.Rows.Count > 0)
                    {
                        foreach (DataRow r in dt.Rows)
                        {
                            string ma = r["MaHangNoiBo"].ToString();
                            string ten = r["TenHang"].ToString().Replace(",", " ");
                            decimal ton = Convert.ToDecimal(r["TonKho"]);
                            decimal sl = ton > 5 ? 5 : (ton > 0 ? ton : 1);
                            sb.AppendLine(string.Format("{0},{1},{2},Chuyển mẫu", ma, ten, sl));
                        }
                    }
                    else
                    {
                        sb.AppendLine("GAS45,Bình gas công nghiệp 45kg,5,Chuyển mẫu");
                        sb.AppendLine("BEP-DOI,Bếp gas đôi Paloma cao cấp,2,Chuyển mẫu");
                    }

                    File.WriteAllText(sfd.FileName, sb.ToString(), Encoding.UTF8);
                    MessageBox.Show("Tải file mẫu CSV thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi tải file mẫu: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnChonFile_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "File dữ liệu (*.csv;*.txt)|*.csv;*.txt|Tất cả các file (*.*)|*.*";
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                txtDuongDanFile.Text = ofd.FileName;
                btnDocDuLieu.Enabled = true;
            }
        }

        private void btnDocDuLieu_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtDuongDanFile.Text) || !File.Exists(txtDuongDanFile.Text))
            {
                MessageBox.Show("Vui lòng chọn file dữ liệu hợp lệ!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                dtXemTruoc.Clear();
                string[] lines = File.ReadAllLines(txtDuongDanFile.Text, Encoding.UTF8);

                int stt = 1;
                int hopLeCount = 0;
                int loiCount = 0;

                for (int i = 1; i < lines.Length; i++)
                {
                    string line = lines[i].Trim();
                    if (string.IsNullOrEmpty(line)) continue;

                    string[] parts = line.Split(',');
                    if (parts.Length < 3) continue;

                    string maHangNoiBo = parts[0].Trim();
                    string tenHangCsv = parts[1].Trim();
                    string slStr = parts[2].Trim();
                    string ghiChu = parts.Length > 3 ? parts[3].Trim() : "";

                    decimal slChuyen = 0;
                    bool parseSl = decimal.TryParse(slStr, out slChuyen);

                    string sqlHang = @"SELECT h.MaHang, h.MaHangNoiBo, h.TenHang, dvt.TenDVT, ISNULL(h.GiaVon, 0) AS GiaVon,
                                              ISNULL(t.SoLuongTon, 0) AS TonKho
                                       FROM HangHoa h
                                       LEFT JOIN DonViTinh dvt ON h.MaDvt = dvt.MaDvt
                                       LEFT JOIN vw_TonKhoHienTai t ON h.MaHang = t.MaHang AND t.MaKho = @MaKho
                                       WHERE h.MaHangNoiBo = @MaHangNoiBo";
                    DataTable dtHang = DataProcessing.ReadData(sqlHang, new SqlParameter[] {
                        new SqlParameter("@MaHangNoiBo", maHangNoiBo),
                        new SqlParameter("@MaKho", this.maKhoNguon)
                    });

                    DataRow newRow = dtXemTruoc.NewRow();
                    newRow["STT"] = stt++;
                    newRow["MaHangNoiBo"] = maHangNoiBo;
                    newRow["GhiChu"] = ghiChu;

                    if (dtHang == null || dtHang.Rows.Count == 0)
                    {
                        newRow["TenHang"] = tenHangCsv;
                        newRow["TenDVT"] = "";
                        newRow["SoLuongChuyen"] = slChuyen;
                        newRow["DonGia"] = 0;
                        newRow["TrangThaiDong"] = "Không tìm thấy mã hàng";
                        newRow["HopLe"] = false;
                        loiCount++;
                    }
                    else if (!parseSl || slChuyen <= 0)
                    {
                        DataRow r = dtHang.Rows[0];
                        newRow["MaHang"] = r["MaHang"];
                        newRow["TenHang"] = r["TenHang"];
                        newRow["TenDVT"] = r["TenDVT"];
                        newRow["SoLuongChuyen"] = slChuyen;
                        newRow["DonGia"] = r["GiaVon"];
                        newRow["TrangThaiDong"] = "Số lượng không hợp lệ (<= 0)";
                        newRow["HopLe"] = false;
                        loiCount++;
                    }
                    else
                    {
                        DataRow r = dtHang.Rows[0];
                        decimal tonKho = Convert.ToDecimal(r["TonKho"]);
                        newRow["MaHang"] = r["MaHang"];
                        newRow["TenHang"] = r["TenHang"];
                        newRow["TenDVT"] = r["TenDVT"];
                        newRow["SoLuongChuyen"] = slChuyen;
                        newRow["DonGia"] = r["GiaVon"];

                        if (slChuyen > tonKho)
                        {
                            newRow["TrangThaiDong"] = string.Format("Vượt tồn kho gửi ({0:N0} > {1:N0})", slChuyen, tonKho);
                            newRow["HopLe"] = false;
                            loiCount++;
                        }
                        else
                        {
                            newRow["TrangThaiDong"] = "Hợp lệ";
                            newRow["HopLe"] = true;
                            hopLeCount++;
                        }
                    }

                    dtXemTruoc.Rows.Add(newRow);
                }

                lblThongKe.Text = string.Format("Hợp lệ: {0} dòng | Lỗi: {1} dòng", hopLeCount, loiCount);
                btnDongY.Enabled = hopLeCount > 0;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi đọc file dữ liệu: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnDongY_Click(object sender, EventArgs e)
        {
            DataView dv = new DataView(dtXemTruoc);
            dv.RowFilter = "HopLe = true";
            this.DtKetQuaNhap = dv.ToTable();

            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void btnDong_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }
    }
}
