using System;
using System.Windows.Forms;

namespace BTL_LTTQ_2.GUI.Forms.BaoCao
{
    public partial class frmHienCotBaoCao : Form
    {
        private DataGridView targetDgv;

        public frmHienCotBaoCao()
        {
            InitializeComponent();
            CaiDatSuKienPhim();
        }

        public frmHienCotBaoCao(DataGridView dgv) : this()
        {
            this.targetDgv = dgv;
            KhoiTaoDanhSachCot();
        }

        private void CaiDatSuKienPhim()
        {
            this.KeyPreview = true;
            this.KeyDown += (s, e) =>
            {
                if (e.KeyCode == Keys.Escape)
                {
                    this.Close();
                }
            };
        }

        private void KhoiTaoDanhSachCot()
        {
            if (targetDgv == null) return;

            flpCot.SuspendLayout();
            flpCot.Controls.Clear();

            foreach (DataGridViewColumn col in targetDgv.Columns)
            {
                // Bỏ qua cột không có HeaderText hoặc cột khóa chính ẩn
                if (string.IsNullOrWhiteSpace(col.HeaderText) || col.Name.StartsWith("colMa") && col.Name.EndsWith("Id"))
                    continue;

                CheckBox chk = new CheckBox
                {
                    Text = col.HeaderText,
                    Tag = col.Name,
                    Checked = col.Visible,
                    AutoSize = true,
                    Margin = new Padding(3, 4, 3, 4),
                    Font = new System.Drawing.Font("Segoe UI", 9.5F)
                };

                string colName = col.Name;
                chk.CheckedChanged += (s, e) =>
                {
                    if (targetDgv.Columns.Contains(colName))
                    {
                        targetDgv.Columns[colName].Visible = chk.Checked;
                    }
                };

                flpCot.Controls.Add(chk);
            }

            flpCot.ResumeLayout();
        }

        private void btnChonTatCa_Click(object sender, EventArgs e)
        {
            bool coMucChuaChon = false;
            foreach (Control ctrl in flpCot.Controls)
            {
                if (ctrl is CheckBox chk && !chk.Checked)
                {
                    coMucChuaChon = true;
                    break;
                }
            }

            foreach (Control ctrl in flpCot.Controls)
            {
                if (ctrl is CheckBox chk)
                {
                    chk.Checked = coMucChuaChon;
                }
            }
        }

        private void btnDong_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void frmHienCotBaoCao_Deactivate(object sender, EventArgs e)
        {
            // Tự đóng khi người dùng click ra ngoài
            this.Close();
        }
    }
}
