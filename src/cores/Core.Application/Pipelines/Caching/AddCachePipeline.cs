using MediatR;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Configuration;
using System.Text;
using System.Text.Json;

namespace Core.Application.Pipelines.Caching;

public class AddCachePipeline<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>

    where TRequest : IRequest<TResponse>, ICachableRequest
{

    private readonly CacheSettings _cacheSettings;
    private readonly IDistributedCache _cache;

    public AddCachePipeline(IDistributedCache cache, IConfiguration configuration)
    {

        _cacheSettings = configuration.GetSection("CacheSettings").Get<CacheSettings>();
        _cache = cache;
    }

    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        if (request.ByPassCache)
        {
            return await next();
        }

   

        TResponse response;

        byte[]? cachedResponse = await _cache.GetAsync(request.CacheKey);
        if(cachedResponse is not null)
        {
            response = JsonSerializer.Deserialize<TResponse>(
                Encoding.Default.GetString(cachedResponse)
                );
        }

        else
        {
            response = await GetResponseAndAddedCache(request,next);
        }

        return response;
    }

    private async Task<TResponse> GetResponseAndAddedCache(TRequest request, RequestHandlerDelegate<TResponse> next)
    {
        TResponse response = await next();

        TimeSpan slidinegExpiration = request.SlidingExpiration ?? TimeSpan.FromHours(_cacheSettings.SlidingExpiration);

        DistributedCacheEntryOptions cacheEntryOptions = new DistributedCacheEntryOptions()
        {
            SlidingExpiration = slidinegExpiration
        };


        byte[] seralizedData = Encoding.UTF8.GetBytes(JsonSerializer.Serialize(response));

        await _cache.SetAsync(request.CacheKey, seralizedData,cacheEntryOptions);

        if(request.CacheGroupKey is not null)
        {
            await AddCacheKeyToGroup(request,slidinegExpiration);
        }

        return response;

    }

    private async Task AddCacheKeyToGroup(TRequest request, TimeSpan expiration)
    {
        byte[]? cachedGroupCache = await _cache.GetAsync(request.CacheGroupKey);
        HashSet<string> cacheKeysInGroup;


        if (cachedGroupCache is not null)
        {
            cacheKeysInGroup = JsonSerializer.Deserialize<HashSet<string>>(Encoding.Default.GetString(cachedGroupCache));

            if (!cacheKeysInGroup.Contains(request.CacheKey))
            {
                cacheKeysInGroup.Add(request.CacheKey);
            }

        }
        else
        {
            cacheKeysInGroup = new HashSet<string>(new[] { request.CacheKey });
        }

            byte[] newGroupCache = JsonSerializer.SerializeToUtf8Bytes(cacheKeysInGroup);

            byte[]? cacheGroupCacheSlidingExpirationCache = await _cache
                .GetAsync($"{request.SlidingExpiration}SlidingExpiration");

            int? cacheGroupCacheSlidingExpirationValue = null;


            if(cacheGroupCacheSlidingExpirationCache is not null)
            {
                cacheGroupCacheSlidingExpirationValue = Convert.ToInt32(Encoding.Default.GetString(cacheGroupCacheSlidingExpirationCache));
            }

            if(cacheGroupCacheSlidingExpirationCache is null)
            {
                cacheGroupCacheSlidingExpirationValue =Convert.ToInt32(expiration.TotalSeconds);
            }

            byte[] serializedCachedGroupSlidingExpirationData = JsonSerializer.SerializeToUtf8Bytes(cacheGroupCacheSlidingExpirationValue);


            DistributedCacheEntryOptions cacheEntryOptions = new()
            {
                SlidingExpiration = TimeSpan.FromSeconds(Convert.ToDouble(cacheGroupCacheSlidingExpirationValue))
            };


            await _cache.SetAsync(request.CacheGroupKey, newGroupCache, cacheEntryOptions);

            await _cache.SetAsync($"{request.SlidingExpiration}SlidingExpiration",
                serializedCachedGroupSlidingExpirationData, cacheEntryOptions);
        
    }

}
