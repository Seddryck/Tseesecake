using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tseesecake.Modeling.Statements;
using Tseesecake.Modeling.Statements.ColumnExpressions;

namespace Tseesecake.Querying.Expressions
{
    internal class ColumnExpression : BaseExpression
    {
        public IColumn Reference { get; set; }
        public ColumnExpression(IColumn reference)
            => Reference = reference;
    }
}
