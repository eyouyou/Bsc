namespace Bsc.Dmtds.Content
{
    public class ItemAlreadyExistsException: FriendlyException
    {
        public ItemAlreadyExistsException()
            : base("该事物已存在")
        {

        }
    }
}