namespace DispNet
{
    partial class FrmBortDrvUkg
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
            C1.Win.C1List.Style style9 = new C1.Win.C1List.Style();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmBortDrvUkg));
            C1.Win.C1List.Style style10 = new C1.Win.C1List.Style();
            C1.Win.C1List.Style style11 = new C1.Win.C1List.Style();
            C1.Win.C1List.Style style12 = new C1.Win.C1List.Style();
            C1.Win.C1List.Style style13 = new C1.Win.C1List.Style();
            C1.Win.C1List.Style style14 = new C1.Win.C1List.Style();
            C1.Win.C1List.Style style15 = new C1.Win.C1List.Style();
            C1.Win.C1List.Style style16 = new C1.Win.C1List.Style();
            this.txtBoxPlace = new System.Windows.Forms.TextBox();
            this.txtBoxTake = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.btnAddBort = new System.Windows.Forms.Button();
            this.btnDelBort = new System.Windows.Forms.Button();
            this.btnRecDrv = new System.Windows.Forms.Button();
            this.c1ListBortDrv = new C1.Win.C1List.C1List();
            this.chkBoxPtz = new System.Windows.Forms.CheckBox();
            this.chkBoxPtu = new System.Windows.Forms.CheckBox();
            this.rdBtnNoCatDrv = new System.Windows.Forms.RadioButton();
            this.m_lblCatDrv = new System.Windows.Forms.Label();
            this.rdBtnDrvPtr = new System.Windows.Forms.RadioButton();
            this.rdBtnDrvOth = new System.Windows.Forms.RadioButton();
            this.rdBtnDrvLrn = new System.Windows.Forms.RadioButton();
            this.grpBoxType = new System.Windows.Forms.GroupBox();
            this.btnCatDrvChs = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.txtBoxDrvNote = new System.Windows.Forms.TextBox();
            this.label11 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.c1ListBortDrv)).BeginInit();
            this.grpBoxType.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // txtBoxPlace
            // 
            this.txtBoxPlace.Location = new System.Drawing.Point(15, 30);
            this.txtBoxPlace.Name = "txtBoxPlace";
            this.txtBoxPlace.Size = new System.Drawing.Size(238, 20);
            this.txtBoxPlace.TabIndex = 1;
            this.txtBoxPlace.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtBoxPlace_KeyPress);
            // 
            // txtBoxTake
            // 
            this.txtBoxTake.Location = new System.Drawing.Point(15, 69);
            this.txtBoxTake.Name = "txtBoxTake";
            this.txtBoxTake.Size = new System.Drawing.Size(238, 20);
            this.txtBoxTake.TabIndex = 2;
            this.txtBoxTake.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtBoxTake_KeyPress);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(121, 14);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(39, 13);
            this.label2.TabIndex = 4;
            this.label2.Text = "Место";
            this.label2.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(104, 53);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(56, 13);
            this.label3.TabIndex = 5;
            this.label3.Text = "Проводит";
            this.label3.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // btnAddBort
            // 
            this.btnAddBort.Location = new System.Drawing.Point(357, 28);
            this.btnAddBort.Name = "btnAddBort";
            this.btnAddBort.Size = new System.Drawing.Size(21, 22);
            this.btnAddBort.TabIndex = 7;
            this.btnAddBort.Text = "<";
            this.btnAddBort.UseVisualStyleBackColor = true;
            this.btnAddBort.Click += new System.EventHandler(this.btnAddBort_Click);
            // 
            // btnDelBort
            // 
            this.btnDelBort.Location = new System.Drawing.Point(357, 69);
            this.btnDelBort.Name = "btnDelBort";
            this.btnDelBort.Size = new System.Drawing.Size(22, 22);
            this.btnDelBort.TabIndex = 8;
            this.btnDelBort.Text = ">";
            this.btnDelBort.UseVisualStyleBackColor = true;
            this.btnDelBort.Click += new System.EventHandler(this.btnDelBort_Click);
            // 
            // btnRecDrv
            // 
            this.btnRecDrv.Location = new System.Drawing.Point(285, 123);
            this.btnRecDrv.Name = "btnRecDrv";
            this.btnRecDrv.Size = new System.Drawing.Size(52, 22);
            this.btnRecDrv.TabIndex = 9;
            this.btnRecDrv.Text = "Запись";
            this.btnRecDrv.UseVisualStyleBackColor = true;
            this.btnRecDrv.Click += new System.EventHandler(this.btnRecDrv_Click);
            // 
            // c1ListBortDrv
            // 
            this.c1ListBortDrv.AddItemSeparator = ';';
            this.c1ListBortDrv.AllowColMove = false;
            this.c1ListBortDrv.AllowColSelect = false;
            this.c1ListBortDrv.Caption = "Борт";
            this.c1ListBortDrv.CaptionHeight = 0;
            this.c1ListBortDrv.CaptionStyle = style9;
            this.c1ListBortDrv.ColumnCaptionHeight = 17;
            this.c1ListBortDrv.ColumnFooterHeight = 17;
            this.c1ListBortDrv.ColumnWidth = 50;
            this.c1ListBortDrv.DataMode = C1.Win.C1List.DataModeEnum.AddItem;
            this.c1ListBortDrv.DeadAreaBackColor = System.Drawing.SystemColors.ControlDark;
            this.c1ListBortDrv.DefColWidth = 50;
            this.c1ListBortDrv.EvenRowStyle = style10;
            this.c1ListBortDrv.ExtendRightColumn = true;
            this.c1ListBortDrv.FooterStyle = style11;
            this.c1ListBortDrv.HeadingStyle = style12;
            this.c1ListBortDrv.HighLightRowStyle = style13;
            this.c1ListBortDrv.Images.Add(((System.Drawing.Image)(resources.GetObject("c1ListBortDrv.Images"))));
            this.c1ListBortDrv.ItemHeight = 15;
            this.c1ListBortDrv.Location = new System.Drawing.Point(277, 14);
            this.c1ListBortDrv.MatchEntryTimeout = ((long)(2000));
            this.c1ListBortDrv.Name = "c1ListBortDrv";
            this.c1ListBortDrv.OddRowStyle = style14;
            this.c1ListBortDrv.RowDivider.Color = System.Drawing.Color.DarkGray;
            this.c1ListBortDrv.RowDivider.Style = C1.Win.C1List.LineStyleEnum.None;
            this.c1ListBortDrv.RowSubDividerColor = System.Drawing.Color.DarkGray;
            this.c1ListBortDrv.SelectedStyle = style15;
            this.c1ListBortDrv.SelectionMode = C1.Win.C1List.SelectionModeEnum.MultiSimple;
            this.c1ListBortDrv.Size = new System.Drawing.Size(74, 97);
            this.c1ListBortDrv.Style = style16;
            this.c1ListBortDrv.TabIndex = 6;
            this.c1ListBortDrv.PropBag = resources.GetString("c1ListBortDrv.PropBag");
            // 
            // chkBoxPtz
            // 
            this.chkBoxPtz.AutoSize = true;
            this.chkBoxPtz.Location = new System.Drawing.Point(277, 52);
            this.chkBoxPtz.Name = "chkBoxPtz";
            this.chkBoxPtz.Size = new System.Drawing.Size(48, 17);
            this.chkBoxPtz.TabIndex = 10;
            this.chkBoxPtz.Text = "ПТЗ";
            this.chkBoxPtz.UseVisualStyleBackColor = true;
            this.chkBoxPtz.CheckedChanged += new System.EventHandler(this.chkBoxPtz_CheckedChanged);
            // 
            // chkBoxPtu
            // 
            this.chkBoxPtu.AutoSize = true;
            this.chkBoxPtu.Location = new System.Drawing.Point(335, 51);
            this.chkBoxPtu.Name = "chkBoxPtu";
            this.chkBoxPtu.Size = new System.Drawing.Size(49, 17);
            this.chkBoxPtu.TabIndex = 11;
            this.chkBoxPtu.Text = "ПТУ";
            this.chkBoxPtu.UseVisualStyleBackColor = true;
            this.chkBoxPtu.CheckedChanged += new System.EventHandler(this.chkBoxPtu_CheckedChanged);
            // 
            // rdBtnNoCatDrv
            // 
            this.rdBtnNoCatDrv.AutoSize = true;
            this.rdBtnNoCatDrv.Checked = true;
            this.rdBtnNoCatDrv.Location = new System.Drawing.Point(15, 51);
            this.rdBtnNoCatDrv.Name = "rdBtnNoCatDrv";
            this.rdBtnNoCatDrv.Size = new System.Drawing.Size(44, 17);
            this.rdBtnNoCatDrv.TabIndex = 45;
            this.rdBtnNoCatDrv.TabStop = true;
            this.rdBtnNoCatDrv.Text = "Нет";
            this.rdBtnNoCatDrv.UseVisualStyleBackColor = true;
            this.rdBtnNoCatDrv.CheckedChanged += new System.EventHandler(this.rdBtnNoCatDrv_CheckedChanged);
            // 
            // m_lblCatDrv
            // 
            this.m_lblCatDrv.BackColor = System.Drawing.SystemColors.Window;
            this.m_lblCatDrv.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.m_lblCatDrv.Location = new System.Drawing.Point(15, 19);
            this.m_lblCatDrv.Name = "m_lblCatDrv";
            this.m_lblCatDrv.Size = new System.Drawing.Size(336, 20);
            this.m_lblCatDrv.TabIndex = 44;
            this.m_lblCatDrv.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // rdBtnDrvPtr
            // 
            this.rdBtnDrvPtr.AutoSize = true;
            this.rdBtnDrvPtr.Location = new System.Drawing.Point(127, 51);
            this.rdBtnDrvPtr.Name = "rdBtnDrvPtr";
            this.rdBtnDrvPtr.Size = new System.Drawing.Size(58, 17);
            this.rdBtnDrvPtr.TabIndex = 38;
            this.rdBtnDrvPtr.Text = "Дозор";
            this.rdBtnDrvPtr.UseVisualStyleBackColor = true;
            this.rdBtnDrvPtr.CheckedChanged += new System.EventHandler(this.rdBtnDrvPtr_CheckedChanged);
            // 
            // rdBtnDrvOth
            // 
            this.rdBtnDrvOth.AutoSize = true;
            this.rdBtnDrvOth.Location = new System.Drawing.Point(191, 51);
            this.rdBtnDrvOth.Name = "rdBtnDrvOth";
            this.rdBtnDrvOth.Size = new System.Drawing.Size(62, 17);
            this.rdBtnDrvOth.TabIndex = 37;
            this.rdBtnDrvOth.Text = "Прочие";
            this.rdBtnDrvOth.UseVisualStyleBackColor = true;
            this.rdBtnDrvOth.CheckedChanged += new System.EventHandler(this.rdBtnDrvOth_CheckedChanged);
            // 
            // rdBtnDrvLrn
            // 
            this.rdBtnDrvLrn.AutoSize = true;
            this.rdBtnDrvLrn.Location = new System.Drawing.Point(62, 51);
            this.rdBtnDrvLrn.Name = "rdBtnDrvLrn";
            this.rdBtnDrvLrn.Size = new System.Drawing.Size(67, 17);
            this.rdBtnDrvLrn.TabIndex = 36;
            this.rdBtnDrvLrn.Text = "Занятия";
            this.rdBtnDrvLrn.UseVisualStyleBackColor = true;
            this.rdBtnDrvLrn.CheckedChanged += new System.EventHandler(this.rdBtnDrvLrn_CheckedChanged);
            // 
            // grpBoxType
            // 
            this.grpBoxType.Controls.Add(this.rdBtnNoCatDrv);
            this.grpBoxType.Controls.Add(this.chkBoxPtu);
            this.grpBoxType.Controls.Add(this.btnCatDrvChs);
            this.grpBoxType.Controls.Add(this.chkBoxPtz);
            this.grpBoxType.Controls.Add(this.m_lblCatDrv);
            this.grpBoxType.Controls.Add(this.rdBtnDrvPtr);
            this.grpBoxType.Controls.Add(this.rdBtnDrvOth);
            this.grpBoxType.Controls.Add(this.rdBtnDrvLrn);
            this.grpBoxType.Location = new System.Drawing.Point(12, 12);
            this.grpBoxType.Name = "grpBoxType";
            this.grpBoxType.Size = new System.Drawing.Size(390, 77);
            this.grpBoxType.TabIndex = 28;
            this.grpBoxType.TabStop = false;
            this.grpBoxType.Text = "Категория выезда";
            // 
            // btnCatDrvChs
            // 
            this.btnCatDrvChs.Image = ((System.Drawing.Image)(resources.GetObject("btnCatDrvChs.Image")));
            this.btnCatDrvChs.Location = new System.Drawing.Point(357, 19);
            this.btnCatDrvChs.Name = "btnCatDrvChs";
            this.btnCatDrvChs.Size = new System.Drawing.Size(20, 20);
            this.btnCatDrvChs.TabIndex = 43;
            this.btnCatDrvChs.UseVisualStyleBackColor = true;
            this.btnCatDrvChs.Click += new System.EventHandler(this.btnCatDrvChs_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.txtBoxDrvNote);
            this.groupBox1.Controls.Add(this.label11);
            this.groupBox1.Controls.Add(this.txtBoxPlace);
            this.groupBox1.Controls.Add(this.txtBoxTake);
            this.groupBox1.Controls.Add(this.btnRecDrv);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.btnDelBort);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.btnAddBort);
            this.groupBox1.Controls.Add(this.c1ListBortDrv);
            this.groupBox1.Location = new System.Drawing.Point(12, 95);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(390, 163);
            this.groupBox1.TabIndex = 45;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Информация";
            // 
            // txtBoxDrvNote
            // 
            this.txtBoxDrvNote.Location = new System.Drawing.Point(15, 108);
            this.txtBoxDrvNote.Multiline = true;
            this.txtBoxDrvNote.Name = "txtBoxDrvNote";
            this.txtBoxDrvNote.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtBoxDrvNote.Size = new System.Drawing.Size(238, 37);
            this.txtBoxDrvNote.TabIndex = 16;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(93, 92);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(70, 13);
            this.label11.TabIndex = 17;
            this.label11.Text = "Примечание";
            // 
            // FrmBortDrvUkg
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(413, 269);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.grpBoxType);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FrmBortDrvUkg";
            this.Text = "Выезд техники - город";
            this.Load += new System.EventHandler(this.FrmBortDrvUkg_Load);
            ((System.ComponentModel.ISupportInitialize)(this.c1ListBortDrv)).EndInit();
            this.grpBoxType.ResumeLayout(false);
            this.grpBoxType.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TextBox txtBoxPlace;
        private System.Windows.Forms.TextBox txtBoxTake;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button btnAddBort;
        private System.Windows.Forms.Button btnDelBort;
        private System.Windows.Forms.Button btnRecDrv;
        public C1.Win.C1List.C1List c1ListBortDrv;
        private System.Windows.Forms.CheckBox chkBoxPtz;
        private System.Windows.Forms.CheckBox chkBoxPtu;
        private System.Windows.Forms.RadioButton rdBtnNoCatDrv;
        public System.Windows.Forms.Label m_lblCatDrv;
        private System.Windows.Forms.RadioButton rdBtnDrvPtr;
        private System.Windows.Forms.RadioButton rdBtnDrvOth;
        private System.Windows.Forms.RadioButton rdBtnDrvLrn;
        private System.Windows.Forms.GroupBox grpBoxType;
        private System.Windows.Forms.Button btnCatDrvChs;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TextBox txtBoxDrvNote;
        private System.Windows.Forms.Label label11;
    }
}