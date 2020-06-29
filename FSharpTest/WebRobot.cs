using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;

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
            };

            var titles =
                from url in urls
                from page in WebClawer(url)
                select (page.url, title: GetTitle(page.content));

            foreach (var (url, title) in titles)
                Console.WriteLine("{0}:\t{1}", url, title);

        }

        private static string GetWebContent(string url)
        {
            try
            {
                using var client = new WebClient { Encoding = Encoding.UTF8 };
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

        private static IEnumerable<(string url, string content)> WebClawer(string url)
        {
            var result = (url, content: GetWebContent(url));
            yield return result;

            foreach (var content_url in GetUrls(result.content))
                yield return (content_url, GetWebContent(content_url));
        }
    }
}
