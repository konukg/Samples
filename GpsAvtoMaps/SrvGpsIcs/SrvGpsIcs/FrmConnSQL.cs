using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using System.Data.SqlClient;

namespace SrvGpsIcs
{
    public partial class FrmConnSQL : Form
    {
        static public FrmConnSQL Instance;
        SqlConnection l_cnn;
        bool l_blConSql = false;    //если true то есть соединение с сервером SQL

        public FrmConnSQL()
        {
            InitializeComponent();
        }

        private void FrmConnSQL_Load(object sender, EventArgs e)
        {
            c1TxtBoxSqlName.Text = FrmMain.Instance.m_strSqlName;
            c1TxtBoxBase.Text = FrmMain.Instance.m_strSqlBase;
            c1TxtBoxLog.Text = FrmMain.Instance.m_strSqlLog;
            c1TxtBoxPass.Text = FrmMain.Instance.m_strSqlPass;
        }

        private void FrmConnSQL_Activated(object sender, EventArgs e)
        {
            c1BtnTestSQL.Focus();
        }

        private void c1BtnTestSQL_Click(object sender, EventArgs e)
        {
            FrmConnSQL.Instance.Cursor = Cursors.WaitCursor;

            try
            {
                string strConnSqlSrv = "server=" + c1TxtBoxSqlName.Text + ";user id=" + c1TxtBoxLog.Text + ";" +
                                        "password=" + c1TxtBoxPass.Text + ";initial catalog=" + c1TxtBoxBase.Text;
                l_cnn = new SqlConnection(strConnSqlSrv);
                l_cnn.Open();
                l_blConSql = true;
            }
            catch (SqlException se)
            {
                string strErr = se.Message;
                //MessageBox.Show(se.Message);
            }
            this.Cursor = Cursors.Arrow;
            if (l_blConSql)
            {
                if (FrmMain.Instance.m_blConSql)
                    FrmMain.Instance.m_cnn.Close(); // закрывает соединение с сервереом SQL и открывает новое

                l_cnn.Close();

                FrmMain.Instance.m_strSqlName = c1TxtBoxSqlName.Text;
                FrmMain.Instance.m_strSqlBase = c1TxtBoxBase.Text;
                FrmMain.Instance.m_strSqlLog = c1TxtBoxLog.Text;
                FrmMain.Instance.m_strSqlPass = c1TxtBoxPass.Text;

                IniFile ini = new IniFile(Application.StartupPath + "\\SrvGpsIcs.ini");
                ini.WriteValue("SQL", "Name", c1TxtBoxSqlName.Text);
                ini.WriteValue("SQL", "Base", c1TxtBoxBase.Text);
                ini.WriteValue("SQL", "Login", c1TxtBoxLog.Text);
                ini.WriteValue("SQL", "Pass", c1TxtBoxPass.Text);
                
                MessageBox.Show("Есть соединение с сервером!", "SQL сервер",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);

                FrmMain.Instance.m_backgrndWrkSql.RunWorkerAsync();
                Close();
            }
            else
            {
                MessageBox.Show("Нет соединение с SQL сервером!", "SQL сервер",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
    }
}
