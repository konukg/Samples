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
    public partial class FrmReportPrintArch : Form
    {
        static public FrmReportPrintArch Instance;
        string lstrDateFind;    // дата для поиска

        public FrmReportPrintArch()
        {
            InitializeComponent();
        }

        private void FrmReportPrintArch_Load(object sender, EventArgs e)
        {
            string strSqlTextDate = "SELECT RptFireDateId, RptDateAft, RptDateBef FROM ReportFirePrnDate ORDER BY RptFireDateId";
            SqlCommand cmd = new SqlCommand(strSqlTextDate, FrmMain.Instance.m_cnn);
            SqlDataReader rdr = cmd.ExecuteReader();
            string strRptDateId = "";
            string strDataTimeSqlAft = "";
            string strDataTimeSqlBef = "";
            string strAddItem;
            if (rdr.HasRows)
            {
                while (rdr.Read())
                {
                    if (!rdr.IsDBNull(0))
                        strRptDateId = rdr.GetInt32(0).ToString();
                    if (!rdr.IsDBNull(1))
                        strDataTimeSqlAft = rdr.GetDateTime(1).ToString();
                    if (!rdr.IsDBNull(2))
                        strDataTimeSqlBef = rdr.GetDateTime(2).ToString();
                    strAddItem = strDataTimeSqlAft + ";" + strDataTimeSqlBef + ";" + strRptDateId;
                    c1ListRptArch.AddItem(strAddItem);
                }
            }
            rdr.Close();

            c1ListRptArch.Splits[0].DisplayColumns[2].Visible = false;
        }

        private void FindDateArch()
        {
            c1ListRptArch.ClearSelected();
            lstrDateFind = c1DateFndAr.Text;
            int found = c1ListRptArch.FindString(lstrDateFind, 0, 0);
            if (found == -1)
            {
                found = c1ListRptArch.FindString(lstrDateFind, 0, 1);
                if (found == -1)
                {
                    string strFindMsg = "Отчет с датой " + lstrDateFind + " не найден!";
                    MessageBox.Show(strFindMsg, "Поиск отчета",
                       MessageBoxButtons.OK, MessageBoxIcon.Stop);
                }
                else
                {
                    c1ListRptArch.SelectedIndex = found;
                    btnPrnArch.Focus();
                }
            }
            else
            {
                c1ListRptArch.SelectedIndex = found;
                btnPrnArch.Focus();
            }
        }

        private void btnPrnArch_Click(object sender, EventArgs e)
        {
            int iCnt = c1ListRptArch.SelectedIndex;
            if (iCnt == -1)
            {
                MessageBox.Show("Необходимо выбрать дату!", "Поиск отчета",
                       MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
            else
            {
                if(!FrmReportMain.ms_blFrmReportMain_Show)
                    FrmReportMain.Instance = new FrmReportMain();
                string strRptDateId = c1ListRptArch.GetItemText(iCnt, 2);
                string strRptDateCap = c1ListRptArch.GetItemText(iCnt, 0);
                string[] strDate;
                strDate = strRptDateCap.Split(' ');   // выделяет только дату
                strRptDateCap = strDate[0];
                strRptDateCap = "Архив за " + strRptDateCap;
                FrmReportMain.Instance.m_LoadNewDoc(strRptDateId, 2, strRptDateCap);

                if (FrmReportMain.Instance.WindowState == FormWindowState.Minimized)
                    FrmReportMain.Instance.WindowState = FormWindowState.Normal;
                else
                    FrmReportMain.Instance.Show();
                //c1ListRptArch.ClearSelected();
                //Close();
            }
            // если отмечено неколько дат отчетов
            /*int iCnt = c1ListRptArch.ListCount;
            if (iCnt > 0)
            {
                FrmReportMain.Instance = new FrmReportMain();
                for (int i = 0; i < iCnt; i = i + 1)
                {
                    if (c1ListRptArch.GetSelected(i))
                    {
                        string strRptDateId = c1ListRptArch.GetItemText(i, 2);
                        FrmReportMain.Instance.m_LoadNewDoc(strRptDateId, 2);
                    }
                }
                FrmReportMain.Instance.Show();
                c1ListRptArch.ClearSelected();
            }*/
        }

        private void c1DateFndAr_DropDownClosed(object sender, C1.Win.C1Input.DropDownClosedEventArgs e)
        {
            FindDateArch();
        }

        private void c1DateFndAr_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Return)
            {
                e.Handled = true;
                FindDateArch();
            }
        }

    }
}