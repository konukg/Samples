using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace DispNet
{
    public partial class FrmReportInfoDrv : Form
    {
        static public FrmReportInfoDrv Instance;
        
        string lstrDataTimeBef;
        string lstrDataTimeAft;
        public string m_strDataTimeBefRpl;
        public string m_strDataTimeAftRpl;

        public FrmReportInfoDrv()
        {
            InitializeComponent();
        }

        private void FrmReportInfoDrv_Load(object sender, EventArgs e)
        {
            DateTime myDateTime, myDateFnd;
            myDateTime = DateTime.Now;
            lstrDataTimeBef = myDateTime.Day.ToString() + "-" + myDateTime.Month.ToString() + "-" + myDateTime.Year.ToString();
            //int iDay_1 = myDateTime.Day - 1;
            myDateFnd = FrmMain.Instance.m_fnFindDateAft(myDateTime);
            lstrDataTimeAft = myDateFnd.Day.ToString() + "-" + myDateFnd.Month.ToString() + "-" + myDateFnd.Year.ToString();

            rdBtn0_6.Text = lstrDataTimeBef + " 00:00:00 - " + lstrDataTimeBef + " 06:00:00";
            rdBtn6_0.Text = lstrDataTimeAft + " 06:00:00 - " + lstrDataTimeBef + " 00:00:00";
            rdBtn0_0.Text = lstrDataTimeAft + " 00:00:00 - " + lstrDataTimeBef + " 00:00:00";
            rdBtn8_8.Text = lstrDataTimeAft + " 08:00:00 - " + lstrDataTimeBef + " 08:00:00";

            DataSet dsMsgOrgRec = new DataSet();
            BindingSource bsMsgOrgRec = new BindingSource();
            string strSqlTextMsgOrgRec = "SELECT MsgServiceName FROM DispMsgService ORDER BY MsgServiceName";
            SqlDataAdapter daMsgOrgRec = new SqlDataAdapter(strSqlTextMsgOrgRec, FrmMain.Instance.m_cnn);
            daMsgOrgRec.Fill(dsMsgOrgRec, "DispMsgService");
            bsMsgOrgRec.DataSource = dsMsgOrgRec;
            bsMsgOrgRec.DataMember = "DispMsgService";
            c1CmbOrgRec.DataSource = bsMsgOrgRec;
            c1CmbOrgRec.ValueMember = "MsgServiceName";
            c1CmbOrgRec.DisplayMember = "MsgServiceName";
            c1CmbOrgRec.Columns[0].Caption = "Наименование С/Службы";
            c1CmbOrgRec.Splits[0].DisplayColumns[0].HeadingStyle.HorizontalAlignment = C1.Win.C1List.AlignHorzEnum.Center;

            lstrDataTimeBef = lstrDataTimeBef + " 06:00:00";
        }

        private void btnRptShow_Click(object sender, EventArgs e)
        {

                       
            
        }

        private void CountDataDisp()
        {
            // считаем в городе пожары, АСР, СНУП, Ложный, спасенных, эвакуированных
            string strDataTime = "05-20-2008 08:00:00";
            string strSqlTextDispFire = "SELECT ReportDisp.RptDispId, " +
                "ReportFire.RptEscapePeople, ReportFire.RptEvacuatePeople, ReportFire.RptSign " +
                "FROM ReportDisp INNER JOIN ReportFire ON ReportDisp.RptDispId = ReportFire.RptDispId " +
                "WHERE (ReportDisp.RptRecRpt = 0) AND (RptDate <= CONVERT(DATETIME, '" + strDataTime + "', 102)) ORDER BY RptDispId";
            SqlCommand cmdDispFire = new SqlCommand(strSqlTextDispFire, FrmMain.Instance.m_cnn);
            SqlDataReader rdrDispFire = cmdDispFire.ExecuteReader();
            int iCntFire = 0;
            int iCntEscape = 0;
            int iCntEvacuate = 0;
            int iCntAsr = 0;
            int iCntSnup = 0;
            int iCntFalse = 0;
            while (rdrDispFire.Read())
            {
                if (!rdrDispFire.IsDBNull(1))
                    iCntEscape = iCntEscape + rdrDispFire.GetInt16(1);
                if (!rdrDispFire.IsDBNull(2))
                    iCntEvacuate = iCntEvacuate + rdrDispFire.GetInt16(2);
                if (!rdrDispFire.IsDBNull(3))
                {
                    switch (rdrDispFire.GetInt16(3))
                    {
                        case 1:
                            iCntAsr++;
                            break;
                        case 2:
                            iCntSnup++;
                            break;
                        case 3:
                            iCntFalse++;
                            break;
                    }
                }
                iCntFire++;
            }
            rdrDispFire.Close();
            // считаем в области пожары, АСР, СНУП, Ложный, спасенных, эвакуированных
            string strSqlTextDispFireObl = "SELECT ReportDisp.RptDispId, ReportFireObl.RptEscapePeople, ReportFireObl.RptEvacuatePeople, ReportFireObl.RptSign " +
                "FROM ReportDisp INNER JOIN ReportFireObl ON ReportDisp.RptDispId = ReportFireObl.RptDispId " +
                "WHERE (ReportDisp.RptRecRpt = 0) AND (RptDate <= CONVERT(DATETIME, '" + strDataTime + "', 102)) ORDER BY RptDispId";
            SqlCommand cmdDispFireObl = new SqlCommand(strSqlTextDispFireObl, FrmMain.Instance.m_cnn);
            SqlDataReader rdrDispFireObl = cmdDispFireObl.ExecuteReader();
            int iCntFireObl = 0;
            int iCntEscapeObl = 0;
            int iCntEvacuateObl = 0;
            int iCntAsrObl = 0;
            int iCntSnupObl = 0;
            int iCntFalseObl = 0;
            while (rdrDispFireObl.Read())
            {
                if (!rdrDispFireObl.IsDBNull(1))
                    iCntEscapeObl = iCntEscapeObl + rdrDispFireObl.GetInt16(1);
                if (!rdrDispFireObl.IsDBNull(2))
                    iCntEvacuateObl = iCntEvacuateObl + rdrDispFireObl.GetInt16(2);
                if (!rdrDispFireObl.IsDBNull(3))
                {
                    switch (rdrDispFireObl.GetInt16(3))
                    {
                        case 1:
                            iCntAsrObl++;
                            break;
                        case 2:
                            iCntSnupObl++;
                            break;
                        case 3:
                            iCntFalseObl++;
                            break;
                    }
                }
                iCntFireObl++;
            }
            rdrDispFireObl.Close();
            // считаем выезды СПЧ
            string strSqlTextDispSPCH = "SELECT ReportDisp.RptDispId, ReportFire.UnitId, Unit.UnitName " +
                "FROM ReportDisp INNER JOIN ReportFire ON ReportDisp.RptDispId = ReportFire.RptDispId INNER JOIN Unit ON ReportFire.UnitId = Unit.UnitId " +
                "WHERE (ReportFire.UnitId = 13) AND (ReportDisp.RptRecRpt = 0) AND (RptDate <= CONVERT(DATETIME, '" + strDataTime + "', 102))";
            SqlCommand cmdDispSPCH = new SqlCommand(strSqlTextDispSPCH, FrmMain.Instance.m_cnn);
            SqlDataReader rdrDispSPCH = cmdDispSPCH.ExecuteReader();
            int iCntSPCH = 0;
            while (rdrDispSPCH.Read())
            {
                iCntSPCH++;
            }
            rdrDispSPCH.Close();
            // считаем погибших
            string strSqlTextDispDead = "SELECT ReportDisp.RptDispId, ReportDisp.DepartTypeId, ReportFireDead.RptDeadName " +
                "FROM ReportDisp INNER JOIN ReportFireDead ON ReportDisp.RptDispId = ReportFireDead.RptDispId " +
                "WHERE (ReportDisp.RptRecRpt = 0) AND (RptDate <= CONVERT(DATETIME, '" + strDataTime + "', 102))";
            SqlCommand cmdDispDead = new SqlCommand(strSqlTextDispDead, FrmMain.Instance.m_cnn);
            SqlDataReader rdrDispDead = cmdDispDead.ExecuteReader();
            int iCntDead = 0;   // всего погибших город + область
            int iCntDeadUkg = 0;   // всего погибших город
            int iCntDeadObl = 0;   // всего погибших область
            int iDepartTypeId = 0;
            while (rdrDispDead.Read())
            {
                if (!rdrDispDead.IsDBNull(1))
                {
                    iDepartTypeId = rdrDispDead.GetInt16(1);
                    switch (iDepartTypeId)
                    {
                        case 1:
                            iCntDeadUkg++;
                            break;
                        case 2:
                            iCntDeadObl++;
                            break;
                    }
                }
                iCntDead++;
            }
            rdrDispDead.Close();
            // считаем пострадавших
            string strSqlTextDispVictim = "SELECT ReportDisp.RptDispId, ReportDisp.DepartTypeId, ReportFireVictim.RptVictimName " +
                "FROM ReportDisp INNER JOIN ReportFireVictim ON ReportDisp.RptDispId = ReportFireVictim.RptDispId " +
                "WHERE (ReportDisp.RptRecRpt = 0) AND (RptDate <= CONVERT(DATETIME, '" + strDataTime + "', 102))";
            SqlCommand cmdDispVictim = new SqlCommand(strSqlTextDispVictim, FrmMain.Instance.m_cnn);
            SqlDataReader rdrDispVictim = cmdDispVictim.ExecuteReader();
            int iCntVictim = 0;   // всего пострадавших город + область
            int iCntVictimUkg = 0;   // всего пострадавших город
            int iCntVictimObl = 0;   // всего пострадавших область
            iDepartTypeId = 0;
            while (rdrDispVictim.Read())
            {
                if (!rdrDispVictim.IsDBNull(1))
                {
                    iDepartTypeId = rdrDispVictim.GetInt16(1);
                    switch (iDepartTypeId)
                    {
                        case 1:
                            iCntVictimUkg++;
                            break;
                        case 2:
                            iCntVictimObl++;
                            break;
                    }
                }
                iCntVictim++;
            }
            rdrDispVictim.Close();
            // считаем виновных
            string strSqlTextDispGuilty = "SELECT ReportDisp.RptDispId, ReportDisp.DepartTypeId, ReportFireGuilty.RptGuiltyName " +
                "FROM ReportDisp INNER JOIN ReportFireGuilty ON ReportDisp.RptDispId = ReportFireGuilty.RptDispId " +
                "WHERE (ReportDisp.RptRecRpt = 0) AND (RptDate <= CONVERT(DATETIME, '" + strDataTime + "', 102))";
            SqlCommand cmdDispGuilty = new SqlCommand(strSqlTextDispGuilty, FrmMain.Instance.m_cnn);
            SqlDataReader rdrDispGuilty = cmdDispGuilty.ExecuteReader();
            int iCntGuilty = 0;   // всего виновных город + область
            int iCntGuiltyUkg = 0;   // всего виновных город
            int iCntGuiltyObl = 0;   // всего пвиновных область
            iDepartTypeId = 0;
            while (rdrDispGuilty.Read())
            {
                if (!rdrDispGuilty.IsDBNull(1))
                {
                    iDepartTypeId = rdrDispGuilty.GetInt16(1);
                    switch (iDepartTypeId)
                    {
                        case 1:
                            iCntGuiltyUkg++;
                            break;
                        case 2:
                            iCntGuiltyObl++;
                            break;
                    }
                }
                iCntGuilty++;
            }
            rdrDispGuilty.Close();
            // считаем ПТУ и ПТЗ в городе
            string strSqlTextDispPtUkg = "SELECT ReportDisp.RptDispId, ReportBortDrive.RptUkgPtz, ReportBortDrive.RptUkgPtu " +
                "FROM ReportDisp INNER JOIN ReportBortDrive ON ReportDisp.RptDispId = ReportBortDrive.RptDispId " +
                "WHERE (ReportDisp.RptRecRpt = 0) AND (RptDate <= CONVERT(DATETIME, '" + strDataTime + "', 102))";
            SqlCommand cmdDispPtUkg = new SqlCommand(strSqlTextDispPtUkg, FrmMain.Instance.m_cnn);
            SqlDataReader rdrDispPtUkg = cmdDispPtUkg.ExecuteReader();
            int iCntPtuUkg = 0;   // ПТУ в городе
            int iCntPtzUkg = 0;   // ПТЗ в городе
            while (rdrDispPtUkg.Read())
            {
                if (!rdrDispPtUkg.IsDBNull(1))
                {
                    if (rdrDispPtUkg.GetBoolean(1))
                        iCntPtzUkg++;
                }
                if (!rdrDispPtUkg.IsDBNull(2))
                {
                    if (rdrDispPtUkg.GetBoolean(2))
                        iCntPtuUkg++;
                }
            }
            rdrDispPtUkg.Close();
            // считаем ПТУ и ПТЗ в области
            string strSqlTextDispPtObl = "SELECT ReportDisp.RptDispId, ReportBortDriveObl.RptOblPtz, ReportBortDriveObl.RptOblPtu " +
                "FROM ReportDisp INNER JOIN ReportBortDriveObl ON ReportDisp.RptDispId = ReportBortDriveObl.RptDispId " +
                "WHERE (ReportDisp.RptRecRpt = 0) AND (RptDate <= CONVERT(DATETIME, '" + strDataTime + "', 102))";
            SqlCommand cmdDispPtObl = new SqlCommand(strSqlTextDispPtObl, FrmMain.Instance.m_cnn);
            SqlDataReader rdrDispPtObl = cmdDispPtObl.ExecuteReader();
            int iCntPtuObl = 0;   // ПТУ в области
            int iCntPtzObl = 0;   // ПТЗ в области
            while (rdrDispPtObl.Read())
            {
                if (!rdrDispPtObl.IsDBNull(1))
                {
                    if (rdrDispPtObl.GetBoolean(1))
                        iCntPtzObl++;
                }
                if (!rdrDispPtObl.IsDBNull(2))
                {
                    if (rdrDispPtObl.GetBoolean(2))
                        iCntPtuObl++;
                }
            }
            rdrDispPtObl.Close();
        }

        private void rdBtn0_6_CheckedChanged(object sender, EventArgs e)
        {
            if (rdBtn0_6.Checked)
            {
                lstrDataTimeBef = lstrDataTimeBef + " 06:00:00";
            }
        }

        private void rdBtn6_0_CheckedChanged(object sender, EventArgs e)
        {
            if (rdBtn6_0.Checked)
            {
                lstrDataTimeBef = lstrDataTimeBef + " 00:00:00";
            }
        }

        private void rdBtn0_0_CheckedChanged(object sender, EventArgs e)
        {
            if (rdBtn0_0.Checked)
            {
                lstrDataTimeBef = lstrDataTimeBef + " 00:00:00";
            }
        }

        private void rdBtn8_8_CheckedChanged(object sender, EventArgs e)
        {
            if (rdBtn8_8.Checked)
            {
                lstrDataTimeBef = lstrDataTimeBef + " 08:00:00";
            }
        }

        private void rdBtnPeriod_CheckedChanged(object sender, EventArgs e)
        {
            if (rdBtnPeriod.Checked)
            {
                //FrmDateRpl.Instance = new FrmDateRpl();
                //FrmDateRpl.Instance.ShowDialog();

                lstrDataTimeBef = m_strDataTimeBefRpl;
            }
        }

        private void c1CmbOrgRec_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Return)
            {
                e.Handled = true;
                txtBoxRec.Focus();
            }
        }

        private void txtBoxRec_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Return)
            {
                e.Handled = true;
                string strMsgRec = c1CmbOrgRec.Text + "-" + txtBoxRec.Text;
                object[] items = { strMsgRec };
                c1FlxGrdMsgRec.AddItem(items, 1, 1);
                c1FlxGrdMsgRec.Select(0, 0);
            }
        }
    }
}