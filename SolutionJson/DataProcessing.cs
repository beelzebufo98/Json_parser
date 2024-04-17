using JsonLibrary;

namespace SolutionJson
{
    public static class DataProcessing
    {
        public static void Filter(List<CustomerData> data, FieldType field, string value, string fPath)
        {
            try
            {
                List<CustomerData> filteredList = DataFilter.FilterByField(data, field, value);
                if (filteredList.Count >= 1)
                {
                    Logger.LogSuccess($"По фильтру найдено совпадений: {filteredList.Count}");
                    DataProcessing.SaveData(filteredList, fPath);
                    Color.SetColor(ColorEnum.SUCCESS);
                    Console.WriteLine("Работа с данными завершена");
                    Color.Reset();
                }
                else
                {
                    Logger.LogError($"По фильтру найдено совпадений: {filteredList.Count}");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"{ex.Message}");
            }
            
        }

        public static void Sort(ref List<CustomerData> list, FieldType fieldtype)
        {
            try
            {
                string type = Menu.GetTypeSortField();
                switch (type)
                {
                    case "1":
                        {
                            list.Sort((customer1, customer2) => customer1.CompareTo(customer2, fieldtype));
                            return;
                        }
                    case "2":
                        {
                            list.Sort((customer1, customer2) => customer2.CompareTo(customer1, fieldtype));
                            return;
                        }
                }
            }
            catch(Exception ex)
            {
                Console.WriteLine($"{ex.Message}");
            }
            
        }
       
        public static void SaveData(List<CustomerData> list, string fPath)
        {
            try
            {
                string output = Menu.FinishDataMenu();
                switch (output)
                {
                    case "1":

                        Console.WriteLine("[");
                        foreach (var item in list)
                        {
                            if (item == list[^1])
                            {
                                Console.WriteLine(item.ToString());
                            }
                            else
                            {
                                Console.WriteLine(item.ToString() + ',');
                            }
                        }
                        Console.WriteLine("]");
                        break;
                    case "2":
                        string action = Menu.GetChoicePath();
                        if (action == "1")
                        {
                            if (fPath == "")
                            {
                                Color.SetColor(ColorEnum.MENU);
                                Console.WriteLine("Путь не был введён в начале программы, пожалуйста, введите его для сохранения данных");
                                Color.Reset();
                                while (true)
                                {
                                    try
                                    {
                                        fPath = FilePath.ReadFilePathValue("Введите путь к файлу: ");
                                        JsonParser.WriteJson(list, fPath);
                                        break;
                                    }
                                    catch (Exception ex)
                                    {
                                        Logger.LogError($"Ошибка ввода-вывода: {ex.Message}");
                                        continue;
                                    }
                                }   
                            }
                            else
                            {
                                JsonParser.WriteJson(list, fPath);
                            }
                            
                        }
                        else if (action == "2")
                        {
                            while (true)
                            {
                                try
                                {
                                    fPath = FilePath.ReadFilePathValue("Введите путь к файлу: ");
                                    JsonParser.WriteJson(list, fPath);
                                    break;
                                }
                                catch (Exception ex)
                                {
                                    Logger.LogError($"Ошибка ввода-вывода: {ex.Message}");
                                    continue;
                                }
                            }
                        }
                        break;
                }
            }
            catch (FileNotFoundException ex)
            {
                Logger.LogError($"Файл не найден: {ex.Message}");
            }
            catch (IOException ex)
            {
                Logger.LogError($"Ошибка ввода-вывода: {ex.Message}");
            }
            catch (Exception ex)
            {
                Logger.LogError($"Произошла ошибка: {ex.Message}");
            }
        }
    }
}
