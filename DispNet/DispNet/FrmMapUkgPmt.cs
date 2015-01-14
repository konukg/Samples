using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace DispNet
{
    public partial class FrmMapUkgPmt : Form
    {
        static public FrmMapUkgPmt Instance;
        MapXLib.Layer m_lyrBuildHm, m_lyrStreetNet, m_lyrWater;
        int iPathMap = 0;
        public bool m_bWaterShow = false;   //показывает водоисточники и дом в главном окне
        public string m_strBuildIdShow;     //номер (Id) дома в главном окне

        public FrmMapUkgPmt()
        {
            InitializeComponent();
        }

        private void btnTest_Click(object sender, EventArgs e)
        {
            //axMap1.CurrentTool = MapXLib.ToolConstants.miSelectTool;
            SortedList<double, int> srWater = new SortedList<double, int>();

            srWater = FindWaterPmt("622");
        }

        public SortedList<double, int> FindWaterPmt(string strHome)
        {
            MapXLib.FindFeature fndFeat;
            SortedList<double, int> srMas = new SortedList<double, int>();
            //axMap1.SelectionStyle.RegionColor = 255;
            if (iPathMap > 0 && strHome.Length > 0)
            {
                fndFeat = m_lyrBuildHm.Find.Search(strHome, null);
                if (fndFeat.FindRC % 10 == 1)
                {
                    axMap1.CenterX = fndFeat.CenterX;
                    axMap1.CenterY = fndFeat.CenterY;
                    axMap1.Zoom = 1;
                    m_lyrWater.Selection.SelectByRadius(fndFeat.CenterX, fndFeat.CenterY, 0.5, MapXLib.SelectionTypeConstants.miSelectionNew);
                    try
                    {
                        foreach (MapXLib.Feature ftr1 in m_lyrWater.Selection)
                        {
                            double d = 1000 * axMap1.Distance(fndFeat.CenterX, fndFeat.CenterY, ftr1.CenterX, ftr1.CenterY);
                            string strKeyV = ftr1.KeyValue;
                            int iId = 0;
                            if (strKeyV != "")
                            {
                                iId = int.Parse(strKeyV);
                                srMas.Add(d, iId);
                            }
                        }
                    }
                    catch (ArgumentException)
                    {
                        Console.WriteLine("An element with Key = \"txt\" already exists.");
                    }
                    axMap1.CenterX = fndFeat.CenterX;
                    axMap1.CenterY = fndFeat.CenterY;
                    axMap1.Zoom = 1;
                }
            }
            return srMas;
        }

        private void frmMapUkgPmt_Load(object sender, EventArgs e)
        {
            //bckgrndWrkMapPmt.RunWorkerAsync();
            string strPath = FrmMain.Instance.m_strPathMapUkg;
            iPathMap = strPath.Length;
            if (iPathMap > 0)
            {
                string strPathFull;

                strPathFull = strPath + "StreetNet.tab";
                m_lyrStreetNet = axMap1.Layers.Add(strPathFull, null);
                m_lyrStreetNet.LabelProperties.Style.TextFontHalo = true;

                strPathFull = strPath + "BuildHm.tab";
                m_lyrBuildHm = axMap1.Layers.Add(strPathFull, null);
                m_lyrBuildHm.LabelProperties.Style.TextFontHalo = true;
                m_lyrBuildHm.Visible = false;

                strPathFull = strPath + "Water.tab";
                m_lyrWater = axMap1.Layers.Add(strPathFull, null);
                m_lyrWater.LabelProperties.Style.TextFontHalo = true;
                m_lyrWater.LabelProperties.Position = MapXLib.PositionConstants.miPositionTC;
                m_lyrWater.Visible = false;

                axMap1.CenterX = 0.477122847749695;
                axMap1.CenterY = 0.393911691731262;
                axMap1.Zoom = 23.66;
            }
            //frmMapUkgPmt.Instance.Show();
        }

        private void axMap1_MapViewChanged(object sender, EventArgs e)
        {
            if (axMap1.Zoom < 6)
            {
                //m_lyrStreetNet.AutoLabel = true;
                m_lyrBuildHm.Visible = true;
            }
            else
            {
                //m_lyrStreetNet.AutoLabel = false;
                m_lyrBuildHm.Visible = false;
            }

            if (axMap1.Zoom < 4)
            {
                m_lyrStreetNet.AutoLabel = true;
                //m_lyrBuildHm.Visible = true;
            }
            else
            {
                m_lyrStreetNet.AutoLabel = false;
                //m_lyrBuildHm.Visible = false;
            }

            if (axMap1.Zoom < 1.2)
            {
                m_lyrBuildHm.AutoLabel = true;
                m_lyrWater.Visible = true;
            }
            else
            {
                m_lyrBuildHm.AutoLabel = false;
                m_lyrWater.Visible = false;
            }

            if (axMap1.Zoom < 0.5)
            {
                
                m_lyrWater.AutoLabel = true;
            }
            else
            {
                
                m_lyrWater.AutoLabel = false;
            }
        }

        private void bckgrndWrkMapPmt_DoWork(object sender, DoWorkEventArgs e)
        {
            string strPath = FrmMain.Instance.m_strPathMapUkg;
            iPathMap = strPath.Length;
            if (iPathMap > 0)
            {
                string strPathFull;

                strPathFull = strPath + "StreetNet.tab";
                m_lyrStreetNet = axMap1.Layers.Add(strPathFull, null);
                m_lyrStreetNet.LabelProperties.Style.TextFontHalo = true;

                strPathFull = strPath + "BuildHm.tab";
                m_lyrBuildHm = axMap1.Layers.Add(strPathFull, null);
                m_lyrBuildHm.LabelProperties.Style.TextFontHalo = true;
                m_lyrBuildHm.Visible = false;

                strPathFull = strPath + "Water.tab";
                m_lyrWater = axMap1.Layers.Add(strPathFull, null);
                m_lyrWater.LabelProperties.Style.TextFontHalo = true;
                m_lyrWater.LabelProperties.Position = MapXLib.PositionConstants.miPositionTC;
                m_lyrWater.Visible = false;

                axMap1.CenterX = 0.477122847749695;
                axMap1.CenterY = 0.393911691731262;
                axMap1.Zoom = 23.66;
            }
        }

        private void frmMapUkgPmt_Resize(object sender, EventArgs e)
        {
            Control control = (Control)sender;

            axMap1.Height = control.Size.Height;
            axMap1.Width = control.Size.Width;
        }

        private void frmMapUkgPmt_Activated(object sender, EventArgs e)
        {
            if (m_bWaterShow)
            {
                SortedList<double, int> srWater = new SortedList<double, int>();

                srWater = FindWaterPmt(m_strBuildIdShow);
                m_bWaterShow = false;
            }
        }

        
    }
}