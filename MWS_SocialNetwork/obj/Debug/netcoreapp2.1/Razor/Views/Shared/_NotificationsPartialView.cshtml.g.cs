#pragma checksum "E:\Thesis\website\MWS_SocialNetwork\MWS_SocialNetwork\Views\Shared\_NotificationsPartialView.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "fbedec13d6cac9a715703c4554596713197b3619"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Shared__NotificationsPartialView), @"mvc.1.0.view", @"/Views/Shared/_NotificationsPartialView.cshtml")]
[assembly:global::Microsoft.AspNetCore.Mvc.Razor.Compilation.RazorViewAttribute(@"/Views/Shared/_NotificationsPartialView.cshtml", typeof(AspNetCore.Views_Shared__NotificationsPartialView))]
namespace AspNetCore
{
    #line hidden
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using Microsoft.AspNetCore.Mvc.ViewFeatures;
#line 1 "E:\Thesis\website\MWS_SocialNetwork\MWS_SocialNetwork\Views\_ViewImports.cshtml"
using Microsoft.AspNetCore.Identity;

#line default
#line hidden
#line 2 "E:\Thesis\website\MWS_SocialNetwork\MWS_SocialNetwork\Views\_ViewImports.cshtml"
using MWS_SocialNetwork;

#line default
#line hidden
#line 3 "E:\Thesis\website\MWS_SocialNetwork\MWS_SocialNetwork\Views\_ViewImports.cshtml"
using MWS_SocialNetwork.Models;

#line default
#line hidden
#line 4 "E:\Thesis\website\MWS_SocialNetwork\MWS_SocialNetwork\Views\_ViewImports.cshtml"
using MWS_SocialNetwork.ViewModels;

#line default
#line hidden
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"fbedec13d6cac9a715703c4554596713197b3619", @"/Views/Shared/_NotificationsPartialView.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"7c2f0d980522bfda3f11902d2564349cbe15af9c", @"/Views/_ViewImports.cshtml")]
    public class Views_Shared__NotificationsPartialView : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<IEnumerable<MWS_SocialNetwork.ViewModels.NotificationViewModel>>
    {
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_0 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("asp-action", "Index", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_1 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("asp-controller", "PrivacySettings", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_2 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("asp-controller", "UserProfile", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_3 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("asp-controller", "GroupProfile", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_4 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("asp-controller", "MyPosts", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        #line hidden
        #pragma warning disable 0169
        private string __tagHelperStringValueBuffer;
        #pragma warning restore 0169
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperExecutionContext __tagHelperExecutionContext;
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperRunner __tagHelperRunner = new global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperRunner();
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperScopeManager __backed__tagHelperScopeManager = null;
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperScopeManager __tagHelperScopeManager
        {
            get
            {
                if (__backed__tagHelperScopeManager == null)
                {
                    __backed__tagHelperScopeManager = new global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperScopeManager(StartTagHelperWritingScope, EndTagHelperWritingScope);
                }
                return __backed__tagHelperScopeManager;
            }
        }
        private global::Microsoft.AspNetCore.Mvc.TagHelpers.AnchorTagHelper __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper;
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
            BeginContext(72, 262, true);
            WriteLiteral(@"
<li class=""dropdown notifications-menu"" style=""width:200px;"">
    <a href=""#"" class=""dropdown-toggle"" data-toggle=""dropdown"">
        <i class=""fa fa-bell-o""></i>
        <span class=""label label-warning"">3</span>
    </a>
    <ul class=""dropdown-menu"">
");
            EndContext();
#line 9 "E:\Thesis\website\MWS_SocialNetwork\MWS_SocialNetwork\Views\Shared\_NotificationsPartialView.cshtml"
         if (Model != null)
        {

#line default
#line hidden
            BeginContext(374, 40, true);
            WriteLiteral("            <li class=\"header\">You have ");
            EndContext();
            BeginContext(415, 35, false);
#line 11 "E:\Thesis\website\MWS_SocialNetwork\MWS_SocialNetwork\Views\Shared\_NotificationsPartialView.cshtml"
                                   Write(Model.Where(x => !x.IsSeen).Count());

#line default
#line hidden
            EndContext();
            BeginContext(450, 21, true);
            WriteLiteral(" notifications</li>\r\n");
            EndContext();
#line 12 "E:\Thesis\website\MWS_SocialNetwork\MWS_SocialNetwork\Views\Shared\_NotificationsPartialView.cshtml"
        }
        else
        {

#line default
#line hidden
            BeginContext(507, 67, true);
            WriteLiteral("            <li class=\"header\">You have no notifications yet</li>\r\n");
            EndContext();
#line 16 "E:\Thesis\website\MWS_SocialNetwork\MWS_SocialNetwork\Views\Shared\_NotificationsPartialView.cshtml"
        }

#line default
#line hidden
            BeginContext(585, 106, true);
            WriteLiteral("\r\n        <li>\r\n            <!-- inner menu: contains the actual data -->\r\n            <ul class=\"menu\">\r\n");
            EndContext();
#line 21 "E:\Thesis\website\MWS_SocialNetwork\MWS_SocialNetwork\Views\Shared\_NotificationsPartialView.cshtml"
                 if (Model.Take(10) != null)
                {
                    foreach (var item in Model.Take(10))
                    {

#line default
#line hidden
            BeginContext(837, 38, true);
            WriteLiteral("                <li class=\"text-sm\">\r\n");
            EndContext();
#line 26 "E:\Thesis\website\MWS_SocialNetwork\MWS_SocialNetwork\Views\Shared\_NotificationsPartialView.cshtml"
                     switch (item.Code)
                    {
                        case "AddRelationship":
                            {

#line default
#line hidden
            BeginContext(1019, 131, true);
            WriteLiteral("                                <a href=\"#\">\r\n                                    <i class=\"fa fa-wrench text-aqua\"></i> We added \'");
            EndContext();
            BeginContext(1151, 14, false);
#line 31 "E:\Thesis\website\MWS_SocialNetwork\MWS_SocialNetwork\Views\Shared\_NotificationsPartialView.cshtml"
                                                                                Write(item.Parameter);

#line default
#line hidden
            EndContext();
            BeginContext(1165, 61, true);
            WriteLiteral("\' as new relationship\r\n                                </a>\r\n");
            EndContext();
#line 33 "E:\Thesis\website\MWS_SocialNetwork\MWS_SocialNetwork\Views\Shared\_NotificationsPartialView.cshtml"
                                break;
                            }
                        case "ChangeConflictPolicy":
                            {

#line default
#line hidden
            BeginContext(1382, 32, true);
            WriteLiteral("                                ");
            EndContext();
            BeginContext(1414, 226, false);
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("a", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "eb71e8b063c5404696fa5f08f64816d0", async() => {
                BeginContext(1469, 87, true);
                WriteLiteral("\r\n                                    <i class=\"fa fa-wrench text-aqua\"></i> We added \'");
                EndContext();
                BeginContext(1557, 14, false);
#line 38 "E:\Thesis\website\MWS_SocialNetwork\MWS_SocialNetwork\Views\Shared\_NotificationsPartialView.cshtml"
                                                                                Write(item.Parameter);

#line default
#line hidden
                EndContext();
                BeginContext(1571, 65, true);
                WriteLiteral("\' as Conflict resolution Policy\r\n                                ");
                EndContext();
            }
            );
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.AnchorTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper);
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.Action = (string)__tagHelperAttribute_0.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_0);
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.Controller = (string)__tagHelperAttribute_1.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_1);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            EndContext();
            BeginContext(1640, 2, true);
            WriteLiteral("\r\n");
            EndContext();
#line 40 "E:\Thesis\website\MWS_SocialNetwork\MWS_SocialNetwork\Views\Shared\_NotificationsPartialView.cshtml"
                                break;
                            }
                        case "ChangeRelationship":
                            {

#line default
#line hidden
            BeginContext(1796, 32, true);
            WriteLiteral("                                ");
            EndContext();
            BeginContext(1828, 254, false);
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("a", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "711526735c254504bc2eb129d7439210", async() => {
                BeginContext(1912, 75, true);
                WriteLiteral("\r\n                                    <i class=\"fa fa-user text-aqua\"></i> ");
                EndContext();
                BeginContext(1988, 21, false);
#line 45 "E:\Thesis\website\MWS_SocialNetwork\MWS_SocialNetwork\Views\Shared\_NotificationsPartialView.cshtml"
                                                                    Write(item.FromUserFullName);

#line default
#line hidden
                EndContext();
                BeginContext(2009, 20, true);
                WriteLiteral(" has defined you as ");
                EndContext();
                BeginContext(2030, 14, false);
#line 45 "E:\Thesis\website\MWS_SocialNetwork\MWS_SocialNetwork\Views\Shared\_NotificationsPartialView.cshtml"
                                                                                                              Write(item.Parameter);

#line default
#line hidden
                EndContext();
                BeginContext(2044, 34, true);
                WriteLiteral("\r\n                                ");
                EndContext();
            }
            );
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.AnchorTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper);
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.Action = (string)__tagHelperAttribute_0.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_0);
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.Controller = (string)__tagHelperAttribute_2.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_2);
            if (__Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.RouteValues == null)
            {
                throw new InvalidOperationException(InvalidTagHelperIndexerAssignment("asp-route-cId", "Microsoft.AspNetCore.Mvc.TagHelpers.AnchorTagHelper", "RouteValues"));
            }
            BeginWriteTagHelperAttribute();
#line 44 "E:\Thesis\website\MWS_SocialNetwork\MWS_SocialNetwork\Views\Shared\_NotificationsPartialView.cshtml"
                                                                                      WriteLiteral(item.FromUserId);

#line default
#line hidden
            __tagHelperStringValueBuffer = EndWriteTagHelperAttribute();
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.RouteValues["cId"] = __tagHelperStringValueBuffer;
            __tagHelperExecutionContext.AddTagHelperAttribute("asp-route-cId", __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.RouteValues["cId"], global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            EndContext();
            BeginContext(2082, 2, true);
            WriteLiteral("\r\n");
            EndContext();
#line 47 "E:\Thesis\website\MWS_SocialNetwork\MWS_SocialNetwork\Views\Shared\_NotificationsPartialView.cshtml"
                                break;
                            }
                        case "GroupInvitation":
                            {

#line default
#line hidden
            BeginContext(2235, 32, true);
            WriteLiteral("                                ");
            EndContext();
            BeginContext(2267, 253, false);
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("a", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "730dd4ad6651448aa6c0adbbf7d49e17", async() => {
                BeginContext(2351, 76, true);
                WriteLiteral("\r\n                                    <i class=\"fa fa-users text-aqua\"></i> ");
                EndContext();
                BeginContext(2428, 21, false);
#line 52 "E:\Thesis\website\MWS_SocialNetwork\MWS_SocialNetwork\Views\Shared\_NotificationsPartialView.cshtml"
                                                                     Write(item.FromUserFullName);

#line default
#line hidden
                EndContext();
                BeginContext(2449, 67, true);
                WriteLiteral(" has invited you to his new group\r\n                                ");
                EndContext();
            }
            );
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.AnchorTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper);
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.Action = (string)__tagHelperAttribute_0.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_0);
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.Controller = (string)__tagHelperAttribute_3.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_3);
            if (__Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.RouteValues == null)
            {
                throw new InvalidOperationException(InvalidTagHelperIndexerAssignment("asp-route-gId", "Microsoft.AspNetCore.Mvc.TagHelpers.AnchorTagHelper", "RouteValues"));
            }
            BeginWriteTagHelperAttribute();
#line 51 "E:\Thesis\website\MWS_SocialNetwork\MWS_SocialNetwork\Views\Shared\_NotificationsPartialView.cshtml"
                                                                                       WriteLiteral(item.Parameter);

#line default
#line hidden
            __tagHelperStringValueBuffer = EndWriteTagHelperAttribute();
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.RouteValues["gId"] = __tagHelperStringValueBuffer;
            __tagHelperExecutionContext.AddTagHelperAttribute("asp-route-gId", __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.RouteValues["gId"], global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            EndContext();
            BeginContext(2520, 2, true);
            WriteLiteral("\r\n");
            EndContext();
#line 54 "E:\Thesis\website\MWS_SocialNetwork\MWS_SocialNetwork\Views\Shared\_NotificationsPartialView.cshtml"
                                break;
                            }
                        case "Tag":
                            {

#line default
#line hidden
            BeginContext(2661, 32, true);
            WriteLiteral("                                ");
            EndContext();
            BeginContext(2693, 238, false);
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("a", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "9ad8e2edea4f408abeee0bcb8d7fce94", async() => {
                BeginContext(2772, 74, true);
                WriteLiteral("\r\n                                    <i class=\"fa fa-tag text-aqua\"></i> ");
                EndContext();
                BeginContext(2847, 21, false);
#line 59 "E:\Thesis\website\MWS_SocialNetwork\MWS_SocialNetwork\Views\Shared\_NotificationsPartialView.cshtml"
                                                                   Write(item.FromUserFullName);

#line default
#line hidden
                EndContext();
                BeginContext(2868, 59, true);
                WriteLiteral(" Tagged you to a new Post\r\n                                ");
                EndContext();
            }
            );
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.AnchorTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper);
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.Action = (string)__tagHelperAttribute_0.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_0);
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.Controller = (string)__tagHelperAttribute_4.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_4);
            if (__Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.RouteValues == null)
            {
                throw new InvalidOperationException(InvalidTagHelperIndexerAssignment("asp-route-gId", "Microsoft.AspNetCore.Mvc.TagHelpers.AnchorTagHelper", "RouteValues"));
            }
            BeginWriteTagHelperAttribute();
#line 58 "E:\Thesis\website\MWS_SocialNetwork\MWS_SocialNetwork\Views\Shared\_NotificationsPartialView.cshtml"
                                                                                  WriteLiteral(item.Parameter);

#line default
#line hidden
            __tagHelperStringValueBuffer = EndWriteTagHelperAttribute();
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.RouteValues["gId"] = __tagHelperStringValueBuffer;
            __tagHelperExecutionContext.AddTagHelperAttribute("asp-route-gId", __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.RouteValues["gId"], global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            EndContext();
            BeginContext(2931, 2, true);
            WriteLiteral("\r\n");
            EndContext();
#line 61 "E:\Thesis\website\MWS_SocialNetwork\MWS_SocialNetwork\Views\Shared\_NotificationsPartialView.cshtml"
                                break;
                            }
                        case "Contribute":
                            {

#line default
#line hidden
            BeginContext(3079, 32, true);
            WriteLiteral("                                ");
            EndContext();
            BeginContext(3111, 252, false);
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("a", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "f596958bae054e4eb164fdcbc34984a8", async() => {
                BeginContext(3190, 86, true);
                WriteLiteral("\r\n                                    <i class=\"fa fa-pencil-square-o text-aqua\"></i> ");
                EndContext();
                BeginContext(3277, 21, false);
#line 66 "E:\Thesis\website\MWS_SocialNetwork\MWS_SocialNetwork\Views\Shared\_NotificationsPartialView.cshtml"
                                                                               Write(item.FromUserFullName);

#line default
#line hidden
                EndContext();
                BeginContext(3298, 61, true);
                WriteLiteral(" added a post to your space\r\n                                ");
                EndContext();
            }
            );
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.AnchorTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper);
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.Action = (string)__tagHelperAttribute_0.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_0);
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.Controller = (string)__tagHelperAttribute_4.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_4);
            if (__Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.RouteValues == null)
            {
                throw new InvalidOperationException(InvalidTagHelperIndexerAssignment("asp-route-gId", "Microsoft.AspNetCore.Mvc.TagHelpers.AnchorTagHelper", "RouteValues"));
            }
            BeginWriteTagHelperAttribute();
#line 65 "E:\Thesis\website\MWS_SocialNetwork\MWS_SocialNetwork\Views\Shared\_NotificationsPartialView.cshtml"
                                                                                  WriteLiteral(item.Parameter);

#line default
#line hidden
            __tagHelperStringValueBuffer = EndWriteTagHelperAttribute();
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.RouteValues["gId"] = __tagHelperStringValueBuffer;
            __tagHelperExecutionContext.AddTagHelperAttribute("asp-route-gId", __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.RouteValues["gId"], global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            EndContext();
            BeginContext(3363, 2, true);
            WriteLiteral("\r\n");
            EndContext();
#line 68 "E:\Thesis\website\MWS_SocialNetwork\MWS_SocialNetwork\Views\Shared\_NotificationsPartialView.cshtml"
                                break;
                            }
                    }

#line default
#line hidden
            BeginContext(3459, 23, true);
            WriteLiteral("                </li>\r\n");
            EndContext();
#line 72 "E:\Thesis\website\MWS_SocialNetwork\MWS_SocialNetwork\Views\Shared\_NotificationsPartialView.cshtml"
                    }
                        }

#line default
#line hidden
            BeginContext(3532, 58, true);
            WriteLiteral("                <li class=\"text-sm\">\r\n                    ");
            EndContext();
            BeginContext(3590, 164, false);
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("a", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "61841d6d026d43499ed9bb53383542fd", async() => {
                BeginContext(3637, 113, true);
                WriteLiteral("\r\n                        <i class=\"fa fa-tag text-aqua\"></i> John Doe Tagged you in a post\r\n                    ");
                EndContext();
            }
            );
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.AnchorTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper);
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.Action = (string)__tagHelperAttribute_0.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_0);
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.Controller = (string)__tagHelperAttribute_4.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_4);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            EndContext();
            BeginContext(3754, 79, true);
            WriteLiteral("\r\n                </li>\r\n\r\n            </ul>\r\n        </li>\r\n    </ul>\r\n</li>\r\n");
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
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<IEnumerable<MWS_SocialNetwork.ViewModels.NotificationViewModel>> Html { get; private set; }
    }
}
#pragma warning restore 1591
