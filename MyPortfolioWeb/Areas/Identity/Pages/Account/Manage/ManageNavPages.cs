using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace MyPortfolioWeb.Areas.Identity.Pages.Account.Manage
{
    public static class ManageNavPages
    {
        public static string Index => "Index";

        public static string Email => "Email";

        public static string ChangePassword => "ChangePassword";

        public static string WelcomePage => "WelcomePage";
        public static string Skills => "Skills";
        public static string Experiences => "Experiences";
        public static string Testimonials => "Testimonials";
        public static string Awards => "Awards";
        public static string Portfolios => "Portfolios";
        public static string IndexNavClass(ViewContext viewContext) => PageNavClass(viewContext, Index);

        public static string EmailNavClass(ViewContext viewContext) => PageNavClass(viewContext, Email);

        public static string ChangePasswordNavClass(ViewContext viewContext) => PageNavClass(viewContext, ChangePassword);
        public static string WelcomePageNavClass(ViewContext viewContext) => PageNavClass(viewContext, WelcomePage);
        public static string SkillsNavClass(ViewContext viewContext) => PageNavClass(viewContext, Skills);
        public static string ExperiencesClass(ViewContext viewContext) => PageNavClass(viewContext, Experiences);
        public static string TestimonialsClass(ViewContext viewContext) => PageNavClass(viewContext, Testimonials);
        public static string AwardsClass(ViewContext viewContext) => PageNavClass(viewContext, Awards);
        public static string PortfoliosClass(ViewContext viewContext) => PageNavClass(viewContext, Portfolios);
        private static string PageNavClass(ViewContext viewContext, string page)
        {
            var activePage = viewContext.ViewData["ActivePage"] as string
                ?? System.IO.Path.GetFileNameWithoutExtension(viewContext.ActionDescriptor.DisplayName);
            return string.Equals(activePage, page, StringComparison.OrdinalIgnoreCase) ? "active" : null;
        }
    }
}
