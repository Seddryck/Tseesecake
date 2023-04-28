using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tseesecake.Modeling;

namespace Tseesecake.Querying.Slicers
{
    internal class TruncateTemporalSlicer : TemporalSlicer
    {
        protected string Precision { get; }

        public override string Label { get => $"date_trunc('{Precision}', {Timestamp.Name})"; }

        public TruncateTemporalSlicer(Timestamp timestamp, string precision)
            : base(timestamp) { Precision = precision; }
    }
}
