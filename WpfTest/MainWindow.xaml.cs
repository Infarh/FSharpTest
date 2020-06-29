using System;
using System.IO;
using System.Net;
using System.Windows;
using System.Windows.Media;

namespace WpfTest
{
    public partial class MainWindow
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void OnLoadButtonClick(object Sender, RoutedEventArgs E)
        {
            UpdateImage(UrlInput.Text);
        }

        private void UpdateImage(string url)
        {
            var image = img;
            var client = new WebClient();
            client.DownloadDataCompleted += (s, e) =>
            {
                if (image == null) return;
                using var stream = new MemoryStream(e.Result);
                var converter = new ImageSourceConverter();
                image.Source = (ImageSource)converter.ConvertFrom(stream);
            };
            client.DownloadDataAsync(new Uri(url));
        }
    }
}
