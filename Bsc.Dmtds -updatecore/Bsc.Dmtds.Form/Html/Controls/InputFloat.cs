

namespace Bsc.Dmtds.Form.Html.Controls
{
    public class InputFloat : ControlBase
    {
        #region IControl Members

        public override string Name
        {
            get
            {
                return "Float";
            }
        }
        public override string DataType
        {
            get
            {
                return "Decimal";
            }
        }
        protected override string RenderInput(IColumn column)
        {

            return string.Format(@"<input class=""long numeric"" id=""{0}"" name=""{0}"" type=""text"" value=""@(Model.{0} ?? """")"" {1} value-type=""{2}""/>", column.Name, ValidationExtensions.GetUnobtrusiveValidationAttributeString(column), "float");
        }


        #endregion
    }
}
