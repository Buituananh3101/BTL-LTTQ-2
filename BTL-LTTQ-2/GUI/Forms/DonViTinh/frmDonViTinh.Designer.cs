namespace BTL_LTTQ_2.GUI.Forms.DonViTinh
{
    partial class frmDonViTinh
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
            this.pnlTop = new System.Windows.Forms.Panel();
            this.grpTop = new System.Windows.Forms.GroupBox();
            this.btnHuongDanSuDung = new System.Windows.Forms.Button();
            this.btnHienCot = new System.Windows.Forms.Button();
            this.btnXuatFile = new System.Windows.Forms.Button();
            this.btnThemDonViTinh = new System.Windows.Forms.Button();
            this.lblTimKiem = new System.Windows.Forms.Label();
            this.txtTimKiem = new System.Windows.Forms.TextBox();
            this.lblTieuDe = new System.Windows.Forms.Label();
            this.pnlBoLoc = new System.Windows.Forms.Panel();
            this.grpBoLoc = new System.Windows.Forms.GroupBox();
            this.grpThoiGianTao = new System.Windows.Forms.GroupBox();
            this.dtpDenNgay = new System.Windows.Forms.DateTimePicker();
            this.lblDenNgay = new System.Windows.Forms.Label();
            this.lblTuNgay = new System.Windows.Forms.Label();
            this.dtpTuNgay = new System.Windows.Forms.DateTimePicker();
            this.lblThoiGianTao = new System.Windows.Forms.Label();
            this.cboSapXep = new System.Windows.Forms.ComboBox();
            this.lblSapXep = new System.Windows.Forms.Label();
            this.cboTrangThai = new System.Windows.Forms.ComboBox();
            this.lblTrangThai = new System.Windows.Forms.Label();
            this.splitterBoLoc = new System.Windows.Forms.Splitter();
            this.pnlPhanTrang = new System.Windows.Forms.Panel();
            this.lblTongSoDvt = new System.Windows.Forms.Label();
            this.lblSoDongMoiTrang = new System.Windows.Forms.Label();
            this.cboSoDongMoiTrang = new System.Windows.Forms.ComboBox();
            this.btnTrangTruoc = new System.Windows.Forms.Button();
            this.lblTrangHienTai = new System.Windows.Forms.Label();
            this.btnTrangSau = new System.Windows.Forms.Button();
            this.dgvDonViTinh = new System.Windows.Forms.DataGridView();
            this.colChon = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.colMaDVT = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colTenDVT = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colSoLuongHangHoa = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colTrangThai = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colHangHoaTieuBieu = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.pnlTop.SuspendLayout();
            this.grpTop.SuspendLayout();
            this.pnlBoLoc.SuspendLayout();
            this.grpBoLoc.SuspendLayout();
            this.grpThoiGianTao.SuspendLayout();
            this.pnlPhanTrang.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvDonViTinh)).BeginInit();
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
            this.grpTop.Controls.Add(this.btnHuongDanSuDung);
            this.grpTop.Controls.Add(this.btnHienCot);
            this.grpTop.Controls.Add(this.btnXuatFile);
            this.grpTop.Controls.Add(this.btnThemDonViTinh);
            this.grpTop.Controls.Add(this.lblTimKiem);
            this.grpTop.Controls.Add(this.txtTimKiem);
            this.grpTop.Controls.Add(this.lblTieuDe);
            this.grpTop.Location = new System.Drawing.Point(0, 3);
            this.grpTop.Name = "grpTop";
            this.grpTop.Size = new System.Drawing.Size(1782, 60);
            this.grpTop.TabIndex = 0;
            this.grpTop.TabStop = false;
            // 
            // btnHuongDanSuDung
            // 
            this.btnHuongDanSuDung.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnHuongDanSuDung.Location = new System.Drawing.Point(1715, 20);
            this.btnHuongDanSuDung.Name = "btnHuongDanSuDung";
            this.btnHuongDanSuDung.Size = new System.Drawing.Size(43, 29);
            this.btnHuongDanSuDung.TabIndex = 6;
            this.btnHuongDanSuDung.Text = "?";
            this.btnHuongDanSuDung.UseVisualStyleBackColor = true;
            this.btnHuongDanSuDung.Click += new System.EventHandler(this.btnHuongDanSuDung_Click);
            // 
            // btnHienCot
            // 
            this.btnHienCot.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnHienCot.Location = new System.Drawing.Point(1605, 20);
            this.btnHienCot.Name = "btnHienCot";
            this.btnHienCot.Size = new System.Drawing.Size(95, 29);
            this.btnHienCot.TabIndex = 5;
            this.btnHienCot.Text = "Hiện cột";
            this.btnHienCot.UseVisualStyleBackColor = true;
            this.btnHienCot.Click += new System.EventHandler(this.btnHienCot_Click);
            // 
            // btnXuatFile
            // 
            this.btnXuatFile.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnXuatFile.Location = new System.Drawing.Point(1480, 20);
            this.btnXuatFile.Name = "btnXuatFile";
            this.btnXuatFile.Size = new System.Drawing.Size(110, 29);
            this.btnXuatFile.TabIndex = 4;
            this.btnXuatFile.Text = "Xuất file";
            this.btnXuatFile.UseVisualStyleBackColor = true;
            this.btnXuatFile.Click += new System.EventHandler(this.btnXuatFile_Click);
            // 
            // btnThemDonViTinh
            // 
            this.btnThemDonViTinh.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnThemDonViTinh.Location = new System.Drawing.Point(1340, 20);
            this.btnThemDonViTinh.Name = "btnThemDonViTinh";
            this.btnThemDonViTinh.Size = new System.Drawing.Size(125, 29);
            this.btnThemDonViTinh.TabIndex = 3;
            this.btnThemDonViTinh.Text = "+ Đơn vị tính";
            this.btnThemDonViTinh.UseVisualStyleBackColor = true;
            this.btnThemDonViTinh.Click += new System.EventHandler(this.btnThemDonViTinh_Click);
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
            // lblTieuDe
            // 
            this.lblTieuDe.AutoSize = true;
            this.lblTieuDe.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.lblTieuDe.Location = new System.Drawing.Point(24, 23);
            this.lblTieuDe.Name = "lblTieuDe";
            this.lblTieuDe.Size = new System.Drawing.Size(95, 21);
            this.lblTieuDe.TabIndex = 0;
            this.lblTieuDe.Text = "Đơn vị tính";
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
            this.grpBoLoc.Controls.Add(this.grpThoiGianTao);
            this.grpBoLoc.Controls.Add(this.lblThoiGianTao);
            this.grpBoLoc.Controls.Add(this.cboSapXep);
            this.grpBoLoc.Controls.Add(this.lblSapXep);
            this.grpBoLoc.Controls.Add(this.cboTrangThai);
            this.grpBoLoc.Controls.Add(this.lblTrangThai);

            this.grpBoLoc.Location = new System.Drawing.Point(12, 9);
            this.grpBoLoc.Name = "grpBoLoc";
            this.grpBoLoc.Size = new System.Drawing.Size(360, 717);
            this.grpBoLoc.TabIndex = 1;
            this.grpBoLoc.TabStop = false;
            // 
            // grpThoiGianTao
            // 
            this.grpThoiGianTao.Controls.Add(this.dtpDenNgay);
            this.grpThoiGianTao.Controls.Add(this.lblDenNgay);
            this.grpThoiGianTao.Controls.Add(this.lblTuNgay);
            this.grpThoiGianTao.Controls.Add(this.dtpTuNgay);
            this.grpThoiGianTao.Location = new System.Drawing.Point(18, 396);
            this.grpThoiGianTao.Name = "grpThoiGianTao";
            this.grpThoiGianTao.Size = new System.Drawing.Size(322, 108);
            this.grpThoiGianTao.TabIndex = 5;
            this.grpThoiGianTao.TabStop = false;
            // 
            // dtpDenNgay
            // 
            this.dtpDenNgay.CustomFormat = "dd/MM/yyyy";
            this.dtpDenNgay.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpDenNgay.Location = new System.Drawing.Point(92, 65);
            this.dtpDenNgay.Name = "dtpDenNgay";
            this.dtpDenNgay.Size = new System.Drawing.Size(220, 24);
            this.dtpDenNgay.TabIndex = 3;
            this.dtpDenNgay.ValueChanged += new System.EventHandler(this.BoLoc_SelectedIndexChanged);
            // 
            // lblDenNgay
            // 
            this.lblDenNgay.AutoSize = true;
            this.lblDenNgay.Location = new System.Drawing.Point(10, 68);
            this.lblDenNgay.Name = "lblDenNgay";
            this.lblDenNgay.Size = new System.Drawing.Size(63, 17);
            this.lblDenNgay.TabIndex = 2;
            this.lblDenNgay.Text = "Đến ngày";
            // 
            // lblTuNgay
            // 
            this.lblTuNgay.AutoSize = true;
            this.lblTuNgay.Location = new System.Drawing.Point(10, 29);
            this.lblTuNgay.Name = "lblTuNgay";
            this.lblTuNgay.Size = new System.Drawing.Size(55, 17);
            this.lblTuNgay.TabIndex = 0;
            this.lblTuNgay.Text = "Từ ngày";
            // 
            // dtpTuNgay
            // 
            this.dtpTuNgay.CustomFormat = "dd/MM/yyyy";
            this.dtpTuNgay.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpTuNgay.Location = new System.Drawing.Point(92, 26);
            this.dtpTuNgay.Name = "dtpTuNgay";
            this.dtpTuNgay.Size = new System.Drawing.Size(220, 24);
            this.dtpTuNgay.TabIndex = 1;
            this.dtpTuNgay.ValueChanged += new System.EventHandler(this.BoLoc_SelectedIndexChanged);
            // 
            // lblThoiGianTao
            // 
            this.lblThoiGianTao.AutoSize = true;
            this.lblThoiGianTao.Location = new System.Drawing.Point(14, 360);
            this.lblThoiGianTao.Name = "lblThoiGianTao";
            this.lblThoiGianTao.Size = new System.Drawing.Size(85, 17);
            this.lblThoiGianTao.TabIndex = 4;
            this.lblThoiGianTao.Text = "Thời gian tạo";
            // 
            // cboSapXep
            // 
            this.cboSapXep.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboSapXep.FormattingEnabled = true;
            this.cboSapXep.Location = new System.Drawing.Point(18, 244);
            this.cboSapXep.Name = "cboSapXep";
            this.cboSapXep.Size = new System.Drawing.Size(322, 25);
            this.cboSapXep.TabIndex = 3;
            this.cboSapXep.SelectedIndexChanged += new System.EventHandler(this.BoLoc_SelectedIndexChanged);
            // 
            // lblSapXep
            // 
            this.lblSapXep.AutoSize = true;
            this.lblSapXep.Location = new System.Drawing.Point(14, 210);
            this.lblSapXep.Name = "lblSapXep";
            this.lblSapXep.Size = new System.Drawing.Size(81, 17);
            this.lblSapXep.TabIndex = 2;
            this.lblSapXep.Text = "Sắp xếp theo";
            // 
            // cboTrangThai
            // 
            this.cboTrangThai.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboTrangThai.FormattingEnabled = true;
            this.cboTrangThai.Location = new System.Drawing.Point(18, 94);
            this.cboTrangThai.Name = "cboTrangThai";
            this.cboTrangThai.Size = new System.Drawing.Size(322, 25);
            this.cboTrangThai.TabIndex = 1;
            this.cboTrangThai.SelectedIndexChanged += new System.EventHandler(this.BoLoc_SelectedIndexChanged);
            // 
            // lblTrangThai
            // 
            this.lblTrangThai.AutoSize = true;
            this.lblTrangThai.Location = new System.Drawing.Point(14, 60);
            this.lblTrangThai.Name = "lblTrangThai";
            this.lblTrangThai.Size = new System.Drawing.Size(117, 17);
            this.lblTrangThai.TabIndex = 0;
            this.lblTrangThai.Text = "Trạng thái sử dụng";
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
            this.pnlPhanTrang.Controls.Add(this.lblTongSoDvt);
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
            // lblTongSoDvt
            // 
            this.lblTongSoDvt.AutoSize = true;
            this.lblTongSoDvt.Location = new System.Drawing.Point(15, 17);
            this.lblTongSoDvt.Name = "lblTongSoDvt";
            this.lblTongSoDvt.Size = new System.Drawing.Size(229, 17);
            this.lblTongSoDvt.TabIndex = 0;
            this.lblTongSoDvt.Text = "Hiển thị 1 - 20 trên tổng số 0 bản ghi";
            // 
            // lblSoDongMoiTrang
            // 
            this.lblSoDongMoiTrang.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblSoDongMoiTrang.AutoSize = true;
            this.lblSoDongMoiTrang.Location = new System.Drawing.Point(1005, 17);
            this.lblSoDongMoiTrang.Name = "lblSoDongMoiTrang";
            this.lblSoDongMoiTrang.Size = new System.Drawing.Size(107, 17);
            this.lblSoDongMoiTrang.TabIndex = 1;
            this.lblSoDongMoiTrang.Text = "Số dòng / trang:";
            // 
            // cboSoDongMoiTrang
            // 
            this.cboSoDongMoiTrang.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.cboSoDongMoiTrang.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboSoDongMoiTrang.FormattingEnabled = true;
            this.cboSoDongMoiTrang.Location = new System.Drawing.Point(1118, 14);
            this.cboSoDongMoiTrang.Name = "cboSoDongMoiTrang";
            this.cboSoDongMoiTrang.Size = new System.Drawing.Size(70, 25);
            this.cboSoDongMoiTrang.TabIndex = 2;
            this.cboSoDongMoiTrang.SelectedIndexChanged += new System.EventHandler(this.cboSoDongMoiTrang_SelectedIndexChanged);
            // 
            // btnTrangTruoc
            // 
            this.btnTrangTruoc.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnTrangTruoc.Location = new System.Drawing.Point(1208, 12);
            this.btnTrangTruoc.Name = "btnTrangTruoc";
            this.btnTrangTruoc.Size = new System.Drawing.Size(80, 29);
            this.btnTrangTruoc.TabIndex = 3;
            this.btnTrangTruoc.Text = "◀ Trước";
            this.btnTrangTruoc.UseVisualStyleBackColor = true;
            this.btnTrangTruoc.Click += new System.EventHandler(this.btnTrangTruoc_Click);
            // 
            // lblTrangHienTai
            // 
            this.lblTrangHienTai.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblTrangHienTai.AutoSize = true;
            this.lblTrangHienTai.Location = new System.Drawing.Point(1296, 17);
            this.lblTrangHienTai.Name = "lblTrangHienTai";
            this.lblTrangHienTai.Size = new System.Drawing.Size(35, 17);
            this.lblTrangHienTai.TabIndex = 4;
            this.lblTrangHienTai.Text = "1 / 1";
            // 
            // btnTrangSau
            // 
            this.btnTrangSau.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnTrangSau.Location = new System.Drawing.Point(1339, 12);
            this.btnTrangSau.Name = "btnTrangSau";
            this.btnTrangSau.Size = new System.Drawing.Size(80, 29);
            this.btnTrangSau.TabIndex = 5;
            this.btnTrangSau.Text = "Sau ▶";
            this.btnTrangSau.UseVisualStyleBackColor = true;
            this.btnTrangSau.Click += new System.EventHandler(this.btnTrangSau_Click);
            // 
            // dgvDonViTinh
            // 
            this.dgvDonViTinh.AllowUserToAddRows = false;
            this.dgvDonViTinh.AllowUserToDeleteRows = false;
            this.dgvDonViTinh.AllowUserToResizeRows = false;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(250)))), ((int)(((byte)(252)))));
            this.dgvDonViTinh.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.dgvDonViTinh.BackgroundColor = System.Drawing.Color.White;
            this.dgvDonViTinh.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dgvDonViTinh.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.SingleHorizontal;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(241)))), ((int)(((byte)(245)))), ((int)(((byte)(249)))));
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvDonViTinh.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.dgvDonViTinh.ColumnHeadersHeight = 36;
            this.dgvDonViTinh.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.dgvDonViTinh.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colChon,
            this.colMaDVT,
            this.colTenDVT,
            this.colSoLuongHangHoa,
            this.colTrangThai,
            this.colHangHoaTieuBieu});
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(231)))), ((int)(((byte)(255)))));
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(41)))), ((int)(((byte)(59)))));
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvDonViTinh.DefaultCellStyle = dataGridViewCellStyle3;
            this.dgvDonViTinh.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvDonViTinh.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(232)))), ((int)(((byte)(240)))));
            this.dgvDonViTinh.Location = new System.Drawing.Point(387, 84);
            this.dgvDonViTinh.MultiSelect = false;
            this.dgvDonViTinh.Name = "dgvDonViTinh";
            this.dgvDonViTinh.RowHeadersVisible = false;
            this.dgvDonViTinh.RowHeadersWidth = 51;
            this.dgvDonViTinh.RowTemplate.Height = 32;
            this.dgvDonViTinh.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvDonViTinh.Size = new System.Drawing.Size(1395, 727);
            this.dgvDonViTinh.TabIndex = 3;
            this.dgvDonViTinh.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvDonViTinh_CellDoubleClick);
            this.dgvDonViTinh.CellFormatting += new System.Windows.Forms.DataGridViewCellFormattingEventHandler(this.dgvDonViTinh_CellFormatting);
            this.dgvDonViTinh.CellPainting += new System.Windows.Forms.DataGridViewCellPaintingEventHandler(this.dgvDonViTinh_CellPainting);
            this.dgvDonViTinh.CellValueChanged += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvDonViTinh_CellValueChanged);
            this.dgvDonViTinh.ColumnHeaderMouseClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.dgvDonViTinh_ColumnHeaderMouseClick);
            this.dgvDonViTinh.CurrentCellDirtyStateChanged += new System.EventHandler(this.dgvDonViTinh_CurrentCellDirtyStateChanged);
            // 
            // colChon
            // 
            this.colChon.DataPropertyName = "Chon";
            this.colChon.FalseValue = "false";
            this.colChon.Frozen = true;
            this.colChon.HeaderText = "";
            this.colChon.MinimumWidth = 45;
            this.colChon.Name = "colChon";
            this.colChon.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.colChon.TrueValue = "true";
            this.colChon.Width = 45;
            // 
            // colMaDVT
            // 
            this.colMaDVT.DataPropertyName = "MaHienThi";
            this.colMaDVT.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
 this.colMaDVT.FillWeight = 120F;
 this.colMaDVT.HeaderText = "Mã ĐVT";
            this.colMaDVT.MinimumWidth = 110;
            this.colMaDVT.Name = "colMaDVT";
            this.colMaDVT.ReadOnly = true;
            this.colMaDVT.Width = 120;
            // 
            // colTenDVT
            // 
            this.colTenDVT.DataPropertyName = "TenDVT";
            this.colTenDVT.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
 this.colTenDVT.FillWeight = 180F;
 this.colTenDVT.HeaderText = "Tên đơn vị tính";
            this.colTenDVT.MinimumWidth = 160;
            this.colTenDVT.Name = "colTenDVT";
            this.colTenDVT.ReadOnly = true;
            this.colTenDVT.Width = 180;
            // 
            // colSoLuongHangHoa
            // 
            this.colSoLuongHangHoa.DataPropertyName = "SoLuongHangHoa";
            this.colSoLuongHangHoa.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
 this.colSoLuongHangHoa.FillWeight = 160F;
 this.colSoLuongHangHoa.HeaderText = "Số lượng hàng hóa";
            this.colSoLuongHangHoa.MinimumWidth = 140;
            this.colSoLuongHangHoa.Name = "colSoLuongHangHoa";
            this.colSoLuongHangHoa.ReadOnly = true;
            this.colSoLuongHangHoa.Width = 160;
            // 
            // colTrangThai
            // 
            this.colTrangThai.DataPropertyName = "TrangThaiSuDung";
            this.colTrangThai.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
 this.colTrangThai.FillWeight = 160F;
 this.colTrangThai.HeaderText = "Trạng thái sử dụng";
            this.colTrangThai.MinimumWidth = 140;
            this.colTrangThai.Name = "colTrangThai";
            this.colTrangThai.ReadOnly = true;
            this.colTrangThai.Width = 160;
            // 
            // colHangHoaTieuBieu
            // 
            this.colHangHoaTieuBieu.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.colHangHoaTieuBieu.DataPropertyName = "HangHoaTieuBieu";
            this.colHangHoaTieuBieu.FillWeight = 250F;
 this.colHangHoaTieuBieu.HeaderText = "Mặt hàng tiêu biểu";
            this.colHangHoaTieuBieu.MinimumWidth = 250;
            this.colHangHoaTieuBieu.Name = "colHangHoaTieuBieu";
            this.colHangHoaTieuBieu.ReadOnly = true;
            // 
            // frmDonViTinh
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1782, 853);
            this.Controls.Add(this.dgvDonViTinh);
            this.Controls.Add(this.pnlPhanTrang);

            this.Controls.Add(this.pnlBoLoc);
            this.Controls.Add(this.pnlTop);
            this.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "frmDonViTinh";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Danh Sách Đơn Vị Tính";
            this.Load += new System.EventHandler(this.frmDonViTinh_Load);
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
            ((System.ComponentModel.ISupportInitialize)(this.dgvDonViTinh)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pnlTop;
        private System.Windows.Forms.GroupBox grpTop;
        private System.Windows.Forms.Label lblTieuDe;
        private System.Windows.Forms.Label lblTimKiem;
        private System.Windows.Forms.TextBox txtTimKiem;
        private System.Windows.Forms.Button btnThemDonViTinh;
        private System.Windows.Forms.Button btnXuatFile;
        private System.Windows.Forms.Button btnHienCot;
        private System.Windows.Forms.Button btnHuongDanSuDung;
        private System.Windows.Forms.Panel pnlBoLoc;
        private System.Windows.Forms.GroupBox grpBoLoc;
        private System.Windows.Forms.Label lblTrangThai;
        private System.Windows.Forms.ComboBox cboTrangThai;
        private System.Windows.Forms.Label lblSapXep;
        private System.Windows.Forms.ComboBox cboSapXep;
        private System.Windows.Forms.Label lblThoiGianTao;
        private System.Windows.Forms.GroupBox grpThoiGianTao;
        private System.Windows.Forms.DateTimePicker dtpTuNgay;
        private System.Windows.Forms.Label lblTuNgay;
        private System.Windows.Forms.DateTimePicker dtpDenNgay;
        private System.Windows.Forms.Label lblDenNgay;
        private System.Windows.Forms.Splitter splitterBoLoc;
        private System.Windows.Forms.Panel pnlPhanTrang;
        private System.Windows.Forms.Label lblTongSoDvt;
        private System.Windows.Forms.Label lblSoDongMoiTrang;
        private System.Windows.Forms.ComboBox cboSoDongMoiTrang;
        private System.Windows.Forms.Button btnTrangTruoc;
        private System.Windows.Forms.Label lblTrangHienTai;
        private System.Windows.Forms.Button btnTrangSau;
        private System.Windows.Forms.DataGridView dgvDonViTinh;
        private System.Windows.Forms.DataGridViewCheckBoxColumn colChon;
        private System.Windows.Forms.DataGridViewTextBoxColumn colMaDVT;
        private System.Windows.Forms.DataGridViewTextBoxColumn colTenDVT;
        private System.Windows.Forms.DataGridViewTextBoxColumn colSoLuongHangHoa;
        private System.Windows.Forms.DataGridViewTextBoxColumn colTrangThai;
        private System.Windows.Forms.DataGridViewTextBoxColumn colHangHoaTieuBieu;
    }
}
