using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using System.Net;
using System.Net.Sockets;
using System.Collections;
using System.Threading;
using System.Xml.Linq;
using System.Xml;
using System.IO;

using AXSMSCFGLib;

namespace SrvSmsIcs
{
    public partial class FrmMain : Form
    {
        string l_strFileCnf = "CnfSrvSms.xml";
        string l_strPort = "11222"; //номер порта LAN    - 11222
        private int m_clientCount = 0;

        public AsyncCallback pfnWorkerCallBack;
        private Socket m_mainSocket;
        private ArrayList m_workerSocketList = ArrayList.Synchronized(new System.Collections.ArrayList());

        public delegate void UpdateClientListCallback();
        public delegate void UpdateRichEditCallback(string text);

        private IXMessageDB objMessageDB;
        private IXConstants objConstants;

        public FrmMain()
        {
            InitializeComponent();
            objMessageDB = new XMessageDB();
            objConstants = new XConstants();
        }

        private void FrmMain_Load(object sender, EventArgs e)
        {
            StartListenSrv();
        }

        private void StartListenSrv()
        {
            try
            {
                if (l_strPort == "0" || l_strPort == "")
                {
                    MessageBox.Show("Введите номер порта сокета!");
                    return;
                }

                // server LAN
                int portLan = System.Convert.ToInt32(l_strPort);
                m_mainSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                IPEndPoint ipLocalLan = new IPEndPoint(IPAddress.Any, portLan);
                m_mainSocket.Bind(ipLocalLan);
                // Start listening...
                m_mainSocket.Listen(40);
                m_mainSocket.BeginAccept(new AsyncCallback(OnClientConnect), null);
                //.......

            }
            catch (SocketException se)
            {
                MessageBox.Show(se.Message);
            }
        }

        public void OnClientConnect(IAsyncResult asyn)
        {
            try
            {
                Socket workerSocket = m_mainSocket.EndAccept(asyn);

                Interlocked.Increment(ref m_clientCount);
                m_workerSocketList.Add(workerSocket);
                
                UpdateClientListControl();
                
                WaitForData(workerSocket, m_clientCount);

                m_mainSocket.BeginAccept(new AsyncCallback(OnClientConnect), null);
            }
            catch (ObjectDisposedException)
            {
                System.Diagnostics.Debugger.Log(0, "1", "\n OnClientConnection: Socket has been closed\n");
            }

        }

        private void UpdateClientListControl()
        {
            if (InvokeRequired) 
            {
                listBoxClient.BeginInvoke(new UpdateClientListCallback(UpdateClientList), null);
            }
            else
            {
                UpdateClientList();
            }
        }

        private void UpdateClientList()
        {
            ArrayList temp_workerSocketList = ArrayList.Synchronized(new System.Collections.ArrayList());
            listBoxClient.Items.Clear();  //.ClearItems();
            for (int i = 0; i < m_workerSocketList.Count; i++)
            {
                string clientKey = Convert.ToString(i + 1);
                Socket workerSocket = (Socket)m_workerSocketList[i];
                if (workerSocket != null)
                {
                    if (workerSocket.Connected)
                    {
                        string strRmAdr = workerSocket.RemoteEndPoint.ToString();
                        clientKey = clientKey + "_" + strRmAdr;
                        listBoxClient.Items.Add(clientKey);     
                        temp_workerSocketList.Add(workerSocket);    //заносит только рабочие сокеты
                        
                    }
                }
            }
            m_workerSocketList = temp_workerSocketList;
        }

        public class SocketPacket
        {
            // Constructor which takes a Socket and a client number
            public SocketPacket(System.Net.Sockets.Socket socket, int clientNumber)
            {
                m_currentSocket = socket;
                m_clientNumber = clientNumber;
            }
            public System.Net.Sockets.Socket m_currentSocket;
            public int m_clientNumber;
            
            public byte[] dataBufferLan = new byte[1024];
        }

        public void WaitForData(System.Net.Sockets.Socket soc, int clientNumber)
        {
            try
            {
                if (pfnWorkerCallBack == null)
                {
                    
                    pfnWorkerCallBack = new AsyncCallback(OnDataReceived);
                }
                SocketPacket theSocPkt = new SocketPacket(soc, clientNumber);

                soc.BeginReceive(theSocPkt.dataBufferLan, 0,
                    theSocPkt.dataBufferLan.Length,
                    SocketFlags.None,
                    pfnWorkerCallBack,
                    theSocPkt);
            }
            catch (SocketException se)
            {
                //MessageBox.Show(se.Message);
                System.Diagnostics.Debugger.Log(0, "1", "\n " + se.Message + "\n");
            }
        }

        public void OnDataReceived(IAsyncResult asyn)
        {
            SocketPacket socketData = (SocketPacket)asyn.AsyncState;
            try
            {
                
                int iRx = socketData.m_currentSocket.EndReceive(asyn);
                char[] chars = new char[iRx + 1];
                
                
                System.Text.Decoder d = System.Text.Encoding.Default.GetDecoder();
                //System.Text.Decoder d = System.Text.Encoding.Unicode.GetDecoder();
                int charLen = d.GetChars(socketData.dataBufferLan, 0, iRx, chars, 0);
                System.String szData = new System.String(chars);
                if (szData == "\0")
                {
                    socketData.m_currentSocket.Close();
                    socketData.m_currentSocket = null;
                    m_clientCount--;
                    UpdateClientListControl();
                }
                else
                {
                    ParseRcvMsg(szData);
                   
                    // Continue the waiting for data on the Socket
                    WaitForData(socketData.m_currentSocket, socketData.m_clientNumber);
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
                    socketData.m_currentSocket.Close();
                    socketData.m_currentSocket = null;
                    m_clientCount--;
                    UpdateClientListControl();
                }
                else
                {
                    //MessageBox.Show(se.Message);
                    System.Diagnostics.Debugger.Log(0, "1", "\n " + se.Message + "\n");
                }
            }
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
                OnUpdateRichEdit(msg);
            }
        }

        private void OnUpdateRichEdit(string msg)
        {
            richTextBoxReceivedMsg.AppendText(msg);
        }

        private void ParseRcvMsg(string strRcvMsgAll)
        {
            
            string strPrs = strRcvMsgAll.Substring(0, strRcvMsgAll.Length - 1);
            var dstSmsXml = XElement.Parse(strPrs);

            string strSms = strRcvMsgAll.Substring(1, 3);
            string strSmsEnd = strRcvMsgAll.Substring(strPrs.Length - 2, 2);
            
            if (strSms == "sms" && strSmsEnd == "/>")
            {
                string strPhone = dstSmsXml.Attribute("phone").Value.ToString();
                string strMessage = dstSmsXml.Attribute("message").Value.ToString();

                AppendToRichEditControl(strPhone);
                AppendToRichEditControl(strMessage);
                AppendToRichEditControl("");

                SendSmsSrv(strPhone, strMessage);
            }
            

        }

        private void SendSmsSrv(string strSmsNumber, string strSmsMsg)
        {
            //HiPerfTimer pt = new HiPerfTimer();   // create a new PerfTimer object
            //pt.Start();

            objMessageDB.Open();

            if (objMessageDB.LastError > 0)
            {
                AppendToRichEditControl("Failed to open message database, error: " + objMessageDB.LastError.ToString());
                return;
            }

            object ob = (object)objMessageDB.Create();

            IXMessage objMessage = (IXMessage)ob;

            if (objMessageDB.LastError > 0)
            {
                AppendToRichEditControl("Create Failed1, error: " + objMessageDB.LastError.ToString());
                return;
            }

            AppendToRichEditControl("Message successfully created, recordID: " + objMessage.ID.ToString());

            objMessage.Direction = objConstants.MESSAGEDIRECTION_OUT;
            objMessage.Type = objConstants.MESSAGETYPE_SMS;
            objMessage.Status = objConstants.MESSAGESTATUS_PENDING;
            objMessage.ChannelID = 0;
            objMessage.ScheduledTime = "";
            objMessage.Recipient = strSmsNumber;
            objMessage.Body = strSmsMsg;
            objMessage.BodyFormat = objConstants.MESSAGEBODYFORMAT_UNICODE;

            objMessageDB.Save(ref ob);

            if (objMessageDB.LastError > 0)
            {
                AppendToRichEditControl("Update message failed, error: " + objMessageDB.LastError.ToString());

                return;
            }
            objMessageDB.Close();

            //pt.Stop();
            //txtBoxErr.Text = pt.Duration.ToString();
        }

        private void WriteCnfFile()
        {
            XmlWriterSettings xws = new XmlWriterSettings();
            xws.OmitXmlDeclaration = true;
            xws.Indent = true;

            using (XmlWriter xw = XmlWriter.Create(l_strFileCnf, xws))
            {
                xw.WriteStartElement("Root");

                XElement child1 = new XElement("SockWAN",
                    new XElement("Port", l_strPort)
                );
                child1.WriteTo(xw);
                xw.WriteEndElement();
            }
        }

        private void ReadCnfFile()
        {
            XElement xmlTree2 = XElement.Load(l_strFileCnf);
            
            foreach (XElement e1 in xmlTree2.DescendantsAndSelf())
            {
                string stringMsg = e1.Value.ToString();
                string strName = e1.Name.ToString();
                switch (strName)
                {
                    case "Port":
                        textBox1.Text = stringMsg;
                        break;
                }

            }
        }

        private void button1_Click(object sender, EventArgs e)
        {

            if (File.Exists(l_strFileCnf))
            {
                ReadCnfFile();
            }
            else
            {
                WriteCnfFile();
            }
            
        }
    }
}
