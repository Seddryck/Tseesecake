using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tseesecake.Modeling.Catalog;

namespace Tseesecake.Querying.Filters
{
    internal class AfterTemporizer : Temporizer
    {
        public DateTime Instant { get; }

        public override string Template { get => nameof(AfterTemporizer); }

        public AfterTemporizer(Timestamp timestamp, DateTime instant)
            : base(timestamp) { (Instant) = (instant); }
    }
}
