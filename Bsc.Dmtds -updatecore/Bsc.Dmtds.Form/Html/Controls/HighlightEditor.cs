

namespace Bsc.Dmtds.Form.Html.Controls
{
    public class HighlightEditor : ControlBase
    {
        public override string Name
        {
            get { return "HighlightEditor"; }
        }

        protected override string RenderInput(IColumn column)
        {
            return string.Format(@"
<textarea name=""{0}"" id=""{0}"" class=""{0} codemirror"" rows=""10"" cols=""100"">@( Model.{0} ?? """")</textarea>
", column.Name);
        }
    }
}
