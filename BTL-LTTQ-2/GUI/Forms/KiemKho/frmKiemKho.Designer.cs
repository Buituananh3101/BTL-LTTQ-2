namespace BTL_LTTQ_2.GUI.Forms.KiemKho
{
    partial class frmKiemKho
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
            this.btnKiemKho = new System.Windows.Forms.Button();
            this.lblTimKiem = new System.Windows.Forms.Label();
            this.txtTimKiem = new System.Windows.Forms.TextBox();
            this.pnlBoLoc = new System.Windows.Forms.Panel();
            this.grpBoLoc = new System.Windows.Forms.GroupBox();
            this.lblKho = new System.Windows.Forms.Label();
            this.cboKho = new System.Windows.Forms.ComboBox();
            this.lblNguoiTao = new System.Windows.Forms.Label();
            this.cboNguoiTao = new System.Windows.Forms.ComboBox();
            this.lblTrangThai = new System.Windows.Forms.Label();
            this.cboTrangThai = new System.Windows.Forms.ComboBox();
            this.lblThoiGianTao = new System.Windows.Forms.Label();
            this.grpThoiGianTao = new System.Windows.Forms.GroupBox();
            this.dtpTuNgay = new System.Windows.Forms.DateTimePicker();
            this.dtpDenNgay = new System.Windows.Forms.DateTimePicker();
            this.lblTuNgay = new System.Windows.Forms.Label();
            this.lblDenNgay = new System.Windows.Forms.Label();
            this.pnlPhanTrang = new System.Windows.Forms.Panel();
            this.lblTongSoPhieu = new System.Windows.Forms.Label();
            this.lblSoDongMoiTrang = new System.Windows.Forms.Label();
            this.cboSoDongMoiTrang = new System.Windows.Forms.ComboBox();
            this.btnTrangTruoc = new System.Windows.Forms.Button();
            this.lblTrangHienTai = new System.Windows.Forms.Label();
            this.btnTrangSau = new System.Windows.Forms.Button();
            this.dgvKiemKho = new System.Windows.Forms.DataGridView();
            this.colChon = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.colMaPhieu = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colThoiGian = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colTongThucTe = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colTongChenhLech = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colSlLechTang = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colSlLechGiam = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colNgayCanBang = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colSlThucTe = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colGhiChu = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colTrangThai = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colMaPhieuKiemKe = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.splitter1 = new System.Windows.Forms.Splitter();
            this.pnlTop.SuspendLayout();
            this.grpTop.SuspendLayout();
            this.pnlBoLoc.SuspendLayout();
            this.grpBoLoc.SuspendLayout();
            this.grpThoiGianTao.SuspendLayout();
            this.pnlPhanTrang.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvKiemKho)).BeginInit();
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
            this.grpTop.Controls.Add(this.btnKiemKho);
            this.grpTop.Controls.Add(this.lblTimKiem);
            this.grpTop.Controls.Add(this.txtTimKiem);
            this.grpTop.Location = new System.Drawing.Point(0, 3);
            this.grpTop.Name = "grpTop";
            this.grpTop.Size = new System.Drawing.Size(1782, 65);
            this.grpTop.TabIndex = 0;
            this.grpTop.TabStop = false;
            // 
            // lblTieuDe
            // 
            this.lblTieuDe.AutoSize = true;
            this.lblTieuDe.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.lblTieuDe.Location = new System.Drawing.Point(12, 23);
            this.lblTieuDe.Name = "lblTieuDe";
            this.lblTieuDe.Size = new System.Drawing.Size(158, 28);
            this.lblTieuDe.TabIndex = 0;
            this.lblTieuDe.Text = "Phiếu kiểm kho";
            // 
            // btnHuongDanSuDung
            // 
            this.btnHuongDanSuDung.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnHuongDanSuDung.Location = new System.Drawing.Point(1721, 21);
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
            this.btnHienCot.Location = new System.Drawing.Point(1620, 21);
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
            this.btnXuatFile.Location = new System.Drawing.Point(1519, 21);
            this.btnXuatFile.Name = "btnXuatFile";
            this.btnXuatFile.Size = new System.Drawing.Size(95, 30);
            this.btnXuatFile.TabIndex = 4;
            this.btnXuatFile.Text = "Xuất file";
            this.btnXuatFile.UseVisualStyleBackColor = true;
            this.btnXuatFile.Click += new System.EventHandler(this.btnXuatFile_Click);
            // 
            // btnKiemKho
            // 
            this.btnKiemKho.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnKiemKho.Location = new System.Drawing.Point(1382, 21);
            this.btnKiemKho.Name = "btnKiemKho";
            this.btnKiemKho.Size = new System.Drawing.Size(130, 30);
            this.btnKiemKho.TabIndex = 3;
            this.btnKiemKho.Text = "+ Kiểm kho";
            this.btnKiemKho.UseVisualStyleBackColor = true;
            this.btnKiemKho.Click += new System.EventHandler(this.btnKiemKho_Click);
            // 
            // lblTimKiem
            // 
            this.lblTimKiem.AutoSize = true;
            this.lblTimKiem.Location = new System.Drawing.Point(361, 26);
            this.lblTimKiem.Name = "lblTimKiem";
            this.lblTimKiem.Size = new System.Drawing.Size(77, 21);
            this.lblTimKiem.TabIndex = 1;
            this.lblTimKiem.Text = "Tìm kiếm:";
            // 
            // txtTimKiem
            // 
            this.txtTimKiem.Location = new System.Drawing.Point(444, 22);
            this.txtTimKiem.Name = "txtTimKiem";
            this.txtTimKiem.Size = new System.Drawing.Size(537, 29);
            this.txtTimKiem.TabIndex = 2;
            this.txtTimKiem.TextChanged += new System.EventHandler(this.txtTimKiem_TextChanged);
            // 
            // pnlBoLoc
            // 
            this.pnlBoLoc.Controls.Add(this.splitter1);
            this.pnlBoLoc.Controls.Add(this.grpBoLoc);
            this.pnlBoLoc.Dock = System.Windows.Forms.DockStyle.Left;
            this.pnlBoLoc.Location = new System.Drawing.Point(0, 84);
            this.pnlBoLoc.Name = "pnlBoLoc";
            this.pnlBoLoc.Padding = new System.Windows.Forms.Padding(12, 0, 0, 10);
            this.pnlBoLoc.Size = new System.Drawing.Size(360, 769);
            this.pnlBoLoc.TabIndex = 1;
            // 
            // grpBoLoc
            // 
            this.grpBoLoc.Controls.Add(this.lblKho);
            this.grpBoLoc.Controls.Add(this.cboKho);
            this.grpBoLoc.Controls.Add(this.lblNguoiTao);
            this.grpBoLoc.Controls.Add(this.cboNguoiTao);
            this.grpBoLoc.Controls.Add(this.lblTrangThai);
            this.grpBoLoc.Controls.Add(this.cboTrangThai);
            this.grpBoLoc.Controls.Add(this.lblThoiGianTao);
            this.grpBoLoc.Controls.Add(this.grpThoiGianTao);
            this.grpBoLoc.Location = new System.Drawing.Point(12, 6);
            this.grpBoLoc.Name = "grpBoLoc";
            this.grpBoLoc.Size = new System.Drawing.Size(332, 719);
            this.grpBoLoc.TabIndex = 0;
            this.grpBoLoc.TabStop = false;
            // 
            // lblKho
            // 
            this.lblKho.AutoSize = true;
            this.lblKho.Location = new System.Drawing.Point(9, 244);
            this.lblKho.Name = "lblKho";
            this.lblKho.Size = new System.Drawing.Size(114, 21);
            this.lblKho.TabIndex = 4;
            this.lblKho.Text = "Chi nhánh kho:";
            // 
            // cboKho
            // 
            this.cboKho.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboKho.FormattingEnabled = true;
            this.cboKho.Location = new System.Drawing.Point(9, 269);
            this.cboKho.Name = "cboKho";
            this.cboKho.Size = new System.Drawing.Size(308, 29);
            this.cboKho.TabIndex = 5;
            this.cboKho.SelectedIndexChanged += new System.EventHandler(this.BoLoc_ThayDoi);
            // 
            // lblNguoiTao
            // 
            this.lblNguoiTao.AutoSize = true;
            this.lblNguoiTao.Location = new System.Drawing.Point(9, 134);
            this.lblNguoiTao.Name = "lblNguoiTao";
            this.lblNguoiTao.Size = new System.Drawing.Size(83, 21);
            this.lblNguoiTao.TabIndex = 2;
            this.lblNguoiTao.Text = "Người tạo:";
            // 
            // cboNguoiTao
            // 
            this.cboNguoiTao.FormattingEnabled = true;
            this.cboNguoiTao.Location = new System.Drawing.Point(9, 159);
            this.cboNguoiTao.Name = "cboNguoiTao";
            this.cboNguoiTao.Size = new System.Drawing.Size(308, 29);
            this.cboNguoiTao.TabIndex = 3;
            // 
            // lblTrangThai
            // 
            this.lblTrangThai.AutoSize = true;
            this.lblTrangThai.Location = new System.Drawing.Point(9, 27);
            this.lblTrangThai.Name = "lblTrangThai";
            this.lblTrangThai.Size = new System.Drawing.Size(82, 21);
            this.lblTrangThai.TabIndex = 0;
            this.lblTrangThai.Text = "Trạng thái:";
            // 
            // cboTrangThai
            // 
            this.cboTrangThai.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboTrangThai.FormattingEnabled = true;
            this.cboTrangThai.Location = new System.Drawing.Point(9, 52);
            this.cboTrangThai.Name = "cboTrangThai";
            this.cboTrangThai.Size = new System.Drawing.Size(308, 29);
            this.cboTrangThai.TabIndex = 1;
            this.cboTrangThai.SelectedIndexChanged += new System.EventHandler(this.BoLoc_ThayDoi);
            // 
            // lblThoiGianTao
            // 
            this.lblThoiGianTao.AutoSize = true;
            this.lblThoiGianTao.Location = new System.Drawing.Point(9, 354);
            this.lblThoiGianTao.Name = "lblThoiGianTao";
            this.lblThoiGianTao.Size = new System.Drawing.Size(104, 21);
            this.lblThoiGianTao.TabIndex = 6;
            this.lblThoiGianTao.Text = "Thời gian tạo:";
            // 
            // grpThoiGianTao
            // 
            this.grpThoiGianTao.Controls.Add(this.dtpTuNgay);
            this.grpThoiGianTao.Controls.Add(this.dtpDenNgay);
            this.grpThoiGianTao.Controls.Add(this.lblTuNgay);
            this.grpThoiGianTao.Controls.Add(this.lblDenNgay);
            this.grpThoiGianTao.Location = new System.Drawing.Point(9, 379);
            this.grpThoiGianTao.Name = "grpThoiGianTao";
            this.grpThoiGianTao.Size = new System.Drawing.Size(308, 100);
            this.grpThoiGianTao.TabIndex = 7;
            this.grpThoiGianTao.TabStop = false;
            // 
            // dtpTuNgay
            // 
            this.dtpTuNgay.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpTuNgay.Location = new System.Drawing.Point(60, 22);
            this.dtpTuNgay.Name = "dtpTuNgay";
            this.dtpTuNgay.Size = new System.Drawing.Size(248, 29);
            this.dtpTuNgay.TabIndex = 1;
            this.dtpTuNgay.ValueChanged += new System.EventHandler(this.BoLoc_ThayDoi);
            // 
            // dtpDenNgay
            // 
            this.dtpDenNgay.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpDenNgay.Location = new System.Drawing.Point(60, 58);
            this.dtpDenNgay.Name = "dtpDenNgay";
            this.dtpDenNgay.Size = new System.Drawing.Size(248, 29);
            this.dtpDenNgay.TabIndex = 3;
            this.dtpDenNgay.ValueChanged += new System.EventHandler(this.BoLoc_ThayDoi);
            // 
            // lblTuNgay
            // 
            this.lblTuNgay.AutoSize = true;
            this.lblTuNgay.Location = new System.Drawing.Point(10, 26);
            this.lblTuNgay.Name = "lblTuNgay";
            this.lblTuNgay.Size = new System.Drawing.Size(30, 21);
            this.lblTuNgay.TabIndex = 0;
            this.lblTuNgay.Text = "Từ:";
            // 
            // lblDenNgay
            // 
            this.lblDenNgay.AutoSize = true;
            this.lblDenNgay.Location = new System.Drawing.Point(10, 62);
            this.lblDenNgay.Name = "lblDenNgay";
            this.lblDenNgay.Size = new System.Drawing.Size(41, 21);
            this.lblDenNgay.TabIndex = 2;
            this.lblDenNgay.Text = "Đến:";
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
            this.pnlPhanTrang.Location = new System.Drawing.Point(360, 809);
            this.pnlPhanTrang.Name = "pnlPhanTrang";
            this.pnlPhanTrang.Size = new System.Drawing.Size(1422, 44);
            this.pnlPhanTrang.TabIndex = 4;
            // 
            // lblTongSoPhieu
            // 
            this.lblTongSoPhieu.AutoSize = true;
            this.lblTongSoPhieu.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold);
            this.lblTongSoPhieu.Location = new System.Drawing.Point(12, 12);
            this.lblTongSoPhieu.Name = "lblTongSoPhieu";
            this.lblTongSoPhieu.Size = new System.Drawing.Size(177, 21);
            this.lblTongSoPhieu.TabIndex = 0;
            this.lblTongSoPhieu.Text = "Tổng số: 0 phiếu kiểm";
            // 
            // lblSoDongMoiTrang
            // 
            this.lblSoDongMoiTrang.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblSoDongMoiTrang.AutoSize = true;
            this.lblSoDongMoiTrang.Location = new System.Drawing.Point(1048, 12);
            this.lblSoDongMoiTrang.Name = "lblSoDongMoiTrang";
            this.lblSoDongMoiTrang.Size = new System.Drawing.Size(67, 21);
            this.lblSoDongMoiTrang.TabIndex = 1;
            this.lblSoDongMoiTrang.Text = "Hiển thị:";
            // 
            // cboSoDongMoiTrang
            // 
            this.cboSoDongMoiTrang.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.cboSoDongMoiTrang.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboSoDongMoiTrang.FormattingEnabled = true;
            this.cboSoDongMoiTrang.Items.AddRange(new object[] {
            "10",
            "15",
            "20",
            "50",
            "100"});
            this.cboSoDongMoiTrang.Location = new System.Drawing.Point(1123, 8);
            this.cboSoDongMoiTrang.Name = "cboSoDongMoiTrang";
            this.cboSoDongMoiTrang.Size = new System.Drawing.Size(70, 29);
            this.cboSoDongMoiTrang.TabIndex = 2;
            this.cboSoDongMoiTrang.SelectedIndexChanged += new System.EventHandler(this.cboSoDongMoiTrang_SelectedIndexChanged);
            // 
            // btnTrangTruoc
            // 
            this.btnTrangTruoc.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnTrangTruoc.Location = new System.Drawing.Point(1218, 7);
            this.btnTrangTruoc.Name = "btnTrangTruoc";
            this.btnTrangTruoc.Size = new System.Drawing.Size(40, 30);
            this.btnTrangTruoc.TabIndex = 3;
            this.btnTrangTruoc.Text = "<";
            this.btnTrangTruoc.UseVisualStyleBackColor = true;
            this.btnTrangTruoc.Click += new System.EventHandler(this.btnTrangTruoc_Click);
            // 
            // lblTrangHienTai
            // 
            this.lblTrangHienTai.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblTrangHienTai.AutoSize = true;
            this.lblTrangHienTai.Location = new System.Drawing.Point(1268, 12);
            this.lblTrangHienTai.Name = "lblTrangHienTai";
            this.lblTrangHienTai.Size = new System.Drawing.Size(42, 21);
            this.lblTrangHienTai.TabIndex = 4;
            this.lblTrangHienTai.Text = "1 / 1";
            // 
            // btnTrangSau
            // 
            this.btnTrangSau.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnTrangSau.Location = new System.Drawing.Point(1333, 7);
            this.btnTrangSau.Name = "btnTrangSau";
            this.btnTrangSau.Size = new System.Drawing.Size(40, 30);
            this.btnTrangSau.TabIndex = 5;
            this.btnTrangSau.Text = ">";
            this.btnTrangSau.UseVisualStyleBackColor = true;
            this.btnTrangSau.Click += new System.EventHandler(this.btnTrangSau_Click);
            // 
            // dgvKiemKho
            // 
            this.dgvKiemKho.AllowUserToAddRows = false;
            this.dgvKiemKho.AllowUserToDeleteRows = false;
            this.dgvKiemKho.BackgroundColor = System.Drawing.Color.White;
            this.dgvKiemKho.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dgvKiemKho.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvKiemKho.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colChon,
            this.colMaPhieu,
            this.colThoiGian,
            this.colTongThucTe,
            this.colTongChenhLech,
            this.colSlLechTang,
            this.colSlLechGiam,
            this.colNgayCanBang,
            this.colSlThucTe,
            this.colGhiChu,
            this.colTrangThai,
            this.colMaPhieuKiemKe});
            this.dgvKiemKho.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvKiemKho.Location = new System.Drawing.Point(360, 84);
            this.dgvKiemKho.Name = "dgvKiemKho";
            this.dgvKiemKho.ReadOnly = true;
            this.dgvKiemKho.RowHeadersVisible = false;
            this.dgvKiemKho.RowHeadersWidth = 51;
            this.dgvKiemKho.RowTemplate.Height = 28;
            this.dgvKiemKho.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvKiemKho.Size = new System.Drawing.Size(1422, 725);
            this.dgvKiemKho.TabIndex = 3;
            this.dgvKiemKho.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvKiemKho_CellDoubleClick);
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
            this.colMaPhieu.DataPropertyName = "MaPhieu";
            this.colMaPhieu.HeaderText = "Mã kiểm kho";
            this.colMaPhieu.MinimumWidth = 100;
            this.colMaPhieu.Name = "colMaPhieu";
            this.colMaPhieu.ReadOnly = true;
            this.colMaPhieu.Width = 115;
            // 
            // colThoiGian
            // 
            this.colThoiGian.DataPropertyName = "ThoiGian";
            this.colThoiGian.HeaderText = "Thời gian";
            this.colThoiGian.MinimumWidth = 120;
            this.colThoiGian.Name = "colThoiGian";
            this.colThoiGian.ReadOnly = true;
            this.colThoiGian.Width = 135;
            // 
            // colTongThucTe
            // 
            this.colTongThucTe.DataPropertyName = "TongGiaTriThucTe";
            this.colTongThucTe.HeaderText = "Tổng thực tế";
            this.colTongThucTe.MinimumWidth = 110;
            this.colTongThucTe.Name = "colTongThucTe";
            this.colTongThucTe.ReadOnly = true;
            this.colTongThucTe.Width = 130;
            // 
            // colTongChenhLech
            // 
            this.colTongChenhLech.DataPropertyName = "TongGiaTriChenhLech";
            this.colTongChenhLech.HeaderText = "Tổng chênh lệch";
            this.colTongChenhLech.MinimumWidth = 110;
            this.colTongChenhLech.Name = "colTongChenhLech";
            this.colTongChenhLech.ReadOnly = true;
            this.colTongChenhLech.Width = 130;
            // 
            // colSlLechTang
            // 
            this.colSlLechTang.DataPropertyName = "SlLechTang";
            this.colSlLechTang.HeaderText = "SL lệch tăng";
            this.colSlLechTang.MinimumWidth = 85;
            this.colSlLechTang.Name = "colSlLechTang";
            this.colSlLechTang.ReadOnly = true;
            this.colSlLechTang.Width = 95;
            // 
            // colSlLechGiam
            // 
            this.colSlLechGiam.DataPropertyName = "SlLechGiam";
            this.colSlLechGiam.HeaderText = "SL lệch giảm";
            this.colSlLechGiam.MinimumWidth = 85;
            this.colSlLechGiam.Name = "colSlLechGiam";
            this.colSlLechGiam.ReadOnly = true;
            this.colSlLechGiam.Width = 95;
            // 
            // colNgayCanBang
            // 
            this.colNgayCanBang.DataPropertyName = "NgayCanBang";
            this.colNgayCanBang.HeaderText = "Ngày cân bằng";
            this.colNgayCanBang.MinimumWidth = 120;
            this.colNgayCanBang.Name = "colNgayCanBang";
            this.colNgayCanBang.ReadOnly = true;
            this.colNgayCanBang.Width = 135;
            // 
            // colSlThucTe
            // 
            this.colSlThucTe.DataPropertyName = "TongSlThucTe";
            this.colSlThucTe.HeaderText = "SL thực tế";
            this.colSlThucTe.MinimumWidth = 85;
            this.colSlThucTe.Name = "colSlThucTe";
            this.colSlThucTe.ReadOnly = true;
            this.colSlThucTe.Width = 95;
            // 
            // colGhiChu
            // 
            this.colGhiChu.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.colGhiChu.DataPropertyName = "GhiChu";
            this.colGhiChu.HeaderText = "Ghi chú";
            this.colGhiChu.MinimumWidth = 120;
            this.colGhiChu.Name = "colGhiChu";
            this.colGhiChu.ReadOnly = true;
            // 
            // colTrangThai
            // 
            this.colTrangThai.DataPropertyName = "TenTrangThai";
            this.colTrangThai.HeaderText = "Trạng thái";
            this.colTrangThai.MinimumWidth = 100;
            this.colTrangThai.Name = "colTrangThai";
            this.colTrangThai.ReadOnly = true;
            this.colTrangThai.Width = 120;
            // 
            // colMaPhieuKiemKe
            // 
            this.colMaPhieuKiemKe.DataPropertyName = "MaPhieuKiemKe";
            this.colMaPhieuKiemKe.HeaderText = "MaPhieuKiemKe";
            this.colMaPhieuKiemKe.MinimumWidth = 6;
            this.colMaPhieuKiemKe.Name = "colMaPhieuKiemKe";
            this.colMaPhieuKiemKe.ReadOnly = true;
            this.colMaPhieuKiemKe.Visible = false;
            this.colMaPhieuKiemKe.Width = 125;
            // 
            // splitter1
            // 
            this.splitter1.BackColor = System.Drawing.SystemColors.ActiveBorder;
            this.splitter1.Dock = System.Windows.Forms.DockStyle.Top;
            this.splitter1.Location = new System.Drawing.Point(12, 0);
            this.splitter1.Name = "splitter1";
            this.splitter1.Size = new System.Drawing.Size(348, 1);
            this.splitter1.TabIndex = 1;
            this.splitter1.TabStop = false;
            // 
            // frmKiemKho
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 21F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1782, 853);
            this.Controls.Add(this.dgvKiemKho);
            this.Controls.Add(this.pnlPhanTrang);
            this.Controls.Add(this.pnlBoLoc);
            this.Controls.Add(this.pnlTop);
            this.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.MaximizeBox = false;
            this.Name = "frmKiemKho";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Quản lý phiếu kiểm kho";
            this.Load += new System.EventHandler(this.frmKiemKho_Load);
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
            ((System.ComponentModel.ISupportInitialize)(this.dgvKiemKho)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pnlTop;
        private System.Windows.Forms.GroupBox grpTop;
        private System.Windows.Forms.Label lblTieuDe;
        private System.Windows.Forms.Button btnHuongDanSuDung;
        private System.Windows.Forms.Button btnHienCot;
        private System.Windows.Forms.Button btnXuatFile;
        private System.Windows.Forms.Button btnKiemKho;
        private System.Windows.Forms.Label lblTimKiem;
        private System.Windows.Forms.TextBox txtTimKiem;
        private System.Windows.Forms.Panel pnlBoLoc;
        private System.Windows.Forms.GroupBox grpBoLoc;
        private System.Windows.Forms.Label lblThoiGianTao;
        private System.Windows.Forms.GroupBox grpThoiGianTao;
        private System.Windows.Forms.DateTimePicker dtpTuNgay;
        private System.Windows.Forms.DateTimePicker dtpDenNgay;
        private System.Windows.Forms.Label lblTuNgay;
        private System.Windows.Forms.Label lblDenNgay;
        private System.Windows.Forms.Label lblTrangThai;
        private System.Windows.Forms.ComboBox cboTrangThai;
        private System.Windows.Forms.Label lblNguoiTao;
        private System.Windows.Forms.ComboBox cboNguoiTao;
        private System.Windows.Forms.Label lblKho;
        private System.Windows.Forms.ComboBox cboKho;
        private System.Windows.Forms.Panel pnlPhanTrang;
        private System.Windows.Forms.Label lblTongSoPhieu;
        private System.Windows.Forms.Label lblSoDongMoiTrang;
        private System.Windows.Forms.ComboBox cboSoDongMoiTrang;
        private System.Windows.Forms.Button btnTrangTruoc;
        private System.Windows.Forms.Label lblTrangHienTai;
        private System.Windows.Forms.Button btnTrangSau;
        private System.Windows.Forms.DataGridView dgvKiemKho;
        private System.Windows.Forms.DataGridViewCheckBoxColumn colChon;
        private System.Windows.Forms.DataGridViewTextBoxColumn colMaPhieu;
        private System.Windows.Forms.DataGridViewTextBoxColumn colThoiGian;
        private System.Windows.Forms.DataGridViewTextBoxColumn colTongThucTe;
        private System.Windows.Forms.DataGridViewTextBoxColumn colTongChenhLech;
        private System.Windows.Forms.DataGridViewTextBoxColumn colSlLechTang;
        private System.Windows.Forms.DataGridViewTextBoxColumn colSlLechGiam;
        private System.Windows.Forms.DataGridViewTextBoxColumn colNgayCanBang;
        private System.Windows.Forms.DataGridViewTextBoxColumn colSlThucTe;
        private System.Windows.Forms.DataGridViewTextBoxColumn colGhiChu;
        private System.Windows.Forms.DataGridViewTextBoxColumn colTrangThai;
        private System.Windows.Forms.DataGridViewTextBoxColumn colMaPhieuKiemKe;
        private System.Windows.Forms.Splitter splitter1;
    }
}
