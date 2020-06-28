using System;
using System.Collections.Generic;
using System.Text;

namespace CSharpLib
{
    public static class FuncService
    {
        public static Func<TValue, TResult> Compose<TValue, TArgument, TResult>(
            this Func<TValue, TArgument> GetArgument, 
            Func<TArgument, TResult> GetResult) => 
            value => GetResult(GetArgument(value));
    }
}
