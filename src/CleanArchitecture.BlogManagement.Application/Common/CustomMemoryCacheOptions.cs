using Microsoft.Extensions.Caching.Memory;

namespace CleanArchitecture.BlogManagement.Application.Common;
internal static class CustomMemoryCacheOptions
{
    internal static ICacheEntry SetAllMemoryCacheOptions(this ICacheEntry cacheEntry)
    {
        return cacheEntry
            .SetSlidingExpiration(TimeSpan.FromHours(2))
            .SetAbsoluteExpiration(TimeSpan.FromHours(24))
            .SetPriority(CacheItemPriority.Normal)
            .SetSize(1024);
    }
}
