namespace FSharpLib

module Say =
    let PrintHello name =
        printfn "Hello %s" name

    let PrintData (data, value) = 
        printfn "Data %s, value %s" data value;
