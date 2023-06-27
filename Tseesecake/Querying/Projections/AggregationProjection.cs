using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tseesecake.Modeling;
using Tseesecake.Querying.Aggregations;

namespace Tseesecake.Querying.Projections
{
    internal class AggregationProjection : IProjection
    {
        public IAggregation Aggregation { get; }
        public Column Column { get; }
        public string Alias { get; }

        public string Template { get => nameof(AggregationProjection); }

        public AggregationProjection(IAggregation aggregation, Column column, string alias)
            => (Aggregation, Column, Alias) = (aggregation, column, alias);
    }
}
