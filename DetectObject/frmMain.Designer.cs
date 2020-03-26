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
            this.components = new System.ComponentModel.Container();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label2 = new System.Windows.Forms.Label();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.lbDiVat = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.label5 = new System.Windows.Forms.Label();
            this.txtVanToc = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.tbMauGiay = new System.Windows.Forms.TrackBar();
            this.label3 = new System.Windows.Forms.Label();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.tlpCamera = new System.Windows.Forms.TableLayoutPanel();
            this.btnSangCuon = new System.Windows.Forms.Button();
            this.btnStop = new System.Windows.Forms.Button();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.btnTimKiem = new System.Windows.Forms.Button();
            this.cbCuon = new System.Windows.Forms.ComboBox();
            this.lbCuon = new System.Windows.Forms.Label();
            this.lbNgay = new System.Windows.Forms.Label();
            this.dtPickerNgayTimKiem = new System.Windows.Forms.DateTimePicker();
            this.btnGopLoi = new System.Windows.Forms.Button();
            this.btnXemAnh = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.dgvDSLoi = new System.Windows.Forms.DataGridView();
            this.Loi = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ThoiGianLoi = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ViTriLoi = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Cuon = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Image = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.btnBatdau = new System.Windows.Forms.Button();
            this.btnTai = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.tbMauGiay)).BeginInit();
            this.groupBox3.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvDSLoi)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.pictureBox1);
            this.groupBox1.Controls.Add(this.lbDiVat);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Location = new System.Drawing.Point(24, 9);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(379, 377);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Thông tin";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(21, 66);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(139, 17);
            this.label2.TabIndex = 3;
            this.label2.Text = "Ảnh dị vật đang quét";
            // 
            // pictureBox1
            // 
            this.pictureBox1.Location = new System.Drawing.Point(24, 97);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(334, 260);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox1.TabIndex = 2;
            this.pictureBox1.TabStop = false;
            // 
            // lbDiVat
            // 
            this.lbDiVat.AutoSize = true;
            this.lbDiVat.Location = new System.Drawing.Point(99, 32);
            this.lbDiVat.Name = "lbDiVat";
            this.lbDiVat.Size = new System.Drawing.Size(16, 17);
            this.lbDiVat.TabIndex = 1;
            this.lbDiVat.Text = "0";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(21, 32);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(44, 17);
            this.label1.TabIndex = 0;
            this.label1.Text = "Dị vật";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.label5);
            this.groupBox2.Controls.Add(this.txtVanToc);
            this.groupBox2.Controls.Add(this.label4);
            this.groupBox2.Controls.Add(this.tbMauGiay);
            this.groupBox2.Controls.Add(this.label3);
            this.groupBox2.Location = new System.Drawing.Point(435, 9);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(346, 120);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Setup";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(192, 79);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(30, 17);
            this.label5.TabIndex = 5;
            this.label5.Text = "m/s";
            // 
            // txtVanToc
            // 
            this.txtVanToc.Location = new System.Drawing.Point(96, 77);
            this.txtVanToc.Name = "txtVanToc";
            this.txtVanToc.Size = new System.Drawing.Size(90, 22);
            this.txtVanToc.TabIndex = 4;
            this.txtVanToc.Text = "0.5";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(12, 77);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(56, 17);
            this.label4.TabIndex = 3;
            this.label4.Text = "Vận tốc";
            // 
            // tbMauGiay
            // 
            this.tbMauGiay.Location = new System.Drawing.Point(83, 23);
            this.tbMauGiay.Maximum = 255;
            this.tbMauGiay.Name = "tbMauGiay";
            this.tbMauGiay.Size = new System.Drawing.Size(249, 56);
            this.tbMauGiay.TabIndex = 2;
            this.tbMauGiay.Value = 100;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 32);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(65, 17);
            this.label3.TabIndex = 1;
            this.label3.Text = "Màu giấy";
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.tlpCamera);
            this.groupBox3.Location = new System.Drawing.Point(435, 146);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(346, 240);
            this.groupBox3.TabIndex = 2;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Camera";
            // 
            // tlpCamera
            // 
            this.tlpCamera.ColumnCount = 1;
            this.tlpCamera.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpCamera.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tlpCamera.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlpCamera.Location = new System.Drawing.Point(3, 18);
            this.tlpCamera.Name = "tlpCamera";
            this.tlpCamera.RowCount = 1;
            this.tlpCamera.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpCamera.Size = new System.Drawing.Size(340, 219);
            this.tlpCamera.TabIndex = 0;
            // 
            // btnSangCuon
            // 
            this.btnSangCuon.Enabled = false;
            this.btnSangCuon.Location = new System.Drawing.Point(806, 162);
            this.btnSangCuon.Name = "btnSangCuon";
            this.btnSangCuon.Size = new System.Drawing.Size(96, 83);
            this.btnSangCuon.TabIndex = 3;
            this.btnSangCuon.Text = "Sang cuộn";
            this.btnSangCuon.UseVisualStyleBackColor = true;
            this.btnSangCuon.Click += new System.EventHandler(this.btnSangCuon_Click);
            // 
            // btnStop
            // 
            this.btnStop.Enabled = false;
            this.btnStop.Location = new System.Drawing.Point(806, 266);
            this.btnStop.Name = "btnStop";
            this.btnStop.Size = new System.Drawing.Size(96, 83);
            this.btnStop.TabIndex = 4;
            this.btnStop.Text = "Dừng";
            this.btnStop.UseVisualStyleBackColor = true;
            this.btnStop.Click += new System.EventHandler(this.btnStop_Click);
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.btnTai);
            this.groupBox4.Controls.Add(this.btnTimKiem);
            this.groupBox4.Controls.Add(this.cbCuon);
            this.groupBox4.Controls.Add(this.lbCuon);
            this.groupBox4.Controls.Add(this.lbNgay);
            this.groupBox4.Controls.Add(this.dtPickerNgayTimKiem);
            this.groupBox4.Controls.Add(this.btnGopLoi);
            this.groupBox4.Controls.Add(this.btnXemAnh);
            this.groupBox4.Controls.Add(this.panel1);
            this.groupBox4.Location = new System.Drawing.Point(24, 408);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(878, 258);
            this.groupBox4.TabIndex = 5;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "Danh sách lỗi";
            // 
            // btnTimKiem
            // 
            this.btnTimKiem.Location = new System.Drawing.Point(376, 19);
            this.btnTimKiem.Name = "btnTimKiem";
            this.btnTimKiem.Size = new System.Drawing.Size(96, 29);
            this.btnTimKiem.TabIndex = 11;
            this.btnTimKiem.Text = "Tìm kiếm";
            this.btnTimKiem.UseVisualStyleBackColor = true;
            this.btnTimKiem.Click += new System.EventHandler(this.btnTimKiem_Click);
            // 
            // cbCuon
            // 
            this.cbCuon.FormattingEnabled = true;
            this.cbCuon.Location = new System.Drawing.Point(272, 21);
            this.cbCuon.Name = "cbCuon";
            this.cbCuon.Size = new System.Drawing.Size(84, 24);
            this.cbCuon.TabIndex = 10;
            // 
            // lbCuon
            // 
            this.lbCuon.AutoSize = true;
            this.lbCuon.Location = new System.Drawing.Point(230, 24);
            this.lbCuon.Name = "lbCuon";
            this.lbCuon.Size = new System.Drawing.Size(41, 17);
            this.lbCuon.TabIndex = 9;
            this.lbCuon.Text = "Cuộn";
            // 
            // lbNgay
            // 
            this.lbNgay.AutoSize = true;
            this.lbNgay.Location = new System.Drawing.Point(13, 23);
            this.lbNgay.Name = "lbNgay";
            this.lbNgay.Size = new System.Drawing.Size(41, 17);
            this.lbNgay.TabIndex = 6;
            this.lbNgay.Text = "Ngày";
            // 
            // dtPickerNgayTimKiem
            // 
            this.dtPickerNgayTimKiem.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtPickerNgayTimKiem.Location = new System.Drawing.Point(60, 22);
            this.dtPickerNgayTimKiem.MinDate = new System.DateTime(2020, 1, 1, 0, 0, 0, 0);
            this.dtPickerNgayTimKiem.Name = "dtPickerNgayTimKiem";
            this.dtPickerNgayTimKiem.Size = new System.Drawing.Size(158, 22);
            this.dtPickerNgayTimKiem.TabIndex = 8;
            this.dtPickerNgayTimKiem.TabStop = false;
            this.dtPickerNgayTimKiem.ValueChanged += new System.EventHandler(this.dtPickerNgayTimKiem_ValueChanged);
            // 
            // btnGopLoi
            // 
            this.btnGopLoi.Location = new System.Drawing.Point(774, 19);
            this.btnGopLoi.Name = "btnGopLoi";
            this.btnGopLoi.Size = new System.Drawing.Size(96, 29);
            this.btnGopLoi.TabIndex = 7;
            this.btnGopLoi.Text = "Gộp lỗi";
            this.btnGopLoi.UseVisualStyleBackColor = true;
            this.btnGopLoi.Click += new System.EventHandler(this.btnGopLoi_Click);
            // 
            // btnXemAnh
            // 
            this.btnXemAnh.Location = new System.Drawing.Point(670, 19);
            this.btnXemAnh.Name = "btnXemAnh";
            this.btnXemAnh.Size = new System.Drawing.Size(96, 29);
            this.btnXemAnh.TabIndex = 6;
            this.btnXemAnh.Text = "Xem ảnh";
            this.btnXemAnh.UseVisualStyleBackColor = true;
            this.btnXemAnh.Click += new System.EventHandler(this.btnXemAnh_Click);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.dgvDSLoi);
            this.panel1.Location = new System.Drawing.Point(0, 58);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(878, 194);
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
            this.Cuon,
            this.Image});
            this.dgvDSLoi.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvDSLoi.Location = new System.Drawing.Point(0, 0);
            this.dgvDSLoi.Name = "dgvDSLoi";
            this.dgvDSLoi.ReadOnly = true;
            this.dgvDSLoi.RowHeadersWidth = 51;
            this.dgvDSLoi.RowTemplate.Height = 24;
            this.dgvDSLoi.Size = new System.Drawing.Size(878, 194);
            this.dgvDSLoi.TabIndex = 0;
            this.dgvDSLoi.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvDSLoi_CellClick);
            this.dgvDSLoi.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvDSLoi_CellDoubleClick);
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
            this.ThoiGianLoi.Width = 200;
            // 
            // ViTriLoi
            // 
            this.ViTriLoi.DataPropertyName = "ViTriLoi";
            this.ViTriLoi.HeaderText = "Vị Trí";
            this.ViTriLoi.MinimumWidth = 6;
            this.ViTriLoi.Name = "ViTriLoi";
            this.ViTriLoi.ReadOnly = true;
            this.ViTriLoi.Width = 150;
            // 
            // Cuon
            // 
            this.Cuon.DataPropertyName = "Cuon";
            this.Cuon.HeaderText = "Cuộn";
            this.Cuon.MinimumWidth = 6;
            this.Cuon.Name = "Cuon";
            this.Cuon.ReadOnly = true;
            this.Cuon.Width = 138;
            // 
            // Image
            // 
            this.Image.DataPropertyName = "Image";
            this.Image.HeaderText = "Image";
            this.Image.MinimumWidth = 6;
            this.Image.Name = "Image";
            this.Image.ReadOnly = true;
            this.Image.Visible = false;
            this.Image.Width = 125;
            // 
            // timer1
            // 
            this.timer1.Interval = 180000;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // btnBatdau
            // 
            this.btnBatdau.Location = new System.Drawing.Point(806, 53);
            this.btnBatdau.Name = "btnBatdau";
            this.btnBatdau.Size = new System.Drawing.Size(96, 83);
            this.btnBatdau.TabIndex = 6;
            this.btnBatdau.Text = "Bắt đầu";
            this.btnBatdau.UseVisualStyleBackColor = true;
            this.btnBatdau.Click += new System.EventHandler(this.btnBatdau_Click);
            // 
            // btnTai
            // 
            this.btnTai.Location = new System.Drawing.Point(566, 19);
            this.btnTai.Name = "btnTai";
            this.btnTai.Size = new System.Drawing.Size(96, 29);
            this.btnTai.TabIndex = 12;
            this.btnTai.Text = "Tải";
            this.btnTai.UseVisualStyleBackColor = true;
            this.btnTai.Click += new System.EventHandler(this.btnTai_Click);
            // 
            // frmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(923, 678);
            this.Controls.Add(this.btnBatdau);
            this.Controls.Add(this.groupBox4);
            this.Controls.Add(this.btnStop);
            this.Controls.Add(this.btnSangCuon);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Name = "frmMain";
            this.Text = "Phát hiện dị vật";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmMain_FormClosing);
            this.Shown += new System.EventHandler(this.frmMain_Shown);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.tbMauGiay)).EndInit();
            this.groupBox3.ResumeLayout(false);
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            this.panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvDSLoi)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Label lbDiVat;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.TextBox txtVanToc;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TrackBar tbMauGiay;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.TableLayoutPanel tlpCamera;
        private System.Windows.Forms.Button btnSangCuon;
        private System.Windows.Forms.Button btnStop;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.Button btnGopLoi;
        private System.Windows.Forms.Button btnXemAnh;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.DataGridView dgvDSLoi;
        protected System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.DataGridViewTextBoxColumn Loi;
        private System.Windows.Forms.DataGridViewTextBoxColumn ThoiGianLoi;
        private System.Windows.Forms.DataGridViewTextBoxColumn ViTriLoi;
        private System.Windows.Forms.DataGridViewTextBoxColumn Cuon;
        private System.Windows.Forms.DataGridViewTextBoxColumn Image;
        private System.Windows.Forms.Button btnBatdau;
        private System.Windows.Forms.Button btnTimKiem;
        private System.Windows.Forms.ComboBox cbCuon;
        private System.Windows.Forms.Label lbCuon;
        private System.Windows.Forms.Label lbNgay;
        private System.Windows.Forms.DateTimePicker dtPickerNgayTimKiem;
        private System.Windows.Forms.Button btnTai;
    }
}

