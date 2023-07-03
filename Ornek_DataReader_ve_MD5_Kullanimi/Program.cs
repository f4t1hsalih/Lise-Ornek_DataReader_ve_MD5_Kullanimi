using System;
using System.Windows.Forms;

namespace Ornek_DataReader_ve_MD5_Kullanimi
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
            Application.Run(new frmGiris());
        }
    }
}
