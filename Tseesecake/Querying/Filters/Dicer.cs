using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tseesecake.Modeling.Catalog;

namespace Tseesecake.Querying.Filters
{
    internal abstract class Dicer : IFilter
    {
        public Facet Facet { get; }

        public abstract string Template { get; }

        public Dicer(Facet facet)
            => (Facet) = (facet);
    }
}
