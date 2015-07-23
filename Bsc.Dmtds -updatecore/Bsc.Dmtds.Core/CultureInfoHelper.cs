using System.Globalization;

namespace Bsc.Dmtds.Core
{
    public class CultureInfoHelper
    {
        /// <summary>
        /// Creates the culture info.
        /// </summary>
        /// <param name="cultureName">Name of the culture.</param>
        /// <returns></returns>
        public static CultureInfo CreateCultureInfo(string cultureName)
        {
            var culture = new CultureInfo("zh-CN");
            try
            {
                culture = new CultureInfo(cultureName);
            }
            catch
            {
            }
            return culture;

        } 
    }
}