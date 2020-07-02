using System;
using System.Collections.Generic;
using System.Text;

namespace FSharpTest.Models
{
    internal readonly struct Temperature
    {
        public static Temperature InCelsius(double t) => new Temperature(t);
        public static Temperature InFahrenheit(double t) => new Temperature((t - 32) * 0.5556);
        public static Temperature InKelvin(double t) => new Temperature(t + 273.15);

        public double CelsiusDegrees { get; }
        public double FahrenheitDegrees => CelsiusDegrees / 0.5556 + 32;
        public double KelvinDegrees => CelsiusDegrees - 273.15;

        public Temperature(double InCelsius) => this.CelsiusDegrees = InCelsius;
    }
}
