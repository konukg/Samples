namespace DispNet
{
    partial class frmStreetDiv
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmStreetDiv));
            C1.Win.C1List.Style style6 = new C1.Win.C1List.Style();
            C1.Win.C1List.Style style7 = new C1.Win.C1List.Style();
            C1.Win.C1List.Style style8 = new C1.Win.C1List.Style();
            this.c1ListStreetDiv = new C1.Win.C1List.C1List();
            this.btnRecStreet = new System.Windows.Forms.Button();
            this.btnExit = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.c1ListStreetDiv)).BeginInit();
            this.SuspendLayout();
            // 
            // c1ListStreetDiv
            // 
            this.c1ListStreetDiv.AddItemSeparator = ';';
            this.c1ListStreetDiv.AllowColMove = false;
            this.c1ListStreetDiv.Caption = "Деление улицы (дома)";
            this.c1ListStreetDiv.CaptionHeight = 17;
            this.c1ListStreetDiv.CaptionStyle = style1;
            this.c1ListStreetDiv.ColumnCaptionHeight = 17;
            this.c1ListStreetDiv.ColumnFooterHeight = 17;
            this.c1ListStreetDiv.DeadAreaBackColor = System.Drawing.SystemColors.ControlDark;
            this.c1ListStreetDiv.DefColWidth = 10;
            this.c1ListStreetDiv.EvenRowStyle = style2;
            this.c1ListStreetDiv.ExtendRightColumn = true;
            this.c1ListStreetDiv.FooterStyle = style3;
            this.c1ListStreetDiv.HeadingStyle = style4;
            this.c1ListStreetDiv.HighLightRowStyle = style5;
            this.c1ListStreetDiv.Images.Add(((System.Drawing.Image)(resources.GetObject("c1ListStreetDiv.Images"))));
            this.c1ListStreetDiv.ItemHeight = 15;
            this.c1ListStreetDiv.Location = new System.Drawing.Point(12, 12);
            this.c1ListStreetDiv.MatchEntryTimeout = ((long)(2000));
            this.c1ListStreetDiv.Name = "c1ListStreetDiv";
            this.c1ListStreetDiv.OddRowStyle = style6;
            this.c1ListStreetDiv.RowDivider.Color = System.Drawing.Color.DarkGray;
            this.c1ListStreetDiv.RowDivider.Style = C1.Win.C1List.LineStyleEnum.None;
            this.c1ListStreetDiv.RowSubDividerColor = System.Drawing.Color.DarkGray;
            this.c1ListStreetDiv.SelectedStyle = style7;
            this.c1ListStreetDiv.Size = new System.Drawing.Size(374, 143);
            this.c1ListStreetDiv.Style = style8;
            this.c1ListStreetDiv.TabIndex = 0;
            this.c1ListStreetDiv.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.c1ListStreetDiv_KeyPress);
            this.c1ListStreetDiv.PropBag = resources.GetString("c1ListStreetDiv.PropBag");
            // 
            // btnRecStreet
            // 
            this.btnRecStreet.Location = new System.Drawing.Point(87, 171);
            this.btnRecStreet.Name = "btnRecStreet";
            this.btnRecStreet.Size = new System.Drawing.Size(64, 22);
            this.btnRecStreet.TabIndex = 1;
            this.btnRecStreet.Text = "Записать";
            this.btnRecStreet.UseVisualStyleBackColor = true;
            this.btnRecStreet.Click += new System.EventHandler(this.btnRecStreet_Click);
            // 
            // btnExit
            // 
            this.btnExit.Location = new System.Drawing.Point(227, 171);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(64, 22);
            this.btnExit.TabIndex = 2;
            this.btnExit.Text = "Закрыть";
            this.btnExit.UseVisualStyleBackColor = true;
            this.btnExit.Click += new System.EventHandler(this.btnExit_Click);
            // 
            // frmStreetDiv
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(399, 201);
            this.Controls.Add(this.btnExit);
            this.Controls.Add(this.btnRecStreet);
            this.Controls.Add(this.c1ListStreetDiv);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmStreetDiv";
            this.Load += new System.EventHandler(this.frmStreetDiv_Load);
            ((System.ComponentModel.ISupportInitialize)(this.c1ListStreetDiv)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private C1.Win.C1List.C1List c1ListStreetDiv;
        private System.Windows.Forms.Button btnRecStreet;
        private System.Windows.Forms.Button btnExit;
    }
}