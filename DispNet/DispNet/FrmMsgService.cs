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
    public partial class FrmMsgService : Form
    {
        static public FrmMsgService Instance;

        //для запуска формы из диспетчерского журнала
        string strRptDispId;
        public int m_iLoadDispRpt = 0; //1-загружает форму для ввода данных, 2-загружает форму из дисп. журнала
        int l_iRowSel = 0; //номер строки таблицы пожаров формы FrmDispFire для редактирования

        public FrmMsgService()
        {
            InitializeComponent();
        }

        private void FrmMsgService_Load(object sender, EventArgs e)
        {
            DataSet dsMsgSrvName = new DataSet();
            BindingSource bsMsgSrvName = new BindingSource();
            string strSqlTextMsgSrvName = "SELECT MsgServiceName, MsgServiceId FROM DispMsgService ORDER BY MsgServiceName";
            SqlDataAdapter daMsgSrvName = new SqlDataAdapter(strSqlTextMsgSrvName, FrmMain.Instance.m_cnn);
            daMsgSrvName.Fill(dsMsgSrvName, "DispMsgService");
            bsMsgSrvName.DataSource = dsMsgSrvName;
            bsMsgSrvName.DataMember = "DispMsgService";
            c1CmbSrvName.DataSource = bsMsgSrvName;
            c1CmbSrvName.ValueMember = "MsgServiceName";
            c1CmbSrvName.DisplayMember = "MsgServiceName";
            c1CmbSrvName.Splits[0].DisplayColumns[1].Visible = false;
            c1CmbSrvName.Columns[0].Caption = "Наименование С/Службы";
            c1CmbSrvName.Splits[0].DisplayColumns[0].HeadingStyle.HorizontalAlignment = C1.Win.C1List.AlignHorzEnum.Center;

            if (m_iLoadDispRpt == 2)
                DispMsgService_Load();
            else
                m_iLoadDispRpt = 1;
            
        }

        private void DispMsgService_Load()
        {
            strRptDispId = FrmDispFire.Instance.m_strRptDispId;
            l_iRowSel = FrmDispFire.Instance.m_iDispFireRowSel;

            string strSqlTextMsgSrv = "SELECT RptDispId, MsgServiceId, RptMsgSrvPass, RptMsgSrvText " +
                "FROM ReportMsgService WHERE (RptDispId = " + strRptDispId + ")";
            SqlCommand cmd = new SqlCommand(strSqlTextMsgSrv, FrmMain.Instance.m_cnn);
            SqlDataReader rdrMsgSrv = cmd.ExecuteReader();

            while (rdrMsgSrv.Read())
            {
                if (!rdrMsgSrv.IsDBNull(1))
                {
                    string strSrvNameId = rdrMsgSrv.GetInt16(1).ToString();
                    int iFndRow = c1CmbSrvName.FindStringExact(strSrvNameId, 0, 1);
                    c1CmbSrvName.Text = c1CmbSrvName.Columns[0].CellText(iFndRow);
                }
                if (!rdrMsgSrv.IsDBNull(2))
                    txtBoxMsgPass.Text = rdrMsgSrv.GetString(2);
                if (!rdrMsgSrv.IsDBNull(3))
                    txtBoxMsgText.Text = rdrMsgSrv.GetString(3);
            }
            rdrMsgSrv.Close();
            
        }
        
        private void c1CmbSrvName_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Return)
            {
                e.Handled = true;
                txtBoxMsgPass.Focus();
            }
        }

        private void txtBoxMsgPass_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Return)
            {
                e.Handled = true;
                txtBoxMsgText.Focus();
            }
        }

        private void txtBoxMsgText_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Return)
            {
                e.Handled = true;
            }
        }

        private void btnRecMsgSrv_Click(object sender, EventArgs e)
        {
            if (c1CmbSrvName.Text == "")
            {
                MessageBox.Show("Введите название С/Службы!", "Сообщение от С/Службы",
                   MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
            else
            {
                if (MessageBox.Show("Записать информацию?", "Сообщение от С/Службы",
                        MessageBoxButtons.YesNo, MessageBoxIcon.Question)
                        == DialogResult.Yes)
                {
                    if (m_iLoadDispRpt == 1)
                        RecReportMsgSrv();
                    if (m_iLoadDispRpt == 2)
                        UpdateReportMsgSrv();

                    FrmMsgService.Instance.Close();
                }
            }
        }

        private void UpdateReportMsgSrv()
        {
            string strSrvName = c1CmbSrvName.Text;
            if (FrmDispFire.Instance.Visible)
                FrmDispFire.Instance.c1FlxGrdDispMain.SetData(l_iRowSel, 3, strSrvName, true);   //вставляет в таблицу данные об объекте
           
            string strSqlTextSrvNameDisp = "UPDATE ReportDisp SET RptDispMsg = '" + strSrvName +
                                        "' WHERE RptDispId = " + strRptDispId;
            SqlCommand commandDisp = new SqlCommand(strSqlTextSrvNameDisp, FrmMain.Instance.m_cnn);
            Int32 recordsAffectedDisp = commandDisp.ExecuteNonQuery();

            int iFndRow = c1CmbSrvName.FindStringExact(c1CmbSrvName.Text, 0, 0);
            string strSrvNameId = c1CmbSrvName.Columns[1].CellText(iFndRow);

            string strSqlTextSrvName = "UPDATE ReportMsgService SET MsgServiceId = '" + strSrvNameId + "', RptMsgSrvPass = '" + txtBoxMsgPass.Text + "', " +
                "RptMsgSrvText = '" + txtBoxMsgText.Text + "' WHERE RptDispId = " + strRptDispId;
            SqlCommand command = new SqlCommand(strSqlTextSrvName, FrmMain.Instance.m_cnn);
            Int32 recordsAffected = command.ExecuteNonQuery();
        }

        private void RecReportMsgSrv()
        {
            DateTime myDateTime = DateTime.Now;
            string strDate = myDateTime.Month.ToString() + "-" + myDateTime.Day.ToString() + "-" + myDateTime.Year.ToString() + " " +
                                myDateTime.ToString("HH:mm:ss");
            string strDispSrvName = c1CmbSrvName.Text;
            string strDistrict = "0";
            string strCity = "0";
            string strSqlTextReportDisp = "INSERT INTO ReportDisp " +
            "(RptDispId, RptDate, RptDispMsg, RptDistrict, RptCity, DepartTypeId, StateChsId, RptRecRpt) " +
            "Values('0', CONVERT(DATETIME, '" + strDate + "', 102), '" + strDispSrvName + "', '" + strDistrict + "', '" + strCity + "', '6', '7', '0')";

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
                strRptDispId = iDispId.ToString();

                int iFndRow = c1CmbSrvName.FindStringExact(c1CmbSrvName.Text, 0, 0);
                string strSrvNameId = c1CmbSrvName.Columns[1].CellText(iFndRow);

                string strSqlTextReportMsgSrv = "INSERT INTO ReportMsgService " +
                "(RptMsgSrvId, RptDispId, MsgServiceId, RptMsgSrvPass, RptMsgSrvText) " +
                "Values('0', '" + strRptDispId + "', '" + strSrvNameId + "', '" + txtBoxMsgPass.Text + "'" +
                ", '" + txtBoxMsgText.Text + "')";

                SqlCommand cmdOblId = new SqlCommand(strSqlTextReportMsgSrv, FrmMain.Instance.m_cnn);
                Int32 rdsAffectedOblId = cmdOblId.ExecuteNonQuery();

                c1CmbSrvName.Text = "";
                txtBoxMsgPass.Text = "";
                txtBoxMsgText.Text = "";
            }   // дополнить сообщением если созданная запись не найдена
        }
    }
}