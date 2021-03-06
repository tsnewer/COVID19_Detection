﻿namespace COVID19_Detection
{
    partial class MainForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.btn_cam_config = new System.Windows.Forms.ToolStripButton();
            this.btn_start_savedata = new System.Windows.Forms.ToolStripButton();
            this.tableLayoutPanel = new System.Windows.Forms.TableLayoutPanel();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.tb_record_time = new System.Windows.Forms.ToolStripTextBox();
            this.label_minute = new System.Windows.Forms.ToolStripLabel();
            this.btn_record_time = new System.Windows.Forms.ToolStripButton();
            this.toolStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // toolStrip1
            // 
            this.toolStrip1.AutoSize = false;
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btn_cam_config,
            this.toolStripSeparator1,
            this.btn_start_savedata,
            this.toolStripSeparator2,
            this.btn_record_time,
            this.tb_record_time,
            this.label_minute});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(800, 36);
            this.toolStrip1.TabIndex = 3;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // btn_cam_config
            // 
            this.btn_cam_config.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.btn_cam_config.Font = new System.Drawing.Font("Microsoft YaHei UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btn_cam_config.Image = ((System.Drawing.Image)(resources.GetObject("btn_cam_config.Image")));
            this.btn_cam_config.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btn_cam_config.Name = "btn_cam_config";
            this.btn_cam_config.Size = new System.Drawing.Size(78, 33);
            this.btn_cam_config.Text = "相机设置";
            this.btn_cam_config.Click += new System.EventHandler(this.btn_cam_config_Click);
            // 
            // btn_start_savedata
            // 
            this.btn_start_savedata.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.btn_start_savedata.Font = new System.Drawing.Font("Microsoft YaHei UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btn_start_savedata.Image = ((System.Drawing.Image)(resources.GetObject("btn_start_savedata.Image")));
            this.btn_start_savedata.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btn_start_savedata.Name = "btn_start_savedata";
            this.btn_start_savedata.Size = new System.Drawing.Size(78, 33);
            this.btn_start_savedata.Text = "开始存储";
            this.btn_start_savedata.Click += new System.EventHandler(this.btn_start_savedata_Click);
            // 
            // tableLayoutPanel
            // 
            this.tableLayoutPanel.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tableLayoutPanel.BackColor = System.Drawing.SystemColors.Control;
            this.tableLayoutPanel.CellBorderStyle = System.Windows.Forms.TableLayoutPanelCellBorderStyle.Outset;
            this.tableLayoutPanel.ColumnCount = 1;
            this.tableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel.Location = new System.Drawing.Point(0, 33);
            this.tableLayoutPanel.Name = "tableLayoutPanel";
            this.tableLayoutPanel.RowCount = 1;
            this.tableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel.Size = new System.Drawing.Size(800, 417);
            this.tableLayoutPanel.TabIndex = 4;
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 36);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 36);
            // 
            // tb_record_time
            // 
            this.tb_record_time.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.tb_record_time.Font = new System.Drawing.Font("Microsoft YaHei UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.tb_record_time.Name = "tb_record_time";
            this.tb_record_time.Size = new System.Drawing.Size(100, 36);
            // 
            // label_minute
            // 
            this.label_minute.Font = new System.Drawing.Font("Microsoft YaHei UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label_minute.Name = "label_minute";
            this.label_minute.Size = new System.Drawing.Size(42, 33);
            this.label_minute.Text = "分钟";
            // 
            // btn_record_time
            // 
            this.btn_record_time.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.btn_record_time.Font = new System.Drawing.Font("Microsoft YaHei UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btn_record_time.Image = ((System.Drawing.Image)(resources.GetObject("btn_record_time.Image")));
            this.btn_record_time.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btn_record_time.Name = "btn_record_time";
            this.btn_record_time.Size = new System.Drawing.Size(78, 33);
            this.btn_record_time.Text = "设置时间";
            this.btn_record_time.Click += new System.EventHandler(this.btn_record_time_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.tableLayoutPanel);
            this.Controls.Add(this.toolStrip1);
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "COVID19_Detection";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.MainForm_FormClosed);
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton btn_cam_config;
        public System.Windows.Forms.TableLayoutPanel tableLayoutPanel;
        private System.Windows.Forms.ToolStripButton btn_start_savedata;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripTextBox tb_record_time;
        private System.Windows.Forms.ToolStripLabel label_minute;
        private System.Windows.Forms.ToolStripButton btn_record_time;
    }
}

