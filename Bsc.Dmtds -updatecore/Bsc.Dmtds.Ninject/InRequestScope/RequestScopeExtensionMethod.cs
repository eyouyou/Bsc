using System;
using Ninject.Activation;
using Ninject.Syntax;

namespace Bsc.Dmtds.Ninject.InRequestScope
{
    public static class RequestScopeExtensionMethod
    {
        // Methods
        private static object GetScope(IContext ctx)
        {
            return ((object)System.Web.HttpContext.Current ?? (object)System.Threading.Thread.CurrentThread);
        }

        public static IBindingNamedWithOrOnSyntax<T> InRequestScope<T>(this IBindingInSyntax<T> syntax)
        {
            return syntax.InScope(new Func<IContext, object>(RequestScopeExtensionMethod.GetScope));
        } 
    }
}