using Newtonsoft.Json.Linq;
using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace Updater
{
    class Program
    {
        public static string UpdateUrl { get; set; } = "https://api.github.com/repos/ShadelessFox/PaintToolSaver/releases/latest";

        static void Main(string[] args)
        {
            JObject json = null;

            try
            {
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(UpdateUrl);
                request.Method = "GET";
                request.Accept = "application/vnd.github.v3+json";
                request.UserAgent = "PaintToolSaver (+ vk.com/rukifox)";

                json = JObject.Parse(new StreamReader(request.GetResponse().GetResponseStream()).ReadToEnd());
            }
            catch (WebException e)
            {
                Debug.WriteLine("[WEB] Failed to get latest version: {}", e);
                return;
            }

            Version currentVersion = AssemblyName.GetAssemblyName("SAI-Autosaver.exe").Version;
            Version lastVersion = Version.Parse(Regex.Replace(json["tag_name"].ToString(), @"[^.0-9]", ""));

            if (true)
            {
                Debug.WriteLine("New update available");

                StringBuilder sb = new StringBuilder();
                sb.Append(String.Format(Properties.Strings.CurrentVersion, currentVersion.ToString()));
                sb.Append('\n');
                sb.Append(String.Format(Properties.Strings.NewVersion, lastVersion.ToString()));
                sb.Append('\n', 2);
                sb.Append(Properties.Strings.Changelog);
                sb.Append('\n');
                sb.Append(String.Join("\n", json["body"].ToString().Split('\n').Select(x => $"  {x}")));
                sb.Append('\n', 2);
                sb.Append(Properties.Strings.Upgrade);

                if (MessageBox.Show(sb.ToString(), Properties.Strings.UpdateAvailable, MessageBoxButtons.YesNoCancel, MessageBoxIcon.Information) == DialogResult.Yes)
                {
                    string downloadUrl = null;
                    foreach (JObject asset in json["assets"])
                    {
                        if (asset["name"].ToString().EndsWith(".msi"))
                        {
                            downloadUrl = asset["browser_download_url"].ToString();
                            break;
                        }
                    }

                    if (downloadUrl == null)
                    {
                        Debug.WriteLine("Failed to get update file");
                    }
                    else
                    {
                        var savePath = Path.GetTempPath() + Guid.NewGuid().ToString() + ".msi";
                        var client = new WebClient();
                        client.DownloadFile(downloadUrl, savePath);

                        Process.Start(savePath);
                        Process.GetCurrentProcess().Parent().Kill();
                    }
                }
            }
            else
            {
                Debug.WriteLine("No updates available");
            }
        }
    }

    public static class ProcessExtensions
    {
        private static string FindIndexedProcessName(int pid)
        {
            var processName = Process.GetProcessById(pid).ProcessName;
            var processesByName = Process.GetProcessesByName(processName);
            string processIndexdName = null;

            for (var index = 0; index < processesByName.Length; index++)
            {
                processIndexdName = index == 0 ? processName : processName + "#" + index;
                var processId = new PerformanceCounter("Process", "ID Process", processIndexdName);
                if ((int)processId.NextValue() == pid)
                {
                    return processIndexdName;
                }
            }

            return processIndexdName;
        }

        private static Process FindPidFromIndexedProcessName(string indexedProcessName)
        {
            var parentId = new PerformanceCounter("Process", "Creating Process ID", indexedProcessName);
            return Process.GetProcessById((int)parentId.NextValue());
        }

        public static Process Parent(this Process process)
        {
            return FindPidFromIndexedProcessName(FindIndexedProcessName(process.Id));
        }
    }
}
