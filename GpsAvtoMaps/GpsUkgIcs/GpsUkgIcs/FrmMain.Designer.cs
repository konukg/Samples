namespace GpsUkgIcs
{
    partial class FrmMain
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmMain));
            this.c1CmndDockMain = new C1.Win.C1Command.C1CommandDock();
            this.c1DckTabMain = new C1.Win.C1Command.C1DockingTab();
            this.c1DckTabPageMain = new C1.Win.C1Command.C1DockingTabPage();
            this.c1SizerFind = new C1.Win.C1Sizer.C1Sizer();
            this.panelFind = new System.Windows.Forms.Panel();
            this.chkBoxTrace = new System.Windows.Forms.CheckBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.c1CmbBort = new C1.Win.C1List.C1Combo();
            this.c1CmbPch = new C1.Win.C1List.C1Combo();
            this.c1BtnTest = new C1.Win.C1Input.C1Button();
            this.c1FlxGrdFind = new C1.Win.C1FlexGrid.C1FlexGrid();
            this.c1SizerMap = new C1.Win.C1Sizer.C1Sizer();
            this.c1StatusBarMain = new C1.Win.C1Ribbon.C1StatusBar();
            this.rbnBtnConnSql = new C1.Win.C1Ribbon.RibbonButton();
            this.ribnLblConSql = new C1.Win.C1Ribbon.RibbonLabel();
            this.rbnBtnConnTcp = new C1.Win.C1Ribbon.RibbonButton();
            this.ribnLblConTcp = new C1.Win.C1Ribbon.RibbonLabel();
            this.axMapUkg = new AxMapXLib.AxMap();
            this.c1CntMnuGrdBort = new C1.Win.C1Command.C1ContextMenu();
            this.c1CommandLink2 = new C1.Win.C1Command.C1CommandLink();
            this.c1CmndBortTrace = new C1.Win.C1Command.C1Command();
            this.c1CommandHolder1 = new C1.Win.C1Command.C1CommandHolder();
            ((System.ComponentModel.ISupportInitialize)(this.c1CmndDockMain)).BeginInit();
            this.c1CmndDockMain.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.c1DckTabMain)).BeginInit();
            this.c1DckTabMain.SuspendLayout();
            this.c1DckTabPageMain.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.c1SizerFind)).BeginInit();
            this.c1SizerFind.SuspendLayout();
            this.panelFind.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.c1CmbBort)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.c1CmbPch)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.c1FlxGrdFind)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.c1SizerMap)).BeginInit();
            this.c1SizerMap.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.c1StatusBarMain)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.axMapUkg)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.c1CommandHolder1)).BeginInit();
            this.SuspendLayout();
            // 
            // c1CmndDockMain
            // 
            this.c1CmndDockMain.Controls.Add(this.c1DckTabMain);
            this.c1CmndDockMain.Dock = System.Windows.Forms.DockStyle.Left;
            this.c1CmndDockMain.Id = 1;
            this.c1CmndDockMain.Location = new System.Drawing.Point(0, 0);
            this.c1CmndDockMain.Name = "c1CmndDockMain";
            this.c1CmndDockMain.Size = new System.Drawing.Size(202, 722);
            // 
            // c1DckTabMain
            // 
            this.c1DckTabMain.AcceptsCtrlTab = false;
            this.c1DckTabMain.Alignment = System.Windows.Forms.TabAlignment.Bottom;
            this.c1DckTabMain.AutoHiding = true;
            this.c1DckTabMain.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.c1DckTabMain.CanAutoHide = true;
            this.c1DckTabMain.Controls.Add(this.c1DckTabPageMain);
            this.c1DckTabMain.KeepClosedPages = false;
            this.c1DckTabMain.Location = new System.Drawing.Point(0, 0);
            this.c1DckTabMain.Name = "c1DckTabMain";
            this.c1DckTabMain.ShowCaption = true;
            this.c1DckTabMain.ShowSingleTab = false;
            this.c1DckTabMain.Size = new System.Drawing.Size(202, 722);
            this.c1DckTabMain.TabIndex = 1;
            this.c1DckTabMain.TabSizeMode = C1.Win.C1Command.TabSizeModeEnum.Fit;
            this.c1DckTabMain.TabStyle = C1.Win.C1Command.TabStyleEnum.Office2010;
            this.c1DckTabMain.VisualStyle = C1.Win.C1Command.VisualStyle.Office2010Silver;
            this.c1DckTabMain.VisualStyleBase = C1.Win.C1Command.VisualStyle.Office2010Silver;
            // 
            // c1DckTabPageMain
            // 
            this.c1DckTabPageMain.BackColor = System.Drawing.SystemColors.Control;
            this.c1DckTabPageMain.Controls.Add(this.c1SizerFind);
            this.c1DckTabPageMain.Image = ((System.Drawing.Image)(resources.GetObject("c1DckTabPageMain.Image")));
            this.c1DckTabPageMain.Location = new System.Drawing.Point(0, 0);
            this.c1DckTabPageMain.Name = "c1DckTabPageMain";
            this.c1DckTabPageMain.Size = new System.Drawing.Size(199, 721);
            this.c1DckTabPageMain.TabIndex = 0;
            this.c1DckTabPageMain.Text = "Поиск";
            this.c1DckTabPageMain.ClientSizeChanged += new System.EventHandler(this.c1DckTabPageMain_ClientSizeChanged);
            // 
            // c1SizerFind
            // 
            this.c1SizerFind.Controls.Add(this.panelFind);
            this.c1SizerFind.Controls.Add(this.c1FlxGrdFind);
            this.c1SizerFind.GridDefinition = "12.1069182389937:False:True;86.4779874213836:False:False;\t99.4818652849741:False:" +
                "False;";
            this.c1SizerFind.Location = new System.Drawing.Point(3, 33);
            this.c1SizerFind.Name = "c1SizerFind";
            this.c1SizerFind.Padding = new System.Windows.Forms.Padding(1, 4, 0, 1);
            this.c1SizerFind.Size = new System.Drawing.Size(193, 636);
            this.c1SizerFind.TabIndex = 0;
            // 
            // panelFind
            // 
            this.panelFind.Controls.Add(this.chkBoxTrace);
            this.panelFind.Controls.Add(this.label2);
            this.panelFind.Controls.Add(this.label1);
            this.panelFind.Controls.Add(this.c1CmbBort);
            this.panelFind.Controls.Add(this.c1CmbPch);
            this.panelFind.Controls.Add(this.c1BtnTest);
            this.panelFind.Location = new System.Drawing.Point(1, 4);
            this.panelFind.Name = "panelFind";
            this.panelFind.Size = new System.Drawing.Size(192, 77);
            this.panelFind.TabIndex = 1;
            // 
            // chkBoxTrace
            // 
            this.chkBoxTrace.AutoSize = true;
            this.chkBoxTrace.Location = new System.Drawing.Point(114, 37);
            this.chkBoxTrace.Name = "chkBoxTrace";
            this.chkBoxTrace.Size = new System.Drawing.Size(68, 17);
            this.chkBoxTrace.TabIndex = 5;
            this.chkBoxTrace.Text = "Следить";
            this.chkBoxTrace.UseVisualStyleBackColor = true;
            this.chkBoxTrace.Visible = false;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(77, 38);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(31, 13);
            this.label2.TabIndex = 4;
            this.label2.Text = "Борт";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(130, 7);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(48, 13);
            this.label1.TabIndex = 3;
            this.label1.Text = "П/часть";
            // 
            // c1CmbBort
            // 
            this.c1CmbBort.AddItemSeparator = ';';
            this.c1CmbBort.AutoCompletion = true;
            this.c1CmbBort.AutoDropDown = true;
            this.c1CmbBort.Caption = "";
            this.c1CmbBort.CaptionHeight = 17;
            this.c1CmbBort.CharacterCasing = System.Windows.Forms.CharacterCasing.Normal;
            this.c1CmbBort.ColumnCaptionHeight = 17;
            this.c1CmbBort.ColumnFooterHeight = 17;
            this.c1CmbBort.ContentHeight = 15;
            this.c1CmbBort.DeadAreaBackColor = System.Drawing.Color.Empty;
            this.c1CmbBort.EditorBackColor = System.Drawing.SystemColors.Window;
            this.c1CmbBort.EditorFont = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.c1CmbBort.EditorForeColor = System.Drawing.SystemColors.WindowText;
            this.c1CmbBort.EditorHeight = 15;
            this.c1CmbBort.ExtendRightColumn = true;
            this.c1CmbBort.Images.Add(((System.Drawing.Image)(resources.GetObject("c1CmbBort.Images"))));
            this.c1CmbBort.ItemHeight = 15;
            this.c1CmbBort.Location = new System.Drawing.Point(0, 34);
            this.c1CmbBort.MatchEntryTimeout = ((long)(2000));
            this.c1CmbBort.MaxDropDownItems = ((short)(5));
            this.c1CmbBort.MaxLength = 32767;
            this.c1CmbBort.MouseCursor = System.Windows.Forms.Cursors.Default;
            this.c1CmbBort.Name = "c1CmbBort";
            this.c1CmbBort.RowDivider.Style = C1.Win.C1List.LineStyleEnum.None;
            this.c1CmbBort.RowSubDividerColor = System.Drawing.Color.DarkGray;
            this.c1CmbBort.Size = new System.Drawing.Size(70, 21);
            this.c1CmbBort.TabIndex = 1;
            this.c1CmbBort.VisualStyle = C1.Win.C1List.VisualStyle.Office2007Silver;
            this.c1CmbBort.Close += new C1.Win.C1List.CloseEventHandler(this.c1CmbBort_Close);
            this.c1CmbBort.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.c1CmbBort_KeyPress);
            this.c1CmbBort.PropBag = resources.GetString("c1CmbBort.PropBag");
            // 
            // c1CmbPch
            // 
            this.c1CmbPch.AddItemSeparator = ';';
            this.c1CmbPch.Caption = "";
            this.c1CmbPch.CaptionHeight = 17;
            this.c1CmbPch.CharacterCasing = System.Windows.Forms.CharacterCasing.Normal;
            this.c1CmbPch.ColumnCaptionHeight = 17;
            this.c1CmbPch.ColumnFooterHeight = 17;
            this.c1CmbPch.ContentHeight = 15;
            this.c1CmbPch.DeadAreaBackColor = System.Drawing.Color.Empty;
            this.c1CmbPch.EditorBackColor = System.Drawing.SystemColors.Window;
            this.c1CmbPch.EditorFont = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.c1CmbPch.EditorForeColor = System.Drawing.SystemColors.WindowText;
            this.c1CmbPch.EditorHeight = 15;
            this.c1CmbPch.Enabled = false;
            this.c1CmbPch.Images.Add(((System.Drawing.Image)(resources.GetObject("c1CmbPch.Images"))));
            this.c1CmbPch.ItemHeight = 15;
            this.c1CmbPch.Location = new System.Drawing.Point(0, 3);
            this.c1CmbPch.MatchEntryTimeout = ((long)(2000));
            this.c1CmbPch.MaxDropDownItems = ((short)(5));
            this.c1CmbPch.MaxLength = 32767;
            this.c1CmbPch.MouseCursor = System.Windows.Forms.Cursors.Default;
            this.c1CmbPch.Name = "c1CmbPch";
            this.c1CmbPch.RowDivider.Style = C1.Win.C1List.LineStyleEnum.None;
            this.c1CmbPch.RowSubDividerColor = System.Drawing.Color.DarkGray;
            this.c1CmbPch.Size = new System.Drawing.Size(127, 21);
            this.c1CmbPch.TabIndex = 2;
            this.c1CmbPch.VisualStyle = C1.Win.C1List.VisualStyle.Office2007Silver;
            this.c1CmbPch.PropBag = resources.GetString("c1CmbPch.PropBag");
            // 
            // c1BtnTest
            // 
            this.c1BtnTest.Location = new System.Drawing.Point(133, 56);
            this.c1BtnTest.Name = "c1BtnTest";
            this.c1BtnTest.Size = new System.Drawing.Size(45, 18);
            this.c1BtnTest.TabIndex = 0;
            this.c1BtnTest.Text = "Test";
            this.c1BtnTest.UseVisualStyleBackColor = true;
            this.c1BtnTest.Visible = false;
            this.c1BtnTest.VisualStyle = C1.Win.C1Input.VisualStyle.Office2010Silver;
            this.c1BtnTest.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.Office2010Silver;
            this.c1BtnTest.Click += new System.EventHandler(this.c1BtnTest_Click);
            // 
            // c1FlxGrdFind
            // 
            this.c1FlxGrdFind.ColumnInfo = resources.GetString("c1FlxGrdFind.ColumnInfo");
            this.c1FlxGrdFind.ExtendLastCol = true;
            this.c1FlxGrdFind.FocusRect = C1.Win.C1FlexGrid.FocusRectEnum.Solid;
            this.c1FlxGrdFind.Location = new System.Drawing.Point(1, 85);
            this.c1FlxGrdFind.Name = "c1FlxGrdFind";
            this.c1FlxGrdFind.Rows.Count = 1;
            this.c1FlxGrdFind.Rows.DefaultSize = 19;
            this.c1FlxGrdFind.ShowButtons = C1.Win.C1FlexGrid.ShowButtonsEnum.Always;
            this.c1FlxGrdFind.ShowCellLabels = true;
            this.c1FlxGrdFind.Size = new System.Drawing.Size(192, 550);
            this.c1FlxGrdFind.TabIndex = 0;
            this.c1FlxGrdFind.CellButtonClick += new C1.Win.C1FlexGrid.RowColEventHandler(this.c1FlxGrdFind_CellButtonClick);
            this.c1FlxGrdFind.CellChecked += new C1.Win.C1FlexGrid.RowColEventHandler(this.c1FlxGrdFind_CellChecked);
            this.c1FlxGrdFind.MouseClick += new System.Windows.Forms.MouseEventHandler(this.c1FlxGrdFind_MouseClick);
            this.c1FlxGrdFind.MouseDown += new System.Windows.Forms.MouseEventHandler(this.c1FlxGrdFind_MouseDown);
            // 
            // c1SizerMap
            // 
            this.c1SizerMap.Controls.Add(this.c1StatusBarMain);
            this.c1SizerMap.Controls.Add(this.axMapUkg);
            this.c1SizerMap.Dock = System.Windows.Forms.DockStyle.Fill;
            this.c1SizerMap.GridDefinition = "98.8919667590028:False:False;\t99.0074441687345:False:False;";
            this.c1SizerMap.Location = new System.Drawing.Point(202, 0);
            this.c1SizerMap.Name = "c1SizerMap";
            this.c1SizerMap.Size = new System.Drawing.Size(806, 722);
            this.c1SizerMap.TabIndex = 1;
            this.c1SizerMap.Text = "c1Sizer1";
            // 
            // c1StatusBarMain
            // 
            this.c1StatusBarMain.LeftPaneItems.Add(this.rbnBtnConnSql);
            this.c1StatusBarMain.LeftPaneItems.Add(this.ribnLblConSql);
            this.c1StatusBarMain.LeftPaneItems.Add(this.rbnBtnConnTcp);
            this.c1StatusBarMain.LeftPaneItems.Add(this.ribnLblConTcp);
            this.c1StatusBarMain.Name = "c1StatusBarMain";
            this.c1StatusBarMain.VisualStyle = C1.Win.C1Ribbon.VisualStyle.Office2010Silver;
            // 
            // rbnBtnConnSql
            // 
            this.rbnBtnConnSql.Name = "rbnBtnConnSql";
            this.rbnBtnConnSql.SmallImage = ((System.Drawing.Image)(resources.GetObject("rbnBtnConnSql.SmallImage")));
            this.rbnBtnConnSql.Click += new System.EventHandler(this.rbnBtnConnSql_Click);
            // 
            // ribnLblConSql
            // 
            this.ribnLblConSql.Name = "ribnLblConSql";
            this.ribnLblConSql.Text = "SQL сервер";
            // 
            // rbnBtnConnTcp
            // 
            this.rbnBtnConnTcp.Name = "rbnBtnConnTcp";
            this.rbnBtnConnTcp.SmallImage = ((System.Drawing.Image)(resources.GetObject("rbnBtnConnTcp.SmallImage")));
            this.rbnBtnConnTcp.Click += new System.EventHandler(this.rbnBtnConnTcp_Click);
            // 
            // ribnLblConTcp
            // 
            this.ribnLblConTcp.Name = "ribnLblConTcp";
            this.ribnLblConTcp.Text = "ТСР сервер";
            // 
            // axMapUkg
            // 
            this.axMapUkg.Enabled = true;
            this.axMapUkg.Location = new System.Drawing.Point(205, 209);
            this.axMapUkg.Name = "axMapUkg";
            this.axMapUkg.OcxState = ((System.Windows.Forms.AxHost.State)(resources.GetObject("axMapUkg.OcxState")));
            this.axMapUkg.Size = new System.Drawing.Size(337, 281);
            this.axMapUkg.TabIndex = 0;
            this.axMapUkg.MouseMoveEvent += new AxMapXLib.CMapXEvents_MouseMoveEventHandler(this.axMapUkg_MouseMoveEvent);
            // 
            // c1CntMnuGrdBort
            // 
            this.c1CntMnuGrdBort.CommandLinks.AddRange(new C1.Win.C1Command.C1CommandLink[] {
            this.c1CommandLink2});
            this.c1CntMnuGrdBort.Name = "c1CntMnuGrdBort";
            this.c1CntMnuGrdBort.VisualStyle = C1.Win.C1Command.VisualStyle.Office2010Silver;
            this.c1CntMnuGrdBort.VisualStyleBase = C1.Win.C1Command.VisualStyle.Office2010Silver;
            // 
            // c1CommandLink2
            // 
            this.c1CommandLink2.Command = this.c1CmndBortTrace;
            // 
            // c1CmndBortTrace
            // 
            this.c1CmndBortTrace.Icon = ((System.Drawing.Icon)(resources.GetObject("c1CmndBortTrace.Icon")));
            this.c1CmndBortTrace.Name = "c1CmndBortTrace";
            this.c1CmndBortTrace.Text = "Следить за объектом";
            this.c1CmndBortTrace.Click += new C1.Win.C1Command.ClickEventHandler(this.c1CmndBortTrace_Click);
            // 
            // c1CommandHolder1
            // 
            this.c1CommandHolder1.Commands.Add(this.c1CntMnuGrdBort);
            this.c1CommandHolder1.Commands.Add(this.c1CmndBortTrace);
            this.c1CommandHolder1.Owner = this;
            // 
            // FrmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1008, 722);
            this.Controls.Add(this.c1SizerMap);
            this.Controls.Add(this.c1CmndDockMain);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "FrmMain";
            this.Text = "Мониторинг автотранспорта";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.FrmMain_Load);
            ((System.ComponentModel.ISupportInitialize)(this.c1CmndDockMain)).EndInit();
            this.c1CmndDockMain.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.c1DckTabMain)).EndInit();
            this.c1DckTabMain.ResumeLayout(false);
            this.c1DckTabPageMain.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.c1SizerFind)).EndInit();
            this.c1SizerFind.ResumeLayout(false);
            this.panelFind.ResumeLayout(false);
            this.panelFind.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.c1CmbBort)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.c1CmbPch)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.c1FlxGrdFind)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.c1SizerMap)).EndInit();
            this.c1SizerMap.ResumeLayout(false);
            this.c1SizerMap.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.c1StatusBarMain)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.axMapUkg)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.c1CommandHolder1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private C1.Win.C1Command.C1CommandDock c1CmndDockMain;
        private C1.Win.C1Command.C1DockingTab c1DckTabMain;
        private C1.Win.C1Command.C1DockingTabPage c1DckTabPageMain;
        private C1.Win.C1Sizer.C1Sizer c1SizerMap;
        private AxMapXLib.AxMap axMapUkg;
        private C1.Win.C1Ribbon.C1StatusBar c1StatusBarMain;
        private C1.Win.C1Ribbon.RibbonButton rbnBtnConnSql;
        private C1.Win.C1Ribbon.RibbonLabel ribnLblConSql;
        private C1.Win.C1Ribbon.RibbonButton rbnBtnConnTcp;
        private C1.Win.C1Ribbon.RibbonLabel ribnLblConTcp;
        private C1.Win.C1Sizer.C1Sizer c1SizerFind;
        private System.Windows.Forms.Panel panelFind;
        private C1.Win.C1Input.C1Button c1BtnTest;
        private C1.Win.C1List.C1Combo c1CmbBort;
        private C1.Win.C1List.C1Combo c1CmbPch;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.CheckBox chkBoxTrace;
        private C1.Win.C1Command.C1ContextMenu c1CntMnuGrdBort;
        private C1.Win.C1Command.C1CommandLink c1CommandLink2;
        private C1.Win.C1Command.C1Command c1CmndBortTrace;
        private C1.Win.C1Command.C1CommandHolder c1CommandHolder1;
        public C1.Win.C1FlexGrid.C1FlexGrid c1FlxGrdFind;
    }
}

