using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using System.Data.SqlClient;

using XtremeCommandBars;

namespace VkoRsc
{
    public partial class FrmMain : Form
    {
        static public FrmMain Instance;

        public SqlConnection m_cnn;
        public string m_connectionString; // строка для подключения к серверу SQL

        public SortedList<int, FrmMapVko> srFrmMap = new SortedList<int, FrmMapVko>();

        public FrmMain()
        {
            InitializeComponent();
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

        private void FrmMain_Load(object sender, EventArgs e)
        {
            axCmndBarsMain.SetMDIClient(FindMDIClient().Handle.ToInt32());

            TabWorkspace Workspace = axCmndBarsMain.ShowTabWorkspace(true);
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

            LoadMapVko();

            m_connectionString = GetConnectionString();
            m_cnn = new SqlConnection(m_connectionString);
            m_cnn.Open();
        }

        private void LoadMapVko()
        {
            FrmMapVko frmDocument = new FrmMapVko();
            frmDocument.MdiParent = this;
            frmDocument.Show();
            frmDocument.Text = "ВКО";
            //несколько открытых карт
            int iCnt = srFrmMap.Count;
            int iCntMax = 0;
            if (iCnt == 0)
            {
                iCnt++;
            }
            else
            {
                for (int i = 0; i < iCnt; i++)
                {
                    iCntMax = FrmMain.Instance.srFrmMap.Keys[i];
                }
                iCnt = iCntMax + 1;
            }
            frmDocument.m_iNmb = iCnt;
            srFrmMap.Add(iCnt, frmDocument);
            //......
        }

        public void m_LoadDocExl(int iSng)
        {
            if (iSng == 1)
            {
                FrmDocExl frmDocument = new FrmDocExl();
                frmDocument.m_strNameTable = "sklad.xls";
                frmDocument.MdiParent = this;
                frmDocument.Show();
                frmDocument.Text = "Excel";
            }

            if (iSng == 2)
            {
                FrmDocExl frmDocument = new FrmDocExl();
                frmDocument.m_strNameTable = "note.xls";
                frmDocument.MdiParent = this;
                frmDocument.Show();
                frmDocument.Text = "Строевая записка - Семей";
            }
        }


        private string GetConnectionString()
        {
            //string strConnSqlSrv = "server=SBWORK\\SQLEXPRESS;user id=sa_fire;" +
            //"password=s1;initial catalog=FireNet";


            //return strConnSqlSrv;
            return "Data Source=SBWORK\\SQLEXPRESS;Initial Catalog=FireNet;"
                    + "Integrated Security=SSPI;";
        }

        private void c1CmdMapVko_Click(object sender, C1.Win.C1Command.ClickEventArgs e)
        {
            LoadMapVko();
        }

       
    }
}
