using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tseesecake.Modeling;

namespace Tseesecake.Querying.Filters
{
    internal abstract class Dicer : IFilter
    {
        protected Facet Facet { get; }

        protected virtual string Reference { get => Facet.Name; }
        protected abstract string Operator { get; }
        protected abstract string Operand { get; }

        public string Label { get => $"{Reference} {Operator} {Operand}"; }

        public Dicer(Facet facet)
            => (Facet) = (facet);
    }
}
