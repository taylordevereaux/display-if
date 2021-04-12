using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.TagHelpers;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Razor.Runtime.TagHelpers;
using Microsoft.AspNetCore.Razor.TagHelpers;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace TagHelpers.DisplayIf
{
    // You may need to install the Microsoft.AspNetCore.Razor.Runtime package into your project
    [HtmlTargetElement(Attributes = "display-if")]
    public class DisplayIfTagHelper : TagHelper
    {
        public ModelExpression DisplayIf { get; set; }

        public string DisplayIfName { get; set; }

        public object DisplayIfValue { get; set; }

        public object DisplayIfNotValue { get; set; }

        public bool DisplayIfHasValue { get; set; }

        public string DisplayIfClientEvaluate { get; set; }

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            bool displayIfValueDoesNotMatch = !ValueMatches() && !this.DisplayIfHasValue && this.DisplayIfNotValue == null;
            bool displayIfNotValueMatches = ValueNotMatches();
            bool displayIfDoesNotHaveValue = this.DisplayIfHasValue && String.IsNullOrWhiteSpace(this.DisplayIf.Model?.ToString());

            if (displayIfValueDoesNotMatch || displayIfNotValueMatches || displayIfDoesNotHaveValue)
            {
                output.AddStyle("display: none;");
            }

            if (DisplayIfValue is bool)
            {
                DisplayIfValue = ((bool)DisplayIfValue).ToString().ToLower();
            }

            if (DisplayIfNotValue is bool)
            {
                DisplayIfNotValue = ((bool)DisplayIfNotValue).ToString().ToLower();
            }

            output.Attributes.Add("data-display-if-name", this.DisplayIf == null ? this.DisplayIfName : this.DisplayIf.Name);
            output.Attributes.Add("data-display-if-value", this.DisplayIfValue);

            if (this.DisplayIfNotValue != null)
                output.Attributes.Add("data-display-if-not-value", this.DisplayIfNotValue);

            output.Attributes.Add("data-display-if-client-evaluate", this.DisplayIfClientEvaluate);
            output.Attributes.Add("data-display-if-has-value", this.DisplayIfHasValue.ToString().ToLower());
        }

        private bool ValueMatches()
        {
            return this.DisplayIf.Model?.Equals(DisplayIfValue) == true
                || this.DisplayIf.Model is bool && (bool)this.DisplayIf.Model == (bool?)DisplayIfValue
                || this.DisplayIf.Model == null && this.DisplayIfValue == null;
        }

        private bool ValueNotMatches()
        {
            return this.DisplayIf.Model?.Equals(DisplayIfNotValue) == true
                || this.DisplayIf.Model is bool && (bool)this.DisplayIf.Model == (bool?)DisplayIfNotValue
                || this.DisplayIf.Model == null && this.DisplayIfNotValue == null;
        }

    }
}
