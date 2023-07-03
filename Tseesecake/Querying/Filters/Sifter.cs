using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tseesecake.Modeling;

namespace Tseesecake.Querying.Filters
{
    public abstract class Sifter : IFilter
    {
        public Measurement Measurement { get; }

        public abstract string Template { get; }

        public Sifter(Measurement measurement)
            => (Measurement) = (measurement);
    }
}
