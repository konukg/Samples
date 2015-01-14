using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace GpsUkgIcs
{
    public partial class FrmBortFollow : Form
    {
        static public FrmBortFollow Instance;

        MapXLib.Layer l_lyrUkgN16, l_lyrUkgN18; //, l_lyrUkgN19;
        MapXLib.Layer lyrMyLayer;
        MapXLib.Style stNewSmb = new MapXLib.Style();
        MapXLib.Point ptNewSmb = new MapXLib.Point();

        public double m_dbLatY;
        public double m_dbLonX;

        int l_iMapLabelId;  //значение FeatureID

        int l_iColData_3 = 3;    // дата и время
        int l_iColLat_5 = 5;    // координаты Lat
        int l_iColLon_6 = 6;    // координаты Lon
        int l_iColInfo_7 = 7;    // информация - гос.№, п/часть и т.п
        
        string[] l_strBortInfo; // буфер с информацией о борте
        public int m_iBortRowFollow;    // RowSel в таблице

        public FrmBortFollow()
        {
            InitializeComponent();
        }

        private void FrmBortFollow_Load(object sender, EventArgs e)
        {
            c1SizerFollpw.Dock = DockStyle.Fill;
            axMapFollow.Dock = DockStyle.Fill;

            LoadMapTab(); //загрузка слоев карты 

            l_strBortInfo = FrmMain.Instance.c1FlxGrdFind.GetDataDisplay(m_iBortRowFollow, l_iColInfo_7).Split(',');
            string strData = FrmMain.Instance.c1FlxGrdFind.GetDataDisplay(m_iBortRowFollow, l_iColData_3);

            c1SuperLblInfo.Text = "Скорость:<b>" + l_strBortInfo[2] + "</b>, Курс:<b>" + l_strBortInfo[3] + "</b>, Время:<b>" + strData + "</b>";
                //"<p style=background-color:#dddddd>" + strData + "</p>";
        }

        private void LoadMapTab()
        {
            string strPath = "c:\\MapGpsUkg\\";
            string strPathFull = "";

            strPathFull = strPath + "UkgN16.tab";
            l_lyrUkgN16 = axMapFollow.Layers.Add(strPathFull, 1);
            //l_lyrUkgN16.LabelProperties.Style.TextFontHalo = true;
            //l_lyrUkgN16.AutoLabel = true;
            l_lyrUkgN16.ZoomLayer = true;
            l_lyrUkgN16.ZoomMin = 8.077;
            l_lyrUkgN16.ZoomMax = 31;

            strPathFull = strPath + "UkgN18.tab";
            l_lyrUkgN18 = axMapFollow.Layers.Add(strPathFull, 2);
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

            MapXLib.CoordSys oCoordSys;
            oCoordSys = l_lyrUkgN18.CoordSys.Clone();
            lyrMyLayer = axMapFollow.Layers.CreateLayer("Cars", null, 1, 32, oCoordSys);
            lyrMyLayer.Selectable = true;
            //lyrMyLayer.AutoLabel = true;

            stNewSmb.SymbolType = MapXLib.SymbolTypeConstants.miSymbolTypeBitmap;
            stNewSmb.SymbolBitmapSize = 16;
            stNewSmb.SymbolBitmapTransparent = true;

            axMapFollow.CenterX = m_dbLonX;
            axMapFollow.CenterY = m_dbLatY;
            axMapFollow.Zoom = 1;
            axMapFollow.Focus();

            ptNewSmb.Set(m_dbLonX, m_dbLatY);
            bool bl = lyrMyLayer.OverrideStyle;
            stNewSmb.SymbolBitmapName = "BORT1-32.BMP";

            MapXLib.Feature ftrNewS = new MapXLib.Feature();
            MapXLib.Feature ftrMapS = new MapXLib.Feature();
            MapXLib.RowValues rvs = new MapXLib.RowValues();
            ftrNewS = axMapFollow.FeatureFactory.CreateSymbol(ptNewSmb, stNewSmb);
            ftrMapS = lyrMyLayer.AddFeature(ftrNewS, rvs);

            l_iMapLabelId = ftrMapS.FeatureID;
        }

        protected override void WndProc(ref Message msg)
        {
            switch (msg.Msg)
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
                case ID.IDR_MOVBORTMAP:
                    int iRowsId = (int)msg.WParam;

                    l_strBortInfo = FrmMain.Instance.c1FlxGrdFind.GetDataDisplay(iRowsId, l_iColInfo_7).Split(',');
                    string strData = FrmMain.Instance.c1FlxGrdFind.GetDataDisplay(iRowsId, l_iColData_3);
                    c1SuperLblInfo.Text = "Скорость:<b>" + l_strBortInfo[2] + "</b>, Курс:<b>" + l_strBortInfo[3] + "</b>, Время:<b>" + strData + "</b>";

                    if ((int)msg.LParam != 112233)
                        SetObjToMap_Follow(iRowsId);
                    break;
            }
            // Call the base WndProc method
            // to process any messages not handled.
            base.WndProc(ref msg);
        }

        private void SetObjToMap_Follow(int strRowId)
        {
            //l_strBortInfo = FrmMain.Instance.c1FlxGrdFind.GetDataDisplay(strRowId, l_iColInfo_7).Split(',');
            //string strData = FrmMain.Instance.c1FlxGrdFind.GetDataDisplay(strRowId, l_iColData_3);
            //c1SuperLblInfo.Text = "Скорость:<b>" + l_strBortInfo[2] + "</b>, Курс:<b>" + l_strBortInfo[3] + "</b>, Время:<b>" + strData + "</b>";
            
            //перемещает созданный point в новую точку координат
            MapXLib.Feature ftrMapSmb = lyrMyLayer.GetFeatureByID(l_iMapLabelId);
            double dbLatY = Convert.ToDouble(FrmMain.Instance.c1FlxGrdFind.GetDataDisplay(strRowId, l_iColLat_5));
            double dbLonX = Convert.ToDouble(FrmMain.Instance.c1FlxGrdFind.GetDataDisplay(strRowId, l_iColLon_6));
            ftrMapSmb.Point.Set(dbLonX, dbLatY);
            ftrMapSmb.Update();

            axMapFollow.CenterX = dbLonX;
            axMapFollow.CenterY = dbLatY;
        }
    }
}
