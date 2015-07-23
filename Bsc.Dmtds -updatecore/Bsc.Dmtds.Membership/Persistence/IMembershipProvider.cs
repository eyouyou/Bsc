using System.IO;
using Bsc.Dmtds.Core.Persistence.Non_Relational;

namespace Bsc.Dmtds.Membership.Persistence
{
    public interface IMembershipProvider : IProvider<Bsc.Dmtds.Membership.Models.Membership>
    {
        Bsc.Dmtds.Membership.Models.Membership Import(string membershipName, Stream stream);
        void Export(Bsc.Dmtds.Membership.Models.Membership membership, Stream outputStream);
    }
}