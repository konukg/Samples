using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;
//using C1.Win.C1List;

namespace DispNet
{
    public partial class FrmAddBort : Form
    {
        static public FrmAddBort Instance;

        DataSet dsTechUnit = new DataSet();
        BindingSource bsTechUnit = new BindingSource();
        DataSet dsTechAbb = new DataSet();
        BindingSource bsTechAbb = new BindingSource();
        DataSet dsUnit = new DataSet();
        BindingSource bsUnit = new BindingSource();
        public int m_iPemitBort = 0;

        public FrmAddBort()
        {
            InitializeComponent();
        }

        private void FrmAddBort_Load(object sender, EventArgs e)
        {
            if (m_iPemitBort == 1)
            {
                int iCnt = FrmPermit.Instance.c1ListBort.ListCount;
                for (int i = 0; i < iCnt; i = i + 1)
                    c1ListBortPmt.AddItem(FrmPermit.Instance.c1ListBort.GetItemText(i, 0));
            }
            string strSqlTextTechUnit = "SELECT TechnicsUnit.TechnicsId, TechnicsUnit.TechnicsName, TechnicsUnit.TechnicsType, " +
                                "TechnicsState.StateName, Unit.UnitName, TechnicsUnit.UnitAllow, TechnicsUnit.TechnicsAbbrev, " +
                                "TechnicsUnit.TechnicsNote, TechnicsState.StateId FROM TechnicsUnit INNER JOIN TechnicsState ON " +
                                "TechnicsUnit.StateId = TechnicsState.StateId INNER JOIN Unit ON TechnicsUnit.UnitId = Unit.UnitId " +
						        "ORDER BY TechnicsUnit.TechnicsId";
            SqlDataAdapter daTechUnit = new SqlDataAdapter(strSqlTextTechUnit, FrmPermit.Instance.cnn);
            daTechUnit.Fill(dsTechUnit, "TechnicsUnit");
            bsTechUnit.DataSource = dsTechUnit;
            bsTechUnit.DataMember = "TechnicsUnit";

            string strSqlTextTechAbb = "SELECT DISTINCT TechnicsAbbrev FROM TechnicsUnit ORDER BY TechnicsAbbrev";
            SqlDataAdapter daTechAbb = new SqlDataAdapter(strSqlTextTechAbb, FrmPermit.Instance.cnn);
            daTechAbb.Fill(dsTechAbb, "TechnicsUnit");
            bsTechAbb.DataSource = dsTechAbb;
            bsTechAbb.DataMember = "TechnicsUnit";

            string strSqlTextUnit = "SELECT DISTINCT Unit.UnitName FROM TechnicsUnit INNER JOIN Unit " +
                                        "ON TechnicsUnit.UnitId = Unit.UnitId ORDER BY Unit.UnitName";
            SqlDataAdapter daUnit = new SqlDataAdapter(strSqlTextUnit, FrmPermit.Instance.cnn);
            daUnit.Fill(dsUnit, "TechnicsUnit");
            bsUnit.DataSource = dsUnit;
            bsUnit.DataMember = "TechnicsUnit";

            c1ListTechPch.AddItemCols = 3;
            c1ListTechPch.Splits[0].DisplayColumns[1].Width = 100;
            //c1ListTechPch.Splits[0].DisplayColumns[2].Visible = false;
            string strFilter = "StateId = '1'";
            BortFilter(strFilter);
           
        }

        private void BortFilter(string strFilter)
        {
            DataRow[] drarray;
            drarray = dsTechUnit.Tables["TechnicsUnit"].Select(strFilter);
            string strAdd;
            
            c1ListTechPch.ClearItems();
            for (int i = 0; i < drarray.Length; i++)
            {
                strAdd = drarray[i]["TechnicsName"].ToString() + ";" + drarray[i]["StateName"].ToString() + ";" + 
                            drarray[i]["UnitName"].ToString();
                c1ListTechPch.AddItem(strAdd);
            }
        }

        private bool BortState(string strBortName)
        {
            bool blBortRpl = false;
            DataView dvTechnicsUnit = new DataView(dsTechUnit.Tables["TechnicsUnit"], "",
                                "TechnicsName", DataViewRowState.CurrentRows);
            int rowIndex = dvTechnicsUnit.Find(strBortName);
            if (rowIndex != -1)
            {
                string TechnicsId;
                string strTechnicsState;
                string strTechnicsName;
                string strMsgState;
                int iTechnicsState = 0;

                TechnicsId = dvTechnicsUnit[rowIndex]["TechnicsId"].ToString();
                iTechnicsState = Convert.ToInt32(dvTechnicsUnit[rowIndex]["StateId"].ToString());
                if (iTechnicsState == 1)
                {
                    blBortRpl = true;
                }
                else
                {
                    strTechnicsName = dvTechnicsUnit[rowIndex]["TechnicsName"].ToString();
                    strTechnicsState = dvTechnicsUnit[rowIndex]["StateName"].ToString();
                    strMsgState = "Борт " + strTechnicsName + " имеет состояние - " + strTechnicsState +
                       ". Заменить на значение - В pасчете?";
                    if (MessageBox.Show(strMsgState, "Путевка - состояние техники",
                                        MessageBoxButtons.YesNo, MessageBoxIcon.Question)
                                        == DialogResult.Yes)
                    {
                        FrmPermit.Instance.ReplaceBortState("1", TechnicsId);
                        blBortRpl = true;
                    }
                }
            }
            return blBortRpl;
        }

        private void btnTest_Click(object sender, EventArgs e)
        {
            int iCnt = c1ListBortPmt.ListCount;
            if (m_iPemitBort == 1)
            {
                FrmPermit.Instance.c1ListBort.ClearItems();
                if (iCnt > 0)
                {
                    for (int i = 0; i < iCnt; i = i + 1)
                        FrmPermit.Instance.c1ListBort.AddItem(c1ListBortPmt.GetItemText(i, 0));
                }
            }
            if (m_iPemitBort == 2)
            {
                //pageTabDispUkgBort.Instance.c1ListBortAdd.ClearItems();
                if (iCnt > 0)
                {
                    for (int i = 0; i < iCnt; i = i + 1)
                        FrmDispUkg.Instance.AddBortPmt(c1ListBortPmt.GetItemText(i, 0));
                }
            }
            if (m_iPemitBort == 3)
            {
                if (iCnt > 0)
                {
                    for (int i = 0; i < iCnt; i = i + 1)
                        FrmBortDrvUkg.Instance.c1ListBortDrv.AddItem(c1ListBortPmt.GetItemText(i, 0));
                }
            }
            Close();
        }

        private void btnRemov_Click(object sender, EventArgs e)
        {
            
            int iCnt = c1ListBortPmt.ListCount;
            int iCntAdd = iCnt - c1ListBortPmt.SelectedIndices.Count;
            string[] strN = new string[iCntAdd];
            int j = 0;
            
            for (int i = 0; i < iCnt ; i = i + 1)
            {
                if (!c1ListBortPmt.GetSelected(i))
                {
                    strN[j] = c1ListBortPmt.GetItemText(i, 0);
                    j++;
                }
                
            }
            c1ListBortPmt.ClearItems();
            for (j = 0; j < iCntAdd; j = j + 1)
                c1ListBortPmt.AddItem(strN[j]);
           
        }

        private void checkBoxRasch_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBoxRasch.Checked)
            {
                string strFilter;
                if (!rdBtnLest.Checked)
                {
                    strFilter = "StateId = '1'";
                    BortFilter(strFilter);
                }
                else
                {
                    strFilter = "TechnicsAbbrev = 'АЛ' AND StateId = '1'";
                    BortFilter(strFilter);
                }
                if (rdBtnBort.Checked)
                {
                    bsTechUnit.Filter = strFilter;
                    cmbBoxBort.DataSource = bsTechUnit;
                    cmbBoxBort.ValueMember = "TechnicsName";
                    cmbBoxBort.DisplayMember = "TechnicsName";
                    cmbBoxBort.Text = "";
                    cmbBoxBort.Focus();
                }
            }
            else
            {
                string strFilter;
                if (!rdBtnLest.Checked)
                {
                    strFilter = ""; ;
                    BortFilter(strFilter);
                }
                else
                {
                    strFilter = "TechnicsAbbrev = 'АЛ'";
                    BortFilter(strFilter);
                }
                if (rdBtnBort.Checked)
                {
                    bsTechUnit.Filter = strFilter;
                    cmbBoxBort.DataSource = bsTechUnit;
                    cmbBoxBort.ValueMember = "TechnicsName";
                    cmbBoxBort.DisplayMember = "TechnicsName";
                    cmbBoxBort.Text = "";
                    cmbBoxBort.Focus();
                }
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            int iCnt = c1ListTechPch.ListCount;
            for (int i = 0; i < iCnt; i = i + 1)
            {
                if (c1ListTechPch.GetSelected(i))
                {
                    string strBort = c1ListTechPch.GetItemText(i, 0);
                    if (c1ListBortPmt.FindString(strBort) == -1)
                    {
                        if(BortState(strBort))
                            c1ListBortPmt.AddItem(strBort);
                    }
                }
            }
            c1ListTechPch.ClearSelected();
        }

        private void rdBtnBort_CheckedChanged(object sender, EventArgs e)
        {
            if (rdBtnBort.Checked)
            {
                c1ListTechPch.ClearItems();
                cmbBoxBort.DataSource = bsTechUnit;
                cmbBoxBort.ValueMember = "TechnicsName";
                cmbBoxBort.DisplayMember = "TechnicsName";
                cmbBoxBort.Text = "";
                cmbBoxBort.Focus();
            }
        }

        private void cmbBoxBort_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyValue == 13)
            {
                if (rdBtnBort.Checked)
                {
                    string strBort = cmbBoxBort.Text;
                    if (c1ListBortPmt.FindString(strBort) == -1)
                    {
                        if (BortState(strBort))
                            c1ListBortPmt.AddItem(strBort);
                    }
                }
                if (rdBtnType.Checked)
                {
                    string strFilter;
                    string strAbb = cmbBoxBort.Text;
                    if (checkBoxRasch.Checked)
                        strFilter = "TechnicsAbbrev = '" + strAbb + "' AND StateId = '1'";
                    else
                        strFilter = "TechnicsAbbrev = '" + strAbb + "'";
                    BortFilter(strFilter);
                }
                if (rdBtnPch.Checked)
                {
                    string strFilter;
                    string strPch = cmbBoxBort.Text;
                    if (checkBoxRasch.Checked)
                        strFilter = "UnitName = '" + strPch + "' AND StateId = '1'";
                    else
                        strFilter = "UnitName = '" + strPch + "'";
                    BortFilter(strFilter);
                }

            }
        }

        private void rdBtnType_CheckedChanged(object sender, EventArgs e)
        {
            if (rdBtnType.Checked)
            {
                c1ListTechPch.ClearItems();
                cmbBoxBort.DataSource = bsTechAbb;
                cmbBoxBort.ValueMember = "TechnicsAbbrev";
                cmbBoxBort.DisplayMember = "TechnicsAbbrev";
                cmbBoxBort.Text = "";
                cmbBoxBort.Focus();
            }   
        }

        private void rdBtnPch_CheckedChanged(object sender, EventArgs e)
        {
            if (rdBtnPch.Checked)
            {
                c1ListTechPch.ClearItems();
                cmbBoxBort.DataSource = bsUnit;
                cmbBoxBort.ValueMember = "UnitName";
                cmbBoxBort.DisplayMember = "UnitName";
                cmbBoxBort.Text = "";
                cmbBoxBort.Focus();
            }
        }

        private void rdBtnLest_CheckedChanged(object sender, EventArgs e)
        {
            string strFilter;
            if (rdBtnLest.Checked)
                strFilter = "TechnicsAbbrev = 'АЛ' AND StateId = '1'";
            else
                strFilter = "TechnicsAbbrev = 'АЛ'";
            BortFilter(strFilter);
        }
    }
}