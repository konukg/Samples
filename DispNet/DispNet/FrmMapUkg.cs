using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;
using C1.Win.C1FlexGrid;
//using AxMapXLib;

namespace DispNet
{
    public partial class FrmMapUkg : Form
    {
        static public FrmMapUkg Instance;
        public MapXLib.Layer m_lyrBuildHm, m_lyrStreetNet, m_lyrWater, m_lyrTemp;
        MapXLib.Selection l_SelBuildHm, l_SelStreetNet, l_SelWater;
        public const int PolyRulerToolId = 105;
        public const int CarTool = 106;
        MapXLib.Layer lyrMyLayer;

        BindingSource bsStreetMap;
        DataSet dsStreetMap = new DataSet();
        
        BindingSource bsHomeMap;
        DataSet dsHomeMap = new DataSet();
        DataSet dsHomeFullMap = new DataSet();
        DataSet dsWaterMap = new DataSet();

        BindingSource bsObjFullMap = new BindingSource();
        DataSet dsObjFullMap = new DataSet();

        int l_intStreetPos = 0; //позиция улицы если при закрытии c1CmbFndStr не выбрана новая улица то форме путевки данные не меняются
        int l_intHomePos = 0; //позиция дома если при закрытии c1ComboHome не выбрана новый дом то форме путевки данные не меняются
        int l_iCntrSz;  //размер формы по вертикали
        int l_iPointX;  //
        int l_iPointY;
        string l_strObjNameMap = "";    //заголовок для форм хар-тик дома и объекта

        MapXLib.Style stNewSmb = new MapXLib.Style();
        MapXLib.Point ptNewSmb = new MapXLib.Point();
        MapXLib.RowValues rvs = new MapXLib.RowValues();

        MapXLib.Feature ftrNewSmbHm = new MapXLib.Feature(); // для символа отображения дома
        MapXLib.Feature ftrMapSmbHm = new MapXLib.Feature();
        int l_iSmbFtrId = 0; // хранит последнее значение .FeatureID

        public FrmMapUkg()
        {
            InitializeComponent();
        }

        private void FrmMapUkg_Resize(object sender, EventArgs e)
        {
            Control control = (Control)sender;

            l_iCntrSz = control.Size.Height - 26;
            pnlFind.Height = l_iCntrSz; 
            axMapUkg.Height = control.Size.Height - 26;
            axMapUkg.Width = control.Size.Width - 243;
                        
            // меняет размер таблицы
            int iCnt = c1FlxGrdObjMap.Rows.Count;
            int iRwSz = c1FlxGrdObjMap.Rows.DefaultSize;
            int iGrdPosY = c1FlxGrdObjMap.Location.Y;
            iRwSz = iRwSz * iCnt;
            if (iRwSz < l_iCntrSz - iGrdPosY)
                c1FlxGrdObjMap.Height = iRwSz;
            else
                c1FlxGrdObjMap.Height = l_iCntrSz - iGrdPosY;
            
        }

        private void FrmMapUkg_Load(object sender, EventArgs e)
        {
            string strPath = FrmMain.Instance.m_strPathMapUkg;
            
            if (strPath.Length > 0)
            {
                string strPathFull;

                axMapUkg.Visible = false;

                strPathFull = strPath + "StreetNet.tab";
                m_lyrStreetNet = axMapUkg.Layers.Add(strPathFull, null);
                m_lyrStreetNet.LabelProperties.Style.TextFontHalo = true;

                strPathFull = strPath + "BuildHm.tab";
                m_lyrBuildHm = axMapUkg.Layers.Add(strPathFull, null);
                m_lyrBuildHm.LabelProperties.Style.TextFontHalo = true;
                m_lyrBuildHm.LabelProperties.Position = MapXLib.PositionConstants.miPositionBR;
                m_lyrBuildHm.Visible = false;
                //m_lyrBuildHm.Selectable = false;

                strPathFull = strPath + "Water.tab";
                m_lyrWater = axMapUkg.Layers.Add(strPathFull, null);
                m_lyrWater.LabelProperties.Style.TextFontHalo = true;
                m_lyrWater.LabelProperties.Position = MapXLib.PositionConstants.miPositionTC;
                m_lyrWater.Visible = false;

                

                int iLyrPos = 0;

                string strSqlTextMapUkg = "SELECT MapUkgId, MapUkgName, MapUkgPos FROM MapUkg ORDER BY MapUkgId";
                SqlCommand cmdMapUkg = new SqlCommand(strSqlTextMapUkg, FrmMain.Instance.m_cnn);
                SqlDataReader rdrMapUkg = cmdMapUkg.ExecuteReader();
                while (rdrMapUkg.Read())
                {
                    if (!rdrMapUkg.IsDBNull(1))
                    {
                        strPathFull = strPath + rdrMapUkg.GetString(1);
                    }
                    if (!rdrMapUkg.IsDBNull(2))
                    {
                        iLyrPos = rdrMapUkg.GetInt16(2);
                    }
                    m_lyrTemp = axMapUkg.Layers.Add(strPathFull, iLyrPos);
                    m_lyrTemp.Selectable = false;
                }
                rdrMapUkg.Close();
                //axMapUkg.sh
                axMapUkg.CenterX = 0.477122847749695;
                axMapUkg.CenterY = 0.393911691731262;
                axMapUkg.Zoom = 39.66;

                c1CmdSel.Pressed = true;
                axMapUkg.Visible = true;

                MapXLib.CoordSys oCoordSys;
                oCoordSys = m_lyrBuildHm.CoordSys.Clone();
                lyrMyLayer = axMapUkg.Layers.CreateLayer("Cars", null, 0, 32, oCoordSys);
                //axMapUkg.Layers.AnimationLayer = lyrMyLayer;
                lyrMyLayer.Selectable = true;

                axMapUkg.CreateCustomTool(PolyRulerToolId, MapXLib.ToolTypeConstants.miToolTypePoly, MapXLib.CursorConstants.miLabelCursor);
                axMapUkg.CreateCustomTool(CarTool, MapXLib.ToolTypeConstants.miToolTypePoint, MapXLib.CursorConstants.miSelectCursor);

                l_SelBuildHm = m_lyrBuildHm.Selection;  //устанавливает select для домов на карте
                l_SelStreetNet = m_lyrStreetNet.Selection;  //устанавливает select для улиц на карте
                l_SelWater = m_lyrWater.Selection;  //устанавливает select для водоисточников на карте

                dsStreetMap = FrmPermit.Instance.dsStreet.Copy();
                bsStreetMap = new BindingSource(dsStreetMap, "Street");
                bsStreetMap.Position = 0; //устанавливает в первую позицию для поиски в многооконном режиме

                dsHomeFullMap = FrmPermit.Instance.dsHomeFull.Copy();   //полный список для отображения info на карте
                dsHomeMap = FrmPermit.Instance.dsHome.Copy();
                bsHomeMap = new BindingSource(dsHomeMap, "Buildings");
                bsHomeMap.Position = 0; //устанавливает в первую позицию для поиски в многооконном режиме

                dsWaterMap = FrmPermit.Instance.m_dsWaterData.Copy();   //водоисточники
                                
                c1CmbFndStr.DataSource = bsStreetMap;
                c1CmbFndStr.ValueMember = "StreetName";
                c1CmbFndStr.DisplayMember = "StreetName";

                c1CmbFndStr.Columns.RemoveAt(0);
                c1CmbFndStr.Columns.RemoveAt(6);
                c1CmbFndStr.Columns.RemoveAt(5);
                c1CmbFndStr.Columns.RemoveAt(4);
                //c1CmbFndStr.Columns.RemoveAt(3);
                c1CmbFndStr.Columns[0].Caption = "Улица";
                c1CmbFndStr.Columns[1].Caption = "Тип";
                c1CmbFndStr.Columns[2].Caption = "Район";
                c1CmbFndStr.Columns[3].Caption = "Регион";

                c1CmbFndStr.DropDownWidth = 440;

                stNewSmb.SymbolType = MapXLib.SymbolTypeConstants.miSymbolTypeBitmap;
                stNewSmb.SymbolBitmapSize = 16;
                stNewSmb.SymbolBitmapTransparent = true;

                //загружает объекты
                string strSqlTextObjFullMap = "SELECT Buildings.BuildId, Buildings.StreetId, Buildings.BuildName, ObjUkg.ObjName, ObjUkgStaff.OrgName " +
                    "FROM Buildings INNER JOIN ObjUkg ON Buildings.BuildId = ObjUkg.BuildId " +
                    "INNER JOIN ObjUkgStaff ON ObjUkg.ObjStaffId = ObjUkgStaff.ObjStaffId";
                SqlDataAdapter daObjFullMap = new SqlDataAdapter(strSqlTextObjFullMap, FrmMain.Instance.m_cnn);
                daObjFullMap.Fill(dsObjFullMap, "Buildings");
                bsObjFullMap.DataSource = dsObjFullMap;
                bsObjFullMap.DataMember = "Buildings";

            }
        }

        public bool m_FindObjMap(MapXLib.Layer lyrFnd, string strHome, double iZoom, int iSetSmb)
        {
            MapXLib.FindFeature fndFeat;
            
            axMapUkg.SelectionStyle.RegionColor = Convert.ToUInt32(MapXLib.ColorConstants.miColorBlack);
            
            //string strHome = "622"; //587 - Пролетарская
            fndFeat = lyrFnd.Find.Search(strHome, null);
            if (fndFeat.FindRC % 10 == 1)
            {
                lyrFnd.Selection.SelectByRegion(lyrFnd, fndFeat.FeatureID, MapXLib.SelectionTypeConstants.miSelectionNew);
                //m_lyrWater.Selection.SelectByRadius(fndFeat.CenterX, fndFeat.CenterY, 0.5, MapXLib.SelectionTypeConstants.miSelectionNew);
                try
                {
                    
                }
                catch (ArgumentException)
                {
                    Console.WriteLine("An element with Key = \"txt\" already exists.");
                }
                
                axMapUkg.CenterX = fndFeat.CenterX;
                axMapUkg.CenterY = fndFeat.CenterY;
                axMapUkg.Zoom = iZoom;

                if(iSetSmb == 1)
                {
                    if(l_iSmbFtrId > 0)
                        lyrMyLayer.DeleteFeature(ftrMapSmbHm);

                    ptNewSmb.Set(fndFeat.CenterX, fndFeat.CenterY);
                    bool bl = lyrMyLayer.OverrideStyle;
                    stNewSmb.SymbolBitmapName = "PIN5-32.BMP";
                    ftrNewSmbHm = axMapUkg.FeatureFactory.CreateSymbol(ptNewSmb, stNewSmb);
                    ftrMapSmbHm = lyrMyLayer.AddFeature(ftrNewSmbHm, rvs);
                    l_iSmbFtrId = ftrMapSmbHm.FeatureID;
                    lyrMyLayer.Refresh();
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

        private void UnSelObjMap(MapXLib.Layer lyrUnSel, string strId)
        {
            MapXLib.FindFeature fndFeat;

            fndFeat = lyrUnSel.Find.Search(strId, null);
            if (fndFeat.FindRC % 10 == 1)
            {
                lyrUnSel.Selection.SelectByRegion(lyrUnSel, fndFeat.FeatureID, MapXLib.SelectionTypeConstants.miSelectionRemove);
                try
                {

                }
                catch (ArgumentException)
                {
                    //Console.WriteLine("An element with Key = \"txt\" already exists.");
                }
            }
            
        }

        private void FindStreetMap(int iSing)
        {
            if (l_intStreetPos != bsStreetMap.Position)    //если просто откыть и закрыть сомбо данные не меняются
            {
                c1FlxGrdObjMap.Visible = false;
                //RemoveItemObjMap();

                c1ComboHome.Focus();
                DataRow dr = dsStreetMap.Tables["Street"].Rows[bsStreetMap.Position];
                string strStrId = dr["StreetId"].ToString();
                DataView dvFndStreet = new DataView(dsStreetMap.Tables["Street"], "StreetId = '" + strStrId + "'",
                                "StreetName", DataViewRowState.CurrentRows);
                int rowIndex = dvFndStreet.Find(c1CmbFndStr.Text);
                if (rowIndex != -1)
                {
                    textBoxStreetType.Text = dr["StreetTypeName"].ToString();
                    bsHomeMap.Filter = "StreetId = '" + strStrId + "'";
                    c1ComboHome.DataSource = bsHomeMap;
                    c1ComboHome.ValueMember = "BuildName";
                    c1ComboHome.DisplayMember = "BuildName";
                    if (c1ComboHome.Columns.Count > 1)
                        c1ComboHome.Columns.RemoveAt(1);
                    if (iSing == 1)
                        m_FindObjMap(m_lyrStreetNet, strStrId, 3.5, 0);
                }
            }
        }

        private void RemoveItemObjMap()
        {
            int iCnt = c1FlxGrdObjMap.Rows.Count;
            for (int i = 0; i < iCnt; i = i + 1)
                c1FlxGrdObjMap.RemoveItem(0);

            if (l_iSmbFtrId > 0)
            {
                lyrMyLayer.DeleteFeature(ftrMapSmbHm);  // убирает значек из lyrMyLayer выбранный через кнопку таблицы объектов
                l_iSmbFtrId = 0;
            }
        }

        private void axMapUkg_MapViewChanged(object sender, EventArgs e)
        {
            if (axMapUkg.Zoom < 6)
            {
                //m_lyrStreetNet.AutoLabel = true;
                m_lyrBuildHm.Visible = true;
            }
            else
            {
                //m_lyrStreetNet.AutoLabel = false;
                m_lyrBuildHm.Visible = false;
            }

            if (axMapUkg.Zoom < 4)
            {
                m_lyrStreetNet.AutoLabel = true;
                //m_lyrBuildHm.Visible = true;
            }
            else
            {
                m_lyrStreetNet.AutoLabel = false;
                //m_lyrBuildHm.Visible = false;
            }

            if (axMapUkg.Zoom < 2)
            {
                m_lyrBuildHm.AutoLabel = true;
                m_lyrWater.Visible = true;
            }
            else
            {
                m_lyrBuildHm.AutoLabel = false;
                m_lyrWater.Visible = false;
            }

            if (axMapUkg.Zoom < 1.5)
            {

                m_lyrWater.AutoLabel = true;
            }
            else
            {

                m_lyrWater.AutoLabel = false;
            }
        }

        private void btnTest_Click(object sender, EventArgs e)
        {
            
        }

        private void c1CmdSel_Click(object sender, C1.Win.C1Command.ClickEventArgs e)
        {
            c1CmdSel.Pressed = true;
            c1CmdPan.Pressed = false;
            c1CmdZoomIn.Pressed = false;
            c1CmdZoomOut.Pressed = false;
            c1CmdInfo.Pressed = false;
            c1CmdSelRec.Pressed = false;
            c1CmdDist.Pressed = false;
            c1CmdSymbol.Pressed = false;
            c1CmdSelRad.Pressed = false;

            l_SelStreetNet.ClearSelection();
            l_SelBuildHm.ClearSelection();
            l_SelWater.ClearSelection();

            c1SprTooltipMapUkg.Hide(axMapUkg);
            axMapUkg.CurrentTool = MapXLib.ToolConstants.miArrowTool;
        }

        private void c1CmdPan_Click(object sender, C1.Win.C1Command.ClickEventArgs e)
        {
            c1CmdSel.Pressed = false;
            c1CmdPan.Pressed = true;
            c1CmdZoomIn.Pressed = false;
            c1CmdZoomOut.Pressed = false;
            c1CmdInfo.Pressed = false;
            c1CmdSelRec.Pressed = false;
            c1CmdDist.Pressed = false;
            c1CmdSymbol.Pressed = false;
            c1CmdSelRad.Pressed = false;

            l_SelStreetNet.ClearSelection();
            l_SelBuildHm.ClearSelection();
            l_SelWater.ClearSelection();

            c1SprTooltipMapUkg.Hide(axMapUkg);
            axMapUkg.CurrentTool = MapXLib.ToolConstants.miPanTool;
        }

        private void c1CmdZoomIn_Click(object sender, C1.Win.C1Command.ClickEventArgs e)
        {
            c1CmdSel.Pressed = false;
            c1CmdPan.Pressed = false;
            c1CmdZoomIn.Pressed = true;
            c1CmdZoomOut.Pressed = false;
            c1CmdInfo.Pressed = false;
            c1CmdSelRec.Pressed = false;
            c1CmdDist.Pressed = false;
            c1CmdSymbol.Pressed = false;
            c1CmdSelRad.Pressed = false;

            l_SelStreetNet.ClearSelection();
            l_SelBuildHm.ClearSelection();
            l_SelWater.ClearSelection();

            c1SprTooltipMapUkg.Hide(axMapUkg);
            axMapUkg.CurrentTool = MapXLib.ToolConstants.miZoomInTool;
        }

        private void c1CmdZoomOut_Click(object sender, C1.Win.C1Command.ClickEventArgs e)
        {
            c1CmdSel.Pressed = false;
            c1CmdPan.Pressed = false;
            c1CmdZoomIn.Pressed = false;
            c1CmdZoomOut.Pressed = true;
            c1CmdInfo.Pressed = false;
            c1CmdSelRec.Pressed = false;
            c1CmdDist.Pressed = false;
            c1CmdSymbol.Pressed = false;
            c1CmdSelRad.Pressed = false;

            l_SelStreetNet.ClearSelection();
            l_SelBuildHm.ClearSelection();
            l_SelWater.ClearSelection();

            c1SprTooltipMapUkg.Hide(axMapUkg);
            axMapUkg.CurrentTool = MapXLib.ToolConstants.miZoomOutTool;
        }

        private void c1CmdInfo_Click(object sender, C1.Win.C1Command.ClickEventArgs e)
        {
            c1CmdSel.Pressed = false;
            c1CmdPan.Pressed = false;
            c1CmdZoomIn.Pressed = false;
            c1CmdZoomOut.Pressed = false;
            c1CmdInfo.Pressed = true;
            c1CmdSelRec.Pressed = false;
            c1CmdDist.Pressed = false;
            c1CmdSymbol.Pressed = false;
            c1CmdSelRad.Pressed = false;

            l_SelStreetNet.ClearSelection();
            l_SelBuildHm.ClearSelection();
            l_SelWater.ClearSelection();

            c1SprTooltipMapUkg.Hide(axMapUkg);
            axMapUkg.CurrentTool = MapXLib.ToolConstants.miSelectTool;
        }

        private void c1CmdSelRec_Click(object sender, C1.Win.C1Command.ClickEventArgs e)
        {
            c1CmdSel.Pressed = false;
            c1CmdPan.Pressed = false;
            c1CmdZoomIn.Pressed = false;
            c1CmdZoomOut.Pressed = false;
            c1CmdInfo.Pressed = false;
            c1CmdSelRec.Pressed = true;
            c1CmdDist.Pressed = false;
            c1CmdSymbol.Pressed = false;
            c1CmdSelRad.Pressed = false;

            l_SelStreetNet.ClearSelection();
            l_SelBuildHm.ClearSelection();
            l_SelWater.ClearSelection();

            c1SprTooltipMapUkg.Hide(axMapUkg);
            axMapUkg.CurrentTool = MapXLib.ToolConstants.miRectSelectTool;
        }

        private void c1CmdDist_Click(object sender, C1.Win.C1Command.ClickEventArgs e)
        {
            c1CmdSel.Pressed = false;
            c1CmdPan.Pressed = false;
            c1CmdZoomIn.Pressed = false;
            c1CmdZoomOut.Pressed = false;
            c1CmdInfo.Pressed = false;
            c1CmdSelRec.Pressed = false;
            c1CmdDist.Pressed = true;
            c1CmdSymbol.Pressed = false;
            c1CmdSelRad.Pressed = false;

            l_SelStreetNet.ClearSelection();
            l_SelBuildHm.ClearSelection();
            l_SelWater.ClearSelection();

            c1SprTooltipMapUkg.Hide(axMapUkg);
            axMapUkg.CurrentTool = (MapXLib.ToolConstants) PolyRulerToolId;
           
        }

        private void c1CmdSymbol_Click(object sender, C1.Win.C1Command.ClickEventArgs e)
        {
            c1CmdSel.Pressed = false;
            c1CmdPan.Pressed = false;
            c1CmdZoomIn.Pressed = false;
            c1CmdZoomOut.Pressed = false;
            c1CmdInfo.Pressed = false;
            c1CmdSelRec.Pressed = false;
            c1CmdDist.Pressed = false;
            c1CmdSelRad.Pressed = false;
            c1CmdSymbol.Pressed = true;

            l_SelStreetNet.ClearSelection();
            l_SelBuildHm.ClearSelection();
            l_SelWater.ClearSelection();

            c1SprTooltipMapUkg.Hide(axMapUkg);
            axMapUkg.CurrentTool = (MapXLib.ToolConstants) CarTool;
        }

        private void c1CmdSelRad_Click(object sender, C1.Win.C1Command.ClickEventArgs e)
        {
            c1CmdSel.Pressed = false;
            c1CmdPan.Pressed = false;
            c1CmdZoomIn.Pressed = false;
            c1CmdZoomOut.Pressed = false;
            c1CmdInfo.Pressed = false;
            c1CmdSelRec.Pressed = false;
            c1CmdDist.Pressed = false;
            c1CmdSymbol.Pressed = false;
            c1CmdSelRad.Pressed = true;

            l_SelStreetNet.ClearSelection();
            l_SelBuildHm.ClearSelection();
            l_SelWater.ClearSelection();

            c1SprTooltipMapUkg.Hide(axMapUkg);
            axMapUkg.CurrentTool = MapXLib.ToolConstants.miRadiusSelectTool;
        }

        private void c1CmbFndStr_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Return)
            {
                e.Handled = true;
                FindStreetMap(1);   //если 1 ищет улицу на карте
            }
            if (e.KeyChar == (char)Keys.Escape)
            {
                e.Handled = true;

                c1CmbFndStr.Text = "";
                textBoxStreetType.Text = "";
                c1ComboHome.Text = "";
                bsHomeMap.Filter = "StreetId = '0'";
                c1ComboHome.DataSource = bsHomeMap;
                c1ComboHome.ValueMember = "BuildName";

                //RemoveItemObjMap();
            }
        }

        private void c1CmbFndStr_BeforeOpen(object sender, CancelEventArgs e)
        {
            l_intStreetPos = bsStreetMap.Position; // запоминает текущую позицию улицы
        }

        private void c1CmbFndStr_Close(object sender, EventArgs e)
        {
            FindStreetMap(0);
        }

        private void c1ComboHome_BeforeOpen(object sender, CancelEventArgs e)
        {
            l_intHomePos = bsHomeMap.Position; // запоминает текущую позицию улицы для combo дом
        }

        private void c1ComboHome_Close(object sender, EventArgs e)
        {
            if (l_intHomePos != bsHomeMap.Position)    //если просто откыть и закрыть сомбо данные не меняются
            {
                DataSet dsHomeSel = new DataSet();
                BindingSource bsHomeSel = new BindingSource();

                DataRow dr = dsStreetMap.Tables["Street"].Rows[bsStreetMap.Position];
                string strStreetId = dr["StreetId"].ToString();
                string strBuildHome = c1ComboHome.Text;
                string strSqlTextSelHome = "SELECT Buildings.BuildId, Buildings.StreetId, Buildings.BuildName, ObjUkg.ObjName, ObjUkgStaff.OrgName " +
                "FROM ((Buildings INNER JOIN ObjUkg ON Buildings.BuildId = ObjUkg.BuildId) " +
                "INNER JOIN ObjUkgStaff ON ObjUkg.ObjStaffId = ObjUkgStaff.ObjStaffId) " +
                "WHERE (((Buildings.StreetId)='" + strStreetId + "') AND ((Buildings.BuildName)='" + strBuildHome + "')) " +
                "ORDER BY ObjUkg.ObjName";
                // выбор объекта по номеру дома - если есть
                SqlDataAdapter daHomeSel = new SqlDataAdapter(strSqlTextSelHome, FrmMain.Instance.m_cnn);
                daHomeSel.Fill(dsHomeSel, "Buildings");
                bsHomeSel.DataSource = dsHomeSel;
                bsHomeSel.DataMember = "Buildings";

                int iCnt = bsHomeSel.Count;
                if (iCnt == 0)
                {
                    //поиск BuildId если выводить дом на карту в качестве информции
                    DataView dvHome = new DataView(dsHomeFullMap.Tables["Buildings"], "StreetId = '" + strStreetId + "'",
                                "BuildName", DataViewRowState.CurrentRows);
                    int rowIndex = dvHome.Find(c1ComboHome.Text);
                    if (rowIndex != -1)
                    {
                        c1FlxGrdObjMap.Visible = false;
                        RemoveItemObjMap();
                        string strBuildId = dvHome[rowIndex]["BuildId"].ToString();
                        m_FindObjMap(m_lyrBuildHm, strBuildId, 1, 1);   //bool blFnd = 
                        UnSelObjMap(m_lyrStreetNet, strStreetId);
                        //string strMapBuildId = "";
                        //if(blFnd)
                            //strMapBuildId = m_lyrBuildHm.Selection._Item(1).KeyValue;
                        axMapUkg.Focus();
                    }

                }
                else
                {
                    RemoveItemObjMap();
                    
                    string strObjName = "";
                    DataRow drObj;
                    for (int i = 0; i < iCnt; i = i + 1)
                    {
                        drObj = dsHomeSel.Tables["Buildings"].Rows[i];
                        strObjName = drObj["ObjName"].ToString();
                        string strBuildId = drObj["BuildId"].ToString();
                        object[] items = { strObjName, strBuildId, strStreetId };
                        c1FlxGrdObjMap.AddItem(items, i, 1);
                    }

                    c1FlxGrdObjMap.Cols[4].Width = 150;
                    //c1FlxGrdObjMap.Cols[4].ComboList = "...";
                    // меняет размер таблицы
                    int iRwSz = c1FlxGrdObjMap.Rows.DefaultSize;
                    int iGrdPosY = c1FlxGrdObjMap.Location.Y;
                    iRwSz = iRwSz * iCnt;
                    if (iRwSz < l_iCntrSz - iGrdPosY)
                        c1FlxGrdObjMap.Height = iRwSz;
                    else
                        c1FlxGrdObjMap.Height = l_iCntrSz - iGrdPosY;
                    
                    c1FlxGrdObjMap.Visible = true;
                    c1FlxGrdObjMap.Select(1, 0);
                }
            }
        }

        private void c1ComboHome_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Return)
            {
                e.Handled = true;
            }
        }

        private void c1FlxGrdObjMap_CellButtonClick(object sender, RowColEventArgs e)
        {
            string strBuildId = c1FlxGrdObjMap.GetDataDisplay(c1FlxGrdObjMap.RowSel, 2);
            string strStreetId = c1FlxGrdObjMap.GetDataDisplay(c1FlxGrdObjMap.RowSel, 3);
            
            c1SprTooltipMapUkg.Hide(axMapUkg);

            m_FindObjMap(m_lyrBuildHm, strBuildId, 0.5, 1);
            UnSelObjMap(m_lyrStreetNet, strStreetId);
            
            axMapUkg.Focus();
            
        }

        private void axMapUkg_MouseWheelEvent(object sender, AxMapXLib.CMapXEvents_MouseWheelEvent e)
        {
            /*e.enableDefault = false;
            double dbZm = axMapUkg.Zoom;
            double dbScr = 39.66 / dbZm;
            if (e.zDelta > 0)
            {
                axMapUkg.Zoom = dbZm - 0.1;   // (1 - l_iScrMap);
                l_iScrMap = l_iScrMap + 0.01;
            }
            else
            {
                axMapUkg.Zoom = dbZm + 0.1;   //1 + l_iScrMap);
                //l_iScrMap++;
            }*/
        }

        private void axMapUkg_ToolUsed(object sender, AxMapXLib.CMapXEvents_ToolUsedEvent e)
        {
 
            switch (e.toolNum)
            {
                case (int)MapXLib.ToolConstants.miSelectTool:
                    ObjFindSelectIhfo(e.x1, e.y1);
                    break;
                case (int)MapXLib.ToolConstants.miRectSelectTool:
                    MapXLib.Features fsSearchObjRect;
                    MapXLib.Rectangle rcRect = new MapXLib.Rectangle();

                    rcRect.Set(e.x1, e.y1, e.x2, e.y2);
                    fsSearchObjRect = m_lyrBuildHm.SearchWithinRectangle(rcRect, MapXLib.SearchTypeConstants.miSearchTypeCentroidWithin);
                    ObjFindSelectTool(fsSearchObjRect);
                    break;
                case (int)MapXLib.ToolConstants.miRadiusSelectTool:
                    MapXLib.Features fsSearchObjRad;
                    
                    MapXLib.Point pt = new MapXLib.Point();
                    pt.Set(e.x1, e.y1);
                    double dbDist = axMapUkg.Distance(e.x1, e.y1, e.x2, e.y2);
                    fsSearchObjRad = m_lyrBuildHm.SearchWithinDistance(pt, dbDist, MapXLib.MapUnitConstants.miUnitKilometer, MapXLib.SearchTypeConstants.miSearchTypeCentroidWithin);
                    ObjFindSelectTool(fsSearchObjRad);
                    break;
                case CarTool:
                    MapXLib.Feature fNewSymbol = new MapXLib.Feature();
                    MapXLib.Feature fMapSymbol = new MapXLib.Feature();
                    
                    ptNewSmb.Set(e.x1, e.y1);
                    bool bl = lyrMyLayer.OverrideStyle;
                    stNewSmb.SymbolBitmapName = "PIN5-32.BMP";
                    fNewSymbol = axMapUkg.FeatureFactory.CreateSymbol(ptNewSmb, stNewSmb);
                    fMapSymbol = lyrMyLayer.AddFeature(fNewSymbol, rvs);
                    lyrMyLayer.Refresh();
                    break;
            }
        }

        private void axMapUkg_PolyToolUsed(object sender, AxMapXLib.CMapXEvents_PolyToolUsedEvent e)
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
                            dbDistSo = axMapUkg.Distance(pt._Item(i).X, pt._Item(i).Y, pt._Item(i - 1).X, pt._Item(i - 1).Y);
                            dbDistSum = dbDistSum + dbDistSo;
                        }
                        MapXLib.PolyToolFlagConstants plFlags = new MapXLib.PolyToolFlagConstants();
                        plFlags = (MapXLib.PolyToolFlagConstants)e.flags;
                        if (plFlags == MapXLib.PolyToolFlagConstants.miPolyToolEnd)
                        {
                            c1SprTooltipMapUkg.Hide(axMapUkg);
                        }
                        else
                        {
                            string strDistSo = Convert.ToString(dbDistSo).Substring(0, 5);
                            string strDistSum = Convert.ToString(dbDistSum).Substring(0, 5);
                            string strMsg = "Растояние - " + strDistSo + " км.<br>" +
                                            "<parm><hr noshade size=1 style='margin:2' color=Darker></parm>" +
                                            "Общее - " + strDistSum + " км.";
                            c1SprTooltipMapUkg.Show(strMsg, axMapUkg, l_iPointX, l_iPointY);    
                        }
                    }
                    break;
            }
        }

        private void axMapUkg_MouseDownEvent(object sender, AxMapXLib.CMapXEvents_MouseDownEvent e)
        {
            l_iPointX = Convert.ToInt32(e.x);
            l_iPointY = Convert.ToInt32(e.y);

            if (e.button == 2)
            {
                if (l_SelBuildHm.Count > 0)
                {
                    c1SprTooltipMapUkg.Hide(axMapUkg);

                    Point pt = new Point();
                    pt.X = l_iPointX;
                    pt.Y = l_iPointY;
                    c1CntMenuMapUkg.ShowContextMenu(axMapUkg, pt);
                }
            }
        }

        private void c1CntMnMapObj_Click(object sender, C1.Win.C1Command.ClickEventArgs e)
        {
            BindingSource bsObjFnd = new BindingSource();
            DataSet dsObjFnd = new DataSet();

            string strBuildId = l_SelBuildHm._Item(1).KeyValue;
            string strSqlTextObjFnd = "SELECT ObjId, BuildId FROM ObjUkg " +
                "WHERE (BuildId ='" + strBuildId + "')";
                // выбор объекта по номеру дома - если есть
            SqlDataAdapter daObjFnd = new SqlDataAdapter(strSqlTextObjFnd, FrmMain.Instance.m_cnn);
            daObjFnd.Fill(dsObjFnd, "ObjUkg");
            bsObjFnd.DataSource = dsObjFnd;
            bsObjFnd.DataMember = "ObjUkg";

            int iCnt = -1;
            iCnt = bsObjFnd.Count;
            if (iCnt == 0)
            {
                FrmHomeData.Instance = new FrmHomeData();
                FrmHomeData.Instance.Text = l_strObjNameMap;
                FrmHomeData.Instance.m_strBuildIdHome = strBuildId;
                FrmHomeData.Instance.Show();
                FrmHomeData.Instance.chkBoxGarret.Focus();
            }
            if (iCnt == 1)
            {
                FrmObjUkgData.Instance = new FrmObjUkgData();
                FrmObjUkgData.Instance.Text = l_strObjNameMap;
                FrmObjUkgData.Instance.m_strBuildIdObj = strBuildId;
                FrmObjUkgData.Instance.Show();
            }
                
        }

        private void c1CntMnMapPmt_Click(object sender, C1.Win.C1Command.ClickEventArgs e)
        {
            string strMsg = "Записать информацию об объекте в путевку?";
            if (MessageBox.Show(strMsg, "Выбор объекта",
                    MessageBoxButtons.YesNo, MessageBoxIcon.Question)
                    == DialogResult.Yes)
            {
                ObjMapPmt();
            }
        }

        private void ObjMapPmt()
        {
            BindingSource bsObjFnd = new BindingSource();
            DataSet dsObjFnd = new DataSet();

            string strBuildId = l_SelBuildHm._Item(1).KeyValue;
            string strSqlTextBuildFnd = "SELECT Buildings.BuildId, Buildings.StreetId, Buildings.BuildName, Street.StreetName, StreetType.StreetTypeName " +
                    "FROM Buildings INNER JOIN Street ON Buildings.StreetId = Street.StreetId " +
                    "INNER JOIN StreetType ON Street.StreetTypeId = StreetType.StreetTypeId " +
                    "WHERE (Buildings.BuildId = '" + strBuildId + "')";
            // поиск по номеру Id дома - если есть
            SqlCommand cmdObjFnd = new SqlCommand(strSqlTextBuildFnd, FrmMain.Instance.m_cnn);
            SqlDataReader rdrObjFnd = cmdObjFnd.ExecuteReader();
            
            int iCnt = 0;
            string strObjId = "";
            string strStreetId = "";
            string strBuildName = "";
            string strStreetName = "";
            string strStreetTypeName = "";
            while (rdrObjFnd.Read())
            {
                if (!rdrObjFnd.IsDBNull(0))
                    strObjId = rdrObjFnd.GetInt32(0).ToString();
                if (!rdrObjFnd.IsDBNull(1))
                    strStreetId = rdrObjFnd.GetInt32(1).ToString();
                if (!rdrObjFnd.IsDBNull(2))
                    strBuildName = rdrObjFnd.GetString(2);
                if (!rdrObjFnd.IsDBNull(3))
                    strStreetName = rdrObjFnd.GetString(3);
                if (!rdrObjFnd.IsDBNull(4))
                    strStreetTypeName = rdrObjFnd.GetString(4);
                iCnt++;
            }
            rdrObjFnd.Close();
            // поиск объекта по Id дома - если есть
            string strSqlTextObjFnd = "SELECT Buildings.BuildId, ObjUkg.ObjName, ObjUkgStaff.OrgName " +
                    "FROM ((Buildings INNER JOIN ObjUkg ON Buildings.BuildId = ObjUkg.BuildId) " +
                    "INNER JOIN ObjUkgStaff ON ObjUkg.ObjStaffId = ObjUkgStaff.ObjStaffId) " +
                    "WHERE (Buildings.BuildId = '" + strBuildId + "')";
            SqlCommand cmdObj = new SqlCommand(strSqlTextObjFnd, FrmMain.Instance.m_cnn);
            SqlDataReader rdrObj = cmdObj.ExecuteReader();

            int iCntObj = 0;
            string strObjName = "";
            string strOrgName = "";
             while (rdrObj.Read())
            {
                if (!rdrObj.IsDBNull(1))
                    strObjName = rdrObj.GetString(1);
                if (!rdrObj.IsDBNull(2))
                    strOrgName = rdrObj.GetString(2);
                iCntObj++;
            }
            rdrObj.Close();

            if (iCnt == 1)
            {
                int itemFound = FrmPermit.Instance.bsStreet.Find("StreetId", strStreetId);
                FrmPermit.Instance.bsStreet.Position = itemFound;   // записывает позицию для путевки

                FrmPermit.Instance.textBoxStreetType.Text = strStreetTypeName;
                FrmPermit.Instance.c1ComboFnd.Text = strStreetName;
                FrmPermit.Instance.c1ComboHome.Text = strBuildName;
                
                FrmPermit.Instance.m_strBuildIdMap = strObjId;    //BuildId объекта для вывода информации в главном окне
                FrmPermit.Instance.m_blWrHome = true;

                if (iCntObj == 1)   //дом найден как объект
                {
                    string strObj = strObjName + " (" + strOrgName + ")";
                    FrmPermit.Instance.textBoxObj.Text = strObj;
                    FrmPermit.Instance.m_TechnicsLoadObj(strObjId);
                }
                else
                {
                    FrmPermit.Instance.textBoxObj.Text = "Жилой дом";
                    FrmPermit.Instance.c1ComboRang.Text = "1";
                    FrmPermit.Instance.m_TechnicsLoad("1", c1ComboHome.Text);
                    FrmPermit.Instance.m_blObjSign = false;
                }
                FrmPermit.Instance.m_strBuildIdMap = strObjId;    //BuildId объекта для вывода информации в главном окне
                FrmPermit.Instance.m_blWrHome = true;

                if (!FrmPermit.Instance.blFrmPmtShow)
                {
                    FrmPermit.Instance.WindowState = FormWindowState.Normal;
                    FrmPermit.Instance.Show();
                    FrmPermit.Instance.blFrmPmtShow = true;
                }
                else
                    FrmPermit.Instance.Activate();

                FrmPermit.Instance.textBoxFire.Focus();
            }

        }

        private void txtBoxFndObj_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Return)
            {
                e.Handled = true;
                if(c1FlxGrdObjMap.Rows.Count > 0)
                    RemoveItemObjMap();
                ObjFindMapLike();
            }
        }

        private void ObjFindMapLike()
        {
            string strFndObName = txtBoxFndObj.Text;
            string strObjNameSQL = "";
            if(chkBoxFndFull.Checked)
                strObjNameSQL = "WHERE (ObjUkg.ObjName = '" + strFndObName + "')";
            else
                strObjNameSQL = "WHERE (ObjUkg.ObjName LIKE '%" + strFndObName + "%')";

            string strSqlTextObjFnd = "SELECT ObjUkg.ObjId, ObjUkg.BuildId, ObjUkg.ObjName, ObjUkg.ObjTel, ObjUkg.ObjAutoMon, " +
                "ObjUkgStaff.OrgName, ObjUkgStaff.DirName, ObjUkgStaff.DirPhone, ObjUkgStaff.DispPhone, " +
                "Buildings.BuildName, BuildingType.BuildTypeName, StreetType.StreetTypeName, Street.StreetName, Unit.UnitName, " +
                "Street.StreetId " +
                "FROM ObjUkg INNER JOIN " +
                      "ObjUkgStaff ON ObjUkg.ObjStaffId = ObjUkgStaff.ObjStaffId INNER JOIN " +
                      "Buildings ON ObjUkg.BuildId = Buildings.BuildId INNER JOIN " +
                      "BuildingType ON Buildings.BuildTypeId = BuildingType.BuildTypeId INNER JOIN " +
                      "Street ON Buildings.StreetId = Street.StreetId INNER JOIN " +
                      "StreetType ON Street.StreetTypeId = StreetType.StreetTypeId INNER JOIN Unit ON ObjUkg.UnitId = Unit.UnitId " +
                strObjNameSQL +
                "ORDER BY ObjUkg.ObjName";
            SqlCommand cmdObjFnd = new SqlCommand(strSqlTextObjFnd, FrmMain.Instance.m_cnn);
            SqlDataReader rdrObjFnd = cmdObjFnd.ExecuteReader();
            string strObjId = "";
            string strBuildId = "";
            string strObjName = "";
            string strObjTel = "";
            string strOrgName = "";
            string strDirName = "";
            string strDirPhone = "";
            string strDispPhone = "";
            string strBuildName = "";
            string strBuildTypeName = "";
            string strStreetTypeName = "";
            string strStreetName = "";
            string strUnitName = "";
            //string strOrgMonName = "";
            //string strOrgMonTel = "";
            string strStreetId = "";
            //string strOrgObjMonId = "";
            int iCntFndObj = 0;
            string strObjInfo = "";

            while (rdrObjFnd.Read())
            {
                if (!rdrObjFnd.IsDBNull(0))
                    strObjId = rdrObjFnd.GetInt32(0).ToString();
                if (!rdrObjFnd.IsDBNull(1))
                    strBuildId = rdrObjFnd.GetInt32(1).ToString();
                if (!rdrObjFnd.IsDBNull(2))
                    strObjName = rdrObjFnd.GetString(2);
                if (!rdrObjFnd.IsDBNull(3))
                    strObjTel = rdrObjFnd.GetString(3);
                if (!rdrObjFnd.IsDBNull(5))
                    strOrgName = rdrObjFnd.GetString(5);
                if (!rdrObjFnd.IsDBNull(6))
                    strDirName = rdrObjFnd.GetString(6);
                if (!rdrObjFnd.IsDBNull(7))
                    strDirPhone = rdrObjFnd.GetString(7);
                if (!rdrObjFnd.IsDBNull(8))
                    strDispPhone = rdrObjFnd.GetString(8);
                if (!rdrObjFnd.IsDBNull(9))
                    strBuildName = rdrObjFnd.GetString(9);
                if (!rdrObjFnd.IsDBNull(10))
                    strBuildTypeName = rdrObjFnd.GetString(10);
                if (!rdrObjFnd.IsDBNull(11))
                    strStreetTypeName = rdrObjFnd.GetString(11);
                if (!rdrObjFnd.IsDBNull(12))
                    strStreetName = rdrObjFnd.GetString(12);
                if (!rdrObjFnd.IsDBNull(13))
                    strUnitName = rdrObjFnd.GetString(13);
                if (!rdrObjFnd.IsDBNull(14))
                    strStreetId = rdrObjFnd.GetInt32(14).ToString();

                strObjInfo = "Адрес: " + strStreetTypeName + " " + strStreetName + " дом: " + strBuildName + "\r\n" +
                    "тел.: " + strObjTel + " (" + strBuildTypeName + ")" + "\r\n" +
                    "п\\часть: " + strUnitName + "\r\n" +
                    "Организация: " + strOrgName + "\r\n" + strDirName + " тел.: " + strDirPhone;

                object[] items = { strObjName, strBuildId, strStreetId };
                c1FlxGrdObjMap.AddItem(items, iCntFndObj, 1);
                CellNote note = new CellNote(strObjInfo);
                CellRange rg = c1FlxGrdObjMap.GetCellRange(iCntFndObj, 1);
                rg.UserData = note;

                iCntFndObj++;
            }
            rdrObjFnd.Close();
            if (iCntFndObj > 0)
            {
                CellNoteManager mgr = new CellNoteManager(c1FlxGrdObjMap);
                
                c1FlxGrdObjMap.Cols[4].Width = 150;
                // меняет размер таблицы
                int iRwSz = c1FlxGrdObjMap.Rows.DefaultSize;
                int iGrdPosY = c1FlxGrdObjMap.Location.Y;
                iRwSz = iRwSz * iCntFndObj;
                if (iRwSz < l_iCntrSz - iGrdPosY)
                    c1FlxGrdObjMap.Height = iRwSz;
                else
                    c1FlxGrdObjMap.Height = l_iCntrSz - iGrdPosY;

                c1FlxGrdObjMap.Visible = true;
                c1FlxGrdObjMap.Focus();
            }
            else
                c1FlxGrdObjMap.Visible = false;
        }

        private void ObjFindSelectIhfo(double x1, double y1)
        {
            MapXLib.SearchResultTypeConstants SrHm = MapXLib.SearchResultTypeConstants.miSearchResultAll;

            l_SelBuildHm.SelectByPoint(x1, y1, MapXLib.SelectionTypeConstants.miSelectionNew, SrHm);

            l_SelStreetNet.SelectByPoint(x1, y1, MapXLib.SelectionTypeConstants.miSelectionNew, SrHm);

            l_SelWater.SelectByPoint(x1, y1, MapXLib.SelectionTypeConstants.miSelectionNew, SrHm);

            if (l_SelBuildHm.Count > 0)
            {
                l_SelStreetNet.ClearSelection();
                l_SelWater.ClearSelection();
                string strBuildId = l_SelBuildHm._Item(1).KeyValue;
                DataView dvHomeSel = new DataView(dsHomeFullMap.Tables["Buildings"], "BuildId = '" + strBuildId + "'",
                        "BuildName", DataViewRowState.CurrentRows);
                int irowCnt = dvHomeSel.Count;
                if (irowCnt > 0)
                {
                    string strBuildName = dvHomeSel[0]["BuildName"].ToString();
                    string strStreetId = dvHomeSel[0]["StreetId"].ToString();
                    DataView dvStreetSel = new DataView(dsStreetMap.Tables["Street"], "StreetId = '" + strStreetId + "'",
                        "StreetName", DataViewRowState.CurrentRows);
                    irowCnt = dvStreetSel.Count;
                    if (irowCnt > 0)
                    {
                        string strStreetName = dvStreetSel[0]["StreetName"].ToString();
                        string strStreetTypeName = dvStreetSel[0]["StreetTypeName"].ToString();
                        string strMsg = "Дом №" + strBuildName + " " + strStreetTypeName + " " + strStreetName;
                        l_strObjNameMap = strStreetTypeName + " " + strStreetName + ", дом № " + strBuildName;
                        c1SprTooltipMapUkg.Show(strMsg, axMapUkg, l_iPointX, l_iPointY);
                    }
                }
            }
            else
            {

                if (l_SelStreetNet.Count > 0)
                {
                    l_SelBuildHm.ClearSelection();
                    l_SelWater.ClearSelection();
                    string strStreetId = l_SelStreetNet._Item(1).KeyValue;
                    DataView dvStreetSel = new DataView(dsStreetMap.Tables["Street"], "StreetId = '" + strStreetId + "'",
                            "StreetName", DataViewRowState.CurrentRows);
                    int irowCnt = dvStreetSel.Count;
                    if (irowCnt > 0)
                    {
                        string strStreetName = dvStreetSel[0]["StreetName"].ToString();
                        string strStreetTypeName = dvStreetSel[0]["StreetTypeName"].ToString();
                        string strMsg = strStreetTypeName + " " + strStreetName;
                        c1SprTooltipMapUkg.Show(strMsg, axMapUkg, l_iPointX, l_iPointY);
                    }
                }
                else
                {
                    if (l_SelWater.Count > 0)
                    {

                        l_SelStreetNet.ClearSelection();
                        l_SelBuildHm.ClearSelection();

                        string strWaterId = l_SelWater._Item(1).KeyValue;
                        DataView dvWaterSel = new DataView(dsWaterMap.Tables["Water"], "WaterId = '" + strWaterId + "'",
                                "WaterName", DataViewRowState.CurrentRows);
                        int irowCnt = dvWaterSel.Count;
                        if (irowCnt > 0)
                        {
                            string strWaterList = "";
                            string strHomeFull = "";
                            string strHome = "нет данных";
                            string strHome1 = "";
                            string strStreetType = "нет данных";
                            string strStreet = "нет данных";
                            string strWaterName = "нет данных";
                            string strWaterNumber = "нет данных";
                            string strWaterDate = "нет данных";
                            string strWaterVolume = "нет данных";
                            string strWaterPath = "нет данных";
                            string strWaterType = "нет данных";
                            string strWaterLook = "нет данных";
                            string strWaterBore = "нет данных";
                            string strWaterPressure = "нет данных";
                            string strWaterExpense = "нет данных";
                            string strUnitName = "нет данных";
                            string strWaterNote = "нет данных";

                            strWaterName = dvWaterSel[0]["WaterName"].ToString();
                            strWaterNumber = dvWaterSel[0]["WaterNumber"].ToString();
                            strHome = dvWaterSel[0]["WaterHome"].ToString();
                            strHome1 = dvWaterSel[0]["WaterHome1"].ToString();
                            string strDate = dvWaterSel[0]["WaterDate"].ToString();
                            strWaterDate = DateTime.Parse(strDate).ToString("d-MM-yyyy");
                            strWaterVolume = dvWaterSel[0]["WaterVolume"].ToString();
                            strWaterPath = dvWaterSel[0]["WaterPath"].ToString();
                            strWaterType = dvWaterSel[0]["WaterType"].ToString();
                            strWaterLook = dvWaterSel[0]["WaterLook"].ToString();
                            strWaterBore = dvWaterSel[0]["WaterBore"].ToString();
                            strWaterPressure = dvWaterSel[0]["WaterPressure"].ToString();
                            strWaterExpense = dvWaterSel[0]["WaterExpense"].ToString();
                            strWaterNote = dvWaterSel[0]["WaterNote"].ToString();
                            strStreet = dvWaterSel[0]["StreetName"].ToString();
                            strStreetType = dvWaterSel[0]["StreetTypeName"].ToString();
                            strUnitName = dvWaterSel[0]["UnitName"].ToString();

                            if (strHome1 != "")
                                strHomeFull = strHome + "/" + strHome1;
                            else
                                strHomeFull = strHome;

                            strWaterList = strWaterName + " №" + strWaterNumber + ", дом " + strHomeFull + ", " + strStreetType + " " + strStreet +
                                "<br>";
                            if (strWaterDate != "")
                                strWaterList = strWaterList + "Проверен:" + strWaterDate;
                            if (strWaterVolume != "")
                                strWaterList = strWaterList + " Объем:" + strWaterVolume;
                            if (strWaterPath != "")
                                strWaterList = strWaterList + " Подъездных путей:" + strWaterPath;
                            if (strWaterType != "")
                                strWaterList = strWaterList + " Тип:" + strWaterType + "<br>";
                            if (strWaterLook != "")
                                strWaterList = strWaterList + "Вид:" + strWaterLook;
                            if (strWaterBore != "")
                                strWaterList = strWaterList + " Диаметр:" + strWaterBore;
                            if (strWaterPressure != "")
                                strWaterList = strWaterList + " Давление:" + strWaterPressure;
                            if (strWaterExpense != "")
                                strWaterList = strWaterList + " Расход:" + strWaterExpense;
                            if (strUnitName != "")
                                strWaterList = strWaterList + "<br>П\\часть:" + strUnitName;
                            if (strWaterNote != "")
                                strWaterList = strWaterList + "," + strWaterNote;

                            c1SprTooltipMapUkg.Show(strWaterList, axMapUkg, l_iPointX, l_iPointY);
                        }
                    }
                    else
                        c1SprTooltipMapUkg.Hide(axMapUkg);
                }
            }
        }

        private void ObjFindSelectTool(MapXLib.Features fsSearchSelect)
        {
            int iCntFndObj = 0;
            c1FlxGrdObjMap.Visible = false;
            if (c1FlxGrdObjMap.Rows.Count > 0)
                RemoveItemObjMap();

            foreach (MapXLib.Feature ftrObj in fsSearchSelect)
            {
                string strObjName = "";
                string strBuildId = ftrObj.KeyValue;

                bsObjFullMap.Filter = "BuildId = '" + strBuildId + "'";
                int iCntObj = bsObjFullMap.Count;
                if (iCntObj == 0)
                {
                    DataView dvHomeId = new DataView(dsHomeFullMap.Tables["Buildings"], "BuildId = '" + strBuildId + "'",
                        "BuildName", DataViewRowState.CurrentRows);
                    if (dvHomeId.Count == 1)
                    {
                        string strBuildName = dvHomeId[0]["BuildName"].ToString();
                        string strStreetId = dvHomeId[0]["StreetId"].ToString();

                        DataView dvStreetName = new DataView(dsStreetMap.Tables["Street"], "StreetId = '" + strStreetId + "'",
                        "StreetName", DataViewRowState.CurrentRows);

                        string strStreetName = dvStreetName[0]["StreetName"].ToString();
                        string strStreetTypeName = dvStreetName[0]["StreetTypeName"].ToString();
                        strObjName = "Дом №" + strBuildName + ", " + strStreetTypeName + " " + strStreetName;

                        object[] items = { strObjName, strBuildId, strStreetId };
                        c1FlxGrdObjMap.AddItem(items, iCntFndObj, 1);

                        iCntFndObj++;
                    }
                }
                else
                {
                    DataView dvObjName = new DataView(dsObjFullMap.Tables["Buildings"], "BuildId = '" + strBuildId + "'",
                        "ObjName", DataViewRowState.CurrentRows);

                    string strStreetId = dvObjName[0]["StreetId"].ToString();
                    strObjName = dvObjName[0]["ObjName"].ToString() + " (" + dvObjName[0]["OrgName"].ToString() + ")";
                    strObjName = strObjName + " дом №" + dvObjName[0]["BuildName"].ToString();

                    DataView dvStreetName = new DataView(dsStreetMap.Tables["Street"], "StreetId = '" + strStreetId + "'",
                    "StreetName", DataViewRowState.CurrentRows);

                    string strStreetName = dvStreetName[0]["StreetName"].ToString();
                    string strStreetTypeName = dvStreetName[0]["StreetTypeName"].ToString();
                    strObjName = strObjName + ", " + strStreetTypeName + " " + strStreetName;

                    object[] items = { strObjName, strBuildId, strStreetId };
                    c1FlxGrdObjMap.AddItem(items, iCntFndObj, 1);

                    iCntFndObj++;
                }
            }

            if (iCntFndObj > 0)
            {
                //CellNoteManager mgr = new CellNoteManager(c1FlxGrdObjMap);

                c1FlxGrdObjMap.Cols[4].Width = 150;
                // меняет размер таблицы
                int iRwSz = c1FlxGrdObjMap.Rows.DefaultSize;
                int iGrdPosY = c1FlxGrdObjMap.Location.Y;
                iRwSz = iRwSz * iCntFndObj;
                if (iRwSz < l_iCntrSz - iGrdPosY)
                    c1FlxGrdObjMap.Height = iRwSz;
                else
                    c1FlxGrdObjMap.Height = l_iCntrSz - iGrdPosY;


                c1FlxGrdObjMap.Visible = true;

            }
            else
                c1FlxGrdObjMap.Visible = false;
        }

        private void axMapUkg_KeyDownEvent(object sender, AxMapXLib.CMapXEvents_KeyDownEvent e)
        {
            if (e.keyCode == (short)Keys.Escape)
            {
                c1SprTooltipMapUkg.Hide(axMapUkg);
            }
        }
  
    }
}