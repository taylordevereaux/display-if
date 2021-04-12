using Microsoft.AspNetCore.Razor.TagHelpers;
using System;
using System.Collections.Generic;
using System.Text;

namespace TagHelpers.Extensions
{
    public static class TagHelperOutputExtensions
    {
        public static void AddStyle(this TagHelperOutput output, string style)
        {
            string existingStyle = output.Attributes["style"]?.Value?.ToString() ?? "";
            output.Attributes.SetAttribute("style", $"{style}; {existingStyle}");
        }
    }
}
