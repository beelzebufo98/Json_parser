
namespace JsonLibrary
{
    /// <summary>
    /// The class for sorting data from a Json file by the field that the user enters.
    /// </summary>
    public static class DataSort
    {
        public static int CompareTo(this CustomerData customer1, CustomerData customer2, FieldType fieldtype)
        {
            switch (fieldtype)
            {
                case FieldType.NAME:
                    return customer1.Name.CompareTo(customer2.Name);
                case FieldType.CUSTOMER_ID:
                    return customer1.CustomerId.CompareTo(customer2.CustomerId);
                case FieldType.CITY:
                    return customer1.City.CompareTo(customer2.City);
                case FieldType.EMAIL:
                    return customer1.Email.CompareTo(customer2.Email);
                case FieldType.AGE:
                    return customer1.Age.CompareTo(customer2.Age);
                case FieldType.IS_PREMIUM:
                    return customer1.IsPremium.CompareTo(customer2.IsPremium);
                case FieldType.ORDEDS:
                    string firstOrders = string.Join(", ", customer1.Orders);
                    string secondrOrders = string.Join(", ", customer2.Orders);
                    return firstOrders.CompareTo(secondrOrders);
                default:
                    throw new ArgumentException("Неподдерживаемый тип сортировки");

            }
        }
    }
}
