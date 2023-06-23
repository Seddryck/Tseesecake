﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tseesecake.Modeling;
using Tseesecake.Querying.Filters;
using Tseesecake.Querying.Slicers;

namespace Tseesecake.Querying
{
    public class SelectStatement
    {
        public Timeseries Timeseries { get; }
        public IProjection[] Projections { get; }
        public IFilter[]? Filters { get; }
        public ISlicer[]? Slicers { get; }

        public SelectStatement(Timeseries timeseries, IProjection[] projections)
            : this(timeseries, projections, null) { }

        public SelectStatement(Timeseries timeseries, IProjection[] projections, IFilter[]? filters)
            : this(timeseries, projections, filters, null) { }

        public SelectStatement(Timeseries timeseries, IProjection[] projections, IFilter[]? filters, ISlicer[]? slicers)
            => (Timeseries, Projections, Filters, Slicers) = (timeseries, projections, filters?.Length>0 ? filters : null, slicers?.Length > 0 ? slicers : null);
    }
}