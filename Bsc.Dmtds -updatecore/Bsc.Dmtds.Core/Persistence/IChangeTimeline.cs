using System;

namespace Bsc.Dmtds.Core.Persistence
{
    public interface IChangeTimeline
    {
        DateTime? UtcCreationDate { get; set; }


        DateTime? UtcLastestModificationDate { get; set; }


        string LastestEditor { get; set; }
    }
}