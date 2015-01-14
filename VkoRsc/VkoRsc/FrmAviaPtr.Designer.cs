namespace VkoRsc
{
    partial class FrmAviaPtr
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmAviaPtr));
            this.c1SprLblAviaPtr = new C1.Win.C1SuperTooltip.C1SuperLabel();
            this.c1FlxGrdAviaPzv = new C1.Win.C1FlexGrid.C1FlexGrid();
            ((System.ComponentModel.ISupportInitialize)(this.c1FlxGrdAviaPzv)).BeginInit();
            this.SuspendLayout();
            // 
            // c1SprLblAviaPtr
            // 
            this.c1SprLblAviaPtr.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.c1SprLblAviaPtr.Location = new System.Drawing.Point(12, 12);
            this.c1SprLblAviaPtr.Name = "c1SprLblAviaPtr";
            this.c1SprLblAviaPtr.Size = new System.Drawing.Size(408, 130);
            this.c1SprLblAviaPtr.TabIndex = 0;
            this.c1SprLblAviaPtr.Text = "c1SuperLabel1";
            this.c1SprLblAviaPtr.UseMnemonic = true;
            // 
            // c1FlxGrdAviaPzv
            // 
            this.c1FlxGrdAviaPzv.ColumnInfo = resources.GetString("c1FlxGrdAviaPzv.ColumnInfo");
            this.c1FlxGrdAviaPzv.ExtendLastCol = true;
            this.c1FlxGrdAviaPzv.Location = new System.Drawing.Point(12, 148);
            this.c1FlxGrdAviaPzv.Name = "c1FlxGrdAviaPzv";
            this.c1FlxGrdAviaPzv.Rows.Count = 1;
            this.c1FlxGrdAviaPzv.Rows.DefaultSize = 17;
            this.c1FlxGrdAviaPzv.ShowCellLabels = true;
            this.c1FlxGrdAviaPzv.Size = new System.Drawing.Size(408, 107);
            this.c1FlxGrdAviaPzv.TabIndex = 1;
            this.c1FlxGrdAviaPzv.VisualStyle = C1.Win.C1FlexGrid.VisualStyle.Office2007Blue;
            // 
            // FrmAviaPtr
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(429, 263);
            this.Controls.Add(this.c1FlxGrdAviaPzv);
            this.Controls.Add(this.c1SprLblAviaPtr);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FrmAviaPtr";
            this.Opacity = 0.8D;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Авиалесоохрана";
            this.Load += new System.EventHandler(this.FrmAviaPtr_Load);
            ((System.ComponentModel.ISupportInitialize)(this.c1FlxGrdAviaPzv)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private C1.Win.C1SuperTooltip.C1SuperLabel c1SprLblAviaPtr;
        private C1.Win.C1FlexGrid.C1FlexGrid c1FlxGrdAviaPzv;
    }
}