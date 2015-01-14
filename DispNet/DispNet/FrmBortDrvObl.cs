using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Threading;

namespace DispNet
{
    public partial class FrmBortDrvObl : Form
    {
        static public FrmBortDrvObl Instance;
        DataSet dsMsgDistrict = new DataSet();
        BindingSource bsMsgDistrict = new BindingSource();

        int l_iRowSel = 0; //номер строки таблицы пожаров формы FrmDispFire для редактирования
        int l_iDrvSign = 0;  //признак в категории для чтения
        int l_iCatDrvId = 0; //категория 0-нет, 1-занятие, 2 - дозор, 3 - прочие
        static public string m_strCatSingDrv = "0"; // Id категория признака для выбора и записи для нескольких открытых окон обязательно static
        string l_strCatSingDrv = "0"; //обязательно копировать если открыты несколько окон иначе во всех бедет последнее значение m_strCatSingDrv

        bool blCatRsLrn = false;  //
        bool blCatRsPtr = false;   // загружает данные категорий один раз пр первом нажатие кнопки
        bool blCatRsOth = false;  //

        DataSet dsCtgDrvList = new DataSet();
        BindingSource bsCtgDrvList = new BindingSource();

        // запуск формы из разных потоков
        private Thread selThread = null;
        delegate void SetCtgCallback(int iCatDrv);
        //.....

        //для запуска формы из диспетчерского журнала
        string strRptDispId;
        public int m_iLoadDispRpt = 0; //1-загружает форму для ввода данных, 2-загружает форму из дисп. журнала

        public FrmBortDrvObl()
        {
            InitializeComponent();
        }

        private void FrmBortDrvObl_Load(object sender, EventArgs e)
        {
            string strSqlTextMsgDistrict = "SELECT MsgDistrictText, MsgDistrictId FROM DispMsgDistrict ORDER BY MsgDistrictText";
            SqlDataAdapter daMsgDistrict = new SqlDataAdapter(strSqlTextMsgDistrict, FrmMain.Instance.m_cnn);
            daMsgDistrict.Fill(dsMsgDistrict, "DispMsgDistrict");
            bsMsgDistrict.DataSource = dsMsgDistrict;
            bsMsgDistrict.DataMember = "DispMsgDistrict";
            c1CmbDistrict.DataSource = bsMsgDistrict;
            c1CmbDistrict.ValueMember = "MsgDistrictText";
            c1CmbDistrict.DisplayMember = "MsgDistrictText";
            c1CmbDistrict.Splits[0].DisplayColumns[1].Visible = false;
            c1CmbDistrict.Columns[0].Caption = "Наименование района";
            c1CmbDistrict.Splits[0].DisplayColumns[0].HeadingStyle.HorizontalAlignment = C1.Win.C1List.AlignHorzEnum.Center;
            
            DataSet dsPchObl = new DataSet();
            BindingSource bsPchObl = new BindingSource();
            string strSqlTextPchObl = "SELECT UnitName, UnitId, UnitTypeId FROM Unit " +
                                        "WHERE (UnitTypeId = 2)ORDER BY UnitId";
            SqlDataAdapter daPchObl = new SqlDataAdapter(strSqlTextPchObl, FrmMain.Instance.m_cnn);
            daPchObl.Fill(dsPchObl, "Unit");
            bsPchObl.DataSource = dsPchObl;
            bsPchObl.DataMember = "Unit";
            c1CmbPchObl.DataSource = bsPchObl;
            c1CmbPchObl.ValueMember = "UnitName";
            c1CmbPchObl.DisplayMember = "UnitName";
            c1CmbPchObl.Splits[0].DisplayColumns[1].Visible = false;
            c1CmbPchObl.Splits[0].DisplayColumns[2].Visible = false;
            c1CmbPchObl.Columns[0].Caption = "Подразделение";
            c1CmbPchObl.Splits[0].DisplayColumns[0].HeadingStyle.HorizontalAlignment = C1.Win.C1List.AlignHorzEnum.Center;

            m_lblCatDrv.Text = "Выезд техники без категории";

            if (m_iLoadDispRpt == 2)
                DispBortDrvObl_Load();
            else
                m_iLoadDispRpt = 1;

            ThreadStartFrmDrvObl();  // старт потока для вызова из данного потока формы с перечнем категорий выезда            
        }

        private void DispBortDrvObl_Load()
        {
            strRptDispId = FrmDispFire.Instance.m_strRptDispId;
            l_iRowSel = FrmDispFire.Instance.m_iDispFireRowSel;

            string strSqlTextOblDrv = "SELECT ReportDisp.RptDispId, ReportDisp.RptDispMsg, ReportDisp.RptDistrict, " +
                "ReportDisp.RptCity, ReportBortDriveObl.UnitId, ReportBortDriveObl.RptBortDrvOblBort, " +
                "ReportBortDriveObl.RptBortDrvOblPlace, ReportBortDriveObl.RptBortDrvOblTake, ReportBortDriveObl.RptOblPtz, ReportBortDriveObl.RptOblPtu, " +
                "ReportBortDriveObl.RptDrvOblSign, ReportBortDriveObl.RptCatDrvOblId " +
                "FROM ReportDisp INNER JOIN ReportBortDriveObl ON ReportDisp.RptDispId = ReportBortDriveObl.RptDispId " +
                "WHERE (ReportDisp.RptDispId = " + strRptDispId + ")";
            SqlCommand cmd = new SqlCommand(strSqlTextOblDrv, FrmMain.Instance.m_cnn);
            SqlDataReader rdrOblDrv = cmd.ExecuteReader();

            string strDistrict = "";
            string strCity = "";
            string strPchObl = "";
            string strListBortObl = "";
            while (rdrOblDrv.Read())
            {
                //if (!rdrOblDrv.IsDBNull(1))
                    //m_lblCatDrv.Text = rdrOblDrv.GetString(1);
                if (!rdrOblDrv.IsDBNull(2))
                    strDistrict = rdrOblDrv.GetString(2);
                if (!rdrOblDrv.IsDBNull(3))
                    strCity = rdrOblDrv.GetString(3);
                if (!rdrOblDrv.IsDBNull(4))
                    strPchObl = rdrOblDrv.GetInt16(4).ToString();
                if (!rdrOblDrv.IsDBNull(5))
                    strListBortObl = rdrOblDrv.GetString(5);
                if (!rdrOblDrv.IsDBNull(6))
                    txtBoxPlaceObl.Text = rdrOblDrv.GetString(6);
                if (!rdrOblDrv.IsDBNull(7))
                    txtBoxTakeObl.Text = rdrOblDrv.GetString(7);
                if (!rdrOblDrv.IsDBNull(8))
                    if (rdrOblDrv.GetBoolean(8))
                        chkBoxPtzObl.CheckState = CheckState.Checked;
                if (!rdrOblDrv.IsDBNull(9))
                    if (rdrOblDrv.GetBoolean(9))
                        chkBoxPtuObl.CheckState = CheckState.Checked;
                if (!rdrOblDrv.IsDBNull(10))
                    l_iDrvSign = rdrOblDrv.GetInt16(10);
                if (!rdrOblDrv.IsDBNull(11))
                    l_iCatDrvId = rdrOblDrv.GetInt16(11);
            }
            rdrOblDrv.Close();
            
            l_strCatSingDrv = l_iDrvSign.ToString();    //назначает категорию если сделать запись без вызова выбора категории

            string[] strMsgList = strListBortObl.Split(',');
            for (int i = 0; i < strMsgList.Length; i++)
            {
                c1ListBortDrvObl.AddItem(strMsgList[i].ToString());
            }
            c1ListBortDrvObl.Sort(0, C1.Win.C1List.SortDirEnum.ASC);

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
            
            iFndRow = c1CmbPchObl.FindStringExact(strPchObl, 0, 1);
            c1CmbPchObl.Text = c1CmbPchObl.Columns[0].CellText(iFndRow);

            switch (l_iCatDrvId)
            {
                case 0:
                    rdBtnNoCatDrv.Checked = true;
                    m_lblCatDrv.Text = "Выезд техники без категории";
                    grpBoxType.Text = "";
                    break;
                case 1:
                    rdBtnDrvLrn.Checked = true;
                    string strSqlLrnCat = "SELECT MsgCatLrnId, MsgCatLrnText FROM InfoCatLrnDrvUkg WHERE (MsgCatLrnId = " + l_iDrvSign.ToString() + ")";
                    CtgMsgListLoad(strSqlLrnCat); //загружает текст категории из таблицы 
                    break;
                case 2:
                    rdBtnDrvPtr.Checked = true;
                    string strSqlPtrCat = "SELECT MsgCatPtrId, MsgCatPtrText FROM InfoCatPtrDrvUkg WHERE (MsgCatPtrId = " + l_iDrvSign.ToString() + ")";
                    CtgMsgListLoad(strSqlPtrCat);
                    break;
                case 3:
                    rdBtnDrvOth.Checked = true;
                    string strSqlOthCat = "SELECT MsgCatOthId, MsgCatOthText FROM InfoCatOthDrvUkg WHERE (MsgCatOthId = " + l_iDrvSign.ToString() + ")";
                    CtgMsgListLoad(strSqlOthCat);
                    break;
            }
            
        }

        private void CtgMsgListLoad(string strSql)
        {
            SqlCommand cmdCat = new SqlCommand(strSql, FrmMain.Instance.m_cnn);
            SqlDataReader rdrMsgCat = cmdCat.ExecuteReader();

            while (rdrMsgCat.Read())
            {
                if (!rdrMsgCat.IsDBNull(1))
                    m_lblCatDrv.Text = rdrMsgCat.GetString(1);
            }
            rdrMsgCat.Close();
        }

        private void c1CmbDistrict_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Return)
            {
                e.Handled = true;
                
            }
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
            c1CmbCity.Splits[0].DisplayColumns[0].HeadingStyle.HorizontalAlignment = C1.Win.C1List.AlignHorzEnum.Center;
            c1CmbCity.Splits[0].DisplayColumns[1].Visible = false;
            c1CmbCity.Focus();
        }

        private void c1CmbCity_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Return)
            {
                e.Handled = true;
                c1CmbPchObl.Focus();
            }
        }

        private void c1CmbPchObl_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Return)
            {
                e.Handled = true;
                
            }
        }

        private void c1CmbDrvObl_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Return)
            {
                e.Handled = true;
                txtBoxPlaceObl.Focus();
            }
        }

        private void txtBoxBortName_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Return)
            {
                e.Handled = true;
                string strBort = txtBoxBortName.Text;
                if (c1ListBortDrvObl.FindString(strBort) == -1)
                    c1ListBortDrvObl.AddItem(strBort);
                txtBoxBortName.Text = "";
            }
        }

        private void btnDelBortObl_Click(object sender, EventArgs e)
        {
            int iCnt = c1ListBortDrvObl.ListCount;
            int iCntAdd = iCnt - c1ListBortDrvObl.SelectedIndices.Count;
            string[] strN = new string[iCntAdd];
            int j = 0;

            for (int i = 0; i < iCnt; i = i + 1)
            {
                if (!c1ListBortDrvObl.GetSelected(i))
                {
                    strN[j] = c1ListBortDrvObl.GetItemText(i, 0);
                    j++;
                }

            }
            c1ListBortDrvObl.ClearItems();
            for (j = 0; j < iCntAdd; j = j + 1)
                c1ListBortDrvObl.AddItem(strN[j]);
        }

        private void btnRecDrvObl_Click(object sender, EventArgs e)
        {
            if (FindCmbTxtDriveObl())
            {
                if (MessageBox.Show("Записать информацию?", "Выезд техники из района",
                        MessageBoxButtons.YesNo, MessageBoxIcon.Question)
                        == DialogResult.Yes)
                {
                    if(m_iLoadDispRpt == 1)
                        RecReportBortDriveObl();
                    if (m_iLoadDispRpt == 2)
                        UpdateReportBortDriveObl();

                    c1CmbDistrict.Text = "";
                    c1CmbCity.Text = "";
                    c1CmbPchObl.Text = "";
                    txtBoxPlaceObl.Text = "";
                    txtBoxTakeObl.Text = "";
                    c1ListBortDrvObl.ClearItems();

                    Close();    // закрывает форму
                }
            }
        }

        private bool FindCmbTxtDriveObl()
        {
            bool blOkDriveObl = true;
            string strMsg = "";
            string strFindText = c1CmbDistrict.Text;
            if (c1CmbDistrict.FindStringExact(strFindText, 0, 0) == -1)
            {
                blOkDriveObl = false;
                if(strFindText == "")
                    strMsg = "Необходимо заполнить поле Район!";
                else
                    strMsg = "В базе данных нет: " + strFindText + "!";
                MessageBox.Show(strMsg, "Выбор района выезда",
                    MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
            strFindText = c1CmbCity.Text;
            if (c1CmbCity.FindStringExact(strFindText, 0, 0) == -1)
            {
                blOkDriveObl = false;
                if(strFindText == "")
                    strMsg = "Необходимо заполнить поле Пункт!";
                else
                    strMsg = "В базе данных нет: " + strFindText + "!";
                MessageBox.Show(strMsg, "Выбор пункта выезда",
                    MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }

            strFindText = c1CmbPchObl.Text;
            if (c1CmbPchObl.FindStringExact(strFindText, 0, 0) == -1)
            {
                blOkDriveObl = false;
                if (strFindText == "")
                    strMsg = "Необходимо заполнить поле П/часть!";
                else
                    strMsg = "В базе данных нет: " + strFindText + "!";
                MessageBox.Show(strMsg, "Выбор п/части выезда",
                    MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }

            return blOkDriveObl;
        }

        private void UpdateReportBortDriveObl()
        {
            string strAdressObl = m_lblCatDrv.Text + " (" + c1CmbCity.Text + ", " + c1CmbDistrict.Text + ")";

            if (FrmDispFire.Instance.Visible)
            {
                FrmDispFire.Instance.c1FlxGrdDispMain.Cols[3].AllowEditing = true;
                FrmDispFire.Instance.c1FlxGrdDispMain.SetData(l_iRowSel, 3, strAdressObl, true);   //вставляет в таблицу данные об объекте
                FrmDispFire.Instance.c1FlxGrdDispMain.Cols[3].AllowEditing = false;
            }

            int iFndRow = c1CmbDistrict.FindStringExact(c1CmbDistrict.Text, 0, 0);
            string strDistrict = c1CmbDistrict.Columns[1].CellText(iFndRow);

            iFndRow = c1CmbCity.FindStringExact(c1CmbCity.Text, 0, 0);
            string strCity = c1CmbCity.Columns[1].CellText(iFndRow);

            string strSqlTextDrvDisp = "UPDATE ReportDisp SET RptDispMsg = '" + strAdressObl + "', " +
                "RptDistrict = '" + strDistrict + "', RptCity = '" + strCity + "' " +
                "WHERE RptDispId = " + strRptDispId;
            SqlCommand commandDisp = new SqlCommand(strSqlTextDrvDisp, FrmMain.Instance.m_cnn);
            Int32 recordsAffectedDisp = commandDisp.ExecuteNonQuery();

            string strBortName = "";
            c1ListBortDrvObl.Sort(0, C1.Win.C1List.SortDirEnum.ASC);
            int iCnt = c1ListBortDrvObl.ListCount;
            if (iCnt > 0)
            {
                for (int i = 0; i < iCnt; i = i + 1)
                {
                    string strName = c1ListBortDrvObl.GetItemText(i, 0);
                    if (iCnt == 1)
                        strBortName = strName;
                    else
                    {
                        if (i == 0)
                            strBortName = strName;
                        else
                            strBortName = strBortName + "," + strName;
                    }
                }
            }

            string strUkgPtz = "0";
            string strUkgPtu = "0";
            if (chkBoxPtzObl.Checked)
                strUkgPtz = "1";
            if (chkBoxPtuObl.Checked)
                strUkgPtu = "1";

            iFndRow = c1CmbPchObl.FindStringExact(c1CmbPchObl.Text, 0, 0);
            string strPchObl = c1CmbPchObl.Columns[1].CellText(iFndRow);

            string strSqlTextDrvDrive = "UPDATE ReportBortDriveObl SET RptBortDrvOblTake = '" + txtBoxTakeObl.Text + "', " +
                "RptBortDrvOblPlace = '" + txtBoxPlaceObl.Text + "', UnitId = '" + strPchObl + "', " +
                "RptOblPtz = '" + strUkgPtz + "', RptOblPtu = '" + strUkgPtu + "', RptBortDrvOblBort = '" + strBortName + "', " +
                "RptDrvOblSign = '" + l_strCatSingDrv + "', RptCatDrvOblId = '" + l_iCatDrvId + "' " +
                "WHERE RptDispId = " + strRptDispId;
            SqlCommand commandDrive = new SqlCommand(strSqlTextDrvDrive, FrmMain.Instance.m_cnn);
            Int32 recordsAffectedDrive = commandDrive.ExecuteNonQuery();                           
        }

        private void RecReportBortDriveObl()
        {
            DateTime myDateTime = DateTime.Now;
            string strDate = myDateTime.Month.ToString() + "-" + myDateTime.Day.ToString() + "-" + myDateTime.Year.ToString() + " " +
                                myDateTime.ToString("HH:mm:ss");
            string strAdressObl = m_lblCatDrv.Text +" (" + c1CmbCity.Text + ", " + c1CmbDistrict.Text + ")";

            int iFndRow = c1CmbDistrict.FindStringExact(c1CmbDistrict.Text, 0, 0);
            string strDistrict = c1CmbDistrict.Columns[1].CellText(iFndRow);

            iFndRow = c1CmbCity.FindStringExact(c1CmbCity.Text, 0, 0);
            string strCity = c1CmbCity.Columns[1].CellText(iFndRow);

            string strSqlTextReportDisp = "INSERT INTO ReportDisp " +
            "(RptDispId, RptDate, RptDispMsg, RptDistrict, RptCity, DepartTypeId, StateChsId, RptRecRpt) " +
            "Values('0', CONVERT(DATETIME, '" + strDate + "', 102), '" + strAdressObl + "', '" + strDistrict + "', '" + strCity + "', '4', '1', '0')";

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
                string strBortName = "";
                strRptDispId = iDispId.ToString();
                c1ListBortDrvObl.Sort(0, C1.Win.C1List.SortDirEnum.ASC);
                int iCnt = c1ListBortDrvObl.ListCount;
                if (iCnt > 0)
                {
                    for (int i = 0; i < iCnt; i = i + 1)
                    {
                        string strName = c1ListBortDrvObl.GetItemText(i, 0);
                        if (iCnt == 1)
                            strBortName = strName;
                        else
                        {
                            if (i == 0)
                                strBortName = strName;
                            else
                                strBortName = strBortName + "," + strName;
                        }
                    }
                }
                string strOblPtz = "0";
                string strOblPtu = "0";
                if (chkBoxPtzObl.Checked)
                    strOblPtz = "1";
                if (chkBoxPtuObl.Checked)
                    strOblPtu = "1";

                iFndRow = c1CmbPchObl.FindStringExact(c1CmbPchObl.Text, 0, 0);
                string strPchObl = c1CmbPchObl.Columns[1].CellText(iFndRow);

                string strSqlTextReportDrvObl = "INSERT INTO ReportBortDriveObl " +
                "(RptBortDrvOblId, RptDispId, UnitId, RptBortDrvOblBort, RptBortDrvOblPlace, RptBortDrvOblTake, RptOblPtz, RptOblPtu, " +
                "RptDrvOblSign, RptCatDrvOblId) " +
                "Values('0', '" + strRptDispId + "', '" + strPchObl + "', '" + strBortName + "'" +
                ", '" + txtBoxPlaceObl.Text + "', '" + txtBoxTakeObl.Text + "', '" + strOblPtz + "', '" + strOblPtu + "'" +
                ", '" + l_strCatSingDrv + "', '" + l_iCatDrvId + "')";

                SqlCommand cmdOblId = new SqlCommand(strSqlTextReportDrvObl, FrmMain.Instance.m_cnn);
                Int32 rdsAffectedOblId = cmdOblId.ExecuteNonQuery();
            
            }   // дополнить сообщением если созданная запись не найдена
          
        }

        private void chkBoxPtzObl_CheckedChanged(object sender, EventArgs e)
        {
            if (chkBoxPtzObl.Checked)
            {
                chkBoxPtuObl.CheckState = CheckState.Unchecked;
            }
        }

        private void chkBoxPtuObl_CheckedChanged(object sender, EventArgs e)
        {
            if (chkBoxPtuObl.Checked)
            {
                chkBoxPtzObl.CheckState = CheckState.Unchecked;
            }
        }

        private void rdBtnNoCatDrv_CheckedChanged(object sender, EventArgs e)
        {
            l_iCatDrvId = 0;
            m_lblCatDrv.Text = "Выезд техники без категории";
            m_strCatSingDrv = "0";
        }

        private void rdBtnDrvLrn_CheckedChanged(object sender, EventArgs e)
        {
            l_iCatDrvId = 1;
            m_lblCatDrv.Text = "";
            m_strCatSingDrv = "0";
        }

        private void rdBtnDrvPtr_CheckedChanged(object sender, EventArgs e)
        {
            l_iCatDrvId = 2;
            m_lblCatDrv.Text = "";
            m_strCatSingDrv = "0";
        }

        private void rdBtnDrvOth_CheckedChanged(object sender, EventArgs e)
        {
            l_iCatDrvId = 3;
            m_lblCatDrv.Text = "";
            m_strCatSingDrv = "0";
        }

        private void btnCatDrvChs_Click(object sender, EventArgs e)
        {
            ThreadLoadCtg(l_iCatDrvId);
        }

        private void ThreadStartFrmDrvObl()
        {
            selThread = new Thread(new ThreadStart(ThreadProcDrvObl));
            selThread.Start();
        }

        private void ThreadProcDrvObl()
        {
            ThreadLoadCtg(-1);
        }

        private void ThreadLoadCtg(int iCatDrv)
        {
            if (m_lblCatDrv.InvokeRequired)
            {
                SetCtgCallback d = new SetCtgCallback(ThreadLoadCtg);
                this.Invoke(d, new object[] { iCatDrv });
            }
            else
            {
                switch (iCatDrv)
                {
                    case 1:
                        CtgLrnListLoad(blCatRsLrn);
                        blCatRsLrn = true;
                        break;
                    case 2:
                        CtgPtrListLoad(blCatRsPtr);
                        blCatRsPtr = true;
                        break;
                    case 3:
                        CtgOthListLoad(blCatRsOth);
                        blCatRsOth = true;
                        break;
                }
            }
        }

        private void CtgLrnListLoad(bool blLoad)
        {
            if (!blLoad)
            {
                string strSqlTextCtgDrvList = "SELECT MsgCatLrnText, MsgCatLrnId FROM InfoCatLrnDrvUkg ORDER BY MsgCatLrnText";
                SqlDataAdapter daCtgDrvList = new SqlDataAdapter(strSqlTextCtgDrvList, FrmMain.Instance.m_cnn);
                daCtgDrvList.Fill(dsCtgDrvList, "InfoCatLrnDrvUkg");
                bsCtgDrvList.DataSource = dsCtgDrvList;
                bsCtgDrvList.DataMember = "InfoCatLrnDrvUkg";
            }

            FrmChsCatDrv.Instance = new FrmChsCatDrv();
            FrmChsCatDrv.Instance.Text = "Выбор категории выезда";
            FrmChsCatDrv.Instance.m_grpBoxCat.Text = "Занятия";
            FrmChsCatDrv.Instance.m_dsMsgCat = dsCtgDrvList.Copy();
            FrmChsCatDrv.Instance.m_bsMsgCat = new BindingSource(dsCtgDrvList, "InfoCatLrnDrvUkg");
            FrmChsCatDrv.Instance.m_strValDispMem = "MsgCatLrnText";
            FrmChsCatDrv.Instance.m_strMsgCatTable = "InfoCatLrnDrvUkg";
            FrmChsCatDrv.Instance.m_strMsgCatId = "MsgCatLrnId";
            FrmChsCatDrv.Instance.m_iSendMsgFrm = 5;    //категорию послать в эту форму
            FrmChsCatDrv.Instance.m_hndFrmCat = m_lblCatDrv.Handle;
            FrmChsCatDrv.Instance.ShowDialog();
            l_strCatSingDrv = m_strCatSingDrv;

        }

        private void CtgPtrListLoad(bool blLoad)
        {
            if (!blLoad)
            {
                string strSqlTextCtgDrvList = "SELECT MsgCatPtrText, MsgCatPtrId FROM InfoCatPtrDrvUkg ORDER BY MsgCatPtrText";
                SqlDataAdapter daCtgDrvList = new SqlDataAdapter(strSqlTextCtgDrvList, FrmMain.Instance.m_cnn);
                daCtgDrvList.Fill(dsCtgDrvList, "InfoCatPtrDrvUkg");
                bsCtgDrvList.DataSource = dsCtgDrvList;
                bsCtgDrvList.DataMember = "InfoCatPtrDrvUkg";
            }

            FrmChsCatDrv.Instance = new FrmChsCatDrv();
            FrmChsCatDrv.Instance.Text = "Выбор категории выезда";
            FrmChsCatDrv.Instance.m_grpBoxCat.Text = "Дозор";
            FrmChsCatDrv.Instance.m_dsMsgCat = dsCtgDrvList.Copy();
            FrmChsCatDrv.Instance.m_bsMsgCat = new BindingSource(dsCtgDrvList, "InfoCatPtrDrvUkg");
            FrmChsCatDrv.Instance.m_strValDispMem = "MsgCatPtrText";
            FrmChsCatDrv.Instance.m_strMsgCatTable = "InfoCatPtrDrvUkg";
            FrmChsCatDrv.Instance.m_strMsgCatId = "MsgCatPtrId";
            FrmChsCatDrv.Instance.m_iSendMsgFrm = 5;    //категорию послать в эту форму
            FrmChsCatDrv.Instance.m_hndFrmCat = m_lblCatDrv.Handle;
            FrmChsCatDrv.Instance.ShowDialog();
            l_strCatSingDrv = m_strCatSingDrv;

        }

        private void CtgOthListLoad(bool blLoad)
        {
            if (!blLoad)
            {
                string strSqlTextCtgDrvList = "SELECT MsgCatOthText, MsgCatOthId FROM InfoCatOthDrvUkg ORDER BY MsgCatOthText";
                SqlDataAdapter daCtgDrvList = new SqlDataAdapter(strSqlTextCtgDrvList, FrmMain.Instance.m_cnn);
                daCtgDrvList.Fill(dsCtgDrvList, "InfoCatOthDrvUkg");
                bsCtgDrvList.DataSource = dsCtgDrvList;
                bsCtgDrvList.DataMember = "InfoCatOthDrvUkg";
            }

            FrmChsCatDrv.Instance = new FrmChsCatDrv();
            FrmChsCatDrv.Instance.Text = "Выбор категории выезда";
            FrmChsCatDrv.Instance.m_grpBoxCat.Text = "Прочие";
            FrmChsCatDrv.Instance.m_dsMsgCat = dsCtgDrvList.Copy();
            FrmChsCatDrv.Instance.m_bsMsgCat = new BindingSource(dsCtgDrvList, "InfoCatOthDrvUkg");
            FrmChsCatDrv.Instance.m_strValDispMem = "MsgCatOthText";
            FrmChsCatDrv.Instance.m_strMsgCatTable = "InfoCatOthDrvUkg";
            FrmChsCatDrv.Instance.m_strMsgCatId = "MsgCatOthId";
            FrmChsCatDrv.Instance.m_iSendMsgFrm = 5;    //категорию послать в эту форму
            FrmChsCatDrv.Instance.m_hndFrmCat = m_lblCatDrv.Handle;
            FrmChsCatDrv.Instance.ShowDialog();
            l_strCatSingDrv = m_strCatSingDrv;

        }
    }
}