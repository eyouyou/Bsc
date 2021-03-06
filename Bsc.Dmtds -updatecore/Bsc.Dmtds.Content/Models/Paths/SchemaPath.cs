﻿using System.IO;
using Bsc.Dmtds.Common.IO;
using Bsc.Dmtds.Common.Util;

namespace Bsc.Dmtds.Content.Models.Paths
{
    public class SchemaPath : IPath
    {
        public SchemaPath(Schema schema)
        {
            RepositoryPath repositoryPath = new RepositoryPath(schema.Repository);
            var basePhysicalPath = GetBaseDir(schema.Repository);
            this.PhysicalPath = Path.Combine(basePhysicalPath, schema.Name);
            this.SettingFile = Path.Combine(PhysicalPath, PathHelper.SettingFileName);
            VirtualPath = UrlUtility.RawCombine(repositoryPath.VirtualPath, PATH_NAME, schema.Name);
        }
        public static string GetBaseDir(Repository repository)
        {
            return Path.Combine(new RepositoryPath(repository).PhysicalPath, PATH_NAME);
        }
        const string PATH_NAME = "Schemas";
        #region IPath Members

        public string PhysicalPath
        {
            get;
            private set;
        }

        public string VirtualPath
        {
            get;
            private set;
        }

        public string SettingFile
        {
            get;
            private set;
        }


        public bool Exists()
        {
            return File.Exists(this.SettingFile);
        }

        public void Rename(string newName)
        {
            IOUtility.RenameFile(this.PhysicalPath, @newName + ".config");
        }

        #endregion       
    }
}