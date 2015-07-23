namespace Bsc.Dmtds.Content.Models
{
    public interface IRepositoryElement
    {
        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        string Name { get; set; }
        /// <summary>
        /// Gets or sets the repository.
        /// </summary>
        Repository Repository { get; set; } 
    }
}