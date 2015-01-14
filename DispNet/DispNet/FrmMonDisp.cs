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
    public partial class FrmMonDisp : Form
    {
        static public FrmMonDisp Instance;

        public FrmMonDisp()
        {
            InitializeComponent();
        }

        private void FrmMonDisp_Load(object sender, EventArgs e)
        {
            DateTime myDateBef, myDateFnd;
            myDateBef = DateTime.Now;
            string strDataTimeSqlUp = myDateBef.ToLocalTime().ToString("MM-d-yyyy HH:mm:ss");
            c1DateRptBef.Text = myDateBef.ToLocalTime().ToString("d-MM-yyyy HH:mm:ss");

            myDateFnd = FrmMain.Instance.m_fnFindDateAft(myDateBef);
            DateTime myDateAft = new DateTime(myDateFnd.Year, myDateFnd.Month, myDateFnd.Day);

            DateTime theTime = new DateTime(DateTime.Today.Year, DateTime.Today.Month, DateTime.Today.Day, 8, 0, 0);
            int iCmp = theTime.CompareTo(myDateBef);
            string strDataTimeSqlDown = "";
            if (iCmp == -1)
            {
                strDataTimeSqlDown = theTime.ToLocalTime().ToString("MM-d-yyyy 08:00:00");
                c1DateRptAft.Text = theTime.ToLocalTime().ToString("d-MM-yyyy 08:00:00");
            }
            else
            {
                strDataTimeSqlDown = myDateAft.ToLocalTime().ToString("MM-d-yyyy 08:00:00");
                c1DateRptAft.Text = myDateAft.ToLocalTime().ToString("d-MM-yyyy 08:00:00"); ;
            }

            string strSqlData = "WHERE(ReportMonObj.RptMonDate >= CONVERT(DATETIME, '" + strDataTimeSqlDown + "', 102) " +
                                 "AND ReportMonObj.RptMonDate <= CONVERT(DATETIME, '" + strDataTimeSqlUp + "', 102)) ";

            FndRptMonAlert(strSqlData);

        }

        private void FndRptMonAlert(string strSqlTextObjMonFnd)
        {
            bool redraw = c1FlxGrdMonAlt.Redraw;
            c1FlxGrdMonAlt.Redraw = false;
            try
            {
                int iCnt = c1FlxGrdMonAlt.Rows.Count - 1;
                for (int i = 0; i < iCnt; i = i + 1)
                    c1FlxGrdMonAlt.RemoveItem(1);

                string strSqlTextObjMon = "SELECT ObjUkg.ObjId, ObjUkg.BuildId, ObjUkg.ObjName, ObjUkg.ObjTel, ObjUkg.LitObjMon, " +
                    "ObjUkgStaff.OrgName, ObjUkgStaff.DirName, ObjUkgStaff.DirPhone, ObjUkgStaff.DispPhone, " +
                    "Buildings.BuildName, BuildingType.BuildTypeName, StreetType.StreetTypeName, Street.StreetName, Unit.UnitName, " +
                    "OrgObjMon.OrgMonName, OrgObjMon.OrgMonTel, Street.StreetId, OrgObjMon.OrgObjMonId, ReportMonObj.RptMonId, " +
                    "ReportMonObj.RptMonDate, ReportMonObj.RptMonDispDate, ReportMonObj.RptMonDispName, ReportMonObj.RptMonObjId " +
                    "FROM ObjUkg INNER JOIN " +
                          "ReportMonObj ON ObjUkg.BuildId = ReportMonObj.RptMonObjId INNER JOIN " +
                          "ObjUkgStaff ON ObjUkg.ObjStaffId = ObjUkgStaff.ObjStaffId INNER JOIN " +
                          "Buildings ON ObjUkg.BuildId = Buildings.BuildId INNER JOIN " +
                          "BuildingType ON Buildings.BuildTypeId = BuildingType.BuildTypeId INNER JOIN " +
                          "Street ON Buildings.StreetId = Street.StreetId INNER JOIN " +
                          "StreetType ON Street.StreetTypeId = StreetType.StreetTypeId INNER JOIN " +
                          "Unit ON ObjUkg.UnitId = Unit.UnitId INNER JOIN " +
                          "OrgObjMon ON ObjUkg.OrgObjMonId = OrgObjMon.OrgObjMonId " + strSqlTextObjMonFnd +
                    "ORDER BY ReportMonObj.RptMonDate DESC";

                SqlCommand cmdObjMon = new SqlCommand(strSqlTextObjMon, FrmMain.Instance.m_cnn);
                SqlDataReader rdrObjMon = cmdObjMon.ExecuteReader();
                string strObjId = "";
                string strBuildId = "";
                string strObjName = "";
                string strObjTel = "";
                string strLitObjMon = "";
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
                string strRptMonDate = "";
                string strRptMonDispDate = "";
                string strRptMonDispName = "";
                string strRptMonTime = "";
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
                    if (!rdrObjMon.IsDBNull(4))
                        strLitObjMon = rdrObjMon.GetInt32(4).ToString();
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
                    if (!rdrObjMon.IsDBNull(19))
                    {
                        strRptMonDate = rdrObjMon.GetDateTime(19).ToString("dd.MM.yyyy");
                        strRptMonTime = rdrObjMon.GetDateTime(19).ToString("HH:mm:ss");
                    }
                    if (!rdrObjMon.IsDBNull(20))
                        strRptMonDispDate = rdrObjMon.GetDateTime(20).ToString("HH:mm:ss");
                    if (!rdrObjMon.IsDBNull(21))
                        strRptMonDispName = rdrObjMon.GetString(21);

                    string strObjInfo = "";
                    string strGridInfo = "";

                    strObjInfo = "Дата: " + strRptMonDate + " Принял: " + strRptMonDispName + " в: " + strRptMonDispDate + "\r\n" +
                        "Адрес: " + strStreetTypeName + " " + strStreetName + " дом: " + strBuildName + "\r\n" +
                        "тел.: " + strObjTel + " (" + strBuildTypeName + ")" + "\r\n" +
                        "п\\часть: " + strUnitName + "\r\n" +
                        "Организация: " + strOrgName + "\r\n" + strDirName + " тел.: " + strDirPhone + "\r\n" +
                        "Мониторинг: " + strOrgMonName + " тел.: " + strOrgMonTel;

                    strGridInfo = strBuildId + "#" + strObjName + "#" + strObjInfo + "#" + strStreetId
                        + "#" + strStreetTypeName + "#" + strStreetName + "#" + strBuildName + "#" + strRptMonDate + " " + strRptMonTime;
                    RptMonAlert(strGridInfo);

                    iCntFndObj++;
                }
                rdrObjMon.Close();
            }
            finally
            {
                // Always restore painting.
                c1FlxGrdMonAlt.Redraw = redraw;
            }
        }

        private void FndRptLossAlert(string strSqlTextObjMonFnd)
        {
            bool redraw = c1FlxGrdMonAlt.Redraw;
            c1FlxGrdMonAlt.Redraw = false;
            try
            {
                int iCnt = c1FlxGrdMonAlt.Rows.Count - 1;
                for (int i = 0; i < iCnt; i = i + 1)
                    c1FlxGrdMonAlt.RemoveItem(1);

                string strSqlTextObjMon = "SELECT RptMonId, RptMonDate, RptMonObjId, RptMonObjLit, RptOrgMonId " +
                    "FROM ReportMonObj WHERE ((RptMonObjId = - 1) AND " + strSqlTextObjMonFnd +
                    "ORDER BY RptMonDate";

                SqlCommand cmdObjMon = new SqlCommand(strSqlTextObjMon, FrmMain.Instance.m_cnn);
                SqlDataReader rdrObjMon = cmdObjMon.ExecuteReader();
                string strRptMonDate = "";
                string strObjMonId = "";
                string strLitObjMon = "";
                string strOrgMonId = "";

                while (rdrObjMon.Read())
                {
                    if (!rdrObjMon.IsDBNull(1))
                        strRptMonDate = rdrObjMon.GetDateTime(1).ToString("dd.MM.yyyy HH:mm:ss");
                    if (!rdrObjMon.IsDBNull(2))
                        strObjMonId = rdrObjMon.GetInt32(2).ToString();
                    if (!rdrObjMon.IsDBNull(3))
                        strLitObjMon = rdrObjMon.GetInt32(3).ToString();
                    if (!rdrObjMon.IsDBNull(4))
                        strOrgMonId = rdrObjMon.GetInt16(4).ToString();
                    string strObjInfo = "";
                    string strGridInfo = "";
                    strObjInfo = "Код организации мониторинга: " + strOrgMonId + "\r\n" +
                        "Литера объекта: " + strLitObjMon;

                    string strObjName = "Объект в базе данных не найден!";
                    strGridInfo = "0#" + strObjName + "#" + strObjInfo + "#0" + "#0" + "#0" + "#0" + "#" + strRptMonDate;
                    RptMonAlert(strGridInfo);
                }
                rdrObjMon.Close();
            }
            finally
            {
                // Always restore painting.
                c1FlxGrdMonAlt.Redraw = redraw;
            }
        }

        private void RptMonAlert(string strGridInfo)
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
            string strRptMonTime = strAlertInfo[7];

            object[] items = { strBuildIdObj, strRptMonTime, strMonObjName, 
                             strStreetId, strStreetTypeName, strStreetName, strBuildName };

            c1FlxGrdMonAlt.AddItem(items, 1, 1);
            // create note
            CellNote note = new CellNote(strMonObjInfo);
            // attach note to column
            CellRange rg = c1FlxGrdMonAlt.GetCellRange(1, 3);
            rg.UserData = note;
            CellNoteManager mgr = new CellNoteManager(c1FlxGrdMonAlt);

            //c1FlxGrdMonAlt.Refresh();
            c1FlxGrdMonAlt.Select(1, 0);
            c1FlxGrdMonAlt.Focus();
        }

        private void btnDateCh_Click(object sender, EventArgs e)
        {
            string strDataTimeSqlDown = null;
            string strDataTimeSqlUp = null;

            DateTime theTimeDown;
            theTimeDown = Convert.ToDateTime(c1DateRptAft.Text);
            strDataTimeSqlDown = theTimeDown.ToString("MM-d-yyyy HH:mm:ss");

            DateTime theTimeUp;
            theTimeUp = Convert.ToDateTime(c1DateRptBef.Text);
            strDataTimeSqlUp = theTimeUp.ToString("MM-d-yyyy HH:mm:ss");

            if (chkBoxMonErr.Checked)
            {
                string strSqlData = "ReportMonObj.RptMonDate >= CONVERT(DATETIME, '" + strDataTimeSqlDown + "', 102) " +
                        "AND ReportMonObj.RptMonDate <= CONVERT(DATETIME, '" + strDataTimeSqlUp + "', 102)) ";
                FndRptLossAlert(strSqlData);
            }
            else
            {
                string strSqlData = "WHERE(ReportMonObj.RptMonDate >= CONVERT(DATETIME, '" + strDataTimeSqlDown + "', 102) " +
                                 "AND ReportMonObj.RptMonDate <= CONVERT(DATETIME, '" + strDataTimeSqlUp + "', 102)) ";
                FndRptMonAlert(strSqlData);
            }
        }

        private void chkBoxMonErr_CheckedChanged(object sender, EventArgs e)
        {
            if (chkBoxMonErr.Checked)
            {
                btnAlertPmt.Enabled = false;
                btnAlertInfo.Enabled = false;
                btnAlertMap.Enabled = false;
            }
            else
            {
                btnAlertPmt.Enabled = true;
                btnAlertInfo.Enabled = true;
                btnAlertMap.Enabled = true;
            }
        }

        private void btnAlertPmt_Click(object sender, EventArgs e)
        {
            int iRowSel = c1FlxGrdMonAlt.RowSel;
            if (iRowSel > 0)
            {
                string strObj = c1FlxGrdMonAlt.GetDataDisplay(iRowSel, 3);
                string strMsg = "Объект: " + strObj + "\r\n" + "Записать информацию об объекте?";

                if (MessageBox.Show(strMsg, "Выбор объекта",
                        MessageBoxButtons.YesNo, MessageBoxIcon.Question)
                        == DialogResult.Yes)
                {
                    string strBuildId = c1FlxGrdMonAlt.GetDataDisplay(iRowSel, 1);
                    string strStreetId = c1FlxGrdMonAlt.GetDataDisplay(iRowSel, 4);
                    int itemFound = FrmPermit.Instance.bsStreet.Find("StreetId", strStreetId);
                    FrmPermit.Instance.bsStreet.Position = itemFound;   // записывает позицию для путевки

                    FrmPermit.Instance.textBoxStreetType.Text = c1FlxGrdMonAlt.GetDataDisplay(iRowSel, 5);
                    FrmPermit.Instance.c1ComboFnd.Text = c1FlxGrdMonAlt.GetDataDisplay(iRowSel, 6);
                    FrmPermit.Instance.c1ComboHome.Text = c1FlxGrdMonAlt.GetDataDisplay(iRowSel, 7);
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
            int iRowSel = c1FlxGrdMonAlt.RowSel;
            if (iRowSel > 0)
            {
                string strBuildId = c1FlxGrdMonAlt.GetDataDisplay(iRowSel, 1);
                string strObjName = "Объект: " + c1FlxGrdMonAlt.GetDataDisplay(iRowSel, 3);
                FrmObjUkgData.Instance = new FrmObjUkgData();
                FrmObjUkgData.Instance.Text = strObjName;
                FrmObjUkgData.Instance.m_strBuildIdObj = strBuildId;
                FrmObjUkgData.Instance.Show();
            }
        }

        private void btnAlertMap_Click(object sender, EventArgs e)
        {
            int iRowSel = c1FlxGrdMonAlt.RowSel;
            if (iRowSel > 0)
            {
                string strBuildId = c1FlxGrdMonAlt.GetDataDisplay(iRowSel, 1);
                if (strBuildId != "")   // не выводить карту если дома нет в базе данных карты
                    FrmMain.Instance.LoadMapUkg(1, strBuildId);
            }
        }

    }
}
