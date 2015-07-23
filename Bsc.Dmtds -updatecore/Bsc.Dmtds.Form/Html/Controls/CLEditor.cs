
namespace Bsc.Dmtds.Form.Html.Controls
{
    /// <summary>
    /// 小巧的html编辑器
    /// </summary>
    public class CLEditor : ControlBase
    {
        public override string Name
        {
            get { return "CLEditor"; }
        }

        protected override string RenderInput(IColumn column)
        {
            return string.Format(@"<textarea name=""{0}"" id=""{0}"" rows=""10"" cols=""100"">@Model[""{0}""]</textarea> 
        <script language=""javascript"" type=""text/javascript"">$(function(){{$(""#{0}"").cleditor();}});</script>", column.Name);
        }
    }
}
