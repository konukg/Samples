namespace DispNet
{
    partial class FrmMapUkgPmt
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmMapUkgPmt));
            this.axMap1 = new AxMapXLib.AxMap();
            this.btnTest = new System.Windows.Forms.Button();
            this.bckgrndWrkMapPmt = new System.ComponentModel.BackgroundWorker();
            ((System.ComponentModel.ISupportInitialize)(this.axMap1)).BeginInit();
            this.SuspendLayout();
            // 
            // axMap1
            // 
            this.axMap1.Enabled = true;
            this.axMap1.Location = new System.Drawing.Point(4, 3);
            this.axMap1.Name = "axMap1";
            this.axMap1.OcxState = ((System.Windows.Forms.AxHost.State)(resources.GetObject("axMap1.OcxState")));
            this.axMap1.Size = new System.Drawing.Size(400, 340);
            this.axMap1.TabIndex = 1;
            this.axMap1.MapViewChanged += new System.EventHandler(this.axMap1_MapViewChanged);
            // 
            // btnTest
            // 
            this.btnTest.Location = new System.Drawing.Point(4, 3);
            this.btnTest.Name = "btnTest";
            this.btnTest.Size = new System.Drawing.Size(37, 22);
            this.btnTest.TabIndex = 2;
            this.btnTest.Text = "t";
            this.btnTest.UseVisualStyleBackColor = true;
            this.btnTest.Click += new System.EventHandler(this.btnTest_Click);
            // 
            // bckgrndWrkMapPmt
            // 
            this.bckgrndWrkMapPmt.DoWork += new System.ComponentModel.DoWorkEventHandler(this.bckgrndWrkMapPmt_DoWork);
            // 
            // frmMapUkgPmt
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(615, 348);
            this.ControlBox = false;
            this.Controls.Add(this.btnTest);
            this.Controls.Add(this.axMap1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "frmMapUkgPmt";
            this.ShowInTaskbar = false;
            this.Text = "frmMapUkgPmt";
            this.WindowState = System.Windows.Forms.FormWindowState.Minimized;
            this.Load += new System.EventHandler(this.frmMapUkgPmt_Load);
            this.Activated += new System.EventHandler(this.frmMapUkgPmt_Activated);
            this.Resize += new System.EventHandler(this.frmMapUkgPmt_Resize);
            ((System.ComponentModel.ISupportInitialize)(this.axMap1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private AxMapXLib.AxMap axMap1;
        private System.Windows.Forms.Button btnTest;
        private System.ComponentModel.BackgroundWorker bckgrndWrkMapPmt;
    }
}