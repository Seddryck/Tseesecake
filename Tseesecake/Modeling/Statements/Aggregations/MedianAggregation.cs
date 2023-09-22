using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tseesecake.Querying.Expressions;

namespace Tseesecake.Modeling.Statements.Aggregations
{
    internal class MedianAggregation : BaseAggregation
    {
        public MedianAggregation(IExpression expression)
            : base(expression) { }
    }
}
