using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tseesecake.Engine.DuckDb;
using Tseesecake.Modeling;

namespace Tseesecake.Querying.Filters
{
    internal class SinceTemporizer : Temporizer
    {
        protected TimeSpan Interval { get; }

        public override string Label { get => $"age({Reference}) < {Interval.ToIntervalDuckDb()}"; }

        public SinceTemporizer(Timestamp timestamp, TimeSpan interval)
            : base(timestamp) { (Interval) = (interval); }
    }
}
