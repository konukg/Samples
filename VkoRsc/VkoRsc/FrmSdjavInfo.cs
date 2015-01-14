using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using System.Data.SqlClient;

namespace VkoRsc
{
    public partial class FrmSdjavInfo : Form
    {
        static public FrmSdjavInfo Instance;

        DataSet dsSdjvName = new DataSet();
        BindingSource bsSdjvName = new BindingSource();
        DataSet dsSdjvNote = new DataSet();
        BindingSource bsSdjvNote = new BindingSource();

        public string m_strInfoSdajv;
        public int m_iSdjavId;

        public FrmSdjavInfo()
        {
            InitializeComponent();
        }

        private void FrmSdjavInfo_Load(object sender, EventArgs e)
        {
            //FrmSdjavInfo.Instance.Text = "Амииак (СДЯВ)";
            c1SprLblInfo.Text = m_strInfoSdajv;

            string strSqlTextSdjvName = "SELECT SdjvName.* FROM SdjvName ORDER BY SdjvNameMsg";
            SqlDataAdapter daSdjvName = new SqlDataAdapter(strSqlTextSdjvName, FrmMain.Instance.m_cnn);
            daSdjvName.Fill(dsSdjvName, "SdjvName");
            bsSdjvName.DataSource = dsSdjvName;
            bsSdjvName.DataMember = "SdjvName";

            string strSqlTextSdjvNote = "SELECT SdNoteId, SdNoteMsg FROM SdjvNote ORDER BY SdNoteId";
            SqlDataAdapter daSdjvNote = new SqlDataAdapter(strSqlTextSdjvNote, FrmMain.Instance.m_cnn);
            daSdjvNote.Fill(dsSdjvNote, "SdjvNote");
            bsSdjvNote.DataSource = dsSdjvNote;
            bsSdjvNote.DataMember = "SdjvNote";

            FindSdjvNoteMsg("SdjvName01");
        }

        private void FindSdjvNoteMsg(string strSdNoteId)
        {
            DataRow drSdName = dsSdjvName.Tables["SdjvName"].Rows[m_iSdjavId];
            string strSdNameId = drSdName[strSdNoteId].ToString();

            int itemFound = bsSdjvNote.Find("SdNoteId", strSdNameId);
            bsSdjvNote.Position = itemFound;
            DataRow drSdNote = dsSdjvNote.Tables["SdjvNote"].Rows[bsSdjvNote.Position];
            c1SprLblTextSdjav.Text = drSdNote["SdNoteMsg"].ToString();
        }

        private void rdBtnNm1_CheckedChanged(object sender, EventArgs e)
        {
            if (rdBtnNm1.Checked)
                FindSdjvNoteMsg("SdjvName01");
        }

        private void rdBtnNm2_CheckedChanged(object sender, EventArgs e)
        {
            if (rdBtnNm2.Checked)
                FindSdjvNoteMsg("SdjvName02");
        }

        private void rdBtnNm3_CheckedChanged(object sender, EventArgs e)
        {
            if (rdBtnNm3.Checked)
                FindSdjvNoteMsg("SdjvName03");
        }

        private void rdBtnNm4_CheckedChanged(object sender, EventArgs e)
        {
            if (rdBtnNm4.Checked)
                FindSdjvNoteMsg("SdjvName04");
        }

        private void rdBtnNm5_CheckedChanged(object sender, EventArgs e)
        {
            if (rdBtnNm5.Checked)
                FindSdjvNoteMsg("SdjvName05");
        }

        private void rdBtnNm6_CheckedChanged(object sender, EventArgs e)
        {
            if (rdBtnNm6.Checked)
                FindSdjvNoteMsg("SdjvName06");
        }

        private void rdBtnNm7_CheckedChanged(object sender, EventArgs e)
        {
            if (rdBtnNm7.Checked)
                FindSdjvNoteMsg("SdjvName07");
        }

        private void rdBtnNm8_CheckedChanged(object sender, EventArgs e)
        {
            if (rdBtnNm8.Checked)
                FindSdjvNoteMsg("SdjvName08");
        }

        private void rdBtnNm9_CheckedChanged(object sender, EventArgs e)
        {
            if (rdBtnNm9.Checked)
                FindSdjvNoteMsg("SdjvName09");
        }

        private void rdBtnNm10_CheckedChanged(object sender, EventArgs e)
        {
            if (rdBtnNm10.Checked)
                FindSdjvNoteMsg("SdjvName10");
        }

        private void rdBtnNm11_CheckedChanged(object sender, EventArgs e)
        {
            if (rdBtnNm11.Checked)
                FindSdjvNoteMsg("SdjvName11");
        }
    }
}
