using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tseesecake.Modeling;

namespace Tseesecake.Querying.Slicers
{
    internal abstract class TemporalSlicer : ISlicer
    {
        public Timestamp Timestamp { get; }

        public abstract string Template { get; }

        public TemporalSlicer(Timestamp timestamp)
            => Timestamp = timestamp;
    }
}
