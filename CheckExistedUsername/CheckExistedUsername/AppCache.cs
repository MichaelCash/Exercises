using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Caching;
using System.Text;
using System.Threading.Tasks;

namespace CheckExistedUsername
{

    public class AppCache 
    {
        public static T GetOrSet<T>(string cacheKey, Func<T> getItemCallback, int seconds = int.MaxValue) where T : class
        {
            T item = MemoryCache.Default.Get(cacheKey) as T;
            if (item == null)
            {
                item = getItemCallback();
                if (item != null)
                {
                    MemoryCache.Default.Add(cacheKey, item, DateTime.Now.AddSeconds(seconds));
                }
            }
            return item;
        }
    }
}
