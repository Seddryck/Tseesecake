using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tseesecake.Modeling
{
    public class Timeseries
    {
        public string Name { get; }
        protected Timestamp Timestamp { get; }
        protected DataPoint[] DataPoints { get; }
        protected Facet[] Facets { get; } = Array.Empty<Facet>();

        public Timeseries(string name, Timestamp timestamp, DataPoint datapoint)
            => (Name, Timestamp, DataPoints) = (name, timestamp, new DataPoint[] { datapoint });
    }
}
