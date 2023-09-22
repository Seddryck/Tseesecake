using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tseesecake.Modeling.Catalog;
using Tseesecake.Modeling.Statements.Arguments;
using Tseesecake.Querying.Filters;

namespace Tseesecake.Querying.Slicers
{
    internal class TruncationTemporalSlicer : TemporalSlicer
    {
        public TruncationTemporal Truncation { get; }

        public override string Template { get => nameof(TruncationTemporalSlicer); }

        public TruncationTemporalSlicer(Timestamp timestamp, TruncationTemporal truncation)
            : base(timestamp) { Truncation = truncation; }
    }
}
