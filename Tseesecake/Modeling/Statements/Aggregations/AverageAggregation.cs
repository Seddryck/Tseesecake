using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tseesecake.Modeling.Statements.Expressions;

namespace Tseesecake.Modeling.Statements.Aggregations
{
    internal class AverageAggregation : BaseAggregation
    {
        public AverageAggregation(IExpression expression)
            : base(expression) { }
    }
}
