using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;
using C1.Win.C1FlexGrid;
using C1.Win.C1List;

namespace DispNet
{
    public partial class FrmTechUnit : Form
    {
        static public FrmTechUnit Instance;

        DataSet dsTechUnit;
        BindingSource bsTechUnit;

        DataSet dsTechAbb = new DataSet();
        BindingSource bsTechAbb = new BindingSource();

        DataSet dsUnit = new DataSet();
        BindingSource bsUnit = new BindingSource();

        BindingSource bsTechState = new BindingSource();
        DataSet dsTechState = new DataSet();

        string l_strStateBef = "";
        int l_iStateCombo;  //не дает выводить сообщение если combo был просто открыт и закрыт
        int iSngNote = 1;    //если форма вызывается из строевой то значение = 5
        public string m_strSngNotePch = "";  //номер части для вызова из строевой

        public FrmTechUnit()
        {
            InitializeComponent();
        }

        public void m_frTechNote_Load() //только при вызове из строевой
        {
            grpBoxFindBort.Visible = false;
            c1FlxGrdTechUnit.Height = 243;
            iSngNote = 5;
        }

        private void FrmTechUnit_Load(object sender, EventArgs e)
        {
            string strSqlTextTechAbb = "SELECT DISTINCT TechnicsAbbrev FROM TechnicsUnit ORDER BY TechnicsAbbrev";
            SqlDataAdapter daTechAbb = new SqlDataAdapter(strSqlTextTechAbb, FrmPermit.Instance.cnn);
            daTechAbb.Fill(dsTechAbb, "TechnicsUnit");
            bsTechAbb.DataSource = dsTechAbb;
            bsTechAbb.DataMember = "TechnicsUnit";

            string strSqlTextUnit = "SELECT DISTINCT Unit.UnitName FROM TechnicsUnit INNER JOIN Unit " +
                                        "ON TechnicsUnit.UnitId = Unit.UnitId ORDER BY Unit.UnitName";
            SqlDataAdapter daUnit = new SqlDataAdapter(strSqlTextUnit, FrmPermit.Instance.cnn);
            
            daUnit.Fill(dsUnit, "TechnicsUnit");
            bsUnit.DataSource = dsUnit;
            bsUnit.DataMember = "TechnicsUnit";

            string strSqlTextState = "SELECT StateName FROM TechnicsState ORDER BY StateName";
            SqlDataAdapter daTechState = new SqlDataAdapter(strSqlTextState, FrmPermit.Instance.cnn);
            daTechState.Fill(dsTechState, "TechnicsState");
            bsTechState.DataSource = dsTechState;
            bsTechState.DataMember = "TechnicsState";

            LoadGridTech(iSngNote, m_strSngNotePch);
        }

        private void LoadBoxFind(string strSng, BindingSource bsLoadSng)
        {
            cmbBoxFind.DataSource = bsLoadSng;
            cmbBoxFind.ValueMember = strSng;
            cmbBoxFind.DisplayMember = strSng;
            cmbBoxFind.Text = "";
        }

        private void LoadGridTech(int iSng, string strSng)
        {
            string strSqlTextTechUnit = "";
            if (iSng == 1)
            {
                strSqlTextTechUnit = "SELECT TechnicsUnit.TechnicsId, TechnicsUnit.TechnicsName, TechnicsUnit.TechnicsType, " +
                                    "TechnicsState.StateName, Unit.UnitName, TechnicsUnit.TechnicsAbbrev, " +
                                    "TechnicsUnit.TechnicsNote, TechnicsState.StateId FROM TechnicsUnit INNER JOIN TechnicsState ON " +
                                    "TechnicsUnit.StateId = TechnicsState.StateId INNER JOIN Unit ON TechnicsUnit.UnitId = Unit.UnitId " +
                                    "ORDER BY TechnicsUnit.TechnicsId";
            }
            if (iSng == 2)
            {
                strSqlTextTechUnit = "SELECT TechnicsUnit.TechnicsId, TechnicsUnit.TechnicsName, TechnicsUnit.TechnicsType, " +
                                    "TechnicsState.StateName, Unit.UnitName, TechnicsUnit.TechnicsAbbrev, " +
                                    "TechnicsUnit.TechnicsNote, TechnicsState.StateId FROM TechnicsUnit INNER JOIN TechnicsState ON " +
                                    "TechnicsUnit.StateId = TechnicsState.StateId INNER JOIN Unit ON TechnicsUnit.UnitId = Unit.UnitId " +
                                    "WHERE (TechnicsUnit.TechnicsAbbrev = '" + strSng + "') " +
                                    "ORDER BY TechnicsUnit.TechnicsId";
            }
            if (iSng == 3)
            {
                strSqlTextTechUnit = "SELECT TechnicsUnit.TechnicsId, TechnicsUnit.TechnicsName, TechnicsUnit.TechnicsType, " +
                                    "TechnicsState.StateName, Unit.UnitName, TechnicsUnit.TechnicsAbbrev, " +
                                    "TechnicsUnit.TechnicsNote, TechnicsState.StateId FROM TechnicsUnit INNER JOIN TechnicsState ON " +
                                    "TechnicsUnit.StateId = TechnicsState.StateId INNER JOIN Unit ON TechnicsUnit.UnitId = Unit.UnitId " +
                                    "WHERE (Unit.UnitName = '" + strSng + "') " +
                                    "ORDER BY TechnicsUnit.TechnicsId";
            }
            if (iSng == 4)
            {
                strSqlTextTechUnit = "SELECT TechnicsUnit.TechnicsId, TechnicsUnit.TechnicsName, TechnicsUnit.TechnicsType, " +
                                    "TechnicsState.StateName, Unit.UnitName, TechnicsUnit.TechnicsAbbrev, " +
                                    "TechnicsUnit.TechnicsNote, TechnicsState.StateId FROM TechnicsUnit INNER JOIN TechnicsState ON " +
                                    "TechnicsUnit.StateId = TechnicsState.StateId INNER JOIN Unit ON TechnicsUnit.UnitId = Unit.UnitId " +
                                    "WHERE (TechnicsState.StateName = '" + strSng + "') " +
                                    "ORDER BY TechnicsUnit.TechnicsId";
            }
            if (iSng == 5)
            {
                strSqlTextTechUnit = "SELECT TechnicsUnit.TechnicsId, TechnicsUnit.TechnicsName, TechnicsUnit.TechnicsType, " +
                                    "TechnicsState.StateName, Unit.UnitName, TechnicsUnit.TechnicsAbbrev, " +
                                    "TechnicsUnit.TechnicsNote, TechnicsState.StateId FROM TechnicsUnit INNER JOIN TechnicsState ON " +
                                    "TechnicsUnit.StateId = TechnicsState.StateId INNER JOIN Unit ON TechnicsUnit.UnitId = Unit.UnitId " +
                                    "WHERE (Unit.UnitId = '" + strSng + "') " +
                                    "ORDER BY TechnicsUnit.TechnicsId";
            }
            dsTechUnit = new DataSet();
            bsTechUnit = new BindingSource();
            SqlDataAdapter daTechUnit = new SqlDataAdapter(strSqlTextTechUnit, FrmPermit.Instance.cnn);
            daTechUnit.Fill(dsTechUnit, "TechnicsUnit");
            bsTechUnit.DataSource = dsTechUnit;
            bsTechUnit.DataMember = "TechnicsUnit";

            bool redraw = c1FlxGrdTechUnit.Redraw; // save original value
            c1FlxGrdTechUnit.Redraw = false;
            try
            {
                c1FlxGrdTechUnit.SetDataBinding(dsTechUnit, "TechnicsUnit");
                c1FlxGrdTechUnit.Cols[1].Visible = false;
                c1FlxGrdTechUnit.Cols[3].Visible = false;
                c1FlxGrdTechUnit.Cols[6].Visible = false;
                c1FlxGrdTechUnit.Cols[8].Visible = false;

                c1FlxGrdTechUnit.Cols[2].Width = 70;
                c1FlxGrdTechUnit.Cols[2].Caption = "Борт";
                c1FlxGrdTechUnit.Cols[2].AllowEditing = false;

                c1FlxGrdTechUnit.Cols[4].Width = 120;
                c1FlxGrdTechUnit.Cols[4].Caption = "Состояние";

                c1FlxGrdTechUnit.Cols[5].Width = 120;
                c1FlxGrdTechUnit.Cols[5].Caption = "П/часть";
                c1FlxGrdTechUnit.Cols[5].AllowEditing = false;

                c1FlxGrdTechUnit.Cols[7].Width = 170;
                c1FlxGrdTechUnit.Cols[7].Caption = "Примечание";
                c1FlxGrdTechUnit.Cols[7].AllowEditing = false;

                CellStyle cs = c1FlxGrdTechUnit.Styles.Add("s1");
                cs.Font = new Font("Arial", 9, FontStyle.Bold);
                cs.TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.CenterCenter;
                c1FlxGrdTechUnit.Rows[0].Style = cs;
            }
            finally
            {
                c1FlxGrdTechUnit.Redraw = redraw;
            }
            if (iSng == 1)
                LoadBoxFind("TechnicsName", bsTechUnit);            
            LoadStateTech();
        }

        private void LoadStateTech()
        {
            string strSqlTextTechState = "SELECT StateId, StateName FROM TechnicsState ORDER BY StateName";
            SqlCommand cmd = new SqlCommand(strSqlTextTechState, FrmMain.Instance.m_cnn);
            SqlDataReader rdr = cmd.ExecuteReader();
            string strTechStateList = "";
            string strTechStateListRec = "";
            int iCnt = 0;
            while (rdr.Read())
            {
                if (!rdr.IsDBNull(1))
                {
                    if (iCnt == 0)
                        strTechStateList = rdr.GetString(1);
                    else
                        strTechStateList = strTechStateList + "|" + rdr.GetString(1);
                    iCnt++;

                    strTechStateListRec = rdr.GetInt16(0).ToString() + ";" + rdr.GetString(1);
                    ListTechState.AddItem(strTechStateListRec);
                }
            }
            rdr.Close();

            c1FlxGrdTechUnit.Cols["StateName"].ComboList = strTechStateList;
            // не дает при double-click в ComboList показывать пункты по кругу
            c1FlxGrdTechUnit.EditOptions = c1FlxGrdTechUnit.EditOptions & ~C1.Win.C1FlexGrid.EditFlags.CycleOnDoubleClick;
        }

        private void btnTest_Click(object sender, EventArgs e)
        {
            //int rtr = c1FlxGrdTechUnit.FindRow("312АЦ", 1, 2, true, true, false);
            
        }

        private void c1FlxGrdTechUnit_ComboDropDown(object sender, RowColEventArgs e)
        {
            l_strStateBef = c1FlxGrdTechUnit.GetDataDisplay(e.Row, e.Col);
            l_iStateCombo = ListTechState.Find(l_strStateBef, MatchCompareEnum.PartiallyEqual, true, 0, 1);
        }

        private void c1FlxGrdTechUnit_AfterEdit(object sender, C1.Win.C1FlexGrid.RowColEventArgs e)
        {
            if (e.Col == 4)
            {
                string strStateAft = c1FlxGrdTechUnit.GetDataDisplay(e.Row, e.Col);
                int iStateAft = ListTechState.Find(strStateAft, MatchCompareEnum.PartiallyEqual, true, 0, 1);
                if (l_iStateCombo != iStateAft)
                {
                    if (MessageBox.Show("Изменить состояние?", "Ввод состояния техники",
                                MessageBoxButtons.YesNo, MessageBoxIcon.Question)
                                == DialogResult.Yes)
                    {
                        string strStateId = ListTechState.GetItemText(iStateAft, 0);
                        string strTechnicsId = c1FlxGrdTechUnit.GetDataDisplay(c1FlxGrdTechUnit.RowSel, 1);
                        string strSqlTextDrv = "UPDATE TechnicsUnit SET StateId = '" + strStateId +
                                                    "' WHERE TechnicsId = " + strTechnicsId;
                        SqlCommand command = new SqlCommand(strSqlTextDrv, FrmMain.Instance.m_cnn);
                        Int32 recordsAffected = command.ExecuteNonQuery();
                    }
                    else
                        c1FlxGrdTechUnit[e.Row, e.Col] = l_strStateBef;
                }
            }
 
        }

        private void rdBtnBort_CheckedChanged(object sender, EventArgs e)
        {
            if (rdBtnBort.Checked)
            {
                LoadGridTech(1, "");
                LoadBoxFind("TechnicsName", bsTechUnit);
            }
        }

        private void cmbBoxFind_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyValue == 13)
            {
                if (rdBtnBort.Checked)
                {
                    string strBort = cmbBoxFind.Text;
                    int iFind = c1FlxGrdTechUnit.FindRow(strBort, 1, 2, true, true, true);
                    if (iFind > 0)
                    {
                        c1FlxGrdTechUnit.Select(iFind, 2);
                    }
                }
                if (rdBtnType.Checked)
                {
                    string strAbb = cmbBoxFind.Text;
                    LoadGridTech(2, strAbb);
                    
                }
                if (rdBtnPch.Checked)
                {
                    string strUnit = cmbBoxFind.Text;
                    LoadGridTech(3, strUnit);
                }
                if (rdBtnState.Checked)
                {
                    string strState = cmbBoxFind.Text;
                    LoadGridTech(4, strState);
                }
            }
        }

        private void rdBtnType_CheckedChanged(object sender, EventArgs e)
        {
            if (rdBtnType.Checked)
                LoadBoxFind("TechnicsAbbrev", bsTechAbb);
        }

        private void rdBtnPch_CheckedChanged(object sender, EventArgs e)
        {
            if (rdBtnPch.Checked)
                LoadBoxFind("UnitName", bsUnit);
        }

        private void rdBtnState_CheckedChanged(object sender, EventArgs e)
        {
            if (rdBtnState.Checked)
                LoadBoxFind("StateName", bsTechState);
        }

        
    }
}