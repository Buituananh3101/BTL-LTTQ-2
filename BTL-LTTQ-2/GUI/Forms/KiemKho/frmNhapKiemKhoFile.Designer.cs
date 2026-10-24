namespace BTL_LTTQ_2.GUI.Forms.KiemKho
{
    partial class frmNhapKiemKhoFile
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
            this.pnlTop = new System.Windows.Forms.Panel();
            this.lblTieuDe = new System.Windows.Forms.Label();
            this.grpFileMau = new System.Windows.Forms.GroupBox();
            this.lblHuongDanMau = new System.Windows.Forms.Label();
            this.btnTaiFileMauExcel = new System.Windows.Forms.Button();
            this.btnTaiFileMauCsv = new System.Windows.Forms.Button();
            this.grpChonFile = new System.Windows.Forms.GroupBox();
            this.txtDuongDanFile = new System.Windows.Forms.TextBox();
            this.btnChonFile = new System.Windows.Forms.Button();
            this.btnDocDuLieu = new System.Windows.Forms.Button();
            this.grpXemTruoc = new System.Windows.Forms.GroupBox();
            this.dgvXemTruoc = new System.Windows.Forms.DataGridView();
            this.colStt = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colMaHang = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colTenHang = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colDonViTinh = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colSoLuongThucTe = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colGhiChu = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colTrangThai = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.pnlBottom = new System.Windows.Forms.Panel();
            this.lblThongKe = new System.Windows.Forms.Label();
            this.btnDongY = new System.Windows.Forms.Button();
            this.btnDong = new System.Windows.Forms.Button();
            this.pnlTop.SuspendLayout();
            this.grpFileMau.SuspendLayout();
            this.grpChonFile.SuspendLayout();
            this.grpXemTruoc.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvXemTruoc)).BeginInit();
            this.pnlBottom.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlTop
            // 
            this.pnlTop.Controls.Add(this.lblTieuDe);
            this.pnlTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlTop.Location = new System.Drawing.Point(0, 0);
            this.pnlTop.Name = "pnlTop";
            this.pnlTop.Size = new System.Drawing.Size(884, 50);
            this.pnlTop.TabIndex = 0;
            // 
            // lblTieuDe
            // 
            this.lblTieuDe.AutoSize = true;
            this.lblTieuDe.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.lblTieuDe.Location = new System.Drawing.Point(14, 12);
            this.lblTieuDe.Name = "lblTieuDe";
            this.lblTieuDe.Size = new System.Drawing.Size(325, 30);
            this.lblTieuDe.TabIndex = 0;
            this.lblTieuDe.Text = "Nhập dữ liệu kiểm kho từ file";
            // 
            // grpFileMau
            // 
            this.grpFileMau.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.grpFileMau.Controls.Add(this.lblHuongDanMau);
            this.grpFileMau.Controls.Add(this.btnTaiFileMauExcel);
            this.grpFileMau.Controls.Add(this.btnTaiFileMauCsv);
            this.grpFileMau.Location = new System.Drawing.Point(14, 56);
            this.grpFileMau.Name = "grpFileMau";
            this.grpFileMau.Size = new System.Drawing.Size(856, 80);
            this.grpFileMau.TabIndex = 1;
            this.grpFileMau.TabStop = false;
            this.grpFileMau.Text = "1. File dữ liệu mẫu chuẩn";
            // 
            // lblHuongDanMau
            // 
            this.lblHuongDanMau.AutoSize = true;
            this.lblHuongDanMau.Location = new System.Drawing.Point(15, 30);
            this.lblHuongDanMau.Name = "lblHuongDanMau";
            this.lblHuongDanMau.Size = new System.Drawing.Size(460, 21);
            this.lblHuongDanMau.TabIndex = 0;
            this.lblHuongDanMau.Text = "Tải file mẫu để điền số lượng kiểm thực tế đúng theo quy chuẩn:";
            // 
            // btnTaiFileMauExcel
            // 
            this.btnTaiFileMauExcel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnTaiFileMauExcel.Location = new System.Drawing.Point(520, 25);
            this.btnTaiFileMauExcel.Name = "btnTaiFileMauExcel";
            this.btnTaiFileMauExcel.Size = new System.Drawing.Size(160, 32);
            this.btnTaiFileMauExcel.TabIndex = 1;
            this.btnTaiFileMauExcel.Text = "Tải file mẫu Excel";
            this.btnTaiFileMauExcel.UseVisualStyleBackColor = true;
            this.btnTaiFileMauExcel.Click += new System.EventHandler(this.btnTaiFileMauExcel_Click);
            // 
            // btnTaiFileMauCsv
            // 
            this.btnTaiFileMauCsv.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnTaiFileMauCsv.Location = new System.Drawing.Point(690, 25);
            this.btnTaiFileMauCsv.Name = "btnTaiFileMauCsv";
            this.btnTaiFileMauCsv.Size = new System.Drawing.Size(150, 32);
            this.btnTaiFileMauCsv.TabIndex = 2;
            this.btnTaiFileMauCsv.Text = "Tải file mẫu CSV";
            this.btnTaiFileMauCsv.UseVisualStyleBackColor = true;
            this.btnTaiFileMauCsv.Click += new System.EventHandler(this.btnTaiFileMauCsv_Click);
            // 
            // grpChonFile
            // 
            this.grpChonFile.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.grpChonFile.Controls.Add(this.txtDuongDanFile);
            this.grpChonFile.Controls.Add(this.btnChonFile);
            this.grpChonFile.Controls.Add(this.btnDocDuLieu);
            this.grpChonFile.Location = new System.Drawing.Point(14, 142);
            this.grpChonFile.Name = "grpChonFile";
            this.grpChonFile.Size = new System.Drawing.Size(856, 75);
            this.grpChonFile.TabIndex = 2;
            this.grpChonFile.TabStop = false;
            this.grpChonFile.Text = "2. Chọn file kiểm kho cần nạp";
            // 
            // txtDuongDanFile
            // 
            this.txtDuongDanFile.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtDuongDanFile.BackColor = System.Drawing.Color.White;
            this.txtDuongDanFile.Location = new System.Drawing.Point(15, 29);
            this.txtDuongDanFile.Name = "txtDuongDanFile";
            this.txtDuongDanFile.ReadOnly = true;
            this.txtDuongDanFile.Size = new System.Drawing.Size(560, 29);
            this.txtDuongDanFile.TabIndex = 0;
            // 
            // btnChonFile
            // 
            this.btnChonFile.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnChonFile.Location = new System.Drawing.Point(590, 27);
            this.btnChonFile.Name = "btnChonFile";
            this.btnChonFile.Size = new System.Drawing.Size(110, 32);
            this.btnChonFile.TabIndex = 1;
            this.btnChonFile.Text = "Chọn file...";
            this.btnChonFile.UseVisualStyleBackColor = true;
            this.btnChonFile.Click += new System.EventHandler(this.btnChonFile_Click);
            // 
            // btnDocDuLieu
            // 
            this.btnDocDuLieu.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnDocDuLieu.Location = new System.Drawing.Point(710, 27);
            this.btnDocDuLieu.Name = "btnDocDuLieu";
            this.btnDocDuLieu.Size = new System.Drawing.Size(130, 32);
            this.btnDocDuLieu.TabIndex = 2;
            this.btnDocDuLieu.Text = "Đọc file";
            this.btnDocDuLieu.UseVisualStyleBackColor = true;
            this.btnDocDuLieu.Click += new System.EventHandler(this.btnDocDuLieu_Click);
            // 
            // grpXemTruoc
            // 
            this.grpXemTruoc.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.grpXemTruoc.Controls.Add(this.dgvXemTruoc);
            this.grpXemTruoc.Location = new System.Drawing.Point(14, 225);
            this.grpXemTruoc.Name = "grpXemTruoc";
            this.grpXemTruoc.Size = new System.Drawing.Size(856, 320);
            this.grpXemTruoc.TabIndex = 3;
            this.grpXemTruoc.TabStop = false;
            this.grpXemTruoc.Text = "3. Xem trước dữ liệu kiểm kho";
            // 
            // dgvXemTruoc
            // 
            this.dgvXemTruoc.AllowUserToAddRows = false;
            this.dgvXemTruoc.AllowUserToDeleteRows = false;
            this.dgvXemTruoc.BackgroundColor = System.Drawing.Color.White;
            this.dgvXemTruoc.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvXemTruoc.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colStt,
            this.colMaHang,
            this.colTenHang,
            this.colDonViTinh,
            this.colSoLuongThucTe,
            this.colGhiChu,
            this.colTrangThai});
            this.dgvXemTruoc.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvXemTruoc.Location = new System.Drawing.Point(3, 25);
            this.dgvXemTruoc.Name = "dgvXemTruoc";
            this.dgvXemTruoc.ReadOnly = true;
            this.dgvXemTruoc.RowHeadersVisible = false;
            this.dgvXemTruoc.RowHeadersWidth = 51;
            this.dgvXemTruoc.RowTemplate.Height = 24;
            this.dgvXemTruoc.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvXemTruoc.Size = new System.Drawing.Size(850, 292);
            this.dgvXemTruoc.TabIndex = 0;
            // 
            // colStt
            // 
            this.colStt.DataPropertyName = "STT";
            this.colStt.HeaderText = "STT";
            this.colStt.MinimumWidth = 6;
            this.colStt.Name = "colStt";
            this.colStt.ReadOnly = true;
            this.colStt.Width = 50;
            // 
            // colMaHang
            // 
            this.colMaHang.DataPropertyName = "MaHangNoiBo";
            this.colMaHang.HeaderText = "Mã hàng";
            this.colMaHang.MinimumWidth = 6;
            this.colMaHang.Name = "colMaHang";
            this.colMaHang.ReadOnly = true;
            this.colMaHang.Width = 110;
            // 
            // colTenHang
            // 
            this.colTenHang.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.colTenHang.DataPropertyName = "TenHang";
            this.colTenHang.HeaderText = "Tên hàng";
            this.colTenHang.MinimumWidth = 6;
            this.colTenHang.Name = "colTenHang";
            this.colTenHang.ReadOnly = true;
            // 
            // colDonViTinh
            // 
            this.colDonViTinh.DataPropertyName = "TenDVT";
            this.colDonViTinh.HeaderText = "ĐVT";
            this.colDonViTinh.MinimumWidth = 6;
            this.colDonViTinh.Name = "colDonViTinh";
            this.colDonViTinh.ReadOnly = true;
            this.colDonViTinh.Width = 80;
            // 
            // colSoLuongThucTe
            // 
            this.colSoLuongThucTe.DataPropertyName = "SoLuongThucTe";
            this.colSoLuongThucTe.HeaderText = "SL thực tế";
            this.colSoLuongThucTe.MinimumWidth = 6;
            this.colSoLuongThucTe.Name = "colSoLuongThucTe";
            this.colSoLuongThucTe.ReadOnly = true;
            this.colSoLuongThucTe.Width = 110;
            // 
            // colGhiChu
            // 
            this.colGhiChu.DataPropertyName = "GhiChu";
            this.colGhiChu.HeaderText = "Ghi chú";
            this.colGhiChu.MinimumWidth = 6;
            this.colGhiChu.Name = "colGhiChu";
            this.colGhiChu.ReadOnly = true;
            this.colGhiChu.Width = 120;
            // 
            // colTrangThai
            // 
            this.colTrangThai.DataPropertyName = "TrangThaiDong";
            this.colTrangThai.HeaderText = "Trạng thái";
            this.colTrangThai.MinimumWidth = 6;
            this.colTrangThai.Name = "colTrangThai";
            this.colTrangThai.ReadOnly = true;
            this.colTrangThai.Width = 150;
            // 
            // pnlBottom
            // 
            this.pnlBottom.Controls.Add(this.lblThongKe);
            this.pnlBottom.Controls.Add(this.btnDongY);
            this.pnlBottom.Controls.Add(this.btnDong);
            this.pnlBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnlBottom.Location = new System.Drawing.Point(0, 555);
            this.pnlBottom.Name = "pnlBottom";
            this.pnlBottom.Size = new System.Drawing.Size(884, 55);
            this.pnlBottom.TabIndex = 4;
            // 
            // lblThongKe
            // 
            this.lblThongKe.AutoSize = true;
            this.lblThongKe.Location = new System.Drawing.Point(14, 17);
            this.lblThongKe.Name = "lblThongKe";
            this.lblThongKe.Size = new System.Drawing.Size(262, 21);
            this.lblThongKe.TabIndex = 0;
            this.lblThongKe.Text = "Tổng: 0 dòng | Hợp lệ: 0 | Lỗi: 0";
            // 
            // btnDongY
            // 
            this.btnDongY.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnDongY.Enabled = false;
            this.btnDongY.Location = new System.Drawing.Point(620, 11);
            this.btnDongY.Name = "btnDongY";
            this.btnDongY.Size = new System.Drawing.Size(140, 34);
            this.btnDongY.TabIndex = 1;
            this.btnDongY.Text = "Đồng ý nhập";
            this.btnDongY.UseVisualStyleBackColor = true;
            this.btnDongY.Click += new System.EventHandler(this.btnDongY_Click);
            // 
            // btnDong
            // 
            this.btnDong.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnDong.Location = new System.Drawing.Point(770, 11);
            this.btnDong.Name = "btnDong";
            this.btnDong.Size = new System.Drawing.Size(100, 34);
            this.btnDong.TabIndex = 2;
            this.btnDong.Text = "Đóng";
            this.btnDong.UseVisualStyleBackColor = true;
            this.btnDong.Click += new System.EventHandler(this.btnDong_Click);
            // 
            // frmNhapKiemKhoFile
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 21F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;

            this.ClientSize = new System.Drawing.Size(884, 610);
            this.Controls.Add(this.grpXemTruoc);
            this.Controls.Add(this.grpChonFile);
            this.Controls.Add(this.grpFileMau);
            this.Controls.Add(this.pnlTop);
            this.Controls.Add(this.pnlBottom);
            this.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmNhapKiemKhoFile";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Nhập dữ liệu kiểm kho từ file";
            this.Load += new System.EventHandler(this.frmNhapKiemKhoFile_Load);
            this.pnlTop.ResumeLayout(false);
            this.pnlTop.PerformLayout();
            this.grpFileMau.ResumeLayout(false);
            this.grpFileMau.PerformLayout();
            this.grpChonFile.ResumeLayout(false);
            this.grpChonFile.PerformLayout();
            this.grpXemTruoc.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvXemTruoc)).EndInit();
            this.pnlBottom.ResumeLayout(false);
            this.pnlBottom.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pnlTop;
        private System.Windows.Forms.Label lblTieuDe;
        private System.Windows.Forms.GroupBox grpFileMau;
        private System.Windows.Forms.Label lblHuongDanMau;
        private System.Windows.Forms.Button btnTaiFileMauExcel;
        private System.Windows.Forms.Button btnTaiFileMauCsv;
        private System.Windows.Forms.GroupBox grpChonFile;
        private System.Windows.Forms.TextBox txtDuongDanFile;
        private System.Windows.Forms.Button btnChonFile;
        private System.Windows.Forms.Button btnDocDuLieu;
        private System.Windows.Forms.GroupBox grpXemTruoc;
        private System.Windows.Forms.DataGridView dgvXemTruoc;
        private System.Windows.Forms.Panel pnlBottom;
        private System.Windows.Forms.Label lblThongKe;
        private System.Windows.Forms.Button btnDongY;
        private System.Windows.Forms.Button btnDong;
        private System.Windows.Forms.DataGridViewTextBoxColumn colStt;
        private System.Windows.Forms.DataGridViewTextBoxColumn colMaHang;
        private System.Windows.Forms.DataGridViewTextBoxColumn colTenHang;
        private System.Windows.Forms.DataGridViewTextBoxColumn colDonViTinh;
        private System.Windows.Forms.DataGridViewTextBoxColumn colSoLuongThucTe;
        private System.Windows.Forms.DataGridViewTextBoxColumn colGhiChu;
        private System.Windows.Forms.DataGridViewTextBoxColumn colTrangThai;
    }
}
