using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tseesecake.Modeling;
using Tseesecake.Querying.Filters;

namespace Tseesecake.Querying.Slicers
{
    internal class PartTemporalSlicer : TemporalSlicer
    {
        public string Part { get; }

        public override string Template { get => nameof(PartTemporalSlicer); }

        public PartTemporalSlicer(Timestamp timestamp, string part)
            : base(timestamp) { Part = part; }
    }
}
