
using System.Linq;
using System.Linq.Dynamic;
using System.Web;
using System.Web.Routing;
using Bsc.Dmtds.Core.Mvc;
using Bsc.Dmtds.Core.Mvc.Routing;

namespace Bsc.Dmtds.Web
{
    public static class SortByExtension
    {
        public static IQueryable<T> SortBy<T>(this IQueryable<T> list, string sortField, string sortDir)
        {
            if (!string.IsNullOrEmpty(sortField))
            {
                var sort = sortField;
                if (sortDir == "desc")
                {
                    sort = sort + " descending";
                }
                list = list.OrderBy(sort);
            }
//            else if (typeof(IChangeTimeline).IsAssignableFrom(typeof(T)))
//            {
//                list = list.OrderBy("UtcCreationDate descending");
//            }
            return list;
        }


        #region 表格头
        public static IHtmlString RenderSortHeaderClass(RequestContext requestContext, string propertyName, int propertyOrder)
        {
            if (IsSortField(requestContext, propertyName, propertyOrder))
            {
                var sortDir = requestContext.GetRequestValue("sortDir") ?? "asc";

                return new HtmlString(string.Format("sort {0}", sortDir));
            }

            return new HtmlString("sort");
        }

        private static bool IsSortField(RequestContext requestContext, string propertyName, int propertyOrder)
        {
            var sortField = requestContext.GetRequestValue("sortField");

            return sortField != null && sortField.ToLower() == propertyName.ToLower();
        }

        public static IHtmlString RenderGridHeader(RequestContext requestContext, string headerText, string propertyName, int propertyOrder)
        {
            var html = @"<a href=""{0}"">{1}<img class=""icon arrow"" src=""{2}""></a>";
            var sortDir = "asc";
            if (IsSortField(requestContext, propertyName, propertyOrder))
            {
                sortDir = requestContext.GetRequestValue("sortDir") == "asc" ? "desc" : "asc";
            }
            var sortUrl = requestContext.UrlHelper().Action(requestContext.GetRequestValue("action"),
                requestContext.AllRouteValues().Merge("sortField", propertyName).Merge("sortDir", sortDir));
            return new HtmlString(string.Format(html, sortUrl, headerText, requestContext.UrlHelper().Content("~/Images/invis.gif")));
        }
        #endregion 
    }
}