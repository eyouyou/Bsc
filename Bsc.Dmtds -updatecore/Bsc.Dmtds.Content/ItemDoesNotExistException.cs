namespace Bsc.Dmtds.Content
{
    public class ItemDoesNotExistException: FriendlyException
    {
        public ItemDoesNotExistException()
            : base("该事物不存在")
        {

        }
    }
}