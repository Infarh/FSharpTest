using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;

namespace CSharpLib
{
    public static class StringsService
    {
        public static string FuzzyMatch(IEnumerable<string> Words, string Word)
        {
            var word_set = new HashSet<string>(Words); // затратная по времени операция отбора неповторяющихся слов

            //var set = word_set
            //   .Select(word => (word, distance: JaroWinkler.Proximity(word, Word)))
            //   .OrderBy(w => w.distance)
            //   .Select(w => w.word);

            //var set = word_set.AsParallel()
            //   .Select(word => new {word, distance = JaroWinkler.Proximity(word, Word)})
            //   .OrderBy(v => v.distance)
            //   .Select(v => v.word);

            var set =
                from word in word_set.AsParallel()
                let distance = JaroWinkler.Proximity(word, Word)
                orderby distance
                select word;

            return set.FirstOrDefault();
        }

        public static Func<string, string> PartialFuzzyMath(IEnumerable<string> Words)
        {
            var words_set = new HashSet<string>(Words); // затратная по времени операция отбора неповторяющихся слов
            return Word =>
            {
                var set =
                    from word in words_set.AsParallel()
                    let distance = JaroWinkler.Proximity(word, Word)
                    orderby distance
                    select word;
                return set.FirstOrDefault();
            };
        }

        private static int[] Correlation(string pattern, string str)
        {
            Swap(ref pattern, ref str);
            static void Swap(ref string a, ref string b)
            {
                if (a.Length <= b.Length) return;
                var c = a;
                a = b;
                b = c;
            }

            var pattern_length = pattern.Length;
            var count = str.Length - pattern_length + 1;
            var result = new int[count];
            for (var i = 0; i < count; i++)
            {
                var eq_count = 0;
                for (var j = 0; j < pattern_length; j++)
                    if (pattern[j] == str[i + j])
                        eq_count++;
                result[i] = eq_count;
            }

            return result;
        }

        private static IEnumerable<double> CorrelationNormal(string pattern, string str)
        {
            Swap(ref pattern, ref str);
            static void Swap(ref string a, ref string b)
            {
                if (a.Length <= b.Length) return;
                var c = a;
                a = b;
                b = c;
            }

            var pattern_length = pattern.Length;
            var count = str.Length - pattern_length + 1;
            var result = new int[count];
            for (var i = 0; i < count; i++)
            {
                var eq_count = 0;
                for (var j = 0; j < pattern_length; j++)
                    if (pattern[j] == str[i + j])
                        eq_count++;
                yield return eq_count / (double)pattern_length;
            }
        }
    }
}
