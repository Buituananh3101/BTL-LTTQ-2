namespace BTL_LTTQ_2.GUI.Forms.DanhSachHangHoa
{
    partial class frmNhapHangHoaFile
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.pnlHeader = new System.Windows.Forms.Panel();
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
            this.panel1 = new System.Windows.Forms.Panel();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.label1 = new System.Windows.Forms.Label();
            this.pnlHeader.SuspendLayout();
            this.grpChonFile.SuspendLayout();
            this.grpTuyChon.SuspendLayout();
            this.panel1.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlHeader
            // 
            this.pnlHeader.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(250)))), ((int)(((byte)(252)))));
            this.pnlHeader.Controls.Add(this.groupBox2);
            this.pnlHeader.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlHeader.Location = new System.Drawing.Point(0, 0);
            this.pnlHeader.Name = "pnlHeader";
            this.pnlHeader.Size = new System.Drawing.Size(882, 80);
            this.pnlHeader.TabIndex = 0;
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
            this.grpTuyChon.Location = new System.Drawing.Point(28, 207);
            this.grpTuyChon.Name = "grpTuyChon";
            this.grpTuyChon.Size = new System.Drawing.Size(835, 121);
            this.grpTuyChon.TabIndex = 3;
            this.grpTuyChon.TabStop = false;
            this.grpTuyChon.Text = "2. Tùy chọn xử lý khi trùng mã hàng";
            // 
            // rdoBoQua
            // 
            this.rdoBoQua.AutoSize = true;
            this.rdoBoQua.Location = new System.Drawing.Point(15, 72);
            this.rdoBoQua.Name = "rdoBoQua";
            this.rdoBoQua.Size = new System.Drawing.Size(532, 25);
            this.rdoBoQua.TabIndex = 1;
            this.rdoBoQua.Text = "Bỏ qua không cập nhật nếu trùng mã hàng (Chỉ thêm các mặt hàng mới)";
            this.rdoBoQua.UseVisualStyleBackColor = true;
            // 
            // rdoCapNhat
            // 
            this.rdoCapNhat.AutoSize = true;
            this.rdoCapNhat.Checked = true;
            this.rdoCapNhat.Location = new System.Drawing.Point(15, 41);
            this.rdoCapNhat.Name = "rdoCapNhat";
            this.rdoCapNhat.Size = new System.Drawing.Size(534, 25);
            this.rdoCapNhat.TabIndex = 0;
            this.rdoCapNhat.TabStop = true;
            this.rdoCapNhat.Text = "Cập nhật (ghi đè) thông tin nếu trùng mã hàng (Phương án khuyến nghị)";
            this.rdoCapNhat.UseVisualStyleBackColor = true;
            // 
            // prgTienTrinh
            // 
            this.prgTienTrinh.Location = new System.Drawing.Point(28, 344);
            this.prgTienTrinh.Name = "prgTienTrinh";
            this.prgTienTrinh.Size = new System.Drawing.Size(835, 16);
            this.prgTienTrinh.TabIndex = 4;
            this.prgTienTrinh.Visible = false;
            // 
            // btnThucHien
            // 
            this.btnThucHien.Location = new System.Drawing.Point(635, 21);
            this.btnThucHien.Name = "btnThucHien";
            this.btnThucHien.Size = new System.Drawing.Size(130, 29);
            this.btnThucHien.TabIndex = 6;
            this.btnThucHien.Text = "Thực hiện nhập";
            this.btnThucHien.UseVisualStyleBackColor = true;
            this.btnThucHien.Click += new System.EventHandler(this.btnThucHien_Click);
            // 
            // btnDong
            // 
            this.btnDong.Location = new System.Drawing.Point(775, 21);
            this.btnDong.Name = "btnDong";
            this.btnDong.Size = new System.Drawing.Size(95, 29);
            this.btnDong.TabIndex = 7;
            this.btnDong.Text = "Đóng";
            this.btnDong.UseVisualStyleBackColor = true;
            this.btnDong.Click += new System.EventHandler(this.btnDong_Click);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.groupBox1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel1.Location = new System.Drawing.Point(0, 379);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(882, 74);
            this.panel1.TabIndex = 8;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.btnTaiFileMauExcel);
            this.groupBox1.Controls.Add(this.btnDong);
            this.groupBox1.Controls.Add(this.btnThucHien);
            this.groupBox1.Location = new System.Drawing.Point(0, 3);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(882, 59);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.label1);
            this.groupBox2.Location = new System.Drawing.Point(0, 12);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(882, 50);
            this.groupBox2.TabIndex = 0;
            this.groupBox2.TabStop = false;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.label1.Location = new System.Drawing.Point(10, 19);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(315, 23);
            this.label1.TabIndex = 0;
            this.label1.Text = "Nhập danh sách hàng hóa từ file excel";
            // 
            // frmNhapHangHoaFile
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 21F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(882, 453);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.prgTienTrinh);
            this.Controls.Add(this.grpTuyChon);
            this.Controls.Add(this.grpChonFile);
            this.Controls.Add(this.pnlHeader);
            this.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmNhapHangHoaFile";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Nhập danh sách hàng hóa từ file";
            this.pnlHeader.ResumeLayout(false);
            this.grpChonFile.ResumeLayout(false);
            this.grpChonFile.PerformLayout();
            this.grpTuyChon.ResumeLayout(false);
            this.grpTuyChon.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pnlHeader;
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
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label label1;
    }
}
