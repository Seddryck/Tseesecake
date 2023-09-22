using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tseesecake.Modeling;
using Tseesecake.Modeling.Statements.Aggregations;
using Tseesecake.Querying.Filters;

namespace Tseesecake.Querying.Projections
{
    internal class AggregationProjection : IProjection
    {
        public IAggregation Aggregation { get; }
        public IFilter[]? Filters { get; }
        public string Alias { get; }

        public string Template { get => nameof(AggregationProjection); }

        public AggregationProjection(IAggregation aggregation, string alias)
            : this(aggregation, null, alias) { }

        public AggregationProjection(IAggregation aggregation, IFilter[]? filters, string alias)
            => (Aggregation, Filters, Alias) = (aggregation, filters?.Length==0 ? null : filters, alias);
    }
}
