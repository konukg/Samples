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
    public partial class FrmObjUkgData : Form
    {
        static public FrmObjUkgData Instance;
        public string m_strBuildIdObj = "";

        public FrmObjUkgData()
        {
            InitializeComponent();
        }

        private void FrmObjUkgData_Load(object sender, EventArgs e)
        {
            //string strBuildIdObj = FrmPermit.Instance.m_strBuildIdMap;

            string strSqlTextObjData = "SELECT ObjUkg.BuildId, ObjUkg.ObjStory, ObjUkg.ObjFlat, ObjUkg.ObjName, ObjUkg.ObjActiv, ObjUkg.ObjDepart, " +
                "ObjUkg.ObjSign, ObjUkg.ObjTel, ObjUkg.ObjClass, ObjUkg.ObjGroup, ObjUkg.ObjDist, Unit.UnitName, ObjUkg.ObjDate, ObjUkg.PtoType, " +
                "ObjUkg.ObjStaff, ObjUkg.ObjAuto, ObjUkg.AutoWater, ObjUkg.AutoPairs, ObjUkg.AutoGas, ObjUkg.AutoFoam, ObjUkg.ObjPoison, ObjUkg.ObjWater, " +
                "ObjUkg.WaterCount, ObjUkg.WaterRsrvCount, ObjUkg.WaterCockCount, ObjUkg.ExtraCard, ObjUkg.ExtraClass, ObjUkg.ExtraStorey, ObjUkg.ExtraArea, " +
                "ObjUkg.ExtraGarret, ObjUkg.ExtraVault, ObjUkg.ExtraWall, ObjUkg.ExtraFloor, ObjUkg.ExtraHeating, ObjUkg.AutoPowder, ObjUkg.AutoCombined, " +
                "ObjUkg.ObjtNote, ObjUkgStaff.DirName, ObjUkgStaff.DirPhone, ObjUkgStaff.DispPhone, ObjUkgStaff.OrgName, ObjUkgStaff.OrgNote, " +
                "ObjUkg.ObjAutoMon " +
                "FROM ObjUkg INNER JOIN ObjUkgStaff ON ObjUkg.ObjStaffId = ObjUkgStaff.ObjStaffId INNER JOIN Unit ON ObjUkg.UnitId = Unit.UnitId " +
                "WHERE (ObjUkg.BuildId = " + m_strBuildIdObj + ")";
            SqlCommand cmdObjData = new SqlCommand(strSqlTextObjData, FrmMain.Instance.m_cnn);
            SqlDataReader rdrObjData = cmdObjData.ExecuteReader();
            if (rdrObjData.HasRows)
            {
                while (rdrObjData.Read())
                {
                    if (!rdrObjData.IsDBNull(1))
                        txtBoxObjStorey.Text = rdrObjData.GetString(1);
                    if (!rdrObjData.IsDBNull(2))
                        txtBoxFlat.Text = rdrObjData.GetString(2);
                    if (!rdrObjData.IsDBNull(3))
                        txtBoxObjName.Text = rdrObjData.GetString(3);
                    if (!rdrObjData.IsDBNull(4))
                        txtBoxObjActiv.Text = rdrObjData.GetString(4);
                    if (!rdrObjData.IsDBNull(5))
                        txtBoxObjDepart.Text = rdrObjData.GetString(5);
                    if (!rdrObjData.IsDBNull(6))
                        chkBoxObjSig.Checked = rdrObjData.GetBoolean(6);
                    if (!rdrObjData.IsDBNull(7))
                        txtBoxObjTel.Text = rdrObjData.GetString(7);
                    if (!rdrObjData.IsDBNull(8))
                        txtBoxObjClass.Text = rdrObjData.GetString(8);
                    if (!rdrObjData.IsDBNull(9))
                        txtBoxObjGroup.Text = rdrObjData.GetInt16(9).ToString();
                    if (!rdrObjData.IsDBNull(10))
                        txtBoxObjDist.Text = rdrObjData.GetInt32(10).ToString();
                    if (!rdrObjData.IsDBNull(11))
                        txtBoxUnitName.Text = rdrObjData.GetString(11);
                    if (!rdrObjData.IsDBNull(12))
                        txtBoxObjDate.Text = rdrObjData.GetDateTime(12).ToString("dd.MM.yyyy");
                    if (!rdrObjData.IsDBNull(13))
                        txtBoxPtoType.Text = rdrObjData.GetString(13);
                    if (!rdrObjData.IsDBNull(14))
                        txtBoxObjStaff.Text = rdrObjData.GetString(14);
                    if (!rdrObjData.IsDBNull(15))
                        chkBoxObjAuto.Checked = rdrObjData.GetBoolean(15);
                    if (!rdrObjData.IsDBNull(16))
                        chkBoxAutoWater.Checked = rdrObjData.GetBoolean(16);
                    if (!rdrObjData.IsDBNull(17))
                        chkBoxAutoPairs.Checked = rdrObjData.GetBoolean(17);
                    if (!rdrObjData.IsDBNull(18))
                        chkBoxAutoGas.Checked = rdrObjData.GetBoolean(18);
                    if (!rdrObjData.IsDBNull(19))
                        chkBoxAutoFoam.Checked = rdrObjData.GetBoolean(19);
                    if (!rdrObjData.IsDBNull(20))
                        txtBoxObjPoison.Text = rdrObjData.GetString(20);
                    if (!rdrObjData.IsDBNull(21))
                        txtBoxObjWater.Text = rdrObjData.GetString(21);
                    if (!rdrObjData.IsDBNull(22))
                        txtBoxWaterCount.Text = rdrObjData.GetInt16(22).ToString();
                    if (!rdrObjData.IsDBNull(23))
                        txtBoxWaterRsrvCount.Text = rdrObjData.GetInt16(23).ToString();
                    if (!rdrObjData.IsDBNull(24))
                        txtBoxWaterCockCount.Text = rdrObjData.GetInt16(24).ToString();
                    if (!rdrObjData.IsDBNull(25))
                        txtBoxExtraCard.Text = rdrObjData.GetInt32(25).ToString();
                    if (!rdrObjData.IsDBNull(26))
                        txtBoxExtraClass.Text = rdrObjData.GetInt16(26).ToString();
                    if (!rdrObjData.IsDBNull(27))
                        txtBoxExtraStorey.Text = rdrObjData.GetInt16(27).ToString();
                    if (!rdrObjData.IsDBNull(28))
                        txtBoxExtraArea.Text = rdrObjData.GetInt32(28).ToString();
                    if (!rdrObjData.IsDBNull(29))
                        chkBoxExtraGarret.Checked = rdrObjData.GetBoolean(29);
                    if (!rdrObjData.IsDBNull(30))
                        chkBoxExtraVault.Checked = rdrObjData.GetBoolean(30);
                    if (!rdrObjData.IsDBNull(31))
                        txtBoxExtraWall.Text = rdrObjData.GetString(31);
                    if (!rdrObjData.IsDBNull(32))
                        txtBoxExtraFloor.Text = rdrObjData.GetString(32);
                    if (!rdrObjData.IsDBNull(33))
                        txtBoxExtraHeating.Text = rdrObjData.GetString(33);
                    if (!rdrObjData.IsDBNull(34))
                        chkBoxAutoPowder.Checked = rdrObjData.GetBoolean(34);
                    if (!rdrObjData.IsDBNull(35))
                        chkBoxAutoCombined.Checked = rdrObjData.GetBoolean(35);
                    if (!rdrObjData.IsDBNull(36))
                        txtBoxObjtNote.Text = rdrObjData.GetString(36);
                    if (!rdrObjData.IsDBNull(37))
                        txtBoxDirName.Text = rdrObjData.GetString(37);
                    if (!rdrObjData.IsDBNull(38))
                        txtBoxDirPhone.Text = rdrObjData.GetString(38);
                    if (!rdrObjData.IsDBNull(39))
                        txtBoxDispPhone.Text = rdrObjData.GetString(39);
                    if (!rdrObjData.IsDBNull(40))
                        txtBoxOrgName.Text = rdrObjData.GetString(40);
                    if (!rdrObjData.IsDBNull(41))
                        txtBoxOrgNote.Text = rdrObjData.GetString(41);
                    if (!rdrObjData.IsDBNull(42))
                        chkBoxMonObj.Checked = rdrObjData.GetBoolean(42);
                }
            }
            rdrObjData.Close();
        }
    }
}
