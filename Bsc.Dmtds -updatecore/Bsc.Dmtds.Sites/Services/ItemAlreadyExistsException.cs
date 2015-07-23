namespace Bsc.Dmtds.Sites.Services
{
    public class ItemAlreadyExistsException : FriendlyException
    {
        public ItemAlreadyExistsException()
            : base("该事物已经存在")
        {

        }
    }
}