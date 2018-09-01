using System;
using System.Configuration;
using System.Diagnostics;
using System.IO;
using System.Windows.Forms;
using System.Xml;

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
            if (Process.GetProcessesByName(typeof(Program).Assembly.GetName().Name).Length > 1)
            {
                MessageBox.Show(Properties.Strings.AlreadyLaunched, Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.PerUserRoamingAndLocal);
            }
            catch (ConfigurationErrorsException e)
            {
                if (MessageBox.Show(Properties.Strings.IncorrectConfigFile, Application.ProductName, MessageBoxButtons.YesNo, MessageBoxIcon.Error) == DialogResult.Yes)
                {
                    File.Delete(e.Filename);
                    Properties.Settings.Default.Upgrade();
                }
                else
                {
                    return;
                }
            }

            Updater.CheckForUpdate();

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new MainForm());
        }
    }
}
