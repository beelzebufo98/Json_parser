
namespace JsonLibrary
{
    enum JsonParserState
    {
        START = 0,
        JSON_BEGIN,
        OBJECT_BEGIN,
        OBJECT_END,
        OBJECT_KEY_START,
        OBJECT_KEY_END,
        OBJECT_VALUE,
        OBJECT_ARRAY_VALUE,
        OBJECT_STRING_VALUE,
        JSON_END,
    }
}
