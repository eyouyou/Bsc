using System.Web;
using Bsc.Dmtds.Sites.Models;
using Bsc.Dmtds.Sites.View;

namespace Bsc.Dmtds.Sites.Membership
{
    public static class MembershipExtensionMethods
    {
        public static IMembershipAuthentication Membership(this HttpContextBase httpContext)
        {
            if (Page_Context.Current.Initialized)
            {
                if (Page_Context.Current.PageRequestContext.RequestChannel == Web.FrontRequestChannel.Design)
                {
                    return new NullMembershipAuthentication();
                }
            }

            var site = Site.Current;
            var membership = site.GetMembership();
            if (membership == null)
            {
                return new NullMembershipAuthentication();
            }
            else
            {
                return new MembershipAuthentication(site, membership, httpContext);
            }
        }
    }
}