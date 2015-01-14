namespace VkoRsc
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmMain));
            this.axCmndBarsMain = new AxXtremeCommandBars.AxCommandBars();
            this.c1mnuMain = new C1.Win.C1Command.C1MainMenu();
            this.c1CmndHolderMain = new C1.Win.C1Command.C1CommandHolder();
            this.c1CommandMenu1 = new C1.Win.C1Command.C1CommandMenu();
            this.c1CommandLink2 = new C1.Win.C1Command.C1CommandLink();
            this.c1CmdMapVko = new C1.Win.C1Command.C1Command();
            this.c1CommandLink1 = new C1.Win.C1Command.C1CommandLink();
            ((System.ComponentModel.ISupportInitialize)(this.axCmndBarsMain)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.c1CmndHolderMain)).BeginInit();
            this.SuspendLayout();
            // 
            // axCmndBarsMain
            // 
            this.axCmndBarsMain.Enabled = true;
            this.axCmndBarsMain.Location = new System.Drawing.Point(25, 58);
            this.axCmndBarsMain.Name = "axCmndBarsMain";
            this.axCmndBarsMain.OcxState = ((System.Windows.Forms.AxHost.State)(resources.GetObject("axCmndBarsMain.OcxState")));
            this.axCmndBarsMain.Size = new System.Drawing.Size(24, 24);
            this.axCmndBarsMain.TabIndex = 1;
            // 
            // c1mnuMain
            // 
            this.c1mnuMain.AccessibleName = "Menu Bar";
            this.c1mnuMain.CommandHolder = this.c1CmndHolderMain;
            this.c1mnuMain.CommandLinks.AddRange(new C1.Win.C1Command.C1CommandLink[] {
            this.c1CommandLink1});
            this.c1mnuMain.Dock = System.Windows.Forms.DockStyle.Top;
            this.c1mnuMain.Location = new System.Drawing.Point(0, 0);
            this.c1mnuMain.Name = "c1mnuMain";
            this.c1mnuMain.Size = new System.Drawing.Size(583, 27);
            this.c1mnuMain.VisualStyle = C1.Win.C1Command.VisualStyle.Office2007Blue;
            this.c1mnuMain.VisualStyleBase = C1.Win.C1Command.VisualStyle.Office2007Blue;
            // 
            // c1CmndHolderMain
            // 
            this.c1CmndHolderMain.Commands.Add(this.c1CommandMenu1);
            this.c1CmndHolderMain.Commands.Add(this.c1CmdMapVko);
            this.c1CmndHolderMain.Owner = this;
            this.c1CmndHolderMain.VisualStyle = C1.Win.C1Command.VisualStyle.Office2007Blue;
            // 
            // c1CommandMenu1
            // 
            this.c1CommandMenu1.CommandLinks.AddRange(new C1.Win.C1Command.C1CommandLink[] {
            this.c1CommandLink2});
            this.c1CommandMenu1.HideNonRecentLinks = false;
            this.c1CommandMenu1.Name = "c1CommandMenu1";
            this.c1CommandMenu1.Text = "Карта";
            this.c1CommandMenu1.VisualStyleBase = C1.Win.C1Command.VisualStyle.Office2007Blue;
            // 
            // c1CommandLink2
            // 
            this.c1CommandLink2.Command = this.c1CmdMapVko;
            // 
            // c1CmdMapVko
            // 
            this.c1CmdMapVko.Name = "c1CmdMapVko";
            this.c1CmdMapVko.Text = "ВКО";
            this.c1CmdMapVko.Click += new C1.Win.C1Command.ClickEventHandler(this.c1CmdMapVko_Click);
            // 
            // c1CommandLink1
            // 
            this.c1CommandLink1.Command = this.c1CommandMenu1;
            // 
            // FrmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(583, 408);
            this.Controls.Add(this.axCmndBarsMain);
            this.Controls.Add(this.c1mnuMain);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.IsMdiContainer = true;
            this.Name = "FrmMain";
            this.Text = "Восточно-Казахстанская область";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.FrmMain_Load);
            ((System.ComponentModel.ISupportInitialize)(this.axCmndBarsMain)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.c1CmndHolderMain)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private AxXtremeCommandBars.AxCommandBars axCmndBarsMain;
        private C1.Win.C1Command.C1MainMenu c1mnuMain;
        private C1.Win.C1Command.C1CommandHolder c1CmndHolderMain;
        private C1.Win.C1Command.C1CommandLink c1CommandLink1;
        private C1.Win.C1Command.C1CommandMenu c1CommandMenu1;
        private C1.Win.C1Command.C1CommandLink c1CommandLink2;
        private C1.Win.C1Command.C1Command c1CmdMapVko;
    }
}

