using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Caching;
using System.Text;
using System.Threading.Tasks;
using OnlineStore.Core.Common.Contracts;

namespace OnlineStore.Core.Common.Caching
{
    public class CacheProvider:ICacheProvider
    {
        private readonly MemoryCache _cache = MemoryCache.Default;

        public void Set(string key, object value, int expireAsMinute)
        {
            System.Diagnostics.Contracts.Contract.Assert(!string.IsNullOrEmpty(key)); //key değerinin null veya boş olmadığından emin ol, yoksa exception fırlat
            System.Diagnostics.Contracts.Contract.Assert(value != null);  //value değerinin null olmadığından emin ol, yoksa exception fırlat
            System.Diagnostics.Contracts.Contract.Assert(expireAsMinute > 0); //expireAsMinute değerinin 0'dan büyük olduğuna emin ol, yoksa exception fırlat
            if (IsExist(key))
            {
                Remove(key);
            }

            _cache.Add(key, value, DateTimeOffset.Now.AddMinutes(expireAsMinute));
        }

        public T Get<T>(string key)
        {
            System.Diagnostics.Contracts.Contract.Assert(!string.IsNullOrEmpty(key)); //key değerinin null veya boş olmadığından emin ol, yoksa exception fırlat
            return (T) _cache.Get(key);
        }

        public void Remove(string key)
        {
            System.Diagnostics.Contracts.Contract.Assert(!string.IsNullOrEmpty(key)); //key değerinin null veya boş olmadığından emin ol, yoksa exception fırlat
            _cache.Remove(key);
        }

        public bool IsExist(string key)
        {
            System.Diagnostics.Contracts.Contract.Assert(!string.IsNullOrEmpty(key)); //key değerinin null veya boş olmadığından emin ol, yoksa exception fırlat
            return _cache.Any(i => i.Key == key);
        }
    }
}
