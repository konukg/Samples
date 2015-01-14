using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;
using C1.Win.C1FlexGrid;
using System.Threading;
using System.Runtime.InteropServices;
//using System.ServiceModel.Channels;


namespace DispNet
{
    public partial class FrmDispUkgDrv : Form
    {
        static public FrmDispUkgDrv Instance;
        string strRptDispId;
        DataSet dsDispUkgBort = new DataSet();
        public int m_iDislOk = 0;

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

        
        public FrmDispUkgDrv()
        {
            InitializeComponent();
        }

        private void FrmDispUkgDrv_Load(object sender, EventArgs e)
        {
            strRptDispId = FrmDispFire.Instance.m_strRptDispId;
            l_iRowSel = FrmDispFire.Instance.m_iDispFireRowSel;
                       
            LoadDataRptBort(strRptDispId);

            c1FlxGrdUkgDrv.Cols[0].AllowEditing = false;
            c1FlxGrdUkgDrv.Cols[1].Visible = false;
            c1FlxGrdUkgDrv.Cols[2].Visible = false;
            c1FlxGrdUkgDrv.Cols[3].Visible = false;
            c1FlxGrdUkgDrv.Cols[4].Width = 65;
            c1FlxGrdUkgDrv.Cols[4].Caption = "Борт";
            c1FlxGrdUkgDrv.Cols[4].AllowEditing = false;
            c1FlxGrdUkgDrv.Cols[5].Width = 70;
            c1FlxGrdUkgDrv.Cols[5].Caption = "Выезд";
            c1FlxGrdUkgDrv.Cols[5].Format = "T";
            c1FlxGrdUkgDrv.Cols[6].Width = 70;
            c1FlxGrdUkgDrv.Cols[6].Caption = "Прибыл";
            c1FlxGrdUkgDrv.Cols[6].Format = "T";
            c1FlxGrdUkgDrv.Cols[7].Width = 70;
            c1FlxGrdUkgDrv.Cols[7].Caption = "Убыл";
            c1FlxGrdUkgDrv.Cols[7].Format = "T";
            c1FlxGrdUkgDrv.Cols[8].Width = 70;
            c1FlxGrdUkgDrv.Cols[8].Caption = "Возврат";
            c1FlxGrdUkgDrv.Cols[8].Format = "T";
            c1FlxGrdUkgDrv.Cols[9].Width = 70;
            c1FlxGrdUkgDrv.Cols[9].Caption = "Дислок.->";
            c1FlxGrdUkgDrv.Cols[9].Format = "T";

            string strSqlTextUkgDrv = "SELECT RptBortDrvPlace, RptBortDrvTake, RptUkgPtz, RptUkgPtu, RptDrvUkgNote, RptDrvUkgSign, RptCatDrvUkgId " +
                                        "FROM ReportBortDrive WHERE (RptDispId = " + strRptDispId + ")";
            SqlCommand cmd = new SqlCommand(strSqlTextUkgDrv, FrmMain.Instance.m_cnn);
            SqlDataReader rdrUkgDrv = cmd.ExecuteReader();

            while (rdrUkgDrv.Read())
            {
                if (!rdrUkgDrv.IsDBNull(0))
                    txtBoxPlace.Text = rdrUkgDrv.GetString(0);
                if (!rdrUkgDrv.IsDBNull(1))
                    txtBoxTake.Text = rdrUkgDrv.GetString(1);
                if (!rdrUkgDrv.IsDBNull(2))
                    if(rdrUkgDrv.GetBoolean(2))
                        chkBoxPtzD.CheckState = CheckState.Checked;
                if (!rdrUkgDrv.IsDBNull(3))
                    if (rdrUkgDrv.GetBoolean(3))
                        chkBoxPtuD.CheckState = CheckState.Checked;
                if (!rdrUkgDrv.IsDBNull(4))
                    txtBoxDrvNote.Text = rdrUkgDrv.GetString(4);
                if (!rdrUkgDrv.IsDBNull(5))
                    l_iDrvSign = rdrUkgDrv.GetInt16(5);
                if (!rdrUkgDrv.IsDBNull(6))
                    l_iCatDrvId = rdrUkgDrv.GetInt16(6);

            }
            rdrUkgDrv.Close();

            l_strCatSingDrv = l_iDrvSign.ToString();    //назначает категорию если сделать запись без вызова выбора категории

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
                    CtgMsgListLoad(strSqlLrnCat); //загружает текст категории из таблицы InfoCatFire
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
            ThreadStartFrmDrvUkg();  // старт потока для вызова из данного потока формы с перечнем категорий выезда
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

        private void LoadDataRptBort(string strRptDispId)
        {
            string strSqlTextUkgBort = "SELECT ReportFireBort.RptFireBortId, ReportFireBort.RptDispId, " +
                      "ReportFireBort.TechnicsId, TechnicsUnit.TechnicsName, ReportFireBort.BortDrive, " +
                      "ReportFireBort.BortArrive, ReportFireBort.BortDeparture, ReportFireBort.BortReturn, ReportFireBort.BortDislocate " +
                      "FROM ReportFireBort INNER JOIN TechnicsUnit ON ReportFireBort.TechnicsId = TechnicsUnit.TechnicsId " +
                      "WHERE (ReportFireBort.RptDispId = " + strRptDispId + ") ORDER BY ReportFireBort.RptFireBortId";
            SqlDataAdapter daDispUkgBort = new SqlDataAdapter(strSqlTextUkgBort, FrmMain.Instance.m_cnn);
            daDispUkgBort.Fill(dsDispUkgBort, "ReportFireBort");
            c1FlxGrdUkgDrv.SetDataBinding(dsDispUkgBort, "ReportFireBort");

            /*int iRws = c1FlxGrdUkgDrv.Rows.Count - 1;
            CellStyle cs = c1FlxGrdUkgDrv.Styles.Add("time");
            cs.DataType = typeof(string);
            cs.ComboList = "|...";
            //cs.EditMask = "##:##:##";

            cs = c1FlxGrdUkgDrv.Styles.Add("disloc");
            cs.DataType = typeof(string);
            cs.ComboList = "...";
            //cs.EditMask = "##:##:##";

            // assign styles to editable cells
            if (iRws > 0)
            {
                CellRange rg = c1FlxGrdUkgDrv.GetCellRange(0, 5, iRws, 8);
                rg.Style = c1FlxGrdUkgDrv.Styles["time"];

                rg = c1FlxGrdUkgDrv.GetCellRange(0, 9, iRws, 9);
                rg.Style = c1FlxGrdUkgDrv.Styles["disloc"];
            }*/
        }

        private void RecTimeRptBort(string strTime, string strBortId, string strColId, int iRowId, int iColId)
        {
            string strSqlTextBortTime = "";
            strSqlTextBortTime = "UPDATE ReportFireBort SET " + strColId + " = CONVERT(DATETIME, '" + strTime + "', 102) " +
                                     "WHERE RptFireBortId = " + strBortId;
            SqlCommand command = new SqlCommand(strSqlTextBortTime, FrmMain.Instance.m_cnn);
            Int32 recordsAffected = command.ExecuteNonQuery();

            int iCntRows = c1FlxGrdUkgDrv.Rows.Count - 1;  //обнавляет данные в таблице c1FlxGrdUkgDrv
            for (int i = iCntRows; i > 0; i--)
                c1FlxGrdUkgDrv.Rows.Remove(i);
            LoadDataRptBort(strRptDispId);
            c1FlxGrdUkgDrv.Select(iRowId, iColId);
        }

        private void c1FlxGrdUkgDrv_AfterEdit(object sender, RowColEventArgs e)
        {
            string strTime = DateTime.Parse(c1FlxGrdUkgDrv.GetDataDisplay(e.Row, e.Col)).ToString("MM-d-yyyy HH:mm:ss");
            string strBortId = c1FlxGrdUkgDrv.GetDataDisplay(e.Row, 1);
            string strColId = c1FlxGrdUkgDrv.Cols[e.Col].Name;
            RecTimeRptBort(strTime, strBortId, strColId, e.Row, e.Col);

            c1FlxGrdUkgDrv.Cols[e.Col].Width = 70;
            c1FlxGrdUkgDrv.Cols[e.Col].Format = "T";
            c1FlxGrdUkgDrv.Cols[e.Col].ComboList = "";
            c1FlxGrdUkgDrv.Cols[e.Col].EditMask = "##:##:##";
        }

        private void c1FlxGrdUkgDrv_CellButtonClick(object sender, RowColEventArgs e)
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
                    if (c1FlxGrdUkgDrv.GetDataDisplay(e.Row, 9) == "")
                        blDisloc = true;
                    else
                        blDisloc = false;
                    strColName = "Ввести время передислокации?";
                    break;
            }
            string strTechnicsId = c1FlxGrdUkgDrv.GetDataDisplay(e.Row, 3);
            string strBortName = c1FlxGrdUkgDrv.GetDataDisplay(e.Row, 4);
            string strBortId = c1FlxGrdUkgDrv.GetDataDisplay(e.Row, 1);
            string strColId = c1FlxGrdUkgDrv.Cols[e.Col].Name;
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
                            FrmDisloc.Instance.m_strBortDslId = c1FlxGrdUkgDrv.GetDataDisplay(e.Row, 3);  //№ борта для дислокации
                            FrmDisloc.Instance.Text = "Передислокация - " + strBortName;
                            FrmDisloc.Instance.ShowDialog();
                            FrmDisloc.Instance.m_iFrmDsl = 2;
                            if (m_iDislOk == 2) // 0 если закрыли форму FrmDisloc без записи
                            {
                                FrmPermit.Instance.ReplaceBortState("4", strTechnicsId);
                                RecTimeRptBort(strTime, strBortId, strColId, e.Row, e.Col);
                            }
                            break;
                    }
                }
            }
        }

        private void txtBoxPlace_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Return)
            {
                e.Handled = true;
                /*if (MessageBox.Show("Записать информацию?", "Место",
                    MessageBoxButtons.YesNo, MessageBoxIcon.Question)
                    == DialogResult.Yes)
                {
                    string strSqlTextPlace = "UPDATE ReportBortDrive SET RptBortDrvPlace = '" + txtBoxPlace.Text +
                                                "' WHERE RptDispId = " + strRptDispId;
                    SqlCommand command = new SqlCommand(strSqlTextPlace, FrmMain.Instance.m_cnn);
                    Int32 recordsAffected = command.ExecuteNonQuery();
                }*/
            }
        }

        private void txtBoxTake_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Return)
            {
                e.Handled = true;
                /*if (MessageBox.Show("Записать информацию?", "Проводит",
                    MessageBoxButtons.YesNo, MessageBoxIcon.Question)
                    == DialogResult.Yes)
                {
                    string strSqlTextTake = "UPDATE ReportBortDrive SET RptBortDrvTake = '" + txtBoxTake.Text +
                                                "' WHERE RptDispId = " + strRptDispId;
                    SqlCommand command = new SqlCommand(strSqlTextTake, FrmMain.Instance.m_cnn);
                    Int32 recordsAffected = command.ExecuteNonQuery();
                }*/
            }
        }

        private void c1FlxGrdUkgDrv_BeforeEdit(object sender, RowColEventArgs e)
        {
            c1FlxGrdUkgDrv.Cols[e.Col].ComboList = "|...";
            c1FlxGrdUkgDrv.Cols[e.Col].EditMask = "##-##-#### ##:##:##";
            c1FlxGrdUkgDrv.Cols[e.Col].Width = 130;
            c1FlxGrdUkgDrv.Cols[e.Col].Format = "G";
        }

        private void btnRecDrv_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Записать информацию?", "Выезд техники (город)",
                    MessageBoxButtons.YesNo, MessageBoxIcon.Question)
                    == DialogResult.Yes)
            {
                if (FrmDispFire.Instance.Visible)
                {
                    //FrmDispFire.Instance.c1FlxGrdDispMain.Cols[3].AllowEditing = true;
                    FrmDispFire.Instance.c1FlxGrdDispMain.SetData(l_iRowSel, 3, m_lblCatDrv.Text, true);   //вставляет в таблицу данные об объекте
                    //FrmDispFire.Instance.c1FlxGrdDispMain.Cols[3].AllowEditing = false;
                }

                string strSqlTextDrvDisp = "UPDATE ReportDisp SET RptDispMsg = '" + m_lblCatDrv.Text + "' " +
                    "WHERE RptDispId = " + strRptDispId;
                SqlCommand commandDisp = new SqlCommand(strSqlTextDrvDisp, FrmMain.Instance.m_cnn);
                Int32 recordsAffectedDisp = commandDisp.ExecuteNonQuery();

                string strUkgPtz = "0";
                string strUkgPtu = "0";
                if (chkBoxPtzD.Checked)
                    strUkgPtz = "1";
                if (chkBoxPtuD.Checked)
                    strUkgPtu = "1";

                string strSqlTextDrvDrive = "UPDATE ReportBortDrive SET RptBortDrvTake = '" + txtBoxTake.Text + "', RptBortDrvPlace = '" + txtBoxPlace.Text + "', " +
                    "RptUkgPtz = '" + strUkgPtz + "', RptUkgPtu = '" + strUkgPtu + "', RptDrvUkgNote = '" + txtBoxDrvNote.Text + "', " +
                    "RptDrvUkgSign = '" + l_strCatSingDrv + "', RptCatDrvUkgId = '" + l_iCatDrvId + "' " +
                    "WHERE RptDispId = " + strRptDispId;
                SqlCommand commandDrive = new SqlCommand(strSqlTextDrvDrive, FrmMain.Instance.m_cnn);
                Int32 recordsAffectedDrive = commandDrive.ExecuteNonQuery();
            }
        }

        private void rdBtnNoCatDrv_CheckedChanged(object sender, EventArgs e)
        {
            if (rdBtnNoCatDrv.Checked)
            {
                l_iCatDrvId = 0;
                m_lblCatDrv.Text = "Выезд техники без категории";
                m_strCatSingDrv = "0";
            }
        }

        private void rdBtnDrvLrn_CheckedChanged(object sender, EventArgs e)
        {
            if (rdBtnDrvLrn.Checked)
            {
                l_iCatDrvId = 1;
                m_lblCatDrv.Text = "";
                m_strCatSingDrv = "0";
            }
        }

        private void rdBtnDrvPtr_CheckedChanged(object sender, EventArgs e)
        {
            if (rdBtnDrvPtr.Checked)
            {
                l_iCatDrvId = 2;
                m_lblCatDrv.Text = "";
                m_strCatSingDrv = "0";
            }
        }

        private void rdBtnDrvOth_CheckedChanged(object sender, EventArgs e)
        {
            if (rdBtnDrvOth.Checked)
            {
                l_iCatDrvId = 3;
                m_lblCatDrv.Text = "";
                m_strCatSingDrv = "0";
            }
        }

        private void btnCatDrvChs_Click(object sender, EventArgs e)
        {
            ThreadLoadCtg(l_iCatDrvId);
        }

        /*protected override void WndProc(ref Message m)
        {
            // Listen for operating system messages.
            switch (m.Msg)
            {
                // The WM_ACTIVATEAPP message occurs when the application
                // becomes the active application or becomes inactive.
                case WM_ACTIVATEAPP_MN:
                    this.Invalidate();                    
                    break;
            }
            base.WndProc(ref m);
        }*/

        private void ThreadStartFrmDrvUkg()
        {
            selThread = new Thread(new ThreadStart(ThreadProcDrvUkg));
            selThread.Start();
        }

        private void ThreadProcDrvUkg()
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
            FrmChsCatDrv.Instance.m_iSendMsgFrm = 4;    //категорию послать в эту форму
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
            FrmChsCatDrv.Instance.m_iSendMsgFrm = 4;    //категорию послать в эту форму
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
            FrmChsCatDrv.Instance.m_iSendMsgFrm = 4;    //категорию послать в эту форму
            FrmChsCatDrv.Instance.m_hndFrmCat = m_lblCatDrv.Handle;
            FrmChsCatDrv.Instance.ShowDialog();
            l_strCatSingDrv = m_strCatSingDrv;

        }
    }
}