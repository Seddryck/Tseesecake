using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tseesecake.Querying.Expressions;

namespace Tseesecake.Querying.Aggregations
{
    internal class MinAggregation : BaseAggregation
    {
        public MinAggregation(IExpression expression)
            : base(expression) { }
    }
}
