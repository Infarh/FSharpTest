using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AlgorithmsLib;
using FSharpLib;
using FSharpTest.Models;
using Microsoft.FSharp.Collections;

namespace FSharpTest
{
    internal class Program
    {
        private static async Task Main(string[] args)
        {
            //foreach (var i in Enumerable.Empty<int>().AddLast(1,2,3,4,5))
            //    Console.WriteLine($"Value {i}");

            //RandomTests.Run();

            //var items = Enumerable.Range(1, 10).ToArray();
            //var list = FList.New(items);

            //var items = Enumerable.Range(1, 3).ToArray();
            //var list = FList<int>.NewRecursive(items);

            var progress = new Progress<(double, double)>(p => Console.Title = $"Progress {p.Item1:p2} : {p.Item2:p2}");

            foreach (var result_task in GetManyDataAsync(10, progress))
            {
                var result = await result_task;
                Console.WriteLine(result);
            }
        }
        
        private static IEnumerable<Task<double>> GetManyDataAsync(int Count, IProgress<(double, double)> Progress = null, CancellationToken Cancel = default)
        {
            Cancel.ThrowIfCancellationRequested();
            var rnd = new Random();
            
            for (var i = 0; i < Count; i++)
            {
                var percent = (double) (i + 1) / Count;
                var task = GetDataAsync(rnd, Progress.Combine((double p) => (percent, p)), Cancel);
                yield return task;
                task.Wait(Cancel);
            }
        }

        private static async Task<double> GetDataAsync(Random rnd, IProgress<double> Progress, CancellationToken Cancel)
        {
            var result = 0d;
            for (int i = 0, count = 10; i < count; i++)
            {
                await Task.Delay(100, Cancel);
                result += rnd.NextDouble();
                Cancel.ThrowIfCancellationRequested();
                Progress?.Report((double)(i+1) / count);
            }

            return result;
        }
    }
}
