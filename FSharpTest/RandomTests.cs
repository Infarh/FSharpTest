using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FSharpTest
{
    static class RandomTests
    {
        class RND
        {
            private static readonly double __K = Math.Sqrt(12);
            private readonly Random _Rnd = new Random();

            private readonly double[] _Values;
            private int _Index;

            public RND(int Count) => _Values = Enumerable.Range(0, Count).Select(i => __K * (_Rnd.NextDouble() - 0.5)).ToArray();

            public double Next()
            {
                _Values[_Index] = __K * (_Rnd.NextDouble() - 0.5);

                var length = _Values.Length;
                _Index = (_Index + 1) % length;

                var result = 1d;
                for (var i = 0; i < length; i++)
                    result *= _Values[i];

                return result;
            }
        }

        public static void Run()
        {
            var N = 6;
            var rnd = new RND(N);

            var count = 1000000;
            var values = new double[count];
            for (var i = 0; i < count; i++)
                values[i] = rnd.Next();

            var avg = values.Average();
            var avg2 = values.Average(x => x * x);

            var D = avg2 - avg * avg;
            var sgm = Math.Sqrt(D);

            var min = values.Min();
            var max = values.Max();
            var delta = max - min;
            var h_count = 100;
            var d_delta = delta / h_count;
            var hist = values
               .GroupBy(x => Math.Round(x / d_delta) * d_delta)
               .OrderBy(x => x.Key)
               .Select(value => (value.Key, Count:value.Count()))
               .ToArray();
        }
    }
}
