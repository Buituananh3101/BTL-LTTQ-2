namespace BTL_LTTQ_2.GUI.Forms.NhapHang
{
    partial class frmChiTietPhieuNhap
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
            this.lblKhoChiNhanh = new System.Windows.Forms.Label();
            this.lblTrangThai = new System.Windows.Forms.Label();
            this.lblMaPhieuHienThi = new System.Windows.Forms.Label();
            this.pnlThongTin = new System.Windows.Forms.Panel();
            this.lblSoHoaDonVal = new System.Windows.Forms.Label();
            this.lblSoHoaDon = new System.Windows.Forms.Label();
            this.lblTenNccVal = new System.Windows.Forms.Label();
            this.lblTenNcc = new System.Windows.Forms.Label();
            this.lblNgayNhapVal = new System.Windows.Forms.Label();
            this.lblNgayNhap = new System.Windows.Forms.Label();
            this.lblNguoiNhapVal = new System.Windows.Forms.Label();
            this.lblNguoiNhap = new System.Windows.Forms.Label();
            this.lblNguoiTaoVal = new System.Windows.Forms.Label();
            this.lblNguoiTao = new System.Windows.Forms.Label();
            this.dgvChiTiet = new System.Windows.Forms.DataGridView();
            this.colStt = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colMaHangNoiBo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colTenHang = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colDonViTinh = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colSoLuong = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colDonGia = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colGiamGia = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colThanhTien = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.pnlTongKet = new System.Windows.Forms.Panel();
            this.txtGhiChu = new System.Windows.Forms.TextBox();
            this.lblGhiChu = new System.Windows.Forms.Label();
            this.lblCanTraNccVal = new System.Windows.Forms.Label();
            this.lblCanTraNcc = new System.Windows.Forms.Label();
            this.lblTienDaTraVal = new System.Windows.Forms.Label();
            this.lblTienDaTra = new System.Windows.Forms.Label();
            this.lblTongCongVal = new System.Windows.Forms.Label();
            this.lblTongCong = new System.Windows.Forms.Label();
            this.lblGiamGiaVal = new System.Windows.Forms.Label();
            this.lblGiamGia = new System.Windows.Forms.Label();
            this.lblTongTienHangVal = new System.Windows.Forms.Label();
            this.lblTongTienHang = new System.Windows.Forms.Label();
            this.lblSoLuongMatHangVal = new System.Windows.Forms.Label();
            this.pnlBottom = new System.Windows.Forms.Panel();
            this.btnDong = new System.Windows.Forms.Button();
            this.btnTraHangNhap = new System.Windows.Forms.Button();
            this.btnHoanThanh = new System.Windows.Forms.Button();
            this.btnXuatFile = new System.Windows.Forms.Button();
            this.btnXoa = new System.Windows.Forms.Button();
            this.pnlTop.SuspendLayout();
            this.pnlThongTin.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvChiTiet)).BeginInit();
            this.pnlTongKet.SuspendLayout();
            this.pnlBottom.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlTop
            // 
            this.pnlTop.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(250)))), ((int)(((byte)(252)))));
            this.pnlTop.Controls.Add(this.lblKhoChiNhanh);
            this.pnlTop.Controls.Add(this.lblTrangThai);
            this.pnlTop.Controls.Add(this.lblMaPhieuHienThi);
            this.pnlTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlTop.Location = new System.Drawing.Point(0, 0);
            this.pnlTop.Name = "pnlTop";
            this.pnlTop.Size = new System.Drawing.Size(1034, 55);
            this.pnlTop.TabIndex = 0;
            // 
            // lblKhoChiNhanh
            // 
            this.lblKhoChiNhanh.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblKhoChiNhanh.AutoSize = true;
            this.lblKhoChiNhanh.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.lblKhoChiNhanh.Location = new System.Drawing.Point(830, 20);
            this.lblKhoChiNhanh.Name = "lblKhoChiNhanh";
            this.lblKhoChiNhanh.Size = new System.Drawing.Size(117, 17);
            this.lblKhoChiNhanh.TabIndex = 2;
            this.lblKhoChiNhanh.Text = "Kho: Kho trung tâm";
            // 
            // lblTrangThai
            // 
            this.lblTrangThai.AutoSize = true;
            this.lblTrangThai.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular);
            this.lblTrangThai.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(22)))), ((int)(((byte)(163)))), ((int)(((byte)(74)))));
            this.lblTrangThai.Location = new System.Drawing.Point(165, 18);
            this.lblTrangThai.Name = "lblTrangThai";
            this.lblTrangThai.Size = new System.Drawing.Size(95, 19);
            this.lblTrangThai.TabIndex = 1;
            this.lblTrangThai.Text = "Đã nhập hàng";
            // 
            // lblMaPhieuHienThi
            // 
            this.lblMaPhieuHienThi.AutoSize = true;
            this.lblMaPhieuHienThi.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.lblMaPhieuHienThi.Location = new System.Drawing.Point(20, 15);
            this.lblMaPhieuHienThi.Name = "lblMaPhieuHienThi";
            this.lblMaPhieuHienThi.Size = new System.Drawing.Size(102, 25);
            this.lblMaPhieuHienThi.TabIndex = 0;
            this.lblMaPhieuHienThi.Text = "PN000046";
            // 
            // pnlThongTin
            // 
            this.pnlThongTin.BackColor = System.Drawing.Color.White;
            this.pnlThongTin.Controls.Add(this.lblSoHoaDonVal);
            this.pnlThongTin.Controls.Add(this.lblSoHoaDon);
            this.pnlThongTin.Controls.Add(this.lblTenNccVal);
            this.pnlThongTin.Controls.Add(this.lblTenNcc);
            this.pnlThongTin.Controls.Add(this.lblNgayNhapVal);
            this.pnlThongTin.Controls.Add(this.lblNgayNhap);
            this.pnlThongTin.Controls.Add(this.lblNguoiNhapVal);
            this.pnlThongTin.Controls.Add(this.lblNguoiNhap);
            this.pnlThongTin.Controls.Add(this.lblNguoiTaoVal);
            this.pnlThongTin.Controls.Add(this.lblNguoiTao);
            this.pnlThongTin.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlThongTin.Location = new System.Drawing.Point(0, 55);
            this.pnlThongTin.Name = "pnlThongTin";
            this.pnlThongTin.Size = new System.Drawing.Size(1034, 75);
            this.pnlThongTin.TabIndex = 1;
            // 
            // lblSoHoaDonVal
            // 
            this.lblSoHoaDonVal.AutoSize = true;
            this.lblSoHoaDonVal.Location = new System.Drawing.Point(750, 42);
            this.lblSoHoaDonVal.Name = "lblSoHoaDonVal";
            this.lblSoHoaDonVal.Size = new System.Drawing.Size(13, 17);
            this.lblSoHoaDonVal.TabIndex = 9;
            this.lblSoHoaDonVal.Text = "-";
            // 
            // lblSoHoaDon
            // 
            this.lblSoHoaDon.AutoSize = true;
            this.lblSoHoaDon.Location = new System.Drawing.Point(665, 42);
            this.lblSoHoaDon.Name = "lblSoHoaDon";
            this.lblSoHoaDon.Size = new System.Drawing.Size(76, 17);
            this.lblSoHoaDon.TabIndex = 8;
            this.lblSoHoaDon.Text = "Số hóa đơn:";
            // 
            // lblTenNccVal
            // 
            this.lblTenNccVal.AutoSize = true;
            this.lblTenNccVal.Location = new System.Drawing.Point(100, 42);
            this.lblTenNccVal.Name = "lblTenNccVal";
            this.lblTenNccVal.Size = new System.Drawing.Size(124, 17);
            this.lblTenNccVal.TabIndex = 7;
            this.lblTenNccVal.Text = "Công ty Pharmedic";
            // 
            // lblTenNcc
            // 
            this.lblTenNcc.AutoSize = true;
            this.lblTenNcc.Location = new System.Drawing.Point(20, 42);
            this.lblTenNcc.Name = "lblTenNcc";
            this.lblTenNcc.Size = new System.Drawing.Size(60, 17);
            this.lblTenNcc.TabIndex = 6;
            this.lblTenNcc.Text = "Tên NCC:";
            // 
            // lblNgayNhapVal
            // 
            this.lblNgayNhapVal.AutoSize = true;
            this.lblNgayNhapVal.Location = new System.Drawing.Point(750, 14);
            this.lblNgayNhapVal.Name = "lblNgayNhapVal";
            this.lblNgayNhapVal.Size = new System.Drawing.Size(116, 17);
            this.lblNgayNhapVal.TabIndex = 5;
            this.lblNgayNhapVal.Text = "22/10/2026 16:10";
            // 
            // lblNgayNhap
            // 
            this.lblNgayNhap.AutoSize = true;
            this.lblNgayNhap.Location = new System.Drawing.Point(665, 14);
            this.lblNgayNhap.Name = "lblNgayNhap";
            this.lblNgayNhap.Size = new System.Drawing.Size(75, 17);
            this.lblNgayNhap.TabIndex = 4;
            this.lblNgayNhap.Text = "Ngày nhập:";
            // 
            // lblNguoiNhapVal
            // 
            this.lblNguoiNhapVal.AutoSize = true;
            this.lblNguoiNhapVal.Location = new System.Drawing.Point(440, 14);
            this.lblNguoiNhapVal.Name = "lblNguoiNhapVal";
            this.lblNguoiNhapVal.Size = new System.Drawing.Size(95, 17);
            this.lblNguoiNhapVal.TabIndex = 3;
            this.lblNguoiNhapVal.Text = "Phạm Văn Hảo";
            // 
            // lblNguoiNhap
            // 
            this.lblNguoiNhap.AutoSize = true;
            this.lblNguoiNhap.Location = new System.Drawing.Point(355, 14);
            this.lblNguoiNhap.Name = "lblNguoiNhap";
            this.lblNguoiNhap.Size = new System.Drawing.Size(82, 17);
            this.lblNguoiNhap.TabIndex = 2;
            this.lblNguoiNhap.Text = "Người nhập:";
            // 
            // lblNguoiTaoVal
            // 
            this.lblNguoiTaoVal.AutoSize = true;
            this.lblNguoiTaoVal.Location = new System.Drawing.Point(100, 14);
            this.lblNguoiTaoVal.Name = "lblNguoiTaoVal";
            this.lblNguoiTaoVal.Size = new System.Drawing.Size(95, 17);
            this.lblNguoiTaoVal.TabIndex = 1;
            this.lblNguoiTaoVal.Text = "Phạm Văn Hảo";
            // 
            // lblNguoiTao
            // 
            this.lblNguoiTao.AutoSize = true;
            this.lblNguoiTao.Location = new System.Drawing.Point(20, 14);
            this.lblNguoiTao.Name = "lblNguoiTao";
            this.lblNguoiTao.Size = new System.Drawing.Size(71, 17);
            this.lblNguoiTao.TabIndex = 0;
            this.lblNguoiTao.Text = "Người tạo:";
            // 
            // dgvChiTiet
            // 
            this.dgvChiTiet.AllowUserToAddRows = false;
            this.dgvChiTiet.AllowUserToDeleteRows = false;
            this.dgvChiTiet.AllowUserToResizeRows = false;
            this.dgvChiTiet.BackgroundColor = System.Drawing.Color.White;
            this.dgvChiTiet.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dgvChiTiet.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvChiTiet.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colStt,
            this.colMaHangNoiBo,
            this.colTenHang,
            this.colDonViTinh,
            this.colSoLuong,
            this.colDonGia,
            this.colGiamGia,
            this.colThanhTien});
            this.dgvChiTiet.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvChiTiet.Location = new System.Drawing.Point(0, 130);
            this.dgvChiTiet.MultiSelect = false;
            this.dgvChiTiet.Name = "dgvChiTiet";
            this.dgvChiTiet.ReadOnly = true;
            this.dgvChiTiet.RowHeadersVisible = false;
            this.dgvChiTiet.RowTemplate.Height = 32;
            this.dgvChiTiet.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvChiTiet.Size = new System.Drawing.Size(1034, 340);
            this.dgvChiTiet.TabIndex = 2;
            // 
            // colStt
            // 
            this.colStt.HeaderText = "STT";
            this.colStt.Name = "colStt";
            this.colStt.ReadOnly = true;
            this.colStt.Width = 45;
            // 
            // colMaHangNoiBo
            // 
            this.colMaHangNoiBo.DataPropertyName = "MaHangNoiBo";
            this.colMaHangNoiBo.HeaderText = "Mã hàng";
            this.colMaHangNoiBo.Name = "colMaHangNoiBo";
            this.colMaHangNoiBo.ReadOnly = true;
            this.colMaHangNoiBo.Width = 120;
            // 
            // colTenHang
            // 
            this.colTenHang.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.colTenHang.DataPropertyName = "TenHang";
            this.colTenHang.HeaderText = "Tên hàng";
            this.colTenHang.Name = "colTenHang";
            this.colTenHang.ReadOnly = true;
            // 
            // colDonViTinh
            // 
            this.colDonViTinh.DataPropertyName = "TenDvt";
            this.colDonViTinh.HeaderText = "ĐVT";
            this.colDonViTinh.Name = "colDonViTinh";
            this.colDonViTinh.ReadOnly = true;
            this.colDonViTinh.Width = 75;
            // 
            // colSoLuong
            // 
            this.colSoLuong.DataPropertyName = "SoLuong";
            this.colSoLuong.HeaderText = "Số lượng";
            this.colSoLuong.Name = "colSoLuong";
            this.colSoLuong.ReadOnly = true;
            this.colSoLuong.Width = 90;
            // 
            // colDonGia
            // 
            this.colDonGia.DataPropertyName = "DonGiaNhap";
            this.colDonGia.HeaderText = "Đơn giá";
            this.colDonGia.Name = "colDonGia";
            this.colDonGia.ReadOnly = true;
            this.colDonGia.Width = 120;
            // 
            // colGiamGia
            // 
            this.colGiamGia.DataPropertyName = "GiamGia";
            this.colGiamGia.HeaderText = "Giảm giá (%)";
            this.colGiamGia.Name = "colGiamGia";
            this.colGiamGia.ReadOnly = true;
            this.colGiamGia.Width = 100;
            // 
            // colThanhTien
            // 
            this.colThanhTien.DataPropertyName = "ThanhTien";
            this.colThanhTien.HeaderText = "Thành tiền";
            this.colThanhTien.Name = "colThanhTien";
            this.colThanhTien.ReadOnly = true;
            this.colThanhTien.Width = 135;
            // 
            // pnlTongKet
            // 
            this.pnlTongKet.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(250)))), ((int)(((byte)(250)))), ((int)(((byte)(250)))));
            this.pnlTongKet.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlTongKet.Controls.Add(this.txtGhiChu);
            this.pnlTongKet.Controls.Add(this.lblGhiChu);
            this.pnlTongKet.Controls.Add(this.lblCanTraNccVal);
            this.pnlTongKet.Controls.Add(this.lblCanTraNcc);
            this.pnlTongKet.Controls.Add(this.lblTienDaTraVal);
            this.pnlTongKet.Controls.Add(this.lblTienDaTra);
            this.pnlTongKet.Controls.Add(this.lblTongCongVal);
            this.pnlTongKet.Controls.Add(this.lblTongCong);
            this.pnlTongKet.Controls.Add(this.lblGiamGiaVal);
            this.pnlTongKet.Controls.Add(this.lblGiamGia);
            this.pnlTongKet.Controls.Add(this.lblTongTienHangVal);
            this.pnlTongKet.Controls.Add(this.lblTongTienHang);
            this.pnlTongKet.Controls.Add(this.lblSoLuongMatHangVal);
            this.pnlTongKet.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnlTongKet.Location = new System.Drawing.Point(0, 470);
            this.pnlTongKet.Name = "pnlTongKet";
            this.pnlTongKet.Size = new System.Drawing.Size(1034, 155);
            this.pnlTongKet.TabIndex = 3;
            // 
            // txtGhiChu
            // 
            this.txtGhiChu.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.txtGhiChu.Location = new System.Drawing.Point(20, 35);
            this.txtGhiChu.Multiline = true;
            this.txtGhiChu.Name = "txtGhiChu";
            this.txtGhiChu.ReadOnly = true;
            this.txtGhiChu.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtGhiChu.Size = new System.Drawing.Size(460, 105);
            this.txtGhiChu.TabIndex = 12;
            // 
            // lblGhiChu
            // 
            this.lblGhiChu.AutoSize = true;
            this.lblGhiChu.Location = new System.Drawing.Point(20, 12);
            this.lblGhiChu.Name = "lblGhiChu";
            this.lblGhiChu.Size = new System.Drawing.Size(54, 17);
            this.lblGhiChu.TabIndex = 11;
            this.lblGhiChu.Text = "Ghi chú:";
            // 
            // lblCanTraNccVal
            // 
            this.lblCanTraNccVal.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblCanTraNccVal.ForeColor = System.Drawing.Color.Red;
            this.lblCanTraNccVal.Location = new System.Drawing.Point(850, 122);
            this.lblCanTraNccVal.Name = "lblCanTraNccVal";
            this.lblCanTraNccVal.Size = new System.Drawing.Size(160, 20);
            this.lblCanTraNccVal.TabIndex = 10;
            this.lblCanTraNccVal.Text = "0";
            this.lblCanTraNccVal.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblCanTraNcc
            // 
            this.lblCanTraNcc.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblCanTraNcc.AutoSize = true;
            this.lblCanTraNcc.Location = new System.Drawing.Point(680, 122);
            this.lblCanTraNcc.Name = "lblCanTraNcc";
            this.lblCanTraNcc.Size = new System.Drawing.Size(92, 19);
            this.lblCanTraNcc.TabIndex = 9;
            this.lblCanTraNcc.Text = "Cần trả NCC:";
            // 
            // lblTienDaTraVal
            // 
            this.lblTienDaTraVal.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblTienDaTraVal.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.lblTienDaTraVal.Location = new System.Drawing.Point(850, 95);
            this.lblTienDaTraVal.Name = "lblTienDaTraVal";
            this.lblTienDaTraVal.Size = new System.Drawing.Size(160, 20);
            this.lblTienDaTraVal.TabIndex = 8;
            this.lblTienDaTraVal.Text = "3,179,000";
            this.lblTienDaTraVal.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblTienDaTra
            // 
            this.lblTienDaTra.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblTienDaTra.AutoSize = true;
            this.lblTienDaTra.Location = new System.Drawing.Point(680, 95);
            this.lblTienDaTra.Name = "lblTienDaTra";
            this.lblTienDaTra.Size = new System.Drawing.Size(102, 17);
            this.lblTienDaTra.TabIndex = 7;
            this.lblTienDaTra.Text = "Tiền đã trả NCC:";
            // 
            // lblTongCongVal
            // 
            this.lblTongCongVal.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblTongCongVal.Location = new System.Drawing.Point(850, 68);
            this.lblTongCongVal.Name = "lblTongCongVal";
            this.lblTongCongVal.Size = new System.Drawing.Size(160, 20);
            this.lblTongCongVal.TabIndex = 6;
            this.lblTongCongVal.Text = "3,179,000";
            this.lblTongCongVal.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblTongCong
            // 
            this.lblTongCong.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblTongCong.AutoSize = true;
            this.lblTongCong.Location = new System.Drawing.Point(680, 68);
            this.lblTongCong.Name = "lblTongCong";
            this.lblTongCong.Size = new System.Drawing.Size(84, 19);
            this.lblTongCong.TabIndex = 5;
            this.lblTongCong.Text = "Tổng cộng:";
            // 
            // lblGiamGiaVal
            // 
            this.lblGiamGiaVal.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblGiamGiaVal.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.lblGiamGiaVal.Location = new System.Drawing.Point(850, 41);
            this.lblGiamGiaVal.Name = "lblGiamGiaVal";
            this.lblGiamGiaVal.Size = new System.Drawing.Size(160, 20);
            this.lblGiamGiaVal.TabIndex = 4;
            this.lblGiamGiaVal.Text = "0";
            this.lblGiamGiaVal.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblGiamGia
            // 
            this.lblGiamGia.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblGiamGia.AutoSize = true;
            this.lblGiamGia.Location = new System.Drawing.Point(680, 41);
            this.lblGiamGia.Name = "lblGiamGia";
            this.lblGiamGia.Size = new System.Drawing.Size(63, 17);
            this.lblGiamGia.TabIndex = 3;
            this.lblGiamGia.Text = "Giảm giá (%):";
            // 
            // lblTongTienHangVal
            // 
            this.lblTongTienHangVal.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblTongTienHangVal.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.lblTongTienHangVal.Location = new System.Drawing.Point(850, 14);
            this.lblTongTienHangVal.Name = "lblTongTienHangVal";
            this.lblTongTienHangVal.Size = new System.Drawing.Size(160, 20);
            this.lblTongTienHangVal.TabIndex = 2;
            this.lblTongTienHangVal.Text = "3,179,000";
            this.lblTongTienHangVal.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblTongTienHang
            // 
            this.lblTongTienHang.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblTongTienHang.AutoSize = true;
            this.lblTongTienHang.Location = new System.Drawing.Point(680, 14);
            this.lblTongTienHang.Name = "lblTongTienHang";
            this.lblTongTienHang.Size = new System.Drawing.Size(99, 17);
            this.lblTongTienHang.TabIndex = 1;
            this.lblTongTienHang.Text = "Tổng tiền hàng:";
            // 
            // lblSoLuongMatHangVal
            // 
            this.lblSoLuongMatHangVal.AutoSize = true;
            this.lblSoLuongMatHangVal.Location = new System.Drawing.Point(500, 14);
            this.lblSoLuongMatHangVal.Name = "lblSoLuongMatHangVal";
            this.lblSoLuongMatHangVal.Size = new System.Drawing.Size(135, 17);
            this.lblSoLuongMatHangVal.TabIndex = 0;
            this.lblSoLuongMatHangVal.Text = "Số lượng mặt hàng: 10";
            // 
            // pnlBottom
            // 
            this.pnlBottom.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(250)))), ((int)(((byte)(252)))));
            this.pnlBottom.Controls.Add(this.btnDong);
            this.pnlBottom.Controls.Add(this.btnTraHangNhap);
            this.pnlBottom.Controls.Add(this.btnHoanThanh);
            this.pnlBottom.Controls.Add(this.btnXuatFile);
            this.pnlBottom.Controls.Add(this.btnXoa);
            this.pnlBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnlBottom.Location = new System.Drawing.Point(0, 625);
            this.pnlBottom.Name = "pnlBottom";
            this.pnlBottom.Size = new System.Drawing.Size(1034, 55);
            this.pnlBottom.TabIndex = 4;
            // 
            // btnDong
            // 
            this.btnDong.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnDong.Location = new System.Drawing.Point(927, 12);
            this.btnDong.Name = "btnDong";
            this.btnDong.Size = new System.Drawing.Size(95, 32);
            this.btnDong.TabIndex = 3;
            this.btnDong.Text = "Đóng";
            this.btnDong.UseVisualStyleBackColor = true;
            this.btnDong.Click += new System.EventHandler(this.btnDong_Click);
            // 
            // btnTraHangNhap
            // 
            this.btnHoanThanh.Location = new System.Drawing.Point(232, 12);
            this.btnHoanThanh.Name = "btnHoanThanh";
            this.btnHoanThanh.Size = new System.Drawing.Size(110, 32);
            this.btnHoanThanh.TabIndex = 2;
            this.btnHoanThanh.Text = "Hoàn thành";
            this.btnHoanThanh.UseVisualStyleBackColor = true;
            this.btnHoanThanh.Visible = false;
            this.btnHoanThanh.Click += new System.EventHandler(this.btnHoanThanh_Click);
            // 
            // btnTraHangNhap
            // 
            this.btnTraHangNhap.Location = new System.Drawing.Point(348, 12);
            this.btnTraHangNhap.Name = "btnTraHangNhap";
            this.btnTraHangNhap.Size = new System.Drawing.Size(130, 32);
            this.btnTraHangNhap.TabIndex = 2;
            this.btnTraHangNhap.Text = "Trả hàng nhập";
            this.btnTraHangNhap.UseVisualStyleBackColor = true;
            this.btnTraHangNhap.Click += new System.EventHandler(this.btnTraHangNhap_Click);
            // 
            // btnXuatFile
            // 
            this.btnXuatFile.Location = new System.Drawing.Point(126, 12);
            this.btnXuatFile.Name = "btnXuatFile";
            this.btnXuatFile.Size = new System.Drawing.Size(100, 32);
            this.btnXuatFile.TabIndex = 1;
            this.btnXuatFile.Text = "Xuất file";
            this.btnXuatFile.UseVisualStyleBackColor = true;
            this.btnXuatFile.Click += new System.EventHandler(this.btnXuatFile_Click);
            // 
            // btnXoa
            // 
            this.btnXoa.Location = new System.Drawing.Point(20, 12);
            this.btnXoa.Name = "btnXoa";
            this.btnXoa.Size = new System.Drawing.Size(100, 32);
            this.btnXoa.TabIndex = 0;
            this.btnXoa.Text = "Xóa";
            this.btnXoa.UseVisualStyleBackColor = true;
            this.btnXoa.Click += new System.EventHandler(this.btnXoa_Click);
            // 
            // frmChiTietPhieuNhap
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;

            this.ClientSize = new System.Drawing.Size(1034, 680);
            this.CancelButton = this.btnDong;
            this.Controls.Add(this.dgvChiTiet);
            this.Controls.Add(this.pnlTongKet);
            this.Controls.Add(this.pnlThongTin);
            this.Controls.Add(this.pnlTop);
            this.Controls.Add(this.pnlBottom);
            this.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.MinimumSize = new System.Drawing.Size(950, 600);
            this.Name = "frmChiTietPhieuNhap";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Chi tiết đơn nhập hàng";
            this.Load += new System.EventHandler(this.frmChiTietPhieuNhap_Load);
            this.pnlTop.ResumeLayout(false);
            this.pnlTop.PerformLayout();
            this.pnlThongTin.ResumeLayout(false);
            this.pnlThongTin.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvChiTiet)).EndInit();
            this.pnlTongKet.ResumeLayout(false);
            this.pnlTongKet.PerformLayout();
            this.pnlBottom.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pnlTop;
        private System.Windows.Forms.Label lblMaPhieuHienThi;
        private System.Windows.Forms.Label lblTrangThai;
        private System.Windows.Forms.Label lblKhoChiNhanh;
        private System.Windows.Forms.Panel pnlThongTin;
        private System.Windows.Forms.Label lblNguoiTao;
        private System.Windows.Forms.Label lblNguoiTaoVal;
        private System.Windows.Forms.Label lblNguoiNhap;
        private System.Windows.Forms.Label lblNguoiNhapVal;
        private System.Windows.Forms.Label lblNgayNhap;
        private System.Windows.Forms.Label lblNgayNhapVal;
        private System.Windows.Forms.Label lblTenNcc;
        private System.Windows.Forms.Label lblTenNccVal;
        private System.Windows.Forms.Label lblSoHoaDon;
        private System.Windows.Forms.Label lblSoHoaDonVal;
        private System.Windows.Forms.DataGridView dgvChiTiet;
        private System.Windows.Forms.Panel pnlTongKet;
        private System.Windows.Forms.Label lblSoLuongMatHangVal;
        private System.Windows.Forms.Label lblTongTienHang;
        private System.Windows.Forms.Label lblTongTienHangVal;
        private System.Windows.Forms.Label lblGiamGia;
        private System.Windows.Forms.Label lblGiamGiaVal;
        private System.Windows.Forms.Label lblTongCong;
        private System.Windows.Forms.Label lblTongCongVal;
        private System.Windows.Forms.Label lblTienDaTra;
        private System.Windows.Forms.Label lblTienDaTraVal;
        private System.Windows.Forms.Label lblCanTraNcc;
        private System.Windows.Forms.Label lblCanTraNccVal;
        private System.Windows.Forms.TextBox txtGhiChu;
        private System.Windows.Forms.Label lblGhiChu;
        private System.Windows.Forms.Panel pnlBottom;
        private System.Windows.Forms.Button btnXoa;
        private System.Windows.Forms.Button btnXuatFile;
        private System.Windows.Forms.Button btnHoanThanh;
        private System.Windows.Forms.Button btnTraHangNhap;
        private System.Windows.Forms.Button btnDong;
        private System.Windows.Forms.DataGridViewTextBoxColumn colStt;
        private System.Windows.Forms.DataGridViewTextBoxColumn colMaHangNoiBo;
        private System.Windows.Forms.DataGridViewTextBoxColumn colTenHang;
        private System.Windows.Forms.DataGridViewTextBoxColumn colDonViTinh;
        private System.Windows.Forms.DataGridViewTextBoxColumn colSoLuong;
        private System.Windows.Forms.DataGridViewTextBoxColumn colDonGia;
        private System.Windows.Forms.DataGridViewTextBoxColumn colGiamGia;
        private System.Windows.Forms.DataGridViewTextBoxColumn colThanhTien;
    }
}
