using System.Text;
using System.Text.RegularExpressions;

namespace JsonLibrary
{
    public enum JsonReadType
    {
        FILE = 1,
        CONSOLE = 2
    }

    /// <summary>
    /// The class is used to work with a Json file.
    /// </summary>
    public static class JsonParser
    {
        public static void WriteJson(List<CustomerData> data, string filePath)
        {
            using (StreamWriter sw = new(filePath))
            {
                Console.SetOut(sw);
                List<string> list = new();
                foreach (var item in data)
                {
                    //Converting an object to a string and writing to a file.
                    list.Add(item.ToString());
                }
                Console.Write($"[\n{String.Join(",\n", list)}\n]");
            }
            StreamWriter standardOutput = new(Console.OpenStandardOutput());
            standardOutput.AutoFlush = true;
            Console.SetOut(standardOutput);
            Console.OutputEncoding = Encoding.UTF8;
        }

        public static List<CustomerData> ReadJson(JsonReadType type, string fPath)
        {
            StringBuilder jsonData = new("");
            switch(type)
            {
                case JsonReadType.FILE:
                    {
                        using (StreamReader sr = new StreamReader(fPath))
                        {
                            Console.SetIn(sr);
                            while (true)
                            {
                                string? str = Console.ReadLine();
                                if (str == null)
                                    break;
                                jsonData.Append(str);
                            }
                        }
                        Console.SetIn(new StreamReader(Console.OpenStandardInput()));
                        Console.InputEncoding = Encoding.UTF8;
                        break;
                    }
                case JsonReadType.CONSOLE:
                    {
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.WriteLine("Введите данные в консоль");
                        Console.ResetColor();
                        var strBilder = new StringBuilder();
                        string? line = Console.ReadLine();

                        while (!string.IsNullOrEmpty(line))
                        {
                            strBilder.AppendLine(line);
                            line = Console.ReadLine();
                            if (string.IsNullOrEmpty(line))
                            {
                                Console.ForegroundColor = ConsoleColor.Blue;
                                Console.WriteLine("Данные заканчиваются пустой строкой");
                                Console.ResetColor();
                            }
                        }
                        
                        jsonData.Append(strBilder);
                        break;
                    }
                default:
                    {
                        return new List<CustomerData>(); 
                    }
            }
            List<JsonObject> objects = Parse(jsonData.ToString());

            List<CustomerData> values = new(objects.Count);
            foreach (var obj in objects) 
            {
                values.Add(new CustomerData(obj));
            }

            return values;
        }

        private static List<JsonObject> Parse(string json)
        {
            List<JsonObject> objects = new();

            JsonParserState state = JsonParserState.START;
            string stream = Regex.Replace(json, @"\r\n?|\n", "").Trim(' ');

            StringBuilder currentKey = new();
            StringBuilder currentValue = new();
            JsonObject jsonObject = new();

            for (int index = 0; index < stream.Length; index++)
            {
                char symbol = stream[index];
                switch (state)
                {
                    case JsonParserState.START:
                        {
                            if (symbol == '[')
                            {
                                state = JsonParserState.JSON_BEGIN;
                            }
                            else throw new Exception("Invalid json file begin");
                            break;
                        }
                    case JsonParserState.JSON_BEGIN:
                        {
                            if (symbol == '{')
                            {
                                state = JsonParserState.OBJECT_BEGIN;
                            }
                            else if (symbol == ']')
                            {
                                state = JsonParserState.JSON_END;
                            }
                            else CheckSymbol(symbol, " ");
                            break;
                        }
                    case JsonParserState.OBJECT_BEGIN:
                        {
                            if (symbol == '}')
                            {
                                state = JsonParserState.OBJECT_END;
                                objects.Add(jsonObject);
                                jsonObject = new JsonObject();
                            }
                            else if (symbol == '"')
                            {
                                state = JsonParserState.OBJECT_KEY_START;
                                currentKey.Clear();
                            }
                            else CheckSymbol(symbol, " ");
                            break;
                        }
                    case JsonParserState.OBJECT_KEY_START:
                        {
                            if (symbol == '"')
                            {
                                string key = currentKey.ToString();
                                if (key.Length == 0)
                                {
                                    throw new Exception("Json exception: empty key in object");
                                }
                                state = JsonParserState.OBJECT_KEY_END;
                                currentValue.Clear();
                            }
                            else if ("[]{} :,".Contains(symbol))
                            {
                                throw new Exception("Json exception: not allowed character in key name");
                            }
                            else currentKey.Append(symbol);
                            break;
                        }
                    case JsonParserState.OBJECT_KEY_END:
                        {
                            if (symbol == ':')
                            {
                                state = JsonParserState.OBJECT_VALUE;
                            }
                            else throw new Exception("Json exception: invalid sequence");
                            break;
                        }
                    case JsonParserState.OBJECT_VALUE:
                        {
                            if (symbol == ':')
                            {
                                throw new Exception("Json exception: invalid sequence");
                            }
                            else if (symbol == ',')
                            {
                                string value = currentValue.ToString().Trim(' ');
                                if (value.Length == 0)
                                {
                                    throw new Exception("Json exception: invalid value");
                                }
                                state = JsonParserState.OBJECT_BEGIN;
                                jsonObject.SetValue(currentKey.ToString(), value);
                                currentValue.Clear();
                                currentKey.Clear();
                            }
                            else if (symbol == '}')
                            {
                                string value = currentValue.ToString().Trim(' ');
                                if (value.Length == 0)
                                {
                                    throw new Exception("Json exception: invalid value");
                                }
                                jsonObject.SetValue(currentKey.ToString(), value);
                                objects.Add(jsonObject);
                                currentKey.Clear();
                                currentValue.Clear();

                                state = JsonParserState.OBJECT_END;
                            }
                            else if (symbol == '[')
                            {
                                currentValue.Append(symbol);
                                state = JsonParserState.OBJECT_ARRAY_VALUE;
                            }
                            else if (symbol == '"')
                            {
                                state = JsonParserState.OBJECT_STRING_VALUE;
                            }
                            else currentValue.Append(symbol);
                            break;
                        }
                    case JsonParserState.OBJECT_ARRAY_VALUE:
                        {
                            if ("[{}:".Contains(symbol))
                            {
                                throw new Exception("Json Exception: invalid sequence");
                            }
                            else
                            {
                                currentValue.Append(symbol);
                                if (symbol == ']')
                                    state = JsonParserState.OBJECT_VALUE;
                            }
                            break;
                        }
                    case JsonParserState.OBJECT_STRING_VALUE:
                        {
                            if (symbol == '"')
                            {
                                state = JsonParserState.OBJECT_VALUE;
                            }
                            else currentValue.Append(symbol);
                            break;
                        }
                    case JsonParserState.OBJECT_END:
                        {
                            if (symbol == ',')
                            {
                                state = JsonParserState.JSON_BEGIN;
                                jsonObject = new JsonObject();
                            }
                            else if (symbol == ']')
                            {
                                state = JsonParserState.JSON_END;
                            }
                            else if (symbol != ' ')
                            {
                                throw new Exception("Json Exception: invalid character in sequence");
                            }
                            break;
                        }
                    case JsonParserState.JSON_END:
                        {
                            throw new Exception("Json Exception: invalid character in sequence");
                        }
                }
            }

            if (state != JsonParserState.JSON_END)
                throw new Exception("Invalid Json Data");

            return objects;
        }

        private static bool CheckSymbol(char symbol, string allowedSymbols)
        {
            if (allowedSymbols.Contains(symbol))
                return true;
            else throw new Exception("Json exception: invalid json sequence");
        }
    }

}

