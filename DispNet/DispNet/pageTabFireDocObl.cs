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
    public partial class pageTabFireDocObl : UserControl
    {
        DataSet dsAreaFireList = new DataSet();
        BindingSource bsAreaFireList = new BindingSource();
        string strRptDispId;

        public pageTabFireDocObl()
        {
            InitializeComponent();
        }

        private void pageTabFireDocObl_Load(object sender, EventArgs e)
        {
            strRptDispId = FrmDispFire.Instance.m_strRptDispId;

            string strSqlTextDocMsg = "SELECT RptDispId, RptFireDoc, RptFireVer, RptFireNote, RptFireLoss " +
                                       "FROM ReportFireObl WHERE (RptDispId = " + strRptDispId + ")";
            SqlCommand cmd = new SqlCommand(strSqlTextDocMsg, FrmMain.Instance.m_cnn);
            SqlDataReader rdrMsgDocObl = cmd.ExecuteReader();

            while (rdrMsgDocObl.Read())
            {
                if (!rdrMsgDocObl.IsDBNull(1))
                    txtBoxDocObl.Text = rdrMsgDocObl.GetString(1);
                if (!rdrMsgDocObl.IsDBNull(2))
                    txtBoxVerObl.Text = rdrMsgDocObl.GetString(2);
                if (!rdrMsgDocObl.IsDBNull(3))
                    txtBoxNoteObl.Text = rdrMsgDocObl.GetString(3);
                if (!rdrMsgDocObl.IsDBNull(4))
                    txtBoxLossObl.Text = rdrMsgDocObl.GetString(4);
            }
            rdrMsgDocObl.Close();

            DataSet dsMsgDocObl = new DataSet();
            BindingSource bsMsgDocObl = new BindingSource();
            string strSqlTextMsgDocObl = "SELECT MsgDocText FROM DispMsgDoc ORDER BY MsgDocText";
            SqlDataAdapter daMsgDocObl = new SqlDataAdapter(strSqlTextMsgDocObl, FrmMain.Instance.m_cnn);
            daMsgDocObl.Fill(dsMsgDocObl, "DispMsgDoc");
            bsMsgDocObl.DataSource = dsMsgDocObl;
            bsMsgDocObl.DataMember = "DispMsgDoc";
            c1CmbDocObl.DataSource = bsMsgDocObl;
            c1CmbDocObl.ValueMember = "MsgDocText";
            c1CmbDocObl.DisplayMember = "MsgDocText";
            c1CmbDocObl.Columns[0].Caption = "Текст сообщения";
            c1CmbDocObl.Splits[0].DisplayColumns[0].HeadingStyle.HorizontalAlignment = C1.Win.C1List.AlignHorzEnum.Center;

            DataSet dsMsgVerObl = new DataSet();
            BindingSource bsMsgVerObl = new BindingSource();
            string strSqlTextMsgVerObl = "SELECT MsgVerText FROM DispMsgVer ORDER BY MsgVerText";
            SqlDataAdapter daMsgVerObl = new SqlDataAdapter(strSqlTextMsgVerObl, FrmMain.Instance.m_cnn);
            daMsgVerObl.Fill(dsMsgVerObl, "DispMsgVer");
            bsMsgVerObl.DataSource = dsMsgVerObl;
            bsMsgVerObl.DataMember = "DispMsgVer";
            c1CmbVerObl.DataSource = bsMsgVerObl;
            c1CmbVerObl.ValueMember = "MsgVerText";
            c1CmbVerObl.DisplayMember = "MsgVerText";
            c1CmbVerObl.Columns[0].Caption = "Текст сообщения";
            c1CmbVerObl.Splits[0].DisplayColumns[0].HeadingStyle.HorizontalAlignment = C1.Win.C1List.AlignHorzEnum.Center;

            AreaFireListLoad();
        }

        private void AreaFireListLoad()
        {
            string strSqlTextAreaFireList = "SELECT AreaFireId, AreaFire FROM AreaFireList ORDER BY AreaFire";
            SqlDataAdapter daAreaFireList = new SqlDataAdapter(strSqlTextAreaFireList, FrmMain.Instance.m_cnn);
            daAreaFireList.Fill(dsAreaFireList, "AreaFireList");

            bsAreaFireList.DataSource = dsAreaFireList;
            bsAreaFireList.DataMember = "AreaFireList";
            c1CmbAreaFireObl.DataSource = bsAreaFireList;
            c1CmbAreaFireObl.ValueMember = "AreaFire";
            c1CmbAreaFireObl.DisplayMember = "AreaFire";

            c1CmbAreaFireObl.Columns.RemoveAt(0);
            c1CmbAreaFireObl.Columns[0].Caption = "Едн.изм";
            c1CmbAreaFireObl.Splits[0].DisplayColumns[0].HeadingStyle.HorizontalAlignment = C1.Win.C1List.AlignHorzEnum.Center;
            c1CmbAreaFireObl.Splits[0].DisplayColumns[0].Width = 50;

            string strSqlTextLoadData = "SELECT ReportFireObl.RptAreaFire, AreaFireList.AreaFire, ReportFireObl.RptAreaFireInfo, " +
                "ReportFireObl.AreaFireId, ReportFireObl.RptDispId " +
                "FROM ReportFireObl INNER JOIN AreaFireList ON ReportFireObl.AreaFireId = AreaFireList.AreaFireId " +
                "WHERE (ReportFireObl.RptDispId = " + strRptDispId + ") ORDER BY ReportFireObl.RptFireOblId";
            SqlCommand cmd = new SqlCommand(strSqlTextLoadData, FrmMain.Instance.m_cnn);
            SqlDataReader rdr = cmd.ExecuteReader();

            while (rdr.Read())
            {
                if (!rdr.IsDBNull(0))
                    c1TxtBoxAreaObl.Text = rdr.GetDouble(0).ToString();
                if (!rdr.IsDBNull(1))
                    c1CmbAreaFireObl.Text = rdr.GetString(1);
                if (!rdr.IsDBNull(2))
                    txtBoxFireInfoObl.Text = rdr.GetString(2);
            }
            rdr.Close();
        }

        private void c1CmbDocObl_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Return)
            {
                e.Handled = true;
                txtBoxDocObl.Text = txtBoxDocObl.Text + c1CmbDocObl.Text + " ";
                c1CmbDocObl.Text = "";
            }
        }

        private void c1CmbVerObl_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Return)
            {
                e.Handled = true;
                txtBoxVerObl.Text = txtBoxVerObl.Text + c1CmbVerObl.Text + " ";
                c1CmbVerObl.Text = "";
            }
        }

        
        private void btnRecDocObl_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Записать информацию?", "Документы по пожару",
                    MessageBoxButtons.YesNo, MessageBoxIcon.Question)
                    == DialogResult.Yes)
            {
                DataRow dr = dsAreaFireList.Tables["AreaFireList"].Rows[bsAreaFireList.Position];
                string strAreaFireId = dr["AreaFireId"].ToString();

                string strSqlTextRecData = "UPDATE ReportFireObl SET RptFireVer = '" + txtBoxVerObl.Text + "', RptFireDoc = '" + txtBoxDocObl.Text + "', " +
                    "RptAreaFire = '" + c1TxtBoxAreaObl.Text + "', AreaFireId = '" + strAreaFireId + "', RptAreaFireInfo = '" + txtBoxFireInfoObl.Text + "', " +
                    "RptFireLoss = '" + txtBoxLossObl.Text + "', RptFireNote = '" + txtBoxNoteObl.Text + "'" +
                    "WHERE RptDispId = " + strRptDispId;
                SqlCommand command = new SqlCommand(strSqlTextRecData, FrmMain.Instance.m_cnn);
                Int32 recordsAffected = command.ExecuteNonQuery();
            }
        }

        private void c1CmbVerObl_Close(object sender, EventArgs e)
        {
            txtBoxVerObl.Text = txtBoxVerObl.Text + c1CmbVerObl.Text + " ";
        }

        private void c1CmbDocObl_Close(object sender, EventArgs e)
        {
            txtBoxDocObl.Text = txtBoxDocObl.Text + c1CmbDocObl.Text + " ";
        }
    }


}
