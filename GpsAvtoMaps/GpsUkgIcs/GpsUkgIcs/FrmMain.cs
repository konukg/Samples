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
using System.Timers;
using System.Data.SqlClient;
using C1.Win.C1FlexGrid;


using System.Runtime.InteropServices;

namespace GpsUkgIcs
{
    public partial class FrmMain : Form
    {
        static public FrmMain Instance;

        public BackgroundWorker m_backgrndWrkSql = new BackgroundWorker();
        public BackgroundWorker m_backgrndWrkTcp = new BackgroundWorker();

        public string m_strPortIP = "11222"; //номер порта
        public string m_strAdrIP = "10.10.10.10"; //IP адрес
        public string m_strSqlName = ""; //имя SQL сервера
        public string m_strSqlLog = ""; //логин SQL сервера
        public string m_strSqlPass = ""; //пароль SQL сервера
        public string m_strSqlBase = ""; //имя базы данных SQL сервера

        IAsyncResult m_result;
        public AsyncCallback m_pfnCallBack;
        public Socket m_clientSocket;
        public delegate void UpdateLblConnectCallback(bool text);
        public delegate void UpdateReciveDataCallback(string text);

        public SqlConnection m_cnn;
        public bool m_blConSql = false;     //если true то есть соединение с сервером SQL
        bool l_blFirstConnSql = false;      // принимает значение true после первого старта чтобы не запускать автоматом FrmConnSQL

        BindingSource bsFindBort = new BindingSource();
        DataSet dsFindBort = new DataSet();

        MapXLib.Layer l_lyrUkgN16, l_lyrUkgN18; //, l_lyrUkgN19;
        MapXLib.Layer lyrMyLayer;
        MapXLib.Style stNewSmb = new MapXLib.Style();
        MapXLib.Point ptNewSmb = new MapXLib.Point();

        System.Timers.Timer conTimerTcp = new System.Timers.Timer(10000);

        //запись и чтение в коллекции объектов
        SortedList<int, C1.Win.C1SuperTooltip.C1SuperTooltip> l_srMapLabel = new SortedList<int, C1.Win.C1SuperTooltip.C1SuperTooltip>();

        C1.Win.C1SuperTooltip.C1SuperTooltip c1SprTooltipBortUkg = new C1.Win.C1SuperTooltip.C1SuperTooltip();

        int l_iTestRec = 0;

        int l_iColTrekId_0 = 0;    // Id контроллера
        //int l_iColChek_1 = 1;    // поле Chek - не используется
        int l_iColAlias_2 = 2;    // позывной борта
        int l_iColData_3 = 3;    // дата и время
        int l_iColMapId_4 = 4;    // Id точки на карте
        int l_iColLat_5 = 5;    // координаты Lat
        int l_iColLon_6 = 6;    // координаты Lon
        int l_iColInfo_7 = 7;    // информация - гос.№, п/часть и т.п

        [DllImport("User32.dll")]
        public static extern int FindWindowA(string lpClassName, string lpWindowName);
        [DllImport("User32.dll")]
        public static extern int PostMessageA(int hwnd, int wMsg, int wParam, int lParam);

        public FrmMain()
        {
            InitializeComponent();

            InitializeBackgoundWorker();

            conTimerTcp.Elapsed += new ElapsedEventHandler(OnTimerEventConnToTcp);
            conTimerTcp.AutoReset = true;
        }

        private void InitializeBackgoundWorker()
        {
            m_backgrndWrkSql.DoWork += new DoWorkEventHandler(backgrndWrkSql_DoWork);
            m_backgrndWrkSql.RunWorkerCompleted += new RunWorkerCompletedEventHandler(backgrndWrkSql_RunWorkerCompleted);

            m_backgrndWrkTcp.DoWork += new DoWorkEventHandler(backgrndWrkTcp_DoWork);
        }

        private void backgrndWrkSql_DoWork(object sender, DoWorkEventArgs e)
        {
            rbnBtnConnSql.Enabled = false;

            try
            {
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
                ribnLblConSql.ForeColorOuter = Color.Blue;
                ribnLblConSql.Text = "SQL сервер!";

                LoadBortSqlData();    //загрузка списка техники на мониторинге

                LoadMapTab(); //загрузка слоев карты           

                MapXLib.CoordSys oCoordSys;
                oCoordSys = l_lyrUkgN18.CoordSys.Clone();
                lyrMyLayer = axMapUkg.Layers.CreateLayer("Cars", null, 1, 32, oCoordSys);
                lyrMyLayer.Selectable = true;
                //lyrMyLayer.AutoLabel = true;

                stNewSmb.SymbolType = MapXLib.SymbolTypeConstants.miSymbolTypeBitmap;
                stNewSmb.SymbolBitmapSize = 16;
                stNewSmb.SymbolBitmapTransparent = true;

                axMapUkg.CenterX = 82.63117242;
                axMapUkg.CenterY = 49.96756057;
                axMapUkg.Zoom = 30.63438257;
                axMapUkg.Focus();
            }
            else
            {
                if (!l_blFirstConnSql)
                {
                    l_blFirstConnSql = true;
                    FrmConnSQL.Instance = new FrmConnSQL();
                    FrmConnSQL.Instance.ShowDialog();
                }
            }
            rbnBtnConnSql.Enabled = true;

            c1SprTooltipBortUkg.IsBalloon = true;   //тип сообщения баллон
        }

        private void backgrndWrkTcp_DoWork(object sender, DoWorkEventArgs e)
        {
            ConnectToServer();
        }

        private void FrmMain_Load(object sender, EventArgs e)
        {
            axMapUkg.Dock = DockStyle.Fill;
            c1SizerFind.Dock = DockStyle.Fill;

            if (File.Exists(Application.StartupPath + "\\ClnGpsUkgIcs.ini"))
                ReadIniFile();
            else
            {
                WriteIniFile();

                //проверка соединения с SQL сервером
                FrmConnSQL.Instance = new FrmConnSQL();
                FrmConnSQL.Instance.ShowDialog();

                //проверка соединения с TCP сервером
                FrmConnTCP.Instance = new FrmConnTCP();
                FrmConnTCP.Instance.ShowDialog();
            }

            ribnLblConSql.ForeColorOuter = Color.Red;
            ribnLblConSql.Text = "SQL сервер?";

            ribnLblConTcp.ForeColorOuter = Color.Red;
            ribnLblConTcp.Text = "ТСР сервер?";

            m_backgrndWrkSql.RunWorkerAsync(); //соединение с SQL сервером
            // Включить обязательно
            m_backgrndWrkTcp.RunWorkerAsync(); //соединение с TCP сервером
            
        }

        private string SetLblObjMap(double dbMapX, double dbMapY)
        {
            //ptNewSmb.Set(81.03778, 50.55694);
            ptNewSmb.Set(dbMapX, dbMapY);
            bool bl = lyrMyLayer.OverrideStyle;
            stNewSmb.SymbolBitmapName = "BORT1-32.BMP";

            MapXLib.Feature ftrNewS = new MapXLib.Feature(); 
            MapXLib.Feature ftrMapS = new MapXLib.Feature();
            MapXLib.RowValues rvs1 = new MapXLib.RowValues();
            ftrNewS = axMapUkg.FeatureFactory.CreateSymbol(ptNewSmb, stNewSmb);
            ftrMapS = lyrMyLayer.AddFeature(ftrNewS, rvs1);
            
            return ftrMapS.FeatureID.ToString();
        }

        private void SetLineMap(double dbMapX, double dbMapY)
        {
            MapXLib.Feature ftrNewS = new MapXLib.Feature();
            MapXLib.Feature ftrMapS = new MapXLib.Feature();
            MapXLib.RowValues rvs1 = new MapXLib.RowValues();
            MapXLib.Style Style = new MapXLib.Style();
            MapXLib.Points ptNewLine = new MapXLib.Points();

            Style = axMapUkg.DefaultStyle.Clone();
            Style.LineStyle = (MapXLib.PenStyleConstants)59;
            Style.LineColor = 65535; 
            Style.LineWidth = 2;

            ptNewSmb.Set(dbMapX, dbMapY);
            ptNewLine.Add(ptNewSmb);
                        
            ptNewSmb.Set(82.62869, 49.95854);
            ptNewLine.Add(ptNewSmb);

            ftrNewS = axMapUkg.FeatureFactory.CreateLine(ptNewLine, Style);
            ftrMapS = lyrMyLayer.AddFeature(ftrNewS, rvs1);

        }

        private void LoadMapTab()
        {
            string strPath = "c:\\MapGpsUkg\\";
            string strPathFull = "";

            strPathFull = strPath + "UkgN16.tab";
            l_lyrUkgN16 = axMapUkg.Layers.Add(strPathFull, 1);
            //l_lyrUkgN16.LabelProperties.Style.TextFontHalo = true;
            //l_lyrUkgN16.AutoLabel = true;
            l_lyrUkgN16.ZoomLayer = true;
            l_lyrUkgN16.ZoomMin = 8.077;
            l_lyrUkgN16.ZoomMax = 31;

            strPathFull = strPath + "UkgN18.tab";
            l_lyrUkgN18 = axMapUkg.Layers.Add(strPathFull, 2);
            //l_lyrUkgN18.LabelProperties.Style.TextFontHalo = true;
            //l_lyrUkgN18.AutoLabel = true;
            l_lyrUkgN18.ZoomLayer = true;
            l_lyrUkgN18.ZoomMin = 0.0096; //2.019;
            l_lyrUkgN18.ZoomMax = 7.7;

            /*strPathFull = strPath + "UkgN19.tab";  //"City_all.tab vnas_rkm.tab";
            l_lyrUkgN19 = axMapUkg.Layers.Add(strPathFull, 3);
            //l_lyrUkgN19.LabelProperties.Style.TextFontHalo = true;
            //l_lyrUkgN19.AutoLabel = true;
            l_lyrUkgN19.ZoomLayer = true;
            l_lyrUkgN19.ZoomMin = 0.0096;
            l_lyrUkgN19.ZoomMax = 3.8;*/
        }

        private void c1BtnTest_Click(object sender, EventArgs e)
        {
            SetLineMap(82.62469, 49.94854);
            //string strRcvMsgAll = "!0343,00009C40,1B001BCF,1D8BCA50,31174EFA,12FBEB4D,000a010Fb51c000d000e000f000g000h000k000000m000000n000000z90";
            if (l_iTestRec == 1)
            {
                
                //strRcvMsgAll = "!0343,0001E1E4,06000ACF,1D8BFA66,31196618,12D48EDD,020a0120b0Dc000d000e000f000g000h000k000000m000000n000000z40";
            }
            l_iTestRec = 1;
            
            
            //ConvertRcvToMsg(strRcvMsgAll);

            
            
        }

        private string UnpackDateTime(int j, int iHourPlus)
        {
            int date;
            int d;
            int m;
            int y;
            int t;
            string strDate = "";
            //int j = 245592934;  //12.15:34   09/05/2009

            date = j / 86400;
            t = j - (date * 86400);
            date += 731000;

            y = (4 * date - 1) / 146097;
            d = (4 * date - 1 - 146097 * y) / 4;
            date = (4 * d + 3) / 1461;
            d = (4 * d + 7 - 1461 * date) / 4;
            m = (5 * d - 3) / 153;
            d = (5 * d + 2 - 153 * m) / 5;
            y = 100 * y + date;

            if (m < 10)
            {
                m += 3;
            }
            else
            {
                m -= 9;
                y++;
            }

            int sec = t % 60; t /= 60;
            int hour = t / 60;
            int minute = t % 60;

            hour = hour + iHourPlus;    //добовляет сдвиг поясного времени
            string strDatePlus = "";
            if (hour > 24)
            {
                hour = hour - 24;
                strDatePlus = FinfDatePlus(y, m, d);
                strDate = hour.ToString() + ":" + minute.ToString() + ":" + sec.ToString() + " " + strDatePlus;
            }
            else
                strDate = hour.ToString() + ":" + minute.ToString() + ":" + sec.ToString() + " " + d.ToString() + "." + m.ToString() + "." + y.ToString();

            return strDate;
        }

        private string FinfDatePlus(int Yr, int iMonth, int iDay)
        {
            double iYear = Yr;

            switch (iMonth)
            {
                case 2:
                    double dYvs = iYear / 4;
                    string[] strYear;
                    strYear = dYvs.ToString().Split(',');
                    int iVs = strYear.Length;   //1-високосный, 2-нет
                    if (iVs == 1)
                    {
                        if (iDay == 29)
                        {
                            iDay = 1;
                            iMonth++;
                        }
                        else
                            iDay++;
                    }
                    else
                    {
                        if (iDay == 28)
                        {
                            iDay = 1;
                            iMonth++;
                        }
                        else
                            iDay++;
                    }
                    break;
                case 1:
                case 3:
                case 5:
                case 7:
                case 8:
                case 10:
                    // месяцы 31 дней
                    if (iDay == 31)
                    {
                        iDay = 1;
                        iMonth++;
                    }
                    else
                        iDay++;
                    break;
                case 12:
                    if (iDay == 31)
                    {
                        iDay = 1;
                        iMonth = 1;
                        iYear++;
                    }
                    else
                        iDay++;
                    break;
                case 4:
                case 6:
                case 9:
                case 11:
                    // месяцы 30 дней
                    if (iDay == 30)
                    {
                        iDay = 1;
                        iMonth++;
                    }
                    else
                        iDay++;
                    break;
                default:
                    iYear = 0;
                    iMonth = 0;
                    iDay = 0;
                    break;
            }

            string strDate = iDay.ToString() + "." + iMonth.ToString() + "." + iYear.ToString();

            return strDate;
        }

        private string ConvLatLon(string strLat)
        {
            string strGr = strLat.Substring(0, 2);
            string strMn = strLat.Substring(2, 2);
            string strSec = strLat.Substring(4, strLat.Length - 4);
            strMn = (Convert.ToDouble(strMn + "," + strSec) / 60).ToString();
            strSec = strMn.Substring(strMn.IndexOf(',') + 1);
            if (strSec.Length > 6)
                strLat = strGr + "," + strSec.Substring(0, 6);
            else
                strLat = strGr + "," + strSec;

            return strLat;
        }

        private void ConvertRcvToMsg(string strRcvMsgAll)
        {
            string[] strRcvMsgBlok;
            char[] charSeparators = new char[] { '!' };

            strRcvMsgBlok = strRcvMsgAll.Split(charSeparators, StringSplitOptions.RemoveEmptyEntries);
            for (int i = 0; i < strRcvMsgBlok.Length; i++)
            {
                string[] strRcvMsg;
                strRcvMsg = strRcvMsgBlok[i].Split('#');

                for (int i1 = 0; i1 < strRcvMsg.Length; i1++)
                {
                    string[] strBfRcvMsgFild;
                    strBfRcvMsgFild = strRcvMsg[i1].Split(',');

                    if (i1 == 0 && strBfRcvMsgFild.Length == 7)
                    {
                        string[] strAddMsgBuf = new string[6];   //{ "", "", "", "", "", "" };
                        // формирует данные первого отчета в блоке сообщения
                        strAddMsgBuf[0] = Convert.ToInt32(strBfRcvMsgFild[0], 16).ToString();
                        // координаты в блоке сообщения
                        strAddMsgBuf[1] = ConvLatLon(Convert.ToInt32(strBfRcvMsgFild[3], 16).ToString());
                        strAddMsgBuf[2] = ConvLatLon(Convert.ToInt32(strBfRcvMsgFild[4], 16).ToString());
                        // дата и время
                        strAddMsgBuf[3] = UnpackDateTime(Convert.ToInt32(strBfRcvMsgFild[5], 16), 6);
                        // скорость
                        string strVsp = ((Convert.ToInt32(strBfRcvMsgFild[6].Substring(0, 3), 16)) * 1.853 / 10).ToString();
                        string[] strVbf;
                        strVbf = strVsp.Split(',');
                        if (strVbf.Length > 1)
                            strVsp = strVbf[0] + "," + strVbf[1].Substring(0, 1);
                        else
                            strVsp = strVbf[0];
                        strAddMsgBuf[4] = strVsp; ;
                        // курс
                        strAddMsgBuf[5] = ((Convert.ToInt32(strBfRcvMsgFild[6].Substring(strBfRcvMsgFild[6].IndexOf('b') + 1, 2), 16)) * 2).ToString();

                        AddBortToGrid(strAddMsgBuf);
                    }
                }
            }
        }

        private void AddBortToGrid(string[] strDataFildBuf)
        {
            if (bsFindBort.Count > 0)
            {
                //поиск
                int itemFound = bsFindBort.Find("BortTrekId", strDataFildBuf[0]);
                if (itemFound != -1)
                {
                    bsFindBort.Position = itemFound;
                    DataRow dr = dsFindBort.Tables["Bort"].Rows[bsFindBort.Position];
                    string strBortAlias = dr["BortAlias"].ToString();
                    string strBortName = dr["BortName"].ToString();
                    string strBortNote = dr["BortNote"].ToString();
                    string strBortPchName = dr["BortPchName"].ToString();

                    int iFindGrid = c1FlxGrdFind.FindRow(strDataFildBuf[0].ToString(), 1, l_iColTrekId_0, true, true, true);
                    string strBortInfo = "Гос.№: " + strBortName + " п\\часть: " + strBortPchName + "\r\n" +
                                            "Прим.: " + strBortNote;
                    string strBortInfoLabel = strBortName + "," + strBortPchName + "," + strDataFildBuf[4] + "," + strDataFildBuf[5];

                    int iRowAdd = 1;
                    if (iFindGrid == -1)
                    {
                        string strMapLabelId = SetLblObjMap(Convert.ToDouble(strDataFildBuf[2]), Convert.ToDouble(strDataFildBuf[1]));

                        iRowAdd = c1FlxGrdFind.Rows.Count;
                        //bool blShowLbl = false;
                        
                        object[] items = { strDataFildBuf[0], "", strBortAlias, strDataFildBuf[3], strMapLabelId, strDataFildBuf[1],
                                             strDataFildBuf[2], strBortInfoLabel };
                        c1FlxGrdFind.AddItem(items, iRowAdd, 0);
                        CellNote note = new CellNote(strBortInfo);
                        CellRange rg = c1FlxGrdFind.GetCellRange(iRowAdd, 3);
                        rg.UserData = note;
                        CellNoteManager mgr = new CellNoteManager(c1FlxGrdFind);
                        //добавляет Checked
                        CellRange crChk;
                        // Add checkboxes to the cell range.
                        crChk = c1FlxGrdFind.GetCellRange(iRowAdd, 1);    //c1FlxGrdFind.Rows.Count - 1, c1FlxGrdFind.Cols.Count - 1);
                        crChk.Checkbox = CheckEnum.Unchecked;

                    }
                    else
                    {
                        c1FlxGrdFind.SetData(iFindGrid, l_iColData_3, strDataFildBuf[3]);
                        c1FlxGrdFind.SetData(iFindGrid, l_iColInfo_7, strBortInfoLabel);

                        double dbLat = Convert.ToDouble(c1FlxGrdFind.GetDataDisplay(iFindGrid, l_iColLat_5));
                        double dbLon = Convert.ToDouble(c1FlxGrdFind.GetDataDisplay(iFindGrid, l_iColLon_6));
                        // слежение за объектом
                        string strBortAliasFl = strBortAlias + " - " + strBortName;
                        int iFndWindow = FindWindowA(null, strBortAliasFl);
                        //
                        if (dbLat != Convert.ToDouble(strDataFildBuf[1]) || dbLon != Convert.ToDouble(strDataFildBuf[2]))
                        {
                            c1FlxGrdFind.SetData(iFindGrid, l_iColLat_5, strDataFildBuf[1]);  //записывает новые координаты
                            c1FlxGrdFind.SetData(iFindGrid, l_iColLon_6, strDataFildBuf[2]);
                            //перемещает созданный point в новую точку координат
                            int iMapLabelId = Convert.ToInt32(c1FlxGrdFind.GetDataDisplay(iFindGrid, l_iColMapId_4));
                            MapXLib.Feature ftrMapSmb = lyrMyLayer.GetFeatureByID(iMapLabelId);
                            ftrMapSmb.Point.Set(Convert.ToDouble(strDataFildBuf[2]), Convert.ToDouble(strDataFildBuf[1]));
                            ftrMapSmb.Update();
                            
                            if (iFndWindow > 0)
                                PostMessageA(iFndWindow, ID.IDR_MOVBORTMAP, iFindGrid, 0); // объект перемещается
                        }
                        else
                        {
                            if (iFndWindow > 0)
                                PostMessageA(iFndWindow, ID.IDR_MOVBORTMAP, iFindGrid, 112233); // 112233 - признак что объект стоит на месте
                        }
                    }
    
                }
            }
        }
       
        private void LoadBortSqlData()
        {
            string strSqlTextBortFnd = "SELECT Bort.BortTrekId, Bort.BortAlias, Bort.BortName, Bort.BortNote, BortPch.BortPchName " +
                      "FROM Bort INNER JOIN BortPch ON Bort.BortPchId = BortPch.BortPchId WHERE (Bort.BortMon = 1) ORDER BY Bort.BortAlias";
            SqlDataAdapter daFindBort = new SqlDataAdapter(strSqlTextBortFnd, m_cnn);
            daFindBort.Fill(dsFindBort, "Bort");
            bsFindBort.DataSource = dsFindBort;
            bsFindBort.DataMember = "Bort";

            BindingSource bsFindBortGrd = new BindingSource(dsFindBort, "Bort");
            //DataSet dsFindBort = new DataSet();

            c1CmbBort.DataSource = bsFindBortGrd;
            c1CmbBort.ValueMember = "BortAlias";
            c1CmbBort.DisplayMember = "BortAlias";

            c1CmbBort.Columns.RemoveAt(0);
            c1CmbBort.Columns.RemoveAt(3);
            c1CmbBort.Columns.RemoveAt(2);
            c1CmbBort.Columns.RemoveAt(1);

            c1CmbBort.Columns[0].Caption = "Борт";
            c1CmbBort.Splits[0].DisplayColumns[0].Width = 30;
            c1CmbBort.Splits[0].DisplayColumns[0].HeadingStyle.HorizontalAlignment = C1.Win.C1List.AlignHorzEnum.Center;
         }

        private void RemoveItemBortFnd()
        {
            int iCnt = c1FlxGrdFind.Rows.Count - 1;
            for (int i = 0; i < iCnt; i = i + 1)
                c1FlxGrdFind.RemoveItem(1);
        }

        private void ConnectToServer()
        {
            try
            {
                // Create the socket instance
                m_clientSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

                // Cet the remote IP address
                IPAddress ip = IPAddress.Parse(m_strAdrIP);
                int iPortNo = System.Convert.ToInt16(m_strPortIP);
                // Create the end point 
                IPEndPoint ipEnd = new IPEndPoint(ip, iPortNo);
                // Connect to the remote host
                m_clientSocket.Connect(ipEnd);
                if (m_clientSocket.Connected)
                {
                    WriteIniFile();
                    AppendToUpdateControls(true);
                    //Wait for data asynchronously 
                    WaitForData();
                }
            }
            catch (SocketException se)
            {
                string str = "\nСоединение не установлено, сервер работает?\n\n" + se.Message;
                //MessageBox.Show(str);
                AppendToUpdateControls(false);
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
                MessageBox.Show(se.Message);
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
                if (szData == "\0")
                {
                    m_clientSocket.Close();
                    m_clientSocket = null;
                    AppendToUpdateControls(false);
                }
                else
                {
                    //AppendToGridRsvMsg(szData);
                    AppendToReciveData(szData);
                    WaitForData();
                }
            }
            catch (ObjectDisposedException)
            {
                System.Diagnostics.Debugger.Log(0, "1", "\nOnDataReceived: Socket has been closed\n");
            }
            catch (SocketException se)
            {
                //MessageBox.Show(se.Message);
                if (se.ErrorCode == 10054 || se.ErrorCode == 10053) // Error code for Connection reset by peer
                {
                    m_clientSocket.Close();
                    m_clientSocket = null;
                    AppendToUpdateControls(false);
                }
            }
        }

        private void AppendToReciveData(string msg)
        {
            if (InvokeRequired)
            {
                object[] pList = { msg };
                c1FlxGrdFind.BeginInvoke(new UpdateReciveDataCallback(OnUpdateReciveData), pList);
            }
            else
            {
                OnUpdateReciveData(msg);
            }
        }

        private void OnUpdateReciveData(string strMsg)
        {
            //try
            //{
                ConvertRcvToMsg(strMsg);
            /*}
            catch
            {
                Console.WriteLine("Caught exception #2.");
            }*/

        }

        private void AppendToUpdateControls(bool msg)
        {
            if (InvokeRequired)
            {
                object[] pList = { msg };
                c1StatusBarMain.BeginInvoke(new UpdateLblConnectCallback(OnUpdateLblConnect), pList);
            }
            else
            {
                OnUpdateLblConnect(msg);
            }
        }

        private void OnUpdateLblConnect(bool connected)
        {
            string connectStatus = connected ? "ТСР сервер!" : "ТСР сервер?";

            if (connected)
            {
                ribnLblConTcp.ForeColorOuter = Color.Blue;
            }
            else
            {
                conTimerTcp.Enabled = true;     // включает таймер
                ribnLblConTcp.ForeColorOuter = Color.Red;
            }
            ribnLblConTcp.Text = connectStatus;
        }

        private void OnTimerEventConnToTcp(object source, ElapsedEventArgs e)
        {
            conTimerTcp.Enabled = false;    // отключает таймер
            m_backgrndWrkTcp.RunWorkerAsync(); //соединение с TCP сервером
        }

        private void ReadIniFile()
        {
            IniFile ini = new IniFile(Application.StartupPath + "\\ClnGpsUkgIcs.ini");

            m_strPortIP = ini.ReadValue("IP", "Port", m_strPortIP);
            m_strAdrIP = ini.ReadValue("IP", "AdrIP", m_strAdrIP);
            m_strSqlName = ini.ReadValue("SQL", "Name", m_strSqlName);
            m_strSqlLog = ini.ReadValue("SQL", "Login", m_strSqlLog);
            m_strSqlPass = ini.ReadValue("SQL", "Pass", m_strSqlPass);
            m_strSqlBase = ini.ReadValue("SQL", "Base", m_strSqlBase);
        }

        private void WriteIniFile()
        {
            IniFile ini = new IniFile(Application.StartupPath + "\\ClnGpsUkgIcs.ini");

            ini.WriteValue("IP", "Port", m_strPortIP);
            ini.WriteValue("IP", "AdrIP", m_strAdrIP);
            ini.WriteValue("SQL", "Name", m_strSqlName);
            ini.WriteValue("SQL", "Login", m_strSqlLog);
            ini.WriteValue("SQL", "Pass", m_strSqlPass);
            ini.WriteValue("SQL", "Base", m_strSqlBase);
        }

        private void rbnBtnConnSql_Click(object sender, EventArgs e)
        {
            FrmConnSQL.Instance = new FrmConnSQL();
            FrmConnSQL.Instance.ShowDialog();
        }

        private void rbnBtnConnTcp_Click(object sender, EventArgs e)
        {
            //проверка соединения с TCP сервером
            FrmConnTCP.Instance = new FrmConnTCP();
            FrmConnTCP.Instance.ShowDialog();
        }

        private void c1DckTabPageMain_ClientSizeChanged(object sender, EventArgs e)
        {
            /*bool blFingVs = c1DckTabPageMain.Visible;
            if (blFingVs && m_blConSql)
            {
                bool redraw = c1FlxGrdFind.Redraw;
                c1FlxGrdFind.Redraw = false;
                try
                {
                    if (c1FlxGrdFind.Rows.Count > 1)
                        RemoveItemBortFnd();
                    LoadBortSqlData();
                }
                finally
                {
                    // Always restore painting.
                    c1FlxGrdFind.Redraw = redraw;
                }
            }*/
        }

        private void c1FlxGrdFind_CellButtonClick(object sender, RowColEventArgs e)
        {
            C1.Win.C1SuperTooltip.C1SuperTooltip SpTlShow = new C1.Win.C1SuperTooltip.C1SuperTooltip();

            int iMapLabelId = Convert.ToInt32(c1FlxGrdFind.GetDataDisplay(e.Row, l_iColMapId_4));
            double dbCorX = Convert.ToDouble(c1FlxGrdFind.GetDataDisplay(e.Row, l_iColLon_6));
            double dbCorY = Convert.ToDouble(c1FlxGrdFind.GetDataDisplay(e.Row, l_iColLat_5));
            float iScX = 0;
            float iScY = 0;
            
            axMapUkg.CenterX = dbCorX;
            axMapUkg.CenterY = dbCorY;
            axMapUkg.ConvertCoord(ref iScX, ref iScY, ref dbCorX, ref dbCorY, MapXLib.ConversionConstants.miMapToScreen);

            string[] strBortInfo = c1FlxGrdFind.GetDataDisplay(e.Row, l_iColInfo_7).Split(',');
            string strBortMsg = c1FlxGrdFind.GetDataDisplay(e.Row, l_iColAlias_2) + " (" + strBortInfo[0] + ")";
            SpTlShow.IsBalloon = true;
            SpTlShow.Show(strBortMsg, axMapUkg, Convert.ToInt32(iScX), Convert.ToInt32(iScY), 5000);
        }

        private void c1FlxGrdFind_CellChecked(object sender, RowColEventArgs e)
        {
            /*CellRange crChk;
            crChk = c1FlxGrdFind.GetCellRange(e.Row, 1);
            int iMapLabelId = Convert.ToInt32(c1FlxGrdFind.GetDataDisplay(e.Row, l_iColMapId_4));

            if (crChk.Checkbox == CheckEnum.Checked)
            {
                //запись и чтение в коллекции объектов
                C1.Win.C1SuperTooltip.C1SuperTooltip SpTlShow = new C1.Win.C1SuperTooltip.C1SuperTooltip();
                
                double dbCorX = Convert.ToDouble(c1FlxGrdFind.GetDataDisplay(e.Row, l_iColLon_6));
                double dbCorY = Convert.ToDouble(c1FlxGrdFind.GetDataDisplay(e.Row, l_iColLat_5));
                float iScX = 0;
                float iScY = 0;

                axMapUkg.ConvertCoord(ref iScX, ref iScY, ref dbCorX, ref dbCorY, MapXLib.ConversionConstants.miMapToScreen);
                string strBortAlias = c1FlxGrdFind.GetDataDisplay(e.Row, l_iColAlias_2);
                l_srMapLabel.Add(iMapLabelId, SpTlShow);
                SpTlShow.Show(strBortAlias, axMapUkg, Convert.ToInt32(iScX), Convert.ToInt32(iScY));
            }
            else
            {
                int iFindFt = l_srMapLabel.IndexOfKey(iMapLabelId);
                if (iFindFt != -1)
                {
                    C1.Win.C1SuperTooltip.C1SuperTooltip TlSh = l_srMapLabel.Values[iFindFt];
                    TlSh.Hide();
                    l_srMapLabel.RemoveAt(iFindFt); //удаляет по найденому индексу
                }
            }*/
        }

        private void axMapUkg_MouseMoveEvent(object sender, AxMapXLib.CMapXEvents_MouseMoveEvent e)
        {
            double dbCorX = 0;
            double dbCorY = 0;
            int iScX = Convert.ToInt32(e.x);
            int iScY = Convert.ToInt32(e.y);

            axMapUkg.ConvertCoord(ref e.x, ref e.y, ref dbCorX, ref dbCorY, MapXLib.ConversionConstants.miScreenToMap);

            MapXLib.Features FtrsUnderPt;
            MapXLib.Point pt = new MapXLib.Point();
            MapXLib.Feature ftrFind;

            pt.Set(dbCorX, dbCorY);
            FtrsUnderPt = lyrMyLayer.SearchAtPoint(pt);
            int iCntF = FtrsUnderPt.Count;
            if (iCntF > 0)
            {
                axMapUkg.CurrentTool = MapXLib.ToolConstants.miArrowTool;
                ftrFind = FtrsUnderPt._Item(1);
                int iFtFindId = ftrFind.FeatureID;
                int iFindRow = c1FlxGrdFind.FindRow(iFtFindId.ToString(), 1, l_iColMapId_4, true, true, true);

                if (iFindRow != -1)
                {
                    string strBort = c1FlxGrdFind.GetDataDisplay(iFindRow, l_iColAlias_2);
                    string strData = c1FlxGrdFind.GetDataDisplay(iFindRow, l_iColData_3);
                    string[] strBortInfo = c1FlxGrdFind.GetDataDisplay(iFindRow, l_iColInfo_7).Split(',');

                    string strInfoGps = "<b>Борт: " + strBort + "</b>, №: " + strBortInfo[0] + "<br>" +
                        " п\\часть: " + strBortInfo[1] + "<br>" +
                        "Скорость: " + strBortInfo[2] + ", Курс: " + strBortInfo[3] + "<br>" +
                        "<parm><hr noshade size=1 style='margin:2' color=Darker></parm>" + strData;
                    //string strInfoGps = "232АЦ";
                    c1SprTooltipBortUkg.Show(strInfoGps, axMapUkg, iScX, iScY);
                }
            }
            else
            {
                axMapUkg.CurrentTool = MapXLib.ToolConstants.miPanTool;
                c1SprTooltipBortUkg.Hide(axMapUkg);
            }
        }

        private void c1CmbBort_Close(object sender, EventArgs e)
        {
            int iFndRow = c1CmbBort.FindStringExact(c1CmbBort.Text, 0, 0);
            if (iFndRow != -1)
            {
                string strBornAlis = c1CmbBort.Columns[0].CellText(iFndRow);
                int iFindGrid = c1FlxGrdFind.FindRow(strBornAlis, 1, l_iColAlias_2, true, true, true);
                //c1FlxGrdFind.r
                if (iFindGrid != -1)
                {
                    c1FlxGrdFind.SelectionMode = SelectionModeEnum.RowRange;
                    c1FlxGrdFind.Select(iFindGrid, 1);
                    c1FlxGrdFind.Focus();
                    //c1FlxGrdFind.SelectionMode = SelectionModeEnum.Default;
                }
            }
        }

        private void c1CmbBort_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Return)
            {
                e.Handled = true;
            }
        }

        private void c1FlxGrdFind_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == System.Windows.Forms.MouseButtons.Right)
            {
                int iRowSel = c1FlxGrdFind.RowSel;
                int iColSel = c1FlxGrdFind.ColSel;
                if (iRowSel > 0 && iColSel > 1)
                {
                    if (iColSel == 2)
                    {
                        C1.Win.C1Command.C1CommandLinks cmnLink = c1CntMnuGrdBort.CommandLinks;
                        string strBortAlias = c1FlxGrdFind.GetDataDisplay(iRowSel, l_iColAlias_2);
                        cmnLink[0].Text = "Следить за " + strBortAlias;
                        Point pt = new Point();
                        pt.X = e.X;
                        pt.Y = e.Y;
                        c1CntMnuGrdBort.ShowContextMenu(c1FlxGrdFind, pt);
                    }
                }
            }
        }

        private void c1FlxGrdFind_MouseClick(object sender, MouseEventArgs e)
        {
            //int iRowSel = c1FlxGrdFind.RowSel;
            //c1FlxGrdFind.Rows[iRowSel].Selected = true;
        }

        private void c1CmndBortTrace_Click(object sender, C1.Win.C1Command.ClickEventArgs e)
        {
            int iRowSel = c1FlxGrdFind.RowSel;
            string[] strBortInfo = c1FlxGrdFind.GetDataDisplay(iRowSel, l_iColInfo_7).Split(',');
            string strBortAlias = c1FlxGrdFind.GetDataDisplay(iRowSel, l_iColAlias_2) + " - " + strBortInfo[0];
            int iFndWindow = FindWindowA(null, strBortAlias);
            if (iFndWindow == 0)
            {
                FrmBortFollow.Instance = new FrmBortFollow();
                FrmBortFollow.Instance.Text = strBortAlias;

                FrmBortFollow.Instance.m_dbLatY = Convert.ToDouble(c1FlxGrdFind.GetDataDisplay(iRowSel, l_iColLat_5));
                FrmBortFollow.Instance.m_dbLonX = Convert.ToDouble(c1FlxGrdFind.GetDataDisplay(iRowSel, l_iColLon_6));
                FrmBortFollow.Instance.m_iBortRowFollow = iRowSel;

                FrmBortFollow.Instance.Show();
            }
            else
                PostMessageA(iFndWindow, ID.IDR_SHOWWINDOW, 0, 0);  //активирует окно
        }

    }
}
