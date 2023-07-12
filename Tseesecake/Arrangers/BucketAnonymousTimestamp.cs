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

            temporal.Timestamp= statement.Timeseries.Timestamp;
        }
    }
}
