
namespace JsonLibrary
{
    /// <summary>
    /// Filtering of data read from a Json file, 
    /// depending on the name of the field,
    /// that the user selects and the value that he enters for this field.
    /// </summary>
    public static class DataFilter
    {
        public static List<CustomerData> FilterByField(List<CustomerData> list, FieldType field, string value)
        {
            List<CustomerData> filteredValues = new();
            foreach (var customer in list)
            {
                if (FilterField(customer, field, value))
                {
                    filteredValues.Add(customer);
                }
            }
            return filteredValues;
        }

        public static bool FilterField(CustomerData customer, FieldType field, string value)
        {
            switch (field)
            {
                case FieldType.NAME:
                    return customer.Name == value;
                case FieldType.CUSTOMER_ID:
                    int number;
                    if (int.TryParse(value, out number))
                    {
                        return customer.CustomerId == number;
                    }
                    else
                    {
                        Console.WriteLine("Incorrect format for field 'CustomerId'");
                        return false;
                    }
                case FieldType.CITY:
                    return customer.City == value;
                case FieldType.EMAIL:
                    return customer.Email == value;
                case FieldType.AGE:
                    int age;
                    if (int.TryParse(value, out age))
                    {
                        return customer.Age == age;
                    }
                    else
                    {
                        Console.WriteLine("Incorrect format for field 'Age'");
                        return false;
                    }
                case FieldType.IS_PREMIUM:
                    bool ispremium;
                    if (bool.TryParse(value, out ispremium))
                    {
                        return customer.IsPremium == ispremium;
                    }
                    else
                    {
                        Console.WriteLine("Incorrect format for field 'IsPremium'");
                        return false;
                    }
                case FieldType.ORDEDS:
                    try
                    {
                        return customer.Orders.Any(o => o.Equals(value));
                    }
                    catch (Exception)
                    {
                        Console.WriteLine("Неправильный формат значения для поля 'Orders'");
                        return false;
                    }
                default:
                    Console.WriteLine("Incorrect field for filtering");
                    return false;
            }
        }
    }
    
}

///<summary>
///Альтернативное решение может быть использование LINQ для фильтрации данных. 
///Вместо явного цикла и проверок через switch, можно воспользоваться методами LINQ для удобного и читаемого фильтра списка данных. 
///Например, вы можете переписать метод FilterByField следующим образом:
///Возможное альтернативное решение представлено ниже.
///</summary>
/*
public static class DataFilter
{
    public static List<CustomerData> FilterByField(List<CustomerData> list, FieldType field, string value)
    {
        switch (field)
        {
            case FieldType.NAME:
                return list.Where(customer => customer.Name == value).ToList();
            case FieldType.CUSTOMER_ID:
                if (int.TryParse(value, out int number))
                {
                    return list.Where(customer => customer.CustomerId == number).ToList();
                }
                else
                {
                    Console.WriteLine("Incorrect format for field 'CustomerId'");
                    return new List<CustomerData>();
                }
            case FieldType.CITY:
                return list.Where(customer => customer.City == value).ToList();
            case FieldType.EMAIL:
                return list.Where(customer => customer.Email == value).ToList();
            case FieldType.AGE:
                if (int.TryParse(value, out int age))
                {
                    return list.Where(customer => customer.Age == age).ToList();
                }
                else
                {
                    Console.WriteLine("Incorrect format for field 'Age'");
                    return new List<CustomerData>();
                }
            case FieldType.IS_PREMIUM:
                if (bool.TryParse(value, out bool isPremium))
                {
                    return list.Where(customer => customer.IsPremium == isPremium).ToList();
                }
                else
                {
                    Console.WriteLine("Incorrect format for field 'IsPremium'");
                    return new List<CustomerData>();
                }
            case FieldType.ORDEDS:
                try
                {
                    return list.Where(customer => customer.Orders.Any(o => o.Equals(value))).ToList();
                }
                catch (Exception)
                {
                    Console.WriteLine("Incorrect format for field 'Orders'");
                    return new List<CustomerData>();
                }
            default:
                Console.WriteLine("Incorrect field for filtering");
                return new List<CustomerData>();
        }
    }
}

 */