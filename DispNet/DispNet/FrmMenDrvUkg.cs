using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Threading;

namespace DispNet
{
    public partial class FrmMenDrvUkg : Form
    {
        static public FrmMenDrvUkg Instance;

        int l_iRowSel = 0; //����� ������ ������� ������� ����� FrmDispFire ��� ��������������
        int l_iDrvSign = 0;  //������� � ��������� ��� ������
        int l_iCatDrvId = 0; //��������� 0-���, 1-�������, 2 - �����, 3 - ������
        static public string m_strCatSingDrv = "0"; // Id ��������� �������� ��� ������ � ������ ��� ���������� �������� ���� ����������� static
        string l_strCatSingDrv = "0"; //����������� ���������� ���� ������� ��������� ���� ����� �� ���� ����� ��������� �������� m_strCatSingDrv

        bool blCatRsLrn = false;  //��������� ������ ��������� ���� ��� �� ������ ������� ������

        DataSet dsCtgDrvList = new DataSet();
        BindingSource bsCtgDrvList = new BindingSource();

        // ������ ����� �� ������ �������
        private Thread selThread = null;
        delegate void SetCtgCallback(int iCatDrv);
        //.....

        //��� ������� ����� �� �������������� �������
        string strRptDispId;
        public int m_iLoadDispRpt = 0; //1-��������� ����� ��� ����� ������, 2-��������� ����� �� ����. �������

        public FrmMenDrvUkg()
        {
            InitializeComponent();
        }

        private void btnRecMen_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("�������� ����������?", "����� - ����� ��� �������",
                    MessageBoxButtons.YesNo, MessageBoxIcon.Question)
                    == DialogResult.Yes)
            {
                if (m_iLoadDispRpt == 1)
                    RecDataRptMenDrive();
                if (m_iLoadDispRpt == 2)
                    UpdateDataRptMenDrive();
                Close();
            }
        }

        private void FrmMenDrvUkg_Load(object sender, EventArgs e)
        {
            m_lblCatDrv.Text = "����� ��� ���������";
            
            if (m_iLoadDispRpt == 2)
                DispBortDrvObl_Load();

            ThreadStartFrmDrvMen();  // ����� ������ ��� ������ �� ������� ������ ����� � �������� ��������� ������  
        }

        private void DispBortDrvObl_Load()
        {
            strRptDispId = FrmDispFire.Instance.m_strRptDispId;
            l_iRowSel = FrmDispFire.Instance.m_iDispFireRowSel;

            string strSqlTextMenDrv = "SELECT ReportDisp.RptDispId, ReportDisp.RptDispMsg, ReportMenDrive.RptMenPlace, " +
                                        "ReportMenDrive.RptMenTake, ReportMenDrive.RptMenTime, " +
                                        "ReportMenDrive.RptDrvMenSign, ReportMenDrive.RptCatDrvMenId " +
                                        "FROM ReportDisp INNER JOIN ReportMenDrive ON ReportDisp.RptDispId = ReportMenDrive.RptDispId " +
                                        "WHERE (ReportDisp.RptDispId = " + strRptDispId + ")";
            SqlCommand cmd = new SqlCommand(strSqlTextMenDrv, FrmMain.Instance.m_cnn);
            SqlDataReader rdrMenDrv = cmd.ExecuteReader();

            while (rdrMenDrv.Read())
            {
                //if (!rdrMenDrv.IsDBNull(1))
                    //txtBoxType.Text = rdrMenDrv.GetString(1);
                if (!rdrMenDrv.IsDBNull(2))
                    txtBoxPlace.Text = rdrMenDrv.GetString(2);
                if (!rdrMenDrv.IsDBNull(3))
                    txtBoxTake.Text = rdrMenDrv.GetString(3);
                if (!rdrMenDrv.IsDBNull(4))
                {
                    c1DateRptDrvMen.Text = rdrMenDrv.GetDateTime(4).ToString("d-MM-yyyy HH:mm:ss");
                    //c1DateRptDrvMen.Text = rdrMenDrv.GetString(4);
                }
                if (!rdrMenDrv.IsDBNull(5))
                    l_iDrvSign = rdrMenDrv.GetInt16(5);
                if (!rdrMenDrv.IsDBNull(6))
                    l_iCatDrvId = rdrMenDrv.GetInt16(6);
            }
            rdrMenDrv.Close();

            l_strCatSingDrv = l_iDrvSign.ToString();    //��������� ��������� ���� ������� ������ ��� ������ ������ ���������

            switch (l_iCatDrvId)
            {
                case 0:
                    rdBtnNoCatDrv.Checked = true;
                    m_lblCatDrv.Text = "����� ������� ��� ���������";
                    grpBoxType.Text = "";
                    break;
                case 1:
                    rdBtnDrvLrn.Checked = true;
                    string strSqlLrnCat = "SELECT MsgCatLrnId, MsgCatLrnText FROM InfoCatLrnDrvUkg WHERE (MsgCatLrnId = " + l_iDrvSign.ToString() + ")";
                    CtgMsgListLoad(strSqlLrnCat); //��������� ����� ��������� �� ������� 
                    break;
            }
        }

        private void CtgMsgListLoad(string strSql)
        {
            SqlCommand cmdCat = new SqlCommand(strSql, FrmMain.Instance.m_cnn);
            SqlDataReader rdrMsgCat = cmdCat.ExecuteReader();

            while (rdrMsgCat.Read())
            {
                if (!rdrMsgCat.IsDBNull(1))
                    m_lblCatDrv.Text = rdrMsgCat.GetString(1);
            }
            rdrMsgCat.Close();
        }

        private void UpdateDataRptMenDrive()
        {
            string strAdressUkg = m_lblCatDrv.Text + " (����� ��� �������)";

            if (FrmDispFire.Instance.Visible)
                FrmDispFire.Instance.c1FlxGrdDispMain.SetData(l_iRowSel, 3, strAdressUkg, true);   //��������� � ������� ������ �� �������
                
            string strSqlTextTypeDisp = "UPDATE ReportDisp SET RptDispMsg = '" + strAdressUkg +
                                                    "' WHERE RptDispId = " + strRptDispId;
            SqlCommand commandDisp = new SqlCommand(strSqlTextTypeDisp, FrmMain.Instance.m_cnn);
            Int32 recordsAffectedDisp = commandDisp.ExecuteNonQuery();

            string myDateTimeValue = c1DateRptDrvMen.Text;
            string strDateTimeRec = "00-00-00 00:00:00";
            if (myDateTimeValue != "")
            {
                DateTime cnvDateTime = DateTime.Parse(myDateTimeValue);
                strDateTimeRec = cnvDateTime.Month.ToString() + "-" + cnvDateTime.Day.ToString() + "-" + cnvDateTime.Year.ToString() + " " +
                                        cnvDateTime.ToString("HH:mm:ss");
            }
            string strSqlTextPlace = "UPDATE ReportMenDrive SET RptMenPlace = '" + txtBoxPlace.Text + "', RptMenTake = '" + txtBoxTake.Text + "', " +
                "RptMenTime = CONVERT(DATETIME, '" + strDateTimeRec + "', 102), " +
                "RptDrvMenSign = '" + l_strCatSingDrv + "', RptCatDrvMenId = '" + l_iCatDrvId + "' " +
                "WHERE RptDispId = " + strRptDispId;
            SqlCommand command = new SqlCommand(strSqlTextPlace, FrmMain.Instance.m_cnn);
            Int32 recordsAffected = command.ExecuteNonQuery();
        }

        private void RecDataRptMenDrive()
        {
            DateTime myDateTime = DateTime.Now;
            string strDate = myDateTime.Month.ToString() + "-" + myDateTime.Day.ToString() + "-" + myDateTime.Year.ToString() + " " +
                                myDateTime.ToString("HH:mm:ss");
            string strUkg = "1";
            string strUkgObl = "1";
            string strAdressUkg = m_lblCatDrv.Text + " (����� ��� �������)";
            string strSqlTextReportDisp = "INSERT INTO ReportDisp " +
            "(RptDispId, RptDate, RptDispMsg, RptDistrict, RptCity, DepartTypeId, StateChsId, RptRecRpt) " +
            "Values('0', CONVERT(DATETIME, '" + strDate + "', 102), '" + strAdressUkg + "', '" + strUkgObl + "', '" + strUkg + "', '5', '5', '0')";

            SqlCommand command = new SqlCommand(strSqlTextReportDisp, FrmMain.Instance.m_cnn);
            Int32 recordsAffected = command.ExecuteNonQuery();

            string strSqlTextReportDispId = "SELECT RptDispId FROM ReportDisp " +
                                            "WHERE(RptDate = CONVERT(DATETIME, '" + strDate + "', 102)) " +
                                            "ORDER BY RptDispId";
            SqlCommand cmd = new SqlCommand(strSqlTextReportDispId, FrmMain.Instance.m_cnn);
            SqlDataReader rdrDispId = cmd.ExecuteReader();
            int iDispId = 0;
            while (rdrDispId.Read())
            {
                if (!rdrDispId.IsDBNull(0))
                    iDispId = rdrDispId.GetInt32(0);
            }
            rdrDispId.Close();
            if (iDispId > 0)
            {
                string myDateTimeValue = c1DateRptDrvMen.Text;
                string strDateTimeRec = "00-00-00 00:00:00";
                if (myDateTimeValue != "")
                {
                    DateTime cnvDateTime = DateTime.Parse(myDateTimeValue);
                    strDateTimeRec = cnvDateTime.Month.ToString() + "-" + cnvDateTime.Day.ToString() + "-" + cnvDateTime.Year.ToString() + " " +
                                            cnvDateTime.ToString("HH:mm:ss");
                }
                string strRptDispId = iDispId.ToString();
                string strSqlTextReportMenDrive = "INSERT INTO ReportMenDrive " +
                "(RptMenDrvId, RptDispId, RptMenPlace, RptMenTake, RptMenTime, RptDrvMenSign, RptCatDrvMenId) " +
                "Values('0', '" + strRptDispId + "', '" + txtBoxPlace.Text + "', '" + txtBoxTake.Text + "', CONVERT(DATETIME, '" + strDateTimeRec + "', 102), " +  //'" + c1DateRptDrvMen.Text + "'"
                "'" + l_strCatSingDrv + "', '" + l_iCatDrvId + "')";

                SqlCommand cmdMenId = new SqlCommand(strSqlTextReportMenDrive, FrmMain.Instance.m_cnn);
                Int32 rdsAffectedMenId = cmdMenId.ExecuteNonQuery();
                
            }// ��������� ���������� ���� ��������� ������ �� �������
        }

        private void rdBtnNoCatDrv_CheckedChanged(object sender, EventArgs e)
        {
            l_iCatDrvId = 0;
            m_lblCatDrv.Text = "����� ��� ���������";
            m_strCatSingDrv = "0";
        }

        private void rdBtnDrvLrn_CheckedChanged(object sender, EventArgs e)
        {
            l_iCatDrvId = 1;
            m_lblCatDrv.Text = "";
            m_strCatSingDrv = "0";
        }

        private void btnCatDrvChs_Click(object sender, EventArgs e)
        {
            ThreadLoadCtg(l_iCatDrvId);
        }

        private void ThreadStartFrmDrvMen()
        {
            selThread = new Thread(new ThreadStart(ThreadProcDrvMen));
            selThread.Start();
        }

        private void ThreadProcDrvMen()
        {
            ThreadLoadCtg(-1);
        }

        private void ThreadLoadCtg(int iCatDrv)
        {
            if (m_lblCatDrv.InvokeRequired)
            {
                SetCtgCallback d = new SetCtgCallback(ThreadLoadCtg);
                this.Invoke(d, new object[] { iCatDrv });
            }
            else
            {
                switch (iCatDrv)
                {
                    case 1:
                        CtgLrnListLoad(blCatRsLrn);
                        blCatRsLrn = true;
                        break;
                }
            }
        }

        private void CtgLrnListLoad(bool blLoad)
        {
            if (!blLoad)
            {
                string strSqlTextCtgDrvList = "SELECT MsgCatLrnText, MsgCatLrnId FROM InfoCatLrnDrvUkg ORDER BY MsgCatLrnText";
                SqlDataAdapter daCtgDrvList = new SqlDataAdapter(strSqlTextCtgDrvList, FrmMain.Instance.m_cnn);
                daCtgDrvList.Fill(dsCtgDrvList, "InfoCatLrnDrvUkg");
                bsCtgDrvList.DataSource = dsCtgDrvList;
                bsCtgDrvList.DataMember = "InfoCatLrnDrvUkg";
            }

            FrmChsCatDrv.Instance = new FrmChsCatDrv();
            FrmChsCatDrv.Instance.Text = "����� ��������� ������";
            FrmChsCatDrv.Instance.m_grpBoxCat.Text = "�������";
            FrmChsCatDrv.Instance.m_dsMsgCat = dsCtgDrvList.Copy();
            FrmChsCatDrv.Instance.m_bsMsgCat = new BindingSource(dsCtgDrvList, "InfoCatLrnDrvUkg");
            FrmChsCatDrv.Instance.m_strValDispMem = "MsgCatLrnText";
            FrmChsCatDrv.Instance.m_strMsgCatTable = "InfoCatLrnDrvUkg";
            FrmChsCatDrv.Instance.m_strMsgCatId = "MsgCatLrnId";
            FrmChsCatDrv.Instance.m_iSendMsgFrm = 6;    //��������� ������� � ��� �����
            FrmChsCatDrv.Instance.m_hndFrmCat = m_lblCatDrv.Handle;
            FrmChsCatDrv.Instance.ShowDialog();
            l_strCatSingDrv = m_strCatSingDrv;

        }
    }
}