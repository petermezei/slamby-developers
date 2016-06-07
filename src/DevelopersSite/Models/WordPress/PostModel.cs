using System;

namespace DevelopersSite.Models.WordPress
{
    public class PostModel
    {
        public int Id { get; set; }
        public PostTitleModel Title { get; set; }
        public PostContentModel Content { get; set; }
        public DateTime Date { get; set; }
        public string Slug { get; set; }
    }
}
