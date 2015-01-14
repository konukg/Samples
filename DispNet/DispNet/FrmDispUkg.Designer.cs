namespace DispNet
{
    partial class FrmDispUkg
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmDispUkg));
            this.axTabCntDispUkg = new AxXtremeSuiteControls.AxTabControl();
            this.axImgMngDispUkg = new AxXtremeCommandBars.AxImageManager();
            ((System.ComponentModel.ISupportInitialize)(this.axTabCntDispUkg)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.axImgMngDispUkg)).BeginInit();
            this.SuspendLayout();
            // 
            // axTabCntDispUkg
            // 
            this.axTabCntDispUkg.Location = new System.Drawing.Point(2, 3);
            this.axTabCntDispUkg.Name = "axTabCntDispUkg";
            this.axTabCntDispUkg.OcxState = ((System.Windows.Forms.AxHost.State)(resources.GetObject("axTabCntDispUkg.OcxState")));
            this.axTabCntDispUkg.Size = new System.Drawing.Size(635, 249);
            this.axTabCntDispUkg.TabIndex = 0;
            this.axTabCntDispUkg.SelectedChanged += new AxXtremeSuiteControls._DTabControlEvents_SelectedChangedEventHandler(this.axTabCntDispUkg_SelectedChanged);
            // 
            // axImgMngDispUkg
            // 
            this.axImgMngDispUkg.Enabled = true;
            this.axImgMngDispUkg.Location = new System.Drawing.Point(220, 179);
            this.axImgMngDispUkg.Name = "axImgMngDispUkg";
            this.axImgMngDispUkg.OcxState = ((System.Windows.Forms.AxHost.State)(resources.GetObject("axImgMngDispUkg.OcxState")));
            this.axImgMngDispUkg.Size = new System.Drawing.Size(24, 24);
            this.axImgMngDispUkg.TabIndex = 1;
            // 
            // FrmDispUkg
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(643, 259);
            this.Controls.Add(this.axImgMngDispUkg);
            this.Controls.Add(this.axTabCntDispUkg);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "FrmDispUkg";
            this.Text = "FrmDispUkg";
            this.Load += new System.EventHandler(this.FrmDispUkg_Load);
            ((System.ComponentModel.ISupportInitialize)(this.axTabCntDispUkg)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.axImgMngDispUkg)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private AxXtremeSuiteControls.AxTabControl axTabCntDispUkg;
        private AxXtremeCommandBars.AxImageManager axImgMngDispUkg;
    }
}