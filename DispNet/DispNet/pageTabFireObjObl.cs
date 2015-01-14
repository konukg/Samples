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
    public partial class pageTabFireObjObl : UserControl
    {
        string strRptDispId;
        DataSet dsMsgDistrict = new DataSet();
        BindingSource bsMsgDistrict = new BindingSource();
        int l_iRowSel = 0; //номер строки таблицы пожаров формы FrmDispFire для редактирования

        public pageTabFireObjObl()
        {
            InitializeComponent();
        }

        private void pageTabFireObjObl_Load(object sender, EventArgs e)
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
            
            strRptDispId = FrmDispFire.Instance.m_strRptDispId;
            l_iRowSel = FrmDispFire.Instance.m_iDispFireRowSel;

            string strSqlTextObjObl = "SELECT ReportDisp.RptDispId, ReportDisp.RptDate, ReportDisp.RptDispMsg, ReportDisp.RptDistrict, " +
                "ReportDisp.RptCity, ReportFireObl.RptRing " +
                "FROM ReportDisp INNER JOIN ReportFireObl ON ReportDisp.RptDispId = ReportFireObl.RptDispId " +
                 "WHERE (ReportDisp.RptDispId = " + strRptDispId + ")";
            SqlCommand cmdObjObl = new SqlCommand(strSqlTextObjObl, FrmMain.Instance.m_cnn);
            SqlDataReader rdrRptObjObl = cmdObjObl.ExecuteReader();
            string strDistrict = "";
            string strCity = "";
            while (rdrRptObjObl.Read())
            {
                if (!rdrRptObjObl.IsDBNull(1))
                    c1DatePermitObl.Text = rdrRptObjObl.GetDateTime(1).ToString("dd.MM.yyyy HH:mm:ss");
                if (!rdrRptObjObl.IsDBNull(2))
                    txtBoxAdress.Text = rdrRptObjObl.GetString(2);
                if (!rdrRptObjObl.IsDBNull(3))
                    strDistrict = rdrRptObjObl.GetString(3);
                if (!rdrRptObjObl.IsDBNull(4))
                    strCity = rdrRptObjObl.GetString(4);
                if (!rdrRptObjObl.IsDBNull(5))
                    textBoxRingObl.Text = rdrRptObjObl.GetString(5);
                
            }
            rdrRptObjObl.Close();

            int iFndRow = c1CmbDistrict.FindStringExact(strDistrict, 0, 1);
            c1CmbDistrict.Text = c1CmbDistrict.Columns[0].CellText(iFndRow);
            string strDistId = c1CmbDistrict.Columns[1].CellText(iFndRow);

            DataSet dsMsgCity = new DataSet();
            BindingSource bsMsgCity = new BindingSource();
            string strSqlTextMsgCity = "SELECT MsgCityText , MsgDistrictId FROM DispMsgCity " +
                                        " WHERE (MsgDistrictId = '" + strDistId + "') ORDER BY MsgCityText";
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

            iFndRow = c1CmbCity.FindStringExact(strCity, 0, 1);
            c1CmbCity.Text = c1CmbCity.Columns[0].CellText(iFndRow);
    
        }

        private void c1CmbDistrict_Close(object sender, EventArgs e)
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

        private void c1CmbDistrict_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Return)
            {
                e.Handled = true;
                c1CmbCity.Focus();
            }
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

        private void btnRecObjObl_Click(object sender, EventArgs e)
        {
            if (FindCmbTxtObjObl())
            {
                if (MessageBox.Show("Записать данные?", "Данные объекта",
                                MessageBoxButtons.YesNo, MessageBoxIcon.Question)
                                == DialogResult.Yes)
                {
                    string strDate = c1DatePermitObl.Text;
                    string strAdressObl = txtBoxAdress.Text + " (" + c1CmbCity.Text + " " + c1CmbDistrict.Text + ")";

                    int iFndRow = c1CmbDistrict.FindStringExact(c1CmbDistrict.Text, 0, 0);
                    string strDistrict = c1CmbDistrict.Columns[1].CellText(iFndRow);

                    iFndRow = c1CmbCity.FindStringExact(c1CmbCity.Text, 0, 0);
                    string strCity = c1CmbCity.Columns[1].CellText(iFndRow);
                    
                    if (FrmDispFire.Instance.Visible)
                        FrmDispFire.Instance.c1FlxGrdDispMain.SetData(l_iRowSel, 3, strAdressObl, true);   //вставляет в таблицу данные об объекте

                    string strSqlTextDisp = "UPDATE ReportDisp SET RptDate = CONVERT(DATETIME, '" + strDate + "', 103), RptDispMsg = '" + strAdressObl + "', " +
                                                "RptDistrict = '" + strDistrict + "', RptCity = '" + strCity + "' " +
                                                    "WHERE RptDispId = " + strRptDispId;
                    SqlCommand commandDisp = new SqlCommand(strSqlTextDisp, FrmMain.Instance.m_cnn);
                    Int32 recordsAffectedDisp = commandDisp.ExecuteNonQuery();

                    string strSqlTextFire = "UPDATE ReportFire SET RptRing = '" + textBoxRingObl.Text + "' WHERE RptDispId = " + strRptDispId;
                    SqlCommand commandFire = new SqlCommand(strSqlTextFire, FrmMain.Instance.m_cnn);
                    Int32 recordsAffectedFire = commandFire.ExecuteNonQuery();
                }
            }
        }

        private bool FindCmbTxtObjObl()
        {
            bool blOkObjObl = true;
            string strMsg = "";
            string strFindText = c1CmbDistrict.Text;
            if (c1CmbDistrict.FindStringExact(strFindText, 0, 0) == -1)
            {
                blOkObjObl = false;
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
                blOkObjObl = false;
                if (strFindText == "")
                    strMsg = "Необходимо заполнить поле Пункт!";
                else
                    strMsg = "В базе данных нет: " + strFindText + "!";
                MessageBox.Show(strMsg, "Выбор пункта выезда",
                    MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }

            return blOkObjObl;
        }
    }
}
