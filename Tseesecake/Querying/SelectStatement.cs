using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tseesecake.Modeling;
using Tseesecake.Querying.Filters;
using Tseesecake.Querying.Ordering;
using Tseesecake.Querying.Restrictions;
using Tseesecake.Querying.Slicers;

namespace Tseesecake.Querying
{
    public class SelectStatement
    {
        public Timeseries Timeseries { get; }
        public IProjection[] Projections { get; }
        public IFilter[]? Filters { get; }
        public ISlicer[]? Slicers { get; }
        public IFilter[]? GroupFilters { get; }
        public IOrderBy[]? Orders { get; }
        public IRestriction? Restriction { get; }

        public SelectStatement(Timeseries timeseries, IProjection[] projections)
            : this(timeseries, projections, null) { }

        public SelectStatement(Timeseries timeseries, IProjection[] projections, IFilter[]? filters)
            : this(timeseries, projections, filters, Array.Empty<ISlicer>()) { }

        public SelectStatement(Timeseries timeseries, IProjection[] projections, IFilter[]? filters, ISlicer[]? slicers)
            : this(timeseries, projections, filters, slicers, Array.Empty<IFilter>(), Array.Empty<IOrderBy>(), null) { }

        public SelectStatement(Timeseries timeseries, IProjection[] projections, IFilter[]? filters, ISlicer[]? slicers, IFilter[]? groupFilters)
            : this(timeseries, projections, filters, slicers, groupFilters, Array.Empty<IOrderBy>(), null) { }

        public SelectStatement(Timeseries timeseries, IProjection[] projections, IFilter[]? filters, ISlicer[]? slicers, IFilter[]? groupFilters, IOrderBy[]? orders, IRestriction? restriction = null)
            => (Timeseries, Projections, Filters, Slicers, GroupFilters, Orders, Restriction) = (timeseries, projections, filters?.Length > 0 ? filters : null, slicers?.Length > 0 ? slicers : null, groupFilters?.Length > 0 ? groupFilters : null, orders?.Length > 0 ? orders : null, restriction);
    }
}
