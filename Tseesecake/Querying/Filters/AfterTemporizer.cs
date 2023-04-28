﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tseesecake.Modeling;

namespace Tseesecake.Querying.Filters
{
    internal class AfterTemporizer : Temporizer
    {
        protected DateTime Instant { get; }

        public override string Label { get => $"{Reference} > {Instant}"; }

        public AfterTemporizer(Timestamp timestamp, DateTime instant)
            : base(timestamp) { (Instant) = (instant); }
    }
}
