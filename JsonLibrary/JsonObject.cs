
namespace JsonLibrary
{
    public class JsonObject
    {
        private readonly Dictionary<string, string> _object = new();

        public JsonObject()
        {
        }

        public void SetValue(string key, string value)
        {
            _object[key] = value;
        }

        public string? GetValue(string key)
        {
            return _object[key];
        }

        public bool HasKey(string key) => _object.ContainsKey(key);

        public bool IsEmpty() => _object.Count == 0;

        public List<string> Keys()
        {
            return _object.Keys.ToList();
        }
    }
}
