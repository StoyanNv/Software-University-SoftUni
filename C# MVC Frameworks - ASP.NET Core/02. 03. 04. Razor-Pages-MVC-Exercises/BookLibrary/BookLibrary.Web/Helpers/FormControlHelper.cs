namespace BookLibrary.Web.Helpers
{
    using Microsoft.AspNetCore.Mvc.ViewFeatures;
    using Microsoft.AspNetCore.Razor.TagHelpers;
    using System.Text;

    [HtmlTargetElement("form-control")]
    public class FormControlHelper : TagHelper
    {
        [HtmlAttributeName("asp-for")]
        public ModelExpression For { get; set; }

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            var result = new StringBuilder();
            result.AppendLine("<div class=\"form-group row\">");

            result.AppendLine("</div>");
            output.Content.SetHtmlContent(result.ToString());
        }
    }
}
