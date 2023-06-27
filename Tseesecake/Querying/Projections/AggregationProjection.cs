using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tseesecake.Modeling;
using Tseesecake.Querying.Aggregations;
using Tseesecake.Querying.Filters;

namespace Tseesecake.Querying.Projections
{
    internal class AggregationProjection : IProjection
    {
        public IAggregation Aggregation { get; }
        public IFilter[]? Filters { get; }
        public Column Column { get; }
        public string Alias { get; }

        public string Template { get => nameof(AggregationProjection); }

        public AggregationProjection(IAggregation aggregation, Column column, string alias)
            : this(aggregation, column, null, alias) { }

        public AggregationProjection(IAggregation aggregation, Column column, IFilter[]? filters, string alias)
            => (Aggregation, Column, Filters, Alias) = (aggregation, column, filters?.Length==0 ? null : filters, alias);
    }
}
