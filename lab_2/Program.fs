open System

let rec readNatural () = 
    printf "Введите натуральное число: "
    let element= Console.ReadLine()
    try
        let value = int element
        if value < 0 then
            printfn "Ошибка: не может быть <0"
            readNatural ()
        else
            value
    with
    | :? FormatException ->
        printfn "Ошибка: введите целое число"
        readNatural ()

let rec productDigits n: int = 
    if n < 10 then n
    else
        let digit = n % 10
        let rest = n / 10
        digit * productDigits rest

[<EntryPoint>]
let main args = 
    let value = readNatural()
    let result = 
        if value = 0 then 0
        else productDigits value
    printfn "Произведение цифр нат.числа: %d" result
    0
