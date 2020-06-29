using System;
using System.Net;
using SixLabors.ImageSharp;

namespace FSharpTest
{
    internal static class ImageProcessorTest
    {
        private static void UpdateImage(string url)
        {
            Image img = null;

            var client = new WebClient();
            client.DownloadDataCompleted += (s, e) =>
            {
                img = Image.Load(Configuration.Default, e.Result);
            };
            client.DownloadDataAsync(new Uri(url));

        }
    }
}
