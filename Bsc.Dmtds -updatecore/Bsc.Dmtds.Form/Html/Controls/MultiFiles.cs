

namespace Bsc.Dmtds.Form.Html.Controls
{
    public class MultiFiles : Input
    {
        public override string Name
        {
            get { return "MultiFiles"; }
        }
        public override string Type
        {
            get { return "file"; }
        }
        protected override string RenderInput(IColumn column)
        {
            return string.Format(@"<input id=""{0}"" name=""{0}"" type=""{1}"" value=""@(Model.{0} ?? """")""  displayValue=""@(Model.{0} ?? """")"" {2} multiple=""multiple""/>", column.Name, Type, ValidationExtensions.GetUnobtrusiveValidationAttributeString(column));
        }
        public override bool IsFile
        {
            get
            {
                return true;
            }
        }        
    }
}
