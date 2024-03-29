﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tseesecake.Modeling.Catalog;

namespace Tseesecake.Modeling.Statements.Filters
{
    internal abstract class ManyDicer : Dicer
    {
        public string[] Values { get; set; }

        public ManyDicer(Facet facet, string[] values)
            : base(facet) { Values = values; }
    }
}
