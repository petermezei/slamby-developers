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

        public string DateFormatted
        {
            get
            {
                return Date.ToString("dd. MMMM, hh:mm", System.Globalization.CultureInfo.InvariantCulture);
            }
        }
    }
}
