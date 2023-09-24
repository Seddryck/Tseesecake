using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tseesecake.Modeling.Catalog;

namespace Tseesecake.Modeling.Statements.Filters
{
    internal class DifferentDicer : SingleDicer
    {
        public override string Template { get => nameof(DifferentDicer); }

        public DifferentDicer(Facet facet, string value)
            : base(facet, value) { }
    }
}
