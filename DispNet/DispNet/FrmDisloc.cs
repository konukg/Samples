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
    public partial class FrmDisloc : Form
    {
        static public FrmDisloc Instance;
        DataSet dsDispDsl = new DataSet();
        public string m_strBortDslId;
        public int m_iFrmDsl = 0;   //признак из какой формы вызывается 1-из  2- из FrmDispUkgDrv
        bool blDislOk = false;
                
        public FrmDisloc()
        {
            InitializeComponent();
        }

        private void FrmDisloc_Load(object sender, EventArgs e)
        {
            string strSqlTextDisp = "SELECT ReportDisp.RptDispId, ReportDisp.RptDate, ReportDisp.RptDispMsg, " +
                                    "DepartType.DepartTypeName, StateChsDrv.StateChsName, DepartType.DepartTypeId " +
                                    "FROM ReportDisp INNER JOIN " +
                                    "DepartType ON ReportDisp.DepartTypeId = DepartType.DepartTypeId INNER JOIN " +
                                    "StateChsDrv ON ReportDisp.StateChsId = StateChsDrv.StateChsId " +
                                    "ORDER BY ReportDisp.RptDate";

            SqlDataAdapter daDispDsl = new SqlDataAdapter(strSqlTextDisp, FrmMain.Instance.m_cnn);
            daDispDsl.Fill(dsDispDsl, "ReportDisp");
                        
            

            DataRow[] drarray;
            drarray = dsDispDsl.Tables["ReportDisp"].Select(""); ;
            string strAdd;

            for (int i = 0; i < drarray.Length; i++)
            {
                strAdd = drarray[i]["RptDispId"].ToString() + ";" + drarray[i]["RptDate"].ToString() + ";" +
                            drarray[i]["RptDispMsg"].ToString() + ";" + drarray[i]["StateChsName"].ToString();
                c1ListDslFire.AddItem(strAdd);
            }
            
            c1ListDslFire.Splits[0].DisplayColumns[0].Visible = false;
            c1ListDslFire.Splits[0].DisplayColumns[1].Width = 110;
            c1ListDslFire.Splits[0].DisplayColumns[2].Width = 200;

        }

        private void btnRecDsl_Click(object sender, EventArgs e)
        {
            string strRptDispId = c1ListDslFire.GetItemText(c1ListDslFire.SelectedIndex, 0);
            DateTime myDateTime = DateTime.Now;
            string strBortDrive = myDateTime.ToString("HH:mm:ss");
            string strSqlTextReportFireBort = "INSERT INTO ReportFireBort " +
                        "(RptFireBortId, RptDispId, TechnicsId, BortDrive) " +
                        "Values('0', '" + strRptDispId + "', '" + m_strBortDslId + "', '" + strBortDrive + "')";

            SqlCommand command = new SqlCommand(strSqlTextReportFireBort, FrmMain.Instance.m_cnn);
            Int32 recordsAffected = command.ExecuteNonQuery();

            switch (m_iFrmDsl)
            {
                case 1:
                    FrmDispUkg.Instance.m_iDislOk = 1;
                    break;
                case 2:
                    FrmDispUkgDrv.Instance.m_iDislOk = 2;
                    break;
            }
            blDislOk = true;
            Close();
        }

        private void FrmDisloc_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (!blDislOk)
            {
                switch (m_iFrmDsl)
                {
                    case 1:
                        FrmDispUkg.Instance.m_iDislOk = 0;
                        break;
                    case 2:
                        FrmDispUkgDrv.Instance.m_iDislOk = 0;
                        break;
                }
            }
        }
       
    }
}