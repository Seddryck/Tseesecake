using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tseesecake.Modeling.Catalog;

namespace Tseesecake.Modeling.Statements.Filters
{
    internal abstract class SingleDicer : Dicer
    {
        public string Value { get; }

        public SingleDicer(Facet facet, string value)
            : base(facet) { Value = value; }
    }
}
