namespace BTL_LTTQ_2.GUI.Forms.NhapHang
{
    partial class frmTraHangNhap
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
            this.lblMaPhieuGoc = new System.Windows.Forms.Label();
            this.lblTieuDe = new System.Windows.Forms.Label();
            this.grpThongTin = new System.Windows.Forms.GroupBox();
            this.txtGhiChu = new System.Windows.Forms.TextBox();
            this.lblGhiChu = new System.Windows.Forms.Label();
            this.lblNoCanTraGocVal = new System.Windows.Forms.Label();
            this.lblNoCanTraGoc = new System.Windows.Forms.Label();
            this.cboNguoiTra = new System.Windows.Forms.ComboBox();
            this.lblNguoiTra = new System.Windows.Forms.Label();
            this.dtpNgayTra = new System.Windows.Forms.DateTimePicker();
            this.lblNgayTra = new System.Windows.Forms.Label();
            this.lblKhoVal = new System.Windows.Forms.Label();
            this.lblKho = new System.Windows.Forms.Label();
            this.lblNccVal = new System.Windows.Forms.Label();
            this.lblNcc = new System.Windows.Forms.Label();
            this.grpHangTra = new System.Windows.Forms.GroupBox();
            this.dgvHangTra = new System.Windows.Forms.DataGridView();
            this.colStt = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colMaHang = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colMaHangNoiBo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colTenHang = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colDonViTinh = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colSoLuongNhap = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colTonKho = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colSoLuongTra = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colDonGiaTra = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colThanhTien = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.grpTinhToan = new System.Windows.Forms.GroupBox();
            this.txtTienNccTraLai = new System.Windows.Forms.TextBox();
            this.lblTienNccTraLai = new System.Windows.Forms.Label();
            this.txtNoConLai = new System.Windows.Forms.TextBox();
            this.lblNoConLai = new System.Windows.Forms.Label();
            this.txtTruVaoCongNo = new System.Windows.Forms.TextBox();
            this.lblTruVaoCongNo = new System.Windows.Forms.Label();
            this.txtTongTienTra = new System.Windows.Forms.TextBox();
            this.lblTongTienTra = new System.Windows.Forms.Label();
            this.lblTongSlTra = new System.Windows.Forms.Label();
            this.pnlBottom = new System.Windows.Forms.Panel();
            this.btnBoQua = new System.Windows.Forms.Button();
            this.btnXacNhanTra = new System.Windows.Forms.Button();
            this.pnlTop.SuspendLayout();
            this.grpThongTin.SuspendLayout();
            this.grpHangTra.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvHangTra)).BeginInit();
            this.grpTinhToan.SuspendLayout();
            this.pnlBottom.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlTop
            // 
            this.pnlTop.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(250)))), ((int)(((byte)(252)))));
            this.pnlTop.Controls.Add(this.lblMaPhieuGoc);
            this.pnlTop.Controls.Add(this.lblTieuDe);
            this.pnlTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlTop.Location = new System.Drawing.Point(0, 0);
            this.pnlTop.Name = "pnlTop";
            this.pnlTop.Size = new System.Drawing.Size(1034, 52);
            this.pnlTop.TabIndex = 0;
            // 
            // lblMaPhieuGoc
            // 
            this.lblMaPhieuGoc.AutoSize = true;
            this.lblMaPhieuGoc.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.lblMaPhieuGoc.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(71)))), ((int)(((byte)(85)))), ((int)(((byte)(105)))));
            this.lblMaPhieuGoc.Location = new System.Drawing.Point(165, 17);
            this.lblMaPhieuGoc.Name = "lblMaPhieuGoc";
            this.lblMaPhieuGoc.Size = new System.Drawing.Size(176, 19);
            this.lblMaPhieuGoc.TabIndex = 1;
            this.lblMaPhieuGoc.Text = "(Theo đơn nhập: PN000046)";
            // 
            // lblTieuDe
            // 
            this.lblTieuDe.AutoSize = true;
            this.lblTieuDe.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.lblTieuDe.Location = new System.Drawing.Point(20, 15);
            this.lblTieuDe.Name = "lblTieuDe";
            this.lblTieuDe.Size = new System.Drawing.Size(121, 21);
            this.lblTieuDe.TabIndex = 0;
            this.lblTieuDe.Text = "Trả hàng nhập";
            // 
            // grpThongTin
            // 
            this.grpThongTin.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.grpThongTin.Controls.Add(this.txtGhiChu);
            this.grpThongTin.Controls.Add(this.lblGhiChu);
            this.grpThongTin.Controls.Add(this.lblNoCanTraGocVal);
            this.grpThongTin.Controls.Add(this.lblNoCanTraGoc);
            this.grpThongTin.Controls.Add(this.cboNguoiTra);
            this.grpThongTin.Controls.Add(this.lblNguoiTra);
            this.grpThongTin.Controls.Add(this.dtpNgayTra);
            this.grpThongTin.Controls.Add(this.lblNgayTra);
            this.grpThongTin.Controls.Add(this.lblKhoVal);
            this.grpThongTin.Controls.Add(this.lblKho);
            this.grpThongTin.Controls.Add(this.lblNccVal);
            this.grpThongTin.Controls.Add(this.lblNcc);
            this.grpThongTin.Location = new System.Drawing.Point(12, 58);
            this.grpThongTin.Name = "grpThongTin";
            this.grpThongTin.Size = new System.Drawing.Size(1010, 110);
            this.grpThongTin.TabIndex = 1;
            this.grpThongTin.TabStop = false;
            this.grpThongTin.Text = "Thông tin chung";
            // 
            // txtGhiChu
            // 
            this.txtGhiChu.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtGhiChu.Location = new System.Drawing.Point(520, 68);
            this.txtGhiChu.Name = "txtGhiChu";
            this.txtGhiChu.Size = new System.Drawing.Size(470, 25);
            this.txtGhiChu.TabIndex = 11;
            // 
            // lblGhiChu
            // 
            this.lblGhiChu.AutoSize = true;
            this.lblGhiChu.Location = new System.Drawing.Point(440, 72);
            this.lblGhiChu.Name = "lblGhiChu";
            this.lblGhiChu.Size = new System.Drawing.Size(65, 17);
            this.lblGhiChu.TabIndex = 10;
            this.lblGhiChu.Text = "Lý do trả:";
            // 
            // lblNoCanTraGocVal
            // 
            this.lblNoCanTraGocVal.AutoSize = true;
            this.lblNoCanTraGocVal.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.lblNoCanTraGocVal.ForeColor = System.Drawing.Color.Red;
            this.lblNoCanTraGocVal.Location = new System.Drawing.Point(135, 72);
            this.lblNoCanTraGocVal.Name = "lblNoCanTraGocVal";
            this.lblNoCanTraGocVal.Size = new System.Drawing.Size(15, 17);
            this.lblNoCanTraGocVal.TabIndex = 9;
            this.lblNoCanTraGocVal.Text = "0";
            // 
            // lblNoCanTraGoc
            // 
            this.lblNoCanTraGoc.AutoSize = true;
            this.lblNoCanTraGoc.Location = new System.Drawing.Point(20, 72);
            this.lblNoCanTraGoc.Name = "lblNoCanTraGoc";
            this.lblNoCanTraGoc.Size = new System.Drawing.Size(107, 17);
            this.lblNoCanTraGoc.TabIndex = 8;
            this.lblNoCanTraGoc.Text = "Nợ cần trả đơn:";
            // 
            // cboNguoiTra
            // 
            this.cboNguoiTra.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboNguoiTra.FormattingEnabled = true;
            this.cboNguoiTra.Location = new System.Drawing.Point(820, 26);
            this.cboNguoiTra.Name = "cboNguoiTra";
            this.cboNguoiTra.Size = new System.Drawing.Size(170, 25);
            this.cboNguoiTra.TabIndex = 7;
            // 
            // lblNguoiTra
            // 
            this.lblNguoiTra.AutoSize = true;
            this.lblNguoiTra.Location = new System.Drawing.Point(745, 30);
            this.lblNguoiTra.Name = "lblNguoiTra";
            this.lblNguoiTra.Size = new System.Drawing.Size(68, 17);
            this.lblNguoiTra.TabIndex = 6;
            this.lblNguoiTra.Text = "Người trả:";
            // 
            // dtpNgayTra
            // 
            this.dtpNgayTra.CustomFormat = "dd/MM/yyyy HH:mm";
            this.dtpNgayTra.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpNgayTra.Location = new System.Drawing.Point(520, 26);
            this.dtpNgayTra.Name = "dtpNgayTra";
            this.dtpNgayTra.Size = new System.Drawing.Size(190, 25);
            this.dtpNgayTra.TabIndex = 5;
            // 
            // lblNgayTra
            // 
            this.lblNgayTra.AutoSize = true;
            this.lblNgayTra.Location = new System.Drawing.Point(440, 30);
            this.lblNgayTra.Name = "lblNgayTra";
            this.lblNgayTra.Size = new System.Drawing.Size(62, 17);
            this.lblNgayTra.TabIndex = 4;
            this.lblNgayTra.Text = "Ngày trả:";
            // 
            // lblKhoVal
            // 
            this.lblKhoVal.AutoSize = true;
            this.lblKhoVal.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.lblKhoVal.Location = new System.Drawing.Point(135, 48);
            this.lblKhoVal.Name = "lblKhoVal";
            this.lblKhoVal.Size = new System.Drawing.Size(95, 17);
            this.lblKhoVal.TabIndex = 3;
            this.lblKhoVal.Text = "Kho trung tâm";
            // 
            // lblKho
            // 
            this.lblKho.AutoSize = true;
            this.lblKho.Location = new System.Drawing.Point(20, 48);
            this.lblKho.Name = "lblKho";
            this.lblKho.Size = new System.Drawing.Size(84, 17);
            this.lblKho.TabIndex = 2;
            this.lblKho.Text = "Kho xuất trả:";
            // 
            // lblNccVal
            // 
            this.lblNccVal.AutoSize = true;
            this.lblNccVal.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.lblNccVal.Location = new System.Drawing.Point(135, 24);
            this.lblNccVal.Name = "lblNccVal";
            this.lblNccVal.Size = new System.Drawing.Size(124, 17);
            this.lblNccVal.TabIndex = 1;
            this.lblNccVal.Text = "Công ty Pharmedic";
            // 
            // lblNcc
            // 
            this.lblNcc.AutoSize = true;
            this.lblNcc.Location = new System.Drawing.Point(20, 24);
            this.lblNcc.Name = "lblNcc";
            this.lblNcc.Size = new System.Drawing.Size(92, 17);
            this.lblNcc.TabIndex = 0;
            this.lblNcc.Text = "Nhà cung cấp:";
            // 
            // grpHangTra
            // 
            this.grpHangTra.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.grpHangTra.Controls.Add(this.dgvHangTra);
            this.grpHangTra.Location = new System.Drawing.Point(12, 174);
            this.grpHangTra.Name = "grpHangTra";
            this.grpHangTra.Size = new System.Drawing.Size(1010, 310);
            this.grpHangTra.TabIndex = 2;
            this.grpHangTra.TabStop = false;
            this.grpHangTra.Text = "Chọn số lượng hàng cần trả lại nhà cung cấp";
            // 
            // dgvHangTra
            // 
            this.dgvHangTra.AllowUserToAddRows = false;
            this.dgvHangTra.AllowUserToDeleteRows = false;
            this.dgvHangTra.AllowUserToResizeRows = false;
            this.dgvHangTra.BackgroundColor = System.Drawing.Color.White;
            this.dgvHangTra.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dgvHangTra.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvHangTra.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colStt,
            this.colMaHang,
            this.colMaHangNoiBo,
            this.colTenHang,
            this.colDonViTinh,
            this.colSoLuongNhap,
            this.colTonKho,
            this.colSoLuongTra,
            this.colDonGiaTra,
            this.colThanhTien});
            this.dgvHangTra.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvHangTra.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnEnter;
            this.dgvHangTra.Location = new System.Drawing.Point(3, 21);
            this.dgvHangTra.MultiSelect = false;
            this.dgvHangTra.Name = "dgvHangTra";
            this.dgvHangTra.RowHeadersVisible = false;
            this.dgvHangTra.RowTemplate.Height = 32;
            this.dgvHangTra.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.CellSelect;
            this.dgvHangTra.Size = new System.Drawing.Size(1004, 286);
            this.dgvHangTra.TabIndex = 0;
            this.dgvHangTra.CellFormatting += new System.Windows.Forms.DataGridViewCellFormattingEventHandler(this.dgvHangTra_CellFormatting);
            this.dgvHangTra.CellValidating += new System.Windows.Forms.DataGridViewCellValidatingEventHandler(this.dgvHangTra_CellValidating);
            this.dgvHangTra.CellValueChanged += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvHangTra_CellValueChanged);
            this.dgvHangTra.CellPainting += new System.Windows.Forms.DataGridViewCellPaintingEventHandler(this.dgvHangTra_CellPainting);
            this.dgvHangTra.EditingControlShowing += new System.Windows.Forms.DataGridViewEditingControlShowingEventHandler(this.dgvHangTra_EditingControlShowing);
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
            this.colMaHang.DataPropertyName = "MaHang";
            this.colMaHang.HeaderText = "MaHang";
            this.colMaHang.Name = "colMaHang";
            this.colMaHang.ReadOnly = true;
            this.colMaHang.Visible = false;
            // 
            // colMaHangNoiBo
            // 
            this.colMaHangNoiBo.DataPropertyName = "MaHangNoiBo";
            this.colMaHangNoiBo.HeaderText = "Mã hàng";
            this.colMaHangNoiBo.Name = "colMaHangNoiBo";
            this.colMaHangNoiBo.ReadOnly = true;
            this.colMaHangNoiBo.Width = 110;
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
            this.colDonViTinh.DataPropertyName = "TenDvt";
            this.colDonViTinh.HeaderText = "ĐVT";
            this.colDonViTinh.Name = "colDonViTinh";
            this.colDonViTinh.ReadOnly = true;
            this.colDonViTinh.Width = 70;
            // 
            // colSoLuongNhap
            // 
            this.colSoLuongNhap.DataPropertyName = "SoLuongNhap";
            this.colSoLuongNhap.HeaderText = "SL đã nhập";
            this.colSoLuongNhap.Name = "colSoLuongNhap";
            this.colSoLuongNhap.ReadOnly = true;
            this.colSoLuongNhap.Width = 95;
            // 
            // colTonKho
            // 
            this.colTonKho.DataPropertyName = "TonKho";
            this.colTonKho.HeaderText = "Tồn hiện tại";
            this.colTonKho.Name = "colTonKho";
            this.colTonKho.ReadOnly = true;
            this.colTonKho.Width = 100;
            // 
            // colSoLuongTra
            // 
            this.colSoLuongTra.DataPropertyName = "SoLuongTra";
            this.colSoLuongTra.HeaderText = "SL cần trả [Nhập] (*)";
            this.colSoLuongTra.Name = "colSoLuongTra";
            this.colSoLuongTra.Width = 145;
            // 
            // colDonGiaTra
            // 
            this.colDonGiaTra.DataPropertyName = "DonGiaNhap";
            this.colDonGiaTra.HeaderText = "Giá trả lại";
            this.colDonGiaTra.Name = "colDonGiaTra";
            this.colDonGiaTra.ReadOnly = true;
            this.colDonGiaTra.Width = 115;
            // 
            // colThanhTien
            // 
            this.colThanhTien.DataPropertyName = "ThanhTienTra";
            this.colThanhTien.HeaderText = "Thành tiền";
            this.colThanhTien.Name = "colThanhTien";
            this.colThanhTien.ReadOnly = true;
            this.colThanhTien.Width = 135;
            // 
            // grpTinhToan
            // 
            this.grpTinhToan.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.grpTinhToan.Controls.Add(this.txtTienNccTraLai);
            this.grpTinhToan.Controls.Add(this.lblTienNccTraLai);
            this.grpTinhToan.Controls.Add(this.txtNoConLai);
            this.grpTinhToan.Controls.Add(this.lblNoConLai);
            this.grpTinhToan.Controls.Add(this.txtTruVaoCongNo);
            this.grpTinhToan.Controls.Add(this.lblTruVaoCongNo);
            this.grpTinhToan.Controls.Add(this.txtTongTienTra);
            this.grpTinhToan.Controls.Add(this.lblTongTienTra);
            this.grpTinhToan.Controls.Add(this.lblTongSlTra);
            this.grpTinhToan.Location = new System.Drawing.Point(12, 490);
            this.grpTinhToan.Name = "grpTinhToan";
            this.grpTinhToan.Size = new System.Drawing.Size(1010, 115);
            this.grpTinhToan.TabIndex = 3;
            this.grpTinhToan.TabStop = false;
            this.grpTinhToan.Text = "Tính toán số tiền & công nợ";
            // 
            // txtTienNccTraLai
            // 
            this.txtTienNccTraLai.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.txtTienNccTraLai.Font = new System.Drawing.Font("Segoe UI", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.txtTienNccTraLai.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(22)))), ((int)(((byte)(163)))), ((int)(((byte)(74)))));
            this.txtTienNccTraLai.Location = new System.Drawing.Point(810, 68);
            this.txtTienNccTraLai.Name = "txtTienNccTraLai";
            this.txtTienNccTraLai.ReadOnly = true;
            this.txtTienNccTraLai.Size = new System.Drawing.Size(180, 26);
            this.txtTienNccTraLai.TabIndex = 8;
            this.txtTienNccTraLai.Text = "0";
            this.txtTienNccTraLai.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // lblTienNccTraLai
            // 
            this.lblTienNccTraLai.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblTienNccTraLai.AutoSize = true;
            this.lblTienNccTraLai.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.lblTienNccTraLai.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(22)))), ((int)(((byte)(163)))), ((int)(((byte)(74)))));
            this.lblTienNccTraLai.Location = new System.Drawing.Point(620, 72);
            this.lblTienNccTraLai.Name = "lblTienNccTraLai";
            this.lblTienNccTraLai.Size = new System.Drawing.Size(177, 19);
            this.lblTienNccTraLai.TabIndex = 7;
            this.lblTienNccTraLai.Text = "Tiền NCC cần trả lại (★):";
            // 
            // txtNoConLai
            // 
            this.txtNoConLai.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.txtNoConLai.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.txtNoConLai.ForeColor = System.Drawing.Color.Red;
            this.txtNoConLai.Location = new System.Drawing.Point(810, 26);
            this.txtNoConLai.Name = "txtNoConLai";
            this.txtNoConLai.ReadOnly = true;
            this.txtNoConLai.Size = new System.Drawing.Size(180, 25);
            this.txtNoConLai.TabIndex = 6;
            this.txtNoConLai.Text = "0";
            this.txtNoConLai.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // lblNoConLai
            // 
            this.lblNoConLai.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblNoConLai.AutoSize = true;
            this.lblNoConLai.Location = new System.Drawing.Point(620, 30);
            this.lblNoConLai.Name = "lblNoConLai";
            this.lblNoConLai.Size = new System.Drawing.Size(149, 17);
            this.lblNoConLai.TabIndex = 5;
            this.lblNoConLai.Text = "Nợ đơn còn lại sau trả:";
            // 
            // txtTruVaoCongNo
            // 
            this.txtTruVaoCongNo.Location = new System.Drawing.Point(400, 68);
            this.txtTruVaoCongNo.Name = "txtTruVaoCongNo";
            this.txtTruVaoCongNo.ReadOnly = true;
            this.txtTruVaoCongNo.Size = new System.Drawing.Size(170, 25);
            this.txtTruVaoCongNo.TabIndex = 4;
            this.txtTruVaoCongNo.Text = "0";
            this.txtTruVaoCongNo.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // lblTruVaoCongNo
            // 
            this.lblTruVaoCongNo.AutoSize = true;
            this.lblTruVaoCongNo.Location = new System.Drawing.Point(260, 72);
            this.lblTruVaoCongNo.Name = "lblTruVaoCongNo";
            this.lblTruVaoCongNo.Size = new System.Drawing.Size(130, 17);
            this.lblTruVaoCongNo.TabIndex = 3;
            this.lblTruVaoCongNo.Text = "Trừ vào nợ cần trả:";
            // 
            // txtTongTienTra
            // 
            this.txtTongTienTra.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.txtTongTienTra.Location = new System.Drawing.Point(400, 26);
            this.txtTongTienTra.Name = "txtTongTienTra";
            this.txtTongTienTra.ReadOnly = true;
            this.txtTongTienTra.Size = new System.Drawing.Size(170, 25);
            this.txtTongTienTra.TabIndex = 2;
            this.txtTongTienTra.Text = "0";
            this.txtTongTienTra.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // lblTongTienTra
            // 
            this.lblTongTienTra.AutoSize = true;
            this.lblTongTienTra.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.lblTongTienTra.Location = new System.Drawing.Point(260, 30);
            this.lblTongTienTra.Name = "lblTongTienTra";
            this.lblTongTienTra.Size = new System.Drawing.Size(130, 17);
            this.lblTongTienTra.TabIndex = 1;
            this.lblTongTienTra.Text = "Tổng tiền hàng trả:";
            // 
            // lblTongSlTra
            // 
            this.lblTongSlTra.AutoSize = true;
            this.lblTongSlTra.Location = new System.Drawing.Point(20, 30);
            this.lblTongSlTra.Name = "lblTongSlTra";
            this.lblTongSlTra.Size = new System.Drawing.Size(131, 17);
            this.lblTongSlTra.TabIndex = 0;
            this.lblTongSlTra.Text = "Tổng số lượng trả: 0";
            // 
            // pnlBottom
            // 
            this.pnlBottom.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(250)))), ((int)(((byte)(252)))));
            this.pnlBottom.Controls.Add(this.btnBoQua);
            this.pnlBottom.Controls.Add(this.btnXacNhanTra);
            this.pnlBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnlBottom.Location = new System.Drawing.Point(0, 615);
            this.pnlBottom.Name = "pnlBottom";
            this.pnlBottom.Size = new System.Drawing.Size(1034, 55);
            this.pnlBottom.TabIndex = 4;
            // 
            // btnBoQua
            // 
            this.btnBoQua.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnBoQua.Location = new System.Drawing.Point(748, 11);
            this.btnBoQua.Name = "btnBoQua";
            this.btnBoQua.Size = new System.Drawing.Size(100, 32);
            this.btnBoQua.TabIndex = 1;
            this.btnBoQua.Text = "Bỏ qua";
            this.btnBoQua.UseVisualStyleBackColor = true;
            this.btnBoQua.Click += new System.EventHandler(this.btnBoQua_Click);
            // 
            // btnXacNhanTra
            // 
            this.btnXacNhanTra.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnXacNhanTra.Location = new System.Drawing.Point(860, 11);
            this.btnXacNhanTra.Name = "btnXacNhanTra";
            this.btnXacNhanTra.Size = new System.Drawing.Size(160, 32);
            this.btnXacNhanTra.TabIndex = 0;
            this.btnXacNhanTra.Text = "Xác nhận trả hàng";
            this.btnXacNhanTra.UseVisualStyleBackColor = true;
            this.btnXacNhanTra.Click += new System.EventHandler(this.btnXacNhanTra_Click);
            // 
            // frmTraHangNhap
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(1034, 670);
            this.CancelButton = this.btnBoQua;
            this.Controls.Add(this.pnlBottom);
            this.Controls.Add(this.grpTinhToan);
            this.Controls.Add(this.grpHangTra);
            this.Controls.Add(this.grpThongTin);
            this.Controls.Add(this.pnlTop);
            this.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.MinimumSize = new System.Drawing.Size(950, 600);
            this.Name = "frmTraHangNhap";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Trả hàng nhập";
            this.Load += new System.EventHandler(this.frmTraHangNhap_Load);
            this.pnlTop.ResumeLayout(false);
            this.pnlTop.PerformLayout();
            this.grpThongTin.ResumeLayout(false);
            this.grpThongTin.PerformLayout();
            this.grpHangTra.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvHangTra)).EndInit();
            this.grpTinhToan.ResumeLayout(false);
            this.grpTinhToan.PerformLayout();
            this.pnlBottom.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pnlTop;
        private System.Windows.Forms.Label lblTieuDe;
        private System.Windows.Forms.Label lblMaPhieuGoc;
        private System.Windows.Forms.GroupBox grpThongTin;
        private System.Windows.Forms.Label lblNcc;
        private System.Windows.Forms.Label lblNccVal;
        private System.Windows.Forms.Label lblKho;
        private System.Windows.Forms.Label lblKhoVal;
        private System.Windows.Forms.Label lblNgayTra;
        private System.Windows.Forms.DateTimePicker dtpNgayTra;
        private System.Windows.Forms.Label lblNguoiTra;
        private System.Windows.Forms.ComboBox cboNguoiTra;
        private System.Windows.Forms.Label lblNoCanTraGoc;
        private System.Windows.Forms.Label lblNoCanTraGocVal;
        private System.Windows.Forms.Label lblGhiChu;
        private System.Windows.Forms.TextBox txtGhiChu;
        private System.Windows.Forms.GroupBox grpHangTra;
        private System.Windows.Forms.DataGridView dgvHangTra;
        private System.Windows.Forms.GroupBox grpTinhToan;
        private System.Windows.Forms.Label lblTongSlTra;
        private System.Windows.Forms.Label lblTongTienTra;
        private System.Windows.Forms.TextBox txtTongTienTra;
        private System.Windows.Forms.Label lblTruVaoCongNo;
        private System.Windows.Forms.TextBox txtTruVaoCongNo;
        private System.Windows.Forms.Label lblNoConLai;
        private System.Windows.Forms.TextBox txtNoConLai;
        private System.Windows.Forms.Label lblTienNccTraLai;
        private System.Windows.Forms.TextBox txtTienNccTraLai;
        private System.Windows.Forms.Panel pnlBottom;
        private System.Windows.Forms.Button btnBoQua;
        private System.Windows.Forms.Button btnXacNhanTra;
        private System.Windows.Forms.DataGridViewTextBoxColumn colStt;
        private System.Windows.Forms.DataGridViewTextBoxColumn colMaHang;
        private System.Windows.Forms.DataGridViewTextBoxColumn colMaHangNoiBo;
        private System.Windows.Forms.DataGridViewTextBoxColumn colTenHang;
        private System.Windows.Forms.DataGridViewTextBoxColumn colDonViTinh;
        private System.Windows.Forms.DataGridViewTextBoxColumn colSoLuongNhap;
        private System.Windows.Forms.DataGridViewTextBoxColumn colTonKho;
        private System.Windows.Forms.DataGridViewTextBoxColumn colSoLuongTra;
        private System.Windows.Forms.DataGridViewTextBoxColumn colDonGiaTra;
        private System.Windows.Forms.DataGridViewTextBoxColumn colThanhTien;
    }
}
