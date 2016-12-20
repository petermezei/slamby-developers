using System.Collections.Generic;

namespace DevelopersSite.Models
{
    public class SiteConfig
    {
        public Dictionary<string, string> Products { get; set; }

        public string WordPressApiUrl { get; set; }

        public string CdnUrl { get; set; }

        public string Version { get; set; }

        private double _cacheExpirationHours;

        public double CacheExpirationHours
        {
            get
            {
                return _cacheExpirationHours;
            }
            set
            {
                _cacheExpirationHours = value < 0 ? 0 : value;
            }
        }

        public string ApiBaseEndpoint { get; set; }

        public string ApiSecret { get; set; }
    }
}
