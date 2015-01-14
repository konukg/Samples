using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Threading;
//using System.Messaging;
//using System.IO;

namespace DispNet
{
    public partial class FrmChsCatDrv : Form
    {
        static public FrmChsCatDrv Instance;

        public DataSet m_dsMsgCat = new DataSet();
        public BindingSource m_bsMsgCat = new BindingSource();
        int l_intBsPos = 0; // текущуя позиция BindingSource
        public string m_strValDispMem = "";
        public string m_strMsgCatTable = "";
        public string m_strMsgCatId = "";
        public int m_iSendMsgFrm = 0;   // если 1-посылает текст категории в форму pageTabFireAsr, 2-в форму pageTabFireAsrObl

        C1.Win.C1List.C1DataColumn DescCol; // показывает всю строку текста в list и combo (подсвечивает) но не работает в Viste в XP работает

        public IntPtr m_hndFrmCat; // указатель окна куда посылать текст категории (очень важно если открвты два одинаковых окна
        
        
        public FrmChsCatDrv()
        {
            InitializeComponent();
        }

        private void FrmChsCatDrv_Load(object sender, EventArgs e)
        {
            c1CmbCatType.DataSource = m_bsMsgCat;
            c1CmbCatType.ValueMember = m_strValDispMem;
            c1CmbCatType.DisplayMember = m_strValDispMem;
            c1CmbCatType.Columns[0].Caption = "Тип";
            c1CmbCatType.Splits[0].DisplayColumns[0].HeadingStyle.HorizontalAlignment = C1.Win.C1List.AlignHorzEnum.Center;
            c1CmbCatType.Splits[0].DisplayColumns[1].Visible = false;

            DescCol = c1CmbCatType.Columns[0];
            //m_hndFrm = FrmDispUkgDrv.Instance.m_hndFrmDispUkgDrv;
        }

        private void c1CmbCatType_Close(object sender, EventArgs e)
        {
            //if (l_intBsPos != m_bsMsgCat.Position)    //если просто открыть и закрыть сомбо данные не меняются
            //{
                btnRecDataFrm.Focus();
            //}
        }

        private void c1CmbCatType_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Return)
            {
                e.Handled = true;
                if (l_intBsPos != m_bsMsgCat.Position)    //если было событие закрыть сомбо данные не меняются
                {
                    btnRecDataFrm.Focus();
                }
            }
        }

        private void c1CmbCatType_BeforeOpen(object sender, CancelEventArgs e)
        {
            l_intBsPos = m_bsMsgCat.Position; // запоминает текущую позицию BindingSource
        }

        private void btnRecDataFrm_Click(object sender, EventArgs e)
        {
            int found = c1CmbCatType.FindStringExact(c1CmbCatType.Text, 0, 0); //проверяет категорию если она в списке иначе пишет 0
            CatTypeChsAsr(found);
        }

        private void CatTypeChsAsr(int fnd)
        {
            if (fnd == -1)
            {
                MessageBox.Show("Данной категории нет в базе данных!", "Выбор категории выезда",
                    MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
            else
            {
                CatAddTbFireAsr();
            }
        }

        private void CatAddTbFireAsr()
        {
            DataRow dr = m_dsMsgCat.Tables[m_strMsgCatTable].Rows[m_bsMsgCat.Position];
            string strCatFireId = dr[m_strMsgCatId].ToString();
            string strCatFireMsg = c1CmbCatType.Text;
            if (m_iSendMsgFrm == 1)
                FrmDispUkg.Instance.m_AddCatTbFireAsr(strCatFireId, strCatFireMsg);
            if (m_iSendMsgFrm == 2)
                FrmDispObl.Instance.m_AddCatTbFireAsrObl(strCatFireId, strCatFireMsg);
            
            Control cntrCatDrv = Control.FromHandle(m_hndFrmCat);
            if (m_iSendMsgFrm == 3)
            {
                FrmBortDrvUkg.Instance.m_lblCatDrv.Text = strCatFireMsg;
                FrmBortDrvUkg.Instance.m_strCatDrvRpt = strCatFireId;
            }

            if (m_iSendMsgFrm == 4)
            {
                cntrCatDrv.Text = strCatFireMsg;    // это FrmBortDrvUkg.Instance.m_lblCatDrv.Text
                FrmDispUkgDrv.m_strCatSingDrv = strCatFireId;
            }

            if (m_iSendMsgFrm == 5)
            {
                FrmBortDrvObl.Instance.m_lblCatDrv.Text = strCatFireMsg;
                FrmBortDrvObl.m_strCatSingDrv = strCatFireId;
            }

            if (m_iSendMsgFrm == 6)
            {
                FrmMenDrvUkg.Instance.m_lblCatDrv.Text = strCatFireMsg;
                FrmMenDrvUkg.m_strCatSingDrv = strCatFireId;
            }

            Close();
        }

        private void c1CmbCatType_FetchCellTips(object sender, C1.Win.C1List.FetchCellTipsEventArgs e)
        {
            e.CellTip = DescCol.CellText(e.Row);
        }
        
    }
}
