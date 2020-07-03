namespace FSharpLib

open System.Collections.Generic
open AlgorithmsLib
open System.Linq
open FSharp.Collections.ParallelSeq

module StringTools =
    let private WordsList = [ "Hello"; "Word"; "Data"; "Table"; "Stream"; "Index" ]

    type JaroWinklerResult = {
            Distance : double
            Word : string
        }

    let private JaroWinklerGetMatch prototype word = 
        let result = { Distance = JaroWinkler.GetMatch(prototype, word); Word = word }
        result

    let FuzzyMatch (words:string list) =
        let words_set = new HashSet<string>(words)
        let partial_fuzze_match word = 
            query { 
                for w in words_set.AsParallel() do 
                select (JaroWinklerGetMatch w word)
            }
            |> Seq.sortBy(fun v -> -v.Distance)
            |> Seq.map(fun v -> v.Word)
            |> Seq.head
        fun word -> partial_fuzze_match word

    let WordsListFuzzyMatch = FuzzyMatch WordsList

    let FuzzyMathPSeq (words:string list) =
        let words_set = new HashSet<string>(words)
        fun word ->
            words_set 
                |> PSeq.map(fun w -> JaroWinklerGetMatch w word) 
                |> PSeq.sortBy(fun v -> -v.Distance) 
                |> Seq.map(fun v -> v.Word)
                |> Seq.head

    let WordsListFuzzyMatchPSeq = FuzzyMathPSeq WordsList