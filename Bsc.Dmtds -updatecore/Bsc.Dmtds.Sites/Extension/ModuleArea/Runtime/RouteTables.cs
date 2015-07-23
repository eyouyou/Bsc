using System;
using System.Collections.Generic;
using System.Web.Routing;
using Bsc.Dmtds.Core.Mvc.Routing;

namespace Bsc.Dmtds.Sites.Extension.ModuleArea.Runtime
{
    public static class RouteTables
    {
        public static string RouteFile = "routes.config";

        static IDictionary<string, RouteCollection> routeTables = new Dictionary<string, RouteCollection>(StringComparer.CurrentCultureIgnoreCase);
        public static RouteCollection GetRouteTable(string moduleName)
        {
            if (!routeTables.ContainsKey(moduleName))
            {
                lock (routeTables)
                {
                    if (!routeTables.ContainsKey(moduleName))
                    {
                        var routeFile = GetRoutesFilePath(moduleName).PhysicalPath;
                        RouteCollection routeTable = new RouteCollection();
                        RouteTableRegister.RegisterRoutes(routeTable, routeFile);
                        routeTables[moduleName] = routeTable;
                    }
                }
            }
            return routeTables[moduleName];
        }

        public static ModuleItemPath GetRoutesFilePath(string moduleName)
        {
            return new ModuleItemPath(moduleName, RouteFile);
        }
    }
}