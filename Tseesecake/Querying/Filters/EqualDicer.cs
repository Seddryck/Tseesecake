﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tseesecake.Modeling;

namespace Tseesecake.Querying.Filters
{
    internal class EqualDicer : SingleDicer
    {
        public override string Template { get => nameof(EqualDicer); }

        public EqualDicer(Facet facet, string value)
            : base(facet, value) { }
    }
}
