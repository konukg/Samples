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
    public partial class FrmStreet : Form
    {
        static public FrmStreet Instance;

        DataSet dsStreetBnd = new DataSet();
        BindingSource bsStreetBnd = new BindingSource();

        DataSet dsStreetDiv = new DataSet();
        BindingSource bsStreetDiv = new BindingSource();

        DataSet dsStrPlace = new DataSet();
        BindingSource bsStrPlace = new BindingSource();

        DataSet dsStrReg = new DataSet();
        BindingSource bsStrReg = new BindingSource();

        int iPosPlace = -1;   //запоминает позицию и не дает изменить в сомбо если просто открыть и закрыть
        int iPosReg = -1;
        
        public FrmStreet()
        {
            InitializeComponent();
        }

        private void FrmStreet_Load(object sender, EventArgs e)
        {
            DataSet dsStreet = new DataSet();
            BindingSource bsStreet = new BindingSource();

            string strSqlTextStreet = "SELECT Street.StreetId, StreetType.StreetTypeName, Street.StreetName, StreetPlace.StreetPlaceName, " +
                                        "StreetRegion.StreetRegionName, Unit.UnitName, Street.StreetDivide, Street.StreetNote, StreetPlace.StreetPlaceId, " +
                                        "StreetRegion.StreetRegionId FROM Street " +
                                        "INNER JOIN StreetType ON Street.StreetTypeId = StreetType.StreetTypeId " +
                                        "INNER JOIN StreetPlace ON Street.StreetPlaceId = StreetPlace.StreetPlaceId " +
                                        "INNER JOIN StreetRegion ON Street.StreetRegionId = StreetRegion.StreetRegionId " +
                                        "INNER JOIN Unit ON Street.UnitId = Unit.UnitId " +
                                        "ORDER BY Street.StreetName";
            SqlDataAdapter daStreet = new SqlDataAdapter(strSqlTextStreet, FrmMain.Instance.m_cnn);
            daStreet.Fill(dsStreet, "Street");
            bsStreet.DataSource = dsStreet;
            bsStreet.DataMember = "Street";
            //делаем копии иначе не связать таблицу с навигатором
            dsStreetBnd = dsStreet.Copy();
            bsStreetBnd = new BindingSource(dsStreet, "Street");
            //....
            c1DbNvgStreet.DataSource = bsStreetBnd;
            c1FlxGrdStreet.DataSource = c1DbNvgStreet.DataSource;

            c1FlxGrdStreet.Cols[1].Visible = false;
            c1FlxGrdStreet.Cols[5].Visible = false;
            c1FlxGrdStreet.Cols[6].Visible = false;
            c1FlxGrdStreet.Cols[7].Visible = false;
            c1FlxGrdStreet.Cols[8].Visible = false;
            c1FlxGrdStreet.Cols[9].Visible = false;
            c1FlxGrdStreet.Cols[10].Visible = false;

            c1FlxGrdStreet.Cols[2].Width = 75;
            c1FlxGrdStreet.Cols[2].Caption = "Улица";
            c1FlxGrdStreet.Cols[2].AllowEditing = false;

            c1FlxGrdStreet.Cols[3].Width = 145;
            c1FlxGrdStreet.Cols[3].Caption = "Наименование";
            c1FlxGrdStreet.Cols[3].AllowEditing = false;

            c1FlxGrdStreet.Cols[4].Width = 100;
            c1FlxGrdStreet.Cols[4].Caption = "Место";
            c1FlxGrdStreet.Cols[4].AllowEditing = false;

            CellStyle cs = c1FlxGrdStreet.Styles.Add("s1");
            cs.Font = new Font("Arial", 9, FontStyle.Bold);
            cs.TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.CenterCenter;
            c1FlxGrdStreet.Rows[0].Style = cs;

            c1TxtBxStId.DataSource = bsStreetBnd;
            c1TxtBxStId.DataField = "StreetId";

            c1TxtBxReg.DataSource = bsStreetBnd;
            c1TxtBxReg.DataField = "StreetRegionName";

            c1TxtBxUnit.DataSource = bsStreetBnd;
            c1TxtBxUnit.DataField = "UnitName";

            c1TxtBxNote.DataSource = bsStreetBnd;
            c1TxtBxNote.DataField = "StreetNote";

            string strSqlTextStreetDiv = "SELECT StreetDivide.DivideHomeFr, StreetDivide.DivideHomeLs, Unit.UnitName, StreetDivide.StreetId " +
                                      "FROM StreetDivide INNER JOIN Unit ON StreetDivide.UnitId = Unit.UnitId " +
                                      "ORDER BY StreetDivide.DivideHomeFr";
            SqlDataAdapter daStreetDiv = new SqlDataAdapter(strSqlTextStreetDiv, FrmPermit.Instance.cnn);
            daStreetDiv.Fill(dsStreetDiv, "StreetDivide");
            bsStreetDiv.DataSource = dsStreetDiv;
            bsStreetDiv.DataMember = "StreetDivide";

            c1FlxGrdStrDv.DataSource = bsStreetDiv;
            c1FlxGrdStrDv.DataMember = "StreetDivide";

            c1FlxGrdStrDv.Cols[1].Width = 35;
            c1FlxGrdStrDv.Cols[1].Caption = "От";
            c1FlxGrdStrDv.Cols[1].AllowEditing = false;

            c1FlxGrdStrDv.Cols[2].Width = 35;
            c1FlxGrdStrDv.Cols[2].Caption = "До";
            c1FlxGrdStrDv.Cols[2].AllowEditing = false;

            c1FlxGrdStrDv.Cols[3].Width = 100;
            c1FlxGrdStrDv.Cols[3].Caption = "П/часть";
            c1FlxGrdStrDv.Cols[3].AllowEditing = false;

            c1FlxGrdStrDv.Cols[4].Visible = false;
            CellStyle csDiv = c1FlxGrdStrDv.Styles.Add("s1");
            csDiv.Font = new Font("Arial", 9, FontStyle.Bold);
            csDiv.TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.CenterCenter;
            c1FlxGrdStrDv.Rows[0].Style = csDiv;
            c1FlxGrdStrDv.Visible = false;

            FindStreetNameLoad();

            this.bsStreetBnd.PositionChanged += new EventHandler(bsStreetBnd_PositionChanged);

            c1FlxGrdStreet.SelectionMode = SelectionModeEnum.Row;
            c1FlxGrdStreet.Select(1, 1);
        }

        private void FindStreetNameLoad()
        {
            c1CmbFindStreet.DataSource = bsStreetBnd;
            c1CmbFindStreet.ValueMember = "StreetName";
            c1CmbFindStreet.DisplayMember = "StreetName";

            c1CmbFindStreet.Splits[0].DisplayColumns[0].Visible = false;

            c1CmbFindStreet.Splits[0].DisplayColumns[1].Width = 100;
            c1CmbFindStreet.Columns[1].Caption = "Улица";
            c1CmbFindStreet.Splits[0].DisplayColumns[2].Width = 120;
            c1CmbFindStreet.Columns[2].Caption = "Название";

            c1CmbFindStreet.Splits[0].DisplayColumns[3].Visible = false;
            c1CmbFindStreet.Splits[0].DisplayColumns[4].Visible = false;

            c1CmbFindStreet.Splits[0].DisplayColumns[5].Visible = false;
            c1CmbFindStreet.Splits[0].DisplayColumns[6].Visible = false;
            c1CmbFindStreet.Splits[0].DisplayColumns[7].Visible = false;
            c1CmbFindStreet.Splits[0].DisplayColumns[8].Visible = false;
            c1CmbFindStreet.Splits[0].DisplayColumns[9].Visible = false;

            c1CmbFindStreet.DropDownWidth = 250;
        }

        private void FindStreetPlaceLoad()
        {
            string strSqlTextStrPlace = "SELECT StreetPlaceId, StreetPlaceName " +
                                        "FROM StreetPlace ORDER BY StreetPlaceName";
            SqlDataAdapter daStrPlace = new SqlDataAdapter(strSqlTextStrPlace, FrmMain.Instance.m_cnn);
            daStrPlace.Fill(dsStrPlace, "StreetPlace");
            bsStrPlace.DataSource = dsStrPlace;
            bsStrPlace.DataMember = "StreetPlace";
                                 
            c1CmbFindStreet.DataSource = bsStrPlace;
            c1CmbFindStreet.ValueMember = "StreetPlaceName";
            c1CmbFindStreet.DisplayMember = "StreetPlaceName";

            c1CmbFindStreet.Splits[0].DisplayColumns[0].Visible = false;

            c1CmbFindStreet.Splits[0].DisplayColumns[1].Width = 100;
            c1CmbFindStreet.Columns[1].Caption = "Место";
            
            c1CmbFindStreet.DropDownWidth = 150;
        }

        private void FindStreetRegLoad()
        {
            string strSqlTextStrReg = "SELECT StreetRegionId, StreetRegionName " +
                                        "FROM StreetRegion ORDER BY StreetRegionName";
            SqlDataAdapter daStrReg = new SqlDataAdapter(strSqlTextStrReg, FrmMain.Instance.m_cnn);
            daStrReg.Fill(dsStrReg, "StreetRegion");
            bsStrReg.DataSource = dsStrReg;
            bsStrReg.DataMember = "StreetRegion";

            c1CmbFindStreet.DataSource = bsStrReg;
            c1CmbFindStreet.ValueMember = "StreetRegionName";
            c1CmbFindStreet.DisplayMember = "StreetRegionName";

            c1CmbFindStreet.Splits[0].DisplayColumns[0].Visible = false;

            c1CmbFindStreet.Splits[0].DisplayColumns[1].Width = 100;
            c1CmbFindStreet.Columns[1].Caption = "Регион";

            c1CmbFindStreet.DropDownWidth = 150;
        }

        private void bsStreetBnd_PositionChanged(Object sender, EventArgs e)
        {
            DataRow drStreet = dsStreetBnd.Tables["Street"].Rows[bsStreetBnd.Position];

            bool blStreetDiv = Convert.ToBoolean(drStreet["StreetDivide"].ToString());
            
            if (blStreetDiv)
            {
                chkBoxStDiv.Checked = true;
                string strStreetId = drStreet["StreetId"].ToString();
                bsStreetDiv.Filter = "StreetId = '" + strStreetId + "'";
                c1FlxGrdStrDv.Visible = true;
            }
            else
            {
                c1FlxGrdStrDv.Visible = false;
                chkBoxStDiv.Checked = false;
            }
        }

        private void rdBtnStrNm_CheckedChanged(object sender, EventArgs e)
        {
            if (rdBtnStrNm.Checked)
            {
                bsStreetBnd.RemoveFilter();
                FindStreetNameLoad();
            }
        }

        private void rdBtnPlace_CheckedChanged(object sender, EventArgs e)
        {
            if (rdBtnPlace.Checked)
            {
                iPosPlace = -1;
                bsStreetBnd.RemoveFilter();
                FindStreetPlaceLoad();
            }
        }

        private void rdBtnReg_CheckedChanged(object sender, EventArgs e)
        {
            if (rdBtnReg.Checked)
            {
                iPosReg = -1;
                bsStreetBnd.RemoveFilter();
                FindStreetRegLoad();
            }
        }

        private void c1CmbFindStreet_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Delete)
            {
                bsStreetBnd.RemoveFilter();
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
            if (rdBtnPlace.Checked)
            {
                if (c1CmbFindStreet.Text != "" && iPosPlace != bsStrPlace.Position && c1CmbFindStreet.ListCount > 0)    //только если есть записи иначе ошибка
                {
                    iPosPlace = bsStrPlace.Position;
                    int iFndRow = c1CmbFindStreet.FindStringExact(c1CmbFindStreet.Text, 0, 1);
                    string strPlace = c1CmbFindStreet.Columns[0].CellText(iFndRow);
                    bsStreetBnd.Filter = "StreetPlaceId = '" + strPlace + "'";
                }
            }

            if (rdBtnReg.Checked)
            {
                if (c1CmbFindStreet.Text != "" && iPosReg != bsStrReg.Position && c1CmbFindStreet.ListCount > 0)    //только если есть записи иначе ошибка
                {
                    iPosReg = bsStrReg.Position;
                    int iFndRow = c1CmbFindStreet.FindStringExact(c1CmbFindStreet.Text, 0, 1);
                    string strReg = c1CmbFindStreet.Columns[0].CellText(iFndRow);
                    bsStreetBnd.Filter = "StreetRegionId = '" + strReg + "'";
                }
            }
        }

    }
}
