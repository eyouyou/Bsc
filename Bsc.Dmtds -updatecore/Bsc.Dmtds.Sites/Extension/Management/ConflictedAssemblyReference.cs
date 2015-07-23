namespace Bsc.Dmtds.Sites.Extension.Management
{
    public class ConflictedAssemblyReference
    {
        public ConflictedAssemblyReference()
        { }
        public ConflictedAssemblyReference(AssemblyReferenceData referenceData, string conflictedVersion)
        {
            this.ReferenceData = referenceData;
            this.ConflictedVersion = conflictedVersion;
        }
        public AssemblyReferenceData ReferenceData { get; set; }
        public string ConflictedVersion { get; set; } 
    }
}