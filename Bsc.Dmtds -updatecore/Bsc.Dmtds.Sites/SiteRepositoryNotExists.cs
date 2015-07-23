using Bsc.Dmtds.Common;

namespace Bsc.Dmtds.Sites
{
    public class SiteRepositoryNotExists:BscException
    {
        public SiteRepositoryNotExists()
            : base("The site repository doest not exists.")
        {

        }
  
    }
}