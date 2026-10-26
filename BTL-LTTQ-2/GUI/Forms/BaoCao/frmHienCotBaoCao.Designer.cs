namespace BTL_LTTQ_2.GUI.Forms.BaoCao
{
    partial class frmHienCotBaoCao
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            this.lblHienThiCot = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // lblHienThiCot
            // 
            this.lblHienThiCot.AutoSize = true;
            this.lblHienThiCot.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblHienThiCot.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(41)))), ((int)(((byte)(59)))));
            this.lblHienThiCot.Location = new System.Drawing.Point(20, 16);
            this.lblHienThiCot.Name = "lblHienThiCot";
            this.lblHienThiCot.Size = new System.Drawing.Size(86, 19);
            this.lblHienThiCot.TabIndex = 0;
            this.lblHienThiCot.Text = "Hiển thị cột";
            // 
            // frmHienCotBaoCao
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(340, 220);
            this.Controls.Add(this.lblHienThiCot);
            this.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.KeyPreview = true;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmHienCotBaoCao";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        #endregion

        private System.Windows.Forms.Label lblHienThiCot;
    }
}
