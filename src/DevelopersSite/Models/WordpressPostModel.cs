using System;
using System.Collections.Generic;

namespace DevelopersSite.Models
{
    public class WordpressPostModel
    {
        public int id { get; set; }
        public Title title { get; set; }
        public Content content { get; set; }
        public DateTime date { get; set; }
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
