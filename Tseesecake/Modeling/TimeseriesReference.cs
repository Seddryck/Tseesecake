using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tseesecake.Modeling
{
    public class TimeseriesReference : Timeseries
    {
        public TimeseriesReference(string name)
            : base(name, new Timestamp(""), new Measurement("")) { }
    }
}
