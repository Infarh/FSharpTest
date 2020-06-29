using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;

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
