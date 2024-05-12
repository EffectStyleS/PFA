namespace client.Infrastructure.Cache;

/// <summary>
/// Интерфейс сервиса работы с локальными файлами на девайсе
/// </summary>
public interface ICacheService
{
    /// <summary>
    /// Сохрание данных пользователя в файл
    /// </summary>
    /// <param name="authCacheModel">Данные пользователя</param>
    /// <returns>True - успешное сохранение, иначе - false</returns>
    Task<bool> SaveCredentialsToFile(AuthCacheModel authCacheModel);

    /// <summary>
    /// Получение данных пользователя из файла
    /// </summary>
    /// <returns></returns>
    AuthCacheModel GetCredentialsFromFile();

    /// <summary>
    /// Удаление файла с данными пользователя
    /// </summary>
    void DeleteCredentialsFile();
}