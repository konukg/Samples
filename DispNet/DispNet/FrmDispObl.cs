using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace DispNet
{
    public partial class FrmDispObl : Form
    {
        static public FrmDispObl Instance;
        pageTabFireObjObl pageTabFireObjObl;
        pageTabFireAsrObl pageTabFireAsrObl;
        pageTabFireDocObl pageTabFireDocObl;
        pageTabFireDataObl pageTabFireDataObl;
        pageTabChronologyObl pageTabChronologyObl;
        pageTabDispOblBort pageTabDispOblBort;

        public FrmDispObl()
        {
            InitializeComponent();
        }

        public void m_AddCatTbFireAsrObl(string strCatSing, string strCatMsg)
        {
            pageTabFireAsrObl.m_lblCatFire.Text = strCatMsg;
            pageTabFireAsrObl.m_strCatSingRpr = strCatSing;
        }

        private void FrmDispObl_Load(object sender, EventArgs e)
        {
            pageTabFireObjObl = new pageTabFireObjObl();
            pageTabFireObjObl.Parent = axTabCntDispObl;
            axTabCntDispObl.InsertItem(0, "Объект", pageTabFireObjObl.Handle.ToInt32(), 0);

            pageTabFireAsrObl = new pageTabFireAsrObl();
            pageTabFireAsrObl.Parent = axTabCntDispObl;
            axTabCntDispObl.InsertItem(1, "Принятые меры", pageTabFireAsrObl.Handle.ToInt32(), 0);

            pageTabFireDocObl = new pageTabFireDocObl();
            pageTabFireDocObl.Parent = axTabCntDispObl;
            axTabCntDispObl.InsertItem(2, "Документы", pageTabFireDocObl.Handle.ToInt32(), 0);

            pageTabFireDataObl = new pageTabFireDataObl();
            pageTabFireDataObl.Parent = axTabCntDispObl;
            axTabCntDispObl.InsertItem(3, "Информация", pageTabFireDataObl.Handle.ToInt32(), 0);

            pageTabChronologyObl = new pageTabChronologyObl();
            pageTabChronologyObl.Parent = axTabCntDispObl;
            axTabCntDispObl.InsertItem(4, "Хронология", pageTabChronologyObl.Handle.ToInt32(), 0);

            pageTabDispOblBort = new pageTabDispOblBort();
            pageTabDispOblBort.Parent = axTabCntDispObl;
            axTabCntDispObl.InsertItem(5, "Техника", pageTabDispOblBort.Handle.ToInt32(), 0);

            axTabCntDispObl.PaintManager.Appearance = XtremeSuiteControls.XTPTabAppearanceStyle.xtpTabAppearancePropertyPage2003;
            axTabCntDispObl.PaintManager.Color = XtremeSuiteControls.XTPTabColorStyle.xtpTabColorOffice2003;
            axTabCntDispObl.PaintManager.BoldSelected = true;
            axTabCntDispObl.PaintManager.OneNoteColors = true;
        }

        private void axTabCntDispObl_SelectedChanged(object sender, AxXtremeSuiteControls._DTabControlEvents_SelectedChangedEvent e)
        {
            string rtr = e.item.Caption;
        }
    }
}