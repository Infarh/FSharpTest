namespace FSharpLib

module FunctionService =
    let Add4 x = x + 4
    let Mult3 x = x * 3
    let ListSrc = [0..100]
    let List1 = List.map(fun x -> Mult3(Add4(x))) ListSrc
    let List2 = ListSrc |> List.map(Add4 >> Mult3)


