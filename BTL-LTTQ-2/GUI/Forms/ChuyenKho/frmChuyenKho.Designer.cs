namespace BTL_LTTQ_2.GUI.Forms.ChuyenKho
{
    partial class frmChuyenKho
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
            this.grpTop = new System.Windows.Forms.GroupBox();
            this.lblTieuDe = new System.Windows.Forms.Label();
            this.btnHuongDanSuDung = new System.Windows.Forms.Button();
            this.btnHienCot = new System.Windows.Forms.Button();
            this.btnXuatFile = new System.Windows.Forms.Button();
            this.btnChuyenKho = new System.Windows.Forms.Button();
            this.lblTimKiem = new System.Windows.Forms.Label();
            this.txtTimKiem = new System.Windows.Forms.TextBox();
            this.pnlBoLoc = new System.Windows.Forms.Panel();
            this.grpBoLoc = new System.Windows.Forms.GroupBox();
            this.lblTrangThai = new System.Windows.Forms.Label();
            this.cboTrangThai = new System.Windows.Forms.ComboBox();
            this.lblHuongChuyen = new System.Windows.Forms.Label();
            this.cboHuongChuyen = new System.Windows.Forms.ComboBox();
            this.lblNguoiTao = new System.Windows.Forms.Label();
            this.cboNguoiTao = new System.Windows.Forms.ComboBox();
            this.lblThoiGianTao = new System.Windows.Forms.Label();
            this.grpThoiGianTao = new System.Windows.Forms.GroupBox();
            this.dtpTuNgay = new System.Windows.Forms.DateTimePicker();
            this.dtpDenNgay = new System.Windows.Forms.DateTimePicker();
            this.lblTuNgay = new System.Windows.Forms.Label();
            this.lblDenNgay = new System.Windows.Forms.Label();
            this.splitterBoLoc = new System.Windows.Forms.Splitter();
            this.pnlPhanTrang = new System.Windows.Forms.Panel();
            this.lblTongSoPhieu = new System.Windows.Forms.Label();
            this.lblSoDongMoiTrang = new System.Windows.Forms.Label();
            this.cboSoDongMoiTrang = new System.Windows.Forms.ComboBox();
            this.btnTrangTruoc = new System.Windows.Forms.Button();
            this.lblTrangHienTai = new System.Windows.Forms.Label();
            this.btnTrangSau = new System.Windows.Forms.Button();
            this.dgvChuyenKho = new System.Windows.Forms.DataGridView();
            this.colChon = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.colMaPhieu = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colNgayLap = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colKhoNguon = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colKhoDich = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colTongSlChuyen = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colTongSlNhan = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colTongGiaTri = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colNguoiTao = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colGhiChu = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colTrangThai = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colMaPhieuChuyen = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.pnlTop.SuspendLayout();
            this.grpTop.SuspendLayout();
            this.pnlBoLoc.SuspendLayout();
            this.grpBoLoc.SuspendLayout();
            this.grpThoiGianTao.SuspendLayout();
            this.pnlPhanTrang.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvChuyenKho)).BeginInit();
            this.SuspendLayout();
            // 
            // pnlTop
            // 
            this.pnlTop.Controls.Add(this.grpTop);
            this.pnlTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlTop.Location = new System.Drawing.Point(0, 0);
            this.pnlTop.Name = "pnlTop";
            this.pnlTop.Size = new System.Drawing.Size(1782, 84);
            this.pnlTop.TabIndex = 0;
            // 
            // grpTop
            // 
            this.grpTop.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.grpTop.Controls.Add(this.lblTieuDe);
            this.grpTop.Controls.Add(this.btnHuongDanSuDung);
            this.grpTop.Controls.Add(this.btnHienCot);
            this.grpTop.Controls.Add(this.btnXuatFile);
            this.grpTop.Controls.Add(this.btnChuyenKho);
            this.grpTop.Controls.Add(this.lblTimKiem);
            this.grpTop.Controls.Add(this.txtTimKiem);
            this.grpTop.Location = new System.Drawing.Point(0, 3);
            this.grpTop.Name = "grpTop";
            this.grpTop.Size = new System.Drawing.Size(1782, 60);
            this.grpTop.TabIndex = 0;
            this.grpTop.TabStop = false;
            // 
            // lblTieuDe
            // 
            this.lblTieuDe.AutoSize = true;
            this.lblTieuDe.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.lblTieuDe.Location = new System.Drawing.Point(12, 23);
            this.lblTieuDe.Name = "lblTieuDe";
            this.lblTieuDe.Size = new System.Drawing.Size(147, 21);
            this.lblTieuDe.TabIndex = 0;
            this.lblTieuDe.Text = "Phiếu chuyển kho";
            // 
            // btnHuongDanSuDung
            // 
            this.btnHuongDanSuDung.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnHuongDanSuDung.Location = new System.Drawing.Point(1736, 21);
            this.btnHuongDanSuDung.Name = "btnHuongDanSuDung";
            this.btnHuongDanSuDung.Size = new System.Drawing.Size(34, 30);
            this.btnHuongDanSuDung.TabIndex = 6;
            this.btnHuongDanSuDung.Text = "?";
            this.btnHuongDanSuDung.UseVisualStyleBackColor = true;
            this.btnHuongDanSuDung.Click += new System.EventHandler(this.btnHuongDanSuDung_Click);
            // 
            // btnHienCot
            // 
            this.btnHienCot.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnHienCot.Location = new System.Drawing.Point(1635, 21);
            this.btnHienCot.Name = "btnHienCot";
            this.btnHienCot.Size = new System.Drawing.Size(95, 30);
            this.btnHienCot.TabIndex = 5;
            this.btnHienCot.Text = "Hiện cột";
            this.btnHienCot.UseVisualStyleBackColor = true;
            this.btnHienCot.Click += new System.EventHandler(this.btnHienCot_Click);
            // 
            // btnXuatFile
            // 
            this.btnXuatFile.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnXuatFile.Location = new System.Drawing.Point(1534, 21);
            this.btnXuatFile.Name = "btnXuatFile";
            this.btnXuatFile.Size = new System.Drawing.Size(95, 30);
            this.btnXuatFile.TabIndex = 4;
            this.btnXuatFile.Text = "Xuất file";
            this.btnXuatFile.UseVisualStyleBackColor = true;
            this.btnXuatFile.Click += new System.EventHandler(this.btnXuatFile_Click);
            // 
            // btnChuyenKho
            // 
            this.btnChuyenKho.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnChuyenKho.Location = new System.Drawing.Point(1385, 21);
            this.btnChuyenKho.Name = "btnChuyenKho";
            this.btnChuyenKho.Size = new System.Drawing.Size(142, 30);
            this.btnChuyenKho.TabIndex = 3;
            this.btnChuyenKho.Text = "+ Chuyển kho";
            this.btnChuyenKho.UseVisualStyleBackColor = true;
            this.btnChuyenKho.Click += new System.EventHandler(this.btnChuyenKho_Click);
            // 
            // lblTimKiem
            // 
            this.lblTimKiem.AutoSize = true;
            this.lblTimKiem.Location = new System.Drawing.Point(385, 25);
            this.lblTimKiem.Name = "lblTimKiem";
            this.lblTimKiem.Size = new System.Drawing.Size(60, 17);
            this.lblTimKiem.TabIndex = 1;
            this.lblTimKiem.Text = "Tìm kiếm";
            // 
            // txtTimKiem
            // 
            this.txtTimKiem.Location = new System.Drawing.Point(465, 21);
            this.txtTimKiem.Name = "txtTimKiem";
            this.txtTimKiem.Size = new System.Drawing.Size(602, 24);
            this.txtTimKiem.TabIndex = 2;
            this.txtTimKiem.TextChanged += new System.EventHandler(this.txtTimKiem_TextChanged);
            // 
            // pnlBoLoc
            // 
            this.pnlBoLoc.Controls.Add(this.grpBoLoc);
            this.pnlBoLoc.Controls.Add(this.splitterBoLoc);
            this.pnlBoLoc.Dock = System.Windows.Forms.DockStyle.Left;
            this.pnlBoLoc.Location = new System.Drawing.Point(0, 84);
            this.pnlBoLoc.Name = "pnlBoLoc";
            this.pnlBoLoc.Size = new System.Drawing.Size(387, 769);
            this.pnlBoLoc.TabIndex = 1;
            // 
            // grpBoLoc
            // 
            this.grpBoLoc.Controls.Add(this.lblTrangThai);
            this.grpBoLoc.Controls.Add(this.cboTrangThai);
            this.grpBoLoc.Controls.Add(this.lblHuongChuyen);
            this.grpBoLoc.Controls.Add(this.cboHuongChuyen);
            this.grpBoLoc.Controls.Add(this.lblNguoiTao);
            this.grpBoLoc.Controls.Add(this.cboNguoiTao);
            this.grpBoLoc.Controls.Add(this.lblThoiGianTao);
            this.grpBoLoc.Controls.Add(this.grpThoiGianTao);
            this.grpBoLoc.Location = new System.Drawing.Point(12, 9);
            this.grpBoLoc.Name = "grpBoLoc";
            this.grpBoLoc.Size = new System.Drawing.Size(360, 717);
            this.grpBoLoc.TabIndex = 0;
            this.grpBoLoc.TabStop = false;
            // 
            // lblTrangThai
            // 
            this.lblTrangThai.AutoSize = true;
            this.lblTrangThai.Location = new System.Drawing.Point(15, 311);
            this.lblTrangThai.Name = "lblTrangThai";
            this.lblTrangThai.Size = new System.Drawing.Size(69, 17);
            this.lblTrangThai.TabIndex = 0;
            this.lblTrangThai.Text = "Trạng thái:";
            // 
            // cboTrangThai
            // 
            this.cboTrangThai.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboTrangThai.FormattingEnabled = true;
            this.cboTrangThai.Location = new System.Drawing.Point(19, 345);
            this.cboTrangThai.Name = "cboTrangThai";
            this.cboTrangThai.Size = new System.Drawing.Size(322, 25);
            this.cboTrangThai.TabIndex = 1;
            this.cboTrangThai.SelectedIndexChanged += new System.EventHandler(this.BoLoc_ThayDoi);
            // 
            // lblHuongChuyen
            // 
            this.lblHuongChuyen.AutoSize = true;
            this.lblHuongChuyen.Location = new System.Drawing.Point(15, 180);
            this.lblHuongChuyen.Name = "lblHuongChuyen";
            this.lblHuongChuyen.Size = new System.Drawing.Size(95, 17);
            this.lblHuongChuyen.TabIndex = 2;
            this.lblHuongChuyen.Text = "Hướng chuyển:";
            // 
            // cboHuongChuyen
            // 
            this.cboHuongChuyen.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboHuongChuyen.FormattingEnabled = true;
            this.cboHuongChuyen.Location = new System.Drawing.Point(19, 214);
            this.cboHuongChuyen.Name = "cboHuongChuyen";
            this.cboHuongChuyen.Size = new System.Drawing.Size(322, 25);
            this.cboHuongChuyen.TabIndex = 3;
            this.cboHuongChuyen.SelectedIndexChanged += new System.EventHandler(this.BoLoc_ThayDoi);
            // 
            // lblNguoiTao
            // 
            this.lblNguoiTao.AutoSize = true;
            this.lblNguoiTao.Location = new System.Drawing.Point(15, 58);
            this.lblNguoiTao.Name = "lblNguoiTao";
            this.lblNguoiTao.Size = new System.Drawing.Size(71, 17);
            this.lblNguoiTao.TabIndex = 6;
            this.lblNguoiTao.Text = "Người tạo:";
            // 
            // cboNguoiTao
            // 
            this.cboNguoiTao.FormattingEnabled = true;
            this.cboNguoiTao.Location = new System.Drawing.Point(19, 92);
            this.cboNguoiTao.Name = "cboNguoiTao";
            this.cboNguoiTao.Size = new System.Drawing.Size(322, 25);
            this.cboNguoiTao.TabIndex = 7;
            this.cboNguoiTao.SelectedIndexChanged += new System.EventHandler(this.BoLoc_ThayDoi);
            // 
            // lblThoiGianTao
            // 
            this.lblThoiGianTao.AutoSize = true;
            this.lblThoiGianTao.Location = new System.Drawing.Point(16, 463);
            this.lblThoiGianTao.Name = "lblThoiGianTao";
            this.lblThoiGianTao.Size = new System.Drawing.Size(88, 17);
            this.lblThoiGianTao.TabIndex = 8;
            this.lblThoiGianTao.Text = "Thời gian tạo:";
            // 
            // grpThoiGianTao
            // 
            this.grpThoiGianTao.Controls.Add(this.dtpTuNgay);
            this.grpThoiGianTao.Controls.Add(this.dtpDenNgay);
            this.grpThoiGianTao.Controls.Add(this.lblTuNgay);
            this.grpThoiGianTao.Controls.Add(this.lblDenNgay);
            this.grpThoiGianTao.Location = new System.Drawing.Point(20, 499);
            this.grpThoiGianTao.Name = "grpThoiGianTao";
            this.grpThoiGianTao.Size = new System.Drawing.Size(322, 108);
            this.grpThoiGianTao.TabIndex = 9;
            this.grpThoiGianTao.TabStop = false;
            // 
            // dtpTuNgay
            // 
            this.dtpTuNgay.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpTuNgay.Location = new System.Drawing.Point(75, 23);
            this.dtpTuNgay.Name = "dtpTuNgay";
            this.dtpTuNgay.Size = new System.Drawing.Size(230, 24);
            this.dtpTuNgay.TabIndex = 1;
            this.dtpTuNgay.ValueChanged += new System.EventHandler(this.BoLoc_ThayDoi);
            // 
            // dtpDenNgay
            // 
            this.dtpDenNgay.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpDenNgay.Location = new System.Drawing.Point(75, 60);
            this.dtpDenNgay.Name = "dtpDenNgay";
            this.dtpDenNgay.Size = new System.Drawing.Size(230, 24);
            this.dtpDenNgay.TabIndex = 3;
            this.dtpDenNgay.ValueChanged += new System.EventHandler(this.BoLoc_ThayDoi);
            // 
            // lblTuNgay
            // 
            this.lblTuNgay.AutoSize = true;
            this.lblTuNgay.Location = new System.Drawing.Point(10, 28);
            this.lblTuNgay.Name = "lblTuNgay";
            this.lblTuNgay.Size = new System.Drawing.Size(58, 17);
            this.lblTuNgay.TabIndex = 0;
            this.lblTuNgay.Text = "Từ ngày:";
            // 
            // lblDenNgay
            // 
            this.lblDenNgay.AutoSize = true;
            this.lblDenNgay.Location = new System.Drawing.Point(10, 65);
            this.lblDenNgay.Name = "lblDenNgay";
            this.lblDenNgay.Size = new System.Drawing.Size(66, 17);
            this.lblDenNgay.TabIndex = 2;
            this.lblDenNgay.Text = "Đến ngày:";
            // 
            // splitterBoLoc
            // 
            this.splitterBoLoc.BackColor = System.Drawing.SystemColors.ActiveBorder;
            this.splitterBoLoc.Dock = System.Windows.Forms.DockStyle.Top;
            this.splitterBoLoc.Location = new System.Drawing.Point(0, 0);
            this.splitterBoLoc.Name = "splitterBoLoc";
            this.splitterBoLoc.Size = new System.Drawing.Size(387, 1);
            this.splitterBoLoc.TabIndex = 0;
            this.splitterBoLoc.TabStop = false;
            // 
            // pnlPhanTrang
            // 
            this.pnlPhanTrang.Controls.Add(this.lblTongSoPhieu);
            this.pnlPhanTrang.Controls.Add(this.lblSoDongMoiTrang);
            this.pnlPhanTrang.Controls.Add(this.cboSoDongMoiTrang);
            this.pnlPhanTrang.Controls.Add(this.btnTrangTruoc);
            this.pnlPhanTrang.Controls.Add(this.lblTrangHienTai);
            this.pnlPhanTrang.Controls.Add(this.btnTrangSau);
            this.pnlPhanTrang.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnlPhanTrang.Location = new System.Drawing.Point(387, 811);
            this.pnlPhanTrang.Name = "pnlPhanTrang";
            this.pnlPhanTrang.Size = new System.Drawing.Size(1395, 42);
            this.pnlPhanTrang.TabIndex = 4;
            // 
            // lblTongSoPhieu
            // 
            this.lblTongSoPhieu.AutoSize = true;
            this.lblTongSoPhieu.Location = new System.Drawing.Point(22, 12);
            this.lblTongSoPhieu.Name = "lblTongSoPhieu";
            this.lblTongSoPhieu.Size = new System.Drawing.Size(150, 17);
            this.lblTongSoPhieu.TabIndex = 0;
            this.lblTongSoPhieu.Text = "Tổng số: 0 phiếu chuyển";
            // 
            // lblSoDongMoiTrang
            // 
            this.lblSoDongMoiTrang.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblSoDongMoiTrang.AutoSize = true;
            this.lblSoDongMoiTrang.Location = new System.Drawing.Point(932, 13);
            this.lblSoDongMoiTrang.Name = "lblSoDongMoiTrang";
            this.lblSoDongMoiTrang.Size = new System.Drawing.Size(61, 17);
            this.lblSoDongMoiTrang.TabIndex = 1;
            this.lblSoDongMoiTrang.Text = "Số dòng:";
            // 
            // cboSoDongMoiTrang
            // 
            this.cboSoDongMoiTrang.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.cboSoDongMoiTrang.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboSoDongMoiTrang.FormattingEnabled = true;
            this.cboSoDongMoiTrang.Items.AddRange(new object[] {
            "10",
            "20",
            "50",
            "100"});
            this.cboSoDongMoiTrang.Location = new System.Drawing.Point(1008, 10);
            this.cboSoDongMoiTrang.Name = "cboSoDongMoiTrang";
            this.cboSoDongMoiTrang.Size = new System.Drawing.Size(70, 25);
            this.cboSoDongMoiTrang.TabIndex = 2;
            this.cboSoDongMoiTrang.SelectedIndexChanged += new System.EventHandler(this.cboSoDongMoiTrang_SelectedIndexChanged);
            // 
            // btnTrangTruoc
            // 
            this.btnTrangTruoc.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnTrangTruoc.Location = new System.Drawing.Point(1117, 6);
            this.btnTrangTruoc.Name = "btnTrangTruoc";
            this.btnTrangTruoc.Size = new System.Drawing.Size(80, 31);
            this.btnTrangTruoc.TabIndex = 3;
            this.btnTrangTruoc.Text = "◀ Trước";
            this.btnTrangTruoc.UseVisualStyleBackColor = true;
            this.btnTrangTruoc.Click += new System.EventHandler(this.btnTrangTruoc_Click);
            // 
            // lblTrangHienTai
            // 
            this.lblTrangHienTai.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblTrangHienTai.AutoSize = true;
            this.lblTrangHienTai.Location = new System.Drawing.Point(1214, 13);
            this.lblTrangHienTai.Name = "lblTrangHienTai";
            this.lblTrangHienTai.Size = new System.Drawing.Size(72, 17);
            this.lblTrangHienTai.TabIndex = 4;
            this.lblTrangHienTai.Text = "Trang 1 / 1";
            // 
            // btnTrangSau
            // 
            this.btnTrangSau.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnTrangSau.Location = new System.Drawing.Point(1303, 6);
            this.btnTrangSau.Name = "btnTrangSau";
            this.btnTrangSau.Size = new System.Drawing.Size(80, 31);
            this.btnTrangSau.TabIndex = 5;
            this.btnTrangSau.Text = "Sau ▶";
            this.btnTrangSau.UseVisualStyleBackColor = true;
            this.btnTrangSau.Click += new System.EventHandler(this.btnTrangSau_Click);
            // 
            // dgvChuyenKho
            // 
            this.dgvChuyenKho.AllowUserToAddRows = false;
            this.dgvChuyenKho.AllowUserToDeleteRows = false;
            this.dgvChuyenKho.BackgroundColor = System.Drawing.Color.White;
            this.dgvChuyenKho.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvChuyenKho.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colChon,
            this.colMaPhieu,
            this.colNgayLap,
            this.colKhoNguon,
            this.colKhoDich,
            this.colTongSlChuyen,
            this.colTongSlNhan,
            this.colTongGiaTri,
            this.colNguoiTao,
            this.colGhiChu,
            this.colTrangThai,
            this.colMaPhieuChuyen});
            this.dgvChuyenKho.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvChuyenKho.Location = new System.Drawing.Point(387, 84);
            this.dgvChuyenKho.Name = "dgvChuyenKho";
            this.dgvChuyenKho.ReadOnly = true;
            this.dgvChuyenKho.RowHeadersVisible = false;
            this.dgvChuyenKho.RowHeadersWidth = 51;
            this.dgvChuyenKho.RowTemplate.Height = 28;
            this.dgvChuyenKho.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvChuyenKho.Size = new System.Drawing.Size(1395, 727);
            this.dgvChuyenKho.TabIndex = 3;
            this.dgvChuyenKho.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvChuyenKho_CellDoubleClick);
            this.dgvChuyenKho.CellFormatting += new System.Windows.Forms.DataGridViewCellFormattingEventHandler(this.dgvChuyenKho_CellFormatting);
            // 
            // colChon
            // 
            this.colChon.HeaderText = "";
            this.colChon.MinimumWidth = 35;
            this.colChon.Name = "colChon";
            this.colChon.ReadOnly = true;
            this.colChon.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.colChon.Width = 35;
            // 
            // colMaPhieu
            // 
            this.colMaPhieu.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.colMaPhieu.DataPropertyName = "MaPhieu";
            this.colMaPhieu.FillWeight = 120F;
            this.colMaPhieu.HeaderText = "Mã chuyển kho";
            this.colMaPhieu.MinimumWidth = 100;
            this.colMaPhieu.Name = "colMaPhieu";
            this.colMaPhieu.ReadOnly = true;
            // 
            // colNgayLap
            // 
            this.colNgayLap.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.colNgayLap.DataPropertyName = "NgayLapHienThi";
            this.colNgayLap.FillWeight = 140F;
            this.colNgayLap.HeaderText = "Thời gian";
            this.colNgayLap.MinimumWidth = 120;
            this.colNgayLap.Name = "colNgayLap";
            this.colNgayLap.ReadOnly = true;
            // 
            // colKhoNguon
            // 
            this.colKhoNguon.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.colKhoNguon.DataPropertyName = "TenKhoNguon";
            this.colKhoNguon.FillWeight = 180F;
            this.colKhoNguon.HeaderText = "Kho gửi";
            this.colKhoNguon.MinimumWidth = 120;
            this.colKhoNguon.Name = "colKhoNguon";
            this.colKhoNguon.ReadOnly = true;
            // 
            // colKhoDich
            // 
            this.colKhoDich.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.colKhoDich.DataPropertyName = "TenKhoDich";
            this.colKhoDich.FillWeight = 180F;
            this.colKhoDich.HeaderText = "Kho nhận";
            this.colKhoDich.MinimumWidth = 120;
            this.colKhoDich.Name = "colKhoDich";
            this.colKhoDich.ReadOnly = true;
            // 
            // colTongSlChuyen
            // 
            this.colTongSlChuyen.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.colTongSlChuyen.DataPropertyName = "TongSoLuongChuyen";
            this.colTongSlChuyen.FillWeight = 110F;
            this.colTongSlChuyen.HeaderText = "Tổng SL chuyển";
            this.colTongSlChuyen.MinimumWidth = 100;
            this.colTongSlChuyen.Name = "colTongSlChuyen";
            this.colTongSlChuyen.ReadOnly = true;
            // 
            // colTongSlNhan
            // 
            this.colTongSlNhan.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.colTongSlNhan.DataPropertyName = "TongSoLuongNhanHienThi";
            this.colTongSlNhan.FillWeight = 110F;
            this.colTongSlNhan.HeaderText = "Tổng SL nhận";
            this.colTongSlNhan.MinimumWidth = 100;
            this.colTongSlNhan.Name = "colTongSlNhan";
            this.colTongSlNhan.ReadOnly = true;
            // 
            // colTongGiaTri
            // 
            this.colTongGiaTri.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.colTongGiaTri.DataPropertyName = "TongGiaTriChuyen";
            this.colTongGiaTri.FillWeight = 130F;
            this.colTongGiaTri.HeaderText = "Tổng giá trị";
            this.colTongGiaTri.MinimumWidth = 110;
            this.colTongGiaTri.Name = "colTongGiaTri";
            this.colTongGiaTri.ReadOnly = true;
            // 
            // colNguoiTao
            // 
            this.colNguoiTao.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.colNguoiTao.DataPropertyName = "TenNguoiTao";
            this.colNguoiTao.FillWeight = 140F;
            this.colNguoiTao.HeaderText = "Người tạo";
            this.colNguoiTao.MinimumWidth = 110;
            this.colNguoiTao.Name = "colNguoiTao";
            this.colNguoiTao.ReadOnly = true;
            // 
            // colGhiChu
            // 
            this.colGhiChu.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.colGhiChu.DataPropertyName = "GhiChu";
            this.colGhiChu.FillWeight = 180F;
            this.colGhiChu.HeaderText = "Ghi chú";
            this.colGhiChu.MinimumWidth = 150;
            this.colGhiChu.Name = "colGhiChu";
            this.colGhiChu.ReadOnly = true;
            this.colGhiChu.Visible = false;
            // 
            // colTrangThai
            // 
            this.colTrangThai.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.colTrangThai.DataPropertyName = "TrangThaiHienThi";
            this.colTrangThai.FillWeight = 120F;
            this.colTrangThai.HeaderText = "Trạng thái";
            this.colTrangThai.MinimumWidth = 100;
            this.colTrangThai.Name = "colTrangThai";
            this.colTrangThai.ReadOnly = true;
            // 
            // colMaPhieuChuyen
            // 
            this.colMaPhieuChuyen.DataPropertyName = "MaPhieuChuyen";
            this.colMaPhieuChuyen.HeaderText = "Mã ID";
            this.colMaPhieuChuyen.Name = "colMaPhieuChuyen";
            this.colMaPhieuChuyen.ReadOnly = true;
            this.colMaPhieuChuyen.Visible = false;
            // 
            // frmChuyenKho
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1782, 853);
            this.Controls.Add(this.dgvChuyenKho);
            this.Controls.Add(this.pnlPhanTrang);
            this.Controls.Add(this.pnlBoLoc);
            this.Controls.Add(this.pnlTop);
            this.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.MaximizeBox = false;
            this.Name = "frmChuyenKho";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Quản lý Chuyển kho";
            this.Load += new System.EventHandler(this.frmChuyenKho_Load);
            this.pnlTop.ResumeLayout(false);
            this.grpTop.ResumeLayout(false);
            this.grpTop.PerformLayout();
            this.pnlBoLoc.ResumeLayout(false);
            this.grpBoLoc.ResumeLayout(false);
            this.grpBoLoc.PerformLayout();
            this.grpThoiGianTao.ResumeLayout(false);
            this.grpThoiGianTao.PerformLayout();
            this.pnlPhanTrang.ResumeLayout(false);
            this.pnlPhanTrang.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvChuyenKho)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pnlTop;
        private System.Windows.Forms.GroupBox grpTop;
        private System.Windows.Forms.Label lblTieuDe;
        private System.Windows.Forms.TextBox txtTimKiem;
        private System.Windows.Forms.Label lblTimKiem;
        private System.Windows.Forms.Button btnChuyenKho;
        private System.Windows.Forms.Button btnXuatFile;
        private System.Windows.Forms.Button btnHienCot;
        private System.Windows.Forms.Button btnHuongDanSuDung;
        private System.Windows.Forms.Panel pnlBoLoc;
        private System.Windows.Forms.GroupBox grpBoLoc;
        private System.Windows.Forms.Label lblTrangThai;
        private System.Windows.Forms.ComboBox cboTrangThai;
        private System.Windows.Forms.Label lblHuongChuyen;
        private System.Windows.Forms.ComboBox cboHuongChuyen;


        private System.Windows.Forms.Label lblNguoiTao;
        private System.Windows.Forms.ComboBox cboNguoiTao;
        private System.Windows.Forms.Label lblThoiGianTao;
        private System.Windows.Forms.GroupBox grpThoiGianTao;
        private System.Windows.Forms.DateTimePicker dtpTuNgay;
        private System.Windows.Forms.DateTimePicker dtpDenNgay;
        private System.Windows.Forms.Label lblTuNgay;
        private System.Windows.Forms.Label lblDenNgay;
        private System.Windows.Forms.Splitter splitterBoLoc;
        private System.Windows.Forms.Panel pnlPhanTrang;
        private System.Windows.Forms.Label lblTongSoPhieu;
        private System.Windows.Forms.Label lblSoDongMoiTrang;
        private System.Windows.Forms.ComboBox cboSoDongMoiTrang;
        private System.Windows.Forms.Button btnTrangTruoc;
        private System.Windows.Forms.Label lblTrangHienTai;
        private System.Windows.Forms.Button btnTrangSau;
        private System.Windows.Forms.DataGridView dgvChuyenKho;
        private System.Windows.Forms.DataGridViewCheckBoxColumn colChon;
        private System.Windows.Forms.DataGridViewTextBoxColumn colMaPhieu;
        private System.Windows.Forms.DataGridViewTextBoxColumn colNgayLap;
        private System.Windows.Forms.DataGridViewTextBoxColumn colKhoNguon;
        private System.Windows.Forms.DataGridViewTextBoxColumn colKhoDich;
        private System.Windows.Forms.DataGridViewTextBoxColumn colTongSlChuyen;
        private System.Windows.Forms.DataGridViewTextBoxColumn colTongSlNhan;
        private System.Windows.Forms.DataGridViewTextBoxColumn colTongGiaTri;
        private System.Windows.Forms.DataGridViewTextBoxColumn colNguoiTao;
        private System.Windows.Forms.DataGridViewTextBoxColumn colGhiChu;
        private System.Windows.Forms.DataGridViewTextBoxColumn colTrangThai;
        private System.Windows.Forms.DataGridViewTextBoxColumn colMaPhieuChuyen;
    }
}
