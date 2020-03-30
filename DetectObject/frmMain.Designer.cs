namespace DetectObject
{
    partial class frmMain
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
            this.lbVanToc = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.btnSangCuon = new System.Windows.Forms.Button();
            this.btnDung = new System.Windows.Forms.Button();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.btnTai = new System.Windows.Forms.Button();
            this.btnTimKiem = new System.Windows.Forms.Button();
            this.cbCuon = new System.Windows.Forms.ComboBox();
            this.lbCuon = new System.Windows.Forms.Label();
            this.lbNgay = new System.Windows.Forms.Label();
            this.dtPickerNgayTimKiem = new System.Windows.Forms.DateTimePicker();
            this.btnXoa = new System.Windows.Forms.Button();
            this.btnIn = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.dgvDSLoi = new System.Windows.Forms.DataGridView();
            this.Loi = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ThoiGianLoi = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ViTriLoi = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.TenCuon = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Image = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.btnQuet = new System.Windows.Forms.Button();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.lbDiVat = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.lbTenCuon = new System.Windows.Forms.Label();
            this.lbTrangThai = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.btnTamDung = new System.Windows.Forms.Button();
            this.groupBox4.SuspendLayout();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvDSLoi)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // lbVanToc
            // 
            this.lbVanToc.AutoSize = true;
            this.lbVanToc.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbVanToc.Location = new System.Drawing.Point(159, 131);
            this.lbVanToc.Name = "lbVanToc";
            this.lbVanToc.Size = new System.Drawing.Size(88, 25);
            this.lbVanToc.TabIndex = 5;
            this.lbVanToc.Text = "mét/phút";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(23, 131);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(73, 25);
            this.label4.TabIndex = 3;
            this.label4.Text = "Tốc độ";
            // 
            // btnSangCuon
            // 
            this.btnSangCuon.Enabled = false;
            this.btnSangCuon.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSangCuon.Location = new System.Drawing.Point(497, 258);
            this.btnSangCuon.Name = "btnSangCuon";
            this.btnSangCuon.Size = new System.Drawing.Size(136, 43);
            this.btnSangCuon.TabIndex = 3;
            this.btnSangCuon.Text = "Sang cuộn";
            this.btnSangCuon.UseVisualStyleBackColor = true;
            this.btnSangCuon.Click += new System.EventHandler(this.btnSangCuon_Click);
            // 
            // btnDung
            // 
            this.btnDung.Enabled = false;
            this.btnDung.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnDung.Location = new System.Drawing.Point(345, 258);
            this.btnDung.Name = "btnDung";
            this.btnDung.Size = new System.Drawing.Size(117, 43);
            this.btnDung.TabIndex = 4;
            this.btnDung.Text = "Dừng";
            this.btnDung.UseVisualStyleBackColor = true;
            this.btnDung.Click += new System.EventHandler(this.btnDung_Click);
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.btnTai);
            this.groupBox4.Controls.Add(this.btnTimKiem);
            this.groupBox4.Controls.Add(this.cbCuon);
            this.groupBox4.Controls.Add(this.lbCuon);
            this.groupBox4.Controls.Add(this.lbNgay);
            this.groupBox4.Controls.Add(this.dtPickerNgayTimKiem);
            this.groupBox4.Controls.Add(this.btnXoa);
            this.groupBox4.Controls.Add(this.btnIn);
            this.groupBox4.Controls.Add(this.panel1);
            this.groupBox4.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox4.Location = new System.Drawing.Point(28, 337);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(1211, 306);
            this.groupBox4.TabIndex = 5;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "Danh sách lỗi đã phát hiện";
            // 
            // btnTai
            // 
            this.btnTai.Location = new System.Drawing.Point(830, 31);
            this.btnTai.Name = "btnTai";
            this.btnTai.Size = new System.Drawing.Size(109, 41);
            this.btnTai.TabIndex = 12;
            this.btnTai.Text = "Tải";
            this.btnTai.UseVisualStyleBackColor = true;
            this.btnTai.Click += new System.EventHandler(this.btnTai_Click);
            // 
            // btnTimKiem
            // 
            this.btnTimKiem.Location = new System.Drawing.Point(482, 32);
            this.btnTimKiem.Name = "btnTimKiem";
            this.btnTimKiem.Size = new System.Drawing.Size(134, 41);
            this.btnTimKiem.TabIndex = 11;
            this.btnTimKiem.Text = "Tìm kiếm";
            this.btnTimKiem.UseVisualStyleBackColor = true;
            this.btnTimKiem.Click += new System.EventHandler(this.btnTimKiem_Click);
            // 
            // cbCuon
            // 
            this.cbCuon.FormattingEnabled = true;
            this.cbCuon.Location = new System.Drawing.Point(369, 36);
            this.cbCuon.Name = "cbCuon";
            this.cbCuon.Size = new System.Drawing.Size(84, 33);
            this.cbCuon.TabIndex = 10;
            // 
            // lbCuon
            // 
            this.lbCuon.AutoSize = true;
            this.lbCuon.Location = new System.Drawing.Point(293, 42);
            this.lbCuon.Name = "lbCuon";
            this.lbCuon.Size = new System.Drawing.Size(60, 25);
            this.lbCuon.TabIndex = 9;
            this.lbCuon.Text = "Cuộn";
            // 
            // lbNgay
            // 
            this.lbNgay.AutoSize = true;
            this.lbNgay.Location = new System.Drawing.Point(27, 42);
            this.lbNgay.Name = "lbNgay";
            this.lbNgay.Size = new System.Drawing.Size(58, 25);
            this.lbNgay.TabIndex = 6;
            this.lbNgay.Text = "Ngày";
            // 
            // dtPickerNgayTimKiem
            // 
            this.dtPickerNgayTimKiem.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtPickerNgayTimKiem.Location = new System.Drawing.Point(104, 39);
            this.dtPickerNgayTimKiem.MinDate = new System.DateTime(2020, 1, 1, 0, 0, 0, 0);
            this.dtPickerNgayTimKiem.Name = "dtPickerNgayTimKiem";
            this.dtPickerNgayTimKiem.Size = new System.Drawing.Size(158, 30);
            this.dtPickerNgayTimKiem.TabIndex = 8;
            this.dtPickerNgayTimKiem.TabStop = false;
            this.dtPickerNgayTimKiem.ValueChanged += new System.EventHandler(this.dtPickerNgayTimKiem_ValueChanged);
            // 
            // btnXoa
            // 
            this.btnXoa.Location = new System.Drawing.Point(959, 31);
            this.btnXoa.Name = "btnXoa";
            this.btnXoa.Size = new System.Drawing.Size(96, 41);
            this.btnXoa.TabIndex = 7;
            this.btnXoa.Text = "Xóa";
            this.btnXoa.UseVisualStyleBackColor = true;
            this.btnXoa.Click += new System.EventHandler(this.btnXoa_Click);
            // 
            // btnIn
            // 
            this.btnIn.Location = new System.Drawing.Point(1074, 31);
            this.btnIn.Name = "btnIn";
            this.btnIn.Size = new System.Drawing.Size(115, 41);
            this.btnIn.TabIndex = 6;
            this.btnIn.Text = "In";
            this.btnIn.UseVisualStyleBackColor = true;
            this.btnIn.Click += new System.EventHandler(this.btnIn_Click);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.dgvDSLoi);
            this.panel1.Location = new System.Drawing.Point(20, 82);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1169, 213);
            this.panel1.TabIndex = 0;
            // 
            // dgvDSLoi
            // 
            this.dgvDSLoi.AllowUserToAddRows = false;
            this.dgvDSLoi.AllowUserToDeleteRows = false;
            this.dgvDSLoi.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvDSLoi.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Loi,
            this.ThoiGianLoi,
            this.ViTriLoi,
            this.TenCuon,
            this.Image});
            this.dgvDSLoi.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvDSLoi.Location = new System.Drawing.Point(0, 0);
            this.dgvDSLoi.Name = "dgvDSLoi";
            this.dgvDSLoi.ReadOnly = true;
            this.dgvDSLoi.RowHeadersWidth = 51;
            this.dgvDSLoi.RowTemplate.Height = 24;
            this.dgvDSLoi.Size = new System.Drawing.Size(1169, 213);
            this.dgvDSLoi.TabIndex = 0;
            this.dgvDSLoi.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvDSLoi_CellClick);
            this.dgvDSLoi.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvDSLoi_CellDoubleClick);
            this.dgvDSLoi.SelectionChanged += new System.EventHandler(this.dgvDSLoi_SelectionChanged);
            // 
            // Loi
            // 
            this.Loi.DataPropertyName = "Loi";
            this.Loi.HeaderText = "Lỗi";
            this.Loi.MinimumWidth = 6;
            this.Loi.Name = "Loi";
            this.Loi.ReadOnly = true;
            this.Loi.Width = 125;
            // 
            // ThoiGianLoi
            // 
            this.ThoiGianLoi.DataPropertyName = "ThoiGianLoi";
            this.ThoiGianLoi.HeaderText = "Thời gian";
            this.ThoiGianLoi.MinimumWidth = 6;
            this.ThoiGianLoi.Name = "ThoiGianLoi";
            this.ThoiGianLoi.ReadOnly = true;
            this.ThoiGianLoi.Width = 250;
            // 
            // ViTriLoi
            // 
            this.ViTriLoi.DataPropertyName = "ViTriLoi";
            this.ViTriLoi.HeaderText = "Vị Trí";
            this.ViTriLoi.MinimumWidth = 6;
            this.ViTriLoi.Name = "ViTriLoi";
            this.ViTriLoi.ReadOnly = true;
            this.ViTriLoi.Width = 300;
            // 
            // TenCuon
            // 
            this.TenCuon.DataPropertyName = "TenCuon";
            this.TenCuon.HeaderText = "Cuộn";
            this.TenCuon.MinimumWidth = 6;
            this.TenCuon.Name = "TenCuon";
            this.TenCuon.ReadOnly = true;
            this.TenCuon.Width = 145;
            // 
            // Image
            // 
            this.Image.DataPropertyName = "ImagePath";
            this.Image.HeaderText = "Image";
            this.Image.MinimumWidth = 6;
            this.Image.Name = "Image";
            this.Image.ReadOnly = true;
            this.Image.Visible = false;
            this.Image.Width = 125;
            // 
            // btnQuet
            // 
            this.btnQuet.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnQuet.Location = new System.Drawing.Point(28, 258);
            this.btnQuet.Name = "btnQuet";
            this.btnQuet.Size = new System.Drawing.Size(124, 43);
            this.btnQuet.TabIndex = 6;
            this.btnQuet.Text = "Quét";
            this.btnQuet.UseVisualStyleBackColor = true;
            this.btnQuet.Click += new System.EventHandler(this.btnQuet_Click);
            // 
            // pictureBox1
            // 
            this.pictureBox1.Location = new System.Drawing.Point(720, 12);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(519, 297);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox1.TabIndex = 9;
            this.pictureBox1.TabStop = false;
            // 
            // lbDiVat
            // 
            this.lbDiVat.AutoSize = true;
            this.lbDiVat.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbDiVat.Location = new System.Drawing.Point(157, 181);
            this.lbDiVat.Name = "lbDiVat";
            this.lbDiVat.Size = new System.Drawing.Size(23, 25);
            this.lbDiVat.TabIndex = 8;
            this.lbDiVat.Text = "0";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(23, 181);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(94, 25);
            this.label1.TabIndex = 7;
            this.label1.Text = "Phát hiện";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(23, 27);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(86, 25);
            this.label2.TabIndex = 10;
            this.label2.Text = "Cuộn số";
            // 
            // lbTenCuon
            // 
            this.lbTenCuon.AutoSize = true;
            this.lbTenCuon.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbTenCuon.Location = new System.Drawing.Point(157, 27);
            this.lbTenCuon.Name = "lbTenCuon";
            this.lbTenCuon.Size = new System.Drawing.Size(23, 25);
            this.lbTenCuon.TabIndex = 11;
            this.lbTenCuon.Text = "1";
            // 
            // lbTrangThai
            // 
            this.lbTrangThai.AutoSize = true;
            this.lbTrangThai.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbTrangThai.Location = new System.Drawing.Point(157, 79);
            this.lbTrangThai.Name = "lbTrangThai";
            this.lbTrangThai.Size = new System.Drawing.Size(42, 25);
            this.lbTrangThai.TabIndex = 13;
            this.lbTrangThai.Text = "OK";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(23, 79);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(100, 25);
            this.label6.TabIndex = 12;
            this.label6.Text = "Trạng thái";
            // 
            // btnTamDung
            // 
            this.btnTamDung.Enabled = false;
            this.btnTamDung.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnTamDung.Location = new System.Drawing.Point(186, 258);
            this.btnTamDung.Name = "btnTamDung";
            this.btnTamDung.Size = new System.Drawing.Size(124, 43);
            this.btnTamDung.TabIndex = 14;
            this.btnTamDung.Text = "Tạm dừng";
            this.btnTamDung.UseVisualStyleBackColor = true;
            this.btnTamDung.Click += new System.EventHandler(this.btnTamDung_Click);
            // 
            // frmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1259, 653);
            this.Controls.Add(this.btnTamDung);
            this.Controls.Add(this.lbTrangThai);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.lbTenCuon);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.lbVanToc);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.lbDiVat);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnQuet);
            this.Controls.Add(this.groupBox4);
            this.Controls.Add(this.btnDung);
            this.Controls.Add(this.btnSangCuon);
            this.MaximizeBox = false;
            this.Name = "frmMain";
            this.Text = "Phát hiện dị vật";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmMain_FormClosing);
            this.Shown += new System.EventHandler(this.frmMain_Shown);
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            this.panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvDSLoi)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button btnSangCuon;
        private System.Windows.Forms.Button btnDung;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.Button btnXoa;
        private System.Windows.Forms.Button btnIn;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.DataGridView dgvDSLoi;
        private System.Windows.Forms.Label lbVanToc;
        private System.Windows.Forms.Button btnQuet;
        private System.Windows.Forms.Button btnTimKiem;
        private System.Windows.Forms.ComboBox cbCuon;
        private System.Windows.Forms.Label lbCuon;
        private System.Windows.Forms.Label lbNgay;
        private System.Windows.Forms.DateTimePicker dtPickerNgayTimKiem;
        private System.Windows.Forms.Button btnTai;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Label lbDiVat;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label lbTenCuon;
        private System.Windows.Forms.Label lbTrangThai;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Button btnTamDung;
        private System.Windows.Forms.DataGridViewTextBoxColumn Loi;
        private System.Windows.Forms.DataGridViewTextBoxColumn ThoiGianLoi;
        private System.Windows.Forms.DataGridViewTextBoxColumn ViTriLoi;
        private System.Windows.Forms.DataGridViewTextBoxColumn TenCuon;
        private System.Windows.Forms.DataGridViewTextBoxColumn Image;
    }
}

