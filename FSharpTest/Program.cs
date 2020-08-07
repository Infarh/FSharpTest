using System;
using System.Collections.Generic;
using System.Linq;
using FSharpLib;
using FSharpTest.Models;
using Microsoft.FSharp.Collections;

namespace FSharpTest
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            //foreach (var i in Enumerable.Empty<int>().AddLast(1,2,3,4,5))
            //    Console.WriteLine($"Value {i}");

            RandomTests.Run();
        }
    }
}
