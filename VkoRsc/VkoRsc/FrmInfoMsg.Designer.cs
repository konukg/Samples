namespace VkoRsc
{
    partial class FrmInfoMsg
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmInfoMsg));
            this.c1SprLbInfoMsg = new C1.Win.C1SuperTooltip.C1SuperLabel();
            this.SuspendLayout();
            // 
            // c1SprLbInfoMsg
            // 
            this.c1SprLbInfoMsg.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.c1SprLbInfoMsg.Location = new System.Drawing.Point(12, 12);
            this.c1SprLbInfoMsg.Name = "c1SprLbInfoMsg";
            this.c1SprLbInfoMsg.Size = new System.Drawing.Size(313, 111);
            this.c1SprLbInfoMsg.TabIndex = 0;
            this.c1SprLbInfoMsg.UseMnemonic = true;
            // 
            // FrmInfoMsg
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(340, 137);
            this.Controls.Add(this.c1SprLbInfoMsg);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FrmInfoMsg";
            this.Opacity = 0.8D;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Водно-спасательная служба";
            this.Load += new System.EventHandler(this.FrmInfoMsg_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private C1.Win.C1SuperTooltip.C1SuperLabel c1SprLbInfoMsg;
    }
}