namespace FSharpLib

open System

module ImmetableTest =
    let point = (31, 57)
    let (x, y) = point

    type Person = { FirstName : string; LastName : string; Age : int }
    let person = { FirstName = "John"; LastName = "Doe"; Age = 42 }

    type Vehicle =
        | Motorcicle of int
        | Car of int
        | Truck of int

    let Print vehicle =
        match vehicle with
        | Car(n) -> Console.WriteLine("Car has {0} wheels", n)
        | Motorcicle(n) -> Console.WriteLine("Motorcicle has {0} wheels", n)
        | Truck(n) -> Console.WriteLine("Truck has {0} wheels", n)

    type FList<'a> =
        | Empty
        | List of head: 'a * tail:FList<'a>

    let rec map f (list:FList<'a>) =
        match list with
        | Empty -> Empty
        | List(head, tail) -> List(f head, map f tail)

    let rec filter p (list:FList<'a>) =
        match list with
        | Empty -> Empty
        | List(head, tail) when p head = true -> List(head, filter p tail)
        | List(_, tail) -> filter p tail

    let list1 = List(1, List(2, List(3, Empty)))
    let list2 = 1 :: 2 :: 3 :: []
    let list3 = [1; 2; 3]

    type LazyList<'a> =
        | List of head:'a * tail:Lazy<LazyList<'a>>
        | Empty
    //let empty = lazy(Empty)

    let rec Append items item =
        match items with
        | List(head, Lazy(tail)) -> List(head, lazy(Append tail item))
        | Empty -> item

    let empty<'a> = lazy(Empty)
    let list_1 = List(42, lazy(List(21, empty)))
    let list_2 = Append (List(3, empty)) list_1

    let rec ListIterator action list =
        match list with
        | List(head, Lazy(tail)) -> 
            action(head)
            ListIterator action tail
        | Empty -> ()

    let rec TestLazyListPrinter items = items |> ListIterator (printf "%d ..")