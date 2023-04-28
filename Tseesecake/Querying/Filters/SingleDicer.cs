using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tseesecake.Modeling;

namespace Tseesecake.Querying.Filters
{
    internal abstract class SingleDicer : Dicer
    {
        protected string Value { get; }

        protected override string Operand { get => $"'{Value}'"; }

        public SingleDicer(Facet facet, string value)
            : base(facet) { Value = value; }
    }
}
