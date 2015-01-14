namespace GpsUkgIcs
{
    partial class FrmConnTCP
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmConnTCP));
            this.label1 = new System.Windows.Forms.Label();
            this.c1TxtBoxSrvIp = new C1.Win.C1Input.C1TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.c1TxtBoxPort = new C1.Win.C1Input.C1TextBox();
            this.c1BtnTcp = new C1.Win.C1Input.C1Button();
            ((System.ComponentModel.ISupportInitialize)(this.c1TxtBoxSrvIp)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.c1TxtBoxPort)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 14);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(50, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "IP адрес";
            // 
            // c1TxtBoxSrvIp
            // 
            this.c1TxtBoxSrvIp.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(177)))), ((int)(((byte)(187)))), ((int)(((byte)(198)))));
            this.c1TxtBoxSrvIp.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.c1TxtBoxSrvIp.Location = new System.Drawing.Point(68, 12);
            this.c1TxtBoxSrvIp.Name = "c1TxtBoxSrvIp";
            this.c1TxtBoxSrvIp.Size = new System.Drawing.Size(90, 18);
            this.c1TxtBoxSrvIp.TabIndex = 1;
            this.c1TxtBoxSrvIp.Tag = null;
            this.c1TxtBoxSrvIp.VisualStyle = C1.Win.C1Input.VisualStyle.Office2010Silver;
            this.c1TxtBoxSrvIp.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.Office2010Silver;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(164, 14);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(32, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "Порт";
            // 
            // c1TxtBoxPort
            // 
            this.c1TxtBoxPort.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(177)))), ((int)(((byte)(187)))), ((int)(((byte)(198)))));
            this.c1TxtBoxPort.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.c1TxtBoxPort.Location = new System.Drawing.Point(202, 12);
            this.c1TxtBoxPort.Name = "c1TxtBoxPort";
            this.c1TxtBoxPort.Size = new System.Drawing.Size(45, 18);
            this.c1TxtBoxPort.TabIndex = 3;
            this.c1TxtBoxPort.Tag = null;
            this.c1TxtBoxPort.VisualStyle = C1.Win.C1Input.VisualStyle.Office2010Silver;
            this.c1TxtBoxPort.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.Office2010Silver;
            // 
            // c1BtnTcp
            // 
            this.c1BtnTcp.Location = new System.Drawing.Point(202, 48);
            this.c1BtnTcp.Name = "c1BtnTcp";
            this.c1BtnTcp.Size = new System.Drawing.Size(45, 20);
            this.c1BtnTcp.TabIndex = 4;
            this.c1BtnTcp.Text = "Тест";
            this.c1BtnTcp.UseVisualStyleBackColor = true;
            this.c1BtnTcp.VisualStyle = C1.Win.C1Input.VisualStyle.Office2010Silver;
            this.c1BtnTcp.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.Office2010Silver;
            this.c1BtnTcp.Click += new System.EventHandler(this.c1BtnTcp_Click);
            // 
            // FrmConnTCP
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(264, 80);
            this.Controls.Add(this.c1BtnTcp);
            this.Controls.Add(this.c1TxtBoxPort);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.c1TxtBoxSrvIp);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FrmConnTCP";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Сервер TCP";
            this.Activated += new System.EventHandler(this.FrmConnTCP_Activated);
            this.Load += new System.EventHandler(this.FrmConnTCP_Load);
            ((System.ComponentModel.ISupportInitialize)(this.c1TxtBoxSrvIp)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.c1TxtBoxPort)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private C1.Win.C1Input.C1TextBox c1TxtBoxSrvIp;
        private System.Windows.Forms.Label label2;
        private C1.Win.C1Input.C1TextBox c1TxtBoxPort;
        private C1.Win.C1Input.C1Button c1BtnTcp;
    }
}