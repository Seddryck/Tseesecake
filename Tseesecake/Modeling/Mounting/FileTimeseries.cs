using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tseesecake.Modeling.Catalog;

namespace Tseesecake.Modeling.Mounting
{
    public class FileTimeseries : Timeseries
    {
        public IFileSource File { get; }

        public FileTimeseries(string name, Timestamp timestamp, Measurement measurement, Facet[]? facets, IFileSource file)
            : base(name, timestamp, measurement, facets) { File = file; }

        public FileTimeseries(string name, Timestamp timestamp, Measurement[] measurements, Facet[]? facets, IFileSource file)
            : base(name, timestamp, measurements, facets) { File = file; }
    }
}
