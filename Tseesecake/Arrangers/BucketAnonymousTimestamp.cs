using DubUrl.Querying.Dialects.Casters;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Tseesecake.Modeling.Catalog;
using Tseesecake.Modeling.Statements;
using Tseesecake.Modeling.Statements.Expressions;
using Tseesecake.Modeling.Statements.Slicers;

namespace Tseesecake.Arrangers
{
    [Polyglot]
    internal class BucketAnonymousTimestamp : ISelectArranger
    {
        public void Execute(SelectStatement statement)
        {
            var slicers = statement.Slicers;

            if (slicers is null || !slicers.Any(x => x is TemporalSlicer))
                return;

            var temporal = (TemporalSlicer) slicers.Single(x => x is TemporalSlicer);
            if (temporal.Timestamp is not AnonymousTimestamp)
                return;

            temporal.Timestamp= (statement.Timeseries as Timeseries)?.Timestamp ?? throw new InvalidOperationException();
        }
    }
}
