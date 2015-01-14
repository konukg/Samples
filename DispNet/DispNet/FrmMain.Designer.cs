namespace DispNet
{
    partial class FrmMain
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmMain));
            this.axCommandBars1 = new AxXtremeCommandBars.AxCommandBars();
            this.statStripMain = new System.Windows.Forms.StatusStrip();
            this.tlStripStatLabelCnt = new System.Windows.Forms.ToolStripStatusLabel();
            this.tmrFrmMain1 = new System.Windows.Forms.Timer(this.components);
            this.bckgrndWorkerMain = new System.ComponentModel.BackgroundWorker();
            this.axImageManager1 = new AxXtremeCommandBars.AxImageManager();
            ((System.ComponentModel.ISupportInitialize)(this.axCommandBars1)).BeginInit();
            this.statStripMain.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.axImageManager1)).BeginInit();
            this.SuspendLayout();
            // 
            // axCommandBars1
            // 
            resources.ApplyResources(this.axCommandBars1, "axCommandBars1");
            this.axCommandBars1.Name = "axCommandBars1";
            this.axCommandBars1.OcxState = ((System.Windows.Forms.AxHost.State)(resources.GetObject("axCommandBars1.OcxState")));
            this.axCommandBars1.Execute += new AxXtremeCommandBars._DCommandBarsEvents_ExecuteEventHandler(this.axCommandBars1_Execute);
            // 
            // statStripMain
            // 
            this.statStripMain.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tlStripStatLabelCnt});
            resources.ApplyResources(this.statStripMain, "statStripMain");
            this.statStripMain.Name = "statStripMain";
            // 
            // tlStripStatLabelCnt
            // 
            this.tlStripStatLabelCnt.Name = "tlStripStatLabelCnt";
            resources.ApplyResources(this.tlStripStatLabelCnt, "tlStripStatLabelCnt");
            // 
            // tmrFrmMain1
            // 
            this.tmrFrmMain1.Interval = 10000;
            this.tmrFrmMain1.Tick += new System.EventHandler(this.tmrFrmMain1_Tick);
            // 
            // bckgrndWorkerMain
            // 
            this.bckgrndWorkerMain.DoWork += new System.ComponentModel.DoWorkEventHandler(this.bckgrndWorkerMain_DoWork);
            this.bckgrndWorkerMain.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.bckgrndWorkerMain_RunWorkerCompleted);
            // 
            // axImageManager1
            // 
            resources.ApplyResources(this.axImageManager1, "axImageManager1");
            this.axImageManager1.Name = "axImageManager1";
            this.axImageManager1.OcxState = ((System.Windows.Forms.AxHost.State)(resources.GetObject("axImageManager1.OcxState")));
            // 
            // FrmMain
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            resources.ApplyResources(this, "$this");
            this.Controls.Add(this.axImageManager1);
            this.Controls.Add(this.statStripMain);
            this.Controls.Add(this.axCommandBars1);
            this.IsMdiContainer = true;
            this.Name = "FrmMain";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FrmMain_FormClosing);
            this.Load += new System.EventHandler(this.FrmMain_Load);
            ((System.ComponentModel.ISupportInitialize)(this.axCommandBars1)).EndInit();
            this.statStripMain.ResumeLayout(false);
            this.statStripMain.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.axImageManager1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        public AxXtremeCommandBars.AxCommandBars axCommandBars1;
        private System.Windows.Forms.StatusStrip statStripMain;
        private System.Windows.Forms.ToolStripStatusLabel tlStripStatLabelCnt;
        private System.Windows.Forms.Timer tmrFrmMain1;
        private System.ComponentModel.BackgroundWorker bckgrndWorkerMain;
        private AxXtremeCommandBars.AxImageManager axImageManager1;

    }
}

