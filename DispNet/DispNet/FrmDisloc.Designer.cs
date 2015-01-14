namespace DispNet
{
    partial class FrmDisloc
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmDisloc));
            C1.Win.C1List.Style style2 = new C1.Win.C1List.Style();
            C1.Win.C1List.Style style3 = new C1.Win.C1List.Style();
            C1.Win.C1List.Style style4 = new C1.Win.C1List.Style();
            C1.Win.C1List.Style style5 = new C1.Win.C1List.Style();
            C1.Win.C1List.Style style6 = new C1.Win.C1List.Style();
            C1.Win.C1List.Style style7 = new C1.Win.C1List.Style();
            C1.Win.C1List.Style style8 = new C1.Win.C1List.Style();
            this.c1ListDslFire = new C1.Win.C1List.C1List();
            this.btnRecDsl = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.c1ListDslFire)).BeginInit();
            this.SuspendLayout();
            // 
            // c1ListDslFire
            // 
            this.c1ListDslFire.AddItemSeparator = ';';
            this.c1ListDslFire.Caption = "Список пожаров и ЧС";
            this.c1ListDslFire.CaptionHeight = 17;
            this.c1ListDslFire.CaptionStyle = style1;
            this.c1ListDslFire.ColumnCaptionHeight = 17;
            this.c1ListDslFire.ColumnFooterHeight = 17;
            this.c1ListDslFire.DataMode = C1.Win.C1List.DataModeEnum.AddItem;
            this.c1ListDslFire.DeadAreaBackColor = System.Drawing.SystemColors.ControlDark;
            this.c1ListDslFire.EvenRowStyle = style2;
            this.c1ListDslFire.ExtendRightColumn = true;
            this.c1ListDslFire.FooterStyle = style3;
            this.c1ListDslFire.HeadingStyle = style4;
            this.c1ListDslFire.HighLightRowStyle = style5;
            this.c1ListDslFire.Images.Add(((System.Drawing.Image)(resources.GetObject("c1ListDslFire.Images"))));
            this.c1ListDslFire.ItemHeight = 15;
            this.c1ListDslFire.Location = new System.Drawing.Point(12, 12);
            this.c1ListDslFire.MatchEntryTimeout = ((long)(2000));
            this.c1ListDslFire.Name = "c1ListDslFire";
            this.c1ListDslFire.OddRowStyle = style6;
            this.c1ListDslFire.RowDivider.Color = System.Drawing.Color.DarkGray;
            this.c1ListDslFire.RowDivider.Style = C1.Win.C1List.LineStyleEnum.None;
            this.c1ListDslFire.RowSubDividerColor = System.Drawing.Color.DarkGray;
            this.c1ListDslFire.SelectedStyle = style7;
            this.c1ListDslFire.Size = new System.Drawing.Size(434, 193);
            this.c1ListDslFire.Style = style8;
            this.c1ListDslFire.TabIndex = 0;
            this.c1ListDslFire.PropBag = resources.GetString("c1ListDslFire.PropBag");
            // 
            // btnRecDsl
            // 
            this.btnRecDsl.Location = new System.Drawing.Point(191, 222);
            this.btnRecDsl.Name = "btnRecDsl";
            this.btnRecDsl.Size = new System.Drawing.Size(60, 23);
            this.btnRecDsl.TabIndex = 1;
            this.btnRecDsl.Text = "Запись";
            this.btnRecDsl.UseVisualStyleBackColor = true;
            this.btnRecDsl.Click += new System.EventHandler(this.btnRecDsl_Click);
            // 
            // FrmDisloc
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(457, 257);
            this.Controls.Add(this.btnRecDsl);
            this.Controls.Add(this.c1ListDslFire);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FrmDisloc";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FrmDisloc_FormClosing);
            this.Load += new System.EventHandler(this.FrmDisloc_Load);
            ((System.ComponentModel.ISupportInitialize)(this.c1ListDslFire)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private C1.Win.C1List.C1List c1ListDslFire;
        private System.Windows.Forms.Button btnRecDsl;
    }
}