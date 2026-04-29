using Microsoft.Web.WebView2.WinForms;
using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace Dota_2_Training_Platform
{
    public class VideoPlayerForm : Form
    {
        private readonly string _videoPath;
        private WebView2 _webView;

        public VideoPlayerForm(string videoPath)
        {
            _videoPath = videoPath;
            InitializeUi();
        }

        private void InitializeUi()
        {
            Text = "Просмотр записи матча";
            StartPosition = FormStartPosition.CenterParent;
            Width = 1200;
            Height = 760;
            BackColor = Color.Black;

            _webView = new WebView2
            {
                Dock = DockStyle.Fill
            };

            Controls.Add(_webView);
            Load += VideoPlayerForm_Load;
        }

        private async void VideoPlayerForm_Load(object sender, EventArgs e)
        {
            if (!File.Exists(_videoPath))
            {
                MessageBox.Show("Файл видео не найден.", "Просмотр", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                Close();
                return;
            }

            try
            {
                await _webView.EnsureCoreWebView2Async();

                string fileUrl = new Uri(_videoPath).AbsoluteUri;
                string html = $@"
<!doctype html>
<html>
<head>
  <meta charset='utf-8'/>
  <style>
    html, body {{ margin:0; background:#111; height:100%; }}
    .wrap {{ display:flex; align-items:center; justify-content:center; width:100%; height:100%; }}
    video {{ width:100%; height:100%; background:#000; }}
  </style>
</head>
<body>
  <div class='wrap'>
    <video controls autoplay src='{fileUrl}'></video>
  </div>
</body>
</html>";

                // Для некоторых версий WebView2 проигрывание file:// из NavigateToString
                // работает нестабильно, поэтому открываем временную html-страницу с диска.
                string tempHtmlPath = Path.Combine(
                    Path.GetTempPath(),
                    "dota2tp_video_" + Guid.NewGuid().ToString("N") + ".html");
                File.WriteAllText(tempHtmlPath, html);
                _webView.Source = new Uri(tempHtmlPath);
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Не удалось открыть видео во встроенном плеере.\n\n" + ex.Message,
                    "Просмотр",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
            }
        }
    }
}
