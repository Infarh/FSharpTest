namespace FSharpLib

open System.Collections.Generic
open AlgorithmsLib
open System.Linq

module StringTools =
    let WordsList = [ "Hello"; "Word"; "Data"; "Table"; "Stream"; "Index" ]

    let FuzzyMatch (words:string list) =
        let words_set = new HashSet<string>(words)
        let partial_fuzze_match word = 
            query { 
                for w in words_set.AsParallel() do 
                select (JaroWinkler.GetMatch(w, word), w)
            }
            |> Seq.sortBy(fun (distance, _) -> -distance)
            |> Seq.head
        fun word ->
            let (_, word) = partial_fuzze_match word
            word

    let WordsListFuzzyMatch = FuzzyMatch WordsList
