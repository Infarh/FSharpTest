using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.RegularExpressions;
using CSharpLib;

namespace FSharpTest
{
    internal static class WebRobot
    {
        public static void Test()
        {
            string[] urls =
            {
                "http://www.google.com",
                "http://www.microsoft.com",
                "http://www.bing.com",
                "http://www.google.com",
                "http://www.bing.com",
                "http://www.yandex.ru",
                "http://www.microsoft.com",
                "http://www.yandex.ru",
                "http://www.bing.com",
                "http://www.google.com",
            };

            var timer = new Stopwatch();
            var times = new List<(string url, double time)>();

            string ProcessUrl(string url, int i)
            {
                Console.WriteLine("[{1} / {2}]>>>>>{0}<<<<<", url, i + 1, urls.Length);
                Console.Title = $"[{i + 1} / {urls.Length}]:{url}";
                if (timer.IsRunning)
                {
                    Console.WriteLine("Elapsed time: {0:0.##}c", timer.Elapsed.TotalSeconds);
                    times.Add((urls[i - 1], timer.Elapsed.TotalSeconds));
                    timer.Reset();
                }
                else
                    timer.Start();
                return url;
            }

            var titles =
                from url in urls.Select(ProcessUrl)
                from page in WebCrawlerMemorized(url)
                select (page.url, title: GetTitle(page.content));

            foreach (var (url, title) in titles)
                Console.WriteLine("{0}:\t{1}", url, title);

            timer.Stop();
            times.Add((urls[^1], timer.Elapsed.TotalSeconds));

            Console.WriteLine(new string('-', Console.BufferWidth));

            times.ForEach(v => Console.WriteLine("{0}\t::\t{1}", v.url, v.time));
        }

        private sealed class TestWebClient : WebClient
        {
            public int Timeout { get; set; } = 30_000;

            protected override WebRequest GetWebRequest(Uri address)
            {
                var request = base.GetWebRequest(address);
                if (request is null) return null;
                request.Timeout = Timeout;
                return request;
            }
        }

        private static string GetWebContent(string url)
        {
            try
            {
                using var client = new TestWebClient { Encoding = Encoding.UTF8, Timeout = 7000 };
                return client.DownloadString(url);
            }
            catch (WebException)
            {
                Console.WriteLine("Error 404!");
                return string.Empty;
            }
        }

        private static readonly Regex __UrlRegEx = new Regex(@"(?<=href=('|""))https?://.*?(?=\1)", RegexOptions.Compiled);
        private static IEnumerable<string> GetUrls(string content) => __UrlRegEx.Matches(content).Select(m => m.Value);
        //{
        //    foreach (Match url in __UrlRegEx.Matches(content))
        //        yield return url.Value;
        //}

        private static readonly Regex __TitleRegEx = new Regex("<title>(?<title>.*?)<\\/title>", RegexOptions.Compiled);

        private static string GetTitle(string html) => __TitleRegEx.Match(html).Group("title", "No page Title found");
        //{
        //    var match = __TitleRegEx.Match(html);
        //    return match.Success ? match.Groups["title"].Value : "No page Title found";
        //}

        private static string Group(this Match match, string GroupName, string Default = null) =>
            match.Success && match.Groups[GroupName].Success
                ? match.Groups[GroupName].Value
                : Default;

        private static IEnumerable<(string url, string content)> WebCrawler(string url)
        {
            var result = (url, content: GetWebContentMemorized(url));
            yield return result;

            foreach (var content_url in GetUrls(result.content))
                yield return (content_url, GetWebContentMemorized(content_url));
        }

        private static readonly Func<string, string> GetWebContentMemorized =
            FuncService.Memorize<string, string>(GetWebContent);

        private static readonly Func<string, IEnumerable<(string url, string content)>> WebCrawlerMemorized =
            FuncService.Memorize<string, IEnumerable<(string url, string content)>>(WebCrawler);
    }
}
