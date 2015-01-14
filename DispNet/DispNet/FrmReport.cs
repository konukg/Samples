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
    public partial class FrmReport : Form
    {
        static public FrmReport Instance;

        //string lstrDataTimeSql = "00-00-00 00:00:00";
        //string lstrDataTimeSqlAft;  //строка с датой для записи в SQL mm-dd-yyyy
        //string lstrDataTimeSqlBef;
        string lstrDataTimeSqlAftMsg = "00-00-00 00:00:00";  //строка с датой в формате dd-mm-yyyy
        string lstrDataTimeSqlBefMsg = "00-00-00 00:00:00";
        public string m_strRptDateId = "0";
        DataSet dsDisp = new DataSet();
        //bool lblCount = false;
        public string m_strDateRpl; //строка даты и времени в длинном формате
        //переменные для измененной даты отчета
        public string m_strDataTimeSqlUp = "00-00-00 00:00:00";  //дата конца периода для SQL формат MM-d-yyyy HH:mm:ss
        public string m_strDataTimeSqlDown = "00-00-00 00:00:00";//дата начала периода для SQL формат MM-d-yyyy HH:mm:ss
        public string m_strDataTimeAft; //дата начала периода формат  d-MM-yyyy HH:mm:ss
        public string m_strDataTimeBef; //дата конца периода формат  d-MM-yyyy HH:mm:ss
        //...

        //DateTime myDateFnd; //дата минус 1 день - начало периода

        int l_CmpDate = 0;  //если меньше нуля то то время меньше 8 часов, если больше нуля то время больше 8 часов
        bool l_blCount = false; // если true то за данный период есть отчет
        
        string lstrDataTimeSqlAft;  //строка с датой начала периода для записи в SQL mm-dd-yyyy
        string lstrDataTimeSqlBef;  //строка с датой конца периода для записи в SQL mm-dd-yyyy

        public FrmReport()
        {
            InitializeComponent();
        }

        private void FrmReport_Load(object sender, EventArgs e)
        {
            DateTime myDateTimeNow, aftDateTime;
            myDateTimeNow = DateTime.Now;
            aftDateTime = DateTime.Parse("8:00:00");    //проверка текущее время больше или меньше 8 часов
            l_CmpDate = DateTime.Compare(myDateTimeNow, aftDateTime);  // 

            if (l_CmpDate < 0)  //время до 8 часов
            {
                DateTime myDateFnd = FrmMain.Instance.m_fnFindDateAft(myDateTimeNow); //дата минус 1 день - начало периода
                DateTime myDateAft = new DateTime(myDateFnd.Year, myDateFnd.Month, myDateFnd.Day);
                m_strDateRpl = "Отчет с " + myDateAft.ToLongDateString() + " 8:00:00 " +
                    " по " + myDateTimeNow.ToLongDateString() + " 8:00:00 ";

                lstrDataTimeSqlAft = myDateAft.Month.ToString() + "-" + myDateAft.Day.ToString() + "-" + myDateAft.Year.ToString() + " 08:00:00";
                lstrDataTimeSqlBef = myDateTimeNow.Month.ToString() + "-" + myDateTimeNow.Day.ToString() + "-" + myDateTimeNow.Year.ToString() + " 08:00:00";
            }

            if (l_CmpDate > 0)  //время после 8 часов
            {
                //DateTime myDateNext = new DateTime(myDateTimeNow.Year, myDateTimeNow.Month, myDateTimeNow.Day + 1);//заменит +1 день на функцию аналогичную m_fnFindDateAft
                DateTime myDateNext = FrmMain.Instance.m_fnFindDateNext(myDateTimeNow); //дата плюс 1 день - конец периода
                m_strDateRpl = "Отчет с " + myDateTimeNow.ToLongDateString() + " 8:00:00 " +
                    " по " + myDateNext.ToLongDateString() + " 8:00:00 ";

                lstrDataTimeSqlAft = myDateTimeNow.Month.ToString() + "-" + myDateTimeNow.Day.ToString() + "-" + myDateTimeNow.Year.ToString() + " 08:00:00";
                lstrDataTimeSqlBef = myDateNext.Month.ToString() + "-" + myDateNext.Day.ToString() + "-" + myDateNext.Year.ToString() + " 08:00:00";
            }
            lbDateTime.Text = m_strDateRpl;

            LoadDataReport();
            LoadDataDispName();
            l_blCount = LoadDataReportName();
            if (!l_blCount)
                btnRptPrint.Enabled = false;
        }

        private void LoadDataReport()
        {
            string strSqlTextDisp = "SELECT RptDispId, RptDate, RptDispMsg, RptRecRpt " +
                             "FROM ReportDisp " +
                             "WHERE(RptDate >= CONVERT(DATETIME, '" + lstrDataTimeSqlAft + "', 102) " +
                             "AND RptDate <= CONVERT(DATETIME, '" + lstrDataTimeSqlBef + "', 102)) " +
                             "ORDER BY RptDate";

            SqlDataAdapter daDisp = new SqlDataAdapter(strSqlTextDisp, FrmMain.Instance.m_cnn);
            daDisp.Fill(dsDisp, "ReportDisp");
            c1FlxGrdRptDay.SetDataBinding(dsDisp, "ReportDisp");

            c1FlxGrdRptDay.Cols[1].Visible = false;
            c1FlxGrdRptDay.Cols[2].Format = "G";
            c1FlxGrdRptDay.Cols[2].Width = 120;
            c1FlxGrdRptDay.Cols[2].Caption = "Дата и время";
            c1FlxGrdRptDay.Cols[2].AllowEditing = false;
            c1FlxGrdRptDay.Cols[3].Width = 250;
            c1FlxGrdRptDay.Cols[3].AllowEditing = false;
            c1FlxGrdRptDay.Cols[3].Caption = "Наименование сообщения";
            c1FlxGrdRptDay.Cols[4].Width = 20;
            c1FlxGrdRptDay.Cols[4].Caption = "Иск.";

            CellStyle cs = c1FlxGrdRptDay.Styles.Add("s1");
            cs.Font = new Font("Arial", 9, FontStyle.Bold);
            cs.TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.CenterCenter;
            c1FlxGrdRptDay.Rows[0].Style = cs;

            int iCntRows = c1FlxGrdRptDay.Rows.Count - 1;
            if (iCntRows > 0)
                btnReport.Enabled = true;
            else
                btnReport.Enabled = false;
        }

        private void LoadDataDispName()
        {
            string strGroup = "50";
            string strSqlTextDispName = "SELECT StaffName, StaffName1, StaffName2 " +
                                "FROM Staff WHERE(UnitId = " + strGroup + ") ORDER BY StaffName";
            SqlCommand cmd = new SqlCommand(strSqlTextDispName, FrmMain.Instance.m_cnn);
            SqlDataReader rdr = cmd.ExecuteReader();
            string strF = "";
            string strI = "";
            string strO = "";
            string strFIO = "";
            while (rdr.Read())
            {
                if (rdr.IsDBNull(0))
                    strF = "";
                else
                    strF = rdr.GetString(0);
                if (rdr.IsDBNull(1))
                    strI = "";
                else
                    strI = rdr.GetString(1);
                if (rdr.IsDBNull(2))
                    strO = "";
                else
                    strO = rdr.GetString(2);

                if (strF != "")
                {
                    strFIO = strF + ";" + strI + ";" + strO;
                    c1CmbNameVice.AddItem(strFIO);
                    c1CmbNameYield.AddItem(strFIO);
                    c1CmbNameTake.AddItem(strFIO);
                }
            }
            rdr.Close();

            c1CmbNameVice.DropDownWidth = 300;
            c1CmbNameVice.Splits[0].DisplayColumns[0].Width = 100;
            c1CmbNameVice.Splits[0].DisplayColumns[1].Width = 80;
            c1CmbNameVice.Splits[0].DisplayColumns[2].Width = 90;
            c1CmbNameVice.Columns[0].Caption = "Фамилия";
            c1CmbNameVice.Columns[1].Caption = "Имя";
            c1CmbNameVice.Columns[2].Caption = "Отчество";

            c1CmbNameYield.DropDownWidth = 300;
            c1CmbNameYield.Splits[0].DisplayColumns[0].Width = 100;
            c1CmbNameYield.Splits[0].DisplayColumns[1].Width = 80;
            c1CmbNameYield.Splits[0].DisplayColumns[2].Width = 90;
            c1CmbNameYield.Columns[0].Caption = "Фамилия";
            c1CmbNameYield.Columns[1].Caption = "Имя";
            c1CmbNameYield.Columns[2].Caption = "Отчество";

            c1CmbNameTake.DropDownWidth = 300;
            c1CmbNameTake.Splits[0].DisplayColumns[0].Width = 100;
            c1CmbNameTake.Splits[0].DisplayColumns[1].Width = 80;
            c1CmbNameTake.Splits[0].DisplayColumns[2].Width = 90;
            c1CmbNameTake.Columns[0].Caption = "Фамилия";
            c1CmbNameTake.Columns[1].Caption = "Имя";
            c1CmbNameTake.Columns[2].Caption = "Отчество";

            strGroup = "67";
            strSqlTextDispName = "SELECT StaffName, StaffName1, StaffName2 " +
                                "FROM Staff WHERE(UnitId = " + strGroup + ") ORDER BY StaffName";
            cmd = new SqlCommand(strSqlTextDispName, FrmMain.Instance.m_cnn);
            rdr = cmd.ExecuteReader();

            while (rdr.Read())
            {
                if (rdr.IsDBNull(0))
                    strF = "";
                else
                    strF = rdr.GetString(0);
                if (rdr.IsDBNull(1))
                    strI = "";
                else
                    strI = rdr.GetString(1);
                if (rdr.IsDBNull(2))
                    strO = "";
                else
                    strO = rdr.GetString(2);

                if (strF != "")
                {
                    strFIO = strF + ";" + strI + ";" + strO;
                    c1CmbNameFind.AddItem(strFIO);
                }
            }
            rdr.Close();

            c1CmbNameFind.DropDownWidth = 300;
            c1CmbNameFind.Splits[0].DisplayColumns[0].Width = 100;
            c1CmbNameFind.Splits[0].DisplayColumns[1].Width = 80;
            c1CmbNameFind.Splits[0].DisplayColumns[2].Width = 90;
            c1CmbNameFind.Columns[0].Caption = "Фамилия";
            c1CmbNameFind.Columns[1].Caption = "Имя";
            c1CmbNameFind.Columns[2].Caption = "Отчество";
        }

        private bool LoadDataReportName()
        {
            string strSqlTextDispName = "SELECT RptFireDateId, RptDateAft, RptDateBef, RptWriteTime, RptNameRPT, " +
                "RptNameFind, RptNameYield, RptNameTake FROM ReportFirePrnDate " +
                "WHERE(RptDateAft = CONVERT(DATETIME, '" + lstrDataTimeSqlAft + "', 102))";
            
            SqlCommand cmd = new SqlCommand(strSqlTextDispName, FrmMain.Instance.m_cnn);
            SqlDataReader rdr = cmd.ExecuteReader();
            bool blRecCount = false;
            if (rdr.HasRows)
            {
                while (rdr.Read())
                {
                    if (!rdr.IsDBNull(0))
                        m_strRptDateId = rdr.GetInt32(0).ToString();
                    if (!rdr.IsDBNull(1))
                        lstrDataTimeSqlAftMsg = rdr.GetDateTime(1).ToString();
                    if (!rdr.IsDBNull(2))
                        lstrDataTimeSqlBefMsg = rdr.GetDateTime(2).ToString();
                    if (rdr.IsDBNull(4))
                        c1CmbNameVice.Text = "";
                    else
                        c1CmbNameVice.Text = rdr.GetString(4);
                    if (rdr.IsDBNull(5))
                        c1CmbNameFind.Text = "";
                    else
                        c1CmbNameFind.Text = rdr.GetString(5);
                    if (rdr.IsDBNull(6))
                        c1CmbNameYield.Text = "";
                    else
                        c1CmbNameYield.Text = rdr.GetString(6);
                    if (rdr.IsDBNull(7))
                        c1CmbNameTake.Text = "";
                    else
                        c1CmbNameTake.Text = rdr.GetString(7);
                }
                blRecCount = true;
            }
            rdr.Close();
            return blRecCount;
        }

        public void m_fn_ReplDateRpt()
        {
            m_strDateRpl = "Отчет с " + m_strDataTimeAft + " по " + m_strDataTimeBef;
            lbDateTime.Text = m_strDateRpl;
            
            int iCntRows = c1FlxGrdRptDay.Rows.Count - 1;
            for (int i = 0; i < iCntRows; i = i + 1)
                c1FlxGrdRptDay.RemoveItem();
            
            string strSqlTextDisp = "SELECT RptDispId, RptDate, RptDispMsg, RptRecRpt " +
                             "FROM ReportDisp " +
                             "WHERE(RptDate >= CONVERT(DATETIME, '" + m_strDataTimeSqlDown + "', 102) " +
                             "AND RptDate <= CONVERT(DATETIME, '" + m_strDataTimeSqlUp + "', 102)) " +
                             "ORDER BY RptDate";
            SqlDataAdapter daDisp = new SqlDataAdapter(strSqlTextDisp, FrmMain.Instance.m_cnn);
            daDisp.Fill(dsDisp, "ReportDisp");
            c1FlxGrdRptDay.SetDataBinding(dsDisp, "ReportDisp");

            c1FlxGrdRptDay.Cols[1].Visible = false;
            c1FlxGrdRptDay.Cols[2].Format = "G";
            c1FlxGrdRptDay.Cols[2].Width = 120;
            c1FlxGrdRptDay.Cols[2].Caption = "Дата и время";
            c1FlxGrdRptDay.Cols[2].AllowEditing = false;
            c1FlxGrdRptDay.Cols[3].Width = 250;
            c1FlxGrdRptDay.Cols[3].AllowEditing = false;
            c1FlxGrdRptDay.Cols[3].Caption = "Наименование сообщения";
            c1FlxGrdRptDay.Cols[4].Width = 20;
            c1FlxGrdRptDay.Cols[4].Caption = "Иск.";

            CellStyle cs = c1FlxGrdRptDay.Styles.Add("s1");
            cs.Font = new Font("Arial", 9, FontStyle.Bold);
            cs.TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.CenterCenter;
            c1FlxGrdRptDay.Rows[0].Style = cs;

            iCntRows = c1FlxGrdRptDay.Rows.Count - 1;
            if (iCntRows > 0)
                btnReport.Enabled = true;
            else
                btnReport.Enabled = false;

            lstrDataTimeSqlAft = m_strDataTimeSqlDown;  //устанавливает дату для формирования отчета
            lstrDataTimeSqlBef = m_strDataTimeSqlUp;
        }

        private void c1FlxGrdRptDay_AfterEdit(object sender, RowColEventArgs e)
        {
            if (e.Col == 4)
            {
                string strStateRec;
                string strRptDispId = c1FlxGrdRptDay.GetDataDisplay(e.Row, 1);
                string strRec = c1FlxGrdRptDay.GetDataDisplay(e.Row, e.Col);
                bool blRec = Convert.ToBoolean(strRec);

                if (blRec)
                    strStateRec = "-1";
                else
                    strStateRec = "0";
                string strSqlTextDrv = "UPDATE ReportDisp SET RptRecRpt = " + strStateRec +
                                                        " WHERE RptDispId = " + strRptDispId;
                SqlCommand command = new SqlCommand(strSqlTextDrv, FrmMain.Instance.m_cnn);
                Int32 recordsAffected = command.ExecuteNonQuery();
            }
        }

        private void RecReportDate( int iSng)
        {
            DateTime myDateTime;
            myDateTime = DateTime.Now;
            string strDateTimeRec = myDateTime.Month.ToString() + "-" + myDateTime.Day.ToString() + "-" + myDateTime.Year.ToString() + " " +
                                    myDateTime.ToString("HH:mm:ss");

            if (iSng == 1)
            {
                string strSqlTextUpdate = "UPDATE ReportFirePrnDate SET RptNameRPT = '" + c1CmbNameVice.Text + "', " +
                            "RptNameFind = '" + c1CmbNameFind.Text + "', RptNameYield = '" + c1CmbNameYield.Text + "', " +
                            "RptNameTake = '" + c1CmbNameTake.Text + "', RptWriteTime = CONVERT(DATETIME, '" + strDateTimeRec + "', 102) " +
                            "WHERE RptFireDateId = " + m_strRptDateId;
                SqlCommand command = new SqlCommand(strSqlTextUpdate, FrmMain.Instance.m_cnn);
                Int32 recordsAffected = command.ExecuteNonQuery();
                RecReportFireDel(m_strRptDateId, iSng);
            }
            else
            {
                string strSqlTextReportName = "INSERT INTO ReportFirePrnDate " +
                "(RptFireDateId, RptDateAft, RptDateBef, RptNameRPT, RptNameFind, RptNameYield, RptNameTake, RptWriteTime) " +
                "Values('0', CONVERT(DATETIME, '" + lstrDataTimeSqlAft + "', 102), CONVERT(DATETIME, '" + lstrDataTimeSqlBef + "', 102), '" + c1CmbNameVice.Text + "', '" + c1CmbNameFind.Text + "'" +
                ", '" + c1CmbNameYield.Text + "', '" + c1CmbNameTake.Text + "', CONVERT(DATETIME, '" + strDateTimeRec + "', 102))";

                SqlCommand command = new SqlCommand(strSqlTextReportName, FrmMain.Instance.m_cnn);
                Int32 recordsAffected = command.ExecuteNonQuery();

                //ищет последнею запись для формирования отчета
                /*string strSqlTextDispName = "SELECT RptFireDateId FROM ReportFirePrnDate ORDER BY RptFireDateId";
                SqlCommand cmd = new SqlCommand(strSqlTextDispName, FrmMain.Instance.m_cnn);
                SqlDataReader rdr = cmd.ExecuteReader();
                if (rdr.HasRows)
                {
                    while (rdr.Read())
                    {
                        if (!rdr.IsDBNull(0))
                            m_strRptDateId = rdr.GetInt32(0).ToString();
                    }
                }
                rdr.Close();*/
                
                DataSet dsFireDate = new DataSet();
                BindingSource bsFireDate = new BindingSource();

                string strSqlTextDispName = "SELECT RptFireDateId FROM ReportFirePrnDate ORDER BY RptFireDateId";
                SqlDataAdapter daFireDate = new SqlDataAdapter(strSqlTextDispName, FrmMain.Instance.m_cnn);
                daFireDate.Fill(dsFireDate, "ReportFirePrnDate");
                bsFireDate.DataSource = dsFireDate;
                bsFireDate.DataMember = "ReportFirePrnDate";
                if (bsFireDate.Count > 0)
                {
                    bsFireDate.MoveLast();
                    DataRow drDate = dsFireDate.Tables["ReportFirePrnDate"].Rows[bsFireDate.Position];
                    m_strRptDateId = drDate["RptFireDateId"].ToString();
                }
                btnRptPrint.Enabled = true;
                RecReportFireDel(m_strRptDateId, iSng);
             }
        }

        private void RecReportFireDel(string strRptFireDateId, int iSng)
        {
            /*string strSqlTextPrnDate = "SELECT RptFireDateId FROM ReportFirePrnDate ORDER BY RptFireDateId";
            SqlCommand cmdPrnDate = new SqlCommand(strSqlTextPrnDate, FrmMain.Instance.m_cnn);
            SqlDataReader rdrPrnDate = cmdPrnDate.ExecuteReader();
            string strRptFireDateId = "0";
            while (rdrPrnDate.Read())
            {
                if (!rdrPrnDate.IsDBNull(0))
                    strRptFireDateId = Convert.ToString(rdrPrnDate.GetInt32(0));
            }
            rdrPrnDate.Close();*/

            //удаляет записи из таб. ReportFirePrn
            if (iSng == 1)
            {
                string strSqlDelFirePrn = "DELETE FROM ReportFirePrn WHERE(RptFireDateId = '" + strRptFireDateId + "')";
                SqlCommand cmdPrn = new SqlCommand(strSqlDelFirePrn, FrmMain.Instance.m_cnn);
                Int32 recordsAffectedPrn = cmdPrn.ExecuteNonQuery();
                //удаляет записи из таб. ReportFirePrnBort
                string strSqlDelFirePrnBort = "DELETE FROM ReportFirePrnBort WHERE(RptFireDateId = '" + strRptFireDateId + "')";
                SqlCommand cmdPrnBort = new SqlCommand(strSqlDelFirePrnBort, FrmMain.Instance.m_cnn);
                Int32 recordsAffectedPrnBort = cmdPrnBort.ExecuteNonQuery();
            }
            
            //ищет номер последней записи в таб. ReportFirePrn
            DataSet dsFirePrn = new DataSet();
            BindingSource bsFirePrn = new BindingSource();

            string strSqlTextFirePrn = "SELECT RptFirePrnId FROM ReportFirePrn ORDER BY RptFirePrnId";
            SqlDataAdapter daFirePrn = new SqlDataAdapter(strSqlTextFirePrn, FrmMain.Instance.m_cnn);
            daFirePrn.Fill(dsFirePrn, "ReportFirePrn");
            bsFirePrn.DataSource = dsFirePrn;
            bsFirePrn.DataMember = "ReportFirePrn";
            int iLastRecFirePrn = 0;
            if (bsFirePrn.Count > 0)
            {
                bsFirePrn.MoveLast();
                DataRow drPrn = dsFirePrn.Tables["ReportFirePrn"].Rows[bsFirePrn.Position];
                iLastRecFirePrn = Convert.ToInt32(drPrn["RptFirePrnId"].ToString());
            }
            iLastRecFirePrn = RecReportFire(1, strRptFireDateId, iLastRecFirePrn);
            iLastRecFirePrn = RecReportFire(2, strRptFireDateId, iLastRecFirePrn);
            iLastRecFirePrn = RecReportTechDrv(3, strRptFireDateId, iLastRecFirePrn);
            iLastRecFirePrn = RecReportTechDrvObl(4, strRptFireDateId, iLastRecFirePrn);
            iLastRecFirePrn = RecReportMenDrv(5, strRptFireDateId, iLastRecFirePrn);
            iLastRecFirePrn = RecReportInfoMsg(6, strRptFireDateId, iLastRecFirePrn);
        }

        private int RecReportFire(int iFireType, string strRptFireDateId, int iLastRecFirePrn)
        {
            //iFireType = 1-пожар город, 2-область

            DataSet dsReport = new DataSet();
            BindingSource bsReport = new BindingSource();
            string strSqlTextReport = "";
            if (iFireType == 1)
            {
                strSqlTextReport = "SELECT ReportDisp.RptDispId, ReportDisp.RptDate, ReportDisp.RptDispMsg, ReportDisp.DepartTypeId, " +
                    "ReportFire.RptRing, ReportFire.RptRingTel, ReportFire.RptMsgFire, ReportFire.RptAreaFire, ReportFire.RptAreaFireInfo, " +
                    "ReportFire.AreaFireId, ReportFire.RptMasterFire, ReportFire.RptEscapePeople, ReportFire.RptEvacuatePeople, " +
                    "ReportFire.RptFireKuz, ReportFire.RptFireGuy, " +
                    "ReportFire.RptFireList, ReportFire.RptFireAffair, ReportFire.RptFireDoc, ReportFire.RptFireVer, ReportFire.RptFireNote, " +
                    "ReportFire.RptFireLoss, ReportFire.RptSign, ReportFire.RptCatFireId, ReportDisp.RptRecRpt, " +
                    "ReportFire.RptTimeBarrel, ReportFire.RptTimeLocal, ReportFire.RptTimeFireSupp " +
                    "FROM ReportDisp INNER JOIN ReportFire ON ReportDisp.RptDispId = ReportFire.RptDispId " +
                    "WHERE (ReportDisp.DepartTypeId = 1) AND (ReportDisp.RptRecRpt = 0) " +
                    "AND (RptDate >= CONVERT(DATETIME, '" + lstrDataTimeSqlAft + "', 102)) " +
                    "AND (RptDate <= CONVERT(DATETIME, '" + lstrDataTimeSqlBef + "', 102)) " +
                    "ORDER BY ReportDisp.RptDate";
            }
            else
            {
                strSqlTextReport = "SELECT ReportDisp.RptDispId, ReportDisp.RptDate, ReportDisp.RptDispMsg, ReportDisp.DepartTypeId, " +
                    "ReportFireObl.RptRing, ReportFireObl.RptMsgFire, ReportFireObl.RptAreaFire, ReportFireObl.RptAreaFireInfo, " +
                    "ReportFireObl.AreaFireId, ReportFireObl.RptMasterFire, ReportFireObl.RptEscapePeople, ReportFireObl.RptEvacuatePeople, " +
                    "ReportFireObl.RptFireDoc, " +
                    "ReportFireObl.RptFireVer, ReportFireObl.RptFireNote, ReportFireObl.RptFireLoss, ReportFireObl.RptFireRTP, " +
                    "ReportFireObl.RptSign, ReportFireObl.RptCatFireId, ReportDisp.RptRecRpt, ReportFireObl.RptTimeBarrel, ReportFireObl.RptTimeLocal, " +
                    "ReportFireObl.RptTimeFireSupp FROM ReportDisp INNER JOIN ReportFireObl ON ReportDisp.RptDispId = ReportFireObl.RptDispId " +
                    "WHERE (ReportDisp.DepartTypeId = 2) AND (ReportDisp.RptRecRpt = 0) " +
                    "AND (RptDate >= CONVERT(DATETIME, '" + lstrDataTimeSqlAft + "', 102)) " +
                    "AND (RptDate <= CONVERT(DATETIME, '" + lstrDataTimeSqlBef + "', 102)) " +
                    "ORDER BY ReportDisp.RptDate";
            }
            SqlDataAdapter daReport = new SqlDataAdapter(strSqlTextReport, FrmMain.Instance.m_cnn);
            daReport.Fill(dsReport, "ReportDisp");
            bsReport.DataSource = dsReport;
            bsReport.DataMember = "ReportDisp";
            int iCntRecReport = bsReport.Count;
            string strRptDispId = "";
            string strRptFirePrnId = "0";
            while (iCntRecReport > 0)
            {
                    DataRow dr = dsReport.Tables["ReportDisp"].Rows[bsReport.Position];
                    strRptDispId = dr["RptDispId"].ToString();
                    string strRptDate = dr["RptDate"].ToString();

                    string strTimeBarrel = dr["RptTimeBarrel"].ToString();
                    string strTimeLocal = dr["RptTimeLocal"].ToString();
                    string strTimeFireSupp = dr["RptTimeFireSupp"].ToString();

                    string strRptRing = dr["RptRing"].ToString();
                    string strRptRingTel = "";
                    if (iFireType == 1)
                    {
                        if (strRptRing != "")
                        {
                            strRptRing = "Сообщил: " + strRptRing;
                            strRptRingTel = dr["RptRingTel"].ToString();
                            if (strRptRingTel != "")
                            {
                                strRptRing = strRptRing + " тел. " + strRptRingTel;
                                //strRptRing = strRptRing + "\r\n" + "тел. " + strRptRingTel;
                            }
                        }
                    }
                    if (iFireType == 2)
                    {
                        if (strRptRing != "")
                        {
                            strRptRing = "Сообщил: " + strRptRing;
                        }
                    }
                    string strRptDispMsg = dr["RptDispMsg"].ToString();
                    string strRptMsgFire = dr["RptMsgFire"].ToString();

                    string strRptAreaFireInfo = dr["RptAreaFireInfo"].ToString();
                    string strRptAreaFire = dr["RptAreaFire"].ToString();
                    string strRptAreaFireMsg = "";  //полный текст площадь пожара
                    if (strRptAreaFire != "")
                    {
                        int iAreaFireId = Convert.ToInt32(dr["AreaFireId"].ToString());
                        strRptAreaFireMsg = "Площадь пожара = " + strRptAreaFire;
                        if (iAreaFireId > 0)
                        {
                            string strSqlAreaFire = "SELECT AreaFireId, AreaFire FROM AreaFireList ORDER BY AreaFireId";
                            SqlCommand cmd = new SqlCommand(strSqlAreaFire, FrmMain.Instance.m_cnn);
                            SqlDataReader rdr = cmd.ExecuteReader();
                            int iAreaFireIdFn = 0;
                            string strAreaFire = "";
                            while (rdr.Read())
                            {
                                if (!rdr.IsDBNull(0))
                                {
                                    iAreaFireIdFn = rdr.GetInt16(0);
                                    if (iAreaFireIdFn == iAreaFireId)
                                    {
                                        if (!rdr.IsDBNull(1))
                                        {
                                            strAreaFire = rdr.GetString(1);
                                            strRptAreaFireMsg = strRptAreaFireMsg + " " + strAreaFire;
                                        }
                                        break;
                                    }
                                }
                            }
                            rdr.Close();
                        }
                    }
                    string strRptEscapePeople = dr["RptEscapePeople"].ToString();
                    if (strRptEscapePeople != "")
                        strRptEscapePeople = "Количество спасенных = " + strRptEscapePeople;
                    string strRptEvacuatePeople = dr["RptEvacuatePeople"].ToString();
                    if (strRptEvacuatePeople != "")
                        strRptEvacuatePeople = "Количество эвакуированных = " + strRptEvacuatePeople;
                    string strRptMasterFire = dr["RptMasterFire"].ToString();  //хозяева
                    
                    //string strRptFireSnup = dr["RptFireSnup"].ToString();  //СНУП
                    //string strRptFireAsr = dr["RptFireAsr"].ToString();  //АСР
                    //string strRptFireFalse = dr["RptFireFalse"].ToString();  //Ложный
                    
                    string strRptFireKuz = "";
                    string strRptFireGuy = "";
                    string strRptFireList = "";
                    string strRptFireAffair = "";
                    string strRptNameRTP = "";
                    if (iFireType == 1) //если городской пожар
                    {
                        strRptFireKuz = dr["RptFireKuz"].ToString();  //КУЗ №
                        if (strRptFireKuz != "")
                            strRptFireKuz = "КУЗ №" + strRptFireKuz;
                        strRptFireGuy = dr["RptFireGuy"].ToString();  //ЖУИ №
                        if (strRptFireGuy != "")
                            strRptFireGuy = "ЖУИ №" + strRptFireGuy;
                        strRptFireList = dr["RptFireList"].ToString();  //Лист №
                        if (strRptFireList != "")
                            strRptFireList = "Лист №" + strRptFireList;
                        strRptFireAffair = dr["RptFireAffair"].ToString();  //Дело №
                        if (strRptFireAffair != "")
                            strRptFireAffair = "Дело №" + strRptFireAffair;
                    }
                    else
                        strRptNameRTP = dr["RptFireRTP"].ToString();  //РТП

                    string strRptFireDoc = dr["RptFireDoc"].ToString();  //Документы о пожаре
                    string strRptFireVer = dr["RptFireVer"].ToString();  //Версия
                    string strRptFireLoss = dr["RptFireLoss"].ToString();  //Ущерб
                    string strRptFireNote = dr["RptFireNote"].ToString();  //Примечание

                    string strCatFireId = dr["RptCatFireId"].ToString(); //Тип пожара 0-нет 1-пожар, 2-АСР, 3-СНУП, 4-ложный
                    int iCatFireId = 0;
                    if (strCatFireId != "")
                        iCatFireId = Convert.ToInt32(strCatFireId);  

                    string strSqlFireDead = "SELECT ReportFireDead.RptDeadName, ReportFireDead.RptDispId " +
                                            "FROM ReportDisp INNER JOIN ReportFireDead ON ReportDisp.RptDispId = ReportFireDead.RptDispId " +
                                            "WHERE (ReportFireDead.RptDispId = '" + strRptDispId + "')";
                    SqlCommand cmdDead = new SqlCommand(strSqlFireDead, FrmMain.Instance.m_cnn);
                    SqlDataReader rdrDead = cmdDead.ExecuteReader();
                    int iCntFireDead = 0;
                    string strFireDead = "";
                    if (rdrDead.HasRows)
                    {
                        while (rdrDead.Read())
                        {
                            if (!rdrDead.IsDBNull(0))
                            {
                                string strFireDeadRd = rdrDead.GetString(0);
                                if (iCntFireDead == 0)
                                    strFireDead = strFireDead + strFireDeadRd;
                                else
                                    strFireDead = strFireDead + "," + strFireDeadRd;
                                iCntFireDead++;
                            }
                        }
                        string strDead = " погибший: ";
                        if (iCntFireDead > 1)
                            strDead = " погибших: ";

                        if (strFireDead != "")
                            strFireDead = iCntFireDead.ToString() + strDead + strFireDead;
                    }
                    rdrDead.Close();

                    string strSqlFireVictim = "SELECT ReportFireVictim.RptVictimName, ReportFireVictim.RptDispId " +
                                        "FROM ReportDisp INNER JOIN ReportFireVictim ON ReportDisp.RptDispId = ReportFireVictim.RptDispId " +
                                        "WHERE (ReportFireVictim.RptDispId = '" + strRptDispId + "')";
                    SqlCommand cmdVictim = new SqlCommand(strSqlFireVictim, FrmMain.Instance.m_cnn);
                    SqlDataReader rdrVictim = cmdVictim.ExecuteReader();
                    int iCntFireVictim = 0;
                    string strFireVictim = "";
                    if (rdrVictim.HasRows)
                    {
                        while (rdrVictim.Read())
                        {
                            if (!rdrVictim.IsDBNull(0))
                            {
                                string strFireVictimRd = rdrVictim.GetString(0);
                                if (iCntFireVictim == 0)
                                    strFireVictim = strFireVictim + strFireVictimRd;
                                else
                                    strFireVictim = strFireVictim + "," + strFireVictimRd;
                                iCntFireVictim++;
                            }
                        }
                        string strVictim = " пострадавший: ";
                        if (iCntFireVictim > 1)
                            strVictim = " пострадавших: ";

                        if (strFireVictim != "")
                            strFireVictim = iCntFireVictim.ToString() + strVictim + strFireVictim;
                    }
                    rdrVictim.Close();

                    string strSqlFireGuilty = "SELECT ReportFireGuilty.RptGuiltyName, ReportFireGuilty.RptDispId " +
                                        "FROM ReportDisp INNER JOIN ReportFireGuilty ON ReportDisp.RptDispId = ReportFireGuilty.RptDispId " +
                                        "WHERE (ReportFireGuilty.RptDispId = '" + strRptDispId + "')";
                    SqlCommand cmdGuilty = new SqlCommand(strSqlFireGuilty, FrmMain.Instance.m_cnn);
                    SqlDataReader rdrGuilty = cmdGuilty.ExecuteReader();
                    int iCntFireGuilty = 0;
                    string strFireGuilty = "";
                    if (rdrGuilty.HasRows)
                    {
                        while (rdrGuilty.Read())
                        {
                            if (!rdrGuilty.IsDBNull(0))
                            {
                                string strFireGuiltyRd = rdrGuilty.GetString(0);
                                if (iCntFireGuilty == 0)
                                    strFireGuilty = strFireGuilty + strFireGuiltyRd;
                                else
                                    strFireGuilty = strFireGuilty + "," + strFireGuiltyRd;
                                iCntFireGuilty++;
                            }
                        }
                        string strGuilty = " виновный: ";
                        if (iCntFireGuilty > 1)
                            strGuilty = " виновных: ";

                        if (strFireGuilty != "")
                            strFireGuilty = iCntFireGuilty.ToString() + strGuilty + strFireGuilty;
                    }
                    rdrGuilty.Close();
                    //формирование столбцов отчета о пожаре
                    string strRptFireData = "";
                    string strRptFireDocPrn;
                    string strRptFireAsrPrn;

                    if (strTimeBarrel != "")
                        strRptDate = strRptDate + "\r\n" + "\r\n" + "Подача ствола" + "\r\n" + strTimeBarrel;
                    if (strTimeLocal != "")
                        strRptDate = strRptDate + "\r\n" + "\r\n" + "Локализация" + "\r\n" + strTimeLocal;
                    if (strTimeFireSupp != "")
                        strRptDate = strRptDate + "\r\n" + "\r\n" + "Ликвидация" + "\r\n" + strTimeFireSupp;

                    strRptFireData = strRptDispMsg;
                    if (strRptRing != "")
                        strRptFireData = strRptFireData + "\r\n" + strRptRing;

                    strRptFireData = strRptFireData + "\r\n" + strRptMsgFire;

                    if (strRptAreaFireMsg != "")
                        strRptFireData = strRptFireData + " " + strRptAreaFireMsg;
                    if (strRptFireVer != "")
                        strRptFireData = strRptFireData + "\r\n" + "Версия причины пожара: " + strRptFireVer;
                    if (strRptMasterFire != "")
                        strRptFireData = strRptFireData + "\r\n" + strRptMasterFire;
                    if (strFireDead != "")
                        strRptFireData = strRptFireData + "\r\n" + strFireDead;
                    if (strFireVictim != "")
                        strRptFireData = strRptFireData + "\r\n" + strFireVictim;
                    if (strFireGuilty != "")
                        strRptFireData = strRptFireData + "\r\n" + strFireGuilty;
                    else
                        strRptFireData = strRptFireData + "\r\n" + "Виновный устанавливается";
                    if (strRptFireLoss != "")
                        strRptFireData = strRptFireData + "\r\n" + strRptFireLoss;
                    else
                        strRptFireData = strRptFireData + "\r\n" + "Ущерб устанавливается";

                    if (iFireType == 2)
                    {
                        if (strRptNameRTP != "")
                            strRptFireData = strRptFireData + "\r\n" + "РТП: " + strRptNameRTP;
                    }
                    
                    if (strRptFireNote != "")
                        strRptFireData = strRptFireData + "\r\n" + strRptFireNote;

                    strRptFireDocPrn = strRptFireDoc;

                    strRptFireAsrPrn = "";
                    switch (iCatFireId)
                    {
                        case 0:
                            strRptFireAsrPrn = "" + "\r\n";
                            break;
                        case 1:
                            strRptFireAsrPrn = "Пожар" + "\r\n";
                            break;
                        case 2:
                            strRptFireAsrPrn = "АСР"+ "\r\n";
                            break;
                        case 3:
                            strRptFireAsrPrn = "СНУП" + "\r\n";
                            break;
                        case 4:
                            strRptFireAsrPrn = "Ложный:" + "\r\n";
                            break;
                    }
                    if (iFireType == 1) //если городской пожар
                    {
                        if (strRptFireKuz != "")
                            strRptFireAsrPrn = strRptFireAsrPrn + strRptFireKuz;
                        if (strRptFireGuy != "")
                            strRptFireAsrPrn = strRptFireAsrPrn + "\r\n" + strRptFireGuy;
                        if (strRptFireList != "")
                            strRptFireAsrPrn = strRptFireAsrPrn + "\r\n" + strRptFireList;
                        if (strRptFireAffair != "")
                            strRptFireAsrPrn = strRptFireAsrPrn + "\r\n" + strRptFireAffair;
                    }
                    iLastRecFirePrn++;
                    strRptFirePrnId = iLastRecFirePrn.ToString();
                    string strSqlTextReportPrn = "INSERT INTO ReportFirePrn " +
                    "(RptFirePrnId, RptFireDateId, RptDateTimeFire, RptFireRing, RptFireData, RptFireDoc, RptFireAsr, " +
                    "RptTimeBarrel, RptTimeLocal, RptTimeFireSupp, RptFireType) " +
                    "Values('" + strRptFirePrnId + "', '" + strRptFireDateId + "', '" + strRptDate + "', " +
                    "'" + strRptRing + "', '" + strRptFireData + "', '" + strRptFireDocPrn + "', '" + strRptFireAsrPrn + "', " +
                    "'" + strTimeBarrel + "', '" + strTimeLocal + "', '" + strTimeFireSupp + "', '" + iFireType.ToString() + "')";
                //"Values('" + strRptFirePrnId + "', '" + strRptFireDateId + "', CONVERT(DATETIME, '" + strRptDate + "', 103), " +

                    SqlCommand command = new SqlCommand(strSqlTextReportPrn, FrmMain.Instance.m_cnn);
                    Int32 recordsAffected = command.ExecuteNonQuery();

                    RecReportFireBort(strRptDispId, strRptFirePrnId, strRptFireDateId, iFireType);
                
                bsReport.MoveNext();
                iCntRecReport--;
            }
            return iLastRecFirePrn;
        }

        private void RecReportFireBort(string strRptDispId, string strRptFirePrnId, string strRptFireDateId, int iFireType)
        {
            DataSet dsReportBort = new DataSet();
            BindingSource bsReportBort = new BindingSource();
            string strSqlFireBort = "";
            if (iFireType == 1 || iFireType == 3)
            {
                strSqlFireBort = "SELECT ReportDisp.RptDispId, Unit.UnitName, TechnicsUnit.TechnicsName, " +
                    "ReportFireBort.BortDrive, ReportFireBort.BortArrive, ReportFireBort.BortReturn " +
                    "FROM ReportDisp INNER JOIN ReportFireBort ON ReportDisp.RptDispId = ReportFireBort.RptDispId " +
                    "INNER JOIN TechnicsUnit ON ReportFireBort.TechnicsId = TechnicsUnit.TechnicsId " +
                    "INNER JOIN Unit ON TechnicsUnit.UnitId = Unit.UnitId " +
                    "WHERE (ReportDisp.RptDispId = '" + strRptDispId + "') " +
                    "AND (RptDate >= CONVERT(DATETIME, '" + lstrDataTimeSqlAft + "', 102)) " +
                    "AND (RptDate <= CONVERT(DATETIME, '" + lstrDataTimeSqlBef + "', 102)) " +
                    "ORDER BY TechnicsUnit.TechnicsName";
            }
            else
            {
                strSqlFireBort = "SELECT ReportDisp.RptDispId, Unit.UnitName, ReportFireBortObl.TechnicsName, " +
                    "ReportFireBortObl.BortDrive, ReportFireBortObl.BortArrive, ReportFireBortObl.BortReturn " +
                    "FROM ReportDisp INNER JOIN ReportFireBortObl ON ReportDisp.RptDispId = ReportFireBortObl.RptDispId " +
                    "INNER JOIN Unit ON ReportFireBortObl.UnitId = Unit.UnitId " +
                    "WHERE (ReportDisp.RptDispId = '" + strRptDispId + "') " +
                    "AND (RptDate >= CONVERT(DATETIME, '" + lstrDataTimeSqlAft + "', 102)) " +
                    "AND (RptDate <= CONVERT(DATETIME, '" + lstrDataTimeSqlBef + "', 102)) " +
                    "ORDER BY ReportFireBortObl.TechnicsName";
            }
            SqlDataAdapter daReportBort = new SqlDataAdapter(strSqlFireBort, FrmMain.Instance.m_cnn);
            daReportBort.Fill(dsReportBort, "ReportDisp");
            bsReportBort.DataSource = dsReportBort;
            bsReportBort.DataMember = "ReportDisp";
            int iCntRecReport = bsReportBort.Count;
            string strFireUnitName = "";
            string strFireTechName = "";
            string strFireBortDrive = "";
            string strFireBortArrive = "";
            string strFireBortReturn = "";
            while (iCntRecReport > 0)
            {
                DataRow dr = dsReportBort.Tables["ReportDisp"].Rows[bsReportBort.Position];
                strFireUnitName = dr["UnitName"].ToString();
                strFireTechName = dr["TechnicsName"].ToString();
                strFireBortDrive = dr["BortDrive"].ToString();
                strFireBortArrive = dr["BortArrive"].ToString();
                strFireBortReturn = dr["BortReturn"].ToString();

                string strSqlTextReportPrn = "INSERT INTO ReportFirePrnBort " +
                    "(RptFireBortPrnId, RptFirePrnId, RptFireDateId, RptFirePchName, RptFireBortName, RptBortDrive, RptBortArrive, RptBortReturn) " +
                    "Values('0', '" + strRptFirePrnId + "', '" + strRptFireDateId + "', '" + strFireUnitName + "', " +
                    "'" + strFireTechName + "', '" + strFireBortDrive + "', '" + strFireBortArrive + "', '" + strFireBortReturn + "')";

                SqlCommand command = new SqlCommand(strSqlTextReportPrn, FrmMain.Instance.m_cnn);
                Int32 recordsAffected = command.ExecuteNonQuery();

                bsReportBort.MoveNext();
                iCntRecReport--;
            }
            
        }

        private int RecReportTechDrv(int iFireType, string strRptFireDateId, int iLastRecFirePrn)
        {
            DataSet dsReportTechDrv = new DataSet();
            BindingSource bsReportTechDrv = new BindingSource();

            string strSqlTechDrv = "SELECT ReportDisp.RptDispId, ReportDisp.RptDate, ReportDisp.RptDispMsg, ReportDisp.DepartTypeId, " +
                "ReportDisp.RptRecRpt, ReportBortDrive.RptBortDrvPlace, ReportBortDrive.RptBortDrvTake " +
                "FROM ReportDisp INNER JOIN ReportBortDrive ON ReportDisp.RptDispId = ReportBortDrive.RptDispId " +
                "WHERE (ReportDisp.DepartTypeId = 3) AND (ReportDisp.RptRecRpt = 0) " +
                "AND (RptDate >= CONVERT(DATETIME, '" + lstrDataTimeSqlAft + "', 102)) " +
                "AND (RptDate <= CONVERT(DATETIME, '" + lstrDataTimeSqlBef + "', 102)) " +
                "ORDER BY ReportDisp.RptDate";
            SqlDataAdapter daReportTechDrv = new SqlDataAdapter(strSqlTechDrv, FrmMain.Instance.m_cnn);
            daReportTechDrv.Fill(dsReportTechDrv, "ReportDisp");
            bsReportTechDrv.DataSource = dsReportTechDrv;
            bsReportTechDrv.DataMember = "ReportDisp";
            int iCntRecReport = bsReportTechDrv.Count;
            string strRptDate = "";
            string strRptDispMsg = "";
            string strDrvPlace = "";
            string strDrvTake = "";
            string strRptFireData = "";
            
            while (iCntRecReport > 0)
            {
                DataRow dr = dsReportTechDrv.Tables["ReportDisp"].Rows[bsReportTechDrv.Position];
                strRptDate = dr["RptDate"].ToString();
                strRptDispMsg = dr["RptDispMsg"].ToString();
                strDrvPlace = dr["RptBortDrvPlace"].ToString();
                strDrvTake = dr["RptBortDrvTake"].ToString();

                strRptFireData = "";
                if (strRptDispMsg != "")
                    strRptFireData = "Выезд техники: " + strRptDispMsg;
                if (strDrvPlace != "" )
                    strRptFireData = strRptFireData + "\r\n" + "Место: " + strDrvPlace;
                if (strDrvTake != "")
                    strRptFireData = strRptFireData + "\r\n" + "Проводит: " + strDrvTake;
                          
                iLastRecFirePrn++;
                string strRptFirePrnId = iLastRecFirePrn.ToString();
                string strSqlTextReportPrn = "INSERT INTO ReportFirePrn " +
                    "(RptFirePrnId, RptFireDateId, RptDateTimeFire, RptFireData, RptFireType) " +
                    "Values('" + strRptFirePrnId + "', '" + strRptFireDateId + "', '" + strRptDate + "', " +
                    "'" + strRptFireData + "', '" + iFireType.ToString() + "')";

                SqlCommand command = new SqlCommand(strSqlTextReportPrn, FrmMain.Instance.m_cnn);
                Int32 recordsAffected = command.ExecuteNonQuery();

                string strRptDispId = dr["RptDispId"].ToString();
                RecReportFireBort(strRptDispId, strRptFirePrnId, strRptFireDateId, iFireType);

                bsReportTechDrv.MoveNext();
                iCntRecReport--;
            }
            return iLastRecFirePrn;
        }

        private int RecReportTechDrvObl(int iFireType, string strRptFireDateId, int iLastRecFirePrn)
        {
            DataSet dsReportTechDrvObl = new DataSet();
            BindingSource bsReportTechDrvObl = new BindingSource();

            string strSqlTechDrvObl = "SELECT ReportDisp.RptDispId, ReportDisp.RptDate, ReportDisp.RptDispMsg, " +
                "ReportDisp.DepartTypeId, ReportDisp.RptRecRpt, ReportBortDriveObl.RptBortDrvOblBort, " +
                "ReportBortDriveObl.RptBortDrvOblPlace, ReportBortDriveObl.RptBortDrvOblTake, Unit.UnitName " +
                "FROM ReportDisp INNER JOIN ReportBortDriveObl ON ReportDisp.RptDispId = ReportBortDriveObl.RptDispId " +
                "INNER JOIN Unit ON ReportBortDriveObl.UnitId = Unit.UnitId " +
                "WHERE (ReportDisp.DepartTypeId = 4) AND (ReportDisp.RptRecRpt = 0) " +
                "AND (RptDate >= CONVERT(DATETIME, '" + lstrDataTimeSqlAft + "', 102)) " +
                "AND (RptDate <= CONVERT(DATETIME, '" + lstrDataTimeSqlBef + "', 102)) " +
                "ORDER BY ReportDisp.RptDate";
            SqlDataAdapter daReportTechDrvObl = new SqlDataAdapter(strSqlTechDrvObl, FrmMain.Instance.m_cnn);
            daReportTechDrvObl.Fill(dsReportTechDrvObl, "ReportDisp");
            bsReportTechDrvObl.DataSource = dsReportTechDrvObl;
            bsReportTechDrvObl.DataMember = "ReportDisp";
            int iCntRecReport = bsReportTechDrvObl.Count;
            string strRptDate = "";
            string strRptDispMsg = "";
            string strRptDispPch = "";
            string strRptDispBort = "";
            string strDrvPlace = "";
            string strDrvTake = "";
            string strRptFireData = "";

            while (iCntRecReport > 0)
            {
                DataRow dr = dsReportTechDrvObl.Tables["ReportDisp"].Rows[bsReportTechDrvObl.Position];
                strRptDate = dr["RptDate"].ToString();
                strRptDispMsg = dr["RptDispMsg"].ToString();
                strRptDispPch = dr["UnitName"].ToString();
                strRptDispBort = dr["RptBortDrvOblBort"].ToString();
                strDrvPlace = dr["RptBortDrvOblPlace"].ToString();
                strDrvTake = dr["RptBortDrvOblTake"].ToString();

                strRptFireData = "";
                if (strRptDispMsg != "")
                    strRptFireData = "Выезд техники: " + strRptDispMsg;
                if (strRptDispPch != "")
                    strRptFireData = strRptFireData + "\r\n" + strRptDispPch;
                if (strRptDispBort != "")
                    strRptFireData = strRptFireData + " Борт: " + strRptDispBort;
                if (strDrvPlace != "")
                    strRptFireData = strRptFireData + "\r\n" + "Место: " + strDrvPlace;
                if (strDrvTake != "")
                    strRptFireData = strRptFireData + "\r\n" + "Проводит: " + strDrvTake;

                iLastRecFirePrn++;
                string strRptFirePrnId = iLastRecFirePrn.ToString();
                string strSqlTextReportPrn = "INSERT INTO ReportFirePrn " +
                    "(RptFirePrnId, RptFireDateId, RptDateTimeFire, RptFireData, RptFireType) " +
                    "Values('" + strRptFirePrnId + "', '" + strRptFireDateId + "', '" + strRptDate + "', " +
                    "'" + strRptFireData + "', '" + iFireType.ToString() + "')";

                SqlCommand command = new SqlCommand(strSqlTextReportPrn, FrmMain.Instance.m_cnn);
                Int32 recordsAffected = command.ExecuteNonQuery();

                bsReportTechDrvObl.MoveNext();
                iCntRecReport--;
            }  
            return iLastRecFirePrn;
        }

        private int RecReportMenDrv(int iFireType, string strRptFireDateId, int iLastRecFirePrn)
        {
            DataSet dsReportMenDrv = new DataSet();
            BindingSource bsReportMenDrv = new BindingSource();

            string strSqlMenDrv = "SELECT ReportDisp.RptDispId, ReportDisp.RptDate, ReportDisp.RptDispMsg, ReportDisp.DepartTypeId, " +
                "ReportDisp.RptRecRpt, ReportMenDrive.RptMenPlace, ReportMenDrive.RptMenTake, ReportMenDrive.RptMenTime " +
                "FROM ReportDisp INNER JOIN ReportMenDrive ON ReportDisp.RptDispId = ReportMenDrive.RptDispId " +
                "WHERE (ReportDisp.DepartTypeId = 5) AND (ReportDisp.RptRecRpt = 0) " +
                "AND (RptDate >= CONVERT(DATETIME, '" + lstrDataTimeSqlAft + "', 102)) " +
                "AND (RptDate <= CONVERT(DATETIME, '" + lstrDataTimeSqlBef + "', 102)) " +
                "ORDER BY ReportDisp.RptDate";
            SqlDataAdapter daReportMenDrv = new SqlDataAdapter(strSqlMenDrv, FrmMain.Instance.m_cnn);
            daReportMenDrv.Fill(dsReportMenDrv, "ReportDisp");
            bsReportMenDrv.DataSource = dsReportMenDrv;
            bsReportMenDrv.DataMember = "ReportDisp";
            int iCntRecReport = bsReportMenDrv.Count;
            string strRptDate = "";
            string strRptDispMsg = "";
            string strRptMenTime = "";
            string strMenPlace = "";
            string strMenTake = "";
            string strRptFireData = "";

            while (iCntRecReport > 0)
            {
                DataRow dr = dsReportMenDrv.Tables["ReportDisp"].Rows[bsReportMenDrv.Position];
                strRptDate = dr["RptDate"].ToString();
                strRptDispMsg = dr["RptDispMsg"].ToString();
                strMenPlace = dr["RptMenPlace"].ToString();
                strMenTake = dr["RptMenTake"].ToString();
                strRptMenTime = dr["RptMenTime"].ToString();

                strRptFireData = "";
                if (strRptDispMsg != "")
                    strRptFireData = "Выезд без техники: " + strRptDispMsg;
                if (strMenPlace != "")
                    strRptFireData = strRptFireData + "\r\n" + "Место: " + strMenPlace;
                if (strMenTake != "")
                    strRptFireData = strRptFireData + "\r\n" + "Проводит: " + strMenTake;
                if (strRptMenTime != "")
                    strRptFireData = strRptFireData + "\r\n" + "Окончание: " + strRptMenTime;

                iLastRecFirePrn++;
                string strRptFirePrnId = iLastRecFirePrn.ToString();
                string strSqlTextReportPrn = "INSERT INTO ReportFirePrn " +
                    "(RptFirePrnId, RptFireDateId, RptDateTimeFire, RptFireData, RptFireType) " +
                    "Values('" + strRptFirePrnId + "', '" + strRptFireDateId + "', '" + strRptDate + "', " +
                    "'" + strRptFireData + "', '" + iFireType.ToString() + "')";

                SqlCommand command = new SqlCommand(strSqlTextReportPrn, FrmMain.Instance.m_cnn);
                Int32 recordsAffected = command.ExecuteNonQuery();

                bsReportMenDrv.MoveNext();
                iCntRecReport--;
            }
            return iLastRecFirePrn;
        }

        private int RecReportInfoMsg(int iFireType, string strRptFireDateId, int iLastRecFirePrn)
        {
            DataSet dsReportInfoMsg = new DataSet();
            BindingSource bsReportInfoMsg = new BindingSource();

            string strSqlInfoMsg = "SELECT ReportDisp.RptDispId, ReportDisp.RptDate, ReportDisp.RptDispMsg, ReportDisp.DepartTypeId, " +
                "ReportDisp.RptRecRpt, ReportMsgService.RptMsgSrvPass, ReportMsgService.RptMsgSrvText, DispMsgService.MsgServiceName " +
                "FROM ReportDisp INNER JOIN ReportMsgService ON ReportDisp.RptDispId = ReportMsgService.RptDispId " +
                "INNER JOIN DispMsgService ON ReportMsgService.MsgServiceId = DispMsgService.MsgServiceId " +
                "WHERE (ReportDisp.DepartTypeId = 6) AND (ReportDisp.RptRecRpt = 0) " +
                "AND (RptDate >= CONVERT(DATETIME, '" + lstrDataTimeSqlAft + "', 102)) " +
                "AND (RptDate <= CONVERT(DATETIME, '" + lstrDataTimeSqlBef + "', 102)) " +
                "ORDER BY ReportDisp.RptDate";
            SqlDataAdapter daReportInfoMsg = new SqlDataAdapter(strSqlInfoMsg, FrmMain.Instance.m_cnn);
            daReportInfoMsg.Fill(dsReportInfoMsg, "ReportDisp");
            bsReportInfoMsg.DataSource = dsReportInfoMsg;
            bsReportInfoMsg.DataMember = "ReportDisp";
            int iCntRecReport = bsReportInfoMsg.Count;
            string strRptDate = "";
            string strRptDispMsg = "";
            string strSrvText = "";
            string strSrvPass = "";
            string strRptFireData = "";

            while (iCntRecReport > 0)
            {
                DataRow dr = dsReportInfoMsg.Tables["ReportDisp"].Rows[bsReportInfoMsg.Position];
                strRptDate = dr["RptDate"].ToString();
                strRptDispMsg = dr["MsgServiceName"].ToString();
                strSrvText = dr["RptMsgSrvText"].ToString();
                strSrvPass = dr["RptMsgSrvPass"].ToString();

                strRptFireData = "";
                if (strRptDispMsg != "")
                    strRptFireData = "Информация: " + strRptDispMsg;
                if (strSrvText != "")
                    strRptFireData = strRptFireData + "\r\n" + "Сообщение: " + strSrvText;
                if (strSrvPass != "")
                    strRptFireData = strRptFireData + "\r\n" + "Передал: " + strSrvPass;
                
                iLastRecFirePrn++;
                string strRptFirePrnId = iLastRecFirePrn.ToString();
                string strSqlTextReportPrn = "INSERT INTO ReportFirePrn " +
                    "(RptFirePrnId, RptFireDateId, RptDateTimeFire, RptFireData, RptFireType) " +
                    "Values('" + strRptFirePrnId + "', '" + strRptFireDateId + "', '" + strRptDate + "', " +
                    "'" + strRptFireData + "', '" + iFireType.ToString() + "')";

                SqlCommand command = new SqlCommand(strSqlTextReportPrn, FrmMain.Instance.m_cnn);
                Int32 recordsAffected = command.ExecuteNonQuery();

                bsReportInfoMsg.MoveNext();
                iCntRecReport--;
            }
            return iLastRecFirePrn;
        }

        private void btnReport_Click(object sender, EventArgs e)
        {
            bool blFillText = true;
            if (c1CmbNameVice.Text == "" && blFillText == true)
            {
                blFillText = false;
                MessageBox.Show("Введите данные в поле Зам РТП!", "Ввод данных",
                   MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }

            if (c1CmbNameFind.Text == "" && blFillText == true)
            {
                blFillText = false;
                MessageBox.Show("Введите данные в поле Дознаватель!", "Ввод данных",
                   MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }

            if (c1CmbNameYield.Text == "" && blFillText == true)
            {
                blFillText = false;
                MessageBox.Show("Введите данные в поле Смену сдал!", "Ввод данных",
                   MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }

            if (c1CmbNameTake.Text == "" && blFillText == true)
            {
                blFillText = false;
                MessageBox.Show("Введите данные в поле Смену принял!", "Ввод данных",
                   MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }

            if (blFillText)
            {
                if (l_blCount)
                {
                    string MsgShow = "Найден отчет с " + lstrDataTimeSqlAftMsg + " по " + lstrDataTimeSqlBefMsg + "\r\n" +
                        "Создать новый отчет?";
                    if (MessageBox.Show(MsgShow, "Запись отчета за сутки",
                            MessageBoxButtons.YesNo, MessageBoxIcon.Question)
                            == DialogResult.Yes)
                    {
                        RecReportDate(1);
                    }
                }
                else
                {
                    if (MessageBox.Show("Создать новый отчет?", "Запись отчета за сутки",
                            MessageBoxButtons.YesNo, MessageBoxIcon.Question)
                            == DialogResult.Yes)
                    {
                        RecReportDate(0);
                    }
                }
            }
        }

        private void btnRptPrint_Click(object sender, EventArgs e)
        {
            FrmReportMain.Instance = new FrmReportMain();
            //FrmReportMain.Instance.m_LoadNewDoc(m_strRptDateId, 1, "За сутки");
            string[] strDate;
            strDate = lstrDataTimeSqlAftMsg.Split(' ');   // выделяет только дату
            string strRptDateCap = strDate[0];
            strRptDateCap = "За сутки " + strRptDateCap;
            FrmReportMain.Instance.m_LoadNewDoc(m_strRptDateId, 1, strRptDateCap);

            if (FrmReportMain.Instance.WindowState == FormWindowState.Minimized)
                FrmReportMain.Instance.WindowState = FormWindowState.Normal;
            else
                FrmReportMain.Instance.Show();
            //RecReportFireDel();

        }

        private void c1CmbNameVice_Close(object sender, EventArgs e)
        {
            int iCnt = c1CmbNameVice.SelectedIndex;
            c1CmbNameVice.Text = c1CmbNameVice.Columns[0].CellText(iCnt) + " " + c1CmbNameVice.Columns[1].CellText(iCnt)
                            + " " + c1CmbNameVice.Columns[2].CellText(iCnt);
            c1CmbNameFind.Focus();
        }

        private void c1CmbNameFind_Close(object sender, EventArgs e)
        {
            int iCnt = c1CmbNameFind.SelectedIndex;
            c1CmbNameFind.Text = c1CmbNameFind.Columns[0].CellText(iCnt) + " " + c1CmbNameFind.Columns[1].CellText(iCnt)
                            + " " + c1CmbNameFind.Columns[2].CellText(iCnt);
            c1CmbNameYield.Focus();
        }

        private void c1CmbNameYield_Close(object sender, EventArgs e)
        {
            int iCnt = c1CmbNameYield.SelectedIndex;
            c1CmbNameYield.Text = c1CmbNameYield.Columns[0].CellText(iCnt) + " " + c1CmbNameYield.Columns[1].CellText(iCnt)
                            + " " + c1CmbNameYield.Columns[2].CellText(iCnt);
            c1CmbNameTake.Focus();
        }

        private void c1CmbNameTake_Close(object sender, EventArgs e)
        {
            int iCnt = c1CmbNameTake.SelectedIndex;
            c1CmbNameTake.Text = c1CmbNameTake.Columns[0].CellText(iCnt) + " " + c1CmbNameTake.Columns[1].CellText(iCnt)
                                    + " " + c1CmbNameTake.Columns[2].CellText(iCnt);
            btnReport.Focus();
        }

        private void btnRplDate_Click(object sender, EventArgs e)
        {
            FrmDateRpl.Instance = new FrmDateRpl();
            FrmDateRpl.Instance.m_iFrmLoad = 2;
            FrmDateRpl.Instance.ShowDialog();
        }

        
    }
}