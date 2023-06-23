using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tseesecake.Modeling;
using Tseesecake.Querying.Filters;

namespace Tseesecake.Querying.Slicers
{
    internal class TruncateTemporalSlicer : TemporalSlicer
    {
        public string Precision { get; }

        public override string Template { get => nameof(TruncateTemporalSlicer); }

        public TruncateTemporalSlicer(Timestamp timestamp, string precision)
            : base(timestamp) { Precision = precision; }
    }
}
