

namespace Bsc.Dmtds.Form.Html.Controls
{
    public class Hidden : Input
    {
        public override string Name
        {
            get { return "Hidden"; }
        }

        public override string Type
        {
            get { return "hidden"; }
        }
        public override string Render(ISchema schema, IColumn column)
        {
            return base.RenderInput(column);
        }
    }
}
