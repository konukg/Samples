namespace DispNet
{
    partial class FrmReportPrintArch
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmReportPrintArch));
            C1.Win.C1List.Style style2 = new C1.Win.C1List.Style();
            C1.Win.C1List.Style style3 = new C1.Win.C1List.Style();
            C1.Win.C1List.Style style4 = new C1.Win.C1List.Style();
            C1.Win.C1List.Style style5 = new C1.Win.C1List.Style();
            C1.Win.C1List.Style style6 = new C1.Win.C1List.Style();
            C1.Win.C1List.Style style7 = new C1.Win.C1List.Style();
            C1.Win.C1List.Style style8 = new C1.Win.C1List.Style();
            this.c1ListRptArch = new C1.Win.C1List.C1List();
            this.btnPrnArch = new System.Windows.Forms.Button();
            this.m_chkBoxTime = new System.Windows.Forms.CheckBox();
            this.c1DateFndAr = new C1.Win.C1Input.C1DateEdit();
            this.label1 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.c1ListRptArch)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.c1DateFndAr)).BeginInit();
            this.SuspendLayout();
            // 
            // c1ListRptArch
            // 
            this.c1ListRptArch.AddItemSeparator = ';';
            this.c1ListRptArch.AllowColMove = false;
            this.c1ListRptArch.AllowColSelect = false;
            this.c1ListRptArch.Caption = "Список отчетов а архиве";
            this.c1ListRptArch.CaptionHeight = 17;
            this.c1ListRptArch.CaptionStyle = style1;
            this.c1ListRptArch.ColumnCaptionHeight = 17;
            this.c1ListRptArch.ColumnFooterHeight = 17;
            this.c1ListRptArch.ColumnWidth = 100;
            this.c1ListRptArch.DataMode = C1.Win.C1List.DataModeEnum.AddItem;
            this.c1ListRptArch.DeadAreaBackColor = System.Drawing.SystemColors.ControlDark;
            this.c1ListRptArch.EvenRowStyle = style2;
            this.c1ListRptArch.ExtendRightColumn = true;
            this.c1ListRptArch.FooterStyle = style3;
            this.c1ListRptArch.HeadingStyle = style4;
            this.c1ListRptArch.HighLightRowStyle = style5;
            this.c1ListRptArch.Images.Add(((System.Drawing.Image)(resources.GetObject("c1ListRptArch.Images"))));
            this.c1ListRptArch.ItemHeight = 15;
            this.c1ListRptArch.Location = new System.Drawing.Point(12, 12);
            this.c1ListRptArch.MatchEntryTimeout = ((long)(2000));
            this.c1ListRptArch.Name = "c1ListRptArch";
            this.c1ListRptArch.OddRowStyle = style6;
            this.c1ListRptArch.RowDivider.Color = System.Drawing.Color.DarkGray;
            this.c1ListRptArch.RowDivider.Style = C1.Win.C1List.LineStyleEnum.None;
            this.c1ListRptArch.RowSubDividerColor = System.Drawing.Color.DarkGray;
            this.c1ListRptArch.SelectedStyle = style7;
            this.c1ListRptArch.Size = new System.Drawing.Size(206, 157);
            this.c1ListRptArch.Style = style8;
            this.c1ListRptArch.TabIndex = 0;
            this.c1ListRptArch.PropBag = resources.GetString("c1ListRptArch.PropBag");
            // 
            // btnPrnArch
            // 
            this.btnPrnArch.Location = new System.Drawing.Point(244, 126);
            this.btnPrnArch.Name = "btnPrnArch";
            this.btnPrnArch.Size = new System.Drawing.Size(76, 23);
            this.btnPrnArch.TabIndex = 1;
            this.btnPrnArch.Text = "Открыть";
            this.btnPrnArch.UseVisualStyleBackColor = true;
            this.btnPrnArch.Click += new System.EventHandler(this.btnPrnArch_Click);
            // 
            // m_chkBoxTime
            // 
            this.m_chkBoxTime.AutoSize = true;
            this.m_chkBoxTime.Checked = true;
            this.m_chkBoxTime.CheckState = System.Windows.Forms.CheckState.Checked;
            this.m_chkBoxTime.Location = new System.Drawing.Point(224, 92);
            this.m_chkBoxTime.Name = "m_chkBoxTime";
            this.m_chkBoxTime.Size = new System.Drawing.Size(137, 17);
            this.m_chkBoxTime.TabIndex = 2;
            this.m_chkBoxTime.Text = "Дата и время выезда";
            this.m_chkBoxTime.UseVisualStyleBackColor = true;
            // 
            // c1DateFndAr
            // 
            // 
            // 
            // 
            this.c1DateFndAr.Calendar.UIStrings.Content = new string[] {
        "&Clear:&Очистить",
        "&Today:&Сегодня"};
            this.c1DateFndAr.Cursor = System.Windows.Forms.Cursors.Default;
            this.c1DateFndAr.CustomFormat = "dd.MM.yyyy 8:00:00";
            this.c1DateFndAr.EditFormat.CustomFormat = "dd.MM.yyyy 8:00:00";
            this.c1DateFndAr.EditFormat.Inherit = ((C1.Win.C1Input.FormatInfoInheritFlags)((((C1.Win.C1Input.FormatInfoInheritFlags.FormatType | C1.Win.C1Input.FormatInfoInheritFlags.NullText)
                        | C1.Win.C1Input.FormatInfoInheritFlags.EmptyAsNull)
                        | C1.Win.C1Input.FormatInfoInheritFlags.TrimStart)));
            this.c1DateFndAr.FormatType = C1.Win.C1Input.FormatTypeEnum.CustomFormat;
            this.c1DateFndAr.Location = new System.Drawing.Point(224, 50);
            this.c1DateFndAr.Name = "c1DateFndAr";
            this.c1DateFndAr.Size = new System.Drawing.Size(125, 20);
            this.c1DateFndAr.TabIndex = 3;
            this.c1DateFndAr.Tag = null;
            this.c1DateFndAr.VisibleButtons = C1.Win.C1Input.DropDownControlButtonFlags.DropDown;
            this.c1DateFndAr.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.c1DateFndAr_KeyPress);
            this.c1DateFndAr.DropDownClosed += new C1.Win.C1Input.DropDownClosedEventHandler(this.c1DateFndAr_DropDownClosed);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(241, 33);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(80, 13);
            this.label1.TabIndex = 4;
            this.label1.Text = "Поиск по дате";
            // 
            // FrmReportPrintArch
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(368, 179);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.c1DateFndAr);
            this.Controls.Add(this.m_chkBoxTime);
            this.Controls.Add(this.btnPrnArch);
            this.Controls.Add(this.c1ListRptArch);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FrmReportPrintArch";
            this.Text = "Архив отчетов";
            this.Load += new System.EventHandler(this.FrmReportPrintArch_Load);
            ((System.ComponentModel.ISupportInitialize)(this.c1ListRptArch)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.c1DateFndAr)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private C1.Win.C1List.C1List c1ListRptArch;
        private System.Windows.Forms.Button btnPrnArch;
        public System.Windows.Forms.CheckBox m_chkBoxTime;
        private C1.Win.C1Input.C1DateEdit c1DateFndAr;
        private System.Windows.Forms.Label label1;
    }
}