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
    public partial class FrmObjFind : Form
    {
        static public FrmObjFind Instance;
        DataSet dsStreetObjFnd = new DataSet();
        BindingSource bsStreetObjFnd = new BindingSource();
        DataSet dsObjFnd = new DataSet();
        BindingSource bsObjFnd = new BindingSource();
        DataSet dsOrgFnd = new DataSet();
        BindingSource bsOrgFnd = new BindingSource();
        DataSet dsPlaceFnd = new DataSet();
        BindingSource bsPlaceFnd = new BindingSource();
        int l_iTypeFnd = 0; //тип поиска 1-улица, 2-район, 3-имя объекта
        public string m_strObjFndId = "";    // Id объекта для отображения на карте
        bool blFullTable;   //true - заполняет в таблицу полные данные

        public FrmObjFind()
        {
            InitializeComponent();
        }

        private void FrmObjFind_Load(object sender, EventArgs e)
        {
            c1CmbOrg.Enabled = false;
            dsStreetObjFnd = FrmPermit.Instance.dsStreet;
            bsStreetObjFnd.DataSource = dsStreetObjFnd;
            bsStreetObjFnd.DataMember = "Street";
            l_iTypeFnd = 1;
            StreetObjLoad();

            string strSqlTextPlaceFnd = "SELECT StreetPlaceId, StreetPlaceName " +
                "FROM StreetPlace ORDER BY StreetPlaceName";
            SqlDataAdapter daPlaceFnd = new SqlDataAdapter(strSqlTextPlaceFnd, FrmMain.Instance.m_cnn);
            daPlaceFnd.Fill(dsPlaceFnd, "StreetPlace");

            bsPlaceFnd.DataSource = dsPlaceFnd;
            bsPlaceFnd.DataMember = "StreetPlace";

            string strSqlTextObjFnd = "SELECT Buildings.BuildId, ObjUkg.ObjName, ObjUkgStaff.OrgName, StreetType.StreetTypeName, Street.StreetName, " +
               "Buildings.BuildName, StreetPlace.StreetPlaceName, Street.StreetId " +
               "FROM Buildings INNER JOIN " +
                     "ObjUkg ON Buildings.BuildId = ObjUkg.BuildId INNER JOIN " +
                     "ObjUkgStaff ON ObjUkg.ObjStaffId = ObjUkgStaff.ObjStaffId INNER JOIN " +
                     "Street ON Buildings.StreetId = Street.StreetId INNER JOIN " +
                     "StreetType ON Street.StreetTypeId = StreetType.StreetTypeId INNER JOIN " +
                     "StreetPlace ON Street.StreetPlaceId = StreetPlace.StreetPlaceId " +
               "ORDER BY ObjUkg.ObjName";
            SqlDataAdapter daObjFnd = new SqlDataAdapter(strSqlTextObjFnd, FrmMain.Instance.m_cnn);
            daObjFnd.Fill(dsObjFnd, "Buildings");

            bsObjFnd.DataSource = dsObjFnd;
            bsObjFnd.DataMember = "Buildings";

            string strSqlTextOrgFnd = "SELECT DISTINCT OrgName FROM ObjUkgStaff ORDER BY OrgName";
            SqlDataAdapter daOrgFnd = new SqlDataAdapter(strSqlTextOrgFnd, FrmMain.Instance.m_cnn);
            daOrgFnd.Fill(dsOrgFnd, "ObjUkgStaff");

            bsOrgFnd.DataSource = dsOrgFnd;
            bsOrgFnd.DataMember = "ObjUkgStaff";

            ObjTableLoad();
            lblOrgReg.Text = "";
           
        }

        private void ObjFilterLoad(string strFilter, bool blFullTable, bool blHomeTable)
        {
            DataSet dsObjFilter = new DataSet();
            string strSqlTextObjFilter = "";
            //if(c1CmbHomeObj.Columns.Count > 0)  //удаляет солонку с номерами домов
                //c1CmbHomeObj.Columns.RemoveAt(0);
            if (blFullTable)
            {
                strSqlTextObjFilter = "SELECT Buildings.BuildId, ObjUkg.ObjName, ObjUkgStaff.OrgName, StreetType.StreetTypeName, Street.StreetName, " +
                    "Buildings.BuildName, StreetPlace.StreetPlaceName, Street.StreetId " +
                    "FROM Buildings INNER JOIN " +
                         "ObjUkg ON Buildings.BuildId = ObjUkg.BuildId INNER JOIN " +
                         "ObjUkgStaff ON ObjUkg.ObjStaffId = ObjUkgStaff.ObjStaffId INNER JOIN " +
                         "Street ON Buildings.StreetId = Street.StreetId INNER JOIN " +
                         "StreetType ON Street.StreetTypeId = StreetType.StreetTypeId INNER JOIN " +
                         "StreetPlace ON Street.StreetPlaceId = StreetPlace.StreetPlaceId " +
                    "ORDER BY ObjUkg.ObjName";
            }
            else
            {
                if (chkBoxMon.Checked)  //включает дополнительно фильтр для объектов мониторинга
                {
                    if (strFilter == "")
                        strFilter = "ObjUkg.ObjAutoMon = 1";
                    else
                        strFilter = strFilter + " AND ObjUkg.ObjAutoMon = 1";
                }

                strSqlTextObjFilter = "SELECT Buildings.BuildId, ObjUkg.ObjName, ObjUkgStaff.OrgName, StreetType.StreetTypeName, Street.StreetName, " +
                    "Buildings.BuildName, StreetPlace.StreetPlaceName, Street.StreetId, ObjUkg.ObjAutoMon " +
                    "FROM Buildings INNER JOIN " +
                         "ObjUkg ON Buildings.BuildId = ObjUkg.BuildId INNER JOIN " +
                         "ObjUkgStaff ON ObjUkg.ObjStaffId = ObjUkgStaff.ObjStaffId INNER JOIN " +
                         "Street ON Buildings.StreetId = Street.StreetId INNER JOIN " +
                         "StreetType ON Street.StreetTypeId = StreetType.StreetTypeId INNER JOIN " +
                         "StreetPlace ON Street.StreetPlaceId = StreetPlace.StreetPlaceId " +
                    "WHERE (" + strFilter + ") " +
                   "ORDER BY ObjUkg.ObjName";
                //загрузка номеров домов
                if (blHomeTable)    // если false не выполняет повторный выбор номеров домов в c1CmbHomeObj
                {
                    string strSqlTextHomeNmb = "SELECT DISTINCT  Buildings.BuildName, Street.StreetName, StreetType.StreetTypeName " +
                            "FROM Buildings INNER JOIN " +
                                "ObjUkg ON Buildings.BuildId = ObjUkg.BuildId INNER JOIN " +
                                "Street ON Buildings.StreetId = Street.StreetId INNER JOIN " +
                                "StreetType ON Street.StreetTypeId = StreetType.StreetTypeId " +
                            "WHERE (" + strFilter + ") " +
                            "ORDER BY Buildings.BuildName";
                    DataSet dsHomeNmb = new DataSet();
                    BindingSource bsHomeNmb = new BindingSource();
                    SqlDataAdapter daHomeNmb = new SqlDataAdapter(strSqlTextHomeNmb, FrmMain.Instance.m_cnn);
                    daHomeNmb.Fill(dsHomeNmb, "Buildings");
                    bsHomeNmb.DataSource = dsHomeNmb;
                    bsHomeNmb.DataMember = "Buildings";

                    c1CmbHomeObj.DataSource = bsHomeNmb;
                    c1CmbHomeObj.ValueMember = "BuildName";
                    c1CmbHomeObj.DisplayMember = "BuildName";
                    c1CmbHomeObj.Columns.RemoveAt(2);
                    c1CmbHomeObj.Columns.RemoveAt(1);
                }
            }
            SqlDataAdapter daObjFilter = new SqlDataAdapter(strSqlTextObjFilter, FrmMain.Instance.m_cnn);
            daObjFilter.Fill(dsObjFilter, "Buildings");
            c1FlxGrdObjFnd.SetDataBinding(dsObjFilter, "Buildings");
            ObjTableStyles();
            
            if (!blFullTable)
                c1FlxGrdObjFnd.Cols[9].Visible = false; //скрывает колонку мониторинга ObjUkg.ObjAutoMon
        }

        private void btnFindObj_Click(object sender, EventArgs e)
        {
            /*FrmObjUkgData.Instance = new FrmObjUkgData();
            FrmObjUkgData.Instance.Text = "Объект: " + c1FlxGrdObjFnd.GetDataDisplay(c1FlxGrdObjFnd.RowSel, 2) + " (" +
                                            c1FlxGrdObjFnd.GetDataDisplay(c1FlxGrdObjFnd.RowSel, 3) + ")";
            string strObjId = c1FlxGrdObjFnd.GetDataDisplay(c1FlxGrdObjFnd.RowSel, 1);

            FrmObjUkgData.Instance.Show();*/

            //tabCntFindAlarm.TabPages[1].t;
            //c1DockingTab1.TabPages[1].TabVisible = false;
                        
        }

        private void ObjTableLoad()
        {
            c1FlxGrdObjFnd.SetDataBinding(dsObjFnd, "Buildings");
            ObjTableStyles();
        }

        private void ObjTableStyles()
        {
            c1FlxGrdObjFnd.Cols[1].Visible = false;
            c1FlxGrdObjFnd.Cols[8].Visible = false; //Street.StreetId
            c1FlxGrdObjFnd.Cols[2].Width = 130;
            c1FlxGrdObjFnd.Cols[2].Caption = "Объект";
            c1FlxGrdObjFnd.Cols[2].AllowEditing = false;
            c1FlxGrdObjFnd.Cols[3].Width = 130;
            c1FlxGrdObjFnd.Cols[3].Caption = "Организация";
            c1FlxGrdObjFnd.Cols[3].AllowEditing = false;
            c1FlxGrdObjFnd.Cols[4].Width = 70;
            c1FlxGrdObjFnd.Cols[4].Caption = "Тип";
            c1FlxGrdObjFnd.Cols[4].AllowEditing = false;
            c1FlxGrdObjFnd.Cols[5].Width = 100;
            c1FlxGrdObjFnd.Cols[5].Caption = "Улица";
            c1FlxGrdObjFnd.Cols[5].AllowEditing = false;
            c1FlxGrdObjFnd.Cols[6].Width = 40;
            c1FlxGrdObjFnd.Cols[6].Caption = "Дом";
            c1FlxGrdObjFnd.Cols[6].AllowEditing = false;
            c1FlxGrdObjFnd.Cols[7].Width = 100;
            c1FlxGrdObjFnd.Cols[7].Caption = "Район";
            c1FlxGrdObjFnd.Cols[7].AllowEditing = false;

            CellStyle cs = c1FlxGrdObjFnd.Styles.Add("s1");
            cs.Font = new Font("Arial", 9, FontStyle.Bold);
            cs.TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.CenterCenter;
            c1FlxGrdObjFnd.Rows[0].Style = cs;
        }

        private void OrgLoad()
        {
            c1CmbOrg.DataSource = bsOrgFnd;
            c1CmbOrg.ValueMember = "OrgName";
            c1CmbOrg.DisplayMember = "OrgName";
        }

        private void StreetPlaceLoad()
        {
            c1CmbStreetObj.DataSource = bsPlaceFnd;
            c1CmbStreetObj.ValueMember = "StreetPlaceName";
            c1CmbStreetObj.DisplayMember = "StreetPlaceName";

            c1CmbStreetObj.Splits[0].DisplayColumns[0].Visible = false; //..Columns
            c1CmbStreetObj.Splits[0].DisplayColumns[1].Width = 130;
            c1CmbStreetObj.Columns[1].Caption = "Район";
            c1CmbStreetObj.DropDownWidth = 200;
        }

        private void ObjLoad()
        {
            c1CmbStreetObj.DataSource = bsObjFnd;
            c1CmbStreetObj.ValueMember = "ObjName";
            c1CmbStreetObj.DisplayMember = "ObjName";

            c1CmbStreetObj.Splits[0].DisplayColumns[0].Visible = false; //..Columns
            c1CmbStreetObj.Splits[0].DisplayColumns[1].Width = 130;
            c1CmbStreetObj.Splits[0].DisplayColumns[2].Width = 130;
            c1CmbStreetObj.Splits[0].DisplayColumns[3].Width = 70;
            c1CmbStreetObj.Splits[0].DisplayColumns[5].Width = 40;

            c1CmbStreetObj.Columns[1].Caption = "Объект";
            c1CmbStreetObj.Columns[2].Caption = "Организация";
            c1CmbStreetObj.Columns[3].Caption = "Тип";
            c1CmbStreetObj.Columns[4].Caption = "Улица";
            c1CmbStreetObj.Columns[5].Caption = "Дом";
            c1CmbStreetObj.Columns[6].Caption = "Район";
            c1CmbStreetObj.DropDownWidth = 600;
        }

        private void StreetObjLoad()
        {
            c1CmbStreetObj.DataSource = bsStreetObjFnd;
            c1CmbStreetObj.ValueMember = "StreetName";
            c1CmbStreetObj.DisplayMember = "StreetName";

            c1CmbStreetObj.Columns.RemoveAt(0);
            c1CmbStreetObj.Columns.RemoveAt(6);
            c1CmbStreetObj.Columns.RemoveAt(5);
            c1CmbStreetObj.Columns.RemoveAt(4);
            c1CmbStreetObj.Columns[0].Caption = "Улица";
            c1CmbStreetObj.Splits[0].DisplayColumns[0].Width = 130;
            c1CmbStreetObj.Columns[1].Caption = "Тип";
            c1CmbStreetObj.Columns[2].Caption = "Район";
            c1CmbStreetObj.Columns[3].Caption = "Регион";

            c1CmbStreetObj.DropDownWidth = 500;
        }

        private void rdBtnStreet_CheckedChanged(object sender, EventArgs e)
        {
            if (rdBtnStreet.Checked)
            {
                l_iTypeFnd = 1;
                lblOrgReg.Text = "";
                c1CmbOrg.Enabled = false;
                c1CmbHomeObj.Enabled = true;
                c1CmbHomeObj.Text = "";
                c1CmbOrg.Text = "";
                StreetObjLoad();
            }
        }

        private void rdBtnObjName_CheckedChanged(object sender, EventArgs e)
        {
            if (rdBtnObjName.Checked)
            {
                l_iTypeFnd = 3;
                c1CmbOrg.Enabled = true;
                lblOrgReg.Text = "Организация";
                c1CmbHomeObj.Enabled = false;
                c1CmbStreetObj.Text = "";
                c1CmbHomeObj.Text = "";
                c1CmbOrg.Text = "";
                ObjLoad();
                OrgLoad();
            }
        }

        private void rdBtnPlace_CheckedChanged(object sender, EventArgs e)
        {
            if (rdBtnPlace.Checked)
            {
                l_iTypeFnd = 2;
                c1CmbOrg.Enabled = false;
                c1CmbHomeObj.Enabled = false;
                c1CmbStreetObj.Text = "";
                c1CmbHomeObj.Text = "";
                c1CmbOrg.Text = "";
                lblOrgReg.Text = "";
                StreetPlaceLoad();
            }
        }

        private void c1CmbStreetObj_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Return)
            {
                e.Handled = true;
                if (l_iTypeFnd == 3 && c1CmbStreetObj.Text != "") //выбор объекта из общего списка без организации
                    ObjFilterLoad("ObjUkg.ObjName = '" + c1CmbStreetObj.Text + "'", false, false);
                else
                    ObjFindSend();
            }
        }

        private void c1CmbStreetObj_Close(object sender, EventArgs e)
        {
            if (l_iTypeFnd == 3 && c1CmbStreetObj.Text != "")
                ObjFilterLoad("ObjUkg.ObjName = '" + c1CmbStreetObj.Text + "'", false, false);
            else
                ObjFindSend();
        }

        private void ObjFindSend()
        {
            string strFilter;

            if (l_iTypeFnd == 1 && c1CmbStreetObj.Text != "")
            {
                string strName = c1CmbStreetObj.Text;
                
                if (strName == "")
                {
                    blFullTable = true; // заполняет в таблицу полные данные
                    strFilter = "Street.StreetName = '" + strName + "'";
                }
                else
                {
                    //добавляет в фильтр улицу и тип улицы
                    blFullTable = false;
                    DataRow dr = dsStreetObjFnd.Tables["Street"].Rows[bsStreetObjFnd.Position];
                    string strType = dr["StreetTypeName"].ToString();
                    strFilter = "Street.StreetName = '" + strName + "' AND StreetType.StreetTypeName = '" + strType + "'";
                }
                ObjFilterLoad(strFilter, blFullTable, true);
                c1CmbHomeObj.Focus();
            }

            if (l_iTypeFnd == 2 && c1CmbStreetObj.Text != "")
            {
                strFilter = "StreetPlace.StreetPlaceName = '" + c1CmbStreetObj.Text + "'";
                ObjFilterLoad(strFilter, false, false);
            }

            if (l_iTypeFnd == 3 && c1CmbOrg.Text != "")
            {
                strFilter = "ObjUkgStaff.OrgName = '" + c1CmbOrg.Text + "'";
                ObjFilterLoad(strFilter, false, false);
            }
        }

        private void ObjFindHome()
        {
            if (l_iTypeFnd == 1 && c1CmbHomeObj.Text != "")
            {
                string strName = c1CmbStreetObj.Text;
                bool blFullTable = false;
                string strFilter;
                string strHomeNb = c1CmbHomeObj.Text;
                if (strName == "")
                {
                    // заполняет в таблицу с номером выбранного дома (поиск только по номеру)
                    strFilter = "Buildings.BuildName = '" + strHomeNb + "'";
                }
                else
                {
                    //добавляет в фильтр улицу, тип улицы и номер дома
                    DataRow dr = dsStreetObjFnd.Tables["Street"].Rows[bsStreetObjFnd.Position];
                    string strType = dr["StreetTypeName"].ToString();
                    strFilter = "Street.StreetName = '" + strName + "' AND StreetType.StreetTypeName = '" + strType + "' " +
                        "AND Buildings.BuildName = '" + strHomeNb + "'";
                }
                ObjFilterLoad(strFilter, blFullTable, false);
            }
        }

        private void c1CmbHomeObj_Close(object sender, EventArgs e)
        {
            ObjFindHome();
        }

        private void c1CmbHomeObj_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Return)
            {
                e.Handled = true;
                ObjFindHome();
            }
        }

        private void c1CmbOrg_Close(object sender, EventArgs e)
        {
            ObjFindSend();
        }

        private void c1CmbOrg_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Return)
            {
                e.Handled = true;
                ObjFindSend();
            }
        }

        private void btnObjPmt_Click(object sender, EventArgs e)
        {
            int iCntRows = c1FlxGrdObjFnd.Rows.Count;
            if (iCntRows > 1)   //разрешает если в таблице есть хотя бы 1 запись объекта
            {
                string strObj = c1FlxGrdObjFnd.GetDataDisplay(c1FlxGrdObjFnd.RowSel, 2) + " (" +
                    c1FlxGrdObjFnd.GetDataDisplay(c1FlxGrdObjFnd.RowSel, 3) + ")";
                string strMsg = "Объект: " + strObj + "\r\n" + "Записать информацию об объекте в путевку?";

                if (MessageBox.Show(strMsg, "Выбор объекта",
                        MessageBoxButtons.YesNo, MessageBoxIcon.Question)
                        == DialogResult.Yes)
                {
                    string strStreetId = c1FlxGrdObjFnd.GetDataDisplay(c1FlxGrdObjFnd.RowSel, 8);
                    int itemFound = FrmPermit.Instance.bsStreet.Find("StreetId", strStreetId);
                    FrmPermit.Instance.bsStreet.Position = itemFound;   // записывает позицию для путевки

                    string strObjId = c1FlxGrdObjFnd.GetDataDisplay(c1FlxGrdObjFnd.RowSel, 1);
                    FrmPermit.Instance.textBoxStreetType.Text = c1FlxGrdObjFnd.GetDataDisplay(c1FlxGrdObjFnd.RowSel, 4);
                    FrmPermit.Instance.c1ComboFnd.Text = c1FlxGrdObjFnd.GetDataDisplay(c1FlxGrdObjFnd.RowSel, 5);
                    FrmPermit.Instance.c1ComboHome.Text = c1FlxGrdObjFnd.GetDataDisplay(c1FlxGrdObjFnd.RowSel, 6);

                    FrmPermit.Instance.textBoxObj.Text = strObj;
                    FrmPermit.Instance.m_TechnicsLoadObj(strObjId);
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
        }

        private void btnObjInfo_Click(object sender, EventArgs e)
        {
            //FrmPermit.Instance.m_strBuildIdMap = c1FlxGrdObjFnd.GetDataDisplay(c1FlxGrdObjFnd.RowSel, 1);
            string strObjName = "Объект: " + c1FlxGrdObjFnd.GetDataDisplay(c1FlxGrdObjFnd.RowSel, 2) + " (" +
                                c1FlxGrdObjFnd.GetDataDisplay(c1FlxGrdObjFnd.RowSel, 3) + ")";
            FrmObjUkgData.Instance = new FrmObjUkgData();
            FrmObjUkgData.Instance.Text = strObjName;
            FrmObjUkgData.Instance.m_strBuildIdObj = c1FlxGrdObjFnd.GetDataDisplay(c1FlxGrdObjFnd.RowSel, 1);
            FrmObjUkgData.Instance.Show();
        }

        private void btnObjMap_Click(object sender, EventArgs e)
        {
            if (c1FlxGrdObjFnd.RowSel > 0)
            {
                m_strObjFndId = "";
                m_strObjFndId = c1FlxGrdObjFnd.GetDataDisplay(c1FlxGrdObjFnd.RowSel, 1);
                if (m_strObjFndId != "")   // не выводить карту если дома нет в базе данных карты
                    FrmMain.Instance.LoadMapUkg(1, m_strObjFndId);
            }
        }

        private void chkBoxMon_CheckedChanged(object sender, EventArgs e)
        {
            string strFilter;
            string strName = "";
            if (chkBoxMon.Checked)
            {
                blFullTable = false;
                strFilter = "";
            }
            else
            {
                blFullTable = true; // заполняет в таблицу полные данные
                strFilter = "Street.StreetName = '" + strName + "'";
            }
            ObjFilterLoad(strFilter, blFullTable, false);
        }
    }
}
