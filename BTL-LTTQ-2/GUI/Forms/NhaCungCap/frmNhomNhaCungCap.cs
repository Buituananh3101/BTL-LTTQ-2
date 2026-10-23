using System;
using System.Data.SqlClient;
using System.Windows.Forms;
using BTL_LTTQ_2.DAL;

namespace BTL_LTTQ_2.GUI.Forms.NhaCungCap
{
    public partial class frmNhomNhaCungCap : Form
    {
        public int MaNhomVuaTao { get; private set; } = 0;
        public string TenNhomVuaTao { get; private set; } = string.Empty;

        public frmNhomNhaCungCap()
        {
            InitializeComponent();
        }

        private void btnLuu_Click(object sender, EventArgs e)
        {
            string tenNhom = txtTenNhom.Text.Trim();
            if (string.IsNullOrWhiteSpace(tenNhom))
            {
                MessageBox.Show("Vui lòng nhập tên nhóm nhà cung cấp!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtTenNhom.Focus();
                return;
            }

            try
            {
                // Kiểm tra xem bảng NhomNhaCungCap đã có trong DB chưa, nếu chưa thì tạo nhanh
                string sqlCheckTable = @"
                    IF NOT EXISTS (SELECT 1 FROM sys.tables WHERE name = N'NhomNhaCungCap')
                    BEGIN
                        CREATE TABLE NhomNhaCungCap (
                            MaNhomNcc INT IDENTITY(1,1) PRIMARY KEY,
                            TenNhom NVARCHAR(150) NOT NULL,
                            GhiChu NVARCHAR(255) NULL
                        );
                    END";
                DataProcessing.ChangeData(sqlCheckTable);

                // Kiểm tra trùng tên nhóm
                string sqlCheck = "SELECT COUNT(1) FROM NhomNhaCungCap WHERE LOWER(TenNhom) = LOWER(@TenNhom)";
                object valCheck = DataProcessing.ReadValue(sqlCheck, new SqlParameter[] { new SqlParameter("@TenNhom", tenNhom) });
                if (valCheck != null && int.TryParse(valCheck.ToString(), out int tonTai) && tonTai > 0)
                {
                    MessageBox.Show("Tên nhóm nhà cung cấp này đã tồn tại trong hệ thống!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtTenNhom.Focus();
                    return;
                }

                // Lưu mới và lấy ID vừa sinh
                string sqlInsert = @"
                    INSERT INTO NhomNhaCungCap (TenNhom, GhiChu)
                    VALUES (@TenNhom, @GhiChu);
                    SELECT SCOPE_IDENTITY();";

                SqlParameter[] pars = new SqlParameter[]
                {
                    new SqlParameter("@TenNhom", tenNhom),
                    new SqlParameter("@GhiChu", (object)txtGhiChu.Text.Trim() ?? DBNull.Value)
                };

                object newId = DataProcessing.ReadValue(sqlInsert, pars);
                if (newId != null && int.TryParse(newId.ToString(), out int id))
                {
                    MaNhomVuaTao = id;
                    TenNhomVuaTao = tenNhom;
                    MessageBox.Show("Tạo mới nhóm nhà cung cấp thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.DialogResult = DialogResult.OK;
                    this.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi lưu nhóm nhà cung cấp: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnBoQua_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }
    }
}
