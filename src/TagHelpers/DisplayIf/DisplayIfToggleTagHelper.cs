using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace TagHelpers.DisplayIf
{
    // You may need to install the Microsoft.AspNetCore.Razor.Runtime package into your project
    [HtmlTargetElement(Attributes = "display-if-toggle")]
    public class DisplayIfToggleTagHelper : TagHelper
    {
        public ModelExpression DisplayIfToggle { get; set; }

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            output.Attributes.Add("data-display-if-toggle", this.DisplayIfToggle.Name);
        }
    }
}
