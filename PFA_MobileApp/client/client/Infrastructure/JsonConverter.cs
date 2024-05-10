using Newtonsoft.Json;

namespace client.Infrastructure;

public static class JsonConverter
{
    public static string ObjectToString(object o)
    {
        try
        {
            return JsonConvert.SerializeObject(o);
        }
        catch
        {
            return string.Empty;
        }
    }

    public static T? StringToObject<T>(string? str)
    {
        if (str == null)
        {
            return default;
        }

        try
        {
            return JsonConvert.DeserializeObject<T>(str);
        }
        catch
        {
            return default;
        }
    }
}