using Newtonsoft.Json;

namespace client.Infrastructure;

/// <summary>
/// Json конвертер
/// </summary>
public static class JsonConverter
{
    /// <summary>
    /// Конвертация объекта в строку
    /// </summary>
    /// <param name="o">Объект</param>
    /// <returns></returns>
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

    /// <summary>
    /// Конвертация строки в объект
    /// </summary>
    /// <param name="str">Строка</param>
    /// <typeparam name="T">Тип объекта</typeparam>
    /// <returns></returns>
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