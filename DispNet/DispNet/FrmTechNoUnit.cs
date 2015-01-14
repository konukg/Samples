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
    public partial class FrmTechNoUnit : Form
    {
        static public FrmTechNoUnit Instance;

        string strStateListRec = "";
        string l_strStateBef = "";
        int l_iStateCombo;  //не дает выводить сообщение если combo был просто открыт и закрыт на одном состоянии пожара

        public FrmTechNoUnit()
        {
            InitializeComponent();
        }

        private void FrmTechNoUnit_Load(object sender, EventArgs e)
        {
            string strSqlTextTechNoUnit = "SELECT TechnicsUnit.TechnicsId, TechnicsUnit.TechnicsName, TechnicsState.StateName, " +
                    "Unit.UnitName, ReportDisp.RptDispMsg, ReportDisp.RptDispId " +
                    "FROM ReportFireBort INNER JOIN " +
                    "ReportDisp ON ReportFireBort.RptDispId = ReportDisp.RptDispId INNER JOIN " +
                    "TechnicsUnit ON ReportFireBort.TechnicsId = TechnicsUnit.TechnicsId INNER JOIN " +
                    "TechnicsState ON TechnicsUnit.StateId = TechnicsState.StateId INNER JOIN " +
                    "Unit ON TechnicsUnit.UnitId = Unit.UnitId " +
                    "WHERE (ReportFireBort.BortReturn Is Null) " + //= '00:00:00') " +
                    "ORDER BY ReportDisp.RptDispId";
            DataSet dsTechNoUnit = new DataSet();
            BindingSource bsTechNoUnit = new BindingSource();
            SqlDataAdapter daTechNoUnit = new SqlDataAdapter(strSqlTextTechNoUnit, FrmPermit.Instance.cnn);
            daTechNoUnit.Fill(dsTechNoUnit, "TechnicsUnit");
            bsTechNoUnit.DataSource = dsTechNoUnit;
            bsTechNoUnit.DataMember = "TechnicsUnit";

            c1FlxGrdTechNoUnit.SetDataBinding(dsTechNoUnit, "TechnicsUnit");
            c1FlxGrdTechNoUnit.Cols[1].Visible = false;
            c1FlxGrdTechNoUnit.Cols[6].Visible = false;

            c1FlxGrdTechNoUnit.Cols[2].Width = 70;
            c1FlxGrdTechNoUnit.Cols[2].Caption = "Борт";
            c1FlxGrdTechNoUnit.Cols[2].AllowEditing = false;

            c1FlxGrdTechNoUnit.Cols[3].Width = 120;
            c1FlxGrdTechNoUnit.Cols[3].Caption = "Состояние";
            //c1FlxGrdTechNoUnit.Cols[3].AllowEditing = false;

            c1FlxGrdTechNoUnit.Cols[4].Width = 70;
            c1FlxGrdTechNoUnit.Cols[4].Caption = "П/часть";
            c1FlxGrdTechNoUnit.Cols[4].AllowEditing = false;

            c1FlxGrdTechNoUnit.Cols[5].Width = 170;
            c1FlxGrdTechNoUnit.Cols[5].Caption = "Примечание";
            c1FlxGrdTechNoUnit.Cols[5].AllowEditing = false;

            CellStyle cs = c1FlxGrdTechNoUnit.Styles.Add("s1");
            cs.Font = new Font("Arial", 9, FontStyle.Bold);
            cs.TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.CenterCenter;
            c1FlxGrdTechNoUnit.Rows[0].Style = cs;

            LoadStateFire();
        }

        private void LoadStateFire()
        {
            string strSqlTextState = "SELECT StateId, StateName FROM TechnicsState ORDER BY StateName";
            SqlCommand cmd = new SqlCommand(strSqlTextState, FrmMain.Instance.m_cnn);
            SqlDataReader rdr = cmd.ExecuteReader();
            string strStateList = "";
            int iCnt = 0;
            while (rdr.Read())
            {
                if (!rdr.IsDBNull(1))
                {
                    if (iCnt == 0)
                        strStateList = rdr.GetString(1);
                    else
                        strStateList = strStateList + "|" + rdr.GetString(1);
                    iCnt++;

                    strStateListRec = rdr.GetInt16(0).ToString() + ";" + rdr.GetString(1);
                    ListState.AddItem(strStateListRec);
                }
            }
            rdr.Close();

            int iRws = c1FlxGrdTechNoUnit.Rows.Count - 1;
            CellStyle cs = c1FlxGrdTechNoUnit.Styles.Add("state");
            cs.DataType = typeof(string);
            cs.ComboList = strStateList;
            // assign styles to editable cells
            if (iRws > 0)
            {
                CellRange rg = c1FlxGrdTechNoUnit.GetCellRange(0, 3, iRws, 3);
                rg.Style = c1FlxGrdTechNoUnit.Styles["state"];
            }
            // не дает при double-click в ComboList показывать пункты по кругу
            c1FlxGrdTechNoUnit.EditOptions = c1FlxGrdTechNoUnit.EditOptions & ~C1.Win.C1FlexGrid.EditFlags.CycleOnDoubleClick;
        }

        private void c1FlxGrdTechNoUnit_ComboDropDown(object sender, RowColEventArgs e)
        {
            l_strStateBef = c1FlxGrdTechNoUnit.GetDataDisplay(e.Row, e.Col);
            l_iStateCombo = ListState.Find(l_strStateBef, MatchCompareEnum.PartiallyEqual, true, 0, 1);
        }

        private void c1FlxGrdTechNoUnit_AfterEdit(object sender, RowColEventArgs e)
        {
            if (e.Col == 3)
            {
                string strStateAft = c1FlxGrdTechNoUnit.GetDataDisplay(e.Row, e.Col);
                int iStateAft = ListState.Find(strStateAft, MatchCompareEnum.PartiallyEqual, true, 0, 1);
                if (l_iStateCombo != iStateAft)
                {
                    if (MessageBox.Show("Изменить состояние?", "Ввод состояния техники",
                                MessageBoxButtons.YesNo, MessageBoxIcon.Question)
                                == DialogResult.Yes)
                    {
                        string strStateId = ListState.GetItemText(iStateAft, 0);
                        string strTechnicsId = c1FlxGrdTechNoUnit.GetDataDisplay(c1FlxGrdTechNoUnit.RowSel, 1);
                        string strSqlTextDrv = "UPDATE TechnicsUnit SET StateId = '" + strStateId +
                                                    "' WHERE TechnicsId = " + strTechnicsId;
                        SqlCommand command = new SqlCommand(strSqlTextDrv, FrmMain.Instance.m_cnn);
                        Int32 recordsAffected = command.ExecuteNonQuery();
                    }
                    else
                        c1FlxGrdTechNoUnit[e.Row, e.Col] = l_strStateBef;
                }
            }
        }
    }
}