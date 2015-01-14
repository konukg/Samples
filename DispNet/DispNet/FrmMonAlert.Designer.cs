namespace DispNet
{
    partial class FrmMonAlert
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmMonAlert));
            this.c1FlxGrdObjAlert = new C1.Win.C1FlexGrid.C1FlexGrid();
            this.label4 = new System.Windows.Forms.Label();
            this.btnAlertMap = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.btnAlertInfo = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.btnAlertPmt = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.c1FlxGrdObjAlert)).BeginInit();
            this.SuspendLayout();
            // 
            // c1FlxGrdObjAlert
            // 
            this.c1FlxGrdObjAlert.ColumnInfo = resources.GetString("c1FlxGrdObjAlert.ColumnInfo");
            this.c1FlxGrdObjAlert.ExtendLastCol = true;
            this.c1FlxGrdObjAlert.Location = new System.Drawing.Point(12, 12);
            this.c1FlxGrdObjAlert.Name = "c1FlxGrdObjAlert";
            this.c1FlxGrdObjAlert.Rows.Count = 1;
            this.c1FlxGrdObjAlert.Rows.DefaultSize = 17;
            this.c1FlxGrdObjAlert.ShowCellLabels = true;
            this.c1FlxGrdObjAlert.Size = new System.Drawing.Size(382, 89);
            this.c1FlxGrdObjAlert.TabIndex = 15;
            this.c1FlxGrdObjAlert.AfterEdit += new C1.Win.C1FlexGrid.RowColEventHandler(this.c1FlxGrdObjAlert_AfterEdit);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(446, 86);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(75, 13);
            this.label4.TabIndex = 35;
            this.label4.Text = "Карта города";
            this.label4.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // btnAlertMap
            // 
            this.btnAlertMap.Image = global::DispNet.Properties.Resources.ico1309;
            this.btnAlertMap.Location = new System.Drawing.Point(417, 80);
            this.btnAlertMap.Name = "btnAlertMap";
            this.btnAlertMap.Size = new System.Drawing.Size(21, 20);
            this.btnAlertMap.TabIndex = 34;
            this.btnAlertMap.UseVisualStyleBackColor = true;
            this.btnAlertMap.Click += new System.EventHandler(this.btnAlertMap_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(446, 49);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(90, 13);
            this.label3.TabIndex = 33;
            this.label3.Text = "Характеристики";
            // 
            // btnAlertInfo
            // 
            this.btnAlertInfo.Image = global::DispNet.Properties.Resources.ico2644;
            this.btnAlertInfo.Location = new System.Drawing.Point(417, 45);
            this.btnAlertInfo.Name = "btnAlertInfo";
            this.btnAlertInfo.Size = new System.Drawing.Size(23, 21);
            this.btnAlertInfo.TabIndex = 32;
            this.btnAlertInfo.UseVisualStyleBackColor = true;
            this.btnAlertInfo.Click += new System.EventHandler(this.btnAlertInfo_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(446, 15);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(99, 13);
            this.label1.TabIndex = 31;
            this.label1.Text = "Данные в путевку";
            // 
            // btnAlertPmt
            // 
            this.btnAlertPmt.Image = global::DispNet.Properties.Resources.FIRE1_16;
            this.btnAlertPmt.Location = new System.Drawing.Point(417, 11);
            this.btnAlertPmt.Name = "btnAlertPmt";
            this.btnAlertPmt.Size = new System.Drawing.Size(23, 21);
            this.btnAlertPmt.TabIndex = 30;
            this.btnAlertPmt.UseVisualStyleBackColor = true;
            this.btnAlertPmt.Click += new System.EventHandler(this.btnAlertPmt_Click);
            // 
            // FrmMonAlert
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(559, 116);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.btnAlertMap);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.btnAlertInfo);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnAlertPmt);
            this.Controls.Add(this.c1FlxGrdObjAlert);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.Name = "FrmMonAlert";
            this.Text = "Сообщение с объекта охраны";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.FrmMonAlert_FormClosed);
            this.Load += new System.EventHandler(this.FrmMonAlert_Load);
            ((System.ComponentModel.ISupportInitialize)(this.c1FlxGrdObjAlert)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private C1.Win.C1FlexGrid.C1FlexGrid c1FlxGrdObjAlert;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button btnAlertMap;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button btnAlertInfo;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnAlertPmt;
    }
}