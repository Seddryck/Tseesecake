using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tseesecake.Modeling.Catalog;

namespace Tseesecake.Modeling.Statements.Filters
{
    internal class BeforeTemporizer : Temporizer
    {
        public DateTime Instant { get; }

        public override string Template { get => nameof(BeforeTemporizer); }

        public BeforeTemporizer(Timestamp timestamp, DateTime instant)
            : base(timestamp) { Instant = instant; }

        public override string ToString() => $"Tempo:'{Instant}'";
    }
}
