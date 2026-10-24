namespace BTL_LTTQ_2.GUI.Forms.NhaCungCap
{
    partial class frmDonViCapHang
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
            this.pnlTopDivider = new System.Windows.Forms.Panel();
            this.lblTieuDe = new System.Windows.Forms.Label();
            this.pnlThongTinCoBan = new System.Windows.Forms.Panel();
            this.cboNhomNcc = new System.Windows.Forms.ComboBox();
            this.lblNhomNcc = new System.Windows.Forms.Label();
            this.txtDiaChi = new System.Windows.Forms.TextBox();
            this.lblDiaChi = new System.Windows.Forms.Label();
            this.txtMaSoThue = new System.Windows.Forms.TextBox();
            this.lblMaSoThue = new System.Windows.Forms.Label();
            this.txtTenDonVi = new System.Windows.Forms.TextBox();
            this.lblTenDonVi = new System.Windows.Forms.Label();
            this.txtMaNcc = new System.Windows.Forms.TextBox();
            this.lblMaNcc = new System.Windows.Forms.Label();
            this.grpLienHe = new System.Windows.Forms.GroupBox();
            this.lblTieuDeLienHe = new System.Windows.Forms.Label();
            this.txtKhuVuc = new System.Windows.Forms.TextBox();
            this.lblKhuVuc = new System.Windows.Forms.Label();
            this.txtEmail = new System.Windows.Forms.TextBox();
            this.lblEmail = new System.Windows.Forms.Label();
            this.txtDienThoai = new System.Windows.Forms.TextBox();
            this.lblDienThoai = new System.Windows.Forms.Label();
            this.grpGhiChu = new System.Windows.Forms.GroupBox();
            this.lblTieuDeGhiChu = new System.Windows.Forms.Label();
            this.cboTrangThai = new System.Windows.Forms.ComboBox();
            this.lblTrangThai = new System.Windows.Forms.Label();
            this.txtGhiChu = new System.Windows.Forms.TextBox();
            this.lblGhiChu = new System.Windows.Forms.Label();
            this.grpCongNo = new System.Windows.Forms.GroupBox();
            this.lblTieuDeCongNo = new System.Windows.Forms.Label();
            this.lblNoHienTaiVal = new System.Windows.Forms.Label();
            this.lblNoHienTaiTieuDe = new System.Windows.Forms.Label();
            this.lblTongMuaVal = new System.Windows.Forms.Label();
            this.lblTongMuaTieuDe = new System.Windows.Forms.Label();
            this.pnlBottom = new System.Windows.Forms.Panel();
            this.pnlBottomDivider = new System.Windows.Forms.Panel();
            this.btnBoQua = new System.Windows.Forms.Button();
            this.btnLuu = new System.Windows.Forms.Button();
            this.pnlTop.SuspendLayout();
            this.pnlThongTinCoBan.SuspendLayout();
            this.grpLienHe.SuspendLayout();
            this.grpGhiChu.SuspendLayout();
            this.grpCongNo.SuspendLayout();
            this.pnlBottom.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlTop
            // 
            this.pnlTop.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(250)))), ((int)(((byte)(252)))));
            this.pnlTop.Controls.Add(this.pnlTopDivider);
            this.pnlTop.Controls.Add(this.lblTieuDe);
            this.pnlTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlTop.Location = new System.Drawing.Point(0, 0);
            this.pnlTop.Name = "pnlTop";
            this.pnlTop.Size = new System.Drawing.Size(960, 60);
            this.pnlTop.TabIndex = 0;
            // 
            // pnlTopDivider
            // 
            this.pnlTopDivider.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(232)))), ((int)(((byte)(240)))));
            this.pnlTopDivider.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnlTopDivider.Location = new System.Drawing.Point(0, 59);
            this.pnlTopDivider.Name = "pnlTopDivider";
            this.pnlTopDivider.Size = new System.Drawing.Size(960, 1);
            this.pnlTopDivider.TabIndex = 1;
            // 
            // lblTieuDe
            // 
            this.lblTieuDe.AutoSize = true;
            this.lblTieuDe.Font = new System.Drawing.Font("Segoe UI", 12.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.lblTieuDe.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(23)))), ((int)(((byte)(42)))));
            this.lblTieuDe.Location = new System.Drawing.Point(26, 15);
            this.lblTieuDe.Name = "lblTieuDe";
            this.lblTieuDe.Size = new System.Drawing.Size(150, 23);
            this.lblTieuDe.TabIndex = 0;
            this.lblTieuDe.Text = "Tạo nhà cung cấp";
            // 
            // pnlThongTinCoBan
            // 
            this.pnlThongTinCoBan.Controls.Add(this.cboNhomNcc);
            this.pnlThongTinCoBan.Controls.Add(this.lblNhomNcc);
            this.pnlThongTinCoBan.Controls.Add(this.txtDiaChi);
            this.pnlThongTinCoBan.Controls.Add(this.lblDiaChi);
            this.pnlThongTinCoBan.Controls.Add(this.txtMaSoThue);
            this.pnlThongTinCoBan.Controls.Add(this.lblMaSoThue);
            this.pnlThongTinCoBan.Controls.Add(this.txtTenDonVi);
            this.pnlThongTinCoBan.Controls.Add(this.lblTenDonVi);
            this.pnlThongTinCoBan.Controls.Add(this.txtMaNcc);
            this.pnlThongTinCoBan.Controls.Add(this.lblMaNcc);
            this.pnlThongTinCoBan.Location = new System.Drawing.Point(30, 75);
            this.pnlThongTinCoBan.Name = "pnlThongTinCoBan";
            this.pnlThongTinCoBan.Size = new System.Drawing.Size(900, 200);
            this.pnlThongTinCoBan.TabIndex = 1;
            // 
            // cboNhomNcc
            // 
            this.cboNhomNcc.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboNhomNcc.FormattingEnabled = true;
            this.cboNhomNcc.Location = new System.Drawing.Point(0, 164);
            this.cboNhomNcc.Name = "cboNhomNcc";
            this.cboNhomNcc.Size = new System.Drawing.Size(400, 25);
            this.cboNhomNcc.TabIndex = 2;
            // 
            // lblNhomNcc
            // 
            this.lblNhomNcc.AutoSize = true;
            this.lblNhomNcc.Location = new System.Drawing.Point(0, 140);
            this.lblNhomNcc.Name = "lblNhomNcc";
            this.lblNhomNcc.Size = new System.Drawing.Size(126, 17);
            this.lblNhomNcc.TabIndex = 0;
            this.lblNhomNcc.Text = "Nhóm nhà cung cấp";
            // 
            // txtDiaChi
            // 
            this.txtDiaChi.Location = new System.Drawing.Point(450, 94);
            this.txtDiaChi.Multiline = true;
            this.txtDiaChi.Name = "txtDiaChi";
            this.txtDiaChi.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtDiaChi.Size = new System.Drawing.Size(450, 99);
            this.txtDiaChi.TabIndex = 4;
            // 
            // lblDiaChi
            // 
            this.lblDiaChi.AutoSize = true;
            this.lblDiaChi.Location = new System.Drawing.Point(450, 70);
            this.lblDiaChi.Name = "lblDiaChi";
            this.lblDiaChi.Size = new System.Drawing.Size(47, 17);
            this.lblDiaChi.TabIndex = 0;
            this.lblDiaChi.Text = "Địa chỉ";
            // 
            // txtMaSoThue
            // 
            this.txtMaSoThue.Location = new System.Drawing.Point(450, 24);
            this.txtMaSoThue.Name = "txtMaSoThue";
            this.txtMaSoThue.Size = new System.Drawing.Size(450, 24);
            this.txtMaSoThue.TabIndex = 3;
            // 
            // lblMaSoThue
            // 
            this.lblMaSoThue.AutoSize = true;
            this.lblMaSoThue.Location = new System.Drawing.Point(450, 0);
            this.lblMaSoThue.Name = "lblMaSoThue";
            this.lblMaSoThue.Size = new System.Drawing.Size(74, 17);
            this.lblMaSoThue.TabIndex = 0;
            this.lblMaSoThue.Text = "Mã số thuế";
            // 
            // txtTenDonVi
            // 
            this.txtTenDonVi.Location = new System.Drawing.Point(0, 94);
            this.txtTenDonVi.Name = "txtTenDonVi";
            this.txtTenDonVi.Size = new System.Drawing.Size(400, 24);
            this.txtTenDonVi.TabIndex = 1;
            // 
            // lblTenDonVi
            // 
            this.lblTenDonVi.AutoSize = true;
            this.lblTenDonVi.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.lblTenDonVi.Location = new System.Drawing.Point(0, 70);
            this.lblTenDonVi.Name = "lblTenDonVi";
            this.lblTenDonVi.Size = new System.Drawing.Size(127, 17);
            this.lblTenDonVi.TabIndex = 0;
            this.lblTenDonVi.Text = "Tên nhà cung cấp (*)";
            // 
            // txtMaNcc
            // 
            this.txtMaNcc.Location = new System.Drawing.Point(0, 24);
            this.txtMaNcc.Name = "txtMaNcc";
            this.txtMaNcc.Size = new System.Drawing.Size(400, 24);
            this.txtMaNcc.TabIndex = 0;
            // 
            // lblMaNcc
            // 
            this.lblMaNcc.AutoSize = true;
            this.lblMaNcc.Location = new System.Drawing.Point(0, 0);
            this.lblMaNcc.Name = "lblMaNcc";
            this.lblMaNcc.Size = new System.Drawing.Size(109, 17);
            this.lblMaNcc.TabIndex = 0;
            this.lblMaNcc.Text = "Mã nhà cung cấp";
            // 
            // grpLienHe
            // 
            this.grpLienHe.Controls.Add(this.lblTieuDeLienHe);
            this.grpLienHe.Controls.Add(this.txtKhuVuc);
            this.grpLienHe.Controls.Add(this.lblKhuVuc);
            this.grpLienHe.Controls.Add(this.txtEmail);
            this.grpLienHe.Controls.Add(this.lblEmail);
            this.grpLienHe.Controls.Add(this.txtDienThoai);
            this.grpLienHe.Controls.Add(this.lblDienThoai);
            this.grpLienHe.Location = new System.Drawing.Point(30, 290);
            this.grpLienHe.Name = "grpLienHe";
            this.grpLienHe.Size = new System.Drawing.Size(900, 110);
            this.grpLienHe.TabIndex = 2;
            this.grpLienHe.TabStop = false;
            // 
            // lblTieuDeLienHe
            // 
            this.lblTieuDeLienHe.AutoSize = true;
            this.lblTieuDeLienHe.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.lblTieuDeLienHe.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(23)))), ((int)(((byte)(42)))));
            this.lblTieuDeLienHe.Location = new System.Drawing.Point(10, 16);
            this.lblTieuDeLienHe.Name = "lblTieuDeLienHe";
            this.lblTieuDeLienHe.Size = new System.Drawing.Size(115, 17);
            this.lblTieuDeLienHe.TabIndex = 0;
            this.lblTieuDeLienHe.Text = "Thông tin liên hệ";
            // 
            // txtKhuVuc
            // 
            this.txtKhuVuc.Location = new System.Drawing.Point(610, 68);
            this.txtKhuVuc.Name = "txtKhuVuc";
            this.txtKhuVuc.Size = new System.Drawing.Size(265, 24);
            this.txtKhuVuc.TabIndex = 2;
            // 
            // lblKhuVuc
            // 
            this.lblKhuVuc.AutoSize = true;
            this.lblKhuVuc.Location = new System.Drawing.Point(610, 45);
            this.lblKhuVuc.Name = "lblKhuVuc";
            this.lblKhuVuc.Size = new System.Drawing.Size(54, 17);
            this.lblKhuVuc.TabIndex = 0;
            this.lblKhuVuc.Text = "Khu vực";
            // 
            // txtEmail
            // 
            this.txtEmail.Location = new System.Drawing.Point(315, 68);
            this.txtEmail.Name = "txtEmail";
            this.txtEmail.Size = new System.Drawing.Size(265, 24);
            this.txtEmail.TabIndex = 1;
            // 
            // lblEmail
            // 
            this.lblEmail.AutoSize = true;
            this.lblEmail.Location = new System.Drawing.Point(315, 45);
            this.lblEmail.Name = "lblEmail";
            this.lblEmail.Size = new System.Drawing.Size(39, 17);
            this.lblEmail.TabIndex = 0;
            this.lblEmail.Text = "Email";
            // 
            // txtDienThoai
            // 
            this.txtDienThoai.Location = new System.Drawing.Point(20, 68);
            this.txtDienThoai.Name = "txtDienThoai";
            this.txtDienThoai.Size = new System.Drawing.Size(265, 24);
            this.txtDienThoai.TabIndex = 0;
            // 
            // lblDienThoai
            // 
            this.lblDienThoai.AutoSize = true;
            this.lblDienThoai.Location = new System.Drawing.Point(20, 45);
            this.lblDienThoai.Name = "lblDienThoai";
            this.lblDienThoai.Size = new System.Drawing.Size(67, 17);
            this.lblDienThoai.TabIndex = 0;
            this.lblDienThoai.Text = "Điện thoại";
            // 
            // grpGhiChu
            // 
            this.grpGhiChu.Controls.Add(this.lblTieuDeGhiChu);
            this.grpGhiChu.Controls.Add(this.cboTrangThai);
            this.grpGhiChu.Controls.Add(this.lblTrangThai);
            this.grpGhiChu.Controls.Add(this.txtGhiChu);
            this.grpGhiChu.Controls.Add(this.lblGhiChu);
            this.grpGhiChu.Location = new System.Drawing.Point(30, 415);
            this.grpGhiChu.Name = "grpGhiChu";
            this.grpGhiChu.Size = new System.Drawing.Size(900, 115);
            this.grpGhiChu.TabIndex = 3;
            this.grpGhiChu.TabStop = false;
            // 
            // lblTieuDeGhiChu
            // 
            this.lblTieuDeGhiChu.AutoSize = true;
            this.lblTieuDeGhiChu.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.lblTieuDeGhiChu.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(23)))), ((int)(((byte)(42)))));
            this.lblTieuDeGhiChu.Location = new System.Drawing.Point(10, 16);
            this.lblTieuDeGhiChu.Name = "lblTieuDeGhiChu";
            this.lblTieuDeGhiChu.Size = new System.Drawing.Size(137, 17);
            this.lblTieuDeGhiChu.TabIndex = 0;
            this.lblTieuDeGhiChu.Text = "Trạng thái && Ghi chú";
            // 
            // cboTrangThai
            // 
            this.cboTrangThai.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboTrangThai.FormattingEnabled = true;
            this.cboTrangThai.Location = new System.Drawing.Point(20, 68);
            this.cboTrangThai.Name = "cboTrangThai";
            this.cboTrangThai.Size = new System.Drawing.Size(265, 25);
            this.cboTrangThai.TabIndex = 0;
            // 
            // lblTrangThai
            // 
            this.lblTrangThai.AutoSize = true;
            this.lblTrangThai.Location = new System.Drawing.Point(20, 45);
            this.lblTrangThai.Name = "lblTrangThai";
            this.lblTrangThai.Size = new System.Drawing.Size(66, 17);
            this.lblTrangThai.TabIndex = 0;
            this.lblTrangThai.Text = "Trạng thái";
            // 
            // txtGhiChu
            // 
            this.txtGhiChu.Location = new System.Drawing.Point(315, 45);
            this.txtGhiChu.Multiline = true;
            this.txtGhiChu.Name = "txtGhiChu";
            this.txtGhiChu.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtGhiChu.Size = new System.Drawing.Size(560, 59);
            this.txtGhiChu.TabIndex = 1;
            // 
            // lblGhiChu
            // 
            this.lblGhiChu.AutoSize = true;
            this.lblGhiChu.Location = new System.Drawing.Point(312, 20);
            this.lblGhiChu.Name = "lblGhiChu";
            this.lblGhiChu.Size = new System.Drawing.Size(51, 17);
            this.lblGhiChu.TabIndex = 0;
            this.lblGhiChu.Text = "Ghi chú";
            // 
            // grpCongNo
            // 
            this.grpCongNo.Controls.Add(this.lblTieuDeCongNo);
            this.grpCongNo.Controls.Add(this.lblNoHienTaiVal);
            this.grpCongNo.Controls.Add(this.lblNoHienTaiTieuDe);
            this.grpCongNo.Controls.Add(this.lblTongMuaVal);
            this.grpCongNo.Controls.Add(this.lblTongMuaTieuDe);
            this.grpCongNo.Location = new System.Drawing.Point(30, 545);
            this.grpCongNo.Name = "grpCongNo";
            this.grpCongNo.Size = new System.Drawing.Size(900, 85);
            this.grpCongNo.TabIndex = 4;
            this.grpCongNo.TabStop = false;
            // 
            // lblTieuDeCongNo
            // 
            this.lblTieuDeCongNo.AutoSize = true;
            this.lblTieuDeCongNo.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.lblTieuDeCongNo.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(23)))), ((int)(((byte)(42)))));
            this.lblTieuDeCongNo.Location = new System.Drawing.Point(10, 16);
            this.lblTieuDeCongNo.Name = "lblTieuDeCongNo";
            this.lblTieuDeCongNo.Size = new System.Drawing.Size(126, 17);
            this.lblTieuDeCongNo.TabIndex = 0;
            this.lblTieuDeCongNo.Text = "Giao dịch & Công nợ";
            // 
            // lblNoHienTaiVal
            // 
            this.lblNoHienTaiVal.AutoSize = true;
            this.lblNoHienTaiVal.Font = new System.Drawing.Font("Segoe UI", 10.5F, System.Drawing.FontStyle.Bold);
            this.lblNoHienTaiVal.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(38)))), ((int)(((byte)(38)))));
            this.lblNoHienTaiVal.Location = new System.Drawing.Point(620, 48);
            this.lblNoHienTaiVal.Name = "lblNoHienTaiVal";
            this.lblNoHienTaiVal.Size = new System.Drawing.Size(30, 19);
            this.lblNoHienTaiVal.TabIndex = 0;
            this.lblNoHienTaiVal.Text = "0 đ";
            // 
            // lblNoHienTaiTieuDe
            // 
            this.lblNoHienTaiTieuDe.AutoSize = true;
            this.lblNoHienTaiTieuDe.Location = new System.Drawing.Point(450, 50);
            this.lblNoHienTaiTieuDe.Name = "lblNoHienTaiTieuDe";
            this.lblNoHienTaiTieuDe.Size = new System.Drawing.Size(119, 17);
            this.lblNoHienTaiTieuDe.TabIndex = 0;
            this.lblNoHienTaiTieuDe.Text = "Nợ cần trả hiện tại:";
            // 
            // lblTongMuaVal
            // 
            this.lblTongMuaVal.AutoSize = true;
            this.lblTongMuaVal.Font = new System.Drawing.Font("Segoe UI", 10.5F, System.Drawing.FontStyle.Bold);
            this.lblTongMuaVal.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(23)))), ((int)(((byte)(42)))));
            this.lblTongMuaVal.Location = new System.Drawing.Point(180, 48);
            this.lblTongMuaVal.Name = "lblTongMuaVal";
            this.lblTongMuaVal.Size = new System.Drawing.Size(30, 19);
            this.lblTongMuaVal.TabIndex = 0;
            this.lblTongMuaVal.Text = "0 đ";
            // 
            // lblTongMuaTieuDe
            // 
            this.lblTongMuaTieuDe.AutoSize = true;
            this.lblTongMuaTieuDe.Location = new System.Drawing.Point(20, 50);
            this.lblTongMuaTieuDe.Name = "lblTongMuaTieuDe";
            this.lblTongMuaTieuDe.Size = new System.Drawing.Size(128, 17);
            this.lblTongMuaTieuDe.TabIndex = 0;
            this.lblTongMuaTieuDe.Text = "Tổng tiền mua hàng:";
            // 
            // pnlBottom
            // 
            this.pnlBottom.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(250)))), ((int)(((byte)(252)))));
            this.pnlBottom.Controls.Add(this.pnlBottomDivider);
            this.pnlBottom.Controls.Add(this.btnBoQua);
            this.pnlBottom.Controls.Add(this.btnLuu);
            this.pnlBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnlBottom.Location = new System.Drawing.Point(0, 645);
            this.pnlBottom.Name = "pnlBottom";
            this.pnlBottom.Size = new System.Drawing.Size(960, 60);
            this.pnlBottom.TabIndex = 5;
            // 
            // pnlBottomDivider
            // 
            this.pnlBottomDivider.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(232)))), ((int)(((byte)(240)))));
            this.pnlBottomDivider.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlBottomDivider.Location = new System.Drawing.Point(0, 0);
            this.pnlBottomDivider.Name = "pnlBottomDivider";
            this.pnlBottomDivider.Size = new System.Drawing.Size(960, 1);
            this.pnlBottomDivider.TabIndex = 2;
            // 
            // btnBoQua
            // 
            this.btnBoQua.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnBoQua.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnBoQua.Location = new System.Drawing.Point(705, 14);
            this.btnBoQua.Name = "btnBoQua";
            this.btnBoQua.Size = new System.Drawing.Size(110, 32);
            this.btnBoQua.TabIndex = 1;
            this.btnBoQua.Text = "Bỏ qua";
            this.btnBoQua.UseVisualStyleBackColor = true;
            this.btnBoQua.Click += new System.EventHandler(this.btnBoQua_Click);
            // 
            // btnLuu
            // 
            this.btnLuu.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnLuu.Location = new System.Drawing.Point(825, 14);
            this.btnLuu.Name = "btnLuu";
            this.btnLuu.Size = new System.Drawing.Size(110, 32);
            this.btnLuu.TabIndex = 0;
            this.btnLuu.Text = "Lưu";
            this.btnLuu.UseVisualStyleBackColor = true;
            this.btnLuu.Click += new System.EventHandler(this.btnLuu_Click);
            // 
            // frmDonViCapHang
            // 
            this.AcceptButton = this.btnLuu;
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.CancelButton = this.btnBoQua;
            this.ClientSize = new System.Drawing.Size(960, 705);
            this.Controls.Add(this.pnlBottom);
            this.Controls.Add(this.grpCongNo);
            this.Controls.Add(this.grpGhiChu);
            this.Controls.Add(this.grpLienHe);
            this.Controls.Add(this.pnlThongTinCoBan);
            this.Controls.Add(this.pnlTop);
            this.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmDonViCapHang";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Thông tin nhà cung cấp";
            this.Load += new System.EventHandler(this.frmDonViCapHang_Load);
            this.pnlTop.ResumeLayout(false);
            this.pnlTop.PerformLayout();
            this.pnlThongTinCoBan.ResumeLayout(false);
            this.pnlThongTinCoBan.PerformLayout();
            this.grpLienHe.ResumeLayout(false);
            this.grpLienHe.PerformLayout();
            this.grpGhiChu.ResumeLayout(false);
            this.grpGhiChu.PerformLayout();
            this.grpCongNo.ResumeLayout(false);
            this.grpCongNo.PerformLayout();
            this.pnlBottom.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pnlTop;
        private System.Windows.Forms.Panel pnlTopDivider;
        private System.Windows.Forms.Label lblTieuDe;
        private System.Windows.Forms.Panel pnlThongTinCoBan;
        private System.Windows.Forms.TextBox txtMaNcc;
        private System.Windows.Forms.Label lblMaNcc;
        private System.Windows.Forms.TextBox txtTenDonVi;
        private System.Windows.Forms.Label lblTenDonVi;
        private System.Windows.Forms.ComboBox cboNhomNcc;
        private System.Windows.Forms.Label lblNhomNcc;
        private System.Windows.Forms.TextBox txtMaSoThue;
        private System.Windows.Forms.Label lblMaSoThue;
        private System.Windows.Forms.TextBox txtDiaChi;
        private System.Windows.Forms.Label lblDiaChi;
        private System.Windows.Forms.GroupBox grpLienHe;
        private System.Windows.Forms.Label lblTieuDeLienHe;
        private System.Windows.Forms.TextBox txtDienThoai;
        private System.Windows.Forms.Label lblDienThoai;
        private System.Windows.Forms.TextBox txtEmail;
        private System.Windows.Forms.Label lblEmail;
        private System.Windows.Forms.TextBox txtKhuVuc;
        private System.Windows.Forms.Label lblKhuVuc;
        private System.Windows.Forms.GroupBox grpGhiChu;
        private System.Windows.Forms.Label lblTieuDeGhiChu;
        private System.Windows.Forms.ComboBox cboTrangThai;
        private System.Windows.Forms.Label lblTrangThai;
        private System.Windows.Forms.TextBox txtGhiChu;
        private System.Windows.Forms.Label lblGhiChu;
        private System.Windows.Forms.GroupBox grpCongNo;
        private System.Windows.Forms.Label lblTieuDeCongNo;
        private System.Windows.Forms.Label lblTongMuaTieuDe;
        private System.Windows.Forms.Label lblTongMuaVal;
        private System.Windows.Forms.Label lblNoHienTaiTieuDe;
        private System.Windows.Forms.Label lblNoHienTaiVal;
        private System.Windows.Forms.Panel pnlBottom;
        private System.Windows.Forms.Panel pnlBottomDivider;
        private System.Windows.Forms.Button btnBoQua;
        private System.Windows.Forms.Button btnLuu;
    }
}