using System;
using System.IO;
using Bsc.Dmtds.Common.Util;

namespace Bsc.Dmtds.Common
{
    public class Settings
    {
        static bool isHostByIIS = false;
        static Settings()
        {
            isHostByIIS = AppDomain.CurrentDomain.FriendlyName.ToUpper().StartsWith("/Bsc/");
        }
        /// <summary>
        /// Gets the base directory.
        /// </summary>
        /// <value>
        /// The base directory.
        /// </value>
        public static string BaseDirectory
        {
            get
            {
                return AppDomain.CurrentDomain.SetupInformation.ApplicationBase;
            }
        }
        /// <summary>
        /// Gets the bin directory.
        /// </summary>
        /// <value>
        /// The bin directory.
        /// </value>
        public static string BinDirectory
        {
            get
            {
                if (IsWebApplication)
                {
                    return AppDomain.CurrentDomain.SetupInformation.PrivateBinPath;
                }
                else
                {
                    return AppDomain.CurrentDomain.SetupInformation.ApplicationBase;
                }
            }
        }
        /// <summary>
        /// Gets the components directory.
        /// </summary>
        /// <value>
        /// The components directory.
        /// </value>
        public static string ComponentsDirectory
        {
            get
            {
                return AppDomain.CurrentDomain.SetupInformation.ApplicationBase + "Components";
            }
        }

        /// <summary>
        /// Gets a value indicating whether this instance is web application.
        /// </summary>
        /// <value>
        /// <c>true</c> if this instance is web application; otherwise, <c>false</c>.
        /// </value>
        public static bool IsWebApplication
        {
            get
            {
                return Path.GetFileName(AppDomain.CurrentDomain.SetupInformation.ConfigurationFile).EqualsOrNullEmpty("Web.config", StringComparison.OrdinalIgnoreCase);
            }
        }
        public static bool IsHostByIIS
        {
            get
            {
                return isHostByIIS;
            }
        } 
    }
}