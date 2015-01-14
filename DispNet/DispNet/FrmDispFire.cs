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
using System.Runtime.InteropServices;

namespace DispNet
{
    public partial class FrmDispFire : Form
    {
        static public FrmDispFire Instance;
        
        DataSet dsDisp = new DataSet();
        BindingSource bsDisp = new BindingSource();
        public string m_strRptDispId;
        string strStateListRec = "";
        string l_strStateBef = "";
        public bool m_blStateWnd = false;    //не дает открвать больше одного окна формы FrmDispFire
        public int m_iDispFireRowSel = 0;    //номер строки в FrmDispFire.Instance.c1FlxGrdDispMain для редактирования в pageTabFireObj
        int l_iStateCombo;  //не дает выводить сообщение если combo был просто открыт и закрыт на одном состоянии пожара
        public bool m_blShowCase1 = false;
        public string m_strDataTimeSqlUp = "00-00-00 00:00:00";  //дата конца периода для SQL формат MM-d-yyyy HH:mm:ss
        public string m_strDataTimeSqlDown = "00-00-00 00:00:00";//дата начала периода для SQL формат MM-d-yyyy HH:mm:ss
        public string m_strDataTimeAft; //дата конца периода формат  d-MM-yyyy HH:mm:ss
        public string m_strDataTimeBef; //дата начала периода формат  d-MM-yyyy HH:mm:ss

        public FrmDispFire()
        {
            InitializeComponent();
        }

        private void FrmDispFire_Load(object sender, EventArgs e)
        {
            string strSqlTextDisp = "";
            int iFireTime = FrmMain.Instance.m_iDispFireTime;
            try
            {

                if (iFireTime == 1)
                {
                    FrmDateRpl.Instance = new FrmDateRpl();
                    FrmDateRpl.Instance.m_iFrmLoad = 1;
                    FrmDateRpl.Instance.ShowDialog();
                    if (!m_blShowCase1)
                        Close();    // закрывает эту форму есле не выбран период
                }
                switch (iFireTime)
                {
                    case 1:
                        strSqlTextDisp = "SELECT ReportDisp.RptDispId, ReportDisp.RptDate, ReportDisp.RptDispMsg, " +
                                     "StateChsDrv.StateChsName, DepartType.DepartTypeName, DepartType.DepartTypeId " +
                                     "FROM ReportDisp INNER JOIN " +
                                     "DepartType ON ReportDisp.DepartTypeId = DepartType.DepartTypeId INNER JOIN " +
                                     "StateChsDrv ON ReportDisp.StateChsId = StateChsDrv.StateChsId " +
                                     "WHERE(ReportDisp.RptDate >= CONVERT(DATETIME, '" + m_strDataTimeSqlDown + "', 102) " +
                                     "AND ReportDisp.RptDate <= CONVERT(DATETIME, '" + m_strDataTimeSqlUp + "', 102)) " +
                                     "ORDER BY ReportDisp.RptDate";
                        break;
                    case 2:
                        DateTime myDateTime, aftDateTime, myDateFnd;
                        myDateTime = DateTime.Now;
                        aftDateTime = DateTime.Parse("8:00:00");
                        int iCmp = DateTime.Compare(myDateTime, aftDateTime);
                        if (iCmp < 0)
                        {
                            m_strDataTimeSqlUp = myDateTime.Month.ToString() + "-" + myDateTime.Day.ToString() + "-" + myDateTime.Year.ToString() + " 08:00:00";
                            //int iDay_1 = myDateTime.Day - 1;
                            myDateFnd = FrmMain.Instance.m_fnFindDateAft(myDateTime);
                            m_strDataTimeSqlDown = myDateFnd.Month.ToString() + "-" + myDateFnd.Day.ToString() + "-" + myDateFnd.Year.ToString() + " 08:00:00";

                            strSqlTextDisp = "SELECT ReportDisp.RptDispId, ReportDisp.RptDate, ReportDisp.RptDispMsg, " +
                                         "StateChsDrv.StateChsName, DepartType.DepartTypeName, DepartType.DepartTypeId " +
                                         "FROM ReportDisp INNER JOIN " +
                                         "DepartType ON ReportDisp.DepartTypeId = DepartType.DepartTypeId INNER JOIN " +
                                         "StateChsDrv ON ReportDisp.StateChsId = StateChsDrv.StateChsId " +
                                         "WHERE(ReportDisp.RptDate >= CONVERT(DATETIME, '" + m_strDataTimeSqlDown + "', 102) " +
                                         "AND ReportDisp.RptDate <= CONVERT(DATETIME, '" + m_strDataTimeSqlUp + "', 102)) " +
                                         "ORDER BY ReportDisp.RptDate";
                        }
                        if (iCmp > 0)
                        {
                            m_strDataTimeSqlUp = myDateTime.Month.ToString() + "-" + myDateTime.Day.ToString() + "-" + myDateTime.Year.ToString() + " 08:00:00";
                            
                            strSqlTextDisp = "SELECT ReportDisp.RptDispId, ReportDisp.RptDate, ReportDisp.RptDispMsg, " +
                                         "StateChsDrv.StateChsName, DepartType.DepartTypeName, DepartType.DepartTypeId " +
                                         "FROM ReportDisp INNER JOIN " +
                                         "DepartType ON ReportDisp.DepartTypeId = DepartType.DepartTypeId INNER JOIN " +
                                         "StateChsDrv ON ReportDisp.StateChsId = StateChsDrv.StateChsId " +
                                         "WHERE(ReportDisp.RptDate >= CONVERT(DATETIME, '" + m_strDataTimeSqlUp + "', 102)) " +
                                         "ORDER BY ReportDisp.RptDate";
                        }
                        break;
                    case 3:
                        strSqlTextDisp = "SELECT ReportDisp.RptDispId, ReportDisp.RptDate, ReportDisp.RptDispMsg, " +
                                     "StateChsDrv.StateChsName, DepartType.DepartTypeName, DepartType.DepartTypeId " +
                                     "FROM ReportDisp INNER JOIN " +
                                     "DepartType ON ReportDisp.DepartTypeId = DepartType.DepartTypeId INNER JOIN " +
                                     "StateChsDrv ON ReportDisp.StateChsId = StateChsDrv.StateChsId " +
                                     "WHERE(DepartType.DepartTypeId < 3 AND ReportDisp.StateChsId != 4) " +
                                     "ORDER BY ReportDisp.RptDate";
                        break;
                }
                SqlDataAdapter daDisp = new SqlDataAdapter(strSqlTextDisp, FrmMain.Instance.m_cnn);
                daDisp.Fill(dsDisp, "ReportDisp");
                bsDisp.DataSource = dsDisp;
                bsDisp.DataMember = "ReportDisp";
                if (bsDisp.Count > 0)
                {
                    c1FlxGrdDispMain.SetDataBinding(dsDisp, "ReportDisp");

                    LoadStateFire();

                    c1FlxGrdDispMain.Cols[1].Visible = false;
                    c1FlxGrdDispMain.Cols[6].Visible = false;
                    c1FlxGrdDispMain.Cols[2].Format = "G";
                    c1FlxGrdDispMain.Cols[2].Width = 110;
                    c1FlxGrdDispMain.Cols[2].Caption = "Дата и время";
                    c1FlxGrdDispMain.Cols[2].AllowEditing = false;
                    c1FlxGrdDispMain.Cols[3].Width = 250;
                    //c1FlxGrdDispMain.Cols[3].AllowEditing = false;
                    c1FlxGrdDispMain.Cols[3].Caption = "Наименование сообщения";
                    c1FlxGrdDispMain.Cols[3].ComboList = "...";
                    c1FlxGrdDispMain.Cols[4].Width = 100;
                    c1FlxGrdDispMain.Cols[4].Caption = "Состояние";
                    c1FlxGrdDispMain.Cols[5].Width = 120;
                    c1FlxGrdDispMain.Cols[5].AllowEditing = false;
                    c1FlxGrdDispMain.Cols[5].Caption = "Тип сообщения";

                    CellStyle cs = c1FlxGrdDispMain.Styles.Add("s1");
                    cs.Font = new Font("Arial", 9, FontStyle.Bold);
                    cs.TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.CenterCenter;
                    c1FlxGrdDispMain.Rows[0].Style = cs;

                    m_blStateWnd = true;
                    string strCapFormMsg = "Журнал сообщений";
                    switch (iFireTime)
                    {
                        case 1:
                            strCapFormMsg = strCapFormMsg + " за период с " + m_strDataTimeAft + " по " + m_strDataTimeBef;
                            break;
                        case 2:
                            strCapFormMsg = strCapFormMsg + " за текущие сутки.";
                            break;
                        case 3:
                            strCapFormMsg = strCapFormMsg + " текущих пожаров.";
                            break;
                    }
                    FrmDispFire.Instance.Text = strCapFormMsg;
                }
                else
                {
                    string strMsgCntRec = "Записей";
                    switch (iFireTime)
                    {
                        case 1:
                            strMsgCntRec = strMsgCntRec + " за период с " + m_strDataTimeAft + " по " + m_strDataTimeBef +
                                "\n" + "не найдено!";
                            break;
                        case 2:
                            strMsgCntRec = strMsgCntRec + " за текущие сутки не найдено!";
                            break;
                        case 3:
                            strMsgCntRec = strMsgCntRec + " текущих пожаров не найдено!";
                            break;
                    }
                    MessageBox.Show(strMsgCntRec, "Диспетчерский журнал",
                                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                    Close();
                }
            }
            catch (SqlException ex)
            {
                for (int i = 0; i < ex.Errors.Count; i++)
                {
                    string strErrTxt = ex.Errors[i].Message; 
                }
            }
        }

        private void LoadStateFire()
        {
            string strSqlTextState = "SELECT StateChsId, StateChsName FROM StateChsDrv ORDER BY StateChsName";
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

            int iRws = c1FlxGrdDispMain.Rows.Count - 1;
            CellStyle cs = c1FlxGrdDispMain.Styles.Add("state");
            cs.DataType = typeof(string);
            cs.ComboList = strStateList;
            // assign styles to editable cells
            if (iRws > 0)
            {
                CellRange rg = c1FlxGrdDispMain.GetCellRange(0, 4, iRws, 4);
                rg.Style = c1FlxGrdDispMain.Styles["state"];
            }
            // не дает при double-click в ComboList показывать пункты по кругу
            c1FlxGrdDispMain.EditOptions = c1FlxGrdDispMain.EditOptions & ~C1.Win.C1FlexGrid.EditFlags.CycleOnDoubleClick;
        }

        private void DispFireType_Show()
        {
            m_strRptDispId = c1FlxGrdDispMain.GetDataDisplay(c1FlxGrdDispMain.RowSel, 1);
            string strDepartType = c1FlxGrdDispMain.GetDataDisplay(c1FlxGrdDispMain.RowSel, 6);
            switch (int.Parse(strDepartType))
            {
                case 1:
                    FrmDispUkg.Instance = new FrmDispUkg();
                    FrmDispUkg.Instance.Text = "Объект - " + c1FlxGrdDispMain.GetDataDisplay(c1FlxGrdDispMain.RowSel, 3);
                    m_iDispFireRowSel = c1FlxGrdDispMain.RowSel;    //передает в форму pageTabFireObj значение выбранного Row
                    FrmDispUkg.Instance.Show();
                    break;
                case 2:
                    FrmDispObl.Instance = new FrmDispObl();
                    FrmDispObl.Instance.Text = "Объект - " + c1FlxGrdDispMain.GetDataDisplay(c1FlxGrdDispMain.RowSel, 3);
                    m_iDispFireRowSel = c1FlxGrdDispMain.RowSel;    //передает в форму pageTabFireObjObl значение выбранного Row
                    FrmDispObl.Instance.Show();
                    break;
                case 3:
                    FrmDispUkgDrv.Instance = new FrmDispUkgDrv();
                    FrmDispUkgDrv.Instance.Text = "Город выезд - " + c1FlxGrdDispMain.GetDataDisplay(c1FlxGrdDispMain.RowSel, 3);
                    m_iDispFireRowSel = c1FlxGrdDispMain.RowSel;
                    FrmDispUkgDrv.Instance.Show();
                    break;
                case 4:
                    FrmBortDrvObl.Instance = new FrmBortDrvObl();
                    FrmBortDrvObl.Instance.Text = "Область выезд - " + c1FlxGrdDispMain.GetDataDisplay(c1FlxGrdDispMain.RowSel, 3);
                    m_iDispFireRowSel = c1FlxGrdDispMain.RowSel;
                    FrmBortDrvObl.Instance.m_iLoadDispRpt = 2;
                    FrmBortDrvObl.Instance.Show();
                    break;
                case 5:
                    FrmMenDrvUkg.Instance = new FrmMenDrvUkg();
                    FrmMenDrvUkg.Instance.Text = "Выезд без техники - " + c1FlxGrdDispMain.GetDataDisplay(c1FlxGrdDispMain.RowSel, 3);
                    m_iDispFireRowSel = c1FlxGrdDispMain.RowSel;
                    FrmMenDrvUkg.Instance.m_iLoadDispRpt = 2;
                    FrmMenDrvUkg.Instance.Show();
                    break;
                case 6:
                    FrmMsgService.Instance = new FrmMsgService();
                    FrmMsgService.Instance.Text = "Объект - " + c1FlxGrdDispMain.GetDataDisplay(c1FlxGrdDispMain.RowSel, 3);
                    m_iDispFireRowSel = c1FlxGrdDispMain.RowSel;
                    FrmMsgService.Instance.m_iLoadDispRpt = 2;
                    FrmMsgService.Instance.Show();
                    break;
            }
        }
                
        private void c1FlxGrdDispMain_AfterEdit(object sender, RowColEventArgs e)
        {
            if (e.Col == 4)
            {
                string strStateAft = c1FlxGrdDispMain.GetDataDisplay(e.Row, e.Col);
                int iStateAft = ListState.Find(strStateAft, MatchCompareEnum.PartiallyEqual, true, 0, 1);
                if (l_iStateCombo != iStateAft)
                {
                    if (MessageBox.Show("Изменить состояние?", "Ввод состояния пожара/чс",
                                MessageBoxButtons.YesNo, MessageBoxIcon.Question)
                                == DialogResult.Yes)
                    {
                        string strStateId = ListState.GetItemText(iStateAft, 0);
                        string strRptDispId = c1FlxGrdDispMain.GetDataDisplay(c1FlxGrdDispMain.RowSel, 1);
                        string strSqlTextDrv = "UPDATE ReportDisp SET StateChsId = '" + strStateId +
                                                        "' WHERE RptDispId = " + strRptDispId;
                        SqlCommand command = new SqlCommand(strSqlTextDrv, FrmMain.Instance.m_cnn);
                        Int32 recordsAffected = command.ExecuteNonQuery();
                    }
                    else
                        c1FlxGrdDispMain[e.Row, e.Col] = l_strStateBef;
                }
            }
        }

        private void c1FlxGrdDispMain_DoubleClick(object sender, EventArgs e)
        {
            if (c1FlxGrdDispMain.ColSel == 2 || c1FlxGrdDispMain.ColSel == 3)
                DispFireType_Show();
        }

        private void FrmDispFire_FormClosed(object sender, FormClosedEventArgs e)
        {
            FrmMain.Instance.m_blFrmDispFireShow = false;
        }

        private void c1FlxGrdDispMain_ComboDropDown(object sender, RowColEventArgs e)
        {
            l_strStateBef = c1FlxGrdDispMain.GetDataDisplay(e.Row, e.Col);
            l_iStateCombo = ListState.Find(l_strStateBef, MatchCompareEnum.PartiallyEqual, true, 0, 1);
        }

        private void c1FlxGrdDispMain_CellButtonClick(object sender, RowColEventArgs e)
        {
            if (e.Col == 3)
            {
                DispFireType_Show();
            }
        }
    }
}