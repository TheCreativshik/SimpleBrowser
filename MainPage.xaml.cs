using Windows.UI.StartScreen;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Input;
using System;
using Windows.UI.Popups;

namespace SimpleBrowser
{
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();
            Browser.Navigate(new Uri("https://google.com"));
        }

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            if (Browser.CanGoBack) Browser.GoBack();
        }

        private void ForwardButton_Click(object sender, RoutedEventArgs e)
        {
            if (Browser.CanGoForward) Browser.GoForward();
        }

        private void RefreshButton_Click(object sender, RoutedEventArgs e)
        {
            Browser.Refresh();
        }

        private void GoButton_Click(object sender, RoutedEventArgs e)
        {
            NavigateToAddress();
        }

        private void AddressBar_KeyUp(object sender, KeyRoutedEventArgs e)
        {
            if (e.Key == Windows.System.VirtualKey.Enter) NavigateToAddress();
        }

        private void Browser_NavigationCompleted(WebView sender, WebViewNavigationCompletedEventArgs args)
        {
            if (args.IsSuccess) AddressBar.Text = args.Uri.ToString();
        }

        private async void PinButton_Click(object sender, RoutedEventArgs e)
        {
            if (Browser.Source == null) return;
            string url = Browser.Source.ToString();
            string title = new Uri(url).Host.Replace("www.", "");
            var tile = new SecondaryTile(
                $"PinnedSite_{Guid.NewGuid()}",
                title,
                url,
                new Uri("ms-appx:///Assets/Square150x150Logo.png"),
                TileSize.Square150x150);
            await tile.RequestCreateAsync();
        }

        private void NavigateToAddress()
        {
            string address = AddressBar.Text;
            if (!address.StartsWith("http")) address = "https://" + address;
            try { Browser.Navigate(new Uri(address)); }
            catch { AddressBar.Text = "Invalid URL"; }
        }
    }
}