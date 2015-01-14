namespace GpsUkgIcs
{
    partial class FrmConnSQL
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmConnSQL));
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.c1TxtBoxSqlName = new C1.Win.C1Input.C1TextBox();
            this.c1TxtBoxBase = new C1.Win.C1Input.C1TextBox();
            this.c1TxtBoxLog = new C1.Win.C1Input.C1TextBox();
            this.c1TxtBoxPass = new C1.Win.C1Input.C1TextBox();
            this.c1BtnTestSQL = new C1.Win.C1Input.C1Button();
            ((System.ComponentModel.ISupportInitialize)(this.c1TxtBoxSqlName)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.c1TxtBoxBase)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.c1TxtBoxLog)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.c1TxtBoxPass)).BeginInit();
            this.SuspendLayout();
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(34, 93);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(45, 13);
            this.label4.TabIndex = 16;
            this.label4.Text = "Пароль";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(41, 69);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(38, 13);
            this.label3.TabIndex = 15;
            this.label3.Text = "Логин";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(7, 45);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(72, 13);
            this.label2.TabIndex = 14;
            this.label2.Text = "База данных";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 14);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(67, 13);
            this.label1.TabIndex = 13;
            this.label1.Text = "SQL сервер";
            // 
            // c1TxtBoxSqlName
            // 
            this.c1TxtBoxSqlName.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(177)))), ((int)(((byte)(187)))), ((int)(((byte)(198)))));
            this.c1TxtBoxSqlName.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.c1TxtBoxSqlName.Location = new System.Drawing.Point(85, 12);
            this.c1TxtBoxSqlName.Name = "c1TxtBoxSqlName";
            this.c1TxtBoxSqlName.Size = new System.Drawing.Size(150, 18);
            this.c1TxtBoxSqlName.TabIndex = 17;
            this.c1TxtBoxSqlName.Tag = null;
            this.c1TxtBoxSqlName.VisualStyle = C1.Win.C1Input.VisualStyle.Office2010Silver;
            this.c1TxtBoxSqlName.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.Office2010Silver;
            // 
            // c1TxtBoxBase
            // 
            this.c1TxtBoxBase.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(177)))), ((int)(((byte)(187)))), ((int)(((byte)(198)))));
            this.c1TxtBoxBase.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.c1TxtBoxBase.Location = new System.Drawing.Point(85, 43);
            this.c1TxtBoxBase.Name = "c1TxtBoxBase";
            this.c1TxtBoxBase.Size = new System.Drawing.Size(100, 18);
            this.c1TxtBoxBase.TabIndex = 18;
            this.c1TxtBoxBase.Tag = null;
            this.c1TxtBoxBase.VisualStyle = C1.Win.C1Input.VisualStyle.Office2010Silver;
            this.c1TxtBoxBase.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.Office2010Silver;
            // 
            // c1TxtBoxLog
            // 
            this.c1TxtBoxLog.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(177)))), ((int)(((byte)(187)))), ((int)(((byte)(198)))));
            this.c1TxtBoxLog.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.c1TxtBoxLog.Location = new System.Drawing.Point(85, 67);
            this.c1TxtBoxLog.Name = "c1TxtBoxLog";
            this.c1TxtBoxLog.Size = new System.Drawing.Size(74, 18);
            this.c1TxtBoxLog.TabIndex = 19;
            this.c1TxtBoxLog.Tag = null;
            this.c1TxtBoxLog.VisualStyle = C1.Win.C1Input.VisualStyle.Office2010Silver;
            this.c1TxtBoxLog.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.Office2010Silver;
            // 
            // c1TxtBoxPass
            // 
            this.c1TxtBoxPass.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(177)))), ((int)(((byte)(187)))), ((int)(((byte)(198)))));
            this.c1TxtBoxPass.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.c1TxtBoxPass.Location = new System.Drawing.Point(85, 91);
            this.c1TxtBoxPass.Name = "c1TxtBoxPass";
            this.c1TxtBoxPass.PasswordChar = '*';
            this.c1TxtBoxPass.Size = new System.Drawing.Size(74, 18);
            this.c1TxtBoxPass.TabIndex = 20;
            this.c1TxtBoxPass.Tag = null;
            this.c1TxtBoxPass.VisualStyle = C1.Win.C1Input.VisualStyle.Office2010Silver;
            this.c1TxtBoxPass.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.Office2010Silver;
            // 
            // c1BtnTestSQL
            // 
            this.c1BtnTestSQL.Location = new System.Drawing.Point(184, 91);
            this.c1BtnTestSQL.Name = "c1BtnTestSQL";
            this.c1BtnTestSQL.Size = new System.Drawing.Size(51, 18);
            this.c1BtnTestSQL.TabIndex = 21;
            this.c1BtnTestSQL.Text = "Тест";
            this.c1BtnTestSQL.UseVisualStyleBackColor = true;
            this.c1BtnTestSQL.VisualStyle = C1.Win.C1Input.VisualStyle.Office2010Silver;
            this.c1BtnTestSQL.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.Office2010Silver;
            this.c1BtnTestSQL.Click += new System.EventHandler(this.c1BtnTestSQL_Click);
            // 
            // FrmConnSQL
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(253, 121);
            this.Controls.Add(this.c1BtnTestSQL);
            this.Controls.Add(this.c1TxtBoxPass);
            this.Controls.Add(this.c1TxtBoxLog);
            this.Controls.Add(this.c1TxtBoxBase);
            this.Controls.Add(this.c1TxtBoxSqlName);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FrmConnSQL";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Сервер SQL";
            this.Activated += new System.EventHandler(this.FrmConnSQL_Activated);
            this.Load += new System.EventHandler(this.FrmConnSQL_Load);
            ((System.ComponentModel.ISupportInitialize)(this.c1TxtBoxSqlName)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.c1TxtBoxBase)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.c1TxtBoxLog)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.c1TxtBoxPass)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private C1.Win.C1Input.C1TextBox c1TxtBoxSqlName;
        private C1.Win.C1Input.C1TextBox c1TxtBoxBase;
        private C1.Win.C1Input.C1TextBox c1TxtBoxLog;
        private C1.Win.C1Input.C1TextBox c1TxtBoxPass;
        private C1.Win.C1Input.C1Button c1BtnTestSQL;
    }
}