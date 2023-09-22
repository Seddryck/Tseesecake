using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using Tseesecake.Modeling.Catalog;

namespace Tseesecake.Modeling.Statements
{
    public class TimeseriesReference : IReference<Timeseries>, ITimeseries
    {
        public string Name { get; }

        public TimeseriesReference(string name)
            => Name = name;
    }
}
