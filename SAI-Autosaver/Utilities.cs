using Microsoft.WindowsAPICodePack.Taskbar;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.DirectoryServices.AccountManagement;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text.RegularExpressions;
using System.Threading;
using System.Windows.Forms;

namespace SAI_Autosaver
{
    static class SaiHelper
    {
        public static string AppName { get; set; }
        public static Process AppProcess { get; private set; }

        private static readonly (string, string)[] CachedDriveInfos;
        private static readonly (string, string)[] CachedPrincipals;

        static SaiHelper()
        {
            CachedDriveInfos = DriveInfo.GetDrives()
                .Where(x => x.IsReady)
                .Select(x => (x.Name, x.VolumeLabel))
                .ToArray();

            CachedPrincipals = new PrincipalSearcher(new UserPrincipal(new PrincipalContext(ContextType.Machine))).FindAll()
                .Select(x => (x.Name, x.DisplayName))
                .ToArray();
        }

        public static bool Connect()
        {
            AppProcess = Process.GetProcessesByName(AppName).FirstOrDefault();
            return AppProcess != null;
        }

        public static bool IsProjectOpened()
        {
            return AppProcess.MainWindowTitle.Contains('-');
        }

        public static bool IsProjectSavedLocally()
        {
            return GetProjectPath() != null;
        }

        public static bool IsProjectModified()
        {
            return AppProcess.MainWindowTitle.EndsWith("(*)");
        }

        public static string GetProjectName()
        {
            return Path.GetFileNameWithoutExtension(GetProjectPath());
        }

        public static string GetProjectPath()
        {
            var path = Regex.Replace(AppProcess.MainWindowTitle.Split('-').Last(), @"\s*\([!|*|?]\)", "").Trim();
            var newPath = path.Contains('/');


            if (path.Contains('/'))
            {
                string[] pathSplit = path.Split('/').Select(x => x.Trim()).ToArray();

                string TryGetDesktop()
                {
                    if (pathSplit.Length == 2 && pathSplit[0].ToLower() == "desktop")
                    {
                        return Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), pathSplit[1]);
                    }

                    return Path.Combine(new string[] { Environment.GetFolderPath(Environment.SpecialFolder.Desktop) }.Concat(pathSplit).ToArray());
                }

                string TryGetProfile()
                {
                    foreach (var (Name, DisplayName) in CachedPrincipals)
                    {
                        if (pathSplit[0] == DisplayName || pathSplit[0] == Name)
                        {
                            var userProfile = Path.Combine(Directory.GetParent(Environment.GetFolderPath(Environment.SpecialFolder.UserProfile)).ToString(), Name);
                            return Path.Combine(new string[] { userProfile }.Concat(pathSplit.Skip(1)).ToArray());
                        }

                    }

                    return null;
                }

                string TryGetDrive()
                {
                    foreach (var (Name, VolumeLabel) in CachedDriveInfos)
                    {
                        try
                        {
                            if (pathSplit[0] == $"{VolumeLabel} ({Name.Substring(0, 2)})")
                            {
                                return Path.Combine(new string[] { Name }.Concat(pathSplit.Skip(1)).ToArray());
                            }
                        }
                        catch (IOException) { }
                    }

                    return null;
                }

                path = TryGetDrive() ?? TryGetProfile() ?? TryGetDesktop();
            }

            return path != null ? File.Exists(path) ? Path.GetFullPath(path) : null : null;
        }

        public static void CopyProjectFileInto(string fileName)
        {
            if (!File.Exists(fileName))
            {
                Directory.CreateDirectory(fileName);
            }

            File.Copy(GetProjectPath(), Path.Combine(fileName, $"{GetProjectName()} - {DateTime.Now.ToString("yyyy.MM.dd - HH.mm.ss")}{Path.GetExtension(GetProjectPath())}"), true);
        }

        public static void SaveProject()
        {
            var handle = AppProcess.MainWindowHandle;

            PostMessage(handle, 0x100, 0x11, 0x001D0001);
            PostMessage(handle, 0x100, 'S', 0x001F0001);
            Thread.Sleep(50);
            PostMessage(handle, 0x101, 'S', 0xC01F0001);
            PostMessage(handle, 0x101, 0x11, 0xC01D0001);
        }

        #region Unmanaged code

        [DllImport("user32.dll")]
        private static extern bool PostMessage(IntPtr hWnd, uint msg, uint wParam, uint lParam);

        #endregion
    }

    static class TaskbarHelper
    {
        private static TaskbarManager taskbar = TaskbarManager.Instance;

        public static void SetProgress(Process process, int current, int maximum)
        {
            if (process != null && !process.HasExited)
            {
                taskbar.SetProgressValue(current, maximum, process.MainWindowHandle);
            }
        }

        public static void SetState(Process process, TaskbarProgressBarState state)
        {
            if (process != null && !process.HasExited)
            {
                taskbar.SetProgressState(state, process.MainWindowHandle);
            }
        }
    }

    static class Threading
    {
        delegate void SetTextCallback(Form f, Control ctrl, string text);

        public static void SetComponentText(Form form, Control ctrl, string text)
        {
            if (ctrl.InvokeRequired)
            {
                var callback = new SetTextCallback(SetComponentText);
                form.Invoke(callback, form, ctrl, text);
            }
            else
            {
                ctrl.Text = text;
            }
        }

        public static void ComponentInvoke<T>(T ctrl, Action<T> callback) where T : Control
        {
            if (ctrl.InvokeRequired)
            {
                ctrl.Invoke(new Action(() => callback(ctrl)));
            }
            else
            {
                callback(ctrl);
            }
        }
    }

    static class Extensions
    {
        public static string Plural(this int num, bool prefix, params string[] words)
        {
            int n0 = Math.Abs(num) % 100;
            int n1 = n0 % 10;
            string word = (10 < n0 && n0 < 20) ? words[2] : (1 < n1 && n1 < 5) ? words[1] : (n1 == 1) ? words[0] : words[2];
            return prefix ? $"{num} {word}" : word;
        }

        public static string FormatHMSDate(this TimeSpan span, string[] hoursPlural, string[] minutesPlural, string[] secondsPlural)
        {
            var items = new List<string>();

            if (span.Hours > 0)
            {
                items.Add(span.Hours.Plural(true, hoursPlural));
            }

            if (span.Minutes > 0)
            {
                items.Add(span.Minutes.Plural(true, minutesPlural));
            }

            if (span.Seconds > 0)
            {
                items.Add(span.Seconds.Plural(true, secondsPlural));
            }

            return String.Join(" ", items);
        }
    }
}
