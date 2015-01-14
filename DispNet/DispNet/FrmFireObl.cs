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
    public partial class FrmFireObl : Form
    {
        static public FrmFireObl Instance;
        DataSet dsMsgDistrict = new DataSet();
        BindingSource bsMsgDistrict = new BindingSource();

        public FrmFireObl()
        {
            InitializeComponent();
        }

        private void FrmFireObl_Load(object sender, EventArgs e)
        {
            string strSqlTextMsgDistrict = "SELECT MsgDistrictText, MsgDistrictId FROM DispMsgDistrict ORDER BY MsgDistrictText";
            SqlDataAdapter daMsgDistrict = new SqlDataAdapter(strSqlTextMsgDistrict, FrmMain.Instance.m_cnn);
            daMsgDistrict.Fill(dsMsgDistrict, "DispMsgDistrict");
            bsMsgDistrict.DataSource = dsMsgDistrict;
            bsMsgDistrict.DataMember = "DispMsgDistrict";
            c1CmbDistrict.DataSource = bsMsgDistrict;
            c1CmbDistrict.ValueMember = "MsgDistrictText";
            c1CmbDistrict.DisplayMember = "MsgDistrictText";
            c1CmbDistrict.Columns[0].Caption = "Наименование района";
            c1CmbDistrict.Splits[0].DisplayColumns[1].Visible = false;
            c1CmbDistrict.Splits[0].DisplayColumns[0].HeadingStyle.HorizontalAlignment = C1.Win.C1List.AlignHorzEnum.Center;
            
        }

        private void LoadDispCity()
        {
            DataRow dr = dsMsgDistrict.Tables["DispMsgDistrict"].Rows[bsMsgDistrict.Position];
            string strDistrictId = dr["MsgDistrictId"].ToString();

            DataSet dsMsgCity = new DataSet();
            BindingSource bsMsgCity = new BindingSource();
            string strSqlTextMsgCity = "SELECT MsgCityText , MsgDistrictId FROM DispMsgCity " +
                                        " WHERE (MsgDistrictId = '" + strDistrictId + "') ORDER BY MsgCityText";
            SqlDataAdapter daMsgCity = new SqlDataAdapter(strSqlTextMsgCity, FrmMain.Instance.m_cnn);
            daMsgCity.Fill(dsMsgCity, "DispMsgCity");
            bsMsgCity.DataSource = dsMsgCity;
            bsMsgCity.DataMember = "DispMsgCity";
            c1CmbCity.DataSource = bsMsgCity;
            c1CmbCity.ValueMember = "MsgCityText";
            c1CmbCity.DisplayMember = "MsgCityText";
            c1CmbCity.Columns[0].Caption = "Наименование города(села)";
            c1CmbCity.Splits[0].DisplayColumns[1].Visible = false;
            c1CmbCity.Splits[0].DisplayColumns[0].HeadingStyle.HorizontalAlignment = C1.Win.C1List.AlignHorzEnum.Center;
            c1CmbCity.Focus();
        }

        private bool FindCmbTxtFireObl()
        {
            bool blOkFireObl = true;
            string strMsg = "";
            string strFindText = c1CmbDistrict.Text;
            if (c1CmbDistrict.FindStringExact(strFindText, 0, 0) == -1)
            {
                blOkFireObl = false;
                if (strFindText == "")
                    strMsg = "Необходимо заполнить поле Район!";
                else
                    strMsg = "В базе данных нет: " + strFindText + "!";
                MessageBox.Show(strMsg, "Выбор района выезда",
                    MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
            strFindText = c1CmbCity.Text;
            if (c1CmbCity.FindStringExact(strFindText, 0, 0) == -1)
            {
                blOkFireObl = false;
                if (strFindText == "")
                    strMsg = "Необходимо заполнить поле Пункт!";
                else
                    strMsg = "В базе данных нет: " + strFindText + "!";
                MessageBox.Show(strMsg, "Выбор пункта выезда",
                    MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }

            return blOkFireObl;
        }

        private void RecDataFireObl()
        {
            if (MessageBox.Show("Записать данные?", "Данные объекта",
                            MessageBoxButtons.YesNo, MessageBoxIcon.Question)
                            == DialogResult.Yes)
            {
                DateTime myDateTime = DateTime.Now;
                string strDate = myDateTime.Month.ToString() + "-" + myDateTime.Day.ToString() + "-" + myDateTime.Year.ToString() + " " +
                                    myDateTime.ToString("HH:mm:ss");

                string strAdressObl = txtBoxAdress.Text + " (" + c1CmbCity.Text + " " + c1CmbDistrict.Text + ")";
                
                int iFndRow = c1CmbDistrict.FindStringExact(c1CmbDistrict.Text, 0, 0);
                string strDistrict = c1CmbDistrict.Columns[1].CellText(iFndRow);

                iFndRow = c1CmbCity.FindStringExact(c1CmbCity.Text, 0, 0);
                string strCity = c1CmbCity.Columns[1].CellText(iFndRow);
                
                string strSqlTextReportDisp = "INSERT INTO ReportDisp " +
                "(RptDispId, RptDate, RptDispMsg, RptDistrict, RptCity, DepartTypeId, StateChsId, RptRecRpt) " +
                "Values('0', CONVERT(DATETIME, '" + strDate + "', 102), '" + strAdressObl + "', '" + strDistrict + "', '" + strCity + "', '2', '1', '0')";

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
                    string strRptDispId = iDispId.ToString();
                    string strSqlTextReportFire = "INSERT INTO ReportFireObl " +
                    "(RptFireOblId, RptDispId, RptRing, RptMsgFire) " +
                    "Values('0', '" + strRptDispId + "', '" + textBoxRingObl.Text + "', '" + txtBoxFireObl.Text + "')";

                    SqlCommand cmdOblId = new SqlCommand(strSqlTextReportFire, FrmMain.Instance.m_cnn);
                    Int32 rdsAffectedOblId = cmdOblId.ExecuteNonQuery();
                }   // дополнить сообщением если созданная запись не найдена

                Close();    // закрывает форму
            }
        }

        private void c1CmbDistrict_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Return)
            {
                e.Handled = true;
                LoadDispCity();
            }
        }

        private void c1CmbDistrict_Close(object sender, EventArgs e)
        {
            LoadDispCity();
        }

        private void c1CmbCity_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Return)
            {
                e.Handled = true;
                txtBoxAdress.Focus();
            }
        }

        private void txtBoxAdress_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Return)
            {
                e.Handled = true;
            }
        }

        private void textBoxRingObl_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Return)
            {
                e.Handled = true;
            }
        }

        private void btnRecFireObl_Click(object sender, EventArgs e)
        {
            if (FindCmbTxtFireObl())
                RecDataFireObl();
        }
                
    }
}