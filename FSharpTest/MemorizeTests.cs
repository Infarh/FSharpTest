using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;
using CSharpLib;

namespace FSharpTest
{
    internal static class MemorizeTests
    {
        public static void Run()
        {
            TimingTest();

            Console.ReadLine();
        }

        private static void TimingTest()
        {
            static string GetString(string str) => $"{str} {DateTime.Now:HH:mm:ss}";
            Func<string, string> get_str = GetString;

            Console.WriteLine(get_str("v1"));
            Thread.Sleep(2000);
            Console.WriteLine(get_str("v2"));
            Thread.Sleep(2000);
            Console.WriteLine(get_str("v1"));

            Console.WriteLine(new string('-', Console.BufferWidth));

            var mem_str = get_str.Memorize();

            Console.WriteLine(mem_str("v1"));
            Thread.Sleep(2000);
            Console.WriteLine(mem_str("v2"));
            Thread.Sleep(2000);
            Console.WriteLine(mem_str("v1"));
        }
    }
}
