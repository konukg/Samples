namespace DispNet
{
    partial class FrmObjUkg
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmObjUkg));
            this.c1FlxGrdObjS = new C1.Win.C1FlexGrid.C1FlexGrid();
            this.btnRec = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.c1FlxGrdObjS)).BeginInit();
            this.SuspendLayout();
            // 
            // c1FlxGrdObjS
            // 
            this.c1FlxGrdObjS.AllowEditing = false;
            this.c1FlxGrdObjS.ColumnInfo = resources.GetString("c1FlxGrdObjS.ColumnInfo");
            this.c1FlxGrdObjS.ExtendLastCol = true;
            this.c1FlxGrdObjS.Location = new System.Drawing.Point(12, 12);
            this.c1FlxGrdObjS.Name = "c1FlxGrdObjS";
            this.c1FlxGrdObjS.Rows.DefaultSize = 17;
            this.c1FlxGrdObjS.ShowCellLabels = true;
            this.c1FlxGrdObjS.Size = new System.Drawing.Size(454, 209);
            this.c1FlxGrdObjS.TabIndex = 0;
            this.c1FlxGrdObjS.DoubleClick += new System.EventHandler(this.c1FlxGrdObjS_DoubleClick);
            this.c1FlxGrdObjS.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.c1FlxGrdObjS_KeyPress);
            // 
            // btnRec
            // 
            this.btnRec.Location = new System.Drawing.Point(100, 234);
            this.btnRec.Name = "btnRec";
            this.btnRec.Size = new System.Drawing.Size(70, 23);
            this.btnRec.TabIndex = 1;
            this.btnRec.Text = "Выбор";
            this.btnRec.UseVisualStyleBackColor = true;
            this.btnRec.Click += new System.EventHandler(this.btnRec_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(316, 235);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(70, 22);
            this.btnCancel.TabIndex = 2;
            this.btnCancel.Text = "Отмена";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // FrmObjUkg
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(476, 270);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnRec);
            this.Controls.Add(this.c1FlxGrdObjS);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FrmObjUkg";
            this.Load += new System.EventHandler(this.FrmObjUkg_Load);
            ((System.ComponentModel.ISupportInitialize)(this.c1FlxGrdObjS)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private C1.Win.C1FlexGrid.C1FlexGrid c1FlxGrdObjS;
        private System.Windows.Forms.Button btnRec;
        private System.Windows.Forms.Button btnCancel;
    }
}