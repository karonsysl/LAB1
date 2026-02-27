open System

// Проверка ввода целого 
//неотрицательного числа размерности
let rec readSize () = 
    printf "Введите размер списка: "
    let sizeList = Console.ReadLine()
    try
        let value = int sizeList
        if value < 0 then
            printfn "Ошибка: не может быть <0."
            readSize()
        else
            value
    with
        | :? FormatException ->
            printfn "Ошибка: нужно ввести целое."
            readSize()

// Проверка элемента(float или int)
let rec readElement () = 
    printf "Введите элемент списка: "
    let element = Console.ReadLine()
    try
        float element
    with
        | :? FormatException ->
            printfn "Ошибка: нужно число."
            readElement()

// Функция добавления элемента
//в конец списка
let rec addElement list element = 
    match list with
    // Если список пустой 
    // То создаём новый список с элементом
    | [] -> [element] 
    // Иначе сохраняем голову
    //и рекурсивно добавляем элемент в хвост
    | head :: tail -> head :: addElement tail element  

// Удаление первого найденного элемента
let rec removeElement list element = 
    match list with
    // Пустой список-ничего удалять
    | [] -> []                               
    | head :: tail ->
        // Если нашли элемент
        // То возвращаем хвост (удаляем)
        if head = element then tail          
        else head :: removeElement tail element
        // Иначе сохраняем голову и продолжаем поиск

// Проверка наличия элемента в списке
let rec contains list element = 
    match list with
    // Если дошли до конца-элемента нет
    | [] -> false                      
    | head :: tail ->
        // Если нашли -возвращаем true
        if head = element then true   
        // Иначе продолжаем поиск
        else contains tail element     

// Объединение двух списков
let rec concat list1 list2 = 
    match list1 with
    // Если 1 список пуст-возвращаем 2
    | [] -> list2                      
    // Иначе добавляем элементы 1 списка к началу 2
    | head :: tail -> head :: concat tail list2

// Получение элемента по индексу
let rec getByIndex list index = 
    match list, index with
    // Если список закончился
    | [], _ -> failwith "Индекс вне диапазона" 
    // Если индекс 0- возвращаем голову
    | head :: _, 0 -> head                      
    | _ :: tail, _ when index > 0 -> 
    // Уменьшаем индекс и идём дальше
        getByIndex tail (index - 1)             
    | _ -> failwith "Индекс не может быть <0"

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
