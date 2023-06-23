using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tseesecake.Modeling;

namespace Tseesecake.Querying.Filters
{
    internal class BeforeTemporizer : Temporizer
    {
        protected DateTime Instant { get; }

        public override string Template { get => nameof(BeforeTemporizer); }

        public BeforeTemporizer(Timestamp timestamp, DateTime instant)
            : base(timestamp) { (Instant) = (instant); }
    }
}
