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
    public partial class FrmAviaPtr : Form
    {
        static public FrmAviaPtr Instance;

        public FrmAviaPtr()
        {
            InitializeComponent();
        }

        private void FrmAviaPtr_Load(object sender, EventArgs e)
        {
            c1SprLblAviaPtr.Text = "<b>с. Катон-Карагай Аэропорт тел. (8-723-43)2-13-56</b><br>Нач. авиагруппы Воробьев Артем Владимирович<br>" +
                "Количество АПС - 7<parm><hr noshade size=1 style='margin:2' color=Darker></parm>" +
                "Маршрут патрулирования<br><span style='color:blue'>Катон-Карагай-Чингистай-Бобровка-Матобай-Маралиха-Солдатово-<br>Катон-Карагай" +
                "</span>" +
                "<parm><hr noshade size=1 style='margin:2' color=Darker></parm>" +
                "Ми-2 постоянная базировка при фактической горимости и высоким КПО";
            
            object[] items1 = { "Летчик-наблюдатель", "Воробъев Артем Владимирович", "", "Тальник-6" };
            c1FlxGrdAviaPzv.AddItem(items1, 1, 0);
            object[] items2 = { "Инструктор АПК", "Воробъев Владимир Михайлович", "", "Катон-1" };
            c1FlxGrdAviaPzv.AddItem(items2, 2, 0);
            object[] items3 = { "Радиооператор", "Шалимов Виктор Иосифович", "Лесхоз-37", "Катон" };
            c1FlxGrdAviaPzv.AddItem(items3, 3, 0);
            object[] items4 = { "УАЗ-31519", "", "", "Катон-2" };
            c1FlxGrdAviaPzv.AddItem(items4, 4, 0);
            object[] items5 = { "Передвижная КВ", "", "Лесхоз-41", "" };
            c1FlxGrdAviaPzv.AddItem(items5, 5, 0);
        }
    }
}
