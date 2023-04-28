using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tseesecake.Modeling;

namespace Tseesecake.Querying.Filters
{
    internal abstract class ManyDicer : Dicer
    {
        protected string[] Values { get; set; }
        protected override string Operand { get => $"('{string.Join("', '", Values)}')"; }

        public ManyDicer(Facet facet, string[] values)
            : base(facet) { Values = values; }
    }
}
