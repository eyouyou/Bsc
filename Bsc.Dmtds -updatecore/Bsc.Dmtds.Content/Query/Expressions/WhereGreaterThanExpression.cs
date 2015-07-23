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
    public class WhereGreaterThanExpression : BinaryExpression
    {
        public WhereGreaterThanExpression(string fieldName, object value)
            : this(null, fieldName, value)
        {

        }
        public WhereGreaterThanExpression(IExpression expression, string fieldName, object value)
            : base(expression, fieldName, value)
        {

        }
    }
}
