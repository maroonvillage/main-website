using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Caching;
using System.Text;
using MaroonVillage.Core.Interfaces.Services;

namespace MaroonVillage.Core.Services
{
    public class CacheService : ICacheService
    {
        private static ObjectCache Cache { get { return MemoryCache.Default; } }
        private const int DefaultCacheMinutes = 5;

        public object this[string key]
        {
            get { return Get(key); }
            set
            {
                if (value == null)
                    Remove(key);
                else
                    Add(key, value, DefaultCacheMinutes);
            }
        }

        public object Get(string key)
        {
            return Cache[key];
        }

        public void Add(string key, object data, int cacheTime)
        {
            var policy = new CacheItemPolicy { AbsoluteExpiration = DateTime.Now + TimeSpan.FromMinutes(cacheTime) };
            Cache.Add(new CacheItem(key, data), policy);
        }

        public void Add(string key, object data, int cacheMinutes, object updateCallback)
        {
            Cache.Set(new CacheItem(key, data), CacheItemPolicy(cacheMinutes, updateCallback as CacheEntryUpdateCallback) as CacheItemPolicy);
        }

        public bool Contains(string key)
        {
            return (Cache[key] != null);
        }

        public object Remove(string key)
        {
            return Cache.Remove(key);
        }

        public object CacheItemPolicy(int cacheMinutes, object updateCallback)
        {
            return new CacheItemPolicy
            {
                UpdateCallback = updateCallback as CacheEntryUpdateCallback,
                AbsoluteExpiration = DateTime.Now + TimeSpan.FromMinutes(cacheMinutes)
            };
        }
    }
}
