namespace BTL_LTTQ_2.GUI.Forms.NhapHang
{
    partial class frmNhapHang
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
            this.btnThemNhapHang = new System.Windows.Forms.Button();
            this.lblTimKiem = new System.Windows.Forms.Label();
            this.txtTimKiem = new System.Windows.Forms.TextBox();
            this.pnlBoLoc = new System.Windows.Forms.Panel();
            this.grpBoLoc = new System.Windows.Forms.GroupBox();
            this.cboNguoiNhap = new System.Windows.Forms.ComboBox();
            this.lblNguoiNhap = new System.Windows.Forms.Label();
            this.cboNguoiTao = new System.Windows.Forms.ComboBox();
            this.lblNguoiTao = new System.Windows.Forms.Label();
            this.cboNhaCungCap = new System.Windows.Forms.ComboBox();
            this.lblNhaCungCap = new System.Windows.Forms.Label();
            this.grpThoiGianTao = new System.Windows.Forms.GroupBox();
            this.dtpTuNgay = new System.Windows.Forms.DateTimePicker();
            this.dtpDenNgay = new System.Windows.Forms.DateTimePicker();
            this.lblTuNgay = new System.Windows.Forms.Label();
            this.lblDenNgay = new System.Windows.Forms.Label();
            this.lblThoiGianTao = new System.Windows.Forms.Label();
            this.cboTrangThai = new System.Windows.Forms.ComboBox();
            this.lblTrangThai = new System.Windows.Forms.Label();
            this.splitterBoLoc = new System.Windows.Forms.Splitter();
            this.pnlPhanTrang = new System.Windows.Forms.Panel();
            this.lblSoDongMoiTrang = new System.Windows.Forms.Label();
            this.cboSoDongMoiTrang = new System.Windows.Forms.ComboBox();
            this.btnTrangTruoc = new System.Windows.Forms.Button();
            this.lblTrangHienTai = new System.Windows.Forms.Label();
            this.btnTrangSau = new System.Windows.Forms.Button();
            this.lblTongSoPhieu = new System.Windows.Forms.Label();
            this.dgvNhapHang = new System.Windows.Forms.DataGridView();
            this.colChon = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.colMaPhieu = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colThoiGian = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colKho = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colMaNcc = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colTenNcc = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colCanTraNcc = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colTongTien = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colTrangThai = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colMaPhieuNhap = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.pnlTop.SuspendLayout();
            this.grpTop.SuspendLayout();
            this.pnlBoLoc.SuspendLayout();
            this.grpBoLoc.SuspendLayout();
            this.grpThoiGianTao.SuspendLayout();
            this.pnlPhanTrang.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvNhapHang)).BeginInit();
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
            this.grpTop.Controls.Add(this.btnThemNhapHang);
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
            this.lblTieuDe.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.lblTieuDe.Location = new System.Drawing.Point(24, 19);
            this.lblTieuDe.Name = "lblTieuDe";
            this.lblTieuDe.Size = new System.Drawing.Size(95, 21);
            this.lblTieuDe.TabIndex = 6;
            this.lblTieuDe.Text = "Nhập hàng";
            // 
            // btnHuongDanSuDung
            // 
            this.btnHuongDanSuDung.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnHuongDanSuDung.Location = new System.Drawing.Point(1711, 21);
            this.btnHuongDanSuDung.Name = "btnHuongDanSuDung";
            this.btnHuongDanSuDung.Size = new System.Drawing.Size(43, 33);
            this.btnHuongDanSuDung.TabIndex = 5;
            this.btnHuongDanSuDung.Text = "?";
            this.btnHuongDanSuDung.UseVisualStyleBackColor = true;
            this.btnHuongDanSuDung.Click += new System.EventHandler(this.btnHuongDanSuDung_Click);
            // 
            // btnHienCot
            // 
            this.btnHienCot.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnHienCot.Location = new System.Drawing.Point(1600, 21);
            this.btnHienCot.Name = "btnHienCot";
            this.btnHienCot.Size = new System.Drawing.Size(95, 33);
            this.btnHienCot.TabIndex = 4;
            this.btnHienCot.Text = "Hiện cột";
            this.btnHienCot.UseVisualStyleBackColor = true;
            this.btnHienCot.Click += new System.EventHandler(this.btnHienCot_Click);
            // 
            // btnXuatFile
            // 
            this.btnXuatFile.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnXuatFile.Location = new System.Drawing.Point(1474, 21);
            this.btnXuatFile.Name = "btnXuatFile";
            this.btnXuatFile.Size = new System.Drawing.Size(110, 33);
            this.btnXuatFile.TabIndex = 3;
            this.btnXuatFile.Text = "Xuất file";
            this.btnXuatFile.UseVisualStyleBackColor = true;
            this.btnXuatFile.Click += new System.EventHandler(this.btnXuatFile_Click);
            // 
            // btnThemNhapHang
            // 
            this.btnThemNhapHang.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnThemNhapHang.Location = new System.Drawing.Point(1313, 21);
            this.btnThemNhapHang.Name = "btnThemNhapHang";
            this.btnThemNhapHang.Size = new System.Drawing.Size(145, 33);
            this.btnThemNhapHang.TabIndex = 1;
            this.btnThemNhapHang.Text = "+ Nhập hàng";
            this.btnThemNhapHang.UseVisualStyleBackColor = true;
            this.btnThemNhapHang.Click += new System.EventHandler(this.btnThemNhapHang_Click);
            // 
            // lblTimKiem
            // 
            this.lblTimKiem.AutoSize = true;
            this.lblTimKiem.Location = new System.Drawing.Point(372, 25);
            this.lblTimKiem.Name = "lblTimKiem";
            this.lblTimKiem.Size = new System.Drawing.Size(60, 17);
            this.lblTimKiem.TabIndex = 1;
            this.lblTimKiem.Text = "Tìm kiếm";
            // 
            // txtTimKiem
            // 
            this.txtTimKiem.Location = new System.Drawing.Point(470, 21);
            this.txtTimKiem.Name = "txtTimKiem";
            this.txtTimKiem.Size = new System.Drawing.Size(597, 24);
            this.txtTimKiem.TabIndex = 0;
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
            this.grpBoLoc.Controls.Add(this.cboNguoiNhap);
            this.grpBoLoc.Controls.Add(this.lblNguoiNhap);
            this.grpBoLoc.Controls.Add(this.cboNguoiTao);
            this.grpBoLoc.Controls.Add(this.lblNguoiTao);
            this.grpBoLoc.Controls.Add(this.cboNhaCungCap);
            this.grpBoLoc.Controls.Add(this.lblNhaCungCap);
            this.grpBoLoc.Controls.Add(this.grpThoiGianTao);
            this.grpBoLoc.Controls.Add(this.lblThoiGianTao);
            this.grpBoLoc.Controls.Add(this.cboTrangThai);
            this.grpBoLoc.Controls.Add(this.lblTrangThai);
            this.grpBoLoc.Location = new System.Drawing.Point(12, 9);
            this.grpBoLoc.Name = "grpBoLoc";
            this.grpBoLoc.Size = new System.Drawing.Size(360, 717);
            this.grpBoLoc.TabIndex = 1;
            this.grpBoLoc.TabStop = false;
            // 
            // cboNguoiNhap
            // 
            this.cboNguoiNhap.FormattingEnabled = true;
            this.cboNguoiNhap.Location = new System.Drawing.Point(19, 185);
            this.cboNguoiNhap.Name = "cboNguoiNhap";
            this.cboNguoiNhap.Size = new System.Drawing.Size(322, 25);
            this.cboNguoiNhap.TabIndex = 9;
            // 
            // lblNguoiNhap
            // 
            this.lblNguoiNhap.AutoSize = true;
            this.lblNguoiNhap.Location = new System.Drawing.Point(19, 155);
            this.lblNguoiNhap.Name = "lblNguoiNhap";
            this.lblNguoiNhap.Size = new System.Drawing.Size(78, 17);
            this.lblNguoiNhap.TabIndex = 8;
            this.lblNguoiNhap.Text = "Người nhập";
            // 
            // cboNguoiTao
            // 
            this.cboNguoiTao.FormattingEnabled = true;
            this.cboNguoiTao.Location = new System.Drawing.Point(19, 64);
            this.cboNguoiTao.Name = "cboNguoiTao";
            this.cboNguoiTao.Size = new System.Drawing.Size(322, 25);
            this.cboNguoiTao.TabIndex = 7;
            // 
            // lblNguoiTao
            // 
            this.lblNguoiTao.AutoSize = true;
            this.lblNguoiTao.Location = new System.Drawing.Point(19, 34);
            this.lblNguoiTao.Name = "lblNguoiTao";
            this.lblNguoiTao.Size = new System.Drawing.Size(68, 17);
            this.lblNguoiTao.TabIndex = 6;
            this.lblNguoiTao.Text = "Người tạo";
            // 
            // cboNhaCungCap
            // 
            this.cboNhaCungCap.FormattingEnabled = true;
            this.cboNhaCungCap.Location = new System.Drawing.Point(19, 303);
            this.cboNhaCungCap.Name = "cboNhaCungCap";
            this.cboNhaCungCap.Size = new System.Drawing.Size(322, 25);
            this.cboNhaCungCap.TabIndex = 5;
            // 
            // lblNhaCungCap
            // 
            this.lblNhaCungCap.AutoSize = true;
            this.lblNhaCungCap.Location = new System.Drawing.Point(19, 273);
            this.lblNhaCungCap.Name = "lblNhaCungCap";
            this.lblNhaCungCap.Size = new System.Drawing.Size(89, 17);
            this.lblNhaCungCap.TabIndex = 4;
            this.lblNhaCungCap.Text = "Nhà cung cấp";
            // 
            // grpThoiGianTao
            // 
            this.grpThoiGianTao.Controls.Add(this.dtpTuNgay);
            this.grpThoiGianTao.Controls.Add(this.dtpDenNgay);
            this.grpThoiGianTao.Controls.Add(this.lblTuNgay);
            this.grpThoiGianTao.Controls.Add(this.lblDenNgay);
            this.grpThoiGianTao.Location = new System.Drawing.Point(16, 544);
            this.grpThoiGianTao.Name = "grpThoiGianTao";
            this.grpThoiGianTao.Size = new System.Drawing.Size(322, 108);
            this.grpThoiGianTao.TabIndex = 3;
            this.grpThoiGianTao.TabStop = false;
            // 
            // dtpTuNgay
            // 
            this.dtpTuNgay.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpTuNgay.Location = new System.Drawing.Point(112, 23);
            this.dtpTuNgay.Name = "dtpTuNgay";
            this.dtpTuNgay.Size = new System.Drawing.Size(200, 24);
            this.dtpTuNgay.TabIndex = 10;
            this.dtpTuNgay.ValueChanged += new System.EventHandler(this.BoLoc_SelectedIndexChanged);
            // 
            // dtpDenNgay
            // 
            this.dtpDenNgay.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpDenNgay.Location = new System.Drawing.Point(112, 63);
            this.dtpDenNgay.Name = "dtpDenNgay";
            this.dtpDenNgay.Size = new System.Drawing.Size(200, 24);
            this.dtpDenNgay.TabIndex = 11;
            this.dtpDenNgay.ValueChanged += new System.EventHandler(this.BoLoc_SelectedIndexChanged);
            // 
            // lblTuNgay
            // 
            this.lblTuNgay.AutoSize = true;
            this.lblTuNgay.Location = new System.Drawing.Point(6, 27);
            this.lblTuNgay.Name = "lblTuNgay";
            this.lblTuNgay.Size = new System.Drawing.Size(55, 17);
            this.lblTuNgay.TabIndex = 7;
            this.lblTuNgay.Text = "Từ ngày";
            // 
            // lblDenNgay
            // 
            this.lblDenNgay.AutoSize = true;
            this.lblDenNgay.Location = new System.Drawing.Point(6, 67);
            this.lblDenNgay.Name = "lblDenNgay";
            this.lblDenNgay.Size = new System.Drawing.Size(63, 17);
            this.lblDenNgay.TabIndex = 8;
            this.lblDenNgay.Text = "Đến ngày";
            // 
            // lblThoiGianTao
            // 
            this.lblThoiGianTao.AutoSize = true;
            this.lblThoiGianTao.Location = new System.Drawing.Point(16, 516);
            this.lblThoiGianTao.Name = "lblThoiGianTao";
            this.lblThoiGianTao.Size = new System.Drawing.Size(85, 17);
            this.lblThoiGianTao.TabIndex = 2;
            this.lblThoiGianTao.Text = "Thời gian tạo";
            // 
            // cboTrangThai
            // 
            this.cboTrangThai.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboTrangThai.FormattingEnabled = true;
            this.cboTrangThai.Location = new System.Drawing.Point(19, 422);
            this.cboTrangThai.Name = "cboTrangThai";
            this.cboTrangThai.Size = new System.Drawing.Size(322, 25);
            this.cboTrangThai.TabIndex = 1;
            this.cboTrangThai.SelectedIndexChanged += new System.EventHandler(this.BoLoc_SelectedIndexChanged);
            // 
            // lblTrangThai
            // 
            this.lblTrangThai.AutoSize = true;
            this.lblTrangThai.Location = new System.Drawing.Point(20, 391);
            this.lblTrangThai.Name = "lblTrangThai";
            this.lblTrangThai.Size = new System.Drawing.Size(66, 17);
            this.lblTrangThai.TabIndex = 0;
            this.lblTrangThai.Text = "Trạng thái";
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
            this.pnlPhanTrang.Controls.Add(this.lblSoDongMoiTrang);
            this.pnlPhanTrang.Controls.Add(this.cboSoDongMoiTrang);
            this.pnlPhanTrang.Controls.Add(this.btnTrangTruoc);
            this.pnlPhanTrang.Controls.Add(this.lblTrangHienTai);
            this.pnlPhanTrang.Controls.Add(this.btnTrangSau);
            this.pnlPhanTrang.Controls.Add(this.lblTongSoPhieu);
            this.pnlPhanTrang.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnlPhanTrang.Location = new System.Drawing.Point(387, 811);
            this.pnlPhanTrang.Name = "pnlPhanTrang";
            this.pnlPhanTrang.Size = new System.Drawing.Size(1395, 42);
            this.pnlPhanTrang.TabIndex = 3;
            // 
            // lblSoDongMoiTrang
            // 
            this.lblSoDongMoiTrang.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblSoDongMoiTrang.AutoSize = true;
            this.lblSoDongMoiTrang.Location = new System.Drawing.Point(855, 9);
            this.lblSoDongMoiTrang.Name = "lblSoDongMoiTrang";
            this.lblSoDongMoiTrang.Size = new System.Drawing.Size(61, 17);
            this.lblSoDongMoiTrang.TabIndex = 6;
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
            this.cboSoDongMoiTrang.Location = new System.Drawing.Point(947, 5);
            this.cboSoDongMoiTrang.Name = "cboSoDongMoiTrang";
            this.cboSoDongMoiTrang.Size = new System.Drawing.Size(70, 25);
            this.cboSoDongMoiTrang.TabIndex = 7;
            this.cboSoDongMoiTrang.SelectedIndexChanged += new System.EventHandler(this.cboSoDongMoiTrang_SelectedIndexChanged);
            // 
            // btnTrangTruoc
            // 
            this.btnTrangTruoc.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnTrangTruoc.Location = new System.Drawing.Point(1027, 5);
            this.btnTrangTruoc.Name = "btnTrangTruoc";
            this.btnTrangTruoc.Size = new System.Drawing.Size(100, 33);
            this.btnTrangTruoc.TabIndex = 8;
            this.btnTrangTruoc.Text = "◀ Trước";
            this.btnTrangTruoc.UseVisualStyleBackColor = true;
            this.btnTrangTruoc.Click += new System.EventHandler(this.btnTrangTruoc_Click);
            // 
            // lblTrangHienTai
            // 
            this.lblTrangHienTai.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblTrangHienTai.Location = new System.Drawing.Point(1135, 5);
            this.lblTrangHienTai.Name = "lblTrangHienTai";
            this.lblTrangHienTai.Size = new System.Drawing.Size(140, 33);
            this.lblTrangHienTai.TabIndex = 9;
            this.lblTrangHienTai.Text = "Trang 1 / 1";
            this.lblTrangHienTai.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // btnTrangSau
            // 
            this.btnTrangSau.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnTrangSau.Location = new System.Drawing.Point(1283, 5);
            this.btnTrangSau.Name = "btnTrangSau";
            this.btnTrangSau.Size = new System.Drawing.Size(100, 33);
            this.btnTrangSau.TabIndex = 10;
            this.btnTrangSau.Text = "Sau ▶";
            this.btnTrangSau.UseVisualStyleBackColor = true;
            this.btnTrangSau.Click += new System.EventHandler(this.btnTrangSau_Click);
            // 
            // lblTongSoPhieu
            // 
            this.lblTongSoPhieu.AutoSize = true;
            this.lblTongSoPhieu.Location = new System.Drawing.Point(22, 9);
            this.lblTongSoPhieu.Name = "lblTongSoPhieu";
            this.lblTongSoPhieu.Size = new System.Drawing.Size(139, 17);
            this.lblTongSoPhieu.TabIndex = 0;
            this.lblTongSoPhieu.Text = "Tổng số: 0 phiếu nhập";
            // 
            // dgvNhapHang
            // 
            this.dgvNhapHang.AllowUserToAddRows = false;
            this.dgvNhapHang.AllowUserToDeleteRows = false;
            this.dgvNhapHang.AllowUserToResizeRows = false;
            this.dgvNhapHang.BackgroundColor = System.Drawing.Color.White;
            this.dgvNhapHang.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dgvNhapHang.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvNhapHang.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colChon,
            this.colMaPhieu,
            this.colThoiGian,
            this.colKho,
            this.colMaNcc,
            this.colTenNcc,
            this.colCanTraNcc,
            this.colTongTien,
            this.colTrangThai,
            this.colMaPhieuNhap});
            this.dgvNhapHang.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvNhapHang.Location = new System.Drawing.Point(387, 84);
            this.dgvNhapHang.MultiSelect = false;
            this.dgvNhapHang.Name = "dgvNhapHang";
            this.dgvNhapHang.RowHeadersVisible = false;
            this.dgvNhapHang.RowHeadersWidth = 51;
            this.dgvNhapHang.RowTemplate.Height = 32;
            this.dgvNhapHang.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvNhapHang.Size = new System.Drawing.Size(1395, 727);
            this.dgvNhapHang.TabIndex = 2;
            this.dgvNhapHang.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvNhapHang_CellContentClick);
            this.dgvNhapHang.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvNhapHang_CellDoubleClick);
            this.dgvNhapHang.CellFormatting += new System.Windows.Forms.DataGridViewCellFormattingEventHandler(this.dgvNhapHang_CellFormatting);
            this.dgvNhapHang.CellMouseLeave += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvNhapHang_CellMouseLeave);
            this.dgvNhapHang.CellMouseMove += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.dgvNhapHang_CellMouseMove);
            this.dgvNhapHang.CellPainting += new System.Windows.Forms.DataGridViewCellPaintingEventHandler(this.dgvNhapHang_CellPainting);
            this.dgvNhapHang.CellValueChanged += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvNhapHang_CellValueChanged);
            this.dgvNhapHang.ColumnHeaderMouseClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.dgvNhapHang_ColumnHeaderMouseClick);
            this.dgvNhapHang.CurrentCellDirtyStateChanged += new System.EventHandler(this.dgvNhapHang_CurrentCellDirtyStateChanged);
            this.dgvNhapHang.DataError += new System.Windows.Forms.DataGridViewDataErrorEventHandler(this.dgvNhapHang_DataError);
            // 
            // colChon
            // 
            this.colChon.DataPropertyName = "Chon";
            this.colChon.FalseValue = false;
            this.colChon.HeaderText = "";
            this.colChon.Name = "colChon";
            this.colChon.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.colChon.TrueValue = true;
            this.colChon.Width = 45;
            // 
            // colMaPhieu
            // 
            this.colMaPhieu.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.colMaPhieu.DataPropertyName = "MaPhieu";
            this.colMaPhieu.FillWeight = 120F;
            this.colMaPhieu.HeaderText = "Mã nhập hàng";
            this.colMaPhieu.Name = "colMaPhieu";
            this.colMaPhieu.ReadOnly = true;
            // 
            // colThoiGian
            // 
            this.colThoiGian.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.colThoiGian.DataPropertyName = "NgayLap";
            this.colThoiGian.FillWeight = 140F;
            this.colThoiGian.HeaderText = "Thời gian";
            this.colThoiGian.Name = "colThoiGian";
            this.colThoiGian.ReadOnly = true;
            // 
            // colKho
            // 
            this.colKho.DataPropertyName = "TenKho";
            this.colKho.HeaderText = "Kho";
            this.colKho.Name = "colKho";
            this.colKho.ReadOnly = true;
            this.colKho.Visible = false;
            this.colKho.Width = 140;
            // 
            // colMaNcc
            // 
            this.colMaNcc.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.colMaNcc.DataPropertyName = "MaNccHienThi";
            this.colMaNcc.FillWeight = 110F;
            this.colMaNcc.HeaderText = "Mã nhà cung cấp";
            this.colMaNcc.Name = "colMaNcc";
            this.colMaNcc.ReadOnly = true;
            // 
            // colTenNcc
            // 
            this.colTenNcc.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.colTenNcc.DataPropertyName = "TenDonVi";
            this.colTenNcc.FillWeight = 220F;
            this.colTenNcc.HeaderText = "Tên nhà cung cấp";
            this.colTenNcc.Name = "colTenNcc";
            this.colTenNcc.ReadOnly = true;
            // 
            // colCanTraNcc
            // 
            this.colCanTraNcc.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.colCanTraNcc.DataPropertyName = "CanTraNcc";
            this.colCanTraNcc.FillWeight = 130F;
            this.colCanTraNcc.HeaderText = "Cần trả NCC";
            this.colCanTraNcc.Name = "colCanTraNcc";
            this.colCanTraNcc.ReadOnly = true;
            // 
            // colTongTien
            // 
            this.colTongTien.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.colTongTien.DataPropertyName = "TongTien";
            this.colTongTien.FillWeight = 130F;
            this.colTongTien.HeaderText = "Tổng tiền";
            this.colTongTien.Name = "colTongTien";
            this.colTongTien.ReadOnly = true;
            // 
            // colTrangThai
            // 
            this.colTrangThai.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.colTrangThai.DataPropertyName = "TenTrangThai";
            this.colTrangThai.FillWeight = 120F;
            this.colTrangThai.HeaderText = "Trạng thái";
            this.colTrangThai.Name = "colTrangThai";
            this.colTrangThai.ReadOnly = true;
            // 
            // colMaPhieuNhap
            // 
            this.colMaPhieuNhap.DataPropertyName = "MaPhieuNhap";
            this.colMaPhieuNhap.HeaderText = "MaPhieuNhap";
            this.colMaPhieuNhap.Name = "colMaPhieuNhap";
            this.colMaPhieuNhap.ReadOnly = true;
            this.colMaPhieuNhap.Visible = false;
            // 
            // frmNhapHang
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1782, 853);
            this.Controls.Add(this.dgvNhapHang);
            this.Controls.Add(this.pnlPhanTrang);
            this.Controls.Add(this.pnlBoLoc);
            this.Controls.Add(this.pnlTop);
            this.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.MaximizeBox = false;
            this.Name = "frmNhapHang";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Nhập hàng";
            this.Load += new System.EventHandler(this.frmNhapHang_Load);
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
            ((System.ComponentModel.ISupportInitialize)(this.dgvNhapHang)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pnlTop;
        private System.Windows.Forms.GroupBox grpTop;
        private System.Windows.Forms.Label lblTieuDe;
        private System.Windows.Forms.Button btnHuongDanSuDung;
        private System.Windows.Forms.Button btnHienCot;
        private System.Windows.Forms.Button btnXuatFile;
        private System.Windows.Forms.Button btnThemNhapHang;
        private System.Windows.Forms.Label lblTimKiem;
        private System.Windows.Forms.TextBox txtTimKiem;
        private System.Windows.Forms.Panel pnlBoLoc;
        private System.Windows.Forms.GroupBox grpBoLoc;
        private System.Windows.Forms.ComboBox cboNguoiNhap;
        private System.Windows.Forms.Label lblNguoiNhap;
        private System.Windows.Forms.ComboBox cboNguoiTao;
        private System.Windows.Forms.Label lblNguoiTao;
        private System.Windows.Forms.ComboBox cboNhaCungCap;
        private System.Windows.Forms.Label lblNhaCungCap;
        private System.Windows.Forms.GroupBox grpThoiGianTao;
        private System.Windows.Forms.DateTimePicker dtpTuNgay;
        private System.Windows.Forms.DateTimePicker dtpDenNgay;
        private System.Windows.Forms.Label lblTuNgay;
        private System.Windows.Forms.Label lblDenNgay;
        private System.Windows.Forms.Label lblThoiGianTao;
        private System.Windows.Forms.ComboBox cboTrangThai;
        private System.Windows.Forms.Label lblTrangThai;
        private System.Windows.Forms.Splitter splitterBoLoc;
        private System.Windows.Forms.Panel pnlPhanTrang;
        private System.Windows.Forms.Label lblTongSoPhieu;
        private System.Windows.Forms.DataGridView dgvNhapHang;
        private System.Windows.Forms.DataGridViewCheckBoxColumn colChon;
        private System.Windows.Forms.DataGridViewTextBoxColumn colMaPhieu;
        private System.Windows.Forms.DataGridViewTextBoxColumn colThoiGian;
        private System.Windows.Forms.DataGridViewTextBoxColumn colKho;
        private System.Windows.Forms.DataGridViewTextBoxColumn colMaNcc;
        private System.Windows.Forms.DataGridViewTextBoxColumn colTenNcc;
        private System.Windows.Forms.DataGridViewTextBoxColumn colCanTraNcc;
        private System.Windows.Forms.DataGridViewTextBoxColumn colTongTien;
        private System.Windows.Forms.DataGridViewTextBoxColumn colTrangThai;
        private System.Windows.Forms.DataGridViewTextBoxColumn colMaPhieuNhap;
        private System.Windows.Forms.Label lblSoDongMoiTrang;
        private System.Windows.Forms.ComboBox cboSoDongMoiTrang;
        private System.Windows.Forms.Button btnTrangTruoc;
        private System.Windows.Forms.Label lblTrangHienTai;
        private System.Windows.Forms.Button btnTrangSau;
    }
}
