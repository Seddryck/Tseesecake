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
        protected Timestamp Timestamp { get; }

        protected virtual string Reference { get => Timestamp.Name; }
        public abstract string Label { get; }

        public Temporizer(Timestamp timestamp)
            => (Timestamp) = (timestamp);
    }
}
