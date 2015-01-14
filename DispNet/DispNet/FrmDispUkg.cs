using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Threading;

namespace DispNet
{
    public partial class FrmDispUkg : Form
    {
        static public FrmDispUkg Instance;

        public int m_iDislOk = 0;
        public string m_strBuildObjId = "";   //Id номер дома или здания объекта
        pageTabDispUkgBort pageTabDispUkgBort;
        pageTabChronology pageTabChronology;
        pageTabFireData pageTabFireData;
        pageTabFireDoc pageTabFireDoc;
        pageTabFireAsr pageTabFireAsr;
        pageTabFireObj pageTabFireObj;

        // запуск формы из разных потоков
        //private Thread InfoCatThread = null;
        //delegate void SetCatCallback(string strCatSing, string strCatMsg, int iMsgFrmSnd);
        //string l_strCatSing, l_strCatMsg;

        public FrmDispUkg()
        {
            InitializeComponent();
        }

        private void FrmDispUkg_Load(object sender, EventArgs e)
        {
            //ThreadStartCat();  // старт потока для обработки данных категорий

            pageTabFireObj = new pageTabFireObj();
            pageTabFireObj.Parent = axTabCntDispUkg;
            axTabCntDispUkg.InsertItem(0, "Объект", pageTabFireObj.Handle.ToInt32(), 0);

            pageTabFireAsr = new pageTabFireAsr();
            pageTabFireAsr.Parent = axTabCntDispUkg;
            axTabCntDispUkg.InsertItem(1, "Принятые меры", pageTabFireAsr.Handle.ToInt32(), 0);

            pageTabFireDoc = new pageTabFireDoc();
            pageTabFireDoc.Parent = axTabCntDispUkg;
            axTabCntDispUkg.InsertItem(2, "Документы", pageTabFireDoc.Handle.ToInt32(), 0);
                        
            pageTabFireData = new pageTabFireData();
            pageTabFireData.Parent = axTabCntDispUkg;
            axTabCntDispUkg.InsertItem(3, "Информация", pageTabFireData.Handle.ToInt32(), 0);
                        
            pageTabChronology = new pageTabChronology();
            pageTabChronology.Parent = axTabCntDispUkg;
            axTabCntDispUkg.InsertItem(4, "Хронология", pageTabChronology.Handle.ToInt32(), 0);

            pageTabDispUkgBort = new pageTabDispUkgBort();
            pageTabDispUkgBort.Parent = axTabCntDispUkg;
            axTabCntDispUkg.InsertItem(5, "Техника", pageTabDispUkgBort.Handle.ToInt32(), 0);
           
            axTabCntDispUkg.PaintManager.Appearance = XtremeSuiteControls.XTPTabAppearanceStyle.xtpTabAppearancePropertyPage2003;
            axTabCntDispUkg.PaintManager.Color = XtremeSuiteControls.XTPTabColorStyle.xtpTabColorOffice2003;
            axTabCntDispUkg.PaintManager.BoldSelected = true;
            axTabCntDispUkg.PaintManager.OneNoteColors = true;

            pageTabDispUkgBort.c1ListBortAdd.AddItemCols = 1;
            pageTabDispUkgBort.c1ListBortAdd.Splits[0].DisplayColumns[0].Width = 50;
            pageTabDispUkgBort.c1ListBortAdd.Splits[0].DisplayColumns[0].Style.BackColor = Color.LightYellow;

            
        }

        public void m_AddCatTbFireAsr(string strCatSing, string strCatMsg)
        {
            pageTabFireAsr.m_lblCatFire.Text = strCatMsg;
            pageTabFireAsr.m_strCatSingRpr = strCatSing;
                    
            //l_strCatSing = strCatSing;
            //l_strCatMsg = strCatMsg;
            //FrmChsCatDrv.Instance.Close();
            //CatTbFireAsr();
        }

        /*private void CatTbFireAsr()
        {
            if (pageTabFireAsr.m_lblCatFire.InvokeRequired)
            {
                SetCatCallback d = new SetCatCallback(m_AddCatTbFireAsr);
                this.Invoke(d, new object[] { l_strCatSing, l_strCatMsg });
            }
            else
            {
                pageTabFireAsr.m_lblCatFire.Text = l_strCatMsg;
                pageTabFireAsr.m_strCatSingRpr = l_strCatSing;
                
            }
        }*/

        public void AddBortPmt(string strBort)
        {
            pageTabDispUkgBort.c1ListBortAdd.AddItem(strBort);

        }

        private void axTabCntDispUkg_SelectedChanged(object sender, AxXtremeSuiteControls._DTabControlEvents_SelectedChangedEvent e)
        {
            string rtr = e.item.Caption;
        }
    }
}