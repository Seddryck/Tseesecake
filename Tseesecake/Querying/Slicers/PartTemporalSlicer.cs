using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tseesecake.Modeling;

namespace Tseesecake.Querying.Slicers
{
    internal class PartTemporalSlicer : TemporalSlicer
    {
        protected string Part { get; }

        public override string Label { get => $"date_part('{Part}', {Timestamp.Name})"; }

        public PartTemporalSlicer(Timestamp timestamp, string part)
            : base(timestamp) { Part = part; }
    }
}
