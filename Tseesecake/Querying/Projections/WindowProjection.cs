﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tseesecake.Modeling;
using Tseesecake.Querying.Aggregations;
using Tseesecake.Querying.Filters;
using Tseesecake.Querying.Ordering;
using Tseesecake.Querying.Slicers;
using Tseesecake.Querying.WindowFunctions;

namespace Tseesecake.Querying.Projections
{
    internal class WindowProjection : IProjection
    {
        public IWindowFunction WindowFunction { get; }
        public IOrderBy[]? OrderBys { get; }
        public ISlicer[]? PartitionBys { get; }
        public string Alias { get; }

        public string Template { get => nameof(WindowProjection); }

        public WindowProjection(IWindowFunction windowFunction, string alias)
            : this(windowFunction, null, null, alias) { }

        public WindowProjection(IWindowFunction windowFunction, IOrderBy[]? orderBys, ISlicer[]? partitionBys, string alias)
            => (WindowFunction, OrderBys, PartitionBys, Alias) 
                = (windowFunction, orderBys?.Length == 0 ? null : orderBys, partitionBys?.Length == 0 ? null : partitionBys, alias);
    }
}
