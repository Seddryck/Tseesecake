using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Tseesecake.Modeling.Catalog;
using Tseesecake.Modeling.Statements.Expressions;
using Tseesecake.Modeling.Statements;
using Tseesecake.Modeling.Statements.Slicers;
using Tseesecake.Modeling.Statements.Projections;
using Tseesecake.Modeling.Statements.Filters;

namespace Tseesecake.Arrangers.Action
{
    [Polyglot]
    internal class InsertBucketAsProjection : IActionArranger<IProjection[]>
    {
        protected TemporalSlicer? Bucket { get; }
        protected Timestamp Timestamp { get; }

        public InsertBucketAsProjection(SelectStatement statement)
            => (Bucket, Timestamp) = 
                (
                    statement.Slicers?.Where(x => x is TemporalSlicer).Cast<TemporalSlicer>().SingleOrDefault(),
                    (statement.Timeseries as Timeseries)!.Timestamp
                );

        public IProjection[] Execute(IProjection[] obj)
        {
            if (Bucket is null)
                return obj;
            var list = obj.ToList();
            Bucket.Timestamp = Timestamp;   
            list.Insert(0, new Projection(new BucketExpression(Bucket), Timestamp.Name));
            return list.ToArray();
        }

        public object Execute(object obj)
        {
            if (obj is List<IProjection> projections)
                obj = Execute(projections.ToArray()).ToList();
            return obj;
        }
    }
}
