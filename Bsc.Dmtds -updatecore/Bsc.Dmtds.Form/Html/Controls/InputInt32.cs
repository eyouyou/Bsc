

namespace Bsc.Dmtds.Form.Html.Controls
{
    public class InputInt32 : ControlBase
    {
        #region IControl Members

        public override string Name
        {
            get
            {
                return "Int32";
            }
        }
        public override string DataType
        {
            get
            {
                return "Int";
            }
        }

        protected override string RenderInput(IColumn column)
        {
            return string.Format(@"<input class=""long"" id=""{0}"" name=""{0}"" type=""text"" value=""@(Model.{0} ?? """")"" {1}/>", column.Name, ValidationExtensions.GetUnobtrusiveValidationAttributeString(column));
        }


        #endregion
    }
}
