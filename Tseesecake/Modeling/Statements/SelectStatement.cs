using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tseesecake.Modeling.Catalog;
using Tseesecake.Modeling.Statements.Filters;
using Tseesecake.Modeling.Statements.Ordering;
using Tseesecake.Modeling.Statements.Projections;
using Tseesecake.Modeling.Statements.Restrictions;
using Tseesecake.Modeling.Statements.Slicers;
using Tseesecake.Modeling.Statements.Windows;

namespace Tseesecake.Modeling.Statements
{
    public class SelectStatement : ISelectStatement
    {
        public ITimeseries Timeseries { get; set; }
        public List<IProjection> Projections { get; }
        public List<IFilter> Filters { get; }
        public List<ISlicer> Slicers { get; }
        public List<IFilter> GroupFilters { get; }
        public List<NamedWindow> Windows { get; }
        public List<IFilter> Qualifiers { get; }
        public List<IOrderBy> Orders { get; }
        public IRestriction? Restriction { get; }
        public List<VirtualMeasurement> VirtualMeasurements { get; set; }

        public SelectStatement(ITimeseries timeseries, IProjection[] projections)
            : this(timeseries, projections, null, null, null, null, null, null, null, null) { }

        public SelectStatement(ITimeseries timeseries, IProjection[] projections, VirtualMeasurement[] expressions)
            : this(timeseries, projections, null, null, null, null, null, null, null, expressions) { }

        public SelectStatement(ITimeseries timeseries, IProjection[] projections, IFilter[]? filters)
            : this(timeseries, projections, filters, Array.Empty<ISlicer>()) { }

        public SelectStatement(ITimeseries timeseries, IProjection[] projections, IFilter[]? filters, ISlicer[]? slicers)
            : this(timeseries, projections, filters, slicers, Array.Empty<IFilter>(), Array.Empty<NamedWindow>(), Array.Empty<IFilter>(), Array.Empty<IOrderBy>(), null, null) { }

        public SelectStatement(ITimeseries timeseries, IProjection[] projections, IFilter[]? filters, ISlicer[]? slicers, NamedWindow[]? namedWindows)
            : this(timeseries, projections, filters, slicers, Array.Empty<IFilter>(), namedWindows, Array.Empty<IFilter>(), Array.Empty<IOrderBy>(), null, null) { }

        public SelectStatement(ITimeseries timeseries, IProjection[] projections, IFilter[]? filters, ISlicer[]? slicers, IFilter[]? groupFilters)
            : this(timeseries, projections, filters, slicers, groupFilters, Array.Empty<NamedWindow>(), Array.Empty<IFilter>(), Array.Empty<IOrderBy>(), null, null) { }

        public SelectStatement(ITimeseries timeseries, IProjection[] projections, IFilter[]? filters, ISlicer[]? slicers, IFilter[]? groupFilters, NamedWindow[]? namedWindows, IFilter[]? qualifiers, IOrderBy[]? orders, IRestriction? restriction = null, VirtualMeasurement[]? expressions = null)
            => (Timeseries, Projections, Filters, Slicers, GroupFilters, Windows, Qualifiers, Orders, Restriction, VirtualMeasurements) =
                    (timeseries
                        , projections.ToList()
                        , filters?.Length > 0 ? filters.ToList() : new List<IFilter>()
                        , slicers?.Length > 0 ? slicers.ToList() : new List<ISlicer>()
                        , groupFilters?.Length > 0 ? groupFilters.ToList() : new List<IFilter>()
                        , namedWindows?.Length > 0 ? namedWindows.ToList() : new List<NamedWindow>()
                        , qualifiers?.Length > 0 ? qualifiers.ToList() : new List<IFilter>()
                        , orders?.Length > 0 ? orders.ToList() : new List<IOrderBy>()
                        , restriction
                        , expressions?.Length > 0 ? expressions.ToList() : new List<VirtualMeasurement>()
            );
    }
}
