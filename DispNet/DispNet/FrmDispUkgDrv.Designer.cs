namespace DispNet
{
    partial class FrmDispUkgDrv
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmDispUkgDrv));
            this.c1FlxGrdUkgDrv = new C1.Win.C1FlexGrid.C1FlexGrid();
            this.txtBoxPlace = new System.Windows.Forms.TextBox();
            this.txtBoxTake = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.chkBoxPtzD = new System.Windows.Forms.CheckBox();
            this.chkBoxPtuD = new System.Windows.Forms.CheckBox();
            this.btnRecDrv = new System.Windows.Forms.Button();
            this.grpBoxType = new System.Windows.Forms.GroupBox();
            this.rdBtnNoCatDrv = new System.Windows.Forms.RadioButton();
            this.btnCatDrvChs = new System.Windows.Forms.Button();
            this.m_lblCatDrv = new System.Windows.Forms.Label();
            this.rdBtnDrvPtr = new System.Windows.Forms.RadioButton();
            this.rdBtnDrvOth = new System.Windows.Forms.RadioButton();
            this.rdBtnDrvLrn = new System.Windows.Forms.RadioButton();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.txtBoxDrvNote = new System.Windows.Forms.TextBox();
            this.label11 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.c1FlxGrdUkgDrv)).BeginInit();
            this.grpBoxType.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // c1FlxGrdUkgDrv
            // 
            this.c1FlxGrdUkgDrv.ColumnInfo = "9,1,0,0,0,85,Columns:0{Width:20;}\t1{Width:43;}\t2{Width:53;}\t3{Width:52;}\t4{Width:" +
                "52;}\t5{Width:42;}\t6{Width:37;}\t7{Width:40;}\t8{Width:40;}\t";
            this.c1FlxGrdUkgDrv.Cursor = System.Windows.Forms.Cursors.Default;
            this.c1FlxGrdUkgDrv.Location = new System.Drawing.Point(12, 12);
            this.c1FlxGrdUkgDrv.Name = "c1FlxGrdUkgDrv";
            this.c1FlxGrdUkgDrv.Rows.DefaultSize = 17;
            this.c1FlxGrdUkgDrv.Size = new System.Drawing.Size(462, 124);
            this.c1FlxGrdUkgDrv.TabIndex = 1;
            this.c1FlxGrdUkgDrv.AfterEdit += new C1.Win.C1FlexGrid.RowColEventHandler(this.c1FlxGrdUkgDrv_AfterEdit);
            this.c1FlxGrdUkgDrv.CellButtonClick += new C1.Win.C1FlexGrid.RowColEventHandler(this.c1FlxGrdUkgDrv_CellButtonClick);
            this.c1FlxGrdUkgDrv.BeforeEdit += new C1.Win.C1FlexGrid.RowColEventHandler(this.c1FlxGrdUkgDrv_BeforeEdit);
            // 
            // txtBoxPlace
            // 
            this.txtBoxPlace.Location = new System.Drawing.Point(15, 29);
            this.txtBoxPlace.Name = "txtBoxPlace";
            this.txtBoxPlace.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtBoxPlace.Size = new System.Drawing.Size(415, 20);
            this.txtBoxPlace.TabIndex = 3;
            this.txtBoxPlace.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtBoxPlace_KeyPress);
            // 
            // txtBoxTake
            // 
            this.txtBoxTake.Location = new System.Drawing.Point(15, 68);
            this.txtBoxTake.Name = "txtBoxTake";
            this.txtBoxTake.Size = new System.Drawing.Size(415, 20);
            this.txtBoxTake.TabIndex = 4;
            this.txtBoxTake.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtBoxTake_KeyPress);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(210, 13);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(39, 13);
            this.label2.TabIndex = 6;
            this.label2.Text = "Место";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(201, 52);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(56, 13);
            this.label3.TabIndex = 7;
            this.label3.Text = "Проводит";
            // 
            // chkBoxPtzD
            // 
            this.chkBoxPtzD.AutoSize = true;
            this.chkBoxPtzD.Location = new System.Drawing.Point(304, 52);
            this.chkBoxPtzD.Name = "chkBoxPtzD";
            this.chkBoxPtzD.Size = new System.Drawing.Size(48, 17);
            this.chkBoxPtzD.TabIndex = 11;
            this.chkBoxPtzD.Text = "ПТЗ";
            this.chkBoxPtzD.UseVisualStyleBackColor = true;
            // 
            // chkBoxPtuD
            // 
            this.chkBoxPtuD.AutoSize = true;
            this.chkBoxPtuD.Location = new System.Drawing.Point(381, 52);
            this.chkBoxPtuD.Name = "chkBoxPtuD";
            this.chkBoxPtuD.Size = new System.Drawing.Size(49, 17);
            this.chkBoxPtuD.TabIndex = 12;
            this.chkBoxPtuD.Text = "ПТУ";
            this.chkBoxPtuD.UseVisualStyleBackColor = true;
            // 
            // btnRecDrv
            // 
            this.btnRecDrv.Location = new System.Drawing.Point(399, 123);
            this.btnRecDrv.Name = "btnRecDrv";
            this.btnRecDrv.Size = new System.Drawing.Size(54, 21);
            this.btnRecDrv.TabIndex = 13;
            this.btnRecDrv.Text = "Запись";
            this.btnRecDrv.UseVisualStyleBackColor = true;
            this.btnRecDrv.Click += new System.EventHandler(this.btnRecDrv_Click);
            // 
            // grpBoxType
            // 
            this.grpBoxType.Controls.Add(this.rdBtnNoCatDrv);
            this.grpBoxType.Controls.Add(this.btnCatDrvChs);
            this.grpBoxType.Controls.Add(this.chkBoxPtzD);
            this.grpBoxType.Controls.Add(this.chkBoxPtuD);
            this.grpBoxType.Controls.Add(this.m_lblCatDrv);
            this.grpBoxType.Controls.Add(this.rdBtnDrvPtr);
            this.grpBoxType.Controls.Add(this.rdBtnDrvOth);
            this.grpBoxType.Controls.Add(this.rdBtnDrvLrn);
            this.grpBoxType.Location = new System.Drawing.Point(12, 142);
            this.grpBoxType.Name = "grpBoxType";
            this.grpBoxType.Size = new System.Drawing.Size(462, 77);
            this.grpBoxType.TabIndex = 29;
            this.grpBoxType.TabStop = false;
            this.grpBoxType.Text = "Категория выезда";
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
            // btnCatDrvChs
            // 
            this.btnCatDrvChs.Image = ((System.Drawing.Image)(resources.GetObject("btnCatDrvChs.Image")));
            this.btnCatDrvChs.Location = new System.Drawing.Point(436, 19);
            this.btnCatDrvChs.Name = "btnCatDrvChs";
            this.btnCatDrvChs.Size = new System.Drawing.Size(20, 20);
            this.btnCatDrvChs.TabIndex = 43;
            this.btnCatDrvChs.UseVisualStyleBackColor = true;
            this.btnCatDrvChs.Click += new System.EventHandler(this.btnCatDrvChs_Click);
            // 
            // m_lblCatDrv
            // 
            this.m_lblCatDrv.BackColor = System.Drawing.SystemColors.Window;
            this.m_lblCatDrv.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.m_lblCatDrv.Location = new System.Drawing.Point(15, 19);
            this.m_lblCatDrv.Name = "m_lblCatDrv";
            this.m_lblCatDrv.Size = new System.Drawing.Size(415, 20);
            this.m_lblCatDrv.TabIndex = 44;
            this.m_lblCatDrv.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // rdBtnDrvPtr
            // 
            this.rdBtnDrvPtr.AutoSize = true;
            this.rdBtnDrvPtr.Location = new System.Drawing.Point(138, 51);
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
            this.rdBtnDrvOth.Location = new System.Drawing.Point(202, 51);
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
            this.rdBtnDrvLrn.Location = new System.Drawing.Point(65, 51);
            this.rdBtnDrvLrn.Name = "rdBtnDrvLrn";
            this.rdBtnDrvLrn.Size = new System.Drawing.Size(67, 17);
            this.rdBtnDrvLrn.TabIndex = 36;
            this.rdBtnDrvLrn.Text = "Занятия";
            this.rdBtnDrvLrn.UseVisualStyleBackColor = true;
            this.rdBtnDrvLrn.CheckedChanged += new System.EventHandler(this.rdBtnDrvLrn_CheckedChanged);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.txtBoxDrvNote);
            this.groupBox1.Controls.Add(this.label11);
            this.groupBox1.Controls.Add(this.btnRecDrv);
            this.groupBox1.Controls.Add(this.txtBoxPlace);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.txtBoxTake);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Location = new System.Drawing.Point(12, 225);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(462, 157);
            this.groupBox1.TabIndex = 30;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Информация";
            // 
            // txtBoxDrvNote
            // 
            this.txtBoxDrvNote.Location = new System.Drawing.Point(15, 107);
            this.txtBoxDrvNote.Multiline = true;
            this.txtBoxDrvNote.Name = "txtBoxDrvNote";
            this.txtBoxDrvNote.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtBoxDrvNote.Size = new System.Drawing.Size(369, 37);
            this.txtBoxDrvNote.TabIndex = 18;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(194, 91);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(70, 13);
            this.label11.TabIndex = 19;
            this.label11.Text = "Примечание";
            // 
            // FrmDispUkgDrv
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(482, 391);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.grpBoxType);
            this.Controls.Add(this.c1FlxGrdUkgDrv);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.MaximizeBox = false;
            this.Name = "FrmDispUkgDrv";
            this.Text = "FrmDispUkgDrv";
            this.Load += new System.EventHandler(this.FrmDispUkgDrv_Load);
            ((System.ComponentModel.ISupportInitialize)(this.c1FlxGrdUkgDrv)).EndInit();
            this.grpBoxType.ResumeLayout(false);
            this.grpBoxType.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private C1.Win.C1FlexGrid.C1FlexGrid c1FlxGrdUkgDrv;
        private System.Windows.Forms.TextBox txtBoxPlace;
        private System.Windows.Forms.TextBox txtBoxTake;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.CheckBox chkBoxPtzD;
        private System.Windows.Forms.CheckBox chkBoxPtuD;
        private System.Windows.Forms.Button btnRecDrv;
        private System.Windows.Forms.GroupBox grpBoxType;
        private System.Windows.Forms.RadioButton rdBtnNoCatDrv;
        private System.Windows.Forms.Button btnCatDrvChs;
        public System.Windows.Forms.Label m_lblCatDrv;
        private System.Windows.Forms.RadioButton rdBtnDrvPtr;
        private System.Windows.Forms.RadioButton rdBtnDrvOth;
        private System.Windows.Forms.RadioButton rdBtnDrvLrn;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TextBox txtBoxDrvNote;
        private System.Windows.Forms.Label label11;
    }
}