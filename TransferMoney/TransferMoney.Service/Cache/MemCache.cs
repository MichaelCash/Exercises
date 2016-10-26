using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Caching;
using System.Text;
using System.Threading.Tasks;

namespace TransferMoney.Service.Cache
{
    public class MemCache
    {
        private static object lockCache = new object(); 
        public static T GetOrSet<T>(string cacheKey, int seconds, Func<T> getItemCallback) where T : class
        {
            lock (lockCache)
            {
                T item = MemoryCache.Default.Get(cacheKey) as T;
                if (item == null)
                {
                    item = getItemCallback();
                    MemoryCache.Default.Add(cacheKey, item, DateTime.Now.AddSeconds(seconds));
                }
                return item;
            }
        }

        public static bool RemoveCache(string key)
        {
            try
            {
                MemoryCache.Default.Remove(key);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public static bool RemoveAllCache()
        {
            try
            {
                MemoryCache.Default.Dispose();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

    }
}
