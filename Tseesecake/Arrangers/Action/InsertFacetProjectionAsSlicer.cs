using DubUrl.Querying.Dialects.Casters;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Net.Sockets;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Tseesecake.Modeling.Catalog;
using Tseesecake.Modeling.Statements;
using Tseesecake.Modeling.Statements.Expressions;
using Tseesecake.Modeling.Statements.Projections;
using Tseesecake.Modeling.Statements.Slicers;

namespace Tseesecake.Arrangers.Action
{
    [Polyglot]
    internal class InsertFacetProjectionAsSlicer : IActionArranger<ISlicer[]>
    {
        protected bool HasAggregation { get; }
        protected Facet[] Facets { get; }

        public InsertFacetProjectionAsSlicer(SelectStatement statement)
            => (HasAggregation, Facets)
                = (statement.Projections.Any(x => x.Expression is AggregationExpression),
                   statement.Projections
                    .Where(x => x.Expression is ColumnExpression).Select(x => x.Expression).Cast<ColumnExpression>()
                    .Where(x => x.Column is Facet).Select(x => x.Column).Cast<Facet>().ToArray());

        public ISlicer[] Execute(ISlicer[] obj)
        {
            if (!HasAggregation)
                return obj;

            var slicers = obj.ToList();
            foreach (var facet in Facets)
            {
                if (!slicers.Where(x => x is FacetSlicer).Cast<FacetSlicer>().Any(x => x.Facet == facet))
                    slicers.Add(new FacetSlicer(facet));
            }
            return slicers.ToArray();
        }

        public object Execute(object obj)
        {
            if (obj is ISlicer[] slicers)
                Execute(slicers);
            return obj;
        }
    }
}