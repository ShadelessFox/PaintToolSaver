using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading;

namespace SAI_Autosaver.Core
{
    static class SaiUtil
    {
        public static string ExeName { get; set; }

        public static Process Process { get; private set; }

        public static string ProjectTitle
        {
            get
            {
                var title = Process.MainWindowTitle;
                return title.IndexOf('-') < 0 ? null : title.Substring(title.IndexOf('-') + 1).Trim();
            }
        }

        public static string ProjectPath
        {
            get
            {
                if (!ProjectHasPath)
                {
                    return null;
                }

                return ProjectModified ? ProjectTitle.Substring(0, ProjectTitle.Length - 3).Trim() : ProjectTitle;
            }
        }

        public static bool HasProject => ProjectTitle != null;

        public static bool ProjectHasPath => HasProject && (ProjectTitle.Contains("/") || ProjectTitle.Contains("\\"));

        public static bool ProjectModified => ProjectTitle.EndsWith("(*)");

        public static bool HasObtained
        {
            get
            {
                var proc = Process.GetProcessesByName(ExeName).FirstOrDefault();
                if (proc != null)
                {
                    Process = proc;
                    return true;
                }

                return false;
            }
        }

        public static void CopyInto(string path)
        {
            if (!File.Exists(path))
            {
                Directory.CreateDirectory(path);
            }

            File.Copy(ProjectPath, Path.Combine(path, $"{Path.GetFileNameWithoutExtension(ProjectPath)} {DateTime.Now.ToString("yyyy.MM.dd - HH.mm.ss")}{Path.GetExtension(ProjectPath)}"), true);
        }

        public static void Save()
        {
            var handle = Process.MainWindowHandle;

            PostMessage(handle, 0x100, 0x11, 0x001D0001);
            PostMessage(handle, 0x100, 0x53, 0x001F0001);
            Thread.Sleep(50);
            PostMessage(handle, 0x101, 0x53, 0xC01F0001);
            PostMessage(handle, 0x101, 0x11, 0xC01D0001);
        }

        [DllImport("user32.dll")]
        private static extern bool PostMessage(IntPtr hWnd, uint msg, uint wParam, uint lParam);
    }
}
