using Microsoft.AspNetCore.Mvc.Rendering;
using Sales.AtomicSeller.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Sales.AtomicSeller.Extensions
{
    public static class HtmlExtensions
    {
        /// <summary>
        /// Is Link active.
        /// </summary>
        /// <param name="htmlHelper"></param>
        /// <param name="controller"></param>
        /// <param name="action"></param>
        /// <returns></returns>
        public static string IsActive(this IHtmlHelper htmlHelper, string controller, string action)
        {
            var routeData = htmlHelper.ViewContext.RouteData;

            var routeAction = routeData.Values["action"].ToString();
            var routeController = routeData.Values["controller"].ToString();

            var returnActive = (controller == routeController && (action == routeAction || routeAction == "Details"));

            return returnActive ? "active" : "";
        }
        /// <summary>
        /// Is link active with Area.
        /// </summary>
        /// <param name="htmlHelper"></param>
        /// <param name="area"></param>
        /// <param name="controller"></param>
        /// <param name="action"></param>
        /// <returns></returns>
        public static string IsActive(this IHtmlHelper htmlHelper, string area, string controller, string action)
        {
            var routeData = htmlHelper.ViewContext.RouteData;

            var routeAction = routeData.Values["action"].ToString();
            var routeController = routeData.Values["controller"].ToString();
            var routeArea = routeData.Values["area"].ToString();

            var returnActive = (area == routeArea && controller == routeController && (action == routeAction || routeAction == "Details"));

            return returnActive ? "active" : "";
        }

    }

    public static class ClaimsPrincipalExtensions
    {
        /// <summary>
        /// Get fullname from ClaimsPrincipal.
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public static string GetFullName(this ClaimsPrincipal user)
        {
            string FullName = "";

            if (user == null)
            {
                return FullName;
            }
            try
            {
                var _ClaimFirst = user.Claims.FirstOrDefault(c => c.Type == "FirstName");
                var _ClaimLast = user.Claims.FirstOrDefault(c => c.Type == "LastName");
                var _ClaimName = user.Claims.FirstOrDefault(c => c.Type == "Name");

                if (_ClaimFirst != null && _ClaimLast!=null)
                {
                    string FirstName = _ClaimFirst.Value;
                    string LastName = _ClaimLast.Value;
                    FullName = $"{FirstName} {LastName}";
                }
                else if(_ClaimName != null)
                {
                    FullName = _ClaimName.Value;
                }

            }
            catch
            {
            }

            return FullName;
        }
    }
}
