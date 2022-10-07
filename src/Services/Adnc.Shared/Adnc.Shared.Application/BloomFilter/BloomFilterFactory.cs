using Adnc.Infra.Caching.Core.Internal;
using Adnc.Infra.Core.Adnc.Configuration;
using Adnc.Infra.Core.System.Extensions.String;
using Microsoft.Extensions.Options;

namespace Adnc.Shared.Application.BloomFilter;

public sealed class BloomFilterFactory
{
    private readonly IEnumerable<IBloomFilter> _instances;
    private readonly IOptions<RedisConfig> _redisOptions;

    public BloomFilterFactory(
        IEnumerable<IBloomFilter> instances
        , IOptions<RedisConfig> redisOptions)
    {
        _instances = instances;
        _redisOptions = redisOptions;
    }

    public IBloomFilter Create(string name)
    {
        ArgumentCheck.NotNullOrWhiteSpace(name, nameof(name));

        IBloomFilter bloomFilter;
        if (_redisOptions.Value.EnableBloomFilter)
            bloomFilter = _instances.FirstOrDefault(x => x.Name.EqualsIgnoreCase(name));
        else
            bloomFilter = _instances.FirstOrDefault(x => x.Name.EqualsIgnoreCase("null"));

        return bloomFilter;
    }
}