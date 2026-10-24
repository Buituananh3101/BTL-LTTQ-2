namespace BTL_LTTQ_2.GUI.Forms.TraHangNhap
{
    partial class frmChiTietTraHangNhap
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
            this.lblMaNhapHangVal = new System.Windows.Forms.Label();
            this.lblMaNhapHang = new System.Windows.Forms.Label();
            this.lblTenNccVal = new System.Windows.Forms.Label();
            this.lblTenNcc = new System.Windows.Forms.Label();
            this.lblNgayTraVal = new System.Windows.Forms.Label();
            this.lblNgayTra = new System.Windows.Forms.Label();
            this.lblNguoiTraVal = new System.Windows.Forms.Label();
            this.lblNguoiTra = new System.Windows.Forms.Label();
            this.lblNguoiTaoVal = new System.Windows.Forms.Label();
            this.lblNguoiTao = new System.Windows.Forms.Label();
            this.dgvChiTiet = new System.Windows.Forms.DataGridView();
            this.colStt = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colMaHangNoiBo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colTenHang = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colDonViTinh = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colSoLuong = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colGiaNhap = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colDonGiaTra = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colThanhTien = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.pnlTongKet = new System.Windows.Forms.Panel();
            this.txtGhiChu = new System.Windows.Forms.TextBox();
            this.lblGhiChu = new System.Windows.Forms.Label();
            this.lblDaThuNccVal = new System.Windows.Forms.Label();
            this.lblDaThuNcc = new System.Windows.Forms.Label();
            this.lblCanThuNccVal = new System.Windows.Forms.Label();
            this.lblCanThuNcc = new System.Windows.Forms.Label();
            this.lblGiamGiaVal = new System.Windows.Forms.Label();
            this.lblGiamGia = new System.Windows.Forms.Label();
            this.lblTongTienHangVal = new System.Windows.Forms.Label();
            this.lblTongTienHang = new System.Windows.Forms.Label();
            this.lblSoLuongMatHangVal = new System.Windows.Forms.Label();
            this.lblSoLuongMatHang = new System.Windows.Forms.Label();
            this.pnlBottom = new System.Windows.Forms.Panel();
            this.btnDong = new System.Windows.Forms.Button();
            this.btnXuatFile = new System.Windows.Forms.Button();
            this.btnHoanThanh = new System.Windows.Forms.Button();
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
            this.lblTrangThai.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.lblTrangThai.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(22)))), ((int)(((byte)(163)))), ((int)(((byte)(74)))));
            this.lblTrangThai.Location = new System.Drawing.Point(165, 18);
            this.lblTrangThai.Name = "lblTrangThai";
            this.lblTrangThai.Size = new System.Drawing.Size(81, 19);
            this.lblTrangThai.TabIndex = 1;
            this.lblTrangThai.Text = "Đã trả hàng";
            // 
            // lblMaPhieuHienThi
            // 
            this.lblMaPhieuHienThi.AutoSize = true;
            this.lblMaPhieuHienThi.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.lblMaPhieuHienThi.Location = new System.Drawing.Point(20, 16);
            this.lblMaPhieuHienThi.Name = "lblMaPhieuHienThi";
            this.lblMaPhieuHienThi.Size = new System.Drawing.Size(109, 21);
            this.lblMaPhieuHienThi.TabIndex = 0;
            this.lblMaPhieuHienThi.Text = "THN000001";
            // 
            // pnlThongTin
            // 
            this.pnlThongTin.BackColor = System.Drawing.Color.White;
            this.pnlThongTin.Controls.Add(this.lblMaNhapHangVal);
            this.pnlThongTin.Controls.Add(this.lblMaNhapHang);
            this.pnlThongTin.Controls.Add(this.lblTenNccVal);
            this.pnlThongTin.Controls.Add(this.lblTenNcc);
            this.pnlThongTin.Controls.Add(this.lblNgayTraVal);
            this.pnlThongTin.Controls.Add(this.lblNgayTra);
            this.pnlThongTin.Controls.Add(this.lblNguoiTraVal);
            this.pnlThongTin.Controls.Add(this.lblNguoiTra);
            this.pnlThongTin.Controls.Add(this.lblNguoiTaoVal);
            this.pnlThongTin.Controls.Add(this.lblNguoiTao);
            this.pnlThongTin.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlThongTin.Location = new System.Drawing.Point(0, 55);
            this.pnlThongTin.Name = "pnlThongTin";
            this.pnlThongTin.Size = new System.Drawing.Size(1034, 90);
            this.pnlThongTin.TabIndex = 1;
            // 
            // lblMaNhapHangVal
            // 
            this.lblMaNhapHangVal.AutoSize = true;
            this.lblMaNhapHangVal.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.lblMaNhapHangVal.Location = new System.Drawing.Point(475, 52);
            this.lblMaNhapHangVal.Name = "lblMaNhapHangVal";
            this.lblMaNhapHangVal.Size = new System.Drawing.Size(64, 17);
            this.lblMaNhapHangVal.TabIndex = 9;
            this.lblMaNhapHangVal.Text = "PN000046";
            // 
            // lblMaNhapHang
            // 
            this.lblMaNhapHang.AutoSize = true;
            this.lblMaNhapHang.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.lblMaNhapHang.Location = new System.Drawing.Point(380, 52);
            this.lblMaNhapHang.Name = "lblMaNhapHang";
            this.lblMaNhapHang.Size = new System.Drawing.Size(95, 17);
            this.lblMaNhapHang.TabIndex = 8;
            this.lblMaNhapHang.Text = "Mã nhập hàng:";
            // 
            // lblTenNccVal
            // 
            this.lblTenNccVal.AutoSize = true;
            this.lblTenNccVal.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.lblTenNccVal.Location = new System.Drawing.Point(100, 52);
            this.lblTenNccVal.Name = "lblTenNccVal";
            this.lblTenNccVal.Size = new System.Drawing.Size(125, 17);
            this.lblTenNccVal.TabIndex = 7;
            this.lblTenNccVal.Text = "Công ty Dược phẩm";
            // 
            // lblTenNcc
            // 
            this.lblTenNcc.AutoSize = true;
            this.lblTenNcc.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.lblTenNcc.Location = new System.Drawing.Point(20, 52);
            this.lblTenNcc.Name = "lblTenNcc";
            this.lblTenNcc.Size = new System.Drawing.Size(61, 17);
            this.lblTenNcc.TabIndex = 6;
            this.lblTenNcc.Text = "Tên NCC:";
            // 
            // lblNgayTraVal
            // 
            this.lblNgayTraVal.AutoSize = true;
            this.lblNgayTraVal.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.lblNgayTraVal.Location = new System.Drawing.Point(745, 18);
            this.lblNgayTraVal.Name = "lblNgayTraVal";
            this.lblNgayTraVal.Size = new System.Drawing.Size(111, 17);
            this.lblNgayTraVal.TabIndex = 5;
            this.lblNgayTraVal.Text = "22/10/2026 17:13";
            // 
            // lblNgayTra
            // 
            this.lblNgayTra.AutoSize = true;
            this.lblNgayTra.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.lblNgayTra.Location = new System.Drawing.Point(675, 18);
            this.lblNgayTra.Name = "lblNgayTra";
            this.lblNgayTra.Size = new System.Drawing.Size(62, 17);
            this.lblNgayTra.TabIndex = 4;
            this.lblNgayTra.Text = "Ngày trả:";
            // 
            // lblNguoiTraVal
            // 
            this.lblNguoiTraVal.AutoSize = true;
            this.lblNguoiTraVal.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.lblNguoiTraVal.Location = new System.Drawing.Point(475, 18);
            this.lblNguoiTraVal.Name = "lblNguoiTraVal";
            this.lblNguoiTraVal.Size = new System.Drawing.Size(91, 17);
            this.lblNguoiTraVal.TabIndex = 3;
            this.lblNguoiTraVal.Text = "Phạm Văn Hảo";
            // 
            // lblNguoiTra
            // 
            this.lblNguoiTra.AutoSize = true;
            this.lblNguoiTra.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.lblNguoiTra.Location = new System.Drawing.Point(380, 18);
            this.lblNguoiTra.Name = "lblNguoiTra";
            this.lblNguoiTra.Size = new System.Drawing.Size(66, 17);
            this.lblNguoiTra.TabIndex = 2;
            this.lblNguoiTra.Text = "Người trả:";
            // 
            // lblNguoiTaoVal
            // 
            this.lblNguoiTaoVal.AutoSize = true;
            this.lblNguoiTaoVal.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.lblNguoiTaoVal.Location = new System.Drawing.Point(100, 18);
            this.lblNguoiTaoVal.Name = "lblNguoiTaoVal";
            this.lblNguoiTaoVal.Size = new System.Drawing.Size(91, 17);
            this.lblNguoiTaoVal.TabIndex = 1;
            this.lblNguoiTaoVal.Text = "Phạm Văn Hảo";
            // 
            // lblNguoiTao
            // 
            this.lblNguoiTao.AutoSize = true;
            this.lblNguoiTao.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.lblNguoiTao.Location = new System.Drawing.Point(20, 18);
            this.lblNguoiTao.Name = "lblNguoiTao";
            this.lblNguoiTao.Size = new System.Drawing.Size(70, 17);
            this.lblNguoiTao.TabIndex = 0;
            this.lblNguoiTao.Text = "Người tạo:";
            // 
            // dgvChiTiet
            // 
            this.dgvChiTiet.AllowUserToAddRows = false;
            this.dgvChiTiet.AllowUserToDeleteRows = false;
            this.dgvChiTiet.BackgroundColor = System.Drawing.Color.White;
            this.dgvChiTiet.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dgvChiTiet.ColumnHeadersHeight = 36;
            this.dgvChiTiet.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colStt,
            this.colMaHangNoiBo,
            this.colTenHang,
            this.colDonViTinh,
            this.colSoLuong,
            this.colGiaNhap,
            this.colDonGiaTra,
            this.colThanhTien});
            this.dgvChiTiet.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvChiTiet.Location = new System.Drawing.Point(0, 145);
            this.dgvChiTiet.Name = "dgvChiTiet";
            this.dgvChiTiet.ReadOnly = true;
            this.dgvChiTiet.RowHeadersVisible = false;
            this.dgvChiTiet.RowHeadersWidth = 51;
            this.dgvChiTiet.RowTemplate.Height = 32;
            this.dgvChiTiet.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvChiTiet.Size = new System.Drawing.Size(1034, 370);
            this.dgvChiTiet.TabIndex = 2;
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
            // colMaHangNoiBo
            // 
            this.colMaHangNoiBo.DataPropertyName = "MaHangNoiBo";
            this.colMaHangNoiBo.HeaderText = "Mã hàng";
            this.colMaHangNoiBo.MinimumWidth = 6;
            this.colMaHangNoiBo.Name = "colMaHangNoiBo";
            this.colMaHangNoiBo.ReadOnly = true;
            this.colMaHangNoiBo.Width = 120;
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
            // colSoLuong
            // 
            this.colSoLuong.DataPropertyName = "SoLuong";
            this.colSoLuong.HeaderText = "Số lượng";
            this.colSoLuong.MinimumWidth = 6;
            this.colSoLuong.Name = "colSoLuong";
            this.colSoLuong.ReadOnly = true;
            this.colSoLuong.Width = 100;
            // 
            // colGiaNhap
            // 
            this.colGiaNhap.DataPropertyName = "GiaNhap";
            this.colGiaNhap.HeaderText = "Giá nhập";
            this.colGiaNhap.MinimumWidth = 6;
            this.colGiaNhap.Name = "colGiaNhap";
            this.colGiaNhap.ReadOnly = true;
            this.colGiaNhap.Width = 110;
            // 
            // colDonGiaTra
            // 
            this.colDonGiaTra.DataPropertyName = "DonGiaTra";
            this.colDonGiaTra.HeaderText = "Giá trả lại";
            this.colDonGiaTra.MinimumWidth = 6;
            this.colDonGiaTra.Name = "colDonGiaTra";
            this.colDonGiaTra.ReadOnly = true;
            this.colDonGiaTra.Width = 110;
            // 
            // colThanhTien
            // 
            this.colThanhTien.DataPropertyName = "ThanhTien";
            this.colThanhTien.HeaderText = "Thành tiền";
            this.colThanhTien.MinimumWidth = 6;
            this.colThanhTien.Name = "colThanhTien";
            this.colThanhTien.ReadOnly = true;
            this.colThanhTien.Width = 130;
            // 
            // pnlTongKet
            // 
            this.pnlTongKet.BackColor = System.Drawing.Color.White;
            this.pnlTongKet.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlTongKet.Controls.Add(this.txtGhiChu);
            this.pnlTongKet.Controls.Add(this.lblGhiChu);
            this.pnlTongKet.Controls.Add(this.lblDaThuNccVal);
            this.pnlTongKet.Controls.Add(this.lblDaThuNcc);
            this.pnlTongKet.Controls.Add(this.lblCanThuNccVal);
            this.pnlTongKet.Controls.Add(this.lblCanThuNcc);
            this.pnlTongKet.Controls.Add(this.lblGiamGiaVal);
            this.pnlTongKet.Controls.Add(this.lblGiamGia);
            this.pnlTongKet.Controls.Add(this.lblTongTienHangVal);
            this.pnlTongKet.Controls.Add(this.lblTongTienHang);
            this.pnlTongKet.Controls.Add(this.lblSoLuongMatHangVal);
            this.pnlTongKet.Controls.Add(this.lblSoLuongMatHang);
            this.pnlTongKet.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnlTongKet.Location = new System.Drawing.Point(0, 515);
            this.pnlTongKet.Name = "pnlTongKet";
            this.pnlTongKet.Size = new System.Drawing.Size(1034, 150);
            this.pnlTongKet.TabIndex = 3;
            // 
            // txtGhiChu
            // 
            this.txtGhiChu.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.txtGhiChu.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(250)))), ((int)(((byte)(252)))));
            this.txtGhiChu.Location = new System.Drawing.Point(24, 38);
            this.txtGhiChu.Multiline = true;
            this.txtGhiChu.Name = "txtGhiChu";
            this.txtGhiChu.ReadOnly = true;
            this.txtGhiChu.Size = new System.Drawing.Size(520, 95);
            this.txtGhiChu.TabIndex = 11;
            // 
            // lblGhiChu
            // 
            this.lblGhiChu.AutoSize = true;
            this.lblGhiChu.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.lblGhiChu.Location = new System.Drawing.Point(20, 14);
            this.lblGhiChu.Name = "lblGhiChu";
            this.lblGhiChu.Size = new System.Drawing.Size(54, 17);
            this.lblGhiChu.TabIndex = 10;
            this.lblGhiChu.Text = "Ghi chú:";
            // 
            // lblDaThuNccVal
            // 
            this.lblDaThuNccVal.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblDaThuNccVal.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.lblDaThuNccVal.Location = new System.Drawing.Point(860, 116);
            this.lblDaThuNccVal.Name = "lblDaThuNccVal";
            this.lblDaThuNccVal.Size = new System.Drawing.Size(150, 17);
            this.lblDaThuNccVal.TabIndex = 9;
            this.lblDaThuNccVal.Text = "0 đ";
            this.lblDaThuNccVal.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblDaThuNcc
            // 
            this.lblDaThuNcc.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblDaThuNcc.AutoSize = true;
            this.lblDaThuNcc.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.lblDaThuNcc.Location = new System.Drawing.Point(680, 116);
            this.lblDaThuNcc.Name = "lblDaThuNcc";
            this.lblDaThuNcc.Size = new System.Drawing.Size(76, 17);
            this.lblDaThuNcc.TabIndex = 8;
            this.lblDaThuNcc.Text = "NCC đã trả:";
            // 
            // lblCanThuNccVal
            // 
            this.lblCanThuNccVal.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblCanThuNccVal.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.lblCanThuNccVal.Location = new System.Drawing.Point(840, 90);
            this.lblCanThuNccVal.Name = "lblCanThuNccVal";
            this.lblCanThuNccVal.Size = new System.Drawing.Size(170, 19);
            this.lblCanThuNccVal.TabIndex = 7;
            this.lblCanThuNccVal.Text = "0 đ";
            this.lblCanThuNccVal.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblCanThuNcc
            // 
            this.lblCanThuNcc.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblCanThuNcc.AutoSize = true;
            this.lblCanThuNcc.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.lblCanThuNcc.Location = new System.Drawing.Point(680, 90);
            this.lblCanThuNcc.Name = "lblCanThuNcc";
            this.lblCanThuNcc.Size = new System.Drawing.Size(84, 17);
            this.lblCanThuNcc.TabIndex = 6;
            this.lblCanThuNcc.Text = "NCC cần trả:";
            // 
            // lblGiamGiaVal
            // 
            this.lblGiamGiaVal.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblGiamGiaVal.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.lblGiamGiaVal.Location = new System.Drawing.Point(860, 64);
            this.lblGiamGiaVal.Name = "lblGiamGiaVal";
            this.lblGiamGiaVal.Size = new System.Drawing.Size(150, 17);
            this.lblGiamGiaVal.TabIndex = 5;
            this.lblGiamGiaVal.Text = "0 đ";
            this.lblGiamGiaVal.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblGiamGia
            // 
            this.lblGiamGia.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblGiamGia.AutoSize = true;
            this.lblGiamGia.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.lblGiamGia.Location = new System.Drawing.Point(680, 64);
            this.lblGiamGia.Name = "lblGiamGia";
            this.lblGiamGia.Size = new System.Drawing.Size(63, 17);
            this.lblGiamGia.TabIndex = 4;
            this.lblGiamGia.Text = "Giảm giá:";
            // 
            // lblTongTienHangVal
            // 
            this.lblTongTienHangVal.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblTongTienHangVal.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.lblTongTienHangVal.Location = new System.Drawing.Point(860, 38);
            this.lblTongTienHangVal.Name = "lblTongTienHangVal";
            this.lblTongTienHangVal.Size = new System.Drawing.Size(150, 17);
            this.lblTongTienHangVal.TabIndex = 3;
            this.lblTongTienHangVal.Text = "0 đ";
            this.lblTongTienHangVal.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblTongTienHang
            // 
            this.lblTongTienHang.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblTongTienHang.AutoSize = true;
            this.lblTongTienHang.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.lblTongTienHang.Location = new System.Drawing.Point(680, 38);
            this.lblTongTienHang.Name = "lblTongTienHang";
            this.lblTongTienHang.Size = new System.Drawing.Size(107, 17);
            this.lblTongTienHang.TabIndex = 2;
            this.lblTongTienHang.Text = "Tổng tiền hàng:";
            // 
            // lblSoLuongMatHangVal
            // 
            this.lblSoLuongMatHangVal.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblSoLuongMatHangVal.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.lblSoLuongMatHangVal.Location = new System.Drawing.Point(860, 14);
            this.lblSoLuongMatHangVal.Name = "lblSoLuongMatHangVal";
            this.lblSoLuongMatHangVal.Size = new System.Drawing.Size(150, 17);
            this.lblSoLuongMatHangVal.TabIndex = 1;
            this.lblSoLuongMatHangVal.Text = "0";
            this.lblSoLuongMatHangVal.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblSoLuongMatHang
            // 
            this.lblSoLuongMatHang.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblSoLuongMatHang.AutoSize = true;
            this.lblSoLuongMatHang.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.lblSoLuongMatHang.Location = new System.Drawing.Point(680, 14);
            this.lblSoLuongMatHang.Name = "lblSoLuongMatHang";
            this.lblSoLuongMatHang.Size = new System.Drawing.Size(126, 17);
            this.lblSoLuongMatHang.TabIndex = 0;
            this.lblSoLuongMatHang.Text = "Số lượng mặt hàng:";
            // 
            // pnlBottom
            // 
            this.pnlBottom.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(250)))), ((int)(((byte)(252)))));
            this.pnlBottom.Controls.Add(this.btnDong);
            this.pnlBottom.Controls.Add(this.btnXuatFile);
            this.pnlBottom.Controls.Add(this.btnHoanThanh);
            this.pnlBottom.Controls.Add(this.btnXoa);
            this.pnlBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnlBottom.Location = new System.Drawing.Point(0, 665);
            this.pnlBottom.Name = "pnlBottom";
            this.pnlBottom.Size = new System.Drawing.Size(1034, 55);
            this.pnlBottom.TabIndex = 4;
            // 
            // btnDong
            // 
            this.btnDong.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnDong.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnDong.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.btnDong.Location = new System.Drawing.Point(920, 10);
            this.btnDong.Name = "btnDong";
            this.btnDong.Size = new System.Drawing.Size(95, 35);
            this.btnDong.TabIndex = 2;
            this.btnDong.Text = "Đóng";
            this.btnDong.UseVisualStyleBackColor = true;
            this.btnDong.Click += new System.EventHandler(this.btnDong_Click);
            // 
            // 
            // btnHoanThanh
            // 
            this.btnHoanThanh.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.btnHoanThanh.Location = new System.Drawing.Point(245, 10);
            this.btnHoanThanh.Name = "btnHoanThanh";
            this.btnHoanThanh.Size = new System.Drawing.Size(110, 35);
            this.btnHoanThanh.TabIndex = 3;
            this.btnHoanThanh.Text = "Hoàn thành";
            this.btnHoanThanh.UseVisualStyleBackColor = true;
            this.btnHoanThanh.Visible = false;
            this.btnHoanThanh.Click += new System.EventHandler(this.btnHoanThanh_Click);
            // 
            // btnXuatFile
            // 
            this.btnXuatFile.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.btnXuatFile.Location = new System.Drawing.Point(135, 10);
            this.btnXuatFile.Name = "btnXuatFile";
            this.btnXuatFile.Size = new System.Drawing.Size(100, 35);
            this.btnXuatFile.TabIndex = 1;
            this.btnXuatFile.Text = "Xuất file";
            this.btnXuatFile.UseVisualStyleBackColor = true;
            this.btnXuatFile.Click += new System.EventHandler(this.btnXuatFile_Click);
            // 
            // btnXoa
            // 
            this.btnXoa.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.btnXoa.Location = new System.Drawing.Point(20, 10);
            this.btnXoa.Name = "btnXoa";
            this.btnXoa.Size = new System.Drawing.Size(100, 35);
            this.btnXoa.TabIndex = 0;
            this.btnXoa.Text = "Xóa";
            this.btnXoa.UseVisualStyleBackColor = true;
            this.btnXoa.Click += new System.EventHandler(this.btnXoa_Click);
            // 
            // frmChiTietTraHangNhap
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;

            this.CancelButton = this.btnDong;
            this.ClientSize = new System.Drawing.Size(1034, 720);
            this.Controls.Add(this.dgvChiTiet);
            this.Controls.Add(this.pnlTongKet);
            this.Controls.Add(this.pnlBottom);
            this.Controls.Add(this.pnlThongTin);
            this.Controls.Add(this.pnlTop);
            this.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            this.Margin = new System.Windows.Forms.Padding(4);
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(1000, 680);
            this.Name = "frmChiTietTraHangNhap";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Chi tiết đơn trả hàng nhập";
            this.Load += new System.EventHandler(this.frmChiTietTraHangNhap_Load);
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
        private System.Windows.Forms.Label lblKhoChiNhanh;
        private System.Windows.Forms.Label lblTrangThai;
        private System.Windows.Forms.Label lblMaPhieuHienThi;
        private System.Windows.Forms.Panel pnlThongTin;
        private System.Windows.Forms.Label lblMaNhapHangVal;
        private System.Windows.Forms.Label lblMaNhapHang;
        private System.Windows.Forms.Label lblTenNccVal;
        private System.Windows.Forms.Label lblTenNcc;
        private System.Windows.Forms.Label lblNgayTraVal;
        private System.Windows.Forms.Label lblNgayTra;
        private System.Windows.Forms.Label lblNguoiTraVal;
        private System.Windows.Forms.Label lblNguoiTra;
        private System.Windows.Forms.Label lblNguoiTaoVal;
        private System.Windows.Forms.Label lblNguoiTao;
        private System.Windows.Forms.DataGridView dgvChiTiet;
        private System.Windows.Forms.DataGridViewTextBoxColumn colStt;
        private System.Windows.Forms.DataGridViewTextBoxColumn colMaHangNoiBo;
        private System.Windows.Forms.DataGridViewTextBoxColumn colTenHang;
        private System.Windows.Forms.DataGridViewTextBoxColumn colDonViTinh;
        private System.Windows.Forms.DataGridViewTextBoxColumn colSoLuong;
        private System.Windows.Forms.DataGridViewTextBoxColumn colGiaNhap;
        private System.Windows.Forms.DataGridViewTextBoxColumn colDonGiaTra;
        private System.Windows.Forms.DataGridViewTextBoxColumn colThanhTien;
        private System.Windows.Forms.Panel pnlTongKet;
        private System.Windows.Forms.TextBox txtGhiChu;
        private System.Windows.Forms.Label lblGhiChu;
        private System.Windows.Forms.Label lblDaThuNccVal;
        private System.Windows.Forms.Label lblDaThuNcc;
        private System.Windows.Forms.Label lblCanThuNccVal;
        private System.Windows.Forms.Label lblCanThuNcc;
        private System.Windows.Forms.Label lblGiamGiaVal;
        private System.Windows.Forms.Label lblGiamGia;
        private System.Windows.Forms.Label lblTongTienHangVal;
        private System.Windows.Forms.Label lblTongTienHang;
        private System.Windows.Forms.Label lblSoLuongMatHangVal;
        private System.Windows.Forms.Label lblSoLuongMatHang;
        private System.Windows.Forms.Panel pnlBottom;
        private System.Windows.Forms.Button btnDong;
        private System.Windows.Forms.Button btnXuatFile;
        private System.Windows.Forms.Button btnHoanThanh;
        private System.Windows.Forms.Button btnXoa;
    }
}
