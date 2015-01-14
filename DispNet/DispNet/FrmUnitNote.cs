using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Collections;

namespace DispNet
{
    public partial class FrmUnitNote : Form
    {
        static public FrmUnitNote Instance;

        DataSet dsUnitNote = new DataSet();
        BindingSource bsUnitNote = new BindingSource();
        DataSet dsPchAll = new DataSet();
        BindingSource bsPchAll = new BindingSource();
        
        DataSet dsPchStaff = new DataSet();
        BindingSource bsPchStaff = new BindingSource();
        DataSet dsPchStaff_1 = new DataSet();
        BindingSource bsPchStaff_1;
        DataSet dsPchStaff_2 = new DataSet();
        BindingSource bsPchStaff_2;

        DataRow drNote;
        SqlDataAdapter daPchStaff;
        
        int iPosPchStaff = 0;   //запоминает позицию и не дает изменить в сомбо если просто открыть и закрыть
        int iPosPchStaff_1 = 0;
        int iPosPchStaff_2 = 0;

        string l_strPchId = ""; // Id п/части
        public string m_strSmenaNmbRtn = "";    //выбранный номер смены
        bool l_blTabStaff = true;   //не дает обновлять поля состава при выполнении свода техники и записи обеспечения

        public FrmUnitNote()
        {
            InitializeComponent();            
        }

        private void FrmUnitNote_Load(object sender, EventArgs e)
        {
            DataUnitNote_BS();

            string strSqlTextPchAll = "SELECT Unit.UnitName, Unit.UnitId FROM Unit INNER JOIN UnitNote ON Unit.UnitId = UnitNote.UnitId " +
                                        "ORDER BY Unit.UnitId";
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

            int iCntRws = bsUnitNote.Count;
            if (iCntRws > 0)
                DataUnitNote_Load();

            this.bsUnitNote.PositionChanged += new EventHandler(bsUnitNote_PositionChanged);
        }

        private void DataUnitNote_BS()
        {
            DataSet dsUnitNoteLoc = new DataSet();
            BindingSource bsUnitNoteLoc = new BindingSource();

            string strSqlTextUnitNote = "SELECT UnitNote.*, Unit.UnitTypeId FROM UnitNote INNER JOIN Unit ON UnitNote.UnitId = Unit.UnitId " +
                "ORDER BY UnitNote.UnitId";
            SqlDataAdapter daUnitNote = new SqlDataAdapter(strSqlTextUnitNote, FrmMain.Instance.m_cnn);
            daUnitNote.Fill(dsUnitNoteLoc, "UnitNote");
            bsUnitNoteLoc.DataSource = dsUnitNoteLoc;
            bsUnitNoteLoc.DataMember = "UnitNote";

            //делаем копии иначе не связать таблицу с навигатором
            dsUnitNote = dsUnitNoteLoc.Copy();
            bsUnitNote = new BindingSource(dsUnitNoteLoc, "UnitNote");
            //....
            bndNvgNote.BindingSource = bsUnitNote;
        }

        private void DataUnitNote_Load()
        {
            drNote = dsUnitNote.Tables["UnitNote"].Rows[bsUnitNote.Position];

            lblSmNb.Text = drNote["OrderChange"].ToString();
            lblDate.Text = drNote["OrderDate"].ToString() + " " + drNote["OrderTime"].ToString();

            l_strPchId = drNote["UnitId"].ToString();
            int iFndRow = c1CmbUnit.FindStringExact(l_strPchId, 0, 1);
            c1CmbUnit.Text = c1CmbUnit.Columns[0].CellText(iFndRow);
            ribLblUnit.Text = c1CmbUnit.Text;

            string strSqlTextUnitStaff = "SELECT Staff.StaffName, Staff.StaffName1, Staff.StaffName2 " +
                "FROM Staff INNER JOIN StaffState ON Staff.StaffCount = StaffState.StaffCount " +
                "WHERE (Staff.UnitId = '" + l_strPchId + "') AND (StaffState.StaffChange = '" + lblSmNb.Text + "') " +
                "ORDER BY Staff.StaffName, Staff.StaffName1";
            
            daPchStaff = new SqlDataAdapter(strSqlTextUnitStaff, FrmMain.Instance.m_cnn);
            daPchStaff.Fill(dsPchStaff, "Staff");
            bsPchStaff.DataSource = dsPchStaff;
            bsPchStaff.DataMember = "Staff";

            c1CmbNach.DataSource = bsPchStaff;
            c1CmbNach.ValueMember = "StaffName";
            c1CmbNach.DisplayMember = "StaffName";
            c1CmbNach.Columns[0].Caption = "Фамилия";
            c1CmbNach.Splits[0].DisplayColumns[0].HeadingStyle.HorizontalAlignment = C1.Win.C1List.AlignHorzEnum.Center;
            c1CmbNach.Columns[1].Caption = "Имя";
            c1CmbNach.Splits[0].DisplayColumns[1].HeadingStyle.HorizontalAlignment = C1.Win.C1List.AlignHorzEnum.Center;
            c1CmbNach.Columns[2].Caption = "Отчество";
            c1CmbNach.Splits[0].DisplayColumns[2].HeadingStyle.HorizontalAlignment = C1.Win.C1List.AlignHorzEnum.Center;
            c1CmbNach.Text = drNote["F_04"].ToString();

            dsPchStaff_1 = dsPchStaff.Copy();
            bsPchStaff_1 = new BindingSource(dsPchStaff_1, "Staff");
            c1CmbDisp.DataSource = bsPchStaff_1;
            c1CmbDisp.ValueMember = "StaffName";
            c1CmbDisp.DisplayMember = "StaffName";
            c1CmbDisp.Columns[0].Caption = "Фамилия";
            c1CmbDisp.Splits[0].DisplayColumns[0].HeadingStyle.HorizontalAlignment = C1.Win.C1List.AlignHorzEnum.Center;
            c1CmbDisp.Columns[1].Caption = "Имя";
            c1CmbDisp.Splits[0].DisplayColumns[1].HeadingStyle.HorizontalAlignment = C1.Win.C1List.AlignHorzEnum.Center;
            c1CmbDisp.Columns[2].Caption = "Отчество";
            c1CmbDisp.Splits[0].DisplayColumns[2].HeadingStyle.HorizontalAlignment = C1.Win.C1List.AlignHorzEnum.Center;
            c1CmbDisp.Text = drNote["F_05"].ToString();

            dsPchStaff_2 = dsPchStaff.Copy();
            bsPchStaff_2 = new BindingSource(dsPchStaff_2, "Staff");
            c1CmbRaz.DataSource = bsPchStaff_2;
            c1CmbRaz.ValueMember = "StaffName";
            c1CmbRaz.DisplayMember = "StaffName";
            c1CmbRaz.Columns[0].Caption = "Фамилия";
            c1CmbRaz.Splits[0].DisplayColumns[0].HeadingStyle.HorizontalAlignment = C1.Win.C1List.AlignHorzEnum.Center;
            c1CmbRaz.Columns[1].Caption = "Имя";
            c1CmbRaz.Splits[0].DisplayColumns[1].HeadingStyle.HorizontalAlignment = C1.Win.C1List.AlignHorzEnum.Center;
            c1CmbRaz.Columns[2].Caption = "Отчество";
            c1CmbRaz.Splits[0].DisplayColumns[2].HeadingStyle.HorizontalAlignment = C1.Win.C1List.AlignHorzEnum.Center;
            c1CmbRaz.Text = drNote["F_06"].ToString();

            if(l_blTabStaff)
                DataTabStaff();
           
        }

        private void DataTabStaff()
        {
            c1TxtBoxF_07.Text = drNote["F_07"].ToString();
            double dF_07 = Convert.ToDouble(c1TxtBoxF_07.Text);
            c1TxtBoxF_08.Text = drNote["F_08"].ToString();
            c1TxtBoxF_09.Text = drNote["F_09"].ToString();
            double dF_09 = Convert.ToDouble(c1TxtBoxF_09.Text);
            c1TxtBoxF_14.Text = drNote["F_14"].ToString();
            c1TxtBoxF_15.Text = drNote["F_15"].ToString();
            c1TxtBoxF_10.Text = drNote["F_10"].ToString();
            c1TxtBoxF_11.Text = drNote["F_11"].ToString();
            c1TxtBoxF_12.Text = drNote["F_12"].ToString();
            c1TxtBoxF_44.Text = drNote["F_44"].ToString();
            c1TxtBoxF_13.Text = drNote["F_13"].ToString();

            double dBoy = 0;
            if (dF_07 > 0 && dF_09 > 0)
            {
                dBoy = dF_09 / dF_07 * 100;
                string strBoy = dBoy.ToString();
                if (strBoy.Length > 4)
                {
                    lblBojNo.Text = "Боеспособность = " + strBoy.Substring(0, 4);
                    ribLblBoy.Text = "Боеспособность = " + strBoy.Substring(0, 4);
                }
                else
                {
                    lblBojNo.Text = "Боеспособность = " + strBoy;
                    ribLblBoy.Text = "Боеспособность = " + strBoy;
                }
            }
            if (dBoy < 75)
            {
                lblBojNo.ForeColor = Color.Red;
                lblBojNo.Visible = true;
            }
            else
                lblBojNo.Visible = false;
        }

        private void DataTabTech()
        {
            c1TxtBoxF_16.Text = drNote["F_16"].ToString();
            c1TxtBoxF_17.Text = drNote["F_17"].ToString();
            c1TxtBoxF_18.Text = drNote["F_18"].ToString();
            c1TxtBoxF_20.Text = drNote["F_20"].ToString();
            c1TxtBoxF_21.Text = drNote["F_21"].ToString();
            c1TxtBoxF_22.Text = drNote["F_22"].ToString();
            c1TxtBoxF_33.Text = drNote["F_33"].ToString();
            c1TxtBoxF_34.Text = drNote["F_34"].ToString();
            c1TxtBoxF_35.Text = drNote["F_35"].ToString();
            c1TxtBoxF_37.Text = drNote["F_37"].ToString();
            c1TxtBoxF_38.Text = drNote["F_38"].ToString();
            c1TxtBoxF_39.Text = drNote["F_39"].ToString();
            c1TxtBoxF_50.Text = drNote["F_50"].ToString();
            c1TxtBoxF_53.Text = drNote["F_53"].ToString();
            c1TxtBoxF_23.Text = drNote["F_23"].ToString();
            c1TxtBoxF_24.Text = drNote["F_24"].ToString();
            c1TxtBoxF_25.Text = drNote["F_25"].ToString();
            c1TxtBoxF_28.Text = drNote["F_28"].ToString();
            c1TxtBoxF_40.Text = drNote["F_40"].ToString();
            c1TxtBoxF_41.Text = drNote["F_41"].ToString();
            c1TxtBoxF_42.Text = drNote["F_42"].ToString();
            c1TxtBoxF_45.Text = drNote["F_45"].ToString();
            c1TxtBoxF_51.Text = drNote["F_51"].ToString();
            c1TxtBoxF_54.Text = drNote["F_54"].ToString();
            c1TxtBoxF_29.Text = drNote["F_29"].ToString();
            c1TxtBoxF_30.Text = drNote["F_30"].ToString();
            c1TxtBoxF_31.Text = drNote["F_31"].ToString();
            c1TxtBoxF_32.Text = drNote["F_32"].ToString();
            c1TxtBoxF_46.Text = drNote["F_46"].ToString();
            c1TxtBoxF_47.Text = drNote["F_47"].ToString();
            c1TxtBoxF_48.Text = drNote["F_48"].ToString();
            c1TxtBoxF_49.Text = drNote["F_49"].ToString();
            c1TxtBoxF_52.Text = drNote["F_52"].ToString();
            c1TxtBoxF_55.Text = drNote["F_55"].ToString();
        }

        private void DataTabObesp()
        {
            c1TxtBoxF_56.Text = drNote["F_56"].ToString();
            c1TxtBoxF_58.Text = drNote["F_58"].ToString();
            c1TxtBoxF_59.Text = drNote["F_59"].ToString();
            c1TxtBoxF_60.Text = drNote["F_60"].ToString();
            c1TxtBoxF_61.Text = drNote["F_61"].ToString();
            c1TxtBoxF_92.Text = drNote["F_92"].ToString();
            c1TxtBoxF_67.Text = drNote["F_67"].ToString();
            c1TxtBoxF_57.Text = drNote["F_57"].ToString();
            c1TxtBoxF_62.Text = drNote["F_62"].ToString();
            c1TxtBoxF_63.Text = drNote["F_63"].ToString();
            c1TxtBoxF_64.Text = drNote["F_64"].ToString();
            c1TxtBoxF_65.Text = drNote["F_65"].ToString();
            c1TxtBoxF_93.Text = drNote["F_93"].ToString();
            c1TxtBoxF_68.Text = drNote["F_68"].ToString();
            c1TxtBoxF_69.Text = drNote["F_69"].ToString();
            c1TxtBoxF_70.Text = drNote["F_70"].ToString();
            c1TxtBoxF_71.Text = drNote["F_71"].ToString();
            c1TxtBoxF_72.Text = drNote["F_72"].ToString();
            c1TxtBoxF_77.Text = drNote["F_77"].ToString();
            c1TxtBoxF_78.Text = drNote["F_78"].ToString();
            c1TxtBoxF_75.Text = drNote["F_75"].ToString();
            c1TxtBoxF_76.Text = drNote["F_76"].ToString();
            c1TxtBoxF_79.Text = drNote["F_79"].ToString();
            c1TxtBoxF_80.Text = drNote["F_80"].ToString();
            c1TxtBoxF_81.Text = drNote["F_81"].ToString();
            c1TxtBoxF_82.Text = drNote["F_82"].ToString();
            c1TxtBoxF_87.Text = drNote["F_87"].ToString();
            c1TxtBoxF_88.Text = drNote["F_88"].ToString();
            c1TxtBoxF_83.Text = drNote["F_83"].ToString();
            c1TxtBoxF_84.Text = drNote["F_84"].ToString();
            c1TxtBoxF_85.Text = drNote["F_85"].ToString();
            c1TxtBoxF_86.Text = drNote["F_86"].ToString();
            c1TxtBoxF_89.Text = drNote["F_89"].ToString();
            c1TxtBoxF_90.Text = drNote["F_90"].ToString();
            c1TxtBoxF_73.Text = drNote["F_73"].ToString();
            c1TxtBoxF_74.Text = drNote["F_74"].ToString();
            c1TxtBoxF_19.Text = drNote["F_19"].ToString();
            c1TxtBoxF_36.Text = drNote["F_36"].ToString();
            c1TxtBoxF_26.Text = drNote["F_26"].ToString();
            c1TxtBoxF_43.Text = drNote["F_43"].ToString();
            c1TxtBoxF_66.Text = drNote["F_66"].ToString();
        }

        private void c1DocTabUnitNote_SelectedIndexChanged(object sender, EventArgs e)
        {
            int iSelTab = c1DocTabUnitNote.SelectedIndex;

            if (iSelTab == 0)
                DataTabStaff();

            if (iSelTab == 1)
                DataTabTech();

            if (iSelTab == 2)
                DataTabObesp();
        }

        private void bsUnitNote_PositionChanged(Object sender, EventArgs e)
        {
            DataUnitNote_Load();
        }

        private void c1CmbNach_Close(object sender, EventArgs e)
        {
            if (iPosPchStaff != bsPchStaff.Position)
            {
                string strUnitName = c1CmbUnit.Text;
                string strCap = "Начальник караула для " + strUnitName;
                if (MessageBox.Show("Записать информацию?", strCap,
                        MessageBoxButtons.YesNo, MessageBoxIcon.Question)
                        == DialogResult.Yes)
                {
                    DataRow drPchStaff;
                    drPchStaff = dsPchStaff.Tables["Staff"].Rows[bsPchStaff.Position];

                    c1CmbNach.Text = drPchStaff["StaffName"].ToString() + " " + drPchStaff["StaffName1"].ToString() +
                        " " + drPchStaff["StaffName2"].ToString();
                    iPosPchStaff = bsPchStaff.Position;

                    DateTime myDateTime;
                    myDateTime = DateTime.Now;
                    string strDate = myDateTime.Day.ToString() + "." + myDateTime.Month.ToString() + "." + myDateTime.Year.ToString();
                    string strTime = myDateTime.ToString("HH:mm:ss");

                    DataRow drNoteStaff;
                    drNoteStaff = dsUnitNote.Tables["UnitNote"].Rows[bsUnitNote.Position];
                    string strOrderId = drNoteStaff["OrderId"].ToString();

                    string strSqlTextRecStaff = "UPDATE UnitNote SET OrderDate = '" + strDate + "', OrderTime = '" + strTime + "', " +
                            "F_04 = '" + c1CmbNach.Text + "' WHERE OrderId = " + strOrderId;
                    SqlCommand cmndRecStaff = new SqlCommand(strSqlTextRecStaff, FrmMain.Instance.m_cnn);
                    Int32 recordsAffectedRecStaff = cmndRecStaff.ExecuteNonQuery();
                }
            }
        }

        private void c1CmbDisp_Close(object sender, EventArgs e)
        {
            if (iPosPchStaff_1 != bsPchStaff_1.Position)
            {
                string strUnitName = c1CmbUnit.Text;
                string strCap = "Диспетчер для " + strUnitName;
                if (MessageBox.Show("Записать информацию?", strCap,
                        MessageBoxButtons.YesNo, MessageBoxIcon.Question)
                        == DialogResult.Yes)
                {
                    DataRow drPchStaff;
                    drPchStaff = dsPchStaff_1.Tables["Staff"].Rows[bsPchStaff_1.Position];

                    c1CmbDisp.Text = drPchStaff["StaffName"].ToString() + " " + drPchStaff["StaffName1"].ToString() +
                        " " + drPchStaff["StaffName2"].ToString();
                    iPosPchStaff_1 = bsPchStaff_1.Position;

                    DateTime myDateTime;
                    myDateTime = DateTime.Now;
                    string strDate = myDateTime.Day.ToString() + "." + myDateTime.Month.ToString() + "." + myDateTime.Year.ToString();
                    string strTime = myDateTime.ToString("HH:mm:ss");

                    DataRow drNoteStaff;
                    drNoteStaff = dsUnitNote.Tables["UnitNote"].Rows[bsUnitNote.Position];
                    string strOrderId = drNoteStaff["OrderId"].ToString();

                    string strSqlTextRecStaff = "UPDATE UnitNote SET OrderDate = '" + strDate + "', OrderTime = '" + strTime + "', " +
                            "F_05 = '" + c1CmbDisp.Text + "' WHERE OrderId = " + strOrderId;
                    SqlCommand cmndRecStaff = new SqlCommand(strSqlTextRecStaff, FrmMain.Instance.m_cnn);
                    Int32 recordsAffectedRecStaff = cmndRecStaff.ExecuteNonQuery();
                }
            }
        }

        private void c1CmbRaz_Close(object sender, EventArgs e)
        {
            if (iPosPchStaff_2 != bsPchStaff_2.Position)
            {
                string strUnitName = c1CmbUnit.Text;
                string strCap = "Разводящий для " + strUnitName;
                if (MessageBox.Show("Записать информацию?", strCap,
                        MessageBoxButtons.YesNo, MessageBoxIcon.Question)
                        == DialogResult.Yes)
                {
                    DataRow drPchStaff;
                    drPchStaff = dsPchStaff_2.Tables["Staff"].Rows[bsPchStaff_2.Position];

                    c1CmbRaz.Text = drPchStaff["StaffName"].ToString() + " " + drPchStaff["StaffName1"].ToString() +
                        " " + drPchStaff["StaffName2"].ToString();
                    iPosPchStaff_2 = bsPchStaff_2.Position;

                    DateTime myDateTime;
                    myDateTime = DateTime.Now;
                    string strDate = myDateTime.Day.ToString() + "." + myDateTime.Month.ToString() + "." + myDateTime.Year.ToString();
                    string strTime = myDateTime.ToString("HH:mm:ss");

                    DataRow drNoteStaff;
                    drNoteStaff = dsUnitNote.Tables["UnitNote"].Rows[bsUnitNote.Position];
                    string strOrderId = drNoteStaff["OrderId"].ToString();

                    string strSqlTextRecStaff = "UPDATE UnitNote SET OrderDate = '" + strDate + "', OrderTime = '" + strTime + "', " +
                            "F_06 = '" + c1CmbRaz.Text + "' WHERE OrderId = " + strOrderId;
                    SqlCommand cmndRecStaff = new SqlCommand(strSqlTextRecStaff, FrmMain.Instance.m_cnn);
                    Int32 recordsAffectedRecStaff = cmndRecStaff.ExecuteNonQuery();
                }
            }
        }

        private void c1CmbUnit_Close(object sender, EventArgs e)
        {
            if (c1CmbUnit.ListCount > 0)    //только если есть записи иначе ошибка
            {
                int iFndRow = c1CmbUnit.FindStringExact(c1CmbUnit.Text, 0, 0);
                string strUnitId = c1CmbUnit.Columns[1].CellText(iFndRow);
                int itemFound = bsPchAll.Find("UnitId", strUnitId);
                bsUnitNote.Position = itemFound;
            }
        }

        private void btnRecTech_Click(object sender, EventArgs e)
        {
            string strUnitName = c1CmbUnit.Text;
            string strCap = "Свод техники для " + strUnitName;
            if (MessageBox.Show("Записать информацию?", strCap,
                    MessageBoxButtons.YesNo, MessageBoxIcon.Question)
                    == DialogResult.Yes)
            {
                DataRow drUnitNote;
                drUnitNote = dsUnitNote.Tables["UnitNote"].Rows[bsUnitNote.Position];
                string strPchId = drUnitNote["UnitId"].ToString();
                string strOrderId = drUnitNote["OrderId"].ToString();

                UpdateTechnicsRecUnitNote(strPchId, strOrderId);

                l_blTabStaff = false;
                UpdateDataSetUnitNote();    //обновляет набор записей в форме
                int itemFound = bsUnitNote.Find("UnitId", strPchId);
                bsUnitNote.Position = itemFound;
                l_blTabStaff = true;
                DataTabTech();
            }
        }

        private void btnRecSupp_Click(object sender, EventArgs e)
        {
            string strUnitName = c1CmbUnit.Text;
            string strCap = "Обеспечение для " + strUnitName;
            if (MessageBox.Show("Записать информацию?", strCap,
                    MessageBoxButtons.YesNo, MessageBoxIcon.Question)
                    == DialogResult.Yes)
            {
                DateTime myDateTime;
                myDateTime = DateTime.Now;
                string strDate = myDateTime.Day.ToString() + "." + myDateTime.Month.ToString() + "." + myDateTime.Year.ToString();
                string strTime = myDateTime.ToString("HH:mm:ss");

                DataRow drNoteStaff;
                drNoteStaff = dsUnitNote.Tables["UnitNote"].Rows[bsUnitNote.Position];
                string strPchId = drNoteStaff["UnitId"].ToString();
                string strOrderId = drNoteStaff["OrderId"].ToString();

                string strSqlTextRecSupp = "UPDATE UnitNote SET OrderDate = '" + strDate + "', OrderTime = '" + strTime + "', " +
                        "F_56 = '" + c1TxtBoxF_56.Text + "', F_57 = '" + c1TxtBoxF_57.Text + "', F_58 = '" + c1TxtBoxF_58.Text + "', " +
                        "F_59 = '" + c1TxtBoxF_59.Text + "', F_60 = '" + c1TxtBoxF_60.Text + "', F_61 = '" + c1TxtBoxF_61.Text + "', " +
                        "F_92 = '" + c1TxtBoxF_92.Text + "', F_67 = '" + c1TxtBoxF_67.Text + "', F_62 = '" + c1TxtBoxF_62.Text + "', " +
                        "F_63 = '" + c1TxtBoxF_63.Text + "', F_64 = '" + c1TxtBoxF_64.Text + "', F_65 = '" + c1TxtBoxF_65.Text + "', " +
                        "F_93 = '" + c1TxtBoxF_93.Text + "', F_68 = '" + c1TxtBoxF_68.Text + "', F_69 = '" + c1TxtBoxF_69.Text + "', " +
                        "F_71 = '" + c1TxtBoxF_71.Text + "', F_77 = '" + c1TxtBoxF_77.Text + "', F_75 = '" + c1TxtBoxF_75.Text + "', " +
                        "F_79 = '" + c1TxtBoxF_79.Text + "', F_81 = '" + c1TxtBoxF_81.Text + "', F_87 = '" + c1TxtBoxF_87.Text + "', " +
                        "F_70 = '" + c1TxtBoxF_70.Text + "', F_72 = '" + c1TxtBoxF_72.Text + "', F_78 = '" + c1TxtBoxF_78.Text + "', " +
                        "F_76 = '" + c1TxtBoxF_76.Text + "', F_80 = '" + c1TxtBoxF_80.Text + "', F_82 = '" + c1TxtBoxF_82.Text + "', " +
                        "F_88 = '" + c1TxtBoxF_88.Text + "', F_83 = '" + c1TxtBoxF_83.Text + "', F_84 = '" + c1TxtBoxF_84.Text + "', " +
                        "F_19 = '" + c1TxtBoxF_19.Text + "', F_36 = '" + c1TxtBoxF_36.Text + "', F_26 = '" + c1TxtBoxF_26.Text + "', " +
                        "F_43 = '" + c1TxtBoxF_43.Text + "', F_85 = '" + c1TxtBoxF_85.Text + "', F_86 = '" + c1TxtBoxF_86.Text + "', " +
                        "F_89 = '" + c1TxtBoxF_89.Text + "', F_90 = '" + c1TxtBoxF_90.Text + "', F_73 = '" + c1TxtBoxF_73.Text + "', " +
                        "F_74 = '" + c1TxtBoxF_74.Text + "', F_66 = '" + c1TxtBoxF_66.Text + "'WHERE OrderId = " + strOrderId;
                SqlCommand cmndRecSupp = new SqlCommand(strSqlTextRecSupp, FrmMain.Instance.m_cnn);
                Int32 recordsAffectedRecSupp = cmndRecSupp.ExecuteNonQuery();

                l_blTabStaff = false;
                UpdateDataSetUnitNote();    //обновляет набор записей в форме
                int itemFound = bsUnitNote.Find("UnitId", strPchId);
                bsUnitNote.Position = itemFound;
                l_blTabStaff = true;
                DataTabObesp();
            }
        }

        private void btnTechNote_Click(object sender, EventArgs e)
        {
            FrmTechUnit.Instance = new FrmTechUnit();
            FrmTechUnit.Instance.m_strSngNotePch = l_strPchId;
            FrmTechUnit.Instance.m_frTechNote_Load();
            FrmTechUnit.Instance.ShowDialog();
        }

        private void btnStaffNote_Click(object sender, EventArgs e)
        {
            FrmStaffNote.Instance = new FrmStaffNote();
            FrmStaffNote.Instance.m_strPchId = l_strPchId;
            FrmStaffNote.Instance.m_strSmena = lblSmNb.Text;
            FrmStaffNote.Instance.ShowDialog();
        }

        private void btnChgSmn_Click(object sender, EventArgs e)
        {
            m_strSmenaNmbRtn = "";
            FrmInputSmena.Instance = new FrmInputSmena();
            FrmInputSmena.Instance.m_strSmenaNmb = lblSmNb.Text;
            FrmInputSmena.Instance.m_iSngFrm = 2;
            FrmInputSmena.Instance.ShowDialog();

            if (m_strSmenaNmbRtn != "")
            {
                UpdateSvodGorodObl(1, "500");
                UpdateSvodGorodObl(2, "501");

                string strStaffState = "Отдых";
                string strStateBr = "Дежурство в б/р";
                string strStateDr = "На дежурстве";
                string strSqlStaffState = "UPDATE StaffState SET StaffState = '" + strStaffState + "' " +
                    "WHERE (StaffState = '" + strStateBr + "' OR StaffState = '" + strStateDr + "') " +
                    "AND (StaffChange <> '0' AND StaffChange <> '" + m_strSmenaNmbRtn + "')";

                SqlCommand command1 = new SqlCommand(strSqlStaffState, FrmMain.Instance.m_cnn);
                Int32 recordsAffected1 = command1.ExecuteNonQuery();

                strStaffState = "Дежурство в б/р";
                strStateBr = "1";
                strStateDr = "Отдых";
                strSqlStaffState = "UPDATE StaffState SET StaffState = '" + strStaffState + "' " +
                    "WHERE (StaffBattle = '" + strStateBr + "' AND StaffState = '" + strStateDr + "') " +
                    "AND (StaffChange = '" + m_strSmenaNmbRtn + "')";

                SqlCommand command2 = new SqlCommand(strSqlStaffState, FrmMain.Instance.m_cnn);
                Int32 recordsAffected2 = command2.ExecuteNonQuery();

                strStaffState = "На дежурстве";
                strStateBr = "0";
                strStateDr = "Отдых";
                strSqlStaffState = "UPDATE StaffState SET StaffState = '" + strStaffState + "' " +
                    "WHERE (StaffBattle = '" + strStateBr + "' AND StaffState = '" + strStateDr + "') " +
                    "AND (StaffChange = '" + m_strSmenaNmbRtn + "')";

                SqlCommand command3 = new SqlCommand(strSqlStaffState, FrmMain.Instance.m_cnn);
                Int32 recordsAffected3 = command3.ExecuteNonQuery();
                          
                string strPchId = "";
                string strOrderId = "";
                
                DataView dvUnitNote = new DataView(dsUnitNote.Tables["UnitNote"]);
                foreach (DataRowView rowView in dvUnitNote)
                {
                    strPchId = rowView["UnitId"].ToString();
                    strOrderId = rowView["OrderId"].ToString();                  
                    
                    UpdateStaffRecUnitNote(m_strSmenaNmbRtn, strPchId, strOrderId);
                    UpdateTechnicsRecUnitNote(strPchId, strOrderId);
                }
                
                RecDataUnitNoteArch();
                UpdateDataSetUnitNote();    //обновляет набор записей в форме
            }
        }

        private void UpdateStaffRecUnitNote(string strSmena, string strPchId, string strOrderId)
        {
            DateTime myDateTime = DateTime.Now;
            string strDate = myDateTime.Day.ToString() + "." + myDateTime.Month.ToString() + "." + myDateTime.Year.ToString();
            string strTime = myDateTime.ToString("HH:mm:ss");

            string strSqlTextStaffState = "SELECT StaffState.StaffState, StaffState.StaffChange, " +
                            "StaffState.StaffGAZS, StaffState.StaffBattle, StaffState.StaffCount " +
                            "FROM Staff INNER JOIN StaffState ON Staff.StaffCount = StaffState.StaffCount " +
                            "INNER JOIN Unit ON Staff.UnitId = Unit.UnitId " +
                            "WHERE (StaffState.StaffChange = '" + strSmena + "') AND (Staff.UnitId = '" + strPchId + "') " +
                            "ORDER BY StaffState.StaffCount";
            SqlCommand cmd = new SqlCommand(strSqlTextStaffState, FrmMain.Instance.m_cnn);
            SqlDataReader rdrStaffState = cmd.ExecuteReader();
            string strStaffState = "";
            bool blGAZS = false;
            int iStaffCnt = 0;
            int iGAZS = 0;
            int iDegr = 0;
            int iBr = 0;
            int iComr = 0;
            int iOtp = 0;
            int iBoln = 0;
            int iUchb = 0;
            int iElse = 0;
            while (rdrStaffState.Read())
            {
                if (!rdrStaffState.IsDBNull(2))
                    blGAZS = rdrStaffState.GetBoolean(2);

                if (!rdrStaffState.IsDBNull(0))
                {
                    strStaffState = rdrStaffState.GetString(0);
                    switch (strStaffState)
                    {
                        case "На дежурстве":
                            if (blGAZS)
                                iGAZS++;
                            iDegr++;
                            break;
                        case "Дежурство в б/р":
                            if (blGAZS)
                                iGAZS++;
                            iBr++;
                            break;
                        case "Командировка":
                            iComr++;
                            break;
                        case "Отпуск":
                            iOtp++;
                            break;
                        case "Болен":
                            iBoln++;
                            break;
                        case "Учеба":
                            iUchb++;
                            break;
                        default:
                            iElse++;
                            break;
                    }
                }
                iStaffCnt++;
            }
            rdrStaffState.Close();

            int iNal = iDegr + iBr;
            string strSqlUnitNote = "UPDATE UnitNote SET OrderChange = '" + strSmena + "', OrderDate = '" + strDate + "', " +
                "OrderTime = '" + strTime + "', F_08 = '" + iStaffCnt + "', F_09 = '" + iNal + "', F_14 = '" + iBr + "', " +
                "F_15 = '" + iGAZS + "', F_10 = '" + iComr + "', F_11 = '" + iOtp + "', F_12 = '" + iBoln + "', " +
                "F_44 = '" + iUchb + "', F_13 = '" + iElse + "' " +
                "WHERE (OrderId = '" + strOrderId + "')";

            SqlCommand command = new SqlCommand(strSqlUnitNote, FrmMain.Instance.m_cnn);
            Int32 recordsAffected = command.ExecuteNonQuery();
        }

        private void UpdateTechnicsRecUnitNote(string strPchId, string strOrderId)
        {
            DateTime myDateTime = DateTime.Now;
            string strDate = myDateTime.Day.ToString() + "." + myDateTime.Month.ToString() + "." + myDateTime.Year.ToString();
            string strTime = myDateTime.ToString("HH:mm:ss");

            int intVidRem1 = 0;
            int intVidRem2 = 0;
            int intVidRem3 = 0;
            int intVidTO1 = 0;
            int intVidTO2 = 0;
            int intVidTO3 = 0;
            int intVid1 = 0;
            int intVid2 = 0;
            int intVid3 = 0;
            int intVid1r = 0;
            int intVid2r = 0;
            int intVid3r = 0;

            int intAbvAC = 0;
            int intAbvAP = 0;
            int intAbvPNS = 0;
            int intAbvMP = 0;
            int intAbvPP = 0;
            int intAbvAL = 0;
            int intAbvASO = 0;
            int intAbvAT = 0;
            int intAbvAVT = 0;
            int intAbvGA = 0;
            int intAbvLA = 0;
            int intAbvACr = 0;
            int intAbvAPr = 0;
            int intAbvPNSr = 0;
            int intAbvMPr = 0;
            int intAbvPPr = 0;
            int intAbvALr = 0;
            int intAbvASOr = 0;
            int intAbvATr = 0;
            int intAbvAVTr = 0;
            int intAbvGAr = 0;
            int intAbvLAr = 0;

            string strSqlTextTechUnit = "SELECT StateId, TechnicsVid, TechnicsAbbrev, TechnicsId " +
                            "FROM TechnicsUnit WHERE UnitId = '" + strPchId + "' ORDER BY TechnicsId";
            SqlCommand cmd = new SqlCommand(strSqlTextTechUnit, FrmMain.Instance.m_cnn);
            SqlDataReader rdrTechUnit = cmd.ExecuteReader();

            int iStateTech = 0;
            string strTechAbv = "";
            int intTechVid = 0;
            while (rdrTechUnit.Read())
            {
                if (!rdrTechUnit.IsDBNull(0))
                    iStateTech = rdrTechUnit.GetInt16(0);
                if (!rdrTechUnit.IsDBNull(1))
                    intTechVid = rdrTechUnit.GetInt16(1);
                if (!rdrTechUnit.IsDBNull(2))
                    strTechAbv = rdrTechUnit.GetString(2);

                if (iStateTech == 12 || iStateTech == 13 || iStateTech == 14)   //12-Ремонт, 13-ТО-1, 14-ТО-2
                {
                    if (iStateTech == 12)   //12-Ремонт
                    {
                        switch (intTechVid)
                        {
                            case 1:
                                intVidRem1++;
                                break;
                            case 2:
                                intVidRem2++;
                                break;
                            case 3:
                                intVidRem3++;
                                break;
                        }
                    }
                    else
                    {
                        switch (intTechVid)
                        {
                            case 1:
                                intVidTO1++;
                                break;
                            case 2:
                                intVidTO2++;
                                break;
                            case 3:
                                intVidTO3++;
                                break;
                        }
                    }
                }
                else
                {
                    if (iStateTech == 1)   //1-В pасчете
                    {
                        switch (strTechAbv)
                        {
                            case "АЦ":
                                intAbvAC++;
                                break;
                            case "АП":
                                intAbvAP++;
                                break;
                            case "ПНС":
                                intAbvPNS++;
                                break;
                            case "МП":
                                intAbvMP++;
                                break;
                            case "ПП":
                                intAbvPP++;
                                break;
                            case "АЛ":
                                intAbvAL++;
                                break;
                            case "АСО":
                                intAbvASO++;
                                break;
                            case "АТ":
                                intAbvAT++;
                                break;
                            case "АВТ":
                                intAbvAVT++;
                                break;
                            case "ЛА":
                                intAbvLA++;
                                break;
                            case "ГА":
                                intAbvGA++;
                                break;
                            default:
                                switch (intTechVid)
                                {
                                    case 1:
                                        intVid1++;
                                        break;
                                    case 2:
                                        intVid2++;
                                        break;
                                    case 3:
                                        intVid3++;
                                        break;
                                }
                                break;
                        }
                    }

                    if (iStateTech == 2)   //2-В резерве
                    {
                        switch (strTechAbv)
                        {
                            case "АЦ":
                                intAbvACr++;
                                break;
                            case "АП":
                                intAbvAPr++;
                                break;
                            case "ПНС":
                                intAbvPNSr++;
                                break;
                            case "МП":
                                intAbvMPr++;
                                break;
                            case "ПП":
                                intAbvPPr++;
                                break;
                            case "АЛ":
                                intAbvALr++;
                                break;
                            case "АСО":
                                intAbvASOr++;
                                break;
                            case "АТ":
                                intAbvATr++;
                                break;
                            case "АВТ":
                                intAbvAVTr++;
                                break;
                            case "ЛА":
                                intAbvLAr++;
                                break;
                            case "ГА":
                                intAbvGAr++;
                                break;
                            default:
                                switch (intTechVid)
                                {
                                    case 1:
                                        intVid1r++;
                                        break;
                                    case 2:
                                        intVid2r++;
                                        break;
                                    case 3:
                                        intVid3r++;
                                        break;
                                }
                                break;
                        }
                    }
                }
            }
            rdrTechUnit.Close();

            string strSqlUnitNote = "UPDATE UnitNote SET OrderDate = '" + strDate + "', OrderTime = '" + strTime + "', " +
                "F_16 = '" + intAbvAC + "', F_17 = '" + intAbvAP + "', F_18 = '" + intAbvPNS + "', F_20 = '" + intAbvMP + "', " +
                "F_21 = '" + intAbvPP + "', F_22 = '" + intVid1 + "', F_23 = '" + intAbvAL + "', F_24 = '" + intAbvASO + "', " +
                "F_25 = '" + intAbvAT + "', F_28 = '" + intVid2 + "', F_29 = '" + intAbvAVT + "', F_30 = '" + intAbvLA + "', " +
                "F_31 = '" + intAbvGA + "', F_32 = '" + intVid3 + "', F_33 = '" + intAbvACr + "', F_34 = '" + intAbvAPr + "', " +
                "F_35 = '" + intAbvPNSr + "', F_37 = '" + intAbvMPr + "', F_38 = '" + intAbvPPr + "', F_39 = '" + intVid1r + "', " +
                "F_40 = '" + intAbvALr + "', F_41 = '" + intAbvASOr + "', F_42 = '" + intAbvATr + "', F_45 = '" + intVid2r + "', " +
                "F_46 = '" + intAbvAVTr + "', F_47 = '" + intAbvLAr + "', F_48 = '" + intAbvGAr + "', F_49 = '" + intVid3r + "', " +
                "F_50 = '" + intVidTO1 + "', F_51 = '" + intVidTO2 + "', F_52 = '" + intVidTO3 + "', F_53 = '" + intVidRem1 + "', " +
                "F_54 = '" + intVidRem2 + "', F_55 = '" + intVidRem3 + "' " +
                "WHERE (OrderId = '" + strOrderId + "')";

            SqlCommand command = new SqlCommand(strSqlUnitNote, FrmMain.Instance.m_cnn);
            Int32 recordsAffected = command.ExecuteNonQuery();
        }

        private void UpdateSvodGorodObl(int iUnitType, string strUnitId)
        {
            string strSmena = lblSmNb.Text;
            string strUnitType = iUnitType.ToString();

            DataView dvUnitNote = new DataView(dsUnitNote.Tables["UnitNote"], "UnitTypeId = '" + strUnitType + "'",
                            "UnitId", DataViewRowState.CurrentRows);

            String[] RowAr = new String[95];// всего 93 поля начинается запись с 7 поля итого 93-6 = 87
            foreach (DataRowView rowView in dvUnitNote)
            {
                for (int i = 8; i < 95; i = i + 1)
                {
                    int iCnt = Convert.ToInt32(RowAr[i]);
                    iCnt = iCnt + Convert.ToInt32(rowView.Row[i].ToString());
                    RowAr[i] = iCnt.ToString();
                }
            }

            DateTime myDateTime = DateTime.Now;
            string strDate = myDateTime.Day.ToString() + "." + myDateTime.Month.ToString() + "." + myDateTime.Year.ToString();
            string strTime = myDateTime.ToString("HH:mm:ss");

            string strSqlUnitNoteArch = "UPDATE UnitNote SET OrderChange = '" + strSmena + "', OrderDate = '" + strDate + "', " +
            "OrderTime = '" + strTime + "', F_07 = '" + RowAr[8] + "', F_08 = '" + RowAr[9] + "', F_09 = '" + RowAr[10] + "', " +
            "F_10 = '" + RowAr[11] + "', F_11 = '" + RowAr[12] + "', F_12 = '" + RowAr[13] + "', F_13 = '" + RowAr[14] + "', F_14 = '" + RowAr[15] + "', " +
            "F_15 = '" + RowAr[16] + "', F_16 = '" + RowAr[17] + "', F_17 = '" + RowAr[18] + "', F_18 = '" + RowAr[19] + "', F_19 = '" + RowAr[20] + "', " +
            "F_20 = '" + RowAr[21] + "', F_21 = '" + RowAr[22] + "', F_22 = '" + RowAr[23] + "', F_23 = '" + RowAr[24] + "', F_24 = '" + RowAr[25] + "', " +
            "F_25 = '" + RowAr[26] + "', F_26 = '" + RowAr[27] + "', F_27 = '" + RowAr[28] + "', F_28 = '" + RowAr[29] + "', F_29 = '" + RowAr[30] + "', " +
            "F_30 = '" + RowAr[31] + "', F_31 = '" + RowAr[32] + "', F_32 = '" + RowAr[33] + "', F_33 = '" + RowAr[34] + "', F_34 = '" + RowAr[35] + "', " +
            "F_35 = '" + RowAr[36] + "', F_36 = '" + RowAr[37] + "', F_37 = '" + RowAr[38] + "', F_38 = '" + RowAr[39] + "', F_39 = '" + RowAr[40] + "', " +
            "F_40 = '" + RowAr[41] + "', F_41 = '" + RowAr[42] + "', F_42 = '" + RowAr[43] + "', F_43 = '" + RowAr[44] + "', F_44 = '" + RowAr[45] + "', " +
            "F_45 = '" + RowAr[46] + "', F_46 = '" + RowAr[47] + "', F_47 = '" + RowAr[48] + "', F_48 = '" + RowAr[49] + "', F_49 = '" + RowAr[50] + "', " +
            "F_50 = '" + RowAr[51] + "', F_51 = '" + RowAr[52] + "', F_52 = '" + RowAr[53] + "', F_53 = '" + RowAr[54] + "', F_54 = '" + RowAr[55] + "', " +
            "F_56 = '" + RowAr[57] + "', F_57 = '" + RowAr[58] + "', F_58 = '" + RowAr[59] + "', F_59 = '" + RowAr[60] + "', F_60 = '" + RowAr[61] + "', " +
            "F_61 = '" + RowAr[62] + "', F_62 = '" + RowAr[63] + "', F_63 = '" + RowAr[64] + "', F_64 = '" + RowAr[65] + "', F_65 = '" + RowAr[66] + "', " +
            "F_66 = '" + RowAr[67] + "', F_67 = '" + RowAr[68] + "', F_68 = '" + RowAr[69] + "', F_69 = '" + RowAr[70] + "', F_70 = '" + RowAr[71] + "', " +
            "F_71 = '" + RowAr[72] + "', F_72 = '" + RowAr[73] + "', F_73 = '" + RowAr[74] + "', F_74 = '" + RowAr[75] + "', F_75 = '" + RowAr[76] + "', " +
            "F_76 = '" + RowAr[77] + "', F_77 = '" + RowAr[78] + "', F_78 = '" + RowAr[79] + "', F_79 = '" + RowAr[80] + "', F_80 = '" + RowAr[81] + "', " +
            "F_81 = '" + RowAr[82] + "', F_82 = '" + RowAr[83] + "', F_83 = '" + RowAr[84] + "', F_84 = '" + RowAr[85] + "', F_85 = '" + RowAr[86] + "', " +
            "F_86 = '" + RowAr[87] + "', F_87 = '" + RowAr[88] + "', F_88 = '" + RowAr[89] + "', F_89 = '" + RowAr[90] + "', F_90 = '" + RowAr[91] + "', " +
            "F_91 = '" + RowAr[92] + "', F_92 = '" + RowAr[93] + "', F_93 = '" + RowAr[94] + "' " +
            "WHERE (UnitNote.UnitId = '" + strUnitId + "')";

            SqlCommand command = new SqlCommand(strSqlUnitNoteArch, FrmMain.Instance.m_cnn);
            Int32 recordsAffected = command.ExecuteNonQuery();

            UpdateDataSetUnitNote();    //обновляет набор записей в форме
            int itemFound = bsUnitNote.Find("UnitId", strUnitId);
            bsUnitNote.Position = itemFound;
        }

        private void UpdateDataSetUnitNote()
        {
            DataUnitNote_BS();
            int iCntRws = bsUnitNote.Count;
            if (iCntRws > 0)
                DataUnitNote_Load();
            
            this.bsUnitNote.PositionChanged += new EventHandler(bsUnitNote_PositionChanged);
        }

        private void RecDataUnitNoteArch()
        {
            DateTime myDateTime = DateTime.Now;
            string rtr = myDateTime.Day.ToString() + "." + myDateTime.Month.ToString() + "." + myDateTime.Year.ToString();
            string strDate = DateTime.Parse(rtr).ToString("dd.MM.yyyy");
            string strTime = myDateTime.ToString("HH:mm:ss");

            string strSqlDelNoteArch = "DELETE FROM UnitNoteArch WHERE(OrderDate = '" + strDate + "')";
            SqlCommand commandD = new SqlCommand(strSqlDelNoteArch, FrmMain.Instance.m_cnn);
            Int32 recordsAffectedD = commandD.ExecuteNonQuery();

            string strSqlTextUnitNoteArch = "SELECT OrderId FROM UnitNoteArch ORDER BY OrderId";
            SqlCommand cmd = new SqlCommand(strSqlTextUnitNoteArch, FrmMain.Instance.m_cnn);
            SqlDataReader rdrNoteArch = cmd.ExecuteReader();
            int iCntId = 0;
            while (rdrNoteArch.Read())
            {
                if (!rdrNoteArch.IsDBNull(0))
                    iCntId = rdrNoteArch.GetInt32(0);
            }
            rdrNoteArch.Close();
            iCntId++;

            String[] RowAr = new String[95];// всего 93 поля начинается запись с 7 поля итого 93-6 = 87

            DataView dvUnitNote = new DataView(dsUnitNote.Tables["UnitNote"]);
            foreach (DataRowView rowView in dvUnitNote)
            {
                RowAr[0] = iCntId.ToString();
                RowAr[1] = strDate;
                RowAr[2] = strTime;
                RowAr[3] = lblSmNb.Text;
                RowAr[4] = rowView["UnitId"].ToString();
                RowAr[5] = rowView["F_04"].ToString();
                RowAr[6] = rowView["F_05"].ToString();
                RowAr[7] = rowView["F_06"].ToString();
                for (int i = 8; i < 95; i = i + 1)
                {
                    RowAr[i] = rowView.Row[i].ToString();
                }

                string strSqlUnitNoteArch = "INSERT INTO UnitNoteArch (OrderId, OrderDate, OrderTime, OrderChange, UnitId, F_04, F_05, F_06, F_07, F_08, F_09, " +
                "F_10, F_11, F_12, F_13, F_14, F_15, F_16, F_17, F_18, F_19, F_20, F_21, F_22, F_23, F_24, F_25, F_26, F_27, F_28, F_29, " +
                "F_30, F_31, F_32, F_33, F_34, F_35, F_36, F_37, F_38, F_39, F_40, F_41, F_42, F_43, F_44, F_45, F_46, F_47, F_48, F_49, " +
                "F_50, F_51, F_52, F_53, F_54, F_55, F_56, F_57, F_58, F_59, F_60, F_61, F_62, F_63, F_64, F_65, F_66, F_67, F_68, F_69, " +
                "F_70, F_71, F_72, F_73, F_74, F_75, F_76, F_77, F_78, F_79, F_80, F_81, F_82, F_83, F_84, F_85, F_86, F_87, F_88, F_89, " +
                "F_90, F_91, F_92, F_93) " +
                "Values('" + RowAr[0] + "', '" + RowAr[1] + "', '" + RowAr[2] + "', '" + RowAr[3] + "', '" + RowAr[4] + "', " +
                "'" + RowAr[5] + "', '" + RowAr[6] + "', '" + RowAr[7] + "', " +
                "'" + RowAr[8] + "',  '" + RowAr[9] + "',  '" + RowAr[10] + "', '" + RowAr[11] + "', '" + RowAr[12] + "', '" + RowAr[13] + "', " +
                "'" + RowAr[14] + "', '" + RowAr[15] + "', '" + RowAr[16] + "', '" + RowAr[17] + "', '" + RowAr[18] + "', '" + RowAr[19] + "', " +
                "'" + RowAr[20] + "', '" + RowAr[21] + "', '" + RowAr[22] + "', '" + RowAr[23] + "', '" + RowAr[24] + "', '" + RowAr[25] + "', " +
                "'" + RowAr[26] + "', '" + RowAr[27] + "', '" + RowAr[28] + "', '" + RowAr[29] + "', '" + RowAr[30] + "', '" + RowAr[31] + "', " +
                "'" + RowAr[32] + "', '" + RowAr[33] + "', '" + RowAr[34] + "', '" + RowAr[35] + "', '" + RowAr[36] + "', '" + RowAr[37] + "', " +
                "'" + RowAr[38] + "', '" + RowAr[39] + "', '" + RowAr[40] + "', '" + RowAr[41] + "', '" + RowAr[42] + "', '" + RowAr[43] + "', " +
                "'" + RowAr[44] + "', '" + RowAr[45] + "', '" + RowAr[46] + "', '" + RowAr[47] + "', '" + RowAr[48] + "', '" + RowAr[49] + "', " +
                "'" + RowAr[50] + "', '" + RowAr[51] + "', '" + RowAr[52] + "', '" + RowAr[53] + "', '" + RowAr[54] + "', '" + RowAr[55] + "', " +
                "'" + RowAr[56] + "', '" + RowAr[57] + "', '" + RowAr[58] + "', '" + RowAr[59] + "', '" + RowAr[60] + "', '" + RowAr[61] + "', " +
                "'" + RowAr[62] + "', '" + RowAr[63] + "', '" + RowAr[64] + "', '" + RowAr[65] + "', '" + RowAr[66] + "', '" + RowAr[67] + "', " +
                "'" + RowAr[68] + "', '" + RowAr[69] + "', '" + RowAr[70] + "', '" + RowAr[71] + "', '" + RowAr[72] + "', '" + RowAr[73] + "', " +
                "'" + RowAr[74] + "', '" + RowAr[75] + "', '" + RowAr[76] + "', '" + RowAr[77] + "', '" + RowAr[78] + "', '" + RowAr[79] + "', " +
                "'" + RowAr[80] + "', '" + RowAr[81] + "', '" + RowAr[82] + "', '" + RowAr[83] + "', '" + RowAr[84] + "', '" + RowAr[85] + "', " +
                "'" + RowAr[86] + "', '" + RowAr[87] + "', '" + RowAr[88] + "', '" + RowAr[89] + "', '" + RowAr[90] + "', '" + RowAr[91] + "', " +
                "'" + RowAr[92] + "', '" + RowAr[93] + "', '" + RowAr[94] + "')";

                SqlCommand command = new SqlCommand(strSqlUnitNoteArch, FrmMain.Instance.m_cnn);
                Int32 recordsAffected = command.ExecuteNonQuery();

                iCntId++;   // Id поля OrderId
            }
        }

        private void btnSvodStaffUnit_Click(object sender, EventArgs e)
        {
            string strSmena = lblSmNb.Text;
            string strUnitName = c1CmbUnit.Text;
            string strCap = "Свод личного состава для " + strUnitName;
            if (MessageBox.Show("Записать информацию?", strCap,
                    MessageBoxButtons.YesNo, MessageBoxIcon.Question)
                    == DialogResult.Yes)
            {
                DataRow drUnitNote;
                drUnitNote = dsUnitNote.Tables["UnitNote"].Rows[bsUnitNote.Position];
                string strPchId = drUnitNote["UnitId"].ToString();
                string strOrderId = drUnitNote["OrderId"].ToString();

                UpdateStaffRecUnitNote(strSmena, strPchId, strOrderId);

                UpdateDataSetUnitNote();    //обновляет набор записей в форме
                int itemFound = bsUnitNote.Find("UnitId", strPchId);
                bsUnitNote.Position = itemFound;
            }
        }

        private void c1CmdTechSmena_Click(object sender, C1.Win.C1Command.ClickEventArgs e)
        {
            string strSmena = lblSmNb.Text;
            string strCap = "Свод техники для смены №" + strSmena;
            if (MessageBox.Show("Записать информацию?", strCap,
                    MessageBoxButtons.YesNo, MessageBoxIcon.Question)
                    == DialogResult.Yes)
            {
                string strPchId = "";
                string strOrderId = "";
                DataView dvUnitNote = new DataView(dsUnitNote.Tables["UnitNote"]);
                foreach (DataRowView rowView in dvUnitNote)
                {
                    strPchId = rowView["UnitId"].ToString();
                    strOrderId = rowView["OrderId"].ToString();

                    UpdateTechnicsRecUnitNote(strPchId, strOrderId);
                }
            }
        }

        private void c1CmdStaffSmena_Click(object sender, C1.Win.C1Command.ClickEventArgs e)
        {
            string strSmena = lblSmNb.Text;
            string strCap = "Свод состава для смены №" + strSmena;
            if (MessageBox.Show("Записать информацию?", strCap,
                    MessageBoxButtons.YesNo, MessageBoxIcon.Question)
                    == DialogResult.Yes)
            {
                string strPchId = "";
                string strOrderId = "";
                DataView dvUnitNote = new DataView(dsUnitNote.Tables["UnitNote"]);
                foreach (DataRowView rowView in dvUnitNote)
                {
                    strPchId = rowView["UnitId"].ToString();
                    strOrderId = rowView["OrderId"].ToString();

                    UpdateStaffRecUnitNote(strSmena, strPchId, strOrderId);
                }
            }
        }

        private void c1CmdSvGorog_Click(object sender, C1.Win.C1Command.ClickEventArgs e)
        {
            string strSmena = lblSmNb.Text;
            string strCap = "Свод город для смены №" + strSmena;
            if (MessageBox.Show("Записать информацию?", strCap,
                    MessageBoxButtons.YesNo, MessageBoxIcon.Question)
                    == DialogResult.Yes)
            {
                UpdateSvodGorodObl(1, "500");
            }
        }

        private void c1CmdSvObl_Click(object sender, C1.Win.C1Command.ClickEventArgs e)
        {
            string strSmena = lblSmNb.Text;
            string strCap = "Свод область для смены №" + strSmena;
            if (MessageBox.Show("Записать информацию?", strCap,
                    MessageBoxButtons.YesNo, MessageBoxIcon.Question)
                    == DialogResult.Yes)
            {
                UpdateSvodGorodObl(2, "501");
            }
        }

        private void FrmUnitNote_FormClosed(object sender, FormClosedEventArgs e)
        {
            FrmMain.Instance.m_blFrmUnitNote = false;
        }
        
    }
}
 