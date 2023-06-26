using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tseesecake.Querying.Aggregations
{
    internal class MaxAggregation : IAggregation
    {
        public string Name => GetType().Name.Replace("Aggregation", "").ToLowerInvariant();
    }
}
