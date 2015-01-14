namespace DispNet
{
    partial class FrmTechNoUnit
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmTechNoUnit));
            C1.Win.C1List.Style style2 = new C1.Win.C1List.Style();
            C1.Win.C1List.Style style3 = new C1.Win.C1List.Style();
            C1.Win.C1List.Style style4 = new C1.Win.C1List.Style();
            C1.Win.C1List.Style style5 = new C1.Win.C1List.Style();
            C1.Win.C1List.Style style6 = new C1.Win.C1List.Style();
            C1.Win.C1List.Style style7 = new C1.Win.C1List.Style();
            C1.Win.C1List.Style style8 = new C1.Win.C1List.Style();
            this.c1FlxGrdTechNoUnit = new C1.Win.C1FlexGrid.C1FlexGrid();
            this.ListState = new C1.Win.C1List.C1List();
            ((System.ComponentModel.ISupportInitialize)(this.c1FlxGrdTechNoUnit)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ListState)).BeginInit();
            this.SuspendLayout();
            // 
            // c1FlxGrdTechNoUnit
            // 
            this.c1FlxGrdTechNoUnit.ColumnInfo = "10,1,0,0,0,85,Columns:0{Width:21;}\t1{Width:49;}\t2{Width:47;}\t3{Width:44;}\t4{Width" +
                ":41;}\t5{Width:50;}\t6{Width:41;}\t7{Width:48;}\t";
            this.c1FlxGrdTechNoUnit.ExtendLastCol = true;
            this.c1FlxGrdTechNoUnit.Location = new System.Drawing.Point(12, 12);
            this.c1FlxGrdTechNoUnit.Name = "c1FlxGrdTechNoUnit";
            this.c1FlxGrdTechNoUnit.Rows.DefaultSize = 17;
            this.c1FlxGrdTechNoUnit.ShowCellLabels = true;
            this.c1FlxGrdTechNoUnit.Size = new System.Drawing.Size(544, 194);
            this.c1FlxGrdTechNoUnit.TabIndex = 0;
            this.c1FlxGrdTechNoUnit.ComboDropDown += new C1.Win.C1FlexGrid.RowColEventHandler(this.c1FlxGrdTechNoUnit_ComboDropDown);
            this.c1FlxGrdTechNoUnit.AfterEdit += new C1.Win.C1FlexGrid.RowColEventHandler(this.c1FlxGrdTechNoUnit_AfterEdit);
            // 
            // ListState
            // 
            this.ListState.AddItemSeparator = ';';
            this.ListState.AllowColMove = false;
            this.ListState.AllowColSelect = false;
            this.ListState.AllowSort = false;
            this.ListState.CaptionHeight = 17;
            this.ListState.CaptionStyle = style1;
            this.ListState.ColumnCaptionHeight = 17;
            this.ListState.ColumnFooterHeight = 17;
            this.ListState.DataMode = C1.Win.C1List.DataModeEnum.AddItem;
            this.ListState.DeadAreaBackColor = System.Drawing.SystemColors.ControlDark;
            this.ListState.EvenRowStyle = style2;
            this.ListState.ExtendRightColumn = true;
            this.ListState.FooterStyle = style3;
            this.ListState.HeadingStyle = style4;
            this.ListState.HighLightRowStyle = style5;
            this.ListState.Images.Add(((System.Drawing.Image)(resources.GetObject("ListState.Images"))));
            this.ListState.ItemHeight = 15;
            this.ListState.Location = new System.Drawing.Point(399, 124);
            this.ListState.MatchEntryTimeout = ((long)(2000));
            this.ListState.Name = "ListState";
            this.ListState.OddRowStyle = style6;
            this.ListState.RowDivider.Color = System.Drawing.Color.DarkGray;
            this.ListState.RowDivider.Style = C1.Win.C1List.LineStyleEnum.None;
            this.ListState.RowSubDividerColor = System.Drawing.Color.DarkGray;
            this.ListState.SelectedStyle = style7;
            this.ListState.SelectionMode = C1.Win.C1List.SelectionModeEnum.None;
            this.ListState.Size = new System.Drawing.Size(114, 59);
            this.ListState.Style = style8;
            this.ListState.TabIndex = 4;
            this.ListState.Text = "c1List1";
            this.ListState.Visible = false;
            this.ListState.PropBag = resources.GetString("ListState.PropBag");
            // 
            // FrmTechNoUnit
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(566, 217);
            this.Controls.Add(this.ListState);
            this.Controls.Add(this.c1FlxGrdTechNoUnit);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "FrmTechNoUnit";
            this.Text = "Техника на выезде";
            this.Load += new System.EventHandler(this.FrmTechNoUnit_Load);
            ((System.ComponentModel.ISupportInitialize)(this.c1FlxGrdTechNoUnit)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ListState)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private C1.Win.C1FlexGrid.C1FlexGrid c1FlxGrdTechNoUnit;
        private C1.Win.C1List.C1List ListState;
    }
}