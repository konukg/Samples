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
    public partial class pageTabFireDataObl : UserControl
    {
        DataSet dsAreaFireList = new DataSet();
        BindingSource bsAreaFireList = new BindingSource();
        //bool l_blRecMasterFire = false; //не дает сделать досткпной кнопку Запись если не изменен текст в txtBoxMasterFire
        string strRptDispId;

        public pageTabFireDataObl()
        {
            InitializeComponent();
        }

        private void pageTabFireDataObl_Load(object sender, EventArgs e)
        {
            strRptDispId = FrmDispFire.Instance.m_strRptDispId;
            LoadFireData();
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
                    c1FlxGrdGuiltyObl.AddItem(items, iCntGuilty, 1);

                }
            }
            rdrGuilty.Close();
            c1FlxGrdGuiltyObl.Cols[2].Caption = "Виновные - " + iCntGuilty.ToString();
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
                    c1FlxGrdVictimObl.AddItem(items, iCntVictim, 1);

                }
            }
            rdrVictim.Close();
            c1FlxGrdVictimObl.Cols[2].Caption = "Пострадавшие - " + iCntVictim.ToString();
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
                    c1FlxGrdtDeadObl.AddItem(items, iCntDead, 1);

                }
            }
            rdrDead.Close();
            c1FlxGrdtDeadObl.Cols[2].Caption = "Погибшие - " + iCntDead.ToString();
        }

        private void LoadFireData()
        {
            string strSqlTextLoadData = "SELECT RptMasterFire, RptEscapePeople, RptEvacuatePeople, " +
                "RptDeadAnmKrs, RptDeadAnmMrs, RptDeadAnmPr, RptDispId " +
                "FROM ReportFireObl WHERE (RptDispId = " + strRptDispId + ") ORDER BY RptFireOblId";
            SqlCommand cmd = new SqlCommand(strSqlTextLoadData, FrmMain.Instance.m_cnn);
            SqlDataReader rdr = cmd.ExecuteReader();

            while (rdr.Read())
            {
                if (!rdr.IsDBNull(0))
                    txtBoxMasterFireObl.Text = rdr.GetString(0);
                if (!rdr.IsDBNull(1))
                    c1TxtBoxEscapePeopleObl.Text = rdr.GetInt16(1).ToString();
                if (!rdr.IsDBNull(2))
                    c1TxtBoxEvacuatePeopleObl.Text = rdr.GetInt16(2).ToString();
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
        
        private void c1TxtBoxEscapePeopleObl_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Return)
            {
                e.Handled = true;
                if (MessageBox.Show("Записать информацию?", "Информация о спасенных",
                    MessageBoxButtons.YesNo, MessageBoxIcon.Question)
                    == DialogResult.Yes)
                {
                    string strSqlTextRecData = "UPDATE ReportFireObl SET RptEscapePeople = '" + c1TxtBoxEscapePeopleObl.Text + "' " +
                                               "WHERE RptDispId = " + strRptDispId;
                    SqlCommand command = new SqlCommand(strSqlTextRecData, FrmMain.Instance.m_cnn);
                    Int32 recordsAffected = command.ExecuteNonQuery();
                }
            }
        }

        private void c1TxtBoxEvacuatePeopleObl_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Return)
            {
                e.Handled = true;
                if (MessageBox.Show("Записать информацию?", "Информация об эвакуированных",
                    MessageBoxButtons.YesNo, MessageBoxIcon.Question)
                    == DialogResult.Yes)
                {
                    string strSqlTextRecData = "UPDATE ReportFireObl SET RptEvacuatePeople = '" + c1TxtBoxEvacuatePeopleObl.Text + "' " +
                                               "WHERE RptDispId = " + strRptDispId;
                    SqlCommand command = new SqlCommand(strSqlTextRecData, FrmMain.Instance.m_cnn);
                    Int32 recordsAffected = command.ExecuteNonQuery();
                }
            }
        }

        private void txtBoxDeadPeopleObl_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Return)
            {
                e.Handled = true;
                if (txtBoxDeadPeopleObl.Text == "")
                {
                    MessageBox.Show("Введите ФИО погибшего", "Погибшие",
                        MessageBoxButtons.OK, MessageBoxIcon.Stop);
                }
                else
                {
                    string strSqlTextMsg = "INSERT INTO ReportFireDead " +
                            "(RptDeadId, RptDispId, RptDeadName) " +
                            "Values('0', '" + strRptDispId + "', '" + txtBoxDeadPeopleObl.Text + "')";

                    SqlCommand command = new SqlCommand(strSqlTextMsg, FrmMain.Instance.m_cnn);
                    Int32 recordsAffected = command.ExecuteNonQuery();

                    //обнавляет данные в таблице c1FlxGrdDeadObl
                    c1FlxGrdtDeadObl.Clear(ClearFlags.Content);
                    c1FlxGrdtDeadObl.Rows.Count = 1;
                    
                    txtBoxDeadPeopleObl.Text = "";
                    LoadDataDeadList();
                }

            }
        }

        private void c1FlxGrdtDeadObl_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Delete)
            {
                string strDeadName;
                int i = c1FlxGrdtDeadObl.RowSel;
                strDeadName = "Удалить " + c1FlxGrdtDeadObl.GetDataDisplay(i, 2) + " из списка погибшие?";
                if (MessageBox.Show(strDeadName, "Погибшие",
                    MessageBoxButtons.YesNo, MessageBoxIcon.Question)
                    == DialogResult.Yes)
                {
                    string strRptDeadId = c1FlxGrdtDeadObl.GetDataDisplay(i, 1);
                    string strSqlDelFireDead = "DELETE FROM ReportFireDead WHERE(RptDeadId = '" + strRptDeadId + "')";
                    SqlCommand cmnd = new SqlCommand(strSqlDelFireDead, FrmMain.Instance.m_cnn);
                    Int32 recordsAffectedDel = cmnd.ExecuteNonQuery();
                    c1FlxGrdtDeadObl.Rows.Remove(i);
                    int iRows = c1FlxGrdtDeadObl.Rows.Count - 1;
                    c1FlxGrdtDeadObl.Cols[2].Caption = "Погибшие - " + iRows.ToString();
                }
            }
        }

        private void c1FlxGrdtDeadObl_AfterEdit(object sender, RowColEventArgs e)
        {
            string strRecMsg = c1FlxGrdtDeadObl.GetDataDisplay(e.Row, 2);
            string strSqlTextMsg = "UPDATE ReportFireDead SET RptDeadName = '" + strRecMsg +
                                            "' WHERE RptDeadId = " + c1FlxGrdtDeadObl.GetDataDisplay(e.Row, 1);
            SqlCommand command = new SqlCommand(strSqlTextMsg, FrmMain.Instance.m_cnn);
            Int32 recordsAffected = command.ExecuteNonQuery();
        }

        private void txtBoxVictimPeopleObl_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Return)
            {
                e.Handled = true;
                if (txtBoxVictimPeopleObl.Text == "")
                {
                    MessageBox.Show("Введите ФИО пострадавшего", "Пострадавшие",
                        MessageBoxButtons.OK, MessageBoxIcon.Stop);
                }
                else
                {
                    string strSqlTextMsg = "INSERT INTO ReportFireVictim " +
                            "(RptVictimId, RptDispId, RptVictimName) " +
                            "Values('0', '" + strRptDispId + "', '" + txtBoxVictimPeopleObl.Text + "')";

                    SqlCommand command = new SqlCommand(strSqlTextMsg, FrmMain.Instance.m_cnn);
                    Int32 recordsAffected = command.ExecuteNonQuery();

                    //обнавляет данные в таблице c1FlxGrdVictim
                    c1FlxGrdVictimObl.Clear(ClearFlags.Content);
                    c1FlxGrdVictimObl.Rows.Count = 1;

                    txtBoxVictimPeopleObl.Text = "";
                    LoadDataVictimList();
                }

            }
        }

        private void c1FlxGrdVictimObl_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Delete)
            {
                string strVictimName;
                int i = c1FlxGrdVictimObl.RowSel;
                strVictimName = "Удалить " + c1FlxGrdVictimObl.GetDataDisplay(i, 2) + " из списка пострадавшие?";
                if (MessageBox.Show(strVictimName, "Пострадавшие",
                    MessageBoxButtons.YesNo, MessageBoxIcon.Question)
                    == DialogResult.Yes)
                {
                    string strRptVictimId = c1FlxGrdVictimObl.GetDataDisplay(i, 1);
                    string strSqlDelFireVictim = "DELETE FROM ReportFireVictim WHERE(RptVictimId = '" + strRptVictimId + "')";
                    SqlCommand cmnd = new SqlCommand(strSqlDelFireVictim, FrmMain.Instance.m_cnn);
                    Int32 recordsAffectedDel = cmnd.ExecuteNonQuery();
                    c1FlxGrdVictimObl.Rows.Remove(i);
                    int iRows = c1FlxGrdVictimObl.Rows.Count - 1;
                    c1FlxGrdVictimObl.Cols[2].Caption = "Пострадавшие - " + iRows.ToString();
                }
            }
        }

        private void c1FlxGrdVictimObl_AfterEdit(object sender, RowColEventArgs e)
        {
            string strRecMsg = c1FlxGrdVictimObl.GetDataDisplay(e.Row, 2);
            string strSqlTextMsg = "UPDATE ReportFireVictim SET RptVictimName = '" + strRecMsg +
                                            "' WHERE RptVictimId = " + c1FlxGrdVictimObl.GetDataDisplay(e.Row, 1);
            SqlCommand command = new SqlCommand(strSqlTextMsg, FrmMain.Instance.m_cnn);
            Int32 recordsAffected = command.ExecuteNonQuery();
        }

        private void txtBoxGuiltyPeopleObl_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Return)
            {
                e.Handled = true;
                if (txtBoxGuiltyPeopleObl.Text == "")
                {
                    MessageBox.Show("Введите ФИО виновного", "Виновные",
                        MessageBoxButtons.OK, MessageBoxIcon.Stop);
                }
                else
                {
                    string strSqlTextMsg = "INSERT INTO ReportFireGuilty " +
                             "(RptGuiltyId, RptDispId, RptGuiltyName) " +
                             "Values('0', '" + strRptDispId + "', '" + txtBoxGuiltyPeopleObl.Text + "')";

                    SqlCommand command = new SqlCommand(strSqlTextMsg, FrmMain.Instance.m_cnn);
                    Int32 recordsAffected = command.ExecuteNonQuery();

                    //обнавляет данные в таблице c1FlxGrdGuilty
                    c1FlxGrdGuiltyObl.Clear(ClearFlags.Content);
                    c1FlxGrdGuiltyObl.Rows.Count = 1;

                    txtBoxGuiltyPeopleObl.Text = "";
                    LoadDataGuiltyList();
                }
            }
        }

        private void c1FlxGrdGuiltyObl_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Delete)
            {
                string strGuiltyName;
                int i = c1FlxGrdGuiltyObl.RowSel;
                strGuiltyName = "Удалить " + c1FlxGrdGuiltyObl.GetDataDisplay(i, 2) + " из списка виновные?";
                if (MessageBox.Show(strGuiltyName, "Виновные",
                    MessageBoxButtons.YesNo, MessageBoxIcon.Question)
                    == DialogResult.Yes)
                {
                    string strRptGuiltyId = c1FlxGrdGuiltyObl.GetDataDisplay(i, 1);
                    string strSqlDelFireGuilty = "DELETE FROM ReportFireGuilty WHERE(RptGuiltyId = '" + strRptGuiltyId + "')";
                    SqlCommand cmnd = new SqlCommand(strSqlDelFireGuilty, FrmMain.Instance.m_cnn);
                    Int32 recordsAffectedDel = cmnd.ExecuteNonQuery();
                    c1FlxGrdGuiltyObl.Rows.Remove(i);
                    int iRows = c1FlxGrdGuiltyObl.Rows.Count - 1;
                    c1FlxGrdGuiltyObl.Cols[2].Caption = "Виновные - " + iRows.ToString();
                }
            }
        }

        private void c1FlxGrdGuiltyObl_AfterEdit(object sender, RowColEventArgs e)
        {
            string strRecMsg = c1FlxGrdGuiltyObl.GetDataDisplay(e.Row, 2);
            string strSqlTextMsg = "UPDATE ReportFireGuilty SET RptGuiltyName = '" + strRecMsg +
                                            "' WHERE RptGuiltyId = " + c1FlxGrdGuiltyObl.GetDataDisplay(e.Row, 1);
            SqlCommand command = new SqlCommand(strSqlTextMsg, FrmMain.Instance.m_cnn);
            Int32 recordsAffected = command.ExecuteNonQuery();
        }

        private void btnRecDataObl_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Записать информацию?", "Информация о хозяевах",
                    MessageBoxButtons.YesNo, MessageBoxIcon.Question)
                    == DialogResult.Yes)
            {
                string strSqlTextRecData = "UPDATE ReportFireObl SET RptMasterFire = '" + txtBoxMasterFireObl.Text + "' " +
                                               "WHERE RptDispId = " + strRptDispId;
                SqlCommand command = new SqlCommand(strSqlTextRecData, FrmMain.Instance.m_cnn);
                Int32 recordsAffected = command.ExecuteNonQuery();
                //l_blRecMasterFire = false;
                //btnRecDataObl.Enabled = false;
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
                    string strSqlTextRecData = "UPDATE ReportFireObl SET RptDeadAnmKrs = '" + c1TxtBoxKrs.Text + "' " +
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
                    string strSqlTextRecData = "UPDATE ReportFireObl SET RptDeadAnmMrs = '" + c1TxtBoxMrs.Text + "' " +
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
                    string strSqlTextRecData = "UPDATE ReportFireObl SET RptDeadAnmPr = '" + c1TxtBoxPr.Text + "' " +
                                               "WHERE RptDispId = " + strRptDispId;
                    SqlCommand command = new SqlCommand(strSqlTextRecData, FrmMain.Instance.m_cnn);
                    Int32 recordsAffected = command.ExecuteNonQuery();
                }
            }
        }

       
    }
}
