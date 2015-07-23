using System.Collections.Generic;
using System.Web.Mvc;

namespace Bsc.Dmtds.Core
{
    public abstract class AreaRegistrationEx : AreaRegistration
    {
        private static List<string> _areas = new List<string>();
        public static IEnumerable<string> AllAreas
        {
            get
            {
                return _areas;
            }
        }
        public override void RegisterArea(AreaRegistrationContext context)
        {
            _areas.Add(context.AreaName);
        }
    }
}