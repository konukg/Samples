using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.Collections;
using System.Data.SqlClient;
using System.Runtime.InteropServices;
using System.Timers;

using System.Management;

using System.CodeDom.Compiler;
//using System.Collections.Generic;
using System.Reflection;
using Microsoft.CSharp;


namespace SrvGpsIcs
{
    public partial class FrmMain : Form
    {
        static public FrmMain Instance;

        bool l_blFrmClose = false;
        bool l_blFrmAcivate = false;

        int l_iFndProg = 0; //признак запущена программа или нет
        private int m_clientCountGprs = 0;
        private int m_clientCountLan = 0;

        [DllImport("User32.dll")]
        public static extern int FindWindowA(string lpClassName, string lpWindowName);
        [DllImport("User32.dll")]
        public static extern int PostMessageA(int hwnd, int wMsg, int wParam, int lParam);

        string l_strPortGprs = "0"; //номер порта GPRS - 10001
        string l_strPortLan = "0"; //номер порта LAN    - 11222

        public string m_strSqlName = ""; //имя SQL сервера
        public string m_strSqlLog = ""; //логин SQL сервера
        public string m_strSqlPass = ""; //пароль SQL сервера
        public string m_strSqlBase = ""; //имя базы данных SQL сервера

        public delegate void UpdateClientListCallback();
        public delegate void UpdateControlsCallback(bool text);
        public delegate void UpdateRichEditCallback(string text);

        public AsyncCallback pfnWorkerCallBackGprs;
        private Socket m_mainSocketGprs;
        private ArrayList m_workerSocketListGprs = ArrayList.Synchronized(new System.Collections.ArrayList());

        public AsyncCallback pfnWorkerCallBackLan;
        private Socket m_mainSocketLan;
        private ArrayList m_workerSocketListLan = ArrayList.Synchronized(new System.Collections.ArrayList());

        public BackgroundWorker m_backgrndWrkSql = new BackgroundWorker();
        BackgroundWorker l_backgrndWrkProcesorId = new BackgroundWorker();
        BackgroundWorker l_backgrndWrkDtTmEnd = new BackgroundWorker();

        public SqlConnection m_cnn;
        public bool m_blConSql = false;     //если true то есть соединение с сервером SQL
        bool l_blFirstConnSql = false;      // принимает значение true после первого старта чтобы не запускать автоматом FrmConnSQL

       //private int l_iRandTimers = 0; // случайный множитель для периода таймера
        
        public FrmMain()
        {
            l_iFndProg = FindWindowA(null, "Сервер GPRS-LAN");
            this.KeyPreview = true;

            m_backgrndWrkSql.DoWork += new DoWorkEventHandler(backgrndWrkSql_DoWork);
            m_backgrndWrkSql.RunWorkerCompleted += new RunWorkerCompletedEventHandler(backgrndWrkSql_RunWorkerCompleted);

            InitializeComponent();
        }

        private void StopServerGprs(bool msg)
        {
            if (InvokeRequired)
            {
                object[] pList = { msg };
                c1ListClientGprs.BeginInvoke(new UpdateControlsCallback(onStopUpdateControls), pList);
            }
            else
            {
                onStopUpdateControls(msg);
            }
        }

        private void onStopUpdateControls(bool listening)
        {
            c1BtnStart.Enabled = listening;
            c1BtnStop.Enabled = listening;
        }

        private void backgrndWrkSql_DoWork(object sender, DoWorkEventArgs e)
        {
            //tlStripBtnSql.Enabled = false;

            try
            {
                m_blConSql = false;
                
                string strConnSqlSrv = "server=" + m_strSqlName + ";user id=" + m_strSqlLog + ";" +
                                        "password=" + m_strSqlPass + ";initial catalog=" + m_strSqlBase;
                m_cnn = new SqlConnection(strConnSqlSrv);
                m_cnn.Open();
                
                m_blConSql = true;
            }
            catch (SqlException se)
            {
                //MessageBox.Show(se.Message);
                string strErr = se.Message;
            }

            if (!m_blConSql)
            {
                MessageBox.Show("Нет соединение с SQL сервером!", "SQL сервер",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void backgrndWrkSql_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (m_blConSql)
            {
                tlStripLblConSql.ForeColor = Color.Blue;
                tlStripLblConSql.Text = "SQL сервер!";

                m_cnn.Close();  // закрывает соединение с SQL
            }
            else
            {
                tlStripLblConSql.ForeColor = Color.Red;
                tlStripLblConSql.Text = "SQL сервер?";

                if (!l_blFirstConnSql)
                {
                    l_blFirstConnSql = true;
                    FrmConnSQL.Instance = new FrmConnSQL();
                    FrmConnSQL.Instance.ShowDialog();
                }
            }
            tlStripBtnSql.Enabled = true;
        }

        private void ToolStripMenuItem_Close_Click(object sender, EventArgs e)
        {
            l_blFrmClose = true;
            Close();
        }

        private void FrmMain_FormClosed(object sender, FormClosedEventArgs e)
        {
            CloseSockets();
        }

        private void FrmMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (l_blFrmClose)
                e.Cancel = false;
            else
            {
                this.Hide();
                e.Cancel = true;
            }

            //this.Hide();
            //e.Cancel = false;
        }

        private void notifyIconMain_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                this.Visible = true;
                if (this.WindowState == FormWindowState.Minimized)
                    this.WindowState = FormWindowState.Normal;

                // Activate the form.
                this.Activate();

                UpdateClientListGprs();
                UpdateClientListLan();
            }
        }

        private void FrmMain_Load(object sender, EventArgs e)
        {
            FrmMain.Instance.Height = 353; //скрывает окно сообщение от Treker

            if (l_iFndProg > 0)
            {
                notifyIconMain.Visible = false;
                
                this.Hide();
                int lgRtr = PostMessageA(l_iFndProg, ID.IDR_SHOWWINDOW, 0, 0);

                l_blFrmClose = true;
                this.Close();
            }
            else
            {
                notifyIconMain.Visible = true;
                if (File.Exists(Application.StartupPath + "\\SrvGpsIcs.ini"))
                    ReadIniFile();
                else
                    WriteIniFile();

                StartListenSrv();
            }

            //tlStripLblConSql.ForeColor = Color.Red;
            //tlStripLblConSql.Text = "SQL сервер?";

            tlStripBtnSql.Enabled = false;

            //m_backgrndWrkSql.RunWorkerAsync(); //соединение с SQL сервером

            // генератор случайных чисел
            //Random randObj = new Random();
            //l_iRandTimers = randObj.Next(6, 16); // в диапозоне 10 - 20
            //...

            //conTimerHard.Start();   //проверка ID processor

            //conTimerData.Start();   // запускает проверку даты конца работы программы
        }

        private void StartListenSrv()
        {
            try
            {
                // Check the port value
                if (l_strPortGprs == "0" || l_strPortGprs == "")
                {
                    MessageBox.Show("Введите номер порта сокета GPRS!");
                    return;
                }

                if (l_strPortLan == "0" || l_strPortLan == "")
                {
                    MessageBox.Show("Введите номер порта сокета LAN!");
                    return;
                }
                          
                // server GPRS
                int portGprs = System.Convert.ToInt32(l_strPortGprs);
                m_mainSocketGprs = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                IPEndPoint ipLocalGprs = new IPEndPoint(IPAddress.Any, portGprs);
                m_mainSocketGprs.Bind(ipLocalGprs);
                // Start listening...
                m_mainSocketGprs.Listen(40);
                // Create the call back for any client connections...
                m_mainSocketGprs.BeginAccept(new AsyncCallback(OnClientConnectGprs), null);
                //.......

                // server LAN
                int portLan = System.Convert.ToInt32(l_strPortLan);
                m_mainSocketLan = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                IPEndPoint ipLocalLan = new IPEndPoint(IPAddress.Any, portLan);
                m_mainSocketLan.Bind(ipLocalLan);
                // Start listening...
                m_mainSocketLan.Listen(40);
                // Create the call back for any client connections...
                m_mainSocketLan.BeginAccept(new AsyncCallback(OnClientConnectLan), null);
                //.......


                AppendToUpdateControlsGprs(true);
            }
            catch (SocketException se)
            {
                MessageBox.Show(se.Message);
            }
        }

        private void AppendToUpdateControlsGprs(bool msg)
        {
            if (InvokeRequired)
            {
                object[] pList = { msg };
                c1ListClientGprs.BeginInvoke(new UpdateControlsCallback(onUpdateControls), pList);
            }
            else
            {
                onUpdateControls(msg);
            }
        }

        private void onUpdateControls(bool listening)
        {
            c1BtnStart.Enabled = !listening;
            c1BtnStop.Enabled = listening;
        }

        public void OnClientConnectGprs(IAsyncResult asyn)
        {
            try
            {
                Socket workerSocket = m_mainSocketGprs.EndAccept(asyn);
                Interlocked.Increment(ref m_clientCountGprs);
                string strRmAdrNew = ((IPEndPoint)workerSocket.RemoteEndPoint).Address.ToString();

                Socket workerSocketOld = null;
                for (int i = 0; i < m_workerSocketListGprs.Count; i++)
                {
                    workerSocketOld = (Socket)m_workerSocketListGprs[i];
                    string strRmAdrOld = ((IPEndPoint)workerSocketOld.RemoteEndPoint).Address.ToString();
                    if (strRmAdrNew == strRmAdrOld)
                    {
                        workerSocketOld.Close();
                        workerSocketOld = null;
                        UpdateClientListControlGprs();
                    }
                   
                }
                m_workerSocketListGprs.Add(workerSocket);
              
                UpdateClientListControlGprs();

                WaitForDataGprs(workerSocket, m_clientCountGprs);

                m_mainSocketGprs.BeginAccept(new AsyncCallback(OnClientConnectGprs), null);
            }
            catch (ObjectDisposedException)
            {
                System.Diagnostics.Debugger.Log(0, "1", "\n OnClientConnection: Socket has been closed\n");
            }

        }

        public void OnClientConnectLan(IAsyncResult asyn)
        {
            try
            {
                Socket workerSocket = m_mainSocketLan.EndAccept(asyn);

                Interlocked.Increment(ref m_clientCountLan);
                // Add the workerSocket reference to our ArrayList
                m_workerSocketListLan.Add(workerSocket);
                // Update the list box showing the list of clients (thread safe call)
                UpdateClientListControlLan();
                // Let the worker Socket do the further processing for the 
                // just connected client
                WaitForDataLan(workerSocket, m_clientCountLan);

                // Since the main Socket is now free, it can go back and wait for
                // other clients who are attempting to connect
                m_mainSocketLan.BeginAccept(new AsyncCallback(OnClientConnectLan), null);
            }
            catch (ObjectDisposedException)
            {
                System.Diagnostics.Debugger.Log(0, "1", "\n OnClientConnection: Socket has been closed\n");
            }

        }

        public class SocketPacketGprs
        {
            // Constructor which takes a Socket and a client number
            public SocketPacketGprs(System.Net.Sockets.Socket socket, int clientNumber)
            {
                m_currentSocketGprs = socket;
                m_clientNumberGprs = clientNumber;
            }
            public System.Net.Sockets.Socket m_currentSocketGprs;
            public int m_clientNumberGprs;
            // Buffer to store the data sent by the client
            public byte[] dataBufferGprs = new byte[1024];
        }

        public class SocketPacketLan
        {
            // Constructor which takes a Socket and a client number
            public SocketPacketLan(System.Net.Sockets.Socket socket, int clientNumber)
            {
                m_currentSocketLan = socket;
                m_clientNumberLan = clientNumber;
            }
            public System.Net.Sockets.Socket m_currentSocketLan;
            public int m_clientNumberLan;
            // Buffer to store the data sent by the client
            public byte[] dataBufferLan = new byte[1024];
        }

        public void WaitForDataGprs(System.Net.Sockets.Socket soc, int clientNumberGprs)
        {
            try
            {
                if (pfnWorkerCallBackGprs == null)
                {
                    // Specify the call back function which is to be 
                    // invoked when there is any write activity by the 
                    // connected client
                    pfnWorkerCallBackGprs = new AsyncCallback(OnDataReceivedGprs);
                }
                SocketPacketGprs theSocPkt = new SocketPacketGprs(soc, clientNumberGprs);

                soc.BeginReceive(theSocPkt.dataBufferGprs, 0,
                    theSocPkt.dataBufferGprs.Length,
                    SocketFlags.None,
                    pfnWorkerCallBackGprs,
                    theSocPkt);
            }
            catch (SocketException se)
            {
                //MessageBox.Show(se.Message);
                System.Diagnostics.Debugger.Log(0, "1", "\n " + se.Message + "\n");
            }
        }

        public void WaitForDataLan(System.Net.Sockets.Socket soc, int clientNumber)
        {
            try
            {
                if (pfnWorkerCallBackLan == null)
                {
                    // Specify the call back function which is to be 
                    // invoked when there is any write activity by the 
                    // connected client
                    pfnWorkerCallBackLan = new AsyncCallback(OnDataReceivedLan);
                }
                SocketPacketLan theSocPkt = new SocketPacketLan(soc, clientNumber);

                soc.BeginReceive(theSocPkt.dataBufferLan, 0,
                    theSocPkt.dataBufferLan.Length,
                    SocketFlags.None,
                    pfnWorkerCallBackLan,
                    theSocPkt);
            }
            catch (SocketException se)
            {
                //MessageBox.Show(se.Message);
                System.Diagnostics.Debugger.Log(0, "1", "\n " + se.Message + "\n");
            }
        }

        public void OnDataReceivedGprs(IAsyncResult asyn)
        {
            SocketPacketGprs socketData = (SocketPacketGprs)asyn.AsyncState;
            try
            {
                // Complete the BeginReceive() asynchronous call by EndReceive() method
                // which will return the number of characters written to the stream 
                // by the client
                int iRx = socketData.m_currentSocketGprs.EndReceive(asyn);
                char[] chars = new char[iRx + 1];
                // Extract the characters as a buffer
                System.Text.Decoder d = System.Text.Encoding.ASCII.GetDecoder();
                int charLen = d.GetChars(socketData.dataBufferGprs, 0, iRx, chars, 0);
                System.String szData = new System.String(chars);

                if (szData == "\0")
                {
                    socketData.m_currentSocketGprs.Close();
                    socketData.m_currentSocketGprs = null;
                    m_clientCountGprs--;
                    UpdateClientListControlGprs();
                }
                else
                {
                    //HiPerfTimer pt = new HiPerfTimer();   // create a new PerfTimer object
                    //pt.Start();
                    ParseRcvMsg(szData, socketData);
                    //pt.Stop();
                    //double dbDur = pt.Duration;
                    //tlStripLblTime.Text = dbDur.ToString();
                    //System.String strDur = "\n" + dbDur.ToString() + "-Duration" + "\n";
                    //szData = szData + strDur;
                    if(c1CheckBoxMsg.Checked)
                        AppendToRichEditControl(szData);
                    // Continue the waiting for data on the Socket
                    WaitForDataGprs(socketData.m_currentSocketGprs, socketData.m_clientNumberGprs);
                }
            }
            catch (ObjectDisposedException)
            {
                System.Diagnostics.Debugger.Log(0, "1", "\nOnDataReceived: Socket has been closed\n");
            }
            catch (SocketException se)
            {
                if (se.ErrorCode == 10054 || se.ErrorCode == 10053) // Error code for Connection reset by peer
                {
                    socketData.m_currentSocketGprs.Close();
                    socketData.m_currentSocketGprs = null;
                    m_clientCountGprs--;
                    UpdateClientListControlGprs();
                }
                else
                {
                    //MessageBox.Show(se.Message);
                    System.Diagnostics.Debugger.Log(0, "1", "\n " + se.Message + "\n");
                }
            }
        }

        private void SendMsgClientLan(string msg)
        {
            try
            {
                byte[] byData = System.Text.Encoding.ASCII.GetBytes(msg);
                Socket workerSocket = null;
                for (int i = 0; i < m_workerSocketListLan.Count; i++)
                {
                    workerSocket = (Socket)m_workerSocketListLan[i];
                    if (workerSocket != null)
                    {
                        if (workerSocket.Connected)
                        {
                            workerSocket.Send(byData);
                        }
                    }
                }
            }
            catch (SocketException se)
            {
                string strRtr = se.Message;
            }
        }

        private void ParseRcvMsg(string strRcvMsgAll, SocketPacketGprs socketDataCrc)
        {
            string strSendMsgClient = "";
            string[] strRcvMsgBlok;
            char[] charSeparators = new char[] { '!' };

            strRcvMsgBlok = strRcvMsgAll.Split(charSeparators, StringSplitOptions.RemoveEmptyEntries);
            for (int i = 0; i < strRcvMsgBlok.Length; i++)
            {
                string strRcvCrcSend = strRcvMsgAll.Substring(strRcvMsgAll.IndexOf('*') + 1, 2);   //контрольная сумма принятого сообщения

                int iLatFldFrs = 0;
                int iLongFldFrs = 0;

                string[] strRcvMsg;
                strRcvMsg = strRcvMsgBlok[i].Split(';');
                
                string strBortTrek = "";    //HEX номер контроллера для записи в таблицу маршрутов SQL
                
                for (int i1 = 0; i1 < strRcvMsg.Length; i1++)
                {
                    string[] strBfRcvMsgFild;
                    strBfRcvMsgFild = strRcvMsg[i1].Split(',');

                    if (i1 == 0 && strBfRcvMsgFild.Length == 8)
                    {
                        strBortTrek = strBfRcvMsgFild[0];
                        // формирует ответ на полученное сообщение
                        string strSendCrc = strBfRcvMsgFild[0] + "," + "FF," + strBfRcvMsgFild[1] + "," + strRcvCrcSend;
                        string strCrc = computeCheckSum(strSendCrc);
                        strSendCrc = "!" + strSendCrc + "*" + strCrc;

                        byte[] byData = System.Text.Encoding.ASCII.GetBytes(strSendCrc);
                        Socket workerSocket = (Socket)socketDataCrc.m_currentSocketGprs;
                        workerSocket.Send(byData);
                        //....
                        // первые координаты в блоке сообщения
                        iLatFldFrs = Convert.ToInt32(strBfRcvMsgFild[4], 16);
                        iLongFldFrs = Convert.ToInt32(strBfRcvMsgFild[5], 16);
                        // формирует первое сообщение в блоке для передачи клиентам
                        strSendMsgClient = "!" + strBfRcvMsgFild[0] + "," + strBfRcvMsgFild[2] + "," + strBfRcvMsgFild[3] + ","
                            + strBfRcvMsgFild[4] + "," + strBfRcvMsgFild[5] + "," + strBfRcvMsgFild[6] + "," + strBfRcvMsgFild[7];
                    }

                    if (i1 > 0 && strBfRcvMsgFild.Length == 5 && strSendMsgClient != "")
                    {
                        // следующие координаты в блоке сообщения
                        int iLatFldNext = Convert.ToInt32(strBfRcvMsgFild[1], 16);
                        int iLongFldNext = Convert.ToInt32(strBfRcvMsgFild[2], 16);
                        if (iLatFldNext != iLatFldFrs || iLongFldNext != iLongFldFrs)
                        {
                            iLatFldFrs = iLatFldNext;
                            iLongFldFrs = iLongFldNext;
                            // формирует следующее сообщение в блоке для передачи клиентам
                            string strSendMsgNext = strBfRcvMsgFild[0] + "," + strBfRcvMsgFild[1] + "," + strBfRcvMsgFild[2] + "," + strBfRcvMsgFild[3] + "," + strBfRcvMsgFild[4];
                            strSendMsgClient = strSendMsgClient + "#" + strSendMsgNext;
                        }
                    }
                }
                if (strSendMsgClient != "")
                {
                    SendMsgClientLan(strSendMsgClient); // посылает сообщение всем клиентам LAN

                    /*string strBort = Convert.ToInt32(strBortTrek, 16).ToString();   // десятичное значение контроллера ID
                    string strConnSql = "server=" + m_strSqlName + ";user id=" + m_strSqlLog + ";" +
                                        "password=" + m_strSqlPass + ";initial catalog=" + m_strSqlBase;
                    SendRecSqlData clsRecSql = new SendRecSqlData(strConnSql, strBort, strSendMsgClient); */  //выполняет запись в сервер SQL
                }
            }
            //return strSendMsgClient;
        }

        private string computeCheckSum(string values)
        {
            int count = 0;
            ushort Conv = 0;

            for (int x = 0; x < values.Length; x++)
            {
                Conv = values[x];
                if (count + Conv > 255)
                    count = (count + Conv) - 256;
                else
                    count = count + Conv;
            }

            //Convert the count to HEX
            string ConvX = (count).ToString("X");

            return ConvX;
        }

        private void AppendToRichEditControl(string msg)
        {
            if (InvokeRequired)
            {
                object[] pList = { msg };
                richTextBoxReceivedMsg.BeginInvoke(new UpdateRichEditCallback(OnUpdateRichEdit), pList);
            }
            else
            {
                // This is the main thread which created this control, hence update it
                // directly 
                OnUpdateRichEdit(msg);
            }
        }

        private void OnUpdateRichEdit(string msg)
        {
            richTextBoxReceivedMsg.AppendText(msg);
        }

        public void OnDataReceivedLan(IAsyncResult asyn)
        {
            SocketPacketLan socketData = (SocketPacketLan)asyn.AsyncState;
            try
            {
                // Complete the BeginReceive() asynchronous call by EndReceive() method
                // which will return the number of characters written to the stream 
                // by the client
                int iRx = socketData.m_currentSocketLan.EndReceive(asyn);
                char[] chars = new char[iRx + 1];
                // Extract the characters as a buffer
                System.Text.Decoder d = System.Text.Encoding.ASCII.GetDecoder();
                int charLen = d.GetChars(socketData.dataBufferLan, 0, iRx, chars, 0);
                System.String szData = new System.String(chars);
                if (szData == "\0")
                {
                    socketData.m_currentSocketLan.Close();
                    socketData.m_currentSocketLan = null;
                    m_clientCountLan--;
                    UpdateClientListControlLan();
                }
                else
                {
                    // Continue the waiting for data on the Socket
                    WaitForDataLan(socketData.m_currentSocketLan, socketData.m_clientNumberLan);
                }
            }
            catch (ObjectDisposedException)
            {
                System.Diagnostics.Debugger.Log(0, "1", "\nOnDataReceived: Socket has been closed\n");
            }
            catch (SocketException se)
            {
                if (se.ErrorCode == 10054 || se.ErrorCode == 10053) // Error code for Connection reset by peer
                {
                    socketData.m_currentSocketLan.Close();
                    socketData.m_currentSocketLan = null;
                    m_clientCountLan--;
                    UpdateClientListControlLan();
                }
                else
                {
                    //MessageBox.Show(se.Message);
                    System.Diagnostics.Debugger.Log(0, "1", "\n " + se.Message + "\n");
                }
            }
        }

        private void UpdateClientListControlGprs()
        {
            if (InvokeRequired) // Is this called from a thread other than the one created
            // the control
            {
                c1ListClientGprs.BeginInvoke(new UpdateClientListCallback(UpdateClientListGprs), null);
            }
            else
            {
                // This is the main thread which created this control, hence update it
                // directly 
                UpdateClientListGprs();
            }
        }

        private void UpdateClientListControlLan()
        {
            if (InvokeRequired) // Is this called from a thread other than the one created
            // the control
            {
                // We cannot update the GUI on this thread.
                // All GUI controls are to be updated by the main (GUI) thread.
                // Hence we will use the invoke method on the control which will
                // be called when the Main thread is free
                // Do UI update on UI thread
                c1ListClientLan.BeginInvoke(new UpdateClientListCallback(UpdateClientListLan), null);
            }
            else
            {
                // This is the main thread which created this control, hence update it
                // directly 
                UpdateClientListLan();
            }
        }

        private void UpdateClientListGprs()
        {
            ArrayList temp_workerSocketList = ArrayList.Synchronized(new System.Collections.ArrayList());
            c1ListClientGprs.ClearItems();
            for (int i = 0; i < m_workerSocketListGprs.Count; i++)
            {
                string clientKey = (i + 1).ToString();
                Socket workerSocket = (Socket)m_workerSocketListGprs[i];
                if (workerSocket != null && workerSocket.Connected)
                {
                    string strLocAdr = ((IPEndPoint)workerSocket.LocalEndPoint).Address.ToString();
                    if (strLocAdr == "0.0.0.0")
                    {
                        workerSocket.Close();
                        workerSocket = null;
                        UpdateClientListControlGprs();
                    }
                    else
                    {
                        string strRmAdr = ((IPEndPoint)workerSocket.RemoteEndPoint).Address.ToString();
                        string strRmPort = ((IPEndPoint)workerSocket.RemoteEndPoint).Port.ToString();
                        clientKey = clientKey + "_" + strRmAdr + ":" + strRmPort;
                        c1ListClientGprs.AddItem(clientKey);
                        temp_workerSocketList.Add(workerSocket);    //заносит только рабочие сокеты
                    }
                }
            }
            m_workerSocketListGprs = temp_workerSocketList;
            c1ListClientGprs.Caption = "Клиенты - " + c1ListClientGprs.ListCount.ToString();
        }

        private void UpdateClientListLan()
        {
            ArrayList temp_workerSocketList = ArrayList.Synchronized(new System.Collections.ArrayList());
            c1ListClientLan.ClearItems();
            for (int i = 0; i < m_workerSocketListLan.Count; i++)
            {
                string clientKey = Convert.ToString(i + 1);
                Socket workerSocket = (Socket)m_workerSocketListLan[i];
                if (workerSocket != null)
                {
                    if (workerSocket.Connected)
                    {
                        string strRmAdr = workerSocket.RemoteEndPoint.ToString();
                        clientKey = clientKey + "_" + strRmAdr;
                        c1ListClientLan.AddItem(clientKey);
                        temp_workerSocketList.Add(workerSocket);    //заносит только рабочие сокеты
                        c1ListClientLan.Caption = "Клиенты - " + c1ListClientLan.ListCount.ToString();
                    }
                }
            }
            m_workerSocketListLan = temp_workerSocketList;
        }

        private void CloseSockets()
        {
            if (m_mainSocketGprs != null)
            {
                m_mainSocketGprs.Close();
            }
            Socket workerSocket = null;
            for (int i = 0; i < m_workerSocketListGprs.Count; i++)
            {
                workerSocket = (Socket)m_workerSocketListGprs[i];
                if (workerSocket != null)
                {
                    workerSocket.Close();
                    workerSocket = null;
                }
            }

            if (m_mainSocketLan != null)
            {
                m_mainSocketLan.Close();
            }
            workerSocket = null;
            for (int i = 0; i < m_workerSocketListLan.Count; i++)
            {
                workerSocket = (Socket)m_workerSocketListLan[i];
                if (workerSocket != null)
                {
                    workerSocket.Close();
                    workerSocket = null;
                }
            }

            UpdateClientListControlGprs();
            UpdateClientListControlLan();
        }

        private void ReadIniFile()
        {
            IniFile ini = new IniFile(Application.StartupPath + "\\SrvGpsIcs.ini");

            l_strPortGprs = ini.ReadValue("IP", "PortGprs", l_strPortGprs);
            l_strPortLan = ini.ReadValue("IP", "PortLan", l_strPortLan);
            m_strSqlName = ini.ReadValue("SQL", "Name", m_strSqlName);
            m_strSqlLog = ini.ReadValue("SQL", "Login", m_strSqlLog);
            m_strSqlPass = ini.ReadValue("SQL", "Pass", m_strSqlPass);
            m_strSqlBase = ini.ReadValue("SQL", "Base", m_strSqlBase);
        }

        private void WriteIniFile()
        {
            IniFile ini = new IniFile(Application.StartupPath + "\\SrvGpsIcs.ini");

            ini.WriteValue("IP", "PortGprs", l_strPortGprs);
            ini.WriteValue("IP", "PortLan", l_strPortLan);
            ini.WriteValue("SQL", "Name", m_strSqlName);
            ini.WriteValue("SQL", "Login", m_strSqlLog);
            ini.WriteValue("SQL", "Pass", m_strSqlPass);
            ini.WriteValue("SQL", "Base", m_strSqlBase);
        }

        protected override void WndProc(ref Message msg)
        {
          switch(msg.Msg)
          {
           // If message is of interest, invoke the method on the form that
           // functions as a callback to perform actions in response to the message.
           case ID.IDR_SHOWWINDOW:
                this.Visible = true;
                if (this.WindowState == FormWindowState.Minimized)
                    this.WindowState = FormWindowState.Normal;

                // Activate the form.
                this.Activate();
            break;
           case 24:
                //c1TxtBoxPortGprs.DataType = typeof(string);
                //c1TxtBoxPortGprs.Text = l_strPortGprs;
                //c1TxtBoxPortGprs.DataType = typeof(int);
            break;
          }
          // Call the base WndProc method
          // to process any messages not handled.
          base.WndProc(ref msg);
        }

        private void FrmMain_Activated(object sender, EventArgs e)
        {
            if (!l_blFrmAcivate)
            {
                c1TxtBoxPortGprs.DataType = typeof(string);
                c1TxtBoxPortGprs.Text = l_strPortGprs;
                c1TxtBoxPortGprs.DataType = typeof(int);

                c1TxtBoxPortLan.DataType = typeof(string);
                c1TxtBoxPortLan.Text = l_strPortLan;
                c1TxtBoxPortLan.DataType = typeof(int);

                label1.Focus();
            }
            l_blFrmAcivate = true;
        }

        private void c1BtnStart_Click(object sender, EventArgs e)
        {
            CloseSockets();

            l_strPortGprs = c1TxtBoxPortGprs.Text;
            l_strPortLan = c1TxtBoxPortLan.Text;

            IniFile ini = new IniFile(Application.StartupPath + "\\SrvGpsIcs.ini");
            ini.WriteValue("IP", "PortGprs", l_strPortGprs);
            ini.WriteValue("IP", "PortLan", l_strPortLan);

            StartListenSrv();
            c1BtnStop.Focus();
        }

        private void c1BtnStop_Click(object sender, EventArgs e)
        {
            CloseSockets();
            AppendToUpdateControlsGprs(false);
            c1BtnStart.Focus();
        }

        private void c1CheckBoxMsg_CheckedChanged(object sender, EventArgs e)
        {
            if (!c1CheckBoxMsg.Checked)
                richTextBoxReceivedMsg.Clear();
        }

        private void c1ListClientGprs_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            UpdateClientListGprs();
        }

        private void FrmMain_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F11 && (e.Alt && e.Control && e.Shift))
            {
                FrmMain.Instance.Height = 353;
            }

            if (e.KeyCode == Keys.F12 && (e.Alt && e.Control && e.Shift))
            {
                FrmMain.Instance.Height = 555;
            }
        }

        private void tlStripBtnSql_Click(object sender, EventArgs e)
        {
            FrmConnSQL.Instance = new FrmConnSQL();
            FrmConnSQL.Instance.ShowDialog();
        }

        private void c1BtnTest_Click(object sender, EventArgs e)
        {
           
        }
    }
}
