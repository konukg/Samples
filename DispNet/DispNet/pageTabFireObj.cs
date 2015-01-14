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
    public partial class pageTabFireObj : UserControl
    {
        string strRptDispId;
        DataSet dsStreetEdit = new DataSet();
        BindingSource bsStreetEdit;
        BindingSource bsHomeEdit = new BindingSource();
        public DataSet dsHomeSelEdit = new DataSet();
        BindingSource bsHomeSelEdit = new BindingSource();
        DataSet dsHomeFullRpt = new DataSet();
        string l_strStrId = "";  // Id улицы для поиска объекта по номеру дома
        public string m_strBuildId = ""; // Id номера дома может изменен если была вызвана форма FrmObjUkg
        int l_iRecSng = 0;  // в базе: 1-не найдена улица, 2-не найден номер дома
        int l_intStreetPos = 0; //позиция улицы если при закрытии c1ComboFnd не выбрана новая улица то форме путевки данные не меняются
        int l_iRowSel = 0; //номер строки таблицы пожаров формы FrmDispFire для редактирования

        DataSet dsHomeObj = new DataSet();
        BindingSource bsHomeObj = new BindingSource();
        
        public pageTabFireObj()
        {
            InitializeComponent();
        }

        private void btnTest_Click(object sender, EventArgs e)
        {
            bool blStWin = FrmDispFire.Instance.Visible;
      
            
        }

        private void pageTabFireObj_Load(object sender, EventArgs e)
        {
            strRptDispId = FrmDispFire.Instance.m_strRptDispId;
            l_iRowSel = FrmDispFire.Instance.m_iDispFireRowSel;

            dsStreetEdit = FrmPermit.Instance.dsStreet.Copy();
            bsStreetEdit = new BindingSource(dsStreetEdit, "Street");
            
            c1ComboStreet.DataSource = bsStreetEdit;
            c1ComboStreet.ValueMember = "StreetName";
            c1ComboStreet.DisplayMember = "StreetName";

            c1ComboStreet.Columns.RemoveAt(0);
            c1ComboStreet.Columns.RemoveAt(6);
            c1ComboStreet.Columns.RemoveAt(5);
            c1ComboStreet.Columns.RemoveAt(4);
            c1ComboStreet.Columns[0].Caption = "Улица";
            c1ComboStreet.Columns[1].Caption = "Тип";
            c1ComboStreet.Columns[2].Caption = "Район";
            c1ComboStreet.Columns[3].Caption = "Регион";

            c1ComboStreet.DropDownWidth = 400;
            // загрузка ранга пожара
            string[] strRang = new string[] { "1", "1,5", "2", "3", "4" };
            foreach (string i in strRang)
                c1ComboRang.AddItem(i);

            LoadDataFire(); //загрузка данных о пожаре
        }

        private void LoadDataFire()
        {
            string strSqlTextReportEdit = "SELECT ReportDisp.RptDispId, ReportDisp.RptDate, ReportDisp.RptDispMsg, ReportFire.StreetId, " +
                "ReportFire.BuildId, ReportFire.BuildHome, ReportFire.RptFireFlat, " +
                "ReportFire.RptRing, ReportFire.RptRingTel, ReportFire.RptMsgFire, ReportFire.RptRang, ReportFire.UnitId " +
                "FROM ReportDisp INNER JOIN ReportFire ON ReportDisp.RptDispId = ReportFire.RptDispId " +
                "WHERE (ReportDisp.RptDispId = " + strRptDispId + ")";

            SqlCommand cmdObj = new SqlCommand(strSqlTextReportEdit, FrmMain.Instance.m_cnn);
            SqlDataReader rdrRptObj = cmdObj.ExecuteReader();
            
            string strRptDispMsg = "";
            int iStreetId = -1;
            int iBuildId = -1;
            string strBuildHome = "";
            string strRptFireFlat = "";
            string strRptRing = "";
            string strRptRingTel = "";
            string strRptMsgFire = "";
            string strRptRang = "1";
            int iUnitId = -1;
            while (rdrRptObj.Read())
            {
                if (!rdrRptObj.IsDBNull(1))
                    c1DatePermit.Text = rdrRptObj.GetDateTime(1).ToString("dd.MM.yyyy HH:mm:ss");
                if (!rdrRptObj.IsDBNull(2))
                    strRptDispMsg = rdrRptObj.GetString(2);
                if (!rdrRptObj.IsDBNull(3))
                    iStreetId = rdrRptObj.GetInt32(3);
                if (!rdrRptObj.IsDBNull(4))
                    iBuildId = rdrRptObj.GetInt32(4);
                if (!rdrRptObj.IsDBNull(5))
                    strBuildHome = rdrRptObj.GetString(5);
                if (!rdrRptObj.IsDBNull(6))
                    strRptFireFlat = rdrRptObj.GetString(6);
                if (!rdrRptObj.IsDBNull(7))
                    strRptRing = rdrRptObj.GetString(7);
                if (!rdrRptObj.IsDBNull(8))
                    strRptRingTel = rdrRptObj.GetString(8);
                if (!rdrRptObj.IsDBNull(9))
                    strRptMsgFire = rdrRptObj.GetString(9);
                if (!rdrRptObj.IsDBNull(10))
                    strRptRang = rdrRptObj.GetString(10);
                if (!rdrRptObj.IsDBNull(11))
                    iUnitId = rdrRptObj.GetInt16(11);
            }
            rdrRptObj.Close();

            l_strStrId = iStreetId.ToString();  //Id улицы
            m_strBuildId = iBuildId.ToString(); //Id номера дома

            txtBoxRing.Text = strRptRing;
            txtBoxRingTel.Text = strRptRingTel;
            c1ComboFlat.Text = strRptFireFlat;
          
            if (iStreetId > 0)
            {
                DataView dvFndStreet = new DataView(dsStreetEdit.Tables["Street"], "StreetId = '" + iStreetId.ToString() + "'",
                            "StreetName", DataViewRowState.CurrentRows);
                int rowIndex = dvFndStreet.Count;   //.Find(c1ComboStreet.Text);
                if (rowIndex == 1)
                {
                    c1ComboStreet.Text = dvFndStreet[0]["StreetName"].ToString();
                    textBoxStreetType.Text = dvFndStreet[0]["StreetTypeName"].ToString();
                    textBoxStreetPlace.Text = dvFndStreet[0]["StreetPlaceName"].ToString() + " " + dvFndStreet[0]["StreetRegionName"].ToString();
                }
                c1ComboRang.Text = strRptRang;
            }
            else
            {
                c1ComboStreet.Text = strRptDispMsg;
                textBoxStreetType.Text = "Такой улицы нет!";
                // устанавливает ренг 1 если улица введена в ручную
                c1ComboRang.Text = "1";
            }
            
            dsHomeFullRpt = FrmPermit.Instance.dsHomeFull.Copy();
            if (iBuildId > 0)
            {
                LoadBuildHome(iStreetId.ToString());
                DataView dvHomeFullRpt = new DataView(dsHomeFullRpt.Tables["Buildings"], "StreetId = '" + iStreetId.ToString() + "'",
                                "BuildId", DataViewRowState.CurrentRows);
                int rowIndex = dvHomeFullRpt.Find(iBuildId.ToString());
                if (rowIndex != -1)
                    c1ComboHome.Text = dvHomeFullRpt[rowIndex]["BuildName"].ToString();
            }
            else
                c1ComboHome.Text = strBuildHome;

            string strSqlTextHomeObj = "SELECT ObjUkg.ObjId, ObjUkg.BuildId, ObjUkg.ObjName, ObjUkgStaff.OrgName " +
                "FROM ObjUkg INNER JOIN ObjUkgStaff ON ObjUkg.ObjStaffId = ObjUkgStaff.ObjStaffId " +
                "WHERE (BuildId = " + iBuildId + ")";
            SqlDataAdapter daHomeObj = new SqlDataAdapter(strSqlTextHomeObj, FrmMain.Instance.m_cnn);
            daHomeObj.Fill(dsHomeObj, "ObjUkg");
            bsHomeObj.DataSource = dsHomeObj;
            bsHomeObj.DataMember = "ObjUkg";
            if (bsHomeObj.Count > 0)
            {
                DataRow dr = dsHomeObj.Tables["ObjUkg"].Rows[bsHomeObj.Position];
                string strObjName = "";
                string strOrgName = "";
                strObjName = dr["ObjName"].ToString();
                strOrgName = dr["OrgName"].ToString();
                textBoxObj.Text = strObjName + " " + strOrgName;
            }
            else
                textBoxObj.Text = "Жилой дом";
        }

        private void LoadBuildHome(string strIndex)
        {
            DataSet dsHomeEdit = new DataSet();
            dsHomeEdit = FrmPermit.Instance.dsHome.Copy();
            bsHomeEdit = new BindingSource(dsHomeEdit, "Buildings"); ;
            bsHomeEdit.Filter = "StreetId = '" + strIndex + "'";
            c1ComboHome.DataSource = bsHomeEdit;
            c1ComboHome.ValueMember = "BuildName";
            c1ComboHome.DisplayMember = "BuildName";
            if(c1ComboHome.Columns.Count > 1)
                c1ComboHome.Columns.RemoveAt(1);
        }

        private void StreetComboFnd()
        {
            textBoxObj.Text = "";
            c1ComboHome.Text = "";
            l_strStrId = "";

            DataRow dr = dsStreetEdit.Tables["Street"].Rows[bsStreetEdit.Position];
            l_strStrId = dr["StreetId"].ToString();
            textBoxStreetType.Text = dr["StreetTypeName"].ToString();
            textBoxStreetPlace.Text = dr["StreetPlaceName"].ToString() + " " + dr["StreetRegionName"].ToString();
            DataView dvFndStreet = new DataView(dsStreetEdit.Tables["Street"], "StreetId = '" + l_strStrId + "'",
                            "StreetName", DataViewRowState.CurrentRows);
            int rowIndex = dvFndStreet.Find(c1ComboStreet.Text);
            if (rowIndex == -1)
            {
                l_iRecSng = 1;
                l_strStrId = "";   // улица в базе не найдена
                bsHomeEdit.Filter = "StreetId = '0'";
                c1ComboHome.DataSource = bsHomeEdit;
                c1ComboHome.ValueMember = "BuildName";
            }
            else
                LoadBuildHome(l_strStrId);

            l_intStreetPos = bsStreetEdit.Position; //запоминает позицию Street
            c1ComboHome.Focus();
        }

        private void c1ComboStreet_BeforeOpen(object sender, CancelEventArgs e)
        {
            l_intStreetPos = bsStreetEdit.Position; // запоминает текущую позицию улицы
        }

        private void c1ComboStreet_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Return)
            {
                e.Handled = true;
                if (l_intStreetPos != bsStreetEdit.Position)    //если было событие закрыть сомбо данные не меняются
                    StreetComboFnd();
                
            }
        }

        private void c1ComboStreet_Close(object sender, EventArgs e)
        {
            if (l_intStreetPos != bsStreetEdit.Position)    //если было событие закрыть сомбо данные не меняются
                StreetComboFnd();
        }

        private void c1ComboHome_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Return)
            {
                e.Handled = true;
            }
        }

        private void c1ComboHome_Close(object sender, EventArgs e)
        {
            textBoxObj.Text = "";
            string strSqlTextSelHome = "SELECT Buildings.BuildId, Buildings.StreetId, Buildings.BuildName, ObjUkg.ObjName, ObjUkgStaff.OrgName " +
            "FROM ((Buildings INNER JOIN ObjUkg ON Buildings.BuildId = ObjUkg.BuildId) " +
            "INNER JOIN ObjUkgStaff ON ObjUkg.ObjStaffId = ObjUkgStaff.ObjStaffId) " +
            "WHERE (((Buildings.StreetId)='" + l_strStrId + "') AND ((Buildings.BuildName)='" + c1ComboHome.Text + "')) " +
            "ORDER BY ObjUkg.ObjName";
            // выбор объекта по номеру дома - если есть
            SqlDataAdapter daHomeSelEdit = new SqlDataAdapter(strSqlTextSelHome, FrmMain.Instance.m_cnn);
            daHomeSelEdit.Fill(dsHomeSelEdit, "Buildings");
            bsHomeSelEdit.DataSource = dsHomeSelEdit;
            bsHomeSelEdit.DataMember = "Buildings";

            int iCnt = bsHomeSelEdit.Count;
            if (iCnt > 0)
            {
                string strStreetObj = textBoxStreetType.Text + " " + c1ComboStreet.Text + ", дом № " + c1ComboHome.Text;
                FrmObjUkg.Instance = new FrmObjUkg();
                FrmObjUkg.Instance.Text = "Объекты по адресу: " + strStreetObj;
                FrmObjUkg.Instance.m_iLoadFrm = 2;
                FrmObjUkg.Instance.dsHomeSelObj = dsHomeSelEdit;
                FrmObjUkg.Instance.ShowDialog();
            }
            else
            {
                if (l_strStrId != "")   //только если найдена улица в базе
                {
                    DataView dvHome = new DataView(dsHomeFullRpt.Tables["Buildings"], "StreetId = '" + l_strStrId + "'",
                                "BuildName", DataViewRowState.CurrentRows);
                    int rowIndex = dvHome.Find(c1ComboHome.Text);
                    if (rowIndex != -1)
                    {
                        m_strBuildId = dvHome[rowIndex]["BuildId"].ToString();
                        textBoxObj.Text = "Жилой дом";
                    }
                    else
                    {
                        m_strBuildId = "";    //дом не найден
                        textBoxObj.Text = "Нет данных";
                    }
                }
            }

        }

        private void btnRecObj_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Записать данные?", "Данные объекта",
                            MessageBoxButtons.YesNo, MessageBoxIcon.Question)
                            == DialogResult.Yes)
            {
                if (l_iRecSng == 1)
                {

                }

                string strDispMsg = "";
                if (textBoxStreetType.Text.Trim() == "")    // не пишет если поле тип улице пустое
                    strDispMsg = c1ComboStreet.Text.Trim();
                else
                    strDispMsg = textBoxStreetType.Text.Trim() + " " + c1ComboStreet.Text.Trim();
                if (c1ComboHome.Text.Trim() != "")  // не пишет если поле № дома пустое
                    strDispMsg = strDispMsg + " дом №" + c1ComboHome.Text.Trim();
                //string rtr = c1ComboFlat.Text.Trim();
                if (c1ComboFlat.Text.Trim() != "")  // не пишет если поле № квартиры пустое
                    strDispMsg = strDispMsg + " кв.№" + c1ComboFlat.Text.Trim();
                if (textBoxObj.Text.Trim() != "")  // не пишет если поле объекта пустое
                    strDispMsg = strDispMsg + " " + textBoxObj.Text.Trim();

                string strDate = c1DatePermit.Text;

                if (FrmDispFire.Instance.Visible)
                {
                    FrmDispFire.Instance.c1FlxGrdDispMain.Cols[3].AllowEditing = true;
                    FrmDispFire.Instance.c1FlxGrdDispMain.SetData(l_iRowSel, 3, strDispMsg, true);   //вставляет в таблицу данные об объекте
                    FrmDispFire.Instance.c1FlxGrdDispMain.Cols[3].AllowEditing = false;
                }
                string strSqlTextDisp = "UPDATE ReportDisp SET RptDate = CONVERT(DATETIME, '" + strDate + "', 103), RptDispMsg = '" + strDispMsg + "' " +
                                                "WHERE RptDispId = " + strRptDispId;
                SqlCommand command = new SqlCommand(strSqlTextDisp, FrmMain.Instance.m_cnn);
                Int32 recordsAffected = command.ExecuteNonQuery();

                string strBuildHome = "";
                string strRptFireFlat = c1ComboFlat.Text;
                string strRptRing = txtBoxRing.Text;
                string strRptRingTel = txtBoxRingTel.Text;
                string strRptRang = "1";
                if (c1ComboRang.Text != "")
                    strRptRang = c1ComboRang.Text;
                string strBuildObjId = FrmDispUkg.Instance.m_strBuildObjId;
                if (strBuildObjId != "")
                    m_strBuildId = strBuildObjId;

                string strSqlTextFire = "UPDATE ReportFire SET StreetId = '" + l_strStrId + "', BuildId = '" + m_strBuildId + "', " +
                    "BuildHome = '" + strBuildHome + "', RptFireFlat = '" + strRptFireFlat + "', RptRing = '" + strRptRing + "', " +
                    "RptRingTel = '" + strRptRingTel + "', RptRang = '" + strRptRang + "' WHERE RptDispId = " + strRptDispId;
                SqlCommand command1 = new SqlCommand(strSqlTextFire, FrmMain.Instance.m_cnn);
                Int32 recordsAffected1 = command1.ExecuteNonQuery();
            }
        }

        private void btnFireObjInfo_Click(object sender, EventArgs e)
        {
            string strObjName = "";

            if (bsHomeObj.Count > 0)
            {
                DataRow dr = dsHomeObj.Tables["ObjUkg"].Rows[bsHomeObj.Position];
                string strOrgName = "";
                strObjName = dr["ObjName"].ToString();
                strOrgName = dr["OrgName"].ToString();
                                
                FrmObjUkgData.Instance = new FrmObjUkgData();
                FrmObjUkgData.Instance.Text = "Объект: " + strObjName + " " + strOrgName;
                FrmObjUkgData.Instance.m_strBuildIdObj = m_strBuildId; //dr["BuildId"].ToString();
                FrmObjUkgData.Instance.Show();
            }
            else
            {
                if (m_strBuildId != "")
                {
                    strObjName = "Объект: " + textBoxStreetType.Text + " " + c1ComboStreet.Text + " дом №" + c1ComboHome.Text;

                    FrmHomeData.Instance = new FrmHomeData();
                    FrmHomeData.Instance.Text = strObjName;
                    FrmHomeData.Instance.m_strBuildIdHome = m_strBuildId;
                    FrmHomeData.Instance.Show();
                    FrmHomeData.Instance.chkBoxGarret.Focus();
                }
            }
        }

        private void btnObjMapData_Click(object sender, EventArgs e)
        {
            if (m_strBuildId != "")   // не выводить карту если дома нет в базе данных карты
            {
                FrmDispUkg.Instance.m_strBuildObjId = m_strBuildId;
                FrmMain.Instance.LoadMapUkg(1, m_strBuildId);
            }
        }

        

                
    }
}
