using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tseesecake.Querying.Aggregations
{
    internal abstract class BaseAggregation : IAggregation
    {
        public string Name => GetType().Name.Replace("Aggregation", "").ToLowerInvariant();
    }
}
