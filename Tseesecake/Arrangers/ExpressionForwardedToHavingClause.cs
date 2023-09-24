using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tseesecake.Modeling.Statements;
using Tseesecake.Modeling.Statements.Aggregations;
using Tseesecake.Modeling.Statements.ColumnExpressions;
using Tseesecake.Modeling.Statements.Filters;

namespace Tseesecake.Arrangers
{
    internal class ExpressionForwardedToHavingClause : ISelectArranger
    {
        public void Execute(SelectStatement statement)
        {
            if (statement.GroupFilters is null)
                return;

            var expressions = statement.Projections.Where(x => x.Expression is AggregationExpression || x is VirtualColumnExpression);
            if (!expressions.Any())
                return;

            for (int i = 0; i < statement.GroupFilters.Count; i++)
            {
                if (statement.GroupFilters[i] is Sifter sifter)
                {
                    var expr = expressions.SingleOrDefault(x => x.Alias == sifter.Measurement.Name)?.Expression;
                    if (expr != null)
                        sifter.Measurement = expr switch
                        {
                            AggregationExpression aggr => new AggregationMeasurement(sifter.Measurement.Name, aggr.Aggregation),
                            VirtualColumnExpression virtualColumn => new LiteralMeasurement(sifter.Measurement.Name, string.Empty),
                            _ => throw new ArgumentOutOfRangeException()
                        };
                }
            }
        }
    }
}
