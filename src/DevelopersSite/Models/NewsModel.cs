using System.Collections.Generic;

namespace DevelopersSite.Models
{
    public class NewsModel
    {
        public int id { get; set; }
        public Title title { get; set; }
        public Content content { get; set; }
        public string date { get; set; }
    }

    public class Title
    {
        public string rendered { get; set; }
    }

    public class Content
    {
        public string rendered { get; set; }
    }
}
