﻿using System;
using CSharpLib;

namespace FSharpTest
{
    internal static class FuncServiceTest
    {
        public static void ComposeTest()
        {
            var str = "Hello World";
            Func<string, int> get_length = s => s.Length;
            var get_sqr_str_len = get_length.Compose(len => len * len);

            var sqr_len = get_sqr_str_len(str);
        }
    }
}
