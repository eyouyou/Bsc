namespace Bsc.Dmtds.Content
{
    public class NameIsReqiredException : FriendlyException
    {
        public NameIsReqiredException() : base("名字是必须的") { }

    }
}