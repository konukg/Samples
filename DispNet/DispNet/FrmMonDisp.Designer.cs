namespace DispNet
{
    partial class FrmMonDisp
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmMonDisp));
            this.c1DateRptAft = new C1.Win.C1Input.C1DateEdit();
            this.c1DateRptBef = new C1.Win.C1Input.C1DateEdit();
            this.chkBoxMonErr = new System.Windows.Forms.CheckBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btnDateCh = new System.Windows.Forms.Button();
            this.label5 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.c1FlxGrdMonAlt = new C1.Win.C1FlexGrid.C1FlexGrid();
            this.label4 = new System.Windows.Forms.Label();
            this.btnAlertMap = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.btnAlertInfo = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.btnAlertPmt = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.c1DateRptAft)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.c1DateRptBef)).BeginInit();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.c1FlxGrdMonAlt)).BeginInit();
            this.SuspendLayout();
            // 
            // c1DateRptAft
            // 
            // 
            // 
            // 
            this.c1DateRptAft.Calendar.UIStrings.Content = new string[] {
        "&Clear:&Очистить",
        "&Today:&Сегодня"};
            this.c1DateRptAft.CustomFormat = "d-MM-yyyy HH:mm:ss";
            this.c1DateRptAft.FormatType = C1.Win.C1Input.FormatTypeEnum.CustomFormat;
            this.c1DateRptAft.Location = new System.Drawing.Point(37, 22);
            this.c1DateRptAft.Name = "c1DateRptAft";
            this.c1DateRptAft.Size = new System.Drawing.Size(150, 20);
            this.c1DateRptAft.TabIndex = 21;
            this.c1DateRptAft.Tag = null;
            // 
            // c1DateRptBef
            // 
            // 
            // 
            // 
            this.c1DateRptBef.Calendar.UIStrings.Content = new string[] {
        "&Clear:&Очистить",
        "&Today:&Сегодня"};
            this.c1DateRptBef.CustomFormat = "d-MM-yyyy HH:mm:ss";
            this.c1DateRptBef.FormatType = C1.Win.C1Input.FormatTypeEnum.CustomFormat;
            this.c1DateRptBef.Location = new System.Drawing.Point(37, 52);
            this.c1DateRptBef.Name = "c1DateRptBef";
            this.c1DateRptBef.Size = new System.Drawing.Size(150, 20);
            this.c1DateRptBef.TabIndex = 20;
            this.c1DateRptBef.Tag = null;
            // 
            // chkBoxMonErr
            // 
            this.chkBoxMonErr.AutoSize = true;
            this.chkBoxMonErr.Location = new System.Drawing.Point(210, 25);
            this.chkBoxMonErr.Name = "chkBoxMonErr";
            this.chkBoxMonErr.Size = new System.Drawing.Size(166, 17);
            this.chkBoxMonErr.TabIndex = 58;
            this.chkBoxMonErr.Text = "Только ложные сообщения";
            this.chkBoxMonErr.UseVisualStyleBackColor = true;
            this.chkBoxMonErr.CheckedChanged += new System.EventHandler(this.chkBoxMonErr_CheckedChanged);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.chkBoxMonErr);
            this.groupBox1.Controls.Add(this.btnDateCh);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.c1DateRptAft);
            this.groupBox1.Controls.Add(this.c1DateRptBef);
            this.groupBox1.Location = new System.Drawing.Point(13, 123);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(382, 85);
            this.groupBox1.TabIndex = 56;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Период";
            // 
            // btnDateCh
            // 
            this.btnDateCh.Location = new System.Drawing.Point(256, 52);
            this.btnDateCh.Name = "btnDateCh";
            this.btnDateCh.Size = new System.Drawing.Size(54, 20);
            this.btnDateCh.TabIndex = 57;
            this.btnDateCh.Text = "Выбор";
            this.btnDateCh.UseVisualStyleBackColor = true;
            this.btnDateCh.Click += new System.EventHandler(this.btnDateCh_Click);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(9, 52);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(22, 13);
            this.label5.TabIndex = 46;
            this.label5.Text = "по:";
            this.label5.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(15, 25);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(16, 13);
            this.label2.TabIndex = 45;
            this.label2.Text = "с:";
            this.label2.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // c1FlxGrdMonAlt
            // 
            this.c1FlxGrdMonAlt.AllowEditing = false;
            this.c1FlxGrdMonAlt.AllowResizing = C1.Win.C1FlexGrid.AllowResizingEnum.None;
            this.c1FlxGrdMonAlt.BorderStyle = C1.Win.C1FlexGrid.Util.BaseControls.BorderStyleEnum.XpThemes;
            this.c1FlxGrdMonAlt.ColumnInfo = resources.GetString("c1FlxGrdMonAlt.ColumnInfo");
            this.c1FlxGrdMonAlt.ExtendLastCol = true;
            this.c1FlxGrdMonAlt.Location = new System.Drawing.Point(13, 13);
            this.c1FlxGrdMonAlt.Name = "c1FlxGrdMonAlt";
            this.c1FlxGrdMonAlt.Rows.Count = 6;
            this.c1FlxGrdMonAlt.Rows.DefaultSize = 17;
            this.c1FlxGrdMonAlt.ShowCellLabels = true;
            this.c1FlxGrdMonAlt.Size = new System.Drawing.Size(382, 104);
            this.c1FlxGrdMonAlt.TabIndex = 55;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(440, 103);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(75, 13);
            this.label4.TabIndex = 54;
            this.label4.Text = "Карта города";
            this.label4.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // btnAlertMap
            // 
            this.btnAlertMap.Image = global::DispNet.Properties.Resources.ico1309;
            this.btnAlertMap.Location = new System.Drawing.Point(411, 97);
            this.btnAlertMap.Name = "btnAlertMap";
            this.btnAlertMap.Size = new System.Drawing.Size(21, 20);
            this.btnAlertMap.TabIndex = 53;
            this.btnAlertMap.UseVisualStyleBackColor = true;
            this.btnAlertMap.Click += new System.EventHandler(this.btnAlertMap_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(440, 66);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(90, 13);
            this.label3.TabIndex = 52;
            this.label3.Text = "Характеристики";
            // 
            // btnAlertInfo
            // 
            this.btnAlertInfo.Image = global::DispNet.Properties.Resources.ico2644;
            this.btnAlertInfo.Location = new System.Drawing.Point(411, 62);
            this.btnAlertInfo.Name = "btnAlertInfo";
            this.btnAlertInfo.Size = new System.Drawing.Size(23, 21);
            this.btnAlertInfo.TabIndex = 51;
            this.btnAlertInfo.UseVisualStyleBackColor = true;
            this.btnAlertInfo.Click += new System.EventHandler(this.btnAlertInfo_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(440, 32);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(99, 13);
            this.label1.TabIndex = 50;
            this.label1.Text = "Данные в путевку";
            // 
            // btnAlertPmt
            // 
            this.btnAlertPmt.Image = global::DispNet.Properties.Resources.FIRE1_16;
            this.btnAlertPmt.Location = new System.Drawing.Point(411, 28);
            this.btnAlertPmt.Name = "btnAlertPmt";
            this.btnAlertPmt.Size = new System.Drawing.Size(23, 21);
            this.btnAlertPmt.TabIndex = 49;
            this.btnAlertPmt.UseVisualStyleBackColor = true;
            this.btnAlertPmt.Click += new System.EventHandler(this.btnAlertPmt_Click);
            // 
            // FrmMonDisp
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(541, 214);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.c1FlxGrdMonAlt);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.btnAlertMap);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.btnAlertInfo);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnAlertPmt);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "FrmMonDisp";
            this.Text = "Журнал мониторинга объектов";
            this.Load += new System.EventHandler(this.FrmMonDisp_Load);
            ((System.ComponentModel.ISupportInitialize)(this.c1DateRptAft)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.c1DateRptBef)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.c1FlxGrdMonAlt)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private C1.Win.C1Input.C1DateEdit c1DateRptAft;
        private C1.Win.C1Input.C1DateEdit c1DateRptBef;
        private System.Windows.Forms.CheckBox chkBoxMonErr;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btnDateCh;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button btnAlertMap;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button btnAlertInfo;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnAlertPmt;
        public C1.Win.C1FlexGrid.C1FlexGrid c1FlxGrdMonAlt;
    }
}