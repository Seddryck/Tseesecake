using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tseesecake.Modeling;
using Tseesecake.Querying;
using Tseesecake.Querying.Filters;
using Tseesecake.Querying.Projections;

namespace Tseesecake.Arrangers
{
    internal class ExpressionForwardedToHavingClause : ISelectArranger
    {
        public void Execute(SelectStatement statement)
        {
            if (statement.GroupFilters is null)
                return;

            var expressions = statement.Projections.Where(x => x is AggregationProjection || x is ExpressionProjection);
            if (!expressions.Any())
                return;

            for (int i = 0; i < statement.GroupFilters.Count; i++)
            {
                if (statement.GroupFilters[i] is Sifter sifter)
                {
                    var expr = expressions.SingleOrDefault(x => x.Alias == sifter.Measurement.Name);
                    if (expr != null)
                        sifter.Measurement = expr switch
                        {
                            AggregationProjection aggrProjection => new AggregationMeasurement(sifter.Measurement.Name, aggrProjection.Aggregation),
                            ExpressionProjection exprProjection => new ExpressionMeasurement(sifter.Measurement.Name, exprProjection.Expression),
                            _ => throw new ArgumentOutOfRangeException()
                        };
                }
            }
        }
    }
}
