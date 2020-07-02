using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using FSharpTest.Models;

namespace FSharpTest.Servcies
{
    class WeatherService
    {
        public static Temperature GetTemperature(string City, params Uri[] Aggresses)
        {
            var cts = new CancellationTokenSource();

            var tasks =
                from address in Aggresses
                select Task.Run(() => GetTemperature(City, address), cts.Token);

            var tasks_array = tasks.ToArray();
            var completed_task_index = Task.WaitAny(tasks_array);
            cts.Cancel();
            return tasks_array[completed_task_index].Result;
        }

        private static Temperature GetTemperature(string City, Uri Address) => new WeatherService(City, Address).GetTemperature();

        public static async Task<Temperature> GetTemperatureAsync(string City, params Uri[] Aggresses)
        {
            var cts = new CancellationTokenSource();

            var tasks =
                from address in Aggresses
                select Task.Run(() => GetTemperature(City, address), cts.Token);

            var result = await Task.WhenAny(tasks).Unwrap();
            cts.Cancel();
            return result;
        }

        private readonly string _City;
        private readonly Uri _ServiceAddress;
        private readonly double _TemperatureAvg;
        private readonly double _TemperatureVar;
        private readonly Random _Rnd = new Random();

        private readonly int _TimeoutMin;
        private readonly int _TimeoutMax;

        public WeatherService(string City, Uri ServiceAddress, int TimeoutMin = 500, int TimeoutMax = 2000, double TemperatureAvg = 25, double TemperatureVar = 3)
        {
            _City = City;
            _ServiceAddress = ServiceAddress;
            _TemperatureAvg = TemperatureAvg;
            _TemperatureVar = TemperatureVar;
            _TimeoutMin = Math.Min(TimeoutMin, TimeoutMax);
            _TimeoutMax = Math.Max(TimeoutMin, TimeoutMax);
        }

        public Temperature GetTemperature()
        {
            var timeout = _Rnd.Next(_TimeoutMin, _TimeoutMax + 1);
            Thread.Sleep(_TimeoutMin);
            return new Temperature(_TemperatureAvg * (_Rnd.NextDouble() - 0.5) + _TemperatureAvg);
        }
    }
}
