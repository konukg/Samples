using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using C1.Win.C1Report;

namespace DispNet
{
    public partial class FrmReportPrint : Form
    {
        static public FrmReportPrint Instance;
        public string m_strRptPrnId;
        public int m_iRptSing;   // признак отчета 1 - текущие сутки 2 - архив 

        public FrmReportPrint()
        {
            InitializeComponent();
        }

        private void FrmReportPrint_Load(object sender, EventArgs e)
        {
            
            //strRptPrnId = FrmReport.Instance.m_strRptDateId;
            string strRecSrsSQL = "SELECT RptFirePrnId, RptFireDateId, RptDateTimeFire, RptFireRing, RptFireData, RptFireDoc, " +
                "RptFireAsr, RptTimeBarrel, RptTimeLocal, RptTimeFireSupp, RptFireType " +
                "FROM ReportFirePrn WHERE (RptFireDateId = '" + m_strRptPrnId + "')";
            c1RptFire.Load("dispfirerpt.xml", "ReportFirePrn Report");
            //string strConStr = @"Provider=SQLOLEDB.1;Integrated Security=SSPI;Persist Security Info=False;Initial Catalog=FireNet;Data Source=SBX64VS\SQLEXPRESS";
            //c1RptFire.DataSource.ConnectionString = strConStr;
            c1RptFire.DataSource.RecordSource = strRecSrsSQL;

            //c1RptFire.Load("dispfirerpt.xml", "ReportFirePrnBort Report");
            //c1RptFire.DataSource.ConnectionString = strConStr;
            //c1RptFire.DataSource.RecordSource = "ReportFirePrnBort";
                        
            //srField.Subreport.Fields["RptBortArriveCtl"].ForeColor = Color.Red;
            Field srField = c1RptFire.Fields["SubFieldBort"];
            bool blPtrPrn = false;
            if(m_iRptSing == 1)
                blPtrPrn = FrmReport.Instance.m_chkBoxTime.Checked;
            if (m_iRptSing == 2)
                blPtrPrn = FrmReportPrintArch.Instance.m_chkBoxTime.Checked;
            if (!blPtrPrn)
            {
                srField.Subreport.Fields["RptBortDriveCtl"].Format = "Long Time";
                srField.Subreport.Fields["RptBortArriveCtl"].Format = "Long Time";
                srField.Subreport.Fields["RptBortReturnCtl"].Format = "Long Time";
            }
            c1PrntPreCntrRpt.Document = c1RptFire.Document;

            
            /*// create export filter
            string fileName = Application.StartupPath + @"\111.xml";
            XmlFilter f = new XmlFilter(fileName);
            f.ValuesAsAttributes = true;

            // render report to filter
            c1RptFire.RenderToFilter(f);

            string fileName = Application.StartupPath + @"\333.txt";
            c1RptFire.RenderToFile(fileName, FileFormatEnum.TextSinglePage);*/
        }

        private void FrmReportPrint_Resize(object sender, EventArgs e)
        {
            Control control = (Control)sender;

            c1PrntPreCntrRpt.Height = control.Size.Height;
            c1PrntPreCntrRpt.Width = control.Size.Width;
        }
    }
}