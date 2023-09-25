using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tseesecake.Modeling;
using Tseesecake.Modeling.Statements;
using Tseesecake.Modeling.Statements.Expressions;
using Tseesecake.Modeling.Statements.Filters;
using Tseesecake.Modeling.Statements.Ordering;
using Tseesecake.Modeling.Statements.Projections;

namespace Tseesecake.Arrangers
{
    internal class ExpressionForwardedToOrderByClause : ISelectArranger
    {
        public void Execute(SelectStatement statement)
        {
            if (statement.Orders is null)
                return;

            var projections = statement.Projections.Where(x => x.Expression is AggregationExpression || x.Expression is VirtualColumnExpression);
            if (!projections.Any())
                return;

            for (int i = 0; i < statement.Orders.Count; i++)
            {
                if (statement.Orders[i] is ColumnOrder order)
                {
                    var projection = projections.SingleOrDefault(x => x.Alias == (order.Expression as ColumnReference)?.Name);
                    if (projection != null)
                        order.Expression = projection.Expression switch
                        {
                            AggregationExpression aggr => aggr,
                            VirtualColumnExpression virtualColumn => virtualColumn,
                            _ => throw new ArgumentOutOfRangeException()
                        };
                }
            }
        }
    }
}
