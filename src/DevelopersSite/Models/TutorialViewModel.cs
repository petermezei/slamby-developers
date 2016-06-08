using System.Collections.Generic;
using DevelopersSite.Models.WordPress;

namespace DevelopersSite.Models
{
    public class TutorialViewModel
    {
        public IEnumerable<PostModel> Tutorials { get; set; } = new List<PostModel>();

        public PostModel Post { get; set; }
    }
}
