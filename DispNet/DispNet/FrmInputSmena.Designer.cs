namespace DispNet
{
    partial class FrmInputSmena
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
            this.btnRecSmn = new System.Windows.Forms.Button();
            this.cmbBoxSmn = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // btnRecSmn
            // 
            this.btnRecSmn.Location = new System.Drawing.Point(90, 25);
            this.btnRecSmn.Name = "btnRecSmn";
            this.btnRecSmn.Size = new System.Drawing.Size(60, 22);
            this.btnRecSmn.TabIndex = 0;
            this.btnRecSmn.Text = "Запись";
            this.btnRecSmn.UseVisualStyleBackColor = true;
            this.btnRecSmn.Click += new System.EventHandler(this.btnRecSmn_Click);
            // 
            // cmbBoxSmn
            // 
            this.cmbBoxSmn.FormattingEnabled = true;
            this.cmbBoxSmn.Items.AddRange(new object[] {
            "1",
            "2",
            "3",
            "4"});
            this.cmbBoxSmn.Location = new System.Drawing.Point(12, 25);
            this.cmbBoxSmn.Name = "cmbBoxSmn";
            this.cmbBoxSmn.Size = new System.Drawing.Size(49, 21);
            this.cmbBoxSmn.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(25, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(18, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "№";
            // 
            // FrmInputSmena
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(177, 66);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.cmbBoxSmn);
            this.Controls.Add(this.btnRecSmn);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FrmInputSmena";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Ввод номера смены";
            this.Load += new System.EventHandler(this.FrmInputSmena_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnRecSmn;
        private System.Windows.Forms.ComboBox cmbBoxSmn;
        private System.Windows.Forms.Label label1;
    }
}