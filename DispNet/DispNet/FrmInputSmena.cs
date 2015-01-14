using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace DispNet
{
    public partial class FrmInputSmena : Form
    {
        static public FrmInputSmena Instance;
        public string m_strSmenaNmb = "";
        public int m_iSngFrm = 0; //если 1 то из FrmStaffNote, если 2 то из FrmUnitNote

        public FrmInputSmena()
        {
            InitializeComponent();
        }

        private void FrmInputSmena_Load(object sender, EventArgs e)
        {
            cmbBoxSmn.Text = m_strSmenaNmb;
        }

        private void btnRecSmn_Click(object sender, EventArgs e)
        {
            string strMsg = "";
            if (m_iSngFrm == 1)
                strMsg = "Установить подмену?";
            if (m_iSngFrm == 2)
                strMsg = "Сменить смену?";

            if (MessageBox.Show(strMsg, "Ввод номера смены",
                                MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2)
                                == DialogResult.Yes)
            {
                int iSmnBef = Convert.ToInt32(m_strSmenaNmb);
                int iSmnAft = Convert.ToInt32(cmbBoxSmn.Text);

                if (iSmnAft > 0 && iSmnAft < 5)
                {
                    if (iSmnAft == iSmnBef)
                        MessageBox.Show("Выбранный номер смены соответствует текущей смене!", "Ввод номера смены",
                                    MessageBoxButtons.OK, MessageBoxIcon.Stop);
                    else
                    {
                        if (m_iSngFrm == 1)
                            FrmStaffNote.Instance.m_strSmenaNmbRtn = cmbBoxSmn.Text;
                        if (m_iSngFrm == 2)
                            FrmUnitNote.Instance.m_strSmenaNmbRtn = cmbBoxSmn.Text;
                        Close();
                    }
                }
                else
                    MessageBox.Show("Номер смены не существует!", "Ввод номера смены",
                                MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }

        }
    }
}
