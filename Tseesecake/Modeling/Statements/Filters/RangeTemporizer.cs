using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tseesecake.Modeling.Catalog;

namespace Tseesecake.Modeling.Statements.Filters
{
    internal class RangeTemporizer : Temporizer
    {
        public DateTime Start { get; }
        public DateTime End { get; }

        public override string Template { get => nameof(RangeTemporizer); }

        public RangeTemporizer(Timestamp timestamp, DateTime start, DateTime end)
            : base(timestamp) { (Start, End) = (start, end); }
    }
}
