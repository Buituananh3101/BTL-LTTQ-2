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
            this.pnlRight = new System.Windows.Forms.Panel();
            this.pnlRightContent = new System.Windows.Forms.Panel();
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
            this.pnlDiv3 = new System.Windows.Forms.Panel();
            this.txtGhiChu = new System.Windows.Forms.TextBox();
            this.lblGhiChu = new System.Windows.Forms.Label();
            this.pnlDiv2 = new System.Windows.Forms.Panel();
            this.cboKho = new System.Windows.Forms.ComboBox();
            this.lblKho = new System.Windows.Forms.Label();
            this.lblMaPhieuVal = new System.Windows.Forms.Label();
            this.lblMaPhieu = new System.Windows.Forms.Label();
            this.pnlDiv1 = new System.Windows.Forms.Panel();
            this.dtpThoiDiemChot = new System.Windows.Forms.DateTimePicker();
            this.lblNgayKiem = new System.Windows.Forms.Label();
            this.cboNguoiKiem = new System.Windows.Forms.ComboBox();
            this.lblNguoiKiem = new System.Windows.Forms.Label();
            this.pnlRightActions = new System.Windows.Forms.Panel();
            this.pnlRightActionsDivider = new System.Windows.Forms.Panel();
            this.btnBoQua = new System.Windows.Forms.Button();
            this.btnLuuTam = new System.Windows.Forms.Button();
            this.btnCanBangKho = new System.Windows.Forms.Button();
            this.pnlRightBorder = new System.Windows.Forms.Panel();
            this.pnlMain = new System.Windows.Forms.Panel();
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
            this.pnlTopLeft = new System.Windows.Forms.Panel();
            this.btnThemHang = new System.Windows.Forms.Button();
            this.nudSoLuongThucTe = new System.Windows.Forms.NumericUpDown();
            this.lblSlThucTe = new System.Windows.Forms.Label();
            this.cboHangHoa = new System.Windows.Forms.ComboBox();
            this.lblChonHang = new System.Windows.Forms.Label();
            this.btnNhapFile = new System.Windows.Forms.Button();
            this.btnNapToanBoHang = new System.Windows.Forms.Button();
            this.lblTieuDe = new System.Windows.Forms.Label();
            this.pnlRight.SuspendLayout();
            this.pnlRightContent.SuspendLayout();
            this.pnlRightActions.SuspendLayout();
            this.pnlMain.SuspendLayout();
            this.pnlCenter.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvHangKiem)).BeginInit();
            this.pnlTopLeft.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudSoLuongThucTe)).BeginInit();
            this.SuspendLayout();
            // 
            // pnlRight
            // 
            this.pnlRight.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(250)))), ((int)(((byte)(252)))));
            this.pnlRight.Controls.Add(this.pnlRightContent);
            this.pnlRight.Controls.Add(this.pnlRightActions);
            this.pnlRight.Controls.Add(this.pnlRightBorder);
            this.pnlRight.Dock = System.Windows.Forms.DockStyle.Right;
            this.pnlRight.Location = new System.Drawing.Point(842, 0);
            this.pnlRight.Name = "pnlRight";
            this.pnlRight.Size = new System.Drawing.Size(340, 764);
            this.pnlRight.TabIndex = 1;
            // 
            // pnlRightContent
            // 
            this.pnlRightContent.AutoScroll = true;
            this.pnlRightContent.Controls.Add(this.lblTongChenhLechVal);
            this.pnlRightContent.Controls.Add(this.lblTongChenhLechLbl);
            this.pnlRightContent.Controls.Add(this.lblTongLechGiamVal);
            this.pnlRightContent.Controls.Add(this.lblTongLechGiamLbl);
            this.pnlRightContent.Controls.Add(this.lblTongLechTangVal);
            this.pnlRightContent.Controls.Add(this.lblTongLechTangLbl);
            this.pnlRightContent.Controls.Add(this.lblTongThucTeVal);
            this.pnlRightContent.Controls.Add(this.lblTongThucTeLbl);
            this.pnlRightContent.Controls.Add(this.lblTongSoHangVal);
            this.pnlRightContent.Controls.Add(this.lblTongSoHangLbl);
            this.pnlRightContent.Controls.Add(this.pnlDiv3);
            this.pnlRightContent.Controls.Add(this.txtGhiChu);
            this.pnlRightContent.Controls.Add(this.lblGhiChu);
            this.pnlRightContent.Controls.Add(this.pnlDiv2);
            this.pnlRightContent.Controls.Add(this.cboKho);
            this.pnlRightContent.Controls.Add(this.lblKho);
            this.pnlRightContent.Controls.Add(this.lblMaPhieuVal);
            this.pnlRightContent.Controls.Add(this.lblMaPhieu);
            this.pnlRightContent.Controls.Add(this.pnlDiv1);
            this.pnlRightContent.Controls.Add(this.dtpThoiDiemChot);
            this.pnlRightContent.Controls.Add(this.lblNgayKiem);
            this.pnlRightContent.Controls.Add(this.cboNguoiKiem);
            this.pnlRightContent.Controls.Add(this.lblNguoiKiem);
            this.pnlRightContent.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlRightContent.Location = new System.Drawing.Point(1, 0);
            this.pnlRightContent.Name = "pnlRightContent";
            this.pnlRightContent.Padding = new System.Windows.Forms.Padding(15, 12, 15, 12);
            this.pnlRightContent.Size = new System.Drawing.Size(339, 704);
            this.pnlRightContent.TabIndex = 0;
            // 
            // lblTongChenhLechVal
            // 
            this.lblTongChenhLechVal.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblTongChenhLechVal.Location = new System.Drawing.Point(135, 432);
            this.lblTongChenhLechVal.Name = "lblTongChenhLechVal";
            this.lblTongChenhLechVal.Size = new System.Drawing.Size(185, 20);
            this.lblTongChenhLechVal.TabIndex = 24;
            this.lblTongChenhLechVal.Text = "0 | 0 đ";
            this.lblTongChenhLechVal.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblTongChenhLechLbl
            // 
            this.lblTongChenhLechLbl.AutoSize = true;
            this.lblTongChenhLechLbl.Location = new System.Drawing.Point(15, 402);
            this.lblTongChenhLechLbl.Name = "lblTongChenhLechLbl";
            this.lblTongChenhLechLbl.Size = new System.Drawing.Size(107, 17);
            this.lblTongChenhLechLbl.TabIndex = 23;
            this.lblTongChenhLechLbl.Text = "Tổng chênh lệch:";
            // 
            // lblTongLechGiamVal
            // 
            this.lblTongLechGiamVal.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblTongLechGiamVal.Location = new System.Drawing.Point(135, 372);
            this.lblTongLechGiamVal.Name = "lblTongLechGiamVal";
            this.lblTongLechGiamVal.Size = new System.Drawing.Size(185, 20);
            this.lblTongLechGiamVal.TabIndex = 22;
            this.lblTongLechGiamVal.Text = "0 | 0 đ";
            this.lblTongLechGiamVal.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblTongLechGiamLbl
            // 
            this.lblTongLechGiamLbl.AutoSize = true;
            this.lblTongLechGiamLbl.Location = new System.Drawing.Point(15, 372);
            this.lblTongLechGiamLbl.Name = "lblTongLechGiamLbl";
            this.lblTongLechGiamLbl.Size = new System.Drawing.Size(101, 17);
            this.lblTongLechGiamLbl.TabIndex = 21;
            this.lblTongLechGiamLbl.Text = "Tổng lệch giảm:";
            // 
            // lblTongLechTangVal
            // 
            this.lblTongLechTangVal.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblTongLechTangVal.Location = new System.Drawing.Point(135, 342);
            this.lblTongLechTangVal.Name = "lblTongLechTangVal";
            this.lblTongLechTangVal.Size = new System.Drawing.Size(185, 20);
            this.lblTongLechTangVal.TabIndex = 20;
            this.lblTongLechTangVal.Text = "0 | 0 đ";
            this.lblTongLechTangVal.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblTongLechTangLbl
            // 
            this.lblTongLechTangLbl.AutoSize = true;
            this.lblTongLechTangLbl.Location = new System.Drawing.Point(15, 342);
            this.lblTongLechTangLbl.Name = "lblTongLechTangLbl";
            this.lblTongLechTangLbl.Size = new System.Drawing.Size(98, 17);
            this.lblTongLechTangLbl.TabIndex = 19;
            this.lblTongLechTangLbl.Text = "Tổng lệch tăng:";
            // 
            // lblTongThucTeVal
            // 
            this.lblTongThucTeVal.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblTongThucTeVal.Location = new System.Drawing.Point(135, 312);
            this.lblTongThucTeVal.Name = "lblTongThucTeVal";
            this.lblTongThucTeVal.Size = new System.Drawing.Size(185, 20);
            this.lblTongThucTeVal.TabIndex = 18;
            this.lblTongThucTeVal.Text = "0 | 0 đ";
            this.lblTongThucTeVal.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblTongThucTeLbl
            // 
            this.lblTongThucTeLbl.AutoSize = true;
            this.lblTongThucTeLbl.Location = new System.Drawing.Point(15, 312);
            this.lblTongThucTeLbl.Name = "lblTongThucTeLbl";
            this.lblTongThucTeLbl.Size = new System.Drawing.Size(86, 17);
            this.lblTongThucTeLbl.TabIndex = 17;
            this.lblTongThucTeLbl.Text = "Tổng thực tế:";
            // 
            // lblTongSoHangVal
            // 
            this.lblTongSoHangVal.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblTongSoHangVal.Location = new System.Drawing.Point(150, 282);
            this.lblTongSoHangVal.Name = "lblTongSoHangVal";
            this.lblTongSoHangVal.Size = new System.Drawing.Size(170, 20);
            this.lblTongSoHangVal.TabIndex = 16;
            this.lblTongSoHangVal.Text = "0";
            this.lblTongSoHangVal.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblTongSoHangLbl
            // 
            this.lblTongSoHangLbl.AutoSize = true;
            this.lblTongSoHangLbl.Location = new System.Drawing.Point(15, 282);
            this.lblTongSoHangLbl.Name = "lblTongSoHangLbl";
            this.lblTongSoHangLbl.Size = new System.Drawing.Size(91, 17);
            this.lblTongSoHangLbl.TabIndex = 15;
            this.lblTongSoHangLbl.Text = "Số dòng kiểm:";
            // 
            // pnlDiv3
            // 
            this.pnlDiv3.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pnlDiv3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(232)))), ((int)(((byte)(240)))));
            this.pnlDiv3.Location = new System.Drawing.Point(15, 268);
            this.pnlDiv3.Name = "pnlDiv3";
            this.pnlDiv3.Size = new System.Drawing.Size(305, 1);
            this.pnlDiv3.TabIndex = 14;
            // 
            // txtGhiChu
            // 
            this.txtGhiChu.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtGhiChu.Location = new System.Drawing.Point(15, 180);
            this.txtGhiChu.Multiline = true;
            this.txtGhiChu.Name = "txtGhiChu";
            this.txtGhiChu.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtGhiChu.Size = new System.Drawing.Size(305, 75);
            this.txtGhiChu.TabIndex = 13;
            // 
            // lblGhiChu
            // 
            this.lblGhiChu.AutoSize = true;
            this.lblGhiChu.Location = new System.Drawing.Point(15, 158);
            this.lblGhiChu.Name = "lblGhiChu";
            this.lblGhiChu.Size = new System.Drawing.Size(54, 17);
            this.lblGhiChu.TabIndex = 12;
            this.lblGhiChu.Text = "Ghi chú:";
            // 
            // pnlDiv2
            // 
            this.pnlDiv2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pnlDiv2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(232)))), ((int)(((byte)(240)))));
            this.pnlDiv2.Location = new System.Drawing.Point(15, 148);
            this.pnlDiv2.Name = "pnlDiv2";
            this.pnlDiv2.Size = new System.Drawing.Size(305, 1);
            this.pnlDiv2.TabIndex = 11;
            // 
            // cboKho
            // 
            this.cboKho.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cboKho.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboKho.FormattingEnabled = true;
            this.cboKho.Location = new System.Drawing.Point(115, 114);
            this.cboKho.Name = "cboKho";
            this.cboKho.Size = new System.Drawing.Size(205, 25);
            this.cboKho.TabIndex = 10;
            this.cboKho.SelectedIndexChanged += new System.EventHandler(this.cboKho_SelectedIndexChanged);
            // 
            // lblKho
            // 
            this.lblKho.AutoSize = true;
            this.lblKho.Location = new System.Drawing.Point(15, 118);
            this.lblKho.Name = "lblKho";
            this.lblKho.Size = new System.Drawing.Size(65, 17);
            this.lblKho.TabIndex = 9;
            this.lblKho.Text = "Kho kiểm:";
            // 
            // lblMaPhieuVal
            // 
            this.lblMaPhieuVal.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblMaPhieuVal.Location = new System.Drawing.Point(115, 88);
            this.lblMaPhieuVal.Name = "lblMaPhieuVal";
            this.lblMaPhieuVal.Size = new System.Drawing.Size(205, 20);
            this.lblMaPhieuVal.TabIndex = 6;
            this.lblMaPhieuVal.Text = "Mã tự động";
            this.lblMaPhieuVal.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblMaPhieu
            // 
            this.lblMaPhieu.AutoSize = true;
            this.lblMaPhieu.Location = new System.Drawing.Point(15, 88);
            this.lblMaPhieu.Name = "lblMaPhieu";
            this.lblMaPhieu.Size = new System.Drawing.Size(65, 17);
            this.lblMaPhieu.TabIndex = 5;
            this.lblMaPhieu.Text = "Mã phiếu:";
            // 
            // pnlDiv1
            // 
            this.pnlDiv1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pnlDiv1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(232)))), ((int)(((byte)(240)))));
            this.pnlDiv1.Location = new System.Drawing.Point(15, 76);
            this.pnlDiv1.Name = "pnlDiv1";
            this.pnlDiv1.Size = new System.Drawing.Size(305, 1);
            this.pnlDiv1.TabIndex = 4;
            // 
            // dtpThoiDiemChot
            // 
            this.dtpThoiDiemChot.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dtpThoiDiemChot.CustomFormat = "dd/MM/yyyy HH:mm";
            this.dtpThoiDiemChot.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpThoiDiemChot.Location = new System.Drawing.Point(115, 42);
            this.dtpThoiDiemChot.Name = "dtpThoiDiemChot";
            this.dtpThoiDiemChot.Size = new System.Drawing.Size(205, 25);
            this.dtpThoiDiemChot.TabIndex = 3;
            // 
            // lblNgayKiem
            // 
            this.lblNgayKiem.AutoSize = true;
            this.lblNgayKiem.Location = new System.Drawing.Point(15, 46);
            this.lblNgayKiem.Name = "lblNgayKiem";
            this.lblNgayKiem.Size = new System.Drawing.Size(65, 17);
            this.lblNgayKiem.TabIndex = 2;
            this.lblNgayKiem.Text = "Thời gian:";
            // 
            // cboNguoiKiem
            // 
            this.cboNguoiKiem.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cboNguoiKiem.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboNguoiKiem.FormattingEnabled = true;
            this.cboNguoiKiem.Location = new System.Drawing.Point(115, 10);
            this.cboNguoiKiem.Name = "cboNguoiKiem";
            this.cboNguoiKiem.Size = new System.Drawing.Size(205, 25);
            this.cboNguoiKiem.TabIndex = 1;
            // 
            // lblNguoiKiem
            // 
            this.lblNguoiKiem.AutoSize = true;
            this.lblNguoiKiem.Location = new System.Drawing.Point(15, 14);
            this.lblNguoiKiem.Name = "lblNguoiKiem";
            this.lblNguoiKiem.Size = new System.Drawing.Size(80, 17);
            this.lblNguoiKiem.TabIndex = 0;
            this.lblNguoiKiem.Text = "Người kiểm:";
            // 
            // pnlRightActions
            // 
            this.pnlRightActions.Controls.Add(this.pnlRightActionsDivider);
            this.pnlRightActions.Controls.Add(this.btnBoQua);
            this.pnlRightActions.Controls.Add(this.btnLuuTam);
            this.pnlRightActions.Controls.Add(this.btnCanBangKho);
            this.pnlRightActions.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnlRightActions.Location = new System.Drawing.Point(1, 704);
            this.pnlRightActions.Name = "pnlRightActions";
            this.pnlRightActions.Size = new System.Drawing.Size(339, 60);
            this.pnlRightActions.TabIndex = 1;
            // 
            // pnlRightActionsDivider
            // 
            this.pnlRightActionsDivider.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(232)))), ((int)(((byte)(240)))));
            this.pnlRightActionsDivider.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlRightActionsDivider.Location = new System.Drawing.Point(0, 0);
            this.pnlRightActionsDivider.Name = "pnlRightActionsDivider";
            this.pnlRightActionsDivider.Size = new System.Drawing.Size(339, 1);
            this.pnlRightActionsDivider.TabIndex = 3;
            // 
            // btnBoQua
            // 
            this.btnBoQua.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnBoQua.Location = new System.Drawing.Point(15, 13);
            this.btnBoQua.Name = "btnBoQua";
            this.btnBoQua.Size = new System.Drawing.Size(80, 34);
            this.btnBoQua.TabIndex = 0;
            this.btnBoQua.Text = "Bỏ qua";
            this.btnBoQua.UseVisualStyleBackColor = true;
            this.btnBoQua.Click += new System.EventHandler(this.btnBoQua_Click);
            // 
            // btnLuuTam
            // 
            this.btnLuuTam.Location = new System.Drawing.Point(105, 13);
            this.btnLuuTam.Name = "btnLuuTam";
            this.btnLuuTam.Size = new System.Drawing.Size(105, 34);
            this.btnLuuTam.TabIndex = 1;
            this.btnLuuTam.Text = "Lưu tạm";
            this.btnLuuTam.UseVisualStyleBackColor = true;
            this.btnLuuTam.Click += new System.EventHandler(this.btnLuuTam_Click);
            // 
            // btnCanBangKho
            // 
            this.btnCanBangKho.Location = new System.Drawing.Point(218, 13);
            this.btnCanBangKho.Name = "btnCanBangKho";
            this.btnCanBangKho.Size = new System.Drawing.Size(106, 34);
            this.btnCanBangKho.TabIndex = 2;
            this.btnCanBangKho.Text = "Lưu";
            this.btnCanBangKho.UseVisualStyleBackColor = true;
            this.btnCanBangKho.Click += new System.EventHandler(this.btnCanBangKho_Click);
            // 
            // pnlRightBorder
            // 
            this.pnlRightBorder.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(232)))), ((int)(((byte)(240)))));
            this.pnlRightBorder.Dock = System.Windows.Forms.DockStyle.Left;
            this.pnlRightBorder.Location = new System.Drawing.Point(0, 0);
            this.pnlRightBorder.Name = "pnlRightBorder";
            this.pnlRightBorder.Size = new System.Drawing.Size(1, 764);
            this.pnlRightBorder.TabIndex = 0;
            // 
            // pnlMain
            // 
            this.pnlMain.Controls.Add(this.pnlCenter);
            this.pnlMain.Controls.Add(this.pnlTopLeft);
            this.pnlMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlMain.Location = new System.Drawing.Point(0, 0);
            this.pnlMain.Name = "pnlMain";
            this.pnlMain.Size = new System.Drawing.Size(842, 764);
            this.pnlMain.TabIndex = 0;
            // 
            // pnlCenter
            // 
            this.pnlCenter.Controls.Add(this.dgvHangKiem);
            this.pnlCenter.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlCenter.Location = new System.Drawing.Point(0, 92);
            this.pnlCenter.Name = "pnlCenter";
            this.pnlCenter.Padding = new System.Windows.Forms.Padding(15, 8, 15, 12);
            this.pnlCenter.Size = new System.Drawing.Size(842, 672);
            this.pnlCenter.TabIndex = 1;
            // 
            // dgvHangKiem
            // 
            this.dgvHangKiem.AllowUserToAddRows = false;
            this.dgvHangKiem.AllowUserToDeleteRows = false;
            this.dgvHangKiem.BackgroundColor = System.Drawing.Color.White;
            this.dgvHangKiem.BorderStyle = System.Windows.Forms.BorderStyle.None;
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
            this.dgvHangKiem.Location = new System.Drawing.Point(15, 8);
            this.dgvHangKiem.Name = "dgvHangKiem";
            this.dgvHangKiem.RowHeadersVisible = false;
            this.dgvHangKiem.RowHeadersWidth = 51;
            this.dgvHangKiem.RowTemplate.Height = 28;
            this.dgvHangKiem.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.CellSelect;
            this.dgvHangKiem.Size = new System.Drawing.Size(812, 652);
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
            this.colMaHangNoiBo.Width = 85;
            // 
            // colTenHang
            // 
            this.colTenHang.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.colTenHang.DataPropertyName = "TenHang";
            this.colTenHang.HeaderText = "Tên hàng";
            this.colTenHang.MinimumWidth = 120;
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
            this.colDonViTinh.Width = 55;
            // 
            // colTonKho
            // 
            this.colTonKho.DataPropertyName = "TonKho";
            this.colTonKho.HeaderText = "Tồn kho";
            this.colTonKho.MinimumWidth = 6;
            this.colTonKho.Name = "colTonKho";
            this.colTonKho.ReadOnly = true;
            this.colTonKho.Width = 75;
            // 
            // colSoLuongThucTe
            // 
            this.colSoLuongThucTe.DataPropertyName = "SoLuongThucTe";
            this.colSoLuongThucTe.HeaderText = "SL thực tế (nhập)";
            this.colSoLuongThucTe.MinimumWidth = 6;
            this.colSoLuongThucTe.Name = "colSoLuongThucTe";
            this.colSoLuongThucTe.Width = 110;
            // 
            // colSlChenhLech
            // 
            this.colSlChenhLech.DataPropertyName = "SlChenhLech";
            this.colSlChenhLech.HeaderText = "SL lệch";
            this.colSlChenhLech.MinimumWidth = 6;
            this.colSlChenhLech.Name = "colSlChenhLech";
            this.colSlChenhLech.ReadOnly = true;
            this.colSlChenhLech.Width = 75;
            // 
            // colDonGia
            // 
            this.colDonGia.DataPropertyName = "DonGia";
            this.colDonGia.HeaderText = "Đơn giá";
            this.colDonGia.MinimumWidth = 6;
            this.colDonGia.Name = "colDonGia";
            this.colDonGia.ReadOnly = true;
            this.colDonGia.Width = 85;
            // 
            // colGiaTriChenhLech
            // 
            this.colGiaTriChenhLech.DataPropertyName = "GiaTriChenhLech";
            this.colGiaTriChenhLech.HeaderText = "Giá trị lệch";
            this.colGiaTriChenhLech.MinimumWidth = 6;
            this.colGiaTriChenhLech.Name = "colGiaTriChenhLech";
            this.colGiaTriChenhLech.ReadOnly = true;
            this.colGiaTriChenhLech.Width = 100;
            // 
            // colXoaDong
            // 
            this.colXoaDong.HeaderText = "Xóa";
            this.colXoaDong.MinimumWidth = 6;
            this.colXoaDong.Name = "colXoaDong";
            this.colXoaDong.Text = "X";
            this.colXoaDong.UseColumnTextForButtonValue = true;
            this.colXoaDong.Width = 45;
            // 
            // pnlTopLeft
            // 
            this.pnlTopLeft.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(250)))), ((int)(((byte)(252)))));
            this.pnlTopLeft.Controls.Add(this.btnThemHang);
            this.pnlTopLeft.Controls.Add(this.nudSoLuongThucTe);
            this.pnlTopLeft.Controls.Add(this.lblSlThucTe);
            this.pnlTopLeft.Controls.Add(this.cboHangHoa);
            this.pnlTopLeft.Controls.Add(this.lblChonHang);
            this.pnlTopLeft.Controls.Add(this.btnNhapFile);
            this.pnlTopLeft.Controls.Add(this.btnNapToanBoHang);
            this.pnlTopLeft.Controls.Add(this.lblTieuDe);
            this.pnlTopLeft.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlTopLeft.Location = new System.Drawing.Point(0, 0);
            this.pnlTopLeft.Name = "pnlTopLeft";
            this.pnlTopLeft.Padding = new System.Windows.Forms.Padding(15, 10, 15, 10);
            this.pnlTopLeft.Size = new System.Drawing.Size(842, 92);
            this.pnlTopLeft.TabIndex = 0;
            // 
            // btnThemHang
            // 
            this.btnThemHang.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnThemHang.Location = new System.Drawing.Point(732, 48);
            this.btnThemHang.Name = "btnThemHang";
            this.btnThemHang.Size = new System.Drawing.Size(95, 30);
            this.btnThemHang.TabIndex = 7;
            this.btnThemHang.Text = "+ Thêm hàng";
            this.btnThemHang.UseVisualStyleBackColor = true;
            this.btnThemHang.Click += new System.EventHandler(this.btnThemHang_Click);
            // 
            // nudSoLuongThucTe
            // 
            this.nudSoLuongThucTe.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.nudSoLuongThucTe.Location = new System.Drawing.Point(645, 51);
            this.nudSoLuongThucTe.Maximum = new decimal(new int[] {
            1000000,
            0,
            0,
            0});
            this.nudSoLuongThucTe.Name = "nudSoLuongThucTe";
            this.nudSoLuongThucTe.Size = new System.Drawing.Size(80, 25);
            this.nudSoLuongThucTe.TabIndex = 6;
            this.nudSoLuongThucTe.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.nudSoLuongThucTe.ThousandsSeparator = true;
            // 
            // lblSlThucTe
            // 
            this.lblSlThucTe.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblSlThucTe.AutoSize = true;
            this.lblSlThucTe.Location = new System.Drawing.Point(572, 55);
            this.lblSlThucTe.Name = "lblSlThucTe";
            this.lblSlThucTe.Size = new System.Drawing.Size(68, 17);
            this.lblSlThucTe.TabIndex = 5;
            this.lblSlThucTe.Text = "SL thực tế:";
            // 
            // cboHangHoa
            // 
            this.cboHangHoa.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cboHangHoa.FormattingEnabled = true;
            this.cboHangHoa.Location = new System.Drawing.Point(92, 51);
            this.cboHangHoa.Name = "cboHangHoa";
            this.cboHangHoa.Size = new System.Drawing.Size(465, 25);
            this.cboHangHoa.TabIndex = 4;
            // 
            // lblChonHang
            // 
            this.lblChonHang.AutoSize = true;
            this.lblChonHang.Location = new System.Drawing.Point(15, 55);
            this.lblChonHang.Name = "lblChonHang";
            this.lblChonHang.Size = new System.Drawing.Size(73, 17);
            this.lblChonHang.TabIndex = 3;
            this.lblChonHang.Text = "Chọn hàng:";
            // 
            // btnNhapFile
            // 
            this.btnNhapFile.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnNhapFile.Location = new System.Drawing.Point(732, 10);
            this.btnNhapFile.Name = "btnNhapFile";
            this.btnNhapFile.Size = new System.Drawing.Size(95, 30);
            this.btnNhapFile.TabIndex = 2;
            this.btnNhapFile.Text = "Nhập file";
            this.btnNhapFile.UseVisualStyleBackColor = true;
            this.btnNhapFile.Click += new System.EventHandler(this.btnNhapFile_Click);
            // 
            // btnNapToanBoHang
            // 
            this.btnNapToanBoHang.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnNapToanBoHang.Location = new System.Drawing.Point(546, 10);
            this.btnNapToanBoHang.Name = "btnNapToanBoHang";
            this.btnNapToanBoHang.Size = new System.Drawing.Size(180, 30);
            this.btnNapToanBoHang.TabIndex = 1;
            this.btnNapToanBoHang.Text = "Nạp tất cả hàng trong kho";
            this.btnNapToanBoHang.UseVisualStyleBackColor = true;
            this.btnNapToanBoHang.Click += new System.EventHandler(this.btnNapToanBoHang_Click);
            // 
            // lblTieuDe
            // 
            this.lblTieuDe.AutoSize = true;
            this.lblTieuDe.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.lblTieuDe.Location = new System.Drawing.Point(14, 13);
            this.lblTieuDe.Name = "lblTieuDe";
            this.lblTieuDe.Size = new System.Drawing.Size(180, 21);
            this.lblTieuDe.TabIndex = 0;
            this.lblTieuDe.Text = "Thêm phiếu kiểm kho";
            // 
            // frmThemPhieuKiemKho
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnBoQua;
            this.ClientSize = new System.Drawing.Size(1182, 803);
            this.Controls.Add(this.pnlMain);
            this.Controls.Add(this.pnlRight);
            this.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            this.KeyPreview = true;
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.MinimumSize = new System.Drawing.Size(1050, 680);
            this.Name = "frmThemPhieuKiemKho";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Thêm mới phiếu kiểm kho";
            this.Load += new System.EventHandler(this.frmThemPhieuKiemKho_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.frmThemPhieuKiemKho_KeyDown);
            this.pnlRight.ResumeLayout(false);
            this.pnlRightContent.ResumeLayout(false);
            this.pnlRightContent.PerformLayout();
            this.pnlRightActions.ResumeLayout(false);
            this.pnlMain.ResumeLayout(false);
            this.pnlCenter.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvHangKiem)).EndInit();
            this.pnlTopLeft.ResumeLayout(false);
            this.pnlTopLeft.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudSoLuongThucTe)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pnlRight;
        private System.Windows.Forms.Panel pnlRightBorder;
        private System.Windows.Forms.Panel pnlRightContent;
        private System.Windows.Forms.Panel pnlRightActions;
        private System.Windows.Forms.Panel pnlRightActionsDivider;
        private System.Windows.Forms.Button btnBoQua;
        private System.Windows.Forms.Button btnLuuTam;
        private System.Windows.Forms.Button btnCanBangKho;
        private System.Windows.Forms.Label lblNguoiKiem;
        private System.Windows.Forms.ComboBox cboNguoiKiem;
        private System.Windows.Forms.Label lblNgayKiem;
        private System.Windows.Forms.DateTimePicker dtpThoiDiemChot;
        private System.Windows.Forms.Panel pnlDiv1;
        private System.Windows.Forms.Label lblMaPhieu;
        private System.Windows.Forms.Label lblMaPhieuVal;
        private System.Windows.Forms.Label lblKho;
        private System.Windows.Forms.ComboBox cboKho;
        private System.Windows.Forms.Panel pnlDiv2;
        private System.Windows.Forms.Label lblGhiChu;
        private System.Windows.Forms.TextBox txtGhiChu;
        private System.Windows.Forms.Panel pnlDiv3;
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
        private System.Windows.Forms.Panel pnlMain;
        private System.Windows.Forms.Panel pnlTopLeft;
        private System.Windows.Forms.Label lblTieuDe;
        private System.Windows.Forms.Button btnNapToanBoHang;
        private System.Windows.Forms.Button btnNhapFile;
        private System.Windows.Forms.Label lblChonHang;
        private System.Windows.Forms.ComboBox cboHangHoa;
        private System.Windows.Forms.Label lblSlThucTe;
        private System.Windows.Forms.NumericUpDown nudSoLuongThucTe;
        private System.Windows.Forms.Button btnThemHang;
        private System.Windows.Forms.Panel pnlCenter;
        private System.Windows.Forms.DataGridView dgvHangKiem;
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
