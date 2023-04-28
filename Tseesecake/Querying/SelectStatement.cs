using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tseesecake.Modeling;

namespace Tseesecake.Querying
{
    public class SelectStatement
    {
        public Timeseries Timeseries { get; }
        public IProjection[] Projections { get; }

        public SelectStatement(Timeseries timeseries, IProjection[] projections)
            => (Timeseries, Projections) = (timeseries, projections);
    }
}
