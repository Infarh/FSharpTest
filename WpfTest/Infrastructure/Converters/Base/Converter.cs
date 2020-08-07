using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Markup;

namespace WpfTest.Infrastructure.Converters.Base
{
    internal abstract class ValueConverter : MarkupExtension, IValueConverter
    {
        public override object ProvideValue(IServiceProvider services) => this;

        object IValueConverter.Convert(object v, Type t, object p, CultureInfo c) => Convert(v, t, p, c);

        object IValueConverter.ConvertBack(object v, Type t, object p, CultureInfo c) => ConvertBack(c, t, p, c);

        protected abstract object Convert(object v, Type t, object p, CultureInfo c);

        protected virtual object ConvertBack(object v, Type t, object p, CultureInfo c) => throw new NotSupportedException("Обратное преобразование не поддерживается");
    }
}
