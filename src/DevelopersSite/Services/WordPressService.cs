using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using DevelopersSite.Models;
using DevelopersSite.Models.WordPress;
using Microsoft.Extensions.Caching.Memory;
using Newtonsoft.Json;

namespace DevelopersSite.Services
{
    public class WordPressService
    {
        readonly SiteConfig siteConfig;
        readonly IMemoryCache memoryCache;

        public WordPressService(SiteConfig siteConfig, IMemoryCache memoryCache)
        {
            this.memoryCache = memoryCache;
            this.siteConfig = siteConfig;
        }

        public async Task<IEnumerable<PostModel>> GetPostsByCategory(int category)
        {
            return await SendRequest<IEnumerable<PostModel>>($"{siteConfig.WordPressApiUrl}posts?categories={category}&per_page=10");
        }

        public async Task<PostModel> GetPost(int id)
        {
            return await SendRequest<PostModel>($"{siteConfig.WordPressApiUrl}posts/{id}");
        }

        private async Task<T> SendRequest<T>(string url)
        {
            T cachedValue;
            if (memoryCache.TryGetValue(url, out cachedValue))
            {
                return cachedValue;
            }

            using (var httpClient = new HttpClient())
            {
                var json = string.Empty;
                try
                {
                    json = await httpClient.GetStringAsync(url);
                    cachedValue = JsonConvert.DeserializeObject<T>(json);

                    var opts = new MemoryCacheEntryOptions()
                    {
                        SlidingExpiration = TimeSpan.FromHours(siteConfig.CacheExpirationHours)
                    };

                    memoryCache.Set(url, cachedValue, opts);

                    return cachedValue;
                }
                catch (Exception ex)
                {
                    return cachedValue;
                }
            }
        }
    }
}
