using BTL_LTTQ_2.Classes;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace BTL_LTTQ_2.GUI.Forms.DoiKho
{
    public partial class frmDoiKho : Form
    {
        public frmDoiKho()
        {
            InitializeComponent();
        }

        private void frmDoiKho_Load(object sender, EventArgs e)
        {
            TaiDanhSachKho();
        }

        private void TaiDanhSachKho()
        {
            try
            {
                string sql;
                SqlParameter[] paras = null;

                if (SessionContext.IsAdmin)
                {
                    sql = "SELECT MaKho, TenKho FROM Kho WHERE TrangThai = 'HOAT_DONG' OR TrangThai = 'ACTIVE' ORDER BY MaKho ASC";
                }
                else
                {
                    sql = @"SELECT k.MaKho, k.TenKho 
                            FROM Kho k 
                            INNER JOIN TaiKhoanKho tk ON k.MaKho = tk.MaKho 
                            WHERE tk.MaTaiKhoan = @MaTaiKhoan 
                              AND (k.TrangThai = 'HOAT_DONG' OR k.TrangThai = 'ACTIVE') 
                            ORDER BY k.MaKho ASC";
                    paras = new SqlParameter[] { new SqlParameter("@MaTaiKhoan", SessionContext.MaTaiKhoan) };
                }

                DataTable dtKho = DataProcessing.ReadData(sql, paras);
                if (dtKho != null && dtKho.Rows.Count > 0)
                {
                    cboKhoLamViec.DataSource = dtKho;
                    cboKhoLamViec.DisplayMember = "TenKho";
                    cboKhoLamViec.ValueMember = "MaKho";

                    if (SessionContext.MaKhoHienTai > 0)
                    {
                        cboKhoLamViec.SelectedValue = SessionContext.MaKhoHienTai;
                    }
                }
                else
                {
                    MessageBox.Show("Không tìm thấy kho làm việc nào được phân quyền cho tài khoản này!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi nạp danh sách kho làm việc: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnXacNhan_Click(object sender, EventArgs e)
        {
            if (cboKhoLamViec.SelectedValue == null)
            {
                MessageBox.Show("Vui lòng chọn một kho làm việc!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            int maKhoMoi = Convert.ToInt32(cboKhoLamViec.SelectedValue);
            string tenKhoMoi = cboKhoLamViec.Text;

            SessionContext.MaKhoHienTai = maKhoMoi;
            SessionContext.TenKhoHienTai = tenKhoMoi;

            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void btnHuy_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }
    }
}
