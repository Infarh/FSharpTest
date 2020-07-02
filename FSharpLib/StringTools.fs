namespace FSharpLib

open System.Collections.Generic
open AlgorithmsLib
open System.Linq
open FSharp.Collections.ParallelSeq

module StringTools =
    let private WordsList = [ "Hello"; "Word"; "Data"; "Table"; "Stream"; "Index" ]

    let private JaroWinklerGetMatch prototype word = (JaroWinkler.GetMatch(prototype, word), prototype)
    let private Distance = fun (distance, _) -> -distance

    let FuzzyMatch (words:string list) =
        let words_set = new HashSet<string>(words)
        let partial_fuzze_match word = 
            query { 
                for w in words_set.AsParallel() do 
                select (JaroWinklerGetMatch w word)
            }
            |> Seq.sortBy Distance
            |> Seq.head
        fun word ->
            let (_, word) = partial_fuzze_match word
            word

    let WordsListFuzzyMatch = FuzzyMatch WordsList

    let FuzzyMathPSeq (words:string list) =
        let words_set = new HashSet<string>(words)
        fun word ->
            let (_, word) = (words_set 
                    |> PSeq.map(fun w -> JaroWinklerGetMatch w word) 
                    |> PSeq.sortBy Distance 
                    |> Seq.head)
            word

    let WordsListFuzzyMatchPSeq = FuzzyMathPSeq WordsList

