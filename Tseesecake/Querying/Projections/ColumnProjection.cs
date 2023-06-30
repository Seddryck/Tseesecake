using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tseesecake.Modeling;
using Tseesecake.Querying.Expressions;

namespace Tseesecake.Querying.Projections
{
    internal class ColumnProjection : ExpressionProjection
    {
        public override string Template { get => nameof(ColumnProjection); }

        public ColumnProjection(Column column)
            : base(new ColumnExpression(column), column.Name) { }
    }
}
