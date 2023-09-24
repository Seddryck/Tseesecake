using DubUrl.Querying.Dialects.Casters;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Tseesecake.Modeling.Catalog;
using Tseesecake.Modeling.Statements;
using Tseesecake.Modeling.Statements.Expressions;
using Tseesecake.Modeling.Statements.Projections;
using Tseesecake.Modeling.Statements.Slicers;

namespace Tseesecake.Arrangers
{
    [Polyglot]
    internal class FacetProjectionAsSlicer : ISelectArranger
    {
        public void Execute(SelectStatement statement)
        {
            if (!statement.Projections.Any(x => x.Expression is AggregationExpression))
                return;

            var facets = statement.Projections
                .Where(x => x.Expression is ColumnExpression).Select(x => x.Expression).Cast<ColumnExpression>()
                .Where(x => x.Column is Facet).Select(x => x.Column).Cast<Facet>();

            if (!facets.Any())
                return;

            var slicers = statement.Slicers
                .Where(x => x is FacetSlicer).Cast<FacetSlicer>()
                .Select(x => x.Facet);

            foreach (var facet in facets)
            {
                if (!slicers.Contains(facet))
                    statement.Slicers.Add(new FacetSlicer(facet));
            }
        }
    }
}