using Microsoft.WindowsAPICodePack.Taskbar;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.DirectoryServices.AccountManagement;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading;
using System.Windows.Forms;

namespace SAI_Autosaver
{
    static class SaiHelper
    {
        public static string AppName { get; set; }
        public static Process AppProcess { get; private set; }

        public static bool ObtainProcess()
        {
            var process = Process.GetProcessesByName(AppName).FirstOrDefault();
            AppProcess = process;
            return process != null;
        }

        public static string GetProjectAbsolutePath()
        {
            var path = AppProcess.MainWindowTitle.Split(new char[] { '-' }, 2)[1].Trim();
            if (IsProjectModified())
            {
                path = path.Replace("(*)", "").Trim();
            }

            if (path.Contains('/'))
            {
                var pathSplit = path.Split('/').Select(x => x.Trim()).ToArray();

                if (pathSplit.Length == 2 && pathSplit[0].ToLower() == "desktop")
                {
                    path = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), pathSplit[1]);
                }
                else
                {
                    var principalSearcher = new PrincipalSearcher(new UserPrincipal(new PrincipalContext(ContextType.Machine)));
                    var found = false;

                    foreach (var principal in principalSearcher.FindAll())
                    {
                        if (pathSplit[0] == principal.DisplayName || pathSplit[0] == principal.Name)
                        {
                            var userProfile = Path.Combine(Directory.GetParent(Environment.GetFolderPath(Environment.SpecialFolder.UserProfile)).ToString(), principal.Name);
                            path = Path.Combine(new string[] { userProfile }.Concat(pathSplit.Skip(1)).ToArray());

                            found = true;
                            break;
                        }
                    }

                    if (!found)
                    {
                        path = Path.Combine(new string[] { Environment.GetFolderPath(Environment.SpecialFolder.Desktop) }.Concat(pathSplit).ToArray());
                    }
                }
            }

            return path.Contains('\\') ? File.Exists(path) ? path : null : null;
        }

        public static string GetProjectFileName()
            => GetProjectNameRaw().Replace("(*)", "");

        public static string GetProjectName()
            => Path.GetFileNameWithoutExtension(GetProjectFileName());

        public static bool IsProjectModified()
            => GetProjectNameRaw().EndsWith("(*)");

        public static bool IsProjectHasPath()
            => GetProjectAbsolutePath() != null;

        public static bool IsProjectOpened()
            => AppProcess.MainWindowTitle.Contains('-');

        private static string GetProjectNameRaw()
        {
            var path = AppProcess.MainWindowTitle.Split(new char[] { '-' }, 2)[1].Trim();
            return path.Contains('/') ? path.Split('/').Last().Trim() : Path.GetFileName(path);
        }

        public static void CopyProjectFileInto(string fileName)
        {
            if (!File.Exists(fileName))
            {
                Directory.CreateDirectory(fileName);
            }

            File.Copy(GetProjectAbsolutePath(), Path.Combine(fileName, $"{GetProjectName()} - {DateTime.Now.ToString("yyyy.MM.dd - HH.mm.ss")}{Path.GetExtension(GetProjectFileName())}"), true);
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
