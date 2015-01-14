using System;
using System.IO;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Runtime.InteropServices;

namespace DispNet
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [DllImport("USER32.DLL")]
        public static extern IntPtr FindWindow(string lpClassName, string lpWindowName);

        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            //Application.Run(new FrmMain());

            IntPtr FrmMainHandle = FindWindow(null, "АСУ-01");
            if (FrmMainHandle == IntPtr.Zero)
            {


                FrmMain.Instance = new FrmMain();
                if (File.Exists("DispNet.ini"))
                {

                    Application.Run(FrmMain.Instance);
                }
                else
                {
                    MessageBox.Show("Не могу найти файл DispNet.ini!", "Ошибка запуска системы",
                       MessageBoxButtons.OK, MessageBoxIcon.Stop);
                }
            }
            else
            {
                //FormCollection frmCln = Application.OpenForms;
            }

        }
    }
}