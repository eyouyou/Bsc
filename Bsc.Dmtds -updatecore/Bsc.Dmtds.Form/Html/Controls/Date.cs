

namespace Bsc.Dmtds.Form.Html.Controls
{
    public class Date : Input
    {
        public override string Name
        {
            get { return "Date"; }
        }
        public override string Type
        {
            get { return "text"; }
        }
        public override string DataType
        {
            get
            {
                return "DateTime";
            }
        }

        protected override string RenderInput(IColumn column)
        {
            string input = string.Format(@"<input class=""long"" id=""{0}"" name=""{0}"" type=""{1}"" value=""@(Model.{0} ==null ? """" : ((Model.{0} is string)? Model.{0} : Model.{0}.ToLocalTime().ToShortDateString()))"" {2}/>", column.Name, Type, ValidationExtensions.GetUnobtrusiveValidationAttributeString(column));
            return input + string.Format(@"<script language=""javascript"" type=""text/javascript"">$(function(){{$(""#{0}"").datepicker();}});</script>", column.Name);
        }
    }
}
