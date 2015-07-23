using Bsc.Dmtds.Common.Util;

namespace Bsc.Dmtds.Content
{
    public class UUIDGenerator
    {
        static UUIDGenerator()
        {
            DefaultGenerator = new UUIDGenerator();
        }
        public static UUIDGenerator DefaultGenerator { get; set; }

        public virtual string Generate(ContentBase content)
        {
            return UniqueIdGenerator.GetInstance().GetBase32UniqueId(16);
        } 
    }
}