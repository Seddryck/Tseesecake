using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tseesecake.Modeling;

namespace Tseesecake.Querying.Filters
{
    internal class RangeTemporizer : Temporizer
    {
        protected DateTime Start { get; }
        protected DateTime End { get; }

        public override string Label { get => $"{Reference} BETWEEN {Start} AND {End}"; }

        public RangeTemporizer(Timestamp timestamp, DateTime start, DateTime end)
            : base(timestamp) { (Start, End) = (start, end); }
    }
}
