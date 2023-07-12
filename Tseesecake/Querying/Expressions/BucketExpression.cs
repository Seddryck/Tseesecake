using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tseesecake.Modeling;
using Tseesecake.Querying.Projections;
using Tseesecake.Querying.Slicers;

namespace Tseesecake.Querying.Expressions
{
    internal class BucketExpression : BaseExpression
    {
        public TemporalSlicer Slicer { get; }

        public BucketExpression(TemporalSlicer bucketSlicer)
            => Slicer = bucketSlicer;
    }
}
