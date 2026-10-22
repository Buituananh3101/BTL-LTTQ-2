namespace BTL_LTTQ_2.GUI.Forms.NhapHang
{
    partial class frmThemPhieuNhap
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
            this.grpThongTinChung = new System.Windows.Forms.GroupBox();
            this.txtGhiChu = new System.Windows.Forms.TextBox();
            this.lblGhiChu = new System.Windows.Forms.Label();
            this.txtSoHoaDon = new System.Windows.Forms.TextBox();
            this.lblSoHoaDon = new System.Windows.Forms.Label();
            this.cboNguoiNhap = new System.Windows.Forms.ComboBox();
            this.lblNguoiNhap = new System.Windows.Forms.Label();
            this.txtMaPhieu = new System.Windows.Forms.TextBox();
            this.lblMaPhieu = new System.Windows.Forms.Label();
            this.dtpNgayNhap = new System.Windows.Forms.DateTimePicker();
            this.lblNgayNhap = new System.Windows.Forms.Label();
            this.cboKho = new System.Windows.Forms.ComboBox();
            this.lblKho = new System.Windows.Forms.Label();
            this.cboNhaCungCap = new System.Windows.Forms.ComboBox();
            this.lblNhaCungCap = new System.Windows.Forms.Label();
            this.grpHangHoa = new System.Windows.Forms.GroupBox();
            this.btnThemHang = new System.Windows.Forms.Button();
            this.txtDonGiaNhap = new System.Windows.Forms.TextBox();
            this.lblDonGia = new System.Windows.Forms.Label();
            this.txtSoLuongNhap = new System.Windows.Forms.NumericUpDown();
            this.lblSoLuong = new System.Windows.Forms.Label();
            this.cboHangHoa = new System.Windows.Forms.ComboBox();
            this.lblChonHang = new System.Windows.Forms.Label();
            this.dgvChiTietHang = new System.Windows.Forms.DataGridView();
            this.colStt = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colMaHang = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colMaHangNoiBo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colTenHang = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colDonViTinh = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colSoLuong = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colDonGia = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colGiamGia = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colThanhTien = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colXoa = new System.Windows.Forms.DataGridViewButtonColumn();
            this.grpThanhToan = new System.Windows.Forms.GroupBox();
            this.txtCanTraNcc = new System.Windows.Forms.TextBox();
            this.lblCanTraNcc = new System.Windows.Forms.Label();
            this.txtTienDaTra = new System.Windows.Forms.TextBox();
            this.lblTienDaTra = new System.Windows.Forms.Label();
            this.txtTongCong = new System.Windows.Forms.TextBox();
            this.lblTongCong = new System.Windows.Forms.Label();
            this.txtGiamGiaTong = new System.Windows.Forms.TextBox();
            this.lblGiamGiaTong = new System.Windows.Forms.Label();
            this.txtTongTienHang = new System.Windows.Forms.TextBox();
            this.lblTongTienHang = new System.Windows.Forms.Label();
            this.lblTongSoLuong = new System.Windows.Forms.Label();
            this.lblSoMatHang = new System.Windows.Forms.Label();
            this.pnlBottom = new System.Windows.Forms.Panel();
            this.btnHoanThanh = new System.Windows.Forms.Button();
            this.btnLuuTam = new System.Windows.Forms.Button();
            this.btnBoQua = new System.Windows.Forms.Button();
            this.pnlTop.SuspendLayout();
            this.grpThongTinChung.SuspendLayout();
            this.grpHangHoa.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtSoLuongNhap)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvChiTietHang)).BeginInit();
            this.grpThanhToan.SuspendLayout();
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
            this.pnlTop.Size = new System.Drawing.Size(1104, 52);
            this.pnlTop.TabIndex = 0;
            // 
            // lblTieuDe
            // 
            this.lblTieuDe.AutoSize = true;
            this.lblTieuDe.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.lblTieuDe.Location = new System.Drawing.Point(20, 15);
            this.lblTieuDe.Name = "lblTieuDe";
            this.lblTieuDe.Size = new System.Drawing.Size(193, 21);
            this.lblTieuDe.TabIndex = 0;
            this.lblTieuDe.Text = "Thêm phiếu nhập hàng";
            // 
            // grpThongTinChung
            // 
            this.grpThongTinChung.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.grpThongTinChung.Controls.Add(this.txtGhiChu);
            this.grpThongTinChung.Controls.Add(this.lblGhiChu);
            this.grpThongTinChung.Controls.Add(this.txtSoHoaDon);
            this.grpThongTinChung.Controls.Add(this.lblSoHoaDon);
            this.grpThongTinChung.Controls.Add(this.cboNguoiNhap);
            this.grpThongTinChung.Controls.Add(this.lblNguoiNhap);
            this.grpThongTinChung.Controls.Add(this.txtMaPhieu);
            this.grpThongTinChung.Controls.Add(this.lblMaPhieu);
            this.grpThongTinChung.Controls.Add(this.dtpNgayNhap);
            this.grpThongTinChung.Controls.Add(this.lblNgayNhap);
            this.grpThongTinChung.Controls.Add(this.cboKho);
            this.grpThongTinChung.Controls.Add(this.lblKho);
            this.grpThongTinChung.Controls.Add(this.cboNhaCungCap);
            this.grpThongTinChung.Controls.Add(this.lblNhaCungCap);
            this.grpThongTinChung.Location = new System.Drawing.Point(12, 58);
            this.grpThongTinChung.Name = "grpThongTinChung";
            this.grpThongTinChung.Size = new System.Drawing.Size(1080, 142);
            this.grpThongTinChung.TabIndex = 1;
            this.grpThongTinChung.TabStop = false;
            this.grpThongTinChung.Text = "Thông tin phiếu nhập";
            // 
            // txtGhiChu
            // 
            this.txtGhiChu.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtGhiChu.Location = new System.Drawing.Point(820, 58);
            this.txtGhiChu.Name = "txtGhiChu";
            this.txtGhiChu.Size = new System.Drawing.Size(242, 25);
            this.txtGhiChu.TabIndex = 13;
            // 
            // lblGhiChu
            // 
            this.lblGhiChu.AutoSize = true;
            this.lblGhiChu.Location = new System.Drawing.Point(745, 62);
            this.lblGhiChu.Name = "lblGhiChu";
            this.lblGhiChu.Size = new System.Drawing.Size(51, 17);
            this.lblGhiChu.TabIndex = 12;
            this.lblGhiChu.Text = "Ghi chú";
            // 
            // txtSoHoaDon
            // 
            this.txtSoHoaDon.Location = new System.Drawing.Point(495, 96);
            this.txtSoHoaDon.Name = "txtSoHoaDon";
            this.txtSoHoaDon.Size = new System.Drawing.Size(230, 25);
            this.txtSoHoaDon.TabIndex = 11;
            // 
            // lblSoHoaDon
            // 
            this.lblSoHoaDon.AutoSize = true;
            this.lblSoHoaDon.Location = new System.Drawing.Point(385, 100);
            this.lblSoHoaDon.Name = "lblSoHoaDon";
            this.lblSoHoaDon.Size = new System.Drawing.Size(76, 17);
            this.lblSoHoaDon.TabIndex = 10;
            this.lblSoHoaDon.Text = "Số hóa đơn";
            // 
            // cboNguoiNhap
            // 
            this.cboNguoiNhap.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboNguoiNhap.FormattingEnabled = true;
            this.cboNguoiNhap.Location = new System.Drawing.Point(495, 58);
            this.cboNguoiNhap.Name = "cboNguoiNhap";
            this.cboNguoiNhap.Size = new System.Drawing.Size(230, 25);
            this.cboNguoiNhap.TabIndex = 9;
            // 
            // lblNguoiNhap
            // 
            this.lblNguoiNhap.AutoSize = true;
            this.lblNguoiNhap.Location = new System.Drawing.Point(385, 62);
            this.lblNguoiNhap.Name = "lblNguoiNhap";
            this.lblNguoiNhap.Size = new System.Drawing.Size(95, 17);
            this.lblNguoiNhap.TabIndex = 8;
            this.lblNguoiNhap.Text = "Người nhập (*)";
            // 
            // txtMaPhieu
            // 
            this.txtMaPhieu.Location = new System.Drawing.Point(495, 22);
            this.txtMaPhieu.Name = "txtMaPhieu";
            this.txtMaPhieu.Size = new System.Drawing.Size(230, 25);
            this.txtMaPhieu.TabIndex = 7;
            // 
            // lblMaPhieu
            // 
            this.lblMaPhieu.AutoSize = true;
            this.lblMaPhieu.Location = new System.Drawing.Point(385, 26);
            this.lblMaPhieu.Name = "lblMaPhieu";
            this.lblMaPhieu.Size = new System.Drawing.Size(62, 17);
            this.lblMaPhieu.TabIndex = 6;
            this.lblMaPhieu.Text = "Mã phiếu";
            // 
            // dtpNgayNhap
            // 
            this.dtpNgayNhap.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dtpNgayNhap.CustomFormat = "dd/MM/yyyy HH:mm";
            this.dtpNgayNhap.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpNgayNhap.Location = new System.Drawing.Point(820, 22);
            this.dtpNgayNhap.Name = "dtpNgayNhap";
            this.dtpNgayNhap.Size = new System.Drawing.Size(242, 25);
            this.dtpNgayNhap.TabIndex = 5;
            // 
            // lblNgayNhap
            // 
            this.lblNgayNhap.AutoSize = true;
            this.lblNgayNhap.Location = new System.Drawing.Point(745, 26);
            this.lblNgayNhap.Name = "lblNgayNhap";
            this.lblNgayNhap.Size = new System.Drawing.Size(72, 17);
            this.lblNgayNhap.TabIndex = 4;
            this.lblNgayNhap.Text = "Ngày nhập";
            // 
            // cboKho
            // 
            this.cboKho.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboKho.FormattingEnabled = true;
            this.cboKho.Location = new System.Drawing.Point(135, 58);
            this.cboKho.Name = "cboKho";
            this.cboKho.Size = new System.Drawing.Size(230, 25);
            this.cboKho.TabIndex = 3;
            // 
            // lblKho
            // 
            this.lblKho.AutoSize = true;
            this.lblKho.Location = new System.Drawing.Point(18, 62);
            this.lblKho.Name = "lblKho";
            this.lblKho.Size = new System.Drawing.Size(79, 17);
            this.lblKho.TabIndex = 2;
            this.lblKho.Text = "Kho nhập (*)";
            // 
            // cboNhaCungCap
            // 
            this.cboNhaCungCap.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboNhaCungCap.FormattingEnabled = true;
            this.cboNhaCungCap.Location = new System.Drawing.Point(135, 22);
            this.cboNhaCungCap.Name = "cboNhaCungCap";
            this.cboNhaCungCap.Size = new System.Drawing.Size(230, 25);
            this.cboNhaCungCap.TabIndex = 1;
            // 
            // lblNhaCungCap
            // 
            this.lblNhaCungCap.AutoSize = true;
            this.lblNhaCungCap.Location = new System.Drawing.Point(18, 26);
            this.lblNhaCungCap.Name = "lblNhaCungCap";
            this.lblNhaCungCap.Size = new System.Drawing.Size(107, 17);
            this.lblNhaCungCap.TabIndex = 0;
            this.lblNhaCungCap.Text = "Nhà cung cấp (*)";
            // 
            // grpHangHoa
            // 
            this.grpHangHoa.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.grpHangHoa.Controls.Add(this.btnThemHang);
            this.grpHangHoa.Controls.Add(this.txtDonGiaNhap);
            this.grpHangHoa.Controls.Add(this.lblDonGia);
            this.grpHangHoa.Controls.Add(this.txtSoLuongNhap);
            this.grpHangHoa.Controls.Add(this.lblSoLuong);
            this.grpHangHoa.Controls.Add(this.cboHangHoa);
            this.grpHangHoa.Controls.Add(this.lblChonHang);
            this.grpHangHoa.Controls.Add(this.dgvChiTietHang);
            this.grpHangHoa.Location = new System.Drawing.Point(12, 206);
            this.grpHangHoa.Name = "grpHangHoa";
            this.grpHangHoa.Size = new System.Drawing.Size(1080, 310);
            this.grpHangHoa.TabIndex = 2;
            this.grpHangHoa.TabStop = false;
            this.grpHangHoa.Text = "Danh sách hàng nhập";
            // 
            // btnThemHang
            // 
            this.btnThemHang.Location = new System.Drawing.Point(860, 23);
            this.btnThemHang.Name = "btnThemHang";
            this.btnThemHang.Size = new System.Drawing.Size(120, 28);
            this.btnThemHang.TabIndex = 7;
            this.btnThemHang.Text = "+ Thêm hàng";
            this.btnThemHang.UseVisualStyleBackColor = true;
            this.btnThemHang.Click += new System.EventHandler(this.btnThemHang_Click);
            // 
            // txtDonGiaNhap
            // 
            this.txtDonGiaNhap.Location = new System.Drawing.Point(715, 25);
            this.txtDonGiaNhap.Name = "txtDonGiaNhap";
            this.txtDonGiaNhap.Size = new System.Drawing.Size(130, 25);
            this.txtDonGiaNhap.TabIndex = 6;
            this.txtDonGiaNhap.Text = "0";
            this.txtDonGiaNhap.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // lblDonGia
            // 
            this.lblDonGia.AutoSize = true;
            this.lblDonGia.Location = new System.Drawing.Point(650, 29);
            this.lblDonGia.Name = "lblDonGia";
            this.lblDonGia.Size = new System.Drawing.Size(57, 17);
            this.lblDonGia.TabIndex = 5;
            this.lblDonGia.Text = "Đơn giá:";
            // 
            // txtSoLuongNhap
            // 
            this.txtSoLuongNhap.DecimalPlaces = 2;
            this.txtSoLuongNhap.Location = new System.Drawing.Point(545, 25);
            this.txtSoLuongNhap.Maximum = new decimal(new int[] {
            1000000,
            0,
            0,
            0});
            this.txtSoLuongNhap.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            131072});
            this.txtSoLuongNhap.Name = "txtSoLuongNhap";
            this.txtSoLuongNhap.Size = new System.Drawing.Size(90, 25);
            this.txtSoLuongNhap.TabIndex = 4;
            this.txtSoLuongNhap.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtSoLuongNhap.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // lblSoLuong
            // 
            this.lblSoLuong.AutoSize = true;
            this.lblSoLuong.Location = new System.Drawing.Point(475, 29);
            this.lblSoLuong.Name = "lblSoLuong";
            this.lblSoLuong.Size = new System.Drawing.Size(64, 17);
            this.lblSoLuong.TabIndex = 3;
            this.lblSoLuong.Text = "Số lượng:";
            // 
            // cboHangHoa
            // 
            this.cboHangHoa.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.cboHangHoa.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cboHangHoa.FormattingEnabled = true;
            this.cboHangHoa.Location = new System.Drawing.Point(135, 25);
            this.cboHangHoa.Name = "cboHangHoa";
            this.cboHangHoa.Size = new System.Drawing.Size(325, 25);
            this.cboHangHoa.TabIndex = 2;
            this.cboHangHoa.SelectedIndexChanged += new System.EventHandler(this.cboHangHoa_SelectedIndexChanged);
            // 
            // lblChonHang
            // 
            this.lblChonHang.AutoSize = true;
            this.lblChonHang.Location = new System.Drawing.Point(18, 29);
            this.lblChonHang.Name = "lblChonHang";
            this.lblChonHang.Size = new System.Drawing.Size(102, 17);
            this.lblChonHang.TabIndex = 1;
            this.lblChonHang.Text = "Chọn hàng hóa:";
            // 
            // dgvChiTietHang
            // 
            this.dgvChiTietHang.AllowUserToAddRows = false;
            this.dgvChiTietHang.AllowUserToDeleteRows = false;
            this.dgvChiTietHang.AllowUserToResizeRows = false;
            this.dgvChiTietHang.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvChiTietHang.BackgroundColor = System.Drawing.Color.White;
            this.dgvChiTietHang.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.dgvChiTietHang.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvChiTietHang.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colStt,
            this.colMaHang,
            this.colMaHangNoiBo,
            this.colTenHang,
            this.colDonViTinh,
            this.colSoLuong,
            this.colDonGia,
            this.colGiamGia,
            this.colThanhTien,
            this.colXoa});
            this.dgvChiTietHang.Location = new System.Drawing.Point(12, 60);
            this.dgvChiTietHang.MultiSelect = false;
            this.dgvChiTietHang.Name = "dgvChiTietHang";
            this.dgvChiTietHang.RowHeadersVisible = false;
            this.dgvChiTietHang.RowTemplate.Height = 32;
            this.dgvChiTietHang.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvChiTietHang.Size = new System.Drawing.Size(1055, 238);
            this.dgvChiTietHang.TabIndex = 0;
            this.dgvChiTietHang.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvChiTietHang_CellContentClick);
            this.dgvChiTietHang.CellValueChanged += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvChiTietHang_CellValueChanged);
            // 
            // colStt
            // 
            this.colStt.HeaderText = "STT";
            this.colStt.Name = "colStt";
            this.colStt.ReadOnly = true;
            this.colStt.Width = 45;
            // 
            // colMaHang
            // 
            this.colMaHang.HeaderText = "MaHang";
            this.colMaHang.Name = "colMaHang";
            this.colMaHang.ReadOnly = true;
            this.colMaHang.Visible = false;
            // 
            // colMaHangNoiBo
            // 
            this.colMaHangNoiBo.HeaderText = "Mã hàng";
            this.colMaHangNoiBo.Name = "colMaHangNoiBo";
            this.colMaHangNoiBo.ReadOnly = true;
            this.colMaHangNoiBo.Width = 110;
            // 
            // colTenHang
            // 
            this.colTenHang.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.colTenHang.HeaderText = "Tên hàng hóa";
            this.colTenHang.Name = "colTenHang";
            this.colTenHang.ReadOnly = true;
            // 
            // colDonViTinh
            // 
            this.colDonViTinh.HeaderText = "ĐVT";
            this.colDonViTinh.Name = "colDonViTinh";
            this.colDonViTinh.ReadOnly = true;
            this.colDonViTinh.Width = 75;
            // 
            // colSoLuong
            // 
            this.colSoLuong.HeaderText = "Số lượng";
            this.colSoLuong.Name = "colSoLuong";
            this.colSoLuong.Width = 90;
            // 
            // colDonGia
            // 
            this.colDonGia.HeaderText = "Đơn giá nhập";
            this.colDonGia.Name = "colDonGia";
            this.colDonGia.Width = 120;
            // 
            // colGiamGia
            // 
            this.colGiamGia.HeaderText = "Giảm giá";
            this.colGiamGia.Name = "colGiamGia";
            this.colGiamGia.Width = 100;
            // 
            // colThanhTien
            // 
            this.colThanhTien.HeaderText = "Thành tiền";
            this.colThanhTien.Name = "colThanhTien";
            this.colThanhTien.ReadOnly = true;
            this.colThanhTien.Width = 130;
            // 
            // colXoa
            // 
            this.colXoa.HeaderText = "Xóa";
            this.colXoa.Name = "colXoa";
            this.colXoa.Text = "✕";
            this.colXoa.UseColumnTextForButtonValue = true;
            this.colXoa.Width = 50;
            // 
            // grpThanhToan
            // 
            this.grpThanhToan.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.grpThanhToan.Controls.Add(this.txtCanTraNcc);
            this.grpThanhToan.Controls.Add(this.lblCanTraNcc);
            this.grpThanhToan.Controls.Add(this.txtTienDaTra);
            this.grpThanhToan.Controls.Add(this.lblTienDaTra);
            this.grpThanhToan.Controls.Add(this.txtTongCong);
            this.grpThanhToan.Controls.Add(this.lblTongCong);
            this.grpThanhToan.Controls.Add(this.txtGiamGiaTong);
            this.grpThanhToan.Controls.Add(this.lblGiamGiaTong);
            this.grpThanhToan.Controls.Add(this.txtTongTienHang);
            this.grpThanhToan.Controls.Add(this.lblTongTienHang);
            this.grpThanhToan.Controls.Add(this.lblTongSoLuong);
            this.grpThanhToan.Controls.Add(this.lblSoMatHang);
            this.grpThanhToan.Location = new System.Drawing.Point(12, 522);
            this.grpThanhToan.Name = "grpThanhToan";
            this.grpThanhToan.Size = new System.Drawing.Size(1080, 105);
            this.grpThanhToan.TabIndex = 3;
            this.grpThanhToan.TabStop = false;
            this.grpThanhToan.Text = "Tổng kết thanh toán";
            // 
            // txtCanTraNcc
            // 
            this.txtCanTraNcc.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.txtCanTraNcc.ForeColor = System.Drawing.Color.Red;
            this.txtCanTraNcc.Location = new System.Drawing.Point(890, 60);
            this.txtCanTraNcc.Name = "txtCanTraNcc";
            this.txtCanTraNcc.ReadOnly = true;
            this.txtCanTraNcc.Size = new System.Drawing.Size(160, 25);
            this.txtCanTraNcc.TabIndex = 11;
            this.txtCanTraNcc.Text = "0";
            this.txtCanTraNcc.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // lblCanTraNcc
            // 
            this.lblCanTraNcc.AutoSize = true;
            this.lblCanTraNcc.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.lblCanTraNcc.Location = new System.Drawing.Point(790, 64);
            this.lblCanTraNcc.Name = "lblCanTraNcc";
            this.lblCanTraNcc.Size = new System.Drawing.Size(89, 17);
            this.lblCanTraNcc.TabIndex = 10;
            this.lblCanTraNcc.Text = "Cần trả NCC:";
            // 
            // txtTienDaTra
            // 
            this.txtTienDaTra.Location = new System.Drawing.Point(890, 22);
            this.txtTienDaTra.Name = "txtTienDaTra";
            this.txtTienDaTra.Size = new System.Drawing.Size(160, 25);
            this.txtTienDaTra.TabIndex = 9;
            this.txtTienDaTra.Text = "0";
            this.txtTienDaTra.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtTienDaTra.TextChanged += new System.EventHandler(this.txtTienDaTra_TextChanged);
            // 
            // lblTienDaTra
            // 
            this.lblTienDaTra.AutoSize = true;
            this.lblTienDaTra.Location = new System.Drawing.Point(790, 26);
            this.lblTienDaTra.Name = "lblTienDaTra";
            this.lblTienDaTra.Size = new System.Drawing.Size(99, 17);
            this.lblTienDaTra.TabIndex = 8;
            this.lblTienDaTra.Text = "Tiền đã trả NCC:";
            // 
            // txtTongCong
            // 
            this.txtTongCong.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.txtTongCong.Location = new System.Drawing.Point(600, 60);
            this.txtTongCong.Name = "txtTongCong";
            this.txtTongCong.ReadOnly = true;
            this.txtTongCong.Size = new System.Drawing.Size(160, 25);
            this.txtTongCong.TabIndex = 7;
            this.txtTongCong.Text = "0";
            this.txtTongCong.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // lblTongCong
            // 
            this.lblTongCong.AutoSize = true;
            this.lblTongCong.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.lblTongCong.Location = new System.Drawing.Point(500, 64);
            this.lblTongCong.Name = "lblTongCong";
            this.lblTongCong.Size = new System.Drawing.Size(78, 17);
            this.lblTongCong.TabIndex = 6;
            this.lblTongCong.Text = "Tổng cộng:";
            // 
            // txtGiamGiaTong
            // 
            this.txtGiamGiaTong.Location = new System.Drawing.Point(600, 22);
            this.txtGiamGiaTong.Name = "txtGiamGiaTong";
            this.txtGiamGiaTong.Size = new System.Drawing.Size(160, 25);
            this.txtGiamGiaTong.TabIndex = 5;
            this.txtGiamGiaTong.Text = "0";
            this.txtGiamGiaTong.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtGiamGiaTong.TextChanged += new System.EventHandler(this.txtGiamGiaTong_TextChanged);
            // 
            // lblGiamGiaTong
            // 
            this.lblGiamGiaTong.AutoSize = true;
            this.lblGiamGiaTong.Location = new System.Drawing.Point(500, 26);
            this.lblGiamGiaTong.Name = "lblGiamGiaTong";
            this.lblGiamGiaTong.Size = new System.Drawing.Size(63, 17);
            this.lblGiamGiaTong.TabIndex = 4;
            this.lblGiamGiaTong.Text = "Giảm giá:";
            // 
            // txtTongTienHang
            // 
            this.txtTongTienHang.Location = new System.Drawing.Point(315, 22);
            this.txtTongTienHang.Name = "txtTongTienHang";
            this.txtTongTienHang.ReadOnly = true;
            this.txtTongTienHang.Size = new System.Drawing.Size(155, 25);
            this.txtTongTienHang.TabIndex = 3;
            this.txtTongTienHang.Text = "0";
            this.txtTongTienHang.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // lblTongTienHang
            // 
            this.lblTongTienHang.AutoSize = true;
            this.lblTongTienHang.Location = new System.Drawing.Point(210, 26);
            this.lblTongTienHang.Name = "lblTongTienHang";
            this.lblTongTienHang.Size = new System.Drawing.Size(99, 17);
            this.lblTongTienHang.TabIndex = 2;
            this.lblTongTienHang.Text = "Tổng tiền hàng:";
            // 
            // lblTongSoLuong
            // 
            this.lblTongSoLuong.AutoSize = true;
            this.lblTongSoLuong.Location = new System.Drawing.Point(18, 64);
            this.lblTongSoLuong.Name = "lblTongSoLuong";
            this.lblTongSoLuong.Size = new System.Drawing.Size(109, 17);
            this.lblTongSoLuong.TabIndex = 1;
            this.lblTongSoLuong.Text = "Tổng số lượng: 0";
            // 
            // lblSoMatHang
            // 
            this.lblSoMatHang.AutoSize = true;
            this.lblSoMatHang.Location = new System.Drawing.Point(18, 26);
            this.lblSoMatHang.Name = "lblSoMatHang";
            this.lblSoMatHang.Size = new System.Drawing.Size(95, 17);
            this.lblSoMatHang.TabIndex = 0;
            this.lblSoMatHang.Text = "Số mặt hàng: 0";
            // 
            // pnlBottom
            // 
            this.pnlBottom.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(250)))), ((int)(((byte)(252)))));
            this.pnlBottom.Controls.Add(this.btnHoanThanh);
            this.pnlBottom.Controls.Add(this.btnLuuTam);
            this.pnlBottom.Controls.Add(this.btnBoQua);
            this.pnlBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnlBottom.Location = new System.Drawing.Point(0, 638);
            this.pnlBottom.Name = "pnlBottom";
            this.pnlBottom.Size = new System.Drawing.Size(1104, 52);
            this.pnlBottom.TabIndex = 4;
            // 
            // btnHoanThanh
            // 
            this.btnHoanThanh.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnHoanThanh.Location = new System.Drawing.Point(972, 11);
            this.btnHoanThanh.Name = "btnHoanThanh";
            this.btnHoanThanh.Size = new System.Drawing.Size(120, 30);
            this.btnHoanThanh.TabIndex = 2;
            this.btnHoanThanh.Text = "Hoàn thành";
            this.btnHoanThanh.UseVisualStyleBackColor = true;
            this.btnHoanThanh.Click += new System.EventHandler(this.btnHoanThanh_Click);
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
            this.btnBoQua.Location = new System.Drawing.Point(751, 11);
            this.btnBoQua.Name = "btnBoQua";
            this.btnBoQua.Size = new System.Drawing.Size(90, 30);
            this.btnBoQua.TabIndex = 0;
            this.btnBoQua.Text = "Bỏ qua";
            this.btnBoQua.UseVisualStyleBackColor = true;
            this.btnBoQua.Click += new System.EventHandler(this.btnBoQua_Click);
            // 
            // frmThemPhieuNhap
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(1104, 690);
            this.Controls.Add(this.pnlBottom);
            this.Controls.Add(this.grpThanhToan);
            this.Controls.Add(this.grpHangHoa);
            this.Controls.Add(this.grpThongTinChung);
            this.Controls.Add(this.pnlTop);
            this.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.MinimumSize = new System.Drawing.Size(950, 650);
            this.Name = "frmThemPhieuNhap";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Thêm phiếu nhập hàng";
            this.Load += new System.EventHandler(this.frmThemPhieuNhap_Load);
            this.pnlTop.ResumeLayout(false);
            this.pnlTop.PerformLayout();
            this.grpThongTinChung.ResumeLayout(false);
            this.grpThongTinChung.PerformLayout();
            this.grpHangHoa.ResumeLayout(false);
            this.grpHangHoa.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtSoLuongNhap)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvChiTietHang)).EndInit();
            this.grpThanhToan.ResumeLayout(false);
            this.grpThanhToan.PerformLayout();
            this.pnlBottom.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pnlTop;
        private System.Windows.Forms.Label lblTieuDe;
        private System.Windows.Forms.GroupBox grpThongTinChung;
        private System.Windows.Forms.ComboBox cboNhaCungCap;
        private System.Windows.Forms.Label lblNhaCungCap;
        private System.Windows.Forms.ComboBox cboKho;
        private System.Windows.Forms.Label lblKho;
        private System.Windows.Forms.DateTimePicker dtpNgayNhap;
        private System.Windows.Forms.Label lblNgayNhap;
        private System.Windows.Forms.TextBox txtMaPhieu;
        private System.Windows.Forms.Label lblMaPhieu;
        private System.Windows.Forms.ComboBox cboNguoiNhap;
        private System.Windows.Forms.Label lblNguoiNhap;
        private System.Windows.Forms.TextBox txtSoHoaDon;
        private System.Windows.Forms.Label lblSoHoaDon;
        private System.Windows.Forms.TextBox txtGhiChu;
        private System.Windows.Forms.Label lblGhiChu;
        private System.Windows.Forms.GroupBox grpHangHoa;
        private System.Windows.Forms.ComboBox cboHangHoa;
        private System.Windows.Forms.Label lblChonHang;
        private System.Windows.Forms.NumericUpDown txtSoLuongNhap;
        private System.Windows.Forms.Label lblSoLuong;
        private System.Windows.Forms.TextBox txtDonGiaNhap;
        private System.Windows.Forms.Label lblDonGia;
        private System.Windows.Forms.Button btnThemHang;
        private System.Windows.Forms.DataGridView dgvChiTietHang;
        private System.Windows.Forms.GroupBox grpThanhToan;
        private System.Windows.Forms.Label lblSoMatHang;
        private System.Windows.Forms.Label lblTongSoLuong;
        private System.Windows.Forms.TextBox txtTongTienHang;
        private System.Windows.Forms.Label lblTongTienHang;
        private System.Windows.Forms.TextBox txtGiamGiaTong;
        private System.Windows.Forms.Label lblGiamGiaTong;
        private System.Windows.Forms.TextBox txtTongCong;
        private System.Windows.Forms.Label lblTongCong;
        private System.Windows.Forms.TextBox txtTienDaTra;
        private System.Windows.Forms.Label lblTienDaTra;
        private System.Windows.Forms.TextBox txtCanTraNcc;
        private System.Windows.Forms.Label lblCanTraNcc;
        private System.Windows.Forms.Panel pnlBottom;
        private System.Windows.Forms.Button btnBoQua;
        private System.Windows.Forms.Button btnLuuTam;
        private System.Windows.Forms.Button btnHoanThanh;
        private System.Windows.Forms.DataGridViewTextBoxColumn colStt;
        private System.Windows.Forms.DataGridViewTextBoxColumn colMaHang;
        private System.Windows.Forms.DataGridViewTextBoxColumn colMaHangNoiBo;
        private System.Windows.Forms.DataGridViewTextBoxColumn colTenHang;
        private System.Windows.Forms.DataGridViewTextBoxColumn colDonViTinh;
        private System.Windows.Forms.DataGridViewTextBoxColumn colSoLuong;
        private System.Windows.Forms.DataGridViewTextBoxColumn colDonGia;
        private System.Windows.Forms.DataGridViewTextBoxColumn colGiamGia;
        private System.Windows.Forms.DataGridViewTextBoxColumn colThanhTien;
        private System.Windows.Forms.DataGridViewButtonColumn colXoa;
    }
}
