#pragma checksum "C:\Users\Asus\Documents\Visual Studio 2017\Projects\BookLibrary\BookLibrary.Web\Pages\Search.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "90e93543123ba80dc10abcfc9c436c05316feb8e"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(BookLibrary.Web.Pages.Pages_Search), @"mvc.1.0.razor-page", @"/Pages/Search.cshtml")]
[assembly:global::Microsoft.AspNetCore.Mvc.RazorPages.Infrastructure.RazorPageAttribute(@"/Pages/Search.cshtml", typeof(BookLibrary.Web.Pages.Pages_Search), null)]
namespace BookLibrary.Web.Pages
{
    #line hidden
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using Microsoft.AspNetCore.Mvc.ViewFeatures;
#line 1 "C:\Users\Asus\Documents\Visual Studio 2017\Projects\BookLibrary\BookLibrary.Web\Pages\_ViewImports.cshtml"
using BookLibrary.Web;

#line default
#line hidden
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"90e93543123ba80dc10abcfc9c436c05316feb8e", @"/Pages/Search.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"261f629b4d3e449c3ea86490da012e8d3a1f729d", @"/Pages/_ViewImports.cshtml")]
    public class Pages_Search : global::Microsoft.AspNetCore.Mvc.RazorPages.Page
    {
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
#line 3 "C:\Users\Asus\Documents\Visual Studio 2017\Projects\BookLibrary\BookLibrary.Web\Pages\Search.cshtml"
  
    ViewData["Title"] = "Search";

#line default
#line hidden
            BeginContext(91, 23, true);
            WriteLiteral("\r\n<h2>Search results: \"");
            EndContext();
            BeginContext(115, 16, false);
#line 7 "C:\Users\Asus\Documents\Visual Studio 2017\Projects\BookLibrary\BookLibrary.Web\Pages\Search.cshtml"
                Write(Model.SearchTerm);

#line default
#line hidden
            EndContext();
            BeginContext(131, 18, true);
            WriteLiteral("\"</h2>\r\n<ul>\r\n    ");
            EndContext();
            BeginContext(150, 45, false);
#line 9 "C:\Users\Asus\Documents\Visual Studio 2017\Projects\BookLibrary\BookLibrary.Web\Pages\Search.cshtml"
Write(Html.DisplayFor(model => model.SearchResults));

#line default
#line hidden
            EndContext();
            BeginContext(195, 7, true);
            WriteLiteral("\r\n</ul>");
            EndContext();
        }
        #pragma warning restore 1998
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.ViewFeatures.IModelExpressionProvider ModelExpressionProvider { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.IUrlHelper Url { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.IViewComponentHelper Component { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.Rendering.IJsonHelper Json { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<BookLibrary.Web.Pages.SearchModel> Html { get; private set; }
        public global::Microsoft.AspNetCore.Mvc.ViewFeatures.ViewDataDictionary<BookLibrary.Web.Pages.SearchModel> ViewData => (global::Microsoft.AspNetCore.Mvc.ViewFeatures.ViewDataDictionary<BookLibrary.Web.Pages.SearchModel>)PageContext?.ViewData;
        public BookLibrary.Web.Pages.SearchModel Model => ViewData.Model;
    }
}
#pragma warning restore 1591
