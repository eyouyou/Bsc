using System;
using System.Collections.Generic;
using System.Text;

namespace Bsc.Dmtds.Core.DataViolation
{
    public class DataViolationException : Exception
    {
        public IEnumerable<DataViolationItem> Violations { get; private set; }
        public DataViolationException(IEnumerable<DataViolationItem> violations)
            : this("", violations)
        {
        }
        public DataViolationException(string msg, IEnumerable<DataViolationItem> violations)
            : base(msg)
        {
            this.Violations = violations;
        }
        public override string Message
        {
            get
            {
                string baseMessage = base.Message;
                StringBuilder stringBuilder = new StringBuilder();
                stringBuilder.AppendFormat("异常信息:{0} \n Issues:\n", baseMessage);
                foreach (var issue in Violations)
                {
                    stringBuilder.AppendFormat("属性名:{0};属性值:{1};错误信息:{2} \n", issue.PropertyName, issue.PropertyValue, issue.ErrorMessage);
                }
                return stringBuilder.ToString();
            }
        }

    }
}