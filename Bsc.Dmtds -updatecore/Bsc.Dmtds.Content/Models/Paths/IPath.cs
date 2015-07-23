namespace Bsc.Dmtds.Content.Models.Paths
{
    public interface IPath
    {
        string PhysicalPath { get; }
        string VirtualPath { get; }
        string SettingFile { get; }

        bool Exists();
        void Rename(string newName); 
    }
}