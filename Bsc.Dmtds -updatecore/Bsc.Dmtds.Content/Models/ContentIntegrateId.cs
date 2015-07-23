using System;
using Bsc.Dmtds.Common;

namespace Bsc.Dmtds.Content.Models
{
    public class ContentIntegrateId : IntegrateIdBase
    {
        public string Repository { get; private set; }
        public string FolderName { get; private set; }
        public string ContentUUID { get; private set; }
        #region .ctor
        public ContentIntegrateId(ContentBase content)
            : this(content.Repository, content.FolderName, content.UUID)
        {

        }
        public ContentIntegrateId(string repository, string folderName, string uuid)
        {
            base.Segments = new[] { repository, folderName, uuid };
        }

        public ContentIntegrateId(string id)
            : base(id)
        {
            if (base.Segments == null || base.Segments.Length < 3)
            {
                throw new ArgumentException("Invalid content integrate id.");
            }

            Repository = this.Segments[0];
            FolderName = this.Segments[1];
            ContentUUID = this.Segments[2];
        }
        #endregion
    }
}