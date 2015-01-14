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
    public partial class pageTabChronology : UserControl
    {
        DataSet dsMsgList = new DataSet();
        BindingSource bsMsgList = new BindingSource();
        string strRptDispId;
        
        public pageTabChronology()
        {
            InitializeComponent();
        }

        private void pageTabChronology_Load(object sender, EventArgs e)
        {
            string strSqlTextMsgList = "SELECT RptChrMsgText FROM ReportChronoMsg ORDER BY RptChrMsgText";
            SqlDataAdapter daMsgList = new SqlDataAdapter(strSqlTextMsgList, FrmMain.Instance.m_cnn);
            daMsgList.Fill(dsMsgList, "ReportChronoMsg");

            bsMsgList.DataSource = dsMsgList;
            bsMsgList.DataMember = "ReportChronoMsg";
            c1CmbChrList.DataSource = bsMsgList;
            c1CmbChrList.ValueMember = "RptChrMsgText";
            c1CmbChrList.DisplayMember = "RptChrMsgText";

            c1CmbChrList.Columns[0].Caption = "Текст сообщения";
            c1CmbChrList.Splits[0].DisplayColumns[0].HeadingStyle.HorizontalAlignment = C1.Win.C1List.AlignHorzEnum.Center;

            strRptDispId = FrmDispFire.Instance.m_strRptDispId;
            LoadDataChronoTime(strRptDispId);

            c1FlxGrdChrTime.Select(0, 0);

            LoadDataMsgList();
        }

        private void btnTest_Click(object sender, EventArgs e)
        {
            DateTime myDateTime = DateTime.Now;
            //string strTime = myDateTime.Month.ToString() + "-" + myDateTime.Day.ToString() + "-" + myDateTime.Year.ToString() + " " +
                                    //myDateTime.ToString("HH:mm:ss");
            string strTime = myDateTime.ToString("dd.MM.yyyy HH:mm:ss");
            object[] items = { strTime, "col2" };
            c1FlxGrdChrMsg.AddItem(items, 1, 3);
            //c1FlxGrdChrMsg.AddItem(strTime, 1);
        }

        private void LoadDataMsgList()
        {
            //string strDate = "08.06.2007 8:30:00";
            DataSet dsFireStat = new DataSet();
            string strSqlTextChrMsg = "SELECT RptFireStatId, RptDispId, RptFireStatDate, RptFireStatTxt " +
                                       "FROM ReportFireStat WHERE (RptDispId = " + strRptDispId + ") " +
                                       //"AND RptFireStatDate < CONVERT(DATETIME, '" + strDate + "', 103) " +
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
                c1FlxGrdChrMsg.AddItem(items, 1, 1);
            }
            rdrMsg.Close();
            c1FlxGrdChrMsg.Sort(SortFlags.Descending, 3);
            c1FlxGrdChrMsg.Cols[3].EditMask = "##-##-#### ##:##:##";
            /*SqlDataAdapter daFireStat = new SqlDataAdapter(strSqlTextChrMsg, FrmMain.Instance.m_cnn);
            daFireStat.Fill(dsFireStat, "ReportFireStat");
            c1FlxGrdChrMsg.SetDataBinding(dsFireStat, "ReportFireStat");

            c1FlxGrdChrMsg.Cols[1].Visible = false;

            c1FlxGrdChrMsg.Cols[2].Format = "G";
            c1FlxGrdChrMsg.Cols[2].Width = 110;
            c1FlxGrdChrMsg.Cols[2].Caption = "Дата и время";
            //c1FlxGrdChrMsg.Cols[2].AllowEditing = false;
            c1FlxGrdChrMsg.Cols[3].Width = 250;
            c1FlxGrdChrMsg.Cols[3].AllowEditing = false;
            c1FlxGrdChrMsg.Cols[3].Caption = "Наименование сообщения";

            CellStyle cs = c1FlxGrdChrMsg.Styles.Add("s1");
            cs.Font = new Font("Arial", 9, FontStyle.Bold);
            cs.TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.CenterCenter;
            c1FlxGrdChrMsg.Rows[0].Style = cs;
            */
            /*SqlCommand cmd = new SqlCommand(strSqlTextChrMsg, FrmMain.Instance.m_cnn);
            SqlDataReader rdrMsg = cmd.ExecuteReader();

            string[] strMsgList;
            _dt = new DataTable();
            _dt.Columns.Add("Id", typeof(int));
            _dt.Columns.Add("Время", typeof(string));
            _dt.Columns.Add("Сообщение", typeof(string));
            while (rdrMsg.Read())
            {
                if (rdrMsg.IsDBNull(1))
                {
                    c1FlxGrdChrMsg.DataSource = _dt;
                    strChrMsg = "";
                    c1FlxGrdChrMsg.Cols[1].Visible = false;
                }
                else
                {
                    strChrMsg = rdrMsg.GetString(1);
                    strMsgList = strChrMsg.Split('|');
                    object[] data = new object[3];

                    for (int i = 0; i < strMsgList.Length; i++)
                    {
                        data[0] = i;
                        int lastLocation = strMsgList[i].IndexOf("#");
                        if (lastLocation > 0)
                        {
                            data[1] = strMsgList[i].Substring(0, lastLocation);
                            data[2] = strMsgList[i].Substring(lastLocation + 1);
                        }
                        _dt.Rows.Add(data);
                    }
                    _dt.AcceptChanges();
                    c1FlxGrdChrMsg.DataSource = _dt;
                    c1FlxGrdChrMsg.Sort(SortFlags.Descending, 1);
                    c1FlxGrdChrMsg.Cols[1].Visible = false;
                    c1FlxGrdChrMsg.Select(1, 2);
                    c1FlxGrdChrMsg.Cols[2].EditMask = "##:##:##";

                }
            }
            if (strChrMsg == "")
            {
                c1FlxGrdChrMsg.DataSource = _dt;
                c1FlxGrdChrMsg.Cols[1].Visible = false;
            }
            c1FlxGrdChrMsg.Cols[3].StyleFixedNew.TextAlign = TextAlignEnum.CenterCenter;
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
                    strColName = "Ввести время подачи ствола" + "\r\n" + strTime + "?";
                    break;
                case 2:
                    strTime = c1DateLoc.Text;
                    strColName = "Ввести время локализации" + "\r\n" + strTime + "?";
                    break;
                case 3:
                    strTime = c1DateLikv.Text;
                    strColName = "Ввести время ликвидации" + "\r\n" + strTime + "?";
                    break;
            }
            bool blRecTime = false;
            if (MessageBox.Show(strColName, "Ввод времени события",
                        MessageBoxButtons.YesNo, MessageBoxIcon.Question)
                        == DialogResult.Yes)
            {
                switch (iSingId)
                {
                    case 1: //время подачи ствола
                        strColId = "RptTimeBarrel";
                        break;
                    case 2: //время локализации
                        strColId = "RptTimeLocal";
                        break;
                    case 3: //время ликвидации
                        strColId = "RptTimeFireSupp";
                        break;
                }
                blRecTime = true;
            }
            if (blRecTime)
            {
                //DateTime myDateTime = DateTime.Now;
                //string strTime = myDateTime.ToString("dd.MM.yyyy HH:mm:ss");
                string strSqlTextChrTime = "UPDATE ReportFire SET " + strColId + " = CONVERT(DATETIME, '" + strTime + "', 103) " +
                                                "WHERE RptDispId = " + strRptDispId;
                SqlCommand command = new SqlCommand(strSqlTextChrTime, FrmMain.Instance.m_cnn);
                Int32 recordsAffected = command.ExecuteNonQuery();
            }
            
        }

        private void LoadDataChronoTime(string strRptDispId)
        {
            string strSqlTextChrTime = "SELECT RptDispId, RptTimeBarrel, RptTimeLocal, RptTimeFireSupp " +
                                       "FROM ReportFire WHERE (RptDispId = " + strRptDispId + ")";
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
        
        private void c1CmbChrList_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Return)
            {
                e.Handled = true;
                DateTime myDateTime = DateTime.Now;
                string strTime = myDateTime.ToString("dd.MM.yyyy HH:mm:ss");
                object[] items = { strTime, c1CmbChrList.Text };
                c1FlxGrdChrMsg.AddItem(items, 1, 3);
                string strSqlTextMsg = "INSERT INTO ReportFireStat " +
                            "(RptFireStatId, RptDispId, RptFireStatDate, RptFireStatTxt) " +
                            "Values('0', '" + strRptDispId + "', CONVERT(DATETIME, '" + strTime + "', 103), '" + c1CmbChrList.Text + "')";
                //CONVERT(DATETIME, '" + strTime + "', 103) иначе меняет местами месяц и день при записи в SQL сервер
                SqlCommand command = new SqlCommand(strSqlTextMsg, FrmMain.Instance.m_cnn);
                Int32 recordsAffected = command.ExecuteNonQuery();
            }
        }

        private void c1FlxGrdChrMsg_AfterEdit(object sender, RowColEventArgs e)
        {
            string strRptStatId = c1FlxGrdChrMsg.GetDataDisplay(e.Row, 1);
            string strTime = c1FlxGrdChrMsg.GetDataDisplay(e.Row, 3);
            string strMsg = c1FlxGrdChrMsg.GetDataDisplay(e.Row, 4);
            string strSqlTextMsg = "UPDATE ReportFireStat SET RptFireStatDate = CONVERT(DATETIME, '" + strTime + "', 103), RptFireStatTxt = '" + strMsg + "' " +
                                            "WHERE RptFireStatId = " + strRptStatId;
            SqlCommand command = new SqlCommand(strSqlTextMsg, FrmMain.Instance.m_cnn);
            Int32 recordsAffected = command.ExecuteNonQuery();

            if(e.Col == 3)
                c1FlxGrdChrMsg.Sort(SortFlags.Descending, 3);
        }

        private void c1FlxGrdChrMsg_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Delete)
            {
                //e.Handled = true;
                int i = c1FlxGrdChrMsg.RowSel;
                string strDeadName = "Удалить запись\r\n" + c1FlxGrdChrMsg.GetDataDisplay(i, 3) + " " + c1FlxGrdChrMsg.GetDataDisplay(i, 4) +
                    " из хронологии?";
                if (MessageBox.Show(strDeadName, "Хронология",
                    MessageBoxButtons.YesNo, MessageBoxIcon.Question)
                    == DialogResult.Yes)
                {
                    string strRptStatId = c1FlxGrdChrMsg.GetDataDisplay(i, 1);
                    string strSqlDelFireStat = "DELETE FROM ReportFireStat WHERE(RptFireStatId = '" + strRptStatId + "')";
                    SqlCommand cmnd = new SqlCommand(strSqlDelFireStat, FrmMain.Instance.m_cnn);
                    Int32 recordsAffectedDel = cmnd.ExecuteNonQuery();

                    c1FlxGrdChrMsg.Rows.Remove(i);
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
