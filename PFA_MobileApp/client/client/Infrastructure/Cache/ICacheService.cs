namespace client.Infrastructure.Cache;

public interface ICacheService
{
    Task<bool> SaveCredentialsToFile(AuthCacheModel authCacheModel);

    AuthCacheModel GetCredentialsFromFile();

    void DeleteCredentialsFile();
}