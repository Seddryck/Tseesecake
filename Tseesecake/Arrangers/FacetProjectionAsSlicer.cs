using DubUrl.Querying.Dialects.Casters;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Tseesecake.Modeling;
using Tseesecake.Querying;
using Tseesecake.Querying.Expressions;
using Tseesecake.Querying.Projections;
using Tseesecake.Querying.Slicers;

namespace Tseesecake.Arrangers
{
    internal class FacetProjectionAsSlicer : ISelectArranger
    {
        public void Execute(SelectStatement statement)
        {
            if (!statement.Projections.Any(x => x is AggregationProjection))
                return;

            var projections = statement.Projections
                .Where(x => x is ColumnProjection).Cast<ColumnProjection>()
                .Where(x => x.Expression is ColumnExpression).Select(x => x.Expression).Cast<ColumnExpression>()
                .Where(x => x.Column is Facet).Select(x => x.Column).Cast<Facet>();

            if (!projections.Any())
                return;

            var slicers = statement.Slicers
                .Where(x => x is FacetSlicer).Cast<FacetSlicer>()
                .Select(x => x.Facet);

            foreach (var projection in projections)
            {
                if (!slicers.Contains(projection))
                    statement.Slicers.Add(new FacetSlicer(projection));
            }
        }
    }
}
