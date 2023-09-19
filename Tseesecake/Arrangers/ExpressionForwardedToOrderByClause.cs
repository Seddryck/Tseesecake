using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tseesecake.Modeling;
using Tseesecake.Querying;
using Tseesecake.Querying.Filters;
using Tseesecake.Querying.Ordering;
using Tseesecake.Querying.Projections;

namespace Tseesecake.Arrangers
{
    internal class ExpressionForwardedToOrderByClause : ISelectArranger
    {
        public void Execute(SelectStatement statement)
        {
            if (statement.Orders is null)
                return;

            var expressions = statement.Projections.Where(x => x is AggregationProjection || x is ExpressionProjection);
            if (!expressions.Any())
                return;

            for (int i = 0; i < statement.Orders.Count; i++)
            {
                if (statement.Orders[i] is ColumnOrder order)
                {
                    var expr = expressions.SingleOrDefault(x => x.Alias == order.Reference.Name);
                    if (expr != null)
                        order.Reference = expr switch
                        {
                            AggregationProjection aggrProjection => new AggregationMeasurement(order.Reference.Name, aggrProjection.Aggregation),
                            ExpressionProjection exprProjection => new ExpressionMeasurement(order.Reference.Name, exprProjection.Expression),
                            _ => throw new ArgumentOutOfRangeException()
                        };
                }
            }
        }
    }
}
