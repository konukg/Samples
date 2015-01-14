namespace DispNet
{
    partial class FrmMsgService
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
            C1.Win.C1List.Style style9 = new C1.Win.C1List.Style();
            C1.Win.C1List.Style style10 = new C1.Win.C1List.Style();
            C1.Win.C1List.Style style11 = new C1.Win.C1List.Style();
            C1.Win.C1List.Style style12 = new C1.Win.C1List.Style();
            C1.Win.C1List.Style style13 = new C1.Win.C1List.Style();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmMsgService));
            C1.Win.C1List.Style style14 = new C1.Win.C1List.Style();
            C1.Win.C1List.Style style15 = new C1.Win.C1List.Style();
            C1.Win.C1List.Style style16 = new C1.Win.C1List.Style();
            this.c1CmbSrvName = new C1.Win.C1List.C1Combo();
            this.txtBoxMsgPass = new System.Windows.Forms.TextBox();
            this.txtBoxMsgText = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.btnRecMsgSrv = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.c1CmbSrvName)).BeginInit();
            this.SuspendLayout();
            // 
            // c1CmbSrvName
            // 
            this.c1CmbSrvName.AddItemSeparator = ';';
            this.c1CmbSrvName.AutoCompletion = true;
            this.c1CmbSrvName.AutoDropDown = true;
            this.c1CmbSrvName.Caption = "";
            this.c1CmbSrvName.CaptionHeight = 17;
            this.c1CmbSrvName.CaptionStyle = style9;
            this.c1CmbSrvName.CharacterCasing = System.Windows.Forms.CharacterCasing.Normal;
            this.c1CmbSrvName.ColumnCaptionHeight = 17;
            this.c1CmbSrvName.ColumnFooterHeight = 17;
            this.c1CmbSrvName.ContentHeight = 15;
            this.c1CmbSrvName.DeadAreaBackColor = System.Drawing.Color.Empty;
            this.c1CmbSrvName.EditorBackColor = System.Drawing.SystemColors.Window;
            this.c1CmbSrvName.EditorFont = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.c1CmbSrvName.EditorForeColor = System.Drawing.SystemColors.WindowText;
            this.c1CmbSrvName.EditorHeight = 15;
            this.c1CmbSrvName.EvenRowStyle = style10;
            this.c1CmbSrvName.ExtendRightColumn = true;
            this.c1CmbSrvName.FooterStyle = style11;
            this.c1CmbSrvName.HeadingStyle = style12;
            this.c1CmbSrvName.HighLightRowStyle = style13;
            this.c1CmbSrvName.Images.Add(((System.Drawing.Image)(resources.GetObject("c1CmbSrvName.Images"))));
            this.c1CmbSrvName.ItemHeight = 15;
            this.c1CmbSrvName.Location = new System.Drawing.Point(12, 27);
            this.c1CmbSrvName.MatchEntryTimeout = ((long)(2000));
            this.c1CmbSrvName.MaxDropDownItems = ((short)(5));
            this.c1CmbSrvName.MaxLength = 32767;
            this.c1CmbSrvName.MouseCursor = System.Windows.Forms.Cursors.Default;
            this.c1CmbSrvName.Name = "c1CmbSrvName";
            this.c1CmbSrvName.OddRowStyle = style14;
            this.c1CmbSrvName.RowDivider.Color = System.Drawing.Color.DarkGray;
            this.c1CmbSrvName.RowDivider.Style = C1.Win.C1List.LineStyleEnum.None;
            this.c1CmbSrvName.RowSubDividerColor = System.Drawing.Color.DarkGray;
            this.c1CmbSrvName.SelectedStyle = style15;
            this.c1CmbSrvName.Size = new System.Drawing.Size(236, 21);
            this.c1CmbSrvName.Style = style16;
            this.c1CmbSrvName.TabIndex = 1;
            this.c1CmbSrvName.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.c1CmbSrvName_KeyPress);
            this.c1CmbSrvName.PropBag = resources.GetString("c1CmbSrvName.PropBag");
            // 
            // txtBoxMsgPass
            // 
            this.txtBoxMsgPass.Location = new System.Drawing.Point(12, 67);
            this.txtBoxMsgPass.Name = "txtBoxMsgPass";
            this.txtBoxMsgPass.Size = new System.Drawing.Size(236, 20);
            this.txtBoxMsgPass.TabIndex = 2;
            this.txtBoxMsgPass.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtBoxMsgPass_KeyPress);
            // 
            // txtBoxMsgText
            // 
            this.txtBoxMsgText.Location = new System.Drawing.Point(12, 106);
            this.txtBoxMsgText.Multiline = true;
            this.txtBoxMsgText.Name = "txtBoxMsgText";
            this.txtBoxMsgText.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtBoxMsgText.Size = new System.Drawing.Size(236, 68);
            this.txtBoxMsgText.TabIndex = 3;
            this.txtBoxMsgText.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtBoxMsgText_KeyPress);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(98, 11);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(57, 13);
            this.label1.TabIndex = 4;
            this.label1.Text = "С/Служба";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(101, 51);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(51, 13);
            this.label2.TabIndex = 5;
            this.label2.Text = "Передал";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(78, 90);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(97, 13);
            this.label3.TabIndex = 6;
            this.label3.Text = "Текст сообщения";
            // 
            // btnRecMsgSrv
            // 
            this.btnRecMsgSrv.Location = new System.Drawing.Point(101, 185);
            this.btnRecMsgSrv.Name = "btnRecMsgSrv";
            this.btnRecMsgSrv.Size = new System.Drawing.Size(52, 22);
            this.btnRecMsgSrv.TabIndex = 0;
            this.btnRecMsgSrv.Text = "Запись";
            this.btnRecMsgSrv.UseVisualStyleBackColor = true;
            this.btnRecMsgSrv.Click += new System.EventHandler(this.btnRecMsgSrv_Click);
            // 
            // FrmMsgService
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(262, 218);
            this.Controls.Add(this.btnRecMsgSrv);
            this.Controls.Add(this.txtBoxMsgText);
            this.Controls.Add(this.txtBoxMsgPass);
            this.Controls.Add(this.c1CmbSrvName);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.label3);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FrmMsgService";
            this.Text = "Сообщение - спец\\службы";
            this.Load += new System.EventHandler(this.FrmMsgService_Load);
            ((System.ComponentModel.ISupportInitialize)(this.c1CmbSrvName)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private C1.Win.C1List.C1Combo c1CmbSrvName;
        private System.Windows.Forms.TextBox txtBoxMsgPass;
        private System.Windows.Forms.TextBox txtBoxMsgText;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button btnRecMsgSrv;
    }
}