namespace BTL_LTTQ_2.GUI.Forms.NhaCungCap
{
    partial class frmNhapNhaCungCapFile
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
            this.grpTieuDe = new System.Windows.Forms.GroupBox();
            this.lblTieuDe = new System.Windows.Forms.Label();
            this.btnTaiFileMauExcel = new System.Windows.Forms.Button();
            this.grpChonFile = new System.Windows.Forms.GroupBox();
            this.btnChonFile = new System.Windows.Forms.Button();
            this.txtDuongDanFile = new System.Windows.Forms.TextBox();
            this.grpTuyChon = new System.Windows.Forms.GroupBox();
            this.rdoBoQua = new System.Windows.Forms.RadioButton();
            this.rdoCapNhat = new System.Windows.Forms.RadioButton();
            this.prgTienTrinh = new System.Windows.Forms.ProgressBar();
            this.btnThucHien = new System.Windows.Forms.Button();
            this.btnDong = new System.Windows.Forms.Button();
            this.pnlBottom = new System.Windows.Forms.Panel();
            this.grpThaoTac = new System.Windows.Forms.GroupBox();
            this.pnlHeader.SuspendLayout();
            this.grpTieuDe.SuspendLayout();
            this.grpChonFile.SuspendLayout();
            this.grpTuyChon.SuspendLayout();
            this.pnlBottom.SuspendLayout();
            this.grpThaoTac.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlHeader
            // 
            this.pnlHeader.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(250)))), ((int)(((byte)(252)))));
            this.pnlHeader.Controls.Add(this.grpTieuDe);
            this.pnlHeader.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlHeader.Location = new System.Drawing.Point(0, 0);
            this.pnlHeader.Name = "pnlHeader";
            this.pnlHeader.Size = new System.Drawing.Size(882, 80);
            this.pnlHeader.TabIndex = 0;
            // 
            // grpTieuDe
            // 
            this.grpTieuDe.Controls.Add(this.lblTieuDe);
            this.grpTieuDe.Location = new System.Drawing.Point(0, 12);
            this.grpTieuDe.Name = "grpTieuDe";
            this.grpTieuDe.Size = new System.Drawing.Size(882, 50);
            this.grpTieuDe.TabIndex = 0;
            this.grpTieuDe.TabStop = false;
            // 
            // lblTieuDe
            // 
            this.lblTieuDe.AutoSize = true;
            this.lblTieuDe.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.lblTieuDe.Location = new System.Drawing.Point(10, 16);
            this.lblTieuDe.Name = "lblTieuDe";
            this.lblTieuDe.Size = new System.Drawing.Size(420, 28);
            this.lblTieuDe.TabIndex = 0;
            this.lblTieuDe.Text = "Nhập danh sách nhà cung cấp từ file excel";
            // 
            // btnTaiFileMauExcel
            // 
            this.btnTaiFileMauExcel.Location = new System.Drawing.Point(28, 21);
            this.btnTaiFileMauExcel.Name = "btnTaiFileMauExcel";
            this.btnTaiFileMauExcel.Size = new System.Drawing.Size(160, 29);
            this.btnTaiFileMauExcel.TabIndex = 1;
            this.btnTaiFileMauExcel.Text = "Tải file mẫu Excel";
            this.btnTaiFileMauExcel.UseVisualStyleBackColor = true;
            this.btnTaiFileMauExcel.Click += new System.EventHandler(this.btnTaiFileMauExcel_Click);
            // 
            // grpChonFile
            // 
            this.grpChonFile.Controls.Add(this.btnChonFile);
            this.grpChonFile.Controls.Add(this.txtDuongDanFile);
            this.grpChonFile.Location = new System.Drawing.Point(28, 98);
            this.grpChonFile.Name = "grpChonFile";
            this.grpChonFile.Size = new System.Drawing.Size(835, 90);
            this.grpChonFile.TabIndex = 2;
            this.grpChonFile.TabStop = false;
            this.grpChonFile.Text = "1. Chọn file dữ liệu cần nhập";
            // 
            // btnChonFile
            // 
            this.btnChonFile.Location = new System.Drawing.Point(720, 38);
            this.btnChonFile.Name = "btnChonFile";
            this.btnChonFile.Size = new System.Drawing.Size(100, 29);
            this.btnChonFile.TabIndex = 1;
            this.btnChonFile.Text = "Chọn file...";
            this.btnChonFile.UseVisualStyleBackColor = true;
            this.btnChonFile.Click += new System.EventHandler(this.btnChonFile_Click);
            // 
            // txtDuongDanFile
            // 
            this.txtDuongDanFile.BackColor = System.Drawing.Color.White;
            this.txtDuongDanFile.Location = new System.Drawing.Point(15, 38);
            this.txtDuongDanFile.Name = "txtDuongDanFile";
            this.txtDuongDanFile.ReadOnly = true;
            this.txtDuongDanFile.Size = new System.Drawing.Size(687, 29);
            this.txtDuongDanFile.TabIndex = 0;
            // 
            // grpTuyChon
            // 
            this.grpTuyChon.Controls.Add(this.rdoBoQua);
            this.grpTuyChon.Controls.Add(this.rdoCapNhat);
            this.grpTuyChon.Location = new System.Drawing.Point(28, 204);
            this.grpTuyChon.Name = "grpTuyChon";
            this.grpTuyChon.Size = new System.Drawing.Size(835, 107);
            this.grpTuyChon.TabIndex = 3;
            this.grpTuyChon.TabStop = false;
            this.grpTuyChon.Text = "2. Tùy chọn xử lý khi trùng mã nhà cung cấp";
            // 
            // rdoBoQua
            // 
            this.rdoBoQua.AutoSize = true;
            this.rdoBoQua.Location = new System.Drawing.Point(15, 68);
            this.rdoBoQua.Name = "rdoBoQua";
            this.rdoBoQua.Size = new System.Drawing.Size(425, 25);
            this.rdoBoQua.TabIndex = 1;
            this.rdoBoQua.Text = "Bỏ qua (không cập nhật dòng trùng mã nhà cung cấp)";
            this.rdoBoQua.UseVisualStyleBackColor = true;
            // 
            // rdoCapNhat
            // 
            this.rdoCapNhat.AutoSize = true;
            this.rdoCapNhat.Checked = true;
            this.rdoCapNhat.Location = new System.Drawing.Point(15, 32);
            this.rdoCapNhat.Name = "rdoCapNhat";
            this.rdoCapNhat.Size = new System.Drawing.Size(437, 25);
            this.rdoCapNhat.TabIndex = 0;
            this.rdoCapNhat.TabStop = true;
            this.rdoCapNhat.Text = "Cập nhật thông tin nhà cung cấp nếu đã trùng mã";
            this.rdoCapNhat.UseVisualStyleBackColor = true;
            // 
            // prgTienTrinh
            // 
            this.prgTienTrinh.Location = new System.Drawing.Point(28, 332);
            this.prgTienTrinh.Name = "prgTienTrinh";
            this.prgTienTrinh.Size = new System.Drawing.Size(835, 15);
            this.prgTienTrinh.TabIndex = 4;
            this.prgTienTrinh.Visible = false;
            // 
            // btnThucHien
            // 
            this.btnThucHien.Location = new System.Drawing.Point(623, 21);
            this.btnThucHien.Name = "btnThucHien";
            this.btnThucHien.Size = new System.Drawing.Size(120, 29);
            this.btnThucHien.TabIndex = 5;
            this.btnThucHien.Text = "Thực hiện";
            this.btnThucHien.UseVisualStyleBackColor = true;
            this.btnThucHien.Click += new System.EventHandler(this.btnThucHien_Click);
            // 
            // btnDong
            // 
            this.btnDong.Location = new System.Drawing.Point(749, 21);
            this.btnDong.Name = "btnDong";
            this.btnDong.Size = new System.Drawing.Size(95, 29);
            this.btnDong.TabIndex = 6;
            this.btnDong.Text = "Đóng";
            this.btnDong.UseVisualStyleBackColor = true;
            this.btnDong.Click += new System.EventHandler(this.btnDong_Click);
            // 
            // pnlBottom
            // 
            this.pnlBottom.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(250)))), ((int)(((byte)(252)))));
            this.pnlBottom.Controls.Add(this.grpThaoTac);
            this.pnlBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnlBottom.Location = new System.Drawing.Point(0, 373);
            this.pnlBottom.Name = "pnlBottom";
            this.pnlBottom.Size = new System.Drawing.Size(882, 80);
            this.pnlBottom.TabIndex = 7;
            // 
            // grpThaoTac
            // 
            this.grpThaoTac.Controls.Add(this.btnTaiFileMauExcel);
            this.grpThaoTac.Controls.Add(this.btnDong);
            this.grpThaoTac.Controls.Add(this.btnThucHien);
            this.grpThaoTac.Location = new System.Drawing.Point(0, 10);
            this.grpThaoTac.Name = "grpThaoTac";
            this.grpThaoTac.Size = new System.Drawing.Size(882, 59);
            this.grpThaoTac.TabIndex = 0;
            this.grpThaoTac.TabStop = false;
            // 
            // frmNhapNhaCungCapFile
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 21F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(882, 453);
            this.Controls.Add(this.pnlBottom);
            this.Controls.Add(this.prgTienTrinh);
            this.Controls.Add(this.grpTuyChon);
            this.Controls.Add(this.grpChonFile);
            this.Controls.Add(this.pnlHeader);
            this.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.KeyPreview = true;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmNhapNhaCungCapFile";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Nhập danh sách nhà cung cấp từ file";
            this.pnlHeader.ResumeLayout(false);
            this.grpTieuDe.ResumeLayout(false);
            this.grpTieuDe.PerformLayout();
            this.grpChonFile.ResumeLayout(false);
            this.grpChonFile.PerformLayout();
            this.grpTuyChon.ResumeLayout(false);
            this.grpTuyChon.PerformLayout();
            this.pnlBottom.ResumeLayout(false);
            this.grpThaoTac.ResumeLayout(false);
            this.ResumeLayout(false);
        }

        #endregion

        private System.Windows.Forms.Panel pnlHeader;
        private System.Windows.Forms.GroupBox grpTieuDe;
        private System.Windows.Forms.Label lblTieuDe;
        private System.Windows.Forms.Button btnTaiFileMauExcel;
        private System.Windows.Forms.GroupBox grpChonFile;
        private System.Windows.Forms.Button btnChonFile;
        private System.Windows.Forms.TextBox txtDuongDanFile;
        private System.Windows.Forms.GroupBox grpTuyChon;
        private System.Windows.Forms.RadioButton rdoBoQua;
        private System.Windows.Forms.RadioButton rdoCapNhat;
        private System.Windows.Forms.ProgressBar prgTienTrinh;
        private System.Windows.Forms.Button btnThucHien;
        private System.Windows.Forms.Button btnDong;
        private System.Windows.Forms.Panel pnlBottom;
        private System.Windows.Forms.GroupBox grpThaoTac;
    }
}
