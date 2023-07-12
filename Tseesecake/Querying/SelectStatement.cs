using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tseesecake.Modeling;
using Tseesecake.Querying.Filters;
using Tseesecake.Querying.Ordering;
using Tseesecake.Querying.Projections;
using Tseesecake.Querying.Restrictions;
using Tseesecake.Querying.Slicers;
using Tseesecake.Querying.Windows;

namespace Tseesecake.Querying
{
    public class SelectStatement
    {
        public Timeseries Timeseries { get; set; }
        public List<IProjection> Projections { get; }
        public List<IFilter> Filters { get; }
        public List<ISlicer> Slicers { get; }
        public List<IFilter> GroupFilters { get; }
        public List<NamedWindow> Windows { get; }
        public List<IFilter> Qualifiers { get; }
        public List<IOrderBy> Orders { get; }
        public IRestriction? Restriction { get; }

        public SelectStatement(Timeseries timeseries, IProjection[] projections)
            : this(timeseries, projections, null) { }

        public SelectStatement(Timeseries timeseries, IProjection[] projections, IFilter[]? filters)
            : this(timeseries, projections, filters, Array.Empty<ISlicer>()) { }

        public SelectStatement(Timeseries timeseries, IProjection[] projections, IFilter[]? filters, ISlicer[]? slicers)
            : this(timeseries, projections, filters, slicers, Array.Empty<IFilter>(), Array.Empty<NamedWindow>(), Array.Empty<IFilter>(), Array.Empty<IOrderBy>(), null) { }

        public SelectStatement(Timeseries timeseries, IProjection[] projections, IFilter[]? filters, ISlicer[]? slicers, NamedWindow[]? namedWindows)
                    : this(timeseries, projections, filters, slicers, Array.Empty<IFilter>(), namedWindows, Array.Empty<IFilter>(), Array.Empty<IOrderBy>(), null) { }

        public SelectStatement(Timeseries timeseries, IProjection[] projections, IFilter[]? filters, ISlicer[]? slicers, IFilter[]? groupFilters)
            : this(timeseries, projections, filters, slicers, groupFilters, Array.Empty<NamedWindow>(), Array.Empty<IFilter>(), Array.Empty<IOrderBy>(), null) { }

        public SelectStatement(Timeseries timeseries, IProjection[] projections, IFilter[]? filters, ISlicer[]? slicers, IFilter[]? groupFilters, NamedWindow[]? namedWindows, IFilter[]? qualifiers, IOrderBy[]? orders, IRestriction? restriction = null)
            => (Timeseries, Projections, Filters, Slicers, GroupFilters, Windows, Qualifiers, Orders, Restriction) =
                    (timeseries
                        , projections.ToList()
                        , filters?.Length > 0 ? filters.ToList() : new List<IFilter>()
                        , slicers?.Length > 0 ? slicers.ToList() : new List<ISlicer>()
                        , groupFilters?.Length > 0 ? groupFilters.ToList() : new List<IFilter>()
                        , namedWindows?.Length > 0 ? namedWindows.ToList() : new List<NamedWindow>()
                        , qualifiers?.Length > 0 ? qualifiers.ToList() : new List<IFilter>()
                        , orders?.Length > 0 ? orders.ToList() : new List<IOrderBy>()
                        , restriction
            );
    }
}
