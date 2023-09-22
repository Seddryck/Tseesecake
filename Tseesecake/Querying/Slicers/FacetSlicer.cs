using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tseesecake.Modeling.Catalog;

namespace Tseesecake.Querying.Slicers
{
    internal class FacetSlicer : ISlicer
    {
        public Facet Facet { get; }

        public string Template { get => nameof(FacetSlicer); }

        public FacetSlicer(Facet facet)
            => Facet = facet;
    }
}
