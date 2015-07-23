

namespace Bsc.Dmtds.Form.Html.Controls
{
    public class File : Input
    {
        public override string Name
        {
            get { return "File"; }
        }

        public override string Type
        {
            get { return "file"; }
        }
        public override bool IsFile
        {
            get
            {
                return true;
            }
        }
        protected override string RenderInput(IColumn column)
        {
            return string.Format(@"<input id=""{0}"" name=""{0}"" type=""{1}"" data-value=""@(Model.{0} ?? """")"" class='filestyle' {2}/>", column.Name, Type, ValidationExtensions.GetUnobtrusiveValidationAttributeString(column));
        }
    }
}
