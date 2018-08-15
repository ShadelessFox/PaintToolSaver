using System;
using System.Diagnostics;
using System.Windows.Forms;

namespace SAI_Autosaver
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            if(Process.GetProcessesByName(typeof(Program).Assembly.GetName().Name).Length > 1)
            {
                MessageBox.Show(Properties.Strings.AlreadyLaunched, Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new MainForm());
        }
    }
}
