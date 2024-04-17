
namespace JsonLibrary
{
    public class CustomerData
    {
        public int CustomerId { get; private set; }
        public string Name { get; private set; }
        public string Email { get; private set; }
        public int Age { get; private set; }
        public string City { get; private set; }
        public bool IsPremium { get; private set; }
        public List<string> Orders { get; private set; } = new List<string>();

        public CustomerData()
        {
            CustomerId = 0;
            Name = string.Empty;
            Email = string.Empty;
            Age = 0;
            City = string.Empty;
            IsPremium = false;
        }

        public CustomerData(JsonObject obj)
        {
            if (obj.HasKey("customer_id"))
            {
                if (int.TryParse(obj.GetValue("customer_id"), out int value))
                {
                    CustomerId = value;
                }
                else throw new Exception("Invalid customer_id value");
            }
            else throw new Exception("Missing customer_id key in JsonObject");

            if (obj.HasKey("name"))
            {
                string name = obj.GetValue("name") ?? "";
                Name = name;
            }
            else throw new Exception("Missing name key in JsonObject");

            if (obj.HasKey("email"))
            {
                string email = obj.GetValue("email") ?? "";
                Email = email;
            }
            else throw new Exception("Missing name email in JsonObject");

            if (obj.HasKey("age"))
            {
                if (int.TryParse(obj.GetValue("age"), out int value))
                {
                    Age = value;
                }
                else throw new Exception("Invalid age value");
            }
            else throw new Exception("Missing name age in JsonObject");

            if (obj.HasKey("city"))
            {
                string city = obj.GetValue("city") ?? "";
                City = city;
            }
            else throw new Exception("Missing name city in JsonObject");

            if (obj.HasKey("is_premium"))
            {
                if (bool.TryParse(obj.GetValue("is_premium"),out bool value))
                {
                    IsPremium = value;
                }
                else throw new Exception("Invalid is_premium value");
            }
            else throw new Exception("Missing name is_premium in JsonObject");

            if (obj.HasKey("orders"))
            {
                string value = obj.GetValue("orders") ?? "";
                if (value[0] == '['  && value[^1] == ']')
                {
                    value = value.Replace("\"", "");
                    Orders = value.Substring(1, value.Length-2).Split(',').ToList();
                    for(int i = 0; i < Orders.Count; i++)
                    {
                        Orders[i] = Orders[i].Trim(' ');
                    }
                }
                else throw new Exception("Invalid orders value");
            }
        }

        private static List<string> Convert(List<string> order)
        {
            List<string> modifiedList = new();
            foreach (var item in order)
            {
                modifiedList.Add("\"" + item + "\"");
            }
            return modifiedList;
        }
        public override string ToString()
        {
            string orders = Orders.Count > 0 ? $"[\n      {String.Join(",\n      ", Convert(Orders))} \n    ]" : "[]";
            return $"  {{\n    \"customer_id\": {CustomerId},\n    \"name\": \"{Name}\",\n    \"email\": \"{Email}\",\n    \"age\": {Age},\n    \"city\": \"{City}\",\n    \"is_premium\": {IsPremium.ToString().ToLower()},\n    \"orders\": {orders}\n  }}";
        }
    }
}
