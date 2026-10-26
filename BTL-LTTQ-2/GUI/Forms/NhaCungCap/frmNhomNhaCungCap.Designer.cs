namespace BTL_LTTQ_2.GUI.Forms.NhaCungCap
{
    partial class frmNhomNhaCungCap
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
            this.splitterThongTin = new System.Windows.Forms.Splitter();
            this.btnHuy = new System.Windows.Forms.Button();
            this.btnSua = new System.Windows.Forms.Button();
            this.lblTenNhom = new System.Windows.Forms.Label();
            this.splitterTop = new System.Windows.Forms.Splitter();
            this.grpThongTin = new System.Windows.Forms.GroupBox();
            this.btnThem = new System.Windows.Forms.Button();
            this.txtTenNhom = new System.Windows.Forms.TextBox();
            this.pnlLeft = new System.Windows.Forms.Panel();
            this.btnXuatFile = new System.Windows.Forms.Button();
            this.lblTieuDe = new System.Windows.Forms.Label();
            this.btnNhapFile = new System.Windows.Forms.Button();
            this.txtTimKiem = new System.Windows.Forms.TextBox();
            this.grpTimKiem = new System.Windows.Forms.GroupBox();
            this.lblTimKiem = new System.Windows.Forms.Label();
            this.pnlHeader = new System.Windows.Forms.Panel();
            this.lblTongSoNhom = new System.Windows.Forms.Label();
            this.dgvNhomHang = new System.Windows.Forms.DataGridView();
            this.pnlFooter = new System.Windows.Forms.Panel();
            this.grpThongTin.SuspendLayout();
            this.pnlLeft.SuspendLayout();
            this.grpTimKiem.SuspendLayout();
            this.pnlHeader.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvNhomHang)).BeginInit();
            this.pnlFooter.SuspendLayout();
            this.SuspendLayout();
            // 
            // splitterThongTin
            // 
            this.splitterThongTin.BackColor = System.Drawing.SystemColors.Control;
            this.splitterThongTin.Dock = System.Windows.Forms.DockStyle.Top;
            this.splitterThongTin.Location = new System.Drawing.Point(3, 25);
            this.splitterThongTin.Name = "splitterThongTin";
            this.splitterThongTin.Size = new System.Drawing.Size(290, 1);
            this.splitterThongTin.TabIndex = 6;
            this.splitterThongTin.TabStop = false;
            // 
            // btnHuy
            // 
            this.btnHuy.Location = new System.Drawing.Point(36, 125);
            this.btnHuy.Name = "btnHuy";
            this.btnHuy.Size = new System.Drawing.Size(75, 29);
            this.btnHuy.TabIndex = 5;
            this.btnHuy.Text = "Hủy";
            this.btnHuy.UseVisualStyleBackColor = true;
            this.btnHuy.Visible = false;
            // 
            // btnSua
            // 
            this.btnSua.Location = new System.Drawing.Point(117, 125);
            this.btnSua.Name = "btnSua";
            this.btnSua.Size = new System.Drawing.Size(75, 29);
            this.btnSua.TabIndex = 4;
            this.btnSua.Text = "Sửa";
            this.btnSua.UseVisualStyleBackColor = true;
            // 
            // lblTenNhom
            // 
            this.lblTenNhom.AutoSize = true;
            this.lblTenNhom.Location = new System.Drawing.Point(18, 46);
            this.lblTenNhom.Name = "lblTenNhom";
            this.lblTenNhom.Size = new System.Drawing.Size(78, 21);
            this.lblTenNhom.TabIndex = 3;
            this.lblTenNhom.Text = "Tên nhóm";
            // 
            // splitterTop
            // 
            this.splitterTop.BackColor = System.Drawing.SystemColors.ActiveBorder;
            this.splitterTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.splitterTop.Location = new System.Drawing.Point(0, 0);
            this.splitterTop.Name = "splitterTop";
            this.splitterTop.Size = new System.Drawing.Size(326, 1);
            this.splitterTop.TabIndex = 5;
            this.splitterTop.TabStop = false;
            // 
            // grpThongTin
            // 
            this.grpThongTin.Controls.Add(this.splitterThongTin);
            this.grpThongTin.Controls.Add(this.btnHuy);
            this.grpThongTin.Controls.Add(this.btnSua);
            this.grpThongTin.Controls.Add(this.btnThem);
            this.grpThongTin.Controls.Add(this.lblTenNhom);
            this.grpThongTin.Controls.Add(this.txtTenNhom);
            this.grpThongTin.Location = new System.Drawing.Point(12, 6);
            this.grpThongTin.Name = "grpThongTin";
            this.grpThongTin.Size = new System.Drawing.Size(296, 433);
            this.grpThongTin.TabIndex = 4;
            this.grpThongTin.TabStop = false;
            // 
            // btnThem
            // 
            this.btnThem.Location = new System.Drawing.Point(198, 125);
            this.btnThem.Name = "btnThem";
            this.btnThem.Size = new System.Drawing.Size(75, 29);
            this.btnThem.TabIndex = 4;
            this.btnThem.Text = "Thêm";
            this.btnThem.UseVisualStyleBackColor = true;
            // 
            // txtTenNhom
            // 
            this.txtTenNhom.Location = new System.Drawing.Point(21, 74);
            this.txtTenNhom.Name = "txtTenNhom";
            this.txtTenNhom.Size = new System.Drawing.Size(251, 29);
            this.txtTenNhom.TabIndex = 2;
            // 
            // pnlLeft
            // 
            this.pnlLeft.Controls.Add(this.splitterTop);
            this.pnlLeft.Controls.Add(this.grpThongTin);
            this.pnlLeft.Dock = System.Windows.Forms.DockStyle.Left;
            this.pnlLeft.Location = new System.Drawing.Point(0, 78);
            this.pnlLeft.Name = "pnlLeft";
            this.pnlLeft.Size = new System.Drawing.Size(326, 439);
            this.pnlLeft.TabIndex = 10;
            // 
            // btnXuatFile
            // 
            this.btnXuatFile.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnXuatFile.Location = new System.Drawing.Point(953, 22);
            this.btnXuatFile.Name = "btnXuatFile";
            this.btnXuatFile.Size = new System.Drawing.Size(110, 29);
            this.btnXuatFile.TabIndex = 6;
            this.btnXuatFile.Text = "Xuất file";
            this.btnXuatFile.UseVisualStyleBackColor = true;
            // 
            // lblTieuDe
            // 
            this.lblTieuDe.AutoSize = true;
            this.lblTieuDe.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.lblTieuDe.Location = new System.Drawing.Point(13, 22);
            this.lblTieuDe.Name = "lblTieuDe";
            this.lblTieuDe.Size = new System.Drawing.Size(202, 28);
            this.lblTieuDe.TabIndex = 4;
            this.lblTieuDe.Text = "Nhóm nhà cung cấp";
            // 
            // btnNhapFile
            // 
            this.btnNhapFile.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnNhapFile.Location = new System.Drawing.Point(827, 22);
            this.btnNhapFile.Name = "btnNhapFile";
            this.btnNhapFile.Size = new System.Drawing.Size(110, 29);
            this.btnNhapFile.TabIndex = 7;
            this.btnNhapFile.Text = "Nhập file";
            this.btnNhapFile.UseVisualStyleBackColor = true;
            // 
            // txtTimKiem
            // 
            this.txtTimKiem.Location = new System.Drawing.Point(413, 22);
            this.txtTimKiem.Name = "txtTimKiem";
            this.txtTimKiem.Size = new System.Drawing.Size(351, 29);
            this.txtTimKiem.TabIndex = 2;
            // 
            // grpTimKiem
            // 
            this.grpTimKiem.Controls.Add(this.btnXuatFile);
            this.grpTimKiem.Controls.Add(this.lblTieuDe);
            this.grpTimKiem.Controls.Add(this.btnNhapFile);
            this.grpTimKiem.Controls.Add(this.lblTimKiem);
            this.grpTimKiem.Controls.Add(this.txtTimKiem);
            this.grpTimKiem.Location = new System.Drawing.Point(0, 3);
            this.grpTimKiem.Name = "grpTimKiem";
            this.grpTimKiem.Size = new System.Drawing.Size(1082, 60);
            this.grpTimKiem.TabIndex = 4;
            this.grpTimKiem.TabStop = false;
            // 
            // lblTimKiem
            // 
            this.lblTimKiem.AutoSize = true;
            this.lblTimKiem.Location = new System.Drawing.Point(324, 25);
            this.lblTimKiem.Name = "lblTimKiem";
            this.lblTimKiem.Size = new System.Drawing.Size(74, 21);
            this.lblTimKiem.TabIndex = 3;
            this.lblTimKiem.Text = "Tìm kiếm";
            // 
            // pnlHeader
            // 
            this.pnlHeader.Controls.Add(this.grpTimKiem);
            this.pnlHeader.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlHeader.Location = new System.Drawing.Point(0, 0);
            this.pnlHeader.Name = "pnlHeader";
            this.pnlHeader.Size = new System.Drawing.Size(1082, 78);
            this.pnlHeader.TabIndex = 8;
            // 
            // lblTongSoNhom
            // 
            this.lblTongSoNhom.AutoSize = true;
            this.lblTongSoNhom.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.lblTongSoNhom.Location = new System.Drawing.Point(330, 9);
            this.lblTongSoNhom.Name = "lblTongSoNhom";
            this.lblTongSoNhom.Size = new System.Drawing.Size(211, 20);
            this.lblTongSoNhom.TabIndex = 0;
            this.lblTongSoNhom.Text = "Tổng số: 0 nhóm nhà cung cấp";
            // 
            // dgvNhomHang
            // 
            this.dgvNhomHang.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvNhomHang.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvNhomHang.Location = new System.Drawing.Point(0, 0);
            this.dgvNhomHang.Name = "dgvNhomHang";
            this.dgvNhomHang.RowHeadersWidth = 51;
            this.dgvNhomHang.RowTemplate.Height = 24;
            this.dgvNhomHang.Size = new System.Drawing.Size(1082, 517);
            this.dgvNhomHang.TabIndex = 7;
            // 
            // pnlFooter
            // 
            this.pnlFooter.Controls.Add(this.lblTongSoNhom);
            this.pnlFooter.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnlFooter.Location = new System.Drawing.Point(0, 517);
            this.pnlFooter.Name = "pnlFooter";
            this.pnlFooter.Size = new System.Drawing.Size(1082, 36);
            this.pnlFooter.TabIndex = 9;
            // 
            // frmNhomNhaCungCap
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 21F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1082, 553);
            this.Controls.Add(this.pnlLeft);
            this.Controls.Add(this.pnlHeader);
            this.Controls.Add(this.dgvNhomHang);
            this.Controls.Add(this.pnlFooter);
            this.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmNhomNhaCungCap";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Nhóm nhà cung cấp";
            this.grpThongTin.ResumeLayout(false);
            this.grpThongTin.PerformLayout();
            this.pnlLeft.ResumeLayout(false);
            this.grpTimKiem.ResumeLayout(false);
            this.grpTimKiem.PerformLayout();
            this.pnlHeader.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvNhomHang)).EndInit();
            this.pnlFooter.ResumeLayout(false);
            this.pnlFooter.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Splitter splitterThongTin;
        private System.Windows.Forms.Button btnHuy;
        private System.Windows.Forms.Button btnSua;
        private System.Windows.Forms.Label lblTenNhom;
        private System.Windows.Forms.Splitter splitterTop;
        private System.Windows.Forms.GroupBox grpThongTin;
        private System.Windows.Forms.Button btnThem;
        private System.Windows.Forms.TextBox txtTenNhom;
        private System.Windows.Forms.Panel pnlLeft;
        private System.Windows.Forms.Button btnXuatFile;
        private System.Windows.Forms.Label lblTieuDe;
        private System.Windows.Forms.Button btnNhapFile;
        private System.Windows.Forms.TextBox txtTimKiem;
        private System.Windows.Forms.GroupBox grpTimKiem;
        private System.Windows.Forms.Label lblTimKiem;
        private System.Windows.Forms.Panel pnlHeader;
        private System.Windows.Forms.Label lblTongSoNhom;
        private System.Windows.Forms.DataGridView dgvNhomHang;
        private System.Windows.Forms.Panel pnlFooter;
    }
}
