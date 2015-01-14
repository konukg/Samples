using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;
using C1.Win.C1FlexGrid;

namespace DispNet
{
    public partial class pageTabChronologyObl : UserControl
    {
        DataSet dsMsgList = new DataSet();
        BindingSource bsMsgList = new BindingSource();
        string strRptDispId;
        
        public pageTabChronologyObl()
        {
            InitializeComponent();
        }

        private void pageTabChronologyObl_Load(object sender, EventArgs e)
        {
            string strSqlTextMsgList = "SELECT RptChrMsgText FROM ReportChronoMsg ORDER BY RptChrMsgText";
            SqlDataAdapter daMsgListObl = new SqlDataAdapter(strSqlTextMsgList, FrmMain.Instance.m_cnn);
            daMsgListObl.Fill(dsMsgList, "ReportChronoMsg");

            bsMsgList.DataSource = dsMsgList;
            bsMsgList.DataMember = "ReportChronoMsg";
            c1CmbChrListObl.DataSource = bsMsgList;
            c1CmbChrListObl.ValueMember = "RptChrMsgText";
            c1CmbChrListObl.DisplayMember = "RptChrMsgText";

            c1CmbChrListObl.Columns[0].Caption = "“екст сообщени€";
            c1CmbChrListObl.Splits[0].DisplayColumns[0].HeadingStyle.HorizontalAlignment = C1.Win.C1List.AlignHorzEnum.Center;

            strRptDispId = FrmDispFire.Instance.m_strRptDispId;
            LoadDataChronoTime(strRptDispId);

            c1FlxGrdChrTimeObl.Select(0, 0);

            LoadDataMsgList();
        }

        private void LoadDataChronoTime(string strRptDispId)
        {
            string strSqlTextChrTime = "SELECT RptDispId, RptTimeBarrel, RptTimeLocal, RptTimeFireSupp " +
                                       "FROM ReportFireObl WHERE (RptDispId = " + strRptDispId + ")";
            SqlCommand cmd = new SqlCommand(strSqlTextChrTime, FrmMain.Instance.m_cnn);
            SqlDataReader rdr = cmd.ExecuteReader();

            while (rdr.Read())
            {
                if (!rdr.IsDBNull(1))
                    c1DatePdSt.Text = rdr.GetDateTime(1).ToString("dd.MM.yyyy HH:mm:ss");
                if (!rdr.IsDBNull(2))
                    c1DateLoc.Text = rdr.GetDateTime(2).ToString("dd.MM.yyyy HH:mm:ss");
                if (!rdr.IsDBNull(3))
                    c1DateLikv.Text = rdr.GetDateTime(3).ToString("dd.MM.yyyy HH:mm:ss");
            }
            rdr.Close();
        }

        private void LoadDataMsgList()
        {
            //string strDate = "08.06.2007 8:30:00";
            DateTime myDateTime;
            myDateTime = DateTime.Now;
            string strDate = myDateTime.Day.ToString() + "-" + myDateTime.Month.ToString() + "-" + myDateTime.Year.ToString() + " " +
                                myDateTime.ToString("HH:mm:ss");
            DataSet dsFireStat = new DataSet();
            string strSqlTextChrMsg = "SELECT RptFireStatId, RptDispId, RptFireStatDate, RptFireStatTxt " +
                                       "FROM ReportFireStat WHERE (RptDispId = " + strRptDispId + ") " +
                                       "AND RptFireStatDate < CONVERT(DATETIME, '" + strDate + "', 103) " +
                                       "ORDER BY RptFireStatDate DESC";
            SqlCommand cmd = new SqlCommand(strSqlTextChrMsg, FrmMain.Instance.m_cnn);
            SqlDataReader rdrMsg = cmd.ExecuteReader();

            string strStatId;
            string strDispId;
            string strStatDate;
            string strStatTxt;
            while (rdrMsg.Read())
            {
                if (rdrMsg.IsDBNull(0))
                    strStatId = "0";
                else
                    strStatId = rdrMsg.GetInt32(0).ToString();

                if (rdrMsg.IsDBNull(1))
                    strDispId = "0";
                else
                    strDispId = rdrMsg.GetInt32(1).ToString();

                if (rdrMsg.IsDBNull(2))
                    strStatDate = "";
                else
                    strStatDate = rdrMsg.GetDateTime(2).ToString("dd.MM.yyyy HH:mm:ss");//выводить 0 перед первой цифрой часа

                if (rdrMsg.IsDBNull(3))
                    strStatTxt = "";
                else
                    strStatTxt = rdrMsg.GetString(3);

                object[] items = { strStatId, strDispId, strStatDate, strStatTxt };
                c1FlxGrdChrMsgObl.AddItem(items, 1, 1);
            }
            rdrMsg.Close();
            c1FlxGrdChrMsgObl.Sort(SortFlags.Descending, 3);
            c1FlxGrdChrMsgObl.Cols[3].EditMask = "##-##-#### ##:##:##";
            /*string strSqlTextChrMsg = "SELECT RptDispId, RptChronoMsg " +
                                       "FROM ReportFireObl WHERE (RptDispId = " + strRptDispId + ")";
            SqlCommand cmd = new SqlCommand(strSqlTextChrMsg, FrmMain.Instance.m_cnn);
            SqlDataReader rdrMsg = cmd.ExecuteReader();

            string[] strMsgList;
            while (rdrMsg.Read())
            {
                if (rdrMsg.IsDBNull(1))
                {
                    strChrMsg = "";
                }
                else
                {
                    strChrMsg = rdrMsg.GetString(1);
                    strMsgList = strChrMsg.Split('|');
                    int j = 1;
                    for (int i = 0; i < strMsgList.Length; i++)
                    {
                        int lastLocation = strMsgList[i].IndexOf("#");
                        if (lastLocation > 0)
                        {
                            j = j + i;
                            string strMsgGrd = "|" + strMsgList[i].Substring(0, lastLocation) + "|" 
                                                + strMsgList[i].Substring(lastLocation + 1);
                            c1FlxGrdChrMsgObl.AddItem(strMsgGrd, i+1);
                        }
                    }
                    c1FlxGrdChrMsgObl.Select(1, 2);
                    c1FlxGrdChrMsgObl.Cols[1].EditMask = "##:##:##";
                }
            }
            c1FlxGrdChrMsgObl.Cols[2].StyleFixedNew.TextAlign = TextAlignEnum.CenterCenter;
            c1FlxGrdChrMsgObl.Select(0, 0);
            rdrMsg.Close();*/
        }

        private void RecTimeRptChrono(int iSingId)
        {
            string strColName = "";
            string strColId = "";

            string strTime = "00.00.0000. 00:00:00";
            switch (iSingId)
            {
                case 1:
                    strTime = c1DatePdSt.Text;
                    strColName = "¬вести врем€ подачи ствола" + "\r\n" + strTime + "?";
                    break;
                case 2:
                    strTime = c1DateLoc.Text;
                    strColName = "¬вести врем€ локализации" + "\r\n" + strTime + "?";
                    break;
                case 3:
                    strTime = c1DateLikv.Text;
                    strColName = "¬вести врем€ ликвидации" + "\r\n" + strTime + "?";
                    break;
            }
            bool blRecTime = false;
            if (MessageBox.Show(strColName, "¬вод времени событи€",
                        MessageBoxButtons.YesNo, MessageBoxIcon.Question)
                        == DialogResult.Yes)
            {
                switch (iSingId)
                {
                    case 1: //врем€ подачи ствола
                        strColId = "RptTimeBarrel";
                        break;
                    case 2: //врем€ локализации
                        strColId = "RptTimeLocal";
                        break;
                    case 3: //врем€ ликвидации
                        strColId = "RptTimeFireSupp";
                        break;
                }
                blRecTime = true;
            }
            if (blRecTime)
            {
                //DateTime myDateTime = DateTime.Now;
                //string strTime = myDateTime.ToString("dd.MM.yyyy HH:mm:ss");
                string strSqlTextChrTime = "UPDATE ReportFireObl SET " + strColId + " = CONVERT(DATETIME, '" + strTime + "', 103) " +
                                                "WHERE RptDispId = " + strRptDispId;
                SqlCommand command = new SqlCommand(strSqlTextChrTime, FrmMain.Instance.m_cnn);
                Int32 recordsAffected = command.ExecuteNonQuery();
            }

        }

        private void c1CmbChrListObl_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Return)
            {
                e.Handled = true;
                DateTime myDateTime = DateTime.Now;
                string strTime = myDateTime.ToString("dd.MM.yyyy HH:mm:ss");
                object[] items = { strTime, c1CmbChrListObl.Text };
                c1FlxGrdChrMsgObl.AddItem(items, 1, 3);
                string strSqlTextMsg = "INSERT INTO ReportFireStat " +
                            "(RptFireStatId, RptDispId, RptFireStatDate, RptFireStatTxt) " +
                            "Values('0', '" + strRptDispId + "', CONVERT(DATETIME, '" + strTime + "', 103), '" + c1CmbChrListObl.Text + "')";
                //CONVERT(DATETIME, '" + strTime + "', 103) иначе мен€ет местами мес€ц и день при записи в SQL сервер
                SqlCommand command = new SqlCommand(strSqlTextMsg, FrmMain.Instance.m_cnn);
                Int32 recordsAffected = command.ExecuteNonQuery();
            }
        }

        private void c1FlxGrdChrMsgObl_AfterEdit(object sender, RowColEventArgs e)
        {
            string strRptStatId = c1FlxGrdChrMsgObl.GetDataDisplay(e.Row, 1);
            string strTime = c1FlxGrdChrMsgObl.GetDataDisplay(e.Row, 3);
            string strMsg = c1FlxGrdChrMsgObl.GetDataDisplay(e.Row, 4);
            string strSqlTextMsg = "UPDATE ReportFireStat SET RptFireStatDate = CONVERT(DATETIME, '" + strTime + "', 103), RptFireStatTxt = '" + strMsg + "' " +
                                            "WHERE RptFireStatId = " + strRptStatId;
            SqlCommand command = new SqlCommand(strSqlTextMsg, FrmMain.Instance.m_cnn);
            Int32 recordsAffected = command.ExecuteNonQuery();

            if (e.Col == 3)
                c1FlxGrdChrMsgObl.Sort(SortFlags.Descending, 3);
        }

        private void c1FlxGrdChrMsgObl_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Delete)
            {
                //e.Handled = true;
                int i = c1FlxGrdChrMsgObl.RowSel;
                string strDeadName = "”далить запись\r\n" + c1FlxGrdChrMsgObl.GetDataDisplay(i, 3) + " " + c1FlxGrdChrMsgObl.GetDataDisplay(i, 4) +
                    " из хронологии?";
                if (MessageBox.Show(strDeadName, "’ронологи€",
                    MessageBoxButtons.YesNo, MessageBoxIcon.Question)
                    == DialogResult.Yes)
                {
                    string strRptStatId = c1FlxGrdChrMsgObl.GetDataDisplay(i, 1);
                    string strSqlDelFireStat = "DELETE FROM ReportFireStat WHERE(RptFireStatId = '" + strRptStatId + "')";
                    SqlCommand cmnd = new SqlCommand(strSqlDelFireStat, FrmMain.Instance.m_cnn);
                    Int32 recordsAffectedDel = cmnd.ExecuteNonQuery();

                    c1FlxGrdChrMsgObl.Rows.Remove(i);
                }
            }
        }

        private void c1DatePdSt_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Return)
            {
                e.Handled = true;
                RecTimeRptChrono(1);
            }
        }

        private void c1DateLoc_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Return)
            {
                e.Handled = true;
                RecTimeRptChrono(2);
            }
        }

        private void c1DateLikv_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Return)
            {
                e.Handled = true;
                RecTimeRptChrono(3);
            }
        }

        
    }
}
