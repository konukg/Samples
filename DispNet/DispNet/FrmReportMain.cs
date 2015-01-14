using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using XtremeCommandBars;

namespace DispNet
{
    public partial class FrmReportMain : Form
    {
        static public FrmReportMain Instance;
        static public bool ms_blFrmReportMain_Show = false;

        //int lDocumentCount = 0;

        public FrmReportMain()
        {
            InitializeComponent();
            //CommandBars.LoadDesignerBars();
        }

        MdiClient FindMDIClient()
        {
            for (int i = 0; i < this.Controls.Count; i = i + 1)
            {
                if (this.Controls[i] is MdiClient)
                {
                    return (MdiClient)this.Controls[i];
                }
            }
            return null;
        }

        private void FrmReportMain_Load(object sender, EventArgs e)
        {
            CommandBars.SetMDIClient(FindMDIClient().Handle.ToInt32());

            TabWorkspace Workspace = CommandBars.ShowTabWorkspace(true);
            Workspace.EnableGroups();
            Workspace.PaintManager.Appearance = XtremeCommandBars.XTPTabAppearanceStyle.xtpTabAppearancePropertyPage2003;
            try 
            {
                Cursor = Cursors.WaitCursor;
                //LoadNewDoc();
                //LoadNewDoc();
            }
            finally
            {
                Cursor = Cursors.Default;
            }
            ms_blFrmReportMain_Show = true;
        }

        bool loadStartupDocument = false;
        private void FrmReportMain_Activated(object sender, EventArgs e)
        {
            if (!loadStartupDocument)
            {
                //LoadNewDoc();
                //LoadNewDoc();
                loadStartupDocument = true;
            }
        }

        public void m_LoadNewDoc(string strRptDateId, int iRptSing, string strDateCap)
        {
            //lDocumentCount = lDocumentCount + 1;
            FrmReportPrint frmDocument = new FrmReportPrint();
            frmDocument.m_strRptPrnId = strRptDateId;
            frmDocument.m_iRptSing = iRptSing;
            frmDocument.MdiParent = this;
            frmDocument.Show();
            frmDocument.Text = strDateCap;   //"Document " + lDocumentCount.ToString();

        }

        private void FrmReportMain_FormClosed(object sender, FormClosedEventArgs e)
        {
            ms_blFrmReportMain_Show = false;
        }
    }
}