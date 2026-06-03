using Microsoft.Web.WebView2.Core;
using System;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace Dota_2_Training_Platform
{
    public partial class TwitchFullscreenForm : Form
    {
        private const string DotaTwitchCategoryUrl = "https://www.twitch.tv/directory/category/dota-2";
        private bool isForcedRedirect;

        public TwitchFullscreenForm(string streamUrl)
        {
            InitializeComponent();
            KeyPreview = true;
            KeyDown += TwitchFullscreenForm_KeyDown;
            twitchFullscreenWebView.CoreWebView2InitializationCompleted += WebView_CoreWebView2InitializationCompleted;
            _ = twitchFullscreenWebView.EnsureCoreWebView2Async();

            if (!string.IsNullOrWhiteSpace(streamUrl))
                twitchFullscreenWebView.Source = new Uri(streamUrl);
        }

        private void TwitchFullscreenForm_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
                Close();
        }

        private void BtnCloseFullscreen_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void WebView_CoreWebView2InitializationCompleted(object sender, CoreWebView2InitializationCompletedEventArgs e)
        {
            if (!e.IsSuccess || twitchFullscreenWebView.CoreWebView2 == null)
                return;

            twitchFullscreenWebView.CoreWebView2.NavigationStarting += CoreWebView2_NavigationStarting;
        }

        private void CoreWebView2_NavigationStarting(object sender, CoreWebView2NavigationStartingEventArgs e)
        {
            if (isForcedRedirect)
            {
                isForcedRedirect = false;
                return;
            }

            if (!Uri.TryCreate(e.Uri, UriKind.Absolute, out var targetUri) || !IsAllowedTwitchUri(targetUri))
            {
                e.Cancel = true;
                isForcedRedirect = true;
                twitchFullscreenWebView.Source = new Uri(DotaTwitchCategoryUrl);
            }
        }

        private static bool IsAllowedTwitchUri(Uri uri)
        {
            if (uri == null)
                return false;

            var host = uri.Host.ToLowerInvariant();
            if (host != "www.twitch.tv" && host != "twitch.tv")
                return false;

            var path = uri.AbsolutePath.TrimEnd('/');
            if (path.Equals("/directory/category/dota-2", StringComparison.OrdinalIgnoreCase))
                return true;

            return Regex.IsMatch(path, "^/[A-Za-z0-9_]+$");
        }
    }
}
