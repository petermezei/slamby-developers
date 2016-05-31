namespace DevelopersSite.Models
{
    public class DocumentViewModel
    {
        public DocumentModel Document { get; set; }

        public string Version { get; set; }

        public string Content { get; set; }

        public string TocContent { get; set; }

        public string CurrentUrl { get; set; }

        public bool IsStartDocument { get; internal set; }
    }
}
