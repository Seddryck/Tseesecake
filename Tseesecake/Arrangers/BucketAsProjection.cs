using DubUrl.Querying.Dialects.Casters;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Tseesecake.Modeling;
using Tseesecake.Modeling.Catalog;
using Tseesecake.Modeling.Statements.ColumnExpressions;
using Tseesecake.Modeling.Statements;
using Tseesecake.Modeling.Statements.Slicers;
using Tseesecake.Modeling.Statements.Projections;

namespace Tseesecake.Arrangers
{
    [Polyglot]
    internal class BucketAsProjection : ISelectArranger
    {
        public void Execute(SelectStatement statement)
        {
            if (statement.Timeseries is not Timeseries)
                throw new InvalidOperationException();

            if (statement.Slicers is null)
                return;
            
            var slicer = statement.Slicers.Where(x => x is TemporalSlicer).Cast<TemporalSlicer>().SingleOrDefault();
            if (slicer is null)
                return;

            statement.Projections.Insert(0, new Projection(new BucketExpression(slicer), (statement.Timeseries as Timeseries)!.Timestamp.Name));
        }
    }
}
