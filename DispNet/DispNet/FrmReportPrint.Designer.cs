namespace DispNet
{
    partial class FrmReportPrint
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmReportPrint));
            this.c1PrntPreCntrRpt = new C1.Win.C1Preview.C1PrintPreviewControl();
            this.c1RptFire = new C1.Win.C1Report.C1Report();
            ((System.ComponentModel.ISupportInitialize)(this.c1PrntPreCntrRpt.PreviewPane)).BeginInit();
            this.c1PrntPreCntrRpt.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.c1RptFire)).BeginInit();
            this.SuspendLayout();
            // 
            // c1PrntPreCntrRpt
            // 
            this.c1PrntPreCntrRpt.Location = new System.Drawing.Point(3, 3);
            this.c1PrntPreCntrRpt.Name = "c1PrntPreCntrRpt";
            this.c1PrntPreCntrRpt.NavigationPanelVisible = false;
            // 
            // c1PrntPreCntrRpt.OutlineView
            // 
            this.c1PrntPreCntrRpt.PreviewOutlineView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.c1PrntPreCntrRpt.PreviewOutlineView.Location = new System.Drawing.Point(0, 0);
            this.c1PrntPreCntrRpt.PreviewOutlineView.Name = "OutlineView";
            this.c1PrntPreCntrRpt.PreviewOutlineView.Size = new System.Drawing.Size(165, 427);
            this.c1PrntPreCntrRpt.PreviewOutlineView.TabIndex = 0;
            // 
            // c1PrntPreCntrRpt.PreviewPane
            // 
            this.c1PrntPreCntrRpt.PreviewPane.IntegrateExternalTools = true;
            this.c1PrntPreCntrRpt.PreviewPane.TabIndex = 0;
            // 
            // c1PrntPreCntrRpt.PreviewTextSearchPanel
            // 
            this.c1PrntPreCntrRpt.PreviewTextSearchPanel.Dock = System.Windows.Forms.DockStyle.Right;
            this.c1PrntPreCntrRpt.PreviewTextSearchPanel.Location = new System.Drawing.Point(550, 0);
            this.c1PrntPreCntrRpt.PreviewTextSearchPanel.MinimumSize = new System.Drawing.Size(180, 240);
            this.c1PrntPreCntrRpt.PreviewTextSearchPanel.Name = "PreviewTextSearchPanel";
            this.c1PrntPreCntrRpt.PreviewTextSearchPanel.Size = new System.Drawing.Size(180, 453);
            this.c1PrntPreCntrRpt.PreviewTextSearchPanel.TabIndex = 0;
            this.c1PrntPreCntrRpt.PreviewTextSearchPanel.Visible = false;
            // 
            // c1PrntPreCntrRpt.ThumbnailView
            // 
            this.c1PrntPreCntrRpt.PreviewThumbnailView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.c1PrntPreCntrRpt.PreviewThumbnailView.HideSelection = false;
            this.c1PrntPreCntrRpt.PreviewThumbnailView.Location = new System.Drawing.Point(0, 0);
            this.c1PrntPreCntrRpt.PreviewThumbnailView.Name = "ThumbnailView";
            this.c1PrntPreCntrRpt.PreviewThumbnailView.OwnerDraw = true;
            this.c1PrntPreCntrRpt.PreviewThumbnailView.Size = new System.Drawing.Size(165, 427);
            this.c1PrntPreCntrRpt.PreviewThumbnailView.TabIndex = 0;
            this.c1PrntPreCntrRpt.PreviewThumbnailView.ThumbnailSize = new System.Drawing.Size(96, 96);
            this.c1PrntPreCntrRpt.PreviewThumbnailView.UseImageAsThumbnail = false;
            this.c1PrntPreCntrRpt.Size = new System.Drawing.Size(726, 439);
            this.c1PrntPreCntrRpt.TabIndex = 0;
            this.c1PrntPreCntrRpt.Text = "c1PrintPreviewControl1";
            // 
            // c1RptFire
            // 
            this.c1RptFire.ReportDefinition = resources.GetString("c1RptFire.ReportDefinition");
            this.c1RptFire.ReportName = "New Report";
            // 
            // FrmReportPrint
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(742, 455);
            this.Controls.Add(this.c1PrntPreCntrRpt);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "FrmReportPrint";
            this.Text = "FrmReportPrint";
            this.Load += new System.EventHandler(this.FrmReportPrint_Load);
            this.Resize += new System.EventHandler(this.FrmReportPrint_Resize);
            ((System.ComponentModel.ISupportInitialize)(this.c1PrntPreCntrRpt.PreviewPane)).EndInit();
            this.c1PrntPreCntrRpt.ResumeLayout(false);
            this.c1PrntPreCntrRpt.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.c1RptFire)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private C1.Win.C1Preview.C1PrintPreviewControl c1PrntPreCntrRpt;
        private C1.Win.C1Report.C1Report c1RptFire;

    }
}