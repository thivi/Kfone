using IdentityModel.OidcClient.Browser;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Windows.ApplicationModel.Core;
using Windows.System;
using Windows.UI.Core;
using Windows.UI.ViewManagement;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml;

namespace Kfone.Services
{
    public class WebView : IBrowser
    {
        public static TaskCompletionSource<BrowserResult> inFlightRequest;
        public Task<BrowserResult> InvokeAsync(BrowserOptions options, CancellationToken cancellationToken = default)
        {
            inFlightRequest = new TaskCompletionSource<BrowserResult>();
            var currentAppView = ApplicationView.GetForCurrentView();

            RunOnNewView(async () => {
                var newAppView = CreateApplicationView();
                var webView = CreateWebView(Window.Current, options, inFlightRequest);
                webView.Navigate(new Uri(options.StartUrl));
                await ApplicationViewSwitcher.TryShowAsStandaloneAsync(newAppView.Id, ViewSizePreference.UseMinimum, currentAppView.Id, ViewSizePreference.UseMinimum);
                BrowserResult result = await inFlightRequest.Task;
                if(result.ResultType == BrowserResultType.Success)
                {
                    Window.Current.Close();
                }
            });

            return inFlightRequest.Task;
        }

        private async void RunOnNewView(DispatchedHandler function)
        {
            await CoreApplication.CreateNewView().Dispatcher.RunAsync(CoreDispatcherPriority.Normal, function);
        }

        private static ApplicationView CreateApplicationView()
        {
            var appView = ApplicationView.GetForCurrentView();
            appView.Title = "Signing you in";
            return appView;
        }

        private static Windows.UI.Xaml.Controls.WebView CreateWebView(Window window, BrowserOptions options, TaskCompletionSource<BrowserResult> tcs)
        {
            var webView = new Windows.UI.Xaml.Controls.WebView();

            // There is no closed event so the best we can do is detect visibility. This means we close when they minimize too.
            window.VisibilityChanged += (sender, e) =>
            {
                if (!window.Visible && !tcs.Task.IsCompleted)
                {
                    tcs.SetResult(new BrowserResult { ResultType = BrowserResultType.UserCancel });
                    window.Close();
                }
            };

            window.Content = webView;
            window.Activate();

            return webView;
        }

        public static void ProcessResponse(Uri uri)
        {
            inFlightRequest.SetResult(new BrowserResult
            {
                Response = uri.OriginalString,
                ResultType = BrowserResultType.Success
            });
        }
    }
}
