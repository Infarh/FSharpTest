namespace FSharpLib

open System.Collections.Generic

module Memoizer =
    let Memoize func =
        let table = Dictionary<_,_>()
        fun x -> if(table.ContainsKey(x)) then table.[x]
                 else
                     let result = func x
                     table.[x] <- result
                     result