#region License
// 
// Copyright (c) 2013, Kooboo team
// 
// Licensed under the BSD License
// See the file LICENSE.txt for details.
// 
#endregion

namespace Bsc.Dmtds.Content.Query.Expressions
{
    public class WhereInExpression : WhereFieldExpression
    {
        public WhereInExpression(string fieldName, object[] values)
            : this(null, fieldName, values)
        {

        }
        public WhereInExpression(IExpression expression, string fieldName, object[] values)
            : base(expression, fieldName)
        {
            this.Values = values;
        }
        public object[] Values { get; set; }
    }
}
