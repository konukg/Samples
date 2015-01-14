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
            axTabCntDispObl.InsertItem(0, "������", pageTabFireObjObl.Handle.ToInt32(), 0);

            pageTabFireAsrObl = new pageTabFireAsrObl();
            pageTabFireAsrObl.Parent = axTabCntDispObl;
            axTabCntDispObl.InsertItem(1, "�������� ����", pageTabFireAsrObl.Handle.ToInt32(), 0);

            pageTabFireDocObl = new pageTabFireDocObl();
            pageTabFireDocObl.Parent = axTabCntDispObl;
            axTabCntDispObl.InsertItem(2, "���������", pageTabFireDocObl.Handle.ToInt32(), 0);

            pageTabFireDataObl = new pageTabFireDataObl();
            pageTabFireDataObl.Parent = axTabCntDispObl;
            axTabCntDispObl.InsertItem(3, "����������", pageTabFireDataObl.Handle.ToInt32(), 0);

            pageTabChronologyObl = new pageTabChronologyObl();
            pageTabChronologyObl.Parent = axTabCntDispObl;
            axTabCntDispObl.InsertItem(4, "����������", pageTabChronologyObl.Handle.ToInt32(), 0);

            pageTabDispOblBort = new pageTabDispOblBort();
            pageTabDispOblBort.Parent = axTabCntDispObl;
            axTabCntDispObl.InsertItem(5, "�������", pageTabDispOblBort.Handle.ToInt32(), 0);

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