﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tseesecake.Modeling.Catalog;

namespace Tseesecake.Modeling.Statements.Filters
{
    internal class SinceTemporizer : Temporizer
    {
        public TimeSpan Interval { get; }

        public override string Template { get => nameof(SinceTemporizer); }

        public SinceTemporizer(Timestamp timestamp, TimeSpan interval)
            : base(timestamp) { Interval = interval; }
    }
}
