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
            this.lblTieuDe = new System.Windows.Forms.Label();
            this.pnlBottom = new System.Windows.Forms.Panel();
            this.btnBoQua = new System.Windows.Forms.Button();
            this.btnLuu = new System.Windows.Forms.Button();
            this.grpThongTin = new System.Windows.Forms.GroupBox();
            this.cboTrangThai = new System.Windows.Forms.ComboBox();
            this.lblTrangThai = new System.Windows.Forms.Label();
            this.txtGhiChu = new System.Windows.Forms.TextBox();
            this.lblGhiChu = new System.Windows.Forms.Label();
            this.txtDiaChi = new System.Windows.Forms.TextBox();
            this.lblDiaChi = new System.Windows.Forms.Label();
            this.txtKhuVuc = new System.Windows.Forms.TextBox();
            this.lblKhuVuc = new System.Windows.Forms.Label();
            this.txtMaSoThue = new System.Windows.Forms.TextBox();
            this.lblMaSoThue = new System.Windows.Forms.Label();
            this.txtEmail = new System.Windows.Forms.TextBox();
            this.lblEmail = new System.Windows.Forms.Label();
            this.txtDienThoai = new System.Windows.Forms.TextBox();
            this.lblDienThoai = new System.Windows.Forms.Label();
            this.cboNhomNcc = new System.Windows.Forms.ComboBox();
            this.lblNhomNcc = new System.Windows.Forms.Label();
            this.txtTenDonVi = new System.Windows.Forms.TextBox();
            this.lblTenDonVi = new System.Windows.Forms.Label();
            this.txtMaNcc = new System.Windows.Forms.TextBox();
            this.lblMaNcc = new System.Windows.Forms.Label();
            this.grpCongNo = new System.Windows.Forms.GroupBox();
            this.lblNoHienTaiVal = new System.Windows.Forms.Label();
            this.lblNoHienTaiTieuDe = new System.Windows.Forms.Label();
            this.lblTongMuaVal = new System.Windows.Forms.Label();
            this.lblTongMuaTieuDe = new System.Windows.Forms.Label();
            this.pnlTop.SuspendLayout();
            this.pnlBottom.SuspendLayout();
            this.grpThongTin.SuspendLayout();
            this.grpCongNo.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlTop
            // 
            this.pnlTop.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(250)))), ((int)(((byte)(252)))));
            this.pnlTop.Controls.Add(this.lblTieuDe);
            this.pnlTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlTop.Location = new System.Drawing.Point(0, 0);
            this.pnlTop.Name = "pnlTop";
            this.pnlTop.Size = new System.Drawing.Size(782, 56);
            this.pnlTop.TabIndex = 0;
            // 
            // lblTieuDe
            // 
            this.lblTieuDe.AutoSize = true;
            this.lblTieuDe.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.lblTieuDe.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(23)))), ((int)(((byte)(42)))));
            this.lblTieuDe.Location = new System.Drawing.Point(20, 14);
            this.lblTieuDe.Name = "lblTieuDe";
            this.lblTieuDe.Size = new System.Drawing.Size(183, 28);
            this.lblTieuDe.TabIndex = 0;
            this.lblTieuDe.Text = "Tạo nhà cung cấp";
            // 
            // pnlBottom
            // 
            this.pnlBottom.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(250)))), ((int)(((byte)(252)))));
            this.pnlBottom.Controls.Add(this.btnBoQua);
            this.pnlBottom.Controls.Add(this.btnLuu);
            this.pnlBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnlBottom.Location = new System.Drawing.Point(0, 597);
            this.pnlBottom.Name = "pnlBottom";
            this.pnlBottom.Size = new System.Drawing.Size(782, 56);
            this.pnlBottom.TabIndex = 2;
            // 
            // btnBoQua
            // 
            this.btnBoQua.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnBoQua.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnBoQua.Location = new System.Drawing.Point(660, 12);
            this.btnBoQua.Name = "btnBoQua";
            this.btnBoQua.Size = new System.Drawing.Size(100, 34);
            this.btnBoQua.TabIndex = 1;
            this.btnBoQua.Text = "Bỏ qua";
            this.btnBoQua.UseVisualStyleBackColor = true;
            this.btnBoQua.Click += new System.EventHandler(this.btnBoQua_Click);
            // 
            // btnLuu
            // 
            this.btnLuu.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnLuu.Location = new System.Drawing.Point(544, 12);
            this.btnLuu.Name = "btnLuu";
            this.btnLuu.Size = new System.Drawing.Size(100, 34);
            this.btnLuu.TabIndex = 0;
            this.btnLuu.Text = "Lưu";
            this.btnLuu.UseVisualStyleBackColor = true;
            this.btnLuu.Click += new System.EventHandler(this.btnLuu_Click);
            // 
            // grpThongTin
            // 
            this.grpThongTin.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.grpThongTin.Controls.Add(this.cboTrangThai);
            this.grpThongTin.Controls.Add(this.lblTrangThai);
            this.grpThongTin.Controls.Add(this.txtGhiChu);
            this.grpThongTin.Controls.Add(this.lblGhiChu);
            this.grpThongTin.Controls.Add(this.txtDiaChi);
            this.grpThongTin.Controls.Add(this.lblDiaChi);
            this.grpThongTin.Controls.Add(this.txtKhuVuc);
            this.grpThongTin.Controls.Add(this.lblKhuVuc);
            this.grpThongTin.Controls.Add(this.txtMaSoThue);
            this.grpThongTin.Controls.Add(this.lblMaSoThue);
            this.grpThongTin.Controls.Add(this.txtEmail);
            this.grpThongTin.Controls.Add(this.lblEmail);
            this.grpThongTin.Controls.Add(this.txtDienThoai);
            this.grpThongTin.Controls.Add(this.lblDienThoai);
            this.grpThongTin.Controls.Add(this.cboNhomNcc);
            this.grpThongTin.Controls.Add(this.lblNhomNcc);
            this.grpThongTin.Controls.Add(this.txtTenDonVi);
            this.grpThongTin.Controls.Add(this.lblTenDonVi);
            this.grpThongTin.Controls.Add(this.txtMaNcc);
            this.grpThongTin.Controls.Add(this.lblMaNcc);
            this.grpThongTin.Location = new System.Drawing.Point(18, 68);
            this.grpThongTin.Name = "grpThongTin";
            this.grpThongTin.Size = new System.Drawing.Size(746, 432);
            this.grpThongTin.TabIndex = 1;
            this.grpThongTin.TabStop = false;
            this.grpThongTin.Text = "Thông tin chung";
            // 
            // cboTrangThai
            // 
            this.cboTrangThai.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboTrangThai.FormattingEnabled = true;
            this.cboTrangThai.Location = new System.Drawing.Point(504, 252);
            this.cboTrangThai.Name = "cboTrangThai";
            this.cboTrangThai.Size = new System.Drawing.Size(225, 29);
            this.cboTrangThai.TabIndex = 17;
            // 
            // lblTrangThai
            // 
            this.lblTrangThai.AutoSize = true;
            this.lblTrangThai.Location = new System.Drawing.Point(389, 255);
            this.lblTrangThai.Name = "lblTrangThai";
            this.lblTrangThai.Size = new System.Drawing.Size(82, 21);
            this.lblTrangThai.TabIndex = 16;
            this.lblTrangThai.Text = "Trạng thái:";
            // 
            // txtGhiChu
            // 
            this.txtGhiChu.Location = new System.Drawing.Point(145, 306);
            this.txtGhiChu.Multiline = true;
            this.txtGhiChu.Name = "txtGhiChu";
            this.txtGhiChu.Size = new System.Drawing.Size(584, 110);
            this.txtGhiChu.TabIndex = 20;
            // 
            // lblGhiChu
            // 
            this.lblGhiChu.AutoSize = true;
            this.lblGhiChu.Location = new System.Drawing.Point(18, 309);
            this.lblGhiChu.Name = "lblGhiChu";
            this.lblGhiChu.Size = new System.Drawing.Size(66, 21);
            this.lblGhiChu.TabIndex = 19;
            this.lblGhiChu.Text = "Ghi chú:";
            // 
            // txtDiaChi
            // 
            this.txtDiaChi.Location = new System.Drawing.Point(145, 252);
            this.txtDiaChi.Name = "txtDiaChi";
            this.txtDiaChi.Size = new System.Drawing.Size(225, 29);
            this.txtDiaChi.TabIndex = 15;
            // 
            // lblDiaChi
            // 
            this.lblDiaChi.AutoSize = true;
            this.lblDiaChi.Location = new System.Drawing.Point(18, 255);
            this.lblDiaChi.Name = "lblDiaChi";
            this.lblDiaChi.Size = new System.Drawing.Size(60, 21);
            this.lblDiaChi.TabIndex = 14;
            this.lblDiaChi.Text = "Địa chỉ:";
            // 
            // txtKhuVuc
            // 
            this.txtKhuVuc.Location = new System.Drawing.Point(504, 198);
            this.txtKhuVuc.Name = "txtKhuVuc";
            this.txtKhuVuc.Size = new System.Drawing.Size(225, 29);
            this.txtKhuVuc.TabIndex = 13;
            // 
            // lblKhuVuc
            // 
            this.lblKhuVuc.AutoSize = true;
            this.lblKhuVuc.Location = new System.Drawing.Point(389, 201);
            this.lblKhuVuc.Name = "lblKhuVuc";
            this.lblKhuVuc.Size = new System.Drawing.Size(68, 21);
            this.lblKhuVuc.TabIndex = 12;
            this.lblKhuVuc.Text = "Khu vực:";
            // 
            // txtMaSoThue
            // 
            this.txtMaSoThue.Location = new System.Drawing.Point(145, 198);
            this.txtMaSoThue.Name = "txtMaSoThue";
            this.txtMaSoThue.Size = new System.Drawing.Size(225, 29);
            this.txtMaSoThue.TabIndex = 11;
            // 
            // lblMaSoThue
            // 
            this.lblMaSoThue.AutoSize = true;
            this.lblMaSoThue.Location = new System.Drawing.Point(18, 201);
            this.lblMaSoThue.Name = "lblMaSoThue";
            this.lblMaSoThue.Size = new System.Drawing.Size(90, 21);
            this.lblMaSoThue.TabIndex = 10;
            this.lblMaSoThue.Text = "Mã số thuế:";
            // 
            // txtEmail
            // 
            this.txtEmail.Location = new System.Drawing.Point(504, 144);
            this.txtEmail.Name = "txtEmail";
            this.txtEmail.Size = new System.Drawing.Size(225, 29);
            this.txtEmail.TabIndex = 9;
            // 
            // lblEmail
            // 
            this.lblEmail.AutoSize = true;
            this.lblEmail.Location = new System.Drawing.Point(389, 147);
            this.lblEmail.Name = "lblEmail";
            this.lblEmail.Size = new System.Drawing.Size(51, 21);
            this.lblEmail.TabIndex = 8;
            this.lblEmail.Text = "Email:";
            // 
            // txtDienThoai
            // 
            this.txtDienThoai.Location = new System.Drawing.Point(145, 144);
            this.txtDienThoai.Name = "txtDienThoai";
            this.txtDienThoai.Size = new System.Drawing.Size(225, 29);
            this.txtDienThoai.TabIndex = 7;
            // 
            // lblDienThoai
            // 
            this.lblDienThoai.AutoSize = true;
            this.lblDienThoai.Location = new System.Drawing.Point(18, 147);
            this.lblDienThoai.Name = "lblDienThoai";
            this.lblDienThoai.Size = new System.Drawing.Size(84, 21);
            this.lblDienThoai.TabIndex = 6;
            this.lblDienThoai.Text = "Điện thoại:";
            // 
            // cboNhomNcc
            // 
            this.cboNhomNcc.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboNhomNcc.FormattingEnabled = true;
            this.cboNhomNcc.Location = new System.Drawing.Point(504, 34);
            this.cboNhomNcc.Name = "cboNhomNcc";
            this.cboNhomNcc.Size = new System.Drawing.Size(225, 29);
            this.cboNhomNcc.TabIndex = 4;
            // 
            // lblNhomNcc
            // 
            this.lblNhomNcc.AutoSize = true;
            this.lblNhomNcc.Location = new System.Drawing.Point(389, 38);
            this.lblNhomNcc.Name = "lblNhomNcc";
            this.lblNhomNcc.Size = new System.Drawing.Size(89, 21);
            this.lblNhomNcc.TabIndex = 3;
            this.lblNhomNcc.Text = "Nhóm NCC:";
            // 
            // txtTenDonVi
            // 
            this.txtTenDonVi.Location = new System.Drawing.Point(145, 88);
            this.txtTenDonVi.Name = "txtTenDonVi";
            this.txtTenDonVi.Size = new System.Drawing.Size(584, 29);
            this.txtTenDonVi.TabIndex = 2;
            // 
            // lblTenDonVi
            // 
            this.lblTenDonVi.AutoSize = true;
            this.lblTenDonVi.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.lblTenDonVi.Location = new System.Drawing.Point(18, 91);
            this.lblTenDonVi.Name = "lblTenDonVi";
            this.lblTenDonVi.Size = new System.Drawing.Size(91, 21);
            this.lblTenDonVi.TabIndex = 1;
            this.lblTenDonVi.Text = "Tên NCC (*):";
            // 
            // txtMaNcc
            // 
            this.txtMaNcc.Location = new System.Drawing.Point(145, 34);
            this.txtMaNcc.Name = "txtMaNcc";
            this.txtMaNcc.Size = new System.Drawing.Size(225, 29);
            this.txtMaNcc.TabIndex = 0;
            // 
            // lblMaNcc
            // 
            this.lblMaNcc.AutoSize = true;
            this.lblMaNcc.Location = new System.Drawing.Point(18, 38);
            this.lblMaNcc.Name = "lblMaNcc";
            this.lblMaNcc.Size = new System.Drawing.Size(69, 21);
            this.lblMaNcc.TabIndex = 0;
            this.lblMaNcc.Text = "Mã NCC:";
            // 
            // grpCongNo
            // 
            this.grpCongNo.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.grpCongNo.Controls.Add(this.lblNoHienTaiVal);
            this.grpCongNo.Controls.Add(this.lblNoHienTaiTieuDe);
            this.grpCongNo.Controls.Add(this.lblTongMuaVal);
            this.grpCongNo.Controls.Add(this.lblTongMuaTieuDe);
            this.grpCongNo.Location = new System.Drawing.Point(18, 510);
            this.grpCongNo.Name = "grpCongNo";
            this.grpCongNo.Size = new System.Drawing.Size(746, 75);
            this.grpCongNo.TabIndex = 3;
            this.grpCongNo.TabStop = false;
            this.grpCongNo.Text = "Thống kê giao dịch & Công nợ";
            // 
            // lblNoHienTaiVal
            // 
            this.lblNoHienTaiVal.AutoSize = true;
            this.lblNoHienTaiVal.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.lblNoHienTaiVal.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(38)))), ((int)(((byte)(38)))));
            this.lblNoHienTaiVal.Location = new System.Drawing.Point(525, 32);
            this.lblNoHienTaiVal.Name = "lblNoHienTaiVal";
            this.lblNoHienTaiVal.Size = new System.Drawing.Size(43, 23);
            this.lblNoHienTaiVal.TabIndex = 3;
            this.lblNoHienTaiVal.Text = "0 đ";
            // 
            // lblNoHienTaiTieuDe
            // 
            this.lblNoHienTaiTieuDe.AutoSize = true;
            this.lblNoHienTaiTieuDe.Location = new System.Drawing.Point(389, 33);
            this.lblNoHienTaiTieuDe.Name = "lblNoHienTaiTieuDe";
            this.lblNoHienTaiTieuDe.Size = new System.Drawing.Size(140, 21);
            this.lblNoHienTaiTieuDe.TabIndex = 2;
            this.lblNoHienTaiTieuDe.Text = "Nợ cần trả hiện tại:";
            // 
            // lblTongMuaVal
            // 
            this.lblTongMuaVal.AutoSize = true;
            this.lblTongMuaVal.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.lblTongMuaVal.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(23)))), ((int)(((byte)(42)))));
            this.lblTongMuaVal.Location = new System.Drawing.Point(141, 32);
            this.lblTongMuaVal.Name = "lblTongMuaVal";
            this.lblTongMuaVal.Size = new System.Drawing.Size(43, 23);
            this.lblTongMuaVal.TabIndex = 1;
            this.lblTongMuaVal.Text = "0 đ";
            // 
            // lblTongMuaTieuDe
            // 
            this.lblTongMuaTieuDe.AutoSize = true;
            this.lblTongMuaTieuDe.Location = new System.Drawing.Point(18, 33);
            this.lblTongMuaTieuDe.Name = "lblTongMuaTieuDe";
            this.lblTongMuaTieuDe.Size = new System.Drawing.Size(117, 21);
            this.lblTongMuaTieuDe.TabIndex = 0;
            this.lblTongMuaTieuDe.Text = "Tổng tiền mua:";
            // 
            // frmDonViCapHang
            // 
            this.AcceptButton = this.btnLuu;
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 21F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.CancelButton = this.btnBoQua;
            this.ClientSize = new System.Drawing.Size(782, 653);
            this.Controls.Add(this.grpCongNo);
            this.Controls.Add(this.grpThongTin);
            this.Controls.Add(this.pnlBottom);
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
            this.pnlBottom.ResumeLayout(false);
            this.grpThongTin.ResumeLayout(false);
            this.grpThongTin.PerformLayout();
            this.grpCongNo.ResumeLayout(false);
            this.grpCongNo.PerformLayout();
            this.ResumeLayout(false);
        }

        #endregion

        private System.Windows.Forms.Panel pnlTop;
        private System.Windows.Forms.Label lblTieuDe;
        private System.Windows.Forms.Panel pnlBottom;
        private System.Windows.Forms.Button btnBoQua;
        private System.Windows.Forms.Button btnLuu;
        private System.Windows.Forms.GroupBox grpThongTin;
        private System.Windows.Forms.TextBox txtMaNcc;
        private System.Windows.Forms.Label lblMaNcc;
        private System.Windows.Forms.TextBox txtTenDonVi;
        private System.Windows.Forms.Label lblTenDonVi;
        private System.Windows.Forms.ComboBox cboNhomNcc;
        private System.Windows.Forms.Label lblNhomNcc;
        private System.Windows.Forms.TextBox txtDienThoai;
        private System.Windows.Forms.Label lblDienThoai;
        private System.Windows.Forms.TextBox txtEmail;
        private System.Windows.Forms.Label lblEmail;
        private System.Windows.Forms.TextBox txtMaSoThue;
        private System.Windows.Forms.Label lblMaSoThue;
        private System.Windows.Forms.TextBox txtKhuVuc;
        private System.Windows.Forms.Label lblKhuVuc;
        private System.Windows.Forms.TextBox txtDiaChi;
        private System.Windows.Forms.Label lblDiaChi;
        private System.Windows.Forms.TextBox txtGhiChu;
        private System.Windows.Forms.Label lblGhiChu;
        private System.Windows.Forms.ComboBox cboTrangThai;
        private System.Windows.Forms.Label lblTrangThai;
        private System.Windows.Forms.GroupBox grpCongNo;
        private System.Windows.Forms.Label lblTongMuaTieuDe;
        private System.Windows.Forms.Label lblTongMuaVal;
        private System.Windows.Forms.Label lblNoHienTaiTieuDe;
        private System.Windows.Forms.Label lblNoHienTaiVal;
    }
}
