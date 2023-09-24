using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Linq.Expressions;

namespace Tseesecake.Modeling.Statements.ColumnExpressions
{
    internal class VirtualColumnExpression : BaseExpression
    {
        public Expression Expression { get; }

        public VirtualColumnExpression(Expression expression)
            => Expression = expression;
    }
}
