using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using C1.Win.C1FlexGrid;

namespace DispNet
{
    public partial class FrmObjUkg : Form
    {
        static public FrmObjUkg Instance;
        public int m_iLoadFrm = 0;  // признак из какой формы получен вызов 1 - форма путевка, 2 - форма редактирования объекта
        public DataSet dsHomeSelObj = new DataSet();
        
        public FrmObjUkg()
        {
            InitializeComponent();
        }

        private void FrmObjUkg_Load(object sender, EventArgs e)
        {
            if (m_iLoadFrm == 2)
                FrmDispUkg.Instance.m_strBuildObjId = "";   //обнуляет Id дома
            c1FlxGrdObjS.SetDataBinding(dsHomeSelObj, "Buildings");
            c1FlxGrdObjS.Cols[1].Visible = false;
            c1FlxGrdObjS.Cols[2].Visible = false;
            c1FlxGrdObjS.Cols[3].Visible = false;
            c1FlxGrdObjS.Cols[4].Width = 250;
            c1FlxGrdObjS.Cols[5].Width = 110;

            c1FlxGrdObjS.Cols[4].Caption = "Объект";
            c1FlxGrdObjS.Cols[5].Caption = "Организация";
            
            CellStyle cs = c1FlxGrdObjS.Styles.Add("s1");
            cs.Font = new Font("Arial", 9, FontStyle.Bold);
            //cs.BackColor = Color.Beige;
            cs.TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.CenterCenter;
            c1FlxGrdObjS.Rows[0].Style = cs;
            
            c1FlxGrdObjS.Select(1,4);

            //c1FlexGrid1.Cols["Product"].ComboList = "Applets|Wahoos|Gadgets";

        }

        private void ObjChoice()
        {
            string strObj = c1FlxGrdObjS.GetDataDisplay(c1FlxGrdObjS.RowSel, 4) + " (" +
                c1FlxGrdObjS.GetDataDisplay(c1FlxGrdObjS.RowSel, 5) + ")";
            string strMsg = "Объект: " + strObj + "\r\n" + "Записать информацию об объекте?";
            string strObjId = c1FlxGrdObjS.GetDataDisplay(c1FlxGrdObjS.RowSel, 1);

            if (MessageBox.Show(strMsg, "Выбор объекта",
                    MessageBoxButtons.YesNo, MessageBoxIcon.Question)
                    == DialogResult.Yes)
            {
                //FrmObjUkg.Instance.Hide();
                
                if (m_iLoadFrm == 1)
                {
                    FrmPermit.Instance.textBoxObj.Text = strObj;
                    FrmPermit.Instance.m_TechnicsLoadObj(strObjId);
                    FrmPermit.Instance.m_strBuildIdMap = strObjId;    //BuildId объекта для вывода информации в главном окне
                    FrmPermit.Instance.m_blWrHome = true;
                    FrmPermit.Instance.m_blObjSign = true;
                }
                if (m_iLoadFrm == 2)
                    FrmDispUkg.Instance.m_strBuildObjId = strObjId;
                Close();    //закрывает окно
            }
            else
            {
                strMsg = "Объект: " + strObj + "\r\n" + "Записать информацию об объекте - как жилой дом?";
                if (MessageBox.Show(strMsg, "Выбор объекта",
                    MessageBoxButtons.YesNo, MessageBoxIcon.Question)
                    == DialogResult.Yes)
                {
                    if (m_iLoadFrm == 1)
                    {
                        FrmPermit.Instance.textBoxObj.Text = "Жилой дом";
                        FrmPermit.Instance.c1ComboRang.Text = "1";
                        FrmPermit.Instance.m_TechnicsLoad("1", FrmPermit.Instance.c1ComboHome.Text);
                        FrmPermit.Instance.m_strBuildIdMap = strObjId;    //BuildId объекта для вывода информации в главном окне
                        FrmPermit.Instance.m_blWrHome = true;
                        FrmPermit.Instance.m_blObjSign = false;
                    }
                    if (m_iLoadFrm == 2)
                        FrmDispUkg.Instance.m_strBuildObjId = strObjId;
                    
                    Close();    //закрывает окно
                }
                else
                {
                    ObjNoChoice();  //объект не выбран ни как объект ни как жилой дом
                }
            }
        }

        private void ObjNoChoice()
        {
            if (m_iLoadFrm == 1)
            {
                FrmPermit.Instance.m_strBuildIdMap = "";
                FrmPermit.Instance.m_blWrHome = false;
                FrmPermit.Instance.m_blObjSign = false;
                FrmPermit.Instance.c1ListBort.ClearItems();
            }
            if (m_iLoadFrm == 2)
                FrmDispUkg.Instance.m_strBuildObjId = "";

            Close();
        }

        private void btnRec_Click(object sender, EventArgs e)
        {
            ObjChoice();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            ObjNoChoice();
        }

        private void c1FlxGrdObjS_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Return)
            {
                e.Handled = true;
                ObjChoice();
            }
        }

        private void c1FlxGrdObjS_DoubleClick(object sender, EventArgs e)
        {
            ObjChoice();
        }
                
    }
}