﻿

namespace Bsc.Dmtds.Form.Html.Controls
{
    public class Empty : IControl
    {
        public string Name
        {
            get
            {
                return "Empty";
            }
        }

        public string Render(ISchema schema, IColumn column)
        {
            return string.Format(@"<input type=""hidden"" name=""{0}"" value=""@(Model.{0} ?? """")""/>", column.Name);
        }

        public bool IsFile
        {
            get { return false; }
        }

        public string GetValue(object oldValue, string newValue)
        {
            return newValue;
        }


        public bool HasDataSource
        {
            get { return false; }
        }


        public string DataType
        {
            get { return null; }
        }
    }
}
