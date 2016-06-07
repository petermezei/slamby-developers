using System.Collections.Generic;
using DevelopersSite.Models.WordPress;

namespace DevelopersSite.Models
{
    public class NewsViewModel
    {
        public IEnumerable<PostModel> News { get; set; } = new List<PostModel>();
    }
}
