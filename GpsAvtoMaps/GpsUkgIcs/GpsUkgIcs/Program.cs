using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace GpsUkgIcs
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            //Application.Run(new FrmMain());
            FrmMain.Instance = new FrmMain();
            Application.Run(FrmMain.Instance);
        }
    }
}
