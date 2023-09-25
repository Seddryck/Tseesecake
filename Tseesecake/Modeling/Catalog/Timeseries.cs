using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tseesecake.Modeling.Statements;

namespace Tseesecake.Modeling.Catalog
{
    public class Timeseries : ICatalogItem, ITimeseries
    {
        public string Name { get; }
        public Timestamp Timestamp { get; }
        public Measurement[] Measurements { get; }
        public Facet[] Facets { get; } = Array.Empty<Facet>();
        public IColumn[] Columns
            => Enumerable.Empty<IColumn>().Append(Timestamp).Union(Facets).Union(Measurements).ToArray();

        public Timeseries(string name, Timestamp timestamp, Measurement measurement)
            : this(name, timestamp, measurement, null) { }

        public Timeseries(string name, Timestamp timestamp, Measurement measurement, Facet[]? facets)
            => (Name, Timestamp, Measurements, Facets)
                = (name, timestamp, new Measurement[] { measurement }, facets ?? Array.Empty<Facet>());

        public Timeseries(string name, Timestamp timestamp, Measurement[] measurements, Facet[]? facets)
            => (Name, Timestamp, Measurements, Facets)
                = (name, timestamp, measurements, facets ?? Array.Empty<Facet>());
    }
}
