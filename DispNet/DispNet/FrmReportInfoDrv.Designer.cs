namespace DispNet
{
    partial class FrmReportInfoDrv
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
            C1.Win.C1List.Style style1 = new C1.Win.C1List.Style();
            C1.Win.C1List.Style style2 = new C1.Win.C1List.Style();
            C1.Win.C1List.Style style3 = new C1.Win.C1List.Style();
            C1.Win.C1List.Style style4 = new C1.Win.C1List.Style();
            C1.Win.C1List.Style style5 = new C1.Win.C1List.Style();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmReportInfoDrv));
            C1.Win.C1List.Style style6 = new C1.Win.C1List.Style();
            C1.Win.C1List.Style style7 = new C1.Win.C1List.Style();
            C1.Win.C1List.Style style8 = new C1.Win.C1List.Style();
            this.rdBtn0_6 = new System.Windows.Forms.RadioButton();
            this.rdBtn6_0 = new System.Windows.Forms.RadioButton();
            this.rdBtn0_0 = new System.Windows.Forms.RadioButton();
            this.rdBtn8_8 = new System.Windows.Forms.RadioButton();
            this.rdBtnPeriod = new System.Windows.Forms.RadioButton();
            this.btnRptShow = new System.Windows.Forms.Button();
            this.c1CmbOrgRec = new C1.Win.C1List.C1Combo();
            this.txtBoxRec = new System.Windows.Forms.TextBox();
            this.c1FlxGrdMsgRec = new C1.Win.C1FlexGrid.C1FlexGrid();
            ((System.ComponentModel.ISupportInitialize)(this.c1CmbOrgRec)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.c1FlxGrdMsgRec)).BeginInit();
            this.SuspendLayout();
            // 
            // rdBtn0_6
            // 
            this.rdBtn0_6.AutoSize = true;
            this.rdBtn0_6.Checked = true;
            this.rdBtn0_6.Location = new System.Drawing.Point(25, 27);
            this.rdBtn0_6.Name = "rdBtn0_6";
            this.rdBtn0_6.Size = new System.Drawing.Size(96, 17);
            this.rdBtn0_6.TabIndex = 0;
            this.rdBtn0_6.TabStop = true;
            this.rdBtn0_6.Text = "с 0 до 6 часов";
            this.rdBtn0_6.UseVisualStyleBackColor = true;
            this.rdBtn0_6.CheckedChanged += new System.EventHandler(this.rdBtn0_6_CheckedChanged);
            // 
            // rdBtn6_0
            // 
            this.rdBtn6_0.AutoSize = true;
            this.rdBtn6_0.Location = new System.Drawing.Point(25, 50);
            this.rdBtn6_0.Name = "rdBtn6_0";
            this.rdBtn6_0.Size = new System.Drawing.Size(96, 17);
            this.rdBtn6_0.TabIndex = 1;
            this.rdBtn6_0.Text = "с 6 до 0 часов";
            this.rdBtn6_0.UseVisualStyleBackColor = true;
            this.rdBtn6_0.CheckedChanged += new System.EventHandler(this.rdBtn6_0_CheckedChanged);
            // 
            // rdBtn0_0
            // 
            this.rdBtn0_0.AutoSize = true;
            this.rdBtn0_0.Location = new System.Drawing.Point(25, 73);
            this.rdBtn0_0.Name = "rdBtn0_0";
            this.rdBtn0_0.Size = new System.Drawing.Size(96, 17);
            this.rdBtn0_0.TabIndex = 2;
            this.rdBtn0_0.Text = "с 0 до 0 часов";
            this.rdBtn0_0.UseVisualStyleBackColor = true;
            this.rdBtn0_0.CheckedChanged += new System.EventHandler(this.rdBtn0_0_CheckedChanged);
            // 
            // rdBtn8_8
            // 
            this.rdBtn8_8.AutoSize = true;
            this.rdBtn8_8.Location = new System.Drawing.Point(25, 96);
            this.rdBtn8_8.Name = "rdBtn8_8";
            this.rdBtn8_8.Size = new System.Drawing.Size(96, 17);
            this.rdBtn8_8.TabIndex = 3;
            this.rdBtn8_8.Text = "с 8 до 8 часов";
            this.rdBtn8_8.UseVisualStyleBackColor = true;
            this.rdBtn8_8.CheckedChanged += new System.EventHandler(this.rdBtn8_8_CheckedChanged);
            // 
            // rdBtnPeriod
            // 
            this.rdBtnPeriod.AutoSize = true;
            this.rdBtnPeriod.Location = new System.Drawing.Point(25, 119);
            this.rdBtnPeriod.Name = "rdBtnPeriod";
            this.rdBtnPeriod.Size = new System.Drawing.Size(108, 17);
            this.rdBtnPeriod.TabIndex = 4;
            this.rdBtnPeriod.Text = "Выбрать период";
            this.rdBtnPeriod.UseVisualStyleBackColor = true;
            this.rdBtnPeriod.CheckedChanged += new System.EventHandler(this.rdBtnPeriod_CheckedChanged);
            // 
            // btnRptShow
            // 
            this.btnRptShow.Location = new System.Drawing.Point(270, 27);
            this.btnRptShow.Name = "btnRptShow";
            this.btnRptShow.Size = new System.Drawing.Size(63, 26);
            this.btnRptShow.TabIndex = 5;
            this.btnRptShow.Text = "Отчет";
            this.btnRptShow.UseVisualStyleBackColor = true;
            this.btnRptShow.Click += new System.EventHandler(this.btnRptShow_Click);
            // 
            // c1CmbOrgRec
            // 
            this.c1CmbOrgRec.AddItemSeparator = ';';
            this.c1CmbOrgRec.AutoCompletion = true;
            this.c1CmbOrgRec.AutoDropDown = true;
            this.c1CmbOrgRec.Caption = "";
            this.c1CmbOrgRec.CaptionHeight = 17;
            this.c1CmbOrgRec.CaptionStyle = style1;
            this.c1CmbOrgRec.CharacterCasing = System.Windows.Forms.CharacterCasing.Normal;
            this.c1CmbOrgRec.ColumnCaptionHeight = 17;
            this.c1CmbOrgRec.ColumnFooterHeight = 17;
            this.c1CmbOrgRec.ContentHeight = 15;
            this.c1CmbOrgRec.DeadAreaBackColor = System.Drawing.Color.Empty;
            this.c1CmbOrgRec.EditorBackColor = System.Drawing.SystemColors.Window;
            this.c1CmbOrgRec.EditorFont = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.c1CmbOrgRec.EditorForeColor = System.Drawing.SystemColors.WindowText;
            this.c1CmbOrgRec.EditorHeight = 15;
            this.c1CmbOrgRec.EvenRowStyle = style2;
            this.c1CmbOrgRec.ExtendRightColumn = true;
            this.c1CmbOrgRec.FooterStyle = style3;
            this.c1CmbOrgRec.HeadingStyle = style4;
            this.c1CmbOrgRec.HighLightRowStyle = style5;
            this.c1CmbOrgRec.Images.Add(((System.Drawing.Image)(resources.GetObject("c1CmbOrgRec.Images"))));
            this.c1CmbOrgRec.ItemHeight = 15;
            this.c1CmbOrgRec.Location = new System.Drawing.Point(25, 246);
            this.c1CmbOrgRec.MatchEntryTimeout = ((long)(2000));
            this.c1CmbOrgRec.MaxDropDownItems = ((short)(5));
            this.c1CmbOrgRec.MaxLength = 32767;
            this.c1CmbOrgRec.MouseCursor = System.Windows.Forms.Cursors.Default;
            this.c1CmbOrgRec.Name = "c1CmbOrgRec";
            this.c1CmbOrgRec.OddRowStyle = style6;
            this.c1CmbOrgRec.RowDivider.Color = System.Drawing.Color.DarkGray;
            this.c1CmbOrgRec.RowDivider.Style = C1.Win.C1List.LineStyleEnum.None;
            this.c1CmbOrgRec.RowSubDividerColor = System.Drawing.Color.DarkGray;
            this.c1CmbOrgRec.SelectedStyle = style7;
            this.c1CmbOrgRec.Size = new System.Drawing.Size(177, 21);
            this.c1CmbOrgRec.Style = style8;
            this.c1CmbOrgRec.TabIndex = 7;
            this.c1CmbOrgRec.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.c1CmbOrgRec_KeyPress);
            this.c1CmbOrgRec.PropBag = resources.GetString("c1CmbOrgRec.PropBag");
            // 
            // txtBoxRec
            // 
            this.txtBoxRec.Location = new System.Drawing.Point(208, 246);
            this.txtBoxRec.Name = "txtBoxRec";
            this.txtBoxRec.Size = new System.Drawing.Size(114, 20);
            this.txtBoxRec.TabIndex = 9;
            this.txtBoxRec.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtBoxRec_KeyPress);
            // 
            // c1FlxGrdMsgRec
            // 
            this.c1FlxGrdMsgRec.ColumnInfo = resources.GetString("c1FlxGrdMsgRec.ColumnInfo");
            this.c1FlxGrdMsgRec.ExtendLastCol = true;
            this.c1FlxGrdMsgRec.Location = new System.Drawing.Point(25, 153);
            this.c1FlxGrdMsgRec.Name = "c1FlxGrdMsgRec";
            this.c1FlxGrdMsgRec.Rows.Count = 1;
            this.c1FlxGrdMsgRec.Rows.DefaultSize = 17;
            this.c1FlxGrdMsgRec.ShowCellLabels = true;
            this.c1FlxGrdMsgRec.Size = new System.Drawing.Size(297, 70);
            this.c1FlxGrdMsgRec.TabIndex = 10;
            // 
            // FrmReportInfoDrv
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(354, 293);
            this.Controls.Add(this.c1FlxGrdMsgRec);
            this.Controls.Add(this.txtBoxRec);
            this.Controls.Add(this.c1CmbOrgRec);
            this.Controls.Add(this.btnRptShow);
            this.Controls.Add(this.rdBtnPeriod);
            this.Controls.Add(this.rdBtn8_8);
            this.Controls.Add(this.rdBtn0_0);
            this.Controls.Add(this.rdBtn6_0);
            this.Controls.Add(this.rdBtn0_6);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "FrmReportInfoDrv";
            this.Text = "Сведения по выездам";
            this.Load += new System.EventHandler(this.FrmReportInfoDrv_Load);
            ((System.ComponentModel.ISupportInitialize)(this.c1CmbOrgRec)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.c1FlxGrdMsgRec)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.RadioButton rdBtn0_6;
        private System.Windows.Forms.RadioButton rdBtn6_0;
        private System.Windows.Forms.RadioButton rdBtn0_0;
        private System.Windows.Forms.RadioButton rdBtn8_8;
        private System.Windows.Forms.Button btnRptShow;
        public System.Windows.Forms.RadioButton rdBtnPeriod;
        private C1.Win.C1List.C1Combo c1CmbOrgRec;
        private System.Windows.Forms.TextBox txtBoxRec;
        private C1.Win.C1FlexGrid.C1FlexGrid c1FlxGrdMsgRec;
    }
}