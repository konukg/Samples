using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace DispNet
{
    public partial class FrmDateRpl : Form
    {
        static public FrmDateRpl Instance;

        bool l_blCloseRec = false;  //признак форма закрыта через кнопку выбор
        public int m_iFrmLoad = 0;  //признак из кокой формы был сделан вызов 1-FrmDispFire, 2-FrmReport

        public FrmDateRpl()
        {
            InitializeComponent();
        }

        private void FrmDateRpl_Load(object sender, EventArgs e)
        {
            DateTime myDateBef;
            myDateBef = DateTime.Now;
            c1DateRptBef.Text = myDateBef.ToLocalTime().ToString("d-MM-yyyy HH:mm:ss"); //.ToString("dd.MM.yyyy");

            //DateTime myDateAft = new DateTime(myDateBef.Year, myDateBef.Month, myDateBef.Day - 1);
            //c1DateRptAft.Text = myDateAft.ToString("d-MM-yyyy 00:00:00");
        }

        private void btnRecDateRpl_Click(object sender, EventArgs e)
        {
            
            //FrmReportInfoDrv.Instance.rdBtnPeriod.Text = c1DateRptAft.Text + " - " + c1DateRptBef.Text;
            //FrmReportInfoDrv.Instance.m_strDataTimeAftRpl = c1DateRptAft.Text;
            //FrmReportInfoDrv.Instance.m_strDataTimeBefRpl = c1DateRptBef.Text;

            //FrmDispFire.Instance.m_strDataTimeSqlDown = myDateCnv.ToString("MM-d-yyyy HH:mm:ss");
            //myDateCnv = DateTime.Parse(c1DateRptBef.Text);
            //FrmDispFire.Instance.m_strDataTimeSqlUp = myDateCnv.ToString("MM-d-yyyy HH:mm:ss");

            string strDateRptAft = c1DateRptAft.Text;
            string strDateRptBef = c1DateRptBef.Text;
            if (strDateRptAft == "" || strDateRptBef == "")
            {
                string strMsgDateErr = "¬ведите дату ";
                if (strDateRptAft == "")
                    strMsgDateErr = strMsgDateErr + "начала периода";
                if (strDateRptBef == "")
                {
                    if (strDateRptAft == "")
                        strMsgDateErr = strMsgDateErr + " и окончани€ периода!";
                    else
                        strMsgDateErr = strMsgDateErr + "окончани€ периода!";
                }
                MessageBox.Show(strMsgDateErr, "¬вод периода времени",
                        MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
            else
            {
                switch (m_iFrmLoad)
                {
                    case 1:
                        FrmDispFire.Instance.m_strDataTimeSqlDown = DateTime.Parse(strDateRptAft).ToString("MM-d-yyyy HH:mm:ss");
                        FrmDispFire.Instance.m_strDataTimeAft = strDateRptAft;
                        FrmDispFire.Instance.m_strDataTimeSqlUp = DateTime.Parse(strDateRptBef).ToString("MM-d-yyyy HH:mm:ss");
                        FrmDispFire.Instance.m_strDataTimeBef = strDateRptBef;
                        l_blCloseRec = true;
                        break;
                    case 2:
                        FrmReport.Instance.m_strDataTimeSqlDown = DateTime.Parse(strDateRptAft).ToString("MM-d-yyyy HH:mm:ss");
                        FrmReport.Instance.m_strDataTimeAft = strDateRptAft;
                        FrmReport.Instance.m_strDataTimeSqlUp = DateTime.Parse(strDateRptBef).ToString("MM-d-yyyy HH:mm:ss");
                        FrmReport.Instance.m_strDataTimeBef = strDateRptBef;
                        FrmReport.Instance.m_fn_ReplDateRpt();
                        l_blCloseRec = true;
                        break;
                }
            }        
            if(l_blCloseRec)
                Close();
        }

        private void FrmDateRpl_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (m_iFrmLoad == 1)
            {
                if (l_blCloseRec)
                    FrmDispFire.Instance.m_blShowCase1 = true;
                else
                    FrmDispFire.Instance.m_blShowCase1 = false;
            }
        }

        
    }
}