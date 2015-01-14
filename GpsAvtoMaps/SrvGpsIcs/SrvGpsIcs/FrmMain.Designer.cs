namespace SrvGpsIcs
{
    partial class FrmMain
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmMain));
            this.notifyIconMain = new System.Windows.Forms.NotifyIcon(this.components);
            this.cntMenuStripMain = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.ToolStripMenuItem_Close = new System.Windows.Forms.ToolStripMenuItem();
            this.c1TxtBoxPortGprs = new C1.Win.C1Input.C1TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.c1TxtBoxPortLan = new C1.Win.C1Input.C1TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.c1ListClientLan = new C1.Win.C1List.C1List();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.c1ListClientGprs = new C1.Win.C1List.C1List();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.c1BtnStop = new C1.Win.C1Input.C1Button();
            this.c1BtnStart = new C1.Win.C1Input.C1Button();
            this.richTextBoxReceivedMsg = new System.Windows.Forms.RichTextBox();
            this.c1CheckBoxMsg = new C1.Win.C1Input.C1CheckBox();
            this.c1BtnTest = new C1.Win.C1Input.C1Button();
            this.toolStripMaim = new System.Windows.Forms.ToolStrip();
            this.tlStripBtnSql = new System.Windows.Forms.ToolStripButton();
            this.tlStripLblConSql = new System.Windows.Forms.ToolStripLabel();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.tlStripLblTime = new System.Windows.Forms.ToolStripLabel();
            this.cntMenuStripMain.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.c1TxtBoxPortGprs)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.c1TxtBoxPortLan)).BeginInit();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.c1ListClientLan)).BeginInit();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.c1ListClientGprs)).BeginInit();
            this.groupBox3.SuspendLayout();
            this.toolStripMaim.SuspendLayout();
            this.SuspendLayout();
            // 
            // notifyIconMain
            // 
            this.notifyIconMain.BalloonTipIcon = System.Windows.Forms.ToolTipIcon.Info;
            this.notifyIconMain.BalloonTipText = "Сервер GPRS-LAN";
            this.notifyIconMain.BalloonTipTitle = "Мониторинг транспорта";
            this.notifyIconMain.ContextMenuStrip = this.cntMenuStripMain;
            this.notifyIconMain.Icon = ((System.Drawing.Icon)(resources.GetObject("notifyIconMain.Icon")));
            this.notifyIconMain.Text = "Сервер GPRS-LAN";
            this.notifyIconMain.Visible = true;
            this.notifyIconMain.MouseClick += new System.Windows.Forms.MouseEventHandler(this.notifyIconMain_MouseClick);
            // 
            // cntMenuStripMain
            // 
            this.cntMenuStripMain.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ToolStripMenuItem_Close});
            this.cntMenuStripMain.Name = "cntMenuStripMain";
            this.cntMenuStripMain.Size = new System.Drawing.Size(109, 26);
            // 
            // ToolStripMenuItem_Close
            // 
            this.ToolStripMenuItem_Close.Image = ((System.Drawing.Image)(resources.GetObject("ToolStripMenuItem_Close.Image")));
            this.ToolStripMenuItem_Close.Name = "ToolStripMenuItem_Close";
            this.ToolStripMenuItem_Close.Size = new System.Drawing.Size(108, 22);
            this.ToolStripMenuItem_Close.Text = "Выход";
            this.ToolStripMenuItem_Close.Click += new System.EventHandler(this.ToolStripMenuItem_Close_Click);
            // 
            // c1TxtBoxPortGprs
            // 
            this.c1TxtBoxPortGprs.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(177)))), ((int)(((byte)(187)))), ((int)(((byte)(198)))));
            this.c1TxtBoxPortGprs.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.c1TxtBoxPortGprs.Location = new System.Drawing.Point(132, 21);
            this.c1TxtBoxPortGprs.Name = "c1TxtBoxPortGprs";
            this.c1TxtBoxPortGprs.Size = new System.Drawing.Size(37, 18);
            this.c1TxtBoxPortGprs.TabIndex = 1;
            this.c1TxtBoxPortGprs.Tag = null;
            this.c1TxtBoxPortGprs.VisualStyle = C1.Win.C1Input.VisualStyle.Office2010Silver;
            this.c1TxtBoxPortGprs.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.Office2010Silver;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(89, 23);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(37, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "GPRS";
            // 
            // c1TxtBoxPortLan
            // 
            this.c1TxtBoxPortLan.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(177)))), ((int)(((byte)(187)))), ((int)(((byte)(198)))));
            this.c1TxtBoxPortLan.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.c1TxtBoxPortLan.Location = new System.Drawing.Point(40, 21);
            this.c1TxtBoxPortLan.Name = "c1TxtBoxPortLan";
            this.c1TxtBoxPortLan.Size = new System.Drawing.Size(37, 18);
            this.c1TxtBoxPortLan.TabIndex = 3;
            this.c1TxtBoxPortLan.Tag = null;
            this.c1TxtBoxPortLan.VisualStyle = C1.Win.C1Input.VisualStyle.Office2010Silver;
            this.c1TxtBoxPortLan.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.Office2010Silver;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(6, 23);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(28, 13);
            this.label2.TabIndex = 4;
            this.label2.Text = "LAN";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.c1ListClientLan);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(180, 219);
            this.groupBox1.TabIndex = 5;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Локальная сеть LAN";
            // 
            // c1ListClientLan
            // 
            this.c1ListClientLan.AddItemSeparator = ';';
            this.c1ListClientLan.Caption = "Клиенты";
            this.c1ListClientLan.CaptionHeight = 17;
            this.c1ListClientLan.ColumnCaptionHeight = 17;
            this.c1ListClientLan.ColumnFooterHeight = 17;
            this.c1ListClientLan.ColumnHeaders = false;
            this.c1ListClientLan.DataMode = C1.Win.C1List.DataModeEnum.AddItem;
            this.c1ListClientLan.DeadAreaBackColor = System.Drawing.SystemColors.ControlDark;
            this.c1ListClientLan.ExtendRightColumn = true;
            this.c1ListClientLan.Images.Add(((System.Drawing.Image)(resources.GetObject("c1ListClientLan.Images"))));
            this.c1ListClientLan.ItemHeight = 15;
            this.c1ListClientLan.Location = new System.Drawing.Point(13, 19);
            this.c1ListClientLan.MatchEntryTimeout = ((long)(2000));
            this.c1ListClientLan.Name = "c1ListClientLan";
            this.c1ListClientLan.RowDivider.Style = C1.Win.C1List.LineStyleEnum.None;
            this.c1ListClientLan.RowSubDividerColor = System.Drawing.Color.DarkGray;
            this.c1ListClientLan.Size = new System.Drawing.Size(150, 186);
            this.c1ListClientLan.TabIndex = 6;
            this.c1ListClientLan.VisualStyle = C1.Win.C1List.VisualStyle.Office2007Silver;
            this.c1ListClientLan.PropBag = resources.GetString("c1ListClientLan.PropBag");
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.c1ListClientGprs);
            this.groupBox2.Location = new System.Drawing.Point(198, 12);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(180, 219);
            this.groupBox2.TabIndex = 6;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Сотовая сеть GPRS";
            // 
            // c1ListClientGprs
            // 
            this.c1ListClientGprs.AddItemSeparator = ';';
            this.c1ListClientGprs.Caption = "Клиенты";
            this.c1ListClientGprs.CaptionHeight = 17;
            this.c1ListClientGprs.ColumnCaptionHeight = 17;
            this.c1ListClientGprs.ColumnFooterHeight = 17;
            this.c1ListClientGprs.ColumnHeaders = false;
            this.c1ListClientGprs.DataMode = C1.Win.C1List.DataModeEnum.AddItem;
            this.c1ListClientGprs.DeadAreaBackColor = System.Drawing.SystemColors.ControlDark;
            this.c1ListClientGprs.ExtendRightColumn = true;
            this.c1ListClientGprs.Images.Add(((System.Drawing.Image)(resources.GetObject("c1ListClientGprs.Images"))));
            this.c1ListClientGprs.ItemHeight = 15;
            this.c1ListClientGprs.Location = new System.Drawing.Point(15, 19);
            this.c1ListClientGprs.MatchEntryTimeout = ((long)(2000));
            this.c1ListClientGprs.Name = "c1ListClientGprs";
            this.c1ListClientGprs.RowDivider.Style = C1.Win.C1List.LineStyleEnum.None;
            this.c1ListClientGprs.RowSubDividerColor = System.Drawing.Color.DarkGray;
            this.c1ListClientGprs.Size = new System.Drawing.Size(150, 186);
            this.c1ListClientGprs.TabIndex = 0;
            this.c1ListClientGprs.Text = "c1List1";
            this.c1ListClientGprs.VisualStyle = C1.Win.C1List.VisualStyle.Office2007Silver;
            this.c1ListClientGprs.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.c1ListClientGprs_MouseDoubleClick);
            this.c1ListClientGprs.PropBag = resources.GetString("c1ListClientGprs.PropBag");
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.c1BtnStop);
            this.groupBox3.Controls.Add(this.label1);
            this.groupBox3.Controls.Add(this.c1TxtBoxPortGprs);
            this.groupBox3.Controls.Add(this.label2);
            this.groupBox3.Controls.Add(this.c1BtnStart);
            this.groupBox3.Controls.Add(this.c1TxtBoxPortLan);
            this.groupBox3.Location = new System.Drawing.Point(12, 237);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(366, 51);
            this.groupBox3.TabIndex = 7;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Порт cервера TCP";
            // 
            // c1BtnStop
            // 
            this.c1BtnStop.Location = new System.Drawing.Point(286, 19);
            this.c1BtnStop.Name = "c1BtnStop";
            this.c1BtnStop.Size = new System.Drawing.Size(65, 20);
            this.c1BtnStop.TabIndex = 1;
            this.c1BtnStop.Text = "Стоп";
            this.c1BtnStop.UseVisualStyleBackColor = true;
            this.c1BtnStop.VisualStyle = C1.Win.C1Input.VisualStyle.Office2010Silver;
            this.c1BtnStop.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.Office2010Silver;
            this.c1BtnStop.Click += new System.EventHandler(this.c1BtnStop_Click);
            // 
            // c1BtnStart
            // 
            this.c1BtnStart.Location = new System.Drawing.Point(201, 19);
            this.c1BtnStart.Name = "c1BtnStart";
            this.c1BtnStart.Size = new System.Drawing.Size(65, 20);
            this.c1BtnStart.TabIndex = 0;
            this.c1BtnStart.Text = "Старт";
            this.c1BtnStart.UseVisualStyleBackColor = true;
            this.c1BtnStart.VisualStyle = C1.Win.C1Input.VisualStyle.Office2010Silver;
            this.c1BtnStart.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.Office2010Silver;
            this.c1BtnStart.Click += new System.EventHandler(this.c1BtnStart_Click);
            // 
            // richTextBoxReceivedMsg
            // 
            this.richTextBoxReceivedMsg.Location = new System.Drawing.Point(12, 331);
            this.richTextBoxReceivedMsg.Name = "richTextBoxReceivedMsg";
            this.richTextBoxReceivedMsg.Size = new System.Drawing.Size(366, 158);
            this.richTextBoxReceivedMsg.TabIndex = 8;
            this.richTextBoxReceivedMsg.Text = "";
            // 
            // c1CheckBoxMsg
            // 
            this.c1CheckBoxMsg.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(232)))), ((int)(((byte)(234)))), ((int)(((byte)(236)))));
            this.c1CheckBoxMsg.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.c1CheckBoxMsg.ForeColor = System.Drawing.Color.Black;
            this.c1CheckBoxMsg.Location = new System.Drawing.Point(12, 495);
            this.c1CheckBoxMsg.Name = "c1CheckBoxMsg";
            this.c1CheckBoxMsg.Padding = new System.Windows.Forms.Padding(1);
            this.c1CheckBoxMsg.Size = new System.Drawing.Size(240, 22);
            this.c1CheckBoxMsg.TabIndex = 9;
            this.c1CheckBoxMsg.Text = "Показывать сообщения от контроллеров";
            this.c1CheckBoxMsg.UseVisualStyleBackColor = false;
            this.c1CheckBoxMsg.Value = null;
            this.c1CheckBoxMsg.CheckedChanged += new System.EventHandler(this.c1CheckBoxMsg_CheckedChanged);
            // 
            // c1BtnTest
            // 
            this.c1BtnTest.Location = new System.Drawing.Point(328, 495);
            this.c1BtnTest.Name = "c1BtnTest";
            this.c1BtnTest.Size = new System.Drawing.Size(50, 22);
            this.c1BtnTest.TabIndex = 10;
            this.c1BtnTest.Text = "Test";
            this.c1BtnTest.UseVisualStyleBackColor = true;
            this.c1BtnTest.Visible = false;
            this.c1BtnTest.VisualStyle = C1.Win.C1Input.VisualStyle.Office2010Silver;
            this.c1BtnTest.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.Office2010Silver;
            this.c1BtnTest.Click += new System.EventHandler(this.c1BtnTest_Click);
            // 
            // toolStripMaim
            // 
            this.toolStripMaim.AutoSize = false;
            this.toolStripMaim.CanOverflow = false;
            this.toolStripMaim.Dock = System.Windows.Forms.DockStyle.None;
            this.toolStripMaim.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.toolStripMaim.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tlStripBtnSql,
            this.tlStripLblConSql,
            this.toolStripSeparator1,
            this.tlStripLblTime});
            this.toolStripMaim.Location = new System.Drawing.Point(12, 291);
            this.toolStripMaim.Name = "toolStripMaim";
            this.toolStripMaim.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
            this.toolStripMaim.Size = new System.Drawing.Size(366, 25);
            this.toolStripMaim.TabIndex = 11;
            this.toolStripMaim.Text = "toolStrip1";
            this.toolStripMaim.Visible = false;
            // 
            // tlStripBtnSql
            // 
            this.tlStripBtnSql.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tlStripBtnSql.Image = ((System.Drawing.Image)(resources.GetObject("tlStripBtnSql.Image")));
            this.tlStripBtnSql.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tlStripBtnSql.Name = "tlStripBtnSql";
            this.tlStripBtnSql.Size = new System.Drawing.Size(23, 22);
            this.tlStripBtnSql.Text = "toolStripButton1";
            this.tlStripBtnSql.Click += new System.EventHandler(this.tlStripBtnSql_Click);
            // 
            // tlStripLblConSql
            // 
            this.tlStripLblConSql.Name = "tlStripLblConSql";
            this.tlStripLblConSql.Size = new System.Drawing.Size(69, 22);
            this.tlStripLblConSql.Text = "SQL сервер";
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
            this.toolStripSeparator1.Visible = false;
            // 
            // tlStripLblTime
            // 
            this.tlStripLblTime.Name = "tlStripLblTime";
            this.tlStripLblTime.Size = new System.Drawing.Size(13, 22);
            this.tlStripLblTime.Text = "0";
            this.tlStripLblTime.Visible = false;
            // 
            // FrmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(387, 527);
            this.Controls.Add(this.toolStripMaim);
            this.Controls.Add(this.c1BtnTest);
            this.Controls.Add(this.c1CheckBoxMsg);
            this.Controls.Add(this.richTextBoxReceivedMsg);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FrmMain";
            this.Text = "Сервер GPRS-LAN";
            this.Activated += new System.EventHandler(this.FrmMain_Activated);
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FrmMain_FormClosing);
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.FrmMain_FormClosed);
            this.Load += new System.EventHandler(this.FrmMain_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.FrmMain_KeyDown);
            this.cntMenuStripMain.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.c1TxtBoxPortGprs)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.c1TxtBoxPortLan)).EndInit();
            this.groupBox1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.c1ListClientLan)).EndInit();
            this.groupBox2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.c1ListClientGprs)).EndInit();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.toolStripMaim.ResumeLayout(false);
            this.toolStripMaim.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ContextMenuStrip cntMenuStripMain;
        private System.Windows.Forms.ToolStripMenuItem ToolStripMenuItem_Close;
        private System.Windows.Forms.NotifyIcon notifyIconMain;
        private C1.Win.C1Input.C1TextBox c1TxtBoxPortGprs;
        private System.Windows.Forms.Label label1;
        private C1.Win.C1Input.C1TextBox c1TxtBoxPortLan;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.GroupBox groupBox1;
        private C1.Win.C1List.C1List c1ListClientLan;
        private System.Windows.Forms.GroupBox groupBox2;
        private C1.Win.C1List.C1List c1ListClientGprs;
        private System.Windows.Forms.GroupBox groupBox3;
        private C1.Win.C1Input.C1Button c1BtnStop;
        private C1.Win.C1Input.C1Button c1BtnStart;
        private System.Windows.Forms.RichTextBox richTextBoxReceivedMsg;
        private C1.Win.C1Input.C1CheckBox c1CheckBoxMsg;
        private C1.Win.C1Input.C1Button c1BtnTest;
        private System.Windows.Forms.ToolStrip toolStripMaim;
        private System.Windows.Forms.ToolStripButton tlStripBtnSql;
        private System.Windows.Forms.ToolStripLabel tlStripLblConSql;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripLabel tlStripLblTime;
    }
}

