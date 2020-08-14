using System.Threading;

namespace System
{
    public static class ProgressInfo
    {
        private readonly struct SelectProgress<TResult, T> : IProgress<TResult>
        {
            private readonly IProgress<T> _Destination;
            private readonly Func<TResult, T> _Selector;

            public SelectProgress(IProgress<T> Destination, Func<TResult, T> Selector)
            {
                _Destination = Destination;
                _Selector = Selector;
            }

            public void Report(TResult value) => _Destination?.Report(_Selector(value));
        }

        public static IProgress<TResult> Combine<T, TResult>(this IProgress<T> Progress, Func<TResult, T> Selector) =>
            Progress is null
                ? default
                : new SelectProgress<TResult, T>(Progress, Selector ?? throw new ArgumentNullException(nameof(Selector)));

        public static IProgress<T> On<T>(Action<T> Reporter) => new Progress<T>(Reporter);

        private class GUIProgress<T> : IProgress<T>
        {
            private SynchronizationContext _Context;
            private readonly Action<T> _Reporter;

            public GUIProgress(Action<T> Reporter)
            {
                _Context = new SynchronizationContext();
                _Reporter = Reporter;
            }

            public void Report(T value) => throw new NotImplementedException();
        }

        public static IProgress<T> OnGUI<T>(Action<T> Reporter) => new GUIProgress<T>(Reporter);
    }
}
