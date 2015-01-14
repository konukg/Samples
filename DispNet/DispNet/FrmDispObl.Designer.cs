namespace DispNet
{
    partial class FrmDispObl
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmDispObl));
            this.axTabCntDispObl = new AxXtremeSuiteControls.AxTabControl();
            this.axImgMngDispObl = new AxXtremeCommandBars.AxImageManager();
            ((System.ComponentModel.ISupportInitialize)(this.axTabCntDispObl)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.axImgMngDispObl)).BeginInit();
            this.SuspendLayout();
            // 
            // axTabCntDispObl
            // 
            this.axTabCntDispObl.Location = new System.Drawing.Point(2, 3);
            this.axTabCntDispObl.Name = "axTabCntDispObl";
            this.axTabCntDispObl.OcxState = ((System.Windows.Forms.AxHost.State)(resources.GetObject("axTabCntDispObl.OcxState")));
            this.axTabCntDispObl.Size = new System.Drawing.Size(635, 249);
            this.axTabCntDispObl.TabIndex = 0;
            this.axTabCntDispObl.SelectedChanged += new AxXtremeSuiteControls._DTabControlEvents_SelectedChangedEventHandler(this.axTabCntDispObl_SelectedChanged);
            // 
            // axImgMngDispObl
            // 
            this.axImgMngDispObl.Enabled = true;
            this.axImgMngDispObl.Location = new System.Drawing.Point(245, 138);
            this.axImgMngDispObl.Name = "axImgMngDispObl";
            this.axImgMngDispObl.OcxState = ((System.Windows.Forms.AxHost.State)(resources.GetObject("axImgMngDispObl.OcxState")));
            this.axImgMngDispObl.Size = new System.Drawing.Size(24, 24);
            this.axImgMngDispObl.TabIndex = 1;
            // 
            // FrmDispObl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(643, 259);
            this.Controls.Add(this.axImgMngDispObl);
            this.Controls.Add(this.axTabCntDispObl);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FrmDispObl";
            this.Text = "FrmDispObl";
            this.Load += new System.EventHandler(this.FrmDispObl_Load);
            ((System.ComponentModel.ISupportInitialize)(this.axTabCntDispObl)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.axImgMngDispObl)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private AxXtremeCommandBars.AxImageManager axImgMngDispObl;
        private AxXtremeSuiteControls.AxTabControl axTabCntDispObl;
    }
}