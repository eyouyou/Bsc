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
    public abstract class WhereFieldExpression : Expression, IWhereExpression
    {
        public WhereFieldExpression(IExpression expression, string fieldName)
            : base(expression)
        {
            this.FieldName = fieldName;
        }
        public string FieldName { get; private set; }

    }
}
