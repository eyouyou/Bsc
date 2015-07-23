namespace System.Web.Routing
{
    public sealed class LiteralSubsegment : PathSubsegment
    {
        public LiteralSubsegment(string literal)
        {
            this.Literal = literal;
        }

        // Properties
        public string Literal { get; private set; }
    }
}