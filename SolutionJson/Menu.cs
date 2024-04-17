using JsonLibrary;

namespace SolutionJson
{
    /// <summary>
    /// The class that implements menus for user interaction.
    /// </summary>
    public static class Menu
    {
        public static string GetMainMenuAction()
        {
            Color.SetColor(ColorEnum.MENU);
            Console.WriteLine("---------Главное меню--------");
            Console.WriteLine("1. Работа с файлом");
            Console.WriteLine("2. Завершить работу программы");
            Color.Reset();
            string? action = Console.ReadLine();
            return action ?? "";
        }

        public static JsonReadType GetDataMenuAction()
        {
            while(true)
            {
                Color.SetColor(ColorEnum.MENU);
                Console.WriteLine("Как вы хотите считать данные ?");
                Console.WriteLine("1. Считать из файла");
                Console.WriteLine("2. Ввести с консоли");
                Color.Reset();
                string? action = Console.ReadLine();

                switch (action)
                {
                    case "1":
                        return JsonReadType.FILE;
                    case "2":
                        return JsonReadType.CONSOLE;
                    default:
                        {
                            Logger.LogError("Выбран некорректный пункт меню");
                            break;
                        }
                }
            }
        }

        public static string GetChoicePath()
        {
            Color.SetColor(ColorEnum.MENU);
            Console.WriteLine("Как вы хотите записать данные ?");
            Console.WriteLine("1. Записать в существующий файл");
            Console.WriteLine("2. Записать в новый файл");
            Color.Reset();
            string action = CheckData.CheckDigits();
            return action;
        }

        public static string FinishDataMenu()
        {
            Color.SetColor(ColorEnum.MENU);
            Console.WriteLine("Как вы хотите сохранить данные ?");
            Console.WriteLine("1. Вывести в консоль");
            Console.WriteLine("2. Сохранить в файл");
            Color.Reset();
            string action = CheckData.CheckDigits();
            return action;
        }

        public static FieldType GetFieldDataMenu()
        {
            Color.SetColor(ColorEnum.MENU);
            Console.WriteLine("Введите поле(нужно ввести цифру от 1 до 8):");
            Console.WriteLine("1 - поле 'customer_id'");
            Console.WriteLine("2 - поле 'name'");
            Console.WriteLine("3 - поле 'email'");
            Console.WriteLine("4 - поле 'age'");
            Console.WriteLine("5 - поле 'city'");
            Console.WriteLine("6 - поле 'is_premium'");
            Console.WriteLine("7 - поле 'orders'");
            Console.WriteLine("8 - если хотите вернуться назад");
            Color.Reset();
            int EnumValue = CheckData.CheckNumber();
            FieldType fieldType = (FieldType)EnumValue;
            
            return fieldType;
        }

        public static string GetTypeSortField()
        {
            Color.SetColor(ColorEnum.MENU);
            Console.WriteLine("Тип сортировки:");
            Console.WriteLine("1. Сортировка по возрастанию");
            Console.WriteLine("2. Сортировка по убыванию");
            Color.Reset();
            string type = CheckData.CheckDigits();
            return type;
        }
        
        public static string GetFileMenuAction()
        {
            Color.SetColor(ColorEnum.MENU);
            Console.WriteLine("Выберите действие с исходными данными:");
            Console.WriteLine("1. Отфильтровать данные по одному из полей");
            Console.WriteLine("2. Отсортировать данные по одному из полей");
            Console.WriteLine("3. Завершить работу с данными файла");
            Console.WriteLine("4. Вернуться в основное меню");
            Color.Reset();
            string? menu = Console.ReadLine();
            return menu ?? "";
        }
    }
}
