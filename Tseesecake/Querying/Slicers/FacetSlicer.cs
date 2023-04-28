using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tseesecake.Modeling;

namespace Tseesecake.Querying.Slicers
{
    internal class FacetSlicer : ISlicer
    {
        protected Facet Facet { get; }

        public string Label { get => Facet.Name; }

        public FacetSlicer(Facet facet)
            => Facet = facet;
    }
}
