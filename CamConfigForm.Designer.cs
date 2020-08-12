namespace COVID19_Detection
{
    partial class CamConfigForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CamConfigForm));
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea1 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend1 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series1 = new System.Windows.Forms.DataVisualization.Charting.Series();
            this.panel1 = new System.Windows.Forms.Panel();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.CameraRead = new System.Windows.Forms.Button();
            this.CameraSave = new System.Windows.Forms.Button();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.check_top_auto = new System.Windows.Forms.CheckBox();
            this.check_tube = new System.Windows.Forms.CheckBox();
            this.V_Pixel = new System.Windows.Forms.NumericUpDown();
            this.label13 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.Tube_Num = new System.Windows.Forms.NumericUpDown();
            this.H_num = new System.Windows.Forms.NumericUpDown();
            this.label11 = new System.Windows.Forms.Label();
            this.numericUpDown1 = new System.Windows.Forms.NumericUpDown();
            this.label10 = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.ROI_Set = new System.Windows.Forms.Button();
            this.checkBox_Roi = new System.Windows.Forms.CheckBox();
            this.ROI_Height = new System.Windows.Forms.TextBox();
            this.ROI_Width = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.V_offset = new System.Windows.Forms.TextBox();
            this.H_offset = new System.Windows.Forms.TextBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.PicNum = new System.Windows.Forms.NumericUpDown();
            this.label3 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.textBox_Expo = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.toolStripButton1 = new System.Windows.Forms.ToolStripButton();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.ShowpictureBoxCamera = new System.Windows.Forms.PictureBox();
            this.chart1 = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.panel1.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.groupBox3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.V_Pixel)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Tube_Num)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.H_num)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown1)).BeginInit();
            this.groupBox2.SuspendLayout();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.PicNum)).BeginInit();
            this.toolStrip1.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ShowpictureBoxCamera)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chart1)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.SystemColors.ControlDark;
            this.panel1.Controls.Add(this.groupBox4);
            this.panel1.Controls.Add(this.groupBox3);
            this.panel1.Controls.Add(this.groupBox2);
            this.panel1.Controls.Add(this.groupBox1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Right;
            this.panel1.Location = new System.Drawing.Point(727, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(221, 674);
            this.panel1.TabIndex = 0;
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.CameraRead);
            this.groupBox4.Controls.Add(this.CameraSave);
            this.groupBox4.Location = new System.Drawing.Point(19, 522);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(159, 54);
            this.groupBox4.TabIndex = 3;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "设置存储与读取";
            // 
            // CameraRead
            // 
            this.CameraRead.Location = new System.Drawing.Point(76, 20);
            this.CameraRead.Name = "CameraRead";
            this.CameraRead.Size = new System.Drawing.Size(53, 28);
            this.CameraRead.TabIndex = 0;
            this.CameraRead.Text = "读取";
            this.CameraRead.UseVisualStyleBackColor = true;
            this.CameraRead.Click += new System.EventHandler(this.CameraRead_Click);
            // 
            // CameraSave
            // 
            this.CameraSave.Location = new System.Drawing.Point(13, 20);
            this.CameraSave.Name = "CameraSave";
            this.CameraSave.Size = new System.Drawing.Size(53, 28);
            this.CameraSave.TabIndex = 0;
            this.CameraSave.Text = "存储";
            this.CameraSave.UseVisualStyleBackColor = true;
            this.CameraSave.Click += new System.EventHandler(this.CameraSave_Click);
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.check_top_auto);
            this.groupBox3.Controls.Add(this.check_tube);
            this.groupBox3.Controls.Add(this.V_Pixel);
            this.groupBox3.Controls.Add(this.label13);
            this.groupBox3.Controls.Add(this.label12);
            this.groupBox3.Controls.Add(this.Tube_Num);
            this.groupBox3.Controls.Add(this.H_num);
            this.groupBox3.Controls.Add(this.label11);
            this.groupBox3.Controls.Add(this.numericUpDown1);
            this.groupBox3.Controls.Add(this.label10);
            this.groupBox3.Location = new System.Drawing.Point(19, 359);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(163, 129);
            this.groupBox3.TabIndex = 2;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "毛细管";
            // 
            // check_top_auto
            // 
            this.check_top_auto.AutoSize = true;
            this.check_top_auto.Location = new System.Drawing.Point(79, 18);
            this.check_top_auto.Name = "check_top_auto";
            this.check_top_auto.Size = new System.Drawing.Size(48, 16);
            this.check_top_auto.TabIndex = 7;
            this.check_top_auto.Text = "auto";
            this.check_top_auto.UseVisualStyleBackColor = true;
            this.check_top_auto.CheckedChanged += new System.EventHandler(this.check_tube_CheckedChanged);
            // 
            // check_tube
            // 
            this.check_tube.AutoSize = true;
            this.check_tube.Location = new System.Drawing.Point(19, 18);
            this.check_tube.Name = "check_tube";
            this.check_tube.Size = new System.Drawing.Size(54, 16);
            this.check_tube.TabIndex = 7;
            this.check_tube.Text = "check";
            this.check_tube.UseVisualStyleBackColor = true;
            this.check_tube.CheckedChanged += new System.EventHandler(this.check_tube_CheckedChanged);
            // 
            // V_Pixel
            // 
            this.V_Pixel.Location = new System.Drawing.Point(79, 71);
            this.V_Pixel.Name = "V_Pixel";
            this.V_Pixel.Size = new System.Drawing.Size(51, 21);
            this.V_Pixel.TabIndex = 6;
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(14, 100);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(53, 12);
            this.label13.TabIndex = 5;
            this.label13.Text = "垂直等分";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(14, 73);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(53, 12);
            this.label12.TabIndex = 5;
            this.label12.Text = "水平扩展";
            // 
            // Tube_Num
            // 
            this.Tube_Num.Location = new System.Drawing.Point(79, 44);
            this.Tube_Num.Name = "Tube_Num";
            this.Tube_Num.Size = new System.Drawing.Size(51, 21);
            this.Tube_Num.TabIndex = 6;
            this.Tube_Num.ValueChanged += new System.EventHandler(this.Tube_Num_ValueChanged);
            // 
            // H_num
            // 
            this.H_num.Location = new System.Drawing.Point(79, 98);
            this.H_num.Name = "H_num";
            this.H_num.Size = new System.Drawing.Size(51, 21);
            this.H_num.TabIndex = 6;
            this.H_num.ValueChanged += new System.EventHandler(this.H_num_ValueChanged);
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(14, 46);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(53, 12);
            this.label11.TabIndex = 5;
            this.label11.Text = "毛细管数";
            // 
            // numericUpDown1
            // 
            this.numericUpDown1.Location = new System.Drawing.Point(79, 71);
            this.numericUpDown1.Name = "numericUpDown1";
            this.numericUpDown1.Size = new System.Drawing.Size(51, 21);
            this.numericUpDown1.TabIndex = 6;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(14, 73);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(53, 12);
            this.label10.TabIndex = 5;
            this.label10.Text = "平均张数";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.ROI_Set);
            this.groupBox2.Controls.Add(this.checkBox_Roi);
            this.groupBox2.Controls.Add(this.ROI_Height);
            this.groupBox2.Controls.Add(this.ROI_Width);
            this.groupBox2.Controls.Add(this.label7);
            this.groupBox2.Controls.Add(this.label6);
            this.groupBox2.Controls.Add(this.label5);
            this.groupBox2.Controls.Add(this.label4);
            this.groupBox2.Controls.Add(this.V_offset);
            this.groupBox2.Controls.Add(this.H_offset);
            this.groupBox2.Location = new System.Drawing.Point(18, 167);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(165, 166);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "ROI设置";
            // 
            // ROI_Set
            // 
            this.ROI_Set.Location = new System.Drawing.Point(80, 13);
            this.ROI_Set.Name = "ROI_Set";
            this.ROI_Set.Size = new System.Drawing.Size(50, 21);
            this.ROI_Set.TabIndex = 7;
            this.ROI_Set.Text = "Set";
            this.ROI_Set.UseVisualStyleBackColor = true;
            this.ROI_Set.Click += new System.EventHandler(this.ROI_Set_Click);
            // 
            // checkBox_Roi
            // 
            this.checkBox_Roi.AutoSize = true;
            this.checkBox_Roi.Location = new System.Drawing.Point(17, 20);
            this.checkBox_Roi.Name = "checkBox_Roi";
            this.checkBox_Roi.Size = new System.Drawing.Size(42, 16);
            this.checkBox_Roi.TabIndex = 6;
            this.checkBox_Roi.Text = "ROI";
            this.checkBox_Roi.UseVisualStyleBackColor = true;
            this.checkBox_Roi.CheckedChanged += new System.EventHandler(this.checkBox_Roi_CheckedChanged);
            // 
            // ROI_Height
            // 
            this.ROI_Height.Location = new System.Drawing.Point(80, 133);
            this.ROI_Height.Name = "ROI_Height";
            this.ROI_Height.Size = new System.Drawing.Size(50, 21);
            this.ROI_Height.TabIndex = 5;
            // 
            // ROI_Width
            // 
            this.ROI_Width.Location = new System.Drawing.Point(80, 103);
            this.ROI_Width.Name = "ROI_Width";
            this.ROI_Width.Size = new System.Drawing.Size(50, 21);
            this.ROI_Width.TabIndex = 5;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(15, 138);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(41, 12);
            this.label7.TabIndex = 4;
            this.label7.Text = "Height";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(15, 108);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(35, 12);
            this.label6.TabIndex = 3;
            this.label6.Text = "Width";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(15, 78);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(53, 12);
            this.label5.TabIndex = 2;
            this.label5.Text = "V Offset";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(15, 48);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(53, 12);
            this.label4.TabIndex = 2;
            this.label4.Text = "H Offset";
            // 
            // V_offset
            // 
            this.V_offset.Location = new System.Drawing.Point(80, 73);
            this.V_offset.Name = "V_offset";
            this.V_offset.Size = new System.Drawing.Size(50, 21);
            this.V_offset.TabIndex = 1;
            // 
            // H_offset
            // 
            this.H_offset.Location = new System.Drawing.Point(80, 43);
            this.H_offset.Name = "H_offset";
            this.H_offset.Size = new System.Drawing.Size(50, 21);
            this.H_offset.TabIndex = 0;
            this.H_offset.TextChanged += new System.EventHandler(this.H_offset_TextChanged);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.PicNum);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.label9);
            this.groupBox1.Controls.Add(this.label8);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.textBox_Expo);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Location = new System.Drawing.Point(22, 43);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(161, 83);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "曝光与平均张数";
            // 
            // PicNum
            // 
            this.PicNum.Location = new System.Drawing.Point(75, 48);
            this.PicNum.Name = "PicNum";
            this.PicNum.Size = new System.Drawing.Size(51, 21);
            this.PicNum.TabIndex = 4;
            this.PicNum.Tag = "";
            this.PicNum.ValueChanged += new System.EventHandler(this.numericUpDown1_ValueChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(10, 50);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(53, 12);
            this.label3.TabIndex = 3;
            this.label3.Text = "平均张数";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(132, 50);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(17, 12);
            this.label9.TabIndex = 2;
            this.label9.Text = "幅";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(132, 23);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(17, 12);
            this.label8.TabIndex = 2;
            this.label8.Text = "ms";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(135, 25);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(17, 12);
            this.label2.TabIndex = 2;
            this.label2.Text = "ms";
            // 
            // textBox_Expo
            // 
            this.textBox_Expo.Location = new System.Drawing.Point(75, 21);
            this.textBox_Expo.Name = "textBox_Expo";
            this.textBox_Expo.Size = new System.Drawing.Size(50, 21);
            this.textBox_Expo.TabIndex = 1;
            this.textBox_Expo.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.textBox_Expo.TextChanged += new System.EventHandler(this.textBox_Expo_TextChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(10, 23);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(53, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "曝光时间";
            // 
            // toolStrip1
            // 
            this.toolStrip1.AutoSize = false;
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripButton1});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(727, 40);
            this.toolStrip1.TabIndex = 1;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // toolStripButton1
            // 
            this.toolStripButton1.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.toolStripButton1.Font = new System.Drawing.Font("Microsoft YaHei UI", 12F);
            this.toolStripButton1.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton1.Image")));
            this.toolStripButton1.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton1.Name = "toolStripButton1";
            this.toolStripButton1.Size = new System.Drawing.Size(140, 37);
            this.toolStripButton1.Text = "toolStripButton1";
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tableLayoutPanel1.BackColor = System.Drawing.SystemColors.Control;
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.Controls.Add(this.ShowpictureBoxCamera, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.chart1, 0, 0);
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 43);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(727, 631);
            this.tableLayoutPanel1.TabIndex = 2;
            // 
            // ShowpictureBoxCamera
            // 
            this.ShowpictureBoxCamera.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.ShowpictureBoxCamera.BackColor = System.Drawing.SystemColors.ControlDarkDark;
            this.ShowpictureBoxCamera.Location = new System.Drawing.Point(3, 318);
            this.ShowpictureBoxCamera.Name = "ShowpictureBoxCamera";
            this.ShowpictureBoxCamera.Size = new System.Drawing.Size(721, 310);
            this.ShowpictureBoxCamera.TabIndex = 0;
            this.ShowpictureBoxCamera.TabStop = false;
            this.ShowpictureBoxCamera.Click += new System.EventHandler(this.ShowpictureBox_Click);
            this.ShowpictureBoxCamera.Paint += new System.Windows.Forms.PaintEventHandler(this.ShowpictureBoxCamera_Paint);
            this.ShowpictureBoxCamera.MouseDown += new System.Windows.Forms.MouseEventHandler(this.ShowpictureBoxCamera_MouseDown);
            this.ShowpictureBoxCamera.MouseMove += new System.Windows.Forms.MouseEventHandler(this.ShowpictureBoxCamera_MouseMove);
            this.ShowpictureBoxCamera.MouseUp += new System.Windows.Forms.MouseEventHandler(this.ShowpictureBoxCamera_MouseUp);
            this.ShowpictureBoxCamera.Resize += new System.EventHandler(this.ShowpictureBoxCamera_Resize);
            // 
            // chart1
            // 
            this.chart1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            chartArea1.Name = "ChartArea1";
            this.chart1.ChartAreas.Add(chartArea1);
            legend1.Name = "Legend1";
            this.chart1.Legends.Add(legend1);
            this.chart1.Location = new System.Drawing.Point(3, 3);
            this.chart1.Name = "chart1";
            series1.ChartArea = "ChartArea1";
            series1.Legend = "Legend1";
            series1.Name = "Series1";
            this.chart1.Series.Add(series1);
            this.chart1.Size = new System.Drawing.Size(721, 309);
            this.chart1.TabIndex = 1;
            this.chart1.Text = "chart1";
            this.chart1.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.chart1_MouseDoubleClick);
            this.chart1.MouseDown += new System.Windows.Forms.MouseEventHandler(this.chart1_MouseDown);
            this.chart1.MouseMove += new System.Windows.Forms.MouseEventHandler(this.chart1_MouseMove);
            this.chart1.MouseUp += new System.Windows.Forms.MouseEventHandler(this.chart1_MouseUp);
            // 
            // CamConfigForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(948, 674);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Controls.Add(this.toolStrip1);
            this.Controls.Add(this.panel1);
            this.Name = "CamConfigForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "相机设置";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.CamConfigForm_FormClosed);
            this.Load += new System.EventHandler(this.CamConfigForm_Load);
            this.panel1.ResumeLayout(false);
            this.groupBox4.ResumeLayout(false);
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.V_Pixel)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Tube_Num)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.H_num)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown1)).EndInit();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.PicNum)).EndInit();
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.tableLayoutPanel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.ShowpictureBoxCamera)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chart1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton toolStripButton1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.PictureBox ShowpictureBoxCamera;
        private System.Windows.Forms.DataVisualization.Charting.Chart chart1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox textBox_Expo;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.TextBox ROI_Height;
        private System.Windows.Forms.TextBox ROI_Width;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox V_offset;
        private System.Windows.Forms.TextBox H_offset;
        private System.Windows.Forms.CheckBox checkBox_Roi;
        private System.Windows.Forms.Button ROI_Set;
        private System.Windows.Forms.NumericUpDown PicNum;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.NumericUpDown V_Pixel;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.NumericUpDown Tube_Num;
        private System.Windows.Forms.NumericUpDown H_num;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.NumericUpDown numericUpDown1;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.CheckBox check_tube;
        private System.Windows.Forms.CheckBox check_top_auto;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.Button CameraRead;
        private System.Windows.Forms.Button CameraSave;
    }
}