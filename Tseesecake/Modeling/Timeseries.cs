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
        public Timestamp Timestamp { get; }
        public Measurement[] Measurements { get; }
        public Facet[] Facets { get; } = Array.Empty<Facet>();

        public Timeseries(string name, Timestamp timestamp, Measurement measurement)
            : this(name, timestamp, measurement, null) {}

        public Timeseries(string name, Timestamp timestamp, Measurement measurement, Facet[]? facets)
            => (Name, Timestamp, Measurements, Facets) 
                = (name, timestamp, new Measurement[] { measurement }, facets ?? Array.Empty<Facet>());

        public Timeseries(string name, Timestamp timestamp, Measurement[] measurements, Facet[]? facets)
            => (Name, Timestamp, Measurements, Facets)
                = (name, timestamp, measurements, facets ?? Array.Empty<Facet>());
    }
}
