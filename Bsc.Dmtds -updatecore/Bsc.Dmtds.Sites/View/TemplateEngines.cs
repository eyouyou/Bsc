using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Bsc.Dmtds.Common;
using Bsc.Dmtds.Common.Util;
using Bsc.Dmtds.Sites.Models;
using Bsc.Dmtds.Sites.View.CodeSnippet;

namespace Bsc.Dmtds.Sites.View
{
    public enum CodeType
    {
        List,
        Detail
    }
    public interface ITemplateEngine
    {
        string Name { get; }
        int Order { get; }
        string GetFileExtensionForLayout();
        string GetFileExtensionForView();
        IEnumerable<string> FileExtensions { get; }
        IViewEngine GetViewEngine();
        IView CreateView(ControllerContext controllerContext, string viewPath, string masterPath);

        IDataRuleCodeSnippet GetDataRuleCodeSnippet(TakeOperation takeOperation);
        ILayoutPositionParser GetLayoutPositionParser();
        ICodeHelper GetCodeHelper();
    }
    public class TemplateEngines
    {
        private static List<ITemplateEngine> engines = new List<ITemplateEngine>();
        static TemplateEngines()
        {
            //RegisterEngine(new ASPXTemplateEngine());
            //RegisterEngine(new RazorTemplateEngine());
        }
        public static IEnumerable<ITemplateEngine> Engines
        {
            get
            {
                return engines.OrderBy(it => it.Order);
            }
        }
        public static void RegisterEngine(ITemplateEngine engine)
        {
            lock (engines)
            {
                engines.Add(engine);
            }
        }

        public static ITemplateEngine GetEngineByFileExtension(string fileExtension)
        {
            var engine = engines.Where(it => it.FileExtensions.Contains(fileExtension, StringComparer.OrdinalIgnoreCase)).FirstOrDefault();
            if (engine == null)
            {
                throw new NotSupportedException(string.Format("Not supported engine for '{0}'", fileExtension));
            }
            return engine;
        }
        public static ITemplateEngine GetEngineByName(string name)
        {
            var engine = engines.Where(it => it.Name.EqualsOrNullEmpty(name, StringComparison.OrdinalIgnoreCase)).FirstOrDefault();
            if (engine == null)
            {
                throw new NotSupportedException(string.Format("找不到引擎. 名:'{0}'", name));
            }
            return engine;
        } 
    }
}