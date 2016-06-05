using System;
using System.Collections.Generic;

namespace DevelopersSite.Models
{
    public class RssModel
    {
        public Channel channel { get; set; } = new Channel();
    }

    public class Channel
    {
        public string title { get; set; }
        public string link { get; set; }
        public string description { get; set; }
        public string language { get; set; }
        public string copyright { get; set; }
        public List<Item> items { get; set; } = new List<Item>();
    }

    public class Item
    {
        public string description { get; set; }
        public DateTime pubDate { get; set; }
        public string guid { get; set; }
        public string title { get; set; }
    }
}
