using System;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using FSharpLib;
using Microsoft.FSharp.Collections;

namespace FSharpTest
{
    class Program
    {
        static void Main(string[] args)
        {
            const int data_count = 1_000_000;
            const int iterations_count = 20;

            var result = Enumerable
               .Range(1, iterations_count)
               .Select(i =>
                {
                    Console.WriteLine("Итерация {0}", i);
                    return TestSort(data_count);
                })
               .ToArray();

            var sequental_avg = result.Average(r => r.Sequental) / 1000d; 
            var sequental_avg2 = result.Average(r => r.Sequental * r.Sequental) / 1000d / 1000d;
            var parallel_avg = result.Average(r => r.Parallel) / 1000d;
            var parallel_avg2 = result.Average(r => r.Parallel * r.Parallel) / 1000d / 1000d;
            var parallel_with_depth_avg = result.Average(r => r.ParallelWithDepth) / 1000d; 
            var parallel_with_depth_avg2 = result.Average(r => r.ParallelWithDepth * r.ParallelWithDepth) / 1000d / 1000d;

            var sequental_var = sequental_avg2 - sequental_avg * sequental_avg;
            var parallel_var = parallel_avg2 - parallel_avg * parallel_avg;
            var parallel_with_depth_var = parallel_with_depth_avg2 - parallel_with_depth_avg * parallel_with_depth_avg;

            var sequental_stdev = Math.Sqrt(sequental_var);
            var parallel_stdev = Math.Sqrt(parallel_var);
            var parallel_with_depth_stdev = Math.Sqrt(parallel_with_depth_var);

            Console.WriteLine("Seq      :{0:0.##}c (±{1:0.##}с)", sequental_avg, sequental_stdev);
            Console.WriteLine("Parallel :{0:0.##}c (±{1:0.##}с)", parallel_avg, parallel_stdev);
            Console.WriteLine("Par.depth:{0:0.##}c (±{1:0.##}с)", parallel_with_depth_avg, parallel_with_depth_stdev);

            Console.ReadLine();
        }

        private static (long Sequental, long Parallel, long ParallelWithDepth) TestSort(int count)
        {
            var data1 = ListModule.OfSeq(GetRandom(count));

            Console.Write("Последовательная сортировка...");
            var sequental_timer = Stopwatch.StartNew();
            var stop_console_timer = Timer(sequental_timer);
            Sort.QuickSort(data1);
            stop_console_timer();
            Console.CursorLeft -= 3;
            Console.WriteLine(" закончена за {0:.0##} с", sequental_timer.Elapsed.TotalSeconds);

            var data2 = ListModule.OfSeq(GetRandom(count));
            Console.Write("Параллельная сортировка...");
            var parallel_timer = Stopwatch.StartNew();
            stop_console_timer = Timer(parallel_timer);
            Sort.QuickSortParallel(data2);
            stop_console_timer();
            Console.CursorLeft -= 3;
            Console.WriteLine(" закончена за {0:.0##} с", parallel_timer.Elapsed.TotalSeconds);

            var data3 = ListModule.OfSeq(GetRandom(count));
            var depth = (int)(Math.Log(Environment.ProcessorCount, 2) + 4);
            Console.Write("Параллельная сортировка на глубину {0}...", depth);
            var parallel_with_depth_timer = Stopwatch.StartNew();
            stop_console_timer = Timer(parallel_with_depth_timer);
            Sort.QuickSortParallelWithDepth(depth, data3);
            stop_console_timer();
            Console.CursorLeft -= 3;
            Console.WriteLine(" закончена за {0:.0##} с", parallel_with_depth_timer.Elapsed.TotalSeconds);

            Console.WriteLine();

            return (sequental_timer.ElapsedMilliseconds, parallel_timer.ElapsedMilliseconds, parallel_with_depth_timer.ElapsedMilliseconds);
        }

        private static double[] GetRandom(int count)
        {
            var rnd = new Random();
            var data = new double[count];
            for (var i = 0; i < count; i++)
                data[i] = rnd.NextDouble();
            return data;
        }

        private static Action Timer(Stopwatch timer, int timeout = 100)
        {
            var cancellation = new CancellationTokenSource();

            var cancel = cancellation.Token;

            Task.Run(async () =>
            {
                while (!cancel.IsCancellationRequested)
                {
                    cancel.ThrowIfCancellationRequested();
                    await Task.Delay(timeout, cancel);
                    Console.Title = $"Elapsed {timer.Elapsed.TotalSeconds:0.##} c";
                }

                Console.Title = "Completed!";
                cancel.ThrowIfCancellationRequested();
            }, cancel);

            return () =>
            {
                timer.Stop();
                cancellation.Cancel();
            };
        }
    }
}
