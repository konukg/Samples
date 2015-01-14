using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using C1.Win.C1FlexGrid;
using System.Timers;
using System.Data.SqlClient;

namespace DispNet
{
    public partial class FrmMonAlert : Form
    {
        static public FrmMonAlert Instance;
        private System.Media.SoundPlayer myPlayer;
        System.Timers.Timer aTimer = new System.Timers.Timer(1000);
        public string m_strLiterMon;
        
        public FrmMonAlert()
        {
            InitializeComponent();
        }

        private void FrmMonAlert_Load(object sender, EventArgs e)
        {
            FrmMain.Instance.m_blFrmMonAlert = true;
            
            myPlayer = new System.Media.SoundPlayer();
            myPlayer.SoundLocation = "ringout.wav";
            aTimer.Elapsed += new ElapsedEventHandler(OnTimedEvent);
            aTimer.AutoReset = true;
   
        }

        public void m_ActivMonAlert()
        {
            string[] strAlertPath;
            strAlertPath = m_strLiterMon.Split('#');
            //int iAlert = Convert.ToInt32(strAlertPath[0]);
            //string strAlertOrg = strAlertPath[1];
            string strAlertOrg = "1";   //прометей-б
            string[] strLitPath;
            strLitPath = strAlertPath[1].Split('\0');
            string strAlertLit = strLitPath[0];
                        
            string strSqlTextObjMon = "SELECT ObjUkg.ObjId, ObjUkg.BuildId, ObjUkg.ObjName, ObjUkg.ObjTel, ObjUkg.ObjAutoMon, " +
                "ObjUkgStaff.OrgName, ObjUkgStaff.DirName, ObjUkgStaff.DirPhone, ObjUkgStaff.DispPhone, " +
                "Buildings.BuildName, BuildingType.BuildTypeName, StreetType.StreetTypeName, Street.StreetName, Unit.UnitName, " +
                "OrgObjMon.OrgMonName, OrgObjMon.OrgMonTel, Street.StreetId, OrgObjMon.OrgObjMonId " +
                "FROM ObjUkg INNER JOIN " +
                      "ObjUkgStaff ON ObjUkg.ObjStaffId = ObjUkgStaff.ObjStaffId INNER JOIN " +
                      "Buildings ON ObjUkg.BuildId = Buildings.BuildId INNER JOIN " +
                      "BuildingType ON Buildings.BuildTypeId = BuildingType.BuildTypeId INNER JOIN " +
                      "Street ON Buildings.StreetId = Street.StreetId INNER JOIN " +
                      "StreetType ON Street.StreetTypeId = StreetType.StreetTypeId INNER JOIN " +
                      "Unit ON ObjUkg.UnitId = Unit.UnitId INNER JOIN " +
                      "OrgObjMon ON ObjUkg.OrgObjMonId = OrgObjMon.OrgObjMonId " +
                "WHERE (ObjUkg.ObjAutoMon = 1) AND (ObjUkg.LitObjMon = '" + strAlertLit + "') AND (ObjUkg.OrgObjMonId = '" + strAlertOrg + "') " +
                "ORDER BY ObjUkg.ObjId";
            SqlCommand cmdObjMon = new SqlCommand(strSqlTextObjMon, FrmMain.Instance.m_cnn);
            SqlDataReader rdrObjMon = cmdObjMon.ExecuteReader();
            string strObjId = "";
            string strBuildId = "";
            string strObjName = "";
            string strObjTel = "";
            string strOrgName = "";
            string strDirName = "";
            string strDirPhone = "";
            string strDispPhone = "";
            string strBuildName = "";
            string strBuildTypeName = "";
            string strStreetTypeName = "";
            string strStreetName = "";
            string strUnitName = "";
            string strOrgMonName = "";
            string strOrgMonTel = "";
            string strStreetId = "";
            string strOrgObjMonId = "";
            int iCntFndObj = 0;
            while (rdrObjMon.Read())
            {
                if (!rdrObjMon.IsDBNull(0))
                    strObjId = rdrObjMon.GetInt32(0).ToString();
                if (!rdrObjMon.IsDBNull(1))
                    strBuildId = rdrObjMon.GetInt32(1).ToString();
                if (!rdrObjMon.IsDBNull(2))
                    strObjName = rdrObjMon.GetString(2);
                if (!rdrObjMon.IsDBNull(3))
                    strObjTel = rdrObjMon.GetString(3);
                if (!rdrObjMon.IsDBNull(5))
                    strOrgName = rdrObjMon.GetString(5);
                if (!rdrObjMon.IsDBNull(6))
                    strDirName = rdrObjMon.GetString(6);
                if (!rdrObjMon.IsDBNull(7))
                    strDirPhone = rdrObjMon.GetString(7);
                if (!rdrObjMon.IsDBNull(8))
                    strDispPhone = rdrObjMon.GetString(8);
                if (!rdrObjMon.IsDBNull(9))
                    strBuildName = rdrObjMon.GetString(9);
                if (!rdrObjMon.IsDBNull(10))
                    strBuildTypeName = rdrObjMon.GetString(10);
                if (!rdrObjMon.IsDBNull(11))
                    strStreetTypeName = rdrObjMon.GetString(11);
                if (!rdrObjMon.IsDBNull(12))
                    strStreetName = rdrObjMon.GetString(12);
                if (!rdrObjMon.IsDBNull(13))
                    strUnitName = rdrObjMon.GetString(13);
                if (!rdrObjMon.IsDBNull(14))
                    strOrgMonName = rdrObjMon.GetString(14);
                if (!rdrObjMon.IsDBNull(15))
                    strOrgMonTel = rdrObjMon.GetString(15);
                if (!rdrObjMon.IsDBNull(16))
                    strStreetId = rdrObjMon.GetInt32(16).ToString();
                if (!rdrObjMon.IsDBNull(17))
                    strOrgObjMonId = rdrObjMon.GetInt16(17).ToString();
                iCntFndObj++;
            }
            rdrObjMon.Close();

            string strObjInfo = "";
            string strGridInfo = "";
            if (iCntFndObj == 1)    // записывает данные если найден только один объект
            {
                strObjInfo = "Адрес: " + strStreetTypeName + " " + strStreetName + " дом: " + strBuildName + "\r\n" +
                    "тел.: " + strObjTel + " (" + strBuildTypeName + ")" + "\r\n" +
                    "п\\часть: " + strUnitName + "\r\n" +
                    "Организация: " + strOrgName + "\r\n" + strDirName + " тел.: " + strDirPhone + "\r\n" + "\r\n" +
                    "Мониторинг: " + strOrgMonName + " тел.: " + strOrgMonTel;

                strGridInfo = strBuildId + "#" + strObjName + "#" + strObjInfo + "#" + strStreetId
                    + "#" + strStreetTypeName + "#" + strStreetName + "#" + strBuildName;
                m_AddMonAlert(strGridInfo);
            }
            else
            {
                strObjInfo = "Код организации мониторинга: " + strAlertOrg + "\r\n" +
                    "Литера объекта: " + strAlertLit;

                strObjName = "Объект в базе данных не найден!";
                strGridInfo = "0#" + strObjName + "#" + strObjInfo + "#0" + "#0" + "#0" + "#0";
                m_AddMonAlert(strGridInfo);
                strBuildId = "-1";   //если объект не найден
            }
            DateTime myDateTime = DateTime.Now;
            string strDate = myDateTime.Month.ToString() + "-" + myDateTime.Day.ToString() + "-" + myDateTime.Year.ToString() + " " +
                                myDateTime.ToString("HH:mm:ss");
            string strSqlTextReportMonObj = "INSERT INTO ReportMonObj " +
                "(RptMonId, RptMonDate, RptMonObjId, RptMonObjLit, RptOrgMonId) " +
                "Values('0', CONVERT(DATETIME, '" + strDate + "', 102), '" + strBuildId + "', '" + strAlertLit + "', '" + strAlertOrg + "')";

            SqlCommand command = new SqlCommand(strSqlTextReportMonObj, FrmMain.Instance.m_cnn);
            Int32 recordsAffected = command.ExecuteNonQuery();
        }

        public void m_AddMonAlert(string strGridInfo)
        {
            string[] strAlertInfo;
            strAlertInfo = strGridInfo.Split('#');

            string strBuildIdObj = strAlertInfo[0];
            string strMonObjName = strAlertInfo[1];
            string strMonObjInfo = strAlertInfo[2];
            string strStreetId = strAlertInfo[3];
            string strStreetTypeName = strAlertInfo[4];
            string strStreetName = strAlertInfo[5];
            string strBuildName = strAlertInfo[6];

            DateTime myDateTime = DateTime.Now;
            string strTime = myDateTime.ToString("HH:mm:ss");

            object[] items = { "1", strBuildIdObj, strTime, strMonObjName, 
                             strStreetId, strStreetTypeName, strStreetName, strBuildName };
            c1FlxGrdObjAlert.AddItem(items, 1, 1);
            // create note
            CellNote note = new CellNote(strMonObjInfo);
            // attach note to column
            CellRange rg = c1FlxGrdObjAlert.GetCellRange(1, 4);
            rg.UserData = note;
            CellNoteManager mgr = new CellNoteManager(c1FlxGrdObjAlert);
            
            c1FlxGrdObjAlert.Refresh();
            c1FlxGrdObjAlert.Select(1, 0);
            c1FlxGrdObjAlert.Focus();
            
            aTimer.Start(); // включает звуковой сигнал
        }

        private void btnAlertPmt_Click(object sender, EventArgs e)
        {
            int iRowSel = c1FlxGrdObjAlert.RowSel;
            if (iRowSel > 0)
            {
                string strObj = c1FlxGrdObjAlert.GetDataDisplay(iRowSel, 4);
                string strMsg = "Объект: " + strObj + "\r\n" + "Записать информацию об объекте?";

                if (MessageBox.Show(strMsg, "Выбор объекта",
                        MessageBoxButtons.YesNo, MessageBoxIcon.Question)
                        == DialogResult.Yes)
                {
                    string strBuildId = c1FlxGrdObjAlert.GetDataDisplay(iRowSel, 2);
                    string strStreetId = c1FlxGrdObjAlert.GetDataDisplay(iRowSel, 5);
                    int itemFound = FrmPermit.Instance.bsStreet.Find("StreetId", strStreetId);
                    FrmPermit.Instance.bsStreet.Position = itemFound;   // записывает позицию для путевки

                    FrmPermit.Instance.textBoxStreetType.Text = c1FlxGrdObjAlert.GetDataDisplay(iRowSel, 6);
                    FrmPermit.Instance.c1ComboFnd.Text = c1FlxGrdObjAlert.GetDataDisplay(iRowSel, 7);
                    FrmPermit.Instance.c1ComboHome.Text = c1FlxGrdObjAlert.GetDataDisplay(iRowSel, 8);
                    FrmPermit.Instance.textBoxMsg.Text = "Автоматизированная система мониторинга";

                    FrmPermit.Instance.textBoxObj.Text = strObj;
                    FrmPermit.Instance.m_TechnicsLoadObj(strBuildId);
                    FrmPermit.Instance.m_strBuildIdMap = strBuildId;    //BuildId объекта для вывода информации в главном окне
                    FrmPermit.Instance.m_blWrHome = true;

                    if (!FrmPermit.Instance.blFrmPmtShow)
                    {
                        FrmPermit.Instance.WindowState = FormWindowState.Normal;
                        FrmPermit.Instance.Show();
                        FrmPermit.Instance.blFrmPmtShow = true;
                    }
                    else
                        FrmPermit.Instance.Activate();

                    FrmPermit.Instance.textBoxFire.Focus();
                }
            }
        }

        private void btnAlertInfo_Click(object sender, EventArgs e)
        {
            int iRowSel = c1FlxGrdObjAlert.RowSel;
            if (iRowSel > 0)
            {
                string strBuildId = c1FlxGrdObjAlert.GetDataDisplay(iRowSel, 2);
                string strObjName = "Объект: " + c1FlxGrdObjAlert.GetDataDisplay(iRowSel, 4);
                FrmObjUkgData.Instance = new FrmObjUkgData();
                FrmObjUkgData.Instance.Text = strObjName;
                FrmObjUkgData.Instance.m_strBuildIdObj = strBuildId;
                FrmObjUkgData.Instance.Show();
            }
        }

        private void btnAlertMap_Click(object sender, EventArgs e)
        {
            int iRowSel = c1FlxGrdObjAlert.RowSel;
            if (iRowSel > 0)
            {
                string strBuildId = c1FlxGrdObjAlert.GetDataDisplay(iRowSel, 2);
                if (strBuildId != "")   // не выводить карту если дома нет в базе данных карты
                    FrmMain.Instance.LoadMapUkg(1, strBuildId);
            }
        }

        private void FrmMonAlert_FormClosed(object sender, FormClosedEventArgs e)
        {
            FrmMain.Instance.m_blFrmMonAlert = false;
            aTimer.Close();
        }

        private void c1FlxGrdObjAlert_AfterEdit(object sender, RowColEventArgs e)
        {
            if (e.Col == 1)
            {
                bool blR = false;
                int iRowsCnt = c1FlxGrdObjAlert.Rows.Count;
                for (int i = 1; i < iRowsCnt; i = i + 1)
                {
                    string rtr = c1FlxGrdObjAlert.GetDataDisplay(i, 1);
                    if (rtr != "")
                        blR = Convert.ToBoolean(rtr);
                }
                if (blR == false)
                {
                    aTimer.Stop();
                    myPlayer.Stop();
                }
                // не дает поставить второй раз крыжик
                bool blChek = Convert.ToBoolean(c1FlxGrdObjAlert.GetDataDisplay(e.Row, 1));
                if (blChek)
                {
                    bool rtr = c1FlxGrdObjAlert.SetData(e.Row, 1, "0", true);
                }
                // записывает время и имя компьютера диспетчера если он ставит крыжик
                string strObjId = c1FlxGrdObjAlert.GetDataDisplay(e.Row, 2);
                if (Convert.ToInt32(strObjId) > 0)
                {
                    RecDispChoice(strObjId);
                }
            }
        }

        private void RecDispChoice(string strObjId)
        {
            string strSqlTextObjMonCheck = "SELECT RptMonId, RptMonDate, RptMonObjId, RptMonDisp " +
                "FROM ReportMonObj WHERE (RptMonObjId = '" + strObjId + "') AND (RptMonDisp = 0) " +
                "ORDER BY RptMonDate DESC";
            DataSet dsMonCheck = new DataSet();
            BindingSource bsMonCheck = new BindingSource();
            SqlDataAdapter daMonCheck = new SqlDataAdapter(strSqlTextObjMonCheck, FrmMain.Instance.m_cnn);
            daMonCheck.Fill(dsMonCheck, "ReportMonObj");
            bsMonCheck.DataSource = dsMonCheck;
            bsMonCheck.DataMember = "ReportMonObj";

            DateTime myDateTime = DateTime.Now;
            string strDate = myDateTime.Month.ToString() + "-" + myDateTime.Day.ToString() + "-" + myDateTime.Year.ToString() + " " +
                                myDateTime.ToString("HH:mm:ss");
            int iCntMonCheck = bsMonCheck.Count;
            string strDispName = FrmMain.Instance.m_strLocalNameComp;
            while (iCntMonCheck > 0)
            {
                DataRow dr = dsMonCheck.Tables["ReportMonObj"].Rows[bsMonCheck.Position];
                string strId = dr["RptMonId"].ToString();
                bool blCh = Convert.ToBoolean(dr["RptMonDisp"].ToString());
                if (!blCh)
                {
                    string strSqlTextUpdate = "UPDATE ReportMonObj SET RptMonDisp = '1', RptMonDispDate = CONVERT(DATETIME, '" + strDate + "', 102), " +
                    "RptMonDispName = '" + strDispName + "' WHERE RptMonId = " + strId;
                    SqlCommand command = new SqlCommand(strSqlTextUpdate, FrmMain.Instance.m_cnn);
                    Int32 recordsAffected = command.ExecuteNonQuery();
                }
                bsMonCheck.MoveNext();
                iCntMonCheck--;
            }
        }

        private void OnTimedEvent(object source, ElapsedEventArgs e)
        {
            myPlayer.Play();
        }
    }
}
