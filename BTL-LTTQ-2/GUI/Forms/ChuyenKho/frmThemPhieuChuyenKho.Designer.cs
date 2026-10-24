namespace BTL_LTTQ_2.GUI.Forms.ChuyenKho
{
    partial class frmThemPhieuChuyenKho
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            this.pnlTop = new System.Windows.Forms.Panel();
            this.lblTieuDe = new System.Windows.Forms.Label();
            this.pnlThongTin = new System.Windows.Forms.Panel();
            this.txtGhiChu = new System.Windows.Forms.TextBox();
            this.lblGhiChu = new System.Windows.Forms.Label();
            this.dtpNgayChuyen = new System.Windows.Forms.DateTimePicker();
            this.lblNgayChuyen = new System.Windows.Forms.Label();
            this.lblNguoiChuyenVal = new System.Windows.Forms.Label();
            this.lblNguoiChuyen = new System.Windows.Forms.Label();
            this.cboKhoDich = new System.Windows.Forms.ComboBox();
            this.lblKhoDich = new System.Windows.Forms.Label();
            this.cboKhoNguon = new System.Windows.Forms.ComboBox();
            this.lblKhoNguon = new System.Windows.Forms.Label();
            this.pnlThemHang = new System.Windows.Forms.Panel();
            this.btnNhapFile = new System.Windows.Forms.Button();
            this.btnThemHang = new System.Windows.Forms.Button();
            this.nudSoLuong = new System.Windows.Forms.NumericUpDown();
            this.lblSoLuong = new System.Windows.Forms.Label();
            this.lblTonKhoGui = new System.Windows.Forms.Label();
            this.cboHangHoa = new System.Windows.Forms.ComboBox();
            this.lblHangHoa = new System.Windows.Forms.Label();
            this.dgvChiTiet = new System.Windows.Forms.DataGridView();
            this.colStt = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colMaHang = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colTenHang = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colDonViTinh = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colTonKho = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colSoLuong = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colDonGia = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colThanhTien = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colXoa = new System.Windows.Forms.DataGridViewButtonColumn();
            this.pnlTongKet = new System.Windows.Forms.Panel();
            this.lblTongGiaTri = new System.Windows.Forms.Label();
            this.lblTongSoLuong = new System.Windows.Forms.Label();
            this.lblSoMatHang = new System.Windows.Forms.Label();
            this.pnlBottom = new System.Windows.Forms.Panel();
            this.btnLuu = new System.Windows.Forms.Button();
            this.btnLuuTam = new System.Windows.Forms.Button();
            this.btnBoQua = new System.Windows.Forms.Button();
            this.pnlTop.SuspendLayout();
            this.pnlThongTin.SuspendLayout();
            this.pnlThemHang.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudSoLuong)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvChiTiet)).BeginInit();
            this.pnlTongKet.SuspendLayout();
            this.pnlBottom.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlTop
            // 
            this.pnlTop.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(250)))), ((int)(((byte)(252)))));
            this.pnlTop.Controls.Add(this.lblTieuDe);
            this.pnlTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlTop.Location = new System.Drawing.Point(0, 0);
            this.pnlTop.Name = "pnlTop";
            this.pnlTop.Size = new System.Drawing.Size(1104, 50);
            this.pnlTop.TabIndex = 0;
            // 
            // lblTieuDe
            // 
            this.lblTieuDe.AutoSize = true;
            this.lblTieuDe.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.lblTieuDe.Location = new System.Drawing.Point(14, 13);
            this.lblTieuDe.Name = "lblTieuDe";
            this.lblTieuDe.Size = new System.Drawing.Size(185, 21);
            this.lblTieuDe.TabIndex = 0;
            this.lblTieuDe.Text = "Thêm phiếu chuyển kho";
            // 
            // pnlThongTin
            // 
            this.pnlThongTin.BackColor = System.Drawing.Color.White;
            this.pnlThongTin.Controls.Add(this.txtGhiChu);
            this.pnlThongTin.Controls.Add(this.lblGhiChu);
            this.pnlThongTin.Controls.Add(this.dtpNgayChuyen);
            this.pnlThongTin.Controls.Add(this.lblNgayChuyen);
            this.pnlThongTin.Controls.Add(this.lblNguoiChuyenVal);
            this.pnlThongTin.Controls.Add(this.lblNguoiChuyen);
            this.pnlThongTin.Controls.Add(this.cboKhoDich);
            this.pnlThongTin.Controls.Add(this.lblKhoDich);
            this.pnlThongTin.Controls.Add(this.cboKhoNguon);
            this.pnlThongTin.Controls.Add(this.lblKhoNguon);
            this.pnlThongTin.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlThongTin.Location = new System.Drawing.Point(0, 50);
            this.pnlThongTin.Name = "pnlThongTin";
            this.pnlThongTin.Size = new System.Drawing.Size(1104, 95);
            this.pnlThongTin.TabIndex = 1;
            // 
            // txtGhiChu
            // 
            this.txtGhiChu.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtGhiChu.Location = new System.Drawing.Point(440, 54);
            this.txtGhiChu.Name = "txtGhiChu";
            this.txtGhiChu.Size = new System.Drawing.Size(645, 24);
            this.txtGhiChu.TabIndex = 9;
            // 
            // lblGhiChu
            // 
            this.lblGhiChu.AutoSize = true;
            this.lblGhiChu.Location = new System.Drawing.Point(365, 57);
            this.lblGhiChu.Name = "lblGhiChu";
            this.lblGhiChu.Size = new System.Drawing.Size(54, 17);
            this.lblGhiChu.TabIndex = 8;
            this.lblGhiChu.Text = "Ghi chú:";
            // 
            // dtpNgayChuyen
            // 
            this.dtpNgayChuyen.CustomFormat = "dd/MM/yyyy HH:mm";
            this.dtpNgayChuyen.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpNgayChuyen.Location = new System.Drawing.Point(440, 16);
            this.dtpNgayChuyen.Name = "dtpNgayChuyen";
            this.dtpNgayChuyen.Size = new System.Drawing.Size(160, 24);
            this.dtpNgayChuyen.TabIndex = 5;
            // 
            // lblNgayChuyen
            // 
            this.lblNgayChuyen.AutoSize = true;
            this.lblNgayChuyen.Location = new System.Drawing.Point(365, 20);
            this.lblNgayChuyen.Name = "lblNgayChuyen";
            this.lblNgayChuyen.Size = new System.Drawing.Size(65, 17);
            this.lblNgayChuyen.TabIndex = 4;
            this.lblNgayChuyen.Text = "Ngày lập:";
            // 
            // lblNguoiChuyenVal
            // 
            this.lblNguoiChuyenVal.AutoSize = true;
            this.lblNguoiChuyenVal.Location = new System.Drawing.Point(740, 20);
            this.lblNguoiChuyenVal.Name = "lblNguoiChuyenVal";
            this.lblNguoiChuyenVal.Size = new System.Drawing.Size(117, 17);
            this.lblNguoiChuyenVal.TabIndex = 7;
            this.lblNguoiChuyenVal.Text = "Nguyễn Minh Quản";
            // 
            // lblNguoiChuyen
            // 
            this.lblNguoiChuyen.AutoSize = true;
            this.lblNguoiChuyen.Location = new System.Drawing.Point(650, 20);
            this.lblNguoiChuyen.Name = "lblNguoiChuyen";
            this.lblNguoiChuyen.Size = new System.Drawing.Size(70, 17);
            this.lblNguoiChuyen.TabIndex = 6;
            this.lblNguoiChuyen.Text = "Người lập:";
            // 
            // cboKhoDich
            // 
            this.cboKhoDich.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboKhoDich.FormattingEnabled = true;
            this.cboKhoDich.Location = new System.Drawing.Point(105, 54);
            this.cboKhoDich.Name = "cboKhoDich";
            this.cboKhoDich.Size = new System.Drawing.Size(230, 24);
            this.cboKhoDich.TabIndex = 3;
            // 
            // lblKhoDich
            // 
            this.lblKhoDich.AutoSize = true;
            this.lblKhoDich.Location = new System.Drawing.Point(15, 57);
            this.lblKhoDich.Name = "lblKhoDich";
            this.lblKhoDich.Size = new System.Drawing.Size(84, 17);
            this.lblKhoDich.TabIndex = 2;
            this.lblKhoDich.Text = "Kho nhận (*):";
            // 
            // cboKhoNguon
            // 
            this.cboKhoNguon.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboKhoNguon.FormattingEnabled = true;
            this.cboKhoNguon.Location = new System.Drawing.Point(105, 16);
            this.cboKhoNguon.Name = "cboKhoNguon";
            this.cboKhoNguon.Size = new System.Drawing.Size(230, 24);
            this.cboKhoNguon.TabIndex = 1;
            this.cboKhoNguon.SelectedIndexChanged += new System.EventHandler(this.cboKhoNguon_SelectedIndexChanged);
            // 
            // lblKhoNguon
            // 
            this.lblKhoNguon.AutoSize = true;
            this.lblKhoNguon.Location = new System.Drawing.Point(15, 20);
            this.lblKhoNguon.Name = "lblKhoNguon";
            this.lblKhoNguon.Size = new System.Drawing.Size(56, 17);
            this.lblKhoNguon.TabIndex = 0;
            this.lblKhoNguon.Text = "Kho gửi:";
            // 
            // pnlThemHang
            // 
            this.pnlThemHang.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(241)))), ((int)(((byte)(245)))), ((int)(((byte)(249)))));
            this.pnlThemHang.Controls.Add(this.btnNhapFile);
            this.pnlThemHang.Controls.Add(this.btnThemHang);
            this.pnlThemHang.Controls.Add(this.nudSoLuong);
            this.pnlThemHang.Controls.Add(this.lblSoLuong);
            this.pnlThemHang.Controls.Add(this.lblTonKhoGui);
            this.pnlThemHang.Controls.Add(this.cboHangHoa);
            this.pnlThemHang.Controls.Add(this.lblHangHoa);
            this.pnlThemHang.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlThemHang.Location = new System.Drawing.Point(0, 145);
            this.pnlThemHang.Name = "pnlThemHang";
            this.pnlThemHang.Size = new System.Drawing.Size(1104, 52);
            this.pnlThemHang.TabIndex = 2;
            // 
            // btnNhapFile
            // 
            this.btnNhapFile.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnNhapFile.Location = new System.Drawing.Point(985, 11);
            this.btnNhapFile.Name = "btnNhapFile";
            this.btnNhapFile.Size = new System.Drawing.Size(100, 30);
            this.btnNhapFile.TabIndex = 6;
            this.btnNhapFile.Text = "Nhập file";
            this.btnNhapFile.UseVisualStyleBackColor = true;
            this.btnNhapFile.Click += new System.EventHandler(this.btnNhapFile_Click);
            // 
            // btnThemHang
            // 
            this.btnThemHang.Location = new System.Drawing.Point(745, 11);
            this.btnThemHang.Name = "btnThemHang";
            this.btnThemHang.Size = new System.Drawing.Size(110, 30);
            this.btnThemHang.TabIndex = 5;
            this.btnThemHang.Text = "+ Thêm hàng";
            this.btnThemHang.UseVisualStyleBackColor = true;
            this.btnThemHang.Click += new System.EventHandler(this.btnThemHang_Click);
            // 
            // nudSoLuong
            // 
            this.nudSoLuong.Location = new System.Drawing.Point(645, 15);
            this.nudSoLuong.Maximum = new decimal(new int[] {
            999999,
            0,
            0,
            0});
            this.nudSoLuong.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.nudSoLuong.Name = "nudSoLuong";
            this.nudSoLuong.Size = new System.Drawing.Size(85, 24);
            this.nudSoLuong.TabIndex = 4;
            this.nudSoLuong.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.nudSoLuong.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // lblSoLuong
            // 
            this.lblSoLuong.AutoSize = true;
            this.lblSoLuong.Location = new System.Drawing.Point(575, 18);
            this.lblSoLuong.Name = "lblSoLuong";
            this.lblSoLuong.Size = new System.Drawing.Size(68, 17);
            this.lblSoLuong.TabIndex = 3;
            this.lblSoLuong.Text = "SL chuyển:";
            // 
            // lblTonKhoGui
            // 
            this.lblTonKhoGui.AutoSize = true;
            this.lblTonKhoGui.Location = new System.Drawing.Point(440, 18);
            this.lblTonKhoGui.Name = "lblTonKhoGui";
            this.lblTonKhoGui.Size = new System.Drawing.Size(107, 17);
            this.lblTonKhoGui.TabIndex = 2;
            this.lblTonKhoGui.Text = "Tồn kho gửi: ---";
            // 
            // cboHangHoa
            // 
            this.cboHangHoa.FormattingEnabled = true;
            this.cboHangHoa.Location = new System.Drawing.Point(105, 15);
            this.cboHangHoa.Name = "cboHangHoa";
            this.cboHangHoa.Size = new System.Drawing.Size(320, 24);
            this.cboHangHoa.TabIndex = 1;
            this.cboHangHoa.SelectedIndexChanged += new System.EventHandler(this.cboHangHoa_SelectedIndexChanged);
            // 
            // lblHangHoa
            // 
            this.lblHangHoa.AutoSize = true;
            this.lblHangHoa.Location = new System.Drawing.Point(15, 18);
            this.lblHangHoa.Name = "lblHangHoa";
            this.lblHangHoa.Size = new System.Drawing.Size(73, 17);
            this.lblHangHoa.TabIndex = 0;
            this.lblHangHoa.Text = "Chọn hàng:";
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
            this.colTonKho,
            this.colSoLuong,
            this.colDonGia,
            this.colThanhTien,
            this.colXoa});
            this.dgvChiTiet.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvChiTiet.Location = new System.Drawing.Point(0, 197);
            this.dgvChiTiet.Name = "dgvChiTiet";
            this.dgvChiTiet.RowHeadersVisible = false;
            this.dgvChiTiet.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvChiTiet.Size = new System.Drawing.Size(1104, 381);
            this.dgvChiTiet.TabIndex = 3;
            this.dgvChiTiet.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvChiTiet_CellContentClick);
            this.dgvChiTiet.CellEndEdit += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvChiTiet_CellEndEdit);
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
            // colTonKho
            // 
            this.colTonKho.DataPropertyName = "TonKho";
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle1.Format = "N0";
            this.colTonKho.DefaultCellStyle = dataGridViewCellStyle1;
            this.colTonKho.HeaderText = "Tồn kho gửi";
            this.colTonKho.Name = "colTonKho";
            this.colTonKho.ReadOnly = true;
            this.colTonKho.Width = 110;
            // 
            // colSoLuong
            // 
            this.colSoLuong.DataPropertyName = "SoLuong";
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(204)))));
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold);
            dataGridViewCellStyle2.Format = "N0";
            this.colSoLuong.DefaultCellStyle = dataGridViewCellStyle2;
            this.colSoLuong.HeaderText = "SL chuyển";
            this.colSoLuong.Name = "colSoLuong";
            this.colSoLuong.Width = 110;
            // 
            // colDonGia
            // 
            this.colDonGia.DataPropertyName = "DonGiaChuyen";
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle3.Format = "N0";
            this.colDonGia.DefaultCellStyle = dataGridViewCellStyle3;
            this.colDonGia.HeaderText = "Đơn giá";
            this.colDonGia.Name = "colDonGia";
            this.colDonGia.ReadOnly = true;
            this.colDonGia.Width = 120;
            // 
            // colThanhTien
            // 
            this.colThanhTien.DataPropertyName = "ThanhTien";
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle4.Format = "N0";
            this.colThanhTien.DefaultCellStyle = dataGridViewCellStyle4;
            this.colThanhTien.HeaderText = "Thành tiền";
            this.colThanhTien.Name = "colThanhTien";
            this.colThanhTien.ReadOnly = true;
            this.colThanhTien.Width = 130;
            // 
            // colXoa
            // 
            this.colXoa.HeaderText = "Xóa";
            this.colXoa.Name = "colXoa";
            this.colXoa.Text = "Xóa";
            this.colXoa.UseColumnTextForButtonValue = true;
            this.colXoa.Width = 60;
            // 
            // pnlTongKet
            // 
            this.pnlTongKet.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(250)))), ((int)(((byte)(252)))));
            this.pnlTongKet.Controls.Add(this.lblTongGiaTri);
            this.pnlTongKet.Controls.Add(this.lblTongSoLuong);
            this.pnlTongKet.Controls.Add(this.lblSoMatHang);
            this.pnlTongKet.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnlTongKet.Location = new System.Drawing.Point(0, 578);
            this.pnlTongKet.Name = "pnlTongKet";
            this.pnlTongKet.Size = new System.Drawing.Size(1104, 50);
            this.pnlTongKet.TabIndex = 4;
            // 
            // lblTongGiaTri
            // 
            this.lblTongGiaTri.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblTongGiaTri.AutoSize = true;
            this.lblTongGiaTri.Location = new System.Drawing.Point(820, 15);
            this.lblTongGiaTri.Name = "lblTongGiaTri";
            this.lblTongGiaTri.Size = new System.Drawing.Size(193, 20);
            this.lblTongGiaTri.TabIndex = 2;
            this.lblTongGiaTri.Text = "Tổng giá trị: 0 VNĐ";
            // 
            // lblTongSoLuong
            // 
            this.lblTongSoLuong.AutoSize = true;
            this.lblTongSoLuong.Location = new System.Drawing.Point(220, 17);
            this.lblTongSoLuong.Name = "lblTongSoLuong";
            this.lblTongSoLuong.Size = new System.Drawing.Size(155, 17);
            this.lblTongSoLuong.TabIndex = 1;
            this.lblTongSoLuong.Text = "Tổng số lượng chuyển: 0";
            // 
            // lblSoMatHang
            // 
            this.lblSoMatHang.AutoSize = true;
            this.lblSoMatHang.Location = new System.Drawing.Point(18, 17);
            this.lblSoMatHang.Name = "lblSoMatHang";
            this.lblSoMatHang.Size = new System.Drawing.Size(95, 17);
            this.lblSoMatHang.TabIndex = 0;
            this.lblSoMatHang.Text = "Số mặt hàng: 0";
            // 
            // pnlBottom
            // 
            this.pnlBottom.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(250)))), ((int)(((byte)(252)))));
            this.pnlBottom.Controls.Add(this.btnLuu);
            this.pnlBottom.Controls.Add(this.btnLuuTam);
            this.pnlBottom.Controls.Add(this.btnBoQua);
            this.pnlBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnlBottom.Location = new System.Drawing.Point(0, 628);
            this.pnlBottom.Name = "pnlBottom";
            this.pnlBottom.Size = new System.Drawing.Size(1104, 52);
            this.pnlBottom.TabIndex = 5;
            // 
            // btnLuu
            // 
            this.btnLuu.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnLuu.Location = new System.Drawing.Point(972, 11);
            this.btnLuu.Name = "btnLuu";
            this.btnLuu.Size = new System.Drawing.Size(120, 30);
            this.btnLuu.TabIndex = 2;
            this.btnLuu.Text = "Lưu";
            this.btnLuu.UseVisualStyleBackColor = true;
            this.btnLuu.Click += new System.EventHandler(this.btnLuu_Click);
            // 
            // btnLuuTam
            // 
            this.btnLuuTam.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnLuuTam.Location = new System.Drawing.Point(857, 11);
            this.btnLuuTam.Name = "btnLuuTam";
            this.btnLuuTam.Size = new System.Drawing.Size(100, 30);
            this.btnLuuTam.TabIndex = 1;
            this.btnLuuTam.Text = "Lưu tạm";
            this.btnLuuTam.UseVisualStyleBackColor = true;
            this.btnLuuTam.Click += new System.EventHandler(this.btnLuuTam_Click);
            // 
            // btnBoQua
            // 
            this.btnBoQua.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnBoQua.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnBoQua.Location = new System.Drawing.Point(751, 11);
            this.btnBoQua.Name = "btnBoQua";
            this.btnBoQua.Size = new System.Drawing.Size(90, 30);
            this.btnBoQua.TabIndex = 0;
            this.btnBoQua.Text = "Bỏ qua";
            this.btnBoQua.UseVisualStyleBackColor = true;
            this.btnBoQua.Click += new System.EventHandler(this.btnBoQua_Click);
            // 
            // frmThemPhieuChuyenKho
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnBoQua;
            this.ClientSize = new System.Drawing.Size(1104, 680);
            this.Controls.Add(this.dgvChiTiet);
            this.Controls.Add(this.pnlTongKet);
            this.Controls.Add(this.pnlBottom);
            this.Controls.Add(this.pnlThemHang);
            this.Controls.Add(this.pnlThongTin);
            this.Controls.Add(this.pnlTop);
            this.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmThemPhieuChuyenKho";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Thêm phiếu chuyển kho";
            this.Load += new System.EventHandler(this.frmThemPhieuChuyenKho_Load);
            this.pnlTop.ResumeLayout(false);
            this.pnlTop.PerformLayout();
            this.pnlThongTin.ResumeLayout(false);
            this.pnlThongTin.PerformLayout();
            this.pnlThemHang.ResumeLayout(false);
            this.pnlThemHang.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudSoLuong)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvChiTiet)).EndInit();
            this.pnlTongKet.ResumeLayout(false);
            this.pnlTongKet.PerformLayout();
            this.pnlBottom.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pnlTop;
        private System.Windows.Forms.Label lblTieuDe;
        private System.Windows.Forms.Panel pnlThongTin;
        private System.Windows.Forms.Label lblKhoNguon;
        private System.Windows.Forms.ComboBox cboKhoNguon;
        private System.Windows.Forms.Label lblKhoDich;
        private System.Windows.Forms.ComboBox cboKhoDich;
        private System.Windows.Forms.Label lblNguoiChuyen;
        private System.Windows.Forms.Label lblNguoiChuyenVal;
        private System.Windows.Forms.Label lblNgayChuyen;
        private System.Windows.Forms.DateTimePicker dtpNgayChuyen;
        private System.Windows.Forms.Label lblGhiChu;
        private System.Windows.Forms.TextBox txtGhiChu;
        private System.Windows.Forms.Panel pnlThemHang;
        private System.Windows.Forms.Label lblHangHoa;
        private System.Windows.Forms.ComboBox cboHangHoa;
        private System.Windows.Forms.Label lblTonKhoGui;
        private System.Windows.Forms.Label lblSoLuong;
        private System.Windows.Forms.NumericUpDown nudSoLuong;
        private System.Windows.Forms.Button btnThemHang;
        private System.Windows.Forms.Button btnNhapFile;
        private System.Windows.Forms.DataGridView dgvChiTiet;
        private System.Windows.Forms.Panel pnlTongKet;
        private System.Windows.Forms.Label lblSoMatHang;
        private System.Windows.Forms.Label lblTongSoLuong;
        private System.Windows.Forms.Label lblTongGiaTri;
        private System.Windows.Forms.Panel pnlBottom;
        private System.Windows.Forms.Button btnLuu;
        private System.Windows.Forms.Button btnLuuTam;
        private System.Windows.Forms.Button btnBoQua;
        private System.Windows.Forms.DataGridViewTextBoxColumn colStt;
        private System.Windows.Forms.DataGridViewTextBoxColumn colMaHang;
        private System.Windows.Forms.DataGridViewTextBoxColumn colTenHang;
        private System.Windows.Forms.DataGridViewTextBoxColumn colDonViTinh;
        private System.Windows.Forms.DataGridViewTextBoxColumn colTonKho;
        private System.Windows.Forms.DataGridViewTextBoxColumn colSoLuong;
        private System.Windows.Forms.DataGridViewTextBoxColumn colDonGia;
        private System.Windows.Forms.DataGridViewTextBoxColumn colThanhTien;
        private System.Windows.Forms.DataGridViewButtonColumn colXoa;
    }
}
