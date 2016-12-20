using DevelopersSite.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Razor.TagHelpers;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DevelopersSite.Helpers
{
    [HtmlTargetElement(Attributes = CdnAttributeName)]
    public class CdnTagHelper : TagHelper
    {
        private const string CdnAttributeName = "use-cdn";
        private string _cdnUrl;
        public CdnTagHelper(IHostingEnvironment env, IOptions<SiteConfig> options)
        {
            _cdnUrl = options.Value.CdnUrl;
        }

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            if (string.IsNullOrEmpty(_cdnUrl)) return;

            string attr;
            if (output.Attributes.ContainsName("src")) attr = "src";
            else if (output.Attributes.ContainsName("href")) attr = "href";
            else return;

            var outputSrc = output.Attributes[attr];
            output.Attributes.Remove(outputSrc);
            output.Attributes.Add(attr, $"{_cdnUrl}{outputSrc.Value}");
            output.Attributes.Add("onerror", $"this.onerror=null;this.{attr}='{outputSrc.Value}'");
        }
    }
}
