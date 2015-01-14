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
    public partial class FrmUnitNoteArch : Form
    {
        static public FrmUnitNoteArch Instance;

        DataSet dsUnitNote = new DataSet();
        BindingSource bsUnitNote = new BindingSource();
        DataSet dsPchAll = new DataSet();
        BindingSource bsPchAll = new BindingSource();

        DataRow drNote;

        string l_strPchId = ""; // Id п/части

        public FrmUnitNoteArch()
        {
            InitializeComponent();
        }

        private void FrmUnitNoteArch_Load(object sender, EventArgs e)
        {
            string strSqlTextUnitNoteArch = "SELECT OrderDate FROM UnitNoteArch ORDER BY OrderId";
            SqlCommand cmd = new SqlCommand(strSqlTextUnitNoteArch, FrmMain.Instance.m_cnn);
            SqlDataReader rdrNoteArch = cmd.ExecuteReader();
            string strLastDate = "";
            while (rdrNoteArch.Read())
            {
                if (!rdrNoteArch.IsDBNull(0))
                    strLastDate = rdrNoteArch.GetString(0);
            }
            rdrNoteArch.Close();

            bool blFndRec = DataUnitNote_BS(strLastDate);

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

            if (blFndRec)
            {
                DataUnitNote_Load();
                this.bsUnitNote.PositionChanged += new EventHandler(bsUnitNote_PositionChanged);
            }
        }

        private bool DataUnitNote_BS(string strDate)
        {
            DataSet dsUnitNoteLoc = new DataSet();
            BindingSource bsUnitNoteLoc = new BindingSource();

            string strSqlTextUnitNote = "SELECT UnitNoteArch.* FROM UnitNoteArch " +
                "WHERE (OrderDate = '" + strDate + "') ORDER BY UnitId";
            SqlDataAdapter daUnitNote = new SqlDataAdapter(strSqlTextUnitNote, FrmMain.Instance.m_cnn);
            daUnitNote.Fill(dsUnitNoteLoc, "UnitNote");
            bsUnitNoteLoc.DataSource = dsUnitNoteLoc;
            bsUnitNoteLoc.DataMember = "UnitNote";

            bool blFindRec = false;
            if (bsUnitNoteLoc.Count > 0)
            {
                blFindRec = true;

                //делаем копии иначе не связать таблицу с навигатором
                dsUnitNote = dsUnitNoteLoc.Copy();
                bsUnitNote = new BindingSource(dsUnitNoteLoc, "UnitNote");
                //....
                bndNvgNote.BindingSource = bsUnitNote;
            }
            return blFindRec;
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

            c1CmbNach.Text = drNote["F_04"].ToString();
            c1CmbDisp.Text = drNote["F_05"].ToString();
            c1CmbRaz.Text = drNote["F_06"].ToString();
            
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

        private void btnFind_Click(object sender, EventArgs e)
        {
            string strDateNew = c1DateNew.Text;

            bool blFndRec = DataUnitNote_BS(strDateNew);
            if (blFndRec)
            {
                DataUnitNote_Load();
                this.bsUnitNote.PositionChanged += new EventHandler(bsUnitNote_PositionChanged);
            }
            else
            {
                string strMsgDate = "Нет записей для даты - " + strDateNew;
                MessageBox.Show(strMsgDate, "Поиск по дате",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
    }
}
