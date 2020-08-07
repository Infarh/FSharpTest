using System;
using System.Diagnostics;
using System.Globalization;
using System.Windows.Markup;
using WpfTest.Infrastructure.Converters.Base;

namespace WpfTest.Infrastructure.Converters
{
    [MarkupExtensionReturnType(typeof(DebugConverter))]
    internal class DebugConverter : ValueConverter
    {
        public bool BreakOnConvert { get; set; } = true;
        public bool BreakOnConvertBack { get; set; } = true;

        public string Name { get; set; }

        protected override object Convert(object v, Type t, object p, CultureInfo c)
        {
            if (BreakOnConvert)
                Debugger.Break();
            return v;
        }

        protected override object ConvertBack(object v, Type t, object p, CultureInfo c)
        {
            if(BreakOnConvertBack)
                Debugger.Break();
            return v;
        }
    }
}
