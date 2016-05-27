using System.Collections.Generic;

namespace DevelopersSite.Models
{
    public class DocumentVersionModel
    {
        public string Path { get; set; }

        public string Version { get; set; }

        public List<string> ContentFilenames { get; set; }

        public string StartFilename { get; set; }

        public string TocFilename { get; set; }

        public string StaticBase { get; set; }
    }
}
