

namespace Bsc.Dmtds.Form.Html.Controls
{
    public class TextArea : ControlBase
    {
        public override string Name
        {
            get { return "TextArea"; }
        }

        protected override string RenderInput(IColumn column)
        {
            return string.Format(@"<textarea class=""extra-large"" name=""{0}"" {1}>@(Model.{0} ?? """")</textarea> ", column.Name, ValidationExtensions.GetUnobtrusiveValidationAttributeString(column));
        }
    }
}
