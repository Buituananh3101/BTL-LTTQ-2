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
            this.pnlRight = new System.Windows.Forms.Panel();
            this.pnlRightBorder = new System.Windows.Forms.Panel();
            this.pnlRightContent = new System.Windows.Forms.Panel();
            this.lblTongGiaTriVal = new System.Windows.Forms.Label();
            this.lblTongGiaTri = new System.Windows.Forms.Label();
            this.lblTongSoLuongVal = new System.Windows.Forms.Label();
            this.lblTongSoLuong = new System.Windows.Forms.Label();
            this.lblSoMatHangVal = new System.Windows.Forms.Label();
            this.lblSoMatHang = new System.Windows.Forms.Label();
            this.pnlDiv4 = new System.Windows.Forms.Panel();
            this.txtGhiChu = new System.Windows.Forms.TextBox();
            this.lblGhiChu = new System.Windows.Forms.Label();
            this.pnlDiv3 = new System.Windows.Forms.Panel();
            this.cboKhoDich = new System.Windows.Forms.ComboBox();
            this.lblKhoDich = new System.Windows.Forms.Label();
            this.cboKhoNguon = new System.Windows.Forms.ComboBox();
            this.lblKhoNguon = new System.Windows.Forms.Label();
            this.pnlDiv2 = new System.Windows.Forms.Panel();
            this.txtMaPhieu = new System.Windows.Forms.TextBox();
            this.lblMaPhieu = new System.Windows.Forms.Label();
            this.pnlDiv1 = new System.Windows.Forms.Panel();
            this.dtpNgayChuyen = new System.Windows.Forms.DateTimePicker();
            this.lblNgayChuyen = new System.Windows.Forms.Label();
            this.lblNguoiChuyenVal = new System.Windows.Forms.Label();
            this.lblNguoiChuyen = new System.Windows.Forms.Label();
            this.pnlRightActions = new System.Windows.Forms.Panel();
            this.pnlRightActionsDivider = new System.Windows.Forms.Panel();
            this.btnBoQua = new System.Windows.Forms.Button();
            this.btnLuuTam = new System.Windows.Forms.Button();
            this.btnLuu = new System.Windows.Forms.Button();
            this.pnlMain = new System.Windows.Forms.Panel();
            this.pnlCenter = new System.Windows.Forms.Panel();
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
            this.pnlTopLeft = new System.Windows.Forms.Panel();
            this.btnThemHang = new System.Windows.Forms.Button();
            this.nudSoLuong = new System.Windows.Forms.NumericUpDown();
            this.lblSoLuong = new System.Windows.Forms.Label();
            this.lblTonKhoGui = new System.Windows.Forms.Label();
            this.cboHangHoa = new System.Windows.Forms.ComboBox();
            this.lblHangHoa = new System.Windows.Forms.Label();
            this.btnNhapFile = new System.Windows.Forms.Button();
            this.lblTieuDe = new System.Windows.Forms.Label();
            this.pnlRight.SuspendLayout();
            this.pnlRightContent.SuspendLayout();
            this.pnlRightActions.SuspendLayout();
            this.pnlMain.SuspendLayout();
            this.pnlCenter.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvChiTiet)).BeginInit();
            this.pnlTopLeft.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudSoLuong)).BeginInit();
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
            this.pnlRight.Size = new System.Drawing.Size(340, 803);
            this.pnlRight.TabIndex = 1;
            // 
            // pnlRightBorder
            // 
            this.pnlRightBorder.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(232)))), ((int)(((byte)(240)))));
            this.pnlRightBorder.Dock = System.Windows.Forms.DockStyle.Left;
            this.pnlRightBorder.Location = new System.Drawing.Point(0, 0);
            this.pnlRightBorder.Name = "pnlRightBorder";
            this.pnlRightBorder.Size = new System.Drawing.Size(1, 803);
            this.pnlRightBorder.TabIndex = 0;
            // 
            // pnlRightContent
            // 
            this.pnlRightContent.AutoScroll = true;
            this.pnlRightContent.Controls.Add(this.lblTongGiaTriVal);
            this.pnlRightContent.Controls.Add(this.lblTongGiaTri);
            this.pnlRightContent.Controls.Add(this.lblTongSoLuongVal);
            this.pnlRightContent.Controls.Add(this.lblTongSoLuong);
            this.pnlRightContent.Controls.Add(this.lblSoMatHangVal);
            this.pnlRightContent.Controls.Add(this.lblSoMatHang);
            this.pnlRightContent.Controls.Add(this.pnlDiv4);
            this.pnlRightContent.Controls.Add(this.txtGhiChu);
            this.pnlRightContent.Controls.Add(this.lblGhiChu);
            this.pnlRightContent.Controls.Add(this.pnlDiv3);
            this.pnlRightContent.Controls.Add(this.cboKhoDich);
            this.pnlRightContent.Controls.Add(this.lblKhoDich);
            this.pnlRightContent.Controls.Add(this.cboKhoNguon);
            this.pnlRightContent.Controls.Add(this.lblKhoNguon);
            this.pnlRightContent.Controls.Add(this.pnlDiv2);
            this.pnlRightContent.Controls.Add(this.txtMaPhieu);
            this.pnlRightContent.Controls.Add(this.lblMaPhieu);
            this.pnlRightContent.Controls.Add(this.pnlDiv1);
            this.pnlRightContent.Controls.Add(this.dtpNgayChuyen);
            this.pnlRightContent.Controls.Add(this.lblNgayChuyen);
            this.pnlRightContent.Controls.Add(this.lblNguoiChuyenVal);
            this.pnlRightContent.Controls.Add(this.lblNguoiChuyen);
            this.pnlRightContent.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlRightContent.Location = new System.Drawing.Point(1, 0);
            this.pnlRightContent.Name = "pnlRightContent";
            this.pnlRightContent.Padding = new System.Windows.Forms.Padding(15, 12, 15, 12);
            this.pnlRightContent.Size = new System.Drawing.Size(339, 743);
            this.pnlRightContent.TabIndex = 1;
            // 
            // lblTongGiaTriVal
            // 
            this.lblTongGiaTriVal.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblTongGiaTriVal.Location = new System.Drawing.Point(130, 372);
            this.lblTongGiaTriVal.Name = "lblTongGiaTriVal";
            this.lblTongGiaTriVal.Size = new System.Drawing.Size(190, 20);
            this.lblTongGiaTriVal.TabIndex = 21;
            this.lblTongGiaTriVal.Text = "0 VNĐ";
            this.lblTongGiaTriVal.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblTongGiaTri
            // 
            this.lblTongGiaTri.AutoSize = true;
            this.lblTongGiaTri.Location = new System.Drawing.Point(15, 372);
            this.lblTongGiaTri.Name = "lblTongGiaTri";
            this.lblTongGiaTri.Size = new System.Drawing.Size(65, 17);
            this.lblTongGiaTri.TabIndex = 20;
            this.lblTongGiaTri.Text = "Tổng giá trị:";
            // 
            // lblTongSoLuongVal
            // 
            this.lblTongSoLuongVal.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblTongSoLuongVal.Location = new System.Drawing.Point(150, 342);
            this.lblTongSoLuongVal.Name = "lblTongSoLuongVal";
            this.lblTongSoLuongVal.Size = new System.Drawing.Size(170, 20);
            this.lblTongSoLuongVal.TabIndex = 19;
            this.lblTongSoLuongVal.Text = "0";
            this.lblTongSoLuongVal.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblTongSoLuong
            // 
            this.lblTongSoLuong.AutoSize = true;
            this.lblTongSoLuong.Location = new System.Drawing.Point(15, 342);
            this.lblTongSoLuong.Name = "lblTongSoLuong";
            this.lblTongSoLuong.Size = new System.Drawing.Size(107, 17);
            this.lblTongSoLuong.TabIndex = 18;
            this.lblTongSoLuong.Text = "Tổng SL chuyển:";
            // 
            // lblSoMatHangVal
            // 
            this.lblSoMatHangVal.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblSoMatHangVal.Location = new System.Drawing.Point(150, 312);
            this.lblSoMatHangVal.Name = "lblSoMatHangVal";
            this.lblSoMatHangVal.Size = new System.Drawing.Size(170, 20);
            this.lblSoMatHangVal.TabIndex = 17;
            this.lblSoMatHangVal.Text = "0";
            this.lblSoMatHangVal.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblSoMatHang
            // 
            this.lblSoMatHang.AutoSize = true;
            this.lblSoMatHang.Location = new System.Drawing.Point(15, 312);
            this.lblSoMatHang.Name = "lblSoMatHang";
            this.lblSoMatHang.Size = new System.Drawing.Size(86, 17);
            this.lblSoMatHang.TabIndex = 16;
            this.lblSoMatHang.Text = "Số mặt hàng:";
            // 
            // pnlDiv4
            // 
            this.pnlDiv4.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pnlDiv4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(232)))), ((int)(((byte)(240)))));
            this.pnlDiv4.Location = new System.Drawing.Point(15, 298);
            this.pnlDiv4.Name = "pnlDiv4";
            this.pnlDiv4.Size = new System.Drawing.Size(305, 1);
            this.pnlDiv4.TabIndex = 15;
            // 
            // txtGhiChu
            // 
            this.txtGhiChu.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtGhiChu.Location = new System.Drawing.Point(15, 218);
            this.txtGhiChu.Multiline = true;
            this.txtGhiChu.Name = "txtGhiChu";
            this.txtGhiChu.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtGhiChu.Size = new System.Drawing.Size(305, 70);
            this.txtGhiChu.TabIndex = 14;
            // 
            // lblGhiChu
            // 
            this.lblGhiChu.AutoSize = true;
            this.lblGhiChu.Location = new System.Drawing.Point(15, 196);
            this.lblGhiChu.Name = "lblGhiChu";
            this.lblGhiChu.Size = new System.Drawing.Size(54, 17);
            this.lblGhiChu.TabIndex = 13;
            this.lblGhiChu.Text = "Ghi chú:";
            // 
            // pnlDiv3
            // 
            this.pnlDiv3.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pnlDiv3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(232)))), ((int)(((byte)(240)))));
            this.pnlDiv3.Location = new System.Drawing.Point(15, 186);
            this.pnlDiv3.Name = "pnlDiv3";
            this.pnlDiv3.Size = new System.Drawing.Size(305, 1);
            this.pnlDiv3.TabIndex = 12;
            // 
            // cboKhoDich
            // 
            this.cboKhoDich.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cboKhoDich.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboKhoDich.FormattingEnabled = true;
            this.cboKhoDich.Location = new System.Drawing.Point(115, 154);
            this.cboKhoDich.Name = "cboKhoDich";
            this.cboKhoDich.Size = new System.Drawing.Size(205, 25);
            this.cboKhoDich.TabIndex = 11;
            // 
            // lblKhoDich
            // 
            this.lblKhoDich.AutoSize = true;
            this.lblKhoDich.Location = new System.Drawing.Point(15, 158);
            this.lblKhoDich.Name = "lblKhoDich";
            this.lblKhoDich.Size = new System.Drawing.Size(84, 17);
            this.lblKhoDich.TabIndex = 10;
            this.lblKhoDich.Text = "Kho nhận (*):";
            // 
            // cboKhoNguon
            // 
            this.cboKhoNguon.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cboKhoNguon.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboKhoNguon.FormattingEnabled = true;
            this.cboKhoNguon.Location = new System.Drawing.Point(115, 122);
            this.cboKhoNguon.Name = "cboKhoNguon";
            this.cboKhoNguon.Size = new System.Drawing.Size(205, 25);
            this.cboKhoNguon.TabIndex = 9;
            this.cboKhoNguon.SelectedIndexChanged += new System.EventHandler(this.cboKhoNguon_SelectedIndexChanged);
            // 
            // lblKhoNguon
            // 
            this.lblKhoNguon.AutoSize = true;
            this.lblKhoNguon.Location = new System.Drawing.Point(15, 126);
            this.lblKhoNguon.Name = "lblKhoNguon";
            this.lblKhoNguon.Size = new System.Drawing.Size(56, 17);
            this.lblKhoNguon.TabIndex = 8;
            this.lblKhoNguon.Text = "Kho gửi:";
            // 
            // pnlDiv2
            // 
            this.pnlDiv2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pnlDiv2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(232)))), ((int)(((byte)(240)))));
            this.pnlDiv2.Location = new System.Drawing.Point(15, 114);
            this.pnlDiv2.Name = "pnlDiv2";
            this.pnlDiv2.Size = new System.Drawing.Size(305, 1);
            this.pnlDiv2.TabIndex = 7;
            // 
            // txtMaPhieu
            // 
            this.txtMaPhieu.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtMaPhieu.Location = new System.Drawing.Point(115, 82);
            this.txtMaPhieu.Name = "txtMaPhieu";
            this.txtMaPhieu.Size = new System.Drawing.Size(205, 25);
            this.txtMaPhieu.TabIndex = 6;
            // 
            // lblMaPhieu
            // 
            this.lblMaPhieu.AutoSize = true;
            this.lblMaPhieu.Location = new System.Drawing.Point(15, 86);
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
            this.pnlDiv1.Location = new System.Drawing.Point(15, 74);
            this.pnlDiv1.Name = "pnlDiv1";
            this.pnlDiv1.Size = new System.Drawing.Size(305, 1);
            this.pnlDiv1.TabIndex = 4;
            // 
            // dtpNgayChuyen
            // 
            this.dtpNgayChuyen.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dtpNgayChuyen.CustomFormat = "dd/MM/yyyy HH:mm";
            this.dtpNgayChuyen.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpNgayChuyen.Location = new System.Drawing.Point(115, 42);
            this.dtpNgayChuyen.Name = "dtpNgayChuyen";
            this.dtpNgayChuyen.Size = new System.Drawing.Size(205, 25);
            this.dtpNgayChuyen.TabIndex = 3;
            // 
            // lblNgayChuyen
            // 
            this.lblNgayChuyen.AutoSize = true;
            this.lblNgayChuyen.Location = new System.Drawing.Point(15, 46);
            this.lblNgayChuyen.Name = "lblNgayChuyen";
            this.lblNgayChuyen.Size = new System.Drawing.Size(65, 17);
            this.lblNgayChuyen.TabIndex = 2;
            this.lblNgayChuyen.Text = "Ngày lập:";
            // 
            // lblNguoiChuyenVal
            // 
            this.lblNguoiChuyenVal.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblNguoiChuyenVal.Location = new System.Drawing.Point(115, 16);
            this.lblNguoiChuyenVal.Name = "lblNguoiChuyenVal";
            this.lblNguoiChuyenVal.Size = new System.Drawing.Size(205, 20);
            this.lblNguoiChuyenVal.TabIndex = 1;
            this.lblNguoiChuyenVal.Text = "Người lập";
            this.lblNguoiChuyenVal.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblNguoiChuyen
            // 
            this.lblNguoiChuyen.AutoSize = true;
            this.lblNguoiChuyen.Location = new System.Drawing.Point(15, 16);
            this.lblNguoiChuyen.Name = "lblNguoiChuyen";
            this.lblNguoiChuyen.Size = new System.Drawing.Size(70, 17);
            this.lblNguoiChuyen.TabIndex = 0;
            this.lblNguoiChuyen.Text = "Người lập:";
            // 
            // pnlRightActions
            // 
            this.pnlRightActions.Controls.Add(this.pnlRightActionsDivider);
            this.pnlRightActions.Controls.Add(this.btnBoQua);
            this.pnlRightActions.Controls.Add(this.btnLuuTam);
            this.pnlRightActions.Controls.Add(this.btnLuu);
            this.pnlRightActions.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnlRightActions.Location = new System.Drawing.Point(1, 743);
            this.pnlRightActions.Name = "pnlRightActions";
            this.pnlRightActions.Size = new System.Drawing.Size(339, 60);
            this.pnlRightActions.TabIndex = 2;
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
            this.btnBoQua.Size = new System.Drawing.Size(90, 34);
            this.btnBoQua.TabIndex = 0;
            this.btnBoQua.Text = "Bỏ qua";
            this.btnBoQua.UseVisualStyleBackColor = true;
            this.btnBoQua.Click += new System.EventHandler(this.btnBoQua_Click);
            // 
            // btnLuuTam
            // 
            this.btnLuuTam.Location = new System.Drawing.Point(115, 13);
            this.btnLuuTam.Name = "btnLuuTam";
            this.btnLuuTam.Size = new System.Drawing.Size(100, 34);
            this.btnLuuTam.TabIndex = 1;
            this.btnLuuTam.Text = "Lưu tạm";
            this.btnLuuTam.UseVisualStyleBackColor = true;
            this.btnLuuTam.Click += new System.EventHandler(this.btnLuuTam_Click);
            // 
            // btnLuu
            // 
            this.btnLuu.Location = new System.Drawing.Point(225, 13);
            this.btnLuu.Name = "btnLuu";
            this.btnLuu.Size = new System.Drawing.Size(100, 34);
            this.btnLuu.TabIndex = 2;
            this.btnLuu.Text = "Lưu";
            this.btnLuu.UseVisualStyleBackColor = true;
            this.btnLuu.Click += new System.EventHandler(this.btnLuu_Click);
            // 
            // pnlMain
            // 
            this.pnlMain.Controls.Add(this.pnlCenter);
            this.pnlMain.Controls.Add(this.pnlTopLeft);
            this.pnlMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlMain.Location = new System.Drawing.Point(0, 0);
            this.pnlMain.Name = "pnlMain";
            this.pnlMain.Size = new System.Drawing.Size(842, 803);
            this.pnlMain.TabIndex = 0;
            // 
            // pnlCenter
            // 
            this.pnlCenter.BackColor = System.Drawing.Color.White;
            this.pnlCenter.Controls.Add(this.dgvChiTiet);
            this.pnlCenter.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlCenter.Location = new System.Drawing.Point(0, 92);
            this.pnlCenter.Name = "pnlCenter";
            this.pnlCenter.Padding = new System.Windows.Forms.Padding(15, 8, 15, 15);
            this.pnlCenter.Size = new System.Drawing.Size(842, 711);
            this.pnlCenter.TabIndex = 1;
            // 
            // dgvChiTiet
            // 
            this.dgvChiTiet.AllowUserToAddRows = false;
            this.dgvChiTiet.AllowUserToDeleteRows = false;
            this.dgvChiTiet.BackgroundColor = System.Drawing.Color.White;
            this.dgvChiTiet.BorderStyle = System.Windows.Forms.BorderStyle.None;
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
            this.dgvChiTiet.Location = new System.Drawing.Point(15, 8);
            this.dgvChiTiet.Name = "dgvChiTiet";
            this.dgvChiTiet.RowHeadersVisible = false;
            this.dgvChiTiet.RowHeadersWidth = 51;
            this.dgvChiTiet.RowTemplate.Height = 28;
            this.dgvChiTiet.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.CellSelect;
            this.dgvChiTiet.Size = new System.Drawing.Size(812, 688);
            this.dgvChiTiet.TabIndex = 0;
            this.dgvChiTiet.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvChiTiet_CellContentClick);
            this.dgvChiTiet.CellEndEdit += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvChiTiet_CellEndEdit);
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
            // colMaHang
            // 
            this.colMaHang.DataPropertyName = "MaHangNoiBo";
            this.colMaHang.HeaderText = "Mã hàng";
            this.colMaHang.MinimumWidth = 6;
            this.colMaHang.Name = "colMaHang";
            this.colMaHang.ReadOnly = true;
            this.colMaHang.Width = 85;
            // 
            // colTenHang
            // 
            this.colTenHang.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.colTenHang.DataPropertyName = "TenHang";
            this.colTenHang.HeaderText = "Tên hàng hóa";
            this.colTenHang.MinimumWidth = 140;
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
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle1.Format = "N0";
            this.colTonKho.DefaultCellStyle = dataGridViewCellStyle1;
            this.colTonKho.HeaderText = "Tồn kho gửi";
            this.colTonKho.MinimumWidth = 6;
            this.colTonKho.Name = "colTonKho";
            this.colTonKho.ReadOnly = true;
            this.colTonKho.Width = 85;
            // 
            // colSoLuong
            // 
            this.colSoLuong.DataPropertyName = "SoLuong";
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(254)))), ((int)(((byte)(252)))), ((int)(((byte)(232)))));
            dataGridViewCellStyle2.Format = "N0";
            this.colSoLuong.DefaultCellStyle = dataGridViewCellStyle2;
            this.colSoLuong.HeaderText = "SL chuyển (nhập)";
            this.colSoLuong.MinimumWidth = 6;
            this.colSoLuong.Name = "colSoLuong";
            this.colSoLuong.Width = 120;
            // 
            // colDonGia
            // 
            this.colDonGia.DataPropertyName = "DonGiaChuyen";
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle3.Format = "N0";
            this.colDonGia.DefaultCellStyle = dataGridViewCellStyle3;
            this.colDonGia.HeaderText = "Đơn giá";
            this.colDonGia.MinimumWidth = 6;
            this.colDonGia.Name = "colDonGia";
            this.colDonGia.ReadOnly = true;
            this.colDonGia.Width = 90;
            // 
            // colThanhTien
            // 
            this.colThanhTien.DataPropertyName = "ThanhTien";
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle4.Format = "N0";
            this.colThanhTien.DefaultCellStyle = dataGridViewCellStyle4;
            this.colThanhTien.HeaderText = "Thành tiền";
            this.colThanhTien.MinimumWidth = 6;
            this.colThanhTien.Name = "colThanhTien";
            this.colThanhTien.ReadOnly = true;
            this.colThanhTien.Width = 105;
            // 
            // colXoa
            // 
            this.colXoa.HeaderText = "Xóa";
            this.colXoa.MinimumWidth = 6;
            this.colXoa.Name = "colXoa";
            this.colXoa.Text = "X";
            this.colXoa.UseColumnTextForButtonValue = true;
            this.colXoa.Width = 45;
            // 
            // pnlTopLeft
            // 
            this.pnlTopLeft.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(250)))), ((int)(((byte)(252)))));
            this.pnlTopLeft.Controls.Add(this.btnThemHang);
            this.pnlTopLeft.Controls.Add(this.nudSoLuong);
            this.pnlTopLeft.Controls.Add(this.lblSoLuong);
            this.pnlTopLeft.Controls.Add(this.lblTonKhoGui);
            this.pnlTopLeft.Controls.Add(this.cboHangHoa);
            this.pnlTopLeft.Controls.Add(this.lblHangHoa);
            this.pnlTopLeft.Controls.Add(this.btnNhapFile);
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
            // nudSoLuong
            // 
            this.nudSoLuong.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.nudSoLuong.Location = new System.Drawing.Point(667, 51);
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
            this.nudSoLuong.Size = new System.Drawing.Size(58, 24);
            this.nudSoLuong.TabIndex = 6;
            this.nudSoLuong.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.nudSoLuong.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // lblSoLuong
            // 
            this.lblSoLuong.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblSoLuong.AutoSize = true;
            this.lblSoLuong.Location = new System.Drawing.Point(597, 55);
            this.lblSoLuong.Name = "lblSoLuong";
            this.lblSoLuong.Size = new System.Drawing.Size(68, 17);
            this.lblSoLuong.TabIndex = 5;
            this.lblSoLuong.Text = "SL chuyển:";
            // 
            // lblTonKhoGui
            // 
            this.lblTonKhoGui.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblTonKhoGui.AutoSize = true;
            this.lblTonKhoGui.Location = new System.Drawing.Point(467, 55);
            this.lblTonKhoGui.Name = "lblTonKhoGui";
            this.lblTonKhoGui.Size = new System.Drawing.Size(98, 17);
            this.lblTonKhoGui.TabIndex = 4;
            this.lblTonKhoGui.Text = "Tồn kho gửi: ---";
            // 
            // cboHangHoa
            // 
            this.cboHangHoa.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cboHangHoa.FormattingEnabled = true;
            this.cboHangHoa.Location = new System.Drawing.Point(92, 51);
            this.cboHangHoa.Name = "cboHangHoa";
            this.cboHangHoa.Size = new System.Drawing.Size(365, 24);
            this.cboHangHoa.TabIndex = 3;
            this.cboHangHoa.SelectedIndexChanged += new System.EventHandler(this.cboHangHoa_SelectedIndexChanged);
            // 
            // lblHangHoa
            // 
            this.lblHangHoa.AutoSize = true;
            this.lblHangHoa.Location = new System.Drawing.Point(14, 55);
            this.lblHangHoa.Name = "lblHangHoa";
            this.lblHangHoa.Size = new System.Drawing.Size(73, 17);
            this.lblHangHoa.TabIndex = 2;
            this.lblHangHoa.Text = "Chọn hàng:";
            // 
            // btnNhapFile
            // 
            this.btnNhapFile.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnNhapFile.Location = new System.Drawing.Point(727, 10);
            this.btnNhapFile.Name = "btnNhapFile";
            this.btnNhapFile.Size = new System.Drawing.Size(100, 30);
            this.btnNhapFile.TabIndex = 1;
            this.btnNhapFile.Text = "Nhập file";
            this.btnNhapFile.UseVisualStyleBackColor = true;
            this.btnNhapFile.Click += new System.EventHandler(this.btnNhapFile_Click);
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
            // frmThemPhieuChuyenKho
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnBoQua;
            this.ClientSize = new System.Drawing.Size(1400, 803);
            this.Controls.Add(this.pnlMain);
            this.Controls.Add(this.pnlRight);
            this.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(1050, 680);
            this.Name = "frmThemPhieuChuyenKho";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Thêm phiếu chuyển kho";
            this.Load += new System.EventHandler(this.frmThemPhieuChuyenKho_Load);
            this.pnlRight.ResumeLayout(false);
            this.pnlRightContent.ResumeLayout(false);
            this.pnlRightContent.PerformLayout();
            this.pnlRightActions.ResumeLayout(false);
            this.pnlMain.ResumeLayout(false);
            this.pnlCenter.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvChiTiet)).EndInit();
            this.pnlTopLeft.ResumeLayout(false);
            this.pnlTopLeft.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudSoLuong)).EndInit();
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
        private System.Windows.Forms.Button btnLuu;
        private System.Windows.Forms.Label lblNguoiChuyen;
        private System.Windows.Forms.Label lblNguoiChuyenVal;
        private System.Windows.Forms.Label lblNgayChuyen;
        private System.Windows.Forms.DateTimePicker dtpNgayChuyen;
        private System.Windows.Forms.Panel pnlDiv1;
        private System.Windows.Forms.Label lblMaPhieu;
        private System.Windows.Forms.TextBox txtMaPhieu;
        private System.Windows.Forms.Panel pnlDiv2;
        private System.Windows.Forms.Label lblKhoNguon;
        private System.Windows.Forms.ComboBox cboKhoNguon;
        private System.Windows.Forms.Label lblKhoDich;
        private System.Windows.Forms.ComboBox cboKhoDich;
        private System.Windows.Forms.Panel pnlDiv3;
        private System.Windows.Forms.Label lblGhiChu;
        private System.Windows.Forms.TextBox txtGhiChu;
        private System.Windows.Forms.Panel pnlDiv4;
        private System.Windows.Forms.Label lblSoMatHang;
        private System.Windows.Forms.Label lblSoMatHangVal;
        private System.Windows.Forms.Label lblTongSoLuong;
        private System.Windows.Forms.Label lblTongSoLuongVal;
        private System.Windows.Forms.Label lblTongGiaTri;
        private System.Windows.Forms.Label lblTongGiaTriVal;
        private System.Windows.Forms.Panel pnlMain;
        private System.Windows.Forms.Panel pnlTopLeft;
        private System.Windows.Forms.Label lblTieuDe;
        private System.Windows.Forms.Button btnNhapFile;
        private System.Windows.Forms.Label lblHangHoa;
        private System.Windows.Forms.ComboBox cboHangHoa;
        private System.Windows.Forms.Label lblTonKhoGui;
        private System.Windows.Forms.Label lblSoLuong;
        private System.Windows.Forms.NumericUpDown nudSoLuong;
        private System.Windows.Forms.Button btnThemHang;
        private System.Windows.Forms.Panel pnlCenter;
        private System.Windows.Forms.DataGridView dgvChiTiet;
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
