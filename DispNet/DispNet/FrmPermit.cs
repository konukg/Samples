using System;
using System.IO;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Net;
using System.Net.Sockets;
using System.Collections;

namespace DispNet
{
    public partial class FrmPermit : Form
    {
        static public FrmPermit Instance;

        public DataSet dsHomeFull = new DataSet(); // таблица включет все дома в т.ч. и повторяющиеся (например объекты)
        public BindingSource bsHomeFull = new BindingSource();
        //public DataSet dsHomeSel = new DataSet();
        //BindingSource bsHomeSel = new BindingSource();

        DataSet dsWater = new DataSet();
        public DataSet dsHome = new DataSet();
        public DataSet dsStreet = new DataSet();
        public BindingSource bsStreet = new BindingSource();
        BindingSource bsWater = new BindingSource();
        public BindingSource bsHome = new BindingSource();
        public BindingSource m_bsWaterData = new BindingSource();   //все данные водоисточника
        public DataSet m_dsWaterData = new DataSet();

        public SqlConnection cnn;
        string strFildRang;
        //bool blRangBlok = false;
        //bool blc1ComboHomeCol = true;
        //bool blBlocEnterHome = false;   //блокирует вызов frmStreetDiv пока выполняется загрузка домов
        public bool m_blObjSign = false;         //признак объекта при выборе пожара
        bool blObjWater = false;    //на объекте нет водоисточников поле ObjWater пустое
        public int m_iStreetDivUnit;
        public bool blFrmPmtShow = false;
        string lstrAdrPmt;  //адрес пожара одной строкой для старого вариента путевки
        string lstrPath;    //путь к папке Send для путевки
        string lstrtObjWaterList = "";  //водоисточники объекта одной строкой
        int liUnitIdObj = 0;    //Id пожарной части объекта
        string lstrBuildIdObj = "0";   // Id порядковый номер дома объекта таблицы Buildings
        //public string m_strStreetObj; //улица и номер дома при выборе объекта по значению дома для FrmObjUkg
        public string m_strBuildIdMap = "";   //BuildId дома или объекта для вывода информации в главном окне
        public bool m_blWrHome = false;         //false - признак если дом не выбран через Enter а введен в ручную
        bool m_blWrPermitHand = false;         //признак если улица, дом,техника введены в ручную (неизвестный адрес)
        int l_intStreetPos = 0; //позиция улицы если при закрытии c1ComboFnd не выбрана новая улица то форме путевки данные не меняются
                
        public FrmPermit()
        {
            InitializeComponent();
        }

        private void FrmPermit_Load(object sender, EventArgs e)
        {
            //this.Hide();
            lstrPath = FrmMain.Instance.m_strPmtPathSend;
            ToolTip toolTipPermit = new ToolTip();

            // Set up the delays for the ToolTip.
            toolTipPermit.AutoPopDelay = 5000;
            toolTipPermit.InitialDelay = 1000;
            toolTipPermit.ReshowDelay = 500;
            // Force the ToolTip text to be displayed whether or not the form is active.
            toolTipPermit.ShowAlways = true;

            // Set up the ToolTip text for the Button and Checkbox.
            toolTipPermit.SetToolTip(this.btnRemovBort, "Удалить борт");
            toolTipPermit.SetToolTip(this.btnAddBort, "Добавить борт");

            string[] strRang = new string[] { "1", "1,5", "2", "3", "4" };
            foreach (string i in strRang)
                c1ComboRang.AddItem(i);
            
            c1ComboFnd.Enabled = false;            
            backgroundWorker1.RunWorkerAsync(1);
        }

        private void btnTest_Click(object sender, EventArgs e)
        {
            DateTime myDateTime, aftDateTime;
            myDateTime = DateTime.Now;
            aftDateTime = DateTime.Parse("8:00:00");
            int iCmp = DateTime.Compare(myDateTime, aftDateTime);
        }

        private void LoadSqlData(int iPrN)
        {
            
            if (iPrN == 1)
            {
                StreetLoad();

                string strSqlTextWater = "SELECT Water.* FROM Water";
                SqlDataAdapter daWater = new SqlDataAdapter(strSqlTextWater, cnn);
                daWater.Fill(dsWater, "Water");
                bsWater.DataSource = dsWater;
                bsWater.DataMember = "Water";

                string strSqlTextWaterData = "SELECT Water.WaterId, Water.WaterName, Water.WaterNumber, Water.WaterHome, Water.WaterHome1, " +
                "WaterState.WaterStateName, Water.WaterDate, Water.WaterVolume, Water.WaterPath, Water.WaterType, Water.WaterLook, " +
                "Water.WaterBore, Water.WaterPressure, Water.WaterExpense, Water.WaterNote, Street.StreetName, " +
                "StreetType.StreetTypeName, Unit.UnitName " +
                "FROM Water INNER JOIN WaterState ON Water.WaterStateId = WaterState.WaterStateId " +
                "INNER JOIN Street ON Water.StreetId = Street.StreetId INNER JOIN StreetType ON " +
                "Street.StreetTypeId = StreetType.StreetTypeId INNER JOIN Unit ON Water.UnitId = Unit.UnitId " +
                "ORDER BY Water.WaterId";
                SqlDataAdapter daWaterData = new SqlDataAdapter(strSqlTextWaterData, cnn);
                daWaterData.Fill(m_dsWaterData, "Water");
                m_bsWaterData.DataSource = m_dsWaterData;
                m_bsWaterData.DataMember = "Water";
                int iCnt = m_bsWaterData.Count;

                string strSqlTextHomeFull = "SELECT BuildId, BuildName, StreetId, UnitId " +
                        "FROM Buildings ORDER BY BuildId";
                SqlDataAdapter daHomeFull = new SqlDataAdapter(strSqlTextHomeFull, cnn);
                daHomeFull.Fill(dsHomeFull, "Buildings");
                bsHomeFull.DataSource = dsHomeFull;
                bsHomeFull.DataMember = "Buildings";

                string strSqlTextHome = "SELECT DISTINCT BuildName, StreetId " +
                        "FROM Buildings " +
                            "ORDER BY BuildName";
                SqlDataAdapter daHome = new SqlDataAdapter(strSqlTextHome, cnn);
                daHome.Fill(dsHome, "Buildings");
                bsHome.DataSource = dsHome;
                bsHome.DataMember = "Buildings";
                bsHome.Filter = "StreetId = '1'";
            }
            if (iPrN == 2)
            {
                RecDataRptDisp();
                if (FrmMain.Instance.m_blSendSrv)   //посылает сообщение в части если доступен сервер
                    SendMsgServer();
            }
        }

        public void m_TechnicsLoad(string strRang, string strHomeNameFnd)
        {
            DataSet dsChooseBort = new DataSet();
            BindingSource bsChooseBort = new BindingSource();
            string strTechSch = "TechnicsSchedule";

            string strSqlTextChooseBort = "SELECT TechnicsSchedule.*, TechnicsUnit.TechnicsName, TechnicsUnit.StateId " +
                                "FROM TechnicsSchedule INNER JOIN TechnicsUnit ON TechnicsSchedule.TechnicsId = TechnicsUnit.TechnicsId " +
                                "WHERE (TechnicsUnit.StateId = 1)ORDER BY TechnicsSchedule.ScheduleId";

            SqlDataAdapter daChooseBort = new SqlDataAdapter(strSqlTextChooseBort, cnn);
            daChooseBort.Fill(dsChooseBort, strTechSch);
            bsChooseBort.DataSource = dsChooseBort;
            bsChooseBort.DataMember = strTechSch;

            int iUnitFnd = 0;
            int iHomeFn = 0;
            string strFildName;
            if (bsChooseBort.Count > 0)
            {
                DataRow dr = dsStreet.Tables["Street"].Rows[bsStreet.Position];
                string strStreetId = dr["StreetId"].ToString();
                bool iId = Convert.ToBoolean(dr["StreetDivide"].ToString());
                if (iId)
                {
                    iHomeFn = StringParse(strHomeNameFnd);
                    if (iHomeFn > 0)
                    {
                        string strStreetIdDv = dr["StreetId"].ToString();
                        iUnitFnd = StreetDivide(strStreetIdDv, iHomeFn);  // выбор ПЧ при делении улицы - номер дома выбран корректно
                    }
                    else
                    {
                        //if (blBlocEnterHome)    //блокирует вызов frmStreetDiv пока выполняется загрузка домов
                        //{
                        frmStreetDiv frmStreetDiv = new frmStreetDiv();
                        frmStreetDiv.strStreetId = strStreetId;     //dr["StreetId"].ToString();
                        m_iStreetDivUnit = 0;
                        frmStreetDiv.ShowDialog();
                        if (m_iStreetDivUnit > 0)
                            iUnitFnd = m_iStreetDivUnit;
                        else
                            iUnitFnd = Convert.ToInt32(dr["UnitId"].ToString());
                        //}
                    }
                }
                else 
                {
                    //поиск BuildId если выводить дом на карту в качестве информции
                    DataView dvHome = new DataView(dsHomeFull.Tables["Buildings"], "StreetId = '" + strStreetId + "'",
                                "BuildName", DataViewRowState.CurrentRows);
                    int rowIndex = dvHome.Find(strHomeNameFnd);
                    if (rowIndex != -1)
                    {
                        m_blWrHome = true;
                        m_strBuildIdMap = dvHome[rowIndex]["BuildId"].ToString();
                        textBoxObj.Text = "Жилой дом";
                        iUnitFnd = Convert.ToInt32(dvHome[rowIndex]["UnitId"].ToString());
                    }
                    else
                    {
                        m_blWrHome = false;
                        m_strBuildIdMap = "";
                        iUnitFnd = Convert.ToInt32(dr["UnitId"].ToString());
                    }
                }

                foreach (DataColumn column in dsChooseBort.Tables[strTechSch].Columns)
                {
                    strFildName = column.ColumnName;
                    strFildRang = strFildName;
                    int lastLocation = strFildName.IndexOf("#");
                    if (lastLocation > 0)
                    {
                        strFildName = strFildName.Substring(0, lastLocation);
                        int iUnit = Convert.ToInt32(strFildName);
                        if (iUnitFnd == iUnit)
                            break;
                    }
                }
                bsChooseBort.MoveFirst();
                int iCnt = bsChooseBort.Count;
                DataRow drBort;
                c1ListBort.ClearItems();
                while (iCnt > 0)
                {
                    drBort = dsChooseBort.Tables[strTechSch].Rows[bsChooseBort.Position];
                    double iRangFnd = Convert.ToDouble(drBort[strFildRang].ToString());
                    if (iRangFnd > 0 && iRangFnd <= Convert.ToDouble(strRang))
                        c1ListBort.AddItem(drBort["TechnicsName"].ToString());
                    bsChooseBort.MoveNext();
                    iCnt--;
                }
            }
            
        }

        public void m_TechnicsLoadObj(string strObjBuildId) //выбор техники для объекта
        {
            /*string strSqlTextObjSel = "SELECT ObjUkg.ObjId, ObjUkg.ObjSign, ObjUkg.ObjClass, ObjUkg.UnitId, ObjUkg.ObjName, " +
                "ObjUkgStaff.OrgName, ObjUkg.BuildId " +
                "FROM ObjUkg INNER JOIN ObjUkgStaff ON ObjUkg.ObjStaffId = ObjUkgStaff.ObjStaffId " +
                "WHERE (ObjUkg.ObjId = '" + strObjId + "')";*/
            string strSqlTextObjSel = "SELECT ObjId, ObjSign, ObjClass, UnitId, ObjWater, BuildId " +
                                        "FROM ObjUkg WHERE(BuildId = '" + strObjBuildId + "')";

            SqlCommand cmd = new SqlCommand(strSqlTextObjSel, cnn);
            SqlDataReader rdrObjSel = cmd.ExecuteReader();
            int iObjId;
            bool bObjSign = false; ;
            string strRang = "0";
            //int iUnitId = 0;
            while (rdrObjSel.Read())
            {
                if (rdrObjSel.IsDBNull(0))
                    iObjId = 0;
                else
                    iObjId = rdrObjSel.GetInt32(0);
                
                if (rdrObjSel.IsDBNull(1))
                    bObjSign = false;
                else
                    bObjSign = rdrObjSel.GetBoolean(1);
                
                if (rdrObjSel.IsDBNull(2))
                    strRang = "1";
                else
                    strRang = rdrObjSel.GetString(2);

                if (rdrObjSel.IsDBNull(3))
                    liUnitIdObj = 0;
                else
                    liUnitIdObj = rdrObjSel.GetInt16(3);

                if (rdrObjSel.IsDBNull(4))
                    blObjWater = false;  //на объекте нет водоисточников поле ObjWater пустое
                else
                {
                    blObjWater = true;
                    lstrtObjWaterList = rdrObjSel.GetString(4);
                }

                lstrBuildIdObj = strObjBuildId;
                
            }
            rdrObjSel.Close();
            
            DataSet dsChooseBort = new DataSet();
            BindingSource bsChooseBort = new BindingSource();
            string strSqlTextChooseBort;
            string strTechSch;
            if (bObjSign)
            {
                strSqlTextChooseBort = "SELECT TechnicsScheduleObj.*, TechnicsUnit.TechnicsName, TechnicsUnit.StateId " +
                                "FROM TechnicsScheduleObj INNER JOIN TechnicsUnit ON TechnicsScheduleObj.TechnicsId = TechnicsUnit.TechnicsId " +
                                "WHERE (TechnicsUnit.StateId = 1)ORDER BY TechnicsScheduleObj.ScheduleId";
                strTechSch = "TechnicsScheduleObj";
            }
            else
            {
                strSqlTextChooseBort = "SELECT TechnicsSchedule.*, TechnicsUnit.TechnicsName, TechnicsUnit.StateId " +
                                "FROM TechnicsSchedule INNER JOIN TechnicsUnit ON TechnicsSchedule.TechnicsId = TechnicsUnit.TechnicsId " +
                                "WHERE (TechnicsUnit.StateId = 1)ORDER BY TechnicsSchedule.ScheduleId";
                strTechSch = "TechnicsSchedule";
            }
            SqlDataAdapter daChooseBort = new SqlDataAdapter(strSqlTextChooseBort, cnn);
            daChooseBort.Fill(dsChooseBort, strTechSch);
            bsChooseBort.DataSource = dsChooseBort;
            bsChooseBort.DataMember = strTechSch;
            string strFildName;
            foreach (DataColumn column in dsChooseBort.Tables[strTechSch].Columns)
            {
                strFildName = column.ColumnName;
                strFildRang = strFildName;
                int lastLocation = strFildName.IndexOf("#");
                if (lastLocation > 0)
                {
                    strFildName = strFildName.Substring(0, lastLocation);
                    int iUnit = Convert.ToInt32(strFildName);
                    if (liUnitIdObj == iUnit)
                        break;
                }
            }
            bsChooseBort.MoveFirst();
            int iCnt = bsChooseBort.Count;
            DataRow drBort;
            c1ListBort.ClearItems();
            while (iCnt > 0)
            {
                drBort = dsChooseBort.Tables[strTechSch].Rows[bsChooseBort.Position];
                double iRangFnd = Convert.ToDouble(drBort[strFildRang].ToString());
                if (iRangFnd > 0 && iRangFnd <= Convert.ToDouble(strRang))
                    c1ListBort.AddItem(drBort["TechnicsName"].ToString());
                bsChooseBort.MoveNext();
                iCnt--;
            }
            //blRangBlok = false;
            c1ComboRang.Text = strRang;
            //blRangBlok = true;
        }

        private void BuildingsLoad(string strStrId)
        {
            bsHome.Filter = "StreetId = '"+ strStrId +"'";
            c1ComboHome.DataSource = bsHome;
            c1ComboHome.ValueMember = "BuildName";
            c1ComboHome.DisplayMember = "BuildName";
            if (c1ComboHome.Columns.Count > 1)
            {
               c1ComboHome.Columns.RemoveAt(1);
            }
        }

        private int StreetDivide(string strStreetId, int iHomeFn)
        {
            string strSqlTextStreetDivide = "SELECT StreetId, DivideHomeFr, DivideHomeLs, " +
                                            "UnitId FROM StreetDivide WHERE(StreetId = ";
            int iUnitFnd = 0;
            strSqlTextStreetDivide = strSqlTextStreetDivide + strStreetId + ") ORDER BY StreetId";
            SqlCommand cmd = new SqlCommand(strSqlTextStreetDivide, cnn);
            SqlDataReader rdr = cmd.ExecuteReader();
            while (rdr.Read())
            {
                int iHomeFr;
                int iHomeLs;
                if (rdr.IsDBNull(1))
                    iHomeFr = 0;
                else
                    iHomeFr = rdr.GetInt16(1);
                if (rdr.IsDBNull(2))
                    iHomeLs = 0;
                else
                    iHomeLs = rdr.GetInt16(2);
                if (iHomeFr >= iHomeFn || iHomeFn <= iHomeLs)
                {
                    iUnitFnd = rdr.GetInt16(3);
                    break;
                }
            }
            rdr.Close();
            return iUnitFnd;
        }

        private void StreetLoad()
        {
                cnn = FrmMain.Instance.m_cnn;
                string strSqlTextStreet = "SELECT Street.StreetId, Street.StreetName, StreetType.StreetTypeName, StreetPlace.StreetPlaceName, " +
                                            "StreetRegion.StreetRegionName, Street.StreetDivide, Street.UnitId, Street.StreetNote FROM Street " +
                                            "INNER JOIN StreetType ON Street.StreetTypeId = StreetType.StreetTypeId " +
                                            "INNER JOIN StreetPlace ON Street.StreetPlaceId = StreetPlace.StreetPlaceId " +
                                            "INNER JOIN StreetRegion ON Street.StreetRegionId = StreetRegion.StreetRegionId " +
                                            "ORDER BY Street.StreetName";
                SqlDataAdapter daStreet = new SqlDataAdapter(strSqlTextStreet, cnn);
                daStreet.Fill(dsStreet, "Street");

                bsStreet.DataSource = dsStreet;
                bsStreet.DataMember = "Street";
                c1ComboFnd.DataSource = bsStreet;
                c1ComboFnd.ValueMember = "StreetName";
                c1ComboFnd.DisplayMember = "StreetName";
                
                c1ComboFnd.Columns.RemoveAt(0);
                c1ComboFnd.Columns.RemoveAt(6);
                c1ComboFnd.Columns.RemoveAt(5);
                c1ComboFnd.Columns.RemoveAt(4);
                c1ComboFnd.Columns[0].Caption = "Улица";
                c1ComboFnd.Columns[1].Caption = "Тип";
                c1ComboFnd.Columns[2].Caption = "Район";
                c1ComboFnd.Columns[3].Caption = "Регион";

                c1ComboFnd.DropDownWidth = 400;
        }

        private void FrmPermit_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = true;
            FrmPermit.Instance.Hide();
            blFrmPmtShow = false;
            ClearItemsFrmPmt();
        }

        private void btnPermit_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Записать информацию?", "Путевка - выезд техники",
                    MessageBoxButtons.YesNo, MessageBoxIcon.Question)
                    == DialogResult.Yes)
            {
                if (c1ListBort.ListCount > 0)
                {
                    FrmPermit.Instance.Hide();
                    blFrmPmtShow = false;
                    //RecDataRptDisp();
                    if (m_blWrPermitHand)
                        RecDataRptDispHand();
                    else
                         backgroundWorker1.RunWorkerAsync(2);   //RecDataRptDisp();
                }
                else
                {
                    if (MessageBox.Show("Список техники пуст!" + "\r\n" + "Записать информацию?", "Путевка - выезд техники",
                        MessageBoxButtons.YesNo, MessageBoxIcon.Question)
                        == DialogResult.Yes)
                    {
                        FrmPermit.Instance.Hide();
                        blFrmPmtShow = false;
                        //RecDataRptDisp();
                        if (m_blWrPermitHand)
                            RecDataRptDispHand();
                        else
                            backgroundWorker1.RunWorkerAsync(2);
                    }    
                }
            }
        }

        private void RecDataRptDispHand()
        {
            m_blWrPermitHand = true;
            DateTime myDateTime = DateTime.Now;
            string strDate = myDateTime.Month.ToString() + "-" + myDateTime.Day.ToString() + "-" + myDateTime.Year.ToString() + " " +
                                myDateTime.ToString("HH:mm:ss");
            //string strDate = myDateTime.Day.ToString() + "-" + myDateTime.Month.ToString() + "-" + myDateTime.Year.ToString() + " " +
                                //myDateTime.ToString("HH:mm:ss");
            string strDispMsg = "";
            if (textBoxStreetType.Text.Trim() == "")    // не пишет если поле тип улице пустое
                strDispMsg = c1ComboFnd.Text.Trim();
            else
                strDispMsg = textBoxStreetType.Text.Trim() + " " + c1ComboFnd.Text.Trim();
            if (c1ComboHome.Text.Trim() != "")  // не пишет если поле № дома пустое
                strDispMsg = strDispMsg + " дом №" + c1ComboHome.Text.Trim();
            if (c1ComboFlat.Text.Trim() != "")  // не пишет если поле № квартиры пустое
                strDispMsg = strDispMsg + " кв.№" + c1ComboFlat.Text.Trim();
            if (textBoxObj.Text.Trim() != "")  // не пишет если поле объекта пустое
                strDispMsg = strDispMsg + " " + textBoxObj.Text.Trim();

            string strUkg = "1";
            string strUkgObl = "1";
            string strSqlTextReportDisp = "INSERT INTO ReportDisp " +
            "(RptDispId, RptDate, RptDispMsg, RptDistrict, RptCity, DepartTypeId, StateChsId, RptRecRpt) " +
            "Values('0', CONVERT(DATETIME, '" + strDate + "', 102), '" + strDispMsg + "', '" + strUkgObl + "', '" + strUkg + "', '1', '1', '0')";

            SqlCommand command = new SqlCommand(strSqlTextReportDisp, cnn);
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
                string strBuildId = "-1";
                string strStreetId = "-1";
                string strUnitId = "-1";
                
                string strSqlTextReportFire = "INSERT INTO ReportFire " +
                "(RptFireId, RptDispId, StreetId, BuildId, BuildHome, RptFireFlat, RptRing, RptMsgFire, RptRang, UnitId, RptRingTel, RptSign) " +
                "Values('0', '" + strRptDispId + "', '" + strStreetId + "', '" + strBuildId + "', '" + c1ComboHome.Text + "', '" + c1ComboFlat.Text + "'" +
                ", '" + textBoxMsg.Text + "', '" + textBoxFire.Text + "', '" + c1ComboRang.Text + "'" +
                ", '" + strUnitId + "', '" + txtBoxRingTel.Text + "', '0')";
                                    
                SqlCommand command1 = new SqlCommand(strSqlTextReportFire, FrmMain.Instance.m_cnn);
                Int32 recordsAffected1 = command1.ExecuteNonQuery();

                string strRptWater = "";
                string strRptWaterDist = "";
                if (c1ListBort.ListCount > 0)   //только если есть в списке техника
                {
                    RecDataRptFireBort(strRptDispId, strRptWater, strRptWaterDist);
                    ClearItemsFrmPmt(); // очистка полей ввода путевки
                    if (FrmMain.Instance.m_blSendSrv)   //посылает сообщение в части если доступен сервер
                        SendMsgServer();
                }
            }// дополнить сообщением если созданная запись не найдена
        }
        
        private void RecDataRptDisp()
        {
            m_blWrPermitHand = false;   //разрешает выбор данных из базы - не ручной ввод

            DateTime myDateTime = DateTime.Now;
            string strDate = myDateTime.Month.ToString() + "-" + myDateTime.Day.ToString() + "-" + myDateTime.Year.ToString() + " " +
                                myDateTime.ToString("HH:mm:ss");
            DataRow dr = dsStreet.Tables["Street"].Rows[bsStreet.Position];
            string strDispMsg = dr["StreetTypeName"].ToString() + " " + dr["StreetName"].ToString();
            //int iHomeFn = StringParse(c1ComboHome.Text);
            //if (iHomeFn > 0)
            //strDispMsg = strDispMsg + " дом №" + c1ComboHome.Text; //пишет номер дома с дробью в жирнал (iHomeFn.ToString());
            if (c1ComboHome.Text.Trim() != "")  // не пишет если поле № дома пустое
                strDispMsg = strDispMsg + " дом №" + c1ComboHome.Text.Trim();
            if (c1ComboFlat.Text.Trim() != "")  // не пишет если поле № квартиры пустое
                strDispMsg = strDispMsg + " кв.№" + c1ComboFlat.Text.Trim();
            if (textBoxObj.Text.Trim() != "")  // не пишет если поле объекта пустое
                strDispMsg = strDispMsg + " " + textBoxObj.Text.Trim();

            string strUkg = "1";
            string strUkgObl = "1";
            string strSqlTextReportDisp = "INSERT INTO ReportDisp " +
            "(RptDispId, RptDate, RptDispMsg, RptDistrict, RptCity, DepartTypeId, StateChsId, RptRecRpt) " +
            "Values('0', CONVERT(DATETIME, '" + strDate + "', 102), '" + strDispMsg + "', '" + strUkgObl + "', '" + strUkg + "', '1', '1', '0')";

            SqlCommand command = new SqlCommand(strSqlTextReportDisp, cnn);
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
                if (blObjWater)
                {
                    if(lstrtObjWaterList.Length > 0)    // если у объекта нет водоисточников ищет по адресу дом + улица
                        RecDataRptFireObj(strRptDispId);
                    else
                        RecDataRptFire(strRptDispId);
                }
                else
                {
                    RecDataRptFire(strRptDispId);
                }
                                
            }// дополнить сообщением если созданная запись не найдена
        }

        private void RecDataRptFireObj(string strRptDispId)
        {
            DataRow dr = dsStreet.Tables["Street"].Rows[bsStreet.Position];

            lstrAdrPmt = dr["StreetRegionName"].ToString() + ", " + dr["StreetPlaceName"].ToString() + ", " +
                         dr["StreetTypeName"].ToString() + " " + dr["StreetName"].ToString();
            if (c1ComboHome.Text != "")
                lstrAdrPmt = lstrAdrPmt + ", Дом:" + c1ComboHome.Text;

            string strStreetId = dr["StreetId"].ToString();
            string strBuildName = c1ComboHome.Text;
            string strFireFlat = "";
            string strRptRing = textBoxMsg.Text;
            string strRptMsgFire = textBoxFire.Text;
            string strRptRang = c1ComboRang.Text;
            
            string strSqlTextReportFire = "INSERT INTO ReportFire " +
            "(RptFireId, RptDispId, StreetId, BuildId, RptFireFlat, RptRing, RptMsgFire, RptRang, RptWater, UnitId, RptRingTel) " +
            "Values('0', '" + strRptDispId + "', '" + strStreetId + "', '" + lstrBuildIdObj + "', '" + strFireFlat + "'" +
            ", '" + strRptRing + "', '" + strRptMsgFire + "', '" + strRptRang + "', '" + lstrtObjWaterList + "'" +
            ", '" + liUnitIdObj.ToString() + "', '" + txtBoxRingTel.Text + "')";

            SqlCommand command = new SqlCommand(strSqlTextReportFire, cnn);
            Int32 recordsAffected = command.ExecuteNonQuery();

            RecDataRptFireBort(strRptDispId, lstrtObjWaterList, "");
        }

        private void RecDataRptFire(string strRptDispId)
        {
            int rowIndex = -1;
            SortedList<double, int> srWater = new SortedList<double, int>();
            DataRow dr = dsStreet.Tables["Street"].Rows[bsStreet.Position];

            lstrAdrPmt = dr["StreetRegionName"].ToString() + ", " + dr["StreetPlaceName"].ToString() + ", " +
                         dr["StreetTypeName"].ToString() + " " + dr["StreetName"].ToString();
            if (c1ComboHome.Text != "")
                lstrAdrPmt = lstrAdrPmt + ", Дом:" + c1ComboHome.Text;
            if (c1ComboFlat.Text != "")
                lstrAdrPmt = lstrAdrPmt + ", кв:" + c1ComboFlat.Text;
            
            string strStreetId = dr["StreetId"].ToString();
            string strUnitId = dr["UnitId"].ToString();
            string strFireFlat = c1ComboFlat.Text;
            string strRptRing = textBoxMsg.Text;
            string strRptMsgFire = textBoxFire.Text;
            string strRptRang = c1ComboRang.Text;
            string strBuildId = "";
            string strBuildName = "";
            string strRptWater = "";
            string strRptWaterDist = "";

            DataView dvHome = new DataView(dsHomeFull.Tables["Buildings"], "StreetId = '" + strStreetId + "'",
                                "BuildName", DataViewRowState.CurrentRows);
            rowIndex = dvHome.Find(c1ComboHome.Text);
            if (rowIndex != -1)
            {
                strBuildId = dvHome[rowIndex]["BuildId"].ToString();
                srWater = FrmMapUkgPmt.Instance.FindWaterPmt(strBuildId);   //поиск водоисточников по карте

                DataView dvWater = new DataView(dsWater.Tables["Water"], "WaterStateId = '1'",
                                "WaterId", DataViewRowState.CurrentRows);
                int iCnt = srWater.Count;
                int iCnt_10 = 0;
                if (iCnt > 0)
                {
                    for (int i = 0; i < iCnt; i++)    
                    {
                        string strId = srWater.Values[i].ToString();
                        string strDist = Convert.ToInt32(srWater.Keys[i]).ToString();
                        rowIndex = dvWater.Find(strId);
                        if (rowIndex != -1)
                        {
                            if (iCnt == 1)  //если нейден только один водоисточник
                            {
                                strRptWater = strId;
                                strRptWaterDist = strDist;
                            }
                            else
                            {
                                if (strRptWater.Length == 0)
                                {
                                    strRptWater = strId;
                                    strRptWaterDist = strDist;
                                }
                                else
                                {
                                    strRptWater = strRptWater + "," + strId;
                                    strRptWaterDist = strRptWaterDist + "," + strDist;
                                }
                            }
                            iCnt_10++;
                            if (iCnt_10 > 9)    //записывает только ближайшие 10 водоисточников
                                break;
                        }
                    }
                }
                else
                {
                    int iHomeFn = StringParse(c1ComboHome.Text);
                    if (iHomeFn > 0)
                    {
                        string strHome = iHomeFn.ToString();
                        strBuildId = "-1";
                        strBuildName = c1ComboHome.Text;
                        strRptWater = FindWaterNoMap(strHome, strStreetId);
                    }
                }
            }
            else
            {
                int iHomeFn = StringParse(c1ComboHome.Text);
                if (iHomeFn > 0)
                {
                    string strHome = iHomeFn.ToString();
                    strBuildId = "-1";
                    strBuildName = c1ComboHome.Text;
                    strRptWater = FindWaterNoMap(strHome, strStreetId); 
                }
            }
            string strSqlTextReportFire = "INSERT INTO ReportFire " +
            "(RptFireId, RptDispId, StreetId, BuildId, BuildHome, RptFireFlat, RptRing, RptMsgFire, RptRang, RptWater, UnitId, RptRingTel, RptSign) " +
            "Values('0', '" + strRptDispId + "', '" + strStreetId + "', '" + strBuildId + "', '" + strBuildName + "', '" + strFireFlat + "'" +
            ", '" + strRptRing + "', '" + strRptMsgFire + "', '" + strRptRang + "', '" + strRptWater + "'" +
            ", '" + strUnitId + "', '" + txtBoxRingTel.Text + "', '0')";

            SqlCommand command = new SqlCommand(strSqlTextReportFire, cnn);
            Int32 recordsAffected = command.ExecuteNonQuery();

            RecDataRptFireBort(strRptDispId, strRptWater, strRptWaterDist);
        }

        private void RecDataRptFireBort(string strRptDispId, string strWaterList, string strWaterDist)
        {
            DataSet dsTechnicsUnit = new DataSet();
            BindingSource bsTechnicsUnit = new BindingSource();
            string strSqlTextTechnicsUnit = "SELECT TechnicsUnit.TechnicsId, TechnicsUnit.StateId, TechnicsState.StateName, " +
                "TechnicsUnit.UnitId, TechnicsUnit.UnitAllow, TechnicsUnit.TechnicsName " +
                "FROM TechnicsUnit INNER JOIN TechnicsState ON TechnicsUnit.StateId = TechnicsState.StateId " +
                "ORDER BY TechnicsUnit.TechnicsId";
            SqlDataAdapter daTechnicsUnit = new SqlDataAdapter(strSqlTextTechnicsUnit, cnn);
            daTechnicsUnit.Fill(dsTechnicsUnit, "TechnicsUnit");
            bsTechnicsUnit.DataSource = dsTechnicsUnit;
            bsTechnicsUnit.DataMember = "TechnicsUnit";
            DataView dvTechnicsUnit = new DataView(dsTechnicsUnit.Tables["TechnicsUnit"], "",
                                "TechnicsName", DataViewRowState.CurrentRows);

            string strBortName;
            string TechnicsId;
            string strTechnicsState;
            string strTechnicsName;
            string strMsgState;
            int iTechnicsState = 0;
            DateTime myDateTime = DateTime.Now;
            string strBortDrive = myDateTime.Month.ToString() + "-" + myDateTime.Day.ToString() + "-" + myDateTime.Year.ToString() + " " +
                                        myDateTime.ToString("HH:mm:ss");
            int iCnt = c1ListBort.ListCount;
            ArrayList arrPmtRec = new ArrayList();  // содержит все строки путевок для записи в базу
            string strPmtRec = "";
            string strWaterPmtRec = ""; // содержит строку путевки водоисточников для записи в базу
            bool blAllow = false;   // 
            for (int i = 0; i < iCnt; i = i + 1)
            {
                strPmtRec = "";
                strBortName = c1ListBort.GetItemText(i, 0);
                int rowIndex = dvTechnicsUnit.Find(strBortName);
                if (rowIndex != -1)
                {
                    TechnicsId = dvTechnicsUnit[rowIndex]["TechnicsId"].ToString();
                    iTechnicsState = Convert.ToInt32(dvTechnicsUnit[rowIndex]["StateId"].ToString());
                    blAllow = Convert.ToBoolean(dvTechnicsUnit[rowIndex]["UnitAllow"]);
                    string strUnitId = dvTechnicsUnit[rowIndex]["UnitId"].ToString();

                    string strPemitPmtRec = "";
                    if (iTechnicsState == 1)
                    {
                        string strSqlTextReportFireBort = "INSERT INTO ReportFireBort " +
                        "(RptFireBortId, RptDispId, TechnicsId, BortDrive) " +
                        "Values('0', '" + strRptDispId + "', '" + TechnicsId + "', CONVERT(DATETIME, '" + strBortDrive + "', 102))";

                        SqlCommand command = new SqlCommand(strSqlTextReportFireBort, cnn);
                        Int32 recordsAffected = command.ExecuteNonQuery();

                        ReplaceBortState("4", TechnicsId);
                        if (blAllow)
                        {
                            strPemitPmtRec = WritePemit(strUnitId, strBortName, strRptDispId);
                            //if(!m_blWrPermitHand)   // не посылает в путевку водоисточники при ручном вводе
                                //strWaterPmtRec = PemitWater(strWaterList, strWaterDist, strUnitId, strRptDispId);
                        }
                    }
                    else
                    {
                        strTechnicsName = dvTechnicsUnit[rowIndex]["TechnicsName"].ToString();
                        strTechnicsState = dvTechnicsUnit[rowIndex]["StateName"].ToString();
                        strMsgState = "Борт " + strTechnicsName + " имеет состояние - " + strTechnicsState +
                           "." + "\r\n" + "Заменить на значение - Приказ на выезд?";
                        if (MessageBox.Show(strMsgState, "Путевка - состояние техники",
                                            MessageBoxButtons.YesNo, MessageBoxIcon.Question)
                                            == DialogResult.Yes)
                        {
                            string strSqlTextReportFireBort = "INSERT INTO ReportFireBort " +
                            "(RptFireBortId, RptDispId, TechnicsId, BortDrive) " +
                            "Values('0', '" + strRptDispId + "', '" + TechnicsId + "', '" + strBortDrive + "')";

                            SqlCommand command = new SqlCommand(strSqlTextReportFireBort, cnn);
                            Int32 recordsAffected = command.ExecuteNonQuery();

                            ReplaceBortState("4", TechnicsId);
                            if (blAllow)
                            {
                                strPemitPmtRec = WritePemit(strUnitId, strBortName, strRptDispId);
                                //if (!m_blWrPermitHand)   // не посылает в путевку водоисточники при ручном вводе
                                    //strWaterPmtRec = PemitWater(strWaterList, strWaterDist, strUnitId, strRptDispId);
                            }
                        }
                    }
                    strPmtRec = strRptDispId + "#" + TechnicsId + "#" + strUnitId + "#" + strPemitPmtRec;
                    int iUnitIdLast = 0;
                    int iUnitIdNw = 0;
                    if (!m_blWrPermitHand)   // не посылает в путевку водоисточники при ручном вводе
                    {
                        iUnitIdNw = Convert.ToInt16(strUnitId);
                        if (iUnitIdLast != iUnitIdNw && blAllow == true)   // один раз создает файл водоисточники для каждой ПЧ
                        {
                            iUnitIdLast = iUnitIdNw;
                            strWaterPmtRec = PemitWater(strWaterList, strWaterDist, strUnitId, strRptDispId);
                            strWaterPmtRec = strWaterPmtRec + "#" + strWaterList + "#" + strWaterDist;
                        }
                    }
                }
                if (blAllow)    // разрешает запись путевок в БД только для техники у которой есть разрешение на выезд
                    arrPmtRec.Add(strPmtRec);
            }
            RecDataRptPemit(arrPmtRec, strWaterPmtRec);
        }

        private void RecDataRptPemit(ArrayList arrPmt, string strWaterPmt)
        {
            DateTime myDateTime = DateTime.Now;
            string strPmtTimeRec = myDateTime.Month.ToString() + "-" + myDateTime.Day.ToString() + "-" + myDateTime.Year.ToString() + " " +
                                        myDateTime.ToString("HH:mm:ss");
            string strDispName = FrmMain.Instance.m_strLocalNameComp;   //имя компьютера
            
            int iCnt = arrPmt.Count;
            string strPmtRec = "";
            string strRptDispId = "";
            string TechnicsId = "";
            string strUnitId = "";
            string strPemitPmtRec = "";

            for (int i = 0; i < iCnt; i++)
            {
                strPmtRec = "";
                strPmtRec = arrPmt[i].ToString();
                string[] wrdPemit = strPmtRec.Split('#');
                strRptDispId = wrdPemit[0];
                TechnicsId = wrdPemit[1];
                strUnitId = wrdPemit[2];
                strPemitPmtRec = wrdPemit[3];

                string strSqlTextReportPemitPmt = "INSERT INTO ReportPemitPmt " +
                        "(RptPmtPemitId, RptPmtTime, RptPmtDispId, RptPmtTechnicsId, RptPmtUnitId, RptPmtPemit, RptPmtDispName) " +
                        "Values('0', CONVERT(DATETIME, '" + strPmtTimeRec + "', 102), '" + strRptDispId + "', '" + TechnicsId + "', '" + strUnitId + "', " +
                        "'" + strPemitPmtRec + "', '" + strDispName + "')";

                SqlCommand cmdPmtPemit = new SqlCommand(strSqlTextReportPemitPmt, cnn);
                Int32 recordsPmtPemit = cmdPmtPemit.ExecuteNonQuery();
            }

            string[] wrdWater = strWaterPmt.Split('#');
            string strWaterPmtRec = wrdWater[0];
            string strWaterList = wrdWater[1];
            string strWaterDist = wrdWater[2];

            string strSqlTextReportWaterPmt = "INSERT INTO ReportWaterPmt " +
                        "(RptPmtWaterId, RptPmtTime, RptPmtDispId, RptPmtWaterList, RptPmtWaterDist,  RptPmtWater, RptPmtDispName) " +
                        "Values('0', CONVERT(DATETIME, '" + strPmtTimeRec + "', 102), '" + strRptDispId + "', '" + strWaterList + "', '" + strWaterDist + "', " +
                        "'" + strWaterPmtRec + "', '" + strDispName + "')";
            SqlCommand cmdPmtWater = new SqlCommand(strSqlTextReportWaterPmt, cnn);
            Int32 recordsPmtWater = cmdPmtWater.ExecuteNonQuery();
        }

        public void ReplaceBortState(string strStateId, string strTechnicsId)
        {
            string strSqlTextBortState = "UPDATE TechnicsUnit SET StateId = " + strStateId +
                                         " WHERE TechnicsId = " + strTechnicsId;
            SqlCommand command = new SqlCommand(strSqlTextBortState, cnn);
            Int32 recordsAffected = command.ExecuteNonQuery();
        }

        private string FindWaterNoMap(string strHomeW, string strStreet)
        {
            DataView dvWaterUp = new DataView(dsWater.Tables["Water"], "WaterHome >= '"+ strHomeW +"' AND StreetId = '" + strStreet +"'",
                                "WaterHome ASC", DataViewRowState.CurrentRows);
            DataView dvWaterDown = new DataView(dsWater.Tables["Water"], "WaterHome < '" + strHomeW + "' AND StreetId = '" + strStreet + "'",
                                "WaterHome ASC", DataViewRowState.CurrentRows);
            string strHome;
            string strWaterId = "";
            int iCntUp5 = 0;
            int iCntDown5 = 0;
            int iCntUp = dvWaterUp.Count;   // количество водоисточников в сторону увелечения номеров домов
            int iCntDown = dvWaterDown.Count;   // количество водоисточников в сторону уменьшения номеров домов
            
            if (iCntUp < 5 && iCntDown < 5)     // если в низ и в вверх от дома меньше 5 водоисточников
            {
                iCntUp5 = iCntUp;
                iCntDown5 = iCntDown;
            }

            if (iCntUp >= 5 && iCntDown >= 5)   // если в низ и в вверх от дома больше 5 водоисточников
            {
                iCntUp5 = 5;
                iCntDown5 = 5;
            }

            if (iCntUp > 5 && iCntDown < 5)     // если меньше 5 водоисточников в низ от дома увеличивает поиск в ввер
            {
                iCntUp5 = 10 - iCntDown;
                iCntDown5 = iCntDown;
            }

            if (iCntUp < 5 && iCntDown > 5)     // если меньше 5 водоисточников в вверх от дома увеличивает поиск в низ
            {
                iCntDown5 = 10 - iCntUp;
                iCntUp5 = iCntUp;
            }
             
            int i = 0;
            string strWaterIdUp = "";
            foreach (DataRowView rowView in dvWaterUp)
            {
                strHome = rowView["WaterId"].ToString();
                i++;
                if (iCntUp5 == 1)
                {
                    strWaterIdUp = strHome;
                }
                else
                {
                    if (i == 1)
                    {
                        strWaterIdUp = strHome;
                    }
                    else
                    {
                        strWaterIdUp = strWaterIdUp + "," + strHome;
                    }
                }
                if (iCntUp5 == i)
                    break;
            }

            i = 0;
            string strWaterIdDown = "";
            foreach (DataRowView rowView in dvWaterDown)
            {
                strHome = rowView["WaterId"].ToString();
                i++;
                if (iCntDown5 == 1)
                {
                    strWaterIdDown = strHome;
                }
                else
                {
                    if (i == 1)
                    {
                        strWaterIdDown = strHome;
                    }
                    else
                    {
                        strWaterIdDown = strWaterIdDown + "," + strHome;
                    }
                }
                if (iCntDown5 == i)
                    break;
            }
            if (strWaterIdDown == "" && strWaterIdUp == "")
                strWaterId = "";
            if (strWaterIdDown == "" && strWaterIdUp != "")
                strWaterId = strWaterIdUp;
            if (strWaterIdDown != "" && strWaterIdUp == "")
                strWaterId = strWaterIdDown;
            if (strWaterIdDown != "" && strWaterIdUp != "")
                strWaterId = strWaterIdDown + "," + strWaterIdUp;

            return strWaterId;
        }

        private void ClearItemsFrmPmt()
        {
            c1ComboFnd.Text = "";
            textBoxStreetType.Text = "";
            textBoxStreetPlace.Text = "";
            textBoxObj.Text = "";
            textBoxFire.Text = "";
            textBoxMsg.Text = "";
            c1ComboHome.Text = "";
            bsHome.Filter = "StreetId = '0'";
            c1ComboHome.DataSource = bsHome;
            c1ComboHome.ValueMember = "BuildName";
            c1ComboRang.Text = "";
            c1ComboFlat.Text = "";
            //blRangBlok = false; //при закрытии формы не дает посылать "" в функцию TechnicsLoad
            //blBlocEnterHome = false;    //убирает признак ручного ввода
            //c1ComboRang.ClearItems();
            c1ListBort.ClearItems();

            lstrAdrPmt = "";

            m_blWrHome = false;
            m_blObjSign = false;
            
            blObjWater = false;
            lstrtObjWaterList = "";
            
            liUnitIdObj = 0;
            lstrBuildIdObj = "0";

            c1ComboFnd.Enabled = true;
            c1ComboFnd.Focus();
            
        }

        private string WritePemit(string strPchId, string strBortName, string strRptId)
        {
            string strPemitPmt = "";    // строка с текстом путевки
            DateTime myDateTime = DateTime.Now;
            string strDate = myDateTime.ToLongDateString();
            string strTime = myDateTime.ToString("HH:mm:ss");
            strDate = "               " + strDate;

            string strPathPmtId = lstrPath + strPchId + @"\" + strRptId + "_" + strBortName + ".prm";
            FileStream f = new FileStream(strPathPmtId, FileMode.Create);
            StreamWriter s = new StreamWriter(f);

            s.WriteLine("1");
            s.WriteLine("                                             П У Т Е В К А");
            s.WriteLine("");
            s.WriteLine(("                выезда дежурного караула пожарной части БОРТ № " + strBortName));
            s.WriteLine("");
            s.WriteLine(("          1.Место пожара: " + lstrAdrPmt));
            if (textBoxObj.Text != "")  // не пишет если поле объекта пустое
                s.WriteLine(("            Объект пожара: " + textBoxObj.Text));
            s.WriteLine("");
            s.WriteLine(("          2.Что горит: " + textBoxFire.Text));
            s.WriteLine("");
            s.WriteLine(("          3.Время получения извещения: " + strTime));
            s.WriteLine("");
            s.WriteLine(("          4.Фамилия и № телефона сообщившего: " + textBoxMsg.Text));
            s.WriteLine("");
            s.WriteLine(("          5.Время возвращения в ПЧ________час.________мин."));
            s.WriteLine("");
            s.WriteLine(("          Дежурный телефонист ______________________"));
            s.WriteLine("");
            s.WriteLine(strDate);
            s.WriteLine("");
            s.WriteLine(("          Примечание:Отсутствие сведений о том, что горит и данных заявителя"));
            s.WriteLine(("          не может задержать выезд караула на пожар"));
            s.WriteLine("");
            s.WriteLine(("          ").PadRight(80, '_'));

            s.Close();
            f.Close();

            StreamReader sr = File.OpenText(strPathPmtId);
            string strRtr = sr.ReadToEnd();
            strPemitPmt = strRtr;
            sr.Close();

            f = new FileStream(strPathPmtId, FileMode.Create);
            Encoding enc = Encoding.GetEncoding(1251);
            Byte[] encodedBytes = enc.GetBytes(strRtr);
            f.Write(encodedBytes, 0, encodedBytes.Length);
            f.Close();

            return strPemitPmt;
        }

        private string WritePemitWater_bak(string strWaterId)
        {
            string strWaterList;
            string strSqlTextWater = "SELECT Water.WaterId, Water.WaterName, Water.WaterNumber, Water.WaterHome, Water.WaterHome1, " +
                "WaterState.WaterStateName, Water.WaterDate, Water.WaterVolume, Water.WaterPath, Water.WaterType, Water.WaterLook, " +
                "Water.WaterBore, Water.WaterPressure, Water.WaterExpense, Water.WaterNote, Street.StreetName, " +
                "StreetType.StreetTypeName, Unit.UnitName " +
                "FROM Water INNER JOIN WaterState ON Water.WaterStateId = WaterState.WaterStateId " +
                "INNER JOIN Street ON Water.StreetId = Street.StreetId INNER JOIN StreetType ON " +
                "Street.StreetTypeId = StreetType.StreetTypeId INNER JOIN Unit ON Water.UnitId = Unit.UnitId " +
                "WHERE (Water.WaterId = " + strWaterId + ")";
            SqlCommand cmd = new SqlCommand(strSqlTextWater, FrmMain.Instance.m_cnn);
            SqlDataReader rdrWater = cmd.ExecuteReader();
            string strHome = "нет данных";
            string strHome1 = "";
            string strStreetType = "нет данных";
            string strStreet = "нет данных";
            string strWaterName = "нет данных";
            string strWaterNumber = "нет данных";
            string strWaterDate = "нет данных";
            string strWaterVolume = "нет данных";
            string strWaterPath = "нет данных";
            string strWaterType = "нет данных";
            string strWaterLook = "нет данных";
            string strWaterBore = "нет данных";
            string strWaterPressure = "нет данных";
            string strWaterExpense = "нет данных";
            string strUnitName = "нет данных";
            string strWaterNote = "нет данных";
            while (rdrWater.Read())
            {
                if (!rdrWater.IsDBNull(3))
                    strHome = rdrWater.GetInt16(3).ToString();
                if (!rdrWater.IsDBNull(4))
                    strHome1 = rdrWater.GetString(4);
                if (!rdrWater.IsDBNull(16))
                    strStreetType = rdrWater.GetString(16);
                if (!rdrWater.IsDBNull(15))
                    strStreet = rdrWater.GetString(15);
                if (!rdrWater.IsDBNull(1))
                    strWaterName = rdrWater.GetString(1);
                if (!rdrWater.IsDBNull(2))
                    strWaterNumber = rdrWater.GetString(2);
                if (!rdrWater.IsDBNull(6))
                    strWaterDate = rdrWater.GetDateTime(6).ToShortDateString();
                if (!rdrWater.IsDBNull(7))
                    strWaterVolume = rdrWater.GetFloat(7).ToString();
                if (!rdrWater.IsDBNull(8))
                    strWaterPath = rdrWater.GetInt16(8).ToString();
                if (!rdrWater.IsDBNull(9))
                    strWaterType = rdrWater.GetString(9);
                if (!rdrWater.IsDBNull(10))
                    strWaterLook = rdrWater.GetString(10);
                if (!rdrWater.IsDBNull(11))
                    strWaterBore = rdrWater.GetInt16(11).ToString();
                if (!rdrWater.IsDBNull(12))
                    strWaterPressure = rdrWater.GetFloat(12).ToString();
                if (!rdrWater.IsDBNull(13))
                    strWaterExpense = rdrWater.GetInt16(13).ToString();
                if (!rdrWater.IsDBNull(17))
                    strUnitName = rdrWater.GetString(17);
                if (!rdrWater.IsDBNull(14))
                    strWaterNote = rdrWater.GetString(14);

            }
            rdrWater.Close();
            
            if(strHome1 != "")
                strWaterList = strHome + "/" + strHome1;
            else
                strWaterList = strHome;
            
            strWaterList = strWaterList + "," + strStreetType + "," + strStreet + "," + strWaterName + "," +
                strWaterNumber + "," + strWaterDate + "," + strWaterVolume + "," + strWaterPath + "," +
                strWaterType + "," + strWaterLook + "," + strWaterBore + "," + strWaterPressure + "," +
                strWaterExpense + "," + strUnitName + "," + strWaterNote;

            return strWaterList;
        }

        private string WritePemitWater(string strWaterId)
        {
            string strWaterList = "";
            string strHome = "нет данных";
            string strHome1 = "";
            string strStreetType = "нет данных";
            string strStreet = "нет данных";
            string strWaterName = "нет данных";
            string strWaterNumber = "нет данных";
            string strWaterDate = "нет данных";
            string strWaterVolume = "нет данных";
            string strWaterPath = "нет данных";
            string strWaterType = "нет данных";
            string strWaterLook = "нет данных";
            string strWaterBore = "нет данных";
            string strWaterPressure = "нет данных";
            string strWaterExpense = "нет данных";
            string strUnitName = "нет данных";
            string strWaterNote = "нет данных";

            DataView dvWaterData = new DataView(m_dsWaterData.Tables["Water"], "WaterId = '" + strWaterId + "'",
                            "WaterName", DataViewRowState.CurrentRows);
            int iCntWater = dvWaterData.Count;
            if (iCntWater == 1)
            {
                strWaterName = dvWaterData[0]["WaterName"].ToString();
                strWaterNumber = dvWaterData[0]["WaterNumber"].ToString();
                strHome = dvWaterData[0]["WaterHome"].ToString();
                strHome1 = dvWaterData[0]["WaterHome1"].ToString();
                strWaterDate = dvWaterData[0]["WaterDate"].ToString();
                strWaterVolume = dvWaterData[0]["WaterVolume"].ToString();
                strWaterPath = dvWaterData[0]["WaterPath"].ToString();
                strWaterType = dvWaterData[0]["WaterType"].ToString();
                strWaterLook = dvWaterData[0]["WaterLook"].ToString();
                strWaterBore = dvWaterData[0]["WaterBore"].ToString();
                strWaterPressure = dvWaterData[0]["WaterPressure"].ToString();
                strWaterExpense = dvWaterData[0]["WaterExpense"].ToString();
                strWaterNote = dvWaterData[0]["WaterNote"].ToString();
                strStreet = dvWaterData[0]["StreetName"].ToString();
                strStreetType = dvWaterData[0]["StreetTypeName"].ToString();
                strUnitName = dvWaterData[0]["UnitName"].ToString();

                if (strHome1 != "")
                    strWaterList = strHome + "/" + strHome1;
                else
                    strWaterList = strHome;

                strWaterList = strWaterList + "," + strStreetType + "," + strStreet + "," + strWaterName + "," +
                    strWaterNumber + "," + strWaterDate + "," + strWaterVolume + "," + strWaterPath + "," +
                    strWaterType + "," + strWaterLook + "," + strWaterBore + "," + strWaterPressure + "," +
                    strWaterExpense + "," + strUnitName + "," + strWaterNote;
            }
            return strWaterList;
        }

        private string PemitWater(string strWater, string strDist, string strPchId, string strRptId)
        {
            string strWaterPmt = "";    // строка с текстом путевки водоисточники
            string[] strListWater = strWater.Split(',');
            if (strListWater.Length > 0)
            {
                string strPathPmtId = lstrPath + strPchId + @"\" + strRptId + "_" + "water.prm";
                //if (File.Exists(strPathPmtId))
                //{

                //}
                //else
                //{
                    FileStream f = new FileStream(strPathPmtId, FileMode.Create);
                    StreamWriter s = new StreamWriter(f);

                    s.WriteLine("                                             В О Д О И С Т О Ч Н И К И");
                    s.WriteLine("");
                    string[] strListDist = strDist.Split(',');
                    bool blDist = false;

                    if (strDist.Length > 0)
                        blDist = true;

                    for (int i = 0; i < strListWater.Length; i++)
                    {
                        string strWaterData = WritePemitWater(strListWater[i]);
                        string[] strMsgList = strWaterData.Split(',');

                        s.WriteLine("          дом " + strMsgList[0] + " " + strMsgList[1] + " " + strMsgList[2] + " " +
                                    strMsgList[3] + " " + strMsgList[4]);
                        if (blDist) // вкдючает строку дистанция только если данные из карты города
                            s.WriteLine("          Расстояние до водоисточника:" + strListDist[i] + " метров");
                        s.WriteLine("          Проверен:" + strMsgList[5] + " Объем:" + strMsgList[6] + " Подъездных путей:" + strMsgList[7] +
                                    " Тип:" + strMsgList[8]);
                        s.WriteLine("          Вид:" + strMsgList[9] + " Диаметр:" + strMsgList[10] + " Давление:" + strMsgList[11] +
                                    " Расход:" + strMsgList[12]);
                        s.WriteLine("          ПЧ:" + strMsgList[13] + " Примечание:" + strMsgList[14]);
                        s.WriteLine("");
                        if (i == 9) // печатает в путевку только первые 10 водоисточников
                            break;
                    }
                    s.Close();
                    f.Close();

                    StreamReader sr = File.OpenText(strPathPmtId);
                    string strRtr = sr.ReadToEnd();
                    strWaterPmt = strRtr;
                    sr.Close();

                    f = new FileStream(strPathPmtId, FileMode.Create);
                    Encoding enc = Encoding.GetEncoding(1251);
                    Byte[] encodedBytes = enc.GetBytes(strRtr);
                    f.Write(encodedBytes, 0, encodedBytes.Length);
                    f.Close();
                //}
            }
            return strWaterPmt;
        }

        public void SendMsgServer()
        {
            try
            {
                string msg = "111";
                // New code to send strings
                NetworkStream networkStream = new NetworkStream(FrmMain.Instance.m_clientSocket);
                System.IO.StreamWriter streamWriter = new System.IO.StreamWriter(networkStream);
                streamWriter.WriteLine(msg);
                streamWriter.Flush();
            }
            catch (SocketException se)
            {
                MessageBox.Show(se.Message);
            }	
        }

        private int StringParse(string sP)
        {
            char[] arr;
            int iCnt = 0;
            int isD = 0;
            arr = sP.ToCharArray();
            foreach (char c in arr)
            {
                if (Char.IsDigit(c))    // выделяет из номера дома только цифры если 112а то для деление улицы будет 112 
                    iCnt++;
                else
                    break;
            }
            if(iCnt > 0)
                isD = int.Parse(sP.Substring(0, iCnt));
            return isD;
        }

        private void StreetComboFnd()
        {
            textBoxObj.Text = "";
            c1ComboHome.Text = "";
            c1ListBort.ClearItems();
            textBoxMsg.Text = "";
            txtBoxRingTel.Text = "";
            c1ComboRang.Text = "";
            textBoxFire.Text = "";
            textBoxStreetType.Text = "";
            textBoxStreetPlace.Text = "";
            m_blWrPermitHand = false;   //улица есть в базе

            DataRow dr = dsStreet.Tables["Street"].Rows[bsStreet.Position];
            string strStrId = dr["StreetId"].ToString();
            DataView dvFndStreet = new DataView(dsStreet.Tables["Street"], "StreetId = '" + strStrId + "'",
                            "StreetName", DataViewRowState.CurrentRows);
            int rowIndex = dvFndStreet.Find(c1ComboFnd.Text);
            if (rowIndex != -1)
            {
                textBoxStreetType.Text = dr["StreetTypeName"].ToString();
                textBoxStreetPlace.Text = dr["StreetPlaceName"].ToString() + " " + dr["StreetRegionName"].ToString();
                BuildingsLoad(strStrId);
            }
            else
            {
                m_blWrPermitHand = true;   //улицы нет в базе - ручной ввод
                
                bsHome.Filter = "StreetId = '0'";
                c1ComboHome.DataSource = bsHome;
                c1ComboHome.ValueMember = "BuildName";
                
                c1ComboRang.Text = "1";
            }
            l_intStreetPos = bsStreet.Position; //запоминает позицию Street
            c1ComboHome.Focus();
        }

        private void c1ComboFnd_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Return)
            {
                e.Handled = true;
                if (l_intStreetPos != bsStreet.Position)    //если было событие закрыть сомбо данные не меняются
                {
                    StreetComboFnd();
                }
            }
            if (e.KeyChar == (char)Keys.Escape)
            {
                e.Handled = true;
                ClearItemsFrmPmt();
            }
        }

        private void c1ComboFnd_BeforeOpen(object sender, CancelEventArgs e)
        {
            l_intStreetPos = bsStreet.Position; // запоминает текущую позицию улицы
        }
        
        private void c1ComboFnd_Close(object sender, EventArgs e)
        {
            if (l_intStreetPos != bsStreet.Position)    //если просто открыть и закрыть сомбо данные не меняются
            {
                StreetComboFnd();
            }
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
            if (!m_blWrPermitHand)
                FindObjHome();  // ищет если не ручной ввод
            
            textBoxFire.Focus();
        }

        private void FindObjHome()
        {
            DataRow dr = dsStreet.Tables["Street"].Rows[bsStreet.Position];
            string strStreetId = dr["StreetId"].ToString();
            string strBuildHome = c1ComboHome.Text;

            DataSet dsHomeSel = new DataSet();
            BindingSource bsHomeSel = new BindingSource();

            string strSqlTextSelObj = "SELECT Buildings.BuildId, Buildings.StreetId, Buildings.BuildName, ObjUkg.ObjName, ObjUkgStaff.OrgName " +
                "FROM ((Buildings INNER JOIN ObjUkg ON Buildings.BuildId = ObjUkg.BuildId) " +
                "INNER JOIN ObjUkgStaff ON ObjUkg.ObjStaffId = ObjUkgStaff.ObjStaffId) " +
                "WHERE (((Buildings.StreetId)='" + strStreetId + "') AND ((Buildings.BuildName)='" + strBuildHome + "')) " +
                "ORDER BY ObjUkg.ObjName";
            // выбор объекта по номеру дома
            SqlDataAdapter daHomeSel = new SqlDataAdapter(strSqlTextSelObj, cnn);
            daHomeSel.Fill(dsHomeSel, "Buildings");
            bsHomeSel.DataSource = dsHomeSel;
            bsHomeSel.DataMember = "Buildings";

            int iCnt = bsHomeSel.Count;
            if (iCnt > 0)
            {
                m_blObjSign = false;
                string strStreetObj = textBoxStreetType.Text + " " + c1ComboFnd.Text + ", дом № " + c1ComboHome.Text;
                FrmObjUkg.Instance = new FrmObjUkg();
                FrmObjUkg.Instance.Text = "Объект по адресу: " + strStreetObj;
                FrmObjUkg.Instance.m_iLoadFrm = 1;
                FrmObjUkg.Instance.dsHomeSelObj = dsHomeSel;
                FrmObjUkg.Instance.ShowDialog();
            }
            else
            {
                if (strStreetId != "")   //только если найдена улица в базе
                {
                    DataView dvHome = new DataView(dsHomeFull.Tables["Buildings"], "StreetId = '" + strStreetId + "'",
                                "BuildName", DataViewRowState.CurrentRows);
                    int rowIndex = dvHome.Find(c1ComboHome.Text);
                    if (rowIndex != -1)
                    {
                        m_strBuildIdMap = dvHome[rowIndex]["BuildId"].ToString();
                        textBoxObj.Text = "Жилой дом";
                    }
                    else
                    {
                        m_strBuildIdMap = "";    //дом не найден
                        textBoxObj.Text = "Нет данных";
                    }

                    c1ComboRang.Text = "1";
                    m_TechnicsLoad("1", c1ComboHome.Text);
                }
            }

        }

        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            int arg = (int)e.Argument;
            LoadSqlData(arg);
        }

        private void backgroundWorker1_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            ClearItemsFrmPmt();
           
        }

        private void btnObjData_Click(object sender, EventArgs e)
        {
            string strHomeFnd = c1ComboHome.Text.Trim();
            if (strHomeFnd != "" && m_blWrHome)
            {
                string strObjName = textBoxStreetType.Text + " " + c1ComboFnd.Text + ", дом № " + c1ComboHome.Text;
                if (m_blObjSign)  //если номер дома принадлежит объекту
                {
                    FrmObjUkgData.Instance = new FrmObjUkgData();
                    FrmObjUkgData.Instance.Text = strObjName;
                    FrmObjUkgData.Instance.m_strBuildIdObj = m_strBuildIdMap;
                    FrmObjUkgData.Instance.Show();
                }
                else
                {
                    FrmHomeData.Instance = new FrmHomeData();
                    FrmHomeData.Instance.Text = strObjName;
                    FrmHomeData.Instance.m_strBuildIdHome = m_strBuildIdMap;
                    FrmHomeData.Instance.Show();
                    FrmHomeData.Instance.chkBoxGarret.Focus();
                }
            }
        }

        private void btnObjMap_Click(object sender, EventArgs e)
        {
            if(m_strBuildIdMap != "")   // не выводить карту если дома нет в базе данных карты
                FrmMain.Instance.LoadMapUkg(1, m_strBuildIdMap);
        }

        private void c1ComboRang_Close(object sender, EventArgs e)
        {
            if (!m_blWrPermitHand)  // меняет ранг если не ручной ввод
            {
                if (c1ComboRang.Text.Trim() != "" && c1ComboFnd.Text.Trim() != "")  // выбор техники по рангу только после выбора улицы
                    m_TechnicsLoad(c1ComboRang.Text, c1ComboHome.Text);
                else
                    c1ComboRang.Text = "";
            }
        }

        private void btnAddBort_Click(object sender, EventArgs e)
        {
            FrmAddBort.Instance = new FrmAddBort();
            FrmAddBort.Instance.m_iPemitBort = 1;
            FrmAddBort.Instance.ShowDialog();
        }

        private void btnRemovBort_Click(object sender, EventArgs e)
        {
            int iCnt = c1ListBort.ListCount;
            int iCntAdd = iCnt - c1ListBort.SelectedIndices.Count;
            string[] strN = new string[iCntAdd];
            int j = 0;

            for (int i = 0; i < iCnt; i = i + 1)
            {
                if (!c1ListBort.GetSelected(i))
                {
                    strN[j] = c1ListBort.GetItemText(i, 0);
                    j++;
                }

            }
            c1ListBort.ClearItems();
            for (j = 0; j < iCntAdd; j = j + 1)
                c1ListBort.AddItem(strN[j]);
        }

       
                
        
    }
}