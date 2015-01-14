using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Threading;

namespace DispNet
{
    public partial class pageTabFireAsr : UserControl
    {
        string strRptDispId;
        DataSet dsCtgFireList = new DataSet();
        BindingSource bsCtgFireList = new BindingSource();

        DataSet dsMsgBurn = new DataSet();
        BindingSource bsMsgBurn = new BindingSource();
        DataSet dsMsgSnup = new DataSet();
        BindingSource bsMsgSnup = new BindingSource();
        DataSet dsMsgFalse = new DataSet();
        BindingSource bsMsgFalse = new BindingSource();
        DataSet dsMsgAsr = new DataSet();
        BindingSource bsMsgAsr = new BindingSource();

        int l_iFireSign = 0;  //������� � ��������� ��� ������
        int l_iCatFireId = 0; //��������� 0-���, 1-�����, 2 - ���, 3 - ����, 4 - ������

        bool blLoadRsFire = false;  //
        bool blLoadRsAsr = false;   // ��������� ������ ���� ��� �� ������ ������ ����� ������
        bool blLoadRsSnup = false;  //
        bool blLoadRsLoss = false;  //

        bool blCatRsFire = false;  //
        bool blCatRsAsr = false;   // ��������� ������ ��������� ���� ��� �� ������ ������� ������
        bool blCatRsSnup = false;  //
        bool blCatRsLoss = false;  //

        public string m_strCatSingRpr = "0"; // Id ��������� �������� ��� ������ � ������

        // ������ ����� �� ������ �������
        private Thread selThreadFireAsr = null;
        delegate void SetCtgFireAsrCallback(int iCatDrv);
        //.....
        
        public pageTabFireAsr()
        {
            InitializeComponent();
        }

        private void pageTabFireAsr_Load(object sender, EventArgs e)
        {
            strRptDispId = FrmDispFire.Instance.m_strRptDispId;

            string strSqlTextAsrMsg = "SELECT RptDispId, RptFireKuz, RptFireGuy, RptFireList, " +
                                       "RptFireAffair, RptSign, RptMsgFire, RptCatFireId " +
                                       "FROM ReportFire WHERE (RptDispId = " + strRptDispId + ")";
            SqlCommand cmd = new SqlCommand(strSqlTextAsrMsg, FrmMain.Instance.m_cnn);
            SqlDataReader rdrMsgAsr = cmd.ExecuteReader();
            
            while (rdrMsgAsr.Read())
            {
                if (!rdrMsgAsr.IsDBNull(1))
                    txtBoxKuz.Text = rdrMsgAsr.GetString(1);
                if (!rdrMsgAsr.IsDBNull(2))
                    txtBoxGuy.Text = rdrMsgAsr.GetString(2);
                if (!rdrMsgAsr.IsDBNull(3))
                    txtBoxList.Text = rdrMsgAsr.GetString(3);
                if (!rdrMsgAsr.IsDBNull(4))
                    txtBoxAffair.Text = rdrMsgAsr.GetString(4);
                if (!rdrMsgAsr.IsDBNull(5))
                    l_iFireSign = rdrMsgAsr.GetInt16(5);
                if (!rdrMsgAsr.IsDBNull(6))
                    txtBoxSnup.Text = rdrMsgAsr.GetString(6);
                if (!rdrMsgAsr.IsDBNull(7))
                    l_iCatFireId = rdrMsgAsr.GetInt16(7);
                                
            }
            rdrMsgAsr.Close();

            switch (l_iCatFireId)
            {
                case 0:
                    rdBtnNoCat.Checked = true;
                    m_lblCatFire.Text = "��� ���������!";
                    m_lblCatFire.ForeColor = Color.Red;
                    m_lblCatFire.Font = new Font(m_lblCatFire.Font, FontStyle.Bold);
                    grpBoxType.Text = "";
                    break;
                case 1:
                    rdBtnFire.Checked = true;
                    string strSqlFireCat = "SELECT MsgCatFireId, MsgCatFireText FROM InfoCatFire WHERE (MsgCatFireId = " + l_iFireSign.ToString() + ")";
                    CtgMsgListLoad(strSqlFireCat); //��������� ����� ��������� �� ������� InfoCatFire
                    break;
                case 2:
                    rdBtnASR.Checked = true;
                    string strSqlAsrCat = "SELECT MsgCatAsrId, MsgCatAsrText FROM InfoCatAsr WHERE (MsgCatAsrId = " + l_iFireSign.ToString() + ")";
                    CtgMsgListLoad(strSqlAsrCat);
                    break;
                case 3:
                    rdBtnSNUP.Checked = true;
                    string strSqlSnupCat = "SELECT MsgCatSnupId, MsgCatSnupText FROM InfoCatSnup WHERE (MsgCatSnupId = " + l_iFireSign.ToString() + ")";
                    CtgMsgListLoad(strSqlSnupCat);
                    break;
                case 4:
                    rdBtnLoss.Checked = true;
                    string strSqlLossCat = "SELECT MsgCatLossId, MsgCatLossText FROM InfoCatLoss WHERE (MsgCatLossId = " + l_iFireSign.ToString() + ")";
                    CtgMsgListLoad(strSqlLossCat);
                    break;
            }
            ThreadStartFrmFireAsr();  // ����� ������ ��� ������ �� ������� ������ ����� � �������� ��������� ������
        }

        private void CtgMsgListLoad(string strSql)
        {
            SqlCommand cmdCat = new SqlCommand(strSql, FrmMain.Instance.m_cnn);
            SqlDataReader rdrMsgCat = cmdCat.ExecuteReader();

            while (rdrMsgCat.Read())
            {
                if (!rdrMsgCat.IsDBNull(1))
                    m_lblCatFire.Text = rdrMsgCat.GetString(1);
            }
            rdrMsgCat.Close();
        }

        private void CtgFireListLoad(bool blLoad)
        {
            if (!blLoad)
            {
                string strSqlTextCtgFireList = "SELECT MsgCatFireText, MsgCatFireId FROM InfoCatFire ORDER BY MsgCatFireText";
                SqlDataAdapter daCtgFireList = new SqlDataAdapter(strSqlTextCtgFireList, FrmMain.Instance.m_cnn);
                daCtgFireList.Fill(dsCtgFireList, "InfoCatFire");
                bsCtgFireList.DataSource = dsCtgFireList;
                bsCtgFireList.DataMember = "InfoCatFire";
            }
            
            FrmChsCatDrv.Instance = new FrmChsCatDrv();
            FrmChsCatDrv.Instance.Text = "����� ���������";
            FrmChsCatDrv.Instance.m_grpBoxCat.Text = "�����";
            FrmChsCatDrv.Instance.m_dsMsgCat = dsCtgFireList.Copy();
            FrmChsCatDrv.Instance.m_bsMsgCat = new BindingSource(dsCtgFireList, "InfoCatFire");
            FrmChsCatDrv.Instance.m_strValDispMem = "MsgCatFireText";
            FrmChsCatDrv.Instance.m_strMsgCatTable = "InfoCatFire";
            FrmChsCatDrv.Instance.m_strMsgCatId = "MsgCatFireId";
            FrmChsCatDrv.Instance.m_iSendMsgFrm = 1;    //��������� ������� � ��� �����
            FrmChsCatDrv.Instance.ShowDialog();
            
        }

        private void CtgAsrListLoad(bool blLoad)
        {
            if (!blLoad)
            {
                string strSqlTextCtgFireList = "SELECT MsgCatAsrText, MsgCatAsrId FROM InfoCatAsr ORDER BY MsgCatAsrText";
                SqlDataAdapter daCtgFireList = new SqlDataAdapter(strSqlTextCtgFireList, FrmMain.Instance.m_cnn);
                daCtgFireList.Fill(dsCtgFireList, "InfoCatAsr");
                bsCtgFireList.DataSource = dsCtgFireList;
                bsCtgFireList.DataMember = "InfoCatAsr";
            }

            FrmChsCatDrv.Instance = new FrmChsCatDrv();
            FrmChsCatDrv.Instance.Text = "����� ���������";
            FrmChsCatDrv.Instance.m_grpBoxCat.Text = "���";
            FrmChsCatDrv.Instance.m_dsMsgCat = dsCtgFireList.Copy();
            FrmChsCatDrv.Instance.m_bsMsgCat = new BindingSource(dsCtgFireList, "InfoCatAsr");
            FrmChsCatDrv.Instance.m_strValDispMem = "MsgCatAsrText";
            FrmChsCatDrv.Instance.m_strMsgCatTable = "InfoCatAsr";
            FrmChsCatDrv.Instance.m_strMsgCatId = "MsgCatAsrId";
            FrmChsCatDrv.Instance.m_iSendMsgFrm = 1;    //��������� ������� � ��� �����
            FrmChsCatDrv.Instance.ShowDialog();

        }

        private void CtgSnupListLoad(bool blLoad)
        {
            if (!blLoad)
            {
                string strSqlTextCtgFireList = "SELECT MsgCatSnupText, MsgCatSnupId FROM InfoCatSnup ORDER BY MsgCatSnupText";
                SqlDataAdapter daCtgFireList = new SqlDataAdapter(strSqlTextCtgFireList, FrmMain.Instance.m_cnn);
                daCtgFireList.Fill(dsCtgFireList, "InfoCatSnup");
                bsCtgFireList.DataSource = dsCtgFireList;
                bsCtgFireList.DataMember = "InfoCatSnup";
            }

            FrmChsCatDrv.Instance = new FrmChsCatDrv();
            FrmChsCatDrv.Instance.Text = "����� ���������";
            FrmChsCatDrv.Instance.m_grpBoxCat.Text = "����";
            FrmChsCatDrv.Instance.m_dsMsgCat = dsCtgFireList.Copy();
            FrmChsCatDrv.Instance.m_bsMsgCat = new BindingSource(dsCtgFireList, "InfoCatSnup");
            FrmChsCatDrv.Instance.m_strValDispMem = "MsgCatSnupText";
            FrmChsCatDrv.Instance.m_strMsgCatTable = "InfoCatSnup";
            FrmChsCatDrv.Instance.m_strMsgCatId = "MsgCatSnupId";
            FrmChsCatDrv.Instance.m_iSendMsgFrm = 1;    //��������� ������� � ��� �����
            FrmChsCatDrv.Instance.ShowDialog();

        }

        private void CtgLossListLoad(bool blLoad)
        {
            if (!blLoad)
            {
                string strSqlTextCtgFireList = "SELECT MsgCatLossText, MsgCatLossId FROM InfoCatLoss ORDER BY MsgCatLossText";
                SqlDataAdapter daCtgFireList = new SqlDataAdapter(strSqlTextCtgFireList, FrmMain.Instance.m_cnn);
                daCtgFireList.Fill(dsCtgFireList, "InfoCatLoss");
                bsCtgFireList.DataSource = dsCtgFireList;
                bsCtgFireList.DataMember = "InfoCatLoss";
            }

            FrmChsCatDrv.Instance = new FrmChsCatDrv();
            FrmChsCatDrv.Instance.Text = "����� ���������";
            FrmChsCatDrv.Instance.m_grpBoxCat.Text = "������";
            FrmChsCatDrv.Instance.m_dsMsgCat = dsCtgFireList.Copy();
            FrmChsCatDrv.Instance.m_bsMsgCat = new BindingSource(dsCtgFireList, "InfoCatLoss");
            FrmChsCatDrv.Instance.m_strValDispMem = "MsgCatLossText";
            FrmChsCatDrv.Instance.m_strMsgCatTable = "InfoCatLoss";
            FrmChsCatDrv.Instance.m_strMsgCatId = "MsgCatLossId";
            FrmChsCatDrv.Instance.m_iSendMsgFrm = 1;    //��������� ������� � ��� �����
            FrmChsCatDrv.Instance.ShowDialog();

        }

        private void LoadDataFire(bool blLoad)
        {
            if (!blLoad)
            {
                string strSqlTextMsgBurn = "SELECT MsgBurnText, MsgBurnId FROM DispMsgBurn ORDER BY MsgBurnText";
                SqlDataAdapter daMsgBurn = new SqlDataAdapter(strSqlTextMsgBurn, FrmMain.Instance.m_cnn);
                daMsgBurn.Fill(dsMsgBurn, "DispMsgBurn");
                bsMsgBurn.DataSource = dsMsgBurn;
                bsMsgBurn.DataMember = "DispMsgBurn";
            }
            c1CmbFireType.DataSource = bsMsgBurn;
            c1CmbFireType.ValueMember = "MsgBurnText";
            c1CmbFireType.DisplayMember = "MsgBurnText";
            c1CmbFireType.Columns[0].Caption = "����� ���������";
            c1CmbFireType.Splits[0].DisplayColumns[0].HeadingStyle.HorizontalAlignment = C1.Win.C1List.AlignHorzEnum.Center;
            c1CmbFireType.Splits[0].DisplayColumns[1].Visible = false;
        }

        private void LoadDataSnup(bool blLoad)
        {
            if (!blLoad)
            {
                string strSqlTextMsgSnup = "SELECT MsgSnupText, MsgSnupId FROM DispMsgSnup ORDER BY MsgSnupText";
                SqlDataAdapter daMsgSnup = new SqlDataAdapter(strSqlTextMsgSnup, FrmMain.Instance.m_cnn);
                daMsgSnup.Fill(dsMsgSnup, "DispMsgSnup");
                bsMsgSnup.DataSource = dsMsgSnup;
                bsMsgSnup.DataMember = "DispMsgSnup";
            }
            c1CmbFireType.DataSource = bsMsgSnup;
            c1CmbFireType.ValueMember = "MsgSnupText";
            c1CmbFireType.DisplayMember = "MsgSnupText";
            c1CmbFireType.Columns[0].Caption = "����� ���������";
            c1CmbFireType.Splits[0].DisplayColumns[0].HeadingStyle.HorizontalAlignment = C1.Win.C1List.AlignHorzEnum.Center;
            c1CmbFireType.Splits[0].DisplayColumns[1].Visible = false;
        }

        private void LoadDataFalse(bool blLoad)
        {
            if (!blLoad)
            {
                string strSqlTextMsgFalse = "SELECT MsgFalseText, MsgFalseId FROM DispMsgFalse ORDER BY MsgFalseText";
                SqlDataAdapter daMsgFalse = new SqlDataAdapter(strSqlTextMsgFalse, FrmMain.Instance.m_cnn);
                daMsgFalse.Fill(dsMsgFalse, "DispMsgFalse");
                bsMsgFalse.DataSource = dsMsgFalse;
                bsMsgFalse.DataMember = "DispMsgFalse";
            }
            c1CmbFireType.DataSource = bsMsgFalse;
            c1CmbFireType.ValueMember = "MsgFalseText";
            c1CmbFireType.DisplayMember = "MsgFalseText";
            c1CmbFireType.Columns[0].Caption = "����� ���������";
            c1CmbFireType.Splits[0].DisplayColumns[0].HeadingStyle.HorizontalAlignment = C1.Win.C1List.AlignHorzEnum.Center;
            c1CmbFireType.Splits[0].DisplayColumns[1].Visible = false;
        }

        private void LoadDataAsr(bool blLoad)
        {
            if (!blLoad)
            {
                string strSqlTextMsgAsr = "SELECT MsgAsrText, MsgAsrId FROM DispMsgAsr ORDER BY MsgAsrText";
                SqlDataAdapter daMsgAsr = new SqlDataAdapter(strSqlTextMsgAsr, FrmMain.Instance.m_cnn);
                daMsgAsr.Fill(dsMsgAsr, "DispMsgAsr");
                bsMsgAsr.DataSource = dsMsgAsr;
                bsMsgAsr.DataMember = "DispMsgAsr";
            }
            c1CmbFireType.DataSource = bsMsgAsr;
            c1CmbFireType.ValueMember = "MsgAsrText";
            c1CmbFireType.DisplayMember = "MsgAsrText";
            c1CmbFireType.Columns[0].Caption = "����� ���������";
            c1CmbFireType.Splits[0].DisplayColumns[0].HeadingStyle.HorizontalAlignment = C1.Win.C1List.AlignHorzEnum.Center;
            c1CmbFireType.Splits[0].DisplayColumns[1].Visible = false;
        }

        private void RecDataRpt()
        {
            string strSqlTextASR = "UPDATE ReportFire SET RptSign = '" + m_strCatSingRpr + "', " +
                        "RptMsgFire = '" + txtBoxSnup.Text + "', RptFireKuz = '" + txtBoxKuz.Text + "', RptFireGuy = '" + txtBoxGuy.Text + "', " +
                        "RptFireList = '" + txtBoxList.Text + "', RptFireAffair = '" + txtBoxAffair.Text + "', " +
                        "RptCatFireId = '" + l_iCatFireId + "' " +
                        "WHERE RptDispId = " + strRptDispId;
            SqlCommand command = new SqlCommand(strSqlTextASR, FrmMain.Instance.m_cnn);
            Int32 recordsAffected = command.ExecuteNonQuery();

        }

        private void btnRecAsr_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("�������� ����������?", "�������� ����",
                    MessageBoxButtons.YesNo, MessageBoxIcon.Question)
                    == DialogResult.Yes)
            {
                if (m_lblCatFire.Text != "")
                {
                    RecDataRpt();
                }
                else
                {
                    MessageBox.Show("���������� ������� ��������� �� ������!", "��� ���������",
                    MessageBoxButtons.OK, MessageBoxIcon.Stop);
                    switch (l_iCatFireId)
                    {
                        case 1:
                            CtgFireListLoad(blCatRsFire);
                            blCatRsFire = true;
                            break;
                        case 2:
                            CtgAsrListLoad(blCatRsAsr);
                            blCatRsAsr = true;
                            break;
                        case 3:
                            CtgSnupListLoad(blCatRsSnup);
                            blCatRsSnup = true;
                            break;
                        case 4:
                            CtgLossListLoad(blCatRsLoss);
                            blCatRsLoss = true;
                            break;
                    }
                }
            }
        }

        private void rdBtnNoCat_CheckedChanged(object sender, EventArgs e)
        {
            if (rdBtnNoCat.Checked)
            {
                c1CmbFireType.Enabled = false;
                l_iCatFireId = 0;
                m_lblCatFire.Text = "";
                m_strCatSingRpr = "0";
                
                m_lblCatFire.Text = "��� ���������!";
                m_lblCatFire.ForeColor = Color.Red;
                m_lblCatFire.Font = new Font(m_lblCatFire.Font, FontStyle.Bold);
            }
        }
            
        private void rdBtnFire_CheckedChanged(object sender, EventArgs e)
        {
            if (rdBtnFire.Checked)
            {
                c1CmbFireType.Enabled = true;
                LoadDataFire(blLoadRsFire);
                l_iCatFireId = 1;
                blLoadRsFire = true;
                m_lblCatFire.Text = "";
                m_strCatSingRpr = "0";

                m_lblCatFire.ForeColor = Color.Black;
                m_lblCatFire.Font = new Font(m_lblCatFire.Font, FontStyle.Regular);
            }
        }
        
        private void rdBtnASR_CheckedChanged(object sender, EventArgs e)
        {
            if (rdBtnASR.Checked)
            {
                c1CmbFireType.Enabled = true;
                LoadDataAsr(blLoadRsAsr);
                l_iCatFireId = 2;
                blLoadRsAsr = true;
                m_lblCatFire.Text = "";
                m_strCatSingRpr = "0";

                m_lblCatFire.ForeColor = Color.Black;
                m_lblCatFire.Font = new Font(m_lblCatFire.Font, FontStyle.Regular);
            }
        }

        private void rdBtnSNUP_CheckedChanged(object sender, EventArgs e)
        {
            if (rdBtnSNUP.Checked)
            {
                c1CmbFireType.Enabled = true;
                LoadDataSnup(blLoadRsSnup);
                l_iCatFireId = 3;
                blLoadRsSnup = true;
                m_lblCatFire.Text = "";
                m_strCatSingRpr = "0";

                m_lblCatFire.ForeColor = Color.Black;
                m_lblCatFire.Font = new Font(m_lblCatFire.Font, FontStyle.Regular);
            }
        }
        
        private void rdBtnLoss_CheckedChanged(object sender, EventArgs e)
        {
            if (rdBtnLoss.Checked)
            {
                c1CmbFireType.Enabled = true;
                LoadDataFalse(blLoadRsLoss);
                l_iCatFireId = 4;
                blLoadRsLoss = true;
                m_lblCatFire.Text = "";
                m_strCatSingRpr = "0";

                m_lblCatFire.ForeColor = Color.Black;
                m_lblCatFire.Font = new Font(m_lblCatFire.Font, FontStyle.Regular);
            }
        }

        private void c1CmbFireType_Close(object sender, EventArgs e)
        {
            if (c1CmbFireType.Text != "")
            {
                string strInfo = txtBoxSnup.Text;
                if (strInfo == "")
                    txtBoxSnup.Text = c1CmbFireType.Text;
                else
                    txtBoxSnup.Text = strInfo + " " + c1CmbFireType.Text;

                c1CmbFireType.Text = "";
            }
        }

        private void btnCatChoose_Click(object sender, EventArgs e)
        {
            ThreadLoadCtg(l_iCatFireId);
        }

        private void ThreadStartFrmFireAsr()
        {
            selThreadFireAsr = new Thread(new ThreadStart(ThreadLoadCtgFireAsr));
            selThreadFireAsr.Start();
        }

        private void ThreadLoadCtgFireAsr()
        {
            ThreadLoadCtg(-1);
        }

        private void ThreadLoadCtg(int iCatDrv)
        {
            if (m_lblCatFire.InvokeRequired)
            {
                SetCtgFireAsrCallback d = new SetCtgFireAsrCallback(ThreadLoadCtg);
                this.Invoke(d, new object[] { iCatDrv });
            }
            else
            {
                switch (iCatDrv)
                {
                    case 1:
                        CtgFireListLoad(blCatRsFire);
                        blCatRsFire = true;
                        break;
                    case 2:
                        CtgAsrListLoad(blCatRsAsr);
                        blCatRsAsr = true;
                        break;
                    case 3:
                        CtgSnupListLoad(blCatRsSnup);
                        blCatRsSnup = true;
                        break;
                    case 4:
                        CtgLossListLoad(blCatRsLoss);
                        blCatRsLoss = true;
                        break;
                }
            }
        }

        private void m_lblCatFire_MouseEnter(object sender, EventArgs e)
        {
            string strMsgCat = m_lblCatFire.Text;
            if (strMsgCat.Length > 60)   //���� ����� �������� ����� m_lblCatFire ������ ������ 60 ����� ���������� ��������
                c1SprTooltipAsr.Show(strMsgCat, m_lblCatFire, 0, 0, 2000);
        }
            
    }
}
