namespace DispNet
{
    partial class FrmDateRpl
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmDateRpl));
            this.btnRecDateRpl = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.c1DateRptAft = new C1.Win.C1Input.C1DateEdit();
            this.c1DateRptBef = new C1.Win.C1Input.C1DateEdit();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.c1DateRptAft)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.c1DateRptBef)).BeginInit();
            this.SuspendLayout();
            // 
            // btnRecDateRpl
            // 
            this.btnRecDateRpl.Location = new System.Drawing.Point(176, 57);
            this.btnRecDateRpl.Name = "btnRecDateRpl";
            this.btnRecDateRpl.Size = new System.Drawing.Size(52, 21);
            this.btnRecDateRpl.TabIndex = 17;
            this.btnRecDateRpl.Text = "Выбор";
            this.btnRecDateRpl.UseVisualStyleBackColor = true;
            this.btnRecDateRpl.Click += new System.EventHandler(this.btnRecDateRpl_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.btnRecDateRpl);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.c1DateRptAft);
            this.groupBox1.Controls.Add(this.c1DateRptBef);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(399, 89);
            this.groupBox1.TabIndex = 57;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Период";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(193, 25);
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
            // c1DateRptAft
            // 
            // 
            // 
            // 
            this.c1DateRptAft.Calendar.UIStrings.Content = new string[] {
        "&Clear:&Очистить",
        "&Today:&Сегодня"};
            this.c1DateRptAft.Culture = 1049;
            this.c1DateRptAft.CustomFormat = "d-MM-yyyy HH:mm:ss";
            this.c1DateRptAft.DisplayFormat.CustomFormat = "d-MM-yyyy HH:mm:ss";
            this.c1DateRptAft.DisplayFormat.Inherit = ((C1.Win.C1Input.FormatInfoInheritFlags)(((((C1.Win.C1Input.FormatInfoInheritFlags.FormatType | C1.Win.C1Input.FormatInfoInheritFlags.NullText)
                        | C1.Win.C1Input.FormatInfoInheritFlags.EmptyAsNull)
                        | C1.Win.C1Input.FormatInfoInheritFlags.TrimStart)
                        | C1.Win.C1Input.FormatInfoInheritFlags.TrimEnd)));
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
            this.c1DateRptBef.DisplayFormat.CustomFormat = "d-MM-yyyy HH:mm:ss";
            this.c1DateRptBef.DisplayFormat.Inherit = ((C1.Win.C1Input.FormatInfoInheritFlags)(((((C1.Win.C1Input.FormatInfoInheritFlags.FormatType | C1.Win.C1Input.FormatInfoInheritFlags.NullText)
                        | C1.Win.C1Input.FormatInfoInheritFlags.EmptyAsNull)
                        | C1.Win.C1Input.FormatInfoInheritFlags.TrimStart)
                        | C1.Win.C1Input.FormatInfoInheritFlags.TrimEnd)));
            this.c1DateRptBef.FormatType = C1.Win.C1Input.FormatTypeEnum.CustomFormat;
            this.c1DateRptBef.Location = new System.Drawing.Point(221, 22);
            this.c1DateRptBef.Name = "c1DateRptBef";
            this.c1DateRptBef.Size = new System.Drawing.Size(150, 20);
            this.c1DateRptBef.TabIndex = 20;
            this.c1DateRptBef.Tag = null;
            // 
            // FrmDateRpl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(419, 110);
            this.Controls.Add(this.groupBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FrmDateRpl";
            this.Text = "Дата и время";
            this.Load += new System.EventHandler(this.FrmDateRpl_Load);
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.FrmDateRpl_FormClosed);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.c1DateRptAft)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.c1DateRptBef)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnRecDateRpl;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label2;
        private C1.Win.C1Input.C1DateEdit c1DateRptAft;
        private C1.Win.C1Input.C1DateEdit c1DateRptBef;
    }
}