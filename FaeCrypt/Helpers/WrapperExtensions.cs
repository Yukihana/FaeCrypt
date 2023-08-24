using System.Text.Json;

namespace FaeCrypt.Helpers;

public static class WrapperExtensions
{
    public static bool TryDeserializeJson<T>(this string jsonText, out T? deserializedObject, JsonSerializerOptions? options = null)
    {
        try
        {
            deserializedObject = JsonSerializer.Deserialize<T>(jsonText, options);
            return true;
        }
        catch
        {
            deserializedObject = default;
            return false;
        }
    }
}