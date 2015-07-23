using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using Bsc.Dmtds.Common.IO;
using Bsc.Dmtds.Common.Util;
using Bsc.Dmtds.Core.Runtime.Dependency;

namespace Bsc.Dmtds.Core
{

    #region IBaseDir

    public interface IBaseDir
    {
        /// <summary>
        /// Gets the CMS base dir.
        /// </summary>
        /// <value>
        /// The CMS base dir.
        /// </value>
        string BscBaseDir { get; }

        /// <summary>
        /// Gets the name of the CMS_ data path.
        /// </summary>
        /// <value>
        /// The name of the CMS_ data path.
        /// </value>
        string DataPathName { get; }

        /// <summary>
        /// Gets the CMS_data base path.
        /// </summary>
        /// <value>
        /// The CMS_data base path.
        /// </value>
        string DataPhysicalPath { get; }

        /// <summary>
        /// Gets the CMS_data virutal path.
        /// </summary>
        /// <value>
        /// The CMS_ data virutal path.
        /// </value>
        string DataVirtualPath { get; }

        /// <summary>
        /// Gets the name of the setting file.
        /// </summary>
        /// <value>
        /// The name of the setting file.
        /// </value>
        string SettingFileName { get; }
        [Obsolete]
        void UpdateFileLink(string sitePath, string newSiteName, string newDatabaseName);
    }

    #endregion

    #region BaseDir
    /// <summary>
    /// 
    /// </summary>
    [Dependency(typeof(IBaseDir))]
    public class BaseDir : IBaseDir
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="BaseDir" /> class.
        /// </summary>
        public BaseDir()
        {
            this.BscBaseDir = AppDomain.CurrentDomain.BaseDirectory;
            this.DataPathName = "Bsc_Data";
            this.DataPhysicalPath = Path.Combine(BscBaseDir, this.DataPathName);
            DataVirtualPath = UrlUtility.Combine("~/", this.DataPathName);
        }

        /// <summary>
        /// Gets the CMS base dir.
        /// </summary>
        /// <value>
        /// The CMS base dir.
        /// </value>
        public string BscBaseDir
        {
            get;
            private set;
        }
        /// <summary>
        /// Gets the name of the CMS_ data path.
        /// </summary>
        /// <value>
        /// The name of the CMS_ data path.
        /// </value>
        public string DataPathName
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets the CMS_ data base path.
        /// </summary>
        /// <value>
        /// The CMS_ data base path.
        /// </value>
        public string DataPhysicalPath
        {
            get;
            private set;
        }


        /// <summary>
        /// Gets the name of the setting file.
        /// </summary>
        /// <value>
        /// The name of the setting file.
        /// </value>
        public string SettingFileName
        {
            get { return "setting.config"; }
        }


        /// <summary>
        /// Gets or sets the CMS_ data virutal path.
        /// </summary>
        public string DataVirtualPath
        {
            get;
            set;
        }


        public virtual void UpdateFileLink(string sitePath, string newSiteName, string newDatabaseName)
        {
            string sitesBaseVirtualPath = "/" + DataPathName + "/Sites";
            string siteFilePathPattern = sitesBaseVirtualPath + "/[^/]+/";
            string siteFileReplacement = sitesBaseVirtualPath + "/" + (newSiteName ?? "") + "/";

            string databaseBaseVirtualPath = "/" + DataPathName + "/Contents";
            string databaseFilePathPattern = databaseBaseVirtualPath + "/[^/]+/";
            string databaseFilePathReplacement = databaseBaseVirtualPath + "/" + (newDatabaseName ?? "") + "/";

            foreach (var file in GetFiles(sitePath, new[] { "*.cshtml", "*.html", "*.xml" }, SearchOption.AllDirectories))
            {
                if (!string.IsNullOrEmpty(newSiteName))
                {
                    ReplaceFile(file, siteFilePathPattern, siteFileReplacement);
                }
                if (!string.IsNullOrEmpty(newDatabaseName))
                {
                    ReplaceFile(file, databaseFilePathPattern, databaseFilePathReplacement);
                }
            }
        }
        protected virtual IEnumerable<string> GetFiles(string path, string[] searchPatterns, SearchOption searchOption = SearchOption.TopDirectoryOnly)
        {
            return searchPatterns.AsParallel().SelectMany(searchPattern => Directory.EnumerateFiles(path, searchPattern, searchOption));
        }
        private void ReplaceFile(string filePath, string pattern, string replacement)
        {
            string fileBody = IOUtility.ReadAsString(filePath);
            fileBody = Regex.Replace(fileBody, pattern, replacement, RegexOptions.IgnoreCase);
            IOUtility.SaveStringToFile(filePath, fileBody);
        }
    }
    #endregion
}