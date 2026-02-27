
open System
let rec readElement () = 
    printf "Введите элемент списка: "
    let element= Console.ReadLine()
    try
        // Преобразовываем число во float
        float element  
    with
    // Тип исключения, которое возникает,
    //если не удалось преобразовать строку в число
    | :? FormatException -> 
        printfn "Ошибка: введите число."
    // Повторный запрос корректного числа 
        readElement () 

let rec readSize () = 
    printf "Введите количество элем: "
    let size = Console.ReadLine()
    try
        // Преобразовываем строку в intчисло
        let value = int size 
        if value < 0 then
            printfn "Ошибка: не может быть <0."
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
    // Создаёт список длиной size, 
    //все элементы которого равны element
    List.init size (fun _ -> element) 

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
