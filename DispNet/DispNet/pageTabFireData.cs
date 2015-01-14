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
    public partial class pageTabFireData : UserControl
    {
        DataSet dsAreaFireList = new DataSet();
        BindingSource bsAreaFireList = new BindingSource();

        //bool l_blRecMasterFire = false; //не дает сделать досткпной кнопку Запись если не изменен текст в txtBoxMasterFire
        string strRptDispId;
        
        public pageTabFireData()
        {
            InitializeComponent();
        }

        private void pageTabFireData_Load(object sender, EventArgs e)
        {
            strRptDispId = FrmDispFire.Instance.m_strRptDispId;
            LoadFireData();
            //l_blRecMasterFire = true;
        }

        private void LoadDataGuiltyList()
        {
            string strSqlTextGuilty = "SELECT RptGuiltyId, RptDispId, RptGuiltyName " +
                                       "FROM ReportFireGuilty WHERE (RptDispId = " + strRptDispId + ")";
            SqlCommand cmd = new SqlCommand(strSqlTextGuilty, FrmMain.Instance.m_cnn);
            SqlDataReader rdrGuilty = cmd.ExecuteReader();

            string strGuiltyId = "";
            string strGuiltyName = "";
            int iCntGuilty = 0;
            while (rdrGuilty.Read())
            {
                if (!rdrGuilty.IsDBNull(0))
                    strGuiltyId = rdrGuilty.GetInt32(0).ToString();
                if (!rdrGuilty.IsDBNull(2))
                    strGuiltyName = rdrGuilty.GetString(2);
                if (strGuiltyId != "" && strGuiltyName != "")
                {
                    iCntGuilty++;
                    object[] items = { strGuiltyId, strGuiltyName };
                    c1FlxGrdGuilty.AddItem(items, iCntGuilty, 1);

                }
            }
            rdrGuilty.Close();
            c1FlxGrdGuilty.Cols[2].Caption = "Виновные - " + iCntGuilty.ToString();
        }

        private void LoadDataVictimList()
        {
            string strSqlTextVictim = "SELECT RptVictimId, RptDispId, RptVictimName " +
                                       "FROM ReportFireVictim WHERE (RptDispId = " + strRptDispId + ")";
            SqlCommand cmd = new SqlCommand(strSqlTextVictim, FrmMain.Instance.m_cnn);
            SqlDataReader rdrVictim = cmd.ExecuteReader();

            string strVictimId = "";
            string strVictimName = "";
            int iCntVictim = 0;
            while (rdrVictim.Read())
            {
                if (!rdrVictim.IsDBNull(0))
                    strVictimId = rdrVictim.GetInt32(0).ToString();
                if (!rdrVictim.IsDBNull(2))
                    strVictimName = rdrVictim.GetString(2);
                if (strVictimId != "" && strVictimName != "")
                {
                    iCntVictim++;
                    object[] items = { strVictimId, strVictimName };
                    c1FlxGrdVictim.AddItem(items, iCntVictim, 1);

                }
            }
            rdrVictim.Close();
            c1FlxGrdVictim.Cols[2].Caption = "Пострадавшие - " + iCntVictim.ToString();
            
            /*string strSqlTextVictim = "SELECT RptDispId, RptVictimPeople " +
                                       "FROM ReportFire WHERE (RptDispId = " + strRptDispId + ")";
            SqlCommand cmd = new SqlCommand(strSqlTextVictim, FrmMain.Instance.m_cnn);
            SqlDataReader rdrVictim = cmd.ExecuteReader();

            string[] strVictimList;
            _dt = new DataTable();
            _dt.Columns.Add("Id", typeof(int));
            _dt.Columns.Add("Пострадавшие", typeof(string));
            while (rdrVictim.Read())
            {
                if (rdrVictim.IsDBNull(1))
                {
                    c1FlxGrdVictim.DataSource = _dt;
                    l_strVictimName = "";
                    c1FlxGrdVictim.Cols[1].Visible = false;
                    c1FlxGrdVictim.Cols[2].Caption = "Пострадавшие - 0";
                }
                else
                {
                    string strChrVictim = rdrVictim.GetString(1);
                    int lastLocation = strChrVictim.IndexOf("#");
                    if (lastLocation > 0)
                    {
                        l_iVictimPeople = Int16.Parse(strChrVictim.Substring(0, lastLocation));
                        l_strVictimName = strChrVictim.Substring(lastLocation + 1);
                    }
                    strVictimList = l_strVictimName.Split('|');
                    object[] data = new object[2];
                    for (int i = 0; i < strVictimList.Length; i++)
                    {
                        data[0] = i;
                        data[1] = strVictimList[i];
                        _dt.Rows.Add(data);
                    }
                    _dt.AcceptChanges();
                    c1FlxGrdVictim.DataSource = _dt;
                    c1FlxGrdVictim.Sort(SortFlags.Ascending, 2);
                    c1FlxGrdVictim.Cols[1].Visible = false;
                    c1FlxGrdVictim.Cols[2].Width = 80;
                    c1FlxGrdVictim.Cols[2].Caption = "Пострадавшие - " + l_iVictimPeople.ToString();
                }
            }
            rdrVictim.Close();*/
        }

        private void LoadDataDeadList()
        {
            string strSqlTextDead = "SELECT RptDeadId, RptDispId, RptDeadName " +
                                       "FROM ReportFireDead WHERE (RptDispId = " + strRptDispId + ")";
            SqlCommand cmd = new SqlCommand(strSqlTextDead, FrmMain.Instance.m_cnn);
            SqlDataReader rdrDead = cmd.ExecuteReader();

            string strDeadId = "";
            string strDeadName = "";
            int iCntDead = 0;
            while (rdrDead.Read())
            {
                if (!rdrDead.IsDBNull(0))
                    strDeadId = rdrDead.GetInt32(0).ToString();
                if (!rdrDead.IsDBNull(2))
                    strDeadName = rdrDead.GetString(2);
                if (strDeadId != "" && strDeadName != "")
                {
                    iCntDead++;
                    object[] items = { strDeadId, strDeadName };
                    c1FlxGrdtDead.AddItem(items, iCntDead, 1);
                    
                }
            }
            rdrDead.Close();
            c1FlxGrdtDead.Cols[2].Caption = "Погибшие - " + iCntDead.ToString();
           
        }

        private void LoadFireData()
        {
            string strSqlTextLoadData = "SELECT RptMasterFire, RptEscapePeople, RptEvacuatePeople, " +
                "RptDeadAnmKrs, RptDeadAnmMrs, RptDeadAnmPr, RptDispId " +
                "FROM ReportFire WHERE (RptDispId = " + strRptDispId + ") ORDER BY RptFireId";
            SqlCommand cmd = new SqlCommand(strSqlTextLoadData, FrmMain.Instance.m_cnn);
            SqlDataReader rdr = cmd.ExecuteReader();

            while (rdr.Read())
            {
                if (!rdr.IsDBNull(0))
                    txtBoxMasterFire.Text = rdr.GetString(0);
                if (!rdr.IsDBNull(1))
                    c1TxtBoxEscapePeople.Text = rdr.GetInt16(1).ToString();
                if (!rdr.IsDBNull(2))
                    c1TxtBoxEvacuatePeople.Text = rdr.GetInt16(2).ToString();
                if (!rdr.IsDBNull(3))
                    c1TxtBoxKrs.Text = rdr.GetInt16(3).ToString();
                if (!rdr.IsDBNull(4))
                    c1TxtBoxMrs.Text = rdr.GetInt16(4).ToString();
                if (!rdr.IsDBNull(5))
                    c1TxtBoxPr.Text = rdr.GetInt16(5).ToString();
                
            }
            rdr.Close();

            LoadDataDeadList();
            LoadDataVictimList();
            LoadDataGuiltyList();
        }
        
        private void c1TxtBoxEscapePeople_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Return)
            {
                e.Handled = true;
                if (MessageBox.Show("Записать информацию?", "Информация о спасенных",
                    MessageBoxButtons.YesNo, MessageBoxIcon.Question)
                    == DialogResult.Yes)
                {
                    string strSqlTextRecData = "UPDATE ReportFire SET RptEscapePeople = '" + c1TxtBoxEscapePeople.Text + "' " +
                                               "WHERE RptDispId = " + strRptDispId;
                    SqlCommand command = new SqlCommand(strSqlTextRecData, FrmMain.Instance.m_cnn);
                    Int32 recordsAffected = command.ExecuteNonQuery();
                }
            }
        }

        private void c1TxtBoxEvacuatePeople_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Return)
            {
                e.Handled = true;
                if (MessageBox.Show("Записать информацию?", "Информация об эвакуированных",
                    MessageBoxButtons.YesNo, MessageBoxIcon.Question)
                    == DialogResult.Yes)
                {
                    string strSqlTextRecData = "UPDATE ReportFire SET RptEvacuatePeople = '" + c1TxtBoxEvacuatePeople.Text + "' " +
                                               "WHERE RptDispId = " + strRptDispId;
                    SqlCommand command = new SqlCommand(strSqlTextRecData, FrmMain.Instance.m_cnn);
                    Int32 recordsAffected = command.ExecuteNonQuery();
                }
            }
        }

        private void txtBoxDeadPeople_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Return)
            {
                e.Handled = true;
                if (txtBoxDeadPeople.Text == "")
                {
                    MessageBox.Show("Введите ФИО погибшего", "Погибшие",
                        MessageBoxButtons.OK, MessageBoxIcon.Stop);
                }
                else
                {
                    string strSqlTextMsg = "INSERT INTO ReportFireDead " +
                            "(RptDeadId, RptDispId, RptDeadName) " +
                            "Values('0', '" + strRptDispId + "', '" + txtBoxDeadPeople.Text + "')";
                    
                    SqlCommand command = new SqlCommand(strSqlTextMsg, FrmMain.Instance.m_cnn);
                    Int32 recordsAffected = command.ExecuteNonQuery();

                    //обнавляет данные в таблице c1FlxGrdDead
                    c1FlxGrdtDead.Clear(ClearFlags.Content);
                    c1FlxGrdtDead.Rows.Count = 1;
                    txtBoxDeadPeople.Text = "";
                    LoadDataDeadList();
                }
                    
            }
        }

        private void c1FlxGrdtDead_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Delete)
            {
                string strDeadName;
                int i = c1FlxGrdtDead.RowSel;
                strDeadName = "Удалить " + c1FlxGrdtDead.GetDataDisplay(i, 2) + " из списка погибшие?";
                if (MessageBox.Show(strDeadName, "Погибшие",
                    MessageBoxButtons.YesNo, MessageBoxIcon.Question)
                    == DialogResult.Yes)
                {
                    string strRptDeadId = c1FlxGrdtDead.GetDataDisplay(i, 1);
                    string strSqlDelFireDead = "DELETE FROM ReportFireDead WHERE(RptDeadId = '" + strRptDeadId + "')";
                    SqlCommand cmnd = new SqlCommand(strSqlDelFireDead, FrmMain.Instance.m_cnn);
                    Int32 recordsAffectedDel = cmnd.ExecuteNonQuery();
                    c1FlxGrdtDead.Rows.Remove(i);
                    int iRows = c1FlxGrdtDead.Rows.Count - 1;
                    c1FlxGrdtDead.Cols[2].Caption = "Погибшие - " + iRows.ToString();
                }
            }
        }

        private void c1FlxGrdtDead_AfterEdit(object sender, RowColEventArgs e)
        {
            string strRecMsg = c1FlxGrdtDead.GetDataDisplay(e.Row, 2);
            string strSqlTextMsg = "UPDATE ReportFireDead SET RptDeadName = '" + strRecMsg +
                                            "' WHERE RptDeadId = " + c1FlxGrdtDead.GetDataDisplay(e.Row, 1);
            SqlCommand command = new SqlCommand(strSqlTextMsg, FrmMain.Instance.m_cnn);
            Int32 recordsAffected = command.ExecuteNonQuery();
        }

        private void txtBoxVictimPeople_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Return)
            {
                e.Handled = true;
                if (txtBoxVictimPeople.Text == "")
                {
                    MessageBox.Show("Введите ФИО пострадавшего", "Пострадавшие",
                        MessageBoxButtons.OK, MessageBoxIcon.Stop);
                }
                else
                {
                    string strSqlTextMsg = "INSERT INTO ReportFireVictim " +
                            "(RptVictimId, RptDispId, RptVictimName) " +
                            "Values('0', '" + strRptDispId + "', '" + txtBoxVictimPeople.Text + "')";

                    SqlCommand command = new SqlCommand(strSqlTextMsg, FrmMain.Instance.m_cnn);
                    Int32 recordsAffected = command.ExecuteNonQuery();

                    //обнавляет данные в таблице c1FlxGrdVictim
                    c1FlxGrdVictim.Clear(ClearFlags.Content);
                    c1FlxGrdVictim.Rows.Count = 1;

                    txtBoxVictimPeople.Text = "";
                    LoadDataVictimList();
                }

            }
        }

        private void c1FlxGrdVictim_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Delete)
            {
                string strVictimName;
                int i = c1FlxGrdVictim.RowSel;
                strVictimName = "Удалить " + c1FlxGrdVictim.GetDataDisplay(i, 2) + " из списка пострадавшие?";
                if (MessageBox.Show(strVictimName, "Пострадавшие",
                    MessageBoxButtons.YesNo, MessageBoxIcon.Question)
                    == DialogResult.Yes)
                {
                    string strRptVictimId = c1FlxGrdVictim.GetDataDisplay(i, 1);
                    string strSqlDelFireVictim = "DELETE FROM ReportFireVictim WHERE(RptVictimId = '" + strRptVictimId + "')";
                    SqlCommand cmnd = new SqlCommand(strSqlDelFireVictim, FrmMain.Instance.m_cnn);
                    Int32 recordsAffectedDel = cmnd.ExecuteNonQuery();
                    c1FlxGrdVictim.Rows.Remove(i);
                    int iRows = c1FlxGrdVictim.Rows.Count - 1;
                    c1FlxGrdVictim.Cols[2].Caption = "Пострадавшие - " + iRows.ToString();
                }
            }
        }

        private void c1FlxGrdVictim_AfterEdit(object sender, RowColEventArgs e)
        {
            string strRecMsg = c1FlxGrdVictim.GetDataDisplay(e.Row, 2);
            string strSqlTextMsg = "UPDATE ReportFireVictim SET RptVictimName = '" + strRecMsg +
                                            "' WHERE RptVictimId = " + c1FlxGrdVictim.GetDataDisplay(e.Row, 1);
            SqlCommand command = new SqlCommand(strSqlTextMsg, FrmMain.Instance.m_cnn);
            Int32 recordsAffected = command.ExecuteNonQuery();
            
        }

        private void txtBoxGuiltyPeople_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Return)
            {
                e.Handled = true;
                if (txtBoxGuiltyPeople.Text == "")
                {
                    MessageBox.Show("Введите ФИО виновного", "Виновные",
                        MessageBoxButtons.OK, MessageBoxIcon.Stop);
                }
                else
                {
                    string strSqlTextMsg = "INSERT INTO ReportFireGuilty " +
                            "(RptGuiltyId, RptDispId, RptGuiltyName) " +
                            "Values('0', '" + strRptDispId + "', '" + txtBoxGuiltyPeople.Text + "')";

                    SqlCommand command = new SqlCommand(strSqlTextMsg, FrmMain.Instance.m_cnn);
                    Int32 recordsAffected = command.ExecuteNonQuery();

                    //обнавляет данные в таблице c1FlxGrdGuilty
                    c1FlxGrdGuilty.Clear(ClearFlags.Content);
                    c1FlxGrdGuilty.Rows.Count = 1;
                    txtBoxGuiltyPeople.Text = "";
                    LoadDataGuiltyList();
                }
            }
        }

        private void c1FlxGrdGuilty_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Delete)
            {
                string strGuiltyName;
                int i = c1FlxGrdGuilty.RowSel;
                strGuiltyName = "Удалить " + c1FlxGrdGuilty.GetDataDisplay(i, 2) + " из списка виновные?";
                if (MessageBox.Show(strGuiltyName, "Виновные",
                    MessageBoxButtons.YesNo, MessageBoxIcon.Question)
                    == DialogResult.Yes)
                {
                    string strRptGuiltyId = c1FlxGrdGuilty.GetDataDisplay(i, 1);
                    string strSqlDelFireGuilty = "DELETE FROM ReportFireGuilty WHERE(RptGuiltyId = '" + strRptGuiltyId + "')";
                    SqlCommand cmnd = new SqlCommand(strSqlDelFireGuilty, FrmMain.Instance.m_cnn);
                    Int32 recordsAffectedDel = cmnd.ExecuteNonQuery();
                    c1FlxGrdGuilty.Rows.Remove(i);
                    int iRows = c1FlxGrdGuilty.Rows.Count - 1;
                    c1FlxGrdGuilty.Cols[2].Caption = "Виновные - " + iRows.ToString();
                }
            }
        }

        private void c1FlxGrdGuilty_AfterEdit(object sender, RowColEventArgs e)
        {
            string strRecMsg = c1FlxGrdGuilty.GetDataDisplay(e.Row, 2);
            string strSqlTextMsg = "UPDATE ReportFireGuilty SET RptGuiltyName = '" + strRecMsg +
                                            "' WHERE RptGuiltyId = " + c1FlxGrdGuilty.GetDataDisplay(e.Row, 1);
            SqlCommand command = new SqlCommand(strSqlTextMsg, FrmMain.Instance.m_cnn);
            Int32 recordsAffected = command.ExecuteNonQuery();
        }

        private void btnRecData_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Записать информацию?", "Информация о хозяевах",
                    MessageBoxButtons.YesNo, MessageBoxIcon.Question)
                    == DialogResult.Yes)
            {
                string strSqlTextRecData = "UPDATE ReportFire SET RptMasterFire = '" + txtBoxMasterFire.Text + "' " +
                                           "WHERE RptDispId = " + strRptDispId;
                SqlCommand command = new SqlCommand(strSqlTextRecData, FrmMain.Instance.m_cnn);
                Int32 recordsAffected = command.ExecuteNonQuery();
                //l_blRecMasterFire = false;
                //btnRecData.Enabled = false;
            }
            //else
                //l_blRecMasterFire = true;
        }
        
        private void c1TxtBoxKrs_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Return)
            {
                e.Handled = true;
                if (MessageBox.Show("Записать информацию?", "Количество погибшего КРС",
                    MessageBoxButtons.YesNo, MessageBoxIcon.Question)
                    == DialogResult.Yes)
                {
                    string strSqlTextRecData = "UPDATE ReportFire SET RptDeadAnmKrs = '" + c1TxtBoxKrs.Text + "' " +
                                               "WHERE RptDispId = " + strRptDispId;
                    SqlCommand command = new SqlCommand(strSqlTextRecData, FrmMain.Instance.m_cnn);
                    Int32 recordsAffected = command.ExecuteNonQuery();
                }
            }
        }

        private void c1TxtBoxMrs_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Return)
            {
                e.Handled = true;
                if (MessageBox.Show("Записать информацию?", "Количество погибшего МРС",
                    MessageBoxButtons.YesNo, MessageBoxIcon.Question)
                    == DialogResult.Yes)
                {
                    string strSqlTextRecData = "UPDATE ReportFire SET RptDeadAnmMrs = '" + c1TxtBoxMrs.Text + "' " +
                                               "WHERE RptDispId = " + strRptDispId;
                    SqlCommand command = new SqlCommand(strSqlTextRecData, FrmMain.Instance.m_cnn);
                    Int32 recordsAffected = command.ExecuteNonQuery();
                }
            }
        }

        private void c1TxtBoxPr_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Return)
            {
                e.Handled = true;
                if (MessageBox.Show("Записать информацию?", "Количество прочих погибшеих животных",
                    MessageBoxButtons.YesNo, MessageBoxIcon.Question)
                    == DialogResult.Yes)
                {
                    string strSqlTextRecData = "UPDATE ReportFire SET RptDeadAnmPr = '" + c1TxtBoxPr.Text + "' " +
                                               "WHERE RptDispId = " + strRptDispId;
                    SqlCommand command = new SqlCommand(strSqlTextRecData, FrmMain.Instance.m_cnn);
                    Int32 recordsAffected = command.ExecuteNonQuery();
                }
            }
        }
   
    }
}
