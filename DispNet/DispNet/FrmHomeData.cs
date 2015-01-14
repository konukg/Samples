using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace DispNet
{
    public partial class FrmHomeData : Form
    {
        static public FrmHomeData Instance;
        public string m_strBuildIdHome = "";
        
        public FrmHomeData()
        {
            InitializeComponent();
        }

        private void FrmHomeData_Load(object sender, EventArgs e)
        {
            //string strBuildIdHome = FrmPermit.Instance.m_strBuildIdMap;

            string strSqlTextHomeData = "SELECT Buildings.BuildId, Buildings.BuildName, Buildings.StreetId, Unit.UnitName, BuildingHome.BuildDivide, " +
                "BuildingHome.BuildPath, BuildingHome.BuildHomeFloor, BuildingHome.BuildEntrance, BuildingHome.BuildApartmen, " +
                "BuildingHome.BuildCellar, BuildingHome.BuildGarret, BuildingHome.BuildRoof, BuildingHome.BuildSwitchBoard, BuildingHome.BuildNote, " +
                "BuildingHome.BuildKskId " +
                "FROM Buildings INNER JOIN Unit ON Buildings.UnitId = Unit.UnitId INNER JOIN BuildingHome ON Buildings.BuildId = BuildingHome.BuildId " +
                "WHERE (Buildings.BuildId = " + m_strBuildIdHome + ")";
            SqlCommand cmdHomeData = new SqlCommand(strSqlTextHomeData, FrmMain.Instance.m_cnn);
            SqlDataReader rdrHomeData = cmdHomeData.ExecuteReader();
            if (rdrHomeData.HasRows)
            {
                txtBoxBuildTypeName.Text = "Жилой дом"; //для всех домов которые не являются объектами
                while (rdrHomeData.Read())
                {
                    //if (!rdrHomeData.IsDBNull(2))
                        //txtBoxBuildTypeName.Text = rdrHomeData.GetString(2);
                    if (!rdrHomeData.IsDBNull(3))
                        txtBoxUnitName.Text = rdrHomeData.GetString(3);
                    if (!rdrHomeData.IsDBNull(4))
                        chkBoxBuildDivide.Checked = rdrHomeData.GetBoolean(4);
                    if (!rdrHomeData.IsDBNull(5))
                        txtBoxBuildPath.Text = rdrHomeData.GetString(5);
                    if (!rdrHomeData.IsDBNull(6))
                        txtBoxFloor.Text = rdrHomeData.GetInt16(6).ToString();
                    if (!rdrHomeData.IsDBNull(7))
                        txtBoxEntrance.Text = rdrHomeData.GetInt16(7).ToString();
                    if (!rdrHomeData.IsDBNull(8))
                        txtBoxApartmen.Text = rdrHomeData.GetInt16(8).ToString();
                    if (!rdrHomeData.IsDBNull(9))
                        chkBoxCellar.Checked = rdrHomeData.GetBoolean(9);
                    if (!rdrHomeData.IsDBNull(10))
                        chkBoxGarret.Checked = rdrHomeData.GetBoolean(10);
                    if (!rdrHomeData.IsDBNull(11))
                        txtBoxRoof.Text = rdrHomeData.GetString(11);
                    if (!rdrHomeData.IsDBNull(12))
                        txtBoxSwitchBoard.Text = rdrHomeData.GetString(12);
                    if (!rdrHomeData.IsDBNull(13))
                        txtBoxBuildNote.Text = rdrHomeData.GetString(13);
                }
            }
            rdrHomeData.Close();
            
        }
    }
}
