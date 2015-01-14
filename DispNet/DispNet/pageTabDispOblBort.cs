using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;
using C1.Win.C1FlexGrid;

namespace DispNet
{
    public partial class pageTabDispOblBort : UserControl
    {
        string strRptDispId;
        DataSet dsDispOblBort = new DataSet();
        DataSet dsPchObl = new DataSet();
        BindingSource bsPchObl = new BindingSource();

        public pageTabDispOblBort()
        {
            InitializeComponent();
        }

        private void pageTabDispOblBort_Load(object sender, EventArgs e)
        {
            strRptDispId = FrmDispFire.Instance.m_strRptDispId;
            c1FlxGrdPchObl.ClipSeparators = "|;";

            LoadDataRptBort();

            c1FlxGrdOblBort.Cols[1].Visible = false;
            c1FlxGrdOblBort.Cols[2].Visible = false;
            c1FlxGrdOblBort.Cols[3].Width = 100;
            c1FlxGrdOblBort.Cols[3].Caption = "П/часть";
            c1FlxGrdOblBort.Cols[3].AllowEditing = false;
            c1FlxGrdOblBort.Cols[4].Width = 65;
            c1FlxGrdOblBort.Cols[4].Caption = "Борт";
            c1FlxGrdOblBort.Cols[4].AllowEditing = false;
            c1FlxGrdOblBort.Cols[5].Width = 70;
            c1FlxGrdOblBort.Cols[5].Caption = "Выезд";
            c1FlxGrdOblBort.Cols[6].Width = 70;
            c1FlxGrdOblBort.Cols[6].Caption = "Прибыл";
            c1FlxGrdOblBort.Cols[7].Width = 70;
            c1FlxGrdOblBort.Cols[7].Caption = "Убыл";
            c1FlxGrdOblBort.Cols[8].Width = 70;
            c1FlxGrdOblBort.Cols[8].Caption = "Возврат";
            
            string strSqlTextPchObl = "SELECT UnitId, UnitName, UnitTypeId FROM Unit " +
                                        "WHERE (UnitTypeId = 2)ORDER BY UnitId";
            SqlDataAdapter daPchObl = new SqlDataAdapter(strSqlTextPchObl, FrmMain.Instance.m_cnn);
            daPchObl.Fill(dsPchObl, "Unit");
            bsPchObl.DataSource = dsPchObl;
            bsPchObl.DataMember = "Unit";
            c1CmbPchObl.DataSource = bsPchObl;
            c1CmbPchObl.ValueMember = "UnitName";
            c1CmbPchObl.DisplayMember = "UnitName";
            c1CmbPchObl.Splits[0].DisplayColumns[0].Visible = false;
            c1CmbPchObl.Splits[0].DisplayColumns[2].Visible = false;
            c1CmbPchObl.Columns[1].Caption = "Текст сообщения";
            c1CmbPchObl.Splits[0].DisplayColumns[1].HeadingStyle.HorizontalAlignment = C1.Win.C1List.AlignHorzEnum.Center;

            LoadDataRptPch();
        }
        
        private void LoadDataRptPch()
        {
            string strSqlTextPchObl = "SELECT RptDispId, UnitIdText " +
                                       "FROM ReportFireObl WHERE (RptDispId = " + strRptDispId + ")";
            SqlCommand cmd = new SqlCommand(strSqlTextPchObl, FrmMain.Instance.m_cnn);
            SqlDataReader rdPchObl = cmd.ExecuteReader();

            DataView dvPchObl = new DataView(dsPchObl.Tables["Unit"], "",
                                    "UnitId", DataViewRowState.CurrentRows);
            string strPchId = "";
            while (rdPchObl.Read())
            {
                if (!rdPchObl.IsDBNull(1))
                {
                    strPchId = rdPchObl.GetString(1);
                }
            }
            rdPchObl.Close();
            if (strPchId != "")
            {
                string[] strPchIdOb = strPchId.Split(',');
                int iCntRows = strPchIdOb.Length;
                c1FlxGrdPchObl.Rows.Count = strPchIdOb.Length;
                for (int i = 0; i < strPchIdOb.Length; i++)
                {
                    string strId = strPchIdOb[i];
                    string strPch;
                    int rowIndex = dvPchObl.Find(strId);
                    if (rowIndex != -1)
                    {
                        strPch = dvPchObl[rowIndex]["UnitName"].ToString();
                        strPch = "|" + strPch + "|" + strId;
                        c1FlxGrdPchObl.AddItem(strPch, i+1);
                    }
                }
                c1FlxGrdPchObl.Select(0, 2);
                c1FlxGrdPchObl.Rows.Count = iCntRows + 1;
            }
        }

        private void LoadDataRptBort()
        {
            string strSqlTextOblBort = "SELECT ReportFireBortObl.RptFireBortOblId, ReportFireBortObl.RptDispId, Unit.UnitName, " +
                      "ReportFireBortObl.TechnicsName, ReportFireBortObl.BortDrive, ReportFireBortObl.BortArrive, " +
                      "ReportFireBortObl.BortDeparture, ReportFireBortObl.BortReturn " +
                      "FROM ReportFireBortObl INNER JOIN Unit ON ReportFireBortObl.UnitId = Unit.UnitId " +
                      "WHERE (ReportFireBortObl.RptDispId = " + strRptDispId + ") ORDER BY ReportFireBortObl.RptFireBortOblId";
            SqlDataAdapter daDispOblBort = new SqlDataAdapter(strSqlTextOblBort, FrmMain.Instance.m_cnn);
            daDispOblBort.Fill(dsDispOblBort, "ReportFireBortObl");
            c1FlxGrdOblBort.SetDataBinding(dsDispOblBort, "ReportFireBortObl");

            Load_c1FlxStyles();
        }

        private void Load_c1FlxStyles()
        {
            int iRws = c1FlxGrdOblBort.Rows.Count - 1;
            CellStyle cs = c1FlxGrdOblBort.Styles.Add("time");
            cs.DataType = typeof(string);
            cs.ComboList = "|...";
            cs.EditMask = "##:##:##";

            cs = c1FlxGrdOblBort.Styles.Add("disloc");
            cs.DataType = typeof(string);
            cs.ComboList = "...";
            cs.EditMask = "##:##:##";

            // assign styles to editable cells
            if (iRws > 0)
            {
                CellRange rg = c1FlxGrdOblBort.GetCellRange(0, 5, iRws, 8);
                rg.Style = c1FlxGrdOblBort.Styles["time"];

                //rg = c1FlxGrdOblBort.GetCellRange(0, 9, iRws, 9);
                //rg.Style = c1FlxGrdOblBort.Styles["disloc"];
            }
        }

        private void AddBort()
        {
            string strMsg;
            strMsg = "Борт №" + txtBoxAddBort.Text;
            if (c1CmbPchObl.Text != "" && txtBoxAddBort.Text != "")
            {
                if (MessageBox.Show("Записать информацию?", strMsg,
                    MessageBoxButtons.YesNo, MessageBoxIcon.Question)
                    == DialogResult.Yes)
                {
                    int iCntRows = c1FlxGrdOblBort.Rows.Count;
                    string strUnitId = c1CmbPchObl.Columns[0].Text;
                                     
                    object[] items = { c1CmbPchObl.Text, txtBoxAddBort.Text };
                    c1FlxGrdOblBort.AddItem(items, iCntRows, 3); //добавляет в таблицу имя ПЧ, имя Борта
                    
                    Load_c1FlxStyles();

                    string strSqlTextRptFireBortObl = "INSERT INTO ReportFireBortObl " +
                                "(RptFireBortOblId, RptDispId, UnitId, TechnicsName) " +
                                "Values('0', '" + strRptDispId + "', '" + strUnitId + "', '" + txtBoxAddBort.Text + "')";

                    SqlCommand command = new SqlCommand(strSqlTextRptFireBortObl, FrmMain.Instance.m_cnn);
                    Int32 recordsAffected = command.ExecuteNonQuery();
                }
            }
            else
            {
                string strMsgBort = "";
                if (c1CmbPchObl.Text == "")
                    strMsgBort = @"Не заполнено поле П\часть!";
                if (txtBoxAddBort.Text == "")
                    strMsgBort = strMsgBort + "\r\n" + "Не заполнено поле Борт!";

                MessageBox.Show(strMsgBort, "Борт - выезд техники",
                    MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
        }

        private void c1CmbPchObl_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Return)
            {
                e.Handled = true;
                /*if (MessageBox.Show("Записать информацию?", "П/часть",
                    MessageBoxButtons.YesNo, MessageBoxIcon.Question)
                    == DialogResult.Yes)
                {
                    string strId;
                    string strMsg;
                    string strRecMsg = "";
                    int iCntRows = c1FlxGrdPchObl.Rows.Count;
                    iCntRows = iCntRows - 1;
                    for (int i = 0; i < iCntRows; i++)
                    {
                        strId = c1FlxGrdPchObl.GetDataDisplay(i + 1, 2);
                        strMsg = c1FlxGrdPchObl.GetDataDisplay(i + 1, 1);
                        if (strMsg != "")   // удаляет строку если текст имя ПЧ пуст
                        {
                            if (i == 0)
                                strRecMsg = strId;
                            else
                                strRecMsg = strRecMsg + "," + strId;
                        }
                    }
                    DataRow dr = dsPchObl.Tables["Unit"].Rows[bsPchObl.Position];
                    string strUnitId = dr["UnitId"].ToString();
                    string strUnitName = dr["UnitName"].ToString();
                    string strPchAdd = "|" + strUnitName + "|" + strUnitId;
                    c1FlxGrdPchObl.AddItem(strPchAdd, iCntRows + 1);
                    if (iCntRows == 0)
                        strRecMsg = strUnitId;
                    else
                        strRecMsg = strRecMsg + "," + strUnitId;

                    string strSqlTextPchObl = "UPDATE ReportFireObl SET UnitIdText = '" + strRecMsg +
                                                    "' WHERE RptDispId = " + strRptDispId;
                    SqlCommand command = new SqlCommand(strSqlTextPchObl, FrmMain.Instance.m_cnn);
                    Int32 recordsAffected = command.ExecuteNonQuery();
                    
                    c1CmbPchObl.Text = "";
                    c1FlxGrdPchObl.Select(0, 2);
                }*/
            }
        }

        private void btnTest_Click(object sender, EventArgs e)
        {
            string rtr = c1CmbPchObl.Columns[0].Text;
        }

        private void c1FlxGrdOblBort_CellButtonClick(object sender, RowColEventArgs e)
        {
            /*string strColName = "";
            bool blDisloc = true;

            switch (e.Col)
            {
                case 5:
                    strColName = "Ввести время выезда?";
                    break;
                case 6:
                    strColName = "Ввести время прибытия?";
                    break;
                case 7:
                    strColName = "Ввести время убытия?";
                    break;
                case 8:
                    strColName = "Ввести время возвращения?";
                    break;
                case 9:
                    if (c1FlxGrdOblBort.GetDataDisplay(e.Row, 9) == "")
                        blDisloc = true;
                    else
                        blDisloc = false;
                    strColName = "Ввести время передислокации?";
                    break;
            }*/
        }

        private void c1FlxGrdOblBort_AfterEdit(object sender, RowColEventArgs e)
        {
            string strTime = c1FlxGrdOblBort.GetDataDisplay(e.Row, e.Col);
            string strBortId = c1FlxGrdOblBort.GetDataDisplay(e.Row, 1);
            string strColId = c1FlxGrdOblBort.Cols[e.Col].Name;

            string strSqlTextBortTime = "UPDATE ReportFireBortObl SET " + strColId + " = CONVERT(DATETIME, '" + strTime + "', 102) " +
                                        "WHERE RptFireBortOblId = " + strBortId;
            SqlCommand command = new SqlCommand(strSqlTextBortTime, FrmMain.Instance.m_cnn);
            Int32 recordsAffected = command.ExecuteNonQuery();
        }

        private void txtBoxAddBort_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Return)
            {
                e.Handled = true;
                AddBort();
            }
        }

        private void btnRecBortObl_Click(object sender, EventArgs e)
        {
            AddBort();
        }
    }
}
