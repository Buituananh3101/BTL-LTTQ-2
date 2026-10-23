namespace BTL_LTTQ_2.GUI.Forms.TraHangNhap
{
    partial class frmThemPhieuTraHang
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
            this.lblMaPhieuVal = new System.Windows.Forms.Label();
            this.lblTieuDe = new System.Windows.Forms.Label();
            this.grpThongTin = new System.Windows.Forms.GroupBox();
            this.cboKho = new System.Windows.Forms.ComboBox();
            this.lblKho = new System.Windows.Forms.Label();
            this.cboNhaCungCap = new System.Windows.Forms.ComboBox();
            this.lblNhaCungCap = new System.Windows.Forms.Label();
            this.txtGhiChu = new System.Windows.Forms.TextBox();
            this.lblGhiChu = new System.Windows.Forms.Label();
            this.cboNguoiTra = new System.Windows.Forms.ComboBox();
            this.lblNguoiTra = new System.Windows.Forms.Label();
            this.dtpNgayTra = new System.Windows.Forms.DateTimePicker();
            this.lblNgayTra = new System.Windows.Forms.Label();
            this.grpChonHang = new System.Windows.Forms.GroupBox();
            this.btnThemHang = new System.Windows.Forms.Button();
            this.txtDonGiaTra = new System.Windows.Forms.TextBox();
            this.lblDonGiaTra = new System.Windows.Forms.Label();
            this.txtSoLuongTra = new System.Windows.Forms.TextBox();
            this.lblSoLuongTra = new System.Windows.Forms.Label();
            this.lblTonKhoVal = new System.Windows.Forms.Label();
            this.lblTonKho = new System.Windows.Forms.Label();
            this.lblDonViTinhVal = new System.Windows.Forms.Label();
            this.lblDonViTinh = new System.Windows.Forms.Label();
            this.cboHangHoa = new System.Windows.Forms.ComboBox();
            this.lblHangHoa = new System.Windows.Forms.Label();
            this.grpHangTra = new System.Windows.Forms.GroupBox();
            this.dgvHangTra = new System.Windows.Forms.DataGridView();
            this.colStt = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colMaHangNoiBo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colTenHang = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colDonViTinh = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colTonKho = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colSoLuongTra = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colDonGiaTra = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colThanhTien = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colXoa = new System.Windows.Forms.DataGridViewButtonColumn();
            this.grpTinhToan = new System.Windows.Forms.GroupBox();
            this.txtDaThuNcc = new System.Windows.Forms.TextBox();
            this.lblDaThuNcc = new System.Windows.Forms.Label();
            this.lblCanThuNccVal = new System.Windows.Forms.Label();
            this.lblCanThuNcc = new System.Windows.Forms.Label();
            this.txtGiamGia = new System.Windows.Forms.TextBox();
            this.lblGiamGia = new System.Windows.Forms.Label();
            this.lblTongTienTraVal = new System.Windows.Forms.Label();
            this.lblTongTienTra = new System.Windows.Forms.Label();
            this.lblTongSlTraVal = new System.Windows.Forms.Label();
            this.lblTongSlTra = new System.Windows.Forms.Label();
            this.pnlBottom = new System.Windows.Forms.Panel();
            this.btnBoQua = new System.Windows.Forms.Button();
            this.btnLuuTam = new System.Windows.Forms.Button();
            this.btnXacNhanTra = new System.Windows.Forms.Button();
            this.pnlTop.SuspendLayout();
            this.grpThongTin.SuspendLayout();
            this.grpChonHang.SuspendLayout();
            this.grpHangTra.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvHangTra)).BeginInit();
            this.grpTinhToan.SuspendLayout();
            this.pnlBottom.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlTop
            // 
            this.pnlTop.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(250)))), ((int)(((byte)(252)))));
            this.pnlTop.Controls.Add(this.lblMaPhieuVal);
            this.pnlTop.Controls.Add(this.lblTieuDe);
            this.pnlTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlTop.Location = new System.Drawing.Point(0, 0);
            this.pnlTop.Name = "pnlTop";
            this.pnlTop.Size = new System.Drawing.Size(1100, 52);
            this.pnlTop.TabIndex = 0;
            // 
            // lblMaPhieuVal
            // 
            this.lblMaPhieuVal.AutoSize = true;
            this.lblMaPhieuVal.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.lblMaPhieuVal.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(71)))), ((int)(((byte)(85)))), ((int)(((byte)(105)))));
            this.lblMaPhieuVal.Location = new System.Drawing.Point(165, 17);
            this.lblMaPhieuVal.Name = "lblMaPhieuVal";
            this.lblMaPhieuVal.Size = new System.Drawing.Size(91, 19);
            this.lblMaPhieuVal.TabIndex = 1;
            this.lblMaPhieuVal.Text = "(THN000001)";
            // 
            // lblTieuDe
            // 
            this.lblTieuDe.AutoSize = true;
            this.lblTieuDe.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.lblTieuDe.Location = new System.Drawing.Point(20, 15);
            this.lblTieuDe.Name = "lblTieuDe";
            this.lblTieuDe.Size = new System.Drawing.Size(133, 21);
            this.lblTieuDe.TabIndex = 0;
            this.lblTieuDe.Text = "+ Trả hàng nhập";
            // 
            // grpThongTin
            // 
            this.grpThongTin.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.grpThongTin.Controls.Add(this.cboKho);
            this.grpThongTin.Controls.Add(this.lblKho);
            this.grpThongTin.Controls.Add(this.cboNhaCungCap);
            this.grpThongTin.Controls.Add(this.lblNhaCungCap);
            this.grpThongTin.Controls.Add(this.txtGhiChu);
            this.grpThongTin.Controls.Add(this.lblGhiChu);
            this.grpThongTin.Controls.Add(this.cboNguoiTra);
            this.grpThongTin.Controls.Add(this.lblNguoiTra);
            this.grpThongTin.Controls.Add(this.dtpNgayTra);
            this.grpThongTin.Controls.Add(this.lblNgayTra);
            this.grpThongTin.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.grpThongTin.Location = new System.Drawing.Point(16, 58);
            this.grpThongTin.Name = "grpThongTin";
            this.grpThongTin.Size = new System.Drawing.Size(1068, 100);
            this.grpThongTin.TabIndex = 1;
            this.grpThongTin.TabStop = false;
            this.grpThongTin.Text = "Thông tin chung";
            // 
            // cboKho
            // 
            this.cboKho.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboKho.FormattingEnabled = true;
            this.cboKho.Location = new System.Drawing.Point(120, 60);
            this.cboKho.Name = "cboKho";
            this.cboKho.Size = new System.Drawing.Size(250, 25);
            this.cboKho.TabIndex = 3;
            this.cboKho.SelectedIndexChanged += new System.EventHandler(this.cboKho_SelectedIndexChanged);
            // 
            // lblKho
            // 
            this.lblKho.AutoSize = true;
            this.lblKho.Location = new System.Drawing.Point(16, 64);
            this.lblKho.Name = "lblKho";
            this.lblKho.Size = new System.Drawing.Size(62, 17);
            this.lblKho.TabIndex = 2;
            this.lblKho.Text = "Kho xuất:";
            // 
            // cboNhaCungCap
            // 
            this.cboNhaCungCap.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboNhaCungCap.FormattingEnabled = true;
            this.cboNhaCungCap.Location = new System.Drawing.Point(120, 24);
            this.cboNhaCungCap.Name = "cboNhaCungCap";
            this.cboNhaCungCap.Size = new System.Drawing.Size(250, 25);
            this.cboNhaCungCap.TabIndex = 1;
            // 
            // lblNhaCungCap
            // 
            this.lblNhaCungCap.AutoSize = true;
            this.lblNhaCungCap.Location = new System.Drawing.Point(16, 28);
            this.lblNhaCungCap.Name = "lblNhaCungCap";
            this.lblNhaCungCap.Size = new System.Drawing.Size(92, 17);
            this.lblNhaCungCap.TabIndex = 0;
            this.lblNhaCungCap.Text = "Nhà cung cấp:";
            // 
            // txtGhiChu
            // 
            this.txtGhiChu.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtGhiChu.Location = new System.Drawing.Point(785, 24);
            this.txtGhiChu.Multiline = true;
            this.txtGhiChu.Name = "txtGhiChu";
            this.txtGhiChu.Size = new System.Drawing.Size(267, 61);
            this.txtGhiChu.TabIndex = 9;
            // 
            // lblGhiChu
            // 
            this.lblGhiChu.AutoSize = true;
            this.lblGhiChu.Location = new System.Drawing.Point(725, 28);
            this.lblGhiChu.Name = "lblGhiChu";
            this.lblGhiChu.Size = new System.Drawing.Size(54, 17);
            this.lblGhiChu.TabIndex = 8;
            this.lblGhiChu.Text = "Ghi chú:";
            // 
            // cboNguoiTra
            // 
            this.cboNguoiTra.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboNguoiTra.FormattingEnabled = true;
            this.cboNguoiTra.Location = new System.Drawing.Point(475, 24);
            this.cboNguoiTra.Name = "cboNguoiTra";
            this.cboNguoiTra.Size = new System.Drawing.Size(225, 25);
            this.cboNguoiTra.TabIndex = 5;
            // 
            // lblNguoiTra
            // 
            this.lblNguoiTra.AutoSize = true;
            this.lblNguoiTra.Location = new System.Drawing.Point(395, 28);
            this.lblNguoiTra.Name = "lblNguoiTra";
            this.lblNguoiTra.Size = new System.Drawing.Size(66, 17);
            this.lblNguoiTra.TabIndex = 4;
            this.lblNguoiTra.Text = "Người trả:";
            // 
            // dtpNgayTra
            // 
            this.dtpNgayTra.CustomFormat = "dd/MM/yyyy HH:mm";
            this.dtpNgayTra.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpNgayTra.Location = new System.Drawing.Point(475, 60);
            this.dtpNgayTra.Name = "dtpNgayTra";
            this.dtpNgayTra.Size = new System.Drawing.Size(225, 24);
            this.dtpNgayTra.TabIndex = 7;
            // 
            // lblNgayTra
            // 
            this.lblNgayTra.AutoSize = true;
            this.lblNgayTra.Location = new System.Drawing.Point(395, 64);
            this.lblNgayTra.Name = "lblNgayTra";
            this.lblNgayTra.Size = new System.Drawing.Size(62, 17);
            this.lblNgayTra.TabIndex = 6;
            this.lblNgayTra.Text = "Ngày trả:";
            // 
            // grpChonHang
            // 
            this.grpChonHang.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.grpChonHang.Controls.Add(this.btnThemHang);
            this.grpChonHang.Controls.Add(this.txtDonGiaTra);
            this.grpChonHang.Controls.Add(this.lblDonGiaTra);
            this.grpChonHang.Controls.Add(this.txtSoLuongTra);
            this.grpChonHang.Controls.Add(this.lblSoLuongTra);
            this.grpChonHang.Controls.Add(this.lblTonKhoVal);
            this.grpChonHang.Controls.Add(this.lblTonKho);
            this.grpChonHang.Controls.Add(this.lblDonViTinhVal);
            this.grpChonHang.Controls.Add(this.lblDonViTinh);
            this.grpChonHang.Controls.Add(this.cboHangHoa);
            this.grpChonHang.Controls.Add(this.lblHangHoa);
            this.grpChonHang.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.grpChonHang.Location = new System.Drawing.Point(16, 164);
            this.grpChonHang.Name = "grpChonHang";
            this.grpChonHang.Size = new System.Drawing.Size(1068, 70);
            this.grpChonHang.TabIndex = 2;
            this.grpChonHang.TabStop = false;
            this.grpChonHang.Text = "Chọn sản phẩm từ danh mục hàng hóa";
            // 
            // btnThemHang
            // 
            this.btnThemHang.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnThemHang.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.btnThemHang.Location = new System.Drawing.Point(950, 24);
            this.btnThemHang.Name = "btnThemHang";
            this.btnThemHang.Size = new System.Drawing.Size(102, 30);
            this.btnThemHang.TabIndex = 10;
            this.btnThemHang.Text = "+ Thêm hàng";
            this.btnThemHang.UseVisualStyleBackColor = true;
            this.btnThemHang.Click += new System.EventHandler(this.btnThemHang_Click);
            // 
            // txtDonGiaTra
            // 
            this.txtDonGiaTra.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.txtDonGiaTra.Location = new System.Drawing.Point(825, 27);
            this.txtDonGiaTra.Name = "txtDonGiaTra";
            this.txtDonGiaTra.Size = new System.Drawing.Size(110, 24);
            this.txtDonGiaTra.TabIndex = 9;
            this.txtDonGiaTra.Text = "0";
            this.txtDonGiaTra.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtDonGiaTra.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtDonGiaTra_KeyDown);
            // 
            // lblDonGiaTra
            // 
            this.lblDonGiaTra.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblDonGiaTra.AutoSize = true;
            this.lblDonGiaTra.Location = new System.Drawing.Point(750, 31);
            this.lblDonGiaTra.Name = "lblDonGiaTra";
            this.lblDonGiaTra.Size = new System.Drawing.Size(73, 17);
            this.lblDonGiaTra.TabIndex = 8;
            this.lblDonGiaTra.Text = "Giá trả lại:";
            // 
            // txtSoLuongTra
            // 
            this.txtSoLuongTra.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.txtSoLuongTra.Location = new System.Drawing.Point(670, 27);
            this.txtSoLuongTra.Name = "txtSoLuongTra";
            this.txtSoLuongTra.Size = new System.Drawing.Size(70, 24);
            this.txtSoLuongTra.TabIndex = 7;
            this.txtSoLuongTra.Text = "1";
            this.txtSoLuongTra.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtSoLuongTra.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtSoLuongTra_KeyDown);
            // 
            // lblSoLuongTra
            // 
            this.lblSoLuongTra.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblSoLuongTra.AutoSize = true;
            this.lblSoLuongTra.Location = new System.Drawing.Point(615, 31);
            this.lblSoLuongTra.Name = "lblSoLuongTra";
            this.lblSoLuongTra.Size = new System.Drawing.Size(51, 17);
            this.lblSoLuongTra.TabIndex = 6;
            this.lblSoLuongTra.Text = "SL trả:";
            // 
            // lblTonKhoVal
            // 
            this.lblTonKhoVal.AutoSize = true;
            this.lblTonKhoVal.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.lblTonKhoVal.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(2)))), ((int)(((byte)(132)))), ((int)(((byte)(199)))));
            this.lblTonKhoVal.Location = new System.Drawing.Point(545, 31);
            this.lblTonKhoVal.Name = "lblTonKhoVal";
            this.lblTonKhoVal.Size = new System.Drawing.Size(15, 17);
            this.lblTonKhoVal.TabIndex = 5;
            this.lblTonKhoVal.Text = "0";
            // 
            // lblTonKho
            // 
            this.lblTonKho.AutoSize = true;
            this.lblTonKho.Location = new System.Drawing.Point(485, 31);
            this.lblTonKho.Name = "lblTonKho";
            this.lblTonKho.Size = new System.Drawing.Size(58, 17);
            this.lblTonKho.TabIndex = 4;
            this.lblTonKho.Text = "Tồn kho:";
            // 
            // lblDonViTinhVal
            // 
            this.lblDonViTinhVal.AutoSize = true;
            this.lblDonViTinhVal.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(71)))), ((int)(((byte)(85)))), ((int)(((byte)(105)))));
            this.lblDonViTinhVal.Location = new System.Drawing.Point(420, 31);
            this.lblDonViTinhVal.Name = "lblDonViTinhVal";
            this.lblDonViTinhVal.Size = new System.Drawing.Size(26, 17);
            this.lblDonViTinhVal.TabIndex = 3;
            this.lblDonViTinhVal.Text = "Cái";
            // 
            // lblDonViTinh
            // 
            this.lblDonViTinh.AutoSize = true;
            this.lblDonViTinh.Location = new System.Drawing.Point(380, 31);
            this.lblDonViTinh.Name = "lblDonViTinh";
            this.lblDonViTinh.Size = new System.Drawing.Size(36, 17);
            this.lblDonViTinh.TabIndex = 2;
            this.lblDonViTinh.Text = "ĐVT:";
            // 
            // cboHangHoa
            // 
            this.cboHangHoa.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.cboHangHoa.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cboHangHoa.FormattingEnabled = true;
            this.cboHangHoa.Location = new System.Drawing.Point(90, 27);
            this.cboHangHoa.Name = "cboHangHoa";
            this.cboHangHoa.Size = new System.Drawing.Size(280, 25);
            this.cboHangHoa.TabIndex = 1;
            this.cboHangHoa.SelectedIndexChanged += new System.EventHandler(this.cboHangHoa_SelectedIndexChanged);
            // 
            // lblHangHoa
            // 
            this.lblHangHoa.AutoSize = true;
            this.lblHangHoa.Location = new System.Drawing.Point(16, 31);
            this.lblHangHoa.Name = "lblHangHoa";
            this.lblHangHoa.Size = new System.Drawing.Size(68, 17);
            this.lblHangHoa.TabIndex = 0;
            this.lblHangHoa.Text = "Hàng hóa:";
            // 
            // grpHangTra
            // 
            this.grpHangTra.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.grpHangTra.Controls.Add(this.dgvHangTra);
            this.grpHangTra.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.grpHangTra.Location = new System.Drawing.Point(16, 240);
            this.grpHangTra.Name = "grpHangTra";
            this.grpHangTra.Padding = new System.Windows.Forms.Padding(8);
            this.grpHangTra.Size = new System.Drawing.Size(1068, 335);
            this.grpHangTra.TabIndex = 3;
            this.grpHangTra.TabStop = false;
            this.grpHangTra.Text = "Danh sách hàng trả (Nhập số lượng và giá trả trực tiếp trên lưới)";
            // 
            // dgvHangTra
            // 
            this.dgvHangTra.AllowUserToAddRows = false;
            this.dgvHangTra.AllowUserToDeleteRows = false;
            this.dgvHangTra.BackgroundColor = System.Drawing.Color.White;
            this.dgvHangTra.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dgvHangTra.ColumnHeadersHeight = 36;
            this.dgvHangTra.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colStt,
            this.colMaHangNoiBo,
            this.colTenHang,
            this.colDonViTinh,
            this.colTonKho,
            this.colSoLuongTra,
            this.colDonGiaTra,
            this.colThanhTien,
            this.colXoa});
            this.dgvHangTra.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvHangTra.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnEnter;
            this.dgvHangTra.Location = new System.Drawing.Point(8, 25);
            this.dgvHangTra.Name = "dgvHangTra";
            this.dgvHangTra.RowHeadersVisible = false;
            this.dgvHangTra.RowHeadersWidth = 51;
            this.dgvHangTra.RowTemplate.Height = 32;
            this.dgvHangTra.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.CellSelect;
            this.dgvHangTra.Size = new System.Drawing.Size(1052, 302);
            this.dgvHangTra.TabIndex = 0;
            this.dgvHangTra.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvHangTra_CellContentClick);
            this.dgvHangTra.CellValueChanged += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvHangTra_CellValueChanged);
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
            this.colMaHangNoiBo.Width = 110;
            // 
            // colTenHang
            // 
            this.colTenHang.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.colTenHang.DataPropertyName = "TenHang";
            this.colTenHang.HeaderText = "Tên hàng hóa";
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
            this.colTonKho.Width = 90;
            // 
            // colSoLuongTra
            // 
            this.colSoLuongTra.DataPropertyName = "SoLuongTra";
            this.colSoLuongTra.HeaderText = "Số lượng trả";
            this.colSoLuongTra.MinimumWidth = 6;
            this.colSoLuongTra.Name = "colSoLuongTra";
            this.colSoLuongTra.Width = 110;
            // 
            // colDonGiaTra
            // 
            this.colDonGiaTra.DataPropertyName = "DonGiaTra";
            this.colDonGiaTra.HeaderText = "Giá trả lại";
            this.colDonGiaTra.MinimumWidth = 6;
            this.colDonGiaTra.Name = "colDonGiaTra";
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
            // colXoa
            // 
            this.colXoa.HeaderText = "";
            this.colXoa.MinimumWidth = 6;
            this.colXoa.Name = "colXoa";
            this.colXoa.Text = "Xóa";
            this.colXoa.UseColumnTextForButtonValue = true;
            this.colXoa.Width = 60;
            // 
            // grpTinhToan
            // 
            this.grpTinhToan.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.grpTinhToan.Controls.Add(this.txtDaThuNcc);
            this.grpTinhToan.Controls.Add(this.lblDaThuNcc);
            this.grpTinhToan.Controls.Add(this.lblCanThuNccVal);
            this.grpTinhToan.Controls.Add(this.lblCanThuNcc);
            this.grpTinhToan.Controls.Add(this.txtGiamGia);
            this.grpTinhToan.Controls.Add(this.lblGiamGia);
            this.grpTinhToan.Controls.Add(this.lblTongTienTraVal);
            this.grpTinhToan.Controls.Add(this.lblTongTienTra);
            this.grpTinhToan.Controls.Add(this.lblTongSlTraVal);
            this.grpTinhToan.Controls.Add(this.lblTongSlTra);
            this.grpTinhToan.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.grpTinhToan.Location = new System.Drawing.Point(16, 580);
            this.grpTinhToan.Name = "grpTinhToan";
            this.grpTinhToan.Size = new System.Drawing.Size(1068, 125);
            this.grpTinhToan.TabIndex = 4;
            this.grpTinhToan.TabStop = false;
            this.grpTinhToan.Text = "Tổng kết thanh toán";
            // 
            // txtDaThuNcc
            // 
            this.txtDaThuNcc.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.txtDaThuNcc.Location = new System.Drawing.Point(880, 85);
            this.txtDaThuNcc.Name = "txtDaThuNcc";
            this.txtDaThuNcc.Size = new System.Drawing.Size(160, 24);
            this.txtDaThuNcc.TabIndex = 9;
            this.txtDaThuNcc.Text = "0";
            this.txtDaThuNcc.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // lblDaThuNcc
            // 
            this.lblDaThuNcc.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblDaThuNcc.AutoSize = true;
            this.lblDaThuNcc.Location = new System.Drawing.Point(740, 89);
            this.lblDaThuNcc.Name = "lblDaThuNcc";
            this.lblDaThuNcc.Size = new System.Drawing.Size(102, 17);
            this.lblDaThuNcc.TabIndex = 8;
            this.lblDaThuNcc.Text = "Tiền NCC đã trả:";
            // 
            // lblCanThuNccVal
            // 
            this.lblCanThuNccVal.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblCanThuNccVal.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.lblCanThuNccVal.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(2)))), ((int)(((byte)(132)))), ((int)(((byte)(199)))));
            this.lblCanThuNccVal.Location = new System.Drawing.Point(880, 52);
            this.lblCanThuNccVal.Name = "lblCanThuNccVal";
            this.lblCanThuNccVal.Size = new System.Drawing.Size(160, 20);
            this.lblCanThuNccVal.TabIndex = 7;
            this.lblCanThuNccVal.Text = "0 đ";
            this.lblCanThuNccVal.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblCanThuNcc
            // 
            this.lblCanThuNcc.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblCanThuNcc.AutoSize = true;
            this.lblCanThuNcc.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.lblCanThuNcc.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(23)))), ((int)(((byte)(42)))));
            this.lblCanThuNcc.Location = new System.Drawing.Point(740, 55);
            this.lblCanThuNcc.Name = "lblCanThuNcc";
            this.lblCanThuNcc.Size = new System.Drawing.Size(84, 17);
            this.lblCanThuNcc.TabIndex = 6;
            this.lblCanThuNcc.Text = "NCC cần trả:";
            // 
            // txtGiamGia
            // 
            this.txtGiamGia.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.txtGiamGia.Location = new System.Drawing.Point(880, 20);
            this.txtGiamGia.Name = "txtGiamGia";
            this.txtGiamGia.Size = new System.Drawing.Size(160, 24);
            this.txtGiamGia.TabIndex = 5;
            this.txtGiamGia.Text = "0";
            this.txtGiamGia.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtGiamGia.TextChanged += new System.EventHandler(this.txtGiamGia_TextChanged);
            // 
            // lblGiamGia
            // 
            this.lblGiamGia.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblGiamGia.AutoSize = true;
            this.lblGiamGia.Location = new System.Drawing.Point(740, 24);
            this.lblGiamGia.Name = "lblGiamGia";
            this.lblGiamGia.Size = new System.Drawing.Size(63, 17);
            this.lblGiamGia.TabIndex = 4;
            this.lblGiamGia.Text = "Giảm giá:";
            // 
            // lblTongTienTraVal
            // 
            this.lblTongTienTraVal.AutoSize = true;
            this.lblTongTienTraVal.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.lblTongTienTraVal.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(23)))), ((int)(((byte)(42)))));
            this.lblTongTienTraVal.Location = new System.Drawing.Point(170, 60);
            this.lblTongTienTraVal.Name = "lblTongTienTraVal";
            this.lblTongTienTraVal.Size = new System.Drawing.Size(32, 19);
            this.lblTongTienTraVal.TabIndex = 3;
            this.lblTongTienTraVal.Text = "0 đ";
            // 
            // lblTongTienTra
            // 
            this.lblTongTienTra.AutoSize = true;
            this.lblTongTienTra.Location = new System.Drawing.Point(20, 60);
            this.lblTongTienTra.Name = "lblTongTienTra";
            this.lblTongTienTra.Size = new System.Drawing.Size(126, 17);
            this.lblTongTienTra.TabIndex = 2;
            this.lblTongTienTra.Text = "Tổng tiền hàng trả:";
            // 
            // lblTongSlTraVal
            // 
            this.lblTongSlTraVal.AutoSize = true;
            this.lblTongSlTraVal.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.lblTongSlTraVal.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(2)))), ((int)(((byte)(132)))), ((int)(((byte)(199)))));
            this.lblTongSlTraVal.Location = new System.Drawing.Point(170, 26);
            this.lblTongSlTraVal.Name = "lblTongSlTraVal";
            this.lblTongSlTraVal.Size = new System.Drawing.Size(17, 19);
            this.lblTongSlTraVal.TabIndex = 1;
            this.lblTongSlTraVal.Text = "0";
            // 
            // lblTongSlTra
            // 
            this.lblTongSlTra.AutoSize = true;
            this.lblTongSlTra.Location = new System.Drawing.Point(20, 26);
            this.lblTongSlTra.Name = "lblTongSlTra";
            this.lblTongSlTra.Size = new System.Drawing.Size(117, 17);
            this.lblTongSlTra.TabIndex = 0;
            this.lblTongSlTra.Text = "Tổng số lượng trả:";
            // 
            // pnlBottom
            // 
            this.pnlBottom.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(250)))), ((int)(((byte)(252)))));
            this.pnlBottom.Controls.Add(this.btnBoQua);
            this.pnlBottom.Controls.Add(this.btnLuuTam);
            this.pnlBottom.Controls.Add(this.btnXacNhanTra);
            this.pnlBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnlBottom.Location = new System.Drawing.Point(0, 715);
            this.pnlBottom.Name = "pnlBottom";
            this.pnlBottom.Size = new System.Drawing.Size(1100, 55);
            this.pnlBottom.TabIndex = 5;
            // 
            // btnBoQua
            // 
            this.btnBoQua.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnBoQua.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnBoQua.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.btnBoQua.Location = new System.Drawing.Point(985, 10);
            this.btnBoQua.Name = "btnBoQua";
            this.btnBoQua.Size = new System.Drawing.Size(95, 35);
            this.btnBoQua.TabIndex = 2;
            this.btnBoQua.Text = "Bỏ qua";
            this.btnBoQua.UseVisualStyleBackColor = true;
            this.btnBoQua.Click += new System.EventHandler(this.btnBoQua_Click);
            // 
            // btnLuuTam
            // 
            this.btnLuuTam.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnLuuTam.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.btnLuuTam.Location = new System.Drawing.Point(755, 10);
            this.btnLuuTam.Name = "btnLuuTam";
            this.btnLuuTam.Size = new System.Drawing.Size(100, 35);
            this.btnLuuTam.TabIndex = 0;
            this.btnLuuTam.Text = "Lưu tạm";
            this.btnLuuTam.UseVisualStyleBackColor = true;
            this.btnLuuTam.Click += new System.EventHandler(this.btnLuuTam_Click);
            // 
            // btnXacNhanTra
            // 
            this.btnXacNhanTra.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnXacNhanTra.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.btnXacNhanTra.Location = new System.Drawing.Point(865, 10);
            this.btnXacNhanTra.Name = "btnXacNhanTra";
            this.btnXacNhanTra.Size = new System.Drawing.Size(110, 35);
            this.btnXacNhanTra.TabIndex = 1;
            this.btnXacNhanTra.Text = "Trả hàng";
            this.btnXacNhanTra.UseVisualStyleBackColor = true;
            this.btnXacNhanTra.Click += new System.EventHandler(this.btnXacNhanTra_Click);
            // 
            // frmThemPhieuTraHang
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.CancelButton = this.btnBoQua;
            this.ClientSize = new System.Drawing.Size(1100, 770);
            this.Controls.Add(this.grpHangTra);
            this.Controls.Add(this.grpChonHang);
            this.Controls.Add(this.grpTinhToan);
            this.Controls.Add(this.pnlBottom);
            this.Controls.Add(this.grpThongTin);
            this.Controls.Add(this.pnlTop);
            this.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            this.Margin = new System.Windows.Forms.Padding(4);
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(1050, 740);
            this.Name = "frmThemPhieuTraHang";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Thêm phiếu trả hàng nhập";
            this.Load += new System.EventHandler(this.frmThemPhieuTraHang_Load);
            this.pnlTop.ResumeLayout(false);
            this.pnlTop.PerformLayout();
            this.grpThongTin.ResumeLayout(false);
            this.grpThongTin.PerformLayout();
            this.grpChonHang.ResumeLayout(false);
            this.grpChonHang.PerformLayout();
            this.grpHangTra.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvHangTra)).EndInit();
            this.grpTinhToan.ResumeLayout(false);
            this.grpTinhToan.PerformLayout();
            this.pnlBottom.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pnlTop;
        private System.Windows.Forms.Label lblMaPhieuVal;
        private System.Windows.Forms.Label lblTieuDe;
        private System.Windows.Forms.GroupBox grpThongTin;
        private System.Windows.Forms.ComboBox cboKho;
        private System.Windows.Forms.Label lblKho;
        private System.Windows.Forms.ComboBox cboNhaCungCap;
        private System.Windows.Forms.Label lblNhaCungCap;
        private System.Windows.Forms.TextBox txtGhiChu;
        private System.Windows.Forms.Label lblGhiChu;
        private System.Windows.Forms.ComboBox cboNguoiTra;
        private System.Windows.Forms.Label lblNguoiTra;
        private System.Windows.Forms.DateTimePicker dtpNgayTra;
        private System.Windows.Forms.Label lblNgayTra;
        private System.Windows.Forms.GroupBox grpChonHang;
        private System.Windows.Forms.Button btnThemHang;
        private System.Windows.Forms.TextBox txtDonGiaTra;
        private System.Windows.Forms.Label lblDonGiaTra;
        private System.Windows.Forms.TextBox txtSoLuongTra;
        private System.Windows.Forms.Label lblSoLuongTra;
        private System.Windows.Forms.Label lblTonKhoVal;
        private System.Windows.Forms.Label lblTonKho;
        private System.Windows.Forms.Label lblDonViTinhVal;
        private System.Windows.Forms.Label lblDonViTinh;
        private System.Windows.Forms.ComboBox cboHangHoa;
        private System.Windows.Forms.Label lblHangHoa;
        private System.Windows.Forms.GroupBox grpHangTra;
        private System.Windows.Forms.DataGridView dgvHangTra;
        private System.Windows.Forms.DataGridViewTextBoxColumn colStt;
        private System.Windows.Forms.DataGridViewTextBoxColumn colMaHangNoiBo;
        private System.Windows.Forms.DataGridViewTextBoxColumn colTenHang;
        private System.Windows.Forms.DataGridViewTextBoxColumn colDonViTinh;
        private System.Windows.Forms.DataGridViewTextBoxColumn colTonKho;
        private System.Windows.Forms.DataGridViewTextBoxColumn colSoLuongTra;
        private System.Windows.Forms.DataGridViewTextBoxColumn colDonGiaTra;
        private System.Windows.Forms.DataGridViewTextBoxColumn colThanhTien;
        private System.Windows.Forms.DataGridViewButtonColumn colXoa;
        private System.Windows.Forms.GroupBox grpTinhToan;
        private System.Windows.Forms.TextBox txtDaThuNcc;
        private System.Windows.Forms.Label lblDaThuNcc;
        private System.Windows.Forms.Label lblCanThuNccVal;
        private System.Windows.Forms.Label lblCanThuNcc;
        private System.Windows.Forms.TextBox txtGiamGia;
        private System.Windows.Forms.Label lblGiamGia;
        private System.Windows.Forms.Label lblTongTienTraVal;
        private System.Windows.Forms.Label lblTongTienTra;
        private System.Windows.Forms.Label lblTongSlTraVal;
        private System.Windows.Forms.Label lblTongSlTra;
        private System.Windows.Forms.Panel pnlBottom;
        private System.Windows.Forms.Button btnBoQua;
        private System.Windows.Forms.Button btnLuuTam;
        private System.Windows.Forms.Button btnXacNhanTra;
    }
}
