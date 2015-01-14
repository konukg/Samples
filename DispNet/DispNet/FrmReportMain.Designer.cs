namespace DispNet
{
    partial class FrmReportMain
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmReportMain));
            this.CommandBars = new AxXtremeCommandBars.AxCommandBars();
            ((System.ComponentModel.ISupportInitialize)(this.CommandBars)).BeginInit();
            this.SuspendLayout();
            // 
            // CommandBars
            // 
            this.CommandBars.Enabled = true;
            this.CommandBars.Location = new System.Drawing.Point(12, 12);
            this.CommandBars.Name = "CommandBars";
            this.CommandBars.OcxState = ((System.Windows.Forms.AxHost.State)(resources.GetObject("CommandBars.OcxState")));
            this.CommandBars.Size = new System.Drawing.Size(24, 24);
            this.CommandBars.TabIndex = 3;
            // 
            // FrmReportMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(772, 511);
            this.Controls.Add(this.CommandBars);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.IsMdiContainer = true;
            this.Name = "FrmReportMain";
            this.Load += new System.EventHandler(this.FrmReportMain_Load);
            this.Activated += new System.EventHandler(this.FrmReportMain_Activated);
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.FrmReportMain_FormClosed);
            ((System.ComponentModel.ISupportInitialize)(this.CommandBars)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        public AxXtremeCommandBars.AxCommandBars CommandBars;
    }
}