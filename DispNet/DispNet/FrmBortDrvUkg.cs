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
    public partial class FrmBortDrvUkg : Form
    {
        static public FrmBortDrvUkg Instance;

        int l_iCatDrvId = 0; //категори€ 0-нет, 1-зан€тие, 2 - дозор, 3 - прочие
        public string m_strCatDrvRpt = "0"; // тип категории выезда
        bool blCatRsLrn = false;  //
        bool blCatRsPtr = false;   // загружает данные категорий один раз пр первом нажатие кнопки
        bool blCatRsOth = false;  //

        DataSet dsCtgDrvList = new DataSet();
        BindingSource bsCtgDrvList = new BindingSource();

        // запуск формы из разных потоков
        private Thread selThread = null;
        delegate void SetCtgCallback();
        //.....

        public FrmBortDrvUkg()
        {
            InitializeComponent();
        }

        private void FrmBortDrvUkg_Load(object sender, EventArgs e)
        {
            m_lblCatDrv.Text = "¬ыезд техники без категории";
            ThreadStartFrmDrvUkg();  // старт потока дл€ вызова из данного потока формы с перечнем категорий выезда
        }

        private void btnAddBort_Click(object sender, EventArgs e)
        {
            FrmAddBort.Instance = new FrmAddBort();
            FrmAddBort.Instance.m_iPemitBort = 3;
            FrmAddBort.Instance.ShowDialog();
        }

        private void btnDelBort_Click(object sender, EventArgs e)
        {
            int iCnt = c1ListBortDrv.ListCount;
            int iCntAdd = iCnt - c1ListBortDrv.SelectedIndices.Count;
            string[] strN = new string[iCntAdd];
            int j = 0;

            for (int i = 0; i < iCnt; i = i + 1)
            {
                if (!c1ListBortDrv.GetSelected(i))
                {
                    strN[j] = c1ListBortDrv.GetItemText(i, 0);
                    j++;
                }

            }
            c1ListBortDrv.ClearItems();
            for (j = 0; j < iCntAdd; j = j + 1)
                c1ListBortDrv.AddItem(strN[j]);
        }

        private void btnRecDrv_Click(object sender, EventArgs e)
        {
            if (c1ListBortDrv.ListCount > 0)
            {
                if (MessageBox.Show("«аписать информацию?", "√ород - выезд техники",
                    MessageBoxButtons.YesNo, MessageBoxIcon.Question)
                    == DialogResult.Yes)
                {
                    RecDataRptBortDrive();
                }
            }
            else MessageBox.Show("—писок техники пуст!", "√ород - выезд техники",
                   MessageBoxButtons.OK, MessageBoxIcon.Stop);
        }

        private void RecDataRptBortDrive()
        {
            DateTime myDateTime = DateTime.Now;
            string strDate = myDateTime.Month.ToString() + "-" + myDateTime.Day.ToString() + "-" + myDateTime.Year.ToString() + " " +
                                myDateTime.ToString("HH:mm:ss");
            string strUkg = "1";
            string strUkgObl = "1";
            string strSqlTextReportDisp = "INSERT INTO ReportDisp " +
            "(RptDispId, RptDate, RptDispMsg, RptDistrict, RptCity, DepartTypeId, StateChsId, RptRecRpt) " +
            "Values('0', CONVERT(DATETIME, '" + strDate + "', 102), '" + m_lblCatDrv.Text + "', '" + strUkgObl + "', '" + strUkg + "', '3', '1', '0')";

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
                string strUkgPtz = "0";
                string strUkgPtu = "0";
                if (chkBoxPtz.Checked)
                    strUkgPtz = "1";
                if (chkBoxPtu.Checked)
                    strUkgPtu = "1";
                string strSqlTextReportBortDrive = "INSERT INTO ReportBortDrive " +
                "(RptBortDrvId, RptDispId, RptBortDrvPlace, RptBortDrvTake, RptUkgPtz, RptUkgPtu, RptDrvUkgNote, RptDrvUkgSign, RptCatDrvUkgId) " +
                "Values('0', '" + strRptDispId + "', '" + txtBoxPlace.Text + "', '" + txtBoxTake.Text + "', '" + strUkgPtz + "', '" + strUkgPtu + "', " +
                "'" + txtBoxDrvNote.Text + "', '" + m_strCatDrvRpt + "', '" + l_iCatDrvId + "')";

                SqlCommand cmdOblId = new SqlCommand(strSqlTextReportBortDrive, FrmMain.Instance.m_cnn);
                Int32 rdsAffectedOblId = cmdOblId.ExecuteNonQuery();

                RecDataRptFireBort(strRptDispId);
                Close();    // закрывает форму
            }// дополнить сообщением если созданна€ запись не найдена
        }

        private void RecDataRptFireBort(string strRptDispId)
        {
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
            int iCnt = c1ListBortDrv.ListCount;
            for (int i = 0; i < iCnt; i = i + 1)
            {
                strBortName = c1ListBortDrv.GetItemText(i, 0);
                int rowIndex = dvTechnicsUnit.Find(strBortName);
                if (rowIndex != -1)
                {
                    TechnicsId = dvTechnicsUnit[rowIndex]["TechnicsId"].ToString();
                    iTechnicsState = Convert.ToInt32(dvTechnicsUnit[rowIndex]["StateId"].ToString());
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
                        strMsgState = "Ѕорт " + strTechnicsName + " имеет состо€ние - " + strTechnicsState +
                           ". «аменить на значение - ѕриказ на выезд?";
                        if (MessageBox.Show(strMsgState, "ѕутевка - состо€ние техники",
                                            MessageBoxButtons.YesNo, MessageBoxIcon.Question)
                                            == DialogResult.Yes)
                        {
                            string strSqlTextReportFireBort = "INSERT INTO ReportFireBort " +
                            "(RptFireBortId, RptDispId, TechnicsId, BortDrive) " +
                            "Values('0', '" + strRptDispId + "', '" + TechnicsId + "', '" + strBortDrive + "')";

                            SqlCommand command = new SqlCommand(strSqlTextReportFireBort, FrmMain.Instance.m_cnn);
                            Int32 recordsAffected = command.ExecuteNonQuery();

                            FrmPermit.Instance.ReplaceBortState("4", TechnicsId);
                        }
                    }
                }
            }
        }

        private void txtBoxPlace_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Return)
            {
                e.Handled = true;
                txtBoxTake.Focus();
            }
        }

        private void txtBoxTake_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Return)
            {
                e.Handled = true;
                txtBoxDrvNote.Focus();
            }
        }

        private void chkBoxPtz_CheckedChanged(object sender, EventArgs e)
        {
            if (chkBoxPtz.Checked)
            {
                chkBoxPtu.CheckState = CheckState.Unchecked;
            }
        }

        private void chkBoxPtu_CheckedChanged(object sender, EventArgs e)
        {
            if (chkBoxPtu.Checked)
            {
                chkBoxPtz.CheckState = CheckState.Unchecked;
            }
        }

        private void rdBtnNoCatDrv_CheckedChanged(object sender, EventArgs e)
        {
            l_iCatDrvId = 0;
            m_lblCatDrv.Text = "¬ыезд техники без категории";
            m_strCatDrvRpt = "0";
        }

        private void rdBtnDrvLrn_CheckedChanged(object sender, EventArgs e)
        {
            l_iCatDrvId = 1;
            m_lblCatDrv.Text = "";
            m_strCatDrvRpt = "0";
        }

        private void rdBtnDrvPtr_CheckedChanged(object sender, EventArgs e)
        {
            l_iCatDrvId = 2;
            m_lblCatDrv.Text = "";
            m_strCatDrvRpt = "0";
        }

        private void rdBtnDrvOth_CheckedChanged(object sender, EventArgs e)
        {
            l_iCatDrvId = 3;
            m_lblCatDrv.Text = "";
            m_strCatDrvRpt = "0";
        }

        private void btnCatDrvChs_Click(object sender, EventArgs e)
        {
            ThreadLoadCtg();
        }

        private void ThreadStartFrmDrvUkg()
        {
            selThread = new Thread(new ThreadStart(ThreadLoadCtg));
            selThread.Start();
        }

        private void ThreadLoadCtg()
        {
            if (m_lblCatDrv.InvokeRequired)
            {
                SetCtgCallback d = new SetCtgCallback(ThreadLoadCtg);
                this.Invoke(d, new object[] { });
            }
            else
            {
                switch (l_iCatDrvId)
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
            FrmChsCatDrv.Instance.Text = "¬ыбор категории выезда";
            FrmChsCatDrv.Instance.m_grpBoxCat.Text = "«ан€ти€";
            FrmChsCatDrv.Instance.m_dsMsgCat = dsCtgDrvList.Copy();
            FrmChsCatDrv.Instance.m_bsMsgCat = new BindingSource(dsCtgDrvList, "InfoCatLrnDrvUkg");
            FrmChsCatDrv.Instance.m_strValDispMem = "MsgCatLrnText";
            FrmChsCatDrv.Instance.m_strMsgCatTable = "InfoCatLrnDrvUkg";
            FrmChsCatDrv.Instance.m_strMsgCatId = "MsgCatLrnId";
            FrmChsCatDrv.Instance.m_iSendMsgFrm = 3;    //категорию послать в эту форму
            FrmChsCatDrv.Instance.ShowDialog();

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
            FrmChsCatDrv.Instance.Text = "¬ыбор категории выезда";
            FrmChsCatDrv.Instance.m_grpBoxCat.Text = "ƒозор";
            FrmChsCatDrv.Instance.m_dsMsgCat = dsCtgDrvList.Copy();
            FrmChsCatDrv.Instance.m_bsMsgCat = new BindingSource(dsCtgDrvList, "InfoCatPtrDrvUkg");
            FrmChsCatDrv.Instance.m_strValDispMem = "MsgCatPtrText";
            FrmChsCatDrv.Instance.m_strMsgCatTable = "InfoCatPtrDrvUkg";
            FrmChsCatDrv.Instance.m_strMsgCatId = "MsgCatPtrId";
            FrmChsCatDrv.Instance.m_iSendMsgFrm = 3;    //категорию послать в эту форму
            FrmChsCatDrv.Instance.ShowDialog();

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
            FrmChsCatDrv.Instance.Text = "¬ыбор категории выезда";
            FrmChsCatDrv.Instance.m_grpBoxCat.Text = "ѕрочие";
            FrmChsCatDrv.Instance.m_dsMsgCat = dsCtgDrvList.Copy();
            FrmChsCatDrv.Instance.m_bsMsgCat = new BindingSource(dsCtgDrvList, "InfoCatOthDrvUkg");
            FrmChsCatDrv.Instance.m_strValDispMem = "MsgCatOthText";
            FrmChsCatDrv.Instance.m_strMsgCatTable = "InfoCatOthDrvUkg";
            FrmChsCatDrv.Instance.m_strMsgCatId = "MsgCatOthId";
            FrmChsCatDrv.Instance.m_iSendMsgFrm = 3;    //категорию послать в эту форму
            FrmChsCatDrv.Instance.ShowDialog();

        }

    }
}