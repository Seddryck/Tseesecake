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
    [Polyglot]
    internal class BucketAsProjection : ISelectArranger
    {
        public void Execute(SelectStatement statement)
        {
            if (statement.Slicers is null)
                return;
            
            var slicer = statement.Slicers.Where(x => x is TemporalSlicer).Cast<TemporalSlicer>().SingleOrDefault();
            if (slicer is null)
                return;

            statement.Projections.Insert(0, new ExpressionProjection(new BucketExpression(slicer), statement.Timeseries.Timestamp.Name));
        }
    }
}
