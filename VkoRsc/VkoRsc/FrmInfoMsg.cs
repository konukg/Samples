using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace VkoRsc
{
    public partial class FrmInfoMsg : Form
    {
        static public FrmInfoMsg Instance;

        public FrmInfoMsg()
        {
            InitializeComponent();
        }

        private void FrmInfoMsg_Load(object sender, EventArgs e)
        {
            c1SprLbInfoMsg.Text = "<b>Спасательная станция “Семей”</b><br>Руководитель Байтуганов К. К. тел 50-26-43<br>" +
                "Время приведения в готовность(час) - постоянно<br>фактически - 17 чел. по штату - 17<br>" +
                "Оснащенность техникой ед(%) - 40(4), имущество - 80%<br>" +
                "<parm><hr noshade size=1 style='margin:2' color=Darker></parm>" +
                "Клубный переулок 1, Ордабаев Г.Б. Тел 50-26-43";
        }
    }
}
