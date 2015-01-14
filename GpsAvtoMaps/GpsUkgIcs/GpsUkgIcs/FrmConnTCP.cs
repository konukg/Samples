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

namespace GpsUkgIcs
{
    public partial class FrmConnTCP : Form
    {
        static public FrmConnTCP Instance;

        Socket clientSocket;
        bool l_blConTCP = false;

        public FrmConnTCP()
        {
            InitializeComponent();
        }

        private void FrmConnTCP_Load(object sender, EventArgs e)
        {
            c1TxtBoxSrvIp.Text = FrmMain.Instance.m_strAdrIP;
            c1TxtBoxPort.Text = FrmMain.Instance.m_strPortIP;
        }

        private void FrmConnTCP_Activated(object sender, EventArgs e)
        {
            c1BtnTcp.Focus();
        }

        private void c1BtnTcp_Click(object sender, EventArgs e)
        {
            FrmConnTCP.Instance.Cursor = Cursors.WaitCursor;
            try
            {
                // Create the socket instance
                clientSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

                IPAddress ip = IPAddress.Parse(c1TxtBoxSrvIp.Text);
                int iPortNo = System.Convert.ToInt16(c1TxtBoxPort.Text);
                // Create the end point 
                IPEndPoint ipEnd = new IPEndPoint(ip, iPortNo);
                // Connect to the remote host
                clientSocket.Connect(ipEnd);
                if (clientSocket.Connected)
                {
                    l_blConTCP = true;
                }
            }
            catch (SocketException se)
            {
                string str = "\nСоединение не установлено, сервер работает?\n\n" + se.Message;
                MessageBox.Show(str);
            }
            this.Cursor = Cursors.Arrow;
            if (l_blConTCP)
            {
                FrmMain.Instance.m_strAdrIP = c1TxtBoxSrvIp.Text;
                FrmMain.Instance.m_strPortIP = c1TxtBoxPort.Text;


                IniFile ini = new IniFile(Application.StartupPath + "\\ClnGpsUkgIcs.ini");
                ini.WriteValue("IP", "Port", c1TxtBoxPort.Text);
                ini.WriteValue("IP", "AdrIP", c1TxtBoxSrvIp.Text);


                MessageBox.Show("Есть соединение с сервером!", "TCP сервер",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);

                clientSocket.Close();
            }
           
        }
    }
}
