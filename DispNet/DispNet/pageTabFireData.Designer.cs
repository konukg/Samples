namespace DispNet
{
    partial class pageTabFireData
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.c1FlxGrdtDead = new C1.Win.C1FlexGrid.C1FlexGrid();
            this.c1FlxGrdVictim = new C1.Win.C1FlexGrid.C1FlexGrid();
            this.c1FlxGrdGuilty = new C1.Win.C1FlexGrid.C1FlexGrid();
            this.txtBoxDeadPeople = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.txtBoxVictimPeople = new System.Windows.Forms.TextBox();
            this.txtBoxGuiltyPeople = new System.Windows.Forms.TextBox();
            this.c1TxtBoxEscapePeople = new C1.Win.C1Input.C1TextBox();
            this.c1TxtBoxEvacuatePeople = new C1.Win.C1Input.C1TextBox();
            this.btnRecData = new System.Windows.Forms.Button();
            this.txtBoxMasterFire = new System.Windows.Forms.TextBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.label5 = new System.Windows.Forms.Label();
            this.c1TxtBoxKrs = new C1.Win.C1Input.C1TextBox();
            this.c1TxtBoxMrs = new C1.Win.C1Input.C1TextBox();
            this.c1TxtBoxPr = new C1.Win.C1Input.C1TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.c1FlxGrdtDead)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.c1FlxGrdVictim)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.c1FlxGrdGuilty)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.c1TxtBoxEscapePeople)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.c1TxtBoxEvacuatePeople)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.c1TxtBoxKrs)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.c1TxtBoxMrs)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.c1TxtBoxPr)).BeginInit();
            this.SuspendLayout();
            // 
            // c1FlxGrdtDead
            // 
            this.c1FlxGrdtDead.ColumnInfo = "3,1,0,0,0,85,Columns:0{Width:20;}\t1{Width:56;Visible:False;}\t2{Caption:\"Погибшие\"" +
                ";}\t";
            this.c1FlxGrdtDead.ExtendLastCol = true;
            this.c1FlxGrdtDead.Location = new System.Drawing.Point(14, 13);
            this.c1FlxGrdtDead.Name = "c1FlxGrdtDead";
            this.c1FlxGrdtDead.Rows.Count = 1;
            this.c1FlxGrdtDead.Rows.DefaultSize = 17;
            this.c1FlxGrdtDead.ShowCellLabels = true;
            this.c1FlxGrdtDead.Size = new System.Drawing.Size(174, 89);
            this.c1FlxGrdtDead.TabIndex = 0;
            this.c1FlxGrdtDead.AfterEdit += new C1.Win.C1FlexGrid.RowColEventHandler(this.c1FlxGrdtDead_AfterEdit);
            this.c1FlxGrdtDead.KeyDown += new System.Windows.Forms.KeyEventHandler(this.c1FlxGrdtDead_KeyDown);
            // 
            // c1FlxGrdVictim
            // 
            this.c1FlxGrdVictim.ColumnInfo = "3,1,0,0,0,85,Columns:0{Width:20;}\t1{Visible:False;}\t2{Caption:\"Пострадавшие\";}\t";
            this.c1FlxGrdVictim.ExtendLastCol = true;
            this.c1FlxGrdVictim.Location = new System.Drawing.Point(229, 13);
            this.c1FlxGrdVictim.Name = "c1FlxGrdVictim";
            this.c1FlxGrdVictim.Rows.Count = 1;
            this.c1FlxGrdVictim.Rows.DefaultSize = 17;
            this.c1FlxGrdVictim.Size = new System.Drawing.Size(174, 89);
            this.c1FlxGrdVictim.TabIndex = 1;
            this.c1FlxGrdVictim.AfterEdit += new C1.Win.C1FlexGrid.RowColEventHandler(this.c1FlxGrdVictim_AfterEdit);
            this.c1FlxGrdVictim.KeyDown += new System.Windows.Forms.KeyEventHandler(this.c1FlxGrdVictim_KeyDown);
            // 
            // c1FlxGrdGuilty
            // 
            this.c1FlxGrdGuilty.ColumnInfo = "3,1,0,0,0,85,Columns:0{Width:20;}\t1{Visible:False;}\t2{Caption:\"Виновные\";}\t";
            this.c1FlxGrdGuilty.ExtendLastCol = true;
            this.c1FlxGrdGuilty.Location = new System.Drawing.Point(440, 13);
            this.c1FlxGrdGuilty.Name = "c1FlxGrdGuilty";
            this.c1FlxGrdGuilty.Rows.Count = 1;
            this.c1FlxGrdGuilty.Rows.DefaultSize = 17;
            this.c1FlxGrdGuilty.Size = new System.Drawing.Size(174, 89);
            this.c1FlxGrdGuilty.TabIndex = 2;
            this.c1FlxGrdGuilty.AfterEdit += new C1.Win.C1FlexGrid.RowColEventHandler(this.c1FlxGrdGuilty_AfterEdit);
            this.c1FlxGrdGuilty.KeyDown += new System.Windows.Forms.KeyEventHandler(this.c1FlxGrdGuilty_KeyDown);
            // 
            // txtBoxDeadPeople
            // 
            this.txtBoxDeadPeople.Location = new System.Drawing.Point(14, 101);
            this.txtBoxDeadPeople.Name = "txtBoxDeadPeople";
            this.txtBoxDeadPeople.Size = new System.Drawing.Size(156, 20);
            this.txtBoxDeadPeople.TabIndex = 5;
            this.txtBoxDeadPeople.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtBoxDeadPeople_KeyPress);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(491, 178);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(64, 13);
            this.label1.TabIndex = 7;
            this.label1.Text = "Спасенные";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(491, 148);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(93, 13);
            this.label2.TabIndex = 9;
            this.label2.Text = "Эвакуированные";
            // 
            // txtBoxVictimPeople
            // 
            this.txtBoxVictimPeople.Location = new System.Drawing.Point(229, 101);
            this.txtBoxVictimPeople.Name = "txtBoxVictimPeople";
            this.txtBoxVictimPeople.Size = new System.Drawing.Size(154, 20);
            this.txtBoxVictimPeople.TabIndex = 10;
            this.txtBoxVictimPeople.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtBoxVictimPeople_KeyPress);
            // 
            // txtBoxGuiltyPeople
            // 
            this.txtBoxGuiltyPeople.Location = new System.Drawing.Point(440, 101);
            this.txtBoxGuiltyPeople.Name = "txtBoxGuiltyPeople";
            this.txtBoxGuiltyPeople.Size = new System.Drawing.Size(157, 20);
            this.txtBoxGuiltyPeople.TabIndex = 11;
            this.txtBoxGuiltyPeople.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtBoxGuiltyPeople_KeyPress);
            // 
            // c1TxtBoxEscapePeople
            // 
            this.c1TxtBoxEscapePeople.DataType = typeof(short);
            this.c1TxtBoxEscapePeople.DateTimeInput = false;
            this.c1TxtBoxEscapePeople.EmptyAsNull = true;
            this.c1TxtBoxEscapePeople.Location = new System.Drawing.Point(440, 175);
            this.c1TxtBoxEscapePeople.Name = "c1TxtBoxEscapePeople";
            this.c1TxtBoxEscapePeople.NullText = "0";
            this.c1TxtBoxEscapePeople.Size = new System.Drawing.Size(45, 20);
            this.c1TxtBoxEscapePeople.TabIndex = 14;
            this.c1TxtBoxEscapePeople.Tag = null;
            this.c1TxtBoxEscapePeople.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.c1TxtBoxEscapePeople_KeyPress);
            // 
            // c1TxtBoxEvacuatePeople
            // 
            this.c1TxtBoxEvacuatePeople.DataType = typeof(short);
            this.c1TxtBoxEvacuatePeople.DateTimeInput = false;
            this.c1TxtBoxEvacuatePeople.EmptyAsNull = true;
            this.c1TxtBoxEvacuatePeople.Location = new System.Drawing.Point(440, 145);
            this.c1TxtBoxEvacuatePeople.Name = "c1TxtBoxEvacuatePeople";
            this.c1TxtBoxEvacuatePeople.NullText = "0";
            this.c1TxtBoxEvacuatePeople.Size = new System.Drawing.Size(45, 20);
            this.c1TxtBoxEvacuatePeople.TabIndex = 15;
            this.c1TxtBoxEvacuatePeople.Tag = null;
            this.c1TxtBoxEvacuatePeople.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.c1TxtBoxEvacuatePeople_KeyPress);
            // 
            // btnRecData
            // 
            this.btnRecData.Location = new System.Drawing.Point(53, 62);
            this.btnRecData.Name = "btnRecData";
            this.btnRecData.Size = new System.Drawing.Size(52, 20);
            this.btnRecData.TabIndex = 16;
            this.btnRecData.Text = "Запись";
            this.btnRecData.UseVisualStyleBackColor = true;
            this.btnRecData.Click += new System.EventHandler(this.btnRecData_Click);
            // 
            // txtBoxMasterFire
            // 
            this.txtBoxMasterFire.Location = new System.Drawing.Point(6, 19);
            this.txtBoxMasterFire.Multiline = true;
            this.txtBoxMasterFire.Name = "txtBoxMasterFire";
            this.txtBoxMasterFire.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtBoxMasterFire.Size = new System.Drawing.Size(156, 34);
            this.txtBoxMasterFire.TabIndex = 22;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.txtBoxMasterFire);
            this.groupBox1.Controls.Add(this.btnRecData);
            this.groupBox1.Location = new System.Drawing.Point(14, 127);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(174, 90);
            this.groupBox1.TabIndex = 24;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Хозяева";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.label4);
            this.groupBox2.Controls.Add(this.label5);
            this.groupBox2.Controls.Add(this.c1TxtBoxMrs);
            this.groupBox2.Controls.Add(this.c1TxtBoxPr);
            this.groupBox2.Controls.Add(this.label3);
            this.groupBox2.Controls.Add(this.c1TxtBoxKrs);
            this.groupBox2.Location = new System.Drawing.Point(229, 127);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(174, 90);
            this.groupBox2.TabIndex = 25;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Животные";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(142, 22);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(24, 13);
            this.label5.TabIndex = 37;
            this.label5.Text = "Пр.";
            // 
            // c1TxtBoxKrs
            // 
            this.c1TxtBoxKrs.DataType = typeof(short);
            this.c1TxtBoxKrs.DateTimeInput = false;
            this.c1TxtBoxKrs.EmptyAsNull = true;
            this.c1TxtBoxKrs.Location = new System.Drawing.Point(6, 19);
            this.c1TxtBoxKrs.Name = "c1TxtBoxKrs";
            this.c1TxtBoxKrs.NullText = "0";
            this.c1TxtBoxKrs.Size = new System.Drawing.Size(45, 20);
            this.c1TxtBoxKrs.TabIndex = 26;
            this.c1TxtBoxKrs.Tag = null;
            this.c1TxtBoxKrs.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.c1TxtBoxKrs_KeyPress);
            // 
            // c1TxtBoxMrs
            // 
            this.c1TxtBoxMrs.DataType = typeof(short);
            this.c1TxtBoxMrs.DateTimeInput = false;
            this.c1TxtBoxMrs.EmptyAsNull = true;
            this.c1TxtBoxMrs.Location = new System.Drawing.Point(6, 48);
            this.c1TxtBoxMrs.Name = "c1TxtBoxMrs";
            this.c1TxtBoxMrs.NullText = "0";
            this.c1TxtBoxMrs.Size = new System.Drawing.Size(45, 20);
            this.c1TxtBoxMrs.TabIndex = 33;
            this.c1TxtBoxMrs.Tag = null;
            this.c1TxtBoxMrs.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.c1TxtBoxMrs_KeyPress);
            // 
            // c1TxtBoxPr
            // 
            this.c1TxtBoxPr.DataType = typeof(short);
            this.c1TxtBoxPr.DateTimeInput = false;
            this.c1TxtBoxPr.EmptyAsNull = true;
            this.c1TxtBoxPr.Location = new System.Drawing.Point(91, 19);
            this.c1TxtBoxPr.Name = "c1TxtBoxPr";
            this.c1TxtBoxPr.NullText = "0";
            this.c1TxtBoxPr.Size = new System.Drawing.Size(45, 20);
            this.c1TxtBoxPr.TabIndex = 34;
            this.c1TxtBoxPr.Tag = null;
            this.c1TxtBoxPr.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.c1TxtBoxPr_KeyPress);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(57, 22);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(28, 13);
            this.label3.TabIndex = 35;
            this.label3.Text = "КРС";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(57, 51);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(30, 13);
            this.label4.TabIndex = 36;
            this.label4.Text = "МРС";
            // 
            // pageTabFireData
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.c1FlxGrdGuilty);
            this.Controls.Add(this.txtBoxGuiltyPeople);
            this.Controls.Add(this.c1FlxGrdVictim);
            this.Controls.Add(this.txtBoxVictimPeople);
            this.Controls.Add(this.c1FlxGrdtDead);
            this.Controls.Add(this.txtBoxDeadPeople);
            this.Controls.Add(this.c1TxtBoxEvacuatePeople);
            this.Controls.Add(this.c1TxtBoxEscapePeople);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Name = "pageTabFireData";
            this.Size = new System.Drawing.Size(633, 230);
            this.Load += new System.EventHandler(this.pageTabFireData_Load);
            ((System.ComponentModel.ISupportInitialize)(this.c1FlxGrdtDead)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.c1FlxGrdVictim)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.c1FlxGrdGuilty)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.c1TxtBoxEscapePeople)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.c1TxtBoxEvacuatePeople)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.c1TxtBoxKrs)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.c1TxtBoxMrs)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.c1TxtBoxPr)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private C1.Win.C1FlexGrid.C1FlexGrid c1FlxGrdtDead;
        private C1.Win.C1FlexGrid.C1FlexGrid c1FlxGrdVictim;
        private C1.Win.C1FlexGrid.C1FlexGrid c1FlxGrdGuilty;
        private System.Windows.Forms.TextBox txtBoxDeadPeople;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtBoxVictimPeople;
        private System.Windows.Forms.TextBox txtBoxGuiltyPeople;
        private C1.Win.C1Input.C1TextBox c1TxtBoxEscapePeople;
        private C1.Win.C1Input.C1TextBox c1TxtBoxEvacuatePeople;
        private System.Windows.Forms.Button btnRecData;
        private System.Windows.Forms.TextBox txtBoxMasterFire;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private C1.Win.C1Input.C1TextBox c1TxtBoxKrs;
        private C1.Win.C1Input.C1TextBox c1TxtBoxMrs;
        private C1.Win.C1Input.C1TextBox c1TxtBoxPr;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
    }
}
