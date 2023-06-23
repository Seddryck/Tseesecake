using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tseesecake.Modeling;

namespace Tseesecake.Querying.Filters
{
    internal abstract class Temporizer : IFilter
    {
        public Timestamp Timestamp { get; }
        public abstract string Template { get; }

        public Temporizer(Timestamp timestamp)
            => (Timestamp) = (timestamp);
    }
}
