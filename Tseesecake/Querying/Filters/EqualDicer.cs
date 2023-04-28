using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tseesecake.Modeling;

namespace Tseesecake.Querying.Filters
{
    internal class EqualDicer : SingleDicer
    {
        protected override string Operator { get => "="; }

        public EqualDicer(Facet facet, string value)
            : base(facet, value) { }
    }
}
