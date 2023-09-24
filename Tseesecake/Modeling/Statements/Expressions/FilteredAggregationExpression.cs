using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tseesecake.Modeling.Statements.Aggregations;
using Tseesecake.Modeling.Statements.Filters;

namespace Tseesecake.Modeling.Statements.Expressions
{
    internal class FilteredAggregationExpression : AggregationExpression
    {
        public IFilter[] Filters { get; }
        public FilteredAggregationExpression(IAggregation aggregation, IFilter[] filters)
            : base(aggregation) { Filters = filters; }
    }
}
