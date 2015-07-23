namespace Bsc.Dmtds.Core.DataViolation
{
    public class DataViolationItem
    {
        public string PropertyName { get; private set; }
        public object PropertyValue { get; private set; }
        public string ErrorMessage { get; private set; }
        public DataViolationItem(string propertyName, string propertyValue, string errorMessage)
        {
            this.PropertyName = propertyName;
            this.PropertyValue = propertyValue;
            this.ErrorMessage = errorMessage;
        } 
    }
}