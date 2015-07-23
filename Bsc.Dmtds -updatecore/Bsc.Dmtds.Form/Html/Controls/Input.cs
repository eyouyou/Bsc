

namespace Bsc.Dmtds.Form.Html.Controls
{
    public abstract class Input : ControlBase
    {
        #region IControl Members

        public abstract string Type { get; }
        protected override string RenderInput(IColumn column)
        {
            return string.Format(@"<input class='long' id=""{0}"" name=""{0}"" type=""{1}"" value=""@(Model.{0} ?? """")"" {2}/>", column.Name, Type, ValidationExtensions.GetUnobtrusiveValidationAttributeString(column));
        }


        #endregion
    }
}
