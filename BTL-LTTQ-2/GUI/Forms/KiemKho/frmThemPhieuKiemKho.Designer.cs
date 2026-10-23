namespace BTL_LTTQ_2.GUI.Forms.KiemKho
{
    partial class frmThemPhieuKiemKho
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
            this.btnNhapFile = new System.Windows.Forms.Button();
            this.btnLuuTam = new System.Windows.Forms.Button();
            this.btnCanBangKho = new System.Windows.Forms.Button();
            this.btnDong = new System.Windows.Forms.Button();
            this.grpMaster = new System.Windows.Forms.GroupBox();
            this.txtGhiChu = new System.Windows.Forms.TextBox();
            this.lblGhiChu = new System.Windows.Forms.Label();
            this.dtpThoiDiemChot = new System.Windows.Forms.DateTimePicker();
            this.lblNgayKiem = new System.Windows.Forms.Label();
            this.cboNguoiKiem = new System.Windows.Forms.ComboBox();
            this.lblNguoiKiem = new System.Windows.Forms.Label();
            this.cboKho = new System.Windows.Forms.ComboBox();
            this.lblKho = new System.Windows.Forms.Label();
            this.lblMaPhieuVal = new System.Windows.Forms.Label();
            this.lblMaPhieu = new System.Windows.Forms.Label();
            this.pnlThemHang = new System.Windows.Forms.Panel();
            this.btnNapToanBoHang = new System.Windows.Forms.Button();
            this.btnThemHang = new System.Windows.Forms.Button();
            this.nudSoLuongThucTe = new System.Windows.Forms.NumericUpDown();
            this.lblSlThucTe = new System.Windows.Forms.Label();
            this.cboHangHoa = new System.Windows.Forms.ComboBox();
            this.lblChonHang = new System.Windows.Forms.Label();
            this.pnlCenter = new System.Windows.Forms.Panel();
            this.dgvHangKiem = new System.Windows.Forms.DataGridView();
            this.colStt = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colMaHangNoiBo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colTenHang = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colDonViTinh = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colTonKho = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colSoLuongThucTe = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colSlChenhLech = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colDonGia = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colGiaTriChenhLech = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colXoaDong = new System.Windows.Forms.DataGridViewButtonColumn();
            this.pnlBottom = new System.Windows.Forms.Panel();
            this.lblTongChenhLechVal = new System.Windows.Forms.Label();
            this.lblTongChenhLechLbl = new System.Windows.Forms.Label();
            this.lblTongLechGiamVal = new System.Windows.Forms.Label();
            this.lblTongLechGiamLbl = new System.Windows.Forms.Label();
            this.lblTongLechTangVal = new System.Windows.Forms.Label();
            this.lblTongLechTangLbl = new System.Windows.Forms.Label();
            this.lblTongThucTeVal = new System.Windows.Forms.Label();
            this.lblTongThucTeLbl = new System.Windows.Forms.Label();
            this.lblTongSoHangVal = new System.Windows.Forms.Label();
            this.lblTongSoHangLbl = new System.Windows.Forms.Label();
            this.pnlTop.SuspendLayout();
            this.grpMaster.SuspendLayout();
            this.pnlThemHang.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudSoLuongThucTe)).BeginInit();
            this.pnlCenter.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvHangKiem)).BeginInit();
            this.pnlBottom.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlTop
            // 
            this.pnlTop.BackColor = System.Drawing.Color.White;
            this.pnlTop.Controls.Add(this.btnDong);
            this.pnlTop.Controls.Add(this.btnCanBangKho);
            this.pnlTop.Controls.Add(this.btnLuuTam);
            this.pnlTop.Controls.Add(this.btnNhapFile);
            this.pnlTop.Controls.Add(this.lblTieuDe);
            this.pnlTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlTop.Location = new System.Drawing.Point(0, 0);
            this.pnlTop.Name = "pnlTop";
            this.pnlTop.Padding = new System.Windows.Forms.Padding(15, 10, 15, 10);
            this.pnlTop.Size = new System.Drawing.Size(1200, 60);
            this.pnlTop.TabIndex = 0;
            // 
            // lblTieuDe
            // 
            this.lblTieuDe.AutoSize = true;
            this.lblTieuDe.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.lblTieuDe.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(23)))), ((int)(((byte)(42)))));
            this.lblTieuDe.Location = new System.Drawing.Point(12, 14);
            this.lblTieuDe.Name = "lblTieuDe";
            this.lblTieuDe.Size = new System.Drawing.Size(262, 31);
            this.lblTieuDe.TabIndex = 0;
            this.lblTieuDe.Text = "Thêm phiếu kiểm kho";
            // 
            // btnNhapFile
            // 
            this.btnNhapFile.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnNhapFile.Location = new System.Drawing.Point(620, 12);
            this.btnNhapFile.Name = "btnNhapFile";
            this.btnNhapFile.Size = new System.Drawing.Size(120, 36);
            this.btnNhapFile.TabIndex = 1;
            this.btnNhapFile.Text = "Nhập file";
            this.btnNhapFile.UseVisualStyleBackColor = true;
            this.btnNhapFile.Click += new System.EventHandler(this.btnNhapFile_Click);
            // 
            // btnLuuTam
            // 
            this.btnLuuTam.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnLuuTam.Location = new System.Drawing.Point(750, 12);
            this.btnLuuTam.Name = "btnLuuTam";
            this.btnLuuTam.Size = new System.Drawing.Size(135, 36);
            this.btnLuuTam.TabIndex = 2;
            this.btnLuuTam.Text = "Lưu tạm (F9)";
            this.btnLuuTam.UseVisualStyleBackColor = true;
            this.btnLuuTam.Click += new System.EventHandler(this.btnLuuTam_Click);
            // 
            // btnCanBangKho
            // 
            this.btnCanBangKho.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCanBangKho.Location = new System.Drawing.Point(895, 12);
            this.btnCanBangKho.Name = "btnCanBangKho";
            this.btnCanBangKho.Size = new System.Drawing.Size(175, 36);
            this.btnCanBangKho.TabIndex = 3;
            this.btnCanBangKho.Text = "Cân bằng kho (F10)";
            this.btnCanBangKho.UseVisualStyleBackColor = true;
            this.btnCanBangKho.Click += new System.EventHandler(this.btnCanBangKho_Click);
            // 
            // btnDong
            // 
            this.btnDong.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnDong.Location = new System.Drawing.Point(1080, 12);
            this.btnDong.Name = "btnDong";
            this.btnDong.Size = new System.Drawing.Size(100, 36);
            this.btnDong.TabIndex = 4;
            this.btnDong.Text = "Đóng";
            this.btnDong.UseVisualStyleBackColor = true;
            this.btnDong.Click += new System.EventHandler(this.btnDong_Click);
            // 
            // grpMaster
            // 
            this.grpMaster.Controls.Add(this.txtGhiChu);
            this.grpMaster.Controls.Add(this.lblGhiChu);
            this.grpMaster.Controls.Add(this.dtpThoiDiemChot);
            this.grpMaster.Controls.Add(this.lblNgayKiem);
            this.grpMaster.Controls.Add(this.cboNguoiKiem);
            this.grpMaster.Controls.Add(this.lblNguoiKiem);
            this.grpMaster.Controls.Add(this.cboKho);
            this.grpMaster.Controls.Add(this.lblKho);
            this.grpMaster.Controls.Add(this.lblMaPhieuVal);
            this.grpMaster.Controls.Add(this.lblMaPhieu);
            this.grpMaster.Dock = System.Windows.Forms.DockStyle.Top;
            this.grpMaster.Location = new System.Drawing.Point(0, 60);
            this.grpMaster.Name = "grpMaster";
            this.grpMaster.Size = new System.Drawing.Size(1200, 105);
            this.grpMaster.TabIndex = 1;
            this.grpMaster.TabStop = false;
            this.grpMaster.Text = "Thông tin phiếu kiểm";
            // 
            // txtGhiChu
            // 
            this.txtGhiChu.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtGhiChu.Location = new System.Drawing.Point(400, 63);
            this.txtGhiChu.Name = "txtGhiChu";
            this.txtGhiChu.Size = new System.Drawing.Size(780, 29);
            this.txtGhiChu.TabIndex = 9;
            // 
            // lblGhiChu
            // 
            this.lblGhiChu.AutoSize = true;
            this.lblGhiChu.Location = new System.Drawing.Point(320, 67);
            this.lblGhiChu.Name = "lblGhiChu";
            this.lblGhiChu.Size = new System.Drawing.Size(66, 21);
            this.lblGhiChu.TabIndex = 8;
            this.lblGhiChu.Text = "Ghi chú:";
            // 
            // dtpThoiDiemChot
            // 
            this.dtpThoiDiemChot.CustomFormat = "dd/MM/yyyy HH:mm";
            this.dtpThoiDiemChot.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpThoiDiemChot.Location = new System.Drawing.Point(115, 63);
            this.dtpThoiDiemChot.Name = "dtpThoiDiemChot";
            this.dtpThoiDiemChot.Size = new System.Drawing.Size(175, 29);
            this.dtpThoiDiemChot.TabIndex = 7;
            // 
            // lblNgayKiem
            // 
            this.lblNgayKiem.AutoSize = true;
            this.lblNgayKiem.Location = new System.Drawing.Point(18, 67);
            this.lblNgayKiem.Name = "lblNgayKiem";
            this.lblNgayKiem.Size = new System.Drawing.Size(87, 21);
            this.lblNgayKiem.TabIndex = 6;
            this.lblNgayKiem.Text = "Thời điểm:";
            // 
            // cboNguoiKiem
            // 
            this.cboNguoiKiem.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.cboNguoiKiem.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboNguoiKiem.FormattingEnabled = true;
            this.cboNguoiKiem.Location = new System.Drawing.Point(880, 23);
            this.cboNguoiKiem.Name = "cboNguoiKiem";
            this.cboNguoiKiem.Size = new System.Drawing.Size(300, 29);
            this.cboNguoiKiem.TabIndex = 5;
            // 
            // lblNguoiKiem
            // 
            this.lblNguoiKiem.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblNguoiKiem.AutoSize = true;
            this.lblNguoiKiem.Location = new System.Drawing.Point(775, 27);
            this.lblNguoiKiem.Name = "lblNguoiKiem";
            this.lblNguoiKiem.Size = new System.Drawing.Size(96, 21);
            this.lblNguoiKiem.TabIndex = 4;
            this.lblNguoiKiem.Text = "Người kiểm:";
            // 
            // cboKho
            // 
            this.cboKho.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboKho.FormattingEnabled = true;
            this.cboKho.Location = new System.Drawing.Point(400, 23);
            this.cboKho.Name = "cboKho";
            this.cboKho.Size = new System.Drawing.Size(320, 29);
            this.cboKho.TabIndex = 3;
            this.cboKho.SelectedIndexChanged += new System.EventHandler(this.cboKho_SelectedIndexChanged);
            // 
            // lblKho
            // 
            this.lblKho.AutoSize = true;
            this.lblKho.Location = new System.Drawing.Point(320, 27);
            this.lblKho.Name = "lblKho";
            this.lblKho.Size = new System.Drawing.Size(77, 21);
            this.lblKho.TabIndex = 2;
            this.lblKho.Text = "Kho kiểm:";
            // 
            // lblMaPhieuVal
            // 
            this.lblMaPhieuVal.AutoSize = true;
            this.lblMaPhieuVal.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold);
            this.lblMaPhieuVal.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(64)))), ((int)(((byte)(175)))));
            this.lblMaPhieuVal.Location = new System.Drawing.Point(115, 27);
            this.lblMaPhieuVal.Name = "lblMaPhieuVal";
            this.lblMaPhieuVal.Size = new System.Drawing.Size(100, 21);
            this.lblMaPhieuVal.TabIndex = 1;
            this.lblMaPhieuVal.Text = "PKK000001";
            // 
            // lblMaPhieu
            // 
            this.lblMaPhieu.AutoSize = true;
            this.lblMaPhieu.Location = new System.Drawing.Point(18, 27);
            this.lblMaPhieu.Name = "lblMaPhieu";
            this.lblMaPhieu.Size = new System.Drawing.Size(76, 21);
            this.lblMaPhieu.TabIndex = 0;
            this.lblMaPhieu.Text = "Mã phiếu:";
            // 
            // pnlThemHang
            // 
            this.pnlThemHang.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(250)))), ((int)(((byte)(252)))));
            this.pnlThemHang.Controls.Add(this.btnNapToanBoHang);
            this.pnlThemHang.Controls.Add(this.btnThemHang);
            this.pnlThemHang.Controls.Add(this.nudSoLuongThucTe);
            this.pnlThemHang.Controls.Add(this.lblSlThucTe);
            this.pnlThemHang.Controls.Add(this.cboHangHoa);
            this.pnlThemHang.Controls.Add(this.lblChonHang);
            this.pnlThemHang.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlThemHang.Location = new System.Drawing.Point(0, 165);
            this.pnlThemHang.Name = "pnlThemHang";
            this.pnlThemHang.Padding = new System.Windows.Forms.Padding(15, 8, 15, 8);
            this.pnlThemHang.Size = new System.Drawing.Size(1200, 52);
            this.pnlThemHang.TabIndex = 2;
            // 
            // btnNapToanBoHang
            // 
            this.btnNapToanBoHang.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnNapToanBoHang.Location = new System.Drawing.Point(965, 9);
            this.btnNapToanBoHang.Name = "btnNapToanBoHang";
            this.btnNapToanBoHang.Size = new System.Drawing.Size(215, 34);
            this.btnNapToanBoHang.TabIndex = 5;
            this.btnNapToanBoHang.Text = "Nạp tất cả hàng trong kho";
            this.btnNapToanBoHang.UseVisualStyleBackColor = true;
            this.btnNapToanBoHang.Click += new System.EventHandler(this.btnNapToanBoHang_Click);
            // 
            // btnThemHang
            // 
            this.btnThemHang.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnThemHang.Location = new System.Drawing.Point(825, 9);
            this.btnThemHang.Name = "btnThemHang";
            this.btnThemHang.Size = new System.Drawing.Size(130, 34);
            this.btnThemHang.TabIndex = 4;
            this.btnThemHang.Text = "+ Thêm hàng";
            this.btnThemHang.UseVisualStyleBackColor = true;
            this.btnThemHang.Click += new System.EventHandler(this.btnThemHang_Click);
            // 
            // nudSoLuongThucTe
            // 
            this.nudSoLuongThucTe.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.nudSoLuongThucTe.Location = new System.Drawing.Point(690, 12);
            this.nudSoLuongThucTe.Maximum = new decimal(new int[] {
            1000000,
            0,
            0,
            0});
            this.nudSoLuongThucTe.Name = "nudSoLuongThucTe";
            this.nudSoLuongThucTe.Size = new System.Drawing.Size(120, 29);
            this.nudSoLuongThucTe.TabIndex = 3;
            this.nudSoLuongThucTe.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.nudSoLuongThucTe.ThousandsSeparator = true;
            // 
            // lblSlThucTe
            // 
            this.lblSlThucTe.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblSlThucTe.AutoSize = true;
            this.lblSlThucTe.Location = new System.Drawing.Point(585, 16);
            this.lblSlThucTe.Name = "lblSlThucTe";
            this.lblSlThucTe.Size = new System.Drawing.Size(89, 21);
            this.lblSlThucTe.TabIndex = 2;
            this.lblSlThucTe.Text = "SL thực tế:";
            // 
            // cboHangHoa
            // 
            this.cboHangHoa.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cboHangHoa.FormattingEnabled = true;
            this.cboHangHoa.Location = new System.Drawing.Point(115, 12);
            this.cboHangHoa.Name = "cboHangHoa";
            this.cboHangHoa.Size = new System.Drawing.Size(450, 29);
            this.cboHangHoa.TabIndex = 1;
            this.cboHangHoa.SelectedIndexChanged += new System.EventHandler(this.cboHangHoa_SelectedIndexChanged);
            // 
            // lblChonHang
            // 
            this.lblChonHang.AutoSize = true;
            this.lblChonHang.Location = new System.Drawing.Point(18, 16);
            this.lblChonHang.Name = "lblChonHang";
            this.lblChonHang.Size = new System.Drawing.Size(88, 21);
            this.lblChonHang.TabIndex = 0;
            this.lblChonHang.Text = "Chọn hàng:";
            // 
            // pnlCenter
            // 
            this.pnlCenter.Controls.Add(this.dgvHangKiem);
            this.pnlCenter.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlCenter.Location = new System.Drawing.Point(0, 217);
            this.pnlCenter.Name = "pnlCenter";
            this.pnlCenter.Padding = new System.Windows.Forms.Padding(15, 5, 15, 5);
            this.pnlCenter.Size = new System.Drawing.Size(1200, 423);
            this.pnlCenter.TabIndex = 3;
            // 
            // dgvHangKiem
            // 
            this.dgvHangKiem.AllowUserToAddRows = false;
            this.dgvHangKiem.AllowUserToDeleteRows = false;
            this.dgvHangKiem.BackgroundColor = System.Drawing.Color.White;
            this.dgvHangKiem.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvHangKiem.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colStt,
            this.colMaHangNoiBo,
            this.colTenHang,
            this.colDonViTinh,
            this.colTonKho,
            this.colSoLuongThucTe,
            this.colSlChenhLech,
            this.colDonGia,
            this.colGiaTriChenhLech,
            this.colXoaDong});
            this.dgvHangKiem.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvHangKiem.Location = new System.Drawing.Point(15, 5);
            this.dgvHangKiem.Name = "dgvHangKiem";
            this.dgvHangKiem.RowHeadersVisible = false;
            this.dgvHangKiem.RowHeadersWidth = 51;
            this.dgvHangKiem.RowTemplate.Height = 28;
            this.dgvHangKiem.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.CellSelect;
            this.dgvHangKiem.Size = new System.Drawing.Size(1170, 413);
            this.dgvHangKiem.TabIndex = 0;
            this.dgvHangKiem.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvHangKiem_CellContentClick);
            this.dgvHangKiem.CellEndEdit += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvHangKiem_CellEndEdit);
            // 
            // colStt
            // 
            this.colStt.DataPropertyName = "STT";
            this.colStt.HeaderText = "STT";
            this.colStt.MinimumWidth = 6;
            this.colStt.Name = "colStt";
            this.colStt.ReadOnly = true;
            this.colStt.Width = 45;
            // 
            // colMaHangNoiBo
            // 
            this.colMaHangNoiBo.DataPropertyName = "MaHangNoiBo";
            this.colMaHangNoiBo.HeaderText = "Mã hàng";
            this.colMaHangNoiBo.MinimumWidth = 6;
            this.colMaHangNoiBo.Name = "colMaHangNoiBo";
            this.colMaHangNoiBo.ReadOnly = true;
            this.colMaHangNoiBo.Width = 110;
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
            this.colDonViTinh.Width = 70;
            // 
            // colTonKho
            // 
            this.colTonKho.DataPropertyName = "TonKho";
            this.colTonKho.HeaderText = "Tồn kho";
            this.colTonKho.MinimumWidth = 6;
            this.colTonKho.Name = "colTonKho";
            this.colTonKho.ReadOnly = true;
            this.colTonKho.Width = 95;
            // 
            // colSoLuongThucTe
            // 
            this.colSoLuongThucTe.DataPropertyName = "SoLuongThucTe";
            this.colSoLuongThucTe.HeaderText = "SL thực tế";
            this.colSoLuongThucTe.MinimumWidth = 6;
            this.colSoLuongThucTe.Name = "colSoLuongThucTe";
            this.colSoLuongThucTe.Width = 105;
            // 
            // colSlChenhLech
            // 
            this.colSlChenhLech.DataPropertyName = "SlChenhLech";
            this.colSlChenhLech.HeaderText = "SL lệch";
            this.colSlChenhLech.MinimumWidth = 6;
            this.colSlChenhLech.Name = "colSlChenhLech";
            this.colSlChenhLech.ReadOnly = true;
            this.colSlChenhLech.Width = 95;
            // 
            // colDonGia
            // 
            this.colDonGia.DataPropertyName = "DonGia";
            this.colDonGia.HeaderText = "Đơn giá";
            this.colDonGia.MinimumWidth = 6;
            this.colDonGia.Name = "colDonGia";
            this.colDonGia.ReadOnly = true;
            this.colDonGia.Width = 110;
            // 
            // colGiaTriChenhLech
            // 
            this.colGiaTriChenhLech.DataPropertyName = "GiaTriChenhLech";
            this.colGiaTriChenhLech.HeaderText = "Giá trị lệch";
            this.colGiaTriChenhLech.MinimumWidth = 6;
            this.colGiaTriChenhLech.Name = "colGiaTriChenhLech";
            this.colGiaTriChenhLech.ReadOnly = true;
            this.colGiaTriChenhLech.Width = 135;
            // 
            // colXoaDong
            // 
            this.colXoaDong.HeaderText = "Xóa";
            this.colXoaDong.MinimumWidth = 6;
            this.colXoaDong.Name = "colXoaDong";
            this.colXoaDong.Text = "X";
            this.colXoaDong.UseColumnTextForButtonValue = true;
            this.colXoaDong.Width = 50;
            // 
            // pnlBottom
            // 
            this.pnlBottom.BackColor = System.Drawing.Color.White;
            this.pnlBottom.Controls.Add(this.lblTongChenhLechVal);
            this.pnlBottom.Controls.Add(this.lblTongChenhLechLbl);
            this.pnlBottom.Controls.Add(this.lblTongLechGiamVal);
            this.pnlBottom.Controls.Add(this.lblTongLechGiamLbl);
            this.pnlBottom.Controls.Add(this.lblTongLechTangVal);
            this.pnlBottom.Controls.Add(this.lblTongLechTangLbl);
            this.pnlBottom.Controls.Add(this.lblTongThucTeVal);
            this.pnlBottom.Controls.Add(this.lblTongThucTeLbl);
            this.pnlBottom.Controls.Add(this.lblTongSoHangVal);
            this.pnlBottom.Controls.Add(this.lblTongSoHangLbl);
            this.pnlBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnlBottom.Location = new System.Drawing.Point(0, 640);
            this.pnlBottom.Name = "pnlBottom";
            this.pnlBottom.Padding = new System.Windows.Forms.Padding(15, 8, 15, 8);
            this.pnlBottom.Size = new System.Drawing.Size(1200, 60);
            this.pnlBottom.TabIndex = 4;
            // 
            // lblTongChenhLechVal
            // 
            this.lblTongChenhLechVal.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblTongChenhLechVal.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold);
            this.lblTongChenhLechVal.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(64)))), ((int)(((byte)(175)))));
            this.lblTongChenhLechVal.Location = new System.Drawing.Point(955, 30);
            this.lblTongChenhLechVal.Name = "lblTongChenhLechVal";
            this.lblTongChenhLechVal.Size = new System.Drawing.Size(225, 21);
            this.lblTongChenhLechVal.TabIndex = 9;
            this.lblTongChenhLechVal.Text = "0 | 0 đ";
            this.lblTongChenhLechVal.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblTongChenhLechLbl
            // 
            this.lblTongChenhLechLbl.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblTongChenhLechLbl.AutoSize = true;
            this.lblTongChenhLechLbl.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold);
            this.lblTongChenhLechLbl.Location = new System.Drawing.Point(825, 30);
            this.lblTongChenhLechLbl.Name = "lblTongChenhLechLbl";
            this.lblTongChenhLechLbl.Size = new System.Drawing.Size(134, 21);
            this.lblTongChenhLechLbl.TabIndex = 8;
            this.lblTongChenhLechLbl.Text = "Tổng chênh lệch:";
            // 
            // lblTongLechGiamVal
            // 
            this.lblTongLechGiamVal.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblTongLechGiamVal.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.lblTongLechGiamVal.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(38)))), ((int)(((byte)(38)))));
            this.lblTongLechGiamVal.Location = new System.Drawing.Point(955, 8);
            this.lblTongLechGiamVal.Name = "lblTongLechGiamVal";
            this.lblTongLechGiamVal.Size = new System.Drawing.Size(225, 20);
            this.lblTongLechGiamVal.TabIndex = 7;
            this.lblTongLechGiamVal.Text = "0 | 0 đ";
            this.lblTongLechGiamVal.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblTongLechGiamLbl
            // 
            this.lblTongLechGiamLbl.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblTongLechGiamLbl.AutoSize = true;
            this.lblTongLechGiamLbl.Location = new System.Drawing.Point(825, 8);
            this.lblTongLechGiamLbl.Name = "lblTongLechGiamLbl";
            this.lblTongLechGiamLbl.Size = new System.Drawing.Size(117, 21);
            this.lblTongLechGiamLbl.TabIndex = 6;
            this.lblTongLechGiamLbl.Text = "Tổng lệch giảm:";
            // 
            // lblTongLechTangVal
            // 
            this.lblTongLechTangVal.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.lblTongLechTangVal.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(16)))), ((int)(((byte)(185)))), ((int)(((byte)(129)))));
            this.lblTongLechTangVal.Location = new System.Drawing.Point(525, 30);
            this.lblTongLechTangVal.Name = "lblTongLechTangVal";
            this.lblTongLechTangVal.Size = new System.Drawing.Size(250, 21);
            this.lblTongLechTangVal.TabIndex = 5;
            this.lblTongLechTangVal.Text = "0 | 0 đ";
            // 
            // lblTongLechTangLbl
            // 
            this.lblTongLechTangLbl.AutoSize = true;
            this.lblTongLechTangLbl.Location = new System.Drawing.Point(405, 30);
            this.lblTongLechTangLbl.Name = "lblTongLechTangLbl";
            this.lblTongLechTangLbl.Size = new System.Drawing.Size(113, 21);
            this.lblTongLechTangLbl.TabIndex = 4;
            this.lblTongLechTangLbl.Text = "Tổng lệch tăng:";
            // 
            // lblTongThucTeVal
            // 
            this.lblTongThucTeVal.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.lblTongThucTeVal.Location = new System.Drawing.Point(525, 8);
            this.lblTongThucTeVal.Name = "lblTongThucTeVal";
            this.lblTongThucTeVal.Size = new System.Drawing.Size(250, 20);
            this.lblTongThucTeVal.TabIndex = 3;
            this.lblTongThucTeVal.Text = "0 | 0 đ";
            // 
            // lblTongThucTeLbl
            // 
            this.lblTongThucTeLbl.AutoSize = true;
            this.lblTongThucTeLbl.Location = new System.Drawing.Point(405, 8);
            this.lblTongThucTeLbl.Name = "lblTongThucTeLbl";
            this.lblTongThucTeLbl.Size = new System.Drawing.Size(103, 21);
            this.lblTongThucTeLbl.TabIndex = 2;
            this.lblTongThucTeLbl.Text = "Tổng thực tế:";
            // 
            // lblTongSoHangVal
            // 
            this.lblTongSoHangVal.AutoSize = true;
            this.lblTongSoHangVal.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold);
            this.lblTongSoHangVal.Location = new System.Drawing.Point(135, 18);
            this.lblTongSoHangVal.Name = "lblTongSoHangVal";
            this.lblTongSoHangVal.Size = new System.Drawing.Size(19, 21);
            this.lblTongSoHangVal.TabIndex = 1;
            this.lblTongSoHangVal.Text = "0";
            // 
            // lblTongSoHangLbl
            // 
            this.lblTongSoHangLbl.AutoSize = true;
            this.lblTongSoHangLbl.Location = new System.Drawing.Point(18, 18);
            this.lblTongSoHangLbl.Name = "lblTongSoHangLbl";
            this.lblTongSoHangLbl.Size = new System.Drawing.Size(111, 21);
            this.lblTongSoHangLbl.TabIndex = 0;
            this.lblTongSoHangLbl.Text = "Số dòng kiểm:";
            // 
            // frmThemPhieuKiemKho
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 21F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(1200, 700);
            this.Controls.Add(this.pnlCenter);
            this.Controls.Add(this.pnlBottom);
            this.Controls.Add(this.pnlThemHang);
            this.Controls.Add(this.grpMaster);
            this.Controls.Add(this.pnlTop);
            this.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            this.KeyPreview = true;
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.MinimumSize = new System.Drawing.Size(1050, 650);
            this.Name = "frmThemPhieuKiemKho";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Thêm mới phiếu kiểm kho";
            this.Load += new System.EventHandler(this.frmThemPhieuKiemKho_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.frmThemPhieuKiemKho_KeyDown);
            this.pnlTop.ResumeLayout(false);
            this.pnlTop.PerformLayout();
            this.grpMaster.ResumeLayout(false);
            this.grpMaster.PerformLayout();
            this.pnlThemHang.ResumeLayout(false);
            this.pnlThemHang.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudSoLuongThucTe)).EndInit();
            this.pnlCenter.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvHangKiem)).EndInit();
            this.pnlBottom.ResumeLayout(false);
            this.pnlBottom.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pnlTop;
        private System.Windows.Forms.Label lblTieuDe;
        private System.Windows.Forms.Button btnNhapFile;
        private System.Windows.Forms.Button btnLuuTam;
        private System.Windows.Forms.Button btnCanBangKho;
        private System.Windows.Forms.Button btnDong;
        private System.Windows.Forms.GroupBox grpMaster;
        private System.Windows.Forms.Label lblMaPhieu;
        private System.Windows.Forms.Label lblMaPhieuVal;
        private System.Windows.Forms.Label lblKho;
        private System.Windows.Forms.ComboBox cboKho;
        private System.Windows.Forms.Label lblNguoiKiem;
        private System.Windows.Forms.ComboBox cboNguoiKiem;
        private System.Windows.Forms.Label lblNgayKiem;
        private System.Windows.Forms.DateTimePicker dtpThoiDiemChot;
        private System.Windows.Forms.Label lblGhiChu;
        private System.Windows.Forms.TextBox txtGhiChu;
        private System.Windows.Forms.Panel pnlThemHang;
        private System.Windows.Forms.Label lblChonHang;
        private System.Windows.Forms.ComboBox cboHangHoa;
        private System.Windows.Forms.Label lblSlThucTe;
        private System.Windows.Forms.NumericUpDown nudSoLuongThucTe;
        private System.Windows.Forms.Button btnThemHang;
        private System.Windows.Forms.Button btnNapToanBoHang;
        private System.Windows.Forms.Panel pnlCenter;
        private System.Windows.Forms.DataGridView dgvHangKiem;
        private System.Windows.Forms.Panel pnlBottom;
        private System.Windows.Forms.Label lblTongSoHangLbl;
        private System.Windows.Forms.Label lblTongSoHangVal;
        private System.Windows.Forms.Label lblTongThucTeLbl;
        private System.Windows.Forms.Label lblTongThucTeVal;
        private System.Windows.Forms.Label lblTongLechTangLbl;
        private System.Windows.Forms.Label lblTongLechTangVal;
        private System.Windows.Forms.Label lblTongLechGiamLbl;
        private System.Windows.Forms.Label lblTongLechGiamVal;
        private System.Windows.Forms.Label lblTongChenhLechLbl;
        private System.Windows.Forms.Label lblTongChenhLechVal;
        private System.Windows.Forms.DataGridViewTextBoxColumn colStt;
        private System.Windows.Forms.DataGridViewTextBoxColumn colMaHangNoiBo;
        private System.Windows.Forms.DataGridViewTextBoxColumn colTenHang;
        private System.Windows.Forms.DataGridViewTextBoxColumn colDonViTinh;
        private System.Windows.Forms.DataGridViewTextBoxColumn colTonKho;
        private System.Windows.Forms.DataGridViewTextBoxColumn colSoLuongThucTe;
        private System.Windows.Forms.DataGridViewTextBoxColumn colSlChenhLech;
        private System.Windows.Forms.DataGridViewTextBoxColumn colDonGia;
        private System.Windows.Forms.DataGridViewTextBoxColumn colGiaTriChenhLech;
        private System.Windows.Forms.DataGridViewButtonColumn colXoaDong;
    }
}
