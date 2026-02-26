open System

//Проверка ввода целого неотрицательного числа размерности
let rec readSize () = 
    printf "Введите размер списка(целое неотрицательное число): "
    let sizeList = Console.ReadLine()
    try
        let value = int sizeList
        if value < 0 then
            printfn "Ошибка: число не может быть отрицательным."
            readSize()
        else
            value
    with
        | :? FormatException ->
            printfn "Ошибка: нужно ввести целое число."
            readSize()

//проверка элемента(float или int)
let rec readElement () = 
    printf "Введите элемент списка (число): "
    let element = Console.ReadLine()
    try
        float element
    with
        | :? FormatException ->
            printfn "Ошибка: нужно ввести число."
            readElement()

// Функция добавления элемента в конец списка
let rec addElement list element = 
    match list with
    | [] -> [element] // Если список пустой — создаём новый список с элементом
    | head :: tail -> head :: addElement tail element  
    // Иначе сохраняем голову и рекурсивно добавляем элемент в хвост

// Удаление первого найденного элемента
let rec removeElement list element = 
    match list with
    | [] -> []                               // Пустой список-ничего удалять
    | head :: tail ->
        if head = element then tail          // Если нашли элемент- возвращаем хвост (удаляем)
        else head :: removeElement tail element
        // Иначе сохраняем голову и продолжаем поиск

// Проверка наличия элемента в списке
let rec contains list element = 
    match list with
    | [] -> false                      // Если дошли до конца - элемента нет
    | head :: tail ->
        if head = element then true    // Если нашли -возвращаем true
        else contains tail element     // Иначе продолжаем поиск

// Объединение двух списков
let rec concat list1 list2 = 
    match list1 with
    | [] -> list2                      // Если первый список пуст-возвращаем второй
    | head :: tail -> head :: concat tail list2
    // Иначе добавляем элементы первого списка к началу второго

// Получение элемента по индексу
let rec getByIndex list index = 
    match list, index with
    | [], _ -> failwith "Индекс вне диапазона"   // Если список закончился
    | head :: _, 0 -> head                      // Если индекс 0- возвращаем голову
    | _ :: tail, _ when index > 0 -> 
        getByIndex tail (index - 1)             // Уменьшаем индекс и идём дальше
    | _ -> failwith "Индекс не может быть отрицательным"

let rec createList size = 
    if size = 0 then
        []
    else
        let value = readElement()
        value :: createList (size - 1)

let printList list = 
    if list = [] then
        printfn "Список пуст."
    else
        list |> List.iter (printf "%f ")
        printfn ""

let rec menu list = 
    printfn "ВЫБОР ДЕЙСТВИЯ"
    printfn "1.Создать список"
    printfn "2.Добавить элемент"
    printfn "3.Удалить элемент"
    printfn "4.Найти элемент"
    printfn "5.Сцепить со вторым списком"
    printfn "6.Получить элемент по индексу"
    printfn "7.Печать списка"
    printfn "0.Выход"

    match Console.ReadLine() with
    | "1" ->
        let size = readSize()
        let newList = createList size
        menu newList
    | "2" ->
        let value = readElement()
        let newList = addElement list value
        menu newList
    | "3" ->
        let value = readElement()
        let newList = removeElement list value
        menu newList
    | "4" ->
        let value = readElement()
        if contains list value then
            printfn "Элемент найден."
        else
            printfn "Элемент не найден."
        menu list
    | "5" ->
        printfn "Создайте второй список:"
        let size2 = readSize()
        let secondList = createList size2
        let newList = concat list secondList
        menu newList
    | "6" ->
        printf "Введите индекс: "
        let s = Console.ReadLine()
        try
            let index = int s
            let value = getByIndex list index
            printfn "Элемент: %f" value
        with
            | _ -> printfn "Ошибка индекса."
        menu list
    | "7" ->
        printList list
        menu list
    | "0" ->
        printfn "Программа завершена."
    | _ ->
        printfn "Неверный пункт."
        menu list
[<EntryPoint>]
let main args = 
    menu []
    0
