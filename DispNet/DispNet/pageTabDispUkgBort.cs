using System;
using System.IO;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;
using C1.Win.C1FlexGrid;
using System.Collections;

namespace DispNet
{
    public partial class pageTabDispUkgBort : UserControl
    {
        //static public pageTabDispUkgBort Instance;

        DataSet dsDispUkgBort = new DataSet();
        string strRptDispId;
        string lstrPath;    //путь к папке Send для путевки
        string strFireObj;  //место пожара
        string strFireMsg;  //что горит
        string strFireRing; //кто позвонил
                
        public pageTabDispUkgBort()
        {
            InitializeComponent();
        }

        private void pageTabDispUkgBort_Load(object sender, EventArgs e)
        {
            strRptDispId = FrmDispFire.Instance.m_strRptDispId;
            lstrPath = FrmMain.Instance.m_strPmtPathSend;

            LoadDataRptBort(strRptDispId);

            c1FlxGrdUkgBort.Cols[0].AllowEditing = false;
            c1FlxGrdUkgBort.Cols[1].Visible = false;
            c1FlxGrdUkgBort.Cols[2].Visible = false;
            c1FlxGrdUkgBort.Cols[3].Visible = false;
            c1FlxGrdUkgBort.Cols[4].AllowEditing = false;
            c1FlxGrdUkgBort.Cols[4].Width = 65;
            c1FlxGrdUkgBort.Cols[4].Caption = "Борт";
            c1FlxGrdUkgBort.Cols[5].Width = 70;
            c1FlxGrdUkgBort.Cols[5].Caption = "Выезд";
            c1FlxGrdUkgBort.Cols[5].Format = "T";
            c1FlxGrdUkgBort.Cols[6].Width = 70;
            c1FlxGrdUkgBort.Cols[6].Caption = "Прибыл";
            c1FlxGrdUkgBort.Cols[6].Format = "T";
            c1FlxGrdUkgBort.Cols[7].Width = 70;
            c1FlxGrdUkgBort.Cols[7].Caption = "Убыл";
            c1FlxGrdUkgBort.Cols[7].Format = "T";
            c1FlxGrdUkgBort.Cols[8].Width = 70;
            c1FlxGrdUkgBort.Cols[8].Caption = "Возврат";
            c1FlxGrdUkgBort.Cols[8].Format = "T";
            c1FlxGrdUkgBort.Cols[9].Width = 70;
            c1FlxGrdUkgBort.Cols[9].Caption = "Дислок.->";
            c1FlxGrdUkgBort.Cols[9].Format = "T";
            
        }

        private void LoadDataRptBort(string strRptDispId)
        {
            string strSqlTextUkgBort = "SELECT ReportFireBort.RptFireBortId, ReportFireBort.RptDispId, " +
                      "ReportFireBort.TechnicsId, TechnicsUnit.TechnicsName, ReportFireBort.BortDrive, " +
                      "ReportFireBort.BortArrive, ReportFireBort.BortDeparture, ReportFireBort.BortReturn, ReportFireBort.BortDislocate " +
                      "FROM ReportFireBort INNER JOIN TechnicsUnit ON ReportFireBort.TechnicsId = TechnicsUnit.TechnicsId " +
                      "WHERE (ReportFireBort.RptDispId = " + strRptDispId + ") ORDER BY ReportFireBort.RptFireBortId";
            SqlDataAdapter daDispUkgBort = new SqlDataAdapter(strSqlTextUkgBort, FrmMain.Instance.m_cnn);
            daDispUkgBort.Fill(dsDispUkgBort, "ReportFireBort");
            c1FlxGrdUkgBort.SetDataBinding(dsDispUkgBort, "ReportFireBort");

            /*int iRws = c1FlxGrdUkgBort.Rows.Count - 1;
            CellStyle cs = c1FlxGrdUkgBort.Styles.Add("time");
            cs.DataType = typeof(string);
            cs.ComboList = "|...";
            cs.EditMask = "##:##:##";

            cs = c1FlxGrdUkgBort.Styles.Add("disloc");
            cs.DataType = typeof(string);
            cs.ComboList = "...";
            cs.EditMask = "##:##:##";

            // assign styles to editable cells
            if (iRws > 0)
            {
                CellRange rg = c1FlxGrdUkgBort.GetCellRange(0, 5, iRws, 8);
                rg.Style = c1FlxGrdUkgBort.Styles["time"];

                rg = c1FlxGrdUkgBort.GetCellRange(0, 9, iRws, 9);
                rg.Style = c1FlxGrdUkgBort.Styles["disloc"];
            }*/
        }

        private void RecTimeRptBort(string strTime, string strBortId, string strColId, int iRowId, int iColId)
        {
            string strSqlTextBortTime = "";
            strSqlTextBortTime = "UPDATE ReportFireBort SET " + strColId + " = CONVERT(DATETIME, '" + strTime + "', 102) " +
                                   "WHERE RptFireBortId = " + strBortId;
            SqlCommand command = new SqlCommand(strSqlTextBortTime, FrmMain.Instance.m_cnn);
            Int32 recordsAffected = command.ExecuteNonQuery();

            int iCntRows = c1FlxGrdUkgBort.Rows.Count - 1;  //обнавляет данные в таблице c1FlxGrdUkgBort
            for (int i = iCntRows; i > 0; i--)
                c1FlxGrdUkgBort.Rows.Remove(i);
            LoadDataRptBort(strRptDispId);
            c1FlxGrdUkgBort.Select(iRowId, iColId);
        }

        private void c1FlxGrdUkgBort_CellButtonClick(object sender, RowColEventArgs e)
        {
            string strColName = "";
            bool blDisloc = true;

            switch (e.Col)
            {
                case 5:
                    strColName = "Ввести время выезда?";
                    break;
                case 6:
                    strColName = "Ввести время прибытия?";
                    break;
                case 7:
                    strColName = "Ввести время убытия?";
                    break;
                case 8:
                    strColName = "Ввести время возвращения?";
                    break;
                case 9:
                    if( c1FlxGrdUkgBort.GetDataDisplay(e.Row, 9) == "")
                        blDisloc = true;
                    else
                        blDisloc = false;
                    strColName = "Ввести время передислокации?";
                    break;
            }
            string strTechnicsId = c1FlxGrdUkgBort.GetDataDisplay(e.Row, 3);
            string strBortName = c1FlxGrdUkgBort.GetDataDisplay(e.Row, 4);
            string strBortId = c1FlxGrdUkgBort.GetDataDisplay(e.Row, 1);
            string strColId = c1FlxGrdUkgBort.Cols[e.Col].Name;
            strBortName = "Борт №" + strBortName;

            if (blDisloc)   // не дает второй раз провести дислокацию
            {
                if (MessageBox.Show(strColName, strBortName,
                        MessageBoxButtons.YesNo, MessageBoxIcon.Question)
                        == DialogResult.Yes)
                {
                    DateTime myDateTime = DateTime.Now;
                    string strTime = myDateTime.Month.ToString() + "-" + myDateTime.Day.ToString() + "-" + myDateTime.Year.ToString() + " " +
                                        myDateTime.ToString("HH:mm:ss");
                    switch (e.Col)
                    {
                        case 6: //На пожаре в работе
                            FrmPermit.Instance.ReplaceBortState("6", strTechnicsId);
                            RecTimeRptBort(strTime, strBortId, strColId, e.Row, e.Col);
                            break;
                        case 7: //Убытие с места вызова
                            FrmPermit.Instance.ReplaceBortState("15", strTechnicsId);
                            RecTimeRptBort(strTime, strBortId, strColId, e.Row, e.Col);
                            break;
                        case 8: //В pасчете
                            FrmPermit.Instance.ReplaceBortState("1", strTechnicsId);
                            RecTimeRptBort(strTime, strBortId, strColId, e.Row, e.Col);
                            break;
                        case 9: //Передислокация техники
                            FrmDisloc.Instance = new FrmDisloc();
                            FrmDisloc.Instance.m_strBortDslId = c1FlxGrdUkgBort.GetDataDisplay(e.Row, 3);  //№ борта для дислокации может не надо?
                            FrmDisloc.Instance.Text = "Передислокация - " + strBortName;
                            FrmDisloc.Instance.ShowDialog();
                            FrmDisloc.Instance.m_iFrmDsl = 1;
                            if (FrmDispUkg.Instance.m_iDislOk == 1) // 0 если закрыли форму FrmDisloc без записи
                            {
                                FrmPermit.Instance.ReplaceBortState("4", strTechnicsId);
                                RecTimeRptBort(strTime, strBortId, strColId, e.Row, e.Col);
                            }
                            break;
                    }
                }
            }
        }

        private void c1FlxGrdUkgBort_AfterEdit(object sender, RowColEventArgs e)
        {
            string strTime = DateTime.Parse(c1FlxGrdUkgBort.GetDataDisplay(e.Row, e.Col)).ToString("MM-d-yyyy HH:mm:ss");
            string strBortId = c1FlxGrdUkgBort.GetDataDisplay(e.Row, 1);
            string strColId = c1FlxGrdUkgBort.Cols[e.Col].Name;
            RecTimeRptBort(strTime, strBortId, strColId, e.Row, e.Col);

            c1FlxGrdUkgBort.Cols[e.Col].Width = 70;
            c1FlxGrdUkgBort.Cols[e.Col].Format = "T";
            c1FlxGrdUkgBort.Cols[e.Col].ComboList = "";
            c1FlxGrdUkgBort.Cols[e.Col].EditMask = "##:##:##";
        }

        private void btnTest_Click(object sender, EventArgs e)
        {
            c1ListBortAdd.ClearItems();
            
        }

        private void btnDopBortAdd_Click(object sender, EventArgs e)
        {
            FrmAddBort.Instance = new FrmAddBort();
            FrmAddBort.Instance.m_iPemitBort = 2;
            FrmAddBort.Instance.ShowDialog();
        }

        private void btnDopBortRem_Click(object sender, EventArgs e)
        {
            int iCnt = c1ListBortAdd.ListCount;
            int iCntAdd = iCnt - c1ListBortAdd.SelectedIndices.Count;
            string[] strN = new string[iCntAdd];
            int j = 0;

            for (int i = 0; i < iCnt; i = i + 1)
            {
                if (!c1ListBortAdd.GetSelected(i))
                {
                    strN[j] = c1ListBortAdd.GetItemText(i, 0);
                    j++;
                }
            }
            c1ListBortAdd.ClearItems();
            for (j = 0; j < iCntAdd; j = j + 1)
                c1ListBortAdd.AddItem(strN[j]);
        }

        private void btnDopBortPmt_Click(object sender, EventArgs e)
        {
            int iCnt = c1ListBortAdd.ListCount;
            if (iCnt >0)
            {
                if (MessageBox.Show("Записать информацию?", "Путевка - дополнительный выезд техники",
                    MessageBoxButtons.YesNo, MessageBoxIcon.Question)
                    == DialogResult.Yes)
                {
                    string strSqlTextFireData = "SELECT ReportDisp.RptDispMsg, ReportFire.RptMsgFire, ReportFire.RptRing, ReportDisp.RptDispId " +
                        "FROM ReportDisp INNER JOIN ReportFire ON ReportDisp.RptDispId = ReportFire.RptDispId " +
                        "WHERE(ReportDisp.RptDispId = " + strRptDispId + ")";
                    SqlCommand cmd = new SqlCommand(strSqlTextFireData, FrmMain.Instance.m_cnn);
                    SqlDataReader rdrFireData = cmd.ExecuteReader();
                    while (rdrFireData.Read())
                    {
                        if (!rdrFireData.IsDBNull(0))
                            strFireObj = rdrFireData.GetString(0);
                        if (!rdrFireData.IsDBNull(1))
                            strFireMsg = rdrFireData.GetString(1);
                        if (!rdrFireData.IsDBNull(2))
                            strFireRing = rdrFireData.GetString(2);
                    }
                    rdrFireData.Close();

                    DataSet dsTechnicsUnit = new DataSet();
                    BindingSource bsTechnicsUnit = new BindingSource();
                    string strSqlTextTechnicsUnit = "SELECT TechnicsUnit.TechnicsId, TechnicsUnit.StateId, TechnicsState.StateName, " +
                        "TechnicsUnit.UnitId, TechnicsUnit.UnitAllow, TechnicsUnit.TechnicsName " +
                        "FROM TechnicsUnit INNER JOIN TechnicsState ON TechnicsUnit.StateId = TechnicsState.StateId " +
                        "ORDER BY TechnicsUnit.TechnicsId";
                    SqlDataAdapter daTechnicsUnit = new SqlDataAdapter(strSqlTextTechnicsUnit, FrmMain.Instance.m_cnn);
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
                    bool blAllowDp = false;
                    ArrayList arrPmtRecDp = new ArrayList();  // содержит все строки путевок для записи в базу

                    for (int i = 0; i < iCnt; i = i + 1)
                    {
                        strBortName = c1ListBortAdd.GetItemText(i, 0);
                        int rowIndex = dvTechnicsUnit.Find(strBortName);
                        if (rowIndex != -1)
                        {
                            TechnicsId = dvTechnicsUnit[rowIndex]["TechnicsId"].ToString();
                            iTechnicsState = Convert.ToInt32(dvTechnicsUnit[rowIndex]["StateId"].ToString());
                            blAllowDp = Convert.ToBoolean(dvTechnicsUnit[rowIndex]["UnitAllow"]);
                            if (iTechnicsState == 1)
                            {
                                string strSqlTextReportFireBort = "INSERT INTO ReportFireBort " +
                                "(RptFireBortId, RptDispId, TechnicsId, BortDrive) " +
                                "Values('0', '" + strRptDispId + "', '" + TechnicsId + "', CONVERT(DATETIME, '" + strBortDrive + "', 102))";

                                SqlCommand command = new SqlCommand(strSqlTextReportFireBort, FrmMain.Instance.m_cnn);
                                Int32 recordsAffected = command.ExecuteNonQuery();

                                FrmPermit.Instance.ReplaceBortState("4", TechnicsId);
                            }
                            else
                            {
                                strTechnicsName = dvTechnicsUnit[rowIndex]["TechnicsName"].ToString();
                                strTechnicsState = dvTechnicsUnit[rowIndex]["StateName"].ToString();
                                strMsgState = "Борт " + strTechnicsName + " имеет состояние - " + strTechnicsState +
                                   ". Заменить на значение - Приказ на выезд?";
                                if (MessageBox.Show(strMsgState, "Путевка - состояние техники",
                                                    MessageBoxButtons.YesNo, MessageBoxIcon.Question)
                                                    == DialogResult.Yes)
                                {
                                    string strSqlTextReportFireBort = "INSERT INTO ReportFireBort " +
                                    "(RptFireBortId, RptDispId, TechnicsId, BortDrive) " +
                                    "Values('0', '" + strRptDispId + "', '" + TechnicsId + "', CONVERT(DATETIME, '" + strBortDrive + "', 102))";

                                    SqlCommand command = new SqlCommand(strSqlTextReportFireBort, FrmMain.Instance.m_cnn);
                                    Int32 recordsAffected = command.ExecuteNonQuery();

                                    FrmPermit.Instance.ReplaceBortState("4", TechnicsId);
                                }
                            }
                            if (blAllowDp)
                            {
                                string strPchId = dvTechnicsUnit[rowIndex]["UnitId"].ToString();
                                string strPemitPmtRec = WritePemitDop(strPchId, strBortName, strRptDispId);
                                string strPmtRec = strRptDispId + "#" + TechnicsId + "#" + strPchId + "#" + strPemitPmtRec;
                                arrPmtRecDp.Add(strPmtRec);
                            }
                        }
                    }
                    
                    if (arrPmtRecDp.Count > 0)
                        RecDataRptPemitDp(arrPmtRecDp);
                    
                    if (FrmMain.Instance.m_blSendSrv && arrPmtRecDp.Count > 0) // не посылает серверу сообщение если нет ни одного борта которому разрешен выезд
                        FrmPermit.Instance.SendMsgServer();

                    int iCntRows = c1FlxGrdUkgBort.Rows.Count - 1;  //обнавляет данные в таблице c1FlxGrdUkgBort
                    for (int i = iCntRows; i > 0; i--)
                        c1FlxGrdUkgBort.Rows.Remove(i);
                    LoadDataRptBort(strRptDispId);
                    c1ListBortAdd.ClearItems();
                }
            }
            else
            {
                MessageBox.Show("Список техники пуст!", "Путевка - выезд техники",
                                MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
        }

        private void RecDataRptPemitDp(ArrayList arrPmt)
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

                SqlCommand cmdPmtPemit = new SqlCommand(strSqlTextReportPemitPmt, FrmMain.Instance.m_cnn);
                Int32 recordsPmtPemit = cmdPmtPemit.ExecuteNonQuery();
            }
          
        }

        private string WritePemitDop(string strPchId, string strBortName, string strRptId)
        {
            string strPemitPmtDop = "";    // строка с текстом путевки
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
            s.WriteLine(("          1.Место пожара: " + strFireObj));
            s.WriteLine("");
            s.WriteLine(("          2.Что горит: " + strFireMsg));
            s.WriteLine("");
            s.WriteLine(("          3.Время получения извещения: " + strTime));
            s.WriteLine("");
            s.WriteLine(("          4.Фамилия и № телефона сообщившего: " + strFireRing));
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
            strPemitPmtDop = strRtr;
            sr.Close();

            f = new FileStream(strPathPmtId, FileMode.Create);
            Encoding enc = Encoding.GetEncoding(1251);
            Byte[] encodedBytes = enc.GetBytes(strRtr);
            f.Write(encodedBytes, 0, encodedBytes.Length);
            f.Close();

            return strPemitPmtDop;
        }

        private void c1FlxGrdUkgBort_BeforeEdit(object sender, RowColEventArgs e)
        {
            c1FlxGrdUkgBort.Cols[e.Col].ComboList = "|...";
            c1FlxGrdUkgBort.Cols[e.Col].EditMask = "##-##-#### ##:##:##";
            c1FlxGrdUkgBort.Cols[e.Col].Width = 130;
            c1FlxGrdUkgBort.Cols[e.Col].Format = "G";
        }
    }
}
