
open System
let rec readElement () = 
    printf "Введите элемент(целое или вещественное число): "
    let element= Console.ReadLine()
    try
        float element //преобразовываем число во float 
    with
    | :? FormatException -> //тип исключения, которое возникает, если не удалось преобразовать строку в число
        printfn "Ошибка: введите число."
        readElement () // повторный запрос корректного числа 

let rec readSize () = 
    printf "Введите количество (целое неотрицательное число): "
    let size = Console.ReadLine()
    try
        let value = int size //преобразовываем строку в intчисло
        if value < 0 then
            printfn "Ошибка: число не может быть отрицательным."
            readSize ()
        else
            value
    with
    | :? FormatException ->
        printfn "Ошибка: введите целое число."
        readSize ()

let dialogueUser () = 
    printf "Ваш сформированный список: "

let createList element size = 
    List.init size (fun _ -> element) //создаёт список длиной size, все элементы которого равны element

let printList list = 
    list |> List.iter (printf "%A ")

[<EntryPoint>]
let main args = 
    let element = readElement()
    let size = readSize()
    let list = createList element size
    dialogueUser ()
    printList list
    0
