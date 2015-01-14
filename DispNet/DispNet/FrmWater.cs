using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;
using C1.Win.C1FlexGrid;

namespace DispNet
{
    public partial class FrmWater : Form
    {
        static public FrmWater Instance;

        DataSet dsWaterBnd = new DataSet();
        BindingSource bsWaterBnd = new BindingSource();

        DataSet dsStreet = new DataSet();
        BindingSource bsStreet = new BindingSource();

        DataSet dsPchAll = new DataSet();
        BindingSource bsPchAll = new BindingSource();

        int iPosStreet = -1;   //запоминает позицию и не дает изменить в сомбо если просто открыть и закрыть
        int iPosUnit = -1;

        public FrmWater()
        {
            InitializeComponent();
        }

        private void FrmWater_Load(object sender, EventArgs e)
        {
            DataSet dsWater = new DataSet();
            BindingSource bsWater = new BindingSource();

            string strSqlTextWater = "SELECT Water.WaterId, Water.WaterName, StreetType.StreetTypeName, Street.StreetName, Water.WaterHome, " +
                "Water.WaterHome1, WaterState.WaterStateName, Water.WaterNumber, Unit.UnitName, Water.WaterDate, Water.WaterType, Water.WaterLook, " +
                "Water.WaterVolume, Water.WaterPath, Water.WaterBore, Water.WaterPressure, Water.WaterExpense, StreetPlace.StreetPlaceName, Water.WaterNote, " +
                "Water.WaterStateId, Water.StreetId, Water.UnitId FROM Water INNER JOIN " +
                      "Street ON Water.StreetId = Street.StreetId INNER JOIN " +
                      "StreetType ON Street.StreetTypeId = StreetType.StreetTypeId INNER JOIN " +
                      "Unit ON Water.UnitId = Unit.UnitId INNER JOIN " +
                      "WaterState ON Water.WaterStateId = WaterState.WaterStateId INNER JOIN " +
                      "StreetPlace ON Street.StreetPlaceId = StreetPlace.StreetPlaceId " +
                "ORDER BY Water.WaterId";
            SqlDataAdapter daWater = new SqlDataAdapter(strSqlTextWater, FrmPermit.Instance.cnn);
            daWater.Fill(dsWater, "Water");
            bsWater.DataSource = dsWater;
            bsWater.DataMember = "Water";
            //делаем копии иначе не связать таблицу с навигатором
            dsWaterBnd = dsWater.Copy();
            bsWaterBnd = new BindingSource(dsWater, "Water");
            //....
            c1DbNvgWater.DataSource = bsWaterBnd;
            c1FlxGrdWater.DataSource = c1DbNvgWater.DataSource;

            c1FlxGrdWater.Cols[8].Visible = false;
            c1FlxGrdWater.Cols[9].Visible = false;
            c1FlxGrdWater.Cols[10].Visible = false;
            c1FlxGrdWater.Cols[11].Visible = false;
            c1FlxGrdWater.Cols[12].Visible = false;
            c1FlxGrdWater.Cols[13].Visible = false;
            c1FlxGrdWater.Cols[14].Visible = false;
            c1FlxGrdWater.Cols[15].Visible = false;
            c1FlxGrdWater.Cols[16].Visible = false;
            c1FlxGrdWater.Cols[17].Visible = false;
            c1FlxGrdWater.Cols[18].Visible = false;
            c1FlxGrdWater.Cols[19].Visible = false;
            c1FlxGrdWater.Cols[20].Visible = false;
            c1FlxGrdWater.Cols[21].Visible = false;
            c1FlxGrdWater.Cols[22].Visible = false;

            FlxGrdWater_Show();

            c1TxtBxPch.DataSource = bsWaterBnd;
            c1TxtBxPch.DataField = "UnitName";
            c1TxtBxDate.DataSource = bsWaterBnd;
            c1TxtBxDate.DataField = "WaterDate";
            c1TxtBxNmb.DataSource = bsWaterBnd;
            c1TxtBxNmb.DataField = "WaterNumber";
            c1TxtBxType.DataSource = bsWaterBnd;
            c1TxtBxType.DataField = "WaterType";
            c1TxtBxLook.DataSource = bsWaterBnd;
            c1TxtBxLook.DataField = "WaterLook";
            c1TxtBxVol.DataSource = bsWaterBnd;
            c1TxtBxVol.DataField = "WaterVolume";
            c1TxtBxPath.DataSource = bsWaterBnd;
            c1TxtBxPath.DataField = "WaterPath";
            c1TxtBxBore.DataSource = bsWaterBnd;
            c1TxtBxBore.DataField = "WaterBore";
            c1TxtBxPres.DataSource = bsWaterBnd;
            c1TxtBxPres.DataField = "WaterPressure";
            c1TxtBxExp.DataSource = bsWaterBnd;
            c1TxtBxExp.DataField = "WaterExpense";
            c1TxtBxPlace.DataSource = bsWaterBnd;
            c1TxtBxPlace.DataField = "StreetPlaceName";
            c1TxtBxNote.DataSource = bsWaterBnd;
            c1TxtBxNote.DataField = "WaterNote";

            string strSqlTextStreet = "SELECT Street.StreetId, Street.StreetName, StreetType.StreetTypeName, StreetPlace.StreetPlaceName, " +
                                        "StreetRegion.StreetRegionName FROM Street " +
                                        "INNER JOIN StreetType ON Street.StreetTypeId = StreetType.StreetTypeId " +
                                        "INNER JOIN StreetPlace ON Street.StreetPlaceId = StreetPlace.StreetPlaceId " +
                                        "INNER JOIN StreetRegion ON Street.StreetRegionId = StreetRegion.StreetRegionId " +
                                        "ORDER BY Street.StreetName";
            SqlDataAdapter daStreet = new SqlDataAdapter(strSqlTextStreet, FrmMain.Instance.m_cnn);
            daStreet.Fill(dsStreet, "Street");

            bsStreet.DataSource = dsStreet;
            bsStreet.DataMember = "Street";
            c1CmbFindStreet.DataSource = bsStreet;
            c1CmbFindStreet.ValueMember = "StreetName";
            c1CmbFindStreet.DisplayMember = "StreetName";

            c1CmbFindStreet.Splits[0].DisplayColumns[0].Visible = false;
            
            c1CmbFindStreet.Columns[1].Caption = "Улица";
            c1CmbFindStreet.Columns[2].Caption = "Тип";
            c1CmbFindStreet.Columns[3].Caption = "Район";
            c1CmbFindStreet.Columns[4].Caption = "Регион";

            c1CmbFindStreet.DropDownWidth = 400;

            string strSqlTextPchAll = "SELECT UnitName, UnitId FROM Unit ORDER BY UnitId";
            SqlDataAdapter daPchAll = new SqlDataAdapter(strSqlTextPchAll, FrmMain.Instance.m_cnn);
            daPchAll.Fill(dsPchAll, "Unit");
            bsPchAll.DataSource = dsPchAll;
            bsPchAll.DataMember = "Unit";
            c1CmbUnit.DataSource = bsPchAll;
            c1CmbUnit.ValueMember = "UnitName";
            c1CmbUnit.DisplayMember = "UnitName";
            c1CmbUnit.Splits[0].DisplayColumns[1].Visible = false;
            c1CmbUnit.Columns[0].Caption = "Подразделение";
            c1CmbUnit.Splits[0].DisplayColumns[0].HeadingStyle.HorizontalAlignment = C1.Win.C1List.AlignHorzEnum.Center;

            c1CmbIdN.DataSource = bsWaterBnd;
            c1CmbIdN.ValueMember = "Water";
            c1CmbIdN.DataMember = "Water";
            c1CmbIdN.Columns[0].Caption = "п/№";
            c1CmbIdN.Splits[0].DisplayColumns[0].HeadingStyle.HorizontalAlignment = C1.Win.C1List.AlignHorzEnum.Center;
            c1CmbIdN.Splits[0].DisplayColumns[1].Visible = false;
            c1CmbIdN.Splits[0].DisplayColumns[2].Visible = false;
            c1CmbIdN.Splits[0].DisplayColumns[3].Visible = false;
            c1CmbIdN.Splits[0].DisplayColumns[4].Visible = false;
            c1CmbIdN.Splits[0].DisplayColumns[5].Visible = false;
            c1CmbIdN.Splits[0].DisplayColumns[6].Visible = false;
            c1CmbIdN.Splits[0].DisplayColumns[7].Visible = false;
            c1CmbIdN.Splits[0].DisplayColumns[8].Visible = false;
            c1CmbIdN.Splits[0].DisplayColumns[9].Visible = false;
            c1CmbIdN.Splits[0].DisplayColumns[10].Visible = false;
            c1CmbIdN.Splits[0].DisplayColumns[11].Visible = false;
            c1CmbIdN.Splits[0].DisplayColumns[12].Visible = false;
            c1CmbIdN.Splits[0].DisplayColumns[13].Visible = false;
            c1CmbIdN.Splits[0].DisplayColumns[14].Visible = false;
            c1CmbIdN.Splits[0].DisplayColumns[15].Visible = false;
            c1CmbIdN.Splits[0].DisplayColumns[16].Visible = false;
            c1CmbIdN.Splits[0].DisplayColumns[17].Visible = false;
            c1CmbIdN.Splits[0].DisplayColumns[18].Visible = false;
            c1CmbIdN.Splits[0].DisplayColumns[19].Visible = false;
            c1CmbIdN.Splits[0].DisplayColumns[20].Visible = false;
            c1CmbIdN.Splits[0].DisplayColumns[21].Visible = false;
            
        }

        private void FlxGrdWater_Show()
        {
            c1FlxGrdWater.Cols[1].Width = 33;
            c1FlxGrdWater.Cols[1].Caption = "п/№";
            c1FlxGrdWater.Cols[1].AllowEditing = false;

            c1FlxGrdWater.Cols[2].Width = 61;
            c1FlxGrdWater.Cols[2].Caption = "Источник";
            c1FlxGrdWater.Cols[2].AllowEditing = false;

            c1FlxGrdWater.Cols[3].Width = 70;
            c1FlxGrdWater.Cols[3].Caption = "Улица";
            c1FlxGrdWater.Cols[3].AllowEditing = false;

            c1FlxGrdWater.Cols[4].Width = 145;
            c1FlxGrdWater.Cols[4].Caption = "Наименование";
            c1FlxGrdWater.Cols[4].AllowEditing = false;

            c1FlxGrdWater.Cols[5].Width = 33;
            c1FlxGrdWater.Cols[5].Caption = "Дом";
            c1FlxGrdWater.Cols[5].AllowEditing = false;

            c1FlxGrdWater.Cols[6].Width = 33;
            c1FlxGrdWater.Cols[6].Caption = "/";
            c1FlxGrdWater.Cols[6].AllowEditing = false;

            c1FlxGrdWater.Cols[7].Width = 70;
            c1FlxGrdWater.Cols[7].Caption = "Состояние";
            c1FlxGrdWater.Cols[7].AllowEditing = false;

            CellStyle cs = c1FlxGrdWater.Styles.Add("s1");
            cs.Font = new Font("Arial", 9, FontStyle.Bold);
            cs.TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.CenterCenter;
            c1FlxGrdWater.Rows[0].Style = cs;
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked)
            {
                if (c1CmbFindStreet.Text != "")
                {
                    int iFndRow = c1CmbFindStreet.FindStringExact(c1CmbFindStreet.Text, 0, 1);
                    string strStreetId = c1CmbFindStreet.Columns[0].CellText(iFndRow);
                    string strFilter = "StreetId = '" + strStreetId + "'";
                    if (c1CmbUnit.Text != "")
                    {
                        iFndRow = c1CmbUnit.FindStringExact(c1CmbUnit.Text, 0, 0);
                        string strUnitId = c1CmbUnit.Columns[1].CellText(iFndRow);
                        strFilter = strFilter + " AND WaterStateId = 1 AND UnitId = '" + strUnitId + "'";
                    }
                    else
                        strFilter = strFilter + " AND WaterStateId = 1";
                    bsWaterBnd.Filter = strFilter;
                }
                else
                {
                    string strFilter = "WaterStateId = 1";
                    if (c1CmbUnit.Text != "")
                    {
                        int iFndRow = c1CmbUnit.FindStringExact(c1CmbUnit.Text, 0, 0);
                        string strUnitId = c1CmbUnit.Columns[1].CellText(iFndRow);
                        strFilter = strFilter + " AND UnitId = '" + strUnitId + "'";
                    }
                    bsWaterBnd.Filter = strFilter;
                }
            }
            else
            {
                bsWaterBnd.RemoveFilter();
                if (c1CmbFindStreet.Text != "")
                {
                    int iFndRow = c1CmbFindStreet.FindStringExact(c1CmbFindStreet.Text, 0, 1);
                    string strStreetId = c1CmbFindStreet.Columns[0].CellText(iFndRow);
                    string strFilter = "StreetId = '" + strStreetId + "'";
                    if (c1CmbUnit.Text != "")
                    {
                        iFndRow = c1CmbUnit.FindStringExact(c1CmbUnit.Text, 0, 0);
                        string strUnitId = c1CmbUnit.Columns[1].CellText(iFndRow);
                        strFilter = strFilter + " AND UnitId = '" + strUnitId + "'";
                    }
                    bsWaterBnd.Filter = strFilter;
                }
                else
                {
                    if (c1CmbUnit.Text != "")
                    {
                        int iFndRow = c1CmbUnit.FindStringExact(c1CmbUnit.Text, 0, 0);
                        string strUnitId = c1CmbUnit.Columns[1].CellText(iFndRow);
                        bsWaterBnd.Filter = "UnitId = '" + strUnitId + "'";
                    }
                }
            }
        }

        private void c1CmbFindStreet_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Delete)
            {
                iPosStreet = -1;
                c1CmbFindStreet.Text = "";
                bsWaterBnd.RemoveFilter();
                if (checkBox1.Checked)
                {
                    string strFilter = "WaterStateId = 1";
                    if (c1CmbUnit.Text != "")
                    {
                        int iFndRow = c1CmbUnit.FindStringExact(c1CmbUnit.Text, 0, 0);
                        string strUnitId = c1CmbUnit.Columns[1].CellText(iFndRow);
                        strFilter = strFilter + " AND UnitId = '" + strUnitId + "'";
                    }
                    bsWaterBnd.Filter = strFilter;
                }
                else
                {
                    if (c1CmbUnit.Text != "")
                    {
                        int iFndRow = c1CmbUnit.FindStringExact(c1CmbUnit.Text, 0, 0);
                        string strUnitId = c1CmbUnit.Columns[1].CellText(iFndRow);
                        bsWaterBnd.Filter = "UnitId = '" + strUnitId + "'";
                    }
                }
                FlxGrdWater_Show();
            }
        }

        private void c1CmbFindStreet_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Return)
            {
                e.Handled = true;
                c1CmbFindStreet.SelectAll();
            }
        }

        private void c1CmbFindStreet_Close(object sender, EventArgs e)
        {
            if (c1CmbFindStreet.Text != "" && iPosStreet != bsStreet.Position && c1CmbFindStreet.ListCount > 0)
            {
                iPosStreet = bsStreet.Position;

                int iFndRow = c1CmbFindStreet.FindStringExact(c1CmbFindStreet.Text, 0, 1);
                string strStreetId = c1CmbFindStreet.Columns[0].CellText(iFndRow);
                string strFilter = "StreetId = '" + strStreetId + "'";
                if (checkBox1.Checked)
                {
                    if (c1CmbUnit.Text != "")
                    {
                        iFndRow = c1CmbUnit.FindStringExact(c1CmbUnit.Text, 0, 0);
                        string strUnitId = c1CmbUnit.Columns[1].CellText(iFndRow);
                        strFilter = strFilter + " AND UnitId = '" + strUnitId + "' AND WaterStateId = 1";
                    }
                    bsWaterBnd.Filter = strFilter;
                }
                else
                {
                    if (c1CmbUnit.Text != "")
                    {
                        iFndRow = c1CmbUnit.FindStringExact(c1CmbUnit.Text, 0, 0);
                        string strUnitId = c1CmbUnit.Columns[1].CellText(iFndRow);
                        strFilter = strFilter + " AND UnitId = '" + strUnitId + "'";
                    }
                    bsWaterBnd.Filter = strFilter;
                }
                FlxGrdWater_Show();
            }
        }

        private void c1CmbUnit_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Delete)
            {
                iPosUnit = -1;
                c1CmbUnit.Text = "";
                bsWaterBnd.RemoveFilter();
                string strFilter = "";
                if (checkBox1.Checked)
                {
                    if (c1CmbFindStreet.Text != "")
                    {
                        int iFndRow = c1CmbFindStreet.FindStringExact(c1CmbFindStreet.Text, 0, 1);
                        string strStreetId = c1CmbFindStreet.Columns[0].CellText(iFndRow);
                        strFilter = "WaterStateId = 1 AND StreetId = '" + strStreetId + "'";
                    }
                    else
                        strFilter = "WaterStateId = 1";
                    bsWaterBnd.Filter = strFilter;
                }
                else
                {
                    if (c1CmbFindStreet.Text != "")
                    {
                        int iFndRow = c1CmbFindStreet.FindStringExact(c1CmbFindStreet.Text, 0, 1);
                        string strStreetId = c1CmbFindStreet.Columns[0].CellText(iFndRow);
                        strFilter = "StreetId = '" + strStreetId + "'";
                        bsWaterBnd.Filter = strFilter;
                    }
                }
                FlxGrdWater_Show();
            }
        }

        private void c1CmbUnit_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Return)
            {
                e.Handled = true;
                c1CmbUnit.SelectAll();
            }
        }

        private void c1CmbUnit_Close(object sender, EventArgs e)
        {
            if (c1CmbUnit.Text != "" && iPosUnit != bsPchAll.Position && c1CmbUnit.ListCount > 0)    //только если есть записи иначе ошибка
            {
                iPosUnit = bsPchAll.Position;
                int iFndRow = c1CmbUnit.FindStringExact(c1CmbUnit.Text, 0, 0);
                string strUnitId = c1CmbUnit.Columns[1].CellText(iFndRow);
                
                string strFilter = "";
                if (checkBox1.Checked)
                {
                    if (c1CmbFindStreet.Text != "")
                    {
                        iFndRow = c1CmbFindStreet.FindStringExact(c1CmbFindStreet.Text, 0, 1);
                        string strStreetId = c1CmbFindStreet.Columns[0].CellText(iFndRow);
                        strFilter = "WaterStateId = 1 AND StreetId = '" + strStreetId + "' AND UnitId = '" + strUnitId + "'";
                    }
                    else
                        strFilter = "WaterStateId = 1 AND UnitId = '" + strUnitId + "'";
                    bsWaterBnd.Filter = strFilter;
                }
                else
                {
                    if (c1CmbFindStreet.Text != "")
                    {
                        iFndRow = c1CmbFindStreet.FindStringExact(c1CmbFindStreet.Text, 0, 1);
                        string strStreetId = c1CmbFindStreet.Columns[0].CellText(iFndRow);
                        strFilter = "StreetId = '" + strStreetId + "' AND UnitId = '" + strUnitId + "'";
                    }
                    else
                        strFilter = "UnitId = '" + strUnitId + "'";
                    bsWaterBnd.Filter = strFilter;
                }
                FlxGrdWater_Show();
            }
        }
    }
}
