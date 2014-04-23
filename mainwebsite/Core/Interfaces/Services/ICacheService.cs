using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MaroonVillage.Core.Interfaces.Services
{
    public interface ICacheService
	{
        object this[string key] { get; set; }
        object Get(string key);
        void Add(string key, object data, int cacheTime);
        void Add(string key, object data, int cacheMinutes, object updateCallback);
        bool Contains(string key);
        object Remove(string key);
        object CacheItemPolicy(int cacheMinutes, object updateCallback);
    }
}
