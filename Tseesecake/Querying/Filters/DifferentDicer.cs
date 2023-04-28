using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tseesecake.Modeling;

namespace Tseesecake.Querying.Filters
{
    internal class DifferentDicer : SingleDicer
    {
        protected override string Operator { get => "!="; }

        public DifferentDicer(Facet facet, string value)
            : base(facet, value) { }
    }
}
