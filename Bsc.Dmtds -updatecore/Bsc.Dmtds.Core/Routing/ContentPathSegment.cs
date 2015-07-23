using System.Collections.Generic;
using System.Linq;

namespace System.Web.Routing
{
    public class ContentPathSegment : PathSegment
    {
        // Methods
        public ContentPathSegment(IList<PathSubsegment> subsegments)
        {
            this.Subsegments = subsegments;
        }

        // Properties
        public bool IsCatchAll
        {
            get
            {
                return this.Subsegments.Any<PathSubsegment>(seg => ((seg is ParameterSubsegment) && ((ParameterSubsegment)seg).IsCatchAll));
            }
        }

        public IList<PathSubsegment> Subsegments { get; private set; }
    }
}