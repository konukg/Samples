namespace VkoRsc
{
    partial class FrmDocExl
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
            this._book = new C1.C1Excel.C1XLBook();
            this.c1BtnLoad = new C1.Win.C1Input.C1Button();
            this._tab = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this._flex = new C1.Win.C1FlexGrid.C1FlexGrid();
            this._tab.SuspendLayout();
            this.tabPage1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this._flex)).BeginInit();
            this.SuspendLayout();
            // 
            // c1BtnLoad
            // 
            this.c1BtnLoad.Location = new System.Drawing.Point(801, 571);
            this.c1BtnLoad.Name = "c1BtnLoad";
            this.c1BtnLoad.Size = new System.Drawing.Size(59, 23);
            this.c1BtnLoad.TabIndex = 1;
            this.c1BtnLoad.Text = "c1Button1";
            this.c1BtnLoad.UseVisualStyleBackColor = true;
            this.c1BtnLoad.Click += new System.EventHandler(this.c1BtnLoad_Click);
            // 
            // _tab
            // 
            this._tab.Alignment = System.Windows.Forms.TabAlignment.Bottom;
            this._tab.Controls.Add(this.tabPage1);
            this._tab.Dock = System.Windows.Forms.DockStyle.Fill;
            this._tab.Location = new System.Drawing.Point(0, 0);
            this._tab.Multiline = true;
            this._tab.Name = "_tab";
            this._tab.SelectedIndex = 0;
            this._tab.Size = new System.Drawing.Size(891, 606);
            this._tab.TabIndex = 1;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this._flex);
            this.tabPage1.Location = new System.Drawing.Point(4, 4);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(883, 580);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "tabPage1";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // _flex
            // 
            this._flex.AllowMerging = C1.Win.C1FlexGrid.AllowMergingEnum.Spill;
            this._flex.BorderStyle = C1.Win.C1FlexGrid.Util.BaseControls.BorderStyleEnum.None;
            this._flex.ColumnInfo = "10,1,0,0,0,85,Columns:";
            this._flex.Dock = System.Windows.Forms.DockStyle.Fill;
            this._flex.Location = new System.Drawing.Point(3, 3);
            this._flex.Name = "_flex";
            this._flex.Rows.DefaultSize = 17;
            this._flex.Size = new System.Drawing.Size(877, 574);
            this._flex.TabIndex = 0;
            // 
            // FrmDocExl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(891, 606);
            this.Controls.Add(this._tab);
            this.Controls.Add(this.c1BtnLoad);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "FrmDocExl";
            this.Text = "FrmDocExl";
            this.Load += new System.EventHandler(this.FrmDocExl_Load);
            this._tab.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this._flex)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private C1.C1Excel.C1XLBook _book;
        private C1.Win.C1Input.C1Button c1BtnLoad;
        private System.Windows.Forms.TabControl _tab;
        private System.Windows.Forms.TabPage tabPage1;
        private C1.Win.C1FlexGrid.C1FlexGrid _flex;

    }
}