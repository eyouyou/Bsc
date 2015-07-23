using System.Security.Principal;
using Bsc.Dmtds.Membership.Models;

namespace Bsc.Dmtds.Sites.Membership
{
    public interface IMembershipAuthentication
    {
        void SetAuthCookie(string userName, bool createPersistentCookie);
        void SignOut();
        IPrincipal GetMember();
        MembershipUser GetMembershipUser();
        bool IsAuthenticated();
 
    }
}