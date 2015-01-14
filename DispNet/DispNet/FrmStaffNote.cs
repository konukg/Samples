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
    public partial class FrmStaffNote : Form
    {
        static public FrmStaffNote Instance;

        DataSet dsPchAll = new DataSet();
        BindingSource bsPchAll = new BindingSource();

        DataSet dsSaffUnitBnd = new DataSet();
        BindingSource bsSaffUnitBnd = new BindingSource();
        
        public string m_strPchId = ""; // Id п/части
        public string m_strSmena = "";

        bool blBsLoad = false; //задержка для данных которые присваиваются chekbox
        int iPosPch = 0;   //запоминает позицию и не дает изменить в сомбо если просто открыть и закрыть
        string strSmnBefEdit = "";  //запоминает номер смены перед редактирование и если отмена возвращает значение в таблицу
        
        string strStateBefEdit = "";  //запоминает состояние перед редактирование и если отмена возвращает значение в таблицу
        int l_iStateBefCombo = 0;   //запоминает Id состояния в List перед редактирование

        string strStaffStateList = ""; //список состояний для изменения в таблице

        public string m_strSmenaNmbRtn = "";    //выбранный номер смены

        public FrmStaffNote()
        {
            InitializeComponent();
        }

        private void FrmStaffNote_Load(object sender, EventArgs e)
        {
            string strSqlTextPchAll = "SELECT Unit.UnitName, Unit.UnitId FROM Unit INNER JOIN UnitNote ON Unit.UnitId = UnitNote.UnitId " +
                                        "ORDER BY Unit.UnitId";
            SqlDataAdapter daPchAll = new SqlDataAdapter(strSqlTextPchAll, FrmMain.Instance.m_cnn);
            daPchAll.Fill(dsPchAll, "Unit");
            bsPchAll.DataSource = dsPchAll;
            bsPchAll.DataMember = "Unit";
            c1CmbUnit.DataSource = bsPchAll;
            c1CmbUnit.ValueMember = "UnitName";
            c1CmbUnit.DisplayMember = "UnitName";
            c1CmbUnit.Splits[0].DisplayColumns[1].Visible = false;
            c1CmbUnit.Columns[0].Caption = "Подразделение";
            c1CmbUnit.Splits[0].DisplayColumns[0].HeadingStyle.HorizontalAlignment = C1.Win.C1List.AlignHorzEnum.Center;

            int iFndRow = c1CmbUnit.FindStringExact(m_strPchId, 0, 1);
            c1CmbUnit.Text = c1CmbUnit.Columns[0].CellText(iFndRow);

            string strSqlTextStaffState = "SELECT StateCount, StateName FROM StaffMyState ORDER BY StateName";
            SqlCommand cmd = new SqlCommand(strSqlTextStaffState, FrmMain.Instance.m_cnn);
            SqlDataReader rdr = cmd.ExecuteReader();
            
            string strStaffStateListRec = "";
            int iCnt = 0;
            while (rdr.Read())
            {
                if (!rdr.IsDBNull(1))
                {
                    if (iCnt == 0)
                        strStaffStateList = rdr.GetString(1);
                    else
                        strStaffStateList = strStaffStateList + "|" + rdr.GetString(1);
                    iCnt++;

                    strStaffStateListRec = rdr.GetInt16(0).ToString() + ";" + rdr.GetString(1);
                    ListStaffState.AddItem(strStaffStateListRec);
                }
            }
            rdr.Close();

            blBsLoad = true;
            SqlStaffNote_Load(false);
        }

        private void SqlStaffNote_Load(bool blAllSmena)
        {
            DataSet dsSaffUnit = new DataSet();
            BindingSource bsSaffUnit = new BindingSource();
            
            bool redraw = c1FlxGrdStaffUnit.Redraw; // save original value
            c1FlxGrdStaffUnit.Redraw = false;
            try
            {
                int iCntRws = c1FlxGrdStaffUnit.Rows.Count - 1;
                if (iCntRws > 1)
                {
                    for (int i = 0; i < iCntRws; i = i + 1)
                        c1FlxGrdStaffUnit.RemoveItem(1);
                }
                string strSqlTextStaffUnit = "";
                if (blAllSmena)
                {
                    strSqlTextStaffUnit = "SELECT Staff.StaffName, Staff.StaffName1, Staff.StaffName2, StaffState.StaffState, StaffState.StaffChange, " +
                            "Staff.StaffPost, StaffState.StaffReplace, StaffState.StaffGAZS, StaffState.StaffBattle, StaffState.StaffCount " +
                            "FROM Staff INNER JOIN StaffState ON Staff.StaffCount = StaffState.StaffCount " +
                            "INNER JOIN Unit ON Staff.UnitId = Unit.UnitId " +
                            "WHERE (Staff.UnitId = '" + m_strPchId + "') " +
                            "ORDER BY Staff.StaffName, Staff.StaffName1";
                }
                else
                {
                    strSqlTextStaffUnit = "SELECT Staff.StaffName, Staff.StaffName1, Staff.StaffName2, StaffState.StaffState, StaffState.StaffChange, " +
                            "Staff.StaffPost, StaffState.StaffReplace, StaffState.StaffGAZS, StaffState.StaffBattle, StaffState.StaffCount " +
                            "FROM Staff INNER JOIN StaffState ON Staff.StaffCount = StaffState.StaffCount " +
                            "INNER JOIN Unit ON Staff.UnitId = Unit.UnitId " +
                            "WHERE (StaffState.StaffChange = '" + m_strSmena + "') AND (Staff.UnitId = '" + m_strPchId + "') " +
                            "ORDER BY Staff.StaffName, Staff.StaffName1";
                }
                SqlDataAdapter daSaffUnit = new SqlDataAdapter(strSqlTextStaffUnit, FrmPermit.Instance.cnn);
                daSaffUnit.Fill(dsSaffUnit, "Staff");
                bsSaffUnit.DataSource = dsSaffUnit;
                bsSaffUnit.DataMember = "Staff";
                //делаем копии иначе не связать таблицу с навигатором
                dsSaffUnitBnd = dsSaffUnit.Copy();
                bsSaffUnitBnd = new BindingSource(dsSaffUnit, "Staff");
                //....
                c1DbNvgStaffNote.DataSource = bsSaffUnitBnd;
                c1FlxGrdStaffUnit.DataSource = c1DbNvgStaffNote.DataSource;
                
                               
                c1FlxGrdStaffUnit.Cols[7].Visible = false;
                c1FlxGrdStaffUnit.Cols[8].Visible = false;
                c1FlxGrdStaffUnit.Cols[9].Visible = false;
                c1FlxGrdStaffUnit.Cols[10].Visible = false;
                
                c1FlxGrdStaffUnit.Cols[1].Width = 90;
                c1FlxGrdStaffUnit.Cols[1].Caption = "Фамилия";
                c1FlxGrdStaffUnit.Cols[1].AllowEditing = false;

                c1FlxGrdStaffUnit.Cols[2].Width = 80;
                c1FlxGrdStaffUnit.Cols[2].Caption = "Имя";
                c1FlxGrdStaffUnit.Cols[2].AllowEditing = false;


                c1FlxGrdStaffUnit.Cols[3].Width = 110;
                c1FlxGrdStaffUnit.Cols[3].Caption = "Отчество";
                c1FlxGrdStaffUnit.Cols[3].AllowEditing = false;

                c1FlxGrdStaffUnit.Cols[4].Width = 110;
                c1FlxGrdStaffUnit.Cols[4].Caption = "Состояние";
                //c1FlxGrdStaffUnit.Cols[4].AllowEditing = false;

                c1FlxGrdStaffUnit.Cols[5].Width = 50;
                c1FlxGrdStaffUnit.Cols[5].Caption = "Смена";
                //c1FlxGrdStaffUnit.Cols[5].AllowEditing = false;
                c1FlxGrdStaffUnit.Cols["StaffChange"].ComboList = "0|1|2|3|4";

                c1FlxGrdStaffUnit.Cols[6].Width = 120;
                c1FlxGrdStaffUnit.Cols[6].Caption = "Должность";
                c1FlxGrdStaffUnit.Cols[6].AllowEditing = false;

                CellStyle cs = c1FlxGrdStaffUnit.Styles.Add("s1");
                cs.Font = new Font("Arial", 9, FontStyle.Bold);
                cs.TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.CenterCenter;
                c1FlxGrdStaffUnit.Rows[0].Style = cs;

                c1TxtBoxGAZS.DataSource = bsSaffUnitBnd;
                c1TxtBoxGAZS.DataField = "StaffGAZS";
                
                c1TxtBoxBoj.DataSource = bsSaffUnitBnd;
                c1TxtBoxBoj.DataField = "StaffBattle";
                
                c1TxtBoxRpl.DataSource = bsSaffUnitBnd;
                c1TxtBoxRpl.DataField = "StaffReplace";

                c1CmbVio.DataSource = bsSaffUnitBnd;
                c1CmbVio.ValueMember = "StaffName";
                c1CmbVio.DisplayMember = "StaffName";
                c1CmbVio.Columns[0].Caption = "Фамилия";
                c1CmbVio.Splits[0].DisplayColumns[0].HeadingStyle.HorizontalAlignment = C1.Win.C1List.AlignHorzEnum.Center;
                c1CmbVio.Columns[1].Caption = "Имя";
                c1CmbVio.Splits[0].DisplayColumns[1].HeadingStyle.HorizontalAlignment = C1.Win.C1List.AlignHorzEnum.Center;
                c1CmbVio.Columns[2].Caption = "Отчество";
                c1CmbVio.Splits[0].DisplayColumns[2].HeadingStyle.HorizontalAlignment = C1.Win.C1List.AlignHorzEnum.Center;
                c1CmbVio.Splits[0].DisplayColumns[3].Visible = false;
                c1CmbVio.Splits[0].DisplayColumns[4].Visible = false;
                c1CmbVio.Splits[0].DisplayColumns[5].Visible = false;
                c1CmbVio.Splits[0].DisplayColumns[6].Visible = false;
                c1CmbVio.Splits[0].DisplayColumns[7].Visible = false;
                c1CmbVio.Splits[0].DisplayColumns[8].Visible = false;
                c1CmbVio.Splits[0].DisplayColumns[9].Visible = false;

                c1FlxGrdStaffUnit.Cols["StaffState"].ComboList = strStaffStateList;
                // не дает при double-click в ComboList показывать пункты по кругу
                c1FlxGrdStaffUnit.EditOptions = c1FlxGrdStaffUnit.EditOptions & ~C1.Win.C1FlexGrid.EditFlags.CycleOnDoubleClick;

            }
            finally
            {
                c1FlxGrdStaffUnit.Redraw = redraw;
            }
            c1FlxGrdStaffUnit.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.Row;
            c1FlxGrdStaffUnit.Select(1, 0);
        }

        private void c1CmbUnit_Close(object sender, EventArgs e)
        {
            if (c1CmbUnit.ListCount > 0 && iPosPch != bsPchAll.Position)    //только если есть записи иначе ошибка
            {
                int iFndRow = c1CmbUnit.FindStringExact(c1CmbUnit.Text, 0, 0);
                m_strPchId = c1CmbUnit.Columns[1].CellText(iFndRow);
                if (chkBoxAllStaff.Checked)
                    SqlStaffNote_Load(true);
                else
                    SqlStaffNote_Load(false);
                
                iPosPch = bsPchAll.Position;
            }
        }

        private void chkBoxAllStaff_CheckedChanged(object sender, EventArgs e)
        {
            if (chkBoxAllStaff.Checked)
                SqlStaffNote_Load(true);
            else
                SqlStaffNote_Load(false);
        }

        private void c1TxtBoxGAZS_TextChanged(object sender, EventArgs e)
        {
            if(blBsLoad)
            {
            bool blChkGAZS = Convert.ToBoolean(c1TxtBoxGAZS.Text);
            if (blChkGAZS)
                chkBoxGDZ.Checked = true;
            else
                chkBoxGDZ.Checked = false;
            }
        }

        private void c1TxtBoxBoj_TextChanged(object sender, EventArgs e)
        {
            if (blBsLoad)
            {
                bool blChkBoj = Convert.ToBoolean(c1TxtBoxBoj.Text);
                if (blChkBoj)
                    chkBoxBoj.Checked = true;
                else
                    chkBoxBoj.Checked = false;
            }
        }

        private void c1TxtBoxRpl_TextChanged(object sender, EventArgs e)
        {
            string strRplSng = c1TxtBoxRpl.Text;
            if (strRplSng == "n")
                chkBoxPodm.Checked = false;
            else
                chkBoxPodm.Checked = true;
        }

        private void c1FlxGrdStaffUnit_ComboDropDown(object sender, RowColEventArgs e)
        {
            if (e.Col == 4)
            {
                strStateBefEdit = c1FlxGrdStaffUnit.GetDataDisplay(e.Row, e.Col);
                l_iStateBefCombo = ListStaffState.Find(strStateBefEdit, MatchCompareEnum.PartiallyEqual, true, 0, 1);
            }
            if (e.Col == 5)
                strSmnBefEdit = c1FlxGrdStaffUnit.GetDataDisplay(e.Row, e.Col);
            
        }

        private void c1FlxGrdStaffUnit_AfterEdit(object sender, RowColEventArgs e)
        {
            if (e.Col == 4)
            {
                string strStateAffEdit = c1FlxGrdStaffUnit.GetDataDisplay(e.Row, e.Col);
                int iStateAftEdit = ListStaffState.Find(strStateAffEdit, MatchCompareEnum.PartiallyEqual, true, 0, 1);
                if (l_iStateBefCombo != iStateAftEdit)
                {
                    if (MessageBox.Show("Изменить состояние?", "Ввод состояние смены",
                                MessageBoxButtons.YesNo, MessageBoxIcon.Question)
                                == DialogResult.Yes)
                    {
                        string strStaffCount = c1FlxGrdStaffUnit.GetDataDisplay(e.Row, 10);
                        string strSqlstrStaffCount = "UPDATE StaffState SET StaffState = '" + strStateAffEdit +
                                                        "' WHERE StaffCount = " + strStaffCount;
                        SqlCommand command = new SqlCommand(strSqlstrStaffCount, FrmMain.Instance.m_cnn);
                        Int32 recordsAffected = command.ExecuteNonQuery();
                    }
                    else
                        c1FlxGrdStaffUnit[e.Row, e.Col] = strStateBefEdit;
                }
            }

            if (e.Col == 5)
            {
                string strSmnAffEdit = c1FlxGrdStaffUnit.GetDataDisplay(e.Row, e.Col);
                if (strSmnBefEdit != strSmnAffEdit)
                {
                    if (MessageBox.Show("Изменить номер смены?", "Ввод номера смены",
                                MessageBoxButtons.YesNo, MessageBoxIcon.Question)
                                == DialogResult.Yes)
                    {
                        string strStaffCount = c1FlxGrdStaffUnit.GetDataDisplay(e.Row, 10);
                        string strSqlstrStaffCount = "UPDATE StaffState SET StaffChange = '" + strSmnAffEdit +
                                                        "' WHERE StaffCount = " + strStaffCount;
                        SqlCommand command = new SqlCommand(strSqlstrStaffCount, FrmMain.Instance.m_cnn);
                        Int32 recordsAffected = command.ExecuteNonQuery();
                    }
                    else
                        c1FlxGrdStaffUnit[e.Row, e.Col] = strSmnBefEdit;
                }
            }
        }

        private void chkBoxPodm_MouseUp(object sender, MouseEventArgs e)
        {
            bool blStChk = chkBoxPodm.Checked;
            int iRowId = c1FlxGrdStaffUnit.Row;
            if (blStChk)
            {
                m_strSmenaNmbRtn = "";
                FrmInputSmena.Instance = new FrmInputSmena();
                string strSmenaNmbBef = c1FlxGrdStaffUnit.GetDataDisplay(iRowId, 5);
                FrmInputSmena.Instance.m_strSmenaNmb = strSmenaNmbBef;
                FrmInputSmena.Instance.m_iSngFrm = 1;
                FrmInputSmena.Instance.ShowDialog();

                if (m_strSmenaNmbRtn != "")
                {
                    string strSmnPodState = "";
                    if (chkBoxBoj.Checked)
                        strSmnPodState = "Дежурство в б/р";
                    else
                        strSmnPodState = "На дежурстве";

                    string strStaffCount = c1FlxGrdStaffUnit.GetDataDisplay(iRowId, 10);
                    string strSqlstrStaffCount = "UPDATE StaffState SET StaffChange = '" + m_strSmenaNmbRtn + "', StaffReplace = '" + strSmenaNmbBef + "', " +
                        "StaffState = '" + strSmnPodState + "' " +
                        "WHERE StaffCount = " + strStaffCount;
                    SqlCommand command = new SqlCommand(strSqlstrStaffCount, FrmMain.Instance.m_cnn);
                    Int32 recordsAffected = command.ExecuteNonQuery();

                    if (chkBoxAllStaff.Checked)
                        SqlStaffNote_Load(true);
                    else
                        SqlStaffNote_Load(false);
                    c1FlxGrdStaffUnit.Select(iRowId, 1);
                }
                else
                    chkBoxPodm.Checked = false;
            }
            else
            {
                if (MessageBox.Show("Отменить подмену?", "Ввод номера смены",
                                MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2 )
                                == DialogResult.Yes)
                {
                    string strStaffCount = c1FlxGrdStaffUnit.GetDataDisplay(iRowId, 10);
                    string strSmenaNmbRepl = c1FlxGrdStaffUnit.GetDataDisplay(iRowId, 7);
                    string strSmnState = "Отдых";
                    string strSqlstrStaffCount = "UPDATE StaffState SET StaffChange = '" + strSmenaNmbRepl + "', StaffReplace = 'n', " +
                        "StaffState = '" + strSmnState + "' " +
                        "WHERE StaffCount = " + strStaffCount;
                    SqlCommand command = new SqlCommand(strSqlstrStaffCount, FrmMain.Instance.m_cnn);
                    Int32 recordsAffected = command.ExecuteNonQuery();

                    if (chkBoxAllStaff.Checked)
                        SqlStaffNote_Load(true);
                    else
                        SqlStaffNote_Load(false);
                    c1FlxGrdStaffUnit.Select(iRowId, 1);
                }
                else
                    chkBoxPodm.Checked = true;
            }
        }

        private void chkBoxGDZ_MouseUp(object sender, MouseEventArgs e)
        {
            bool blChkGDS = chkBoxGDZ.Checked;
            int iRowId = c1FlxGrdStaffUnit.Row;
            if (blChkGDS)
            {
                if (MessageBox.Show("Установить ГДЗащ.?", "Ввод состояния ГДЗащ.",
                                MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2)
                                == DialogResult.Yes)
                {
                    string strStaffCount = c1FlxGrdStaffUnit.GetDataDisplay(iRowId, 10);
                    string strSqlstrStaffCount = "UPDATE StaffState SET StaffGAZS = '1' " +
                        "WHERE StaffCount = " + strStaffCount;
                    SqlCommand command = new SqlCommand(strSqlstrStaffCount, FrmMain.Instance.m_cnn);
                    Int32 recordsAffected = command.ExecuteNonQuery();

                    if (chkBoxAllStaff.Checked)
                        SqlStaffNote_Load(true);
                    else
                        SqlStaffNote_Load(false);
                    c1FlxGrdStaffUnit.Select(iRowId, 1);
                }
                else
                    chkBoxGDZ.Checked = false;
            }
            else
            {
                if (MessageBox.Show("Снять ГДЗащ.?", "Ввод состояния ГДЗащ.",
                                MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2)
                                == DialogResult.Yes)
                {
                    string strStaffCount = c1FlxGrdStaffUnit.GetDataDisplay(iRowId, 10);
                    string strSqlstrStaffCount = "UPDATE StaffState SET StaffGAZS = '0' " +
                        "WHERE StaffCount = " + strStaffCount;
                    SqlCommand command = new SqlCommand(strSqlstrStaffCount, FrmMain.Instance.m_cnn);
                    Int32 recordsAffected = command.ExecuteNonQuery();

                    if (chkBoxAllStaff.Checked)
                        SqlStaffNote_Load(true);
                    else
                        SqlStaffNote_Load(false);
                    c1FlxGrdStaffUnit.Select(iRowId, 1);
                }
                else
                    chkBoxGDZ.Checked = true;
            }
        }

        private void chkBoxBoj_MouseUp(object sender, MouseEventArgs e)
        {
            bool blChkBoj = chkBoxBoj.Checked;
            int iRowId = c1FlxGrdStaffUnit.Row;
            if (blChkBoj)
            {
                if (MessageBox.Show("Установить В б/р.?", "Ввод состояния В б/р.",
                                MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2)
                                == DialogResult.Yes)
                {
                    string strStaffCount = c1FlxGrdStaffUnit.GetDataDisplay(iRowId, 10);
                    string strSqlstrStaffCount = "UPDATE StaffState SET StaffBattle = '1' " +
                        "WHERE StaffCount = " + strStaffCount;
                    SqlCommand command = new SqlCommand(strSqlstrStaffCount, FrmMain.Instance.m_cnn);
                    Int32 recordsAffected = command.ExecuteNonQuery();

                    if (chkBoxAllStaff.Checked)
                        SqlStaffNote_Load(true);
                    else
                        SqlStaffNote_Load(false);
                    c1FlxGrdStaffUnit.Select(iRowId, 1);
                }
                else
                    chkBoxBoj.Checked = false;
            }
            else
            {
                if (MessageBox.Show("Снять В б/р.?", "Ввод состояния В б/р.",
                                MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2)
                                == DialogResult.Yes)
                {
                    string strStaffCount = c1FlxGrdStaffUnit.GetDataDisplay(iRowId, 10);
                    string strSqlstrStaffCount = "UPDATE StaffState SET StaffBattle = '0' " +
                        "WHERE StaffCount = " + strStaffCount;
                    SqlCommand command = new SqlCommand(strSqlstrStaffCount, FrmMain.Instance.m_cnn);
                    Int32 recordsAffected = command.ExecuteNonQuery();

                    if (chkBoxAllStaff.Checked)
                        SqlStaffNote_Load(true);
                    else
                        SqlStaffNote_Load(false);
                    c1FlxGrdStaffUnit.Select(iRowId, 1);
                }
                else
                    chkBoxBoj.Checked = true;
            }
        }
    }
}
