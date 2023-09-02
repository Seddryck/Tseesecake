using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tseesecake.Modeling
{
    internal class ShowFieldsTimeseries : IShowStatement
    {
        public string TimeseriesName { get; }

        public ShowFieldsTimeseries(string timeseriesName)
            => TimeseriesName = timeseriesName;
    }
}
