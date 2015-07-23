

namespace Bsc.Dmtds.Content.Query.Expressions
{
    public static class ExpressionValueHelper
    {
        public static object Escape(object value)
        {
            //if (value is DateTime)
            //{
            //    return TimeZoneHelper.ConvertToUtcTime((DateTime)value);
            //}
            //else
            //{
            return value;
            //}
        }
    }
}
