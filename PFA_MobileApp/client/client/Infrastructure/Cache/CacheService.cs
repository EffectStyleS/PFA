namespace client.Infrastructure.Cache;

/// <summary>
/// Сервис работы с локальными файлами на девайсе
/// </summary>
public class CacheService : ICacheService
{
    private static readonly string FilePath =
        Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "auth.config");
    
    /// <inheritdoc />
    public async Task<bool> SaveCredentialsToFile(AuthCacheModel authCacheModel)
    {
        try
        {
            await using var fileCreateStream = File.Create(FilePath);
            fileCreateStream.Close();
            
            await File.WriteAllTextAsync(FilePath, JsonConverter.ObjectToString(authCacheModel));
            return true;
        }
        catch
        {
            return false;
        }
    }

    /// <inheritdoc />
    public AuthCacheModel GetCredentialsFromFile()
    {
        if (!File.Exists(FilePath))
        {
            return new AuthCacheModel();
        }

        string strFromFile;
        try
        {
            strFromFile = File.ReadAllText(FilePath);
        }
        catch
        {
            return new AuthCacheModel();
        }

        return JsonConverter.StringToObject<AuthCacheModel>(strFromFile) ?? new AuthCacheModel();
    }

    /// <inheritdoc />
    public void DeleteCredentialsFile()
    {
        if (!File.Exists(FilePath))
        {
            return;
        }
        
        File.Delete(FilePath);
    }
}