namespace GpsUkgIcs
{
    partial class FrmBortFollow
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmBortFollow));
            this.c1SizerFollpw = new C1.Win.C1Sizer.C1Sizer();
            this.axMapFollow = new AxMapXLib.AxMap();
            this.c1SuperLblInfo = new C1.Win.C1SuperTooltip.C1SuperLabel();
            ((System.ComponentModel.ISupportInitialize)(this.c1SizerFollpw)).BeginInit();
            this.c1SizerFollpw.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.axMapFollow)).BeginInit();
            this.SuspendLayout();
            // 
            // c1SizerFollpw
            // 
            this.c1SizerFollpw.Controls.Add(this.axMapFollow);
            this.c1SizerFollpw.GridDefinition = "98.2142857142857:False:False;\t98.6159169550173:False:False;";
            this.c1SizerFollpw.Location = new System.Drawing.Point(113, 86);
            this.c1SizerFollpw.Name = "c1SizerFollpw";
            this.c1SizerFollpw.Size = new System.Drawing.Size(578, 448);
            this.c1SizerFollpw.TabIndex = 0;
            this.c1SizerFollpw.Text = "c1SizerFollpw";
            // 
            // axMapFollow
            // 
            this.axMapFollow.Enabled = true;
            this.axMapFollow.Location = new System.Drawing.Point(91, 90);
            this.axMapFollow.Name = "axMapFollow";
            this.axMapFollow.OcxState = ((System.Windows.Forms.AxHost.State)(resources.GetObject("axMapFollow.OcxState")));
            this.axMapFollow.Size = new System.Drawing.Size(411, 305);
            this.axMapFollow.TabIndex = 0;
            // 
            // c1SuperLblInfo
            // 
            this.c1SuperLblInfo.AutoSize = true;
            this.c1SuperLblInfo.BackColor = System.Drawing.SystemColors.Info;
            this.c1SuperLblInfo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.c1SuperLblInfo.Location = new System.Drawing.Point(0, 0);
            this.c1SuperLblInfo.Name = "c1SuperLblInfo";
            this.c1SuperLblInfo.Size = new System.Drawing.Size(38, 21);
            this.c1SuperLblInfo.TabIndex = 2;
            this.c1SuperLblInfo.Text = "11111";
            this.c1SuperLblInfo.UseMnemonic = true;
            // 
            // FrmBortFollow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(834, 612);
            this.Controls.Add(this.c1SuperLblInfo);
            this.Controls.Add(this.c1SizerFollpw);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "FrmBortFollow";
            this.Load += new System.EventHandler(this.FrmBortFollow_Load);
            ((System.ComponentModel.ISupportInitialize)(this.c1SizerFollpw)).EndInit();
            this.c1SizerFollpw.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.axMapFollow)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private C1.Win.C1Sizer.C1Sizer c1SizerFollpw;
        private AxMapXLib.AxMap axMapFollow;
        private C1.Win.C1SuperTooltip.C1SuperLabel c1SuperLblInfo;
    }
}