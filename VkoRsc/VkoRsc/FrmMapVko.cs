using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using System.Data.SqlClient;

namespace VkoRsc
{
    public partial class FrmMapVko : Form
    {
        static public FrmMapVko Instance;

        MapXLib.Layer l_lyrGorod, l_lyrPoselok, l_lyrCity_all, l_lyrLep, l_lyrAuto, l_lyrAuto1, l_lyrAuto2, l_lyrVko_rrl, l_lyrVko_rrl_ge,
            l_lyrGranica, l_lyrVrek1, l_lyrVrekoz, l_lyrKr_gor, l_lyrPiki, l_lyrRelef_z, l_lyrVko_les, l_lyrRegion, l_lyrZal_vko,
            l_lyrSdjav, l_lyrAvia_ptr;
        MapXLib.Selection l_lyrSelCity_all, l_lyrSelSdjav, l_lyrSelAvia_ptr, lyrSelMyLayer;
        MapXLib.Layer lyrMyLayer;
       
        public const int PolyRulerToolId = 105;

        MapXLib.Style stNewSmb = new MapXLib.Style();
        MapXLib.Point ptNewSmb = new MapXLib.Point();
        MapXLib.RowValues rvs = new MapXLib.RowValues();

        MapXLib.Feature ftrNewSmbHm = new MapXLib.Feature(); // для символа отображения дома
        MapXLib.Feature ftrMapSmbHm = new MapXLib.Feature();
        int l_iSmbFtrId = 0; // хранит последнее значение .FeatureID

        int l_iPointX;  //
        int l_iPointY;

        DataSet dsCityName = new DataSet();
        BindingSource bsCityName = new BindingSource();

        int l_intCityName = 0;  //позиция

        public int m_iNmb = 0;

        public FrmMapVko()
        {
            InitializeComponent();
        }

        private void FrmMapVko_Load(object sender, EventArgs e)
        {
            c1SizerMapVko.Dock = DockStyle.Fill;
            axMapVko.Dock = DockStyle.Fill;

            LoadMapTab(); //загрузка слоев карты           

            axMapVko.CenterX = 81.63828964;
            axMapVko.CenterY = 48.62319396;
            axMapVko.Zoom = 1050; //934.7653867;

            axMapVko.CreateCustomTool(PolyRulerToolId, MapXLib.ToolTypeConstants.miToolTypePoly, MapXLib.CursorConstants.miLabelCursor);

            MapXLib.CoordSys oCoordSys;
            oCoordSys = l_lyrCity_all.CoordSys.Clone();
            lyrMyLayer = axMapVko.Layers.CreateLayer("Cars", null, 1, 32, oCoordSys);
            lyrMyLayer.Selectable = true;
            lyrMyLayer.AutoLabel = true;
          
            MapXLib.Dataset ds; //= new MapXLib.Dataset();
            MapXLib.Fields flds = new MapXLib.Fields();
            //MapXLib.Features ftrs; //= new MapXLib.Features();

            ds = axMapVko.DataSets.Add(MapXLib.DatasetTypeConstants.miDataSetLayer, lyrMyLayer);
            flds = ds.Fields;
            //ds.AddField

            /*MapXLib.LayerInfo lyrInfo = new MapXLib.LayerInfo();
            lyrInfo.Type = MapXLib.LayerInfoTypeConstants.miLayerInfoTypeTemp;
            lyrInfo.AddParameter("Name", "Temporary Layer");
            lyrInfo.AddParameter("Fields", flds);
            //lyrInfo.AddParameter("Features", ftrs);
            //lyrInfo.AddParameter("OverwriteFile", "1");
            lyrMyLayer = axMapVko.Layers.Add(lyrInfo, 1);
            rvs = ds.RowValues[0];
            //lyrMyLayer.Editable = true;

            MapXLib.LayerInfo lyrInfo = new MapXLib.LayerInfo();
            lyrInfo.Type = MapXLib.LayerInfoTypeConstants.miLayerInfoTypeNewTable;
            lyrInfo.AddParameter("FileSpec", "c:\\MapVko\\custtab.tab");
            lyrInfo.AddParameter("Name", "Temporary Layer");
            lyrInfo.AddParameter("Fields", flds);
            //lyrInfo.AddParameter("Features", ftrs);
            //lyrInfo.AddParameter("OverwriteFile", "1");
            lyrMyLayer = axMapVko.Layers.Add(lyrInfo, 1);
            rvs = ds.RowValues[0];*/
            
            stNewSmb.SymbolType = MapXLib.SymbolTypeConstants.miSymbolTypeBitmap;
            stNewSmb.SymbolBitmapSize = 24;
            stNewSmb.SymbolBitmapTransparent = true;
            
            l_lyrSelCity_all = l_lyrCity_all.Selection;  //устанавливает select для населенных пунктов
            l_lyrSelSdjav = l_lyrSdjav.Selection;  //устанавливает select для СДЯВ
            l_lyrSelAvia_ptr = l_lyrAvia_ptr.Selection;  //устанавливает select для Авиа лес
            lyrSelMyLayer = lyrMyLayer.Selection;  //устанавливает select

            //загружает список населенных пунктов
            string strSqlTextCityNameMap = "SELECT CityName, CityID, N3, People FROM CityVko ORDER BY CityName";
            SqlDataAdapter daCityName = new SqlDataAdapter(strSqlTextCityNameMap, FrmMain.Instance.m_cnn);
            daCityName.Fill(dsCityName, "CityVko");
            bsCityName.DataSource = dsCityName;
            bsCityName.DataMember = "CityVko";

            c1CmbFindCity.DataSource = bsCityName;
            c1CmbFindCity.ValueMember = "CityName";
            c1CmbFindCity.DisplayMember = "CityName";

            //c1CmbFindCity.Columns.RemoveAt(1);
            c1CmbFindCity.Splits[0].DisplayColumns[1].Visible = false;
            c1CmbFindCity.Splits[0].DisplayColumns[2].Visible = false;
            c1CmbFindCity.Splits[0].DisplayColumns[3].Visible = false;
            c1CmbFindCity.Columns[0].Caption = "Название";

            //c1CmbFindCity.DropDownWidth = 440;
        }

        private void LoadMapTab()
        {
            string strPath = "c:\\MapVko\\";
            string strPathFull = "";

            strPathFull = strPath + "vko112_g.tab";
            l_lyrGorod = axMapVko.Layers.Add(strPathFull, 1);
            l_lyrGorod.LabelProperties.Style.TextFontHalo = true;
            l_lyrGorod.AutoLabel = true;
            l_lyrGorod.ZoomLayer = true;
            l_lyrGorod.ZoomMin = 0;
            l_lyrGorod.ZoomMax = 1600;

            strPathFull = strPath + "vko112_p.tab";
            l_lyrPoselok = axMapVko.Layers.Add(strPathFull, 2);
            l_lyrPoselok.LabelProperties.Style.TextFontHalo = true;
            l_lyrPoselok.AutoLabel = true;
            l_lyrPoselok.ZoomLayer = true;
            l_lyrPoselok.ZoomMin = 0;
            l_lyrPoselok.ZoomMax = 800;

            strPathFull = strPath + "City_all.tab";  //"City_all.tab vnas_rkm.tab";
            l_lyrCity_all = axMapVko.Layers.Add(strPathFull, 3);
            l_lyrCity_all.LabelProperties.Style.TextFontHalo = true;
            l_lyrCity_all.AutoLabel = true;
            l_lyrCity_all.ZoomLayer = true;
            l_lyrCity_all.ZoomMin = 0;
            l_lyrCity_all.ZoomMax = 650;

            strPathFull = strPath + "lep.tab";
            l_lyrLep = axMapVko.Layers.Add(strPathFull, 4);
            l_lyrLep.LabelProperties.Style.TextFontHalo = true;
            l_lyrLep.ZoomLayer = true;
            l_lyrLep.ZoomMin = 0;
            l_lyrLep.ZoomMax = 650;

            strPathFull = strPath + "auto_.tab";
            l_lyrAuto = axMapVko.Layers.Add(strPathFull, 5);
            l_lyrAuto.LabelProperties.Style.TextFontHalo = true;
            l_lyrAuto.ZoomLayer = true;
            l_lyrAuto.ZoomMin = 450;
            l_lyrAuto.ZoomMax = 10000;

            strPathFull = strPath + "auto_1.tab";
            l_lyrAuto1 = axMapVko.Layers.Add(strPathFull, 6);
            l_lyrAuto1.LabelProperties.Style.TextFontHalo = true;
            l_lyrAuto1.ZoomLayer = true;
            l_lyrAuto1.ZoomMin = 0;
            l_lyrAuto1.ZoomMax = 700;

            strPathFull = strPath + "auto_2.tab";
            l_lyrAuto2 = axMapVko.Layers.Add(strPathFull, 7);
            l_lyrAuto2.LabelProperties.Style.TextFontHalo = true;
            l_lyrAuto2.ZoomLayer = true;
            l_lyrAuto2.ZoomMin = 0;
            l_lyrAuto2.ZoomMax = 700;

            strPathFull = strPath + "vko_rrl.tab";
            l_lyrVko_rrl = axMapVko.Layers.Add(strPathFull, 8);
            l_lyrVko_rrl.LabelProperties.Style.TextFontHalo = true;
            l_lyrVko_rrl.ZoomLayer = true;
            l_lyrVko_rrl.ZoomMin = 0;
            l_lyrVko_rrl.ZoomMax = 700;

            strPathFull = strPath + "vko_rrl_ge.tab";
            l_lyrVko_rrl_ge = axMapVko.Layers.Add(strPathFull, 9);
            l_lyrVko_rrl_ge.LabelProperties.Style.TextFontHalo = true;
            l_lyrVko_rrl_ge.ZoomLayer = true;
            l_lyrVko_rrl_ge.ZoomMin = 450;
            l_lyrVko_rrl_ge.ZoomMax = 10000;

            strPathFull = strPath + "granica_gos.tab";
            l_lyrGranica = axMapVko.Layers.Add(strPathFull, 10);
            l_lyrGranica.LabelProperties.Style.TextFontHalo = true;
            l_lyrGranica.ZoomLayer = true;
            l_lyrGranica.ZoomMin = 0;
            l_lyrGranica.ZoomMax = 10000;

            strPathFull = strPath + "vrek1.tab";
            l_lyrVrek1 = axMapVko.Layers.Add(strPathFull, 11);
            l_lyrVrek1.LabelProperties.Style.TextFontHalo = true;
            l_lyrVrek1.ZoomLayer = true;
            l_lyrVrek1.ZoomMin = 150;
            l_lyrVrek1.ZoomMax = 10000;

            strPathFull = strPath + "vrekoz.tab";
            l_lyrVrekoz = axMapVko.Layers.Add(strPathFull, 12);
            l_lyrVrekoz.LabelProperties.Style.TextFontHalo = true;
            l_lyrVrekoz.ZoomLayer = true;
            l_lyrVrekoz.ZoomMin = 0;
            l_lyrVrekoz.ZoomMax = 250;

            strPathFull = strPath + "kr_gor.tab";
            l_lyrKr_gor = axMapVko.Layers.Add(strPathFull, 13);
            l_lyrKr_gor.LabelProperties.Style.TextFontHalo = true;
            l_lyrKr_gor.ZoomLayer = true;
            l_lyrKr_gor.ZoomMin = 0;
            l_lyrKr_gor.ZoomMax = 750;

            strPathFull = strPath + "piki.tab";
            l_lyrPiki = axMapVko.Layers.Add(strPathFull, 14);
            l_lyrPiki.LabelProperties.Style.TextFontHalo = true;
            l_lyrPiki.AutoLabel = true;
            l_lyrPiki.ZoomLayer = true;
            l_lyrPiki.ZoomMin = 0;
            l_lyrPiki.ZoomMax = 250;

            strPathFull = strPath + "relef_z.tab";
            l_lyrRelef_z = axMapVko.Layers.Add(strPathFull, 15);
            l_lyrRelef_z.LabelProperties.Style.TextFontHalo = true;
            l_lyrRelef_z.AutoLabel = true;
            l_lyrRelef_z.ZoomLayer = true;
            l_lyrRelef_z.ZoomMin = 0;
            l_lyrRelef_z.ZoomMax = 250;

            strPathFull = strPath + "vko_les.tab";
            l_lyrVko_les = axMapVko.Layers.Add(strPathFull, 16);
            l_lyrVko_les.LabelProperties.Style.TextFontHalo = true;
            l_lyrVko_les.ZoomLayer = true;
            l_lyrVko_les.ZoomMin = 0;
            l_lyrVko_les.ZoomMax = 400;

            strPathFull = strPath + "region.tab";
            l_lyrRegion = axMapVko.Layers.Add(strPathFull, 17);
            l_lyrRegion.LabelProperties.Style.TextFontHalo = true;
            l_lyrRegion.ZoomLayer = true;
            l_lyrRegion.ZoomMin = 250;
            l_lyrRegion.ZoomMax = 10000;

            strPathFull = strPath + "zal_vko.tab";
            l_lyrZal_vko = axMapVko.Layers.Add(strPathFull, 18);
            l_lyrZal_vko.LabelProperties.Style.TextFontHalo = true;
            //l_lyrZal_vko.ZoomLayer = true;
            //l_lyrZal_vko.ZoomMin = 250;
            //l_lyrZal_vko.ZoomMax = 10000;

            strPathFull = strPath + "sdjav.tab";
            l_lyrSdjav = axMapVko.Layers.Add(strPathFull, 1);
            l_lyrSdjav.LabelProperties.Style.TextFontHalo = true;
            l_lyrSdjav.AutoLabel = true;
            l_lyrSdjav.ZoomLayer = true;
            l_lyrSdjav.ZoomMin = 0;
            l_lyrSdjav.ZoomMax = 1600;
            l_lyrSdjav.Visible = false;

            strPathFull = strPath + "avia_ptr.tab";
            l_lyrAvia_ptr = axMapVko.Layers.Add(strPathFull, 1);
            l_lyrAvia_ptr.LabelProperties.Style.TextFontHalo = true;
            l_lyrAvia_ptr.AutoLabel = true;
            l_lyrAvia_ptr.ZoomLayer = true;
            l_lyrAvia_ptr.ZoomMin = 0;
            l_lyrAvia_ptr.ZoomMax = 1600;
            l_lyrAvia_ptr.Visible = false;
        }

        public bool m_FindObjMap(MapXLib.Layer lyrFnd, string strHome, double iZoom, int iSetSmb)
        {
            MapXLib.FindFeature fndFeat;

            axMapVko.SelectionStyle.RegionColor = Convert.ToUInt32(MapXLib.ColorConstants.miColorBlack);

            //string strHome = "622"; //587 - Пролетарская
            //fndFeat = lyrFnd.Find.Search(strHome, null);

            MapXLib.Dataset ds;

            ds = axMapVko.DataSets.Add(MapXLib.DatasetTypeConstants.miDataSetLayer, lyrFnd);
            lyrFnd.Find.FindDataset = ds;
            lyrFnd.Find.FindField = ds.Fields["Cityid"];
            fndFeat = lyrFnd.Find.Search(strHome, null);

            if (fndFeat.FindRC % 10 == 1)
            {
                //lyrFnd.Selection.SelectByPoint(fndFeat.CenterX, fndFeat.CenterY, MapXLib.SelectionTypeConstants.miSelectionNew);
                //lyrFnd.Selection.SelectByID(fndFeat.FeatureID, MapXLib.SelectionTypeConstants.miSelectionNew);
                //lyrFnd.Selection.SelectByRegion(lyrFnd, fndFeat.FeatureID, MapXLib.SelectionTypeConstants.miSelectionNew);
                //m_lyrWater.Selection.SelectByRadius(fndFeat.CenterX, fndFeat.CenterY, 0.5, MapXLib.SelectionTypeConstants.miSelectionNew);
                try
                {

                }
                catch (ArgumentException)
                {
                    Console.WriteLine("An element with Key = \"txt\" already exists.");
                }

                axMapVko.CenterX = fndFeat.CenterX;
                axMapVko.CenterY = fndFeat.CenterY;
                axMapVko.Zoom = iZoom;

                if (iSetSmb == 1)
                {
                    if (l_iSmbFtrId > 0)
                        lyrMyLayer.DeleteFeature(ftrMapSmbHm);

                    ptNewSmb.Set(fndFeat.CenterX, fndFeat.CenterY);
                    bool bl = lyrMyLayer.OverrideStyle;
                    //lyrMyLayer.LabelProperties.Position = MapXLib.PositionConstants.miPositionBC;
                    stNewSmb.SymbolBitmapName = "PIN2-32.BMP";
                    ftrNewSmbHm = axMapVko.FeatureFactory.CreateSymbol(ptNewSmb, stNewSmb);
                    ftrMapSmbHm = lyrMyLayer.AddFeature(ftrNewSmbHm, rvs);
                    l_iSmbFtrId = ftrMapSmbHm.FeatureID;
                    //lyrMyLayer.Refresh();
                    
                    //ObjFindSelectIhfo(fndFeat.CenterX, fndFeat.CenterY);
                }

                return true;
            }
            else
            {
                MessageBox.Show("Объект на карте не найден!", "Поиск объекта",
                    MessageBoxButtons.OK, MessageBoxIcon.Stop);
                //Close();
                return false;
            }
        }

        private void ObjFindSelectIhfo(double x1, double y1)
        {
            MapXLib.SearchResultTypeConstants SrHm = MapXLib.SearchResultTypeConstants.miSearchResultAll;
            
            l_lyrSelCity_all.SelectByPoint(x1, y1, MapXLib.SelectionTypeConstants.miSelectionNew, SrHm);
            l_lyrSelSdjav.SelectByPoint(x1, y1, MapXLib.SelectionTypeConstants.miSelectionNew, SrHm);
            l_lyrSelAvia_ptr.SelectByPoint(x1, y1, MapXLib.SelectionTypeConstants.miSelectionNew, SrHm);
            lyrSelMyLayer.SelectByPoint(x1, y1, MapXLib.SelectionTypeConstants.miSelectionNew, SrHm);
            //lyrSelMyLayer.SelectByID(l_iSmbFtrId, MapXLib.SelectionTypeConstants.miSelectionNew);
            
            //string strCityId = l_lyrSelCity_all._Item(1).KeyValue;

            //int rtr = lyrSelMyLayer.Count;
            //string strId = lyrSelMyLayer._Item(1).KeyValue;
            
            MapXLib.Features FtrsUnderPt;
            MapXLib.Point pt = new MapXLib.Point();
            MapXLib.Feature ftr;

            pt.Set(x1, y1);
            FtrsUnderPt = lyrMyLayer.SearchAtPoint(pt);
            int iCntF = FtrsUnderPt.Count;
            if (iCntF > 0)
            {
                ftr = FtrsUnderPt._Item(1);
                int iFt = ftr.FeatureID;
                if (iFt == 1)
                {
                    string strInfoGps = "Опасность возникновения пожара!.<br>" +
                        "с.Жерновка Бородулихинский район.<br>" +
                        "растояние - 6,598 км, азимут - 311,517.<br>" +
                        "<parm><hr noshade size=1 style='margin:2' color=Darker></parm>" +
                        "Система спутникового мониторинга.";
                    c1SprTooltipMapVko.Show(strInfoGps, axMapVko, l_iPointX, l_iPointY);
                }
                if (iFt == 2)
                {
                    C1.Win.C1SuperTooltip.C1SuperTooltip TlSh = new C1.Win.C1SuperTooltip.C1SuperTooltip();
                    string strInfoGps = "Опасность возникновения пожара!.<br>" +
                        "с.Жерновка222 Бородулихинский район.<br>" +
                        "растояние - 6,598 км, азимут - 311,517.<br>" +
                        "<parm><hr noshade size=1 style='margin:2' color=Darker></parm>" +
                        "Система спутникового мониторинга.";
                    TlSh.Show(strInfoGps, axMapVko, l_iPointX, l_iPointY, 3000);
                }
            }
            ptNewSmb.Set(x1, y1);
            
            /*ptNewSmb.Set(81.03778, 50.55694);
            MapXLib.Features ftr; //= new MapXLib.Features();
            ftr = lyrMyLayer.SearchAtPoint(ptNewSmb);
            int iSel = ftr.Count;
            if (iSel > 0)
            {
                string strInfoGps = "Опасность возникновения пожара!.<br>" +
                    "с.Жерновка Бородулихинский район.<br>" +
                    "растояние - 6,598 км, азимут - 311,517.<br>" +
                    "<parm><hr noshade size=1 style='margin:2' color=Darker></parm>" +
                    "Система спутникового мониторинга.";
                c1SprTooltipMapVko.Show(strInfoGps, axMapVko, l_iPointX, l_iPointY);  
            }

            if (l_lyrSelCity_all.Count > 0)
            {
                //ifDataRow dr = dsCityName.Tables["CityVko"].Rows[bsCityName.Position];
                string strCityId = l_lyrSelCity_all._Item(1).KeyValue;
                DataView dv = new DataView(dsCityName.Tables["CityVko"], "CityID = '" + strCityId + "'",
                    "CityName", DataViewRowState.CurrentRows);
                string strCityName = dv[0]["CityName"].ToString();
                string strCityN3 = dv[0]["N3"].ToString();
                string strCityPeople = dv[0]["People"].ToString();
                string strInfoCity = strCityName + "<br>" +
                    "Население - " + strCityPeople +"<br>" +
                    "<parm><hr noshade size=1 style='margin:2' color=Darker></parm>" +
                    "Район - " + strCityN3;
                c1SprTooltipMapVko.Show(strInfoCity, axMapVko, l_iPointX, l_iPointY);  
            }*/
        }

        
        public void m_SetObjMap()
        {
            ptNewSmb.Set(81.03778, 50.55694);
            bool bl = lyrMyLayer.OverrideStyle;
            stNewSmb.SymbolBitmapName = "PIN2-32.BMP";

            ftrNewSmbHm = axMapVko.FeatureFactory.CreateSymbol(ptNewSmb, stNewSmb); //axMapVko.DefaultStyle);       //axMapVko.FeatureFactory.CreateSymbol(ptNewSmb, stNewSmb);
            ftrMapSmbHm = lyrMyLayer.AddFeature(ftrNewSmbHm, rvs);
            l_iSmbFtrId = ftrMapSmbHm.FeatureID;

            ptNewSmb.Set(81.54778, 50.55694);
            MapXLib.Feature ftrNewS = new MapXLib.Feature(); // для символа отображения дома
            MapXLib.Feature ftrMapS = new MapXLib.Feature();
            MapXLib.RowValues rvs1 = new MapXLib.RowValues();
            ftrNewS = axMapVko.FeatureFactory.CreateSymbol(ptNewSmb, stNewSmb);
            ftrMapS = lyrMyLayer.AddFeature(ftrNewS, rvs1);
        }

        private void c1CmndLayer_Click(object sender, C1.Win.C1Command.ClickEventArgs e)
        {
            //m_SetObjMap();
            int iCnt = FrmMain.Instance.srFrmMap.Count;
            FrmMapVko frmMaps;
            for (int i = 0; i < iCnt; i++)
            {
                frmMaps = FrmMain.Instance.srFrmMap.Values[i];
                frmMaps.m_SetObjMap();
            }

            /*ptNewSmb.Set(81.03778, 50.55694);
            bool bl = lyrMyLayer.OverrideStyle;
            stNewSmb.SymbolBitmapName = "PIN2-32.BMP";
                        
            ftrNewSmbHm = axMapVko.FeatureFactory.CreateSymbol(ptNewSmb, stNewSmb); //axMapVko.DefaultStyle);       //axMapVko.FeatureFactory.CreateSymbol(ptNewSmb, stNewSmb);
            ftrMapSmbHm = lyrMyLayer.AddFeature(ftrNewSmbHm, rvs);
            l_iSmbFtrId = ftrMapSmbHm.FeatureID;

            ptNewSmb.Set(81.54778, 50.55694);
            MapXLib.Feature ftrNewS = new MapXLib.Feature(); // для символа отображения дома
            MapXLib.Feature ftrMapS = new MapXLib.Feature();
            MapXLib.RowValues rvs1 = new MapXLib.RowValues();
            ftrNewS = axMapVko.FeatureFactory.CreateSymbol(ptNewSmb, stNewSmb);
            ftrMapS = lyrMyLayer.AddFeature(ftrNewS, rvs1);
            //lyrMyLayer.LabelAtPoint(81.03778, 50.55694);*/
            
            
            //c1SprTooltipMapVko.Hide(axMapVko);
            //axMapVko.Layers.ClearSelection();

            //m_FindObjMap(l_lyrCity_all, "751", 100, 1);
            //rvs.Add("AviaPtrId").Value = "1111";
            //rvs._Item("AviaPtrId").Value = "1111";
            //ptNewSmb.Set(81.01694, 50.55028);
            
            //MapXLib.FeatureFactory ff = new MapXLib.FeatureFactory();
            //ftrNewSmbHm = ff.CreateSymbol(ptNewSmb);
            
            /*ftrNewSmbHm = axMapVko.FeatureFactory.CreateSymbol(ptNewSmb);
            ftrNewSmbHm.Attach(axMapVko);
            ftrMapSmbHm = lyrMyLayer.AddFeature(ftrNewSmbHm, rvs);*/

            //ftrNewSmbHm.Attach(axMapVko.Layers.Add(lyrMyLayer));

            //lyrMyLayer.
            //lyrSelMyLayer.AddByID(l_iSmbFtrId);
            //lyrMyLayer.Refresh();

            ///axMapVko.DefaultStyle.PickSymbol();
            
        }

        private void c1CmndDist_Click(object sender, C1.Win.C1Command.ClickEventArgs e)
        {
            c1SprTooltipMapVko.Hide(axMapVko);
            axMapVko.Layers.ClearSelection();
            axMapVko.CurrentTool = (MapXLib.ToolConstants)PolyRulerToolId;
        }

        private void c1CmndSelRec_Click(object sender, C1.Win.C1Command.ClickEventArgs e)
        {
            c1SprTooltipMapVko.Hide(axMapVko);
            axMapVko.Layers.ClearSelection();
            axMapVko.CurrentTool = MapXLib.ToolConstants.miRectSelectTool;
        }

        private void c1CmndSelRad_Click(object sender, C1.Win.C1Command.ClickEventArgs e)
        {
            c1SprTooltipMapVko.Hide(axMapVko);
            axMapVko.Layers.ClearSelection();
            axMapVko.CurrentTool = MapXLib.ToolConstants.miRadiusSelectTool;
        }

        private void c1CmndInfo_Click(object sender, C1.Win.C1Command.ClickEventArgs e)
        {
            c1SprTooltipMapVko.Hide(axMapVko);
            axMapVko.Layers.ClearSelection();
            axMapVko.CurrentTool = MapXLib.ToolConstants.miSelectTool;
        }

        private void c1CmndZoomOut_Click(object sender, C1.Win.C1Command.ClickEventArgs e)
        {
            c1SprTooltipMapVko.Hide(axMapVko);
            axMapVko.Layers.ClearSelection();
            axMapVko.CurrentTool = MapXLib.ToolConstants.miZoomOutTool;
        }

        private void c1CmndZoomIn_Click(object sender, C1.Win.C1Command.ClickEventArgs e)
        {
            c1SprTooltipMapVko.Hide(axMapVko);
            axMapVko.Layers.ClearSelection();
            axMapVko.CurrentTool = MapXLib.ToolConstants.miZoomInTool;
        }

        private void c1CmndPan_Click(object sender, C1.Win.C1Command.ClickEventArgs e)
        {
            c1SprTooltipMapVko.Hide(axMapVko);
            axMapVko.Layers.ClearSelection();
            axMapVko.CurrentTool = MapXLib.ToolConstants.miPanTool;
        }

        private void c1CmndArrow_Click(object sender, C1.Win.C1Command.ClickEventArgs e)
        {
            c1SprTooltipMapVko.Hide(axMapVko);
            axMapVko.Layers.ClearSelection();
            axMapVko.CurrentTool = MapXLib.ToolConstants.miArrowTool;

            l_lyrSdjav.Visible = false;
            l_lyrAvia_ptr.Visible = false;
        }

        private void axMapVko_ToolUsed(object sender, AxMapXLib.CMapXEvents_ToolUsedEvent e)
        {
            switch (e.toolNum)
            {
                case (int)MapXLib.ToolConstants.miSelectTool:
                    ObjFindSelectIhfo(e.x1, e.y1);
                    break;
            }

            /*ptNewSmb.Set(e.x1, e.y1);
            MapXLib.Features ftr; //= new MapXLib.Features();
            ftr = lyrMyLayer.SearchAtPoint(ptNewSmb);
            int iSel = ftr.Count;

            MapXLib.SearchResultTypeConstants SrHm = MapXLib.SearchResultTypeConstants.miSearchResultAll;
            lyrSelMyLayer.SelectByPoint(e.x1, e.y1, MapXLib.SelectionTypeConstants.miSelectionNew, SrHm);

            string strCityId = lyrSelMyLayer._Item(1).KeyValue;*/
        }

        private void axMapVko_PolyToolUsed(object sender, AxMapXLib.CMapXEvents_PolyToolUsedEvent e)
        {
            switch (e.toolNum)
            {
                case PolyRulerToolId:
                    double dbDistSo = 0;
                    double dbDistSum = 0;
                    MapXLib.Points pt = new MapXLib.Points();
                    pt = (MapXLib.Points)e.points;
                    if (pt.Count > 1)
                    {
                        for (int i = 2; i <= pt.Count; i++)
                        {
                            dbDistSo = axMapVko.Distance(pt._Item(i).X, pt._Item(i).Y, pt._Item(i - 1).X, pt._Item(i - 1).Y);
                            dbDistSum = dbDistSum + dbDistSo;
                        }
                        MapXLib.PolyToolFlagConstants plFlags = new MapXLib.PolyToolFlagConstants();
                        plFlags = (MapXLib.PolyToolFlagConstants)e.flags;
                        if (plFlags == MapXLib.PolyToolFlagConstants.miPolyToolEnd)
                        {
                            c1SprTooltipMapVko.Hide(axMapVko);
                        }
                        else
                        {
                            string strDistSo = Convert.ToString(dbDistSo).Substring(0, 5);
                            string strDistSum = Convert.ToString(dbDistSum).Substring(0, 5);
                            string strMsg = "Растояние - " + strDistSo + " км.<br>" +
                                            "<parm><hr noshade size=1 style='margin:2' color=Darker></parm>" +
                                            "Общее - " + strDistSum + " км.";
                            c1SprTooltipMapVko.Show(strMsg, axMapVko, l_iPointX, l_iPointY);
                        }
                    }
                    break;
            }
        }

        private void axMapVko_MouseDownEvent(object sender, AxMapXLib.CMapXEvents_MouseDownEvent e)
        {
            l_iPointX = Convert.ToInt32(e.x);
            l_iPointY = Convert.ToInt32(e.y);

            if (e.button == 2)
            {
                c1SprTooltipMapVko.Hide(axMapVko);
                //axMapVko.Layers.ClearSelection();
                string strCityId = l_lyrSelCity_all._Item(1).KeyValue;
                if (l_lyrSelCity_all.Count > 0 && strCityId == "18")
                {
                    Point pt = new Point();
                    pt.X = l_iPointX;
                    pt.Y = l_iPointY;
                    c1CntMenuMapNote.ShowContextMenu(axMapVko, pt);
                }

                if (l_lyrSelSdjav.Count > 0)
                {
                    Point pt = new Point();
                    pt.X = l_iPointX;
                    pt.Y = l_iPointY;
                    c1CntMenuMapSdjav.ShowContextMenu(axMapVko, pt);
                }

                if (l_lyrSelAvia_ptr.Count > 0)
                {
                    FrmAviaPtr.Instance = new FrmAviaPtr();
                    FrmAviaPtr.Instance.Show();
                }
            }
            else
            {
                double dbCorX = 0;
                double dbCorY = 0;
                int iScX = Convert.ToInt32(e.x);
                int iScY = Convert.ToInt32(e.y);

                axMapVko.ConvertCoord(ref e.x, ref e.y, ref dbCorX, ref dbCorY, MapXLib.ConversionConstants.miScreenToMap);

                MapXLib.Features FtrsUnderPt;
                MapXLib.Point pt = new MapXLib.Point();
                MapXLib.Feature ftr;

                pt.Set(dbCorX, dbCorY);
                FtrsUnderPt = lyrMyLayer.SearchAtPoint(pt);
                int iCntF = FtrsUnderPt.Count;
                if (iCntF > 0)
                {
                    ftr = FtrsUnderPt._Item(1);
                    int iFt = ftr.FeatureID;

                    //выделяет и отменяет выделение объекта одним кликом
                    bool blFnd = false;
                    foreach (MapXLib.Feature ftr1 in lyrMyLayer.Selection)
                    {
                        if (ftr1.FeatureID == iFt)
                        {
                            lyrMyLayer.Selection.SelectByID(iFt, MapXLib.SelectionTypeConstants.miSelectionRemove);
                            blFnd = true;
                        }
                    }
                    if (!blFnd)
                    {
                        lyrMyLayer.Selection.SelectByID(iFt, MapXLib.SelectionTypeConstants.miSelectionAppend);
                        //toolTip1.Show("234", axMapVko, iScX, iScY);

                        C1.Win.C1SuperTooltip.C1SuperTooltip TlSh = new C1.Win.C1SuperTooltip.C1SuperTooltip();
                        /*TlSh.IsBalloon = true;
                        string strInfoGps = "Опасность возникновения пожара!.<br>" +
                            "с.Жерновка222 Бородулихинский район.<br>" +
                            "растояние - 6,598 км, азимут - 311,517.<br>" +
                            "<parm><hr noshade size=1 style='margin:2' color=Darker></parm>" +
                            "Система спутникового мониторинга.";
                        TlSh.BackgroundGradient = C1.Win.C1SuperTooltip.BackgroundGradient.None;*/
                        
                        //запись и чтение в коллекции объектов
                        /*SortedList<int, C1.Win.C1SuperTooltip.C1SuperTooltip> srMas = new SortedList<int, C1.Win.C1SuperTooltip.C1SuperTooltip>();
                        int iId = iFt;
                        srMas.Add(iId, TlSh);
                        C1.Win.C1SuperTooltip.C1SuperTooltip TlSh1 = srMas.Values[0];
                        int iKey = srMas.Keys[0];
                        TlSh1.Show("234", axMapVko, iScX, iScY);*/
                        //......
                    }
                    //........


                }
            }
            //Double d_lon = 0;
            //Double d_lat = 0;
            //axMapVko.ConvertCoord(600, 700, d_lon, d_lat, 1);
            
            //ptNewSmb.Set(l_iPointX, l_iPointY);
            /*ptNewSmb.Set(81.03778, 50.55694);
            MapXLib.Features ftr; //= new MapXLib.Features();
            ftr = lyrMyLayer.SearchAtPoint(ptNewSmb);
            int iSel = ftr.Count;

            if (iSel > 0)
            {
                string strInfoGps = "";
            }*/
        }

        private void axMapVko_MouseWheelEvent(object sender, AxMapXLib.CMapXEvents_MouseWheelEvent e)
        {

        }

        private void axMapVko_KeyDownEvent(object sender, AxMapXLib.CMapXEvents_KeyDownEvent e)
        {
            if (e.keyCode == (short)Keys.Escape)
            {
                c1SprTooltipMapVko.Hide(axMapVko);
            }
        }

        private void c1CmndLyrSdjv_Click(object sender, C1.Win.C1Command.ClickEventArgs e)
        {
            l_lyrSdjav.Visible = true;
            l_lyrAvia_ptr.Visible = false;
        }

        private void c1CmndLyrAvia_Click(object sender, C1.Win.C1Command.ClickEventArgs e)
        {
            l_lyrSdjav.Visible = false;
            l_lyrAvia_ptr.Visible = true;           
        }

        private void c1CmndMnuAkimSklad_Click(object sender, C1.Win.C1Command.ClickEventArgs e)
        {
           FrmMain.Instance.m_LoadDocExl(1);
        }

        private void c1CmndSdjav1_Click(object sender, C1.Win.C1Command.ClickEventArgs e)
        {
            FrmSdjavInfo.Instance = new FrmSdjavInfo();

            FrmSdjavInfo.Instance.Text = "Амииак (СДЯВ)";
            FrmSdjavInfo.Instance.m_strInfoSdajv = "Количествово СДЯВ фактически-40 тн<br>Способ хранения - сжиж. под давл. в системе<br>" +
                "максимальная глубина заражения, 6,3 км<br>Количество населения, проживающего в ЗВЗ, 139 тыс.чел.<br>" +
                "<b><span style='color:red'>Возможные потери общие-69,5 санитарные-45,2 тыс.чел.</span></b><br>" +
                "<parm><hr noshade size=1 style='margin:2' color=Darker></parm>" +
                "Мин.энергетики и минеральных ресурсов НАК Казатомпром<br>" +
                "И.о.Ген.директора Шахворостов Ю.В.,<br>пр.Абая,102, тел. 87232 29-81-03";
            FrmSdjavInfo.Instance.m_iSdjavId = 45;

            FrmSdjavInfo.Instance.Show();
        }

        private void c1CmndSdjavTmk_1_Click(object sender, C1.Win.C1Command.ClickEventArgs e)
        {
            FrmSdjavInfo.Instance = new FrmSdjavInfo();

            FrmSdjavInfo.Instance.Text = "Хлор (СДЯВ)";
            FrmSdjavInfo.Instance.m_strInfoSdajv = "Количествово СДЯВ фактически-154 тн<br>Способ хранения - в танках (емкостях)<br>" +
                "максимальная глубина заражения, 7,45 км<br>Количество населения, проживающего в ЗВЗ, 43,1 тыс.чел.<br>" +
                "<b><span style='color:red'>Возможные потери общие-21,55 санитарные-7,54 тыс.чел.</span></b><br>" +
                "<parm><hr noshade size=1 style='margin:2' color=Darker></parm>" +
                "Министерство индустрии и торговли<br>" +
                "Президент - Шаяхметов Б.М. п. Новая Согра тел.87232 23-30-10";
            FrmSdjavInfo.Instance.m_iSdjavId = 701;

            FrmSdjavInfo.Instance.Show();
        }

        private void c1CmbFindCity_BeforeOpen(object sender, CancelEventArgs e)
        {
            l_intCityName = bsCityName.Position; // запоминает текущую позицию
        }

        private void c1CmbFindCity_Close(object sender, EventArgs e)
        {
            if (l_intCityName != bsCityName.Position)    //если просто откыть и закрыть сомбо данные не меняются
            {
                DataRow dr = dsCityName.Tables["CityVko"].Rows[bsCityName.Position];
                string strCityNameId = dr["CityID"].ToString();
                m_FindObjMap(l_lyrCity_all, strCityNameId, 100, 1);
            }
        }

        private void c1CmndNote_Click(object sender, C1.Win.C1Command.ClickEventArgs e)
        {
            FrmMain.Instance.m_LoadDocExl(2);
        }

        private void c1CmbFindCity_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Return)
            {
                e.Handled = true;
                
            }
        }

        private void c1CmndWaterSpas_Click(object sender, C1.Win.C1Command.ClickEventArgs e)
        {
            FrmInfoMsg.Instance = new FrmInfoMsg();
            FrmInfoMsg.Instance.Show();
        }

        private void c1BtnTest_Click(object sender, EventArgs e)
        {
            //lyrMyLayer.DeleteFeature(l_iSmbFtrId);  //удаляет созданный point
            
                //перемещает созданный point в новую точку координат
                MapXLib.Feature ftrMapSmbHm1 = lyrMyLayer.GetFeatureByID(l_iSmbFtrId);
                ftrMapSmbHm1.Point.Set(81.94778, 50.55694);
                ftrMapSmbHm1.Update();

                double dbCorX = 81.94778;
                double dbCorY = 50.55694;
                float iScX = 0;
                float iScY = 0;

                axMapVko.ConvertCoord(ref iScX, ref iScY, ref dbCorX, ref dbCorY, MapXLib.ConversionConstants.miMapToScreen);
                C1.Win.C1SuperTooltip.C1SuperTooltip TlSh = new C1.Win.C1SuperTooltip.C1SuperTooltip();
                TlSh.ShowAlways = true;
                TlSh.Show("234", axMapVko, Convert.ToInt32(iScX), Convert.ToInt32(iScY));
            //...................

            //ищет все выделенные объекты
                /*string strId1= lyrMyLayer.Selection._Item(1).KeyValue;
                foreach (MapXLib.Feature ftr1 in lyrMyLayer.Selection)
                {
                    int iId = ftr1.FeatureID;
                    string strRtr = ftr1.FeatureKey;
                }*/
            //........

            //делает select конкретному объекту FeatureID
                //lyrMyLayer.Selection.SelectByID(ftrMapSmbHm.FeatureID, MapXLib.SelectionTypeConstants.miSelectionAppend);
            //........


        }

        private void axMapVko_MouseMoveEvent(object sender, AxMapXLib.CMapXEvents_MouseMoveEvent e)
        {
            double dbCorX = 0;
            double dbCorY = 0;
            int iScX = Convert.ToInt32(e.x);
            int iScY = Convert.ToInt32(e.y);

            axMapVko.ConvertCoord(ref e.x, ref e.y, ref dbCorX, ref dbCorY, MapXLib.ConversionConstants.miScreenToMap);

            MapXLib.Features FtrsUnderPt;
            MapXLib.Point pt = new MapXLib.Point();
            MapXLib.Feature ftr;

            pt.Set(dbCorX, dbCorY);
            FtrsUnderPt = lyrMyLayer.SearchAtPoint(pt);
            int iCntF = FtrsUnderPt.Count;
            if (iCntF > 0)
            {
                axMapVko.CurrentTool = MapXLib.ToolConstants.miArrowTool;
                ftr = FtrsUnderPt._Item(1);
                int iFt = ftr.FeatureID;
                if (iFt == 1)
                {
                    //c1SprTooltipMapVko.Hide(axMapVko);
                    /*string strInfoGps = "Опасность возникновения пожара!.<br>" +
                        "с.Жерновка Бородулихинский район.<br>" +
                        "растояние - 6,598 км, азимут - 311,517.<br>" +
                        "<parm><hr noshade size=1 style='margin:2' color=Darker></parm>" +
                        "Система спутникового мониторинга.";
                    //string strInfoGps = "232АЦ";
                    c1SprTooltipMapVko.Show(strInfoGps, axMapVko, iScX, iScY);*/
                }
                if (iFt == 2)
                {
                    /*C1.Win.C1SuperTooltip.C1SuperTooltip TlSh = new C1.Win.C1SuperTooltip.C1SuperTooltip();
                    TlSh.Hide(axMapVko);
                    string strInfoGps = "Опасность возникновения пожара!.<br>" +
                        "с.Жерновка222 Бородулихинский район.<br>" +
                        "растояние - 6,598 км, азимут - 311,517.<br>" +
                        "<parm><hr noshade size=1 style='margin:2' color=Darker></parm>" +
                        "Система спутникового мониторинга.";
                    TlSh.Show(strInfoGps, axMapVko, iScX, iScY, 3000);*/
                }
            }
            else
            {
                axMapVko.CurrentTool = MapXLib.ToolConstants.miPanTool;
                c1SprTooltipMapVko.Hide(axMapVko);
            }
        }

        private void FrmMapVko_FormClosed(object sender, FormClosedEventArgs e)
        {
            int rtr = m_iNmb;
            FrmMain.Instance.srFrmMap.Remove(rtr);
        }

        private void FrmMapVko_FormClosing(object sender, FormClosingEventArgs e)
        {

        }

        private void axMapVko_MapViewChanged(object sender, EventArgs e)
        {

        }

        private void axMapVko_MapDraw(object sender, AxMapXLib.CMapXEvents_MapDrawEvent e)
        {

        }
    }
}
