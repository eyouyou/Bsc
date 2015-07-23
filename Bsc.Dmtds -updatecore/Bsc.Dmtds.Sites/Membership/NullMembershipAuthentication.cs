using System.Security.Principal;

namespace Bsc.Dmtds.Sites.Membership
{
    public class NullMembershipAuthentication : IMembershipAuthentication
    {
        public void SetAuthCookie(string userName, bool createPersistentCookie)
        {

        }

        public void SignOut()
        {

        }

        public System.Security.Principal.IPrincipal GetMember()
        {
            return new GenericPrincipal(new GenericIdentity(""), new string[0]);
        }

        public bool IsAuthenticated()
        {
            return GetMember().Identity.IsAuthenticated;
        }


        public Bsc.Dmtds.Membership.Models.MembershipUser GetMembershipUser()
        {
            return null;
        }
    }
}