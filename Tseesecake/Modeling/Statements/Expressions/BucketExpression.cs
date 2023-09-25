using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tseesecake.Modeling.Statements.Slicers;

namespace Tseesecake.Modeling.Statements.Expressions
{
    internal class BucketExpression : BaseExpression
    {
        public TemporalSlicer Slicer { get; }

        public BucketExpression(TemporalSlicer bucketSlicer)
            => Slicer = bucketSlicer;
    }
}
