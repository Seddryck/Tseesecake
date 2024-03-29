﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tseesecake.Modeling.Catalog;
using Tseesecake.Modeling.Statements.Arguments;

namespace Tseesecake.Modeling.Statements.Slicers
{
    internal class CyclicTemporalSlicer : TemporalSlicer
    {
        public CyclicTemporal Cyclic { get; }

        public override string Template { get => nameof(CyclicTemporalSlicer); }

        public CyclicTemporalSlicer(Timestamp timestamp, CyclicTemporal cyclic)
            : base(timestamp) { Cyclic = cyclic; }
    }
}
