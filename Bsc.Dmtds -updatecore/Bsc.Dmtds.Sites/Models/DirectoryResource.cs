using System.IO;
using System.Runtime.Serialization;
using Bsc.Dmtds.Common.IO;

namespace Bsc.Dmtds.Sites.Models
{
    [DataContract]
    public abstract class DirectoryResource : PathResource
    {
        protected DirectoryResource()
        {
        }
        protected DirectoryResource(string physicalPath)
            : base(physicalPath)
        {
        }
        protected DirectoryResource(Site site, string name)
            : base(site, name)
        {
        }
        public override bool Exists()
        {
            return Directory.Exists(this.PhysicalPath);
        }
        public override void Delete()
        {
            if (Exists())
            {
                Directory.Delete(this.PhysicalPath, true);
            }
        }
        public override void Rename(string @newName)
        {
            var newPath = Path.Combine(this.BasePhysicalPath, @newName);
            IOUtility.RenameDirectory(this.PhysicalPath, newPath);
            this.Name = @newName;
        }
    }
}