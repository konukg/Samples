namespace DispNet
{
    partial class FrmChsCatDrv
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
            C1.Win.C1List.Style style1 = new C1.Win.C1List.Style();
            C1.Win.C1List.Style style2 = new C1.Win.C1List.Style();
            C1.Win.C1List.Style style3 = new C1.Win.C1List.Style();
            C1.Win.C1List.Style style4 = new C1.Win.C1List.Style();
            C1.Win.C1List.Style style5 = new C1.Win.C1List.Style();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmChsCatDrv));
            C1.Win.C1List.Style style6 = new C1.Win.C1List.Style();
            C1.Win.C1List.Style style7 = new C1.Win.C1List.Style();
            C1.Win.C1List.Style style8 = new C1.Win.C1List.Style();
            this.m_grpBoxCat = new System.Windows.Forms.GroupBox();
            this.btnRecDataFrm = new System.Windows.Forms.Button();
            this.c1CmbCatType = new C1.Win.C1List.C1Combo();
            this.c1CommandHolder1 = new C1.Win.C1Command.C1CommandHolder();
            this.m_grpBoxCat.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.c1CmbCatType)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.c1CommandHolder1)).BeginInit();
            this.SuspendLayout();
            // 
            // m_grpBoxCat
            // 
            this.m_grpBoxCat.Controls.Add(this.btnRecDataFrm);
            this.m_grpBoxCat.Controls.Add(this.c1CmbCatType);
            this.m_grpBoxCat.Location = new System.Drawing.Point(12, 12);
            this.m_grpBoxCat.Name = "m_grpBoxCat";
            this.m_grpBoxCat.Size = new System.Drawing.Size(361, 77);
            this.m_grpBoxCat.TabIndex = 0;
            this.m_grpBoxCat.TabStop = false;
            // 
            // btnRecDataFrm
            // 
            this.btnRecDataFrm.Location = new System.Drawing.Point(155, 46);
            this.btnRecDataFrm.Name = "btnRecDataFrm";
            this.btnRecDataFrm.Size = new System.Drawing.Size(62, 21);
            this.btnRecDataFrm.TabIndex = 36;
            this.btnRecDataFrm.Text = "Запись";
            this.btnRecDataFrm.UseVisualStyleBackColor = true;
            this.btnRecDataFrm.Click += new System.EventHandler(this.btnRecDataFrm_Click);
            // 
            // c1CmbCatType
            // 
            this.c1CmbCatType.AddItemSeparator = ';';
            this.c1CmbCatType.AllowColMove = false;
            this.c1CmbCatType.AutoCompletion = true;
            this.c1CmbCatType.AutoDropDown = true;
            this.c1CmbCatType.Caption = "";
            this.c1CmbCatType.CaptionHeight = 17;
            this.c1CmbCatType.CaptionStyle = style1;
            this.c1CmbCatType.CharacterCasing = System.Windows.Forms.CharacterCasing.Normal;
            this.c1CmbCatType.ColumnCaptionHeight = 17;
            this.c1CmbCatType.ColumnFooterHeight = 17;
            this.c1CmbCatType.ContentHeight = 15;
            this.c1CmbCatType.DeadAreaBackColor = System.Drawing.Color.Empty;
            this.c1CmbCatType.EditorBackColor = System.Drawing.SystemColors.Window;
            this.c1CmbCatType.EditorFont = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.c1CmbCatType.EditorForeColor = System.Drawing.SystemColors.WindowText;
            this.c1CmbCatType.EditorHeight = 15;
            this.c1CmbCatType.EvenRowStyle = style2;
            this.c1CmbCatType.ExtendRightColumn = true;
            this.c1CmbCatType.FooterStyle = style3;
            this.c1CmbCatType.HeadingStyle = style4;
            this.c1CmbCatType.HighLightRowStyle = style5;
            this.c1CmbCatType.Images.Add(((System.Drawing.Image)(resources.GetObject("c1CmbCatType.Images"))));
            this.c1CmbCatType.ItemHeight = 15;
            this.c1CmbCatType.Location = new System.Drawing.Point(6, 19);
            this.c1CmbCatType.MatchEntryTimeout = ((long)(2000));
            this.c1CmbCatType.MaxDropDownItems = ((short)(5));
            this.c1CmbCatType.MaxLength = 32767;
            this.c1CmbCatType.MouseCursor = System.Windows.Forms.Cursors.Default;
            this.c1CmbCatType.Name = "c1CmbCatType";
            this.c1CmbCatType.OddRowStyle = style6;
            this.c1CmbCatType.RowDivider.Color = System.Drawing.Color.DarkGray;
            this.c1CmbCatType.RowDivider.Style = C1.Win.C1List.LineStyleEnum.None;
            this.c1CmbCatType.RowSubDividerColor = System.Drawing.Color.DarkGray;
            this.c1CmbCatType.SelectedStyle = style7;
            this.c1CmbCatType.Size = new System.Drawing.Size(349, 21);
            this.c1CmbCatType.Style = style8;
            this.c1CmbCatType.TabIndex = 35;
            this.c1CmbCatType.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.c1CmbCatType_KeyPress);
            this.c1CmbCatType.Close += new C1.Win.C1List.CloseEventHandler(this.c1CmbCatType_Close);
            this.c1CmbCatType.FetchCellTips += new C1.Win.C1List.FetchCellTipsEventHandler(this.c1CmbCatType_FetchCellTips);
            this.c1CmbCatType.BeforeOpen += new System.ComponentModel.CancelEventHandler(this.c1CmbCatType_BeforeOpen);
            this.c1CmbCatType.PropBag = resources.GetString("c1CmbCatType.PropBag");
            // 
            // c1CommandHolder1
            // 
            this.c1CommandHolder1.Owner = this;
            // 
            // FrmChsCatDrv
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(383, 97);
            this.Controls.Add(this.m_grpBoxCat);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FrmChsCatDrv";
            this.Load += new System.EventHandler(this.FrmChsCatDrv_Load);
            this.m_grpBoxCat.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.c1CmbCatType)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.c1CommandHolder1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private C1.Win.C1List.C1Combo c1CmbCatType;
        public System.Windows.Forms.GroupBox m_grpBoxCat;
        private System.Windows.Forms.Button btnRecDataFrm;
        private C1.Win.C1Command.C1CommandHolder c1CommandHolder1;
    }
}