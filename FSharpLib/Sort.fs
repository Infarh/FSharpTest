namespace FSharpLib

open System.Threading.Tasks

module Sort =
    let rec QuickSort list =
        match list with
        | [] -> []
        | head :: tail ->
            let smaller, larger = List.partition(fun n -> n < head) tail
            QuickSort smaller @ (head :: QuickSort larger)

    let rec QuickSortParallel list =
        match list with
        | [] -> []
        | head :: tail ->
            let smaller, larger = List.partition(fun n -> n < head) tail
            let left = Task.Run(fun () -> QuickSortParallel smaller)
            let right = Task.Run(fun () -> QuickSortParallel larger)
            left.Result @ (head :: right.Result)

    let rec QuickSortParallelWithDepth depth list =
        match list with
        | [] -> []
        | head :: tail ->
            let smaller, larger = List.partition(fun n -> n < head) tail
            if depth < 0 then
                let left = QuickSortParallelWithDepth depth smaller
                let right = QuickSortParallelWithDepth depth larger
                left @ (head :: right)
            else
                let left = Task.Run(fun () -> QuickSortParallelWithDepth (depth - 1) smaller)
                let right = Task.Run(fun () -> QuickSortParallelWithDepth (depth - 1) larger)
                left.Result @ (head :: right.Result)
