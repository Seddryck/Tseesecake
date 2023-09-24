using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tseesecake.Modeling.Statements.Aggregations;

namespace Tseesecake.Modeling.Statements.ColumnExpressions
{
    internal class AggregationExpression : BaseExpression
    {
        public IAggregation Aggregation { get; }
        public AggregationExpression(IAggregation aggregation)
            => (Aggregation) = (aggregation);
    }
}
