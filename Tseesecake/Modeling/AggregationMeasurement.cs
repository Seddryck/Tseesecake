using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tseesecake.Querying.Aggregations;

namespace Tseesecake.Modeling
{
    internal class AggregationMeasurement : Measurement
    {
        public IAggregation Aggregation { get; }
        public AggregationMeasurement(string name, IAggregation aggregation)
            : base(name) { Aggregation = aggregation; }
    }
}
