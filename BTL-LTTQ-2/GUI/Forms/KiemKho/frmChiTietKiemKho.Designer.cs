namespace BTL_LTTQ_2.GUI.Forms.KiemKho
{
    partial class frmChiTietKiemKho
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
            this.pnlHeader = new System.Windows.Forms.Panel();
            this.lblMaPhieu = new System.Windows.Forms.Label();
            this.lblTrangThaiBadge = new System.Windows.Forms.Label();
            this.lblNguoiTao = new System.Windows.Forms.Label();
            this.lblNgayTao = new System.Windows.Forms.Label();
            this.lblNguoiCanBang = new System.Windows.Forms.Label();
            this.lblNgayCanBang = new System.Windows.Forms.Label();
            this.lblKho = new System.Windows.Forms.Label();
            this.pnlCenter = new System.Windows.Forms.Panel();
            this.dgvChiTiet = new System.Windows.Forms.DataGridView();
            this.colStt = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colMaHangNoiBo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colTenHang = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colDonViTinh = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colTonKho = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colThucTe = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colSlLech = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colDonGia = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colGiaTriLech = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.pnlBottom = new System.Windows.Forms.Panel();
            this.pnlTongKet = new System.Windows.Forms.Panel();
            this.lblTongThucTeLbl = new System.Windows.Forms.Label();
            this.lblTongThucTeVal = new System.Windows.Forms.Label();
            this.lblTongLechTangLbl = new System.Windows.Forms.Label();
            this.lblTongLechTangVal = new System.Windows.Forms.Label();
            this.lblTongLechGiamLbl = new System.Windows.Forms.Label();
            this.lblTongLechGiamVal = new System.Windows.Forms.Label();
            this.lblTongChenhLechLbl = new System.Windows.Forms.Label();
            this.lblTongChenhLechVal = new System.Windows.Forms.Label();
            this.pnlGhiChu = new System.Windows.Forms.Panel();
            this.lblGhiChuTieuDe = new System.Windows.Forms.Label();
            this.txtGhiChu = new System.Windows.Forms.TextBox();
            this.pnlActions = new System.Windows.Forms.Panel();
            this.btnXoa = new System.Windows.Forms.Button();
            this.btnXuatFile = new System.Windows.Forms.Button();
            this.btnDong = new System.Windows.Forms.Button();
            this.pnlHeader.SuspendLayout();
            this.pnlCenter.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvChiTiet)).BeginInit();
            this.pnlBottom.SuspendLayout();
            this.pnlTongKet.SuspendLayout();
            this.pnlGhiChu.SuspendLayout();
            this.pnlActions.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlHeader
            // 
            this.pnlHeader.BackColor = System.Drawing.Color.White;
            this.pnlHeader.Controls.Add(this.lblKho);
            this.pnlHeader.Controls.Add(this.lblNgayCanBang);
            this.pnlHeader.Controls.Add(this.lblNguoiCanBang);
            this.pnlHeader.Controls.Add(this.lblNgayTao);
            this.pnlHeader.Controls.Add(this.lblNguoiTao);
            this.pnlHeader.Controls.Add(this.lblTrangThaiBadge);
            this.pnlHeader.Controls.Add(this.lblMaPhieu);
            this.pnlHeader.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlHeader.Location = new System.Drawing.Point(0, 0);
            this.pnlHeader.Name = "pnlHeader";
            this.pnlHeader.Padding = new System.Windows.Forms.Padding(15, 12, 15, 10);
            this.pnlHeader.Size = new System.Drawing.Size(1080, 115);
            this.pnlHeader.TabIndex = 0;
            // 
            // lblMaPhieu
            // 
            this.lblMaPhieu.AutoSize = true;
            this.lblMaPhieu.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.lblMaPhieu.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(23)))), ((int)(((byte)(42)))));
            this.lblMaPhieu.Location = new System.Drawing.Point(12, 12);
            this.lblMaPhieu.Name = "lblMaPhieu";
            this.lblMaPhieu.Size = new System.Drawing.Size(148, 32);
            this.lblMaPhieu.TabIndex = 0;
            this.lblMaPhieu.Text = "PKK000003";
            // 
            // lblTrangThaiBadge
            // 
            this.lblTrangThaiBadge.AutoSize = true;
            this.lblTrangThaiBadge.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.lblTrangThaiBadge.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(22)))), ((int)(((byte)(163)))), ((int)(((byte)(74)))));
            this.lblTrangThaiBadge.Location = new System.Drawing.Point(175, 18);
            this.lblTrangThaiBadge.Name = "lblTrangThaiBadge";
            this.lblTrangThaiBadge.Size = new System.Drawing.Size(87, 19);
            this.lblTrangThaiBadge.TabIndex = 1;
            this.lblTrangThaiBadge.Text = "Đã cân bằng";
            // 
            // lblNguoiTao
            // 
            this.lblNguoiTao.AutoSize = true;
            this.lblNguoiTao.Location = new System.Drawing.Point(14, 55);
            this.lblNguoiTao.Name = "lblNguoiTao";
            this.lblNguoiTao.Size = new System.Drawing.Size(182, 21);
            this.lblNguoiTao.TabIndex = 2;
            this.lblNguoiTao.Text = "Người tạo: Phạm Văn Hảo";
            // 
            // lblNgayTao
            // 
            this.lblNgayTao.AutoSize = true;
            this.lblNgayTao.Location = new System.Drawing.Point(260, 55);
            this.lblNgayTao.Name = "lblNgayTao";
            this.lblNgayTao.Size = new System.Drawing.Size(193, 21);
            this.lblNgayTao.TabIndex = 3;
            this.lblNgayTao.Text = "Ngày tạo: 23/10/2026 17:07";
            // 
            // lblNguoiCanBang
            // 
            this.lblNguoiCanBang.AutoSize = true;
            this.lblNguoiCanBang.Location = new System.Drawing.Point(14, 82);
            this.lblNguoiCanBang.Name = "lblNguoiCanBang";
            this.lblNguoiCanBang.Size = new System.Drawing.Size(222, 21);
            this.lblNguoiCanBang.TabIndex = 4;
            this.lblNguoiCanBang.Text = "Người cân bằng: Phạm Văn Hảo";
            // 
            // lblNgayCanBang
            // 
            this.lblNgayCanBang.AutoSize = true;
            this.lblNgayCanBang.Location = new System.Drawing.Point(260, 82);
            this.lblNgayCanBang.Name = "lblNgayCanBang";
            this.lblNgayCanBang.Size = new System.Drawing.Size(233, 21);
            this.lblNgayCanBang.TabIndex = 5;
            this.lblNgayCanBang.Text = "Ngày cân bằng: 23/10/2026 17:07";
            // 
            // lblKho
            // 
            this.lblKho.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblKho.AutoSize = true;
            this.lblKho.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold);
            this.lblKho.Location = new System.Drawing.Point(820, 18);
            this.lblKho.Name = "lblKho";
            this.lblKho.Size = new System.Drawing.Size(148, 21);
            this.lblKho.TabIndex = 6;
            this.lblKho.Text = "Kho: Kho trung tâm";
            this.lblKho.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // pnlCenter
            // 
            this.pnlCenter.Controls.Add(this.dgvChiTiet);
            this.pnlCenter.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlCenter.Location = new System.Drawing.Point(0, 115);
            this.pnlCenter.Name = "pnlCenter";
            this.pnlCenter.Padding = new System.Windows.Forms.Padding(15, 0, 15, 0);
            this.pnlCenter.Size = new System.Drawing.Size(1080, 360);
            this.pnlCenter.TabIndex = 1;
            // 
            // dgvChiTiet
            // 
            this.dgvChiTiet.AllowUserToAddRows = false;
            this.dgvChiTiet.AllowUserToDeleteRows = false;
            this.dgvChiTiet.BackgroundColor = System.Drawing.Color.White;
            this.dgvChiTiet.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvChiTiet.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colStt,
            this.colMaHangNoiBo,
            this.colTenHang,
            this.colDonViTinh,
            this.colTonKho,
            this.colThucTe,
            this.colSlLech,
            this.colDonGia,
            this.colGiaTriLech});
            this.dgvChiTiet.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvChiTiet.Location = new System.Drawing.Point(15, 0);
            this.dgvChiTiet.Name = "dgvChiTiet";
            this.dgvChiTiet.ReadOnly = true;
            this.dgvChiTiet.RowHeadersVisible = false;
            this.dgvChiTiet.RowHeadersWidth = 51;
            this.dgvChiTiet.RowTemplate.Height = 28;
            this.dgvChiTiet.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvChiTiet.Size = new System.Drawing.Size(1050, 360);
            this.dgvChiTiet.TabIndex = 0;
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
            // colThucTe
            // 
            this.colThucTe.DataPropertyName = "SoLuongThucTe";
            this.colThucTe.HeaderText = "Thực tế";
            this.colThucTe.MinimumWidth = 6;
            this.colThucTe.Name = "colThucTe";
            this.colThucTe.ReadOnly = true;
            this.colThucTe.Width = 100;
            // 
            // colSlLech
            // 
            this.colSlLech.DataPropertyName = "SlChenhLech";
            this.colSlLech.HeaderText = "SL lệch";
            this.colSlLech.MinimumWidth = 6;
            this.colSlLech.Name = "colSlLech";
            this.colSlLech.ReadOnly = true;
            this.colSlLech.Width = 95;
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
            // colGiaTriLech
            // 
            this.colGiaTriLech.DataPropertyName = "GiaTriChenhLech";
            this.colGiaTriLech.HeaderText = "Giá trị lệch";
            this.colGiaTriLech.MinimumWidth = 6;
            this.colGiaTriLech.Name = "colGiaTriLech";
            this.colGiaTriLech.ReadOnly = true;
            this.colGiaTriLech.Width = 135;
            // 
            // pnlBottom
            // 
            this.pnlBottom.BackColor = System.Drawing.Color.White;
            this.pnlBottom.Controls.Add(this.pnlActions);
            this.pnlBottom.Controls.Add(this.pnlTongKet);
            this.pnlBottom.Controls.Add(this.pnlGhiChu);
            this.pnlBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnlBottom.Location = new System.Drawing.Point(0, 475);
            this.pnlBottom.Name = "pnlBottom";
            this.pnlBottom.Padding = new System.Windows.Forms.Padding(15, 10, 15, 12);
            this.pnlBottom.Size = new System.Drawing.Size(1080, 205);
            this.pnlBottom.TabIndex = 2;
            // 
            // pnlGhiChu
            // 
            this.pnlGhiChu.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pnlGhiChu.Controls.Add(this.txtGhiChu);
            this.pnlGhiChu.Controls.Add(this.lblGhiChuTieuDe);
            this.pnlGhiChu.Location = new System.Drawing.Point(15, 6);
            this.pnlGhiChu.Name = "pnlGhiChu";
            this.pnlGhiChu.Size = new System.Drawing.Size(530, 140);
            this.pnlGhiChu.TabIndex = 0;
            // 
            // lblGhiChuTieuDe
            // 
            this.lblGhiChuTieuDe.AutoSize = true;
            this.lblGhiChuTieuDe.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold);
            this.lblGhiChuTieuDe.Location = new System.Drawing.Point(3, 4);
            this.lblGhiChuTieuDe.Name = "lblGhiChuTieuDe";
            this.lblGhiChuTieuDe.Size = new System.Drawing.Size(72, 21);
            this.lblGhiChuTieuDe.TabIndex = 0;
            this.lblGhiChuTieuDe.Text = "Ghi chú:";
            // 
            // txtGhiChu
            // 
            this.txtGhiChu.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtGhiChu.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(250)))), ((int)(((byte)(252)))));
            this.txtGhiChu.Location = new System.Drawing.Point(3, 28);
            this.txtGhiChu.Multiline = true;
            this.txtGhiChu.Name = "txtGhiChu";
            this.txtGhiChu.ReadOnly = true;
            this.txtGhiChu.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtGhiChu.Size = new System.Drawing.Size(524, 105);
            this.txtGhiChu.TabIndex = 1;
            // 
            // pnlTongKet
            // 
            this.pnlTongKet.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.pnlTongKet.Controls.Add(this.lblTongChenhLechVal);
            this.pnlTongKet.Controls.Add(this.lblTongChenhLechLbl);
            this.pnlTongKet.Controls.Add(this.lblTongLechGiamVal);
            this.pnlTongKet.Controls.Add(this.lblTongLechGiamLbl);
            this.pnlTongKet.Controls.Add(this.lblTongLechTangVal);
            this.pnlTongKet.Controls.Add(this.lblTongLechTangLbl);
            this.pnlTongKet.Controls.Add(this.lblTongThucTeVal);
            this.pnlTongKet.Controls.Add(this.lblTongThucTeLbl);
            this.pnlTongKet.Location = new System.Drawing.Point(560, 6);
            this.pnlTongKet.Name = "pnlTongKet";
            this.pnlTongKet.Size = new System.Drawing.Size(505, 140);
            this.pnlTongKet.TabIndex = 1;
            // 
            // lblTongThucTeLbl
            // 
            this.lblTongThucTeLbl.AutoSize = true;
            this.lblTongThucTeLbl.Location = new System.Drawing.Point(15, 8);
            this.lblTongThucTeLbl.Name = "lblTongThucTeLbl";
            this.lblTongThucTeLbl.Size = new System.Drawing.Size(103, 21);
            this.lblTongThucTeLbl.TabIndex = 0;
            this.lblTongThucTeLbl.Text = "Tổng thực tế:";
            // 
            // lblTongThucTeVal
            // 
            this.lblTongThucTeVal.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblTongThucTeVal.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold);
            this.lblTongThucTeVal.Location = new System.Drawing.Point(240, 8);
            this.lblTongThucTeVal.Name = "lblTongThucTeVal";
            this.lblTongThucTeVal.Size = new System.Drawing.Size(255, 21);
            this.lblTongThucTeVal.TabIndex = 1;
            this.lblTongThucTeVal.Text = "0 | 0 đ";
            this.lblTongThucTeVal.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblTongLechTangLbl
            // 
            this.lblTongLechTangLbl.AutoSize = true;
            this.lblTongLechTangLbl.Location = new System.Drawing.Point(15, 38);
            this.lblTongLechTangLbl.Name = "lblTongLechTangLbl";
            this.lblTongLechTangLbl.Size = new System.Drawing.Size(113, 21);
            this.lblTongLechTangLbl.TabIndex = 2;
            this.lblTongLechTangLbl.Text = "Tổng lệch tăng:";
            // 
            // lblTongLechTangVal
            // 
            this.lblTongLechTangVal.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblTongLechTangVal.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold);
            this.lblTongLechTangVal.Location = new System.Drawing.Point(240, 38);
            this.lblTongLechTangVal.Name = "lblTongLechTangVal";
            this.lblTongLechTangVal.Size = new System.Drawing.Size(255, 21);
            this.lblTongLechTangVal.TabIndex = 3;
            this.lblTongLechTangVal.Text = "0 | 0 đ";
            this.lblTongLechTangVal.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblTongLechGiamLbl
            // 
            this.lblTongLechGiamLbl.AutoSize = true;
            this.lblTongLechGiamLbl.Location = new System.Drawing.Point(15, 68);
            this.lblTongLechGiamLbl.Name = "lblTongLechGiamLbl";
            this.lblTongLechGiamLbl.Size = new System.Drawing.Size(117, 21);
            this.lblTongLechGiamLbl.TabIndex = 4;
            this.lblTongLechGiamLbl.Text = "Tổng lệch giảm:";
            // 
            // lblTongLechGiamVal
            // 
            this.lblTongLechGiamVal.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblTongLechGiamVal.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold);
            this.lblTongLechGiamVal.Location = new System.Drawing.Point(240, 68);
            this.lblTongLechGiamVal.Name = "lblTongLechGiamVal";
            this.lblTongLechGiamVal.Size = new System.Drawing.Size(255, 21);
            this.lblTongLechGiamVal.TabIndex = 5;
            this.lblTongLechGiamVal.Text = "0 | 0 đ";
            this.lblTongLechGiamVal.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblTongChenhLechLbl
            // 
            this.lblTongChenhLechLbl.AutoSize = true;
            this.lblTongChenhLechLbl.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.lblTongChenhLechLbl.Location = new System.Drawing.Point(15, 102);
            this.lblTongChenhLechLbl.Name = "lblTongChenhLechLbl";
            this.lblTongChenhLechLbl.Size = new System.Drawing.Size(147, 23);
            this.lblTongChenhLechLbl.TabIndex = 6;
            this.lblTongChenhLechLbl.Text = "Tổng chênh lệch:";
            // 
            // lblTongChenhLechVal
            // 
            this.lblTongChenhLechVal.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblTongChenhLechVal.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            this.lblTongChenhLechVal.Location = new System.Drawing.Point(220, 100);
            this.lblTongChenhLechVal.Name = "lblTongChenhLechVal";
            this.lblTongChenhLechVal.Size = new System.Drawing.Size(275, 25);
            this.lblTongChenhLechVal.TabIndex = 7;
            this.lblTongChenhLechVal.Text = "0 | 0 đ";
            this.lblTongChenhLechVal.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // pnlActions
            // 
            this.pnlActions.Controls.Add(this.btnXoa);
            this.pnlActions.Controls.Add(this.btnXuatFile);
            this.pnlActions.Controls.Add(this.btnDong);
            this.pnlActions.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnlActions.Location = new System.Drawing.Point(15, 153);
            this.pnlActions.Name = "pnlActions";
            this.pnlActions.Size = new System.Drawing.Size(1050, 40);
            this.pnlActions.TabIndex = 2;
            // 
            // btnXoa
            // 
            this.btnXoa.Location = new System.Drawing.Point(0, 4);
            this.btnXoa.Name = "btnXoa";
            this.btnXoa.Size = new System.Drawing.Size(100, 32);
            this.btnXoa.TabIndex = 0;
            this.btnXoa.Text = "Xóa";
            this.btnXoa.UseVisualStyleBackColor = true;
            this.btnXoa.Click += new System.EventHandler(this.btnXoa_Click);
            // 
            // btnXuatFile
            // 
            this.btnXuatFile.Location = new System.Drawing.Point(120, 4);
            this.btnXuatFile.Name = "btnXuatFile";
            this.btnXuatFile.Size = new System.Drawing.Size(120, 32);
            this.btnXuatFile.TabIndex = 1;
            this.btnXuatFile.Text = "Xuất file";
            this.btnXuatFile.UseVisualStyleBackColor = true;
            this.btnXuatFile.Click += new System.EventHandler(this.btnXuatFile_Click);
            // 
            // btnDong
            // 
            this.btnDong.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnDong.Location = new System.Drawing.Point(945, 4);
            this.btnDong.Name = "btnDong";
            this.btnDong.Size = new System.Drawing.Size(105, 32);
            this.btnDong.TabIndex = 2;
            this.btnDong.Text = "Đóng";
            this.btnDong.UseVisualStyleBackColor = true;
            this.btnDong.Click += new System.EventHandler(this.btnDong_Click);
            // 
            // frmChiTietKiemKho
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 21F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(1080, 680);
            this.Controls.Add(this.pnlCenter);
            this.Controls.Add(this.pnlBottom);
            this.Controls.Add(this.pnlHeader);
            this.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.MinimumSize = new System.Drawing.Size(950, 600);
            this.Name = "frmChiTietKiemKho";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Chi tiết phiếu kiểm kho";
            this.Load += new System.EventHandler(this.frmChiTietKiemKho_Load);
            this.pnlHeader.ResumeLayout(false);
            this.pnlHeader.PerformLayout();
            this.pnlCenter.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvChiTiet)).EndInit();
            this.pnlBottom.ResumeLayout(false);
            this.pnlTongKet.ResumeLayout(false);
            this.pnlTongKet.PerformLayout();
            this.pnlGhiChu.ResumeLayout(false);
            this.pnlGhiChu.PerformLayout();
            this.pnlActions.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pnlHeader;
        private System.Windows.Forms.Label lblMaPhieu;
        private System.Windows.Forms.Label lblTrangThaiBadge;
        private System.Windows.Forms.Label lblNguoiTao;
        private System.Windows.Forms.Label lblNgayTao;
        private System.Windows.Forms.Label lblNguoiCanBang;
        private System.Windows.Forms.Label lblNgayCanBang;
        private System.Windows.Forms.Label lblKho;
        private System.Windows.Forms.Panel pnlCenter;
        private System.Windows.Forms.DataGridView dgvChiTiet;
        private System.Windows.Forms.Panel pnlBottom;
        private System.Windows.Forms.Panel pnlGhiChu;
        private System.Windows.Forms.Label lblGhiChuTieuDe;
        private System.Windows.Forms.TextBox txtGhiChu;
        private System.Windows.Forms.Panel pnlTongKet;
        private System.Windows.Forms.Label lblTongThucTeLbl;
        private System.Windows.Forms.Label lblTongThucTeVal;
        private System.Windows.Forms.Label lblTongLechTangLbl;
        private System.Windows.Forms.Label lblTongLechTangVal;
        private System.Windows.Forms.Label lblTongLechGiamLbl;
        private System.Windows.Forms.Label lblTongLechGiamVal;
        private System.Windows.Forms.Label lblTongChenhLechLbl;
        private System.Windows.Forms.Label lblTongChenhLechVal;
        private System.Windows.Forms.Panel pnlActions;
        private System.Windows.Forms.Button btnXoa;
        private System.Windows.Forms.Button btnXuatFile;
        private System.Windows.Forms.Button btnDong;
        private System.Windows.Forms.DataGridViewTextBoxColumn colStt;
        private System.Windows.Forms.DataGridViewTextBoxColumn colMaHangNoiBo;
        private System.Windows.Forms.DataGridViewTextBoxColumn colTenHang;
        private System.Windows.Forms.DataGridViewTextBoxColumn colDonViTinh;
        private System.Windows.Forms.DataGridViewTextBoxColumn colTonKho;
        private System.Windows.Forms.DataGridViewTextBoxColumn colThucTe;
        private System.Windows.Forms.DataGridViewTextBoxColumn colSlLech;
        private System.Windows.Forms.DataGridViewTextBoxColumn colDonGia;
        private System.Windows.Forms.DataGridViewTextBoxColumn colGiaTriLech;
    }
}
