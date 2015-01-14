using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace DispNet
{
    public partial class pageTabFireDoc : UserControl
    {
        string strRptDispId;
        DataSet dsAreaFireList = new DataSet();
        BindingSource bsAreaFireList = new BindingSource();

        public pageTabFireDoc()
        {
            InitializeComponent();
        }

        private void pageTabFireDoc_Load(object sender, EventArgs e)
        {
            strRptDispId = FrmDispFire.Instance.m_strRptDispId;

            string strSqlTextDocMsg = "SELECT RptDispId, RptFireDoc, RptFireVer, RptFireNote, RptFireLoss " +
                                       "FROM ReportFire WHERE (RptDispId = " + strRptDispId + ")";
            SqlCommand cmd = new SqlCommand(strSqlTextDocMsg, FrmMain.Instance.m_cnn);
            SqlDataReader rdrMsgDoc = cmd.ExecuteReader();
                        
            while (rdrMsgDoc.Read())
            {
                if (!rdrMsgDoc.IsDBNull(1))
                    txtBoxDoc.Text = rdrMsgDoc.GetString(1);
                if (!rdrMsgDoc.IsDBNull(2))
                    txtBoxVer.Text = rdrMsgDoc.GetString(2);
                if (!rdrMsgDoc.IsDBNull(3))
                    txtBoxNote.Text = rdrMsgDoc.GetString(3);
                if (!rdrMsgDoc.IsDBNull(4))
                    txtBoxLoss.Text = rdrMsgDoc.GetString(4);
            }
            rdrMsgDoc.Close();

            DataSet dsMsgDoc = new DataSet();
            BindingSource bsMsgDoc = new BindingSource();
            string strSqlTextMsgDoc = "SELECT MsgDocText FROM DispMsgDoc ORDER BY MsgDocText";
            SqlDataAdapter daMsgDoc = new SqlDataAdapter(strSqlTextMsgDoc, FrmMain.Instance.m_cnn);
            daMsgDoc.Fill(dsMsgDoc, "DispMsgDoc");
            bsMsgDoc.DataSource = dsMsgDoc;
            bsMsgDoc.DataMember = "DispMsgDoc";
            c1CmbDoc.DataSource = bsMsgDoc;
            c1CmbDoc.ValueMember = "MsgDocText";
            c1CmbDoc.DisplayMember = "MsgDocText";
            c1CmbDoc.Columns[0].Caption = "Текст сообщения";
            c1CmbDoc.Splits[0].DisplayColumns[0].HeadingStyle.HorizontalAlignment = C1.Win.C1List.AlignHorzEnum.Center;

            DataSet dsMsgVer = new DataSet();
            BindingSource bsMsgVer = new BindingSource();
            string strSqlTextMsgVer = "SELECT MsgVerText FROM DispMsgVer ORDER BY MsgVerText";
            SqlDataAdapter daMsgVer = new SqlDataAdapter(strSqlTextMsgVer, FrmMain.Instance.m_cnn);
            daMsgVer.Fill(dsMsgVer, "DispMsgVer");
            bsMsgVer.DataSource = dsMsgVer;
            bsMsgVer.DataMember = "DispMsgVer";
            c1CmbVer.DataSource = bsMsgVer;
            c1CmbVer.ValueMember = "MsgVerText";
            c1CmbVer.DisplayMember = "MsgVerText";
            c1CmbVer.Columns[0].Caption = "Текст сообщения";
            c1CmbVer.Splits[0].DisplayColumns[0].HeadingStyle.HorizontalAlignment = C1.Win.C1List.AlignHorzEnum.Center;

            AreaFireListLoad();
        }

        private void AreaFireListLoad()
        {
            string strSqlTextAreaFireList = "SELECT AreaFireId, AreaFire FROM AreaFireList ORDER BY AreaFire";
            SqlDataAdapter daAreaFireList = new SqlDataAdapter(strSqlTextAreaFireList, FrmMain.Instance.m_cnn);
            daAreaFireList.Fill(dsAreaFireList, "AreaFireList");

            bsAreaFireList.DataSource = dsAreaFireList;
            bsAreaFireList.DataMember = "AreaFireList";
            c1CmbAreaFire.DataSource = bsAreaFireList;
            c1CmbAreaFire.ValueMember = "AreaFire";
            c1CmbAreaFire.DisplayMember = "AreaFire";

            c1CmbAreaFire.Columns.RemoveAt(0);
            c1CmbAreaFire.Columns[0].Caption = "Едн.изм";
            c1CmbAreaFire.Splits[0].DisplayColumns[0].HeadingStyle.HorizontalAlignment = C1.Win.C1List.AlignHorzEnum.Center;
            c1CmbAreaFire.Splits[0].DisplayColumns[0].Width = 50;

            string strSqlTextLoadData = "SELECT ReportFire.RptAreaFire, AreaFireList.AreaFire, ReportFire.RptAreaFireInfo, " +
                "ReportFire.AreaFireId, ReportFire.RptDispId " +
                "FROM ReportFire INNER JOIN AreaFireList ON ReportFire.AreaFireId = AreaFireList.AreaFireId " +
                "WHERE (ReportFire.RptDispId = " + strRptDispId + ") ORDER BY ReportFire.RptFireId";
            SqlCommand cmd = new SqlCommand(strSqlTextLoadData, FrmMain.Instance.m_cnn);
            SqlDataReader rdr = cmd.ExecuteReader();

            while (rdr.Read())
            {
                if (!rdr.IsDBNull(0))
                    c1TxtBoxArea.Text = rdr.GetDouble(0).ToString();
                if (!rdr.IsDBNull(1))
                    c1CmbAreaFire.Text = rdr.GetString(1);
                if (!rdr.IsDBNull(2))
                    txtBoxFireInfo.Text = rdr.GetString(2);
            }
            rdr.Close();
        }

        /*private void RecDataDoc(int iCmb)
        {
            string strDocMsg;
            if (iCmb == 1)
            {
                strDocMsg = txtBoxDoc.Text + c1CmbDoc.Text + " ";
                txtBoxDoc.Text = strDocMsg;
            }
            else strDocMsg = txtBoxDoc.Text + " ";

            string strSqlTextMsg = "UPDATE ReportFire SET RptFireDoc = '" + strDocMsg + "' WHERE RptDispId = " + strRptDispId;
            SqlCommand command = new SqlCommand(strSqlTextMsg, FrmMain.Instance.m_cnn);
            Int32 recordsAffected = command.ExecuteNonQuery();
        }

        private void RecDataVer(int iCmb)
        {
            string strDocMsg;
            if (iCmb == 1)
            {
                strDocMsg = txtBoxVer.Text + c1CmbVer.Text + " ";
                txtBoxVer.Text = strDocMsg;
            }
            else strDocMsg = txtBoxVer.Text + " ";

            string strSqlTextMsg = "UPDATE ReportFire SET RptFireVer = '" + strDocMsg + "' WHERE RptDispId = " + strRptDispId;
            SqlCommand command = new SqlCommand(strSqlTextMsg, FrmMain.Instance.m_cnn);
            Int32 recordsAffected = command.ExecuteNonQuery();
        }*/

        private void c1CmbDoc_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Return)
            {
                e.Handled = true;
                //RecDataDoc(1);
                txtBoxDoc.Text = txtBoxDoc.Text + c1CmbDoc.Text + " ";
                c1CmbDoc.Text = "";
            }
        }

        private void c1CmbVer_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Return)
            {
                e.Handled = true;
                //RecDataVer(1);
                txtBoxVer.Text = txtBoxVer.Text + c1CmbVer.Text + " ";
                c1CmbVer.Text = "";
            }
        }

        private void txtBoxDoc_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Return)
            {
                /*ie.Handled = true;
                f (MessageBox.Show("Записать информацию?", "Документы по пожару",
                    MessageBoxButtons.YesNo, MessageBoxIcon.Question)
                    == DialogResult.Yes)
                {
                    RecDataDoc(0);
                }*/
            }
        }

        private void txtBoxVer_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Return)
            {
                /*e.Handled = true;
                if (MessageBox.Show("Записать информацию?", "Версия пожара",
                    MessageBoxButtons.YesNo, MessageBoxIcon.Question)
                    == DialogResult.Yes)
                {
                    RecDataVer(0);
                }*/
            }
        }

        private void txtBoxNote_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Return)
            {
                /*e.Handled = true;
                if (MessageBox.Show("Записать информацию?", "Примечание",
                    MessageBoxButtons.YesNo, MessageBoxIcon.Question)
                    == DialogResult.Yes)
                {
                    string strSqlTextMsg = "UPDATE ReportFire SET RptFireNote = '" + txtBoxNote.Text + "' WHERE RptDispId = " + strRptDispId;
                    SqlCommand command = new SqlCommand(strSqlTextMsg, FrmMain.Instance.m_cnn);
                    Int32 recordsAffected = command.ExecuteNonQuery();
                }*/
            }
        }

        private void txtBoxLoss_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Return)
            {
                /*e.Handled = true;
                if (MessageBox.Show("Записать информацию?", "Ущерб",
                    MessageBoxButtons.YesNo, MessageBoxIcon.Question)
                    == DialogResult.Yes)
                {
                    string strSqlTextLoss = "UPDATE ReportFire SET RptFireLoss = '" + txtBoxLoss.Text + "' WHERE RptDispId = " + strRptDispId;
                    SqlCommand command = new SqlCommand(strSqlTextLoss, FrmMain.Instance.m_cnn);
                    Int32 recordsAffected = command.ExecuteNonQuery();
                }*/
            }
        }

        private void btnRecDoc_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Записать информацию?", "Документы по пожару",
                    MessageBoxButtons.YesNo, MessageBoxIcon.Question)
                    == DialogResult.Yes)
            {
                DataRow dr = dsAreaFireList.Tables["AreaFireList"].Rows[bsAreaFireList.Position];
                string strAreaFireId = dr["AreaFireId"].ToString();

                string strSqlTextRecData = "UPDATE ReportFire SET RptFireVer = '" + txtBoxVer.Text + "', RptFireDoc = '" + txtBoxDoc.Text + "', " +
                    "RptAreaFire = '" + c1TxtBoxArea.Text + "', AreaFireId = '" + strAreaFireId + "', RptAreaFireInfo = '" + txtBoxFireInfo.Text + "', " +
                    "RptFireLoss = '" + txtBoxLoss.Text + "', RptFireNote = '" + txtBoxNote.Text + "'" +
                    "WHERE RptDispId = " + strRptDispId;
                SqlCommand command = new SqlCommand(strSqlTextRecData, FrmMain.Instance.m_cnn);
                Int32 recordsAffected = command.ExecuteNonQuery();
            }
        }

        private void c1CmbVer_Close(object sender, EventArgs e)
        {
            txtBoxVer.Text = txtBoxVer.Text + c1CmbVer.Text + " ";
        }

        private void c1CmbDoc_Close(object sender, EventArgs e)
        {
            txtBoxDoc.Text = txtBoxDoc.Text + c1CmbDoc.Text + " ";
        }
    }
}
