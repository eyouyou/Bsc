using System.Web.Mvc;

namespace Bsc.Dmtds.Core.Mvc.Html
{
    public static class ValidatorExtensions
    {
        public static ModelClientValidationRule Remote(string url, string errorMessage = "", string httpMethod = "POST")
        {
            ModelClientValidationRemoteRule clientValidationRule = new ModelClientValidationRemoteRule(errorMessage, url, httpMethod, "");

            return clientValidationRule;
        }
    }
}