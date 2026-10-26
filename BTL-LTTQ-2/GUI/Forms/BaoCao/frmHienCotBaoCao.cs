using System;
using System.Collections.Generic;
using System.Drawing;
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
            // Đóng popup khi form báo cáo chứa lưới bị đóng (tránh thao tác trên lưới đã giải phóng)
            if (dgv != null)
            {
                dgv.Disposed += (s, e) =>
                {
                    if (!this.IsDisposed) this.Close();
                };
            }
            KhoiTaoDanhSachCot();
        }

        private void CaiDatSuKienPhim()
        {
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

            this.SuspendLayout();

            // Lấy danh sách các cột hợp lệ cần tùy chọn hiển thị
            List<DataGridViewColumn> validCols = new List<DataGridViewColumn>();
            foreach (DataGridViewColumn col in targetDgv.Columns)
            {
                if (string.IsNullOrWhiteSpace(col.HeaderText) ||
                    col.Name == "colMaHang" ||
                    string.Equals(col.HeaderText, "colMaHang", StringComparison.OrdinalIgnoreCase) || 
                    (col.Name.StartsWith("colMa") && col.Name.EndsWith("Id")) ||
                    col is DataGridViewCheckBoxColumn)
                {
                    continue;
                }
                validCols.Add(col);
            }

            int count = validCols.Count;
            int numRows = (count + 1) / 2; // Số dòng nếu chia 2 cột

            for (int i = 0; i < count; i++)
            {
                DataGridViewColumn col = validCols[i];
                int colIndex = i < numRows ? 0 : 1; // Nửa đầu xếp cột 1, nửa sau xếp cột 2
                int rowIndex = i < numRows ? i : (i - numRows);

                int posX = colIndex == 0 ? 24 : 185;
                int posY = 48 + rowIndex * 28;

                CheckBox chk = new CheckBox
                {
                    Text = col.HeaderText,
                    Tag = col.Name,
                    Checked = col.Visible,
                    AutoSize = true,
                    Location = new Point(posX, posY),
                    Cursor = Cursors.Hand,
                    Font = new Font("Segoe UI", 9.5F, FontStyle.Regular, GraphicsUnit.Point, ((byte)(0))),
                    ForeColor = Color.FromArgb(51, 65, 85),
                    UseVisualStyleBackColor = true
                };

                string colName = col.Name;
                chk.CheckedChanged += (s, e) =>
                {
                    if (targetDgv.Columns.Contains(colName))
                    {
                        targetDgv.Columns[colName].Visible = chk.Checked;
                    }
                };

                this.Controls.Add(chk);
            }

            // Tự động tính chiều cao form vừa vặn theo số hàng
            int formHeight = Math.Max(120, 48 + numRows * 28 + 20);
            this.ClientSize = new Size(340, formHeight);

            this.ResumeLayout(false);
            this.PerformLayout();
        }
    }
}
