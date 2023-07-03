using Sprache;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Tseesecake.Modeling;
using Tseesecake.Querying.Expressions;
using Tseesecake.Querying.Ordering;
using Tseesecake.Querying.Projections;
using Tseesecake.Querying.Slicers;

namespace Tseesecake.Parsing.Query
{
    internal class SlicerParser
    {
        public static Parser<ISlicer> Facet = 
            Grammar.Identifier.Select(column => new FacetSlicer(new Facet(column)));
    }
}
