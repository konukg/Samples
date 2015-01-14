using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.ComponentModel;
using System.Threading;
using System.Data.SqlClient;

namespace SrvGpsIcs
{
    class SendRecSqlData
    {
        BackgroundWorker m_backgrndRecSql = new BackgroundWorker();
        
        private SqlConnection l_cnn;
        private string l_strSqlConn;
        private string l_strBortTrek;
        private string l_strMsgInfo;

        public SendRecSqlData(string strSql, string strBort, string strMsg)
        {
            l_strSqlConn = strSql;
            l_strBortTrek = strBort;
            l_strMsgInfo = strMsg;
            
            m_backgrndRecSql.DoWork += new DoWorkEventHandler(backgrndRecSql_DoWork);
            m_backgrndRecSql.RunWorkerCompleted += new RunWorkerCompletedEventHandler(backgrndRecSql_RunWorkerCompleted);

            m_backgrndRecSql.RunWorkerAsync();
        }

        private void backgrndRecSql_DoWork(object sender, DoWorkEventArgs e)
        {
            try
            {
                l_cnn = new SqlConnection(l_strSqlConn);
                l_cnn.Open();

                DateTime myDateTime = DateTime.Now;
                string strDate = myDateTime.Month.ToString() + "-" + myDateTime.Day.ToString() + "-" + myDateTime.Year.ToString() + " " +
                                    myDateTime.ToString("HH:mm:ss");
                string strSqlTextReport = "INSERT INTO BortReportRoute " +
                    "(RptBortId, RptBortDate, RptBortTrek, RptBortRoute) " +
                    "Values('0', CONVERT(DATETIME, '" + strDate + "', 102), '" + l_strBortTrek + "', '" + l_strMsgInfo + "')";
                SqlCommand command = new SqlCommand(strSqlTextReport, l_cnn);
                Int32 recordsAffected = command.ExecuteNonQuery();
            }
            catch (SqlException se)
            {
                //MessageBox.Show(se.Message);
                string strErr = se.Message;
            }
            
        }

        private void backgrndRecSql_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            l_cnn.Close();
        }
    }
}
