namespace BasakSehirBurada.Application.Services.RedisServices;

public interface IRedisService
{

    Task AddDataAsync<T>(string key,T value);

    Task<T> GetDataAsync<T>(string key);


    Task RemoveDataAsync(string key);
}
