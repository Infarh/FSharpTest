using FSharpLib;
using Microsoft.FSharp.Collections;

namespace FSharpTest
{
    class Program
    {
        static void Main(string[] args)
        {
            var words = new[] {"Hello", "Word", "Data", "Table", "Stream", "Index"};
            var finder = StringTools.FuzzyMatch(ListModule.OfSeq(words));
            var result = finder.Invoke("Halo");

            var r2 = StringTools.WordsListFuzzyMatchPSeq.Invoke("Halo");
        }
    }
}
