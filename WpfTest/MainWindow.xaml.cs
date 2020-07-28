using System;
using System.Windows;
using Microsoft.AspNet.SignalR.Client;

namespace WpfTest
{
    public partial class MainWindow
    {
        private IHubProxy _Hub;

        public MainWindow()
        {
            InitializeComponent();

            const string hub_address = "http://localhost:5000/information";
            var connection = new HubConnection(hub_address);
            _Hub = connection.CreateHubProxy("ServerHub");
            _Hub.On("HubEventName", new Action<string>(OnHubMessageReceived));

            _ = connection.Start();
        }

        private void OnHubMessageReceived(string Message)
        {
            Title = Message;
        }

        private void OnLoadButtonClick(object Sender, RoutedEventArgs E)
        {
            //UpdateImage(UrlInput.Text);
        }

        //private void UpdateImage(string url)
        //{
        //    var image = img;
        //    var client = new WebClient();
        //    client.DownloadDataCompleted += (s, e) =>
        //    {
        //        if (image == null) return;
        //        using var stream = new MemoryStream(e.Result);
        //        var converter = new ImageSourceConverter();
        //        image.Source = (ImageSource)converter.ConvertFrom(stream);
        //    };
        //    client.DownloadDataAsync(new Uri(url));
        //}
    }
}
