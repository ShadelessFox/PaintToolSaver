using Microsoft.Win32;
using Microsoft.WindowsAPICodePack.Taskbar;
using SAI_Autosaver.Core;
using System;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Threading;
using System.Windows.Forms;

namespace SAI_Autosaver
{
    public partial class MainForm : Form
    {
        private DelayTimer timer;

        public MainForm()
        {
            InitializeComponent();
            Text = $"{Application.ProductName} v{Application.ProductVersion} by {Application.CompanyName}";

            notifyIcon1.Text = Application.ProductName;
            notifyIcon1.BalloonTipTitle = Application.ProductName;

            timer = new DelayTimer(1000);
            timer.OnNewState += Timer_OnNewState;
            timer.OnProgress += Timer_OnProgress;
            timer.OnNotSavedProjectNotify += Timer_OnNotSavedProjectNotify;

            LoadSettings();

            Updater.CheckForUpdate();
        }

        private void Timer_OnNotSavedProjectNotify(object sender, EventArgs e)
        {
            notifyIcon1.ShowBalloonTip(10000);
        }

        private void Timer_OnProgress(object sender, int e)
        {
            Threading.ComponentInvoke(progressBar1, (x) => x.Value = e);
            TaskbarHelper.SetProgress(SaiHelper.AppProcess, e, progressBar1.Maximum);
        }

        private void Timer_OnNewState(object sender, TimerState e)
        {
            Threading.ComponentInvoke(buttonManualBackup, (x) => x.Enabled = e == TimerState.SavingEnabled || e == TimerState.WaitingForCanvas);

            if (e == TimerState.NeverSaved)
            {
                TaskbarHelper.SetState(SaiHelper.AppProcess, TaskbarProgressBarState.Paused);
            }
            else if (e == TimerState.SavingEnabled)
            {
                TaskbarHelper.SetState(SaiHelper.AppProcess, TaskbarProgressBarState.Normal);
            }

            switch (e)
            {
                case TimerState.NeverSaved:
                    Threading.SetComponentText(this, labelStatus, Properties.Strings.StateNeverSaved);
                    break;
                case TimerState.SavingDisabled:
                    Threading.SetComponentText(this, labelStatus, Properties.Strings.StateSavingDisabled);
                    break;
                case TimerState.SavingEnabled:
                    Threading.SetComponentText(this, labelStatus, Properties.Strings.StateSavingEnabled);
                    break;
                case TimerState.WaitingForCanvas:
                    Threading.SetComponentText(this, labelStatus, Properties.Strings.StateWaitingForCanvas);
                    break;
                case TimerState.WaitingForProcess:
                    Threading.SetComponentText(this, labelStatus, Properties.Strings.StateWaitingForProcess);
                    break;
                case TimerState.WaitingForProject:
                    Threading.SetComponentText(this, labelStatus, Properties.Strings.StateWaitingForProject);
                    break;
            }

            BeginInvoke(new Action(() => sToolStripMenuItem.Text = labelStatus.Text));
        }

        private void LoadSettings()
        {
            foreach (var item in Properties.Resources.ItemsBackupDelays)
            {
                var formatted = TimeSpan.FromSeconds(item)
                    .FormatHMSDate(Properties.Strings.TimeHour,
                                   Properties.Strings.TimeMinute,
                                   Properties.Strings.TimeSecond);

                comboBackupDelay.Items.Add(formatted);
            }

            foreach (var kv in Properties.Resources.ItemsSaiVersions)
            {
                comboSaiVersion.Items.Add(kv.Value);
            }

            checkRunWithWindows.Checked = Properties.Settings.Default.RunWithWindows;
            checkRunInTray.Checked = Properties.Settings.Default.RunInTray;
            checkEnableBackups.Checked = Properties.Settings.Default.BackupEnabled;
            checkNotifyProjectNotSaved.Checked = Properties.Settings.Default.BackupNotifyNotSaved;
            checkBackupIntoFolder.Checked = Properties.Settings.Default.BackupIntoFolder;
            textBackupPath.Text = Properties.Settings.Default.BackupFolderPath;
            comboBackupDelay.SelectedIndex = Properties.Settings.Default.IndexBackupDelay;
            comboSaiVersion.SelectedIndex = Properties.Settings.Default.IndexSaiVersion;
        }

        private void ToggleMinimizeState()
        {
            bool isMinimized = WindowState == FormWindowState.Minimized;

            WindowState = isMinimized ? FormWindowState.Normal : FormWindowState.Minimized;
        }

        private void SetMinimizeState()
        {
            bool isMinimized = WindowState == FormWindowState.Minimized;

            ShowInTaskbar = !isMinimized;
            notifyIcon1.Visible = isMinimized;
        }

        private void SetAutostartMode(bool enable)
        {
            RegistryKey registry = Registry.CurrentUser.OpenSubKey(@"SOFTWARE\Microsoft\Windows\CurrentVersion\Run", true);

            if (enable)
            {
                registry.SetValue(Application.ProductName, Application.ExecutablePath);
            }
            else
            {
                registry.DeleteValue(Application.ProductName, false);
            }
        }

        private bool SelectBackupFolder()
        {
            if (folderBrowserDialog1.ShowDialog() == DialogResult.OK)
            {
                textBackupPath.Text = folderBrowserDialog1.SelectedPath;

                Properties.Settings.Default.BackupFolderPath = folderBrowserDialog1.SelectedPath;

                return true;
            }

            return false;
        }

        private void checkEnableBackups_CheckedChanged(object sender, EventArgs e)
        {
            bool isChecked = (sender as CheckBox).Checked;

            groupBackupSettings.Enabled = isChecked;
            groupGeneralSettings.Enabled = isChecked;

            Properties.Settings.Default.BackupEnabled = isChecked;
        }

        private void checkBackupIntoFolder_CheckedChanged(object sender, EventArgs e)
        {
            bool isChecked = (sender as CheckBox).Checked;

            if (isChecked && Properties.Settings.Default.BackupFolderPath.Trim().Count() == 0)
            {
                if (folderBrowserDialog1.ShowDialog() == DialogResult.OK)
                {
                    textBackupPath.Text = folderBrowserDialog1.SelectedPath;

                    Properties.Settings.Default.BackupFolderPath = folderBrowserDialog1.SelectedPath;
                }
                else
                {
                    (sender as CheckBox).Checked = false;
                    return;
                }
            }

            buttonSelectFolder.Enabled = isChecked;
            buttonOpenBackupFolder.Enabled = isChecked;
            textBackupPath.Enabled = isChecked;

            Properties.Settings.Default.BackupIntoFolder = isChecked;
        }

        private void buttonSelectFolder_Click(object sender, EventArgs e)
        {
            SelectBackupFolder();
        }

        private void buttonOpenBackupFolder_Click(object sender, EventArgs e)
        {
            try
            {
                Process.Start(Properties.Settings.Default.BackupFolderPath);
            }
            catch (Exception)
            {
                MessageBox.Show(Properties.Strings.IncorrectBackupPath);

                if (!SelectBackupFolder())
                {
                    checkBackupIntoFolder.Checked = false;
                }
            }
        }

        private void checkRunWithWindows_CheckedChanged(object sender, EventArgs e)
        {
            bool isChecked = (sender as CheckBox).Checked;

            Properties.Settings.Default.RunWithWindows = isChecked;
            SetAutostartMode(isChecked);
        }

        private void checkRunInTray_CheckedChanged(object sender, EventArgs e)
        {
            Properties.Settings.Default.RunInTray = (sender as CheckBox).Checked;
        }

        private void checkNotifyProjectNotSaved_CheckedChanged(object sender, EventArgs e)
        {
            Properties.Settings.Default.BackupNotifyNotSaved = (sender as CheckBox).Checked;
        }

        private void buttonSaveSettings_Click(object sender, EventArgs e)
        {
            Properties.Settings.Default.Save();
        }

        private void comboBackupDelay_SelectedIndexChanged(object sender, EventArgs e)
        {
            int selectedIndex = (sender as ComboBox).SelectedIndex;

            progressBar1.Maximum = Properties.Resources.ItemsBackupDelays[selectedIndex];
            timer.MaximumTime = Properties.Resources.ItemsBackupDelays[selectedIndex];

            Properties.Settings.Default.IndexBackupDelay = selectedIndex;
        }

        private void comboSaiVersion_SelectedIndexChanged(object sender, EventArgs e)
        {
            int selectedIndex = (sender as ComboBox).SelectedIndex;

            SaiHelper.AppName = Properties.Resources.ItemsSaiVersions.Keys.ToArray()[selectedIndex];

            Properties.Settings.Default.IndexSaiVersion = selectedIndex;
        }

        private void buttonManualBackup_Click(object sender, EventArgs e)
        {
            if (timer.LastState == TimerState.SavingEnabled)
            {
                SaiHelper.SaveProject();
                timer.CurrentTime = 0;
            }

            SaiHelper.CopyProjectFileInto(Properties.Settings.Default.BackupFolderPath);
        }

        private void notifyIcon1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            ToggleMinimizeState();
        }

        private void buttonHideWindow_Click(object sender, EventArgs e)
        {
            WindowState = FormWindowState.Minimized;
        }

        private void MainForm_Resize(object sender, EventArgs e)
        {
            SetMinimizeState();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            if (Properties.Settings.Default.RunInTray)
            {
                SetMinimizeState();
                WindowState = FormWindowState.Minimized;
            }
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            Properties.Settings.Default.Save();
            timer.CurrentTime = 0;
        }

        private void exitProgramToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Properties.Settings.Default.Save();
            Close();
        }

        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {
            WindowState = FormWindowState.Normal;
        }
    }
}
