﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tseesecake.Modeling.Catalog;

namespace Tseesecake.Modeling.Statements.Filters
{
    internal class InDicer : ManyDicer
    {
        public override string Template { get => nameof(InDicer); }

        public InDicer(Facet facet, string[] values)
            : base(facet, values) { }
    }
}
