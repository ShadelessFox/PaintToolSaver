using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SAI_Autosaver
{
    static class Updater
    {
        public static string UpdaterUrl { get; set; } = $"https://api.github.com/repos/{Properties.Resources.UpdaterRepo}/releases/latest";
        public static string ReleasesUrl { get; set; } = $"https://github.com/{Properties.Resources.UpdaterRepo}/releases/latest";

        public static async void CheckForUpdate()
        {
            try
            {
                var response = await GetUpdates();

                var curVersion = Assembly.GetExecutingAssembly().GetName().Version;
                var newVersion = Version.Parse(Regex.Replace(response["tag_name"].ToString(), @"[^.0-9]", ""));

                if (newVersion > curVersion)
                {
                    var sb = new StringBuilder();
                    foreach (var line in response["body"].ToString().Split('\n'))
                    {
                        sb.Append($"\n  {line}");
                    }

                    if (MessageBox.Show(String.Format(Properties.Strings.UpdateInfo, curVersion, newVersion, sb), Properties.Strings.UpdateAvailable, MessageBoxButtons.YesNoCancel, MessageBoxIcon.Information) == DialogResult.Yes)
                    {
                        string assetUrl = null;
                        foreach (var asset in response["assets"])
                        {
                            if (Path.GetExtension(asset["name"].ToString()) == ".msi")
                            {
                                assetUrl = asset["browser_download_url"].ToString();
                                break;
                            }
                        }

                        if (assetUrl == null)
                        {
                            Process.Start(ReleasesUrl);
                        }
                        else
                        {
                            var form = new UpdaterForm(assetUrl);
                            form.DownloadFinished += (object sender, string path) =>
                            {
                                Process.Start(path);
                                Process.GetCurrentProcess().Kill();
                            };

                            form.ShowDialog();
                        }
                    }
                }

            }
            catch (WebException e)
            {
                Debug.WriteLine("Failed to parse github releases: %s", e);
            }
        }

        private static async Task<JObject> GetUpdates()
        {
            var req = (HttpWebRequest)WebRequest.Create(UpdaterUrl);
            req.Method = "GET";
            req.Accept = "application/vnd.github.v3+json";
            req.UserAgent = "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/68.0.3440.106 Safari/537.36";

            var response = await req.GetResponseAsync();
            return JObject.Parse(new StreamReader(response.GetResponseStream()).ReadToEnd());
        }
    }
}
