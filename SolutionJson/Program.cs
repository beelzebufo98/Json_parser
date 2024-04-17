using JsonLibrary;

namespace SolutionJson 
{
    class Program
    {
        static void Main()
        {
            string fPath = "";
            List<CustomerData> dataList = new();
            while (true)
            {
                try
                {
                    string action = Menu.GetMainMenuAction();

                    if (action != "1" && action != "2")
                    {
                        Logger.LogError("Выбран некорректный пункт меню");
                        continue;
                    }

                    if (action == "2")
                        break;

                    // Clear data before processing file again.
                    dataList.Clear();
                }
                catch (Exception ex)
                {
                    Logger.LogError($"Произошла ошибка: {ex.Message}");
                }

                while (true)
                {
                    try
                    {
                        JsonReadType type = Menu.GetDataMenuAction();
                        fPath = "";
                        if (type == JsonReadType.FILE)
                        {
                            try
                            {
                                fPath = FilePath.ReadFilePathValue("Введите путь к файлу с json данными: ");
                            }
                            catch(Exception ex)
                            {
                                Logger.LogError($"Произошла ошибка: {ex.Message}");
                                continue;
                            }
                        }
                        dataList = JsonParser.ReadJson(type, fPath);
                        Logger.LogSuccess("Данные успешно прочитаны!");
                        break;
                    }
                    catch(ArgumentNullException ex)
                    {
                        Logger.LogError($"Произошла ошибка: {ex.Message}");
                    }
                    catch(ArgumentException ex)
                    {
                        Logger.LogError($"Произошла ошибка: {ex.Message}");
                    }
                    catch(DirectoryNotFoundException ex)
                    {
                        Logger.LogError($"Произошла ошибка: {ex.Message}");
                    }
                    catch(IOException ex)
                    {
                        Logger.LogError($"Произошла ошибка: {ex.Message}");
                    }
                    catch (Exception ex)
                    {
                        Logger.LogError($"Произошла ошибка: {ex.Message}");
                    }
                }
                bool exitFlag = false;
                while (!exitFlag)
                {
                    string menuAction = Menu.GetFileMenuAction();
                    switch (menuAction)
                    {
                        case "1":
                            {
                                //Filter data by one of the fields.
                                FieldType data = Menu.GetFieldDataMenu();
                                if (data == FieldType.NONE)
                                {
                                    Console.WriteLine("Поле для сортировки не было выбрано");
                                    continue;
                                }
                                Console.Write("Введите значение поля: ");
                                string value = Console.ReadLine() ?? "";
                                if (string.IsNullOrEmpty(value))
                                {
                                    Console.WriteLine("Введено некорректное значение");
                                    continue;
                                }
                                DataProcessing.Filter(dataList, data, value, fPath);
                                break;
                            }
                        case "2":
                            {
                                FieldType data = Menu.GetFieldDataMenu();
                                if (data == FieldType.NONE)
                                {
                                    continue;
                                }
                                DataProcessing.Sort(ref dataList, data);
                                DataProcessing.SaveData(dataList, fPath);
                                Color.SetColor(ColorEnum.SUCCESS);
                                Console.WriteLine("Работа с данными завершена");
                                Color.Reset();
                                break;
                            }
                        case "3":
                            {
                                //completing work with file data
                                DataProcessing.SaveData(dataList, fPath);
                                Color.SetColor(ColorEnum.SUCCESS);
                                Console.WriteLine("Работа с данными завершена");
                                Color.Reset();
                                exitFlag = true;
                                break;
                            }
                        case "4":
                            {
                                exitFlag = true;
                                break;
                            }
                        default:
                            {
                                Logger.LogError("Выбран некорректный пункт меню");
                                break;
                            }
                    }

                    if (exitFlag)
                        break;
                }
            }
        }
    }
}