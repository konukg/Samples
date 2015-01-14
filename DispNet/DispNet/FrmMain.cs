using System;
using System.IO;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Net;
using System.Net.Sockets;
using XtremeCommandBars;
using System.Timers;
using System.Threading;

namespace DispNet
{
    public partial class FrmMain : Form
    {
        static public FrmMain Instance;
        public SqlConnection m_cnn;
        public string m_strPmtPathSend = "";
        public Socket m_clientSocket;
        //byte[] m_dataBuffer = new byte[10];
        IAsyncResult m_result;
        public AsyncCallback m_pfnCallBack;
        bool blConSrv = false;
        public bool m_blSendSrv = false; //true разрешает посылать сообщение 111 серверу
        string strRemSrvName = "";
        public string m_strLocalNameComp = "";
        string strSrvIP = "10.10.10.10";
        string l_strSqlName = "";
        public string m_strPathMapUkg = "";
        public int m_iDispFireTime = 0; //определ€ет тип пожара за сутки, текущие или все
        public string m_connectionString; // строка дл€ подключени€ к серверу SQL
        public bool m_blFrmDispFireShow = false; // не дает открыть больше одной формы FrmDispFire - список пожаров
        public bool m_blFrmMonAlert = false; // не дает открыть больше одной формы FrmMonAlert - список сработок объектов мониторинга
        public bool m_blFrmUnitNote = false; // не дает открыть больше одной формы FrmUnitNote

        // запуск формы из разных потоков
        private Thread selThread = null;
        delegate void SetTextCallback(string text, bool blCn);
        delegate void SetAlertCallback(bool blSt, int iStart, string strMsgMon);
        //.....

        //private AxXtremeCommandBars.AxImageManager ImageManager;
        //int lDocumentCount = 0;
        
        public FrmMain()
        {
            //Form myForm = FindForm();
            //int rtr = myForm.Controls.Count;

            InitializeComponent();
            axCommandBars1.LoadDesignerBars();
            axCommandBars1.VisualTheme = XtremeCommandBars.XTPVisualTheme.xtpThemeOffice2007;
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
            axCommandBars1.SetMDIClient(FindMDIClient().Handle.ToInt32());
            
            m_strLocalNameComp = Dns.GetHostName(); // им€ компьютера
                        
            StreamReader sr = File.OpenText("DispNet.ini");
            string strFileText = sr.ReadToEnd();
            sr.Close();

            string[] strIpPath;
            strIpPath = strFileText.Split('#');
            strSrvIP = strIpPath[0];
            m_strPmtPathSend = strIpPath[1];
            m_strPathMapUkg = strIpPath[2];
            l_strSqlName = strIpPath[3];

            m_connectionString = GetConnectionString();
            m_cnn = new SqlConnection(m_connectionString);
            m_cnn.Open();

            // включить код запуска таймера в функции SetText(string text, bool blCn)
            bckgrndWorkerMain.RunWorkerAsync(); // первый старт ConnectToServer() потом если нет оединени€ через таймер
                        
            //tlStripStatLabelCnt.ForeColor = Color.Red;
                        
            FrmMapUkgPmt.Instance = new FrmMapUkgPmt();
            FrmMapUkgPmt.Instance.Show();
            //FrmMapUkg.Instance = new FrmMapUkg();
            //FrmMapUkg.Instance.Show();
            
            TabWorkspace Workspace = axCommandBars1.ShowTabWorkspace(true);
            Workspace.EnableGroups();
            Workspace.PaintManager.Appearance = XtremeCommandBars.XTPTabAppearanceStyle.xtpTabAppearancePropertyPage2003;
            //Workspace.PaintManager.ShowIcons = true;
            Workspace.PaintManager.OneNoteColors = true;
            
            try
            {
                Cursor = Cursors.WaitCursor;
                //LoadMapUkg(0);
                //LoadMapUkg();
                Cursor = Cursors.Default;
            }
            finally
            {
                Cursor = Cursors.Default;
            }


            FrmPermit.Instance = new FrmPermit();
            FrmPermit.Instance.Show();

            ThreadStartSock();  // старт потока дл€ обработки данных поступающих через сокет
        }

        public DateTime m_fnFindDateNext(DateTime myDateNext)
        {
            int iYear = myDateNext.Year;
            int iMonth = myDateNext.Month;
            int iDay = myDateNext.Day;

            switch (iMonth)
            {
                case 1:
                    if (iDay == 31)
                    {
                        iDay = 1;
                        iMonth = 2;
                    }
                    else
                        iDay = iDay + 1;

                    break;
                case 2:
                    double iYearDate = myDateNext.Year;
                    double dYvs = iYearDate / 4;
                    string[] strYear;
                    strYear = dYvs.ToString().Split(',');
                    int iVs = strYear.Length;   //1-високосный, 2-нет
                    if (iVs == 1)
                    {
                        if (iDay == 29)
                        {
                            iDay = 1;
                            iMonth = 3;
                        }
                        else
                            iDay = iDay + 1;
                    }
                    else
                    {
                        if (iDay == 28)
                        {
                            iDay = 1;
                            iMonth = 3;
                        }
                        else
                            iDay = iDay + 1;
                    }
                    break;
                case 3:
                    if (iDay == 31)
                    {
                        iDay = 1;
                        iMonth = 4;
                    }
                    else
                        iDay = iDay + 1;
                    break;
                case 4:
                    if (iDay == 30)
                    {
                        iDay = 1;
                        iMonth = 5;
                    }
                    else
                        iDay = iDay + 1;
                    break;
                case 5:
                    if (iDay == 31)
                    {
                        iDay = 1;
                        iMonth = 5;
                    }
                    else
                        iDay = iDay + 1;
                    break;
                case 6:
                    if (iDay == 30)
                    {
                        iDay = 1;
                        iMonth = 7;
                    }
                    else
                        iDay = iDay + 1;
                    break;
                case 7:
                    if (iDay == 31)
                    {
                        iDay = 1;
                        iMonth = 8;
                    }
                    else
                        iDay = iDay + 1;
                    break;
                case 8:
                    if (iDay == 31)
                    {
                        iDay = 1;
                        iMonth = 9;
                    }
                    else
                        iDay = iDay + 1;
                    break;
                case 9:
                    if (iDay == 30)
                    {
                        iDay = 1;
                        iMonth = 10;
                    }
                    else
                        iDay = iDay + 1;
                    break;
                case 10:
                    if (iDay == 31)
                    {
                        iDay = 1;
                        iMonth = 11;
                    }
                    else
                        iDay = iDay + 1;
                    break;
                case 11:
                    if (iDay == 30)
                    {
                        iDay = 1;
                        iMonth = 12;
                    }
                    else
                        iDay = iDay + 1;
                    break;
                case 12:
                    if (iDay == 31)
                    {
                        iDay = 1;
                        iMonth = 1;
                        iYear = iYear + 1;
                    }
                    else
                        iDay = iDay + 1;
                    break;
            }

            DateTime myDateFnd = new DateTime(iYear, iMonth, iDay);

            return myDateFnd;
        }

        public DateTime m_fnFindDateAft(DateTime myDateAft)
        {
            int iYear = myDateAft.Year;
            int iMonth = myDateAft.Month;
            int iDay = myDateAft.Day;

            switch (iMonth)
            {
                case 1:
                    if (iDay == 1)
                    {
                        iDay = 31;
                        iMonth = 12;
                        iYear = iYear - 1;
                    }
                    else
                        iDay = iDay - 1;

                    break;
                case 2:
                    if (iDay == 1)
                    {
                        iDay = 31;
                        iMonth = 1;
                    }
                    else
                        iDay = iDay - 1;
                    break;
                case 3:
                    if (iDay == 1)
                    {
                        double iYearDate = myDateAft.Year;
                        double dYvs = iYearDate / 4;
                        string[] strYear;
                        strYear = dYvs.ToString().Split(',');
                        int iVs = strYear.Length;   //1-високосный, 2-нет
                        if (iVs == 1)
                            iDay = 29;
                        else
                            iDay = 28;
                        iMonth = 2;
                    }
                    else
                        iDay = iDay - 1;
                    break;
                case 4:
                    if (iDay == 1)
                    {
                        iDay = 31;
                        iMonth = 3;
                    }
                    else
                        iDay = iDay - 1;
                    break;
                case 5:
                    if (iDay == 1)
                    {
                        iDay = 30;
                        iMonth = 4;
                    }
                    else
                        iDay = iDay - 1;
                    break;
                case 6:
                    if (iDay == 1)
                    {
                        iDay = 31;
                        iMonth = 5;
                    }
                    else
                        iDay = iDay - 1;
                    break;
                case 7:
                    if (iDay == 1)
                    {
                        iDay = 30;
                        iMonth = 6;
                    }
                    else
                        iDay = iDay - 1;
                    break;
                case 8:
                    if (iDay == 1)
                    {
                        iDay = 31;
                        iMonth = 7;
                    }
                    else
                        iDay = iDay - 1;
                    break;
                case 9:
                    if (iDay == 1)
                    {
                        iDay = 31;
                        iMonth = 8;
                    }
                    else
                        iDay = iDay - 1;
                    break;
                case 10:
                    if (iDay == 1)
                    {
                        iDay = 30;
                        iMonth = 9;
                    }
                    else
                        iDay = iDay - 1;
                    break;
                case 11:
                    if (iDay == 1)
                    {
                        iDay = 31;
                        iMonth = 10;
                    }
                    else
                        iDay = iDay - 1;
                    break;
                case 12:
                    if (iDay == 1)
                    {
                        iDay = 30;
                        iMonth = 11;
                    }
                    else
                        iDay = iDay - 1;
                    break;
            }

            DateTime myDateFnd = new DateTime(iYear, iMonth, iDay);

            return myDateFnd;
        }

        public void LoadMapWaterUkg(string strHomeId)
        {
            //lDocumentCount = lDocumentCount + 1;
            FrmMapUkgPmt frmMapWater = new FrmMapUkgPmt();
            
            frmMapWater.m_bWaterShow = true;
            frmMapWater.m_strBuildIdShow = strHomeId;
            frmMapWater.Show();
            
            frmMapWater.Text = "¬одоисточники"; //+lDocumentCount.ToString();
            frmMapWater.MdiParent = this;
            frmMapWater.WindowState = FormWindowState.Normal;
            
        }

        public void LoadMapUkg(int iSng, string strBuildIdMap)
        {
            FrmMapUkg frmMap = new FrmMapUkg();
            //frmDocument.m_strRptPrnId = strRptDateId;
            //frmDocument.m_iRptSing = iRptSing;
            frmMap.MdiParent = this;
            
            frmMap.Show();
            frmMap.Text = "√ород"; //+lDocumentCount.ToString();
            string rtr = frmMap.Icon.ToString();// = new Icon("ico1989.ico"); 

            if (iSng == 1)  //признак отображени€ дома на карте форма FrmPermit
            {
                //frmMap.m_FindObjMap(frmMap.m_lyrBuildHm, FrmPermit.Instance.m_strBuildIdMap, 1);
                frmMap.m_FindObjMap(frmMap.m_lyrBuildHm, strBuildIdMap, 1, 1);
            }

            if (iSng == 2)  //признак отображени€ дома на карте форма FrmHomeData
            {
                //string strRtr = FrmHomeData.Instance.m_strBuildIdHome;
                frmMap.m_FindObjMap(frmMap.m_lyrBuildHm, FrmDispUkg.Instance.m_strBuildObjId, 1, 1);
            }

            if (iSng == 3)  //признак отображени€ дома на карте форма FrmObjFind
            {
                //string strRtr = FrmHomeData.Instance.m_strBuildIdHome;
                frmMap.m_FindObjMap(frmMap.m_lyrBuildHm, FrmObjFind.Instance.m_strObjFndId, 1, 1);
            }
        }

        private string GetConnectionString()
        {
            // To avoid storing the connection string in your code, 
            // you can retrieve it from a configuration file, using the 
            // System.Configuration.ConfigurationSettings.AppSettings property

            //string strConnSqlSrv = "server=" + l_strSqlName + ";user id=sa_fire;" +
            //"password=s1;initial catalog=FireNet";

            string strConnSqlSrv = "Data Source=" + l_strSqlName + ";Initial Catalog=FireNet;"
                + "Integrated Security=SSPI;";
            
            //return "Data Source=SB-WORK\\SQLNTB;Initial Catalog=FireNet;"
                //+ "Integrated Security=SSPI;";
            
            return strConnSqlSrv;
        }

        private void axCommandBars1_Execute(object sender, AxXtremeCommandBars._DCommandBarsEvents_ExecuteEvent e)
        {
            switch (e.control.Id)
            {
                case ID.ID_PERMIT:
                    if (!FrmPermit.Instance.blFrmPmtShow)
                    {
                        FrmPermit.Instance.WindowState = FormWindowState.Normal;
                        FrmPermit.Instance.Show();
                        FrmPermit.Instance.blFrmPmtShow = true;
                    }
                    else
                        FrmPermit.Instance.Activate();
                    FrmPermit.Instance.c1ComboFnd.Focus();
                    break;
                case ID.ID_BORT_DRIVE:
                    FrmBortDrvUkg.Instance = new FrmBortDrvUkg();
                    FrmBortDrvUkg.Instance.ShowDialog();
                    break;
                case ID.ID_DRV_PEOPLE:
                    FrmMenDrvUkg.Instance = new FrmMenDrvUkg();
                    FrmMenDrvUkg.Instance.m_iLoadDispRpt = 1;
                    FrmMenDrvUkg.Instance.ShowDialog();
                    break;
                case ID.ID_PERMIT_OBL:
                    FrmFireObl.Instance = new FrmFireObl();
                    FrmFireObl.Instance.ShowDialog();
                    break;
                case ID.ID_DISP_FIRE:   //все пожары и чс
                    m_iDispFireTime = 1;
                    if (!m_blFrmDispFireShow)
                    {
                        m_blFrmDispFireShow = true;
                        FrmDispFire.Instance = new FrmDispFire();
                        FrmDispFire.Instance.Show();
                    }
                    else
                    {
                        FrmDispFire.Instance.WindowState = FormWindowState.Normal;
                        FrmDispFire.Instance.Activate();
                    }
                    break;
                case ID.ID_DISP_FIRE_L: //за сутки
                    m_iDispFireTime = 2;
                    FrmDispFire.Instance = new FrmDispFire();
                    FrmDispFire.Instance.Show();
                    break;
                case ID.ID_DISP_FIRE_N: //текущие
                    m_iDispFireTime = 3;
                    FrmDispFire.Instance = new FrmDispFire();
                    FrmDispFire.Instance.Show();
                    break;
                case ID.ID_BORT_OBL:
                    FrmBortDrvObl.Instance = new FrmBortDrvObl();
                    FrmBortDrvObl.Instance.m_iLoadDispRpt = 1;
                    FrmBortDrvObl.Instance.ShowDialog();
                    break;
                case ID.ID_MSG_SERVICE:
                    FrmMsgService.Instance = new FrmMsgService();
                    FrmMsgService.Instance.ShowDialog();
                    break;
                case ID.ID_BORT_PCH:
                    FrmTechUnit.Instance = new FrmTechUnit();
                    FrmTechUnit.Instance.Show();
                    break;
                case ID.ID_BORT_NO_PCH:
                    FrmTechNoUnit.Instance = new FrmTechNoUnit();
                    FrmTechNoUnit.Instance.Show();
                    break;
                case ID.ID_RPT_DAY:
                    FrmReport.Instance = new FrmReport();
                    FrmReport.Instance.Show();
                    break;
                case ID.ID_RPT_ARCH:
                    FrmReportPrintArch.Instance = new FrmReportPrintArch();
                    FrmReportPrintArch.Instance.Show();
                    break;
                case ID.ID_DRV_TECH:
                    FrmReportInfoDrv.Instance = new FrmReportInfoDrv();
                    FrmReportInfoDrv.Instance.Show();
                    break;
                case ID.ID_OBJ_UKG:
                    FrmObjFind.Instance = new FrmObjFind();
                    FrmObjFind.Instance.Show();
                    FrmObjFind.Instance.c1CmbStreetObj.Focus();
                    break;
                case ID.ID_OBJ_MON:
                    if (!m_blFrmMonAlert)
                        SetAlert(true, 1, "777#41015"); // 1 заменить после тестировани€ на 2
                    else
                        SetAlert(false, 2, "777#1111");
                    break;
                case ID.ID_OBJ_MON_DAY:
                    
                    break;
                case ID.ID_OBJ_MON_ARCH:
                    FrmMonDisp.Instance = new FrmMonDisp();
                    FrmMonDisp.Instance.Show();
                    FrmMonDisp.Instance.c1FlxGrdMonAlt.Select(1, 1);
                    FrmMonDisp.Instance.c1FlxGrdMonAlt.Focus();
                    break;
                case ID.ID_MAP_UKG:
                    FrmMapUkg frmMap = new FrmMapUkg();
                    frmMap.MdiParent = this;
                    frmMap.Show();
                    frmMap.Text = "√ород"; //+lDocumentCount.ToString();
                    frmMap.axMapUkg.Focus();
                    break;
                case ID.ID_UNIT_NOTE:
                    if (!m_blFrmUnitNote)
                    {
                        m_blFrmUnitNote = true;
                        FrmUnitNote.Instance = new FrmUnitNote();
                        FrmUnitNote.Instance.Show();
                    }
                    else
                    {
                        FrmUnitNote.Instance.WindowState = FormWindowState.Normal;
                        FrmUnitNote.Instance.Activate();
                    }
                    break;
                case ID.ID_UNIT_NOTE_ARCH:
                    FrmUnitNoteArch.Instance = new FrmUnitNoteArch();
                    FrmUnitNoteArch.Instance.Show();
                    break;
                case ID.ID_STREET:
                    FrmStreet.Instance = new FrmStreet();
                    FrmStreet.Instance.Show();
                    break;
                case ID.ID_WATER:
                    FrmWater.Instance = new FrmWater();
                    FrmWater.Instance.Show();
                    break;
                case ID.ID_POISON:
                    FrmSdjvName.Instance = new FrmSdjvName();
                    FrmSdjvName.Instance.Show();
                    break;
            }
        }

        private void ConnectToServer()
        {
            //string strSrvIP = "ics-sb";
            //string strSrvIP = "10.21.23.181";
            string strSrvPort = "1205";
            
            try
            {
                // Create the socket instance
                m_clientSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

                // Cet the remote IP address
                IPAddress ip = IPAddress.Parse(strSrvIP);
                int iPortNo = System.Convert.ToInt16(strSrvPort);
                // Create the end point 
                IPEndPoint ipEnd = new IPEndPoint(ip, iPortNo);
                // Connect to the remote host
                m_clientSocket.Connect(ipEnd);
                if (m_clientSocket.Connected)
                {
                    //m_blSendSrv = true;
                    //string strF = ("" + IPAddress.Parse(((IPEndPoint)m_clientSocket.RemoteEndPoint).Address.ToString ())); 
                    
                    /*IPHostEntry iphostentry = Dns.GetHostEntry(strSrvIP);// определ€ет только если в домене или одной подсетке
                    strRemSrvName = iphostentry.HostName;*/
                    strRemSrvName = strSrvIP;
                    UpdateControls(true);
                    //Wait for data asynchronously 
                    WaitForData();
                }
                //else
                    //m_blSendSrv = false;
            }
            catch (SocketException se)
            {
                string str;
                str = "\nConnection failed, is the server running?\n" + se.Message;
                //MessageBox.Show(str);
                UpdateControls(false);
            }
        }

        public void WaitForData()
        {
            try
            {
                if (m_pfnCallBack == null)
                {
                    m_pfnCallBack = new AsyncCallback(OnDataReceived);
                }
                SocketPacket theSocPkt = new SocketPacket();
                theSocPkt.thisSocket = m_clientSocket;
                // Start listening to the data asynchronously
                m_result = m_clientSocket.BeginReceive(theSocPkt.dataBuffer,
                                                        0, theSocPkt.dataBuffer.Length,
                                                        SocketFlags.None,
                                                        m_pfnCallBack,
                                                        theSocPkt);
            }
            catch (SocketException se)
            {
                string strSe = se.Message;
                //MessageBox.Show(se.Message);
            }

        }
        public class SocketPacket
        {
            public System.Net.Sockets.Socket thisSocket;
            public byte[] dataBuffer = new byte[1024];
        }

        public void OnDataReceived(IAsyncResult asyn)
        {
            try
            {
                SocketPacket theSockId = (SocketPacket)asyn.AsyncState;
                int iRx = theSockId.thisSocket.EndReceive(asyn);
                char[] chars = new char[iRx + 1];
                System.Text.Decoder d = System.Text.Encoding.UTF8.GetDecoder();
                int charLen = d.GetChars(theSockId.dataBuffer, 0, iRx, chars, 0);
                System.String szData = new System.String(chars);
                int iMsgAlert = 0;
                if (szData == "\0") //"\0" признак - сервер разорвал соединение
                {
                    if (blConSrv)
                        UpdateControls(false);
                }
                else
                {
                    string[] strAlertPath;
                    strAlertPath = szData.Split('#');
                    iMsgAlert = Convert.ToInt32(strAlertPath[0]);
                }
                switch (iMsgAlert)
                {
                    case 777:
                        if (!m_blFrmMonAlert)
                            SetAlert(true, 1, szData.ToString());
                        else
                            SetAlert(false, 1, szData.ToString());
                        
                        break;
                }

                WaitForData();
            }

            catch (ObjectDisposedException)
            {
                //System.Diagnostics.Debugger.Log(0, "1", "\nOnDataReceived: Socket has been closed\n");
                //tlStripStatLabelCnt.Text = "No";
            }
            catch (SocketException se)
            {
                MessageBox.Show(se.Message);
            }
        }

        private void UpdateControls(bool connected)
        {
            string connectStatus;
            if (connected)
            {
                connectStatus = "Connected:" + strRemSrvName;
                blConSrv = true;
                m_blSendSrv = true;
                SetText(connectStatus, true);
            }
            else
            {
                connectStatus = "Not Connected";
                blConSrv = false;
                m_blSendSrv = false;
                SetText(connectStatus, false);
            }
         }

        private void ThreadStartSock()
        {
            selThread = new Thread(new ThreadStart(ThreadProcSock));
            selThread.Start();
        }

        private void ThreadProcSock()
        {
            string connectStatus;
            if (blConSrv)
            {
                connectStatus = "Connected:" + strRemSrvName;
                SetText(connectStatus, true);
            }
            else
                SetText("Not Connected", false);
            SetAlert(true, 0, "");
        }

        private void SetAlert(bool blSt, int iStart, string strMsgMon)
        {
            if (statStripMain.InvokeRequired)
            {
                SetAlertCallback d = new SetAlertCallback(SetAlert);
                this.Invoke(d, new object[] { blSt, iStart, strMsgMon });
            }
            else
            {
                switch (iStart)
                {
                    case 1: //открытие окна получена тревого с объекта
                        if (blSt)
                        {
                            FrmMonAlert.Instance = new FrmMonAlert();
                            FrmMonAlert.Instance.Show();
                        }
                        else
                            FrmMonAlert.Instance.Activate();
                        FrmMonAlert.Instance.m_strLiterMon = strMsgMon;
                        FrmMonAlert.Instance.m_ActivMonAlert();
                        break;
                    case 2: //открытие окна из главного меню
                        if (blSt)
                        {
                            FrmMonAlert.Instance = new FrmMonAlert();
                            FrmMonAlert.Instance.Show();
                        }
                        else
                            FrmMonAlert.Instance.Activate();
                        break;
                }
            }
        }

        private void SetText(string text, bool blCn)
        {
            if (statStripMain.InvokeRequired)
            {
                SetTextCallback d = new SetTextCallback(SetText);
                this.Invoke(d, new object[] { text, blCn });
            }
            else
            {
                if (blCn)
                {
                    if(tmrFrmMain1.Enabled)// включить!!! 
                        tmrFrmMain1.Enabled = false;    // отключает таймер
                    tlStripStatLabelCnt.ForeColor = Color.Blue;
                }
                else
                {
                    if (!tmrFrmMain1.Enabled)// включить!!! 
                        tmrFrmMain1.Enabled = true;
                    tlStripStatLabelCnt.ForeColor = Color.Red;
                }
                tlStripStatLabelCnt.Text = text;
            }
            
        }

        private void FrmMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            tmrFrmMain1.Enabled = false;
            if (m_clientSocket != null)
            {
                m_clientSocket.Close();
                m_clientSocket = null;
            }
        }

        private void tmrFrmMain1_Tick(object sender, EventArgs e)
        {
            if (!blConSrv)
                bckgrndWorkerMain.RunWorkerAsync();
        }

        private void bckgrndWorkerMain_DoWork(object sender, DoWorkEventArgs e)
        {
            //blConSrv = true;
            ConnectToServer();
        }

        private void bckgrndWorkerMain_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            //bckgrndWorkerMain.
            //blConSrv = false;
        }

        

        
    }
}