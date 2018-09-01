using System;
using System.ComponentModel;
using System.IO;
using System.Net;
using System.Windows.Forms;

namespace SAI_Autosaver
{
    public partial class UpdaterForm : Form
    {
        public event EventHandler<string> DownloadFinished;
        public string SavePath { get; set; }

        private string text;

        public UpdaterForm(string url)
        {
            InitializeComponent();
            text = status.Text;

            SavePath = Path.GetTempPath() + Guid.NewGuid().ToString() + ".msi";

            var client = new WebClient();
            client.DownloadProgressChanged += Client_DownloadProgressChanged;
            client.DownloadFileCompleted += Client_DownloadFileCompleted;

            client.DownloadFileAsync(new Uri(url), SavePath);
        }

        private void Client_DownloadProgressChanged(object sender, DownloadProgressChangedEventArgs e)
        {
            double percentage = e.BytesReceived / (double)e.TotalBytesToReceive * 100;
            Threading.SetComponentText(this, status, String.Format(Properties.Strings.UpdateDownloading, String.Format("{0}/{1} ({2:f}%)", e.BytesReceived, e.TotalBytesToReceive, percentage)));
            Threading.ComponentInvoke(progressBar1, (x) => x.Value = (int)Math.Truncate(percentage));
        }

        private void Client_DownloadFileCompleted(object sender, AsyncCompletedEventArgs e)
        {
            Threading.SetComponentText(this, status, Properties.Strings.UpdateDone);
            DownloadFinished?.Invoke(this, SavePath);
        }

        private void UpdaterForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = true;
        }
    }
}
