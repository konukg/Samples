namespace DispNet
{
    partial class FrmMenDrvUkg
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmMenDrvUkg));
            this.label2 = new System.Windows.Forms.Label();
            this.txtBoxPlace = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.txtBoxTake = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.btnRecMen = new System.Windows.Forms.Button();
            this.grpBoxType = new System.Windows.Forms.GroupBox();
            this.rdBtnNoCatDrv = new System.Windows.Forms.RadioButton();
            this.btnCatDrvChs = new System.Windows.Forms.Button();
            this.m_lblCatDrv = new System.Windows.Forms.Label();
            this.rdBtnDrvLrn = new System.Windows.Forms.RadioButton();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.c1DateRptDrvMen = new C1.Win.C1Input.C1DateEdit();
            this.grpBoxType.SuspendLayout();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.c1DateRptDrvMen)).BeginInit();
            this.SuspendLayout();
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(263, 22);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(42, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "Место:";
            // 
            // txtBoxPlace
            // 
            this.txtBoxPlace.Location = new System.Drawing.Point(15, 19);
            this.txtBoxPlace.Name = "txtBoxPlace";
            this.txtBoxPlace.Size = new System.Drawing.Size(242, 20);
            this.txtBoxPlace.TabIndex = 3;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(263, 48);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(59, 13);
            this.label3.TabIndex = 4;
            this.label3.Text = "Проводит:";
            // 
            // txtBoxTake
            // 
            this.txtBoxTake.Location = new System.Drawing.Point(15, 45);
            this.txtBoxTake.Name = "txtBoxTake";
            this.txtBoxTake.Size = new System.Drawing.Size(242, 20);
            this.txtBoxTake.TabIndex = 5;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(171, 74);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(65, 13);
            this.label4.TabIndex = 6;
            this.label4.Text = "Окончание:";
            // 
            // btnRecMen
            // 
            this.btnRecMen.Location = new System.Drawing.Point(344, 133);
            this.btnRecMen.Name = "btnRecMen";
            this.btnRecMen.Size = new System.Drawing.Size(58, 23);
            this.btnRecMen.TabIndex = 8;
            this.btnRecMen.Text = "Запись";
            this.btnRecMen.UseVisualStyleBackColor = true;
            this.btnRecMen.Click += new System.EventHandler(this.btnRecMen_Click);
            // 
            // grpBoxType
            // 
            this.grpBoxType.Controls.Add(this.rdBtnNoCatDrv);
            this.grpBoxType.Controls.Add(this.btnCatDrvChs);
            this.grpBoxType.Controls.Add(this.m_lblCatDrv);
            this.grpBoxType.Controls.Add(this.rdBtnDrvLrn);
            this.grpBoxType.Location = new System.Drawing.Point(12, 12);
            this.grpBoxType.Name = "grpBoxType";
            this.grpBoxType.Size = new System.Drawing.Size(390, 77);
            this.grpBoxType.TabIndex = 30;
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
            this.btnCatDrvChs.Location = new System.Drawing.Point(357, 19);
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
            this.m_lblCatDrv.Size = new System.Drawing.Size(336, 20);
            this.m_lblCatDrv.TabIndex = 44;
            this.m_lblCatDrv.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
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
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.c1DateRptDrvMen);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.txtBoxTake);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.txtBoxPlace);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Location = new System.Drawing.Point(12, 95);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(326, 104);
            this.groupBox1.TabIndex = 31;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Информация";
            // 
            // c1DateRptDrvMen
            // 
            // 
            // 
            // 
            this.c1DateRptDrvMen.Calendar.UIStrings.Content = new string[] {
        "&Clear:&Очистить",
        "&Today:&Сегодня"};
            this.c1DateRptDrvMen.CustomFormat = "d-MM-yyyy HH:mm:ss";
            this.c1DateRptDrvMen.FormatType = C1.Win.C1Input.FormatTypeEnum.CustomFormat;
            this.c1DateRptDrvMen.Location = new System.Drawing.Point(15, 71);
            this.c1DateRptDrvMen.Name = "c1DateRptDrvMen";
            this.c1DateRptDrvMen.Size = new System.Drawing.Size(150, 20);
            this.c1DateRptDrvMen.TabIndex = 32;
            this.c1DateRptDrvMen.Tag = null;
            // 
            // FrmMenDrvUkg
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(412, 207);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.btnRecMen);
            this.Controls.Add(this.grpBoxType);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FrmMenDrvUkg";
            this.Text = "Выезд без техники";
            this.Load += new System.EventHandler(this.FrmMenDrvUkg_Load);
            this.grpBoxType.ResumeLayout(false);
            this.grpBoxType.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.c1DateRptDrvMen)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtBoxPlace;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtBoxTake;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button btnRecMen;
        private System.Windows.Forms.GroupBox grpBoxType;
        private System.Windows.Forms.RadioButton rdBtnNoCatDrv;
        private System.Windows.Forms.Button btnCatDrvChs;
        public System.Windows.Forms.Label m_lblCatDrv;
        private System.Windows.Forms.RadioButton rdBtnDrvLrn;
        private System.Windows.Forms.GroupBox groupBox1;
        private C1.Win.C1Input.C1DateEdit c1DateRptDrvMen;
    }
}