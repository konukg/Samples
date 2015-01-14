using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;
using C1.Win.C1FlexGrid;

namespace DispNet
{
    public partial class FrmSdjvName : Form
    {
        static public FrmSdjvName Instance;

        DataSet dsSdjvName = new DataSet();
        BindingSource bsSdjvName = new BindingSource();

        DataSet dsSdjvNote = new DataSet();
        BindingSource bsSdjvNote = new BindingSource();

        public FrmSdjvName()
        {
            InitializeComponent();
        }

        private void FrmSdjvName_Load(object sender, EventArgs e)
        {
            DataSet dsSdjvNameLoc = new DataSet();
            BindingSource bsSdjvNameLoc = new BindingSource();

            string strSqlTextSdjvName = "SELECT SdjvName.* FROM SdjvName ORDER BY SdjvNameMsg";
            SqlDataAdapter daSdjvName = new SqlDataAdapter(strSqlTextSdjvName, FrmMain.Instance.m_cnn);
            daSdjvName.Fill(dsSdjvNameLoc, "SdjvName");
            bsSdjvNameLoc.DataSource = dsSdjvNameLoc;
            bsSdjvNameLoc.DataMember = "SdjvName";

            //делаем копии иначе не связать таблицу с навигатором
            dsSdjvName = dsSdjvNameLoc.Copy();
            bsSdjvName = new BindingSource(dsSdjvNameLoc, "SdjvName");
            //....
            
            c1DbNvgSdjv.DataSource = bsSdjvName;
            c1FlxGrdSdjv.DataSource = c1DbNvgSdjv.DataSource;

            c1FlxGrdSdjv.Cols[1].Visible = false;
            c1FlxGrdSdjv.Cols[5].Visible = false;
            c1FlxGrdSdjv.Cols[6].Visible = false;
            c1FlxGrdSdjv.Cols[7].Visible = false;
            c1FlxGrdSdjv.Cols[8].Visible = false;
            c1FlxGrdSdjv.Cols[9].Visible = false;
            c1FlxGrdSdjv.Cols[10].Visible = false;
            c1FlxGrdSdjv.Cols[11].Visible = false;
            c1FlxGrdSdjv.Cols[12].Visible = false;
            c1FlxGrdSdjv.Cols[13].Visible = false;
            c1FlxGrdSdjv.Cols[14].Visible = false;
            c1FlxGrdSdjv.Cols[15].Visible = false;

            c1FlxGrdSdjv.Cols[2].Width = 200;
            c1FlxGrdSdjv.Cols[2].Caption = "Наименование";
            c1FlxGrdSdjv.Cols[2].AllowEditing = false;

            c1FlxGrdSdjv.Cols[3].Width = 100;
            c1FlxGrdSdjv.Cols[3].Caption = "Вещество";
            c1FlxGrdSdjv.Cols[3].AllowEditing = false;

            c1FlxGrdSdjv.Cols[4].Width = 100;
            c1FlxGrdSdjv.Cols[4].Caption = "Токсичность";
            c1FlxGrdSdjv.Cols[4].AllowEditing = false;

            CellStyle csDiv = c1FlxGrdSdjv.Styles.Add("s1");
            csDiv.Font = new Font("Arial", 9, FontStyle.Bold);
            csDiv.TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.CenterCenter;
            c1FlxGrdSdjv.Rows[0].Style = csDiv;

            c1FlxGrdSdjv.SelectionMode = SelectionModeEnum.Row;
            c1FlxGrdSdjv.Select(1, 1);

            c1CmbFindSdjv.DataSource = bsSdjvName;
            c1CmbFindSdjv.ValueMember = "SdjvName";
            c1CmbFindSdjv.DataMember = "SdjvName";
            c1CmbFindSdjv.Columns.RemoveAt(0);
            c1CmbFindSdjv.Columns[0].Caption = "Наименование";
            c1CmbFindSdjv.Splits[0].DisplayColumns[0].HeadingStyle.HorizontalAlignment = C1.Win.C1List.AlignHorzEnum.Center;
            c1CmbFindSdjv.Splits[0].DisplayColumns[1].Visible = false;
            c1CmbFindSdjv.Splits[0].DisplayColumns[2].Visible = false;
            c1CmbFindSdjv.Splits[0].DisplayColumns[3].Visible = false;
            c1CmbFindSdjv.Splits[0].DisplayColumns[4].Visible = false;
            c1CmbFindSdjv.Splits[0].DisplayColumns[5].Visible = false;
            c1CmbFindSdjv.Splits[0].DisplayColumns[6].Visible = false;
            c1CmbFindSdjv.Splits[0].DisplayColumns[7].Visible = false;
            c1CmbFindSdjv.Splits[0].DisplayColumns[8].Visible = false;
            c1CmbFindSdjv.Splits[0].DisplayColumns[9].Visible = false;
            c1CmbFindSdjv.Splits[0].DisplayColumns[10].Visible = false;
            c1CmbFindSdjv.Splits[0].DisplayColumns[11].Visible = false;
            c1CmbFindSdjv.Splits[0].DisplayColumns[12].Visible = false;
            c1CmbFindSdjv.Splits[0].DisplayColumns[13].Visible = false;
            
            c1CmbFindSdjv.DropDownWidth = 230;

            string strSqlTextSdjvNote = "SELECT SdNoteId, SdNoteMsg FROM SdjvNote ORDER BY SdNoteId";
            SqlDataAdapter daSdjvNote = new SqlDataAdapter(strSqlTextSdjvNote, FrmMain.Instance.m_cnn);
            daSdjvNote.Fill(dsSdjvNote, "SdjvNote");
            bsSdjvNote.DataSource = dsSdjvNote;
            bsSdjvNote.DataMember = "SdjvNote";

            FindSdjvNoteMsg("SdjvName01");

            this.bsSdjvName.PositionChanged += new EventHandler(bsSdjvName_PositionChanged);
        }

        private void FindSdjvNoteMsg(string strSdNoteId)
        {
            DataRow drSdName = dsSdjvName.Tables["SdjvName"].Rows[bsSdjvName.Position];
            string strSdNameId = drSdName[strSdNoteId].ToString();

            int itemFound = bsSdjvNote.Find("SdNoteId", strSdNameId);
            bsSdjvNote.Position = itemFound;
            DataRow drSdNote = dsSdjvNote.Tables["SdjvNote"].Rows[bsSdjvNote.Position];
            c1TxtBxInfoSdv.Text = drSdNote["SdNoteMsg"].ToString();
        }

        private void SelectSdjvNoteMsg()
        {
            c1TxtBxInfoSdv.Text = "";
            if(rdBtnNm1.Checked)
                FindSdjvNoteMsg("SdjvName01");
            if (rdBtnNm2.Checked)
                FindSdjvNoteMsg("SdjvName02");
            if (rdBtnNm3.Checked)
                FindSdjvNoteMsg("SdjvName03");
            if (rdBtnNm4.Checked)
                FindSdjvNoteMsg("SdjvName04");
            if (rdBtnNm5.Checked)
                FindSdjvNoteMsg("SdjvName05");
            if (rdBtnNm6.Checked)
                FindSdjvNoteMsg("SdjvName06");
            if (rdBtnNm7.Checked)
                FindSdjvNoteMsg("SdjvName07");
            if (rdBtnNm8.Checked)
                FindSdjvNoteMsg("SdjvName08");
            if (rdBtnNm9.Checked)
                FindSdjvNoteMsg("SdjvName09");
            if (rdBtnNm10.Checked)
                FindSdjvNoteMsg("SdjvName10");
            if (rdBtnNm11.Checked)
                FindSdjvNoteMsg("SdjvName11");
        }

        private void bsSdjvName_PositionChanged(Object sender, EventArgs e)
        {
            SelectSdjvNoteMsg();
        }

        private void rdBtnNm1_CheckedChanged(object sender, EventArgs e)
        {
            if (rdBtnNm1.Checked)
                FindSdjvNoteMsg("SdjvName01");
        }

        private void rdBtnNm2_CheckedChanged(object sender, EventArgs e)
        {
            if (rdBtnNm2.Checked)
                FindSdjvNoteMsg("SdjvName02");
        }

        private void rdBtnNm3_CheckedChanged(object sender, EventArgs e)
        {
            if (rdBtnNm3.Checked)
                FindSdjvNoteMsg("SdjvName03");
        }

        private void rdBtnNm4_CheckedChanged(object sender, EventArgs e)
        {
            if (rdBtnNm4.Checked)
                FindSdjvNoteMsg("SdjvName04");
        }

        private void rdBtnNm5_CheckedChanged(object sender, EventArgs e)
        {
            if (rdBtnNm5.Checked)
                FindSdjvNoteMsg("SdjvName05");
        }

        private void rdBtnNm6_CheckedChanged(object sender, EventArgs e)
        {
            if (rdBtnNm6.Checked)
                FindSdjvNoteMsg("SdjvName06");
        }

        private void rdBtnNm7_CheckedChanged(object sender, EventArgs e)
        {
            if (rdBtnNm7.Checked)
                FindSdjvNoteMsg("SdjvName07");
        }

        private void rdBtnNm8_CheckedChanged(object sender, EventArgs e)
        {
            if (rdBtnNm8.Checked)
                FindSdjvNoteMsg("SdjvName08");
        }

        private void rdBtnNm9_CheckedChanged(object sender, EventArgs e)
        {
            if (rdBtnNm9.Checked)
                FindSdjvNoteMsg("SdjvName09");
        }

        private void rdBtnNm10_CheckedChanged(object sender, EventArgs e)
        {
            if (rdBtnNm10.Checked)
                FindSdjvNoteMsg("SdjvName10");
        }

        private void rdBtnNm11_CheckedChanged(object sender, EventArgs e)
        {
            if (rdBtnNm11.Checked)
                FindSdjvNoteMsg("SdjvName11");
        }

        private void c1CmbFindSdjv_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Return)
            {
                e.Handled = true;
            }
        }
    }
}
