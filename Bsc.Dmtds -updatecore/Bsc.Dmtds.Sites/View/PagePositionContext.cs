using System.Collections.Generic;
using System.Web.Mvc;
using Bsc.Dmtds.Common.Collection;
using Bsc.Dmtds.Core.Persistence.Non_Relational;

namespace Bsc.Dmtds.Sites.View
{
    public class PagePositionContext
    {
        public PagePositionContext(Models.View view, IDictionary<string, object> parameters, ViewDataDictionary viewData)
        {
            this.View = view.AsActual();
            this.Parameters = new DynamicDictionary(View.CombineDefaultParameters(parameters));
            this.ViewData = viewData;
        }

        public Models.View View { get; private set; }

        public ViewDataDictionary ViewData { get; private set; }

        public IDictionary<string, object> Parameters
        {
            get;
            private set;
        } 
    }
}