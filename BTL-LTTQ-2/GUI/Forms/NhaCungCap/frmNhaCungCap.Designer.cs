namespace BTL_LTTQ_2.GUI.Forms.NhaCungCap
{
    partial class frmNhaCungCap
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
            this.btnNhapFile = new System.Windows.Forms.Button();
            this.btnThemNhaCungCap = new System.Windows.Forms.Button();
            this.lblTimKiem = new System.Windows.Forms.Label();
            this.txtTimKiem = new System.Windows.Forms.TextBox();
            this.pnlBoLoc = new System.Windows.Forms.Panel();
            this.grpBoLoc = new System.Windows.Forms.GroupBox();
            this.grpThoiGianTao = new System.Windows.Forms.GroupBox();
            this.dtpTuNgay = new System.Windows.Forms.DateTimePicker();
            this.dtpDenNgay = new System.Windows.Forms.DateTimePicker();
            this.lblTuNgay = new System.Windows.Forms.Label();
            this.lblDenNgay = new System.Windows.Forms.Label();
            this.lblThoiGianTao = new System.Windows.Forms.Label();
            this.cboTrangThai = new System.Windows.Forms.ComboBox();
            this.lblTrangThai = new System.Windows.Forms.Label();
            this.cboNoHienTai = new System.Windows.Forms.ComboBox();
            this.lblNoHienTai = new System.Windows.Forms.Label();
            this.cboMucTongMua = new System.Windows.Forms.ComboBox();
            this.lblMucTongMua = new System.Windows.Forms.Label();
            this.btnThemNhomNCC = new System.Windows.Forms.Button();
            this.cboNhomNCC = new System.Windows.Forms.ComboBox();
            this.lblNhomNCC = new System.Windows.Forms.Label();
            this.splitterBoLoc = new System.Windows.Forms.Splitter();
            this.pnlPhanTrang = new System.Windows.Forms.Panel();
            this.lblTongSoNCC = new System.Windows.Forms.Label();
            this.lblSoDongMoiTrang = new System.Windows.Forms.Label();
            this.cboSoDongMoiTrang = new System.Windows.Forms.ComboBox();
            this.btnTrangTruoc = new System.Windows.Forms.Button();
            this.lblTrangHienTai = new System.Windows.Forms.Label();
            this.btnTrangSau = new System.Windows.Forms.Button();
            this.dgvNhaCungCap = new System.Windows.Forms.DataGridView();
            this.pnlTop.SuspendLayout();
            this.grpTop.SuspendLayout();
            this.pnlBoLoc.SuspendLayout();
            this.grpBoLoc.SuspendLayout();
            this.grpThoiGianTao.SuspendLayout();
            this.pnlPhanTrang.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvNhaCungCap)).BeginInit();
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
            this.grpTop.Controls.Add(this.btnNhapFile);
            this.grpTop.Controls.Add(this.btnThemNhaCungCap);
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
            this.lblTieuDe.Location = new System.Drawing.Point(24, 25);
            this.lblTieuDe.Name = "lblTieuDe";
            this.lblTieuDe.Size = new System.Drawing.Size(115, 21);
            this.lblTieuDe.TabIndex = 6;
            this.lblTieuDe.Text = "Nhà cung cấp";
            // 
            // btnHuongDanSuDung
            // 
            this.btnHuongDanSuDung.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnHuongDanSuDung.Location = new System.Drawing.Point(1711, 21);
            this.btnHuongDanSuDung.Name = "btnHuongDanSuDung";
            this.btnHuongDanSuDung.Size = new System.Drawing.Size(43, 29);
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
            this.btnHienCot.Size = new System.Drawing.Size(95, 29);
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
            this.btnXuatFile.Size = new System.Drawing.Size(110, 29);
            this.btnXuatFile.TabIndex = 3;
            this.btnXuatFile.Text = "Xuất file";
            this.btnXuatFile.UseVisualStyleBackColor = true;
            this.btnXuatFile.Click += new System.EventHandler(this.btnXuatFile_Click);
            // 
            // btnNhapFile
            // 
            this.btnNhapFile.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnNhapFile.Location = new System.Drawing.Point(1348, 21);
            this.btnNhapFile.Name = "btnNhapFile";
            this.btnNhapFile.Size = new System.Drawing.Size(110, 29);
            this.btnNhapFile.TabIndex = 2;
            this.btnNhapFile.Text = "Nhập file";
            this.btnNhapFile.UseVisualStyleBackColor = true;
            this.btnNhapFile.Click += new System.EventHandler(this.btnNhapFile_Click);
            // 
            // btnThemNhaCungCap
            // 
            this.btnThemNhaCungCap.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnThemNhaCungCap.Location = new System.Drawing.Point(1212, 21);
            this.btnThemNhaCungCap.Name = "btnThemNhaCungCap";
            this.btnThemNhaCungCap.Size = new System.Drawing.Size(120, 29);
            this.btnThemNhaCungCap.TabIndex = 1;
            this.btnThemNhaCungCap.Text = "+ Nhà cung cấp";
            this.btnThemNhaCungCap.UseVisualStyleBackColor = true;
            this.btnThemNhaCungCap.Click += new System.EventHandler(this.btnThemNhaCungCap_Click);
            // 
            // lblTimKiem
            // 
            this.lblTimKiem.AutoSize = true;
            this.lblTimKiem.Location = new System.Drawing.Point(385, 26);
            this.lblTimKiem.Name = "lblTimKiem";
            this.lblTimKiem.Size = new System.Drawing.Size(60, 17);
            this.lblTimKiem.TabIndex = 1;
            this.lblTimKiem.Text = "Tìm kiếm";
            // 
            // txtTimKiem
            // 
            this.txtTimKiem.Location = new System.Drawing.Point(465, 22);
            this.txtTimKiem.Name = "txtTimKiem";
            this.txtTimKiem.Size = new System.Drawing.Size(540, 24);
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
            this.grpBoLoc.Controls.Add(this.grpThoiGianTao);
            this.grpBoLoc.Controls.Add(this.lblThoiGianTao);
            this.grpBoLoc.Controls.Add(this.cboTrangThai);
            this.grpBoLoc.Controls.Add(this.lblTrangThai);
            this.grpBoLoc.Controls.Add(this.cboNoHienTai);
            this.grpBoLoc.Controls.Add(this.lblNoHienTai);
            this.grpBoLoc.Controls.Add(this.cboMucTongMua);
            this.grpBoLoc.Controls.Add(this.lblMucTongMua);
            this.grpBoLoc.Controls.Add(this.btnThemNhomNCC);
            this.grpBoLoc.Controls.Add(this.cboNhomNCC);
            this.grpBoLoc.Controls.Add(this.lblNhomNCC);
            this.grpBoLoc.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.grpBoLoc.Location = new System.Drawing.Point(12, 9);
            this.grpBoLoc.Name = "grpBoLoc";
            this.grpBoLoc.Size = new System.Drawing.Size(360, 717);
            this.grpBoLoc.TabIndex = 1;
            this.grpBoLoc.TabStop = false;
            // 
            // grpThoiGianTao
            // 
            this.grpThoiGianTao.Controls.Add(this.dtpTuNgay);
            this.grpThoiGianTao.Controls.Add(this.dtpDenNgay);
            this.grpThoiGianTao.Controls.Add(this.lblTuNgay);
            this.grpThoiGianTao.Controls.Add(this.lblDenNgay);
            this.grpThoiGianTao.Location = new System.Drawing.Point(18, 404);
            this.grpThoiGianTao.Name = "grpThoiGianTao";
            this.grpThoiGianTao.Size = new System.Drawing.Size(322, 108);
            this.grpThoiGianTao.TabIndex = 9;
            this.grpThoiGianTao.TabStop = false;
            // 
            // dtpTuNgay
            // 
            this.dtpTuNgay.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpTuNgay.Location = new System.Drawing.Point(92, 23);
            this.dtpTuNgay.Name = "dtpTuNgay";
            this.dtpTuNgay.Size = new System.Drawing.Size(227, 24);
            this.dtpTuNgay.TabIndex = 10;
            this.dtpTuNgay.ValueChanged += new System.EventHandler(this.BoLoc_SelectedIndexChanged);
            // 
            // dtpDenNgay
            // 
            this.dtpDenNgay.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpDenNgay.Location = new System.Drawing.Point(92, 63);
            this.dtpDenNgay.Name = "dtpDenNgay";
            this.dtpDenNgay.Size = new System.Drawing.Size(227, 24);
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
            this.lblThoiGianTao.Location = new System.Drawing.Point(14, 370);
            this.lblThoiGianTao.Name = "lblThoiGianTao";
            this.lblThoiGianTao.Size = new System.Drawing.Size(85, 17);
            this.lblThoiGianTao.TabIndex = 8;
            this.lblThoiGianTao.Text = "Thời gian tạo";
            // 
            // cboTrangThai
            // 
            this.cboTrangThai.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboTrangThai.FormattingEnabled = true;
            this.cboTrangThai.Location = new System.Drawing.Point(18, 318);
            this.cboTrangThai.Name = "cboTrangThai";
            this.cboTrangThai.Size = new System.Drawing.Size(322, 25);
            this.cboTrangThai.TabIndex = 7;
            this.cboTrangThai.SelectedIndexChanged += new System.EventHandler(this.BoLoc_SelectedIndexChanged);
            // 
            // lblTrangThai
            // 
            this.lblTrangThai.AutoSize = true;
            this.lblTrangThai.Location = new System.Drawing.Point(14, 284);
            this.lblTrangThai.Name = "lblTrangThai";
            this.lblTrangThai.Size = new System.Drawing.Size(66, 17);
            this.lblTrangThai.TabIndex = 6;
            this.lblTrangThai.Text = "Trạng thái";
            // 
            // cboNoHienTai
            // 
            this.cboNoHienTai.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboNoHienTai.FormattingEnabled = true;
            this.cboNoHienTai.Location = new System.Drawing.Point(18, 234);
            this.cboNoHienTai.Name = "cboNoHienTai";
            this.cboNoHienTai.Size = new System.Drawing.Size(322, 25);
            this.cboNoHienTai.TabIndex = 5;
            this.cboNoHienTai.SelectedIndexChanged += new System.EventHandler(this.BoLoc_SelectedIndexChanged);
            // 
            // lblNoHienTai
            // 
            this.lblNoHienTai.AutoSize = true;
            this.lblNoHienTai.Location = new System.Drawing.Point(14, 200);
            this.lblNoHienTai.Name = "lblNoHienTai";
            this.lblNoHienTai.Size = new System.Drawing.Size(72, 17);
            this.lblNoHienTai.TabIndex = 4;
            this.lblNoHienTai.Text = "Nợ hiện tại";
            // 
            // cboMucTongMua
            // 
            this.cboMucTongMua.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboMucTongMua.FormattingEnabled = true;
            this.cboMucTongMua.Location = new System.Drawing.Point(18, 150);
            this.cboMucTongMua.Name = "cboMucTongMua";
            this.cboMucTongMua.Size = new System.Drawing.Size(322, 25);
            this.cboMucTongMua.TabIndex = 3;
            this.cboMucTongMua.SelectedIndexChanged += new System.EventHandler(this.BoLoc_SelectedIndexChanged);
            // 
            // lblMucTongMua
            // 
            this.lblMucTongMua.AutoSize = true;
            this.lblMucTongMua.Location = new System.Drawing.Point(14, 116);
            this.lblMucTongMua.Name = "lblMucTongMua";
            this.lblMucTongMua.Size = new System.Drawing.Size(67, 17);
            this.lblMucTongMua.TabIndex = 2;
            this.lblMucTongMua.Text = "Tổng mua";
            // 
            // btnThemNhomNCC
            // 
            this.btnThemNhomNCC.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnThemNhomNCC.FlatAppearance.BorderColor = System.Drawing.Color.Silver;
            this.btnThemNhomNCC.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnThemNhomNCC.Font = new System.Drawing.Font("Segoe UI", 10.5F, System.Drawing.FontStyle.Bold);
            this.btnThemNhomNCC.Location = new System.Drawing.Point(311, 66);
            this.btnThemNhomNCC.Margin = new System.Windows.Forms.Padding(0);
            this.btnThemNhomNCC.Name = "btnThemNhomNCC";
            this.btnThemNhomNCC.Size = new System.Drawing.Size(25, 25);
            this.btnThemNhomNCC.TabIndex = 2;
            this.btnThemNhomNCC.Text = "+";
            this.btnThemNhomNCC.UseVisualStyleBackColor = true;
            this.btnThemNhomNCC.Click += new System.EventHandler(this.btnThemNhomNCC_Click);
            // 
            // cboNhomNCC
            // 
            this.cboNhomNCC.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboNhomNCC.FormattingEnabled = true;
            this.cboNhomNCC.Location = new System.Drawing.Point(18, 66);
            this.cboNhomNCC.Name = "cboNhomNCC";
            this.cboNhomNCC.Size = new System.Drawing.Size(287, 25);
            this.cboNhomNCC.TabIndex = 1;
            this.cboNhomNCC.SelectedIndexChanged += new System.EventHandler(this.BoLoc_SelectedIndexChanged);
            // 
            // lblNhomNCC
            // 
            this.lblNhomNCC.AutoSize = true;
            this.lblNhomNCC.Location = new System.Drawing.Point(14, 32);
            this.lblNhomNCC.Name = "lblNhomNCC";
            this.lblNhomNCC.Size = new System.Drawing.Size(126, 17);
            this.lblNhomNCC.TabIndex = 0;
            this.lblNhomNCC.Text = "Nhóm nhà cung cấp";
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
            this.pnlPhanTrang.Controls.Add(this.lblTongSoNCC);
            this.pnlPhanTrang.Controls.Add(this.lblSoDongMoiTrang);
            this.pnlPhanTrang.Controls.Add(this.cboSoDongMoiTrang);
            this.pnlPhanTrang.Controls.Add(this.btnTrangTruoc);
            this.pnlPhanTrang.Controls.Add(this.lblTrangHienTai);
            this.pnlPhanTrang.Controls.Add(this.btnTrangSau);
            this.pnlPhanTrang.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnlPhanTrang.Location = new System.Drawing.Point(387, 811);
            this.pnlPhanTrang.Name = "pnlPhanTrang";
            this.pnlPhanTrang.Size = new System.Drawing.Size(1395, 42);
            this.pnlPhanTrang.TabIndex = 3;
            // 
            // lblTongSoNCC
            // 
            this.lblTongSoNCC.AutoSize = true;
            this.lblTongSoNCC.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold);
            this.lblTongSoNCC.Location = new System.Drawing.Point(15, 11);
            this.lblTongSoNCC.Name = "lblTongSoNCC";
            this.lblTongSoNCC.Size = new System.Drawing.Size(159, 17);
            this.lblTongSoNCC.TabIndex = 0;
            this.lblTongSoNCC.Text = "Tổng số: 0 nhà cung cấp";
            // 
            // lblSoDongMoiTrang
            // 
            this.lblSoDongMoiTrang.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblSoDongMoiTrang.AutoSize = true;
            this.lblSoDongMoiTrang.Location = new System.Drawing.Point(951, 12);
            this.lblSoDongMoiTrang.Name = "lblSoDongMoiTrang";
            this.lblSoDongMoiTrang.Size = new System.Drawing.Size(55, 17);
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
            this.cboSoDongMoiTrang.Location = new System.Drawing.Point(1035, 7);
            this.cboSoDongMoiTrang.Name = "cboSoDongMoiTrang";
            this.cboSoDongMoiTrang.Size = new System.Drawing.Size(70, 25);
            this.cboSoDongMoiTrang.TabIndex = 2;
            this.cboSoDongMoiTrang.SelectedIndexChanged += new System.EventHandler(this.cboSoDongMoiTrang_SelectedIndexChanged);
            // 
            // btnTrangTruoc
            // 
            this.btnTrangTruoc.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnTrangTruoc.Location = new System.Drawing.Point(1118, 6);
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
            this.lblTrangHienTai.Location = new System.Drawing.Point(1211, 11);
            this.lblTrangHienTai.Name = "lblTrangHienTai";
            this.lblTrangHienTai.Size = new System.Drawing.Size(72, 17);
            this.lblTrangHienTai.TabIndex = 4;
            this.lblTrangHienTai.Text = "Trang 1 / 1";
            // 
            // btnTrangSau
            // 
            this.btnTrangSau.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnTrangSau.Location = new System.Drawing.Point(1309, 6);
            this.btnTrangSau.Name = "btnTrangSau";
            this.btnTrangSau.Size = new System.Drawing.Size(80, 31);
            this.btnTrangSau.TabIndex = 5;
            this.btnTrangSau.Text = "Sau ▶";
            this.btnTrangSau.UseVisualStyleBackColor = true;
            this.btnTrangSau.Click += new System.EventHandler(this.btnTrangSau_Click);
            // 
            // dgvNhaCungCap
            // 
            this.dgvNhaCungCap.AllowUserToResizeRows = false;
            this.dgvNhaCungCap.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvNhaCungCap.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvNhaCungCap.Location = new System.Drawing.Point(387, 84);
            this.dgvNhaCungCap.Name = "dgvNhaCungCap";
            this.dgvNhaCungCap.RowHeadersWidth = 51;
            this.dgvNhaCungCap.RowTemplate.Height = 32;
            this.dgvNhaCungCap.Size = new System.Drawing.Size(1395, 727);
            this.dgvNhaCungCap.TabIndex = 2;
            this.dgvNhaCungCap.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvNhaCungCap_CellContentClick);
            this.dgvNhaCungCap.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvNhaCungCap_CellDoubleClick);
            this.dgvNhaCungCap.CellFormatting += new System.Windows.Forms.DataGridViewCellFormattingEventHandler(this.dgvNhaCungCap_CellFormatting);
            this.dgvNhaCungCap.CellMouseLeave += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvNhaCungCap_CellMouseLeave);
            this.dgvNhaCungCap.CellMouseMove += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.dgvNhaCungCap_CellMouseMove);
            this.dgvNhaCungCap.CellPainting += new System.Windows.Forms.DataGridViewCellPaintingEventHandler(this.dgvNhaCungCap_CellPainting);
            this.dgvNhaCungCap.CellValueChanged += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvNhaCungCap_CellValueChanged);
            this.dgvNhaCungCap.ColumnHeaderMouseClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.dgvNhaCungCap_ColumnHeaderMouseClick);
            this.dgvNhaCungCap.CurrentCellDirtyStateChanged += new System.EventHandler(this.dgvNhaCungCap_CurrentCellDirtyStateChanged);
            this.dgvNhaCungCap.DataError += new System.Windows.Forms.DataGridViewDataErrorEventHandler(this.dgvNhaCungCap_DataError);
            // 
            // frmNhaCungCap
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1782, 853);
            this.Controls.Add(this.dgvNhaCungCap);
            this.Controls.Add(this.pnlPhanTrang);
            this.Controls.Add(this.pnlBoLoc);
            this.Controls.Add(this.pnlTop);
            this.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "frmNhaCungCap";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Danh Sách Nhà Cung Cấp";
            this.Load += new System.EventHandler(this.frmNhaCungCap_Load);
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
            ((System.ComponentModel.ISupportInitialize)(this.dgvNhaCungCap)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pnlTop;
        private System.Windows.Forms.GroupBox grpTop;
        private System.Windows.Forms.Label lblTieuDe;
        private System.Windows.Forms.Button btnHuongDanSuDung;
        private System.Windows.Forms.Button btnHienCot;
        private System.Windows.Forms.Button btnXuatFile;
        private System.Windows.Forms.Button btnNhapFile;
        private System.Windows.Forms.Button btnThemNhaCungCap;
        private System.Windows.Forms.Label lblTimKiem;
        private System.Windows.Forms.TextBox txtTimKiem;
        private System.Windows.Forms.Panel pnlBoLoc;
        private System.Windows.Forms.GroupBox grpBoLoc;
        private System.Windows.Forms.Splitter splitterBoLoc;
        private System.Windows.Forms.Label lblNhomNCC;
        private System.Windows.Forms.ComboBox cboNhomNCC;
        private System.Windows.Forms.Button btnThemNhomNCC;
        private System.Windows.Forms.Label lblMucTongMua;
        private System.Windows.Forms.ComboBox cboMucTongMua;
        private System.Windows.Forms.Label lblNoHienTai;
        private System.Windows.Forms.ComboBox cboNoHienTai;
        private System.Windows.Forms.Label lblTrangThai;
        private System.Windows.Forms.ComboBox cboTrangThai;
        private System.Windows.Forms.GroupBox grpThoiGianTao;
        private System.Windows.Forms.DateTimePicker dtpTuNgay;
        private System.Windows.Forms.DateTimePicker dtpDenNgay;
        private System.Windows.Forms.Label lblTuNgay;
        private System.Windows.Forms.Label lblDenNgay;
        private System.Windows.Forms.Label lblThoiGianTao;
        private System.Windows.Forms.Panel pnlPhanTrang;
        private System.Windows.Forms.Label lblTongSoNCC;
        private System.Windows.Forms.Label lblSoDongMoiTrang;
        private System.Windows.Forms.ComboBox cboSoDongMoiTrang;
        private System.Windows.Forms.Button btnTrangTruoc;
        private System.Windows.Forms.Label lblTrangHienTai;
        private System.Windows.Forms.Button btnTrangSau;
        private System.Windows.Forms.DataGridView dgvNhaCungCap;
    }
}

