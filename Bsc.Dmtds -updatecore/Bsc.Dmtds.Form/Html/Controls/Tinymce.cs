

namespace Bsc.Dmtds.Form.Html.Controls
{
    /// <summary>
    /// 在线编辑器
    /// </summary>
    public class Tinymce : ControlBase
    {
        public override string Name
        {
            get { return "Tinymce"; }
        }

        protected override string RenderInput(IColumn column)
        {
            return string.Format(@"<textarea name=""{0}"" id=""{0}"" class=""{0} tinymce"" media_library_url=""@Url.Action(""Selection"",""MediaContent"",ViewContext.RequestContext.AllRouteValues()))"" media_library_title =""@(""Selected Files"".Localize())"" rows=""10"" cols=""100"">@( Model.{0} ?? """")</textarea>", column.Name);
        }
    }
}
