using System.Collections.Generic;

namespace DevelopersSite.Models
{
    public class DocumentModel
    {
        public string Title { get; internal set; }

        public string Product { get; set; }

        public List<string> Versions { get; set; }

        public List<DocumentVersionModel> DocumentVersions { get; } = new List<DocumentVersionModel>();
    }
}
