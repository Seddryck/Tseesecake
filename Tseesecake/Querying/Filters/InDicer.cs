using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tseesecake.Modeling;

namespace Tseesecake.Querying.Filters
{
    internal class InDicer : ManyDicer
    {
        protected override string Operator { get => "IN"; }

        public InDicer(Facet facet, string[] values)
            : base(facet, values) { }
    }
}
