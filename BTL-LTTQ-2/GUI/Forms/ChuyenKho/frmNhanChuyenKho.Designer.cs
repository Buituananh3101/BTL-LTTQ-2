namespace BTL_LTTQ_2.GUI.Forms.ChuyenKho
{
    partial class frmNhanChuyenKho
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            this.pnlTop = new System.Windows.Forms.Panel();
            this.lblThongTinPhieu = new System.Windows.Forms.Label();
            this.lblTieuDe = new System.Windows.Forms.Label();
            this.dgvHangNhan = new System.Windows.Forms.DataGridView();
            this.colStt = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colMaHang = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colTenHang = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colDonViTinh = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colSoLuongChuyen = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colSoLuongNhan = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colHaoHut = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.pnlBottom = new System.Windows.Forms.Panel();
            this.btnHuy = new System.Windows.Forms.Button();
            this.btnXacNhanNhan = new System.Windows.Forms.Button();
            this.lblTongHaoHut = new System.Windows.Forms.Label();
            this.lblTongThucNhan = new System.Windows.Forms.Label();
            this.lblTongChuyen = new System.Windows.Forms.Label();
            this.txtGhiChuNhan = new System.Windows.Forms.TextBox();
            this.lblGhiChuNhan = new System.Windows.Forms.Label();
            this.pnlTop.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvHangNhan)).BeginInit();
            this.pnlBottom.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlTop
            // 
            this.pnlTop.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(250)))), ((int)(((byte)(252)))));
            this.pnlTop.Controls.Add(this.lblThongTinPhieu);
            this.pnlTop.Controls.Add(this.lblTieuDe);
            this.pnlTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlTop.Location = new System.Drawing.Point(0, 0);
            this.pnlTop.Name = "pnlTop";
            this.pnlTop.Size = new System.Drawing.Size(864, 60);
            this.pnlTop.TabIndex = 0;
            // 
            // lblThongTinPhieu
            // 
            this.lblThongTinPhieu.AutoSize = true;
            this.lblThongTinPhieu.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            this.lblThongTinPhieu.Location = new System.Drawing.Point(16, 34);
            this.lblThongTinPhieu.Name = "lblThongTinPhieu";
            this.lblThongTinPhieu.Size = new System.Drawing.Size(376, 17);
            this.lblThongTinPhieu.TabIndex = 1;
            this.lblThongTinPhieu.Text = "Mã phiếu: CK000000 | Kho gửi: ... -> Kho nhận: ...";
            // 
            // lblTieuDe
            // 
            this.lblTieuDe.AutoSize = true;
            this.lblTieuDe.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.lblTieuDe.Location = new System.Drawing.Point(14, 9);
            this.lblTieuDe.Name = "lblTieuDe";
            this.lblTieuDe.Size = new System.Drawing.Size(256, 21);
            this.lblTieuDe.TabIndex = 0;
            this.lblTieuDe.Text = "Xác nhận nhận hàng chuyển kho";
            // 
            // dgvHangNhan
            // 
            this.dgvHangNhan.AllowUserToAddRows = false;
            this.dgvHangNhan.AllowUserToDeleteRows = false;
            this.dgvHangNhan.BackgroundColor = System.Drawing.Color.White;
            this.dgvHangNhan.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvHangNhan.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colStt,
            this.colMaHang,
            this.colTenHang,
            this.colDonViTinh,
            this.colSoLuongChuyen,
            this.colSoLuongNhan,
            this.colHaoHut});
            this.dgvHangNhan.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvHangNhan.Location = new System.Drawing.Point(0, 60);
            this.dgvHangNhan.Name = "dgvHangNhan";
            this.dgvHangNhan.RowHeadersVisible = false;
            this.dgvHangNhan.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvHangNhan.Size = new System.Drawing.Size(864, 311);
            this.dgvHangNhan.TabIndex = 1;
            this.dgvHangNhan.CellEndEdit += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvHangNhan_CellEndEdit);
            this.dgvHangNhan.CellFormatting += new System.Windows.Forms.DataGridViewCellFormattingEventHandler(this.dgvHangNhan_CellFormatting);
            // 
            // colStt
            // 
            this.colStt.DataPropertyName = "STT";
            this.colStt.HeaderText = "STT";
            this.colStt.Name = "colStt";
            this.colStt.ReadOnly = true;
            this.colStt.Width = 50;
            // 
            // colMaHang
            // 
            this.colMaHang.DataPropertyName = "MaHangNoiBo";
            this.colMaHang.HeaderText = "Mã hàng";
            this.colMaHang.Name = "colMaHang";
            this.colMaHang.ReadOnly = true;
            this.colMaHang.Width = 110;
            // 
            // colTenHang
            // 
            this.colTenHang.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.colTenHang.DataPropertyName = "TenHang";
            this.colTenHang.HeaderText = "Tên hàng hóa";
            this.colTenHang.Name = "colTenHang";
            this.colTenHang.ReadOnly = true;
            // 
            // colDonViTinh
            // 
            this.colDonViTinh.DataPropertyName = "TenDVT";
            this.colDonViTinh.HeaderText = "ĐVT";
            this.colDonViTinh.Name = "colDonViTinh";
            this.colDonViTinh.ReadOnly = true;
            this.colDonViTinh.Width = 80;
            // 
            // colSoLuongChuyen
            // 
            this.colSoLuongChuyen.DataPropertyName = "SoLuong";
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle1.Format = "N0";
            this.colSoLuongChuyen.DefaultCellStyle = dataGridViewCellStyle1;
            this.colSoLuongChuyen.HeaderText = "SL chuyển";
            this.colSoLuongChuyen.Name = "colSoLuongChuyen";
            this.colSoLuongChuyen.ReadOnly = true;
            this.colSoLuongChuyen.Width = 100;
            // 
            // colSoLuongNhan
            // 
            this.colSoLuongNhan.DataPropertyName = "SoLuongNhan";
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(204)))));
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold);
            dataGridViewCellStyle2.Format = "N0";
            this.colSoLuongNhan.DefaultCellStyle = dataGridViewCellStyle2;
            this.colSoLuongNhan.HeaderText = "SL thực nhận";
            this.colSoLuongNhan.Name = "colSoLuongNhan";
            this.colSoLuongNhan.Width = 110;
            // 
            // colHaoHut
            // 
            this.colHaoHut.DataPropertyName = "HaoHut";
            this.colHaoHut.HeaderText = "Hao hụt/Thiếu";
            this.colHaoHut.Name = "colHaoHut";
            this.colHaoHut.ReadOnly = true;
            this.colHaoHut.Width = 110;
            // 
            // pnlBottom
            // 
            this.pnlBottom.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(250)))), ((int)(((byte)(252)))));
            this.pnlBottom.Controls.Add(this.btnHuy);
            this.pnlBottom.Controls.Add(this.btnXacNhanNhan);
            this.pnlBottom.Controls.Add(this.lblTongHaoHut);
            this.pnlBottom.Controls.Add(this.lblTongThucNhan);
            this.pnlBottom.Controls.Add(this.lblTongChuyen);
            this.pnlBottom.Controls.Add(this.txtGhiChuNhan);
            this.pnlBottom.Controls.Add(this.lblGhiChuNhan);
            this.pnlBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnlBottom.Location = new System.Drawing.Point(0, 371);
            this.pnlBottom.Name = "pnlBottom";
            this.pnlBottom.Size = new System.Drawing.Size(864, 110);
            this.pnlBottom.TabIndex = 2;
            // 
            // btnHuy
            // 
            this.btnHuy.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnHuy.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnHuy.Location = new System.Drawing.Point(595, 68);
            this.btnHuy.Name = "btnHuy";
            this.btnHuy.Size = new System.Drawing.Size(95, 30);
            this.btnHuy.TabIndex = 5;
            this.btnHuy.Text = "Bỏ qua";
            this.btnHuy.UseVisualStyleBackColor = true;
            this.btnHuy.Click += new System.EventHandler(this.btnHuy_Click);
            // 
            // btnXacNhanNhan
            // 
            this.btnXacNhanNhan.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnXacNhanNhan.Location = new System.Drawing.Point(700, 68);
            this.btnXacNhanNhan.Name = "btnXacNhanNhan";
            this.btnXacNhanNhan.Size = new System.Drawing.Size(150, 30);
            this.btnXacNhanNhan.TabIndex = 6;
            this.btnXacNhanNhan.Text = "Xác nhận nhận hàng";
            this.btnXacNhanNhan.UseVisualStyleBackColor = true;
            this.btnXacNhanNhan.Click += new System.EventHandler(this.btnXacNhanNhan_Click);
            // 
            // lblTongHaoHut
            // 
            this.lblTongHaoHut.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblTongHaoHut.AutoSize = true;
            this.lblTongHaoHut.Location = new System.Drawing.Point(670, 38);
            this.lblTongHaoHut.Name = "lblTongHaoHut";
            this.lblTongHaoHut.Size = new System.Drawing.Size(107, 17);
            this.lblTongHaoHut.TabIndex = 4;
            this.lblTongHaoHut.Text = "Tổng hao hụt: 0";
            // 
            // lblTongThucNhan
            // 
            this.lblTongThucNhan.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblTongThucNhan.AutoSize = true;
            this.lblTongThucNhan.Location = new System.Drawing.Point(670, 14);
            this.lblTongThucNhan.Name = "lblTongThucNhan";
            this.lblTongThucNhan.Size = new System.Drawing.Size(124, 17);
            this.lblTongThucNhan.TabIndex = 3;
            this.lblTongThucNhan.Text = "Tổng thực nhận: 0";
            // 
            // lblTongChuyen
            // 
            this.lblTongChuyen.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblTongChuyen.AutoSize = true;
            this.lblTongChuyen.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            this.lblTongChuyen.Location = new System.Drawing.Point(500, 14);
            this.lblTongChuyen.Name = "lblTongChuyen";
            this.lblTongChuyen.Size = new System.Drawing.Size(102, 17);
            this.lblTongChuyen.TabIndex = 2;
            this.lblTongChuyen.Text = "Tổng chuyển: 0";
            // 
            // txtGhiChuNhan
            // 
            this.txtGhiChuNhan.Location = new System.Drawing.Point(18, 35);
            this.txtGhiChuNhan.Multiline = true;
            this.txtGhiChuNhan.Name = "txtGhiChuNhan";
            this.txtGhiChuNhan.Size = new System.Drawing.Size(460, 60);
            this.txtGhiChuNhan.TabIndex = 1;
            // 
            // lblGhiChuNhan
            // 
            this.lblGhiChuNhan.AutoSize = true;
            this.lblGhiChuNhan.Location = new System.Drawing.Point(15, 14);
            this.lblGhiChuNhan.Name = "lblGhiChuNhan";
            this.lblGhiChuNhan.Size = new System.Drawing.Size(262, 17);
            this.lblGhiChuNhan.TabIndex = 0;
            this.lblGhiChuNhan.Text = "Ghi chú nhận hàng (bắt buộc nếu có hao hụt):";
            // 
            // frmNhanChuyenKho
            // 
            this.AcceptButton = this.btnXacNhanNhan;
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnHuy;
            this.ClientSize = new System.Drawing.Size(864, 481);
            this.Controls.Add(this.dgvHangNhan);
            this.Controls.Add(this.pnlBottom);
            this.Controls.Add(this.pnlTop);
            this.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmNhanChuyenKho";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Xác nhận nhận hàng chuyển kho";
            this.Load += new System.EventHandler(this.frmNhanChuyenKho_Load);
            this.pnlTop.ResumeLayout(false);
            this.pnlTop.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvHangNhan)).EndInit();
            this.pnlBottom.ResumeLayout(false);
            this.pnlBottom.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pnlTop;
        private System.Windows.Forms.Label lblThongTinPhieu;
        private System.Windows.Forms.Label lblTieuDe;
        private System.Windows.Forms.DataGridView dgvHangNhan;
        private System.Windows.Forms.Panel pnlBottom;
        private System.Windows.Forms.TextBox txtGhiChuNhan;
        private System.Windows.Forms.Label lblGhiChuNhan;
        private System.Windows.Forms.Label lblTongHaoHut;
        private System.Windows.Forms.Label lblTongThucNhan;
        private System.Windows.Forms.Label lblTongChuyen;
        private System.Windows.Forms.Button btnHuy;
        private System.Windows.Forms.Button btnXacNhanNhan;
        private System.Windows.Forms.DataGridViewTextBoxColumn colStt;
        private System.Windows.Forms.DataGridViewTextBoxColumn colMaHang;
        private System.Windows.Forms.DataGridViewTextBoxColumn colTenHang;
        private System.Windows.Forms.DataGridViewTextBoxColumn colDonViTinh;
        private System.Windows.Forms.DataGridViewTextBoxColumn colSoLuongChuyen;
        private System.Windows.Forms.DataGridViewTextBoxColumn colSoLuongNhan;
        private System.Windows.Forms.DataGridViewTextBoxColumn colHaoHut;
    }
}
