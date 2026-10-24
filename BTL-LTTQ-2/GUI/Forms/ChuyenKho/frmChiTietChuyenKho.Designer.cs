namespace BTL_LTTQ_2.GUI.Forms.ChuyenKho
{
    partial class frmChiTietChuyenKho
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            this.pnlTop = new System.Windows.Forms.Panel();
            this.lblLoTrinh = new System.Windows.Forms.Label();
            this.lblTrangThai = new System.Windows.Forms.Label();
            this.lblMaPhieuHienThi = new System.Windows.Forms.Label();
            this.pnlThongTin = new System.Windows.Forms.Panel();
            this.lblThoiDiemNhanVal = new System.Windows.Forms.Label();
            this.lblThoiDiemNhan = new System.Windows.Forms.Label();
            this.lblNguoiNhanVal = new System.Windows.Forms.Label();
            this.lblNguoiNhan = new System.Windows.Forms.Label();
            this.lblThoiDiemXuatVal = new System.Windows.Forms.Label();
            this.lblThoiDiemXuat = new System.Windows.Forms.Label();
            this.lblNguoiXuatVal = new System.Windows.Forms.Label();
            this.lblNguoiXuat = new System.Windows.Forms.Label();
            this.lblNgayLapVal = new System.Windows.Forms.Label();
            this.lblNgayLap = new System.Windows.Forms.Label();
            this.lblNguoiLapVal = new System.Windows.Forms.Label();
            this.lblNguoiLap = new System.Windows.Forms.Label();
            this.lblKhoDichVal = new System.Windows.Forms.Label();
            this.lblKhoDich = new System.Windows.Forms.Label();
            this.lblKhoNguonVal = new System.Windows.Forms.Label();
            this.lblKhoNguon = new System.Windows.Forms.Label();
            this.dgvChiTiet = new System.Windows.Forms.DataGridView();
            this.colStt = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colMaHang = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colTenHang = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colDonViTinh = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colSoLuongChuyen = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colSoLuongNhan = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colHaoHut = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colDonGia = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colThanhTien = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.pnlTongKet = new System.Windows.Forms.Panel();
            this.lblGhiChuNhanVal = new System.Windows.Forms.Label();
            this.lblGhiChuNhan = new System.Windows.Forms.Label();
            this.lblGhiChuChuyenVal = new System.Windows.Forms.Label();
            this.lblGhiChuChuyen = new System.Windows.Forms.Label();
            this.lblTongGiaTriVal = new System.Windows.Forms.Label();
            this.lblTongGiaTri = new System.Windows.Forms.Label();
            this.lblTongHaoHutVal = new System.Windows.Forms.Label();
            this.lblTongHaoHut = new System.Windows.Forms.Label();
            this.lblTongSlNhanVal = new System.Windows.Forms.Label();
            this.lblTongSlNhan = new System.Windows.Forms.Label();
            this.lblTongSlChuyenVal = new System.Windows.Forms.Label();
            this.lblTongSlChuyen = new System.Windows.Forms.Label();
            this.pnlBottom = new System.Windows.Forms.Panel();
            this.btnNhanHang = new System.Windows.Forms.Button();
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
            this.pnlTop.Controls.Add(this.lblLoTrinh);
            this.pnlTop.Controls.Add(this.lblTrangThai);
            this.pnlTop.Controls.Add(this.lblMaPhieuHienThi);
            this.pnlTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlTop.Location = new System.Drawing.Point(0, 0);
            this.pnlTop.Name = "pnlTop";
            this.pnlTop.Size = new System.Drawing.Size(1034, 55);
            this.pnlTop.TabIndex = 0;
            // 
            // lblLoTrinh
            // 
            this.lblLoTrinh.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblLoTrinh.AutoSize = true;
            this.lblLoTrinh.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.lblLoTrinh.Location = new System.Drawing.Point(730, 20);
            this.lblLoTrinh.Name = "lblLoTrinh";
            this.lblLoTrinh.Size = new System.Drawing.Size(130, 17);
            this.lblLoTrinh.TabIndex = 2;
            this.lblLoTrinh.Text = "Kho gửi ➜ Kho nhận";
            // 
            // lblTrangThai
            // 
            this.lblTrangThai.AutoSize = true;
            this.lblTrangThai.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.lblTrangThai.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(22)))), ((int)(((byte)(163)))), ((int)(((byte)(74)))));
            this.lblTrangThai.Location = new System.Drawing.Point(180, 20);
            this.lblTrangThai.Name = "lblTrangThai";
            this.lblTrangThai.Size = new System.Drawing.Size(61, 19);
            this.lblTrangThai.TabIndex = 1;
            this.lblTrangThai.Text = "Đã nhận";
            // 
            // lblMaPhieuHienThi
            // 
            this.lblMaPhieuHienThi.AutoSize = true;
            this.lblMaPhieuHienThi.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.lblMaPhieuHienThi.Location = new System.Drawing.Point(16, 17);
            this.lblMaPhieuHienThi.Name = "lblMaPhieuHienThi";
            this.lblMaPhieuHienThi.Size = new System.Drawing.Size(84, 21);
            this.lblMaPhieuHienThi.TabIndex = 0;
            this.lblMaPhieuHienThi.Text = "CK000000";
            // 
            // pnlThongTin
            // 
            this.pnlThongTin.BackColor = System.Drawing.Color.White;
            this.pnlThongTin.Controls.Add(this.lblThoiDiemNhanVal);
            this.pnlThongTin.Controls.Add(this.lblThoiDiemNhan);
            this.pnlThongTin.Controls.Add(this.lblNguoiNhanVal);
            this.pnlThongTin.Controls.Add(this.lblNguoiNhan);
            this.pnlThongTin.Controls.Add(this.lblThoiDiemXuatVal);
            this.pnlThongTin.Controls.Add(this.lblThoiDiemXuat);
            this.pnlThongTin.Controls.Add(this.lblNguoiXuatVal);
            this.pnlThongTin.Controls.Add(this.lblNguoiXuat);
            this.pnlThongTin.Controls.Add(this.lblNgayLapVal);
            this.pnlThongTin.Controls.Add(this.lblNgayLap);
            this.pnlThongTin.Controls.Add(this.lblNguoiLapVal);
            this.pnlThongTin.Controls.Add(this.lblNguoiLap);
            this.pnlThongTin.Controls.Add(this.lblKhoDichVal);
            this.pnlThongTin.Controls.Add(this.lblKhoDich);
            this.pnlThongTin.Controls.Add(this.lblKhoNguonVal);
            this.pnlThongTin.Controls.Add(this.lblKhoNguon);
            this.pnlThongTin.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlThongTin.Location = new System.Drawing.Point(0, 55);
            this.pnlThongTin.Name = "pnlThongTin";
            this.pnlThongTin.Size = new System.Drawing.Size(1034, 100);
            this.pnlThongTin.TabIndex = 1;
            // 
            // lblThoiDiemNhanVal
            // 
            this.lblThoiDiemNhanVal.AutoSize = true;
            this.lblThoiDiemNhanVal.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            this.lblThoiDiemNhanVal.Location = new System.Drawing.Point(835, 55);
            this.lblThoiDiemNhanVal.Name = "lblThoiDiemNhanVal";
            this.lblThoiDiemNhanVal.Size = new System.Drawing.Size(23, 17);
            this.lblThoiDiemNhanVal.TabIndex = 15;
            this.lblThoiDiemNhanVal.Text = "---";
            // 
            // lblThoiDiemNhan
            // 
            this.lblThoiDiemNhan.AutoSize = true;
            this.lblThoiDiemNhan.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            this.lblThoiDiemNhan.Location = new System.Drawing.Point(735, 55);
            this.lblThoiDiemNhan.Name = "lblThoiDiemNhan";
            this.lblThoiDiemNhan.Size = new System.Drawing.Size(74, 17);
            this.lblThoiDiemNhan.TabIndex = 14;
            this.lblThoiDiemNhan.Text = "Ngày nhận:";
            // 
            // lblNguoiNhanVal
            // 
            this.lblNguoiNhanVal.AutoSize = true;
            this.lblNguoiNhanVal.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            this.lblNguoiNhanVal.Location = new System.Drawing.Point(835, 20);
            this.lblNguoiNhanVal.Name = "lblNguoiNhanVal";
            this.lblNguoiNhanVal.Size = new System.Drawing.Size(23, 17);
            this.lblNguoiNhanVal.TabIndex = 13;
            this.lblNguoiNhanVal.Text = "---";
            // 
            // lblNguoiNhan
            // 
            this.lblNguoiNhan.AutoSize = true;
            this.lblNguoiNhan.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            this.lblNguoiNhan.Location = new System.Drawing.Point(735, 20);
            this.lblNguoiNhan.Name = "lblNguoiNhan";
            this.lblNguoiNhan.Size = new System.Drawing.Size(80, 17);
            this.lblNguoiNhan.TabIndex = 12;
            this.lblNguoiNhan.Text = "Người nhận:";
            // 
            // lblThoiDiemXuatVal
            // 
            this.lblThoiDiemXuatVal.AutoSize = true;
            this.lblThoiDiemXuatVal.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            this.lblThoiDiemXuatVal.Location = new System.Drawing.Point(595, 55);
            this.lblThoiDiemXuatVal.Name = "lblThoiDiemXuatVal";
            this.lblThoiDiemXuatVal.Size = new System.Drawing.Size(23, 17);
            this.lblThoiDiemXuatVal.TabIndex = 11;
            this.lblThoiDiemXuatVal.Text = "---";
            // 
            // lblThoiDiemXuat
            // 
            this.lblThoiDiemXuat.AutoSize = true;
            this.lblThoiDiemXuat.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            this.lblThoiDiemXuat.Location = new System.Drawing.Point(505, 55);
            this.lblThoiDiemXuat.Name = "lblThoiDiemXuat";
            this.lblThoiDiemXuat.Size = new System.Drawing.Size(70, 17);
            this.lblThoiDiemXuat.TabIndex = 10;
            this.lblThoiDiemXuat.Text = "Ngày xuất:";
            // 
            // lblNguoiXuatVal
            // 
            this.lblNguoiXuatVal.AutoSize = true;
            this.lblNguoiXuatVal.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            this.lblNguoiXuatVal.Location = new System.Drawing.Point(595, 20);
            this.lblNguoiXuatVal.Name = "lblNguoiXuatVal";
            this.lblNguoiXuatVal.Size = new System.Drawing.Size(23, 17);
            this.lblNguoiXuatVal.TabIndex = 9;
            this.lblNguoiXuatVal.Text = "---";
            // 
            // lblNguoiXuat
            // 
            this.lblNguoiXuat.AutoSize = true;
            this.lblNguoiXuat.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            this.lblNguoiXuat.Location = new System.Drawing.Point(505, 20);
            this.lblNguoiXuat.Name = "lblNguoiXuat";
            this.lblNguoiXuat.Size = new System.Drawing.Size(76, 17);
            this.lblNguoiXuat.TabIndex = 8;
            this.lblNguoiXuat.Text = "Người xuất:";
            // 
            // lblNgayLapVal
            // 
            this.lblNgayLapVal.AutoSize = true;
            this.lblNgayLapVal.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            this.lblNgayLapVal.Location = new System.Drawing.Point(345, 55);
            this.lblNgayLapVal.Name = "lblNgayLapVal";
            this.lblNgayLapVal.Size = new System.Drawing.Size(23, 17);
            this.lblNgayLapVal.TabIndex = 7;
            this.lblNgayLapVal.Text = "---";
            // 
            // lblNgayLap
            // 
            this.lblNgayLap.AutoSize = true;
            this.lblNgayLap.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            this.lblNgayLap.Location = new System.Drawing.Point(265, 55);
            this.lblNgayLap.Name = "lblNgayLap";
            this.lblNgayLap.Size = new System.Drawing.Size(64, 17);
            this.lblNgayLap.TabIndex = 6;
            this.lblNgayLap.Text = "Ngày lập:";
            // 
            // lblNguoiLapVal
            // 
            this.lblNguoiLapVal.AutoSize = true;
            this.lblNguoiLapVal.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            this.lblNguoiLapVal.Location = new System.Drawing.Point(345, 20);
            this.lblNguoiLapVal.Name = "lblNguoiLapVal";
            this.lblNguoiLapVal.Size = new System.Drawing.Size(23, 17);
            this.lblNguoiLapVal.TabIndex = 5;
            this.lblNguoiLapVal.Text = "---";
            // 
            // lblNguoiLap
            // 
            this.lblNguoiLap.AutoSize = true;
            this.lblNguoiLap.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            this.lblNguoiLap.Location = new System.Drawing.Point(265, 20);
            this.lblNguoiLap.Name = "lblNguoiLap";
            this.lblNguoiLap.Size = new System.Drawing.Size(70, 17);
            this.lblNguoiLap.TabIndex = 4;
            this.lblNguoiLap.Text = "Người lập:";
            // 
            // lblKhoDichVal
            // 
            this.lblKhoDichVal.AutoSize = true;
            this.lblKhoDichVal.Location = new System.Drawing.Point(85, 55);
            this.lblKhoDichVal.Name = "lblKhoDichVal";
            this.lblKhoDichVal.Size = new System.Drawing.Size(23, 17);
            this.lblKhoDichVal.TabIndex = 3;
            this.lblKhoDichVal.Text = "---";
            // 
            // lblKhoDich
            // 
            this.lblKhoDich.AutoSize = true;
            this.lblKhoDich.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            this.lblKhoDich.Location = new System.Drawing.Point(16, 55);
            this.lblKhoDich.Name = "lblKhoDich";
            this.lblKhoDich.Size = new System.Drawing.Size(66, 17);
            this.lblKhoDich.TabIndex = 2;
            this.lblKhoDich.Text = "Kho nhận:";
            // 
            // lblKhoNguonVal
            // 
            this.lblKhoNguonVal.AutoSize = true;
            this.lblKhoNguonVal.Location = new System.Drawing.Point(85, 20);
            this.lblKhoNguonVal.Name = "lblKhoNguonVal";
            this.lblKhoNguonVal.Size = new System.Drawing.Size(23, 17);
            this.lblKhoNguonVal.TabIndex = 1;
            this.lblKhoNguonVal.Text = "---";
            // 
            // lblKhoNguon
            // 
            this.lblKhoNguon.AutoSize = true;
            this.lblKhoNguon.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            this.lblKhoNguon.Location = new System.Drawing.Point(16, 20);
            this.lblKhoNguon.Name = "lblKhoNguon";
            this.lblKhoNguon.Size = new System.Drawing.Size(57, 17);
            this.lblKhoNguon.TabIndex = 0;
            this.lblKhoNguon.Text = "Kho gửi:";
            // 
            // dgvChiTiet
            // 
            this.dgvChiTiet.AllowUserToAddRows = false;
            this.dgvChiTiet.AllowUserToDeleteRows = false;
            this.dgvChiTiet.BackgroundColor = System.Drawing.Color.White;
            this.dgvChiTiet.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvChiTiet.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colStt,
            this.colMaHang,
            this.colTenHang,
            this.colDonViTinh,
            this.colSoLuongChuyen,
            this.colSoLuongNhan,
            this.colHaoHut,
            this.colDonGia,
            this.colThanhTien});
            this.dgvChiTiet.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvChiTiet.Location = new System.Drawing.Point(0, 155);
            this.dgvChiTiet.Name = "dgvChiTiet";
            this.dgvChiTiet.ReadOnly = true;
            this.dgvChiTiet.RowHeadersVisible = false;
            this.dgvChiTiet.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvChiTiet.Size = new System.Drawing.Size(1034, 380);
            this.dgvChiTiet.TabIndex = 2;
            this.dgvChiTiet.CellFormatting += new System.Windows.Forms.DataGridViewCellFormattingEventHandler(this.dgvChiTiet_CellFormatting);
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
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle4.Format = "N0";
            this.colSoLuongChuyen.DefaultCellStyle = dataGridViewCellStyle4;
            this.colSoLuongChuyen.HeaderText = "SL chuyển";
            this.colSoLuongChuyen.Name = "colSoLuongChuyen";
            this.colSoLuongChuyen.ReadOnly = true;
            // 
            // colSoLuongNhan
            // 
            this.colSoLuongNhan.DataPropertyName = "SoLuongNhanHienThi";
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.colSoLuongNhan.DefaultCellStyle = dataGridViewCellStyle5;
            this.colSoLuongNhan.HeaderText = "SL thực nhận";
            this.colSoLuongNhan.Name = "colSoLuongNhan";
            this.colSoLuongNhan.ReadOnly = true;
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
            // colDonGia
            // 
            this.colDonGia.DataPropertyName = "DonGiaChuyen";
            dataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle6.Format = "N0";
            this.colDonGia.DefaultCellStyle = dataGridViewCellStyle6;
            this.colDonGia.HeaderText = "Đơn giá chuyển";
            this.colDonGia.Name = "colDonGia";
            this.colDonGia.ReadOnly = true;
            this.colDonGia.Width = 120;
            // 
            // colThanhTien
            // 
            this.colThanhTien.DataPropertyName = "ThanhTien";
            this.colThanhTien.HeaderText = "Thành tiền";
            this.colThanhTien.Name = "colThanhTien";
            this.colThanhTien.ReadOnly = true;
            this.colThanhTien.Width = 130;
            // 
            // pnlTongKet
            // 
            this.pnlTongKet.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(250)))), ((int)(((byte)(252)))));
            this.pnlTongKet.Controls.Add(this.lblGhiChuNhanVal);
            this.pnlTongKet.Controls.Add(this.lblGhiChuNhan);
            this.pnlTongKet.Controls.Add(this.lblGhiChuChuyenVal);
            this.pnlTongKet.Controls.Add(this.lblGhiChuChuyen);
            this.pnlTongKet.Controls.Add(this.lblTongGiaTriVal);
            this.pnlTongKet.Controls.Add(this.lblTongGiaTri);
            this.pnlTongKet.Controls.Add(this.lblTongHaoHutVal);
            this.pnlTongKet.Controls.Add(this.lblTongHaoHut);
            this.pnlTongKet.Controls.Add(this.lblTongSlNhanVal);
            this.pnlTongKet.Controls.Add(this.lblTongSlNhan);
            this.pnlTongKet.Controls.Add(this.lblTongSlChuyenVal);
            this.pnlTongKet.Controls.Add(this.lblTongSlChuyen);
            this.pnlTongKet.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnlTongKet.Location = new System.Drawing.Point(0, 535);
            this.pnlTongKet.Name = "pnlTongKet";
            this.pnlTongKet.Size = new System.Drawing.Size(1034, 90);
            this.pnlTongKet.TabIndex = 3;
            // 
            // lblGhiChuNhanVal
            // 
            this.lblGhiChuNhanVal.AutoSize = true;
            this.lblGhiChuNhanVal.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            this.lblGhiChuNhanVal.Location = new System.Drawing.Point(125, 50);
            this.lblGhiChuNhanVal.Name = "lblGhiChuNhanVal";
            this.lblGhiChuNhanVal.Size = new System.Drawing.Size(48, 17);
            this.lblGhiChuNhanVal.TabIndex = 11;
            this.lblGhiChuNhanVal.Text = "(trống)";
            // 
            // lblGhiChuNhan
            // 
            this.lblGhiChuNhan.AutoSize = true;
            this.lblGhiChuNhan.Location = new System.Drawing.Point(16, 50);
            this.lblGhiChuNhan.Name = "lblGhiChuNhan";
            this.lblGhiChuNhan.Size = new System.Drawing.Size(86, 17);
            this.lblGhiChuNhan.TabIndex = 10;
            this.lblGhiChuNhan.Text = "Ghi chú nhận:";
            // 
            // lblGhiChuChuyenVal
            // 
            this.lblGhiChuChuyenVal.AutoSize = true;
            this.lblGhiChuChuyenVal.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            this.lblGhiChuChuyenVal.Location = new System.Drawing.Point(125, 18);
            this.lblGhiChuChuyenVal.Name = "lblGhiChuChuyenVal";
            this.lblGhiChuChuyenVal.Size = new System.Drawing.Size(48, 17);
            this.lblGhiChuChuyenVal.TabIndex = 9;
            this.lblGhiChuChuyenVal.Text = "(trống)";
            // 
            // lblGhiChuChuyen
            // 
            this.lblGhiChuChuyen.AutoSize = true;
            this.lblGhiChuChuyen.Location = new System.Drawing.Point(16, 18);
            this.lblGhiChuChuyen.Name = "lblGhiChuChuyen";
            this.lblGhiChuChuyen.Size = new System.Drawing.Size(98, 17);
            this.lblGhiChuChuyen.TabIndex = 8;
            this.lblGhiChuChuyen.Text = "Ghi chú chuyển:";
            // 
            // lblTongGiaTriVal
            // 
            this.lblTongGiaTriVal.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblTongGiaTriVal.AutoSize = true;
            this.lblTongGiaTriVal.Location = new System.Drawing.Point(895, 50);
            this.lblTongGiaTriVal.Name = "lblTongGiaTriVal";
            this.lblTongGiaTriVal.Size = new System.Drawing.Size(46, 17);
            this.lblTongGiaTriVal.TabIndex = 7;
            this.lblTongGiaTriVal.Text = "0 VNĐ";
            // 
            // lblTongGiaTri
            // 
            this.lblTongGiaTri.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblTongGiaTri.AutoSize = true;
            this.lblTongGiaTri.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            this.lblTongGiaTri.Location = new System.Drawing.Point(790, 52);
            this.lblTongGiaTri.Name = "lblTongGiaTri";
            this.lblTongGiaTri.Size = new System.Drawing.Size(79, 17);
            this.lblTongGiaTri.TabIndex = 6;
            this.lblTongGiaTri.Text = "Tổng giá trị:";
            // 
            // lblTongHaoHutVal
            // 
            this.lblTongHaoHutVal.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblTongHaoHutVal.AutoSize = true;
            this.lblTongHaoHutVal.Location = new System.Drawing.Point(895, 18);
            this.lblTongHaoHutVal.Name = "lblTongHaoHutVal";
            this.lblTongHaoHutVal.Size = new System.Drawing.Size(15, 17);
            this.lblTongHaoHutVal.TabIndex = 5;
            this.lblTongHaoHutVal.Text = "0";
            // 
            // lblTongHaoHut
            // 
            this.lblTongHaoHut.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblTongHaoHut.AutoSize = true;
            this.lblTongHaoHut.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            this.lblTongHaoHut.Location = new System.Drawing.Point(790, 18);
            this.lblTongHaoHut.Name = "lblTongHaoHut";
            this.lblTongHaoHut.Size = new System.Drawing.Size(89, 17);
            this.lblTongHaoHut.TabIndex = 4;
            this.lblTongHaoHut.Text = "Tổng hao hụt:";
            // 
            // lblTongSlNhanVal
            // 
            this.lblTongSlNhanVal.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblTongSlNhanVal.AutoSize = true;
            this.lblTongSlNhanVal.Location = new System.Drawing.Point(690, 52);
            this.lblTongSlNhanVal.Name = "lblTongSlNhanVal";
            this.lblTongSlNhanVal.Size = new System.Drawing.Size(15, 17);
            this.lblTongSlNhanVal.TabIndex = 3;
            this.lblTongSlNhanVal.Text = "0";
            // 
            // lblTongSlNhan
            // 
            this.lblTongSlNhan.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblTongSlNhan.AutoSize = true;
            this.lblTongSlNhan.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            this.lblTongSlNhan.Location = new System.Drawing.Point(580, 52);
            this.lblTongSlNhan.Name = "lblTongSlNhan";
            this.lblTongSlNhan.Size = new System.Drawing.Size(102, 17);
            this.lblTongSlNhan.TabIndex = 2;
            this.lblTongSlNhan.Text = "Tổng thực nhận:";
            // 
            // lblTongSlChuyenVal
            // 
            this.lblTongSlChuyenVal.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblTongSlChuyenVal.AutoSize = true;
            this.lblTongSlChuyenVal.Location = new System.Drawing.Point(690, 18);
            this.lblTongSlChuyenVal.Name = "lblTongSlChuyenVal";
            this.lblTongSlChuyenVal.Size = new System.Drawing.Size(15, 17);
            this.lblTongSlChuyenVal.TabIndex = 1;
            this.lblTongSlChuyenVal.Text = "0";
            // 
            // lblTongSlChuyen
            // 
            this.lblTongSlChuyen.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblTongSlChuyen.AutoSize = true;
            this.lblTongSlChuyen.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            this.lblTongSlChuyen.Location = new System.Drawing.Point(580, 18);
            this.lblTongSlChuyen.Name = "lblTongSlChuyen";
            this.lblTongSlChuyen.Size = new System.Drawing.Size(102, 17);
            this.lblTongSlChuyen.TabIndex = 0;
            this.lblTongSlChuyen.Text = "Tổng SL chuyển:";
            // 
            // pnlBottom
            // 
            this.pnlBottom.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(250)))), ((int)(((byte)(252)))));
            this.pnlBottom.Controls.Add(this.btnNhanHang);
            this.pnlBottom.Controls.Add(this.btnDong);
            this.pnlBottom.Controls.Add(this.btnXuatFile);
            this.pnlBottom.Controls.Add(this.btnHoanThanh);
            this.pnlBottom.Controls.Add(this.btnXoa);
            this.pnlBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnlBottom.Location = new System.Drawing.Point(0, 625);
            this.pnlBottom.Name = "pnlBottom";
            this.pnlBottom.Size = new System.Drawing.Size(1034, 55);
            this.pnlBottom.TabIndex = 4;
            // 
            // btnNhanHang
            // 
            this.btnNhanHang.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnNhanHang.Location = new System.Drawing.Point(372, 12);
            this.btnNhanHang.Name = "btnNhanHang";
            this.btnNhanHang.Size = new System.Drawing.Size(110, 32);
            this.btnNhanHang.TabIndex = 0;
            this.btnNhanHang.Text = "Nhận hàng";
            this.btnNhanHang.UseVisualStyleBackColor = true;
            this.btnNhanHang.Visible = false;
            this.btnNhanHang.Click += new System.EventHandler(this.btnNhanHang_Click);
            // 
            // btnDong
            // 
            this.btnDong.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnDong.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnDong.Location = new System.Drawing.Point(927, 12);
            this.btnDong.Name = "btnDong";
            this.btnDong.Size = new System.Drawing.Size(95, 32);
            this.btnDong.TabIndex = 3;
            this.btnDong.Text = "Đóng";
            this.btnDong.UseVisualStyleBackColor = true;
            this.btnDong.Click += new System.EventHandler(this.btnDong_Click);
            // 
            // btnXuatFile
            // 
            this.btnXuatFile.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnXuatFile.Location = new System.Drawing.Point(128, 12);
            this.btnXuatFile.Name = "btnXuatFile";
            this.btnXuatFile.Size = new System.Drawing.Size(100, 32);
            this.btnXuatFile.TabIndex = 2;
            this.btnXuatFile.Text = "Xuất file";
            this.btnXuatFile.UseVisualStyleBackColor = true;
            this.btnXuatFile.Click += new System.EventHandler(this.btnXuatFile_Click);
            // 
            // btnHoanThanh
            // 
            this.btnHoanThanh.Location = new System.Drawing.Point(247, 12);
            this.btnHoanThanh.Name = "btnHoanThanh";
            this.btnHoanThanh.Size = new System.Drawing.Size(110, 32);
            this.btnHoanThanh.TabIndex = 4;
            this.btnHoanThanh.Text = "Hoàn thành";
            this.btnHoanThanh.UseVisualStyleBackColor = true;
            this.btnHoanThanh.Visible = false;
            this.btnHoanThanh.Click += new System.EventHandler(this.btnHoanThanh_Click);
            // 
            // btnXoa
            // 
            this.btnXoa.Location = new System.Drawing.Point(16, 12);
            this.btnXoa.Name = "btnXoa";
            this.btnXoa.Size = new System.Drawing.Size(95, 32);
            this.btnXoa.TabIndex = 1;
            this.btnXoa.Text = "Xóa";
            this.btnXoa.UseVisualStyleBackColor = true;
            this.btnXoa.Click += new System.EventHandler(this.btnXoa_Click);
            // 
            // frmChiTietChuyenKho
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnDong;
            this.ClientSize = new System.Drawing.Size(1034, 680);
            this.Controls.Add(this.dgvChiTiet);
            this.Controls.Add(this.pnlTongKet);
            this.Controls.Add(this.pnlBottom);
            this.Controls.Add(this.pnlThongTin);
            this.Controls.Add(this.pnlTop);
            this.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmChiTietChuyenKho";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Chi tiết phiếu chuyển kho";
            this.Load += new System.EventHandler(this.frmChiTietChuyenKho_Load);
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
        private System.Windows.Forms.Label lblLoTrinh;
        private System.Windows.Forms.Label lblTrangThai;
        private System.Windows.Forms.Label lblMaPhieuHienThi;
        private System.Windows.Forms.Panel pnlThongTin;
        private System.Windows.Forms.Label lblKhoNguon;
        private System.Windows.Forms.Label lblKhoNguonVal;
        private System.Windows.Forms.Label lblKhoDich;
        private System.Windows.Forms.Label lblKhoDichVal;
        private System.Windows.Forms.Label lblNguoiLap;
        private System.Windows.Forms.Label lblNguoiLapVal;
        private System.Windows.Forms.Label lblNgayLap;
        private System.Windows.Forms.Label lblNgayLapVal;
        private System.Windows.Forms.Label lblNguoiXuat;
        private System.Windows.Forms.Label lblNguoiXuatVal;
        private System.Windows.Forms.Label lblThoiDiemXuat;
        private System.Windows.Forms.Label lblThoiDiemXuatVal;
        private System.Windows.Forms.Label lblNguoiNhan;
        private System.Windows.Forms.Label lblNguoiNhanVal;
        private System.Windows.Forms.Label lblThoiDiemNhan;
        private System.Windows.Forms.Label lblThoiDiemNhanVal;
        private System.Windows.Forms.DataGridView dgvChiTiet;
        private System.Windows.Forms.Panel pnlTongKet;
        private System.Windows.Forms.Label lblGhiChuChuyen;
        private System.Windows.Forms.Label lblGhiChuChuyenVal;
        private System.Windows.Forms.Label lblGhiChuNhan;
        private System.Windows.Forms.Label lblGhiChuNhanVal;
        private System.Windows.Forms.Label lblTongSlChuyen;
        private System.Windows.Forms.Label lblTongSlChuyenVal;
        private System.Windows.Forms.Label lblTongSlNhan;
        private System.Windows.Forms.Label lblTongSlNhanVal;
        private System.Windows.Forms.Label lblTongHaoHut;
        private System.Windows.Forms.Label lblTongHaoHutVal;
        private System.Windows.Forms.Label lblTongGiaTri;
        private System.Windows.Forms.Label lblTongGiaTriVal;
        private System.Windows.Forms.Panel pnlBottom;
        private System.Windows.Forms.Button btnNhanHang;
        private System.Windows.Forms.Button btnDong;
        private System.Windows.Forms.Button btnXuatFile;
        private System.Windows.Forms.Button btnHoanThanh;
        private System.Windows.Forms.Button btnXoa;
        private System.Windows.Forms.DataGridViewTextBoxColumn colStt;
        private System.Windows.Forms.DataGridViewTextBoxColumn colMaHang;
        private System.Windows.Forms.DataGridViewTextBoxColumn colTenHang;
        private System.Windows.Forms.DataGridViewTextBoxColumn colDonViTinh;
        private System.Windows.Forms.DataGridViewTextBoxColumn colSoLuongChuyen;
        private System.Windows.Forms.DataGridViewTextBoxColumn colSoLuongNhan;
        private System.Windows.Forms.DataGridViewTextBoxColumn colHaoHut;
        private System.Windows.Forms.DataGridViewTextBoxColumn colDonGia;
        private System.Windows.Forms.DataGridViewTextBoxColumn colThanhTien;
    }
}
